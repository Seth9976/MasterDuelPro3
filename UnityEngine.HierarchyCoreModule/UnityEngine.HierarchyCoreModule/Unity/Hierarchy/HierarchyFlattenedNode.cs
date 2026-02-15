using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace Unity.Hierarchy
{
	// Token: 0x0200001D RID: 29
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyFlattenedNode.h")]
	public readonly struct HierarchyFlattenedNode : IEquatable<HierarchyFlattenedNode>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000035E2 File Offset: 0x000017E2
		public static readonly ref HierarchyFlattenedNode Null
		{
			get
			{
				return ref HierarchyFlattenedNode.s_Null;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000035E9 File Offset: 0x000017E9
		public HierarchyNode Node
		{
			get
			{
				return this.m_Node;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000035F1 File Offset: 0x000017F1
		public int NextSiblingOffset
		{
			get
			{
				return this.m_NextSiblingOffset;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000035F9 File Offset: 0x000017F9
		public int ChildrenCount
		{
			get
			{
				return this.m_ChildrenCount;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003604 File Offset: 0x00001804
		[ExcludeFromDocs]
		public static bool operator ==(in HierarchyFlattenedNode lhs, in HierarchyFlattenedNode rhs)
		{
			HierarchyNode node = lhs.Node;
			HierarchyNode node2 = rhs.Node;
			return (in node) == (in node2);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003628 File Offset: 0x00001828
		[ExcludeFromDocs]
		public bool Equals(HierarchyFlattenedNode other)
		{
			HierarchyNode node = other.Node;
			HierarchyNode node2 = this.Node;
			return (in node) == (in node2);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003650 File Offset: 0x00001850
		[ExcludeFromDocs]
		public override string ToString()
		{
			return "HierarchyFlattenedNode(" + (((in this) == HierarchyFlattenedNode.Null) ? "Null" : string.Format("{0}:{1}", this.Node.Id, this.Node.Version)) + ")";
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000036B0 File Offset: 0x000018B0
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is HierarchyFlattenedNode)
			{
				HierarchyFlattenedNode node = (HierarchyFlattenedNode)obj;
				flag = this.Equals(node);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000036D8 File Offset: 0x000018D8
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return this.Node.GetHashCode();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000036F9 File Offset: 0x000018F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static readonly ref HierarchyNode GetNodeByRef(in HierarchyFlattenedNode hierarchyFlattenedNode)
		{
			return ref hierarchyFlattenedNode.m_Node;
		}

		// Token: 0x04000040 RID: 64
		private static readonly HierarchyFlattenedNode s_Null;

		// Token: 0x04000041 RID: 65
		private readonly HierarchyNode m_Node;

		// Token: 0x04000042 RID: 66
		private readonly HierarchyNodeType m_Type;

		// Token: 0x04000043 RID: 67
		private readonly int m_ParentOffset;

		// Token: 0x04000044 RID: 68
		private readonly int m_NextSiblingOffset;

		// Token: 0x04000045 RID: 69
		private readonly int m_ChildrenCount;

		// Token: 0x04000046 RID: 70
		private readonly int m_Depth;
	}
}
