using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x020005D7 RID: 1495
	public class SelectionTransitionReciever : MonoBehaviour
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06002F88 RID: 12168 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem recieverItem
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnEnable()
		{
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ThrowToNearTarget()
		{
			return false;
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnSelected()
		{
		}

		// Token: 0x04002C82 RID: 11394
		public List<SelectionItem> m_ThrowTargets;

		// Token: 0x04002C83 RID: 11395
		private SelectionItem m_RecieverCache;
	}
}
