using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005B RID: 91
	public abstract class MultiColumnTreeViewController : BaseTreeViewController
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000E823 File Offset: 0x0000CA23
		public MultiColumnController columnController
		{
			get
			{
				return this.m_ColumnController;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000E82B File Offset: 0x0000CA2B
		protected MultiColumnTreeViewController(Columns columns, SortColumnDescriptions sortDescriptions, List<SortColumnDescription> sortedColumns)
		{
			this.m_ColumnController = new MultiColumnController(columns, sortDescriptions, sortedColumns);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000E843 File Offset: 0x0000CA43
		internal override void PreRefresh()
		{
			base.PreRefresh();
			this.m_ColumnController.SortIfNeeded();
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000E85C File Offset: 0x0000CA5C
		internal override void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			ReusableMultiColumnTreeViewItem treeItem = reusableItem as ReusableMultiColumnTreeViewItem;
			bool flag = treeItem != null;
			if (flag)
			{
				treeItem.Init(this.MakeItem(), this.m_ColumnController.header.columns);
				base.PostInitRegistration(treeItem);
			}
			else
			{
				base.InvokeMakeItem(reusableItem);
			}
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000E8AC File Offset: 0x0000CAAC
		internal override void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			int sortedIndex = this.m_ColumnController.GetSortedIndex(index);
			base.InvokeBindItem(reusableItem, sortedIndex);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000E8D0 File Offset: 0x0000CAD0
		internal override void InvokeUnbindItem(ReusableCollectionItem reusableItem, int index)
		{
			int sortedIndex = this.m_ColumnController.GetSortedIndex(index);
			base.InvokeUnbindItem(reusableItem, sortedIndex);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000E8F4 File Offset: 0x0000CAF4
		protected override VisualElement MakeItem()
		{
			return this.m_ColumnController.MakeItem();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000E911 File Offset: 0x0000CB11
		protected override void BindItem(VisualElement element, int index)
		{
			this.m_ColumnController.BindItem<object>(element, index, this.GetItemForIndex(index));
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000E929 File Offset: 0x0000CB29
		protected override void UnbindItem(VisualElement element, int index)
		{
			this.m_ColumnController.UnbindItem(element, index);
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000E93A File Offset: 0x0000CB3A
		protected override void DestroyItem(VisualElement element)
		{
			this.m_ColumnController.DestroyItem(element);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000E94A File Offset: 0x0000CB4A
		protected override void PrepareView()
		{
			this.m_ColumnController.PrepareView(base.view);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000E95F File Offset: 0x0000CB5F
		public override void Dispose()
		{
			this.m_ColumnController.Dispose();
			this.m_ColumnController = null;
			base.Dispose();
		}

		// Token: 0x040001B5 RID: 437
		private MultiColumnController m_ColumnController;
	}
}
