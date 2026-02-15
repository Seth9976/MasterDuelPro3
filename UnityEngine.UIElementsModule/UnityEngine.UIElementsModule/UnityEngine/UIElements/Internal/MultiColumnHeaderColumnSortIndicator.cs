using System;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x020005F2 RID: 1522
	internal class MultiColumnHeaderColumnSortIndicator : VisualElement
	{
		// Token: 0x17000AB5 RID: 2741
		// (set) Token: 0x0600295E RID: 10590 RVA: 0x000AB0BF File Offset: 0x000A92BF
		public string sortOrderLabel
		{
			set
			{
				this.m_IndexLabel.text = value;
			}
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000AB0D0 File Offset: 0x000A92D0
		public MultiColumnHeaderColumnSortIndicator()
		{
			base.AddToClassList(MultiColumnHeaderColumnSortIndicator.ussClassName);
			base.pickingMode = PickingMode.Ignore;
			VisualElement arrowIndicator = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			arrowIndicator.AddToClassList(MultiColumnHeaderColumnSortIndicator.arrowUssClassName);
			base.Add(arrowIndicator);
			this.m_IndexLabel = new Label
			{
				pickingMode = PickingMode.Ignore
			};
			this.m_IndexLabel.AddToClassList(MultiColumnHeaderColumnSortIndicator.indexLabelUssClassName);
			base.Add(this.m_IndexLabel);
		}

		// Token: 0x040015D5 RID: 5589
		public static readonly string ussClassName = MultiColumnHeaderColumn.ussClassName + "__sort-indicator";

		// Token: 0x040015D6 RID: 5590
		public static readonly string arrowUssClassName = MultiColumnHeaderColumnSortIndicator.ussClassName + "__arrow";

		// Token: 0x040015D7 RID: 5591
		public static readonly string indexLabelUssClassName = MultiColumnHeaderColumnSortIndicator.ussClassName + "__index-label";

		// Token: 0x040015D8 RID: 5592
		private Label m_IndexLabel;
	}
}
