using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomGame.Card;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.InfinityScroll;
using YgomSystem.YGomTMPro;

namespace YgomGame.Deck
{
	// Token: 0x02000FA7 RID: 4007
	public class CardCollectionView : MonoBehaviour
	{
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x06007682 RID: 30338 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007683 RID: 30339 RVA: 0x0000216D File Offset: 0x0000036D
		public CardCollectionView.FilterAndSearchArea m_FilterAndSearchArea
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

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06007684 RID: 30340 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007685 RID: 30341 RVA: 0x0000216D File Offset: 0x0000036D
		public CardCollectionView.RelatedArea m_RelatedArea
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

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06007686 RID: 30342 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007687 RID: 30343 RVA: 0x0000216D File Offset: 0x0000036D
		public CardCollectionView.TabArea m_TabArea
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

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x06007688 RID: 30344 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007689 RID: 30345 RVA: 0x0000216D File Offset: 0x0000036D
		public CardCollectionView.CardDropArea m_CardDropArea
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

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x0600768A RID: 30346 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600768B RID: 30347 RVA: 0x0000216D File Offset: 0x0000036D
		public CardCollectionView.CardListArea m_CardListArea
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

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x0600768C RID: 30348 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600768D RID: 30349 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<List<CardBaseData>> m_CollectionCardGetter
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

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x0600768E RID: 30350 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x0600768F RID: 30351 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<List<CardBaseData>> m_BookmarkCardGetter
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

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06007690 RID: 30352 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06007691 RID: 30353 RVA: 0x0000216D File Offset: 0x0000036D
		public Func<List<CardBaseData>> m_HistoryCardGetter
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

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06007692 RID: 30354 RVA: 0x0000216A File Offset: 0x0000036A
		public List<CardBaseData> m_DataList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06007693 RID: 30355 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007694 RID: 30356 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isRelatedCardActive
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06007695 RID: 30357 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007696 RID: 30358 RVA: 0x0000216D File Offset: 0x0000036D
		public CardCollectionView.Area m_Area
		{
			[CompilerGenerated]
			get
			{
				return CardCollectionView.Area.Collection;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06007697 RID: 30359 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06007698 RID: 30360 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isDismantleMode
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06007699 RID: 30361 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton m_CollectionTab
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x0600769A RID: 30362 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isLoading
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x0600769B RID: 30363 RVA: 0x0000216A File Offset: 0x0000036A
		public RectTransform m_CollectionAreaLayoutGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600769C RID: 30364 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetReguration(int regulationID)
		{
		}

		// Token: 0x0600769D RID: 30365 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDirty()
		{
		}

		// Token: 0x0600769E RID: 30366 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsModified()
		{
			return false;
		}

		// Token: 0x0600769F RID: 30367 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnSaved()
		{
		}

		// Token: 0x060076A0 RID: 30368 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060076A1 RID: 30369 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060076A2 RID: 30370 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToggleTabAndArea(CardCollectionView.Area a)
		{
		}

		// Token: 0x060076A3 RID: 30371 RVA: 0x0000216D File Offset: 0x0000036D
		private void ToggleTab(CardCollectionView.Area a)
		{
		}

		// Token: 0x060076A4 RID: 30372 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToggleNextTabAndArea()
		{
		}

		// Token: 0x060076A5 RID: 30373 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetupTabShortcutIcon(CardCollectionView.Area currentArea)
		{
		}

		// Token: 0x060076A6 RID: 30374 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideTabShortcutIcon()
		{
		}

		// Token: 0x060076A7 RID: 30375 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCollectionTabButton(UnityAction callback)
		{
		}

		// Token: 0x060076A8 RID: 30376 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickBookmarkTabButton(UnityAction callback)
		{
		}

		// Token: 0x060076A9 RID: 30377 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickHistoryTabButton(UnityAction callback)
		{
		}

		// Token: 0x060076AA RID: 30378 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTabChangeShortcutKey(SelectorManager.KeyType main, SelectorManager.KeyType sub)
		{
		}

		// Token: 0x060076AB RID: 30379 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToggleFilterButton(bool filtered)
		{
		}

		// Token: 0x060076AC RID: 30380 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToggleShowAllCardsButton(bool showNotOwned)
		{
		}

		// Token: 0x060076AD RID: 30381 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetInteractableFilterSort(bool interactable)
		{
		}

		// Token: 0x060076AE RID: 30382 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSearchKeyWord(string s)
		{
		}

		// Token: 0x060076AF RID: 30383 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSubmitSearch(UnityAction<string> callback)
		{
		}

		// Token: 0x060076B0 RID: 30384 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickResetSearchButton(UnityAction callback)
		{
		}

		// Token: 0x060076B1 RID: 30385 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickFilterButton(UnityAction callback)
		{
		}

		// Token: 0x060076B2 RID: 30386 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickShowAllCardsButton(UnityAction callback)
		{
		}

		// Token: 0x060076B3 RID: 30387 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickSortButton(UnityAction callback)
		{
		}

		// Token: 0x060076B4 RID: 30388 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetFilterButtonShortcutKey(SelectorManager.KeyType main, SelectorManager.KeyType sub)
		{
		}

		// Token: 0x060076B5 RID: 30389 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSortButtonShortcutKey(SelectorManager.KeyType main, SelectorManager.KeyType sub)
		{
		}

		// Token: 0x060076B6 RID: 30390 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetResetSearchButtonShortcutKey(SelectorManager.KeyType main, SelectorManager.KeyType sub)
		{
		}

		// Token: 0x060076B7 RID: 30391 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetShowAllCardsButtonShortcutKey(SelectorManager.KeyType main, SelectorManager.KeyType sub)
		{
		}

		// Token: 0x060076B8 RID: 30392 RVA: 0x0000216D File Offset: 0x0000036D
		public void ActivateSearchInput()
		{
		}

		// Token: 0x060076B9 RID: 30393 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTextSearchShortcutKey(SelectorManager.KeyType main, SelectorManager.KeyType sub)
		{
		}

		// Token: 0x060076BA RID: 30394 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetUncurrentView()
		{
		}

		// Token: 0x060076BB RID: 30395 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCurrentView()
		{
		}

		// Token: 0x060076BC RID: 30396 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDismantleMode(bool b)
		{
		}

		// Token: 0x060076BD RID: 30397 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSortButtonLabel(SortComparer.Sorter sorter)
		{
		}

		// Token: 0x060076BE RID: 30398 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectCloseRelatedCard(UnityAction callback)
		{
		}

		// Token: 0x060076BF RID: 30399 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickCloseRelatedCard(UnityAction callback)
		{
		}

		// Token: 0x060076C0 RID: 30400 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveRelatedCard(bool b, int id = 0)
		{
		}

		// Token: 0x060076C1 RID: 30401 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDispRelatedCard(bool disp)
		{
		}

		// Token: 0x060076C2 RID: 30402 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectRelatedCard(UnityAction<int> callback)
		{
		}

		// Token: 0x060076C3 RID: 30403 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClickRelatedCard(UnityAction<int> callback)
		{
		}

		// Token: 0x060076C4 RID: 30404 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSelectedShortcutRelatedCard(SelectorManager.KeyType main, SelectorManager.KeyType sub, Action<int> callback)
		{
		}

		// Token: 0x060076C5 RID: 30405 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShortcut(SelectionButton button, DeviceIcon deviceIcon, SelectorManager.KeyType main, SelectorManager.KeyType sub)
		{
		}

		// Token: 0x060076C6 RID: 30406 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispDropArea(bool disp)
		{
		}

		// Token: 0x060076C7 RID: 30407 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetDispDropAreaOver(bool disp)
		{
		}

		// Token: 0x060076C8 RID: 30408 RVA: 0x0000216D File Offset: 0x0000036D
		public void CursorJumpUp()
		{
		}

		// Token: 0x060076C9 RID: 30409 RVA: 0x0000216D File Offset: 0x0000036D
		public void CursorJumpDown()
		{
		}

		// Token: 0x060076CA RID: 30410 RVA: 0x0000216D File Offset: 0x0000036D
		public void CursorJumpRight()
		{
		}

		// Token: 0x060076CB RID: 30411 RVA: 0x0000216D File Offset: 0x0000036D
		public void CursorJumpLeft()
		{
		}

		// Token: 0x060076CC RID: 30412 RVA: 0x0000216D File Offset: 0x0000036D
		public void FocusItemAt(int index, bool immediate, bool select)
		{
		}

		// Token: 0x060076CD RID: 30413 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetIndex(int cardID, int premiumID = -1)
		{
			return 0;
		}

		// Token: 0x060076CE RID: 30414 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDisplayMode(DeckEditViewController2.DisplayMode mode, bool updateScroll = true)
		{
		}

		// Token: 0x060076CF RID: 30415 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateView(bool updateDataCount, bool select = true)
		{
		}

		// Token: 0x060076D0 RID: 30416 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowLoading()
		{
		}

		// Token: 0x060076D1 RID: 30417 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideLoading()
		{
		}

		// Token: 0x060076D2 RID: 30418 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectLeftEdgeClosestItem(Vector2 screenPoint, float angleDot, bool isIni = false)
		{
		}

		// Token: 0x060076D3 RID: 30419 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActiveScroll(bool condition)
		{
		}

		// Token: 0x060076D4 RID: 30420 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator waitDecrementDragCounter()
		{
			return null;
		}

		// Token: 0x060076D5 RID: 30421 RVA: 0x0000216D File Offset: 0x0000036D
		public void SelectLeftUpEdgeItem(bool isIni = true)
		{
		}

		// Token: 0x060076D6 RID: 30422 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnSelectNoItemButton(UnityAction callback)
		{
		}

		// Token: 0x0400AF7F RID: 44927
		private ElementObjectManager m_Eom;

		// Token: 0x0400AF80 RID: 44928
		private static Content m_cci;

		// Token: 0x0400AF81 RID: 44929
		private const string k_ELabelShortcutIconGroup = "ShortcutIconGroup";

		// Token: 0x0400AF82 RID: 44930
		private const string k_ELabelShortcutIcon0 = "Icon0";

		// Token: 0x0400AF83 RID: 44931
		private const string k_ELabelShortcutIcon1 = "Icon1";

		// Token: 0x0400AF84 RID: 44932
		private const string k_ELabelShortcutIconPlus = "IconPlus";

		// Token: 0x0400AF85 RID: 44933
		private const string k_ELabelFilterAndSortArea = "FilterAndSortArea";

		// Token: 0x0400AF86 RID: 44934
		private const string k_ELabelRelatedArea = "RelatedArea";

		// Token: 0x0400AF87 RID: 44935
		private const string k_ELabelTabArea = "TabArea";

		// Token: 0x0400AF88 RID: 44936
		private const string k_ELabelDropArea = "DropArea";

		// Token: 0x0400AF89 RID: 44937
		private const string k_ELabelCardListArea = "CardListArea";

		// Token: 0x0400AF8A RID: 44938
		private const string k_ELabelSelectedWindowCursor = "SelectedWindowCursor";

		// Token: 0x0400AF8B RID: 44939
		private ElementObjectManager m_FilterAndSortEom;

		// Token: 0x0400AF8C RID: 44940
		private ElementObjectManager m_RelatedEom;

		// Token: 0x0400AF8D RID: 44941
		private ElementObjectManager m_TabEom;

		// Token: 0x0400AF8E RID: 44942
		private ElementObjectManager m_DropEom;

		// Token: 0x0400AF8F RID: 44943
		private ElementObjectManager m_CardListEom;

		// Token: 0x0400AF90 RID: 44944
		private RectTransform m_SelectedWindowCursor;

		// Token: 0x0400AF91 RID: 44945
		protected List<int> pooledHistoryCardIDs;

		// Token: 0x0400AF92 RID: 44946
		protected List<int> pooledBookmarkCardIDs;

		// Token: 0x0400AF93 RID: 44947
		private SelectionItem currentItem;

		// Token: 0x0400AF94 RID: 44948
		private DeckEditViewController2.DisplayMode displayMode;

		// Token: 0x0400AF95 RID: 44949
		private int regulationID;

		// Token: 0x0400AF96 RID: 44950
		private bool isModified;

		// Token: 0x0400AF97 RID: 44951
		private bool isCurrentView;

		// Token: 0x02000FA8 RID: 4008
		public abstract class PartialView
		{
			// Token: 0x17000E92 RID: 3730
			// (get) Token: 0x060076D8 RID: 30424 RVA: 0x0000216A File Offset: 0x0000036A
			public GameObject gameObject
			{
				get
				{
					return null;
				}
			}

			// Token: 0x060076D9 RID: 30425
			protected abstract void InitializeElements();

			// Token: 0x060076DA RID: 30426 RVA: 0x0000216D File Offset: 0x0000036D
			protected void Initialize(ElementObjectManager eom)
			{
			}

			// Token: 0x0400AF98 RID: 44952
			protected ElementObjectManager m_Eom;

			// Token: 0x0400AF99 RID: 44953
			protected bool isInitialized;
		}

		// Token: 0x02000FA9 RID: 4009
		public enum Area
		{
			// Token: 0x0400AF9B RID: 44955
			Collection,
			// Token: 0x0400AF9C RID: 44956
			Bookmark,
			// Token: 0x0400AF9D RID: 44957
			History
		}

		// Token: 0x02000FAA RID: 4010
		public class TabArea : CardCollectionView.PartialView
		{
			// Token: 0x17000E93 RID: 3731
			// (get) Token: 0x060076DC RID: 30428 RVA: 0x0000216A File Offset: 0x0000036A
			public SelectionButton m_CardListButton
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000E94 RID: 3732
			// (get) Token: 0x060076DD RID: 30429 RVA: 0x0000216A File Offset: 0x0000036A
			public SelectionButton m_BookmarkButton
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000E95 RID: 3733
			// (get) Token: 0x060076DE RID: 30430 RVA: 0x0000216A File Offset: 0x0000036A
			public SelectionButton m_HistoryButton
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000E96 RID: 3734
			// (get) Token: 0x060076DF RID: 30431 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060076E0 RID: 30432 RVA: 0x0000216D File Offset: 0x0000036D
			public Action<CardCollectionView.Area> onChagedAreaCallback
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

			// Token: 0x060076E1 RID: 30433 RVA: 0x000F660A File Offset: 0x000F480A
			public TabArea(ElementObjectManager eom)
			{
			}

			// Token: 0x060076E2 RID: 30434 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x060076E3 RID: 30435 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnOnClickCardListButtonCallback(Action callback)
			{
			}

			// Token: 0x060076E4 RID: 30436 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnOnClickBookmarkButtonCallback(Action callback)
			{
			}

			// Token: 0x060076E5 RID: 30437 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnOnClickHistoryButtonCallback(Action callback)
			{
			}

			// Token: 0x060076E6 RID: 30438 RVA: 0x0000216D File Offset: 0x0000036D
			public void ToggleByArea(CardCollectionView.Area area)
			{
			}

			// Token: 0x060076E7 RID: 30439 RVA: 0x0000216D File Offset: 0x0000036D
			public void DispShotcutIconByArea(CardCollectionView.Area area)
			{
			}

			// Token: 0x060076E8 RID: 30440 RVA: 0x0000216D File Offset: 0x0000036D
			public void DispShortcutIcons(bool isDisp)
			{
			}

			// Token: 0x060076E9 RID: 30441 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetShortcutIcons(SelectorManager.KeyType main, SelectorManager.KeyType sub)
			{
			}

			// Token: 0x0400AF9E RID: 44958
			private const string k_ELabelImageOn = "ImageOn";

			// Token: 0x0400AF9F RID: 44959
			private const string k_ELabelImageOff = "ImageOff";

			// Token: 0x0400AFA0 RID: 44960
			private const string k_ELabelShortcutIcon = "ShortcutIcon";

			// Token: 0x0400AFA1 RID: 44961
			private const string k_ELabelCardListButton = "CardListButton";

			// Token: 0x0400AFA2 RID: 44962
			private const string k_ELabelBookmarkButton = "BookmarkButton";

			// Token: 0x0400AFA3 RID: 44963
			private const string k_ELabelHistoryButton = "HistoryButton";

			// Token: 0x0400AFA4 RID: 44964
			private CardCollectionView.TabArea.TabButton m_CardListTab;

			// Token: 0x0400AFA5 RID: 44965
			private CardCollectionView.TabArea.TabButton m_BookmarkTab;

			// Token: 0x0400AFA6 RID: 44966
			private CardCollectionView.TabArea.TabButton m_HistoryTab;

			// Token: 0x0400AFA7 RID: 44967
			private Dictionary<CardCollectionView.Area, CardCollectionView.TabArea.TabButton> m_Tabs;

			// Token: 0x0400AFA8 RID: 44968
			private Action OnClickCardListButtonCallback;

			// Token: 0x0400AFA9 RID: 44969
			private Action OnClickBookmarkButtonCallback;

			// Token: 0x0400AFAA RID: 44970
			private Action OnClickHistoryButtonCallback;

			// Token: 0x02000FAB RID: 4011
			private class TabButton : CardCollectionView.PartialView
			{
				// Token: 0x17000E97 RID: 3735
				// (get) Token: 0x060076EA RID: 30442 RVA: 0x0000216A File Offset: 0x0000036A
				// (set) Token: 0x060076EB RID: 30443 RVA: 0x0000216D File Offset: 0x0000036D
				public SelectionButton button
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

				// Token: 0x17000E98 RID: 3736
				// (get) Token: 0x060076EC RID: 30444 RVA: 0x0000216A File Offset: 0x0000036A
				// (set) Token: 0x060076ED RID: 30445 RVA: 0x0000216D File Offset: 0x0000036D
				public RectTransform imageOn
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

				// Token: 0x17000E99 RID: 3737
				// (get) Token: 0x060076EE RID: 30446 RVA: 0x0000216A File Offset: 0x0000036A
				// (set) Token: 0x060076EF RID: 30447 RVA: 0x0000216D File Offset: 0x0000036D
				public RectTransform imageOff
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

				// Token: 0x17000E9A RID: 3738
				// (get) Token: 0x060076F0 RID: 30448 RVA: 0x0000216A File Offset: 0x0000036A
				// (set) Token: 0x060076F1 RID: 30449 RVA: 0x0000216D File Offset: 0x0000036D
				public ShortcutIcon shortcutIcon
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

				// Token: 0x060076F2 RID: 30450 RVA: 0x000F660A File Offset: 0x000F480A
				public TabButton(ElementObjectManager eom)
				{
				}

				// Token: 0x060076F3 RID: 30451 RVA: 0x0000216D File Offset: 0x0000036D
				protected override void InitializeElements()
				{
				}

				// Token: 0x060076F4 RID: 30452 RVA: 0x0000216D File Offset: 0x0000036D
				public void Toggle(bool isOn)
				{
				}
			}
		}

		// Token: 0x02000FAC RID: 4012
		public class FilterAndSearchArea : CardCollectionView.PartialView
		{
			// Token: 0x17000E9B RID: 3739
			// (get) Token: 0x060076F5 RID: 30453 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060076F6 RID: 30454 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_SortButton
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

			// Token: 0x17000E9C RID: 3740
			// (get) Token: 0x060076F7 RID: 30455 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060076F8 RID: 30456 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_FilterButton
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

			// Token: 0x17000E9D RID: 3741
			// (get) Token: 0x060076F9 RID: 30457 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060076FA RID: 30458 RVA: 0x0000216D File Offset: 0x0000036D
			public InputFieldWidget m_InputField
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

			// Token: 0x17000E9E RID: 3742
			// (get) Token: 0x060076FB RID: 30459 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060076FC RID: 30460 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_ClearButton
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

			// Token: 0x17000E9F RID: 3743
			// (get) Token: 0x060076FD RID: 30461 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x060076FE RID: 30462 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_NotOwnButton
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

			// Token: 0x17000EA0 RID: 3744
			// (get) Token: 0x060076FF RID: 30463 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007700 RID: 30464 RVA: 0x0000216D File Offset: 0x0000036D
			public DeviceIcon m_SortShortcutIcon
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

			// Token: 0x17000EA1 RID: 3745
			// (get) Token: 0x06007701 RID: 30465 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007702 RID: 30466 RVA: 0x0000216D File Offset: 0x0000036D
			public DeviceIcon m_FilterShortcutIcon
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

			// Token: 0x17000EA2 RID: 3746
			// (get) Token: 0x06007703 RID: 30467 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007704 RID: 30468 RVA: 0x0000216D File Offset: 0x0000036D
			public DeviceIcon m_InputFieldShortcutIcon
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

			// Token: 0x17000EA3 RID: 3747
			// (get) Token: 0x06007705 RID: 30469 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007706 RID: 30470 RVA: 0x0000216D File Offset: 0x0000036D
			public DeviceIcon m_ClearShortcutIcon
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

			// Token: 0x17000EA4 RID: 3748
			// (get) Token: 0x06007707 RID: 30471 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007708 RID: 30472 RVA: 0x0000216D File Offset: 0x0000036D
			public DeviceIcon m_NotOwnShortcutIcon
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

			// Token: 0x06007709 RID: 30473 RVA: 0x000F660A File Offset: 0x000F480A
			public FilterAndSearchArea(ElementObjectManager eom)
			{
			}

			// Token: 0x0600770A RID: 30474 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x0600770B RID: 30475 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeSortButtonElements()
			{
			}

			// Token: 0x0600770C RID: 30476 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnOnClickSortButtonCallback(Action callback)
			{
			}

			// Token: 0x0600770D RID: 30477 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetSortIcon(SortComparer.Sorter s)
			{
			}

			// Token: 0x0600770E RID: 30478 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeDispButtonElements()
			{
			}

			// Token: 0x0600770F RID: 30479 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnOnClickFilterButtonCallback(Action callback)
			{
			}

			// Token: 0x06007710 RID: 30480 RVA: 0x0000216D File Offset: 0x0000036D
			public void ToggleFilterButton(bool isFiltered)
			{
			}

			// Token: 0x06007711 RID: 30481 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeInputFieldElements()
			{
			}

			// Token: 0x06007712 RID: 30482 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnEndSubmitCallback(Action<string> callback)
			{
			}

			// Token: 0x06007713 RID: 30483 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearKeyword()
			{
			}

			// Token: 0x06007714 RID: 30484 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeClearButtonElements()
			{
			}

			// Token: 0x06007715 RID: 30485 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnOnClickClearButtonCallback(Action callback)
			{
			}

			// Token: 0x06007716 RID: 30486 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeNotOwnButtonElements()
			{
			}

			// Token: 0x06007717 RID: 30487 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnOnClickNotOwnButtonCallback(Action callback)
			{
			}

			// Token: 0x06007718 RID: 30488 RVA: 0x0000216D File Offset: 0x0000036D
			public void ToggleNotOwnButton(bool isDispNotOwned)
			{
			}

			// Token: 0x06007719 RID: 30489 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetInteractableButtons(bool isInteractable)
			{
			}

			// Token: 0x0600771A RID: 30490 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetShortcutIcons(bool isActivate)
			{
			}

			// Token: 0x0400AFAB RID: 44971
			private const string k_ELabelInputField = "InputField";

			// Token: 0x0400AFAC RID: 44972
			private const string k_ELabelNotOwnedButton = "NotOwnedButton";

			// Token: 0x0400AFAD RID: 44973
			private const string k_ELabelSortButton = "SortButton";

			// Token: 0x0400AFAE RID: 44974
			private const string k_ELabelFilterButton = "FilterButton";

			// Token: 0x0400AFAF RID: 44975
			private const string k_ELabelClearButton = "ClearButton";

			// Token: 0x0400AFB0 RID: 44976
			private ElementObjectManager m_SortButtonEom;

			// Token: 0x0400AFB1 RID: 44977
			private const string k_ELabelSortIconAsc = "IconAsc";

			// Token: 0x0400AFB2 RID: 44978
			private const string k_ELabelSortIconDesc = "IconDesc";

			// Token: 0x0400AFB3 RID: 44979
			private const string k_ELabelSortText = "TextTMP";

			// Token: 0x0400AFB4 RID: 44980
			private RectTransform m_SortIconAsc;

			// Token: 0x0400AFB5 RID: 44981
			private RectTransform m_SortIconDesc;

			// Token: 0x0400AFB6 RID: 44982
			private ExtendedTextMeshProUGUI m_SortText;

			// Token: 0x0400AFB7 RID: 44983
			private Action OnClickSortButtonCallback;

			// Token: 0x0400AFB8 RID: 44984
			private ElementObjectManager m_FilterButtonEom;

			// Token: 0x0400AFB9 RID: 44985
			private const string k_ELabelFilterOnIcon = "IconOn";

			// Token: 0x0400AFBA RID: 44986
			private const string k_ELabelFilterOffIcon = "IconOff";

			// Token: 0x0400AFBB RID: 44987
			private const string k_ELabelFilterOnImage = "On";

			// Token: 0x0400AFBC RID: 44988
			private const string k_ELabelFilterOffImage = "Off";

			// Token: 0x0400AFBD RID: 44989
			private RectTransform m_FilterButtonImageOn;

			// Token: 0x0400AFBE RID: 44990
			private RectTransform m_FilterButtonImageOff;

			// Token: 0x0400AFBF RID: 44991
			private RectTransform m_FilterButtonIconOn;

			// Token: 0x0400AFC0 RID: 44992
			private RectTransform m_FilterButtonIconOff;

			// Token: 0x0400AFC1 RID: 44993
			private Action OnClickFilterButtonCallback;

			// Token: 0x0400AFC2 RID: 44994
			private Action<string> OnEndSubmitEditCallback;

			// Token: 0x0400AFC3 RID: 44995
			private ElementObjectManager m_ClearButtonEom;

			// Token: 0x0400AFC4 RID: 44996
			private Action OnClickClearButtonCallback;

			// Token: 0x0400AFC5 RID: 44997
			private ElementObjectManager m_NotOwnButtonEom;

			// Token: 0x0400AFC6 RID: 44998
			private const string k_ELabelNotOwnOnIcon = "IconOn";

			// Token: 0x0400AFC7 RID: 44999
			private const string k_ELabelNotOwnOffIcon = "IconOff";

			// Token: 0x0400AFC8 RID: 45000
			private const string k_ELabelNotOwnOnImage = "On";

			// Token: 0x0400AFC9 RID: 45001
			private const string k_ELabelNotOwnOffImage = "Off";

			// Token: 0x0400AFCA RID: 45002
			private RectTransform m_NotOwnButtonImageOn;

			// Token: 0x0400AFCB RID: 45003
			private RectTransform m_NotOwnButtonImageOff;

			// Token: 0x0400AFCC RID: 45004
			private RectTransform m_NotOwnButtonIconOn;

			// Token: 0x0400AFCD RID: 45005
			private RectTransform m_NotOwnButtonIconOff;

			// Token: 0x0400AFCE RID: 45006
			private Action OnClickNotOwnButtonCallback;
		}

		// Token: 0x02000FAD RID: 4013
		public class RelatedArea : CardCollectionView.PartialView
		{
			// Token: 0x17000EA5 RID: 3749
			// (get) Token: 0x0600771B RID: 30491 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600771C RID: 30492 RVA: 0x0000216D File Offset: 0x0000036D
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

			// Token: 0x17000EA6 RID: 3750
			// (get) Token: 0x0600771D RID: 30493 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600771E RID: 30494 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_CloseButton
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

			// Token: 0x17000EA7 RID: 3751
			// (get) Token: 0x0600771F RID: 30495 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool isActive
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000EA8 RID: 3752
			// (get) Token: 0x06007720 RID: 30496 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isSet
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06007721 RID: 30497 RVA: 0x000F660A File Offset: 0x000F480A
			public RelatedArea(ElementObjectManager eom)
			{
			}

			// Token: 0x06007722 RID: 30498 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x06007723 RID: 30499 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeRelatedCardElements()
			{
			}

			// Token: 0x06007724 RID: 30500 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetRelatedCardId(int cardId)
			{
			}

			// Token: 0x06007725 RID: 30501 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnSetRelatedCardCallback(Action callback)
			{
			}

			// Token: 0x06007726 RID: 30502 RVA: 0x0000216D File Offset: 0x0000036D
			public void DispRelatedCard(bool b)
			{
			}

			// Token: 0x06007727 RID: 30503 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnDispRelatedCardCallback(Action callback)
			{
			}

			// Token: 0x06007728 RID: 30504 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnOnClickRelatedCardButtonCallback(Action<int> callback)
			{
			}

			// Token: 0x06007729 RID: 30505 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnSelectedRelatedCardButtonCallback(Action<int> callback)
			{
			}

			// Token: 0x0600772A RID: 30506 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnSelectdRelatedCardButtonKetyDownCallback(SelectorManager.KeyType main, SelectorManager.KeyType sub, Action<int> callback)
			{
			}

			// Token: 0x0600772B RID: 30507 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitializeCloseButtonElement()
			{
			}

			// Token: 0x0600772C RID: 30508 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnClickCloseButtonCallback(Action callback)
			{
			}

			// Token: 0x0600772D RID: 30509 RVA: 0x0000216D File Offset: 0x0000036D
			public void SetOnSelectCloseButtonCallback(Action callback)
			{
			}

			// Token: 0x0400AFCF RID: 45007
			private const string k_ELabelRelatedCardRoot = "RelatedCard";

			// Token: 0x0400AFD0 RID: 45008
			private const string k_ELabelRelatedCardText = "RelatedCardText";

			// Token: 0x0400AFD1 RID: 45009
			private const string k_ELabelRelatedCardButton = "RelatedCardButton";

			// Token: 0x0400AFD2 RID: 45010
			private ElementObjectManager m_RelatedCardEom;

			// Token: 0x0400AFD3 RID: 45011
			private Transform m_CardRoot;

			// Token: 0x0400AFD4 RID: 45012
			private RawImage m_CardImage;

			// Token: 0x0400AFD5 RID: 45013
			private ExtendedTextMeshProUGUI m_CardText;

			// Token: 0x0400AFD6 RID: 45014
			private int relatedCardID;

			// Token: 0x0400AFD7 RID: 45015
			private Action OnSetRelatedCardCallback;

			// Token: 0x0400AFD8 RID: 45016
			private Action OnDispRelatedCardCallback;

			// Token: 0x0400AFD9 RID: 45017
			private Action<int> OnClickRelatedCardButtonCallback;

			// Token: 0x0400AFDA RID: 45018
			private Action<int> OnSelectdRelatedCardButtonCallback;

			// Token: 0x0400AFDB RID: 45019
			private Action<int> OnSelectdRelatedCardButtonKetyDownCallback;

			// Token: 0x0400AFDC RID: 45020
			private const string k_ELabelCloseButton = "CloseButton";

			// Token: 0x0400AFDD RID: 45021
			private Action OnClickCloseButtonCallback;

			// Token: 0x0400AFDE RID: 45022
			private Action OnSelectdCloseButtonCallback;
		}

		// Token: 0x02000FAE RID: 4014
		public class CardDropArea : CardCollectionView.PartialView
		{
			// Token: 0x17000EA9 RID: 3753
			// (get) Token: 0x0600772E RID: 30510 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600772F RID: 30511 RVA: 0x0000216D File Offset: 0x0000036D
			public DropArea m_DropArea
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

			// Token: 0x17000EAA RID: 3754
			// (get) Token: 0x06007730 RID: 30512 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007731 RID: 30513 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_RemoveDeckImage
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

			// Token: 0x17000EAB RID: 3755
			// (get) Token: 0x06007732 RID: 30514 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007733 RID: 30515 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_AddBookmarkImage
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

			// Token: 0x17000EAC RID: 3756
			// (get) Token: 0x06007734 RID: 30516 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007735 RID: 30517 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_DropAreaOver
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

			// Token: 0x06007736 RID: 30518 RVA: 0x000F660A File Offset: 0x000F480A
			public CardDropArea(ElementObjectManager eom)
			{
			}

			// Token: 0x06007737 RID: 30519 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x0400AFDF RID: 45023
			private const string k_ELabelAddBookmarkImage = "AddBookmarkImage";

			// Token: 0x0400AFE0 RID: 45024
			private const string k_ELabelRemoveDeckImage = "RemoveDeckImage";

			// Token: 0x0400AFE1 RID: 45025
			private const string k_ELabelDropAreaOver = "DropAreaOver";
		}

		// Token: 0x02000FAF RID: 4015
		public class CardListArea : CardCollectionView.PartialView
		{
			// Token: 0x17000EAD RID: 3757
			// (get) Token: 0x06007738 RID: 30520 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007739 RID: 30521 RVA: 0x0000216D File Offset: 0x0000036D
			public CardCollectionView cardCollectionView
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

			// Token: 0x17000EAE RID: 3758
			// (get) Token: 0x0600773A RID: 30522 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600773B RID: 30523 RVA: 0x0000216D File Offset: 0x0000036D
			public InfinityScrollView m_InfinityScroll
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

			// Token: 0x17000EAF RID: 3759
			// (get) Token: 0x0600773C RID: 30524 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600773D RID: 30525 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_LoadingIcon
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

			// Token: 0x17000EB0 RID: 3760
			// (get) Token: 0x0600773E RID: 30526 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600773F RID: 30527 RVA: 0x0000216D File Offset: 0x0000036D
			public SelectionButton m_NoItemButton
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

			// Token: 0x17000EB1 RID: 3761
			// (get) Token: 0x06007740 RID: 30528 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007741 RID: 30529 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedTextMeshProUGUI m_NoItemText
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

			// Token: 0x17000EB2 RID: 3762
			// (get) Token: 0x06007742 RID: 30530 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007743 RID: 30531 RVA: 0x0000216D File Offset: 0x0000036D
			public ExtendedScrollRect m_ExtendedScrollRect
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

			// Token: 0x17000EB3 RID: 3763
			// (get) Token: 0x06007744 RID: 30532 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007745 RID: 30533 RVA: 0x0000216D File Offset: 0x0000036D
			public RectTransform m_CollectionAreaCenter
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

			// Token: 0x17000EB4 RID: 3764
			// (get) Token: 0x06007746 RID: 30534 RVA: 0x000029CC File Offset: 0x00000BCC
			private int constraintCount
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000EB5 RID: 3765
			// (get) Token: 0x06007747 RID: 30535 RVA: 0x000029CC File Offset: 0x00000BCC
			private int constraintCount2
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000EB6 RID: 3766
			// (get) Token: 0x06007748 RID: 30536 RVA: 0x0000216A File Offset: 0x0000036A
			private List<CardBaseData> m_CardList
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000EB7 RID: 3767
			// (get) Token: 0x06007749 RID: 30537 RVA: 0x000029CC File Offset: 0x00000BCC
			private int regulationId
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000EB8 RID: 3768
			// (get) Token: 0x0600774A RID: 30538 RVA: 0x000029CC File Offset: 0x00000BCC
			private DeckEditViewController2.DisplayMode displayMode
			{
				get
				{
					return DeckEditViewController2.DisplayMode.Simple;
				}
			}

			// Token: 0x17000EB9 RID: 3769
			// (get) Token: 0x0600774B RID: 30539 RVA: 0x000029CC File Offset: 0x00000BCC
			private CardCollectionView.Area currentArea
			{
				get
				{
					return CardCollectionView.Area.Collection;
				}
			}

			// Token: 0x17000EBA RID: 3770
			// (get) Token: 0x0600774C RID: 30540 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600774D RID: 30541 RVA: 0x0000216D File Offset: 0x0000036D
			public Action<CardStrip, bool> onCreateCardCallback
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

			// Token: 0x17000EBB RID: 3771
			// (get) Token: 0x0600774E RID: 30542 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x0600774F RID: 30543 RVA: 0x0000216D File Offset: 0x0000036D
			public Action<SelectionItem> onSelectedCardCallback
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

			// Token: 0x17000EBC RID: 3772
			// (get) Token: 0x06007750 RID: 30544 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007751 RID: 30545 RVA: 0x0000216D File Offset: 0x0000036D
			public Action<bool, CardBaseData> onUpdateViewCallback
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

			// Token: 0x17000EBD RID: 3773
			// (get) Token: 0x06007752 RID: 30546 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007753 RID: 30547 RVA: 0x0000216D File Offset: 0x0000036D
			public Action<SelectionItem> onInputLeftEdge
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

			// Token: 0x17000EBE RID: 3774
			// (get) Token: 0x06007754 RID: 30548 RVA: 0x0000216A File Offset: 0x0000036A
			// (set) Token: 0x06007755 RID: 30549 RVA: 0x0000216D File Offset: 0x0000036D
			public Action onInputUpEdge
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

			// Token: 0x06007756 RID: 30550 RVA: 0x000F660A File Offset: 0x000F480A
			public CardListArea(ElementObjectManager eom)
			{
			}

			// Token: 0x06007757 RID: 30551 RVA: 0x0000216D File Offset: 0x0000036D
			protected override void InitializeElements()
			{
			}

			// Token: 0x06007758 RID: 30552 RVA: 0x0000216D File Offset: 0x0000036D
			public void InitializeTemplate()
			{
			}

			// Token: 0x06007759 RID: 30553 RVA: 0x0000216D File Offset: 0x0000036D
			public void InitializeScroll()
			{
			}

			// Token: 0x0600775A RID: 30554 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnCreateEntity(GameObject obj)
			{
			}

			// Token: 0x0600775B RID: 30555 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnUpdateEntity(GameObject obj, int idx)
			{
			}

			// Token: 0x0600775C RID: 30556 RVA: 0x0000216D File Offset: 0x0000036D
			private void OnRemoveEntity(GameObject obj, int idx, bool isTop)
			{
			}

			// Token: 0x0600775D RID: 30557 RVA: 0x0000216D File Offset: 0x0000036D
			public void UpdateView(bool updateDataCount, bool select = true)
			{
			}

			// Token: 0x0600775E RID: 30558 RVA: 0x000029CC File Offset: 0x00000BCC
			public int GetIndex(int cardID, int premiumID = -1)
			{
				return 0;
			}

			// Token: 0x0600775F RID: 30559 RVA: 0x0000216D File Offset: 0x0000036D
			public void CursorJumpUp()
			{
			}

			// Token: 0x06007760 RID: 30560 RVA: 0x0000216D File Offset: 0x0000036D
			public void CursorJumpDown()
			{
			}

			// Token: 0x06007761 RID: 30561 RVA: 0x0000216D File Offset: 0x0000036D
			public void CursorJumpRight()
			{
			}

			// Token: 0x06007762 RID: 30562 RVA: 0x0000216D File Offset: 0x0000036D
			public void CursorJumpLeft()
			{
			}

			// Token: 0x06007763 RID: 30563 RVA: 0x0000216D File Offset: 0x0000036D
			public void FocusItemAt(int index, bool immediate, bool select)
			{
			}

			// Token: 0x06007764 RID: 30564 RVA: 0x000029CC File Offset: 0x00000BCC
			private bool OnCustomEdgeTransition(SelectionItem selectionItem, PadInputDirection direction)
			{
				return false;
			}

			// Token: 0x06007765 RID: 30565 RVA: 0x0000216D File Offset: 0x0000036D
			public void SelectLeftEdgeClosestItem(Vector2 screenPoint, float angleDot, bool isIni = false)
			{
			}

			// Token: 0x06007766 RID: 30566 RVA: 0x0000216D File Offset: 0x0000036D
			public void StopScroll()
			{
			}

			// Token: 0x06007767 RID: 30567 RVA: 0x0000216D File Offset: 0x0000036D
			private void SetDispNoItem(bool disp)
			{
			}

			// Token: 0x06007768 RID: 30568 RVA: 0x0000216D File Offset: 0x0000036D
			public void DispLoadingIcon(bool disp)
			{
			}

			// Token: 0x06007769 RID: 30569 RVA: 0x0000216D File Offset: 0x0000036D
			private void InitilizeNoItem()
			{
			}

			// Token: 0x0400AFE2 RID: 45026
			private const int numPreLoad = 12;

			// Token: 0x0400AFE3 RID: 45027
			private const string k_ELabelInfinityScroll = "CardList";

			// Token: 0x0400AFE4 RID: 45028
			private const string k_ELabelLoadingIcon = "Loading";

			// Token: 0x0400AFE5 RID: 45029
			private const string k_ELabelNoItemButton = "NoItemButton";

			// Token: 0x0400AFE6 RID: 45030
			private const string k_ELabelNoItemText = "NoItemText";

			// Token: 0x0400AFE7 RID: 45031
			private const string k_ELabelCollection = "CollectionAreaCenter";

			// Token: 0x0400AFE8 RID: 45032
			private GridLayoutGroup m_GridLayout;

			// Token: 0x0400AFE9 RID: 45033
			private CanvasGroup m_CardListCanavsGroup;

			// Token: 0x0400AFEA RID: 45034
			private EntityPoolController entityPoolController;
		}
	}
}
