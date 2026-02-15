using System;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000005 RID: 5
	[Il2CppEagerStaticClassConstruction]
	public static class noise
	{
		// Token: 0x06000004 RID: 4 RVA: 0x000020CC File Offset: 0x000002CC
		public static float2 cellular(float2 P)
		{
			float2 Pi = noise.mod289(math.floor(P));
			float2 @float = math.frac(P);
			float3 oi = math.float3(-1f, 0f, 1f);
			float3 of = math.float3(-0.5f, 0.5f, 1.5f);
			float3 px = noise.permute(Pi.x + oi);
			float3 float2 = noise.permute(px.x + Pi.y + oi);
			float3 ox = math.frac(float2 * 0.14285715f) - 0.42857143f;
			float3 oy = noise.mod7(math.floor(float2 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 dx = @float.x + 0.5f + 1f * ox;
			float3 dy = @float.y - of + 1f * oy;
			float3 d = dx * dx + dy * dy;
			float3 float3 = noise.permute(px.y + Pi.y + oi);
			ox = math.frac(float3 * 0.14285715f) - 0.42857143f;
			oy = noise.mod7(math.floor(float3 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			dx = @float.x - 0.5f + 1f * ox;
			dy = @float.y - of + 1f * oy;
			float3 d2 = dx * dx + dy * dy;
			float3 float4 = noise.permute(px.z + Pi.y + oi);
			ox = math.frac(float4 * 0.14285715f) - 0.42857143f;
			oy = noise.mod7(math.floor(float4 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			dx = @float.x - 1.5f + 1f * ox;
			dy = @float.y - of + 1f * oy;
			float3 d3 = dx * dx + dy * dy;
			float3 float5 = math.min(d, d2);
			d2 = math.max(d, d2);
			d2 = math.min(d2, d3);
			d = math.min(float5, d2);
			d2 = math.max(float5, d2);
			d.xy = ((d.x < d.y) ? d.xy : d.yx);
			d.xz = ((d.x < d.z) ? d.xz : d.zx);
			d.yz = math.min(d.yz, d2.yz);
			d.y = math.min(d.y, d.z);
			d.y = math.min(d.y, d2.x);
			return math.sqrt(d.xy);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002420 File Offset: 0x00000620
		public static float2 cellular2x2(float2 P)
		{
			float2 Pi = noise.mod289(math.floor(P));
			float2 @float = math.frac(P);
			float4 Pfx = @float.x + math.float4(-0.5f, -1.5f, -0.5f, -1.5f);
			float4 Pfy = @float.y + math.float4(-0.5f, -0.5f, -1.5f, -1.5f);
			float4 float2 = noise.permute(noise.permute(Pi.x + math.float4(0f, 1f, 0f, 1f)) + Pi.y + math.float4(0f, 0f, 1f, 1f));
			float4 ox = noise.mod7(float2) * 0.14285715f + 0.071428575f;
			float4 oy = noise.mod7(math.floor(float2 * 0.14285715f)) * 0.14285715f + 0.071428575f;
			float4 float3 = Pfx + 0.8f * ox;
			float4 dy = Pfy + 0.8f * oy;
			float4 d = float3 * float3 + dy * dy;
			d.xy = ((d.x < d.y) ? d.xy : d.yx);
			d.xz = ((d.x < d.z) ? d.xz : d.zx);
			d.xw = ((d.x < d.w) ? d.xw : d.wx);
			d.y = math.min(d.y, d.z);
			d.y = math.min(d.y, d.w);
			return math.sqrt(d.xy);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002614 File Offset: 0x00000814
		public static float2 cellular2x2x2(float3 P)
		{
			float3 Pi = noise.mod289(math.floor(P));
			float3 Pf = math.frac(P);
			float4 Pfx = Pf.x + math.float4(0f, -1f, 0f, -1f);
			float4 Pfy = Pf.y + math.float4(0f, 0f, -1f, -1f);
			float4 @float = noise.permute(noise.permute(Pi.x + math.float4(0f, 1f, 0f, 1f)) + Pi.y + math.float4(0f, 0f, 1f, 1f));
			float4 p = noise.permute(@float + Pi.z);
			float4 float2 = noise.permute(@float + Pi.z + math.float4(1f, 1f, 1f, 1f));
			float4 ox = math.frac(p * 0.14285715f) - 0.42857143f;
			float4 oy = noise.mod7(math.floor(p * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float4 oz = math.floor(p * 0.020408163f) * 0.16666667f - 0.41666666f;
			float4 ox2 = math.frac(float2 * 0.14285715f) - 0.42857143f;
			float4 oy2 = noise.mod7(math.floor(float2 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float4 oz2 = math.floor(float2 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float4 dx = Pfx + 0.8f * ox;
			float4 dy = Pfy + 0.8f * oy;
			float4 dz = Pf.z + 0.8f * oz;
			float4 dx2 = Pfx + 0.8f * ox2;
			float4 dy2 = Pfy + 0.8f * oy2;
			float4 dz2 = Pf.z - 1f + 0.8f * oz2;
			float4 float3 = dx * dx + dy * dy + dz * dz;
			float4 d2 = dx2 * dx2 + dy2 * dy2 + dz2 * dz2;
			float4 d3 = math.min(float3, d2);
			d2 = math.max(float3, d2);
			d3.xy = ((d3.x < d3.y) ? d3.xy : d3.yx);
			d3.xz = ((d3.x < d3.z) ? d3.xz : d3.zx);
			d3.xw = ((d3.x < d3.w) ? d3.xw : d3.wx);
			d3.yzw = math.min(d3.yzw, d2.yzw);
			d3.y = math.min(d3.y, d3.z);
			d3.y = math.min(d3.y, d3.w);
			d3.y = math.min(d3.y, d2.x);
			return math.sqrt(d3.xy);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000029C4 File Offset: 0x00000BC4
		public static float2 cellular(float3 P)
		{
			float3 Pi = noise.mod289(math.floor(P));
			float3 @float = math.frac(P) - 0.5f;
			float3 Pfx = @float.x + math.float3(1f, 0f, -1f);
			float3 Pfy = @float.y + math.float3(1f, 0f, -1f);
			float3 Pfz = @float.z + math.float3(1f, 0f, -1f);
			float3 float2 = noise.permute(Pi.x + math.float3(-1f, 0f, 1f));
			float3 p = noise.permute(float2 + Pi.y - 1f);
			float3 p2 = noise.permute(float2 + Pi.y);
			float3 float3 = noise.permute(float2 + Pi.y + 1f);
			float3 p3 = noise.permute(p + Pi.z - 1f);
			float3 p4 = noise.permute(p + Pi.z);
			float3 p5 = noise.permute(p + Pi.z + 1f);
			float3 p6 = noise.permute(p2 + Pi.z - 1f);
			float3 p7 = noise.permute(p2 + Pi.z);
			float3 p8 = noise.permute(p2 + Pi.z + 1f);
			float3 p9 = noise.permute(float3 + Pi.z - 1f);
			float3 p10 = noise.permute(float3 + Pi.z);
			float3 float4 = noise.permute(float3 + Pi.z + 1f);
			float3 ox11 = math.frac(p3 * 0.14285715f) - 0.42857143f;
			float3 oy11 = noise.mod7(math.floor(p3 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 oz11 = math.floor(p3 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 ox12 = math.frac(p4 * 0.14285715f) - 0.42857143f;
			float3 oy12 = noise.mod7(math.floor(p4 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 oz12 = math.floor(p4 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 ox13 = math.frac(p5 * 0.14285715f) - 0.42857143f;
			float3 oy13 = noise.mod7(math.floor(p5 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 oz13 = math.floor(p5 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 ox14 = math.frac(p6 * 0.14285715f) - 0.42857143f;
			float3 oy14 = noise.mod7(math.floor(p6 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 oz14 = math.floor(p6 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 ox15 = math.frac(p7 * 0.14285715f) - 0.42857143f;
			float3 oy15 = noise.mod7(math.floor(p7 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 oz15 = math.floor(p7 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 ox16 = math.frac(p8 * 0.14285715f) - 0.42857143f;
			float3 oy16 = noise.mod7(math.floor(p8 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 oz16 = math.floor(p8 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 ox17 = math.frac(p9 * 0.14285715f) - 0.42857143f;
			float3 oy17 = noise.mod7(math.floor(p9 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 oz17 = math.floor(p9 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 ox18 = math.frac(p10 * 0.14285715f) - 0.42857143f;
			float3 oy18 = noise.mod7(math.floor(p10 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 oz18 = math.floor(p10 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 ox19 = math.frac(float4 * 0.14285715f) - 0.42857143f;
			float3 oy19 = noise.mod7(math.floor(float4 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 oz19 = math.floor(float4 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 dx11 = Pfx + 1f * ox11;
			float3 dy11 = Pfy.x + 1f * oy11;
			float3 dz11 = Pfz.x + 1f * oz11;
			float3 dx12 = Pfx + 1f * ox12;
			float3 dy12 = Pfy.x + 1f * oy12;
			float3 dz12 = Pfz.y + 1f * oz12;
			float3 dx13 = Pfx + 1f * ox13;
			float3 dy13 = Pfy.x + 1f * oy13;
			float3 dz13 = Pfz.z + 1f * oz13;
			float3 dx14 = Pfx + 1f * ox14;
			float3 dy14 = Pfy.y + 1f * oy14;
			float3 dz14 = Pfz.x + 1f * oz14;
			float3 dx15 = Pfx + 1f * ox15;
			float3 dy15 = Pfy.y + 1f * oy15;
			float3 dz15 = Pfz.y + 1f * oz15;
			float3 dx16 = Pfx + 1f * ox16;
			float3 dy16 = Pfy.y + 1f * oy16;
			float3 dz16 = Pfz.z + 1f * oz16;
			float3 dx17 = Pfx + 1f * ox17;
			float3 dy17 = Pfy.z + 1f * oy17;
			float3 dz17 = Pfz.x + 1f * oz17;
			float3 dx18 = Pfx + 1f * ox18;
			float3 dy18 = Pfy.z + 1f * oy18;
			float3 dz18 = Pfz.y + 1f * oz18;
			float3 float5 = Pfx + 1f * ox19;
			float3 dy19 = Pfy.z + 1f * oy19;
			float3 dz19 = Pfz.z + 1f * oz19;
			float3 d11 = dx11 * dx11 + dy11 * dy11 + dz11 * dz11;
			float3 d12 = dx12 * dx12 + dy12 * dy12 + dz12 * dz12;
			float3 d13 = dx13 * dx13 + dy13 * dy13 + dz13 * dz13;
			float3 d14 = dx14 * dx14 + dy14 * dy14 + dz14 * dz14;
			float3 d15 = dx15 * dx15 + dy15 * dy15 + dz15 * dz15;
			float3 d16 = dx16 * dx16 + dy16 * dy16 + dz16 * dz16;
			float3 d17 = dx17 * dx17 + dy17 * dy17 + dz17 * dz17;
			float3 d18 = dx18 * dx18 + dy18 * dy18 + dz18 * dz18;
			float3 d19 = float5 * float5 + dy19 * dy19 + dz19 * dz19;
			float3 float6 = math.min(d11, d12);
			d12 = math.max(d11, d12);
			d11 = math.min(float6, d13);
			d13 = math.max(float6, d13);
			d12 = math.min(d12, d13);
			float3 float7 = math.min(d14, d15);
			d15 = math.max(d14, d15);
			d14 = math.min(float7, d16);
			d16 = math.max(float7, d16);
			d15 = math.min(d15, d16);
			float3 float8 = math.min(d17, d18);
			d18 = math.max(d17, d18);
			d17 = math.min(float8, d19);
			d19 = math.max(float8, d19);
			d18 = math.min(d18, d19);
			float3 float9 = math.min(d11, d14);
			d14 = math.max(d11, d14);
			d11 = math.min(float9, d17);
			d17 = math.max(float9, d17);
			d11.xy = ((d11.x < d11.y) ? d11.xy : d11.yx);
			d11.xz = ((d11.x < d11.z) ? d11.xz : d11.zx);
			d12 = math.min(d12, d14);
			d12 = math.min(d12, d15);
			d12 = math.min(d12, d17);
			d12 = math.min(d12, d18);
			d11.yz = math.min(d11.yz, d12.xy);
			d11.y = math.min(d11.y, d12.z);
			d11.y = math.min(d11.y, d11.z);
			return math.sqrt(d11.xy);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000034F4 File Offset: 0x000016F4
		public static float cnoise(float2 P)
		{
			float4 Pi = math.floor(P.xyxy) + math.float4(0f, 0f, 1f, 1f);
			float4 Pf = math.frac(P.xyxy) - math.float4(0f, 0f, 1f, 1f);
			Pi = noise.mod289(Pi);
			float4 xzxz = Pi.xzxz;
			float4 iy = Pi.yyww;
			float4 fx = Pf.xzxz;
			float4 fy = Pf.yyww;
			float4 @float = math.frac(noise.permute(noise.permute(xzxz) + iy) * 0.024390243f) * 2f - 1f;
			float4 gy = math.abs(@float) - 0.5f;
			float4 tx = math.floor(@float + 0.5f);
			float4 float2 = @float - tx;
			float2 g0 = math.float2(float2.x, gy.x);
			float2 g = math.float2(float2.y, gy.y);
			float2 g2 = math.float2(float2.z, gy.z);
			float2 g3 = math.float2(float2.w, gy.w);
			float4 norm = noise.taylorInvSqrt(math.float4(math.dot(g0, g0), math.dot(g2, g2), math.dot(g, g), math.dot(g3, g3)));
			g0 *= norm.x;
			g2 *= norm.y;
			g *= norm.z;
			g3 *= norm.w;
			float num = math.dot(g0, math.float2(fx.x, fy.x));
			float n10 = math.dot(g, math.float2(fx.y, fy.y));
			float n11 = math.dot(g2, math.float2(fx.z, fy.z));
			float n12 = math.dot(g3, math.float2(fx.w, fy.w));
			float2 fade_xy = noise.fade(Pf.xy);
			float2 n_x = math.lerp(math.float2(num, n11), math.float2(n10, n12), fade_xy.x);
			float n_xy = math.lerp(n_x.x, n_x.y, fade_xy.y);
			return 2.3f * n_xy;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00003758 File Offset: 0x00001958
		public static float pnoise(float2 P, float2 rep)
		{
			float4 Pi = math.floor(P.xyxy) + math.float4(0f, 0f, 1f, 1f);
			float4 Pf = math.frac(P.xyxy) - math.float4(0f, 0f, 1f, 1f);
			Pi = math.fmod(Pi, rep.xyxy);
			Pi = noise.mod289(Pi);
			float4 xzxz = Pi.xzxz;
			float4 iy = Pi.yyww;
			float4 fx = Pf.xzxz;
			float4 fy = Pf.yyww;
			float4 @float = math.frac(noise.permute(noise.permute(xzxz) + iy) * 0.024390243f) * 2f - 1f;
			float4 gy = math.abs(@float) - 0.5f;
			float4 tx = math.floor(@float + 0.5f);
			float4 float2 = @float - tx;
			float2 g0 = math.float2(float2.x, gy.x);
			float2 g = math.float2(float2.y, gy.y);
			float2 g2 = math.float2(float2.z, gy.z);
			float2 g3 = math.float2(float2.w, gy.w);
			float4 norm = noise.taylorInvSqrt(math.float4(math.dot(g0, g0), math.dot(g2, g2), math.dot(g, g), math.dot(g3, g3)));
			g0 *= norm.x;
			g2 *= norm.y;
			g *= norm.z;
			g3 *= norm.w;
			float num = math.dot(g0, math.float2(fx.x, fy.x));
			float n10 = math.dot(g, math.float2(fx.y, fy.y));
			float n11 = math.dot(g2, math.float2(fx.z, fy.z));
			float n12 = math.dot(g3, math.float2(fx.w, fy.w));
			float2 fade_xy = noise.fade(Pf.xy);
			float2 n_x = math.lerp(math.float2(num, n11), math.float2(n10, n12), fade_xy.x);
			float n_xy = math.lerp(n_x.x, n_x.y, fade_xy.y);
			return 2.3f * n_xy;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000039C8 File Offset: 0x00001BC8
		public static float cnoise(float3 P)
		{
			float3 Pi0 = math.floor(P);
			float3 Pi = Pi0 + math.float3(1f);
			Pi0 = noise.mod289(Pi0);
			Pi = noise.mod289(Pi);
			float3 Pf0 = math.frac(P);
			float3 Pf = Pf0 - math.float3(1f);
			float4 @float = math.float4(Pi0.x, Pi.x, Pi0.x, Pi.x);
			float4 iy = math.float4(Pi0.yy, Pi.yy);
			float4 iz0 = Pi0.zzzz;
			float4 iz = Pi.zzzz;
			float4 float2 = noise.permute(noise.permute(@float) + iy);
			float4 ixy0 = noise.permute(float2 + iz0);
			float4 float3 = noise.permute(float2 + iz);
			float4 gx0 = ixy0 * 0.14285715f;
			float4 gy0 = math.frac(math.floor(gx0) * 0.14285715f) - 0.5f;
			gx0 = math.frac(gx0);
			float4 gz0 = math.float4(0.5f) - math.abs(gx0) - math.abs(gy0);
			float4 sz0 = math.step(gz0, math.float4(0f));
			gx0 -= sz0 * (math.step(0f, gx0) - 0.5f);
			gy0 -= sz0 * (math.step(0f, gy0) - 0.5f);
			float4 gx = float3 * 0.14285715f;
			float4 gy = math.frac(math.floor(gx) * 0.14285715f) - 0.5f;
			gx = math.frac(gx);
			float4 gz = math.float4(0.5f) - math.abs(gx) - math.abs(gy);
			float4 sz = math.step(gz, math.float4(0f));
			gx -= sz * (math.step(0f, gx) - 0.5f);
			gy -= sz * (math.step(0f, gy) - 0.5f);
			float3 g0 = math.float3(gx0.x, gy0.x, gz0.x);
			float3 g = math.float3(gx0.y, gy0.y, gz0.y);
			float3 g2 = math.float3(gx0.z, gy0.z, gz0.z);
			float3 g3 = math.float3(gx0.w, gy0.w, gz0.w);
			float3 g4 = math.float3(gx.x, gy.x, gz.x);
			float3 g5 = math.float3(gx.y, gy.y, gz.y);
			float3 g6 = math.float3(gx.z, gy.z, gz.z);
			float3 g7 = math.float3(gx.w, gy.w, gz.w);
			float4 norm0 = noise.taylorInvSqrt(math.float4(math.dot(g0, g0), math.dot(g2, g2), math.dot(g, g), math.dot(g3, g3)));
			g0 *= norm0.x;
			g2 *= norm0.y;
			g *= norm0.z;
			g3 *= norm0.w;
			float4 norm = noise.taylorInvSqrt(math.float4(math.dot(g4, g4), math.dot(g6, g6), math.dot(g5, g5), math.dot(g7, g7)));
			g4 *= norm.x;
			g6 *= norm.y;
			g5 *= norm.z;
			g7 *= norm.w;
			float n0 = math.dot(g0, Pf0);
			float n = math.dot(g, math.float3(Pf.x, Pf0.yz));
			float n2 = math.dot(g2, math.float3(Pf0.x, Pf.y, Pf0.z));
			float n3 = math.dot(g3, math.float3(Pf.xy, Pf0.z));
			float n4 = math.dot(g4, math.float3(Pf0.xy, Pf.z));
			float n5 = math.dot(g5, math.float3(Pf.x, Pf0.y, Pf.z));
			float n6 = math.dot(g6, math.float3(Pf0.x, Pf.yz));
			float n7 = math.dot(g7, Pf);
			float3 fade_xyz = noise.fade(Pf0);
			float4 n_z = math.lerp(math.float4(n0, n, n2, n3), math.float4(n4, n5, n6, n7), fade_xyz.z);
			float2 n_yz = math.lerp(n_z.xy, n_z.zw, fade_xyz.y);
			float n_xyz = math.lerp(n_yz.x, n_yz.y, fade_xyz.x);
			return 2.2f * n_xyz;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00003F00 File Offset: 0x00002100
		public static float pnoise(float3 P, float3 rep)
		{
			float3 Pi0 = math.fmod(math.floor(P), rep);
			float3 Pi = math.fmod(Pi0 + math.float3(1f), rep);
			Pi0 = noise.mod289(Pi0);
			Pi = noise.mod289(Pi);
			float3 Pf0 = math.frac(P);
			float3 Pf = Pf0 - math.float3(1f);
			float4 @float = math.float4(Pi0.x, Pi.x, Pi0.x, Pi.x);
			float4 iy = math.float4(Pi0.yy, Pi.yy);
			float4 iz0 = Pi0.zzzz;
			float4 iz = Pi.zzzz;
			float4 float2 = noise.permute(noise.permute(@float) + iy);
			float4 ixy0 = noise.permute(float2 + iz0);
			float4 float3 = noise.permute(float2 + iz);
			float4 gx0 = ixy0 * 0.14285715f;
			float4 gy0 = math.frac(math.floor(gx0) * 0.14285715f) - 0.5f;
			gx0 = math.frac(gx0);
			float4 gz0 = math.float4(0.5f) - math.abs(gx0) - math.abs(gy0);
			float4 sz0 = math.step(gz0, math.float4(0f));
			gx0 -= sz0 * (math.step(0f, gx0) - 0.5f);
			gy0 -= sz0 * (math.step(0f, gy0) - 0.5f);
			float4 gx = float3 * 0.14285715f;
			float4 gy = math.frac(math.floor(gx) * 0.14285715f) - 0.5f;
			gx = math.frac(gx);
			float4 gz = math.float4(0.5f) - math.abs(gx) - math.abs(gy);
			float4 sz = math.step(gz, math.float4(0f));
			gx -= sz * (math.step(0f, gx) - 0.5f);
			gy -= sz * (math.step(0f, gy) - 0.5f);
			float3 g0 = math.float3(gx0.x, gy0.x, gz0.x);
			float3 g = math.float3(gx0.y, gy0.y, gz0.y);
			float3 g2 = math.float3(gx0.z, gy0.z, gz0.z);
			float3 g3 = math.float3(gx0.w, gy0.w, gz0.w);
			float3 g4 = math.float3(gx.x, gy.x, gz.x);
			float3 g5 = math.float3(gx.y, gy.y, gz.y);
			float3 g6 = math.float3(gx.z, gy.z, gz.z);
			float3 g7 = math.float3(gx.w, gy.w, gz.w);
			float4 norm0 = noise.taylorInvSqrt(math.float4(math.dot(g0, g0), math.dot(g2, g2), math.dot(g, g), math.dot(g3, g3)));
			g0 *= norm0.x;
			g2 *= norm0.y;
			g *= norm0.z;
			g3 *= norm0.w;
			float4 norm = noise.taylorInvSqrt(math.float4(math.dot(g4, g4), math.dot(g6, g6), math.dot(g5, g5), math.dot(g7, g7)));
			g4 *= norm.x;
			g6 *= norm.y;
			g5 *= norm.z;
			g7 *= norm.w;
			float n0 = math.dot(g0, Pf0);
			float n = math.dot(g, math.float3(Pf.x, Pf0.yz));
			float n2 = math.dot(g2, math.float3(Pf0.x, Pf.y, Pf0.z));
			float n3 = math.dot(g3, math.float3(Pf.xy, Pf0.z));
			float n4 = math.dot(g4, math.float3(Pf0.xy, Pf.z));
			float n5 = math.dot(g5, math.float3(Pf.x, Pf0.y, Pf.z));
			float n6 = math.dot(g6, math.float3(Pf0.x, Pf.yz));
			float n7 = math.dot(g7, Pf);
			float3 fade_xyz = noise.fade(Pf0);
			float4 n_z = math.lerp(math.float4(n0, n, n2, n3), math.float4(n4, n5, n6, n7), fade_xyz.z);
			float2 n_yz = math.lerp(n_z.xy, n_z.zw, fade_xyz.y);
			float n_xyz = math.lerp(n_yz.x, n_yz.y, fade_xyz.x);
			return 2.2f * n_xyz;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00004444 File Offset: 0x00002644
		public static float cnoise(float4 P)
		{
			float4 Pi0 = math.floor(P);
			float4 Pi = Pi0 + 1f;
			Pi0 = noise.mod289(Pi0);
			Pi = noise.mod289(Pi);
			float4 Pf0 = math.frac(P);
			float4 Pf = Pf0 - 1f;
			float4 @float = math.float4(Pi0.x, Pi.x, Pi0.x, Pi.x);
			float4 iy = math.float4(Pi0.yy, Pi.yy);
			float4 iz0 = math.float4(Pi0.zzzz);
			float4 iz = math.float4(Pi.zzzz);
			float4 iw0 = math.float4(Pi0.wwww);
			float4 iw = math.float4(Pi.wwww);
			float4 float2 = noise.permute(noise.permute(@float) + iy);
			float4 ixy0 = noise.permute(float2 + iz0);
			float4 float3 = noise.permute(float2 + iz);
			float4 ixy = noise.permute(ixy0 + iw0);
			float4 ixy2 = noise.permute(ixy0 + iw);
			float4 ixy3 = noise.permute(float3 + iw0);
			float4 float4 = noise.permute(float3 + iw);
			float4 gx0 = ixy * 0.14285715f;
			float4 gy0 = math.floor(gx0) * 0.14285715f;
			float4 gz0 = math.floor(gy0) * 0.16666667f;
			gx0 = math.frac(gx0) - 0.5f;
			gy0 = math.frac(gy0) - 0.5f;
			gz0 = math.frac(gz0) - 0.5f;
			float4 gw0 = math.float4(0.75f) - math.abs(gx0) - math.abs(gy0) - math.abs(gz0);
			float4 sw0 = math.step(gw0, math.float4(0f));
			gx0 -= sw0 * (math.step(0f, gx0) - 0.5f);
			gy0 -= sw0 * (math.step(0f, gy0) - 0.5f);
			float4 gx = ixy2 * 0.14285715f;
			float4 gy = math.floor(gx) * 0.14285715f;
			float4 gz = math.floor(gy) * 0.16666667f;
			gx = math.frac(gx) - 0.5f;
			gy = math.frac(gy) - 0.5f;
			gz = math.frac(gz) - 0.5f;
			float4 gw = math.float4(0.75f) - math.abs(gx) - math.abs(gy) - math.abs(gz);
			float4 sw = math.step(gw, math.float4(0f));
			gx -= sw * (math.step(0f, gx) - 0.5f);
			gy -= sw * (math.step(0f, gy) - 0.5f);
			float4 gx2 = ixy3 * 0.14285715f;
			float4 gy2 = math.floor(gx2) * 0.14285715f;
			float4 gz2 = math.floor(gy2) * 0.16666667f;
			gx2 = math.frac(gx2) - 0.5f;
			gy2 = math.frac(gy2) - 0.5f;
			gz2 = math.frac(gz2) - 0.5f;
			float4 gw2 = math.float4(0.75f) - math.abs(gx2) - math.abs(gy2) - math.abs(gz2);
			float4 sw2 = math.step(gw2, math.float4(0f));
			gx2 -= sw2 * (math.step(0f, gx2) - 0.5f);
			gy2 -= sw2 * (math.step(0f, gy2) - 0.5f);
			float4 gx3 = float4 * 0.14285715f;
			float4 gy3 = math.floor(gx3) * 0.14285715f;
			float4 gz3 = math.floor(gy3) * 0.16666667f;
			gx3 = math.frac(gx3) - 0.5f;
			gy3 = math.frac(gy3) - 0.5f;
			gz3 = math.frac(gz3) - 0.5f;
			float4 gw3 = math.float4(0.75f) - math.abs(gx3) - math.abs(gy3) - math.abs(gz3);
			float4 sw3 = math.step(gw3, math.float4(0f));
			gx3 -= sw3 * (math.step(0f, gx3) - 0.5f);
			gy3 -= sw3 * (math.step(0f, gy3) - 0.5f);
			float4 g0 = math.float4(gx0.x, gy0.x, gz0.x, gw0.x);
			float4 g = math.float4(gx0.y, gy0.y, gz0.y, gw0.y);
			float4 g2 = math.float4(gx0.z, gy0.z, gz0.z, gw0.z);
			float4 g3 = math.float4(gx0.w, gy0.w, gz0.w, gw0.w);
			float4 g4 = math.float4(gx2.x, gy2.x, gz2.x, gw2.x);
			float4 g5 = math.float4(gx2.y, gy2.y, gz2.y, gw2.y);
			float4 g6 = math.float4(gx2.z, gy2.z, gz2.z, gw2.z);
			float4 g7 = math.float4(gx2.w, gy2.w, gz2.w, gw2.w);
			float4 float5 = math.float4(gx.x, gy.x, gz.x, gw.x);
			float4 g8 = math.float4(gx.y, gy.y, gz.y, gw.y);
			float4 g9 = math.float4(gx.z, gy.z, gz.z, gw.z);
			float4 g10 = math.float4(gx.w, gy.w, gz.w, gw.w);
			float4 g11 = math.float4(gx3.x, gy3.x, gz3.x, gw3.x);
			float4 g12 = math.float4(gx3.y, gy3.y, gz3.y, gw3.y);
			float4 g13 = math.float4(gx3.z, gy3.z, gz3.z, gw3.z);
			float4 g14 = math.float4(gx3.w, gy3.w, gz3.w, gw3.w);
			float4 norm0 = noise.taylorInvSqrt(math.float4(math.dot(g0, g0), math.dot(g2, g2), math.dot(g, g), math.dot(g3, g3)));
			g0 *= norm0.x;
			g2 *= norm0.y;
			g *= norm0.z;
			g3 *= norm0.w;
			float4 norm = noise.taylorInvSqrt(math.float4(math.dot(float5, float5), math.dot(g9, g9), math.dot(g8, g8), math.dot(g10, g10)));
			float4 float6 = float5 * norm.x;
			g9 *= norm.y;
			g8 *= norm.z;
			g10 *= norm.w;
			float4 norm2 = noise.taylorInvSqrt(math.float4(math.dot(g4, g4), math.dot(g6, g6), math.dot(g5, g5), math.dot(g7, g7)));
			g4 *= norm2.x;
			g6 *= norm2.y;
			g5 *= norm2.z;
			g7 *= norm2.w;
			float4 norm3 = noise.taylorInvSqrt(math.float4(math.dot(g11, g11), math.dot(g13, g13), math.dot(g12, g12), math.dot(g14, g14)));
			g11 *= norm3.x;
			g13 *= norm3.y;
			g12 *= norm3.z;
			g14 *= norm3.w;
			float n0 = math.dot(g0, Pf0);
			float n = math.dot(g, math.float4(Pf.x, Pf0.yzw));
			float n2 = math.dot(g2, math.float4(Pf0.x, Pf.y, Pf0.zw));
			float n3 = math.dot(g3, math.float4(Pf.xy, Pf0.zw));
			float n4 = math.dot(g4, math.float4(Pf0.xy, Pf.z, Pf0.w));
			float n5 = math.dot(g5, math.float4(Pf.x, Pf0.y, Pf.z, Pf0.w));
			float n6 = math.dot(g6, math.float4(Pf0.x, Pf.yz, Pf0.w));
			float n7 = math.dot(g7, math.float4(Pf.xyz, Pf0.w));
			float n8 = math.dot(float6, math.float4(Pf0.xyz, Pf.w));
			float n9 = math.dot(g8, math.float4(Pf.x, Pf0.yz, Pf.w));
			float n10 = math.dot(g9, math.float4(Pf0.x, Pf.y, Pf0.z, Pf.w));
			float n11 = math.dot(g10, math.float4(Pf.xy, Pf0.z, Pf.w));
			float n12 = math.dot(g11, math.float4(Pf0.xy, Pf.zw));
			float n13 = math.dot(g12, math.float4(Pf.x, Pf0.y, Pf.zw));
			float n14 = math.dot(g13, math.float4(Pf0.x, Pf.yzw));
			float n15 = math.dot(g14, Pf);
			float4 fade_xyzw = noise.fade(Pf0);
			float4 float7 = math.lerp(math.float4(n0, n, n2, n3), math.float4(n8, n9, n10, n11), fade_xyzw.w);
			float4 n_1w = math.lerp(math.float4(n4, n5, n6, n7), math.float4(n12, n13, n14, n15), fade_xyzw.w);
			float4 n_zw = math.lerp(float7, n_1w, fade_xyzw.z);
			float2 n_yzw = math.lerp(n_zw.xy, n_zw.zw, fade_xyzw.y);
			float n_xyzw = math.lerp(n_yzw.x, n_yzw.y, fade_xyzw.x);
			return 2.2f * n_xyzw;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00004FC4 File Offset: 0x000031C4
		public static float pnoise(float4 P, float4 rep)
		{
			float4 Pi0 = math.fmod(math.floor(P), rep);
			float4 Pi = math.fmod(Pi0 + 1f, rep);
			Pi0 = noise.mod289(Pi0);
			Pi = noise.mod289(Pi);
			float4 Pf0 = math.frac(P);
			float4 Pf = Pf0 - 1f;
			float4 @float = math.float4(Pi0.x, Pi.x, Pi0.x, Pi.x);
			float4 iy = math.float4(Pi0.yy, Pi.yy);
			float4 iz0 = math.float4(Pi0.zzzz);
			float4 iz = math.float4(Pi.zzzz);
			float4 iw0 = math.float4(Pi0.wwww);
			float4 iw = math.float4(Pi.wwww);
			float4 float2 = noise.permute(noise.permute(@float) + iy);
			float4 ixy0 = noise.permute(float2 + iz0);
			float4 float3 = noise.permute(float2 + iz);
			float4 ixy = noise.permute(ixy0 + iw0);
			float4 ixy2 = noise.permute(ixy0 + iw);
			float4 ixy3 = noise.permute(float3 + iw0);
			float4 float4 = noise.permute(float3 + iw);
			float4 gx0 = ixy * 0.14285715f;
			float4 gy0 = math.floor(gx0) * 0.14285715f;
			float4 gz0 = math.floor(gy0) * 0.16666667f;
			gx0 = math.frac(gx0) - 0.5f;
			gy0 = math.frac(gy0) - 0.5f;
			gz0 = math.frac(gz0) - 0.5f;
			float4 gw0 = math.float4(0.75f) - math.abs(gx0) - math.abs(gy0) - math.abs(gz0);
			float4 sw0 = math.step(gw0, math.float4(0f));
			gx0 -= sw0 * (math.step(0f, gx0) - 0.5f);
			gy0 -= sw0 * (math.step(0f, gy0) - 0.5f);
			float4 gx = ixy2 * 0.14285715f;
			float4 gy = math.floor(gx) * 0.14285715f;
			float4 gz = math.floor(gy) * 0.16666667f;
			gx = math.frac(gx) - 0.5f;
			gy = math.frac(gy) - 0.5f;
			gz = math.frac(gz) - 0.5f;
			float4 gw = math.float4(0.75f) - math.abs(gx) - math.abs(gy) - math.abs(gz);
			float4 sw = math.step(gw, math.float4(0f));
			gx -= sw * (math.step(0f, gx) - 0.5f);
			gy -= sw * (math.step(0f, gy) - 0.5f);
			float4 gx2 = ixy3 * 0.14285715f;
			float4 gy2 = math.floor(gx2) * 0.14285715f;
			float4 gz2 = math.floor(gy2) * 0.16666667f;
			gx2 = math.frac(gx2) - 0.5f;
			gy2 = math.frac(gy2) - 0.5f;
			gz2 = math.frac(gz2) - 0.5f;
			float4 gw2 = math.float4(0.75f) - math.abs(gx2) - math.abs(gy2) - math.abs(gz2);
			float4 sw2 = math.step(gw2, math.float4(0f));
			gx2 -= sw2 * (math.step(0f, gx2) - 0.5f);
			gy2 -= sw2 * (math.step(0f, gy2) - 0.5f);
			float4 gx3 = float4 * 0.14285715f;
			float4 gy3 = math.floor(gx3) * 0.14285715f;
			float4 gz3 = math.floor(gy3) * 0.16666667f;
			gx3 = math.frac(gx3) - 0.5f;
			gy3 = math.frac(gy3) - 0.5f;
			gz3 = math.frac(gz3) - 0.5f;
			float4 gw3 = math.float4(0.75f) - math.abs(gx3) - math.abs(gy3) - math.abs(gz3);
			float4 sw3 = math.step(gw3, math.float4(0f));
			gx3 -= sw3 * (math.step(0f, gx3) - 0.5f);
			gy3 -= sw3 * (math.step(0f, gy3) - 0.5f);
			float4 g0 = math.float4(gx0.x, gy0.x, gz0.x, gw0.x);
			float4 g = math.float4(gx0.y, gy0.y, gz0.y, gw0.y);
			float4 g2 = math.float4(gx0.z, gy0.z, gz0.z, gw0.z);
			float4 g3 = math.float4(gx0.w, gy0.w, gz0.w, gw0.w);
			float4 g4 = math.float4(gx2.x, gy2.x, gz2.x, gw2.x);
			float4 g5 = math.float4(gx2.y, gy2.y, gz2.y, gw2.y);
			float4 g6 = math.float4(gx2.z, gy2.z, gz2.z, gw2.z);
			float4 g7 = math.float4(gx2.w, gy2.w, gz2.w, gw2.w);
			float4 float5 = math.float4(gx.x, gy.x, gz.x, gw.x);
			float4 g8 = math.float4(gx.y, gy.y, gz.y, gw.y);
			float4 g9 = math.float4(gx.z, gy.z, gz.z, gw.z);
			float4 g10 = math.float4(gx.w, gy.w, gz.w, gw.w);
			float4 g11 = math.float4(gx3.x, gy3.x, gz3.x, gw3.x);
			float4 g12 = math.float4(gx3.y, gy3.y, gz3.y, gw3.y);
			float4 g13 = math.float4(gx3.z, gy3.z, gz3.z, gw3.z);
			float4 g14 = math.float4(gx3.w, gy3.w, gz3.w, gw3.w);
			float4 norm0 = noise.taylorInvSqrt(math.float4(math.dot(g0, g0), math.dot(g2, g2), math.dot(g, g), math.dot(g3, g3)));
			g0 *= norm0.x;
			g2 *= norm0.y;
			g *= norm0.z;
			g3 *= norm0.w;
			float4 norm = noise.taylorInvSqrt(math.float4(math.dot(float5, float5), math.dot(g9, g9), math.dot(g8, g8), math.dot(g10, g10)));
			float4 float6 = float5 * norm.x;
			g9 *= norm.y;
			g8 *= norm.z;
			g10 *= norm.w;
			float4 norm2 = noise.taylorInvSqrt(math.float4(math.dot(g4, g4), math.dot(g6, g6), math.dot(g5, g5), math.dot(g7, g7)));
			g4 *= norm2.x;
			g6 *= norm2.y;
			g5 *= norm2.z;
			g7 *= norm2.w;
			float4 norm3 = noise.taylorInvSqrt(math.float4(math.dot(g11, g11), math.dot(g13, g13), math.dot(g12, g12), math.dot(g14, g14)));
			g11 *= norm3.x;
			g13 *= norm3.y;
			g12 *= norm3.z;
			g14 *= norm3.w;
			float n0 = math.dot(g0, Pf0);
			float n = math.dot(g, math.float4(Pf.x, Pf0.yzw));
			float n2 = math.dot(g2, math.float4(Pf0.x, Pf.y, Pf0.zw));
			float n3 = math.dot(g3, math.float4(Pf.xy, Pf0.zw));
			float n4 = math.dot(g4, math.float4(Pf0.xy, Pf.z, Pf0.w));
			float n5 = math.dot(g5, math.float4(Pf.x, Pf0.y, Pf.z, Pf0.w));
			float n6 = math.dot(g6, math.float4(Pf0.x, Pf.yz, Pf0.w));
			float n7 = math.dot(g7, math.float4(Pf.xyz, Pf0.w));
			float n8 = math.dot(float6, math.float4(Pf0.xyz, Pf.w));
			float n9 = math.dot(g8, math.float4(Pf.x, Pf0.yz, Pf.w));
			float n10 = math.dot(g9, math.float4(Pf0.x, Pf.y, Pf0.z, Pf.w));
			float n11 = math.dot(g10, math.float4(Pf.xy, Pf0.z, Pf.w));
			float n12 = math.dot(g11, math.float4(Pf0.xy, Pf.zw));
			float n13 = math.dot(g12, math.float4(Pf.x, Pf0.y, Pf.zw));
			float n14 = math.dot(g13, math.float4(Pf0.x, Pf.yzw));
			float n15 = math.dot(g14, Pf);
			float4 fade_xyzw = noise.fade(Pf0);
			float4 float7 = math.lerp(math.float4(n0, n, n2, n3), math.float4(n8, n9, n10, n11), fade_xyzw.w);
			float4 n_1w = math.lerp(math.float4(n4, n5, n6, n7), math.float4(n12, n13, n14, n15), fade_xyzw.w);
			float4 n_zw = math.lerp(float7, n_1w, fade_xyzw.z);
			float2 n_yzw = math.lerp(n_zw.xy, n_zw.zw, fade_xyzw.y);
			float n_xyzw = math.lerp(n_yzw.x, n_yzw.y, fade_xyzw.x);
			return 2.2f * n_xyzw;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00005B4E File Offset: 0x00003D4E
		private static float mod289(float x)
		{
			return x - math.floor(x * 0.0034602077f) * 289f;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00005B64 File Offset: 0x00003D64
		private static float2 mod289(float2 x)
		{
			return x - math.floor(x * 0.0034602077f) * 289f;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00005B86 File Offset: 0x00003D86
		private static float3 mod289(float3 x)
		{
			return x - math.floor(x * 0.0034602077f) * 289f;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00005BA8 File Offset: 0x00003DA8
		private static float4 mod289(float4 x)
		{
			return x - math.floor(x * 0.0034602077f) * 289f;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00005BCA File Offset: 0x00003DCA
		private static float3 mod7(float3 x)
		{
			return x - math.floor(x * 0.14285715f) * 7f;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00005BEC File Offset: 0x00003DEC
		private static float4 mod7(float4 x)
		{
			return x - math.floor(x * 0.14285715f) * 7f;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00005C0E File Offset: 0x00003E0E
		private static float permute(float x)
		{
			return noise.mod289((34f * x + 1f) * x);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00005C24 File Offset: 0x00003E24
		private static float3 permute(float3 x)
		{
			return noise.mod289((34f * x + 1f) * x);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00005C46 File Offset: 0x00003E46
		private static float4 permute(float4 x)
		{
			return noise.mod289((34f * x + 1f) * x);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00005C68 File Offset: 0x00003E68
		private static float taylorInvSqrt(float r)
		{
			return 1.7928429f - 0.85373473f * r;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00005C77 File Offset: 0x00003E77
		private static float4 taylorInvSqrt(float4 r)
		{
			return 1.7928429f - 0.85373473f * r;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00005C8E File Offset: 0x00003E8E
		private static float2 fade(float2 t)
		{
			return t * t * t * (t * (t * 6f - 15f) + 10f);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00005CC7 File Offset: 0x00003EC7
		private static float3 fade(float3 t)
		{
			return t * t * t * (t * (t * 6f - 15f) + 10f);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00005D00 File Offset: 0x00003F00
		private static float4 fade(float4 t)
		{
			return t * t * t * (t * (t * 6f - 15f) + 10f);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00005D3C File Offset: 0x00003F3C
		private static float4 grad4(float j, float4 ip)
		{
			float4 ones = math.float4(1f, 1f, 1f, -1f);
			float3 pxyz = math.floor(math.frac(math.float3(j) * ip.xyz) * 7f) * ip.z - 1f;
			float pw = 1.5f - math.dot(math.abs(pxyz), ones.xyz);
			float4 p = math.float4(pxyz, pw);
			float4 s = math.float4(p < 0f);
			p.xyz += (s.xyz * 2f - 1f) * s.www;
			return p;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00005E10 File Offset: 0x00004010
		private static float2 rgrad2(float2 p, float rot)
		{
			float u = noise.permute(noise.permute(p.x) + p.y) * 0.024390243f + rot;
			u = math.frac(u) * 6.2831855f;
			return math.float2(math.cos(u), math.sin(u));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00005E5C File Offset: 0x0000405C
		public static float snoise(float2 v)
		{
			float4 C = math.float4(0.21132487f, 0.36602542f, -0.57735026f, 0.024390243f);
			float2 i = math.floor(v + math.dot(v, C.yy));
			float2 x0 = v - i + math.dot(i, C.xx);
			float2 i2 = ((x0.x > x0.y) ? math.float2(1f, 0f) : math.float2(0f, 1f));
			float4 x = x0.xyxy + C.xxzz;
			x.xy -= i2;
			i = noise.mod289(i);
			float3 p = noise.permute(noise.permute(i.y + math.float3(0f, i2.y, 1f)) + i.x + math.float3(0f, i2.x, 1f));
			float3 j = math.max(0.5f - math.float3(math.dot(x0, x0), math.dot(x.xy, x.xy), math.dot(x.zw, x.zw)), 0f);
			j *= j;
			j *= j;
			float3 @float = 2f * math.frac(p * C.www) - 1f;
			float3 h = math.abs(@float) - 0.5f;
			float3 ox = math.floor(@float + 0.5f);
			float3 a0 = @float - ox;
			j *= 1.7928429f - 0.85373473f * (a0 * a0 + h * h);
			float num = a0.x * x0.x + h.x * x0.y;
			float2 gyz = a0.yz * x.xz + h.yz * x.yw;
			float3 g = math.float3(num, gyz);
			return 130f * math.dot(j, g);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000060B4 File Offset: 0x000042B4
		public static float snoise(float3 v)
		{
			float2 C = math.float2(0.16666667f, 0.33333334f);
			float4 D = math.float4(0f, 0.5f, 1f, 2f);
			float3 i = math.floor(v + math.dot(v, C.yyy));
			float3 x0 = v - i + math.dot(i, C.xxx);
			float3 g = math.step(x0.yzx, x0.xyz);
			float3 j = 1f - g;
			float3 i2 = math.min(g.xyz, j.zxy);
			float3 i3 = math.max(g.xyz, j.zxy);
			float3 x = x0 - i2 + C.xxx;
			float3 x2 = x0 - i3 + C.yyy;
			float3 x3 = x0 - D.yyy;
			i = noise.mod289(i);
			float4 p = noise.permute(noise.permute(noise.permute(i.z + math.float4(0f, i2.z, i3.z, 1f)) + i.y + math.float4(0f, i2.y, i3.y, 1f)) + i.x + math.float4(0f, i2.x, i3.x, 1f));
			float3 ns = 0.14285715f * D.wyz - D.xzx;
			float4 @float = p - 49f * math.floor(p * ns.z * ns.z);
			float4 x_ = math.floor(@float * ns.z);
			float4 float2 = math.floor(@float - 7f * x_);
			float4 x4 = x_ * ns.x + ns.yyyy;
			float4 y = float2 * ns.x + ns.yyyy;
			float4 h = 1f - math.abs(x4) - math.abs(y);
			float4 b0 = math.float4(x4.xy, y.xy);
			float4 b = math.float4(x4.zw, y.zw);
			float4 s0 = math.floor(b0) * 2f + 1f;
			float4 s = math.floor(b) * 2f + 1f;
			float4 sh = -math.step(h, math.float4(0f));
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a = b.xzyw + s.xzyw * sh.zzww;
			float3 p2 = math.float3(a0.xy, h.x);
			float3 p3 = math.float3(a0.zw, h.y);
			float3 p4 = math.float3(a.xy, h.z);
			float3 p5 = math.float3(a.zw, h.w);
			float4 norm = noise.taylorInvSqrt(math.float4(math.dot(p2, p2), math.dot(p3, p3), math.dot(p4, p4), math.dot(p5, p5)));
			p2 *= norm.x;
			p3 *= norm.y;
			p4 *= norm.z;
			p5 *= norm.w;
			float4 k = math.max(0.6f - math.float4(math.dot(x0, x0), math.dot(x, x), math.dot(x2, x2), math.dot(x3, x3)), 0f);
			k *= k;
			return 42f * math.dot(k * k, math.float4(math.dot(p2, x0), math.dot(p3, x), math.dot(p4, x2), math.dot(p5, x3)));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000651C File Offset: 0x0000471C
		public static float snoise(float3 v, out float3 gradient)
		{
			float2 C = math.float2(0.16666667f, 0.33333334f);
			float4 D = math.float4(0f, 0.5f, 1f, 2f);
			float3 i = math.floor(v + math.dot(v, C.yyy));
			float3 x0 = v - i + math.dot(i, C.xxx);
			float3 g = math.step(x0.yzx, x0.xyz);
			float3 j = 1f - g;
			float3 i2 = math.min(g.xyz, j.zxy);
			float3 i3 = math.max(g.xyz, j.zxy);
			float3 x = x0 - i2 + C.xxx;
			float3 x2 = x0 - i3 + C.yyy;
			float3 x3 = x0 - D.yyy;
			i = noise.mod289(i);
			float4 p = noise.permute(noise.permute(noise.permute(i.z + math.float4(0f, i2.z, i3.z, 1f)) + i.y + math.float4(0f, i2.y, i3.y, 1f)) + i.x + math.float4(0f, i2.x, i3.x, 1f));
			float3 ns = 0.14285715f * D.wyz - D.xzx;
			float4 @float = p - 49f * math.floor(p * ns.z * ns.z);
			float4 x_ = math.floor(@float * ns.z);
			float4 float2 = math.floor(@float - 7f * x_);
			float4 x4 = x_ * ns.x + ns.yyyy;
			float4 y = float2 * ns.x + ns.yyyy;
			float4 h = 1f - math.abs(x4) - math.abs(y);
			float4 b0 = math.float4(x4.xy, y.xy);
			float4 b = math.float4(x4.zw, y.zw);
			float4 s0 = math.floor(b0) * 2f + 1f;
			float4 s = math.floor(b) * 2f + 1f;
			float4 sh = -math.step(h, math.float4(0f));
			float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
			float4 a = b.xzyw + s.xzyw * sh.zzww;
			float3 p2 = math.float3(a0.xy, h.x);
			float3 p3 = math.float3(a0.zw, h.y);
			float3 p4 = math.float3(a.xy, h.z);
			float3 p5 = math.float3(a.zw, h.w);
			float4 norm = noise.taylorInvSqrt(math.float4(math.dot(p2, p2), math.dot(p3, p3), math.dot(p4, p4), math.dot(p5, p5)));
			p2 *= norm.x;
			p3 *= norm.y;
			p4 *= norm.z;
			p5 *= norm.w;
			float4 k = math.max(0.6f - math.float4(math.dot(x0, x0), math.dot(x, x), math.dot(x2, x2), math.dot(x3, x3)), 0f);
			float4 float3 = k * k;
			float4 m4 = float3 * float3;
			float4 pdotx = math.float4(math.dot(p2, x0), math.dot(p3, x), math.dot(p4, x2), math.dot(p5, x3));
			float4 temp = float3 * k * pdotx;
			gradient = -8f * (temp.x * x0 + temp.y * x + temp.z * x2 + temp.w * x3);
			gradient += m4.x * p2 + m4.y * p3 + m4.z * p4 + m4.w * p5;
			gradient *= 42f;
			return 42f * math.dot(m4, pdotx);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00006A5C File Offset: 0x00004C5C
		public static float snoise(float4 v)
		{
			float4 C = math.float4(0.1381966f, 0.2763932f, 0.4145898f, -0.4472136f);
			float4 i = math.floor(v + math.dot(v, math.float4(0.309017f)));
			float4 x0 = v - i + math.dot(i, C.xxxx);
			float4 i2 = math.float4(0f);
			float3 isX = math.step(x0.yzw, x0.xxx);
			float3 isYZ = math.step(x0.zww, x0.yyz);
			i2.x = isX.x + isX.y + isX.z;
			i2.yzw = 1f - isX;
			i2.y += isYZ.x + isYZ.y;
			i2.zw += 1f - isYZ.xy;
			i2.z += isYZ.z;
			i2.w += 1f - isYZ.z;
			float4 i3 = math.clamp(i2, 0f, 1f);
			float4 i4 = math.clamp(i2 - 1f, 0f, 1f);
			float4 i5 = math.clamp(i2 - 2f, 0f, 1f);
			float4 x = x0 - i5 + C.xxxx;
			float4 x2 = x0 - i4 + C.yyyy;
			float4 x3 = x0 - i3 + C.zzzz;
			float4 x4 = x0 + C.wwww;
			i = noise.mod289(i);
			float j0 = noise.permute(noise.permute(noise.permute(noise.permute(i.w) + i.z) + i.y) + i.x);
			float4 @float = noise.permute(noise.permute(noise.permute(noise.permute(i.w + math.float4(i5.w, i4.w, i3.w, 1f)) + i.z + math.float4(i5.z, i4.z, i3.z, 1f)) + i.y + math.float4(i5.y, i4.y, i3.y, 1f)) + i.x + math.float4(i5.x, i4.x, i3.x, 1f));
			float4 ip = math.float4(0.0034013605f, 0.020408163f, 0.14285715f, 0f);
			float4 p0 = noise.grad4(j0, ip);
			float4 p = noise.grad4(@float.x, ip);
			float4 p2 = noise.grad4(@float.y, ip);
			float4 p3 = noise.grad4(@float.z, ip);
			float4 p4 = noise.grad4(@float.w, ip);
			float4 norm = noise.taylorInvSqrt(math.float4(math.dot(p0, p0), math.dot(p, p), math.dot(p2, p2), math.dot(p3, p3)));
			p0 *= norm.x;
			p *= norm.y;
			p2 *= norm.z;
			p3 *= norm.w;
			p4 *= noise.taylorInvSqrt(math.dot(p4, p4));
			float3 m0 = math.max(0.6f - math.float3(math.dot(x0, x0), math.dot(x, x), math.dot(x2, x2)), 0f);
			float2 m = math.max(0.6f - math.float2(math.dot(x3, x3), math.dot(x4, x4)), 0f);
			m0 *= m0;
			m *= m;
			return 49f * (math.dot(m0 * m0, math.float3(math.dot(p0, x0), math.dot(p, x), math.dot(p2, x2))) + math.dot(m * m, math.float2(math.dot(p3, x3), math.dot(p4, x4))));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00006F04 File Offset: 0x00005104
		public static float3 psrdnoise(float2 pos, float2 per, float rot)
		{
			pos.y += 0.01f;
			float2 @float = math.float2(pos.x + pos.y * 0.5f, pos.y);
			float2 i0 = math.floor(@float);
			float2 f0 = math.frac(@float);
			float2 i = ((f0.x > f0.y) ? math.float2(1f, 0f) : math.float2(0f, 1f));
			float2 p0 = math.float2(i0.x - i0.y * 0.5f, i0.y);
			float2 p = math.float2(p0.x + i.x - i.y * 0.5f, p0.y + i.y);
			float2 p2 = math.float2(p0.x + 0.5f, p0.y + 1f);
			float2 d0 = pos - p0;
			float2 d = pos - p;
			float2 d2 = pos - p2;
			float3 float2 = math.fmod(math.float3(p0.x, p.x, p2.x), per.x);
			float3 yw = math.fmod(math.float3(p0.y, p.y, p2.y), per.y);
			float3 float3 = float2 + 0.5f * yw;
			float3 ivw = yw;
			float2 g0 = noise.rgrad2(math.float2(float3.x, ivw.x), rot);
			float2 g = noise.rgrad2(math.float2(float3.y, ivw.y), rot);
			float2 g2 = noise.rgrad2(math.float2(float3.z, ivw.z), rot);
			float3 w = math.float3(math.dot(g0, d0), math.dot(g, d), math.dot(g2, d2));
			float3 t = 0.8f - math.float3(math.dot(d0, d0), math.dot(d, d), math.dot(d2, d2));
			float3 dtdx = -2f * math.float3(d0.x, d.x, d2.x);
			float3 dtdy = -2f * math.float3(d0.y, d.y, d2.y);
			if (t.x < 0f)
			{
				dtdx.x = 0f;
				dtdy.x = 0f;
				t.x = 0f;
			}
			if (t.y < 0f)
			{
				dtdx.y = 0f;
				dtdy.y = 0f;
				t.y = 0f;
			}
			if (t.z < 0f)
			{
				dtdx.z = 0f;
				dtdy.z = 0f;
				t.z = 0f;
			}
			float3 float4 = t * t;
			float3 t2 = float4 * float4;
			float3 t3 = float4 * t;
			float j = math.dot(t2, w);
			float2 dt0 = math.float2(dtdx.x, dtdy.x) * 4f * t3.x;
			float2 dn0 = t2.x * g0 + dt0 * w.x;
			float2 dt = math.float2(dtdx.y, dtdy.y) * 4f * t3.y;
			float2 dn = t2.y * g + dt * w.y;
			float2 dt2 = math.float2(dtdx.z, dtdy.z) * 4f * t3.z;
			float2 dn2 = t2.z * g2 + dt2 * w.z;
			return 11f * math.float3(j, dn0 + dn + dn2);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000731B File Offset: 0x0000551B
		public static float3 psrdnoise(float2 pos, float2 per)
		{
			return noise.psrdnoise(pos, per, 0f);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000732C File Offset: 0x0000552C
		public static float psrnoise(float2 pos, float2 per, float rot)
		{
			pos.y += 0.001f;
			float2 @float = math.float2(pos.x + pos.y * 0.5f, pos.y);
			float2 i0 = math.floor(@float);
			float2 f0 = math.frac(@float);
			float2 i = ((f0.x > f0.y) ? math.float2(1f, 0f) : math.float2(0f, 1f));
			float2 p0 = math.float2(i0.x - i0.y * 0.5f, i0.y);
			float2 p = math.float2(p0.x + i.x - i.y * 0.5f, p0.y + i.y);
			float2 p2 = math.float2(p0.x + 0.5f, p0.y + 1f);
			float2 d0 = pos - p0;
			float2 d = pos - p;
			float2 d2 = pos - p2;
			float3 float2 = math.fmod(math.float3(p0.x, p.x, p2.x), per.x);
			float3 yw = math.fmod(math.float3(p0.y, p.y, p2.y), per.y);
			float3 float3 = float2 + 0.5f * yw;
			float3 ivw = yw;
			float2 g0 = noise.rgrad2(math.float2(float3.x, ivw.x), rot);
			float2 g = noise.rgrad2(math.float2(float3.y, ivw.y), rot);
			float2 g2 = noise.rgrad2(math.float2(float3.z, ivw.z), rot);
			float3 w = math.float3(math.dot(g0, d0), math.dot(g, d), math.dot(g2, d2));
			float3 float4 = math.max(0.8f - math.float3(math.dot(d0, d0), math.dot(d, d), math.dot(d2, d2)), 0f);
			float3 float5 = float4 * float4;
			float j = math.dot(float5 * float5, w);
			return 11f * j;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000755A File Offset: 0x0000575A
		public static float psrnoise(float2 pos, float2 per)
		{
			return noise.psrnoise(pos, per, 0f);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00007568 File Offset: 0x00005768
		public static float3 srdnoise(float2 pos, float rot)
		{
			pos.y += 0.001f;
			float2 @float = math.float2(pos.x + pos.y * 0.5f, pos.y);
			float2 i0 = math.floor(@float);
			float2 f0 = math.frac(@float);
			float2 i = ((f0.x > f0.y) ? math.float2(1f, 0f) : math.float2(0f, 1f));
			float2 p0 = math.float2(i0.x - i0.y * 0.5f, i0.y);
			float2 p = math.float2(p0.x + i.x - i.y * 0.5f, p0.y + i.y);
			float2 p2 = math.float2(p0.x + 0.5f, p0.y + 1f);
			float2 d0 = pos - p0;
			float2 d = pos - p;
			float2 d2 = pos - p2;
			float3 float2 = math.float3(p0.x, p.x, p2.x);
			float3 y = math.float3(p0.y, p.y, p2.y);
			float3 float3 = float2 + 0.5f * y;
			float3 ivw = y;
			float3 float4 = noise.mod289(float3);
			ivw = noise.mod289(ivw);
			float2 g0 = noise.rgrad2(math.float2(float4.x, ivw.x), rot);
			float2 g = noise.rgrad2(math.float2(float4.y, ivw.y), rot);
			float2 g2 = noise.rgrad2(math.float2(float4.z, ivw.z), rot);
			float3 w = math.float3(math.dot(g0, d0), math.dot(g, d), math.dot(g2, d2));
			float3 t = 0.8f - math.float3(math.dot(d0, d0), math.dot(d, d), math.dot(d2, d2));
			float3 dtdx = -2f * math.float3(d0.x, d.x, d2.x);
			float3 dtdy = -2f * math.float3(d0.y, d.y, d2.y);
			if (t.x < 0f)
			{
				dtdx.x = 0f;
				dtdy.x = 0f;
				t.x = 0f;
			}
			if (t.y < 0f)
			{
				dtdx.y = 0f;
				dtdy.y = 0f;
				t.y = 0f;
			}
			if (t.z < 0f)
			{
				dtdx.z = 0f;
				dtdy.z = 0f;
				t.z = 0f;
			}
			float3 float5 = t * t;
			float3 t2 = float5 * float5;
			float3 t3 = float5 * t;
			float j = math.dot(t2, w);
			float2 dt0 = math.float2(dtdx.x, dtdy.x) * 4f * t3.x;
			float2 dn0 = t2.x * g0 + dt0 * w.x;
			float2 dt = math.float2(dtdx.y, dtdy.y) * 4f * t3.y;
			float2 dn = t2.y * g + dt * w.y;
			float2 dt2 = math.float2(dtdx.z, dtdy.z) * 4f * t3.z;
			float2 dn2 = t2.z * g2 + dt2 * w.z;
			return 11f * math.float3(j, dn0 + dn + dn2);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000796D File Offset: 0x00005B6D
		public static float3 srdnoise(float2 pos)
		{
			return noise.srdnoise(pos, 0f);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000797C File Offset: 0x00005B7C
		public static float srnoise(float2 pos, float rot)
		{
			pos.y += 0.001f;
			float2 @float = math.float2(pos.x + pos.y * 0.5f, pos.y);
			float2 i0 = math.floor(@float);
			float2 f0 = math.frac(@float);
			float2 i = ((f0.x > f0.y) ? math.float2(1f, 0f) : math.float2(0f, 1f));
			float2 p0 = math.float2(i0.x - i0.y * 0.5f, i0.y);
			float2 p = math.float2(p0.x + i.x - i.y * 0.5f, p0.y + i.y);
			float2 p2 = math.float2(p0.x + 0.5f, p0.y + 1f);
			float2 d0 = pos - p0;
			float2 d = pos - p;
			float2 d2 = pos - p2;
			float3 float2 = math.float3(p0.x, p.x, p2.x);
			float3 y = math.float3(p0.y, p.y, p2.y);
			float3 float3 = float2 + 0.5f * y;
			float3 ivw = y;
			float3 float4 = noise.mod289(float3);
			ivw = noise.mod289(ivw);
			float2 g0 = noise.rgrad2(math.float2(float4.x, ivw.x), rot);
			float2 g = noise.rgrad2(math.float2(float4.y, ivw.y), rot);
			float2 g2 = noise.rgrad2(math.float2(float4.z, ivw.z), rot);
			float3 w = math.float3(math.dot(g0, d0), math.dot(g, d), math.dot(g2, d2));
			float3 float5 = math.max(0.8f - math.float3(math.dot(d0, d0), math.dot(d, d), math.dot(d2, d2)), 0f);
			float3 float6 = float5 * float5;
			float j = math.dot(float6 * float6, w);
			return 11f * j;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00007B98 File Offset: 0x00005D98
		public static float srnoise(float2 pos)
		{
			return noise.srnoise(pos, 0f);
		}
	}
}
