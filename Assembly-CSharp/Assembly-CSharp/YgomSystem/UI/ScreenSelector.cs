using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YgomSystem.UI
{
	// Token: 0x020005BD RID: 1469
	public class ScreenSelector : Selector
	{
		// Token: 0x06002E4B RID: 11851 RVA: 0x0000216A File Offset: 0x0000036A
		public static ScreenSelector Create(Transform parent, string group_label, int group_priority)
		{
			return null;
		}

		// Token: 0x06002E4C RID: 11852 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionButton AddShortCutKeyReceiver(SelectorManager.KeyType key_type, ScreenSelector.Type type, UnityAction click_event)
		{
			return null;
		}

		// Token: 0x06002E4D RID: 11853 RVA: 0x0000216D File Offset: 0x0000036D
		public void DeleteAllShortCutButton()
		{
		}

		// Token: 0x04002BFE RID: 11262
		private List<SelectionButton> buttonList;

		// Token: 0x020005BE RID: 1470
		public enum Type
		{
			// Token: 0x04002C00 RID: 11264
			FullScreen,
			// Token: 0x04002C01 RID: 11265
			Untouchable
		}
	}
}
