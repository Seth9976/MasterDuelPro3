using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000019 RID: 25
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool4x2 : IEquatable<bool4x2>
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x00027E05 File Offset: 0x00026005
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x2(bool4 c0, bool4 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00027E15 File Offset: 0x00026015
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x2(bool m00, bool m01, bool m10, bool m11, bool m20, bool m21, bool m30, bool m31)
		{
			this.c0 = new bool4(m00, m10, m20, m30);
			this.c1 = new bool4(m01, m11, m21, m31);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00027E3A File Offset: 0x0002603A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x2(bool v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0000921D File Offset: 0x0000741D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool4x2(bool v)
		{
			return new bool4x2(v);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00027E54 File Offset: 0x00026054
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(bool4x2 lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00027E7D File Offset: 0x0002607D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(bool4x2 lhs, bool rhs)
		{
			return new bool4x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00027E9C File Offset: 0x0002609C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(bool lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x00027EBB File Offset: 0x000260BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(bool4x2 lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x00027EE4 File Offset: 0x000260E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(bool4x2 lhs, bool rhs)
		{
			return new bool4x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x00027F03 File Offset: 0x00026103
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(bool lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00027F22 File Offset: 0x00026122
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !(bool4x2 val)
		{
			return new bool4x2(!val.c0, !val.c1);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00027F3F File Offset: 0x0002613F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator &(bool4x2 lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00027F68 File Offset: 0x00026168
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator &(bool4x2 lhs, bool rhs)
		{
			return new bool4x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00027F87 File Offset: 0x00026187
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator &(bool lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00027FA6 File Offset: 0x000261A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator |(bool4x2 lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00027FCF File Offset: 0x000261CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator |(bool4x2 lhs, bool rhs)
		{
			return new bool4x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00027FEE File Offset: 0x000261EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator |(bool lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002800D File Offset: 0x0002620D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ^(bool4x2 lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x00028036 File Offset: 0x00026236
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ^(bool4x2 lhs, bool rhs)
		{
			return new bool4x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x00028055 File Offset: 0x00026255
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ^(bool lhs, bool4x2 rhs)
		{
			return new bool4x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x170001EB RID: 491
		public unsafe ref bool4 this[int index]
		{
			get
			{
				fixed (bool4x2* ptr = &this)
				{
					return ref *(bool4*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool4) / (IntPtr)sizeof(bool4x2));
				}
			}
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002808F File Offset: 0x0002628F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool4x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000280B8 File Offset: 0x000262B8
		public override bool Equals(object o)
		{
			if (o is bool4x2)
			{
				bool4x2 converted = (bool4x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000280DD File Offset: 0x000262DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000280EC File Offset: 0x000262EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool4x2({0}, {1},  {2}, {3},  {4}, {5},  {6}, {7})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z,
				this.c0.w,
				this.c1.w
			});
		}

		// Token: 0x04000061 RID: 97
		public bool4 c0;

		// Token: 0x04000062 RID: 98
		public bool4 c1;
	}
}
