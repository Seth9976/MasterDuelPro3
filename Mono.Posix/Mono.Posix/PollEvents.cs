using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000007 RID: 7
	[Map]
	[Flags]
	public enum PollEvents : short
	{
		// Token: 0x04000020 RID: 32
		POLLIN = 1,
		// Token: 0x04000021 RID: 33
		POLLPRI = 2,
		// Token: 0x04000022 RID: 34
		POLLOUT = 4,
		// Token: 0x04000023 RID: 35
		POLLERR = 8,
		// Token: 0x04000024 RID: 36
		POLLHUP = 16,
		// Token: 0x04000025 RID: 37
		POLLNVAL = 32,
		// Token: 0x04000026 RID: 38
		POLLRDNORM = 64,
		// Token: 0x04000027 RID: 39
		POLLRDBAND = 128,
		// Token: 0x04000028 RID: 40
		POLLWRNORM = 256,
		// Token: 0x04000029 RID: 41
		POLLWRBAND = 512
	}
}
