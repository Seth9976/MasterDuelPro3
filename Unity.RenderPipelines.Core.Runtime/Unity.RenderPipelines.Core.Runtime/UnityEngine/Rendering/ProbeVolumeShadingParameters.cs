using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200011D RID: 285
	internal struct ProbeVolumeShadingParameters
	{
		// Token: 0x04000512 RID: 1298
		public float normalBias;

		// Token: 0x04000513 RID: 1299
		public float viewBias;

		// Token: 0x04000514 RID: 1300
		public bool scaleBiasByMinDistanceBetweenProbes;

		// Token: 0x04000515 RID: 1301
		public float samplingNoise;

		// Token: 0x04000516 RID: 1302
		public float weight;

		// Token: 0x04000517 RID: 1303
		public APVLeakReductionMode leakReductionMode;

		// Token: 0x04000518 RID: 1304
		public int frameIndexForNoise;

		// Token: 0x04000519 RID: 1305
		public float reflNormalizationLowerClamp;

		// Token: 0x0400051A RID: 1306
		public float reflNormalizationUpperClamp;

		// Token: 0x0400051B RID: 1307
		public float skyOcclusionIntensity;

		// Token: 0x0400051C RID: 1308
		public bool skyOcclusionShadingDirection;

		// Token: 0x0400051D RID: 1309
		public int regionCount;

		// Token: 0x0400051E RID: 1310
		public uint4 regionLayerMasks;

		// Token: 0x0400051F RID: 1311
		public Vector3 worldOffset;
	}
}
