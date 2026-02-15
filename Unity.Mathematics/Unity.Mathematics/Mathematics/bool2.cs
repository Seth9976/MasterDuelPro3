using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200000D RID: 13
	[DebuggerTypeProxy(typeof(bool2.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool2 : IEquatable<bool2>
	{
		// Token: 0x060007ED RID: 2029 RVA: 0x000213FA File Offset: 0x0001F5FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2(bool x, bool y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002140A File Offset: 0x0001F60A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2(bool2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00021424 File Offset: 0x0001F624
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2(bool v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0000829B File Offset: 0x0000649B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool2(bool v)
		{
			return new bool2(v);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00021434 File Offset: 0x0001F634
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(bool2 lhs, bool2 rhs)
		{
			return new bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00021457 File Offset: 0x0001F657
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(bool2 lhs, bool rhs)
		{
			return new bool2(lhs.x == rhs, lhs.y == rhs);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00021470 File Offset: 0x0001F670
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(bool lhs, bool2 rhs)
		{
			return new bool2(lhs == rhs.x, lhs == rhs.y);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00021489 File Offset: 0x0001F689
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(bool2 lhs, bool2 rhs)
		{
			return new bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000214B2 File Offset: 0x0001F6B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(bool2 lhs, bool rhs)
		{
			return new bool2(lhs.x != rhs, lhs.y != rhs);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x000214D1 File Offset: 0x0001F6D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(bool lhs, bool2 rhs)
		{
			return new bool2(lhs != rhs.x, lhs != rhs.y);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000214F0 File Offset: 0x0001F6F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !(bool2 val)
		{
			return new bool2(!val.x, !val.y);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00021509 File Offset: 0x0001F709
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator &(bool2 lhs, bool2 rhs)
		{
			return new bool2(lhs.x & rhs.x, lhs.y & rhs.y);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0002152A File Offset: 0x0001F72A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator &(bool2 lhs, bool rhs)
		{
			return new bool2(lhs.x && rhs, lhs.y && rhs);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00021541 File Offset: 0x0001F741
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator &(bool lhs, bool2 rhs)
		{
			return new bool2(lhs & rhs.x, lhs & rhs.y);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00021558 File Offset: 0x0001F758
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator |(bool2 lhs, bool2 rhs)
		{
			return new bool2(lhs.x | rhs.x, lhs.y | rhs.y);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00021579 File Offset: 0x0001F779
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator |(bool2 lhs, bool rhs)
		{
			return new bool2(lhs.x || rhs, lhs.y || rhs);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00021590 File Offset: 0x0001F790
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator |(bool lhs, bool2 rhs)
		{
			return new bool2(lhs | rhs.x, lhs | rhs.y);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x000215A7 File Offset: 0x0001F7A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ^(bool2 lhs, bool2 rhs)
		{
			return new bool2(lhs.x ^ rhs.x, lhs.y ^ rhs.y);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x000215C8 File Offset: 0x0001F7C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ^(bool2 lhs, bool rhs)
		{
			return new bool2(lhs.x ^ rhs, lhs.y ^ rhs);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000215DF File Offset: 0x0001F7DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ^(bool lhs, bool2 rhs)
		{
			return new bool2(lhs ^ rhs.x, lhs ^ rhs.y);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x000215F6 File Offset: 0x0001F7F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00021615 File Offset: 0x0001F815
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x00021634 File Offset: 0x0001F834
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00021653 File Offset: 0x0001F853
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x00021672 File Offset: 0x0001F872
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00021691 File Offset: 0x0001F891
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x000216B0 File Offset: 0x0001F8B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x000216CF File Offset: 0x0001F8CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x000216EE File Offset: 0x0001F8EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0002170D File Offset: 0x0001F90D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x0002172C File Offset: 0x0001F92C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0002174B File Offset: 0x0001F94B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x0002176A File Offset: 0x0001F96A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00021789 File Offset: 0x0001F989
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x000217A8 File Offset: 0x0001F9A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x000217C7 File Offset: 0x0001F9C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x000217E6 File Offset: 0x0001F9E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x000217FF File Offset: 0x0001F9FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00021818 File Offset: 0x0001FA18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00021831 File Offset: 0x0001FA31
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x0002184A File Offset: 0x0001FA4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00021863 File Offset: 0x0001FA63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x0002187C File Offset: 0x0001FA7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x00021895 File Offset: 0x0001FA95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x000218AE File Offset: 0x0001FAAE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.x, this.x);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x000218C1 File Offset: 0x0001FAC1
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x0002140A File Offset: 0x0001F60A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x000218D4 File Offset: 0x0001FAD4
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x000218E7 File Offset: 0x0001FAE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00021901 File Offset: 0x0001FB01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.y, this.y);
			}
		}

		// Token: 0x1700001D RID: 29
		public unsafe bool this[int index]
		{
			get
			{
				fixed (bool2* ptr = &this)
				{
					return ((byte*)ptr)[index] != 0;
				}
			}
			set
			{
				fixed (bool* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00021945 File Offset: 0x0001FB45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool2 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00021968 File Offset: 0x0001FB68
		public override bool Equals(object o)
		{
			if (o is bool2)
			{
				bool2 converted = (bool2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0002198D File Offset: 0x0001FB8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0002199A File Offset: 0x0001FB9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool2({0}, {1})", this.x, this.y);
		}

		// Token: 0x0400003D RID: 61
		[MarshalAs(UnmanagedType.U1)]
		public bool x;

		// Token: 0x0400003E RID: 62
		[MarshalAs(UnmanagedType.U1)]
		public bool y;

		// Token: 0x0200000E RID: 14
		internal sealed class DebuggerProxy
		{
			// Token: 0x06000825 RID: 2085 RVA: 0x000219BC File Offset: 0x0001FBBC
			public DebuggerProxy(bool2 v)
			{
				this.x = v.x;
				this.y = v.y;
			}

			// Token: 0x0400003F RID: 63
			public bool x;

			// Token: 0x04000040 RID: 64
			public bool y;
		}
	}
}
