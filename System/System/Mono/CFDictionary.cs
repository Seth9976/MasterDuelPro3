using System;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x0200000E RID: 14
	internal class CFDictionary : CFObject
	{
		// Token: 0x0600002D RID: 45 RVA: 0x0000248C File Offset: 0x0000068C
		static CFDictionary()
		{
			IntPtr intPtr = CFObject.dlopen("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation", 0);
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				CFDictionary.KeyCallbacks = CFObject.GetIndirect(intPtr, "kCFTypeDictionaryKeyCallBacks");
				CFDictionary.ValueCallbacks = CFObject.GetIndirect(intPtr, "kCFTypeDictionaryValueCallBacks");
			}
			finally
			{
				CFObject.dlclose(intPtr);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000022B4 File Offset: 0x000004B4
		public CFDictionary(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x0600002F RID: 47
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFDictionaryGetValue(IntPtr handle, IntPtr key);

		// Token: 0x06000030 RID: 48 RVA: 0x000024F0 File Offset: 0x000006F0
		public IntPtr GetValue(IntPtr key)
		{
			return CFDictionary.CFDictionaryGetValue(base.Handle, key);
		}

		// Token: 0x17000006 RID: 6
		public IntPtr this[IntPtr key]
		{
			get
			{
				return this.GetValue(key);
			}
		}

		// Token: 0x0400001A RID: 26
		private static readonly IntPtr KeyCallbacks;

		// Token: 0x0400001B RID: 27
		private static readonly IntPtr ValueCallbacks;
	}
}
