using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000431 RID: 1073
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum StyleSelectorType
	{
		// Token: 0x04000D95 RID: 3477
		Unknown,
		// Token: 0x04000D96 RID: 3478
		Wildcard,
		// Token: 0x04000D97 RID: 3479
		Type,
		// Token: 0x04000D98 RID: 3480
		Class,
		// Token: 0x04000D99 RID: 3481
		PseudoClass,
		// Token: 0x04000D9A RID: 3482
		RecursivePseudoClass,
		// Token: 0x04000D9B RID: 3483
		ID,
		// Token: 0x04000D9C RID: 3484
		Predicate
	}
}
