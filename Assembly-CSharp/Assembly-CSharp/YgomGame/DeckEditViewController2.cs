using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomGame.Card;
using YgomGame.Deck;
using YgomGame.Duel;
using YgomGame.Menu;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.Utility;
using YgomSystem.YGomTMPro;

namespace YgomGame
{
	// Token: 0x020007B7 RID: 1975
	public class DeckEditViewController2 : BaseMenuViewController
	{
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06003D4E RID: 15694 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003D4F RID: 15695 RVA: 0x0000216D File Offset: 0x0000036D
		private DeckEditViewController2.DisplayMode m_DisplayMode
		{
			[CompilerGenerated]
			get
			{
				return DeckEditViewController2.DisplayMode.Simple;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06003D50 RID: 15696 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003D51 RID: 15697 RVA: 0x0000216D File Offset: 0x0000036D
		private DeckEditViewController2.EditMode m_EditMode
		{
			[CompilerGenerated]
			get
			{
				return DeckEditViewController2.EditMode.Default;
			}
			[CompilerGenerated]
			set
			{
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06003D52 RID: 15698 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003D53 RID: 15699 RVA: 0x0000216D File Offset: 0x0000036D
		private int m_OldRegulationID
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

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x06003D54 RID: 15700 RVA: 0x0000216A File Offset: 0x0000036A
		private DragCard dragCard
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06003D55 RID: 15701 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003D56 RID: 15702 RVA: 0x0000216D File Offset: 0x0000036D
		private List<CardBaseData> m_CardCollection
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

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06003D57 RID: 15703 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003D58 RID: 15704 RVA: 0x0000216D File Offset: 0x0000036D
		private List<CardBaseData> m_CardListBuff
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

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06003D59 RID: 15705 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003D5A RID: 15706 RVA: 0x0000216D File Offset: 0x0000036D
		private List<CardBaseData> m_RelatedCardList
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

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06003D5B RID: 15707 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003D5C RID: 15708 RVA: 0x0000216D File Offset: 0x0000036D
		public int m_DeckID
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

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06003D5D RID: 15709 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003D5E RID: 15710 RVA: 0x0000216D File Offset: 0x0000036D
		public string m_DeckName
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

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06003D5F RID: 15711 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003D60 RID: 15712 RVA: 0x0000216D File Offset: 0x0000036D
		public string m_EventDeckName
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

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06003D61 RID: 15713 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06003D62 RID: 15714 RVA: 0x0000216D File Offset: 0x0000036D
		public string m_OldDeckName
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

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06003D63 RID: 15715 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003D64 RID: 15716 RVA: 0x0000216D File Offset: 0x0000036D
		private bool showAllCards
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06003D65 RID: 15717 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003D66 RID: 15718 RVA: 0x0000216D File Offset: 0x0000036D
		public bool m_IsCopyDeckEdit
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

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06003D67 RID: 15719 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isMobile
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeElements()
		{
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsImplemented(int cardId)
		{
			return false;
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitializeView()
		{
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetShortcutSettings()
		{
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitDeckList()
		{
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x0000216D File Offset: 0x0000036D
		private void InitRentalCards()
		{
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDisable()
		{
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnFocusChanged(bool setfocus)
		{
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x0000216D File Offset: 0x0000036D
		private void MainViewActivated()
		{
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x0000216D File Offset: 0x0000036D
		private void MainViewDeactivated()
		{
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x0000216D File Offset: 0x0000036D
		private void SortDeckViewCards()
		{
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitializeCardCollectionView()
		{
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x06003D78 RID: 15736 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnSubmitDeckName(string deckName)
		{
		}

		// Token: 0x06003D79 RID: 15737 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnResetSearchButton()
		{
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnSubmitSearch(string text)
		{
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickFilterButton()
		{
		}

		// Token: 0x06003D7C RID: 15740 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToggleShowAllCards()
		{
		}

		// Token: 0x06003D7D RID: 15741 RVA: 0x0000216A File Offset: 0x0000036A
		private List<CardBaseData> getTargetCardCollection()
		{
			return null;
		}

		// Token: 0x06003D7E RID: 15742 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickRelatedCardButton(int cardID)
		{
		}

		// Token: 0x06003D7F RID: 15743 RVA: 0x0000216A File Offset: 0x0000036A
		private List<CardBaseData> getRelatedCardList(int cardID, bool fullStyle)
		{
			return null;
		}

		// Token: 0x06003D80 RID: 15744 RVA: 0x0000216D File Offset: 0x0000036D
		private void CloseRelatedCard()
		{
		}

		// Token: 0x06003D81 RID: 15745 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool AddToBookmark(CardBaseData cbd)
		{
			return false;
		}

		// Token: 0x06003D82 RID: 15746 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool RemoveFromBookmark(CardBaseData cbd)
		{
			return false;
		}

		// Token: 0x06003D83 RID: 15747 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddToCardHistory(int id, int premiumID)
		{
		}

		// Token: 0x06003D84 RID: 15748 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddToCardHistory(CardBaseData cbd)
		{
		}

		// Token: 0x06003D85 RID: 15749 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickSortButton()
		{
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsBookmarked(CardBaseData cbd)
		{
			return false;
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool IsHistoried(CardBaseData cbd)
		{
			return false;
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x000029CC File Offset: 0x00000BCC
		private int GetDataIndex(CardBaseData cbd, List<CardBaseData> target)
		{
			return 0;
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenCPOverDialog()
		{
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickMassDismantleButton()
		{
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<int, int> GetLackCards()
		{
			return null;
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x0000216A File Offset: 0x0000036A
		private List<CardBaseData> FormatLackCards(Dictionary<int, int> lackCards)
		{
			return null;
		}

		// Token: 0x06003D8D RID: 15757 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenMultiCreateCraftDialog()
		{
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickMultiDismantleButton()
		{
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenCardActionMenu(CardBaseData baseData, bool fromDeckList = true, int idx = -1)
		{
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x0000216D File Offset: 0x0000036D
		private void CraftCreate(CardBaseData baseData, bool actionMenu)
		{
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x0000216D File Offset: 0x0000036D
		private void CraftDismantle(CardBaseData baseData, bool actionMenu)
		{
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x0000216D File Offset: 0x0000036D
		public void CloseActionDialog()
		{
		}

		// Token: 0x06003D93 RID: 15763 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool updateInDeckCards(DeckInfo.DeckType type)
		{
			return false;
		}

		// Token: 0x06003D94 RID: 15764 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool NeedSave()
		{
			return false;
		}

		// Token: 0x06003D95 RID: 15765 RVA: 0x0000216D File Offset: 0x0000036D
		private void initializeDetailView(CardBaseData baseData)
		{
		}

		// Token: 0x06003D96 RID: 15766 RVA: 0x0000216D File Offset: 0x0000036D
		private void initializeCard(DeckCard card, bool setData, int idx = -1)
		{
		}

		// Token: 0x06003D97 RID: 15767 RVA: 0x0000216D File Offset: 0x0000036D
		private void initializeCard(CardStrip card, bool historyView)
		{
		}

		// Token: 0x06003D98 RID: 15768 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnCreateCardInDeck(CardBaseData baseData, int craftNum, bool invokeFromActionMenu)
		{
		}

		// Token: 0x06003D99 RID: 15769 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDismantleCardInDeck(CardBaseData baseData, int craftNum, bool invokeFromActionMenu, int compensationId)
		{
		}

		// Token: 0x06003D9A RID: 15770 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCard AddToMainOrExtraDeck(CardBase card, Vector3? basePos = null, Vector3? targetPos = null)
		{
			return null;
		}

		// Token: 0x06003D9B RID: 15771 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCard AddToMainOrExtraDeck(CardBaseData baseData)
		{
			return null;
		}

		// Token: 0x06003D9C RID: 15772 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCard AddForDismantle(CardBase card, Vector3? basePos = null, Vector3? targetPos = null)
		{
			return null;
		}

		// Token: 0x06003D9D RID: 15773 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCard AddForDismantle(CardBaseData baseData)
		{
			return null;
		}

		// Token: 0x06003D9E RID: 15774 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ShowAddableMessage(DeckView.AddableType type)
		{
			return false;
		}

		// Token: 0x06003D9F RID: 15775 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveFromDeck(DeckCard card, bool isDrag = false, Vector3? pos = null)
		{
		}

		// Token: 0x06003DA0 RID: 15776 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCard RemoveFromDeck(CardBaseData baseData)
		{
			return null;
		}

		// Token: 0x06003DA1 RID: 15777 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveFromDismantle(DeckCard card, bool isDrag = false, Vector3? pos = null)
		{
		}

		// Token: 0x06003DA2 RID: 15778 RVA: 0x0000216A File Offset: 0x0000036A
		public DeckCard RemoveFromDismantle(CardBaseData baseData)
		{
			return null;
		}

		// Token: 0x06003DA3 RID: 15779 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartCardTransition(CardBase baseCard, CardBase targetCard, TransitionCard.MotionMode motionMode, bool outFade, TransitionCard.Size size)
		{
		}

		// Token: 0x06003DA4 RID: 15780 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartCardTransition(Vector3 baseCardPosition, CardBase targetCard, TransitionCard.MotionMode motionMode, bool outFade, TransitionCard.Size size)
		{
		}

		// Token: 0x06003DA5 RID: 15781 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartCardTransition(CardBase baseCard, Vector3 targetPosition, TransitionCard.MotionMode motionMode, bool outFade, TransitionCard.Size size)
		{
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartCardTransition(CardBaseData cbd, Vector3 baseCardPosition, Vector3 targetPosition, TransitionCard.MotionMode motionMode, bool outFade, TransitionCard.Size size)
		{
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartCardAddEffect(CardBase targetCard, TransitionCard.Size size)
		{
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool BookmarkCard(CardBaseData baseData)
		{
			return false;
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickBackButton()
		{
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowModifiedDialog(DeckEditViewController2.SaveDialogType type, Action onAccept, Action onCancel)
		{
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickSaveButton()
		{
		}

		// Token: 0x06003DAC RID: 15788 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetSaveAlertMessage()
		{
			return null;
		}

		// Token: 0x06003DAD RID: 15789 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> GetCurrentPickCards()
		{
			return null;
		}

		// Token: 0x06003DAE RID: 15790 RVA: 0x0000216D File Offset: 0x0000036D
		private void SaveDeck(Action finishedCallback = null, bool blockInput = false)
		{
		}

		// Token: 0x06003DAF RID: 15791 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenOutOfTermDialog()
		{
		}

		// Token: 0x06003DB0 RID: 15792 RVA: 0x0000216D File Offset: 0x0000036D
		private void SaveBookmark(Action finishedCallback = null)
		{
		}

		// Token: 0x06003DB1 RID: 15793 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowMenu()
		{
		}

		// Token: 0x06003DB2 RID: 15794 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshRegulation(int regId)
		{
		}

		// Token: 0x06003DB3 RID: 15795 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickRegulation()
		{
		}

		// Token: 0x06003DB4 RID: 15796 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickSecretPack()
		{
		}

		// Token: 0x06003DB5 RID: 15797 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowSecretPackActivateEffect()
		{
		}

		// Token: 0x06003DB6 RID: 15798 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDispSecretPackButton(bool disp)
		{
		}

		// Token: 0x06003DB7 RID: 15799 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetActiveDropAreas(bool b, int cardId = 0)
		{
		}

		// Token: 0x06003DB8 RID: 15800 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetActiveExclusiveDropAreas(bool active, string label = null, int cardId = 0, bool canDrop = true)
		{
		}

		// Token: 0x06003DB9 RID: 15801 RVA: 0x0000216D File Offset: 0x0000036D
		private void onCraftCreateCard(CardBaseData cbd, int craftNum, DeckEditViewController2.craftCallBack callback, bool invokeFromActionMenu)
		{
		}

		// Token: 0x06003DBA RID: 15802 RVA: 0x0000216A File Offset: 0x0000036A
		private string GetCraftCreateResultMessage(CardBaseData cbd, int numNormal, int numShine, int numRoyal)
		{
			return null;
		}

		// Token: 0x06003DBB RID: 15803 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdatePremNum(CardBaseData cbd, int premID)
		{
		}

		// Token: 0x06003DBC RID: 15804 RVA: 0x0000216D File Offset: 0x0000036D
		private void onCraftDismantleCard(CardBaseData cbd, int craftNum, DeckEditViewController2.craftCallBack callback, bool invokeFromActionMenu, int compensationId)
		{
		}

		// Token: 0x06003DBD RID: 15805 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateCardBaseData(List<CardBaseData> list, int cardID, int premiumID)
		{
		}

		// Token: 0x06003DBE RID: 15806 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartCreateEffect(RectTransform targetCard, RectTransform targetPoint, bool invokeFromActionMenu)
		{
		}

		// Token: 0x06003DBF RID: 15807 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartDismantleEffect(RectTransform targetCard, RectTransform targetPoint, bool invokeFromActionMenu)
		{
		}

		// Token: 0x06003DC0 RID: 15808 RVA: 0x0000216A File Offset: 0x0000036A
		private RectTransform GetHeaderCraftPointRectTransform(int rarityID)
		{
			return null;
		}

		// Token: 0x06003DC1 RID: 15809 RVA: 0x0000216D File Offset: 0x0000036D
		private void saveFilterOptions()
		{
		}

		// Token: 0x06003DC2 RID: 15810 RVA: 0x0000216D File Offset: 0x0000036D
		private void loadFilterOptions()
		{
		}

		// Token: 0x06003DC3 RID: 15811 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetDisplayMode(DeckEditViewController2.DisplayMode displayMode, bool updateView)
		{
		}

		// Token: 0x06003DC4 RID: 15812 RVA: 0x0000216D File Offset: 0x0000036D
		private void toggleDisplayMode()
		{
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x000029CC File Offset: 0x00000BCC
		private int getRemainPremiumCard(int id, CardCollectionInfo.Premium prem)
		{
			return 0;
		}

		// Token: 0x06003DC6 RID: 15814 RVA: 0x0000216D File Offset: 0x0000036D
		private void toggleSelectedWindow(DeckEditViewController2.ViewType viewType, DeckEditViewController2.SelectedCardType selectingCard)
		{
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateSelectedWindow()
		{
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetupFooter()
		{
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearFooterDescription()
		{
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x0000216D File Offset: 0x0000036D
		private void ShowFooterDescription(DeckEditViewController2.ViewType viewType, DeckEditViewController2.SelectedCardType selectingCard)
		{
		}

		// Token: 0x06003DCB RID: 15819 RVA: 0x0000216A File Offset: 0x0000036A
		private Task AsyncFilterAndSort(Action onFinish, bool setAll = true, SortComparer.Sorter? targetSorter = null, bool filter = true)
		{
			return null;
		}

		// Token: 0x06003DCC RID: 15820 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateCraftPointNum(int rarityID)
		{
		}

		// Token: 0x06003DCD RID: 15821 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateCraftPointNum()
		{
		}

		// Token: 0x06003DCE RID: 15822 RVA: 0x0000216D File Offset: 0x0000036D
		public void ToggleDismantleMode(bool b)
		{
		}

		// Token: 0x06003DCF RID: 15823 RVA: 0x0000216D File Offset: 0x0000036D
		public void CancelDismantleModeCheck()
		{
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator InitialSetMainDeck()
		{
			return null;
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator InitialSetExtraDeck()
		{
			return null;
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool CheckStyleFilling()
		{
			return false;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CheckStyleFilling(CardBaseData cbd)
		{
			return false;
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetNumInDeck(int cardID, int premiumID)
		{
			return 0;
		}

		// Token: 0x06003DD5 RID: 15829 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator InitialSetDeck(Action onFinish)
		{
			return null;
		}

		// Token: 0x06003DD6 RID: 15830 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenStyleFillingDialog()
		{
		}

		// Token: 0x06003DD7 RID: 15831 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool CheckReglationExistence()
		{
			return false;
		}

		// Token: 0x06003DD8 RID: 15832 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenReglationFillingDialog(Action onFinish = null)
		{
		}

		// Token: 0x06003DD9 RID: 15833 RVA: 0x0000216D File Offset: 0x0000036D
		private void OpenRoomRegulationCheck(Action onFinish = null)
		{
		}

		// Token: 0x06003DDA RID: 15834 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClickLootSourceButton(CardBaseData data)
		{
		}

		// Token: 0x06003DDB RID: 15835 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> GetRepCards()
		{
			return null;
		}

		// Token: 0x06003DDC RID: 15836 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> CheckRepCards()
		{
			return null;
		}

		// Token: 0x06003DDD RID: 15837 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnInputAnalogDirection(SelectorManager.AnalogType analogType, PadInputDirection dir)
		{
		}

		// Token: 0x06003DDE RID: 15838 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckFirstVisitRentalCard()
		{
		}

		// Token: 0x040035C9 RID: 13769
		private int m_ExhibitionID;

		// Token: 0x040035CA RID: 13770
		private int m_CupID;

		// Token: 0x040035CB RID: 13771
		private int m_WcsID;

		// Token: 0x040035CC RID: 13772
		private int m_RankEventID;

		// Token: 0x040035CD RID: 13773
		private int m_DuelTrialID;

		// Token: 0x040035CE RID: 13774
		private int m_VersusID;

		// Token: 0x040035CF RID: 13775
		private int m_VersusGroupID;

		// Token: 0x040035D0 RID: 13776
		private int m_RegulationID;

		// Token: 0x040035D1 RID: 13777
		private int m_RoomRegulationID;

		// Token: 0x040035D2 RID: 13778
		private int m_RentalCardID;

		// Token: 0x040035D3 RID: 13779
		private ElementObjectManager m_UI;

		// Token: 0x040035D4 RID: 13780
		private Action<int> onSavedDeckCallback;

		// Token: 0x040035D5 RID: 13781
		private bool isDismantleMode;

		// Token: 0x040035D6 RID: 13782
		private bool isScratch;

		// Token: 0x040035D7 RID: 13783
		private static bool isRunningFilterAndSort;

		// Token: 0x040035D8 RID: 13784
		private bool firstSort;

		// Token: 0x040035D9 RID: 13785
		private Content m_cci;

		// Token: 0x040035DA RID: 13786
		public const string k_ArgsKeyDefaultDeck = "DeckName";

		// Token: 0x040035DB RID: 13787
		public const string k_ArgsKeyEventDeckName = "EventDeckName";

		// Token: 0x040035DC RID: 13788
		public const string k_ArgsKeyDeckID = "DeckId";

		// Token: 0x040035DD RID: 13789
		public const string k_ArgsKeyExhibitionDeck = "ExhibitionID";

		// Token: 0x040035DE RID: 13790
		public const string k_ArgsKeyCupDeck = "CupID";

		// Token: 0x040035DF RID: 13791
		public const string k_ArgsKeyWcsDeck = "WcsID";

		// Token: 0x040035E0 RID: 13792
		public const string k_ArgsKeyRankEventDeck = "RankEventID";

		// Token: 0x040035E1 RID: 13793
		public const string k_ArgsKeyDuelTrialDeck = "DuelTrialID";

		// Token: 0x040035E2 RID: 13794
		public const string k_ArgsKeyVersusDeck = "VersusID";

		// Token: 0x040035E3 RID: 13795
		public const string k_ArgsKeyVersusGroupDeck = "VersusGroupID";

		// Token: 0x040035E4 RID: 13796
		public const string k_ArgsKeyIsScratch = "Scratch";

		// Token: 0x040035E5 RID: 13797
		public const string k_ArgsKeySecretPack = "SecretPack";

		// Token: 0x040035E6 RID: 13798
		public const string k_ArgsKeyRegulation = "RegulationID";

		// Token: 0x040035E7 RID: 13799
		public const string k_ArgsKeyRoomRegulation = "RoomRegulationID";

		// Token: 0x040035E8 RID: 13800
		public const string PREFAB_PATH_DECKLISTVIEW = "DeckEdit/DeckList";

		// Token: 0x040035E9 RID: 13801
		public const string PREFAB_PATH_CARDACTIONMENU = "DeckEdit/CardActionMenu";

		// Token: 0x040035EA RID: 13802
		public const string PREFAB_PATH_CARDDETAIL = "DeckEdit/CardDetail";

		// Token: 0x040035EB RID: 13803
		public const string PREFAB_PATH_CARDCOLLECTIONVIEW = "DeckEdit/CardCollection";

		// Token: 0x040035EC RID: 13804
		public const string PREFAB_PATH_CARDHISTORYVIEW = "DeckEdit/CardHIstory";

		// Token: 0x040035ED RID: 13805
		public const string PREFAB_PATH_LOOTSOURCE_VC = "DeckEdit/LootSource";

		// Token: 0x040035EE RID: 13806
		private const string LABEL_HEADER = "Header";

		// Token: 0x040035EF RID: 13807
		private const string LABEL_OVERHEADER = "OverHeader";

		// Token: 0x040035F0 RID: 13808
		private const string LABEL_TXT_TOURNAMENTTITLE = "TextRegulation";

		// Token: 0x040035F1 RID: 13809
		private const string LABEL_DECKVIEW = "DeckView";

		// Token: 0x040035F2 RID: 13810
		private const string LABEL_CARDACTIONMENU = "CardActionMenu";

		// Token: 0x040035F3 RID: 13811
		private const string LABEL_DETAILVIEW = "DetailView";

		// Token: 0x040035F4 RID: 13812
		private const string LABEL_COLLECTIONVIEW = "CollectionView";

		// Token: 0x040035F5 RID: 13813
		private const string LABEL_HISTORYVIEW = "HistoryView";

		// Token: 0x040035F6 RID: 13814
		private const string LABEL_TEMPLATEFOOTERDESC = "TemplateFooterDesc";

		// Token: 0x040035F7 RID: 13815
		private const string k_ELabelAnalogDirectionItem = "AnalogDirectionItem";

		// Token: 0x040035F8 RID: 13816
		private const string LABEL_DROPAREA = "DropArea";

		// Token: 0x040035F9 RID: 13817
		private const string LABEL_DRAGCARD = "DragCard";

		// Token: 0x040035FA RID: 13818
		private const string LABEL_RT_SELECTEDWINDOW = "CursorWindowSelect";

		// Token: 0x040035FB RID: 13819
		private const string LABEL_RT_FOOTER = "Footer";

		// Token: 0x040035FC RID: 13820
		public const string SG_DeckList = "DeckListGroup";

		// Token: 0x040035FD RID: 13821
		public const string SG_Collection = "CardCollectionGroup";

		// Token: 0x040035FE RID: 13822
		public const string SG_History = "CardHistoryGroup";

		// Token: 0x040035FF RID: 13823
		public const string SG_CardActionMenu = "CardActionMenu";

		// Token: 0x04003600 RID: 13824
		public const string SG_FilterDialog = "FilterDialog";

		// Token: 0x04003601 RID: 13825
		public const string SG_CraftDialog = "CraftDialog";

		// Token: 0x04003602 RID: 13826
		private const string LABEL_SBN_DISPLAYMODE = "ButtonInfoSwitching";

		// Token: 0x04003603 RID: 13827
		private const string LABEL_RT_DISPLAYMODE0 = "ButtonInfoSwitching/IconInfoSwitching0";

		// Token: 0x04003604 RID: 13828
		private const string LABEL_RT_DISPLAYMODE1 = "ButtonInfoSwitching/IconInfoSwitching1";

		// Token: 0x04003605 RID: 13829
		private const string LABEL_RT_DISPLAYMODE2 = "ButtonInfoSwitching/IconInfoSwitching2";

		// Token: 0x04003606 RID: 13830
		private const string LABEL_SBN_REGUBUTTON = "ButtonRegulation";

		// Token: 0x04003607 RID: 13831
		private const string LABEL_IMG_REGU = "ButtonRegulation/Logo";

		// Token: 0x04003608 RID: 13832
		private const string LABEL_SBN_SAVEBUTTON = "ButtonSave";

		// Token: 0x04003609 RID: 13833
		private const string LABEL_SBN_MENUBUTTON = "ButtonMenu";

		// Token: 0x0400360A RID: 13834
		private const string LABEL_SBN_CANCELBUTTON = "ButtonCancel";

		// Token: 0x0400360B RID: 13835
		private const string LABEL_SBN_BACKBUTTON = "Back";

		// Token: 0x0400360C RID: 13836
		private const string LABEL_TXT_NUMCPN = "NumTextCPN";

		// Token: 0x0400360D RID: 13837
		private const string LABEL_TXT_NUMCPR = "NumTextCPR";

		// Token: 0x0400360E RID: 13838
		private const string LABEL_TXT_NUMCPSR = "NumTextCPSR";

		// Token: 0x0400360F RID: 13839
		private const string LABEL_TXT_NUMCPUR = "NumTextCPUR";

		// Token: 0x04003610 RID: 13840
		private const string LABEL_SBN_SECRETPACK = "ButtonSecretPack";

		// Token: 0x04003611 RID: 13841
		private const string LABEL_SBN_REGLATION = "ButtonReglation";

		// Token: 0x04003612 RID: 13842
		private const string LABEL_TXT_NUMSECRETPACK = "NumBadgeText";

		// Token: 0x04003613 RID: 13843
		private const string LABEL_BADGE_SECRETPACK = "NumBadge";

		// Token: 0x04003614 RID: 13844
		private const string Label_BGM = "BGM_MENU_02";

		// Token: 0x04003615 RID: 13845
		private ElementObjectManager m_HeaderEom;

		// Token: 0x04003616 RID: 13846
		private ElementObjectManager m_OverHeaderEom;

		// Token: 0x04003617 RID: 13847
		private ElementObjectManager m_DeckViewEom;

		// Token: 0x04003618 RID: 13848
		private ElementObjectManager m_CardActionMenuEom;

		// Token: 0x04003619 RID: 13849
		private ElementObjectManager m_DetailViewEom;

		// Token: 0x0400361A RID: 13850
		private ElementObjectManager m_CollectionViewEom;

		// Token: 0x0400361B RID: 13851
		private CraftEffect craftEffect;

		// Token: 0x0400361C RID: 13852
		private bool fromDeckSelect;

		// Token: 0x0400361D RID: 13853
		private Action<int, List<int>> shopTransitionCallback;

		// Token: 0x0400361E RID: 13854
		private SelectionButton m_DisplayModeButton;

		// Token: 0x0400361F RID: 13855
		private RectTransform m_DisplayMode0;

		// Token: 0x04003620 RID: 13856
		private RectTransform m_DisplayMode1;

		// Token: 0x04003621 RID: 13857
		private RectTransform m_DisplayMode2;

		// Token: 0x04003622 RID: 13858
		private SelectionButton m_RegulationButton;

		// Token: 0x04003623 RID: 13859
		private Image m_RegulationImage;

		// Token: 0x04003624 RID: 13860
		private SelectionButton m_SaveButton;

		// Token: 0x04003625 RID: 13861
		private SelectionButton m_MenuButton;

		// Token: 0x04003626 RID: 13862
		private SelectionButton m_CancelButton;

		// Token: 0x04003627 RID: 13863
		private SelectionButton m_BackButton;

		// Token: 0x04003628 RID: 13864
		private SelectionButton m_SecretPackButton;

		// Token: 0x04003629 RID: 13865
		private GameObject m_BadgeSecretPack;

		// Token: 0x0400362A RID: 13866
		private ExtendedTextMeshProUGUI m_NumSecretPackText;

		// Token: 0x0400362B RID: 13867
		private ExtendedTextMeshProUGUI m_NumCPN;

		// Token: 0x0400362C RID: 13868
		private ExtendedTextMeshProUGUI m_NumCPR;

		// Token: 0x0400362D RID: 13869
		private ExtendedTextMeshProUGUI m_NumCPSR;

		// Token: 0x0400362E RID: 13870
		private ExtendedTextMeshProUGUI m_NumCPUR;

		// Token: 0x0400362F RID: 13871
		private ElementObjectManager m_templateFooterDesc;

		// Token: 0x04003630 RID: 13872
		private RectTransform m_Footer;

		// Token: 0x04003631 RID: 13873
		private AnalogDirectionListener m_AnalogManager;

		// Token: 0x04003632 RID: 13874
		private DeckView m_DeckView;

		// Token: 0x04003633 RID: 13875
		private CardActionMenu m_CardActionMenu;

		// Token: 0x04003634 RID: 13876
		private CardCollectionView m_CollectionView;

		// Token: 0x04003635 RID: 13877
		private CardDetailView m_DetailView;

		// Token: 0x04003636 RID: 13878
		private bool isCardActionMenuOpen;

		// Token: 0x04003637 RID: 13879
		private List<CardBaseData> m_MainDeckCards;

		// Token: 0x04003638 RID: 13880
		private List<CardBaseData> m_ExtraDeckCards;

		// Token: 0x04003639 RID: 13881
		private List<CardBaseData> m_BookmarkedCards;

		// Token: 0x0400363A RID: 13882
		private List<CardBaseData> m_HistoryCards;

		// Token: 0x0400363B RID: 13883
		private List<CardBaseData> m_DismanlteCards;

		// Token: 0x0400363C RID: 13884
		[SerializeField]
		private TransitionCard prefabTransitionCard;

		// Token: 0x0400363D RID: 13885
		private DeckEditFooter m_DeckEditFooter;

		// Token: 0x0400363E RID: 13886
		private const string LABEL_DropArea_Collection = "CardCollection";

		// Token: 0x0400363F RID: 13887
		private const string LABEL_DropArea_Deck = "DeckList";

		// Token: 0x04003640 RID: 13888
		private Dictionary<string, UnityAction> dropAreaActions;

		// Token: 0x04003641 RID: 13889
		private Dictionary<string, DropArea> dropAreas;

		// Token: 0x04003642 RID: 13890
		private Camera m_Camera;

		// Token: 0x04003643 RID: 13891
		private SearchFilter.Setting m_FilterSettings;

		// Token: 0x04003644 RID: 13892
		private const string FilterOptionsFileName = "FilterOptions";

		// Token: 0x04003645 RID: 13893
		private string m_SearchKeyword;

		// Token: 0x04003646 RID: 13894
		private SortComparer.Sorter m_Sorter;

		// Token: 0x04003647 RID: 13895
		private float holdTime;

		// Token: 0x04003648 RID: 13896
		private SecretPackEffect secretPackEffect;

		// Token: 0x04003649 RID: 13897
		private int relatedCardID;

		// Token: 0x0400364A RID: 13898
		private DeckEditViewController2.ViewType currentView;

		// Token: 0x0400364B RID: 13899
		private DeckEditViewController2.SelectedCardType footerSelectingCard;

		// Token: 0x0400364C RID: 13900
		private bool option1Activate;

		// Token: 0x0400364D RID: 13901
		private bool option2Activate;

		// Token: 0x0400364E RID: 13902
		private bool option1ActivateChecker;

		// Token: 0x0400364F RID: 13903
		private bool option2ActivateChecker;

		// Token: 0x04003650 RID: 13904
		private bool mainViewActivated;

		// Token: 0x04003651 RID: 13905
		[SerializeField]
		private KeyConfigContainer keyConfig;

		// Token: 0x04003652 RID: 13906
		[SerializeField]
		private BezierMotionContainer bezierCraftCreate;

		// Token: 0x04003653 RID: 13907
		[SerializeField]
		private BezierMotionContainer bezierCraftDismantle;

		// Token: 0x04003654 RID: 13908
		private const int MAX_BOOKMARK_CARDNUM = 100;

		// Token: 0x04003655 RID: 13909
		private const int MAX_HISTORY_CARDNUM = 30;

		// Token: 0x04003656 RID: 13910
		private bool horizontalSwipe;

		// Token: 0x04003657 RID: 13911
		private Vector2 pressedPoint;

		// Token: 0x04003658 RID: 13912
		private Dictionary<int, CardCollectionInfo.SecretPackInfo> secretPacks;

		// Token: 0x04003659 RID: 13913
		private const int MULTIDISMANTLEMAX = 60;

		// Token: 0x0400365A RID: 13914
		private bool requestUpdateView;

		// Token: 0x020007B8 RID: 1976
		public enum DisplayMode
		{
			// Token: 0x0400365C RID: 13916
			Simple,
			// Token: 0x0400365D RID: 13917
			Detailed,
			// Token: 0x0400365E RID: 13918
			Rarity
		}

		// Token: 0x020007B9 RID: 1977
		public enum EditMode
		{
			// Token: 0x04003660 RID: 13920
			Default,
			// Token: 0x04003661 RID: 13921
			Exhibition,
			// Token: 0x04003662 RID: 13922
			Cup,
			// Token: 0x04003663 RID: 13923
			Wcs,
			// Token: 0x04003664 RID: 13924
			RankEvent,
			// Token: 0x04003665 RID: 13925
			DuelTrial,
			// Token: 0x04003666 RID: 13926
			Versus
		}

		// Token: 0x020007BA RID: 1978
		private enum ViewType
		{
			// Token: 0x04003668 RID: 13928
			None,
			// Token: 0x04003669 RID: 13929
			CardCollection,
			// Token: 0x0400366A RID: 13930
			Deck,
			// Token: 0x0400366B RID: 13931
			Unknown
		}

		// Token: 0x020007BB RID: 1979
		private enum SelectedCardType
		{
			// Token: 0x0400366D RID: 13933
			None,
			// Token: 0x0400366E RID: 13934
			CollectionCard,
			// Token: 0x0400366F RID: 13935
			DeckCard,
			// Token: 0x04003670 RID: 13936
			RelatedCard
		}

		// Token: 0x020007BC RID: 1980
		private enum SaveDialogType
		{
			// Token: 0x04003672 RID: 13938
			Back,
			// Token: 0x04003673 RID: 13939
			SecretPack
		}

		// Token: 0x020007BD RID: 1981
		// (Invoke) Token: 0x06003DE1 RID: 15841
		private delegate void craftCallBack();
	}
}
