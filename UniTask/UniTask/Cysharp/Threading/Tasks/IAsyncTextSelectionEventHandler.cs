using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000177 RID: 375
	public interface IAsyncTextSelectionEventHandler<T> : IDisposable
	{
		// Token: 0x06000905 RID: 2309
		UniTask<T> OnTextSelectionAsync();
	}
}
