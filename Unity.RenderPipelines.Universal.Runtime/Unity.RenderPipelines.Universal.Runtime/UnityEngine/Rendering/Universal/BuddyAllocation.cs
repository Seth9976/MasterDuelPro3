using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000DB RID: 219
	internal struct BuddyAllocation
	{
		// Token: 0x06000598 RID: 1432 RVA: 0x00014F03 File Offset: 0x00013103
		public BuddyAllocation(int level, int index)
		{
			this.level = level;
			this.index = index;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x00014F13 File Offset: 0x00013113
		public uint2 index2D
		{
			get
			{
				return SpaceFillingCurves.DecodeMorton2D((uint)this.index);
			}
		}

		// Token: 0x040004D6 RID: 1238
		public int level;

		// Token: 0x040004D7 RID: 1239
		public int index;
	}
}
