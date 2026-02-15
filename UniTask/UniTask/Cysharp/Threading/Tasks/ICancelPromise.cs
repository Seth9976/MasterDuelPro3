using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200011B RID: 283
	public interface ICancelPromise
	{
		// Token: 0x060006D0 RID: 1744
		bool TrySetCanceled(CancellationToken cancellationToken = default(CancellationToken));
	}
}
