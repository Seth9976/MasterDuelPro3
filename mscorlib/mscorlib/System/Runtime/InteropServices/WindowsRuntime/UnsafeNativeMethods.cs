using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000569 RID: 1385
	internal static class UnsafeNativeMethods
	{
		// Token: 0x06002A92 RID: 10898
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool RoOriginateLanguageException(int error, string message, IntPtr languageException);

		// Token: 0x06002A93 RID: 10899
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void RoReportUnhandledError(IRestrictedErrorInfo error);

		// Token: 0x06002A94 RID: 10900
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IRestrictedErrorInfo GetRestrictedErrorInfo();
	}
}
