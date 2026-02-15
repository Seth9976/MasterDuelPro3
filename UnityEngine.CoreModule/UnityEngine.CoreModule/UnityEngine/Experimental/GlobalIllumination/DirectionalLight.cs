using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003E5 RID: 997
	public struct DirectionalLight
	{
		// Token: 0x04000D35 RID: 3381
		public int instanceID;

		// Token: 0x04000D36 RID: 3382
		public bool shadow;

		// Token: 0x04000D37 RID: 3383
		public LightMode mode;

		// Token: 0x04000D38 RID: 3384
		public Vector3 position;

		// Token: 0x04000D39 RID: 3385
		public Quaternion orientation;

		// Token: 0x04000D3A RID: 3386
		public LinearColor color;

		// Token: 0x04000D3B RID: 3387
		public LinearColor indirectColor;

		// Token: 0x04000D3C RID: 3388
		public float penumbraWidthRadian;

		// Token: 0x04000D3D RID: 3389
		[Obsolete("Directional lights support cookies now. In order to position the cookie projection in the world, a position and full orientation are necessary. Use the position and orientation members instead of the direction parameter.", true)]
		public Vector3 direction;
	}
}
