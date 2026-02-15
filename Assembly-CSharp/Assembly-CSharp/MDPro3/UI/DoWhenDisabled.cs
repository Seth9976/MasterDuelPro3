using System;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x0200136E RID: 4974
	public class DoWhenDisabled : MonoBehaviour
	{
		// Token: 0x0600901D RID: 36893 RVA: 0x0013AC08 File Offset: 0x00138E08
		private void OnDisable()
		{
			Action action = this.action;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0400CEC0 RID: 52928
		public Action action;
	}
}
