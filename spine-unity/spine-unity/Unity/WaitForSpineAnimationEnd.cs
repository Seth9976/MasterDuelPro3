using System;
using System.Collections;

namespace Spine.Unity
{
	// Token: 0x0200007B RID: 123
	public class WaitForSpineAnimationEnd : WaitForSpineAnimation, IEnumerator
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00013A14 File Offset: 0x00011C14
		public WaitForSpineAnimationEnd(TrackEntry trackEntry)
			: base(trackEntry, WaitForSpineAnimation.AnimationEventTypes.End)
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00013A1E File Offset: 0x00011C1E
		public WaitForSpineAnimationEnd NowWaitFor(TrackEntry trackEntry)
		{
			base.SafeSubscribe(trackEntry, WaitForSpineAnimation.AnimationEventTypes.End);
			return this;
		}
	}
}
