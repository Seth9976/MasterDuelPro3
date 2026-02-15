using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000021 RID: 33
	[DebuggerTypeProxy(typeof(double3.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double3 : IEquatable<double3>, IFormattable
	{
		// Token: 0x06000C73 RID: 3187 RVA: 0x0002BAB5 File Offset: 0x00029CB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(double x, double y, double z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0002BACC File Offset: 0x00029CCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(double x, double2 yz)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0002BAED File Offset: 0x00029CED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(double2 xy, double z)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0002BB0E File Offset: 0x00029D0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(double3 xyz)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0002BB34 File Offset: 0x00029D34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(double v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0002BB4C File Offset: 0x00029D4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(bool v)
		{
			this.x = (v ? 1.0 : 0.0);
			this.y = (v ? 1.0 : 0.0);
			this.z = (v ? 1.0 : 0.0);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0002BBB0 File Offset: 0x00029DB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(bool3 v)
		{
			this.x = (v.x ? 1.0 : 0.0);
			this.y = (v.y ? 1.0 : 0.0);
			this.z = (v.z ? 1.0 : 0.0);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002BC23 File Offset: 0x00029E23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(int v)
		{
			this.x = (double)v;
			this.y = (double)v;
			this.z = (double)v;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002BC3D File Offset: 0x00029E3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(int3 v)
		{
			this.x = (double)v.x;
			this.y = (double)v.y;
			this.z = (double)v.z;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002BC66 File Offset: 0x00029E66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(uint v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002BC83 File Offset: 0x00029E83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(uint3 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002BCAF File Offset: 0x00029EAF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(half v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002BCD5 File Offset: 0x00029ED5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(half3 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002BC23 File Offset: 0x00029E23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(float v)
		{
			this.x = (double)v;
			this.y = (double)v;
			this.z = (double)v;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002BD0A File Offset: 0x00029F0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3(float3 v)
		{
			this.x = (double)v.x;
			this.y = (double)v.y;
			this.z = (double)v.z;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0000A0BE File Offset: 0x000082BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3(double v)
		{
			return new double3(v);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0000A0C6 File Offset: 0x000082C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double3(bool v)
		{
			return new double3(v);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0000A0CE File Offset: 0x000082CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double3(bool3 v)
		{
			return new double3(v);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0000A0D6 File Offset: 0x000082D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3(int v)
		{
			return new double3(v);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0000A0DE File Offset: 0x000082DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3(int3 v)
		{
			return new double3(v);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0000A0E6 File Offset: 0x000082E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3(uint v)
		{
			return new double3(v);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0000A0EE File Offset: 0x000082EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3(uint3 v)
		{
			return new double3(v);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0000A0F6 File Offset: 0x000082F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3(half v)
		{
			return new double3(v);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0000A0FE File Offset: 0x000082FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3(half3 v)
		{
			return new double3(v);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0000A106 File Offset: 0x00008306
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3(float v)
		{
			return new double3(v);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0000A10E File Offset: 0x0000830E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double3(float3 v)
		{
			return new double3(v);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002BD33 File Offset: 0x00029F33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator *(double3 lhs, double3 rhs)
		{
			return new double3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0002BD61 File Offset: 0x00029F61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator *(double3 lhs, double rhs)
		{
			return new double3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002BD80 File Offset: 0x00029F80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator *(double lhs, double3 rhs)
		{
			return new double3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002BD9F File Offset: 0x00029F9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator +(double3 lhs, double3 rhs)
		{
			return new double3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002BDCD File Offset: 0x00029FCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator +(double3 lhs, double rhs)
		{
			return new double3(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002BDEC File Offset: 0x00029FEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator +(double lhs, double3 rhs)
		{
			return new double3(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002BE0B File Offset: 0x0002A00B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator -(double3 lhs, double3 rhs)
		{
			return new double3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002BE39 File Offset: 0x0002A039
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator -(double3 lhs, double rhs)
		{
			return new double3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0002BE58 File Offset: 0x0002A058
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator -(double lhs, double3 rhs)
		{
			return new double3(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0002BE77 File Offset: 0x0002A077
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator /(double3 lhs, double3 rhs)
		{
			return new double3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0002BEA5 File Offset: 0x0002A0A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator /(double3 lhs, double rhs)
		{
			return new double3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0002BEC4 File Offset: 0x0002A0C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator /(double lhs, double3 rhs)
		{
			return new double3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0001244A File Offset: 0x0001064A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator %(double3 lhs, double3 rhs)
		{
			return new double3(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0002BEE3 File Offset: 0x0002A0E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator %(double3 lhs, double rhs)
		{
			return new double3(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0002BF02 File Offset: 0x0002A102
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator %(double lhs, double3 rhs)
		{
			return new double3(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0002BF24 File Offset: 0x0002A124
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator ++(double3 val)
		{
			double num = val.x + 1.0;
			val.x = num;
			double num2 = num;
			num = val.y + 1.0;
			val.y = num;
			double num3 = num;
			num = val.z + 1.0;
			val.z = num;
			return new double3(num2, num3, num);
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0002BF7C File Offset: 0x0002A17C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator --(double3 val)
		{
			double num = val.x - 1.0;
			val.x = num;
			double num2 = num;
			num = val.y - 1.0;
			val.y = num;
			double num3 = num;
			num = val.z - 1.0;
			val.z = num;
			return new double3(num2, num3, num);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0002BFD3 File Offset: 0x0002A1D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(double3 lhs, double3 rhs)
		{
			return new bool3(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0002C004 File Offset: 0x0002A204
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(double3 lhs, double rhs)
		{
			return new bool3(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0002C026 File Offset: 0x0002A226
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(double lhs, double3 rhs)
		{
			return new bool3(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z);
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0002C048 File Offset: 0x0002A248
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(double3 lhs, double3 rhs)
		{
			return new bool3(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0002C082 File Offset: 0x0002A282
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(double3 lhs, double rhs)
		{
			return new bool3(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs);
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0002C0AD File Offset: 0x0002A2AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(double lhs, double3 rhs)
		{
			return new bool3(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0002C0D8 File Offset: 0x0002A2D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(double3 lhs, double3 rhs)
		{
			return new bool3(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0002C109 File Offset: 0x0002A309
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(double3 lhs, double rhs)
		{
			return new bool3(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0002C12B File Offset: 0x0002A32B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(double lhs, double3 rhs)
		{
			return new bool3(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002C14D File Offset: 0x0002A34D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(double3 lhs, double3 rhs)
		{
			return new bool3(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002C187 File Offset: 0x0002A387
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(double3 lhs, double rhs)
		{
			return new bool3(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002C1B2 File Offset: 0x0002A3B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(double lhs, double3 rhs)
		{
			return new bool3(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002C1DD File Offset: 0x0002A3DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator -(double3 val)
		{
			return new double3(-val.x, -val.y, -val.z);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002C1F9 File Offset: 0x0002A3F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 operator +(double3 val)
		{
			return new double3(val.x, val.y, val.z);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002C212 File Offset: 0x0002A412
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(double3 lhs, double3 rhs)
		{
			return new bool3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002C243 File Offset: 0x0002A443
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(double3 lhs, double rhs)
		{
			return new bool3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002C265 File Offset: 0x0002A465
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(double lhs, double3 rhs)
		{
			return new bool3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002C287 File Offset: 0x0002A487
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(double3 lhs, double3 rhs)
		{
			return new bool3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002C2C1 File Offset: 0x0002A4C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(double3 lhs, double rhs)
		{
			return new bool3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002C2EC File Offset: 0x0002A4EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(double lhs, double3 rhs)
		{
			return new bool3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0002C317 File Offset: 0x0002A517
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x0002C336 File Offset: 0x0002A536
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0002C355 File Offset: 0x0002A555
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000CB5 RID: 3253 RVA: 0x0002C374 File Offset: 0x0002A574
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0002C393 File Offset: 0x0002A593
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0002C3B2 File Offset: 0x0002A5B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0002C3D1 File Offset: 0x0002A5D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0002C3F0 File Offset: 0x0002A5F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0002C40F File Offset: 0x0002A60F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0002C42E File Offset: 0x0002A62E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0002C44D File Offset: 0x0002A64D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0002C46C File Offset: 0x0002A66C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0002C48B File Offset: 0x0002A68B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0002C4AA File Offset: 0x0002A6AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0002C4C9 File Offset: 0x0002A6C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0002C4E8 File Offset: 0x0002A6E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0002C507 File Offset: 0x0002A707
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0002C526 File Offset: 0x0002A726
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x0002C545 File Offset: 0x0002A745
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0002C564 File Offset: 0x0002A764
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x0002C583 File Offset: 0x0002A783
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0002C5A2 File Offset: 0x0002A7A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x0002C5C1 File Offset: 0x0002A7C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0002C5E0 File Offset: 0x0002A7E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0002C5FF File Offset: 0x0002A7FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0002C61E File Offset: 0x0002A81E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x0002C63D File Offset: 0x0002A83D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0002C65C File Offset: 0x0002A85C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x0002C67B File Offset: 0x0002A87B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0002C69A File Offset: 0x0002A89A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0002C6B9 File Offset: 0x0002A8B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0002C6D8 File Offset: 0x0002A8D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000CD2 RID: 3282 RVA: 0x0002C6F7 File Offset: 0x0002A8F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0002C716 File Offset: 0x0002A916
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x0002C735 File Offset: 0x0002A935
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0002C754 File Offset: 0x0002A954
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x0002C773 File Offset: 0x0002A973
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0002C792 File Offset: 0x0002A992
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x0002C7B1 File Offset: 0x0002A9B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0002C7D0 File Offset: 0x0002A9D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0002C7EF File Offset: 0x0002A9EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0002C80E File Offset: 0x0002AA0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0002C82D File Offset: 0x0002AA2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0002C84C File Offset: 0x0002AA4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0002C86B File Offset: 0x0002AA6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0002C88A File Offset: 0x0002AA8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0002C8A9 File Offset: 0x0002AAA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0002C8C8 File Offset: 0x0002AAC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0002C8E7 File Offset: 0x0002AAE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0002C906 File Offset: 0x0002AB06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0002C925 File Offset: 0x0002AB25
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0002C944 File Offset: 0x0002AB44
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0002C963 File Offset: 0x0002AB63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0002C982 File Offset: 0x0002AB82
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0002C9A1 File Offset: 0x0002ABA1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0002C9C0 File Offset: 0x0002ABC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0002C9DF File Offset: 0x0002ABDF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0002C9FE File Offset: 0x0002ABFE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0002CA1D File Offset: 0x0002AC1D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0002CA3C File Offset: 0x0002AC3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0002CA5B File Offset: 0x0002AC5B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0002CA7A File Offset: 0x0002AC7A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0002CA99 File Offset: 0x0002AC99
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0002CAB8 File Offset: 0x0002ACB8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0002CAD7 File Offset: 0x0002ACD7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0002CAF6 File Offset: 0x0002ACF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0002CB15 File Offset: 0x0002AD15
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0002CB34 File Offset: 0x0002AD34
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0002CB53 File Offset: 0x0002AD53
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0002CB72 File Offset: 0x0002AD72
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0002CB91 File Offset: 0x0002AD91
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0002CBB0 File Offset: 0x0002ADB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0002CBCF File Offset: 0x0002ADCF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0002CBEE File Offset: 0x0002ADEE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0002CC0D File Offset: 0x0002AE0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0002CC2C File Offset: 0x0002AE2C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0002CC4B File Offset: 0x0002AE4B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0002CC6A File Offset: 0x0002AE6A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0002CC89 File Offset: 0x0002AE89
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0002CCA8 File Offset: 0x0002AEA8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0002CCC7 File Offset: 0x0002AEC7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0002CCE6 File Offset: 0x0002AEE6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0002CCFF File Offset: 0x0002AEFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0002CD18 File Offset: 0x0002AF18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0002CD31 File Offset: 0x0002AF31
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0002CD4A File Offset: 0x0002AF4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0002C1F9 File Offset: 0x0002A3F9
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x0002BB0E File Offset: 0x00029D0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0002CD63 File Offset: 0x0002AF63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x0002CD7C File Offset: 0x0002AF7C
		// (set) Token: 0x06000D0C RID: 3340 RVA: 0x0002CD95 File Offset: 0x0002AF95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0002CDBB File Offset: 0x0002AFBB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0002CDD4 File Offset: 0x0002AFD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x0002CDED File Offset: 0x0002AFED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.y);
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0002CE06 File Offset: 0x0002B006
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x0002CE1F File Offset: 0x0002B01F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0002CE45 File Offset: 0x0002B045
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.x);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0002CE5E File Offset: 0x0002B05E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.y);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0002CE77 File Offset: 0x0002B077
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.z);
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0002CE90 File Offset: 0x0002B090
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x0002CEA9 File Offset: 0x0002B0A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0002CECF File Offset: 0x0002B0CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0002CEE8 File Offset: 0x0002B0E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.z, this.z);
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0002CF01 File Offset: 0x0002B101
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0002CF1A File Offset: 0x0002B11A
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x0002CF33 File Offset: 0x0002B133
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x0002CF59 File Offset: 0x0002B159
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0002CF72 File Offset: 0x0002B172
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x0002CF8B File Offset: 0x0002B18B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0002CFB1 File Offset: 0x0002B1B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0002CFCA File Offset: 0x0002B1CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0002CFE3 File Offset: 0x0002B1E3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0002CFFC File Offset: 0x0002B1FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0002D015 File Offset: 0x0002B215
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.z, this.z);
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0002D02E File Offset: 0x0002B22E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.x, this.x);
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0002D041 File Offset: 0x0002B241
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x0002D054 File Offset: 0x0002B254
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0002D06E File Offset: 0x0002B26E
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x0002D081 File Offset: 0x0002B281
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x0002D09B File Offset: 0x0002B29B
		// (set) Token: 0x06000D2A RID: 3370 RVA: 0x0002D0AE File Offset: 0x0002B2AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x0002D0C8 File Offset: 0x0002B2C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.y, this.y);
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0002D0DB File Offset: 0x0002B2DB
		// (set) Token: 0x06000D2D RID: 3373 RVA: 0x0002D0EE File Offset: 0x0002B2EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0002D108 File Offset: 0x0002B308
		// (set) Token: 0x06000D2F RID: 3375 RVA: 0x0002D11B File Offset: 0x0002B31B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0002D135 File Offset: 0x0002B335
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x0002D148 File Offset: 0x0002B348
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0002D162 File Offset: 0x0002B362
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.z, this.z);
			}
		}

		// Token: 0x17000283 RID: 643
		public unsafe double this[int index]
		{
			get
			{
				fixed (double3* ptr = &this)
				{
					return ((double*)ptr)[index];
				}
			}
			set
			{
				fixed (double* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0002D1B0 File Offset: 0x0002B3B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double3 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0002D1E0 File Offset: 0x0002B3E0
		public override bool Equals(object o)
		{
			if (o is double3)
			{
				double3 converted = (double3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0002D205 File Offset: 0x0002B405
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0002D212 File Offset: 0x0002B412
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double3({0}, {1}, {2})", this.x, this.y, this.z);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0002D23F File Offset: 0x0002B43F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double3({0}, {1}, {2})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider), this.z.ToString(format, formatProvider));
		}

		// Token: 0x0400007C RID: 124
		public double x;

		// Token: 0x0400007D RID: 125
		public double y;

		// Token: 0x0400007E RID: 126
		public double z;

		// Token: 0x0400007F RID: 127
		public static readonly double3 zero;

		// Token: 0x02000022 RID: 34
		internal sealed class DebuggerProxy
		{
			// Token: 0x06000D3A RID: 3386 RVA: 0x0002D272 File Offset: 0x0002B472
			public DebuggerProxy(double3 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
			}

			// Token: 0x04000080 RID: 128
			public double x;

			// Token: 0x04000081 RID: 129
			public double y;

			// Token: 0x04000082 RID: 130
			public double z;
		}
	}
}
