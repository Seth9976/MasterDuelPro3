using System;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005D8 RID: 1496
	[Serializable]
	public class SelectionTransitionSetting
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06002F8D RID: 12173 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06002F8E RID: 12174 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionItem.TransitionMode mode
		{
			get
			{
				return SelectionItem.TransitionMode.Automatic;
			}
			set
			{
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06002F8F RID: 12175 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002F90 RID: 12176 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionItem manualItem
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06002F91 RID: 12177 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002F92 RID: 12178 RVA: 0x0000216D File Offset: 0x0000036D
		public Selector manualSelector
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06002F93 RID: 12179 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTransitionSetting(PadInputDirection direction, SelectionItem target)
		{
		}

		// Token: 0x04002C84 RID: 11396
		[SerializeField]
		private SelectionItem.TransitionMode m_Mode;

		// Token: 0x04002C85 RID: 11397
		[SerializeField]
		private SelectionItem m_ManualItem;

		// Token: 0x04002C86 RID: 11398
		[SerializeField]
		private Selector m_ManualSelector;
	}
}
