using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003ED RID: 1005
	public static class Lightmapping
	{
		// Token: 0x06001B36 RID: 6966 RVA: 0x0003C578 File Offset: 0x0003A778
		[RequiredByNativeCode]
		public static void SetDelegate(Lightmapping.RequestLightsDelegate del)
		{
			Lightmapping.s_RequestLightsDelegate = ((del != null) ? del : Lightmapping.s_DefaultDelegate);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x0003C58C File Offset: 0x0003A78C
		[RequiredByNativeCode]
		public static Lightmapping.RequestLightsDelegate GetDelegate()
		{
			return Lightmapping.s_RequestLightsDelegate;
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x0003C5A3 File Offset: 0x0003A7A3
		[RequiredByNativeCode]
		public static void ResetDelegate()
		{
			Lightmapping.s_RequestLightsDelegate = Lightmapping.s_DefaultDelegate;
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x0003C5B0 File Offset: 0x0003A7B0
		[RequiredByNativeCode]
		internal unsafe static void RequestLights(Light[] lights, IntPtr outLightsPtr, int outLightsCount)
		{
			NativeArray<LightDataGI> outLights = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<LightDataGI>((void*)outLightsPtr, outLightsCount, Allocator.None);
			Lightmapping.s_RequestLightsDelegate(lights, outLights);
		}

		// Token: 0x04000D7D RID: 3453
		[RequiredByNativeCode]
		private static readonly Lightmapping.RequestLightsDelegate s_DefaultDelegate = delegate(Light[] requests, NativeArray<LightDataGI> lightsOutput)
		{
			DirectionalLight dir = default(DirectionalLight);
			PointLight point = default(PointLight);
			SpotLight spot = default(SpotLight);
			RectangleLight rect = default(RectangleLight);
			DiscLight disc = default(DiscLight);
			Cookie cookie = default(Cookie);
			LightDataGI ld = default(LightDataGI);
			for (int i = 0; i < requests.Length; i++)
			{
				Light j = requests[i];
				switch (j.type)
				{
				case LightType.Spot:
					LightmapperUtils.Extract(j, ref spot);
					LightmapperUtils.Extract(j, out cookie);
					ld.Init(ref spot, ref cookie);
					break;
				case LightType.Directional:
					LightmapperUtils.Extract(j, ref dir);
					LightmapperUtils.Extract(j, out cookie);
					ld.Init(ref dir, ref cookie);
					break;
				case LightType.Point:
					LightmapperUtils.Extract(j, ref point);
					LightmapperUtils.Extract(j, out cookie);
					ld.Init(ref point, ref cookie);
					break;
				case LightType.Area:
					LightmapperUtils.Extract(j, ref rect);
					LightmapperUtils.Extract(j, out cookie);
					ld.Init(ref rect, ref cookie);
					break;
				case LightType.Disc:
					LightmapperUtils.Extract(j, ref disc);
					LightmapperUtils.Extract(j, out cookie);
					ld.Init(ref disc, ref cookie);
					break;
				default:
					ld.InitNoBake(j.GetInstanceID());
					break;
				}
				lightsOutput[i] = ld;
			}
		};

		// Token: 0x04000D7E RID: 3454
		[RequiredByNativeCode]
		private static Lightmapping.RequestLightsDelegate s_RequestLightsDelegate = Lightmapping.s_DefaultDelegate;

		// Token: 0x020003EE RID: 1006
		// (Invoke) Token: 0x06001B3C RID: 6972
		public delegate void RequestLightsDelegate(Light[] requests, NativeArray<LightDataGI> lightsOutput);
	}
}
