using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005D6 RID: 1494
	public class SelectionItemChangeListener : MonoBehaviour
	{
		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06002F83 RID: 12163 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06002F84 RID: 12164 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action<SelectionItem, SelectionItem> onChangedSelectionItemEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x0000216A File Offset: 0x0000036A
		public static SelectionItemChangeListener Attach(GameObject owner)
		{
			return null;
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x0000216D File Offset: 0x0000036D
		private void LateUpdate()
		{
		}

		// Token: 0x04002C81 RID: 11393
		private SelectionItem m_LastSelectItem;
	}
}
