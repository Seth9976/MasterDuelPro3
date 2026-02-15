using System;
using System.Collections.Generic;
using YgomGame.Duel;
using YgomGame.Menu;
using YgomGame.Team;
using YgomSystem.Network;
using YgomSystem.UI;

namespace YgomGame.Colosseum
{
	// Token: 0x02001079 RID: 4217
	public class ColosseumUtil
	{
		// Token: 0x06007E42 RID: 32322 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallDuelBeginPvE(ViewControllerManager manager, Util.GameMode gameMode, int tid = 0, bool isSwap = false)
		{
		}

		// Token: 0x06007E43 RID: 32323 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallDuelBeginSolo(ViewController swapTarget, ViewControllerManager manager, int chapterID, ColosseumUtil.Turn turn = ColosseumUtil.Turn.NONE)
		{
		}

		// Token: 0x06007E44 RID: 32324 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StartMatching(ViewControllerManager manager, PvpMenuDefine.MatchingType type, PvpMenuDefine.MatchingType match, int id = 0, int logoId = 0, int rentalState = 0, int researchTime = 0)
		{
		}

		// Token: 0x06007E45 RID: 32325 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StartMatchingTeam(ViewControllerManager manager, bool isLeader, OpponentTeamInfo oppTeamInfo = null)
		{
		}

		// Token: 0x06007E46 RID: 32326 RVA: 0x0000216D File Offset: 0x0000036D
		private static void StartMatching(ViewControllerManager manager, PvpMenuDefine.MatchingType type, PvpMenuDefine.MatchingType match, Dictionary<string, object> duelParams, Dictionary<string, object> matchingParams, Dictionary<string, object> otherParams = null)
		{
		}

		// Token: 0x06007E47 RID: 32327 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallTournamentEntry(int tid, Action endAction = null)
		{
		}

		// Token: 0x06007E48 RID: 32328 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetOrderString(int order, bool changeScale = true)
		{
			return null;
		}

		// Token: 0x06007E49 RID: 32329 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallAPIDeckCheck(ColosseumUtil.PlayMode playMode, int tid = 0, Action onFinish = null, int regulationId = 0)
		{
		}

		// Token: 0x06007E4A RID: 32330 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallAPIPlayReplay(Util.GameMode gameMode, long did, int idx = 0, int eid = 0, Action<PvPCode> onFailed = null)
		{
		}

		// Token: 0x06007E4B RID: 32331 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetResultStringWithColor(Engine.ResultType resultType)
		{
			return null;
		}

		// Token: 0x06007E4C RID: 32332 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HandleResultCodeDuelMenu(Handle handle, Action onSuccess = null, Action<DuelMenuCode> onFailed = null)
		{
		}

		// Token: 0x06007E4D RID: 32333 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HandleResultCodeChallenge(Handle handle, Action onSuccess = null, Action<ChallengeCode> onFailed = null)
		{
		}

		// Token: 0x06007E4E RID: 32334 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HandleResultCodeTournament(Handle handle, Action onSuccess = null, Action<TournamentCode> onFailed = null)
		{
		}

		// Token: 0x06007E4F RID: 32335 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HandleResultCodeExhibition(Handle handle, Action onSuccess = null, Action<ExhibitionCode> onFailed = null)
		{
		}

		// Token: 0x06007E50 RID: 32336 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HandleResultCodeCasual(Handle handle, Action onSuccess = null, Action<CasualCode> onFailed = null)
		{
		}

		// Token: 0x06007E51 RID: 32337 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HandleResultCodeRankEvent(Handle handle, Action onSuccess = null, Action<RankEventCode> onFailed = null)
		{
		}

		// Token: 0x06007E52 RID: 32338 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HandleResultCodeCup(Handle handle, Action onSuccess = null, Action<CupCode> onFailed = null)
		{
		}

		// Token: 0x06007E53 RID: 32339 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HandleResultCodeWcs(Handle handle, Action onSuccess = null, Action<WcsCode> onFailed = null)
		{
		}

		// Token: 0x06007E54 RID: 32340 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HandleResultCodeDuelTrial(Handle handle, Action onSuccess = null, Action<DuelTrialCode> onFailed = null)
		{
		}

		// Token: 0x06007E55 RID: 32341 RVA: 0x0000216D File Offset: 0x0000036D
		public static void HandleResultCodeVersus(Handle handle, Action onSuccess = null, Action<VersusCode> onFailed = null)
		{
		}

