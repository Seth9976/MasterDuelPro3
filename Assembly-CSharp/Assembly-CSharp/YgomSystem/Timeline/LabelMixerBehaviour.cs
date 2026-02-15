using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace YgomSystem.Timeline
{
	// Token: 0x020006AC RID: 1708
	[Serializable]
	public class LabelMixerBehaviour : PlayableBehaviour
	{
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600356E RID: 13678 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyDictionary<string, TimelineClip> trackClips
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(IReadOnlyList<TimelineClip> trackClips)
		{
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnPlayableCreate(Playable playable)
		{
		}

		// Token: 0x040030D0 RID: 12496
		private PlayableDirector m_Director;

		// Token: 0x040030D1 RID: 12497
		private Dictionary<string, TimelineClip> m_TrackClips;
	}
}
