using System;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200005B RID: 91
	internal class MSCompatUnicodeTableUtil
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00004854 File Offset: 0x00002A54
		static MSCompatUnicodeTableUtil()
		{
			int[] array = new int[] { 0, 40960, 63744 };
			int[] array2 = new int[] { 13312, 42240, 65536 };
			int[] array3 = new int[] { 0, 7680, 12288, 19968, 44032, 63744 };
			int[] array4 = new int[] { 4608, 10240, 13312, 40960, 55216, 65536 };
			int[] array5 = new int[] { 0, 7680, 12288, 19968, 44032, 63744 };
			int[] array6 = new int[] { 4608, 10240, 13312, 40960, 55216, 65536 };
			int[] array7 = new int[] { 0, 7680, 12288, 64256 };
			int[] array8 = new int[] { 3840, 10240, 13312, 65536 };
			int[] array9 = new int[] { 0, 7680, 12288, 64256 };
			int[] array10 = new int[] { 4608, 10240, 13312, 65536 };
			int[] array11 = new int[] { 12544, 19968, 59392 };
			int[] array12 = new int[] { 13312, 40960, 65536 };
			int[] array13 = new int[] { 12544, 19968, 63744 };
			int[] array14 = new int[] { 13312, 40960, 64256 };
			MSCompatUnicodeTableUtil.Ignorable = new CodePointIndexer(array, array2, -1, -1);
			MSCompatUnicodeTableUtil.Category = new CodePointIndexer(array3, array4, 0, 0);
			MSCompatUnicodeTableUtil.Level1 = new CodePointIndexer(array5, array6, 0, 0);
			MSCompatUnicodeTableUtil.Level2 = new CodePointIndexer(array7, array8, 0, 0);
			MSCompatUnicodeTableUtil.Level3 = new CodePointIndexer(array9, array10, 0, 0);
			MSCompatUnicodeTableUtil.CjkCHS = new CodePointIndexer(array11, array12, -1, -1);
			MSCompatUnicodeTableUtil.Cjk = new CodePointIndexer(array13, array14, -1, -1);
		}

		// Token: 0x0400017D RID: 381
		public static readonly CodePointIndexer Ignorable;

		// Token: 0x0400017E RID: 382
		public static readonly CodePointIndexer Category;

		// Token: 0x0400017F RID: 383
		public static readonly CodePointIndexer Level1;

		// Token: 0x04000180 RID: 384
		public static readonly CodePointIndexer Level2;

		// Token: 0x04000181 RID: 385
		public static readonly CodePointIndexer Level3;

		// Token: 0x04000182 RID: 386
		public static readonly CodePointIndexer CjkCHS;

		// Token: 0x04000183 RID: 387
		public static readonly CodePointIndexer Cjk;
	}
}
