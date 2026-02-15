using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000030 RID: 48
	internal static class TaskToApm
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00008460 File Offset: 0x00006660
		public static IAsyncResult Begin(Task task, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult;
			if (task.IsCompleted)
			{
				asyncResult = new TaskToApm.TaskWrapperAsyncResult(task, state, true);
				if (callback != null)
				{
					callback(asyncResult);
				}
			}
			else
			{
				IAsyncResult asyncResult3;
				if (task.AsyncState != state)
				{
					IAsyncResult asyncResult2 = new TaskToApm.TaskWrapperAsyncResult(task, state, false);
					asyncResult3 = asyncResult2;
				}
				else
				{
					asyncResult3 = task;
				}
				asyncResult = asyncResult3;
				if (callback != null)
				{
					TaskToApm.InvokeCallbackWhenTaskCompletes(task, callback, asyncResult);
				}
			}
			return asyncResult;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000084B0 File Offset: 0x000066B0
		public static void End(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task;
			}
			else
			{
				task = asyncResult as Task;
			}
			if (task == null)
			{
				throw new ArgumentNullException();
			}
			task.GetAwaiter().GetResult();
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000084F0 File Offset: 0x000066F0
		public static TResult End<TResult>(IAsyncResult asyncResult)
		{
			TaskToApm.TaskWrapperAsyncResult taskWrapperAsyncResult = asyncResult as TaskToApm.TaskWrapperAsyncResult;
			Task<TResult> task;
			if (taskWrapperAsyncResult != null)
			{
				task = taskWrapperAsyncResult.Task as Task<TResult>;
			}
			else
			{
				task = asyncResult as Task<TResult>;
			}
			if (task == null)
			{
				throw new ArgumentNullException();
			}
			return task.GetAwaiter().GetResult();
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00008534 File Offset: 0x00006734
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x02000031 RID: 49
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x06000164 RID: 356 RVA: 0x00008578 File Offset: 0x00006778
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this._state = state;
				this._completedSynchronously = completedSynchronously;
			}

			// Token: 0x1700004B RID: 75
			// (get) Token: 0x06000165 RID: 357 RVA: 0x00008595 File Offset: 0x00006795
			object IAsyncResult.AsyncState
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x06000166 RID: 358 RVA: 0x0000859D File Offset: 0x0000679D
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this._completedSynchronously;
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x06000167 RID: 359 RVA: 0x000085A5 File Offset: 0x000067A5
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x06000168 RID: 360 RVA: 0x000085B2 File Offset: 0x000067B2
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x04000135 RID: 309
			internal readonly Task Task;

			// Token: 0x04000136 RID: 310
			private readonly object _state;

			// Token: 0x04000137 RID: 311
			private readonly bool _completedSynchronously;
		}
	}
}
