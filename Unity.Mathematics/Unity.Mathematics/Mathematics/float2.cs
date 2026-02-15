using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Unity.Mathematics
{
	// Token: 0x0200002B RID: 43
	[DebuggerTypeProxy(typeof(float2.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float2 : IEquatable<float2>, IFormattable
	{
		// Token: 0x0600108E RID: 4238 RVA: 0x00035BFB File Offset: 0x00033DFB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x00035C0B File Offset: 0x00033E0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(float2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00035C25 File Offset: 0x00033E25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(float v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x00035C35 File Offset: 0x00033E35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(bool v)
		{
			this.x = (v ? 1f : 0f);
			this.y = (v ? 1f : 0f);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x00035C61 File Offset: 0x00033E61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(bool2 v)
		{
			this.x = (v.x ? 1f : 0f);
			this.y = (v.y ? 1f : 0f);
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x00035C97 File Offset: 0x00033E97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(int v)
		{
			this.x = (float)v;
			this.y = (float)v;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x00035CA9 File Offset: 0x00033EA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(int2 v)
		{
			this.x = (float)v.x;
			this.y = (float)v.y;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x00035CC5 File Offset: 0x00033EC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(uint v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00035CD9 File Offset: 0x00033ED9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(uint2 v)
		{
			this.x = v.x;
			this.y = v.y;
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00035CF7 File Offset: 0x00033EF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(half v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00035D11 File Offset: 0x00033F11
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(half2 v)
		{
			this.x = v.x;
			this.y = v.y;
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x00035C97 File Offset: 0x00033E97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(double v)
		{
			this.x = (float)v;
			this.y = (float)v;
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x00035D35 File Offset: 0x00033F35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(double2 v)
		{
			this.x = (float)v.x;
			this.y = (float)v.y;
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0000B98F File Offset: 0x00009B8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(float v)
		{
			return new float2(v);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0000B997 File Offset: 0x00009B97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2(bool v)
		{
			return new float2(v);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0000B99F File Offset: 0x00009B9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2(bool2 v)
		{
			return new float2(v);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0000B9A7 File Offset: 0x00009BA7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(int v)
		{
			return new float2(v);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0000B9AF File Offset: 0x00009BAF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(int2 v)
		{
			return new float2(v);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0000B9B7 File Offset: 0x00009BB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(uint v)
		{
			return new float2(v);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0000B9BF File Offset: 0x00009BBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(uint2 v)
		{
			return new float2(v);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x0000B9C7 File Offset: 0x00009BC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(half v)
		{
			return new float2(v);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x0000B9CF File Offset: 0x00009BCF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(half2 v)
		{
			return new float2(v);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x0000B9D7 File Offset: 0x00009BD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2(double v)
		{
			return new float2(v);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x0000B9DF File Offset: 0x00009BDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2(double2 v)
		{
			return new float2(v);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x00035D51 File Offset: 0x00033F51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator *(float2 lhs, float2 rhs)
		{
			return new float2(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x00035D72 File Offset: 0x00033F72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator *(float2 lhs, float rhs)
		{
			return new float2(lhs.x * rhs, lhs.y * rhs);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x00035D89 File Offset: 0x00033F89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator *(float lhs, float2 rhs)
		{
			return new float2(lhs * rhs.x, lhs * rhs.y);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00035DA0 File Offset: 0x00033FA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator +(float2 lhs, float2 rhs)
		{
			return new float2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00035DC1 File Offset: 0x00033FC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator +(float2 lhs, float rhs)
		{
			return new float2(lhs.x + rhs, lhs.y + rhs);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00035DD8 File Offset: 0x00033FD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator +(float lhs, float2 rhs)
		{
			return new float2(lhs + rhs.x, lhs + rhs.y);
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x00035DEF File Offset: 0x00033FEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator -(float2 lhs, float2 rhs)
		{
			return new float2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x00035E10 File Offset: 0x00034010
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator -(float2 lhs, float rhs)
		{
			return new float2(lhs.x - rhs, lhs.y - rhs);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00035E27 File Offset: 0x00034027
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator -(float lhs, float2 rhs)
		{
			return new float2(lhs - rhs.x, lhs - rhs.y);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x00035E3E File Offset: 0x0003403E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator /(float2 lhs, float2 rhs)
		{
			return new float2(lhs.x / rhs.x, lhs.y / rhs.y);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x00035E5F File Offset: 0x0003405F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator /(float2 lhs, float rhs)
		{
			return new float2(lhs.x / rhs, lhs.y / rhs);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x00035E76 File Offset: 0x00034076
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator /(float lhs, float2 rhs)
		{
			return new float2(lhs / rhs.x, lhs / rhs.y);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0001239F File Offset: 0x0001059F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator %(float2 lhs, float2 rhs)
		{
			return new float2(lhs.x % rhs.x, lhs.y % rhs.y);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x00035E8D File Offset: 0x0003408D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator %(float2 lhs, float rhs)
		{
			return new float2(lhs.x % rhs, lhs.y % rhs);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x00035EA4 File Offset: 0x000340A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator %(float lhs, float2 rhs)
		{
			return new float2(lhs % rhs.x, lhs % rhs.y);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x00035EBC File Offset: 0x000340BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator ++(float2 val)
		{
			float num = val.x + 1f;
			val.x = num;
			float num2 = num;
			num = val.y + 1f;
			val.y = num;
			return new float2(num2, num);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x00035EF4 File Offset: 0x000340F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator --(float2 val)
		{
			float num = val.x - 1f;
			val.x = num;
			float num2 = num;
			num = val.y - 1f;
			val.y = num;
			return new float2(num2, num);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x00035F2C File Offset: 0x0003412C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(float2 lhs, float2 rhs)
		{
			return new bool2(lhs.x < rhs.x, lhs.y < rhs.y);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x00035F4F File Offset: 0x0003414F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(float2 lhs, float rhs)
		{
			return new bool2(lhs.x < rhs, lhs.y < rhs);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x00035F68 File Offset: 0x00034168
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(float lhs, float2 rhs)
		{
			return new bool2(lhs < rhs.x, lhs < rhs.y);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x00035F81 File Offset: 0x00034181
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(float2 lhs, float2 rhs)
		{
			return new bool2(lhs.x <= rhs.x, lhs.y <= rhs.y);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x00035FAA File Offset: 0x000341AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(float2 lhs, float rhs)
		{
			return new bool2(lhs.x <= rhs, lhs.y <= rhs);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x00035FC9 File Offset: 0x000341C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(float lhs, float2 rhs)
		{
			return new bool2(lhs <= rhs.x, lhs <= rhs.y);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x00035FE8 File Offset: 0x000341E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(float2 lhs, float2 rhs)
		{
			return new bool2(lhs.x > rhs.x, lhs.y > rhs.y);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0003600B File Offset: 0x0003420B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(float2 lhs, float rhs)
		{
			return new bool2(lhs.x > rhs, lhs.y > rhs);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00036024 File Offset: 0x00034224
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(float lhs, float2 rhs)
		{
			return new bool2(lhs > rhs.x, lhs > rhs.y);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x0003603D File Offset: 0x0003423D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(float2 lhs, float2 rhs)
		{
			return new bool2(lhs.x >= rhs.x, lhs.y >= rhs.y);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00036066 File Offset: 0x00034266
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(float2 lhs, float rhs)
		{
			return new bool2(lhs.x >= rhs, lhs.y >= rhs);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00036085 File Offset: 0x00034285
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(float lhs, float2 rhs)
		{
			return new bool2(lhs >= rhs.x, lhs >= rhs.y);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000360A4 File Offset: 0x000342A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator -(float2 val)
		{
			return new float2(-val.x, -val.y);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000360B9 File Offset: 0x000342B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator +(float2 val)
		{
			return new float2(val.x, val.y);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x000360CC File Offset: 0x000342CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(float2 lhs, float2 rhs)
		{
			return new bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x000360EF File Offset: 0x000342EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(float2 lhs, float rhs)
		{
			return new bool2(lhs.x == rhs, lhs.y == rhs);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00036108 File Offset: 0x00034308
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(float lhs, float2 rhs)
		{
			return new bool2(lhs == rhs.x, lhs == rhs.y);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00036121 File Offset: 0x00034321
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(float2 lhs, float2 rhs)
		{
			return new bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x0003614A File Offset: 0x0003434A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(float2 lhs, float rhs)
		{
			return new bool2(lhs.x != rhs, lhs.y != rhs);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00036169 File Offset: 0x00034369
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(float lhs, float2 rhs)
		{
			return new bool2(lhs != rhs.x, lhs != rhs.y);
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x00036188 File Offset: 0x00034388
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060010CC RID: 4300 RVA: 0x000361A7 File Offset: 0x000343A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x000361C6 File Offset: 0x000343C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x000361E5 File Offset: 0x000343E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x00036204 File Offset: 0x00034404
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060010D0 RID: 4304 RVA: 0x00036223 File Offset: 0x00034423
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060010D1 RID: 4305 RVA: 0x00036242 File Offset: 0x00034442
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060010D2 RID: 4306 RVA: 0x00036261 File Offset: 0x00034461
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x00036280 File Offset: 0x00034480
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060010D4 RID: 4308 RVA: 0x0003629F File Offset: 0x0003449F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060010D5 RID: 4309 RVA: 0x000362BE File Offset: 0x000344BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060010D6 RID: 4310 RVA: 0x000362DD File Offset: 0x000344DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x000362FC File Offset: 0x000344FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060010D8 RID: 4312 RVA: 0x0003631B File Offset: 0x0003451B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x0003633A File Offset: 0x0003453A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x00036359 File Offset: 0x00034559
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00036378 File Offset: 0x00034578
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.x);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060010DC RID: 4316 RVA: 0x00036391 File Offset: 0x00034591
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.y);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x000363AA File Offset: 0x000345AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.x);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x060010DE RID: 4318 RVA: 0x000363C3 File Offset: 0x000345C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.y);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x000363DC File Offset: 0x000345DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.x);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x060010E0 RID: 4320 RVA: 0x000363F5 File Offset: 0x000345F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.y);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0003640E File Offset: 0x0003460E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.x);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x060010E2 RID: 4322 RVA: 0x00036427 File Offset: 0x00034627
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.y);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00036440 File Offset: 0x00034640
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.x);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060010E4 RID: 4324 RVA: 0x000360B9 File Offset: 0x000342B9
		// (set) Token: 0x060010E5 RID: 4325 RVA: 0x00035C0B File Offset: 0x00033E0B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x00036453 File Offset: 0x00034653
		// (set) Token: 0x060010E7 RID: 4327 RVA: 0x00036466 File Offset: 0x00034666
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x00036480 File Offset: 0x00034680
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.y);
			}
		}

		// Token: 0x170003F7 RID: 1015
		public unsafe float this[int index]
		{
			get
			{
				fixed (float2* ptr = &this)
				{
					return ((float*)ptr)[index];
				}
			}
			set
			{
				fixed (float* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000364CC File Offset: 0x000346CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float2 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000364EC File Offset: 0x000346EC
		public override bool Equals(object o)
		{
			if (o is float2)
			{
				float2 converted = (float2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00036511 File Offset: 0x00034711
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0003651E File Offset: 0x0003471E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float2({0}f, {1}f)", this.x, this.y);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00036540 File Offset: 0x00034740
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float2({0}f, {1}f)", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider));
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x00036566 File Offset: 0x00034766
		public static implicit operator Vector2(float2 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00036579 File Offset: 0x00034779
		public static implicit operator float2(Vector2 v)
		{
			return new float2(v.x, v.y);
		}

		// Token: 0x040000A6 RID: 166
		public float x;

		// Token: 0x040000A7 RID: 167
		public float y;

		// Token: 0x040000A8 RID: 168
		public static readonly float2 zero;

		// Token: 0x0200002C RID: 44
		internal sealed class DebuggerProxy
		{
			// Token: 0x060010F2 RID: 4338 RVA: 0x0003658C File Offset: 0x0003478C
			public DebuggerProxy(float2 v)
			{
				this.x = v.x;
				this.y = v.y;
			}

			// Token: 0x040000A9 RID: 169
			public float x;

			// Token: 0x040000AA RID: 170
			public float y;
		}
	}
}
