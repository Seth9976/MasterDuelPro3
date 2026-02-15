using System;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.MDMarkup;
using YgomGame.Menu.Common;
using YgomSystem.ElementSystem;

namespace YgomSystem.Home
{
	// Token: 0x02000766 RID: 1894
	public class TopicsBannerWidget : IAsyncProgressContainer
	{
		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06003B13 RID: 15123 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<IAsyncProgressContent> asyncProgressContents
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003B14 RID: 15124 RVA: 0x0000216A File Offset: 0x0000036A
		public static TopicsBannerWidget Create(GameObject parent, MDMarkupBannerContext bannerContext)
		{
			return null;
		}

		// Token: 0x06003B15 RID: 15125 RVA: 0x0000216A File Offset: 0x0000036A
		private TopicsBannerWidget InnerBinding(GameObject parent, string prefPath, Dictionary<string, object> prefArgs)
		{
			return null;
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x0000216A File Offset: 0x0000036A
		private TopicsBannerWidget FailedInnerBinding(Dictionary<string, object> prefArgs)
		{
			return null;
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x0000216D File Offset: 0x0000036D
		private void ClearProgressContent()
		{
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x0000216D File Offset: 0x0000036D
		private void AssignProgressContent(IAsyncProgressContent progressContent)
		{
		}

		// Token: 0x0400347B RID: 13435
		private List<IAsyncProgressContent> m_AsyncProgressContents;

		// Token: 0x0400347C RID: 13436
		public ElementObjectManager rootEom;
	}
}
