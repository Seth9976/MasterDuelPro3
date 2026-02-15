using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000010 RID: 16
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool2x3 : IEquatable<bool2x3>
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x00021D25 File Offset: 0x0001FF25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x3(bool2 c0, bool2 c1, bool2 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00021D3C File Offset: 0x0001FF3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12)
		{
			this.c0 = new bool2(m00, m10);
			this.c1 = new bool2(m01, m11);
			this.c2 = new bool2(m02, m12);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00021D68 File Offset: 0x0001FF68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x3(bool v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x000084FD File Offset: 0x000066FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool2x3(bool v)
		{
			return new bool2x3(v);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00021D8E File Offset: 0x0001FF8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(bool2x3 lhs, bool2x3 rhs)
		{
			return new bool2x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00021DC8 File Offset: 0x0001FFC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(bool2x3 lhs, bool rhs)
		{
			return new bool2x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00021DF3 File Offset: 0x0001FFF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(bool lhs, bool2x3 rhs)
		{
			return new bool2x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00021E1E File Offset: 0x0002001E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(bool2x3 lhs, bool2x3 rhs)
		{
			return new bool2x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00021E58 File Offset: 0x00020058
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(bool2x3 lhs, bool rhs)
		{
			return new bool2x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00021E83 File Offset: 0x00020083
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(bool lhs, bool2x3 rhs)
		{
			return new bool2x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00021EAE File Offset: 0x000200AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !(bool2x3 val)
		{
			return new bool2x3(!val.c0, !val.c1, !val.c2);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00021ED6 File Offset: 0x000200D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator &(bool2x3 lhs, bool2x3 rhs)
		{
			return new bool2x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00021F10 File Offset: 0x00020110
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator &(bool2x3 lhs, bool rhs)
		{
			return new bool2x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00021F3B File Offset: 0x0002013B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator &(bool lhs, bool2x3 rhs)
		{
			return new bool2x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00021F66 File Offset: 0x00020166
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator |(bool2x3 lhs, bool2x3 rhs)
		{
			return new bool2x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00021FA0 File Offset: 0x000201A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator |(bool2x3 lhs, bool rhs)
		{
			return new bool2x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00021FCB File Offset: 0x000201CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator |(bool lhs, bool2x3 rhs)
		{
			return new bool2x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00021FF6 File Offset: 0x000201F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ^(bool2x3 lhs, bool2x3 rhs)
		{
			return new bool2x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00022030 File Offset: 0x00020230
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ^(bool2x3 lhs, bool rhs)
		{
			return new bool2x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002205B File Offset: 0x0002025B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ^(bool lhs, bool2x3 rhs)
		{
			return new bool2x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x1700001F RID: 31
		public unsafe ref bool2 this[int index]
		{
			get
			{
				fixed (bool2x3* ptr = &this)
				{
					return ref *(bool2*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool2) / (IntPtr)sizeof(bool2x3));
				}
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000220A3 File Offset: 0x000202A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool2x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x000220E0 File Offset: 0x000202E0
		public override bool Equals(object o)
		{
			if (o is bool2x3)
			{
				bool2x3 converted = (bool2x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00022105 File Offset: 0x00020305
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00022114 File Offset: 0x00020314
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool2x3({0}, {1}, {2},  {3}, {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y
			});
		}

		// Token: 0x04000043 RID: 67
		public bool2 c0;

		// Token: 0x04000044 RID: 68
		public bool2 c1;

		// Token: 0x04000045 RID: 69
		public bool2 c2;
	}
}
