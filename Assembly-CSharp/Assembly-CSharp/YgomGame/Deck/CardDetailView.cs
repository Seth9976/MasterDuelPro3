using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomGame.Card;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02000FB5 RID: 4021
	public class CardDetailView : MonoBehaviour
	{
		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x060077A0 RID: 30624 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060077A1 RID: 30625 RVA: 0x0000216D File Offset: 0x0000036D
		protected int m_CardID
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x060077A2 RID: 30626 RVA: 0x000F661C File Offset: 0x000F481C
		public CardBaseData card
		{
			get
			{
				return default(CardBaseData);
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x060077A3 RID: 30627 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060077A4 RID: 30628 RVA: 0x0000216D File Offset: 0x0000036D
		protected CardCollectionInfo.Premium m_Premium
		{
			[CompilerGenerated]
			get
			{
				return (CardCollectionInfo.Premium)0;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x060077A5 RID: 30629 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060077A6 RID: 30630 RVA: 0x0000216D File Offset: 0x0000036D
		protected bool m_IsRental
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x060077A7 RID: 30631 RVA: 0x0000216A File Offset: 0x0000036A
		private static Content m_cci
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x060077A8 RID: 30632 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_AttrIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x060077A9 RID: 30633 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_NameAreaBG
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x060077AA RID: 30634 RVA: 0x0000216A File Offset: 0x0000036A
		protected RubyTextGX m_CardName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x060077AB RID: 30635 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_NameArea
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x060077AC RID: 30636 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton m_CardButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x060077AD RID: 30637 RVA: 0x0000216A File Offset: 0x0000036A
		protected RawImage m_CardImage
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x060077AE RID: 30638 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_RarityIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x060077AF RID: 30639 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_RegulationIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x060077B0 RID: 30640 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_CardTotalText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x060077B1 RID: 30641 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_CardNumRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x060077B2 RID: 30642 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_TunerIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x060077B3 RID: 30643 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_TypeIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x060077B4 RID: 30644 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_PendScaleIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x060077B5 RID: 30645 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_LvlIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x060077B6 RID: 30646 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_RankIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x060077B7 RID: 30647 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_LinkIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x060077B8 RID: 30648 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_AtkIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x060077B9 RID: 30649 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_DefIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x060077BA RID: 30650 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_PendScaleText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x060077BB RID: 30651 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_LvlText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x060077BC RID: 30652 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_RankText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x060077BD RID: 30653 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_LinkText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x060077BE RID: 30654 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_AtkText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x060077BF RID: 30655 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_DefText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x060077C0 RID: 30656 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_SpellTrapType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x060077C1 RID: 30657 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_SpellTrapTypeIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x060077C2 RID: 30658 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_SpellTrapTypeText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x060077C3 RID: 30659 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_DismantleableRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x060077C4 RID: 30660 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_DismantleableValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x060077C5 RID: 30661 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton m_AddCardButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x060077C6 RID: 30662 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton m_RemoveCardButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x060077C7 RID: 30663 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton m_BookmarkButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x060077C8 RID: 30664 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton m_HowToGetButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x060077C9 RID: 30665 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton m_RelatedCardButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x060077CA RID: 30666 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton m_CreateButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x060077CB RID: 30667 RVA: 0x0000216A File Offset: 0x0000036A
		protected SelectionButton m_DismantleButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x060077CC RID: 30668 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_MenuGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x060077CD RID: 30669 RVA: 0x0000216A File Offset: 0x0000036A
		protected RectTransform m_CraftGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x060077CE RID: 30670 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_DescText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x060077CF RID: 30671 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedTextMeshProUGUI m_DescTitleText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x060077D0 RID: 30672 RVA: 0x0000216A File Offset: 0x0000036A
		protected Image m_DescAreaBG
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x060077D1 RID: 30673 RVA: 0x0000216A File Offset: 0x0000036A
		protected ExtendedScrollRect m_TextAreaScroll
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x060077D2 RID: 30674 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060077D3 RID: 30675 RVA: 0x0000216D File Offset: 0x0000036D
		private ExtendedScrollRect m_TextArea
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x060077D4 RID: 30676 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060077D5 RID: 30677 RVA: 0x0000216D File Offset: 0x0000036D
		private ExtendedTextMeshProUGUI m_CardDesc
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x060077D6 RID: 30678 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060077D7 RID: 30679 RVA: 0x0000216D File Offset: 0x0000036D
		private ExtendedTextMeshProUGUI m_CardDescHeading
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x060077D8 RID: 30680 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060077D9 RID: 30681 RVA: 0x0000216D File Offset: 0x0000036D
		private Image m_DescAreaBG_
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x060077DA RID: 30682 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveButtonCraftCreate(bool active)
		{
		}

		// Token: 0x060077DB RID: 30683 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveButtonCraftDismantle(bool active)
		{
		}

		// Token: 0x060077DC RID: 30684 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setCraftPoint(bool isRental = false)
		{
		}

		// Token: 0x060077DD RID: 30685 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateCraftPoint()
		{
		}

		// Token: 0x060077DE RID: 30686 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetPrems(int cardId, CardCollectionInfo.Premium prem, bool isRental)
		{
		}

		// Token: 0x060077DF RID: 30687 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x060077E0 RID: 30688 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeElements()
		{
		}

		// Token: 0x060077E1 RID: 30689 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060077E2 RID: 30690 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060077E3 RID: 30691 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setCardImage()
		{
		}

		// Token: 0x060077E4 RID: 30692 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setRentalCardImage(bool isRental, bool dispDismantleableText = false)
		{
		}

		// Token: 0x060077E5 RID: 30693 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setCardName()
		{
		}

		// Token: 0x060077E6 RID: 30694 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setTextBGMat()
		{
		}

		// Token: 0x060077E7 RID: 30695 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setCardText()
		{
		}

		// Token: 0x060077E8 RID: 30696 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setAttribute()
		{
		}

		// Token: 0x060077E9 RID: 30697 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setAttack()
		{
		}

		// Token: 0x060077EA RID: 30698 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setDefence()
		{
		}

		// Token: 0x060077EB RID: 30699 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setTuner()
		{
		}

		// Token: 0x060077EC RID: 30700 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setPendulumScale()
		{
		}

		// Token: 0x060077ED RID: 30701 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setLevel()
		{
		}

		// Token: 0x060077EE RID: 30702 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setRank()
		{
		}

		// Token: 0x060077EF RID: 30703 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setLinkRating()
		{
		}

		// Token: 0x060077F0 RID: 30704 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setType()
		{
		}

		// Token: 0x060077F1 RID: 30705 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setSpellTrapType()
		{
		}

		// Token: 0x060077F2 RID: 30706 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setRarity()
		{
		}

		// Token: 0x060077F3 RID: 30707 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setDescriptionTitle()
		{
		}

		// Token: 0x060077F4 RID: 30708 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInventory(int rentalID = 0, bool isRental = false)
		{
		}

		// Token: 0x060077F5 RID: 30709 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setDismantleableValue()
		{
		}

		// Token: 0x060077F6 RID: 30710 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setRegulationIcon(int id)
		{
		}

		// Token: 0x060077F7 RID: 30711 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveButtons(bool b)
		{
		}

		// Token: 0x060077F8 RID: 30712 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBookmark(bool b)
		{
		}

		// Token: 0x060077F9 RID: 30713 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBlank(bool b)
		{
		}

		// Token: 0x060077FA RID: 30714 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void SetCard(CardBaseData data, int rent = 0, int reg = -1, bool bookmark = false, bool dispDismantleableText = false)
		{
		}

		// Token: 0x060077FB RID: 30715 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickBookmarkCallBack(UnityAction callback)
		{
		}

		// Token: 0x060077FC RID: 30716 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCreateCallBack(UnityAction callback)
		{
		}

		// Token: 0x060077FD RID: 30717 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickDismantleCallBack(UnityAction callback)
		{
		}

		// Token: 0x060077FE RID: 30718 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickRelatedCardsCallBack(UnityAction callback)
		{
		}

		// Token: 0x060077FF RID: 30719 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCheckSourceCallBack(UnityAction callback)
		{
		}

		// Token: 0x06007800 RID: 30720 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickAddCardCallBack(UnityAction callback)
		{
		}

		// Token: 0x06007801 RID: 30721 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveAddCard(bool enabled)
		{
		}

		// Token: 0x06007802 RID: 30722 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickRemoveCardCallBack(UnityAction callback)
		{
		}

		// Token: 0x06007803 RID: 30723 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveRemove(bool enabled)
		{
		}

		// Token: 0x06007804 RID: 30724 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCardImage(UnityAction callback)
		{
		}

		// Token: 0x06007805 RID: 30725 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardButtonShortcutKey(SelectorManager.KeyType keyMain, SelectorManager.KeyType keySub)
		{
		}

		// Token: 0x06007806 RID: 30726 RVA: 0x0000216A File Offset: 0x0000036A
		public RectTransform GetCardImageRectTransform()
		{
			return null;
		}

		// Token: 0x06007807 RID: 30727 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDimsmantleMode(bool b)
		{
		}

		// Token: 0x06007808 RID: 30728 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveSubMenuButton(bool activeRelatedCard, bool activeSourceButton)
		{
		}

		// Token: 0x06007809 RID: 30729 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveCardMenu(bool b)
		{
		}

		// Token: 0x0400B01E RID: 45086
		private ElementObjectManager m_Eom;

		// Token: 0x0400B01F RID: 45087
		private ElementObjectManager m_eom;

		// Token: 0x0400B020 RID: 45088
		private bool isIni;

		// Token: 0x0400B021 RID: 45089
		protected int m_Rarity;

		// Token: 0x0400B022 RID: 45090
		protected CardIconSprites m_CardIconSprites;

		// Token: 0x0400B023 RID: 45091
		private CardDetailView.TitleArea m_TitleArea;

		// Token: 0x0400B024 RID: 45092
		protected CardDetailView.CardArea m_CardArea;

		// Token: 0x0400B025 RID: 45093
		private CardDetailView.ParameterArea m_ParameterArea;

		// Token: 0x0400B026 RID: 45094
		private CardDetailView.MenuArea m_MenuArea;

		// Token: 0x0400B027 RID: 45095
		private const string LABEL_SB_WINDOW = "Window";

		// Token: 0x0400B028 RID: 45096
		protected const string LABEL_Tween_AutoScroll = "AutoScroll";

		// Token: 0x0400B029 RID: 45097
		protected Material m_TextBGMaterial;

		// Token: 0x0400B02A RID: 45098
		protected ContentSizeFitter m_DescTextSizeFitter;

		// Token: 0x0400B02B RID: 45099
		protected SelectionButtonUntouchable m_Window;

		// Token: 0x0400B02C RID: 45100
		private UnityAction m_OnClickCraftCreate;

		// Token: 0x0400B02D RID: 45101
		private UnityAction m_OnClickCraftDismantle;

		// Token: 0x0400B02E RID: 45102
		private UnityAction m_OnClickRelatedCards;

		// Token: 0x0400B02F RID: 45103
		private UnityAction m_OnClickButtonBookmark;

		// Token: 0x0400B030 RID: 45104
		private UnityAction m_OnClickCheckSource;

		// Token: 0x0400B031 RID: 45105
		private UnityAction m_OnClickAddCard;

		// Token: 0x0400B032 RID: 45106
		private UnityAction m_OnClickRemoveCard;

		// Token: 0x0400B033 RID: 45107
		private UnityAction m_OnClickCardButton;

		// Token: 0x0400B034 RID: 45108
		public ToggleWidget bookmarkToggle;

		// Token: 0x02000FB6 RID: 4022
		public abstract class ElementWidget : MonoBehaviour
		{
			// Token: 0x17000EFF RID: 3839
			// (get) Token: 0x0600780B RID: 30731 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isIni
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600780C RID: 30732 RVA: 0x0000216D File Offset: 0x0000036D
			public void Initialize()
			{
			}

			// Token: 0x0600780D RID: 30733 RVA: 0x0000216D File Offset: 0x0000036D
			private void Awake()
			{
			}

			// Token: 0x0600780E RID: 30734
			protected abstract void InitializeElements();

			// Token: 0x0400B035 RID: 45109
			protected ElementObjectManager m_Eom;

			// Token: 0x0400B036 RID: 45110
			protected bool isInitialized;
		}

		// Token: 0x02000FB7 RID: 4023
		private class TitleArea : CardDetailView.ElementWidget
		{
			// Token: 0x17000F00 RID: 3840
			// (get) Token: 0x06007810 RID: 30736 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007811 RID: 30737 RVA: 0x0000216D File Offset: 0x0000036D
			public RubyTextGX m_CardName
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F01 RID: 3841
			// (get) Token: 0x06007812 RID: 30738 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007813 RID: 30739 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_NameArea
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F02 RID: 3842
			// (get) Token: 0x06007814 RID: 30740 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007815 RID: 30741 RVA: 0x0000216D File Offset: 0x0000036D
			public Image m_NameAreaBG
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F03 RID: 3843
			// (get) Token: 0x06007816 RID: 30742 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007817 RID: 30743 RVA: 0x0000216D File Offset: 0x0000036D
			public Image m_AttrIcon
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x06007818 RID: 30744 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}
		}

		// Token: 0x02000FB8 RID: 4024
		public class CardArea : CardDetailView.ElementWidget
		{
			// Token: 0x17000F04 RID: 3844
			// (get) Token: 0x0600781A RID: 30746 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600781B RID: 30747 RVA: 0x0000216D File Offset: 0x0000036D
			public RawImage m_CardImage
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F05 RID: 3845
			// (get) Token: 0x0600781C RID: 30748 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600781D RID: 30749 RVA: 0x0000216D File Offset: 0x0000036D
			public Image m_LimitIcon
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F06 RID: 3846
			// (get) Token: 0x0600781E RID: 30750 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600781F RID: 30751 RVA: 0x0000216D File Offset: 0x0000036D
			public Image m_RarityIcon
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F07 RID: 3847
			// (get) Token: 0x06007820 RID: 30752 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007821 RID: 30753 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_CardButton
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F08 RID: 3848
			// (get) Token: 0x06007822 RID: 30754 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007823 RID: 30755 RVA: 0x0000216D File Offset: 0x0000036D
			public Image m_RentalImage
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F09 RID: 3849
			// (get) Token: 0x06007824 RID: 30756 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007825 RID: 30757 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedTextMeshProUGUI m_CardTotalText
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F0A RID: 3850
			// (get) Token: 0x06007826 RID: 30758 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007827 RID: 30759 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedTextMeshProUGUI m_NonPrizeCardTotalText
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F0B RID: 3851
			// (get) Token: 0x06007828 RID: 30760 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007829 RID: 30761 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_CardNumRoot
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x0600782A RID: 30762 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetCard(int cardId, CardCollectionInfo.Premium premium)
			{
			}

			// Token: 0x0600782B RID: 30763 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x0600782C RID: 30764 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeCardImage()
			{
			}

			// Token: 0x0600782D RID: 30765 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeCardNumText()
			{
			}

			// Token: 0x0400B037 RID: 45111
			private int m_CardID;

			// Token: 0x0400B038 RID: 45112
			private CardCollectionInfo.Premium m_Premium;
		}

		// Token: 0x02000FB9 RID: 4025
		private class ParameterArea : CardDetailView.ElementWidget
		{
			// Token: 0x17000F0C RID: 3852
			// (get) Token: 0x0600782F RID: 30767 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007830 RID: 30768 RVA: 0x0000216D File Offset: 0x0000036D
			public CardDetailView.ParameterArea.ParamIcon m_TunerIcon
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F0D RID: 3853
			// (get) Token: 0x06007831 RID: 30769 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007832 RID: 30770 RVA: 0x0000216D File Offset: 0x0000036D
			public CardDetailView.ParameterArea.ParamIcon m_TypeIcon
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F0E RID: 3854
			// (get) Token: 0x06007833 RID: 30771 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007834 RID: 30772 RVA: 0x0000216D File Offset: 0x0000036D
			public CardDetailView.ParameterArea.ParamIcon m_PendScaleIcon
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F0F RID: 3855
			// (get) Token: 0x06007835 RID: 30773 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007836 RID: 30774 RVA: 0x0000216D File Offset: 0x0000036D
			public CardDetailView.ParameterArea.ParamIcon m_LvlIcon
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F10 RID: 3856
			// (get) Token: 0x06007837 RID: 30775 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007838 RID: 30776 RVA: 0x0000216D File Offset: 0x0000036D
			public CardDetailView.ParameterArea.ParamIcon m_RankIcon
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F11 RID: 3857
			// (get) Token: 0x06007839 RID: 30777 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600783A RID: 30778 RVA: 0x0000216D File Offset: 0x0000036D
			public CardDetailView.ParameterArea.ParamIcon m_LinkIcon
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F12 RID: 3858
			// (get) Token: 0x0600783B RID: 30779 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600783C RID: 30780 RVA: 0x0000216D File Offset: 0x0000036D
			public CardDetailView.ParameterArea.ParamIcon m_AtkIcon
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F13 RID: 3859
			// (get) Token: 0x0600783D RID: 30781 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600783E RID: 30782 RVA: 0x0000216D File Offset: 0x0000036D
			public CardDetailView.ParameterArea.ParamIcon m_DefIcon
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F14 RID: 3860
			// (get) Token: 0x0600783F RID: 30783 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007840 RID: 30784 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_SpellTrapTypeRoot
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F15 RID: 3861
			// (get) Token: 0x06007841 RID: 30785 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007842 RID: 30786 RVA: 0x0000216D File Offset: 0x0000036D
			public Image m_SpellTrapIconImage
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F16 RID: 3862
			// (get) Token: 0x06007843 RID: 30787 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007844 RID: 30788 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedTextMeshProUGUI m_SpellTrapIconText
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F17 RID: 3863
			// (get) Token: 0x06007845 RID: 30789 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007846 RID: 30790 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_DismantleableRoot
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F18 RID: 3864
			// (get) Token: 0x06007847 RID: 30791 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007848 RID: 30792 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedTextMeshProUGUI m_DismantleableValueText
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x06007849 RID: 30793 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetAsRental(bool isRental, bool dispDismantleableText = false)
			{
			}

			// Token: 0x0600784A RID: 30794 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPrems(int cardId, CardCollectionInfo.Premium prem, bool isRental)
			{
			}

			// Token: 0x0600784B RID: 30795 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x0400B039 RID: 45113
			public GameObject m_RentalCardText;

			// Token: 0x0400B03A RID: 45114
			private ElementObjectManager m_PremiumNumGroupEom;

			// Token: 0x0400B03B RID: 45115
			private ExtendedTextMeshProUGUI m_PremNum0;

			// Token: 0x0400B03C RID: 45116
			private ExtendedTextMeshProUGUI m_PremNum1;

			// Token: 0x0400B03D RID: 45117
			private ExtendedTextMeshProUGUI m_PremNum2;

			// Token: 0x0400B03E RID: 45118
			private Image m_PremSelector0;

			// Token: 0x0400B03F RID: 45119
			private Image m_PremSelector1;

			// Token: 0x0400B040 RID: 45120
			private Image m_PremSelector2;

			// Token: 0x02000FBA RID: 4026
			public class ParamIcon : CardDetailView.ElementWidget
			{
				// Token: 0x17000F19 RID: 3865
				// (get) Token: 0x0600784D RID: 30797 RVA: 0x0000216A File Offset: 0x0000036A
				// (set) Token: 0x0600784E RID: 30798 RVA: 0x0000216D File Offset: 0x0000036D
				public Image m_IconImage
				{
					[CompilerGenerated]
					get
					{
						return null;
					}
					[CompilerGenerated]
					private set
					{
					}
				}

				// Token: 0x17000F1A RID: 3866
				// (get) Token: 0x0600784F RID: 30799 RVA: 0x0000216A File Offset: 0x0000036A
				// (set) Token: 0x06007850 RID: 30800 RVA: 0x0000216D File Offset: 0x0000036D
				public ExtendedTextMeshProUGUI m_IconText
				{
					[CompilerGenerated]
					get
					{
						return null;
					}
					[CompilerGenerated]
					private set
					{
					}
				}

				// Token: 0x06007851 RID: 30801 RVA: 0x0000216D File Offset: 0x0000036D
				protected override void InitializeElements()
				{
				}
			}
		}

		// Token: 0x02000FBB RID: 4027
		private class MenuArea : CardDetailView.ElementWidget
		{
			// Token: 0x17000F1B RID: 3867
			// (get) Token: 0x06007853 RID: 30803 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007854 RID: 30804 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_BookmarkButton
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F1C RID: 3868
			// (get) Token: 0x06007855 RID: 30805 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007856 RID: 30806 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_HowToGetButton
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F1D RID: 3869
			// (get) Token: 0x06007857 RID: 30807 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007858 RID: 30808 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_RelatedCardButton
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F1E RID: 3870
			// (get) Token: 0x06007859 RID: 30809 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600785A RID: 30810 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_AddCardButton
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F1F RID: 3871
			// (get) Token: 0x0600785B RID: 30811 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600785C RID: 30812 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_RemoveCardButton
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F20 RID: 3872
			// (get) Token: 0x0600785D RID: 30813 RVA: 0x0000216A File Offset: 0x0000036A
			public SelectionButton m_CreateButton
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000F21 RID: 3873
			// (get) Token: 0x0600785E RID: 30814 RVA: 0x0000216A File Offset: 0x0000036A
			public SelectionButton m_DismantleButton
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000F22 RID: 3874
			// (get) Token: 0x0600785F RID: 30815 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007860 RID: 30816 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_CardGroupRoot
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F23 RID: 3875
			// (get) Token: 0x06007861 RID: 30817 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007862 RID: 30818 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_MenuGroupRoot
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x17000F24 RID: 3876
			// (get) Token: 0x06007863 RID: 30819 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007864 RID: 30820 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_CraftGroupRoot
			{
				[CompilerGenerated]
				get
				{
					return null;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x06007865 RID: 30821 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x06007866 RID: 30822 RVA: 0x0000216D File Offset: 0x0000036D
			public void ToggleBookmark(bool isBookmarked)
			{
			}

			// Token: 0x06007867 RID: 30823 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeCraftElements()
			{
			}

			// Token: 0x06007868 RID: 30824 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetCraftable(bool craftable, int rarityID)
			{
			}

			// Token: 0x06007869 RID: 30825 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetDismantable(bool dismantable, int rarityID, CardCollectionInfo.Premium premium)
			{
			}

			// Token: 0x0400B041 RID: 45121
			private RectTransform m_BookmarkOn;

			// Token: 0x0400B042 RID: 45122
			private RectTransform m_BookmarkOff;

			// Token: 0x0400B043 RID: 45123
			private CardDetailView.MenuArea.CraftButtonWidget m_CraftCreateButton;

			// Token: 0x0400B044 RID: 45124
			private CardDetailView.MenuArea.CraftButtonWidget m_CraftDismantleButton;

			// Token: 0x02000FBC RID: 4028
			private class CraftButtonWidget : CardDetailView.ElementWidget
			{
				// Token: 0x0600786B RID: 30827 RVA: 0x0000216D File Offset: 0x0000036D
				protected override void InitializeElements()
				{
				}

				// Token: 0x0400B045 RID: 45125
				public SelectionButton m_Button;

				// Token: 0x0400B046 RID: 45126
				public Transform m_CPGroup;

				// Token: 0x0400B047 RID: 45127
				public ExtendedTextMeshProUGUI m_ButtonText;

				// Token: 0x0400B048 RID: 45128
				public Image m_IconCP;

				// Token: 0x0400B049 RID: 45129
				public ExtendedTextMeshProUGUI m_TextCP;

				// Token: 0x0400B04A RID: 45130
				public Image m_IconEnabled;

				// Token: 0x0400B04B RID: 45131
				public Image m_IconDisabled;
			}
		}
	}
}
