using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000032 RID: 50
	internal struct LightStats
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000BA62 File Offset: 0x00009C62
		public bool useAnyLights
		{
			get
			{
				return this.totalLights + this.totalShadowLights > 0;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000BA74 File Offset: 0x00009C74
		public bool useLights
		{
			get
			{
				return this.totalLights > 0;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000BA7F File Offset: 0x00009C7F
		public bool useShadows
		{
			get
			{
				return this.totalShadows > 0;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000BA8A File Offset: 0x00009C8A
		public bool useVolumetricLights
		{
			get
			{
				return this.totalVolumetricUsage > 0;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000BA95 File Offset: 0x00009C95
		public bool useVolumetricShadowLights
		{
			get
			{
				return this.totalVolumetricShadowUsage > 0;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		public bool useNormalMap
		{
			get
			{
				return this.totalNormalMapUsage > 0;
			}
		}

		// Token: 0x04000104 RID: 260
		public int totalLights;

		// Token: 0x04000105 RID: 261
		public int totalShadowLights;

		// Token: 0x04000106 RID: 262
		public int totalShadows;

		// Token: 0x04000107 RID: 263
		public int totalNormalMapUsage;

		// Token: 0x04000108 RID: 264
		public int totalVolumetricUsage;

		// Token: 0x04000109 RID: 265
		public int totalVolumetricShadowUsage;

		// Token: 0x0400010A RID: 266
		public uint blendStylesUsed;

		// Token: 0x0400010B RID: 267
		public uint blendStylesWithLights;
	}
}
