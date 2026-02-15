using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MDPro3.Utility
{
	// Token: 0x020012C2 RID: 4802
	public static class TaskUtility
	{
		// Token: 0x06008C8D RID: 35981 RVA: 0x00124678 File Offset: 0x00122878
		public static async Task WaitWhile(Func<bool> condition)
		{
			while (condition())
			{
				await TaskUtility.WaitOneFrame();
			}
		}

		// Token: 0x06008C8E RID: 35982 RVA: 0x001246BC File Offset: 0x001228BC
		public static async Task WaitUntil(Func<bool> condition)
		{
			while (!condition())
			{
				await TaskUtility.WaitOneFrame();
			}
		}

		// Token: 0x06008C8F RID: 35983 RVA: 0x00124700 File Offset: 0x00122900
		public static async Task WaitOneFrame()
		{
			await Task.Yield();
			if (!Application.isPlaying)
			{
				throw new OperationCanceledException();
			}
		}

		// Token: 0x06008C90 RID: 35984 RVA: 0x0012473C File Offset: 0x0012293C
		public static async Task WaitOneFrame(GameObject gameObject)
		{
			await Task.Yield();
			if (!Application.isPlaying || gameObject == null)
			{
				throw new OperationCanceledException();
			}
		}

		// Token: 0x06008C91 RID: 35985 RVA: 0x00124780 File Offset: 0x00122980
		public static async Task WaitOneFrame(CancellationToken token)
		{
			await Task.Yield();
			if (!Application.isPlaying)
			{
				throw new OperationCanceledException();
			}
			token.ThrowIfCancellationRequested();
		}

		// Token: 0x06008C92 RID: 35986 RVA: 0x001247C4 File Offset: 0x001229C4
		public static async Task WaitOneFrame(GameObject gameObject, CancellationToken token)
		{
			await Task.Yield();
			if (!Application.isPlaying || gameObject == null)
			{
				throw new OperationCanceledException();
			}
			token.ThrowIfCancellationRequested();
		}
	}
}
