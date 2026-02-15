using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace UnityEngine.AddressableAssets.ResourceLocators
{
	// Token: 0x02000058 RID: 88
	public interface IResourceLocator
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600022B RID: 555
		string LocatorId { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600022C RID: 556
		IEnumerable<object> Keys { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600022D RID: 557
		IEnumerable<IResourceLocation> AllLocations { get; }

		// Token: 0x0600022E RID: 558
		bool Locate(object key, Type type, out IList<IResourceLocation> locations);
	}
}
