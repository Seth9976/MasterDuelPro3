using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Unity.IntegerTime
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public struct DiscreteTime : IEquatable<DiscreteTime>, IFormattable, IComparable<DiscreteTime>
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002676 File Offset: 0x00000876
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DiscreteTime(float v)
		{
			this.Value = (long)Math.Round((double)v * 141120000.0);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002691 File Offset: 0x00000891
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public DiscreteTime(double v)
		{
			this.Value = (long)Math.Round(v * 141120000.0);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000026AB File Offset: 0x000008AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private DiscreteTime(long v, int _)
		{
			this.Value = v;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000026B8 File Offset: 0x000008B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime FromTicks(long v)
		{
			return new DiscreteTime(v, 0);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000026D4 File Offset: 0x000008D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float(DiscreteTime d)
		{
			return (float)((double)d.Value / 141120000.0);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000026F8 File Offset: 0x000008F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double(DiscreteTime d)
		{
			return (double)d.Value / 141120000.0;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000271C File Offset: 0x0000091C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.Value != rhs.Value;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002740 File Offset: 0x00000940
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.Value > rhs.Value;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002760 File Offset: 0x00000960
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator >=(DiscreteTime lhs, DiscreteTime rhs)
		{
			return lhs.Value >= rhs.Value;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002784 File Offset: 0x00000984
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime operator +(DiscreteTime lhs, DiscreteTime rhs)
		{
			return DiscreteTime.FromTicks(lhs.Value + rhs.Value);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000027A8 File Offset: 0x000009A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime operator -(DiscreteTime lhs, DiscreteTime rhs)
		{
			return DiscreteTime.FromTicks(lhs.Value - rhs.Value);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000027CC File Offset: 0x000009CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly bool Equals(DiscreteTime rhs)
		{
			return this.Value == rhs.Value;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000027EC File Offset: 0x000009EC
		public override readonly bool Equals(object o)
		{
			return this.Equals((DiscreteTime)o);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000280C File Offset: 0x00000A0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000282C File Offset: 0x00000A2C
		public override readonly string ToString()
		{
			return ((double)this).ToString();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002854 File Offset: 0x00000A54
		public readonly string ToString(string format, IFormatProvider formatProvider)
		{
			return ((double)this).ToString(format, formatProvider);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000287C File Offset: 0x00000A7C
		public readonly int CompareTo(DiscreteTime other)
		{
			return this.Value.CompareTo(other.Value);
		}

		// Token: 0x0400001E RID: 30
		[SerializeField]
		public long Value;

		// Token: 0x0400001F RID: 31
		public static readonly DiscreteTime Zero = default(DiscreteTime);

		// Token: 0x04000020 RID: 32
		public static readonly DiscreteTime MinValue = new DiscreteTime(long.MinValue, 0);

		// Token: 0x04000021 RID: 33
		public static readonly DiscreteTime MaxValue = new DiscreteTime(long.MaxValue, 0);

		// Token: 0x04000022 RID: 34
		private static readonly int TicksPerSecondBits = (int)Mathf.Ceil(Mathf.Log(141120000f, 2f));

		// Token: 0x04000023 RID: 35
		private static readonly int NonPow2TpsBits = (int)Mathf.Ceil(Mathf.Log(275625f, 2f));
	}
}
