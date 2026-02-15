using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000033 RID: 51
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float3x3 : IEquatable<float3x3>, IFormattable
	{
		// Token: 0x060012BE RID: 4798 RVA: 0x0003A95B File Offset: 0x00038B5B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x3(float3 c0, float3 c1, float3 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0003A972 File Offset: 0x00038B72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22)
		{
			this.c0 = new float3(m00, m10, m20);
			this.c1 = new float3(m01, m11, m21);
			this.c2 = new float3(m02, m12, m22);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0003A9A4 File Offset: 0x00038BA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x3(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0003A9CC File Offset: 0x00038BCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x3(bool v)
		{
			this.c0 = math.select(new float3(0f), new float3(1f), v);
			this.c1 = math.select(new float3(0f), new float3(1f), v);
			this.c2 = math.select(new float3(0f), new float3(1f), v);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0003AA3C File Offset: 0x00038C3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x3(bool3x3 v)
		{
			this.c0 = math.select(new float3(0f), new float3(1f), v.c0);
			this.c1 = math.select(new float3(0f), new float3(1f), v.c1);
			this.c2 = math.select(new float3(0f), new float3(1f), v.c2);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0003AAB8 File Offset: 0x00038CB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x3(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0003AADE File Offset: 0x00038CDE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x3(int3x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0003AB13 File Offset: 0x00038D13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0003AB39 File Offset: 0x00038D39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x3(uint3x3 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0003AB6E File Offset: 0x00038D6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x3(double v)
		{
			this.c0 = (float3)v;
			this.c1 = (float3)v;
			this.c2 = (float3)v;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0003AB94 File Offset: 0x00038D94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x3(double3x3 v)
		{
			this.c0 = (float3)v.c0;
			this.c1 = (float3)v.c1;
			this.c2 = (float3)v.c2;
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0000C41C File Offset: 0x0000A61C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x3(float v)
		{
			return new float3x3(v);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0000C424 File Offset: 0x0000A624
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x3(bool v)
		{
			return new float3x3(v);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0000C42C File Offset: 0x0000A62C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x3(bool3x3 v)
		{
			return new float3x3(v);
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0000C434 File Offset: 0x0000A634
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x3(int v)
		{
			return new float3x3(v);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0000C43C File Offset: 0x0000A63C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x3(int3x3 v)
		{
			return new float3x3(v);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0000C444 File Offset: 0x0000A644
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x3(uint v)
		{
			return new float3x3(v);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0000C44C File Offset: 0x0000A64C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x3(uint3x3 v)
		{
			return new float3x3(v);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0000C454 File Offset: 0x0000A654
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x3(double v)
		{
			return new float3x3(v);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0000C45C File Offset: 0x0000A65C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x3(double3x3 v)
		{
			return new float3x3(v);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0003ABC9 File Offset: 0x00038DC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator *(float3x3 lhs, float3x3 rhs)
		{
			return new float3x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0003AC03 File Offset: 0x00038E03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator *(float3x3 lhs, float rhs)
		{
			return new float3x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0003AC2E File Offset: 0x00038E2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator *(float lhs, float3x3 rhs)
		{
			return new float3x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0003AC59 File Offset: 0x00038E59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator +(float3x3 lhs, float3x3 rhs)
		{
			return new float3x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0003AC93 File Offset: 0x00038E93
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator +(float3x3 lhs, float rhs)
		{
			return new float3x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0003ACBE File Offset: 0x00038EBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator +(float lhs, float3x3 rhs)
		{
			return new float3x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0003ACE9 File Offset: 0x00038EE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator -(float3x3 lhs, float3x3 rhs)
		{
			return new float3x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0003AD23 File Offset: 0x00038F23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator -(float3x3 lhs, float rhs)
		{
			return new float3x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0003AD4E File Offset: 0x00038F4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator -(float lhs, float3x3 rhs)
		{
			return new float3x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0003AD79 File Offset: 0x00038F79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator /(float3x3 lhs, float3x3 rhs)
		{
			return new float3x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0003ADB3 File Offset: 0x00038FB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator /(float3x3 lhs, float rhs)
		{
			return new float3x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0003ADDE File Offset: 0x00038FDE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator /(float lhs, float3x3 rhs)
		{
			return new float3x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0003AE09 File Offset: 0x00039009
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator %(float3x3 lhs, float3x3 rhs)
		{
			return new float3x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0003AE43 File Offset: 0x00039043
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator %(float3x3 lhs, float rhs)
		{
			return new float3x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0003AE6E File Offset: 0x0003906E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator %(float lhs, float3x3 rhs)
		{
			return new float3x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0003AE9C File Offset: 0x0003909C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator ++(float3x3 val)
		{
			float3 @float = float3.op_Increment(val.c0);
			val.c0 = @float;
			float3 float2 = @float;
			@float = float3.op_Increment(val.c1);
			val.c1 = @float;
			float3 float3 = @float;
			@float = float3.op_Increment(val.c2);
			val.c2 = @float;
			return new float3x3(float2, float3, @float);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0003AEFC File Offset: 0x000390FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator --(float3x3 val)
		{
			float3 @float = float3.op_Decrement(val.c0);
			val.c0 = @float;
			float3 float2 = @float;
			@float = float3.op_Decrement(val.c1);
			val.c1 = @float;
			float3 float3 = @float;
			@float = float3.op_Decrement(val.c2);
			val.c2 = @float;
			return new float3x3(float2, float3, @float);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0003AF5C File Offset: 0x0003915C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(float3x3 lhs, float3x3 rhs)
		{
			return new bool3x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0003AF96 File Offset: 0x00039196
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(float3x3 lhs, float rhs)
		{
			return new bool3x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0003AFC1 File Offset: 0x000391C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(float lhs, float3x3 rhs)
		{
			return new bool3x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0003AFEC File Offset: 0x000391EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(float3x3 lhs, float3x3 rhs)
		{
			return new bool3x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0003B026 File Offset: 0x00039226
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(float3x3 lhs, float rhs)
		{
			return new bool3x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0003B051 File Offset: 0x00039251
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(float lhs, float3x3 rhs)
		{
			return new bool3x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0003B07C File Offset: 0x0003927C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(float3x3 lhs, float3x3 rhs)
		{
			return new bool3x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0003B0B6 File Offset: 0x000392B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(float3x3 lhs, float rhs)
		{
			return new bool3x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0003B0E1 File Offset: 0x000392E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(float lhs, float3x3 rhs)
		{
			return new bool3x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0003B10C File Offset: 0x0003930C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(float3x3 lhs, float3x3 rhs)
		{
			return new bool3x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0003B146 File Offset: 0x00039346
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(float3x3 lhs, float rhs)
		{
			return new bool3x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0003B171 File Offset: 0x00039371
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(float lhs, float3x3 rhs)
		{
			return new bool3x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0003B19C File Offset: 0x0003939C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator -(float3x3 val)
		{
			return new float3x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0003B1C4 File Offset: 0x000393C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 operator +(float3x3 val)
		{
			return new float3x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0003B1EC File Offset: 0x000393EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(float3x3 lhs, float3x3 rhs)
		{
			return new bool3x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0003B226 File Offset: 0x00039426
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(float3x3 lhs, float rhs)
		{
			return new bool3x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0003B251 File Offset: 0x00039451
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(float lhs, float3x3 rhs)
		{
			return new bool3x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0003B27C File Offset: 0x0003947C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(float3x3 lhs, float3x3 rhs)
		{
			return new bool3x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0003B2B6 File Offset: 0x000394B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(float3x3 lhs, float rhs)
		{
			return new bool3x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0003B2E1 File Offset: 0x000394E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(float lhs, float3x3 rhs)
		{
			return new bool3x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x17000472 RID: 1138
		public unsafe ref float3 this[int index]
		{
			get
			{
				fixed (float3x3* ptr = &this)
				{
					return ref *(float3*)(ptr + (IntPtr)index * (IntPtr)sizeof(float3) / (IntPtr)sizeof(float3x3));
				}
			}
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0003B327 File Offset: 0x00039527
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float3x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0003B364 File Offset: 0x00039564
		public override bool Equals(object o)
		{
			if (o is float3x3)
			{
				float3x3 converted = (float3x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0003B389 File Offset: 0x00039589
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0003B398 File Offset: 0x00039598
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float3x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z
			});
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0003B464 File Offset: 0x00039664
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float3x3({0}f, {1}f, {2}f,  {3}f, {4}f, {5}f,  {6}f, {7}f, {8}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0003B53F File Offset: 0x0003973F
		public float3x3(float4x4 f4x4)
		{
			this.c0 = f4x4.c0.xyz;
			this.c1 = f4x4.c1.xyz;
			this.c2 = f4x4.c2.xyz;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0003B578 File Offset: 0x00039778
		public float3x3(quaternion q)
		{
			float4 v = q.value;
			float4 v2 = v + v;
			uint3 npn = math.uint3(2147483648U, 0U, 2147483648U);
			uint3 nnp = math.uint3(2147483648U, 2147483648U, 0U);
			uint3 pnn = math.uint3(0U, 2147483648U, 2147483648U);
			this.c0 = v2.y * math.asfloat(math.asuint(v.yxw) ^ npn) - v2.z * math.asfloat(math.asuint(v.zwx) ^ pnn) + math.float3(1f, 0f, 0f);
			this.c1 = v2.z * math.asfloat(math.asuint(v.wzy) ^ nnp) - v2.x * math.asfloat(math.asuint(v.yxw) ^ npn) + math.float3(0f, 1f, 0f);
			this.c2 = v2.x * math.asfloat(math.asuint(v.zwx) ^ pnn) - v2.y * math.asfloat(math.asuint(v.wzy) ^ nnp) + math.float3(0f, 0f, 1f);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0003B704 File Offset: 0x00039904
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 AxisAngle(float3 axis, float angle)
		{
			float sina;
			float cosa;
			math.sincos(angle, out sina, out cosa);
			float3 u = axis;
			float3 yzx = u.yzx;
			float3 zxy = u.zxy;
			float3 u_inv_cosa = u - u * cosa;
			float4 t = math.float4(u * sina, cosa);
			uint3 ppn = math.uint3(0U, 0U, 2147483648U);
			uint3 npp = math.uint3(2147483648U, 0U, 0U);
			uint3 pnp = math.uint3(0U, 2147483648U, 0U);
			return math.float3x3(u.x * u_inv_cosa + math.asfloat(math.asuint(t.wzy) ^ ppn), u.y * u_inv_cosa + math.asfloat(math.asuint(t.zwx) ^ npp), u.z * u_inv_cosa + math.asfloat(math.asuint(t.yxw) ^ pnp));
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0003B7F4 File Offset: 0x000399F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 EulerXYZ(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(xyz, out s, out c);
			return math.float3x3(c.y * c.z, c.z * s.x * s.y - c.x * s.z, c.x * c.z * s.y + s.x * s.z, c.y * s.z, c.x * c.z + s.x * s.y * s.z, c.x * s.y * s.z - c.z * s.x, -s.y, c.y * s.x, c.x * c.y);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0003B8D4 File Offset: 0x00039AD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 EulerXZY(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(xyz, out s, out c);
			return math.float3x3(c.y * c.z, s.x * s.y - c.x * c.y * s.z, c.x * s.y + c.y * s.x * s.z, s.z, c.x * c.z, -c.z * s.x, -c.z * s.y, c.y * s.x + c.x * s.y * s.z, c.x * c.y - s.x * s.y * s.z);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x0003B9B4 File Offset: 0x00039BB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 EulerYXZ(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(xyz, out s, out c);
			return math.float3x3(c.y * c.z - s.x * s.y * s.z, -c.x * s.z, c.z * s.y + c.y * s.x * s.z, c.z * s.x * s.y + c.y * s.z, c.x * c.z, s.y * s.z - c.y * c.z * s.x, -c.x * s.y, s.x, c.x * c.y);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x0003BA94 File Offset: 0x00039C94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 EulerYZX(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(xyz, out s, out c);
			return math.float3x3(c.y * c.z, -s.z, c.z * s.y, s.x * s.y + c.x * c.y * s.z, c.x * c.z, c.x * s.y * s.z - c.y * s.x, c.y * s.x * s.z - c.x * s.y, c.z * s.x, c.x * c.y + s.x * s.y * s.z);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x0003BB74 File Offset: 0x00039D74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 EulerZXY(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(xyz, out s, out c);
			return math.float3x3(c.y * c.z + s.x * s.y * s.z, c.z * s.x * s.y - c.y * s.z, c.x * s.y, c.x * s.z, c.x * c.z, -s.x, c.y * s.x * s.z - c.z * s.y, c.y * c.z * s.x + s.y * s.z, c.x * c.y);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x0003BC54 File Offset: 0x00039E54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 EulerZYX(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(xyz, out s, out c);
			return math.float3x3(c.y * c.z, -c.y * s.z, s.y, c.z * s.x * s.y + c.x * s.z, c.x * c.z - s.x * s.y * s.z, -c.y * s.x, s.x * s.z - c.x * c.z * s.y, c.z * s.x + c.x * s.y * s.z, c.x * c.y);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x0003BD34 File Offset: 0x00039F34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 EulerXYZ(float x, float y, float z)
		{
			return float3x3.EulerXYZ(math.float3(x, y, z));
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0003BD43 File Offset: 0x00039F43
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 EulerXZY(float x, float y, float z)
		{
			return float3x3.EulerXZY(math.float3(x, y, z));
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0003BD52 File Offset: 0x00039F52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 EulerYXZ(float x, float y, float z)
		{
			return float3x3.EulerYXZ(math.float3(x, y, z));
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x0003BD61 File Offset: 0x00039F61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 EulerYZX(float x, float y, float z)
		{
			return float3x3.EulerYZX(math.float3(x, y, z));
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x0003BD70 File Offset: 0x00039F70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 EulerZXY(float x, float y, float z)
		{
			return float3x3.EulerZXY(math.float3(x, y, z));
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x0003BD7F File Offset: 0x00039F7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 EulerZYX(float x, float y, float z)
		{
			return float3x3.EulerZYX(math.float3(x, y, z));
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x0003BD90 File Offset: 0x00039F90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 Euler(float3 xyz, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			switch (order)
			{
			case math.RotationOrder.XYZ:
				return float3x3.EulerXYZ(xyz);
			case math.RotationOrder.XZY:
				return float3x3.EulerXZY(xyz);
			case math.RotationOrder.YXZ:
				return float3x3.EulerYXZ(xyz);
			case math.RotationOrder.YZX:
				return float3x3.EulerYZX(xyz);
			case math.RotationOrder.ZXY:
				return float3x3.EulerZXY(xyz);
			case math.RotationOrder.ZYX:
				return float3x3.EulerZYX(xyz);
			default:
				return float3x3.identity;
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x0003BDEC File Offset: 0x00039FEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 Euler(float x, float y, float z, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			return float3x3.Euler(math.float3(x, y, z), order);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x0003BDFC File Offset: 0x00039FFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 RotateX(float angle)
		{
			float s;
			float c;
			math.sincos(angle, out s, out c);
			return math.float3x3(1f, 0f, 0f, 0f, c, -s, 0f, s, c);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x0003BE38 File Offset: 0x0003A038
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 RotateY(float angle)
		{
			float s;
			float c;
			math.sincos(angle, out s, out c);
			return math.float3x3(c, 0f, s, 0f, 1f, 0f, -s, 0f, c);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x0003BE74 File Offset: 0x0003A074
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 RotateZ(float angle)
		{
			float s;
			float c;
			math.sincos(angle, out s, out c);
			return math.float3x3(c, -s, 0f, s, c, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x0003BEB0 File Offset: 0x0003A0B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 Scale(float s)
		{
			return math.float3x3(s, 0f, 0f, 0f, s, 0f, 0f, 0f, s);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x0003BEE4 File Offset: 0x0003A0E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 Scale(float x, float y, float z)
		{
			return math.float3x3(x, 0f, 0f, 0f, y, 0f, 0f, 0f, z);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0003BF17 File Offset: 0x0003A117
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 Scale(float3 v)
		{
			return float3x3.Scale(v.x, v.y, v.z);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0003BF30 File Offset: 0x0003A130
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 LookRotation(float3 forward, float3 up)
		{
			float3 t = math.normalize(math.cross(up, forward));
			return math.float3x3(t, math.cross(forward, t), forward);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0003BF58 File Offset: 0x0003A158
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 LookRotationSafe(float3 forward, float3 up)
		{
			float forwardLengthSq = math.dot(forward, forward);
			float upLengthSq = math.dot(up, up);
			forward *= math.rsqrt(forwardLengthSq);
			up *= math.rsqrt(upLengthSq);
			float3 t = math.cross(up, forward);
			float tLengthSq = math.dot(t, t);
			t *= math.rsqrt(tLengthSq);
			float num = math.min(math.min(forwardLengthSq, upLengthSq), tLengthSq);
			float mx = math.max(math.max(forwardLengthSq, upLengthSq), tLengthSq);
			bool accept = num > 1E-35f && mx < 1E+35f && math.isfinite(forwardLengthSq) && math.isfinite(upLengthSq) && math.isfinite(tLengthSq);
			return math.float3x3(math.select(math.float3(1f, 0f, 0f), t, accept), math.select(math.float3(0f, 1f, 0f), math.cross(forward, t), accept), math.select(math.float3(0f, 0f, 1f), forward, accept));
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000167F0 File Offset: 0x000149F0
		public static explicit operator float3x3(float4x4 f4x4)
		{
			return new float3x3(f4x4);
		}

		// Token: 0x040000C2 RID: 194
		public float3 c0;

		// Token: 0x040000C3 RID: 195
		public float3 c1;

		// Token: 0x040000C4 RID: 196
		public float3 c2;

		// Token: 0x040000C5 RID: 197
		public static readonly float3x3 identity = new float3x3(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f);

		// Token: 0x040000C6 RID: 198
		public static readonly float3x3 zero;
	}
}
