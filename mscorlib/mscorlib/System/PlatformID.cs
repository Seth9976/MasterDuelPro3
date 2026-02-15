using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Identifies the operating system, or platform, supported by an assembly.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DC RID: 476
	[ComVisible(true)]
	[Serializable]
	public enum PlatformID
	{
		/// <summary>The operating system is Win32s. Win32s is a layer that runs on 16-bit versions of Windows to provide access to 32-bit applications.</summary>
		// Token: 0x04000774 RID: 1908
		Win32S,
		/// <summary>The operating system is Windows 95 or Windows 98.</summary>
		// Token: 0x04000775 RID: 1909
		Win32Windows,
		/// <summary>The operating system is Windows NT or later.</summary>
		// Token: 0x04000776 RID: 1910
		Win32NT,
		/// <summary>The operating system is Windows CE.</summary>
		// Token: 0x04000777 RID: 1911
		WinCE,
		/// <summary>The operating system is Unix.</summary>
		// Token: 0x04000778 RID: 1912
		Unix,
		/// <summary>The development platform is Xbox 360.</summary>
		// Token: 0x04000779 RID: 1913
		Xbox,
		/// <summary>The operating system is Macintosh.</summary>
		// Token: 0x0400077A RID: 1914
		MacOSX
	}
}
