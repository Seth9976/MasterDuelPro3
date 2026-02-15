using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	/// <summary>Provides an awaitable object that enables configured awaits on a task.</summary>
	// Token: 0x020005A1 RID: 1441
	public readonly struct ConfiguredTaskAwaitable
	{
		// Token: 0x06002B30 RID: 11056 RVA: 0x000AB111 File Offset: 0x000A9311
		internal ConfiguredTaskAwaitable(Task task, bool continueOnCapturedContext)
		{
			this.m_configuredTaskAwaiter = new ConfiguredTaskAwaitable.ConfiguredTaskAwaiter(task, continueOnCapturedContext);
		}

		/// <summary>Returns an awaiter for this awaitable object.</summary>
		/// <returns>The awaiter.</returns>
		// Token: 0x06002B31 RID: 11057 RVA: 0x000AB120 File Offset: 0x000A9320
		public ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter()
		{
			return this.m_configuredTaskAwaiter;
		}

		// Token: 0x040015E0 RID: 5600
		private readonly ConfiguredTaskAwaitable.ConfiguredTaskAwaiter m_configuredTaskAwaiter;

		/// <summary>Provides an awaiter for an awaitable (<see cref="T:System.Runtime.CompilerServices.ConfiguredTaskAwaitable" />) object.</summary>
		// Token: 0x020005A2 RID: 1442
		public readonly struct ConfiguredTaskAwaiter : ICriticalNotifyCompletion, INotifyCompletion
		{
			// Token: 0x06002B32 RID: 11058 RVA: 0x000AB128 File Offset: 0x000A9328
			internal ConfiguredTaskAwaiter(Task task, bool continueOnCapturedContext)
			{
				this.m_task = task;
				this.m_continueOnCapturedContext = continueOnCapturedContext;
			}

			/// <summary>Gets a value that specifies whether the task being awaited is completed.</summary>
			/// <returns>true if the task being awaited is completed; otherwise, false.</returns>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			// Token: 0x17000583 RID: 1411
			// (get) Token: 0x06002B33 RID: 11059 RVA: 0x000AB138 File Offset: 0x000A9338
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
			// Token: 0x06002B34 RID: 11060 RVA: 0x000AB145 File Offset: 0x000A9345
			public void OnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, true);
			}

			/// <summary>Schedules the continuation action for the task associated with this awaiter. </summary>
			/// <param name="continuation">The action to invoke when the await operation completes.</param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="continuation" /> argument is null.</exception>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			// Token: 0x06002B35 RID: 11061 RVA: 0x000AB15A File Offset: 0x000A935A
			public void UnsafeOnCompleted(Action continuation)
			{
				TaskAwaiter.OnCompletedInternal(this.m_task, continuation, this.m_continueOnCapturedContext, false);
			}

			/// <summary>Ends the await on the completed task.</summary>
			/// <exception cref="T:System.NullReferenceException">The awaiter was not properly initialized.</exception>
			/// <exception cref="T:System.Threading.Tasks.TaskCanceledException">The task was canceled.</exception>
			/// <exception cref="T:System.Exception">The task completed in a faulted state.</exception>
			// Token: 0x06002B36 RID: 11062 RVA: 0x000AB16F File Offset: 0x000A936F
			[StackTraceHidden]
			public void GetResult()
			{
				TaskAwaiter.ValidateEnd(this.m_task);
			}

			// Token: 0x040015E1 RID: 5601
			internal readonly Task m_task;

			// Token: 0x040015E2 RID: 5602
			internal readonly bool m_continueOnCapturedContext;
		}
	}
}
