using System;
using Microsoft.Win32.SafeHandles;

namespace Ookii.Dialogs
{
	// Token: 0x0200003E RID: 62
	internal class SafeGDIHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00005876 File Offset: 0x00003A76
		internal SafeGDIHandle()
			: base(true)
		{
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000058A3 File Offset: 0x00003AA3
		internal SafeGDIHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000058B8 File Offset: 0x00003AB8
		protected override bool ReleaseHandle()
		{
			return NativeMethods.DeleteObject(this.handle);
		}
	}
}
