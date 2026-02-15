using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Unity.Hierarchy
{
	// Token: 0x02000006 RID: 6
	[DefaultMember("Item")]
	public readonly struct HierarchyNodeChildren
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000022E0 File Offset: 0x000004E0
		internal unsafe HierarchyNodeChildren(Hierarchy hierarchy, IntPtr nodeChildrenPtr)
		{
			bool flag = hierarchy == null;
			if (flag)
			{
				throw new ArgumentNullException("hierarchy");
			}
			bool flag2 = nodeChildrenPtr == IntPtr.Zero;
			if (flag2)
			{
				throw new ArgumentNullException("nodeChildrenPtr");
			}
			this.m_Hierarchy = hierarchy;
			this.m_Version = hierarchy.Version;
			ref HierarchyNodeChildrenAlloc alloc = ref *(HierarchyNodeChildrenAlloc*)(void*)nodeChildrenPtr;
			bool flag3 = (alloc.Reserved.FixedElementField & int.MinValue) == int.MinValue;
			if (flag3)
			{
				this.m_Ptr = alloc.Ptr;
				this.m_Count = alloc.Size;
			}
			else
			{
				this.m_Ptr = (HierarchyNode*)(void*)nodeChildrenPtr;
				this.m_Count = 0;
				for (int i = 0; i < 4; i++)
				{
					bool flag4 = (in this.m_Ptr[i]) != HierarchyNode.Null;
					if (!flag4)
					{
						break;
					}
					this.m_Count++;
				}
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000023CE File Offset: 0x000005CE
		public HierarchyNodeChildren.Enumerator GetEnumerator()
		{
			return new HierarchyNodeChildren.Enumerator(in this);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000023D8 File Offset: 0x000005D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ThrowIfVersionChanged()
		{
			bool flag = this.m_Version != this.m_Hierarchy.Version;
			if (flag)
			{
				throw new InvalidOperationException("Hierarchy was modified.");
			}
		}

		// Token: 0x0400000B RID: 11
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x0400000C RID: 12
		private unsafe readonly HierarchyNode* m_Ptr;

		// Token: 0x0400000D RID: 13
		private readonly int m_Version;

		// Token: 0x0400000E RID: 14
		private readonly int m_Count;

		// Token: 0x02000007 RID: 7
		public struct Enumerator
		{
			// Token: 0x0600000C RID: 12 RVA: 0x0000240B File Offset: 0x0000060B
			internal Enumerator(in HierarchyNodeChildren enumerable)
			{
				this.m_Enumerable = enumerable;
				this.m_Index = -1;
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600000D RID: 13 RVA: 0x00002424 File Offset: 0x00000624
			public readonly ref HierarchyNode Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					this.m_Enumerable.ThrowIfVersionChanged();
					return this.m_Enumerable.m_Ptr + this.m_Index;
				}
			}

			// Token: 0x0600000E RID: 14 RVA: 0x0000245C File Offset: 0x0000065C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_Enumerable.m_Count;
			}

			// Token: 0x0400000F RID: 15
			private readonly HierarchyNodeChildren m_Enumerable;

			// Token: 0x04000010 RID: 16
			private int m_Index;
		}
	}
}
