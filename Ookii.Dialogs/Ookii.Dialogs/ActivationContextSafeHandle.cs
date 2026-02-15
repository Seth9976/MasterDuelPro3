using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace Ookii.Dialogs
{
	// Token: 0x0200003D RID: 61
	internal class ActivationContextSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00005876 File Offset: 0x00003A76
		public ActivationContextSafeHandle()
			: base(true)
		{
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005884 File Offset: 0x00003A84
		[ReliabilityContract(3, 1)]
		protected override bool ReleaseHandle()
		{
			NativeMethods.ReleaseActCtx(this.handle);
			return true;
		}
	}
}
