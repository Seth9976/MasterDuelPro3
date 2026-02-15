using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x020005F5 RID: 1525
	internal class MultiColumnHeaderColumnResizeHandle : VisualElement
	{
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x000ABCA9 File Offset: 0x000A9EA9
		public VisualElement dragArea { get; }

		// Token: 0x06002982 RID: 10626 RVA: 0x000ABCB4 File Offset: 0x000A9EB4
		public MultiColumnHeaderColumnResizeHandle()
		{
			base.AddToClassList(MultiColumnHeaderColumnResizeHandle.ussClassName);
			this.dragArea = new VisualElement
			{
				focusable = true
			};
			this.dragArea.AddToClassList(MultiColumnHeaderColumnResizeHandle.dragAreaUssClassName);
			base.Add(this.dragArea);
		}

		// Token: 0x040015F3 RID: 5619
		public static readonly string ussClassName = MultiColumnCollectionHeader.ussClassName + "__column-resize-handle";

		// Token: 0x040015F4 RID: 5620
		public static readonly string dragAreaUssClassName = MultiColumnHeaderColumnResizeHandle.ussClassName + "__drag-area";
	}
}
