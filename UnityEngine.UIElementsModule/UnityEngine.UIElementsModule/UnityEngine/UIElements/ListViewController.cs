using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000059 RID: 89
	public class ListViewController : BaseListViewController
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000E45A File Offset: 0x0000C65A
		protected ListView listView
		{
			get
			{
				return base.view as ListView;
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000E468 File Offset: 0x0000C668
		protected override VisualElement MakeItem()
		{
			bool flag = this.listView.makeItem == null;
			VisualElement visualElement;
			if (flag)
			{
				bool flag2 = this.listView.bindItem != null;
				if (flag2)
				{
					throw new NotImplementedException("You must specify makeItem if bindItem is specified.");
				}
				visualElement = new Label();
			}
			else
			{
				visualElement = this.listView.makeItem();
			}
			return visualElement;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000E4C4 File Offset: 0x0000C6C4
		protected override void BindItem(VisualElement element, int index)
		{
			bool flag = this.listView.bindItem == null;
			if (flag)
			{
				bool isMakeItemSet = this.listView.makeItem != null;
				bool flag2 = this.listView.autoAssignSource && isMakeItemSet;
				if (!flag2)
				{
					bool flag3 = isMakeItemSet;
					if (flag3)
					{
						throw new NotImplementedException("You must specify bindItem if makeItem is specified.");
					}
					Label label = (Label)element;
					object item = this.listView.itemsSource[index];
					label.text = ((item != null) ? item.ToString() : null) ?? "null";
				}
			}
			else
			{
				this.listView.bindItem(element, index);
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E565 File Offset: 0x0000C765
		protected override void UnbindItem(VisualElement element, int index)
		{
			Action<VisualElement, int> unbindItem = this.listView.unbindItem;
			if (unbindItem != null)
			{
				unbindItem(element, index);
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000E581 File Offset: 0x0000C781
		protected override void DestroyItem(VisualElement element)
		{
			Action<VisualElement> destroyItem = this.listView.destroyItem;
			if (destroyItem != null)
			{
				destroyItem(element);
			}
		}
	}
}
