using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000263 RID: 611
	internal class DefaultGroupManager : IGroupManager
	{
		// Token: 0x06001095 RID: 4245 RVA: 0x000470F4 File Offset: 0x000452F4
		public void Init(IGroupBox groupBox)
		{
			this.m_GroupBox = groupBox;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00047100 File Offset: 0x00045300
		public void OnOptionSelectionChanged(IGroupBoxOption selectedOption)
		{
			bool flag = this.m_SelectedOption == selectedOption;
			if (!flag)
			{
				this.m_SelectedOption = selectedOption;
				foreach (IGroupBoxOption option in this.m_GroupOptions)
				{
					option.SetSelected(option == this.m_SelectedOption);
				}
			}
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00047178 File Offset: 0x00045378
		public void RegisterOption(IGroupBoxOption option)
		{
			bool flag = !this.m_GroupOptions.Contains(option);
			if (flag)
			{
				this.m_GroupOptions.Add(option);
				this.m_GroupBox.OnOptionAdded(option);
			}
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x000471B5 File Offset: 0x000453B5
		public void UnregisterOption(IGroupBoxOption option)
		{
			this.m_GroupOptions.Remove(option);
			this.m_GroupBox.OnOptionRemoved(option);
		}

		// Token: 0x0400094D RID: 2381
		private List<IGroupBoxOption> m_GroupOptions = new List<IGroupBoxOption>();

		// Token: 0x0400094E RID: 2382
		private IGroupBoxOption m_SelectedOption;

		// Token: 0x0400094F RID: 2383
		private IGroupBox m_GroupBox;
	}
}
