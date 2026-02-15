using System;
using UnityEngine.Bindings;

namespace Unity.Hierarchy
{
	// Token: 0x02000024 RID: 36
	[NativeHeader("Modules/HierarchyCore/Public/HierarchySearch.h")]
	public enum HierarchySearchFilterOperator
	{
		// Token: 0x0400005C RID: 92
		Equal,
		// Token: 0x0400005D RID: 93
		Contains,
		// Token: 0x0400005E RID: 94
		Greater,
		// Token: 0x0400005F RID: 95
		GreaterOrEqual,
		// Token: 0x04000060 RID: 96
		Lesser,
		// Token: 0x04000061 RID: 97
		LesserOrEqual,
		// Token: 0x04000062 RID: 98
		NotEqual,
		// Token: 0x04000063 RID: 99
		Not
	}
}
