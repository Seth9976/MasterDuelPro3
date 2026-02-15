using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Unity.Mathematics
{
	// Token: 0x02000039 RID: 57
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float4x4 : IEquatable<float4x4>, IFormattable
	{
		// Token: 0x060015AE RID: 5550 RVA: 0x00042289 File Offset: 0x00040489
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(float4 c0, float4 c1, float4 c2, float4 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x000422A8 File Offset: 0x000404A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33)
		{
			this.c0 = new float4(m00, m10, m20, m30);
			this.c1 = new float4(m01, m11, m21, m31);
			this.c2 = new float4(m02, m12, m22, m32);
			this.c3 = new float4(m03, m13, m23, m33);
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x000422FE File Offset: 0x000404FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00042330 File Offset: 0x00040530
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(bool v)
		{
			this.c0 = math.select(new float4(0f), new float4(1f), v);
			this.c1 = math.select(new float4(0f), new float4(1f), v);
			this.c2 = math.select(new float4(0f), new float4(1f), v);
			this.c3 = math.select(new float4(0f), new float4(1f), v);
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x000423C0 File Offset: 0x000405C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(bool4x4 v)
		{
			this.c0 = math.select(new float4(0f), new float4(1f), v.c0);
			this.c1 = math.select(new float4(0f), new float4(1f), v.c1);
			this.c2 = math.select(new float4(0f), new float4(1f), v.c2);
			this.c3 = math.select(new float4(0f), new float4(1f), v.c3);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00042461 File Offset: 0x00040661
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00042494 File Offset: 0x00040694
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(int4x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x000424E5 File Offset: 0x000406E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00042518 File Offset: 0x00040718
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(uint4x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00042569 File Offset: 0x00040769
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(double v)
		{
			this.c0 = (float4)v;
			this.c1 = (float4)v;
			this.c2 = (float4)v;
			this.c3 = (float4)v;
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x0004259C File Offset: 0x0004079C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4x4(double4x4 v)
		{
			this.c0 = (float4)v.c0;
			this.c1 = (float4)v.c1;
			this.c2 = (float4)v.c2;
			this.c3 = (float4)v.c3;
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x0000D0F2 File Offset: 0x0000B2F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x4(float v)
		{
			return new float4x4(v);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x0000D0FA File Offset: 0x0000B2FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x4(bool v)
		{
			return new float4x4(v);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0000D102 File Offset: 0x0000B302
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x4(bool4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0000D10A File Offset: 0x0000B30A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x4(int v)
		{
			return new float4x4(v);
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0000D112 File Offset: 0x0000B312
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x4(int4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0000D11A File Offset: 0x0000B31A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x4(uint v)
		{
			return new float4x4(v);
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x0000D122 File Offset: 0x0000B322
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4x4(uint4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x0000D12A File Offset: 0x0000B32A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x4(double v)
		{
			return new float4x4(v);
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x0000D132 File Offset: 0x0000B332
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4x4(double4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000425F0 File Offset: 0x000407F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator *(float4x4 lhs, float4x4 rhs)
		{
			return new float4x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x00042646 File Offset: 0x00040846
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator *(float4x4 lhs, float rhs)
		{
			return new float4x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0004267D File Offset: 0x0004087D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator *(float lhs, float4x4 rhs)
		{
			return new float4x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x000426B4 File Offset: 0x000408B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator +(float4x4 lhs, float4x4 rhs)
		{
			return new float4x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0004270A File Offset: 0x0004090A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator +(float4x4 lhs, float rhs)
		{
			return new float4x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00042741 File Offset: 0x00040941
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator +(float lhs, float4x4 rhs)
		{
			return new float4x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00042778 File Offset: 0x00040978
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator -(float4x4 lhs, float4x4 rhs)
		{
			return new float4x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000427CE File Offset: 0x000409CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator -(float4x4 lhs, float rhs)
		{
			return new float4x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00042805 File Offset: 0x00040A05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator -(float lhs, float4x4 rhs)
		{
			return new float4x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0004283C File Offset: 0x00040A3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator /(float4x4 lhs, float4x4 rhs)
		{
			return new float4x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00042892 File Offset: 0x00040A92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator /(float4x4 lhs, float rhs)
		{
			return new float4x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000428C9 File Offset: 0x00040AC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator /(float lhs, float4x4 rhs)
		{
			return new float4x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00042900 File Offset: 0x00040B00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator %(float4x4 lhs, float4x4 rhs)
		{
			return new float4x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00042956 File Offset: 0x00040B56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator %(float4x4 lhs, float rhs)
		{
			return new float4x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0004298D File Offset: 0x00040B8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator %(float lhs, float4x4 rhs)
		{
			return new float4x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000429C4 File Offset: 0x00040BC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator ++(float4x4 val)
		{
			float4 @float = float4.op_Increment(val.c0);
			val.c0 = @float;
			float4 float2 = @float;
			@float = float4.op_Increment(val.c1);
			val.c1 = @float;
			float4 float3 = @float;
			@float = float4.op_Increment(val.c2);
			val.c2 = @float;
			float4 float4 = @float;
			@float = float4.op_Increment(val.c3);
			val.c3 = @float;
			return new float4x4(float2, float3, float4, @float);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00042A40 File Offset: 0x00040C40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator --(float4x4 val)
		{
			float4 @float = float4.op_Decrement(val.c0);
			val.c0 = @float;
			float4 float2 = @float;
			@float = float4.op_Decrement(val.c1);
			val.c1 = @float;
			float4 float3 = @float;
			@float = float4.op_Decrement(val.c2);
			val.c2 = @float;
			float4 float4 = @float;
			@float = float4.op_Decrement(val.c3);
			val.c3 = @float;
			return new float4x4(float2, float3, float4, @float);
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00042ABC File Offset: 0x00040CBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(float4x4 lhs, float4x4 rhs)
		{
			return new bool4x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00042B12 File Offset: 0x00040D12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(float4x4 lhs, float rhs)
		{
			return new bool4x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00042B49 File Offset: 0x00040D49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(float lhs, float4x4 rhs)
		{
			return new bool4x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00042B80 File Offset: 0x00040D80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(float4x4 lhs, float4x4 rhs)
		{
			return new bool4x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00042BD6 File Offset: 0x00040DD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(float4x4 lhs, float rhs)
		{
			return new bool4x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00042C0D File Offset: 0x00040E0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(float lhs, float4x4 rhs)
		{
			return new bool4x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00042C44 File Offset: 0x00040E44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(float4x4 lhs, float4x4 rhs)
		{
			return new bool4x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00042C9A File Offset: 0x00040E9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(float4x4 lhs, float rhs)
		{
			return new bool4x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00042CD1 File Offset: 0x00040ED1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(float lhs, float4x4 rhs)
		{
			return new bool4x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00042D08 File Offset: 0x00040F08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(float4x4 lhs, float4x4 rhs)
		{
			return new bool4x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00042D5E File Offset: 0x00040F5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(float4x4 lhs, float rhs)
		{
			return new bool4x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00042D95 File Offset: 0x00040F95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(float lhs, float4x4 rhs)
		{
			return new bool4x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00042DCC File Offset: 0x00040FCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator -(float4x4 val)
		{
			return new float4x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00042DFF File Offset: 0x00040FFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 operator +(float4x4 val)
		{
			return new float4x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00042E34 File Offset: 0x00041034
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(float4x4 lhs, float4x4 rhs)
		{
			return new bool4x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00042E8A File Offset: 0x0004108A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(float4x4 lhs, float rhs)
		{
			return new bool4x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x00042EC1 File Offset: 0x000410C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(float lhs, float4x4 rhs)
		{
			return new bool4x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00042EF8 File Offset: 0x000410F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(float4x4 lhs, float4x4 rhs)
		{
			return new bool4x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00042F4E File Offset: 0x0004114E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(float4x4 lhs, float rhs)
		{
			return new bool4x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00042F85 File Offset: 0x00041185
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(float lhs, float4x4 rhs)
		{
			return new bool4x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x170005C7 RID: 1479
		public unsafe ref float4 this[int index]
		{
			get
			{
				fixed (float4x4* ptr = &this)
				{
					return ref *(float4*)(ptr + (IntPtr)index * (IntPtr)sizeof(float4) / (IntPtr)sizeof(float4x4));
				}
			}
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00042FD8 File Offset: 0x000411D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float4x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00043034 File Offset: 0x00041234
		public override bool Equals(object o)
		{
			if (o is float4x4)
			{
				float4x4 converted = (float4x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00043059 File Offset: 0x00041259
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00043068 File Offset: 0x00041268
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float4x4({0}f, {1}f, {2}f, {3}f,  {4}f, {5}f, {6}f, {7}f,  {8}f, {9}f, {10}f, {11}f,  {12}f, {13}f, {14}f, {15}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c3.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c3.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c3.z,
				this.c0.w,
				this.c1.w,
				this.c2.w,
				this.c3.w
			});
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x000431C0 File Offset: 0x000413C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float4x4({0}f, {1}f, {2}f, {3}f,  {4}f, {5}f, {6}f, {7}f,  {8}f, {9}f, {10}f, {11}f,  {12}f, {13}f, {14}f, {15}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c3.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c3.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider),
				this.c3.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider),
				this.c2.w.ToString(format, formatProvider),
				this.c3.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00043335 File Offset: 0x00041535
		public static implicit operator float4x4(Matrix4x4 m)
		{
			return new float4x4(m.GetColumn(0), m.GetColumn(1), m.GetColumn(2), m.GetColumn(3));
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00043370 File Offset: 0x00041570
		public static implicit operator Matrix4x4(float4x4 m)
		{
			return new Matrix4x4(m.c0, m.c1, m.c2, m.c3);
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x000433A4 File Offset: 0x000415A4
		public float4x4(float3x3 rotation, float3 translation)
		{
			this.c0 = math.float4(rotation.c0, 0f);
			this.c1 = math.float4(rotation.c1, 0f);
			this.c2 = math.float4(rotation.c2, 0f);
			this.c3 = math.float4(translation, 1f);
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00043404 File Offset: 0x00041604
		public float4x4(quaternion rotation, float3 translation)
		{
			float3x3 rot = math.float3x3(rotation);
			this.c0 = math.float4(rot.c0, 0f);
			this.c1 = math.float4(rot.c1, 0f);
			this.c2 = math.float4(rot.c2, 0f);
			this.c3 = math.float4(translation, 1f);
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x0004346C File Offset: 0x0004166C
		public float4x4(RigidTransform transform)
		{
			float3x3 rot = math.float3x3(transform.rot);
			this.c0 = math.float4(rot.c0, 0f);
			this.c1 = math.float4(rot.c1, 0f);
			this.c2 = math.float4(rot.c2, 0f);
			this.c3 = math.float4(transform.pos, 1f);
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000434E0 File Offset: 0x000416E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 AxisAngle(float3 axis, float angle)
		{
			float sina;
			float cosa;
			math.sincos(angle, out sina, out cosa);
			float4 u = math.float4(axis, 0f);
			float4 yzxx = u.yzxx;
			float4 zxyx = u.zxyx;
			float4 u_inv_cosa = u - u * cosa;
			float4 t = math.float4(u.xyz * sina, cosa);
			uint4 ppnp = math.uint4(0U, 0U, 2147483648U, 0U);
			uint4 nppp = math.uint4(2147483648U, 0U, 0U, 0U);
			uint4 pnpp = math.uint4(0U, 2147483648U, 0U, 0U);
			uint4 mask = math.uint4(uint.MaxValue, uint.MaxValue, uint.MaxValue, 0U);
			return math.float4x4(u.x * u_inv_cosa + math.asfloat((math.asuint(t.wzyx) ^ ppnp) & mask), u.y * u_inv_cosa + math.asfloat((math.asuint(t.zwxx) ^ nppp) & mask), u.z * u_inv_cosa + math.asfloat((math.asuint(t.yxwx) ^ pnpp) & mask), math.float4(0f, 0f, 0f, 1f));
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0004361C File Offset: 0x0004181C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerXYZ(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(xyz, out s, out c);
			return math.float4x4(c.y * c.z, c.z * s.x * s.y - c.x * s.z, c.x * c.z * s.y + s.x * s.z, 0f, c.y * s.z, c.x * c.z + s.x * s.y * s.z, c.x * s.y * s.z - c.z * s.x, 0f, -s.y, c.y * s.x, c.x * c.y, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00043720 File Offset: 0x00041920
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerXZY(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(xyz, out s, out c);
			return math.float4x4(c.y * c.z, s.x * s.y - c.x * c.y * s.z, c.x * s.y + c.y * s.x * s.z, 0f, s.z, c.x * c.z, -c.z * s.x, 0f, -c.z * s.y, c.y * s.x + c.x * s.y * s.z, c.x * c.y - s.x * s.y * s.z, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00043824 File Offset: 0x00041A24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerYXZ(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(xyz, out s, out c);
			return math.float4x4(c.y * c.z - s.x * s.y * s.z, -c.x * s.z, c.z * s.y + c.y * s.x * s.z, 0f, c.z * s.x * s.y + c.y * s.z, c.x * c.z, s.y * s.z - c.y * c.z * s.x, 0f, -c.x * s.y, s.x, c.x * c.y, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00043928 File Offset: 0x00041B28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerYZX(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(xyz, out s, out c);
			return math.float4x4(c.y * c.z, -s.z, c.z * s.y, 0f, s.x * s.y + c.x * c.y * s.z, c.x * c.z, c.x * s.y * s.z - c.y * s.x, 0f, c.y * s.x * s.z - c.x * s.y, c.z * s.x, c.x * c.y + s.x * s.y * s.z, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00043A2C File Offset: 0x00041C2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerZXY(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(xyz, out s, out c);
			return math.float4x4(c.y * c.z + s.x * s.y * s.z, c.z * s.x * s.y - c.y * s.z, c.x * s.y, 0f, c.x * s.z, c.x * c.z, -s.x, 0f, c.y * s.x * s.z - c.z * s.y, c.y * c.z * s.x + s.y * s.z, c.x * c.y, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00043B30 File Offset: 0x00041D30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerZYX(float3 xyz)
		{
			float3 s;
			float3 c;
			math.sincos(xyz, out s, out c);
			return math.float4x4(c.y * c.z, -c.y * s.z, s.y, 0f, c.z * s.x * s.y + c.x * s.z, c.x * c.z - s.x * s.y * s.z, -c.y * s.x, 0f, s.x * s.z - c.x * c.z * s.y, c.z * s.x + c.x * s.y * s.z, c.x * c.y, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00043C33 File Offset: 0x00041E33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerXYZ(float x, float y, float z)
		{
			return float4x4.EulerXYZ(math.float3(x, y, z));
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00043C42 File Offset: 0x00041E42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerXZY(float x, float y, float z)
		{
			return float4x4.EulerXZY(math.float3(x, y, z));
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00043C51 File Offset: 0x00041E51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerYXZ(float x, float y, float z)
		{
			return float4x4.EulerYXZ(math.float3(x, y, z));
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00043C60 File Offset: 0x00041E60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerYZX(float x, float y, float z)
		{
			return float4x4.EulerYZX(math.float3(x, y, z));
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00043C6F File Offset: 0x00041E6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerZXY(float x, float y, float z)
		{
			return float4x4.EulerZXY(math.float3(x, y, z));
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00043C7E File Offset: 0x00041E7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 EulerZYX(float x, float y, float z)
		{
			return float4x4.EulerZYX(math.float3(x, y, z));
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00043C90 File Offset: 0x00041E90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Euler(float3 xyz, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			switch (order)
			{
			case math.RotationOrder.XYZ:
				return float4x4.EulerXYZ(xyz);
			case math.RotationOrder.XZY:
				return float4x4.EulerXZY(xyz);
			case math.RotationOrder.YXZ:
				return float4x4.EulerYXZ(xyz);
			case math.RotationOrder.YZX:
				return float4x4.EulerYZX(xyz);
			case math.RotationOrder.ZXY:
				return float4x4.EulerZXY(xyz);
			case math.RotationOrder.ZYX:
				return float4x4.EulerZYX(xyz);
			default:
				return float4x4.identity;
			}
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00043CEC File Offset: 0x00041EEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Euler(float x, float y, float z, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			return float4x4.Euler(math.float3(x, y, z), order);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00043CFC File Offset: 0x00041EFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 RotateX(float angle)
		{
			float s;
			float c;
			math.sincos(angle, out s, out c);
			return math.float4x4(1f, 0f, 0f, 0f, 0f, c, -s, 0f, 0f, s, c, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00043D5C File Offset: 0x00041F5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 RotateY(float angle)
		{
			float s;
			float c;
			math.sincos(angle, out s, out c);
			return math.float4x4(c, 0f, s, 0f, 0f, 1f, 0f, 0f, -s, 0f, c, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00043DBC File Offset: 0x00041FBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 RotateZ(float angle)
		{
			float s;
			float c;
			math.sincos(angle, out s, out c);
			return math.float4x4(c, -s, 0f, 0f, s, c, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00043E1C File Offset: 0x0004201C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Scale(float s)
		{
			return math.float4x4(s, 0f, 0f, 0f, 0f, s, 0f, 0f, 0f, 0f, s, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00043E74 File Offset: 0x00042074
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Scale(float x, float y, float z)
		{
			return math.float4x4(x, 0f, 0f, 0f, 0f, y, 0f, 0f, 0f, 0f, z, 0f, 0f, 0f, 0f, 1f);
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00043ECA File Offset: 0x000420CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Scale(float3 scales)
		{
			return float4x4.Scale(scales.x, scales.y, scales.z);
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00043EE4 File Offset: 0x000420E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Translate(float3 vector)
		{
			return math.float4x4(math.float4(1f, 0f, 0f, 0f), math.float4(0f, 1f, 0f, 0f), math.float4(0f, 0f, 1f, 0f), math.float4(vector.x, vector.y, vector.z, 1f));
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00043F60 File Offset: 0x00042160
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 LookAt(float3 eye, float3 target, float3 up)
		{
			float3x3 rot = float3x3.LookRotation(math.normalize(target - eye), up);
			float4x4 matrix;
			matrix.c0 = math.float4(rot.c0, 0f);
			matrix.c1 = math.float4(rot.c1, 0f);
			matrix.c2 = math.float4(rot.c2, 0f);
			matrix.c3 = math.float4(eye, 1f);
			return matrix;
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00043FD8 File Offset: 0x000421D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 Ortho(float width, float height, float near, float far)
		{
			float rcpdx = 1f / width;
			float rcpdy = 1f / height;
			float rcpdz = 1f / (far - near);
			return math.float4x4(2f * rcpdx, 0f, 0f, 0f, 0f, 2f * rcpdy, 0f, 0f, 0f, 0f, -2f * rcpdz, -(far + near) * rcpdz, 0f, 0f, 0f, 1f);
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x0004405C File Offset: 0x0004225C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 OrthoOffCenter(float left, float right, float bottom, float top, float near, float far)
		{
			float rcpdx = 1f / (right - left);
			float rcpdy = 1f / (top - bottom);
			float rcpdz = 1f / (far - near);
			return math.float4x4(2f * rcpdx, 0f, 0f, -(right + left) * rcpdx, 0f, 2f * rcpdy, 0f, -(top + bottom) * rcpdy, 0f, 0f, -2f * rcpdz, -(far + near) * rcpdz, 0f, 0f, 0f, 1f);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x000440EC File Offset: 0x000422EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 PerspectiveFov(float verticalFov, float aspect, float near, float far)
		{
			float cotangent = 1f / math.tan(verticalFov * 0.5f);
			float rcpdz = 1f / (near - far);
			return math.float4x4(cotangent / aspect, 0f, 0f, 0f, 0f, cotangent, 0f, 0f, 0f, 0f, (far + near) * rcpdz, 2f * near * far * rcpdz, 0f, 0f, -1f, 0f);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0004416C File Offset: 0x0004236C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 PerspectiveOffCenter(float left, float right, float bottom, float top, float near, float far)
		{
			float rcpdz = 1f / (near - far);
			float rcpWidth = 1f / (right - left);
			float rcpHeight = 1f / (top - bottom);
			return math.float4x4(2f * near * rcpWidth, 0f, (left + right) * rcpWidth, 0f, 0f, 2f * near * rcpHeight, (bottom + top) * rcpHeight, 0f, 0f, 0f, (far + near) * rcpdz, 2f * near * far * rcpdz, 0f, 0f, -1f, 0f);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00044204 File Offset: 0x00042404
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 TRS(float3 translation, quaternion rotation, float3 scale)
		{
			float3x3 r = math.float3x3(rotation);
			return math.float4x4(math.float4(r.c0 * scale.x, 0f), math.float4(r.c1 * scale.y, 0f), math.float4(r.c2 * scale.z, 0f), math.float4(translation, 1f));
		}

		// Token: 0x040000DC RID: 220
		public float4 c0;

		// Token: 0x040000DD RID: 221
		public float4 c1;

		// Token: 0x040000DE RID: 222
		public float4 c2;

		// Token: 0x040000DF RID: 223
		public float4 c3;

		// Token: 0x040000E0 RID: 224
		public static readonly float4x4 identity = new float4x4(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

		// Token: 0x040000E1 RID: 225
		public static readonly float4x4 zero;
	}
}
