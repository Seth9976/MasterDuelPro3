using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200022E RID: 558
	internal sealed class ContinuationQueue
	{
		// Token: 0x06000C9D RID: 3229 RVA: 0x0002C074 File Offset: 0x0002A274
		public ContinuationQueue(PlayerLoopTiming timing)
		{
			this.timing = timing;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0002C0AC File Offset: 0x0002A2AC
		public void Enqueue(Action continuation)
		{
			bool lockTaken = false;
			try
			{
				this.gate.Enter(ref lockTaken);
				if (this.dequing)
				{
					if (this.waitingList.Length == this.waitingListCount)
					{
						int newLength = this.waitingListCount * 2;
						if (newLength > 2146435071)
						{
							newLength = 2146435071;
						}
						Action[] newArray = new Action[newLength];
						Array.Copy(this.waitingList, newArray, this.waitingListCount);
						this.waitingList = newArray;
					}
					this.waitingList[this.waitingListCount] = continuation;
					this.waitingListCount++;
				}
				else
				{
					if (this.actionList.Length == this.actionListCount)
					{
						int newLength2 = this.actionListCount * 2;
						if (newLength2 > 2146435071)
						{
							newLength2 = 2146435071;
						}
						Action[] newArray2 = new Action[newLength2];
						Array.Copy(this.actionList, newArray2, this.actionListCount);
						this.actionList = newArray2;
					}
					this.actionList[this.actionListCount] = continuation;
					this.actionListCount++;
				}
			}
			finally
			{
				if (lockTaken)
				{
					this.gate.Exit(false);
				}
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0002C1C0 File Offset: 0x0002A3C0
		public int Clear()
		{
			int num = this.actionListCount + this.waitingListCount;
			this.actionListCount = 0;
			this.actionList = new Action[16];
			this.waitingListCount = 0;
			this.waitingList = new Action[16];
			return num;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		public void Run()
		{
			this.RunCore();
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void Initialization()
		{
			this.RunCore();
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void LastInitialization()
		{
			this.RunCore();
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void EarlyUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void LastEarlyUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void FixedUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void LastFixedUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void PreUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void LastPreUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void Update()
		{
			this.RunCore();
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void LastUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void PreLateUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void LastPreLateUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void PostLateUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void LastPostLateUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void TimeUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002C1F7 File Offset: 0x0002A3F7
		private void LastTimeUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002C200 File Offset: 0x0002A400
		[DebuggerHidden]
		private void RunCore()
		{
			bool lockTaken = false;
			try
			{
				this.gate.Enter(ref lockTaken);
				if (this.actionListCount == 0)
				{
					return;
				}
				this.dequing = true;
			}
			finally
			{
				if (lockTaken)
				{
					this.gate.Exit(false);
				}
			}
			for (int i = 0; i < this.actionListCount; i++)
			{
				Action action = this.actionList[i];
				this.actionList[i] = null;
				try
				{
					action();
				}
				catch (Exception ex)
				{
					global::UnityEngine.Debug.LogException(ex);
				}
			}
			bool lockTaken2 = false;
			try
			{
				this.gate.Enter(ref lockTaken2);
				this.dequing = false;
				Action[] swapTempActionList = this.actionList;
				this.actionListCount = this.waitingListCount;
				this.actionList = this.waitingList;
				this.waitingListCount = 0;
				this.waitingList = swapTempActionList;
			}
			finally
			{
				if (lockTaken2)
				{
					this.gate.Exit(false);
				}
			}
		}

		// Token: 0x0400065D RID: 1629
		private const int MaxArrayLength = 2146435071;

		// Token: 0x0400065E RID: 1630
		private const int InitialSize = 16;

		// Token: 0x0400065F RID: 1631
		private readonly PlayerLoopTiming timing;

		// Token: 0x04000660 RID: 1632
		private SpinLock gate = new SpinLock(false);

		// Token: 0x04000661 RID: 1633
		private bool dequing;

		// Token: 0x04000662 RID: 1634
		private int actionListCount;

		// Token: 0x04000663 RID: 1635
		private Action[] actionList = new Action[16];

		// Token: 0x04000664 RID: 1636
		private int waitingListCount;

		// Token: 0x04000665 RID: 1637
		private Action[] waitingList = new Action[16];
	}
}
