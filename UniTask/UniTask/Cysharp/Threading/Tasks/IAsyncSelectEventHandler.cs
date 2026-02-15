using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000179 RID: 377
	public interface IAsyncSelectEventHandler<T> : IDisposable
	{
		// Token: 0x06000907 RID: 2311
		UniTask<T> OnSelectAsync();
	}
}
