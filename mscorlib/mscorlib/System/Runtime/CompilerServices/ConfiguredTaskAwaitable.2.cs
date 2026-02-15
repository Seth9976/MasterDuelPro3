using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides an awaitable object that enables configured awaits on a task.</summary>
	/// <typeparam name="TResult">The type of the result produced by this <see cref="T:System.Threading.Tasks.Task`1" />.</typeparam>
	// Token: 0x020005A3 RID: 1443
	public readonly struct ConfiguredTaskAwaitable<TResult>
	{
		// Token: 0x06002B37 RID: 11063 RVA: 0x000AB17C File Offset: 0x000A937C
		internal ConfiguredTaskAwaitable(Task<TResult> task, bool continueOnCapturedContext)
		{
			this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
		}

		/// <summary>Returns an awaiter for this awaitable object.</summary>
		/// <returns>The awaiter.</returns>
		// Token: 0x06002B38 RID: 11064 RVA: 0x000AB18B File Offset: 0x000A938B
		public ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter GetAwaiter()
		{
			return this.m_configuredTaskAwaiter;
		}

		// Token: 0x040015E3 RID: 5603
		private readonly ConfiguredTaskAwaitable<TResult>.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

		/// <summary>Provides an awaiter for an awaitable object(<see cref="T:System.Runtime.CompilerServices.ConfiguredTaskAwaitable`1" />).</summary>
		// Token: 0x020005A4 RID: 1444
		public readonly struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06002B39 RID: 11065 RVA: 0x000AB193 File Offset: 0x000A9393
			internal ConfiguredTaskAwaiter(Task<TResult> task, bool continueOnCapturedContext)
			{
				this.m_task = task;
				this.m_continueOnCapturedContext = continueOnCapturedContext;
			}

			/// <summary>Gets a value that specifies whether the task being awaited has been completed.</summary>
			/// <returns>true if the task being awaited has been completed; otherwise, false.</returns>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			// Token: 0x17000584 RID: 1412
			// (get) Token: 0x06002B3A RID: 11066 RVA: 0x000AB1A3 File Offset: 0x000A93A3
			public bool IsCompleted
			{
				get
				{
					return this.m_task.IsCompleted;
				}
			}

			/// <summary>Schedules the continuation action for the task associated with this awaiter.</summary>
			/// <param name="continuation">The action to invoke when the await operation completes.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is null.</exception>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			// Token: 0x06002B3B RID: 11067 RVA: 0x000AB1B0 File Offset: 0x000A93B0
			public void OnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
			}

			/// <summary>Schedules the continuation action for the task associated with this awaiter. </summary>
			/// <param name="continuation">The action to invoke when the await operation completes.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is null.</exception>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			// Token: 0x06002B3C RID: 11068 RVA: 0x000AB1C5 File Offset: 0x000A93C5
			public void UnsafeOnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
			}

			/// <summary>Ends the await on the completed task.</summary>
			/// <returns>The result of the completed task.</returns>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task was canceled.</exception>
			/// <exception cref="T:System.Exception">The task completed in a faulted state.</exception>
			// Token: 0x06002B3D RID: 11069 RVA: 0x000AB1DA File Offset: 0x000A93DA
			[StackTraceHidden]
			public TResult GetResult()
			{
				TaskAwaiter.ValidateEnd(this.m_task);
				return this.m_task.ResultOnSuccess;
			}

			// Token: 0x040015E4 RID: 5604
			private readonly Task<TResult> m_task;

			// Token: 0x040015E5 RID: 5605
			private readonly bool m_continueOnCapturedContext;
		}
	}
}
