using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomGame.Card;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02000F9B RID: 3995
	public class CardActionMenu : CardUtilWidget
	{
		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x0600759F RID: 30111 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_AttrIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060075A0 RID: 30112 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_TunerIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060075A1 RID: 30113 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_TypeIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060075A2 RID: 30114 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_SpellTrapTypeIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060075A3 RID: 30115 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_PendScaleIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060075A4 RID: 30116 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_PendScaleText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060075A5 RID: 30117 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_LvlIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060075A6 RID: 30118 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_LvlText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060075A7 RID: 30119 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_RankIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060075A8 RID: 30120 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_RankText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060075A9 RID: 30121 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_LinkIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x060075AA RID: 30122 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_LinkText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x060075AB RID: 30123 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_AtkIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060075AC RID: 30124 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_AtkText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060075AD RID: 30125 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_DefIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x060075AE RID: 30126 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_DefText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x060075AF RID: 30127 RVA: 0x0000216A File Offset: 0x0000036A
		protected override RectTransform m_SpellTrapType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x060075B0 RID: 30128 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_SpellTrapTypeText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060075B1 RID: 30129 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_RegulationIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060075B2 RID: 30130 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_RarityIcon
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060075B3 RID: 30131 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedScrollRect m_TextArea
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060075B4 RID: 30132 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_CardDesc
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060075B5 RID: 30133 RVA: 0x0000216A File Offset: 0x0000036A
		protected override ExtendedTextMeshProUGUI m_CardDescHeading
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060075B6 RID: 30134 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_DescAreaBG
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060075B7 RID: 30135 RVA: 0x0000216A File Offset: 0x0000036A
		protected override SelectionButton m_CreateButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060075B8 RID: 30136 RVA: 0x0000216A File Offset: 0x0000036A
		protected override SelectionButton m_DismantleButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060075B9 RID: 30137 RVA: 0x0000216A File Offset: 0x0000036A
		protected override SelectionButton m_AddCardButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x060075BA RID: 30138 RVA: 0x0000216A File Offset: 0x0000036A
		protected override SelectionButton m_RemoveCardButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060075BB RID: 30139 RVA: 0x0000216A File Offset: 0x0000036A
		protected override SelectionButton m_BookmarkButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060075BC RID: 30140 RVA: 0x0000216A File Offset: 0x0000036A
		protected override SelectionButton m_HowToGetButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x060075BD RID: 30141 RVA: 0x0000216A File Offset: 0x0000036A
		protected override SelectionButton m_RelatedCardButton
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x060075BE RID: 30142 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Image m_NameAreaBG
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x060075BF RID: 30143 RVA: 0x0000216A File Offset: 0x0000036A
		protected override RubyTextGX m_CardName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x060075C0 RID: 30144 RVA: 0x0000216A File Offset: 0x0000036A
		private ExtendedScrollRect m_PendulumTextArea
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x060075C1 RID: 30145 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform m_PendulumDescArea
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x060075C2 RID: 30146 RVA: 0x0000216A File Offset: 0x0000036A
		private ExtendedTextMeshProUGUI m_CardDescPend
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x060075C3 RID: 30147 RVA: 0x0000216A File Offset: 0x0000036A
		private ExtendedTextMeshProUGUI m_CardDescHeadingPendulum
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x060075C4 RID: 30148 RVA: 0x0000216A File Offset: 0x0000036A
		private Image m_PendulumDescAreaBG
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x060075C5 RID: 30149 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060075C6 RID: 30150 RVA: 0x0000216D File Offset: 0x0000036D
		public Action onClickPrevButton
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

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x060075C7 RID: 30151 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060075C8 RID: 30152 RVA: 0x0000216D File Offset: 0x0000036D
		public Action onClickNextButton
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

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x060075C9 RID: 30153 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060075CA RID: 30154 RVA: 0x0000216D File Offset: 0x0000036D
		private int m_CurrentCardID
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

		// Token: 0x060075CB RID: 30155 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveAddCardArea(bool isActive)
		{
		}

		// Token: 0x060075CC RID: 30156 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveButtonCraftCreate(bool active)
		{
		}

		// Token: 0x060075CD RID: 30157 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveButtonAddCard(bool active)
		{
		}

		// Token: 0x060075CE RID: 30158 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveButtonRemoveCard(bool active)
		{
		}

		// Token: 0x060075CF RID: 30159 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveButtonCraftDismantle(bool active)
		{
		}

		// Token: 0x060075D0 RID: 30160 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetCraftPoint()
		{
		}

		// Token: 0x060075D1 RID: 30161 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateCraftPoint()
		{
		}

		// Token: 0x060075D2 RID: 30162 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToggleBookmark(bool isBookmarked)
		{
		}

		// Token: 0x060075D3 RID: 30163 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveSubMenuButton(bool activeRelatedCard, bool activeSourceButton)
		{
		}

		// Token: 0x060075D4 RID: 30164 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeElements()
		{
		}

		// Token: 0x060075D5 RID: 30165 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x060075D6 RID: 30166 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060075D7 RID: 30167 RVA: 0x0000216D File Offset: 0x0000036D
		private void InnerOpen(int id, int inDeckN, int inDeckAlterN, int inDeckP1, int inDeckAlterP1, int inDeckP2, int inDeckAlterP2, int inDeckR, int inDeckAlterR, bool isFull, CardCollectionInfo.Premium prem, bool isRental, bool isBatchDismantleMode, int reg, int rent, CardActionMenu.TweenType tweenType = CardActionMenu.TweenType.Open)
		{
		}

		// Token: 0x060075D8 RID: 30168 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open(CardActionMenu.TweenType tweenType = CardActionMenu.TweenType.Open, Action onUpdate = null)
		{
		}

		// Token: 0x060075D9 RID: 30169 RVA: 0x0000216D File Offset: 0x0000036D
		public void Open(int cardID, int inDeckN, int inDeckAlterN, int inDeckP1, int inDeckAlterP1, int inDeckP2, int inDeckAlterP2, int inDeckR, int inDeckAlterR, CardCollectionInfo.Premium prem, bool isFull, bool isBatchDismantleMode, int regulationID, int rentalID)
		{
		}

		// Token: 0x060075DA RID: 30170 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenFromCollectionView(int cardID, int premID, InDeckNumInfo inDeckInfo, bool isFull, bool isBatchDismantleMode, int regulationID, int rentalID, DeckView deckView, CardCollectionView collectionView, Action<CardActionMenu, CardBaseData> initAction, CardActionMenu.TweenType tweenType = CardActionMenu.TweenType.Open)
		{
		}

		// Token: 0x060075DB RID: 30171 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenFromDeckView(int idx, InDeckNumInfo inDeckInfo, bool isFull, bool isBatchDismantleMode, int regulationID, int rentalID, DeckView deckView, CardCollectionView collectionView, Action<CardActionMenu, CardBaseData> initAction, CardActionMenu.TweenType tweenType = CardActionMenu.TweenType.Open)
		{
		}

		// Token: 0x060075DC RID: 30172 RVA: 0x0000216D File Offset: 0x0000036D
		public void Close()
		{
		}

		// Token: 0x060075DD RID: 30173 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayPaging(CardActionMenu.TweenType tweenType = CardActionMenu.TweenType.Next, Action onFinish = null, Action onUpdate = null)
		{
			return null;
		}

		// Token: 0x060075DE RID: 30174 RVA: 0x0000216D File Offset: 0x0000036D
		public void PagingCheck(CardBaseData baseData, bool added)
		{
		}

		// Token: 0x060075DF RID: 30175 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBatchDismantleMode(bool active)
		{
		}

		// Token: 0x060075E0 RID: 30176 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInteractableWindow(bool b)
		{
		}

		// Token: 0x060075E1 RID: 30177 RVA: 0x0000216D File Offset: 0x0000036D
		private new void setDescriptionHeading()
		{
		}

		// Token: 0x060075E2 RID: 30178 RVA: 0x0000216D File Offset: 0x0000036D
		protected void setCardText()
		{
		}

		// Token: 0x060075E3 RID: 30179 RVA: 0x0000216D File Offset: 0x0000036D
		protected new void setDescAreaBG()
		{
		}

		// Token: 0x060075E4 RID: 30180 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInDeckIndicators(int numN, int alterN, int numP1, int alterP1, int numP2, int alterP2, int numR, int alterR, bool isFull)
		{
		}

		// Token: 0x060075E5 RID: 30181 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInDeckIndicators(InDeckNumInfo inDeckInfo, bool isFull)
		{
		}

		// Token: 0x060075E6 RID: 30182 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPremiums()
		{
		}

		// Token: 0x060075E7 RID: 30183 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardPremiumType(CardCollectionInfo.Premium prem)
		{
		}

		// Token: 0x060075E8 RID: 30184 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCardTotals(int rentalID = 0, bool isRental = false)
		{
		}

		// Token: 0x060075E9 RID: 30185 RVA: 0x0000216A File Offset: 0x0000036A
		public RectTransform GetCardImageRectTransform()
		{
			return null;
		}

		// Token: 0x060075EA RID: 30186 RVA: 0x0000216D File Offset: 0x0000036D
		protected new void setRarity(bool b = true)
		{
		}

		// Token: 0x060075EB RID: 30187 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRentalImageOverlay(bool active)
		{
		}

		// Token: 0x0400AF20 RID: 44832
		private ElementObjectManager m_Eom;

		// Token: 0x0400AF21 RID: 44833
		private bool isIni;

		// Token: 0x0400AF22 RID: 44834
		private const string tweenLabelIn = "In";

		// Token: 0x0400AF23 RID: 44835
		private const string tweenLabelOut = "Out";

		// Token: 0x0400AF24 RID: 44836
		private SelectionButton m_PrevButton;

		// Token: 0x0400AF25 RID: 44837
		private SelectionButton m_NextButton;

		// Token: 0x0400AF26 RID: 44838
		private SelectionButton m_FlickButton;

		// Token: 0x0400AF27 RID: 44839
		private bool horizontalSwipe;

		// Token: 0x0400AF28 RID: 44840
		private Vector2 pressedPoint;

		// Token: 0x0400AF29 RID: 44841
		private CardBaseData m_PrevCard;

		// Token: 0x0400AF2A RID: 44842
		private CardBaseData m_NextCard;

		// Token: 0x0400AF2B RID: 44843
		private DeckView m_DeckView;

		// Token: 0x0400AF2C RID: 44844
		private CardCollectionView m_CollectionView;

		// Token: 0x0400AF2D RID: 44845
		private bool fromDeck;

		// Token: 0x0400AF2E RID: 44846
		private int regulationID;

		// Token: 0x0400AF2F RID: 44847
		private int rentalID;

		// Token: 0x0400AF30 RID: 44848
		private Action<CardActionMenu, CardBaseData> onInitAction;

		// Token: 0x0400AF31 RID: 44849
		private CardCollectionInfo.Premium m_CurrentPremium;

		// Token: 0x0400AF32 RID: 44850
		private bool m_CurrentRental;

		// Token: 0x0400AF33 RID: 44851
		private int m_CurrentIdx;

		// Token: 0x0400AF34 RID: 44852
		private CardActionMenu.TitleArea m_TitleArea;

		// Token: 0x0400AF35 RID: 44853
		private CardActionMenu.ParameterArea m_ParameterArea;

		// Token: 0x0400AF36 RID: 44854
		private CardActionMenu.DescriptionArea m_DescriptionArea;

		// Token: 0x0400AF37 RID: 44855
		private CardActionMenu.CardArea m_CardArea;

		// Token: 0x0400AF38 RID: 44856
		private CardActionMenu.CraftArea m_CraftArea;

		// Token: 0x0400AF39 RID: 44857
		private CardActionMenu.MenuArea m_MenuArea;

		// Token: 0x0400AF3A RID: 44858
		public CanvasGroup m_Window;

		// Token: 0x0400AF3B RID: 44859
		public SelectionItem m_WindowItem;

		// Token: 0x0400AF3C RID: 44860
		private SelectionButton m_ButtonBack;

		// Token: 0x02000F9C RID: 3996
		public enum TweenType
		{
			// Token: 0x0400AF3E RID: 44862
			Open,
			// Token: 0x0400AF3F RID: 44863
			Close,
			// Token: 0x0400AF40 RID: 44864
			Update,
			// Token: 0x0400AF41 RID: 44865
			Prev,
			// Token: 0x0400AF42 RID: 44866
			Next
		}

		// Token: 0x02000F9D RID: 3997
		private class TitleArea : ElementWidget
		{
			// Token: 0x17000E54 RID: 3668
			// (get) Token: 0x060075ED RID: 30189 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060075EE RID: 30190 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E55 RID: 3669
			// (get) Token: 0x060075EF RID: 30191 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060075F0 RID: 30192 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E56 RID: 3670
			// (get) Token: 0x060075F1 RID: 30193 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060075F2 RID: 30194 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E57 RID: 3671
			// (get) Token: 0x060075F3 RID: 30195 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060075F4 RID: 30196 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x060075F5 RID: 30197 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}
		}

		// Token: 0x02000F9E RID: 3998
		private class ParameterArea : ElementWidget
		{
			// Token: 0x17000E58 RID: 3672
			// (get) Token: 0x060075F7 RID: 30199 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060075F8 RID: 30200 RVA: 0x0000216D File Offset: 0x0000036D
			public CardActionMenu.ParameterArea.ParamIcon m_TunerIcon
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

			// Token: 0x17000E59 RID: 3673
			// (get) Token: 0x060075F9 RID: 30201 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060075FA RID: 30202 RVA: 0x0000216D File Offset: 0x0000036D
			public CardActionMenu.ParameterArea.ParamIcon m_TypeIcon
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

			// Token: 0x17000E5A RID: 3674
			// (get) Token: 0x060075FB RID: 30203 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060075FC RID: 30204 RVA: 0x0000216D File Offset: 0x0000036D
			public CardActionMenu.ParameterArea.ParamIcon m_PendScaleIcon
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

			// Token: 0x17000E5B RID: 3675
			// (get) Token: 0x060075FD RID: 30205 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060075FE RID: 30206 RVA: 0x0000216D File Offset: 0x0000036D
			public CardActionMenu.ParameterArea.ParamIcon m_LvlIcon
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

			// Token: 0x17000E5C RID: 3676
			// (get) Token: 0x060075FF RID: 30207 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007600 RID: 30208 RVA: 0x0000216D File Offset: 0x0000036D
			public CardActionMenu.ParameterArea.ParamIcon m_RankIcon
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

			// Token: 0x17000E5D RID: 3677
			// (get) Token: 0x06007601 RID: 30209 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007602 RID: 30210 RVA: 0x0000216D File Offset: 0x0000036D
			public CardActionMenu.ParameterArea.ParamIcon m_LinkIcon
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

			// Token: 0x17000E5E RID: 3678
			// (get) Token: 0x06007603 RID: 30211 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007604 RID: 30212 RVA: 0x0000216D File Offset: 0x0000036D
			public CardActionMenu.ParameterArea.ParamIcon m_AtkIcon
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

			// Token: 0x17000E5F RID: 3679
			// (get) Token: 0x06007605 RID: 30213 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007606 RID: 30214 RVA: 0x0000216D File Offset: 0x0000036D
			public CardActionMenu.ParameterArea.ParamIcon m_DefIcon
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

			// Token: 0x17000E60 RID: 3680
			// (get) Token: 0x06007607 RID: 30215 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007608 RID: 30216 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_SpellTrapType
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

			// Token: 0x17000E61 RID: 3681
			// (get) Token: 0x06007609 RID: 30217 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600760A RID: 30218 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E62 RID: 3682
			// (get) Token: 0x0600760B RID: 30219 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600760C RID: 30220 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x0600760D RID: 30221 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x02000F9F RID: 3999
			public class ParamIcon : ElementWidget
			{
				// Token: 0x17000E63 RID: 3683
				// (get) Token: 0x0600760F RID: 30223 RVA: 0x0000216A File Offset: 0x0000036A
				// (set) Token: 0x06007610 RID: 30224 RVA: 0x0000216D File Offset: 0x0000036D
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

				// Token: 0x17000E64 RID: 3684
				// (get) Token: 0x06007611 RID: 30225 RVA: 0x0000216A File Offset: 0x0000036A
				// (set) Token: 0x06007612 RID: 30226 RVA: 0x0000216D File Offset: 0x0000036D
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

				// Token: 0x06007613 RID: 30227 RVA: 0x0000216D File Offset: 0x0000036D
				protected override void InitializeElements()
				{
				}
			}
		}

		// Token: 0x02000FA0 RID: 4000
		private class DescriptionArea : ElementWidget
		{
			// Token: 0x17000E65 RID: 3685
			// (get) Token: 0x06007615 RID: 30229 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007616 RID: 30230 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedScrollRect m_PendulumTextArea
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

			// Token: 0x17000E66 RID: 3686
			// (get) Token: 0x06007617 RID: 30231 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007618 RID: 30232 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_PendulumDescArea
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

			// Token: 0x17000E67 RID: 3687
			// (get) Token: 0x06007619 RID: 30233 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600761A RID: 30234 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedTextMeshProUGUI m_CardDescPend
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

			// Token: 0x17000E68 RID: 3688
			// (get) Token: 0x0600761B RID: 30235 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600761C RID: 30236 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedTextMeshProUGUI m_CardDescHeadingPendulum
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

			// Token: 0x17000E69 RID: 3689
			// (get) Token: 0x0600761D RID: 30237 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600761E RID: 30238 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedScrollRect m_TextArea
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

			// Token: 0x17000E6A RID: 3690
			// (get) Token: 0x0600761F RID: 30239 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007620 RID: 30240 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedTextMeshProUGUI m_CardDesc
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

			// Token: 0x17000E6B RID: 3691
			// (get) Token: 0x06007621 RID: 30241 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007622 RID: 30242 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedTextMeshProUGUI m_CardDescHeading
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

			// Token: 0x17000E6C RID: 3692
			// (get) Token: 0x06007623 RID: 30243 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007624 RID: 30244 RVA: 0x0000216D File Offset: 0x0000036D
			public Image m_DescAreaBG
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

			// Token: 0x17000E6D RID: 3693
			// (get) Token: 0x06007625 RID: 30245 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007626 RID: 30246 RVA: 0x0000216D File Offset: 0x0000036D
			public Image m_PendulumDescAreaBG
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

			// Token: 0x06007627 RID: 30247 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}
		}

		// Token: 0x02000FA1 RID: 4001
		private class CardArea : ElementWidget
		{
			// Token: 0x17000E6E RID: 3694
			// (get) Token: 0x06007629 RID: 30249 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600762A RID: 30250 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E6F RID: 3695
			// (get) Token: 0x0600762B RID: 30251 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600762C RID: 30252 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E70 RID: 3696
			// (get) Token: 0x0600762D RID: 30253 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600762E RID: 30254 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E71 RID: 3697
			// (get) Token: 0x0600762F RID: 30255 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007630 RID: 30256 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E72 RID: 3698
			// (get) Token: 0x06007631 RID: 30257 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007632 RID: 30258 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x06007633 RID: 30259 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetCard(int cardId, CardCollectionInfo.Premium premium, bool isRental)
			{
			}

			// Token: 0x06007634 RID: 30260 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x06007635 RID: 30261 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeCardImage()
			{
			}

			// Token: 0x06007636 RID: 30262 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetRentalImage(bool isRental)
			{
			}

			// Token: 0x06007637 RID: 30263 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeCardNumText()
			{
			}

			// Token: 0x06007638 RID: 30264 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetPrems()
			{
			}

			// Token: 0x06007639 RID: 30265 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeIndicator()
			{
			}

			// Token: 0x0600763A RID: 30266 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetInDeckSum(int numN, int alterN, int numP1, int alterP1, int numP2, int alterP2, int numR, int alterR)
			{
			}

			// Token: 0x0600763B RID: 30267 RVA: 0x0000216D File Offset: 0x0000036D
			private void AdjustIndicator(int oldNum, int newNum, List<Image> list, Image template, bool isAlter = false)
			{
			}

			// Token: 0x0600763C RID: 30268 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetInDeckIndicatorColor(bool isFull)
			{
			}

			// Token: 0x0400AF43 RID: 44867
			private int m_CardID;

			// Token: 0x0400AF44 RID: 44868
			private CardCollectionInfo.Premium m_Premium;

			// Token: 0x0400AF45 RID: 44869
			private Image m_RentalImage;

			// Token: 0x0400AF46 RID: 44870
			private bool m_IsRental;

			// Token: 0x0400AF47 RID: 44871
			private ElementObjectManager m_PremiumNumGroupEom;

			// Token: 0x0400AF48 RID: 44872
			private ElementObject m_DismantleCardNumGroup;

			// Token: 0x0400AF49 RID: 44873
			private ElementObject m_RentalCardTextGroup;

			// Token: 0x0400AF4A RID: 44874
			private ExtendedTextMeshProUGUI m_PremNum0;

			// Token: 0x0400AF4B RID: 44875
			private ExtendedTextMeshProUGUI m_PremNum1;

			// Token: 0x0400AF4C RID: 44876
			private ExtendedTextMeshProUGUI m_PremNum2;

			// Token: 0x0400AF4D RID: 44877
			private Image m_PremSelector0;

			// Token: 0x0400AF4E RID: 44878
			private Image m_PremSelector1;

			// Token: 0x0400AF4F RID: 44879
			private Image m_PremSelector2;

			// Token: 0x0400AF50 RID: 44880
			private Image m_IndicatorRental;

			// Token: 0x0400AF51 RID: 44881
			private Image m_Indicator0;

			// Token: 0x0400AF52 RID: 44882
			private Image m_Indicator1;

			// Token: 0x0400AF53 RID: 44883
			private Image m_Indicator2;

			// Token: 0x0400AF54 RID: 44884
			private int inDeckR;

			// Token: 0x0400AF55 RID: 44885
			private int inDeckN;

			// Token: 0x0400AF56 RID: 44886
			private int inDeckP1;

			// Token: 0x0400AF57 RID: 44887
			private int inDeckP2;

			// Token: 0x0400AF58 RID: 44888
			private List<Image> m_IndicatorsR;

			// Token: 0x0400AF59 RID: 44889
			private List<Image> m_IndicatorsN;

			// Token: 0x0400AF5A RID: 44890
			private List<Image> m_IndicatorsP1;

			// Token: 0x0400AF5B RID: 44891
			private List<Image> m_IndicatorsP2;

			// Token: 0x0400AF5C RID: 44892
			private int inDeckAlterR;

			// Token: 0x0400AF5D RID: 44893
			private int inDeckAlterN;

			// Token: 0x0400AF5E RID: 44894
			private int inDeckAlterP1;

			// Token: 0x0400AF5F RID: 44895
			private int inDeckAlterP2;

			// Token: 0x0400AF60 RID: 44896
			private List<Image> m_IndicatorsAlterR;

			// Token: 0x0400AF61 RID: 44897
			private List<Image> m_IndicatorsAlterN;

			// Token: 0x0400AF62 RID: 44898
			private List<Image> m_IndicatorsAlterP1;

			// Token: 0x0400AF63 RID: 44899
			private List<Image> m_IndicatorsAlterP2;
		}

		// Token: 0x02000FA2 RID: 4002
		private class CraftArea : ElementWidget
		{
			// Token: 0x17000E73 RID: 3699
			// (get) Token: 0x0600763E RID: 30270 RVA: 0x0000216A File Offset: 0x0000036A
			public SelectionButton CreateButton
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000E74 RID: 3700
			// (get) Token: 0x0600763F RID: 30271 RVA: 0x0000216A File Offset: 0x0000036A
			public SelectionButton DismantleButton
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06007640 RID: 30272 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x06007641 RID: 30273 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetCraftable(bool craftable, int rarityID)
			{
			}

			// Token: 0x06007642 RID: 30274 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetDismantable(bool dismantable, int rarityID, CardCollectionInfo.Premium premium)
			{
			}

			// Token: 0x0400AF64 RID: 44900
			private CardActionMenu.CraftArea.CraftButtonWidget CraftCreateButton;

			// Token: 0x0400AF65 RID: 44901
			private CardActionMenu.CraftArea.CraftButtonWidget CraftDismantleButton;

			// Token: 0x02000FA3 RID: 4003
			private class CraftButtonWidget : ElementWidget
			{
				// Token: 0x06007644 RID: 30276 RVA: 0x0000216D File Offset: 0x0000036D
				protected override void InitializeElements()
				{
				}

				// Token: 0x0400AF66 RID: 44902
				public SelectionButton m_Button;

				// Token: 0x0400AF67 RID: 44903
				public ExtendedTextMeshProUGUI m_ButtonText;

				// Token: 0x0400AF68 RID: 44904
				public Image m_IconCP;

				// Token: 0x0400AF69 RID: 44905
				public ExtendedTextMeshProUGUI m_TextCP;

				// Token: 0x0400AF6A RID: 44906
				public Image m_IconEnabled;

				// Token: 0x0400AF6B RID: 44907
				public Image m_IconDisabled;
			}
		}

		// Token: 0x02000FA4 RID: 4004
		private class MenuArea : ElementWidget
		{
			// Token: 0x17000E75 RID: 3701
			// (get) Token: 0x06007646 RID: 30278 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007647 RID: 30279 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E76 RID: 3702
			// (get) Token: 0x06007648 RID: 30280 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007649 RID: 30281 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E77 RID: 3703
			// (get) Token: 0x0600764A RID: 30282 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600764B RID: 30283 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E78 RID: 3704
			// (get) Token: 0x0600764C RID: 30284 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600764D RID: 30285 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E79 RID: 3705
			// (get) Token: 0x0600764E RID: 30286 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600764F RID: 30287 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000E7A RID: 3706
			// (get) Token: 0x06007650 RID: 30288 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007651 RID: 30289 RVA: 0x0000216D File Offset: 0x0000036D
			public Selector m_Selector
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

			// Token: 0x06007652 RID: 30290 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x06007653 RID: 30291 RVA: 0x0000216D File Offset: 0x0000036D
			public void ToggleBookmark(bool isBookmarked)
			{
			}

			// Token: 0x06007654 RID: 30292 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetActiveAddCardButtons(bool isActive)
			{
			}

			// Token: 0x0400AF6C RID: 44908
			private RectTransform m_BookmarkOn;

			// Token: 0x0400AF6D RID: 44909
			private RectTransform m_BookmarkOff;
		}
	}
}
