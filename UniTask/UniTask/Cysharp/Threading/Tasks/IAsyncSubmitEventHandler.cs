using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200017A RID: 378
	public interface IAsyncSubmitEventHandler<T> : IDisposable
	{
		// Token: 0x06000908 RID: 2312
		UniTask<T> OnSubmitAsync();
	}
}
