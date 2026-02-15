using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000174 RID: 372
	public interface IAsyncValueChangedEventHandler<T> : IDisposable
	{
		// Token: 0x06000902 RID: 2306
		UniTask<T> OnValueChangedAsync();
	}
}
