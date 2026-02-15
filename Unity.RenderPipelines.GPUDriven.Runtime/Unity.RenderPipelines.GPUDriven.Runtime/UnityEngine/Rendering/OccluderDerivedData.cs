using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008F RID: 143
	internal struct OccluderDerivedData
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0001007C File Offset: 0x0000E27C
		public static OccluderDerivedData FromParameters(in OccluderSubviewUpdate occluderSubviewUpdate)
		{
			Vector3 viewOffsetWorldSpace = occluderSubviewUpdate.viewOffsetWorldSpace;
			Matrix4x4 matrix4x = occluderSubviewUpdate.invViewMatrix;
			Vector3 origin = viewOffsetWorldSpace + matrix4x.GetColumn(3);
			matrix4x = occluderSubviewUpdate.invViewMatrix;
			Vector3 xViewVec = matrix4x.GetColumn(0);
			matrix4x = occluderSubviewUpdate.invViewMatrix;
			Vector3 yViewVec = matrix4x.GetColumn(1);
			matrix4x = occluderSubviewUpdate.invViewMatrix;
			Vector3 towardsVec = matrix4x.GetColumn(2);
			Matrix4x4 viewMatrixNoTranslation = occluderSubviewUpdate.viewMatrix;
			viewMatrixNoTranslation.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
			return new OccluderDerivedData
			{
				viewOriginWorldSpace = origin,
				facingDirWorldSpace = towardsVec.normalized,
				radialDirWorldSpace = (xViewVec + yViewVec).normalized,
				viewProjMatrix = occluderSubviewUpdate.gpuProjMatrix * viewMatrixNoTranslation
			};
		}

		// Token: 0x040002F2 RID: 754
		public Matrix4x4 viewProjMatrix;

		// Token: 0x040002F3 RID: 755
		public Vector4 viewOriginWorldSpace;

		// Token: 0x040002F4 RID: 756
		public Vector4 radialDirWorldSpace;

		// Token: 0x040002F5 RID: 757
		public Vector4 facingDirWorldSpace;
	}
}
