using System;
using System.Collections.Generic;

namespace UnityEngine.ResourceManagement.ResourceLocations
{
	// Token: 0x0200006B RID: 107
	internal class LocationWrapper : IResourceLocation
	{
		// Token: 0x0600026B RID: 619 RVA: 0x00009EB0 File Offset: 0x000080B0
		public LocationWrapper(IResourceLocation location)
		{
			this.m_InternalLocation = location;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00009EBF File Offset: 0x000080BF
		public string InternalId
		{
			get
			{
				return this.m_InternalLocation.InternalId;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00009ECC File Offset: 0x000080CC
		public string ProviderId
		{
			get
			{
				return this.m_InternalLocation.ProviderId;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00009ED9 File Offset: 0x000080D9
		public IList<IResourceLocation> Dependencies
		{
			get
			{
				return this.m_InternalLocation.Dependencies;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00009EE6 File Offset: 0x000080E6
		public int DependencyHashCode
		{
			get
			{
				return this.m_InternalLocation.DependencyHashCode;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00009EF3 File Offset: 0x000080F3
		public bool HasDependencies
		{
			get
			{
				return this.m_InternalLocation.HasDependencies;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00009F00 File Offset: 0x00008100
		public object Data
		{
			get
			{
				return this.m_InternalLocation.Data;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000272 RID: 626 RVA: 0x00009F0D File Offset: 0x0000810D
		public string PrimaryKey
		{
			get
			{
				return this.m_InternalLocation.PrimaryKey;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00009F1A File Offset: 0x0000811A
		public Type ResourceType
		{
			get
			{
				return this.m_InternalLocation.ResourceType;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009F27 File Offset: 0x00008127
		public int Hash(Type resultType)
		{
			return this.m_InternalLocation.Hash(resultType);
		}

		// Token: 0x04000116 RID: 278
		private IResourceLocation m_InternalLocation;
	}
}
