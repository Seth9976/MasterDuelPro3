using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x020005E9 RID: 1513
	internal class MultiColumnHeaderColumnResizePreview : VisualElement
	{
		// Token: 0x0600290E RID: 10510 RVA: 0x000A91FC File Offset: 0x000A73FC
		public MultiColumnHeaderColumnResizePreview()
		{
			base.AddToClassList(MultiColumnHeaderColumnResizePreview.ussClassName);
			base.pickingMode = PickingMode.Ignore;
			VisualElement visual = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			visual.AddToClassList(MultiColumnHeaderColumnResizePreview.visualUssClassName);
			base.Add(visual);
		}

		// Token: 0x040015A7 RID: 5543
		public static readonly string ussClassName = MultiColumnHeaderColumn.ussClassName + "__resize-preview";

		// Token: 0x040015A8 RID: 5544
		public static readonly string visualUssClassName = MultiColumnHeaderColumnResizePreview.ussClassName + "__visual";
	}
}
