using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000041 RID: 65
	public interface IMarker
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000273 RID: 627
		// (set) Token: 0x06000274 RID: 628
		double time { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000275 RID: 629
		TrackAsset parent { get; }

		// Token: 0x06000276 RID: 630
		void Initialize(TrackAsset parent);
	}
}
