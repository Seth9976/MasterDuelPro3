using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014B RID: 331
	internal class TabDragPreview : VisualElement
	{
		// Token: 0x060009F8 RID: 2552 RVA: 0x00030D0A File Offset: 0x0002EF0A
		public TabDragPreview()
		{
			base.AddToClassList(TabDragPreview.ussClassName);
			base.pickingMode = PickingMode.Ignore;
		}

		// Token: 0x04000683 RID: 1667
		public static readonly string ussClassName = TabView.ussClassName + "__drag-preview";
	}
}
