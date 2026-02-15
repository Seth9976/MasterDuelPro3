using System;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks.CompilerServices
{
	// Token: 0x02000261 RID: 609
	internal interface IStateMachineRunnerPromise<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000D99 RID: 3481
		Action MoveNext { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000D9A RID: 3482
		UniTask<T> Task { get; }

		// Token: 0x06000D9B RID: 3483
		void SetResult(T result);

		// Token: 0x06000D9C RID: 3484
		void SetException(Exception exception);
	}
}
