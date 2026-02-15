using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000012 RID: 18
	[DebuggerTypeProxy(typeof(bool3.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool3 : IEquatable<bool3>
	{
		// Token: 0x06000871 RID: 2161 RVA: 0x00022799 File Offset: 0x00020999
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3(bool x, bool y, bool z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x000227B0 File Offset: 0x000209B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3(bool x, bool2 yz)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000227D1 File Offset: 0x000209D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3(bool2 xy, bool z)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000227F2 File Offset: 0x000209F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3(bool3 xyz)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00022818 File Offset: 0x00020A18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3(bool v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x000088C4 File Offset: 0x00006AC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool3(bool v)
		{
			return new bool3(v);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002282F File Offset: 0x00020A2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(bool3 lhs, bool3 rhs)
		{
			return new bool3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00022860 File Offset: 0x00020A60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(bool3 lhs, bool rhs)
		{
			return new bool3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00022882 File Offset: 0x00020A82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(bool lhs, bool3 rhs)
		{
			return new bool3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000228A4 File Offset: 0x00020AA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(bool3 lhs, bool3 rhs)
		{
			return new bool3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000228DE File Offset: 0x00020ADE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(bool3 lhs, bool rhs)
		{
			return new bool3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00022909 File Offset: 0x00020B09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(bool lhs, bool3 rhs)
		{
			return new bool3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00022934 File Offset: 0x00020B34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !(bool3 val)
		{
			return new bool3(!val.x, !val.y, !val.z);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00022956 File Offset: 0x00020B56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator &(bool3 lhs, bool3 rhs)
		{
			return new bool3(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z);
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00022984 File Offset: 0x00020B84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator &(bool3 lhs, bool rhs)
		{
			return new bool3(lhs.x && rhs, lhs.y && rhs, lhs.z && rhs);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000229A3 File Offset: 0x00020BA3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator &(bool lhs, bool3 rhs)
		{
			return new bool3(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z);
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x000229C2 File Offset: 0x00020BC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator |(bool3 lhs, bool3 rhs)
		{
			return new bool3(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000229F0 File Offset: 0x00020BF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator |(bool3 lhs, bool rhs)
		{
			return new bool3(lhs.x || rhs, lhs.y || rhs, lhs.z || rhs);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00022A0F File Offset: 0x00020C0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator |(bool lhs, bool3 rhs)
		{
			return new bool3(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00022A2E File Offset: 0x00020C2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ^(bool3 lhs, bool3 rhs)
		{
			return new bool3(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z);
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00022A5C File Offset: 0x00020C5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ^(bool3 lhs, bool rhs)
		{
			return new bool3(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00022A7B File Offset: 0x00020C7B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ^(bool lhs, bool3 rhs)
		{
			return new bool3(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00022A9A File Offset: 0x00020C9A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x00022AB9 File Offset: 0x00020CB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x00022AD8 File Offset: 0x00020CD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x00022AF7 File Offset: 0x00020CF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00022B16 File Offset: 0x00020D16
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x00022B35 File Offset: 0x00020D35
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600088D RID: 2189 RVA: 0x00022B54 File Offset: 0x00020D54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x00022B73 File Offset: 0x00020D73
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00022B92 File Offset: 0x00020D92
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x00022BB1 File Offset: 0x00020DB1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000891 RID: 2193 RVA: 0x00022BD0 File Offset: 0x00020DD0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x00022BEF File Offset: 0x00020DEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00022C0E File Offset: 0x00020E0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000894 RID: 2196 RVA: 0x00022C2D File Offset: 0x00020E2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00022C4C File Offset: 0x00020E4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000896 RID: 2198 RVA: 0x00022C6B File Offset: 0x00020E6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00022C8A File Offset: 0x00020E8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000898 RID: 2200 RVA: 0x00022CA9 File Offset: 0x00020EA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00022CC8 File Offset: 0x00020EC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00022CE7 File Offset: 0x00020EE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00022D06 File Offset: 0x00020F06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x00022D25 File Offset: 0x00020F25
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x00022D44 File Offset: 0x00020F44
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00022D63 File Offset: 0x00020F63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00022D82 File Offset: 0x00020F82
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00022DA1 File Offset: 0x00020FA1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00022DC0 File Offset: 0x00020FC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00022DDF File Offset: 0x00020FDF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x00022DFE File Offset: 0x00020FFE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00022E1D File Offset: 0x0002101D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00022E3C File Offset: 0x0002103C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00022E5B File Offset: 0x0002105B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00022E7A File Offset: 0x0002107A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00022E99 File Offset: 0x00021099
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x00022EB8 File Offset: 0x000210B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00022ED7 File Offset: 0x000210D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060008AB RID: 2219 RVA: 0x00022EF6 File Offset: 0x000210F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x00022F15 File Offset: 0x00021115
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x00022F34 File Offset: 0x00021134
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00022F53 File Offset: 0x00021153
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x00022F72 File Offset: 0x00021172
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00022F91 File Offset: 0x00021191
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x00022FB0 File Offset: 0x000211B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x00022FCF File Offset: 0x000211CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060008B3 RID: 2227 RVA: 0x00022FEE File Offset: 0x000211EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x0002300D File Offset: 0x0002120D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0002302C File Offset: 0x0002122C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0002304B File Offset: 0x0002124B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0002306A File Offset: 0x0002126A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00023089 File Offset: 0x00021289
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x000230A8 File Offset: 0x000212A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x000230C7 File Offset: 0x000212C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x000230E6 File Offset: 0x000212E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00023105 File Offset: 0x00021305
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x00023124 File Offset: 0x00021324
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00023143 File Offset: 0x00021343
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x00023162 File Offset: 0x00021362
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00023181 File Offset: 0x00021381
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x000231A0 File Offset: 0x000213A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x000231BF File Offset: 0x000213BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x000231DE File Offset: 0x000213DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x000231FD File Offset: 0x000213FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0002321C File Offset: 0x0002141C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x0002323B File Offset: 0x0002143B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0002325A File Offset: 0x0002145A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00023279 File Offset: 0x00021479
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00023298 File Offset: 0x00021498
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x000232B7 File Offset: 0x000214B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x000232D6 File Offset: 0x000214D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x000232F5 File Offset: 0x000214F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00023314 File Offset: 0x00021514
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00023333 File Offset: 0x00021533
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00023352 File Offset: 0x00021552
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00023371 File Offset: 0x00021571
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x00023390 File Offset: 0x00021590
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x000233AF File Offset: 0x000215AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x000233CE File Offset: 0x000215CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x000233ED File Offset: 0x000215ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x0002340C File Offset: 0x0002160C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0002342B File Offset: 0x0002162B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0002344A File Offset: 0x0002164A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00023469 File Offset: 0x00021669
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x00023482 File Offset: 0x00021682
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0002349B File Offset: 0x0002169B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x000234B4 File Offset: 0x000216B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x000234CD File Offset: 0x000216CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x000234E6 File Offset: 0x000216E6
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x000227F2 File Offset: 0x000209F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x000234FF File Offset: 0x000216FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x00023518 File Offset: 0x00021718
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x00023531 File Offset: 0x00021731
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x00023557 File Offset: 0x00021757
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.z, this.z);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00023570 File Offset: 0x00021770
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.x);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x00023589 File Offset: 0x00021789
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.y);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x000235A2 File Offset: 0x000217A2
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x000235BB File Offset: 0x000217BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x000235E1 File Offset: 0x000217E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.x);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x000235FA File Offset: 0x000217FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x00023613 File Offset: 0x00021813
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x0002362C File Offset: 0x0002182C
		// (set) Token: 0x060008EB RID: 2283 RVA: 0x00023645 File Offset: 0x00021845
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0002366B File Offset: 0x0002186B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00023684 File Offset: 0x00021884
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.z, this.z);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0002369D File Offset: 0x0002189D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x000236B6 File Offset: 0x000218B6
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x000236CF File Offset: 0x000218CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x000236F5 File Offset: 0x000218F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060008F2 RID: 2290 RVA: 0x0002370E File Offset: 0x0002190E
		// (set) Token: 0x060008F3 RID: 2291 RVA: 0x00023727 File Offset: 0x00021927
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x0002374D File Offset: 0x0002194D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x00023766 File Offset: 0x00021966
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.y, this.z);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x0002377F File Offset: 0x0002197F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.x);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00023798 File Offset: 0x00021998
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.y);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060008F8 RID: 2296 RVA: 0x000237B1 File Offset: 0x000219B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.z);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x000237CA File Offset: 0x000219CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.x, this.x);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x000237DD File Offset: 0x000219DD
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x000237F0 File Offset: 0x000219F0
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

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x0002380A File Offset: 0x00021A0A
		// (set) Token: 0x060008FD RID: 2301 RVA: 0x0002381D File Offset: 0x00021A1D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00023837 File Offset: 0x00021A37
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x0002384A File Offset: 0x00021A4A
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

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00023864 File Offset: 0x00021A64
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.y, this.y);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00023877 File Offset: 0x00021A77
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x0002388A File Offset: 0x00021A8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x000238A4 File Offset: 0x00021AA4
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x000238B7 File Offset: 0x00021AB7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x000238D1 File Offset: 0x00021AD1
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x000238E4 File Offset: 0x00021AE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x000238FE File Offset: 0x00021AFE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.z, this.z);
			}
		}

		// Token: 0x17000096 RID: 150
		public unsafe bool this[int index]
		{
			get
			{
				fixed (bool3* ptr = &this)
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

		// Token: 0x0600090A RID: 2314 RVA: 0x00023945 File Offset: 0x00021B45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool3 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z;
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00023974 File Offset: 0x00021B74
		public override bool Equals(object o)
		{
			if (o is bool3)
			{
				bool3 converted = (bool3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00023999 File Offset: 0x00021B99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x000239A6 File Offset: 0x00021BA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool3({0}, {1}, {2})", this.x, this.y, this.z);
		}

		// Token: 0x0400004A RID: 74
		[MarshalAs(UnmanagedType.U1)]
		public bool x;

		// Token: 0x0400004B RID: 75
		[MarshalAs(UnmanagedType.U1)]
		public bool y;

		// Token: 0x0400004C RID: 76
		[MarshalAs(UnmanagedType.U1)]
		public bool z;

		// Token: 0x02000013 RID: 19
		internal sealed class DebuggerProxy
		{
			// Token: 0x0600090E RID: 2318 RVA: 0x000239D3 File Offset: 0x00021BD3
			public DebuggerProxy(bool3 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
			}

			// Token: 0x0400004D RID: 77
			public bool x;

			// Token: 0x0400004E RID: 78
			public bool y;

			// Token: 0x0400004F RID: 79
			public bool z;
		}
	}
}
