using System;
using System.Runtime.ExceptionServices;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000259 RID: 601
	internal class ThrowObserver<T> : IObserver<T>
	{
		// Token: 0x06000D6A RID: 3434 RVA: 0x000020BB File Offset: 0x000002BB
		private ThrowObserver()
		{
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000030EE File Offset: 0x000012EE
		public void OnCompleted()
		{
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x0002EB6A File Offset: 0x0002CD6A
		public void OnError(Exception error)
		{
			ExceptionDispatchInfo.Capture(error).Throw();
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000030EE File Offset: 0x000012EE
		public void OnNext(T value)
		{
		}

		// Token: 0x040006C5 RID: 1733
		public static readonly ThrowObserver<T> Instance = new ThrowObserver<T>();
	}
}
