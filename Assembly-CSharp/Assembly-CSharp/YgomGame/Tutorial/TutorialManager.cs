using System;
using System.Collections.Generic;

namespace YgomGame.Tutorial
{
	// Token: 0x02000839 RID: 2105
	public class TutorialManager
	{
		// Token: 0x060040E0 RID: 16608 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsHomeGuidancePassed()
		{
			return false;
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x0000216D File Offset: 0x0000036D
		public static void FetchInfo(Action onReceivedFunc = null)
		{
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckGoingTutorial(Action onGoingTutorial, Action onGoingStructDeckSelect, Action onGoingNameEntry, Action onGoingHome)
		{
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsFirstVisit(TutorialManager.FirstVisitMenu menu)
		{
			return false;
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Visited(TutorialManager.FirstVisitMenu menu)
		{
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetFirstVisitData(TutorialManager.FirstVisitMenu menu)
		{
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetFirstVisitData()
		{
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadFirstVisitData()
		{
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetTutorialDuelistName()
		{
			return null;
		}

		// Token: 0x040039E6 RID: 14822
		private const string FIRSTVISIT_SAVEPATH = "Tutorial_FirstVisit";

		// Token: 0x040039E7 RID: 14823
		private static Dictionary<int, bool> s_FirstVisitFlag;

		// Token: 0x0200083A RID: 2106
		private enum Step
		{
			// Token: 0x040039E9 RID: 14825
			NOT_GET_DECK,
			// Token: 0x040039EA RID: 14826
			NO_INPUT_NAME,
			// Token: 0x040039EB RID: 14827
			NO_GUIDANCE
		}

		// Token: 0x0200083B RID: 2107
		public enum FirstVisitMenu
		{
			// Token: 0x040039ED RID: 14829
			DUEL_STANDARD,
			// Token: 0x040039EE RID: 14830
			SHOP,
			// Token: 0x040039EF RID: 14831
			DECK_EDIT,
			// Token: 0x040039F0 RID: 14832
			QUEST,
			// Token: 0x040039F1 RID: 14833
			HOME_ENTER_ONCE,
			// Token: 0x040039F2 RID: 14834
			RENTAL_CARD,
			// Token: 0x040039F3 RID: 14835
			DUELLIVE,
			// Token: 0x040039F4 RID: 14836
			SHOP_MONSTER_CUTIN,
			// Token: 0x040039F5 RID: 14837
			RESERVED9,
			// Token: 0x040039F6 RID: 14838
			RESERVED10,
			// Token: 0x040039F7 RID: 14839
			RESERVED11,
			// Token: 0x040039F8 RID: 14840
			RESERVED12,
			// Token: 0x040039F9 RID: 14841
			RESERVED13,
			// Token: 0x040039FA RID: 14842
			RESERVED14,
			// Token: 0x040039FB RID: 14843
			RESERVED15,
			// Token: 0x040039FC RID: 14844
			RESERVED16,
			// Token: 0x040039FD RID: 14845
			RESERVED17,
			// Token: 0x040039FE RID: 14846
			RESERVED18,
			// Token: 0x040039FF RID: 14847
			RESERVED19,
			// Token: 0x04003A00 RID: 14848
			RESERVED20
		}
	}
}
