using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x020000AD RID: 173
	internal struct XCompare : IComparer<double>
	{
		// Token: 0x060003DE RID: 990 RVA: 0x0001C97A File Offset: 0x0001AB7A
		public int Compare(double a, double b)
		{
			if (a >= b)
			{
				return 1;
			}
			return -1;
		}
	}
}
