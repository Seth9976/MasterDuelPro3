using System;

namespace System.Runtime
{
	// Token: 0x0200000A RID: 10
	internal class AsyncEventArgs<TArgument, TResult> : AsyncEventArgs<TArgument>
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002399 File Offset: 0x00000599
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000023A1 File Offset: 0x000005A1
		public TResult Result { get; set; }
	}
}
