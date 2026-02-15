using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000EE RID: 238
	public interface IProbeVolumeEnabledRenderPipeline
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060007B2 RID: 1970
		bool supportProbeVolume { get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060007B3 RID: 1971
		ProbeVolumeSHBands maxSHBands { get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060007B4 RID: 1972
		[Obsolete("This field is no longer necessary")]
		ProbeVolumeSceneData probeVolumeSceneData { get; }
	}
}
