using System;

namespace DG.Tweening
{
	// Token: 0x02000015 RID: 21
	public enum LinkBehaviour
	{
		// Token: 0x04000062 RID: 98
		PauseOnDisable,
		// Token: 0x04000063 RID: 99
		PauseOnDisablePlayOnEnable,
		// Token: 0x04000064 RID: 100
		PauseOnDisableRestartOnEnable,
		// Token: 0x04000065 RID: 101
		PlayOnEnable,
		// Token: 0x04000066 RID: 102
		RestartOnEnable,
		// Token: 0x04000067 RID: 103
		KillOnDisable,
		// Token: 0x04000068 RID: 104
		KillOnDestroy,
		// Token: 0x04000069 RID: 105
		CompleteOnDisable,
		// Token: 0x0400006A RID: 106
		CompleteAndKillOnDisable,
		// Token: 0x0400006B RID: 107
		RewindOnDisable,
		// Token: 0x0400006C RID: 108
		RewindAndKillOnDisable
	}
}
