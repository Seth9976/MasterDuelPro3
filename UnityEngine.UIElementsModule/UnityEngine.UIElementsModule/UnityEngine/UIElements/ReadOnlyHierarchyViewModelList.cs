using System;
using System.Collections;
using Unity.Hierarchy;

namespace UnityEngine.UIElements
{
	// Token: 0x02000050 RID: 80
	internal class ReadOnlyHierarchyViewModelList : IList, ICollection, IEnumerable
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000C45B File Offset: 0x0000A65B
		public bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000C45B File Offset: 0x0000A65B
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000C45E File Offset: 0x0000A65E
		public int Count
		{
			get
			{
				return this.m_HierarchyViewModel.Count;
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000C46C File Offset: 0x0000A66C
		public bool Contains(object value)
		{
			bool flag;
			if (value is HierarchyNode)
			{
				HierarchyNode node = (HierarchyNode)value;
				flag = this.m_HierarchyViewModel.Contains(in node);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000C498 File Offset: 0x0000A698
		public int IndexOf(object value)
		{
			int num;
			if (value is HierarchyNode)
			{
				HierarchyNode node = (HierarchyNode)value;
				num = this.m_HierarchyViewModel.IndexOf(in node);
			}
			else
			{
				num = BaseTreeView.invalidId;
			}
			return num;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000C4CA File Offset: 0x0000A6CA
		public ReadOnlyHierarchyViewModelList(HierarchyViewModel viewModel)
		{
			this.m_HierarchyViewModel = viewModel;
		}

		// Token: 0x17000054 RID: 84
		public unsafe object this[int index]
		{
			get
			{
				return *this.m_HierarchyViewModel[index];
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		public unsafe void CopyTo(Array array, int index)
		{
			for (int i = index; i < this.m_HierarchyViewModel.Count; i++)
			{
				array.SetValue(*this.m_HierarchyViewModel[i], i - index);
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000C544 File Offset: 0x0000A744
		public IEnumerator GetEnumerator()
		{
			return new ReadOnlyHierarchyViewModelList.Enumerator(this.m_HierarchyViewModel);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000C4F3 File Offset: 0x0000A6F3
		public bool IsSynchronized
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000C4F3 File Offset: 0x0000A6F3
		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000C4F3 File Offset: 0x0000A6F3
		public int Add(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000C4F3 File Offset: 0x0000A6F3
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000C4F3 File Offset: 0x0000A6F3
		public void Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000C4F3 File Offset: 0x0000A6F3
		public void Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C4F3 File Offset: 0x0000A6F3
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000183 RID: 387
		private readonly HierarchyViewModel m_HierarchyViewModel;

		// Token: 0x02000051 RID: 81
		private struct Enumerator : IEnumerator
		{
			// Token: 0x0600028B RID: 651 RVA: 0x0000C556 File Offset: 0x0000A756
			public Enumerator(HierarchyViewModel hierarchyViewModel)
			{
				this.m_HierarchyViewModel = hierarchyViewModel;
				this.m_Enumerator = hierarchyViewModel.GetEnumerator();
			}

			// Token: 0x17000057 RID: 87
			// (get) Token: 0x0600028C RID: 652 RVA: 0x0000C56C File Offset: 0x0000A76C
			public unsafe object Current
			{
				get
				{
					return *this.m_Enumerator.Current;
				}
			}

			// Token: 0x0600028D RID: 653 RVA: 0x0000C583 File Offset: 0x0000A783
			public bool MoveNext()
			{
				return this.m_Enumerator.MoveNext();
			}

			// Token: 0x0600028E RID: 654 RVA: 0x0000C590 File Offset: 0x0000A790
			public void Reset()
			{
				this.m_Enumerator = this.m_HierarchyViewModel.GetEnumerator();
			}

			// Token: 0x04000184 RID: 388
			private readonly HierarchyViewModel m_HierarchyViewModel;

			// Token: 0x04000185 RID: 389
			private HierarchyViewModel.Enumerator m_Enumerator;
		}
	}
}
