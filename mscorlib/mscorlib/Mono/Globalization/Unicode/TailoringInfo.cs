using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000055 RID: 85
	internal class TailoringInfo
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00003C30 File Offset: 0x00001E30
		public TailoringInfo(int lcid, int tailoringIndex, int tailoringCount, bool frenchSort)
		{
			this.LCID = lcid;
			this.TailoringIndex = tailoringIndex;
			this.TailoringCount = tailoringCount;
			this.FrenchSort = frenchSort;
		}

		// Token: 0x0400015D RID: 349
		public readonly int LCID;

		// Token: 0x0400015E RID: 350
		public readonly int TailoringIndex;

		// Token: 0x0400015F RID: 351
		public readonly int TailoringCount;

		// Token: 0x04000160 RID: 352
		public readonly bool FrenchSort;
	}
}
