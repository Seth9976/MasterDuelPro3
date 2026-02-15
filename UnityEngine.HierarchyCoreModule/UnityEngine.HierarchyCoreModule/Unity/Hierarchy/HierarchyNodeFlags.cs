using System;
using UnityEngine.Bindings;

namespace Unity.Hierarchy
{
	// Token: 0x0200001F RID: 31
	[Flags]
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyNodeFlags.h")]
	public enum HierarchyNodeFlags : uint
	{
		// Token: 0x0400004B RID: 75
		None = 0U,
		// Token: 0x0400004C RID: 76
		Expanded = 1U,
		// Token: 0x0400004D RID: 77
		Selected = 2U,
		// Token: 0x0400004E RID: 78
		Cut = 4U,
		// Token: 0x0400004F RID: 79
		Hidden = 8U
	}
}
