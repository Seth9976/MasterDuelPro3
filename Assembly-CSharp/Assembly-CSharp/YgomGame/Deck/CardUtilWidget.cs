using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02000FBF RID: 4031
	public abstract class CardUtilWidget : CardParameterWidget
	{
		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06007894 RID: 30868
		protected abstract Image m_AtkIcon { get; }

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06007895 RID: 30869
		protected abstract ExtendedTextMeshProUGUI m_AtkText { get; }

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06007896 RID: 30870
		protected abstract Image m_DefIcon { get; }

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06007897 RID: 30871
		protected abstract ExtendedTextMeshProUGUI m_DefText { get; }

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06007898 RID: 30872
		protected abstract RectTransform m_SpellTrapType { get; }

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06007899 RID: 30873
		protected abstract ExtendedTextMeshProUGUI m_SpellTrapTypeText { get; }

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x0600789A RID: 30874
		protected abstract ExtendedScrollRect m_TextArea { get; }

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x0600789B RID: 30875
		protected abstract ExtendedTextMeshProUGUI m_CardDesc { get; }

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x0600789C RID: 30876
		protected abstract ExtendedTextMeshProUGUI m_CardDescHeading { get; }

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x0600789D RID: 30877
		protected abstract Image m_NameAreaBG { get; }

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x0600789E RID: 30878
		protected abstract Image m_DescAreaBG { get; }

		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x0600789F RID: 30879
		protected abstract RubyTextGX m_CardName { get; }

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x060078A0 RID: 30880
		protected abstract SelectionButton m_CreateButton { get; }

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x060078A1 RID: 30881
		protected abstract SelectionButton m_DismantleButton { get; }

		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x060078A2 RID: 30882
		protected abstract SelectionButton m_AddCardButton { get; }

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x060078A3 RID: 30883
		protected abstract SelectionButton m_RemoveCardButton { get; }

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x060078A4 RID: 30884
		protected abstract SelectionButton m_BookmarkButton { get; }

		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x060078A5 RID: 30885
		protected abstract SelectionButton m_HowToGetButton { get; }

		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x060078A6 RID: 30886
		protected abstract SelectionButton m_RelatedCardButton { get; }

		// Token: 0x060078A7 RID: 30887 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setCardName()
		{
		}

		// Token: 0x060078A8 RID: 30888 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setAttack()
		{
		}

		// Token: 0x060078A9 RID: 30889 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setDefence()
		{
		}

		// Token: 0x060078AA RID: 30890 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setDescriptionHeading()
		{
		}

		// Token: 0x060078AB RID: 30891 RVA: 0x0000216D File Offset: 0x0000036D
		private void setCardText()
		{
		}

		// Token: 0x060078AC RID: 30892 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setCardNameBG()
		{
		}

		// Token: 0x060078AD RID: 30893 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setDescAreaBG()
		{
		}

		// Token: 0x060078AE RID: 30894 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setSpellTrapType()
		{
		}

		// Token: 0x060078AF RID: 30895 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickAddToMainButtonCallBack(UnityAction callback)
		{
		}

		// Token: 0x060078B0 RID: 30896 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickRemoveFromMainButtonCallBack(UnityAction callback)
		{
		}

		// Token: 0x060078B1 RID: 30897 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickBookmarkButtonCallBack(UnityAction callback)
		{
		}

		// Token: 0x060078B2 RID: 30898 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCraftCreateButtonCallBack(UnityAction callback)
		{
		}

		// Token: 0x060078B3 RID: 30899 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCraftDismantleButtonCallBack(UnityAction callback)
		{
		}

		// Token: 0x060078B4 RID: 30900 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickRelatedCardsButton(UnityAction callback)
		{
		}

		// Token: 0x060078B5 RID: 30901 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickLootSourceButton(UnityAction callback)
		{
		}

		// Token: 0x060078B6 RID: 30902 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickBackButton(UnityAction callback)
		{
		}

		// Token: 0x0400B064 RID: 45156
		protected Material m_TextBGMaterial;

		// Token: 0x0400B065 RID: 45157
		protected UnityAction m_OnClickCraftCreate;

		// Token: 0x0400B066 RID: 45158
		protected UnityAction m_OnClickCraftDismantle;

		// Token: 0x0400B067 RID: 45159
		protected UnityAction m_OnClickRelatedCards;

		// Token: 0x0400B068 RID: 45160
		protected UnityAction m_OnClickButtonAddCard;

		// Token: 0x0400B069 RID: 45161
		protected UnityAction m_OnClickButtonRemoveCard;

		// Token: 0x0400B06A RID: 45162
		protected UnityAction m_OnClickBackButton;

		// Token: 0x0400B06B RID: 45163
		protected UnityAction m_OnClickButtonBookmark;

		// Token: 0x0400B06C RID: 45164
		protected UnityAction m_OnClickLootSource;
	}
}
