using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000175 RID: 373
	public interface IAsyncEndEditEventHandler<T> : IDisposable
	{
		// Token: 0x06000903 RID: 2307
		UniTask<T> OnEndEditAsync();
	}
}
