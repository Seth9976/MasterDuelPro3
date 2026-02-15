using System;
using Microsoft.Win32.SafeHandles;

namespace Ookii.Dialogs
{
	// Token: 0x0200003F RID: 63
	internal class SafeDeviceHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600010D RID: 269 RVA: 0x00005876 File Offset: 0x00003A76
		internal SafeDeviceHandle()
			: base(true)
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000058A3 File Offset: 0x00003AA3
		internal SafeDeviceHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000058D8 File Offset: 0x00003AD8
		protected override bool ReleaseHandle()
		{
			return NativeMethods.DeleteDC(this.handle);
		}
	}
}
