using System;
using UnityEngine.Bindings;

namespace Unity.Hierarchy
{
	// Token: 0x02000022 RID: 34
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyPropertyId.h")]
	internal readonly struct HierarchyPropertyId : IEquatable<HierarchyPropertyId>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000038BD File Offset: 0x00001ABD
		public static readonly ref HierarchyPropertyId Null
		{
			get
			{
				return ref HierarchyPropertyId.s_Null;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x000038C4 File Offset: 0x00001AC4
		public int Id
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000038CC File Offset: 0x00001ACC
		public HierarchyPropertyId()
		{
			this.m_Id = 0;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000038D6 File Offset: 0x00001AD6
		public static bool operator ==(in HierarchyPropertyId lhs, in HierarchyPropertyId rhs)
		{
			return lhs.Id == rhs.Id;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000038E6 File Offset: 0x00001AE6
		public bool Equals(HierarchyPropertyId other)
		{
			return other.Id == this.Id;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000038F7 File Offset: 0x00001AF7
		public override string ToString()
		{
			return string.Format("{0}({1})", "HierarchyPropertyId", ((in this) == HierarchyPropertyId.Null) ? "Null" : this.Id);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00003928 File Offset: 0x00001B28
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is HierarchyPropertyId)
			{
				HierarchyPropertyId node = (HierarchyPropertyId)obj;
				flag = this.Equals(node);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003950 File Offset: 0x00001B50
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

		// Token: 0x04000054 RID: 84
		private static readonly HierarchyPropertyId s_Null;

		// Token: 0x04000055 RID: 85
		private readonly int m_Id;
	}
}
