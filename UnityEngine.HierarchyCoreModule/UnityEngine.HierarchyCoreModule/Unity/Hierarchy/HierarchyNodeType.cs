using System;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace Unity.Hierarchy
{
	// Token: 0x02000020 RID: 32
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyNodeType.h")]
	public readonly struct HierarchyNodeType : IEquatable<HierarchyNodeType>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003805 File Offset: 0x00001A05
		public static readonly ref HierarchyNodeType Null
		{
			get
			{
				return ref HierarchyNodeType.s_Null;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000380C File Offset: 0x00001A0C
		public int Id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003814 File Offset: 0x00001A14
		[ExcludeFromDocs]
		public static bool operator ==(in HierarchyNodeType lhs, in HierarchyNodeType rhs)
		{
			return lhs.Id == rhs.Id;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003824 File Offset: 0x00001A24
		[ExcludeFromDocs]
		public bool Equals(HierarchyNodeType other)
		{
			return other.Id == this.Id;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003835 File Offset: 0x00001A35
		[ExcludeFromDocs]
		public override string ToString()
		{
			return string.Format("{0}({1})", "HierarchyNodeType", ((in this) == HierarchyNodeType.Null) ? "Null" : this.Id);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003868 File Offset: 0x00001A68
		[ExcludeFromDocs]
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is HierarchyNodeType)
			{
				HierarchyNodeType node = (HierarchyNodeType)obj;
				flag = this.Equals(node);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003890 File Offset: 0x00001A90
		[ExcludeFromDocs]
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x04000050 RID: 80
		private static readonly HierarchyNodeType s_Null;

		// Token: 0x04000051 RID: 81
		private readonly int m_Id;
	}
}
