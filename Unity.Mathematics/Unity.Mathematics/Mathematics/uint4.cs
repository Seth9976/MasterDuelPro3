using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x0200005E RID: 94
	[DebuggerTypeProxy(typeof(uint4.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint4 : IEquatable<uint4>, IFormattable
	{
		// Token: 0x060021EF RID: 8687 RVA: 0x00060A15 File Offset: 0x0005EC15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint x, uint y, uint z, uint w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x00060A34 File Offset: 0x0005EC34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint x, uint y, uint2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x060021F1 RID: 8689 RVA: 0x00060A5C File Offset: 0x0005EC5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint x, uint2 yz, uint w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}

		// Token: 0x060021F2 RID: 8690 RVA: 0x00060A84 File Offset: 0x0005EC84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint x, uint3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x00060AB1 File Offset: 0x0005ECB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint2 xy, uint z, uint w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x00060AD9 File Offset: 0x0005ECD9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint2 xy, uint2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00060B0B File Offset: 0x0005ED0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint3 xyz, uint w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x00060B38 File Offset: 0x0005ED38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.y;
			this.z = xyzw.z;
			this.w = xyzw.w;
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00060B6A File Offset: 0x0005ED6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(uint v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00060B88 File Offset: 0x0005ED88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(bool v)
		{
			this.x = (v ? 1U : 0U);
			this.y = (v ? 1U : 0U);
			this.z = (v ? 1U : 0U);
			this.w = (v ? 1U : 0U);
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00060BC0 File Offset: 0x0005EDC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(bool4 v)
		{
			this.x = (v.x ? 1U : 0U);
			this.y = (v.y ? 1U : 0U);
			this.z = (v.z ? 1U : 0U);
			this.w = (v.w ? 1U : 0U);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x00060B6A File Offset: 0x0005ED6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(int v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
			this.z = (uint)v;
			this.w = (uint)v;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00060C15 File Offset: 0x0005EE15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(int4 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
			this.z = (uint)v.z;
			this.w = (uint)v.w;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00060C47 File Offset: 0x0005EE47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(float v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
			this.z = (uint)v;
			this.w = (uint)v;
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00060C69 File Offset: 0x0005EE69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(float4 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
			this.z = (uint)v.z;
			this.w = (uint)v.w;
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00060C47 File Offset: 0x0005EE47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(double v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
			this.z = (uint)v;
			this.w = (uint)v;
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x00060C9F File Offset: 0x0005EE9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4(double4 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
			this.z = (uint)v.z;
			this.w = (uint)v.w;
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x00020BE2 File Offset: 0x0001EDE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint4(uint v)
		{
			return new uint4(v);
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x00020BEA File Offset: 0x0001EDEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(bool v)
		{
			return new uint4(v);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x00020BF2 File Offset: 0x0001EDF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(bool4 v)
		{
			return new uint4(v);
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x00020BFA File Offset: 0x0001EDFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(int v)
		{
			return new uint4(v);
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x00020C02 File Offset: 0x0001EE02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(int4 v)
		{
			return new uint4(v);
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x00020C0A File Offset: 0x0001EE0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(float v)
		{
			return new uint4(v);
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x00020C12 File Offset: 0x0001EE12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(float4 v)
		{
			return new uint4(v);
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x00020C1A File Offset: 0x0001EE1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(double v)
		{
			return new uint4(v);
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00020C22 File Offset: 0x0001EE22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint4(double4 v)
		{
			return new uint4(v);
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00060CD5 File Offset: 0x0005EED5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator *(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w);
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x00060D10 File Offset: 0x0005EF10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator *(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x00060D37 File Offset: 0x0005EF37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator *(uint lhs, uint4 rhs)
		{
			return new uint4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w);
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x00060D5E File Offset: 0x0005EF5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator +(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00060D99 File Offset: 0x0005EF99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator +(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs);
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x00060DC0 File Offset: 0x0005EFC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator +(uint lhs, uint4 rhs)
		{
			return new uint4(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x00060DE7 File Offset: 0x0005EFE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator -(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x00060E22 File Offset: 0x0005F022
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator -(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs);
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x00060E49 File Offset: 0x0005F049
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator -(uint lhs, uint4 rhs)
		{
			return new uint4(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w);
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x00060E70 File Offset: 0x0005F070
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator /(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w);
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x00060EAB File Offset: 0x0005F0AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator /(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00060ED2 File Offset: 0x0005F0D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator /(uint lhs, uint4 rhs)
		{
			return new uint4(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w);
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x00060EF9 File Offset: 0x0005F0F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator %(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z, lhs.w % rhs.w);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x00060F34 File Offset: 0x0005F134
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator %(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs, lhs.w % rhs);
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x00060F5B File Offset: 0x0005F15B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator %(uint lhs, uint4 rhs)
		{
			return new uint4(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z, lhs % rhs.w);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00060F84 File Offset: 0x0005F184
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator ++(uint4 val)
		{
			uint num = val.x + 1U;
			val.x = num;
			uint num2 = num;
			num = val.y + 1U;
			val.y = num;
			uint num3 = num;
			num = val.z + 1U;
			val.z = num;
			uint num4 = num;
			num = val.w + 1U;
			val.w = num;
			return new uint4(num2, num3, num4, num);
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00060FD4 File Offset: 0x0005F1D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator --(uint4 val)
		{
			uint num = val.x - 1U;
			val.x = num;
			uint num2 = num;
			num = val.y - 1U;
			val.y = num;
			uint num3 = num;
			num = val.z - 1U;
			val.z = num;
			uint num4 = num;
			num = val.w - 1U;
			val.w = num;
			return new uint4(num2, num3, num4, num);
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x00061022 File Offset: 0x0005F222
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(uint4 lhs, uint4 rhs)
		{
			return new bool4(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z, lhs.w < rhs.w);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x00061061 File Offset: 0x0005F261
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(uint4 lhs, uint rhs)
		{
			return new bool4(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs, lhs.w < rhs);
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x0006108C File Offset: 0x0005F28C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(uint lhs, uint4 rhs)
		{
			return new bool4(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z, lhs < rhs.w);
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x000610B8 File Offset: 0x0005F2B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(uint4 lhs, uint4 rhs)
		{
			return new bool4(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z, lhs.w <= rhs.w);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x0006110E File Offset: 0x0005F30E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(uint4 lhs, uint rhs)
		{
			return new bool4(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs, lhs.w <= rhs);
		}

		// Token: 0x0600221F RID: 8735 RVA: 0x00061145 File Offset: 0x0005F345
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(uint lhs, uint4 rhs)
		{
			return new bool4(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z, lhs <= rhs.w);
		}

		// Token: 0x06002220 RID: 8736 RVA: 0x0006117C File Offset: 0x0005F37C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(uint4 lhs, uint4 rhs)
		{
			return new bool4(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z, lhs.w > rhs.w);
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000611BB File Offset: 0x0005F3BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(uint4 lhs, uint rhs)
		{
			return new bool4(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs, lhs.w > rhs);
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000611E6 File Offset: 0x0005F3E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(uint lhs, uint4 rhs)
		{
			return new bool4(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z, lhs > rhs.w);
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x00061214 File Offset: 0x0005F414
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(uint4 lhs, uint4 rhs)
		{
			return new bool4(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z, lhs.w >= rhs.w);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x0006126A File Offset: 0x0005F46A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(uint4 lhs, uint rhs)
		{
			return new bool4(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs, lhs.w >= rhs);
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x000612A1 File Offset: 0x0005F4A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(uint lhs, uint4 rhs)
		{
			return new bool4(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z, lhs >= rhs.w);
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x000612D8 File Offset: 0x0005F4D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator -(uint4 val)
		{
			return new uint4((uint)(-(uint)((ulong)val.x)), (uint)(-(uint)((ulong)val.y)), (uint)(-(uint)((ulong)val.z)), (uint)(-(uint)((ulong)val.w)));
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00061303 File Offset: 0x0005F503
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator +(uint4 val)
		{
			return new uint4(val.x, val.y, val.z, val.w);
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x00061322 File Offset: 0x0005F522
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator <<(uint4 x, int n)
		{
			return new uint4(x.x << n, x.y << n, x.z << n, x.w << n);
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x00061355 File Offset: 0x0005F555
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator >>(uint4 x, int n)
		{
			return new uint4(x.x >> n, x.y >> n, x.z >> n, x.w >> n);
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x00061388 File Offset: 0x0005F588
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(uint4 lhs, uint4 rhs)
		{
			return new bool4(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w);
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x000613C7 File Offset: 0x0005F5C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(uint4 lhs, uint rhs)
		{
			return new bool4(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs);
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x000613F2 File Offset: 0x0005F5F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(uint lhs, uint4 rhs)
		{
			return new bool4(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w);
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x00061420 File Offset: 0x0005F620
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(uint4 lhs, uint4 rhs)
		{
			return new bool4(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w);
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00061476 File Offset: 0x0005F676
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(uint4 lhs, uint rhs)
		{
			return new bool4(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs);
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x000614AD File Offset: 0x0005F6AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(uint lhs, uint4 rhs)
		{
			return new bool4(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w);
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x000614E4 File Offset: 0x0005F6E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator ~(uint4 val)
		{
			return new uint4(~val.x, ~val.y, ~val.z, ~val.w);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x00061507 File Offset: 0x0005F707
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator &(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z, lhs.w & rhs.w);
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x00061542 File Offset: 0x0005F742
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator &(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x & rhs, lhs.y & rhs, lhs.z & rhs, lhs.w & rhs);
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x00061569 File Offset: 0x0005F769
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator &(uint lhs, uint4 rhs)
		{
			return new uint4(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z, lhs & rhs.w);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x00061590 File Offset: 0x0005F790
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator |(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z, lhs.w | rhs.w);
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x000615CB File Offset: 0x0005F7CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator |(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x | rhs, lhs.y | rhs, lhs.z | rhs, lhs.w | rhs);
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x000615F2 File Offset: 0x0005F7F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator |(uint lhs, uint4 rhs)
		{
			return new uint4(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z, lhs | rhs.w);
		}

		// Token: 0x06002237 RID: 8759 RVA: 0x00061619 File Offset: 0x0005F819
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator ^(uint4 lhs, uint4 rhs)
		{
			return new uint4(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z, lhs.w ^ rhs.w);
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x00061654 File Offset: 0x0005F854
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator ^(uint4 lhs, uint rhs)
		{
			return new uint4(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs, lhs.w ^ rhs);
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x0006167B File Offset: 0x0005F87B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 operator ^(uint lhs, uint4 rhs)
		{
			return new uint4(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z, lhs ^ rhs.w);
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x000616A2 File Offset: 0x0005F8A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x000616C1 File Offset: 0x0005F8C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x000616E0 File Offset: 0x0005F8E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x000616FF File Offset: 0x0005F8FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x0006171E File Offset: 0x0005F91E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x0006173D File Offset: 0x0005F93D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x0006175C File Offset: 0x0005F95C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x0006177B File Offset: 0x0005F97B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x0006179A File Offset: 0x0005F99A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x000617B9 File Offset: 0x0005F9B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x000617D8 File Offset: 0x0005F9D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x000617F7 File Offset: 0x0005F9F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x00061816 File Offset: 0x0005FA16
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x00061835 File Offset: 0x0005FA35
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002248 RID: 8776 RVA: 0x00061854 File Offset: 0x0005FA54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002249 RID: 8777 RVA: 0x00061873 File Offset: 0x0005FA73
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x0600224A RID: 8778 RVA: 0x00061892 File Offset: 0x0005FA92
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x0600224B RID: 8779 RVA: 0x000618B1 File Offset: 0x0005FAB1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x000618D0 File Offset: 0x0005FAD0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x000618EF File Offset: 0x0005FAEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x0006190E File Offset: 0x0005FB0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x0006192D File Offset: 0x0005FB2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002250 RID: 8784 RVA: 0x0006194C File Offset: 0x0005FB4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x0006196B File Offset: 0x0005FB6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06002252 RID: 8786 RVA: 0x0006198A File Offset: 0x0005FB8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x000619A9 File Offset: 0x0005FBA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002254 RID: 8788 RVA: 0x000619C8 File Offset: 0x0005FBC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x00061303 File Offset: 0x0005F503
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x00060B38 File Offset: 0x0005ED38
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x000619E7 File Offset: 0x0005FBE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x00061A06 File Offset: 0x0005FC06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002259 RID: 8793 RVA: 0x00061A25 File Offset: 0x0005FC25
		// (set) Token: 0x0600225A RID: 8794 RVA: 0x00061A44 File Offset: 0x0005FC44
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x0600225B RID: 8795 RVA: 0x00061A76 File Offset: 0x0005FC76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x00061A95 File Offset: 0x0005FC95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x0600225D RID: 8797 RVA: 0x00061AB4 File Offset: 0x0005FCB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x00061AD3 File Offset: 0x0005FCD3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x0600225F RID: 8799 RVA: 0x00061AF2 File Offset: 0x0005FCF2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x00061B11 File Offset: 0x0005FD11
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x00061B30 File Offset: 0x0005FD30
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x00061B4F File Offset: 0x0005FD4F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002263 RID: 8803 RVA: 0x00061B6E File Offset: 0x0005FD6E
		// (set) Token: 0x06002264 RID: 8804 RVA: 0x00061B8D File Offset: 0x0005FD8D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002265 RID: 8805 RVA: 0x00061BBF File Offset: 0x0005FDBF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x00061BDE File Offset: 0x0005FDDE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x00061BFD File Offset: 0x0005FDFD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x00061C1C File Offset: 0x0005FE1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002269 RID: 8809 RVA: 0x00061C3B File Offset: 0x0005FE3B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x00061C5A File Offset: 0x0005FE5A
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x00061C79 File Offset: 0x0005FE79
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x00061CAB File Offset: 0x0005FEAB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x00061CCA File Offset: 0x0005FECA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x00061CE9 File Offset: 0x0005FEE9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600226F RID: 8815 RVA: 0x00061D08 File Offset: 0x0005FF08
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x00061D27 File Offset: 0x0005FF27
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x00061D46 File Offset: 0x0005FF46
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x00061D65 File Offset: 0x0005FF65
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.y, this.x);
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x00061D84 File Offset: 0x0005FF84
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002274 RID: 8820 RVA: 0x00061DA3 File Offset: 0x0005FFA3
		// (set) Token: 0x06002275 RID: 8821 RVA: 0x00061DC2 File Offset: 0x0005FFC2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002276 RID: 8822 RVA: 0x00061DF4 File Offset: 0x0005FFF4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x00061E13 File Offset: 0x00060013
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06002278 RID: 8824 RVA: 0x00061E32 File Offset: 0x00060032
		// (set) Token: 0x06002279 RID: 8825 RVA: 0x00061E51 File Offset: 0x00060051
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600227A RID: 8826 RVA: 0x00061E83 File Offset: 0x00060083
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x00061EA2 File Offset: 0x000600A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x0600227C RID: 8828 RVA: 0x00061EC1 File Offset: 0x000600C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x00061EE0 File Offset: 0x000600E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x00061EFF File Offset: 0x000600FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x0600227F RID: 8831 RVA: 0x00061F1E File Offset: 0x0006011E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x00061F3D File Offset: 0x0006013D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002281 RID: 8833 RVA: 0x00061F5C File Offset: 0x0006015C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x00061F7B File Offset: 0x0006017B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002283 RID: 8835 RVA: 0x00061F9A File Offset: 0x0006019A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x00061FB9 File Offset: 0x000601B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x00061FD8 File Offset: 0x000601D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x00061FF7 File Offset: 0x000601F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x00062016 File Offset: 0x00060216
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002288 RID: 8840 RVA: 0x00062035 File Offset: 0x00060235
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x00062054 File Offset: 0x00060254
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x00062073 File Offset: 0x00060273
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x00062092 File Offset: 0x00060292
		// (set) Token: 0x0600228C RID: 8844 RVA: 0x000620B1 File Offset: 0x000602B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x000620E3 File Offset: 0x000602E3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x0600228E RID: 8846 RVA: 0x00062102 File Offset: 0x00060302
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x0600228F RID: 8847 RVA: 0x00062121 File Offset: 0x00060321
		// (set) Token: 0x06002290 RID: 8848 RVA: 0x00062140 File Offset: 0x00060340
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002291 RID: 8849 RVA: 0x00062172 File Offset: 0x00060372
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x00062191 File Offset: 0x00060391
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002293 RID: 8851 RVA: 0x000621B0 File Offset: 0x000603B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x000621CF File Offset: 0x000603CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002295 RID: 8853 RVA: 0x000621EE File Offset: 0x000603EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x0006220D File Offset: 0x0006040D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002297 RID: 8855 RVA: 0x0006222C File Offset: 0x0006042C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002298 RID: 8856 RVA: 0x0006224B File Offset: 0x0006044B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002299 RID: 8857 RVA: 0x0006226A File Offset: 0x0006046A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600229A RID: 8858 RVA: 0x00062289 File Offset: 0x00060489
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x000622A8 File Offset: 0x000604A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x000622C7 File Offset: 0x000604C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x000622E6 File Offset: 0x000604E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x0600229E RID: 8862 RVA: 0x00062305 File Offset: 0x00060505
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x0600229F RID: 8863 RVA: 0x00062324 File Offset: 0x00060524
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x00062343 File Offset: 0x00060543
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060022A1 RID: 8865 RVA: 0x00062362 File Offset: 0x00060562
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060022A2 RID: 8866 RVA: 0x00062381 File Offset: 0x00060581
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060022A3 RID: 8867 RVA: 0x000623A0 File Offset: 0x000605A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x000623BF File Offset: 0x000605BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060022A5 RID: 8869 RVA: 0x000623DE File Offset: 0x000605DE
		// (set) Token: 0x060022A6 RID: 8870 RVA: 0x000623FD File Offset: 0x000605FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060022A7 RID: 8871 RVA: 0x0006242F File Offset: 0x0006062F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060022A8 RID: 8872 RVA: 0x0006244E File Offset: 0x0006064E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060022A9 RID: 8873 RVA: 0x0006246D File Offset: 0x0006066D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060022AA RID: 8874 RVA: 0x0006248C File Offset: 0x0006068C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x060022AB RID: 8875 RVA: 0x000624AB File Offset: 0x000606AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060022AC RID: 8876 RVA: 0x000624CA File Offset: 0x000606CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060022AD RID: 8877 RVA: 0x000624E9 File Offset: 0x000606E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060022AE RID: 8878 RVA: 0x00062508 File Offset: 0x00060708
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x00062527 File Offset: 0x00060727
		// (set) Token: 0x060022B0 RID: 8880 RVA: 0x00062546 File Offset: 0x00060746
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x00062578 File Offset: 0x00060778
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060022B2 RID: 8882 RVA: 0x00062597 File Offset: 0x00060797
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x000625B6 File Offset: 0x000607B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x000625D5 File Offset: 0x000607D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x000625F4 File Offset: 0x000607F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x00062613 File Offset: 0x00060813
		// (set) Token: 0x060022B7 RID: 8887 RVA: 0x00062632 File Offset: 0x00060832
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x00062664 File Offset: 0x00060864
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x00062683 File Offset: 0x00060883
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.y, this.x);
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x000626A2 File Offset: 0x000608A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x000626C1 File Offset: 0x000608C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x000626E0 File Offset: 0x000608E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x000626FF File Offset: 0x000608FF
		// (set) Token: 0x060022BE RID: 8894 RVA: 0x0006271E File Offset: 0x0006091E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x00062750 File Offset: 0x00060950
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x0006276F File Offset: 0x0006096F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x0006278E File Offset: 0x0006098E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x000627AD File Offset: 0x000609AD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x000627CC File Offset: 0x000609CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x060022C4 RID: 8900 RVA: 0x000627EB File Offset: 0x000609EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x060022C5 RID: 8901 RVA: 0x0006280A File Offset: 0x00060A0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 ywww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x060022C6 RID: 8902 RVA: 0x00062829 File Offset: 0x00060A29
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x00062848 File Offset: 0x00060A48
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060022C8 RID: 8904 RVA: 0x00062867 File Offset: 0x00060A67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x00062886 File Offset: 0x00060A86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060022CA RID: 8906 RVA: 0x000628A5 File Offset: 0x00060AA5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x060022CB RID: 8907 RVA: 0x000628C4 File Offset: 0x00060AC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x060022CC RID: 8908 RVA: 0x000628E3 File Offset: 0x00060AE3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060022CD RID: 8909 RVA: 0x00062902 File Offset: 0x00060B02
		// (set) Token: 0x060022CE RID: 8910 RVA: 0x00062921 File Offset: 0x00060B21
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060022CF RID: 8911 RVA: 0x00062953 File Offset: 0x00060B53
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x00062972 File Offset: 0x00060B72
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x00062991 File Offset: 0x00060B91
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x000629B0 File Offset: 0x00060BB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x060022D3 RID: 8915 RVA: 0x000629CF File Offset: 0x00060BCF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x000629EE File Offset: 0x00060BEE
		// (set) Token: 0x060022D5 RID: 8917 RVA: 0x00062A0D File Offset: 0x00060C0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x00062A3F File Offset: 0x00060C3F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x00062A5E File Offset: 0x00060C5E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x00062A7D File Offset: 0x00060C7D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x00062A9C File Offset: 0x00060C9C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060022DA RID: 8922 RVA: 0x00062ABB File Offset: 0x00060CBB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x00062ADA File Offset: 0x00060CDA
		// (set) Token: 0x060022DC RID: 8924 RVA: 0x00062AF9 File Offset: 0x00060CF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
				this.w = value.w;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x00062B2B File Offset: 0x00060D2B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x00062B4A File Offset: 0x00060D4A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x00062B69 File Offset: 0x00060D69
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060022E0 RID: 8928 RVA: 0x00062B88 File Offset: 0x00060D88
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x00062BA7 File Offset: 0x00060DA7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x060022E2 RID: 8930 RVA: 0x00062BC6 File Offset: 0x00060DC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x00062BE5 File Offset: 0x00060DE5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x00062C04 File Offset: 0x00060E04
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x00062C23 File Offset: 0x00060E23
		// (set) Token: 0x060022E6 RID: 8934 RVA: 0x00062C42 File Offset: 0x00060E42
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x00062C74 File Offset: 0x00060E74
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x00062C93 File Offset: 0x00060E93
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x00062CB2 File Offset: 0x00060EB2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x00062CD1 File Offset: 0x00060ED1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x00062CF0 File Offset: 0x00060EF0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x00062D0F File Offset: 0x00060F0F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x060022ED RID: 8941 RVA: 0x00062D2E File Offset: 0x00060F2E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x00062D4D File Offset: 0x00060F4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x00062D6C File Offset: 0x00060F6C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x00062D8B File Offset: 0x00060F8B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060022F1 RID: 8945 RVA: 0x00062DAA File Offset: 0x00060FAA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x060022F2 RID: 8946 RVA: 0x00062DC9 File Offset: 0x00060FC9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x00062DE8 File Offset: 0x00060FE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x00062E07 File Offset: 0x00061007
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x00062E26 File Offset: 0x00061026
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x060022F6 RID: 8950 RVA: 0x00062E45 File Offset: 0x00061045
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x060022F7 RID: 8951 RVA: 0x00062E64 File Offset: 0x00061064
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x060022F8 RID: 8952 RVA: 0x00062E83 File Offset: 0x00061083
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x00062EA2 File Offset: 0x000610A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x060022FA RID: 8954 RVA: 0x00062EC1 File Offset: 0x000610C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x060022FB RID: 8955 RVA: 0x00062EE0 File Offset: 0x000610E0
		// (set) Token: 0x060022FC RID: 8956 RVA: 0x00062EFF File Offset: 0x000610FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x00062F31 File Offset: 0x00061131
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x00062F50 File Offset: 0x00061150
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x00062F6F File Offset: 0x0006116F
		// (set) Token: 0x06002300 RID: 8960 RVA: 0x00062F8E File Offset: 0x0006118E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x00062FC0 File Offset: 0x000611C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002302 RID: 8962 RVA: 0x00062FDF File Offset: 0x000611DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x00062FFE File Offset: 0x000611FE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002304 RID: 8964 RVA: 0x0006301D File Offset: 0x0006121D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x0006303C File Offset: 0x0006123C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06002306 RID: 8966 RVA: 0x0006305B File Offset: 0x0006125B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x0006307A File Offset: 0x0006127A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06002308 RID: 8968 RVA: 0x00063099 File Offset: 0x00061299
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x000630B8 File Offset: 0x000612B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x000630D7 File Offset: 0x000612D7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x000630F6 File Offset: 0x000612F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x00063115 File Offset: 0x00061315
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x0600230D RID: 8973 RVA: 0x00063134 File Offset: 0x00061334
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x00063153 File Offset: 0x00061353
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x00063172 File Offset: 0x00061372
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x00063191 File Offset: 0x00061391
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x000631B0 File Offset: 0x000613B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x000631CF File Offset: 0x000613CF
		// (set) Token: 0x06002313 RID: 8979 RVA: 0x000631EE File Offset: 0x000613EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x00063220 File Offset: 0x00061420
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.y, this.w);
			}
		}

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x0006323F File Offset: 0x0006143F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x0006325E File Offset: 0x0006145E
		// (set) Token: 0x06002317 RID: 8983 RVA: 0x0006327D File Offset: 0x0006147D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x000632AF File Offset: 0x000614AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x000632CE File Offset: 0x000614CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x000632ED File Offset: 0x000614ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x0006330C File Offset: 0x0006150C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x0006332B File Offset: 0x0006152B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x0006334A File Offset: 0x0006154A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x00063369 File Offset: 0x00061569
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x00063388 File Offset: 0x00061588
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06002320 RID: 8992 RVA: 0x000633A7 File Offset: 0x000615A7
		// (set) Token: 0x06002321 RID: 8993 RVA: 0x000633C6 File Offset: 0x000615C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
				this.z = value.w;
			}
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x000633F8 File Offset: 0x000615F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x06002323 RID: 8995 RVA: 0x00063417 File Offset: 0x00061617
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x00063436 File Offset: 0x00061636
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x00063455 File Offset: 0x00061655
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x00063474 File Offset: 0x00061674
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.y, this.w);
			}
		}

		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x00063493 File Offset: 0x00061693
		// (set) Token: 0x06002328 RID: 9000 RVA: 0x000634B2 File Offset: 0x000616B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x000634E4 File Offset: 0x000616E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x00063503 File Offset: 0x00061703
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600232B RID: 9003 RVA: 0x00063522 File Offset: 0x00061722
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x00063541 File Offset: 0x00061741
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x0600232D RID: 9005 RVA: 0x00063560 File Offset: 0x00061760
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x0006357F File Offset: 0x0006177F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x0006359E File Offset: 0x0006179E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000B16 RID: 2838
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x000635BD File Offset: 0x000617BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x000635DC File Offset: 0x000617DC
		// (set) Token: 0x06002332 RID: 9010 RVA: 0x000635FB File Offset: 0x000617FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
				this.y = value.w;
			}
		}

		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x0006362D File Offset: 0x0006182D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002334 RID: 9012 RVA: 0x0006364C File Offset: 0x0006184C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002335 RID: 9013 RVA: 0x0006366B File Offset: 0x0006186B
		// (set) Token: 0x06002336 RID: 9014 RVA: 0x0006368A File Offset: 0x0006188A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
				this.x = value.w;
			}
		}

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06002337 RID: 9015 RVA: 0x000636BC File Offset: 0x000618BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x000636DB File Offset: 0x000618DB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x000636FA File Offset: 0x000618FA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.y, this.w);
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x00063719 File Offset: 0x00061919
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x00063738 File Offset: 0x00061938
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x00063757 File Offset: 0x00061957
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x0600233D RID: 9021 RVA: 0x00063776 File Offset: 0x00061976
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x00063795 File Offset: 0x00061995
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x0600233F RID: 9023 RVA: 0x000637B4 File Offset: 0x000619B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x000637D3 File Offset: 0x000619D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x000637F2 File Offset: 0x000619F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x00063811 File Offset: 0x00061A11
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x00063830 File Offset: 0x00061A30
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x0006384F File Offset: 0x00061A4F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x0006386E File Offset: 0x00061A6E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x0006388D File Offset: 0x00061A8D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.y, this.x);
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x000638AC File Offset: 0x00061AAC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.y, this.y);
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x000638CB File Offset: 0x00061ACB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.y, this.z);
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002349 RID: 9033 RVA: 0x000638EA File Offset: 0x00061AEA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.y, this.w);
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x00063909 File Offset: 0x00061B09
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.z, this.x);
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600234B RID: 9035 RVA: 0x00063928 File Offset: 0x00061B28
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.z, this.y);
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x00063947 File Offset: 0x00061B47
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.z, this.z);
			}
		}

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x0600234D RID: 9037 RVA: 0x00063966 File Offset: 0x00061B66
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x00063985 File Offset: 0x00061B85
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x000639A4 File Offset: 0x00061BA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002350 RID: 9040 RVA: 0x000639C3 File Offset: 0x00061BC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x000639E2 File Offset: 0x00061BE2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 wwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.w, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x06002352 RID: 9042 RVA: 0x00063A01 File Offset: 0x00061C01
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x00063A1A File Offset: 0x00061C1A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x00063A33 File Offset: 0x00061C33
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06002355 RID: 9045 RVA: 0x00063A4C File Offset: 0x00061C4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.w);
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06002356 RID: 9046 RVA: 0x00063A65 File Offset: 0x00061C65
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x00063A7E File Offset: 0x00061C7E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002358 RID: 9048 RVA: 0x00063A97 File Offset: 0x00061C97
		// (set) Token: 0x06002359 RID: 9049 RVA: 0x00063AB0 File Offset: 0x00061CB0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x00063AD6 File Offset: 0x00061CD6
		// (set) Token: 0x0600235B RID: 9051 RVA: 0x00063AEF File Offset: 0x00061CEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600235C RID: 9052 RVA: 0x00063B15 File Offset: 0x00061D15
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x0600235D RID: 9053 RVA: 0x00063B2E File Offset: 0x00061D2E
		// (set) Token: 0x0600235E RID: 9054 RVA: 0x00063B47 File Offset: 0x00061D47
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x0600235F RID: 9055 RVA: 0x00063B6D File Offset: 0x00061D6D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002360 RID: 9056 RVA: 0x00063B86 File Offset: 0x00061D86
		// (set) Token: 0x06002361 RID: 9057 RVA: 0x00063B9F File Offset: 0x00061D9F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002362 RID: 9058 RVA: 0x00063BC5 File Offset: 0x00061DC5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.w, this.x);
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002363 RID: 9059 RVA: 0x00063BDE File Offset: 0x00061DDE
		// (set) Token: 0x06002364 RID: 9060 RVA: 0x00063BF7 File Offset: 0x00061DF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002365 RID: 9061 RVA: 0x00063C1D File Offset: 0x00061E1D
		// (set) Token: 0x06002366 RID: 9062 RVA: 0x00063C36 File Offset: 0x00061E36
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002367 RID: 9063 RVA: 0x00063C5C File Offset: 0x00061E5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.w, this.w);
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002368 RID: 9064 RVA: 0x00063C75 File Offset: 0x00061E75
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002369 RID: 9065 RVA: 0x00063C8E File Offset: 0x00061E8E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x00063CA7 File Offset: 0x00061EA7
		// (set) Token: 0x0600236B RID: 9067 RVA: 0x00063CC0 File Offset: 0x00061EC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x00063CE6 File Offset: 0x00061EE6
		// (set) Token: 0x0600236D RID: 9069 RVA: 0x00063CFF File Offset: 0x00061EFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x0600236E RID: 9070 RVA: 0x00063D25 File Offset: 0x00061F25
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600236F RID: 9071 RVA: 0x00063D3E File Offset: 0x00061F3E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002370 RID: 9072 RVA: 0x00063D57 File Offset: 0x00061F57
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002371 RID: 9073 RVA: 0x00063D70 File Offset: 0x00061F70
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.w);
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002372 RID: 9074 RVA: 0x00063D89 File Offset: 0x00061F89
		// (set) Token: 0x06002373 RID: 9075 RVA: 0x00063DA2 File Offset: 0x00061FA2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002374 RID: 9076 RVA: 0x00063DC8 File Offset: 0x00061FC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x00063DE1 File Offset: 0x00061FE1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.z, this.z);
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06002376 RID: 9078 RVA: 0x00063DFA File Offset: 0x00061FFA
		// (set) Token: 0x06002377 RID: 9079 RVA: 0x00063E13 File Offset: 0x00062013
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002378 RID: 9080 RVA: 0x00063E39 File Offset: 0x00062039
		// (set) Token: 0x06002379 RID: 9081 RVA: 0x00063E52 File Offset: 0x00062052
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 ywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x0600237A RID: 9082 RVA: 0x00063E78 File Offset: 0x00062078
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 ywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.w, this.y);
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x0600237B RID: 9083 RVA: 0x00063E91 File Offset: 0x00062091
		// (set) Token: 0x0600237C RID: 9084 RVA: 0x00063EAA File Offset: 0x000620AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 ywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600237D RID: 9085 RVA: 0x00063ED0 File Offset: 0x000620D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.w, this.w);
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x0600237E RID: 9086 RVA: 0x00063EE9 File Offset: 0x000620E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x00063F02 File Offset: 0x00062102
		// (set) Token: 0x06002380 RID: 9088 RVA: 0x00063F1B File Offset: 0x0006211B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x00063F41 File Offset: 0x00062141
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x00063F5A File Offset: 0x0006215A
		// (set) Token: 0x06002383 RID: 9091 RVA: 0x00063F73 File Offset: 0x00062173
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06002384 RID: 9092 RVA: 0x00063F99 File Offset: 0x00062199
		// (set) Token: 0x06002385 RID: 9093 RVA: 0x00063FB2 File Offset: 0x000621B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x00063FD8 File Offset: 0x000621D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06002387 RID: 9095 RVA: 0x00063FF1 File Offset: 0x000621F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06002388 RID: 9096 RVA: 0x0006400A File Offset: 0x0006220A
		// (set) Token: 0x06002389 RID: 9097 RVA: 0x00064023 File Offset: 0x00062223
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600238A RID: 9098 RVA: 0x00064049 File Offset: 0x00062249
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600238B RID: 9099 RVA: 0x00064062 File Offset: 0x00062262
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x0600238C RID: 9100 RVA: 0x0006407B File Offset: 0x0006227B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.z);
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x0600238D RID: 9101 RVA: 0x00064094 File Offset: 0x00062294
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.w);
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x000640AD File Offset: 0x000622AD
		// (set) Token: 0x0600238F RID: 9103 RVA: 0x000640C6 File Offset: 0x000622C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x000640EC File Offset: 0x000622EC
		// (set) Token: 0x06002391 RID: 9105 RVA: 0x00064105 File Offset: 0x00062305
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002392 RID: 9106 RVA: 0x0006412B File Offset: 0x0006232B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.w, this.z);
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002393 RID: 9107 RVA: 0x00064144 File Offset: 0x00062344
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.w, this.w);
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x0006415D File Offset: 0x0006235D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.x, this.x);
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x00064176 File Offset: 0x00062376
		// (set) Token: 0x06002396 RID: 9110 RVA: 0x0006418F File Offset: 0x0006238F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x000641B5 File Offset: 0x000623B5
		// (set) Token: 0x06002398 RID: 9112 RVA: 0x000641CE File Offset: 0x000623CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x000641F4 File Offset: 0x000623F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.x, this.w);
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x0006420D File Offset: 0x0006240D
		// (set) Token: 0x0600239B RID: 9115 RVA: 0x00064226 File Offset: 0x00062426
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x0006424C File Offset: 0x0006244C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.y, this.y);
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x0600239D RID: 9117 RVA: 0x00064265 File Offset: 0x00062465
		// (set) Token: 0x0600239E RID: 9118 RVA: 0x0006427E File Offset: 0x0006247E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x0600239F RID: 9119 RVA: 0x000642A4 File Offset: 0x000624A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.y, this.w);
			}
		}

		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060023A0 RID: 9120 RVA: 0x000642BD File Offset: 0x000624BD
		// (set) Token: 0x060023A1 RID: 9121 RVA: 0x000642D6 File Offset: 0x000624D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x060023A2 RID: 9122 RVA: 0x000642FC File Offset: 0x000624FC
		// (set) Token: 0x060023A3 RID: 9123 RVA: 0x00064315 File Offset: 0x00062515
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x060023A4 RID: 9124 RVA: 0x0006433B File Offset: 0x0006253B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.z, this.z);
			}
		}

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x060023A5 RID: 9125 RVA: 0x00064354 File Offset: 0x00062554
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.z, this.w);
			}
		}

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x0006436D File Offset: 0x0006256D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.w, this.x);
			}
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x00064386 File Offset: 0x00062586
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.w, this.y);
			}
		}

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x0006439F File Offset: 0x0006259F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 wwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.w, this.z);
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x000643B8 File Offset: 0x000625B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 www
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.w, this.w, this.w);
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x000643D1 File Offset: 0x000625D1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.x);
			}
		}

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x060023AB RID: 9131 RVA: 0x000643E4 File Offset: 0x000625E4
		// (set) Token: 0x060023AC RID: 9132 RVA: 0x000643F7 File Offset: 0x000625F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x060023AD RID: 9133 RVA: 0x00064411 File Offset: 0x00062611
		// (set) Token: 0x060023AE RID: 9134 RVA: 0x00064424 File Offset: 0x00062624
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x0006443E File Offset: 0x0006263E
		// (set) Token: 0x060023B0 RID: 9136 RVA: 0x00064451 File Offset: 0x00062651
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060023B1 RID: 9137 RVA: 0x0006446B File Offset: 0x0006266B
		// (set) Token: 0x060023B2 RID: 9138 RVA: 0x0006447E File Offset: 0x0006267E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060023B3 RID: 9139 RVA: 0x00064498 File Offset: 0x00062698
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.y);
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x000644AB File Offset: 0x000626AB
		// (set) Token: 0x060023B5 RID: 9141 RVA: 0x000644BE File Offset: 0x000626BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060023B6 RID: 9142 RVA: 0x000644D8 File Offset: 0x000626D8
		// (set) Token: 0x060023B7 RID: 9143 RVA: 0x000644EB File Offset: 0x000626EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060023B8 RID: 9144 RVA: 0x00064505 File Offset: 0x00062705
		// (set) Token: 0x060023B9 RID: 9145 RVA: 0x00064518 File Offset: 0x00062718
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060023BA RID: 9146 RVA: 0x00064532 File Offset: 0x00062732
		// (set) Token: 0x060023BB RID: 9147 RVA: 0x00064545 File Offset: 0x00062745
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060023BC RID: 9148 RVA: 0x0006455F File Offset: 0x0006275F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.z, this.z);
			}
		}

		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x00064572 File Offset: 0x00062772
		// (set) Token: 0x060023BE RID: 9150 RVA: 0x00064585 File Offset: 0x00062785
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 zw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x0006459F File Offset: 0x0006279F
		// (set) Token: 0x060023C0 RID: 9152 RVA: 0x000645B2 File Offset: 0x000627B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 wx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x060023C1 RID: 9153 RVA: 0x000645CC File Offset: 0x000627CC
		// (set) Token: 0x060023C2 RID: 9154 RVA: 0x000645DF File Offset: 0x000627DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 wy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x060023C3 RID: 9155 RVA: 0x000645F9 File Offset: 0x000627F9
		// (set) Token: 0x060023C4 RID: 9156 RVA: 0x0006460C File Offset: 0x0006280C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 wz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x060023C5 RID: 9157 RVA: 0x00064626 File Offset: 0x00062826
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 ww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.w, this.w);
			}
		}

		// Token: 0x17000B86 RID: 2950
		public unsafe uint this[int index]
		{
			get
			{
				fixed (uint4* ptr = &this)
				{
					return ((uint*)ptr)[index];
				}
			}
			set
			{
				fixed (uint* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x00064674 File Offset: 0x00062874
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint4 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z && this.w == rhs.w;
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000646B0 File Offset: 0x000628B0
		public override bool Equals(object o)
		{
			if (o is uint4)
			{
				uint4 converted = (uint4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000646D5 File Offset: 0x000628D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000646E4 File Offset: 0x000628E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint4({0}, {1}, {2}, {3})", new object[] { this.x, this.y, this.z, this.w });
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x0006473C File Offset: 0x0006293C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint4({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x04000165 RID: 357
		public uint x;

		// Token: 0x04000166 RID: 358
		public uint y;

		// Token: 0x04000167 RID: 359
		public uint z;

		// Token: 0x04000168 RID: 360
		public uint w;

		// Token: 0x04000169 RID: 361
		public static readonly uint4 zero;

		// Token: 0x0200005F RID: 95
		internal sealed class DebuggerProxy
		{
			// Token: 0x060023CD RID: 9165 RVA: 0x00064799 File Offset: 0x00062999
			public DebuggerProxy(uint4 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
				this.w = v.w;
			}

			// Token: 0x0400016A RID: 362
			public uint x;

			// Token: 0x0400016B RID: 363
			public uint y;

			// Token: 0x0400016C RID: 364
			public uint z;

			// Token: 0x0400016D RID: 365
			public uint w;
		}
	}
}
