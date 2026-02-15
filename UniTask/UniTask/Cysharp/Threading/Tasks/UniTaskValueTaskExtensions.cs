using System;
using System.Threading.Tasks;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000075 RID: 117
	public static class UniTaskValueTaskExtensions
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00005EAD File Offset: 0x000040AD
		public static ValueTask AsValueTask(this UniTask task)
		{
			return in task;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005EB5 File Offset: 0x000040B5
		public static ValueTask<T> AsValueTask<T>(this UniTask<T> task)
		{
			return in task;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00005EC0 File Offset: 0x000040C0
		public static async UniTask<T> AsUniTask<T>(this ValueTask<T> task)
		{
			return await task;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00005F04 File Offset: 0x00004104
		public static async UniTask AsUniTask(this ValueTask task)
		{
			await task;
		}
	}
}
