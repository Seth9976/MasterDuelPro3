using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200001A RID: 26
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool4x3 : IEquatable<bool4x3>
	{
		// Token: 0x06000B20 RID: 2848 RVA: 0x000281A1 File Offset: 0x000263A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x3(bool4 c0, bool4 c1, bool4 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000281B8 File Offset: 0x000263B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12, bool m20, bool m21, bool m22, bool m30, bool m31, bool m32)
		{
			this.c0 = new bool4(m00, m10, m20, m30);
			this.c1 = new bool4(m01, m11, m21, m31);
			this.c2 = new bool4(m02, m12, m22, m32);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x000281F0 File Offset: 0x000263F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x3(bool v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000093E6 File Offset: 0x000075E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool4x3(bool v)
		{
			return new bool4x3(v);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00028216 File Offset: 0x00026416
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(bool4x3 lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00028250 File Offset: 0x00026450
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(bool4x3 lhs, bool rhs)
		{
			return new bool4x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002827B File Offset: 0x0002647B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(bool lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x000282A6 File Offset: 0x000264A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(bool4x3 lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000282E0 File Offset: 0x000264E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(bool4x3 lhs, bool rhs)
		{
			return new bool4x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x0002830B File Offset: 0x0002650B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(bool lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00028336 File Offset: 0x00026536
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !(bool4x3 val)
		{
			return new bool4x3(!val.c0, !val.c1, !val.c2);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0002835E File Offset: 0x0002655E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator &(bool4x3 lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00028398 File Offset: 0x00026598
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator &(bool4x3 lhs, bool rhs)
		{
			return new bool4x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000283C3 File Offset: 0x000265C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator &(bool lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000283EE File Offset: 0x000265EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator |(bool4x3 lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00028428 File Offset: 0x00026628
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator |(bool4x3 lhs, bool rhs)
		{
			return new bool4x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00028453 File Offset: 0x00026653
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator |(bool lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0002847E File Offset: 0x0002667E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ^(bool4x3 lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000284B8 File Offset: 0x000266B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ^(bool4x3 lhs, bool rhs)
		{
			return new bool4x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000284E3 File Offset: 0x000266E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ^(bool lhs, bool4x3 rhs)
		{
			return new bool4x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x170001EC RID: 492
		public unsafe ref bool4 this[int index]
		{
			get
			{
				fixed (bool4x3* ptr = &this)
				{
					return ref *(bool4*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool4) / (IntPtr)sizeof(bool4x3));
				}
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0002852B File Offset: 0x0002672B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool4x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00028568 File Offset: 0x00026768
		public override bool Equals(object o)
		{
			if (o is bool4x3)
			{
				bool4x3 converted = (bool4x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x0002858D File Offset: 0x0002678D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x0002859C File Offset: 0x0002679C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c0.w,
				this.c1.w,
				this.c2.w
			});
		}

		// Token: 0x04000063 RID: 99
		public bool4 c0;

		// Token: 0x04000064 RID: 100
		public bool4 c1;

		// Token: 0x04000065 RID: 101
		public bool4 c2;
	}
}
