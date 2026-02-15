using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.DuelPass
{
	// Token: 0x02000C4B RID: 3147
	public class DuelPassRewardListViewController : BaseMenuViewController, IBackButtonSupported, IHeaderBorderSupported
	{
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x060059F7 RID: 23031 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(Dictionary<string, object> shopDuelPassDic)
		{
		}

		// Token: 0x060059F9 RID: 23033 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddTotalItemContext(DuelPassRewardListViewController.ItemContext itemContext)
		{
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x0000216D File Offset: 0x0000036D
		private void CreateTotalItemContextGrid()
		{
		}

		// Token: 0x060059FE RID: 23038 RVA: 0x0000216A File Offset: 0x0000036A
		private IReadOnlyList<ValueTuple<SelectionItem, int, int>> OnItemCollectSelectionItems(GameObject go)
		{
			return null;
		}

		// Token: 0x060059FF RID: 23039 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnUpdateEntity(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetItemWidget(ElementObjectManager itemWidget, DuelPassRewardListViewController.ItemContext itemContext)
		{
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsSelectableDataIndex(int dataindex)
		{
			return false;
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickItem(DuelPassRewardListViewController.ItemContext itemContext)
		{
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
		{
		}

		// Token: 0x04009570 RID: 38256
		private readonly string k_ELabelScrollView;

		// Token: 0x04009571 RID: 38257
		private readonly string k_ELabelThumbHolder;

		// Token: 0x04009572 RID: 38258
		private readonly string k_ELabelGradeText;

		// Token: 0x04009573 RID: 38259
		private readonly string k_ELabelNumText;

		// Token: 0x04009574 RID: 38260
		private readonly string k_ELabelItemRow;

		// Token: 0x04009575 RID: 38261
		private readonly string k_ELabelTotalItemRow;

		// Token: 0x04009576 RID: 38262
		private readonly string k_ELabelText;

		// Token: 0x04009577 RID: 38263
		private readonly string k_ELabelItemTemplate;

		// Token: 0x04009578 RID: 38264
		private readonly string k_ELabelTotalItemTemplate;

		// Token: 0x04009579 RID: 38265
		private readonly string k_ELabelAnalogDirectionItem;

		// Token: 0x0400957A RID: 38266
		private readonly List<string> m_cloneTotalItemNameList;

		// Token: 0x0400957B RID: 38267
		private readonly List<string> m_cloneItemNameList;

		// Token: 0x0400957C RID: 38268
		private readonly string k_grade;

		// Token: 0x0400957D RID: 38269
		private readonly string k_isPeriod;

		// Token: 0x0400957E RID: 38270
		private readonly string k_itemCategory;

		// Token: 0x0400957F RID: 38271
		private readonly string k_itemId;

		// Token: 0x04009580 RID: 38272
		private readonly string k_itemCount;

		// Token: 0x04009581 RID: 38273
		private readonly string k_isRecommend;

		// Token: 0x04009582 RID: 38274
		private const int k_TextTNo = 0;

		// Token: 0x04009583 RID: 38275
		private const int k_TotalItemRowTNo = 1;

		// Token: 0x04009584 RID: 38276
		private const int k_ItemRowTNo = 2;

		// Token: 0x04009585 RID: 38277
		private static string k_ArgDuelPassDic;

		// Token: 0x04009586 RID: 38278
		[SerializeField]
		private readonly int MAX_COL_CONSOLE;

		// Token: 0x04009587 RID: 38279
		[SerializeField]
		private readonly int MAX_COL_MOBILE;

		// Token: 0x04009588 RID: 38280
		private int m_maxCol;

		// Token: 0x04009589 RID: 38281
		private int m_maxTotalItemRow;

		// Token: 0x0400958A RID: 38282
		private int m_maxItemRow;

		// Token: 0x0400958B RID: 38283
		private readonly int GRADE_TOTALITEM;

		// Token: 0x0400958C RID: 38284
		private int m_itemRowStartNum;

		// Token: 0x0400958D RID: 38285
		private int m_totalItemRowStartNum;

		// Token: 0x0400958E RID: 38286
		private List<DuelPassRewardListViewController.ItemContext> m_totalItemContexts;

		// Token: 0x0400958F RID: 38287
		private List<int> m_templates;

		// Token: 0x04009590 RID: 38288
		private List<List<DuelPassRewardListViewController.ItemContext>> m_totalItemContextGrid;

		// Token: 0x04009591 RID: 38289
		private List<List<DuelPassRewardListViewController.ItemContext>> m_itemContextGrid;

		// Token: 0x04009592 RID: 38290
		private GameObject m_itemTemplateGO;

		// Token: 0x04009593 RID: 38291
		private GameObject m_totalItemTemplateGO;

		// Token: 0x04009594 RID: 38292
		private InfinityScrollView m_scrollView;

		// Token: 0x02000C4C RID: 3148
		private class ItemContext
		{
			// Token: 0x06005A05 RID: 23045 RVA: 0x00002739 File Offset: 0x00000939
			public ItemContext(int grade, bool isPeriod, int itemCategory, int itemId, int itemCount, bool isRecommend)
			{
			}

			// Token: 0x06005A06 RID: 23046 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsSameItem(DuelPassRewardListViewController.ItemContext itemContext)
			{
				return false;
			}

			// Token: 0x04009595 RID: 38293
			public int grade;

			// Token: 0x04009596 RID: 38294
			public bool isPeriod;

			// Token: 0x04009597 RID: 38295
			public int itemCategory;

			// Token: 0x04009598 RID: 38296
			public int itemId;

			// Token: 0x04009599 RID: 38297
			public int itemCount;

			// Token: 0x0400959A RID: 38298
			public bool isRecommend;
		}
	}
}
