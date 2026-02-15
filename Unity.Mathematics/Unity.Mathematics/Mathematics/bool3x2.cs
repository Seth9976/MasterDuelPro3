using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000014 RID: 20
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool3x2 : IEquatable<bool3x2>
	{
		// Token: 0x0600090F RID: 2319 RVA: 0x000239FF File Offset: 0x00021BFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x2(bool3 c0, bool3 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x00023A0F File Offset: 0x00021C0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x2(bool m00, bool m01, bool m10, bool m11, bool m20, bool m21)
		{
			this.c0 = new bool3(m00, m10, m20);
			this.c1 = new bool3(m01, m11, m21);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x00023A30 File Offset: 0x00021C30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3x2(bool v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x00008A2B File Offset: 0x00006C2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool3x2(bool v)
		{
			return new bool3x2(v);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00023A4A File Offset: 0x00021C4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(bool3x2 lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00023A73 File Offset: 0x00021C73
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(bool3x2 lhs, bool rhs)
		{
			return new bool3x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00023A92 File Offset: 0x00021C92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(bool lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x00023AB1 File Offset: 0x00021CB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(bool3x2 lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x00023ADA File Offset: 0x00021CDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(bool3x2 lhs, bool rhs)
		{
			return new bool3x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00023AF9 File Offset: 0x00021CF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(bool lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x00023B18 File Offset: 0x00021D18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !(bool3x2 val)
		{
			return new bool3x2(!val.c0, !val.c1);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00023B35 File Offset: 0x00021D35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator &(bool3x2 lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x00023B5E File Offset: 0x00021D5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator &(bool3x2 lhs, bool rhs)
		{
			return new bool3x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00023B7D File Offset: 0x00021D7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator &(bool lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00023B9C File Offset: 0x00021D9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator |(bool3x2 lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x00023BC5 File Offset: 0x00021DC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator |(bool3x2 lhs, bool rhs)
		{
			return new bool3x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x00023BE4 File Offset: 0x00021DE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator |(bool lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00023C03 File Offset: 0x00021E03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ^(bool3x2 lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00023C2C File Offset: 0x00021E2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ^(bool3x2 lhs, bool rhs)
		{
			return new bool3x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00023C4B File Offset: 0x00021E4B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ^(bool lhs, bool3x2 rhs)
		{
			return new bool3x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x17000097 RID: 151
		public unsafe ref bool3 this[int index]
		{
			get
			{
				fixed (bool3x2* ptr = &this)
				{
					return ref *(bool3*)(ptr + (IntPtr)index * (IntPtr)sizeof(bool3) / (IntPtr)sizeof(bool3x2));
				}
			}
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x00023C87 File Offset: 0x00021E87
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool3x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x00023CB0 File Offset: 0x00021EB0
		public override bool Equals(object o)
		{
			if (o is bool3x2)
			{
				bool3x2 converted = (bool3x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x00023CD5 File Offset: 0x00021ED5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x00023CE4 File Offset: 0x00021EE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z
			});
		}

		// Token: 0x04000050 RID: 80
		public bool3 c0;

		// Token: 0x04000051 RID: 81
		public bool3 c1;
	}
}
