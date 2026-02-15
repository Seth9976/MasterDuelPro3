using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000015 RID: 21
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool3x3 : IEquatable<bool3x3>
	{
		// Token: 0x06000928 RID: 2344 RVA: 0x00023D73 File Offset: 0x00021F73
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x3(bool3 c0, bool3 c1, bool3 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x00023D8A File Offset: 0x00021F8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12, bool m20, bool m21, bool m22)
		{
			this.c0 = new bool3(m00, m10, m20);
			this.c1 = new bool3(m01, m11, m21);
			this.c2 = new bool3(m02, m12, m22);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00023DBC File Offset: 0x00021FBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x3(bool v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x00008BAC File Offset: 0x00006DAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool3x3(bool v)
		{
			return new bool3x3(v);
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x00023DE2 File Offset: 0x00021FE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(bool3x3 lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00023E1C File Offset: 0x0002201C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(bool3x3 lhs, bool rhs)
		{
			return new bool3x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x00023E47 File Offset: 0x00022047
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(bool lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00023E72 File Offset: 0x00022072
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(bool3x3 lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00023EAC File Offset: 0x000220AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(bool3x3 lhs, bool rhs)
		{
			return new bool3x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x00023ED7 File Offset: 0x000220D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(bool lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00023F02 File Offset: 0x00022102
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !(bool3x3 val)
		{
			return new bool3x3(!val.c0, !val.c1, !val.c2);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00023F2A File Offset: 0x0002212A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator &(bool3x3 lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00023F64 File Offset: 0x00022164
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator &(bool3x3 lhs, bool rhs)
		{
			return new bool3x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00023F8F File Offset: 0x0002218F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator &(bool lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00023FBA File Offset: 0x000221BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator |(bool3x3 lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00023FF4 File Offset: 0x000221F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator |(bool3x3 lhs, bool rhs)
		{
			return new bool3x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0002401F File Offset: 0x0002221F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator |(bool lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x0002404A File Offset: 0x0002224A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ^(bool3x3 lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00024084 File Offset: 0x00022284
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ^(bool3x3 lhs, bool rhs)
		{
			return new bool3x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x000240AF File Offset: 0x000222AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ^(bool lhs, bool3x3 rhs)
		{
			return new bool3x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x17000098 RID: 152
		public unsafe ref bool3 this[int index]
		{
			get
			{
				fixed (bool3x3* ptr = &this)
				{
					return ref *(bool3*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool3) / (IntPtr)sizeof(bool3x3));
				}
			}
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x000240F7 File Offset: 0x000222F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool3x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00024134 File Offset: 0x00022334
		public override bool Equals(object o)
		{
			if (o is bool3x3)
			{
				bool3x3 converted = (bool3x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00024159 File Offset: 0x00022359
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00024168 File Offset: 0x00022368
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool3x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z
			});
		}

		// Token: 0x04000052 RID: 82
		public bool3 c0;

		// Token: 0x04000053 RID: 83
		public bool3 c1;

		// Token: 0x04000054 RID: 84
		public bool3 c2;
	}
}
