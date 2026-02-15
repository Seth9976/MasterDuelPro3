using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore
{
	// Token: 0x0200009B RID: 155
	internal class CatmullRomDecoder : ABSPathDecoder
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		internal override int minInputWaypoints
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		internal override void FinalizePath(Path p, Vector3[] wps, bool isClosedPath)
		{
			int num = wps.Length;
			if (p.controlPoints == null || p.controlPoints.Length != 2)
			{
				p.controlPoints = new ControlPoint[2];
			}
			if (isClosedPath)
			{
				p.controlPoints[0] = new ControlPoint(wps[num - 2], Vector3.zero);
				p.controlPoints[1] = new ControlPoint(wps[1], Vector3.zero);
			}
			else
			{
				p.controlPoints[0] = new ControlPoint(wps[1], Vector3.zero);
				Vector3 vector = wps[num - 1];
				Vector3 vector2 = vector - wps[num - 2];
				p.controlPoints[1] = new ControlPoint(vector + vector2, Vector3.zero);
			}
			p.subdivisions = num * p.subdivisionsXSegment;
			this.SetTimeToLengthTables(p, p.subdivisions);
			this.SetWaypointsLengths(p, p.subdivisionsXSegment);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
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
			Vector3 vector = ((num3 == 0) ? controlPoints[0].a : wps[num3 - 1]);
			Vector3 vector2 = wps[num3];
			Vector3 vector3 = wps[num3 + 1];
			Vector3 vector4 = ((num3 + 2 > wps.Length - 1) ? controlPoints[1].a : wps[num3 + 2]);
			return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num4 * num4 * num4) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num4 * num4) + (-vector + vector3) * num4 + 2f * vector2);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000F918 File Offset: 0x0000DB18
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

		// Token: 0x06000393 RID: 915 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		internal void SetWaypointsLengths(Path p, int subdivisions)
		{
			int num = p.wps.Length;
			float[] array = new float[num];
			array[0] = 0f;
			for (int i = 1; i < num; i++)
			{
				CatmullRomDecoder._PartialControlPs[0].a = ((i == 1) ? p.controlPoints[0].a : p.wps[i - 2]);
				CatmullRomDecoder._PartialWps[0] = p.wps[i - 1];
				CatmullRomDecoder._PartialWps[1] = p.wps[i];
				CatmullRomDecoder._PartialControlPs[1].a = ((i == num - 1) ? p.controlPoints[1].a : p.wps[i + 1]);
				float num2 = 0f;
				float num3 = 1f / (float)subdivisions;
				Vector3 vector = this.GetPoint(0f, CatmullRomDecoder._PartialWps, p, CatmullRomDecoder._PartialControlPs);
				for (int j = 1; j < subdivisions + 1; j++)
				{
					float num4 = num3 * (float)j;
					Vector3 point = this.GetPoint(num4, CatmullRomDecoder._PartialWps, p, CatmullRomDecoder._PartialControlPs);
					num2 += Vector3.Distance(point, vector);
					vector = point;
				}
				array[i] = num2;
			}
			p.wpLengths = array;
		}

		// Token: 0x040001A8 RID: 424
		private static readonly ControlPoint[] _PartialControlPs = new ControlPoint[2];

		// Token: 0x040001A9 RID: 425
		private static readonly Vector3[] _PartialWps = new Vector3[2];
	}
}
