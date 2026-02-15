using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000C7B RID: 3195
	internal class AllCardBreaker
	{
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06005BD3 RID: 23507 RVA: 0x000029C5 File Offset: 0x00000BC5
		private float intervalTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06005BD4 RID: 23508 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool finished
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x0000216D File Offset: 0x0000036D
		public void Play(DuelGameObjectManager goManager, int loser, AllCardBreaker.Type type)
		{
		}

		// Token: 0x06005BD6 RID: 23510 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool UpdateBreakLoserCard()
		{
			return false;
		}

		// Token: 0x040096D8 RID: 38616
		private DuelGameObjectManager goManager;

		// Token: 0x040096D9 RID: 38617
		private Queue<CardRoot> breakTargets;

		// Token: 0x040096DA RID: 38618
		private float timer;

		// Token: 0x040096DB RID: 38619
		private const float breakPerSecond = 5f;

		// Token: 0x040096DC RID: 38620
		private const float maxSecond = 3f;

		// Token: 0x040096DD RID: 38621
		private int numMovings;

		// Token: 0x040096DE RID: 38622
		private int targetNum;

		// Token: 0x040096DF RID: 38623
		private AllCardBreaker.Type type;

		// Token: 0x02000C7C RID: 3196
		public enum Type
		{
			// Token: 0x040096E1 RID: 38625
			Break,
			// Token: 0x040096E2 RID: 38626
			DeckOut
		}
	}
}
