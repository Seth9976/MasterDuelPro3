using System;
using System.Threading;
using System.Threading.Tasks.Sources;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class AsyncReactiveProperty<T> : IAsyncReactiveProperty<T>, IReadOnlyAsyncReactiveProperty<T>, IUniTaskAsyncEnumerable<T>, IDisposable
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000253B File Offset: 0x0000073B
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002543 File Offset: 0x00000743
		public T Value
		{
			get
			{
				return this.latestValue;
			}
			set
			{
				this.latestValue = value;
				this.triggerEvent.SetResult(value);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002558 File Offset: 0x00000758
		public AsyncReactiveProperty(T value)
		{
			this.latestValue = value;
			this.triggerEvent = default(TriggerEvent<T>);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002573 File Offset: 0x00000773
		public IUniTaskAsyncEnumerable<T> WithoutCurrent()
		{
			return new AsyncReactiveProperty<T>.WithoutCurrentEnumerable(this);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000257B File Offset: 0x0000077B
		public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken)
		{
			return new AsyncReactiveProperty<T>.Enumerator(this, cancellationToken, true);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002585 File Offset: 0x00000785
		public void Dispose()
		{
			this.triggerEvent.SetCompleted();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002592 File Offset: 0x00000792
		public static implicit operator T(AsyncReactiveProperty<T> value)
		{
			return value.Value;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000259C File Offset: 0x0000079C
		public override string ToString()
		{
			if (AsyncReactiveProperty<T>.isValueType)
			{
				return this.latestValue.ToString();
			}
			ref T ptr = ref this.latestValue;
			T t = default(T);
			if (t == null)
			{
				t = this.latestValue;
				ptr = ref t;
				if (t == null)
				{
					return null;
				}
			}
			return ptr.ToString();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025F8 File Offset: 0x000007F8
		public UniTask<T> WaitAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			short token;
			return new UniTask<T>(AsyncReactiveProperty<T>.WaitAsyncSource.Create(this, cancellationToken, out token), token);
		}

		// Token: 0x04000013 RID: 19
		private TriggerEvent<T> triggerEvent;

		// Token: 0x04000014 RID: 20
		[SerializeField]
		private T latestValue;

		// Token: 0x04000015 RID: 21
		private static bool isValueType = typeof(T).IsValueType;

		// Token: 0x0200000A RID: 10
		private sealed class WaitAsyncSource : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>, ITriggerHandler<T>, ITaskPoolNode<AsyncReactiveProperty<T>.WaitAsyncSource>
		{
			// Token: 0x17000007 RID: 7
			// (get) Token: 0x06000026 RID: 38 RVA: 0x0000262A File Offset: 0x0000082A
			ref AsyncReactiveProperty<T>.WaitAsyncSource ITaskPoolNode<AsyncReactiveProperty<T>.WaitAsyncSource>.NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000027 RID: 39 RVA: 0x00002632 File Offset: 0x00000832
			static WaitAsyncSource()
			{
				TaskPool.RegisterSizeGetter(typeof(AsyncReactiveProperty<T>.WaitAsyncSource), () => AsyncReactiveProperty<T>.WaitAsyncSource.pool.Size);
			}

			// Token: 0x06000028 RID: 40 RVA: 0x000020BB File Offset: 0x000002BB
			private WaitAsyncSource()
			{
			}

			// Token: 0x06000029 RID: 41 RVA: 0x00002664 File Offset: 0x00000864
			public static IUniTaskSource<T> Create(AsyncReactiveProperty<T> parent, CancellationToken cancellationToken, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<T>.CreateFromCanceled(cancellationToken, out token);
				}
				AsyncReactiveProperty<T>.WaitAsyncSource result;
				if (!AsyncReactiveProperty<T>.WaitAsyncSource.pool.TryPop(out result))
				{
					result = new AsyncReactiveProperty<T>.WaitAsyncSource();
				}
				result.parent = parent;
				result.cancellationToken = cancellationToken;
				if (cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(AsyncReactiveProperty<T>.WaitAsyncSource.cancellationCallback, result);
				}
				result.parent.triggerEvent.Add(result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x0600002A RID: 42 RVA: 0x000026E0 File Offset: 0x000008E0
			private bool TryReturn()
			{
				this.core.Reset();
				this.cancellationTokenRegistration.Dispose();
				this.cancellationTokenRegistration = default(CancellationTokenRegistration);
				this.parent.triggerEvent.Remove(this);
				this.parent = null;
				this.cancellationToken = default(CancellationToken);
				return AsyncReactiveProperty<T>.WaitAsyncSource.pool.TryPush(this);
			}

			// Token: 0x0600002B RID: 43 RVA: 0x0000273E File Offset: 0x0000093E
			private static void CancellationCallback(object state)
			{
				AsyncReactiveProperty<T>.WaitAsyncSource waitAsyncSource = (AsyncReactiveProperty<T>.WaitAsyncSource)state;
				waitAsyncSource.OnCanceled(waitAsyncSource.cancellationToken);
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00002754 File Offset: 0x00000954
			public T GetResult(short token)
			{
				T result;
				try
				{
					result = this.core.GetResult(token);
				}
				finally
				{
					this.TryReturn();
				}
				return result;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x0000278C File Offset: 0x0000098C
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600002E RID: 46 RVA: 0x00002796 File Offset: 0x00000996
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x0600002F RID: 47 RVA: 0x000027A6 File Offset: 0x000009A6
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000030 RID: 48 RVA: 0x000027B4 File Offset: 0x000009B4
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000031 RID: 49 RVA: 0x000027C1 File Offset: 0x000009C1
			// (set) Token: 0x06000032 RID: 50 RVA: 0x000027C9 File Offset: 0x000009C9
			ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000033 RID: 51 RVA: 0x000027D2 File Offset: 0x000009D2
			// (set) Token: 0x06000034 RID: 52 RVA: 0x000027DA File Offset: 0x000009DA
			ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

			// Token: 0x06000035 RID: 53 RVA: 0x000027E3 File Offset: 0x000009E3
			public void OnCanceled(CancellationToken cancellationToken)
			{
				this.core.TrySetCanceled(cancellationToken);
			}

			// Token: 0x06000036 RID: 54 RVA: 0x000027F2 File Offset: 0x000009F2
			public void OnCompleted()
			{
				this.core.TrySetCanceled(CancellationToken.None);
			}

			// Token: 0x06000037 RID: 55 RVA: 0x00002805 File Offset: 0x00000A05
			public void OnError(Exception ex)
			{
				this.core.TrySetException(ex);
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00002814 File Offset: 0x00000A14
			public void OnNext(T value)
			{
				this.core.TrySetResult(value);
			}

			// Token: 0x04000016 RID: 22
			private static Action<object> cancellationCallback = new Action<object>(AsyncReactiveProperty<T>.WaitAsyncSource.CancellationCallback);

			// Token: 0x04000017 RID: 23
			private static TaskPool<AsyncReactiveProperty<T>.WaitAsyncSource> pool;

			// Token: 0x04000018 RID: 24
			private AsyncReactiveProperty<T>.WaitAsyncSource nextNode;

			// Token: 0x04000019 RID: 25
			private AsyncReactiveProperty<T> parent;

			// Token: 0x0400001A RID: 26
			private CancellationToken cancellationToken;

			// Token: 0x0400001B RID: 27
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400001C RID: 28
			private UniTaskCompletionSourceCore<T> core;
		}

		// Token: 0x0200000C RID: 12
		private sealed class WithoutCurrentEnumerable : IUniTaskAsyncEnumerable<T>
		{
			// Token: 0x0600003C RID: 60 RVA: 0x0000283B File Offset: 0x00000A3B
			public WithoutCurrentEnumerable(AsyncReactiveProperty<T> parent)
			{
				this.parent = parent;
			}

			// Token: 0x0600003D RID: 61 RVA: 0x0000284A File Offset: 0x00000A4A
			public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
			{
				return new AsyncReactiveProperty<T>.Enumerator(this.parent, cancellationToken, false);
			}

			// Token: 0x04000020 RID: 32
			private readonly AsyncReactiveProperty<T> parent;
		}

		// Token: 0x0200000D RID: 13
		private sealed class Enumerator : MoveNextSource, IUniTaskAsyncEnumerator<T>, IUniTaskAsyncDisposable, ITriggerHandler<T>
		{
			// Token: 0x0600003E RID: 62 RVA: 0x0000285C File Offset: 0x00000A5C
			public Enumerator(AsyncReactiveProperty<T> parent, CancellationToken cancellationToken, bool publishCurrentValue)
			{
				this.parent = parent;
				this.cancellationToken = cancellationToken;
				this.firstCall = publishCurrentValue;
				parent.triggerEvent.Add(this);
				if (cancellationToken.CanBeCanceled)
				{
					this.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(AsyncReactiveProperty<T>.Enumerator.cancellationCallback, this);
				}
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x0600003F RID: 63 RVA: 0x000028AB File Offset: 0x00000AAB
			public T Current
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000040 RID: 64 RVA: 0x000028B3 File Offset: 0x00000AB3
			// (set) Token: 0x06000041 RID: 65 RVA: 0x000028BB File Offset: 0x00000ABB
			ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000042 RID: 66 RVA: 0x000028C4 File Offset: 0x00000AC4
			// (set) Token: 0x06000043 RID: 67 RVA: 0x000028CC File Offset: 0x00000ACC
			ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

			// Token: 0x06000044 RID: 68 RVA: 0x000028D8 File Offset: 0x00000AD8
			public UniTask<bool> MoveNextAsync()
			{
				if (this.firstCall)
				{
					this.firstCall = false;
					this.value = this.parent.Value;
					return CompletedTasks.True;
				}
				this.completionSource.Reset();
				return new UniTask<bool>(this, this.completionSource.Version);
			}

			// Token: 0x06000045 RID: 69 RVA: 0x00002928 File Offset: 0x00000B28
			public UniTask DisposeAsync()
			{
				if (!this.isDisposed)
				{
					this.isDisposed = true;
					this.completionSource.TrySetCanceled(this.cancellationToken);
					this.parent.triggerEvent.Remove(this);
				}
				return default(UniTask);
			}

			// Token: 0x06000046 RID: 70 RVA: 0x00002970 File Offset: 0x00000B70
			public void OnNext(T value)
			{
				this.value = value;
				this.completionSource.TrySetResult(true);
			}

			// Token: 0x06000047 RID: 71 RVA: 0x00002986 File Offset: 0x00000B86
			public void OnCanceled(CancellationToken cancellationToken)
			{
				this.DisposeAsync().Forget();
			}

			// Token: 0x06000048 RID: 72 RVA: 0x00002993 File Offset: 0x00000B93
			public void OnCompleted()
			{
				this.completionSource.TrySetResult(false);
			}

			// Token: 0x06000049 RID: 73 RVA: 0x000029A2 File Offset: 0x00000BA2
			public void OnError(Exception ex)
			{
				this.completionSource.TrySetException(ex);
			}

			// Token: 0x0600004A RID: 74 RVA: 0x000029B1 File Offset: 0x00000BB1
			private static void CancellationCallback(object state)
			{
				((AsyncReactiveProperty<T>.Enumerator)state).DisposeAsync().Forget();
			}

			// Token: 0x04000021 RID: 33
			private static Action<object> cancellationCallback = new Action<object>(AsyncReactiveProperty<T>.Enumerator.CancellationCallback);

			// Token: 0x04000022 RID: 34
			private readonly AsyncReactiveProperty<T> parent;

			// Token: 0x04000023 RID: 35
			private readonly CancellationToken cancellationToken;

			// Token: 0x04000024 RID: 36
			private readonly CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000025 RID: 37
			private T value;

			// Token: 0x04000026 RID: 38
			private bool isDisposed;

			// Token: 0x04000027 RID: 39
			private bool firstCall;
		}
	}
}
