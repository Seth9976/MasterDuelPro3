using System;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks
{
	// Token: 0x02000154 RID: 340
	public class UniTaskSynchronizationContext : SynchronizationContext
	{
		// Token: 0x060007E5 RID: 2021 RVA: 0x00025E03 File Offset: 0x00024003
		public override void Send(SendOrPostCallback d, object state)
		{
			d(state);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00025E0C File Offset: 0x0002400C
		public override void Post(SendOrPostCallback d, object state)
		{
			bool lockTaken = false;
			try
			{
				UniTaskSynchronizationContext.gate.Enter(ref lockTaken);
				if (UniTaskSynchronizationContext.dequing)
				{
					if (UniTaskSynchronizationContext.waitingList.Length == UniTaskSynchronizationContext.waitingListCount)
					{
						int newLength = UniTaskSynchronizationContext.waitingListCount * 2;
						if (newLength > 2146435071)
						{
							newLength = 2146435071;
						}
						UniTaskSynchronizationContext.Callback[] newArray = new UniTaskSynchronizationContext.Callback[newLength];
						Array.Copy(UniTaskSynchronizationContext.waitingList, newArray, UniTaskSynchronizationContext.waitingListCount);
						UniTaskSynchronizationContext.waitingList = newArray;
					}
					UniTaskSynchronizationContext.waitingList[UniTaskSynchronizationContext.waitingListCount] = new UniTaskSynchronizationContext.Callback(d, state);
					UniTaskSynchronizationContext.waitingListCount++;
				}
				else
				{
					if (UniTaskSynchronizationContext.actionList.Length == UniTaskSynchronizationContext.actionListCount)
					{
						int newLength2 = UniTaskSynchronizationContext.actionListCount * 2;
						if (newLength2 > 2146435071)
						{
							newLength2 = 2146435071;
						}
						UniTaskSynchronizationContext.Callback[] newArray2 = new UniTaskSynchronizationContext.Callback[newLength2];
						Array.Copy(UniTaskSynchronizationContext.actionList, newArray2, UniTaskSynchronizationContext.actionListCount);
						UniTaskSynchronizationContext.actionList = newArray2;
					}
					UniTaskSynchronizationContext.actionList[UniTaskSynchronizationContext.actionListCount] = new UniTaskSynchronizationContext.Callback(d, state);
					UniTaskSynchronizationContext.actionListCount++;
				}
			}
			finally
			{
				if (lockTaken)
				{
					UniTaskSynchronizationContext.gate.Exit(false);
				}
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00025F1C File Offset: 0x0002411C
		public override void OperationStarted()
		{
			Interlocked.Increment(ref UniTaskSynchronizationContext.opCount);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00025F29 File Offset: 0x00024129
		public override void OperationCompleted()
		{
			Interlocked.Decrement(ref UniTaskSynchronizationContext.opCount);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00025F36 File Offset: 0x00024136
		public override SynchronizationContext CreateCopy()
		{
			return this;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00025F3C File Offset: 0x0002413C
		internal static void Run()
		{
			bool lockTaken = false;
			try
			{
				UniTaskSynchronizationContext.gate.Enter(ref lockTaken);
				if (UniTaskSynchronizationContext.actionListCount == 0)
				{
					return;
				}
				UniTaskSynchronizationContext.dequing = true;
			}
			finally
			{
				if (lockTaken)
				{
					UniTaskSynchronizationContext.gate.Exit(false);
				}
			}
			for (int i = 0; i < UniTaskSynchronizationContext.actionListCount; i++)
			{
				UniTaskSynchronizationContext.Callback action = UniTaskSynchronizationContext.actionList[i];
				UniTaskSynchronizationContext.actionList[i] = default(UniTaskSynchronizationContext.Callback);
				action.Invoke();
			}
			bool lockTaken2 = false;
			try
			{
				UniTaskSynchronizationContext.gate.Enter(ref lockTaken2);
				UniTaskSynchronizationContext.dequing = false;
				UniTaskSynchronizationContext.Callback[] array = UniTaskSynchronizationContext.actionList;
				UniTaskSynchronizationContext.actionListCount = UniTaskSynchronizationContext.waitingListCount;
				UniTaskSynchronizationContext.actionList = UniTaskSynchronizationContext.waitingList;
				UniTaskSynchronizationContext.waitingListCount = 0;
				UniTaskSynchronizationContext.waitingList = array;
			}
			finally
			{
				if (lockTaken2)
				{
					UniTaskSynchronizationContext.gate.Exit(false);
				}
			}
		}

		// Token: 0x04000547 RID: 1351
		private const int MaxArrayLength = 2146435071;

		// Token: 0x04000548 RID: 1352
		private const int InitialSize = 16;

		// Token: 0x04000549 RID: 1353
		private static SpinLock gate = new SpinLock(false);

		// Token: 0x0400054A RID: 1354
		private static bool dequing = false;

		// Token: 0x0400054B RID: 1355
		private static int actionListCount = 0;

		// Token: 0x0400054C RID: 1356
		private static UniTaskSynchronizationContext.Callback[] actionList = new UniTaskSynchronizationContext.Callback[16];

		// Token: 0x0400054D RID: 1357
		private static int waitingListCount = 0;

		// Token: 0x0400054E RID: 1358
		private static UniTaskSynchronizationContext.Callback[] waitingList = new UniTaskSynchronizationContext.Callback[16];

		// Token: 0x0400054F RID: 1359
		private static int opCount;

		// Token: 0x02000155 RID: 341
		[StructLayout(LayoutKind.Auto)]
		private readonly struct Callback
		{
			// Token: 0x060007ED RID: 2029 RVA: 0x00026053 File Offset: 0x00024253
			public Callback(SendOrPostCallback callback, object state)
			{
				this.callback = callback;
				this.state = state;
			}

			// Token: 0x060007EE RID: 2030 RVA: 0x00026064 File Offset: 0x00024264
			public void Invoke()
			{
				try
				{
					this.callback(this.state);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}

			// Token: 0x04000550 RID: 1360
			private readonly SendOrPostCallback callback;

			// Token: 0x04000551 RID: 1361
			private readonly object state;
		}
	}
}
