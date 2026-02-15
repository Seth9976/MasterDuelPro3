using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200011A RID: 282
	public interface IRejectPromise
	{
		// Token: 0x060006CF RID: 1743
		bool TrySetException(Exception exception);
	}
}
