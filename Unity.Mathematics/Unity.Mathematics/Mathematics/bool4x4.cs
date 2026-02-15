using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200001B RID: 27
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool4x4 : IEquatable<bool4x4>
	{
		// Token: 0x06000B39 RID: 2873 RVA: 0x000286A1 File Offset: 0x000268A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x4(bool4 c0, bool4 c1, bool4 c2, bool4 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000286C0 File Offset: 0x000268C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13, bool m20, bool m21, bool m22, bool m23, bool m30, bool m31, bool m32, bool m33)
		{
			this.c0 = new bool4(m00, m10, m20, m30);
			this.c1 = new bool4(m01, m11, m21, m31);
			this.c2 = new bool4(m02, m12, m22, m32);
			this.c3 = new bool4(m03, m13, m23, m33);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00028716 File Offset: 0x00026916
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4x4(bool v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00009666 File Offset: 0x00007866
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool4x4(bool v)
		{
			return new bool4x4(v);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00028748 File Offset: 0x00026948
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(bool4x4 lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0002879E File Offset: 0x0002699E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(bool4x4 lhs, bool rhs)
		{
			return new bool4x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000287D5 File Offset: 0x000269D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(bool lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0002880C File Offset: 0x00026A0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(bool4x4 lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00028862 File Offset: 0x00026A62
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(bool4x4 lhs, bool rhs)
		{
			return new bool4x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00028899 File Offset: 0x00026A99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(bool lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000288D0 File Offset: 0x00026AD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !(bool4x4 val)
		{
			return new bool4x4(!val.c0, !val.c1, !val.c2, !val.c3);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00028904 File Offset: 0x00026B04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator &(bool4x4 lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002895A File Offset: 0x00026B5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator &(bool4x4 lhs, bool rhs)
		{
			return new bool4x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00028991 File Offset: 0x00026B91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator &(bool lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000289C8 File Offset: 0x00026BC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator |(bool4x4 lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00028A1E File Offset: 0x00026C1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator |(bool4x4 lhs, bool rhs)
		{
			return new bool4x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00028A55 File Offset: 0x00026C55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator |(bool lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00028A8C File Offset: 0x00026C8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ^(bool4x4 lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00028AE2 File Offset: 0x00026CE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ^(bool4x4 lhs, bool rhs)
		{
			return new bool4x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00028B19 File Offset: 0x00026D19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ^(bool lhs, bool4x4 rhs)
		{
			return new bool4x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x170001ED RID: 493
		public unsafe ref bool4 this[int index]
		{
			get
			{
				fixed (bool4x4* ptr = &this)
				{
					return ref *(bool4*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool4) / (IntPtr)sizeof(bool4x4));
				}
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00028B6C File Offset: 0x00026D6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool4x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00028BC8 File Offset: 0x00026DC8
		public override bool Equals(object o)
		{
			if (o is bool4x4)
			{
				bool4x4 converted = (bool4x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00028BED File Offset: 0x00026DED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00028BFC File Offset: 0x00026DFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c3.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c3.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c3.z,
				this.c0.w,
				this.c1.w,
				this.c2.w,
				this.c3.w
			});
		}

		// Token: 0x04000066 RID: 102
		public bool4 c0;

		// Token: 0x04000067 RID: 103
		public bool4 c1;

		// Token: 0x04000068 RID: 104
		public bool4 c2;

		// Token: 0x04000069 RID: 105
		public bool4 c3;
	}
}
