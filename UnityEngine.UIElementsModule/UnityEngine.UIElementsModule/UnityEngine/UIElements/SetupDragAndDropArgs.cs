using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000197 RID: 407
	public readonly struct SetupDragAndDropArgs
	{
		// Token: 0x06000BEB RID: 3051 RVA: 0x00038BB9 File Offset: 0x00036DB9
		internal SetupDragAndDropArgs(VisualElement draggedElement, IEnumerable<int> selectedIds, StartDragArgs startDragArgs)
		{
			this.draggedElement = draggedElement;
			this.selectedIds = selectedIds;
			this.startDragArgs = startDragArgs;
		}

		// Token: 0x0400078D RID: 1933
		public readonly VisualElement draggedElement;

		// Token: 0x0400078E RID: 1934
		public readonly IEnumerable<int> selectedIds;

		// Token: 0x0400078F RID: 1935
		public readonly StartDragArgs startDragArgs;
	}
}
