using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000088 RID: 136
	internal static class SafeHandleCache<T> where T : SafeHandle
	{
		// Token: 0x0600029B RID: 667 RVA: 0x000106CF File Offset: 0x0000E8CF
		internal static bool IsCachedInvalidHandle(SafeHandle handle)
		{
			return handle == Volatile.Read<T>(ref SafeHandleCache<T>.s_invalidHandle);
		}

		// Token: 0x04000270 RID: 624
		private static T s_invalidHandle;
	}
}
