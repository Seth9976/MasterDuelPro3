using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x02000194 RID: 404
	internal interface IDragAndDropController<in TArgs>
	{
		// Token: 0x06000BE3 RID: 3043
		bool CanStartDrag(IEnumerable<int> itemIds);

		// Token: 0x06000BE4 RID: 3044
		StartDragArgs SetupDragAndDrop(IEnumerable<int> itemIds, bool skipText = false);

		// Token: 0x06000BE5 RID: 3045
		DragVisualMode HandleDragAndDrop(TArgs args);

		// Token: 0x06000BE6 RID: 3046
		void OnDrop(TArgs args);

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000020EA File Offset: 0x000002EA
		void DragCleanup()
		{
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000020EA File Offset: 0x000002EA
		void HandleAutoExpand(ReusableCollectionItem item, Vector2 pointerPosition)
		{
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00038B9A File Offset: 0x00036D9A
		IEnumerable<int> GetSortedSelectedIds()
		{
			return Enumerable.Empty<int>();
		}
	}
}
