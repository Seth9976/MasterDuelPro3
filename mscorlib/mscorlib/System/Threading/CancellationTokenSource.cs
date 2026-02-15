using System;
using System.Collections.Generic;

namespace System.Threading
{
	/// <summary>Signals to a <see cref="T:System.Threading.CancellationToken" /> that it should be canceled.</summary>
	// Token: 0x02000235 RID: 565
	public class CancellationTokenSource : IDisposable
	{
		/// <summary>Gets whether cancellation has been requested for this <see cref="T:System.Threading.CancellationTokenSource" />.</summary>
		/// <returns>Whether cancellation has been requested for this <see cref="T:System.Threading.CancellationTokenSource" />.</returns>
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00054919 File Offset: 0x00052B19
		public bool IsCancellationRequested
		{
			get
			{
				return this._state >= 2;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00054929 File Offset: 0x00052B29
		internal bool IsCancellationCompleted
		{
			get
			{
				return this._state == 3;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x00054936 File Offset: 0x00052B36
		internal bool IsDisposed
		{
			get
			{
				return this._disposed;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0005493E File Offset: 0x00052B3E
		// (set) Token: 0x060014FD RID: 5373 RVA: 0x00054948 File Offset: 0x00052B48
		internal int ThreadIDExecutingCallbacks
		{
			get
			{
				return this._threadIDExecutingCallbacks;
			}
			set
			{
				this._threadIDExecutingCallbacks = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.CancellationToken" /> associated with this <see cref="T:System.Threading.CancellationTokenSource" />.</summary>
		/// <returns>The <see cref="T:System.Threading.CancellationToken" /> associated with this <see cref="T:System.Threading.CancellationTokenSource" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The token source has been disposed.</exception>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00054953 File Offset: 0x00052B53
		public CancellationToken Token
		{
			get
			{
				this.ThrowIfDisposed();
				return new CancellationToken(this);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00054961 File Offset: 0x00052B61
		internal bool CanBeCanceled
		{
			get
			{
				return this._state != 0;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x00054970 File Offset: 0x00052B70
		internal WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				if (this._kernelEvent != null)
				{
					return this._kernelEvent;
				}
				ManualResetEvent manualResetEvent = new ManualResetEvent(false);
				if (Interlocked.CompareExchange<ManualResetEvent>(ref this._kernelEvent, manualResetEvent, null) != null)
				{
					manualResetEvent.Dispose();
				}
				if (this.IsCancellationRequested)
				{
					this._kernelEvent.Set();
				}
				return this._kernelEvent;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x000549D0 File Offset: 0x00052BD0
		internal CancellationCallbackInfo ExecutingCallback
		{
			get
			{
				return this._executingCallback;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.CancellationTokenSource" /> class.</summary>
		// Token: 0x06001502 RID: 5378 RVA: 0x000549DA File Offset: 0x00052BDA
		public CancellationTokenSource()
		{
			this._state = 1;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.CancellationTokenSource" /> class that will be canceled after the specified time span.</summary>
		/// <param name="delay">The time span to wait before canceling this <see cref="T:System.Threading.CancellationTokenSource" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="delay" />.<see cref="P:System.TimeSpan.TotalMilliseconds" /> is less than -1 or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x06001503 RID: 5379 RVA: 0x000549F4 File Offset: 0x00052BF4
		public CancellationTokenSource(TimeSpan delay)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			this.InitializeWithTimer((int)num);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.CancellationTokenSource" /> class that will be canceled after the specified delay in milliseconds.</summary>
		/// <param name="millisecondsDelay">The time span to wait before canceling this <see cref="T:System.Threading.CancellationTokenSource" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when <paramref name="millisecondsDelay" /> is less than -1.</exception>
		// Token: 0x06001504 RID: 5380 RVA: 0x00054A3A File Offset: 0x00052C3A
		public CancellationTokenSource(int millisecondsDelay)
		{
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			this.InitializeWithTimer(millisecondsDelay);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00054A61 File Offset: 0x00052C61
		private void InitializeWithTimer(int millisecondsDelay)
		{
			this._state = 1;
			this._timer = new Timer(CancellationTokenSource.s_timerCallback, this, millisecondsDelay, -1);
		}

		/// <summary>Communicates a request for cancellation.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">An aggregate exception containing all the exceptions thrown by the registered callbacks on the associated <see cref="T:System.Threading.CancellationToken" />.</exception>
		// Token: 0x06001506 RID: 5382 RVA: 0x00054A81 File Offset: 0x00052C81
		public void Cancel()
		{
			this.Cancel(false);
		}

		/// <summary>Communicates a request for cancellation, and specifies whether remaining callbacks and cancelable operations should be processed.</summary>
		/// <param name="throwOnFirstException">true if exceptions should immediately propagate; otherwise, false.</param>
		/// <exception cref="T:System.ObjectDisposedException">This <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.AggregateException">An aggregate exception containing all the exceptions thrown by the registered callbacks on the associated <see cref="T:System.Threading.CancellationToken" />.</exception>
		// Token: 0x06001507 RID: 5383 RVA: 0x00054A8A File Offset: 0x00052C8A
		public void Cancel(bool throwOnFirstException)
		{
			this.ThrowIfDisposed();
			this.NotifyCancellation(throwOnFirstException);
		}

		/// <summary>Schedules a cancel operation on this <see cref="T:System.Threading.CancellationTokenSource" /> after the specified time span.</summary>
		/// <param name="delay">The time span to wait before canceling this <see cref="T:System.Threading.CancellationTokenSource" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The exception thrown when this <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception that is thrown when <paramref name="delay" /> is less than -1 or greater than Int32.MaxValue.</exception>
		// Token: 0x06001508 RID: 5384 RVA: 0x00054A9C File Offset: 0x00052C9C
		public void CancelAfter(TimeSpan delay)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			this.CancelAfter((int)num);
		}

		/// <summary>Schedules a cancel operation on this <see cref="T:System.Threading.CancellationTokenSource" /> after the specified number of milliseconds.</summary>
		/// <param name="millisecondsDelay">The time span to wait before canceling this <see cref="T:System.Threading.CancellationTokenSource" />.</param>
		/// <exception cref="T:System.ObjectDisposedException">The exception thrown when this <see cref="T:System.Threading.CancellationTokenSource" /> has been disposed.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The exception thrown when <paramref name="millisecondsDelay" /> is less than -1.</exception>
		// Token: 0x06001509 RID: 5385 RVA: 0x00054AD4 File Offset: 0x00052CD4
		public void CancelAfter(int millisecondsDelay)
		{
			this.ThrowIfDisposed();
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			if (this.IsCancellationRequested)
			{
				return;
			}
			if (this._timer == null)
			{
				Timer timer = new Timer(CancellationTokenSource.s_timerCallback, this, -1, -1);
				if (Interlocked.CompareExchange<Timer>(ref this._timer, timer, null) != null)
				{
					timer.Dispose();
				}
			}
			try
			{
				this._timer.Change(millisecondsDelay, -1);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00054B54 File Offset: 0x00052D54
		private static void TimerCallbackLogic(object obj)
		{
			CancellationTokenSource cancellationTokenSource = (CancellationTokenSource)obj;
			if (!cancellationTokenSource.IsDisposed)
			{
				try
				{
					cancellationTokenSource.Cancel();
				}
				catch (ObjectDisposedException)
				{
					if (!cancellationTokenSource.IsDisposed)
					{
						throw;
					}
				}
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.CancellationTokenSource" /> class.</summary>
		/// <exception cref="T:System.ObjectDisposedException">A linked <see cref="T:System.Threading.CancellationTokenSource" /> has already been disposed.</exception>
		// Token: 0x0600150B RID: 5387 RVA: 0x00054B98 File Offset: 0x00052D98
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Threading.CancellationTokenSource" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600150C RID: 5388 RVA: 0x00054BA8 File Offset: 0x00052DA8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._disposed)
			{
				Timer timer = this._timer;
				if (timer != null)
				{
					timer.Dispose();
				}
				this._registeredCallbacksLists = null;
				if (this._kernelEvent != null)
				{
					ManualResetEvent manualResetEvent = Interlocked.Exchange<ManualResetEvent>(ref this._kernelEvent, null);
					if (manualResetEvent != null && this._state != 2)
					{
						manualResetEvent.Dispose();
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x00054C0E File Offset: 0x00052E0E
		internal void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				CancellationTokenSource.ThrowObjectDisposedException();
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00054C1D File Offset: 0x00052E1D
		private static void ThrowObjectDisposedException()
		{
			throw new ObjectDisposedException(null, "The CancellationTokenSource has been disposed.");
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x00054C2C File Offset: 0x00052E2C
		internal CancellationTokenRegistration InternalRegister(Action<object> callback, object stateForCallback, SynchronizationContext targetSyncContext, ExecutionContext executionContext)
		{
			if (!this.IsCancellationRequested)
			{
				if (this._disposed)
				{
					return default(CancellationTokenRegistration);
				}
				int num = Environment.CurrentManagedThreadId % CancellationTokenSource.s_nLists;
				CancellationCallbackInfo cancellationCallbackInfo = ((targetSyncContext != null) ? new CancellationCallbackInfo.WithSyncContext(callback, stateForCallback, executionContext, this, targetSyncContext) : new CancellationCallbackInfo(callback, stateForCallback, executionContext, this));
				SparselyPopulatedArray<CancellationCallbackInfo>[] array = this._registeredCallbacksLists;
				if (array == null)
				{
					SparselyPopulatedArray<CancellationCallbackInfo>[] array2 = new SparselyPopulatedArray<CancellationCallbackInfo>[CancellationTokenSource.s_nLists];
					array = Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>[]>(ref this._registeredCallbacksLists, array2, null);
					if (array == null)
					{
						array = array2;
					}
				}
				SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref array[num]);
				if (sparselyPopulatedArray == null)
				{
					SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray2 = new SparselyPopulatedArray<CancellationCallbackInfo>(4);
					Interlocked.CompareExchange<SparselyPopulatedArray<CancellationCallbackInfo>>(ref array[num], sparselyPopulatedArray2, null);
					sparselyPopulatedArray = array[num];
				}
				SparselyPopulatedArrayAddInfo<CancellationCallbackInfo> sparselyPopulatedArrayAddInfo = sparselyPopulatedArray.Add(cancellationCallbackInfo);
				CancellationTokenRegistration cancellationTokenRegistration = new CancellationTokenRegistration(cancellationCallbackInfo, sparselyPopulatedArrayAddInfo);
				if (!this.IsCancellationRequested)
				{
					return cancellationTokenRegistration;
				}
				if (!cancellationTokenRegistration.Unregister())
				{
					return cancellationTokenRegistration;
				}
			}
			callback(stateForCallback);
			return default(CancellationTokenRegistration);
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00054D14 File Offset: 0x00052F14
		private void NotifyCancellation(bool throwOnFirstException)
		{
			if (!this.IsCancellationRequested && Interlocked.CompareExchange(ref this._state, 2, 1) == 1)
			{
				Timer timer = this._timer;
				if (timer != null)
				{
					timer.Dispose();
				}
				this.ThreadIDExecutingCallbacks = Environment.CurrentManagedThreadId;
				ManualResetEvent kernelEvent = this._kernelEvent;
				if (kernelEvent != null)
				{
					kernelEvent.Set();
				}
				this.ExecuteCallbackHandlers(throwOnFirstException);
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x00054D74 File Offset: 0x00052F74
		private void ExecuteCallbackHandlers(bool throwOnFirstException)
		{
			LowLevelListWithIList<Exception> lowLevelListWithIList = null;
			SparselyPopulatedArray<CancellationCallbackInfo>[] registeredCallbacksLists = this._registeredCallbacksLists;
			if (registeredCallbacksLists == null)
			{
				Interlocked.Exchange(ref this._state, 3);
				return;
			}
			try
			{
				for (int i = 0; i < registeredCallbacksLists.Length; i++)
				{
					SparselyPopulatedArray<CancellationCallbackInfo> sparselyPopulatedArray = Volatile.Read<SparselyPopulatedArray<CancellationCallbackInfo>>(ref registeredCallbacksLists[i]);
					if (sparselyPopulatedArray != null)
					{
						for (SparselyPopulatedArrayFragment<CancellationCallbackInfo> sparselyPopulatedArrayFragment = sparselyPopulatedArray.Tail; sparselyPopulatedArrayFragment != null; sparselyPopulatedArrayFragment = sparselyPopulatedArrayFragment.Prev)
						{
							for (int j = sparselyPopulatedArrayFragment.Length - 1; j >= 0; j--)
							{
								this._executingCallback = sparselyPopulatedArrayFragment[j];
								if (this._executingCallback != null)
								{
									CancellationCallbackCoreWorkArguments cancellationCallbackCoreWorkArguments = new CancellationCallbackCoreWorkArguments(sparselyPopulatedArrayFragment, j);
									try
									{
										CancellationCallbackInfo.WithSyncContext withSyncContext = this._executingCallback as CancellationCallbackInfo.WithSyncContext;
										if (withSyncContext != null)
										{
											withSyncContext.TargetSyncContext.Send(new SendOrPostCallback(this.CancellationCallbackCoreWork_OnSyncContext), cancellationCallbackCoreWorkArguments);
											this.ThreadIDExecutingCallbacks = Environment.CurrentManagedThreadId;
										}
										else
										{
											this.CancellationCallbackCoreWork(cancellationCallbackCoreWorkArguments);
										}
									}
									catch (Exception ex)
									{
										if (throwOnFirstException)
										{
											throw;
										}
										if (lowLevelListWithIList == null)
										{
											lowLevelListWithIList = new LowLevelListWithIList<Exception>();
										}
										lowLevelListWithIList.Add(ex);
									}
								}
							}
						}
					}
				}
			}
			finally
			{
				this._state = 3;
				this._executingCallback = null;
				Interlocked.MemoryBarrier();
			}
			if (lowLevelListWithIList != null)
			{
				throw new AggregateException(lowLevelListWithIList);
			}
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00054ECC File Offset: 0x000530CC
		private void CancellationCallbackCoreWork_OnSyncContext(object obj)
		{
			this.CancellationCallbackCoreWork((CancellationCallbackCoreWorkArguments)obj);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00054EDC File Offset: 0x000530DC
		private void CancellationCallbackCoreWork(CancellationCallbackCoreWorkArguments args)
		{
			CancellationCallbackInfo cancellationCallbackInfo = args._currArrayFragment.SafeAtomicRemove(args._currArrayIndex, this._executingCallback);
			if (cancellationCallbackInfo == this._executingCallback)
			{
				cancellationCallbackInfo.CancellationTokenSource.ThreadIDExecutingCallbacks = Environment.CurrentManagedThreadId;
				cancellationCallbackInfo.ExecuteCallback();
			}
		}

		/// <summary>Creates a <see cref="T:System.Threading.CancellationTokenSource" /> that will be in the canceled state when any of the source tokens are in the canceled state.</summary>
		/// <returns>A <see cref="T:System.Threading.CancellationTokenSource" /> that is linked to the source tokens.</returns>
		/// <param name="token1">The first cancellation token to observe.</param>
		/// <param name="token2">The second cancellation token to observe.</param>
		/// <exception cref="T:System.ObjectDisposedException">A <see cref="T:System.Threading.CancellationTokenSource" /> associated with one of the source tokens has been disposed.</exception>
		// Token: 0x06001514 RID: 5396 RVA: 0x00054F24 File Offset: 0x00053124
		public static CancellationTokenSource CreateLinkedTokenSource(CancellationToken token1, CancellationToken token2)
		{
			if (!token1.CanBeCanceled)
			{
				return CancellationTokenSource.CreateLinkedTokenSource(token2);
			}
			if (!token2.CanBeCanceled)
			{
				return new CancellationTokenSource.Linked1CancellationTokenSource(token1);
			}
			return new CancellationTokenSource.Linked2CancellationTokenSource(token1, token2);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00054F4D File Offset: 0x0005314D
		internal static CancellationTokenSource CreateLinkedTokenSource(CancellationToken token)
		{
			if (!token.CanBeCanceled)
			{
				return new CancellationTokenSource();
			}
			return new CancellationTokenSource.Linked1CancellationTokenSource(token);
		}

		/// <summary>Creates a <see cref="T:System.Threading.CancellationTokenSource" /> that will be in the canceled state when any of the source tokens in the specified array are in the canceled state.</summary>
		/// <returns>A <see cref="T:System.Threading.CancellationTokenSource" /> that is linked to the source tokens.</returns>
		/// <param name="tokens">An array that contains the cancellation token instances to observe.</param>
		/// <exception cref="T:System.ObjectDisposedException">A <see cref="T:System.Threading.CancellationTokenSource" /> associated with one of the source tokens has been disposed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tokens" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tokens" /> is empty.</exception>
		// Token: 0x06001516 RID: 5398 RVA: 0x00054F64 File Offset: 0x00053164
		public static CancellationTokenSource CreateLinkedTokenSource(params CancellationToken[] tokens)
		{
			if (tokens == null)
			{
				throw new ArgumentNullException("tokens");
			}
			switch (tokens.Length)
			{
			case 0:
				throw new ArgumentException("No tokens were supplied.");
			case 1:
				return CancellationTokenSource.CreateLinkedTokenSource(tokens[0]);
			case 2:
				return CancellationTokenSource.CreateLinkedTokenSource(tokens[0], tokens[1]);
			default:
				return new CancellationTokenSource.LinkedNCancellationTokenSource(tokens);
			}
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x00054FCC File Offset: 0x000531CC
		internal void WaitForCallbackToComplete(CancellationCallbackInfo callbackInfo)
		{
			SpinWait spinWait = default(SpinWait);
			while (this.ExecutingCallback == callbackInfo)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x04000A43 RID: 2627
		internal static readonly CancellationTokenSource s_canceledSource = new CancellationTokenSource
		{
			_state = 3
		};

		// Token: 0x04000A44 RID: 2628
		internal static readonly CancellationTokenSource s_neverCanceledSource = new CancellationTokenSource
		{
			_state = 0
		};

		// Token: 0x04000A45 RID: 2629
		private static readonly int s_nLists = ((PlatformHelper.ProcessorCount > 24) ? 24 : PlatformHelper.ProcessorCount);

		// Token: 0x04000A46 RID: 2630
		private volatile ManualResetEvent _kernelEvent;

		// Token: 0x04000A47 RID: 2631
		private volatile SparselyPopulatedArray<CancellationCallbackInfo>[] _registeredCallbacksLists;

		// Token: 0x04000A48 RID: 2632
		private const int CannotBeCanceled = 0;

		// Token: 0x04000A49 RID: 2633
		private const int NotCanceledState = 1;

		// Token: 0x04000A4A RID: 2634
		private const int NotifyingState = 2;

		// Token: 0x04000A4B RID: 2635
		private const int NotifyingCompleteState = 3;

		// Token: 0x04000A4C RID: 2636
		private volatile int _state;

		// Token: 0x04000A4D RID: 2637
		private volatile int _threadIDExecutingCallbacks = -1;

		// Token: 0x04000A4E RID: 2638
		private bool _disposed;

		// Token: 0x04000A4F RID: 2639
		private volatile CancellationCallbackInfo _executingCallback;

		// Token: 0x04000A50 RID: 2640
		private volatile Timer _timer;

		// Token: 0x04000A51 RID: 2641
		private static readonly TimerCallback s_timerCallback = new TimerCallback(CancellationTokenSource.TimerCallbackLogic);

		// Token: 0x02000236 RID: 566
		private sealed class Linked1CancellationTokenSource : CancellationTokenSource
		{
			// Token: 0x06001519 RID: 5401 RVA: 0x0005504F File Offset: 0x0005324F
			internal Linked1CancellationTokenSource(CancellationToken token1)
			{
				this._reg1 = token1.InternalRegisterWithoutEC(CancellationTokenSource.LinkedNCancellationTokenSource.s_linkedTokenCancelDelegate, this);
			}

			// Token: 0x0600151A RID: 5402 RVA: 0x0005506A File Offset: 0x0005326A
			protected override void Dispose(bool disposing)
			{
				if (!disposing || this._disposed)
				{
					return;
				}
				this._reg1.Dispose();
				base.Dispose(disposing);
			}

			// Token: 0x04000A52 RID: 2642
			private readonly CancellationTokenRegistration _reg1;
		}

		// Token: 0x02000237 RID: 567
		private sealed class Linked2CancellationTokenSource : CancellationTokenSource
		{
			// Token: 0x0600151B RID: 5403 RVA: 0x0005508A File Offset: 0x0005328A
			internal Linked2CancellationTokenSource(CancellationToken token1, CancellationToken token2)
			{
				this._reg1 = token1.InternalRegisterWithoutEC(CancellationTokenSource.LinkedNCancellationTokenSource.s_linkedTokenCancelDelegate, this);
				this._reg2 = token2.InternalRegisterWithoutEC(CancellationTokenSource.LinkedNCancellationTokenSource.s_linkedTokenCancelDelegate, this);
			}

			// Token: 0x0600151C RID: 5404 RVA: 0x000550B8 File Offset: 0x000532B8
			protected override void Dispose(bool disposing)
			{
				if (!disposing || this._disposed)
				{
					return;
				}
				this._reg1.Dispose();
				this._reg2.Dispose();
				base.Dispose(disposing);
			}

			// Token: 0x04000A53 RID: 2643
			private readonly CancellationTokenRegistration _reg1;

			// Token: 0x04000A54 RID: 2644
			private readonly CancellationTokenRegistration _reg2;
		}

		// Token: 0x02000238 RID: 568
		private sealed class LinkedNCancellationTokenSource : CancellationTokenSource
		{
			// Token: 0x0600151D RID: 5405 RVA: 0x000550E4 File Offset: 0x000532E4
			internal LinkedNCancellationTokenSource(params CancellationToken[] tokens)
			{
				this._linkingRegistrations = new CancellationTokenRegistration[tokens.Length];
				for (int i = 0; i < tokens.Length; i++)
				{
					if (tokens[i].CanBeCanceled)
					{
						this._linkingRegistrations[i] = tokens[i].InternalRegisterWithoutEC(CancellationTokenSource.LinkedNCancellationTokenSource.s_linkedTokenCancelDelegate, this);
					}
				}
			}

			// Token: 0x0600151E RID: 5406 RVA: 0x00055140 File Offset: 0x00053340
			protected override void Dispose(bool disposing)
			{
				if (!disposing || this._disposed)
				{
					return;
				}
				CancellationTokenRegistration[] linkingRegistrations = this._linkingRegistrations;
				if (linkingRegistrations != null)
				{
					this._linkingRegistrations = null;
					for (int i = 0; i < linkingRegistrations.Length; i++)
					{
						linkingRegistrations[i].Dispose();
					}
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000A55 RID: 2645
			internal static readonly Action<object> s_linkedTokenCancelDelegate = delegate(object s)
			{
				((CancellationTokenSource)s).NotifyCancellation(false);
			};

			// Token: 0x04000A56 RID: 2646
			private CancellationTokenRegistration[] _linkingRegistrations;
		}
	}
}
