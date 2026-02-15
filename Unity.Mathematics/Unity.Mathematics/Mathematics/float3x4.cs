using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000034 RID: 52
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float3x4 : IEquatable<float3x4>, IFormattable
	{
		// Token: 0x06001318 RID: 4888 RVA: 0x0003C098 File Offset: 0x0003A298
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(float3 c0, float3 c1, float3 c2, float3 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0003C0B8 File Offset: 0x0003A2B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23)
		{
			this.c0 = new float3(m00, m10, m20);
			this.c1 = new float3(m01, m11, m21);
			this.c2 = new float3(m02, m12, m22);
			this.c3 = new float3(m03, m13, m23);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0003C106 File Offset: 0x0003A306
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(float v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0003C138 File Offset: 0x0003A338
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(bool v)
		{
			this.c0 = math.select(new float3(0f), new float3(1f), v);
			this.c1 = math.select(new float3(0f), new float3(1f), v);
			this.c2 = math.select(new float3(0f), new float3(1f), v);
			this.c3 = math.select(new float3(0f), new float3(1f), v);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0003C1C8 File Offset: 0x0003A3C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(bool3x4 v)
		{
			this.c0 = math.select(new float3(0f), new float3(1f), v.c0);
			this.c1 = math.select(new float3(0f), new float3(1f), v.c1);
			this.c2 = math.select(new float3(0f), new float3(1f), v.c2);
			this.c3 = math.select(new float3(0f), new float3(1f), v.c3);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0003C269 File Offset: 0x0003A469
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(int v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0003C29C File Offset: 0x0003A49C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(int3x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0003C2ED File Offset: 0x0003A4ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0003C320 File Offset: 0x0003A520
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(uint3x4 v)
		{
			this.c0 = v.c0;
			this.c1 = v.c1;
			this.c2 = v.c2;
			this.c3 = v.c3;
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0003C371 File Offset: 0x0003A571
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(double v)
		{
			this.c0 = (float3)v;
			this.c1 = (float3)v;
			this.c2 = (float3)v;
			this.c3 = (float3)v;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0003C3A4 File Offset: 0x0003A5A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3x4(double3x4 v)
		{
			this.c0 = (float3)v.c0;
			this.c1 = (float3)v.c1;
			this.c2 = (float3)v.c2;
			this.c3 = (float3)v.c3;
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0000C7BA File Offset: 0x0000A9BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x4(float v)
		{
			return new float3x4(v);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0000C7C2 File Offset: 0x0000A9C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x4(bool v)
		{
			return new float3x4(v);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0000C7CA File Offset: 0x0000A9CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x4(bool3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0000C7D2 File Offset: 0x0000A9D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x4(int v)
		{
			return new float3x4(v);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0000C7DA File Offset: 0x0000A9DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x4(int3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0000C7E2 File Offset: 0x0000A9E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x4(uint v)
		{
			return new float3x4(v);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0000C7EA File Offset: 0x0000A9EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3x4(uint3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0000C7F2 File Offset: 0x0000A9F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x4(double v)
		{
			return new float3x4(v);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0000C7FA File Offset: 0x0000A9FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3x4(double3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0003C3F8 File Offset: 0x0003A5F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator *(float3x4 lhs, float3x4 rhs)
		{
			return new float3x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0003C44E File Offset: 0x0003A64E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator *(float3x4 lhs, float rhs)
		{
			return new float3x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0003C485 File Offset: 0x0003A685
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator *(float lhs, float3x4 rhs)
		{
			return new float3x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0003C4BC File Offset: 0x0003A6BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator +(float3x4 lhs, float3x4 rhs)
		{
			return new float3x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0003C512 File Offset: 0x0003A712
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator +(float3x4 lhs, float rhs)
		{
			return new float3x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0003C549 File Offset: 0x0003A749
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator +(float lhs, float3x4 rhs)
		{
			return new float3x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0003C580 File Offset: 0x0003A780
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator -(float3x4 lhs, float3x4 rhs)
		{
			return new float3x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0003C5D6 File Offset: 0x0003A7D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator -(float3x4 lhs, float rhs)
		{
			return new float3x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0003C60D File Offset: 0x0003A80D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator -(float lhs, float3x4 rhs)
		{
			return new float3x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0003C644 File Offset: 0x0003A844
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator /(float3x4 lhs, float3x4 rhs)
		{
			return new float3x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0003C69A File Offset: 0x0003A89A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator /(float3x4 lhs, float rhs)
		{
			return new float3x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0003C6D1 File Offset: 0x0003A8D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator /(float lhs, float3x4 rhs)
		{
			return new float3x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0003C708 File Offset: 0x0003A908
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator %(float3x4 lhs, float3x4 rhs)
		{
			return new float3x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0003C75E File Offset: 0x0003A95E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator %(float3x4 lhs, float rhs)
		{
			return new float3x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0003C795 File Offset: 0x0003A995
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator %(float lhs, float3x4 rhs)
		{
			return new float3x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0003C7CC File Offset: 0x0003A9CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator ++(float3x4 val)
		{
			float3 @float = float3.op_Increment(val.c0);
			val.c0 = @float;
			float3 float2 = @float;
			@float = float3.op_Increment(val.c1);
			val.c1 = @float;
			float3 float3 = @float;
			@float = float3.op_Increment(val.c2);
			val.c2 = @float;
			float3 float4 = @float;
			@float = float3.op_Increment(val.c3);
			val.c3 = @float;
			return new float3x4(float2, float3, float4, @float);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0003C848 File Offset: 0x0003AA48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator --(float3x4 val)
		{
			float3 @float = float3.op_Decrement(val.c0);
			val.c0 = @float;
			float3 float2 = @float;
			@float = float3.op_Decrement(val.c1);
			val.c1 = @float;
			float3 float3 = @float;
			@float = float3.op_Decrement(val.c2);
			val.c2 = @float;
			float3 float4 = @float;
			@float = float3.op_Decrement(val.c3);
			val.c3 = @float;
			return new float3x4(float2, float3, float4, @float);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0003C8C4 File Offset: 0x0003AAC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(float3x4 lhs, float3x4 rhs)
		{
			return new bool3x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0003C91A File Offset: 0x0003AB1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(float3x4 lhs, float rhs)
		{
			return new bool3x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0003C951 File Offset: 0x0003AB51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(float lhs, float3x4 rhs)
		{
			return new bool3x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0003C988 File Offset: 0x0003AB88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(float3x4 lhs, float3x4 rhs)
		{
			return new bool3x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0003C9DE File Offset: 0x0003ABDE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(float3x4 lhs, float rhs)
		{
			return new bool3x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0003CA15 File Offset: 0x0003AC15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(float lhs, float3x4 rhs)
		{
			return new bool3x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0003CA4C File Offset: 0x0003AC4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(float3x4 lhs, float3x4 rhs)
		{
			return new bool3x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0003CAA2 File Offset: 0x0003ACA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(float3x4 lhs, float rhs)
		{
			return new bool3x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0003CAD9 File Offset: 0x0003ACD9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(float lhs, float3x4 rhs)
		{
			return new bool3x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0003CB10 File Offset: 0x0003AD10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(float3x4 lhs, float3x4 rhs)
		{
			return new bool3x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0003CB66 File Offset: 0x0003AD66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(float3x4 lhs, float rhs)
		{
			return new bool3x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0003CB9D File Offset: 0x0003AD9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(float lhs, float3x4 rhs)
		{
			return new bool3x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0003CBD4 File Offset: 0x0003ADD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator -(float3x4 val)
		{
			return new float3x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0003CC07 File Offset: 0x0003AE07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 operator +(float3x4 val)
		{
			return new float3x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0003CC3C File Offset: 0x0003AE3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(float3x4 lhs, float3x4 rhs)
		{
			return new bool3x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0003CC92 File Offset: 0x0003AE92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(float3x4 lhs, float rhs)
		{
			return new bool3x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0003CCC9 File Offset: 0x0003AEC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(float lhs, float3x4 rhs)
		{
			return new bool3x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0003CD00 File Offset: 0x0003AF00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(float3x4 lhs, float3x4 rhs)
		{
			return new bool3x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0003CD56 File Offset: 0x0003AF56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(float3x4 lhs, float rhs)
		{
			return new bool3x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0003CD8D File Offset: 0x0003AF8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(float lhs, float3x4 rhs)
		{
			return new bool3x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x17000473 RID: 1139
		public unsafe ref float3 this[int index]
		{
			get
			{
				fixed (float3x4* ptr = &this)
				{
					return ref *(float3*)(ptr + (IntPtr)index * (IntPtr)sizeof(float3) / (IntPtr)sizeof(float3x4));
				}
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0003CDE0 File Offset: 0x0003AFE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float3x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0003CE3C File Offset: 0x0003B03C
		public override bool Equals(object o)
		{
			if (o is float3x4)
			{
				float3x4 converted = (float3x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0003CE61 File Offset: 0x0003B061
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0003CE70 File Offset: 0x0003B070
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float3x4({0}f, {1}f, {2}f, {3}f,  {4}f, {5}f, {6}f, {7}f,  {8}f, {9}f, {10}f, {11}f)", new object[]
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
				this.c3.z
			});
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0003CF78 File Offset: 0x0003B178
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float3x4({0}f, {1}f, {2}f, {3}f,  {4}f, {5}f, {6}f, {7}f,  {8}f, {9}f, {10}f, {11}f)", new object[]
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
				this.c3.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x040000C7 RID: 199
		public float3 c0;

		// Token: 0x040000C8 RID: 200
		public float3 c1;

		// Token: 0x040000C9 RID: 201
		public float3 c2;

		// Token: 0x040000CA RID: 202
		public float3 c3;

		// Token: 0x040000CB RID: 203
		public static readonly float3x4 zero;
	}
}
