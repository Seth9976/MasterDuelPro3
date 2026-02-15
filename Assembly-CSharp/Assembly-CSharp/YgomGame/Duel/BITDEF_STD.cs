using System;

namespace YgomGame.Duel
{
	// Token: 0x02000C91 RID: 3217
	internal static class BITDEF_STD
	{
		// Token: 0x06005C20 RID: 23584 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetBitData(ref ulong bitdata, int value, int shift)
		{
		}

		// Token: 0x06005C21 RID: 23585 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetBitData(ref uint bitdata, int value, int shift)
		{
		}

		// Token: 0x06005C22 RID: 23586 RVA: 0x000029CC File Offset: 0x00000BCC
		public static byte GetByteData(ulong bitdata, int shift)
		{
			return 0;
		}

		// Token: 0x06005C23 RID: 23587 RVA: 0x000029CC File Offset: 0x00000BCC
		public static short GetShortData(ulong bitdata, int shift)
		{
			return 0;
		}

		// Token: 0x06005C24 RID: 23588 RVA: 0x000029CC File Offset: 0x00000BCC
		public static byte GetByteData(uint bitdata, int shift)
		{
			return 0;
		}

		// Token: 0x06005C25 RID: 23589 RVA: 0x000029CC File Offset: 0x00000BCC
		public static short GetShortData(uint bitdata, int shift)
		{
			return 0;
		}

		// Token: 0x06005C26 RID: 23590 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetIntData(ulong bitdata, int shift)
		{
			return 0;
		}

		// Token: 0x04009760 RID: 38752
		public const int SHIFT_PLAYERIDC = 0;

		// Token: 0x04009761 RID: 38753
		public const int SHIFT_TURN = 32;

		// Token: 0x04009762 RID: 38754
		public const int SHIFT_PLAYERIDL = 0;

		// Token: 0x04009763 RID: 38755
		public const int SHIFT_PLAYERIDR = 16;

		// Token: 0x04009764 RID: 38756
		public const uint BYTEMASK = 255U;

		// Token: 0x04009765 RID: 38757
		public const uint SHORTMASK = 65535U;

		// Token: 0x04009766 RID: 38758
		public const uint INTMASK = 4294967295U;
	}
}
