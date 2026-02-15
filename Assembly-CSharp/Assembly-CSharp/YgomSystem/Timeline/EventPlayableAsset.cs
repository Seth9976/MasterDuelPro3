using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace YgomSystem.Timeline
{
	// Token: 0x020006A2 RID: 1698
	public class EventPlayableAsset : PlayableAsset
	{
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x000F165E File Offset: 0x000EF85E
		public override double duration
		{
			get
			{
				return 0.0;
			}
		}

		// Token: 0x0600355A RID: 13658 RVA: 0x000F2CD0 File Offset: 0x000F0ED0
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			ScriptPlayable<EventPlayableBehaviour> playable = ScriptPlayable<EventPlayableBehaviour>.Create(graph, 0);
			EventPlayableBehaviour behaviour = playable.GetBehaviour();
			behaviour.label = this.label;
			behaviour.eventList = new List<EventPlayableBehaviour.EventInfo>();
			foreach (EventPlayableAsset.EventInfo info in this.eventList)
			{
				behaviour.eventList.Add(new EventPlayableBehaviour.EventInfo
				{
					label = info.label,
					time = info.time,
					isDone = false
				});
			}
			return playable;
		}

		// Token: 0x040030B8 RID: 12472
		public string label;

		// Token: 0x040030B9 RID: 12473
		public List<EventPlayableAsset.EventInfo> eventList;

		// Token: 0x020006A3 RID: 1699
		[Serializable]
		public class EventInfo
		{
			// Token: 0x040030BA RID: 12474
			public string label;

			// Token: 0x040030BB RID: 12475
			public double time;
		}
	}
}
