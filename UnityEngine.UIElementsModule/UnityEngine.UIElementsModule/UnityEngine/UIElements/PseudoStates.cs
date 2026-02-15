using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004C6 RID: 1222
	[Flags]
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal enum PseudoStates
	{
		// Token: 0x04000F86 RID: 3974
		Active = 1,
		// Token: 0x04000F87 RID: 3975
		Hover = 2,
		// Token: 0x04000F88 RID: 3976
		Checked = 8,
		// Token: 0x04000F89 RID: 3977
		Disabled = 32,
		// Token: 0x04000F8A RID: 3978
		Focus = 64,
		// Token: 0x04000F8B RID: 3979
		Root = 128
	}
}
