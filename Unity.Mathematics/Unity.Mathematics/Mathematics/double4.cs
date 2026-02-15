using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000026 RID: 38
	[DebuggerTypeProxy(typeof(double4.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double4 : IEquatable<double4>, IFormattable
	{
		// Token: 0x06000DF9 RID: 3577 RVA: 0x0002F801 File Offset: 0x0002DA01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double x, double y, double z, double w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06000DFA RID: 3578 RVA: 0x0002F820 File Offset: 0x0002DA20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double x, double y, double2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0002F848 File Offset: 0x0002DA48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double x, double2 yz, double w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0002F870 File Offset: 0x0002DA70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double x, double3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0002F89D File Offset: 0x0002DA9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double2 xy, double z, double w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0002F8C5 File Offset: 0x0002DAC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double2 xy, double2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0002F8F7 File Offset: 0x0002DAF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double3 xyz, double w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0002F924 File Offset: 0x0002DB24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.y;
			this.z = xyzw.z;
			this.w = xyzw.w;
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0002F956 File Offset: 0x0002DB56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(double v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0002F974 File Offset: 0x0002DB74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(bool v)
		{
			this.x = (v ? 1.0 : 0.0);
			this.y = (v ? 1.0 : 0.0);
			this.z = (v ? 1.0 : 0.0);
			this.w = (v ? 1.0 : 0.0);
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0002F9F8 File Offset: 0x0002DBF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(bool4 v)
		{
			this.x = (v.x ? 1.0 : 0.0);
			this.y = (v.y ? 1.0 : 0.0);
			this.z = (v.z ? 1.0 : 0.0);
			this.w = (v.w ? 1.0 : 0.0);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0002FA8D File Offset: 0x0002DC8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(int v)
		{
			this.x = (double)v;
			this.y = (double)v;
			this.z = (double)v;
			this.w = (double)v;
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0002FAAF File Offset: 0x0002DCAF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(int4 v)
		{
			this.x = (double)v.x;
			this.y = (double)v.y;
			this.z = (double)v.z;
			this.w = (double)v.w;
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x0002FAE5 File Offset: 0x0002DCE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(uint v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x0002FB0B File Offset: 0x0002DD0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(uint4 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
			this.w = v.w;
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x0002FB45 File Offset: 0x0002DD45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(half v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x0002FB78 File Offset: 0x0002DD78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(half4 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
			this.w = v.w;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x0002FA8D File Offset: 0x0002DC8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(float v)
		{
			this.x = (double)v;
			this.y = (double)v;
			this.z = (double)v;
			this.w = (double)v;
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x0002FBC9 File Offset: 0x0002DDC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4(float4 v)
		{
			this.x = (double)v.x;
			this.y = (double)v.y;
			this.z = (double)v.z;
			this.w = (double)v.w;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x0000AAEE File Offset: 0x00008CEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(double v)
		{
			return new double4(v);
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0000AAF6 File Offset: 0x00008CF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4(bool v)
		{
			return new double4(v);
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x0000AAFE File Offset: 0x00008CFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double4(bool4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x0000AB06 File Offset: 0x00008D06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(int v)
		{
			return new double4(v);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0000AB0E File Offset: 0x00008D0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(int4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x0000AB16 File Offset: 0x00008D16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(uint v)
		{
			return new double4(v);
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x0000AB1E File Offset: 0x00008D1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(uint4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x0000AB26 File Offset: 0x00008D26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(half v)
		{
			return new double4(v);
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0000AB2E File Offset: 0x00008D2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(half4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x0000AB36 File Offset: 0x00008D36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(float v)
		{
			return new double4(v);
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0000AB3E File Offset: 0x00008D3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double4(float4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0002FBFF File Offset: 0x0002DDFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator *(double4 lhs, double4 rhs)
		{
			return new double4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w);
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x0002FC3A File Offset: 0x0002DE3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator *(double4 lhs, double rhs)
		{
			return new double4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0002FC61 File Offset: 0x0002DE61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator *(double lhs, double4 rhs)
		{
			return new double4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w);
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x0002FC88 File Offset: 0x0002DE88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator +(double4 lhs, double4 rhs)
		{
			return new double4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x0002FCC3 File Offset: 0x0002DEC3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator +(double4 lhs, double rhs)
		{
			return new double4(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs);
		}

		// Token: 0x06000E1C RID: 3612 RVA: 0x0002FCEA File Offset: 0x0002DEEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator +(double lhs, double4 rhs)
		{
			return new double4(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w);
		}

		// Token: 0x06000E1D RID: 3613 RVA: 0x0002FD11 File Offset: 0x0002DF11
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator -(double4 lhs, double4 rhs)
		{
			return new double4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x0002FD4C File Offset: 0x0002DF4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator -(double4 lhs, double rhs)
		{
			return new double4(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs);
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x0002FD73 File Offset: 0x0002DF73
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator -(double lhs, double4 rhs)
		{
			return new double4(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w);
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0002FD9A File Offset: 0x0002DF9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator /(double4 lhs, double4 rhs)
		{
			return new double4(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w);
		}

		// Token: 0x06000E21 RID: 3617 RVA: 0x0002FDD5 File Offset: 0x0002DFD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator /(double4 lhs, double rhs)
		{
			return new double4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x0002FDFC File Offset: 0x0002DFFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator /(double lhs, double4 rhs)
		{
			return new double4(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00012478 File Offset: 0x00010678
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator %(double4 lhs, double4 rhs)
		{
			return new double4(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z, lhs.w % rhs.w);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0002FE23 File Offset: 0x0002E023
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator %(double4 lhs, double rhs)
		{
			return new double4(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs, lhs.w % rhs);
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0002FE4A File Offset: 0x0002E04A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator %(double lhs, double4 rhs)
		{
			return new double4(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z, lhs % rhs.w);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0002FE74 File Offset: 0x0002E074
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator ++(double4 val)
		{
			double num = val.x + 1.0;
			val.x = num;
			double num2 = num;
			num = val.y + 1.0;
			val.y = num;
			double num3 = num;
			num = val.z + 1.0;
			val.z = num;
			double num4 = num;
			num = val.w + 1.0;
			val.w = num;
			return new double4(num2, num3, num4, num);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0002FEE4 File Offset: 0x0002E0E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator --(double4 val)
		{
			double num = val.x - 1.0;
			val.x = num;
			double num2 = num;
			num = val.y - 1.0;
			val.y = num;
			double num3 = num;
			num = val.z - 1.0;
			val.z = num;
			double num4 = num;
			num = val.w - 1.0;
			val.w = num;
			return new double4(num2, num3, num4, num);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0002FF52 File Offset: 0x0002E152
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(double4 lhs, double4 rhs)
		{
			return new bool4(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z, lhs.w < rhs.w);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0002FF91 File Offset: 0x0002E191
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(double4 lhs, double rhs)
		{
			return new bool4(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs, lhs.w < rhs);
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0002FFBC File Offset: 0x0002E1BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(double lhs, double4 rhs)
		{
			return new bool4(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z, lhs < rhs.w);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0002FFE8 File Offset: 0x0002E1E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(double4 lhs, double4 rhs)
		{
			return new bool4(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z, lhs.w <= rhs.w);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0003003E File Offset: 0x0002E23E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(double4 lhs, double rhs)
		{
			return new bool4(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs, lhs.w <= rhs);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x00030075 File Offset: 0x0002E275
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(double lhs, double4 rhs)
		{
			return new bool4(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z, lhs <= rhs.w);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x000300AC File Offset: 0x0002E2AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(double4 lhs, double4 rhs)
		{
			return new bool4(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z, lhs.w > rhs.w);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000300EB File Offset: 0x0002E2EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(double4 lhs, double rhs)
		{
			return new bool4(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs, lhs.w > rhs);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x00030116 File Offset: 0x0002E316
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(double lhs, double4 rhs)
		{
			return new bool4(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z, lhs > rhs.w);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x00030144 File Offset: 0x0002E344
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(double4 lhs, double4 rhs)
		{
			return new bool4(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z, lhs.w >= rhs.w);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0003019A File Offset: 0x0002E39A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(double4 lhs, double rhs)
		{
			return new bool4(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs, lhs.w >= rhs);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000301D1 File Offset: 0x0002E3D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(double lhs, double4 rhs)
		{
			return new bool4(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z, lhs >= rhs.w);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00030208 File Offset: 0x0002E408
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator -(double4 val)
		{
			return new double4(-val.x, -val.y, -val.z, -val.w);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0003022B File Offset: 0x0002E42B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 operator +(double4 val)
		{
			return new double4(val.x, val.y, val.z, val.w);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0003024A File Offset: 0x0002E44A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(double4 lhs, double4 rhs)
		{
			return new bool4(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00030289 File Offset: 0x0002E489
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(double4 lhs, double rhs)
		{
			return new bool4(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs);
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000302B4 File Offset: 0x0002E4B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(double lhs, double4 rhs)
		{
			return new bool4(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000302E0 File Offset: 0x0002E4E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(double4 lhs, double4 rhs)
		{
			return new bool4(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00030336 File Offset: 0x0002E536
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(double4 lhs, double rhs)
		{
			return new bool4(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0003036D File Offset: 0x0002E56D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(double lhs, double4 rhs)
		{
			return new bool4(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w);
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x000303A4 File Offset: 0x0002E5A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x000303C3 File Offset: 0x0002E5C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x000303E2 File Offset: 0x0002E5E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x00030401 File Offset: 0x0002E601
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x00030420 File Offset: 0x0002E620
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0003043F File Offset: 0x0002E63F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000E42 RID: 3650 RVA: 0x0003045E File Offset: 0x0002E65E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0003047D File Offset: 0x0002E67D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.w);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000E44 RID: 3652 RVA: 0x0003049C File Offset: 0x0002E69C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x000304BB File Offset: 0x0002E6BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000E46 RID: 3654 RVA: 0x000304DA File Offset: 0x0002E6DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x000304F9 File Offset: 0x0002E6F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00030518 File Offset: 0x0002E718
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x00030537 File Offset: 0x0002E737
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000E4A RID: 3658 RVA: 0x00030556 File Offset: 0x0002E756
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x00030575 File Offset: 0x0002E775
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000E4C RID: 3660 RVA: 0x00030594 File Offset: 0x0002E794
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x000305B3 File Offset: 0x0002E7B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000E4E RID: 3662 RVA: 0x000305D2 File Offset: 0x0002E7D2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x000305F1 File Offset: 0x0002E7F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.w);
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x00030610 File Offset: 0x0002E810
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0003062F File Offset: 0x0002E82F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0003064E File Offset: 0x0002E84E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0003066D File Offset: 0x0002E86D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.w);
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0003068C File Offset: 0x0002E88C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x000306AB File Offset: 0x0002E8AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x000306CA File Offset: 0x0002E8CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0003022B File Offset: 0x0002E42B
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x0002F924 File Offset: 0x0002DB24
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.z, this.w);
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

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x000306E9 File Offset: 0x0002E8E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.w, this.x);
			}
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x00030708 File Offset: 0x0002E908
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.w, this.y);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00030727 File Offset: 0x0002E927
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x00030746 File Offset: 0x0002E946
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.w, this.z);
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

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00030778 File Offset: 0x0002E978
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.w, this.w);
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00030797 File Offset: 0x0002E997
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x000307B6 File Offset: 0x0002E9B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x000307D5 File Offset: 0x0002E9D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x000307F4 File Offset: 0x0002E9F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.x, this.w);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x00030813 File Offset: 0x0002EA13
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x00030832 File Offset: 0x0002EA32
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000E64 RID: 3684 RVA: 0x00030851 File Offset: 0x0002EA51
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00030870 File Offset: 0x0002EA70
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x0003088F File Offset: 0x0002EA8F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.y, this.w);
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

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x000308C1 File Offset: 0x0002EAC1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x000308E0 File Offset: 0x0002EAE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x000308FF File Offset: 0x0002EAFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x0003091E File Offset: 0x0002EB1E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.z, this.w);
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0003093D File Offset: 0x0002EB3D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.w, this.x);
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x0003095C File Offset: 0x0002EB5C
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x0003097B File Offset: 0x0002EB7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.w, this.y);
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

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000E6E RID: 3694 RVA: 0x000309AD File Offset: 0x0002EBAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.w, this.z);
			}
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x000309CC File Offset: 0x0002EBCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.z, this.w, this.w);
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000E70 RID: 3696 RVA: 0x000309EB File Offset: 0x0002EBEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.x, this.x);
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x00030A0A File Offset: 0x0002EC0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.x, this.y);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000E72 RID: 3698 RVA: 0x00030A29 File Offset: 0x0002EC29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.x, this.z);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x00030A48 File Offset: 0x0002EC48
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.x, this.w);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x00030A67 File Offset: 0x0002EC67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.y, this.x);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x00030A86 File Offset: 0x0002EC86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.y, this.y);
			}
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000E76 RID: 3702 RVA: 0x00030AA5 File Offset: 0x0002ECA5
		// (set) Token: 0x06000E77 RID: 3703 RVA: 0x00030AC4 File Offset: 0x0002ECC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.y, this.z);
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

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000E78 RID: 3704 RVA: 0x00030AF6 File Offset: 0x0002ECF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.y, this.w);
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x00030B15 File Offset: 0x0002ED15
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.z, this.x);
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000E7A RID: 3706 RVA: 0x00030B34 File Offset: 0x0002ED34
		// (set) Token: 0x06000E7B RID: 3707 RVA: 0x00030B53 File Offset: 0x0002ED53
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.z, this.y);
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

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x00030B85 File Offset: 0x0002ED85
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.z, this.z);
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00030BA4 File Offset: 0x0002EDA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.z, this.w);
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x00030BC3 File Offset: 0x0002EDC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.w, this.x);
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x00030BE2 File Offset: 0x0002EDE2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.w, this.y);
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x00030C01 File Offset: 0x0002EE01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.w, this.z);
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x00030C20 File Offset: 0x0002EE20
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.w, this.w, this.w);
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00030C3F File Offset: 0x0002EE3F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x00030C5E File Offset: 0x0002EE5E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x00030C7D File Offset: 0x0002EE7D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00030C9C File Offset: 0x0002EE9C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.w);
			}
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000E86 RID: 3718 RVA: 0x00030CBB File Offset: 0x0002EEBB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00030CDA File Offset: 0x0002EEDA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00030CF9 File Offset: 0x0002EEF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00030D18 File Offset: 0x0002EF18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.w);
			}
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000E8A RID: 3722 RVA: 0x00030D37 File Offset: 0x0002EF37
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00030D56 File Offset: 0x0002EF56
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x00030D75 File Offset: 0x0002EF75
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00030D94 File Offset: 0x0002EF94
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x00030DB3 File Offset: 0x0002EFB3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.z, this.w);
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

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x00030DE5 File Offset: 0x0002EFE5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.w, this.x);
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x00030E04 File Offset: 0x0002F004
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.w, this.y);
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000E91 RID: 3729 RVA: 0x00030E23 File Offset: 0x0002F023
		// (set) Token: 0x06000E92 RID: 3730 RVA: 0x00030E42 File Offset: 0x0002F042
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.w, this.z);
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

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000E93 RID: 3731 RVA: 0x00030E74 File Offset: 0x0002F074
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.w, this.w);
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x00030E93 File Offset: 0x0002F093
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000E95 RID: 3733 RVA: 0x00030EB2 File Offset: 0x0002F0B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x00030ED1 File Offset: 0x0002F0D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000E97 RID: 3735 RVA: 0x00030EF0 File Offset: 0x0002F0F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.w);
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x00030F0F File Offset: 0x0002F10F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x00030F2E File Offset: 0x0002F12E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00030F4D File Offset: 0x0002F14D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00030F6C File Offset: 0x0002F16C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.w);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00030F8B File Offset: 0x0002F18B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x00030FAA File Offset: 0x0002F1AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x00030FC9 File Offset: 0x0002F1C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x00030FE8 File Offset: 0x0002F1E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.z, this.w);
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00031007 File Offset: 0x0002F207
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.w, this.x);
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00031026 File Offset: 0x0002F226
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.w, this.y);
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x00031045 File Offset: 0x0002F245
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.w, this.z);
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x00031064 File Offset: 0x0002F264
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.w, this.w);
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00031083 File Offset: 0x0002F283
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x000310A2 File Offset: 0x0002F2A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x000310C1 File Offset: 0x0002F2C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x000310E0 File Offset: 0x0002F2E0
		// (set) Token: 0x06000EA8 RID: 3752 RVA: 0x000310FF File Offset: 0x0002F2FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.x, this.w);
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

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x00031131 File Offset: 0x0002F331
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x00031150 File Offset: 0x0002F350
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x0003116F File Offset: 0x0002F36F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0003118E File Offset: 0x0002F38E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.y, this.w);
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x000311AD File Offset: 0x0002F3AD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x000311CC File Offset: 0x0002F3CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x000311EB File Offset: 0x0002F3EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0003120A File Offset: 0x0002F40A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.z, this.w);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x00031229 File Offset: 0x0002F429
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x00031248 File Offset: 0x0002F448
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.w, this.x);
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

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0003127A File Offset: 0x0002F47A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.w, this.y);
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000EB4 RID: 3764 RVA: 0x00031299 File Offset: 0x0002F499
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.w, this.z);
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x000312B8 File Offset: 0x0002F4B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.z, this.w, this.w);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x000312D7 File Offset: 0x0002F4D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.x, this.x);
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x000312F6 File Offset: 0x0002F4F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.x, this.y);
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x00031315 File Offset: 0x0002F515
		// (set) Token: 0x06000EB9 RID: 3769 RVA: 0x00031334 File Offset: 0x0002F534
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.x, this.z);
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

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00031366 File Offset: 0x0002F566
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.x, this.w);
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00031385 File Offset: 0x0002F585
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.y, this.x);
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000EBC RID: 3772 RVA: 0x000313A4 File Offset: 0x0002F5A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.y, this.y);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x000313C3 File Offset: 0x0002F5C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.y, this.z);
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x000313E2 File Offset: 0x0002F5E2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.y, this.w);
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00031401 File Offset: 0x0002F601
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x00031420 File Offset: 0x0002F620
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.z, this.x);
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

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x00031452 File Offset: 0x0002F652
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x00031471 File Offset: 0x0002F671
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x00031490 File Offset: 0x0002F690
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x000314AF File Offset: 0x0002F6AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x000314CE File Offset: 0x0002F6CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x000314ED File Offset: 0x0002F6ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x0003150C File Offset: 0x0002F70C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 ywww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x0003152B File Offset: 0x0002F72B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0003154A File Offset: 0x0002F74A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00031569 File Offset: 0x0002F769
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00031588 File Offset: 0x0002F788
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x000315A7 File Offset: 0x0002F7A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x000315C6 File Offset: 0x0002F7C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x000315E5 File Offset: 0x0002F7E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00031604 File Offset: 0x0002F804
		// (set) Token: 0x06000ED0 RID: 3792 RVA: 0x00031623 File Offset: 0x0002F823
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.y, this.w);
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

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x00031655 File Offset: 0x0002F855
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00031674 File Offset: 0x0002F874
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00031693 File Offset: 0x0002F893
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x000316B2 File Offset: 0x0002F8B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x000316D1 File Offset: 0x0002F8D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x000316F0 File Offset: 0x0002F8F0
		// (set) Token: 0x06000ED7 RID: 3799 RVA: 0x0003170F File Offset: 0x0002F90F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.w, this.y);
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

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00031741 File Offset: 0x0002F941
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00031760 File Offset: 0x0002F960
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0003177F File Offset: 0x0002F97F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x0003179E File Offset: 0x0002F99E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x000317BD File Offset: 0x0002F9BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x000317DC File Offset: 0x0002F9DC
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x000317FB File Offset: 0x0002F9FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.x, this.w);
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

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x0003182D File Offset: 0x0002FA2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0003184C File Offset: 0x0002FA4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0003186B File Offset: 0x0002FA6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x0003188A File Offset: 0x0002FA8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.y, this.w);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x000318A9 File Offset: 0x0002FAA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x000318C8 File Offset: 0x0002FAC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x000318E7 File Offset: 0x0002FAE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x00031906 File Offset: 0x0002FB06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x00031925 File Offset: 0x0002FB25
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x00031944 File Offset: 0x0002FB44
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.w, this.x);
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

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00031976 File Offset: 0x0002FB76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x00031995 File Offset: 0x0002FB95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x000319B4 File Offset: 0x0002FBB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x000319D3 File Offset: 0x0002FBD3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x000319F2 File Offset: 0x0002FBF2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00031A11 File Offset: 0x0002FC11
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00031A30 File Offset: 0x0002FC30
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.x, this.w);
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00031A4F File Offset: 0x0002FC4F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x00031A6E File Offset: 0x0002FC6E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00031A8D File Offset: 0x0002FC8D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x00031AAC File Offset: 0x0002FCAC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.y, this.w);
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00031ACB File Offset: 0x0002FCCB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00031AEA File Offset: 0x0002FCEA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x00031B09 File Offset: 0x0002FD09
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x00031B28 File Offset: 0x0002FD28
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x00031B47 File Offset: 0x0002FD47
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x00031B66 File Offset: 0x0002FD66
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x00031B85 File Offset: 0x0002FD85
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x00031BA4 File Offset: 0x0002FDA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00031BC3 File Offset: 0x0002FDC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00031BE2 File Offset: 0x0002FDE2
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x00031C01 File Offset: 0x0002FE01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.x, this.y);
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

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00031C33 File Offset: 0x0002FE33
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.x, this.z);
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00031C52 File Offset: 0x0002FE52
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00031C71 File Offset: 0x0002FE71
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x00031C90 File Offset: 0x0002FE90
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.y, this.x);
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

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x00031CC2 File Offset: 0x0002FEC2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.y, this.y);
			}
		}

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x00031CE1 File Offset: 0x0002FEE1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.y, this.z);
			}
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x00031D00 File Offset: 0x0002FF00
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.y, this.w);
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00031D1F File Offset: 0x0002FF1F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00031D3E File Offset: 0x0002FF3E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000341 RID: 833
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00031D5D File Offset: 0x0002FF5D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000342 RID: 834
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00031D7C File Offset: 0x0002FF7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00031D9B File Offset: 0x0002FF9B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x00031DBA File Offset: 0x0002FFBA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00031DD9 File Offset: 0x0002FFD9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x00031DF8 File Offset: 0x0002FFF8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 zwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.z, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00031E17 File Offset: 0x00030017
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x00031E36 File Offset: 0x00030036
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00031E55 File Offset: 0x00030055
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.x, this.z);
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000F11 RID: 3857 RVA: 0x00031E74 File Offset: 0x00030074
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.x, this.w);
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00031E93 File Offset: 0x00030093
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000F13 RID: 3859 RVA: 0x00031EB2 File Offset: 0x000300B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00031ED1 File Offset: 0x000300D1
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x00031EF0 File Offset: 0x000300F0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.y, this.z);
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

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00031F22 File Offset: 0x00030122
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.y, this.w);
			}
		}

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000F17 RID: 3863 RVA: 0x00031F41 File Offset: 0x00030141
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00031F60 File Offset: 0x00030160
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x00031F7F File Offset: 0x0003017F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.z, this.y);
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

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00031FB1 File Offset: 0x000301B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000F1B RID: 3867 RVA: 0x00031FD0 File Offset: 0x000301D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x00031FEF File Offset: 0x000301EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000F1D RID: 3869 RVA: 0x0003200E File Offset: 0x0003020E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0003202D File Offset: 0x0003022D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0003204C File Offset: 0x0003024C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0003206B File Offset: 0x0003026B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0003208A File Offset: 0x0003028A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x000320A9 File Offset: 0x000302A9
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x000320C8 File Offset: 0x000302C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.x, this.z);
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

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x000320FA File Offset: 0x000302FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.x, this.w);
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00032119 File Offset: 0x00030319
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00032138 File Offset: 0x00030338
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00032157 File Offset: 0x00030357
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x00032176 File Offset: 0x00030376
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.y, this.w);
			}
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x00032195 File Offset: 0x00030395
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x000321B4 File Offset: 0x000303B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.z, this.x);
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

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x000321E6 File Offset: 0x000303E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00032205 File Offset: 0x00030405
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00032224 File Offset: 0x00030424
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x00032243 File Offset: 0x00030443
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x00032262 File Offset: 0x00030462
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x00032281 File Offset: 0x00030481
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x000322A0 File Offset: 0x000304A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x000322BF File Offset: 0x000304BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x000322DE File Offset: 0x000304DE
		// (set) Token: 0x06000F34 RID: 3892 RVA: 0x000322FD File Offset: 0x000304FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.x, this.y);
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

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0003232F File Offset: 0x0003052F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0003234E File Offset: 0x0003054E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.x, this.w);
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0003236D File Offset: 0x0003056D
		// (set) Token: 0x06000F38 RID: 3896 RVA: 0x0003238C File Offset: 0x0003058C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.y, this.x);
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

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x000323BE File Offset: 0x000305BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x000323DD File Offset: 0x000305DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x000323FC File Offset: 0x000305FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.y, this.w);
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0003241B File Offset: 0x0003061B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x0003243A File Offset: 0x0003063A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00032459 File Offset: 0x00030659
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00032478 File Offset: 0x00030678
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00032497 File Offset: 0x00030697
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x000324B6 File Offset: 0x000306B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x000324D5 File Offset: 0x000306D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x000324F4 File Offset: 0x000306F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x00032513 File Offset: 0x00030713
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00032532 File Offset: 0x00030732
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x00032551 File Offset: 0x00030751
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.x, this.z);
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x00032570 File Offset: 0x00030770
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.x, this.w);
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0003258F File Offset: 0x0003078F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.y, this.x);
			}
		}

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x000325AE File Offset: 0x000307AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.y, this.y);
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x000325CD File Offset: 0x000307CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.y, this.z);
			}
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x000325EC File Offset: 0x000307EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.y, this.w);
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0003260B File Offset: 0x0003080B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x0003262A File Offset: 0x0003082A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00032649 File Offset: 0x00030849
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x00032668 File Offset: 0x00030868
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x00032687 File Offset: 0x00030887
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x000326A6 File Offset: 0x000308A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x000326C5 File Offset: 0x000308C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x000326E4 File Offset: 0x000308E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 wwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.w, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00032703 File Offset: 0x00030903
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0003271C File Offset: 0x0003091C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00032735 File Offset: 0x00030935
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.z);
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003274E File Offset: 0x0003094E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.w);
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x00032767 File Offset: 0x00030967
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.x);
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x00032780 File Offset: 0x00030980
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.y);
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00032799 File Offset: 0x00030999
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x000327B2 File Offset: 0x000309B2
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

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x000327D8 File Offset: 0x000309D8
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x000327F1 File Offset: 0x000309F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00032817 File Offset: 0x00030A17
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00032830 File Offset: 0x00030A30
		// (set) Token: 0x06000F60 RID: 3936 RVA: 0x00032849 File Offset: 0x00030A49
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

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0003286F File Offset: 0x00030A6F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x00032888 File Offset: 0x00030A88
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x000328A1 File Offset: 0x00030AA1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x000328C7 File Offset: 0x00030AC7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.w, this.x);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x000328E0 File Offset: 0x00030AE0
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x000328F9 File Offset: 0x00030AF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0003291F File Offset: 0x00030B1F
		// (set) Token: 0x06000F68 RID: 3944 RVA: 0x00032938 File Offset: 0x00030B38
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0003295E File Offset: 0x00030B5E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.w, this.w);
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000F6A RID: 3946 RVA: 0x00032977 File Offset: 0x00030B77
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x00032990 File Offset: 0x00030B90
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x000329A9 File Offset: 0x00030BA9
		// (set) Token: 0x06000F6D RID: 3949 RVA: 0x000329C2 File Offset: 0x00030BC2
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

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x000329E8 File Offset: 0x00030BE8
		// (set) Token: 0x06000F6F RID: 3951 RVA: 0x00032A01 File Offset: 0x00030C01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x00032A27 File Offset: 0x00030C27
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.x);
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x00032A40 File Offset: 0x00030C40
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.y);
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x00032A59 File Offset: 0x00030C59
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.z);
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x00032A72 File Offset: 0x00030C72
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.w);
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x00032A8B File Offset: 0x00030C8B
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x00032AA4 File Offset: 0x00030CA4
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

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x00032ACA File Offset: 0x00030CCA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.z, this.y);
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x00032AE3 File Offset: 0x00030CE3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.z, this.z);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x00032AFC File Offset: 0x00030CFC
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x00032B15 File Offset: 0x00030D15
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x00032B3B File Offset: 0x00030D3B
		// (set) Token: 0x06000F7B RID: 3963 RVA: 0x00032B54 File Offset: 0x00030D54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 ywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x00032B7A File Offset: 0x00030D7A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 ywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.w, this.y);
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x00032B93 File Offset: 0x00030D93
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x00032BAC File Offset: 0x00030DAC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 ywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x00032BD2 File Offset: 0x00030DD2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.w, this.w);
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000F80 RID: 3968 RVA: 0x00032BEB File Offset: 0x00030DEB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.x, this.x);
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x00032C04 File Offset: 0x00030E04
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x00032C1D File Offset: 0x00030E1D
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

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x00032C43 File Offset: 0x00030E43
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.x, this.z);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x00032C5C File Offset: 0x00030E5C
		// (set) Token: 0x06000F85 RID: 3973 RVA: 0x00032C75 File Offset: 0x00030E75
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x00032C9B File Offset: 0x00030E9B
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x00032CB4 File Offset: 0x00030EB4
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

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000F88 RID: 3976 RVA: 0x00032CDA File Offset: 0x00030EDA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.y, this.y);
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x00032CF3 File Offset: 0x00030EF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.y, this.z);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000F8A RID: 3978 RVA: 0x00032D0C File Offset: 0x00030F0C
		// (set) Token: 0x06000F8B RID: 3979 RVA: 0x00032D25 File Offset: 0x00030F25
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00032D4B File Offset: 0x00030F4B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.z, this.x);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00032D64 File Offset: 0x00030F64
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.z, this.y);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00032D7D File Offset: 0x00030F7D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.z, this.z);
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00032D96 File Offset: 0x00030F96
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.z, this.w);
			}
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00032DAF File Offset: 0x00030FAF
		// (set) Token: 0x06000F91 RID: 3985 RVA: 0x00032DC8 File Offset: 0x00030FC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00032DEE File Offset: 0x00030FEE
		// (set) Token: 0x06000F93 RID: 3987 RVA: 0x00032E07 File Offset: 0x00031007
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x00032E2D File Offset: 0x0003102D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.w, this.z);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00032E46 File Offset: 0x00031046
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 zww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.z, this.w, this.w);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x00032E5F File Offset: 0x0003105F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.x, this.x);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x00032E78 File Offset: 0x00031078
		// (set) Token: 0x06000F98 RID: 3992 RVA: 0x00032E91 File Offset: 0x00031091
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00032EB7 File Offset: 0x000310B7
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x00032ED0 File Offset: 0x000310D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00032EF6 File Offset: 0x000310F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.x, this.w);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x00032F0F File Offset: 0x0003110F
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x00032F28 File Offset: 0x00031128
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00032F4E File Offset: 0x0003114E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.y, this.y);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000F9F RID: 3999 RVA: 0x00032F67 File Offset: 0x00031167
		// (set) Token: 0x06000FA0 RID: 4000 RVA: 0x00032F80 File Offset: 0x00031180
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000FA1 RID: 4001 RVA: 0x00032FA6 File Offset: 0x000311A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.y, this.w);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00032FBF File Offset: 0x000311BF
		// (set) Token: 0x06000FA3 RID: 4003 RVA: 0x00032FD8 File Offset: 0x000311D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00032FFE File Offset: 0x000311FE
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x00033017 File Offset: 0x00031217
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0003303D File Offset: 0x0003123D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.z, this.z);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000FA7 RID: 4007 RVA: 0x00033056 File Offset: 0x00031256
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.z, this.w);
			}
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x0003306F File Offset: 0x0003126F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.w, this.x);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00033088 File Offset: 0x00031288
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.w, this.y);
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x000330A1 File Offset: 0x000312A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 wwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.w, this.z);
			}
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x000330BA File Offset: 0x000312BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 www
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.w, this.w, this.w);
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x000330D3 File Offset: 0x000312D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.x, this.x);
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x000330E6 File Offset: 0x000312E6
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x000330F9 File Offset: 0x000312F9
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

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00033113 File Offset: 0x00031313
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x00033126 File Offset: 0x00031326
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

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00033140 File Offset: 0x00031340
		// (set) Token: 0x06000FB2 RID: 4018 RVA: 0x00033153 File Offset: 0x00031353
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 xw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0003316D File Offset: 0x0003136D
		// (set) Token: 0x06000FB4 RID: 4020 RVA: 0x00033180 File Offset: 0x00031380
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

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000FB5 RID: 4021 RVA: 0x0003319A File Offset: 0x0003139A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.y, this.y);
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x000331AD File Offset: 0x000313AD
		// (set) Token: 0x06000FB7 RID: 4023 RVA: 0x000331C0 File Offset: 0x000313C0
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

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x000331DA File Offset: 0x000313DA
		// (set) Token: 0x06000FB9 RID: 4025 RVA: 0x000331ED File Offset: 0x000313ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 yw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x00033207 File Offset: 0x00031407
		// (set) Token: 0x06000FBB RID: 4027 RVA: 0x0003321A File Offset: 0x0003141A
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

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000FBC RID: 4028 RVA: 0x00033234 File Offset: 0x00031434
		// (set) Token: 0x06000FBD RID: 4029 RVA: 0x00033247 File Offset: 0x00031447
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

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x00033261 File Offset: 0x00031461
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.z, this.z);
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00033274 File Offset: 0x00031474
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x00033287 File Offset: 0x00031487
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 zw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x000332A1 File Offset: 0x000314A1
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x000332B4 File Offset: 0x000314B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 wx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x000332CE File Offset: 0x000314CE
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x000332E1 File Offset: 0x000314E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 wy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x000332FB File Offset: 0x000314FB
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x0003330E File Offset: 0x0003150E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 wz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00033328 File Offset: 0x00031528
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 ww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.w, this.w);
			}
		}

		// Token: 0x170003D7 RID: 983
		public unsafe double this[int index]
		{
			get
			{
				fixed (double4* ptr = &this)
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

		// Token: 0x06000FCA RID: 4042 RVA: 0x00033374 File Offset: 0x00031574
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double4 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z && this.w == rhs.w;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x000333B0 File Offset: 0x000315B0
		public override bool Equals(object o)
		{
			if (o is double4)
			{
				double4 converted = (double4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000333D5 File Offset: 0x000315D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x000333E4 File Offset: 0x000315E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double4({0}, {1}, {2}, {3})", new object[] { this.x, this.y, this.z, this.w });
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0003343C File Offset: 0x0003163C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double4({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000090 RID: 144
		public double x;

		// Token: 0x04000091 RID: 145
		public double y;

		// Token: 0x04000092 RID: 146
		public double z;

		// Token: 0x04000093 RID: 147
		public double w;

		// Token: 0x04000094 RID: 148
		public static readonly double4 zero;

		// Token: 0x02000027 RID: 39
		internal sealed class DebuggerProxy
		{
			// Token: 0x06000FCF RID: 4047 RVA: 0x00033499 File Offset: 0x00031699
			public DebuggerProxy(double4 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
				this.w = v.w;
			}

			// Token: 0x04000095 RID: 149
			public double x;

			// Token: 0x04000096 RID: 150
			public double y;

			// Token: 0x04000097 RID: 151
			public double z;

			// Token: 0x04000098 RID: 152
			public double w;
		}
	}
}
