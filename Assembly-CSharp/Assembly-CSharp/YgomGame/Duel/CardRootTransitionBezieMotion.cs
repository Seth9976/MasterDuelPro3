using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CF8 RID: 3320
	public class CardRootTransitionBezieMotion : CardRootTransitionTimeBase
	{
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06005F9C RID: 24476 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected override float dulation
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06005F9D RID: 24477 RVA: 0x000F533A File Offset: 0x000F353A
		public CardRootTransitionBezieMotion(BezierMotionSetting[] motion, Camera camera)
		{
		}

		// Token: 0x06005F9E RID: 24478 RVA: 0x000F533A File Offset: 0x000F353A
		public CardRootTransitionBezieMotion(BezierMotionSetting motion, Camera camera)
		{
		}

		// Token: 0x06005F9F RID: 24479 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetCardLocator(CardLocator fromLocator, CardLocator toLocator)
		{
		}

		// Token: 0x06005FA0 RID: 24480 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void UpdateTransition(float t)
		{
		}

		// Token: 0x04009A72 RID: 39538
		private ChainedBezierMotion motion;

		// Token: 0x04009A73 RID: 39539
		private Camera camera;
	}
}
