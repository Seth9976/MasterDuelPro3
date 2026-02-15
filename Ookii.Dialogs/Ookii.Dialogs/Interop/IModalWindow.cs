using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x0200005C RID: 92
	[Guid("b4db1657-70d7-485e-8e3e-6fcb5a5c1802")]
	[InterfaceType(1)]
	[ComImport]
	internal interface IModalWindow
	{
		// Token: 0x0600025E RID: 606
		[MethodImpl(4224, MethodCodeType = 3)]
		int Show([In] IntPtr parent);
	}
}
