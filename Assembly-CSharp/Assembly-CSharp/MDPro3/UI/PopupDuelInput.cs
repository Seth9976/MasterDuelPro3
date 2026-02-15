using System;
using MDPro3.Servant;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x0200138C RID: 5004
	public class PopupDuelInput : PopupDuel
	{
		// Token: 0x060090BA RID: 37050 RVA: 0x0013D9A4 File Offset: 0x0013BBA4
		public override void InitializeSelections()
		{
			base.InitializeSelections();
			this.btnConfirm.transform.GetChild(0).GetComponent<Text>().text = this.selections[1];
			this.btnCancel.transform.GetChild(0).GetComponent<Text>().text = this.selections[2];
			this.input.text = this.selections[3];
			this.input.GetComponent<InputValidation>().type = this.validationType;
			if (this.cancelAction == null)
			{
				this.btnCancel.gameObject.SetActive(false);
				float height = this.btnConfirm.GetComponent<RectTransform>().anchoredPosition.y;
				this.btnConfirm.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, height);
			}
			OcgCore.inputMode = true;
			Program.instance.ocgcore.GreenBackgroundOff();
		}

		// Token: 0x060090BB RID: 37051 RVA: 0x0013DA91 File Offset: 0x0013BC91
		public override void OnConfirm()
		{
			base.OnConfirm();
			Action<string> action = this.confirmAction;
			if (action != null)
			{
				action(this.input.text);
			}
			this.Hide();
		}

		// Token: 0x060090BC RID: 37052 RVA: 0x0013DABB File Offset: 0x0013BCBB
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

		// Token: 0x060090BD RID: 37053 RVA: 0x0013DADA File Offset: 0x0013BCDA
		private new void OnDestroy()
		{
			OcgCore.inputMode = false;
		}

		// Token: 0x0400CF74 RID: 53108
		[Header("Popup Duel Input")]
		public Action<string> confirmAction;

		// Token: 0x0400CF75 RID: 53109
		public Action cancelAction;

		// Token: 0x0400CF76 RID: 53110
		public InputField input;

		// Token: 0x0400CF77 RID: 53111
		public InputValidation.ValidationType validationType;
	}
}
