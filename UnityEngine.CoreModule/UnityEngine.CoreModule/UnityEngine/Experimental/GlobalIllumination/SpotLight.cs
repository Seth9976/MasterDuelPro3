using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003E7 RID: 999
	public struct SpotLight
	{
		// Token: 0x04000D48 RID: 3400
		public int instanceID;

		// Token: 0x04000D49 RID: 3401
		public bool shadow;

		// Token: 0x04000D4A RID: 3402
		public LightMode mode;

		// Token: 0x04000D4B RID: 3403
		public Vector3 position;

		// Token: 0x04000D4C RID: 3404
		public Quaternion orientation;

		// Token: 0x04000D4D RID: 3405
		public LinearColor color;

		// Token: 0x04000D4E RID: 3406
		public LinearColor indirectColor;

		// Token: 0x04000D4F RID: 3407
		public float range;

		// Token: 0x04000D50 RID: 3408
		public float sphereRadius;

		// Token: 0x04000D51 RID: 3409
		public float coneAngle;

		// Token: 0x04000D52 RID: 3410
		public float innerConeAngle;

		// Token: 0x04000D53 RID: 3411
		public FalloffType falloff;

		// Token: 0x04000D54 RID: 3412
		public AngularFalloffType angularFalloff;
	}
}
