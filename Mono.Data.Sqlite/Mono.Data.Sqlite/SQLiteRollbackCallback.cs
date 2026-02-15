using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200002B RID: 43
	// (Invoke) Token: 0x060001E6 RID: 486
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteRollbackCallback(IntPtr puser);
}
