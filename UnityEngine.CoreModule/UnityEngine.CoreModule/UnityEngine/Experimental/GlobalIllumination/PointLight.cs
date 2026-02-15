using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003E6 RID: 998
	public struct PointLight
	{
		// Token: 0x04000D3E RID: 3390
		public int instanceID;

		// Token: 0x04000D3F RID: 3391
		public bool shadow;

		// Token: 0x04000D40 RID: 3392
		public LightMode mode;

		// Token: 0x04000D41 RID: 3393
		public Vector3 position;

		// Token: 0x04000D42 RID: 3394
		public Quaternion orientation;

		// Token: 0x04000D43 RID: 3395
		public LinearColor color;

		// Token: 0x04000D44 RID: 3396
		public LinearColor indirectColor;

		// Token: 0x04000D45 RID: 3397
		public float range;

		// Token: 0x04000D46 RID: 3398
		public float sphereRadius;

		// Token: 0x04000D47 RID: 3399
		public FalloffType falloff;
	}
}
