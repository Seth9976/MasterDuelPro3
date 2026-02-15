using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000011 RID: 17
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool2x4 : IEquatable<bool2x4>
	{
		// Token: 0x06000858 RID: 2136 RVA: 0x000221A3 File Offset: 0x000203A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x4(bool2 c0, bool2 c1, bool2 c2, bool2 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000221C2 File Offset: 0x000203C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13)
		{
			this.c0 = new bool2(m00, m10);
			this.c1 = new bool2(m01, m11);
			this.c2 = new bool2(m02, m12);
			this.c3 = new bool2(m03, m13);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000221FD File Offset: 0x000203FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2x4(bool v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000086A4 File Offset: 0x000068A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool2x4(bool v)
		{
			return new bool2x4(v);
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00022230 File Offset: 0x00020430
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(bool2x4 lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00022286 File Offset: 0x00020486
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(bool2x4 lhs, bool rhs)
		{
			return new bool2x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000222BD File Offset: 0x000204BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(bool lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x000222F4 File Offset: 0x000204F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(bool2x4 lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002234A File Offset: 0x0002054A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(bool2x4 lhs, bool rhs)
		{
			return new bool2x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00022381 File Offset: 0x00020581
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(bool lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000223B8 File Offset: 0x000205B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !(bool2x4 val)
		{
			return new bool2x4(!val.c0, !val.c1, !val.c2, !val.c3);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x000223EC File Offset: 0x000205EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator &(bool2x4 lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00022442 File Offset: 0x00020642
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator &(bool2x4 lhs, bool rhs)
		{
			return new bool2x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00022479 File Offset: 0x00020679
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator &(bool lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000224B0 File Offset: 0x000206B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator |(bool2x4 lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00022506 File Offset: 0x00020706
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator |(bool2x4 lhs, bool rhs)
		{
			return new bool2x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0002253D File Offset: 0x0002073D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator |(bool lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00022574 File Offset: 0x00020774
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ^(bool2x4 lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000225CA File Offset: 0x000207CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ^(bool2x4 lhs, bool rhs)
		{
			return new bool2x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00022601 File Offset: 0x00020801
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ^(bool lhs, bool2x4 rhs)
		{
			return new bool2x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x17000020 RID: 32
		public unsafe ref bool2 this[int index]
		{
			get
			{
				fixed (bool2x4* ptr = &this)
				{
					return ref *(bool2*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool2) / (IntPtr)sizeof(bool2x4));
				}
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00022654 File Offset: 0x00020854
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool2x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000226B0 File Offset: 0x000208B0
		public override bool Equals(object o)
		{
			if (o is bool2x4)
			{
				bool2x4 converted = (bool2x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000226D5 File Offset: 0x000208D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000226E4 File Offset: 0x000208E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool2x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c3.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c3.y
			});
		}

		// Token: 0x04000046 RID: 70
		public bool2 c0;

		// Token: 0x04000047 RID: 71
		public bool2 c1;

		// Token: 0x04000048 RID: 72
		public bool2 c2;

		// Token: 0x04000049 RID: 73
		public bool2 c3;
	}
}
