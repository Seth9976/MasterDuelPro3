using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000017 RID: 23
	[DebuggerTypeProxy(typeof(bool4.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct bool4 : IEquatable<bool4>
	{
		// Token: 0x0600095A RID: 2394 RVA: 0x00024889 File Offset: 0x00022A89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool x, bool y, bool z, bool w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x000248A8 File Offset: 0x00022AA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool x, bool y, bool2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000248D0 File Offset: 0x00022AD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool x, bool2 yz, bool w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x000248F8 File Offset: 0x00022AF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool x, bool3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00024925 File Offset: 0x00022B25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool2 xy, bool z, bool w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002494D File Offset: 0x00022B4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool2 xy, bool2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002497F File Offset: 0x00022B7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool3 xyz, bool w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x000249AC File Offset: 0x00022BAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.y;
			this.z = xyzw.z;
			this.w = xyzw.w;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x000249DE File Offset: 0x00022BDE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4(bool v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0000908C File Offset: 0x0000728C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator bool4(bool v)
		{
			return new bool4(v);
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x000249FC File Offset: 0x00022BFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(bool4 lhs, bool4 rhs)
		{
			return new bool4(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x00024A3B File Offset: 0x00022C3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(bool4 lhs, bool rhs)
		{
			return new bool4(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x00024A66 File Offset: 0x00022C66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(bool lhs, bool4 rhs)
		{
			return new bool4(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x00024A94 File Offset: 0x00022C94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(bool4 lhs, bool4 rhs)
		{
			return new bool4(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w);
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00024AEA File Offset: 0x00022CEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(bool4 lhs, bool rhs)
		{
			return new bool4(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs);
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x00024B21 File Offset: 0x00022D21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(bool lhs, bool4 rhs)
		{
			return new bool4(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x00024B58 File Offset: 0x00022D58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !(bool4 val)
		{
			return new bool4(!val.x, !val.y, !val.z, !val.w);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00024B83 File Offset: 0x00022D83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator &(bool4 lhs, bool4 rhs)
		{
			return new bool4(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z, lhs.w & rhs.w);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00024BBE File Offset: 0x00022DBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator &(bool4 lhs, bool rhs)
		{
			return new bool4(lhs.x && rhs, lhs.y && rhs, lhs.z && rhs, lhs.w && rhs);
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x00024BE5 File Offset: 0x00022DE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator &(bool lhs, bool4 rhs)
		{
			return new bool4(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z, lhs & rhs.w);
		}

		// Token: 0x0600096E RID: 2414 RVA: 0x00024C0C File Offset: 0x00022E0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator |(bool4 lhs, bool4 rhs)
		{
			return new bool4(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z, lhs.w | rhs.w);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00024C47 File Offset: 0x00022E47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator |(bool4 lhs, bool rhs)
		{
			return new bool4(lhs.x || rhs, lhs.y || rhs, lhs.z || rhs, lhs.w || rhs);
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00024C6E File Offset: 0x00022E6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator |(bool lhs, bool4 rhs)
		{
			return new bool4(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z, lhs | rhs.w);
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00024C95 File Offset: 0x00022E95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ^(bool4 lhs, bool4 rhs)
		{
			return new bool4(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z, lhs.w ^ rhs.w);
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00024CD0 File Offset: 0x00022ED0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ^(bool4 lhs, bool rhs)
		{
			return new bool4(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs, lhs.w ^ rhs);
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00024CF7 File Offset: 0x00022EF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ^(bool lhs, bool4 rhs)
		{
			return new bool4(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z, lhs ^ rhs.w);
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00024D1E File Offset: 0x00022F1E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00024D3D File Offset: 0x00022F3D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00024D5C File Offset: 0x00022F5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00024D7B File Offset: 0x00022F7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000978 RID: 2424 RVA: 0x00024D9A File Offset: 0x00022F9A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00024DB9 File Offset: 0x00022FB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x00024DD8 File Offset: 0x00022FD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00024DF7 File Offset: 0x00022FF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.y, this.w);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x00024E16 File Offset: 0x00023016
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00024E35 File Offset: 0x00023035
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x00024E54 File Offset: 0x00023054
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x00024E73 File Offset: 0x00023073
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.z, this.w);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00024E92 File Offset: 0x00023092
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.w, this.x);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x00024EB1 File Offset: 0x000230B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.w, this.y);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00024ED0 File Offset: 0x000230D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.w, this.z);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x00024EEF File Offset: 0x000230EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.x, this.w, this.w);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00024F0E File Offset: 0x0002310E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00024F2D File Offset: 0x0002312D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00024F4C File Offset: 0x0002314C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x00024F6B File Offset: 0x0002316B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.x, this.w);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00024F8A File Offset: 0x0002318A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00024FA9 File Offset: 0x000231A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00024FC8 File Offset: 0x000231C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600098B RID: 2443 RVA: 0x00024FE7 File Offset: 0x000231E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.y, this.w);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00025006 File Offset: 0x00023206
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600098D RID: 2445 RVA: 0x00025025 File Offset: 0x00023225
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600098E RID: 2446 RVA: 0x00025044 File Offset: 0x00023244
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600098F RID: 2447 RVA: 0x00025063 File Offset: 0x00023263
		// (set) Token: 0x06000990 RID: 2448 RVA: 0x000249AC File Offset: 0x00022BAC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000991 RID: 2449 RVA: 0x00025082 File Offset: 0x00023282
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.w, this.x);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x000250A1 File Offset: 0x000232A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.w, this.y);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x000250C0 File Offset: 0x000232C0
		// (set) Token: 0x06000994 RID: 2452 RVA: 0x000250DF File Offset: 0x000232DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00025111 File Offset: 0x00023311
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.y, this.w, this.w);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00025130 File Offset: 0x00023330
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x0002514F File Offset: 0x0002334F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x0002516E File Offset: 0x0002336E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x0002518D File Offset: 0x0002338D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.x, this.w);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x000251AC File Offset: 0x000233AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x000251CB File Offset: 0x000233CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x000251EA File Offset: 0x000233EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00025209 File Offset: 0x00023409
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x00025228 File Offset: 0x00023428
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x0002525A File Offset: 0x0002345A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00025279 File Offset: 0x00023479
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00025298 File Offset: 0x00023498
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x000252B7 File Offset: 0x000234B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.z, this.w);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x000252D6 File Offset: 0x000234D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.w, this.x);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x000252F5 File Offset: 0x000234F5
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x00025314 File Offset: 0x00023514
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00025346 File Offset: 0x00023546
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.w, this.z);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00025365 File Offset: 0x00023565
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.z, this.w, this.w);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00025384 File Offset: 0x00023584
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.x, this.x);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x000253A3 File Offset: 0x000235A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.x, this.y);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x000253C2 File Offset: 0x000235C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.x, this.z);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x000253E1 File Offset: 0x000235E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.x, this.w);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00025400 File Offset: 0x00023600
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.y, this.x);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0002541F File Offset: 0x0002361F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.y, this.y);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0002543E File Offset: 0x0002363E
		// (set) Token: 0x060009AF RID: 2479 RVA: 0x0002545D File Offset: 0x0002365D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060009B0 RID: 2480 RVA: 0x0002548F File Offset: 0x0002368F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.y, this.w);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x000254AE File Offset: 0x000236AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.z, this.x);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x000254CD File Offset: 0x000236CD
		// (set) Token: 0x060009B3 RID: 2483 RVA: 0x000254EC File Offset: 0x000236EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060009B4 RID: 2484 RVA: 0x0002551E File Offset: 0x0002371E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.z, this.z);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x0002553D File Offset: 0x0002373D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.z, this.w);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060009B6 RID: 2486 RVA: 0x0002555C File Offset: 0x0002375C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.w, this.x);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x0002557B File Offset: 0x0002377B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.w, this.y);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0002559A File Offset: 0x0002379A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.w, this.z);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x000255B9 File Offset: 0x000237B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 xwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.x, this.w, this.w, this.w);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x000255D8 File Offset: 0x000237D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x000255F7 File Offset: 0x000237F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00025616 File Offset: 0x00023816
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00025635 File Offset: 0x00023835
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.x, this.w);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060009BE RID: 2494 RVA: 0x00025654 File Offset: 0x00023854
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x00025673 File Offset: 0x00023873
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060009C0 RID: 2496 RVA: 0x00025692 File Offset: 0x00023892
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060009C1 RID: 2497 RVA: 0x000256B1 File Offset: 0x000238B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.y, this.w);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x000256D0 File Offset: 0x000238D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060009C3 RID: 2499 RVA: 0x000256EF File Offset: 0x000238EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x0002570E File Offset: 0x0002390E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0002572D File Offset: 0x0002392D
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0002574C File Offset: 0x0002394C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0002577E File Offset: 0x0002397E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.w, this.x);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0002579D File Offset: 0x0002399D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.w, this.y);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x000257BC File Offset: 0x000239BC
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x000257DB File Offset: 0x000239DB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0002580D File Offset: 0x00023A0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.x, this.w, this.w);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0002582C File Offset: 0x00023A2C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0002584B File Offset: 0x00023A4B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0002586A File Offset: 0x00023A6A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00025889 File Offset: 0x00023A89
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.x, this.w);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060009D0 RID: 2512 RVA: 0x000258A8 File Offset: 0x00023AA8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x000258C7 File Offset: 0x00023AC7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x000258E6 File Offset: 0x00023AE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00025905 File Offset: 0x00023B05
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.y, this.w);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x00025924 File Offset: 0x00023B24
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00025943 File Offset: 0x00023B43
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00025962 File Offset: 0x00023B62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00025981 File Offset: 0x00023B81
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.z, this.w);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x000259A0 File Offset: 0x00023BA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.w, this.x);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x000259BF File Offset: 0x00023BBF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.w, this.y);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x000259DE File Offset: 0x00023BDE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.w, this.z);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x000259FD File Offset: 0x00023BFD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.y, this.w, this.w);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00025A1C File Offset: 0x00023C1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00025A3B File Offset: 0x00023C3B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00025A5A File Offset: 0x00023C5A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x00025A79 File Offset: 0x00023C79
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x00025A98 File Offset: 0x00023C98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x00025ACA File Offset: 0x00023CCA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00025AE9 File Offset: 0x00023CE9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x00025B08 File Offset: 0x00023D08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060009E4 RID: 2532 RVA: 0x00025B27 File Offset: 0x00023D27
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060009E5 RID: 2533 RVA: 0x00025B46 File Offset: 0x00023D46
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00025B65 File Offset: 0x00023D65
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x00025B84 File Offset: 0x00023D84
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00025BA3 File Offset: 0x00023DA3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x00025BC2 File Offset: 0x00023DC2
		// (set) Token: 0x060009EA RID: 2538 RVA: 0x00025BE1 File Offset: 0x00023DE1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x00025C13 File Offset: 0x00023E13
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x00025C32 File Offset: 0x00023E32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00025C51 File Offset: 0x00023E51
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 yzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.z, this.w, this.w);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x00025C70 File Offset: 0x00023E70
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.x, this.x);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x00025C8F File Offset: 0x00023E8F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.x, this.y);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00025CAE File Offset: 0x00023EAE
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x00025CCD File Offset: 0x00023ECD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00025CFF File Offset: 0x00023EFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x00025D1E File Offset: 0x00023F1E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.y, this.x);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00025D3D File Offset: 0x00023F3D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x00025D5C File Offset: 0x00023F5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00025D7B File Offset: 0x00023F7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x00025D9A File Offset: 0x00023F9A
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x00025DB9 File Offset: 0x00023FB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00025DEB File Offset: 0x00023FEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00025E0A File Offset: 0x0002400A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00025E29 File Offset: 0x00024029
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x00025E48 File Offset: 0x00024048
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00025E67 File Offset: 0x00024067
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x00025E86 File Offset: 0x00024086
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x00025EA5 File Offset: 0x000240A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 ywww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.y, this.w, this.w, this.w);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00025EC4 File Offset: 0x000240C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x00025EE3 File Offset: 0x000240E3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00025F02 File Offset: 0x00024102
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00025F21 File Offset: 0x00024121
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00025F40 File Offset: 0x00024140
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x00025F5F File Offset: 0x0002415F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x00025F7E File Offset: 0x0002417E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00025F9D File Offset: 0x0002419D
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x00025FBC File Offset: 0x000241BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x00025FEE File Offset: 0x000241EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x0002600D File Offset: 0x0002420D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002602C File Offset: 0x0002422C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x0002604B File Offset: 0x0002424B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002606A File Offset: 0x0002426A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x00026089 File Offset: 0x00024289
		// (set) Token: 0x06000A0F RID: 2575 RVA: 0x000260A8 File Offset: 0x000242A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000A10 RID: 2576 RVA: 0x000260DA File Offset: 0x000242DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x000260F9 File Offset: 0x000242F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.x, this.w, this.w);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00026118 File Offset: 0x00024318
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000A13 RID: 2579 RVA: 0x00026137 File Offset: 0x00024337
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00026156 File Offset: 0x00024356
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000A15 RID: 2581 RVA: 0x00026175 File Offset: 0x00024375
		// (set) Token: 0x06000A16 RID: 2582 RVA: 0x00026194 File Offset: 0x00024394
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x000261C6 File Offset: 0x000243C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x000261E5 File Offset: 0x000243E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x00026204 File Offset: 0x00024404
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x00026223 File Offset: 0x00024423
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00026242 File Offset: 0x00024442
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x00026261 File Offset: 0x00024461
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00026280 File Offset: 0x00024480
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002629F File Offset: 0x0002449F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x000262BE File Offset: 0x000244BE
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x000262DD File Offset: 0x000244DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0002630F File Offset: 0x0002450F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002632E File Offset: 0x0002452E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0002634D File Offset: 0x0002454D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.y, this.w, this.w);
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002636C File Offset: 0x0002456C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0002638B File Offset: 0x0002458B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x000263AA File Offset: 0x000245AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x000263C9 File Offset: 0x000245C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.x, this.w);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000A28 RID: 2600 RVA: 0x000263E8 File Offset: 0x000245E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000A29 RID: 2601 RVA: 0x00026407 File Offset: 0x00024607
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x00026426 File Offset: 0x00024626
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00026445 File Offset: 0x00024645
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00026464 File Offset: 0x00024664
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00026483 File Offset: 0x00024683
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x000264A2 File Offset: 0x000246A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x000264C1 File Offset: 0x000246C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x000264E0 File Offset: 0x000246E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x000264FF File Offset: 0x000246FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x0002651E File Offset: 0x0002471E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000A33 RID: 2611 RVA: 0x0002653D File Offset: 0x0002473D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.z, this.w, this.w);
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x0002655C File Offset: 0x0002475C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.x, this.x);
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0002657B File Offset: 0x0002477B
		// (set) Token: 0x06000A36 RID: 2614 RVA: 0x0002659A File Offset: 0x0002479A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000A37 RID: 2615 RVA: 0x000265CC File Offset: 0x000247CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.x, this.z);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x000265EB File Offset: 0x000247EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x0002660A File Offset: 0x0002480A
		// (set) Token: 0x06000A3A RID: 2618 RVA: 0x00026629 File Offset: 0x00024829
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000A3B RID: 2619 RVA: 0x0002665B File Offset: 0x0002485B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000A3C RID: 2620 RVA: 0x0002667A File Offset: 0x0002487A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x00026699 File Offset: 0x00024899
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x000266B8 File Offset: 0x000248B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x000266D7 File Offset: 0x000248D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x000266F6 File Offset: 0x000248F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00026715 File Offset: 0x00024915
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x00026734 File Offset: 0x00024934
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00026753 File Offset: 0x00024953
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000A44 RID: 2628 RVA: 0x00026772 File Offset: 0x00024972
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x00026791 File Offset: 0x00024991
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 zwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.z, this.w, this.w, this.w);
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x000267B0 File Offset: 0x000249B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x000267CF File Offset: 0x000249CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x000267EE File Offset: 0x000249EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0002680D File Offset: 0x00024A0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0002682C File Offset: 0x00024A2C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0002684B File Offset: 0x00024A4B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0002686A File Offset: 0x00024A6A
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x00026889 File Offset: 0x00024A89
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x000268BB File Offset: 0x00024ABB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x000268DA File Offset: 0x00024ADA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x000268F9 File Offset: 0x00024AF9
		// (set) Token: 0x06000A51 RID: 2641 RVA: 0x00026918 File Offset: 0x00024B18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0002694A File Offset: 0x00024B4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x00026969 File Offset: 0x00024B69
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x00026988 File Offset: 0x00024B88
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x000269A7 File Offset: 0x00024BA7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x000269C6 File Offset: 0x00024BC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000A57 RID: 2647 RVA: 0x000269E5 File Offset: 0x00024BE5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.x, this.w, this.w);
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000A58 RID: 2648 RVA: 0x00026A04 File Offset: 0x00024C04
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00026A23 File Offset: 0x00024C23
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00026A42 File Offset: 0x00024C42
		// (set) Token: 0x06000A5B RID: 2651 RVA: 0x00026A61 File Offset: 0x00024C61
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00026A93 File Offset: 0x00024C93
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.x, this.w);
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000A5D RID: 2653 RVA: 0x00026AB2 File Offset: 0x00024CB2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x00026AD1 File Offset: 0x00024CD1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00026AF0 File Offset: 0x00024CF0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00026B0F File Offset: 0x00024D0F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x00026B2E File Offset: 0x00024D2E
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x00026B4D File Offset: 0x00024D4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x00026B7F File Offset: 0x00024D7F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00026B9E File Offset: 0x00024D9E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00026BBD File Offset: 0x00024DBD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00026BDC File Offset: 0x00024DDC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00026BFB File Offset: 0x00024DFB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00026C1A File Offset: 0x00024E1A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00026C39 File Offset: 0x00024E39
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.y, this.w, this.w);
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00026C58 File Offset: 0x00024E58
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00026C77 File Offset: 0x00024E77
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x00026C96 File Offset: 0x00024E96
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00026CC8 File Offset: 0x00024EC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00026CE7 File Offset: 0x00024EE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.x, this.w);
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00026D06 File Offset: 0x00024F06
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x00026D25 File Offset: 0x00024F25
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00026D57 File Offset: 0x00024F57
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x00026D76 File Offset: 0x00024F76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00026D95 File Offset: 0x00024F95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00026DB4 File Offset: 0x00024FB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00026DD3 File Offset: 0x00024FD3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x00026DF2 File Offset: 0x00024FF2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00026E11 File Offset: 0x00025011
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00026E30 File Offset: 0x00025030
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00026E4F File Offset: 0x0002504F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000A7A RID: 2682 RVA: 0x00026E6E File Offset: 0x0002506E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00026E8D File Offset: 0x0002508D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.z, this.w, this.w);
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x00026EAC File Offset: 0x000250AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.x, this.x);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00026ECB File Offset: 0x000250CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.x, this.y);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x00026EEA File Offset: 0x000250EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.x, this.z);
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x00026F09 File Offset: 0x00025109
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x00026F28 File Offset: 0x00025128
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.y, this.x);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x00026F47 File Offset: 0x00025147
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00026F66 File Offset: 0x00025166
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x00026F85 File Offset: 0x00025185
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00026FA4 File Offset: 0x000251A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00026FC3 File Offset: 0x000251C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00026FE2 File Offset: 0x000251E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00027001 File Offset: 0x00025201
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00027020 File Offset: 0x00025220
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0002703F File Offset: 0x0002523F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0002705E File Offset: 0x0002525E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x0002707D File Offset: 0x0002527D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool4 wwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool4(this.w, this.w, this.w, this.w);
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0002709C File Offset: 0x0002529C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.x);
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x000270B5 File Offset: 0x000252B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.y);
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x000270CE File Offset: 0x000252CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.z);
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x000270E7 File Offset: 0x000252E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.x, this.w);
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00027100 File Offset: 0x00025300
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.x);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00027119 File Offset: 0x00025319
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.y);
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x00027132 File Offset: 0x00025332
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x0002714B File Offset: 0x0002534B
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

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x00027171 File Offset: 0x00025371
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x0002718A File Offset: 0x0002538A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x000271B0 File Offset: 0x000253B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.z, this.x);
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x000271C9 File Offset: 0x000253C9
		// (set) Token: 0x06000A98 RID: 2712 RVA: 0x000271E2 File Offset: 0x000253E2
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

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000A99 RID: 2713 RVA: 0x00027208 File Offset: 0x00025408
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.z, this.z);
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00027221 File Offset: 0x00025421
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x0002723A File Offset: 0x0002543A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x00027260 File Offset: 0x00025460
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.w, this.x);
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x00027279 File Offset: 0x00025479
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x00027292 File Offset: 0x00025492
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x000272B8 File Offset: 0x000254B8
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x000272D1 File Offset: 0x000254D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000272F7 File Offset: 0x000254F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 xww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.x, this.w, this.w);
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x00027310 File Offset: 0x00025510
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.x);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x00027329 File Offset: 0x00025529
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.y);
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00027342 File Offset: 0x00025542
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x0002735B File Offset: 0x0002555B
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

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x00027381 File Offset: 0x00025581
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x0002739A File Offset: 0x0002559A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x000273C0 File Offset: 0x000255C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.x);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000273D9 File Offset: 0x000255D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.y);
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x000273F2 File Offset: 0x000255F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.z);
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0002740B File Offset: 0x0002560B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.y, this.w);
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x00027424 File Offset: 0x00025624
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x0002743D File Offset: 0x0002563D
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

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x00027463 File Offset: 0x00025663
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.z, this.y);
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0002747C File Offset: 0x0002567C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.z, this.z);
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x00027495 File Offset: 0x00025695
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x000274AE File Offset: 0x000256AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000AB2 RID: 2738 RVA: 0x000274D4 File Offset: 0x000256D4
		// (set) Token: 0x06000AB3 RID: 2739 RVA: 0x000274ED File Offset: 0x000256ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 ywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000AB4 RID: 2740 RVA: 0x00027513 File Offset: 0x00025713
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 ywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.w, this.y);
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000AB5 RID: 2741 RVA: 0x0002752C File Offset: 0x0002572C
		// (set) Token: 0x06000AB6 RID: 2742 RVA: 0x00027545 File Offset: 0x00025745
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 ywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000AB7 RID: 2743 RVA: 0x0002756B File Offset: 0x0002576B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 yww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.y, this.w, this.w);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000AB8 RID: 2744 RVA: 0x00027584 File Offset: 0x00025784
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.x, this.x);
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000AB9 RID: 2745 RVA: 0x0002759D File Offset: 0x0002579D
		// (set) Token: 0x06000ABA RID: 2746 RVA: 0x000275B6 File Offset: 0x000257B6
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

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000ABB RID: 2747 RVA: 0x000275DC File Offset: 0x000257DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.x, this.z);
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000ABC RID: 2748 RVA: 0x000275F5 File Offset: 0x000257F5
		// (set) Token: 0x06000ABD RID: 2749 RVA: 0x0002760E File Offset: 0x0002580E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00027634 File Offset: 0x00025834
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x0002764D File Offset: 0x0002584D
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

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x00027673 File Offset: 0x00025873
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.y, this.y);
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0002768C File Offset: 0x0002588C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.y, this.z);
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x000276A5 File Offset: 0x000258A5
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x000276BE File Offset: 0x000258BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000AC4 RID: 2756 RVA: 0x000276E4 File Offset: 0x000258E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.x);
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x000276FD File Offset: 0x000258FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.y);
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00027716 File Offset: 0x00025916
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.z);
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x0002772F File Offset: 0x0002592F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.z, this.w);
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00027748 File Offset: 0x00025948
		// (set) Token: 0x06000AC9 RID: 2761 RVA: 0x00027761 File Offset: 0x00025961
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00027787 File Offset: 0x00025987
		// (set) Token: 0x06000ACB RID: 2763 RVA: 0x000277A0 File Offset: 0x000259A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x000277C6 File Offset: 0x000259C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.w, this.z);
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000277DF File Offset: 0x000259DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 zww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.z, this.w, this.w);
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x000277F8 File Offset: 0x000259F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.x, this.x);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x00027811 File Offset: 0x00025A11
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x0002782A File Offset: 0x00025A2A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00027850 File Offset: 0x00025A50
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x00027869 File Offset: 0x00025A69
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002788F File Offset: 0x00025A8F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.x, this.w);
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x000278A8 File Offset: 0x00025AA8
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x000278C1 File Offset: 0x00025AC1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x000278E7 File Offset: 0x00025AE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.y, this.y);
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00027900 File Offset: 0x00025B00
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x00027919 File Offset: 0x00025B19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0002793F File Offset: 0x00025B3F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.y, this.w);
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00027958 File Offset: 0x00025B58
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x00027971 File Offset: 0x00025B71
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00027997 File Offset: 0x00025B97
		// (set) Token: 0x06000ADD RID: 2781 RVA: 0x000279B0 File Offset: 0x00025BB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x000279D6 File Offset: 0x00025BD6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.z, this.z);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000ADF RID: 2783 RVA: 0x000279EF File Offset: 0x00025BEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.z, this.w);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00027A08 File Offset: 0x00025C08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.w, this.x);
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x00027A21 File Offset: 0x00025C21
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.w, this.y);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00027A3A File Offset: 0x00025C3A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 wwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.w, this.z);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x00027A53 File Offset: 0x00025C53
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool3 www
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool3(this.w, this.w, this.w);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000AE4 RID: 2788 RVA: 0x00027A6C File Offset: 0x00025C6C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.x, this.x);
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00027A7F File Offset: 0x00025C7F
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x00027A92 File Offset: 0x00025C92
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

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00027AAC File Offset: 0x00025CAC
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x00027ABF File Offset: 0x00025CBF
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

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00027AD9 File Offset: 0x00025CD9
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x00027AEC File Offset: 0x00025CEC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 xw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00027B06 File Offset: 0x00025D06
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x00027B19 File Offset: 0x00025D19
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

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x00027B33 File Offset: 0x00025D33
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.y, this.y);
			}
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000AEE RID: 2798 RVA: 0x00027B46 File Offset: 0x00025D46
		// (set) Token: 0x06000AEF RID: 2799 RVA: 0x00027B59 File Offset: 0x00025D59
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

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000AF0 RID: 2800 RVA: 0x00027B73 File Offset: 0x00025D73
		// (set) Token: 0x06000AF1 RID: 2801 RVA: 0x00027B86 File Offset: 0x00025D86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 yw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000AF2 RID: 2802 RVA: 0x00027BA0 File Offset: 0x00025DA0
		// (set) Token: 0x06000AF3 RID: 2803 RVA: 0x00027BB3 File Offset: 0x00025DB3
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00027BCD File Offset: 0x00025DCD
		// (set) Token: 0x06000AF5 RID: 2805 RVA: 0x00027BE0 File Offset: 0x00025DE0
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

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00027BFA File Offset: 0x00025DFA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.z, this.z);
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00027C0D File Offset: 0x00025E0D
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x00027C20 File Offset: 0x00025E20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 zw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x00027C3A File Offset: 0x00025E3A
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x00027C4D File Offset: 0x00025E4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 wx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x00027C67 File Offset: 0x00025E67
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x00027C7A File Offset: 0x00025E7A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 wy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x00027C94 File Offset: 0x00025E94
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x00027CA7 File Offset: 0x00025EA7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 wz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x00027CC1 File Offset: 0x00025EC1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool2 ww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new bool2(this.w, this.w);
			}
		}

		// Token: 0x170001EA RID: 490
		public unsafe bool this[int index]
		{
			get
			{
				fixed (bool4* ptr = &this)
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

		// Token: 0x06000B02 RID: 2818 RVA: 0x00027D05 File Offset: 0x00025F05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(bool4 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z && this.w == rhs.w;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00027D44 File Offset: 0x00025F44
		public override bool Equals(object o)
		{
			if (o is bool4)
			{
				bool4 converted = (bool4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00027D69 File Offset: 0x00025F69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x00027D78 File Offset: 0x00025F78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("bool4({0}, {1}, {2}, {3})", new object[] { this.x, this.y, this.z, this.w });
		}

		// Token: 0x04000059 RID: 89
		[MarshalAs(UnmanagedType.U1)]
		public bool x;

		// Token: 0x0400005A RID: 90
		[MarshalAs(UnmanagedType.U1)]
		public bool y;

		// Token: 0x0400005B RID: 91
		[MarshalAs(UnmanagedType.U1)]
		public bool z;

		// Token: 0x0400005C RID: 92
		[MarshalAs(UnmanagedType.U1)]
		public bool w;

		// Token: 0x02000018 RID: 24
		internal sealed class DebuggerProxy
		{
			// Token: 0x06000B06 RID: 2822 RVA: 0x00027DCD File Offset: 0x00025FCD
			public DebuggerProxy(bool4 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
				this.w = v.w;
			}

			// Token: 0x0400005D RID: 93
			public bool x;

			// Token: 0x0400005E RID: 94
			public bool y;

			// Token: 0x0400005F RID: 95
			public bool z;

			// Token: 0x04000060 RID: 96
			public bool w;
		}
	}
}
