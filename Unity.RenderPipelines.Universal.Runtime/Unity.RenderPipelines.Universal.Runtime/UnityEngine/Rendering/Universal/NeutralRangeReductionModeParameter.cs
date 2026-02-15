using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000102 RID: 258
	[Serializable]
	public sealed class NeutralRangeReductionModeParameter : VolumeParameter<NeutralRangeReductionMode>
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x00016D2E File Offset: 0x00014F2E
		public NeutralRangeReductionModeParameter(NeutralRangeReductionMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
