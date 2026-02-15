using System;
using System.Runtime.CompilerServices;

namespace Unity.Hierarchy
{
	// Token: 0x02000012 RID: 18
	public readonly struct HierarchyViewNodesEnumerable
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002ACF File Offset: 0x00000CCF
		internal HierarchyViewNodesEnumerable(HierarchyViewModel viewModel, HierarchyNodeFlags flags, HierarchyViewNodesEnumerable.Predicate predicate)
		{
			if (viewModel == null)
			{
				throw new ArgumentNullException("viewModel");
			}
			this.m_HierarchyViewModel = viewModel;
			if (predicate == null)
			{
				throw new ArgumentNullException("predicate");
			}
			this.m_Predicate = predicate;
			this.m_Flags = flags;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B05 File Offset: 0x00000D05
		public HierarchyViewNodesEnumerable.Enumerator GetEnumerator()
		{
			return new HierarchyViewNodesEnumerable.Enumerator(this);
		}

		// Token: 0x04000025 RID: 37
		private readonly HierarchyViewModel m_HierarchyViewModel;

		// Token: 0x04000026 RID: 38
		private readonly HierarchyViewNodesEnumerable.Predicate m_Predicate;

		// Token: 0x04000027 RID: 39
		private readonly HierarchyNodeFlags m_Flags;

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x06000046 RID: 70
		internal delegate bool Predicate(in HierarchyNode node, HierarchyNodeFlags flags);

		// Token: 0x02000014 RID: 20
		public struct Enumerator
		{
			// Token: 0x06000047 RID: 71 RVA: 0x00002B14 File Offset: 0x00000D14
			internal Enumerator(HierarchyViewNodesEnumerable enumerable)
			{
				this.m_HierarchyFlattened = enumerable.m_HierarchyViewModel.HierarchyFlattened;
				this.m_Predicate = enumerable.m_Predicate;
				this.m_Flags = enumerable.m_Flags;
				this.m_NodesPtr = this.m_HierarchyFlattened.NodesPtr;
				this.m_NodesCount = this.m_HierarchyFlattened.Count;
				this.m_Version = this.m_HierarchyFlattened.Version;
				this.m_Index = 0;
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000048 RID: 72 RVA: 0x00002B88 File Offset: 0x00000D88
			public unsafe readonly ref HierarchyNode Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					this.ThrowIfVersionChanged();
					return HierarchyFlattenedNode.GetNodeByRef(in this.m_NodesPtr[this.m_Index]);
				}
			}

			// Token: 0x06000049 RID: 73 RVA: 0x00002BBC File Offset: 0x00000DBC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public unsafe bool MoveNext()
			{
				this.ThrowIfVersionChanged();
				for (;;)
				{
					int num = this.m_Index + 1;
					this.m_Index = num;
					bool flag = num >= this.m_NodesCount;
					if (flag)
					{
						break;
					}
					bool flag2 = this.m_Predicate(HierarchyFlattenedNode.GetNodeByRef(in this.m_NodesPtr[this.m_Index]), this.m_Flags);
					if (flag2)
					{
						goto Block_2;
					}
				}
				return false;
				Block_2:
				return true;
			}

			// Token: 0x0600004A RID: 74 RVA: 0x00002C30 File Offset: 0x00000E30
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void ThrowIfVersionChanged()
			{
				bool flag = this.m_Version != this.m_HierarchyFlattened.Version;
				if (flag)
				{
					throw new InvalidOperationException("HierarchyFlattened was modified.");
				}
			}

			// Token: 0x04000028 RID: 40
			private readonly HierarchyFlattened m_HierarchyFlattened;

			// Token: 0x04000029 RID: 41
			private readonly HierarchyViewNodesEnumerable.Predicate m_Predicate;

			// Token: 0x0400002A RID: 42
			private readonly HierarchyNodeFlags m_Flags;

			// Token: 0x0400002B RID: 43
			private unsafe readonly HierarchyFlattenedNode* m_NodesPtr;

			// Token: 0x0400002C RID: 44
			private readonly int m_NodesCount;

			// Token: 0x0400002D RID: 45
			private readonly int m_Version;

			// Token: 0x0400002E RID: 46
			private int m_Index;
		}
	}
}
