using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001B6 RID: 438
	public static class CoreMatrixUtils
	{
		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002DF68 File Offset: 0x0002C168
		public static void MatrixTimesTranslation(ref Matrix4x4 inOutMatrix, Vector3 translation)
		{
			inOutMatrix.m03 += inOutMatrix.m00 * translation.x + inOutMatrix.m01 * translation.y + inOutMatrix.m02 * translation.z;
			inOutMatrix.m13 += inOutMatrix.m10 * translation.x + inOutMatrix.m11 * translation.y + inOutMatrix.m12 * translation.z;
			inOutMatrix.m23 += inOutMatrix.m20 * translation.x + inOutMatrix.m21 * translation.y + inOutMatrix.m22 * translation.z;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002E010 File Offset: 0x0002C210
		public static void TranslationTimesMatrix(ref Matrix4x4 inOutMatrix, Vector3 translation)
		{
			inOutMatrix.m00 += translation.x * inOutMatrix.m30;
			inOutMatrix.m01 += translation.x * inOutMatrix.m31;
			inOutMatrix.m02 += translation.x * inOutMatrix.m32;
			inOutMatrix.m03 += translation.x * inOutMatrix.m33;
			inOutMatrix.m10 += translation.y * inOutMatrix.m30;
			inOutMatrix.m11 += translation.y * inOutMatrix.m31;
			inOutMatrix.m12 += translation.y * inOutMatrix.m32;
			inOutMatrix.m13 += translation.y * inOutMatrix.m33;
			inOutMatrix.m20 += translation.z * inOutMatrix.m30;
			inOutMatrix.m21 += translation.z * inOutMatrix.m31;
			inOutMatrix.m22 += translation.z * inOutMatrix.m32;
			inOutMatrix.m23 += translation.z * inOutMatrix.m33;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002E134 File Offset: 0x0002C334
		public static Matrix4x4 MultiplyPerspectiveMatrix(Matrix4x4 perspective, Matrix4x4 rhs)
		{
			Matrix4x4 outMat;
			outMat.m00 = perspective.m00 * rhs.m00;
			outMat.m01 = perspective.m00 * rhs.m01;
			outMat.m02 = perspective.m00 * rhs.m02;
			outMat.m03 = perspective.m00 * rhs.m03;
			outMat.m10 = perspective.m11 * rhs.m10;
			outMat.m11 = perspective.m11 * rhs.m11;
			outMat.m12 = perspective.m11 * rhs.m12;
			outMat.m13 = perspective.m11 * rhs.m13;
			outMat.m20 = perspective.m22 * rhs.m20 + perspective.m23 * rhs.m30;
			outMat.m21 = perspective.m22 * rhs.m21 + perspective.m23 * rhs.m31;
			outMat.m22 = perspective.m22 * rhs.m22 + perspective.m23 * rhs.m32;
			outMat.m23 = perspective.m22 * rhs.m23 + perspective.m23 * rhs.m33;
			outMat.m30 = -rhs.m20;
			outMat.m31 = -rhs.m21;
			outMat.m32 = -rhs.m22;
			outMat.m33 = -rhs.m23;
			return outMat;
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x0002E2A4 File Offset: 0x0002C4A4
		private static Matrix4x4 MultiplyOrthoMatrixCentered(Matrix4x4 ortho, Matrix4x4 rhs)
		{
			Matrix4x4 outMat;
			outMat.m00 = ortho.m00 * rhs.m00;
			outMat.m01 = ortho.m00 * rhs.m01;
			outMat.m02 = ortho.m00 * rhs.m02;
			outMat.m03 = ortho.m00 * rhs.m03;
			outMat.m10 = ortho.m11 * rhs.m10;
			outMat.m11 = ortho.m11 * rhs.m11;
			outMat.m12 = ortho.m11 * rhs.m12;
			outMat.m13 = ortho.m11 * rhs.m13;
			outMat.m20 = ortho.m22 * rhs.m20 + ortho.m23 * rhs.m30;
			outMat.m21 = ortho.m22 * rhs.m21 + ortho.m23 * rhs.m31;
			outMat.m22 = ortho.m22 * rhs.m22 + ortho.m23 * rhs.m32;
			outMat.m23 = ortho.m22 * rhs.m23 + ortho.m23 * rhs.m33;
			outMat.m30 = rhs.m20;
			outMat.m31 = rhs.m21;
			outMat.m32 = rhs.m22;
			outMat.m33 = rhs.m23;
			return outMat;
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x0002E410 File Offset: 0x0002C610
		private static Matrix4x4 MultiplyGenericOrthoMatrix(Matrix4x4 ortho, Matrix4x4 rhs)
		{
			Matrix4x4 outMat;
			outMat.m00 = ortho.m00 * rhs.m00 + ortho.m03 * rhs.m30;
			outMat.m01 = ortho.m00 * rhs.m01 + ortho.m03 * rhs.m31;
			outMat.m02 = ortho.m00 * rhs.m02 + ortho.m03 * rhs.m32;
			outMat.m03 = ortho.m00 * rhs.m03 + ortho.m03 * rhs.m33;
			outMat.m10 = ortho.m11 * rhs.m10 + ortho.m13 * rhs.m30;
			outMat.m11 = ortho.m11 * rhs.m11 + ortho.m13 * rhs.m31;
			outMat.m12 = ortho.m11 * rhs.m12 + ortho.m13 * rhs.m32;
			outMat.m13 = ortho.m11 * rhs.m13 + ortho.m13 * rhs.m33;
			outMat.m20 = ortho.m22 * rhs.m20 + ortho.m23 * rhs.m30;
			outMat.m21 = ortho.m22 * rhs.m21 + ortho.m23 * rhs.m31;
			outMat.m22 = ortho.m22 * rhs.m22 + ortho.m23 * rhs.m32;
			outMat.m23 = ortho.m22 * rhs.m23 + ortho.m23 * rhs.m33;
			outMat.m30 = rhs.m20;
			outMat.m31 = rhs.m21;
			outMat.m32 = rhs.m22;
			outMat.m33 = rhs.m23;
			return outMat;
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002E5EA File Offset: 0x0002C7EA
		public static Matrix4x4 MultiplyOrthoMatrix(Matrix4x4 ortho, Matrix4x4 rhs, bool centered)
		{
			if (!centered)
			{
				return CoreMatrixUtils.MultiplyOrthoMatrixCentered(ortho, rhs);
			}
			return CoreMatrixUtils.MultiplyGenericOrthoMatrix(ortho, rhs);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x0002E5FE File Offset: 0x0002C7FE
		public static Matrix4x4 MultiplyProjectionMatrix(Matrix4x4 projMatrix, Matrix4x4 rhs, bool orthoCentered)
		{
			if (!orthoCentered)
			{
				return CoreMatrixUtils.MultiplyPerspectiveMatrix(projMatrix, rhs);
			}
			return CoreMatrixUtils.MultiplyOrthoMatrixCentered(projMatrix, rhs);
		}
	}
}
