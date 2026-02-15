using System;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	public enum FocusType
	{
		// Token: 0x0400005A RID: 90
		[Obsolete("FocusType.Native now behaves the same as FocusType.Passive in all OS cases. (UnityUpgradable) -> Passive", false)]
		Native,
		// Token: 0x0400005B RID: 91
		Keyboard,
		// Token: 0x0400005C RID: 92
		Passive
	}
}
