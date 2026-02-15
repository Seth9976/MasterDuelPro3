using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200007B RID: 123
	public abstract class ShadowShape2D
	{
		// Token: 0x06000309 RID: 777
		public abstract void SetFlip(bool flipX, bool flipY);

		// Token: 0x0600030A RID: 778
		public abstract void GetFlip(out bool flipX, out bool flipY);

		// Token: 0x0600030B RID: 779
		public abstract void SetDefaultTrim(float trim);

		// Token: 0x0600030C RID: 780
		public abstract void SetShape(NativeArray<Vector3> vertices, NativeArray<int> indices, NativeArray<float> radii, Matrix4x4 transform, ShadowShape2D.WindingOrder windingOrder = ShadowShape2D.WindingOrder.Clockwise, bool allowContraction = true, bool createInteriorGeometry = false);

		// Token: 0x0600030D RID: 781
		public abstract void SetShape(NativeArray<Vector3> vertices, NativeArray<int> indices, ShadowShape2D.OutlineTopology outlineTopology, ShadowShape2D.WindingOrder windingOrder = ShadowShape2D.WindingOrder.Clockwise, bool allowContraction = true, bool createInteriorGeometry = false);

		// Token: 0x0200007C RID: 124
		public enum OutlineTopology
		{
			// Token: 0x040002B1 RID: 689
			Lines,
			// Token: 0x040002B2 RID: 690
			Triangles
		}

		// Token: 0x0200007D RID: 125
		public enum WindingOrder
		{
			// Token: 0x040002B4 RID: 692
			Clockwise,
			// Token: 0x040002B5 RID: 693
			CounterClockwise
		}
	}
}
