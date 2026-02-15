using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004D RID: 77
	[Serializable]
	public struct GlobalDynamicResolutionSettings
	{
		// Token: 0x06000486 RID: 1158 RVA: 0x000089B0 File Offset: 0x00006BB0
		public static GlobalDynamicResolutionSettings NewDefault()
		{
			return new GlobalDynamicResolutionSettings
			{
				useMipBias = false,
				maxPercentage = 100f,
				minPercentage = 100f,
				dynResType = DynamicResolutionType.Hardware,
				upsampleFilter = DynamicResUpscaleFilter.CatmullRom,
				forcedPercentage = 100f,
				lowResTransparencyMinimumThreshold = 0f,
				lowResVolumetricCloudsMinimumThreshold = 50f,
				rayTracingHalfResThreshold = 50f,
				DLSSUseOptimalSettings = true,
				DLSSPerfQualitySetting = 0U,
				DLSSSharpness = 0.5f,
				DLSSInjectionPoint = DynamicResolutionHandler.UpsamplerScheduleType.BeforePost,
				FSR2InjectionPoint = DynamicResolutionHandler.UpsamplerScheduleType.BeforePost,
				TAAUInjectionPoint = DynamicResolutionHandler.UpsamplerScheduleType.BeforePost,
				defaultInjectionPoint = DynamicResolutionHandler.UpsamplerScheduleType.AfterPost,
				advancedUpscalersByPriority = new List<AdvancedUpscalers> { AdvancedUpscalers.STP },
				fsrOverrideSharpness = false,
				fsrSharpness = 0.92f
			};
		}

		// Token: 0x04000107 RID: 263
		public bool enabled;

		// Token: 0x04000108 RID: 264
		public bool useMipBias;

		// Token: 0x04000109 RID: 265
		public List<AdvancedUpscalers> advancedUpscalersByPriority;

		// Token: 0x0400010A RID: 266
		public uint DLSSPerfQualitySetting;

		// Token: 0x0400010B RID: 267
		public DynamicResolutionHandler.UpsamplerScheduleType DLSSInjectionPoint;

		// Token: 0x0400010C RID: 268
		public DynamicResolutionHandler.UpsamplerScheduleType TAAUInjectionPoint;

		// Token: 0x0400010D RID: 269
		public DynamicResolutionHandler.UpsamplerScheduleType STPInjectionPoint;

		// Token: 0x0400010E RID: 270
		public DynamicResolutionHandler.UpsamplerScheduleType defaultInjectionPoint;

		// Token: 0x0400010F RID: 271
		public bool DLSSUseOptimalSettings;

		// Token: 0x04000110 RID: 272
		[Range(0f, 1f)]
		public float DLSSSharpness;

		// Token: 0x04000111 RID: 273
		public bool FSR2EnableSharpness;

		// Token: 0x04000112 RID: 274
		[Range(0f, 1f)]
		public float FSR2Sharpness;

		// Token: 0x04000113 RID: 275
		public bool FSR2UseOptimalSettings;

		// Token: 0x04000114 RID: 276
		public uint FSR2QualitySetting;

		// Token: 0x04000115 RID: 277
		public DynamicResolutionHandler.UpsamplerScheduleType FSR2InjectionPoint;

		// Token: 0x04000116 RID: 278
		public bool fsrOverrideSharpness;

		// Token: 0x04000117 RID: 279
		[Range(0f, 1f)]
		public float fsrSharpness;

		// Token: 0x04000118 RID: 280
		public float maxPercentage;

		// Token: 0x04000119 RID: 281
		public float minPercentage;

		// Token: 0x0400011A RID: 282
		public DynamicResolutionType dynResType;

		// Token: 0x0400011B RID: 283
		public DynamicResUpscaleFilter upsampleFilter;

		// Token: 0x0400011C RID: 284
		public bool forceResolution;

		// Token: 0x0400011D RID: 285
		public float forcedPercentage;

		// Token: 0x0400011E RID: 286
		public float lowResTransparencyMinimumThreshold;

		// Token: 0x0400011F RID: 287
		public float rayTracingHalfResThreshold;

		// Token: 0x04000120 RID: 288
		public float lowResSSGIMinimumThreshold;

		// Token: 0x04000121 RID: 289
		public float lowResVolumetricCloudsMinimumThreshold;

		// Token: 0x04000122 RID: 290
		[Obsolete("Obsolete, used only for data migration. Use the advancedUpscalersByPriority list instead to add the proper supported advanced upscaler by priority.")]
		public bool enableDLSS;
	}
}
