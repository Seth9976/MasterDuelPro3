using System;

namespace DG.Tweening.Core
{
	// Token: 0x020000B3 RID: 179
	internal class SequenceCallback : ABSSequentiable
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0001147B File Offset: 0x0000F67B
		public SequenceCallback(float sequencedPosition, TweenCallback callback)
		{
			this.tweenType = TweenType.Callback;
			this.sequencedPosition = sequencedPosition;
			this.onStart = callback;
		}
	}
}
