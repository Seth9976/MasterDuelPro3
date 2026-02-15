using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace Unity.Hierarchy
{
	// Token: 0x0200001E RID: 30
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyNode.h")]
	public readonly struct HierarchyNode : IEquatable<HierarchyNode>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00003701 File Offset: 0x00001901
		public static readonly ref HierarchyNode Null
		{
			get
			{
				return ref HierarchyNode.s_Null;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00003708 File Offset: 0x00001908
		public int Id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00003710 File Offset: 0x00001910
		public int Version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003718 File Offset: 0x00001918
		public HierarchyNode()
		{
			this.m_Id = 0;
			this.m_Version = 0;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003729 File Offset: 0x00001929
		[ExcludeFromDocs]
		public static bool operator ==(in HierarchyNode lhs, in HierarchyNode rhs)
		{
			return lhs.Id == rhs.Id && lhs.Version == rhs.Version;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000374A File Offset: 0x0000194A
		[ExcludeFromDocs]
		public static bool operator !=(in HierarchyNode lhs, in HierarchyNode rhs)
		{
			return !((in lhs) == (in rhs));
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003756 File Offset: 0x00001956
		[ExcludeFromDocs]
		public bool Equals(HierarchyNode other)
		{
			return other.Id == this.Id && other.Version == this.Version;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000377C File Offset: 0x0000197C
		[ExcludeFromDocs]
		public override string ToString()
		{
			return "HierarchyNode(" + (((in this) == HierarchyNode.Null) ? "Null" : string.Format("{0}:{1}", this.Id, this.Version)) + ")";
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000037CC File Offset: 0x000019CC
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is HierarchyNode)
			{
				HierarchyNode node = (HierarchyNode)obj;
				flag = this.Equals(node);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000037F2 File Offset: 0x000019F2
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return HashCode.Combine<int, int>(this.Id, this.Version);
		}

		// Token: 0x04000047 RID: 71
		private static readonly HierarchyNode s_Null;

		// Token: 0x04000048 RID: 72
		private readonly int m_Id;

		// Token: 0x04000049 RID: 73
		private readonly int m_Version;
	}
}
