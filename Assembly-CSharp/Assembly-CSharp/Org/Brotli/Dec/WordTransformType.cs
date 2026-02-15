using System;

namespace Org.Brotli.Dec
{
	// Token: 0x02000086 RID: 134
	internal sealed class WordTransformType
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000A0E5 File Offset: 0x000082E5
		internal static int GetOmitFirst(int type)
		{
			if (type < 12)
			{
				return 0;
			}
			return type - 12 + 1;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A0F4 File Offset: 0x000082F4
		internal static int GetOmitLast(int type)
		{
			if (type > 9)
			{
				return 0;
			}
			return type - 1 + 1;
		}

		// Token: 0x04000340 RID: 832
		internal const int Identity = 0;

		// Token: 0x04000341 RID: 833
		internal const int OmitLast1 = 1;

		// Token: 0x04000342 RID: 834
		internal const int OmitLast2 = 2;

		// Token: 0x04000343 RID: 835
		internal const int OmitLast3 = 3;

		// Token: 0x04000344 RID: 836
		internal const int OmitLast4 = 4;

		// Token: 0x04000345 RID: 837
		internal const int OmitLast5 = 5;

		// Token: 0x04000346 RID: 838
		internal const int OmitLast6 = 6;

		// Token: 0x04000347 RID: 839
		internal const int OmitLast7 = 7;

		// Token: 0x04000348 RID: 840
		internal const int OmitLast8 = 8;

		// Token: 0x04000349 RID: 841
		internal const int OmitLast9 = 9;

		// Token: 0x0400034A RID: 842
		internal const int UppercaseFirst = 10;

		// Token: 0x0400034B RID: 843
		internal const int UppercaseAll = 11;

		// Token: 0x0400034C RID: 844
		internal const int OmitFirst1 = 12;

		// Token: 0x0400034D RID: 845
		internal const int OmitFirst2 = 13;

		// Token: 0x0400034E RID: 846
		internal const int OmitFirst3 = 14;

		// Token: 0x0400034F RID: 847
		internal const int OmitFirst4 = 15;

		// Token: 0x04000350 RID: 848
		internal const int OmitFirst5 = 16;

		// Token: 0x04000351 RID: 849
		internal const int OmitFirst6 = 17;

		// Token: 0x04000352 RID: 850
		internal const int OmitFirst7 = 18;

		// Token: 0x04000353 RID: 851
		internal const int OmitFirst8 = 19;

		// Token: 0x04000354 RID: 852
		internal const int OmitFirst9 = 20;
	}
}
