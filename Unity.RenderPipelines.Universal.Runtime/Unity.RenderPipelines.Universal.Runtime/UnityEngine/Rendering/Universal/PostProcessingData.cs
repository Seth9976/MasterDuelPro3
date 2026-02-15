using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001D0 RID: 464
	public struct PostProcessingData
	{
		// Token: 0x06000A4D RID: 2637 RVA: 0x000330AE File Offset: 0x000312AE
		internal PostProcessingData(ContextContainer frameData)
		{
			this.frameData = frameData;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x000330B7 File Offset: 0x000312B7
		internal UniversalPostProcessingData universalPostProcessingData
		{
			get
			{
				return this.frameData.Get<UniversalPostProcessingData>();
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x000330C4 File Offset: 0x000312C4
		public ref ColorGradingMode gradingMode
		{
			get
			{
				return ref this.frameData.Get<UniversalPostProcessingData>().gradingMode;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x000330D6 File Offset: 0x000312D6
		public ref int lutSize
		{
			get
			{
				return ref this.frameData.Get<UniversalPostProcessingData>().lutSize;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A51 RID: 2641 RVA: 0x000330E8 File Offset: 0x000312E8
		public ref bool useFastSRGBLinearConversion
		{
			get
			{
				return ref this.frameData.Get<UniversalPostProcessingData>().useFastSRGBLinearConversion;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x000330FA File Offset: 0x000312FA
		public ref bool supportScreenSpaceLensFlare
		{
			get
			{
				return ref this.frameData.Get<UniversalPostProcessingData>().supportScreenSpaceLensFlare;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000A53 RID: 2643 RVA: 0x0003310C File Offset: 0x0003130C
		public ref bool supportDataDrivenLensFlare
		{
			get
			{
				return ref this.frameData.Get<UniversalPostProcessingData>().supportDataDrivenLensFlare;
			}
		}

		// Token: 0x04000A58 RID: 2648
		private ContextContainer frameData;
	}
}
