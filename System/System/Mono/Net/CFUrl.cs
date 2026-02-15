using System;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x02000054 RID: 84
	internal class CFUrl : CFObject
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000022B4 File Offset: 0x000004B4
		public CFUrl(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x060000DD RID: 221
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFURLCreateWithString(IntPtr allocator, IntPtr str, IntPtr baseURL);

		// Token: 0x060000DE RID: 222 RVA: 0x000041C8 File Offset: 0x000023C8
		public static CFUrl Create(string absolute)
		{
			if (string.IsNullOrEmpty(absolute))
			{
				return null;
			}
			CFString cfstring = CFString.Create(absolute);
			IntPtr intPtr = CFUrl.CFURLCreateWithString(IntPtr.Zero, cfstring.Handle, IntPtr.Zero);
			cfstring.Dispose();
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new CFUrl(intPtr, true);
		}
	}
}
