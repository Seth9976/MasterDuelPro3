using System;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x020000CC RID: 204
	internal struct RenderersParameters
	{
		// Token: 0x0600031B RID: 795 RVA: 0x000138E0 File Offset: 0x00011AE0
		public static GPUInstanceDataBuffer CreateInstanceDataBuffer(RenderersParameters.Flags flags, in InstanceNumInfo instanceNumInfo)
		{
			GPUInstanceDataBuffer gpuinstanceDataBuffer;
			using (GPUInstanceDataBufferBuilder builder = default(GPUInstanceDataBufferBuilder))
			{
				builder.AddComponent<Vector4>(RenderersParameters.ParamNames._BaseColor, false, false, InstanceType.MeshRenderer, InstanceComponentGroup.Default);
				builder.AddComponent<Vector4>(RenderersParameters.ParamNames.unity_SpecCube0_HDR, false, false, InstanceType.MeshRenderer, InstanceComponentGroup.Default);
				builder.AddComponent<SHCoefficients>(RenderersParameters.ParamNames.unity_SHCoefficients, true, true, InstanceType.MeshRenderer, InstanceComponentGroup.LightProbe);
				builder.AddComponent<Vector4>(RenderersParameters.ParamNames.unity_LightmapST, true, true, InstanceType.MeshRenderer, InstanceComponentGroup.Lightmap);
				builder.AddComponent<PackedMatrix>(RenderersParameters.ParamNames.unity_ObjectToWorld, true, true, InstanceType.MeshRenderer, InstanceComponentGroup.Default);
				builder.AddComponent<PackedMatrix>(RenderersParameters.ParamNames.unity_WorldToObject, true, true, InstanceType.MeshRenderer, InstanceComponentGroup.Default);
				builder.AddComponent<PackedMatrix>(RenderersParameters.ParamNames.unity_MatrixPreviousM, true, true, InstanceType.MeshRenderer, InstanceComponentGroup.Default);
				builder.AddComponent<PackedMatrix>(RenderersParameters.ParamNames.unity_MatrixPreviousMI, true, true, InstanceType.MeshRenderer, InstanceComponentGroup.Default);
				if ((flags & RenderersParameters.Flags.UseBoundingSphereParameter) != RenderersParameters.Flags.None)
				{
					builder.AddComponent<Vector4>(RenderersParameters.ParamNames.unity_WorldBoundingSphere, true, true, InstanceType.MeshRenderer, InstanceComponentGroup.Default);
				}
				for (int i = 0; i < 16; i++)
				{
					builder.AddComponent<Vector4>(RenderersParameters.ParamNames.DOTS_ST_WindParams[i], true, true, InstanceType.SpeedTree, InstanceComponentGroup.Wind);
				}
				for (int j = 0; j < 16; j++)
				{
					builder.AddComponent<Vector4>(RenderersParameters.ParamNames.DOTS_ST_WindHistoryParams[j], true, true, InstanceType.SpeedTree, InstanceComponentGroup.Wind);
				}
				gpuinstanceDataBuffer = builder.Build(in instanceNumInfo);
			}
			return gpuinstanceDataBuffer;
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000139F4 File Offset: 0x00011BF4
		public RenderersParameters(in GPUInstanceDataBuffer instanceDataBuffer)
		{
			this.lightmapScale = RenderersParameters.<.ctor>g__GetParamInfo|14_0(in instanceDataBuffer, RenderersParameters.ParamNames.unity_LightmapST, true);
			this.localToWorld = RenderersParameters.<.ctor>g__GetParamInfo|14_0(in instanceDataBuffer, RenderersParameters.ParamNames.unity_ObjectToWorld, true);
			this.worldToLocal = RenderersParameters.<.ctor>g__GetParamInfo|14_0(in instanceDataBuffer, RenderersParameters.ParamNames.unity_WorldToObject, true);
			this.matrixPreviousM = RenderersParameters.<.ctor>g__GetParamInfo|14_0(in instanceDataBuffer, RenderersParameters.ParamNames.unity_MatrixPreviousM, true);
			this.matrixPreviousMI = RenderersParameters.<.ctor>g__GetParamInfo|14_0(in instanceDataBuffer, RenderersParameters.ParamNames.unity_MatrixPreviousMI, true);
			this.shCoefficients = RenderersParameters.<.ctor>g__GetParamInfo|14_0(in instanceDataBuffer, RenderersParameters.ParamNames.unity_SHCoefficients, true);
			this.boundingSphere = RenderersParameters.<.ctor>g__GetParamInfo|14_0(in instanceDataBuffer, RenderersParameters.ParamNames.unity_WorldBoundingSphere, false);
			this.windParams = new RenderersParameters.ParamInfo[16];
			this.windHistoryParams = new RenderersParameters.ParamInfo[16];
			for (int i = 0; i < 16; i++)
			{
				this.windParams[i] = RenderersParameters.<.ctor>g__GetParamInfo|14_0(in instanceDataBuffer, RenderersParameters.ParamNames.DOTS_ST_WindParams[i], true);
				this.windHistoryParams[i] = RenderersParameters.<.ctor>g__GetParamInfo|14_0(in instanceDataBuffer, RenderersParameters.ParamNames.DOTS_ST_WindHistoryParams[i], true);
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00013AE8 File Offset: 0x00011CE8
		[CompilerGenerated]
		internal static RenderersParameters.ParamInfo <.ctor>g__GetParamInfo|14_0(in GPUInstanceDataBuffer instanceDataBuffer, int paramNameIdx, bool assertOnFail = true)
		{
			int gpuAddress = instanceDataBuffer.GetGpuAddress(paramNameIdx, assertOnFail);
			int index = instanceDataBuffer.GetPropertyIndex(paramNameIdx, assertOnFail);
			return new RenderersParameters.ParamInfo
			{
				index = index,
				gpuAddress = gpuAddress,
				uintOffset = gpuAddress / RenderersParameters.s_uintSize
			};
		}

		// Token: 0x04000410 RID: 1040
		private static int s_uintSize = UnsafeUtility.SizeOf<uint>();

		// Token: 0x04000411 RID: 1041
		public RenderersParameters.ParamInfo lightmapScale;

		// Token: 0x04000412 RID: 1042
		public RenderersParameters.ParamInfo localToWorld;

		// Token: 0x04000413 RID: 1043
		public RenderersParameters.ParamInfo worldToLocal;

		// Token: 0x04000414 RID: 1044
		public RenderersParameters.ParamInfo matrixPreviousM;

		// Token: 0x04000415 RID: 1045
		public RenderersParameters.ParamInfo matrixPreviousMI;

		// Token: 0x04000416 RID: 1046
		public RenderersParameters.ParamInfo shCoefficients;

		// Token: 0x04000417 RID: 1047
		public RenderersParameters.ParamInfo boundingSphere;

		// Token: 0x04000418 RID: 1048
		public RenderersParameters.ParamInfo[] windParams;

		// Token: 0x04000419 RID: 1049
		public RenderersParameters.ParamInfo[] windHistoryParams;

		// Token: 0x020000CD RID: 205
		[Flags]
		public enum Flags
		{
			// Token: 0x0400041B RID: 1051
			None = 0,
			// Token: 0x0400041C RID: 1052
			UseBoundingSphereParameter = 1
		}

		// Token: 0x020000CE RID: 206
		public static class ParamNames
		{
			// Token: 0x0600031F RID: 799 RVA: 0x00013B30 File Offset: 0x00011D30
			static ParamNames()
			{
				for (int i = 0; i < 16; i++)
				{
					RenderersParameters.ParamNames.DOTS_ST_WindParams[i] = Shader.PropertyToID(string.Format("DOTS_ST_WindParam{0}", i));
					RenderersParameters.ParamNames.DOTS_ST_WindHistoryParams[i] = Shader.PropertyToID(string.Format("DOTS_ST_WindHistoryParam{0}", i));
				}
			}

			// Token: 0x0400041D RID: 1053
			public static readonly int _BaseColor = Shader.PropertyToID("_BaseColor");

			// Token: 0x0400041E RID: 1054
			public static readonly int unity_SpecCube0_HDR = Shader.PropertyToID("unity_SpecCube0_HDR");

			// Token: 0x0400041F RID: 1055
			public static readonly int unity_SHCoefficients = Shader.PropertyToID("unity_SHCoefficients");

			// Token: 0x04000420 RID: 1056
			public static readonly int unity_LightmapST = Shader.PropertyToID("unity_LightmapST");

			// Token: 0x04000421 RID: 1057
			public static readonly int unity_ObjectToWorld = Shader.PropertyToID("unity_ObjectToWorld");

			// Token: 0x04000422 RID: 1058
			public static readonly int unity_WorldToObject = Shader.PropertyToID("unity_WorldToObject");

			// Token: 0x04000423 RID: 1059
			public static readonly int unity_MatrixPreviousM = Shader.PropertyToID("unity_MatrixPreviousM");

			// Token: 0x04000424 RID: 1060
			public static readonly int unity_MatrixPreviousMI = Shader.PropertyToID("unity_MatrixPreviousMI");

			// Token: 0x04000425 RID: 1061
			public static readonly int unity_WorldBoundingSphere = Shader.PropertyToID("unity_WorldBoundingSphere");

			// Token: 0x04000426 RID: 1062
			public static readonly int[] DOTS_ST_WindParams = new int[16];

			// Token: 0x04000427 RID: 1063
			public static readonly int[] DOTS_ST_WindHistoryParams = new int[16];
		}

		// Token: 0x020000CF RID: 207
		public struct ParamInfo
		{
			// Token: 0x1700006A RID: 106
			// (get) Token: 0x06000320 RID: 800 RVA: 0x00013C21 File Offset: 0x00011E21
			public bool valid
			{
				get
				{
					return this.index != 0;
				}
			}

			// Token: 0x04000428 RID: 1064
			public int index;

			// Token: 0x04000429 RID: 1065
			public int gpuAddress;

			// Token: 0x0400042A RID: 1066
			public int uintOffset;
		}
	}
}
