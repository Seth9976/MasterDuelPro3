using System;

namespace System.Threading
{
	// Token: 0x0200022D RID: 557
	internal interface IDeferredDisposable
	{
		// Token: 0x060014BD RID: 5309
		void OnFinalRelease(bool disposed);
	}
}
