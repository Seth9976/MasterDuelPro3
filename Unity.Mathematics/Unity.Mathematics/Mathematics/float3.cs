using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Unity.Mathematics
{
	// Token: 0x02000030 RID: 48
	[DebuggerTypeProxy(typeof(float3.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float3 : IEquatable<float3>, IFormattable
	{
		// Token: 0x060011B5 RID: 4533 RVA: 0x00038901 File Offset: 0x00036B01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x00038918 File Offset: 0x00036B18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(float x, float2 yz)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x00038939 File Offset: 0x00036B39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(float2 xy, float z)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0003895A File Offset: 0x00036B5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(float3 xyz)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x00038980 File Offset: 0x00036B80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(float v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x00038998 File Offset: 0x00036B98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(bool v)
		{
			this.x = (v ? 1f : 0f);
			this.y = (v ? 1f : 0f);
			this.z = (v ? 1f : 0f);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000389E4 File Offset: 0x00036BE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(bool3 v)
		{
			this.x = (v.x ? 1f : 0f);
			this.y = (v.y ? 1f : 0f);
			this.z = (v.z ? 1f : 0f);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x00038A3F File Offset: 0x00036C3F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(int v)
		{
			this.x = (float)v;
			this.y = (float)v;
			this.z = (float)v;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x00038A59 File Offset: 0x00036C59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(int3 v)
		{
			this.x = (float)v.x;
			this.y = (float)v.y;
			this.z = (float)v.z;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x00038A82 File Offset: 0x00036C82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(uint v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x00038A9F File Offset: 0x00036C9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(uint3 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x00038ACB File Offset: 0x00036CCB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(half v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x00038AF1 File Offset: 0x00036CF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(half3 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x00038A3F File Offset: 0x00036C3F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(double v)
		{
			this.x = (float)v;
			this.y = (float)v;
			this.z = (float)v;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x00038B26 File Offset: 0x00036D26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3(double3 v)
		{
			this.x = (float)v.x;
			this.y = (float)v.y;
			this.z = (float)v.z;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0000C0DE File Offset: 0x0000A2DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(float v)
		{
			return new float3(v);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0000C0E6 File Offset: 0x0000A2E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3(bool v)
		{
			return new float3(v);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0000C0EE File Offset: 0x0000A2EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3(bool3 v)
		{
			return new float3(v);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0000C0F6 File Offset: 0x0000A2F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(int v)
		{
			return new float3(v);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0000C0FE File Offset: 0x0000A2FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(int3 v)
		{
			return new float3(v);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0000C106 File Offset: 0x0000A306
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(uint v)
		{
			return new float3(v);
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0000C10E File Offset: 0x0000A30E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(uint3 v)
		{
			return new float3(v);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0000C116 File Offset: 0x0000A316
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(half v)
		{
			return new float3(v);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0000C11E File Offset: 0x0000A31E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float3(half3 v)
		{
			return new float3(v);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0000C126 File Offset: 0x0000A326
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3(double v)
		{
			return new float3(v);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0000C12E File Offset: 0x0000A32E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float3(double3 v)
		{
			return new float3(v);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00038B4F File Offset: 0x00036D4F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator *(float3 lhs, float3 rhs)
		{
			return new float3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00038B7D File Offset: 0x00036D7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator *(float3 lhs, float rhs)
		{
			return new float3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00038B9C File Offset: 0x00036D9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator *(float lhs, float3 rhs)
		{
			return new float3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00038BBB File Offset: 0x00036DBB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator +(float3 lhs, float3 rhs)
		{
			return new float3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00038BE9 File Offset: 0x00036DE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator +(float3 lhs, float rhs)
		{
			return new float3(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00038C08 File Offset: 0x00036E08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator +(float lhs, float3 rhs)
		{
			return new float3(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00038C27 File Offset: 0x00036E27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator -(float3 lhs, float3 rhs)
		{
			return new float3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00038C55 File Offset: 0x00036E55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator -(float3 lhs, float rhs)
		{
			return new float3(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00038C74 File Offset: 0x00036E74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator -(float lhs, float3 rhs)
		{
			return new float3(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00038C93 File Offset: 0x00036E93
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator /(float3 lhs, float3 rhs)
		{
			return new float3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00038CC1 File Offset: 0x00036EC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator /(float3 lhs, float rhs)
		{
			return new float3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00038CE0 File Offset: 0x00036EE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator /(float lhs, float3 rhs)
		{
			return new float3(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000123C0 File Offset: 0x000105C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator %(float3 lhs, float3 rhs)
		{
			return new float3(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00038CFF File Offset: 0x00036EFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator %(float3 lhs, float rhs)
		{
			return new float3(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00038D1E File Offset: 0x00036F1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator %(float lhs, float3 rhs)
		{
			return new float3(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x00038D40 File Offset: 0x00036F40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator ++(float3 val)
		{
			float num = val.x + 1f;
			val.x = num;
			float num2 = num;
			num = val.y + 1f;
			val.y = num;
			float num3 = num;
			num = val.z + 1f;
			val.z = num;
			return new float3(num2, num3, num);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x00038D8C File Offset: 0x00036F8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator --(float3 val)
		{
			float num = val.x - 1f;
			val.x = num;
			float num2 = num;
			num = val.y - 1f;
			val.y = num;
			float num3 = num;
			num = val.z - 1f;
			val.z = num;
			return new float3(num2, num3, num);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00038DD7 File Offset: 0x00036FD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(float3 lhs, float3 rhs)
		{
			return new bool3(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00038E08 File Offset: 0x00037008
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(float3 lhs, float rhs)
		{
			return new bool3(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00038E2A File Offset: 0x0003702A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <(float lhs, float3 rhs)
		{
			return new bool3(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00038E4C File Offset: 0x0003704C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(float3 lhs, float3 rhs)
		{
			return new bool3(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x00038E86 File Offset: 0x00037086
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(float3 lhs, float rhs)
		{
			return new bool3(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00038EB1 File Offset: 0x000370B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator <=(float lhs, float3 rhs)
		{
			return new bool3(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00038EDC File Offset: 0x000370DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(float3 lhs, float3 rhs)
		{
			return new bool3(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00038F0D File Offset: 0x0003710D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(float3 lhs, float rhs)
		{
			return new bool3(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs);
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x00038F2F File Offset: 0x0003712F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >(float lhs, float3 rhs)
		{
			return new bool3(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00038F51 File Offset: 0x00037151
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(float3 lhs, float3 rhs)
		{
			return new bool3(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00038F8B File Offset: 0x0003718B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(float3 lhs, float rhs)
		{
			return new bool3(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00038FB6 File Offset: 0x000371B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator >=(float lhs, float3 rhs)
		{
			return new bool3(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z);
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00038FE1 File Offset: 0x000371E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator -(float3 val)
		{
			return new float3(-val.x, -val.y, -val.z);
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00038FFD File Offset: 0x000371FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 operator +(float3 val)
		{
			return new float3(val.x, val.y, val.z);
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00039016 File Offset: 0x00037216
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(float3 lhs, float3 rhs)
		{
			return new bool3(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z);
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00039047 File Offset: 0x00037247
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(float3 lhs, float rhs)
		{
			return new bool3(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00039069 File Offset: 0x00037269
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator ==(float lhs, float3 rhs)
		{
			return new bool3(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0003908B File Offset: 0x0003728B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(float3 lhs, float3 rhs)
		{
			return new bool3(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000390C5 File Offset: 0x000372C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(float3 lhs, float rhs)
		{
			return new bool3(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x000390F0 File Offset: 0x000372F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 operator !=(float lhs, float3 rhs)
		{
			return new bool3(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z);
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x0003911B File Offset: 0x0003731B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0003913A File Offset: 0x0003733A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x00039159 File Offset: 0x00037359
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00039178 File Offset: 0x00037378
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00039197 File Offset: 0x00037397
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x000391B6 File Offset: 0x000373B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x000391D5 File Offset: 0x000373D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x000391F4 File Offset: 0x000373F4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x00039213 File Offset: 0x00037413
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x00039232 File Offset: 0x00037432
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x00039251 File Offset: 0x00037451
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x00039270 File Offset: 0x00037470
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0003928F File Offset: 0x0003748F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x000392AE File Offset: 0x000374AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x000392CD File Offset: 0x000374CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x000392EC File Offset: 0x000374EC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x0003930B File Offset: 0x0003750B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x0003932A File Offset: 0x0003752A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x00039349 File Offset: 0x00037549
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x00039368 File Offset: 0x00037568
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x00039387 File Offset: 0x00037587
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x000393A6 File Offset: 0x000375A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x000393C5 File Offset: 0x000375C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x000393E4 File Offset: 0x000375E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x00039403 File Offset: 0x00037603
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00039422 File Offset: 0x00037622
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x00039441 File Offset: 0x00037641
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00039460 File Offset: 0x00037660
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0003947F File Offset: 0x0003767F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0003949E File Offset: 0x0003769E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x000394BD File Offset: 0x000376BD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x000394DC File Offset: 0x000376DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x000394FB File Offset: 0x000376FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x0003951A File Offset: 0x0003771A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00039539 File Offset: 0x00037739
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x00039558 File Offset: 0x00037758
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x00039577 File Offset: 0x00037777
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x00039596 File Offset: 0x00037796
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x000395B5 File Offset: 0x000377B5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x000395D4 File Offset: 0x000377D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x000395F3 File Offset: 0x000377F3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x00039612 File Offset: 0x00037812
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x00039631 File Offset: 0x00037831
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x00039650 File Offset: 0x00037850
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001220 RID: 4640 RVA: 0x0003966F File Offset: 0x0003786F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001221 RID: 4641 RVA: 0x0003968E File Offset: 0x0003788E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x000396AD File Offset: 0x000378AD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001223 RID: 4643 RVA: 0x000396CC File Offset: 0x000378CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x000396EB File Offset: 0x000378EB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x0003970A File Offset: 0x0003790A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x00039729 File Offset: 0x00037929
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00039748 File Offset: 0x00037948
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00039767 File Offset: 0x00037967
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00039786 File Offset: 0x00037986
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x000397A5 File Offset: 0x000379A5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x000397C4 File Offset: 0x000379C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x000397E3 File Offset: 0x000379E3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00039802 File Offset: 0x00037A02
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x00039821 File Offset: 0x00037A21
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x00039840 File Offset: 0x00037A40
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001230 RID: 4656 RVA: 0x0003985F File Offset: 0x00037A5F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0003987E File Offset: 0x00037A7E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x0003989D File Offset: 0x00037A9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x000398BC File Offset: 0x00037ABC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x000398DB File Offset: 0x00037ADB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x000398FA File Offset: 0x00037AFA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x00039919 File Offset: 0x00037B19
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x00039938 File Offset: 0x00037B38
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001238 RID: 4664 RVA: 0x00039957 File Offset: 0x00037B57
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x00039976 File Offset: 0x00037B76
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x00039995 File Offset: 0x00037B95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x000399B4 File Offset: 0x00037BB4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x000399D3 File Offset: 0x00037BD3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x000399F2 File Offset: 0x00037BF2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x00039A11 File Offset: 0x00037C11
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x00039A30 File Offset: 0x00037C30
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001240 RID: 4672 RVA: 0x00039A4F File Offset: 0x00037C4F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00039A6E File Offset: 0x00037C6E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x00039A8D File Offset: 0x00037C8D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x00039AAC File Offset: 0x00037CAC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x00039ACB File Offset: 0x00037CCB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x00039AEA File Offset: 0x00037CEA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.x);
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00039B03 File Offset: 0x00037D03
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.y);
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00039B1C File Offset: 0x00037D1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.z);
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x00039B35 File Offset: 0x00037D35
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00039B4E File Offset: 0x00037D4E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.y);
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x0600124A RID: 4682 RVA: 0x00038FFD File Offset: 0x000371FD
		// (set) Token: 0x0600124B RID: 4683 RVA: 0x0003895A File Offset: 0x00036B5A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x0600124C RID: 4684 RVA: 0x00039B67 File Offset: 0x00037D67
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.z, this.x);
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x0600124D RID: 4685 RVA: 0x00039B80 File Offset: 0x00037D80
		// (set) Token: 0x0600124E RID: 4686 RVA: 0x00039B99 File Offset: 0x00037D99
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x00039BBF File Offset: 0x00037DBF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.z, this.z);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x00039BD8 File Offset: 0x00037DD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x00039BF1 File Offset: 0x00037DF1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00039C0A File Offset: 0x00037E0A
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x00039C23 File Offset: 0x00037E23
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x00039C49 File Offset: 0x00037E49
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00039C62 File Offset: 0x00037E62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.y);
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00039C7B File Offset: 0x00037E7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.z);
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00039C94 File Offset: 0x00037E94
		// (set) Token: 0x06001258 RID: 4696 RVA: 0x00039CAD File Offset: 0x00037EAD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x00039CD3 File Offset: 0x00037ED3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.z, this.y);
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x00039CEC File Offset: 0x00037EEC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.z, this.z);
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x00039D05 File Offset: 0x00037F05
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.x, this.x);
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x00039D1E File Offset: 0x00037F1E
		// (set) Token: 0x0600125D RID: 4701 RVA: 0x00039D37 File Offset: 0x00037F37
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x00039D5D File Offset: 0x00037F5D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00039D76 File Offset: 0x00037F76
		// (set) Token: 0x06001260 RID: 4704 RVA: 0x00039D8F File Offset: 0x00037F8F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00039DB5 File Offset: 0x00037FB5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.y, this.y);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x00039DCE File Offset: 0x00037FCE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.y, this.z);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00039DE7 File Offset: 0x00037FE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.x);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001264 RID: 4708 RVA: 0x00039E00 File Offset: 0x00038000
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.y);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001265 RID: 4709 RVA: 0x00039E19 File Offset: 0x00038019
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.z);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x00039E32 File Offset: 0x00038032
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.x);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001267 RID: 4711 RVA: 0x00039E45 File Offset: 0x00038045
		// (set) Token: 0x06001268 RID: 4712 RVA: 0x00039E58 File Offset: 0x00038058
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001269 RID: 4713 RVA: 0x00039E72 File Offset: 0x00038072
		// (set) Token: 0x0600126A RID: 4714 RVA: 0x00039E85 File Offset: 0x00038085
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600126B RID: 4715 RVA: 0x00039E9F File Offset: 0x0003809F
		// (set) Token: 0x0600126C RID: 4716 RVA: 0x00039EB2 File Offset: 0x000380B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600126D RID: 4717 RVA: 0x00039ECC File Offset: 0x000380CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.y);
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600126E RID: 4718 RVA: 0x00039EDF File Offset: 0x000380DF
		// (set) Token: 0x0600126F RID: 4719 RVA: 0x00039EF2 File Offset: 0x000380F2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00039F0C File Offset: 0x0003810C
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x00039F1F File Offset: 0x0003811F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 zx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00039F39 File Offset: 0x00038139
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x00039F4C File Offset: 0x0003814C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 zy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00039F66 File Offset: 0x00038166
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.z, this.z);
			}
		}

		// Token: 0x17000470 RID: 1136
		public unsafe float this[int index]
		{
			get
			{
				fixed (float3* ptr = &this)
				{
					return ((float*)ptr)[index];
				}
			}
			set
			{
				fixed (float* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00039FB4 File Offset: 0x000381B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float3 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00039FE4 File Offset: 0x000381E4
		public override bool Equals(object o)
		{
			if (o is float3)
			{
				float3 converted = (float3)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x0003A009 File Offset: 0x00038209
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0003A016 File Offset: 0x00038216
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float3({0}f, {1}f, {2}f)", this.x, this.y, this.z);
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x0003A043 File Offset: 0x00038243
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float3({0}f, {1}f, {2}f)", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider), this.z.ToString(format, formatProvider));
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0003A076 File Offset: 0x00038276
		public static implicit operator Vector3(float3 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x0003A08F File Offset: 0x0003828F
		public static implicit operator float3(Vector3 v)
		{
			return new float3(v.x, v.y, v.z);
		}

		// Token: 0x040000B8 RID: 184
		public float x;

		// Token: 0x040000B9 RID: 185
		public float y;

		// Token: 0x040000BA RID: 186
		public float z;

		// Token: 0x040000BB RID: 187
		public static readonly float3 zero;

		// Token: 0x02000031 RID: 49
		internal sealed class DebuggerProxy
		{
			// Token: 0x0600127E RID: 4734 RVA: 0x0003A0A8 File Offset: 0x000382A8
			public DebuggerProxy(float3 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
			}

			// Token: 0x040000BC RID: 188
			public float x;

			// Token: 0x040000BD RID: 189
			public float y;

			// Token: 0x040000BE RID: 190
			public float z;
		}
	}
}
