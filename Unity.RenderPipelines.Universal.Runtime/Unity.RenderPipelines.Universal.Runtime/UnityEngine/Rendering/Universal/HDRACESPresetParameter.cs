using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000103 RID: 259
	[Serializable]
	public sealed class HDRACESPresetParameter : VolumeParameter<HDRACESPreset>
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x00016D38 File Offset: 0x00014F38
		public HDRACESPresetParameter(HDRACESPreset value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
