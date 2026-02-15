using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000037 RID: 55
	internal struct DiscreteTime : IComparable
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00007DF3 File Offset: 0x00005FF3
		public static double tickValue
		{
			get
			{
				return 1E-12;
			}
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00007DFE File Offset: 0x00005FFE
		public DiscreteTime(DiscreteTime time)
		{
			this.m_DiscreteTime = time.m_DiscreteTime;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007E0C File Offset: 0x0000600C
		private DiscreteTime(long time)
		{
			this.m_DiscreteTime = time;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00007E15 File Offset: 0x00006015
		public DiscreteTime(double time)
		{
			this.m_DiscreteTime = DiscreteTime.DoubleToDiscreteTime(time);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007E23 File Offset: 0x00006023
		public DiscreteTime(float time)
		{
			this.m_DiscreteTime = DiscreteTime.FloatToDiscreteTime(time);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00007E31 File Offset: 0x00006031
		public DiscreteTime(int time)
		{
			this.m_DiscreteTime = DiscreteTime.IntToDiscreteTime(time);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007E3F File Offset: 0x0000603F
		public DiscreteTime(int frame, double fps)
		{
			this.m_DiscreteTime = DiscreteTime.DoubleToDiscreteTime((double)frame * fps);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00007E50 File Offset: 0x00006050
		public DiscreteTime OneTickBefore()
		{
			return new DiscreteTime(this.m_DiscreteTime - 1L);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007E60 File Offset: 0x00006060
		public DiscreteTime OneTickAfter()
		{
			return new DiscreteTime(this.m_DiscreteTime + 1L);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00007E70 File Offset: 0x00006070
		public long GetTick()
		{
			return this.m_DiscreteTime;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007E78 File Offset: 0x00006078
		public static DiscreteTime FromTicks(long ticks)
		{
			return new DiscreteTime(ticks);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007E80 File Offset: 0x00006080
		public int CompareTo(object obj)
		{
			if (obj is DiscreteTime)
			{
				return this.m_DiscreteTime.CompareTo(((DiscreteTime)obj).m_DiscreteTime);
			}
			return 1;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007EB0 File Offset: 0x000060B0
		public bool Equals(DiscreteTime other)
		{
			return this.m_DiscreteTime == other.m_DiscreteTime;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00007EC0 File Offset: 0x000060C0
		public override bool Equals(object obj)
		{
			return obj is DiscreteTime && this.Equals((DiscreteTime)obj);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007ED8 File Offset: 0x000060D8
		private static long DoubleToDiscreteTime(double time)
		{
			double number = time / 1E-12 + 0.5;
			if (number < 9.223372036854776E+18 && number > -9.223372036854776E+18)
			{
				return (long)number;
			}
			throw new ArgumentOutOfRangeException("Time is over the discrete range.");
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007F20 File Offset: 0x00006120
		private static long FloatToDiscreteTime(float time)
		{
			float number = time / 1E-12f + 0.5f;
			if (number < 9.223372E+18f && number > -9.223372E+18f)
			{
				return (long)number;
			}
			throw new ArgumentOutOfRangeException("Time is over the discrete range.");
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007F58 File Offset: 0x00006158
		private static long IntToDiscreteTime(int time)
		{
			return DiscreteTime.DoubleToDiscreteTime((double)time);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00007F61 File Offset: 0x00006161
		private static double ToDouble(long time)
		{
			return (double)time * 1E-12;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00007F6F File Offset: 0x0000616F
		private static float ToFloat(long time)
		{
			return (float)DiscreteTime.ToDouble(time);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00007F78 File Offset: 0x00006178
		public static explicit operator double(DiscreteTime b)
		{
			return DiscreteTime.ToDouble(b.m_DiscreteTime);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00007F85 File Offset: 0x00006185
		public static explicit operator float(DiscreteTime b)
		{
			return DiscreteTime.ToFloat(b.m_DiscreteTime);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007E70 File Offset: 0x00006070
		public static explicit operator long(DiscreteTime b)
		{
			return b.m_DiscreteTime;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007F92 File Offset: 0x00006192
		public static explicit operator DiscreteTime(double time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00007F9A File Offset: 0x0000619A
		public static explicit operator DiscreteTime(float time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00007FA2 File Offset: 0x000061A2
		public static implicit operator DiscreteTime(int time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00007E78 File Offset: 0x00006078
		public static explicit operator DiscreteTime(long time)
		{
			return new DiscreteTime(time);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007EB0 File Offset: 0x000060B0
		public static bool operator ==(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime == rhs.m_DiscreteTime;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00007FAA File Offset: 0x000061AA
		public static bool operator !=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00007FB6 File Offset: 0x000061B6
		public static bool operator >(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime > rhs.m_DiscreteTime;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00007FC6 File Offset: 0x000061C6
		public static bool operator <(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime < rhs.m_DiscreteTime;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00007FD6 File Offset: 0x000061D6
		public static bool operator <=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime <= rhs.m_DiscreteTime;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00007FE9 File Offset: 0x000061E9
		public static bool operator >=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.m_DiscreteTime >= rhs.m_DiscreteTime;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00007FFC File Offset: 0x000061FC
		public static DiscreteTime operator +(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(lhs.m_DiscreteTime + rhs.m_DiscreteTime);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00008010 File Offset: 0x00006210
		public static DiscreteTime operator -(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(lhs.m_DiscreteTime - rhs.m_DiscreteTime);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00008024 File Offset: 0x00006224
		public override string ToString()
		{
			return this.m_DiscreteTime.ToString();
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00008040 File Offset: 0x00006240
		public override int GetHashCode()
		{
			return this.m_DiscreteTime.GetHashCode();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000805B File Offset: 0x0000625B
		public static DiscreteTime Min(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(Math.Min(lhs.m_DiscreteTime, rhs.m_DiscreteTime));
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008073 File Offset: 0x00006273
		public static DiscreteTime Max(DiscreteTime lhs, DiscreteTime rhs)
		{
			return new DiscreteTime(Math.Max(lhs.m_DiscreteTime, rhs.m_DiscreteTime));
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000808B File Offset: 0x0000628B
		public static double SnapToNearestTick(double time)
		{
			return DiscreteTime.ToDouble(DiscreteTime.DoubleToDiscreteTime(time));
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00008098 File Offset: 0x00006298
		public static float SnapToNearestTick(float time)
		{
			return DiscreteTime.ToFloat(DiscreteTime.FloatToDiscreteTime(time));
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000080A5 File Offset: 0x000062A5
		public static long GetNearestTick(double time)
		{
			return DiscreteTime.DoubleToDiscreteTime(time);
		}

		// Token: 0x0400010A RID: 266
		private const double k_Tick = 1E-12;

		// Token: 0x0400010B RID: 267
		public static readonly DiscreteTime kMaxTime = new DiscreteTime(long.MaxValue);

		// Token: 0x0400010C RID: 268
		private readonly long m_DiscreteTime;
	}
}
