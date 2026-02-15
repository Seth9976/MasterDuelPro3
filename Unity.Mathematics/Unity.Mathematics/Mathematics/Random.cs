using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000051 RID: 81
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct Random
	{
		// Token: 0x06001E84 RID: 7812 RVA: 0x00057D75 File Offset: 0x00055F75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Random(uint seed)
		{
			this.state = seed;
			this.NextState();
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x00057D85 File Offset: 0x00055F85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Random CreateFromIndex(uint index)
		{
			return new Random(Random.WangHash(index + 62U));
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x00057D95 File Offset: 0x00055F95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint WangHash(uint n)
		{
			n = n ^ 61U ^ (n >> 16);
			n *= 9U;
			n ^= n >> 4;
			n *= 668265261U;
			n ^= n >> 15;
			return n;
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x00057D75 File Offset: 0x00055F75
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void InitState(uint seed = 1851936439U)
		{
			this.state = seed;
			this.NextState();
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x00057DC1 File Offset: 0x00055FC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool NextBool()
		{
			return (this.NextState() & 1U) == 1U;
		}

		// Token: 0x06001E89 RID: 7817 RVA: 0x00057DCE File Offset: 0x00055FCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool2 NextBool2()
		{
			return (math.uint2(this.NextState()) & math.uint2(1U, 2U)) == 0U;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x00057DED File Offset: 0x00055FED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool3 NextBool3()
		{
			return (math.uint3(this.NextState()) & math.uint3(1U, 2U, 4U)) == 0U;
		}

		// Token: 0x06001E8B RID: 7819 RVA: 0x00057E0D File Offset: 0x0005600D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool4 NextBool4()
		{
			return (math.uint4(this.NextState()) & math.uint4(1U, 2U, 4U, 8U)) == 0U;
		}

		// Token: 0x06001E8C RID: 7820 RVA: 0x00057E2E File Offset: 0x0005602E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int NextInt()
		{
			return (int)(this.NextState() ^ 2147483648U);
		}

		// Token: 0x06001E8D RID: 7821 RVA: 0x00057E3C File Offset: 0x0005603C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2 NextInt2()
		{
			return math.int2((int)this.NextState(), (int)this.NextState()) ^ int.MinValue;
		}

		// Token: 0x06001E8E RID: 7822 RVA: 0x00057E59 File Offset: 0x00056059
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3 NextInt3()
		{
			return math.int3((int)this.NextState(), (int)this.NextState(), (int)this.NextState()) ^ int.MinValue;
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x00057E7C File Offset: 0x0005607C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4 NextInt4()
		{
			return math.int4((int)this.NextState(), (int)this.NextState(), (int)this.NextState(), (int)this.NextState()) ^ int.MinValue;
		}

		// Token: 0x06001E90 RID: 7824 RVA: 0x00057EA5 File Offset: 0x000560A5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int NextInt(int max)
		{
			return (int)((ulong)this.NextState() * (ulong)((long)max) >> 32);
		}

		// Token: 0x06001E91 RID: 7825 RVA: 0x00057EB5 File Offset: 0x000560B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2 NextInt2(int2 max)
		{
			return math.int2((int)((ulong)this.NextState() * (ulong)((long)max.x) >> 32), (int)((ulong)this.NextState() * (ulong)((long)max.y) >> 32));
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x00057EE2 File Offset: 0x000560E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3 NextInt3(int3 max)
		{
			return math.int3((int)((ulong)this.NextState() * (ulong)((long)max.x) >> 32), (int)((ulong)this.NextState() * (ulong)((long)max.y) >> 32), (int)((ulong)this.NextState() * (ulong)((long)max.z) >> 32));
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x00057F24 File Offset: 0x00056124
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4 NextInt4(int4 max)
		{
			return math.int4((int)((ulong)this.NextState() * (ulong)((long)max.x) >> 32), (int)((ulong)this.NextState() * (ulong)((long)max.y) >> 32), (int)((ulong)this.NextState() * (ulong)((long)max.z) >> 32), (int)((ulong)this.NextState() * (ulong)((long)max.w) >> 32));
		}

		// Token: 0x06001E94 RID: 7828 RVA: 0x00057F84 File Offset: 0x00056184
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int NextInt(int min, int max)
		{
			uint range = (uint)(max - min);
			return (int)((ulong)this.NextState() * (ulong)range >> 32) + min;
		}

		// Token: 0x06001E95 RID: 7829 RVA: 0x00057FA8 File Offset: 0x000561A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int2 NextInt2(int2 min, int2 max)
		{
			uint2 range = (uint2)(max - min);
			return math.int2((int)((ulong)this.NextState() * (ulong)range.x >> 32), (int)((ulong)this.NextState() * (ulong)range.y >> 32)) + min;
		}

		// Token: 0x06001E96 RID: 7830 RVA: 0x00057FF4 File Offset: 0x000561F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int3 NextInt3(int3 min, int3 max)
		{
			uint3 range = (uint3)(max - min);
			return math.int3((int)((ulong)this.NextState() * (ulong)range.x >> 32), (int)((ulong)this.NextState() * (ulong)range.y >> 32), (int)((ulong)this.NextState() * (ulong)range.z >> 32)) + min;
		}

		// Token: 0x06001E97 RID: 7831 RVA: 0x00058054 File Offset: 0x00056254
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int4 NextInt4(int4 min, int4 max)
		{
			uint4 range = (uint4)(max - min);
			return math.int4((int)((ulong)this.NextState() * (ulong)range.x >> 32), (int)((ulong)this.NextState() * (ulong)range.y >> 32), (int)((ulong)this.NextState() * (ulong)range.z >> 32), (int)((ulong)this.NextState() * (ulong)range.w >> 32)) + min;
		}

		// Token: 0x06001E98 RID: 7832 RVA: 0x000580C5 File Offset: 0x000562C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint NextUInt()
		{
			return this.NextState() - 1U;
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x000580CF File Offset: 0x000562CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2 NextUInt2()
		{
			return math.uint2(this.NextState(), this.NextState()) - 1U;
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x000580E8 File Offset: 0x000562E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3 NextUInt3()
		{
			return math.uint3(this.NextState(), this.NextState(), this.NextState()) - 1U;
		}

		// Token: 0x06001E9B RID: 7835 RVA: 0x00058107 File Offset: 0x00056307
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4 NextUInt4()
		{
			return math.uint4(this.NextState(), this.NextState(), this.NextState(), this.NextState()) - 1U;
		}

		// Token: 0x06001E9C RID: 7836 RVA: 0x0005812C File Offset: 0x0005632C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint NextUInt(uint max)
		{
			return (uint)((ulong)this.NextState() * (ulong)max >> 32);
		}

		// Token: 0x06001E9D RID: 7837 RVA: 0x0005813C File Offset: 0x0005633C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2 NextUInt2(uint2 max)
		{
			return math.uint2((uint)((ulong)this.NextState() * (ulong)max.x >> 32), (uint)((ulong)this.NextState() * (ulong)max.y >> 32));
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00058169 File Offset: 0x00056369
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3 NextUInt3(uint3 max)
		{
			return math.uint3((uint)((ulong)this.NextState() * (ulong)max.x >> 32), (uint)((ulong)this.NextState() * (ulong)max.y >> 32), (uint)((ulong)this.NextState() * (ulong)max.z >> 32));
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000581AC File Offset: 0x000563AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4 NextUInt4(uint4 max)
		{
			return math.uint4((uint)((ulong)this.NextState() * (ulong)max.x >> 32), (uint)((ulong)this.NextState() * (ulong)max.y >> 32), (uint)((ulong)this.NextState() * (ulong)max.z >> 32), (uint)((ulong)this.NextState() * (ulong)max.w >> 32));
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x0005820C File Offset: 0x0005640C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint NextUInt(uint min, uint max)
		{
			uint range = max - min;
			return (uint)((ulong)this.NextState() * (ulong)range >> 32) + min;
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00058230 File Offset: 0x00056430
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint2 NextUInt2(uint2 min, uint2 max)
		{
			uint2 range = max - min;
			return math.uint2((uint)((ulong)this.NextState() * (ulong)range.x >> 32), (uint)((ulong)this.NextState() * (ulong)range.y >> 32)) + min;
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00058278 File Offset: 0x00056478
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint3 NextUInt3(uint3 min, uint3 max)
		{
			uint3 range = max - min;
			return math.uint3((uint)((ulong)this.NextState() * (ulong)range.x >> 32), (uint)((ulong)this.NextState() * (ulong)range.y >> 32), (uint)((ulong)this.NextState() * (ulong)range.z >> 32)) + min;
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000582D4 File Offset: 0x000564D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public uint4 NextUInt4(uint4 min, uint4 max)
		{
			uint4 range = max - min;
			return math.uint4((uint)((ulong)this.NextState() * (ulong)range.x >> 32), (uint)((ulong)this.NextState() * (ulong)range.y >> 32), (uint)((ulong)this.NextState() * (ulong)range.z >> 32), (uint)((ulong)this.NextState() * (ulong)range.w >> 32)) + min;
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00058340 File Offset: 0x00056540
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float NextFloat()
		{
			return math.asfloat(1065353216U | (this.NextState() >> 9)) - 1f;
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x0005835C File Offset: 0x0005655C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2 NextFloat2()
		{
			return math.asfloat(1065353216U | (math.uint2(this.NextState(), this.NextState()) >> 9)) - 1f;
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x0005838F File Offset: 0x0005658F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 NextFloat3()
		{
			return math.asfloat(1065353216U | (math.uint3(this.NextState(), this.NextState(), this.NextState()) >> 9)) - 1f;
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x000583C8 File Offset: 0x000565C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4 NextFloat4()
		{
			return math.asfloat(1065353216U | (math.uint4(this.NextState(), this.NextState(), this.NextState(), this.NextState()) >> 9)) - 1f;
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x00058407 File Offset: 0x00056607
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float NextFloat(float max)
		{
			return this.NextFloat() * max;
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00058411 File Offset: 0x00056611
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2 NextFloat2(float2 max)
		{
			return this.NextFloat2() * max;
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0005841F File Offset: 0x0005661F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 NextFloat3(float3 max)
		{
			return this.NextFloat3() * max;
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x0005842D File Offset: 0x0005662D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4 NextFloat4(float4 max)
		{
			return this.NextFloat4() * max;
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x0005843B File Offset: 0x0005663B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float NextFloat(float min, float max)
		{
			return this.NextFloat() * (max - min) + min;
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00058449 File Offset: 0x00056649
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2 NextFloat2(float2 min, float2 max)
		{
			return this.NextFloat2() * (max - min) + min;
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x00058463 File Offset: 0x00056663
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 NextFloat3(float3 min, float3 max)
		{
			return this.NextFloat3() * (max - min) + min;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x0005847D File Offset: 0x0005667D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float4 NextFloat4(float4 min, float4 max)
		{
			return this.NextFloat4() * (max - min) + min;
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00058498 File Offset: 0x00056698
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double NextDouble()
		{
			ulong sx = ((ulong)this.NextState() << 20) ^ (ulong)this.NextState();
			return math.asdouble(4607182418800017408UL | sx) - 1.0;
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x000584D4 File Offset: 0x000566D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2 NextDouble2()
		{
			ulong sx = ((ulong)this.NextState() << 20) ^ (ulong)this.NextState();
			ulong sy = ((ulong)this.NextState() << 20) ^ (ulong)this.NextState();
			return math.double2(math.asdouble(4607182418800017408UL | sx), math.asdouble(4607182418800017408UL | sy)) - 1.0;
		}

		// Token: 0x06001EB2 RID: 7858 RVA: 0x0005853C File Offset: 0x0005673C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3 NextDouble3()
		{
			ulong sx = ((ulong)this.NextState() << 20) ^ (ulong)this.NextState();
			ulong sy = ((ulong)this.NextState() << 20) ^ (ulong)this.NextState();
			ulong sz = ((ulong)this.NextState() << 20) ^ (ulong)this.NextState();
			return math.double3(math.asdouble(4607182418800017408UL | sx), math.asdouble(4607182418800017408UL | sy), math.asdouble(4607182418800017408UL | sz)) - 1.0;
		}

		// Token: 0x06001EB3 RID: 7859 RVA: 0x000585C8 File Offset: 0x000567C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4 NextDouble4()
		{
			ulong sx = ((ulong)this.NextState() << 20) ^ (ulong)this.NextState();
			ulong sy = ((ulong)this.NextState() << 20) ^ (ulong)this.NextState();
			ulong sz = ((ulong)this.NextState() << 20) ^ (ulong)this.NextState();
			ulong sw = ((ulong)this.NextState() << 20) ^ (ulong)this.NextState();
			return math.double4(math.asdouble(4607182418800017408UL | sx), math.asdouble(4607182418800017408UL | sy), math.asdouble(4607182418800017408UL | sz), math.asdouble(4607182418800017408UL | sw)) - 1.0;
		}

		// Token: 0x06001EB4 RID: 7860 RVA: 0x00058674 File Offset: 0x00056874
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double NextDouble(double max)
		{
			return this.NextDouble() * max;
		}

		// Token: 0x06001EB5 RID: 7861 RVA: 0x0005867E File Offset: 0x0005687E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2 NextDouble2(double2 max)
		{
			return this.NextDouble2() * max;
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x0005868C File Offset: 0x0005688C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3 NextDouble3(double3 max)
		{
			return this.NextDouble3() * max;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x0005869A File Offset: 0x0005689A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4 NextDouble4(double4 max)
		{
			return this.NextDouble4() * max;
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x000586A8 File Offset: 0x000568A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double NextDouble(double min, double max)
		{
			return this.NextDouble() * (max - min) + min;
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000586B6 File Offset: 0x000568B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2 NextDouble2(double2 min, double2 max)
		{
			return this.NextDouble2() * (max - min) + min;
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x000586D0 File Offset: 0x000568D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3 NextDouble3(double3 min, double3 max)
		{
			return this.NextDouble3() * (max - min) + min;
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000586EA File Offset: 0x000568EA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double4 NextDouble4(double4 min, double4 max)
		{
			return this.NextDouble4() * (max - min) + min;
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x00058704 File Offset: 0x00056904
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2 NextFloat2Direction()
		{
			float s;
			float c;
			math.sincos(this.NextFloat() * 3.1415927f * 2f, out s, out c);
			return math.float2(c, s);
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x00058734 File Offset: 0x00056934
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double2 NextDouble2Direction()
		{
			double s;
			double c;
			math.sincos(this.NextDouble() * 3.141592653589793 * 2.0, out s, out c);
			return math.double2(c, s);
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x0005876C File Offset: 0x0005696C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float3 NextFloat3Direction()
		{
			float2 @float = this.NextFloat2();
			float z = @float.x * 2f - 1f;
			float r = math.sqrt(math.max(1f - z * z, 0f));
			float s;
			float c;
			math.sincos(@float.y * 3.1415927f * 2f, out s, out c);
			return math.float3(c * r, s * r, z);
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x000587D4 File Offset: 0x000569D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public double3 NextDouble3Direction()
		{
			double2 @double = this.NextDouble2();
			double z = @double.x * 2.0 - 1.0;
			double r = math.sqrt(math.max(1.0 - z * z, 0.0));
			double s;
			double c;
			math.sincos(@double.y * 3.141592653589793 * 2.0, out s, out c);
			return math.double3(c * r, s * r, z);
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00058854 File Offset: 0x00056A54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public quaternion NextQuaternionRotation()
		{
			float3 rnd = this.NextFloat3(math.float3(6.2831855f, 6.2831855f, 1f));
			float u = rnd.z;
			float2 xy = rnd.xy;
			float i = math.sqrt(1f - u);
			float j = math.sqrt(u);
			float2 sin_theta_rho;
			float2 cos_theta_rho;
			math.sincos(xy, out sin_theta_rho, out cos_theta_rho);
			quaternion q = math.quaternion(i * sin_theta_rho.x, i * cos_theta_rho.x, j * sin_theta_rho.y, j * cos_theta_rho.y);
			return math.quaternion(math.select(q.value, -q.value, q.value.w < 0f));
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00058904 File Offset: 0x00056B04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private uint NextState()
		{
			uint num = this.state;
			this.state ^= this.state << 13;
			this.state ^= this.state >> 17;
			this.state ^= this.state << 5;
			return num;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000443E6 File Offset: 0x000425E6
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckInitState()
		{
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x00058958 File Offset: 0x00056B58
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private static void CheckIndexForHash(uint index)
		{
			if (index == 4294967295U)
			{
				throw new ArgumentException("Index must not be uint.MaxValue");
			}
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000443E6 File Offset: 0x000425E6
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckState()
		{
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x000443E6 File Offset: 0x000425E6
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNextIntMax(int max)
		{
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000443E6 File Offset: 0x000425E6
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNextIntMinMax(int min, int max)
		{
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x000443E6 File Offset: 0x000425E6
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckNextUIntMinMax(uint min, uint max)
		{
		}

		// Token: 0x04000137 RID: 311
		public uint state;
	}
}
