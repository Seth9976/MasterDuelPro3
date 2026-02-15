using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime.Interop
{
	// Token: 0x0200002B RID: 43
	internal sealed class SafeEventLogWriteHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000045B7 File Offset: 0x000027B7
		private SafeEventLogWriteHandle()
			: base(true)
		{
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000045C0 File Offset: 0x000027C0
		public static SafeEventLogWriteHandle RegisterEventSource(string uncServerName, string sourceName)
		{
			SafeEventLogWriteHandle safeEventLogWriteHandle = UnsafeNativeMethods.RegisterEventSource(uncServerName, sourceName);
			Marshal.GetLastWin32Error();
			bool isInvalid = safeEventLogWriteHandle.IsInvalid;
			return safeEventLogWriteHandle;
		}

		// Token: 0x060000C0 RID: 192
		[DllImport("advapi32", SetLastError = true)]
		private static extern bool DeregisterEventSource(IntPtr hEventLog);

		// Token: 0x060000C1 RID: 193 RVA: 0x000045D6 File Offset: 0x000027D6
		protected override bool ReleaseHandle()
		{
			return SafeEventLogWriteHandle.DeregisterEventSource(this.handle);
		}
	}
}
