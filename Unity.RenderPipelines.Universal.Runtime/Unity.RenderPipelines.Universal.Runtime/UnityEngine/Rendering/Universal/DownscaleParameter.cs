using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000E4 RID: 228
	[Serializable]
	public sealed class DownscaleParameter : VolumeParameter<BloomDownscaleMode>
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x00015DA7 File Offset: 0x00013FA7
		public DownscaleParameter(BloomDownscaleMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
