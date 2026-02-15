using System;
using System.Collections.Generic;
using UnityEngine.UIElements.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005A RID: 90
	public class MultiColumnListViewController : BaseListViewController
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000E5A5 File Offset: 0x0000C7A5
		public MultiColumnController columnController
		{
			get
			{
				return this.m_ColumnController;
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E5AD File Offset: 0x0000C7AD
		public MultiColumnListViewController(Columns columns, SortColumnDescriptions sortDescriptions, List<SortColumnDescription> sortedColumns)
		{
			this.m_ColumnController = new MultiColumnController(columns, sortDescriptions, sortedColumns);
			base.itemsSourceSizeChanged += this.SortIfNeeded;
			base.itemsSourceChanged += this.SortIfNeeded;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000E5EB File Offset: 0x0000C7EB
		internal override void PreRefresh()
		{
			base.PreRefresh();
			this.m_ColumnController.SortIfNeeded();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000E604 File Offset: 0x0000C804
		private void SortIfNeeded()
		{
			this.m_ColumnController.UpdateDragger();
			bool flag = this.m_ColumnController.sortingMode == ColumnSortingMode.Default;
			if (flag)
			{
				base.view.RefreshItems();
			}
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000E640 File Offset: 0x0000C840
		internal override void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			ReusableMultiColumnListViewItem listItem = reusableItem as ReusableMultiColumnListViewItem;
			bool flag = listItem != null;
			if (flag)
			{
				listItem.Init(this.MakeItem(), this.m_ColumnController.header.columns, base.baseListView.reorderMode == ListViewReorderMode.Animated);
				base.PostInitRegistration(listItem);
			}
			else
			{
				base.InvokeMakeItem(reusableItem);
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		internal override void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			int sortedIndex = this.m_ColumnController.GetSortedIndex(index);
			base.InvokeBindItem(reusableItem, sortedIndex);
			ReusableListViewItem listItem = reusableItem as ReusableListViewItem;
			bool flag = listItem != null;
			if (flag)
			{
				bool isSorted = this.m_ColumnController.header.sortingEnabled && this.m_ColumnController.header.sortedColumnReadonly.Count > 0;
				listItem.SetDragHandleEnabled(!isSorted);
			}
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000E710 File Offset: 0x0000C910
		internal override void InvokeUnbindItem(ReusableCollectionItem reusableItem, int index)
		{
			int sortedIndex = this.m_ColumnController.GetSortedIndex(index);
			base.InvokeUnbindItem(reusableItem, sortedIndex);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000E734 File Offset: 0x0000C934
		protected override VisualElement MakeItem()
		{
			return this.m_ColumnController.MakeItem();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000E751 File Offset: 0x0000C951
		protected override void BindItem(VisualElement element, int index)
		{
			this.m_ColumnController.BindItem<object>(element, index, this.GetItemForIndex(index));
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000E769 File Offset: 0x0000C969
		protected override void UnbindItem(VisualElement element, int index)
		{
			this.m_ColumnController.UnbindItem(element, index);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000E77A File Offset: 0x0000C97A
		protected override void DestroyItem(VisualElement element)
		{
			this.m_ColumnController.DestroyItem(element);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000E78A File Offset: 0x0000C98A
		protected override void PrepareView()
		{
			this.m_ColumnController.PrepareView(base.view);
			base.baseListView.reorderModeChanged += this.UpdateReorderClassList;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000E7B7 File Offset: 0x0000C9B7
		public override void Dispose()
		{
			base.baseListView.reorderModeChanged -= this.UpdateReorderClassList;
			this.m_ColumnController.Dispose();
			this.m_ColumnController = null;
			base.Dispose();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000E7EC File Offset: 0x0000C9EC
		private void UpdateReorderClassList()
		{
			this.m_ColumnController.header.EnableInClassList(MultiColumnCollectionHeader.reorderableUssClassName, base.baseListView.reorderable && base.baseListView.reorderMode == ListViewReorderMode.Animated);
		}

		// Token: 0x040001B4 RID: 436
		private MultiColumnController m_ColumnController;
	}
}
