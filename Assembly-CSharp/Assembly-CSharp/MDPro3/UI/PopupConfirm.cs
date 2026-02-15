using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001389 RID: 5001
	public class PopupConfirm : PopupBase
	{
		// Token: 0x060090A9 RID: 37033 RVA: 0x0013D607 File Offset: 0x0013B807
		public override void InitializeSelections()
		{
			base.InitializeSelections();
			this.description.text = this.selections[1];
		}

		// Token: 0x060090AA RID: 37034 RVA: 0x0013D626 File Offset: 0x0013B826
		public override void Show()
		{
			base.Show();
			AudioManager.PlaySE("SE_SYS_VERIFY", 1f);
		}

		// Token: 0x060090AB RID: 37035 RVA: 0x0013D63D File Offset: 0x0013B83D
		public override void OnConfirm()
		{
			base.OnConfirm();
			this.Hide();
		}

		// Token: 0x0400CF6D RID: 53101
		[Header("Popup Confirm")]
		public Text description;
	}
}
