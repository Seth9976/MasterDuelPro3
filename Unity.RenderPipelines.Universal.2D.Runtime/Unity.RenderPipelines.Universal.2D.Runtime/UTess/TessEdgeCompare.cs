using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x020000B2 RID: 178
	internal struct TessEdgeCompare : IComparer<int2>
	{
		// Token: 0x060003E1 RID: 993 RVA: 0x0001CCA4 File Offset: 0x0001AEA4
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
