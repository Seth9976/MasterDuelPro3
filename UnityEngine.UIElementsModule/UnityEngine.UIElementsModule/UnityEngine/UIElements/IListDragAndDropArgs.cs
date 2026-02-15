using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000191 RID: 401
	internal interface IListDragAndDropArgs
	{
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000BD3 RID: 3027
		int insertAtIndex { get; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000BD4 RID: 3028
		int parentId { get; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000BD5 RID: 3029
		int childIndex { get; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000BD6 RID: 3030
		DragAndDropData dragAndDropData { get; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000BD7 RID: 3031
		DragAndDropPosition dragAndDropPosition { get; }
	}
}
