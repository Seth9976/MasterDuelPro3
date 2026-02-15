using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000F5 RID: 245
	[Serializable]
	public sealed class MotionBlurModeParameter : VolumeParameter<MotionBlurMode>
	{
		// Token: 0x060005F6 RID: 1526 RVA: 0x0001685B File Offset: 0x00014A5B
		public MotionBlurModeParameter(MotionBlurMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
