using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x020002A3 RID: 675
	internal readonly struct ForceAsyncAwaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x06001893 RID: 6291 RVA: 0x0005DFB0 File Offset: 0x0005C1B0
		internal ForceAsyncAwaiter(Task task)
		{
			this._task = task;
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0005DFB9 File Offset: 0x0005C1B9
		public ForceAsyncAwaiter GetAwaiter()
		{
			return this;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x00033991 File Offset: 0x00031B91
		public bool IsCompleted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x0005DFC4 File Offset: 0x0005C1C4
		public void GetResult()
		{
			this._task.GetAwaiter().GetResult();
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x0005DFE4 File Offset: 0x0005C1E4
		public void OnCompleted(Action action)
		{
			this._task.ConfigureAwait(false).GetAwaiter().OnCompleted(action);
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x0005E010 File Offset: 0x0005C210
		public void UnsafeOnCompleted(Action action)
		{
			this._task.ConfigureAwait(false).GetAwaiter().UnsafeOnCompleted(action);
		}

		// Token: 0x04000BA9 RID: 2985
		private readonly Task _task;
	}
}
