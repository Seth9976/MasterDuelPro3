using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MDPro3.UI.Popup
{
	// Token: 0x02001489 RID: 5257
	public class PopupProgress : Popup
	{
		// Token: 0x06009A00 RID: 39424 RVA: 0x0016F895 File Offset: 0x0016DA95
		protected override void OnCancel()
		{
			Action action = this.cancelAction;
			if (action != null)
			{
				action();
			}
			this.Hide();
		}

		// Token: 0x0400D7AA RID: 55210
		[Header("Popup Progress")]
		public Slider progressBar;

		// Token: 0x0400D7AB RID: 55211
		public TextMeshProUGUI text;

		// Token: 0x0400D7AC RID: 55212
		public Action cancelAction;
	}
}
