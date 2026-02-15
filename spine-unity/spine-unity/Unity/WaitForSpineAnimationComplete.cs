using System;
using System.Collections;

namespace Spine.Unity
{
	// Token: 0x0200007A RID: 122
	public class WaitForSpineAnimationComplete : WaitForSpineAnimation, IEnumerator
	{
		// Token: 0x06000371 RID: 881 RVA: 0x000139EF File Offset: 0x00011BEF
		public WaitForSpineAnimationComplete(TrackEntry trackEntry, bool includeEndEvent = false)
			: base(trackEntry, includeEndEvent ? (WaitForSpineAnimation.AnimationEventTypes.End | WaitForSpineAnimation.AnimationEventTypes.Complete) : WaitForSpineAnimation.AnimationEventTypes.Complete)
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00013A01 File Offset: 0x00011C01
		public WaitForSpineAnimationComplete NowWaitFor(TrackEntry trackEntry, bool includeEndEvent = false)
		{
			base.SafeSubscribe(trackEntry, includeEndEvent ? (WaitForSpineAnimation.AnimationEventTypes.End | WaitForSpineAnimation.AnimationEventTypes.Complete) : WaitForSpineAnimation.AnimationEventTypes.Complete);
			return this;
		}
	}
}
