using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x020000B3 RID: 179
	internal struct TessCellCompare : IComparer<int3>
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x0001CCD4 File Offset: 0x0001AED4
		public int Compare(int3 a, int3 b)
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
			return a.z - b.z;
		}
	}
}
