using System;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.IntegerTime
{
	// Token: 0x0200001D RID: 29
	[NativeHeader("Runtime/Input/RationalTime.h")]
	[Serializable]
	public struct RationalTime
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002914 File Offset: 0x00000B14
		public long Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000291C File Offset: 0x00000B1C
		public static explicit operator DiscreteTime(RationalTime t)
		{
			return DiscreteTime.FromTicks(t.Convert(RationalTime.TicksPerSecond.DiscreteTimeRate).Count);
		}

		// Token: 0x04000024 RID: 36
		[SerializeField]
		private long m_Count;

		// Token: 0x04000025 RID: 37
		[SerializeField]
		private RationalTime.TicksPerSecond m_TicksPerSecond;

		// Token: 0x0200001E RID: 30
		[Serializable]
		public struct TicksPerSecond : IEquatable<RationalTime.TicksPerSecond>
		{
			// Token: 0x0600005F RID: 95 RVA: 0x00002946 File Offset: 0x00000B46
			public TicksPerSecond(uint num, uint den = 1U)
			{
				this.m_Numerator = num;
				this.m_Denominator = den;
				RationalTime.TicksPerSecond.Simplify(ref this.m_Numerator, ref this.m_Denominator);
			}

			// Token: 0x06000060 RID: 96 RVA: 0x0000296C File Offset: 0x00000B6C
			public readonly bool Equals(RationalTime.TicksPerSecond rhs)
			{
				return this.m_Numerator == rhs.m_Numerator && this.m_Denominator == rhs.m_Denominator;
			}

			// Token: 0x06000061 RID: 97 RVA: 0x000029A0 File Offset: 0x00000BA0
			public override readonly bool Equals(object rhs)
			{
				bool flag;
				if (rhs is RationalTime.TicksPerSecond)
				{
					RationalTime.TicksPerSecond other = (RationalTime.TicksPerSecond)rhs;
					flag = this.Equals(other);
				}
				else
				{
					flag = false;
				}
				return flag;
			}

			// Token: 0x06000062 RID: 98 RVA: 0x000029CC File Offset: 0x00000BCC
			public override readonly int GetHashCode()
			{
				return HashCode.Combine<uint, uint>(this.m_Numerator, this.m_Denominator);
			}

			// Token: 0x06000063 RID: 99 RVA: 0x000029F0 File Offset: 0x00000BF0
			private static void Simplify(ref uint num, ref uint den)
			{
				bool flag = den > 1U && num > 0U;
				if (flag)
				{
					uint gcd = RationalTime.TicksPerSecond.Gcd(num, den);
					num /= gcd;
					den /= gcd;
				}
			}

			// Token: 0x06000064 RID: 100 RVA: 0x00002A28 File Offset: 0x00000C28
			private static uint Gcd(uint a, uint b)
			{
				for (;;)
				{
					bool flag = a == 0U;
					if (flag)
					{
						break;
					}
					b %= a;
					bool flag2 = b == 0U;
					if (flag2)
					{
						goto Block_2;
					}
					a %= b;
				}
				return b;
				Block_2:
				return a;
			}

			// Token: 0x04000026 RID: 38
			[SerializeField]
			private uint m_Numerator;

			// Token: 0x04000027 RID: 39
			[SerializeField]
			private uint m_Denominator;

			// Token: 0x04000028 RID: 40
			public static readonly RationalTime.TicksPerSecond DefaultTicksPerSecond = new RationalTime.TicksPerSecond(141120000U, 1U);

			// Token: 0x04000029 RID: 41
			public static readonly RationalTime.TicksPerSecond TicksPerSecond24 = new RationalTime.TicksPerSecond(24U, 1U);

			// Token: 0x0400002A RID: 42
			public static readonly RationalTime.TicksPerSecond TicksPerSecond25 = new RationalTime.TicksPerSecond(25U, 1U);

			// Token: 0x0400002B RID: 43
			public static readonly RationalTime.TicksPerSecond TicksPerSecond30 = new RationalTime.TicksPerSecond(30U, 1U);

			// Token: 0x0400002C RID: 44
			public static readonly RationalTime.TicksPerSecond TicksPerSecond50 = new RationalTime.TicksPerSecond(50U, 1U);

			// Token: 0x0400002D RID: 45
			public static readonly RationalTime.TicksPerSecond TicksPerSecond60 = new RationalTime.TicksPerSecond(60U, 1U);

			// Token: 0x0400002E RID: 46
			public static readonly RationalTime.TicksPerSecond TicksPerSecond120 = new RationalTime.TicksPerSecond(120U, 1U);

			// Token: 0x0400002F RID: 47
			public static readonly RationalTime.TicksPerSecond TicksPerSecond2397 = new RationalTime.TicksPerSecond(24000U, 1001U);

			// Token: 0x04000030 RID: 48
			public static readonly RationalTime.TicksPerSecond TicksPerSecond2425 = new RationalTime.TicksPerSecond(25000U, 1001U);

			// Token: 0x04000031 RID: 49
			public static readonly RationalTime.TicksPerSecond TicksPerSecond2997 = new RationalTime.TicksPerSecond(30000U, 1001U);

			// Token: 0x04000032 RID: 50
			public static readonly RationalTime.TicksPerSecond TicksPerSecond5994 = new RationalTime.TicksPerSecond(60000U, 1001U);

			// Token: 0x04000033 RID: 51
			public static readonly RationalTime.TicksPerSecond TicksPerSecond11988 = new RationalTime.TicksPerSecond(120000U, 1001U);

			// Token: 0x04000034 RID: 52
			internal static readonly RationalTime.TicksPerSecond DiscreteTimeRate = new RationalTime.TicksPerSecond(141120000U, 1U);
		}
	}
}
