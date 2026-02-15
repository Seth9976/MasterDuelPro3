using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000062 RID: 98
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint4x4 : IEquatable<uint4x4>, IFormattable
	{
		// Token: 0x06002464 RID: 9316 RVA: 0x0006606D File Offset: 0x0006426D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(uint4 c0, uint4 c1, uint4 c2, uint4 c3)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
			this.c3 = c3;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x0006608C File Offset: 0x0006428C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13, uint m20, uint m21, uint m22, uint m23, uint m30, uint m31, uint m32, uint m33)
		{
			this.c0 = new uint4(m00, m10, m20, m30);
			this.c1 = new uint4(m01, m11, m21, m31);
			this.c2 = new uint4(m02, m12, m22, m32);
			this.c3 = new uint4(m03, m13, m23, m33);
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000660E2 File Offset: 0x000642E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
			this.c3 = v;
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x00066114 File Offset: 0x00064314
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(bool v)
		{
			this.c0 = math.select(new uint4(0U), new uint4(1U), v);
			this.c1 = math.select(new uint4(0U), new uint4(1U), v);
			this.c2 = math.select(new uint4(0U), new uint4(1U), v);
			this.c3 = math.select(new uint4(0U), new uint4(1U), v);
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00066184 File Offset: 0x00064384
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(bool4x4 v)
		{
			this.c0 = math.select(new uint4(0U), new uint4(1U), v.c0);
			this.c1 = math.select(new uint4(0U), new uint4(1U), v.c1);
			this.c2 = math.select(new uint4(0U), new uint4(1U), v.c2);
			this.c3 = math.select(new uint4(0U), new uint4(1U), v.c3);
		}

		// Token: 0x06002469 RID: 9321 RVA: 0x00066205 File Offset: 0x00064405
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(int v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
			this.c2 = (uint4)v;
			this.c3 = (uint4)v;
		}

		// Token: 0x0600246A RID: 9322 RVA: 0x00066238 File Offset: 0x00064438
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(int4x4 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
			this.c2 = (uint4)v.c2;
			this.c3 = (uint4)v.c3;
		}

		// Token: 0x0600246B RID: 9323 RVA: 0x00066289 File Offset: 0x00064489
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(float v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
			this.c2 = (uint4)v;
			this.c3 = (uint4)v;
		}

		// Token: 0x0600246C RID: 9324 RVA: 0x000662BC File Offset: 0x000644BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(float4x4 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
			this.c2 = (uint4)v.c2;
			this.c3 = (uint4)v.c3;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x0006630D File Offset: 0x0006450D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(double v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
			this.c2 = (uint4)v;
			this.c3 = (uint4)v;
		}

		// Token: 0x0600246E RID: 9326 RVA: 0x00066340 File Offset: 0x00064540
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x4(double4x4 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
			this.c2 = (uint4)v.c2;
			this.c3 = (uint4)v.c3;
		}

		// Token: 0x0600246F RID: 9327 RVA: 0x0002117E File Offset: 0x0001F37E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint4x4(uint v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002470 RID: 9328 RVA: 0x00021186 File Offset: 0x0001F386
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(bool v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002471 RID: 9329 RVA: 0x0002118E File Offset: 0x0001F38E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(bool4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002472 RID: 9330 RVA: 0x00021196 File Offset: 0x0001F396
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(int v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002473 RID: 9331 RVA: 0x0002119E File Offset: 0x0001F39E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(int4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002474 RID: 9332 RVA: 0x000211A6 File Offset: 0x0001F3A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(float v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x000211AE File Offset: 0x0001F3AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(float4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000211B6 File Offset: 0x0001F3B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(double v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x000211BE File Offset: 0x0001F3BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x4(double4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x00066394 File Offset: 0x00064594
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator *(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2, lhs.c3 * rhs.c3);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000663EA File Offset: 0x000645EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator *(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs, lhs.c3 * rhs);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x00066421 File Offset: 0x00064621
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator *(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2, lhs * rhs.c3);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x00066458 File Offset: 0x00064658
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator +(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2, lhs.c3 + rhs.c3);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x000664AE File Offset: 0x000646AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator +(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs, lhs.c3 + rhs);
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000664E5 File Offset: 0x000646E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator +(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2, lhs + rhs.c3);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x0006651C File Offset: 0x0006471C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator -(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2, lhs.c3 - rhs.c3);
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00066572 File Offset: 0x00064772
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator -(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs, lhs.c3 - rhs);
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x000665A9 File Offset: 0x000647A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator -(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2, lhs - rhs.c3);
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000665E0 File Offset: 0x000647E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator /(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2, lhs.c3 / rhs.c3);
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x00066636 File Offset: 0x00064836
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator /(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs, lhs.c3 / rhs);
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x0006666D File Offset: 0x0006486D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator /(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2, lhs / rhs.c3);
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x000666A4 File Offset: 0x000648A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator %(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2, lhs.c3 % rhs.c3);
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x000666FA File Offset: 0x000648FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator %(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs, lhs.c3 % rhs);
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x00066731 File Offset: 0x00064931
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator %(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2, lhs % rhs.c3);
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x00066768 File Offset: 0x00064968
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator ++(uint4x4 val)
		{
			uint4 @uint = uint4.op_Increment(val.c0);
			val.c0 = @uint;
			uint4 uint2 = @uint;
			@uint = uint4.op_Increment(val.c1);
			val.c1 = @uint;
			uint4 uint3 = @uint;
			@uint = uint4.op_Increment(val.c2);
			val.c2 = @uint;
			uint4 uint4 = @uint;
			@uint = uint4.op_Increment(val.c3);
			val.c3 = @uint;
			return new uint4x4(uint2, uint3, uint4, @uint);
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x000667E4 File Offset: 0x000649E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator --(uint4x4 val)
		{
			uint4 @uint = uint4.op_Decrement(val.c0);
			val.c0 = @uint;
			uint4 uint2 = @uint;
			@uint = uint4.op_Decrement(val.c1);
			val.c1 = @uint;
			uint4 uint3 = @uint;
			@uint = uint4.op_Decrement(val.c2);
			val.c2 = @uint;
			uint4 uint4 = @uint;
			@uint = uint4.op_Decrement(val.c3);
			val.c3 = @uint;
			return new uint4x4(uint2, uint3, uint4, @uint);
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x00066860 File Offset: 0x00064A60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(uint4x4 lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2, lhs.c3 < rhs.c3);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x000668B6 File Offset: 0x00064AB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(uint4x4 lhs, uint rhs)
		{
			return new bool4x4(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs, lhs.c3 < rhs);
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x000668ED File Offset: 0x00064AED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <(uint lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2, lhs < rhs.c3);
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x00066924 File Offset: 0x00064B24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(uint4x4 lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2, lhs.c3 <= rhs.c3);
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x0006697A File Offset: 0x00064B7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(uint4x4 lhs, uint rhs)
		{
			return new bool4x4(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs, lhs.c3 <= rhs);
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x000669B1 File Offset: 0x00064BB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator <=(uint lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2, lhs <= rhs.c3);
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x000669E8 File Offset: 0x00064BE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(uint4x4 lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2, lhs.c3 > rhs.c3);
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x00066A3E File Offset: 0x00064C3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(uint4x4 lhs, uint rhs)
		{
			return new bool4x4(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs, lhs.c3 > rhs);
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x00066A75 File Offset: 0x00064C75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >(uint lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2, lhs > rhs.c3);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x00066AAC File Offset: 0x00064CAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(uint4x4 lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2, lhs.c3 >= rhs.c3);
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x00066B02 File Offset: 0x00064D02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(uint4x4 lhs, uint rhs)
		{
			return new bool4x4(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs, lhs.c3 >= rhs);
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x00066B39 File Offset: 0x00064D39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator >=(uint lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2, lhs >= rhs.c3);
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x00066B70 File Offset: 0x00064D70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator -(uint4x4 val)
		{
			return new uint4x4(-val.c0, -val.c1, -val.c2, -val.c3);
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x00066BA3 File Offset: 0x00064DA3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator +(uint4x4 val)
		{
			return new uint4x4(+val.c0, +val.c1, +val.c2, +val.c3);
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x00066BD6 File Offset: 0x00064DD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator <<(uint4x4 x, int n)
		{
			return new uint4x4(x.c0 << n, x.c1 << n, x.c2 << n, x.c3 << n);
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x00066C0D File Offset: 0x00064E0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator >>(uint4x4 x, int n)
		{
			return new uint4x4(x.c0 >> n, x.c1 >> n, x.c2 >> n, x.c3 >> n);
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00066C44 File Offset: 0x00064E44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(uint4x4 lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2, lhs.c3 == rhs.c3);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x00066C9A File Offset: 0x00064E9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(uint4x4 lhs, uint rhs)
		{
			return new bool4x4(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs, lhs.c3 == rhs);
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x00066CD1 File Offset: 0x00064ED1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator ==(uint lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2, lhs == rhs.c3);
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x00066D08 File Offset: 0x00064F08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(uint4x4 lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2, lhs.c3 != rhs.c3);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x00066D5E File Offset: 0x00064F5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(uint4x4 lhs, uint rhs)
		{
			return new bool4x4(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs, lhs.c3 != rhs);
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x00066D95 File Offset: 0x00064F95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 operator !=(uint lhs, uint4x4 rhs)
		{
			return new bool4x4(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2, lhs != rhs.c3);
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x00066DCC File Offset: 0x00064FCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator ~(uint4x4 val)
		{
			return new uint4x4(~val.c0, ~val.c1, ~val.c2, ~val.c3);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x00066E00 File Offset: 0x00065000
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator &(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2, lhs.c3 & rhs.c3);
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00066E56 File Offset: 0x00065056
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator &(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs, lhs.c3 & rhs);
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x00066E8D File Offset: 0x0006508D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator &(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2, lhs & rhs.c3);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x00066EC4 File Offset: 0x000650C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator |(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2, lhs.c3 | rhs.c3);
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x00066F1A File Offset: 0x0006511A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator |(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs, lhs.c3 | rhs);
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x00066F51 File Offset: 0x00065151
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator |(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2, lhs | rhs.c3);
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x00066F88 File Offset: 0x00065188
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator ^(uint4x4 lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2, lhs.c3 ^ rhs.c3);
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x00066FDE File Offset: 0x000651DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator ^(uint4x4 lhs, uint rhs)
		{
			return new uint4x4(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs, lhs.c3 ^ rhs);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x00067015 File Offset: 0x00065215
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 operator ^(uint lhs, uint4x4 rhs)
		{
			return new uint4x4(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2, lhs ^ rhs.c3);
		}

		// Token: 0x17000B89 RID: 2953
		public unsafe ref uint4 this[int index]
		{
			get
			{
				fixed (uint4x4* ptr = &this)
				{
					return ref *(uint4*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint4) / (IntPtr)sizeof(uint4x4));
				}
			}
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x00067068 File Offset: 0x00065268
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint4x4 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2) && this.c3.Equals(rhs.c3);
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000670C4 File Offset: 0x000652C4
		public override bool Equals(object o)
		{
			if (o is uint4x4)
			{
				uint4x4 converted = (uint4x4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000670E9 File Offset: 0x000652E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x000670F8 File Offset: 0x000652F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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

		// Token: 0x060024AE RID: 9390 RVA: 0x00067250 File Offset: 0x00065450
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint4x4({0}, {1}, {2}, {3},  {4}, {5}, {6}, {7},  {8}, {9}, {10}, {11},  {12}, {13}, {14}, {15})", new object[]
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

		// Token: 0x04000175 RID: 373
		public uint4 c0;

		// Token: 0x04000176 RID: 374
		public uint4 c1;

		// Token: 0x04000177 RID: 375
		public uint4 c2;

		// Token: 0x04000178 RID: 376
		public uint4 c3;

		// Token: 0x04000179 RID: 377
		public static readonly uint4x4 identity = new uint4x4(1U, 0U, 0U, 0U, 0U, 1U, 0U, 0U, 0U, 0U, 1U, 0U, 0U, 0U, 0U, 1U);

		// Token: 0x0400017A RID: 378
		public static readonly uint4x4 zero;
	}
}
