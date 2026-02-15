using System;
using System.Collections;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000078 RID: 120
	public class WaitForSpineAnimation : IEnumerator
	{
		// Token: 0x0600036A RID: 874 RVA: 0x00013915 File Offset: 0x00011B15
		public WaitForSpineAnimation(TrackEntry trackEntry, WaitForSpineAnimation.AnimationEventTypes eventsToWaitFor)
		{
			this.SafeSubscribe(trackEntry, eventsToWaitFor);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00013925 File Offset: 0x00011B25
		public WaitForSpineAnimation NowWaitFor(TrackEntry trackEntry, WaitForSpineAnimation.AnimationEventTypes eventsToWaitFor)
		{
			this.SafeSubscribe(trackEntry, eventsToWaitFor);
			return this;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00013930 File Offset: 0x00011B30
		bool IEnumerator.MoveNext()
		{
			if (this.m_WasFired)
			{
				((IEnumerator)this).Reset();
				return false;
			}
			return true;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00013943 File Offset: 0x00011B43
		void IEnumerator.Reset()
		{
			this.m_WasFired = false;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600036E RID: 878 RVA: 0x0001394C File Offset: 0x00011B4C
		object IEnumerator.Current
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00013950 File Offset: 0x00011B50
		protected void SafeSubscribe(TrackEntry trackEntry, WaitForSpineAnimation.AnimationEventTypes eventsToWaitFor)
		{
			if (trackEntry == null)
			{
				Debug.LogWarning("TrackEntry was null. Coroutine will continue immediately.");
				this.m_WasFired = true;
				return;
			}
			if ((eventsToWaitFor & WaitForSpineAnimation.AnimationEventTypes.Start) != (WaitForSpineAnimation.AnimationEventTypes)0)
			{
				trackEntry.Start += this.HandleComplete;
			}
			if ((eventsToWaitFor & WaitForSpineAnimation.AnimationEventTypes.Interrupt) != (WaitForSpineAnimation.AnimationEventTypes)0)
			{
				trackEntry.Interrupt += this.HandleComplete;
			}
			if ((eventsToWaitFor & WaitForSpineAnimation.AnimationEventTypes.End) != (WaitForSpineAnimation.AnimationEventTypes)0)
			{
				trackEntry.End += this.HandleComplete;
			}
			if ((eventsToWaitFor & WaitForSpineAnimation.AnimationEventTypes.Dispose) != (WaitForSpineAnimation.AnimationEventTypes)0)
			{
				trackEntry.Dispose += this.HandleComplete;
			}
			if ((eventsToWaitFor & WaitForSpineAnimation.AnimationEventTypes.Complete) != (WaitForSpineAnimation.AnimationEventTypes)0)
			{
				trackEntry.Complete += this.HandleComplete;
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000139E6 File Offset: 0x00011BE6
		private void HandleComplete(TrackEntry trackEntry)
		{
			this.m_WasFired = true;
		}

		// Token: 0x0400022B RID: 555
		private bool m_WasFired;

		// Token: 0x02000079 RID: 121
		[Flags]
		public enum AnimationEventTypes
		{
			// Token: 0x0400022D RID: 557
			Start = 1,
			// Token: 0x0400022E RID: 558
			Interrupt = 2,
			// Token: 0x0400022F RID: 559
			End = 4,
			// Token: 0x04000230 RID: 560
			Dispose = 8,
			// Token: 0x04000231 RID: 561
			Complete = 16
		}
	}
}
