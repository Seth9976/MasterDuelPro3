using System;

namespace System.Buffers
{
	// Token: 0x0200077C RID: 1916
	// (Invoke) Token: 0x06003CD6 RID: 15574
	public delegate void SpanAction<T, in TArg>(Span<T> span, TArg arg);
}
