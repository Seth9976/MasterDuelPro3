using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Unity.Hierarchy
{
	// Token: 0x02000004 RID: 4
	[DefaultMember("Item")]
	public readonly struct HierarchyFlattenedNodeChildren
	{
		// Token: 0x06000003 RID: 3 RVA: 0x0000205C File Offset: 0x0000025C
		internal HierarchyFlattenedNodeChildren(HierarchyFlattened hierarchyFlattened, in HierarchyNode node)
		{
			bool flag = hierarchyFlattened == null;
			if (flag)
			{
				throw new ArgumentNullException("hierarchyFlattened");
			}
			bool flag2 = (in node) == HierarchyNode.Null;
			if (flag2)
			{
				throw new ArgumentNullException("node");
			}
			bool flag3 = !hierarchyFlattened.Contains(in node);
			if (flag3)
			{
				throw new InvalidOperationException(string.Format("node {0}:{1} not found", node.Id, node.Version));
			}
			this.m_HierarchyFlattened = hierarchyFlattened;
			this.m_Node = node;
			this.m_Version = hierarchyFlattened.Version;
			this.m_Count = this.m_HierarchyFlattened.GetChildrenCount(in this.m_Node);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002101 File Offset: 0x00000301
		public HierarchyFlattenedNodeChildren.Enumerator GetEnumerator()
		{
			return new HierarchyFlattenedNodeChildren.Enumerator(this, this.m_Node);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002114 File Offset: 0x00000314
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ThrowIfVersionChanged()
		{
			bool flag = this.m_Version != this.m_HierarchyFlattened.Version;
			if (flag)
			{
				throw new InvalidOperationException("HierarchyFlattened was modified.");
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly HierarchyFlattened m_HierarchyFlattened;

		// Token: 0x04000002 RID: 2
		private readonly HierarchyNode m_Node;

		// Token: 0x04000003 RID: 3
		private readonly int m_Version;

		// Token: 0x04000004 RID: 4
		private readonly int m_Count;

		// Token: 0x02000005 RID: 5
		public struct Enumerator
		{
			// Token: 0x06000006 RID: 6 RVA: 0x00002147 File Offset: 0x00000347
			internal Enumerator(HierarchyFlattenedNodeChildren enumerable, HierarchyNode node)
			{
				this.m_Enumerable = enumerable;
				this.m_HierarchyFlattened = enumerable.m_HierarchyFlattened;
				this.m_Node = node;
				this.m_CurrentIndex = -1;
				this.m_ChildrenIndex = 0;
				this.m_ChildrenCount = 0;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000007 RID: 7 RVA: 0x0000217C File Offset: 0x0000037C
			public readonly ref HierarchyNode Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					this.m_Enumerable.ThrowIfVersionChanged();
					return HierarchyFlattenedNode.GetNodeByRef(this.m_HierarchyFlattened[this.m_CurrentIndex]);
				}
			}

			// Token: 0x06000008 RID: 8 RVA: 0x000021B0 File Offset: 0x000003B0
			public bool MoveNext()
			{
				this.m_Enumerable.ThrowIfVersionChanged();
				bool flag = this.m_CurrentIndex == -1;
				bool flag3;
				if (flag)
				{
					int index = this.m_HierarchyFlattened.IndexOf(in this.m_Node);
					bool flag2 = index == -1;
					if (flag2)
					{
						flag3 = false;
					}
					else
					{
						readonly ref HierarchyFlattenedNode flatNode = ref this.m_HierarchyFlattened[index];
						bool flag4 = (in flatNode) == HierarchyFlattenedNode.Null || flatNode.ChildrenCount <= 0;
						if (flag4)
						{
							flag3 = false;
						}
						else
						{
							bool flag5 = index + 1 >= this.m_HierarchyFlattened.Count;
							if (flag5)
							{
								flag3 = false;
							}
							else
							{
								this.m_CurrentIndex = index + 1;
								this.m_ChildrenIndex = 0;
								this.m_ChildrenCount = flatNode.ChildrenCount;
								flag3 = true;
							}
						}
					}
				}
				else
				{
					readonly ref HierarchyFlattenedNode currentFlatNode = ref this.m_HierarchyFlattened[this.m_CurrentIndex];
					bool flag6 = this.m_ChildrenIndex + 1 >= this.m_ChildrenCount || currentFlatNode.NextSiblingOffset <= 0;
					if (flag6)
					{
						flag3 = false;
					}
					else
					{
						this.m_CurrentIndex += currentFlatNode.NextSiblingOffset;
						this.m_ChildrenIndex++;
						flag3 = true;
					}
				}
				return flag3;
			}

			// Token: 0x04000005 RID: 5
			private readonly HierarchyFlattenedNodeChildren m_Enumerable;

			// Token: 0x04000006 RID: 6
			private readonly HierarchyFlattened m_HierarchyFlattened;

			// Token: 0x04000007 RID: 7
			private readonly HierarchyNode m_Node;

			// Token: 0x04000008 RID: 8
			private int m_CurrentIndex;

			// Token: 0x04000009 RID: 9
			private int m_ChildrenIndex;

			// Token: 0x0400000A RID: 10
			private int m_ChildrenCount;
		}
	}
}
