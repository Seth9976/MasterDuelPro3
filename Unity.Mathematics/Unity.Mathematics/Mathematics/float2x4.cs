using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200002F RID: 47
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float2x4 : IEquatable<float2x4>, IFormattable
	{
		// Token: 0x06001176 RID: 4470 RVA: 0x000379BF File Offset: 0x00035BBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x4(float2 c0, float2 c1, float2 c2, float2 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000379DE File Offset: 0x00035BDE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13)
		{
			this.c0 = new float2(m00, m10);
			this.c1 = new float2(m01, m11);
			this.c2 = new float2(m02, m12);
			this.c3 = new float2(m03, m13);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00037A19 File Offset: 0x00035C19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x4(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x00037A4C File Offset: 0x00035C4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x4(bool v)
		{
			this.c0 = math.select(new float2(0f), new float2(1f), v);
			this.c1 = math.select(new float2(0f), new float2(1f), v);
			this.c2 = math.select(new float2(0f), new float2(1f), v);
			this.c3 = math.select(new float2(0f), new float2(1f), v);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x00037ADC File Offset: 0x00035CDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x4(bool2x4 v)
		{
			this.c0 = math.select(new float2(0f), new float2(1f), v.c0);
			this.c1 = math.select(new float2(0f), new float2(1f), v.c1);
			this.c2 = math.select(new float2(0f), new float2(1f), v.c2);
			this.c3 = math.select(new float2(0f), new float2(1f), v.c3);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00037B7D File Offset: 0x00035D7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00037BB0 File Offset: 0x00035DB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x4(int2x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00037C01 File Offset: 0x00035E01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00037C34 File Offset: 0x00035E34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x4(uint2x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00037C85 File Offset: 0x00035E85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x4(double v)
		{
			this.c0 = (float2)v;
			this.c1 = (float2)v;
			this.c2 = (float2)v;
			this.c3 = (float2)v;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00037CB8 File Offset: 0x00035EB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2x4(double2x4 v)
		{
			this.c0 = (float2)v.c0;
			this.c1 = (float2)v.c1;
			this.c2 = (float2)v.c2;
			this.c3 = (float2)v.c3;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x4(float v)
		{
			return new float2x4(v);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x4(bool v)
		{
			return new float2x4(v);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x4(bool2x4 v)
		{
			return new float2x4(v);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0000BED8 File Offset: 0x0000A0D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x4(int v)
		{
			return new float2x4(v);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0000BEE0 File Offset: 0x0000A0E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x4(int2x4 v)
		{
			return new float2x4(v);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x4(uint v)
		{
			return new float2x4(v);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2x4(uint2x4 v)
		{
			return new float2x4(v);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x4(double v)
		{
			return new float2x4(v);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0000BF00 File Offset: 0x0000A100
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2x4(double2x4 v)
		{
			return new float2x4(v);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x00037D0C File Offset: 0x00035F0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator *(float2x4 lhs, float2x4 rhs)
		{
			return new float2x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00037D62 File Offset: 0x00035F62
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator *(float2x4 lhs, float rhs)
		{
			return new float2x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x00037D99 File Offset: 0x00035F99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator *(float lhs, float2x4 rhs)
		{
			return new float2x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00037DD0 File Offset: 0x00035FD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator +(float2x4 lhs, float2x4 rhs)
		{
			return new float2x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00037E26 File Offset: 0x00036026
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator +(float2x4 lhs, float rhs)
		{
			return new float2x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x00037E5D File Offset: 0x0003605D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator +(float lhs, float2x4 rhs)
		{
			return new float2x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x00037E94 File Offset: 0x00036094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator -(float2x4 lhs, float2x4 rhs)
		{
			return new float2x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00037EEA File Offset: 0x000360EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator -(float2x4 lhs, float rhs)
		{
			return new float2x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00037F21 File Offset: 0x00036121
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator -(float lhs, float2x4 rhs)
		{
			return new float2x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00037F58 File Offset: 0x00036158
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator /(float2x4 lhs, float2x4 rhs)
		{
			return new float2x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x00037FAE File Offset: 0x000361AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator /(float2x4 lhs, float rhs)
		{
			return new float2x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x00037FE5 File Offset: 0x000361E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator /(float lhs, float2x4 rhs)
		{
			return new float2x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0003801C File Offset: 0x0003621C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator %(float2x4 lhs, float2x4 rhs)
		{
			return new float2x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00038072 File Offset: 0x00036272
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator %(float2x4 lhs, float rhs)
		{
			return new float2x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000380A9 File Offset: 0x000362A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator %(float lhs, float2x4 rhs)
		{
			return new float2x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000380E0 File Offset: 0x000362E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator ++(float2x4 val)
		{
			float2 @float = float2.op_Increment(val.c0);
			val.c0 = @float;
			float2 float2 = @float;
			@float = float2.op_Increment(val.c1);
			val.c1 = @float;
			float2 float3 = @float;
			@float = float2.op_Increment(val.c2);
			val.c2 = @float;
			float2 float4 = @float;
			@float = float2.op_Increment(val.c3);
			val.c3 = @float;
			return new float2x4(float2, float3, float4, @float);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0003815C File Offset: 0x0003635C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator --(float2x4 val)
		{
			float2 @float = float2.op_Decrement(val.c0);
			val.c0 = @float;
			float2 float2 = @float;
			@float = float2.op_Decrement(val.c1);
			val.c1 = @float;
			float2 float3 = @float;
			@float = float2.op_Decrement(val.c2);
			val.c2 = @float;
			float2 float4 = @float;
			@float = float2.op_Decrement(val.c3);
			val.c3 = @float;
			return new float2x4(float2, float3, float4, @float);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000381D8 File Offset: 0x000363D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(float2x4 lhs, float2x4 rhs)
		{
			return new bool2x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0003822E File Offset: 0x0003642E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(float2x4 lhs, float rhs)
		{
			return new bool2x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00038265 File Offset: 0x00036465
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <(float lhs, float2x4 rhs)
		{
			return new bool2x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0003829C File Offset: 0x0003649C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(float2x4 lhs, float2x4 rhs)
		{
			return new bool2x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000382F2 File Offset: 0x000364F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(float2x4 lhs, float rhs)
		{
			return new bool2x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00038329 File Offset: 0x00036529
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator <=(float lhs, float2x4 rhs)
		{
			return new bool2x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00038360 File Offset: 0x00036560
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(float2x4 lhs, float2x4 rhs)
		{
			return new bool2x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000383B6 File Offset: 0x000365B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(float2x4 lhs, float rhs)
		{
			return new bool2x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x000383ED File Offset: 0x000365ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >(float lhs, float2x4 rhs)
		{
			return new bool2x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00038424 File Offset: 0x00036624
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(float2x4 lhs, float2x4 rhs)
		{
			return new bool2x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0003847A File Offset: 0x0003667A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(float2x4 lhs, float rhs)
		{
			return new bool2x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000384B1 File Offset: 0x000366B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator >=(float lhs, float2x4 rhs)
		{
			return new bool2x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000384E8 File Offset: 0x000366E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator -(float2x4 val)
		{
			return new float2x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0003851B File Offset: 0x0003671B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 operator +(float2x4 val)
		{
			return new float2x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00038550 File Offset: 0x00036750
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(float2x4 lhs, float2x4 rhs)
		{
			return new bool2x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000385A6 File Offset: 0x000367A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(float2x4 lhs, float rhs)
		{
			return new bool2x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000385DD File Offset: 0x000367DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator ==(float lhs, float2x4 rhs)
		{
			return new bool2x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00038614 File Offset: 0x00036814
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(float2x4 lhs, float2x4 rhs)
		{
			return new bool2x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0003866A File Offset: 0x0003686A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(float2x4 lhs, float rhs)
		{
			return new bool2x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000386A1 File Offset: 0x000368A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 operator !=(float lhs, float2x4 rhs)
		{
			return new bool2x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x170003FA RID: 1018
		public unsafe ref float2 this[int index]
		{
			get
			{
				fixed (float2x4* ptr = &this)
				{
					return ref *(float2*)(ptr + (IntPtr)index * (IntPtr)sizeof(float2) / (IntPtr)sizeof(float2x4));
				}
			}
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x000386F4 File Offset: 0x000368F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float2x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00038750 File Offset: 0x00036950
		public override bool Equals(object o)
		{
			if (o is float2x4)
			{
				float2x4 converted = (float2x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00038775 File Offset: 0x00036975
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x00038784 File Offset: 0x00036984
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float2x4({0}f, {1}f, {2}f, {3}f,  {4}f, {5}f, {6}f, {7}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c3.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c3.y
			});
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x0003883C File Offset: 0x00036A3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float2x4({0}f, {1}f, {2}f, {3}f,  {4}f, {5}f, {6}f, {7}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c3.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c3.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000B3 RID: 179
		public float2 c0;

		// Token: 0x040000B4 RID: 180
		public float2 c1;

		// Token: 0x040000B5 RID: 181
		public float2 c2;

		// Token: 0x040000B6 RID: 182
		public float2 c3;

		// Token: 0x040000B7 RID: 183
		public static readonly float2x4 zero;
	}
}
