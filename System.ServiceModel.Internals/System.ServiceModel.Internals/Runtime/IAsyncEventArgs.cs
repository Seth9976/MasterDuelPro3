using System;

namespace System.Runtime
{
	// Token: 0x0200001A RID: 26
	internal interface IAsyncEventArgs
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000063 RID: 99
		object AsyncState { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000064 RID: 100
		Exception Exception { get; }
	}
}
