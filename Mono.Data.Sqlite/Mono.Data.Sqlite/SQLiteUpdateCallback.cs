using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000029 RID: 41
	// (Invoke) Token: 0x060001E2 RID: 482
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteUpdateCallback(IntPtr puser, int type, IntPtr database, IntPtr table, long rowid);
}
