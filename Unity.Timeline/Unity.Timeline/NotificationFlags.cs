using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000056 RID: 86
	[Flags]
	[Serializable]
	public enum NotificationFlags : short
	{
		// Token: 0x04000147 RID: 327
		TriggerInEditMode = 1,
		// Token: 0x04000148 RID: 328
		Retroactive = 2,
		// Token: 0x04000149 RID: 329
		TriggerOnce = 4
	}
}
