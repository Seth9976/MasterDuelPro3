using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200002E RID: 46
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float2x3 : IEquatable<float2x3>, IFormattable
	{
		// Token: 0x06001137 RID: 4407 RVA: 0x00036E5E File Offset: 0x0003505E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(float2 c0, float2 c1, float2 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00036E75 File Offset: 0x00035075
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(float m00, float m01, float m02, float m10, float m11, float m12)
		{
			this.c0 = new float2(m00, m10);
			this.c1 = new float2(m01, m11);
			this.c2 = new float2(m02, m12);
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00036EA1 File Offset: 0x000350A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00036EC8 File Offset: 0x000350C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(bool v)
		{
			this.c0 = math.select(new float2(0f), new float2(1f), v);
			this.c1 = math.select(new float2(0f), new float2(1f), v);
			this.c2 = math.select(new float2(0f), new float2(1f), v);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00036F38 File Offset: 0x00035138
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(bool2x3 v)
		{
			this.c0 = math.select(new float2(0f), new float2(1f), v.c0);
			this.c1 = math.select(new float2(0f), new float2(1f), v.c1);
			this.c2 = math.select(new float2(0f), new float2(1f), v.c2);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00036FB4 File Offset: 0x000351B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00036FDA File Offset: 0x000351DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(int2x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0003700F File Offset: 0x0003520F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00037035 File Offset: 0x00035235
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(uint2x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0003706A File Offset: 0x0003526A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(double v)
		{
			this.c0 = (float2)v;
			this.c1 = (float2)v;
			this.c2 = (float2)v;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00037090 File Offset: 0x00035290
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x3(double2x3 v)
		{
			this.c0 = (float2)v.c0;
			this.c1 = (float2)v.c1;
			this.c2 = (float2)v.c2;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x0000BD07 File Offset: 0x00009F07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x3(float v)
		{
			return new float2x3(v);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x0000BD0F File Offset: 0x00009F0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x3(bool v)
		{
			return new float2x3(v);
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x0000BD17 File Offset: 0x00009F17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x3(bool2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x0000BD1F File Offset: 0x00009F1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x3(int v)
		{
			return new float2x3(v);
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0000BD27 File Offset: 0x00009F27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x3(int2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0000BD2F File Offset: 0x00009F2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x3(uint v)
		{
			return new float2x3(v);
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0000BD37 File Offset: 0x00009F37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x3(uint2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x0000BD3F File Offset: 0x00009F3F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x3(double v)
		{
			return new float2x3(v);
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x0000BD47 File Offset: 0x00009F47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x3(double2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000370C5 File Offset: 0x000352C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator *(float2x3 lhs, float2x3 rhs)
		{
			return new float2x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x000370FF File Offset: 0x000352FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator *(float2x3 lhs, float rhs)
		{
			return new float2x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0003712A File Offset: 0x0003532A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator *(float lhs, float2x3 rhs)
		{
			return new float2x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x00037155 File Offset: 0x00035355
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator +(float2x3 lhs, float2x3 rhs)
		{
			return new float2x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0003718F File Offset: 0x0003538F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator +(float2x3 lhs, float rhs)
		{
			return new float2x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x000371BA File Offset: 0x000353BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator +(float lhs, float2x3 rhs)
		{
			return new float2x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x000371E5 File Offset: 0x000353E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator -(float2x3 lhs, float2x3 rhs)
		{
			return new float2x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0003721F File Offset: 0x0003541F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator -(float2x3 lhs, float rhs)
		{
			return new float2x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003724A File Offset: 0x0003544A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator -(float lhs, float2x3 rhs)
		{
			return new float2x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x00037275 File Offset: 0x00035475
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator /(float2x3 lhs, float2x3 rhs)
		{
			return new float2x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x000372AF File Offset: 0x000354AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator /(float2x3 lhs, float rhs)
		{
			return new float2x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x000372DA File Offset: 0x000354DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator /(float lhs, float2x3 rhs)
		{
			return new float2x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x00037305 File Offset: 0x00035505
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator %(float2x3 lhs, float2x3 rhs)
		{
			return new float2x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0003733F File Offset: 0x0003553F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator %(float2x3 lhs, float rhs)
		{
			return new float2x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0003736A File Offset: 0x0003556A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator %(float lhs, float2x3 rhs)
		{
			return new float2x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x00037398 File Offset: 0x00035598
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator ++(float2x3 val)
		{
			float2 @float = float2.op_Increment(val.c0);
			val.c0 = @float;
			float2 float2 = @float;
			@float = float2.op_Increment(val.c1);
			val.c1 = @float;
			float2 float3 = @float;
			@float = float2.op_Increment(val.c2);
			val.c2 = @float;
			return new float2x3(float2, float3, @float);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x000373F8 File Offset: 0x000355F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator --(float2x3 val)
		{
			float2 @float = float2.op_Decrement(val.c0);
			val.c0 = @float;
			float2 float2 = @float;
			@float = float2.op_Decrement(val.c1);
			val.c1 = @float;
			float2 float3 = @float;
			@float = float2.op_Decrement(val.c2);
			val.c2 = @float;
			return new float2x3(float2, float3, @float);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00037458 File Offset: 0x00035658
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(float2x3 lhs, float2x3 rhs)
		{
			return new bool2x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00037492 File Offset: 0x00035692
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(float2x3 lhs, float rhs)
		{
			return new bool2x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000374BD File Offset: 0x000356BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <(float lhs, float2x3 rhs)
		{
			return new bool2x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000374E8 File Offset: 0x000356E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(float2x3 lhs, float2x3 rhs)
		{
			return new bool2x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00037522 File Offset: 0x00035722
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(float2x3 lhs, float rhs)
		{
			return new bool2x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0003754D File Offset: 0x0003574D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator <=(float lhs, float2x3 rhs)
		{
			return new bool2x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x00037578 File Offset: 0x00035778
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(float2x3 lhs, float2x3 rhs)
		{
			return new bool2x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000375B2 File Offset: 0x000357B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(float2x3 lhs, float rhs)
		{
			return new bool2x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000375DD File Offset: 0x000357DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >(float lhs, float2x3 rhs)
		{
			return new bool2x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x00037608 File Offset: 0x00035808
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(float2x3 lhs, float2x3 rhs)
		{
			return new bool2x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00037642 File Offset: 0x00035842
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(float2x3 lhs, float rhs)
		{
			return new bool2x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x0003766D File Offset: 0x0003586D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator >=(float lhs, float2x3 rhs)
		{
			return new bool2x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x00037698 File Offset: 0x00035898
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator -(float2x3 val)
		{
			return new float2x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x000376C0 File Offset: 0x000358C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 operator +(float2x3 val)
		{
			return new float2x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x000376E8 File Offset: 0x000358E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(float2x3 lhs, float2x3 rhs)
		{
			return new bool2x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x00037722 File Offset: 0x00035922
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(float2x3 lhs, float rhs)
		{
			return new bool2x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0003774D File Offset: 0x0003594D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator ==(float lhs, float2x3 rhs)
		{
			return new bool2x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x00037778 File Offset: 0x00035978
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(float2x3 lhs, float2x3 rhs)
		{
			return new bool2x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000377B2 File Offset: 0x000359B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(float2x3 lhs, float rhs)
		{
			return new bool2x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000377DD File Offset: 0x000359DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 operator !=(float lhs, float2x3 rhs)
		{
			return new bool2x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x170003F9 RID: 1017
		public unsafe ref float2 this[int index]
		{
			get
			{
				fixed (float2x3* ptr = &this)
				{
					return ref *(float2*)(ptr + (IntPtr)index * (IntPtr)sizeof(float2) / (IntPtr)sizeof(float2x3));
				}
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00037823 File Offset: 0x00035A23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float2x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x00037860 File Offset: 0x00035A60
		public override bool Equals(object o)
		{
			if (o is float2x3)
			{
				float2x3 converted = (float2x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00037885 File Offset: 0x00035A85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x00037894 File Offset: 0x00035A94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float2x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y
			});
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x00037924 File Offset: 0x00035B24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float2x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000AF RID: 175
		public float2 c0;

		// Token: 0x040000B0 RID: 176
		public float2 c1;

		// Token: 0x040000B1 RID: 177
		public float2 c2;

		// Token: 0x040000B2 RID: 178
		public static readonly float2x3 zero;
	}
}
