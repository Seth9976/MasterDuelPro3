using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001CE RID: 462
	public struct PunctualLightData
	{
		// Token: 0x04000A15 RID: 2581
		public Vector3 wsPos;

		// Token: 0x04000A16 RID: 2582
		public float radius;

		// Token: 0x04000A17 RID: 2583
		public Vector4 color;

		// Token: 0x04000A18 RID: 2584
		public Vector4 attenuation;

		// Token: 0x04000A19 RID: 2585
		public Vector3 spotDirection;

		// Token: 0x04000A1A RID: 2586
		public int flags;

		// Token: 0x04000A1B RID: 2587
		public Vector4 occlusionProbeInfo;

		// Token: 0x04000A1C RID: 2588
		public uint layerMask;
	}
}
