using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200018D RID: 397
	public static class AsyncOperationAwaitableExtensions
	{
		// Token: 0x06000FD9 RID: 4057 RVA: 0x000217E4 File Offset: 0x0001F9E4
		[ExcludeFromDocs]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Awaitable.Awaiter GetAwaiter(this AsyncOperation op)
		{
			return Awaitable.FromAsyncOperation(op, default(CancellationToken)).GetAwaiter();
		}
	}
}
