using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using Unity.Hierarchy;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x02000056 RID: 86
	public abstract class CollectionViewController : IDisposable
	{
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060002DE RID: 734 RVA: 0x0000DE80 File Offset: 0x0000C080
		// (remove) Token: 0x060002DF RID: 735 RVA: 0x0000DEB8 File Offset: 0x0000C0B8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action itemsSourceChanged;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060002E0 RID: 736 RVA: 0x0000DEF0 File Offset: 0x0000C0F0
		// (remove) Token: 0x060002E1 RID: 737 RVA: 0x0000DF28 File Offset: 0x0000C128
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, int> itemIndexChanged;

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000DF5D File Offset: 0x0000C15D
		// (set) Token: 0x060002E3 RID: 739 RVA: 0x0000DF68 File Offset: 0x0000C168
		public virtual IList itemsSource
		{
			get
			{
				return this.m_ItemsSource;
			}
			set
			{
				bool flag = this.m_ItemsSource == value;
				if (!flag)
				{
					this.m_ItemsSource = value;
					bool flag2 = this.m_View.GetProperty("__unity-collection-view-internal-binding") == null;
					if (flag2)
					{
						this.m_View.RefreshItems();
					}
					this.RaiseItemsSourceChanged();
				}
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000DFBD File Offset: 0x0000C1BD
		private protected void SetHierarchyViewModelWithoutNotify(HierarchyViewModel source)
		{
			this.m_ItemsSource = new ReadOnlyHierarchyViewModelList(source);
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000DFCC File Offset: 0x0000C1CC
		protected BaseVerticalCollectionView view
		{
			get
			{
				return this.m_View;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		public void SetView(BaseVerticalCollectionView collectionView)
		{
			this.m_View = collectionView;
			this.PrepareView();
			Assert.IsNotNull<BaseVerticalCollectionView>(this.m_View, "View must not be null.");
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000020EA File Offset: 0x000002EA
		protected virtual void PrepareView()
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000DFF6 File Offset: 0x0000C1F6
		public virtual void Dispose()
		{
			this.itemsSourceChanged = null;
			this.itemIndexChanged = null;
			this.m_View = null;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000E010 File Offset: 0x0000C210
		public virtual int GetItemsCount()
		{
			IList itemsSource = this.m_ItemsSource;
			return (itemsSource != null) ? itemsSource.Count : 0;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000E034 File Offset: 0x0000C234
		internal virtual int GetItemsMinCount()
		{
			return this.GetItemsCount();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000E03C File Offset: 0x0000C23C
		public virtual int GetIndexForId(int id)
		{
			return id;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000E050 File Offset: 0x0000C250
		public virtual int GetIdForIndex(int index)
		{
			return index;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000E064 File Offset: 0x0000C264
		public virtual object GetItemForIndex(int index)
		{
			bool flag = this.m_ItemsSource == null;
			object obj;
			if (flag)
			{
				obj = null;
			}
			else
			{
				bool flag2 = index < 0 || index >= this.m_ItemsSource.Count;
				if (flag2)
				{
					obj = null;
				}
				else
				{
					obj = this.m_ItemsSource[index];
				}
			}
			return obj;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000E0B3 File Offset: 0x0000C2B3
		internal virtual void InvokeMakeItem(ReusableCollectionItem reusableItem)
		{
			reusableItem.Init(this.MakeItem());
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000020EA File Offset: 0x000002EA
		internal virtual void SetBindingContext(ReusableCollectionItem reusableItem, int index)
		{
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		internal virtual void InvokeBindItem(ReusableCollectionItem reusableItem, int index)
		{
			this.BindItem(reusableItem.bindableElement, index);
			this.SetBindingContext(reusableItem, index);
			reusableItem.SetSelected(this.m_View.selectedIndices.Contains(index));
			reusableItem.rootElement.pseudoStates &= ~PseudoStates.Hover;
			reusableItem.index = index;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000E11E File Offset: 0x0000C31E
		internal virtual void InvokeUnbindItem(ReusableCollectionItem reusableItem, int index)
		{
			this.UnbindItem(reusableItem.bindableElement, index);
			reusableItem.index = -1;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000E137 File Offset: 0x0000C337
		internal virtual void InvokeDestroyItem(ReusableCollectionItem reusableItem)
		{
			this.DestroyItem(reusableItem.bindableElement);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000020EA File Offset: 0x000002EA
		internal virtual void PreRefresh()
		{
		}

		// Token: 0x060002F4 RID: 756
		protected abstract VisualElement MakeItem();

		// Token: 0x060002F5 RID: 757
		protected abstract void BindItem(VisualElement element, int index);

		// Token: 0x060002F6 RID: 758
		protected abstract void UnbindItem(VisualElement element, int index);

		// Token: 0x060002F7 RID: 759
		protected abstract void DestroyItem(VisualElement element);

		// Token: 0x060002F8 RID: 760 RVA: 0x0000E147 File Offset: 0x0000C347
		protected void RaiseItemsSourceChanged()
		{
			Action action = this.itemsSourceChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000E15C File Offset: 0x0000C35C
		protected void RaiseItemIndexChanged(int srcIndex, int dstIndex)
		{
			Action<int, int> action = this.itemIndexChanged;
			if (action != null)
			{
				action(srcIndex, dstIndex);
			}
		}

		// Token: 0x040001AE RID: 430
		private BaseVerticalCollectionView m_View;

		// Token: 0x040001AF RID: 431
		private IList m_ItemsSource;
	}
}
