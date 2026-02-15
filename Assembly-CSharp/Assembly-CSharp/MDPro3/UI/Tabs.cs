using System;
using System.Collections.Generic;
using UnityEngine;

namespace MDPro3.UI
{
	// Token: 0x02001382 RID: 4994
	public class Tabs : MonoBehaviour
	{
		// Token: 0x06009083 RID: 36995 RVA: 0x0013CBD8 File Offset: 0x0013ADD8
		private void Start()
		{
			foreach (Tab tab in this.tabs)
			{
				SystemEvent.OnResolutionChange += tab.AdjustSize;
			}
		}

		// Token: 0x06009084 RID: 36996 RVA: 0x0013CC34 File Offset: 0x0013AE34
		public void Tab(Tab tab)
		{
			foreach (Tab t in this.tabs)
			{
				if (t != tab)
				{
					t.CancelSelect();
				}
			}
		}

		// Token: 0x06009085 RID: 36997 RVA: 0x0013CC90 File Offset: 0x0013AE90
		public void AdjustSize()
		{
			foreach (Tab tab in this.tabs)
			{
				tab.AdjustSize();
			}
		}

		// Token: 0x0400CF37 RID: 53047
		public List<Tab> tabs = new List<Tab>();
	}
}
