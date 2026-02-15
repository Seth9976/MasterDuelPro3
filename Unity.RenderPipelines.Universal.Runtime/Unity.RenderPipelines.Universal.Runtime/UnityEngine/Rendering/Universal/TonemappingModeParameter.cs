using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000101 RID: 257
	[Serializable]
	public sealed class TonemappingModeParameter : VolumeParameter<TonemappingMode>
	{
		// Token: 0x06000609 RID: 1545 RVA: 0x00016D24 File Offset: 0x00014F24
		public TonemappingModeParameter(TonemappingMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
