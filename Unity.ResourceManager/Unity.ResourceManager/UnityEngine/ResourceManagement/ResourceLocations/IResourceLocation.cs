using System;
using System.Collections.Generic;

namespace UnityEngine.ResourceManagement.ResourceLocations
{
	// Token: 0x02000068 RID: 104
	public interface IResourceLocation
	{
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000251 RID: 593
		string InternalId { get; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000252 RID: 594
		string ProviderId { get; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000253 RID: 595
		IList<IResourceLocation> Dependencies { get; }

		// Token: 0x06000254 RID: 596
		int Hash(Type resultType);

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000255 RID: 597
		int DependencyHashCode { get; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000256 RID: 598
		bool HasDependencies { get; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000257 RID: 599
		object Data { get; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000258 RID: 600
		string PrimaryKey { get; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000259 RID: 601
		Type ResourceType { get; }
	}
}
