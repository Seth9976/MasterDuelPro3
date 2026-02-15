using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using MDPro3.Duel.YGOSharp;
using MDPro3.Net;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001423 RID: 5155
	public class DeckView : UIWidget
	{
		// Token: 0x17001313 RID: 4883
		// (get) Token: 0x0600956B RID: 38251 RVA: 0x00157728 File Offset: 0x00155928
		protected DoTweenManager TweenLoading
		{
			get
			{
				return this.m_TweenLoading = ((this.m_TweenLoading != null) ? this.m_TweenLoading : base.Manager.GetElement<DoTweenManager>("Loading"));
			}
		}

		// Token: 0x17001314 RID: 4884
		// (get) Token: 0x0600956C RID: 38252 RVA: 0x00157764 File Offset: 0x00155964
		protected CanvasGroup Viewport
		{
			get
			{
				return this.m_Viewport = ((this.m_Viewport != null) ? this.m_Viewport : base.Manager.GetElement<CanvasGroup>("Viewport"));
			}
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x0600956D RID: 38253 RVA: 0x001577A0 File Offset: 0x001559A0
		protected SelectionButton ButtonNoItem
		{
			get
			{
				return this.m_ButtonNoItem = ((this.m_ButtonNoItem != null) ? this.m_ButtonNoItem : base.Manager.GetElement<SelectionButton>("NoItemButton"));
			}
		}

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x0600956E RID: 38254 RVA: 0x001577DC File Offset: 0x001559DC
		private TextMeshProUGUI TextNoItem
		{
			get
			{
				return this.m_TextNoItem = ((this.m_TextNoItem != null) ? this.m_TextNoItem : base.Manager.GetElement<TextMeshProUGUI>("NoItemText"));
			}
		}

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x0600956F RID: 38255 RVA: 0x00157818 File Offset: 0x00155A18
		protected GamepadCursor CursorWindowSelect
		{
			get
			{
				return this.m_CursorWindowSelect = ((this.m_CursorWindowSelect != null) ? this.m_CursorWindowSelect : base.Manager.GetElement<GamepadCursor>("CursorWindowSelect"));
			}
		}

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06009570 RID: 38256 RVA: 0x00157854 File Offset: 0x00155A54
		public ScrollRect ScrollRect
		{
			get
			{
				return this.m_ScrollRect = ((this.m_ScrollRect != null) ? this.m_ScrollRect : base.Manager.GetElement<ScrollRect>("ScrollRect"));
			}
		}

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06009571 RID: 38257 RVA: 0x00157890 File Offset: 0x00155A90
		protected UIScrollToSelection AutoScroll
		{
			get
			{
				return this.m_AutoScroll = ((this.m_AutoScroll != null) ? this.m_AutoScroll : this.ScrollRect.GetComponent<UIScrollToSelection>());
			}
		}

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06009572 RID: 38258 RVA: 0x001578C8 File Offset: 0x00155AC8
		public RectTransform TempView
		{
			get
			{
				return this.m_TempView = ((this.m_TempView != null) ? this.m_TempView : base.Manager.GetElement<RectTransform>("TempView"));
			}
		}

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06009573 RID: 38259 RVA: 0x00157904 File Offset: 0x00155B04
		protected RectTransform HeaderArea
		{
			get
			{
				return this.m_HeaderArea = ((this.m_HeaderArea != null) ? this.m_HeaderArea : base.Manager.GetElement<RectTransform>("HeaderArea"));
			}
		}

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06009574 RID: 38260 RVA: 0x00157940 File Offset: 0x00155B40
		protected TextMeshProUGUI TextDeckName
		{
			get
			{
				return this.m_TextDeckName = ((this.m_TextDeckName != null) ? this.m_TextDeckName : base.Manager.GetNestedElement<TextMeshProUGUI>("HeaderArea/DeckNameText"));
			}
		}

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06009575 RID: 38261 RVA: 0x0015797C File Offset: 0x00155B7C
		protected GameObject NameAreaGroup
		{
			get
			{
				return this.m_NameAreaGroup = ((this.m_NameAreaGroup != null) ? this.m_NameAreaGroup : base.Manager.GetNestedElement("HeaderArea/NameAreaGroup"));
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06009576 RID: 38262 RVA: 0x001579B8 File Offset: 0x00155BB8
		protected TMP_InputField InputDeckName
		{
			get
			{
				return this.m_InputDeckName = ((this.m_InputDeckName != null) ? this.m_InputDeckName : base.Manager.GetNestedElement<TMP_InputField>("HeaderArea/InputField"));
			}
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06009577 RID: 38263 RVA: 0x001579F4 File Offset: 0x00155BF4
		public SelectionButton ButtonDeck
		{
			get
			{
				return this.m_ButtonDeck = ((this.m_ButtonDeck != null) ? this.m_ButtonDeck : base.Manager.GetNestedElement<SelectionButton>("HeaderArea/ButtonDeck"));
			}
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06009578 RID: 38264 RVA: 0x00157A30 File Offset: 0x00155C30
		protected Image IconDeck
		{
			get
			{
				return this.m_IconDeck = ((this.m_IconDeck != null) ? this.m_IconDeck : base.Manager.GetNestedElement<Image>("HeaderArea/IconDeck"));
			}
		}

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06009579 RID: 38265 RVA: 0x00157A6C File Offset: 0x00155C6C
		protected UIHover MainDeckView
		{
			get
			{
				return this.m_MainDeckView = ((this.m_MainDeckView != null) ? this.m_MainDeckView : base.Manager.GetElement<UIHover>("MainDeckView"));
			}
		}

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x0600957A RID: 38266 RVA: 0x00157AA8 File Offset: 0x00155CA8
		protected TextMeshProUGUI TextMainDeckCardNum
		{
			get
			{
				return this.m_TextMainDeckCardNum = ((this.m_TextMainDeckCardNum != null) ? this.m_TextMainDeckCardNum : base.Manager.GetNestedElement<TextMeshProUGUI>("MainDeckView/TextMainDeckCardNum"));
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x0600957B RID: 38267 RVA: 0x00157AE4 File Offset: 0x00155CE4
		protected TextMeshProUGUI TextMainDeckMonsterNum
		{
			get
			{
				return this.m_TextMainDeckMonsterNum = ((this.m_TextMainDeckMonsterNum != null) ? this.m_TextMainDeckMonsterNum : base.Manager.GetNestedElement<TextMeshProUGUI>("MainDeckView/TextMainDeckMonsterNum"));
			}
		}

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x0600957C RID: 38268 RVA: 0x00157B20 File Offset: 0x00155D20
		protected TextMeshProUGUI TextMainDeckSpellNum
		{
			get
			{
				return this.m_TextMainDeckSpellNum = ((this.m_TextMainDeckSpellNum != null) ? this.m_TextMainDeckSpellNum : base.Manager.GetNestedElement<TextMeshProUGUI>("MainDeckView/TextMainDeckSpellNum"));
			}
		}

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x0600957D RID: 38269 RVA: 0x00157B5C File Offset: 0x00155D5C
		protected TextMeshProUGUI TextMainDeckTrapNum
		{
			get
			{
				return this.m_TextMainDeckTrapNum = ((this.m_TextMainDeckTrapNum != null) ? this.m_TextMainDeckTrapNum : base.Manager.GetNestedElement<TextMeshProUGUI>("MainDeckView/TextMainDeckTrapNum"));
			}
		}

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x0600957E RID: 38270 RVA: 0x00157B98 File Offset: 0x00155D98
		protected GridLayoutGroup MainDeckContent
		{
			get
			{
				return this.m_MainDeckContent = ((this.m_MainDeckContent != null) ? this.m_MainDeckContent : base.Manager.GetNestedElement<GridLayoutGroup>("MainDeckView/MainDeckContent"));
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x0600957F RID: 38271 RVA: 0x00157BD4 File Offset: 0x00155DD4
		protected GameObject Template
		{
			get
			{
				return this.m_Template = ((this.m_Template != null) ? this.m_Template : base.Manager.GetNestedElement("MainDeckView/template"));
			}
		}

		// Token: 0x17001328 RID: 4904
		// (get) Token: 0x06009580 RID: 38272 RVA: 0x00157C10 File Offset: 0x00155E10
		protected GameObject MainDeckGenesys
		{
			get
			{
				return this.m_MainDeckGenesys = ((this.m_MainDeckGenesys != null) ? this.m_MainDeckGenesys : base.Manager.GetNestedElement("MainDeckView/TextMainDeckGenesys"));
			}
		}

		// Token: 0x17001329 RID: 4905
		// (get) Token: 0x06009581 RID: 38273 RVA: 0x00157C4C File Offset: 0x00155E4C
		protected TextMeshProUGUI TextMainDeckGP
		{
			get
			{
				return this.m_TextMainDeckGP = ((this.m_TextMainDeckGP != null) ? this.m_TextMainDeckGP : base.Manager.GetNestedElement<TextMeshProUGUI>("MainDeckView/TextMainDeckGP"));
			}
		}

		// Token: 0x1700132A RID: 4906
		// (get) Token: 0x06009582 RID: 38274 RVA: 0x00157C88 File Offset: 0x00155E88
		protected UIHover ExtraDeckView
		{
			get
			{
				return this.m_ExtraDeckView = ((this.m_ExtraDeckView != null) ? this.m_ExtraDeckView : base.Manager.GetElement<UIHover>("ExtraDeckView"));
			}
		}

		// Token: 0x1700132B RID: 4907
		// (get) Token: 0x06009583 RID: 38275 RVA: 0x00157CC4 File Offset: 0x00155EC4
		protected TextMeshProUGUI TextExtraDeckCardNum
		{
			get
			{
				return this.m_TextExtraDeckCardNum = ((this.m_TextExtraDeckCardNum != null) ? this.m_TextExtraDeckCardNum : base.Manager.GetNestedElement<TextMeshProUGUI>("ExtraDeckView/TextExtraDeckCardNum"));
			}
		}

		// Token: 0x1700132C RID: 4908
		// (get) Token: 0x06009584 RID: 38276 RVA: 0x00157D00 File Offset: 0x00155F00
		protected TextMeshProUGUI TextExtraDeckFusionNum
		{
			get
			{
				return this.m_TextExtraDeckFusionNum = ((this.m_TextExtraDeckFusionNum != null) ? this.m_TextExtraDeckFusionNum : base.Manager.GetNestedElement<TextMeshProUGUI>("ExtraDeckView/TextExtraDeckFusionNum"));
			}
		}

		// Token: 0x1700132D RID: 4909
		// (get) Token: 0x06009585 RID: 38277 RVA: 0x00157D3C File Offset: 0x00155F3C
		protected TextMeshProUGUI TextExtraDeckSynchroNum
		{
			get
			{
				return this.m_TextExtraDeckSynchroNum = ((this.m_TextExtraDeckSynchroNum != null) ? this.m_TextExtraDeckSynchroNum : base.Manager.GetNestedElement<TextMeshProUGUI>("ExtraDeckView/TextExtraDeckSynchroNum"));
			}
		}

		// Token: 0x1700132E RID: 4910
		// (get) Token: 0x06009586 RID: 38278 RVA: 0x00157D78 File Offset: 0x00155F78
		protected TextMeshProUGUI TextExtraDeckXyzNum
		{
			get
			{
				return this.m_TextExtraDeckXyzNum = ((this.m_TextExtraDeckXyzNum != null) ? this.m_TextExtraDeckXyzNum : base.Manager.GetNestedElement<TextMeshProUGUI>("ExtraDeckView/TextExtraDeckXyzNum"));
			}
		}

		// Token: 0x1700132F RID: 4911
		// (get) Token: 0x06009587 RID: 38279 RVA: 0x00157DB4 File Offset: 0x00155FB4
		protected TextMeshProUGUI TextExtraDeckLinkNum
		{
			get
			{
				return this.m_TextExtraDeckLinkNum = ((this.m_TextExtraDeckLinkNum != null) ? this.m_TextExtraDeckLinkNum : base.Manager.GetNestedElement<TextMeshProUGUI>("ExtraDeckView/TextExtraDeckLinkNum"));
			}
		}

		// Token: 0x17001330 RID: 4912
		// (get) Token: 0x06009588 RID: 38280 RVA: 0x00157DF0 File Offset: 0x00155FF0
		protected GridLayoutGroup ExtraDeckContent
		{
			get
			{
				return this.m_ExtraDeckContent = ((this.m_ExtraDeckContent != null) ? this.m_ExtraDeckContent : base.Manager.GetNestedElement<GridLayoutGroup>("ExtraDeckView/ExtraDeckContent"));
			}
		}

		// Token: 0x17001331 RID: 4913
		// (get) Token: 0x06009589 RID: 38281 RVA: 0x00157E2C File Offset: 0x0015602C
		protected GameObject ExtraDeckGenesys
		{
			get
			{
				return this.m_ExtraDeckGenesys = ((this.m_ExtraDeckGenesys != null) ? this.m_ExtraDeckGenesys : base.Manager.GetNestedElement("ExtraDeckView/TextExtraDeckGenesys"));
			}
		}

		// Token: 0x17001332 RID: 4914
		// (get) Token: 0x0600958A RID: 38282 RVA: 0x00157E68 File Offset: 0x00156068
		protected TextMeshProUGUI TextExtraDeckGP
		{
			get
			{
				return this.m_TextExtraDeckGP = ((this.m_TextExtraDeckGP != null) ? this.m_TextExtraDeckGP : base.Manager.GetNestedElement<TextMeshProUGUI>("ExtraDeckView/TextExtraDeckGP"));
			}
		}

		// Token: 0x17001333 RID: 4915
		// (get) Token: 0x0600958B RID: 38283 RVA: 0x00157EA4 File Offset: 0x001560A4
		protected UIHover SideDeckView
		{
			get
			{
				return this.m_SideDeckView = ((this.m_SideDeckView != null) ? this.m_SideDeckView : base.Manager.GetElement<UIHover>("SideDeckView"));
			}
		}

		// Token: 0x17001334 RID: 4916
		// (get) Token: 0x0600958C RID: 38284 RVA: 0x00157EE0 File Offset: 0x001560E0
		protected TextMeshProUGUI TextSideDeckCardNum
		{
			get
			{
				return this.m_TextSideDeckCardNum = ((this.m_TextSideDeckCardNum != null) ? this.m_TextSideDeckCardNum : base.Manager.GetNestedElement<TextMeshProUGUI>("SideDeckView/TextSideDeckCardNum"));
			}
		}

		// Token: 0x17001335 RID: 4917
		// (get) Token: 0x0600958D RID: 38285 RVA: 0x00157F1C File Offset: 0x0015611C
		protected TextMeshProUGUI TextSideDeckMonsterNum
		{
			get
			{
				return this.m_TextSideDeckMonsterNum = ((this.m_TextSideDeckMonsterNum != null) ? this.m_TextSideDeckMonsterNum : base.Manager.GetNestedElement<TextMeshProUGUI>("SideDeckView/TextSideDeckMonsterNum"));
			}
		}

		// Token: 0x17001336 RID: 4918
		// (get) Token: 0x0600958E RID: 38286 RVA: 0x00157F58 File Offset: 0x00156158
		protected TextMeshProUGUI TextSideDeckSpellNum
		{
			get
			{
				return this.m_TextSideDeckSpellNum = ((this.m_TextSideDeckSpellNum != null) ? this.m_TextSideDeckSpellNum : base.Manager.GetNestedElement<TextMeshProUGUI>("SideDeckView/TextSideDeckSpellNum"));
			}
		}

		// Token: 0x17001337 RID: 4919
		// (get) Token: 0x0600958F RID: 38287 RVA: 0x00157F94 File Offset: 0x00156194
		protected TextMeshProUGUI TextSideDeckTrapNum
		{
			get
			{
				return this.m_TextSideDeckTrapNum = ((this.m_TextSideDeckTrapNum != null) ? this.m_TextSideDeckTrapNum : base.Manager.GetNestedElement<TextMeshProUGUI>("SideDeckView/TextSideDeckTrapNum"));
			}
		}

		// Token: 0x17001338 RID: 4920
		// (get) Token: 0x06009590 RID: 38288 RVA: 0x00157FD0 File Offset: 0x001561D0
		protected GridLayoutGroup SideDeckContent
		{
			get
			{
				return this.m_SideDeckContent = ((this.m_SideDeckContent != null) ? this.m_SideDeckContent : base.Manager.GetNestedElement<GridLayoutGroup>("SideDeckView/SideDeckContent"));
			}
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06009591 RID: 38289 RVA: 0x0015800C File Offset: 0x0015620C
		protected GameObject SideDeckGenesys
		{
			get
			{
				return this.m_SideDeckGenesys = ((this.m_SideDeckGenesys != null) ? this.m_SideDeckGenesys : base.Manager.GetNestedElement("SideDeckView/TextSideDeckGenesys"));
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x06009592 RID: 38290 RVA: 0x00158048 File Offset: 0x00156248
		protected TextMeshProUGUI TextSideDeckGP
		{
			get
			{
				return this.m_TextSideDeckGP = ((this.m_TextSideDeckGP != null) ? this.m_TextSideDeckGP : base.Manager.GetNestedElement<TextMeshProUGUI>("SideDeckView/TextSideDeckGP"));
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x06009593 RID: 38291 RVA: 0x00158084 File Offset: 0x00156284
		// (set) Token: 0x06009594 RID: 38292 RVA: 0x0015808C File Offset: 0x0015628C
		public Deck Deck { get; set; }

		// Token: 0x06009595 RID: 38293 RVA: 0x00158098 File Offset: 0x00156298
		public void PrintDeck(Deck deck, string deckName, DeckView.Condition condition)
		{
			this.Deck = deck;
			this.deckNameWithType = deckName;
			if (Path.GetFileName(deckName) == deckName)
			{
				this.deckType = string.Empty;
			}
			else
			{
				this.deckType = Path.GetFileName(Path.GetDirectoryName(deckName));
			}
			this.deckFileName = Path.GetFileName(deckName);
			this.condition = condition;
			this.SetCondition(condition);
			this.InputDeckName.text = this.deckFileName;
			this.TextDeckName.text = this.deckFileName;
			this.LoadDeckCaseAsync(deck.Case);
			base.StartCoroutine(this.PrintDeckAsync());
			if (Program.instance.currentServant == Program.instance.deckEditor)
			{
				this.SetCardInfoTypeInternal(DeckEditorUI.cardInfoType);
			}
		}

		// Token: 0x06009596 RID: 38294 RVA: 0x0015815C File Offset: 0x0015635C
		private async UniTask LoadDeckCaseAsync(int deckCase)
		{
			Sprite icon = await Program.items.LoadDeckCaseIconAsync(deckCase, string.Empty);
			if (base.gameObject != null)
			{
				this.IconDeck.sprite = icon;
			}
		}

		// Token: 0x06009597 RID: 38295 RVA: 0x001581A7 File Offset: 0x001563A7
		public void SetDirty(bool dirty)
		{
			this.needSave = dirty;
		}

		// Token: 0x06009598 RID: 38296 RVA: 0x001581B0 File Offset: 0x001563B0
		public bool GetDirty()
		{
			return (this.condition == DeckView.Condition.Editable && this.InputDeckName.text != this.deckFileName) || this.needSave;
		}

		// Token: 0x06009599 RID: 38297 RVA: 0x001581DA File Offset: 0x001563DA
		public string GetDeckName()
		{
			if (this.condition == DeckView.Condition.Editable)
			{
				return this.InputDeckName.text;
			}
			return this.deckFileName;
		}

		// Token: 0x0600959A RID: 38298 RVA: 0x001581F8 File Offset: 0x001563F8
		public void SetCondition(DeckView.Condition condition)
		{
			this.condition = condition;
			this.InputDeckName.gameObject.SetActive(condition == DeckView.Condition.Editable);
			this.TextDeckName.gameObject.SetActive(condition > DeckView.Condition.Editable);
			if (condition == DeckView.Condition.Pickup)
			{
				this.ButtonDeck.gameObject.SetActive(false);
			}
		}

		// Token: 0x0600959B RID: 38299 RVA: 0x0015824C File Offset: 0x0015644C
		public RectTransform GetDeckLocationParent(DeckView.DeckLocation location)
		{
			switch (location)
			{
			case DeckView.DeckLocation.MainDeck:
				return this.MainDeckContent.GetComponent<RectTransform>();
			case DeckView.DeckLocation.ExtraDeck:
				return this.ExtraDeckContent.GetComponent<RectTransform>();
			case DeckView.DeckLocation.SideDeck:
				return this.SideDeckContent.GetComponent<RectTransform>();
			}
			return null;
		}

		// Token: 0x0600959C RID: 38300 RVA: 0x001582A0 File Offset: 0x001564A0
		public int GetCardCount(int code)
		{
			int count = 0;
			Card data = CardsManager.Get(code, false);
			if (data == null)
			{
				return count;
			}
			foreach (SelectionButton_CardInDeck card in this.cards)
			{
				if (card.Card != null && card.Card.Id != 0)
				{
					if (data.Alias == 0)
					{
						if (card.Card.Id == code || card.Card.Alias == code)
						{
							count++;
						}
					}
					else if (card.Card.Id == data.Alias || card.Card.Alias == data.Alias)
					{
						count++;
					}
				}
			}
			return count;
		}

		// Token: 0x0600959D RID: 38301 RVA: 0x00158368 File Offset: 0x00156568
		public bool AddCard(Card data, bool playMoveAnimation, bool playBirthAnimation)
		{
			if (!this.deckLoaded)
			{
				return false;
			}
			if (!this.CanEditCard())
			{
				return false;
			}
			if (!this.CanAddCard(data.Id))
			{
				return false;
			}
			AudioManager.PlaySE("SE_DECK_PLUS", 1f);
			DeckView.DeckLocation targetLocaltion = DeckView.DeckLocation.SideDeck;
			if (data.IsExtraCard())
			{
				if (this.GetDeckLocationCount(DeckView.DeckLocation.ExtraDeck) < 15)
				{
					targetLocaltion = DeckView.DeckLocation.ExtraDeck;
				}
			}
			else if (this.GetDeckLocationCount(DeckView.DeckLocation.MainDeck) < 60)
			{
				targetLocaltion = DeckView.DeckLocation.MainDeck;
			}
			this.AddCard(data, targetLocaltion, playMoveAnimation, playBirthAnimation);
			return true;
		}

		// Token: 0x0600959E RID: 38302 RVA: 0x001583DC File Offset: 0x001565DC
		public bool AddCardFromPosition(Card data, Vector3 position)
		{
			if (!this.deckLoaded)
			{
				return false;
			}
			if (!this.CanEditCard())
			{
				return false;
			}
			this.SortCards();
			SelectionButton_CardInDeck hoverCard = null;
			foreach (SelectionButton_CardInDeck card in this.cards)
			{
				if (card.IsHovering())
				{
					hoverCard = card;
					break;
				}
			}
			DeckView.DeckLocation location;
			if (hoverCard == null)
			{
				location = this.GetHoveredLocation();
			}
			else
			{
				location = hoverCard.location;
			}
			if (location == DeckView.DeckLocation.All || !this.CanSwitchPosition(data, location))
			{
				return false;
			}
			if (!this.CanAddCard(data.Id))
			{
				return false;
			}
			if (hoverCard == null)
			{
				this.AddCard(data, location, true, false).MoveToParent(position);
			}
			else
			{
				int siblingIndex = hoverCard.transform.GetSiblingIndex();
				SelectionButton_CardInDeck added = this.AddCard(data, location, false, false);
				foreach (SelectionButton_CardInDeck card2 in this.cards)
				{
					if (card2.location == location && card2 != added)
					{
						card2.LockPosition();
					}
				}
				added.LockPosition(position, this.dragCardScale);
				added.transform.SetSiblingIndex(siblingIndex);
			}
			return true;
		}

		// Token: 0x0600959F RID: 38303 RVA: 0x00158538 File Offset: 0x00156738
		public bool AddCardFromPositionWithSequence(Card data, Vector3 position)
		{
			if (!this.deckLoaded)
			{
				return false;
			}
			if (!this.CanEditCard())
			{
				return false;
			}
			if (!this.CanAddCard(data.Id))
			{
				return false;
			}
			this.SortCards();
			AudioManager.PlaySE("SE_DECK_PLUS", 1f);
			DeckView.DeckLocation targetLocaltion = DeckView.DeckLocation.SideDeck;
			if (data.IsExtraCard())
			{
				if (this.GetDeckLocationCount(DeckView.DeckLocation.ExtraDeck) < 15)
				{
					targetLocaltion = DeckView.DeckLocation.ExtraDeck;
				}
			}
			else if (this.GetDeckLocationCount(DeckView.DeckLocation.MainDeck) < 60)
			{
				targetLocaltion = DeckView.DeckLocation.MainDeck;
			}
			this.AddCard(data, targetLocaltion, !DeckEditor.UseMobileLayout, false).MoveToParentSequence(position);
			return true;
		}

		// Token: 0x060095A0 RID: 38304 RVA: 0x001585BC File Offset: 0x001567BC
		public SelectionButton_CardInDeck GetHoveringCard()
		{
			foreach (SelectionButton_CardInDeck card in this.cards)
			{
				if (card.IsHovering())
				{
					return card;
				}
			}
			return null;
		}

		// Token: 0x060095A1 RID: 38305 RVA: 0x00158618 File Offset: 0x00156818
		public void MoveCardToLocation(SelectionButton_CardInDeck card, DeckView.DeckLocation location, Vector3 position)
		{
			if (!this.deckLoaded)
			{
				return;
			}
			this.SetDirty(true);
			foreach (SelectionButton_CardInDeck c in this.cards)
			{
				if (c != card && (c.location == location || c.location == card.location))
				{
					c.LockPosition();
				}
			}
			card.LockPosition(position, this.dragCardScale);
			card.transform.SetParent(this.GetDeckLocationParent(location), false);
			card.location = location;
			this.RefreshCardsCount(DeckView.DeckLocation.All);
			this.ChangeGridSpacing(DeckView.DeckLocation.All);
		}

		// Token: 0x060095A2 RID: 38306 RVA: 0x001586D0 File Offset: 0x001568D0
		public void MoveCardToLocationWithSiblingIndex(SelectionButton_CardInDeck card, DeckView.DeckLocation location, int siblingIndex, Vector3 position)
		{
			if (!this.deckLoaded)
			{
				return;
			}
			this.SetDirty(true);
			foreach (SelectionButton_CardInDeck c in this.cards)
			{
				if (c != card && (c.location == card.location || c.location == location))
				{
					c.LockPosition();
				}
			}
			card.LockPosition(position, this.dragCardScale);
			card.transform.SetParent(this.GetDeckLocationParent(location), false);
			card.transform.SetSiblingIndex(siblingIndex);
			card.location = location;
			this.RefreshCardsCount(DeckView.DeckLocation.All);
			this.ChangeGridSpacing(DeckView.DeckLocation.All);
		}

		// Token: 0x060095A3 RID: 38307 RVA: 0x00158794 File Offset: 0x00156994
		public bool RemoveCard(SelectionButton_CardInDeck card, bool needSelect, bool playMoveAnimation, bool destroy)
		{
			if (!this.deckLoaded)
			{
				return false;
			}
			if (!this.CanEditCard())
			{
				return false;
			}
			this.SetDirty(true);
			this.SortCards();
			int index = -1;
			for (int i = 0; i < this.cards.Count; i++)
			{
				if (this.cards[i] == card)
				{
					index = i;
					break;
				}
			}
			if (index < 0)
			{
				Debug.LogError("Card to be deleted not found in the list.");
				return false;
			}
			int siblingIndex = card.transform.GetSiblingIndex();
			for (int j = 0; j < this.cards.Count; j++)
			{
				if (this.cards[j].location == card.location && (!DeckEditor.UseMobileLayout || this.cards[j].transform.GetSiblingIndex() > siblingIndex) && this.cards[j] != card)
				{
					this.cards[j].LockPosition();
				}
			}
			this.cards.RemoveAt(index);
			if (destroy)
			{
				global::UnityEngine.Object.Destroy(card.gameObject);
			}
			else
			{
				card.transform.SetParent(this.TempView, true);
			}
			if (needSelect)
			{
				if (index - 1 >= 0 && index < this.cards.Count && this.cards[index].location != card.location)
				{
					index--;
				}
				if (this.cards.Count <= index)
				{
					index = this.cards.Count - 1;
				}
				if (this.cards.Count == 0)
				{
					this.TextNoItem.gameObject.SetActive(true);
					if (UserInput.gamepadType != UserInput.GamepadType.None)
					{
						this.ButtonNoItem.GetSelectable().Select();
					}
				}
				else if (UserInput.gamepadType != UserInput.GamepadType.None)
				{
					EventSystem.current.SetSelectedGameObject(this.cards[index].gameObject);
				}
			}
			this.RefreshCardsCount(card.location);
			this.ChangeGridSpacing(card.location);
			return true;
		}

		// Token: 0x060095A4 RID: 38308 RVA: 0x00158978 File Offset: 0x00156B78
		public bool ClearDeck()
		{
			if (!this.deckLoaded)
			{
				return false;
			}
			if (!this.CanEditCard())
			{
				return false;
			}
			this.SetDirty(true);
			foreach (SelectionButton_CardInDeck selectionButton_CardInDeck in this.cards)
			{
				selectionButton_CardInDeck.transform.SetParent(Program.instance.ui_.transform);
				selectionButton_CardInDeck.gameObject.SetActive(false);
				global::UnityEngine.Object.Destroy(selectionButton_CardInDeck.gameObject);
			}
			this.cards.Clear();
			this.RefreshCardsCount(DeckView.DeckLocation.All);
			this.ChangeGridSpacing(DeckView.DeckLocation.All);
			return true;
		}

		// Token: 0x060095A5 RID: 38309 RVA: 0x00158A28 File Offset: 0x00156C28
		public SelectionButton_CardInDeck GetCardByData(Card data)
		{
			if (!this.deckLoaded)
			{
				return null;
			}
			List<SelectionButton_CardInDeck> aliasCards = new List<SelectionButton_CardInDeck>();
			foreach (SelectionButton_CardInDeck card in this.cards)
			{
				if (card.Card.Id == data.Id)
				{
					return card;
				}
				if (data.Alias == 0)
				{
					if (card.Card.Alias == data.Id)
					{
						aliasCards.Add(card);
					}
				}
				else if (card.Card.Id == data.Alias || card.Card.Alias == data.Alias)
				{
					aliasCards.Add(card);
				}
			}
			if (aliasCards.Count > 0)
			{
				return aliasCards[0];
			}
			return null;
		}

		// Token: 0x060095A6 RID: 38310 RVA: 0x00158B00 File Offset: 0x00156D00
		public SelectionButton_CardInDeck GetNavigationTarget(DeckView.DeckLocation location, MoveDirection direction, Vector3 position)
		{
			List<SelectionButton_CardInDeck> targetList = new List<SelectionButton_CardInDeck>();
			foreach (SelectionButton_CardInDeck card in this.cards)
			{
				if (card.location == location)
				{
					targetList.Add(card);
				}
			}
			if (targetList.Count == 0)
			{
				if (location != DeckView.DeckLocation.ExtraDeck)
				{
					return null;
				}
				if (direction == MoveDirection.Up)
				{
					location = DeckView.DeckLocation.MainDeck;
				}
				else if (direction == MoveDirection.Down)
				{
					location = DeckView.DeckLocation.SideDeck;
				}
				foreach (SelectionButton_CardInDeck card2 in this.cards)
				{
					if (card2.location == location)
					{
						targetList.Add(card2);
					}
				}
				if (targetList.Count == 0)
				{
					return null;
				}
			}
			Dictionary<SelectionButton_CardInDeck, float> distances = new Dictionary<SelectionButton_CardInDeck, float>();
			foreach (SelectionButton_CardInDeck card3 in targetList)
			{
				distances.Add(card3, Tools.CalculateWeightedDistance(position, card3.transform.position, 'y'));
			}
			return distances.Aggregate(delegate(KeyValuePair<SelectionButton_CardInDeck, float> left, KeyValuePair<SelectionButton_CardInDeck, float> right)
			{
				if (left.Value >= right.Value)
				{
					return right;
				}
				return left;
			}).Key;
		}

		// Token: 0x060095A7 RID: 38311 RVA: 0x00158C60 File Offset: 0x00156E60
		public bool CanEditCard()
		{
			if (this.condition == DeckView.Condition.NonEditable)
			{
				MessageManager.Toast(InterString.Get("请先保存卡组", 0));
			}
			return this.condition == DeckView.Condition.Editable;
		}

		// Token: 0x060095A8 RID: 38312 RVA: 0x00158C84 File Offset: 0x00156E84
		public bool CanAddCard(int code)
		{
			int count = this.GetCardCount(code);
			if (count >= DeckEditor.banlist.GetQuantity(code))
			{
				if (count == 3)
				{
					MessageManager.Toast(InterString.Get("卡组中同名卡片不得超过3张", 0));
				}
				else if (count == 2)
				{
					MessageManager.Toast(InterString.Get("卡组中准限制卡片不得超过2张，@n如需无视限制，请将禁限卡表设置为无（N/A）。", 0));
				}
				else if (count == 1)
				{
					MessageManager.Toast(InterString.Get("卡组中限制卡片不得超过1张，@n如需无视限制，请将禁限卡表设置为无（N/A）。", 0));
				}
				else
				{
					MessageManager.Toast(InterString.Get("无法将禁止卡片放入卡组，@n如需无视限制，请将禁限卡表设置为无（N/A）。", 0));
				}
				return false;
			}
			return true;
		}

		// Token: 0x060095A9 RID: 38313 RVA: 0x00158CFC File Offset: 0x00156EFC
		public bool CanSwitchPosition(Card card, DeckView.DeckLocation location)
		{
			if (card.IsExtraCard() && location == DeckView.DeckLocation.MainDeck)
			{
				MessageManager.Toast(InterString.Get("无法将该卡片加入主卡组", 0));
				return false;
			}
			if (!card.IsExtraCard() && location == DeckView.DeckLocation.ExtraDeck)
			{
				MessageManager.Toast(InterString.Get("无法将该卡片加入额外卡组", 0));
				return false;
			}
			return true;
		}

		// Token: 0x060095AA RID: 38314 RVA: 0x00158D3B File Offset: 0x00156F3B
		public void HideDeckLocationTable()
		{
			this.MainDeckView.Hide();
			this.ExtraDeckView.Hide();
			this.SideDeckView.Hide();
		}

		// Token: 0x060095AB RID: 38315 RVA: 0x00158D60 File Offset: 0x00156F60
		public void SetCursor(bool selected)
		{
			this.CursorWindowSelect.Show = selected;
			if (this.shortcutIcons == null)
			{
				this.shortcutIcons = base.GetComponentsInChildren<ShortcutIcon>(true);
			}
			ShortcutIcon[] array = this.shortcutIcons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].GroupShow = selected;
			}
		}

		// Token: 0x060095AC RID: 38316 RVA: 0x00158DAC File Offset: 0x00156FAC
		public void SelectDefaultItem()
		{
			if (this.cards.Count > 0)
			{
				this.SortCards();
				EventSystem.current.SetSelectedGameObject(this.cards[0].gameObject);
				return;
			}
			this.ButtonNoItem.GetSelectable().Select();
		}

		// Token: 0x060095AD RID: 38317 RVA: 0x00158DFC File Offset: 0x00156FFC
		public void SelectNearestCard(Vector3 fromPosition)
		{
			UserInput.NextSelectionIsAxis = true;
			if (this.cards.Count == 0)
			{
				this.SelectDefaultItem();
				return;
			}
			Dictionary<SelectionButton_CardInDeck, float> distance = new Dictionary<SelectionButton_CardInDeck, float>();
			foreach (SelectionButton_CardInDeck card in this.cards)
			{
				distance.Add(card, Tools.CalculateWeightedDistance(fromPosition, card.transform.position, 'x'));
			}
			SelectionButton_CardInDeck minKey = distance.Aggregate(delegate(KeyValuePair<SelectionButton_CardInDeck, float> left, KeyValuePair<SelectionButton_CardInDeck, float> right)
			{
				if (left.Value >= right.Value)
				{
					return right;
				}
				return left;
			}).Key;
			EventSystem.current.SetSelectedGameObject(minKey.gameObject);
		}

		// Token: 0x060095AE RID: 38318 RVA: 0x00158EC4 File Offset: 0x001570C4
		public void SetNoItemButtonNavigationEvent(MoveDirection direction, UnityAction navigation)
		{
			this.ButtonNoItem.SetNavigationEvent(direction, navigation);
		}

		// Token: 0x060095AF RID: 38319 RVA: 0x00158ED3 File Offset: 0x001570D3
		public bool Save()
		{
			if (this.condition == DeckView.Condition.Editable && !this.deckLoaded)
			{
				return false;
			}
			this.Deck = this.FromObjectDeckToCodedDeck();
			this.DeckFileSave();
			this.SetCondition(DeckView.Condition.Editable);
			return true;
		}

		// Token: 0x060095B0 RID: 38320 RVA: 0x00158F04 File Offset: 0x00157104
		public int GetDeckLocationCount(DeckView.DeckLocation location)
		{
			int count = 0;
			if ((location & DeckView.DeckLocation.MainDeck) > (DeckView.DeckLocation)0)
			{
				count += this.GetDeckLocationParent(DeckView.DeckLocation.MainDeck).childCount;
			}
			if ((location & DeckView.DeckLocation.ExtraDeck) > (DeckView.DeckLocation)0)
			{
				count += this.GetDeckLocationParent(DeckView.DeckLocation.ExtraDeck).childCount;
			}
			if ((location & DeckView.DeckLocation.SideDeck) > (DeckView.DeckLocation)0)
			{
				count += this.GetDeckLocationParent(DeckView.DeckLocation.SideDeck).childCount;
			}
			return count;
		}

		// Token: 0x060095B1 RID: 38321 RVA: 0x00158F54 File Offset: 0x00157154
		public void SetCardInfoType(DeckEditorUI.CardInfoType type)
		{
			foreach (SelectionButton_CardInDeck selectionButton_CardInDeck in this.cards)
			{
				selectionButton_CardInDeck.RefreshIcons();
			}
			this.SetCardInfoTypeInternal(type);
		}

		// Token: 0x060095B2 RID: 38322 RVA: 0x00158FAC File Offset: 0x001571AC
		private void SetCardInfoTypeInternal(DeckEditorUI.CardInfoType type)
		{
			if (Program.instance.currentServant == Program.instance.deckEditor)
			{
				this.MainDeckGenesys.SetActive(type == DeckEditorUI.CardInfoType.Genesys);
				this.TextMainDeckGP.gameObject.SetActive(type == DeckEditorUI.CardInfoType.Genesys);
				this.ExtraDeckGenesys.SetActive(type == DeckEditorUI.CardInfoType.Genesys);
				this.TextExtraDeckGP.gameObject.SetActive(type == DeckEditorUI.CardInfoType.Genesys);
				this.SideDeckGenesys.SetActive(type == DeckEditorUI.CardInfoType.Genesys);
				this.TextSideDeckGP.gameObject.SetActive(type == DeckEditorUI.CardInfoType.Genesys);
				return;
			}
			this.MainDeckGenesys.SetActive(false);
			this.TextMainDeckGP.gameObject.SetActive(false);
			this.ExtraDeckGenesys.SetActive(false);
			this.TextExtraDeckGP.gameObject.SetActive(false);
			this.SideDeckGenesys.SetActive(false);
			this.TextSideDeckGP.gameObject.SetActive(false);
		}

		// Token: 0x060095B3 RID: 38323 RVA: 0x00159095 File Offset: 0x00157295
		public TMP_InputField GetInputField()
		{
			return this.InputDeckName;
		}

		// Token: 0x060095B4 RID: 38324 RVA: 0x001590A0 File Offset: 0x001572A0
		public void RefreshRarity(int code)
		{
			foreach (SelectionButton_CardInDeck selectionButton_CardInDeck in this.cards)
			{
				selectionButton_CardInDeck.RefreshRarity(code);
			}
		}

		// Token: 0x060095B5 RID: 38325 RVA: 0x001590F4 File Offset: 0x001572F4
		public void ScrollTo(SelectionButton_CardInDeck card)
		{
			if (this.AutoScroll == null)
			{
				return;
			}
			this.AutoScroll.VerticalScrollTo(card.GetComponent<RectTransform>());
		}

		// Token: 0x060095B6 RID: 38326 RVA: 0x00159116 File Offset: 0x00157316
		public void ResetDeck()
		{
			if (!this.deckLoaded)
			{
				return;
			}
			if (this.condition != DeckView.Condition.ChangeSide && !this.CanEditCard())
			{
				return;
			}
			this.PrintDeck(this.Deck, this.deckNameWithType, this.condition);
		}

		// Token: 0x060095B7 RID: 38327 RVA: 0x0015914C File Offset: 0x0015734C
		public void Sort()
		{
			if (!this.deckLoaded)
			{
				return;
			}
			if (this.condition != DeckView.Condition.ChangeSide && !this.CanEditCard())
			{
				return;
			}
			this.SetDirty(true);
			List<SelectionButton_CardInDeck> main = new List<SelectionButton_CardInDeck>();
			List<SelectionButton_CardInDeck> extra = new List<SelectionButton_CardInDeck>();
			List<SelectionButton_CardInDeck> side = new List<SelectionButton_CardInDeck>();
			foreach (SelectionButton_CardInDeck card in this.cards)
			{
				if (card.location == DeckView.DeckLocation.MainDeck)
				{
					main.Add(card);
				}
				else if (card.location == DeckView.DeckLocation.ExtraDeck)
				{
					extra.Add(card);
				}
				else if (card.location == DeckView.DeckLocation.SideDeck)
				{
					side.Add(card);
				}
				card.LockPosition();
			}
			main.Sort((SelectionButton_CardInDeck left, SelectionButton_CardInDeck right) => CardsManager.ComparisonOfCard()(left.Card, right.Card));
			extra.Sort((SelectionButton_CardInDeck left, SelectionButton_CardInDeck right) => CardsManager.ComparisonOfCard()(left.Card, right.Card));
			side.Sort((SelectionButton_CardInDeck left, SelectionButton_CardInDeck right) => CardsManager.ComparisonOfCard()(left.Card, right.Card));
			for (int i = 0; i < main.Count; i++)
			{
				main[i].transform.SetSiblingIndex(i);
			}
			for (int j = 0; j < extra.Count; j++)
			{
				extra[j].transform.SetSiblingIndex(j);
			}
			for (int k = 0; k < side.Count; k++)
			{
				side[k].transform.SetSiblingIndex(k);
			}
			this.cards.Clear();
			this.cards = new List<SelectionButton_CardInDeck>(main);
			this.cards.AddRange(extra);
			this.cards.AddRange(side);
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				this.SelectDefaultItem();
			}
		}

		// Token: 0x060095B8 RID: 38328 RVA: 0x00159338 File Offset: 0x00157538
		public void Randomize()
		{
			if (!this.deckLoaded)
			{
				return;
			}
			if (this.condition != DeckView.Condition.ChangeSide && !this.CanEditCard())
			{
				return;
			}
			this.SetDirty(true);
			List<SelectionButton_CardInDeck> main = new List<SelectionButton_CardInDeck>();
			foreach (SelectionButton_CardInDeck card in this.cards)
			{
				if (card.location == DeckView.DeckLocation.MainDeck)
				{
					main.Add(card);
					card.LockPosition();
				}
			}
			global::System.Random rand = new global::System.Random();
			for (int i = 0; i < main.Count; i++)
			{
				int random_index = rand.Next() % main.Count;
				List<SelectionButton_CardInDeck> list = main;
				int num = random_index;
				List<SelectionButton_CardInDeck> list2 = main;
				int num2 = i;
				SelectionButton_CardInDeck selectionButton_CardInDeck = main[i];
				SelectionButton_CardInDeck selectionButton_CardInDeck2 = main[random_index];
				list[num] = selectionButton_CardInDeck;
				list2[num2] = selectionButton_CardInDeck2;
			}
			for (int j = 0; j < main.Count; j++)
			{
				main[j].transform.SetSiblingIndex(j);
			}
			if (Cursor.lockState == CursorLockMode.Locked)
			{
				this.SelectDefaultItem();
			}
		}

		// Token: 0x060095B9 RID: 38329 RVA: 0x00159460 File Offset: 0x00157660
		public void Copy()
		{
			if (!this.deckLoaded)
			{
				return;
			}
			if (!this.CanEditCard())
			{
				return;
			}
			this.SetDirty(true);
			this.deckNameWithType = this.deckNameWithType + " - " + InterString.Get("复制", 0);
			this.deckFileName = this.deckFileName + " - " + InterString.Get("复制", 0);
			this.InputDeckName.text = this.deckFileName;
			this.Deck.deckId = string.Empty;
		}

		// Token: 0x060095BA RID: 38330 RVA: 0x001594EC File Offset: 0x001576EC
		public void Share()
		{
			if (!this.deckLoaded)
			{
				return;
			}
			if (!this.CanEditCard())
			{
				return;
			}
			if (this.GetDirty() || !File.Exists("Deck/" + this.deckNameWithType + ".ydk"))
			{
				if (this.condition != DeckView.Condition.ChangeSide)
				{
					MessageManager.Toast(InterString.Get("请先保存卡组", 0));
				}
				return;
			}
			Application.OpenURL(GUIUtility.systemCopyBuffer = DeckShareURL.DeckToUri(this.Deck.Main, this.Deck.Extra, this.Deck.Side, null).ToString());
		}

		// Token: 0x060095BB RID: 38331 RVA: 0x00159580 File Offset: 0x00157780
		public List<int> GetAllCardCodes()
		{
			List<int> returnValue = new List<int>();
			foreach (SelectionButton_CardInDeck card in this.cards)
			{
				returnValue.Add(card.Card.Id);
			}
			return returnValue;
		}

		// Token: 0x060095BC RID: 38332 RVA: 0x001595E4 File Offset: 0x001577E4
		public void ActivateInputField()
		{
			this.InputDeckName.ActivateInputField();
		}

		// Token: 0x060095BD RID: 38333 RVA: 0x001595F4 File Offset: 0x001577F4
		public Deck FromObjectDeckToCodedDeck()
		{
			this.SortCards();
			Deck deck = new Deck();
			foreach (SelectionButton_CardInDeck card in this.cards)
			{
				if (card.location == DeckView.DeckLocation.MainDeck)
				{
					deck.Main.Add(card.Card.Id);
				}
				else if (card.location == DeckView.DeckLocation.ExtraDeck)
				{
					deck.Extra.Add(card.Card.Id);
				}
				else if (card.location == DeckView.DeckLocation.SideDeck)
				{
					deck.Side.Add(card.Card.Id);
				}
			}
			deck.Pickup = this.Deck.Pickup;
			deck.Protector = this.Deck.Protector;
			deck.Case = this.Deck.Case;
			deck.Field = this.Deck.Field;
			deck.Grave = this.Deck.Grave;
			deck.Stand = this.Deck.Stand;
			deck.Mate = this.Deck.Mate;
			deck.deckId = this.Deck.deckId;
			deck.userId = this.Deck.userId;
			if (Path.GetFileName(this.deckNameWithType) == this.deckNameWithType)
			{
				deck.type = string.Empty;
			}
			else
			{
				deck.type = Path.GetDirectoryName(this.deckNameWithType);
			}
			return deck;
		}

		// Token: 0x060095BE RID: 38334 RVA: 0x00159780 File Offset: 0x00157980
		public int GetGenesysPoints()
		{
			int value = 0;
			foreach (SelectionButton_CardInDeck card in this.cards)
			{
				if (card.genesysPoint > 0)
				{
					value += card.genesysPoint;
				}
			}
			return value;
		}

		// Token: 0x060095BF RID: 38335 RVA: 0x001597E4 File Offset: 0x001579E4
		protected override void Awake()
		{
			base.Awake();
			this.Template.transform.SetParent(base.transform, false);
			this.Template.SetActive(false);
			this.ButtonNoItem.SetSelectEvent(delegate
			{
				if (Program.instance.currentServant == Program.instance.deckEditor)
				{
					Program.instance.deckEditor.ResponseRegion = DeckEditorUI.ResponseRegion.Deck;
				}
			});
		}

		// Token: 0x060095C0 RID: 38336 RVA: 0x00159844 File Offset: 0x00157A44
		protected IEnumerator PrintDeckAsync()
		{
			this.deckLoaded = false;
			if (this.cards != null)
			{
				foreach (SelectionButton_CardInDeck selectionButton_CardInDeck in this.cards)
				{
					global::UnityEngine.Object.Destroy(selectionButton_CardInDeck.gameObject);
				}
			}
			this.cards = new List<SelectionButton_CardInDeck>();
			this.Viewport.alpha = 0f;
			this.Viewport.blocksRaycasts = false;
			while (Program.instance.deckEditor.inTransition)
			{
				yield return null;
			}
			this.TweenLoading.Show();
			if (this.Deck == null)
			{
				while (DeckEditor.Deck == null)
				{
					yield return null;
				}
				this.Deck = DeckEditor.Deck;
			}
			foreach (int card in this.Deck.Main)
			{
				this.AddCard(CardsManager.Get(card, false), DeckView.DeckLocation.MainDeck, false, false);
				yield return null;
			}
			List<int>.Enumerator enumerator2 = default(List<int>.Enumerator);
			foreach (int card2 in this.Deck.Extra)
			{
				this.AddCard(CardsManager.Get(card2, false), DeckView.DeckLocation.ExtraDeck, false, false);
				yield return null;
			}
			enumerator2 = default(List<int>.Enumerator);
			foreach (int card3 in this.Deck.Side)
			{
				this.AddCard(CardsManager.Get(card3, false), DeckView.DeckLocation.SideDeck, false, false);
				yield return null;
			}
			enumerator2 = default(List<int>.Enumerator);
			this.RefreshCardsCount(DeckView.DeckLocation.All);
			this.TweenLoading.Hide();
			this.Viewport.alpha = 1f;
			this.Viewport.blocksRaycasts = true;
			this.deckLoaded = true;
			this.SetDirty(false);
			if (this.cards.Count > 0 && UserInput.NeedDefaultSelect())
			{
				this.cards[0].GetSelectable().Select();
			}
			if (Program.instance.currentServant == Program.instance.deckBrowser)
			{
				this.PrePick();
			}
			yield break;
			yield break;
		}

		// Token: 0x060095C1 RID: 38337 RVA: 0x00159854 File Offset: 0x00157A54
		protected virtual void ChangeGridSpacing(DeckView.DeckLocation location)
		{
			if ((location & DeckView.DeckLocation.MainDeck) > (DeckView.DeckLocation)0)
			{
				int count = this.GetDeckLocationCount(DeckView.DeckLocation.MainDeck);
				if (count <= this.defaultMainDeckRows * this.defaultColumns)
				{
					this.MainDeckContent.spacing = this.defaultSpacing;
				}
				else
				{
					int columns = Mathf.CeilToInt((float)count / (float)this.defaultMainDeckRows);
					float targetSpace = (this.contentWidth - (float)columns * this.templateWidth) / (float)(columns - 1);
					this.MainDeckContent.spacing = new Vector2(targetSpace, this.defaultVerticalSpacing);
				}
			}
			if ((location & DeckView.DeckLocation.ExtraDeck) > (DeckView.DeckLocation)0)
			{
				int count2 = this.GetDeckLocationCount(DeckView.DeckLocation.ExtraDeck);
				if (count2 <= this.defaultExtraDeckRows * this.defaultColumns)
				{
					this.ExtraDeckContent.spacing = this.defaultSpacing;
				}
				else
				{
					int columns2 = Mathf.CeilToInt((float)count2 / (float)this.defaultExtraDeckRows);
					float targetSpace2 = (this.contentWidth - (float)columns2 * this.templateWidth) / (float)(columns2 - 1);
					this.ExtraDeckContent.spacing = new Vector2(targetSpace2, this.defaultVerticalSpacing);
				}
			}
			if ((location & DeckView.DeckLocation.SideDeck) > (DeckView.DeckLocation)0)
			{
				int count3 = this.GetDeckLocationCount(DeckView.DeckLocation.SideDeck);
				if (count3 <= this.defaultSideDeckRows * this.defaultColumns)
				{
					this.SideDeckContent.spacing = this.defaultSpacing;
					return;
				}
				int columns3 = Mathf.CeilToInt((float)count3 / (float)this.defaultSideDeckRows);
				float targetSpace3 = (this.contentWidth - (float)columns3 * this.templateWidth) / (float)(columns3 - 1);
				this.SideDeckContent.spacing = new Vector2(targetSpace3, this.defaultVerticalSpacing);
			}
		}

		// Token: 0x060095C2 RID: 38338 RVA: 0x001599B8 File Offset: 0x00157BB8
		protected void RefreshCardsCount(DeckView.DeckLocation location)
		{
			if ((location & DeckView.DeckLocation.MainDeck) > (DeckView.DeckLocation)0)
			{
				this.mainCount = 0;
				int monsterCount = 0;
				int spellCount = 0;
				int trapCount = 0;
				int gp = 0;
				foreach (SelectionButton_CardInDeck card in this.cards)
				{
					if (card.location == DeckView.DeckLocation.MainDeck)
					{
						this.mainCount++;
						if (card.Card.HasType(CardType.Spell))
						{
							spellCount++;
						}
						else if (card.Card.HasType(CardType.Trap))
						{
							trapCount++;
						}
						else
						{
							monsterCount++;
						}
						if (card.genesysPoint > 0)
						{
							gp += card.genesysPoint;
						}
					}
				}
				this.TextMainDeckCardNum.text = this.mainCount.ToString();
				this.TextMainDeckMonsterNum.text = monsterCount.ToString();
				this.TextMainDeckSpellNum.text = spellCount.ToString();
				this.TextMainDeckTrapNum.text = trapCount.ToString();
				this.TextMainDeckGP.text = gp.ToString();
			}
			if ((location & DeckView.DeckLocation.ExtraDeck) > (DeckView.DeckLocation)0)
			{
				this.extraCount = 0;
				int fusionCount = 0;
				int synchroCount = 0;
				int xyzCount = 0;
				int linkCount = 0;
				int gp2 = 0;
				foreach (SelectionButton_CardInDeck card2 in this.cards)
				{
					if (card2.location == DeckView.DeckLocation.ExtraDeck)
					{
						this.extraCount++;
						if (card2.Card.HasType(CardType.Fusion))
						{
							fusionCount++;
						}
						else if (card2.Card.HasType(CardType.Synchro))
						{
							synchroCount++;
						}
						else if (card2.Card.HasType(CardType.Xyz))
						{
							xyzCount++;
						}
						else if (card2.Card.HasType(CardType.Link))
						{
							linkCount++;
						}
						if (card2.genesysPoint > 0)
						{
							gp2 += card2.genesysPoint;
						}
					}
				}
				this.TextExtraDeckCardNum.text = this.extraCount.ToString();
				this.TextExtraDeckFusionNum.text = fusionCount.ToString();
				this.TextExtraDeckSynchroNum.text = synchroCount.ToString();
				this.TextExtraDeckXyzNum.text = xyzCount.ToString();
				this.TextExtraDeckLinkNum.text = linkCount.ToString();
				this.TextExtraDeckGP.text = gp2.ToString();
			}
			if ((location & DeckView.DeckLocation.SideDeck) > (DeckView.DeckLocation)0)
			{
				this.sideCount = 0;
				int monsterCount2 = 0;
				int spellCount2 = 0;
				int trapCount2 = 0;
				int gp3 = 0;
				foreach (SelectionButton_CardInDeck card3 in this.cards)
				{
					if (card3.location == DeckView.DeckLocation.SideDeck)
					{
						this.sideCount++;
						if (card3.Card.HasType(CardType.Spell))
						{
							spellCount2++;
						}
						else if (card3.Card.HasType(CardType.Trap))
						{
							trapCount2++;
						}
						else
						{
							monsterCount2++;
						}
						if (card3.genesysPoint > 0)
						{
							gp3 += card3.genesysPoint;
						}
					}
				}
				this.TextSideDeckCardNum.text = this.sideCount.ToString();
				this.TextSideDeckMonsterNum.text = monsterCount2.ToString();
				this.TextSideDeckSpellNum.text = spellCount2.ToString();
				this.TextSideDeckTrapNum.text = trapCount2.ToString();
				this.TextSideDeckGP.text = gp3.ToString();
			}
			if (Program.instance.currentServant == Program.instance.deckEditor)
			{
				SelectionButton_CardInfoType.SetGenesysPoints(this.GetGenesysPoints());
			}
		}

		// Token: 0x060095C3 RID: 38339 RVA: 0x00159D80 File Offset: 0x00157F80
		protected void SortCards()
		{
			if (this.cards == null)
			{
				return;
			}
			this.cards.Sort(DeckView.ComparisonOfCard());
		}

		// Token: 0x060095C4 RID: 38340 RVA: 0x00159D9B File Offset: 0x00157F9B
		internal static Comparison<SelectionButton_CardInDeck> ComparisonOfCard()
		{
			return delegate(SelectionButton_CardInDeck left, SelectionButton_CardInDeck right)
			{
				int a;
				if (left.location < right.location)
				{
					a = -1;
				}
				else if (right.location < left.location)
				{
					a = 1;
				}
				else if (left.transform.GetSiblingIndex() <= right.transform.GetSiblingIndex())
				{
					a = -1;
				}
				else
				{
					a = 1;
				}
				return a;
			};
		}

		// Token: 0x060095C5 RID: 38341 RVA: 0x00159DBC File Offset: 0x00157FBC
		protected SelectionButton_CardInDeck AddCard(Card data, DeckView.DeckLocation location, bool playMoveAnimation, bool playBirthAnimation)
		{
			this.SetDirty(true);
			this.SortCards();
			this.TextNoItem.gameObject.SetActive(false);
			if (playMoveAnimation)
			{
				foreach (SelectionButton_CardInDeck card in this.cards)
				{
					if (card.location == location)
					{
						card.LockPosition();
					}
				}
			}
			GameObject gameObject = global::UnityEngine.Object.Instantiate<GameObject>(this.Template);
			gameObject.SetActive(true);
			gameObject.transform.SetParent(this.GetDeckLocationParent(location), false);
			SelectionButton_CardInDeck handler = gameObject.GetComponent<SelectionButton_CardInDeck>();
			handler.deckView = this;
			handler.Card = data;
			handler.location = location;
			this.cards.Add(handler);
			this.RefreshCardsCount(location);
			this.ChangeGridSpacing(location);
			if (playBirthAnimation)
			{
				handler.PlayBirthAnimation();
			}
			return handler;
		}

		// Token: 0x060095C6 RID: 38342 RVA: 0x00159EA0 File Offset: 0x001580A0
		protected DeckView.DeckLocation GetHoveredLocation()
		{
			if (this.MainDeckView.Hover)
			{
				return DeckView.DeckLocation.MainDeck;
			}
			if (this.ExtraDeckView.Hover)
			{
				return DeckView.DeckLocation.ExtraDeck;
			}
			if (this.SideDeckView.Hover)
			{
				return DeckView.DeckLocation.SideDeck;
			}
			return DeckView.DeckLocation.All;
		}

		// Token: 0x060095C7 RID: 38343 RVA: 0x00159ED0 File Offset: 0x001580D0
		protected void DeckFileSave()
		{
			try
			{
				string deckName = this.GetDeckName();
				this.Deck.type = this.deckType;
				this.Deck.Save(deckName, DateTime.UtcNow, true, true);
				if (deckName != this.deckFileName)
				{
					File.Delete("Deck/" + this.deckNameWithType + ".ydk");
				}
				this.deckFileName = deckName;
				this.deckNameWithType = ((this.deckType == string.Empty) ? string.Empty : ("/" + this.deckType + deckName));
				MessageManager.Toast(InterString.Get("本地卡组「[?]」已保存。", deckName, 0));
				Config.SetConfigDeck(deckName, true);
				this.SetDirty(false);
			}
			catch (Exception ex)
			{
				MessageManager.Toast(InterString.Get("保存失败！", 0));
				MessageManager.Cast(ex.Message);
			}
		}

		// Token: 0x060095C8 RID: 38344 RVA: 0x00159FB8 File Offset: 0x001581B8
		private void PrePick()
		{
			for (int i = 0; i < 3; i++)
			{
				foreach (SelectionButton_CardInDeck card in this.cards)
				{
					if (card.Card.Id == DeckEditor.Deck.Pickup[i] && !card.picked)
					{
						card.PrePickThis(i);
						break;
					}
				}
			}
		}

		// Token: 0x060095C9 RID: 38345 RVA: 0x0015A040 File Offset: 0x00158240
		public void Pickup(SelectionButton_CardInDeck card)
		{
			foreach (SelectionButton_CardInDeck c in this.cards)
			{
				if (c != card && c.pickupIndex == card.pickupIndex)
				{
					c.DepickupThis();
				}
			}
		}

		// Token: 0x060095CA RID: 38346 RVA: 0x0015A0AC File Offset: 0x001582AC
		public void Depickup(int index)
		{
			foreach (SelectionButton_CardInDeck c in this.cards)
			{
				if (c.pickupIndex == index)
				{
					c.DepickupThis();
				}
			}
		}

		// Token: 0x0400D3BE RID: 54206
		private const string LABEL_DTM_LOADING = "Loading";

		// Token: 0x0400D3BF RID: 54207
		private DoTweenManager m_TweenLoading;

		// Token: 0x0400D3C0 RID: 54208
		private const string LABEL_CG_VIEWPORT = "Viewport";

		// Token: 0x0400D3C1 RID: 54209
		private CanvasGroup m_Viewport;

		// Token: 0x0400D3C2 RID: 54210
		private const string LABEL_SBN_NOITEM = "NoItemButton";

		// Token: 0x0400D3C3 RID: 54211
		private SelectionButton m_ButtonNoItem;

		// Token: 0x0400D3C4 RID: 54212
		private const string LABEL_TXT_NOITEM = "NoItemText";

		// Token: 0x0400D3C5 RID: 54213
		private TextMeshProUGUI m_TextNoItem;

		// Token: 0x0400D3C6 RID: 54214
		private const string LABEL_GPC_CURSORWINDOWSELECT = "CursorWindowSelect";

		// Token: 0x0400D3C7 RID: 54215
		private GamepadCursor m_CursorWindowSelect;

		// Token: 0x0400D3C8 RID: 54216
		private const string LABEL_SR_DECKVIEW = "ScrollRect";

		// Token: 0x0400D3C9 RID: 54217
		private ScrollRect m_ScrollRect;

		// Token: 0x0400D3CA RID: 54218
		protected UIScrollToSelection m_AutoScroll;

		// Token: 0x0400D3CB RID: 54219
		private const string LABEL_RT_TEMPVIEW = "TempView";

		// Token: 0x0400D3CC RID: 54220
		private RectTransform m_TempView;

		// Token: 0x0400D3CD RID: 54221
		private const string LABEL_RT_HEADERAREA = "HeaderArea";

		// Token: 0x0400D3CE RID: 54222
		private RectTransform m_HeaderArea;

		// Token: 0x0400D3CF RID: 54223
		private const string LABEL_TXT_DECKNAME = "HeaderArea/DeckNameText";

		// Token: 0x0400D3D0 RID: 54224
		private TextMeshProUGUI m_TextDeckName;

		// Token: 0x0400D3D1 RID: 54225
		private const string LABEL_GO_NAMEAREAGROUP = "HeaderArea/NameAreaGroup";

		// Token: 0x0400D3D2 RID: 54226
		private GameObject m_NameAreaGroup;

		// Token: 0x0400D3D3 RID: 54227
		private const string LABEL_IPT_DECKNAME = "HeaderArea/InputField";

		// Token: 0x0400D3D4 RID: 54228
		private TMP_InputField m_InputDeckName;

		// Token: 0x0400D3D5 RID: 54229
		private const string LABEL_SBN_BUTTON_DECK = "HeaderArea/ButtonDeck";

		// Token: 0x0400D3D6 RID: 54230
		private SelectionButton m_ButtonDeck;

		// Token: 0x0400D3D7 RID: 54231
		private const string LABEL_IMG_DECK = "HeaderArea/IconDeck";

		// Token: 0x0400D3D8 RID: 54232
		private Image m_IconDeck;

		// Token: 0x0400D3D9 RID: 54233
		private const string LABEL_UH_MAINDECKVIEW = "MainDeckView";

		// Token: 0x0400D3DA RID: 54234
		private UIHover m_MainDeckView;

		// Token: 0x0400D3DB RID: 54235
		private const string LABEL_TXT_MAINDECKCARDNUM = "MainDeckView/TextMainDeckCardNum";

		// Token: 0x0400D3DC RID: 54236
		private TextMeshProUGUI m_TextMainDeckCardNum;

		// Token: 0x0400D3DD RID: 54237
		private const string LABEL_TXT_MAINDECKMONSTERNUM = "MainDeckView/TextMainDeckMonsterNum";

		// Token: 0x0400D3DE RID: 54238
		private TextMeshProUGUI m_TextMainDeckMonsterNum;

		// Token: 0x0400D3DF RID: 54239
		private const string LABEL_TXT_MAINDECKSPELLNUM = "MainDeckView/TextMainDeckSpellNum";

		// Token: 0x0400D3E0 RID: 54240
		private TextMeshProUGUI m_TextMainDeckSpellNum;

		// Token: 0x0400D3E1 RID: 54241
		private const string LABEL_TXT_MAINDECKTRAPNUM = "MainDeckView/TextMainDeckTrapNum";

		// Token: 0x0400D3E2 RID: 54242
		private TextMeshProUGUI m_TextMainDeckTrapNum;

		// Token: 0x0400D3E3 RID: 54243
		private const string LABEL_GLG_MAIN_DECK_CONTENT = "MainDeckView/MainDeckContent";

		// Token: 0x0400D3E4 RID: 54244
		private GridLayoutGroup m_MainDeckContent;

		// Token: 0x0400D3E5 RID: 54245
		private const string LABEL_GO_TEMPLATE = "MainDeckView/template";

		// Token: 0x0400D3E6 RID: 54246
		private GameObject m_Template;

		// Token: 0x0400D3E7 RID: 54247
		private const string LABEL_GO_MAIN_GENESYS = "MainDeckView/TextMainDeckGenesys";

		// Token: 0x0400D3E8 RID: 54248
		private GameObject m_MainDeckGenesys;

		// Token: 0x0400D3E9 RID: 54249
		private const string LABEL_TXT_MAIN_DECK_GP = "MainDeckView/TextMainDeckGP";

		// Token: 0x0400D3EA RID: 54250
		private TextMeshProUGUI m_TextMainDeckGP;

		// Token: 0x0400D3EB RID: 54251
		private const string LABEL_UH_EXTRADECKVIEW = "ExtraDeckView";

		// Token: 0x0400D3EC RID: 54252
		private UIHover m_ExtraDeckView;

		// Token: 0x0400D3ED RID: 54253
		private const string LABEL_TXT_EXTRADECKCARDNUM = "ExtraDeckView/TextExtraDeckCardNum";

		// Token: 0x0400D3EE RID: 54254
		private TextMeshProUGUI m_TextExtraDeckCardNum;

		// Token: 0x0400D3EF RID: 54255
		private const string LABEL_TXT_EXTRADECKFUSIONNUM = "ExtraDeckView/TextExtraDeckFusionNum";

		// Token: 0x0400D3F0 RID: 54256
		private TextMeshProUGUI m_TextExtraDeckFusionNum;

		// Token: 0x0400D3F1 RID: 54257
		private const string LABEL_TXT_EXTRADECKSYNCHRONUM = "ExtraDeckView/TextExtraDeckSynchroNum";

		// Token: 0x0400D3F2 RID: 54258
		private TextMeshProUGUI m_TextExtraDeckSynchroNum;

		// Token: 0x0400D3F3 RID: 54259
		private const string LABEL_TXT_EXTRADECKXYZNUM = "ExtraDeckView/TextExtraDeckXyzNum";

		// Token: 0x0400D3F4 RID: 54260
		private TextMeshProUGUI m_TextExtraDeckXyzNum;

		// Token: 0x0400D3F5 RID: 54261
		private const string LABEL_TXT_EXTRADECKLINKNUM = "ExtraDeckView/TextExtraDeckLinkNum";

		// Token: 0x0400D3F6 RID: 54262
		private TextMeshProUGUI m_TextExtraDeckLinkNum;

		// Token: 0x0400D3F7 RID: 54263
		private const string LABEL_GLG_EXTRADeckContent = "ExtraDeckView/ExtraDeckContent";

		// Token: 0x0400D3F8 RID: 54264
		private GridLayoutGroup m_ExtraDeckContent;

		// Token: 0x0400D3F9 RID: 54265
		private const string LABEL_GO_EXTRA_GENESYS = "ExtraDeckView/TextExtraDeckGenesys";

		// Token: 0x0400D3FA RID: 54266
		private GameObject m_ExtraDeckGenesys;

		// Token: 0x0400D3FB RID: 54267
		private const string LABEL_TXT_EXTRA_DECK_GP = "ExtraDeckView/TextExtraDeckGP";

		// Token: 0x0400D3FC RID: 54268
		private TextMeshProUGUI m_TextExtraDeckGP;

		// Token: 0x0400D3FD RID: 54269
		private const string LABEL_UH_SIDEDECKVIEW = "SideDeckView";

		// Token: 0x0400D3FE RID: 54270
		private UIHover m_SideDeckView;

		// Token: 0x0400D3FF RID: 54271
		private const string LABEL_TXT_SIDEDECKCARDNUM = "SideDeckView/TextSideDeckCardNum";

		// Token: 0x0400D400 RID: 54272
		private TextMeshProUGUI m_TextSideDeckCardNum;

		// Token: 0x0400D401 RID: 54273
		private const string LABEL_TXT_SIDEDECKMONSTERNUM = "SideDeckView/TextSideDeckMonsterNum";

		// Token: 0x0400D402 RID: 54274
		private TextMeshProUGUI m_TextSideDeckMonsterNum;

		// Token: 0x0400D403 RID: 54275
		private const string LABEL_TXT_SIDEDECKSPELLNUM = "SideDeckView/TextSideDeckSpellNum";

		// Token: 0x0400D404 RID: 54276
		private TextMeshProUGUI m_TextSideDeckSpellNum;

		// Token: 0x0400D405 RID: 54277
		private const string LABEL_TXT_SIDEDECKTRAPNUM = "SideDeckView/TextSideDeckTrapNum";

		// Token: 0x0400D406 RID: 54278
		private TextMeshProUGUI m_TextSideDeckTrapNum;

		// Token: 0x0400D407 RID: 54279
		private const string LABEL_GLG_SideDeckContent = "SideDeckView/SideDeckContent";

		// Token: 0x0400D408 RID: 54280
		private GridLayoutGroup m_SideDeckContent;

		// Token: 0x0400D409 RID: 54281
		private const string LABEL_GO_SIDE_GENESYS = "SideDeckView/TextSideDeckGenesys";

		// Token: 0x0400D40A RID: 54282
		private GameObject m_SideDeckGenesys;

		// Token: 0x0400D40B RID: 54283
		private const string LABEL_TXT_SIDE_DECK_GP = "SideDeckView/TextSideDeckGP";

		// Token: 0x0400D40C RID: 54284
		private TextMeshProUGUI m_TextSideDeckGP;

		// Token: 0x0400D40D RID: 54285
		public DeckView.Condition condition;

		// Token: 0x0400D40E RID: 54286
		protected float contentWidth = 756f;

		// Token: 0x0400D40F RID: 54287
		protected float templateWidth = 72f;

		// Token: 0x0400D410 RID: 54288
		protected Vector2 defaultSpacing = new Vector2(4f, 4f);

		// Token: 0x0400D411 RID: 54289
		protected float defaultVerticalSpacing = 4f;

		// Token: 0x0400D412 RID: 54290
		protected int defaultColumns = 10;

		// Token: 0x0400D413 RID: 54291
		protected int defaultMainDeckRows = 4;

		// Token: 0x0400D414 RID: 54292
		protected int defaultExtraDeckRows = 1;

		// Token: 0x0400D415 RID: 54293
		protected int defaultSideDeckRows = 1;

		// Token: 0x0400D416 RID: 54294
		protected Vector3 dragCardScale = new Vector3(1.7f, 1.7f, 1f);

		// Token: 0x0400D417 RID: 54295
		public bool deckLoaded;

		// Token: 0x0400D418 RID: 54296
		public int mainCount;

		// Token: 0x0400D419 RID: 54297
		public int extraCount;

		// Token: 0x0400D41A RID: 54298
		public int sideCount;

		// Token: 0x0400D41B RID: 54299
		public List<SelectionButton_CardInDeck> cards;

		// Token: 0x0400D41D RID: 54301
		protected string deckNameWithType;

		// Token: 0x0400D41E RID: 54302
		protected string deckFileName;

		// Token: 0x0400D41F RID: 54303
		protected string deckType;

		// Token: 0x0400D420 RID: 54304
		protected bool needSave;

		// Token: 0x0400D421 RID: 54305
		private ShortcutIcon[] shortcutIcons;

		// Token: 0x02001424 RID: 5156
		public enum DeckLocation
		{
			// Token: 0x0400D423 RID: 54307
			MainDeck = 1,
			// Token: 0x0400D424 RID: 54308
			ExtraDeck,
			// Token: 0x0400D425 RID: 54309
			SideDeck = 4,
			// Token: 0x0400D426 RID: 54310
			All = 7
		}

		// Token: 0x02001425 RID: 5157
		public enum Condition
		{
			// Token: 0x0400D428 RID: 54312
			Editable,
			// Token: 0x0400D429 RID: 54313
			ChangeSide,
			// Token: 0x0400D42A RID: 54314
			Pickup,
			// Token: 0x0400D42B RID: 54315
			NonEditable
		}
	}
}
