using System;
using UnityEngine.Bindings;

namespace Unity.Hierarchy
{
	// Token: 0x02000023 RID: 35
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyPropertyStorageType.h")]
	public enum HierarchyPropertyStorageType
	{
		// Token: 0x04000057 RID: 87
		Sparse,
		// Token: 0x04000058 RID: 88
		Dense,
		// Token: 0x04000059 RID: 89
		Blob,
		// Token: 0x0400005A RID: 90
		Default = 1
	}
}
