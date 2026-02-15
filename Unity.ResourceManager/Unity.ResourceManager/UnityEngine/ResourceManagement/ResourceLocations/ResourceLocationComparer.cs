using System;
using System.Collections.Generic;

namespace UnityEngine.ResourceManagement.ResourceLocations
{
	// Token: 0x02000069 RID: 105
	public class ResourceLocationComparer : IEqualityComparer<IResourceLocation>
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00009CD0 File Offset: 0x00007ED0
		public bool Equals(IResourceLocation x, IResourceLocation y)
		{
			return this.GetHashCode(x) == this.GetHashCode(y);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009CE2 File Offset: 0x00007EE2
		public int GetHashCode(IResourceLocation obj)
		{
			return obj.InternalId.GetHashCode() * 31 + obj.ResourceType.GetHashCode();
		}
	}
}
