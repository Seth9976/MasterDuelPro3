using System;
using MDPro3.UI.ServantUI;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x020013CA RID: 5066
	public class SelectionToggle_Mate : SelectionToggle_ScrollRectItem
	{
		// Token: 0x060092BB RID: 37563 RVA: 0x00149E8C File Offset: 0x0014808C
		public override void Refresh()
		{
			this.SetButtonText(this.mateName);
		}

		// Token: 0x060092BC RID: 37564 RVA: 0x00149E9A File Offset: 0x0014809A
		protected override void CallSubmitEvent()
		{
			AudioManager.PlaySE("SE_MENU_DECIDE", 1f);
			if (this.code == 0)
			{
				return;
			}
			Program.instance.mate.ViewMate(this.code);
		}

		// Token: 0x060092BD RID: 37565 RVA: 0x00149EC9 File Offset: 0x001480C9
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			Program.instance.mate.lastSelectedMateItem = this;
		}

		// Token: 0x060092BE RID: 37566 RVA: 0x00149EE1 File Offset: 0x001480E1
		protected override void OnClick()
		{
			Program.instance.mate.lastSelectedMateItem = this;
			this.CallSubmitEvent();
		}

		// Token: 0x060092BF RID: 37567 RVA: 0x001465A5 File Offset: 0x001447A5
		protected override void ToggleOn()
		{
			this.isOn = true;
		}

		// Token: 0x060092C0 RID: 37568 RVA: 0x001465A5 File Offset: 0x001447A5
		public override void ToggleOnNow()
		{
			this.isOn = true;
		}

		// Token: 0x060092C1 RID: 37569 RVA: 0x001465AE File Offset: 0x001447AE
		protected override void ToggleOff()
		{
			this.isOn = false;
		}

		// Token: 0x060092C2 RID: 37570 RVA: 0x001465AE File Offset: 0x001447AE
		public override void ToggleOffNow()
		{
			this.isOn = false;
		}

		// Token: 0x060092C3 RID: 37571 RVA: 0x00149EF9 File Offset: 0x001480F9
		protected override void OnNavigation(AxisEventData eventData)
		{
			base.OnNavigation(eventData);
			if (eventData.moveDir == MoveDirection.Right)
			{
				UserInput.NextSelectionIsAxis = true;
				Program.instance.mate.GetUI<MateViewerUI>().SelectButtonInteract();
			}
		}

		// Token: 0x0400D122 RID: 53538
		public int code;

		// Token: 0x0400D123 RID: 53539
		public string mateName;
	}
}
