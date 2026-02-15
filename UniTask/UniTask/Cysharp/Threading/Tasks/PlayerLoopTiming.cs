using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200005F RID: 95
	public enum PlayerLoopTiming
	{
		// Token: 0x0400009E RID: 158
		Initialization,
		// Token: 0x0400009F RID: 159
		LastInitialization,
		// Token: 0x040000A0 RID: 160
		EarlyUpdate,
		// Token: 0x040000A1 RID: 161
		LastEarlyUpdate,
		// Token: 0x040000A2 RID: 162
		FixedUpdate,
		// Token: 0x040000A3 RID: 163
		LastFixedUpdate,
		// Token: 0x040000A4 RID: 164
		PreUpdate,
		// Token: 0x040000A5 RID: 165
		LastPreUpdate,
		// Token: 0x040000A6 RID: 166
		Update,
		// Token: 0x040000A7 RID: 167
		LastUpdate,
		// Token: 0x040000A8 RID: 168
		PreLateUpdate,
		// Token: 0x040000A9 RID: 169
		LastPreLateUpdate,
		// Token: 0x040000AA RID: 170
		PostLateUpdate,
		// Token: 0x040000AB RID: 171
		LastPostLateUpdate,
		// Token: 0x040000AC RID: 172
		TimeUpdate,
		// Token: 0x040000AD RID: 173
		LastTimeUpdate
	}
}
