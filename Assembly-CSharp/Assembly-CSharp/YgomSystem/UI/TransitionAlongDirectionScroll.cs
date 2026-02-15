using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000611 RID: 1553
	public class TransitionAlongDirectionScroll : MonoBehaviour
	{
		// Token: 0x0600317A RID: 12666 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnEdgeTransition(SelectionItem selectionItem, PadInputDirection direction)
		{
			return false;
		}

		// Token: 0x04002DE8 RID: 11752
		[SerializeField]
		private List<TransitionAlongDirection> m_TransitionAlongDirections;
	}
}
