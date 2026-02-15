using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000AB RID: 171
	// (Invoke) Token: 0x060002EF RID: 751
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int MonoBtlsSelectCallback(string[] acceptableIssuers);
}
