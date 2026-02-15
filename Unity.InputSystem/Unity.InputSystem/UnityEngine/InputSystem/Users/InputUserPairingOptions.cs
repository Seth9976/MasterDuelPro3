using System;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x02000111 RID: 273
	[Flags]
	public enum InputUserPairingOptions
	{
		// Token: 0x04000631 RID: 1585
		None = 0,
		// Token: 0x04000632 RID: 1586
		ForcePlatformUserAccountSelection = 1,
		// Token: 0x04000633 RID: 1587
		ForceNoPlatformUserAccountSelection = 2,
		// Token: 0x04000634 RID: 1588
		UnpairCurrentDevicesFromUser = 8
	}
}
