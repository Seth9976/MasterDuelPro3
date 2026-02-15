using System;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000258 RID: 600
	internal class EmptyObserver<T> : IObserver<T>
	{
		// Token: 0x06000D65 RID: 3429 RVA: 0x000020BB File Offset: 0x000002BB
		private EmptyObserver()
		{
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x000030EE File Offset: 0x000012EE
		public void OnCompleted()
		{
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x000030EE File Offset: 0x000012EE
		public void OnError(Exception error)
		{
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000030EE File Offset: 0x000012EE
		public void OnNext(T value)
		{
		}

		// Token: 0x040006C4 RID: 1732
		public static readonly EmptyObserver<T> Instance = new EmptyObserver<T>();
	}
}
