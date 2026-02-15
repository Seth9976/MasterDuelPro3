using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000061 RID: 97
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint4x3 : IEquatable<uint4x3>, IFormattable
	{
		// Token: 0x06002419 RID: 9241 RVA: 0x00065205 File Offset: 0x00063405
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(uint4 c0, uint4 c1, uint4 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x0600241A RID: 9242 RVA: 0x0006521C File Offset: 0x0006341C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12, uint m20, uint m21, uint m22, uint m30, uint m31, uint m32)
		{
			this.c0 = new uint4(m00, m10, m20, m30);
			this.c1 = new uint4(m01, m11, m21, m31);
			this.c2 = new uint4(m02, m12, m22, m32);
		}

		// Token: 0x0600241B RID: 9243 RVA: 0x00065254 File Offset: 0x00063454
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x0006527C File Offset: 0x0006347C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(bool v)
		{
			this.c0 = math.select(new uint4(0U), new uint4(1U), v);
			this.c1 = math.select(new uint4(0U), new uint4(1U), v);
			this.c2 = math.select(new uint4(0U), new uint4(1U), v);
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000652D4 File Offset: 0x000634D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(bool4x3 v)
		{
			this.c0 = math.select(new uint4(0U), new uint4(1U), v.c0);
			this.c1 = math.select(new uint4(0U), new uint4(1U), v.c1);
			this.c2 = math.select(new uint4(0U), new uint4(1U), v.c2);
		}

		// Token: 0x0600241E RID: 9246 RVA: 0x00065338 File Offset: 0x00063538
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(int v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
			this.c2 = (uint4)v;
		}

		// Token: 0x0600241F RID: 9247 RVA: 0x0006535E File Offset: 0x0006355E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(int4x3 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
			this.c2 = (uint4)v.c2;
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x00065393 File Offset: 0x00063593
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(float v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
			this.c2 = (uint4)v;
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000653B9 File Offset: 0x000635B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(float4x3 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
			this.c2 = (uint4)v.c2;
		}

		// Token: 0x06002422 RID: 9250 RVA: 0x000653EE File Offset: 0x000635EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(double v)
		{
			this.c0 = (uint4)v;
			this.c1 = (uint4)v;
			this.c2 = (uint4)v;
		}

		// Token: 0x06002423 RID: 9251 RVA: 0x00065414 File Offset: 0x00063614
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4x3(double4x3 v)
		{
			this.c0 = (uint4)v.c0;
			this.c1 = (uint4)v.c1;
			this.c2 = (uint4)v.c2;
		}

		// Token: 0x06002424 RID: 9252 RVA: 0x00020F46 File Offset: 0x0001F146
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint4x3(uint v)
		{
			return new uint4x3(v);
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x00020F4E File Offset: 0x0001F14E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(bool v)
		{
			return new uint4x3(v);
		}

		// Token: 0x06002426 RID: 9254 RVA: 0x00020F56 File Offset: 0x0001F156
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(bool4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x00020F5E File Offset: 0x0001F15E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(int v)
		{
			return new uint4x3(v);
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x00020F66 File Offset: 0x0001F166
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(int4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x00020F6E File Offset: 0x0001F16E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(float v)
		{
			return new uint4x3(v);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00020F76 File Offset: 0x0001F176
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(float4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x00020F7E File Offset: 0x0001F17E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(double v)
		{
			return new uint4x3(v);
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x00020F86 File Offset: 0x0001F186
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4x3(double4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x00065449 File Offset: 0x00063649
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator *(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x00065483 File Offset: 0x00063683
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator *(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x000654AE File Offset: 0x000636AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator *(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x000654D9 File Offset: 0x000636D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator +(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x00065513 File Offset: 0x00063713
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator +(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x0006553E File Offset: 0x0006373E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator +(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00065569 File Offset: 0x00063769
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator -(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000655A3 File Offset: 0x000637A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator -(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000655CE File Offset: 0x000637CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator -(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x000655F9 File Offset: 0x000637F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator /(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x00065633 File Offset: 0x00063833
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator /(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x0006565E File Offset: 0x0006385E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator /(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x00065689 File Offset: 0x00063889
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator %(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000656C3 File Offset: 0x000638C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator %(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000656EE File Offset: 0x000638EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator %(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x0006571C File Offset: 0x0006391C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator ++(uint4x3 val)
		{
			uint4 @uint = uint4.op_Increment(val.c0);
			val.c0 = @uint;
			uint4 uint2 = @uint;
			@uint = uint4.op_Increment(val.c1);
			val.c1 = @uint;
			uint4 uint3 = @uint;
			@uint = uint4.op_Increment(val.c2);
			val.c2 = @uint;
			return new uint4x3(uint2, uint3, @uint);
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x0006577C File Offset: 0x0006397C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator --(uint4x3 val)
		{
			uint4 @uint = uint4.op_Decrement(val.c0);
			val.c0 = @uint;
			uint4 uint2 = @uint;
			@uint = uint4.op_Decrement(val.c1);
			val.c1 = @uint;
			uint4 uint3 = @uint;
			@uint = uint4.op_Decrement(val.c2);
			val.c2 = @uint;
			return new uint4x3(uint2, uint3, @uint);
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000657DC File Offset: 0x000639DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(uint4x3 lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x00065816 File Offset: 0x00063A16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(uint4x3 lhs, uint rhs)
		{
			return new bool4x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x00065841 File Offset: 0x00063A41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <(uint lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x0006586C File Offset: 0x00063A6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(uint4x3 lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x000658A6 File Offset: 0x00063AA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(uint4x3 lhs, uint rhs)
		{
			return new bool4x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06002443 RID: 9283 RVA: 0x000658D1 File Offset: 0x00063AD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator <=(uint lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06002444 RID: 9284 RVA: 0x000658FC File Offset: 0x00063AFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(uint4x3 lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06002445 RID: 9285 RVA: 0x00065936 File Offset: 0x00063B36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(uint4x3 lhs, uint rhs)
		{
			return new bool4x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06002446 RID: 9286 RVA: 0x00065961 File Offset: 0x00063B61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >(uint lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06002447 RID: 9287 RVA: 0x0006598C File Offset: 0x00063B8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(uint4x3 lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06002448 RID: 9288 RVA: 0x000659C6 File Offset: 0x00063BC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(uint4x3 lhs, uint rhs)
		{
			return new bool4x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06002449 RID: 9289 RVA: 0x000659F1 File Offset: 0x00063BF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator >=(uint lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x0600244A RID: 9290 RVA: 0x00065A1C File Offset: 0x00063C1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator -(uint4x3 val)
		{
			return new uint4x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x0600244B RID: 9291 RVA: 0x00065A44 File Offset: 0x00063C44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator +(uint4x3 val)
		{
			return new uint4x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x0600244C RID: 9292 RVA: 0x00065A6C File Offset: 0x00063C6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator <<(uint4x3 x, int n)
		{
			return new uint4x3(x.c0 << n, x.c1 << n, x.c2 << n);
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x00065A97 File Offset: 0x00063C97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator >>(uint4x3 x, int n)
		{
			return new uint4x3(x.c0 >> n, x.c1 >> n, x.c2 >> n);
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x00065AC2 File Offset: 0x00063CC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(uint4x3 lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00065AFC File Offset: 0x00063CFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(uint4x3 lhs, uint rhs)
		{
			return new bool4x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x00065B27 File Offset: 0x00063D27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator ==(uint lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x00065B52 File Offset: 0x00063D52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(uint4x3 lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x00065B8C File Offset: 0x00063D8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(uint4x3 lhs, uint rhs)
		{
			return new bool4x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x00065BB7 File Offset: 0x00063DB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 operator !=(uint lhs, uint4x3 rhs)
		{
			return new bool4x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x00065BE2 File Offset: 0x00063DE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator ~(uint4x3 val)
		{
			return new uint4x3(~val.c0, ~val.c1, ~val.c2);
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x00065C0A File Offset: 0x00063E0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator &(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x00065C44 File Offset: 0x00063E44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator &(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x00065C6F File Offset: 0x00063E6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator &(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x00065C9A File Offset: 0x00063E9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator |(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x00065CD4 File Offset: 0x00063ED4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator |(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x0600245A RID: 9306 RVA: 0x00065CFF File Offset: 0x00063EFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator |(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x0600245B RID: 9307 RVA: 0x00065D2A File Offset: 0x00063F2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator ^(uint4x3 lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x0600245C RID: 9308 RVA: 0x00065D64 File Offset: 0x00063F64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator ^(uint4x3 lhs, uint rhs)
		{
			return new uint4x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x0600245D RID: 9309 RVA: 0x00065D8F File Offset: 0x00063F8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 operator ^(uint lhs, uint4x3 rhs)
		{
			return new uint4x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x17000B88 RID: 2952
		public unsafe ref uint4 this[int index]
		{
			get
			{
				fixed (uint4x3* ptr = &this)
				{
					return ref *(uint4*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint4) / (IntPtr)sizeof(uint4x3));
				}
			}
		}

		// Token: 0x0600245F RID: 9311 RVA: 0x00065DD7 File Offset: 0x00063FD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint4x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x06002460 RID: 9312 RVA: 0x00065E14 File Offset: 0x00064014
		public override bool Equals(object o)
		{
			if (o is uint4x3)
			{
				uint4x3 converted = (uint4x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06002461 RID: 9313 RVA: 0x00065E39 File Offset: 0x00064039
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06002462 RID: 9314 RVA: 0x00065E48 File Offset: 0x00064048
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
			{
				this.c0.x,
				this.c1.x,
				this.c2.x,
				this.c0.y,
				this.c1.y,
				this.c2.y,
				this.c0.z,
				this.c1.z,
				this.c2.z,
				this.c0.w,
				this.c1.w,
				this.c2.w
			});
		}

		// Token: 0x06002463 RID: 9315 RVA: 0x00065F50 File Offset: 0x00064150
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint4x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8},  {9}, {10}, {11})", new object[]
			{
				this.c0.x.ToString(format, formatProvider),
				this.c1.x.ToString(format, formatProvider),
				this.c2.x.ToString(format, formatProvider),
				this.c0.y.ToString(format, formatProvider),
				this.c1.y.ToString(format, formatProvider),
				this.c2.y.ToString(format, formatProvider),
				this.c0.z.ToString(format, formatProvider),
				this.c1.z.ToString(format, formatProvider),
				this.c2.z.ToString(format, formatProvider),
				this.c0.w.ToString(format, formatProvider),
				this.c1.w.ToString(format, formatProvider),
				this.c2.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000171 RID: 369
		public uint4 c0;

		// Token: 0x04000172 RID: 370
		public uint4 c1;

		// Token: 0x04000173 RID: 371
		public uint4 c2;

		// Token: 0x04000174 RID: 372
		public static readonly uint4x3 zero;
	}
}
