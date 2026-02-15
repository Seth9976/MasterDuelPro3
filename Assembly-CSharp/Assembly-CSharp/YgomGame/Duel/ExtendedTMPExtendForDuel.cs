using System;
using TMPro;

namespace YgomGame.Duel
{
	// Token: 0x02000E8A RID: 3722
	public static class ExtendedTMPExtendForDuel
	{
		// Token: 0x06006C0E RID: 27662 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLifePoint(this TextMeshPro exTmp, int value, int digitnum = 0, bool coloring = false)
		{
		}

		// Token: 0x06006C0F RID: 27663 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetLifePoint(this TextMeshProUGUI exTmp, int value, int digitnum = 0, bool coloring = false)
		{
		}

		// Token: 0x06006C10 RID: 27664 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Set2DigitNum(this TextMeshPro exTmp, int value, int orgvalue)
		{
		}

		// Token: 0x06006C11 RID: 27665 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Set2DigitNum(this TextMeshProUGUI exTmp, int value, int orgvalue)
		{
		}

		// Token: 0x06006C12 RID: 27666 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetOverlayUnit(this TextMeshPro exTmp, int value)
		{
		}

		// Token: 0x06006C13 RID: 27667 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetOverlayUnit(this TextMeshProUGUI exTmp, int value)
		{
		}

		// Token: 0x06006C14 RID: 27668 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetLifePointStr(int value, int digitnum = 0, bool coloring = false)
		{
			return null;
		}

		// Token: 0x06006C15 RID: 27669 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetLevelRankStr(int value, int orgvalue)
		{
			return null;
		}

		// Token: 0x0400A778 RID: 42872
		public const int MAXDISPBATTLEPOWER = 99999999;

		// Token: 0x0400A779 RID: 42873
		public const int MAXDISPLIFEPOINT = 999999;

		// Token: 0x0400A77A RID: 42874
		public const int MAX2DIGITNUM = 99;
	}
}
