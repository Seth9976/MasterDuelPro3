using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200005C RID: 92
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint3x3 : IEquatable<uint3x3>, IFormattable
	{
		// Token: 0x06002158 RID: 8536 RVA: 0x0005E967 File Offset: 0x0005CB67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(uint3 c0, uint3 c1, uint3 c2)
		{
			this.c0 = c0;
			this.c1 = c1;
			this.c2 = c2;
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0005E97E File Offset: 0x0005CB7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12, uint m20, uint m21, uint m22)
		{
			this.c0 = new uint3(m00, m10, m20);
			this.c1 = new uint3(m01, m11, m21);
			this.c2 = new uint3(m02, m12, m22);
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x0005E9B0 File Offset: 0x0005CBB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(uint v)
		{
			this.c0 = v;
			this.c1 = v;
			this.c2 = v;
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x0005E9D8 File Offset: 0x0005CBD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(bool v)
		{
			this.c0 = math.select(new uint3(0U), new uint3(1U), v);
			this.c1 = math.select(new uint3(0U), new uint3(1U), v);
			this.c2 = math.select(new uint3(0U), new uint3(1U), v);
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0005EA30 File Offset: 0x0005CC30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(bool3x3 v)
		{
			this.c0 = math.select(new uint3(0U), new uint3(1U), v.c0);
			this.c1 = math.select(new uint3(0U), new uint3(1U), v.c1);
			this.c2 = math.select(new uint3(0U), new uint3(1U), v.c2);
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0005EA94 File Offset: 0x0005CC94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(int v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
			this.c2 = (uint3)v;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0005EABA File Offset: 0x0005CCBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(int3x3 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
			this.c2 = (uint3)v.c2;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0005EAEF File Offset: 0x0005CCEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(float v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
			this.c2 = (uint3)v;
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0005EB15 File Offset: 0x0005CD15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(float3x3 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
			this.c2 = (uint3)v.c2;
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0005EB4A File Offset: 0x0005CD4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(double v)
		{
			this.c0 = (uint3)v;
			this.c1 = (uint3)v;
			this.c2 = (uint3)v;
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0005EB70 File Offset: 0x0005CD70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3x3(double3x3 v)
		{
			this.c0 = (uint3)v.c0;
			this.c1 = (uint3)v.c1;
			this.c2 = (uint3)v.c2;
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0002077C File Offset: 0x0001E97C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint3x3(uint v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x00020784 File Offset: 0x0001E984
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(bool v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0002078C File Offset: 0x0001E98C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(bool3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00020794 File Offset: 0x0001E994
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(int v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0002079C File Offset: 0x0001E99C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(int3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000207A4 File Offset: 0x0001E9A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(float v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000207AC File Offset: 0x0001E9AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(float3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x000207B4 File Offset: 0x0001E9B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(double v)
		{
			return new uint3x3(v);
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000207BC File Offset: 0x0001E9BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3x3(double3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0005EBA5 File Offset: 0x0005CDA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator *(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 * rhs.c0, lhs.c1 * rhs.c1, lhs.c2 * rhs.c2);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0005EBDF File Offset: 0x0005CDDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator *(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 * rhs, lhs.c1 * rhs, lhs.c2 * rhs);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0005EC0A File Offset: 0x0005CE0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator *(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs * rhs.c0, lhs * rhs.c1, lhs * rhs.c2);
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0005EC35 File Offset: 0x0005CE35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator +(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 + rhs.c0, lhs.c1 + rhs.c1, lhs.c2 + rhs.c2);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0005EC6F File Offset: 0x0005CE6F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator +(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 + rhs, lhs.c1 + rhs, lhs.c2 + rhs);
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0005EC9A File Offset: 0x0005CE9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator +(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs + rhs.c0, lhs + rhs.c1, lhs + rhs.c2);
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0005ECC5 File Offset: 0x0005CEC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator -(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 - rhs.c0, lhs.c1 - rhs.c1, lhs.c2 - rhs.c2);
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0005ECFF File Offset: 0x0005CEFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator -(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 - rhs, lhs.c1 - rhs, lhs.c2 - rhs);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0005ED2A File Offset: 0x0005CF2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator -(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs - rhs.c0, lhs - rhs.c1, lhs - rhs.c2);
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0005ED55 File Offset: 0x0005CF55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator /(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 / rhs.c0, lhs.c1 / rhs.c1, lhs.c2 / rhs.c2);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0005ED8F File Offset: 0x0005CF8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator /(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 / rhs, lhs.c1 / rhs, lhs.c2 / rhs);
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0005EDBA File Offset: 0x0005CFBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator /(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs / rhs.c0, lhs / rhs.c1, lhs / rhs.c2);
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0005EDE5 File Offset: 0x0005CFE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator %(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 % rhs.c0, lhs.c1 % rhs.c1, lhs.c2 % rhs.c2);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0005EE1F File Offset: 0x0005D01F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator %(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 % rhs, lhs.c1 % rhs, lhs.c2 % rhs);
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0005EE4A File Offset: 0x0005D04A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator %(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs % rhs.c0, lhs % rhs.c1, lhs % rhs.c2);
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0005EE78 File Offset: 0x0005D078
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator ++(uint3x3 val)
		{
			uint3 @uint = uint3.op_Increment(val.c0);
			val.c0 = @uint;
			uint3 uint2 = @uint;
			@uint = uint3.op_Increment(val.c1);
			val.c1 = @uint;
			uint3 uint3 = @uint;
			@uint = uint3.op_Increment(val.c2);
			val.c2 = @uint;
			return new uint3x3(uint2, uint3, @uint);
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0005EED8 File Offset: 0x0005D0D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator --(uint3x3 val)
		{
			uint3 @uint = uint3.op_Decrement(val.c0);
			val.c0 = @uint;
			uint3 uint2 = @uint;
			@uint = uint3.op_Decrement(val.c1);
			val.c1 = @uint;
			uint3 uint3 = @uint;
			@uint = uint3.op_Decrement(val.c2);
			val.c2 = @uint;
			return new uint3x3(uint2, uint3, @uint);
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0005EF38 File Offset: 0x0005D138
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(uint3x3 lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs.c0 < rhs.c0, lhs.c1 < rhs.c1, lhs.c2 < rhs.c2);
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0005EF72 File Offset: 0x0005D172
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(uint3x3 lhs, uint rhs)
		{
			return new bool3x3(lhs.c0 < rhs, lhs.c1 < rhs, lhs.c2 < rhs);
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0005EF9D File Offset: 0x0005D19D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <(uint lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs < rhs.c0, lhs < rhs.c1, lhs < rhs.c2);
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0005EFC8 File Offset: 0x0005D1C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(uint3x3 lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs.c0 <= rhs.c0, lhs.c1 <= rhs.c1, lhs.c2 <= rhs.c2);
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x0005F002 File Offset: 0x0005D202
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(uint3x3 lhs, uint rhs)
		{
			return new bool3x3(lhs.c0 <= rhs, lhs.c1 <= rhs, lhs.c2 <= rhs);
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0005F02D File Offset: 0x0005D22D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator <=(uint lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs <= rhs.c0, lhs <= rhs.c1, lhs <= rhs.c2);
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0005F058 File Offset: 0x0005D258
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(uint3x3 lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs.c0 > rhs.c0, lhs.c1 > rhs.c1, lhs.c2 > rhs.c2);
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x0005F092 File Offset: 0x0005D292
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(uint3x3 lhs, uint rhs)
		{
			return new bool3x3(lhs.c0 > rhs, lhs.c1 > rhs, lhs.c2 > rhs);
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0005F0BD File Offset: 0x0005D2BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >(uint lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs > rhs.c0, lhs > rhs.c1, lhs > rhs.c2);
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x0005F0E8 File Offset: 0x0005D2E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(uint3x3 lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs.c0 >= rhs.c0, lhs.c1 >= rhs.c1, lhs.c2 >= rhs.c2);
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x0005F122 File Offset: 0x0005D322
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(uint3x3 lhs, uint rhs)
		{
			return new bool3x3(lhs.c0 >= rhs, lhs.c1 >= rhs, lhs.c2 >= rhs);
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x0005F14D File Offset: 0x0005D34D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator >=(uint lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs >= rhs.c0, lhs >= rhs.c1, lhs >= rhs.c2);
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0005F178 File Offset: 0x0005D378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator -(uint3x3 val)
		{
			return new uint3x3(-val.c0, -val.c1, -val.c2);
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x0005F1A0 File Offset: 0x0005D3A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator +(uint3x3 val)
		{
			return new uint3x3(+val.c0, +val.c1, +val.c2);
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x0005F1C8 File Offset: 0x0005D3C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator <<(uint3x3 x, int n)
		{
			return new uint3x3(x.c0 << n, x.c1 << n, x.c2 << n);
		}

		// Token: 0x0600218C RID: 8588 RVA: 0x0005F1F3 File Offset: 0x0005D3F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator >>(uint3x3 x, int n)
		{
			return new uint3x3(x.c0 >> n, x.c1 >> n, x.c2 >> n);
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x0005F21E File Offset: 0x0005D41E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(uint3x3 lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs.c0 == rhs.c0, lhs.c1 == rhs.c1, lhs.c2 == rhs.c2);
		}

		// Token: 0x0600218E RID: 8590 RVA: 0x0005F258 File Offset: 0x0005D458
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(uint3x3 lhs, uint rhs)
		{
			return new bool3x3(lhs.c0 == rhs, lhs.c1 == rhs, lhs.c2 == rhs);
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x0005F283 File Offset: 0x0005D483
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator ==(uint lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs == rhs.c0, lhs == rhs.c1, lhs == rhs.c2);
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x0005F2AE File Offset: 0x0005D4AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(uint3x3 lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs.c0 != rhs.c0, lhs.c1 != rhs.c1, lhs.c2 != rhs.c2);
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x0005F2E8 File Offset: 0x0005D4E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(uint3x3 lhs, uint rhs)
		{
			return new bool3x3(lhs.c0 != rhs, lhs.c1 != rhs, lhs.c2 != rhs);
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x0005F313 File Offset: 0x0005D513
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 operator !=(uint lhs, uint3x3 rhs)
		{
			return new bool3x3(lhs != rhs.c0, lhs != rhs.c1, lhs != rhs.c2);
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x0005F33E File Offset: 0x0005D53E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator ~(uint3x3 val)
		{
			return new uint3x3(~val.c0, ~val.c1, ~val.c2);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x0005F366 File Offset: 0x0005D566
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator &(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 & rhs.c0, lhs.c1 & rhs.c1, lhs.c2 & rhs.c2);
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x0005F3A0 File Offset: 0x0005D5A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator &(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 & rhs, lhs.c1 & rhs, lhs.c2 & rhs);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0005F3CB File Offset: 0x0005D5CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator &(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs & rhs.c0, lhs & rhs.c1, lhs & rhs.c2);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0005F3F6 File Offset: 0x0005D5F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator |(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 | rhs.c0, lhs.c1 | rhs.c1, lhs.c2 | rhs.c2);
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x0005F430 File Offset: 0x0005D630
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator |(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 | rhs, lhs.c1 | rhs, lhs.c2 | rhs);
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x0005F45B File Offset: 0x0005D65B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator |(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs | rhs.c0, lhs | rhs.c1, lhs | rhs.c2);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x0005F486 File Offset: 0x0005D686
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator ^(uint3x3 lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs.c0 ^ rhs.c0, lhs.c1 ^ rhs.c1, lhs.c2 ^ rhs.c2);
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x0005F4C0 File Offset: 0x0005D6C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator ^(uint3x3 lhs, uint rhs)
		{
			return new uint3x3(lhs.c0 ^ rhs, lhs.c1 ^ rhs, lhs.c2 ^ rhs);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x0005F4EB File Offset: 0x0005D6EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 operator ^(uint lhs, uint3x3 rhs)
		{
			return new uint3x3(lhs ^ rhs.c0, lhs ^ rhs.c1, lhs ^ rhs.c2);
		}

		// Token: 0x17000A34 RID: 2612
		public unsafe ref uint3 this[int index]
		{
			get
			{
				fixed (uint3x3* ptr = &this)
				{
					return ref *(uint3*)(ptr + (IntPtr)index * (IntPtr)sizeof(uint3) / (IntPtr)sizeof(uint3x3));
				}
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0005F533 File Offset: 0x0005D733
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint3x3 rhs)
		{
			return this.c0.Equals(rhs.c0) && this.c1.Equals(rhs.c1) && this.c2.Equals(rhs.c2);
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0005F570 File Offset: 0x0005D770
		public override bool Equals(object o)
		{
			if (o is uint3x3)
			{
				uint3x3 converted = (uint3x3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0005F595 File Offset: 0x0005D795
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x0005F5A4 File Offset: 0x0005D7A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint3x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8})", new object[]
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

		// Token: 0x060021A2 RID: 8610 RVA: 0x0005F670 File Offset: 0x0005D870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint3x3({0}, {1}, {2},  {3}, {4}, {5},  {6}, {7}, {8})", new object[]
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

		// Token: 0x0400015B RID: 347
		public uint3 c0;

		// Token: 0x0400015C RID: 348
		public uint3 c1;

		// Token: 0x0400015D RID: 349
		public uint3 c2;

		// Token: 0x0400015E RID: 350
		public static readonly uint3x3 identity = new uint3x3(1U, 0U, 0U, 0U, 1U, 0U, 0U, 0U, 1U);

		// Token: 0x0400015F RID: 351
		public static readonly uint3x3 zero;
	}
}
