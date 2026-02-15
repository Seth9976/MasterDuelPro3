using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x0200002F RID: 47
	// (Invoke) Token: 0x060001EE RID: 494
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate void SQLiteFinalCallback(IntPtr context);
}
