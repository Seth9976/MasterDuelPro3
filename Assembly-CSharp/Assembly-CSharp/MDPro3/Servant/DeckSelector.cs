using System;
using MDPro3.UI;
using MDPro3.UI.ServantUI;
using UnityEngine;

namespace MDPro3.Servant
{
	// Token: 0x020012D9 RID: 4825
	public class DeckSelector : Servant
	{
		// Token: 0x06008D00 RID: 36096 RVA: 0x00128924 File Offset: 0x00126B24
		public void SwitchCondition(DeckSelector.Condition condition)
		{
			DeckSelector.condition = condition;
			switch (condition)
			{
			case DeckSelector.Condition.ForEdit:
				this.returnServant = Program.instance.menu;
				return;
			case DeckSelector.Condition.ForDuel:
				this.returnServant = Program.instance.room;
				return;
			case DeckSelector.Condition.ForSolo:
				this.returnServant = Program.instance.solo;
				return;
			case DeckSelector.Condition.MyCard:
				this.returnServant = Program.instance.online;
				return;
			default:
				return;
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06008D01 RID: 36097 RVA: 0x00128991 File Offset: 0x00126B91
		public override int Depth
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06008D02 RID: 36098 RVA: 0x0000763C File Offset: 0x0000583C
		protected override bool ShowLine
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06008D03 RID: 36099 RVA: 0x00128994 File Offset: 0x00126B94
		public string DeckType
		{
			get
			{
				return this.GetUI<DeckSelectorUI>().deckType;
			}
		}

		// Token: 0x06008D04 RID: 36100 RVA: 0x001289A1 File Offset: 0x00126BA1
		public override void Initialize()
		{
			base.Initialize();
			this.SwitchCondition(DeckSelector.Condition.ForEdit);
		}

		// Token: 0x06008D05 RID: 36101 RVA: 0x001289B0 File Offset: 0x00126BB0
		public override void OnExit()
		{
			if (Program.exitOnReturn)
			{
				Program.GameQuit();
				return;
			}
			Program.instance.ShiftToServant(this.returnServant);
		}

		// Token: 0x06008D06 RID: 36102 RVA: 0x001289D0 File Offset: 0x00126BD0
		public override void PerFrameFunction()
		{
			if (!this.showing)
			{
				return;
			}
			if (this.NeedResponseInput())
			{
				if (UserInput.WasLeftStickPressed)
				{
					this.GetUI<DeckSelectorUI>().TogglePickupCard.SwitchToggle();
				}
				if (UserInput.WasGamepadButtonWestPressed)
				{
					AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
					if (this.GetUI<DeckSelectorUI>().ButtonOnline.gameObject.activeSelf)
					{
						this.GetUI<DeckSelectorUI>().OnOnlineDeckView();
					}
					else
					{
						this.GetUI<DeckSelectorUI>().OnConfirm();
					}
				}
				if (UserInput.WasGamepadButtonNorthPressed)
				{
					AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
					this.GetUI<DeckSelectorUI>().ActivateInputField();
				}
				if (UserInput.WasLeftShoulderPressed && this.GetUI<DeckSelectorUI>().ButtonType.gameObject.activeSelf)
				{
					AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
					this.GetUI<DeckSelectorUI>().OnType();
				}
				if (UserInput.WasRightShoulderPressed && this.GetUI<DeckSelectorUI>().ButtonOnline.gameObject.activeSelf)
				{
					AudioManager.PlaySE("SE_MENU_SELECT_01", 1f);
					this.GetUI<DeckSelectorUI>().OnDelete();
				}
				if (UserInput.MouseRightDown || UserInput.WasCancelPressed)
				{
					if (this.GetUI<DeckSelectorUI>().ButtonOnline.gameObject.activeSelf)
					{
						this.OnReturn();
						return;
					}
					AudioManager.PlaySE("SE_MENU_CANCEL", 1f);
					this.GetUI<DeckSelectorUI>().OnCancel();
				}
			}
		}

		// Token: 0x06008D07 RID: 36103 RVA: 0x00128B24 File Offset: 0x00126D24
		public override void Select(bool forced = false)
		{
			if (!forced && !UserInput.NeedDefaultSelect())
			{
				return;
			}
			this.lastSelectedDeckItem.GetSelectable().Select();
		}

		// Token: 0x06008D08 RID: 36104 RVA: 0x00128B41 File Offset: 0x00126D41
		public override bool NeedResponseInput()
		{
			return !(this.servantUI == null) && !this.GetUI<DeckSelectorUI>().buttonLayoutSwitching && base.NeedResponseInput();
		}

		// Token: 0x06008D09 RID: 36105 RVA: 0x00127555 File Offset: 0x00125755
		public void SelectLastDeckItem()
		{
			UserInput.NextSelectionIsAxis = true;
			this.Select(false);
		}

		// Token: 0x0400CB12 RID: 51986
		[HideInInspector]
		public SelectionToggle_Deck lastSelectedDeckItem;

		// Token: 0x0400CB13 RID: 51987
		public static DeckSelector.Condition condition;

		// Token: 0x020012DA RID: 4826
		public enum Condition
		{
			// Token: 0x0400CB15 RID: 51989
			ForEdit,
			// Token: 0x0400CB16 RID: 51990
			ForDuel,
			// Token: 0x0400CB17 RID: 51991
			ForSolo,
			// Token: 0x0400CB18 RID: 51992
			MyCard
		}
	}
}
