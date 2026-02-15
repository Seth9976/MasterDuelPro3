using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Internal;
using UnityEngine.Pool;

namespace UnityEngine
{
	// Token: 0x0200018E RID: 398
	[AsyncMethodBuilder(typeof(Awaitable.AwaitableAsyncMethodBuilder<>))]
	public class Awaitable<T>
	{
		// Token: 0x06000FDA RID: 4058 RVA: 0x0002180A File Offset: 0x0001FA0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ContinueWith(Action continuation)
		{
			this._awaitable.SetContinuation(continuation);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0002181C File Offset: 0x0001FA1C
		private T GetResult()
		{
			T result;
			try
			{
				this._awaitable.PropagateExceptionAndRelease();
				result = this._result;
			}
			finally
			{
				this._awaitable = null;
				this._result = default(T);
				Awaitable<T>._pool.Value.Release(this);
			}
			return result;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x000205EB File Offset: 0x0001E7EB
		private Awaitable()
		{
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00021878 File Offset: 0x0001FA78
		[ExcludeFromDocs]
		public Awaitable<T>.Awaiter GetAwaiter()
		{
			return new Awaitable<T>.Awaiter(this);
		}

		// Token: 0x04000642 RID: 1602
		private static readonly ThreadLocal<ObjectPool<Awaitable<T>>> _pool = new ThreadLocal<ObjectPool<Awaitable<T>>>(() => new ObjectPool<Awaitable<T>>(() => new Awaitable<T>(), null, null, null, false, 10, 10000));

		// Token: 0x04000643 RID: 1603
		private Awaitable _awaitable;

		// Token: 0x04000644 RID: 1604
		private T _result;

		// Token: 0x0200018F RID: 399
		[ExcludeFromDocs]
		public struct Awaiter : INotifyCompletion
		{
			// Token: 0x06000FDF RID: 4063 RVA: 0x000218AC File Offset: 0x0001FAAC
			public Awaiter(Awaitable<T> coroutine)
			{
				this._coroutine = coroutine;
			}

			// Token: 0x06000FE0 RID: 4064 RVA: 0x000218B6 File Offset: 0x0001FAB6
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void OnCompleted(Action continuation)
			{
				this._coroutine.ContinueWith(continuation);
			}

			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x000218C6 File Offset: 0x0001FAC6
			public bool IsCompleted
			{
				get
				{
					return this._coroutine._awaitable.IsCompleted;
				}
			}

			// Token: 0x06000FE2 RID: 4066 RVA: 0x000218D8 File Offset: 0x0001FAD8
			public T GetResult()
			{
				return this._coroutine.GetResult();
			}

			// Token: 0x04000645 RID: 1605
			private readonly Awaitable<T> _coroutine;
		}
	}
}
