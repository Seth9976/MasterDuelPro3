using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000054 RID: 84
	[DebuggerTypeProxy(typeof(uint2.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint2 : IEquatable<uint2>, IFormattable
	{
		// Token: 0x06001EF0 RID: 7920 RVA: 0x00059394 File Offset: 0x00057594
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(uint x, uint y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x000593A4 File Offset: 0x000575A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(uint2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x000593BE File Offset: 0x000575BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(uint v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x000593CE File Offset: 0x000575CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(bool v)
		{
			this.x = (v ? 1U : 0U);
			this.y = (v ? 1U : 0U);
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x000593EA File Offset: 0x000575EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(bool2 v)
		{
			this.x = (v.x ? 1U : 0U);
			this.y = (v.y ? 1U : 0U);
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x000593BE File Offset: 0x000575BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(int v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00059410 File Offset: 0x00057610
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(int2 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0005942A File Offset: 0x0005762A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(float v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0005943C File Offset: 0x0005763C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(float2 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0005942A File Offset: 0x0005762A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(double v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x00059458 File Offset: 0x00057658
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2(double2 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x0001FE33 File Offset: 0x0001E033
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint2(uint v)
		{
			return new uint2(v);
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x0001FE3B File Offset: 0x0001E03B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(bool v)
		{
			return new uint2(v);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x0001FE43 File Offset: 0x0001E043
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(bool2 v)
		{
			return new uint2(v);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x0001FE4B File Offset: 0x0001E04B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(int v)
		{
			return new uint2(v);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0001FE53 File Offset: 0x0001E053
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(int2 v)
		{
			return new uint2(v);
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x0001FE5B File Offset: 0x0001E05B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(float v)
		{
			return new uint2(v);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x0001FE63 File Offset: 0x0001E063
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(float2 v)
		{
			return new uint2(v);
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x0001FE6B File Offset: 0x0001E06B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(double v)
		{
			return new uint2(v);
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0001FE73 File Offset: 0x0001E073
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint2(double2 v)
		{
			return new uint2(v);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x00059474 File Offset: 0x00057674
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator *(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x00059495 File Offset: 0x00057695
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator *(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x * rhs, lhs.y * rhs);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000594AC File Offset: 0x000576AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator *(uint lhs, uint2 rhs)
		{
			return new uint2(lhs * rhs.x, lhs * rhs.y);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000594C3 File Offset: 0x000576C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator +(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000594E4 File Offset: 0x000576E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator +(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x + rhs, lhs.y + rhs);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x000594FB File Offset: 0x000576FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator +(uint lhs, uint2 rhs)
		{
			return new uint2(lhs + rhs.x, lhs + rhs.y);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x00059512 File Offset: 0x00057712
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator -(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x00059533 File Offset: 0x00057733
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator -(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x - rhs, lhs.y - rhs);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0005954A File Offset: 0x0005774A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator -(uint lhs, uint2 rhs)
		{
			return new uint2(lhs - rhs.x, lhs - rhs.y);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x00059561 File Offset: 0x00057761
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator /(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x / rhs.x, lhs.y / rhs.y);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x00059582 File Offset: 0x00057782
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator /(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x / rhs, lhs.y / rhs);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x00059599 File Offset: 0x00057799
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator /(uint lhs, uint2 rhs)
		{
			return new uint2(lhs / rhs.x, lhs / rhs.y);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x000595B0 File Offset: 0x000577B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator %(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x % rhs.x, lhs.y % rhs.y);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x000595D1 File Offset: 0x000577D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator %(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x % rhs, lhs.y % rhs);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x000595E8 File Offset: 0x000577E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator %(uint lhs, uint2 rhs)
		{
			return new uint2(lhs % rhs.x, lhs % rhs.y);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00059600 File Offset: 0x00057800
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator ++(uint2 val)
		{
			uint num = val.x + 1U;
			val.x = num;
			uint num2 = num;
			num = val.y + 1U;
			val.y = num;
			return new uint2(num2, num);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x00059630 File Offset: 0x00057830
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator --(uint2 val)
		{
			uint num = val.x - 1U;
			val.x = num;
			uint num2 = num;
			num = val.y - 1U;
			val.y = num;
			return new uint2(num2, num);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00059660 File Offset: 0x00057860
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(uint2 lhs, uint2 rhs)
		{
			return new bool2(lhs.x < rhs.x, lhs.y < rhs.y);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x00059683 File Offset: 0x00057883
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(uint2 lhs, uint rhs)
		{
			return new bool2(lhs.x < rhs, lhs.y < rhs);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0005969C File Offset: 0x0005789C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(uint lhs, uint2 rhs)
		{
			return new bool2(lhs < rhs.x, lhs < rhs.y);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000596B5 File Offset: 0x000578B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(uint2 lhs, uint2 rhs)
		{
			return new bool2(lhs.x <= rhs.x, lhs.y <= rhs.y);
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x000596DE File Offset: 0x000578DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(uint2 lhs, uint rhs)
		{
			return new bool2(lhs.x <= rhs, lhs.y <= rhs);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x000596FD File Offset: 0x000578FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(uint lhs, uint2 rhs)
		{
			return new bool2(lhs <= rhs.x, lhs <= rhs.y);
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0005971C File Offset: 0x0005791C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(uint2 lhs, uint2 rhs)
		{
			return new bool2(lhs.x > rhs.x, lhs.y > rhs.y);
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0005973F File Offset: 0x0005793F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(uint2 lhs, uint rhs)
		{
			return new bool2(lhs.x > rhs, lhs.y > rhs);
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00059758 File Offset: 0x00057958
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(uint lhs, uint2 rhs)
		{
			return new bool2(lhs > rhs.x, lhs > rhs.y);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x00059771 File Offset: 0x00057971
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(uint2 lhs, uint2 rhs)
		{
			return new bool2(lhs.x >= rhs.x, lhs.y >= rhs.y);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0005979A File Offset: 0x0005799A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(uint2 lhs, uint rhs)
		{
			return new bool2(lhs.x >= rhs, lhs.y >= rhs);
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000597B9 File Offset: 0x000579B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(uint lhs, uint2 rhs)
		{
			return new bool2(lhs >= rhs.x, lhs >= rhs.y);
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000597D8 File Offset: 0x000579D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator -(uint2 val)
		{
			return new uint2((uint)(-(uint)((ulong)val.x)), (uint)(-(uint)((ulong)val.y)));
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x000597F1 File Offset: 0x000579F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator +(uint2 val)
		{
			return new uint2(val.x, val.y);
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x00059804 File Offset: 0x00057A04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator <<(uint2 x, int n)
		{
			return new uint2(x.x << n, x.y << n);
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x00059821 File Offset: 0x00057A21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator >>(uint2 x, int n)
		{
			return new uint2(x.x >> n, x.y >> n);
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0005983E File Offset: 0x00057A3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(uint2 lhs, uint2 rhs)
		{
			return new bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x00059861 File Offset: 0x00057A61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(uint2 lhs, uint rhs)
		{
			return new bool2(lhs.x == rhs, lhs.y == rhs);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0005987A File Offset: 0x00057A7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(uint lhs, uint2 rhs)
		{
			return new bool2(lhs == rhs.x, lhs == rhs.y);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x00059893 File Offset: 0x00057A93
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(uint2 lhs, uint2 rhs)
		{
			return new bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x000598BC File Offset: 0x00057ABC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(uint2 lhs, uint rhs)
		{
			return new bool2(lhs.x != rhs, lhs.y != rhs);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000598DB File Offset: 0x00057ADB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(uint lhs, uint2 rhs)
		{
			return new bool2(lhs != rhs.x, lhs != rhs.y);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x000598FA File Offset: 0x00057AFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator ~(uint2 val)
		{
			return new uint2(~val.x, ~val.y);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0005990F File Offset: 0x00057B0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator &(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x & rhs.x, lhs.y & rhs.y);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00059930 File Offset: 0x00057B30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator &(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x & rhs, lhs.y & rhs);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00059947 File Offset: 0x00057B47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator &(uint lhs, uint2 rhs)
		{
			return new uint2(lhs & rhs.x, lhs & rhs.y);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0005995E File Offset: 0x00057B5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator |(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x | rhs.x, lhs.y | rhs.y);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0005997F File Offset: 0x00057B7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator |(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x | rhs, lhs.y | rhs);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00059996 File Offset: 0x00057B96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator |(uint lhs, uint2 rhs)
		{
			return new uint2(lhs | rhs.x, lhs | rhs.y);
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000599AD File Offset: 0x00057BAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator ^(uint2 lhs, uint2 rhs)
		{
			return new uint2(lhs.x ^ rhs.x, lhs.y ^ rhs.y);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x000599CE File Offset: 0x00057BCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator ^(uint2 lhs, uint rhs)
		{
			return new uint2(lhs.x ^ rhs, lhs.y ^ rhs);
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x000599E5 File Offset: 0x00057BE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 operator ^(uint lhs, uint2 rhs)
		{
			return new uint2(lhs ^ rhs.x, lhs ^ rhs.y);
		}

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x000599FC File Offset: 0x00057BFC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x00059A1B File Offset: 0x00057C1B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x00059A3A File Offset: 0x00057C3A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06001F38 RID: 7992 RVA: 0x00059A59 File Offset: 0x00057C59
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x00059A78 File Offset: 0x00057C78
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06001F3A RID: 7994 RVA: 0x00059A97 File Offset: 0x00057C97
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x00059AB6 File Offset: 0x00057CB6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x06001F3C RID: 7996 RVA: 0x00059AD5 File Offset: 0x00057CD5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001F3D RID: 7997 RVA: 0x00059AF4 File Offset: 0x00057CF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001F3E RID: 7998 RVA: 0x00059B13 File Offset: 0x00057D13
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06001F3F RID: 7999 RVA: 0x00059B32 File Offset: 0x00057D32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06001F40 RID: 8000 RVA: 0x00059B51 File Offset: 0x00057D51
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001F41 RID: 8001 RVA: 0x00059B70 File Offset: 0x00057D70
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001F42 RID: 8002 RVA: 0x00059B8F File Offset: 0x00057D8F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001F43 RID: 8003 RVA: 0x00059BAE File Offset: 0x00057DAE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001F44 RID: 8004 RVA: 0x00059BCD File Offset: 0x00057DCD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x00059BEC File Offset: 0x00057DEC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.x);
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x00059C05 File Offset: 0x00057E05
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.y);
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00059C1E File Offset: 0x00057E1E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.x);
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x00059C37 File Offset: 0x00057E37
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.y);
			}
		}

		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x00059C50 File Offset: 0x00057E50
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.x);
			}
		}

		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001F4A RID: 8010 RVA: 0x00059C69 File Offset: 0x00057E69
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.y);
			}
		}

		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x00059C82 File Offset: 0x00057E82
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.x);
			}
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06001F4C RID: 8012 RVA: 0x00059C9B File Offset: 0x00057E9B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.y);
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x00059CB4 File Offset: 0x00057EB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.x);
			}
		}

		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001F4E RID: 8014 RVA: 0x000597F1 File Offset: 0x000579F1
		// (set) Token: 0x06001F4F RID: 8015 RVA: 0x000593A4 File Offset: 0x000575A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x00059CC7 File Offset: 0x00057EC7
		// (set) Token: 0x06001F51 RID: 8017 RVA: 0x00059CDA File Offset: 0x00057EDA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x00059CF4 File Offset: 0x00057EF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.y);
			}
		}

		// Token: 0x170009B9 RID: 2489
		public unsafe uint this[int index]
		{
			get
			{
				fixed (uint2* ptr = &this)
				{
					return ((uint*)ptr)[index];
				}
			}
			set
			{
				fixed (uint* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x00059D40 File Offset: 0x00057F40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint2 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y;
		}

		// Token: 0x06001F56 RID: 8022 RVA: 0x00059D60 File Offset: 0x00057F60
		public override bool Equals(object o)
		{
			if (o is uint2)
			{
				uint2 converted = (uint2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001F57 RID: 8023 RVA: 0x00059D85 File Offset: 0x00057F85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001F58 RID: 8024 RVA: 0x00059D92 File Offset: 0x00057F92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint2({0}, {1})", this.x, this.y);
		}

		// Token: 0x06001F59 RID: 8025 RVA: 0x00059DB4 File Offset: 0x00057FB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint2({0}, {1})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider));
		}

		// Token: 0x0400013F RID: 319
		public uint x;

		// Token: 0x04000140 RID: 320
		public uint y;

		// Token: 0x04000141 RID: 321
		public static readonly uint2 zero;

		// Token: 0x02000055 RID: 85
		internal sealed class DebuggerProxy
		{
			// Token: 0x06001F5A RID: 8026 RVA: 0x00059DDA File Offset: 0x00057FDA
			public DebuggerProxy(uint2 v)
			{
				this.x = v.x;
				this.y = v.y;
			}

			// Token: 0x04000142 RID: 322
			public uint x;

			// Token: 0x04000143 RID: 323
			public uint y;
		}
	}
}
