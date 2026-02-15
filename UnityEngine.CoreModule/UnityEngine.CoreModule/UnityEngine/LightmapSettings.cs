using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000E1 RID: 225
	[StaticAccessor("GetLightmapSettings()")]
	[NativeHeader("Runtime/Graphics/LightmapSettings.h")]
	public sealed class LightmapSettings : Object
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x00004DC7 File Offset: 0x00002FC7
		private LightmapSettings()
		{
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005D6 RID: 1494
		// (set) Token: 0x060005D7 RID: 1495
		public static extern LightmapData[] lightmaps
		{
			[FreeFunction]
			[MethodImpl(MethodImplOptions.InternalCall)]
			[return: Unmarshalled]
			get;
			[FreeFunction(ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			[param: Unmarshalled]
			set;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060005D8 RID: 1496
		// (set) Token: 0x060005D9 RID: 1497
		public static extern LightmapsMode lightmapsMode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[FreeFunction(ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		// (set) Token: 0x060005DB RID: 1499 RVA: 0x0000C9F8 File Offset: 0x0000ABF8
		public static LightProbes lightProbes
		{
			get
			{
				return Unmarshal.UnmarshalUnityObject<LightProbes>(LightmapSettings.get_lightProbes_Injected());
			}
			[FreeFunction]
			[NativeName("SetLightProbes")]
			set
			{
				LightmapSettings.set_lightProbes_Injected(Object.MarshalledUnityObject.Marshal<LightProbes>(value));
			}
		}

		// Token: 0x060005DC RID: 1500
		[NativeName("ResetAndAwakeFromLoad")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Reset();

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000CA10 File Offset: 0x0000AC10
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x00003D56 File Offset: 0x00001F56
		[Obsolete("Use lightmapsMode instead.", false)]
		public static LightmapsModeLegacy lightmapsModeLegacy
		{
			get
			{
				return LightmapsModeLegacy.Single;
			}
			set
			{
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000CA24 File Offset: 0x0000AC24
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x00003D56 File Offset: 0x00001F56
		[Obsolete("Use QualitySettings.desiredColorSpace instead.", false)]
		public static ColorSpace bakedColorSpace
		{
			get
			{
				return QualitySettings.desiredColorSpace;
			}
			set
			{
			}
		}

		// Token: 0x060005E1 RID: 1505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_lightProbes_Injected();

		// Token: 0x060005E2 RID: 1506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_lightProbes_Injected(IntPtr value);
	}
}
