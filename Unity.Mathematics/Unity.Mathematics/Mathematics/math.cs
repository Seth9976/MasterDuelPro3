using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000009 RID: 9
	[Il2CppEagerStaticClassConstruction]
	public static class math
	{
		// Token: 0x0600003B RID: 59 RVA: 0x0000801C File Offset: 0x0000621C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AffineTransform AffineTransform(float3 translation, quaternion rotation)
		{
			return new AffineTransform(translation, rotation);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00008025 File Offset: 0x00006225
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AffineTransform AffineTransform(float3 translation, quaternion rotation, float3 scale)
		{
			return new AffineTransform(translation, rotation, scale);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000802F File Offset: 0x0000622F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AffineTransform AffineTransform(float3 translation, float3x3 rotationScale)
		{
			return new AffineTransform(translation, rotationScale);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00008038 File Offset: 0x00006238
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AffineTransform AffineTransform(float3x3 rotationScale)
		{
			return new AffineTransform(rotationScale);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00008040 File Offset: 0x00006240
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AffineTransform AffineTransform(float4x4 m)
		{
			return new AffineTransform(m);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00008048 File Offset: 0x00006248
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AffineTransform AffineTransform(float3x4 m)
		{
			return new AffineTransform(m);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00008050 File Offset: 0x00006250
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AffineTransform AffineTransform(RigidTransform rigid)
		{
			return new AffineTransform(rigid);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00008058 File Offset: 0x00006258
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(AffineTransform transform)
		{
			return math.float4x4(math.float4(transform.rs.c0, 0f), math.float4(transform.rs.c1, 0f), math.float4(transform.rs.c2, 0f), math.float4(transform.t, 1f));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00007C9A File Offset: 0x00005E9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(AffineTransform transform)
		{
			return math.float3x4(transform.rs.c0, transform.rs.c1, transform.rs.c2, transform.t);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000080B9 File Offset: 0x000062B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AffineTransform mul(AffineTransform a, AffineTransform b)
		{
			return new AffineTransform(math.transform(a, b.t), math.mul(a.rs, b.rs));
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000080DD File Offset: 0x000062DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AffineTransform mul(float3x3 a, AffineTransform b)
		{
			return new AffineTransform(math.mul(a, b.t), math.mul(a, b.rs));
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000080FC File Offset: 0x000062FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AffineTransform mul(AffineTransform a, float3x3 b)
		{
			return new AffineTransform(a.t, math.mul(b, a.rs));
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00008115 File Offset: 0x00006315
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(AffineTransform a, float4 pos)
		{
			return math.float4(math.mul(a.rs, pos.xyz) + a.t * pos.w, pos.w);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000814A File Offset: 0x0000634A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 rotate(AffineTransform a, float3 dir)
		{
			return math.mul(a.rs, dir);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00008158 File Offset: 0x00006358
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 transform(AffineTransform a, float3 pos)
		{
			return a.t + math.mul(a.rs, pos);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00008174 File Offset: 0x00006374
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AffineTransform inverse(AffineTransform a)
		{
			AffineTransform inv;
			inv.rs = math.pseudoinverse(a.rs);
			inv.t = math.mul(inv.rs, -a.t);
			return inv;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000081B4 File Offset: 0x000063B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void decompose(AffineTransform a, out float3 translation, out quaternion rotation, out float3 scale)
		{
			translation = a.t;
			rotation = math.rotation(a.rs);
			float3x3 sm = math.mul(math.float3x3(math.conjugate(rotation)), a.rs);
			scale = math.float3(sm.c0.x, sm.c1.y, sm.c2.z);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00008226 File Offset: 0x00006426
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(AffineTransform a)
		{
			return math.hash(a.rs) + 3318036811U * math.hash(a.t);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00008248 File Offset: 0x00006448
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(AffineTransform a)
		{
			return math.hashwide(a.rs).xyzz + 3318036811U * math.hashwide(a.t).xyzz;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000828A File Offset: 0x0000648A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 bool2(bool x, bool y)
		{
			return new bool2(x, y);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00008293 File Offset: 0x00006493
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 bool2(bool2 xy)
		{
			return new bool2(xy);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000829B File Offset: 0x0000649B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 bool2(bool v)
		{
			return new bool2(v);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000082A3 File Offset: 0x000064A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool2 v)
		{
			return math.csum(math.select(math.uint2(2426570171U, 1561977301U), math.uint2(4205774813U, 1650214333U), v));
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000082CE File Offset: 0x000064CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(bool2 v)
		{
			return math.select(math.uint2(3388112843U, 1831150513U), math.uint2(1848374953U, 3430200247U), v);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000082F4 File Offset: 0x000064F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool shuffle(bool2 left, bool2 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000082FE File Offset: 0x000064FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 shuffle(bool2 left, bool2 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.bool2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00008315 File Offset: 0x00006515
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 shuffle(bool2 left, bool2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.bool3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00008335 File Offset: 0x00006535
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 shuffle(bool2 left, bool2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.bool4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00008360 File Offset: 0x00006560
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool select_shuffle_component(bool2 a, bool2 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000083C5 File Offset: 0x000065C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 bool2x2(bool2 c0, bool2 c1)
		{
			return new bool2x2(c0, c1);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000083CE File Offset: 0x000065CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 bool2x2(bool m00, bool m01, bool m10, bool m11)
		{
			return new bool2x2(m00, m01, m10, m11);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000083D9 File Offset: 0x000065D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 bool2x2(bool v)
		{
			return new bool2x2(v);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000083E1 File Offset: 0x000065E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x2 transpose(bool2x2 v)
		{
			return math.bool2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00008414 File Offset: 0x00006614
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool2x2 v)
		{
			return math.csum(math.select(math.uint2(2062756937U, 2920485769U), math.uint2(1562056283U, 2265541847U), v.c0) + math.select(math.uint2(1283419601U, 1210229737U), math.uint2(2864955997U, 3525118277U), v.c1));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00008480 File Offset: 0x00006680
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(bool2x2 v)
		{
			return math.select(math.uint2(2298260269U, 1632478733U), math.uint2(1537393931U, 2353355467U), v.c0) + math.select(math.uint2(3441847433U, 4052036147U), math.uint2(2011389559U, 2252224297U), v.c1);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000084E4 File Offset: 0x000066E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 bool2x3(bool2 c0, bool2 c1, bool2 c2)
		{
			return new bool2x3(c0, c1, c2);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000084EE File Offset: 0x000066EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 bool2x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12)
		{
			return new bool2x3(m00, m01, m02, m10, m11, m12);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000084FD File Offset: 0x000066FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 bool2x3(bool v)
		{
			return new bool2x3(v);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00008508 File Offset: 0x00006708
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 transpose(bool2x3 v)
		{
			return math.bool3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000855C File Offset: 0x0000675C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool2x3 v)
		{
			return math.csum(math.select(math.uint2(2078515003U, 4206465343U), math.uint2(3025146473U, 3763046909U), v.c0) + math.select(math.uint2(3678265601U, 2070747979U), math.uint2(1480171127U, 1588341193U), v.c1) + math.select(math.uint2(4234155257U, 1811310911U), math.uint2(2635799963U, 4165137857U), v.c2));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000085F4 File Offset: 0x000067F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(bool2x3 v)
		{
			return math.select(math.uint2(2759770933U, 2759319383U), math.uint2(3299952959U, 3121178323U), v.c0) + math.select(math.uint2(2948522579U, 1531026433U), math.uint2(1365086453U, 3969870067U), v.c1) + math.select(math.uint2(4192899797U, 3271228601U), math.uint2(1634639009U, 3318036811U), v.c2);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00008686 File Offset: 0x00006886
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 bool2x4(bool2 c0, bool2 c1, bool2 c2, bool2 c3)
		{
			return new bool2x4(c0, c1, c2, c3);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00008691 File Offset: 0x00006891
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 bool2x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13)
		{
			return new bool2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000086A4 File Offset: 0x000068A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 bool2x4(bool v)
		{
			return new bool2x4(v);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000086AC File Offset: 0x000068AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 transpose(bool2x4 v)
		{
			return math.bool4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00008718 File Offset: 0x00006918
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool2x4 v)
		{
			return math.csum(math.select(math.uint2(1168253063U, 4228926523U), math.uint2(1610574617U, 1584185147U), v.c0) + math.select(math.uint2(3041325733U, 3150930919U), math.uint2(3309258581U, 1770373673U), v.c1) + math.select(math.uint2(3778261171U, 3286279097U), math.uint2(4264629071U, 1898591447U), v.c2) + math.select(math.uint2(2641864091U, 1229113913U), math.uint2(3020867117U, 1449055807U), v.c3));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000087E0 File Offset: 0x000069E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(bool2x4 v)
		{
			return math.select(math.uint2(2479033387U, 3702457169U), math.uint2(1845824257U, 1963973621U), v.c0) + math.select(math.uint2(2134758553U, 1391111867U), math.uint2(1167706003U, 2209736489U), v.c1) + math.select(math.uint2(3261535807U, 1740411209U), math.uint2(2910609089U, 2183822701U), v.c2) + math.select(math.uint2(3029516053U, 3547472099U), math.uint2(2057487037U, 3781937309U), v.c3);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000088A0 File Offset: 0x00006AA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 bool3(bool x, bool y, bool z)
		{
			return new bool3(x, y, z);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000088AA File Offset: 0x00006AAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 bool3(bool x, bool2 yz)
		{
			return new bool3(x, yz);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000088B3 File Offset: 0x00006AB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 bool3(bool2 xy, bool z)
		{
			return new bool3(xy, z);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000088BC File Offset: 0x00006ABC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 bool3(bool3 xyz)
		{
			return new bool3(xyz);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000088C4 File Offset: 0x00006AC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 bool3(bool v)
		{
			return new bool3(v);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000088CC File Offset: 0x00006ACC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool3 v)
		{
			return math.csum(math.select(math.uint3(2716413241U, 1166264321U, 2503385333U), math.uint3(2944493077U, 2599999021U, 3814721321U), v));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00008901 File Offset: 0x00006B01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(bool3 v)
		{
			return math.select(math.uint3(1595355149U, 1728931849U, 2062756937U), math.uint3(2920485769U, 1562056283U, 2265541847U), v);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00008931 File Offset: 0x00006B31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool shuffle(bool3 left, bool3 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000893B File Offset: 0x00006B3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 shuffle(bool3 left, bool3 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.bool2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00008952 File Offset: 0x00006B52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 shuffle(bool3 left, bool3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.bool3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00008972 File Offset: 0x00006B72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 shuffle(bool3 left, bool3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.bool4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000899C File Offset: 0x00006B9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool select_shuffle_component(bool3 a, bool3 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00008A13 File Offset: 0x00006C13
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 bool3x2(bool3 c0, bool3 c1)
		{
			return new bool3x2(c0, c1);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00008A1C File Offset: 0x00006C1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 bool3x2(bool m00, bool m01, bool m10, bool m11, bool m20, bool m21)
		{
			return new bool3x2(m00, m01, m10, m11, m20, m21);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00008A2B File Offset: 0x00006C2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x2 bool3x2(bool v)
		{
			return new bool3x2(v);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00008A34 File Offset: 0x00006C34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x3 transpose(bool3x2 v)
		{
			return math.bool2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00008A88 File Offset: 0x00006C88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool3x2 v)
		{
			return math.csum(math.select(math.uint3(2627668003U, 1520214331U, 2949502447U), math.uint3(2827819133U, 3480140317U, 2642994593U), v.c0) + math.select(math.uint3(3940484981U, 1954192763U, 1091696537U), math.uint3(3052428017U, 4253034763U, 2338696631U), v.c1));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00008B08 File Offset: 0x00006D08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(bool3x2 v)
		{
			return math.select(math.uint3(3757372771U, 1885959949U, 3508684087U), math.uint3(3919501043U, 1209161033U, 4007793211U), v.c0) + math.select(math.uint3(3819806693U, 3458005183U, 2078515003U), math.uint3(4206465343U, 3025146473U, 3763046909U), v.c1);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00008B80 File Offset: 0x00006D80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 bool3x3(bool3 c0, bool3 c1, bool3 c2)
		{
			return new bool3x3(c0, c1, c2);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00008B8C File Offset: 0x00006D8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 bool3x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12, bool m20, bool m21, bool m22)
		{
			return new bool3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00008BAC File Offset: 0x00006DAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 bool3x3(bool v)
		{
			return new bool3x3(v);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00008BB4 File Offset: 0x00006DB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x3 transpose(bool3x3 v)
		{
			return math.bool3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00008C2C File Offset: 0x00006E2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool3x3 v)
		{
			return math.csum(math.select(math.uint3(3881277847U, 4017968839U, 1727237899U), math.uint3(1648514723U, 1385344481U, 3538260197U), v.c0) + math.select(math.uint3(4066109527U, 2613148903U, 3367528529U), math.uint3(1678332449U, 2918459647U, 2744611081U), v.c1) + math.select(math.uint3(1952372791U, 2631698677U, 4200781601U), math.uint3(2119021007U, 1760485621U, 3157985881U), v.c2));
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00008CE4 File Offset: 0x00006EE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(bool3x3 v)
		{
			return math.select(math.uint3(2171534173U, 2723054263U, 1168253063U), math.uint3(4228926523U, 1610574617U, 1584185147U), v.c0) + math.select(math.uint3(3041325733U, 3150930919U, 3309258581U), math.uint3(1770373673U, 3778261171U, 3286279097U), v.c1) + math.select(math.uint3(4264629071U, 1898591447U, 2641864091U), math.uint3(1229113913U, 3020867117U, 1449055807U), v.c2);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00008D94 File Offset: 0x00006F94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 bool3x4(bool3 c0, bool3 c1, bool3 c2, bool3 c3)
		{
			return new bool3x4(c0, c1, c2, c3);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00008DA0 File Offset: 0x00006FA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 bool3x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13, bool m20, bool m21, bool m22, bool m23)
		{
			return new bool3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00008DC6 File Offset: 0x00006FC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 bool3x4(bool v)
		{
			return new bool3x4(v);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00008DD0 File Offset: 0x00006FD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 transpose(bool3x4 v)
		{
			return math.bool4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00008E68 File Offset: 0x00007068
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool3x4 v)
		{
			return math.csum(math.select(math.uint3(2209710647U, 2201894441U, 2849577407U), math.uint3(3287031191U, 3098675399U, 1564399943U), v.c0) + math.select(math.uint3(1148435377U, 3416333663U, 1750611407U), math.uint3(3285396193U, 3110507567U, 4271396531U), v.c1) + math.select(math.uint3(4198118021U, 2908068253U, 3705492289U), math.uint3(2497566569U, 2716413241U, 1166264321U), v.c2) + math.select(math.uint3(2503385333U, 2944493077U, 2599999021U), math.uint3(3814721321U, 1595355149U, 1728931849U), v.c3));
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00008F58 File Offset: 0x00007158
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(bool3x4 v)
		{
			return math.select(math.uint3(2062756937U, 2920485769U, 1562056283U), math.uint3(2265541847U, 1283419601U, 1210229737U), v.c0) + math.select(math.uint3(2864955997U, 3525118277U, 2298260269U), math.uint3(1632478733U, 1537393931U, 2353355467U), v.c1) + math.select(math.uint3(3441847433U, 4052036147U, 2011389559U), math.uint3(2252224297U, 3784421429U, 1750626223U), v.c2) + math.select(math.uint3(3571447507U, 3412283213U, 2601761069U), math.uint3(1254033427U, 2248573027U, 3612677113U), v.c3);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00009040 File Offset: 0x00007240
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool x, bool y, bool z, bool w)
		{
			return new bool4(x, y, z, w);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000904B File Offset: 0x0000724B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool x, bool y, bool2 zw)
		{
			return new bool4(x, y, zw);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00009055 File Offset: 0x00007255
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool x, bool2 yz, bool w)
		{
			return new bool4(x, yz, w);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000905F File Offset: 0x0000725F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool x, bool3 yzw)
		{
			return new bool4(x, yzw);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00009068 File Offset: 0x00007268
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool2 xy, bool z, bool w)
		{
			return new bool4(xy, z, w);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00009072 File Offset: 0x00007272
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool2 xy, bool2 zw)
		{
			return new bool4(xy, zw);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000907B File Offset: 0x0000727B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool3 xyz, bool w)
		{
			return new bool4(xyz, w);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00009084 File Offset: 0x00007284
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool4 xyzw)
		{
			return new bool4(xyzw);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000908C File Offset: 0x0000728C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 bool4(bool v)
		{
			return new bool4(v);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00009094 File Offset: 0x00007294
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool4 v)
		{
			return math.csum(math.select(math.uint4(1610574617U, 1584185147U, 3041325733U, 3150930919U), math.uint4(3309258581U, 1770373673U, 3778261171U, 3286279097U), v));
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000090D3 File Offset: 0x000072D3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(bool4 v)
		{
			return math.select(math.uint4(4264629071U, 1898591447U, 2641864091U, 1229113913U), math.uint4(3020867117U, 1449055807U, 2479033387U, 3702457169U), v);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000910D File Offset: 0x0000730D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool shuffle(bool4 left, bool4 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00009117 File Offset: 0x00007317
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 shuffle(bool4 left, bool4 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.bool2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000912E File Offset: 0x0000732E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 shuffle(bool4 left, bool4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.bool3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000914E File Offset: 0x0000734E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 shuffle(bool4 left, bool4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.bool4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00009178 File Offset: 0x00007378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool select_shuffle_component(bool4 a, bool4 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.LeftW:
				return a.w;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			case math.ShuffleComponent.RightW:
				return b.w;
			default:
				throw new ArgumentException("Invalid shuffle component: " + component.ToString());
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00009201 File Offset: 0x00007401
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 bool4x2(bool4 c0, bool4 c1)
		{
			return new bool4x2(c0, c1);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000920A File Offset: 0x0000740A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 bool4x2(bool m00, bool m01, bool m10, bool m11, bool m20, bool m21, bool m30, bool m31)
		{
			return new bool4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000921D File Offset: 0x0000741D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x2 bool4x2(bool v)
		{
			return new bool4x2(v);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00009228 File Offset: 0x00007428
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2x4 transpose(bool4x2 v)
		{
			return math.bool2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00009294 File Offset: 0x00007494
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool4x2 v)
		{
			return math.csum(math.select(math.uint4(3516359879U, 3050356579U, 4178586719U, 2558655391U), math.uint4(1453413133U, 2152428077U, 1938706661U, 1338588197U), v.c0) + math.select(math.uint4(3439609253U, 3535343003U, 3546061613U, 2702024231U), math.uint4(1452124841U, 1966089551U, 2668168249U, 1587512777U), v.c1));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00009328 File Offset: 0x00007528
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(bool4x2 v)
		{
			return math.select(math.uint4(2353831999U, 3101256173U, 2891822459U, 2837054189U), math.uint4(3016004371U, 4097481403U, 2229788699U, 2382715877U), v.c0) + math.select(math.uint4(1851936439U, 1938025801U, 3712598587U, 3956330501U), math.uint4(2437373431U, 1441286183U, 2426570171U, 1561977301U), v.c1);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000093B4 File Offset: 0x000075B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 bool4x3(bool4 c0, bool4 c1, bool4 c2)
		{
			return new bool4x3(c0, c1, c2);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000093C0 File Offset: 0x000075C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 bool4x3(bool m00, bool m01, bool m02, bool m10, bool m11, bool m12, bool m20, bool m21, bool m22, bool m30, bool m31, bool m32)
		{
			return new bool4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000093E6 File Offset: 0x000075E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x3 bool4x3(bool v)
		{
			return new bool4x3(v);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000093F0 File Offset: 0x000075F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3x4 transpose(bool4x3 v)
		{
			return math.bool3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00009488 File Offset: 0x00007688
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool4x3 v)
		{
			return math.csum(math.select(math.uint4(3940484981U, 1954192763U, 1091696537U, 3052428017U), math.uint4(4253034763U, 2338696631U, 3757372771U, 1885959949U), v.c0) + math.select(math.uint4(3508684087U, 3919501043U, 1209161033U, 4007793211U), math.uint4(3819806693U, 3458005183U, 2078515003U, 4206465343U), v.c1) + math.select(math.uint4(3025146473U, 3763046909U, 3678265601U, 2070747979U), math.uint4(1480171127U, 1588341193U, 4234155257U, 1811310911U), v.c2));
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000955C File Offset: 0x0000775C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(bool4x3 v)
		{
			return math.select(math.uint4(2635799963U, 4165137857U, 2759770933U, 2759319383U), math.uint4(3299952959U, 3121178323U, 2948522579U, 1531026433U), v.c0) + math.select(math.uint4(1365086453U, 3969870067U, 4192899797U, 3271228601U), math.uint4(1634639009U, 3318036811U, 3404170631U, 2048213449U), v.c1) + math.select(math.uint4(4164671783U, 1780759499U, 1352369353U, 2446407751U), math.uint4(1391928079U, 3475533443U, 3777095341U, 3385463369U), v.c2);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000962A File Offset: 0x0000782A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 bool4x4(bool4 c0, bool4 c1, bool4 c2, bool4 c3)
		{
			return new bool4x4(c0, c1, c2, c3);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00009638 File Offset: 0x00007838
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 bool4x4(bool m00, bool m01, bool m02, bool m03, bool m10, bool m11, bool m12, bool m13, bool m20, bool m21, bool m22, bool m23, bool m30, bool m31, bool m32, bool m33)
		{
			return new bool4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00009666 File Offset: 0x00007866
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 bool4x4(bool v)
		{
			return new bool4x4(v);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00009670 File Offset: 0x00007870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4x4 transpose(bool4x4 v)
		{
			return math.bool4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00009734 File Offset: 0x00007934
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(bool4x4 v)
		{
			return math.csum(math.select(math.uint4(3516359879U, 3050356579U, 4178586719U, 2558655391U), math.uint4(1453413133U, 2152428077U, 1938706661U, 1338588197U), v.c0) + math.select(math.uint4(3439609253U, 3535343003U, 3546061613U, 2702024231U), math.uint4(1452124841U, 1966089551U, 2668168249U, 1587512777U), v.c1) + math.select(math.uint4(2353831999U, 3101256173U, 2891822459U, 2837054189U), math.uint4(3016004371U, 4097481403U, 2229788699U, 2382715877U), v.c2) + math.select(math.uint4(1851936439U, 1938025801U, 3712598587U, 3956330501U), math.uint4(2437373431U, 1441286183U, 2426570171U, 1561977301U), v.c3));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000984C File Offset: 0x00007A4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(bool4x4 v)
		{
			return math.select(math.uint4(4205774813U, 1650214333U, 3388112843U, 1831150513U), math.uint4(1848374953U, 3430200247U, 2209710647U, 2201894441U), v.c0) + math.select(math.uint4(2849577407U, 3287031191U, 3098675399U, 1564399943U), math.uint4(1148435377U, 3416333663U, 1750611407U, 3285396193U), v.c1) + math.select(math.uint4(3110507567U, 4271396531U, 4198118021U, 2908068253U), math.uint4(3705492289U, 2497566569U, 2716413241U, 1166264321U), v.c2) + math.select(math.uint4(2503385333U, 2944493077U, 2599999021U, 3814721321U), math.uint4(1595355149U, 1728931849U, 2062756937U, 2920485769U), v.c3);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000995C File Offset: 0x00007B5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(double x, double y)
		{
			return new double2(x, y);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00009965 File Offset: 0x00007B65
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(double2 xy)
		{
			return new double2(xy);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000996D File Offset: 0x00007B6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(double v)
		{
			return new double2(v);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00009975 File Offset: 0x00007B75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(bool v)
		{
			return new double2(v);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000997D File Offset: 0x00007B7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(bool2 v)
		{
			return new double2(v);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00009985 File Offset: 0x00007B85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(int v)
		{
			return new double2(v);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000998D File Offset: 0x00007B8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(int2 v)
		{
			return new double2(v);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00009995 File Offset: 0x00007B95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(uint v)
		{
			return new double2(v);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000999D File Offset: 0x00007B9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(uint2 v)
		{
			return new double2(v);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000099A5 File Offset: 0x00007BA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(half v)
		{
			return new double2(v);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000099AD File Offset: 0x00007BAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(half2 v)
		{
			return new double2(v);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000099B5 File Offset: 0x00007BB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(float v)
		{
			return new double2(v);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000099BD File Offset: 0x00007BBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 double2(float2 v)
		{
			return new double2(v);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000099C5 File Offset: 0x00007BC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double2 v)
		{
			return math.csum(math.fold_to_uint(v) * math.uint2(2503385333U, 2944493077U)) + 2599999021U;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000099EC File Offset: 0x00007BEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(double2 v)
		{
			return math.fold_to_uint(v) * math.uint2(3814721321U, 1595355149U) + 1728931849U;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00009A12 File Offset: 0x00007C12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double shuffle(double2 left, double2 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00009A1C File Offset: 0x00007C1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 shuffle(double2 left, double2 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.double2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00009A33 File Offset: 0x00007C33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 shuffle(double2 left, double2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.double3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00009A53 File Offset: 0x00007C53
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 shuffle(double2 left, double2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.double4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00009A7C File Offset: 0x00007C7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double select_shuffle_component(double2 a, double2 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00009AE1 File Offset: 0x00007CE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(double2 c0, double2 c1)
		{
			return new double2x2(c0, c1);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00009AEA File Offset: 0x00007CEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(double m00, double m01, double m10, double m11)
		{
			return new double2x2(m00, m01, m10, m11);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00009AF5 File Offset: 0x00007CF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(double v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00009AFD File Offset: 0x00007CFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(bool v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00009B05 File Offset: 0x00007D05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(bool2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00009B0D File Offset: 0x00007D0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(int v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00009B15 File Offset: 0x00007D15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(int2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00009B1D File Offset: 0x00007D1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(uint v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00009B25 File Offset: 0x00007D25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(uint2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00009B2D File Offset: 0x00007D2D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(float v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00009B35 File Offset: 0x00007D35
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 double2x2(float2x2 v)
		{
			return new double2x2(v);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00009B3D File Offset: 0x00007D3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 transpose(double2x2 v)
		{
			return math.double2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00009B70 File Offset: 0x00007D70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 inverse(double2x2 m)
		{
			double a = m.c0.x;
			double b = m.c1.x;
			double c = m.c0.y;
			double d = m.c1.y;
			double det = a * d - b * c;
			return math.double2x2(d, -b, -c, a) * (1.0 / det);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00009BD4 File Offset: 0x00007DD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double determinant(double2x2 m)
		{
			double x = m.c0.x;
			double b = m.c1.x;
			double c = m.c0.y;
			double d = m.c1.y;
			return x * d - b * c;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00009C18 File Offset: 0x00007E18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double2x2 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint2(4253034763U, 2338696631U) + math.fold_to_uint(v.c1) * math.uint2(3757372771U, 1885959949U)) + 3508684087U;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00009C74 File Offset: 0x00007E74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(double2x2 v)
		{
			return math.fold_to_uint(v.c0) * math.uint2(3919501043U, 1209161033U) + math.fold_to_uint(v.c1) * math.uint2(4007793211U, 3819806693U) + 3458005183U;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00009CCE File Offset: 0x00007ECE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(double2 c0, double2 c1, double2 c2)
		{
			return new double2x3(c0, c1, c2);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00009CD8 File Offset: 0x00007ED8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(double m00, double m01, double m02, double m10, double m11, double m12)
		{
			return new double2x3(m00, m01, m02, m10, m11, m12);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00009CE7 File Offset: 0x00007EE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(double v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00009CEF File Offset: 0x00007EEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(bool v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00009CF7 File Offset: 0x00007EF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(bool2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00009CFF File Offset: 0x00007EFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(int v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00009D07 File Offset: 0x00007F07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(int2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00009D0F File Offset: 0x00007F0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(uint v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00009D17 File Offset: 0x00007F17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(uint2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00009D1F File Offset: 0x00007F1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(float v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00009D27 File Offset: 0x00007F27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 double2x3(float2x3 v)
		{
			return new double2x3(v);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00009D30 File Offset: 0x00007F30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 transpose(double2x3 v)
		{
			return math.double3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00009D84 File Offset: 0x00007F84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double2x3 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint2(4066109527U, 2613148903U) + math.fold_to_uint(v.c1) * math.uint2(3367528529U, 1678332449U) + math.fold_to_uint(v.c2) * math.uint2(2918459647U, 2744611081U)) + 1952372791U;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00009E04 File Offset: 0x00008004
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(double2x3 v)
		{
			return math.fold_to_uint(v.c0) * math.uint2(2631698677U, 4200781601U) + math.fold_to_uint(v.c1) * math.uint2(2119021007U, 1760485621U) + math.fold_to_uint(v.c2) * math.uint2(3157985881U, 2171534173U) + 2723054263U;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00009E82 File Offset: 0x00008082
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(double2 c0, double2 c1, double2 c2, double2 c3)
		{
			return new double2x4(c0, c1, c2, c3);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00009E8D File Offset: 0x0000808D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13)
		{
			return new double2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00009EA0 File Offset: 0x000080A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(double v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00009EA8 File Offset: 0x000080A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(bool v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00009EB0 File Offset: 0x000080B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(bool2x4 v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00009EB8 File Offset: 0x000080B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(int v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00009EC0 File Offset: 0x000080C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(int2x4 v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00009EC8 File Offset: 0x000080C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(uint v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00009ED0 File Offset: 0x000080D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(uint2x4 v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00009ED8 File Offset: 0x000080D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(float v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00009EE0 File Offset: 0x000080E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 double2x4(float2x4 v)
		{
			return new double2x4(v);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00009EE8 File Offset: 0x000080E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 transpose(double2x4 v)
		{
			return math.double4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00009F54 File Offset: 0x00008154
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double2x4 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint2(2437373431U, 1441286183U) + math.fold_to_uint(v.c1) * math.uint2(2426570171U, 1561977301U) + math.fold_to_uint(v.c2) * math.uint2(4205774813U, 1650214333U) + math.fold_to_uint(v.c3) * math.uint2(3388112843U, 1831150513U)) + 1848374953U;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00009FF8 File Offset: 0x000081F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(double2x4 v)
		{
			return math.fold_to_uint(v.c0) * math.uint2(3430200247U, 2209710647U) + math.fold_to_uint(v.c1) * math.uint2(2201894441U, 2849577407U) + math.fold_to_uint(v.c2) * math.uint2(3287031191U, 3098675399U) + math.fold_to_uint(v.c3) * math.uint2(1564399943U, 1148435377U) + 3416333663U;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000A09A File Offset: 0x0000829A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(double x, double y, double z)
		{
			return new double3(x, y, z);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000A0A4 File Offset: 0x000082A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(double x, double2 yz)
		{
			return new double3(x, yz);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000A0AD File Offset: 0x000082AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(double2 xy, double z)
		{
			return new double3(xy, z);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000A0B6 File Offset: 0x000082B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(double3 xyz)
		{
			return new double3(xyz);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000A0BE File Offset: 0x000082BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(double v)
		{
			return new double3(v);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000A0C6 File Offset: 0x000082C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(bool v)
		{
			return new double3(v);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000A0CE File Offset: 0x000082CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(bool3 v)
		{
			return new double3(v);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000A0D6 File Offset: 0x000082D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(int v)
		{
			return new double3(v);
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000A0DE File Offset: 0x000082DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(int3 v)
		{
			return new double3(v);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000A0E6 File Offset: 0x000082E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(uint v)
		{
			return new double3(v);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000A0EE File Offset: 0x000082EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(uint3 v)
		{
			return new double3(v);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000A0F6 File Offset: 0x000082F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(half v)
		{
			return new double3(v);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000A0FE File Offset: 0x000082FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(half3 v)
		{
			return new double3(v);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000A106 File Offset: 0x00008306
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(float v)
		{
			return new double3(v);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000A10E File Offset: 0x0000830E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 double3(float3 v)
		{
			return new double3(v);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000A116 File Offset: 0x00008316
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double3 v)
		{
			return math.csum(math.fold_to_uint(v) * math.uint3(2937008387U, 3835713223U, 2216526373U)) + 3375971453U;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x0000A142 File Offset: 0x00008342
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(double3 v)
		{
			return math.fold_to_uint(v) * math.uint3(3559829411U, 3652178029U, 2544260129U) + 2013864031U;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000A16D File Offset: 0x0000836D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double shuffle(double3 left, double3 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000A177 File Offset: 0x00008377
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 shuffle(double3 left, double3 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.double2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000A18E File Offset: 0x0000838E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 shuffle(double3 left, double3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.double3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0000A1AE File Offset: 0x000083AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 shuffle(double3 left, double3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.double4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000A1D8 File Offset: 0x000083D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double select_shuffle_component(double3 a, double3 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000A24F File Offset: 0x0000844F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(double3 c0, double3 c1)
		{
			return new double3x2(c0, c1);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x0000A258 File Offset: 0x00008458
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(double m00, double m01, double m10, double m11, double m20, double m21)
		{
			return new double3x2(m00, m01, m10, m11, m20, m21);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000A267 File Offset: 0x00008467
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(double v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000A26F File Offset: 0x0000846F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(bool v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000A277 File Offset: 0x00008477
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(bool3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000A27F File Offset: 0x0000847F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(int v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000A287 File Offset: 0x00008487
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(int3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000A28F File Offset: 0x0000848F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(uint v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000A297 File Offset: 0x00008497
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(uint3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000A29F File Offset: 0x0000849F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(float v)
		{
			return new double3x2(v);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000A2A7 File Offset: 0x000084A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 double3x2(float3x2 v)
		{
			return new double3x2(v);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000A2B0 File Offset: 0x000084B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 transpose(double3x2 v)
		{
			return math.double2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x0000A304 File Offset: 0x00008504
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double3x2 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint3(3996716183U, 2626301701U, 1306289417U) + math.fold_to_uint(v.c1) * math.uint3(2096137163U, 1548578029U, 4178800919U)) + 3898072289U;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000A36C File Offset: 0x0000856C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(double3x2 v)
		{
			return math.fold_to_uint(v.c0) * math.uint3(4129428421U, 2631575897U, 2854656703U) + math.fold_to_uint(v.c1) * math.uint3(3578504047U, 4245178297U, 2173281923U) + 2973357649U;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000A3D0 File Offset: 0x000085D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(double3 c0, double3 c1, double3 c2)
		{
			return new double3x3(c0, c1, c2);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000A3DC File Offset: 0x000085DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22)
		{
			return new double3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000A3FC File Offset: 0x000085FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(double v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000A404 File Offset: 0x00008604
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(bool v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000A40C File Offset: 0x0000860C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(bool3x3 v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000A414 File Offset: 0x00008614
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(int v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000A41C File Offset: 0x0000861C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(int3x3 v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000A424 File Offset: 0x00008624
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(uint v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000A42C File Offset: 0x0000862C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(uint3x3 v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000A434 File Offset: 0x00008634
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(float v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000A43C File Offset: 0x0000863C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 double3x3(float3x3 v)
		{
			return new double3x3(v);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000A444 File Offset: 0x00008644
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 transpose(double3x3 v)
		{
			return math.double3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000A4BC File Offset: 0x000086BC
		public static double3x3 inverse(double3x3 m)
		{
			double3 c0 = m.c0;
			double3 c2 = m.c1;
			double3 c = m.c2;
			double3 t0 = math.double3(c2.x, c.x, c0.x);
			double3 t = math.double3(c2.y, c.y, c0.y);
			double3 t2 = math.double3(c2.z, c.z, c0.z);
			double3 m2 = t * t2.yzx - t.yzx * t2;
			double3 m3 = t0.yzx * t2 - t0 * t2.yzx;
			double3 m4 = t0 * t.yzx - t0.yzx * t;
			double rcpDet = 1.0 / math.csum(t0.zxy * m2);
			return math.double3x3(m2, m3, m4) * rcpDet;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000A5BC File Offset: 0x000087BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double determinant(double3x3 m)
		{
			double3 c0 = m.c0;
			double3 c = m.c1;
			double3 c2 = m.c2;
			double m2 = c.y * c2.z - c.z * c2.y;
			double m3 = c0.y * c2.z - c0.z * c2.y;
			double m4 = c0.y * c.z - c0.z * c.y;
			return c0.x * m2 - c.x * m3 + c2.x * m4;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000A650 File Offset: 0x00008850
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double3x3 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint3(2891822459U, 2837054189U, 3016004371U) + math.fold_to_uint(v.c1) * math.uint3(4097481403U, 2229788699U, 2382715877U) + math.fold_to_uint(v.c2) * math.uint3(1851936439U, 1938025801U, 3712598587U)) + 3956330501U;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000A6E0 File Offset: 0x000088E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(double3x3 v)
		{
			return math.fold_to_uint(v.c0) * math.uint3(2437373431U, 1441286183U, 2426570171U) + math.fold_to_uint(v.c1) * math.uint3(1561977301U, 4205774813U, 1650214333U) + math.fold_to_uint(v.c2) * math.uint3(3388112843U, 1831150513U, 1848374953U) + 3430200247U;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000A76D File Offset: 0x0000896D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(double3 c0, double3 c1, double3 c2, double3 c3)
		{
			return new double3x4(c0, c1, c2, c3);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000A778 File Offset: 0x00008978
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23)
		{
			return new double3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000A79E File Offset: 0x0000899E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(double v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000A7A6 File Offset: 0x000089A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(bool v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000A7AE File Offset: 0x000089AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(bool3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000A7B6 File Offset: 0x000089B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(int v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000A7BE File Offset: 0x000089BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(int3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000A7C6 File Offset: 0x000089C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(uint v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000A7CE File Offset: 0x000089CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(uint3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000A7D6 File Offset: 0x000089D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(float v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000A7DE File Offset: 0x000089DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 double3x4(float3x4 v)
		{
			return new double3x4(v);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000A7E8 File Offset: 0x000089E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 transpose(double3x4 v)
		{
			return math.double4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000A880 File Offset: 0x00008A80
		public static double3x4 fastinverse(double3x4 m)
		{
			double3 c3 = m.c0;
			double3 c = m.c1;
			double3 c2 = m.c2;
			double3 pos = m.c3;
			double3 r0 = math.double3(c3.x, c.x, c2.x);
			double3 r = math.double3(c3.y, c.y, c2.y);
			double3 r2 = math.double3(c3.z, c.z, c2.z);
			pos = -(r0 * pos.x + r * pos.y + r2 * pos.z);
			return math.double3x4(r0, r, r2, pos);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000A934 File Offset: 0x00008B34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double3x4 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint3(3996716183U, 2626301701U, 1306289417U) + math.fold_to_uint(v.c1) * math.uint3(2096137163U, 1548578029U, 4178800919U) + math.fold_to_uint(v.c2) * math.uint3(3898072289U, 4129428421U, 2631575897U) + math.fold_to_uint(v.c3) * math.uint3(2854656703U, 3578504047U, 4245178297U)) + 2173281923U;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000A9EC File Offset: 0x00008BEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(double3x4 v)
		{
			return math.fold_to_uint(v.c0) * math.uint3(2973357649U, 3881277847U, 4017968839U) + math.fold_to_uint(v.c1) * math.uint3(1727237899U, 1648514723U, 1385344481U) + math.fold_to_uint(v.c2) * math.uint3(3538260197U, 4066109527U, 2613148903U) + math.fold_to_uint(v.c3) * math.uint3(3367528529U, 1678332449U, 2918459647U) + 2744611081U;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000AAA2 File Offset: 0x00008CA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double x, double y, double z, double w)
		{
			return new double4(x, y, z, w);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000AAAD File Offset: 0x00008CAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double x, double y, double2 zw)
		{
			return new double4(x, y, zw);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000AAB7 File Offset: 0x00008CB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double x, double2 yz, double w)
		{
			return new double4(x, yz, w);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000AAC1 File Offset: 0x00008CC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double x, double3 yzw)
		{
			return new double4(x, yzw);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000AACA File Offset: 0x00008CCA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double2 xy, double z, double w)
		{
			return new double4(xy, z, w);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double2 xy, double2 zw)
		{
			return new double4(xy, zw);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000AADD File Offset: 0x00008CDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double3 xyz, double w)
		{
			return new double4(xyz, w);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000AAE6 File Offset: 0x00008CE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double4 xyzw)
		{
			return new double4(xyzw);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000AAEE File Offset: 0x00008CEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(double v)
		{
			return new double4(v);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000AAF6 File Offset: 0x00008CF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(bool v)
		{
			return new double4(v);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000AAFE File Offset: 0x00008CFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(bool4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000AB06 File Offset: 0x00008D06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(int v)
		{
			return new double4(v);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000AB0E File Offset: 0x00008D0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(int4 v)
		{
			return new double4(v);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000AB16 File Offset: 0x00008D16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(uint v)
		{
			return new double4(v);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000AB1E File Offset: 0x00008D1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(uint4 v)
		{
			return new double4(v);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000AB26 File Offset: 0x00008D26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(half v)
		{
			return new double4(v);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000AB2E File Offset: 0x00008D2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(half4 v)
		{
			return new double4(v);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000AB36 File Offset: 0x00008D36
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(float v)
		{
			return new double4(v);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000AB3E File Offset: 0x00008D3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 double4(float4 v)
		{
			return new double4(v);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000AB46 File Offset: 0x00008D46
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double4 v)
		{
			return math.csum(math.fold_to_uint(v) * math.uint4(2669441947U, 1260114311U, 2650080659U, 4052675461U)) + 2652487619U;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000AB77 File Offset: 0x00008D77
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(double4 v)
		{
			return math.fold_to_uint(v) * math.uint4(2174136431U, 3528391193U, 2105559227U, 1899745391U) + 1966790317U;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000ABA7 File Offset: 0x00008DA7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double shuffle(double4 left, double4 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000ABB1 File Offset: 0x00008DB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 shuffle(double4 left, double4 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.double2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000ABC8 File Offset: 0x00008DC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 shuffle(double4 left, double4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.double3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000ABE8 File Offset: 0x00008DE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 shuffle(double4 left, double4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.double4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000AC14 File Offset: 0x00008E14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double select_shuffle_component(double4 a, double4 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.LeftW:
				return a.w;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			case math.ShuffleComponent.RightW:
				return b.w;
			default:
				throw new ArgumentException("Invalid shuffle component: " + component.ToString());
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000AC9D File Offset: 0x00008E9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(double4 c0, double4 c1)
		{
			return new double4x2(c0, c1);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000ACA6 File Offset: 0x00008EA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(double m00, double m01, double m10, double m11, double m20, double m21, double m30, double m31)
		{
			return new double4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000ACB9 File Offset: 0x00008EB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(double v)
		{
			return new double4x2(v);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000ACC1 File Offset: 0x00008EC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(bool v)
		{
			return new double4x2(v);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000ACC9 File Offset: 0x00008EC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(bool4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000ACD1 File Offset: 0x00008ED1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(int v)
		{
			return new double4x2(v);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000ACD9 File Offset: 0x00008ED9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(int4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000ACE1 File Offset: 0x00008EE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(uint v)
		{
			return new double4x2(v);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000ACE9 File Offset: 0x00008EE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(uint4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000ACF1 File Offset: 0x00008EF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(float v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 double4x2(float4x2 v)
		{
			return new double4x2(v);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000AD04 File Offset: 0x00008F04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 transpose(double4x2 v)
		{
			return math.double2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000AD70 File Offset: 0x00008F70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double4x2 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint4(1521739981U, 1735296007U, 3010324327U, 1875523709U) + math.fold_to_uint(v.c1) * math.uint4(2937008387U, 3835713223U, 2216526373U, 3375971453U)) + 3559829411U;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(double4x2 v)
		{
			return math.fold_to_uint(v.c0) * math.uint4(3652178029U, 2544260129U, 2013864031U, 2627668003U) + math.fold_to_uint(v.c1) * math.uint4(1520214331U, 2949502447U, 2827819133U, 3480140317U) + 2642994593U;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000AE4E File Offset: 0x0000904E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(double4 c0, double4 c1, double4 c2)
		{
			return new double4x3(c0, c1, c2);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000AE58 File Offset: 0x00009058
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(double m00, double m01, double m02, double m10, double m11, double m12, double m20, double m21, double m22, double m30, double m31, double m32)
		{
			return new double4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000AE7E File Offset: 0x0000907E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(double v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000AE86 File Offset: 0x00009086
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(bool v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000AE8E File Offset: 0x0000908E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(bool4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000AE96 File Offset: 0x00009096
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(int v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000AE9E File Offset: 0x0000909E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(int4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000AEA6 File Offset: 0x000090A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(uint v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AEAE File Offset: 0x000090AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(uint4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AEB6 File Offset: 0x000090B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(float v)
		{
			return new double4x3(v);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AEBE File Offset: 0x000090BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 double4x3(float4x3 v)
		{
			return new double4x3(v);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000AEC8 File Offset: 0x000090C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 transpose(double4x3 v)
		{
			return math.double3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000AF60 File Offset: 0x00009160
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double4x3 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint4(2057338067U, 2942577577U, 2834440507U, 2671762487U) + math.fold_to_uint(v.c1) * math.uint4(2892026051U, 2455987759U, 3868600063U, 3170963179U) + math.fold_to_uint(v.c2) * math.uint4(2632835537U, 1136528209U, 2944626401U, 2972762423U)) + 1417889653U;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B000 File Offset: 0x00009200
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(double4x3 v)
		{
			return math.fold_to_uint(v.c0) * math.uint4(2080514593U, 2731544287U, 2828498809U, 2669441947U) + math.fold_to_uint(v.c1) * math.uint4(1260114311U, 2650080659U, 4052675461U, 2652487619U) + math.fold_to_uint(v.c2) * math.uint4(2174136431U, 3528391193U, 2105559227U, 1899745391U) + 1966790317U;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B09C File Offset: 0x0000929C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(double4 c0, double4 c1, double4 c2, double4 c3)
		{
			return new double4x4(c0, c1, c2, c3);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B0A8 File Offset: 0x000092A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21, double m22, double m23, double m30, double m31, double m32, double m33)
		{
			return new double4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B0D6 File Offset: 0x000092D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(double v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000B0DE File Offset: 0x000092DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(bool v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B0E6 File Offset: 0x000092E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(bool4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B0EE File Offset: 0x000092EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(int v)
		{
			return new double4x4(v);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B0F6 File Offset: 0x000092F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(int4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000B0FE File Offset: 0x000092FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(uint v)
		{
			return new double4x4(v);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000B106 File Offset: 0x00009306
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(uint4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000B10E File Offset: 0x0000930E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(float v)
		{
			return new double4x4(v);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B116 File Offset: 0x00009316
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 double4x4(float4x4 v)
		{
			return new double4x4(v);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B120 File Offset: 0x00009320
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 rotate(double4x4 a, double3 b)
		{
			return (a.c0 * b.x + a.c1 * b.y + a.c2 * b.z).xyz;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000B174 File Offset: 0x00009374
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 transform(double4x4 a, double3 b)
		{
			return (a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3).xyz;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B1D4 File Offset: 0x000093D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 transpose(double4x4 v)
		{
			return math.double4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B298 File Offset: 0x00009498
		public static double4x4 inverse(double4x4 m)
		{
			double4 c0 = m.c0;
			double4 c = m.c1;
			double4 c2 = m.c2;
			double4 c3 = m.c3;
			double4 r0y_r1y_r0x_r1x = math.movelh(c, c0);
			double4 r0z_r1z_r0w_r1w = math.movelh(c2, c3);
			double4 r2y_r3y_r2x_r3x = math.movehl(c0, c);
			double4 r2z_r3z_r2w_r3w = math.movehl(c3, c2);
			double4 @double = math.shuffle(c, c0, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightY, math.ShuffleComponent.RightZ);
			double4 r1z_r2z_r1w_r2w = math.shuffle(c2, c3, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightY, math.ShuffleComponent.RightZ);
			double4 r3y_r0y_r3x_r0x = math.shuffle(c, c0, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightW, math.ShuffleComponent.RightX);
			double4 r3z_r0z_r3w_r0w = math.shuffle(c2, c3, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightW, math.ShuffleComponent.RightX);
			double4 r0_wzyx = math.shuffle(r0z_r1z_r0w_r1w, r0y_r1y_r0x_r1x, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.RightZ);
			double4 r1_wzyx = math.shuffle(r0z_r1z_r0w_r1w, r0y_r1y_r0x_r1x, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightY, math.ShuffleComponent.RightW);
			double4 r2_wzyx = math.shuffle(r2z_r3z_r2w_r3w, r2y_r3y_r2x_r3x, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.RightZ);
			double4 r3_wzyx = math.shuffle(r2z_r3z_r2w_r3w, r2y_r3y_r2x_r3x, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightY, math.ShuffleComponent.RightW);
			double4 r0_xyzw = math.shuffle(r0y_r1y_r0x_r1x, r0z_r1z_r0w_r1w, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.RightZ);
			double4 double2 = @double * r2z_r3z_r2w_r3w - r1z_r2z_r1w_r2w * r2y_r3y_r2x_r3x;
			double4 inner02_13 = r0y_r1y_r0x_r1x * r2z_r3z_r2w_r3w - r0z_r1z_r0w_r1w * r2y_r3y_r2x_r3x;
			double4 inner30_ = r3z_r0z_r3w_r0w * r0y_r1y_r0x_r1x - r3y_r0y_r3x_r0x * r0z_r1z_r0w_r1w;
			double4 inner12 = math.shuffle(double2, double2, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightX);
			double4 inner13 = math.shuffle(double2, double2, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW, math.ShuffleComponent.RightY);
			double4 inner14 = math.shuffle(inner02_13, inner02_13, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightX);
			double4 inner15 = math.shuffle(inner02_13, inner02_13, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW, math.ShuffleComponent.RightY);
			double4 minors0 = r3_wzyx * inner12 - r2_wzyx * inner15 + r1_wzyx * inner13;
			double4 denom = r0_xyzw * minors0;
			denom += math.shuffle(denom, denom, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightW, math.ShuffleComponent.RightZ);
			denom -= math.shuffle(denom, denom, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightX, math.ShuffleComponent.RightX);
			double4 rcp_denom_ppnn = math.double4(1.0) / denom;
			double4x4 res;
			res.c0 = minors0 * rcp_denom_ppnn;
			double4 inner16 = math.shuffle(inner30_, inner30_, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightX);
			double4 inner17 = math.shuffle(inner30_, inner30_, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW, math.ShuffleComponent.RightY);
			double4 minors = r2_wzyx * inner16 - r0_wzyx * inner13 - r3_wzyx * inner14;
			res.c1 = minors * rcp_denom_ppnn;
			double4 minors2 = r0_wzyx * inner15 - r1_wzyx * inner16 - r3_wzyx * inner17;
			res.c2 = minors2 * rcp_denom_ppnn;
			double4 minors3 = r1_wzyx * inner14 - r0_wzyx * inner12 + r2_wzyx * inner17;
			res.c3 = minors3 * rcp_denom_ppnn;
			return res;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000B52C File Offset: 0x0000972C
		public static double4x4 fastinverse(double4x4 m)
		{
			double4 c3 = m.c0;
			double4 c = m.c1;
			double4 c2 = m.c2;
			double4 pos = m.c3;
			double4 zero = math.double4(0);
			double4 t0 = math.unpacklo(c3, c2);
			double4 t = math.unpacklo(c, zero);
			double4 @double = math.unpackhi(c3, c2);
			double4 t2 = math.unpackhi(c, zero);
			double4 r0 = math.unpacklo(t0, t);
			double4 r = math.unpackhi(t0, t);
			double4 r2 = math.unpacklo(@double, t2);
			pos = -(r0 * pos.x + r * pos.y + r2 * pos.z);
			pos.w = 1.0;
			return math.double4x4(r0, r, r2, pos);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000B5F0 File Offset: 0x000097F0
		public static double determinant(double4x4 m)
		{
			double4 c0 = m.c0;
			double4 c = m.c1;
			double4 c2 = m.c2;
			double4 c3 = m.c3;
			double m2 = c.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c.z * c3.w - c.w * c3.z) + c3.y * (c.z * c2.w - c.w * c2.z);
			double m3 = c0.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c0.z * c3.w - c0.w * c3.z) + c3.y * (c0.z * c2.w - c0.w * c2.z);
			double m4 = c0.y * (c.z * c3.w - c.w * c3.z) - c.y * (c0.z * c3.w - c0.w * c3.z) + c3.y * (c0.z * c.w - c0.w * c.z);
			double m5 = c0.y * (c.z * c2.w - c.w * c2.z) - c.y * (c0.z * c2.w - c0.w * c2.z) + c2.y * (c0.z * c.w - c0.w * c.z);
			return c0.x * m2 - c.x * m3 + c2.x * m4 - c3.x * m5;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000B7E8 File Offset: 0x000099E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(double4x4 v)
		{
			return math.csum(math.fold_to_uint(v.c0) * math.uint4(1306289417U, 2096137163U, 1548578029U, 4178800919U) + math.fold_to_uint(v.c1) * math.uint4(3898072289U, 4129428421U, 2631575897U, 2854656703U) + math.fold_to_uint(v.c2) * math.uint4(3578504047U, 4245178297U, 2173281923U, 2973357649U) + math.fold_to_uint(v.c3) * math.uint4(3881277847U, 4017968839U, 1727237899U, 1648514723U)) + 1385344481U;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(double4x4 v)
		{
			return math.fold_to_uint(v.c0) * math.uint4(3538260197U, 4066109527U, 2613148903U, 3367528529U) + math.fold_to_uint(v.c1) * math.uint4(1678332449U, 2918459647U, 2744611081U, 1952372791U) + math.fold_to_uint(v.c2) * math.uint4(2631698677U, 4200781601U, 2119021007U, 1760485621U) + math.fold_to_uint(v.c3) * math.uint4(3157985881U, 2171534173U, 2723054263U, 1168253063U) + 4228926523U;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000B97E File Offset: 0x00009B7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(float x, float y)
		{
			return new float2(x, y);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000B987 File Offset: 0x00009B87
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(float2 xy)
		{
			return new float2(xy);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000B98F File Offset: 0x00009B8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(float v)
		{
			return new float2(v);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000B997 File Offset: 0x00009B97
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(bool v)
		{
			return new float2(v);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000B99F File Offset: 0x00009B9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(bool2 v)
		{
			return new float2(v);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000B9A7 File Offset: 0x00009BA7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(int v)
		{
			return new float2(v);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B9AF File Offset: 0x00009BAF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(int2 v)
		{
			return new float2(v);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B9B7 File Offset: 0x00009BB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(uint v)
		{
			return new float2(v);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B9BF File Offset: 0x00009BBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(uint2 v)
		{
			return new float2(v);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000B9C7 File Offset: 0x00009BC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(half v)
		{
			return new float2(v);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000B9CF File Offset: 0x00009BCF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(half2 v)
		{
			return new float2(v);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000B9D7 File Offset: 0x00009BD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(double v)
		{
			return new float2(v);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000B9DF File Offset: 0x00009BDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 float2(double2 v)
		{
			return new float2(v);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000B9E7 File Offset: 0x00009BE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float2 v)
		{
			return math.csum(math.asuint(v) * math.uint2(4198118021U, 2908068253U)) + 3705492289U;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000BA0E File Offset: 0x00009C0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(float2 v)
		{
			return math.asuint(v) * math.uint2(2497566569U, 2716413241U) + 1166264321U;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000BA34 File Offset: 0x00009C34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float shuffle(float2 left, float2 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000BA3E File Offset: 0x00009C3E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 shuffle(float2 left, float2 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.float2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000BA55 File Offset: 0x00009C55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 shuffle(float2 left, float2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.float3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000BA75 File Offset: 0x00009C75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 shuffle(float2 left, float2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.float4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float select_shuffle_component(float2 a, float2 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000BB05 File Offset: 0x00009D05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(float2 c0, float2 c1)
		{
			return new float2x2(c0, c1);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000BB0E File Offset: 0x00009D0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(float m00, float m01, float m10, float m11)
		{
			return new float2x2(m00, m01, m10, m11);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000BB19 File Offset: 0x00009D19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(float v)
		{
			return new float2x2(v);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000BB21 File Offset: 0x00009D21
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(bool v)
		{
			return new float2x2(v);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000BB29 File Offset: 0x00009D29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(bool2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000BB31 File Offset: 0x00009D31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(int v)
		{
			return new float2x2(v);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000BB39 File Offset: 0x00009D39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(int2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000BB41 File Offset: 0x00009D41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(uint v)
		{
			return new float2x2(v);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000BB49 File Offset: 0x00009D49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(uint2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000BB51 File Offset: 0x00009D51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(double v)
		{
			return new float2x2(v);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000BB59 File Offset: 0x00009D59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 float2x2(double2x2 v)
		{
			return new float2x2(v);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000BB61 File Offset: 0x00009D61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 transpose(float2x2 v)
		{
			return math.float2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000BB94 File Offset: 0x00009D94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 inverse(float2x2 m)
		{
			float a = m.c0.x;
			float b = m.c1.x;
			float c = m.c0.y;
			float d = m.c1.y;
			float det = a * d - b * c;
			return math.float2x2(d, -b, -c, a) * (1f / det);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000BBF4 File Offset: 0x00009DF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float determinant(float2x2 m)
		{
			float x = m.c0.x;
			float b = m.c1.x;
			float c = m.c0.y;
			float d = m.c1.y;
			return x * d - b * c;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000BC38 File Offset: 0x00009E38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float2x2 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint2(2627668003U, 1520214331U) + math.asuint(v.c1) * math.uint2(2949502447U, 2827819133U)) + 3480140317U;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000BC94 File Offset: 0x00009E94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(float2x2 v)
		{
			return math.asuint(v.c0) * math.uint2(2642994593U, 3940484981U) + math.asuint(v.c1) * math.uint2(1954192763U, 1091696537U) + 3052428017U;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000BCEE File Offset: 0x00009EEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(float2 c0, float2 c1, float2 c2)
		{
			return new float2x3(c0, c1, c2);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(float m00, float m01, float m02, float m10, float m11, float m12)
		{
			return new float2x3(m00, m01, m02, m10, m11, m12);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000BD07 File Offset: 0x00009F07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(float v)
		{
			return new float2x3(v);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000BD0F File Offset: 0x00009F0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(bool v)
		{
			return new float2x3(v);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000BD17 File Offset: 0x00009F17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(bool2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000BD1F File Offset: 0x00009F1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(int v)
		{
			return new float2x3(v);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000BD27 File Offset: 0x00009F27
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(int2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000BD2F File Offset: 0x00009F2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(uint v)
		{
			return new float2x3(v);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000BD37 File Offset: 0x00009F37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(uint2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000BD3F File Offset: 0x00009F3F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(double v)
		{
			return new float2x3(v);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000BD47 File Offset: 0x00009F47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 float2x3(double2x3 v)
		{
			return new float2x3(v);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000BD50 File Offset: 0x00009F50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 transpose(float2x3 v)
		{
			return math.float3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000BDA4 File Offset: 0x00009FA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float2x3 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint2(3898072289U, 4129428421U) + math.asuint(v.c1) * math.uint2(2631575897U, 2854656703U) + math.asuint(v.c2) * math.uint2(3578504047U, 4245178297U)) + 2173281923U;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000BE24 File Offset: 0x0000A024
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(float2x3 v)
		{
			return math.asuint(v.c0) * math.uint2(2973357649U, 3881277847U) + math.asuint(v.c1) * math.uint2(4017968839U, 1727237899U) + math.asuint(v.c2) * math.uint2(1648514723U, 1385344481U) + 3538260197U;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000BEA2 File Offset: 0x0000A0A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(float2 c0, float2 c1, float2 c2, float2 c3)
		{
			return new float2x4(c0, c1, c2, c3);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000BEAD File Offset: 0x0000A0AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13)
		{
			return new float2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(float v)
		{
			return new float2x4(v);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(bool v)
		{
			return new float2x4(v);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(bool2x4 v)
		{
			return new float2x4(v);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000BED8 File Offset: 0x0000A0D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(int v)
		{
			return new float2x4(v);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000BEE0 File Offset: 0x0000A0E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(int2x4 v)
		{
			return new float2x4(v);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000BEE8 File Offset: 0x0000A0E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(uint v)
		{
			return new float2x4(v);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(uint2x4 v)
		{
			return new float2x4(v);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000BEF8 File Offset: 0x0000A0F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(double v)
		{
			return new float2x4(v);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000BF00 File Offset: 0x0000A100
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 float2x4(double2x4 v)
		{
			return new float2x4(v);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000BF08 File Offset: 0x0000A108
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 transpose(float2x4 v)
		{
			return math.float4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000BF74 File Offset: 0x0000A174
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float2x4 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint2(3546061613U, 2702024231U) + math.asuint(v.c1) * math.uint2(1452124841U, 1966089551U) + math.asuint(v.c2) * math.uint2(2668168249U, 1587512777U) + math.asuint(v.c3) * math.uint2(2353831999U, 3101256173U)) + 2891822459U;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000C018 File Offset: 0x0000A218
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(float2x4 v)
		{
			return math.asuint(v.c0) * math.uint2(2837054189U, 3016004371U) + math.asuint(v.c1) * math.uint2(4097481403U, 2229788699U) + math.asuint(v.c2) * math.uint2(2382715877U, 1851936439U) + math.asuint(v.c3) * math.uint2(1938025801U, 3712598587U) + 3956330501U;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000C0BA File Offset: 0x0000A2BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(float x, float y, float z)
		{
			return new float3(x, y, z);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000C0C4 File Offset: 0x0000A2C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(float x, float2 yz)
		{
			return new float3(x, yz);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000C0CD File Offset: 0x0000A2CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(float2 xy, float z)
		{
			return new float3(xy, z);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000C0D6 File Offset: 0x0000A2D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(float3 xyz)
		{
			return new float3(xyz);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000C0DE File Offset: 0x0000A2DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(float v)
		{
			return new float3(v);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000C0E6 File Offset: 0x0000A2E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(bool v)
		{
			return new float3(v);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000C0EE File Offset: 0x0000A2EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(bool3 v)
		{
			return new float3(v);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000C0F6 File Offset: 0x0000A2F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(int v)
		{
			return new float3(v);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000C0FE File Offset: 0x0000A2FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(int3 v)
		{
			return new float3(v);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000C106 File Offset: 0x0000A306
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(uint v)
		{
			return new float3(v);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000C10E File Offset: 0x0000A30E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(uint3 v)
		{
			return new float3(v);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000C116 File Offset: 0x0000A316
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(half v)
		{
			return new float3(v);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000C11E File Offset: 0x0000A31E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(half3 v)
		{
			return new float3(v);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000C126 File Offset: 0x0000A326
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(double v)
		{
			return new float3(v);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000C12E File Offset: 0x0000A32E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 float3(double3 v)
		{
			return new float3(v);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000C136 File Offset: 0x0000A336
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float3 v)
		{
			return math.csum(math.asuint(v) * math.uint3(2601761069U, 1254033427U, 2248573027U)) + 3612677113U;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000C162 File Offset: 0x0000A362
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(float3 v)
		{
			return math.asuint(v) * math.uint3(1521739981U, 1735296007U, 3010324327U) + 1875523709U;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000C18D File Offset: 0x0000A38D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float shuffle(float3 left, float3 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000C197 File Offset: 0x0000A397
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 shuffle(float3 left, float3 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.float2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000C1AE File Offset: 0x0000A3AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 shuffle(float3 left, float3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.float3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000C1CE File Offset: 0x0000A3CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 shuffle(float3 left, float3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.float4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float select_shuffle_component(float3 a, float3 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000C26F File Offset: 0x0000A46F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(float3 c0, float3 c1)
		{
			return new float3x2(c0, c1);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000C278 File Offset: 0x0000A478
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(float m00, float m01, float m10, float m11, float m20, float m21)
		{
			return new float3x2(m00, m01, m10, m11, m20, m21);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000C287 File Offset: 0x0000A487
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(float v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000C28F File Offset: 0x0000A48F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(bool v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000C297 File Offset: 0x0000A497
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(bool3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000C29F File Offset: 0x0000A49F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(int v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000C2A7 File Offset: 0x0000A4A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(int3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000C2AF File Offset: 0x0000A4AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(uint v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000C2B7 File Offset: 0x0000A4B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(uint3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000C2BF File Offset: 0x0000A4BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(double v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000C2C7 File Offset: 0x0000A4C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 float3x2(double3x2 v)
		{
			return new float3x2(v);
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 transpose(float3x2 v)
		{
			return math.float2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000C324 File Offset: 0x0000A524
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float3x2 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint3(3777095341U, 3385463369U, 1773538433U) + math.asuint(v.c1) * math.uint3(3773525029U, 4131962539U, 1809525511U)) + 4016293529U;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000C38C File Offset: 0x0000A58C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(float3x2 v)
		{
			return math.asuint(v.c0) * math.uint3(2416021567U, 2828384717U, 2636362241U) + math.asuint(v.c1) * math.uint3(1258410977U, 1952565773U, 2037535609U) + 3592785499U;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(float3 c0, float3 c1, float3 c2)
		{
			return new float3x3(c0, c1, c2);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22)
		{
			return new float3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000C41C File Offset: 0x0000A61C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(float v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000C424 File Offset: 0x0000A624
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(bool v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000C42C File Offset: 0x0000A62C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(bool3x3 v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000C434 File Offset: 0x0000A634
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(int v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000C43C File Offset: 0x0000A63C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(int3x3 v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000C444 File Offset: 0x0000A644
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(uint v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000C44C File Offset: 0x0000A64C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(uint3x3 v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000C454 File Offset: 0x0000A654
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(double v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000C45C File Offset: 0x0000A65C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(double3x3 v)
		{
			return new float3x3(v);
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000C464 File Offset: 0x0000A664
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 transpose(float3x3 v)
		{
			return math.float3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		public static float3x3 inverse(float3x3 m)
		{
			float3 c0 = m.c0;
			float3 c2 = m.c1;
			float3 c = m.c2;
			float3 t0 = math.float3(c2.x, c.x, c0.x);
			float3 t = math.float3(c2.y, c.y, c0.y);
			float3 t2 = math.float3(c2.z, c.z, c0.z);
			float3 m2 = t * t2.yzx - t.yzx * t2;
			float3 m3 = t0.yzx * t2 - t0 * t2.yzx;
			float3 m4 = t0 * t.yzx - t0.yzx * t;
			float rcpDet = 1f / math.csum(t0.zxy * m2);
			return math.float3x3(m2, m3, m4) * rcpDet;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000C5D8 File Offset: 0x0000A7D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float determinant(float3x3 m)
		{
			float3 c0 = m.c0;
			float3 c = m.c1;
			float3 c2 = m.c2;
			float m2 = c.y * c2.z - c.z * c2.y;
			float m3 = c0.y * c2.z - c0.z * c2.y;
			float m4 = c0.y * c.z - c0.z * c.y;
			return c0.x * m2 - c.x * m3 + c2.x * m4;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000C66C File Offset: 0x0000A86C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float3x3 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint3(1899745391U, 1966790317U, 3516359879U) + math.asuint(v.c1) * math.uint3(3050356579U, 4178586719U, 2558655391U) + math.asuint(v.c2) * math.uint3(1453413133U, 2152428077U, 1938706661U)) + 1338588197U;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000C6FC File Offset: 0x0000A8FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(float3x3 v)
		{
			return math.asuint(v.c0) * math.uint3(3439609253U, 3535343003U, 3546061613U) + math.asuint(v.c1) * math.uint3(2702024231U, 1452124841U, 1966089551U) + math.asuint(v.c2) * math.uint3(2668168249U, 1587512777U, 2353831999U) + 3101256173U;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000C789 File Offset: 0x0000A989
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(float3 c0, float3 c1, float3 c2, float3 c3)
		{
			return new float3x4(c0, c1, c2, c3);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000C794 File Offset: 0x0000A994
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23)
		{
			return new float3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000C7BA File Offset: 0x0000A9BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(float v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000C7C2 File Offset: 0x0000A9C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(bool v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000C7CA File Offset: 0x0000A9CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(bool3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000C7D2 File Offset: 0x0000A9D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(int v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000C7DA File Offset: 0x0000A9DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(int3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000C7E2 File Offset: 0x0000A9E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(uint v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000C7EA File Offset: 0x0000A9EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(uint3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000C7F2 File Offset: 0x0000A9F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(double v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000C7FA File Offset: 0x0000A9FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 float3x4(double3x4 v)
		{
			return new float3x4(v);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000C804 File Offset: 0x0000AA04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 transpose(float3x4 v)
		{
			return math.float4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000C89C File Offset: 0x0000AA9C
		public static float3x4 fastinverse(float3x4 m)
		{
			float3 c3 = m.c0;
			float3 c = m.c1;
			float3 c2 = m.c2;
			float3 pos = m.c3;
			float3 r0 = math.float3(c3.x, c.x, c2.x);
			float3 r = math.float3(c3.y, c.y, c2.y);
			float3 r2 = math.float3(c3.z, c.z, c2.z);
			pos = -(r0 * pos.x + r * pos.y + r2 * pos.z);
			return math.float3x4(r0, r, r2, pos);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000C950 File Offset: 0x0000AB50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float3x4 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint3(4192899797U, 3271228601U, 1634639009U) + math.asuint(v.c1) * math.uint3(3318036811U, 3404170631U, 2048213449U) + math.asuint(v.c2) * math.uint3(4164671783U, 1780759499U, 1352369353U) + math.asuint(v.c3) * math.uint3(2446407751U, 1391928079U, 3475533443U)) + 3777095341U;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000CA08 File Offset: 0x0000AC08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(float3x4 v)
		{
			return math.asuint(v.c0) * math.uint3(3385463369U, 1773538433U, 3773525029U) + math.asuint(v.c1) * math.uint3(4131962539U, 1809525511U, 4016293529U) + math.asuint(v.c2) * math.uint3(2416021567U, 2828384717U, 2636362241U) + math.asuint(v.c3) * math.uint3(1258410977U, 1952565773U, 2037535609U) + 3592785499U;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000CABE File Offset: 0x0000ACBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float x, float y, float z, float w)
		{
			return new float4(x, y, z, w);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000CAC9 File Offset: 0x0000ACC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float x, float y, float2 zw)
		{
			return new float4(x, y, zw);
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000CAD3 File Offset: 0x0000ACD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float x, float2 yz, float w)
		{
			return new float4(x, yz, w);
		}

		// Token: 0x060001FC RID: 508 RVA: 0x0000CADD File Offset: 0x0000ACDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float x, float3 yzw)
		{
			return new float4(x, yzw);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000CAE6 File Offset: 0x0000ACE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float2 xy, float z, float w)
		{
			return new float4(xy, z, w);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float2 xy, float2 zw)
		{
			return new float4(xy, zw);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000CAF9 File Offset: 0x0000ACF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float3 xyz, float w)
		{
			return new float4(xyz, w);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000CB02 File Offset: 0x0000AD02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float4 xyzw)
		{
			return new float4(xyzw);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000CB0A File Offset: 0x0000AD0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(float v)
		{
			return new float4(v);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000CB12 File Offset: 0x0000AD12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(bool v)
		{
			return new float4(v);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000CB1A File Offset: 0x0000AD1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(bool4 v)
		{
			return new float4(v);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000CB22 File Offset: 0x0000AD22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(int v)
		{
			return new float4(v);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000CB2A File Offset: 0x0000AD2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(int4 v)
		{
			return new float4(v);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000CB32 File Offset: 0x0000AD32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(uint v)
		{
			return new float4(v);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000CB3A File Offset: 0x0000AD3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(uint4 v)
		{
			return new float4(v);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000CB42 File Offset: 0x0000AD42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(half v)
		{
			return new float4(v);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000CB4A File Offset: 0x0000AD4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(half4 v)
		{
			return new float4(v);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000CB52 File Offset: 0x0000AD52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(double v)
		{
			return new float4(v);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000CB5A File Offset: 0x0000AD5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 float4(double4 v)
		{
			return new float4(v);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000CB62 File Offset: 0x0000AD62
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float4 v)
		{
			return math.csum(math.asuint(v) * math.uint4(3868600063U, 3170963179U, 2632835537U, 1136528209U)) + 2944626401U;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000CB93 File Offset: 0x0000AD93
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(float4 v)
		{
			return math.asuint(v) * math.uint4(2972762423U, 1417889653U, 2080514593U, 2731544287U) + 2828498809U;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000CBC3 File Offset: 0x0000ADC3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float shuffle(float4 left, float4 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000CBCD File Offset: 0x0000ADCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 shuffle(float4 left, float4 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.float2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000CBE4 File Offset: 0x0000ADE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 shuffle(float4 left, float4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.float3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000CC04 File Offset: 0x0000AE04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 shuffle(float4 left, float4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.float4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000CC30 File Offset: 0x0000AE30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float select_shuffle_component(float4 a, float4 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.LeftW:
				return a.w;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			case math.ShuffleComponent.RightW:
				return b.w;
			default:
				throw new ArgumentException("Invalid shuffle component: " + component.ToString());
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000CCB9 File Offset: 0x0000AEB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(float4 c0, float4 c1)
		{
			return new float4x2(c0, c1);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000CCC2 File Offset: 0x0000AEC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(float m00, float m01, float m10, float m11, float m20, float m21, float m30, float m31)
		{
			return new float4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000CCD5 File Offset: 0x0000AED5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(float v)
		{
			return new float4x2(v);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000CCDD File Offset: 0x0000AEDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(bool v)
		{
			return new float4x2(v);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000CCE5 File Offset: 0x0000AEE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(bool4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000CCED File Offset: 0x0000AEED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(int v)
		{
			return new float4x2(v);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000CCF5 File Offset: 0x0000AEF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(int4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000CCFD File Offset: 0x0000AEFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(uint v)
		{
			return new float4x2(v);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000CD05 File Offset: 0x0000AF05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(uint4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000CD0D File Offset: 0x0000AF0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(double v)
		{
			return new float4x2(v);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000CD15 File Offset: 0x0000AF15
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 float4x2(double4x2 v)
		{
			return new float4x2(v);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000CD20 File Offset: 0x0000AF20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 transpose(float4x2 v)
		{
			return math.float2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000CD8C File Offset: 0x0000AF8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float4x2 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint4(2864955997U, 3525118277U, 2298260269U, 1632478733U) + math.asuint(v.c1) * math.uint4(1537393931U, 2353355467U, 3441847433U, 4052036147U)) + 2011389559U;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(float4x2 v)
		{
			return math.asuint(v.c0) * math.uint4(2252224297U, 3784421429U, 1750626223U, 3571447507U) + math.asuint(v.c1) * math.uint4(3412283213U, 2601761069U, 1254033427U, 2248573027U) + 3612677113U;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000CE6A File Offset: 0x0000B06A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(float4 c0, float4 c1, float4 c2)
		{
			return new float4x3(c0, c1, c2);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000CE74 File Offset: 0x0000B074
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(float m00, float m01, float m02, float m10, float m11, float m12, float m20, float m21, float m22, float m30, float m31, float m32)
		{
			return new float4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000CE9A File Offset: 0x0000B09A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(float v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000CEA2 File Offset: 0x0000B0A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(bool v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000CEAA File Offset: 0x0000B0AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(bool4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000CEB2 File Offset: 0x0000B0B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(int v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000CEBA File Offset: 0x0000B0BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(int4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000CEC2 File Offset: 0x0000B0C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(uint v)
		{
			return new float4x3(v);
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000CECA File Offset: 0x0000B0CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(uint4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000CED2 File Offset: 0x0000B0D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(double v)
		{
			return new float4x3(v);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000CEDA File Offset: 0x0000B0DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 float4x3(double4x3 v)
		{
			return new float4x3(v);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 transpose(float4x3 v)
		{
			return math.float3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000CF7C File Offset: 0x0000B17C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float4x3 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint4(3309258581U, 1770373673U, 3778261171U, 3286279097U) + math.asuint(v.c1) * math.uint4(4264629071U, 1898591447U, 2641864091U, 1229113913U) + math.asuint(v.c2) * math.uint4(3020867117U, 1449055807U, 2479033387U, 3702457169U)) + 1845824257U;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000D01C File Offset: 0x0000B21C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(float4x3 v)
		{
			return math.asuint(v.c0) * math.uint4(1963973621U, 2134758553U, 1391111867U, 1167706003U) + math.asuint(v.c1) * math.uint4(2209736489U, 3261535807U, 1740411209U, 2910609089U) + math.asuint(v.c2) * math.uint4(2183822701U, 3029516053U, 3547472099U, 2057487037U) + 3781937309U;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(float4 c0, float4 c1, float4 c2, float4 c3)
		{
			return new float4x4(c0, c1, c2, c3);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(float m00, float m01, float m02, float m03, float m10, float m11, float m12, float m13, float m20, float m21, float m22, float m23, float m30, float m31, float m32, float m33)
		{
			return new float4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000D0F2 File Offset: 0x0000B2F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(float v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000D0FA File Offset: 0x0000B2FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(bool v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000D102 File Offset: 0x0000B302
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(bool4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000D10A File Offset: 0x0000B30A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(int v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000D112 File Offset: 0x0000B312
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(int4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000D11A File Offset: 0x0000B31A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(uint v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000D122 File Offset: 0x0000B322
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(uint4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000D12A File Offset: 0x0000B32A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(double v)
		{
			return new float4x4(v);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000D132 File Offset: 0x0000B332
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(double4x4 v)
		{
			return new float4x4(v);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D13C File Offset: 0x0000B33C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 rotate(float4x4 a, float3 b)
		{
			return (a.c0 * b.x + a.c1 * b.y + a.c2 * b.z).xyz;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D190 File Offset: 0x0000B390
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 transform(float4x4 a, float3 b)
		{
			return (a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3).xyz;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000D1F0 File Offset: 0x0000B3F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 transpose(float4x4 v)
		{
			return math.float4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		public static float4x4 inverse(float4x4 m)
		{
			float4 c0 = m.c0;
			float4 c = m.c1;
			float4 c2 = m.c2;
			float4 c3 = m.c3;
			float4 r0y_r1y_r0x_r1x = math.movelh(c, c0);
			float4 r0z_r1z_r0w_r1w = math.movelh(c2, c3);
			float4 r2y_r3y_r2x_r3x = math.movehl(c0, c);
			float4 r2z_r3z_r2w_r3w = math.movehl(c3, c2);
			float4 @float = math.shuffle(c, c0, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightY, math.ShuffleComponent.RightZ);
			float4 r1z_r2z_r1w_r2w = math.shuffle(c2, c3, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightY, math.ShuffleComponent.RightZ);
			float4 r3y_r0y_r3x_r0x = math.shuffle(c, c0, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightW, math.ShuffleComponent.RightX);
			float4 r3z_r0z_r3w_r0w = math.shuffle(c2, c3, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightW, math.ShuffleComponent.RightX);
			float4 r0_wzyx = math.shuffle(r0z_r1z_r0w_r1w, r0y_r1y_r0x_r1x, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.RightZ);
			float4 r1_wzyx = math.shuffle(r0z_r1z_r0w_r1w, r0y_r1y_r0x_r1x, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightY, math.ShuffleComponent.RightW);
			float4 r2_wzyx = math.shuffle(r2z_r3z_r2w_r3w, r2y_r3y_r2x_r3x, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.RightZ);
			float4 r3_wzyx = math.shuffle(r2z_r3z_r2w_r3w, r2y_r3y_r2x_r3x, math.ShuffleComponent.LeftW, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightY, math.ShuffleComponent.RightW);
			float4 r0_xyzw = math.shuffle(r0y_r1y_r0x_r1x, r0z_r1z_r0w_r1w, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.RightZ);
			float4 float2 = @float * r2z_r3z_r2w_r3w - r1z_r2z_r1w_r2w * r2y_r3y_r2x_r3x;
			float4 inner02_13 = r0y_r1y_r0x_r1x * r2z_r3z_r2w_r3w - r0z_r1z_r0w_r1w * r2y_r3y_r2x_r3x;
			float4 inner30_ = r3z_r0z_r3w_r0w * r0y_r1y_r0x_r1x - r3y_r0y_r3x_r0x * r0z_r1z_r0w_r1w;
			float4 inner12 = math.shuffle(float2, float2, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightX);
			float4 inner13 = math.shuffle(float2, float2, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW, math.ShuffleComponent.RightY);
			float4 inner14 = math.shuffle(inner02_13, inner02_13, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightX);
			float4 inner15 = math.shuffle(inner02_13, inner02_13, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW, math.ShuffleComponent.RightY);
			float4 minors0 = r3_wzyx * inner12 - r2_wzyx * inner15 + r1_wzyx * inner13;
			float4 denom = r0_xyzw * minors0;
			denom += math.shuffle(denom, denom, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightW, math.ShuffleComponent.RightZ);
			denom -= math.shuffle(denom, denom, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightX, math.ShuffleComponent.RightX);
			float4 rcp_denom_ppnn = math.float4(1f) / denom;
			float4x4 res;
			res.c0 = minors0 * rcp_denom_ppnn;
			float4 inner16 = math.shuffle(inner30_, inner30_, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightX);
			float4 inner17 = math.shuffle(inner30_, inner30_, math.ShuffleComponent.LeftY, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW, math.ShuffleComponent.RightY);
			float4 minors = r2_wzyx * inner16 - r0_wzyx * inner13 - r3_wzyx * inner14;
			res.c1 = minors * rcp_denom_ppnn;
			float4 minors2 = r0_wzyx * inner15 - r1_wzyx * inner16 - r3_wzyx * inner17;
			res.c2 = minors2 * rcp_denom_ppnn;
			float4 minors3 = r1_wzyx * inner14 - r0_wzyx * inner12 + r2_wzyx * inner17;
			res.c3 = minors3 * rcp_denom_ppnn;
			return res;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D544 File Offset: 0x0000B744
		public static float4x4 fastinverse(float4x4 m)
		{
			float4 c3 = m.c0;
			float4 c = m.c1;
			float4 c2 = m.c2;
			float4 pos = m.c3;
			float4 zero = math.float4(0);
			float4 t0 = math.unpacklo(c3, c2);
			float4 t = math.unpacklo(c, zero);
			float4 @float = math.unpackhi(c3, c2);
			float4 t2 = math.unpackhi(c, zero);
			float4 r0 = math.unpacklo(t0, t);
			float4 r = math.unpackhi(t0, t);
			float4 r2 = math.unpacklo(@float, t2);
			pos = -(r0 * pos.x + r * pos.y + r2 * pos.z);
			pos.w = 1f;
			return math.float4x4(r0, r, r2, pos);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D604 File Offset: 0x0000B804
		public static float determinant(float4x4 m)
		{
			float4 c0 = m.c0;
			float4 c = m.c1;
			float4 c2 = m.c2;
			float4 c3 = m.c3;
			float m2 = c.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c.z * c3.w - c.w * c3.z) + c3.y * (c.z * c2.w - c.w * c2.z);
			float m3 = c0.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c0.z * c3.w - c0.w * c3.z) + c3.y * (c0.z * c2.w - c0.w * c2.z);
			float m4 = c0.y * (c.z * c3.w - c.w * c3.z) - c.y * (c0.z * c3.w - c0.w * c3.z) + c3.y * (c0.z * c.w - c0.w * c.z);
			float m5 = c0.y * (c.z * c2.w - c.w * c2.z) - c.y * (c0.z * c2.w - c0.w * c2.z) + c2.y * (c0.z * c.w - c0.w * c.z);
			return c0.x * m2 - c.x * m3 + c2.x * m4 - c3.x * m5;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000D7FC File Offset: 0x0000B9FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(float4x4 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint4(3299952959U, 3121178323U, 2948522579U, 1531026433U) + math.asuint(v.c1) * math.uint4(1365086453U, 3969870067U, 4192899797U, 3271228601U) + math.asuint(v.c2) * math.uint4(1634639009U, 3318036811U, 3404170631U, 2048213449U) + math.asuint(v.c3) * math.uint4(4164671783U, 1780759499U, 1352369353U, 2446407751U)) + 1391928079U;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(float4x4 v)
		{
			return math.asuint(v.c0) * math.uint4(3475533443U, 3777095341U, 3385463369U, 1773538433U) + math.asuint(v.c1) * math.uint4(3773525029U, 4131962539U, 1809525511U, 4016293529U) + math.asuint(v.c2) * math.uint4(2416021567U, 2828384717U, 2636362241U, 1258410977U) + math.asuint(v.c3) * math.uint4(1952565773U, 2037535609U, 3592785499U, 3996716183U) + 2626301701U;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D992 File Offset: 0x0000BB92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half half(half x)
		{
			return new half(x);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000D99A File Offset: 0x0000BB9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half half(float v)
		{
			return new half(v);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D9A2 File Offset: 0x0000BBA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half half(double v)
		{
			return new half(v);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000D9AA File Offset: 0x0000BBAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(half v)
		{
			return (uint)v.value * 1952372791U + 2171534173U;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D9BE File Offset: 0x0000BBBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(half x, half y)
		{
			return new half2(x, y);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000D9C7 File Offset: 0x0000BBC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(half2 xy)
		{
			return new half2(xy);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000D9CF File Offset: 0x0000BBCF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(half v)
		{
			return new half2(v);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000D9D7 File Offset: 0x0000BBD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(float v)
		{
			return new half2(v);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000D9DF File Offset: 0x0000BBDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(float2 v)
		{
			return new half2(v);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000D9E7 File Offset: 0x0000BBE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(double v)
		{
			return new half2(v);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000D9EF File Offset: 0x0000BBEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half2 half2(double2 v)
		{
			return new half2(v);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000D9F7 File Offset: 0x0000BBF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(half2 v)
		{
			return math.csum(math.uint2((uint)v.x.value, (uint)v.y.value) * math.uint2(1851936439U, 1938025801U)) + 3712598587U;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000DA33 File Offset: 0x0000BC33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(half2 v)
		{
			return math.uint2((uint)v.x.value, (uint)v.y.value) * math.uint2(3956330501U, 2437373431U) + 1441286183U;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000DA6E File Offset: 0x0000BC6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(half x, half y, half z)
		{
			return new half3(x, y, z);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000DA78 File Offset: 0x0000BC78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(half x, half2 yz)
		{
			return new half3(x, yz);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000DA81 File Offset: 0x0000BC81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(half2 xy, half z)
		{
			return new half3(xy, z);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000DA8A File Offset: 0x0000BC8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(half3 xyz)
		{
			return new half3(xyz);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000DA92 File Offset: 0x0000BC92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(half v)
		{
			return new half3(v);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000DA9A File Offset: 0x0000BC9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(float v)
		{
			return new half3(v);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000DAA2 File Offset: 0x0000BCA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(float3 v)
		{
			return new half3(v);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000DAAA File Offset: 0x0000BCAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(double v)
		{
			return new half3(v);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000DAB2 File Offset: 0x0000BCB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half3 half3(double3 v)
		{
			return new half3(v);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000DABC File Offset: 0x0000BCBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(half3 v)
		{
			return math.csum(math.uint3((uint)v.x.value, (uint)v.y.value, (uint)v.z.value) * math.uint3(1750611407U, 3285396193U, 3110507567U)) + 4271396531U;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000DB14 File Offset: 0x0000BD14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(half3 v)
		{
			return math.uint3((uint)v.x.value, (uint)v.y.value, (uint)v.z.value) * math.uint3(4198118021U, 2908068253U, 3705492289U) + 2497566569U;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000DB6A File Offset: 0x0000BD6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half x, half y, half z, half w)
		{
			return new half4(x, y, z, w);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000DB75 File Offset: 0x0000BD75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half x, half y, half2 zw)
		{
			return new half4(x, y, zw);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000DB7F File Offset: 0x0000BD7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half x, half2 yz, half w)
		{
			return new half4(x, yz, w);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000DB89 File Offset: 0x0000BD89
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half x, half3 yzw)
		{
			return new half4(x, yzw);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000DB92 File Offset: 0x0000BD92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half2 xy, half z, half w)
		{
			return new half4(xy, z, w);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000DB9C File Offset: 0x0000BD9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half2 xy, half2 zw)
		{
			return new half4(xy, zw);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000DBA5 File Offset: 0x0000BDA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half3 xyz, half w)
		{
			return new half4(xyz, w);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000DBAE File Offset: 0x0000BDAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half4 xyzw)
		{
			return new half4(xyzw);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000DBB6 File Offset: 0x0000BDB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(half v)
		{
			return new half4(v);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000DBBE File Offset: 0x0000BDBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(float v)
		{
			return new half4(v);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000DBC6 File Offset: 0x0000BDC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(float4 v)
		{
			return new half4(v);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000DBCE File Offset: 0x0000BDCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(double v)
		{
			return new half4(v);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000DBD6 File Offset: 0x0000BDD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static half4 half4(double4 v)
		{
			return new half4(v);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(half4 v)
		{
			return math.csum(math.uint4((uint)v.x.value, (uint)v.y.value, (uint)v.z.value, (uint)v.w.value) * math.uint4(1952372791U, 2631698677U, 4200781601U, 2119021007U)) + 1760485621U;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000DC48 File Offset: 0x0000BE48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(half4 v)
		{
			return math.uint4((uint)v.x.value, (uint)v.y.value, (uint)v.z.value, (uint)v.w.value) * math.uint4(3157985881U, 2171534173U, 2723054263U, 1168253063U) + 4228926523U;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000DCAE File Offset: 0x0000BEAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(int x, int y)
		{
			return new int2(x, y);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000DCB7 File Offset: 0x0000BEB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(int2 xy)
		{
			return new int2(xy);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000DCBF File Offset: 0x0000BEBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(int v)
		{
			return new int2(v);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000DCC7 File Offset: 0x0000BEC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(bool v)
		{
			return new int2(v);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000DCCF File Offset: 0x0000BECF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(bool2 v)
		{
			return new int2(v);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000DCD7 File Offset: 0x0000BED7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(uint v)
		{
			return new int2(v);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000DCDF File Offset: 0x0000BEDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(uint2 v)
		{
			return new int2(v);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000DCE7 File Offset: 0x0000BEE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(float v)
		{
			return new int2(v);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000DCEF File Offset: 0x0000BEEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(float2 v)
		{
			return new int2(v);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000DCF7 File Offset: 0x0000BEF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(double v)
		{
			return new int2(v);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000DCFF File Offset: 0x0000BEFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 int2(double2 v)
		{
			return new int2(v);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000DD07 File Offset: 0x0000BF07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int2 v)
		{
			return math.csum(math.asuint(v) * math.uint2(2209710647U, 2201894441U)) + 2849577407U;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000DD2E File Offset: 0x0000BF2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(int2 v)
		{
			return math.asuint(v) * math.uint2(3287031191U, 3098675399U) + 1564399943U;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000DD54 File Offset: 0x0000BF54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int shuffle(int2 left, int2 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000DD5E File Offset: 0x0000BF5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 shuffle(int2 left, int2 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.int2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000DD75 File Offset: 0x0000BF75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 shuffle(int2 left, int2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.int3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000DD95 File Offset: 0x0000BF95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 shuffle(int2 left, int2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.int4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int select_shuffle_component(int2 a, int2 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000DE25 File Offset: 0x0000C025
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(int2 c0, int2 c1)
		{
			return new int2x2(c0, c1);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000DE2E File Offset: 0x0000C02E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(int m00, int m01, int m10, int m11)
		{
			return new int2x2(m00, m01, m10, m11);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000DE39 File Offset: 0x0000C039
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(int v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000DE41 File Offset: 0x0000C041
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(bool v)
		{
			return new int2x2(v);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000DE49 File Offset: 0x0000C049
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(bool2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000DE51 File Offset: 0x0000C051
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(uint v)
		{
			return new int2x2(v);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000DE59 File Offset: 0x0000C059
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(uint2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000DE61 File Offset: 0x0000C061
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(float v)
		{
			return new int2x2(v);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000DE69 File Offset: 0x0000C069
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(float2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000DE71 File Offset: 0x0000C071
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(double v)
		{
			return new int2x2(v);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000DE79 File Offset: 0x0000C079
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 int2x2(double2x2 v)
		{
			return new int2x2(v);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000DE81 File Offset: 0x0000C081
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 transpose(int2x2 v)
		{
			return math.int2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000DEB4 File Offset: 0x0000C0B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int determinant(int2x2 m)
		{
			int x = m.c0.x;
			int b = m.c1.x;
			int c = m.c0.y;
			int d = m.c1.y;
			return x * d - b * c;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int2x2 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint2(3784421429U, 1750626223U) + math.asuint(v.c1) * math.uint2(3571447507U, 3412283213U)) + 2601761069U;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000DF54 File Offset: 0x0000C154
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(int2x2 v)
		{
			return math.asuint(v.c0) * math.uint2(1254033427U, 2248573027U) + math.asuint(v.c1) * math.uint2(3612677113U, 1521739981U) + 1735296007U;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000DFAE File Offset: 0x0000C1AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(int2 c0, int2 c1, int2 c2)
		{
			return new int2x3(c0, c1, c2);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000DFB8 File Offset: 0x0000C1B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(int m00, int m01, int m02, int m10, int m11, int m12)
		{
			return new int2x3(m00, m01, m02, m10, m11, m12);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000DFC7 File Offset: 0x0000C1C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(int v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000DFCF File Offset: 0x0000C1CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(bool v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000DFD7 File Offset: 0x0000C1D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(bool2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000DFDF File Offset: 0x0000C1DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(uint v)
		{
			return new int2x3(v);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000DFE7 File Offset: 0x0000C1E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(uint2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000DFEF File Offset: 0x0000C1EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(float v)
		{
			return new int2x3(v);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000DFF7 File Offset: 0x0000C1F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(float2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000DFFF File Offset: 0x0000C1FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(double v)
		{
			return new int2x3(v);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000E007 File Offset: 0x0000C207
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 int2x3(double2x3 v)
		{
			return new int2x3(v);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000E010 File Offset: 0x0000C210
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 transpose(int2x3 v)
		{
			return math.int3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000E064 File Offset: 0x0000C264
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int2x3 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint2(3404170631U, 2048213449U) + math.asuint(v.c1) * math.uint2(4164671783U, 1780759499U) + math.asuint(v.c2) * math.uint2(1352369353U, 2446407751U)) + 1391928079U;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(int2x3 v)
		{
			return math.asuint(v.c0) * math.uint2(3475533443U, 3777095341U) + math.asuint(v.c1) * math.uint2(3385463369U, 1773538433U) + math.asuint(v.c2) * math.uint2(3773525029U, 4131962539U) + 1809525511U;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000E162 File Offset: 0x0000C362
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(int2 c0, int2 c1, int2 c2, int2 c3)
		{
			return new int2x4(c0, c1, c2, c3);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000E16D File Offset: 0x0000C36D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13)
		{
			return new int2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000E180 File Offset: 0x0000C380
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(int v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000E188 File Offset: 0x0000C388
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(bool v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000E190 File Offset: 0x0000C390
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(bool2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000E198 File Offset: 0x0000C398
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(uint v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(uint2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000E1A8 File Offset: 0x0000C3A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(float v)
		{
			return new int2x4(v);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(float2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(double v)
		{
			return new int2x4(v);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 int2x4(double2x4 v)
		{
			return new int2x4(v);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000E1C8 File Offset: 0x0000C3C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 transpose(int2x4 v)
		{
			return math.int4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000E234 File Offset: 0x0000C434
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int2x4 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint2(2057338067U, 2942577577U) + math.asuint(v.c1) * math.uint2(2834440507U, 2671762487U) + math.asuint(v.c2) * math.uint2(2892026051U, 2455987759U) + math.asuint(v.c3) * math.uint2(3868600063U, 3170963179U)) + 2632835537U;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(int2x4 v)
		{
			return math.asuint(v.c0) * math.uint2(1136528209U, 2944626401U) + math.asuint(v.c1) * math.uint2(2972762423U, 1417889653U) + math.asuint(v.c2) * math.uint2(2080514593U, 2731544287U) + math.asuint(v.c3) * math.uint2(2828498809U, 2669441947U) + 1260114311U;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000E37A File Offset: 0x0000C57A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(int x, int y, int z)
		{
			return new int3(x, y, z);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000E384 File Offset: 0x0000C584
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(int x, int2 yz)
		{
			return new int3(x, yz);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000E38D File Offset: 0x0000C58D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(int2 xy, int z)
		{
			return new int3(xy, z);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000E396 File Offset: 0x0000C596
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(int3 xyz)
		{
			return new int3(xyz);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000E39E File Offset: 0x0000C59E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(int v)
		{
			return new int3(v);
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000E3A6 File Offset: 0x0000C5A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(bool v)
		{
			return new int3(v);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000E3AE File Offset: 0x0000C5AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(bool3 v)
		{
			return new int3(v);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000E3B6 File Offset: 0x0000C5B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(uint v)
		{
			return new int3(v);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000E3BE File Offset: 0x0000C5BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(uint3 v)
		{
			return new int3(v);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000E3C6 File Offset: 0x0000C5C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(float v)
		{
			return new int3(v);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000E3CE File Offset: 0x0000C5CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(float3 v)
		{
			return new int3(v);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000E3D6 File Offset: 0x0000C5D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(double v)
		{
			return new int3(v);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000E3DE File Offset: 0x0000C5DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 int3(double3 v)
		{
			return new int3(v);
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000E3E6 File Offset: 0x0000C5E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int3 v)
		{
			return math.csum(math.asuint(v) * math.uint3(1283419601U, 1210229737U, 2864955997U)) + 3525118277U;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000E412 File Offset: 0x0000C612
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(int3 v)
		{
			return math.asuint(v) * math.uint3(2298260269U, 1632478733U, 1537393931U) + 2353355467U;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000E43D File Offset: 0x0000C63D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int shuffle(int3 left, int3 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000E447 File Offset: 0x0000C647
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 shuffle(int3 left, int3 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.int2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000E45E File Offset: 0x0000C65E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 shuffle(int3 left, int3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.int3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000E47E File Offset: 0x0000C67E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 shuffle(int3 left, int3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.int4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000E4A8 File Offset: 0x0000C6A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int select_shuffle_component(int3 a, int3 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000E51F File Offset: 0x0000C71F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(int3 c0, int3 c1)
		{
			return new int3x2(c0, c1);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000E528 File Offset: 0x0000C728
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(int m00, int m01, int m10, int m11, int m20, int m21)
		{
			return new int3x2(m00, m01, m10, m11, m20, m21);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000E537 File Offset: 0x0000C737
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(int v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000E53F File Offset: 0x0000C73F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(bool v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000E547 File Offset: 0x0000C747
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(bool3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000E54F File Offset: 0x0000C74F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(uint v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000E557 File Offset: 0x0000C757
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(uint3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000E55F File Offset: 0x0000C75F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(float v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000E567 File Offset: 0x0000C767
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(float3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000E56F File Offset: 0x0000C76F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(double v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000E577 File Offset: 0x0000C777
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 int3x2(double3x2 v)
		{
			return new int3x2(v);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000E580 File Offset: 0x0000C780
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 transpose(int3x2 v)
		{
			return math.int2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int3x2 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint3(3678265601U, 2070747979U, 1480171127U) + math.asuint(v.c1) * math.uint3(1588341193U, 4234155257U, 1811310911U)) + 2635799963U;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000E63C File Offset: 0x0000C83C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(int3x2 v)
		{
			return math.asuint(v.c0) * math.uint3(4165137857U, 2759770933U, 2759319383U) + math.asuint(v.c1) * math.uint3(3299952959U, 3121178323U, 2948522579U) + 1531026433U;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(int3 c0, int3 c1, int3 c2)
		{
			return new int3x3(c0, c1, c2);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000E6AC File Offset: 0x0000C8AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(int m00, int m01, int m02, int m10, int m11, int m12, int m20, int m21, int m22)
		{
			return new int3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(int v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(bool v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(bool3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000E6E4 File Offset: 0x0000C8E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(uint v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(uint3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(float v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(float3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000E704 File Offset: 0x0000C904
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(double v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000E70C File Offset: 0x0000C90C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 int3x3(double3x3 v)
		{
			return new int3x3(v);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000E714 File Offset: 0x0000C914
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 transpose(int3x3 v)
		{
			return math.int3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000E78C File Offset: 0x0000C98C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int determinant(int3x3 m)
		{
			int3 c0 = m.c0;
			int3 c = m.c1;
			int3 c2 = m.c2;
			int m2 = c.y * c2.z - c.z * c2.y;
			int m3 = c0.y * c2.z - c0.z * c2.y;
			int m4 = c0.y * c.z - c0.z * c.y;
			return c0.x * m2 - c.x * m3 + c2.x * m4;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000E820 File Offset: 0x0000CA20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int3x3 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint3(2479033387U, 3702457169U, 1845824257U) + math.asuint(v.c1) * math.uint3(1963973621U, 2134758553U, 1391111867U) + math.asuint(v.c2) * math.uint3(1167706003U, 2209736489U, 3261535807U)) + 1740411209U;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000E8B0 File Offset: 0x0000CAB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(int3x3 v)
		{
			return math.asuint(v.c0) * math.uint3(2910609089U, 2183822701U, 3029516053U) + math.asuint(v.c1) * math.uint3(3547472099U, 2057487037U, 3781937309U) + math.asuint(v.c2) * math.uint3(2057338067U, 2942577577U, 2834440507U) + 2671762487U;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000E93D File Offset: 0x0000CB3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(int3 c0, int3 c1, int3 c2, int3 c3)
		{
			return new int3x4(c0, c1, c2, c3);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000E948 File Offset: 0x0000CB48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13, int m20, int m21, int m22, int m23)
		{
			return new int3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000E96E File Offset: 0x0000CB6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(int v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000E976 File Offset: 0x0000CB76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(bool v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000E97E File Offset: 0x0000CB7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(bool3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E986 File Offset: 0x0000CB86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(uint v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E98E File Offset: 0x0000CB8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(uint3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000E996 File Offset: 0x0000CB96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(float v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000E99E File Offset: 0x0000CB9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(float3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000E9A6 File Offset: 0x0000CBA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(double v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000E9AE File Offset: 0x0000CBAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 int3x4(double3x4 v)
		{
			return new int3x4(v);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 transpose(int3x4 v)
		{
			return math.int4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000EA50 File Offset: 0x0000CC50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int3x4 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint3(1521739981U, 1735296007U, 3010324327U) + math.asuint(v.c1) * math.uint3(1875523709U, 2937008387U, 3835713223U) + math.asuint(v.c2) * math.uint3(2216526373U, 3375971453U, 3559829411U) + math.asuint(v.c3) * math.uint3(3652178029U, 2544260129U, 2013864031U)) + 2627668003U;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000EB08 File Offset: 0x0000CD08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(int3x4 v)
		{
			return math.asuint(v.c0) * math.uint3(1520214331U, 2949502447U, 2827819133U) + math.asuint(v.c1) * math.uint3(3480140317U, 2642994593U, 3940484981U) + math.asuint(v.c2) * math.uint3(1954192763U, 1091696537U, 3052428017U) + math.asuint(v.c3) * math.uint3(4253034763U, 2338696631U, 3757372771U) + 1885959949U;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000EBBE File Offset: 0x0000CDBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int x, int y, int z, int w)
		{
			return new int4(x, y, z, w);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000EBC9 File Offset: 0x0000CDC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int x, int y, int2 zw)
		{
			return new int4(x, y, zw);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000EBD3 File Offset: 0x0000CDD3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int x, int2 yz, int w)
		{
			return new int4(x, yz, w);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000EBDD File Offset: 0x0000CDDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int x, int3 yzw)
		{
			return new int4(x, yzw);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000EBE6 File Offset: 0x0000CDE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int2 xy, int z, int w)
		{
			return new int4(xy, z, w);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000EBF0 File Offset: 0x0000CDF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int2 xy, int2 zw)
		{
			return new int4(xy, zw);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000EBF9 File Offset: 0x0000CDF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int3 xyz, int w)
		{
			return new int4(xyz, w);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000EC02 File Offset: 0x0000CE02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int4 xyzw)
		{
			return new int4(xyzw);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000EC0A File Offset: 0x0000CE0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(int v)
		{
			return new int4(v);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000EC12 File Offset: 0x0000CE12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(bool v)
		{
			return new int4(v);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000EC1A File Offset: 0x0000CE1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(bool4 v)
		{
			return new int4(v);
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x0000EC22 File Offset: 0x0000CE22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(uint v)
		{
			return new int4(v);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000EC2A File Offset: 0x0000CE2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(uint4 v)
		{
			return new int4(v);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000EC32 File Offset: 0x0000CE32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(float v)
		{
			return new int4(v);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000EC3A File Offset: 0x0000CE3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(float4 v)
		{
			return new int4(v);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000EC42 File Offset: 0x0000CE42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(double v)
		{
			return new int4(v);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000EC4A File Offset: 0x0000CE4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 int4(double4 v)
		{
			return new int4(v);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x0000EC52 File Offset: 0x0000CE52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int4 v)
		{
			return math.csum(math.asuint(v) * math.uint4(1845824257U, 1963973621U, 2134758553U, 1391111867U)) + 1167706003U;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000EC83 File Offset: 0x0000CE83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(int4 v)
		{
			return math.asuint(v) * math.uint4(2209736489U, 3261535807U, 1740411209U, 2910609089U) + 2183822701U;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000ECB3 File Offset: 0x0000CEB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int shuffle(int4 left, int4 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000ECBD File Offset: 0x0000CEBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 shuffle(int4 left, int4 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.int2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 shuffle(int4 left, int4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.int3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 shuffle(int4 left, int4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.int4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000ED20 File Offset: 0x0000CF20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int select_shuffle_component(int4 a, int4 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.LeftW:
				return a.w;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			case math.ShuffleComponent.RightW:
				return b.w;
			default:
				throw new ArgumentException("Invalid shuffle component: " + component.ToString());
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000EDA9 File Offset: 0x0000CFA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(int4 c0, int4 c1)
		{
			return new int4x2(c0, c1);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000EDB2 File Offset: 0x0000CFB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(int m00, int m01, int m10, int m11, int m20, int m21, int m30, int m31)
		{
			return new int4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000EDC5 File Offset: 0x0000CFC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(int v)
		{
			return new int4x2(v);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000EDCD File Offset: 0x0000CFCD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(bool v)
		{
			return new int4x2(v);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000EDD5 File Offset: 0x0000CFD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(bool4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000EDDD File Offset: 0x0000CFDD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(uint v)
		{
			return new int4x2(v);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000EDE5 File Offset: 0x0000CFE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(uint4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000EDED File Offset: 0x0000CFED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(float v)
		{
			return new int4x2(v);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000EDF5 File Offset: 0x0000CFF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(float4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000EDFD File Offset: 0x0000CFFD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(double v)
		{
			return new int4x2(v);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000EE05 File Offset: 0x0000D005
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 int4x2(double4x2 v)
		{
			return new int4x2(v);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000EE10 File Offset: 0x0000D010
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 transpose(int4x2 v)
		{
			return math.int2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000EE7C File Offset: 0x0000D07C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int4x2 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint4(4205774813U, 1650214333U, 3388112843U, 1831150513U) + math.asuint(v.c1) * math.uint4(1848374953U, 3430200247U, 2209710647U, 2201894441U)) + 2849577407U;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000EEEC File Offset: 0x0000D0EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(int4x2 v)
		{
			return math.asuint(v.c0) * math.uint4(3287031191U, 3098675399U, 1564399943U, 1148435377U) + math.asuint(v.c1) * math.uint4(3416333663U, 1750611407U, 3285396193U, 3110507567U) + 4271396531U;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000EF5A File Offset: 0x0000D15A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(int4 c0, int4 c1, int4 c2)
		{
			return new int4x3(c0, c1, c2);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000EF64 File Offset: 0x0000D164
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(int m00, int m01, int m02, int m10, int m11, int m12, int m20, int m21, int m22, int m30, int m31, int m32)
		{
			return new int4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000EF8A File Offset: 0x0000D18A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(int v)
		{
			return new int4x3(v);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000EF92 File Offset: 0x0000D192
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(bool v)
		{
			return new int4x3(v);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000EF9A File Offset: 0x0000D19A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(bool4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000EFA2 File Offset: 0x0000D1A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(uint v)
		{
			return new int4x3(v);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000EFAA File Offset: 0x0000D1AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(uint4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000EFB2 File Offset: 0x0000D1B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(float v)
		{
			return new int4x3(v);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000EFBA File Offset: 0x0000D1BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(float4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000EFC2 File Offset: 0x0000D1C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(double v)
		{
			return new int4x3(v);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000EFCA File Offset: 0x0000D1CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 int4x3(double4x3 v)
		{
			return new int4x3(v);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 transpose(int4x3 v)
		{
			return math.int3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000F06C File Offset: 0x0000D26C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int4x3 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint4(1773538433U, 3773525029U, 4131962539U, 1809525511U) + math.asuint(v.c1) * math.uint4(4016293529U, 2416021567U, 2828384717U, 2636362241U) + math.asuint(v.c2) * math.uint4(1258410977U, 1952565773U, 2037535609U, 3592785499U)) + 3996716183U;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000F10C File Offset: 0x0000D30C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(int4x3 v)
		{
			return math.asuint(v.c0) * math.uint4(2626301701U, 1306289417U, 2096137163U, 1548578029U) + math.asuint(v.c1) * math.uint4(4178800919U, 3898072289U, 4129428421U, 2631575897U) + math.asuint(v.c2) * math.uint4(2854656703U, 3578504047U, 4245178297U, 2173281923U) + 2973357649U;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(int4 c0, int4 c1, int4 c2, int4 c3)
		{
			return new int4x4(c0, c1, c2, c3);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(int m00, int m01, int m02, int m03, int m10, int m11, int m12, int m13, int m20, int m21, int m22, int m23, int m30, int m31, int m32, int m33)
		{
			return new int4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000F1E2 File Offset: 0x0000D3E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(int v)
		{
			return new int4x4(v);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000F1EA File Offset: 0x0000D3EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(bool v)
		{
			return new int4x4(v);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000F1F2 File Offset: 0x0000D3F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(bool4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000F1FA File Offset: 0x0000D3FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(uint v)
		{
			return new int4x4(v);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000F202 File Offset: 0x0000D402
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(uint4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000F20A File Offset: 0x0000D40A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(float v)
		{
			return new int4x4(v);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000F212 File Offset: 0x0000D412
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(float4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000F21A File Offset: 0x0000D41A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(double v)
		{
			return new int4x4(v);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F222 File Offset: 0x0000D422
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 int4x4(double4x4 v)
		{
			return new int4x4(v);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000F22C File Offset: 0x0000D42C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 transpose(int4x4 v)
		{
			return math.int4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000F2F0 File Offset: 0x0000D4F0
		public static int determinant(int4x4 m)
		{
			int4 c0 = m.c0;
			int4 c = m.c1;
			int4 c2 = m.c2;
			int4 c3 = m.c3;
			int m2 = c.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c.z * c3.w - c.w * c3.z) + c3.y * (c.z * c2.w - c.w * c2.z);
			int m3 = c0.y * (c2.z * c3.w - c2.w * c3.z) - c2.y * (c0.z * c3.w - c0.w * c3.z) + c3.y * (c0.z * c2.w - c0.w * c2.z);
			int m4 = c0.y * (c.z * c3.w - c.w * c3.z) - c.y * (c0.z * c3.w - c0.w * c3.z) + c3.y * (c0.z * c.w - c0.w * c.z);
			int m5 = c0.y * (c.z * c2.w - c.w * c2.z) - c.y * (c0.z * c2.w - c0.w * c2.z) + c2.y * (c0.z * c.w - c0.w * c.z);
			return c0.x * m2 - c.x * m3 + c2.x * m4 - c3.x * m5;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000F4E8 File Offset: 0x0000D6E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(int4x4 v)
		{
			return math.csum(math.asuint(v.c0) * math.uint4(1562056283U, 2265541847U, 1283419601U, 1210229737U) + math.asuint(v.c1) * math.uint4(2864955997U, 3525118277U, 2298260269U, 1632478733U) + math.asuint(v.c2) * math.uint4(1537393931U, 2353355467U, 3441847433U, 4052036147U) + math.asuint(v.c3) * math.uint4(2011389559U, 2252224297U, 3784421429U, 1750626223U)) + 3571447507U;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000F5B4 File Offset: 0x0000D7B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(int4x4 v)
		{
			return math.asuint(v.c0) * math.uint4(3412283213U, 2601761069U, 1254033427U, 2248573027U) + math.asuint(v.c1) * math.uint4(3612677113U, 1521739981U, 1735296007U, 3010324327U) + math.asuint(v.c2) * math.uint4(1875523709U, 2937008387U, 3835713223U, 2216526373U) + math.asuint(v.c3) * math.uint4(3375971453U, 3559829411U, 3652178029U, 2544260129U) + 2013864031U;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000F67E File Offset: 0x0000D87E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int asint(uint x)
		{
			return (int)(*(&x));
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000F684 File Offset: 0x0000D884
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int2 asint(uint2 x)
		{
			return *(int2*)(&x);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000F68E File Offset: 0x0000D88E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int3 asint(uint3 x)
		{
			return *(int3*)(&x);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000F698 File Offset: 0x0000D898
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int4 asint(uint4 x)
		{
			return *(int4*)(&x);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000F67E File Offset: 0x0000D87E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int asint(float x)
		{
			return *(int*)(&x);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000F684 File Offset: 0x0000D884
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int2 asint(float2 x)
		{
			return *(int2*)(&x);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000F68E File Offset: 0x0000D88E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int3 asint(float3 x)
		{
			return *(int3*)(&x);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000F698 File Offset: 0x0000D898
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int4 asint(float4 x)
		{
			return *(int4*)(&x);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000F6A2 File Offset: 0x0000D8A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint asuint(int x)
		{
			return (uint)x;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000F6A5 File Offset: 0x0000D8A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint2 asuint(int2 x)
		{
			return *(uint2*)(&x);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000F6AF File Offset: 0x0000D8AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint3 asuint(int3 x)
		{
			return *(uint3*)(&x);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000F6B9 File Offset: 0x0000D8B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint4 asuint(int4 x)
		{
			return *(uint4*)(&x);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000F6C3 File Offset: 0x0000D8C3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint asuint(float x)
		{
			return *(uint*)(&x);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000F6A5 File Offset: 0x0000D8A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint2 asuint(float2 x)
		{
			return *(uint2*)(&x);
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000F6AF File Offset: 0x0000D8AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint3 asuint(float3 x)
		{
			return *(uint3*)(&x);
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000F6B9 File Offset: 0x0000D8B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint4 asuint(float4 x)
		{
			return *(uint4*)(&x);
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000F6A2 File Offset: 0x0000D8A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long aslong(ulong x)
		{
			return (long)x;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000F6C9 File Offset: 0x0000D8C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static long aslong(double x)
		{
			return *(long*)(&x);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000F6A2 File Offset: 0x0000D8A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong asulong(long x)
		{
			return (ulong)x;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000F6C9 File Offset: 0x0000D8C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong asulong(double x)
		{
			return (ulong)(*(long*)(&x));
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000F6CF File Offset: 0x0000D8CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float asfloat(int x)
		{
			return *(float*)(&x);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000F6D5 File Offset: 0x0000D8D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float2 asfloat(int2 x)
		{
			return *(float2*)(&x);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000F6DF File Offset: 0x0000D8DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float3 asfloat(int3 x)
		{
			return *(float3*)(&x);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000F6E9 File Offset: 0x0000D8E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float4 asfloat(int4 x)
		{
			return *(float4*)(&x);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000F6CF File Offset: 0x0000D8CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float asfloat(uint x)
		{
			return *(float*)(&x);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000F6D5 File Offset: 0x0000D8D5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float2 asfloat(uint2 x)
		{
			return *(float2*)(&x);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000F6DF File Offset: 0x0000D8DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float3 asfloat(uint3 x)
		{
			return *(float3*)(&x);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000F6E9 File Offset: 0x0000D8E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float4 asfloat(uint4 x)
		{
			return *(float4*)(&x);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
		public static int bitmask(bool4 value)
		{
			int mask = 0;
			if (value.x)
			{
				mask |= 1;
			}
			if (value.y)
			{
				mask |= 2;
			}
			if (value.z)
			{
				mask |= 4;
			}
			if (value.w)
			{
				mask |= 8;
			}
			return mask;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000F734 File Offset: 0x0000D934
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double asdouble(long x)
		{
			return *(double*)(&x);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000F734 File Offset: 0x0000D934
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double asdouble(ulong x)
		{
			return *(double*)(&x);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000F73A File Offset: 0x0000D93A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool isfinite(float x)
		{
			return math.abs(x) < float.PositiveInfinity;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000F749 File Offset: 0x0000D949
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 isfinite(float2 x)
		{
			return math.abs(x) < float.PositiveInfinity;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000F75B File Offset: 0x0000D95B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 isfinite(float3 x)
		{
			return math.abs(x) < float.PositiveInfinity;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000F76D File Offset: 0x0000D96D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 isfinite(float4 x)
		{
			return math.abs(x) < float.PositiveInfinity;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000F77F File Offset: 0x0000D97F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool isfinite(double x)
		{
			return math.abs(x) < double.PositiveInfinity;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000F792 File Offset: 0x0000D992
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 isfinite(double2 x)
		{
			return math.abs(x) < double.PositiveInfinity;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 isfinite(double3 x)
		{
			return math.abs(x) < double.PositiveInfinity;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000F7BE File Offset: 0x0000D9BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 isfinite(double4 x)
		{
			return math.abs(x) < double.PositiveInfinity;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000F7D4 File Offset: 0x0000D9D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool isinf(float x)
		{
			return math.abs(x) == float.PositiveInfinity;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000F7E3 File Offset: 0x0000D9E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 isinf(float2 x)
		{
			return math.abs(x) == float.PositiveInfinity;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000F7F5 File Offset: 0x0000D9F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 isinf(float3 x)
		{
			return math.abs(x) == float.PositiveInfinity;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000F807 File Offset: 0x0000DA07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 isinf(float4 x)
		{
			return math.abs(x) == float.PositiveInfinity;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000F819 File Offset: 0x0000DA19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool isinf(double x)
		{
			return math.abs(x) == double.PositiveInfinity;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000F82C File Offset: 0x0000DA2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 isinf(double2 x)
		{
			return math.abs(x) == double.PositiveInfinity;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000F842 File Offset: 0x0000DA42
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 isinf(double3 x)
		{
			return math.abs(x) == double.PositiveInfinity;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000F858 File Offset: 0x0000DA58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 isinf(double4 x)
		{
			return math.abs(x) == double.PositiveInfinity;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000F86E File Offset: 0x0000DA6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool isnan(float x)
		{
			return (math.asuint(x) & 2147483647U) > 2139095040U;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000F883 File Offset: 0x0000DA83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 isnan(float2 x)
		{
			return (math.asuint(x) & 2147483647U) > 2139095040U;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000F89F File Offset: 0x0000DA9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 isnan(float3 x)
		{
			return (math.asuint(x) & 2147483647U) > 2139095040U;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000F8BB File Offset: 0x0000DABB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 isnan(float4 x)
		{
			return (math.asuint(x) & 2147483647U) > 2139095040U;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000F8D7 File Offset: 0x0000DAD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool isnan(double x)
		{
			return (math.asulong(x) & 9223372036854775807UL) > 9218868437227405312UL;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000F8F4 File Offset: 0x0000DAF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 isnan(double2 x)
		{
			return math.bool2((math.asulong(x.x) & 9223372036854775807UL) > 9218868437227405312UL, (math.asulong(x.y) & 9223372036854775807UL) > 9218868437227405312UL);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000F948 File Offset: 0x0000DB48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 isnan(double3 x)
		{
			return math.bool3((math.asulong(x.x) & 9223372036854775807UL) > 9218868437227405312UL, (math.asulong(x.y) & 9223372036854775807UL) > 9218868437227405312UL, (math.asulong(x.z) & 9223372036854775807UL) > 9218868437227405312UL);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000F9BC File Offset: 0x0000DBBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 isnan(double4 x)
		{
			return math.bool4((math.asulong(x.x) & 9223372036854775807UL) > 9218868437227405312UL, (math.asulong(x.y) & 9223372036854775807UL) > 9218868437227405312UL, (math.asulong(x.z) & 9223372036854775807UL) > 9218868437227405312UL, (math.asulong(x.w) & 9223372036854775807UL) > 9218868437227405312UL);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000FA4E File Offset: 0x0000DC4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ispow2(int x)
		{
			return x > 0 && (x & (x - 1)) == 0;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000FA5E File Offset: 0x0000DC5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 ispow2(int2 x)
		{
			return new bool2(math.ispow2(x.x), math.ispow2(x.y));
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000FA7B File Offset: 0x0000DC7B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 ispow2(int3 x)
		{
			return new bool3(math.ispow2(x.x), math.ispow2(x.y), math.ispow2(x.z));
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000FAA3 File Offset: 0x0000DCA3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 ispow2(int4 x)
		{
			return new bool4(math.ispow2(x.x), math.ispow2(x.y), math.ispow2(x.z), math.ispow2(x.w));
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000FAD6 File Offset: 0x0000DCD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ispow2(uint x)
		{
			return x > 0U && (x & (x - 1U)) == 0U;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000FAE6 File Offset: 0x0000DCE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 ispow2(uint2 x)
		{
			return new bool2(math.ispow2(x.x), math.ispow2(x.y));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000FB03 File Offset: 0x0000DD03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool3 ispow2(uint3 x)
		{
			return new bool3(math.ispow2(x.x), math.ispow2(x.y), math.ispow2(x.z));
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000FB2B File Offset: 0x0000DD2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool4 ispow2(uint4 x)
		{
			return new bool4(math.ispow2(x.x), math.ispow2(x.y), math.ispow2(x.z), math.ispow2(x.w));
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000FB5E File Offset: 0x0000DD5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int min(int x, int y)
		{
			if (x >= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000FB67 File Offset: 0x0000DD67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 min(int2 x, int2 y)
		{
			return new int2(math.min(x.x, y.x), math.min(x.y, y.y));
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000FB90 File Offset: 0x0000DD90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 min(int3 x, int3 y)
		{
			return new int3(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z));
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 min(int4 x, int4 y)
		{
			return new int4(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z), math.min(x.w, y.w));
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000FC22 File Offset: 0x0000DE22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint min(uint x, uint y)
		{
			if (x >= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000FC2B File Offset: 0x0000DE2B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 min(uint2 x, uint2 y)
		{
			return new uint2(math.min(x.x, y.x), math.min(x.y, y.y));
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000FC54 File Offset: 0x0000DE54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 min(uint3 x, uint3 y)
		{
			return new uint3(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z));
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000FC90 File Offset: 0x0000DE90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 min(uint4 x, uint4 y)
		{
			return new uint4(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z), math.min(x.w, y.w));
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000FB5E File Offset: 0x0000DD5E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long min(long x, long y)
		{
			if (x >= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000FC22 File Offset: 0x0000DE22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong min(ulong x, ulong y)
		{
			if (x >= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000FCE6 File Offset: 0x0000DEE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float min(float x, float y)
		{
			if (!float.IsNaN(y) && x >= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000FCF7 File Offset: 0x0000DEF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 min(float2 x, float2 y)
		{
			return new float2(math.min(x.x, y.x), math.min(x.y, y.y));
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000FD20 File Offset: 0x0000DF20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 min(float3 x, float3 y)
		{
			return new float3(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z));
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000FD5C File Offset: 0x0000DF5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 min(float4 x, float4 y)
		{
			return new float4(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z), math.min(x.w, y.w));
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000FDB2 File Offset: 0x0000DFB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double min(double x, double y)
		{
			if (!double.IsNaN(y) && x >= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000FDC3 File Offset: 0x0000DFC3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 min(double2 x, double2 y)
		{
			return new double2(math.min(x.x, y.x), math.min(x.y, y.y));
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 min(double3 x, double3 y)
		{
			return new double3(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z));
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000FE28 File Offset: 0x0000E028
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 min(double4 x, double4 y)
		{
			return new double4(math.min(x.x, y.x), math.min(x.y, y.y), math.min(x.z, y.z), math.min(x.w, y.w));
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000FE7E File Offset: 0x0000E07E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int max(int x, int y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000FE87 File Offset: 0x0000E087
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 max(int2 x, int2 y)
		{
			return new int2(math.max(x.x, y.x), math.max(x.y, y.y));
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 max(int3 x, int3 y)
		{
			return new int3(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z));
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000FEEC File Offset: 0x0000E0EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 max(int4 x, int4 y)
		{
			return new int4(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z), math.max(x.w, y.w));
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000FF42 File Offset: 0x0000E142
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint max(uint x, uint y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0000FF4B File Offset: 0x0000E14B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 max(uint2 x, uint2 y)
		{
			return new uint2(math.max(x.x, y.x), math.max(x.y, y.y));
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000FF74 File Offset: 0x0000E174
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 max(uint3 x, uint3 y)
		{
			return new uint3(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z));
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 max(uint4 x, uint4 y)
		{
			return new uint4(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z), math.max(x.w, y.w));
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0000FE7E File Offset: 0x0000E07E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long max(long x, long y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000FF42 File Offset: 0x0000E142
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong max(ulong x, ulong y)
		{
			if (x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00010006 File Offset: 0x0000E206
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float max(float x, float y)
		{
			if (!float.IsNaN(y) && x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00010017 File Offset: 0x0000E217
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 max(float2 x, float2 y)
		{
			return new float2(math.max(x.x, y.x), math.max(x.y, y.y));
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00010040 File Offset: 0x0000E240
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 max(float3 x, float3 y)
		{
			return new float3(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z));
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001007C File Offset: 0x0000E27C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 max(float4 x, float4 y)
		{
			return new float4(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z), math.max(x.w, y.w));
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000100D2 File Offset: 0x0000E2D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double max(double x, double y)
		{
			if (!double.IsNaN(y) && x <= y)
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x000100E3 File Offset: 0x0000E2E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 max(double2 x, double2 y)
		{
			return new double2(math.max(x.x, y.x), math.max(x.y, y.y));
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001010C File Offset: 0x0000E30C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 max(double3 x, double3 y)
		{
			return new double3(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z));
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00010148 File Offset: 0x0000E348
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 max(double4 x, double4 y)
		{
			return new double4(math.max(x.x, y.x), math.max(x.y, y.y), math.max(x.z, y.z), math.max(x.w, y.w));
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001019E File Offset: 0x0000E39E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float lerp(float start, float end, float t)
		{
			return start + t * (end - start);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000101A7 File Offset: 0x0000E3A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 lerp(float2 start, float2 end, float t)
		{
			return start + t * (end - start);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x000101BC File Offset: 0x0000E3BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 lerp(float3 start, float3 end, float t)
		{
			return start + t * (end - start);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x000101D1 File Offset: 0x0000E3D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 lerp(float4 start, float4 end, float t)
		{
			return start + t * (end - start);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x000101E6 File Offset: 0x0000E3E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 lerp(float2 start, float2 end, float2 t)
		{
			return start + t * (end - start);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x000101FB File Offset: 0x0000E3FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 lerp(float3 start, float3 end, float3 t)
		{
			return start + t * (end - start);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00010210 File Offset: 0x0000E410
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 lerp(float4 start, float4 end, float4 t)
		{
			return start + t * (end - start);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001019E File Offset: 0x0000E39E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double lerp(double start, double end, double t)
		{
			return start + t * (end - start);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00010225 File Offset: 0x0000E425
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 lerp(double2 start, double2 end, double t)
		{
			return start + t * (end - start);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001023A File Offset: 0x0000E43A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 lerp(double3 start, double3 end, double t)
		{
			return start + t * (end - start);
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001024F File Offset: 0x0000E44F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 lerp(double4 start, double4 end, double t)
		{
			return start + t * (end - start);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00010264 File Offset: 0x0000E464
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 lerp(double2 start, double2 end, double2 t)
		{
			return start + t * (end - start);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00010279 File Offset: 0x0000E479
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 lerp(double3 start, double3 end, double3 t)
		{
			return start + t * (end - start);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001028E File Offset: 0x0000E48E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 lerp(double4 start, double4 end, double4 t)
		{
			return start + t * (end - start);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000102A3 File Offset: 0x0000E4A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float unlerp(float start, float end, float x)
		{
			return (x - start) / (end - start);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000102AC File Offset: 0x0000E4AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 unlerp(float2 start, float2 end, float2 x)
		{
			return (x - start) / (end - start);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000102C1 File Offset: 0x0000E4C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 unlerp(float3 start, float3 end, float3 x)
		{
			return (x - start) / (end - start);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000102D6 File Offset: 0x0000E4D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 unlerp(float4 start, float4 end, float4 x)
		{
			return (x - start) / (end - start);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000102A3 File Offset: 0x0000E4A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double unlerp(double start, double end, double x)
		{
			return (x - start) / (end - start);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000102EB File Offset: 0x0000E4EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 unlerp(double2 start, double2 end, double2 x)
		{
			return (x - start) / (end - start);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00010300 File Offset: 0x0000E500
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 unlerp(double3 start, double3 end, double3 x)
		{
			return (x - start) / (end - start);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00010315 File Offset: 0x0000E515
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 unlerp(double4 start, double4 end, double4 x)
		{
			return (x - start) / (end - start);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001032A File Offset: 0x0000E52A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float remap(float srcStart, float srcEnd, float dstStart, float dstEnd, float x)
		{
			return math.lerp(dstStart, dstEnd, math.unlerp(srcStart, srcEnd, x));
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001033C File Offset: 0x0000E53C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 remap(float2 srcStart, float2 srcEnd, float2 dstStart, float2 dstEnd, float2 x)
		{
			return math.lerp(dstStart, dstEnd, math.unlerp(srcStart, srcEnd, x));
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001034E File Offset: 0x0000E54E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 remap(float3 srcStart, float3 srcEnd, float3 dstStart, float3 dstEnd, float3 x)
		{
			return math.lerp(dstStart, dstEnd, math.unlerp(srcStart, srcEnd, x));
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00010360 File Offset: 0x0000E560
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 remap(float4 srcStart, float4 srcEnd, float4 dstStart, float4 dstEnd, float4 x)
		{
			return math.lerp(dstStart, dstEnd, math.unlerp(srcStart, srcEnd, x));
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00010372 File Offset: 0x0000E572
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double remap(double srcStart, double srcEnd, double dstStart, double dstEnd, double x)
		{
			return math.lerp(dstStart, dstEnd, math.unlerp(srcStart, srcEnd, x));
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00010384 File Offset: 0x0000E584
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 remap(double2 srcStart, double2 srcEnd, double2 dstStart, double2 dstEnd, double2 x)
		{
			return math.lerp(dstStart, dstEnd, math.unlerp(srcStart, srcEnd, x));
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00010396 File Offset: 0x0000E596
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 remap(double3 srcStart, double3 srcEnd, double3 dstStart, double3 dstEnd, double3 x)
		{
			return math.lerp(dstStart, dstEnd, math.unlerp(srcStart, srcEnd, x));
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000103A8 File Offset: 0x0000E5A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 remap(double4 srcStart, double4 srcEnd, double4 dstStart, double4 dstEnd, double4 x)
		{
			return math.lerp(dstStart, dstEnd, math.unlerp(srcStart, srcEnd, x));
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000103BA File Offset: 0x0000E5BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int mad(int mulA, int mulB, int addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000103C1 File Offset: 0x0000E5C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mad(int2 mulA, int2 mulB, int2 addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000103D0 File Offset: 0x0000E5D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mad(int3 mulA, int3 mulB, int3 addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000103DF File Offset: 0x0000E5DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mad(int4 mulA, int4 mulB, int4 addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000103BA File Offset: 0x0000E5BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint mad(uint mulA, uint mulB, uint addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000103EE File Offset: 0x0000E5EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mad(uint2 mulA, uint2 mulB, uint2 addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000103FD File Offset: 0x0000E5FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mad(uint3 mulA, uint3 mulB, uint3 addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001040C File Offset: 0x0000E60C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mad(uint4 mulA, uint4 mulB, uint4 addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000103BA File Offset: 0x0000E5BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long mad(long mulA, long mulB, long addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000103BA File Offset: 0x0000E5BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong mad(ulong mulA, ulong mulB, ulong addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000103BA File Offset: 0x0000E5BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float mad(float mulA, float mulB, float addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001041B File Offset: 0x0000E61B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mad(float2 mulA, float2 mulB, float2 addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001042A File Offset: 0x0000E62A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mad(float3 mulA, float3 mulB, float3 addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00010439 File Offset: 0x0000E639
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mad(float4 mulA, float4 mulB, float4 addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000103BA File Offset: 0x0000E5BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double mad(double mulA, double mulB, double addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00010448 File Offset: 0x0000E648
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mad(double2 mulA, double2 mulB, double2 addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00010457 File Offset: 0x0000E657
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mad(double3 mulA, double3 mulB, double3 addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00010466 File Offset: 0x0000E666
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mad(double4 mulA, double4 mulB, double4 addC)
		{
			return mulA * mulB + addC;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00010475 File Offset: 0x0000E675
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int clamp(int valueToClamp, int lowerBound, int upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00010484 File Offset: 0x0000E684
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 clamp(int2 valueToClamp, int2 lowerBound, int2 upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00010493 File Offset: 0x0000E693
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 clamp(int3 valueToClamp, int3 lowerBound, int3 upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000104A2 File Offset: 0x0000E6A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 clamp(int4 valueToClamp, int4 lowerBound, int4 upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000104B1 File Offset: 0x0000E6B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint clamp(uint valueToClamp, uint lowerBound, uint upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000104C0 File Offset: 0x0000E6C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 clamp(uint2 valueToClamp, uint2 lowerBound, uint2 upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000104CF File Offset: 0x0000E6CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 clamp(uint3 valueToClamp, uint3 lowerBound, uint3 upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000104DE File Offset: 0x0000E6DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 clamp(uint4 valueToClamp, uint4 lowerBound, uint4 upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000104ED File Offset: 0x0000E6ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long clamp(long valueToClamp, long lowerBound, long upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000104FC File Offset: 0x0000E6FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong clamp(ulong valueToClamp, ulong lowerBound, ulong upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001050B File Offset: 0x0000E70B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float clamp(float valueToClamp, float lowerBound, float upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001051A File Offset: 0x0000E71A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 clamp(float2 valueToClamp, float2 lowerBound, float2 upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00010529 File Offset: 0x0000E729
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 clamp(float3 valueToClamp, float3 lowerBound, float3 upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00010538 File Offset: 0x0000E738
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 clamp(float4 valueToClamp, float4 lowerBound, float4 upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00010547 File Offset: 0x0000E747
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double clamp(double valueToClamp, double lowerBound, double upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00010556 File Offset: 0x0000E756
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 clamp(double2 valueToClamp, double2 lowerBound, double2 upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00010565 File Offset: 0x0000E765
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 clamp(double3 valueToClamp, double3 lowerBound, double3 upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00010574 File Offset: 0x0000E774
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 clamp(double4 valueToClamp, double4 lowerBound, double4 upperBound)
		{
			return math.max(lowerBound, math.min(upperBound, valueToClamp));
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00010583 File Offset: 0x0000E783
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float saturate(float x)
		{
			return math.clamp(x, 0f, 1f);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00010595 File Offset: 0x0000E795
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 saturate(float2 x)
		{
			return math.clamp(x, new float2(0f), new float2(1f));
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000105B1 File Offset: 0x0000E7B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 saturate(float3 x)
		{
			return math.clamp(x, new float3(0f), new float3(1f));
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x000105CD File Offset: 0x0000E7CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 saturate(float4 x)
		{
			return math.clamp(x, new float4(0f), new float4(1f));
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000105E9 File Offset: 0x0000E7E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double saturate(double x)
		{
			return math.clamp(x, 0.0, 1.0);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00010603 File Offset: 0x0000E803
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 saturate(double2 x)
		{
			return math.clamp(x, new double2(0.0), new double2(1.0));
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00010627 File Offset: 0x0000E827
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 saturate(double3 x)
		{
			return math.clamp(x, new double3(0.0), new double3(1.0));
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001064B File Offset: 0x0000E84B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 saturate(double4 x)
		{
			return math.clamp(x, new double4(0.0), new double4(1.0));
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001066F File Offset: 0x0000E86F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int abs(int x)
		{
			return math.max(-x, x);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00010679 File Offset: 0x0000E879
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 abs(int2 x)
		{
			return math.max(-x, x);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00010687 File Offset: 0x0000E887
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 abs(int3 x)
		{
			return math.max(-x, x);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00010695 File Offset: 0x0000E895
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 abs(int4 x)
		{
			return math.max(-x, x);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000106A3 File Offset: 0x0000E8A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long abs(long x)
		{
			return math.max(-x, x);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000106AD File Offset: 0x0000E8AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float abs(float x)
		{
			return math.asfloat(math.asuint(x) & 2147483647U);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000106C0 File Offset: 0x0000E8C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 abs(float2 x)
		{
			return math.asfloat(math.asuint(x) & 2147483647U);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000106D7 File Offset: 0x0000E8D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 abs(float3 x)
		{
			return math.asfloat(math.asuint(x) & 2147483647U);
		}

		// Token: 0x060003DD RID: 989 RVA: 0x000106EE File Offset: 0x0000E8EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 abs(float4 x)
		{
			return math.asfloat(math.asuint(x) & 2147483647U);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00010705 File Offset: 0x0000E905
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double abs(double x)
		{
			return math.asdouble(math.asulong(x) & 9223372036854775807UL);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001071C File Offset: 0x0000E91C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 abs(double2 x)
		{
			return math.double2(math.asdouble(math.asulong(x.x) & 9223372036854775807UL), math.asdouble(math.asulong(x.y) & 9223372036854775807UL));
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00010758 File Offset: 0x0000E958
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 abs(double3 x)
		{
			return math.double3(math.asdouble(math.asulong(x.x) & 9223372036854775807UL), math.asdouble(math.asulong(x.y) & 9223372036854775807UL), math.asdouble(math.asulong(x.z) & 9223372036854775807UL));
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000107B8 File Offset: 0x0000E9B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 abs(double4 x)
		{
			return math.double4(math.asdouble(math.asulong(x.x) & 9223372036854775807UL), math.asdouble(math.asulong(x.y) & 9223372036854775807UL), math.asdouble(math.asulong(x.z) & 9223372036854775807UL), math.asdouble(math.asulong(x.w) & 9223372036854775807UL));
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00010832 File Offset: 0x0000EA32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int dot(int x, int y)
		{
			return x * y;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00010837 File Offset: 0x0000EA37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int dot(int2 x, int2 y)
		{
			return x.x * y.x + x.y * y.y;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00010854 File Offset: 0x0000EA54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int dot(int3 x, int3 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001087F File Offset: 0x0000EA7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int dot(int4 x, int4 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00010832 File Offset: 0x0000EA32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint dot(uint x, uint y)
		{
			return x * y;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000108B8 File Offset: 0x0000EAB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint dot(uint2 x, uint2 y)
		{
			return x.x * y.x + x.y * y.y;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x000108D5 File Offset: 0x0000EAD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint dot(uint3 x, uint3 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00010900 File Offset: 0x0000EB00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint dot(uint4 x, uint4 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00010832 File Offset: 0x0000EA32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float dot(float x, float y)
		{
			return x * y;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00010939 File Offset: 0x0000EB39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float dot(float2 x, float2 y)
		{
			return x.x * y.x + x.y * y.y;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00010956 File Offset: 0x0000EB56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float dot(float3 x, float3 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00010981 File Offset: 0x0000EB81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float dot(float4 x, float4 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00010832 File Offset: 0x0000EA32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double dot(double x, double y)
		{
			return x * y;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000109BA File Offset: 0x0000EBBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double dot(double2 x, double2 y)
		{
			return x.x * y.x + x.y * y.y;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000109D7 File Offset: 0x0000EBD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double dot(double3 x, double3 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00010A02 File Offset: 0x0000EC02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double dot(double4 x, double4 y)
		{
			return x.x * y.x + x.y * y.y + x.z * y.z + x.w * y.w;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00010A3B File Offset: 0x0000EC3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float tan(float x)
		{
			return (float)Math.Tan((double)x);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00010A45 File Offset: 0x0000EC45
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 tan(float2 x)
		{
			return new float2(math.tan(x.x), math.tan(x.y));
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00010A62 File Offset: 0x0000EC62
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 tan(float3 x)
		{
			return new float3(math.tan(x.x), math.tan(x.y), math.tan(x.z));
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00010A8A File Offset: 0x0000EC8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 tan(float4 x)
		{
			return new float4(math.tan(x.x), math.tan(x.y), math.tan(x.z), math.tan(x.w));
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00010ABD File Offset: 0x0000ECBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double tan(double x)
		{
			return Math.Tan(x);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00010AC5 File Offset: 0x0000ECC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 tan(double2 x)
		{
			return new double2(math.tan(x.x), math.tan(x.y));
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00010AE2 File Offset: 0x0000ECE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 tan(double3 x)
		{
			return new double3(math.tan(x.x), math.tan(x.y), math.tan(x.z));
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00010B0A File Offset: 0x0000ED0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 tan(double4 x)
		{
			return new double4(math.tan(x.x), math.tan(x.y), math.tan(x.z), math.tan(x.w));
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00010B3D File Offset: 0x0000ED3D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float tanh(float x)
		{
			return (float)Math.Tanh((double)x);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00010B47 File Offset: 0x0000ED47
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 tanh(float2 x)
		{
			return new float2(math.tanh(x.x), math.tanh(x.y));
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00010B64 File Offset: 0x0000ED64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 tanh(float3 x)
		{
			return new float3(math.tanh(x.x), math.tanh(x.y), math.tanh(x.z));
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00010B8C File Offset: 0x0000ED8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 tanh(float4 x)
		{
			return new float4(math.tanh(x.x), math.tanh(x.y), math.tanh(x.z), math.tanh(x.w));
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00010BBF File Offset: 0x0000EDBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double tanh(double x)
		{
			return Math.Tanh(x);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00010BC7 File Offset: 0x0000EDC7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 tanh(double2 x)
		{
			return new double2(math.tanh(x.x), math.tanh(x.y));
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00010BE4 File Offset: 0x0000EDE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 tanh(double3 x)
		{
			return new double3(math.tanh(x.x), math.tanh(x.y), math.tanh(x.z));
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00010C0C File Offset: 0x0000EE0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 tanh(double4 x)
		{
			return new double4(math.tanh(x.x), math.tanh(x.y), math.tanh(x.z), math.tanh(x.w));
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00010C3F File Offset: 0x0000EE3F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float atan(float x)
		{
			return (float)Math.Atan((double)x);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00010C49 File Offset: 0x0000EE49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 atan(float2 x)
		{
			return new float2(math.atan(x.x), math.atan(x.y));
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00010C66 File Offset: 0x0000EE66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 atan(float3 x)
		{
			return new float3(math.atan(x.x), math.atan(x.y), math.atan(x.z));
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00010C8E File Offset: 0x0000EE8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 atan(float4 x)
		{
			return new float4(math.atan(x.x), math.atan(x.y), math.atan(x.z), math.atan(x.w));
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00010CC1 File Offset: 0x0000EEC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double atan(double x)
		{
			return Math.Atan(x);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00010CC9 File Offset: 0x0000EEC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 atan(double2 x)
		{
			return new double2(math.atan(x.x), math.atan(x.y));
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00010CE6 File Offset: 0x0000EEE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 atan(double3 x)
		{
			return new double3(math.atan(x.x), math.atan(x.y), math.atan(x.z));
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00010D0E File Offset: 0x0000EF0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 atan(double4 x)
		{
			return new double4(math.atan(x.x), math.atan(x.y), math.atan(x.z), math.atan(x.w));
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00010D41 File Offset: 0x0000EF41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00010D4D File Offset: 0x0000EF4D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 atan2(float2 y, float2 x)
		{
			return new float2(math.atan2(y.x, x.x), math.atan2(y.y, x.y));
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00010D76 File Offset: 0x0000EF76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 atan2(float3 y, float3 x)
		{
			return new float3(math.atan2(y.x, x.x), math.atan2(y.y, x.y), math.atan2(y.z, x.z));
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 atan2(float4 y, float4 x)
		{
			return new float4(math.atan2(y.x, x.x), math.atan2(y.y, x.y), math.atan2(y.z, x.z), math.atan2(y.w, x.w));
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00010E06 File Offset: 0x0000F006
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double atan2(double y, double x)
		{
			return Math.Atan2(y, x);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00010E0F File Offset: 0x0000F00F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 atan2(double2 y, double2 x)
		{
			return new double2(math.atan2(y.x, x.x), math.atan2(y.y, x.y));
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00010E38 File Offset: 0x0000F038
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 atan2(double3 y, double3 x)
		{
			return new double3(math.atan2(y.x, x.x), math.atan2(y.y, x.y), math.atan2(y.z, x.z));
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00010E74 File Offset: 0x0000F074
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 atan2(double4 y, double4 x)
		{
			return new double4(math.atan2(y.x, x.x), math.atan2(y.y, x.y), math.atan2(y.z, x.z), math.atan2(y.w, x.w));
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00010ECA File Offset: 0x0000F0CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cos(float x)
		{
			return (float)Math.Cos((double)x);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00010ED4 File Offset: 0x0000F0D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 cos(float2 x)
		{
			return new float2(math.cos(x.x), math.cos(x.y));
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00010EF1 File Offset: 0x0000F0F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 cos(float3 x)
		{
			return new float3(math.cos(x.x), math.cos(x.y), math.cos(x.z));
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00010F19 File Offset: 0x0000F119
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 cos(float4 x)
		{
			return new float4(math.cos(x.x), math.cos(x.y), math.cos(x.z), math.cos(x.w));
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00010F4C File Offset: 0x0000F14C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cos(double x)
		{
			return Math.Cos(x);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00010F54 File Offset: 0x0000F154
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 cos(double2 x)
		{
			return new double2(math.cos(x.x), math.cos(x.y));
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00010F71 File Offset: 0x0000F171
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 cos(double3 x)
		{
			return new double3(math.cos(x.x), math.cos(x.y), math.cos(x.z));
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00010F99 File Offset: 0x0000F199
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 cos(double4 x)
		{
			return new double4(math.cos(x.x), math.cos(x.y), math.cos(x.z), math.cos(x.w));
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00010FCC File Offset: 0x0000F1CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cosh(float x)
		{
			return (float)Math.Cosh((double)x);
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00010FD6 File Offset: 0x0000F1D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 cosh(float2 x)
		{
			return new float2(math.cosh(x.x), math.cosh(x.y));
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00010FF3 File Offset: 0x0000F1F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 cosh(float3 x)
		{
			return new float3(math.cosh(x.x), math.cosh(x.y), math.cosh(x.z));
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001101B File Offset: 0x0000F21B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 cosh(float4 x)
		{
			return new float4(math.cosh(x.x), math.cosh(x.y), math.cosh(x.z), math.cosh(x.w));
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001104E File Offset: 0x0000F24E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cosh(double x)
		{
			return Math.Cosh(x);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00011056 File Offset: 0x0000F256
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 cosh(double2 x)
		{
			return new double2(math.cosh(x.x), math.cosh(x.y));
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00011073 File Offset: 0x0000F273
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 cosh(double3 x)
		{
			return new double3(math.cosh(x.x), math.cosh(x.y), math.cosh(x.z));
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0001109B File Offset: 0x0000F29B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 cosh(double4 x)
		{
			return new double4(math.cosh(x.x), math.cosh(x.y), math.cosh(x.z), math.cosh(x.w));
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000110CE File Offset: 0x0000F2CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float acos(float x)
		{
			return (float)Math.Acos((double)x);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000110D9 File Offset: 0x0000F2D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 acos(float2 x)
		{
			return new float2(math.acos(x.x), math.acos(x.y));
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000110F6 File Offset: 0x0000F2F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 acos(float3 x)
		{
			return new float3(math.acos(x.x), math.acos(x.y), math.acos(x.z));
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001111E File Offset: 0x0000F31E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 acos(float4 x)
		{
			return new float4(math.acos(x.x), math.acos(x.y), math.acos(x.z), math.acos(x.w));
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00011151 File Offset: 0x0000F351
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double acos(double x)
		{
			return Math.Acos(x);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00011159 File Offset: 0x0000F359
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 acos(double2 x)
		{
			return new double2(math.acos(x.x), math.acos(x.y));
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00011176 File Offset: 0x0000F376
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 acos(double3 x)
		{
			return new double3(math.acos(x.x), math.acos(x.y), math.acos(x.z));
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001119E File Offset: 0x0000F39E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 acos(double4 x)
		{
			return new double4(math.acos(x.x), math.acos(x.y), math.acos(x.z), math.acos(x.w));
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000111D1 File Offset: 0x0000F3D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float sin(float x)
		{
			return (float)Math.Sin((double)x);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000111DC File Offset: 0x0000F3DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 sin(float2 x)
		{
			return new float2(math.sin(x.x), math.sin(x.y));
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000111F9 File Offset: 0x0000F3F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 sin(float3 x)
		{
			return new float3(math.sin(x.x), math.sin(x.y), math.sin(x.z));
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00011221 File Offset: 0x0000F421
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 sin(float4 x)
		{
			return new float4(math.sin(x.x), math.sin(x.y), math.sin(x.z), math.sin(x.w));
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00011254 File Offset: 0x0000F454
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double sin(double x)
		{
			return Math.Sin(x);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001125C File Offset: 0x0000F45C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 sin(double2 x)
		{
			return new double2(math.sin(x.x), math.sin(x.y));
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00011279 File Offset: 0x0000F479
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 sin(double3 x)
		{
			return new double3(math.sin(x.x), math.sin(x.y), math.sin(x.z));
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000112A1 File Offset: 0x0000F4A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 sin(double4 x)
		{
			return new double4(math.sin(x.x), math.sin(x.y), math.sin(x.z), math.sin(x.w));
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000112D4 File Offset: 0x0000F4D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float sinh(float x)
		{
			return (float)Math.Sinh((double)x);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000112DF File Offset: 0x0000F4DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 sinh(float2 x)
		{
			return new float2(math.sinh(x.x), math.sinh(x.y));
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000112FC File Offset: 0x0000F4FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 sinh(float3 x)
		{
			return new float3(math.sinh(x.x), math.sinh(x.y), math.sinh(x.z));
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00011324 File Offset: 0x0000F524
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 sinh(float4 x)
		{
			return new float4(math.sinh(x.x), math.sinh(x.y), math.sinh(x.z), math.sinh(x.w));
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00011357 File Offset: 0x0000F557
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double sinh(double x)
		{
			return Math.Sinh(x);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0001135F File Offset: 0x0000F55F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 sinh(double2 x)
		{
			return new double2(math.sinh(x.x), math.sinh(x.y));
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001137C File Offset: 0x0000F57C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 sinh(double3 x)
		{
			return new double3(math.sinh(x.x), math.sinh(x.y), math.sinh(x.z));
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x000113A4 File Offset: 0x0000F5A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 sinh(double4 x)
		{
			return new double4(math.sinh(x.x), math.sinh(x.y), math.sinh(x.z), math.sinh(x.w));
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000113D7 File Offset: 0x0000F5D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float asin(float x)
		{
			return (float)Math.Asin((double)x);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000113E2 File Offset: 0x0000F5E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 asin(float2 x)
		{
			return new float2(math.asin(x.x), math.asin(x.y));
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000113FF File Offset: 0x0000F5FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 asin(float3 x)
		{
			return new float3(math.asin(x.x), math.asin(x.y), math.asin(x.z));
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00011427 File Offset: 0x0000F627
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 asin(float4 x)
		{
			return new float4(math.asin(x.x), math.asin(x.y), math.asin(x.z), math.asin(x.w));
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001145A File Offset: 0x0000F65A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double asin(double x)
		{
			return Math.Asin(x);
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x00011462 File Offset: 0x0000F662
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 asin(double2 x)
		{
			return new double2(math.asin(x.x), math.asin(x.y));
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001147F File Offset: 0x0000F67F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 asin(double3 x)
		{
			return new double3(math.asin(x.x), math.asin(x.y), math.asin(x.z));
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000114A7 File Offset: 0x0000F6A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 asin(double4 x)
		{
			return new double4(math.asin(x.x), math.asin(x.y), math.asin(x.z), math.asin(x.w));
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000114DA File Offset: 0x0000F6DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float floor(float x)
		{
			return (float)Math.Floor((double)x);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x000114E5 File Offset: 0x0000F6E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 floor(float2 x)
		{
			return new float2(math.floor(x.x), math.floor(x.y));
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00011502 File Offset: 0x0000F702
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 floor(float3 x)
		{
			return new float3(math.floor(x.x), math.floor(x.y), math.floor(x.z));
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001152A File Offset: 0x0000F72A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 floor(float4 x)
		{
			return new float4(math.floor(x.x), math.floor(x.y), math.floor(x.z), math.floor(x.w));
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001155D File Offset: 0x0000F75D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double floor(double x)
		{
			return Math.Floor(x);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00011565 File Offset: 0x0000F765
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 floor(double2 x)
		{
			return new double2(math.floor(x.x), math.floor(x.y));
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00011582 File Offset: 0x0000F782
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 floor(double3 x)
		{
			return new double3(math.floor(x.x), math.floor(x.y), math.floor(x.z));
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x000115AA File Offset: 0x0000F7AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 floor(double4 x)
		{
			return new double4(math.floor(x.x), math.floor(x.y), math.floor(x.z), math.floor(x.w));
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000115DD File Offset: 0x0000F7DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ceil(float x)
		{
			return (float)Math.Ceiling((double)x);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x000115E8 File Offset: 0x0000F7E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 ceil(float2 x)
		{
			return new float2(math.ceil(x.x), math.ceil(x.y));
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00011605 File Offset: 0x0000F805
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 ceil(float3 x)
		{
			return new float3(math.ceil(x.x), math.ceil(x.y), math.ceil(x.z));
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0001162D File Offset: 0x0000F82D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 ceil(float4 x)
		{
			return new float4(math.ceil(x.x), math.ceil(x.y), math.ceil(x.z), math.ceil(x.w));
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00011660 File Offset: 0x0000F860
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double ceil(double x)
		{
			return Math.Ceiling(x);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00011668 File Offset: 0x0000F868
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 ceil(double2 x)
		{
			return new double2(math.ceil(x.x), math.ceil(x.y));
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00011685 File Offset: 0x0000F885
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 ceil(double3 x)
		{
			return new double3(math.ceil(x.x), math.ceil(x.y), math.ceil(x.z));
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000116AD File Offset: 0x0000F8AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 ceil(double4 x)
		{
			return new double4(math.ceil(x.x), math.ceil(x.y), math.ceil(x.z), math.ceil(x.w));
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000116E0 File Offset: 0x0000F8E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float round(float x)
		{
			return (float)Math.Round((double)x);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x000116EB File Offset: 0x0000F8EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 round(float2 x)
		{
			return new float2(math.round(x.x), math.round(x.y));
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00011708 File Offset: 0x0000F908
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 round(float3 x)
		{
			return new float3(math.round(x.x), math.round(x.y), math.round(x.z));
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00011730 File Offset: 0x0000F930
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 round(float4 x)
		{
			return new float4(math.round(x.x), math.round(x.y), math.round(x.z), math.round(x.w));
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00011763 File Offset: 0x0000F963
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double round(double x)
		{
			return Math.Round(x);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001176B File Offset: 0x0000F96B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 round(double2 x)
		{
			return new double2(math.round(x.x), math.round(x.y));
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00011788 File Offset: 0x0000F988
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 round(double3 x)
		{
			return new double3(math.round(x.x), math.round(x.y), math.round(x.z));
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000117B0 File Offset: 0x0000F9B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 round(double4 x)
		{
			return new double4(math.round(x.x), math.round(x.y), math.round(x.z), math.round(x.w));
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000117E3 File Offset: 0x0000F9E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float trunc(float x)
		{
			return (float)Math.Truncate((double)x);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000117EE File Offset: 0x0000F9EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 trunc(float2 x)
		{
			return new float2(math.trunc(x.x), math.trunc(x.y));
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001180B File Offset: 0x0000FA0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 trunc(float3 x)
		{
			return new float3(math.trunc(x.x), math.trunc(x.y), math.trunc(x.z));
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00011833 File Offset: 0x0000FA33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 trunc(float4 x)
		{
			return new float4(math.trunc(x.x), math.trunc(x.y), math.trunc(x.z), math.trunc(x.w));
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00011866 File Offset: 0x0000FA66
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double trunc(double x)
		{
			return Math.Truncate(x);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001186E File Offset: 0x0000FA6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 trunc(double2 x)
		{
			return new double2(math.trunc(x.x), math.trunc(x.y));
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001188B File Offset: 0x0000FA8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 trunc(double3 x)
		{
			return new double3(math.trunc(x.x), math.trunc(x.y), math.trunc(x.z));
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000118B3 File Offset: 0x0000FAB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 trunc(double4 x)
		{
			return new double4(math.trunc(x.x), math.trunc(x.y), math.trunc(x.z), math.trunc(x.w));
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000118E6 File Offset: 0x0000FAE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float frac(float x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000118F0 File Offset: 0x0000FAF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 frac(float2 x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000118FE File Offset: 0x0000FAFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 frac(float3 x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001190C File Offset: 0x0000FB0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 frac(float4 x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001191A File Offset: 0x0000FB1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double frac(double x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00011924 File Offset: 0x0000FB24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 frac(double2 x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00011932 File Offset: 0x0000FB32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 frac(double3 x)
		{
			return x - math.floor(x);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00011940 File Offset: 0x0000FB40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 frac(double4 x)
		{
			return x - math.floor(x);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001194E File Offset: 0x0000FB4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float rcp(float x)
		{
			return 1f / x;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00011957 File Offset: 0x0000FB57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 rcp(float2 x)
		{
			return 1f / x;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00011964 File Offset: 0x0000FB64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 rcp(float3 x)
		{
			return 1f / x;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00011971 File Offset: 0x0000FB71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 rcp(float4 x)
		{
			return 1f / x;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001197E File Offset: 0x0000FB7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double rcp(double x)
		{
			return 1.0 / x;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001198B File Offset: 0x0000FB8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 rcp(double2 x)
		{
			return 1.0 / x;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001199C File Offset: 0x0000FB9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 rcp(double3 x)
		{
			return 1.0 / x;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000119AD File Offset: 0x0000FBAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 rcp(double4 x)
		{
			return 1.0 / x;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000119BE File Offset: 0x0000FBBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int sign(int x)
		{
			return ((x > 0) ? 1 : 0) - ((x < 0) ? 1 : 0);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x000119D1 File Offset: 0x0000FBD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 sign(int2 x)
		{
			return new int2(math.sign(x.x), math.sign(x.y));
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x000119EE File Offset: 0x0000FBEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 sign(int3 x)
		{
			return new int3(math.sign(x.x), math.sign(x.y), math.sign(x.z));
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00011A16 File Offset: 0x0000FC16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 sign(int4 x)
		{
			return new int4(math.sign(x.x), math.sign(x.y), math.sign(x.z), math.sign(x.w));
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00011A49 File Offset: 0x0000FC49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float sign(float x)
		{
			return ((x > 0f) ? 1f : 0f) - ((x < 0f) ? 1f : 0f);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00011A74 File Offset: 0x0000FC74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 sign(float2 x)
		{
			return new float2(math.sign(x.x), math.sign(x.y));
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00011A91 File Offset: 0x0000FC91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 sign(float3 x)
		{
			return new float3(math.sign(x.x), math.sign(x.y), math.sign(x.z));
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00011AB9 File Offset: 0x0000FCB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 sign(float4 x)
		{
			return new float4(math.sign(x.x), math.sign(x.y), math.sign(x.z), math.sign(x.w));
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00011AEC File Offset: 0x0000FCEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double sign(double x)
		{
			if (x != 0.0)
			{
				return ((x > 0.0) ? 1.0 : 0.0) - ((x < 0.0) ? 1.0 : 0.0);
			}
			return 0.0;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00011B50 File Offset: 0x0000FD50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 sign(double2 x)
		{
			return new double2(math.sign(x.x), math.sign(x.y));
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00011B6D File Offset: 0x0000FD6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 sign(double3 x)
		{
			return new double3(math.sign(x.x), math.sign(x.y), math.sign(x.z));
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00011B95 File Offset: 0x0000FD95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 sign(double4 x)
		{
			return new double4(math.sign(x.x), math.sign(x.y), math.sign(x.z), math.sign(x.w));
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00011BC8 File Offset: 0x0000FDC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float pow(float x, float y)
		{
			return (float)Math.Pow((double)x, (double)y);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00011BD6 File Offset: 0x0000FDD6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 pow(float2 x, float2 y)
		{
			return new float2(math.pow(x.x, y.x), math.pow(x.y, y.y));
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00011BFF File Offset: 0x0000FDFF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 pow(float3 x, float3 y)
		{
			return new float3(math.pow(x.x, y.x), math.pow(x.y, y.y), math.pow(x.z, y.z));
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00011C3C File Offset: 0x0000FE3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 pow(float4 x, float4 y)
		{
			return new float4(math.pow(x.x, y.x), math.pow(x.y, y.y), math.pow(x.z, y.z), math.pow(x.w, y.w));
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00011C92 File Offset: 0x0000FE92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double pow(double x, double y)
		{
			return Math.Pow(x, y);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00011C9B File Offset: 0x0000FE9B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 pow(double2 x, double2 y)
		{
			return new double2(math.pow(x.x, y.x), math.pow(x.y, y.y));
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00011CC4 File Offset: 0x0000FEC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 pow(double3 x, double3 y)
		{
			return new double3(math.pow(x.x, y.x), math.pow(x.y, y.y), math.pow(x.z, y.z));
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00011D00 File Offset: 0x0000FF00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 pow(double4 x, double4 y)
		{
			return new double4(math.pow(x.x, y.x), math.pow(x.y, y.y), math.pow(x.z, y.z), math.pow(x.w, y.w));
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00011D56 File Offset: 0x0000FF56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp(float x)
		{
			return (float)Math.Exp((double)x);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00011D61 File Offset: 0x0000FF61
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 exp(float2 x)
		{
			return new float2(math.exp(x.x), math.exp(x.y));
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00011D7E File Offset: 0x0000FF7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 exp(float3 x)
		{
			return new float3(math.exp(x.x), math.exp(x.y), math.exp(x.z));
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00011DA6 File Offset: 0x0000FFA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 exp(float4 x)
		{
			return new float4(math.exp(x.x), math.exp(x.y), math.exp(x.z), math.exp(x.w));
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00011DD9 File Offset: 0x0000FFD9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double exp(double x)
		{
			return Math.Exp(x);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00011DE1 File Offset: 0x0000FFE1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 exp(double2 x)
		{
			return new double2(math.exp(x.x), math.exp(x.y));
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00011DFE File Offset: 0x0000FFFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 exp(double3 x)
		{
			return new double3(math.exp(x.x), math.exp(x.y), math.exp(x.z));
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00011E26 File Offset: 0x00010026
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 exp(double4 x)
		{
			return new double4(math.exp(x.x), math.exp(x.y), math.exp(x.z), math.exp(x.w));
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00011E59 File Offset: 0x00010059
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp2(float x)
		{
			return (float)Math.Exp((double)(x * 0.6931472f));
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00011E6A File Offset: 0x0001006A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 exp2(float2 x)
		{
			return new float2(math.exp2(x.x), math.exp2(x.y));
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00011E87 File Offset: 0x00010087
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 exp2(float3 x)
		{
			return new float3(math.exp2(x.x), math.exp2(x.y), math.exp2(x.z));
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00011EAF File Offset: 0x000100AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 exp2(float4 x)
		{
			return new float4(math.exp2(x.x), math.exp2(x.y), math.exp2(x.z), math.exp2(x.w));
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00011EE2 File Offset: 0x000100E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double exp2(double x)
		{
			return Math.Exp(x * 0.6931471805599453);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00011EF4 File Offset: 0x000100F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 exp2(double2 x)
		{
			return new double2(math.exp2(x.x), math.exp2(x.y));
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00011F11 File Offset: 0x00010111
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 exp2(double3 x)
		{
			return new double3(math.exp2(x.x), math.exp2(x.y), math.exp2(x.z));
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00011F39 File Offset: 0x00010139
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 exp2(double4 x)
		{
			return new double4(math.exp2(x.x), math.exp2(x.y), math.exp2(x.z), math.exp2(x.w));
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00011F6C File Offset: 0x0001016C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float exp10(float x)
		{
			return (float)Math.Exp((double)(x * 2.3025851f));
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00011F7D File Offset: 0x0001017D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 exp10(float2 x)
		{
			return new float2(math.exp10(x.x), math.exp10(x.y));
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00011F9A File Offset: 0x0001019A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 exp10(float3 x)
		{
			return new float3(math.exp10(x.x), math.exp10(x.y), math.exp10(x.z));
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00011FC2 File Offset: 0x000101C2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 exp10(float4 x)
		{
			return new float4(math.exp10(x.x), math.exp10(x.y), math.exp10(x.z), math.exp10(x.w));
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00011FF5 File Offset: 0x000101F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double exp10(double x)
		{
			return Math.Exp(x * 2.302585092994046);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00012007 File Offset: 0x00010207
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 exp10(double2 x)
		{
			return new double2(math.exp10(x.x), math.exp10(x.y));
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00012024 File Offset: 0x00010224
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 exp10(double3 x)
		{
			return new double3(math.exp10(x.x), math.exp10(x.y), math.exp10(x.z));
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001204C File Offset: 0x0001024C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 exp10(double4 x)
		{
			return new double4(math.exp10(x.x), math.exp10(x.y), math.exp10(x.z), math.exp10(x.w));
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001207F File Offset: 0x0001027F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log(float x)
		{
			return (float)Math.Log((double)x);
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001208A File Offset: 0x0001028A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 log(float2 x)
		{
			return new float2(math.log(x.x), math.log(x.y));
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x000120A7 File Offset: 0x000102A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 log(float3 x)
		{
			return new float3(math.log(x.x), math.log(x.y), math.log(x.z));
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x000120CF File Offset: 0x000102CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 log(float4 x)
		{
			return new float4(math.log(x.x), math.log(x.y), math.log(x.z), math.log(x.w));
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x00012102 File Offset: 0x00010302
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double log(double x)
		{
			return Math.Log(x);
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001210A File Offset: 0x0001030A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 log(double2 x)
		{
			return new double2(math.log(x.x), math.log(x.y));
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00012127 File Offset: 0x00010327
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 log(double3 x)
		{
			return new double3(math.log(x.x), math.log(x.y), math.log(x.z));
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001214F File Offset: 0x0001034F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 log(double4 x)
		{
			return new double4(math.log(x.x), math.log(x.y), math.log(x.z), math.log(x.w));
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00012182 File Offset: 0x00010382
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log2(float x)
		{
			return (float)Math.Log((double)x, 2.0);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00012196 File Offset: 0x00010396
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 log2(float2 x)
		{
			return new float2(math.log2(x.x), math.log2(x.y));
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x000121B3 File Offset: 0x000103B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 log2(float3 x)
		{
			return new float3(math.log2(x.x), math.log2(x.y), math.log2(x.z));
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000121DB File Offset: 0x000103DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 log2(float4 x)
		{
			return new float4(math.log2(x.x), math.log2(x.y), math.log2(x.z), math.log2(x.w));
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001220E File Offset: 0x0001040E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double log2(double x)
		{
			return Math.Log(x, 2.0);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001221F File Offset: 0x0001041F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 log2(double2 x)
		{
			return new double2(math.log2(x.x), math.log2(x.y));
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001223C File Offset: 0x0001043C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 log2(double3 x)
		{
			return new double3(math.log2(x.x), math.log2(x.y), math.log2(x.z));
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00012264 File Offset: 0x00010464
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 log2(double4 x)
		{
			return new double4(math.log2(x.x), math.log2(x.y), math.log2(x.z), math.log2(x.w));
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00012297 File Offset: 0x00010497
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float log10(float x)
		{
			return (float)Math.Log10((double)x);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x000122A2 File Offset: 0x000104A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 log10(float2 x)
		{
			return new float2(math.log10(x.x), math.log10(x.y));
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000122BF File Offset: 0x000104BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 log10(float3 x)
		{
			return new float3(math.log10(x.x), math.log10(x.y), math.log10(x.z));
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x000122E7 File Offset: 0x000104E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 log10(float4 x)
		{
			return new float4(math.log10(x.x), math.log10(x.y), math.log10(x.z), math.log10(x.w));
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001231A File Offset: 0x0001051A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double log10(double x)
		{
			return Math.Log10(x);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x00012322 File Offset: 0x00010522
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 log10(double2 x)
		{
			return new double2(math.log10(x.x), math.log10(x.y));
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001233F File Offset: 0x0001053F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 log10(double3 x)
		{
			return new double3(math.log10(x.x), math.log10(x.y), math.log10(x.z));
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x00012367 File Offset: 0x00010567
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 log10(double4 x)
		{
			return new double4(math.log10(x.x), math.log10(x.y), math.log10(x.z), math.log10(x.w));
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001239A File Offset: 0x0001059A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float fmod(float x, float y)
		{
			return x % y;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001239F File Offset: 0x0001059F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 fmod(float2 x, float2 y)
		{
			return new float2(x.x % y.x, x.y % y.y);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000123C0 File Offset: 0x000105C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 fmod(float3 x, float3 y)
		{
			return new float3(x.x % y.x, x.y % y.y, x.z % y.z);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x000123EE File Offset: 0x000105EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 fmod(float4 x, float4 y)
		{
			return new float4(x.x % y.x, x.y % y.y, x.z % y.z, x.w % y.w);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001239A File Offset: 0x0001059A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double fmod(double x, double y)
		{
			return x % y;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x00012429 File Offset: 0x00010629
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 fmod(double2 x, double2 y)
		{
			return new double2(x.x % y.x, x.y % y.y);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001244A File Offset: 0x0001064A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 fmod(double3 x, double3 y)
		{
			return new double3(x.x % y.x, x.y % y.y, x.z % y.z);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00012478 File Offset: 0x00010678
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 fmod(double4 x, double4 y)
		{
			return new double4(x.x % y.x, x.y % y.y, x.z % y.z, x.w % y.w);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x000124B3 File Offset: 0x000106B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float modf(float x, out float i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x000124C1 File Offset: 0x000106C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 modf(float2 x, out float2 i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000124DB File Offset: 0x000106DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 modf(float3 x, out float3 i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000124F5 File Offset: 0x000106F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 modf(float4 x, out float4 i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001250F File Offset: 0x0001070F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double modf(double x, out double i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001251D File Offset: 0x0001071D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 modf(double2 x, out double2 i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00012537 File Offset: 0x00010737
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 modf(double3 x, out double3 i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x00012551 File Offset: 0x00010751
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 modf(double4 x, out double4 i)
		{
			i = math.trunc(x);
			return x - i;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001256B File Offset: 0x0001076B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float sqrt(float x)
		{
			return (float)Math.Sqrt((double)x);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00012576 File Offset: 0x00010776
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 sqrt(float2 x)
		{
			return new float2(math.sqrt(x.x), math.sqrt(x.y));
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x00012593 File Offset: 0x00010793
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 sqrt(float3 x)
		{
			return new float3(math.sqrt(x.x), math.sqrt(x.y), math.sqrt(x.z));
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000125BB File Offset: 0x000107BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 sqrt(float4 x)
		{
			return new float4(math.sqrt(x.x), math.sqrt(x.y), math.sqrt(x.z), math.sqrt(x.w));
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x000125EE File Offset: 0x000107EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double sqrt(double x)
		{
			return Math.Sqrt(x);
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000125F6 File Offset: 0x000107F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 sqrt(double2 x)
		{
			return new double2(math.sqrt(x.x), math.sqrt(x.y));
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00012613 File Offset: 0x00010813
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 sqrt(double3 x)
		{
			return new double3(math.sqrt(x.x), math.sqrt(x.y), math.sqrt(x.z));
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001263B File Offset: 0x0001083B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 sqrt(double4 x)
		{
			return new double4(math.sqrt(x.x), math.sqrt(x.y), math.sqrt(x.z), math.sqrt(x.w));
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001266E File Offset: 0x0001086E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float rsqrt(float x)
		{
			return 1f / math.sqrt(x);
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001267C File Offset: 0x0001087C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 rsqrt(float2 x)
		{
			return 1f / math.sqrt(x);
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001268E File Offset: 0x0001088E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 rsqrt(float3 x)
		{
			return 1f / math.sqrt(x);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000126A0 File Offset: 0x000108A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 rsqrt(float4 x)
		{
			return 1f / math.sqrt(x);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x000126B2 File Offset: 0x000108B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double rsqrt(double x)
		{
			return 1.0 / math.sqrt(x);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000126C4 File Offset: 0x000108C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 rsqrt(double2 x)
		{
			return 1.0 / math.sqrt(x);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x000126DA File Offset: 0x000108DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 rsqrt(double3 x)
		{
			return 1.0 / math.sqrt(x);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000126F0 File Offset: 0x000108F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 rsqrt(double4 x)
		{
			return 1.0 / math.sqrt(x);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00012706 File Offset: 0x00010906
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 normalize(float2 x)
		{
			return math.rsqrt(math.dot(x, x)) * x;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001271A File Offset: 0x0001091A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 normalize(float3 x)
		{
			return math.rsqrt(math.dot(x, x)) * x;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001272E File Offset: 0x0001092E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 normalize(float4 x)
		{
			return math.rsqrt(math.dot(x, x)) * x;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00012742 File Offset: 0x00010942
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 normalize(double2 x)
		{
			return math.rsqrt(math.dot(x, x)) * x;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00012756 File Offset: 0x00010956
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 normalize(double3 x)
		{
			return math.rsqrt(math.dot(x, x)) * x;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001276A File Offset: 0x0001096A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 normalize(double4 x)
		{
			return math.rsqrt(math.dot(x, x)) * x;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00012780 File Offset: 0x00010980
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 normalizesafe(float2 x, float2 defaultvalue = default(float2))
		{
			float len = math.dot(x, x);
			return math.select(defaultvalue, x * math.rsqrt(len), len > 1.1754944E-38f);
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000127B0 File Offset: 0x000109B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 normalizesafe(float3 x, float3 defaultvalue = default(float3))
		{
			float len = math.dot(x, x);
			return math.select(defaultvalue, x * math.rsqrt(len), len > 1.1754944E-38f);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000127E0 File Offset: 0x000109E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 normalizesafe(float4 x, float4 defaultvalue = default(float4))
		{
			float len = math.dot(x, x);
			return math.select(defaultvalue, x * math.rsqrt(len), len > 1.1754944E-38f);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00012810 File Offset: 0x00010A10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 normalizesafe(double2 x, double2 defaultvalue = default(double2))
		{
			double len = math.dot(x, x);
			return math.select(defaultvalue, x * math.rsqrt(len), len > 1.1754943508222875E-38);
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00012844 File Offset: 0x00010A44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 normalizesafe(double3 x, double3 defaultvalue = default(double3))
		{
			double len = math.dot(x, x);
			return math.select(defaultvalue, x * math.rsqrt(len), len > 1.1754943508222875E-38);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00012878 File Offset: 0x00010A78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 normalizesafe(double4 x, double4 defaultvalue = default(double4))
		{
			double len = math.dot(x, x);
			return math.select(defaultvalue, x * math.rsqrt(len), len > 1.1754943508222875E-38);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x000128AB File Offset: 0x00010AAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float length(float x)
		{
			return math.abs(x);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x000128B3 File Offset: 0x00010AB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float length(float2 x)
		{
			return math.sqrt(math.dot(x, x));
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x000128C1 File Offset: 0x00010AC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float length(float3 x)
		{
			return math.sqrt(math.dot(x, x));
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000128CF File Offset: 0x00010ACF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float length(float4 x)
		{
			return math.sqrt(math.dot(x, x));
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000128DD File Offset: 0x00010ADD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double length(double x)
		{
			return math.abs(x);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x000128E5 File Offset: 0x00010AE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double length(double2 x)
		{
			return math.sqrt(math.dot(x, x));
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x000128F3 File Offset: 0x00010AF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double length(double3 x)
		{
			return math.sqrt(math.dot(x, x));
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x00012901 File Offset: 0x00010B01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double length(double4 x)
		{
			return math.sqrt(math.dot(x, x));
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001290F File Offset: 0x00010B0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(float x)
		{
			return x * x;
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00012914 File Offset: 0x00010B14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(float2 x)
		{
			return math.dot(x, x);
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001291D File Offset: 0x00010B1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(float3 x)
		{
			return math.dot(x, x);
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00012926 File Offset: 0x00010B26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(float4 x)
		{
			return math.dot(x, x);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001290F File Offset: 0x00010B0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double lengthsq(double x)
		{
			return x * x;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001292F File Offset: 0x00010B2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double lengthsq(double2 x)
		{
			return math.dot(x, x);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00012938 File Offset: 0x00010B38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double lengthsq(double3 x)
		{
			return math.dot(x, x);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00012941 File Offset: 0x00010B41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double lengthsq(double4 x)
		{
			return math.dot(x, x);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001294A File Offset: 0x00010B4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distance(float x, float y)
		{
			return math.abs(y - x);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00012954 File Offset: 0x00010B54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distance(float2 x, float2 y)
		{
			return math.length(y - x);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00012962 File Offset: 0x00010B62
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distance(float3 x, float3 y)
		{
			return math.length(y - x);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00012970 File Offset: 0x00010B70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distance(float4 x, float4 y)
		{
			return math.length(y - x);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001297E File Offset: 0x00010B7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distance(double x, double y)
		{
			return math.abs(y - x);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00012988 File Offset: 0x00010B88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distance(double2 x, double2 y)
		{
			return math.length(y - x);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00012996 File Offset: 0x00010B96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distance(double3 x, double3 y)
		{
			return math.length(y - x);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000129A4 File Offset: 0x00010BA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distance(double4 x, double4 y)
		{
			return math.length(y - x);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000129B2 File Offset: 0x00010BB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distancesq(float x, float y)
		{
			return (y - x) * (y - x);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000129BB File Offset: 0x00010BBB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distancesq(float2 x, float2 y)
		{
			return math.lengthsq(y - x);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000129C9 File Offset: 0x00010BC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distancesq(float3 x, float3 y)
		{
			return math.lengthsq(y - x);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000129D7 File Offset: 0x00010BD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float distancesq(float4 x, float4 y)
		{
			return math.lengthsq(y - x);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000129B2 File Offset: 0x00010BB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distancesq(double x, double y)
		{
			return (y - x) * (y - x);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000129E5 File Offset: 0x00010BE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distancesq(double2 x, double2 y)
		{
			return math.lengthsq(y - x);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000129F3 File Offset: 0x00010BF3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distancesq(double3 x, double3 y)
		{
			return math.lengthsq(y - x);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00012A01 File Offset: 0x00010C01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double distancesq(double4 x, double4 y)
		{
			return math.lengthsq(y - x);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00012A10 File Offset: 0x00010C10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 cross(float3 x, float3 y)
		{
			return (x * y.yzx - x.yzx * y).yzx;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00012A44 File Offset: 0x00010C44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 cross(double3 x, double3 y)
		{
			return (x * y.yzx - x.yzx * y).yzx;
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00012A78 File Offset: 0x00010C78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float smoothstep(float xMin, float xMax, float x)
		{
			float t = math.saturate((x - xMin) / (xMax - xMin));
			return t * t * (3f - 2f * t);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00012AA4 File Offset: 0x00010CA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 smoothstep(float2 xMin, float2 xMax, float2 x)
		{
			float2 t = math.saturate((x - xMin) / (xMax - xMin));
			return t * t * (3f - 2f * t);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00012AEC File Offset: 0x00010CEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 smoothstep(float3 xMin, float3 xMax, float3 x)
		{
			float3 t = math.saturate((x - xMin) / (xMax - xMin));
			return t * t * (3f - 2f * t);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00012B34 File Offset: 0x00010D34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 smoothstep(float4 xMin, float4 xMax, float4 x)
		{
			float4 t = math.saturate((x - xMin) / (xMax - xMin));
			return t * t * (3f - 2f * t);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00012B7C File Offset: 0x00010D7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double smoothstep(double xMin, double xMax, double x)
		{
			double t = math.saturate((x - xMin) / (xMax - xMin));
			return t * t * (3.0 - 2.0 * t);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00012BB0 File Offset: 0x00010DB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 smoothstep(double2 xMin, double2 xMax, double2 x)
		{
			double2 t = math.saturate((x - xMin) / (xMax - xMin));
			return t * t * (3.0 - 2.0 * t);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00012C00 File Offset: 0x00010E00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 smoothstep(double3 xMin, double3 xMax, double3 x)
		{
			double3 t = math.saturate((x - xMin) / (xMax - xMin));
			return t * t * (3.0 - 2.0 * t);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00012C50 File Offset: 0x00010E50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 smoothstep(double4 xMin, double4 xMax, double4 x)
		{
			double4 t = math.saturate((x - xMin) / (xMax - xMin));
			return t * t * (3.0 - 2.0 * t);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00012C9F File Offset: 0x00010E9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(bool2 x)
		{
			return x.x || x.y;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00012CB1 File Offset: 0x00010EB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(bool3 x)
		{
			return x.x || x.y || x.z;
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00012CCB File Offset: 0x00010ECB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(bool4 x)
		{
			return x.x || x.y || x.z || x.w;
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00012CED File Offset: 0x00010EED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(int2 x)
		{
			return x.x != 0 || x.y != 0;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00012D02 File Offset: 0x00010F02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(int3 x)
		{
			return x.x != 0 || x.y != 0 || x.z != 0;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00012D1F File Offset: 0x00010F1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(int4 x)
		{
			return x.x != 0 || x.y != 0 || x.z != 0 || x.w != 0;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00012D44 File Offset: 0x00010F44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(uint2 x)
		{
			return x.x != 0U || x.y > 0U;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00012D59 File Offset: 0x00010F59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(uint3 x)
		{
			return x.x != 0U || x.y != 0U || x.z > 0U;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00012D76 File Offset: 0x00010F76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(uint4 x)
		{
			return x.x != 0U || x.y != 0U || x.z != 0U || x.w > 0U;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00012D9B File Offset: 0x00010F9B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(float2 x)
		{
			return x.x != 0f || x.y != 0f;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00012DBC File Offset: 0x00010FBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(float3 x)
		{
			return x.x != 0f || x.y != 0f || x.z != 0f;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00012DEA File Offset: 0x00010FEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(float4 x)
		{
			return x.x != 0f || x.y != 0f || x.z != 0f || x.w != 0f;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00012E25 File Offset: 0x00011025
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(double2 x)
		{
			return x.x != 0.0 || x.y != 0.0;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00012E4E File Offset: 0x0001104E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(double3 x)
		{
			return x.x != 0.0 || x.y != 0.0 || x.z != 0.0;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00012E88 File Offset: 0x00011088
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool any(double4 x)
		{
			return x.x != 0.0 || x.y != 0.0 || x.z != 0.0 || x.w != 0.0;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00012EDE File Offset: 0x000110DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(bool2 x)
		{
			return x.x && x.y;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00012EF0 File Offset: 0x000110F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(bool3 x)
		{
			return x.x && x.y && x.z;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00012F0A File Offset: 0x0001110A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(bool4 x)
		{
			return x.x && x.y && x.z && x.w;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00012F2C File Offset: 0x0001112C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(int2 x)
		{
			return x.x != 0 && x.y != 0;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00012F41 File Offset: 0x00011141
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(int3 x)
		{
			return x.x != 0 && x.y != 0 && x.z != 0;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00012F5E File Offset: 0x0001115E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(int4 x)
		{
			return x.x != 0 && x.y != 0 && x.z != 0 && x.w != 0;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x00012F83 File Offset: 0x00011183
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(uint2 x)
		{
			return x.x != 0U && x.y > 0U;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00012F98 File Offset: 0x00011198
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(uint3 x)
		{
			return x.x != 0U && x.y != 0U && x.z > 0U;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00012FB5 File Offset: 0x000111B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(uint4 x)
		{
			return x.x != 0U && x.y != 0U && x.z != 0U && x.w > 0U;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00012FDA File Offset: 0x000111DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(float2 x)
		{
			return x.x != 0f && x.y != 0f;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00012FFB File Offset: 0x000111FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(float3 x)
		{
			return x.x != 0f && x.y != 0f && x.z != 0f;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00013029 File Offset: 0x00011229
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(float4 x)
		{
			return x.x != 0f && x.y != 0f && x.z != 0f && x.w != 0f;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00013064 File Offset: 0x00011264
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(double2 x)
		{
			return x.x != 0.0 && x.y != 0.0;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0001308D File Offset: 0x0001128D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(double3 x)
		{
			return x.x != 0.0 && x.y != 0.0 && x.z != 0.0;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000130C8 File Offset: 0x000112C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool all(double4 x)
		{
			return x.x != 0.0 && x.y != 0.0 && x.z != 0.0 && x.w != 0.0;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int select(int falseValue, int trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 select(int2 falseValue, int2 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 select(int3 falseValue, int3 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 select(int4 falseValue, int4 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00013126 File Offset: 0x00011326
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 select(int2 falseValue, int2 trueValue, bool2 test)
		{
			return new int2(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y);
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001315C File Offset: 0x0001135C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 select(int3 falseValue, int3 trueValue, bool3 test)
		{
			return new int3(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x000131B0 File Offset: 0x000113B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 select(int4 falseValue, int4 trueValue, bool4 test)
		{
			return new int4(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z, test.w ? trueValue.w : falseValue.w);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint select(uint falseValue, uint trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 select(uint2 falseValue, uint2 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 select(uint3 falseValue, uint3 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 select(uint4 falseValue, uint4 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001321A File Offset: 0x0001141A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 select(uint2 falseValue, uint2 trueValue, bool2 test)
		{
			return new uint2(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y);
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x00013250 File Offset: 0x00011450
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 select(uint3 falseValue, uint3 trueValue, bool3 test)
		{
			return new uint3(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z);
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x000132A4 File Offset: 0x000114A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 select(uint4 falseValue, uint4 trueValue, bool4 test)
		{
			return new uint4(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z, test.w ? trueValue.w : falseValue.w);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long select(long falseValue, long trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong select(ulong falseValue, ulong trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float select(float falseValue, float trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 select(float2 falseValue, float2 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 select(float3 falseValue, float3 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 select(float4 falseValue, float4 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001330E File Offset: 0x0001150E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 select(float2 falseValue, float2 trueValue, bool2 test)
		{
			return new float2(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00013344 File Offset: 0x00011544
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 select(float3 falseValue, float3 trueValue, bool3 test)
		{
			return new float3(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00013398 File Offset: 0x00011598
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 select(float4 falseValue, float4 trueValue, bool4 test)
		{
			return new float4(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z, test.w ? trueValue.w : falseValue.w);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double select(double falseValue, double trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 select(double2 falseValue, double2 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 select(double3 falseValue, double3 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001311E File Offset: 0x0001131E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 select(double4 falseValue, double4 trueValue, bool test)
		{
			if (!test)
			{
				return falseValue;
			}
			return trueValue;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00013402 File Offset: 0x00011602
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 select(double2 falseValue, double2 trueValue, bool2 test)
		{
			return new double2(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00013438 File Offset: 0x00011638
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 select(double3 falseValue, double3 trueValue, bool3 test)
		{
			return new double3(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001348C File Offset: 0x0001168C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 select(double4 falseValue, double4 trueValue, bool4 test)
		{
			return new double4(test.x ? trueValue.x : falseValue.x, test.y ? trueValue.y : falseValue.y, test.z ? trueValue.z : falseValue.z, test.w ? trueValue.w : falseValue.w);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000134F6 File Offset: 0x000116F6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float step(float threshold, float x)
		{
			return math.select(0f, 1f, x >= threshold);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001350E File Offset: 0x0001170E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 step(float2 threshold, float2 x)
		{
			return math.select(math.float2(0f), math.float2(1f), x >= threshold);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00013530 File Offset: 0x00011730
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 step(float3 threshold, float3 x)
		{
			return math.select(math.float3(0f), math.float3(1f), x >= threshold);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00013552 File Offset: 0x00011752
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 step(float4 threshold, float4 x)
		{
			return math.select(math.float4(0f), math.float4(1f), x >= threshold);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00013574 File Offset: 0x00011774
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double step(double threshold, double x)
		{
			return math.select(0.0, 1.0, x >= threshold);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00013594 File Offset: 0x00011794
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 step(double2 threshold, double2 x)
		{
			return math.select(math.double2(0.0), math.double2(1.0), x >= threshold);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000135BE File Offset: 0x000117BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 step(double3 threshold, double3 x)
		{
			return math.select(math.double3(0.0), math.double3(1.0), x >= threshold);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000135E8 File Offset: 0x000117E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 step(double4 threshold, double4 x)
		{
			return math.select(math.double4(0.0), math.double4(1.0), x >= threshold);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00013612 File Offset: 0x00011812
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 reflect(float2 i, float2 n)
		{
			return i - 2f * n * math.dot(i, n);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00013631 File Offset: 0x00011831
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 reflect(float3 i, float3 n)
		{
			return i - 2f * n * math.dot(i, n);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00013650 File Offset: 0x00011850
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 reflect(float4 i, float4 n)
		{
			return i - 2f * n * math.dot(i, n);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x0001366F File Offset: 0x0001186F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 reflect(double2 i, double2 n)
		{
			return i - 2.0 * n * math.dot(i, n);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00013692 File Offset: 0x00011892
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 reflect(double3 i, double3 n)
		{
			return i - 2.0 * n * math.dot(i, n);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x000136B5 File Offset: 0x000118B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 reflect(double4 i, double4 n)
		{
			return i - 2.0 * n * math.dot(i, n);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000136D8 File Offset: 0x000118D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 refract(float2 i, float2 n, float indexOfRefraction)
		{
			float ni = math.dot(n, i);
			float j = 1f - indexOfRefraction * indexOfRefraction * (1f - ni * ni);
			return math.select(0f, indexOfRefraction * i - (indexOfRefraction * ni + math.sqrt(j)) * n, j >= 0f);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00013738 File Offset: 0x00011938
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 refract(float3 i, float3 n, float indexOfRefraction)
		{
			float ni = math.dot(n, i);
			float j = 1f - indexOfRefraction * indexOfRefraction * (1f - ni * ni);
			return math.select(0f, indexOfRefraction * i - (indexOfRefraction * ni + math.sqrt(j)) * n, j >= 0f);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00013798 File Offset: 0x00011998
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 refract(float4 i, float4 n, float indexOfRefraction)
		{
			float ni = math.dot(n, i);
			float j = 1f - indexOfRefraction * indexOfRefraction * (1f - ni * ni);
			return math.select(0f, indexOfRefraction * i - (indexOfRefraction * ni + math.sqrt(j)) * n, j >= 0f);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x000137F8 File Offset: 0x000119F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 refract(double2 i, double2 n, double indexOfRefraction)
		{
			double ni = math.dot(n, i);
			double j = 1.0 - indexOfRefraction * indexOfRefraction * (1.0 - ni * ni);
			return math.select(0f, indexOfRefraction * i - (indexOfRefraction * ni + math.sqrt(j)) * n, j >= 0.0);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00013864 File Offset: 0x00011A64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 refract(double3 i, double3 n, double indexOfRefraction)
		{
			double ni = math.dot(n, i);
			double j = 1.0 - indexOfRefraction * indexOfRefraction * (1.0 - ni * ni);
			return math.select(0f, indexOfRefraction * i - (indexOfRefraction * ni + math.sqrt(j)) * n, j >= 0.0);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x000138D0 File Offset: 0x00011AD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 refract(double4 i, double4 n, double indexOfRefraction)
		{
			double ni = math.dot(n, i);
			double j = 1.0 - indexOfRefraction * indexOfRefraction * (1.0 - ni * ni);
			return math.select(0f, indexOfRefraction * i - (indexOfRefraction * ni + math.sqrt(j)) * n, j >= 0.0);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x0001393B File Offset: 0x00011B3B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 project(float2 a, float2 ontoB)
		{
			return math.dot(a, ontoB) / math.dot(ontoB, ontoB) * ontoB;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00013952 File Offset: 0x00011B52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 project(float3 a, float3 ontoB)
		{
			return math.dot(a, ontoB) / math.dot(ontoB, ontoB) * ontoB;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00013969 File Offset: 0x00011B69
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 project(float4 a, float4 ontoB)
		{
			return math.dot(a, ontoB) / math.dot(ontoB, ontoB) * ontoB;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00013980 File Offset: 0x00011B80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 projectsafe(float2 a, float2 ontoB, float2 defaultValue = default(float2))
		{
			float2 proj = math.project(a, ontoB);
			return math.select(defaultValue, proj, math.all(math.isfinite(proj)));
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x000139A8 File Offset: 0x00011BA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 projectsafe(float3 a, float3 ontoB, float3 defaultValue = default(float3))
		{
			float3 proj = math.project(a, ontoB);
			return math.select(defaultValue, proj, math.all(math.isfinite(proj)));
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x000139D0 File Offset: 0x00011BD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 projectsafe(float4 a, float4 ontoB, float4 defaultValue = default(float4))
		{
			float4 proj = math.project(a, ontoB);
			return math.select(defaultValue, proj, math.all(math.isfinite(proj)));
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000139F7 File Offset: 0x00011BF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 project(double2 a, double2 ontoB)
		{
			return math.dot(a, ontoB) / math.dot(ontoB, ontoB) * ontoB;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00013A0E File Offset: 0x00011C0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 project(double3 a, double3 ontoB)
		{
			return math.dot(a, ontoB) / math.dot(ontoB, ontoB) * ontoB;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00013A25 File Offset: 0x00011C25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 project(double4 a, double4 ontoB)
		{
			return math.dot(a, ontoB) / math.dot(ontoB, ontoB) * ontoB;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00013A3C File Offset: 0x00011C3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 projectsafe(double2 a, double2 ontoB, double2 defaultValue = default(double2))
		{
			double2 proj = math.project(a, ontoB);
			return math.select(defaultValue, proj, math.all(math.isfinite(proj)));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00013A64 File Offset: 0x00011C64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 projectsafe(double3 a, double3 ontoB, double3 defaultValue = default(double3))
		{
			double3 proj = math.project(a, ontoB);
			return math.select(defaultValue, proj, math.all(math.isfinite(proj)));
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00013A8C File Offset: 0x00011C8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 projectsafe(double4 a, double4 ontoB, double4 defaultValue = default(double4))
		{
			double4 proj = math.project(a, ontoB);
			return math.select(defaultValue, proj, math.all(math.isfinite(proj)));
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00013AB3 File Offset: 0x00011CB3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 faceforward(float2 n, float2 i, float2 ng)
		{
			return math.select(n, -n, math.dot(ng, i) >= 0f);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00013AD2 File Offset: 0x00011CD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 faceforward(float3 n, float3 i, float3 ng)
		{
			return math.select(n, -n, math.dot(ng, i) >= 0f);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00013AF1 File Offset: 0x00011CF1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 faceforward(float4 n, float4 i, float4 ng)
		{
			return math.select(n, -n, math.dot(ng, i) >= 0f);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00013B10 File Offset: 0x00011D10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 faceforward(double2 n, double2 i, double2 ng)
		{
			return math.select(n, -n, math.dot(ng, i) >= 0.0);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00013B33 File Offset: 0x00011D33
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 faceforward(double3 n, double3 i, double3 ng)
		{
			return math.select(n, -n, math.dot(ng, i) >= 0.0);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00013B56 File Offset: 0x00011D56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 faceforward(double4 n, double4 i, double4 ng)
		{
			return math.select(n, -n, math.dot(ng, i) >= 0.0);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00013B79 File Offset: 0x00011D79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(float x, out float s, out float c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00013B8B File Offset: 0x00011D8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(float2 x, out float2 s, out float2 c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00013BA5 File Offset: 0x00011DA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(float3 x, out float3 s, out float3 c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00013BBF File Offset: 0x00011DBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(float4 x, out float4 s, out float4 c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00013BD9 File Offset: 0x00011DD9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(double x, out double s, out double c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00013BEB File Offset: 0x00011DEB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(double2 x, out double2 s, out double2 c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00013C05 File Offset: 0x00011E05
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(double3 x, out double3 s, out double3 c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00013C1F File Offset: 0x00011E1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void sincos(double4 x, out double4 s, out double4 c)
		{
			s = math.sin(x);
			c = math.cos(x);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00013C39 File Offset: 0x00011E39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int countbits(int x)
		{
			return math.countbits((uint)x);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00013C41 File Offset: 0x00011E41
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 countbits(int2 x)
		{
			return math.countbits((uint2)x);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00013C4E File Offset: 0x00011E4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 countbits(int3 x)
		{
			return math.countbits((uint3)x);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00013C5B File Offset: 0x00011E5B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 countbits(int4 x)
		{
			return math.countbits((uint4)x);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00013C68 File Offset: 0x00011E68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int countbits(uint x)
		{
			x -= (x >> 1) & 1431655765U;
			x = (x & 858993459U) + ((x >> 2) & 858993459U);
			return (int)(((x + (x >> 4)) & 252645135U) * 16843009U >> 24);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00013CA0 File Offset: 0x00011EA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 countbits(uint2 x)
		{
			x -= (x >> 1) & 1431655765U;
			x = (x & 858993459U) + ((x >> 2) & 858993459U);
			return math.int2(((x + (x >> 4)) & 252645135U) * 16843009U >> 24);
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00013D18 File Offset: 0x00011F18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 countbits(uint3 x)
		{
			x -= (x >> 1) & 1431655765U;
			x = (x & 858993459U) + ((x >> 2) & 858993459U);
			return math.int3(((x + (x >> 4)) & 252645135U) * 16843009U >> 24);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00013D90 File Offset: 0x00011F90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 countbits(uint4 x)
		{
			x -= (x >> 1) & 1431655765U;
			x = (x & 858993459U) + ((x >> 2) & 858993459U);
			return math.int4(((x + (x >> 4)) & 252645135U) * 16843009U >> 24);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00013E08 File Offset: 0x00012008
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int countbits(ulong x)
		{
			x -= (x >> 1) & 6148914691236517205UL;
			x = (x & 3689348814741910323UL) + ((x >> 2) & 3689348814741910323UL);
			return (int)(((x + (x >> 4)) & 1085102592571150095UL) * 72340172838076673UL >> 56);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00013E5E File Offset: 0x0001205E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int countbits(long x)
		{
			return math.countbits((ulong)x);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00013E66 File Offset: 0x00012066
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int lzcnt(int x)
		{
			return math.lzcnt((uint)x);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00013E6E File Offset: 0x0001206E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 lzcnt(int2 x)
		{
			return math.int2(math.lzcnt(x.x), math.lzcnt(x.y));
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00013E8B File Offset: 0x0001208B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 lzcnt(int3 x)
		{
			return math.int3(math.lzcnt(x.x), math.lzcnt(x.y), math.lzcnt(x.z));
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00013EB3 File Offset: 0x000120B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 lzcnt(int4 x)
		{
			return math.int4(math.lzcnt(x.x), math.lzcnt(x.y), math.lzcnt(x.z), math.lzcnt(x.w));
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00013EE8 File Offset: 0x000120E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int lzcnt(uint x)
		{
			if (x == 0U)
			{
				return 32;
			}
			math.LongDoubleUnion u;
			u.doubleValue = 0.0;
			u.longValue = (long)(4841369599423283200UL + (ulong)x);
			u.doubleValue -= 4503599627370496.0;
			return 1054 - (int)(u.longValue >> 52);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00013F42 File Offset: 0x00012142
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 lzcnt(uint2 x)
		{
			return math.int2(math.lzcnt(x.x), math.lzcnt(x.y));
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00013F5F File Offset: 0x0001215F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 lzcnt(uint3 x)
		{
			return math.int3(math.lzcnt(x.x), math.lzcnt(x.y), math.lzcnt(x.z));
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00013F87 File Offset: 0x00012187
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 lzcnt(uint4 x)
		{
			return math.int4(math.lzcnt(x.x), math.lzcnt(x.y), math.lzcnt(x.z), math.lzcnt(x.w));
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00013FBA File Offset: 0x000121BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int lzcnt(long x)
		{
			return math.lzcnt((ulong)x);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00013FC4 File Offset: 0x000121C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int lzcnt(ulong x)
		{
			if (x == 0UL)
			{
				return 64;
			}
			uint xh = (uint)(x >> 32);
			uint bits = ((xh != 0U) ? xh : ((uint)x));
			int num = ((xh != 0U) ? 1054 : 1086);
			math.LongDoubleUnion u;
			u.doubleValue = 0.0;
			u.longValue = (long)(4841369599423283200UL + (ulong)bits);
			u.doubleValue -= 4503599627370496.0;
			return num - (int)(u.longValue >> 52);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00014037 File Offset: 0x00012237
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int tzcnt(int x)
		{
			return math.tzcnt((uint)x);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0001403F File Offset: 0x0001223F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 tzcnt(int2 x)
		{
			return math.int2(math.tzcnt(x.x), math.tzcnt(x.y));
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0001405C File Offset: 0x0001225C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 tzcnt(int3 x)
		{
			return math.int3(math.tzcnt(x.x), math.tzcnt(x.y), math.tzcnt(x.z));
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x00014084 File Offset: 0x00012284
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 tzcnt(int4 x)
		{
			return math.int4(math.tzcnt(x.x), math.tzcnt(x.y), math.tzcnt(x.z), math.tzcnt(x.w));
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000140B8 File Offset: 0x000122B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int tzcnt(uint x)
		{
			if (x == 0U)
			{
				return 32;
			}
			x &= (uint)(-(uint)((ulong)x));
			math.LongDoubleUnion u;
			u.doubleValue = 0.0;
			u.longValue = (long)(4841369599423283200UL + (ulong)x);
			u.doubleValue -= 4503599627370496.0;
			return (int)(u.longValue >> 52) - 1023;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001411A File Offset: 0x0001231A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 tzcnt(uint2 x)
		{
			return math.int2(math.tzcnt(x.x), math.tzcnt(x.y));
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00014137 File Offset: 0x00012337
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 tzcnt(uint3 x)
		{
			return math.int3(math.tzcnt(x.x), math.tzcnt(x.y), math.tzcnt(x.z));
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001415F File Offset: 0x0001235F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 tzcnt(uint4 x)
		{
			return math.int4(math.tzcnt(x.x), math.tzcnt(x.y), math.tzcnt(x.z), math.tzcnt(x.w));
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00014192 File Offset: 0x00012392
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int tzcnt(long x)
		{
			return math.tzcnt((ulong)x);
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0001419C File Offset: 0x0001239C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int tzcnt(ulong x)
		{
			if (x == 0UL)
			{
				return 64;
			}
			x &= -x;
			uint xl = (uint)x;
			uint bits = ((xl != 0U) ? xl : ((uint)(x >> 32)));
			int offset = ((xl != 0U) ? 1023 : 991);
			math.LongDoubleUnion u;
			u.doubleValue = 0.0;
			u.longValue = (long)(4841369599423283200UL + (ulong)bits);
			u.doubleValue -= 4503599627370496.0;
			return (int)(u.longValue >> 52) - offset;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00014217 File Offset: 0x00012417
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int reversebits(int x)
		{
			return (int)math.reversebits((uint)x);
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001421F File Offset: 0x0001241F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 reversebits(int2 x)
		{
			return (int2)math.reversebits((uint2)x);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00014231 File Offset: 0x00012431
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 reversebits(int3 x)
		{
			return (int3)math.reversebits((uint3)x);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00014243 File Offset: 0x00012443
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 reversebits(int4 x)
		{
			return (int4)math.reversebits((uint4)x);
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00014258 File Offset: 0x00012458
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint reversebits(uint x)
		{
			x = ((x >> 1) & 1431655765U) | ((x & 1431655765U) << 1);
			x = ((x >> 2) & 858993459U) | ((x & 858993459U) << 2);
			x = ((x >> 4) & 252645135U) | ((x & 252645135U) << 4);
			x = ((x >> 8) & 16711935U) | ((x & 16711935U) << 8);
			return (x >> 16) | (x << 16);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x000142C4 File Offset: 0x000124C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 reversebits(uint2 x)
		{
			x = ((x >> 1) & 1431655765U) | ((x & 1431655765U) << 1);
			x = ((x >> 2) & 858993459U) | ((x & 858993459U) << 2);
			x = ((x >> 4) & 252645135U) | ((x & 252645135U) << 4);
			x = ((x >> 8) & 16711935U) | ((x & 16711935U) << 8);
			return (x >> 16) | (x << 16);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001438C File Offset: 0x0001258C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 reversebits(uint3 x)
		{
			x = ((x >> 1) & 1431655765U) | ((x & 1431655765U) << 1);
			x = ((x >> 2) & 858993459U) | ((x & 858993459U) << 2);
			x = ((x >> 4) & 252645135U) | ((x & 252645135U) << 4);
			x = ((x >> 8) & 16711935U) | ((x & 16711935U) << 8);
			return (x >> 16) | (x << 16);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00014454 File Offset: 0x00012654
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 reversebits(uint4 x)
		{
			x = ((x >> 1) & 1431655765U) | ((x & 1431655765U) << 1);
			x = ((x >> 2) & 858993459U) | ((x & 858993459U) << 2);
			x = ((x >> 4) & 252645135U) | ((x & 252645135U) << 4);
			x = ((x >> 8) & 16711935U) | ((x & 16711935U) << 8);
			return (x >> 16) | (x << 16);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0001451A File Offset: 0x0001271A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long reversebits(long x)
		{
			return (long)math.reversebits((ulong)x);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00014524 File Offset: 0x00012724
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong reversebits(ulong x)
		{
			x = ((x >> 1) & 6148914691236517205UL) | ((x & 6148914691236517205UL) << 1);
			x = ((x >> 2) & 3689348814741910323UL) | ((x & 3689348814741910323UL) << 2);
			x = ((x >> 4) & 1085102592571150095UL) | ((x & 1085102592571150095UL) << 4);
			x = ((x >> 8) & 71777214294589695UL) | ((x & 71777214294589695UL) << 8);
			x = ((x >> 16) & 281470681808895UL) | ((x & 281470681808895UL) << 16);
			return (x >> 32) | (x << 32);
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000145CD File Offset: 0x000127CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int rol(int x, int n)
		{
			return (int)math.rol((uint)x, n);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x000145D6 File Offset: 0x000127D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 rol(int2 x, int n)
		{
			return (int2)math.rol((uint2)x, n);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x000145E9 File Offset: 0x000127E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 rol(int3 x, int n)
		{
			return (int3)math.rol((uint3)x, n);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x000145FC File Offset: 0x000127FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 rol(int4 x, int n)
		{
			return (int4)math.rol((uint4)x, n);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001460F File Offset: 0x0001280F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint rol(uint x, int n)
		{
			return (x << n) | (x >> 32 - n);
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x00014621 File Offset: 0x00012821
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 rol(uint2 x, int n)
		{
			return (x << n) | (x >> 32 - n);
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00014639 File Offset: 0x00012839
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 rol(uint3 x, int n)
		{
			return (x << n) | (x >> 32 - n);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00014651 File Offset: 0x00012851
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 rol(uint4 x, int n)
		{
			return (x << n) | (x >> 32 - n);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00014669 File Offset: 0x00012869
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long rol(long x, int n)
		{
			return (long)math.rol((ulong)x, n);
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x00014672 File Offset: 0x00012872
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong rol(ulong x, int n)
		{
			return (x << n) | (x >> 64 - n);
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x00014684 File Offset: 0x00012884
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ror(int x, int n)
		{
			return (int)math.ror((uint)x, n);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001468D File Offset: 0x0001288D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 ror(int2 x, int n)
		{
			return (int2)math.ror((uint2)x, n);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x000146A0 File Offset: 0x000128A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 ror(int3 x, int n)
		{
			return (int3)math.ror((uint3)x, n);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000146B3 File Offset: 0x000128B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 ror(int4 x, int n)
		{
			return (int4)math.ror((uint4)x, n);
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000146C6 File Offset: 0x000128C6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ror(uint x, int n)
		{
			return (x >> n) | (x << 32 - n);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x000146D8 File Offset: 0x000128D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 ror(uint2 x, int n)
		{
			return (x >> n) | (x << 32 - n);
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x000146F0 File Offset: 0x000128F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 ror(uint3 x, int n)
		{
			return (x >> n) | (x << 32 - n);
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x00014708 File Offset: 0x00012908
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 ror(uint4 x, int n)
		{
			return (x >> n) | (x << 32 - n);
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x00014720 File Offset: 0x00012920
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ror(long x, int n)
		{
			return (long)math.ror((ulong)x, n);
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00014729 File Offset: 0x00012929
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ror(ulong x, int n)
		{
			return (x >> n) | (x << 64 - n);
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0001473B File Offset: 0x0001293B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ceilpow2(int x)
		{
			x--;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001476C File Offset: 0x0001296C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 ceilpow2(int2 x)
		{
			x -= 1;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000147D8 File Offset: 0x000129D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 ceilpow2(int3 x)
		{
			x -= 1;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00014844 File Offset: 0x00012A44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 ceilpow2(int4 x)
		{
			x -= 1;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000148AD File Offset: 0x00012AAD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ceilpow2(uint x)
		{
			x -= 1U;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1U;
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000148DC File Offset: 0x00012ADC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 ceilpow2(uint2 x)
		{
			x -= 1U;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1U;
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00014948 File Offset: 0x00012B48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 ceilpow2(uint3 x)
		{
			x -= 1U;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1U;
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000149B4 File Offset: 0x00012BB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 ceilpow2(uint4 x)
		{
			x -= 1U;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			return x + 1U;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00014A1D File Offset: 0x00012C1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ceilpow2(long x)
		{
			x -= 1L;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			x |= x >> 32;
			return x + 1L;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x00014A55 File Offset: 0x00012C55
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ceilpow2(ulong x)
		{
			x -= 1UL;
			x |= x >> 1;
			x |= x >> 2;
			x |= x >> 4;
			x |= x >> 8;
			x |= x >> 16;
			x |= x >> 32;
			return x + 1UL;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00014A8D File Offset: 0x00012C8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ceillog2(int x)
		{
			return 32 - math.lzcnt((uint)(x - 1));
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00014A9A File Offset: 0x00012C9A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 ceillog2(int2 x)
		{
			return new int2(math.ceillog2(x.x), math.ceillog2(x.y));
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00014AB7 File Offset: 0x00012CB7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 ceillog2(int3 x)
		{
			return new int3(math.ceillog2(x.x), math.ceillog2(x.y), math.ceillog2(x.z));
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00014ADF File Offset: 0x00012CDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 ceillog2(int4 x)
		{
			return new int4(math.ceillog2(x.x), math.ceillog2(x.y), math.ceillog2(x.z), math.ceillog2(x.w));
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00014A8D File Offset: 0x00012C8D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ceillog2(uint x)
		{
			return 32 - math.lzcnt(x - 1U);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00014B12 File Offset: 0x00012D12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 ceillog2(uint2 x)
		{
			return new int2(math.ceillog2(x.x), math.ceillog2(x.y));
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00014B2F File Offset: 0x00012D2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 ceillog2(uint3 x)
		{
			return new int3(math.ceillog2(x.x), math.ceillog2(x.y), math.ceillog2(x.z));
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00014B57 File Offset: 0x00012D57
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 ceillog2(uint4 x)
		{
			return new int4(math.ceillog2(x.x), math.ceillog2(x.y), math.ceillog2(x.z), math.ceillog2(x.w));
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00014B8A File Offset: 0x00012D8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int floorlog2(int x)
		{
			return 31 - math.lzcnt((uint)x);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00014B95 File Offset: 0x00012D95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 floorlog2(int2 x)
		{
			return new int2(math.floorlog2(x.x), math.floorlog2(x.y));
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00014BB2 File Offset: 0x00012DB2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 floorlog2(int3 x)
		{
			return new int3(math.floorlog2(x.x), math.floorlog2(x.y), math.floorlog2(x.z));
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00014BDA File Offset: 0x00012DDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 floorlog2(int4 x)
		{
			return new int4(math.floorlog2(x.x), math.floorlog2(x.y), math.floorlog2(x.z), math.floorlog2(x.w));
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00014B8A File Offset: 0x00012D8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int floorlog2(uint x)
		{
			return 31 - math.lzcnt(x);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00014C0D File Offset: 0x00012E0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 floorlog2(uint2 x)
		{
			return new int2(math.floorlog2(x.x), math.floorlog2(x.y));
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00014C2A File Offset: 0x00012E2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 floorlog2(uint3 x)
		{
			return new int3(math.floorlog2(x.x), math.floorlog2(x.y), math.floorlog2(x.z));
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00014C52 File Offset: 0x00012E52
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 floorlog2(uint4 x)
		{
			return new int4(math.floorlog2(x.x), math.floorlog2(x.y), math.floorlog2(x.z), math.floorlog2(x.w));
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00014C85 File Offset: 0x00012E85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float radians(float x)
		{
			return x * 0.017453292f;
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00014C8E File Offset: 0x00012E8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 radians(float2 x)
		{
			return x * 0.017453292f;
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00014C9B File Offset: 0x00012E9B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 radians(float3 x)
		{
			return x * 0.017453292f;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00014CA8 File Offset: 0x00012EA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 radians(float4 x)
		{
			return x * 0.017453292f;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00014CB5 File Offset: 0x00012EB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double radians(double x)
		{
			return x * 0.017453292519943295;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00014CC2 File Offset: 0x00012EC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 radians(double2 x)
		{
			return x * 0.017453292519943295;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x00014CD3 File Offset: 0x00012ED3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 radians(double3 x)
		{
			return x * 0.017453292519943295;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00014CE4 File Offset: 0x00012EE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 radians(double4 x)
		{
			return x * 0.017453292519943295;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x00014CF5 File Offset: 0x00012EF5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float degrees(float x)
		{
			return x * 57.29578f;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00014CFE File Offset: 0x00012EFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 degrees(float2 x)
		{
			return x * 57.29578f;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00014D0B File Offset: 0x00012F0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 degrees(float3 x)
		{
			return x * 57.29578f;
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00014D18 File Offset: 0x00012F18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 degrees(float4 x)
		{
			return x * 57.29578f;
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00014D25 File Offset: 0x00012F25
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double degrees(double x)
		{
			return x * 57.29577951308232;
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00014D32 File Offset: 0x00012F32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 degrees(double2 x)
		{
			return x * 57.29577951308232;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00014D43 File Offset: 0x00012F43
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 degrees(double3 x)
		{
			return x * 57.29577951308232;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00014D54 File Offset: 0x00012F54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 degrees(double4 x)
		{
			return x * 57.29577951308232;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00014D65 File Offset: 0x00012F65
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int cmin(int2 x)
		{
			return math.min(x.x, x.y);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00014D78 File Offset: 0x00012F78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int cmin(int3 x)
		{
			return math.min(math.min(x.x, x.y), x.z);
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00014D96 File Offset: 0x00012F96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int cmin(int4 x)
		{
			return math.min(math.min(x.x, x.y), math.min(x.z, x.w));
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00014DBF File Offset: 0x00012FBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint cmin(uint2 x)
		{
			return math.min(x.x, x.y);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00014DD2 File Offset: 0x00012FD2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint cmin(uint3 x)
		{
			return math.min(math.min(x.x, x.y), x.z);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00014DF0 File Offset: 0x00012FF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint cmin(uint4 x)
		{
			return math.min(math.min(x.x, x.y), math.min(x.z, x.w));
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00014E19 File Offset: 0x00013019
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cmin(float2 x)
		{
			return math.min(x.x, x.y);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00014E2C File Offset: 0x0001302C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cmin(float3 x)
		{
			return math.min(math.min(x.x, x.y), x.z);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00014E4A File Offset: 0x0001304A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cmin(float4 x)
		{
			return math.min(math.min(x.x, x.y), math.min(x.z, x.w));
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00014E73 File Offset: 0x00013073
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cmin(double2 x)
		{
			return math.min(x.x, x.y);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00014E86 File Offset: 0x00013086
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cmin(double3 x)
		{
			return math.min(math.min(x.x, x.y), x.z);
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00014EA4 File Offset: 0x000130A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cmin(double4 x)
		{
			return math.min(math.min(x.x, x.y), math.min(x.z, x.w));
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00014ECD File Offset: 0x000130CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int cmax(int2 x)
		{
			return math.max(x.x, x.y);
		}

		// Token: 0x060005E9 RID: 1513 RVA: 0x00014EE0 File Offset: 0x000130E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int cmax(int3 x)
		{
			return math.max(math.max(x.x, x.y), x.z);
		}

		// Token: 0x060005EA RID: 1514 RVA: 0x00014EFE File Offset: 0x000130FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int cmax(int4 x)
		{
			return math.max(math.max(x.x, x.y), math.max(x.z, x.w));
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00014F27 File Offset: 0x00013127
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint cmax(uint2 x)
		{
			return math.max(x.x, x.y);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00014F3A File Offset: 0x0001313A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint cmax(uint3 x)
		{
			return math.max(math.max(x.x, x.y), x.z);
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00014F58 File Offset: 0x00013158
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint cmax(uint4 x)
		{
			return math.max(math.max(x.x, x.y), math.max(x.z, x.w));
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00014F81 File Offset: 0x00013181
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cmax(float2 x)
		{
			return math.max(x.x, x.y);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00014F94 File Offset: 0x00013194
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cmax(float3 x)
		{
			return math.max(math.max(x.x, x.y), x.z);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00014FB2 File Offset: 0x000131B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float cmax(float4 x)
		{
			return math.max(math.max(x.x, x.y), math.max(x.z, x.w));
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00014FDB File Offset: 0x000131DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cmax(double2 x)
		{
			return math.max(x.x, x.y);
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00014FEE File Offset: 0x000131EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cmax(double3 x)
		{
			return math.max(math.max(x.x, x.y), x.z);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001500C File Offset: 0x0001320C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double cmax(double4 x)
		{
			return math.max(math.max(x.x, x.y), math.max(x.z, x.w));
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00015035 File Offset: 0x00013235
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int csum(int2 x)
		{
			return x.x + x.y;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00015044 File Offset: 0x00013244
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int csum(int3 x)
		{
			return x.x + x.y + x.z;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001505A File Offset: 0x0001325A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int csum(int4 x)
		{
			return x.x + x.y + x.z + x.w;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00015077 File Offset: 0x00013277
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint csum(uint2 x)
		{
			return x.x + x.y;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00015086 File Offset: 0x00013286
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint csum(uint3 x)
		{
			return x.x + x.y + x.z;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x0001509C File Offset: 0x0001329C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint csum(uint4 x)
		{
			return x.x + x.y + x.z + x.w;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000150B9 File Offset: 0x000132B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float csum(float2 x)
		{
			return x.x + x.y;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000150C8 File Offset: 0x000132C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float csum(float3 x)
		{
			return x.x + x.y + x.z;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000150DE File Offset: 0x000132DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float csum(float4 x)
		{
			return x.x + x.y + (x.z + x.w);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000150FB File Offset: 0x000132FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double csum(double2 x)
		{
			return x.x + x.y;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001510A File Offset: 0x0001330A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double csum(double3 x)
		{
			return x.x + x.y + x.z;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00015120 File Offset: 0x00013320
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double csum(double4 x)
		{
			return x.x + x.y + (x.z + x.w);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x0001290F File Offset: 0x00010B0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float square(float x)
		{
			return x * x;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001513D File Offset: 0x0001333D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 square(float2 x)
		{
			return x * x;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00015146 File Offset: 0x00013346
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 square(float3 x)
		{
			return x * x;
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001514F File Offset: 0x0001334F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 square(float4 x)
		{
			return x * x;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001290F File Offset: 0x00010B0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double square(double x)
		{
			return x * x;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00015158 File Offset: 0x00013358
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 square(double2 x)
		{
			return x * x;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00015161 File Offset: 0x00013361
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 square(double3 x)
		{
			return x * x;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0001516A File Offset: 0x0001336A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 square(double4 x)
		{
			return x * x;
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x0001290F File Offset: 0x00010B0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int square(int x)
		{
			return x * x;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00015173 File Offset: 0x00013373
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 square(int2 x)
		{
			return x * x;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x0001517C File Offset: 0x0001337C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 square(int3 x)
		{
			return x * x;
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x00015185 File Offset: 0x00013385
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 square(int4 x)
		{
			return x * x;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001290F File Offset: 0x00010B0F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint square(uint x)
		{
			return x * x;
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001518E File Offset: 0x0001338E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 square(uint2 x)
		{
			return x * x;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00015197 File Offset: 0x00013397
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 square(uint3 x)
		{
			return x * x;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x000151A0 File Offset: 0x000133A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 square(uint4 x)
		{
			return x * x;
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x000151AC File Offset: 0x000133AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int compress(int* output, int index, int4 val, bool4 mask)
		{
			if (mask.x)
			{
				output[index++] = val.x;
			}
			if (mask.y)
			{
				output[index++] = val.y;
			}
			if (mask.z)
			{
				output[index++] = val.z;
			}
			if (mask.w)
			{
				output[index++] = val.w;
			}
			return index;
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00015222 File Offset: 0x00013422
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int compress(uint* output, int index, uint4 val, bool4 mask)
		{
			return math.compress((int*)output, index, *(int4*)(&val), mask);
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00015222 File Offset: 0x00013422
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int compress(float* output, int index, float4 val, bool4 mask)
		{
			return math.compress((int*)output, index, *(int4*)(&val), mask);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00015234 File Offset: 0x00013434
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float f16tof32(uint x)
		{
			uint num = (x & 32767U) << 13;
			uint e = num & 260046848U;
			uint num2 = num + 939524096U + math.select(0U, 939524096U, e == 260046848U);
			return math.asfloat(math.select(num2, math.asuint(math.asfloat(num2 + 8388608U) - 6.1035156E-05f), e == 0U) | ((x & 32768U) << 16));
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000152A0 File Offset: 0x000134A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 f16tof32(uint2 x)
		{
			uint2 @uint = (x & 32767U) << 13;
			uint2 e = @uint & 260046848U;
			uint2 uint2 = @uint + 939524096U + math.select(0U, 939524096U, e == 260046848U);
			return math.asfloat(math.select(uint2, math.asuint(math.asfloat(uint2 + 8388608U) - 6.1035156E-05f), e == 0U) | ((x & 32768U) << 16));
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00015344 File Offset: 0x00013544
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 f16tof32(uint3 x)
		{
			uint3 @uint = (x & 32767U) << 13;
			uint3 e = @uint & 260046848U;
			uint3 uint2 = @uint + 939524096U + math.select(0U, 939524096U, e == 260046848U);
			return math.asfloat(math.select(uint2, math.asuint(math.asfloat(uint2 + 8388608U) - 6.1035156E-05f), e == 0U) | ((x & 32768U) << 16));
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000153E8 File Offset: 0x000135E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 f16tof32(uint4 x)
		{
			uint4 @uint = (x & 32767U) << 13;
			uint4 e = @uint & 260046848U;
			uint4 uint2 = @uint + 939524096U + math.select(0U, 939524096U, e == 260046848U);
			return math.asfloat(math.select(uint2, math.asuint(math.asfloat(uint2 + 8388608U) - 6.1035156E-05f), e == 0U) | ((x & 32768U) << 16));
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001548C File Offset: 0x0001368C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint f32tof16(float x)
		{
			uint ux = math.asuint(x);
			uint uux = ux & 2147479552U;
			return math.select(math.asuint(math.min(math.asfloat(uux) * 1.92593E-34f, 260042750f)) + 4096U >> 13, math.select(31744U, 32256U, uux > 2139095040U), uux >= 2139095040U) | ((ux & 2147487743U) >> 16);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00015500 File Offset: 0x00013700
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 f32tof16(float2 x)
		{
			uint2 ux = math.asuint(x);
			uint2 uux = ux & 2147479552U;
			return math.select((uint2)(math.asint(math.min(math.asfloat(uux) * 1.92593E-34f, 260042750f)) + 4096) >> 13, math.select(31744U, 32256U, (int2)uux > 2139095040), (int2)uux >= 2139095040) | ((ux & 2147487743U) >> 16);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x000155B0 File Offset: 0x000137B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 f32tof16(float3 x)
		{
			uint3 ux = math.asuint(x);
			uint3 uux = ux & 2147479552U;
			return math.select((uint3)(math.asint(math.min(math.asfloat(uux) * 1.92593E-34f, 260042750f)) + 4096) >> 13, math.select(31744U, 32256U, (int3)uux > 2139095040), (int3)uux >= 2139095040) | ((ux & 2147487743U) >> 16);
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00015660 File Offset: 0x00013860
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 f32tof16(float4 x)
		{
			uint4 ux = math.asuint(x);
			uint4 uux = ux & 2147479552U;
			return math.select((uint4)(math.asint(math.min(math.asfloat(uux) * 1.92593E-34f, 260042750f)) + 4096) >> 13, math.select(31744U, 32256U, (int4)uux > 2139095040), (int4)uux >= 2139095040) | ((ux & 2147487743U) >> 16);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00015710 File Offset: 0x00013910
		public static void orthonormal_basis(float3 normal, out float3 basis1, out float3 basis2)
		{
			float sign = ((normal.z >= 0f) ? 1f : (-1f));
			float a = -1f / (sign + normal.z);
			float b = normal.x * normal.y * a;
			basis1.x = 1f + sign * normal.x * normal.x * a;
			basis1.y = sign * b;
			basis1.z = -sign * normal.x;
			basis2.x = b;
			basis2.y = sign + normal.y * normal.y * a;
			basis2.z = -normal.y;
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000157B8 File Offset: 0x000139B8
		public static void orthonormal_basis(double3 normal, out double3 basis1, out double3 basis2)
		{
			double sign = ((normal.z >= 0.0) ? 1.0 : (-1.0));
			double a = -1.0 / (sign + normal.z);
			double b = normal.x * normal.y * a;
			basis1.x = 1.0 + sign * normal.x * normal.x * a;
			basis1.y = sign * b;
			basis1.z = -sign * normal.x;
			basis2.x = b;
			basis2.y = sign + normal.y * normal.y * a;
			basis2.z = -normal.y;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00015872 File Offset: 0x00013A72
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float chgsign(float x, float y)
		{
			return math.asfloat(math.asuint(x) ^ (math.asuint(y) & 2147483648U));
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001588C File Offset: 0x00013A8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 chgsign(float2 x, float2 y)
		{
			return math.asfloat(math.asuint(x) ^ (math.asuint(y) & 2147483648U));
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000158AE File Offset: 0x00013AAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 chgsign(float3 x, float3 y)
		{
			return math.asfloat(math.asuint(x) ^ (math.asuint(y) & 2147483648U));
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000158D0 File Offset: 0x00013AD0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 chgsign(float4 x, float4 y)
		{
			return math.asfloat(math.asuint(x) ^ (math.asuint(y) & 2147483648U));
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000158F4 File Offset: 0x00013AF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static uint read32_little_endian(void* pBuffer)
		{
			return (uint)((int)(*(byte*)pBuffer) | ((int)((byte*)pBuffer)[1] << 8) | ((int)((byte*)pBuffer)[2] << 16) | ((int)((byte*)pBuffer)[3] << 24));
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001591C File Offset: 0x00013B1C
		private unsafe static uint hash_with_unaligned_loads(void* pBuffer, int numBytes, uint seed)
		{
			uint4* p = (uint4*)pBuffer;
			uint hash = seed + 374761393U;
			if (numBytes >= 16)
			{
				uint4 state = new uint4(606290984U, 2246822519U, 0U, 1640531535U) + seed;
				int count = numBytes >> 4;
				for (int i = 0; i < count; i++)
				{
					state += *(p++) * 2246822519U;
					state = (state << 13) | (state >> 19);
					state *= 2654435761U;
				}
				hash = math.rol(state.x, 1) + math.rol(state.y, 7) + math.rol(state.z, 12) + math.rol(state.w, 18);
			}
			hash += (uint)numBytes;
			uint* puint = (uint*)p;
			for (int j = 0; j < ((numBytes >> 2) & 3); j++)
			{
				hash += *(puint++) * 3266489917U;
				hash = math.rol(hash, 17) * 668265263U;
			}
			byte* pbyte = (byte*)puint;
			for (int k = 0; k < (numBytes & 3); k++)
			{
				hash += (uint)(*(pbyte++)) * 374761393U;
				hash = math.rol(hash, 11) * 2654435761U;
			}
			hash ^= hash >> 15;
			hash *= 2246822519U;
			hash ^= hash >> 13;
			hash *= 3266489917U;
			return hash ^ (hash >> 16);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00015A84 File Offset: 0x00013C84
		private unsafe static uint hash_without_unaligned_loads(void* pBuffer, int numBytes, uint seed)
		{
			byte* p = (byte*)pBuffer;
			uint hash = seed + 374761393U;
			if (numBytes >= 16)
			{
				uint4 state = new uint4(606290984U, 2246822519U, 0U, 1640531535U) + seed;
				int count = numBytes >> 4;
				for (int i = 0; i < count; i++)
				{
					uint4 data = new uint4(math.read32_little_endian((void*)p), math.read32_little_endian((void*)(p + 4)), math.read32_little_endian((void*)(p + 8)), math.read32_little_endian((void*)(p + 12)));
					state += data * 2246822519U;
					state = math.rol(state, 13);
					state *= 2654435761U;
					p += 16;
				}
				hash = math.rol(state.x, 1) + math.rol(state.y, 7) + math.rol(state.z, 12) + math.rol(state.w, 18);
			}
			hash += (uint)numBytes;
			for (int j = 0; j < ((numBytes >> 2) & 3); j++)
			{
				hash += math.read32_little_endian((void*)p) * 3266489917U;
				hash = math.rol(hash, 17) * 668265263U;
				p += 4;
			}
			for (int k = 0; k < (numBytes & 3); k++)
			{
				hash += (uint)(*(p++)) * 374761393U;
				hash = math.rol(hash, 11) * 2654435761U;
			}
			hash ^= hash >> 15;
			hash *= 2246822519U;
			hash ^= hash >> 13;
			hash *= 3266489917U;
			return hash ^ (hash >> 16);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00015BEC File Offset: 0x00013DEC
		public unsafe static uint hash(void* pBuffer, int numBytes, uint seed = 0U)
		{
			return math.hash_with_unaligned_loads(pBuffer, numBytes, seed);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00015BF6 File Offset: 0x00013DF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 up()
		{
			return new float3(0f, 1f, 0f);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00015C0C File Offset: 0x00013E0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 down()
		{
			return new float3(0f, -1f, 0f);
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00015C22 File Offset: 0x00013E22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 forward()
		{
			return new float3(0f, 0f, 1f);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00015C38 File Offset: 0x00013E38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 back()
		{
			return new float3(0f, 0f, -1f);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00015C4E File Offset: 0x00013E4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 left()
		{
			return new float3(-1f, 0f, 0f);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00015C64 File Offset: 0x00013E64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 right()
		{
			return new float3(1f, 0f, 0f);
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00015C7C File Offset: 0x00013E7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 EulerXYZ(quaternion q)
		{
			float4 qv = q.value;
			float4 d = qv * qv.wwww * math.float4(2f);
			float4 d2 = qv * qv.yzxw * math.float4(2f);
			float4 d3 = qv * qv;
			float3 euler = Unity.Mathematics.float3.zero;
			float y = d2.z - d.y;
			if (y * y < 0.99999595f)
			{
				float num = d2.y + d.x;
				float x2 = d3.z + d3.w - d3.y - d3.x;
				float z = d2.x + d.z;
				float z2 = d3.x + d3.w - d3.y - d3.z;
				euler = math.float3(math.atan2(num, x2), -math.asin(y), math.atan2(z, z2));
			}
			else
			{
				y = math.clamp(y, -1f, 1f);
				float4 abcd = math.float4(d2.z, d.y, d2.x, d.z);
				float num2 = 2f * (abcd.x * abcd.w + abcd.y * abcd.z);
				float x3 = math.csum(abcd * abcd * math.float4(-1f, 1f, -1f, 1f));
				euler = math.float3(math.atan2(num2, x3), -math.asin(y), 0f);
			}
			return euler;
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00015E10 File Offset: 0x00014010
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 EulerXZY(quaternion q)
		{
			float4 qv = q.value;
			float4 d = qv * qv.wwww * math.float4(2f);
			float4 d2 = qv * qv.yzxw * math.float4(2f);
			float4 d3 = qv * qv;
			float3 euler = Unity.Mathematics.float3.zero;
			float y = d2.x + d.z;
			if (y * y < 0.99999595f)
			{
				float num = -d2.y + d.x;
				float x2 = d3.y + d3.w - d3.z - d3.x;
				float z = -d2.z + d.y;
				float z2 = d3.x + d3.w - d3.y - d3.z;
				euler = math.float3(math.atan2(num, x2), math.asin(y), math.atan2(z, z2));
			}
			else
			{
				y = math.clamp(y, -1f, 1f);
				float4 abcd = math.float4(d2.x, d.z, d2.z, d.y);
				float num2 = 2f * (abcd.x * abcd.w + abcd.y * abcd.z);
				float x3 = math.csum(abcd * abcd * math.float4(-1f, 1f, -1f, 1f));
				euler = math.float3(math.atan2(num2, x3), math.asin(y), 0f);
			}
			return euler.xzy;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00015FAC File Offset: 0x000141AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 EulerYXZ(quaternion q)
		{
			float4 qv = q.value;
			float4 d = qv * qv.wwww * math.float4(2f);
			float4 d2 = qv * qv.yzxw * math.float4(2f);
			float4 d3 = qv * qv;
			float3 euler = Unity.Mathematics.float3.zero;
			float y = d2.y + d.x;
			if (y * y < 0.99999595f)
			{
				float num = -d2.z + d.y;
				float x2 = d3.z + d3.w - d3.x - d3.y;
				float z = -d2.x + d.z;
				float z2 = d3.y + d3.w - d3.z - d3.x;
				euler = math.float3(math.atan2(num, x2), math.asin(y), math.atan2(z, z2));
			}
			else
			{
				y = math.clamp(y, -1f, 1f);
				float4 abcd = math.float4(d2.x, d.z, d2.y, d.x);
				float num2 = 2f * (abcd.x * abcd.w + abcd.y * abcd.z);
				float x3 = math.csum(abcd * abcd * math.float4(-1f, 1f, -1f, 1f));
				euler = math.float3(math.atan2(num2, x3), math.asin(y), 0f);
			}
			return euler.yxz;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00016148 File Offset: 0x00014348
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 EulerYZX(quaternion q)
		{
			float4 qv = q.value;
			float4 d = qv * qv.wwww * math.float4(2f);
			float4 d2 = qv * qv.yzxw * math.float4(2f);
			float4 d3 = qv * qv;
			float3 euler = Unity.Mathematics.float3.zero;
			float y = d2.x - d.z;
			if (y * y < 0.99999595f)
			{
				float num = d2.z + d.y;
				float x2 = d3.x + d3.w - d3.z - d3.y;
				float z = d2.y + d.x;
				float z2 = d3.y + d3.w - d3.x - d3.z;
				euler = math.float3(math.atan2(num, x2), -math.asin(y), math.atan2(z, z2));
			}
			else
			{
				y = math.clamp(y, -1f, 1f);
				float4 abcd = math.float4(d2.x, d.z, d2.y, d.x);
				float num2 = 2f * (abcd.x * abcd.w + abcd.y * abcd.z);
				float x3 = math.csum(abcd * abcd * math.float4(-1f, 1f, -1f, 1f));
				euler = math.float3(math.atan2(num2, x3), -math.asin(y), 0f);
			}
			return euler.zxy;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000162E4 File Offset: 0x000144E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 EulerZXY(quaternion q)
		{
			float4 qv = q.value;
			float4 d = qv * qv.wwww * math.float4(2f);
			float4 d2 = qv * qv.yzxw * math.float4(2f);
			float4 d3 = qv * qv;
			float3 euler = Unity.Mathematics.float3.zero;
			float y = d2.y - d.x;
			if (y * y < 0.99999595f)
			{
				float num = d2.x + d.z;
				float x2 = d3.y + d3.w - d3.x - d3.z;
				float z = d2.z + d.y;
				float z2 = d3.z + d3.w - d3.x - d3.y;
				euler = math.float3(math.atan2(num, x2), -math.asin(y), math.atan2(z, z2));
			}
			else
			{
				y = math.clamp(y, -1f, 1f);
				float4 abcd = math.float4(d2.z, d.y, d2.y, d.x);
				float num2 = 2f * (abcd.x * abcd.w + abcd.y * abcd.z);
				float x3 = math.csum(abcd * abcd * math.float4(-1f, 1f, -1f, 1f));
				euler = math.float3(math.atan2(num2, x3), -math.asin(y), 0f);
			}
			return euler.yzx;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00016480 File Offset: 0x00014680
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 EulerZYX(quaternion q)
		{
			float4 qv = q.value;
			float4 d = qv * qv.wwww * math.float4(2f);
			float4 d2 = qv * qv.yzxw * math.float4(2f);
			float4 d3 = qv * qv;
			float3 euler = Unity.Mathematics.float3.zero;
			float y = d2.z + d.y;
			if (y * y < 0.99999595f)
			{
				float num = -d2.x + d.z;
				float x2 = d3.x + d3.w - d3.y - d3.z;
				float z = -d2.y + d.x;
				float z2 = d3.z + d3.w - d3.y - d3.x;
				euler = math.float3(math.atan2(num, x2), math.asin(y), math.atan2(z, z2));
			}
			else
			{
				y = math.clamp(y, -1f, 1f);
				float4 abcd = math.float4(d2.z, d.y, d2.y, d.x);
				float num2 = 2f * (abcd.x * abcd.w + abcd.y * abcd.z);
				float x3 = math.csum(abcd * abcd * math.float4(-1f, 1f, -1f, 1f));
				euler = math.float3(math.atan2(num2, x3), math.asin(y), 0f);
			}
			return euler.zyx;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001661C File Offset: 0x0001481C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 Euler(quaternion q, math.RotationOrder order = math.RotationOrder.ZXY)
		{
			switch (order)
			{
			case math.RotationOrder.XYZ:
				return math.EulerXYZ(q);
			case math.RotationOrder.XZY:
				return math.EulerXZY(q);
			case math.RotationOrder.YXZ:
				return math.EulerYXZ(q);
			case math.RotationOrder.YZX:
				return math.EulerYZX(q);
			case math.RotationOrder.ZXY:
				return math.EulerZXY(q);
			case math.RotationOrder.ZYX:
				return math.EulerZYX(q);
			default:
				return Unity.Mathematics.float3.zero;
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00016678 File Offset: 0x00014878
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 mulScale(float3x3 m, float3 s)
		{
			return new float3x3(m.c0 * s.x, m.c1 * s.y, m.c2 * s.z);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000166B2 File Offset: 0x000148B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 scaleMul(float3 s, float3x3 m)
		{
			return new float3x3(m.c0 * s, m.c1 * s, m.c2 * s);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x000166DD File Offset: 0x000148DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float4 unpacklo(float4 a, float4 b)
		{
			return math.shuffle(a, b, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightY);
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000166EA File Offset: 0x000148EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double4 unpacklo(double4 a, double4 b)
		{
			return math.shuffle(a, b, math.ShuffleComponent.LeftX, math.ShuffleComponent.RightX, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightY);
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000166F7 File Offset: 0x000148F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float4 unpackhi(float4 a, float4 b)
		{
			return math.shuffle(a, b, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW);
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00016704 File Offset: 0x00014904
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double4 unpackhi(double4 a, double4 b)
		{
			return math.shuffle(a, b, math.ShuffleComponent.LeftZ, math.ShuffleComponent.RightZ, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightW);
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00016711 File Offset: 0x00014911
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float4 movelh(float4 a, float4 b)
		{
			return math.shuffle(a, b, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightX, math.ShuffleComponent.RightY);
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x0001671E File Offset: 0x0001491E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double4 movelh(double4 a, double4 b)
		{
			return math.shuffle(a, b, math.ShuffleComponent.LeftX, math.ShuffleComponent.LeftY, math.ShuffleComponent.RightX, math.ShuffleComponent.RightY);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001672B File Offset: 0x0001492B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float4 movehl(float4 a, float4 b)
		{
			return math.shuffle(b, a, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightW);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00016738 File Offset: 0x00014938
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double4 movehl(double4 a, double4 b)
		{
			return math.shuffle(b, a, math.ShuffleComponent.LeftZ, math.ShuffleComponent.LeftW, math.ShuffleComponent.RightZ, math.ShuffleComponent.RightW);
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00016748 File Offset: 0x00014948
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint fold_to_uint(double x)
		{
			math.LongDoubleUnion u;
			u.longValue = 0L;
			u.doubleValue = x;
			return (uint)(u.longValue >> 32) ^ (uint)u.longValue;
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00016778 File Offset: 0x00014978
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint2 fold_to_uint(double2 x)
		{
			return math.uint2(math.fold_to_uint(x.x), math.fold_to_uint(x.y));
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00016795 File Offset: 0x00014995
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint3 fold_to_uint(double3 x)
		{
			return math.uint3(math.fold_to_uint(x.x), math.fold_to_uint(x.y), math.fold_to_uint(x.z));
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x000167BD File Offset: 0x000149BD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint4 fold_to_uint(double4 x)
		{
			return math.uint4(math.fold_to_uint(x.x), math.fold_to_uint(x.y), math.fold_to_uint(x.z), math.fold_to_uint(x.w));
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x000167F0 File Offset: 0x000149F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(float4x4 f4x4)
		{
			return new float3x3(f4x4);
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x000167F8 File Offset: 0x000149F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 float3x3(quaternion rotation)
		{
			return new float3x3(rotation);
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00016800 File Offset: 0x00014A00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(float3x3 rotation, float3 translation)
		{
			return new float4x4(rotation, translation);
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00016809 File Offset: 0x00014A09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(quaternion rotation, float3 translation)
		{
			return new float4x4(rotation, translation);
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00016812 File Offset: 0x00014A12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 float4x4(RigidTransform transform)
		{
			return new float4x4(transform);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x0001681C File Offset: 0x00014A1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 orthonormalize(float3x3 i)
		{
			float3 u = i.c0;
			float3 v = i.c1 - i.c0 * math.dot(i.c1, i.c0);
			float lenU = math.length(u);
			float lenV = math.length(v);
			bool c = lenU > 1E-30f && lenV > 1E-30f;
			float3x3 o;
			o.c0 = math.select(math.float3(1f, 0f, 0f), u / lenU, c);
			o.c1 = math.select(math.float3(0f, 1f, 0f), v / lenV, c);
			o.c2 = math.cross(o.c0, o.c1);
			return o;
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000168EC File Offset: 0x00014AEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 pseudoinverse(float3x3 m)
		{
			float scaleSq = 0.333333f * (math.lengthsq(m.c0) + math.lengthsq(m.c1) + math.lengthsq(m.c2));
			if (scaleSq < 1E-30f)
			{
				return Unity.Mathematics.float3x3.zero;
			}
			float3 scaleInv = math.rsqrt(scaleSq);
			float3x3 ms = math.mulScale(m, scaleInv);
			float3x3 i;
			if (!math.adjInverse(ms, out i, 1E-06f))
			{
				i = svd.svdInverse(ms);
			}
			return math.mulScale(i, scaleInv);
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00010832 File Offset: 0x0000EA32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float mul(float a, float b)
		{
			return a * b;
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00010939 File Offset: 0x0000EB39
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float mul(float2 a, float2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00016964 File Offset: 0x00014B64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mul(float2 a, float2x2 b)
		{
			return math.float2(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y);
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x000169C0 File Offset: 0x00014BC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(float2 a, float2x3 b)
		{
			return math.float3(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00016A44 File Offset: 0x00014C44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(float2 a, float2x4 b)
		{
			return math.float4(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y, a.x * b.c3.x + a.y * b.c3.y);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00010956 File Offset: 0x0000EB56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float mul(float3 a, float3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00016AEC File Offset: 0x00014CEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mul(float3 a, float3x2 b)
		{
			return math.float2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x00016B70 File Offset: 0x00014D70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(float3 a, float3x3 b)
		{
			return math.float3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x00016C2C File Offset: 0x00014E2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(float3 a, float3x4 b)
		{
			return math.float4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00010981 File Offset: 0x0000EB81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float mul(float4 a, float4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00016D20 File Offset: 0x00014F20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mul(float4 a, float4x2 b)
		{
			return math.float2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00016DC8 File Offset: 0x00014FC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(float4 a, float4x3 b)
		{
			return math.float3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00016EBC File Offset: 0x000150BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(float4 a, float4x4 b)
		{
			return math.float4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00016FFA File Offset: 0x000151FA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mul(float2x2 a, float2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00017024 File Offset: 0x00015224
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 mul(float2x2 a, float2x2 b)
		{
			return math.float2x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00017098 File Offset: 0x00015298
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 mul(float2x2 a, float2x3 b)
		{
			return math.float2x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00017140 File Offset: 0x00015340
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 mul(float2x2 a, float2x4 b)
		{
			return math.float2x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00017216 File Offset: 0x00015416
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mul(float2x3 a, float3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00017258 File Offset: 0x00015458
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 mul(float2x3 a, float3x2 b)
		{
			return math.float2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00017304 File Offset: 0x00015504
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 mul(float2x3 a, float3x3 b)
		{
			return math.float2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000173FC File Offset: 0x000155FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 mul(float2x3 a, float3x4 b)
		{
			return math.float2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00017540 File Offset: 0x00015740
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 mul(float2x4 a, float4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000175A0 File Offset: 0x000157A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x2 mul(float2x4 a, float4x2 b)
		{
			return math.float2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00017680 File Offset: 0x00015880
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x3 mul(float2x4 a, float4x3 b)
		{
			return math.float2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000177C8 File Offset: 0x000159C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2x4 mul(float2x4 a, float4x4 b)
		{
			return math.float2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00017976 File Offset: 0x00015B76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(float3x2 a, float2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x000179A0 File Offset: 0x00015BA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 mul(float3x2 a, float2x2 b)
		{
			return math.float3x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00017A14 File Offset: 0x00015C14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 mul(float3x2 a, float2x3 b)
		{
			return math.float3x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00017ABC File Offset: 0x00015CBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 mul(float3x2 a, float2x4 b)
		{
			return math.float3x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00017B92 File Offset: 0x00015D92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(float3x3 a, float3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00017BD4 File Offset: 0x00015DD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 mul(float3x3 a, float3x2 b)
		{
			return math.float3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00017C80 File Offset: 0x00015E80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 mul(float3x3 a, float3x3 b)
		{
			return math.float3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00017D78 File Offset: 0x00015F78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 mul(float3x3 a, float3x4 b)
		{
			return math.float3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00017EBC File Offset: 0x000160BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(float3x4 a, float4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00017F1C File Offset: 0x0001611C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x2 mul(float3x4 a, float4x2 b)
		{
			return math.float3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00017FFC File Offset: 0x000161FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x3 mul(float3x4 a, float4x3 b)
		{
			return math.float3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00018144 File Offset: 0x00016344
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3x4 mul(float3x4 a, float4x4 b)
		{
			return math.float3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000182F2 File Offset: 0x000164F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(float4x2 a, float2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001831C File Offset: 0x0001651C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 mul(float4x2 a, float2x2 b)
		{
			return math.float4x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00018390 File Offset: 0x00016590
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 mul(float4x2 a, float2x3 b)
		{
			return math.float4x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00018438 File Offset: 0x00016638
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 mul(float4x2 a, float2x4 b)
		{
			return math.float4x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001850E File Offset: 0x0001670E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(float4x3 a, float3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00018550 File Offset: 0x00016750
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 mul(float4x3 a, float3x2 b)
		{
			return math.float4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000185FC File Offset: 0x000167FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 mul(float4x3 a, float3x3 b)
		{
			return math.float4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x000186F4 File Offset: 0x000168F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 mul(float4x3 a, float3x4 b)
		{
			return math.float4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00018838 File Offset: 0x00016A38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(float4x4 a, float4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00018898 File Offset: 0x00016A98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x2 mul(float4x4 a, float4x2 b)
		{
			return math.float4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00018978 File Offset: 0x00016B78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x3 mul(float4x4 a, float4x3 b)
		{
			return math.float4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00018AC0 File Offset: 0x00016CC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4x4 mul(float4x4 a, float4x4 b)
		{
			return math.float4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00010832 File Offset: 0x0000EA32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double mul(double a, double b)
		{
			return a * b;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x000109BA File Offset: 0x0000EBBA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double mul(double2 a, double2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00018C70 File Offset: 0x00016E70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mul(double2 a, double2x2 b)
		{
			return math.double2(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y);
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00018CCC File Offset: 0x00016ECC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mul(double2 a, double2x3 b)
		{
			return math.double3(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00018D50 File Offset: 0x00016F50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mul(double2 a, double2x4 b)
		{
			return math.double4(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y, a.x * b.c3.x + a.y * b.c3.y);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x000109D7 File Offset: 0x0000EBD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double mul(double3 a, double3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00018DF8 File Offset: 0x00016FF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mul(double3 a, double3x2 b)
		{
			return math.double2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00018E7C File Offset: 0x0001707C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mul(double3 a, double3x3 b)
		{
			return math.double3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00018F38 File Offset: 0x00017138
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mul(double3 a, double3x4 b)
		{
			return math.double4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00010A02 File Offset: 0x0000EC02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double mul(double4 a, double4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0001902C File Offset: 0x0001722C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mul(double4 a, double4x2 b)
		{
			return math.double2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x000190D4 File Offset: 0x000172D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mul(double4 a, double4x3 b)
		{
			return math.double3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x000191C8 File Offset: 0x000173C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mul(double4 a, double4x4 b)
		{
			return math.double4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00019306 File Offset: 0x00017506
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mul(double2x2 a, double2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00019330 File Offset: 0x00017530
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 mul(double2x2 a, double2x2 b)
		{
			return math.double2x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000193A4 File Offset: 0x000175A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 mul(double2x2 a, double2x3 b)
		{
			return math.double2x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001944C File Offset: 0x0001764C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 mul(double2x2 a, double2x4 b)
		{
			return math.double2x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00019522 File Offset: 0x00017722
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mul(double2x3 a, double3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00019564 File Offset: 0x00017764
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 mul(double2x3 a, double3x2 b)
		{
			return math.double2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00019610 File Offset: 0x00017810
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 mul(double2x3 a, double3x3 b)
		{
			return math.double2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00019708 File Offset: 0x00017908
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 mul(double2x3 a, double3x4 b)
		{
			return math.double2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001984C File Offset: 0x00017A4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2 mul(double2x4 a, double4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000198AC File Offset: 0x00017AAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x2 mul(double2x4 a, double4x2 b)
		{
			return math.double2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x0001998C File Offset: 0x00017B8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x3 mul(double2x4 a, double4x3 b)
		{
			return math.double2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00019AD4 File Offset: 0x00017CD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double2x4 mul(double2x4 a, double4x4 b)
		{
			return math.double2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x00019C82 File Offset: 0x00017E82
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mul(double3x2 a, double2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00019CAC File Offset: 0x00017EAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 mul(double3x2 a, double2x2 b)
		{
			return math.double3x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00019D20 File Offset: 0x00017F20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 mul(double3x2 a, double2x3 b)
		{
			return math.double3x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x00019DC8 File Offset: 0x00017FC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 mul(double3x2 a, double2x4 b)
		{
			return math.double3x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x00019E9E File Offset: 0x0001809E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mul(double3x3 a, double3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x00019EE0 File Offset: 0x000180E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 mul(double3x3 a, double3x2 b)
		{
			return math.double3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00019F8C File Offset: 0x0001818C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 mul(double3x3 a, double3x3 b)
		{
			return math.double3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0001A084 File Offset: 0x00018284
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 mul(double3x3 a, double3x4 b)
		{
			return math.double3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001A1C8 File Offset: 0x000183C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3 mul(double3x4 a, double4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x0001A228 File Offset: 0x00018428
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x2 mul(double3x4 a, double4x2 b)
		{
			return math.double3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001A308 File Offset: 0x00018508
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x3 mul(double3x4 a, double4x3 b)
		{
			return math.double3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001A450 File Offset: 0x00018650
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double3x4 mul(double3x4 a, double4x4 b)
		{
			return math.double3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001A5FE File Offset: 0x000187FE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mul(double4x2 a, double2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001A628 File Offset: 0x00018828
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 mul(double4x2 a, double2x2 b)
		{
			return math.double4x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001A69C File Offset: 0x0001889C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 mul(double4x2 a, double2x3 b)
		{
			return math.double4x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001A744 File Offset: 0x00018944
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 mul(double4x2 a, double2x4 b)
		{
			return math.double4x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001A81A File Offset: 0x00018A1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mul(double4x3 a, double3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001A85C File Offset: 0x00018A5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 mul(double4x3 a, double3x2 b)
		{
			return math.double4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001A908 File Offset: 0x00018B08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 mul(double4x3 a, double3x3 b)
		{
			return math.double4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001AA00 File Offset: 0x00018C00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 mul(double4x3 a, double3x4 b)
		{
			return math.double4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0001AB44 File Offset: 0x00018D44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4 mul(double4x4 a, double4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0001ABA4 File Offset: 0x00018DA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x2 mul(double4x4 a, double4x2 b)
		{
			return math.double4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x0001AC84 File Offset: 0x00018E84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x3 mul(double4x4 a, double4x3 b)
		{
			return math.double4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0001ADCC File Offset: 0x00018FCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double4x4 mul(double4x4 a, double4x4 b)
		{
			return math.double4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00010832 File Offset: 0x0000EA32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int mul(int a, int b)
		{
			return a * b;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00010837 File Offset: 0x0000EA37
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int mul(int2 a, int2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0001AF7C File Offset: 0x0001917C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mul(int2 a, int2x2 b)
		{
			return math.int2(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001AFD8 File Offset: 0x000191D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mul(int2 a, int2x3 b)
		{
			return math.int3(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001B05C File Offset: 0x0001925C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mul(int2 a, int2x4 b)
		{
			return math.int4(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y, a.x * b.c3.x + a.y * b.c3.y);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00010854 File Offset: 0x0000EA54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int mul(int3 a, int3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001B104 File Offset: 0x00019304
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mul(int3 a, int3x2 b)
		{
			return math.int2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001B188 File Offset: 0x00019388
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mul(int3 a, int3x3 b)
		{
			return math.int3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001B244 File Offset: 0x00019444
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mul(int3 a, int3x4 b)
		{
			return math.int4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001087F File Offset: 0x0000EA7F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int mul(int4 a, int4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001B338 File Offset: 0x00019538
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mul(int4 a, int4x2 b)
		{
			return math.int2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001B3E0 File Offset: 0x000195E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mul(int4 a, int4x3 b)
		{
			return math.int3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001B4D4 File Offset: 0x000196D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mul(int4 a, int4x4 b)
		{
			return math.int4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001B612 File Offset: 0x00019812
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mul(int2x2 a, int2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001B63C File Offset: 0x0001983C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 mul(int2x2 a, int2x2 b)
		{
			return math.int2x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001B6B0 File Offset: 0x000198B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 mul(int2x2 a, int2x3 b)
		{
			return math.int2x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001B758 File Offset: 0x00019958
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 mul(int2x2 a, int2x4 b)
		{
			return math.int2x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001B82E File Offset: 0x00019A2E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mul(int2x3 a, int3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001B870 File Offset: 0x00019A70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 mul(int2x3 a, int3x2 b)
		{
			return math.int2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001B91C File Offset: 0x00019B1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 mul(int2x3 a, int3x3 b)
		{
			return math.int2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001BA14 File Offset: 0x00019C14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 mul(int2x3 a, int3x4 b)
		{
			return math.int2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001BB58 File Offset: 0x00019D58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2 mul(int2x4 a, int4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001BBB8 File Offset: 0x00019DB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x2 mul(int2x4 a, int4x2 b)
		{
			return math.int2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001BC98 File Offset: 0x00019E98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x3 mul(int2x4 a, int4x3 b)
		{
			return math.int2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001BDE0 File Offset: 0x00019FE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int2x4 mul(int2x4 a, int4x4 b)
		{
			return math.int2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001BF8E File Offset: 0x0001A18E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mul(int3x2 a, int2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001BFB8 File Offset: 0x0001A1B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 mul(int3x2 a, int2x2 b)
		{
			return math.int3x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001C02C File Offset: 0x0001A22C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 mul(int3x2 a, int2x3 b)
		{
			return math.int3x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001C0D4 File Offset: 0x0001A2D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 mul(int3x2 a, int2x4 b)
		{
			return math.int3x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001C1AA File Offset: 0x0001A3AA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mul(int3x3 a, int3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001C1EC File Offset: 0x0001A3EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 mul(int3x3 a, int3x2 b)
		{
			return math.int3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001C298 File Offset: 0x0001A498
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 mul(int3x3 a, int3x3 b)
		{
			return math.int3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001C390 File Offset: 0x0001A590
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 mul(int3x3 a, int3x4 b)
		{
			return math.int3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001C4D4 File Offset: 0x0001A6D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3 mul(int3x4 a, int4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001C534 File Offset: 0x0001A734
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x2 mul(int3x4 a, int4x2 b)
		{
			return math.int3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001C614 File Offset: 0x0001A814
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x3 mul(int3x4 a, int4x3 b)
		{
			return math.int3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001C75C File Offset: 0x0001A95C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int3x4 mul(int3x4 a, int4x4 b)
		{
			return math.int3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001C90A File Offset: 0x0001AB0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mul(int4x2 a, int2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001C934 File Offset: 0x0001AB34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 mul(int4x2 a, int2x2 b)
		{
			return math.int4x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001C9A8 File Offset: 0x0001ABA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 mul(int4x2 a, int2x3 b)
		{
			return math.int4x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001CA50 File Offset: 0x0001AC50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 mul(int4x2 a, int2x4 b)
		{
			return math.int4x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001CB26 File Offset: 0x0001AD26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mul(int4x3 a, int3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001CB68 File Offset: 0x0001AD68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 mul(int4x3 a, int3x2 b)
		{
			return math.int4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001CC14 File Offset: 0x0001AE14
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 mul(int4x3 a, int3x3 b)
		{
			return math.int4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001CD0C File Offset: 0x0001AF0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 mul(int4x3 a, int3x4 b)
		{
			return math.int4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001CE50 File Offset: 0x0001B050
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4 mul(int4x4 a, int4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001CEB0 File Offset: 0x0001B0B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x2 mul(int4x4 a, int4x2 b)
		{
			return math.int4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001CF90 File Offset: 0x0001B190
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x3 mul(int4x4 a, int4x3 b)
		{
			return math.int4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001D0D8 File Offset: 0x0001B2D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int4x4 mul(int4x4 a, int4x4 b)
		{
			return math.int4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00010832 File Offset: 0x0000EA32
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint mul(uint a, uint b)
		{
			return a * b;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000108B8 File Offset: 0x0000EAB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint mul(uint2 a, uint2 b)
		{
			return a.x * b.x + a.y * b.y;
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001D288 File Offset: 0x0001B488
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mul(uint2 a, uint2x2 b)
		{
			return math.uint2(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001D2E4 File Offset: 0x0001B4E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mul(uint2 a, uint2x3 b)
		{
			return math.uint3(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001D368 File Offset: 0x0001B568
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mul(uint2 a, uint2x4 b)
		{
			return math.uint4(a.x * b.c0.x + a.y * b.c0.y, a.x * b.c1.x + a.y * b.c1.y, a.x * b.c2.x + a.y * b.c2.y, a.x * b.c3.x + a.y * b.c3.y);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000108D5 File Offset: 0x0000EAD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint mul(uint3 a, uint3 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z;
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001D410 File Offset: 0x0001B610
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mul(uint3 a, uint3x2 b)
		{
			return math.uint2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001D494 File Offset: 0x0001B694
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mul(uint3 a, uint3x3 b)
		{
			return math.uint3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001D550 File Offset: 0x0001B750
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mul(uint3 a, uint3x4 b)
		{
			return math.uint4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00010900 File Offset: 0x0000EB00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint mul(uint4 a, uint4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001D644 File Offset: 0x0001B844
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mul(uint4 a, uint4x2 b)
		{
			return math.uint2(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001D6EC File Offset: 0x0001B8EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mul(uint4 a, uint4x3 b)
		{
			return math.uint3(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001D7E0 File Offset: 0x0001B9E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mul(uint4 a, uint4x4 b)
		{
			return math.uint4(a.x * b.c0.x + a.y * b.c0.y + a.z * b.c0.z + a.w * b.c0.w, a.x * b.c1.x + a.y * b.c1.y + a.z * b.c1.z + a.w * b.c1.w, a.x * b.c2.x + a.y * b.c2.y + a.z * b.c2.z + a.w * b.c2.w, a.x * b.c3.x + a.y * b.c3.y + a.z * b.c3.z + a.w * b.c3.w);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001D91E File Offset: 0x0001BB1E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mul(uint2x2 a, uint2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001D948 File Offset: 0x0001BB48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 mul(uint2x2 a, uint2x2 b)
		{
			return math.uint2x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001D9BC File Offset: 0x0001BBBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 mul(uint2x2 a, uint2x3 b)
		{
			return math.uint2x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001DA64 File Offset: 0x0001BC64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 mul(uint2x2 a, uint2x4 b)
		{
			return math.uint2x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001DB3A File Offset: 0x0001BD3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mul(uint2x3 a, uint3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001DB7C File Offset: 0x0001BD7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 mul(uint2x3 a, uint3x2 b)
		{
			return math.uint2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001DC28 File Offset: 0x0001BE28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 mul(uint2x3 a, uint3x3 b)
		{
			return math.uint2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001DD20 File Offset: 0x0001BF20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 mul(uint2x3 a, uint3x4 b)
		{
			return math.uint2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001DE64 File Offset: 0x0001C064
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 mul(uint2x4 a, uint4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001DEC4 File Offset: 0x0001C0C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 mul(uint2x4 a, uint4x2 b)
		{
			return math.uint2x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001DFA4 File Offset: 0x0001C1A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 mul(uint2x4 a, uint4x3 b)
		{
			return math.uint2x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 mul(uint2x4 a, uint4x4 b)
		{
			return math.uint2x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001E29A File Offset: 0x0001C49A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mul(uint3x2 a, uint2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001E2C4 File Offset: 0x0001C4C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 mul(uint3x2 a, uint2x2 b)
		{
			return math.uint3x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001E338 File Offset: 0x0001C538
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 mul(uint3x2 a, uint2x3 b)
		{
			return math.uint3x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001E3E0 File Offset: 0x0001C5E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 mul(uint3x2 a, uint2x4 b)
		{
			return math.uint3x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001E4B6 File Offset: 0x0001C6B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mul(uint3x3 a, uint3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001E4F8 File Offset: 0x0001C6F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 mul(uint3x3 a, uint3x2 b)
		{
			return math.uint3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001E5A4 File Offset: 0x0001C7A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 mul(uint3x3 a, uint3x3 b)
		{
			return math.uint3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001E69C File Offset: 0x0001C89C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 mul(uint3x3 a, uint3x4 b)
		{
			return math.uint3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001E7E0 File Offset: 0x0001C9E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 mul(uint3x4 a, uint4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001E840 File Offset: 0x0001CA40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 mul(uint3x4 a, uint4x2 b)
		{
			return math.uint3x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0001E920 File Offset: 0x0001CB20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 mul(uint3x4 a, uint4x3 b)
		{
			return math.uint3x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x0001EA68 File Offset: 0x0001CC68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 mul(uint3x4 a, uint4x4 b)
		{
			return math.uint3x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001EC16 File Offset: 0x0001CE16
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mul(uint4x2 a, uint2 b)
		{
			return a.c0 * b.x + a.c1 * b.y;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001EC40 File Offset: 0x0001CE40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 mul(uint4x2 a, uint2x2 b)
		{
			return math.uint4x2(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0001ECB4 File Offset: 0x0001CEB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 mul(uint4x2 a, uint2x3 b)
		{
			return math.uint4x3(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001ED5C File Offset: 0x0001CF5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 mul(uint4x2 a, uint2x4 b)
		{
			return math.uint4x4(a.c0 * b.c0.x + a.c1 * b.c0.y, a.c0 * b.c1.x + a.c1 * b.c1.y, a.c0 * b.c2.x + a.c1 * b.c2.y, a.c0 * b.c3.x + a.c1 * b.c3.y);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001EE32 File Offset: 0x0001D032
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mul(uint4x3 a, uint3 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0001EE74 File Offset: 0x0001D074
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 mul(uint4x3 a, uint3x2 b)
		{
			return math.uint4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0001EF20 File Offset: 0x0001D120
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 mul(uint4x3 a, uint3x3 b)
		{
			return math.uint4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0001F018 File Offset: 0x0001D218
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 mul(uint4x3 a, uint3x4 b)
		{
			return math.uint4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z);
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x0001F15C File Offset: 0x0001D35C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 mul(uint4x4 a, uint4 b)
		{
			return a.c0 * b.x + a.c1 * b.y + a.c2 * b.z + a.c3 * b.w;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0001F1BC File Offset: 0x0001D3BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 mul(uint4x4 a, uint4x2 b)
		{
			return math.uint4x2(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w);
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0001F29C File Offset: 0x0001D49C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 mul(uint4x4 a, uint4x3 b)
		{
			return math.uint4x3(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0001F3E4 File Offset: 0x0001D5E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 mul(uint4x4 a, uint4x4 b)
		{
			return math.uint4x4(a.c0 * b.c0.x + a.c1 * b.c0.y + a.c2 * b.c0.z + a.c3 * b.c0.w, a.c0 * b.c1.x + a.c1 * b.c1.y + a.c2 * b.c1.z + a.c3 * b.c1.w, a.c0 * b.c2.x + a.c1 * b.c2.y + a.c2 * b.c2.z + a.c3 * b.c2.w, a.c0 * b.c3.x + a.c1 * b.c3.y + a.c2 * b.c3.z + a.c3 * b.c3.w);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0001F592 File Offset: 0x0001D792
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion quaternion(float x, float y, float z, float w)
		{
			return new quaternion(x, y, z, w);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001F59D File Offset: 0x0001D79D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion quaternion(float4 value)
		{
			return new quaternion(value);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0001F5A5 File Offset: 0x0001D7A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion quaternion(float3x3 m)
		{
			return new quaternion(m);
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001F5AD File Offset: 0x0001D7AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion quaternion(float4x4 m)
		{
			return new quaternion(m);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0001F5B5 File Offset: 0x0001D7B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion conjugate(quaternion q)
		{
			return math.quaternion(q.value * math.float4(-1f, -1f, -1f, 1f));
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001F5E0 File Offset: 0x0001D7E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion inverse(quaternion q)
		{
			float4 x = q.value;
			return math.quaternion(math.rcp(math.dot(x, x)) * x * math.float4(-1f, -1f, -1f, 1f));
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x0001F629 File Offset: 0x0001D829
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float dot(quaternion a, quaternion b)
		{
			return math.dot(a.value, b.value);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0001F63C File Offset: 0x0001D83C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float length(quaternion q)
		{
			return math.sqrt(math.dot(q.value, q.value));
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001F654 File Offset: 0x0001D854
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float lengthsq(quaternion q)
		{
			return math.dot(q.value, q.value);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001F668 File Offset: 0x0001D868
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion normalize(quaternion q)
		{
			float4 x = q.value;
			return math.quaternion(math.rsqrt(math.dot(x, x)) * x);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001F694 File Offset: 0x0001D894
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion normalizesafe(quaternion q)
		{
			float4 x = q.value;
			float len = math.dot(x, x);
			return math.quaternion(math.select(Unity.Mathematics.quaternion.identity.value, x * math.rsqrt(len), len > 1.1754944E-38f));
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001F6D8 File Offset: 0x0001D8D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion normalizesafe(quaternion q, quaternion defaultvalue)
		{
			float4 x = q.value;
			float len = math.dot(x, x);
			return math.quaternion(math.select(defaultvalue.value, x * math.rsqrt(len), len > 1.1754944E-38f));
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0001F718 File Offset: 0x0001D918
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion unitexp(quaternion q)
		{
			float v_rcp_len = math.rsqrt(math.dot(q.value.xyz, q.value.xyz));
			float sin_v_len;
			float cos_v_len;
			math.sincos(math.rcp(v_rcp_len), out sin_v_len, out cos_v_len);
			return math.quaternion(math.float4(q.value.xyz * v_rcp_len * sin_v_len, cos_v_len));
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0001F77C File Offset: 0x0001D97C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion exp(quaternion q)
		{
			float v_rcp_len = math.rsqrt(math.dot(q.value.xyz, q.value.xyz));
			float sin_v_len;
			float cos_v_len;
			math.sincos(math.rcp(v_rcp_len), out sin_v_len, out cos_v_len);
			return math.quaternion(math.float4(q.value.xyz * v_rcp_len * sin_v_len, cos_v_len) * math.exp(q.value.w));
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001F7F4 File Offset: 0x0001D9F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion unitlog(quaternion q)
		{
			float w = math.clamp(q.value.w, -1f, 1f);
			float s = math.acos(w) * math.rsqrt(1f - w * w);
			return math.quaternion(math.float4(q.value.xyz * s, 0f));
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001F854 File Offset: 0x0001DA54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion log(quaternion q)
		{
			float v_len_sq = math.dot(q.value.xyz, q.value.xyz);
			float q_len_sq = v_len_sq + q.value.w * q.value.w;
			float s = math.acos(math.clamp(q.value.w * math.rsqrt(q_len_sq), -1f, 1f)) * math.rsqrt(v_len_sq);
			return math.quaternion(math.float4(q.value.xyz * s, 0.5f * math.log(q_len_sq)));
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001F8F0 File Offset: 0x0001DAF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion mul(quaternion a, quaternion b)
		{
			return math.quaternion(a.value.wwww * b.value + (a.value.xyzx * b.value.wwwx + a.value.yzxy * b.value.zxyy) * math.float4(1f, 1f, 1f, -1f) - a.value.zxyz * b.value.yzxz);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001F9A0 File Offset: 0x0001DBA0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 mul(quaternion q, float3 v)
		{
			float3 t = 2f * math.cross(q.value.xyz, v);
			return v + q.value.w * t + math.cross(q.value.xyz, t);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001F9F8 File Offset: 0x0001DBF8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 rotate(quaternion q, float3 v)
		{
			float3 t = 2f * math.cross(q.value.xyz, v);
			return v + q.value.w * t + math.cross(q.value.xyz, t);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001FA50 File Offset: 0x0001DC50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion nlerp(quaternion q1, quaternion q2, float t)
		{
			return math.normalize(q1.value + t * (math.chgsign(q2.value, math.dot(q1, q2)) - q1.value));
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001FA90 File Offset: 0x0001DC90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion slerp(quaternion q1, quaternion q2, float t)
		{
			float dt = math.dot(q1, q2);
			if (dt < 0f)
			{
				dt = -dt;
				q2.value = -q2.value;
			}
			if (dt < 0.9995f)
			{
				float num = math.acos(dt);
				float s = math.rsqrt(1f - dt * dt);
				float w = math.sin(num * (1f - t)) * s;
				float w2 = math.sin(num * t) * s;
				return math.quaternion(q1.value * w + q2.value * w2);
			}
			return math.nlerp(q1, q2, t);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001FB28 File Offset: 0x0001DD28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float angle(quaternion q1, quaternion q2)
		{
			float num = math.asin(math.length(math.normalize(math.mul(math.conjugate(q1), q2)).value.xyz));
			return num + num;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001FB60 File Offset: 0x0001DD60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static quaternion rotation(float3x3 m)
		{
			float det = math.determinant(m);
			if (math.abs(1f - det) < 1E-06f)
			{
				return math.quaternion(m);
			}
			if (math.abs(det) > 1E-06f)
			{
				float3x3 tmp = math.mulScale(m, math.rsqrt(math.float3(math.lengthsq(m.c0), math.lengthsq(m.c1), math.lengthsq(m.c2))));
				if (math.abs(1f - math.determinant(tmp)) < 1E-06f)
				{
					return math.quaternion(tmp);
				}
			}
			return svd.svdRotation(m);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001FBF4 File Offset: 0x0001DDF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static float3x3 adj(float3x3 m, out float det)
		{
			float3x3 adjT;
			adjT.c0 = math.cross(m.c1, m.c2);
			adjT.c1 = math.cross(m.c2, m.c0);
			adjT.c2 = math.cross(m.c0, m.c1);
			det = math.dot(m.c0, adjT.c0);
			return math.transpose(adjT);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001FC64 File Offset: 0x0001DE64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool adjInverse(float3x3 m, out float3x3 i, float epsilon = 1E-30f)
		{
			float det;
			i = math.adj(m, out det);
			bool c = math.abs(det) > epsilon;
			float3 detInv = math.select(math.float3(1f), math.rcp(det), c);
			i = math.scaleMul(detInv, i);
			return c;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001FCB8 File Offset: 0x0001DEB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(quaternion q)
		{
			return math.hash(q.value);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001FCC5 File Offset: 0x0001DEC5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(quaternion q)
		{
			return math.hashwide(q.value);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001FCD2 File Offset: 0x0001DED2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 forward(quaternion q)
		{
			return math.mul(q, math.float3(0f, 0f, 1f));
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001FCEE File Offset: 0x0001DEEE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform RigidTransform(quaternion rot, float3 pos)
		{
			return new RigidTransform(rot, pos);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001FCF7 File Offset: 0x0001DEF7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform RigidTransform(float3x3 rotation, float3 translation)
		{
			return new RigidTransform(rotation, translation);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001FD00 File Offset: 0x0001DF00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform RigidTransform(float4x4 transform)
		{
			return new RigidTransform(transform);
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001FD08 File Offset: 0x0001DF08
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform inverse(RigidTransform t)
		{
			quaternion quaternion = math.inverse(t.rot);
			float3 invTranslation = math.mul(quaternion, -t.pos);
			return new RigidTransform(quaternion, invTranslation);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001FD38 File Offset: 0x0001DF38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static RigidTransform mul(RigidTransform a, RigidTransform b)
		{
			return new RigidTransform(math.mul(a.rot, b.rot), math.mul(a.rot, b.pos) + a.pos);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001FD6C File Offset: 0x0001DF6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float4 mul(RigidTransform a, float4 pos)
		{
			return math.float4(math.mul(a.rot, pos.xyz) + a.pos * pos.w, pos.w);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001FDA1 File Offset: 0x0001DFA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 rotate(RigidTransform a, float3 dir)
		{
			return math.mul(a.rot, dir);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0001FDAF File Offset: 0x0001DFAF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float3 transform(RigidTransform a, float3 pos)
		{
			return math.mul(a.rot, pos) + a.pos;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0001FDC8 File Offset: 0x0001DFC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(RigidTransform t)
		{
			return math.hash(t.rot) + 3318036811U * math.hash(t.pos);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0001FDE8 File Offset: 0x0001DFE8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(RigidTransform t)
		{
			return math.hashwide(t.rot) + 3318036811U * math.hashwide(t.pos).xyzz;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0001FE22 File Offset: 0x0001E022
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(uint x, uint y)
		{
			return new uint2(x, y);
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0001FE2B File Offset: 0x0001E02B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(uint2 xy)
		{
			return new uint2(xy);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001FE33 File Offset: 0x0001E033
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(uint v)
		{
			return new uint2(v);
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001FE3B File Offset: 0x0001E03B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(bool v)
		{
			return new uint2(v);
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001FE43 File Offset: 0x0001E043
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(bool2 v)
		{
			return new uint2(v);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0001FE4B File Offset: 0x0001E04B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(int v)
		{
			return new uint2(v);
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0001FE53 File Offset: 0x0001E053
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(int2 v)
		{
			return new uint2(v);
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0001FE5B File Offset: 0x0001E05B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(float v)
		{
			return new uint2(v);
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0001FE63 File Offset: 0x0001E063
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(float2 v)
		{
			return new uint2(v);
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x0001FE6B File Offset: 0x0001E06B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(double v)
		{
			return new uint2(v);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0001FE73 File Offset: 0x0001E073
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 uint2(double2 v)
		{
			return new uint2(v);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0001FE7B File Offset: 0x0001E07B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint2 v)
		{
			return math.csum(v * math.uint2(1148435377U, 3416333663U)) + 1750611407U;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0001FE9D File Offset: 0x0001E09D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(uint2 v)
		{
			return v * math.uint2(3285396193U, 3110507567U) + 4271396531U;
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0001FEBE File Offset: 0x0001E0BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint shuffle(uint2 left, uint2 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001FEC8 File Offset: 0x0001E0C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 shuffle(uint2 left, uint2 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.uint2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001FEDF File Offset: 0x0001E0DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 shuffle(uint2 left, uint2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.uint3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001FEFF File Offset: 0x0001E0FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 shuffle(uint2 left, uint2 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.uint4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001FF28 File Offset: 0x0001E128
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint select_shuffle_component(uint2 a, uint2 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001FF8D File Offset: 0x0001E18D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(uint2 c0, uint2 c1)
		{
			return new uint2x2(c0, c1);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001FF96 File Offset: 0x0001E196
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(uint m00, uint m01, uint m10, uint m11)
		{
			return new uint2x2(m00, m01, m10, m11);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001FFA1 File Offset: 0x0001E1A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(uint v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001FFA9 File Offset: 0x0001E1A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(bool v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0001FFB1 File Offset: 0x0001E1B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(bool2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0001FFB9 File Offset: 0x0001E1B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(int v)
		{
			return new uint2x2(v);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001FFC1 File Offset: 0x0001E1C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(int2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001FFC9 File Offset: 0x0001E1C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(float v)
		{
			return new uint2x2(v);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001FFD1 File Offset: 0x0001E1D1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(float2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001FFD9 File Offset: 0x0001E1D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(double v)
		{
			return new uint2x2(v);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0001FFE1 File Offset: 0x0001E1E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 uint2x2(double2x2 v)
		{
			return new uint2x2(v);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0001FFE9 File Offset: 0x0001E1E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x2 transpose(uint2x2 v)
		{
			return math.uint2x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0002001C File Offset: 0x0001E21C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint2x2 v)
		{
			return math.csum(v.c0 * math.uint2(3010324327U, 1875523709U) + v.c1 * math.uint2(2937008387U, 3835713223U)) + 2216526373U;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00020070 File Offset: 0x0001E270
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(uint2x2 v)
		{
			return v.c0 * math.uint2(3375971453U, 3559829411U) + v.c1 * math.uint2(3652178029U, 2544260129U) + 2013864031U;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x000200C0 File Offset: 0x0001E2C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(uint2 c0, uint2 c1, uint2 c2)
		{
			return new uint2x3(c0, c1, c2);
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000200CA File Offset: 0x0001E2CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12)
		{
			return new uint2x3(m00, m01, m02, m10, m11, m12);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000200D9 File Offset: 0x0001E2D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(uint v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000200E1 File Offset: 0x0001E2E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(bool v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000200E9 File Offset: 0x0001E2E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(bool2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000200F1 File Offset: 0x0001E2F1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(int v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000200F9 File Offset: 0x0001E2F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(int2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x00020101 File Offset: 0x0001E301
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(float v)
		{
			return new uint2x3(v);
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00020109 File Offset: 0x0001E309
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(float2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00020111 File Offset: 0x0001E311
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(double v)
		{
			return new uint2x3(v);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00020119 File Offset: 0x0001E319
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 uint2x3(double2x3 v)
		{
			return new uint2x3(v);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00020124 File Offset: 0x0001E324
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 transpose(uint2x3 v)
		{
			return math.uint3x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y);
		}

		// Token: 0x0600075D RID: 1885 RVA: 0x00020178 File Offset: 0x0001E378
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint2x3 v)
		{
			return math.csum(v.c0 * math.uint2(4016293529U, 2416021567U) + v.c1 * math.uint2(2828384717U, 2636362241U) + v.c2 * math.uint2(1258410977U, 1952565773U)) + 2037535609U;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x000201E8 File Offset: 0x0001E3E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(uint2x3 v)
		{
			return v.c0 * math.uint2(3592785499U, 3996716183U) + v.c1 * math.uint2(2626301701U, 1306289417U) + v.c2 * math.uint2(2096137163U, 1548578029U) + 4178800919U;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00020257 File Offset: 0x0001E457
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(uint2 c0, uint2 c1, uint2 c2, uint2 c3)
		{
			return new uint2x4(c0, c1, c2, c3);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00020262 File Offset: 0x0001E462
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13)
		{
			return new uint2x4(m00, m01, m02, m03, m10, m11, m12, m13);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x00020275 File Offset: 0x0001E475
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(uint v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0002027D File Offset: 0x0001E47D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(bool v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x00020285 File Offset: 0x0001E485
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(bool2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0002028D File Offset: 0x0001E48D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(int v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00020295 File Offset: 0x0001E495
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(int2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0002029D File Offset: 0x0001E49D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(float v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000202A5 File Offset: 0x0001E4A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(float2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000202AD File Offset: 0x0001E4AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(double v)
		{
			return new uint2x4(v);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x000202B5 File Offset: 0x0001E4B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 uint2x4(double2x4 v)
		{
			return new uint2x4(v);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x000202C0 File Offset: 0x0001E4C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 transpose(uint2x4 v)
		{
			return math.uint4x2(v.c0.x, v.c0.y, v.c1.x, v.c1.y, v.c2.x, v.c2.y, v.c3.x, v.c3.y);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0002032C File Offset: 0x0001E52C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint2x4 v)
		{
			return math.csum(v.c0 * math.uint2(2650080659U, 4052675461U) + v.c1 * math.uint2(2652487619U, 2174136431U) + v.c2 * math.uint2(3528391193U, 2105559227U) + v.c3 * math.uint2(1899745391U, 1966790317U)) + 3516359879U;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x000203BC File Offset: 0x0001E5BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 hashwide(uint2x4 v)
		{
			return v.c0 * math.uint2(3050356579U, 4178586719U) + v.c1 * math.uint2(2558655391U, 1453413133U) + v.c2 * math.uint2(2152428077U, 1938706661U) + v.c3 * math.uint2(1338588197U, 3439609253U) + 3535343003U;
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0002044A File Offset: 0x0001E64A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(uint x, uint y, uint z)
		{
			return new uint3(x, y, z);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x00020454 File Offset: 0x0001E654
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(uint x, uint2 yz)
		{
			return new uint3(x, yz);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0002045D File Offset: 0x0001E65D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(uint2 xy, uint z)
		{
			return new uint3(xy, z);
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x00020466 File Offset: 0x0001E666
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(uint3 xyz)
		{
			return new uint3(xyz);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0002046E File Offset: 0x0001E66E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(uint v)
		{
			return new uint3(v);
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x00020476 File Offset: 0x0001E676
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(bool v)
		{
			return new uint3(v);
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0002047E File Offset: 0x0001E67E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(bool3 v)
		{
			return new uint3(v);
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00020486 File Offset: 0x0001E686
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(int v)
		{
			return new uint3(v);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0002048E File Offset: 0x0001E68E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(int3 v)
		{
			return new uint3(v);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x00020496 File Offset: 0x0001E696
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(float v)
		{
			return new uint3(v);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0002049E File Offset: 0x0001E69E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(float3 v)
		{
			return new uint3(v);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x000204A6 File Offset: 0x0001E6A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(double v)
		{
			return new uint3(v);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000204AE File Offset: 0x0001E6AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 uint3(double3 v)
		{
			return new uint3(v);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000204B6 File Offset: 0x0001E6B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint3 v)
		{
			return math.csum(v * math.uint3(3441847433U, 4052036147U, 2011389559U)) + 2252224297U;
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000204DD File Offset: 0x0001E6DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(uint3 v)
		{
			return v * math.uint3(3784421429U, 1750626223U, 3571447507U) + 3412283213U;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x00020503 File Offset: 0x0001E703
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint shuffle(uint3 left, uint3 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0002050D File Offset: 0x0001E70D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 shuffle(uint3 left, uint3 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.uint2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00020524 File Offset: 0x0001E724
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 shuffle(uint3 left, uint3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.uint3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00020544 File Offset: 0x0001E744
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 shuffle(uint3 left, uint3 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.uint4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x00020570 File Offset: 0x0001E770
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint select_shuffle_component(uint3 a, uint3 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			}
			throw new ArgumentException("Invalid shuffle component: " + component.ToString());
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x000205E7 File Offset: 0x0001E7E7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(uint3 c0, uint3 c1)
		{
			return new uint3x2(c0, c1);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x000205F0 File Offset: 0x0001E7F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(uint m00, uint m01, uint m10, uint m11, uint m20, uint m21)
		{
			return new uint3x2(m00, m01, m10, m11, m20, m21);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x000205FF File Offset: 0x0001E7FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(uint v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00020607 File Offset: 0x0001E807
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(bool v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0002060F File Offset: 0x0001E80F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(bool3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00020617 File Offset: 0x0001E817
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(int v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0002061F File Offset: 0x0001E81F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(int3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x00020627 File Offset: 0x0001E827
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(float v)
		{
			return new uint3x2(v);
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0002062F File Offset: 0x0001E82F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(float3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x00020637 File Offset: 0x0001E837
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(double v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0002063F File Offset: 0x0001E83F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x2 uint3x2(double3x2 v)
		{
			return new uint3x2(v);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x00020648 File Offset: 0x0001E848
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x3 transpose(uint3x2 v)
		{
			return math.uint2x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0002069C File Offset: 0x0001E89C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint3x2 v)
		{
			return math.csum(v.c0 * math.uint3(1365086453U, 3969870067U, 4192899797U) + v.c1 * math.uint3(3271228601U, 1634639009U, 3318036811U)) + 3404170631U;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x000206F8 File Offset: 0x0001E8F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(uint3x2 v)
		{
			return v.c0 * math.uint3(2048213449U, 4164671783U, 1780759499U) + v.c1 * math.uint3(1352369353U, 2446407751U, 1391928079U) + 3475533443U;
		}

		// Token: 0x0600078F RID: 1935 RVA: 0x00020752 File Offset: 0x0001E952
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(uint3 c0, uint3 c1, uint3 c2)
		{
			return new uint3x3(c0, c1, c2);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x0002075C File Offset: 0x0001E95C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12, uint m20, uint m21, uint m22)
		{
			return new uint3x3(m00, m01, m02, m10, m11, m12, m20, m21, m22);
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0002077C File Offset: 0x0001E97C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(uint v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00020784 File Offset: 0x0001E984
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(bool v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x0002078C File Offset: 0x0001E98C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(bool3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00020794 File Offset: 0x0001E994
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(int v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0002079C File Offset: 0x0001E99C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(int3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000207A4 File Offset: 0x0001E9A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(float v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000207AC File Offset: 0x0001E9AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(float3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x000207B4 File Offset: 0x0001E9B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(double v)
		{
			return new uint3x3(v);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000207BC File Offset: 0x0001E9BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 uint3x3(double3x3 v)
		{
			return new uint3x3(v);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x000207C4 File Offset: 0x0001E9C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x3 transpose(uint3x3 v)
		{
			return math.uint3x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0002083C File Offset: 0x0001EA3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint3x3 v)
		{
			return math.csum(v.c0 * math.uint3(2892026051U, 2455987759U, 3868600063U) + v.c1 * math.uint3(3170963179U, 2632835537U, 1136528209U) + v.c2 * math.uint3(2944626401U, 2972762423U, 1417889653U)) + 2080514593U;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x000208BC File Offset: 0x0001EABC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(uint3x3 v)
		{
			return v.c0 * math.uint3(2731544287U, 2828498809U, 2669441947U) + v.c1 * math.uint3(1260114311U, 2650080659U, 4052675461U) + v.c2 * math.uint3(2652487619U, 2174136431U, 3528391193U) + 2105559227U;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0002093A File Offset: 0x0001EB3A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(uint3 c0, uint3 c1, uint3 c2, uint3 c3)
		{
			return new uint3x4(c0, c1, c2, c3);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00020948 File Offset: 0x0001EB48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13, uint m20, uint m21, uint m22, uint m23)
		{
			return new uint3x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0002096E File Offset: 0x0001EB6E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(uint v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00020976 File Offset: 0x0001EB76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(bool v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0002097E File Offset: 0x0001EB7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(bool3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00020986 File Offset: 0x0001EB86
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(int v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0002098E File Offset: 0x0001EB8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(int3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00020996 File Offset: 0x0001EB96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(float v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0002099E File Offset: 0x0001EB9E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(float3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x000209A6 File Offset: 0x0001EBA6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(double v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x000209AE File Offset: 0x0001EBAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 uint3x4(double3x4 v)
		{
			return new uint3x4(v);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x000209B8 File Offset: 0x0001EBB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 transpose(uint3x4 v)
		{
			return math.uint4x3(v.c0.x, v.c0.y, v.c0.z, v.c1.x, v.c1.y, v.c1.z, v.c2.x, v.c2.y, v.c2.z, v.c3.x, v.c3.y, v.c3.z);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00020A50 File Offset: 0x0001EC50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint3x4 v)
		{
			return math.csum(v.c0 * math.uint3(3508684087U, 3919501043U, 1209161033U) + v.c1 * math.uint3(4007793211U, 3819806693U, 3458005183U) + v.c2 * math.uint3(2078515003U, 4206465343U, 3025146473U) + v.c3 * math.uint3(3763046909U, 3678265601U, 2070747979U)) + 1480171127U;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00020AF4 File Offset: 0x0001ECF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 hashwide(uint3x4 v)
		{
			return v.c0 * math.uint3(1588341193U, 4234155257U, 1811310911U) + v.c1 * math.uint3(2635799963U, 4165137857U, 2759770933U) + v.c2 * math.uint3(2759319383U, 3299952959U, 3121178323U) + v.c3 * math.uint3(2948522579U, 1531026433U, 1365086453U) + 3969870067U;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00020B96 File Offset: 0x0001ED96
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint x, uint y, uint z, uint w)
		{
			return new uint4(x, y, z, w);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00020BA1 File Offset: 0x0001EDA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint x, uint y, uint2 zw)
		{
			return new uint4(x, y, zw);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00020BAB File Offset: 0x0001EDAB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint x, uint2 yz, uint w)
		{
			return new uint4(x, yz, w);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00020BB5 File Offset: 0x0001EDB5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint x, uint3 yzw)
		{
			return new uint4(x, yzw);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00020BBE File Offset: 0x0001EDBE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint2 xy, uint z, uint w)
		{
			return new uint4(xy, z, w);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00020BC8 File Offset: 0x0001EDC8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint2 xy, uint2 zw)
		{
			return new uint4(xy, zw);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00020BD1 File Offset: 0x0001EDD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint3 xyz, uint w)
		{
			return new uint4(xyz, w);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00020BDA File Offset: 0x0001EDDA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint4 xyzw)
		{
			return new uint4(xyzw);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00020BE2 File Offset: 0x0001EDE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(uint v)
		{
			return new uint4(v);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00020BEA File Offset: 0x0001EDEA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(bool v)
		{
			return new uint4(v);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00020BF2 File Offset: 0x0001EDF2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(bool4 v)
		{
			return new uint4(v);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00020BFA File Offset: 0x0001EDFA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(int v)
		{
			return new uint4(v);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00020C02 File Offset: 0x0001EE02
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(int4 v)
		{
			return new uint4(v);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00020C0A File Offset: 0x0001EE0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(float v)
		{
			return new uint4(v);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00020C12 File Offset: 0x0001EE12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(float4 v)
		{
			return new uint4(v);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00020C1A File Offset: 0x0001EE1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(double v)
		{
			return new uint4(v);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00020C22 File Offset: 0x0001EE22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 uint4(double4 v)
		{
			return new uint4(v);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00020C2A File Offset: 0x0001EE2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint4 v)
		{
			return math.csum(v * math.uint4(3029516053U, 3547472099U, 2057487037U, 3781937309U)) + 2057338067U;
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00020C56 File Offset: 0x0001EE56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(uint4 v)
		{
			return v * math.uint4(2942577577U, 2834440507U, 2671762487U, 2892026051U) + 2455987759U;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x00020C81 File Offset: 0x0001EE81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint shuffle(uint4 left, uint4 right, math.ShuffleComponent x)
		{
			return math.select_shuffle_component(left, right, x);
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00020C8B File Offset: 0x0001EE8B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2 shuffle(uint4 left, uint4 right, math.ShuffleComponent x, math.ShuffleComponent y)
		{
			return math.uint2(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y));
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00020CA2 File Offset: 0x0001EEA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3 shuffle(uint4 left, uint4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z)
		{
			return math.uint3(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z));
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00020CC2 File Offset: 0x0001EEC2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 shuffle(uint4 left, uint4 right, math.ShuffleComponent x, math.ShuffleComponent y, math.ShuffleComponent z, math.ShuffleComponent w)
		{
			return math.uint4(math.select_shuffle_component(left, right, x), math.select_shuffle_component(left, right, y), math.select_shuffle_component(left, right, z), math.select_shuffle_component(left, right, w));
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00020CEC File Offset: 0x0001EEEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint select_shuffle_component(uint4 a, uint4 b, math.ShuffleComponent component)
		{
			switch (component)
			{
			case math.ShuffleComponent.LeftX:
				return a.x;
			case math.ShuffleComponent.LeftY:
				return a.y;
			case math.ShuffleComponent.LeftZ:
				return a.z;
			case math.ShuffleComponent.LeftW:
				return a.w;
			case math.ShuffleComponent.RightX:
				return b.x;
			case math.ShuffleComponent.RightY:
				return b.y;
			case math.ShuffleComponent.RightZ:
				return b.z;
			case math.ShuffleComponent.RightW:
				return b.w;
			default:
				throw new ArgumentException("Invalid shuffle component: " + component.ToString());
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00020D75 File Offset: 0x0001EF75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(uint4 c0, uint4 c1)
		{
			return new uint4x2(c0, c1);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00020D7E File Offset: 0x0001EF7E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(uint m00, uint m01, uint m10, uint m11, uint m20, uint m21, uint m30, uint m31)
		{
			return new uint4x2(m00, m01, m10, m11, m20, m21, m30, m31);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00020D91 File Offset: 0x0001EF91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(uint v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00020D99 File Offset: 0x0001EF99
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(bool v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00020DA1 File Offset: 0x0001EFA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(bool4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00020DA9 File Offset: 0x0001EFA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(int v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00020DB1 File Offset: 0x0001EFB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(int4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00020DB9 File Offset: 0x0001EFB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(float v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00020DC1 File Offset: 0x0001EFC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(float4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00020DC9 File Offset: 0x0001EFC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(double v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00020DD1 File Offset: 0x0001EFD1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x2 uint4x2(double4x2 v)
		{
			return new uint4x2(v);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00020DDC File Offset: 0x0001EFDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint2x4 transpose(uint4x2 v)
		{
			return math.uint2x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00020E48 File Offset: 0x0001F048
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint4x2 v)
		{
			return math.csum(v.c0 * math.uint4(4198118021U, 2908068253U, 3705492289U, 2497566569U) + v.c1 * math.uint4(2716413241U, 1166264321U, 2503385333U, 2944493077U)) + 2599999021U;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00020EB0 File Offset: 0x0001F0B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(uint4x2 v)
		{
			return v.c0 * math.uint4(3814721321U, 1595355149U, 1728931849U, 2062756937U) + v.c1 * math.uint4(2920485769U, 1562056283U, 2265541847U, 1283419601U) + 1210229737U;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00020F14 File Offset: 0x0001F114
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(uint4 c0, uint4 c1, uint4 c2)
		{
			return new uint4x3(c0, c1, c2);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00020F20 File Offset: 0x0001F120
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(uint m00, uint m01, uint m02, uint m10, uint m11, uint m12, uint m20, uint m21, uint m22, uint m30, uint m31, uint m32)
		{
			return new uint4x3(m00, m01, m02, m10, m11, m12, m20, m21, m22, m30, m31, m32);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00020F46 File Offset: 0x0001F146
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(uint v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00020F4E File Offset: 0x0001F14E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(bool v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00020F56 File Offset: 0x0001F156
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(bool4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00020F5E File Offset: 0x0001F15E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(int v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00020F66 File Offset: 0x0001F166
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(int4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00020F6E File Offset: 0x0001F16E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(float v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00020F76 File Offset: 0x0001F176
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(float4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00020F7E File Offset: 0x0001F17E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(double v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00020F86 File Offset: 0x0001F186
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x3 uint4x3(double4x3 v)
		{
			return new uint4x3(v);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00020F90 File Offset: 0x0001F190
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint3x4 transpose(uint4x3 v)
		{
			return math.uint3x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w);
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00021028 File Offset: 0x0001F228
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint4x3 v)
		{
			return math.csum(v.c0 * math.uint4(3881277847U, 4017968839U, 1727237899U, 1648514723U) + v.c1 * math.uint4(1385344481U, 3538260197U, 4066109527U, 2613148903U) + v.c2 * math.uint4(3367528529U, 1678332449U, 2918459647U, 2744611081U)) + 1952372791U;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x000210B8 File Offset: 0x0001F2B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(uint4x3 v)
		{
			return v.c0 * math.uint4(2631698677U, 4200781601U, 2119021007U, 1760485621U) + v.c1 * math.uint4(3157985881U, 2171534173U, 2723054263U, 1168253063U) + v.c2 * math.uint4(4228926523U, 1610574617U, 1584185147U, 3041325733U) + 3150930919U;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00021145 File Offset: 0x0001F345
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(uint4 c0, uint4 c1, uint4 c2, uint4 c3)
		{
			return new uint4x4(c0, c1, c2, c3);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00021150 File Offset: 0x0001F350
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(uint m00, uint m01, uint m02, uint m03, uint m10, uint m11, uint m12, uint m13, uint m20, uint m21, uint m22, uint m23, uint m30, uint m31, uint m32, uint m33)
		{
			return new uint4x4(m00, m01, m02, m03, m10, m11, m12, m13, m20, m21, m22, m23, m30, m31, m32, m33);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x0002117E File Offset: 0x0001F37E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(uint v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00021186 File Offset: 0x0001F386
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(bool v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x0002118E File Offset: 0x0001F38E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(bool4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00021196 File Offset: 0x0001F396
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(int v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002119E File Offset: 0x0001F39E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(int4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x000211A6 File Offset: 0x0001F3A6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(float v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000211AE File Offset: 0x0001F3AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(float4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x000211B6 File Offset: 0x0001F3B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(double v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x000211BE File Offset: 0x0001F3BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 uint4x4(double4x4 v)
		{
			return new uint4x4(v);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000211C8 File Offset: 0x0001F3C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4x4 transpose(uint4x4 v)
		{
			return math.uint4x4(v.c0.x, v.c0.y, v.c0.z, v.c0.w, v.c1.x, v.c1.y, v.c1.z, v.c1.w, v.c2.x, v.c2.y, v.c2.z, v.c2.w, v.c3.x, v.c3.y, v.c3.z, v.c3.w);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x0002128C File Offset: 0x0001F48C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint hash(uint4x4 v)
		{
			return math.csum(v.c0 * math.uint4(2627668003U, 1520214331U, 2949502447U, 2827819133U) + v.c1 * math.uint4(3480140317U, 2642994593U, 3940484981U, 1954192763U) + v.c2 * math.uint4(1091696537U, 3052428017U, 4253034763U, 2338696631U) + v.c3 * math.uint4(3757372771U, 1885959949U, 3508684087U, 3919501043U)) + 1209161033U;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00021344 File Offset: 0x0001F544
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint4 hashwide(uint4x4 v)
		{
			return v.c0 * math.uint4(4007793211U, 3819806693U, 3458005183U, 2078515003U) + v.c1 * math.uint4(4206465343U, 3025146473U, 3763046909U, 3678265601U) + v.c2 * math.uint4(2070747979U, 1480171127U, 1588341193U, 4234155257U) + v.c3 * math.uint4(1811310911U, 2635799963U, 4165137857U, 2759770933U) + 2759319383U;
		}

		// Token: 0x0400000A RID: 10
		public const double E_DBL = 2.718281828459045;

		// Token: 0x0400000B RID: 11
		public const double LOG2E_DBL = 1.4426950408889634;

		// Token: 0x0400000C RID: 12
		public const double LOG10E_DBL = 0.4342944819032518;

		// Token: 0x0400000D RID: 13
		public const double LN2_DBL = 0.6931471805599453;

		// Token: 0x0400000E RID: 14
		public const double LN10_DBL = 2.302585092994046;

		// Token: 0x0400000F RID: 15
		public const double PI_DBL = 3.141592653589793;

		// Token: 0x04000010 RID: 16
		public const double PI2_DBL = 6.283185307179586;

		// Token: 0x04000011 RID: 17
		public const double PIHALF_DBL = 1.5707963267948966;

		// Token: 0x04000012 RID: 18
		public const double TAU_DBL = 6.283185307179586;

		// Token: 0x04000013 RID: 19
		public const double TODEGREES_DBL = 57.29577951308232;

		// Token: 0x04000014 RID: 20
		public const double TORADIANS_DBL = 0.017453292519943295;

		// Token: 0x04000015 RID: 21
		public const double SQRT2_DBL = 1.4142135623730951;

		// Token: 0x04000016 RID: 22
		public const double EPSILON_DBL = 2.220446049250313E-16;

		// Token: 0x04000017 RID: 23
		public const double INFINITY_DBL = double.PositiveInfinity;

		// Token: 0x04000018 RID: 24
		public const double NAN_DBL = double.NaN;

		// Token: 0x04000019 RID: 25
		public const float FLT_MIN_NORMAL = 1.1754944E-38f;

		// Token: 0x0400001A RID: 26
		public const double DBL_MIN_NORMAL = 2.2250738585072014E-308;

		// Token: 0x0400001B RID: 27
		public const float E = 2.7182817f;

		// Token: 0x0400001C RID: 28
		public const float LOG2E = 1.442695f;

		// Token: 0x0400001D RID: 29
		public const float LOG10E = 0.4342945f;

		// Token: 0x0400001E RID: 30
		public const float LN2 = 0.6931472f;

		// Token: 0x0400001F RID: 31
		public const float LN10 = 2.3025851f;

		// Token: 0x04000020 RID: 32
		public const float PI = 3.1415927f;

		// Token: 0x04000021 RID: 33
		public const float PI2 = 6.2831855f;

		// Token: 0x04000022 RID: 34
		public const float PIHALF = 1.5707964f;

		// Token: 0x04000023 RID: 35
		public const float TAU = 6.2831855f;

		// Token: 0x04000024 RID: 36
		public const float TODEGREES = 57.29578f;

		// Token: 0x04000025 RID: 37
		public const float TORADIANS = 0.017453292f;

		// Token: 0x04000026 RID: 38
		public const float SQRT2 = 1.4142135f;

		// Token: 0x04000027 RID: 39
		public const float EPSILON = 1.1920929E-07f;

		// Token: 0x04000028 RID: 40
		public const float INFINITY = float.PositiveInfinity;

		// Token: 0x04000029 RID: 41
		public const float NAN = float.NaN;

		// Token: 0x0200000A RID: 10
		public enum RotationOrder : byte
		{
			// Token: 0x0400002B RID: 43
			XYZ,
			// Token: 0x0400002C RID: 44
			XZY,
			// Token: 0x0400002D RID: 45
			YXZ,
			// Token: 0x0400002E RID: 46
			YZX,
			// Token: 0x0400002F RID: 47
			ZXY,
			// Token: 0x04000030 RID: 48
			ZYX,
			// Token: 0x04000031 RID: 49
			Default = 4
		}

		// Token: 0x0200000B RID: 11
		public enum ShuffleComponent : byte
		{
			// Token: 0x04000033 RID: 51
			LeftX,
			// Token: 0x04000034 RID: 52
			LeftY,
			// Token: 0x04000035 RID: 53
			LeftZ,
			// Token: 0x04000036 RID: 54
			LeftW,
			// Token: 0x04000037 RID: 55
			RightX,
			// Token: 0x04000038 RID: 56
			RightY,
			// Token: 0x04000039 RID: 57
			RightZ,
			// Token: 0x0400003A RID: 58
			RightW
		}

		// Token: 0x0200000C RID: 12
		[StructLayout(LayoutKind.Explicit)]
		internal struct LongDoubleUnion
		{
			// Token: 0x0400003B RID: 59
			[FieldOffset(0)]
			public long longValue;

			// Token: 0x0400003C RID: 60
			[FieldOffset(0)]
			public double doubleValue;
		}
	}
}
