using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000126 RID: 294
	public class UniTaskCompletionSource<T> : IUniTaskSource<T>, IUniTaskSource, IValueTaskSource, IValueTaskSource<T>, IPromise<T>, IResolvePromise<T>, IRejectPromise, ICancelPromise
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x00021078 File Offset: 0x0001F278
		[DebuggerHidden]
		internal void MarkHandled()
		{
			if (!this.handled)
			{
				this.handled = true;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00021089 File Offset: 0x0001F289
		public UniTask<T> Task
		{
			[DebuggerHidden]
			get
			{
				return new UniTask<T>(this, 0);
			}
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00021092 File Offset: 0x0001F292
		[DebuggerHidden]
		public bool TrySetResult(T result)
		{
			if (this.UnsafeGetStatus() != UniTaskStatus.Pending)
			{
				return false;
			}
			this.result = result;
			return this.TrySignalCompletion(UniTaskStatus.Succeeded);
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000210AC File Offset: 0x0001F2AC
		[DebuggerHidden]
		public bool TrySetCanceled(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (this.UnsafeGetStatus() != UniTaskStatus.Pending)
			{
				return false;
			}
			this.cancellationToken = cancellationToken;
			return this.TrySignalCompletion(UniTaskStatus.Canceled);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x000210C8 File Offset: 0x0001F2C8
		[DebuggerHidden]
		public bool TrySetException(Exception exception)
		{
			OperationCanceledException oce = exception as OperationCanceledException;
			if (oce != null)
			{
				return this.TrySetCanceled(oce.CancellationToken);
			}
			if (this.UnsafeGetStatus() != UniTaskStatus.Pending)
			{
				return false;
			}
			this.exception = new ExceptionHolder(ExceptionDispatchInfo.Capture(exception));
			return this.TrySignalCompletion(UniTaskStatus.Faulted);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00021110 File Offset: 0x0001F310
		[DebuggerHidden]
		public T GetResult(short token)
		{
			this.MarkHandled();
			switch (this.intStatus)
			{
			case 1:
				return this.result;
			case 2:
				this.exception.GetException().Throw();
				return default(T);
			case 3:
				throw new OperationCanceledException(this.cancellationToken);
			}
			throw new InvalidOperationException("not yet completed.");
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00021179 File Offset: 0x0001F379
		[DebuggerHidden]
		void IUniTaskSource.GetResult(short token)
		{
			this.GetResult(token);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00021183 File Offset: 0x0001F383
		[DebuggerHidden]
		public UniTaskStatus GetStatus(short token)
		{
			return (UniTaskStatus)this.intStatus;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00021183 File Offset: 0x0001F383
		[DebuggerHidden]
		public UniTaskStatus UnsafeGetStatus()
		{
			return (UniTaskStatus)this.intStatus;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0002118C File Offset: 0x0001F38C
		[DebuggerHidden]
		public void OnCompleted(Action<object> continuation, object state, short token)
		{
			if (this.gate == null)
			{
				Interlocked.CompareExchange(ref this.gate, new object(), null);
			}
			object obj = Thread.VolatileRead(ref this.gate);
			lock (obj)
			{
				if (this.intStatus != 0)
				{
					continuation(state);
				}
				else if (this.singleContinuation == null)
				{
					this.singleContinuation = continuation;
					this.singleState = state;
				}
				else
				{
					if (this.secondaryContinuationList == null)
					{
						this.secondaryContinuationList = new List<ValueTuple<Action<object>, object>>();
					}
					this.secondaryContinuationList.Add(new ValueTuple<Action<object>, object>(continuation, state));
				}
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00021234 File Offset: 0x0001F434
		[DebuggerHidden]
		private bool TrySignalCompletion(UniTaskStatus status)
		{
			if (Interlocked.CompareExchange(ref this.intStatus, (int)status, 0) == 0)
			{
				if (this.gate == null)
				{
					Interlocked.CompareExchange(ref this.gate, new object(), null);
				}
				object obj = Thread.VolatileRead(ref this.gate);
				lock (obj)
				{
					if (this.singleContinuation != null)
					{
						try
						{
							this.singleContinuation(this.singleState);
						}
						catch (Exception ex)
						{
							UniTaskScheduler.PublishUnobservedTaskException(ex);
						}
					}
					if (this.secondaryContinuationList != null)
					{
						foreach (ValueTuple<Action<object>, object> valueTuple in this.secondaryContinuationList)
						{
							Action<object> c = valueTuple.Item1;
							object state = valueTuple.Item2;
							try
							{
								c(state);
							}
							catch (Exception ex2)
							{
								UniTaskScheduler.PublishUnobservedTaskException(ex2);
							}
						}
					}
					this.singleContinuation = null;
					this.singleState = null;
					this.secondaryContinuationList = null;
				}
				return true;
			}
			return false;
		}

		// Token: 0x04000451 RID: 1105
		private CancellationToken cancellationToken;

		// Token: 0x04000452 RID: 1106
		private T result;

		// Token: 0x04000453 RID: 1107
		private ExceptionHolder exception;

		// Token: 0x04000454 RID: 1108
		private object gate;

		// Token: 0x04000455 RID: 1109
		private Action<object> singleContinuation;

		// Token: 0x04000456 RID: 1110
		private object singleState;

		// Token: 0x04000457 RID: 1111
		private List<ValueTuple<Action<object>, object>> secondaryContinuationList;

		// Token: 0x04000458 RID: 1112
		private int intStatus;

		// Token: 0x04000459 RID: 1113
		private bool handled;
	}
}
