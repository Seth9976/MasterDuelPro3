using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000299 RID: 665
	internal static class TaskToApm
	{
		// Token: 0x0600185F RID: 6239 RVA: 0x0005D6AC File Offset: 0x0005B8AC
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

		// Token: 0x06001860 RID: 6240 RVA: 0x0005D6FC File Offset: 0x0005B8FC
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

		// Token: 0x06001861 RID: 6241 RVA: 0x0005D73C File Offset: 0x0005B93C
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

		// Token: 0x06001862 RID: 6242 RVA: 0x0005D780 File Offset: 0x0005B980
		private static void InvokeCallbackWhenTaskCompletes(Task antecedent, AsyncCallback callback, IAsyncResult asyncResult)
		{
			antecedent.ConfigureAwait(false).GetAwaiter().OnCompleted(delegate
			{
				callback(asyncResult);
			});
		}

		// Token: 0x0200029A RID: 666
		private sealed class TaskWrapperAsyncResult : IAsyncResult
		{
			// Token: 0x06001863 RID: 6243 RVA: 0x0005D7C4 File Offset: 0x0005B9C4
			internal TaskWrapperAsyncResult(Task task, object state, bool completedSynchronously)
			{
				this.Task = task;
				this._state = state;
				this._completedSynchronously = completedSynchronously;
			}

			// Token: 0x1700028A RID: 650
			// (get) Token: 0x06001864 RID: 6244 RVA: 0x0005D7E1 File Offset: 0x0005B9E1
			object IAsyncResult.AsyncState
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x1700028B RID: 651
			// (get) Token: 0x06001865 RID: 6245 RVA: 0x0005D7E9 File Offset: 0x0005B9E9
			bool IAsyncResult.CompletedSynchronously
			{
				get
				{
					return this._completedSynchronously;
				}
			}

			// Token: 0x1700028C RID: 652
			// (get) Token: 0x06001866 RID: 6246 RVA: 0x0005D7F1 File Offset: 0x0005B9F1
			bool IAsyncResult.IsCompleted
			{
				get
				{
					return this.Task.IsCompleted;
				}
			}

			// Token: 0x1700028D RID: 653
			// (get) Token: 0x06001867 RID: 6247 RVA: 0x0005D7FE File Offset: 0x0005B9FE
			WaitHandle IAsyncResult.AsyncWaitHandle
			{
				get
				{
					return ((IAsyncResult)this.Task).AsyncWaitHandle;
				}
			}

			// Token: 0x04000B93 RID: 2963
			internal readonly Task Task;

			// Token: 0x04000B94 RID: 2964
			private readonly object _state;

			// Token: 0x04000B95 RID: 2965
			private readonly bool _completedSynchronously;
		}
	}
}
