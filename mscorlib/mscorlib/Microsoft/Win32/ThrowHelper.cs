using System;
using System.Security;

namespace Microsoft.Win32
{
	// Token: 0x02000086 RID: 134
	internal static class ThrowHelper
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00010681 File Offset: 0x0000E881
		internal static void ThrowArgumentException(string msg)
		{
			throw new ArgumentException(msg);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00010689 File Offset: 0x0000E889
		internal static void ThrowArgumentException(string msg, string argument)
		{
			throw new ArgumentException(msg, argument);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x00010692 File Offset: 0x0000E892
		internal static void ThrowArgumentNullException(string argument)
		{
			throw new ArgumentNullException(argument);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0001069A File Offset: 0x0000E89A
		internal static void ThrowSecurityException(string msg)
		{
			throw new SecurityException(msg);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000106A2 File Offset: 0x0000E8A2
		internal static void ThrowUnauthorizedAccessException(string msg)
		{
			throw new UnauthorizedAccessException(msg);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000106AA File Offset: 0x0000E8AA
		internal static void ThrowObjectDisposedException(string objectName, string msg)
		{
			throw new ObjectDisposedException(objectName, msg);
		}
	}
}
