using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002A2 RID: 674
	internal static class TaskAwaiters
	{
		// Token: 0x06001892 RID: 6290 RVA: 0x0005DFA8 File Offset: 0x0005C1A8
		public static ForceAsyncAwaiter ForceAsync(this Task task)
		{
			return new ForceAsyncAwaiter(task);
		}
	}
}
