using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000F6 RID: 246
	[Serializable]
	public sealed class MotionBlurQualityParameter : VolumeParameter<MotionBlurQuality>
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x00016865 File Offset: 0x00014A65
		public MotionBlurQualityParameter(MotionBlurQuality value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
