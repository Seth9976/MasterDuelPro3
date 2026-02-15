using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200139C RID: 5020
	public class PopupYesOrNo : PopupBase
	{
		// Token: 0x0600910B RID: 37131 RVA: 0x0013D626 File Offset: 0x0013B826
		public override void Show()
		{
			base.Show();
			AudioManager.PlaySE("SE_SYS_VERIFY", 1f);
		}

		// Token: 0x0600910C RID: 37132 RVA: 0x001408CC File Offset: 0x0013EACC
		public override void InitializeSelections()
		{
			base.InitializeSelections();
			this.description.text = this.selections[1];
			this.btnConfirm.transform.GetChild(0).GetComponent<Text>().text = this.selections[2];
			this.btnCancel.transform.GetChild(0).GetComponent<Text>().text = this.selections[3];
		}

		// Token: 0x0600910D RID: 37133 RVA: 0x00140944 File Offset: 0x0013EB44
		public override void OnConfirm()
		{
			base.OnConfirm();
			this.Hide();
			Action action = this.confirmAction;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600910E RID: 37134 RVA: 0x00140962 File Offset: 0x0013EB62
		public override void OnCancel()
		{
			base.OnCancel();
			this.Hide();
			Action action = this.cancelAction;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0400CFDA RID: 53210
		[Header("Popup YesOrNo Reference")]
		public Text description;

		// Token: 0x0400CFDB RID: 53211
		public Action confirmAction;

		// Token: 0x0400CFDC RID: 53212
		public Action cancelAction;
	}
}
