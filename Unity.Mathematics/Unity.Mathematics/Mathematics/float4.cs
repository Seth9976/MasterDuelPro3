using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Unity.Mathematics
{
	// Token: 0x02000035 RID: 53
	[DebuggerTypeProxy(typeof(float4.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float4 : IEquatable<float4>, IFormattable
	{
		// Token: 0x06001357 RID: 4951 RVA: 0x0003D095 File Offset: 0x0003B295
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0003D0B4 File Offset: 0x0003B2B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float x, float y, float2 zw)
		{
			this.x = x;
			this.y = y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0003D0DC File Offset: 0x0003B2DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float x, float2 yz, float w)
		{
			this.x = x;
			this.y = yz.x;
			this.z = yz.y;
			this.w = w;
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0003D104 File Offset: 0x0003B304
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float x, float3 yzw)
		{
			this.x = x;
			this.y = yzw.x;
			this.z = yzw.y;
			this.w = yzw.z;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0003D131 File Offset: 0x0003B331
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float2 xy, float z, float w)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0003D159 File Offset: 0x0003B359
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float2 xy, float2 zw)
		{
			this.x = xy.x;
			this.y = xy.y;
			this.z = zw.x;
			this.w = zw.y;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0003D18B File Offset: 0x0003B38B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float3 xyz, float w)
		{
			this.x = xyz.x;
			this.y = xyz.y;
			this.z = xyz.z;
			this.w = w;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0003D1B8 File Offset: 0x0003B3B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float4 xyzw)
		{
			this.x = xyzw.x;
			this.y = xyzw.y;
			this.z = xyzw.z;
			this.w = xyzw.w;
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0003D1EA File Offset: 0x0003B3EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(float v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0003D208 File Offset: 0x0003B408
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(bool v)
		{
			this.x = (v ? 1f : 0f);
			this.y = (v ? 1f : 0f);
			this.z = (v ? 1f : 0f);
			this.w = (v ? 1f : 0f);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0003D26C File Offset: 0x0003B46C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(bool4 v)
		{
			this.x = (v.x ? 1f : 0f);
			this.y = (v.y ? 1f : 0f);
			this.z = (v.z ? 1f : 0f);
			this.w = (v.w ? 1f : 0f);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0003D2E1 File Offset: 0x0003B4E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(int v)
		{
			this.x = (float)v;
			this.y = (float)v;
			this.z = (float)v;
			this.w = (float)v;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0003D303 File Offset: 0x0003B503
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(int4 v)
		{
			this.x = (float)v.x;
			this.y = (float)v.y;
			this.z = (float)v.z;
			this.w = (float)v.w;
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0003D339 File Offset: 0x0003B539
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(uint v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0003D35F File Offset: 0x0003B55F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(uint4 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
			this.w = v.w;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0003D399 File Offset: 0x0003B599
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(half v)
		{
			this.x = v;
			this.y = v;
			this.z = v;
			this.w = v;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0003D3CC File Offset: 0x0003B5CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(half4 v)
		{
			this.x = v.x;
			this.y = v.y;
			this.z = v.z;
			this.w = v.w;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0003D2E1 File Offset: 0x0003B4E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(double v)
		{
			this.x = (float)v;
			this.y = (float)v;
			this.z = (float)v;
			this.w = (float)v;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0003D41D File Offset: 0x0003B61D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4(double4 v)
		{
			this.x = (float)v.x;
			this.y = (float)v.y;
			this.z = (float)v.z;
			this.w = (float)v.w;
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0000CB0A File Offset: 0x0000AD0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(float v)
		{
			return new float4(v);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0000CB12 File Offset: 0x0000AD12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4(bool v)
		{
			return new float4(v);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0000CB1A File Offset: 0x0000AD1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4(bool4 v)
		{
			return new float4(v);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0000CB22 File Offset: 0x0000AD22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(int v)
		{
			return new float4(v);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0000CB2A File Offset: 0x0000AD2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(int4 v)
		{
			return new float4(v);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0000CB32 File Offset: 0x0000AD32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(uint v)
		{
			return new float4(v);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0000CB3A File Offset: 0x0000AD3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(uint4 v)
		{
			return new float4(v);
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x0000CB42 File Offset: 0x0000AD42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(half v)
		{
			return new float4(v);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0000CB4A File Offset: 0x0000AD4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float4(half4 v)
		{
			return new float4(v);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0000CB52 File Offset: 0x0000AD52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4(double v)
		{
			return new float4(v);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0000CB5A File Offset: 0x0000AD5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float4(double4 v)
		{
			return new float4(v);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0003D453 File Offset: 0x0003B653
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator *(float4 lhs, float4 rhs)
		{
			return new float4(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z, lhs.w * rhs.w);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x0003D48E File Offset: 0x0003B68E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator *(float4 lhs, float rhs)
		{
			return new float4(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x0003D4B5 File Offset: 0x0003B6B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator *(float lhs, float4 rhs)
		{
			return new float4(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z, lhs * rhs.w);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0003D4DC File Offset: 0x0003B6DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator +(float4 lhs, float4 rhs)
		{
			return new float4(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z, lhs.w + rhs.w);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0003D517 File Offset: 0x0003B717
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator +(float4 lhs, float rhs)
		{
			return new float4(lhs.x + rhs, lhs.y + rhs, lhs.z + rhs, lhs.w + rhs);
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0003D53E File Offset: 0x0003B73E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator +(float lhs, float4 rhs)
		{
			return new float4(lhs + rhs.x, lhs + rhs.y, lhs + rhs.z, lhs + rhs.w);
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0003D565 File Offset: 0x0003B765
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator -(float4 lhs, float4 rhs)
		{
			return new float4(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z, lhs.w - rhs.w);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0003D5A0 File Offset: 0x0003B7A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator -(float4 lhs, float rhs)
		{
			return new float4(lhs.x - rhs, lhs.y - rhs, lhs.z - rhs, lhs.w - rhs);
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0003D5C7 File Offset: 0x0003B7C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator -(float lhs, float4 rhs)
		{
			return new float4(lhs - rhs.x, lhs - rhs.y, lhs - rhs.z, lhs - rhs.w);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0003D5EE File Offset: 0x0003B7EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator /(float4 lhs, float4 rhs)
		{
			return new float4(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z, lhs.w / rhs.w);
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0003D629 File Offset: 0x0003B829
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator /(float4 lhs, float rhs)
		{
			return new float4(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0003D650 File Offset: 0x0003B850
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator /(float lhs, float4 rhs)
		{
			return new float4(lhs / rhs.x, lhs / rhs.y, lhs / rhs.z, lhs / rhs.w);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000123EE File Offset: 0x000105EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator %(float4 lhs, float4 rhs)
		{
			return new float4(lhs.x % rhs.x, lhs.y % rhs.y, lhs.z % rhs.z, lhs.w % rhs.w);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0003D677 File Offset: 0x0003B877
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator %(float4 lhs, float rhs)
		{
			return new float4(lhs.x % rhs, lhs.y % rhs, lhs.z % rhs, lhs.w % rhs);
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0003D69E File Offset: 0x0003B89E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator %(float lhs, float4 rhs)
		{
			return new float4(lhs % rhs.x, lhs % rhs.y, lhs % rhs.z, lhs % rhs.w);
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0003D6C8 File Offset: 0x0003B8C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator ++(float4 val)
		{
			float num = val.x + 1f;
			val.x = num;
			float num2 = num;
			num = val.y + 1f;
			val.y = num;
			float num3 = num;
			num = val.z + 1f;
			val.z = num;
			float num4 = num;
			num = val.w + 1f;
			val.w = num;
			return new float4(num2, num3, num4, num);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0003D728 File Offset: 0x0003B928
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator --(float4 val)
		{
			float num = val.x - 1f;
			val.x = num;
			float num2 = num;
			num = val.y - 1f;
			val.y = num;
			float num3 = num;
			num = val.z - 1f;
			val.z = num;
			float num4 = num;
			num = val.w - 1f;
			val.w = num;
			return new float4(num2, num3, num4, num);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0003D786 File Offset: 0x0003B986
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(float4 lhs, float4 rhs)
		{
			return new bool4(lhs.x < rhs.x, lhs.y < rhs.y, lhs.z < rhs.z, lhs.w < rhs.w);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0003D7C5 File Offset: 0x0003B9C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(float4 lhs, float rhs)
		{
			return new bool4(lhs.x < rhs, lhs.y < rhs, lhs.z < rhs, lhs.w < rhs);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0003D7F0 File Offset: 0x0003B9F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <(float lhs, float4 rhs)
		{
			return new bool4(lhs < rhs.x, lhs < rhs.y, lhs < rhs.z, lhs < rhs.w);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0003D81C File Offset: 0x0003BA1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(float4 lhs, float4 rhs)
		{
			return new bool4(lhs.x <= rhs.x, lhs.y <= rhs.y, lhs.z <= rhs.z, lhs.w <= rhs.w);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0003D872 File Offset: 0x0003BA72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(float4 lhs, float rhs)
		{
			return new bool4(lhs.x <= rhs, lhs.y <= rhs, lhs.z <= rhs, lhs.w <= rhs);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0003D8A9 File Offset: 0x0003BAA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator <=(float lhs, float4 rhs)
		{
			return new bool4(lhs <= rhs.x, lhs <= rhs.y, lhs <= rhs.z, lhs <= rhs.w);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0003D8E0 File Offset: 0x0003BAE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(float4 lhs, float4 rhs)
		{
			return new bool4(lhs.x > rhs.x, lhs.y > rhs.y, lhs.z > rhs.z, lhs.w > rhs.w);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0003D91F File Offset: 0x0003BB1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(float4 lhs, float rhs)
		{
			return new bool4(lhs.x > rhs, lhs.y > rhs, lhs.z > rhs, lhs.w > rhs);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0003D94A File Offset: 0x0003BB4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >(float lhs, float4 rhs)
		{
			return new bool4(lhs > rhs.x, lhs > rhs.y, lhs > rhs.z, lhs > rhs.w);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0003D978 File Offset: 0x0003BB78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(float4 lhs, float4 rhs)
		{
			return new bool4(lhs.x >= rhs.x, lhs.y >= rhs.y, lhs.z >= rhs.z, lhs.w >= rhs.w);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0003D9CE File Offset: 0x0003BBCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(float4 lhs, float rhs)
		{
			return new bool4(lhs.x >= rhs, lhs.y >= rhs, lhs.z >= rhs, lhs.w >= rhs);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0003DA05 File Offset: 0x0003BC05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator >=(float lhs, float4 rhs)
		{
			return new bool4(lhs >= rhs.x, lhs >= rhs.y, lhs >= rhs.z, lhs >= rhs.w);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0003DA3C File Offset: 0x0003BC3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator -(float4 val)
		{
			return new float4(-val.x, -val.y, -val.z, -val.w);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0003DA5F File Offset: 0x0003BC5F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 operator +(float4 val)
		{
			return new float4(val.x, val.y, val.z, val.w);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0003DA7E File Offset: 0x0003BC7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(float4 lhs, float4 rhs)
		{
			return new bool4(lhs.x == rhs.x, lhs.y == rhs.y, lhs.z == rhs.z, lhs.w == rhs.w);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0003DABD File Offset: 0x0003BCBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(float4 lhs, float rhs)
		{
			return new bool4(lhs.x == rhs, lhs.y == rhs, lhs.z == rhs, lhs.w == rhs);
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x0003DAE8 File Offset: 0x0003BCE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator ==(float lhs, float4 rhs)
		{
			return new bool4(lhs == rhs.x, lhs == rhs.y, lhs == rhs.z, lhs == rhs.w);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0003DB14 File Offset: 0x0003BD14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(float4 lhs, float4 rhs)
		{
			return new bool4(lhs.x != rhs.x, lhs.y != rhs.y, lhs.z != rhs.z, lhs.w != rhs.w);
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0003DB6A File Offset: 0x0003BD6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(float4 lhs, float rhs)
		{
			return new bool4(lhs.x != rhs, lhs.y != rhs, lhs.z != rhs, lhs.w != rhs);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0003DBA1 File Offset: 0x0003BDA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 operator !=(float lhs, float4 rhs)
		{
			return new bool4(lhs != rhs.x, lhs != rhs.y, lhs != rhs.z, lhs != rhs.w);
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x0003DBD8 File Offset: 0x0003BDD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0003DBF7 File Offset: 0x0003BDF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0003DC16 File Offset: 0x0003BE16
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0003DC35 File Offset: 0x0003BE35
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0003DC54 File Offset: 0x0003BE54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0003DC73 File Offset: 0x0003BE73
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0003DC92 File Offset: 0x0003BE92
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.z);
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0003DCB1 File Offset: 0x0003BEB1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.w);
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060013A2 RID: 5026 RVA: 0x0003DCD0 File Offset: 0x0003BED0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x0003DCEF File Offset: 0x0003BEEF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.y);
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0003DD0E File Offset: 0x0003BF0E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x060013A5 RID: 5029 RVA: 0x0003DD2D File Offset: 0x0003BF2D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x0003DD4C File Offset: 0x0003BF4C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x0003DD6B File Offset: 0x0003BF6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x060013A8 RID: 5032 RVA: 0x0003DD8A File Offset: 0x0003BF8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0003DDA9 File Offset: 0x0003BFA9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x0003DDC8 File Offset: 0x0003BFC8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x0003DDE7 File Offset: 0x0003BFE7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x0003DE06 File Offset: 0x0003C006
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x0003DE25 File Offset: 0x0003C025
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060013AE RID: 5038 RVA: 0x0003DE44 File Offset: 0x0003C044
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x0003DE63 File Offset: 0x0003C063
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0003DE82 File Offset: 0x0003C082
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0003DEA1 File Offset: 0x0003C0A1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.w);
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0003DEC0 File Offset: 0x0003C0C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.x);
			}
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0003DEDF File Offset: 0x0003C0DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.y);
			}
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x0003DEFE File Offset: 0x0003C0FE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0003DA5F File Offset: 0x0003BC5F
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x0003D1B8 File Offset: 0x0003B3B8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.z, this.w);
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

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0003DF1D File Offset: 0x0003C11D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x0003DF3C File Offset: 0x0003C13C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0003DF5B File Offset: 0x0003C15B
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x0003DF7A File Offset: 0x0003C17A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.w, this.z);
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

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0003DFAC File Offset: 0x0003C1AC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0003DFCB File Offset: 0x0003C1CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0003DFEA File Offset: 0x0003C1EA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x0003E009 File Offset: 0x0003C209
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0003E028 File Offset: 0x0003C228
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x0003E047 File Offset: 0x0003C247
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0003E066 File Offset: 0x0003C266
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x0003E085 File Offset: 0x0003C285
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0003E0A4 File Offset: 0x0003C2A4
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x0003E0C3 File Offset: 0x0003C2C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.y, this.w);
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

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0003E0F5 File Offset: 0x0003C2F5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0003E114 File Offset: 0x0003C314
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060013C7 RID: 5063 RVA: 0x0003E133 File Offset: 0x0003C333
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x0003E152 File Offset: 0x0003C352
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.z, this.w);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060013C9 RID: 5065 RVA: 0x0003E171 File Offset: 0x0003C371
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.w, this.x);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x0003E190 File Offset: 0x0003C390
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x0003E1AF File Offset: 0x0003C3AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.w, this.y);
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

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x0003E1E1 File Offset: 0x0003C3E1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.w, this.z);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060013CD RID: 5069 RVA: 0x0003E200 File Offset: 0x0003C400
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.z, this.w, this.w);
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x0003E21F File Offset: 0x0003C41F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.x, this.x);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060013CF RID: 5071 RVA: 0x0003E23E File Offset: 0x0003C43E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.x, this.y);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x0003E25D File Offset: 0x0003C45D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.x, this.z);
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060013D1 RID: 5073 RVA: 0x0003E27C File Offset: 0x0003C47C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.x, this.w);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0003E29B File Offset: 0x0003C49B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.y, this.x);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0003E2BA File Offset: 0x0003C4BA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.y, this.y);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0003E2D9 File Offset: 0x0003C4D9
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x0003E2F8 File Offset: 0x0003C4F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.y, this.z);
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

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x0003E32A File Offset: 0x0003C52A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.y, this.w);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0003E349 File Offset: 0x0003C549
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.z, this.x);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0003E368 File Offset: 0x0003C568
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x0003E387 File Offset: 0x0003C587
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.z, this.y);
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

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x0003E3B9 File Offset: 0x0003C5B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.z, this.z);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0003E3D8 File Offset: 0x0003C5D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.z, this.w);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0003E3F7 File Offset: 0x0003C5F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.w, this.x);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0003E416 File Offset: 0x0003C616
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.w, this.y);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x0003E435 File Offset: 0x0003C635
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.w, this.z);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0003E454 File Offset: 0x0003C654
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.w, this.w, this.w);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0003E473 File Offset: 0x0003C673
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x0003E492 File Offset: 0x0003C692
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x0003E4B1 File Offset: 0x0003C6B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.z);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x0003E4D0 File Offset: 0x0003C6D0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.w);
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x0003E4EF File Offset: 0x0003C6EF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060013E5 RID: 5093 RVA: 0x0003E50E File Offset: 0x0003C70E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x0003E52D File Offset: 0x0003C72D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.z);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x0003E54C File Offset: 0x0003C74C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.w);
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060013E8 RID: 5096 RVA: 0x0003E56B File Offset: 0x0003C76B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.x);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x0003E58A File Offset: 0x0003C78A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.y);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060013EA RID: 5098 RVA: 0x0003E5A9 File Offset: 0x0003C7A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.z);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060013EB RID: 5099 RVA: 0x0003E5C8 File Offset: 0x0003C7C8
		// (set) Token: 0x060013EC RID: 5100 RVA: 0x0003E5E7 File Offset: 0x0003C7E7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.z, this.w);
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

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x0003E619 File Offset: 0x0003C819
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.w, this.x);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x0003E638 File Offset: 0x0003C838
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.w, this.y);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0003E657 File Offset: 0x0003C857
		// (set) Token: 0x060013F0 RID: 5104 RVA: 0x0003E676 File Offset: 0x0003C876
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.w, this.z);
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

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x0003E6A8 File Offset: 0x0003C8A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.w, this.w);
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x0003E6C7 File Offset: 0x0003C8C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x0003E6E6 File Offset: 0x0003C8E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x0003E705 File Offset: 0x0003C905
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.z);
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x0003E724 File Offset: 0x0003C924
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.w);
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x0003E743 File Offset: 0x0003C943
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060013F7 RID: 5111 RVA: 0x0003E762 File Offset: 0x0003C962
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060013F8 RID: 5112 RVA: 0x0003E781 File Offset: 0x0003C981
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.z);
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x0003E7A0 File Offset: 0x0003C9A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.w);
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x0003E7BF File Offset: 0x0003C9BF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.x);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x0003E7DE File Offset: 0x0003C9DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.y);
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060013FC RID: 5116 RVA: 0x0003E7FD File Offset: 0x0003C9FD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.z);
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x0003E81C File Offset: 0x0003CA1C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.z, this.w);
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060013FE RID: 5118 RVA: 0x0003E83B File Offset: 0x0003CA3B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.w, this.x);
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x0003E85A File Offset: 0x0003CA5A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.w, this.y);
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0003E879 File Offset: 0x0003CA79
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.w, this.z);
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001401 RID: 5121 RVA: 0x0003E898 File Offset: 0x0003CA98
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.w, this.w);
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001402 RID: 5122 RVA: 0x0003E8B7 File Offset: 0x0003CAB7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.x);
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001403 RID: 5123 RVA: 0x0003E8D6 File Offset: 0x0003CAD6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.y);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x0003E8F5 File Offset: 0x0003CAF5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.z);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0003E914 File Offset: 0x0003CB14
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x0003E933 File Offset: 0x0003CB33
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.x, this.w);
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

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0003E965 File Offset: 0x0003CB65
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.x);
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001408 RID: 5128 RVA: 0x0003E984 File Offset: 0x0003CB84
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.y);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0003E9A3 File Offset: 0x0003CBA3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.z);
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x0003E9C2 File Offset: 0x0003CBC2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.y, this.w);
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0003E9E1 File Offset: 0x0003CBE1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.x);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x0600140C RID: 5132 RVA: 0x0003EA00 File Offset: 0x0003CC00
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.y);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x0003EA1F File Offset: 0x0003CC1F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.z);
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x0600140E RID: 5134 RVA: 0x0003EA3E File Offset: 0x0003CC3E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.z, this.w);
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600140F RID: 5135 RVA: 0x0003EA5D File Offset: 0x0003CC5D
		// (set) Token: 0x06001410 RID: 5136 RVA: 0x0003EA7C File Offset: 0x0003CC7C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.w, this.x);
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

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06001411 RID: 5137 RVA: 0x0003EAAE File Offset: 0x0003CCAE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.w, this.y);
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06001412 RID: 5138 RVA: 0x0003EACD File Offset: 0x0003CCCD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.w, this.z);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x0003EAEC File Offset: 0x0003CCEC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.z, this.w, this.w);
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0003EB0B File Offset: 0x0003CD0B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.x, this.x);
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0003EB2A File Offset: 0x0003CD2A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.x, this.y);
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0003EB49 File Offset: 0x0003CD49
		// (set) Token: 0x06001417 RID: 5143 RVA: 0x0003EB68 File Offset: 0x0003CD68
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.x, this.z);
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

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0003EB9A File Offset: 0x0003CD9A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.x, this.w);
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0003EBB9 File Offset: 0x0003CDB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.y, this.x);
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x0003EBD8 File Offset: 0x0003CDD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.y, this.y);
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0003EBF7 File Offset: 0x0003CDF7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.y, this.z);
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0003EC16 File Offset: 0x0003CE16
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.y, this.w);
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0003EC35 File Offset: 0x0003CE35
		// (set) Token: 0x0600141E RID: 5150 RVA: 0x0003EC54 File Offset: 0x0003CE54
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.z, this.x);
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

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0003EC86 File Offset: 0x0003CE86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.z, this.y);
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x0003ECA5 File Offset: 0x0003CEA5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.z, this.z);
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x0003ECC4 File Offset: 0x0003CEC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.z, this.w);
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0003ECE3 File Offset: 0x0003CEE3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.w, this.x);
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0003ED02 File Offset: 0x0003CF02
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.w, this.y);
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0003ED21 File Offset: 0x0003CF21
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.w, this.z);
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0003ED40 File Offset: 0x0003CF40
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 ywww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.w, this.w, this.w);
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0003ED5F File Offset: 0x0003CF5F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.x);
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0003ED7E File Offset: 0x0003CF7E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.y);
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0003ED9D File Offset: 0x0003CF9D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.z);
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0003EDBC File Offset: 0x0003CFBC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.x, this.w);
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x0003EDDB File Offset: 0x0003CFDB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.x);
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0003EDFA File Offset: 0x0003CFFA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.y);
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0003EE19 File Offset: 0x0003D019
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.z);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0003EE38 File Offset: 0x0003D038
		// (set) Token: 0x0600142E RID: 5166 RVA: 0x0003EE57 File Offset: 0x0003D057
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.y, this.w);
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

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0003EE89 File Offset: 0x0003D089
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.x);
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0003EEA8 File Offset: 0x0003D0A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.y);
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0003EEC7 File Offset: 0x0003D0C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.z);
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0003EEE6 File Offset: 0x0003D0E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0003EF05 File Offset: 0x0003D105
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0003EF24 File Offset: 0x0003D124
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x0003EF43 File Offset: 0x0003D143
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.w, this.y);
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

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0003EF75 File Offset: 0x0003D175
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0003EF94 File Offset: 0x0003D194
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0003EFB3 File Offset: 0x0003D1B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0003EFD2 File Offset: 0x0003D1D2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x0003EFF1 File Offset: 0x0003D1F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.z);
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0003F010 File Offset: 0x0003D210
		// (set) Token: 0x0600143C RID: 5180 RVA: 0x0003F02F File Offset: 0x0003D22F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.x, this.w);
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

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0003F061 File Offset: 0x0003D261
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x0003F080 File Offset: 0x0003D280
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0003F09F File Offset: 0x0003D29F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0003F0BE File Offset: 0x0003D2BE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.y, this.w);
			}
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0003F0DD File Offset: 0x0003D2DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.x);
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0003F0FC File Offset: 0x0003D2FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.y);
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001443 RID: 5187 RVA: 0x0003F11B File Offset: 0x0003D31B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x0003F13A File Offset: 0x0003D33A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001445 RID: 5189 RVA: 0x0003F159 File Offset: 0x0003D359
		// (set) Token: 0x06001446 RID: 5190 RVA: 0x0003F178 File Offset: 0x0003D378
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.w, this.x);
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

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001447 RID: 5191 RVA: 0x0003F1AA File Offset: 0x0003D3AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x0003F1C9 File Offset: 0x0003D3C9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001449 RID: 5193 RVA: 0x0003F1E8 File Offset: 0x0003D3E8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x0003F207 File Offset: 0x0003D407
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0003F226 File Offset: 0x0003D426
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.y);
			}
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x0003F245 File Offset: 0x0003D445
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x0003F264 File Offset: 0x0003D464
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0003F283 File Offset: 0x0003D483
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.x);
			}
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600144F RID: 5199 RVA: 0x0003F2A2 File Offset: 0x0003D4A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0003F2C1 File Offset: 0x0003D4C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x0003F2E0 File Offset: 0x0003D4E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.y, this.w);
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x0003F2FF File Offset: 0x0003D4FF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x0003F31E File Offset: 0x0003D51E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0003F33D File Offset: 0x0003D53D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x0003F35C File Offset: 0x0003D55C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001456 RID: 5206 RVA: 0x0003F37B File Offset: 0x0003D57B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x0003F39A File Offset: 0x0003D59A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x0003F3B9 File Offset: 0x0003D5B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x0003F3D8 File Offset: 0x0003D5D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x0003F3F7 File Offset: 0x0003D5F7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x0003F416 File Offset: 0x0003D616
		// (set) Token: 0x0600145C RID: 5212 RVA: 0x0003F435 File Offset: 0x0003D635
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.x, this.y);
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

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x0003F467 File Offset: 0x0003D667
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x0003F486 File Offset: 0x0003D686
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x0003F4A5 File Offset: 0x0003D6A5
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x0003F4C4 File Offset: 0x0003D6C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.y, this.x);
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

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x0003F4F6 File Offset: 0x0003D6F6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.y, this.y);
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001462 RID: 5218 RVA: 0x0003F515 File Offset: 0x0003D715
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.y, this.z);
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001463 RID: 5219 RVA: 0x0003F534 File Offset: 0x0003D734
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.y, this.w);
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001464 RID: 5220 RVA: 0x0003F553 File Offset: 0x0003D753
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.z, this.x);
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001465 RID: 5221 RVA: 0x0003F572 File Offset: 0x0003D772
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.z, this.y);
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001466 RID: 5222 RVA: 0x0003F591 File Offset: 0x0003D791
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.z, this.z);
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001467 RID: 5223 RVA: 0x0003F5B0 File Offset: 0x0003D7B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001468 RID: 5224 RVA: 0x0003F5CF File Offset: 0x0003D7CF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001469 RID: 5225 RVA: 0x0003F5EE File Offset: 0x0003D7EE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600146A RID: 5226 RVA: 0x0003F60D File Offset: 0x0003D80D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600146B RID: 5227 RVA: 0x0003F62C File Offset: 0x0003D82C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 zwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.z, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0003F64B File Offset: 0x0003D84B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.x, this.x);
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x0003F66A File Offset: 0x0003D86A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.x, this.y);
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600146E RID: 5230 RVA: 0x0003F689 File Offset: 0x0003D889
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.x, this.z);
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x0003F6A8 File Offset: 0x0003D8A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.x, this.w);
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0003F6C7 File Offset: 0x0003D8C7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.y, this.x);
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x0003F6E6 File Offset: 0x0003D8E6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.y, this.y);
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0003F705 File Offset: 0x0003D905
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x0003F724 File Offset: 0x0003D924
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.y, this.z);
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

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001474 RID: 5236 RVA: 0x0003F756 File Offset: 0x0003D956
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.y, this.w);
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x0003F775 File Offset: 0x0003D975
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.z, this.x);
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x0003F794 File Offset: 0x0003D994
		// (set) Token: 0x06001477 RID: 5239 RVA: 0x0003F7B3 File Offset: 0x0003D9B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.z, this.y);
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

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0003F7E5 File Offset: 0x0003D9E5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.z, this.z);
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0003F804 File Offset: 0x0003DA04
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.z, this.w);
			}
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x0003F823 File Offset: 0x0003DA23
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.w, this.x);
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0003F842 File Offset: 0x0003DA42
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.w, this.y);
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0003F861 File Offset: 0x0003DA61
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.w, this.z);
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0003F880 File Offset: 0x0003DA80
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wxww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.x, this.w, this.w);
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x0003F89F File Offset: 0x0003DA9F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.x, this.x);
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x0003F8BE File Offset: 0x0003DABE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.x, this.y);
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x0003F8DD File Offset: 0x0003DADD
		// (set) Token: 0x06001481 RID: 5249 RVA: 0x0003F8FC File Offset: 0x0003DAFC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.x, this.z);
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

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x0003F92E File Offset: 0x0003DB2E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.x, this.w);
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x0003F94D File Offset: 0x0003DB4D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.y, this.x);
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x0003F96C File Offset: 0x0003DB6C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.y, this.y);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0003F98B File Offset: 0x0003DB8B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.y, this.z);
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0003F9AA File Offset: 0x0003DBAA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.y, this.w);
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001487 RID: 5255 RVA: 0x0003F9C9 File Offset: 0x0003DBC9
		// (set) Token: 0x06001488 RID: 5256 RVA: 0x0003F9E8 File Offset: 0x0003DBE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.z, this.x);
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

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0003FA1A File Offset: 0x0003DC1A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.z, this.y);
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x0003FA39 File Offset: 0x0003DC39
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.z, this.z);
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x0003FA58 File Offset: 0x0003DC58
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.z, this.w);
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x0003FA77 File Offset: 0x0003DC77
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.w, this.x);
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600148D RID: 5261 RVA: 0x0003FA96 File Offset: 0x0003DC96
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.w, this.y);
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x0003FAB5 File Offset: 0x0003DCB5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.w, this.z);
			}
		}

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x0003FAD4 File Offset: 0x0003DCD4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wyww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.y, this.w, this.w);
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x0003FAF3 File Offset: 0x0003DCF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.x, this.x);
			}
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001491 RID: 5265 RVA: 0x0003FB12 File Offset: 0x0003DD12
		// (set) Token: 0x06001492 RID: 5266 RVA: 0x0003FB31 File Offset: 0x0003DD31
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.x, this.y);
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

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001493 RID: 5267 RVA: 0x0003FB63 File Offset: 0x0003DD63
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.x, this.z);
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0003FB82 File Offset: 0x0003DD82
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.x, this.w);
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0003FBA1 File Offset: 0x0003DDA1
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x0003FBC0 File Offset: 0x0003DDC0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.y, this.x);
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

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0003FBF2 File Offset: 0x0003DDF2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.y, this.y);
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001498 RID: 5272 RVA: 0x0003FC11 File Offset: 0x0003DE11
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.y, this.z);
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001499 RID: 5273 RVA: 0x0003FC30 File Offset: 0x0003DE30
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.y, this.w);
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600149A RID: 5274 RVA: 0x0003FC4F File Offset: 0x0003DE4F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.z, this.x);
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x0003FC6E File Offset: 0x0003DE6E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.z, this.y);
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x0003FC8D File Offset: 0x0003DE8D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.z, this.z);
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x0003FCAC File Offset: 0x0003DEAC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.z, this.w);
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x0003FCCB File Offset: 0x0003DECB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.w, this.x);
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x0003FCEA File Offset: 0x0003DEEA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.w, this.y);
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x0003FD09 File Offset: 0x0003DF09
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.w, this.z);
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x0003FD28 File Offset: 0x0003DF28
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wzww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.z, this.w, this.w);
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x0003FD47 File Offset: 0x0003DF47
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.x, this.x);
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x0003FD66 File Offset: 0x0003DF66
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.x, this.y);
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x0003FD85 File Offset: 0x0003DF85
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.x, this.z);
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x0003FDA4 File Offset: 0x0003DFA4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.x, this.w);
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0003FDC3 File Offset: 0x0003DFC3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.y, this.x);
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060014A7 RID: 5287 RVA: 0x0003FDE2 File Offset: 0x0003DFE2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.y, this.y);
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060014A8 RID: 5288 RVA: 0x0003FE01 File Offset: 0x0003E001
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.y, this.z);
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x0003FE20 File Offset: 0x0003E020
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.y, this.w);
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060014AA RID: 5290 RVA: 0x0003FE3F File Offset: 0x0003E03F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.z, this.x);
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060014AB RID: 5291 RVA: 0x0003FE5E File Offset: 0x0003E05E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.z, this.y);
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060014AC RID: 5292 RVA: 0x0003FE7D File Offset: 0x0003E07D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.z, this.z);
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x0003FE9C File Offset: 0x0003E09C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.z, this.w);
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060014AE RID: 5294 RVA: 0x0003FEBB File Offset: 0x0003E0BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.w, this.x);
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060014AF RID: 5295 RVA: 0x0003FEDA File Offset: 0x0003E0DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.w, this.y);
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x0003FEF9 File Offset: 0x0003E0F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.w, this.z);
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0003FF18 File Offset: 0x0003E118
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 wwww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.w, this.w, this.w, this.w);
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0003FF37 File Offset: 0x0003E137
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.x);
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0003FF50 File Offset: 0x0003E150
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.y);
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0003FF69 File Offset: 0x0003E169
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.z);
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0003FF82 File Offset: 0x0003E182
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.w);
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0003FF9B File Offset: 0x0003E19B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.x);
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0003FFB4 File Offset: 0x0003E1B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.y);
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x0003FFCD File Offset: 0x0003E1CD
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x0003FFE6 File Offset: 0x0003E1E6
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

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060014BA RID: 5306 RVA: 0x0004000C File Offset: 0x0003E20C
		// (set) Token: 0x060014BB RID: 5307 RVA: 0x00040025 File Offset: 0x0003E225
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0004004B File Offset: 0x0003E24B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.z, this.x);
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x00040064 File Offset: 0x0003E264
		// (set) Token: 0x060014BE RID: 5310 RVA: 0x0004007D File Offset: 0x0003E27D
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

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x000400A3 File Offset: 0x0003E2A3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.z, this.z);
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x000400BC File Offset: 0x0003E2BC
		// (set) Token: 0x060014C1 RID: 5313 RVA: 0x000400D5 File Offset: 0x0003E2D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x000400FB File Offset: 0x0003E2FB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.w, this.x);
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060014C3 RID: 5315 RVA: 0x00040114 File Offset: 0x0003E314
		// (set) Token: 0x060014C4 RID: 5316 RVA: 0x0004012D File Offset: 0x0003E32D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060014C5 RID: 5317 RVA: 0x00040153 File Offset: 0x0003E353
		// (set) Token: 0x060014C6 RID: 5318 RVA: 0x0004016C File Offset: 0x0003E36C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060014C7 RID: 5319 RVA: 0x00040192 File Offset: 0x0003E392
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.w, this.w);
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x000401AB File Offset: 0x0003E3AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.x);
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060014C9 RID: 5321 RVA: 0x000401C4 File Offset: 0x0003E3C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.y);
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060014CA RID: 5322 RVA: 0x000401DD File Offset: 0x0003E3DD
		// (set) Token: 0x060014CB RID: 5323 RVA: 0x000401F6 File Offset: 0x0003E3F6
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

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060014CC RID: 5324 RVA: 0x0004021C File Offset: 0x0003E41C
		// (set) Token: 0x060014CD RID: 5325 RVA: 0x00040235 File Offset: 0x0003E435
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060014CE RID: 5326 RVA: 0x0004025B File Offset: 0x0003E45B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.x);
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x00040274 File Offset: 0x0003E474
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.y);
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060014D0 RID: 5328 RVA: 0x0004028D File Offset: 0x0003E48D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.z);
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x000402A6 File Offset: 0x0003E4A6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.w);
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x000402BF File Offset: 0x0003E4BF
		// (set) Token: 0x060014D3 RID: 5331 RVA: 0x000402D8 File Offset: 0x0003E4D8
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

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060014D4 RID: 5332 RVA: 0x000402FE File Offset: 0x0003E4FE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.z, this.y);
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00040317 File Offset: 0x0003E517
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.z, this.z);
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x00040330 File Offset: 0x0003E530
		// (set) Token: 0x060014D7 RID: 5335 RVA: 0x00040349 File Offset: 0x0003E549
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.z = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060014D8 RID: 5336 RVA: 0x0004036F File Offset: 0x0003E56F
		// (set) Token: 0x060014D9 RID: 5337 RVA: 0x00040388 File Offset: 0x0003E588
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 ywx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x000403AE File Offset: 0x0003E5AE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 ywy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.w, this.y);
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060014DB RID: 5339 RVA: 0x000403C7 File Offset: 0x0003E5C7
		// (set) Token: 0x060014DC RID: 5340 RVA: 0x000403E0 File Offset: 0x0003E5E0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 ywz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x060014DD RID: 5341 RVA: 0x00040406 File Offset: 0x0003E606
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.w, this.w);
			}
		}

		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x060014DE RID: 5342 RVA: 0x0004041F File Offset: 0x0003E61F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.x, this.x);
			}
		}

		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x060014DF RID: 5343 RVA: 0x00040438 File Offset: 0x0003E638
		// (set) Token: 0x060014E0 RID: 5344 RVA: 0x00040451 File Offset: 0x0003E651
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

		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00040477 File Offset: 0x0003E677
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.x, this.z);
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00040490 File Offset: 0x0003E690
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x000404A9 File Offset: 0x0003E6A9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.x = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x000404CF File Offset: 0x0003E6CF
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x000404E8 File Offset: 0x0003E6E8
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

		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0004050E File Offset: 0x0003E70E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.y, this.y);
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060014E7 RID: 5351 RVA: 0x00040527 File Offset: 0x0003E727
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.y, this.z);
			}
		}

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x00040540 File Offset: 0x0003E740
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x00040559 File Offset: 0x0003E759
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.y = value.y;
				this.w = value.z;
			}
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x0004057F File Offset: 0x0003E77F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.x);
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x00040598 File Offset: 0x0003E798
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.y);
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x000405B1 File Offset: 0x0003E7B1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.z);
			}
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x060014ED RID: 5357 RVA: 0x000405CA File Offset: 0x0003E7CA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.z, this.w);
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060014EE RID: 5358 RVA: 0x000405E3 File Offset: 0x0003E7E3
		// (set) Token: 0x060014EF RID: 5359 RVA: 0x000405FC File Offset: 0x0003E7FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00040622 File Offset: 0x0003E822
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x0004063B File Offset: 0x0003E83B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00040661 File Offset: 0x0003E861
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.w, this.z);
			}
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x0004067A File Offset: 0x0003E87A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 zww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.z, this.w, this.w);
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00040693 File Offset: 0x0003E893
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.x, this.x);
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x000406AC File Offset: 0x0003E8AC
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x000406C5 File Offset: 0x0003E8C5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x000406EB File Offset: 0x0003E8EB
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x00040704 File Offset: 0x0003E904
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wxz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.x, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x0004072A File Offset: 0x0003E92A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wxw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.x, this.w);
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00040743 File Offset: 0x0003E943
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x0004075C File Offset: 0x0003E95C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00040782 File Offset: 0x0003E982
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.y, this.y);
			}
		}

		// Token: 0x170005AA RID: 1450
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0004079B File Offset: 0x0003E99B
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x000407B4 File Offset: 0x0003E9B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wyz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.y, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
				this.z = value.z;
			}
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x000407DA File Offset: 0x0003E9DA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wyw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.y, this.w);
			}
		}

		// Token: 0x170005AC RID: 1452
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x000407F3 File Offset: 0x0003E9F3
		// (set) Token: 0x06001501 RID: 5377 RVA: 0x0004080C File Offset: 0x0003EA0C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wzx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.z, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.x = value.z;
			}
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x00040832 File Offset: 0x0003EA32
		// (set) Token: 0x06001503 RID: 5379 RVA: 0x0004084B File Offset: 0x0003EA4B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wzy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.z, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
				this.y = value.z;
			}
		}

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00040871 File Offset: 0x0003EA71
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wzz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.z, this.z);
			}
		}

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x0004088A File Offset: 0x0003EA8A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wzw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.z, this.w);
			}
		}

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x000408A3 File Offset: 0x0003EAA3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wwx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.w, this.x);
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x000408BC File Offset: 0x0003EABC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wwy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.w, this.y);
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x000408D5 File Offset: 0x0003EAD5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 wwz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.w, this.z);
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x000408EE File Offset: 0x0003EAEE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 www
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.w, this.w, this.w);
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00040907 File Offset: 0x0003EB07
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.x);
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0004091A File Offset: 0x0003EB1A
		// (set) Token: 0x0600150C RID: 5388 RVA: 0x0004092D File Offset: 0x0003EB2D
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

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x00040947 File Offset: 0x0003EB47
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x0004095A File Offset: 0x0003EB5A
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

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00040974 File Offset: 0x0003EB74
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x00040987 File Offset: 0x0003EB87
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x000409A1 File Offset: 0x0003EBA1
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x000409B4 File Offset: 0x0003EBB4
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

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x000409CE File Offset: 0x0003EBCE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.y);
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x000409E1 File Offset: 0x0003EBE1
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x000409F4 File Offset: 0x0003EBF4
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

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001516 RID: 5398 RVA: 0x00040A0E File Offset: 0x0003EC0E
		// (set) Token: 0x06001517 RID: 5399 RVA: 0x00040A21 File Offset: 0x0003EC21
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06001518 RID: 5400 RVA: 0x00040A3B File Offset: 0x0003EC3B
		// (set) Token: 0x06001519 RID: 5401 RVA: 0x00040A4E File Offset: 0x0003EC4E
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

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00040A68 File Offset: 0x0003EC68
		// (set) Token: 0x0600151B RID: 5403 RVA: 0x00040A7B File Offset: 0x0003EC7B
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

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x00040A95 File Offset: 0x0003EC95
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 zz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.z, this.z);
			}
		}

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x00040AA8 File Offset: 0x0003ECA8
		// (set) Token: 0x0600151E RID: 5406 RVA: 0x00040ABB File Offset: 0x0003ECBB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 zw
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.z, this.w);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.z = value.x;
				this.w = value.y;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x00040AD5 File Offset: 0x0003ECD5
		// (set) Token: 0x06001520 RID: 5408 RVA: 0x00040AE8 File Offset: 0x0003ECE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 wx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.w, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00040B02 File Offset: 0x0003ED02
		// (set) Token: 0x06001522 RID: 5410 RVA: 0x00040B15 File Offset: 0x0003ED15
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 wy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.w, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00040B2F File Offset: 0x0003ED2F
		// (set) Token: 0x06001524 RID: 5412 RVA: 0x00040B42 File Offset: 0x0003ED42
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 wz
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.w, this.z);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.w = value.x;
				this.z = value.y;
			}
		}

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00040B5C File Offset: 0x0003ED5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 ww
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.w, this.w);
			}
		}

		// Token: 0x170005C4 RID: 1476
		public unsafe float this[int index]
		{
			get
			{
				fixed (float4* ptr = &this)
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

		// Token: 0x06001528 RID: 5416 RVA: 0x00040BA8 File Offset: 0x0003EDA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float4 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y && this.z == rhs.z && this.w == rhs.w;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00040BE4 File Offset: 0x0003EDE4
		public override bool Equals(object o)
		{
			if (o is float4)
			{
				float4 converted = (float4)o;
				return this.Equals(converted);
			}
			return false;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00040C09 File Offset: 0x0003EE09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x00040C18 File Offset: 0x0003EE18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float4({0}f, {1}f, {2}f, {3}f)", new object[] { this.x, this.y, this.z, this.w });
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00040C70 File Offset: 0x0003EE70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float4({0}f, {1}f, {2}f, {3}f)", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00040CCD File Offset: 0x0003EECD
		public static implicit operator float4(Vector4 v)
		{
			return new float4(v.x, v.y, v.z, v.w);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00040CEC File Offset: 0x0003EEEC
		public static implicit operator Vector4(float4 v)
		{
			return new Vector4(v.x, v.y, v.z, v.w);
		}

		// Token: 0x040000CC RID: 204
		public float x;

		// Token: 0x040000CD RID: 205
		public float y;

		// Token: 0x040000CE RID: 206
		public float z;

		// Token: 0x040000CF RID: 207
		public float w;

		// Token: 0x040000D0 RID: 208
		public static readonly float4 zero;

		// Token: 0x02000036 RID: 54
		internal sealed class DebuggerProxy
		{
			// Token: 0x0600152F RID: 5423 RVA: 0x00040D0B File Offset: 0x0003EF0B
			public DebuggerProxy(float4 v)
			{
				this.x = v.x;
				this.y = v.y;
				this.z = v.z;
				this.w = v.w;
			}

			// Token: 0x040000D1 RID: 209
			public float x;

			// Token: 0x040000D2 RID: 210
			public float y;

			// Token: 0x040000D3 RID: 211
			public float z;

			// Token: 0x040000D4 RID: 212
			public float w;
		}
	}
}
