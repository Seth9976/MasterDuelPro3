using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200008B RID: 139
	internal sealed class SafePasswordHandle : SafeHandle
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x00010719 File Offset: 0x0000E919
		private IntPtr CreateHandle(string password)
		{
			return Marshal.StringToHGlobalUni(password);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00010721 File Offset: 0x0000E921
		private void FreeHandle()
		{
			Marshal.ZeroFreeGlobalAllocUnicode(this.handle);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0001072E File Offset: 0x0000E92E
		public SafePasswordHandle(string password)
			: base(IntPtr.Zero, true)
		{
			if (password != null)
			{
				base.SetHandle(this.CreateHandle(password));
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0001074C File Offset: 0x0000E94C
		protected override bool ReleaseHandle()
		{
			if (this.handle != IntPtr.Zero)
			{
				this.FreeHandle();
			}
			base.SetHandle((IntPtr)(-1));
			return true;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00010773 File Offset: 0x0000E973
		protected override void Dispose(bool disposing)
		{
			if (disposing && SafeHandleCache<SafePasswordHandle>.IsCachedInvalidHandle(this))
			{
				return;
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00010788 File Offset: 0x0000E988
		public override bool IsInvalid
		{
			get
			{
				return this.handle == (IntPtr)(-1);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0001079B File Offset: 0x0000E99B
		internal string Mono_DangerousGetString()
		{
			return Marshal.PtrToStringUni(base.DangerousGetHandle());
		}
	}
}
