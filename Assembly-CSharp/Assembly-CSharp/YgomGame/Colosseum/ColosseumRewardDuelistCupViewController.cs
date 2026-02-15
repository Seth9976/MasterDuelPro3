using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Colosseum
{
	// Token: 0x0200105F RID: 4191
	public class ColosseumRewardDuelistCupViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06007DE0 RID: 32224 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007DE1 RID: 32225 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06007DE2 RID: 32226 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06007DE3 RID: 32227 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06007DE4 RID: 32228 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateDisplay()
		{
		}

		// Token: 0x06007DE5 RID: 32229 RVA: 0x0000216A File Offset: 0x0000036A
		private ColosseumRewardDuelistCupViewController.ModeBehaviour GetModeBehaviour(ColosseumUtil.PlayMode playMode, string txtDate, ColosseumUtil.StatusDuelistCup status, bool isTabChange)
		{
			return null;
		}

		// Token: 0x06007DE6 RID: 32230 RVA: 0x0000216D File Offset: 0x0000036D
		public override void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06007DE7 RID: 32231 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
		{
		}

		// Token: 0x0400B633 RID: 46643
		private readonly string SCROLL_LABEL;

		// Token: 0x0400B634 RID: 46644
		private ColosseumRewardDuelistCupViewController.ModeBehaviour modeBehaviour;

		// Token: 0x0400B635 RID: 46645
		private InfinityScrollView isv;

		// Token: 0x0400B636 RID: 46646
		private List<SelectionItem> isvEdgeSelectSearchList;

		// Token: 0x0400B637 RID: 46647
		private ColosseumRewardDuelistCupViewController.ChangeFormWidget m_changeFormWidget;

		// Token: 0x0400B638 RID: 46648
		private readonly string k_ELabelchangeRewardsTabs;

		// Token: 0x0400B639 RID: 46649
		private readonly string k_ELabelShortcutButtonRarityBack;

		// Token: 0x0400B63A RID: 46650
		private readonly string k_ELabelShortcutButtonRarityNext;

		// Token: 0x0400B63B RID: 46651
		private readonly string k_ELabelAnalogDirectionItem;

		// Token: 0x0400B63C RID: 46652
		private ColosseumUtil.StatusDuelistCup status;

		// Token: 0x0400B63D RID: 46653
		private ColosseumUtil.PlayMode mode;

		// Token: 0x0400B63E RID: 46654
		private string endDate;

		// Token: 0x0400B63F RID: 46655
		private string endDateReward;

		// Token: 0x0400B640 RID: 46656
		private int id;

		// Token: 0x0400B641 RID: 46657
		private int currentTabIdx;

		// Token: 0x02001060 RID: 4192
		private class ChangeFormWidget : ElementWidgetBase
		{
			// Token: 0x06007DE9 RID: 32233 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public ChangeFormWidget(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x0400B642 RID: 46658
			public readonly DirectionalToggleGroupWidget rewardsToggleWidget;
		}

		// Token: 0x02001061 RID: 4193
		internal abstract class ModeBehaviour
		{
			// Token: 0x06007DEA RID: 32234 RVA: 0x00002739 File Offset: 0x00000939
			protected ModeBehaviour(ColosseumRewardDuelistCupViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, string endDateReward, ColosseumUtil.StatusDuelistCup status, bool isTabChange)
			{
			}

			// Token: 0x06007DEB RID: 32235
			internal abstract void CallAPI();

			// Token: 0x06007DEC RID: 32236
			internal abstract void UpdateView();

			// Token: 0x06007DED RID: 32237
			internal abstract void OnUpdateEntity(GameObject go, int dataIndex);

			// Token: 0x06007DEE RID: 32238
			internal abstract void InitializeScroll();

			// Token: 0x0400B643 RID: 46659
			protected readonly string BTN_LABEL;

			// Token: 0x0400B644 RID: 46660
			protected readonly string BTN_ICON_LABEL;

			// Token: 0x0400B645 RID: 46661
			protected readonly string IMG_LINE_LABEL;

			// Token: 0x0400B646 RID: 46662
			protected readonly string TXT_ORDER_LABEL;

			// Token: 0x0400B647 RID: 46663
			protected readonly string TXT_NAME_LABEL;

			// Token: 0x0400B648 RID: 46664
			protected readonly string TXT_TITLE_LABEL;

			// Token: 0x0400B649 RID: 46665
			protected readonly string TXT_DATE_LABEL;

			// Token: 0x0400B64A RID: 46666
			protected readonly string TXT_EXPLAIN_LABEL;

			// Token: 0x0400B64B RID: 46667
			protected readonly string IMG_RECEIVED_LABEL;

			// Token: 0x0400B64C RID: 46668
			protected readonly ColosseumRewardDuelistCupViewController vc;

			// Token: 0x0400B64D RID: 46669
			protected readonly InfinityScrollView isv;

			// Token: 0x0400B64E RID: 46670
			protected readonly ElementObjectManager eom;

			// Token: 0x0400B64F RID: 46671
			protected readonly int id;

			// Token: 0x0400B650 RID: 46672
			protected readonly string endDateReward;

			// Token: 0x0400B651 RID: 46673
			protected readonly ColosseumUtil.StatusDuelistCup status;

			// Token: 0x0400B652 RID: 46674
			protected readonly bool isTabChange;
		}

		// Token: 0x02001062 RID: 4194
		internal class DuelistCupBehaviour : ColosseumRewardDuelistCupViewController.ModeBehaviour
		{
			// Token: 0x06007DEF RID: 32239 RVA: 0x000F680D File Offset: 0x000F4A0D
			public DuelistCupBehaviour(ColosseumRewardDuelistCupViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, string endDateReward, ColosseumUtil.StatusDuelistCup status, bool isTabChange)
				: base(null, null, null, 0, null, ColosseumUtil.StatusDuelistCup.USER_STATUS_NO_ENTRY, false)
			{
			}

			// Token: 0x06007DF0 RID: 32240 RVA: 0x0000216D File Offset: 0x0000036D
			protected virtual void SetTabLabel(ElementObjectManager eom)
			{
			}

			// Token: 0x06007DF1 RID: 32241 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string GetIdsRewardsInfoSecond()
			{
				return null;
			}

			// Token: 0x06007DF2 RID: 32242 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06007DF3 RID: 32243 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitializeScroll()
			{
			}

			// Token: 0x06007DF4 RID: 32244 RVA: 0x0000216A File Offset: 0x0000036A
			private string orderTextConvert(ColosseumRewardDuelistCupViewController.DuelistCupBehaviour.Data data, bool isSingleRank = false, ColosseumRewardDuelistCupViewController.DuelistCupBehaviour.Data nextData = null)
			{
				return null;
			}

			// Token: 0x06007DF5 RID: 32245 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnUpdateEntity(GameObject go, int dataIndex)
			{
			}

			// Token: 0x06007DF6 RID: 32246 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateView()
			{
			}

			// Token: 0x06007DF7 RID: 32247 RVA: 0x0000216A File Offset: 0x0000036A
			private IReadOnlyList<ValueTuple<SelectionItem, int, int>> CustomCollectionSelectionItems(GameObject rewardEntity)
			{
				return null;
			}

			// Token: 0x0400B653 RID: 46675
			private ColosseumUtil.StatusDuelistCup statusDuelistCup;

			// Token: 0x0400B654 RID: 46676
			protected ColosseumUtil.PlayMode mode;

			// Token: 0x0400B655 RID: 46677
			private List<ColosseumRewardDuelistCupViewController.DuelistCupBehaviour.Data> dataList;

			// Token: 0x0400B656 RID: 46678
			private int rewardDlv;

			// Token: 0x0400B657 RID: 46679
			private int maxDLv;

			// Token: 0x0400B658 RID: 46680
			private bool isChanged;

			// Token: 0x02001063 RID: 4195
			private class Data
			{
				// Token: 0x06007DF8 RID: 32248 RVA: 0x00002739 File Offset: 0x00000939
				public Data(int indexNum)
				{
				}

				// Token: 0x0400B659 RID: 46681
				internal List<ColosseumRewardDuelistCupViewController.DuelistCupBehaviour.ItemDataDC> itemDatas;

				// Token: 0x0400B65A RID: 46682
				internal int indexNum;
			}

			// Token: 0x02001064 RID: 4196
			private class ItemData
			{
				// Token: 0x06007DF9 RID: 32249 RVA: 0x00002739 File Offset: 0x00000939
				public ItemData(int itemID, int quantity)
				{
				}

				// Token: 0x0400B65B RID: 46683
				internal int itemID;

				// Token: 0x0400B65C RID: 46684
				internal int quantity;
			}

			// Token: 0x02001065 RID: 4197
			private class ItemDataDC
			{
				// Token: 0x06007DFA RID: 32250 RVA: 0x00002739 File Offset: 0x00000939
				public ItemDataDC(int itemCategory, int itemId, int num, bool isPeriod, int needDlv, bool focus, bool received)
				{
				}

				// Token: 0x0400B65D RID: 46685
				public int itemCategory;

				// Token: 0x0400B65E RID: 46686
				public int itemId;

				// Token: 0x0400B65F RID: 46687
				public int num;

				// Token: 0x0400B660 RID: 46688
				public int needDlv;

				// Token: 0x0400B661 RID: 46689
				public bool isPeriod;

				// Token: 0x0400B662 RID: 46690
				public bool focus;

				// Token: 0x0400B663 RID: 46691
				public bool received;
			}
		}

		// Token: 0x02001066 RID: 4198
		internal class WCSBehaviour : ColosseumRewardDuelistCupViewController.DuelistCupBehaviour
		{
			// Token: 0x06007DFB RID: 32251 RVA: 0x000F681C File Offset: 0x000F4A1C
			public WCSBehaviour(ColosseumRewardDuelistCupViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, string endDateReward, ColosseumUtil.StatusDuelistCup status, bool isTabChange)
				: base(null, null, null, 0, null, ColosseumUtil.StatusDuelistCup.USER_STATUS_NO_ENTRY, false)
			{
			}

			// Token: 0x06007DFC RID: 32252 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void SetTabLabel(ElementObjectManager eom)
			{
			}

			// Token: 0x06007DFD RID: 32253 RVA: 0x0000216A File Offset: 0x0000036A
			protected override string GetIdsRewardsInfoSecond()
			{
				return null;
			}
		}
	}
}
