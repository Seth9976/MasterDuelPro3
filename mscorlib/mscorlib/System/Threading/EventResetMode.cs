using System;

namespace System.Threading
{
	/// <summary>Indicates whether an <see cref="T:System.Threading.EventWaitHandle" /> is reset automatically or manually after receiving a signal.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200021A RID: 538
	public enum EventResetMode
	{
		/// <summary>When signaled, the <see cref="T:System.Threading.EventWaitHandle" /> resets automatically after releasing a single thread. If no threads are waiting, the <see cref="T:System.Threading.EventWaitHandle" /> remains signaled until a thread blocks, and resets after releasing the thread.</summary>
		// Token: 0x04000A02 RID: 2562
		AutoReset,
		/// <summary>When signaled, the <see cref="T:System.Threading.EventWaitHandle" /> releases all waiting threads and remains signaled until it is manually reset.</summary>
		// Token: 0x04000A03 RID: 2563
		ManualReset
	}
}
