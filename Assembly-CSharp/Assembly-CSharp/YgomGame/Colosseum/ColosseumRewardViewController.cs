using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Colosseum
{
	// Token: 0x02001073 RID: 4211
	public class ColosseumRewardViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FEE RID: 4078
		// (get) Token: 0x06007E23 RID: 32291 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007E24 RID: 32292 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007E25 RID: 32293 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007E26 RID: 32294 RVA: 0x0000216D File Offset: 0x0000036D
		public override void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06007E27 RID: 32295 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool TransitionUpdate(ViewController.TransitionType type)
		{
			return false;
		}

		// Token: 0x06007E28 RID: 32296 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateView()
		{
		}

		// Token: 0x06007E29 RID: 32297 RVA: 0x0000216A File Offset: 0x0000036A
		private IReadOnlyList<ValueTuple<SelectionItem, int, int>> CustomCollectionSelectionItems(GameObject rewardEntity)
		{
			return null;
		}

		// Token: 0x06007E2A RID: 32298 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06007E2B RID: 32299 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemExit(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06007E2C RID: 32300 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x0400B691 RID: 46737
		private readonly string SCROLL_LABEL;

		// Token: 0x0400B692 RID: 46738
		private readonly string IMG_ITEM_LABEL;

		// Token: 0x0400B693 RID: 46739
		private readonly string IMG_ICON_LABEL;

		// Token: 0x0400B694 RID: 46740
		private readonly string IMG_RANK_LABEL;

		// Token: 0x0400B695 RID: 46741
		private readonly string IMG_RANK_BG_LABEL;

		// Token: 0x0400B696 RID: 46742
		private readonly string IMG_RECEIVED_LABEL;

		// Token: 0x0400B697 RID: 46743
		private readonly string IMG_ARROW_LABEL;

		// Token: 0x0400B698 RID: 46744
		private readonly string TXT_QUANTITY_LABEL;

		// Token: 0x0400B699 RID: 46745
		private readonly string TXT_MESSAGE_LABEL;

		// Token: 0x0400B69A RID: 46746
		private readonly string TXT_RANK_LABEL;

		// Token: 0x0400B69B RID: 46747
		private readonly string TXT_TIER_LABEL;

		// Token: 0x0400B69C RID: 46748
		private InfinityScrollView isv;

		// Token: 0x0400B69D RID: 46749
		private List<ColosseumRewardViewController.Data> dataList;

		// Token: 0x0400B69E RID: 46750
		private int defaultCursorIndex;

		// Token: 0x0400B69F RID: 46751
		private bool isSelectedDefault;

		// Token: 0x02001074 RID: 4212
		internal class Data
		{
			// Token: 0x06007E2E RID: 32302 RVA: 0x00002739 File Offset: 0x00000939
			public Data(int rank, int tier, bool received)
			{
			}

			// Token: 0x0400B6A0 RID: 46752
			internal List<ColosseumRewardViewController.ItemData> itemDatas;

			// Token: 0x0400B6A1 RID: 46753
			internal int rank;

			// Token: 0x0400B6A2 RID: 46754
			internal int tier;

			// Token: 0x0400B6A3 RID: 46755
			internal bool received;
		}

		// Token: 0x02001075 RID: 4213
		internal class ItemData
		{
			// Token: 0x06007E2F RID: 32303 RVA: 0x00002739 File Offset: 0x00000939
			internal ItemData(int itemID, int quantity)
			{
			}

			// Token: 0x0400B6A4 RID: 46756
			internal int itemID;

			// Token: 0x0400B6A5 RID: 46757
			internal int quantity;
		}
	}
}
