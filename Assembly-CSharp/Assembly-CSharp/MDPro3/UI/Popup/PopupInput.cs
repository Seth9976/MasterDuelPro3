using System;
using TMPro;
using UnityEngine;

namespace MDPro3.UI.Popup
{
	// Token: 0x02001488 RID: 5256
	public class PopupInput : Popup
	{
		// Token: 0x060099FC RID: 39420 RVA: 0x0016F818 File Offset: 0x0016DA18
		protected override void InitializeSelections()
		{
			base.InitializeSelections();
			this.input.GetComponent<TmpInputValidation>().type = this.validationType;
			this.input.text = this.args[1];
			this.input.ActivateInputField();
		}

		// Token: 0x060099FD RID: 39421 RVA: 0x0016F858 File Offset: 0x0016DA58
		protected override void OnCancel()
		{
			Action action = this.cancelAction;
			if (action != null)
			{
				action();
			}
			this.Hide();
		}

		// Token: 0x060099FE RID: 39422 RVA: 0x0016F871 File Offset: 0x0016DA71
		protected override void OnDecide()
		{
			Action<string> action = this.decideAction;
			if (action != null)
			{
				action(this.input.text);
			}
			this.Hide();
		}

		// Token: 0x0400D7A6 RID: 55206
		[Header("Popup Input Reference")]
		public TMP_InputField input;

		// Token: 0x0400D7A7 RID: 55207
		public Action<string> decideAction;

		// Token: 0x0400D7A8 RID: 55208
		public Action cancelAction;

		// Token: 0x0400D7A9 RID: 55209
		public TmpInputValidation.ValidationType validationType;
	}
}
