using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005A9 RID: 1449
	internal static class AsyncTaskCache
	{
		// Token: 0x06002B59 RID: 11097 RVA: 0x000AB9A4 File Offset: 0x000A9BA4
		private static Task<int>[] CreateInt32Tasks()
		{
			Task<int>[] array = new Task<int>[10];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = AsyncTaskCache.CreateCacheableTask<int>(i + -1);
			}
			return array;
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x000AB9D4 File Offset: 0x000A9BD4
		internal static Task<TResult> CreateCacheableTask<TResult>(TResult result)
		{
			return new Task<TResult>(false, result, (TaskCreationOptions)16384, default(CancellationToken));
		}

		// Token: 0x040015EE RID: 5614
		internal static readonly Task<bool> TrueTask = AsyncTaskCache.CreateCacheableTask<bool>(true);

		// Token: 0x040015EF RID: 5615
		internal static readonly Task<bool> FalseTask = AsyncTaskCache.CreateCacheableTask<bool>(false);

		// Token: 0x040015F0 RID: 5616
		internal static readonly Task<int>[] Int32Tasks = AsyncTaskCache.CreateInt32Tasks();
	}
}
