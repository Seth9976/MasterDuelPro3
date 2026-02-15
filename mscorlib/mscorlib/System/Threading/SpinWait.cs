using System;
using Internal.Runtime.Augments;

namespace System.Threading
{
	/// <summary>Provides support for spin-based waiting.</summary>
	// Token: 0x02000231 RID: 561
	public struct SpinWait
	{
		/// <summary>Gets the number of times <see cref="M:System.Threading.SpinWait.SpinOnce" /> has been called on this instance.</summary>
		/// <returns>Returns an integer that represents the number of times <see cref="M:System.Threading.SpinWait.SpinOnce" /> has been called on this instance.</returns>
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x00054494 File Offset: 0x00052694
		// (set) Token: 0x060014E1 RID: 5345 RVA: 0x0005449C File Offset: 0x0005269C
		public int Count
		{
			get
			{
				return this._count;
			}
			internal set
			{
				this._count = value;
			}
		}

		/// <summary>Gets whether the next call to <see cref="M:System.Threading.SpinWait.SpinOnce" /> will yield the processor, triggering a forced context switch.</summary>
		/// <returns>Whether the next call to <see cref="M:System.Threading.SpinWait.SpinOnce" /> will yield the processor, triggering a forced context switch.</returns>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x000544A5 File Offset: 0x000526A5
		public bool NextSpinWillYield
		{
			get
			{
				return this._count >= 10 || PlatformHelper.IsSingleProcessor;
			}
		}

		/// <summary>Performs a single spin.</summary>
		// Token: 0x060014E3 RID: 5347 RVA: 0x000544B8 File Offset: 0x000526B8
		public void SpinOnce()
		{
			this.SpinOnceCore(20);
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x000544C2 File Offset: 0x000526C2
		public void SpinOnce(int sleep1Threshold)
		{
			if (sleep1Threshold < -1)
			{
				throw new ArgumentOutOfRangeException("sleep1Threshold", sleep1Threshold, "Number must be either non-negative and less than or equal to Int32.MaxValue or -1.");
			}
			if (sleep1Threshold >= 0 && sleep1Threshold < 10)
			{
				sleep1Threshold = 10;
			}
			this.SpinOnceCore(sleep1Threshold);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x000544F4 File Offset: 0x000526F4
		private void SpinOnceCore(int sleep1Threshold)
		{
			if ((this._count >= 10 && ((this._count >= sleep1Threshold && sleep1Threshold >= 0) || (this._count - 10) % 2 == 0)) || PlatformHelper.IsSingleProcessor)
			{
				if (this._count >= sleep1Threshold && sleep1Threshold >= 0)
				{
					RuntimeThread.Sleep(1);
				}
				else if (((this._count >= 10) ? ((this._count - 10) / 2) : this._count) % 5 == 4)
				{
					RuntimeThread.Sleep(0);
				}
				else
				{
					RuntimeThread.Yield();
				}
			}
			else
			{
				int num = RuntimeThread.OptimalMaxSpinWaitsPerSpinIteration;
				if (this._count <= 30 && 1 << this._count < num)
				{
					num = 1 << this._count;
				}
				RuntimeThread.SpinWait(num);
			}
			this._count = ((this._count == int.MaxValue) ? 10 : (this._count + 1));
		}

		/// <summary>Resets the spin counter.</summary>
		// Token: 0x060014E6 RID: 5350 RVA: 0x000545C3 File Offset: 0x000527C3
		public void Reset()
		{
			this._count = 0;
		}

		/// <summary>Spins until the specified condition is satisfied.</summary>
		/// <param name="condition">A delegate to be executed over and over until it returns true.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="condition" /> argument is null.</exception>
		// Token: 0x060014E7 RID: 5351 RVA: 0x000545CC File Offset: 0x000527CC
		public static void SpinUntil(Func<bool> condition)
		{
			SpinWait.SpinUntil(condition, -1);
		}

		/// <summary>Spins until the specified condition is satisfied or until the specified timeout is expired.</summary>
		/// <returns>True if the condition is satisfied within the timeout; otherwise, false</returns>
		/// <param name="condition">A delegate to be executed over and over until it returns true.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a TimeSpan that represents -1 milliseconds to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="condition" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out -or- timeout is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x060014E8 RID: 5352 RVA: 0x000545D8 File Offset: 0x000527D8
		public static bool SpinUntil(Func<bool> condition, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, "The timeout must represent a value between -1 and Int32.MaxValue, inclusive.");
			}
			return SpinWait.SpinUntil(condition, (int)num);
		}

		/// <summary>Spins until the specified condition is satisfied or until the specified timeout is expired.</summary>
		/// <returns>True if the condition is satisfied within the timeout; otherwise, false</returns>
		/// <param name="condition">A delegate to be executed over and over until it returns true.</param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="condition" /> argument is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out.</exception>
		// Token: 0x060014E9 RID: 5353 RVA: 0x0005461C File Offset: 0x0005281C
		public static bool SpinUntil(Func<bool> condition, int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, "The timeout must represent a value between -1 and Int32.MaxValue, inclusive.");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition", "The condition argument is null.");
			}
			uint num = 0U;
			if (millisecondsTimeout != 0 && millisecondsTimeout != -1)
			{
				num = TimeoutHelper.GetTime();
			}
			SpinWait spinWait = default(SpinWait);
			while (!condition())
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				spinWait.SpinOnce();
				if (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && (long)millisecondsTimeout <= (long)((ulong)(TimeoutHelper.GetTime() - num)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000A37 RID: 2615
		internal const int YieldThreshold = 10;

		// Token: 0x04000A38 RID: 2616
		private const int Sleep0EveryHowManyYields = 5;

		// Token: 0x04000A39 RID: 2617
		internal const int DefaultSleep1Threshold = 20;

		// Token: 0x04000A3A RID: 2618
		internal static readonly int SpinCountforSpinBeforeWait = (PlatformHelper.IsSingleProcessor ? 1 : 35);

		// Token: 0x04000A3B RID: 2619
		internal const int Sleep1ThresholdForLongSpinBeforeWait = 40;

		// Token: 0x04000A3C RID: 2620
		private int _count;
	}
}
