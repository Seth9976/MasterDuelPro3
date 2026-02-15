using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x020000E1 RID: 225
	public sealed class SafeProcessHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x0000EC70 File Offset: 0x0000CE70
		internal SafeProcessHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000EC80 File Offset: 0x0000CE80
		public SafeProcessHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000EC90 File Offset: 0x0000CE90
		protected override bool ReleaseHandle()
		{
			return NativeMethods.CloseProcess(this.handle);
		}

		// Token: 0x04000375 RID: 885
		internal static SafeProcessHandle InvalidHandle = new SafeProcessHandle(IntPtr.Zero);
	}
}
