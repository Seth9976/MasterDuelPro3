using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000140 RID: 320
	[Serializable]
	public sealed class APVLeakReductionModeParameter : VolumeParameter<APVLeakReductionMode>
	{
		// Token: 0x06000A0F RID: 2575 RVA: 0x00020D4E File Offset: 0x0001EF4E
		public APVLeakReductionModeParameter(APVLeakReductionMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
