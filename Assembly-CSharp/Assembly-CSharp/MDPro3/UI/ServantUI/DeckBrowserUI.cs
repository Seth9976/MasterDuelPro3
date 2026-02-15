using System;
using System.Collections.Generic;
using DG.Tweening;
using MDPro3.Servant;
using MDPro3.UI.PropertyOverride;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001458 RID: 5208
	public class DeckBrowserUI : ServantUI
	{
		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x0600971B RID: 38683 RVA: 0x0016020C File Offset: 0x0015E40C
		private CanvasGroup DialogBG
		{
			get
			{
				return this.m_DialogBG = ((this.m_DialogBG != null) ? this.m_DialogBG : base.Manager.GetElement<CanvasGroup>("DialogBG"));
			}
		}

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x0600971C RID: 38684 RVA: 0x00160248 File Offset: 0x0015E448
		public DeckView DeckView
		{
			get
			{
				return this.m_DeckView = ((this.m_DeckView != null) ? this.m_DeckView : base.Manager.GetElement<DeckView>("DeckView"));
			}
		}

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x0600971D RID: 38685 RVA: 0x00160284 File Offset: 0x0015E484
		public CardDetailView CardDetailView
		{
			get
			{
				return this.m_CardDetailView = ((this.m_CardDetailView != null) ? this.m_CardDetailView : base.Manager.GetElement<CardDetailView>("CardDetailView"));
			}
		}

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x0600971E RID: 38686 RVA: 0x001602C0 File Offset: 0x0015E4C0
		private RectTransform OptionalAreaLocator
		{
			get
			{
				return this.m_OptionalAreaLocator = ((this.m_OptionalAreaLocator != null) ? this.m_OptionalAreaLocator : base.Manager.GetElement<RectTransform>("OptionalAreaLocator"));
			}
		}

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x0600971F RID: 38687 RVA: 0x001602FC File Offset: 0x0015E4FC
		private RectTransform OptionalAreaLocator2
		{
			get
			{
				return this.m_OptionalAreaLocator2 = ((this.m_OptionalAreaLocator2 != null) ? this.m_OptionalAreaLocator2 : base.Manager.GetElement<RectTransform>("OptionalAreaLocator2"));
			}
		}

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x06009720 RID: 38688 RVA: 0x00160338 File Offset: 0x0015E538
		// (set) Token: 0x06009721 RID: 38689 RVA: 0x00160340 File Offset: 0x0015E540
		public DeckBrowserUI.ResponseRegion _ResponseRegion
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

		// Token: 0x06009722 RID: 38690 RVA: 0x0016034F File Offset: 0x0015E54F
		private void Awake()
		{
			this.DeckView.PrintDeck(DeckEditor.Deck, DeckEditor.DeckName, DeckView.Condition.Pickup);
			this.DeckView.SetNoItemButtonNavigationEvent(MoveDirection.Right, delegate
			{
				UserInput.NextSelectionIsAxis = true;
				this.PickupCardSelection.Select();
			});
		}

		// Token: 0x06009723 RID: 38691 RVA: 0x00160380 File Offset: 0x0015E580
		public override void ShowEvent()
		{
			base.ShowEvent();
			this.DialogBG.alpha = 0f;
			this.DialogBG.DOFade(1f, 0.3f);
			UIManager.SetCanvasMatch(Program.instance.deckEditor.GetCanvasMatch(), 0.45f);
		}

		// Token: 0x06009724 RID: 38692 RVA: 0x001603D2 File Offset: 0x0015E5D2
		protected override void HideEvent()
		{
			base.HideEvent();
			this.DialogBG.DOFade(0f, 0.3f);
			UIManager.SetCanvasMatch(1f, 0.4f);
		}

		// Token: 0x06009725 RID: 38693 RVA: 0x001603FF File Offset: 0x0015E5FF
		protected override void AfterHideEvent()
		{
			base.AfterHideEvent();
			this.Dispose();
		}

		// Token: 0x06009726 RID: 38694 RVA: 0x0016040D File Offset: 0x0015E60D
		public void ShowDetail(List<int> cards, int index)
		{
			if (this.CardDetailView != null)
			{
				this.CardDetailView.ShowCard(cards, index);
			}
		}

		// Token: 0x06009727 RID: 38695 RVA: 0x0016042A File Offset: 0x0015E62A
		private void ShiftToResponseRegion()
		{
			this.DeckView.SetCursor(this._ResponseRegion == DeckBrowserUI.ResponseRegion.Deck);
		}

		// Token: 0x06009728 RID: 38696 RVA: 0x00160440 File Offset: 0x0015E640
		public void SetCardInfoType()
		{
			DeckEditorUI.CardInfoType type = (DeckBrowserUI.cardInfoType + 1) % (DeckEditorUI.CardInfoType)4;
			this.SetCardInfoType(type);
			SelectionButton_CardInfoType.instance.SetCardInfoTypeIcon(type);
		}

		// Token: 0x06009729 RID: 38697 RVA: 0x0016046C File Offset: 0x0015E66C
		public void SetCardInfoType(DeckEditorUI.CardInfoType type)
		{
			AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
			DeckBrowserUI.cardInfoType = type;
			switch (DeckBrowserUI.cardInfoType)
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
			}
			this.DeckView.SetCardInfoType(type);
		}

		// Token: 0x0600972A RID: 38698 RVA: 0x001604E8 File Offset: 0x0015E6E8
		public void SetCondition(DeckBrowser.Condition condition)
		{
			if (condition == DeckBrowser.Condition.ChangePickup)
			{
				base.Title.text = InterString.Get("变更三大代表卡", 0);
				this.LoadOptionalArea("UIWidges/DeckBrowserOptionForPickupCardSelection.prefab", 1);
			}
		}

		// Token: 0x0600972B RID: 38699 RVA: 0x00160510 File Offset: 0x0015E710
		private void LoadOptionalArea(string address, int areaIndex)
		{
			if (PropertyOverrider.NeedMobileLayout())
			{
				areaIndex = 1;
			}
			Addressables.InstantiateAsync(address, null, false, true).Completed += delegate(AsyncOperationHandle<GameObject> result)
			{
				result.Result.transform.SetParent((areaIndex == 1) ? this.OptionalAreaLocator : this.OptionalAreaLocator2, false);
				this.PickupCardSelection = result.Result.GetComponent<PickupCardSelection>();
			};
		}

		// Token: 0x0600972C RID: 38700 RVA: 0x0014AE0F File Offset: 0x0014900F
		private void Dispose()
		{
			global::UnityEngine.Object.Destroy(base.gameObject);
		}

		// Token: 0x0400D581 RID: 54657
		private const string LABEL_CG_BG = "DialogBG";

		// Token: 0x0400D582 RID: 54658
		private CanvasGroup m_DialogBG;

		// Token: 0x0400D583 RID: 54659
		private const string LABEL_MONO_DECKVIEW = "DeckView";

		// Token: 0x0400D584 RID: 54660
		private DeckView m_DeckView;

		// Token: 0x0400D585 RID: 54661
		private const string LABEL_MONO_CARDDETAILVIEW = "CardDetailView";

		// Token: 0x0400D586 RID: 54662
		private CardDetailView m_CardDetailView;

		// Token: 0x0400D587 RID: 54663
		private const string LABEL_RT_OPTIONAL_AREA_LOCATOR = "OptionalAreaLocator";

		// Token: 0x0400D588 RID: 54664
		private RectTransform m_OptionalAreaLocator;

		// Token: 0x0400D589 RID: 54665
		private const string LABEL_RT_OPTIONAL_AREA_LOCATOR_2 = "OptionalAreaLocator2";

		// Token: 0x0400D58A RID: 54666
		private RectTransform m_OptionalAreaLocator2;

		// Token: 0x0400D58B RID: 54667
		[HideInInspector]
		public PickupCardSelection PickupCardSelection;

		// Token: 0x0400D58C RID: 54668
		private const string LABEL_WIDGET_PICKUP = "UIWidges/DeckBrowserOptionForPickupCardSelection.prefab";

		// Token: 0x0400D58D RID: 54669
		public static DeckEditorUI.CardInfoType cardInfoType;

		// Token: 0x0400D58E RID: 54670
		private DeckBrowserUI.ResponseRegion m_ResponseRegion;

		// Token: 0x02001459 RID: 5209
		public enum ResponseRegion
		{
			// Token: 0x0400D590 RID: 54672
			Deck,
			// Token: 0x0400D591 RID: 54673
			Option
		}
	}
}
