using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000119 RID: 281
	public interface IResolvePromise<T>
	{
		// Token: 0x060006CE RID: 1742
		bool TrySetResult(T value);
	}
}
