using System;
using System.IO;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200008E RID: 142
	internal class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060002AE RID: 686 RVA: 0x000106E3 File Offset: 0x0000E8E3
		internal SafeFindHandle()
			: base(true)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00010807 File Offset: 0x0000EA07
		protected override bool ReleaseHandle()
		{
			return MonoIO.FindCloseFile(this.handle);
		}
	}
}
