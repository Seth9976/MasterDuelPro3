using System;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000182 RID: 386
	public static class UnityAwaitableExtensions
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x0002918C File Offset: 0x0002738C
		public static async UniTask AsUniTask(this Awaitable awaitable)
		{
			await awaitable;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x000291D0 File Offset: 0x000273D0
		public static async UniTask<T> AsUniTask<T>(this Awaitable<T> awaitable)
		{
			return await awaitable;
		}
	}
}
