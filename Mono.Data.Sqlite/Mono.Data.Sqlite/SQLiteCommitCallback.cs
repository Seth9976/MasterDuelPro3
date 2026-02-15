using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200002A RID: 42
	// (Invoke) Token: 0x060001E4 RID: 484
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int SQLiteCommitCallback(IntPtr puser);
}
