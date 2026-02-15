using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000060 RID: 96
	[Flags]
	public enum InjectPlayerLoopTimings
	{
		// Token: 0x040000AF RID: 175
		All = 65535,
		// Token: 0x040000B0 RID: 176
		Standard = 30037,
		// Token: 0x040000B1 RID: 177
		Minimum = 8464,
		// Token: 0x040000B2 RID: 178
		Initialization = 1,
		// Token: 0x040000B3 RID: 179
		LastInitialization = 2,
		// Token: 0x040000B4 RID: 180
		EarlyUpdate = 4,
		// Token: 0x040000B5 RID: 181
		LastEarlyUpdate = 8,
		// Token: 0x040000B6 RID: 182
		FixedUpdate = 16,
		// Token: 0x040000B7 RID: 183
		LastFixedUpdate = 32,
		// Token: 0x040000B8 RID: 184
		PreUpdate = 64,
		// Token: 0x040000B9 RID: 185
		LastPreUpdate = 128,
		// Token: 0x040000BA RID: 186
		Update = 256,
		// Token: 0x040000BB RID: 187
		LastUpdate = 512,
		// Token: 0x040000BC RID: 188
		PreLateUpdate = 1024,
		// Token: 0x040000BD RID: 189
		LastPreLateUpdate = 2048,
		// Token: 0x040000BE RID: 190
		PostLateUpdate = 4096,
		// Token: 0x040000BF RID: 191
		LastPostLateUpdate = 8192,
		// Token: 0x040000C0 RID: 192
		TimeUpdate = 16384,
		// Token: 0x040000C1 RID: 193
		LastTimeUpdate = 32768
	}
}
