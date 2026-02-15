using System;
using System.Threading;
using System.Threading.Tasks.Sources;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200016E RID: 366
	public static class AsyncInstantiateOperationExtensions
	{
		// Token: 0x060008DB RID: 2267 RVA: 0x00028252 File Offset: 0x00026452
		public static UniTask<global::UnityEngine.Object[]> WithCancellation<T>(this AsyncInstantiateOperation asyncOperation, CancellationToken cancellationToken)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002825E File Offset: 0x0002645E
		public static UniTask<global::UnityEngine.Object[]> WithCancellation<T>(this AsyncInstantiateOperation asyncOperation, CancellationToken cancellationToken, bool cancelImmediately)
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, cancelImmediately);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002826C File Offset: 0x0002646C
		public static UniTask<global::UnityEngine.Object[]> ToUniTask(this AsyncInstantiateOperation asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false)
		{
			Error.ThrowArgumentNullException<AsyncInstantiateOperation>(asyncOperation, "asyncOperation");
			if (cancellationToken.IsCancellationRequested)
			{
				return UniTask.FromCanceled<global::UnityEngine.Object[]>(cancellationToken);
			}
			if (asyncOperation.isDone)
			{
				return UniTask.FromResult<global::UnityEngine.Object[]>(asyncOperation.Result);
			}
			short token;
			return new UniTask<global::UnityEngine.Object[]>(AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource.Create(asyncOperation, timing, progress, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x000282BB File Offset: 0x000264BB
		public static UniTask<T[]> WithCancellation<T>(this AsyncInstantiateOperation<T> asyncOperation, CancellationToken cancellationToken) where T : global::UnityEngine.Object
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, false);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x000282C7 File Offset: 0x000264C7
		public static UniTask<T[]> WithCancellation<T>(this AsyncInstantiateOperation<T> asyncOperation, CancellationToken cancellationToken, bool cancelImmediately) where T : global::UnityEngine.Object
		{
			return asyncOperation.ToUniTask(null, PlayerLoopTiming.Update, cancellationToken, cancelImmediately);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000282D4 File Offset: 0x000264D4
		public static UniTask<T[]> ToUniTask<T>(this AsyncInstantiateOperation<T> asyncOperation, IProgress<float> progress = null, PlayerLoopTiming timing = PlayerLoopTiming.Update, CancellationToken cancellationToken = default(CancellationToken), bool cancelImmediately = false) where T : global::UnityEngine.Object
		{
			Error.ThrowArgumentNullException<AsyncInstantiateOperation<T>>(asyncOperation, "asyncOperation");
			if (cancellationToken.IsCancellationRequested)
			{
				return UniTask.FromCanceled<T[]>(cancellationToken);
			}
			if (asyncOperation.isDone)
			{
				return UniTask.FromResult<T[]>(asyncOperation.Result);
			}
			short token;
			return new UniTask<T[]>(AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>.Create(asyncOperation, timing, progress, cancellationToken, cancelImmediately, out token), token);
		}

		// Token: 0x0200016F RID: 367
		private sealed class AsyncInstantiateOperationConfiguredSource : IUniTaskSource<global::UnityEngine.Object[]>, IUniTaskSource, IValueTaskSource, IValueTaskSource<global::UnityEngine.Object[]>, IPlayerLoopItem, ITaskPoolNode<AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource>
		{
			// Token: 0x17000065 RID: 101
			// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00028323 File Offset: 0x00026523
			public ref AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060008E2 RID: 2274 RVA: 0x0002832B File Offset: 0x0002652B
			static AsyncInstantiateOperationConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource), () => AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource.pool.Size);
			}

			// Token: 0x060008E3 RID: 2275 RVA: 0x0002834C File Offset: 0x0002654C
			private AsyncInstantiateOperationConfiguredSource()
			{
				this.continuationAction = new Action<AsyncOperation>(this.Continuation);
			}

			// Token: 0x060008E4 RID: 2276 RVA: 0x00028368 File Offset: 0x00026568
			public static IUniTaskSource<global::UnityEngine.Object[]> Create(AsyncInstantiateOperation asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<global::UnityEngine.Object[]>.CreateFromCanceled(cancellationToken, out token);
				}
				AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource result;
				if (!AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource.pool.TryPop(out result))
				{
					result = new AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource();
				}
				result.asyncOperation = asyncOperation;
				result.progress = progress;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				result.completed = false;
				asyncOperation.completed += result.continuationAction;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource source = (AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource)state;
						source.core.TrySetCanceled(source.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x060008E5 RID: 2277 RVA: 0x0002841C File Offset: 0x0002661C
			public global::UnityEngine.Object[] GetResult(short token)
			{
				global::UnityEngine.Object[] result;
				try
				{
					result = this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
				return result;
			}

			// Token: 0x060008E6 RID: 2278 RVA: 0x00028468 File Offset: 0x00026668
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060008E7 RID: 2279 RVA: 0x00028472 File Offset: 0x00026672
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060008E8 RID: 2280 RVA: 0x00028480 File Offset: 0x00026680
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060008E9 RID: 2281 RVA: 0x0002848D File Offset: 0x0002668D
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060008EA RID: 2282 RVA: 0x000284A0 File Offset: 0x000266A0
			public bool MoveNext()
			{
				if (this.completed || this.asyncOperation == null)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.progress != null)
				{
					this.progress.Report(this.asyncOperation.progress);
				}
				if (this.asyncOperation.isDone)
				{
					this.core.TrySetResult(this.asyncOperation.Result);
					return false;
				}
				return true;
			}

			// Token: 0x060008EB RID: 2283 RVA: 0x00028528 File Offset: 0x00026728
			private bool TryReturn()
			{
				this.core.Reset();
				this.asyncOperation.completed -= this.continuationAction;
				this.asyncOperation = null;
				this.progress = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource.pool.TryPush(this);
			}

			// Token: 0x060008EC RID: 2284 RVA: 0x00028588 File Offset: 0x00026788
			private void Continuation(AsyncOperation _)
			{
				if (this.completed)
				{
					return;
				}
				this.completed = true;
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return;
				}
				this.core.TrySetResult(this.asyncOperation.Result);
			}

			// Token: 0x040005B7 RID: 1463
			private static TaskPool<AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource> pool;

			// Token: 0x040005B8 RID: 1464
			private AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource nextNode;

			// Token: 0x040005B9 RID: 1465
			private AsyncInstantiateOperation asyncOperation;

			// Token: 0x040005BA RID: 1466
			private IProgress<float> progress;

			// Token: 0x040005BB RID: 1467
			private CancellationToken cancellationToken;

			// Token: 0x040005BC RID: 1468
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x040005BD RID: 1469
			private bool cancelImmediately;

			// Token: 0x040005BE RID: 1470
			private bool completed;

			// Token: 0x040005BF RID: 1471
			private UniTaskCompletionSourceCore<global::UnityEngine.Object[]> core;

			// Token: 0x040005C0 RID: 1472
			private Action<AsyncOperation> continuationAction;
		}

		// Token: 0x02000171 RID: 369
		private sealed class AsyncInstantiateOperationConfiguredSource<T> : IUniTaskSource<T[]>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T[]>, IPlayerLoopItem, ITaskPoolNode<AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>> where T : global::UnityEngine.Object
		{
			// Token: 0x17000066 RID: 102
			// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0002861A File Offset: 0x0002681A
			public ref AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T> NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x060008F2 RID: 2290 RVA: 0x00028622 File Offset: 0x00026822
			static AsyncInstantiateOperationConfiguredSource()
			{
				TaskPool.RegisterSizeGetter(typeof(AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>), () => AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>.pool.Size);
			}

			// Token: 0x060008F3 RID: 2291 RVA: 0x00028643 File Offset: 0x00026843
			private AsyncInstantiateOperationConfiguredSource()
			{
				this.continuationAction = new Action<AsyncOperation>(this.Continuation);
			}

			// Token: 0x060008F4 RID: 2292 RVA: 0x00028660 File Offset: 0x00026860
			public static IUniTaskSource<T[]> Create(AsyncInstantiateOperation<T> asyncOperation, PlayerLoopTiming timing, IProgress<float> progress, CancellationToken cancellationToken, bool cancelImmediately, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<T[]>.CreateFromCanceled(cancellationToken, out token);
				}
				AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T> result;
				if (!AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>.pool.TryPop(out result))
				{
					result = new AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>();
				}
				result.asyncOperation = asyncOperation;
				result.progress = progress;
				result.cancellationToken = cancellationToken;
				result.cancelImmediately = cancelImmediately;
				result.completed = false;
				asyncOperation.completed += result.continuationAction;
				if (cancelImmediately && cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(delegate(object state)
					{
						AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T> source = (AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>)state;
						source.core.TrySetCanceled(source.cancellationToken);
					}, result);
				}
				PlayerLoopHelper.AddAction(timing, result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x060008F5 RID: 2293 RVA: 0x00028714 File Offset: 0x00026914
			public T[] GetResult(short token)
			{
				T[] result;
				try
				{
					result = this.core.GetResult(token);
				}
				finally
				{
					if (!this.cancelImmediately || !this.cancellationToken.IsCancellationRequested)
					{
						this.TryReturn();
					}
				}
				return result;
			}

			// Token: 0x060008F6 RID: 2294 RVA: 0x00028760 File Offset: 0x00026960
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x060008F7 RID: 2295 RVA: 0x0002876A File Offset: 0x0002696A
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060008F8 RID: 2296 RVA: 0x00028778 File Offset: 0x00026978
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060008F9 RID: 2297 RVA: 0x00028785 File Offset: 0x00026985
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060008FA RID: 2298 RVA: 0x00028798 File Offset: 0x00026998
			public bool MoveNext()
			{
				if (this.completed || this.asyncOperation == null)
				{
					return false;
				}
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return false;
				}
				if (this.progress != null)
				{
					this.progress.Report(this.asyncOperation.progress);
				}
				if (this.asyncOperation.isDone)
				{
					this.core.TrySetResult(this.asyncOperation.Result);
					return false;
				}
				return true;
			}

			// Token: 0x060008FB RID: 2299 RVA: 0x00028820 File Offset: 0x00026A20
			private bool TryReturn()
			{
				this.core.Reset();
				this.asyncOperation.completed -= this.continuationAction;
				this.asyncOperation = null;
				this.progress = null;
				this.cancellationToken = default(CancellationToken);
				this.cancellationTokenRegistration.Dispose();
				this.cancelImmediately = false;
				return AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>.pool.TryPush(this);
			}

			// Token: 0x060008FC RID: 2300 RVA: 0x00028880 File Offset: 0x00026A80
			private void Continuation(AsyncOperation _)
			{
				if (this.completed)
				{
					return;
				}
				this.completed = true;
				if (this.cancellationToken.IsCancellationRequested)
				{
					this.core.TrySetCanceled(this.cancellationToken);
					return;
				}
				this.core.TrySetResult(this.asyncOperation.Result);
			}

			// Token: 0x040005C3 RID: 1475
			private static TaskPool<AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T>> pool;

			// Token: 0x040005C4 RID: 1476
			private AsyncInstantiateOperationExtensions.AsyncInstantiateOperationConfiguredSource<T> nextNode;

			// Token: 0x040005C5 RID: 1477
			private AsyncInstantiateOperation<T> asyncOperation;

			// Token: 0x040005C6 RID: 1478
			private IProgress<float> progress;

			// Token: 0x040005C7 RID: 1479
			private CancellationToken cancellationToken;

			// Token: 0x040005C8 RID: 1480
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x040005C9 RID: 1481
			private bool cancelImmediately;

			// Token: 0x040005CA RID: 1482
			private bool completed;

			// Token: 0x040005CB RID: 1483
			private UniTaskCompletionSourceCore<T[]> core;

			// Token: 0x040005CC RID: 1484
			private Action<AsyncOperation> continuationAction;
		}
	}
}
