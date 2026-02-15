using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000EC RID: 236
	[Serializable]
	public sealed class DepthOfFieldModeParameter : VolumeParameter<DepthOfFieldMode>
	{
		// Token: 0x060005E8 RID: 1512 RVA: 0x00016572 File Offset: 0x00014772
		public DepthOfFieldModeParameter(DepthOfFieldMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
