using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.WCS
{
	// Token: 0x020007F6 RID: 2038
	public class WinPredictionRewardViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06003F61 RID: 16225 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003F62 RID: 16226 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003F63 RID: 16227 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x06003F64 RID: 16228 RVA: 0x0000216A File Offset: 0x0000036A
		private WinPredictionRewardViewController.ModeBehaviour GetModeBehaviour(string txtDate, bool isTabChange)
		{
			return null;
		}

		// Token: 0x06003F65 RID: 16229 RVA: 0x0000216D File Offset: 0x0000036D
		public override void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
		{
		}

		// Token: 0x0400384B RID: 14411
		private readonly string SCROLL_LABEL;

		// Token: 0x0400384C RID: 14412
		private WinPredictionRewardViewController.ModeBehaviour modeBehaviour;

		// Token: 0x0400384D RID: 14413
		private InfinityScrollView isv;

		// Token: 0x0400384E RID: 14414
		private List<SelectionItem> isvEdgeSelectSearchList;

		// Token: 0x0400384F RID: 14415
		private readonly string k_ELabelAnalogDirectionItem;

		// Token: 0x04003850 RID: 14416
		private string endDateReward;

		// Token: 0x04003851 RID: 14417
		private int id;

		// Token: 0x04003852 RID: 14418
		private const int NormalRewardType = 1;

		// Token: 0x04003853 RID: 14419
		private const int SpecialRewardType = 2;

		// Token: 0x020007F7 RID: 2039
		private class ChangeFormWidget : ElementWidgetBase
		{
			// Token: 0x06003F68 RID: 16232 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public ChangeFormWidget(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x04003854 RID: 14420
			public readonly DirectionalToggleGroupWidget rewardsToggleWidget;
		}

		// Token: 0x020007F8 RID: 2040
		internal abstract class ModeBehaviour
		{
			// Token: 0x06003F69 RID: 16233 RVA: 0x00002739 File Offset: 0x00000939
			protected ModeBehaviour(WinPredictionRewardViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, string endDateReward, bool isTabChange)
			{
			}

			// Token: 0x06003F6A RID: 16234
			internal abstract void CallAPI();

			// Token: 0x06003F6B RID: 16235
			internal abstract void UpdateView();

			// Token: 0x06003F6C RID: 16236
			internal abstract void OnUpdateEntity(GameObject go, int dataIndex);

			// Token: 0x06003F6D RID: 16237
			internal abstract void InitializeScroll();

			// Token: 0x04003855 RID: 14421
			protected readonly string BTN_LABEL;

			// Token: 0x04003856 RID: 14422
			protected readonly string BTN_ICON_LABEL;

			// Token: 0x04003857 RID: 14423
			protected readonly string IMG_LINE_LABEL;

			// Token: 0x04003858 RID: 14424
			protected readonly string TXT_ORDER_LABEL;

			// Token: 0x04003859 RID: 14425
			protected readonly string TXT_NAME_LABEL;

			// Token: 0x0400385A RID: 14426
			protected readonly string TXT_DATE_LABEL;

			// Token: 0x0400385B RID: 14427
			protected readonly string TXT_EXPLAIN_LABEL;

			// Token: 0x0400385C RID: 14428
			protected readonly string IMG_RECEIVED_LABEL;

			// Token: 0x0400385D RID: 14429
			protected readonly WinPredictionRewardViewController vc;

			// Token: 0x0400385E RID: 14430
			protected readonly InfinityScrollView isv;

			// Token: 0x0400385F RID: 14431
			protected readonly ElementObjectManager eom;

			// Token: 0x04003860 RID: 14432
			protected readonly int id;

			// Token: 0x04003861 RID: 14433
			protected readonly string endDateReward;

			// Token: 0x04003862 RID: 14434
			protected readonly bool isTabChange;
		}

		// Token: 0x020007F9 RID: 2041
		internal class WcsWinPredictionBehaviour : WinPredictionRewardViewController.ModeBehaviour
		{
			// Token: 0x06003F6E RID: 16238 RVA: 0x000F4696 File Offset: 0x000F2896
			public WcsWinPredictionBehaviour(WinPredictionRewardViewController vc, InfinityScrollView isv, ElementObjectManager eom, int id, string endDateReward, bool isTabChange)
				: base(null, null, null, 0, null, false)
			{
			}

			// Token: 0x06003F6F RID: 16239 RVA: 0x0000216A File Offset: 0x0000036A
			protected virtual string GetIdsRewardsInfoSecond()
			{
				return null;
			}

			// Token: 0x06003F70 RID: 16240 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void CallAPI()
			{
			}

			// Token: 0x06003F71 RID: 16241 RVA: 0x0000216A File Offset: 0x0000036A
			private IEnumerator wait()
			{
				return null;
			}

			// Token: 0x06003F72 RID: 16242 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void InitializeScroll()
			{
			}

			// Token: 0x06003F73 RID: 16243 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void OnUpdateEntity(GameObject go, int dataIndex)
			{
			}

			// Token: 0x06003F74 RID: 16244 RVA: 0x0000216D File Offset: 0x0000036D
			internal override void UpdateView()
			{
			}

			// Token: 0x06003F75 RID: 16245 RVA: 0x0000216D File Offset: 0x0000036D
			private void SetItemData(List<object> list, int index, bool isSpecial, WinPredictionRewardViewController.WcsWinPredictionBehaviour.Data data)
			{
			}

			// Token: 0x06003F76 RID: 16246 RVA: 0x0000216A File Offset: 0x0000036A
			private IReadOnlyList<ValueTuple<SelectionItem, int, int>> CustomCollectionSelectionItems(GameObject rewardEntity)
			{
				return null;
			}

			// Token: 0x04003863 RID: 14435
			private List<WinPredictionRewardViewController.WcsWinPredictionBehaviour.Data> dataList;

			// Token: 0x020007FA RID: 2042
			private class Data
			{
				// Token: 0x06003F77 RID: 16247 RVA: 0x00002739 File Offset: 0x00000939
				public Data(int indexNum)
				{
				}

				// Token: 0x06003F78 RID: 16248 RVA: 0x00002739 File Offset: 0x00000939
				public Data(string title)
				{
				}

				// Token: 0x04003864 RID: 14436
				internal List<WinPredictionRewardViewController.WcsWinPredictionBehaviour.ItemDataDC> itemDatas;

				// Token: 0x04003865 RID: 14437
				internal int indexNum;

				// Token: 0x04003866 RID: 14438
				internal string title;
			}

			// Token: 0x020007FB RID: 2043
			private class ItemData
			{
				// Token: 0x06003F79 RID: 16249 RVA: 0x00002739 File Offset: 0x00000939
				public ItemData(int itemID, int quantity)
				{
				}

				// Token: 0x04003867 RID: 14439
				internal int itemID;

				// Token: 0x04003868 RID: 14440
				internal int quantity;
			}

			// Token: 0x020007FC RID: 2044
			private class ItemDataDC
			{
				// Token: 0x06003F7A RID: 16250 RVA: 0x00002739 File Offset: 0x00000939
				public ItemDataDC(int itemCategory, int itemId, int num, bool isPeriod, int needDlv, bool focus, bool received, bool isSpecial = false)
				{
				}

				// Token: 0x04003869 RID: 14441
				public int itemCategory;

				// Token: 0x0400386A RID: 14442
				public int itemId;

				// Token: 0x0400386B RID: 14443
				public int num;

				// Token: 0x0400386C RID: 14444
				public int needDlv;

				// Token: 0x0400386D RID: 14445
				public bool isPeriod;

				// Token: 0x0400386E RID: 14446
				public bool focus;

				// Token: 0x0400386F RID: 14447
				public bool received;

				// Token: 0x04003870 RID: 14448
				public bool isSpecial;
			}
		}
	}
}
