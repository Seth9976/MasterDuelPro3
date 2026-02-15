using System;
using System.Threading.Tasks;

namespace System.Runtime
{
	// Token: 0x02000021 RID: 33
	internal static class TaskExtensions
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00003844 File Offset: 0x00001A44
		public static IAsyncResult AsAsyncResult(this Task task, AsyncCallback callback, object state)
		{
			if (task == null)
			{
				throw Fx.Exception.ArgumentNull("task");
			}
			if (task.Status == TaskStatus.Created)
			{
				throw Fx.Exception.AsError(new InvalidOperationException("SFx Task Not Started"));
			}
			TaskCompletionSource<object> tcs = new TaskCompletionSource<object>(state);
			task.ContinueWith(delegate(Task t)
			{
				if (t.IsFaulted)
				{
					tcs.TrySetException(t.Exception.InnerExceptions);
				}
				else if (t.IsCanceled)
				{
					tcs.TrySetCanceled();
				}
				else
				{
					tcs.TrySetResult(null);
				}
				if (callback != null)
				{
					callback(tcs.Task);
				}
			}, TaskContinuationOptions.ExecuteSynchronously);
			return tcs.Task;
		}
	}
}
