using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200005B RID: 91
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint3x2 : IEquatable<uint3x2>, IFormattable
	{
		// Token: 0x0600210D RID: 8461 RVA: 0x0005DF8A File Offset: 0x0005C18A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(uint3 c0, uint3 c1)
		{
			this.c0 = c0;
			this.c1 = c1;
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x0005DF9A File Offset: 0x0005C19A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(uint m00, uint m01, uint m10, uint m11, uint m20, uint m21)
		{
			this.c0 = new uint3(m00, m10, m20);
			this.c1 = new uint3(m01, m11, m21);
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x0005DFBB File Offset: 0x0005C1BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(uint v)
		{
			this.c0 = v;
			this.c1 = v;
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x0005DFD5 File Offset: 0x0005C1D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(bool v)
		{
			this.c0 = math.select(new uint3(0U), new uint3(1U), v);
			this.c1 = math.select(new uint3(0U), new uint3(1U), v);
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x0005E007 File Offset: 0x0005C207
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(bool3x2 v)
		{
			this.c0 = math.select(new uint3(0U), new uint3(1U), v.c0);
			this.c1 = math.select(new uint3(0U), new uint3(1U), v.c1);
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x0005E043 File Offset: 0x0005C243
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(int v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0005E05D File Offset: 0x0005C25D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(int3x2 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0005E081 File Offset: 0x0005C281
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(float v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0005E09B File Offset: 0x0005C29B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(float3x2 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0005E0BF File Offset: 0x0005C2BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(double v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0005E0D9 File Offset: 0x0005C2D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x2(double3x2 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x000205FF File Offset: 0x0001E7FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint3x2(uint v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x00020607 File Offset: 0x0001E807
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(bool v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0002060F File Offset: 0x0001E80F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(bool3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x00020617 File Offset: 0x0001E817
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(int v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0002061F File Offset: 0x0001E81F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(int3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x00020627 File Offset: 0x0001E827
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(float v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0002062F File Offset: 0x0001E82F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(float3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x00020637 File Offset: 0x0001E837
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(double v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0002063F File Offset: 0x0001E83F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x2(double3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0005E0FD File Offset: 0x0005C2FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator *(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1);
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0005E126 File Offset: 0x0005C326
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator *(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 * rhs, lhs.c1 * rhs);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0005E145 File Offset: 0x0005C345
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator *(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs * rhs.c0, lhs * rhs.c1);
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x0005E164 File Offset: 0x0005C364
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator +(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1);
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0005E18D File Offset: 0x0005C38D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator +(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 + rhs, lhs.c1 + rhs);
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0005E1AC File Offset: 0x0005C3AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator +(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs + rhs.c0, lhs + rhs.c1);
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0005E1CB File Offset: 0x0005C3CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator -(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0005E1F4 File Offset: 0x0005C3F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator -(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 - rhs, lhs.c1 - rhs);
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0005E213 File Offset: 0x0005C413
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator -(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs - rhs.c0, lhs - rhs.c1);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x0005E232 File Offset: 0x0005C432
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator /(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1);
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0005E25B File Offset: 0x0005C45B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator /(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 / rhs, lhs.c1 / rhs);
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x0005E27A File Offset: 0x0005C47A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator /(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs / rhs.c0, lhs / rhs.c1);
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0005E299 File Offset: 0x0005C499
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator %(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1);
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0005E2C2 File Offset: 0x0005C4C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator %(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 % rhs, lhs.c1 % rhs);
		}

		// Token: 0x0600212F RID: 8495 RVA: 0x0005E2E1 File Offset: 0x0005C4E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator %(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs % rhs.c0, lhs % rhs.c1);
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x0005E300 File Offset: 0x0005C500
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator ++(uint3x2 val)
		{
			uint3 @uint = uint3.op_Increment(val.c0);
			val.c0 = @uint;
			uint3 uint2 = @uint;
			@uint = uint3.op_Increment(val.c1);
			val.c1 = @uint;
			return new uint3x2(uint2, @uint);
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0005E348 File Offset: 0x0005C548
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator --(uint3x2 val)
		{
			uint3 @uint = uint3.op_Decrement(val.c0);
			val.c0 = @uint;
			uint3 uint2 = @uint;
			@uint = uint3.op_Decrement(val.c1);
			val.c1 = @uint;
			return new uint3x2(uint2, @uint);
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x0005E38E File Offset: 0x0005C58E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(uint3x2 lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1);
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x0005E3B7 File Offset: 0x0005C5B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(uint3x2 lhs, uint rhs)
		{
			return new bool3x2(lhs.c0 < rhs, lhs.c1 < rhs);
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x0005E3D6 File Offset: 0x0005C5D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <(uint lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs < rhs.c0, lhs < rhs.c1);
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x0005E3F5 File Offset: 0x0005C5F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(uint3x2 lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1);
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0005E41E File Offset: 0x0005C61E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(uint3x2 lhs, uint rhs)
		{
			return new bool3x2(lhs.c0 <= rhs, lhs.c1 <= rhs);
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x0005E43D File Offset: 0x0005C63D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator <=(uint lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs <= rhs.c0, lhs <= rhs.c1);
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x0005E45C File Offset: 0x0005C65C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(uint3x2 lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1);
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x0005E485 File Offset: 0x0005C685
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(uint3x2 lhs, uint rhs)
		{
			return new bool3x2(lhs.c0 > rhs, lhs.c1 > rhs);
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x0005E4A4 File Offset: 0x0005C6A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >(uint lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs > rhs.c0, lhs > rhs.c1);
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x0005E4C3 File Offset: 0x0005C6C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(uint3x2 lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1);
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x0005E4EC File Offset: 0x0005C6EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(uint3x2 lhs, uint rhs)
		{
			return new bool3x2(lhs.c0 >= rhs, lhs.c1 >= rhs);
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x0005E50B File Offset: 0x0005C70B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator >=(uint lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs >= rhs.c0, lhs >= rhs.c1);
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x0005E52A File Offset: 0x0005C72A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator -(uint3x2 val)
		{
			return new uint3x2(-val.c0, -val.c1);
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x0005E547 File Offset: 0x0005C747
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator +(uint3x2 val)
		{
			return new uint3x2(+val.c0, +val.c1);
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x0005E564 File Offset: 0x0005C764
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator <<(uint3x2 x, int n)
		{
			return new uint3x2(x.c0 << n, x.c1 << n);
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0005E583 File Offset: 0x0005C783
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator >>(uint3x2 x, int n)
		{
			return new uint3x2(x.c0 >> n, x.c1 >> n);
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x0005E5A2 File Offset: 0x0005C7A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(uint3x2 lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1);
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x0005E5CB File Offset: 0x0005C7CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(uint3x2 lhs, uint rhs)
		{
			return new bool3x2(lhs.c0 == rhs, lhs.c1 == rhs);
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0005E5EA File Offset: 0x0005C7EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator ==(uint lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs == rhs.c0, lhs == rhs.c1);
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x0005E609 File Offset: 0x0005C809
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(uint3x2 lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1);
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0005E632 File Offset: 0x0005C832
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(uint3x2 lhs, uint rhs)
		{
			return new bool3x2(lhs.c0 != rhs, lhs.c1 != rhs);
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x0005E651 File Offset: 0x0005C851
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 operator !=(uint lhs, uint3x2 rhs)
		{
			return new bool3x2(lhs != rhs.c0, lhs != rhs.c1);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0005E670 File Offset: 0x0005C870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator ~(uint3x2 val)
		{
			return new uint3x2(~val.c0, ~val.c1);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0005E68D File Offset: 0x0005C88D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator &(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1);
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0005E6B6 File Offset: 0x0005C8B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator &(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 & rhs, lhs.c1 & rhs);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0005E6D5 File Offset: 0x0005C8D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator &(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs & rhs.c0, lhs & rhs.c1);
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x0005E6F4 File Offset: 0x0005C8F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator |(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1);
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x0005E71D File Offset: 0x0005C91D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator |(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 | rhs, lhs.c1 | rhs);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x0005E73C File Offset: 0x0005C93C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator |(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs | rhs.c0, lhs | rhs.c1);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0005E75B File Offset: 0x0005C95B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator ^(uint3x2 lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0005E784 File Offset: 0x0005C984
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator ^(uint3x2 lhs, uint rhs)
		{
			return new uint3x2(lhs.c0 ^ rhs, lhs.c1 ^ rhs);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0005E7A3 File Offset: 0x0005C9A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 operator ^(uint lhs, uint3x2 rhs)
		{
			return new uint3x2(lhs ^ rhs.c0, lhs ^ rhs.c1);
		}

		// Token: 0x17000A33 RID: 2611
		public unsafe ref uint3 this[int index]
		{
			get
			{
				fixed (uint3x2* ptr = &this)
				{
					return ref *(uint3*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint3) / (IntPtr)sizeof(uint3x2));
				}
			}
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0005E7DF File Offset: 0x0005C9DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint3x2 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1);
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0005E808 File Offset: 0x0005CA08
		public override bool Equals(object o)
		{
			if (o is uint3x2)
			{
				uint3x2 converted = (uint3x2)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0005E82D File Offset: 0x0005CA2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0005E83C File Offset: 0x0005CA3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c0.y,
				this.c1.y,
				this.c0.z,
				this.c1.z
			});
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0005E8CC File Offset: 0x0005CACC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint3x2({0}, {1},  {2}, {3},  {4}, {5})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000158 RID: 344
		public uint3 c0;

		// Token: 0x04000159 RID: 345
		public uint3 c1;

		// Token: 0x0400015A RID: 346
		public static readonly uint3x2 zero;
	}
}
