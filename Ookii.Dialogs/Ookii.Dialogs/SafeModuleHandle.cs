using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs
{
	// Token: 0x02000040 RID: 64
	internal class SafeModuleHandle : SafeHandle
	{
		// Token: 0x06000110 RID: 272 RVA: 0x000058F5 File Offset: 0x00003AF5
		public SafeModuleHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00005908 File Offset: 0x00003B08
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000592C File Offset: 0x00003B2C
		[ReliabilityContract(3, 1)]
		protected override bool ReleaseHandle()
		{
			return NativeMethods.FreeLibrary(this.handle);
		}
	}
}
