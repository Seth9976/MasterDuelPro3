using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200054D RID: 1357
	[Flags]
	internal enum RenderDataFlags
	{
		// Token: 0x04001298 RID: 4760
		IsInChain = 1,
		// Token: 0x04001299 RID: 4761
		IsGroupTransform = 2,
		// Token: 0x0400129A RID: 4762
		IsIgnoringDynamicColorHint = 4,
		// Token: 0x0400129B RID: 4763
		HasExtraData = 8,
		// Token: 0x0400129C RID: 4764
		HasExtraMeshes = 16
	}
}
