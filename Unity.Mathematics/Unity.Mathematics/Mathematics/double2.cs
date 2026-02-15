using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200001C RID: 28
	[DebuggerTypeProxy(typeof(double2.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct double2 : IEquatable<double2>, IFormattable
	{
		// Token: 0x06000B52 RID: 2898 RVA: 0x00028D51 File Offset: 0x00026F51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x00028D61 File Offset: 0x00026F61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(double2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00028D7B File Offset: 0x00026F7B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(double v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00028D8B File Offset: 0x00026F8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(bool v)
		{
			this.x = (v ? 1.0 : 0.0);
			this.y = (v ? 1.0 : 0.0);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00028DC8 File Offset: 0x00026FC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(bool2 v)
		{
			this.x = (v.x ? 1.0 : 0.0);
			this.y = (v.y ? 1.0 : 0.0);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00028E19 File Offset: 0x00027019
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(int v)
		{
			this.x = (double)v;
			this.y = (double)v;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00028E2B File Offset: 0x0002702B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(int2 v)
		{
			this.x = (double)v.x;
			this.y = (double)v.y;
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x00028E47 File Offset: 0x00027047
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(uint v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00028E5B File Offset: 0x0002705B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(uint2 v)
		{
			this.x = v.x;
			this.y = v.y;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00028E79 File Offset: 0x00027079
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(half v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x00028E93 File Offset: 0x00027093
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(half2 v)
		{
			this.x = v.x;
			this.y = v.y;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x00028E19 File Offset: 0x00027019
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(float v)
		{
			this.x = (double)v;
			this.y = (double)v;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00028EB7 File Offset: 0x000270B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2(float2 v)
		{
			this.x = (double)v.x;
			this.y = (double)v.y;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0000996D File Offset: 0x00007B6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(double v)
		{
			return new double2(v);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00009975 File Offset: 0x00007B75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2(bool v)
		{
			return new double2(v);
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0000997D File Offset: 0x00007B7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator double2(bool2 v)
		{
			return new double2(v);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00009985 File Offset: 0x00007B85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(int v)
		{
			return new double2(v);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0000998D File Offset: 0x00007B8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(int2 v)
		{
			return new double2(v);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00009995 File Offset: 0x00007B95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(uint v)
		{
			return new double2(v);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0000999D File Offset: 0x00007B9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(uint2 v)
		{
			return new double2(v);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x000099A5 File Offset: 0x00007BA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(half v)
		{
			return new double2(v);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000099AD File Offset: 0x00007BAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(half2 v)
		{
			return new double2(v);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000099B5 File Offset: 0x00007BB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(float v)
		{
			return new double2(v);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000099BD File Offset: 0x00007BBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double2(float2 v)
		{
			return new double2(v);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00028ED3 File Offset: 0x000270D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator *(double2 lhs, double2 rhs)
		{
			return new double2(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00028EF4 File Offset: 0x000270F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator *(double2 lhs, double rhs)
		{
			return new double2(lhs.x * rhs, lhs.y * rhs);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00028F0B File Offset: 0x0002710B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator *(double lhs, double2 rhs)
		{
			return new double2(lhs * rhs.x, lhs * rhs.y);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00028F22 File Offset: 0x00027122
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator +(double2 lhs, double2 rhs)
		{
			return new double2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x00028F43 File Offset: 0x00027143
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator +(double2 lhs, double rhs)
		{
			return new double2(lhs.x + rhs, lhs.y + rhs);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x00028F5A File Offset: 0x0002715A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator +(double lhs, double2 rhs)
		{
			return new double2(lhs + rhs.x, lhs + rhs.y);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00028F71 File Offset: 0x00027171
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator -(double2 lhs, double2 rhs)
		{
			return new double2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x00028F92 File Offset: 0x00027192
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator -(double2 lhs, double rhs)
		{
			return new double2(lhs.x - rhs, lhs.y - rhs);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x00028FA9 File Offset: 0x000271A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator -(double lhs, double2 rhs)
		{
			return new double2(lhs - rhs.x, lhs - rhs.y);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x00028FC0 File Offset: 0x000271C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator /(double2 lhs, double2 rhs)
		{
			return new double2(lhs.x / rhs.x, lhs.y / rhs.y);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x00028FE1 File Offset: 0x000271E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator /(double2 lhs, double rhs)
		{
			return new double2(lhs.x / rhs, lhs.y / rhs);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x00028FF8 File Offset: 0x000271F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator /(double lhs, double2 rhs)
		{
			return new double2(lhs / rhs.x, lhs / rhs.y);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x00012429 File Offset: 0x00010629
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator %(double2 lhs, double2 rhs)
		{
			return new double2(lhs.x % rhs.x, lhs.y % rhs.y);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0002900F File Offset: 0x0002720F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator %(double2 lhs, double rhs)
		{
			return new double2(lhs.x % rhs, lhs.y % rhs);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00029026 File Offset: 0x00027226
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator %(double lhs, double2 rhs)
		{
			return new double2(lhs % rhs.x, lhs % rhs.y);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x00029040 File Offset: 0x00027240
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator ++(double2 val)
		{
			double num = val.x + 1.0;
			val.x = num;
			double num2 = num;
			num = val.y + 1.0;
			val.y = num;
			return new double2(num2, num);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x00029080 File Offset: 0x00027280
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator --(double2 val)
		{
			double num = val.x - 1.0;
			val.x = num;
			double num2 = num;
			num = val.y - 1.0;
			val.y = num;
			return new double2(num2, num);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000290C0 File Offset: 0x000272C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(double2 lhs, double2 rhs)
		{
			return new bool2(lhs.x < rhs.x, lhs.y < rhs.y);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000290E3 File Offset: 0x000272E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(double2 lhs, double rhs)
		{
			return new bool2(lhs.x < rhs, lhs.y < rhs);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000290FC File Offset: 0x000272FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(double lhs, double2 rhs)
		{
			return new bool2(lhs < rhs.x, lhs < rhs.y);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00029115 File Offset: 0x00027315
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(double2 lhs, double2 rhs)
		{
			return new bool2(lhs.x <= rhs.x, lhs.y <= rhs.y);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0002913E File Offset: 0x0002733E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(double2 lhs, double rhs)
		{
			return new bool2(lhs.x <= rhs, lhs.y <= rhs);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0002915D File Offset: 0x0002735D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(double lhs, double2 rhs)
		{
			return new bool2(lhs <= rhs.x, lhs <= rhs.y);
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0002917C File Offset: 0x0002737C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(double2 lhs, double2 rhs)
		{
			return new bool2(lhs.x > rhs.x, lhs.y > rhs.y);
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0002919F File Offset: 0x0002739F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(double2 lhs, double rhs)
		{
			return new bool2(lhs.x > rhs, lhs.y > rhs);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x000291B8 File Offset: 0x000273B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(double lhs, double2 rhs)
		{
			return new bool2(lhs > rhs.x, lhs > rhs.y);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000291D1 File Offset: 0x000273D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(double2 lhs, double2 rhs)
		{
			return new bool2(lhs.x >= rhs.x, lhs.y >= rhs.y);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000291FA File Offset: 0x000273FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(double2 lhs, double rhs)
		{
			return new bool2(lhs.x >= rhs, lhs.y >= rhs);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00029219 File Offset: 0x00027419
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(double lhs, double2 rhs)
		{
			return new bool2(lhs >= rhs.x, lhs >= rhs.y);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00029238 File Offset: 0x00027438
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator -(double2 val)
		{
			return new double2(-val.x, -val.y);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0002924D File Offset: 0x0002744D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 operator +(double2 val)
		{
			return new double2(val.x, val.y);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00029260 File Offset: 0x00027460
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(double2 lhs, double2 rhs)
		{
			return new bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00029283 File Offset: 0x00027483
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(double2 lhs, double rhs)
		{
			return new bool2(lhs.x == rhs, lhs.y == rhs);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0002929C File Offset: 0x0002749C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(double lhs, double2 rhs)
		{
			return new bool2(lhs == rhs.x, lhs == rhs.y);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000292B5 File Offset: 0x000274B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(double2 lhs, double2 rhs)
		{
			return new bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000292DE File Offset: 0x000274DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(double2 lhs, double rhs)
		{
			return new bool2(lhs.x != rhs, lhs.y != rhs);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000292FD File Offset: 0x000274FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(double lhs, double2 rhs)
		{
			return new bool2(lhs != rhs.x, lhs != rhs.y);
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0002931C File Offset: 0x0002751C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000B90 RID: 2960 RVA: 0x0002933B File Offset: 0x0002753B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0002935A File Offset: 0x0002755A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000B92 RID: 2962 RVA: 0x00029379 File Offset: 0x00027579
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x00029398 File Offset: 0x00027598
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x000293B7 File Offset: 0x000275B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x000293D6 File Offset: 0x000275D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x000293F5 File Offset: 0x000275F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00029414 File Offset: 0x00027614
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00029433 File Offset: 0x00027633
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x00029452 File Offset: 0x00027652
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00029471 File Offset: 0x00027671
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x00029490 File Offset: 0x00027690
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x000294AF File Offset: 0x000276AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x000294CE File Offset: 0x000276CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x000294ED File Offset: 0x000276ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0002950C File Offset: 0x0002770C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.x);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x00029525 File Offset: 0x00027725
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0002953E File Offset: 0x0002773E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x00029557 File Offset: 0x00027757
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x00029570 File Offset: 0x00027770
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x00029589 File Offset: 0x00027789
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x000295A2 File Offset: 0x000277A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x000295BB File Offset: 0x000277BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x000295D4 File Offset: 0x000277D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.x, this.x);
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x0002924D File Offset: 0x0002744D
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x00028D61 File Offset: 0x00026F61
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

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x000295E7 File Offset: 0x000277E7
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x000295FA File Offset: 0x000277FA
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

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x00029614 File Offset: 0x00027814
		[EditorBrowsable(EditorBrowsableState.Never)]
		public double2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new double2(this.y, this.y);
			}
		}

		// Token: 0x1700020A RID: 522
		public unsafe double this[int index]
		{
			get
			{
				fixed (double2* ptr = &this)
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

		// Token: 0x06000BAF RID: 2991 RVA: 0x00029660 File Offset: 0x00027860
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(double2 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00029680 File Offset: 0x00027880
		public override bool Equals(object o)
		{
			if (o is double2)
			{
				double2 converted = (double2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000296A5 File Offset: 0x000278A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000296B2 File Offset: 0x000278B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("double2({0}, {1})", this.x, this.y);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000296D4 File Offset: 0x000278D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("double2({0}, {1})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider));
		}

		// Token: 0x0400006A RID: 106
		public double x;

		// Token: 0x0400006B RID: 107
		public double y;

		// Token: 0x0400006C RID: 108
		public static readonly double2 zero;

		// Token: 0x0200001D RID: 29
		internal sealed class DebuggerProxy
		{
			// Token: 0x06000BB4 RID: 2996 RVA: 0x000296FA File Offset: 0x000278FA
			public DebuggerProxy(double2 v)
			{
				this.x = v.x;
				this.y = v.y;
			}

			// Token: 0x0400006D RID: 109
			public double x;

			// Token: 0x0400006E RID: 110
			public double y;
		}
	}
}
