using System;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x0200000A RID: 10
	internal class CFArray : CFObject
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000022B4 File Offset: 0x000004B4
		public CFArray(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000022C0 File Offset: 0x000004C0
		static CFArray()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				CFArray.kCFTypeArrayCallbacks = CFObject.GetIndirect(intPtr, "kCFTypeArrayCallBacks");
			}
			finally
			{
				CFObject.dlclose(intPtr);
			}
		}

		// Token: 0x0600001C RID: 28
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFArrayGetCount(IntPtr handle);

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002314 File Offset: 0x00000514
		public int Count
		{
			get
			{
				return (int)CFArray.CFArrayGetCount(base.Handle);
			}
		}

		// Token: 0x0600001E RID: 30
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFArrayGetValueAtIndex(IntPtr handle, IntPtr index);

		// Token: 0x17000005 RID: 5
		public IntPtr this[int index]
		{
			get
			{
				return CFArray.CFArrayGetValueAtIndex(base.Handle, (IntPtr)index);
			}
		}

		// Token: 0x04000016 RID: 22
		private static readonly IntPtr kCFTypeArrayCallbacks;
	}
}
