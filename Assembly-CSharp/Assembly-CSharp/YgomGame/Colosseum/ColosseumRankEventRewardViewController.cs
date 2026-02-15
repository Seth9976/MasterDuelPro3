using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Colosseum
{
	// Token: 0x02001051 RID: 4177
	public class ColosseumRankEventRewardViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06007D8B RID: 32139 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007D8C RID: 32140 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007D8D RID: 32141 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007D8E RID: 32142 RVA: 0x0000216D File Offset: 0x0000036D
		public override void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06007D8F RID: 32143 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool TransitionUpdate(ViewController.TransitionType type)
		{
			return false;
		}

		// Token: 0x06007D90 RID: 32144 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateView()
		{
		}

		// Token: 0x06007D91 RID: 32145 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x06007D92 RID: 32146 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06007D93 RID: 32147 RVA: 0x0000216A File Offset: 0x0000036A
		private string RankEventConvert(int rank)
		{
			return null;
		}

		// Token: 0x06007D94 RID: 32148 RVA: 0x0000216A File Offset: 0x0000036A
		private IReadOnlyList<ValueTuple<SelectionItem, int, int>> CustomCollectionSelectionItems(GameObject rewardEntity)
		{
			return null;
		}

		// Token: 0x0400B587 RID: 46471
		private readonly string SCROLL_LABEL;

		// Token: 0x0400B588 RID: 46472
		private readonly string IMG_ITEM_LABEL;

		// Token: 0x0400B589 RID: 46473
		private readonly string IMG_ICON_LABEL;

		// Token: 0x0400B58A RID: 46474
		private readonly string IMG_RANK_LABEL;

		// Token: 0x0400B58B RID: 46475
		private readonly string IMG_RANK_BG_LABEL;

		// Token: 0x0400B58C RID: 46476
		private readonly string IMG_RECEIVED_LABEL;

		// Token: 0x0400B58D RID: 46477
		private readonly string IMG_ARROW_LABEL;

		// Token: 0x0400B58E RID: 46478
		private readonly string TXT_QUANTITY_LABEL;

		// Token: 0x0400B58F RID: 46479
		private readonly string TXT_MESSAGE_LABEL;

		// Token: 0x0400B590 RID: 46480
		private readonly string TXT_RANK_LABEL;

		// Token: 0x0400B591 RID: 46481
		private readonly string TXT_TIER_LABEL;

		// Token: 0x0400B592 RID: 46482
		private InfinityScrollView isv;

		// Token: 0x0400B593 RID: 46483
		private List<ColosseumRankEventRewardViewController.Data> dataList;

		// Token: 0x0400B594 RID: 46484
		private int defaultCursorIndex;

		// Token: 0x0400B595 RID: 46485
		private bool isSelectedDefault;

		// Token: 0x0400B596 RID: 46486
		private int id;

		// Token: 0x02001052 RID: 4178
		internal class ItemData
		{
			// Token: 0x06007D96 RID: 32150 RVA: 0x00002739 File Offset: 0x00000939
			internal ItemData(int itemID, int quantity, int itemCategory, bool is_period = false)
			{
			}

			// Token: 0x0400B597 RID: 46487
			internal int itemID;

			// Token: 0x0400B598 RID: 46488
			internal int quantity;

			// Token: 0x0400B599 RID: 46489
			internal int itemCategory;

			// Token: 0x0400B59A RID: 46490
			internal bool is_period;
		}

		// Token: 0x02001053 RID: 4179
		internal class Data
		{
			// Token: 0x06007D97 RID: 32151 RVA: 0x00002739 File Offset: 0x00000939
			public Data(int rank, int tier, bool received)
			{
			}

			// Token: 0x0400B59B RID: 46491
			internal List<ColosseumRankEventRewardViewController.ItemData> itemDatas;

			// Token: 0x0400B59C RID: 46492
			internal int rank;

			// Token: 0x0400B59D RID: 46493
			internal int tier;

			// Token: 0x0400B59E RID: 46494
			internal bool received;
		}
	}
}
