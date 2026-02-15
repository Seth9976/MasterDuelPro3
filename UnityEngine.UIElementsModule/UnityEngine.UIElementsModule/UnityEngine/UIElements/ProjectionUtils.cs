using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000296 RID: 662
	internal static class ProjectionUtils
	{
		// Token: 0x060011E5 RID: 4581 RVA: 0x0004AD38 File Offset: 0x00048F38
		public static Matrix4x4 Ortho(float left, float right, float bottom, float top, float near, float far)
		{
			Matrix4x4 result = default(Matrix4x4);
			float rightMinusLeft = right - left;
			float topMinusBottom = top - bottom;
			float farMinusNear = far - near;
			result.m00 = 2f / rightMinusLeft;
			result.m11 = 2f / topMinusBottom;
			result.m22 = 2f / farMinusNear;
			result.m03 = -(right + left) / rightMinusLeft;
			result.m13 = -(top + bottom) / topMinusBottom;
			result.m23 = -(far + near) / farMinusNear;
			result.m33 = 1f;
			return result;
		}
	}
}
