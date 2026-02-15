using System;
using System.Collections.Generic;
using System.Linq;
using MDPro3.Duel.YGOSharp;
using MDPro3.Servant;
using MDPro3.UI.Popup;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001418 RID: 5144
	public class CardCollectionView : UIWidget
	{
		// Token: 0x170012CE RID: 4814
		// (get) Token: 0x060094BE RID: 38078 RVA: 0x00153EAC File Offset: 0x001520AC
		protected SelectionButton ButtonNoItem
		{
			get
			{
				return this.m_ButtonNoItem = ((this.m_ButtonNoItem != null) ? this.m_ButtonNoItem : base.Manager.GetElement<SelectionButton>("NoItemButton"));
			}
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x060094BF RID: 38079 RVA: 0x00153EE8 File Offset: 0x001520E8
		protected GamepadCursor CursorWindowSelect
		{
			get
			{
				return this.m_CursorWindowSelect = ((this.m_CursorWindowSelect != null) ? this.m_CursorWindowSelect : base.Manager.GetElement<GamepadCursor>("CursorWindowSelect"));
			}
		}

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x060094C0 RID: 38080 RVA: 0x00153F24 File Offset: 0x00152124
		protected SelectionToggle_CardCollectionTab ToggleCardList
		{
			get
			{
				return this.m_ToggleCardList = ((this.m_ToggleCardList != null) ? this.m_ToggleCardList : base.Manager.GetNestedElement<SelectionToggle_CardCollectionTab>("TabArea/CardListToggle"));
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x060094C1 RID: 38081 RVA: 0x00153F60 File Offset: 0x00152160
		protected SelectionToggle_CardCollectionTab ToggleBookmark
		{
			get
			{
				return this.m_ToggleBookmark = ((this.m_ToggleBookmark != null) ? this.m_ToggleBookmark : base.Manager.GetNestedElement<SelectionToggle_CardCollectionTab>("TabArea/BookmarkToggle"));
			}
		}

		// Token: 0x170012D2 RID: 4818
		// (get) Token: 0x060094C2 RID: 38082 RVA: 0x00153F9C File Offset: 0x0015219C
		protected SelectionToggle_CardCollectionTab ToggleHistory
		{
			get
			{
				return this.m_ToggleHistory = ((this.m_ToggleHistory != null) ? this.m_ToggleHistory : base.Manager.GetNestedElement<SelectionToggle_CardCollectionTab>("TabArea/HistoryToggle"));
			}
		}

		// Token: 0x170012D3 RID: 4819
		// (get) Token: 0x060094C3 RID: 38083 RVA: 0x00153FD8 File Offset: 0x001521D8
		protected GameObject FilterAndSortArea
		{
			get
			{
				return this.m_FilterAndSortArea = ((this.m_FilterAndSortArea != null) ? this.m_FilterAndSortArea : base.Manager.GetElement("FilterAndSortArea"));
			}
		}

		// Token: 0x170012D4 RID: 4820
		// (get) Token: 0x060094C4 RID: 38084 RVA: 0x00154014 File Offset: 0x00152214
		public SelectionInputField InputSearch
		{
			get
			{
				return this.m_InputSearch = ((this.m_InputSearch != null) ? this.m_InputSearch : base.Manager.GetNestedElement<SelectionInputField>("FilterAndSortArea/InputField"));
			}
		}

		// Token: 0x170012D5 RID: 4821
		// (get) Token: 0x060094C5 RID: 38085 RVA: 0x00154050 File Offset: 0x00152250
		protected SelectionButton ButtonSearch
		{
			get
			{
				return this.m_ButtonSearch = ((this.m_ButtonSearch != null) ? this.m_ButtonSearch : base.Manager.GetNestedElement<SelectionButton>("FilterAndSortArea/SearchButton"));
			}
		}

		// Token: 0x170012D6 RID: 4822
		// (get) Token: 0x060094C6 RID: 38086 RVA: 0x0015408C File Offset: 0x0015228C
		protected SelectionToggle_CardFilter ToggleFilter
		{
			get
			{
				return this.m_ToggleFilter = ((this.m_ToggleFilter != null) ? this.m_ToggleFilter : base.Manager.GetNestedElement<SelectionToggle_CardFilter>("FilterAndSortArea/FilterToggle"));
			}
		}

		// Token: 0x170012D7 RID: 4823
		// (get) Token: 0x060094C7 RID: 38087 RVA: 0x001540C8 File Offset: 0x001522C8
		protected SelectionButton ButtonSort
		{
			get
			{
				return this.m_ButtonSort = ((this.m_ButtonSort != null) ? this.m_ButtonSort : base.Manager.GetNestedElement<SelectionButton>("FilterAndSortArea/SortButton"));
			}
		}

		// Token: 0x170012D8 RID: 4824
		// (get) Token: 0x060094C8 RID: 38088 RVA: 0x00154104 File Offset: 0x00152304
		protected SelectionButton ButtonClear
		{
			get
			{
				return this.m_ButtonClear = ((this.m_ButtonClear != null) ? this.m_ButtonClear : base.Manager.GetNestedElement<SelectionButton>("FilterAndSortArea/ClearButton"));
			}
		}

		// Token: 0x170012D9 RID: 4825
		// (get) Token: 0x060094C9 RID: 38089 RVA: 0x00154140 File Offset: 0x00152340
		protected GameObject RelatedArea
		{
			get
			{
				return this.m_RelatedArea = ((this.m_RelatedArea != null) ? this.m_RelatedArea : base.Manager.GetNestedElement("RelatedArea"));
			}
		}

		// Token: 0x170012DA RID: 4826
		// (get) Token: 0x060094CA RID: 38090 RVA: 0x0015417C File Offset: 0x0015237C
		protected SelectionButton ButtonRelatedCard
		{
			get
			{
				return this.m_ButtonRelatedCard = ((this.m_ButtonRelatedCard != null) ? this.m_ButtonRelatedCard : base.Manager.GetNestedElement<SelectionButton>("RelatedArea/RelatedCard/RelatedCardButton"));
			}
		}

		// Token: 0x170012DB RID: 4827
		// (get) Token: 0x060094CB RID: 38091 RVA: 0x001541B8 File Offset: 0x001523B8
		protected TextMeshProUGUI TextRelatedCard
		{
			get
			{
				return this.m_TextRelatedCard = ((this.m_TextRelatedCard != null) ? this.m_TextRelatedCard : base.Manager.GetNestedElement<TextMeshProUGUI>("RelatedArea/RelatedCard/RelatedCardText"));
			}
		}

		// Token: 0x170012DC RID: 4828
		// (get) Token: 0x060094CC RID: 38092 RVA: 0x001541F4 File Offset: 0x001523F4
		protected SelectionButton ButtonClose
		{
			get
			{
				return this.m_ButtonClose = ((this.m_ButtonClose != null) ? this.m_ButtonClose : base.Manager.GetNestedElement<SelectionButton>("RelatedArea/CloseButton"));
			}
		}

		// Token: 0x170012DD RID: 4829
		// (get) Token: 0x060094CD RID: 38093 RVA: 0x00154230 File Offset: 0x00152430
		protected RectTransform CollectionAreaCenter
		{
			get
			{
				return this.m_CollectionAreaCenter = ((this.m_CollectionAreaCenter != null) ? this.m_CollectionAreaCenter : base.Manager.GetNestedElement<RectTransform>("CardListArea/CollectionAreaCenter"));
			}
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x060094CE RID: 38094 RVA: 0x0015426C File Offset: 0x0015246C
		protected ScrollRect ScrollRect
		{
			get
			{
				return this.m_ScrollRect = ((this.m_ScrollRect != null) ? this.m_ScrollRect : base.Manager.GetNestedElement<ScrollRect>("CardListArea/CardList"));
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x060094CF RID: 38095 RVA: 0x001542A8 File Offset: 0x001524A8
		protected GameObject Template
		{
			get
			{
				return this.m_Template = ((this.m_Template != null) ? this.m_Template : base.Manager.GetNestedElement("CardListArea/CardList/template"));
			}
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x060094D0 RID: 38096 RVA: 0x001542E4 File Offset: 0x001524E4
		protected TextMeshProUGUI TextNoItem
		{
			get
			{
				return this.m_TextNoItem = ((this.m_TextNoItem != null) ? this.m_TextNoItem : base.Manager.GetNestedElement<TextMeshProUGUI>("CardListArea/NoItemText"));
			}
		}

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x060094D1 RID: 38097 RVA: 0x00154320 File Offset: 0x00152520
		protected DoTweenManager Loading
		{
			get
			{
				return this.m_Loading = ((this.m_Loading != null) ? this.m_Loading : base.Manager.GetNestedElement<DoTweenManager>("CardListArea/Loading"));
			}
		}

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x060094D2 RID: 38098 RVA: 0x0015435C File Offset: 0x0015255C
		protected DropArea DropArea
		{
			get
			{
				return this.m_DropArea = ((this.m_DropArea != null) ? this.m_DropArea : base.Manager.GetElement<DropArea>("DropArea"));
			}
		}

		// Token: 0x060094D3 RID: 38099 RVA: 0x00154398 File Offset: 0x00152598
		public void SetNoItemButtonNavigationEvent(MoveDirection direction, UnityAction action)
		{
			this.ButtonNoItem.SetNavigationEvent(direction, action);
		}

		// Token: 0x060094D4 RID: 38100 RVA: 0x001543A8 File Offset: 0x001525A8
		public void SelectDefaultItem()
		{
			if (this.superScrollView.gameObjects.Count > 0)
			{
				EventSystem.current.SetSelectedGameObject(this.superScrollView.gameObjects[0]);
				return;
			}
			if (this.showingRelatedCards)
			{
				this.ButtonRelatedCard.GetSelectable().Select();
				return;
			}
			this.ButtonNoItem.GetSelectable().Select();
		}

		// Token: 0x060094D5 RID: 38101 RVA: 0x00154410 File Offset: 0x00152610
		public void SelectNearestCard(Vector3 position)
		{
			if (this.superScrollView.gameObjects.Count == 0)
			{
				this.ButtonNoItem.GetSelectable().Select();
				return;
			}
			Dictionary<GameObject, float> distance = new Dictionary<GameObject, float>();
			foreach (GameObject card in this.superScrollView.gameObjects)
			{
				distance.Add(card, Tools.CalculateWeightedDistance(position, card.transform.GetChild(0).position, 'x'));
			}
			EventSystem.current.SetSelectedGameObject(distance.Aggregate(delegate(KeyValuePair<GameObject, float> left, KeyValuePair<GameObject, float> right)
			{
				if (left.Value >= right.Value)
				{
					return right;
				}
				return left;
			}).Key);
		}

		// Token: 0x060094D6 RID: 38102 RVA: 0x001544E4 File Offset: 0x001526E4
		public void PrintSearchCards(string text = "")
		{
			List<int> cards = new List<int>();
			List<Card> results = CardsManager.Search(this.InputSearch.InputField.text, CardCollectionView.filters, DeckEditor.banlist, CardCollectionView.packName);
			this.SortCards(results);
			foreach (Card card in results)
			{
				cards.Add(card.Id);
			}
			this.ButtonSearch.SetButtonText((cards.Count == 0) ? InterString.Get("搜索", 0) : cards.Count.ToString());
			this.PrintCards(cards);
		}

		// Token: 0x060094D7 RID: 38103 RVA: 0x001545A0 File Offset: 0x001527A0
		protected void PrintRelatedCards(Card data)
		{
			this.relatedCardData = data;
			List<int> cards = new List<int>();
			List<Card> results = CardsManager.RelatedSearch(data.Id);
			this.SortCards(results);
			foreach (Card card in results)
			{
				cards.Add(card.Id);
			}
			this.ButtonSearch.SetButtonText((cards.Count == 0) ? InterString.Get("搜索", 0) : cards.Count.ToString());
			this.PrintCards(cards);
		}

		// Token: 0x060094D8 RID: 38104 RVA: 0x0015464C File Offset: 0x0015284C
		public void PrintBookmarkCards()
		{
			this.PrintCards(CardRarity.GetBookCards());
		}

		// Token: 0x060094D9 RID: 38105 RVA: 0x00154659 File Offset: 0x00152859
		public void PrintHistoryCards()
		{
			this.PrintCards(this.historyCards);
		}

		// Token: 0x060094DA RID: 38106 RVA: 0x00154668 File Offset: 0x00152868
		private void SortCards(List<Card> cards)
		{
			switch (CardCollectionView._SortOrder)
			{
			case CardCollectionView.SortOrder.ByType:
				cards.Sort(CardsManager.ComparisonOfCard());
				return;
			case CardCollectionView.SortOrder.ByTypeReverse:
				cards.Sort(CardsManager.ComparisonOfCardReverse());
				return;
			case CardCollectionView.SortOrder.ByLevelUp:
				cards.Sort(CardsManager.ComparisonOfCard_LV_Up());
				return;
			case CardCollectionView.SortOrder.ByLevelDown:
				cards.Sort(CardsManager.ComparisonOfCard_LV_Down());
				return;
			case CardCollectionView.SortOrder.ByAttackUp:
				cards.Sort(CardsManager.ComparisonOfCard_ATK_Up());
				return;
			case CardCollectionView.SortOrder.ByAttackDown:
				cards.Sort(CardsManager.ComparisonOfCard_ATK_Down());
				return;
			case CardCollectionView.SortOrder.ByDefenceUp:
				cards.Sort(CardsManager.ComparisonOfCard_DEF_Up());
				return;
			case CardCollectionView.SortOrder.ByDefenceDown:
				cards.Sort(CardsManager.ComparisonOfCard_DEF_Down());
				return;
			case CardCollectionView.SortOrder.ByRarityUp:
				cards.Sort(CardsManager.ComparisonOfCard_Rarity_Up());
				return;
			case CardCollectionView.SortOrder.ByRarityDown:
				cards.Sort(CardsManager.ComparisonOfCard_Rarity_Down());
				return;
			case CardCollectionView.SortOrder.ByGPUp:
				cards.Sort(CardsManager.ComparisonOfCard_GP_Up());
				return;
			case CardCollectionView.SortOrder.ByGPDown:
				cards.Sort(CardsManager.ComparisonOfCard_GP_Down());
				return;
			default:
				return;
			}
		}

		// Token: 0x060094DB RID: 38107 RVA: 0x00154743 File Offset: 0x00152943
		public void AddHistoryCard(int code)
		{
			this.historyCards.Remove(code);
			this.historyCards.Insert(0, code);
			if (this.area == CardCollectionView.Area.History)
			{
				this.PrintHistoryCards();
			}
		}

		// Token: 0x060094DC RID: 38108 RVA: 0x00154770 File Offset: 0x00152970
		public void AddHistoryCards(List<int> codes)
		{
			foreach (int code in codes)
			{
				this.historyCards.Remove(code);
				this.historyCards.Insert(0, code);
			}
			if (this.area == CardCollectionView.Area.History)
			{
				this.PrintHistoryCards();
			}
		}

		// Token: 0x060094DD RID: 38109 RVA: 0x001547E0 File Offset: 0x001529E0
		public void SetSortIcon(Sprite icon)
		{
			this.ButtonSort.SetIconSprite(icon);
		}

		// Token: 0x060094DE RID: 38110 RVA: 0x001547EE File Offset: 0x001529EE
		public void SetSortText(string text)
		{
			this.ButtonSort.SetButtonText(text);
		}

		// Token: 0x060094DF RID: 38111 RVA: 0x001547FC File Offset: 0x001529FC
		public void SetCursor(bool selected)
		{
			this.CursorWindowSelect.Show = selected;
			ShortcutIcon[] componentsInChildren = base.transform.GetComponentsInChildren<ShortcutIcon>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].GroupShow = selected;
			}
		}

		// Token: 0x060094E0 RID: 38112 RVA: 0x00154839 File Offset: 0x00152A39
		public void OnTabRight()
		{
			this.ToggleCardList.OnRightSelection();
		}

		// Token: 0x060094E1 RID: 38113 RVA: 0x00154846 File Offset: 0x00152A46
		public Vector3 GetRubbishBinPositon()
		{
			if (this.area == CardCollectionView.Area.Collection)
			{
				return this.CollectionAreaCenter.position;
			}
			return this.ToggleCardList.transform.position;
		}

		// Token: 0x060094E2 RID: 38114 RVA: 0x0015486C File Offset: 0x00152A6C
		public void ShowFilters()
		{
			Addressables.InstantiateAsync("Popup/PopupSearchFilter.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(Program.instance.ui_.popup, false);
				result.Result.GetComponent<PopupSearchFilter>().Show();
			};
		}

		// Token: 0x060094E3 RID: 38115 RVA: 0x001548B0 File Offset: 0x00152AB0
		public void ResetFilters()
		{
			AudioManager.PlaySE("SE_MENU_DECIDE", 1f);
			CardCollectionView.filters.Clear();
			CardCollectionView.packName = string.Empty;
			SelectionToggle_CardFilter.Instance.SetToggleOff(true);
			this.InputSearch.InputField.text = string.Empty;
			this.PrintSearchCards("");
		}

		// Token: 0x060094E4 RID: 38116 RVA: 0x0015490C File Offset: 0x00152B0C
		public void ShowSortOrder()
		{
			Addressables.InstantiateAsync("Popup/PopupSearchOrder.prefab", null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent(Program.instance.ui_.popup, false);
				result.Result.GetComponent<PopupSearchOrder>().Show();
			};
		}

		// Token: 0x060094E5 RID: 38117 RVA: 0x0015494D File Offset: 0x00152B4D
		public void ActivateInputField()
		{
			this.InputSearch.InputField.ActivateInputField();
		}

		// Token: 0x060094E6 RID: 38118 RVA: 0x0015495F File Offset: 0x00152B5F
		public TMP_InputField GetInputField()
		{
			return this.InputSearch.InputField;
		}

		// Token: 0x060094E7 RID: 38119 RVA: 0x0015496C File Offset: 0x00152B6C
		public void RefreshCardCount()
		{
			foreach (GameObject gameObject in this.superScrollView.gameObjects)
			{
				gameObject.GetComponent<SelectionButton_CardInCollection>().RefreshCountIcon();
			}
		}

		// Token: 0x060094E8 RID: 38120 RVA: 0x001549C8 File Offset: 0x00152BC8
		public void SetCardInfoType(DeckEditorUI.CardInfoType type)
		{
			foreach (GameObject gameObject in this.superScrollView.gameObjects)
			{
				gameObject.GetComponent<SelectionButton_CardInCollection>().RefreshIcons();
			}
		}

		// Token: 0x060094E9 RID: 38121 RVA: 0x00154A24 File Offset: 0x00152C24
		public void ShowArea(CardCollectionView.Area area)
		{
			if (this.area == area)
			{
				return;
			}
			this.FilterAndSortArea.SetActive(area == CardCollectionView.Area.Collection);
			this.area = area;
			if (area == CardCollectionView.Area.Collection)
			{
				if (this.showingRelatedCards)
				{
					this.ShowRelatedCard(this.relatedCardData);
				}
				else
				{
					this.HideRelatedCardArea();
					this.PrintSearchCards("");
				}
			}
			else if (area == CardCollectionView.Area.Bookmark)
			{
				this.HideRelatedCardArea();
				this.PrintBookmarkCards();
			}
			else if (area == CardCollectionView.Area.History)
			{
				this.HideRelatedCardArea();
				this.PrintHistoryCards();
			}
			this.SetDropArea();
		}

		// Token: 0x060094EA RID: 38122 RVA: 0x00154AA8 File Offset: 0x00152CA8
		public void RefreshRarity(int code)
		{
			foreach (GameObject gameObject in this.superScrollView.gameObjects)
			{
				gameObject.GetComponent<SelectionButton_CardInCollection>().RefreshRarity(code);
			}
			if (this.showingRelatedCards)
			{
				this.ButtonRelatedCard.GetComponent<CardRawImageHandler>().RefreshRarity(code);
			}
		}

		// Token: 0x060094EB RID: 38123 RVA: 0x00154B1C File Offset: 0x00152D1C
		public void ShowRelatedCard(Card data)
		{
			this.showingRelatedCards = true;
			this.ToggleCardList.SetToggleOn(false);
			this.FilterAndSortArea.SetActive(true);
			this.InputSearch.SetInteractable(false);
			this.ButtonSearch.SetInteractable(false);
			this.ToggleFilter.SetInteractable(false);
			this.ButtonSort.SetInteractable(false);
			this.ButtonClear.SetInteractable(false);
			this.RelatedArea.SetActive(true);
			this.ButtonRelatedCard.GetComponent<CardRawImageHandler>().SetCard(data);
			this.TextRelatedCard.text = InterString.Get("「[?]」的相关卡片", data.Name, 0);
			this.PrintRelatedCards(data);
		}

		// Token: 0x060094EC RID: 38124 RVA: 0x00154BC4 File Offset: 0x00152DC4
		public void HideRelatedCard()
		{
			this.showingRelatedCards = false;
			this.HideRelatedCardArea();
			this.PrintSearchCards("");
		}

		// Token: 0x060094ED RID: 38125 RVA: 0x00154BE0 File Offset: 0x00152DE0
		protected void HideRelatedCardArea()
		{
			this.InputSearch.SetInteractable(true);
			this.ButtonSearch.SetInteractable(true);
			this.ToggleFilter.SetInteractable(true);
			this.ButtonSort.SetInteractable(true);
			this.ButtonClear.SetInteractable(true);
			this.RelatedArea.SetActive(false);
		}

		// Token: 0x060094EE RID: 38126 RVA: 0x00154C35 File Offset: 0x00152E35
		public GameObject GetUpNavigationObject()
		{
			if (!this.showingRelatedCards)
			{
				return null;
			}
			return this.ButtonRelatedCard.gameObject;
		}

		// Token: 0x060094EF RID: 38127 RVA: 0x00154C4C File Offset: 0x00152E4C
		public void OnRalatedAreaNavigationDown()
		{
			if (Program.instance.deckEditor.lastSelectedCardInCollection != null)
			{
				UserInput.NextSelectionIsAxis = true;
				EventSystem.current.SetSelectedGameObject(Program.instance.deckEditor.lastSelectedCardInCollection.gameObject);
			}
		}

		// Token: 0x060094F0 RID: 38128 RVA: 0x00154C89 File Offset: 0x00152E89
		public void SetDropAreaActive(bool active)
		{
			this.DropArea.active = active;
		}

		// Token: 0x060094F1 RID: 38129 RVA: 0x00154C98 File Offset: 0x00152E98
		public void SetBookmarkDropArea(int code)
		{
			if (this.area != CardCollectionView.Area.Bookmark)
			{
				return;
			}
			this.DropArea.ClearLabels();
			if (CardRarity.CardBookmarked(code))
			{
				this.DropArea.SetShowLabel("CanNotAddBookmark");
			}
			else
			{
				this.DropArea.SetShowLabel("AddBookmark");
			}
			if (DeckEditor.UseMobileLayout)
			{
				this.DropArea.SetShowLabel("Right");
				this.DropArea.SetShowLabel("MainDeck");
				this.DropArea.SetShowLabel("ExtraDeck");
				this.DropArea.SetShowLabel("SideDeck");
			}
		}

		// Token: 0x060094F2 RID: 38130 RVA: 0x00154D2C File Offset: 0x00152F2C
		protected override void Awake()
		{
			base.Awake();
			CardCollectionView._SortOrder = CardCollectionView.SortOrder.ByType;
			this.Template.transform.SetParent(base.transform, false);
			this.Template.SetActive(false);
			this.superScrollView = new SuperScrollView(6, (float)(DeckEditor.UseMobileLayout ? 158 : 88), (float)(DeckEditor.UseMobileLayout ? 239 : 143), (float)(DeckEditor.UseMobileLayout ? 10 : 5), (float)(DeckEditor.UseMobileLayout ? 10 : 5), this.Template, new Action<string[], GameObject>(this.ItemOnListRefresh), this.ScrollRect, 2);
			this.InputSearch.InputField.onEndEdit.AddListener(new UnityAction<string>(this.PrintSearchCards));
			this.ButtonSearch.SetClickEvent(delegate
			{
				this.PrintSearchCards("");
			});
			this.ButtonSort.SetClickEvent(new UnityAction(this.ShowSortOrder));
			this.ButtonClear.SetClickEvent(new UnityAction(this.ResetFilters));
			this.RelatedArea.SetActive(false);
			this.ButtonRelatedCard.SetClickEvent(delegate
			{
				Program.instance.deckEditor.GetUI<DeckEditorUI>().ShowDetail(this.relatedCardData);
			});
			this.ButtonClose.SetClickEvent(new UnityAction(this.HideRelatedCard));
			if (DeckEditor.condition != DeckEditor.Condition.ChangeSide)
			{
				this.SetDropArea();
				return;
			}
			this.defaultArea = CardCollectionView.Area.History;
		}

		// Token: 0x060094F3 RID: 38131 RVA: 0x00154E82 File Offset: 0x00153082
		protected void OnDestroy()
		{
			SuperScrollView superScrollView = this.superScrollView;
			if (superScrollView != null)
			{
				superScrollView.Clear();
			}
			CardCollectionView.filters.Clear();
			CardCollectionView.packName = string.Empty;
		}

		// Token: 0x060094F4 RID: 38132 RVA: 0x00154EA9 File Offset: 0x001530A9
		protected void ItemOnListRefresh(string[] tasks, GameObject item)
		{
			SelectionButton_CardInCollection component = item.GetComponent<SelectionButton_CardInCollection>();
			component.CardCode = int.Parse(tasks[0]);
			component.cardCollectionView = this;
		}

		// Token: 0x060094F5 RID: 38133 RVA: 0x00154EC8 File Offset: 0x001530C8
		protected void PrintCards(List<int> cards)
		{
			this.TextNoItem.gameObject.SetActive(cards.Count == 0);
			this.printedCards = cards;
			List<string[]> args = new List<string[]>();
			for (int i = 0; i < cards.Count; i++)
			{
				string[] arg = new string[] { cards[i].ToString() };
				args.Add(arg);
			}
			this.superScrollView.Print(args);
			if (Program.instance.deckEditor.ResponseRegion == DeckEditorUI.ResponseRegion.Collection)
			{
				this.SelectDefaultItem();
			}
		}

		// Token: 0x060094F6 RID: 38134 RVA: 0x00154F50 File Offset: 0x00153150
		protected void SetDropArea()
		{
			if (this.area == CardCollectionView.Area.Bookmark)
			{
				this.DropArea.ClearLabels();
				this.DropArea.SetShowLabel("AddBookmark");
			}
			else
			{
				this.DropArea.ClearLabels();
				if (DeckEditor.condition != DeckEditor.Condition.ChangeSide)
				{
					this.DropArea.SetShowLabel("RemoveDeck");
				}
			}
			if (DeckEditor.UseMobileLayout)
			{
				this.DropArea.SetShowLabel("MainDeck");
				this.DropArea.SetShowLabel("ExtraDeck");
				this.DropArea.SetShowLabel("SideDeck");
				if (DeckEditor.condition != DeckEditor.Condition.ChangeSide)
				{
					this.DropArea.SetShowLabel("Right");
				}
			}
		}

		// Token: 0x0400D2F6 RID: 54006
		private const string LABEL_SBN_NOITEM = "NoItemButton";

		// Token: 0x0400D2F7 RID: 54007
		private SelectionButton m_ButtonNoItem;

		// Token: 0x0400D2F8 RID: 54008
		private const string LABEL_GPC_CURSORWINDOWSELECT = "CursorWindowSelect";

		// Token: 0x0400D2F9 RID: 54009
		private GamepadCursor m_CursorWindowSelect;

		// Token: 0x0400D2FA RID: 54010
		private const string LABEL_STG_CARDLIST = "TabArea/CardListToggle";

		// Token: 0x0400D2FB RID: 54011
		private SelectionToggle_CardCollectionTab m_ToggleCardList;

		// Token: 0x0400D2FC RID: 54012
		private const string LABEL_STG_BOOKMARK = "TabArea/BookmarkToggle";

		// Token: 0x0400D2FD RID: 54013
		private SelectionToggle_CardCollectionTab m_ToggleBookmark;

		// Token: 0x0400D2FE RID: 54014
		private const string LABEL_STG_HISTORY = "TabArea/HistoryToggle";

		// Token: 0x0400D2FF RID: 54015
		private SelectionToggle_CardCollectionTab m_ToggleHistory;

		// Token: 0x0400D300 RID: 54016
		private const string LABEL_GO_FILTERANDSORTAREA = "FilterAndSortArea";

		// Token: 0x0400D301 RID: 54017
		private GameObject m_FilterAndSortArea;

		// Token: 0x0400D302 RID: 54018
		private const string LABEL_IPT_SEARCH = "FilterAndSortArea/InputField";

		// Token: 0x0400D303 RID: 54019
		private SelectionInputField m_InputSearch;

		// Token: 0x0400D304 RID: 54020
		private const string LABEL_SBN_SEARCH = "FilterAndSortArea/SearchButton";

		// Token: 0x0400D305 RID: 54021
		private SelectionButton m_ButtonSearch;

		// Token: 0x0400D306 RID: 54022
		private const string LABEL_STG_FILTER = "FilterAndSortArea/FilterToggle";

		// Token: 0x0400D307 RID: 54023
		private SelectionToggle_CardFilter m_ToggleFilter;

		// Token: 0x0400D308 RID: 54024
		private const string LABEL_SBN_SORT = "FilterAndSortArea/SortButton";

		// Token: 0x0400D309 RID: 54025
		private SelectionButton m_ButtonSort;

		// Token: 0x0400D30A RID: 54026
		private const string LABEL_SBN_CLEAR = "FilterAndSortArea/ClearButton";

		// Token: 0x0400D30B RID: 54027
		private SelectionButton m_ButtonClear;

		// Token: 0x0400D30C RID: 54028
		private const string LABEL_GO_RELATEDAREA = "RelatedArea";

		// Token: 0x0400D30D RID: 54029
		private GameObject m_RelatedArea;

		// Token: 0x0400D30E RID: 54030
		private const string LABEL_SBN_RELATEDCARD = "RelatedArea/RelatedCard/RelatedCardButton";

		// Token: 0x0400D30F RID: 54031
		private SelectionButton m_ButtonRelatedCard;

		// Token: 0x0400D310 RID: 54032
		private const string LABEL_TXT_RELATEDCARD = "RelatedArea/RelatedCard/RelatedCardText";

		// Token: 0x0400D311 RID: 54033
		private TextMeshProUGUI m_TextRelatedCard;

		// Token: 0x0400D312 RID: 54034
		private const string LABEL_BTN_CLOSE = "RelatedArea/CloseButton";

		// Token: 0x0400D313 RID: 54035
		private SelectionButton m_ButtonClose;

		// Token: 0x0400D314 RID: 54036
		private const string LABEL_RT_COLLECTIONAREACENTER = "CardListArea/CollectionAreaCenter";

		// Token: 0x0400D315 RID: 54037
		private RectTransform m_CollectionAreaCenter;

		// Token: 0x0400D316 RID: 54038
		private const string LABEL_SR_CARDLIST = "CardListArea/CardList";

		// Token: 0x0400D317 RID: 54039
		private ScrollRect m_ScrollRect;

		// Token: 0x0400D318 RID: 54040
		private const string LABEL_GO_TEMPLATE = "CardListArea/CardList/template";

		// Token: 0x0400D319 RID: 54041
		private GameObject m_Template;

		// Token: 0x0400D31A RID: 54042
		private const string LABEL_TXT_NOITEM = "CardListArea/NoItemText";

		// Token: 0x0400D31B RID: 54043
		private TextMeshProUGUI m_TextNoItem;

		// Token: 0x0400D31C RID: 54044
		private const string LABEL_DTM_LOADING = "CardListArea/Loading";

		// Token: 0x0400D31D RID: 54045
		private DoTweenManager m_Loading;

		// Token: 0x0400D31E RID: 54046
		private const string LABEL_DA_DROPAREA = "DropArea";

		// Token: 0x0400D31F RID: 54047
		private DropArea m_DropArea;

		// Token: 0x0400D320 RID: 54048
		[HideInInspector]
		public CardCollectionView.Area area;

		// Token: 0x0400D321 RID: 54049
		[HideInInspector]
		public CardCollectionView.Area defaultArea;

		// Token: 0x0400D322 RID: 54050
		[HideInInspector]
		public bool showingRelatedCards;

		// Token: 0x0400D323 RID: 54051
		public static CardCollectionView.SortOrder _SortOrder = CardCollectionView.SortOrder.ByType;

		// Token: 0x0400D324 RID: 54052
		public SuperScrollView superScrollView;

		// Token: 0x0400D325 RID: 54053
		public static List<long> filters = new List<long>();

		// Token: 0x0400D326 RID: 54054
		public static string packName = string.Empty;

		// Token: 0x0400D327 RID: 54055
		[HideInInspector]
		public List<int> historyCards = new List<int>();

		// Token: 0x0400D328 RID: 54056
		[HideInInspector]
		public List<int> printedCards;

		// Token: 0x0400D329 RID: 54057
		protected Card relatedCardData;

		// Token: 0x02001419 RID: 5145
		public enum Area
		{
			// Token: 0x0400D32B RID: 54059
			Collection,
			// Token: 0x0400D32C RID: 54060
			Bookmark,
			// Token: 0x0400D32D RID: 54061
			History
		}

		// Token: 0x0200141A RID: 5146
		public enum SortOrder
		{
			// Token: 0x0400D32F RID: 54063
			ByType = 1,
			// Token: 0x0400D330 RID: 54064
			ByTypeReverse,
			// Token: 0x0400D331 RID: 54065
			ByLevelUp,
			// Token: 0x0400D332 RID: 54066
			ByLevelDown,
			// Token: 0x0400D333 RID: 54067
			ByAttackUp,
			// Token: 0x0400D334 RID: 54068
			ByAttackDown,
			// Token: 0x0400D335 RID: 54069
			ByDefenceUp,
			// Token: 0x0400D336 RID: 54070
			ByDefenceDown,
			// Token: 0x0400D337 RID: 54071
			ByRarityUp,
			// Token: 0x0400D338 RID: 54072
			ByRarityDown,
			// Token: 0x0400D339 RID: 54073
			ByGPUp,
			// Token: 0x0400D33A RID: 54074
			ByGPDown
		}
	}
}
