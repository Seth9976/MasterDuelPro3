using System;

namespace UnityEngine.Playables
{
	// Token: 0x0200030E RID: 782
	public enum PlayState
	{
		// Token: 0x04000824 RID: 2084
		Paused,
		// Token: 0x04000825 RID: 2085
		Playing,
		// Token: 0x04000826 RID: 2086
		[Obsolete("Delayed is obsolete; use a custom ScriptPlayable to implement this feature", false)]
		Delayed
	}
}
