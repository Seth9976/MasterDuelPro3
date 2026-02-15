using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004D RID: 77
	[TrackClipType(typeof(TrackAsset))]
	[SupportsChildTracks(null, 2147483647)]
	[ExcludeFromPreset]
	[Serializable]
	public class GroupTrack : TrackAsset
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x000021D7 File Offset: 0x000003D7
		internal override bool CanCompileClips()
		{
			return false;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x000094EC File Offset: 0x000076EC
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				return PlayableBinding.None;
			}
		}
	}
}
