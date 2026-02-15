using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BBF RID: 3007
	public class MDMarkupPagerContainerWidget : ElementWidgetBase, IMDMarkupContainerWidget
	{
		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x060055CB RID: 21963 RVA: 0x0000216A File Offset: 0x0000036A
		public MDMarkupGraphWidget markupGraph
		{
			get
			{
				return null;
			}
		}

		// Token: 0x14000082 RID: 130
		// (add) Token: 0x060055CC RID: 21964 RVA: 0x0000216D File Offset: 0x0000036D
		// (remove) Token: 0x060055CD RID: 21965 RVA: 0x0000216D File Offset: 0x0000036D
		public event Action onClickCloseEvent
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

		// Token: 0x060055CE RID: 21966 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MDMarkupPagerContainerWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(IMDMarkupContainer containerData)
		{
		}

		// Token: 0x060055D0 RID: 21968 RVA: 0x0000216D File Offset: 0x0000036D
		public void Output(MDMarkupGraphFactory graphFactory, Action onComplete)
		{
		}

		// Token: 0x060055D1 RID: 21969 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnStart(Dictionary<string, object> args)
		{
		}

		// Token: 0x060055D2 RID: 21970 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnGraphOutputComplete(MDMarkupGraphWidget graphWidget)
		{
		}

		// Token: 0x060055D3 RID: 21971 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnPageChanged()
		{
		}

		// Token: 0x040092D7 RID: 37591
		private readonly string k_ELabelTitleText;

		// Token: 0x040092D8 RID: 37592
		private readonly string k_ELabelScrollView;

		// Token: 0x040092D9 RID: 37593
		private readonly string k_ELabelCloseButton;

		// Token: 0x040092DA RID: 37594
		private readonly string k_ELabelBackShortcutButton;

		// Token: 0x040092DB RID: 37595
		private readonly string k_ELabelFooter;

		// Token: 0x040092DC RID: 37596
		private ScrollRect m_ScrollRect;

		// Token: 0x040092DD RID: 37597
		private SlidePagerWidget m_SlidePagerWidget;

		// Token: 0x040092DE RID: 37598
		private MDMarkupGraphWidget m_MarkupGraphWidget;

		// Token: 0x040092DF RID: 37599
		private MDMarkupPagerContainer m_ContainerData;

		// Token: 0x040092E0 RID: 37600
		private GameObject m_Footer;

		// Token: 0x040092E1 RID: 37601
		private SelectionButton m_CloseButton;

		// Token: 0x040092E2 RID: 37602
		private SelectionButton m_BackShortcutButton;

		// Token: 0x040092E3 RID: 37603
		private List<int> m_FocusedPages;

		// Token: 0x040092E4 RID: 37604
		public bool titleVisible;

		// Token: 0x040092E5 RID: 37605
		public MDMarkupDef.CloseButtonType closeButtonType;
	}
}
