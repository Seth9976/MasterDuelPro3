using System;
using System.Diagnostics;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x02000233 RID: 563
	internal sealed class PlayerLoopRunner
	{
		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002CD30 File Offset: 0x0002AF30
		public PlayerLoopRunner(PlayerLoopTiming timing)
		{
			this.unhandledExceptionCallback = delegate(Exception ex)
			{
				global::UnityEngine.Debug.LogException(ex);
			};
			this.timing = timing;
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002CDA0 File Offset: 0x0002AFA0
		public void AddAction(IPlayerLoopItem item)
		{
			object obj = this.runningAndQueueLock;
			lock (obj)
			{
				if (this.running)
				{
					this.waitQueue.Enqueue(item);
					return;
				}
			}
			obj = this.arrayLock;
			lock (obj)
			{
				if (this.loopItems.Length == this.tail)
				{
					Array.Resize<IPlayerLoopItem>(ref this.loopItems, checked(this.tail * 2));
				}
				IPlayerLoopItem[] array = this.loopItems;
				int num = this.tail;
				this.tail = num + 1;
				array[num] = item;
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0002CE58 File Offset: 0x0002B058
		public int Clear()
		{
			object obj = this.arrayLock;
			int num;
			lock (obj)
			{
				int rest = 0;
				for (int index = 0; index < this.loopItems.Length; index++)
				{
					if (this.loopItems[index] != null)
					{
						rest++;
					}
					this.loopItems[index] = null;
				}
				this.tail = 0;
				num = rest;
			}
			return num;
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0002CECC File Offset: 0x0002B0CC
		public void Run()
		{
			this.RunCore();
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void Initialization()
		{
			this.RunCore();
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void LastInitialization()
		{
			this.RunCore();
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void EarlyUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void LastEarlyUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void FixedUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void LastFixedUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void PreUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void LastPreUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void Update()
		{
			this.RunCore();
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void LastUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void PreLateUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void LastPreLateUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void PostLateUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void LastPostLateUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void TimeUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x0002CECC File Offset: 0x0002B0CC
		private void LastTimeUpdate()
		{
			this.RunCore();
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002CED4 File Offset: 0x0002B0D4
		[DebuggerHidden]
		private void RunCore()
		{
			object obj = this.runningAndQueueLock;
			lock (obj)
			{
				this.running = true;
			}
			obj = this.arrayLock;
			lock (obj)
			{
				int i = this.tail - 1;
				int j = 0;
				while (j < this.loopItems.Length)
				{
					IPlayerLoopItem action = this.loopItems[j];
					if (action != null)
					{
						try
						{
							if (!action.MoveNext())
							{
								this.loopItems[j] = null;
								goto IL_00F9;
							}
							goto IL_0106;
						}
						catch (Exception ex)
						{
							this.loopItems[j] = null;
							try
							{
								this.unhandledExceptionCallback(ex);
							}
							catch
							{
							}
							goto IL_00F9;
						}
						goto IL_0093;
					}
					goto IL_00F9;
					IL_0106:
					j++;
					continue;
					IL_0093:
					IPlayerLoopItem fromTail = this.loopItems[i];
					if (fromTail != null)
					{
						try
						{
							if (!fromTail.MoveNext())
							{
								this.loopItems[i] = null;
								i--;
								goto IL_00F9;
							}
							this.loopItems[j] = fromTail;
							this.loopItems[i] = null;
							i--;
							goto IL_0106;
						}
						catch (Exception ex2)
						{
							this.loopItems[i] = null;
							i--;
							try
							{
								this.unhandledExceptionCallback(ex2);
							}
							catch
							{
							}
							goto IL_00F9;
						}
					}
					i--;
					IL_00F9:
					if (j >= i)
					{
						this.tail = j;
						break;
					}
					goto IL_0093;
				}
				object obj2 = this.runningAndQueueLock;
				lock (obj2)
				{
					this.running = false;
					while (this.waitQueue.Count != 0)
					{
						if (this.loopItems.Length == this.tail)
						{
							Array.Resize<IPlayerLoopItem>(ref this.loopItems, checked(this.tail * 2));
						}
						IPlayerLoopItem[] array = this.loopItems;
						int num = this.tail;
						this.tail = num + 1;
						array[num] = this.waitQueue.Dequeue();
					}
				}
			}
		}

		// Token: 0x04000673 RID: 1651
		private const int InitialSize = 16;

		// Token: 0x04000674 RID: 1652
		private readonly PlayerLoopTiming timing;

		// Token: 0x04000675 RID: 1653
		private readonly object runningAndQueueLock = new object();

		// Token: 0x04000676 RID: 1654
		private readonly object arrayLock = new object();

		// Token: 0x04000677 RID: 1655
		private readonly Action<Exception> unhandledExceptionCallback;

		// Token: 0x04000678 RID: 1656
		private int tail;

		// Token: 0x04000679 RID: 1657
		private bool running;

		// Token: 0x0400067A RID: 1658
		private IPlayerLoopItem[] loopItems = new IPlayerLoopItem[16];

		// Token: 0x0400067B RID: 1659
		private MinimumQueue<IPlayerLoopItem> waitQueue = new MinimumQueue<IPlayerLoopItem>(16);
	}
}
