using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Deck;
using YgomSystem.UI;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Menu
{
	// Token: 0x02000AD5 RID: 2773
	public class PublicDeckSearchFilterController : BaseMenuViewController, IDynamicChangeDispHeaderSupported
	{
		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x060050D8 RID: 20696 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenCategorySelectUI()
		{
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenTagSelectUI()
		{
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeInfinityScrollView()
		{
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x060050E1 RID: 20705 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickCategoryButton(int dataindex, CategoryReference categoryRef, SelectionButton button)
		{
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize2(GameObject gob)
		{
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby2()
		{
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData2(GameObject gob, int dataindex)
		{
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickTagButton(int dataindex, CategoryReference tagRef, SelectionButton button)
		{
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnResult(ViewController from, object value)
		{
			return false;
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickButtonOK()
		{
		}

		// Token: 0x04008F32 RID: 36658
		private readonly string k_ELabelCategoryScrollView;

		// Token: 0x04008F33 RID: 36659
		private readonly string k_ELabelAddCategoryButton;

		// Token: 0x04008F34 RID: 36660
		private readonly string k_ELabelTagScrollView;

		// Token: 0x04008F35 RID: 36661
		private readonly string k_ELabelAddTagButton;

		// Token: 0x04008F36 RID: 36662
		private readonly string k_ELabelCancelButton;

		// Token: 0x04008F37 RID: 36663
		private readonly string k_ELabelSearchButton;

		// Token: 0x04008F38 RID: 36664
		private InfinityScrollView m_CategoryScrollView;

		// Token: 0x04008F39 RID: 36665
		private SelectionButton m_AddCategoryButton;

		// Token: 0x04008F3A RID: 36666
		private InfinityScrollView m_TagScrollView;

		// Token: 0x04008F3B RID: 36667
		private SelectionButton m_AddTagButton;

		// Token: 0x04008F3C RID: 36668
		private SelectionButton m_CancelButton;

		// Token: 0x04008F3D RID: 36669
		private SelectionButton m_SearchButton;

		// Token: 0x04008F3E RID: 36670
		private List<CategoryReference> m_SelectedCategories;

		// Token: 0x04008F3F RID: 36671
		private List<CategoryReference> m_SelectedTags;
	}
}
