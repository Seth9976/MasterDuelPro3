using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000D7 RID: 215
	[NativeType("Runtime/Graphics/RefreshRate.h")]
	public struct RefreshRate : IEquatable<RefreshRate>, IComparable<RefreshRate>
	{
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000C1D7 File Offset: 0x0000A3D7
		public double value
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.numerator / this.denominator;
			}
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(RefreshRate other)
		{
			bool flag = this.denominator == 0U;
			bool flag2;
			if (flag)
			{
				flag2 = other.denominator == 0U;
			}
			else
			{
				bool flag3 = other.denominator == 0U;
				flag2 = !flag3 && (ulong)this.numerator * (ulong)other.denominator == (ulong)this.denominator * (ulong)other.numerator;
			}
			return flag2;
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0000C248 File Offset: 0x0000A448
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int CompareTo(RefreshRate other)
		{
			bool flag = this.denominator == 0U;
			int num;
			if (flag)
			{
				num = ((other.denominator == 0U) ? 0 : 1);
			}
			else
			{
				bool flag2 = other.denominator == 0U;
				if (flag2)
				{
					num = -1;
				}
				else
				{
					num = ((ulong)this.numerator * (ulong)other.denominator).CompareTo((ulong)this.denominator * (ulong)other.numerator);
				}
			}
			return num;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		public override string ToString()
		{
			return this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x04000289 RID: 649
		public uint numerator;

		// Token: 0x0400028A RID: 650
		public uint denominator;
	}
}
