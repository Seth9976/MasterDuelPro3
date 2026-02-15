using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000059 RID: 89
	[DebuggerTypeProxy(typeof(uint3.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct uint3 : IEquatable<uint3>, IFormattable
	{
		// Token: 0x0600203D RID: 8253 RVA: 0x0005C6E5 File Offset: 0x0005A8E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(uint x, uint y, uint z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x0005C6FC File Offset: 0x0005A8FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(uint x, uint2 yz)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
		}

		// Token: 0x0600203F RID: 8255 RVA: 0x0005C71D File Offset: 0x0005A91D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(uint2 xy, uint z)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
		}

		// Token: 0x06002040 RID: 8256 RVA: 0x0005C73E File Offset: 0x0005A93E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(uint3 xyz)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
		}

		// Token: 0x06002041 RID: 8257 RVA: 0x0005C764 File Offset: 0x0005A964
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(uint v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x0005C77B File Offset: 0x0005A97B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(bool v)
		{
			this.x = (v ? 1U : 0U);
			this.y = (v ? 1U : 0U);
			this.z = (v ? 1U : 0U);
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x0005C7A4 File Offset: 0x0005A9A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(bool3 v)
		{
			this.x = (v.x ? 1U : 0U);
			this.y = (v.y ? 1U : 0U);
			this.z = (v.z ? 1U : 0U);
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x0005C764 File Offset: 0x0005A964
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(int v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
			this.z = (uint)v;
		}

		// Token: 0x06002045 RID: 8261 RVA: 0x0005C7DC File Offset: 0x0005A9DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(int3 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
			this.z = (uint)v.z;
		}

		// Token: 0x06002046 RID: 8262 RVA: 0x0005C802 File Offset: 0x0005AA02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(float v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
			this.z = (uint)v;
		}

		// Token: 0x06002047 RID: 8263 RVA: 0x0005C81C File Offset: 0x0005AA1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(float3 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
			this.z = (uint)v.z;
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x0005C802 File Offset: 0x0005AA02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(double v)
		{
			this.x = (uint)v;
			this.y = (uint)v;
			this.z = (uint)v;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x0005C845 File Offset: 0x0005AA45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3(double3 v)
		{
			this.x = (uint)v.x;
			this.y = (uint)v.y;
			this.z = (uint)v.z;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x0002046E File Offset: 0x0001E66E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator uint3(uint v)
		{
			return new uint3(v);
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00020476 File Offset: 0x0001E676
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(bool v)
		{
			return new uint3(v);
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0002047E File Offset: 0x0001E67E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(bool3 v)
		{
			return new uint3(v);
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x00020486 File Offset: 0x0001E686
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(int v)
		{
			return new uint3(v);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0002048E File Offset: 0x0001E68E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(int3 v)
		{
			return new uint3(v);
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x00020496 File Offset: 0x0001E696
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(float v)
		{
			return new uint3(v);
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x0002049E File Offset: 0x0001E69E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(float3 v)
		{
			return new uint3(v);
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000204A6 File Offset: 0x0001E6A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(double v)
		{
			return new uint3(v);
		}

		// Token: 0x06002052 RID: 8274 RVA: 0x000204AE File Offset: 0x0001E6AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator uint3(double3 v)
		{
			return new uint3(v);
		}

		// Token: 0x06002053 RID: 8275 RVA: 0x0005C86E File Offset: 0x0005AA6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator *(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		}

		// Token: 0x06002054 RID: 8276 RVA: 0x0005C89C File Offset: 0x0005AA9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator *(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}

		// Token: 0x06002055 RID: 8277 RVA: 0x0005C8BB File Offset: 0x0005AABB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator *(uint lhs, uint3 rhs)
		{
			return new uint3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
		}

		// Token: 0x06002056 RID: 8278 RVA: 0x0005C8DA File Offset: 0x0005AADA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator +(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

		// Token: 0x06002057 RID: 8279 RVA: 0x0005C908 File Offset: 0x0005AB08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator +(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);
		}

		// Token: 0x06002058 RID: 8280 RVA: 0x0005C927 File Offset: 0x0005AB27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator +(uint lhs, uint3 rhs)
		{
			return new uint3(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);
		}

		// Token: 0x06002059 RID: 8281 RVA: 0x0005C946 File Offset: 0x0005AB46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator -(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}

		// Token: 0x0600205A RID: 8282 RVA: 0x0005C974 File Offset: 0x0005AB74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator -(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
		}

		// Token: 0x0600205B RID: 8283 RVA: 0x0005C993 File Offset: 0x0005AB93
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator -(uint lhs, uint3 rhs)
		{
			return new uint3(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);
		}

		// Token: 0x0600205C RID: 8284 RVA: 0x0005C9B2 File Offset: 0x0005ABB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator /(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x0005C9E0 File Offset: 0x0005ABE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator /(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x0005C9FF File Offset: 0x0005ABFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator /(uint lhs, uint3 rhs)
		{
			return new uint3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x0005CA1E File Offset: 0x0005AC1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator %(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z);
		}

		// Token: 0x06002060 RID: 8288 RVA: 0x0005CA4C File Offset: 0x0005AC4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator %(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs);
		}

		// Token: 0x06002061 RID: 8289 RVA: 0x0005CA6B File Offset: 0x0005AC6B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator %(uint lhs, uint3 rhs)
		{
			return new uint3(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z);
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x0005CA8C File Offset: 0x0005AC8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator ++(uint3 val)
		{
			uint num = val.x + 1U;
			val.x = num;
			uint num2 = num;
			num = val.y + 1U;
			val.y = num;
			uint num3 = num;
			num = val.z + 1U;
			val.z = num;
			return new uint3(num2, num3, num);
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x0005CACC File Offset: 0x0005ACCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator --(uint3 val)
		{
			uint num = val.x - 1U;
			val.x = num;
			uint num2 = num;
			num = val.y - 1U;
			val.y = num;
			uint num3 = num;
			num = val.z - 1U;
			val.z = num;
			return new uint3(num2, num3, num);
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x0005CB0B File Offset: 0x0005AD0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(uint3 lhs, uint3 rhs)
		{
			return new bool3(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z);
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x0005CB3C File Offset: 0x0005AD3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(uint3 lhs, uint rhs)
		{
			return new bool3(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs);
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x0005CB5E File Offset: 0x0005AD5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(uint lhs, uint3 rhs)
		{
			return new bool3(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z);
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x0005CB80 File Offset: 0x0005AD80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(uint3 lhs, uint3 rhs)
		{
			return new bool3(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z);
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x0005CBBA File Offset: 0x0005ADBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(uint3 lhs, uint rhs)
		{
			return new bool3(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs);
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x0005CBE5 File Offset: 0x0005ADE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(uint lhs, uint3 rhs)
		{
			return new bool3(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z);
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x0005CC10 File Offset: 0x0005AE10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(uint3 lhs, uint3 rhs)
		{
			return new bool3(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z);
		}

		// Token: 0x0600206B RID: 8299 RVA: 0x0005CC41 File Offset: 0x0005AE41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(uint3 lhs, uint rhs)
		{
			return new bool3(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs);
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x0005CC63 File Offset: 0x0005AE63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(uint lhs, uint3 rhs)
		{
			return new bool3(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x0005CC85 File Offset: 0x0005AE85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(uint3 lhs, uint3 rhs)
		{
			return new bool3(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z);
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x0005CCBF File Offset: 0x0005AEBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(uint3 lhs, uint rhs)
		{
			return new bool3(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs);
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x0005CCEA File Offset: 0x0005AEEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(uint lhs, uint3 rhs)
		{
			return new bool3(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z);
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x0005CD15 File Offset: 0x0005AF15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator -(uint3 val)
		{
			return new uint3((uint)(-(uint)((ulong)val.x)), (uint)(-(uint)((ulong)val.y)), (uint)(-(uint)((ulong)val.z)));
		}

		// Token: 0x06002071 RID: 8305 RVA: 0x0005CD37 File Offset: 0x0005AF37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator +(uint3 val)
		{
			return new uint3(val.x, val.y, val.z);
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x0005CD50 File Offset: 0x0005AF50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator <<(uint3 x, int n)
		{
			return new uint3(x.x << n, x.y << n, x.z << n);
		}

		// Token: 0x06002073 RID: 8307 RVA: 0x0005CD78 File Offset: 0x0005AF78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator >>(uint3 x, int n)
		{
			return new uint3(x.x >> n, x.y >> n, x.z >> n);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x0005CDA0 File Offset: 0x0005AFA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(uint3 lhs, uint3 rhs)
		{
			return new bool3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x0005CDD1 File Offset: 0x0005AFD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(uint3 lhs, uint rhs)
		{
			return new bool3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x0005CDF3 File Offset: 0x0005AFF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(uint lhs, uint3 rhs)
		{
			return new bool3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x0005CE15 File Offset: 0x0005B015
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(uint3 lhs, uint3 rhs)
		{
			return new bool3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x0005CE4F File Offset: 0x0005B04F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(uint3 lhs, uint rhs)
		{
			return new bool3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x0005CE7A File Offset: 0x0005B07A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(uint lhs, uint3 rhs)
		{
			return new bool3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x0005CEA5 File Offset: 0x0005B0A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator ~(uint3 val)
		{
			return new uint3(~val.x, ~val.y, ~val.z);
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x0005CEC1 File Offset: 0x0005B0C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator &(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x & rhs.x, lhs.y & rhs.y, lhs.z & rhs.z);
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x0005CEEF File Offset: 0x0005B0EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator &(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x & rhs, lhs.y & rhs, lhs.z & rhs);
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0005CF0E File Offset: 0x0005B10E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator &(uint lhs, uint3 rhs)
		{
			return new uint3(lhs & rhs.x, lhs & rhs.y, lhs & rhs.z);
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x0005CF2D File Offset: 0x0005B12D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator |(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x | rhs.x, lhs.y | rhs.y, lhs.z | rhs.z);
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x0005CF5B File Offset: 0x0005B15B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator |(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x | rhs, lhs.y | rhs, lhs.z | rhs);
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x0005CF7A File Offset: 0x0005B17A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator |(uint lhs, uint3 rhs)
		{
			return new uint3(lhs | rhs.x, lhs | rhs.y, lhs | rhs.z);
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x0005CF99 File Offset: 0x0005B199
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator ^(uint3 lhs, uint3 rhs)
		{
			return new uint3(lhs.x ^ rhs.x, lhs.y ^ rhs.y, lhs.z ^ rhs.z);
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0005CFC7 File Offset: 0x0005B1C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator ^(uint3 lhs, uint rhs)
		{
			return new uint3(lhs.x ^ rhs, lhs.y ^ rhs, lhs.z ^ rhs);
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0005CFE6 File Offset: 0x0005B1E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 operator ^(uint lhs, uint3 rhs)
		{
			return new uint3(lhs ^ rhs.x, lhs ^ rhs.y, lhs ^ rhs.z);
		}

		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06002084 RID: 8324 RVA: 0x0005D005 File Offset: 0x0005B205
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x0005D024 File Offset: 0x0005B224
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06002086 RID: 8326 RVA: 0x0005D043 File Offset: 0x0005B243
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x0005D062 File Offset: 0x0005B262
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x0005D081 File Offset: 0x0005B281
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06002089 RID: 8329 RVA: 0x0005D0A0 File Offset: 0x0005B2A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x0600208A RID: 8330 RVA: 0x0005D0BF File Offset: 0x0005B2BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x0600208B RID: 8331 RVA: 0x0005D0DE File Offset: 0x0005B2DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x0600208C RID: 8332 RVA: 0x0005D0FD File Offset: 0x0005B2FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x0005D11C File Offset: 0x0005B31C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x0600208E RID: 8334 RVA: 0x0005D13B File Offset: 0x0005B33B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x0600208F RID: 8335 RVA: 0x0005D15A File Offset: 0x0005B35A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002090 RID: 8336 RVA: 0x0005D179 File Offset: 0x0005B379
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002091 RID: 8337 RVA: 0x0005D198 File Offset: 0x0005B398
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x0005D1B7 File Offset: 0x0005B3B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x0005D1D6 File Offset: 0x0005B3D6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002094 RID: 8340 RVA: 0x0005D1F5 File Offset: 0x0005B3F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x0005D214 File Offset: 0x0005B414
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002096 RID: 8342 RVA: 0x0005D233 File Offset: 0x0005B433
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002097 RID: 8343 RVA: 0x0005D252 File Offset: 0x0005B452
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x0005D271 File Offset: 0x0005B471
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x0005D290 File Offset: 0x0005B490
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x0600209A RID: 8346 RVA: 0x0005D2AF File Offset: 0x0005B4AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x0005D2CE File Offset: 0x0005B4CE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600209C RID: 8348 RVA: 0x0005D2ED File Offset: 0x0005B4ED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x0005D30C File Offset: 0x0005B50C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x0005D32B File Offset: 0x0005B52B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x0600209F RID: 8351 RVA: 0x0005D34A File Offset: 0x0005B54A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x0005D369 File Offset: 0x0005B569
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x0005D388 File Offset: 0x0005B588
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x0005D3A7 File Offset: 0x0005B5A7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x0005D3C6 File Offset: 0x0005B5C6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x0005D3E5 File Offset: 0x0005B5E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x0005D404 File Offset: 0x0005B604
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x0005D423 File Offset: 0x0005B623
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x0005D442 File Offset: 0x0005B642
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x0005D461 File Offset: 0x0005B661
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x0005D480 File Offset: 0x0005B680
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x0005D49F File Offset: 0x0005B69F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x0005D4BE File Offset: 0x0005B6BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x0005D4DD File Offset: 0x0005B6DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x0005D4FC File Offset: 0x0005B6FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x0005D51B File Offset: 0x0005B71B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x060020AF RID: 8367 RVA: 0x0005D53A File Offset: 0x0005B73A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0005D559 File Offset: 0x0005B759
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x060020B1 RID: 8369 RVA: 0x0005D578 File Offset: 0x0005B778
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x0005D597 File Offset: 0x0005B797
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x0005D5B6 File Offset: 0x0005B7B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x0005D5D5 File Offset: 0x0005B7D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x0005D5F4 File Offset: 0x0005B7F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x0005D613 File Offset: 0x0005B813
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x0005D632 File Offset: 0x0005B832
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x0005D651 File Offset: 0x0005B851
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x0005D670 File Offset: 0x0005B870
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x0005D68F File Offset: 0x0005B88F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x0005D6AE File Offset: 0x0005B8AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x0005D6CD File Offset: 0x0005B8CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x0005D6EC File Offset: 0x0005B8EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x0005D70B File Offset: 0x0005B90B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x0005D72A File Offset: 0x0005B92A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x0005D749 File Offset: 0x0005B949
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0005D768 File Offset: 0x0005B968
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x0005D787 File Offset: 0x0005B987
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x0005D7A6 File Offset: 0x0005B9A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x0005D7C5 File Offset: 0x0005B9C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x0005D7E4 File Offset: 0x0005B9E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x0005D803 File Offset: 0x0005BA03
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0005D822 File Offset: 0x0005BA22
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x0005D841 File Offset: 0x0005BA41
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x0005D860 File Offset: 0x0005BA60
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x0005D87F File Offset: 0x0005BA7F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x0005D89E File Offset: 0x0005BA9E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x0005D8BD File Offset: 0x0005BABD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x0005D8DC File Offset: 0x0005BADC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x0005D8FB File Offset: 0x0005BAFB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x0005D91A File Offset: 0x0005BB1A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x0005D939 File Offset: 0x0005BB39
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x0005D958 File Offset: 0x0005BB58
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x0005D977 File Offset: 0x0005BB77
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0005D996 File Offset: 0x0005BB96
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x0005D9B5 File Offset: 0x0005BBB5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x0005D9D4 File Offset: 0x0005BBD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x0005D9ED File Offset: 0x0005BBED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x0005DA06 File Offset: 0x0005BC06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x0005DA1F File Offset: 0x0005BC1F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x0005DA38 File Offset: 0x0005BC38
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x0005CD37 File Offset: 0x0005AF37
		// (set) Token: 0x060020DB RID: 8411 RVA: 0x0005C73E File Offset: 0x0005A93E
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

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x0005DA51 File Offset: 0x0005BC51
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x0005DA6A File Offset: 0x0005BC6A
		// (set) Token: 0x060020DE RID: 8414 RVA: 0x0005DA83 File Offset: 0x0005BC83
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

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x0005DAA9 File Offset: 0x0005BCA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x0005DAC2 File Offset: 0x0005BCC2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x060020E1 RID: 8417 RVA: 0x0005DADB File Offset: 0x0005BCDB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x060020E2 RID: 8418 RVA: 0x0005DAF4 File Offset: 0x0005BCF4
		// (set) Token: 0x060020E3 RID: 8419 RVA: 0x0005DB0D File Offset: 0x0005BD0D
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

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x060020E4 RID: 8420 RVA: 0x0005DB33 File Offset: 0x0005BD33
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x0005DB4C File Offset: 0x0005BD4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.y);
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x0005DB65 File Offset: 0x0005BD65
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.y, this.z);
			}
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x060020E7 RID: 8423 RVA: 0x0005DB7E File Offset: 0x0005BD7E
		// (set) Token: 0x060020E8 RID: 8424 RVA: 0x0005DB97 File Offset: 0x0005BD97
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

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060020E9 RID: 8425 RVA: 0x0005DBBD File Offset: 0x0005BDBD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.z, this.y);
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060020EA RID: 8426 RVA: 0x0005DBD6 File Offset: 0x0005BDD6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.y, this.z, this.z);
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060020EB RID: 8427 RVA: 0x0005DBEF File Offset: 0x0005BDEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x0005DC08 File Offset: 0x0005BE08
		// (set) Token: 0x060020ED RID: 8429 RVA: 0x0005DC21 File Offset: 0x0005BE21
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

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x060020EE RID: 8430 RVA: 0x0005DC47 File Offset: 0x0005BE47
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x0005DC60 File Offset: 0x0005BE60
		// (set) Token: 0x060020F0 RID: 8432 RVA: 0x0005DC79 File Offset: 0x0005BE79
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

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x0005DC9F File Offset: 0x0005BE9F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060020F2 RID: 8434 RVA: 0x0005DCB8 File Offset: 0x0005BEB8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x0005DCD1 File Offset: 0x0005BED1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060020F4 RID: 8436 RVA: 0x0005DCEA File Offset: 0x0005BEEA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x0005DD03 File Offset: 0x0005BF03
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint3(this.z, this.z, this.z);
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x0005DD1C File Offset: 0x0005BF1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.x, this.x);
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x0005DD2F File Offset: 0x0005BF2F
		// (set) Token: 0x060020F8 RID: 8440 RVA: 0x0005DD42 File Offset: 0x0005BF42
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

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x0005DD5C File Offset: 0x0005BF5C
		// (set) Token: 0x060020FA RID: 8442 RVA: 0x0005DD6F File Offset: 0x0005BF6F
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

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x0005DD89 File Offset: 0x0005BF89
		// (set) Token: 0x060020FC RID: 8444 RVA: 0x0005DD9C File Offset: 0x0005BF9C
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

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x0005DDB6 File Offset: 0x0005BFB6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.y, this.y);
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x0005DDC9 File Offset: 0x0005BFC9
		// (set) Token: 0x060020FF RID: 8447 RVA: 0x0005DDDC File Offset: 0x0005BFDC
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

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06002100 RID: 8448 RVA: 0x0005DDF6 File Offset: 0x0005BFF6
		// (set) Token: 0x06002101 RID: 8449 RVA: 0x0005DE09 File Offset: 0x0005C009
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

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002102 RID: 8450 RVA: 0x0005DE23 File Offset: 0x0005C023
		// (set) Token: 0x06002103 RID: 8451 RVA: 0x0005DE36 File Offset: 0x0005C036
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

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x0005DE50 File Offset: 0x0005C050
		[EditorBrowsable(EditorBrowsableState.Never)]
		public uint2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new uint2(this.z, this.z);
			}
		}

		// Token: 0x17000A32 RID: 2610
		public unsafe uint this[int index]
		{
			get
			{
				fixed (uint3* ptr = &this)
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

		// Token: 0x06002107 RID: 8455 RVA: 0x0005DE9C File Offset: 0x0005C09C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(uint3 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z;
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x0005DECC File Offset: 0x0005C0CC
		public override bool Equals(object o)
		{
			if (o is uint3)
			{
				uint3 converted = (uint3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0005DEF1 File Offset: 0x0005C0F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x0005DEFE File Offset: 0x0005C0FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("uint3({0}, {1}, {2})", this.x, this.y, this.z);
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x0005DF2B File Offset: 0x0005C12B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("uint3({0}, {1}, {2})", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider), this.z.ToString(format, formatProvider));
		}

		// Token: 0x04000151 RID: 337
		public uint x;

		// Token: 0x04000152 RID: 338
		public uint y;

		// Token: 0x04000153 RID: 339
		public uint z;

		// Token: 0x04000154 RID: 340
		public static readonly uint3 zero;

		// Token: 0x0200005A RID: 90
		internal sealed class DebuggerProxy
		{
			// Token: 0x0600210C RID: 8460 RVA: 0x0005DF5E File Offset: 0x0005C15E
			public DebuggerProxy(uint3 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
			}

			// Token: 0x04000155 RID: 341
			public uint x;

			// Token: 0x04000156 RID: 342
			public uint y;

			// Token: 0x04000157 RID: 343
			public uint z;
		}
	}
}
