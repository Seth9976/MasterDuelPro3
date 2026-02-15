using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000068 RID: 104
	internal class ReusableMultiColumnTreeViewItem : ReusableTreeViewItem
	{
		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060003AA RID: 938 RVA: 0x000116CE File Offset: 0x0000F8CE
		public override VisualElement rootElement
		{
			get
			{
				return base.bindableElement;
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000020EA File Offset: 0x000002EA
		public override void Init(VisualElement item)
		{
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00011D4C File Offset: 0x0000FF4C
		public void Init(VisualElement container, Columns columns)
		{
			int i = 0;
			base.bindableElement = container;
			foreach (Column column in columns.visibleList)
			{
				bool flag = columns.IsPrimary(column);
				if (flag)
				{
					VisualElement cellContainer = container[i];
					VisualElement cellItem = cellContainer.GetProperty(MultiColumnController.bindableElementPropertyName) as VisualElement;
					base.InitExpandHierarchy(cellContainer, cellItem);
					break;
				}
				i++;
			}
		}
	}
}
