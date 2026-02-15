using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000089 RID: 137
	internal sealed class SafeLibraryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600029C RID: 668 RVA: 0x000106E3 File Offset: 0x0000E8E3
		internal SafeLibraryHandle()
			: base(true)
		{
		}

		// Token: 0x0600029D RID: 669 RVA: 0x000106EC File Offset: 0x0000E8EC
		protected override bool ReleaseHandle()
		{
			return Interop.Kernel32.FreeLibrary(this.handle);
		}
	}
}
