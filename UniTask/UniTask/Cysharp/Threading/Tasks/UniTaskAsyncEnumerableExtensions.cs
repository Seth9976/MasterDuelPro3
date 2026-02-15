using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000035 RID: 53
	public static class UniTaskAsyncEnumerableExtensions
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00004417 File Offset: 0x00002617
		public static UniTaskCancelableAsyncEnumerable<T> WithCancellation<T>(this IUniTaskAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			return new UniTaskCancelableAsyncEnumerable<T>(source, cancellationToken);
		}
	}
}
