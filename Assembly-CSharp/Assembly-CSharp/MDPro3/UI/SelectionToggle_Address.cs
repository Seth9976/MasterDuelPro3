using System;
using MDPro3.UI.ServantUI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI
{
	// Token: 0x020013B2 RID: 5042
	public class SelectionToggle_Address : SelectionToggle_ScrollRectItem
	{
		// Token: 0x06009225 RID: 37413 RVA: 0x00146518 File Offset: 0x00144718
		public override void Refresh()
		{
			this.SetButtonText(this.addressName);
		}

		// Token: 0x06009226 RID: 37414 RVA: 0x00146528 File Offset: 0x00144728
		protected override void CallSubmitEvent()
		{
			base.CallSubmitEvent();
			AudioManager.PlaySE("SE_MENU_DECIDE", 1f);
			Program.instance.online.GetUI<OnlineServantUI>().PageLegacy.SetHost(this.addressHost, this.addressPort, this.addressPassword);
		}

		// Token: 0x06009227 RID: 37415 RVA: 0x00146575 File Offset: 0x00144775
		protected override void CallToggleOnEvent()
		{
			base.CallHoverOnEvent();
			Program.instance.online.lastSelectedAddressItem = this;
		}

		// Token: 0x06009228 RID: 37416 RVA: 0x0014658D File Offset: 0x0014478D
		protected override void OnClick()
		{
			Program.instance.online.lastSelectedAddressItem = this;
			this.CallSubmitEvent();
		}

		// Token: 0x06009229 RID: 37417 RVA: 0x001465A5 File Offset: 0x001447A5
		protected override void ToggleOn()
		{
			this.isOn = true;
		}

		// Token: 0x0600922A RID: 37418 RVA: 0x001465A5 File Offset: 0x001447A5
		public override void ToggleOnNow()
		{
			this.isOn = true;
		}

		// Token: 0x0600922B RID: 37419 RVA: 0x001465AE File Offset: 0x001447AE
		protected override void ToggleOff()
		{
			this.isOn = false;
		}

		// Token: 0x0600922C RID: 37420 RVA: 0x001465AE File Offset: 0x001447AE
		public override void ToggleOffNow()
		{
			this.isOn = false;
		}

		// Token: 0x0600922D RID: 37421 RVA: 0x001465B7 File Offset: 0x001447B7
		protected override void OnNavigation(AxisEventData eventData)
		{
			base.OnNavigation(eventData);
			if (eventData.moveDir == MoveDirection.Right)
			{
				UserInput.NextSelectionIsAxis = true;
				Program.instance.online.GetUI<OnlineServantUI>().SelectLastSelectable(null);
			}
		}

		// Token: 0x0600922E RID: 37422 RVA: 0x001465E4 File Offset: 0x001447E4
		public void OnDelete()
		{
			Program.instance.online.GetUI<OnlineServantUI>().PageLegacy.DeleteAddress(this.addressName);
		}

		// Token: 0x0600922F RID: 37423 RVA: 0x00146605 File Offset: 0x00144805
		public void OnMoveUp()
		{
			Program.instance.online.GetUI<OnlineServantUI>().PageLegacy.AddressMoveUp(this.addressName);
		}

		// Token: 0x0400D0B9 RID: 53433
		[Header("SelectionToggle Address")]
		public string addressName;

		// Token: 0x0400D0BA RID: 53434
		public string addressHost;

		// Token: 0x0400D0BB RID: 53435
		public string addressPort;

		// Token: 0x0400D0BC RID: 53436
		public string addressPassword;
	}
}
