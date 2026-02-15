using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000196 RID: 406
	public readonly struct CanStartDragArgs
	{
		// Token: 0x06000BEA RID: 3050 RVA: 0x00038BA1 File Offset: 0x00036DA1
		internal CanStartDragArgs(VisualElement draggedElement, int id, IEnumerable<int> selectedIds)
		{
			this.draggedElement = draggedElement;
			this.id = id;
			this.selectedIds = selectedIds;
		}

		// Token: 0x0400078A RID: 1930
		public readonly VisualElement draggedElement;

		// Token: 0x0400078B RID: 1931
		public readonly int id;

		// Token: 0x0400078C RID: 1932
		public readonly IEnumerable<int> selectedIds;
	}
}
