using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000016 RID: 22
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool3x4 : IEquatable<bool3x4>
	{
		// Token: 0x06000941 RID: 2369 RVA: 0x00024231 File Offset: 0x00022431
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x4(bool3 c0, bool3 c1, bool3 c2, bool3 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00024250 File Offset: 0x00022450
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13, bool m20, bool m21, bool m22, bool m23)
		{
			this.c0 = new bool3(m00, m10, m20);
			this.c1 = new bool3(m01, m11, m21);
			this.c2 = new bool3(m02, m12, m22);
			this.c3 = new bool3(m03, m13, m23);
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x0002429E File Offset: 0x0002249E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x4(bool v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00008DC6 File Offset: 0x00006FC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool3x4(bool v)
		{
			return new bool3x4(v);
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000242D0 File Offset: 0x000224D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(bool3x4 lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00024326 File Offset: 0x00022526
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(bool3x4 lhs, bool rhs)
		{
			return new bool3x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0002435D File Offset: 0x0002255D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(bool lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00024394 File Offset: 0x00022594
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(bool3x4 lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x000243EA File Offset: 0x000225EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(bool3x4 lhs, bool rhs)
		{
			return new bool3x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00024421 File Offset: 0x00022621
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(bool lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00024458 File Offset: 0x00022658
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !(bool3x4 val)
		{
			return new bool3x4(!val.c0, !val.c1, !val.c2, !val.c3);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0002448C File Offset: 0x0002268C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator &(bool3x4 lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x000244E2 File Offset: 0x000226E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator &(bool3x4 lhs, bool rhs)
		{
			return new bool3x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x00024519 File Offset: 0x00022719
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator &(bool lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x00024550 File Offset: 0x00022750
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator |(bool3x4 lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x000245A6 File Offset: 0x000227A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator |(bool3x4 lhs, bool rhs)
		{
			return new bool3x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x000245DD File Offset: 0x000227DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator |(bool lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x00024614 File Offset: 0x00022814
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ^(bool3x4 lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0002466A File Offset: 0x0002286A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ^(bool3x4 lhs, bool rhs)
		{
			return new bool3x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x000246A1 File Offset: 0x000228A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ^(bool lhs, bool3x4 rhs)
		{
			return new bool3x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x17000099 RID: 153
		public unsafe ref bool3 this[int index]
		{
			get
			{
				fixed (bool3x4* ptr = &this)
				{
					return ref *(bool3*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool3) / (IntPtr)sizeof(bool3x4));
				}
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x000246F4 File Offset: 0x000228F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool3x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00024750 File Offset: 0x00022950
		public override bool Equals(object o)
		{
			if (o is bool3x4)
			{
				bool3x4 converted = (bool3x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00024775 File Offset: 0x00022975
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00024784 File Offset: 0x00022984
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
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
				this.c3.z
			});
		}

		// Token: 0x04000055 RID: 85
		public bool3 c0;

		// Token: 0x04000056 RID: 86
		public bool3 c1;

		// Token: 0x04000057 RID: 87
		public bool3 c2;

		// Token: 0x04000058 RID: 88
		public bool3 c3;
	}
}
