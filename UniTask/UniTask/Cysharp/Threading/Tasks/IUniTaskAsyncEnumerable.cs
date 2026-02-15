using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000030 RID: 48
	public interface IUniTaskAsyncEnumerable<out T>
	{
		// Token: 0x0600010A RID: 266
		IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken));
	}
}
