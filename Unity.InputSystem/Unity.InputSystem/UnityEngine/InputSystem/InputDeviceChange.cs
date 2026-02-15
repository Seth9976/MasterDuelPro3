using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200008B RID: 139
	public enum InputDeviceChange
	{
		// Token: 0x04000329 RID: 809
		Added,
		// Token: 0x0400032A RID: 810
		Removed,
		// Token: 0x0400032B RID: 811
		Disconnected,
		// Token: 0x0400032C RID: 812
		Reconnected,
		// Token: 0x0400032D RID: 813
		Enabled,
		// Token: 0x0400032E RID: 814
		Disabled,
		// Token: 0x0400032F RID: 815
		UsageChanged,
		// Token: 0x04000330 RID: 816
		ConfigurationChanged,
		// Token: 0x04000331 RID: 817
		SoftReset,
		// Token: 0x04000332 RID: 818
		HardReset,
		// Token: 0x04000333 RID: 819
		[Obsolete("Destroyed enum has been deprecated.")]
		Destroyed
	}
}
