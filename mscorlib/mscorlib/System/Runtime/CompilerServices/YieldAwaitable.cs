using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides the context for waiting when asynchronously switching into a target environment.</summary>
	// Token: 0x020005AF RID: 1455
	public readonly struct YieldAwaitable
	{
		/// <summary>Retrieves a <see cref="T:System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter" /> object  for this instance of the class.</summary>
		/// <returns>The object that is used to monitor the completion of an asynchronous operation.</returns>
		// Token: 0x06002B6E RID: 11118 RVA: 0x000ABD7C File Offset: 0x000A9F7C
		public YieldAwaitable.YieldAwaiter GetAwaiter()
		{
			return default(YieldAwaitable.YieldAwaiter);
		}

		/// <summary>Provides an awaiter for switching into a target environment.</summary>
		// Token: 0x020005B0 RID: 1456
		public readonly struct YieldAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			/// <summary>Gets a value that indicates whether a yield is not required.</summary>
			/// <returns>Always false, which indicates that a yield is always required for <see cref="T:System.Runtime.CompilerServices.YieldAwaitable.YieldAwaiter" />.</returns>
			// Token: 0x17000588 RID: 1416
			// (get) Token: 0x06002B6F RID: 11119 RVA: 0x00033991 File Offset: 0x00031B91
			public bool IsCompleted
			{
				get
				{
					return false;
				}
			}

			/// <summary>Sets the continuation to invoke.</summary>
			/// <param name="continuation">The action to invoke asynchronously.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="continuation" /> is null.</exception>
			// Token: 0x06002B70 RID: 11120 RVA: 0x000ABD92 File Offset: 0x000A9F92
			public void OnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, true);
			}

			/// <summary>Posts the <paramref name="continuation" /> back to the current context.</summary>
			/// <param name="continuation">The action to invoke asynchronously.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is null.</exception>
			// Token: 0x06002B71 RID: 11121 RVA: 0x000ABD9B File Offset: 0x000A9F9B
			public void UnsafeOnCompleted(Action continuation)
			{
				YieldAwaitable.YieldAwaiter.QueueContinuation(continuation, false);
			}

			// Token: 0x06002B72 RID: 11122 RVA: 0x000ABDA4 File Offset: 0x000A9FA4
			private static void QueueContinuation(Action continuation, bool flowContext)
			{
				if (continuation == null)
				{
					throw new ArgumentNullException("continuation");
				}
				SynchronizationContext currentNoFlow = SynchronizationContext.CurrentNoFlow;
				if (currentNoFlow != null && currentNoFlow.GetType() != typeof(SynchronizationContext))
				{
					currentNoFlow.Post(YieldAwaitable.YieldAwaiter.s_sendOrPostCallbackRunAction, continuation);
					return;
				}
				TaskScheduler taskScheduler = TaskScheduler.Current;
				if (taskScheduler != TaskScheduler.Default)
				{
					Task.Factory.StartNew(continuation, default(CancellationToken), TaskCreationOptions.PreferFairness, taskScheduler);
					return;
				}
				if (flowContext)
				{
					ThreadPool.QueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
					return;
				}
				ThreadPool.UnsafeQueueUserWorkItem(YieldAwaitable.YieldAwaiter.s_waitCallbackRunAction, continuation);
			}

			// Token: 0x06002B73 RID: 11123 RVA: 0x00061606 File Offset: 0x0005F806
			private static void RunAction(object state)
			{
				((Action)state)();
			}

			/// <summary>Ends the await operation.</summary>
			// Token: 0x06002B74 RID: 11124 RVA: 0x00002C89 File Offset: 0x00000E89
			public void GetResult()
			{
			}

			// Token: 0x040015FE RID: 5630
			private static readonly WaitCallback s_waitCallbackRunAction = new WaitCallback(YieldAwaitable.YieldAwaiter.RunAction);

			// Token: 0x040015FF RID: 5631
			private static readonly SendOrPostCallback s_sendOrPostCallbackRunAction = new SendOrPostCallback(YieldAwaitable.YieldAwaiter.RunAction);
		}
	}
}
