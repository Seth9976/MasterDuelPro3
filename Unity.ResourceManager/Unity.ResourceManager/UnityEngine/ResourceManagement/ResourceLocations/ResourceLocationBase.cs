using System;
using System.Collections.Generic;

namespace UnityEngine.ResourceManagement.ResourceLocations
{
	// Token: 0x0200006A RID: 106
	public class ResourceLocationBase : IResourceLocation
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00009CFE File Offset: 0x00007EFE
		public string InternalId
		{
			get
			{
				return this.m_Id;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00009D06 File Offset: 0x00007F06
		public string ProviderId
		{
			get
			{
				return this.m_ProviderId;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00009D0E File Offset: 0x00007F0E
		public IList<IResourceLocation> Dependencies
		{
			get
			{
				return this.m_Dependencies;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00009D16 File Offset: 0x00007F16
		public bool HasDependencies
		{
			get
			{
				return this.m_Dependencies != null && this.m_Dependencies.Count > 0;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00009D30 File Offset: 0x00007F30
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00009D38 File Offset: 0x00007F38
		public object Data
		{
			get
			{
				return this.m_Data;
			}
			set
			{
				this.m_Data = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00009D41 File Offset: 0x00007F41
		// (set) Token: 0x06000264 RID: 612 RVA: 0x00009D49 File Offset: 0x00007F49
		public string PrimaryKey
		{
			get
			{
				return this.m_PrimaryKey;
			}
			set
			{
				this.m_PrimaryKey = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000265 RID: 613 RVA: 0x00009D52 File Offset: 0x00007F52
		public int DependencyHashCode
		{
			get
			{
				return this.m_DependencyHashCode;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00009D5A File Offset: 0x00007F5A
		public Type ResourceType
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009D62 File Offset: 0x00007F62
		public int Hash(Type t)
		{
			return (this.m_HashCode * 31 + t.GetHashCode()) * 31 + this.DependencyHashCode;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009CFE File Offset: 0x00007EFE
		public override string ToString()
		{
			return this.m_Id;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009D80 File Offset: 0x00007F80
		public ResourceLocationBase(string name, string id, string providerId, Type t, params IResourceLocation[] dependencies)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}
			if (string.IsNullOrEmpty(providerId))
			{
				throw new ArgumentNullException("providerId");
			}
			this.m_PrimaryKey = name;
			this.m_HashCode = (name.GetHashCode() * 31 + id.GetHashCode()) * 31 + providerId.GetHashCode();
			this.m_Name = name;
			this.m_Id = id;
			this.m_ProviderId = providerId;
			this.m_Dependencies = new List<IResourceLocation>(dependencies);
			this.m_Type = ((t == null) ? typeof(object) : t);
			this.ComputeDependencyHash();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009E28 File Offset: 0x00008028
		public void ComputeDependencyHash()
		{
			this.m_DependencyHashCode = ((this.m_Dependencies.Count > 0) ? 17 : 0);
			foreach (IResourceLocation d in this.m_Dependencies)
			{
				this.m_DependencyHashCode = this.m_DependencyHashCode * 31 + d.Hash(typeof(object));
			}
		}

		// Token: 0x0400010D RID: 269
		private string m_Name;

		// Token: 0x0400010E RID: 270
		private string m_Id;

		// Token: 0x0400010F RID: 271
		private string m_ProviderId;

		// Token: 0x04000110 RID: 272
		private object m_Data;

		// Token: 0x04000111 RID: 273
		private int m_DependencyHashCode;

		// Token: 0x04000112 RID: 274
		private int m_HashCode;

		// Token: 0x04000113 RID: 275
		private Type m_Type;

		// Token: 0x04000114 RID: 276
		private List<IResourceLocation> m_Dependencies;

		// Token: 0x04000115 RID: 277
		private string m_PrimaryKey;
	}
}
