using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore
{
	// Token: 0x02000098 RID: 152
	internal class CubicBezierDecoder : ABSPathDecoder
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000F214 File Offset: 0x0000D414
		internal override int minInputWaypoints
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000F218 File Offset: 0x0000D418
		internal override void FinalizePath(Path p, Vector3[] wps, bool isClosedPath)
		{
			if (isClosedPath && !p.addedExtraEndWp)
			{
				isClosedPath = false;
			}
			int num = wps.Length;
			int num2 = (p.addedExtraStartWp ? 1 : 0);
			if (p.addedExtraEndWp)
			{
				num2++;
			}
			if (num < 3 + num2 || (num - num2) % 3 != 0)
			{
				Debug.LogError("CubicBezier paths must contain waypoints in multiple of 3 excluding the starting point added automatically by DOTween (1: waypoint, 2: IN control point, 3: OUT control point — the minimum amount of waypoints for a single curve is 3)");
				return;
			}
			int num3 = num2 + (num - num2) / 3;
			Vector3[] array = new Vector3[num3];
			p.controlPoints = new ControlPoint[num3 - 1];
			array[0] = wps[0];
			int num4 = 1;
			int num5 = 0;
			for (int i = 3 + (p.addedExtraStartWp ? 0 : 2); i < num; i += 3)
			{
				array[num4] = wps[i - 2];
				num4++;
				p.controlPoints[num5] = new ControlPoint(wps[i - 1], wps[i]);
				num5++;
			}
			p.wps = array;
			if (isClosedPath)
			{
				Vector3 vector = p.wps[p.wps.Length - 2];
				Vector3 vector2 = p.wps[0];
				Vector3 b = p.controlPoints[p.controlPoints.Length - 2].b;
				Vector3 a = p.controlPoints[0].a;
				float magnitude = (vector2 - vector).magnitude;
				p.controlPoints[p.controlPoints.Length - 1] = new ControlPoint(vector + Vector3.ClampMagnitude(vector - b, magnitude), vector2 + Vector3.ClampMagnitude(vector2 - a, magnitude));
			}
			p.subdivisions = num3 * p.subdivisionsXSegment;
			this.SetTimeToLengthTables(p, p.subdivisions);
			this.SetWaypointsLengths(p, p.subdivisionsXSegment);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		internal override Vector3 GetPoint(float perc, Vector3[] wps, Path p, ControlPoint[] controlPoints)
		{
			int num = wps.Length - 1;
			int num2 = (int)Math.Floor((double)(perc * (float)num));
			int num3 = num - 1;
			if (num3 > num2)
			{
				num3 = num2;
			}
			float num4 = perc * (float)num - (float)num3;
			Vector3 vector = wps[num3];
			Vector3 a = controlPoints[num3].a;
			Vector3 b = controlPoints[num3].b;
			Vector3 vector2 = wps[num3 + 1];
			float num5 = 1f - num4;
			float num6 = num4 * num4;
			float num7 = num5 * num5;
			float num8 = num7 * num5;
			float num9 = num6 * num4;
			return num8 * vector + 3f * num7 * num4 * a + 3f * num5 * num6 * b + num9 * vector2;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
		internal void SetTimeToLengthTables(Path p, int subdivisions)
		{
			float num = 0f;
			float num2 = 1f / (float)subdivisions;
			float[] array = new float[subdivisions];
			float[] array2 = new float[subdivisions];
			Vector3 vector = this.GetPoint(0f, p.wps, p, p.controlPoints);
			for (int i = 1; i < subdivisions + 1; i++)
			{
				float num3 = num2 * (float)i;
				Vector3 point = this.GetPoint(num3, p.wps, p, p.controlPoints);
				num += Vector3.Distance(point, vector);
				vector = point;
				array[i - 1] = num3;
				array2[i - 1] = num;
			}
			p.length = num;
			p.timesTable = array;
			p.lengthsTable = array2;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0000F550 File Offset: 0x0000D750
		internal void SetWaypointsLengths(Path p, int subdivisions)
		{
			int num = p.wps.Length;
			float[] array = new float[num];
			array[0] = 0f;
			for (int i = 1; i < num; i++)
			{
				CubicBezierDecoder._PartialControlPs[0] = p.controlPoints[i - 1];
				CubicBezierDecoder._PartialWps[0] = p.wps[i - 1];
				CubicBezierDecoder._PartialWps[1] = p.wps[i];
				float num2 = 0f;
				float num3 = 1f / (float)subdivisions;
				Vector3 vector = this.GetPoint(0f, CubicBezierDecoder._PartialWps, p, CubicBezierDecoder._PartialControlPs);
				for (int j = 1; j < subdivisions + 1; j++)
				{
					float num4 = num3 * (float)j;
					Vector3 point = this.GetPoint(num4, CubicBezierDecoder._PartialWps, p, CubicBezierDecoder._PartialControlPs);
					num2 += Vector3.Distance(point, vector);
					vector = point;
				}
				array[i] = num2;
			}
			p.wpLengths = array;
		}

		// Token: 0x040001A4 RID: 420
		private static readonly ControlPoint[] _PartialControlPs = new ControlPoint[1];

		// Token: 0x040001A5 RID: 421
		private static readonly Vector3[] _PartialWps = new Vector3[2];
	}
}
