using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Duel;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI.InfinityScroll;
using YgomSystem.YGomTMPro;

namespace YgomGame.Colosseum
{
	// Token: 0x02001054 RID: 4180
	public class ColosseumRankingDuelistCupViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06007D98 RID: 32152 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007D99 RID: 32153 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007D9A RID: 32154 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007D9B RID: 32155 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPIDuelistCupRanking(int cid)
		{
		}

		// Token: 0x06007D9C RID: 32156 RVA: 0x0000216D File Offset: 0x0000036D
		private void CallAPI_WCSRanking(int wcs_id)
		{
		}

		// Token: 0x06007D9D RID: 32157 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetActiveFooterArea(bool active)
		{
		}

		// Token: 0x06007D9E RID: 32158 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitRanking()
		{
		}

		// Token: 0x06007D9F RID: 32159 RVA: 0x0000216D File Offset: 0x0000036D
		private void setFooterAreaCallBack()
		{
		}

		// Token: 0x06007DA0 RID: 32160 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickChangeArea()
		{
		}

		// Token: 0x06007DA1 RID: 32161 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateMyRanking()
		{
		}

		// Token: 0x06007DA2 RID: 32162 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateRanking(Dictionary<string, object> rankingList, bool isChangeRegion = false)
		{
		}

		// Token: 0x06007DA3 RID: 32163 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06007DA4 RID: 32164 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x06007DA5 RID: 32165 RVA: 0x0000216A File Offset: 0x0000036A
		protected string GetWinLoseText(Dictionary<string, object> dic)
		{
			return null;
		}

		// Token: 0x06007DA6 RID: 32166 RVA: 0x0000216A File Offset: 0x0000036A
		protected string GetWinLoseText(string win, string lose, string draw)
		{
			return null;
		}

		// Token: 0x06007DA7 RID: 32167 RVA: 0x0000216A File Offset: 0x0000036A
		protected string GetWinLoseTextFromNum(int win, int lose, int draw)
		{
			return null;
		}

		// Token: 0x0400B59F RID: 46495
		protected readonly string ROOT_MYORDER_LABEL;

		// Token: 0x0400B5A0 RID: 46496
		protected readonly string ROOT_RANK1_LABEL;

		// Token: 0x0400B5A1 RID: 46497
		protected readonly string ROOT_RANK2_LABEL;

		// Token: 0x0400B5A2 RID: 46498
		protected readonly string ROOT_RANK3_LABEL;

		// Token: 0x0400B5A3 RID: 46499
		protected readonly string SCROLL_RANKING_LABEL;

		// Token: 0x0400B5A4 RID: 46500
		protected readonly string BUTTON_LABEL;

		// Token: 0x0400B5A5 RID: 46501
		protected readonly string IMG_ICON_LABEL;

		// Token: 0x0400B5A6 RID: 46502
		protected readonly string IMG_RANK_LABEL;

		// Token: 0x0400B5A7 RID: 46503
		protected readonly string IMG_ARROW_LABEL;

		// Token: 0x0400B5A8 RID: 46504
		protected readonly string PLATFORM_NAME_LABEL;

		// Token: 0x0400B5A9 RID: 46505
		protected readonly string PLATFORM_ICON_LABEL;

		// Token: 0x0400B5AA RID: 46506
		protected readonly string TXT_ORDER_LABEL;

		// Token: 0x0400B5AB RID: 46507
		protected readonly string TXT_SCORE_LABEL;

		// Token: 0x0400B5AC RID: 46508
		protected readonly string TXT_AGGREGATE_MY_TEXT;

		// Token: 0x0400B5AD RID: 46509
		protected readonly string TXT_AGGREGATE_TEXT;

		// Token: 0x0400B5AE RID: 46510
		protected readonly string TXT_CAUTION_TEXT;

		// Token: 0x0400B5AF RID: 46511
		protected readonly string ROOT_WCS_MYORDER_LABEL;

		// Token: 0x0400B5B0 RID: 46512
		protected readonly string TXT_WCS_SCORE_NAME_LABEL;

		// Token: 0x0400B5B1 RID: 46513
		protected readonly string TXT_WCS_SCORE_LABEL;

		// Token: 0x0400B5B2 RID: 46514
		protected readonly string TXT_WCS_ORDER_NAME_LABEL;

		// Token: 0x0400B5B3 RID: 46515
		protected readonly string TXT_WCS_REGION_ORDER_NAME_LABEL;