		// Token: 0x06007E56 RID: 32342 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool PopColosseumViewControler(ColosseumUtil.PlayMode playMode)
		{
			return false;
		}

		// Token: 0x06007E57 RID: 32343 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenItemConfirmDialogUnpackRight(string title, bool isPeriod, int itemCategory, int itemId, int itemNum, Action closeCallback = null, bool hideNum = false, int shopId = 0)
		{
		}

		// Token: 0x0200107A RID: 4218
		public enum StatusTournament
		{
			// Token: 0x0400B6C7 RID: 46791
			NOT_WATCHED_START = -3,
			// Token: 0x0400B6C8 RID: 46792
			NOT_ENTRY,
			// Token: 0x0400B6C9 RID: 46793
			ENTRY_IMPOSSIBLE,
			// Token: 0x0400B6CA RID: 46794
			ENTRY_POSSIBLE,
			// Token: 0x0400B6CB RID: 46795
			ENTRY,
			// Token: 0x0400B6CC RID: 46796
			JOIN,
			// Token: 0x0400B6CD RID: 46797
			DEFEATED,
			// Token: 0x0400B6CE RID: 46798
			RESULT_UNRECV,
			// Token: 0x0400B6CF RID: 46799
			RESULT_RECV,
			// Token: 0x0400B6D0 RID: 46800
			STATUS_RESULT_NO_ENTRY
		}

		// Token: 0x0200107B RID: 4219
		public enum StatusTournamentHolding
		{
			// Token: 0x0400B6D2 RID: 46802
			PRECEDE,
			// Token: 0x0400B6D3 RID: 46803
			ENTRY,
			// Token: 0x0400B6D4 RID: 46804
			START,
			// Token: 0x0400B6D5 RID: 46805
			FINISH,
			// Token: 0x0400B6D6 RID: 46806
			CLOSE
		}

		// Token: 0x0200107C RID: 4220
		public enum StatusExhibition
		{
			// Token: 0x0400B6D8 RID: 46808
			BEFORE_HOLDING = -1,
			// Token: 0x0400B6D9 RID: 46809
			NOT_WATCHED_START,
			// Token: 0x0400B6DA RID: 46810
			HOLDING,
			// Token: 0x0400B6DB RID: 46811
			AFTER_HOLDING
		}

		// Token: 0x0200107D RID: 4221
		public enum StatusRankEvent
		{
			// Token: 0x0400B6DD RID: 46813
			TERM_STATUS_PREPARE,
			// Token: 0x0400B6DE RID: 46814
			TERM_STATUS_OPEN,
			// Token: 0x0400B6DF RID: 46815
			TERM_STATUS_START,
			// Token: 0x0400B6E0 RID: 46816
			TERM_STATUS_END,
			// Token: 0x0400B6E1 RID: 46817
			TERM_STATUS_RESULT_END
		}

		// Token: 0x0200107E RID: 4222
		public enum StatusRankEventUser
		{
			// Token: 0x0400B6E3 RID: 46819
			STATUS_NONE,
			// Token: 0x0400B6E4 RID: 46820
			STATUS_JOIN
		}

		// Token: 0x0200107F RID: 4223
		public enum StatusDuelistCup
		{
			// Token: 0x0400B6E6 RID: 46822
			USER_STATUS_NO_ENTRY,
			// Token: 0x0400B6E7 RID: 46823
			USER_STATUS_ENTRY,
			// Token: 0x0400B6E8 RID: 46824
			USER_STATUS_STAGE_1ST,
			// Token: 0x0400B6E9 RID: 46825
			USER_STATUS_STAGE_2ND,
			// Token: 0x0400B6EA RID: 46826
			USER_STATUS_PRE_RESULT,
			// Token: 0x0400B6EB RID: 46827
			USER_STATUS_RESULT,
			// Token: 0x0400B6EC RID: 46828
			USER_STATUS_RESULT_COMP
		}

		// Token: 0x02001080 RID: 4224
		public enum StatusDuelTrial
		{
			// Token: 0x0400B6EE RID: 46830
			TERM_STATUS_PREPARE,
			// Token: 0x0400B6EF RID: 46831
			TERM_STATUS_OPEN,
			// Token: 0x0400B6F0 RID: 46832
			TERM_STATUS_START,
			// Token: 0x0400B6F1 RID: 46833
			TERM_STATUS_END,
			// Token: 0x0400B6F2 RID: 46834
			TERM_STATUS_RESULT_END
		}

