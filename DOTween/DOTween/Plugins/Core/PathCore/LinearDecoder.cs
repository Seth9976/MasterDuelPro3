using System;
using UnityEngine;

namespace DG.Tweening.Plugins.Core.PathCore
{
	// Token: 0x0200009C RID: 156
	internal class LinearDecoder : ABSPathDecoder
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000396 RID: 918 RVA: 0x0000F6EC File Offset: 0x0000D8EC
		internal override int minInputWaypoints
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000FB1D File Offset: 0x0000DD1D
		internal override void FinalizePath(Path p, Vector3[] wps, bool isClosedPath)
		{
			p.controlPoints = null;
			p.subdivisions = wps.Length * p.subdivisionsXSegment;
			this.SetTimeToLengthTables(p, p.subdivisions);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000FB44 File Offset: 0x0000DD44
		internal override Vector3 GetPoint(float perc, Vector3[] wps, Path p, ControlPoint[] controlPoints)
		{
			if (perc <= 0f)
			{
				p.linearWPIndex = 1;
				return wps[0];
			}
			int num = 0;
			int num2 = 0;
			int num3 = p.timesTable.Length;
			for (int i = 1; i < num3; i++)
			{
				if (p.timesTable[i] >= perc)
				{
					num = i - 1;
					num2 = i;
					break;
				}
			}
			float num4 = p.timesTable[num];
			float num5 = perc - num4;
			float num6 = p.length * num5;
			Vector3 vector = wps[num];
			Vector3 vector2 = wps[num2];
			p.linearWPIndex = num2;
			return vector + Vector3.ClampMagnitude(vector2 - vector, num6);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0000FBE4 File Offset: 0x0000DDE4
		internal void SetTimeToLengthTables(Path p, int subdivisions)
		{
			float num = 0f;
			int num2 = p.wps.Length;
			float[] array = new float[num2];
			Vector3 vector = p.wps[0];
			for (int i = 0; i < num2; i++)
			{
				Vector3 vector2 = p.wps[i];
				float num3 = Vector3.Distance(vector2, vector);
				num += num3;
				vector = vector2;
				array[i] = num3;
			}
			float[] array2 = new float[num2];
			float num4 = 0f;
			for (int j = 1; j < num2; j++)
			{
				num4 += array[j];
				array2[j] = num4 / num;
			}
			p.length = num;
			p.wpLengths = array;
			p.timesTable = array2;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00009DF5 File Offset: 0x00007FF5
		internal void SetWaypointsLengths(Path p, int subdivisions)
		{
		}
	}
}
