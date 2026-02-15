using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000CF7 RID: 3319
	public class CardRootTransitionArc : CardRootTransitionTimeBase
	{
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06005F99 RID: 24473 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected override float dulation
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x06005F9A RID: 24474 RVA: 0x000F533A File Offset: 0x000F353A
		public CardRootTransitionArc(Vector3 viaPos, float p1, float p2, float p3, float dulVal)
		{
		}

		// Token: 0x06005F9B RID: 24475 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void UpdateTransition(float t)
		{
		}

		// Token: 0x04009A6C RID: 39532
		private float time;

		// Token: 0x04009A6D RID: 39533
		private Vector3 viaPos;

		// Token: 0x04009A6E RID: 39534
		private float p1;

		// Token: 0x04009A6F RID: 39535
		private float p2;

		// Token: 0x04009A70 RID: 39536
		private float p3;

		// Token: 0x04009A71 RID: 39537
		private float dulVal;
	}
}
