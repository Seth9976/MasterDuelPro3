using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014C RID: 332
	internal class TabDragLocationPreview : VisualElement
	{
		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00030D3E File Offset: 0x0002EF3E
		internal VisualElement preview
		{
			get
			{
				return this.m_Preview;
			}
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x00030D48 File Offset: 0x0002EF48
		public TabDragLocationPreview()
		{
			base.AddToClassList(TabDragLocationPreview.ussClassName);
			base.pickingMode = PickingMode.Ignore;
			this.m_Preview = new VisualElement();
			this.m_Preview.AddToClassList(TabDragLocationPreview.visualUssClassName);
			this.m_Preview.pickingMode = PickingMode.Ignore;
			base.Add(this.m_Preview);
		}

		// Token: 0x04000684 RID: 1668
		public static readonly string ussClassName = TabView.ussClassName + "__drag-location-preview";

		// Token: 0x04000685 RID: 1669
		public static readonly string visualUssClassName = TabDragLocationPreview.ussClassName + "__visual";

		// Token: 0x04000686 RID: 1670
		public static readonly string verticalUssClassName = TabDragLocationPreview.ussClassName + "__vertical";

		// Token: 0x04000687 RID: 1671
		public static readonly string horizontalUssClassName = TabDragLocationPreview.ussClassName + "__horizontal";

		// Token: 0x04000688 RID: 1672
		private VisualElement m_Preview;
	}
}
