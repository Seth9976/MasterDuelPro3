using System;
using Unity.Collections;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000191 RID: 401
	internal struct URPLightShadowCullingInfos
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x00028452 File Offset: 0x00026652
		public readonly bool IsSliceValid(int i)
		{
			return ((ulong)this.slicesValidMask & (ulong)(1L << (i & 31))) > 0UL;
		}

		// Token: 0x040008E9 RID: 2281
		public NativeArray<ShadowSliceData> slices;

		// Token: 0x040008EA RID: 2282
		public uint slicesValidMask;
	}
}
