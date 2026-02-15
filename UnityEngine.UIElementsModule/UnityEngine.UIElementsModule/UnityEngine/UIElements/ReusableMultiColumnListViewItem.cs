using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000067 RID: 103
	internal class ReusableMultiColumnListViewItem : ReusableListViewItem
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x000116CE File Offset: 0x0000F8CE
		public override VisualElement rootElement
		{
			get
			{
				return base.bindableElement;
			}
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x000020EA File Offset: 0x000002EA
		public override void Init(VisualElement item)
		{
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00011CB0 File Offset: 0x0000FEB0
		public void Init(VisualElement container, Columns columns, bool usesAnimatedDrag)
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
					base.UpdateHierarchy(cellContainer, cellItem, usesAnimatedDrag);
					break;
				}
				i++;
			}
		}
	}
}
