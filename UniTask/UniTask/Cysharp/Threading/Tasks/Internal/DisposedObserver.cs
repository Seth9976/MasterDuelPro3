using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200025A RID: 602
	internal class DisposedObserver<T> : IObserver<T>
	{
		// Token: 0x06000D6F RID: 3439 RVA: 0x000020BB File Offset: 0x000002BB
		private DisposedObserver()
		{
		}

		// Token: 0x06000D70 RID: 3440 RVA: 0x0002EB83 File Offset: 0x0002CD83
		public void OnCompleted()
		{
			throw new ObjectDisposedException("");
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0002EB83 File Offset: 0x0002CD83
		public void OnError(Exception error)
		{
			throw new ObjectDisposedException("");
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0002EB83 File Offset: 0x0002CD83
		public void OnNext(T value)
		{
			throw new ObjectDisposedException("");
		}

		// Token: 0x040006C6 RID: 1734
		public static readonly DisposedObserver<T> Instance = new DisposedObserver<T>();
	}
}
