using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020001CA RID: 458
	[Flags]
	public enum InputUpdateType
	{
		// Token: 0x04000A52 RID: 2642
		None = 0,
		// Token: 0x04000A53 RID: 2643
		Dynamic = 1,
		// Token: 0x04000A54 RID: 2644
		Fixed = 2,
		// Token: 0x04000A55 RID: 2645
		BeforeRender = 4,
		// Token: 0x04000A56 RID: 2646
		Editor = 8,
		// Token: 0x04000A57 RID: 2647
		Manual = 16,
		// Token: 0x04000A58 RID: 2648
		Default = 11
	}
}
