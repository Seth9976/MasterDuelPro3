using System;
using System.Collections.Generic;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000045 RID: 69
	[TrackBindingType(typeof(GameObject))]
	[HideInMenu]
	[ExcludeFromPreset]
	[Serializable]
	public class MarkerTrack : TrackAsset
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00008F38 File Offset: 0x00007138
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				TimelineAsset timelineAsset = base.timelineAsset;
				if (!(this == ((timelineAsset != null) ? timelineAsset.markerTrack : null)))
				{
					return base.outputs;
				}
				return new List<PlayableBinding> { ScriptPlayableBinding.Create(base.name, null, typeof(GameObject)) };
			}
		}
	}
}
