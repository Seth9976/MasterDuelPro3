using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using YgomGame.MDMarkup;
using YgomGame.Menu.Common;
using YgomSystem.UI;

namespace YgomSystem.Home
{
	// Token: 0x02000763 RID: 1891
	public class BindingTopicsBanner : MonoBehaviour, IAsyncProgressContainer, ILoadingIconHandler
	{
		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x0000216A File Offset: 0x0000036A
		public MDMarkupBannerContext bannerContext
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06003AFB RID: 15099 RVA: 0x0000216A File Offset: 0x0000036A
		public TopicsBannerWidget topicsBanner
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06003AFD RID: 15101 RVA: 0x0000216D File Offset: 0x0000036D
		public bool visible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<IAsyncProgressContent> asyncProgressContents
		{
			get
			{
				return null;
			}
		}

		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06003AFF RID: 15103 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x06003B00 RID: 15104 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onReloadEvent
		{
			[CompilerGenerated]
			add
			{
			}
			[CompilerGenerated]
			remove
			{
			}
		}

		// Token: 0x06003B01 RID: 15105 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06003B02 RID: 15106 RVA: 0x0000216D File Offset: 0x0000036D
		public void ApplyContext()
		{
		}

		// Token: 0x06003B03 RID: 15107 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsDone()
		{
			return false;
		}

		// Token: 0x06003B04 RID: 15108 RVA: 0x0000216D File Offset: 0x0000036D
		private void RefreshVisible()
		{
		}

		// Token: 0x0400346F RID: 13423
		[SerializeField]
		private MDMarkupBannerContext m_BannerContext;

		// Token: 0x04003470 RID: 13424
		private TopicsBannerWidget m_TopicsBanner;

		// Token: 0x04003471 RID: 13425
		private bool m_Visible;
	}
}
