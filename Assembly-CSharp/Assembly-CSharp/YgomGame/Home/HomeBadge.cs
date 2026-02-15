using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;

namespace YgomGame.Home
{
	// Token: 0x02000BDE RID: 3038
	public class HomeBadge
	{
		// Token: 0x06005681 RID: 22145 RVA: 0x0000216D File Offset: 0x0000036D
		public void InitBadge(HomeBadge.Badge badge, ElementObjectManager buttonEom)
		{
		}

		// Token: 0x06005682 RID: 22146 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetBadge(HomeBadge.Badge badge, int count = 0)
		{
		}

		// Token: 0x04009373 RID: 37747
		private readonly string IMG_NUMBADGE_LABEL;

		// Token: 0x04009374 RID: 37748
		private readonly string IMG_NEWBADGE_LABEL;

		// Token: 0x04009375 RID: 37749
		private readonly string TXT_BADGE_LABEL;

		// Token: 0x04009376 RID: 37750
		private Dictionary<HomeBadge.Badge, ElementObjectManager> badgeMap;

		// Token: 0x02000BDF RID: 3039
		public enum Badge
		{
			// Token: 0x04009378 RID: 37752
			MISSION = 1,
			// Token: 0x04009379 RID: 37753
			FRIEND,
			// Token: 0x0400937A RID: 37754
			DUEL,
			// Token: 0x0400937B RID: 37755
			SHOP,
			// Token: 0x0400937C RID: 37756
			PRESENT,
			// Token: 0x0400937D RID: 37757
			NOTICE,
			// Token: 0x0400937E RID: 37758
			QUEST,
			// Token: 0x0400937F RID: 37759
			DUELPASS,
			// Token: 0x04009380 RID: 37760
			DECK = 100,
			// Token: 0x04009381 RID: 37761
			SUBMENU,
			// Token: 0x04009382 RID: 37762
			DUELLIVE
		}
	}
}