		// Token: 0x0400B5B4 RID: 46516
		protected readonly string TXT_WCS_ORDER_LABEL;

		// Token: 0x0400B5B5 RID: 46517
		protected readonly string TXT_WCS_REGION_ORDER_LABEL;

		// Token: 0x0400B5B6 RID: 46518
		protected readonly string TXT_TITLE;

		// Token: 0x0400B5B7 RID: 46519
		protected readonly string FOOTER_LABEL;

		// Token: 0x0400B5B8 RID: 46520
		protected readonly string EOM_BUTTON_CAHNGE_AREA_LABEL;

		// Token: 0x0400B5B9 RID: 46521
		protected int TOP_RANK;

		// Token: 0x0400B5BA RID: 46522
		protected InfinityScrollView isv;

		// Token: 0x0400B5BB RID: 46523
		protected ElementObjectManager rootMyOrder;

		// Token: 0x0400B5BC RID: 46524
		protected ElementObjectManager wcsRootMyOrder;

		// Token: 0x0400B5BD RID: 46525
		protected List<ElementObjectManager> rootRanks;

		// Token: 0x0400B5BE RID: 46526
		protected int tid;

		// Token: 0x0400B5BF RID: 46527
		protected int cid;

		// Token: 0x0400B5C0 RID: 46528
		protected ColosseumUtil.RankingType type;

		// Token: 0x0400B5C1 RID: 46529
		protected Util.GameMode mode;

		// Token: 0x0400B5C2 RID: 46530
		protected List<ColosseumRankingDuelistCupViewController.RankingDuelistCupTemplate> rankingTemplates;

		// Token: 0x0400B5C3 RID: 46531
		protected List<string> rankingAreaList;

		// Token: 0x0400B5C4 RID: 46532
		protected string currentArea;

		// Token: 0x0400B5C5 RID: 46533
		protected int currentAreaIdx;

		// Token: 0x0400B5C6 RID: 46534
		private ExtendedTextMeshProUGUI TitleText;

		// Token: 0x0400B5C7 RID: 46535
		private long myPcode;

		// Token: 0x0400B5C8 RID: 46536
		private int myIconId;

		// Token: 0x0400B5C9 RID: 46537
		private int myFrameId;

		// Token: 0x0400B5CA RID: 46538
		private int DefaultThresholdNum;

		// Token: 0x02001055 RID: 4181
		public enum regionType
		{
			// Token: 0x0400B5CC RID: 46540
			REGION_JAPAN = 1,
			// Token: 0x0400B5CD RID: 46541
			REGION_ASIA,
			// Token: 0x0400B5CE RID: 46542
			REGION_NORTH_AMERICA,
			// Token: 0x0400B5CF RID: 46543
			REGION_LATIN_AMERICA,
			// Token: 0x0400B5D0 RID: 46544
			REGION_EUROPE_OTHER
		}

		// Token: 0x02001056 RID: 4182
		protected class RankingDuelistCupTemplate
		{
			// Token: 0x06007DA9 RID: 32169 RVA: 0x00002739 File Offset: 0x00000939
			private RankingDuelistCupTemplate(long pcode, string name, string order, int iconID, int frameID, bool isResistedPlatform = false, bool isSamePlatform = false, string platformName = "")
			{
			}

			// Token: 0x06007DAA RID: 32170 RVA: 0x00002739 File Offset: 0x00000939
			public RankingDuelistCupTemplate(long pcode, string name, string order, int iconID, int frameID, int dp, bool isResistedPlatform = false, bool isSamePlatform = false, string platformName = "")
			{
			}

			// Token: 0x0400B5D1 RID: 46545
			public readonly int dp;

			// Token: 0x0400B5D2 RID: 46546
			public readonly long pcode;

			// Token: 0x0400B5D3 RID: 46547
			public readonly string name;

			// Token: 0x0400B5D4 RID: 46548
			public readonly string order;

			// Token: 0x0400B5D5 RID: 46549
			public readonly int iconID;

			// Token: 0x0400B5D6 RID: 46550
			public readonly int frameID;

			// Token: 0x0400B5D7 RID: 46551
			public readonly int is_same_os;

			// Token: 0x0400B5D8 RID: 46552
			public readonly int online_id;

			// Token: 0x0400B5D9 RID: 46553
			public readonly bool isResistedPlatform;

			// Token: 0x0400B5DA RID: 46554
			public readonly bool isSamePlatform;

			// Token: 0x0400B5DB RID: 46555
			public readonly string platformName;
		}
	}
}
