using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001396 RID: 5014
	public class PopupDuelYesOrNo : PopupDuel
	{
		// Token: 0x060090F5 RID: 37109 RVA: 0x001402F8 File Offset: 0x0013E4F8
		public override void Show()
		{
			base.Show();
			AudioManager.PlaySE("SE_SYS_VERIFY", 1f);
		}

		// Token: 0x060090F6 RID: 37110 RVA: 0x00140310 File Offset: 0x0013E510
		public override void InitializeSelections()
		{
			base.InitializeSelections();
			this.description.text = this.selections[1];
			this.btnConfirm.transform.GetChild(0).GetComponent<Text>().text = this.selections[2];
			this.btnCancel.transform.GetChild(0).GetComponent<Text>().text = this.selections[3];
		}

		// Token: 0x060090F7 RID: 37111 RVA: 0x00140388 File Offset: 0x0013E588
		public override void OnConfirm()
		{
			base.OnConfirm();
			Action action = this.confirmAction;
			if (action != null)
			{
				action();
			}
			this.Hide();
		}

		// Token: 0x060090F8 RID: 37112 RVA: 0x001403A7 File Offset: 0x0013E5A7
		public override void OnCancel()
		{
			base.OnCancel();
			Action action = this.cancelAction;
			if (action != null)
			{
				action();
			}
			this.Hide();
		}

		// Token: 0x0400CFC3 RID: 53187
		[Header("Popup Duel YesOrNo Reference")]
		public Text description;

		// Token: 0x0400CFC4 RID: 53188
		public Action confirmAction;

		// Token: 0x0400CFC5 RID: 53189
		public Action cancelAction;
	}
}
