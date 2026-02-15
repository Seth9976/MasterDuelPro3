using System;
using System.Collections.Generic;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BCF RID: 3023
	public class MDMarkupTabsContainerWidget : ElementWidgetBase, IMDMarkupContainerWidget
	{
		// Token: 0x0600563D RID: 22077 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MDMarkupTabsContainerWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(IMDMarkupContainer containerData)
		{
		}

		// Token: 0x0600563F RID: 22079 RVA: 0x0000216D File Offset: 0x0000036D
		public void Output(MDMarkupGraphFactory graphFactory, Action onComplete)
		{
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x0000216D File Offset: 0x0000036D
		private void OutputGraph(int idx, Action onComplete)
		{
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnStart(Dictionary<string, object> args)
		{
		}

		// Token: 0x06005642 RID: 22082 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeIdx(int newIdx)
		{
		}

		// Token: 0x04009321 RID: 37665
		private readonly string k_ELabelTitleText;

		// Token: 0x04009322 RID: 37666
		private readonly string k_ELabelScrollViewTemplate;

		// Token: 0x04009323 RID: 37667
		private readonly string k_ELabelTabGroup;

		// Token: 0x04009324 RID: 37668
		private readonly string k_ELabelTabTemplate;

		// Token: 0x04009325 RID: 37669
		private readonly string k_ELabelTabOnLabel;

		// Token: 0x04009326 RID: 37670
		private readonly string k_ELabelTabOffLabel;

		// Token: 0x04009327 RID: 37671
		private readonly string k_ELabelShortcutButtonBack;

		// Token: 0x04009328 RID: 37672
		private readonly string k_ELabelShortcutButtonNext;

		// Token: 0x04009329 RID: 37673
		private ScrollRect[] m_ScrollRects;

		// Token: 0x0400932A RID: 37674
		private MDMarkupGraphWidget[] m_MarkupGraphs;

		// Token: 0x0400932B RID: 37675
		private ToggleGroupWidget m_ToggleGroupWidget;

		// Token: 0x0400932C RID: 37676
		private MDMarkupTabsContainer m_ContainerData;

		// Token: 0x0400932D RID: 37677
		private MDMarkupGraphFactory m_GraphFactory;
	}
}
