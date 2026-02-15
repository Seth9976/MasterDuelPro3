using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001397 RID: 5015
	public class PopupInput : PopupBase
	{
		// Token: 0x060090FA RID: 37114 RVA: 0x001403C6 File Offset: 0x0013E5C6
		public override void InitializeSelections()
		{
			base.InitializeSelections();
			this.input.GetComponent<InputValidation>().type = this.validationType;
			this.input.text = this.selections[1];
		}

		// Token: 0x060090FB RID: 37115 RVA: 0x001403FB File Offset: 0x0013E5FB
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

		// Token: 0x060090FC RID: 37116 RVA: 0x0014041A File Offset: 0x0013E61A
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

		// Token: 0x0400CFC6 RID: 53190
		[Header("Popup Select Reference")]
		public InputField input;

		// Token: 0x0400CFC7 RID: 53191
		public Action<string> confirmAction;

		// Token: 0x0400CFC8 RID: 53192
		public Action cancelAction;

		// Token: 0x0400CFC9 RID: 53193
		public InputValidation.ValidationType validationType;
	}
}
