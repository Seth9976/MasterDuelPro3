using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x020000B4 RID: 180
	internal struct TessJunctionCompare : IComparer<int2>
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0001CD18 File Offset: 0x0001AF18
		public int Compare(int2 a, int2 b)
		{
			int i = a.x - b.x;
			if (i != 0)
			{
				return i;
			}
			return a.y - b.y;
		}
	}
}
