using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200004B RID: 75
	[DebuggerTypeProxy(typeof(int4.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct int4 : IEquatable<int4>, IFormattable
	{
		// Token: 0x06001BA2 RID: 7074 RVA: 0x000507C1 File Offset: 0x0004E9C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int x, int y, int z, int w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x000507E0 File Offset: 0x0004E9E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int x, int y, int2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x00050808 File Offset: 0x0004EA08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int x, int2 yz, int w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00050830 File Offset: 0x0004EA30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int x, int3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0005085D File Offset: 0x0004EA5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int2 xy, int z, int w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x00050885 File Offset: 0x0004EA85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int2 xy, int2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x000508B7 File Offset: 0x0004EAB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int3 xyz, int w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x000508E4 File Offset: 0x0004EAE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.y;
			this.z = xyzw.z;
			this.w = xyzw.w;
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x00050916 File Offset: 0x0004EB16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(int v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x00050934 File Offset: 0x0004EB34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(bool v)
		{
			this.x = (v ? 1 : 0);
			this.y = (v ? 1 : 0);
			this.z = (v ? 1 : 0);
			this.w = (v ? 1 : 0);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x0005096C File Offset: 0x0004EB6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(bool4 v)
		{
			this.x = (v.x ? 1 : 0);
			this.y = (v.y ? 1 : 0);
			this.z = (v.z ? 1 : 0);
			this.w = (v.w ? 1 : 0);
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00050916 File Offset: 0x0004EB16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(uint v)
		{
			this.x = (int)v;
			this.y = (int)v;
			this.z = (int)v;
			this.w = (int)v;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x000509C1 File Offset: 0x0004EBC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(uint4 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
			this.z = (int)v.z;
			this.w = (int)v.w;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x000509F3 File Offset: 0x0004EBF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(float v)
		{
			this.x = (int)v;
			this.y = (int)v;
			this.z = (int)v;
			this.w = (int)v;
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x00050A15 File Offset: 0x0004EC15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(float4 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
			this.z = (int)v.z;
			this.w = (int)v.w;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x000509F3 File Offset: 0x0004EBF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(double v)
		{
			this.x = (int)v;
			this.y = (int)v;
			this.z = (int)v;
			this.w = (int)v;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00050A4B File Offset: 0x0004EC4B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4(double4 v)
		{
			this.x = (int)v.x;
			this.y = (int)v.y;
			this.z = (int)v.z;
			this.w = (int)v.w;
		}

		// Token: 0x06001BB3 RID: 7091 RVA: 0x0000EC0A File Offset: 0x0000CE0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator int4(int v)
		{
			return new int4(v);
		}

		// Token: 0x06001BB4 RID: 7092 RVA: 0x0000EC12 File Offset: 0x0000CE12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(bool v)
		{
			return new int4(v);
		}

		// Token: 0x06001BB5 RID: 7093 RVA: 0x0000EC1A File Offset: 0x0000CE1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(bool4 v)
		{
			return new int4(v);
		}

		// Token: 0x06001BB6 RID: 7094 RVA: 0x0000EC22 File Offset: 0x0000CE22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(uint v)
		{
			return new int4(v);
		}

		// Token: 0x06001BB7 RID: 7095 RVA: 0x0000EC2A File Offset: 0x0000CE2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(uint4 v)
		{
			return new int4(v);
		}

		// Token: 0x06001BB8 RID: 7096 RVA: 0x0000EC32 File Offset: 0x0000CE32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(float v)
		{
			return new int4(v);
		}

		// Token: 0x06001BB9 RID: 7097 RVA: 0x0000EC3A File Offset: 0x0000CE3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(float4 v)
		{
			return new int4(v);
		}

		// Token: 0x06001BBA RID: 7098 RVA: 0x0000EC42 File Offset: 0x0000CE42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(double v)
		{
			return new int4(v);
		}

		// Token: 0x06001BBB RID: 7099 RVA: 0x0000EC4A File Offset: 0x0000CE4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator int4(double4 v)
		{
			return new int4(v);
		}

		// Token: 0x06001BBC RID: 7100 RVA: 0x00050A81 File Offset: 0x0004EC81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator *(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x00050ABC File Offset: 0x0004ECBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator *(int4 lhs, int rhs)
		{
			return new int4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
		}

		// Token: 0x06001BBE RID: 7102 RVA: 0x00050AE3 File Offset: 0x0004ECE3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator *(int lhs, int4 rhs)
		{
			return new int4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w);
		}

		// Token: 0x06001BBF RID: 7103 RVA: 0x00050B0A File Offset: 0x0004ED0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator +(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
		}

		// Token: 0x06001BC0 RID: 7104 RVA: 0x00050B45 File Offset: 0x0004ED45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator +(int4 lhs, int rhs)
		{
			return new int4(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs);
		}

		// Token: 0x06001BC1 RID: 7105 RVA: 0x00050B6C File Offset: 0x0004ED6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator +(int lhs, int4 rhs)
		{
			return new int4(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w);
		}

		// Token: 0x06001BC2 RID: 7106 RVA: 0x00050B93 File Offset: 0x0004ED93
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator -(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x00050BCE File Offset: 0x0004EDCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator -(int4 lhs, int rhs)
		{
			return new int4(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs);
		}

		// Token: 0x06001BC4 RID: 7108 RVA: 0x00050BF5 File Offset: 0x0004EDF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator -(int lhs, int4 rhs)
		{
			return new int4(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w);
		}

		// Token: 0x06001BC5 RID: 7109 RVA: 0x00050C1C File Offset: 0x0004EE1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator /(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w);
		}

		// Token: 0x06001BC6 RID: 7110 RVA: 0x00050C57 File Offset: 0x0004EE57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator /(int4 lhs, int rhs)
		{
			return new int4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
		}

		// Token: 0x06001BC7 RID: 7111 RVA: 0x00050C7E File Offset: 0x0004EE7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator /(int lhs, int4 rhs)
		{
			return new int4(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w);
		}

		// Token: 0x06001BC8 RID: 7112 RVA: 0x00050CA5 File Offset: 0x0004EEA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator %(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z, lhs.w % rhs.w);
		}

		// Token: 0x06001BC9 RID: 7113 RVA: 0x00050CE0 File Offset: 0x0004EEE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator %(int4 lhs, int rhs)
		{
			return new int4(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs, lhs.w % rhs);
		}

		// Token: 0x06001BCA RID: 7114 RVA: 0x00050D07 File Offset: 0x0004EF07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator %(int lhs, int4 rhs)
		{
			return new int4(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z, lhs % rhs.w);
		}

		// Token: 0x06001BCB RID: 7115 RVA: 0x00050D30 File Offset: 0x0004EF30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator ++(int4 val)
		{
			int num = val.x + 1;
			val.x = num;
			int num2 = num;
			num = val.y + 1;
			val.y = num;
			int num3 = num;
			num = val.z + 1;
			val.z = num;
			int num4 = num;
			num = val.w + 1;
			val.w = num;
			return new int4(num2, num3, num4, num);
		}

		// Token: 0x06001BCC RID: 7116 RVA: 0x00050D80 File Offset: 0x0004EF80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator --(int4 val)
		{
			int num = val.x - 1;
			val.x = num;
			int num2 = num;
			num = val.y - 1;
			val.y = num;
			int num3 = num;
			num = val.z - 1;
			val.z = num;
			int num4 = num;
			num = val.w - 1;
			val.w = num;
			return new int4(num2, num3, num4, num);
		}

		// Token: 0x06001BCD RID: 7117 RVA: 0x00050DCE File Offset: 0x0004EFCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(int4 lhs, int4 rhs)
		{
			return new bool4(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z, lhs.w < rhs.w);
		}

		// Token: 0x06001BCE RID: 7118 RVA: 0x00050E0D File Offset: 0x0004F00D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(int4 lhs, int rhs)
		{
			return new bool4(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs, lhs.w < rhs);
		}

		// Token: 0x06001BCF RID: 7119 RVA: 0x00050E38 File Offset: 0x0004F038
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(int lhs, int4 rhs)
		{
			return new bool4(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z, lhs < rhs.w);
		}

		// Token: 0x06001BD0 RID: 7120 RVA: 0x00050E64 File Offset: 0x0004F064
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(int4 lhs, int4 rhs)
		{
			return new bool4(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z, lhs.w <= rhs.w);
		}

		// Token: 0x06001BD1 RID: 7121 RVA: 0x00050EBA File Offset: 0x0004F0BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(int4 lhs, int rhs)
		{
			return new bool4(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs, lhs.w <= rhs);
		}

		// Token: 0x06001BD2 RID: 7122 RVA: 0x00050EF1 File Offset: 0x0004F0F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(int lhs, int4 rhs)
		{
			return new bool4(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z, lhs <= rhs.w);
		}

		// Token: 0x06001BD3 RID: 7123 RVA: 0x00050F28 File Offset: 0x0004F128
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(int4 lhs, int4 rhs)
		{
			return new bool4(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z, lhs.w > rhs.w);
		}

		// Token: 0x06001BD4 RID: 7124 RVA: 0x00050F67 File Offset: 0x0004F167
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(int4 lhs, int rhs)
		{
			return new bool4(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs, lhs.w > rhs);
		}

		// Token: 0x06001BD5 RID: 7125 RVA: 0x00050F92 File Offset: 0x0004F192
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(int lhs, int4 rhs)
		{
			return new bool4(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z, lhs > rhs.w);
		}

		// Token: 0x06001BD6 RID: 7126 RVA: 0x00050FC0 File Offset: 0x0004F1C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(int4 lhs, int4 rhs)
		{
			return new bool4(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z, lhs.w >= rhs.w);
		}

		// Token: 0x06001BD7 RID: 7127 RVA: 0x00051016 File Offset: 0x0004F216
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(int4 lhs, int rhs)
		{
			return new bool4(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs, lhs.w >= rhs);
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x0005104D File Offset: 0x0004F24D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(int lhs, int4 rhs)
		{
			return new bool4(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z, lhs >= rhs.w);
		}

		// Token: 0x06001BD9 RID: 7129 RVA: 0x00051084 File Offset: 0x0004F284
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator -(int4 val)
		{
			return new int4(-val.x, -val.y, -val.z, -val.w);
		}

		// Token: 0x06001BDA RID: 7130 RVA: 0x000510A7 File Offset: 0x0004F2A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator +(int4 val)
		{
			return new int4(val.x, val.y, val.z, val.w);
		}

		// Token: 0x06001BDB RID: 7131 RVA: 0x000510C6 File Offset: 0x0004F2C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator <<(int4 x, int n)
		{
			return new int4(x.x << n, x.y << n, x.z << n, x.w << n);
		}

		// Token: 0x06001BDC RID: 7132 RVA: 0x000510F9 File Offset: 0x0004F2F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator >>(int4 x, int n)
		{
			return new int4(x.x >> n, x.y >> n, x.z >> n, x.w >> n);
		}

		// Token: 0x06001BDD RID: 7133 RVA: 0x0005112C File Offset: 0x0004F32C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(int4 lhs, int4 rhs)
		{
			return new bool4(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w);
		}

		// Token: 0x06001BDE RID: 7134 RVA: 0x0005116B File Offset: 0x0004F36B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(int4 lhs, int rhs)
		{
			return new bool4(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs);
		}

		// Token: 0x06001BDF RID: 7135 RVA: 0x00051196 File Offset: 0x0004F396
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(int lhs, int4 rhs)
		{
			return new bool4(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w);
		}

		// Token: 0x06001BE0 RID: 7136 RVA: 0x000511C4 File Offset: 0x0004F3C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(int4 lhs, int4 rhs)
		{
			return new bool4(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w);
		}

		// Token: 0x06001BE1 RID: 7137 RVA: 0x0005121A File Offset: 0x0004F41A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(int4 lhs, int rhs)
		{
			return new bool4(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs);
		}

		// Token: 0x06001BE2 RID: 7138 RVA: 0x00051251 File Offset: 0x0004F451
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(int lhs, int4 rhs)
		{
			return new bool4(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w);
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x00051288 File Offset: 0x0004F488
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator ~(int4 val)
		{
			return new int4(~val.x, ~val.y, ~val.z, ~val.w);
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x000512AB File Offset: 0x0004F4AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator &(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z, lhs.w & rhs.w);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x000512E6 File Offset: 0x0004F4E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator &(int4 lhs, int rhs)
		{
			return new int4(lhs.x & rhs, lhs.y & rhs, lhs.z & rhs, lhs.w & rhs);
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x0005130D File Offset: 0x0004F50D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator &(int lhs, int4 rhs)
		{
			return new int4(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z, lhs & rhs.w);
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x00051334 File Offset: 0x0004F534
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator |(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z, lhs.w | rhs.w);
		}

		// Token: 0x06001BE8 RID: 7144 RVA: 0x0005136F File Offset: 0x0004F56F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator |(int4 lhs, int rhs)
		{
			return new int4(lhs.x | rhs, lhs.y | rhs, lhs.z | rhs, lhs.w | rhs);
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x00051396 File Offset: 0x0004F596
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator |(int lhs, int4 rhs)
		{
			return new int4(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z, lhs | rhs.w);
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x000513BD File Offset: 0x0004F5BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator ^(int4 lhs, int4 rhs)
		{
			return new int4(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z, lhs.w ^ rhs.w);
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000513F8 File Offset: 0x0004F5F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator ^(int4 lhs, int rhs)
		{
			return new int4(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs, lhs.w ^ rhs);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x0005141F File Offset: 0x0004F61F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 operator ^(int lhs, int4 rhs)
		{
			return new int4(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z, lhs ^ rhs.w);
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x00051446 File Offset: 0x0004F646
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x00051465 File Offset: 0x0004F665
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x00051484 File Offset: 0x0004F684
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x000514A3 File Offset: 0x0004F6A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x000514C2 File Offset: 0x0004F6C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x000514E1 File Offset: 0x0004F6E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x00051500 File Offset: 0x0004F700
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001BF4 RID: 7156 RVA: 0x0005151F File Offset: 0x0004F71F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x0005153E File Offset: 0x0004F73E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001BF6 RID: 7158 RVA: 0x0005155D File Offset: 0x0004F75D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x0005157C File Offset: 0x0004F77C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x06001BF8 RID: 7160 RVA: 0x0005159B File Offset: 0x0004F79B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x000515BA File Offset: 0x0004F7BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x06001BFA RID: 7162 RVA: 0x000515D9 File Offset: 0x0004F7D9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000857 RID: 2135
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x000515F8 File Offset: 0x0004F7F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000858 RID: 2136
		// (get) Token: 0x06001BFC RID: 7164 RVA: 0x00051617 File Offset: 0x0004F817
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000859 RID: 2137
		// (get) Token: 0x06001BFD RID: 7165 RVA: 0x00051636 File Offset: 0x0004F836
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700085A RID: 2138
		// (get) Token: 0x06001BFE RID: 7166 RVA: 0x00051655 File Offset: 0x0004F855
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06001BFF RID: 7167 RVA: 0x00051674 File Offset: 0x0004F874
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06001C00 RID: 7168 RVA: 0x00051693 File Offset: 0x0004F893
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.x, this.w);
			}
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001C01 RID: 7169 RVA: 0x000516B2 File Offset: 0x0004F8B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001C02 RID: 7170 RVA: 0x000516D1 File Offset: 0x0004F8D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001C03 RID: 7171 RVA: 0x000516F0 File Offset: 0x0004F8F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001C04 RID: 7172 RVA: 0x0005170F File Offset: 0x0004F90F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001C05 RID: 7173 RVA: 0x0005172E File Offset: 0x0004F92E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06001C06 RID: 7174 RVA: 0x0005174D File Offset: 0x0004F94D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0005176C File Offset: 0x0004F96C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x000510A7 File Offset: 0x0004F2A7
		// (set) Token: 0x06001C09 RID: 7177 RVA: 0x000508E4 File Offset: 0x0004EAE4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.z, this.w);
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

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x0005178B File Offset: 0x0004F98B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x000517AA File Offset: 0x0004F9AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x000517C9 File Offset: 0x0004F9C9
		// (set) Token: 0x06001C0D RID: 7181 RVA: 0x000517E8 File Offset: 0x0004F9E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.w, this.z);
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

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001C0E RID: 7182 RVA: 0x0005181A File Offset: 0x0004FA1A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001C0F RID: 7183 RVA: 0x00051839 File Offset: 0x0004FA39
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x00051858 File Offset: 0x0004FA58
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x00051877 File Offset: 0x0004FA77
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x00051896 File Offset: 0x0004FA96
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.x, this.w);
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x000518B5 File Offset: 0x0004FAB5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001C14 RID: 7188 RVA: 0x000518D4 File Offset: 0x0004FAD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x000518F3 File Offset: 0x0004FAF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001C16 RID: 7190 RVA: 0x00051912 File Offset: 0x0004FB12
		// (set) Token: 0x06001C17 RID: 7191 RVA: 0x00051931 File Offset: 0x0004FB31
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.y, this.w);
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

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x06001C18 RID: 7192 RVA: 0x00051963 File Offset: 0x0004FB63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06001C19 RID: 7193 RVA: 0x00051982 File Offset: 0x0004FB82
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x000519A1 File Offset: 0x0004FBA1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x000519C0 File Offset: 0x0004FBC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06001C1C RID: 7196 RVA: 0x000519DF File Offset: 0x0004FBDF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x000519FE File Offset: 0x0004FBFE
		// (set) Token: 0x06001C1E RID: 7198 RVA: 0x00051A1D File Offset: 0x0004FC1D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.w, this.y);
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

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x00051A4F File Offset: 0x0004FC4F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x00051A6E File Offset: 0x0004FC6E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x00051A8D File Offset: 0x0004FC8D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.x, this.x);
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x00051AAC File Offset: 0x0004FCAC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.x, this.y);
			}
		}

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x00051ACB File Offset: 0x0004FCCB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.x, this.z);
			}
		}

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06001C24 RID: 7204 RVA: 0x00051AEA File Offset: 0x0004FCEA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x00051B09 File Offset: 0x0004FD09
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.y, this.x);
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x06001C26 RID: 7206 RVA: 0x00051B28 File Offset: 0x0004FD28
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.y, this.y);
			}
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x00051B47 File Offset: 0x0004FD47
		// (set) Token: 0x06001C28 RID: 7208 RVA: 0x00051B66 File Offset: 0x0004FD66
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.y, this.z);
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

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x00051B98 File Offset: 0x0004FD98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x00051BB7 File Offset: 0x0004FDB7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x00051BD6 File Offset: 0x0004FDD6
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x00051BF5 File Offset: 0x0004FDF5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.z, this.y);
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

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x00051C27 File Offset: 0x0004FE27
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x00051C46 File Offset: 0x0004FE46
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x00051C65 File Offset: 0x0004FE65
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001C30 RID: 7216 RVA: 0x00051C84 File Offset: 0x0004FE84
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x00051CA3 File Offset: 0x0004FEA3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001C32 RID: 7218 RVA: 0x00051CC2 File Offset: 0x0004FEC2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 xwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.x, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001C33 RID: 7219 RVA: 0x00051CE1 File Offset: 0x0004FEE1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06001C34 RID: 7220 RVA: 0x00051D00 File Offset: 0x0004FF00
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x06001C35 RID: 7221 RVA: 0x00051D1F File Offset: 0x0004FF1F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x06001C36 RID: 7222 RVA: 0x00051D3E File Offset: 0x0004FF3E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06001C37 RID: 7223 RVA: 0x00051D5D File Offset: 0x0004FF5D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x06001C38 RID: 7224 RVA: 0x00051D7C File Offset: 0x0004FF7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06001C39 RID: 7225 RVA: 0x00051D9B File Offset: 0x0004FF9B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06001C3A RID: 7226 RVA: 0x00051DBA File Offset: 0x0004FFBA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001C3B RID: 7227 RVA: 0x00051DD9 File Offset: 0x0004FFD9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x06001C3C RID: 7228 RVA: 0x00051DF8 File Offset: 0x0004FFF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x06001C3D RID: 7229 RVA: 0x00051E17 File Offset: 0x00050017
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x00051E36 File Offset: 0x00050036
		// (set) Token: 0x06001C3F RID: 7231 RVA: 0x00051E55 File Offset: 0x00050055
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.z, this.w);
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

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x00051E87 File Offset: 0x00050087
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x00051EA6 File Offset: 0x000500A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x00051EC5 File Offset: 0x000500C5
		// (set) Token: 0x06001C43 RID: 7235 RVA: 0x00051EE4 File Offset: 0x000500E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.w, this.z);
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

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001C44 RID: 7236 RVA: 0x00051F16 File Offset: 0x00050116
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001C45 RID: 7237 RVA: 0x00051F35 File Offset: 0x00050135
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06001C46 RID: 7238 RVA: 0x00051F54 File Offset: 0x00050154
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06001C47 RID: 7239 RVA: 0x00051F73 File Offset: 0x00050173
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x00051F92 File Offset: 0x00050192
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.x, this.w);
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06001C49 RID: 7241 RVA: 0x00051FB1 File Offset: 0x000501B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x00051FD0 File Offset: 0x000501D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x00051FEF File Offset: 0x000501EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x0005200E File Offset: 0x0005020E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.y, this.w);
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x0005202D File Offset: 0x0005022D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001C4E RID: 7246 RVA: 0x0005204C File Offset: 0x0005024C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0005206B File Offset: 0x0005026B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001C50 RID: 7248 RVA: 0x0005208A File Offset: 0x0005028A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.z, this.w);
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x000520A9 File Offset: 0x000502A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.w, this.x);
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x000520C8 File Offset: 0x000502C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.w, this.y);
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001C53 RID: 7251 RVA: 0x000520E7 File Offset: 0x000502E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.w, this.z);
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001C54 RID: 7252 RVA: 0x00052106 File Offset: 0x00050306
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.y, this.w, this.w);
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x00052125 File Offset: 0x00050325
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001C56 RID: 7254 RVA: 0x00052144 File Offset: 0x00050344
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00052163 File Offset: 0x00050363
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001C58 RID: 7256 RVA: 0x00052182 File Offset: 0x00050382
		// (set) Token: 0x06001C59 RID: 7257 RVA: 0x000521A1 File Offset: 0x000503A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.x, this.w);
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

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001C5A RID: 7258 RVA: 0x000521D3 File Offset: 0x000503D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x000521F2 File Offset: 0x000503F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001C5C RID: 7260 RVA: 0x00052211 File Offset: 0x00050411
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00052230 File Offset: 0x00050430
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.y, this.w);
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x0005224F File Offset: 0x0005044F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x0005226E File Offset: 0x0005046E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x0005228D File Offset: 0x0005048D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x000522AC File Offset: 0x000504AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.z, this.w);
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x000522CB File Offset: 0x000504CB
		// (set) Token: 0x06001C63 RID: 7267 RVA: 0x000522EA File Offset: 0x000504EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.w, this.x);
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

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x0005231C File Offset: 0x0005051C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.w, this.y);
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x06001C65 RID: 7269 RVA: 0x0005233B File Offset: 0x0005053B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.w, this.z);
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x0005235A File Offset: 0x0005055A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 yzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.z, this.w, this.w);
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x06001C67 RID: 7271 RVA: 0x00052379 File Offset: 0x00050579
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.x, this.x);
			}
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00052398 File Offset: 0x00050598
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.x, this.y);
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x000523B7 File Offset: 0x000505B7
		// (set) Token: 0x06001C6A RID: 7274 RVA: 0x000523D6 File Offset: 0x000505D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.x, this.z);
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

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06001C6B RID: 7275 RVA: 0x00052408 File Offset: 0x00050608
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.x, this.w);
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06001C6C RID: 7276 RVA: 0x00052427 File Offset: 0x00050627
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.y, this.x);
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06001C6D RID: 7277 RVA: 0x00052446 File Offset: 0x00050646
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.y, this.y);
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06001C6E RID: 7278 RVA: 0x00052465 File Offset: 0x00050665
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.y, this.z);
			}
		}

		// Token: 0x170008C0 RID: 2240
		// (get) Token: 0x06001C6F RID: 7279 RVA: 0x00052484 File Offset: 0x00050684
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.y, this.w);
			}
		}

		// Token: 0x170008C1 RID: 2241
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x000524A3 File Offset: 0x000506A3
		// (set) Token: 0x06001C71 RID: 7281 RVA: 0x000524C2 File Offset: 0x000506C2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.z, this.x);
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

		// Token: 0x170008C2 RID: 2242
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x000524F4 File Offset: 0x000506F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.z, this.y);
			}
		}

		// Token: 0x170008C3 RID: 2243
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x00052513 File Offset: 0x00050713
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.z, this.z);
			}
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x06001C74 RID: 7284 RVA: 0x00052532 File Offset: 0x00050732
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.z, this.w);
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x00052551 File Offset: 0x00050751
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.w, this.x);
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x00052570 File Offset: 0x00050770
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.w, this.y);
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x0005258F File Offset: 0x0005078F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.w, this.z);
			}
		}

		// Token: 0x170008C8 RID: 2248
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x000525AE File Offset: 0x000507AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 ywww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.y, this.w, this.w, this.w);
			}
		}

		// Token: 0x170008C9 RID: 2249
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x000525CD File Offset: 0x000507CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x170008CA RID: 2250
		// (get) Token: 0x06001C7A RID: 7290 RVA: 0x000525EC File Offset: 0x000507EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x170008CB RID: 2251
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x0005260B File Offset: 0x0005080B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x170008CC RID: 2252
		// (get) Token: 0x06001C7C RID: 7292 RVA: 0x0005262A File Offset: 0x0005082A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.x, this.w);
			}
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x00052649 File Offset: 0x00050849
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001C7E RID: 7294 RVA: 0x00052668 File Offset: 0x00050868
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x00052687 File Offset: 0x00050887
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001C80 RID: 7296 RVA: 0x000526A6 File Offset: 0x000508A6
		// (set) Token: 0x06001C81 RID: 7297 RVA: 0x000526C5 File Offset: 0x000508C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.y, this.w);
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

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001C82 RID: 7298 RVA: 0x000526F7 File Offset: 0x000508F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x00052716 File Offset: 0x00050916
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001C84 RID: 7300 RVA: 0x00052735 File Offset: 0x00050935
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x00052754 File Offset: 0x00050954
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.z, this.w);
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001C86 RID: 7302 RVA: 0x00052773 File Offset: 0x00050973
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.w, this.x);
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x00052792 File Offset: 0x00050992
		// (set) Token: 0x06001C88 RID: 7304 RVA: 0x000527B1 File Offset: 0x000509B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.w, this.y);
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

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x000527E3 File Offset: 0x000509E3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.w, this.z);
			}
		}

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x00052802 File Offset: 0x00050A02
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.x, this.w, this.w);
			}
		}

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001C8B RID: 7307 RVA: 0x00052821 File Offset: 0x00050A21
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001C8C RID: 7308 RVA: 0x00052840 File Offset: 0x00050A40
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001C8D RID: 7309 RVA: 0x0005285F File Offset: 0x00050A5F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001C8E RID: 7310 RVA: 0x0005287E File Offset: 0x00050A7E
		// (set) Token: 0x06001C8F RID: 7311 RVA: 0x0005289D File Offset: 0x00050A9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.x, this.w);
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

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001C90 RID: 7312 RVA: 0x000528CF File Offset: 0x00050ACF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001C91 RID: 7313 RVA: 0x000528EE File Offset: 0x00050AEE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001C92 RID: 7314 RVA: 0x0005290D File Offset: 0x00050B0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x06001C93 RID: 7315 RVA: 0x0005292C File Offset: 0x00050B2C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.y, this.w);
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06001C94 RID: 7316 RVA: 0x0005294B File Offset: 0x00050B4B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06001C95 RID: 7317 RVA: 0x0005296A File Offset: 0x00050B6A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06001C96 RID: 7318 RVA: 0x00052989 File Offset: 0x00050B89
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06001C97 RID: 7319 RVA: 0x000529A8 File Offset: 0x00050BA8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.z, this.w);
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001C98 RID: 7320 RVA: 0x000529C7 File Offset: 0x00050BC7
		// (set) Token: 0x06001C99 RID: 7321 RVA: 0x000529E6 File Offset: 0x00050BE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.w, this.x);
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

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001C9A RID: 7322 RVA: 0x00052A18 File Offset: 0x00050C18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.w, this.y);
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001C9B RID: 7323 RVA: 0x00052A37 File Offset: 0x00050C37
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.w, this.z);
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001C9C RID: 7324 RVA: 0x00052A56 File Offset: 0x00050C56
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.y, this.w, this.w);
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06001C9D RID: 7325 RVA: 0x00052A75 File Offset: 0x00050C75
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06001C9E RID: 7326 RVA: 0x00052A94 File Offset: 0x00050C94
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06001C9F RID: 7327 RVA: 0x00052AB3 File Offset: 0x00050CB3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06001CA0 RID: 7328 RVA: 0x00052AD2 File Offset: 0x00050CD2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.x, this.w);
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001CA1 RID: 7329 RVA: 0x00052AF1 File Offset: 0x00050CF1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06001CA2 RID: 7330 RVA: 0x00052B10 File Offset: 0x00050D10
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06001CA3 RID: 7331 RVA: 0x00052B2F File Offset: 0x00050D2F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001CA4 RID: 7332 RVA: 0x00052B4E File Offset: 0x00050D4E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.y, this.w);
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001CA5 RID: 7333 RVA: 0x00052B6D File Offset: 0x00050D6D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001CA6 RID: 7334 RVA: 0x00052B8C File Offset: 0x00050D8C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001CA7 RID: 7335 RVA: 0x00052BAB File Offset: 0x00050DAB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x00052BCA File Offset: 0x00050DCA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.z, this.w);
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001CA9 RID: 7337 RVA: 0x00052BE9 File Offset: 0x00050DE9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.w, this.x);
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x00052C08 File Offset: 0x00050E08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.w, this.y);
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001CAB RID: 7339 RVA: 0x00052C27 File Offset: 0x00050E27
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.w, this.z);
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x00052C46 File Offset: 0x00050E46
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.z, this.w, this.w);
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001CAD RID: 7341 RVA: 0x00052C65 File Offset: 0x00050E65
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.x, this.x);
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x00052C84 File Offset: 0x00050E84
		// (set) Token: 0x06001CAF RID: 7343 RVA: 0x00052CA3 File Offset: 0x00050EA3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.x, this.y);
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

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x00052CD5 File Offset: 0x00050ED5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.x, this.z);
			}
		}

		// Token: 0x170008FC RID: 2300
		// (get) Token: 0x06001CB1 RID: 7345 RVA: 0x00052CF4 File Offset: 0x00050EF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.x, this.w);
			}
		}

		// Token: 0x170008FD RID: 2301
		// (get) Token: 0x06001CB2 RID: 7346 RVA: 0x00052D13 File Offset: 0x00050F13
		// (set) Token: 0x06001CB3 RID: 7347 RVA: 0x00052D32 File Offset: 0x00050F32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.y, this.x);
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

		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x06001CB4 RID: 7348 RVA: 0x00052D64 File Offset: 0x00050F64
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.y, this.y);
			}
		}

		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x00052D83 File Offset: 0x00050F83
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x00052DA2 File Offset: 0x00050FA2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x00052DC1 File Offset: 0x00050FC1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x00052DE0 File Offset: 0x00050FE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x00052DFF File Offset: 0x00050FFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x00052E1E File Offset: 0x0005101E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x00052E3D File Offset: 0x0005103D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x00052E5C File Offset: 0x0005105C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x00052E7B File Offset: 0x0005107B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x00052E9A File Offset: 0x0005109A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 zwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.z, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x00052EB9 File Offset: 0x000510B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x00052ED8 File Offset: 0x000510D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x00052EF7 File Offset: 0x000510F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001CC2 RID: 7362 RVA: 0x00052F16 File Offset: 0x00051116
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x00052F35 File Offset: 0x00051135
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001CC4 RID: 7364 RVA: 0x00052F54 File Offset: 0x00051154
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001CC5 RID: 7365 RVA: 0x00052F73 File Offset: 0x00051173
		// (set) Token: 0x06001CC6 RID: 7366 RVA: 0x00052F92 File Offset: 0x00051192
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.y, this.z);
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

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x00052FC4 File Offset: 0x000511C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x00052FE3 File Offset: 0x000511E3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001CC9 RID: 7369 RVA: 0x00053002 File Offset: 0x00051202
		// (set) Token: 0x06001CCA RID: 7370 RVA: 0x00053021 File Offset: 0x00051221
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.z, this.y);
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

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x00053053 File Offset: 0x00051253
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001CCC RID: 7372 RVA: 0x00053072 File Offset: 0x00051272
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001CCD RID: 7373 RVA: 0x00053091 File Offset: 0x00051291
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001CCE RID: 7374 RVA: 0x000530B0 File Offset: 0x000512B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x000530CF File Offset: 0x000512CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x000530EE File Offset: 0x000512EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x0005310D File Offset: 0x0005130D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x0005312C File Offset: 0x0005132C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x0005314B File Offset: 0x0005134B
		// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x0005316A File Offset: 0x0005136A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.x, this.z);
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

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06001CD5 RID: 7381 RVA: 0x0005319C File Offset: 0x0005139C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.x, this.w);
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06001CD6 RID: 7382 RVA: 0x000531BB File Offset: 0x000513BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06001CD7 RID: 7383 RVA: 0x000531DA File Offset: 0x000513DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x000531F9 File Offset: 0x000513F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06001CD9 RID: 7385 RVA: 0x00053218 File Offset: 0x00051418
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00053237 File Offset: 0x00051437
		// (set) Token: 0x06001CDB RID: 7387 RVA: 0x00053256 File Offset: 0x00051456
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.z, this.x);
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

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x00053288 File Offset: 0x00051488
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x000532A7 File Offset: 0x000514A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x000532C6 File Offset: 0x000514C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x000532E5 File Offset: 0x000514E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00053304 File Offset: 0x00051504
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x00053323 File Offset: 0x00051523
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x00053342 File Offset: 0x00051542
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x00053361 File Offset: 0x00051561
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x00053380 File Offset: 0x00051580
		// (set) Token: 0x06001CE5 RID: 7397 RVA: 0x0005339F File Offset: 0x0005159F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.x, this.y);
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

		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x000533D1 File Offset: 0x000515D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x000533F0 File Offset: 0x000515F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.x, this.w);
			}
		}

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x0005340F File Offset: 0x0005160F
		// (set) Token: 0x06001CE9 RID: 7401 RVA: 0x0005342E File Offset: 0x0005162E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.y, this.x);
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

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x00053460 File Offset: 0x00051660
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x0005347F File Offset: 0x0005167F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x0005349E File Offset: 0x0005169E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001CED RID: 7405 RVA: 0x000534BD File Offset: 0x000516BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x000534DC File Offset: 0x000516DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001CEF RID: 7407 RVA: 0x000534FB File Offset: 0x000516FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001CF0 RID: 7408 RVA: 0x0005351A File Offset: 0x0005171A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x00053539 File Offset: 0x00051739
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001CF2 RID: 7410 RVA: 0x00053558 File Offset: 0x00051758
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x00053577 File Offset: 0x00051777
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001CF4 RID: 7412 RVA: 0x00053596 File Offset: 0x00051796
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x000535B5 File Offset: 0x000517B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.x, this.x);
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001CF6 RID: 7414 RVA: 0x000535D4 File Offset: 0x000517D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.x, this.y);
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x000535F3 File Offset: 0x000517F3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.x, this.z);
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001CF8 RID: 7416 RVA: 0x00053612 File Offset: 0x00051812
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x00053631 File Offset: 0x00051831
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.y, this.x);
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001CFA RID: 7418 RVA: 0x00053650 File Offset: 0x00051850
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.y, this.y);
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001CFB RID: 7419 RVA: 0x0005366F File Offset: 0x0005186F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x0005368E File Offset: 0x0005188E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x000536AD File Offset: 0x000518AD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001CFE RID: 7422 RVA: 0x000536CC File Offset: 0x000518CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x000536EB File Offset: 0x000518EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001D00 RID: 7424 RVA: 0x0005370A File Offset: 0x0005190A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x00053729 File Offset: 0x00051929
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001D02 RID: 7426 RVA: 0x00053748 File Offset: 0x00051948
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x00053767 File Offset: 0x00051967
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001D04 RID: 7428 RVA: 0x00053786 File Offset: 0x00051986
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int4 wwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int4(this.w, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x000537A5 File Offset: 0x000519A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.x);
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001D06 RID: 7430 RVA: 0x000537BE File Offset: 0x000519BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.y);
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x000537D7 File Offset: 0x000519D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.z);
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001D08 RID: 7432 RVA: 0x000537F0 File Offset: 0x000519F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.x, this.w);
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x00053809 File Offset: 0x00051A09
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.x);
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x00053822 File Offset: 0x00051A22
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.y);
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001D0B RID: 7435 RVA: 0x0005383B File Offset: 0x00051A3B
		// (set) Token: 0x06001D0C RID: 7436 RVA: 0x00053854 File Offset: 0x00051A54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001D0D RID: 7437 RVA: 0x0005387A File Offset: 0x00051A7A
		// (set) Token: 0x06001D0E RID: 7438 RVA: 0x00053893 File Offset: 0x00051A93
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001D0F RID: 7439 RVA: 0x000538B9 File Offset: 0x00051AB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x000538D2 File Offset: 0x00051AD2
		// (set) Token: 0x06001D11 RID: 7441 RVA: 0x000538EB File Offset: 0x00051AEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x00053911 File Offset: 0x00051B11
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x0005392A File Offset: 0x00051B2A
		// (set) Token: 0x06001D14 RID: 7444 RVA: 0x00053943 File Offset: 0x00051B43
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x00053969 File Offset: 0x00051B69
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.w, this.x);
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x00053982 File Offset: 0x00051B82
		// (set) Token: 0x06001D17 RID: 7447 RVA: 0x0005399B File Offset: 0x00051B9B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x000539C1 File Offset: 0x00051BC1
		// (set) Token: 0x06001D19 RID: 7449 RVA: 0x000539DA File Offset: 0x00051BDA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00053A00 File Offset: 0x00051C00
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 xww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.x, this.w, this.w);
			}
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06001D1B RID: 7451 RVA: 0x00053A19 File Offset: 0x00051C19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.x);
			}
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00053A32 File Offset: 0x00051C32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.y);
			}
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x00053A4B File Offset: 0x00051C4B
		// (set) Token: 0x06001D1E RID: 7454 RVA: 0x00053A64 File Offset: 0x00051C64
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x00053A8A File Offset: 0x00051C8A
		// (set) Token: 0x06001D20 RID: 7456 RVA: 0x00053AA3 File Offset: 0x00051CA3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x00053AC9 File Offset: 0x00051CC9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.x);
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00053AE2 File Offset: 0x00051CE2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.y);
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x00053AFB File Offset: 0x00051CFB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x00053B14 File Offset: 0x00051D14
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.y, this.w);
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x00053B2D File Offset: 0x00051D2D
		// (set) Token: 0x06001D26 RID: 7462 RVA: 0x00053B46 File Offset: 0x00051D46
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x00053B6C File Offset: 0x00051D6C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x00053B85 File Offset: 0x00051D85
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.z);
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x00053B9E File Offset: 0x00051D9E
		// (set) Token: 0x06001D2A RID: 7466 RVA: 0x00053BB7 File Offset: 0x00051DB7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x00053BDD File Offset: 0x00051DDD
		// (set) Token: 0x06001D2C RID: 7468 RVA: 0x00053BF6 File Offset: 0x00051DF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 ywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x00053C1C File Offset: 0x00051E1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 ywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.w, this.y);
			}
		}

		// Token: 0x17000967 RID: 2407
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x00053C35 File Offset: 0x00051E35
		// (set) Token: 0x06001D2F RID: 7471 RVA: 0x00053C4E File Offset: 0x00051E4E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 ywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000968 RID: 2408
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x00053C74 File Offset: 0x00051E74
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 yww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.y, this.w, this.w);
			}
		}

		// Token: 0x17000969 RID: 2409
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x00053C8D File Offset: 0x00051E8D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.x);
			}
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x00053CA6 File Offset: 0x00051EA6
		// (set) Token: 0x06001D33 RID: 7475 RVA: 0x00053CBF File Offset: 0x00051EBF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x00053CE5 File Offset: 0x00051EE5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.z);
			}
		}

		// Token: 0x1700096C RID: 2412
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x00053CFE File Offset: 0x00051EFE
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x00053D17 File Offset: 0x00051F17
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x00053D3D File Offset: 0x00051F3D
		// (set) Token: 0x06001D38 RID: 7480 RVA: 0x00053D56 File Offset: 0x00051F56
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x00053D7C File Offset: 0x00051F7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.y);
			}
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x00053D95 File Offset: 0x00051F95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x00053DAE File Offset: 0x00051FAE
		// (set) Token: 0x06001D3C RID: 7484 RVA: 0x00053DC7 File Offset: 0x00051FC7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x00053DED File Offset: 0x00051FED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x00053E06 File Offset: 0x00052006
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x00053E1F File Offset: 0x0005201F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.z);
			}
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x00053E38 File Offset: 0x00052038
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.z, this.w);
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x00053E51 File Offset: 0x00052051
		// (set) Token: 0x06001D42 RID: 7490 RVA: 0x00053E6A File Offset: 0x0005206A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x00053E90 File Offset: 0x00052090
		// (set) Token: 0x06001D44 RID: 7492 RVA: 0x00053EA9 File Offset: 0x000520A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x00053ECF File Offset: 0x000520CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.w, this.z);
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x00053EE8 File Offset: 0x000520E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 zww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.z, this.w, this.w);
			}
		}

		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x00053F01 File Offset: 0x00052101
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.x, this.x);
			}
		}

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x00053F1A File Offset: 0x0005211A
		// (set) Token: 0x06001D49 RID: 7497 RVA: 0x00053F33 File Offset: 0x00052133
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x00053F59 File Offset: 0x00052159
		// (set) Token: 0x06001D4B RID: 7499 RVA: 0x00053F72 File Offset: 0x00052172
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x00053F98 File Offset: 0x00052198
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.x, this.w);
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x00053FB1 File Offset: 0x000521B1
		// (set) Token: 0x06001D4E RID: 7502 RVA: 0x00053FCA File Offset: 0x000521CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x00053FF0 File Offset: 0x000521F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.y, this.y);
			}
		}

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x00054009 File Offset: 0x00052209
		// (set) Token: 0x06001D51 RID: 7505 RVA: 0x00054022 File Offset: 0x00052222
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x00054048 File Offset: 0x00052248
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.y, this.w);
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x00054061 File Offset: 0x00052261
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x0005407A File Offset: 0x0005227A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000982 RID: 2434
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x000540A0 File Offset: 0x000522A0
		// (set) Token: 0x06001D56 RID: 7510 RVA: 0x000540B9 File Offset: 0x000522B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000983 RID: 2435
		// (get) Token: 0x06001D57 RID: 7511 RVA: 0x000540DF File Offset: 0x000522DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.z, this.z);
			}
		}

		// Token: 0x17000984 RID: 2436
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x000540F8 File Offset: 0x000522F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.z, this.w);
			}
		}

		// Token: 0x17000985 RID: 2437
		// (get) Token: 0x06001D59 RID: 7513 RVA: 0x00054111 File Offset: 0x00052311
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.w, this.x);
			}
		}

		// Token: 0x17000986 RID: 2438
		// (get) Token: 0x06001D5A RID: 7514 RVA: 0x0005412A File Offset: 0x0005232A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.w, this.y);
			}
		}

		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06001D5B RID: 7515 RVA: 0x00054143 File Offset: 0x00052343
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 wwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.w, this.z);
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06001D5C RID: 7516 RVA: 0x0005415C File Offset: 0x0005235C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int3 www
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int3(this.w, this.w, this.w);
			}
		}

		// Token: 0x17000989 RID: 2441
		// (get) Token: 0x06001D5D RID: 7517 RVA: 0x00054175 File Offset: 0x00052375
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.x);
			}
		}

		// Token: 0x1700098A RID: 2442
		// (get) Token: 0x06001D5E RID: 7518 RVA: 0x00054188 File Offset: 0x00052388
		// (set) Token: 0x06001D5F RID: 7519 RVA: 0x0005419B File Offset: 0x0005239B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x1700098B RID: 2443
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x000541B5 File Offset: 0x000523B5
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x000541C8 File Offset: 0x000523C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x1700098C RID: 2444
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x000541E2 File Offset: 0x000523E2
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x000541F5 File Offset: 0x000523F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 xw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x1700098D RID: 2445
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x0005420F File Offset: 0x0005240F
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x00054222 File Offset: 0x00052422
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x1700098E RID: 2446
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x0005423C File Offset: 0x0005243C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.y);
			}
		}

		// Token: 0x1700098F RID: 2447
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x0005424F File Offset: 0x0005244F
		// (set) Token: 0x06001D68 RID: 7528 RVA: 0x00054262 File Offset: 0x00052462
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000990 RID: 2448
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x0005427C File Offset: 0x0005247C
		// (set) Token: 0x06001D6A RID: 7530 RVA: 0x0005428F File Offset: 0x0005248F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 yw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x000542A9 File Offset: 0x000524A9
		// (set) Token: 0x06001D6C RID: 7532 RVA: 0x000542BC File Offset: 0x000524BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x000542D6 File Offset: 0x000524D6
		// (set) Token: 0x06001D6E RID: 7534 RVA: 0x000542E9 File Offset: 0x000524E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x00054303 File Offset: 0x00052503
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.z);
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x00054316 File Offset: 0x00052516
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x00054329 File Offset: 0x00052529
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 zw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x00054343 File Offset: 0x00052543
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x00054356 File Offset: 0x00052556
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 wx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x00054370 File Offset: 0x00052570
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x00054383 File Offset: 0x00052583
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 wy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x0005439D File Offset: 0x0005259D
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x000543B0 File Offset: 0x000525B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 wz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x000543CA File Offset: 0x000525CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int2 ww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new int2(this.w, this.w);
			}
		}

		// Token: 0x17000999 RID: 2457
		public unsafe int this[int index]
		{
			get
			{
				fixed (int4* ptr = &this)
				{
					return ((int*)ptr)[index];
				}
			}
			set
			{
				fixed (int* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x00054418 File Offset: 0x00052618
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(int4 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z && this.w == rhs.w;
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x00054454 File Offset: 0x00052654
		public override bool Equals(object o)
		{
			if (o is int4)
			{
				int4 converted = (int4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x00054479 File Offset: 0x00052679
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00054488 File Offset: 0x00052688
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("int4({0}, {1}, {2}, {3})", new object[] { this.x, this.y, this.z, this.w });
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x000544E0 File Offset: 0x000526E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("int4({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x0400011F RID: 287
		public int x;

		// Token: 0x04000120 RID: 288
		public int y;

		// Token: 0x04000121 RID: 289
		public int z;

		// Token: 0x04000122 RID: 290
		public int w;

		// Token: 0x04000123 RID: 291
		public static readonly int4 zero;

		// Token: 0x0200004C RID: 76
		internal sealed class DebuggerProxy
		{
			// Token: 0x06001D80 RID: 7552 RVA: 0x0005453D File Offset: 0x0005273D
			public DebuggerProxy(int4 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
				this.w = v.w;
			}

			// Token: 0x04000124 RID: 292
			public int x;

			// Token: 0x04000125 RID: 293
			public int y;

			// Token: 0x04000126 RID: 294
			public int z;

			// Token: 0x04000127 RID: 295
			public int w;
		}
	}
}
