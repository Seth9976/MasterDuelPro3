using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;

namespace UnityEngine.UIElements
{
	// Token: 0x02000057 RID: 87
	public class DefaultMultiColumnTreeViewController<T> : MultiColumnTreeViewController
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000E174 File Offset: 0x0000C374
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

		// Token: 0x060002FC RID: 764 RVA: 0x0000E199 File Offset: 0x0000C399
		public DefaultMultiColumnTreeViewController(Columns columns, SortColumnDescriptions sortDescriptions, List<SortColumnDescription> sortedColumns)
			: base(columns, sortDescriptions, sortedColumns)
		{
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000E1A6 File Offset: 0x0000C3A6
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
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

		// Token: 0x060002FF RID: 767 RVA: 0x0000E20C File Offset: 0x0000C40C
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

		// Token: 0x06000300 RID: 768 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		public override object GetItemForIndex(int index)
		{
			return this.treeDataController.GetDataForNode(base.GetHierarchyNodeByIndex(index));
		}

		// Token: 0x040001B2 RID: 434
		private TreeDataController<T> m_TreeDataController;
	}
}
