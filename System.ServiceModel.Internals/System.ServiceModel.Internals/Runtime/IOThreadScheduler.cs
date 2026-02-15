using System;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x0200001B RID: 27
	internal class IOThreadScheduler
	{
		// Token: 0x06000065 RID: 101 RVA: 0x000030C8 File Offset: 0x000012C8
		private IOThreadScheduler(int capacity, int capacityLowPri)
		{
			this.slots = new IOThreadScheduler.Slot[capacity];
			this.slotsLowPri = new IOThreadScheduler.Slot[capacityLowPri];
			this.overlapped = new IOThreadScheduler.ScheduledOverlapped();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003114 File Offset: 0x00001314
		public static void ScheduleCallbackNoFlow(Action<object> callback, object state)
		{
			if (callback == null)
			{
				throw Fx.Exception.ArgumentNull("callback");
			}
			bool flag = false;
			while (!flag)
			{
				try
				{
				}
				finally
				{
					flag = IOThreadScheduler.current.ScheduleCallbackHelper(callback, state);
				}
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000315C File Offset: 0x0000135C
		public static void ScheduleCallbackLowPriNoFlow(Action<object> callback, object state)
		{
			if (callback == null)
			{
				throw Fx.Exception.ArgumentNull("callback");
			}
			bool flag = false;
			while (!flag)
			{
				try
				{
				}
				finally
				{
					flag = IOThreadScheduler.current.ScheduleCallbackLowPriHelper(callback, state);
				}
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000031A4 File Offset: 0x000013A4
		private bool ScheduleCallbackHelper(Action<object> callback, object state)
		{
			int num = Interlocked.Add(ref this.headTail, 65536);
			bool flag = IOThreadScheduler.Bits.Count(num) == 0;
			if (flag)
			{
				num = Interlocked.Add(ref this.headTail, 65536);
			}
			if (IOThreadScheduler.Bits.Count(num) == -1)
			{
				throw Fx.AssertAndThrowFatal("Head/Tail overflow!");
			}
			bool flag3;
			bool flag2 = this.slots[(num >> 16) & this.SlotMask].TryEnqueueWorkItem(callback, state, out flag3);
			if (flag3)
			{
				IOThreadScheduler iothreadScheduler = new IOThreadScheduler(Math.Min(this.slots.Length * 2, 32768), this.slotsLowPri.Length);
				Interlocked.CompareExchange<IOThreadScheduler>(ref IOThreadScheduler.current, iothreadScheduler, this);
			}
			if (flag)
			{
				this.overlapped.Post(this);
			}
			return flag2;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003254 File Offset: 0x00001454
		private bool ScheduleCallbackLowPriHelper(Action<object> callback, object state)
		{
			int num = Interlocked.Add(ref this.headTailLowPri, 65536);
			bool flag = false;
			if (IOThreadScheduler.Bits.CountNoIdle(num) == 1)
			{
				int num2 = this.headTail;
				if (IOThreadScheduler.Bits.Count(num2) == -1)
				{
					int num3 = Interlocked.CompareExchange(ref this.headTail, num2 + 65536, num2);
					if (num2 == num3)
					{
						flag = true;
					}
				}
			}
			if (IOThreadScheduler.Bits.CountNoIdle(num) == 0)
			{
				throw Fx.AssertAndThrowFatal("Low-priority Head/Tail overflow!");
			}
			bool flag3;
			bool flag2 = this.slotsLowPri[(num >> 16) & this.SlotMaskLowPri].TryEnqueueWorkItem(callback, state, out flag3);
			if (flag3)
			{
				IOThreadScheduler iothreadScheduler = new IOThreadScheduler(this.slots.Length, Math.Min(this.slotsLowPri.Length * 2, 32768));
				Interlocked.CompareExchange<IOThreadScheduler>(ref IOThreadScheduler.current, iothreadScheduler, this);
			}
			if (flag)
			{
				this.overlapped.Post(this);
			}
			return flag2;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003320 File Offset: 0x00001520
		private void CompletionCallback(out Action<object> callback, out object state)
		{
			int num = this.headTail;
			int num2;
			for (;;)
			{
				bool flag = IOThreadScheduler.Bits.Count(num) == 0;
				if (flag)
				{
					num2 = this.headTailLowPri;
					while (IOThreadScheduler.Bits.CountNoIdle(num2) != 0)
					{
						if (num2 == (num2 = Interlocked.CompareExchange(ref this.headTailLowPri, IOThreadScheduler.Bits.IncrementLo(num2), num2)))
						{
							goto Block_2;
						}
					}
				}
				if (num == (num = Interlocked.CompareExchange(ref this.headTail, IOThreadScheduler.Bits.IncrementLo(num), num)))
				{
					if (!flag)
					{
						goto Block_4;
					}
					num2 = this.headTailLowPri;
					if (IOThreadScheduler.Bits.CountNoIdle(num2) == 0)
					{
						goto IL_00DD;
					}
					num = IOThreadScheduler.Bits.IncrementLo(num);
					if (num != Interlocked.CompareExchange(ref this.headTail, num + 65536, num))
					{
						goto IL_00DD;
					}
					num += 65536;
				}
			}
			Block_2:
			this.overlapped.Post(this);
			this.slotsLowPri[num2 & this.SlotMaskLowPri].DequeueWorkItem(out callback, out state);
			return;
			Block_4:
			this.overlapped.Post(this);
			this.slots[num & this.SlotMask].DequeueWorkItem(out callback, out state);
			return;
			IL_00DD:
			callback = null;
			state = null;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003410 File Offset: 0x00001610
		private bool TryCoalesce(out Action<object> callback, out object state)
		{
			int num = this.headTail;
			int num2;
			for (;;)
			{
				if (IOThreadScheduler.Bits.Count(num) > 0)
				{
					if (num == (num = Interlocked.CompareExchange(ref this.headTail, IOThreadScheduler.Bits.IncrementLo(num), num)))
					{
						break;
					}
				}
				else
				{
					num2 = this.headTailLowPri;
					if (IOThreadScheduler.Bits.CountNoIdle(num2) <= 0)
					{
						goto IL_0092;
					}
					if (num2 == (num2 = Interlocked.CompareExchange(ref this.headTailLowPri, IOThreadScheduler.Bits.IncrementLo(num2), num2)))
					{
						goto Block_4;
					}
					num = this.headTail;
				}
			}
			this.slots[num & this.SlotMask].DequeueWorkItem(out callback, out state);
			return true;
			Block_4:
			this.slotsLowPri[num2 & this.SlotMaskLowPri].DequeueWorkItem(out callback, out state);
			return true;
			IL_0092:
			callback = null;
			state = null;
			return false;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000034B6 File Offset: 0x000016B6
		private int SlotMask
		{
			get
			{
				return this.slots.Length - 1;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000034C2 File Offset: 0x000016C2
		private int SlotMaskLowPri
		{
			get
			{
				return this.slotsLowPri.Length - 1;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000034D0 File Offset: 0x000016D0
		~IOThreadScheduler()
		{
			if (!Environment.HasShutdownStarted && !AppDomain.CurrentDomain.IsFinalizingForUnload())
			{
				this.Cleanup();
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003510 File Offset: 0x00001710
		private void Cleanup()
		{
			if (this.overlapped != null)
			{
				this.overlapped.Cleanup();
			}
		}

		// Token: 0x0400002F RID: 47
		private static IOThreadScheduler current = new IOThreadScheduler(32, 32);

		// Token: 0x04000030 RID: 48
		private readonly IOThreadScheduler.ScheduledOverlapped overlapped;

		// Token: 0x04000031 RID: 49
		private readonly IOThreadScheduler.Slot[] slots;

		// Token: 0x04000032 RID: 50
		private readonly IOThreadScheduler.Slot[] slotsLowPri;

		// Token: 0x04000033 RID: 51
		private int headTail = -131072;

		// Token: 0x04000034 RID: 52
		private int headTailLowPri = -65536;

		// Token: 0x0200001C RID: 28
		private static class Bits
		{
			// Token: 0x06000071 RID: 113 RVA: 0x00003535 File Offset: 0x00001735
			public static int Count(int slot)
			{
				return (((slot >> 16) - slot + 2) & 65535) - 1;
			}

			// Token: 0x06000072 RID: 114 RVA: 0x00003547 File Offset: 0x00001747
			public static int CountNoIdle(int slot)
			{
				return ((slot >> 16) - slot + 1) & 65535;
			}

			// Token: 0x06000073 RID: 115 RVA: 0x00003557 File Offset: 0x00001757
			public static int IncrementLo(int slot)
			{
				return ((slot + 1) & 65535) | (slot & -65536);
			}

			// Token: 0x06000074 RID: 116 RVA: 0x0000356A File Offset: 0x0000176A
			public static bool IsComplete(int gate)
			{
				return (gate & -65536) == gate << 16;
			}
		}

		// Token: 0x0200001D RID: 29
		private struct Slot
		{
			// Token: 0x06000075 RID: 117 RVA: 0x0000357C File Offset: 0x0000177C
			public bool TryEnqueueWorkItem(Action<object> callback, object state, out bool wrapped)
			{
				int num = Interlocked.Increment(ref this.gate);
				wrapped = (num & 32767) != 1;
				if (wrapped)
				{
					if ((num & 32768) != 0 && IOThreadScheduler.Bits.IsComplete(num))
					{
						Interlocked.CompareExchange(ref this.gate, 0, num);
					}
					return false;
				}
				this.state = state;
				this.callback = callback;
				num = Interlocked.Add(ref this.gate, 32768);
				if ((num & 2147418112) == 0)
				{
					return true;
				}
				this.state = null;
				this.callback = null;
				if (num >> 16 != (num & 32767) || Interlocked.CompareExchange(ref this.gate, 0, num) != num)
				{
					num = Interlocked.Add(ref this.gate, int.MinValue);
					if (IOThreadScheduler.Bits.IsComplete(num))
					{
						Interlocked.CompareExchange(ref this.gate, 0, num);
					}
				}
				return false;
			}

			// Token: 0x06000076 RID: 118 RVA: 0x00003648 File Offset: 0x00001848
			public void DequeueWorkItem(out Action<object> callback, out object state)
			{
				int num = Interlocked.Add(ref this.gate, 65536);
				if ((num & 32768) == 0)
				{
					callback = null;
					state = null;
					return;
				}
				if ((num & 2147418112) == 65536)
				{
					callback = this.callback;
					state = this.state;
					this.state = null;
					this.callback = null;
					if ((num & 32767) != 1 || Interlocked.CompareExchange(ref this.gate, 0, num) != num)
					{
						num = Interlocked.Add(ref this.gate, int.MinValue);
						if (IOThreadScheduler.Bits.IsComplete(num))
						{
							Interlocked.CompareExchange(ref this.gate, 0, num);
							return;
						}
					}
				}
				else
				{
					callback = null;
					state = null;
					if (IOThreadScheduler.Bits.IsComplete(num))
					{
						Interlocked.CompareExchange(ref this.gate, 0, num);
					}
				}
			}

			// Token: 0x04000035 RID: 53
			private int gate;

			// Token: 0x04000036 RID: 54
			private Action<object> callback;

			// Token: 0x04000037 RID: 55
			private object state;
		}

		// Token: 0x0200001E RID: 30
		private class ScheduledOverlapped
		{
			// Token: 0x06000077 RID: 119 RVA: 0x00003700 File Offset: 0x00001900
			public ScheduledOverlapped()
			{
				this.nativeOverlapped = new Overlapped().UnsafePack(Fx.ThunkCallback(new IOCompletionCallback(this.IOCallback)), null);
			}

			// Token: 0x06000078 RID: 120 RVA: 0x0000372C File Offset: 0x0000192C
			private unsafe void IOCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
			{
				IOThreadScheduler iothreadScheduler = this.scheduler;
				this.scheduler = null;
				Action<object> action;
				object obj;
				try
				{
				}
				finally
				{
					iothreadScheduler.CompletionCallback(out action, out obj);
				}
				bool flag = true;
				while (flag)
				{
					if (action != null)
					{
						action(obj);
					}
					try
					{
					}
					finally
					{
						flag = iothreadScheduler.TryCoalesce(out action, out obj);
					}
				}
			}

			// Token: 0x06000079 RID: 121 RVA: 0x00003790 File Offset: 0x00001990
			public void Post(IOThreadScheduler iots)
			{
				this.scheduler = iots;
				ThreadPool.UnsafeQueueNativeOverlapped(this.nativeOverlapped);
			}

			// Token: 0x0600007A RID: 122 RVA: 0x000037A5 File Offset: 0x000019A5
			public void Cleanup()
			{
				if (this.scheduler != null)
				{
					throw Fx.AssertAndThrowFatal("Cleanup called on an overlapped that is in-flight.");
				}
				Overlapped.Free(this.nativeOverlapped);
			}

			// Token: 0x04000038 RID: 56
			private unsafe readonly NativeOverlapped* nativeOverlapped;

			// Token: 0x04000039 RID: 57
			private IOThreadScheduler scheduler;
		}
	}
}
