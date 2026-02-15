using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;

namespace YgomGame.Credit
{
	// Token: 0x0200100F RID: 4111
	public class CreditBGTimeline
	{
		// Token: 0x06007BAE RID: 31662 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTimeline()
		{
		}

		// Token: 0x06007BAF RID: 31663 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartTimeline(bool result)
		{
		}

		// Token: 0x06007BB0 RID: 31664 RVA: 0x0000216D File Offset: 0x0000036D
		public void StopTimeline()
		{
		}

		// Token: 0x0400B35A RID: 45914
		private readonly string k_timelinePath;

		// Token: 0x0400B35B RID: 45915
		private ElementObjectManager m_effEom;

		// Token: 0x0400B35C RID: 45916
		private List<UnityAction<Texture2D>> m_onFinishList;

		// Token: 0x0400B35D RID: 45917
		private PlayableDirector m_playableDirector;

		// Token: 0x0400B35E RID: 45918
		private List<int> m_favoriteIds;
	}
}
