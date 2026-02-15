using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C6A RID: 3178
	public class ProductShowcaseWidget : ElementWidgetBase
	{
		// Token: 0x17000987 RID: 2439
		// (get) Token: 0x06005AF8 RID: 23288 RVA: 0x0000216A File Offset: 0x0000036A
		public SubTabListWidget subTabListWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000988 RID: 2440
		// (get) Token: 0x06005AF9 RID: 23289 RVA: 0x0000216A File Offset: 0x0000036A
		public ProductListWidget productListWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005AFA RID: 23290 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ProductShowcaseWidget(ElementObjectManager eom, DuelLiveRootWidget owner, bool frag = false)
			: base(null)
		{
		}

		// Token: 0x06005AFB RID: 23291 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetActive(bool isActive)
		{
		}

		// Token: 0x06005AFC RID: 23292 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TrySelectDefault(bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x04009610 RID: 38416
		private const string k_ELabelSubTabs = "SubTabs";

		// Token: 0x04009611 RID: 38417
		private readonly SubTabListWidget m_SubTabListWidget;

		// Token: 0x04009612 RID: 38418
		private readonly ProductListWidget m_ProductListWidget;

		// Token: 0x04009613 RID: 38419
		private readonly Selector[] m_Selectors;

		// Token: 0x04009614 RID: 38420
		public readonly ProductShowcaseWidget.Context ctx;

		// Token: 0x02000C6B RID: 3179
		public class Context
		{
			// Token: 0x17000989 RID: 2441
			// (get) Token: 0x06005AFD RID: 23293 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool existsSubTab
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04009615 RID: 38421
			public List<SubTabListWidget.TabContext> subTabListCtx;

			// Token: 0x04009616 RID: 38422
			public readonly ProductListWidget.Context productListCtx;
		}
	}
}
