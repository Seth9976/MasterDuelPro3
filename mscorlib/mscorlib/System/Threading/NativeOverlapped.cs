using System;

namespace System.Threading
{
	/// <summary>Provides an explicit layout that is visible from unmanaged code and that will have the same layout as the Win32 OVERLAPPED structure with additional reserved fields at the end.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000230 RID: 560
	public struct NativeOverlapped
	{
		/// <summary>Specifies a system-dependent status. Reserved for operating system use.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A32 RID: 2610
		public IntPtr InternalLow;

		/// <summary>Specifies the length of the data transferred. Reserved for operating system use.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A33 RID: 2611
		public IntPtr InternalHigh;

		/// <summary>Specifies a file position at which to start the transfer.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A34 RID: 2612
		public int OffsetLow;

		/// <summary>Specifies the high word of the byte offset at which to start the transfer.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A35 RID: 2613
		public int OffsetHigh;

		/// <summary>Specifies the handle to an event set to the signaled state when the operation is complete. The calling process must set this member either to zero or to a valid event handle before calling any overlapped functions.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000A36 RID: 2614
		public IntPtr EventHandle;
	}
}
