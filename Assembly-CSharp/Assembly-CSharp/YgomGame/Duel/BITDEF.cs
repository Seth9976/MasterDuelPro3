using System;

namespace YgomGame.Duel
{
	// Token: 0x02000C90 RID: 3216
	internal static class BITDEF
	{
		// Token: 0x06005C17 RID: 23575 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckBit_SHOW(byte src)
		{
			return false;
		}

		// Token: 0x06005C18 RID: 23576 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckBit_FACE(byte src)
		{
			return false;
		}

		// Token: 0x06005C19 RID: 23577 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckBit_INSIGHT(byte src)
		{
			return false;
		}

		// Token: 0x06005C1A RID: 23578 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckBit_INDENT(byte src)
		{
			return false;
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckBit_TEAM(byte src)
		{
			return false;
		}

		// Token: 0x06005C1C RID: 23580 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckBit_EXTENDINFO(byte src)
		{
			return false;
		}

		// Token: 0x06005C1D RID: 23581 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckBit_TURN(byte src)
		{
			return false;
		}

		// Token: 0x06005C1E RID: 23582 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckBit_OVERLAYUNIT(byte src)
		{
			return false;
		}

		// Token: 0x06005C1F RID: 23583 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool CheckBit_COINRESULT(byte src, int index)
		{
			return false;
		}

		// Token: 0x04009748 RID: 38728
		public const byte ZERO = 0;

		// Token: 0x04009749 RID: 38729
		public const byte SHOW = 1;

		// Token: 0x0400974A RID: 38730
		public const byte TURN = 2;

		// Token: 0x0400974B RID: 38731
		public const byte FACE = 4;

		// Token: 0x0400974C RID: 38732
		public const byte INSIGHT = 8;

		// Token: 0x0400974D RID: 38733
		public const byte INDENT = 16;

		// Token: 0x0400974E RID: 38734
		public const byte TEAM = 32;

		// Token: 0x0400974F RID: 38735
		public const byte EXTENDINFO = 64;

		// Token: 0x04009750 RID: 38736
		public const byte OVERLAYUNIT = 128;

		// Token: 0x04009751 RID: 38737
		public const byte TYPE_SIDE_NONE = 0;

		// Token: 0x04009752 RID: 38738
		public const byte TYPE_SIDE_CARD = 1;

		// Token: 0x04009753 RID: 38739
		public const byte TYPE_SIDE_ICON = 2;

		// Token: 0x04009754 RID: 38740
		public const byte TYPE_CENTER_ACT = 3;

		// Token: 0x04009755 RID: 38741
		public const byte TYPE_CENTER_LPC = 4;

		// Token: 0x04009756 RID: 38742
		public const byte TYPE_CENTER_CC = 5;

		// Token: 0x04009757 RID: 38743
		public const byte TYPE_CENTER_DICE = 6;

		// Token: 0x04009758 RID: 38744
		public const byte TYPE_CENTER_COIN = 7;

		// Token: 0x04009759 RID: 38745
		public const int FRAMEIDBIAS = 16;

		// Token: 0x0400975A RID: 38746
		public const int ICONIDMASK = 255;

		// Token: 0x0400975B RID: 38747
		public const int EFFECTIDBIAS = 16;

		// Token: 0x0400975C RID: 38748
		public const int EFXBEGINBIAS = 0;

		// Token: 0x0400975D RID: 38749
		public const int EFXENDBIAS = 16;

		// Token: 0x0400975E RID: 38750
		public const int CARDMASK = 65535;

		// Token: 0x0400975F RID: 38751
		public const int EFXMASK = 65535;
	}
}
