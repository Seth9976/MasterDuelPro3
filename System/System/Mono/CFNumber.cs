using System;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x0200000B RID: 11
	internal class CFNumber : CFObject
	{
		// Token: 0x06000020 RID: 32
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		[return: MarshalAs(UnmanagedType.I1)]
		private static extern bool CFNumberGetValue(IntPtr handle, IntPtr type, out int value);

		// Token: 0x06000021 RID: 33 RVA: 0x0000233C File Offset: 0x0000053C
		public static int AsInt32(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return 0;
			}
			int num;
			CFNumber.CFNumberGetValue(handle, (IntPtr)9, out num);
			return num;
		}
	}
}
