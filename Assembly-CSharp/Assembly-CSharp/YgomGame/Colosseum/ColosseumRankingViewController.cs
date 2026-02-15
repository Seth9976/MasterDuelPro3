using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Duel;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Colosseum
{
	// Token: 0x02001058 RID: 4184
	public class ColosseumRankingViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06007DAD RID: 32173 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007DAE RID: 32174 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007DAF RID: 32175 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007DB0 RID: 32176 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIChallengeRanking()
		{
		}

		// Token: 0x06007DB1 RID: 32177 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPITournamentRanking()
		{
		}

		// Token: 0x06007DB2 RID: 32178 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitRanking()
		{
		}

		// Token: 0x06007DB3 RID: 32179 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateRanking(List<object> rankingList)
		{
		}

		// Token: 0x06007DB4 RID: 32180 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06007DB5 RID: 32181 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x06007DB6 RID: 32182 RVA: 0x0000216A File Offset: 0x0000036A
		protected string GetWinLoseText(Dictionary<string, object> dic)
		{
			return null;
		}

		// Token: 0x06007DB7 RID: 32183 RVA: 0x0000216A File Offset: 0x0000036A
		protected string GetWinLoseText(string win, string lose, string draw)
		{
			return null;
		}

		// Token: 0x06007DB8 RID: 32184 RVA: 0x0000216A File Offset: 0x0000036A
		protected string GetWinLoseTextFromNum(int win, int lose, int draw)
		{
			return null;
		}

		// Token: 0x0400B5DC RID: 46556
		protected readonly string ROOT_MYORDER_LABEL;

		// Token: 0x0400B5DD RID: 46557
		protected readonly string ROOT_RANK1_LABEL;

		// Token: 0x0400B5DE RID: 46558
		protected readonly string ROOT_RANK2_LABEL;

		// Token: 0x0400B5DF RID: 46559
		protected readonly string ROOT_RANK3_LABEL;

		// Token: 0x0400B5E0 RID: 46560
		protected readonly string SCROLL_RANKING_LABEL;

		// Token: 0x0400B5E1 RID: 46561
		protected readonly string BUTTON_LABEL;

		// Token: 0x0400B5E2 RID: 46562
		protected readonly string IMG_ICON_LABEL;

		// Token: 0x0400B5E3 RID: 46563
		protected readonly string IMG_RANK_LABEL;

		// Token: 0x0400B5E4 RID: 46564
		protected readonly string IMG_ARROW_LABEL;

		// Token: 0x0400B5E5 RID: 46565
		protected readonly string PLATFORM_NAME_LABEL;

		// Token: 0x0400B5E6 RID: 46566
		protected readonly string PLATFORM_ICON_LABEL;

		// Token: 0x0400B5E7 RID: 46567
		protected readonly string TXT_ORDER_LABEL;

		// Token: 0x0400B5E8 RID: 46568
		protected readonly string TXT_SCORE_LABEL;

		// Token: 0x0400B5E9 RID: 46569
		protected readonly string TXT_LOW_RANK_LABEL;

		// Token: 0x0400B5EA RID: 46570
		protected int TOP_RANK;

		// Token: 0x0400B5EB RID: 46571
		protected InfinityScrollView isv;

		// Token: 0x0400B5EC RID: 46572
		protected ElementObjectManager rootMyOrder;

		// Token: 0x0400B5ED RID: 46573
		protected List<ElementObjectManager> rootRanks;

		// Token: 0x0400B5EE RID: 46574
		protected int tid;

		// Token: 0x0400B5EF RID: 46575
		protected ColosseumUtil.RankingType type;

		// Token: 0x0400B5F0 RID: 46576
		protected Util.GameMode mode;

		// Token: 0x0400B5F1 RID: 46577
		protected List<ColosseumRankingViewController.RankingTemplate> rankingTemplates;

		// Token: 0x0400B5F2 RID: 46578
		private long myPcode;

		// Token: 0x0400B5F3 RID: 46579
		private int myIconId;

		// Token: 0x0400B5F4 RID: 46580
		private int myFrameId;

		// Token: 0x02001059 RID: 4185
		protected class RankingTemplate
		{
			// Token: 0x06007DBA RID: 32186 RVA: 0x00002739 File Offset: 0x00000939
			private RankingTemplate(long pcode, string name, int order, int iconID, int frameID, int rank, int tier, bool isResistedPlatform = false, bool isSamePlatform = false, string platformName = "")
			{
			}

			// Token: 0x06007DBB RID: 32187 RVA: 0x00002739 File Offset: 0x00000939
			public RankingTemplate(long pcode, string name, int order, int iconID, int frameID, int rank, int tier, int score, bool isResistedPlatform = false, bool isSamePlatform = false, string platformName = "")
			{
			}

			// Token: 0x06007DBC RID: 32188 RVA: 0x00002739 File Offset: 0x00000939
			public RankingTemplate(long pcode, string name, int order, int iconID, int frameID, int rank, int tier, int win, int lose, int draw, bool isResistedPlatform = false, bool isSamePlatform = false, string platformName = "")
			{
			}

			// Token: 0x0400B5F5 RID: 46581
			public readonly long pcode;

			// Token: 0x0400B5F6 RID: 46582
			public readonly string name;

			// Token: 0x0400B5F7 RID: 46583
			public readonly int order;

			// Token: 0x0400B5F8 RID: 46584
			public readonly int iconID;

			// Token: 0x0400B5F9 RID: 46585
			public readonly int frameID;

			// Token: 0x0400B5FA RID: 46586
			public readonly int rank;

			// Token: 0x0400B5FB RID: 46587
			public readonly int tier;

			// Token: 0x0400B5FC RID: 46588
			public readonly int score;

			// Token: 0x0400B5FD RID: 46589
			public readonly int win;

			// Token: 0x0400B5FE RID: 46590
			public readonly int lose;

			// Token: 0x0400B5FF RID: 46591
			public readonly int draw;

			// Token: 0x0400B600 RID: 46592
			public readonly bool isResistedPlatform;

			// Token: 0x0400B601 RID: 46593
			public readonly bool isSamePlatform;

			// Token: 0x0400B602 RID: 46594
			public readonly string platformName;
		}
	}
}
