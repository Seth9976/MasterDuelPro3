using System;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x02000979 RID: 2425
	public class SubTabSingleWidget : ElementWidgetBase, ISubTabWidget
	{
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06004713 RID: 18195 RVA: 0x0000216A File Offset: 0x0000036A
		public ShopTabWidget tabWidget
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06004714 RID: 18196 RVA: 0x0000216A File Offset: 0x0000036A
		public SubTabGroupWidget parentGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public SubTabSingleWidget(ElementObjectManager eom, SubTabGroupWidget parentGroupWidget = null)
			: base(null)
		{
		}

		// Token: 0x04008539 RID: 34105
		private readonly SubTabGroupWidget m_ParentGroupWidget;

		// Token: 0x0400853A RID: 34106
		private readonly ShopTabWidget m_TabWidget;
	}
}
