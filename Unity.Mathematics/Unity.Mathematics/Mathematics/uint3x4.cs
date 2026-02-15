using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200005D RID: 93
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint3x4 : IEquatable<uint3x4>, IFormattable
	{
		// Token: 0x060021A4 RID: 8612 RVA: 0x0005F76C File Offset: 0x0005D96C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(uint3 c0, uint3 c1, uint3 c2, uint3 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0005F78C File Offset: 0x0005D98C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13, uint m20, uint m21, uint m22, uint m23)
		{
			this.c0 = new uint3(m00, m10, m20);
			this.c1 = new uint3(m01, m11, m21);
			this.c2 = new uint3(m02, m12, m22);
			this.c3 = new uint3(m03, m13, m23);
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0005F7DA File Offset: 0x0005D9DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0005F80C File Offset: 0x0005DA0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(bool v)
		{
			this.c0 = math.select(new uint3(0U), new uint3(1U), v);
			this.c1 = math.select(new uint3(0U), new uint3(1U), v);
			this.c2 = math.select(new uint3(0U), new uint3(1U), v);
			this.c3 = math.select(new uint3(0U), new uint3(1U), v);
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0005F87C File Offset: 0x0005DA7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(bool3x4 v)
		{
			this.c0 = math.select(new uint3(0U), new uint3(1U), v.c0);
			this.c1 = math.select(new uint3(0U), new uint3(1U), v.c1);
			this.c2 = math.select(new uint3(0U), new uint3(1U), v.c2);
			this.c3 = math.select(new uint3(0U), new uint3(1U), v.c3);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x0005F8FD File Offset: 0x0005DAFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(int v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
			this.c2 = (uint3)v;
			this.c3 = (uint3)v;
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x0005F930 File Offset: 0x0005DB30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(int3x4 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
			this.c2 = (uint3)v.c2;
			this.c3 = (uint3)v.c3;
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x0005F981 File Offset: 0x0005DB81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(float v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
			this.c2 = (uint3)v;
			this.c3 = (uint3)v;
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x0005F9B4 File Offset: 0x0005DBB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(float3x4 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
			this.c2 = (uint3)v.c2;
			this.c3 = (uint3)v.c3;
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x0005FA05 File Offset: 0x0005DC05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(double v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
			this.c2 = (uint3)v;
			this.c3 = (uint3)v;
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0005FA38 File Offset: 0x0005DC38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x4(double3x4 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
			this.c2 = (uint3)v.c2;
			this.c3 = (uint3)v.c3;
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x0002096E File Offset: 0x0001EB6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint3x4(uint v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x00020976 File Offset: 0x0001EB76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(bool v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x0002097E File Offset: 0x0001EB7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(bool3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x00020986 File Offset: 0x0001EB86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(int v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x0002098E File Offset: 0x0001EB8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(int3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x00020996 File Offset: 0x0001EB96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(float v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x0002099E File Offset: 0x0001EB9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(float3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x000209A6 File Offset: 0x0001EBA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(double v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x000209AE File Offset: 0x0001EBAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x4(double3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060021B8 RID: 8632 RVA: 0x0005FA8C File Offset: 0x0005DC8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator *(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x0005FAE2 File Offset: 0x0005DCE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator *(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0005FB19 File Offset: 0x0005DD19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator *(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0005FB50 File Offset: 0x0005DD50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator +(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0005FBA6 File Offset: 0x0005DDA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator +(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x0005FBDD File Offset: 0x0005DDDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator +(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x0005FC14 File Offset: 0x0005DE14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator -(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x060021BF RID: 8639 RVA: 0x0005FC6A File Offset: 0x0005DE6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator -(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x0005FCA1 File Offset: 0x0005DEA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator -(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x0005FCD8 File Offset: 0x0005DED8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator /(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x0005FD2E File Offset: 0x0005DF2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator /(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x0005FD65 File Offset: 0x0005DF65
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator /(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x060021C4 RID: 8644 RVA: 0x0005FD9C File Offset: 0x0005DF9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator %(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x060021C5 RID: 8645 RVA: 0x0005FDF2 File Offset: 0x0005DFF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator %(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x060021C6 RID: 8646 RVA: 0x0005FE29 File Offset: 0x0005E029
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator %(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x060021C7 RID: 8647 RVA: 0x0005FE60 File Offset: 0x0005E060
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator ++(uint3x4 val)
		{
			uint3 @uint = uint3.op_Increment(val.c0);
			val.c0 = @uint;
			uint3 uint2 = @uint;
			@uint = uint3.op_Increment(val.c1);
			val.c1 = @uint;
			uint3 uint3 = @uint;
			@uint = uint3.op_Increment(val.c2);
			val.c2 = @uint;
			uint3 uint4 = @uint;
			@uint = uint3.op_Increment(val.c3);
			val.c3 = @uint;
			return new uint3x4(uint2, uint3, uint4, @uint);
		}

		// Token: 0x060021C8 RID: 8648 RVA: 0x0005FEDC File Offset: 0x0005E0DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator --(uint3x4 val)
		{
			uint3 @uint = uint3.op_Decrement(val.c0);
			val.c0 = @uint;
			uint3 uint2 = @uint;
			@uint = uint3.op_Decrement(val.c1);
			val.c1 = @uint;
			uint3 uint3 = @uint;
			@uint = uint3.op_Decrement(val.c2);
			val.c2 = @uint;
			uint3 uint4 = @uint;
			@uint = uint3.op_Decrement(val.c3);
			val.c3 = @uint;
			return new uint3x4(uint2, uint3, uint4, @uint);
		}

		// Token: 0x060021C9 RID: 8649 RVA: 0x0005FF58 File Offset: 0x0005E158
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(uint3x4 lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x060021CA RID: 8650 RVA: 0x0005FFAE File Offset: 0x0005E1AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(uint3x4 lhs, uint rhs)
		{
			return new bool3x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x060021CB RID: 8651 RVA: 0x0005FFE5 File Offset: 0x0005E1E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <(uint lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x060021CC RID: 8652 RVA: 0x0006001C File Offset: 0x0005E21C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(uint3x4 lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x060021CD RID: 8653 RVA: 0x00060072 File Offset: 0x0005E272
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(uint3x4 lhs, uint rhs)
		{
			return new bool3x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x060021CE RID: 8654 RVA: 0x000600A9 File Offset: 0x0005E2A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator <=(uint lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x060021CF RID: 8655 RVA: 0x000600E0 File Offset: 0x0005E2E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(uint3x4 lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x060021D0 RID: 8656 RVA: 0x00060136 File Offset: 0x0005E336
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(uint3x4 lhs, uint rhs)
		{
			return new bool3x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x060021D1 RID: 8657 RVA: 0x0006016D File Offset: 0x0005E36D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >(uint lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x060021D2 RID: 8658 RVA: 0x000601A4 File Offset: 0x0005E3A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(uint3x4 lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x060021D3 RID: 8659 RVA: 0x000601FA File Offset: 0x0005E3FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(uint3x4 lhs, uint rhs)
		{
			return new bool3x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x00060231 File Offset: 0x0005E431
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator >=(uint lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x00060268 File Offset: 0x0005E468
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator -(uint3x4 val)
		{
			return new uint3x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x060021D6 RID: 8662 RVA: 0x0006029B File Offset: 0x0005E49B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator +(uint3x4 val)
		{
			return new uint3x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x000602CE File Offset: 0x0005E4CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator <<(uint3x4 x, int n)
		{
			return new uint3x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x00060305 File Offset: 0x0005E505
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator >>(uint3x4 x, int n)
		{
			return new uint3x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x0006033C File Offset: 0x0005E53C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(uint3x4 lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x00060392 File Offset: 0x0005E592
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(uint3x4 lhs, uint rhs)
		{
			return new bool3x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x000603C9 File Offset: 0x0005E5C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator ==(uint lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x00060400 File Offset: 0x0005E600
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(uint3x4 lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x00060456 File Offset: 0x0005E656
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(uint3x4 lhs, uint rhs)
		{
			return new bool3x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0006048D File Offset: 0x0005E68D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 operator !=(uint lhs, uint3x4 rhs)
		{
			return new bool3x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x000604C4 File Offset: 0x0005E6C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator ~(uint3x4 val)
		{
			return new uint3x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x000604F8 File Offset: 0x0005E6F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator &(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x0006054E File Offset: 0x0005E74E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator &(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x00060585 File Offset: 0x0005E785
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator &(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x000605BC File Offset: 0x0005E7BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator |(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x00060612 File Offset: 0x0005E812
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator |(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x00060649 File Offset: 0x0005E849
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator |(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x060021E6 RID: 8678 RVA: 0x00060680 File Offset: 0x0005E880
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator ^(uint3x4 lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x000606D6 File Offset: 0x0005E8D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator ^(uint3x4 lhs, uint rhs)
		{
			return new uint3x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0006070D File Offset: 0x0005E90D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 operator ^(uint lhs, uint3x4 rhs)
		{
			return new uint3x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x17000A35 RID: 2613
		public unsafe ref uint3 this[int index]
		{
			get
			{
				fixed (uint3x4* ptr = &this)
				{
					return ref *(uint3*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint3) / (IntPtr)sizeof(uint3x4));
				}
			}
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x00060760 File Offset: 0x0005E960
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint3x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x000607BC File Offset: 0x0005E9BC
		public override bool Equals(object o)
		{
			if (o is uint3x4)
			{
				uint3x4 converted = (uint3x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x000607E1 File Offset: 0x0005E9E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x000607F0 File Offset: 0x0005E9F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
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

		// Token: 0x060021EE RID: 8686 RVA: 0x000608F8 File Offset: 0x0005EAF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint3x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11})", new object[]
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

		// Token: 0x04000160 RID: 352
		public uint3 c0;

		// Token: 0x04000161 RID: 353
		public uint3 c1;

		// Token: 0x04000162 RID: 354
		public uint3 c2;

		// Token: 0x04000163 RID: 355
		public uint3 c3;

		// Token: 0x04000164 RID: 356
		public static readonly uint3x4 zero;
	}
}
