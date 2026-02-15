using System;
using UnityEngine;
using UnityEngine.Events;
using YgomSystem.UI;

namespace MDPro3.UI.Popup
{
	// Token: 0x02001492 RID: 5266
	public class SelectionToggle_PopupSelectionItem : SelectionToggle_ScrollRectItem
	{
		// Token: 0x06009A28 RID: 39464 RVA: 0x00170CD0 File Offset: 0x0016EED0
		private void SetTextColor()
		{
			Color targetColor = Color.white;
			if (this.color == "r")
			{
				targetColor = Color.red;
			}
			else if (this.color == "g")
			{
				targetColor = Color.green;
			}
			else if (this.color == "b")
			{
				targetColor = Color.cyan;
			}
			base.ButtonText.GetComponent<ColorContainerGraphic>().baseColor = targetColor;
			this.SetButtonTextColor(targetColor);
		}

		// Token: 0x06009A29 RID: 39465 RVA: 0x00170D47 File Offset: 0x0016EF47
		public override void Refresh()
		{
			this.SetButtonText(this.selection);
			this.SetTextColor();
			this.RemoveAllListeners();
			this.SetClickEvent(new UnityAction(this.clickAction.Invoke));
		}

		// Token: 0x06009A2A RID: 39466 RVA: 0x00170D78 File Offset: 0x0016EF78
		protected override void ToggleOn()
		{
			this.isOn = true;
			this.manager.lastSelectedItem = this;
		}

		// Token: 0x06009A2B RID: 39467 RVA: 0x001465A5 File Offset: 0x001447A5
		public override void ToggleOnNow()
		{
			this.isOn = true;
		}

		// Token: 0x06009A2C RID: 39468 RVA: 0x001465AE File Offset: 0x001447AE
		protected override void ToggleOff()
		{
			this.isOn = false;
		}

		// Token: 0x06009A2D RID: 39469 RVA: 0x001465AE File Offset: 0x001447AE
		public override void ToggleOffNow()
		{
			this.isOn = false;
		}

		// Token: 0x0400D7CB RID: 55243
		public string selection;

		// Token: 0x0400D7CC RID: 55244
		public string color = string.Empty;

		// Token: 0x0400D7CD RID: 55245
		public Action clickAction;

		// Token: 0x0400D7CE RID: 55246
		public PopupSelection manager;
	}
}
