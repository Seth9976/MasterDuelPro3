using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YgomSystem.UI;

namespace MDPro3.UI
{
	// Token: 0x020013A3 RID: 5027
	public class SelectionButton_CardInDeck : SelectionButton, IBeginDragHandler, IEventSystemHandler, IEndDragHandler, IDragHandler
	{
		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x0600916C RID: 37228 RVA: 0x00142638 File Offset: 0x00140838
		private CardRawImageHandler ImageCardHandler
		{
			get
			{
				return this.m_ImageCardHandler = ((this.m_ImageCardHandler != null) ? this.m_ImageCardHandler : base.Manager.GetElement<CardRawImageHandler>("ImageCard"));
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x0600916D RID: 37229 RVA: 0x00142674 File Offset: 0x00140874
		private Image IconLimit
		{
			get
			{
				return this.m_IconLimit = ((this.m_IconLimit != null) ? this.m_IconLimit : base.Manager.GetElement<Image>("IconLimit"));
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x0600916E RID: 37230 RVA: 0x001426B0 File Offset: 0x001408B0
		private Image IconAttribute
		{
			get
			{
				return this.m_IconAttribute = ((this.m_IconAttribute != null) ? this.m_IconAttribute : base.Manager.GetElement<Image>("IconAttribute"));
			}
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x0600916F RID: 37231 RVA: 0x001426EC File Offset: 0x001408EC
		private Image IconSpellTrapType
		{
			get
			{
				return this.m_IconSpellTrapType = ((this.m_IconSpellTrapType != null) ? this.m_IconSpellTrapType : base.Manager.GetElement<Image>("IconSpellTrapType"));
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x06009170 RID: 37232 RVA: 0x00142728 File Offset: 0x00140928
		private Image IconRace
		{
			get
			{
				return this.m_IconRace = ((this.m_IconRace != null) ? this.m_IconRace : base.Manager.GetElement<Image>("IconRace"));
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x06009171 RID: 37233 RVA: 0x00142764 File Offset: 0x00140964
		private Image IconPool
		{
			get
			{
				return this.m_IconPool = ((this.m_IconPool != null) ? this.m_IconPool : base.Manager.GetElement<Image>("IconPool"));
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x06009172 RID: 37234 RVA: 0x001427A0 File Offset: 0x001409A0
		private Image IconTuner
		{
			get
			{
				return this.m_IconTuner = ((this.m_IconTuner != null) ? this.m_IconTuner : base.Manager.GetElement<Image>("IconTuner"));
			}
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x06009173 RID: 37235 RVA: 0x001427DC File Offset: 0x001409DC
		private Image IconLevel
		{
			get
			{
				return this.m_IconLevel = ((this.m_IconLevel != null) ? this.m_IconLevel : base.Manager.GetElement<Image>("IconLevel"));
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x06009174 RID: 37236 RVA: 0x00142818 File Offset: 0x00140A18
		private Image IconRank
		{
			get
			{
				return this.m_IconRank = ((this.m_IconRank != null) ? this.m_IconRank : base.Manager.GetElement<Image>("IconRank"));
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x06009175 RID: 37237 RVA: 0x00142854 File Offset: 0x00140A54
		private Image IconLink
		{
			get
			{
				return this.m_IconLink = ((this.m_IconLink != null) ? this.m_IconLink : base.Manager.GetElement<Image>("IconLink"));
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x06009176 RID: 37238 RVA: 0x00142890 File Offset: 0x00140A90
		private Image IconPendulumScale
		{
			get
			{
				return this.m_IconPendulumScale = ((this.m_IconPendulumScale != null) ? this.m_IconPendulumScale : base.Manager.GetElement<Image>("IconPendulumScale"));
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06009177 RID: 37239 RVA: 0x001428CC File Offset: 0x00140ACC
		private TextMeshProUGUI TextLevel
		{
			get
			{
				return this.m_TextLevel = ((this.m_TextLevel != null) ? this.m_TextLevel : base.Manager.GetElement<TextMeshProUGUI>("TextLevel"));
			}
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x06009178 RID: 37240 RVA: 0x00142908 File Offset: 0x00140B08
		private TextMeshProUGUI TextRank
		{
			get
			{
				return this.m_TextRank = ((this.m_TextRank != null) ? this.m_TextRank : base.Manager.GetElement<TextMeshProUGUI>("TextRank"));
			}
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x06009179 RID: 37241 RVA: 0x00142944 File Offset: 0x00140B44
		private TextMeshProUGUI TextLink
		{
			get
			{
				return this.m_TextLink = ((this.m_TextLink != null) ? this.m_TextLink : base.Manager.GetElement<TextMeshProUGUI>("TextLink"));
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x0600917A RID: 37242 RVA: 0x00142980 File Offset: 0x00140B80
		private TextMeshProUGUI TextPendulumScale
		{
			get
			{
				return this.m_TextPendulumScale = ((this.m_TextPendulumScale != null) ? this.m_TextPendulumScale : base.Manager.GetElement<TextMeshProUGUI>("TextPendulumScale"));
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x0600917B RID: 37243 RVA: 0x001429BC File Offset: 0x00140BBC
		private GameObject PickupCursor
		{
			get
			{
				return this.m_PickupCursor = ((this.m_PickupCursor != null) ? this.m_PickupCursor : base.Manager.GetElement("PickupCursor"));
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x0600917C RID: 37244 RVA: 0x001429F8 File Offset: 0x00140BF8
		private TextMeshProUGUI TextPickupCursor
		{
			get
			{
				return this.m_TextPickupCursor = ((this.m_TextPickupCursor != null) ? this.m_TextPickupCursor : base.Manager.GetNestedElement<TextMeshProUGUI>("PickupCursor/Text"));
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x0600917D RID: 37245 RVA: 0x00142A34 File Offset: 0x00140C34
		private GameObject CardPoint
		{
			get
			{
				return this.m_CardPoint = ((this.m_CardPoint != null) ? this.m_CardPoint : base.Manager.GetElement("CardPointRoot"));
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x0600917E RID: 37246 RVA: 0x00142A70 File Offset: 0x00140C70
		private TextMeshProUGUI TextCardPoint
		{
			get
			{
				return this.m_TextCardPoint = ((this.m_TextCardPoint != null) ? this.m_TextCardPoint : base.Manager.GetElement<TextMeshProUGUI>("TextCardPointValue"));
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x0600917F RID: 37247 RVA: 0x00142AAC File Offset: 0x00140CAC
		// (set) Token: 0x06009180 RID: 37248 RVA: 0x00142AB4 File Offset: 0x00140CB4
		public Card Card
		{
			get
			{
				return this._card;
			}
			set
			{
				if (this._card == null || value.Id != this._card.Id)
				{
					this._card = value;
					this.SetIcons();
					this.Refresh();
				}
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x06009181 RID: 37249 RVA: 0x00142AE4 File Offset: 0x00140CE4
		public bool Refreshed
		{
			get
			{
				return this.ImageCardHandler.Refreshed;
			}
		}

		// Token: 0x06009182 RID: 37250 RVA: 0x00142AF4 File Offset: 0x00140CF4
		protected override void Awake()
		{
			this.manuallySetNavigation = false;
			base.Awake();
			this.child = base.transform.GetChild(0).GetComponent<RectTransform>();
			this.SetClickEvent(delegate
			{
				if (Program.instance.currentServant == Program.instance.deckEditor)
				{
					if (UserInput.gamepadType == UserInput.GamepadType.None)
					{
						if (!DeckEditor.UseMobileLayout)
						{
							Program.instance.deckEditor.GetUI<DeckEditorUI>().AddHistoryCard(this.Card.Id);
							this.ShowThisCard();
							return;
						}
						if (this.dragProcessing)
						{
							return;
						}
						AudioManager.PlaySE("SE_MENU_DECIDE", 1f);
						Program.instance.deckEditor.lastSelectedCardInDeck = this;
						Program.instance.deckEditor.ResponseRegion = DeckEditorUI.ResponseRegion.Deck;
						Program.instance.deckEditor.GetUI<DeckEditorUI>().ShowCardActionMenu();
						return;
					}
					else if (DeckEditor.condition == DeckEditor.Condition.EditDeck)
					{
						Program.instance.deckEditor.GetUI<DeckEditorUI>().RemoveCardWithAnimation(this);
						return;
					}
				}
				else if (Program.instance.currentServant == Program.instance.deckBrowser)
				{
					if (this.dragProcessing)
					{
						return;
					}
					AudioManager.PlaySE("SE_MENU_DECIDE", 1f);
					this.PickupClick();
				}
			});
			this.SetRightClickEvent(delegate
			{
				if (Program.instance.currentServant == Program.instance.deckEditor)
				{
					if (DeckEditor.condition != DeckEditor.Condition.ChangeSide)
					{
						Program.instance.deckEditor.GetUI<DeckEditorUI>().RemoveCardWithAnimation(this);
					}
					else
					{
						Program.instance.deckEditor.GetUI<DeckEditorUI>().CardChangeSide(this);
					}
					this.ShowThisCard();
				}
			});
			this.SetMiddleClickEvent(delegate
			{
				if (Program.instance.currentServant == Program.instance.deckEditor)
				{
					Program.instance.deckEditor.GetUI<DeckEditorUI>().AddCard(this.Card);
					this.ShowThisCard();
				}
			});
		}

		// Token: 0x06009183 RID: 37251 RVA: 0x00142B5C File Offset: 0x00140D5C
		protected override void OnSelect(bool playSE)
		{
			base.OnSelect(playSE);
			ColorContainerGraphic[] componentsInChildren = base.Manager.GetElement<Transform>("ImageCard").GetComponentsInChildren<ColorContainerGraphic>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetColor(ColorContainer.SelectMode.Selected, this.hovering ? ColorContainer.StatusMode.Enter : ColorContainer.StatusMode.Normal, base.Selectable.interactable);
			}
			this.ShowThisCard();
			if (Program.instance.currentServant == Program.instance.deckEditor)
			{
				Program.instance.deckEditor.lastSelectedCardInDeck = this;
				Program.instance.deckEditor.ResponseRegion = DeckEditorUI.ResponseRegion.Deck;
				return;
			}
			if (Program.instance.currentServant == Program.instance.deckBrowser)
			{
				Program.instance.deckBrowser.lastSelectedCardInDeck = this;
				Program.instance.deckBrowser.ResponseRegion = DeckBrowserUI.ResponseRegion.Deck;
			}
		}

		// Token: 0x06009184 RID: 37252 RVA: 0x00142C32 File Offset: 0x00140E32
		private void Refresh()
		{
			this.ImageCardHandler.SetCard(this.Card);
		}

		// Token: 0x06009185 RID: 37253 RVA: 0x00142C45 File Offset: 0x00140E45
		public void RefreshRarity(int code)
		{
			this.ImageCardHandler.RefreshRarity(code);
		}

		// Token: 0x06009186 RID: 37254 RVA: 0x00142C53 File Offset: 0x00140E53
		public void SetRegulationIcon()
		{
			this.IconLimit.sprite = TextureManager.container.GetCardRegulationIcon(this.Card.Id, DeckEditor.banlist);
		}

		// Token: 0x06009187 RID: 37255 RVA: 0x00142C7C File Offset: 0x00140E7C
		private void SetIcons()
		{
			this.SetRegulationIcon();
			Sprite attributeIcon = TextureManager.container.GetCardAttributeIcon(this.Card, false);
			this.IconAttribute.sprite = ((attributeIcon == null) ? TextureManager.container.typeNone : attributeIcon);
			Sprite spellTrapTypeIcon = TextureManager.container.GetCardSpellTrapTypeIcon(this.Card);
			this.IconSpellTrapType.sprite = ((spellTrapTypeIcon == null) ? TextureManager.container.typeNone : spellTrapTypeIcon);
			Sprite raceIcon = TextureManager.container.GetCardRaceIcon(this.Card);
			this.IconRace.sprite = ((raceIcon == null) ? TextureManager.container.typeNone : raceIcon);
			this.IconPool.sprite = TextureManager.container.GetCardPoolIcon(this.Card);
			this.TextLevel.text = this.Card.Level.ToString();
			this.TextRank.text = this.Card.Level.ToString();
			this.TextLink.text = this.Card.GetLinkCount().ToString();
			this.TextPendulumScale.text = this.Card.LScale.ToString();
			this.genesysPoint = OnlineService.GetGenesysPoint(this.Card.GetOriginalID());
			string text = this.genesysPoint.ToString();
			if (this.genesysPoint < 0)
			{
				text = "X";
			}
			this.TextCardPoint.text = text;
			this.TextCardPoint.color = OnlineService.GetGenesysPointColor(this.genesysPoint);
			this.RefreshIcons();
		}

		// Token: 0x06009188 RID: 37256 RVA: 0x00142E0C File Offset: 0x0014100C
		public void RefreshIcons()
		{
			if (Program.instance.currentServant == Program.instance.deckEditor)
			{
				this.IconAttribute.gameObject.SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail);
				this.IconSpellTrapType.gameObject.SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail);
				this.IconRace.gameObject.SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail);
				this.IconTuner.gameObject.SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail && this.Card.HasType(CardType.Tuner));
				Card.LevelType levelType = this.Card.GetLevelType();
				this.IconLevel.gameObject.SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail && this.Card.HasType(CardType.Monster) && levelType == Card.LevelType.Level);
				this.IconRank.gameObject.SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail && this.Card.HasType(CardType.Monster) && levelType == Card.LevelType.Rank);
				this.IconLink.gameObject.SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail && this.Card.HasType(CardType.Monster) && levelType == Card.LevelType.Link);
				this.IconPendulumScale.gameObject.SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Detail && this.Card.HasType(CardType.Pendulum));
				this.IconPool.gameObject.SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Pool);
				this.CardPoint.SetActive(DeckEditorUI.cardInfoType == DeckEditorUI.CardInfoType.Genesys);
				this.IconLimit.gameObject.SetActive(DeckEditorUI.cardInfoType != DeckEditorUI.CardInfoType.Genesys);
				return;
			}
			if (Program.instance.currentServant == Program.instance.deckBrowser)
			{
				this.IconAttribute.gameObject.SetActive(false);
				this.IconSpellTrapType.gameObject.SetActive(false);
				this.IconRace.gameObject.SetActive(false);
				this.IconTuner.gameObject.SetActive(false);
				this.IconLevel.gameObject.SetActive(false);
				this.IconRank.gameObject.SetActive(false);
				this.IconLink.gameObject.SetActive(false);
				this.IconPendulumScale.gameObject.SetActive(false);
				this.IconPool.gameObject.SetActive(false);
				this.CardPoint.SetActive(false);
			}
		}

		// Token: 0x06009189 RID: 37257 RVA: 0x00143072 File Offset: 0x00141272
		public void PlayBirthAnimation()
		{
			base.StartCoroutine(this.PlayBirthAnimationAsync());
		}

		// Token: 0x0600918A RID: 37258 RVA: 0x00143081 File Offset: 0x00141281
		private IEnumerator PlayBirthAnimationAsync()
		{
			yield return null;
			this.child.SetParent(Program.instance.ui_.transform, true);
			this.child.localScale = this.dragScale;
			this.child.DOScale(Vector3.one, 0.3f).SetEase(Ease.InQuart).OnComplete(delegate
			{
				this.child.SetParent(base.transform, true);
				this.child.localPosition = Vector3.zero;
				this.child.localScale = Vector3.one;
				this.child.localEulerAngles = Vector3.zero;
			});
			yield break;
		}

		// Token: 0x0600918B RID: 37259 RVA: 0x00143090 File Offset: 0x00141290
		public void LockPosition()
		{
			this.child.SetParent(this.deckView.TempView, true);
			base.StartCoroutine(this.AutoMoveToParent());
		}

		// Token: 0x0600918C RID: 37260 RVA: 0x001430B8 File Offset: 0x001412B8
		public void LockPosition(Vector3 position, Vector3 scale)
		{
			this.child.SetParent(Program.instance.ui_.transform, true);
			this.child.position = position;
			this.child.localScale = scale;
			base.StartCoroutine(this.AutoMoveToParent());
		}

		// Token: 0x0600918D RID: 37261 RVA: 0x00143105 File Offset: 0x00141305
		private IEnumerator AutoMoveToParent()
		{
			yield return null;
			ColorContainerGraphic[] componentsInChildren = this.child.GetComponentsInChildren<ColorContainerGraphic>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetColor(this.selected ? ColorContainer.SelectMode.Selected : ColorContainer.SelectMode.Unselected, ColorContainer.StatusMode.Normal, base.Selectable.interactable);
			}
			Vector3 position = base.transform.position;
			DOTween.Sequence().Append(this.child.DOMove(position, 0.1f, false).SetEase(Ease.OutCubic)).Join(this.child.DOScale(Vector3.one, 0.1f).SetEase(Ease.OutCubic))
				.OnComplete(delegate
				{
					this.child.SetParent(base.transform, true);
					this.child.localPosition = Vector3.zero;
					this.child.localScale = Vector3.one;
					this.child.localEulerAngles = Vector3.zero;
				});
			yield break;
		}

		// Token: 0x0600918E RID: 37262 RVA: 0x00143114 File Offset: 0x00141314
		public void MoveToParent(Vector3 position)
		{
			this.child.SetParent(Program.instance.ui_.transform, true);
			this.child.localScale = this.dragScale;
			this.child.position = position;
			base.StartCoroutine(this.AutoMoveToParent());
		}

		// Token: 0x0600918F RID: 37263 RVA: 0x00143168 File Offset: 0x00141368
		public void MoveToParentSequence(Vector3 position)
		{
			if (!DeckEditor.UseMobileLayout)
			{
				this.child.SetParent(Program.instance.ui_.transform, true);
				this.child.localScale = this.dragScale;
				this.child.position = position;
			}
			base.StartCoroutine(this.AutoMoveToParentSequence(position));
		}

		// Token: 0x06009190 RID: 37264 RVA: 0x001431C2 File Offset: 0x001413C2
		private IEnumerator AutoMoveToParentSequence(Vector3 position)
		{
			if (DeckEditor.UseMobileLayout)
			{
				this.child.gameObject.SetActive(false);
				yield return null;
				this.deckView.ScrollTo(this);
			}
			yield return null;
			if (DeckEditor.UseMobileLayout)
			{
				this.child.gameObject.SetActive(true);
				this.child.SetParent(Program.instance.ui_.transform, true);
				this.child.localScale = this.dragScale;
				this.child.position = position;
			}
			yield return null;
			ColorContainerGraphic[] componentsInChildren = this.child.GetComponentsInChildren<ColorContainerGraphic>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].SetColor(ColorContainer.SelectMode.Unselected, ColorContainer.StatusMode.Normal, base.Selectable.interactable);
			}
			Vector3 endPosition = base.transform.position;
			DOTween.Sequence().Append(this.child.DOMove(endPosition, 0.2f, false).SetEase(Ease.OutCubic)).Append(this.child.DOScale(Vector3.one, 0.2f).SetEase(Ease.InCubic))
				.OnComplete(delegate
				{
					this.child.SetParent(base.transform, true);
					this.child.localPosition = Vector3.zero;
					this.child.localScale = Vector3.one;
					this.child.localEulerAngles = Vector3.zero;
				});
			yield break;
		}

		// Token: 0x06009191 RID: 37265 RVA: 0x001431D8 File Offset: 0x001413D8
		public bool IsHovering()
		{
			return this.hovering;
		}

		// Token: 0x06009192 RID: 37266 RVA: 0x001431E0 File Offset: 0x001413E0
		private void ShowThisCard()
		{
			List<int> codes = this.deckView.GetAllCardCodes();
			if (Program.instance.currentServant == Program.instance.deckEditor)
			{
				Program.instance.deckEditor.GetUI<DeckEditorUI>().ShowDetail(codes, this.GetIndex());
				return;
			}
			if (Program.instance.currentServant == Program.instance.deckBrowser)
			{
				Program.instance.deckBrowser.GetUI<DeckBrowserUI>().ShowDetail(codes, this.GetIndex());
			}
		}

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06009193 RID: 37267 RVA: 0x00143266 File Offset: 0x00141466
		private static PickupCardSelection pickupCardSelection
		{
			get
			{
				return Program.instance.deckBrowser.GetUI<DeckBrowserUI>().PickupCardSelection;
			}
		}

		// Token: 0x06009194 RID: 37268 RVA: 0x0014327C File Offset: 0x0014147C
		public void PrePickThis(int index)
		{
			this.picked = true;
			this.pickupIndex = index;
			this.PickupCursor.SetActive(true);
			this.TextPickupCursor.text = (index + 1).ToString();
		}

		// Token: 0x06009195 RID: 37269 RVA: 0x001432B9 File Offset: 0x001414B9
		public void PickThisByIndex(int index)
		{
			this.PrePickThis(index);
			this.deckView.Pickup(this);
			SelectionButton_CardInDeck.pickupCardSelection.SetPickup(this.ImageCardHandler.card, this.pickupIndex, true);
		}

		// Token: 0x06009196 RID: 37270 RVA: 0x001432EA File Offset: 0x001414EA
		public void DepickupThis()
		{
			this.picked = false;
			this.pickupIndex = -1;
			this.PickupCursor.SetActive(false);
		}

		// Token: 0x06009197 RID: 37271 RVA: 0x00143308 File Offset: 0x00141508
		private void PickupClick()
		{
			Program.instance.deckBrowser.lastSelectedCardInDeck = this;
			this.ShowThisCard();
			if (this.picked)
			{
				SelectionButton_CardInDeck.pickupCardSelection.SetPickup(null, this.pickupIndex, true);
				this.DepickupThis();
				return;
			}
			this.PickThisByIndex(SelectionButton_CardInDeck.pickupCardSelection.GetPickupIndex());
		}

		// Token: 0x06009198 RID: 37272 RVA: 0x0014335C File Offset: 0x0014155C
		private int GetIndex()
		{
			int index = 0;
			List<SelectionButton_CardInDeck> cards = this.deckView.cards;
			for (int i = 0; i < cards.Count; i++)
			{
				if (cards[i] == this)
				{
					index = i;
					break;
				}
			}
			return index;
		}

		// Token: 0x06009199 RID: 37273 RVA: 0x0014339C File Offset: 0x0014159C
		private bool NeedResponseDrag()
		{
			return this.deckView.condition == DeckView.Condition.Editable || this.deckView.condition == DeckView.Condition.ChangeSide;
		}

		// Token: 0x0600919A RID: 37274 RVA: 0x001433BC File Offset: 0x001415BC
		public void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.deckView.ScrollRect.OnBeginDrag(eventData);
			this.dragStartPosition = eventData.position;
			this.dragProcessing = true;
			this.draging = !DeckEditor.UseMobileLayout;
			this.dragIni = false;
		}

		// Token: 0x0600919B RID: 37275 RVA: 0x0014340C File Offset: 0x0014160C
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
					this.dragTarget.GetChild(0).GetComponent<RawImage>().texture = this.ImageCardHandler.RawImage.texture;
					this.dragTarget.GetChild(0).GetComponent<RawImage>().material = this.ImageCardHandler.RawImage.material;
					this.dragIni = true;
					UIHover.HoveringLabel = string.Empty;
					Program.instance.deckEditor.GetUI<DeckEditorUI>().CardCollectionView.SetBookmarkDropArea(this.Card.Id);
					UserInput.Draging = true;
					CanvasGroup component = base.GetComponent<CanvasGroup>();
					component.blocksRaycasts = false;
					component.alpha = 0f;
				}
				Vector3 position;
				RectTransformUtility.ScreenPointToWorldPointInRectangle(this.dragTarget, eventData.position, eventData.enterEventCamera, out position);
				this.dragTarget.position = position;
				Vector3 anchoredPositon = this.dragTarget.anchoredPosition3D;
				anchoredPositon.z = -10f;
				this.dragTarget.anchoredPosition3D = anchoredPositon;
				return;
			}
			this.deckView.ScrollRect.OnDrag(eventData);
			this.draging = this.NeedResponseDrag() & this.NeedStartDrag(this.dragStartPosition, eventData.position);
		}

		// Token: 0x0600919C RID: 37276 RVA: 0x00143588 File Offset: 0x00141788
		public void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.deckView.ScrollRect.OnEndDrag(eventData);
			this.dragProcessing = false;
			UserInput.Draging = false;
			if (this.draging)
			{
				CanvasGroup component = base.GetComponent<CanvasGroup>();
				component.blocksRaycasts = true;
				component.alpha = 1f;
				this.dragTarget.gameObject.SetActive(false);
				Program.instance.deckEditor.GetUI<DeckEditorUI>().DragCardTo(this);
				this.deckView.HideDeckLocationTable();
			}
		}

		// Token: 0x0600919D RID: 37277 RVA: 0x0014360C File Offset: 0x0014180C
		private bool NeedStartDrag(Vector3 startPosition, Vector3 position)
		{
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

		// Token: 0x0600919E RID: 37278 RVA: 0x0014368C File Offset: 0x0014188C
		protected override int GetButtonsCount()
		{
			return this.deckView.GetDeckLocationCount(this.location);
		}

		// Token: 0x0600919F RID: 37279 RVA: 0x001436A0 File Offset: 0x001418A0
		protected override int GetColumnsCount()
		{
			return this.deckView.GetDeckLocationParent(this.location).GetComponent<GridLayoutGroup>().Size()
				.x;
		}

		// Token: 0x060091A0 RID: 37280 RVA: 0x001436D0 File Offset: 0x001418D0
		protected override void OnNavigation(AxisEventData eventData)
		{
			int selfIndex = base.transform.GetSiblingIndex();
			int count = this.GetButtonsCount();
			int columes = this.GetColumnsCount();
			if (columes == 0)
			{
				Debug.LogError("divide by zero");
				return;
			}
			int targetIndex = selfIndex + 1;
			if (eventData.moveDir == MoveDirection.Left)
			{
				if (selfIndex % columes == 0)
				{
					return;
				}
				targetIndex = selfIndex - 1;
			}
			else if (eventData.moveDir == MoveDirection.Right)
			{
				if (selfIndex % columes == columes - 1 || selfIndex == count - 1)
				{
					if (Program.instance.currentServant == Program.instance.deckEditor)
					{
						Program.instance.deckEditor.SelectNearestCollectionViewItem(base.transform.position);
						return;
					}
					if (Program.instance.currentServant == Program.instance.deckBrowser)
					{
						Program.instance.deckBrowser.GetUI<DeckBrowserUI>().PickupCardSelection.Select();
					}
					return;
				}
			}
			else if (eventData.moveDir == MoveDirection.Up)
			{
				targetIndex = selfIndex - columes;
				if (targetIndex < 0)
				{
					this.SelectTarget(this.GetNavivationTarget(eventData.moveDir));
					return;
				}
			}
			else if (eventData.moveDir == MoveDirection.Down)
			{
				targetIndex = selfIndex + columes;
				if (targetIndex >= count)
				{
					if (this.location != DeckView.DeckLocation.SideDeck || Tools.InLastRow(selfIndex, count, columes))
					{
						this.SelectTarget(this.GetNavivationTarget(eventData.moveDir));
						return;
					}
					targetIndex = count - 1;
				}
			}
			for (int i = 0; i < base.transform.parent.childCount; i++)
			{
				Transform child = base.transform.parent.GetChild(i);
				if (child.gameObject.activeSelf)
				{
					int buttonIndex = child.GetComponent<SelectionButton>().index;
					if (buttonIndex < 0)
					{
						buttonIndex = i;
					}
					if (buttonIndex == targetIndex)
					{
						UserInput.NextSelectionIsAxis = true;
						EventSystem.current.SetSelectedGameObject(base.transform.parent.GetChild(i).gameObject);
						return;
					}
				}
			}
		}

		// Token: 0x060091A1 RID: 37281 RVA: 0x0014388C File Offset: 0x00141A8C
		private SelectionButton_CardInDeck GetNavivationTarget(MoveDirection direction)
		{
			if (direction == MoveDirection.Up)
			{
				if (this.location == DeckView.DeckLocation.MainDeck)
				{
					return null;
				}
				if (this.location == DeckView.DeckLocation.ExtraDeck)
				{
					return this.deckView.GetNavigationTarget(DeckView.DeckLocation.MainDeck, direction, base.transform.position);
				}
				if (this.location == DeckView.DeckLocation.SideDeck)
				{
					return this.deckView.GetNavigationTarget(DeckView.DeckLocation.ExtraDeck, direction, base.transform.position);
				}
			}
			else if (direction == MoveDirection.Down)
			{
				if (this.location == DeckView.DeckLocation.MainDeck)
				{
					return this.deckView.GetNavigationTarget(DeckView.DeckLocation.ExtraDeck, direction, base.transform.position);
				}
				if (this.location == DeckView.DeckLocation.ExtraDeck)
				{
					return this.deckView.GetNavigationTarget(DeckView.DeckLocation.SideDeck, direction, base.transform.position);
				}
				DeckView.DeckLocation deckLocation = this.location;
				return null;
			}
			return null;
		}

		// Token: 0x060091A2 RID: 37282 RVA: 0x00143940 File Offset: 0x00141B40
		private void SelectTarget(SelectionButton_CardInDeck target)
		{
			if (target == null)
			{
				return;
			}
			UserInput.NextSelectionIsAxis = true;
			target.GetSelectable().Select();
		}

		// Token: 0x0400D01F RID: 53279
		private const string LABEL_IMAGE_CARD = "ImageCard";

		// Token: 0x0400D020 RID: 53280
		private CardRawImageHandler m_ImageCardHandler;

		// Token: 0x0400D021 RID: 53281
		private const string LABEL_ICON_LIMIT = "IconLimit";

		// Token: 0x0400D022 RID: 53282
		private Image m_IconLimit;

		// Token: 0x0400D023 RID: 53283
		private const string LABEL_ICON_ATTRIBUTE = "IconAttribute";

		// Token: 0x0400D024 RID: 53284
		private Image m_IconAttribute;

		// Token: 0x0400D025 RID: 53285
		private const string LABEL_ICON_SPELL_TRAP_TYPE = "IconSpellTrapType";

		// Token: 0x0400D026 RID: 53286
		private Image m_IconSpellTrapType;

		// Token: 0x0400D027 RID: 53287
		private const string LABEL_ICON_RACE = "IconRace";

		// Token: 0x0400D028 RID: 53288
		private Image m_IconRace;

		// Token: 0x0400D029 RID: 53289
		private const string LABEL_ICON_POOL = "IconPool";

		// Token: 0x0400D02A RID: 53290
		private Image m_IconPool;

		// Token: 0x0400D02B RID: 53291
		private const string LABEL_ICON_TUNER = "IconTuner";

		// Token: 0x0400D02C RID: 53292
		private Image m_IconTuner;

		// Token: 0x0400D02D RID: 53293
		private const string LABEL_ICON_LEVEL = "IconLevel";

		// Token: 0x0400D02E RID: 53294
		private Image m_IconLevel;

		// Token: 0x0400D02F RID: 53295
		private const string LABEL_ICON_RANK = "IconRank";

		// Token: 0x0400D030 RID: 53296
		private Image m_IconRank;

		// Token: 0x0400D031 RID: 53297
		private const string LABEL_ICON_LINK = "IconLink";

		// Token: 0x0400D032 RID: 53298
		private Image m_IconLink;

		// Token: 0x0400D033 RID: 53299
		private const string LABEL_ICON_PENDULUM_SCALE = "IconPendulumScale";

		// Token: 0x0400D034 RID: 53300
		private Image m_IconPendulumScale;

		// Token: 0x0400D035 RID: 53301
		private const string LABEL_TEXT_LEVEL = "TextLevel";

		// Token: 0x0400D036 RID: 53302
		private TextMeshProUGUI m_TextLevel;

		// Token: 0x0400D037 RID: 53303
		private const string LABEL_TEXT_RANK = "TextRank";

		// Token: 0x0400D038 RID: 53304
		private TextMeshProUGUI m_TextRank;

		// Token: 0x0400D039 RID: 53305
		private const string LABEL_TEXT_LINK = "TextLink";

		// Token: 0x0400D03A RID: 53306
		private TextMeshProUGUI m_TextLink;

		// Token: 0x0400D03B RID: 53307
		private const string LABEL_TEXT_PENDULUM_SCALE = "TextPendulumScale";

		// Token: 0x0400D03C RID: 53308
		private TextMeshProUGUI m_TextPendulumScale;

		// Token: 0x0400D03D RID: 53309
		private const string LABEL_GO_PICKUP_CURSOR = "PickupCursor";

		// Token: 0x0400D03E RID: 53310
		private GameObject m_PickupCursor;

		// Token: 0x0400D03F RID: 53311
		private const string LABEL_TXT_PICKUP_CURSOR = "PickupCursor/Text";

		// Token: 0x0400D040 RID: 53312
		private TextMeshProUGUI m_TextPickupCursor;

		// Token: 0x0400D041 RID: 53313
		private const string LABEL_GO_CARD_POINT = "CardPointRoot";

		// Token: 0x0400D042 RID: 53314
		private GameObject m_CardPoint;

		// Token: 0x0400D043 RID: 53315
		private const string LABEL_TXT_CARD_POINT = "TextCardPointValue";

		// Token: 0x0400D044 RID: 53316
		private TextMeshProUGUI m_TextCardPoint;

		// Token: 0x0400D045 RID: 53317
		[Header("SelectionButton CardInDeck")]
		[HideInInspector]
		public DeckView deckView;

		// Token: 0x0400D046 RID: 53318
		private Card _card;

		// Token: 0x0400D047 RID: 53319
		public DeckView.DeckLocation location;

		// Token: 0x0400D048 RID: 53320
		private Vector3 dragScale = new Vector3(1.7f, 1.7f, 1f);

		// Token: 0x0400D049 RID: 53321
		private RectTransform child;

		// Token: 0x0400D04A RID: 53322
		public int genesysPoint;

		// Token: 0x0400D04B RID: 53323
		public bool picked;

		// Token: 0x0400D04C RID: 53324
		public int pickupIndex = -1;

		// Token: 0x0400D04D RID: 53325
		private RectTransform dragTarget;

		// Token: 0x0400D04E RID: 53326
		private Vector2 dragStartPosition;

		// Token: 0x0400D04F RID: 53327
		private bool dragProcessing;

		// Token: 0x0400D050 RID: 53328
		private bool draging;

		// Token: 0x0400D051 RID: 53329
		private bool dragIni;

		// Token: 0x0400D052 RID: 53330
		private Vector3 lastDragStartPosition;

		// Token: 0x0400D053 RID: 53331
		private bool diffYOverLimit;
	}
}
