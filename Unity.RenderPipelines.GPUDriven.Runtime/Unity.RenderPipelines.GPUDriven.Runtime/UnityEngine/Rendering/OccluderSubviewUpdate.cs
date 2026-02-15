using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200002E RID: 46
	public struct OccluderSubviewUpdate
	{
		// Token: 0x060000EF RID: 239 RVA: 0x00005944 File Offset: 0x00003B44
		public OccluderSubviewUpdate(int subviewIndex)
		{
			this.subviewIndex = subviewIndex;
			this.depthSliceIndex = 0;
			this.depthOffset = Vector2Int.zero;
			this.viewMatrix = Matrix4x4.identity;
			this.invViewMatrix = Matrix4x4.identity;
			this.gpuProjMatrix = Matrix4x4.identity;
			this.viewOffsetWorldSpace = Vector3.zero;
		}

		// Token: 0x04000093 RID: 147
		public int subviewIndex;

		// Token: 0x04000094 RID: 148
		public int depthSliceIndex;

		// Token: 0x04000095 RID: 149
		public Vector2Int depthOffset;

		// Token: 0x04000096 RID: 150
		public Matrix4x4 viewMatrix;

		// Token: 0x04000097 RID: 151
		public Matrix4x4 invViewMatrix;

		// Token: 0x04000098 RID: 152
		public Matrix4x4 gpuProjMatrix;

		// Token: 0x04000099 RID: 153
		public Vector3 viewOffsetWorldSpace;
	}
}
