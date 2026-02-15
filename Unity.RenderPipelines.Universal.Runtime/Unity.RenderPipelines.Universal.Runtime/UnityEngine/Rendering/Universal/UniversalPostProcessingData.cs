using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C5 RID: 197
	public class UniversalPostProcessingData : ContextItem
	{
		// Token: 0x060004E3 RID: 1251 RVA: 0x00012E6C File Offset: 0x0001106C
		public override void Reset()
		{
			this.isEnabled = false;
			this.gradingMode = ColorGradingMode.LowDynamicRange;
			this.lutSize = 0;
			this.useFastSRGBLinearConversion = false;
			this.supportScreenSpaceLensFlare = false;
			this.supportDataDrivenLensFlare = false;
		}

		// Token: 0x0400044C RID: 1100
		public bool isEnabled;

		// Token: 0x0400044D RID: 1101
		public ColorGradingMode gradingMode;

		// Token: 0x0400044E RID: 1102
		public int lutSize;

		// Token: 0x0400044F RID: 1103
		public bool useFastSRGBLinearConversion;

		// Token: 0x04000450 RID: 1104
		public bool supportScreenSpaceLensFlare;

		// Token: 0x04000451 RID: 1105
		public bool supportDataDrivenLensFlare;
	}
}
