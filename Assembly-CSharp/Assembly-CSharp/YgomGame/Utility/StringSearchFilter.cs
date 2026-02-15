using System;

namespace YgomGame.Utility
{
	// Token: 0x02000834 RID: 2100
	public static class StringSearchFilter
	{
		// Token: 0x060040B0 RID: 16560 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool SplitFilter(string keywordStr, string sourceStr)
		{
			return false;
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x0000216A File Offset: 0x0000036A
		public static string convertSearchText(string src)
		{
			return null;
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x0000216A File Offset: 0x0000036A
		private static string toZenkaku(string src)
		{
			return null;
		}

		// Token: 0x060040B3 RID: 16563 RVA: 0x0000216A File Offset: 0x0000036A
		private static string toKatakana_Komoji(string src)
		{
			return null;
		}

		// Token: 0x040039A3 RID: 14755
		private const int HAN_SPACE = 32;

		// Token: 0x040039A4 RID: 14756
		private const int HAN_YEN = 92;

		// Token: 0x040039A5 RID: 14757
		private const int HAN_ALPHA_START = 33;

		// Token: 0x040039A6 RID: 14758
		private const int HAN_ALPHA_END = 126;

		// Token: 0x040039A7 RID: 14759
		private const int ZEN_SPACE = 12288;

		// Token: 0x040039A8 RID: 14760
		private const int ZEN_YEN = 65509;

		// Token: 0x040039A9 RID: 14761
		private const int ZEN_ALPHA_START = 65281;

		// Token: 0x040039AA RID: 14762
		private const int ZEN_ALPHA_OFFSET = 65248;

		// Token: 0x040039AB RID: 14763
		private const int HIRA_START = 12352;

		// Token: 0x040039AC RID: 14764
		private const int HIRA_END = 12442;

		// Token: 0x040039AD RID: 14765
		private const int KATA_START = 12448;

		// Token: 0x040039AE RID: 14766
		private const int KATA_OFFSET = 96;

		// Token: 0x040039AF RID: 14767
		private const int ZAL_START = 65313;

		// Token: 0x040039B0 RID: 14768
		private const int ZAL_END = 65338;

		// Token: 0x040039B1 RID: 14769
		private const int ZAS_START = 65345;

		// Token: 0x040039B2 RID: 14770
		private const int ZAS_OFFSET = 32;
	}
}
