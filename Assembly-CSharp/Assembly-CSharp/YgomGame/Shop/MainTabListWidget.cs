using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Shop
{
	// Token: 0x02000936 RID: 2358
	public class MainTabListWidget : ElementWidgetBase
	{
		// Token: 0x060044B3 RID: 17587 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MainTabListWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x0000216D File Offset: 0x0000036D
		public void Init(IMainTabListWidgetHandler handler, IMainTabListWidgetListener listener)
		{
		}

		// Token: 0x060044B5 RID: 17589 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateDataCount()
		{
		}

		// Token: 0x060044B6 RID: 17590 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateData()
		{
		}

		// Token: 0x060044B7 RID: 17591 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool SelectCurrentIdx(bool isInitializeSelect = false)
		{
			return false;
		}

		// Token: 0x060044B8 RID: 17592 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool CheckRecoverSelectItem()
		{
			return false;
		}

		// Token: 0x04008325 RID: 33573
		private const string k_ELabelFormatTab = "Tab{0:D2}";

		// Token: 0x04008326 RID: 33574
		public readonly Selector selector;

		// Token: 0x04008327 RID: 33575
		private readonly ShopTabWidget[] m_SourceTabWidgets;

		// Token: 0x04008328 RID: 33576
		private readonly List<ShopTabWidget> m_ActiveTabWidets;

		// Token: 0x04008329 RID: 33577
		private IMainTabListWidgetHandler m_Handler;

		// Token: 0x0400832A RID: 33578
		private IMainTabListWidgetListener m_Listener;
	}
}
