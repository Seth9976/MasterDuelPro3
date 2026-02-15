using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000118 RID: 280
	internal class ProbeSamplingDebugData
	{
		// Token: 0x040004D0 RID: 1232
		public ProbeSamplingDebugUpdate update;

		// Token: 0x040004D1 RID: 1233
		public Vector2 coordinates = new Vector2(0.5f, 0.5f);

		// Token: 0x040004D2 RID: 1234
		public bool forceScreenCenterCoordinates;

		// Token: 0x040004D3 RID: 1235
		public Camera camera;

		// Token: 0x040004D4 RID: 1236
		public bool shortcutPressed;

		// Token: 0x040004D5 RID: 1237
		public GraphicsBuffer positionNormalBuffer;
	}
}
