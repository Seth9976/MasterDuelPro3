using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200011C RID: 284
	public struct ProbeVolumeSystemParameters
	{
		// Token: 0x04000501 RID: 1281
		public ProbeVolumeTextureMemoryBudget memoryBudget;

		// Token: 0x04000502 RID: 1282
		public ProbeVolumeBlendingTextureMemoryBudget blendingMemoryBudget;

		// Token: 0x04000503 RID: 1283
		public ProbeVolumeSHBands shBands;

		// Token: 0x04000504 RID: 1284
		public bool supportScenarios;

		// Token: 0x04000505 RID: 1285
		public bool supportScenarioBlending;

		// Token: 0x04000506 RID: 1286
		public bool supportGPUStreaming;

		// Token: 0x04000507 RID: 1287
		public bool supportDiskStreaming;

		// Token: 0x04000508 RID: 1288
		[Obsolete("This field is not used anymore.")]
		public Shader probeDebugShader;

		// Token: 0x04000509 RID: 1289
		[Obsolete("This field is not used anymore.")]
		public Shader probeSamplingDebugShader;

		// Token: 0x0400050A RID: 1290
		[Obsolete("This field is not used anymore.")]
		public Texture probeSamplingDebugTexture;

		// Token: 0x0400050B RID: 1291
		[Obsolete("This field is not used anymore.")]
		public Mesh probeSamplingDebugMesh;

		// Token: 0x0400050C RID: 1292
		[Obsolete("This field is not used anymore.")]
		public Shader offsetDebugShader;

		// Token: 0x0400050D RID: 1293
		[Obsolete("This field is not used anymore.")]
		public Shader fragmentationDebugShader;

		// Token: 0x0400050E RID: 1294
		[Obsolete("This field is not used anymore.")]
		public ComputeShader scenarioBlendingShader;

		// Token: 0x0400050F RID: 1295
		[Obsolete("This field is not used anymore.")]
		public ComputeShader streamingUploadShader;

		// Token: 0x04000510 RID: 1296
		[Obsolete("This field is not used anymore.")]
		public ProbeVolumeSceneData sceneData;

		// Token: 0x04000511 RID: 1297
		[Obsolete("This field is not used anymore. Used with the current Shader Stripping Settings. #from(2023.3)")]
		public bool supportsRuntimeDebug;
	}
}
