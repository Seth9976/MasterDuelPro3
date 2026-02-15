using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Threading
{
	/// <summary>Provides a mechanism for executing a method at specified intervals. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000286 RID: 646
	[ComVisible(true)]
	public sealed class Timer : MarshalByRefObject, IDisposable, IAsyncDisposable
	{
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x0005CC0B File Offset: 0x0005AE0B
		private static Timer.Scheduler scheduler
		{
			get
			{
				return Timer.Scheduler.Instance;
			}
		}

		/// <summary>Initializes a new instance of the Timer class, using a 32-bit signed integer to specify the time interval.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed. </param>
		/// <param name="state">An object containing information to be used by the callback method, or null. </param>
		/// <param name="dueTime">The amount of time to delay before <paramref name="callback" /> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param>
		/// <param name="period">The time interval between invocations of <paramref name="callback" />, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null. </exception>
		// Token: 0x0600180D RID: 6157 RVA: 0x0005CC12 File Offset: 0x0005AE12
		public Timer(TimerCallback callback, object state, int dueTime, int period)
		{
			this.Init(callback, state, (long)dueTime, (long)period);
		}

		/// <summary>Initializes a new instance of the Timer class, using 64-bit signed integers to measure time intervals.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed. </param>
		/// <param name="state">An object containing information to be used by the callback method, or null. </param>
		/// <param name="dueTime">The amount of time to delay before <paramref name="callback" /> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param>
		/// <param name="period">The time interval between invocations of <paramref name="callback" />, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is greater than 4294967294. </exception>
		// Token: 0x0600180E RID: 6158 RVA: 0x0005CC27 File Offset: 0x0005AE27
		public Timer(TimerCallback callback, object state, long dueTime, long period)
		{
			this.Init(callback, state, dueTime, period);
		}

		/// <summary>Initializes a new instance of the Timer class, using <see cref="T:System.TimeSpan" /> values to measure time intervals.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed. </param>
		/// <param name="state">An object containing information to be used by the callback method, or null. </param>
		/// <param name="dueTime">The <see cref="T:System.TimeSpan" /> representing the amount of time to delay before the <paramref name="callback" /> parameter invokes its methods. Specify negative one (-1) milliseconds to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the methods referenced by <paramref name="callback" />. Specify negative one (-1) milliseconds to disable periodic signaling. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of milliseconds in the value of <paramref name="dueTime" /> or <paramref name="period" /> is negative and not equal to <see cref="F:System.Threading.Timeout.Infinite" />, or is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null. </exception>
		// Token: 0x0600180F RID: 6159 RVA: 0x0005CC3A File Offset: 0x0005AE3A
		public Timer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
		{
			this.Init(callback, state, (long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds);
		}

		/// <summary>Initializes a new instance of the Timer class, using 32-bit unsigned integers to measure time intervals.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed. </param>
		/// <param name="state">An object containing information to be used by the callback method, or null. </param>
		/// <param name="dueTime">The amount of time to delay before <paramref name="callback" /> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param>
		/// <param name="period">The time interval between invocations of <paramref name="callback" />, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null. </exception>
		// Token: 0x06001810 RID: 6160 RVA: 0x0005CC5C File Offset: 0x0005AE5C
		[CLSCompliant(false)]
		public Timer(TimerCallback callback, object state, uint dueTime, uint period)
		{
			long num = (long)((dueTime == uint.MaxValue) ? ulong.MaxValue : ((ulong)dueTime));
			long num2 = (long)((period == uint.MaxValue) ? ulong.MaxValue : ((ulong)period));
			this.Init(callback, state, num, num2);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Timer" /> class with an infinite period and an infinite due time, using the newly created <see cref="T:System.Threading.Timer" /> object as the state object. </summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed.</param>
		// Token: 0x06001811 RID: 6161 RVA: 0x0005CC91 File Offset: 0x0005AE91
		public Timer(TimerCallback callback)
		{
			this.Init(callback, this, -1L, -1L);
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0005CCA5 File Offset: 0x0005AEA5
		private void Init(TimerCallback callback, object state, long dueTime, long period)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.callback = callback;
			this.state = state;
			this.is_dead = false;
			this.is_added = false;
			this.Change(dueTime, period, true);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using 32-bit signed integers to measure time intervals.</summary>
		/// <returns>true if the timer was successfully updated; otherwise, false.</returns>
		/// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001813 RID: 6163 RVA: 0x0005CCDC File Offset: 0x0005AEDC
		public bool Change(int dueTime, int period)
		{
			return this.Change((long)dueTime, (long)period, false);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using <see cref="T:System.TimeSpan" /> values to measure time intervals.</summary>
		/// <returns>true if the timer was successfully updated; otherwise, false.</returns>
		/// <param name="dueTime">A <see cref="T:System.TimeSpan" /> representing the amount of time to delay before invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed. Specify negative one (-1) milliseconds to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed. Specify negative one (-1) milliseconds to disable periodic signaling. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter, in milliseconds, is less than -1. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter, in milliseconds, is greater than 4294967294. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001814 RID: 6164 RVA: 0x0005CCE9 File Offset: 0x0005AEE9
		public bool Change(TimeSpan dueTime, TimeSpan period)
		{
			return this.Change((long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds, false);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using 32-bit unsigned integers to measure time intervals.</summary>
		/// <returns>true if the timer was successfully updated; otherwise, false.</returns>
		/// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001815 RID: 6165 RVA: 0x0005CD04 File Offset: 0x0005AF04
		[CLSCompliant(false)]
		public bool Change(uint dueTime, uint period)
		{
			long num = (long)((dueTime == uint.MaxValue) ? ulong.MaxValue : ((ulong)dueTime));
			long num2 = (long)((period == uint.MaxValue) ? ulong.MaxValue : ((ulong)period));
			return this.Change(num, num2, false);
		}

		/// <summary>Releases all resources used by the current instance of <see cref="T:System.Threading.Timer" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001816 RID: 6166 RVA: 0x0005CD30 File Offset: 0x0005AF30
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			Timer.scheduler.Remove(this);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using 64-bit signed integers to measure time intervals.</summary>
		/// <returns>true if the timer was successfully updated; otherwise, false.</returns>
		/// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is less than -1. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is greater than 4294967294. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001817 RID: 6167 RVA: 0x0005CD4D File Offset: 0x0005AF4D
		public bool Change(long dueTime, long period)
		{
			return this.Change(dueTime, period, false);
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x0005CD58 File Offset: 0x0005AF58
		private bool Change(long dueTime, long period, bool first)
		{
			if (dueTime > (long)((ulong)(-2)))
			{
				throw new ArgumentOutOfRangeException("dueTime", "Due time too large");
			}
			if (period > (long)((ulong)(-2)))
			{
				throw new ArgumentOutOfRangeException("period", "Period too large");
			}
			if (dueTime < -1L)
			{
				throw new ArgumentOutOfRangeException("dueTime");
			}
			if (period < -1L)
			{
				throw new ArgumentOutOfRangeException("period");
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a disposed object."));
			}
			this.due_time_ms = dueTime;
			this.period_ms = period;
			long num;
			if (dueTime == 0L)
			{
				num = 0L;
			}
			else if (dueTime < 0L)
			{
				num = long.MaxValue;
				if (first)
				{
					this.next_run = num;
					return true;
				}
			}
			else
			{
				num = dueTime * 10000L + Timer.GetTimeMonotonic();
			}
			Timer.scheduler.Change(this, num);
			return true;
		}

		/// <summary>Releases all resources used by the current instance of <see cref="T:System.Threading.Timer" /> and signals when the timer has been disposed of.</summary>
		/// <returns>true if the function succeeds; otherwise, false.</returns>
		/// <param name="notifyObject">The <see cref="T:System.Threading.WaitHandle" /> to be signaled when the Timer has been disposed of. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="notifyObject" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001819 RID: 6169 RVA: 0x0005CE17 File Offset: 0x0005B017
		public bool Dispose(WaitHandle notifyObject)
		{
			if (notifyObject == null)
			{
				throw new ArgumentNullException("notifyObject");
			}
			this.Dispose();
			NativeEventCalls.SetEvent(notifyObject.SafeWaitHandle);
			return true;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x0005CE3A File Offset: 0x0005B03A
		public ValueTask DisposeAsync()
		{
			this.Dispose();
			return new ValueTask(Task.FromResult<object>(null));
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00002C89 File Offset: 0x00000E89
		internal void KeepRootedWhileScheduled()
		{
		}

		// Token: 0x0600181C RID: 6172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long GetTimeMonotonic();

		// Token: 0x04000B78 RID: 2936
		private TimerCallback callback;

		// Token: 0x04000B79 RID: 2937
		private object state;

		// Token: 0x04000B7A RID: 2938
		private long due_time_ms;

		// Token: 0x04000B7B RID: 2939
		private long period_ms;

		// Token: 0x04000B7C RID: 2940
		private long next_run;

		// Token: 0x04000B7D RID: 2941
		private bool disposed;

		// Token: 0x04000B7E RID: 2942
		private bool is_dead;

		// Token: 0x04000B7F RID: 2943
		private bool is_added;

		// Token: 0x04000B80 RID: 2944
		private const long MaxValue = 4294967294L;

		// Token: 0x02000287 RID: 647
		private struct TimerComparer : IComparer, IComparer<Timer>
		{
			// Token: 0x0600181D RID: 6173 RVA: 0x0005CE50 File Offset: 0x0005B050
			int IComparer.Compare(object x, object y)
			{
				if (x == y)
				{
					return 0;
				}
				Timer timer = x as Timer;
				if (timer == null)
				{
					return -1;
				}
				Timer timer2 = y as Timer;
				if (timer2 == null)
				{
					return 1;
				}
				return this.Compare(timer, timer2);
			}

			// Token: 0x0600181E RID: 6174 RVA: 0x0005CE83 File Offset: 0x0005B083
			public int Compare(Timer tx, Timer ty)
			{
				return Math.Sign(tx.next_run - ty.next_run);
			}
		}

		// Token: 0x02000288 RID: 648
		private sealed class Scheduler
		{
			// Token: 0x0600181F RID: 6175 RVA: 0x0005CE97 File Offset: 0x0005B097
			private void InitScheduler()
			{
				this.changed = new ManualResetEvent(false);
				new Thread(new ThreadStart(this.SchedulerThread))
				{
					IsBackground = true
				}.Start();
			}

			// Token: 0x06001820 RID: 6176 RVA: 0x0005CEC2 File Offset: 0x0005B0C2
			private void WakeupScheduler()
			{
				this.changed.Set();
			}

			// Token: 0x06001821 RID: 6177 RVA: 0x0005CED0 File Offset: 0x0005B0D0
			private void SchedulerThread()
			{
				Thread.CurrentThread.Name = "Timer-Scheduler";
				for (;;)
				{
					int num = -1;
					lock (this)
					{
						this.changed.Reset();
						num = this.RunSchedulerLoop();
					}
					this.changed.WaitOne(num);
				}
			}

			// Token: 0x17000288 RID: 648
			// (get) Token: 0x06001822 RID: 6178 RVA: 0x0005CF38 File Offset: 0x0005B138
			public static Timer.Scheduler Instance
			{
				get
				{
					return Timer.Scheduler.instance;
				}
			}

			// Token: 0x06001823 RID: 6179 RVA: 0x0005CF3F File Offset: 0x0005B13F
			private Scheduler()
			{
				this.list = new List<Timer>(1024);
				this.InitScheduler();
			}

			// Token: 0x06001824 RID: 6180 RVA: 0x0005CF78 File Offset: 0x0005B178
			public void Remove(Timer timer)
			{
				lock (this)
				{
					this.InternalRemove(timer);
				}
			}

			// Token: 0x06001825 RID: 6181 RVA: 0x0005CFB4 File Offset: 0x0005B1B4
			public void Change(Timer timer, long new_next_run)
			{
				if (timer.is_dead)
				{
					timer.is_dead = false;
				}
				bool flag = false;
				lock (this)
				{
					this.needReSort = true;
					if (!timer.is_added)
					{
						timer.next_run = new_next_run;
						this.Add(timer);
						flag = this.current_next_run > new_next_run;
					}
					else
					{
						if (new_next_run == 9223372036854775807L)
						{
							timer.next_run = new_next_run;
							this.InternalRemove(timer);
							return;
						}
						if (!timer.disposed)
						{
							timer.next_run = new_next_run;
							flag = this.current_next_run > new_next_run;
						}
					}
				}
				if (flag)
				{
					this.WakeupScheduler();
				}
			}

			// Token: 0x06001826 RID: 6182 RVA: 0x0005D064 File Offset: 0x0005B264
			private void Add(Timer timer)
			{
				timer.is_added = true;
				this.needReSort = true;
				this.list.Add(timer);
				if (this.list.Count == 1)
				{
					this.WakeupScheduler();
				}
			}

			// Token: 0x06001827 RID: 6183 RVA: 0x0005D096 File Offset: 0x0005B296
			private void InternalRemove(Timer timer)
			{
				timer.is_dead = true;
				this.needReSort = true;
			}

			// Token: 0x06001828 RID: 6184 RVA: 0x0005D0A8 File Offset: 0x0005B2A8
			private static void TimerCB(object o)
			{
				Timer timer = (Timer)o;
				timer.callback(timer.state);
			}

			// Token: 0x06001829 RID: 6185 RVA: 0x0005D0D0 File Offset: 0x0005B2D0
			private void FireTimer(Timer timer)
			{
				long period_ms = timer.period_ms;
				long due_time_ms = timer.due_time_ms;
				if (period_ms == -1L || ((period_ms == 0L || period_ms == -1L) && due_time_ms != -1L))
				{
					timer.next_run = long.MaxValue;
					timer.is_dead = true;
				}
				else
				{
					timer.next_run = Timer.GetTimeMonotonic() + 10000L * timer.period_ms;
					timer.is_dead = false;
				}
				ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(Timer.Scheduler.TimerCB), timer);
			}

			// Token: 0x0600182A RID: 6186 RVA: 0x0005D154 File Offset: 0x0005B354
			private int RunSchedulerLoop()
			{
				long timeMonotonic = Timer.GetTimeMonotonic();
				Timer.TimerComparer timerComparer = default(Timer.TimerComparer);
				if (this.needReSort)
				{
					this.list.Sort(new Comparison<Timer>(timerComparer.Compare));
					this.needReSort = false;
				}
				long num = long.MaxValue;
				for (int i = 0; i < this.list.Count; i++)
				{
					Timer timer = this.list[i];
					if (!timer.is_dead)
					{
						if (timer.next_run <= timeMonotonic)
						{
							this.FireTimer(timer);
						}
						num = Math.Min(num, timer.next_run);
						if (timer.next_run > timeMonotonic && timer.next_run < 9223372036854775807L)
						{
							timer.is_dead = false;
						}
					}
				}
				for (int i = 0; i < this.list.Count; i++)
				{
					Timer timer2 = this.list[i];
					if (timer2.is_dead)
					{
						timer2.is_added = false;
						this.needReSort = true;
						this.list[i] = this.list[this.list.Count - 1];
						i--;
						this.list.RemoveAt(this.list.Count - 1);
						if (this.list.Count == 0)
						{
							break;
						}
					}
				}
				if (this.needReSort)
				{
					this.list.Sort(timerComparer);
					this.needReSort = false;
				}
				int num2 = -1;
				this.current_next_run = num;
				if (num != 9223372036854775807L)
				{
					long num3 = (num - Timer.GetTimeMonotonic()) / 10000L;
					if (num3 > 2147483647L)
					{
						num2 = 2147483646;
					}
					else
					{
						num2 = (int)num3;
						if (num2 < 0)
						{
							num2 = 0;
						}
					}
				}
				return num2;
			}

			// Token: 0x04000B81 RID: 2945
			private static readonly Timer.Scheduler instance = new Timer.Scheduler();

			// Token: 0x04000B82 RID: 2946
			private volatile bool needReSort = true;

			// Token: 0x04000B83 RID: 2947
			private List<Timer> list;

			// Token: 0x04000B84 RID: 2948
			private long current_next_run = long.MaxValue;

			// Token: 0x04000B85 RID: 2949
			private ManualResetEvent changed;
		}
	}
}
