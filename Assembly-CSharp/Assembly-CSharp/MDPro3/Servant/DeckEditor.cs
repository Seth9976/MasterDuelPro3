using System;
using System.Collections.Generic;
using DG.Tweening;
using MDPro3.Duel.YGOSharp;
using MDPro3.UI;
using MDPro3.UI.PropertyOverride;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.Servant
{
	// Token: 0x020012D6 RID: 4822
	public class DeckEditor : Servant
	{
		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x06008CE1 RID: 36065 RVA: 0x00128302 File Offset: 0x00126502
		// (set) Token: 0x06008CE2 RID: 36066 RVA: 0x00128309 File Offset: 0x00126509
		public static Deck Deck { get; set; }

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x06008CE3 RID: 36067 RVA: 0x00128311 File Offset: 0x00126511
		// (set) Token: 0x06008CE4 RID: 36068 RVA: 0x00128318 File Offset: 0x00126518
		public static string DeckName { get; set; }

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x06008CE5 RID: 36069 RVA: 0x00128320 File Offset: 0x00126520
		public static bool UseMobileLayout
		{
			get
			{
				return PropertyOverrider.NeedMobileLayout();
			}
		}

		// Token: 0x06008CE6 RID: 36070 RVA: 0x00128328 File Offset: 0x00126528
		public void SwitchCondition(DeckEditor.Condition condition, string deckName = "", Deck deck = null)
		{
			DeckEditor.condition = condition;
			switch (condition)
			{
			case DeckEditor.Condition.EditDeck:
				this.returnServant = Program.instance.deckSelector;
				DeckEditor.DeckName = Config.GetConfigDeckName(true);
				DeckEditor.Deck = new Deck("Deck/" + DeckEditor.DeckName + ".ydk");
				DeckEditor.DeckIsFromLocal = true;
				DeckEditor.historyCards = new List<int>();
				return;
			case DeckEditor.Condition.OnlineDeck:
				this.returnServant = Program.instance.onlineDeckViewer;
				DeckEditor.DeckName = deckName;
				DeckEditor.Deck = null;
				DeckEditor.DeckIsFromLocal = false;
				DeckEditor.historyCards = new List<int>();
				return;
			case DeckEditor.Condition.ReplayDeck:
				this.returnServant = Program.instance.replay;
				DeckEditor.DeckName = deckName;
				DeckEditor.Deck = deck;
				DeckEditor.DeckIsFromLocal = false;
				DeckEditor.historyCards = new List<int>();
				return;
			case DeckEditor.Condition.ChangeSide:
				DeckEditor.DeckName = Config.GetConfigDeckName(true);
				DeckEditor.Deck = TcpHelper.deck;
				DeckEditor.DeckIsFromLocal = false;
				DeckEditor.historyCards = OcgCore.sideReference.Main;
				return;
			default:
				return;
			}
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x06008CE7 RID: 36071 RVA: 0x00128420 File Offset: 0x00126620
		// (set) Token: 0x06008CE8 RID: 36072 RVA: 0x0012842D File Offset: 0x0012662D
		public DeckEditorUI.ResponseRegion ResponseRegion
		{
			get
			{
				return this.GetUI<DeckEditorUI>()._ResponseRegion;
			}
			set
			{
				this.GetUI<DeckEditorUI>()._ResponseRegion = value;
			}
		}

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x06008CE9 RID: 36073 RVA: 0x0012843B File Offset: 0x0012663B
		public override int Depth
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06008CEA RID: 36074 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool ShowLine
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06008CEB RID: 36075 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override bool NeedExitButton
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06008CEC RID: 36076 RVA: 0x0012843E File Offset: 0x0012663E
		public override float TransitionTime
		{
			get
			{
				return 0.6f;
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06008CED RID: 36077 RVA: 0x00128445 File Offset: 0x00126645
		protected override string Label_UI
		{
			get
			{
				if (!PropertyOverrider.NeedMobileLayout())
				{
					return "ServantUI/DeckEditorUI.prefab";
				}
				return "ServantUI/DeckEditorUIMobile.prefab";
			}
		}

		// Token: 0x06008CEE RID: 36078 RVA: 0x00128459 File Offset: 0x00126659
		public override void Initialize()
		{
			SystemEvent.OnResolutionChange += this.ChangeCanvasMatch;
			this.returnServant = Program.instance.deckSelector;
			DeckEditor.banlist = BanlistManager.Banlists[0];
			base.Initialize();
		}

		// Token: 0x06008CEF RID: 36079 RVA: 0x00128494 File Offset: 0x00126694
		public override void PerFrameFunction()
		{
			if (!this.NeedResponseInput())
			{
				return;
			}
			if (!UserInput.WasRightShoulderPressing)
			{
				if (UserInput.WasCancelPressed && DeckEditor.condition != DeckEditor.Condition.ChangeSide)
				{
					this.OnReturn();
				}
				if (UserInput.WasGamepadSelectPressed)
				{
					if (DeckEditor.condition == DeckEditor.Condition.ChangeSide)
					{
						this.GetUI<DeckEditorUI>().OnChangeSideComplete();
					}
					else
					{
						this.GetUI<DeckEditorUI>().OnSave();
					}
				}
				if (UserInput.WasGamepadStartPressed)
				{
					this.GetUI<DeckEditorUI>().OnSubMenu();
				}
				if (UserInput.WasLeftTriggerPressed)
				{
					this.GetUI<DeckEditorUI>().ShowCardActionMenu();
				}
				if (UserInput.WasRightTriggerPressed)
				{
					if (this.ResponseRegion == DeckEditorUI.ResponseRegion.Deck)
					{
						this.SelectLastCollectionViewItem();
					}
					else if (this.ResponseRegion == DeckEditorUI.ResponseRegion.Collection)
					{
						this.SelectLastDeckViewItem();
					}
				}
				if (this.ResponseRegion == DeckEditorUI.ResponseRegion.Deck)
				{
					if (UserInput.WasGamepadButtonNorthPressed)
					{
						this.GetUI<DeckEditorUI>().DeckView.ActivateInputField();
						return;
					}
					if (UserInput.WasGamepadButtonWestPressed)
					{
						this.GetUI<DeckEditorUI>().OnDeckButtonClicked();
						return;
					}
				}
				else if (this.ResponseRegion == DeckEditorUI.ResponseRegion.Collection)
				{
					if (this.GetUI<DeckEditorUI>().CardCollectionView.area == CardCollectionView.Area.Collection)
					{
						if (UserInput.WasLeftStickPressed)
						{
							this.GetUI<DeckEditorUI>().CardCollectionView.PrintSearchCards("");
						}
						if (this.GetUI<DeckEditorUI>().CardCollectionView.showingRelatedCards)
						{
							return;
						}
						if (UserInput.WasGamepadButtonNorthPressed)
						{
							if (UserInput.WasLeftShoulderPressing)
							{
								this.GetUI<DeckEditorUI>().CardCollectionView.ShowSortOrder();
							}
							else
							{
								this.GetUI<DeckEditorUI>().CardCollectionView.InputSearch.InputField.ActivateInputField();
							}
						}
						else if (UserInput.WasGamepadButtonWestPressed)
						{
							if (UserInput.WasLeftShoulderPressing)
							{
								this.GetUI<DeckEditorUI>().CardCollectionView.ResetFilters();
							}
							else
							{
								this.GetUI<DeckEditorUI>().CardCollectionView.ShowFilters();
							}
						}
					}
					if (UserInput.WasRightStickPressed)
					{
						this.GetUI<DeckEditorUI>().CardCollectionView.OnTabRight();
					}
				}
				return;
			}
			if (UserInput.WasGamepadButtonNorthPressed)
			{
				this.GetUI<DeckEditorUI>().OnRegulation();
				return;
			}
			if (UserInput.WasGamepadButtonWestPressed)
			{
				this.GetUI<DeckEditorUI>().SetCardInfoType();
				return;
			}
			if (UserInput.WasGamepadStartPressed)
			{
				this.GetUI<DeckEditorUI>().ShiftToAppearance();
			}
		}

		// Token: 0x06008CF0 RID: 36080 RVA: 0x00128679 File Offset: 0x00126879
		public override bool NeedResponseInput()
		{
			return !(this.servantUI == null) && !this.GetUI<DeckEditorUI>().CardActionMenu.showing && base.NeedResponseInput();
		}

		// Token: 0x06008CF1 RID: 36081 RVA: 0x001286A8 File Offset: 0x001268A8
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			if (this.ResponseRegion == DeckEditorUI.ResponseRegion.Collection)
			{
				this.SelectLastCollectionViewItem();
				return;
			}
			if (this.ResponseRegion == DeckEditorUI.ResponseRegion.Deck)
			{
				this.SelectLastDeckViewItem();
				return;
			}
			if (this.ResponseRegion == DeckEditorUI.ResponseRegion.Action)
			{
				if (this.lastSelectable != null)
				{
					this.lastSelectable.Select();
					return;
				}
				this.GetUI<DeckEditorUI>().CardActionMenu.SelectDefaultButton();
			}
		}

		// Token: 0x06008CF2 RID: 36082 RVA: 0x00128712 File Offset: 0x00126912
		public void SelectLastDeckViewItem()
		{
			this.ResponseRegion = DeckEditorUI.ResponseRegion.Deck;
			if (this.lastSelectedCardInDeck != null)
			{
				this.lastSelectedCardInDeck.GetSelectable().Select();
				return;
			}
			this.GetUI<DeckEditorUI>().DeckView.SelectDefaultItem();
		}

		// Token: 0x06008CF3 RID: 36083 RVA: 0x0012874A File Offset: 0x0012694A
		public void SelectNearestDeckViewItem(Vector3 position)
		{
			this.ResponseRegion = DeckEditorUI.ResponseRegion.Deck;
			UserInput.NextSelectionIsAxis = true;
			this.GetUI<DeckEditorUI>().DeckView.SelectNearestCard(position);
		}

		// Token: 0x06008CF4 RID: 36084 RVA: 0x0012876A File Offset: 0x0012696A
		public void SelectLastCollectionViewItem()
		{
			this.ResponseRegion = DeckEditorUI.ResponseRegion.Collection;
			if (this.lastSelectedCardInCollection != null)
			{
				EventSystem.current.SetSelectedGameObject(this.lastSelectedCardInCollection.gameObject);
				return;
			}
			this.GetUI<DeckEditorUI>().CardCollectionView.SelectDefaultItem();
		}

		// Token: 0x06008CF5 RID: 36085 RVA: 0x001287A7 File Offset: 0x001269A7
		public void SelectNearestCollectionViewItem(Vector3 position)
		{
			this.ResponseRegion = DeckEditorUI.ResponseRegion.Collection;
			UserInput.NextSelectionIsAxis = true;
			this.GetUI<DeckEditorUI>().CardCollectionView.SelectNearestCard(position);
		}

		// Token: 0x06008CF6 RID: 36086 RVA: 0x001287C8 File Offset: 0x001269C8
		public override void OnReturn()
		{
			if (!this.GetUI<DeckEditorUI>().DeckView.GetDirty() || !DeckEditor.DeckIsFromLocal)
			{
				base.OnReturn();
				return;
			}
			this.GetUI<DeckEditorUI>().callExit = true;
			UIManager.ShowPopupYesOrNo(new List<string>
			{
				InterString.Get("卡组未保存", 0),
				InterString.Get("卡组已修改，是否保存？", 0),
				InterString.Get("保存", 0),
				InterString.Get("不保存", 0)
			}, new Action(this.GetUI<DeckEditorUI>().OnSave), new Action(this.OnExit));
		}

		// Token: 0x06008CF7 RID: 36087 RVA: 0x0012886D File Offset: 0x00126A6D
		public override void JudgeInputBlockerExitMark(object o)
		{
			this.ResponseRegion = (DeckEditorUI.ResponseRegion)o;
		}

		// Token: 0x06008CF8 RID: 36088 RVA: 0x0012887C File Offset: 0x00126A7C
		public void CallExitIn(float time)
		{
			this.inTransition = true;
			DOTween.To(delegate(float v)
			{
			}, 0f, 0f, time).OnComplete(delegate
			{
				this.OnExit();
			});
		}

		// Token: 0x06008CF9 RID: 36089 RVA: 0x001288D1 File Offset: 0x00126AD1
		private void ChangeCanvasMatch()
		{
			if (!this.showing)
			{
				return;
			}
			UIManager.SetCanvasMatch(this.GetCanvasMatch(), 0f);
		}

		// Token: 0x06008CFA RID: 36090 RVA: 0x001288EC File Offset: 0x00126AEC
		public float GetCanvasMatch()
		{
			if ((float)Screen.width / (float)Screen.height > 1.7777778f)
			{
				return 1f;
			}
			return 0f;
		}

		// Token: 0x0400CB03 RID: 51971
		public static bool DeckIsFromLocal;

		// Token: 0x0400CB04 RID: 51972
		public static Banlist banlist;

		// Token: 0x0400CB05 RID: 51973
		public static List<int> historyCards;

		// Token: 0x0400CB06 RID: 51974
		public static string onlineDeckID;

		// Token: 0x0400CB07 RID: 51975
		public static DeckEditor.Condition condition;

		// Token: 0x0400CB08 RID: 51976
		[HideInInspector]
		public SelectionButton_CardInDeck lastSelectedCardInDeck;

		// Token: 0x0400CB09 RID: 51977
		[HideInInspector]
		public SelectionButton_CardInCollection lastSelectedCardInCollection;

		// Token: 0x0400CB0A RID: 51978
		public static bool ToHandTest;

		// Token: 0x020012D7 RID: 4823
		public enum Condition
		{
			// Token: 0x0400CB0C RID: 51980
			EditDeck,
			// Token: 0x0400CB0D RID: 51981
			OnlineDeck,
			// Token: 0x0400CB0E RID: 51982
			ReplayDeck,
			// Token: 0x0400CB0F RID: 51983
			ChangeSide
		}
	}
}
