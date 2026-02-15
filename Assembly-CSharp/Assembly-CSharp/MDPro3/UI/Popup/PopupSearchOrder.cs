using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MDPro3.UI.Popup
{
	// Token: 0x0200148D RID: 5261
	public class PopupSearchOrder : Popup
	{
		// Token: 0x06009A14 RID: 39444 RVA: 0x0016F7F9 File Offset: 0x0016D9F9
		public override void Show()
		{
			base.Show();
			AudioManager.PlaySE("SE_SYS_VERIFY", 1f);
		}

		// Token: 0x06009A15 RID: 39445 RVA: 0x001707BB File Offset: 0x0016E9BB
		protected override void SelectLastSelected()
		{
			EventSystem.current.SetSelectedGameObject(this.lastSelectedToggle.gameObject);
		}

		// Token: 0x06009A16 RID: 39446 RVA: 0x001707D2 File Offset: 0x0016E9D2
		public void SelectLastItem()
		{
			UserInput.NextSelectionIsAxis = true;
			this.SelectLastSelected();
		}

		// Token: 0x0400D7BE RID: 55230
		[Header("Popup Selection")]
		public SelectionToggle_SearchOrder lastSelectedToggle;
	}
}
