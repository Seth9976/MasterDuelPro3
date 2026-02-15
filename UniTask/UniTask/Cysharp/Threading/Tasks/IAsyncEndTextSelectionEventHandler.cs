using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000176 RID: 374
	public interface IAsyncEndTextSelectionEventHandler<T> : IDisposable
	{
		// Token: 0x06000904 RID: 2308
		UniTask<T> OnEndTextSelectionAsync();
	}
}
