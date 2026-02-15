using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000AC RID: 172
	// (Invoke) Token: 0x060002F1 RID: 753
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int MonoBtlsServerNameCallback();
}
