using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005D RID: 93
	public abstract class TreeViewController : BaseTreeViewController
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000EB19 File Offset: 0x0000CD19
		protected TreeView treeView
		{
			get
			{
				return base.view as TreeView;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000EB28 File Offset: 0x0000CD28
		protected override VisualElement MakeItem()
		{
			bool flag = this.treeView.makeItem == null;
			VisualElement visualElement;
			if (flag)
			{
				bool flag2 = this.treeView.bindItem != null;
				if (flag2)
				{
					throw new NotImplementedException("You must specify makeItem if bindItem is specified.");
				}
				visualElement = new Label();
			}
			else
			{
				visualElement = this.treeView.makeItem();
			}
			return visualElement;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000EB84 File Offset: 0x0000CD84
		protected override void BindItem(VisualElement element, int index)
		{
			bool flag = this.treeView.bindItem == null;
			if (flag)
			{
				bool isMakeItemSet = this.treeView.makeItem != null;
				bool flag2 = isMakeItemSet;
				if (flag2)
				{
					throw new NotImplementedException("You must specify bindItem if makeItem is specified.");
				}
				Label label = (Label)element;
				object item = this.GetItemForIndex(index);
				label.text = ((item != null) ? item.ToString() : null) ?? "null";
			}
			else
			{
				this.treeView.bindItem(element, index);
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000EC06 File Offset: 0x0000CE06
		protected override void UnbindItem(VisualElement element, int index)
		{
			Action<VisualElement, int> unbindItem = this.treeView.unbindItem;
			if (unbindItem != null)
			{
				unbindItem(element, index);
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000EC22 File Offset: 0x0000CE22
		protected override void DestroyItem(VisualElement element)
		{
			Action<VisualElement> destroyItem = this.treeView.destroyItem;
			if (destroyItem != null)
			{
				destroyItem(element);
			}
		}
	}
}
