using System;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000034 RID: 52
	public interface IConnectableUniTaskAsyncEnumerable<out T> : IUniTaskAsyncEnumerable<T>
	{
		// Token: 0x06000111 RID: 273
		IDisposable Connect();
	}
}