		// Token: 0x02001081 RID: 4225
		public enum StatusVersus
		{
			// Token: 0x0400B6F4 RID: 46836
			TERM_STATUS_PREPARE,
			// Token: 0x0400B6F5 RID: 46837
			TERM_STATUS_OPEN,
			// Token: 0x0400B6F6 RID: 46838
			TERM_STATUS_START,
			// Token: 0x0400B6F7 RID: 46839
			TERM_STATUS_END,
			// Token: 0x0400B6F8 RID: 46840
			TERM_STATUS_RESULT_OPEN,
			// Token: 0x0400B6F9 RID: 46841
			TERM_STATUS_RESULT_END
		}

		// Token: 0x02001082 RID: 4226
		public enum TransitionProcess
		{
			// Token: 0x0400B6FB RID: 46843
			NONE,
			// Token: 0x0400B6FC RID: 46844
			NORMAL,
			// Token: 0x0400B6FD RID: 46845
			QUICK
		}

		// Token: 0x02001083 RID: 4227
		public enum RankingType
		{
			// Token: 0x0400B6FF RID: 46847
			SCORE,
			// Token: 0x0400B700 RID: 46848
			WINLOSE
		}

		// Token: 0x02001084 RID: 4228
		public enum ChallengeMode
		{
			// Token: 0x0400B702 RID: 46850
			STANDARD = 1,
			// Token: 0x0400B703 RID: 46851
			BOT = 10
		}

		// Token: 0x02001085 RID: 4229
		public enum StandardRank
		{
			// Token: 0x0400B705 RID: 46853
			ROOKIE = 1,
			// Token: 0x0400B706 RID: 46854
			BRONZE,
			// Token: 0x0400B707 RID: 46855
			SILVER,
			// Token: 0x0400B708 RID: 46856
			GOLD,
			// Token: 0x0400B709 RID: 46857
			PLATINUM,
			// Token: 0x0400B70A RID: 46858
			DIAMOND,
			// Token: 0x0400B70B RID: 46859
			MASTER,
			// Token: 0x0400B70C RID: 46860
			TEMP08,
			// Token: 0x0400B70D RID: 46861
			TEMP09,
			// Token: 0x0400B70E RID: 46862
			TEMP10
		}

		// Token: 0x02001086 RID: 4230
		public enum Turn
		{
			// Token: 0x0400B710 RID: 46864
			FIRST,
			// Token: 0x0400B711 RID: 46865
			SECOND,
			// Token: 0x0400B712 RID: 46866
			RANDOM,
			// Token: 0x0400B713 RID: 46867
			NONE = 10
		}

		// Token: 0x02001087 RID: 4231
		public enum PlayMode
		{
			// Token: 0x0400B715 RID: 46869
			NONE,
			// Token: 0x0400B716 RID: 46870
			RANK,
			// Token: 0x0400B717 RID: 46871
			TOURNAMENT,
			// Token: 0x0400B718 RID: 46872
			ROOM,
			// Token: 0x0400B719 RID: 46873
			EXHIBITION,
			// Token: 0x0400B71A RID: 46874
			FREE,
			// Token: 0x0400B71B RID: 46875
			DUELISTCUP,
			// Token: 0x0400B71C RID: 46876
			RANKEVENT,
			// Token: 0x0400B71D RID: 46877
			TEAMMATCH,
			// Token: 0x0400B71E RID: 46878
			DUELTRIAL,
			// Token: 0x0400B71F RID: 46879
			WCS,
			// Token: 0x0400B720 RID: 46880
			VERSUS,
			// Token: 0x0400B721 RID: 46881
			WCS_FINAL
		}

		// Token: 0x02001088 RID: 4232
		public enum Region
		{
			// Token: 0x0400B723 RID: 46883
			NONE,
			// Token: 0x0400B724 RID: 46884
			JAPAN,
			// Token: 0x0400B725 RID: 46885
			ASIA,
			// Token: 0x0400B726 RID: 46886
			NORTH_AMERICA,
			// Token: 0x0400B727 RID: 46887
			LATIN_AMERICA,
			// Token: 0x0400B728 RID: 46888
			EUROPE_OTHER
		}
	}
}
