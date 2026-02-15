using System;
using System.Threading;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000005 RID: 5
	public class AsyncLazy
	{
		// Token: 0x06000005 RID: 5 RVA: 0x000020DA File Offset: 0x000002DA
		public AsyncLazy(Func<UniTask> taskFactory)
		{
			this.taskFactory = taskFactory;
			this.completionSource = new UniTaskCompletionSource();
			this.syncLock = new object();
			this.initialized = false;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002108 File Offset: 0x00000308
		internal AsyncLazy(UniTask task)
		{
			this.taskFactory = null;
			this.completionSource = new UniTaskCompletionSource();
			this.syncLock = null;
			this.initialized = true;
			UniTask.Awaiter awaiter = task.GetAwaiter();
			if (awaiter.IsCompleted)
			{
				this.SetCompletionSource(in awaiter);
				return;
			}
			this.awaiter = awaiter;
			awaiter.SourceOnCompleted(AsyncLazy.continuation, this);
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002169 File Offset: 0x00000369
		public UniTask Task
		{
			get
			{
				this.EnsureInitialized();
				return this.completionSource.Task;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000217C File Offset: 0x0000037C
		public UniTask.Awaiter GetAwaiter()
		{
			return this.Task.GetAwaiter();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002197 File Offset: 0x00000397
		private void EnsureInitialized()
		{
			if (Volatile.Read(ref this.initialized))
			{
				return;
			}
			this.EnsureInitializedCore();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021B0 File Offset: 0x000003B0
		private void EnsureInitializedCore()
		{
			object obj = this.syncLock;
			lock (obj)
			{
				if (!Volatile.Read(ref this.initialized))
				{
					Func<UniTask> f = Interlocked.Exchange<Func<UniTask>>(ref this.taskFactory, null);
					if (f != null)
					{
						UniTask.Awaiter awaiter = f().GetAwaiter();
						if (awaiter.IsCompleted)
						{
							this.SetCompletionSource(in awaiter);
						}
						else
						{
							this.awaiter = awaiter;
							awaiter.SourceOnCompleted(AsyncLazy.continuation, this);
						}
						Volatile.Write(ref this.initialized, true);
					}
				}
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000224C File Offset: 0x0000044C
		private void SetCompletionSource(in UniTask.Awaiter awaiter)
		{
			try
			{
				awaiter.GetResult();
				this.completionSource.TrySetResult();
			}
			catch (Exception ex)
			{
				this.completionSource.TrySetException(ex);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002290 File Offset: 0x00000490
		private static void SetCompletionSource(object state)
		{
			AsyncLazy self = (AsyncLazy)state;
			try
			{
				self.awaiter.GetResult();
				self.completionSource.TrySetResult();
			}
			catch (Exception ex)
			{
				self.completionSource.TrySetException(ex);
			}
			finally
			{
				self.awaiter = default(UniTask.Awaiter);
			}
		}

		// Token: 0x04000007 RID: 7
		private static Action<object> continuation = new Action<object>(AsyncLazy.SetCompletionSource);

		// Token: 0x04000008 RID: 8
		private Func<UniTask> taskFactory;

		// Token: 0x04000009 RID: 9
		private UniTaskCompletionSource completionSource;

		// Token: 0x0400000A RID: 10
		private UniTask.Awaiter awaiter;

		// Token: 0x0400000B RID: 11
		private object syncLock;

		// Token: 0x0400000C RID: 12
		private bool initialized;
	}
}
