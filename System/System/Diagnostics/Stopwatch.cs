using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that you can use to accurately measure elapsed time.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000191 RID: 401
	public class Stopwatch
	{
		/// <summary>Gets the current number of ticks in the timer mechanism.</summary>
		/// <returns>A long integer representing the tick counter value of the underlying timer mechanism.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000994 RID: 2452
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTimestamp();

		/// <summary>Gets the total elapsed time measured by the current instance.</summary>
		/// <returns>A read-only <see cref="T:System.TimeSpan" /> representing the total elapsed time measured by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00032214 File Offset: 0x00030414
		public TimeSpan Elapsed
		{
			get
			{
				if (Stopwatch.IsHighResolution)
				{
					return TimeSpan.FromTicks(this.ElapsedTicks / (Stopwatch.Frequency / 10000000L));
				}
				return TimeSpan.FromTicks(this.ElapsedTicks);
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance, in milliseconds.</summary>
		/// <returns>A read-only long integer representing the total number of milliseconds measured by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00032244 File Offset: 0x00030444
		public long ElapsedMilliseconds
		{
			get
			{
				if (Stopwatch.IsHighResolution)
				{
					return this.ElapsedTicks / (Stopwatch.Frequency / 1000L);
				}
				return checked((long)this.Elapsed.TotalMilliseconds);
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance, in timer ticks.</summary>
		/// <returns>A read-only long integer representing the total number of timer ticks measured by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0003227B File Offset: 0x0003047B
		public long ElapsedTicks
		{
			get
			{
				if (!this.is_running)
				{
					return this.elapsed;
				}
				return Stopwatch.GetTimestamp() - this.started + this.elapsed;
			}
		}

		/// <summary>Starts, or resumes, measuring elapsed time for an interval.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000999 RID: 2457 RVA: 0x0003229F File Offset: 0x0003049F
		public void Start()
		{
			if (this.is_running)
			{
				return;
			}
			this.started = Stopwatch.GetTimestamp();
			this.is_running = true;
		}

		/// <summary>Gets the frequency of the timer as the number of ticks per second. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400072C RID: 1836
		public static readonly long Frequency = 10000000L;

		/// <summary>Indicates whether the timer is based on a high-resolution performance counter. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400072D RID: 1837
		public static readonly bool IsHighResolution = true;

		// Token: 0x0400072E RID: 1838
		private long elapsed;

		// Token: 0x0400072F RID: 1839
		private long started;

		// Token: 0x04000730 RID: 1840
		private bool is_running;
	}
}
