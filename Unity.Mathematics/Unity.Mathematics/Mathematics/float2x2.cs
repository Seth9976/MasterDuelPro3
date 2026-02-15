using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200002D RID: 45
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float2x2 : IEquatable<float2x2>, IFormattable
	{
		// Token: 0x060010F3 RID: 4339 RVA: 0x000365AC File Offset: 0x000347AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(float2 c0, float2 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000365BC File Offset: 0x000347BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(float m00, float m01, float m10, float m11)
		{
			this.c0 = new float2(m00, m10);
			this.c1 = new float2(m01, m11);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000365D9 File Offset: 0x000347D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(float v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000365F4 File Offset: 0x000347F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(bool v)
		{
			this.c0 = math.select(new float2(0f), new float2(1f), v);
			this.c1 = math.select(new float2(0f), new float2(1f), v);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00036644 File Offset: 0x00034844
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(bool2x2 v)
		{
			this.c0 = math.select(new float2(0f), new float2(1f), v.c0);
			this.c1 = math.select(new float2(0f), new float2(1f), v.c1);
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0003669B File Offset: 0x0003489B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000366B5 File Offset: 0x000348B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(int2x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000366D9 File Offset: 0x000348D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x000366F3 File Offset: 0x000348F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(uint2x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00036717 File Offset: 0x00034917
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(double v)
		{
			this.c0 = (float2)v;
			this.c1 = (float2)v;
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00036731 File Offset: 0x00034931
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x2(double2x2 v)
		{
			this.c0 = (float2)v.c0;
			this.c1 = (float2)v.c1;
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x0000BB19 File Offset: 0x00009D19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x2(float v)
		{
			return new float2x2(v);
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x0000BB21 File Offset: 0x00009D21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x2(bool v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0000BB29 File Offset: 0x00009D29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x2(bool2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0000BB31 File Offset: 0x00009D31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x2(int v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0000BB39 File Offset: 0x00009D39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x2(int2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0000BB41 File Offset: 0x00009D41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x2(uint v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0000BB49 File Offset: 0x00009D49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x2(uint2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0000BB51 File Offset: 0x00009D51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x2(double v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0000BB59 File Offset: 0x00009D59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x2(double2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x00036755 File Offset: 0x00034955
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator *(float2x2 lhs, float2x2 rhs)
		{
			return new float2x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0003677E File Offset: 0x0003497E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator *(float2x2 lhs, float rhs)
		{
			return new float2x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0003679D File Offset: 0x0003499D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator *(float lhs, float2x2 rhs)
		{
			return new float2x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000367BC File Offset: 0x000349BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator +(float2x2 lhs, float2x2 rhs)
		{
			return new float2x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000367E5 File Offset: 0x000349E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator +(float2x2 lhs, float rhs)
		{
			return new float2x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00036804 File Offset: 0x00034A04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator +(float lhs, float2x2 rhs)
		{
			return new float2x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x00036823 File Offset: 0x00034A23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator -(float2x2 lhs, float2x2 rhs)
		{
			return new float2x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x0003684C File Offset: 0x00034A4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator -(float2x2 lhs, float rhs)
		{
			return new float2x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x0003686B File Offset: 0x00034A6B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator -(float lhs, float2x2 rhs)
		{
			return new float2x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0003688A File Offset: 0x00034A8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator /(float2x2 lhs, float2x2 rhs)
		{
			return new float2x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x000368B3 File Offset: 0x00034AB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator /(float2x2 lhs, float rhs)
		{
			return new float2x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x000368D2 File Offset: 0x00034AD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator /(float lhs, float2x2 rhs)
		{
			return new float2x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000368F1 File Offset: 0x00034AF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator %(float2x2 lhs, float2x2 rhs)
		{
			return new float2x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0003691A File Offset: 0x00034B1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator %(float2x2 lhs, float rhs)
		{
			return new float2x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00036939 File Offset: 0x00034B39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator %(float lhs, float2x2 rhs)
		{
			return new float2x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00036958 File Offset: 0x00034B58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator ++(float2x2 val)
		{
			float2 @float = float2.op_Increment(val.c0);
			val.c0 = @float;
			float2 float2 = @float;
			@float = float2.op_Increment(val.c1);
			val.c1 = @float;
			return new float2x2(float2, @float);
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x000369A0 File Offset: 0x00034BA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator --(float2x2 val)
		{
			float2 @float = float2.op_Decrement(val.c0);
			val.c0 = @float;
			float2 float2 = @float;
			@float = float2.op_Decrement(val.c1);
			val.c1 = @float;
			return new float2x2(float2, @float);
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000369E6 File Offset: 0x00034BE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(float2x2 lhs, float2x2 rhs)
		{
			return new bool2x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00036A0F File Offset: 0x00034C0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(float2x2 lhs, float rhs)
		{
			return new bool2x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00036A2E File Offset: 0x00034C2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <(float lhs, float2x2 rhs)
		{
			return new bool2x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00036A4D File Offset: 0x00034C4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(float2x2 lhs, float2x2 rhs)
		{
			return new bool2x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00036A76 File Offset: 0x00034C76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(float2x2 lhs, float rhs)
		{
			return new bool2x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00036A95 File Offset: 0x00034C95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator <=(float lhs, float2x2 rhs)
		{
			return new bool2x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00036AB4 File Offset: 0x00034CB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(float2x2 lhs, float2x2 rhs)
		{
			return new bool2x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00036ADD File Offset: 0x00034CDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(float2x2 lhs, float rhs)
		{
			return new bool2x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x00036AFC File Offset: 0x00034CFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >(float lhs, float2x2 rhs)
		{
			return new bool2x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00036B1B File Offset: 0x00034D1B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(float2x2 lhs, float2x2 rhs)
		{
			return new bool2x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00036B44 File Offset: 0x00034D44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(float2x2 lhs, float rhs)
		{
			return new bool2x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00036B63 File Offset: 0x00034D63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator >=(float lhs, float2x2 rhs)
		{
			return new bool2x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00036B82 File Offset: 0x00034D82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator -(float2x2 val)
		{
			return new float2x2(-val.c0, -val.c1);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00036B9F File Offset: 0x00034D9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 operator +(float2x2 val)
		{
			return new float2x2(+val.c0, +val.c1);
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00036BBC File Offset: 0x00034DBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(float2x2 lhs, float2x2 rhs)
		{
			return new bool2x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x00036BE5 File Offset: 0x00034DE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(float2x2 lhs, float rhs)
		{
			return new bool2x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x00036C04 File Offset: 0x00034E04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator ==(float lhs, float2x2 rhs)
		{
			return new bool2x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x00036C23 File Offset: 0x00034E23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(float2x2 lhs, float2x2 rhs)
		{
			return new bool2x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x00036C4C File Offset: 0x00034E4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(float2x2 lhs, float rhs)
		{
			return new bool2x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x00036C6B File Offset: 0x00034E6B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 operator !=(float lhs, float2x2 rhs)
		{
			return new bool2x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x170003F8 RID: 1016
		public unsafe ref float2 this[int index]
		{
			get
			{
				fixed (float2x2* ptr = &this)
				{
					return ref *(float2*)(ptr + (IntPtr)index * (IntPtr)sizeof(float2) / (IntPtr)sizeof(float2x2));
				}
			}
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x00036CA7 File Offset: 0x00034EA7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float2x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x00036CD0 File Offset: 0x00034ED0
		public override bool Equals(object o)
		{
			if (o is float2x2)
			{
				float2x2 converted = (float2x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x00036CF5 File Offset: 0x00034EF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x00036D04 File Offset: 0x00034F04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float2x2({0}f, {1}f,  {2}f, {3}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y
			});
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00036D70 File Offset: 0x00034F70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float2x2({0}f, {1}f,  {2}f, {3}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00036DE4 File Offset: 0x00034FE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 Rotate(float angle)
		{
			float s;
			float c;
			math.sincos(angle, out s, out c);
			return math.float2x2(c, -s, s, c);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00036E05 File Offset: 0x00035005
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 Scale(float s)
		{
			return math.float2x2(s, 0f, 0f, s);
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00036E18 File Offset: 0x00035018
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 Scale(float x, float y)
		{
			return math.float2x2(x, 0f, 0f, y);
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x00036E2B File Offset: 0x0003502B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 Scale(float2 v)
		{
			return float2x2.Scale(v.x, v.y);
		}

		// Token: 0x040000AB RID: 171
		public float2 c0;

		// Token: 0x040000AC RID: 172
		public float2 c1;

		// Token: 0x040000AD RID: 173
		public static readonly float2x2 identity = new float2x2(1f, 0f, 0f, 1f);

		// Token: 0x040000AE RID: 174
		public static readonly float2x2 zero;
	}
}
