using System;
using System.Collections.Generic;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x020013A2 RID: 5026
	public class SelectionButton_CardInCollection : SelectionButton, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
	{
		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x06009151 RID: 37201 RVA: 0x00141B1C File Offset: 0x0013FD1C
		private GameObject CardPoint
		{
			get
			{
				return this.m_CardPoint = ((this.m_CardPoint != null) ? this.m_CardPoint : base.Manager.GetElement("CardPointRoot"));
			}
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x06009152 RID: 37202 RVA: 0x00141B58 File Offset: 0x0013FD58
		private TextMeshProUGUI TextCardPoint
		{
			get
			{
				return this.m_TextCardPoint = ((this.m_TextCardPoint != null) ? this.m_TextCardPoint : base.Manager.GetElement<TextMeshProUGUI>("TextCardPointValue"));
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x06009153 RID: 37203 RVA: 0x00141B94 File Offset: 0x0013FD94
		// (set) Token: 0x06009154 RID: 37204 RVA: 0x00141B9C File Offset: 0x0013FD9C
		public int CardCode
		{
			get
			{
				return this._cardCode;
			}
			set
			{
				if (this._cardCode != value)
				{
					this._cardCode = value;
					this.card = CardsManager.Get(this._cardCode, false);
					this.SetIcons();
					this.Refresh();
				}
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x06009155 RID: 37205 RVA: 0x00141BCC File Offset: 0x0013FDCC
		private CardRawImageHandler ImageHandler
		{
			get
			{
				return this.m_ImageHandler = ((this.m_ImageHandler != null) ? this.m_ImageHandler : base.Manager.GetElement<CardRawImageHandler>("ImageCard"));
			}
		}

		// Token: 0x06009156 RID: 37206 RVA: 0x00141C08 File Offset: 0x0013FE08
		protected override void Awake()
		{
			this.manuallySetNavigation = false;
			base.Awake();
			this.SetClickEvent(delegate
			{
				if (UserInput.gamepadType == UserInput.GamepadType.None)
				{
					if (DeckEditor.UseMobileLayout)
					{
						if (this.dragProcessing)
						{
							return;
						}
						AudioManager.PlaySE("SE_MENU_DECIDE", 1f);
						Program.instance.deckEditor.lastSelectedCardInCollection = this;
						Program.instance.deckEditor.ResponseRegion = DeckEditorUI.ResponseRegion.Collection;
						Program.instance.deckEditor.GetUI<DeckEditorUI>().ShowCardActionMenu();
						return;
					}
					else
					{
						this.ShowThis();
						if (this.cardCollectionView.area != CardCollectionView.Area.History)
						{
							Program.instance.deckEditor.GetUI<DeckEditorUI>().AddHistoryCard(this.card.Id);
							return;
						}
					}
				}
				else
				{
					this.AddThisToDeck();
				}
			});
			this.SetRightClickEvent(delegate
			{
				this.AddThisToDeck();
			});
		}

		// Token: 0x06009157 RID: 37207 RVA: 0x00141C3B File Offset: 0x0013FE3B
		protected override void OnSelect(bool playSE)
		{
			base.OnSelect(playSE);
			this.ShowThis();
			Program.instance.deckEditor.lastSelectedCardInCollection = this;
			Program.instance.deckEditor.ResponseRegion = DeckEditorUI.ResponseRegion.Collection;
		}

		// Token: 0x06009158 RID: 37208 RVA: 0x00141C6C File Offset: 0x0013FE6C
		private void AddThisToDeck()
		{
			Vector3 position = base.transform.GetChild(0).position;
			Program.instance.deckEditor.GetUI<DeckEditorUI>().AddCardFromCollection(this.card, position);
			this.ShowThis();
		}

		// Token: 0x06009159 RID: 37209 RVA: 0x00141CAC File Offset: 0x0013FEAC
		public void Refresh()
		{
			this.ImageHandler.SetCard(this.card);
		}

		// Token: 0x0600915A RID: 37210 RVA: 0x00141CBF File Offset: 0x0013FEBF
		public void RefreshRarity(int code)
		{
			this.ImageHandler.RefreshRarity(code);
		}

		// Token: 0x0600915B RID: 37211 RVA: 0x00141CCD File Offset: 0x0013FECD
		public void SetRegulationIcon()
		{
			base.Manager.GetElement<Image>("IconLimit").sprite = TextureManager.container.GetCardRegulationIcon(this.CardCode, DeckEditor.banlist);
		}

		// Token: 0x0600915C RID: 37212 RVA: 0x00141CFC File Offset: 0x0013FEFC
		private void SetIcons()
		{
			this.SetRegulationIcon();
			Sprite attributeIcon = TextureManager.container.GetCardAttributeIcon(this.card, false);
			base.Manager.GetElement<Image>("IconAttribute").sprite = ((attributeIcon == null) ? TextureManager.container.typeNone : attributeIcon);
			Sprite spellTrapTypeIcon = TextureManager.container.GetCardSpellTrapTypeIcon(this.card);
			base.Manager.GetElement<Image>("IconSpellTrapType").sprite = ((spellTrapTypeIcon == null) ? TextureManager.container.typeNone : spellTrapTypeIcon);
			Sprite raceIcon = TextureManager.container.GetCardRaceIcon(this.card);
			base.Manager.GetElement<Image>("IconRace").sprite = ((raceIcon == null) ? TextureManager.container.typeNone : raceIcon);
			base.Manager.GetElement<Image>("IconPool").sprite = TextureManager.container.GetCardPoolIcon(this.card);
			base.Manager.GetElement<TextMeshProUGUI>("TextLevel").text = this.card.Level.ToString();
			base.Manager.GetElement<TextMeshProUGUI>("TextRank").text = this.card.Level.ToString();
			base.Manager.GetElement<TextMeshProUGUI>("TextLink").text = this.card.GetLinkCount().ToString();
			base.Manager.GetElement<TextMeshProUGUI>("TextPendulumScale").text = this.card.LScale.ToString();
			this.genesysPoint = OnlineService.GetGenesysPoint(this.card.GetOriginalID());
			string text = this.genesysPoint.ToString();
			if (this.genesysPoint < 0)
			{
				text = "X";
			}
			this.TextCardPoint.text = text;
			this.TextCardPoint.color = OnlineService.GetGenesysPointColor(this.genesysPoint);
			this.RefreshIcons();
			this.RefreshCountIcon();
		}

		// Token: 0x0600915D RID: 37213 RVA: 0x00141EE0 File Offset: 0x001400E0
		public void RefreshIcons()
		{
			base.Manager.GetElement("IconAttribute").SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail);
			base.Manager.GetElement("IconSpellTrapType").SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail);
			base.Manager.GetElement("IconRace").SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail);
			base.Manager.GetElement("IconTuner").SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail && this.card.HasType(CardType.Tuner));
			Card.LevelType levelType = this.card.GetLevelType();
			base.Manager.GetElement("IconLevel").SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail && this.card.HasType(CardType.Monster) && levelType == Card.LevelType.Level);
			base.Manager.GetElement("IconRank").SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail && this.card.HasType(CardType.Monster) && levelType == Card.LevelType.Rank);
			base.Manager.GetElement("IconLink").SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail && this.card.HasType(CardType.Monster) && levelType == Card.LevelType.Link);
			base.Manager.GetElement("IconPendulumScale").SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail && this.card.HasType(CardType.Pendulum));
			base.Manager.GetElement("IconPool").SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Pool);
			this.CardPoint.SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Genesys);
			base.Manager.GetElement("IconLimit").SetActive(DeckEditorUI.cardInfoType != DeckEditorUI.CardInfoType.Genesys);
		}

		// Token: 0x0600915E RID: 37214 RVA: 0x00142098 File Offset: 0x00140298
		public void RefreshCountIcon()
		{
			int ragulation = DeckEditor.banlist.GetQuantity(this.card.Id);
			int count = Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.GetCardCount(this.card.Id);
			Color color = Color.white;
			if (count == ragulation)
			{
				color = Color.yellow;
			}
			if (count > ragulation)
			{
				color = Color.red;
			}
			base.Manager.GetElement<Image>("IconCardUse1").color = color;
			base.Manager.GetElement<Image>("IconCardUse2").color = color;
			base.Manager.GetElement<Image>("IconCardUse3").color = color;
			int i = 1;
			while (i <= count && i < 4)
			{
				base.Manager.GetElement("IconCardUse" + i.ToString()).SetActive(true);
				i++;
			}
			for (int j = count + 1; j < 4; j++)
			{
				base.Manager.GetElement("IconCardUse" + j.ToString()).SetActive(false);
			}
		}

		// Token: 0x0600915F RID: 37215 RVA: 0x001421A4 File Offset: 0x001403A4
		private void ShowThis()
		{
			List<int> cards = Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.printedCards;
			int index = 0;
			for (int i = 0; i < cards.Count; i++)
			{
				if (cards[i] == this.card.Id)
				{
					index = i;
					break;
				}
			}
			Program.instance.deckEditor.GetUI<DeckEditorUI>().ShowDetail(cards, index);
		}

		// Token: 0x06009160 RID: 37216 RVA: 0x0014220C File Offset: 0x0014040C
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.cardCollectionView.superScrollView.scrollRect.OnBeginDrag(eventData);
			this.dragStartPosition = eventData.position;
			this.dragProcessing = true;
			this.draging = false;
			this.dragIni = false;
		}

		// Token: 0x06009161 RID: 37217 RVA: 0x0014225C File Offset: 0x0014045C
		public void OnDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.draging)
			{
				if (!this.dragIni)
				{
					this.dragTarget = Program.instance.deckEditor.GetUI<DeckEditorUI>().DragCard;
					this.dragTarget.gameObject.SetActive(true);
					this.dragTarget.GetChild(0).GetComponent<RawImage>().texture = this.ImageHandler.RawImage.texture;
					this.dragTarget.GetChild(0).GetComponent<RawImage>().material = this.ImageHandler.RawImage.material;
					this.dragIni = true;
					Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.SetDropAreaActive(false);
					UIHover.HoveringLabel = string.Empty;
					UserInput.Draging = true;
				}
				Vector3 position;
				RectTransformUtility.ScreenPointToWorldPointInRectangle(this.dragTarget, eventData.position, eventData.enterEventCamera, out position);
				this.dragTarget.position = position;
				Vector3 anchoredPositon = this.dragTarget.anchoredPosition3D;
				anchoredPositon.z = -10f;
				this.dragTarget.anchoredPosition3D = anchoredPositon;
				return;
			}
			this.cardCollectionView.superScrollView.scrollRect.OnDrag(eventData);
			this.draging = this.NeedStartDrag(this.dragStartPosition, eventData.position);
		}

		// Token: 0x06009162 RID: 37218 RVA: 0x001423B4 File Offset: 0x001405B4
		public void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.cardCollectionView.superScrollView.scrollRect.OnEndDrag(eventData);
			this.dragProcessing = false;
			if (this.draging)
			{
				UserInput.Draging = false;
				Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.SetDropAreaActive(true);
				this.dragTarget.gameObject.SetActive(false);
				Program.instance.deckEditor.GetUI<DeckEditorUI>().AddCardFromCollection(this.card);
				Program.instance.deckEditor.GetUI<DeckEditorUI>().DeckView.HideDeckLocationTable();
			}
		}

		// Token: 0x06009163 RID: 37219 RVA: 0x00142454 File Offset: 0x00140654
		private bool NeedStartDrag(Vector3 startPosition, Vector3 position)
		{
			if (!this.NeedDrag())
			{
				return false;
			}
			Vector3 vector = this.lastDragStartPosition;
			if (startPosition != this.lastDragStartPosition)
			{
				this.diffYOverLimit = false;
				this.lastDragStartPosition = startPosition;
			}
			if (this.diffYOverLimit)
			{
				return false;
			}
			float diffX = Mathf.Abs(position.x - startPosition.x);
			float diffY = Mathf.Abs(position.y - startPosition.y);
			if (diffY > 100f)
			{
				this.diffYOverLimit = true;
				return false;
			}
			return diffX > 10f && diffX > diffY;
		}

		// Token: 0x06009164 RID: 37220 RVA: 0x001424DE File Offset: 0x001406DE
		private bool NeedDrag()
		{
			return DeckEditor.condition != DeckEditor.Condition.ChangeSide;
		}

		// Token: 0x06009165 RID: 37221 RVA: 0x001424EB File Offset: 0x001406EB
		protected override int GetButtonsCount()
		{
			return Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.superScrollView.items.Count;
		}

		// Token: 0x06009166 RID: 37222 RVA: 0x0012843B File Offset: 0x0012663B
		protected override int GetColumnsCount()
		{
			return 6;
		}

		// Token: 0x06009167 RID: 37223 RVA: 0x00142510 File Offset: 0x00140710
		protected override void OnNavigationLeftBorder()
		{
			base.OnNavigationLeftBorder();
			Program.instance.deckEditor.SelectNearestDeckViewItem(base.transform.GetChild(0).position);
		}

		// Token: 0x06009168 RID: 37224 RVA: 0x00142538 File Offset: 0x00140738
		protected override void OnNavigationUpBorder()
		{
			base.OnNavigationUpBorder();
			GameObject target = Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.GetUpNavigationObject();
			if (target != null)
			{
				UserInput.NextSelectionIsAxis = true;
				EventSystem.current.SetSelectedGameObject(target);
			}
		}

		// Token: 0x0400D00F RID: 53263
		private const string LABEL_GO_CARD_POINT = "CardPointRoot";

		// Token: 0x0400D010 RID: 53264
		private GameObject m_CardPoint;

		// Token: 0x0400D011 RID: 53265
		private const string LABEL_TXT_CARD_POINT = "TextCardPointValue";

		// Token: 0x0400D012 RID: 53266
		private TextMeshProUGUI m_TextCardPoint;

		// Token: 0x0400D013 RID: 53267
		private int _cardCode;

		// Token: 0x0400D014 RID: 53268
		public Card card;

		// Token: 0x0400D015 RID: 53269
		public CardCollectionView cardCollectionView;

		// Token: 0x0400D016 RID: 53270
		public int genesysPoint;

		// Token: 0x0400D017 RID: 53271
		private CardRawImageHandler m_ImageHandler;

		// Token: 0x0400D018 RID: 53272
		private RectTransform dragTarget;

		// Token: 0x0400D019 RID: 53273
		private Vector2 dragStartPosition;

		// Token: 0x0400D01A RID: 53274
		private bool dragProcessing;

		// Token: 0x0400D01B RID: 53275
		private bool draging;

		// Token: 0x0400D01C RID: 53276
		private bool dragIni;

		// Token: 0x0400D01D RID: 53277
		private Vector3 lastDragStartPosition;

		// Token: 0x0400D01E RID: 53278
		private bool diffYOverLimit;
	}
}
