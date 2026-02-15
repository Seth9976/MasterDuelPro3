using System;
using System.Collections.Generic;
using UnityEngine;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;

namespace YgomGame.CardPack.OpenResult
{
	// Token: 0x020010B9 RID: 4281
	public class ObtainedCardsWidget : ElementWidgetBase
	{
		// Token: 0x17001003 RID: 4099
		// (get) Token: 0x06007F30 RID: 32560 RVA: 0x0000216A File Offset: 0x0000036A
		public InfinityScrollView scrollView
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007F31 RID: 32561 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ObtainedCardsWidget(ElementObjectManager eom, ElementObjectManager showOwnedNumEom)
			: base(null)
		{
		}

		// Token: 0x06007F32 RID: 32562 RVA: 0x0000216D File Offset: 0x0000036D
		public void ActivateScroll(Action onComplete)
		{
		}

		// Token: 0x06007F33 RID: 32563 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitLayout(List<string> headerLabels, bool dispSendGift)
		{
		}

		// Token: 0x06007F34 RID: 32564 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedEntity(GameObject gob)
		{
		}

		// Token: 0x06007F35 RID: 32565 RVA: 0x0000216A File Offset: 0x0000036A
		private IReadOnlyList<ValueTuple<SelectionItem, int, int>> OnCollectEntitySelectionItems(GameObject entity)
		{
			return null;
		}

		// Token: 0x06007F36 RID: 32566 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnUpdateEntity(GameObject gob, int dataindex)
		{
		}

		// Token: 0x06007F37 RID: 32567 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsSelectableDataIndex(int dataIndex)
		{
			return false;
		}

		// Token: 0x06007F38 RID: 32568 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnCustomDirectionTransition(SelectionItem selectionItem, PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x06007F39 RID: 32569 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreatedObtainWidget(CardWidget cardWidget)
		{
		}

		// Token: 0x06007F3A RID: 32570 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetObtainCardWidget(CardWidget cardWidget, int dataindex, bool isDefaultItem)
		{
		}

		// Token: 0x06007F3B RID: 32571 RVA: 0x0000216D File Offset: 0x0000036D
		private void CloseSelectedPulldown()
		{
		}

		// Token: 0x06007F3C RID: 32572 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenSelectedPulldown()
		{
		}

		// Token: 0x06007F3D RID: 32573 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickCard(CardWidget cardWidget)
		{
		}

		// Token: 0x06007F3E RID: 32574 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickPulldown(CardWidget cardWidget)
		{
		}

		// Token: 0x06007F3F RID: 32575 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelectedCard(CardWidget cardWidget)
		{
		}

		// Token: 0x06007F40 RID: 32576 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelectedPulldown(CardWidget cardWidget)
		{
		}

		// Token: 0x06007F41 RID: 32577 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeselectedCard(CardWidget cardWidget)
		{
		}

		// Token: 0x06007F42 RID: 32578 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDeselectedPulldown(CardWidget cardWidget)
		{
		}

		// Token: 0x06007F43 RID: 32579 RVA: 0x0000216D File Offset: 0x0000036D
		public void JumpToDirection(PadInputDirection direction)
		{
		}

		// Token: 0x0400B7C2 RID: 47042
		private const string k_ELabelScrollView = "ScrollView";

		// Token: 0x0400B7C3 RID: 47043
		private const string k_ELabel_Entity_PackCardTemplate = "ScrollView/PackCardGroupTemplate/PackCardTemplate";

		// Token: 0x0400B7C4 RID: 47044
		private const string k_OLabel_Template_Default = "Default";

		// Token: 0x0400B7C5 RID: 47045
		private const string k_OLabel_Template_Expand = "Expand";

		// Token: 0x0400B7C6 RID: 47046
		private readonly ToggleWidget m_ShowOwnedNumToggle;

		// Token: 0x0400B7C7 RID: 47047
		private readonly InfinityScrollView m_ScrollView;

		// Token: 0x0400B7C8 RID: 47048
		private readonly Dictionary<GameObject, CardWidget[]> m_CardWidgetGroupMap;

		// Token: 0x0400B7C9 RID: 47049
		private readonly List<int> m_SecretShopIdsCache;

		// Token: 0x0400B7CA RID: 47050
		private List<object> m_ListDatas;

		// Token: 0x0400B7CB RID: 47051
		private List<object> m_DrawListDatas;

		// Token: 0x0400B7CC RID: 47052
		private List<int> m_TemplateIdxs;

		// Token: 0x0400B7CD RID: 47053
		public readonly List<object> workDrawDatas;

		// Token: 0x0400B7CE RID: 47054
		public List<object> workExtraGroupDatas;

		// Token: 0x0400B7CF RID: 47055
		public bool workIsSendGift;

		// Token: 0x0400B7D0 RID: 47056
		public bool isExpand;

		// Token: 0x0400B7D1 RID: 47057
		public bool pulldownSecretsEnable;

		// Token: 0x0400B7D2 RID: 47058
		private ElementObjectManager m_PackCardTemplatePref;

		// Token: 0x0400B7D3 RID: 47059
		private CardWidget m_SelectedPulldownOwner;

		// Token: 0x0400B7D4 RID: 47060
		private ObtainedCardsWidget.AutoSelectHelper m_AutoSelectHelper;

		// Token: 0x0400B7D5 RID: 47061
		private Selector[] m_AutoSelectIgnoreSelectors;

		// Token: 0x0400B7D6 RID: 47062
		private List<object> m_CardDetailMrksCache;

		// Token: 0x0400B7D7 RID: 47063
		private List<object> m_CardDetailPremiumsCache;

		// Token: 0x020010BA RID: 4282
		private class AutoSelectHelper
		{
			// Token: 0x06007F44 RID: 32580 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool TrySelect(SelectionItem fromItem, Vector2 dir, Selector[] ignoreSelectors = null)
			{
				return false;
			}

			// Token: 0x0400B7D8 RID: 47064
			private List<SelectionItem> m_SortTargetSelections;

			// Token: 0x0400B7D9 RID: 47065
			private Dictionary<SelectionItem, float> m_SortAmounts;
		}
	}
}
