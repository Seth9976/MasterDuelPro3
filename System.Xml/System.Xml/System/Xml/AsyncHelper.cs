using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x02000007 RID: 7
	internal static class AsyncHelper
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000213E File Offset: 0x0000033E
		public static bool IsSuccess(this Task task)
		{
			return task.IsCompleted && task.Exception == null;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002153 File Offset: 0x00000353
		public static Task CallVoidFuncWhenFinish(this Task task, Action func)
		{
			if (task.IsSuccess())
			{
				func();
				return AsyncHelper.DoneTask;
			}
			return task._CallVoidFuncWhenFinish(func);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002170 File Offset: 0x00000370
		private static async Task _CallVoidFuncWhenFinish(this Task task, Action func)
		{
			await task.ConfigureAwait(false);
			func();
		}

		// Token: 0x04000007 RID: 7
		public static readonly Task DoneTask = Task.FromResult<bool>(true);

		// Token: 0x04000008 RID: 8
		public static readonly Task<bool> DoneTaskTrue = Task.FromResult<bool>(true);

		// Token: 0x04000009 RID: 9
		public static readonly Task<bool> DoneTaskFalse = Task.FromResult<bool>(false);

		// Token: 0x0400000A RID: 10
		public static readonly Task<int> DoneTaskZero = Task.FromResult<int>(0);
	}
}
