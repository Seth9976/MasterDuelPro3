using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x0200000E RID: 14
	public class ReadOnlyAsyncReactiveProperty<T> : IReadOnlyAsyncReactiveProperty<T>, IUniTaskAsyncEnumerable<T>, IDisposable
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000029D6 File Offset: 0x00000BD6
		public T Value
		{
			get
			{
				return this.latestValue;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000029E0 File Offset: 0x00000BE0
		public ReadOnlyAsyncReactiveProperty(T initialValue, IUniTaskAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			this.latestValue = initialValue;
			this.ConsumeEnumerator(source, cancellationToken).Forget();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A0C File Offset: 0x00000C0C
		public ReadOnlyAsyncReactiveProperty(IUniTaskAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			this.ConsumeEnumerator(source, cancellationToken).Forget();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A30 File Offset: 0x00000C30
		private async UniTaskVoid ConsumeEnumerator(IUniTaskAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			this.enumerator = source.GetAsyncEnumerator(cancellationToken);
			object obj = null;
			try
			{
				for (;;)
				{
					UniTask<bool>.Awaiter awaiter = this.enumerator.MoveNextAsync().GetAwaiter();
					if (!awaiter.IsCompleted)
					{
						await awaiter;
						UniTask<bool>.Awaiter awaiter2;
						awaiter = awaiter2;
						awaiter2 = default(UniTask<bool>.Awaiter);
					}
					if (!awaiter.GetResult())
					{
						break;
					}
					T value = this.enumerator.Current;
					this.latestValue = value;
					this.triggerEvent.SetResult(value);
				}
			}
			catch (object obj)
			{
			}
			await this.enumerator.DisposeAsync();
			this.enumerator = null;
			object obj2 = obj;
			if (obj2 != null)
			{
				Exception ex = obj2 as Exception;
				if (ex == null)
				{
					throw obj2;
				}
				ExceptionDispatchInfo.Capture(ex).Throw();
			}
			obj = null;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A83 File Offset: 0x00000C83
		public IUniTaskAsyncEnumerable<T> WithoutCurrent()
		{
			return new ReadOnlyAsyncReactiveProperty<T>.WithoutCurrentEnumerable(this);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A8B File Offset: 0x00000C8B
		public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken)
		{
			return new ReadOnlyAsyncReactiveProperty<T>.Enumerator(this, cancellationToken, true);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A95 File Offset: 0x00000C95
		public void Dispose()
		{
			if (this.enumerator != null)
			{
				this.enumerator.DisposeAsync().Forget();
			}
			this.triggerEvent.SetCompleted();
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002ABA File Offset: 0x00000CBA
		public static implicit operator T(ReadOnlyAsyncReactiveProperty<T> value)
		{
			return value.Value;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public override string ToString()
		{
			if (ReadOnlyAsyncReactiveProperty<T>.isValueType)
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

		// Token: 0x06000055 RID: 85 RVA: 0x00002B20 File Offset: 0x00000D20
		public UniTask<T> WaitAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			short token;
			return new UniTask<T>(ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource.Create(this, cancellationToken, out token), token);
		}

		// Token: 0x0400002A RID: 42
		private TriggerEvent<T> triggerEvent;

		// Token: 0x0400002B RID: 43
		private T latestValue;

		// Token: 0x0400002C RID: 44
		private IUniTaskAsyncEnumerator<T> enumerator;

		// Token: 0x0400002D RID: 45
		private static bool isValueType = typeof(T).IsValueType;

		// Token: 0x0200000F RID: 15
		private sealed class WaitAsyncSource : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>, ITriggerHandler<T>, ITaskPoolNode<ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource>
		{
			// Token: 0x1700000E RID: 14
			// (get) Token: 0x06000057 RID: 87 RVA: 0x00002B52 File Offset: 0x00000D52
			ref ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource ITaskPoolNode<ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource>.NextNode
			{
				get
				{
					return ref this.nextNode;
				}
			}

			// Token: 0x06000058 RID: 88 RVA: 0x00002B5A File Offset: 0x00000D5A
			static WaitAsyncSource()
			{
				TaskPool.RegisterSizeGetter(typeof(ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource), () => ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource.pool.Size);
			}

			// Token: 0x06000059 RID: 89 RVA: 0x000020BB File Offset: 0x000002BB
			private WaitAsyncSource()
			{
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00002B8C File Offset: 0x00000D8C
			public static IUniTaskSource<T> Create(ReadOnlyAsyncReactiveProperty<T> parent, CancellationToken cancellationToken, out short token)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return AutoResetUniTaskCompletionSource<T>.CreateFromCanceled(cancellationToken, out token);
				}
				ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource result;
				if (!ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource.pool.TryPop(out result))
				{
					result = new ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource();
				}
				result.parent = parent;
				result.cancellationToken = cancellationToken;
				if (cancellationToken.CanBeCanceled)
				{
					result.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource.cancellationCallback, result);
				}
				result.parent.triggerEvent.Add(result);
				token = result.core.Version;
				return result;
			}

			// Token: 0x0600005B RID: 91 RVA: 0x00002C08 File Offset: 0x00000E08
			private bool TryReturn()
			{
				this.core.Reset();
				this.cancellationTokenRegistration.Dispose();
				this.cancellationTokenRegistration = default(CancellationTokenRegistration);
				this.parent.triggerEvent.Remove(this);
				this.parent = null;
				this.cancellationToken = default(CancellationToken);
				return ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource.pool.TryPush(this);
			}

			// Token: 0x0600005C RID: 92 RVA: 0x00002C66 File Offset: 0x00000E66
			private static void CancellationCallback(object state)
			{
				ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource waitAsyncSource = (ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource)state;
				waitAsyncSource.OnCanceled(waitAsyncSource.cancellationToken);
			}

			// Token: 0x0600005D RID: 93 RVA: 0x00002C7C File Offset: 0x00000E7C
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

			// Token: 0x0600005E RID: 94 RVA: 0x00002CB4 File Offset: 0x00000EB4
			void IUniTaskSource.GetResult(short token)
			{
				this.GetResult(token);
			}

			// Token: 0x0600005F RID: 95 RVA: 0x00002CBE File Offset: 0x00000EBE
			public void OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x06000060 RID: 96 RVA: 0x00002CCE File Offset: 0x00000ECE
			public UniTaskStatus GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x06000061 RID: 97 RVA: 0x00002CDC File Offset: 0x00000EDC
			public UniTaskStatus UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000062 RID: 98 RVA: 0x00002CE9 File Offset: 0x00000EE9
			// (set) Token: 0x06000063 RID: 99 RVA: 0x00002CF1 File Offset: 0x00000EF1
			ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000064 RID: 100 RVA: 0x00002CFA File Offset: 0x00000EFA
			// (set) Token: 0x06000065 RID: 101 RVA: 0x00002D02 File Offset: 0x00000F02
			ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

			// Token: 0x06000066 RID: 102 RVA: 0x00002D0B File Offset: 0x00000F0B
			public void OnCanceled(CancellationToken cancellationToken)
			{
				this.core.TrySetCanceled(cancellationToken);
			}

			// Token: 0x06000067 RID: 103 RVA: 0x00002D1A File Offset: 0x00000F1A
			public void OnCompleted()
			{
				this.core.TrySetCanceled(CancellationToken.None);
			}

			// Token: 0x06000068 RID: 104 RVA: 0x00002D2D File Offset: 0x00000F2D
			public void OnError(Exception ex)
			{
				this.core.TrySetException(ex);
			}

			// Token: 0x06000069 RID: 105 RVA: 0x00002D3C File Offset: 0x00000F3C
			public void OnNext(T value)
			{
				this.core.TrySetResult(value);
			}

			// Token: 0x0400002E RID: 46
			private static Action<object> cancellationCallback = new Action<object>(ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource.CancellationCallback);

			// Token: 0x0400002F RID: 47
			private static TaskPool<ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource> pool;

			// Token: 0x04000030 RID: 48
			private ReadOnlyAsyncReactiveProperty<T>.WaitAsyncSource nextNode;

			// Token: 0x04000031 RID: 49
			private ReadOnlyAsyncReactiveProperty<T> parent;

			// Token: 0x04000032 RID: 50
			private CancellationToken cancellationToken;

			// Token: 0x04000033 RID: 51
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000034 RID: 52
			private UniTaskCompletionSourceCore<T> core;
		}

		// Token: 0x02000011 RID: 17
		private sealed class WithoutCurrentEnumerable : IUniTaskAsyncEnumerable<T>
		{
			// Token: 0x0600006D RID: 109 RVA: 0x00002D63 File Offset: 0x00000F63
			public WithoutCurrentEnumerable(ReadOnlyAsyncReactiveProperty<T> parent)
			{
				this.parent = parent;
			}

			// Token: 0x0600006E RID: 110 RVA: 0x00002D72 File Offset: 0x00000F72
			public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
			{
				return new ReadOnlyAsyncReactiveProperty<T>.Enumerator(this.parent, cancellationToken, false);
			}

			// Token: 0x04000038 RID: 56
			private readonly ReadOnlyAsyncReactiveProperty<T> parent;
		}

		// Token: 0x02000012 RID: 18
		private sealed class Enumerator : MoveNextSource, IUniTaskAsyncEnumerator<T>, IUniTaskAsyncDisposable, ITriggerHandler<T>
		{
			// Token: 0x0600006F RID: 111 RVA: 0x00002D84 File Offset: 0x00000F84
			public Enumerator(ReadOnlyAsyncReactiveProperty<T> parent, CancellationToken cancellationToken, bool publishCurrentValue)
			{
				this.parent = parent;
				this.cancellationToken = cancellationToken;
				this.firstCall = publishCurrentValue;
				parent.triggerEvent.Add(this);
				if (cancellationToken.CanBeCanceled)
				{
					this.cancellationTokenRegistration = cancellationToken.RegisterWithoutCaptureExecutionContext(ReadOnlyAsyncReactiveProperty<T>.Enumerator.cancellationCallback, this);
				}
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000070 RID: 112 RVA: 0x00002DD3 File Offset: 0x00000FD3
			public T Current
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000071 RID: 113 RVA: 0x00002DDB File Offset: 0x00000FDB
			// (set) Token: 0x06000072 RID: 114 RVA: 0x00002DE3 File Offset: 0x00000FE3
			ITriggerHandler<T> ITriggerHandler<T>.Prev { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000073 RID: 115 RVA: 0x00002DEC File Offset: 0x00000FEC
			// (set) Token: 0x06000074 RID: 116 RVA: 0x00002DF4 File Offset: 0x00000FF4
			ITriggerHandler<T> ITriggerHandler<T>.Next { get; set; }

			// Token: 0x06000075 RID: 117 RVA: 0x00002E00 File Offset: 0x00001000
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

			// Token: 0x06000076 RID: 118 RVA: 0x00002E50 File Offset: 0x00001050
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

			// Token: 0x06000077 RID: 119 RVA: 0x00002E98 File Offset: 0x00001098
			public void OnNext(T value)
			{
				this.value = value;
				this.completionSource.TrySetResult(true);
			}

			// Token: 0x06000078 RID: 120 RVA: 0x00002EAE File Offset: 0x000010AE
			public void OnCanceled(CancellationToken cancellationToken)
			{
				this.DisposeAsync().Forget();
			}

			// Token: 0x06000079 RID: 121 RVA: 0x00002993 File Offset: 0x00000B93
			public void OnCompleted()
			{
				this.completionSource.TrySetResult(false);
			}

			// Token: 0x0600007A RID: 122 RVA: 0x000029A2 File Offset: 0x00000BA2
			public void OnError(Exception ex)
			{
				this.completionSource.TrySetException(ex);
			}

			// Token: 0x0600007B RID: 123 RVA: 0x00002EBB File Offset: 0x000010BB
			private static void CancellationCallback(object state)
			{
				((ReadOnlyAsyncReactiveProperty<T>.Enumerator)state).DisposeAsync().Forget();
			}

			// Token: 0x04000039 RID: 57
			private static Action<object> cancellationCallback = new Action<object>(ReadOnlyAsyncReactiveProperty<T>.Enumerator.CancellationCallback);

			// Token: 0x0400003A RID: 58
			private readonly ReadOnlyAsyncReactiveProperty<T> parent;

			// Token: 0x0400003B RID: 59
			private readonly CancellationToken cancellationToken;

			// Token: 0x0400003C RID: 60
			private readonly CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x0400003D RID: 61
			private T value;

			// Token: 0x0400003E RID: 62
			private bool isDisposed;

			// Token: 0x0400003F RID: 63
			private bool firstCall;
		}
	}
}
