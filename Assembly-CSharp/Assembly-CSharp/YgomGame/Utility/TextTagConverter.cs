using System;
using System.Text.RegularExpressions;
using YgomGame.Card;

namespace YgomGame.Utility
{
	// Token: 0x02000836 RID: 2102
	public class TextTagConverter
	{
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x060040B9 RID: 16569 RVA: 0x0000216A File Offset: 0x0000036A
		private static Content cci
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x0000216A File Offset: 0x0000036A
		public static string Convert(string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
			return null;
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x0000216D File Offset: 0x0000036D
		public static void LoadDependencieTextDatas()
		{
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnloadDependencieTextDatas()
		{
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetIdAsInt(string src)
		{
			return 0;
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int GetCategoryAsInt(string src)
		{
			return 0;
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x0000216A File Offset: 0x0000036A
		private static string GetPathStr(string src)
		{
			return null;
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ReplaceMatch(ref string src, ref int diffIdx, Match match, string word)
		{
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertNoBreakSpace(ref string src)
		{
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertCardTag(ref string src)
		{
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertItemTag(ref string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertPeriodItemTag(ref string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertCardPackTag(ref string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertDuelModeTag(ref string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertRankTag(ref string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertTournamentNameTag(ref string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertExhibitionNameTag(ref string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertRankEventNameTag(ref string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertDuelistCupNameTag(ref string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertDuelTrialNameTag(ref string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertRarityNameTag(ref string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertPremiumNameTag(ref string src, TextGroupLoadHolder textGroupLoadHolder = null)
		{
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x0000216D File Offset: 0x0000036D
		private static void ConvertClientWorkPath(ref string src)
		{
		}

		// Token: 0x040039B4 RID: 14772
		private const string tagCardRegex = "<card(.+?)/>";

		// Token: 0x040039B5 RID: 14773
		private const string mrkRegex = "mrk\\s*=\\s*'\\d{4,5}'";

		// Token: 0x040039B6 RID: 14774
		private const string getCardRegex = "get\\s*=\\s*'(name|poss|rare)'";

		// Token: 0x040039B7 RID: 14775
		private const string paramMrkRegex = "'\\d{4,5}'";

		// Token: 0x040039B8 RID: 14776
		private const string paramGetCardRegex = "'(name|poss|rare)'";

		// Token: 0x040039B9 RID: 14777
		private const string tagItemRegex = "<item(.+?)/>";

		// Token: 0x040039BA RID: 14778
		private const string idRegex = "id\\s*=\\s*'\\d+'";

		// Token: 0x040039BB RID: 14779
		private const string getItemRegex = "get\\s*=\\s*'(name|poss|type)'";

		// Token: 0x040039BC RID: 14780
		private const string paramIdRegex = "'\\d+'";

		// Token: 0x040039BD RID: 14781
		private const string paramGetItemRegex = "'(name|poss|type)'";

		// Token: 0x040039BE RID: 14782
		private const string tagPeriodItemRegex = "<periodItem(.+?)/>";

		// Token: 0x040039BF RID: 14783
		private const string categoryRegex = "category\\s*=\\s*'\\d+'";

		// Token: 0x040039C0 RID: 14784
		private const string tagCardPackRegex = "<cardpack(.+?)/>";

		// Token: 0x040039C1 RID: 14785
		private const string getCardPackRegex = "get\\s*=\\s*'name'";

		// Token: 0x040039C2 RID: 14786
		private const string paramGetCardPackRegex = "'name'";

		// Token: 0x040039C3 RID: 14787
		private const string idRegex2 = "id\\s*=\\s*'\\d{2}'";

		// Token: 0x040039C4 RID: 14788
		private const string idRegex3 = "id\\s*=\\s*'\\d{3}'";

		// Token: 0x040039C5 RID: 14789
		private const string tagModeRegex = "<mode(.+?)/>";

		// Token: 0x040039C6 RID: 14790
		private const string tagRankRegex = "<rank(.+?)/>";

		// Token: 0x040039C7 RID: 14791
		private const string tagTournamentRegex = "<tournament(.+?)/>";

		// Token: 0x040039C8 RID: 14792
		private const string tagExhibitionRegex = "<exhibition(.+?)/>";

		// Token: 0x040039C9 RID: 14793
		private const string tagRankEventRegex = "<(rankevent.+?)/>";

		// Token: 0x040039CA RID: 14794
		private const string tagDuelistCupRegex = "<duelistcup(.+?)/>";

		// Token: 0x040039CB RID: 14795
		private const string tagDuelTrialRegex = "<(dueltrial.+?)/>";

		// Token: 0x040039CC RID: 14796
		private const string tagEventRegex = "<event(.+?)/>";

		// Token: 0x040039CD RID: 14797
		private const string tagRarityRegex = "<rarity(.+?)/>";

		// Token: 0x040039CE RID: 14798
		private const string tagPremiumRegex = "<prem(.+?)/>";

		// Token: 0x040039CF RID: 14799
		private const string tagCwRegex = "<cw(.+?)/>";

		// Token: 0x040039D0 RID: 14800
		private const string pathRegex = "path\\s*=\\s*'(.+?)'";

		// Token: 0x040039D1 RID: 14801
		private const string paramRegex = "'(.+?)'";

		// Token: 0x040039D2 RID: 14802
		private const int startIdxComma = 1;

		// Token: 0x040039D3 RID: 14803
		private const int lengthComma = 2;
	}
}
