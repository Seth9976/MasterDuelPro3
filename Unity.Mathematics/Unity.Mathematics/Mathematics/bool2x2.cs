using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200000F RID: 15
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool2x2 : IEquatable<bool2x2>
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x000219DC File Offset: 0x0001FBDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x2(bool2 c0, bool2 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000219EC File Offset: 0x0001FBEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x2(bool m00, bool m01, bool m10, bool m11)
		{
			this.c0 = new bool2(m00, m10);
			this.c1 = new bool2(m01, m11);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00021A09 File Offset: 0x0001FC09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x2(bool v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000083D9 File Offset: 0x000065D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool2x2(bool v)
		{
			return new bool2x2(v);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00021A23 File Offset: 0x0001FC23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(bool2x2 lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00021A4C File Offset: 0x0001FC4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(bool2x2 lhs, bool rhs)
		{
			return new bool2x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00021A6B File Offset: 0x0001FC6B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(bool lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00021A8A File Offset: 0x0001FC8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(bool2x2 lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00021AB3 File Offset: 0x0001FCB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(bool2x2 lhs, bool rhs)
		{
			return new bool2x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00021AD2 File Offset: 0x0001FCD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(bool lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00021AF1 File Offset: 0x0001FCF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !(bool2x2 val)
		{
			return new bool2x2(!val.c0, !val.c1);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00021B0E File Offset: 0x0001FD0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator &(bool2x2 lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00021B37 File Offset: 0x0001FD37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator &(bool2x2 lhs, bool rhs)
		{
			return new bool2x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00021B56 File Offset: 0x0001FD56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator &(bool lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00021B75 File Offset: 0x0001FD75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator |(bool2x2 lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00021B9E File Offset: 0x0001FD9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator |(bool2x2 lhs, bool rhs)
		{
			return new bool2x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00021BBD File Offset: 0x0001FDBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator |(bool lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00021BDC File Offset: 0x0001FDDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ^(bool2x2 lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00021C05 File Offset: 0x0001FE05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ^(bool2x2 lhs, bool rhs)
		{
			return new bool2x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00021C24 File Offset: 0x0001FE24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ^(bool lhs, bool2x2 rhs)
		{
			return new bool2x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x1700001E RID: 30
		public unsafe ref bool2 this[int index]
		{
			get
			{
				fixed (bool2x2* ptr = &this)
				{
					return ref *(bool2*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool2) / (IntPtr)sizeof(bool2x2));
				}
			}
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00021C5F File Offset: 0x0001FE5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool2x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00021C88 File Offset: 0x0001FE88
		public override bool Equals(object o)
		{
			if (o is bool2x2)
			{
				bool2x2 converted = (bool2x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00021CAD File Offset: 0x0001FEAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00021CBC File Offset: 0x0001FEBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool2x2({0}, {1},  {2}, {3})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y
			});
		}

		// Token: 0x04000041 RID: 65
		public bool2 c0;

		// Token: 0x04000042 RID: 66
		public bool2 c1;
	}
}
