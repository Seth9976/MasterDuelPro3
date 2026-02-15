using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000013 RID: 19
	public static class ShaderInput
	{
		// Token: 0x02000014 RID: 20
		[Obsolete("ShaderInput.ShadowData was deprecated. Shadow slice matrices and per-light shadow parameters are now passed to the GPU using entries in buffers m_AdditionalLightsWorldToShadow_SSBO and m_AdditionalShadowParams_SSBO", true)]
		public struct ShadowData
		{
			// Token: 0x04000069 RID: 105
			public Matrix4x4 worldToShadowMatrix;

			// Token: 0x0400006A RID: 106
			public Vector4 shadowParams;
		}

		// Token: 0x02000015 RID: 21
		[GenerateHLSL(PackingRules.Exact, false, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.universal/ShaderLibrary/ShaderTypes.cs")]
		public struct LightData
		{
			// Token: 0x0400006B RID: 107
			public Vector4 position;

			// Token: 0x0400006C RID: 108
			public Vector4 color;

			// Token: 0x0400006D RID: 109
			public Vector4 attenuation;

			// Token: 0x0400006E RID: 110
			public Vector4 spotDirection;

			// Token: 0x0400006F RID: 111
			public Vector4 occlusionProbeChannels;

			// Token: 0x04000070 RID: 112
			public uint layerMask;
		}
	}
}
