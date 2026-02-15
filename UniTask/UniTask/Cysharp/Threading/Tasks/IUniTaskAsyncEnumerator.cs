using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000031 RID: 49
	public interface IUniTaskAsyncEnumerator<out T> : IUniTaskAsyncDisposable
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600010B RID: 267
		T Current { get; }

		// Token: 0x0600010C RID: 268
		UniTask<bool> MoveNextAsync();
	}
}
