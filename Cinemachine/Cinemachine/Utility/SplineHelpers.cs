using System;
using UnityEngine;

namespace Cinemachine.Utility
{
	// Token: 0x020000EF RID: 239
	public static class SplineHelpers
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x000226DC File Offset: 0x000208DC
		public static Vector3 Bezier3(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
		{
			t = Mathf.Clamp01(t);
			float d = 1f - t;
			return d * d * d * p0 + 3f * d * d * t * p1 + 3f * d * t * t * p2 + t * t * t * p3;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00022744 File Offset: 0x00020944
		public static Vector3 BezierTangent3(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
		{
			t = Mathf.Clamp01(t);
			return (-3f * p0 + 9f * p1 - 9f * p2 + 3f * p3) * (t * t) + (6f * p0 - 12f * p1 + 6f * p2) * t - 3f * p0 + 3f * p1;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000227F4 File Offset: 0x000209F4
		public static void BezierTangentWeights3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, out Vector3 w0, out Vector3 w1, out Vector3 w2)
		{
			w0 = -3f * p0 + 9f * p1 - 9f * p2 + 3f * p3;
			w1 = 6f * p0 - 12f * p1 + 6f * p2;
			w2 = -3f * p0 + 3f * p1;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00022898 File Offset: 0x00020A98
		public static float Bezier1(float t, float p0, float p1, float p2, float p3)
		{
			t = Mathf.Clamp01(t);
			float d = 1f - t;
			return d * d * d * p0 + 3f * d * d * t * p1 + 3f * d * t * t * p2 + t * t * t * p3;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000228E4 File Offset: 0x00020AE4
		public static float BezierTangent1(float t, float p0, float p1, float p2, float p3)
		{
			t = Mathf.Clamp01(t);
			return (-3f * p0 + 9f * p1 - 9f * p2 + 3f * p3) * t * t + (6f * p0 - 12f * p1 + 6f * p2) * t - 3f * p0 + 3f * p1;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00022948 File Offset: 0x00020B48
		public static void ComputeSmoothControlPoints(ref Vector4[] knot, ref Vector4[] ctrl1, ref Vector4[] ctrl2)
		{
			int numPoints = knot.Length;
			if (numPoints > 2)
			{
				float[] a = new float[numPoints];
				float[] b = new float[numPoints];
				float[] c = new float[numPoints];
				float[] r = new float[numPoints];
				for (int axis = 0; axis < 4; axis++)
				{
					int i = numPoints - 1;
					a[0] = 0f;
					b[0] = 2f;
					c[0] = 1f;
					r[0] = knot[0][axis] + 2f * knot[1][axis];
					for (int j = 1; j < i - 1; j++)
					{
						a[j] = 1f;
						b[j] = 4f;
						c[j] = 1f;
						r[j] = 4f * knot[j][axis] + 2f * knot[j + 1][axis];
					}
					a[i - 1] = 2f;
					b[i - 1] = 7f;
					c[i - 1] = 0f;
					r[i - 1] = 8f * knot[i - 1][axis] + knot[i][axis];
					for (int k = 1; k < i; k++)
					{
						float l = a[k] / b[k - 1];
						b[k] -= l * c[k - 1];
						r[k] -= l * r[k - 1];
					}
					ctrl1[i - 1][axis] = r[i - 1] / b[i - 1];
					for (int m = i - 2; m >= 0; m--)
					{
						ctrl1[m][axis] = (r[m] - c[m] * ctrl1[m + 1][axis]) / b[m];
					}
					for (int n = 0; n < i; n++)
					{
						ctrl2[n][axis] = 2f * knot[n + 1][axis] - ctrl1[n + 1][axis];
					}
					ctrl2[i - 1][axis] = 0.5f * (knot[i][axis] + ctrl1[i - 1][axis]);
				}
				return;
			}
			if (numPoints == 2)
			{
				ctrl1[0] = Vector4.Lerp(knot[0], knot[1], 0.33333f);
				ctrl2[0] = Vector4.Lerp(knot[0], knot[1], 0.66666f);
				return;
			}
			if (numPoints == 1)
			{
				ctrl1[0] = (ctrl2[0] = knot[0]);
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00022C28 File Offset: 0x00020E28
		public static void ComputeSmoothControlPointsLooped(ref Vector4[] knot, ref Vector4[] ctrl1, ref Vector4[] ctrl2)
		{
			int numPoints = knot.Length;
			if (numPoints < 2)
			{
				if (numPoints == 1)
				{
					ctrl1[0] = (ctrl2[0] = knot[0]);
				}
				return;
			}
			int margin = Mathf.Min(4, numPoints - 1);
			Vector4[] knotLooped = new Vector4[numPoints + 2 * margin];
			Vector4[] ctrl1Looped = new Vector4[numPoints + 2 * margin];
			Vector4[] ctrl2Looped = new Vector4[numPoints + 2 * margin];
			for (int i = 0; i < margin; i++)
			{
				knotLooped[i] = knot[numPoints - (margin - i)];
				knotLooped[numPoints + margin + i] = knot[i];
			}
			for (int j = 0; j < numPoints; j++)
			{
				knotLooped[j + margin] = knot[j];
			}
			SplineHelpers.ComputeSmoothControlPoints(ref knotLooped, ref ctrl1Looped, ref ctrl2Looped);
			for (int k = 0; k < numPoints; k++)
			{
				ctrl1[k] = ctrl1Looped[k + margin];
				ctrl2[k] = ctrl2Looped[k + margin];
			}
		}
	}
}
