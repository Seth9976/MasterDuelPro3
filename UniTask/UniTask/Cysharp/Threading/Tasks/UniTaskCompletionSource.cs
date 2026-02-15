using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks.Sources;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000125 RID: 293
	public class UniTaskCompletionSource : IUniTaskSource, IValueTaskSource, IPromise, IResolvePromise, IRejectPromise, ICancelPromise
	{
		// Token: 0x0600070A RID: 1802 RVA: 0x00020DC5 File Offset: 0x0001EFC5
		[DebuggerHidden]
		internal void MarkHandled()
		{
			if (!this.handled)
			{
				this.handled = true;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600070B RID: 1803 RVA: 0x00020DD6 File Offset: 0x0001EFD6
		public UniTask Task
		{
			[DebuggerHidden]
			get
			{
				return new UniTask(this, 0);
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00020DDF File Offset: 0x0001EFDF
		[DebuggerHidden]
		public bool TrySetResult()
		{
			return this.TrySignalCompletion(UniTaskStatus.Succeeded);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00020DE8 File Offset: 0x0001EFE8
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

		// Token: 0x0600070E RID: 1806 RVA: 0x00020E04 File Offset: 0x0001F004
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

		// Token: 0x0600070F RID: 1807 RVA: 0x00020E4C File Offset: 0x0001F04C
		[DebuggerHidden]
		public void GetResult(short token)
		{
			this.MarkHandled();
			switch (this.intStatus)
			{
			case 1:
				return;
			case 2:
				this.exception.GetException().Throw();
				return;
			case 3:
				throw new OperationCanceledException(this.cancellationToken);
			}
			throw new InvalidOperationException("not yet completed.");
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00020EA6 File Offset: 0x0001F0A6
		[DebuggerHidden]
		public UniTaskStatus GetStatus(short token)
		{
			return (UniTaskStatus)this.intStatus;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00020EA6 File Offset: 0x0001F0A6
		[DebuggerHidden]
		public UniTaskStatus UnsafeGetStatus()
		{
			return (UniTaskStatus)this.intStatus;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00020EB0 File Offset: 0x0001F0B0
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

		// Token: 0x06000713 RID: 1811 RVA: 0x00020F58 File Offset: 0x0001F158
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

		// Token: 0x04000449 RID: 1097
		private CancellationToken cancellationToken;

		// Token: 0x0400044A RID: 1098
		private ExceptionHolder exception;

		// Token: 0x0400044B RID: 1099
		private object gate;

		// Token: 0x0400044C RID: 1100
		private Action<object> singleContinuation;

		// Token: 0x0400044D RID: 1101
		private object singleState;

		// Token: 0x0400044E RID: 1102
		private List<ValueTuple<Action<object>, object>> secondaryContinuationList;

		// Token: 0x0400044F RID: 1103
		private int intStatus;

		// Token: 0x04000450 RID: 1104
		private bool handled;
	}
}
