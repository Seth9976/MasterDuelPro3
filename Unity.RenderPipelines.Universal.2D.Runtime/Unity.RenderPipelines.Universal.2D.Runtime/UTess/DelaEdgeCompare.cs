using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x020000B5 RID: 181
	internal struct DelaEdgeCompare : IComparer<int4>
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x0001CD48 File Offset: 0x0001AF48
		public int Compare(int4 a, int4 b)
		{
			int i = a.x - b.x;
			if (i != 0)
			{
				return i;
			}
			i = a.y - b.y;
			if (i != 0)
			{
				return i;
			}
			i = a.z - b.z;
			if (i != 0)
			{
				return i;
			}
			return a.w - b.w;
		}
	}
}
