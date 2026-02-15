using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x020000A6 RID: 166
	internal struct UHull
	{
		// Token: 0x0400030E RID: 782
		public float2 a;

		// Token: 0x0400030F RID: 783
		public float2 b;

		// Token: 0x04000310 RID: 784
		public int idx;

		// Token: 0x04000311 RID: 785
		public ArraySlice<int> ilarray;

		// Token: 0x04000312 RID: 786
		public int ilcount;

		// Token: 0x04000313 RID: 787
		public ArraySlice<int> iuarray;

		// Token: 0x04000314 RID: 788
		public int iucount;
	}
}
