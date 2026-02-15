using System;
using UnityEngine;

namespace Spine.Unity.Playables
{
	// Token: 0x02000009 RID: 9
	public abstract class SpinePlayableHandleBase : MonoBehaviour
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21
		public abstract SkeletonData SkeletonData { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22
		public abstract Skeleton Skeleton { get; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000017 RID: 23 RVA: 0x00002184 File Offset: 0x00000384
		// (remove) Token: 0x06000018 RID: 24 RVA: 0x000021BC File Offset: 0x000003BC
		public event SpineEventDelegate AnimationEvents;

		// Token: 0x06000019 RID: 25 RVA: 0x000021F4 File Offset: 0x000003F4
		public virtual void HandleEvents(ExposedList<Event> eventBuffer)
		{
			if (eventBuffer == null || this.AnimationEvents == null)
			{
				return;
			}
			int i = 0;
			int j = eventBuffer.Count;
			while (i < j)
			{
				this.AnimationEvents(eventBuffer.Items[i]);
				i++;
			}
		}
	}
}
