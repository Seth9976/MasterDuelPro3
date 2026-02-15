using System;

namespace DG.Tweening.Core
{
	// Token: 0x0200009F RID: 159
	public abstract class ABSSequentiable
	{
		// Token: 0x040001C4 RID: 452
		internal TweenType tweenType;

		// Token: 0x040001C5 RID: 453
		internal float sequencedPosition;

		// Token: 0x040001C6 RID: 454
		internal float sequencedEndPosition;

		// Token: 0x040001C7 RID: 455
		internal TweenCallback onStart;
	}
}
