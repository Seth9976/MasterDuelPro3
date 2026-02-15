using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Willow
{
	// Token: 0x02001543 RID: 5443
	[Serializable]
	public class ActiveObjectPlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		// Token: 0x1700149C RID: 5276
		// (get) Token: 0x06009DC3 RID: 40387 RVA: 0x000029CC File Offset: 0x00000BCC
		public ClipCaps clipCaps
		{
			get
			{
				return ClipCaps.None;
			}
		}

		// Token: 0x06009DC4 RID: 40388 RVA: 0x0019AFE4 File Offset: 0x001991E4
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			ScriptPlayable<ActiveObjectPlayableBehaviour> playable = ScriptPlayable<ActiveObjectPlayableBehaviour>.Create(graph, 0);
			ActiveObjectPlayableBehaviour behaviour = playable.GetBehaviour();
			behaviour.m_parentObject = this.m_parentObject.Resolve(graph.GetResolver());
			behaviour.m_relayObject = this.m_relayObject.Resolve(graph.GetResolver());
			behaviour.m_prefab = this.m_prefab;
			behaviour.m_isFullLifespan = this.m_isFullLifespan;
			behaviour.m_isLookAtCamera = this.m_isLookAtCamera;
			behaviour.controller = go.transform.parent.GetComponent<CustomTimelineController>();
			behaviour.m_name = this.m_name;
			return playable;
		}

		// Token: 0x0400DD7D RID: 56701
		public string m_name;

		// Token: 0x0400DD7E RID: 56702
		public ExposedReference<GameObject> m_parentObject;

		// Token: 0x0400DD7F RID: 56703
		public GameObject m_prefab;

		// Token: 0x0400DD80 RID: 56704
		public bool m_isFullLifespan;

		// Token: 0x0400DD81 RID: 56705
		public bool m_onCreateRelayObject;

		// Token: 0x0400DD82 RID: 56706
		public ExposedReference<CustomTimelineObject> m_relayObject;

		// Token: 0x0400DD83 RID: 56707
		public bool m_isSyncPosition;

		// Token: 0x0400DD84 RID: 56708
		public bool m_isSyncRotation;

		// Token: 0x0400DD85 RID: 56709
		public bool m_isSyncScale;

		// Token: 0x0400DD86 RID: 56710
		public bool m_isLookAtCamera;

		// Token: 0x0400DD87 RID: 56711
		public bool m_isIgnoreInitRotation;

		// Token: 0x0400DD88 RID: 56712
		public bool m_isIgnoreInitScale;

		// Token: 0x0400DD89 RID: 56713
		public bool m_isRelayChild;
	}
}
