using System;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000016 RID: 22
	internal readonly struct XRView
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00004764 File Offset: 0x00002964
		internal XRView(Matrix4x4 projMatrix, Matrix4x4 viewMatrix, Matrix4x4 prevViewMatrix, bool isPrevViewMatrixValid, Rect viewport, Mesh occlusionMesh, int textureArraySlice)
		{
			this.projMatrix = projMatrix;
			this.viewMatrix = viewMatrix;
			this.prevViewMatrix = prevViewMatrix;
			this.viewport = viewport;
			this.occlusionMesh = occlusionMesh;
			this.textureArraySlice = textureArraySlice;
			this.isPrevViewMatrixValid = isPrevViewMatrixValid;
			this.eyeCenterUV = XRView.ComputeEyeCenterUV(projMatrix);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000047B4 File Offset: 0x000029B4
		private static Vector2 ComputeEyeCenterUV(Matrix4x4 proj)
		{
			FrustumPlanes decomposeProjection = proj.decomposeProjection;
			float left = Math.Abs(decomposeProjection.left);
			float right = Math.Abs(decomposeProjection.right);
			float top = Math.Abs(decomposeProjection.top);
			float bottom = Math.Abs(decomposeProjection.bottom);
			return new Vector2(left / (right + left), top / (top + bottom));
		}

		// Token: 0x04000072 RID: 114
		internal readonly Matrix4x4 projMatrix;

		// Token: 0x04000073 RID: 115
		internal readonly Matrix4x4 viewMatrix;

		// Token: 0x04000074 RID: 116
		internal readonly Matrix4x4 prevViewMatrix;

		// Token: 0x04000075 RID: 117
		internal readonly Rect viewport;

		// Token: 0x04000076 RID: 118
		internal readonly Mesh occlusionMesh;

		// Token: 0x04000077 RID: 119
		internal readonly int textureArraySlice;

		// Token: 0x04000078 RID: 120
		internal readonly Vector2 eyeCenterUV;

		// Token: 0x04000079 RID: 121
		internal readonly bool isPrevViewMatrixValid;
	}
}
