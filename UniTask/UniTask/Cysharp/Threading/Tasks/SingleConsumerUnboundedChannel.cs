using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000023 RID: 35
	internal class SingleConsumerUnboundedChannel<T> : Channel<T>
	{
		// Token: 0x060000BF RID: 191 RVA: 0x000036B1 File Offset: 0x000018B1
		public SingleConsumerUnboundedChannel()
		{
			this.items = new Queue<T>();
			base.Writer = new SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelWriter(this);
			this.readerSource = new SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader(this);
			base.Reader = this.readerSource;
		}

		// Token: 0x0400005E RID: 94
		private readonly Queue<T> items;

		// Token: 0x0400005F RID: 95
		private readonly SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader readerSource;

		// Token: 0x04000060 RID: 96
		private UniTaskCompletionSource completedTaskSource;

		// Token: 0x04000061 RID: 97
		private UniTask completedTask;

		// Token: 0x04000062 RID: 98
		private Exception completionError;

		// Token: 0x04000063 RID: 99
		private bool closed;

		// Token: 0x02000024 RID: 36
		private sealed class SingleConsumerUnboundedChannelWriter : ChannelWriter<T>
		{
			// Token: 0x060000C0 RID: 192 RVA: 0x000036E8 File Offset: 0x000018E8
			public SingleConsumerUnboundedChannelWriter(SingleConsumerUnboundedChannel<T> parent)
			{
				this.parent = parent;
			}

			// Token: 0x060000C1 RID: 193 RVA: 0x000036F8 File Offset: 0x000018F8
			public override bool TryWrite(T item)
			{
				Queue<T> items = this.parent.items;
				bool waiting;
				lock (items)
				{
					if (this.parent.closed)
					{
						return false;
					}
					this.parent.items.Enqueue(item);
					waiting = this.parent.readerSource.isWaiting;
				}
				if (waiting)
				{
					this.parent.readerSource.SingalContinuation();
				}
				return true;
			}

			// Token: 0x060000C2 RID: 194 RVA: 0x00003780 File Offset: 0x00001980
			public override bool TryComplete(Exception error = null)
			{
				Queue<T> items = this.parent.items;
				lock (items)
				{
					if (this.parent.closed)
					{
						return false;
					}
					this.parent.closed = true;
					bool waiting = this.parent.readerSource.isWaiting;
					if (this.parent.items.Count == 0)
					{
						if (error == null)
						{
							if (this.parent.completedTaskSource != null)
							{
								this.parent.completedTaskSource.TrySetResult();
							}
							else
							{
								this.parent.completedTask = UniTask.CompletedTask;
							}
						}
						else if (this.parent.completedTaskSource != null)
						{
							this.parent.completedTaskSource.TrySetException(error);
						}
						else
						{
							this.parent.completedTask = UniTask.FromException(error);
						}
						if (waiting)
						{
							this.parent.readerSource.SingalCompleted(error);
						}
					}
					this.parent.completionError = error;
				}
				return true;
			}

			// Token: 0x04000064 RID: 100
			private readonly SingleConsumerUnboundedChannel<T> parent;
		}

		// Token: 0x02000025 RID: 37
		private sealed class SingleConsumerUnboundedChannelReader : ChannelReader<T>, IUniTaskSource<bool>, IUniTaskSource, IValueTaskSource, IValueTaskSource<bool>
		{
			// Token: 0x060000C3 RID: 195 RVA: 0x0000388C File Offset: 0x00001A8C
			public SingleConsumerUnboundedChannelReader(SingleConsumerUnboundedChannel<T> parent)
			{
				this.parent = parent;
			}

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x060000C4 RID: 196 RVA: 0x000038B0 File Offset: 0x00001AB0
			public override UniTask Completion
			{
				get
				{
					if (this.parent.completedTaskSource != null)
					{
						return this.parent.completedTaskSource.Task;
					}
					if (this.parent.closed)
					{
						return this.parent.completedTask;
					}
					this.parent.completedTaskSource = new UniTaskCompletionSource();
					return this.parent.completedTaskSource.Task;
				}
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x00003914 File Offset: 0x00001B14
			public override bool TryRead(out T item)
			{
				Queue<T> items = this.parent.items;
				lock (items)
				{
					if (this.parent.items.Count == 0)
					{
						item = default(T);
						return false;
					}
					item = this.parent.items.Dequeue();
					if (this.parent.closed && this.parent.items.Count == 0)
					{
						if (this.parent.completionError != null)
						{
							if (this.parent.completedTaskSource != null)
							{
								this.parent.completedTaskSource.TrySetException(this.parent.completionError);
							}
							else
							{
								this.parent.completedTask = UniTask.FromException(this.parent.completionError);
							}
						}
						else if (this.parent.completedTaskSource != null)
						{
							this.parent.completedTaskSource.TrySetResult();
						}
						else
						{
							this.parent.completedTask = UniTask.CompletedTask;
						}
					}
				}
				return true;
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x00003A38 File Offset: 0x00001C38
			public override UniTask<bool> WaitToReadAsync(CancellationToken cancellationToken)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					return UniTask.FromCanceled<bool>(cancellationToken);
				}
				Queue<T> items = this.parent.items;
				UniTask<bool> uniTask;
				lock (items)
				{
					if (this.parent.items.Count != 0)
					{
						uniTask = CompletedTasks.True;
					}
					else if (this.parent.closed)
					{
						if (this.parent.completionError == null)
						{
							uniTask = CompletedTasks.False;
						}
						else
						{
							uniTask = UniTask.FromException<bool>(this.parent.completionError);
						}
					}
					else
					{
						this.cancellationTokenRegistration.Dispose();
						this.core.Reset();
						this.isWaiting = true;
						this.cancellationToken = cancellationToken;
						if (this.cancellationToken.CanBeCanceled)
						{
							this.cancellationTokenRegistration = this.cancellationToken.RegisterWithoutCaptureExecutionContext(this.CancellationCallbackDelegate, this);
						}
						uniTask = new UniTask<bool>(this, this.core.Version);
					}
				}
				return uniTask;
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x00003B38 File Offset: 0x00001D38
			public void SingalContinuation()
			{
				this.core.TrySetResult(true);
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x00003B47 File Offset: 0x00001D47
			public void SingalCancellation(CancellationToken cancellationToken)
			{
				this.core.TrySetCanceled(cancellationToken);
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x00003B56 File Offset: 0x00001D56
			public void SingalCompleted(Exception error)
			{
				if (error != null)
				{
					this.core.TrySetException(error);
					return;
				}
				this.core.TrySetResult(false);
			}

			// Token: 0x060000CA RID: 202 RVA: 0x00003B76 File Offset: 0x00001D76
			public override IUniTaskAsyncEnumerable<T> ReadAllAsync(CancellationToken cancellationToken = default(CancellationToken))
			{
				return new SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable(this, cancellationToken);
			}

			// Token: 0x060000CB RID: 203 RVA: 0x00003B7F File Offset: 0x00001D7F
			bool IUniTaskSource<bool>.GetResult(short token)
			{
				return this.core.GetResult(token);
			}

			// Token: 0x060000CC RID: 204 RVA: 0x00003B8D File Offset: 0x00001D8D
			void IUniTaskSource.GetResult(short token)
			{
				this.core.GetResult(token);
			}

			// Token: 0x060000CD RID: 205 RVA: 0x00003B9C File Offset: 0x00001D9C
			UniTaskStatus IUniTaskSource.GetStatus(short token)
			{
				return this.core.GetStatus(token);
			}

			// Token: 0x060000CE RID: 206 RVA: 0x00003BAA File Offset: 0x00001DAA
			void IUniTaskSource.OnCompleted(Action<object> continuation, object state, short token)
			{
				this.core.OnCompleted(continuation, state, token);
			}

			// Token: 0x060000CF RID: 207 RVA: 0x00003BBA File Offset: 0x00001DBA
			UniTaskStatus IUniTaskSource.UnsafeGetStatus()
			{
				return this.core.UnsafeGetStatus();
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x00003BC7 File Offset: 0x00001DC7
			private static void CancellationCallback(object state)
			{
				SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader singleConsumerUnboundedChannelReader = (SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader)state;
				singleConsumerUnboundedChannelReader.SingalCancellation(singleConsumerUnboundedChannelReader.cancellationToken);
			}

			// Token: 0x04000065 RID: 101
			private readonly Action<object> CancellationCallbackDelegate = new Action<object>(SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.CancellationCallback);

			// Token: 0x04000066 RID: 102
			private readonly SingleConsumerUnboundedChannel<T> parent;

			// Token: 0x04000067 RID: 103
			private CancellationToken cancellationToken;

			// Token: 0x04000068 RID: 104
			private CancellationTokenRegistration cancellationTokenRegistration;

			// Token: 0x04000069 RID: 105
			private UniTaskCompletionSourceCore<bool> core;

			// Token: 0x0400006A RID: 106
			internal bool isWaiting;

			// Token: 0x02000026 RID: 38
			private sealed class ReadAllAsyncEnumerable : IUniTaskAsyncEnumerable<T>, IUniTaskAsyncEnumerator<T>, IUniTaskAsyncDisposable
			{
				// Token: 0x060000D1 RID: 209 RVA: 0x00003BDA File Offset: 0x00001DDA
				public ReadAllAsyncEnumerable(SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader parent, CancellationToken cancellationToken)
				{
					this.parent = parent;
					this.cancellationToken1 = cancellationToken;
				}

				// Token: 0x060000D2 RID: 210 RVA: 0x00003C14 File Offset: 0x00001E14
				public IUniTaskAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
				{
					if (this.running)
					{
						throw new InvalidOperationException("Enumerator is already running, does not allow call GetAsyncEnumerator twice.");
					}
					if (this.cancellationToken1 != cancellationToken)
					{
						this.cancellationToken2 = cancellationToken;
					}
					if (this.cancellationToken1.CanBeCanceled)
					{
						this.cancellationTokenRegistration1 = this.cancellationToken1.RegisterWithoutCaptureExecutionContext(this.CancellationCallback1Delegate, this);
					}
					if (this.cancellationToken2.CanBeCanceled)
					{
						this.cancellationTokenRegistration2 = this.cancellationToken2.RegisterWithoutCaptureExecutionContext(this.CancellationCallback2Delegate, this);
					}
					this.running = true;
					return this;
				}

				// Token: 0x17000019 RID: 25
				// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003C9B File Offset: 0x00001E9B
				public T Current
				{
					get
					{
						if (this.cacheValue)
						{
							return this.current;
						}
						this.parent.TryRead(out this.current);
						return this.current;
					}
				}

				// Token: 0x060000D4 RID: 212 RVA: 0x00003CC4 File Offset: 0x00001EC4
				public UniTask<bool> MoveNextAsync()
				{
					this.cacheValue = false;
					return this.parent.WaitToReadAsync(CancellationToken.None);
				}

				// Token: 0x060000D5 RID: 213 RVA: 0x00003CE0 File Offset: 0x00001EE0
				public UniTask DisposeAsync()
				{
					this.cancellationTokenRegistration1.Dispose();
					this.cancellationTokenRegistration2.Dispose();
					return default(UniTask);
				}

				// Token: 0x060000D6 RID: 214 RVA: 0x00003D0C File Offset: 0x00001F0C
				private static void CancellationCallback1(object state)
				{
					SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable self = (SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable)state;
					self.parent.SingalCancellation(self.cancellationToken1);
				}

				// Token: 0x060000D7 RID: 215 RVA: 0x00003D34 File Offset: 0x00001F34
				private static void CancellationCallback2(object state)
				{
					SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable self = (SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable)state;
					self.parent.SingalCancellation(self.cancellationToken2);
				}

				// Token: 0x0400006B RID: 107
				private readonly Action<object> CancellationCallback1Delegate = new Action<object>(SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable.CancellationCallback1);

				// Token: 0x0400006C RID: 108
				private readonly Action<object> CancellationCallback2Delegate = new Action<object>(SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader.ReadAllAsyncEnumerable.CancellationCallback2);

				// Token: 0x0400006D RID: 109
				private readonly SingleConsumerUnboundedChannel<T>.SingleConsumerUnboundedChannelReader parent;

				// Token: 0x0400006E RID: 110
				private CancellationToken cancellationToken1;

				// Token: 0x0400006F RID: 111
				private CancellationToken cancellationToken2;

				// Token: 0x04000070 RID: 112
				private CancellationTokenRegistration cancellationTokenRegistration1;

				// Token: 0x04000071 RID: 113
				private CancellationTokenRegistration cancellationTokenRegistration2;

				// Token: 0x04000072 RID: 114
				private T current;

				// Token: 0x04000073 RID: 115
				private bool cacheValue;

				// Token: 0x04000074 RID: 116
				private bool running;
			}
		}
	}
}
