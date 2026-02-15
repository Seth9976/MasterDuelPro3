using System;
using System.Runtime.InteropServices;

namespace Mono.Net
{
	// Token: 0x02000055 RID: 85
	internal class CFRunLoop : CFObject
	{
		// Token: 0x060000DF RID: 223
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRunLoopAddSource(IntPtr rl, IntPtr source, IntPtr mode);

		// Token: 0x060000E0 RID: 224
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRunLoopRemoveSource(IntPtr rl, IntPtr source, IntPtr mode);

		// Token: 0x060000E1 RID: 225
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern int CFRunLoopRunInMode(IntPtr mode, double seconds, bool returnAfterSourceHandled);

		// Token: 0x060000E2 RID: 226
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern IntPtr CFRunLoopGetCurrent();

		// Token: 0x060000E3 RID: 227
		[DllImport("/System/Library/Frameworks/CoreFoundation.framework/CoreFoundation")]
		private static extern void CFRunLoopStop(IntPtr rl);

		// Token: 0x060000E4 RID: 228 RVA: 0x000022B4 File Offset: 0x000004B4
		public CFRunLoop(IntPtr handle, bool own)
			: base(handle, own)
		{
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004218 File Offset: 0x00002418
		public static CFRunLoop CurrentRunLoop
		{
			get
			{
				return new CFRunLoop(CFRunLoop.CFRunLoopGetCurrent(), false);
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004225 File Offset: 0x00002425
		public void AddSource(IntPtr source, CFString mode)
		{
			CFRunLoop.CFRunLoopAddSource(base.Handle, source, mode.Handle);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004239 File Offset: 0x00002439
		public void RemoveSource(IntPtr source, CFString mode)
		{
			CFRunLoop.CFRunLoopRemoveSource(base.Handle, source, mode.Handle);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000424D File Offset: 0x0000244D
		public int RunInMode(CFString mode, double seconds, bool returnAfterSourceHandled)
		{
			return CFRunLoop.CFRunLoopRunInMode(mode.Handle, seconds, returnAfterSourceHandled);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000425C File Offset: 0x0000245C
		public void Stop()
		{
			CFRunLoop.CFRunLoopStop(base.Handle);
		}
	}
}
