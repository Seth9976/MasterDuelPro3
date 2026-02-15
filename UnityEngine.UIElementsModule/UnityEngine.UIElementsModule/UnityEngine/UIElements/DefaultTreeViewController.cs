using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;

namespace UnityEngine.UIElements
{
	// Token: 0x02000058 RID: 88
	public class DefaultTreeViewController<T> : TreeViewController
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		private TreeDataController<T> treeDataController
		{
			get
			{
				TreeDataController<T> treeDataController;
				if ((treeDataController = this.m_TreeDataController) == null)
				{
					treeDataController = (this.m_TreeDataController = new TreeDataController<T>());
				}
				return treeDataController;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000E1A6 File Offset: 0x0000C3A6
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000E320 File Offset: 0x0000C520
		public override IList itemsSource
		{
			get
			{
				return base.itemsSource;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.SetRootItems(null);
				}
				else
				{
					IList<TreeViewItemData<T>> dataList = value as IList<TreeViewItemData<T>>;
					bool flag2 = dataList != null;
					if (flag2)
					{
						this.SetRootItems(dataList);
					}
					else
					{
						Debug.LogError(string.Format("Type does not match this tree view controller's data type ({0}).", typeof(T)));
					}
				}
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000E37C File Offset: 0x0000C57C
		public void SetRootItems(IList<TreeViewItemData<T>> items)
		{
			bool flag = items == base.itemsSource;
			if (!flag)
			{
				bool isCreated = this.m_Hierarchy.IsCreated;
				if (isCreated)
				{
					base.ClearIdToNodeDictionary();
					this.treeDataController.ClearNodeToDataDictionary();
					base.hierarchy = new Hierarchy();
				}
				bool flag2 = items != null;
				if (flag2)
				{
					this.treeDataController.ConvertTreeViewItemDataToHierarchy(items, (HierarchyNode node) => base.CreateNode(in node), delegate(int id, HierarchyNode node)
					{
						base.UpdateIdToNodeDictionary(id, in node, true);
					});
					base.UpdateHierarchy();
					bool flag3 = base.IsViewDataKeyEnabled();
					if (flag3)
					{
						base.OnViewDataReadyUpdateNodes();
					}
				}
				base.SetHierarchyViewModelWithoutNotify(this.m_HierarchyViewModel);
				base.RaiseItemsSourceChanged();
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000E428 File Offset: 0x0000C628
		public override object GetItemForIndex(int index)
		{
			return this.treeDataController.GetDataForNode(base.GetHierarchyNodeByIndex(index));
		}

		// Token: 0x040001B3 RID: 435
		private TreeDataController<T> m_TreeDataController;
	}
}
