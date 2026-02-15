using System;
using System.Collections.Generic;
using UnityEngine;

namespace DG.Tweening
{
	// Token: 0x02000008 RID: 8
	public static class DOCurve
	{
		// Token: 0x02000009 RID: 9
		public static class CubicBezier
		{
			// Token: 0x06000011 RID: 17 RVA: 0x000020D4 File Offset: 0x000002D4
			public static Vector3 GetPointOnSegment(Vector3 startPoint, Vector3 startControlPoint, Vector3 endPoint, Vector3 endControlPoint, float factor)
			{
				float num = 1f - factor;
				float num2 = factor * factor;
				float num3 = num * num;
				float num4 = num3 * num;
				float num5 = num2 * factor;
				return num4 * startPoint + 3f * num3 * factor * startControlPoint + 3f * num * num2 * endControlPoint + num5 * endPoint;
			}

			// Token: 0x06000012 RID: 18 RVA: 0x00002138 File Offset: 0x00000338
			public static Vector3[] GetSegmentPointCloud(Vector3 startPoint, Vector3 startControlPoint, Vector3 endPoint, Vector3 endControlPoint, int resolution = 10)
			{
				if (resolution < 2)
				{
					resolution = 2;
				}
				Vector3[] array = new Vector3[resolution];
				float num = 1f / (float)(resolution - 1);
				for (int i = 0; i < resolution; i++)
				{
					array[i] = DOCurve.CubicBezier.GetPointOnSegment(startPoint, startControlPoint, endPoint, endControlPoint, num * (float)i);
				}
				return array;
			}

			// Token: 0x06000013 RID: 19 RVA: 0x00002184 File Offset: 0x00000384
			public static void GetSegmentPointCloud(List<Vector3> addToList, Vector3 startPoint, Vector3 startControlPoint, Vector3 endPoint, Vector3 endControlPoint, int resolution = 10)
			{
				if (resolution < 2)
				{
					resolution = 2;
				}
				float num = 1f / (float)(resolution - 1);
				for (int i = 0; i < resolution; i++)
				{
					addToList.Add(DOCurve.CubicBezier.GetPointOnSegment(startPoint, startControlPoint, endPoint, endControlPoint, num * (float)i));
				}
			}
		}
	}
}
