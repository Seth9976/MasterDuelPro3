using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CFB RID: 3323
	public class CardRootTransitionShuffle : CardRootTransitionTimeBase
	{
		// Token: 0x06005FA5 RID: 24485 RVA: 0x000F533A File Offset: 0x000F353A
		public CardRootTransitionShuffle(Vector3 pausePos)
		{
		}

		// Token: 0x06005FA6 RID: 24486 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void UpdateTransition(float t)
		{
		}

		// Token: 0x04009A75 RID: 39541
		private Vector3 pausePos;

		// Token: 0x04009A76 RID: 39542
		private CardRootTransitionShuffle.Step step;

		// Token: 0x04009A77 RID: 39543
		private const float timeToMove1 = 0f;

		// Token: 0x04009A78 RID: 39544
		private const float timeToPause = 0.3f;

		// Token: 0x04009A79 RID: 39545
		private const float timeToMove2 = 0.7f;

		// Token: 0x04009A7A RID: 39546
		private const float timeOfEnd = 1f;

		// Token: 0x02000CFC RID: 3324
		private enum Step
		{
			// Token: 0x04009A7C RID: 39548
			GoCenter,
			// Token: 0x04009A7D RID: 39549
			WaitPause,
			// Token: 0x04009A7E RID: 39550
			GoToLocator,
			// Token: 0x04009A7F RID: 39551
			Finish
		}
	}
}
