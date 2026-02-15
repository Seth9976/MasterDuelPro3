using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x020005E7 RID: 1511
	internal class MultiColumnHeaderColumnMoveLocationPreview : VisualElement
	{
		// Token: 0x060028F0 RID: 10480 RVA: 0x000A8788 File Offset: 0x000A6988
		public MultiColumnHeaderColumnMoveLocationPreview()
		{
			base.AddToClassList(MultiColumnHeaderColumnMoveLocationPreview.ussClassName);
			base.pickingMode = PickingMode.Ignore;
			VisualElement visual = new VisualElement();
			visual.AddToClassList(MultiColumnHeaderColumnMoveLocationPreview.visualUssClassName);
			visual.pickingMode = PickingMode.Ignore;
			base.Add(visual);
		}

		// Token: 0x04001595 RID: 5525
		public static readonly string ussClassName = MultiColumnHeaderColumn.ussClassName + "__move-location-preview";

		// Token: 0x04001596 RID: 5526
		public static readonly string visualUssClassName = MultiColumnHeaderColumnMoveLocationPreview.ussClassName + "__visual";
	}
}
