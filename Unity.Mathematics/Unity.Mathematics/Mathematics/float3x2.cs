using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000032 RID: 50
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float3x2 : IEquatable<float3x2>, IFormattable
	{
		// Token: 0x0600127F RID: 4735 RVA: 0x0003A0D4 File Offset: 0x000382D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(float3 c0, float3 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x06001280 RID: 4736 RVA: 0x0003A0E4 File Offset: 0x000382E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(float m00, float m01, float m10, float m11, float m20, float m21)
		{
			this.c0 = new float3(m00, m10, m20);
			this.c1 = new float3(m01, m11, m21);
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0003A105 File Offset: 0x00038305
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(float v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0003A120 File Offset: 0x00038320
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(bool v)
		{
			this.c0 = math.select(new float3(0f), new float3(1f), v);
			this.c1 = math.select(new float3(0f), new float3(1f), v);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0003A170 File Offset: 0x00038370
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(bool3x2 v)
		{
			this.c0 = math.select(new float3(0f), new float3(1f), v.c0);
			this.c1 = math.select(new float3(0f), new float3(1f), v.c1);
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0003A1C7 File Offset: 0x000383C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(int v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x0003A1E1 File Offset: 0x000383E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(int3x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0003A205 File Offset: 0x00038405
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06001287 RID: 4743 RVA: 0x0003A21F File Offset: 0x0003841F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(uint3x2 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
		}

		// Token: 0x06001288 RID: 4744 RVA: 0x0003A243 File Offset: 0x00038443
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(double v)
		{
			this.c0 = (float3)v;
			this.c1 = (float3)v;
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x0003A25D File Offset: 0x0003845D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x2(double3x2 v)
		{
			this.c0 = (float3)v.c0;
			this.c1 = (float3)v.c1;
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0000C287 File Offset: 0x0000A487
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x2(float v)
		{
			return new float3x2(v);
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0000C28F File Offset: 0x0000A48F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x2(bool v)
		{
			return new float3x2(v);
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x0000C297 File Offset: 0x0000A497
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x2(bool3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0000C29F File Offset: 0x0000A49F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x2(int v)
		{
			return new float3x2(v);
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x0000C2A7 File Offset: 0x0000A4A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x2(int3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x0000C2AF File Offset: 0x0000A4AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x2(uint v)
		{
			return new float3x2(v);
		}

		// Token: 0x06001290 RID: 4752 RVA: 0x0000C2B7 File Offset: 0x0000A4B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x2(uint3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x0000C2BF File Offset: 0x0000A4BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x2(double v)
		{
			return new float3x2(v);
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x0000C2C7 File Offset: 0x0000A4C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x2(double3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x06001293 RID: 4755 RVA: 0x0003A281 File Offset: 0x00038481
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator *(float3x2 lhs, float3x2 rhs)
		{
			return new float3x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06001294 RID: 4756 RVA: 0x0003A2AA File Offset: 0x000384AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator *(float3x2 lhs, float rhs)
		{
			return new float3x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0003A2C9 File Offset: 0x000384C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator *(float lhs, float3x2 rhs)
		{
			return new float3x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0003A2E8 File Offset: 0x000384E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator +(float3x2 lhs, float3x2 rhs)
		{
			return new float3x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0003A311 File Offset: 0x00038511
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator +(float3x2 lhs, float rhs)
		{
			return new float3x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0003A330 File Offset: 0x00038530
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator +(float lhs, float3x2 rhs)
		{
			return new float3x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0003A34F File Offset: 0x0003854F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator -(float3x2 lhs, float3x2 rhs)
		{
			return new float3x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0003A378 File Offset: 0x00038578
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator -(float3x2 lhs, float rhs)
		{
			return new float3x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0003A397 File Offset: 0x00038597
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator -(float lhs, float3x2 rhs)
		{
			return new float3x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0003A3B6 File Offset: 0x000385B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator /(float3x2 lhs, float3x2 rhs)
		{
			return new float3x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0003A3DF File Offset: 0x000385DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator /(float3x2 lhs, float rhs)
		{
			return new float3x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0003A3FE File Offset: 0x000385FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator /(float lhs, float3x2 rhs)
		{
			return new float3x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0003A41D File Offset: 0x0003861D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator %(float3x2 lhs, float3x2 rhs)
		{
			return new float3x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0003A446 File Offset: 0x00038646
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator %(float3x2 lhs, float rhs)
		{
			return new float3x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0003A465 File Offset: 0x00038665
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator %(float lhs, float3x2 rhs)
		{
			return new float3x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0003A484 File Offset: 0x00038684
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator ++(float3x2 val)
		{
			float3 @float = float3.op_Increment(val.c0);
			val.c0 = @float;
			float3 float2 = @float;
			@float = float3.op_Increment(val.c1);
			val.c1 = @float;
			return new float3x2(float2, @float);
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0003A4CC File Offset: 0x000386CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator --(float3x2 val)
		{
			float3 @float = float3.op_Decrement(val.c0);
			val.c0 = @float;
			float3 float2 = @float;
			@float = float3.op_Decrement(val.c1);
			val.c1 = @float;
			return new float3x2(float2, @float);
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0003A512 File Offset: 0x00038712
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(float3x2 lhs, float3x2 rhs)
		{
			return new bool3x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0003A53B File Offset: 0x0003873B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(float3x2 lhs, float rhs)
		{
			return new bool3x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0003A55A File Offset: 0x0003875A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(float lhs, float3x2 rhs)
		{
			return new bool3x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0003A579 File Offset: 0x00038779
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(float3x2 lhs, float3x2 rhs)
		{
			return new bool3x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0003A5A2 File Offset: 0x000387A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(float3x2 lhs, float rhs)
		{
			return new bool3x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0003A5C1 File Offset: 0x000387C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(float lhs, float3x2 rhs)
		{
			return new bool3x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0003A5E0 File Offset: 0x000387E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(float3x2 lhs, float3x2 rhs)
		{
			return new bool3x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0003A609 File Offset: 0x00038809
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(float3x2 lhs, float rhs)
		{
			return new bool3x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0003A628 File Offset: 0x00038828
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(float lhs, float3x2 rhs)
		{
			return new bool3x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0003A647 File Offset: 0x00038847
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(float3x2 lhs, float3x2 rhs)
		{
			return new bool3x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0003A670 File Offset: 0x00038870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(float3x2 lhs, float rhs)
		{
			return new bool3x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0003A68F File Offset: 0x0003888F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(float lhs, float3x2 rhs)
		{
			return new bool3x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0003A6AE File Offset: 0x000388AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator -(float3x2 val)
		{
			return new float3x2(-val.c0, -val.c1);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0003A6CB File Offset: 0x000388CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 operator +(float3x2 val)
		{
			return new float3x2(+val.c0, +val.c1);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0003A6E8 File Offset: 0x000388E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(float3x2 lhs, float3x2 rhs)
		{
			return new bool3x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0003A711 File Offset: 0x00038911
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(float3x2 lhs, float rhs)
		{
			return new bool3x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0003A730 File Offset: 0x00038930
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(float lhs, float3x2 rhs)
		{
			return new bool3x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0003A74F File Offset: 0x0003894F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(float3x2 lhs, float3x2 rhs)
		{
			return new bool3x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0003A778 File Offset: 0x00038978
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(float3x2 lhs, float rhs)
		{
			return new bool3x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0003A797 File Offset: 0x00038997
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(float lhs, float3x2 rhs)
		{
			return new bool3x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x17000471 RID: 1137
		public unsafe ref float3 this[int index]
		{
			get
			{
				fixed (float3x2* ptr = &this)
				{
					return ref *(float3*)(ptr + (IntPtr)index * (IntPtr)sizeof(float3) / (IntPtr)sizeof(float3x2));
				}
			}
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x0003A7D3 File Offset: 0x000389D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float3x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0003A7FC File Offset: 0x000389FC
		public override bool Equals(object o)
		{
			if (o is float3x2)
			{
				float3x2 converted = (float3x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0003A821 File Offset: 0x00038A21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0003A830 File Offset: 0x00038A30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float3x2({0}f, {1}f,  {2}f, {3}f,  {4}f, {5}f)", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z
			});
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0003A8C0 File Offset: 0x00038AC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float3x2({0}f, {1}f,  {2}f, {3}f,  {4}f, {5}f)", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000BF RID: 191
		public float3 c0;

		// Token: 0x040000C0 RID: 192
		public float3 c1;

		// Token: 0x040000C1 RID: 193
		public static readonly float3x2 zero;
	}
}
