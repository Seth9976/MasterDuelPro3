using System;
using System.Collections.Generic;
using YgomSystem.UI;

namespace YgomGame.Shop
{
	// Token: 0x02000973 RID: 2419
	public class ShowcaseWidgetsController
	{
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool inProgress
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x00002739 File Offset: 0x00000939
		public ShowcaseWidgetsController(ViewController owner, ShopSettings shopSettings, ShopViewController.ShowcaseData showcaseData, MainTabListWidget mainTabList, SubTabListWidget subTabList, ProductListWidget productList)
		{
		}

		// Token: 0x060046D7 RID: 18135 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsAutoAcordionShrink(int categoryId, int subCategoryId)
		{
			return false;
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetAutoAcordionShrink(int categoryId, int subCategoryId, bool value)
		{
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x0000216D File Offset: 0x0000036D
		public void ApplyAllImmediate()
		{
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool MoveByProductListScroll(int categoryId, int subCategoryId, int sectionId, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool MoveMainCategoryByTab(int categoryIdx, ShowcaseWidgetsController.SelectBlockOperate selectBlock = ShowcaseWidgetsController.SelectBlockOperate.HeadProduct, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool MoveSubCategoryByTab(int subCategoryIdx, int sectionIdx, ShowcaseWidgetsController.AcordionOperate acordionOperate = ShowcaseWidgetsController.AcordionOperate.None, ShowcaseWidgetsController.SelectBlockOperate selectBlock = ShowcaseWidgetsController.SelectBlockOperate.HeadProduct, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool InnerChangeCategories(int categoryId, int subCategoryId, int sectionId, ShowcaseWidgetsController.AcordionOperate acordionOperate, ShowcaseWidgetsController.SelectBlockOperate selectBlockOperate, bool focusProductList, bool immediate, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x0000216D File Offset: 0x0000036D
		public void OperateTargetAcordion(ValueTuple<int, int, int> targetIds, ShowcaseWidgetsController.AcordionOperate acordionOperate)
		{
		}

		// Token: 0x060046DF RID: 18143 RVA: 0x0000216D File Offset: 0x0000036D
		private void FocusProductListImmediate()
		{
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateBgView()
		{
		}

		// Token: 0x060046E1 RID: 18145 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TrySelectBlock(ShowcaseWidgetsController.SelectBlockOperate selectBlockOperate, bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x060046E2 RID: 18146 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TrySelectHeadProduct(bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x060046E3 RID: 18147 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TrySelectCurrentSubTab(bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x060046E4 RID: 18148 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool TrySelectMainTab(bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x0400850B RID: 34059
		private readonly ViewController m_Owner;

		// Token: 0x0400850C RID: 34060
		private readonly ShopSettings m_ShopSettings;

		// Token: 0x0400850D RID: 34061
		private readonly ShopViewController.ShowcaseData m_ShowcaseData;

		// Token: 0x0400850E RID: 34062
		private readonly MainTabListWidget m_MainTabList;

		// Token: 0x0400850F RID: 34063
		private readonly SubTabListWidget m_SubTabList;

		// Token: 0x04008510 RID: 34064
		private readonly ProductListWidget m_ProductList;

		// Token: 0x04008511 RID: 34065
		private Dictionary<int, Dictionary<int, bool>> m_AutoAcordionShrinkMap;

		// Token: 0x04008512 RID: 34066
		private bool m_InProgress;

		// Token: 0x02000974 RID: 2420
		public enum SelectBlockOperate
		{
			// Token: 0x04008514 RID: 34068
			None,
			// Token: 0x04008515 RID: 34069
			CurrentBlock,
			// Token: 0x04008516 RID: 34070
			CurrentMainTab,
			// Token: 0x04008517 RID: 34071
			CurrentSubTabList,
			// Token: 0x04008518 RID: 34072
			HeadProduct
		}

		// Token: 0x02000975 RID: 2421
		public enum AcordionOperate
		{
			// Token: 0x0400851A RID: 34074
			None,
			// Token: 0x0400851B RID: 34075
			Auto,
			// Token: 0x0400851C RID: 34076
			Switch,
			// Token: 0x0400851D RID: 34077
			Expand,
			// Token: 0x0400851E RID: 34078
			Shrink
		}
	}
}
