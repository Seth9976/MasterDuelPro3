using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.Servant;
using MDPro3.Utility;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x0200145B RID: 5211
	public class DeckEditorUI : ServantUI
	{
		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x06009731 RID: 38705 RVA: 0x001605C8 File Offset: 0x0015E7C8
		public DeckView DeckView
		{
			get
			{
				return this.m_DeckView = ((this.m_DeckView != null) ? this.m_DeckView : base.Manager.GetElement<DeckView>("DeckView"));
			}
		}

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x06009732 RID: 38706 RVA: 0x00160604 File Offset: 0x0015E804
		public CardCollectionView CardCollectionView
		{
			get
			{
				return this.m_CardCollectionView = ((this.m_CardCollectionView != null) ? this.m_CardCollectionView : base.Manager.GetElement<CardCollectionView>("CardCollectionView"));
			}
		}

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x06009733 RID: 38707 RVA: 0x00160640 File Offset: 0x0015E840
		public CardDetailView CardDetailView
		{
			get
			{
				return this.m_CardDetailView = ((this.m_CardDetailView != null) ? this.m_CardDetailView : base.Manager.GetElement<CardDetailView>("CardDetailView"));
			}
		}

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x06009734 RID: 38708 RVA: 0x0016067C File Offset: 0x0015E87C
		public CardActionMenu CardActionMenu
		{
			get
			{
				return this.m_CardActionMenu = ((this.m_CardActionMenu != null) ? this.m_CardActionMenu : base.Manager.GetElement<CardActionMenu>("CardActionMenu"));
			}
		}

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x06009735 RID: 38709 RVA: 0x001606B8 File Offset: 0x0015E8B8
		public RectTransform DragCard
		{
			get
			{
				return this.m_DragCard = ((this.m_DragCard != null) ? this.m_DragCard : base.Manager.GetElement<RectTransform>("DragCard"));
			}
		}

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x06009736 RID: 38710 RVA: 0x001606F4 File Offset: 0x0015E8F4
		public RawImage DragCardImage
		{
			get
			{
				return this.m_DragCardImage = ((this.m_DragCardImage != null) ? this.m_DragCardImage : base.Manager.GetNestedElement<RawImage>("DragCard/ImageCard"));
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06009737 RID: 38711 RVA: 0x00160730 File Offset: 0x0015E930
		private SelectionButton ButtonBack
		{
			get
			{
				return this.m_ButtonBack = ((this.m_ButtonBack != null) ? this.m_ButtonBack : base.Manager.GetNestedElement<SelectionButton>("Header/ButtonBack"));
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06009738 RID: 38712 RVA: 0x0016076C File Offset: 0x0015E96C
		private RectTransform RectBack
		{
			get
			{
				return this.m_RectBack = ((this.m_RectBack != null) ? this.m_RectBack : this.ButtonBack.GetComponent<RectTransform>());
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06009739 RID: 38713 RVA: 0x001607A4 File Offset: 0x0015E9A4
		private SelectionButton ButtonInfo
		{
			get
			{
				return this.m_ButtonInfo = ((this.m_ButtonInfo != null) ? this.m_ButtonInfo : base.Manager.GetNestedElement<SelectionButton>("Header/ButtonInfoSwitching"));
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x0600973A RID: 38714 RVA: 0x001607E0 File Offset: 0x0015E9E0
		private SelectionButton ButtonRegulation
		{
			get
			{
				return this.m_ButtonRegulation = ((this.m_ButtonRegulation != null) ? this.m_ButtonRegulation : base.Manager.GetNestedElement<SelectionButton>("Header/ButtonRegulation"));
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x0600973B RID: 38715 RVA: 0x0016081C File Offset: 0x0015EA1C
		private SelectionButton ButtonTest
		{
			get
			{
				return this.m_ButtonTest = ((this.m_ButtonTest != null) ? this.m_ButtonTest : base.Manager.GetNestedElement<SelectionButton>("Header/ButtonTest"));
			}
		}

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x0600973C RID: 38716 RVA: 0x00160858 File Offset: 0x0015EA58
		private SelectionButton ButtonSave
		{
			get
			{
				return this.m_ButtonSave = ((this.m_ButtonSave != null) ? this.m_ButtonSave : base.Manager.GetNestedElement<SelectionButton>("Header/ButtonSave"));
			}
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x0600973D RID: 38717 RVA: 0x00160894 File Offset: 0x0015EA94
		private SelectionButton ButtonChangeSide
		{
			get
			{
				return this.m_ButtonChangeSide = ((this.m_ButtonChangeSide != null) ? this.m_ButtonChangeSide : base.Manager.GetNestedElement<SelectionButton>("Header/ButtonChangeSide"));
			}
		}

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x0600973E RID: 38718 RVA: 0x001608D0 File Offset: 0x0015EAD0
		private SelectionButton ButtonAppearance
		{
			get
			{
				return this.m_ButtonAppearance = ((this.m_ButtonAppearance != null) ? this.m_ButtonAppearance : base.Manager.GetNestedElement<SelectionButton>("AppearanceGroup"));
			}
		}

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x0600973F RID: 38719 RVA: 0x0016090C File Offset: 0x0015EB0C
		public Image IconCase
		{
			get
			{
				return this.m_IconCase = ((this.m_IconCase != null) ? this.m_IconCase : base.Manager.GetNestedElement<Image>("OverHeader/IconCase"));
			}
		}

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x06009740 RID: 38720 RVA: 0x00160948 File Offset: 0x0015EB48
		public Image IconProtector
		{
			get
			{
				return this.m_IconProtector = ((this.m_IconProtector != null) ? this.m_IconProtector : base.Manager.GetNestedElement<Image>("OverHeader/IconProtector"));
			}
		}

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x06009741 RID: 38721 RVA: 0x00160984 File Offset: 0x0015EB84
		public Image IconField
		{
			get
			{
				return this.m_IconField = ((this.m_IconField != null) ? this.m_IconField : base.Manager.GetNestedElement<Image>("OverHeader/IconField"));
			}
		}

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x06009742 RID: 38722 RVA: 0x001609C0 File Offset: 0x0015EBC0
		public Image IconGrave
		{
			get
			{
				return this.m_IconGrave = ((this.m_IconGrave != null) ? this.m_IconGrave : base.Manager.GetNestedElement<Image>("OverHeader/IconGrave"));
			}
		}

		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x06009743 RID: 38723 RVA: 0x001609FC File Offset: 0x0015EBFC
		public Image IconStand
		{
			get
			{
				return this.m_IconStand = ((this.m_IconStand != null) ? this.m_IconStand : base.Manager.GetNestedElement<Image>("OverHeader/IconStand"));
			}
		}

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x06009744 RID: 38724 RVA: 0x00160A38 File Offset: 0x0015EC38
		public Image IconMate
		{
			get
			{
				return this.m_IconMate = ((this.m_IconMate != null) ? this.m_IconMate : base.Manager.GetNestedElement<Image>("OverHeader/IconMate"));
			}
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06009745 RID: 38725 RVA: 0x00160A74 File Offset: 0x0015EC74
		// (set) Token: 0x06009746 RID: 38726 RVA: 0x00160A7C File Offset: 0x0015EC7C
		public DeckEditorUI.ResponseRegion _ResponseRegion
		{
			get
			{
				return this.m_ResponseRegion;
			}
			set
			{
				this.m_ResponseRegion = value;
				this.ShiftToResponseRegion();
			}
		}

		// Token: 0x06009747 RID: 38727 RVA: 0x00160A8B File Offset: 0x0015EC8B
		private void Awake()
		{
			this.InitializeCardActionMenu();
			this.InitializeDeckView();
			this.InitializeCardDetailView();
			this.InitializeCardCollectionView();
			this.InitializeHeader();
			this.InitializeOverHeader();
		}

		// Token: 0x06009748 RID: 38728 RVA: 0x00160AB1 File Offset: 0x0015ECB1
		private void ShiftToResponseRegion()
		{
			this.DeckView.SetCursor(this._ResponseRegion == DeckEditorUI.ResponseRegion.Deck);
			this.CardCollectionView.SetCursor(this._ResponseRegion == DeckEditorUI.ResponseRegion.Collection);
		}

		// Token: 0x06009749 RID: 38729 RVA: 0x00160ADC File Offset: 0x0015ECDC
		public override void ShowEvent()
		{
			base.ShowEvent();
			if (!this.gotoAppearance && !RoomServant.FromHandTest)
			{
				if (DeckEditor.condition != DeckEditor.Condition.ChangeSide)
				{
					this.ShowBackButton();
				}
				else
				{
					this.HideBackButton();
				}
			}
			else
			{
				this.gotoAppearance = false;
				RoomServant.FromHandTest = false;
				this.ShowBackButton();
			}
			UIManager.SetCanvasMatch(Program.instance.deckEditor.GetCanvasMatch(), 0.45f);
		}

		// Token: 0x0600974A RID: 38730 RVA: 0x00160B42 File Offset: 0x0015ED42
		protected override void HideEvent()
		{
			base.HideEvent();
			UIManager.SetCanvasMatch(1f, 0.4f);
			this.HideBackButton();
			CardRarity.Save();
		}

		// Token: 0x0600974B RID: 38731 RVA: 0x00160B64 File Offset: 0x0015ED64
		protected override void AfterHideEvent()
		{
			base.AfterHideEvent();
			if (!this.gotoAppearance && !RoomServant.FromHandTest)
			{
				this.Dispose();
			}
		}

		// Token: 0x0600974C RID: 38732 RVA: 0x00160B81 File Offset: 0x0015ED81
		private void Dispose()
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
			if (this.loadOnlineDeckCoroutine != null)
			{
				base.StopCoroutine(this.loadOnlineDeckCoroutine);
			}
			this.callExit = false;
			DeckEditorUI.deckLiked = false;
		}

		// Token: 0x0600974D RID: 38733 RVA: 0x00160BAF File Offset: 0x0015EDAF
		private void InitializeCardActionMenu()
		{
			this.CardActionMenu.SetRelatedCardEvent(delegate
			{
				this.CardActionMenu.blockMark = DeckEditorUI.ResponseRegion.Collection;
				this.CardActionMenu.Hide();
				this.ShowRelatedCard(this.CardActionMenu.Card);
			});
		}

		// Token: 0x0600974E RID: 38734 RVA: 0x00160BC8 File Offset: 0x0015EDC8
		public void ShowCardActionMenu()
		{
			if (this._ResponseRegion == DeckEditorUI.ResponseRegion.Deck && Program.instance.deckEditor.lastSelectedCardInDeck != null)
			{
				List<Card> list = new List<Card>();
				int index = 0;
				for (int i = 0; i < this.DeckView.cards.Count; i++)
				{
					list.Add(this.DeckView.cards[i].Card);
					if (this.DeckView.cards[i] == Program.instance.deckEditor.lastSelectedCardInDeck)
					{
						index = i;
					}
				}
				this.CardActionMenu.Show(list, index, this._ResponseRegion);
				this._ResponseRegion = DeckEditorUI.ResponseRegion.Action;
				return;
			}
			if (this._ResponseRegion == DeckEditorUI.ResponseRegion.Collection)
			{
				if (this.CardCollectionView.printedCards == null || this.CardCollectionView.printedCards.Count == 0)
				{
					return;
				}
				if (Program.instance.deckEditor.lastSelectedCardInCollection == null || !Program.instance.deckEditor.lastSelectedCardInCollection.selected)
				{
					return;
				}
				new List<Card>();
				int index2 = 0;
				for (int j = 0; j < this.CardCollectionView.printedCards.Count; j++)
				{
					if (Program.instance.deckEditor.lastSelectedCardInCollection.card.Id == this.CardCollectionView.printedCards[j])
					{
						index2 = j;
						break;
					}
				}
				this.CardActionMenu.Show(this.CardCollectionView.printedCards, index2, this._ResponseRegion);
				this._ResponseRegion = DeckEditorUI.ResponseRegion.Action;
			}
		}

		// Token: 0x0600974F RID: 38735 RVA: 0x00160D5E File Offset: 0x0015EF5E
		private void InitializeCardDetailView()
		{
			if (this.CardDetailView == null)
			{
				return;
			}
			this.CardDetailView.SetRelatedCardEvent(delegate
			{
				this.ShowRelatedCard(this.CardDetailView.Card);
			});
		}

		// Token: 0x06009750 RID: 38736 RVA: 0x00160D86 File Offset: 0x0015EF86
		public void ShowDetail(Card data)
		{
			if (this.CardDetailView != null)
			{
				this.CardDetailView.ShowCard(data);
			}
		}

		// Token: 0x06009751 RID: 38737 RVA: 0x00160DA2 File Offset: 0x0015EFA2
		public void ShowDetail(List<int> cards, int index)
		{
			if (this.CardDetailView != null)
			{
				this.CardDetailView.ShowCard(cards, index);
			}
		}

		// Token: 0x06009752 RID: 38738 RVA: 0x00160DC0 File Offset: 0x0015EFC0
		public void ChangeRarity(CardRarity.Rarity rarity)
		{
			int code = 0;
			if (this._ResponseRegion == DeckEditorUI.ResponseRegion.Action)
			{
				code = this.CardActionMenu.Card.Id;
			}
			else if (this.CardDetailView != null)
			{
				code = this.CardDetailView.Card.Id;
			}
			CardRarity.SetRarity(code, rarity);
			this.UpdateRarity(code);
		}

		// Token: 0x06009753 RID: 38739 RVA: 0x00160E18 File Offset: 0x0015F018
		private void UpdateRarity(int code)
		{
			if (this.CardDetailView != null)
			{
				this.CardDetailView.RefreshRarity(code);
			}
			if (this.CardActionMenu.showing)
			{
				this.CardActionMenu.RefreshRarity(code);
			}
			this.CardCollectionView.RefreshRarity(code);
			this.DeckView.RefreshRarity(code);
		}

		// Token: 0x06009754 RID: 38740 RVA: 0x00160E70 File Offset: 0x0015F070
		private void InitializeDeckView()
		{
			this.DeckView.SetNoItemButtonNavigationEvent(MoveDirection.Right, delegate
			{
				UserInput.NextSelectionIsAxis = true;
				Program.instance.deckEditor.SelectLastCollectionViewItem();
			});
			this.DeckView.GetInputField().onEndEdit.AddListener(delegate(string text)
			{
				Program.instance.deckEditor.SelectLastDeckViewItem();
			});
			DeckView.Condition editConditon = DeckView.Condition.Editable;
			if (DeckEditor.condition == DeckEditor.Condition.OnlineDeck || DeckEditor.condition == DeckEditor.Condition.ReplayDeck)
			{
				editConditon = DeckView.Condition.NonEditable;
			}
			else if (DeckEditor.condition == DeckEditor.Condition.ChangeSide)
			{
				editConditon = DeckView.Condition.ChangeSide;
			}
			this.DeckView.ButtonDeck.SetClickEvent(new UnityAction(this.OnDeckButtonClicked));
			if (DeckEditor.Deck == null)
			{
				this.loadOnlineDeckCoroutine = base.StartCoroutine(this.LoadOnlineDeckAsync());
			}
			else
			{
				this.DeckView.PrintDeck(DeckEditor.Deck, DeckEditor.DeckName, editConditon);
			}
			this.SetDeckButtonText();
		}

		// Token: 0x06009755 RID: 38741 RVA: 0x00160F4F File Offset: 0x0015F14F
		private IEnumerator LoadOnlineDeckAsync()
		{
			Task<OnlineDeck.OnlineDeckData> task = OnlineDeck.GetDeck(DeckEditor.onlineDeckID);
			while (!task.IsCompleted)
			{
				yield return null;
			}
			OnlineDeck.OnlineDeckData onlineDeckData = task.Result;
			if (onlineDeckData == null)
			{
				MessageManager.Cast(InterString.Get("网络异常，获取在线卡组失败。", 0));
				yield break;
			}
			DeckEditor.DeckName = onlineDeckData.deckName;
			DeckEditor.Deck = new Deck(onlineDeckData.deckYdk, onlineDeckData.deckContributor, "");
			this.InitializeDeckView();
			this.loadOnlineDeckCoroutine = null;
			yield break;
		}

		// Token: 0x06009756 RID: 38742 RVA: 0x00160F5E File Offset: 0x0015F15E
		private void RefreshShowingCardCount()
		{
			if (this.CardDetailView != null)
			{
				this.CardDetailView.SetCardCount();
			}
			this.CardCollectionView.RefreshCardCount();
			if (this._ResponseRegion == DeckEditorUI.ResponseRegion.Action)
			{
				this.CardActionMenu.SetCardCount();
			}
		}

		// Token: 0x06009757 RID: 38743 RVA: 0x00160F98 File Offset: 0x0015F198
		public void AddCard(Card data)
		{
			bool playAnimation = this._ResponseRegion != DeckEditorUI.ResponseRegion.Action;
			if (!this.DeckView.AddCard(data, playAnimation, playAnimation))
			{
				return;
			}
			this.AddHistoryCard(data.Id);
			this.RefreshShowingCardCount();
		}

		// Token: 0x06009758 RID: 38744 RVA: 0x00160FD5 File Offset: 0x0015F1D5
		public void AddCardFromCollection(Card data)
		{
			if (!this.DeckView.AddCardFromPosition(data, this.GetDragCardPositon()))
			{
				return;
			}
			if (this.CardCollectionView.area != CardCollectionView.Area.History)
			{
				this.AddHistoryCard(data.Id);
			}
			this.RefreshShowingCardCount();
		}

		// Token: 0x06009759 RID: 38745 RVA: 0x0016100C File Offset: 0x0015F20C
		public void AddCardFromCollection(Card data, Vector3 position)
		{
			if (!this.DeckView.AddCardFromPositionWithSequence(data, position))
			{
				return;
			}
			if (this.CardCollectionView.area != CardCollectionView.Area.History)
			{
				this.AddHistoryCard(data.Id);
			}
			this.RefreshShowingCardCount();
		}

		// Token: 0x0600975A RID: 38746 RVA: 0x00161040 File Offset: 0x0015F240
		public void RemoveCard(Card data)
		{
			if (DeckEditor.condition == DeckEditor.Condition.ChangeSide)
			{
				return;
			}
			SelectionButton_CardInDeck card = this.DeckView.GetCardByData(data);
			if (card == null)
			{
				MessageManager.Toast(InterString.Get("无法删除更多卡片", 0));
				return;
			}
			this.RemoveCardWithAnimation(card);
		}

		// Token: 0x0600975B RID: 38747 RVA: 0x00161084 File Offset: 0x0015F284
		public void RemoveCardWithAnimation(SelectionButton_CardInDeck card)
		{
			bool needSelect = this._ResponseRegion != DeckEditorUI.ResponseRegion.Action;
			if (!this.DeckView.RemoveCard(card, needSelect, true, false))
			{
				return;
			}
			this.AddHistoryCard(card.Card.Id);
			AudioManager.PlaySE("SE_DECK_MINUS", 1f);
			card.transform.SetParent(base.transform, true);
			card.transform.localScale = new Vector3(2f, 2f, 1f);
			CanvasGroup cg = card.GetComponent<CanvasGroup>();
			cg.blocksRaycasts = false;
			this.RefreshShowingCardCount();
			if (needSelect)
			{
				Vector3 endPostion = this.CardCollectionView.GetRubbishBinPositon();
				endPostion.z -= 1f;
				Vector3 startPostion = card.transform.position;
				startPostion.z = endPostion.z;
				card.transform.position = startPostion;
				DOTween.Sequence().Append(card.transform.DOMove(endPostion, 0.4f, false).SetEase(Ease.OutCubic)).Append(card.transform.DOScale(1f, 0.2f).SetEase(Ease.InCubic))
					.Join(cg.DOFade(0f, 0.2f))
					.OnComplete(delegate
					{
						global::UnityEngine.Object.Destroy(card.gameObject);
					});
				return;
			}
			global::UnityEngine.Object.Destroy(card.gameObject);
		}

		// Token: 0x0600975C RID: 38748 RVA: 0x00161213 File Offset: 0x0015F413
		public void RemoveCardByDrag(SelectionButton_CardInDeck card)
		{
			if (!this.DeckView.RemoveCard(card, false, true, true))
			{
				return;
			}
			this.AddHistoryCard(card.Card.Id);
			this.PlayDragCardShrinkAnimation();
		}

		// Token: 0x0600975D RID: 38749 RVA: 0x00161240 File Offset: 0x0015F440
		public void DragCardTo(SelectionButton_CardInDeck dragCard)
		{
			if (!this.DeckView.deckLoaded)
			{
				return;
			}
			if (DeckEditor.condition != DeckEditor.Condition.ChangeSide && !this.DeckView.CanEditCard())
			{
				return;
			}
			Vector3 position = this.GetDragCardPositon();
			SelectionButton_CardInDeck hoverCard = this.DeckView.GetHoveringCard();
			DeckView.DeckLocation location;
			if (hoverCard == null)
			{
				if (UIHover.HoveringLabel == "RemoveDeck")
				{
					this.RemoveCardByDrag(dragCard);
					return;
				}
				if (UIHover.HoveringLabel == "AddBookmark")
				{
					this.BookmarkCard(dragCard.Card.Id);
					this.PlayDragCardShrinkAnimation();
					return;
				}
				if (UIHover.HoveringLabel == "CanNotAddBookmark")
				{
					MessageManager.Toast(InterString.Get("已加入卡片收藏", 0));
					return;
				}
				if (UIHover.HoveringLabel == "MainDeck")
				{
					location = DeckView.DeckLocation.MainDeck;
				}
				else if (UIHover.HoveringLabel == "ExtraDeck")
				{
					location = DeckView.DeckLocation.ExtraDeck;
				}
				else
				{
					if (!(UIHover.HoveringLabel == "SideDeck"))
					{
						dragCard.MoveToParent(position);
						return;
					}
					location = DeckView.DeckLocation.SideDeck;
				}
			}
			else
			{
				location = hoverCard.location;
			}
			if (!this.DeckView.CanSwitchPosition(dragCard.Card, location))
			{
				dragCard.MoveToParent(position);
				return;
			}
			if (!(hoverCard == null))
			{
				this.DeckView.MoveCardToLocationWithSiblingIndex(dragCard, location, hoverCard.transform.GetSiblingIndex(), this.GetDragCardPositon());
				return;
			}
			if (dragCard.location == location)
			{
				dragCard.MoveToParent(position);
				return;
			}
			this.DeckView.MoveCardToLocation(dragCard, location, this.GetDragCardPositon());
		}

		// Token: 0x0600975E RID: 38750 RVA: 0x001613AC File Offset: 0x0015F5AC
		public void CardChangeSide(SelectionButton_CardInDeck card)
		{
			DeckView.DeckLocation location = DeckView.DeckLocation.SideDeck;
			if (card.location == DeckView.DeckLocation.SideDeck)
			{
				location = (card.Card.IsExtraCard() ? DeckView.DeckLocation.ExtraDeck : DeckView.DeckLocation.MainDeck);
			}
			this.DeckView.MoveCardToLocation(card, location, card.transform.position);
		}

		// Token: 0x0600975F RID: 38751 RVA: 0x001613EE File Offset: 0x0015F5EE
		public Vector3 GetDragCardPositon()
		{
			return this.DragCard.position;
		}

		// Token: 0x06009760 RID: 38752 RVA: 0x001613FC File Offset: 0x0015F5FC
		public void OnDeckButtonClicked()
		{
			if (!this.DeckView.deckLoaded)
			{
				return;
			}
			if (!this.DeckView.ButtonDeck.gameObject.activeSelf)
			{
				return;
			}
			if (!DeckEditor.DeckIsFromLocal && DeckEditor.condition == DeckEditor.Condition.OnlineDeck)
			{
				OnlineDeck.LikeDeck(DeckEditor.onlineDeckID);
				DeckEditorUI.deckLiked = true;
				this.SetDeckButtonText();
				return;
			}
			if (this.DeckView.GetDirty() || !DeckEditor.DeckIsFromLocal)
			{
				if (DeckEditor.condition != DeckEditor.Condition.ChangeSide)
				{
					MessageManager.Toast(InterString.Get("请先保存卡组", 0));
				}
				return;
			}
			if (MyCard.account != null)
			{
				OnlineDeck.OnlineDeckData onlineDeck = OnlineDeck.GetByID(DeckEditor.Deck.deckId);
				if (onlineDeck == null || onlineDeck.isDelete)
				{
					return;
				}
				OnlineDeck.UpdatePublicState(DeckEditor.Deck.deckId, !onlineDeck.isPublic);
				onlineDeck.isPublic = !onlineDeck.isPublic;
			}
			this.SetDeckButtonText();
		}

		// Token: 0x06009761 RID: 38753 RVA: 0x001614D4 File Offset: 0x0015F6D4
		private void SetDeckButtonText()
		{
			string text = string.Empty;
			if (DeckEditor.DeckIsFromLocal)
			{
				if (MyCard.account != null)
				{
					OnlineDeck.OnlineDeckData onlineDeck = OnlineDeck.GetByID(DeckEditor.Deck.deckId);
					if (onlineDeck == null || onlineDeck.isDelete)
					{
						this.DeckView.ButtonDeck.gameObject.SetActive(false);
						return;
					}
					if (onlineDeck.isPublic)
					{
						text = InterString.Get("公开中", 0);
					}
					else
					{
						text = InterString.Get("非公开中", 0);
					}
				}
			}
			else if (DeckEditor.condition == DeckEditor.Condition.OnlineDeck)
			{
				text = InterString.Get("点赞", 0);
				if (DeckEditorUI.deckLiked)
				{
					this.DeckView.ButtonDeck.gameObject.SetActive(false);
					return;
				}
			}
			this.DeckView.ButtonDeck.SetButtonText(text);
			this.DeckView.ButtonDeck.gameObject.SetActive(text != string.Empty);
		}

		// Token: 0x06009762 RID: 38754 RVA: 0x001615B1 File Offset: 0x0015F7B1
		private void InitializeCardCollectionView()
		{
			this.CardCollectionView.historyCards = DeckEditor.historyCards;
			this.CardCollectionView.SetNoItemButtonNavigationEvent(MoveDirection.Left, delegate
			{
				UserInput.NextSelectionIsAxis = true;
				Program.instance.deckEditor.SelectLastDeckViewItem();
			});
		}

		// Token: 0x06009763 RID: 38755 RVA: 0x001615F0 File Offset: 0x0015F7F0
		public void BookmarkCard(int code)
		{
			CardRarity.BookmarkCard(code);
			if (this.CardDetailView != null)
			{
				this.CardDetailView.RefreshBookmarkToggle();
			}
			if (this._ResponseRegion == DeckEditorUI.ResponseRegion.Action)
			{
				this.CardActionMenu.RefreshBookmarkToggle();
			}
			if (this.CardCollectionView.area == CardCollectionView.Area.Bookmark)
			{
				this.CardCollectionView.PrintBookmarkCards();
			}
		}

		// Token: 0x06009764 RID: 38756 RVA: 0x0016164C File Offset: 0x0015F84C
		public void UnbookmarkCard(int code)
		{
			CardRarity.UnbookmarkCard(code);
			if (this.CardDetailView != null)
			{
				this.CardDetailView.RefreshBookmarkToggle();
			}
			if (this._ResponseRegion == DeckEditorUI.ResponseRegion.Action)
			{
				this.CardActionMenu.RefreshBookmarkToggle();
			}
			if (this.CardCollectionView.area == CardCollectionView.Area.Bookmark)
			{
				this.CardCollectionView.PrintBookmarkCards();
			}
		}

		// Token: 0x06009765 RID: 38757 RVA: 0x001616A5 File Offset: 0x0015F8A5
		public void AddHistoryCard(int code)
		{
			if (DeckEditor.condition == DeckEditor.Condition.ChangeSide)
			{
				return;
			}
			this.CardCollectionView.AddHistoryCard(code);
		}

		// Token: 0x06009766 RID: 38758 RVA: 0x001616BC File Offset: 0x0015F8BC
		public void AddHistoryCards(List<int> codes)
		{
			if (DeckEditor.condition == DeckEditor.Condition.ChangeSide)
			{
				return;
			}
			this.CardCollectionView.AddHistoryCards(codes);
		}

		// Token: 0x06009767 RID: 38759 RVA: 0x001616D3 File Offset: 0x0015F8D3
		public bool NeedAddCardToHistoryWhenClick()
		{
			return this.CardCollectionView.area != CardCollectionView.Area.History;
		}

		// Token: 0x06009768 RID: 38760 RVA: 0x001616E6 File Offset: 0x0015F8E6
		public void ShowRelatedCard(Card data)
		{
			this.CardCollectionView.ShowRelatedCard(data);
		}

		// Token: 0x06009769 RID: 38761 RVA: 0x001616F4 File Offset: 0x0015F8F4
		public void HideRelatedCard()
		{
			this.CardCollectionView.HideRelatedCard();
		}

		// Token: 0x0600976A RID: 38762 RVA: 0x00161704 File Offset: 0x0015F904
		private void InitializeHeader()
		{
			this.ButtonRegulation.SetButtonText(DeckEditor.banlist.Name);
			if (DeckEditor.condition == DeckEditor.Condition.ChangeSide)
			{
				global::UnityEngine.Object.Destroy(this.ButtonTest.gameObject);
				global::UnityEngine.Object.Destroy(this.ButtonSave.gameObject);
				return;
			}
			global::UnityEngine.Object.Destroy(this.ButtonChangeSide.gameObject);
		}

		// Token: 0x0600976B RID: 38763 RVA: 0x00161760 File Offset: 0x0015F960
		public void SetCardInfoType()
		{
			DeckEditorUI.CardInfoType type = (DeckEditorUI.cardInfoType + 1) % (DeckEditorUI.CardInfoType)4;
			this.SetCardInfoType(type);
			SelectionButton_CardInfoType.instance.SetCardInfoTypeIcon(type);
		}

		// Token: 0x0600976C RID: 38764 RVA: 0x0016178C File Offset: 0x0015F98C
		public void SetCardInfoType(DeckEditorUI.CardInfoType type)
		{
			AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
			DeckEditorUI.cardInfoType = type;
			switch (DeckEditorUI.cardInfoType)
			{
			case DeckEditorUI.CardInfoType.None:
				MessageManager.Toast(InterString.Get("切换到简单显示", 0));
				break;
			case DeckEditorUI.CardInfoType.Detail:
				MessageManager.Toast(InterString.Get("切换到详情显示", 0));
				break;
			case DeckEditorUI.CardInfoType.Pool:
				MessageManager.Toast(InterString.Get("切换到归属显示", 0));
				break;
			case DeckEditorUI.CardInfoType.Genesys:
				MessageManager.Toast(InterString.Get("切换到Genesys积分显示", 0));
				break;
			}
			this.DeckView.SetCardInfoType(type);
			this.CardCollectionView.SetCardInfoType(type);
		}

		// Token: 0x0600976D RID: 38765 RVA: 0x0016182C File Offset: 0x0015FA2C
		private void RefreshRegulationIcons()
		{
			foreach (SelectionButton_CardInDeck selectionButton_CardInDeck in this.DeckView.cards)
			{
				selectionButton_CardInDeck.SetRegulationIcon();
			}
			foreach (GameObject gameObject in this.CardCollectionView.superScrollView.gameObjects)
			{
				gameObject.GetComponent<SelectionButton_CardInCollection>().SetRegulationIcon();
			}
		}

		// Token: 0x0600976E RID: 38766 RVA: 0x001618D0 File Offset: 0x0015FAD0
		public void OnRegulation()
		{
			AudioManager.PlaySE("SE_MENU_DECIDE", 1f);
			List<string> selections = new List<string>
			{
				InterString.Get("禁限卡表", 0),
				string.Empty
			};
			foreach (Banlist list in BanlistManager.Banlists)
			{
				selections.Add(list.Name);
			}
			UIManager.ShowPopupSelection(selections, new Action(this.ChangeRegulation), null);
		}

		// Token: 0x0600976F RID: 38767 RVA: 0x0016196C File Offset: 0x0015FB6C
		private void ChangeRegulation()
		{
			string selected = EventSystem.current.currentSelectedGameObject.GetComponent<SelectionButton>().GetButtonText();
			DeckEditor.banlist = BanlistManager.GetByName(selected);
			this.ButtonRegulation.SetButtonText(selected);
			this.RefreshRegulationIcons();
		}

		// Token: 0x06009770 RID: 38768 RVA: 0x001619AC File Offset: 0x0015FBAC
		public void OnSubMenu()
		{
			if (!this.DeckView.deckLoaded)
			{
				return;
			}
			List<string> menus = new List<string>
			{
				InterString.Get("副菜单", 0),
				InterString.Get("重置", 0),
				InterString.Get("排序", 0),
				InterString.Get("打乱", 0)
			};
			List<Action> actions = new List<Action>
			{
				null,
				new Action(this.OnReset),
				new Action(this.OnSort),
				new Action(this.OnRandom)
			};
			if (DeckEditor.condition != DeckEditor.Condition.ChangeSide)
			{
				menus.AddRange(new List<string>
				{
					InterString.Get("复制", 0),
					InterString.Get("分享", 0),
					InterString.Get("测试", 0),
					InterString.Get("清空", 0)
				});
				actions.AddRange(new List<Action>
				{
					new Action(this.OnCopy),
					new Action(this.OnShare),
					new Action(this.OnHandTest),
					new Action(this.OnClearDeck)
				});
			}
			UIManager.ShowSubMenu(menus, actions);
		}

		// Token: 0x06009771 RID: 38769 RVA: 0x00161B08 File Offset: 0x0015FD08
		public void OnSave()
		{
			if (DeckEditor.DeckIsFromLocal && !this.DeckView.GetDirty())
			{
				return;
			}
			if (DeckEditor.DeckIsFromLocal && DeckEditor.banlist.Name != BanlistManager.EmptyBanlistName && (this.DeckView.mainCount > 60 || this.DeckView.extraCount > 15 || this.DeckView.sideCount > 15))
			{
				UIManager.ShowPopupConfirm(new List<string>
				{
					InterString.Get("保存失败", 0),
					InterString.Get("卡组内卡片张数超过限制。@n如需无视限制，请将禁限卡表设置为无（N/A）。", 0)
				});
				this.callExit = false;
				return;
			}
			if (!DeckEditor.DeckIsFromLocal && File.Exists("Deck/" + DeckEditor.DeckName + ".ydk"))
			{
				UIManager.ShowPopupYesOrNo(new List<string>
				{
					InterString.Get("该卡组名已存在", 0),
					InterString.Get("该卡组名的文件已存在，是否直接覆盖创建？", 0),
					InterString.Get("覆盖", 0),
					InterString.Get("取消", 0)
				}, new Action(this.OnSaveConfirmed), delegate
				{
					this.callExit = false;
				});
				return;
			}
			this.OnSaveConfirmed();
		}

		// Token: 0x06009772 RID: 38770 RVA: 0x00161C3C File Offset: 0x0015FE3C
		private void OnSaveConfirmed()
		{
			if (!this.DeckView.Save())
			{
				return;
			}
			if (this.callExit)
			{
				this.CG.blocksRaycasts = false;
				Program.instance.deckEditor.CallExitIn(2f);
				return;
			}
			DeckEditor.DeckIsFromLocal = true;
			this.SetDeckButtonText();
		}

		// Token: 0x06009773 RID: 38771 RVA: 0x00161C8C File Offset: 0x0015FE8C
		public void OnReset()
		{
			this.DeckView.ResetDeck();
		}

		// Token: 0x06009774 RID: 38772 RVA: 0x00161C99 File Offset: 0x0015FE99
		public void OnSort()
		{
			this.DeckView.Sort();
		}

		// Token: 0x06009775 RID: 38773 RVA: 0x00161CA6 File Offset: 0x0015FEA6
		public void OnRandom()
		{
			this.DeckView.Randomize();
		}

		// Token: 0x06009776 RID: 38774 RVA: 0x00161CB3 File Offset: 0x0015FEB3
		public void OnCopy()
		{
			this.DeckView.Copy();
		}

		// Token: 0x06009777 RID: 38775 RVA: 0x00161CC0 File Offset: 0x0015FEC0
		public void OnShare()
		{
			this.DeckView.Share();
		}

		// Token: 0x06009778 RID: 38776 RVA: 0x00161CCD File Offset: 0x0015FECD
		public void OnHandTest()
		{
			if (!this.DeckView.deckLoaded)
			{
				return;
			}
			if (DeckEditor.condition == DeckEditor.Condition.ChangeSide)
			{
				return;
			}
			if (!DeckEditor.DeckIsFromLocal)
			{
				if (DeckEditor.condition != DeckEditor.Condition.ChangeSide)
				{
					MessageManager.Toast(InterString.Get("请先保存卡组", 0));
				}
				return;
			}
			this.HandTestAsync();
		}

		// Token: 0x06009779 RID: 38777 RVA: 0x00161D10 File Offset: 0x0015FF10
		private async UniTask HandTestAsync()
		{
			int port = 7911;
			while (!TcpHelper.IsPortAvailable(port))
			{
				int num = port;
				port = num + 1;
				if (port == 65536)
				{
					port = 1;
				}
			}
			string text = string.Format("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9} {10} {11}", new object[]
			{
				port.ToString(),
				"-1",
				"5",
				"0",
				"F",
				"T",
				"T",
				"8000",
				"5",
				"1",
				"0",
				"0"
			});
			RoomServant.FromSolo = false;
			RoomServant.FromLocalHost = false;
			RoomServant.FromHandTest = true;
			YgoServer.StartServer(text);
			UIManager.UIBlackIn(0.3f);
			await UniTask.WaitForSeconds(0.3f, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			OcgCore.handler = new OcgCore.ResponseHandler(Program.instance.room.Handler);
			Program.instance.solo.StartAIForHandTest(port);
			await UniTask.Delay(100, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			bool joined = false;
			TcpHelper.LinkStart("127.0.0.1", Config.Get("DuelPlayerName0", "@ui"), port.ToString(), string.Empty, true, delegate
			{
				joined = true;
			});
			await UniTask.WaitUntil(() => joined, PlayerLoopTiming.Update, default(CancellationToken), false);
			TcpHelper.CtosMessage_UpdateDeck(this.DeckView.FromObjectDeckToCodedDeck());
			TcpHelper.CtosMessage_HsReady();
			await UniTask.Delay(50, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			TcpHelper.CtosMessage_HandResult(2);
			await UniTask.Delay(50, false, PlayerLoopTiming.Update, default(CancellationToken), false);
			TcpHelper.CtosMessage_TpResult(true);
		}

		// Token: 0x0600977A RID: 38778 RVA: 0x00161D54 File Offset: 0x0015FF54
		private void OnClearDeck()
		{
			List<int> codes = new List<int>();
			foreach (SelectionButton_CardInDeck card in this.DeckView.cards)
			{
				codes.Add(card.Card.Id);
			}
			if (!this.DeckView.ClearDeck())
			{
				return;
			}
			AudioManager.PlaySE("SE_DECK_MINUS", 1f);
			this.AddHistoryCards(codes);
			this.RefreshShowingCardCount();
		}

		// Token: 0x0600977B RID: 38779 RVA: 0x00161DE8 File Offset: 0x0015FFE8
		public void OnChangeSideComplete()
		{
			TcpHelper.CtosMessage_UpdateDeck(this.DeckView.FromObjectDeckToCodedDeck());
		}

		// Token: 0x0600977C RID: 38780 RVA: 0x00161DFC File Offset: 0x0015FFFC
		private void ShowBackButton()
		{
			this.ButtonBack.gameObject.SetActive(true);
			this.RectBack.anchoredPosition3D = new Vector3(24f, 120f, 0f);
			DOTween.Sequence().AppendInterval(0.6f).Append(this.RectBack.DOAnchorPos3D(new Vector3(24f, 0f, 0f), 0.2f, false).SetEase(Ease.OutQuart));
		}

		// Token: 0x0600977D RID: 38781 RVA: 0x00161E7A File Offset: 0x0016007A
		private void HideBackButton()
		{
			this.ButtonBack.gameObject.SetActive(false);
		}

		// Token: 0x0600977E RID: 38782 RVA: 0x00161E8D File Offset: 0x0016008D
		private void InitializeOverHeader()
		{
			if (DeckEditor.condition == DeckEditor.Condition.ChangeSide)
			{
				this.ButtonAppearance.gameObject.SetActive(false);
				return;
			}
			this.RefreshOverHeaderIconsAsync();
		}

		// Token: 0x0600977F RID: 38783 RVA: 0x00161EB0 File Offset: 0x001600B0
		private async UniTask RefreshOverHeaderIconsAsync()
		{
			this.IconCase.color = Color.clear;
			this.IconProtector.color = Color.clear;
			this.IconField.color = Color.clear;
			this.IconGrave.color = Color.clear;
			this.IconStand.color = Color.clear;
			this.IconMate.color = Color.clear;
			while (DeckEditor.Deck == null)
			{
				await TaskUtility.WaitOneFrame(base.gameObject);
			}
			Sprite sprite = await Program.items.LoadDeckCaseIconAsync(DeckEditor.Deck.Case, "_L_SD");
			if (sprite != null)
			{
				this.IconCase.color = Color.white;
				this.IconCase.sprite = sprite;
			}
			Image image = this.IconProtector;
			image.material = await ABLoader.LoadProtectorMaterial(DeckEditor.Deck.Protector.ToString(), Application.exitCancellationToken);
			image = null;
			this.IconProtector.color = Color.white;
			image = this.IconField;
			image.sprite = await Program.items.LoadItemIconAsync(DeckEditor.Deck.Field.ToString(), Items.ItemType.Mat);
			image = null;
			this.IconField.color = Color.white;
			image = this.IconGrave;
			image.sprite = await Program.items.LoadItemIconAsync(DeckEditor.Deck.Grave.ToString(), Items.ItemType.Grave);
			image = null;
			this.IconGrave.color = Color.white;
			image = this.IconStand;
			image.sprite = await Program.items.LoadItemIconAsync(DeckEditor.Deck.Stand.ToString(), Items.ItemType.Stand);
			image = null;
			this.IconStand.color = Color.white;
			string mate = DeckEditor.Deck.Mate.ToString();
			if (mate.Length == 7 && mate.StartsWith("100"))
			{
				image = this.IconMate;
				image.sprite = await Program.items.LoadItemIconAsync(mate, Items.ItemType.Mate);
				image = null;
				this.IconMate.color = Color.white;
			}
			else
			{
				Texture2D art = await CardImageLoader.LoadArtAsync(DeckEditor.Deck.Mate, true, default(CancellationToken));
				this.IconMate.sprite = TextureManager.Texture2Sprite(art);
				this.IconMate.color = Color.white;
			}
		}

		// Token: 0x06009780 RID: 38784 RVA: 0x00161EF4 File Offset: 0x001600F4
		public void ShiftToAppearance()
		{
			if (!this.DeckView.deckLoaded)
			{
				return;
			}
			if (DeckEditor.condition == DeckEditor.Condition.ChangeSide)
			{
				return;
			}
			if (!DeckEditor.DeckIsFromLocal)
			{
				if (DeckEditor.condition != DeckEditor.Condition.ChangeSide)
				{
					MessageManager.Toast(InterString.Get("请先保存卡组", 0));
				}
				return;
			}
			this.gotoAppearance = true;
			Program.instance.appearance.SwitchCondition(Appearance.Condition.DeckEditor);
			Program.instance.ShiftToServant(Program.instance.appearance);
		}

		// Token: 0x06009781 RID: 38785 RVA: 0x00161F64 File Offset: 0x00160164
		private void PlayDragCardShrinkAnimation()
		{
			this.DragCard.gameObject.SetActive(true);
			this.DragCard.localScale = Vector3.one;
			this.DragCard.DOScale(0.5f, 0.2f).SetEase(Ease.InCubic);
			this.DragCardImage.DOFade(0.5f, 0.2f).SetEase(Ease.InCubic).OnComplete(delegate
			{
				this.DragCard.localScale = Vector3.one;
				this.DragCardImage.color = Color.white;
				this.DragCard.gameObject.SetActive(false);
			});
		}

		// Token: 0x0400D594 RID: 54676
		private const string LABEL_MONO_DECKVIEW = "DeckView";

		// Token: 0x0400D595 RID: 54677
		private DeckView m_DeckView;

		// Token: 0x0400D596 RID: 54678
		private const string LABEL_MONO_CARDCOLLECTIONVIEW = "CardCollectionView";

		// Token: 0x0400D597 RID: 54679
		private CardCollectionView m_CardCollectionView;

		// Token: 0x0400D598 RID: 54680
		private const string LABEL_MONO_CARDDETAILVIEW = "CardDetailView";

		// Token: 0x0400D599 RID: 54681
		private CardDetailView m_CardDetailView;

		// Token: 0x0400D59A RID: 54682
		private const string LABEL_MONO_CARDACTIONMENU = "CardActionMenu";

		// Token: 0x0400D59B RID: 54683
		private CardActionMenu m_CardActionMenu;

		// Token: 0x0400D59C RID: 54684
		private const string LABEL_RT_DRAGCARD = "DragCard";

		// Token: 0x0400D59D RID: 54685
		private RectTransform m_DragCard;

		// Token: 0x0400D59E RID: 54686
		private const string LABEL_RIMG_DRAGCARDIMAGE = "DragCard/ImageCard";

		// Token: 0x0400D59F RID: 54687
		private RawImage m_DragCardImage;

		// Token: 0x0400D5A0 RID: 54688
		private const string LABEL_SBN_BACK = "Header/ButtonBack";

		// Token: 0x0400D5A1 RID: 54689
		private SelectionButton m_ButtonBack;

		// Token: 0x0400D5A2 RID: 54690
		private RectTransform m_RectBack;

		// Token: 0x0400D5A3 RID: 54691
		private const string LABEL_SBN_INFO = "Header/ButtonInfoSwitching";

		// Token: 0x0400D5A4 RID: 54692
		private SelectionButton m_ButtonInfo;

		// Token: 0x0400D5A5 RID: 54693
		private const string LABEL_SBN_REGULATION = "Header/ButtonRegulation";

		// Token: 0x0400D5A6 RID: 54694
		private SelectionButton m_ButtonRegulation;

		// Token: 0x0400D5A7 RID: 54695
		private const string LABEL_SBN_TEST = "Header/ButtonTest";

		// Token: 0x0400D5A8 RID: 54696
		private SelectionButton m_ButtonTest;

		// Token: 0x0400D5A9 RID: 54697
		private const string LABEL_SBN_SAVE = "Header/ButtonSave";

		// Token: 0x0400D5AA RID: 54698
		private SelectionButton m_ButtonSave;

		// Token: 0x0400D5AB RID: 54699
		private const string LABEL_SBN_CHANGESIDE = "Header/ButtonChangeSide";

		// Token: 0x0400D5AC RID: 54700
		private SelectionButton m_ButtonChangeSide;

		// Token: 0x0400D5AD RID: 54701
		private const string LABEL_SBN_APPEARANCE = "AppearanceGroup";

		// Token: 0x0400D5AE RID: 54702
		private SelectionButton m_ButtonAppearance;

		// Token: 0x0400D5AF RID: 54703
		private const string LABEL_IMG_ICONCASE = "OverHeader/IconCase";

		// Token: 0x0400D5B0 RID: 54704
		private Image m_IconCase;

		// Token: 0x0400D5B1 RID: 54705
		private const string LABEL_IMG_ICONPROTECTOR = "OverHeader/IconProtector";

		// Token: 0x0400D5B2 RID: 54706
		private Image m_IconProtector;

		// Token: 0x0400D5B3 RID: 54707
		private const string LABEL_IMG_ICONFIELD = "OverHeader/IconField";

		// Token: 0x0400D5B4 RID: 54708
		private Image m_IconField;

		// Token: 0x0400D5B5 RID: 54709
		private const string LABEL_IMG_ICONGRAVE = "OverHeader/IconGrave";

		// Token: 0x0400D5B6 RID: 54710
		private Image m_IconGrave;

		// Token: 0x0400D5B7 RID: 54711
		private const string LABEL_IMG_ICONSTAND = "OverHeader/IconStand";

		// Token: 0x0400D5B8 RID: 54712
		private Image m_IconStand;

		// Token: 0x0400D5B9 RID: 54713
		private const string LABEL_IMG_ICONMATE = "OverHeader/IconMate";

		// Token: 0x0400D5BA RID: 54714
		private Image m_IconMate;

		// Token: 0x0400D5BB RID: 54715
		private bool gotoAppearance;

		// Token: 0x0400D5BC RID: 54716
		private static bool deckLiked;

		// Token: 0x0400D5BD RID: 54717
		public bool callExit;

		// Token: 0x0400D5BE RID: 54718
		public static DeckEditorUI.CardInfoType cardInfoType;

		// Token: 0x0400D5BF RID: 54719
		private DeckEditorUI.ResponseRegion m_ResponseRegion;

		// Token: 0x0400D5C0 RID: 54720
		private Coroutine loadOnlineDeckCoroutine;

		// Token: 0x0200145C RID: 5212
		public enum CardInfoType
		{
			// Token: 0x0400D5C2 RID: 54722
			None,
			// Token: 0x0400D5C3 RID: 54723
			Detail,
			// Token: 0x0400D5C4 RID: 54724
			Pool,
			// Token: 0x0400D5C5 RID: 54725
			Genesys
		}

		// Token: 0x0200145D RID: 5213
		public enum ResponseRegion
		{
			// Token: 0x0400D5C7 RID: 54727
			Deck,
			// Token: 0x0400D5C8 RID: 54728
			Collection,
			// Token: 0x0400D5C9 RID: 54729
			Action
		}
	}
}
