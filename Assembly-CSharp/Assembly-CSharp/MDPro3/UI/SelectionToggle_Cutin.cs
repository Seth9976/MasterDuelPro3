using System;
using MDPro3.Servant;
using MDPro3.UI.ServantUI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x020013C0 RID: 5056
	public class SelectionToggle_Cutin : SelectionToggle_ScrollRectItem
	{
		// Token: 0x0600927C RID: 37500 RVA: 0x00147FA4 File Offset: 0x001461A4
		public override void Refresh()
		{
			base.Manager.GetElement<TextMeshProUGUI>("ButtonText").text = this.cardName;
			if (CutinViewer.codes.Contains(this.code))
			{
				base.Manager.GetElement<TextMeshProUGUI>("ButtonText").color = Color.white;
				return;
			}
			base.Manager.GetElement<TextMeshProUGUI>("ButtonText").color = this.colorForDiyCutin;
		}

		// Token: 0x0600927D RID: 37501 RVA: 0x00148014 File Offset: 0x00146214
		protected override void CallSubmitEvent()
		{
			AudioManager.PlaySE("SE_MENU_DECIDE", 1f);
			if (CutinViewer.HasCutin(this.code))
			{
				CutinViewer.Play(this.code, 0);
			}
		}

		// Token: 0x0600927E RID: 37502 RVA: 0x0014803F File Offset: 0x0014623F
		protected override void CallToggleOnEvent()
		{
			base.CallToggleOnEvent();
			Program.instance.cutin.lastSelectedCutinItem = this;
		}

		// Token: 0x0600927F RID: 37503 RVA: 0x00148057 File Offset: 0x00146257
		protected override void OnClick()
		{
			Program.instance.cutin.lastSelectedCutinItem = this;
			this.CallSubmitEvent();
		}

		// Token: 0x06009280 RID: 37504 RVA: 0x001465A5 File Offset: 0x001447A5
		protected override void ToggleOn()
		{
			this.isOn = true;
		}

		// Token: 0x06009281 RID: 37505 RVA: 0x001465A5 File Offset: 0x001447A5
		public override void ToggleOnNow()
		{
			this.isOn = true;
		}

		// Token: 0x06009282 RID: 37506 RVA: 0x001465AE File Offset: 0x001447AE
		protected override void ToggleOff()
		{
			this.isOn = false;
		}

		// Token: 0x06009283 RID: 37507 RVA: 0x001465AE File Offset: 0x001447AE
		public override void ToggleOffNow()
		{
			this.isOn = false;
		}

		// Token: 0x06009284 RID: 37508 RVA: 0x0014806F File Offset: 0x0014626F
		protected override void OnNavigation(AxisEventData eventData)
		{
			base.OnNavigation(eventData);
			if (eventData.moveDir == MoveDirection.Right)
			{
				UserInput.NextSelectionIsAxis = true;
				Program.instance.cutin.GetUI<CutinViewerUI>().ButtonAutoPlay.GetSelectable().Select();
			}
		}

		// Token: 0x0400D0E6 RID: 53478
		public int code;

		// Token: 0x0400D0E7 RID: 53479
		public string cardName;

		// Token: 0x0400D0E8 RID: 53480
		private Color colorForDiyCutin = Color.gray;
	}
}
