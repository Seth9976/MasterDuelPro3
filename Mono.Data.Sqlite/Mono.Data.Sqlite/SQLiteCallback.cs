using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200002E RID: 46
	// (Invoke) Token: 0x060001EC RID: 492
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteCallback(IntPtr context, int nArgs, IntPtr argsptr);
}
