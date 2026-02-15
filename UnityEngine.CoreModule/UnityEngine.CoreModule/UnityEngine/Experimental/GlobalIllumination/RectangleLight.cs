using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003E8 RID: 1000
	public struct RectangleLight
	{
		// Token: 0x04000D55 RID: 3413
		public int instanceID;

		// Token: 0x04000D56 RID: 3414
		public bool shadow;

		// Token: 0x04000D57 RID: 3415
		public LightMode mode;

		// Token: 0x04000D58 RID: 3416
		public Vector3 position;

		// Token: 0x04000D59 RID: 3417
		public Quaternion orientation;

		// Token: 0x04000D5A RID: 3418
		public LinearColor color;

		// Token: 0x04000D5B RID: 3419
		public LinearColor indirectColor;

		// Token: 0x04000D5C RID: 3420
		public float range;

		// Token: 0x04000D5D RID: 3421
		public float width;

		// Token: 0x04000D5E RID: 3422
		public float height;

		// Token: 0x04000D5F RID: 3423
		public FalloffType falloff;
	}
}
