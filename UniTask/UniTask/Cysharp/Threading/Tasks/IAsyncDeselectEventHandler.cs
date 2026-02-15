using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000178 RID: 376
	public interface IAsyncDeselectEventHandler<T> : IDisposable
	{
		// Token: 0x06000906 RID: 2310
		UniTask<T> OnDeselectAsync();
	}
}
