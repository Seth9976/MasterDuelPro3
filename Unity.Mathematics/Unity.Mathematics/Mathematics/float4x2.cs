using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000037 RID: 55
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float4x2 : IEquatable<float4x2>, IFormattable
	{
		// Token: 0x06001530 RID: 5424 RVA: 0x00040D43 File Offset: 0x0003EF43
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(float4 c0, float4 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00040D53 File Offset: 0x0003EF53
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(float m00, float m01, float m10, float m11, float m20, float m21, float m30, float m31)
		{
			this.c0 = new float4(m00, m10, m20, m30);
			this.c1 = new float4(m01, m11, m21, m31);
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x00040D78 File Offset: 0x0003EF78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(float v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00040D94 File Offset: 0x0003EF94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(bool v)
		{
			this.c0 = math.select(new float4(0f), new float4(1f), v);
			this.c1 = math.select(new float4(0f), new float4(1f), v);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00040DE4 File Offset: 0x0003EFE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(bool4x2 v)
		{
			this.c0 = math.select(new float4(0f), new float4(1f), v.c0);
			this.c1 = math.select(new float4(0f), new float4(1f), v.c1);
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00040E3B File Offset: 0x0003F03B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00040E55 File Offset: 0x0003F055
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(int4x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x00040E79 File Offset: 0x0003F079
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x00040E93 File Offset: 0x0003F093
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(uint4x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00040EB7 File Offset: 0x0003F0B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(double v)
		{
			this.c0 = (float4)v;
			this.c1 = (float4)v;
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00040ED1 File Offset: 0x0003F0D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x2(double4x2 v)
		{
			this.c0 = (float4)v.c0;
			this.c1 = (float4)v.c1;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0000CCD5 File Offset: 0x0000AED5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x2(float v)
		{
			return new float4x2(v);
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0000CCDD File Offset: 0x0000AEDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x2(bool v)
		{
			return new float4x2(v);
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0000CCE5 File Offset: 0x0000AEE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x2(bool4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0000CCED File Offset: 0x0000AEED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x2(int v)
		{
			return new float4x2(v);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0000CCF5 File Offset: 0x0000AEF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x2(int4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0000CCFD File Offset: 0x0000AEFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x2(uint v)
		{
			return new float4x2(v);
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x0000CD05 File Offset: 0x0000AF05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x2(uint4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x0000CD0D File Offset: 0x0000AF0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x2(double v)
		{
			return new float4x2(v);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x0000CD15 File Offset: 0x0000AF15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x2(double4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00040EF5 File Offset: 0x0003F0F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator *(float4x2 lhs, float4x2 rhs)
		{
			return new float4x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00040F1E File Offset: 0x0003F11E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator *(float4x2 lhs, float rhs)
		{
			return new float4x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00040F3D File Offset: 0x0003F13D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator *(float lhs, float4x2 rhs)
		{
			return new float4x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00040F5C File Offset: 0x0003F15C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator +(float4x2 lhs, float4x2 rhs)
		{
			return new float4x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06001548 RID: 5448 RVA: 0x00040F85 File Offset: 0x0003F185
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator +(float4x2 lhs, float rhs)
		{
			return new float4x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06001549 RID: 5449 RVA: 0x00040FA4 File Offset: 0x0003F1A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator +(float lhs, float4x2 rhs)
		{
			return new float4x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00040FC3 File Offset: 0x0003F1C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator -(float4x2 lhs, float4x2 rhs)
		{
			return new float4x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00040FEC File Offset: 0x0003F1EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator -(float4x2 lhs, float rhs)
		{
			return new float4x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x0004100B File Offset: 0x0003F20B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator -(float lhs, float4x2 rhs)
		{
			return new float4x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x0004102A File Offset: 0x0003F22A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator /(float4x2 lhs, float4x2 rhs)
		{
			return new float4x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00041053 File Offset: 0x0003F253
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator /(float4x2 lhs, float rhs)
		{
			return new float4x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00041072 File Offset: 0x0003F272
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator /(float lhs, float4x2 rhs)
		{
			return new float4x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x00041091 File Offset: 0x0003F291
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator %(float4x2 lhs, float4x2 rhs)
		{
			return new float4x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x000410BA File Offset: 0x0003F2BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator %(float4x2 lhs, float rhs)
		{
			return new float4x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x000410D9 File Offset: 0x0003F2D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator %(float lhs, float4x2 rhs)
		{
			return new float4x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x000410F8 File Offset: 0x0003F2F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator ++(float4x2 val)
		{
			float4 @float = float4.op_Increment(val.c0);
			val.c0 = @float;
			float4 float2 = @float;
			@float = float4.op_Increment(val.c1);
			val.c1 = @float;
			return new float4x2(float2, @float);
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00041140 File Offset: 0x0003F340
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator --(float4x2 val)
		{
			float4 @float = float4.op_Decrement(val.c0);
			val.c0 = @float;
			float4 float2 = @float;
			@float = float4.op_Decrement(val.c1);
			val.c1 = @float;
			return new float4x2(float2, @float);
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00041186 File Offset: 0x0003F386
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(float4x2 lhs, float4x2 rhs)
		{
			return new bool4x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x000411AF File Offset: 0x0003F3AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(float4x2 lhs, float rhs)
		{
			return new bool4x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x000411CE File Offset: 0x0003F3CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <(float lhs, float4x2 rhs)
		{
			return new bool4x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x000411ED File Offset: 0x0003F3ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(float4x2 lhs, float4x2 rhs)
		{
			return new bool4x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00041216 File Offset: 0x0003F416
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(float4x2 lhs, float rhs)
		{
			return new bool4x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00041235 File Offset: 0x0003F435
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator <=(float lhs, float4x2 rhs)
		{
			return new bool4x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00041254 File Offset: 0x0003F454
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(float4x2 lhs, float4x2 rhs)
		{
			return new bool4x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x0004127D File Offset: 0x0003F47D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(float4x2 lhs, float rhs)
		{
			return new bool4x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0004129C File Offset: 0x0003F49C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >(float lhs, float4x2 rhs)
		{
			return new bool4x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x000412BB File Offset: 0x0003F4BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(float4x2 lhs, float4x2 rhs)
		{
			return new bool4x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x000412E4 File Offset: 0x0003F4E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(float4x2 lhs, float rhs)
		{
			return new bool4x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00041303 File Offset: 0x0003F503
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator >=(float lhs, float4x2 rhs)
		{
			return new bool4x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00041322 File Offset: 0x0003F522
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator -(float4x2 val)
		{
			return new float4x2(-val.c0, -val.c1);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0004133F File Offset: 0x0003F53F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 operator +(float4x2 val)
		{
			return new float4x2(+val.c0, +val.c1);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0004135C File Offset: 0x0003F55C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(float4x2 lhs, float4x2 rhs)
		{
			return new bool4x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00041385 File Offset: 0x0003F585
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(float4x2 lhs, float rhs)
		{
			return new bool4x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x000413A4 File Offset: 0x0003F5A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator ==(float lhs, float4x2 rhs)
		{
			return new bool4x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x000413C3 File Offset: 0x0003F5C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(float4x2 lhs, float4x2 rhs)
		{
			return new bool4x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x000413EC File Offset: 0x0003F5EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(float4x2 lhs, float rhs)
		{
			return new bool4x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0004140B File Offset: 0x0003F60B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 operator !=(float lhs, float4x2 rhs)
		{
			return new bool4x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x170005C5 RID: 1477
		public unsafe ref float4 this[int index]
		{
			get
			{
				fixed (float4x2* ptr = &this)
				{
					return ref *(float4*)(ptr + (IntPtr)index * (IntPtr)sizeof(float4) / (IntPtr)sizeof(float4x2));
				}
			}
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x00041447 File Offset: 0x0003F647
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float4x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00041470 File Offset: 0x0003F670
		public override bool Equals(object o)
		{
			if (o is float4x2)
			{
				float4x2 converted = (float4x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00041495 File Offset: 0x0003F695
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x000414A4 File Offset: 0x0003F6A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float4x2({0}f, {1}f,  {2}f, {3}f,  {4}f, {5}f,  {6}f, {7}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z,
				this.c0.w,
				this.c1.w
			});
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0004155C File Offset: 0x0003F75C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float4x2({0}f, {1}f,  {2}f, {3}f,  {4}f, {5}f,  {6}f, {7}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000D5 RID: 213
		public float4 c0;

		// Token: 0x040000D6 RID: 214
		public float4 c1;

		// Token: 0x040000D7 RID: 215
		public static readonly float4x2 zero;
	}
}
