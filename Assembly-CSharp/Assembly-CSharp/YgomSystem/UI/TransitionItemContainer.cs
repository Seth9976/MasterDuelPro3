using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomSystem.UI
{
	// Token: 0x02000612 RID: 1554
	public class TransitionItemContainer : MonoBehaviour
	{
		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x0000216A File Offset: 0x0000036A
		private List<SelectionItem> itemListUp
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x0000216A File Offset: 0x0000036A
		private List<SelectionItem> itemListDown
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x0000216A File Offset: 0x0000036A
		private List<SelectionItem> itemListRight
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x0000216A File Offset: 0x0000036A
		private List<SelectionItem> itemListLeft
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem GetItem(PadInputDirection direction)
		{
			return null;
		}

		// Token: 0x04002DE9 RID: 11753
		[SerializeField]
		private List<SelectionItem> _itemListUp;

		// Token: 0x04002DEA RID: 11754
		[SerializeField]
		private List<SelectionItem> _itemListDown;

		// Token: 0x04002DEB RID: 11755
		[SerializeField]
		private List<SelectionItem> _itemListRight;

		// Token: 0x04002DEC RID: 11756
		[SerializeField]
		private List<SelectionItem> _itemListLeft;
	}
}
