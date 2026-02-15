using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003E9 RID: 1001
	public struct DiscLight
	{
		// Token: 0x04000D60 RID: 3424
		public int instanceID;

		// Token: 0x04000D61 RID: 3425
		public bool shadow;

		// Token: 0x04000D62 RID: 3426
		public LightMode mode;

		// Token: 0x04000D63 RID: 3427
		public Vector3 position;

		// Token: 0x04000D64 RID: 3428
		public Quaternion orientation;

		// Token: 0x04000D65 RID: 3429
		public LinearColor color;

		// Token: 0x04000D66 RID: 3430
		public LinearColor indirectColor;

		// Token: 0x04000D67 RID: 3431
		public float range;

		// Token: 0x04000D68 RID: 3432
		public float radius;

		// Token: 0x04000D69 RID: 3433
		public FalloffType falloff;
	}
}
