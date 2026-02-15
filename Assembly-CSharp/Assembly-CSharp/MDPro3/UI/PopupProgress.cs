using System;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI
{
	// Token: 0x02001398 RID: 5016
	public class PopupProgress : PopupBase
	{
		// Token: 0x060090FE RID: 37118 RVA: 0x00140444 File Offset: 0x0013E644
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

		// Token: 0x0400CFCA RID: 53194
		[Header("Popup Progress")]
		public Text description;

		// Token: 0x0400CFCB RID: 53195
		public Slider progressBar;

		// Token: 0x0400CFCC RID: 53196
		public Action cancelAction;
	}
}
