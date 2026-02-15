using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000AA RID: 170
	// (Invoke) Token: 0x060002ED RID: 749
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	internal delegate int MonoBtlsVerifyCallback(MonoBtlsX509StoreCtx ctx);
}
