using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YgomGame.Deck;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.Menu
{
	// Token: 0x02000AD6 RID: 2774
	public class PublicDeckSearchFilterTagSelectController : BaseMenuViewController
	{
		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x060050EB RID: 20715 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x060050EC RID: 20716 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060050EF RID: 20719 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void OnCreatedView()
		{
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeCategoryList()
		{
		}

		// Token: 0x060050F1 RID: 20721 RVA: 0x0000216D File Offset: 0x0000036D
		private void SortCategory()
		{
		}

		// Token: 0x060050F2 RID: 20722 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeInfinityScrollView()
		{
		}

		// Token: 0x060050F3 RID: 20723 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemInitialize(GameObject gob)
		{
		}

		// Token: 0x060050F4 RID: 20724 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnGsvStanby()
		{
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnItemSetData(GameObject gob, int dataindex)
		{
		}

		// Token: 0x060050F6 RID: 20726 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickButtonOK()
		{
		}

		// Token: 0x060050F7 RID: 20727 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickButtonCancel()
		{
		}

		// Token: 0x060050F8 RID: 20728 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetKeyWord(string keyword)
		{
		}

		// Token: 0x060050F9 RID: 20729 RVA: 0x0000216A File Offset: 0x0000036A
		private string StrConvHiraToKata(string str)
		{
			return null;
		}

		// Token: 0x060050FA RID: 20730 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckNOListMessage(string keyword)
		{
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x0000216A File Offset: 0x0000036A
		public string LangKey(string lang)
		{
			return null;
		}

		// Token: 0x04008F40 RID: 36672
		private readonly string k_ELabelTitleText;

		// Token: 0x04008F41 RID: 36673
		private readonly string k_ELabelInputField;

		// Token: 0x04008F42 RID: 36674
		private readonly string k_ELabelPlaceholder;

		// Token: 0x04008F43 RID: 36675
		private readonly string k_ELabelScrollView;

		// Token: 0x04008F44 RID: 36676
		private readonly string k_ELabelEmptyMessage;

		// Token: 0x04008F45 RID: 36677
		private readonly string k_ELabelEmptyMessageText;

		// Token: 0x04008F46 RID: 36678
		private readonly string k_ELabelInputButton;

		// Token: 0x04008F47 RID: 36679
		private readonly string k_ELabelButtonOK;

		// Token: 0x04008F48 RID: 36680
		private readonly string k_ELabelButtonCancel;

		// Token: 0x04008F49 RID: 36681
		private TextMeshProUGUI m_TitleText;

		// Token: 0x04008F4A RID: 36682
		private ElementObjectManager m_InputFieldEom;

		// Token: 0x04008F4B RID: 36683
		private InputFieldWidget m_InputFieldWidget;

		// Token: 0x04008F4C RID: 36684
		private ElementObjectManager m_ScrollViewEom;

		// Token: 0x04008F4D RID: 36685
		private InfinityScrollView m_ScrollView;

		// Token: 0x04008F4E RID: 36686
		private Transform m_EmptyMessage;

		// Token: 0x04008F4F RID: 36687
		private TextMeshProUGUI m_EmptyMessageText;

		// Token: 0x04008F50 RID: 36688
		private Transform m_TitleArea;

		// Token: 0x04008F51 RID: 36689
		private Transform m_TextFieldArea;

		// Token: 0x04008F52 RID: 36690
		private SelectionButton m_InputButton;

		// Token: 0x04008F53 RID: 36691
		private SelectionButton m_ButtonOK;

		// Token: 0x04008F54 RID: 36692
		private SelectionButton m_ButtonCancel;

		// Token: 0x04008F55 RID: 36693
		private PublicDeckSearchFilterTagSelectController.CType type;

		// Token: 0x04008F56 RID: 36694
		private string m_Language;

		// Token: 0x04008F57 RID: 36695
		private List<CategoryReference> m_Categories;

		// Token: 0x04008F58 RID: 36696
		private List<CategoryReference> m_SelectedCategories;

		// Token: 0x04008F59 RID: 36697
		private List<CategoryReference> m_tmpSelected;

		// Token: 0x02000AD7 RID: 2775
		private enum CType
		{
			// Token: 0x04008F5B RID: 36699
			Category,
			// Token: 0x04008F5C RID: 36700
			Tag
		}
	}
}
