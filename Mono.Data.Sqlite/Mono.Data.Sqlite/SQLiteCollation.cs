using System;
using System.Runtime.InteropServices;

namespace Mono.Data.Sqlite
{
	// Token: 0x02000030 RID: 48
	// (Invoke) Token: 0x060001F0 RID: 496
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int SQLiteCollation(IntPtr puser, int len1, IntPtr pv1, int len2, IntPtr pv2);
}
