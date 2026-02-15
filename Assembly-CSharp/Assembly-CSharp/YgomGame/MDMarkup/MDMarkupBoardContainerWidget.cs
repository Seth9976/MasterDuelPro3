using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;
using YgomSystem.UI.FreeScroll;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B8A RID: 2954
	public class MDMarkupBoardContainerWidget : ElementWidgetBase, IMDMarkupContainerWidget
	{
		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060054C7 RID: 21703 RVA: 0x0000216A File Offset: 0x0000036A
		public ScrollRect scrollRect
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x060054C8 RID: 21704 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool existsFooter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x060054C9 RID: 21705 RVA: 0x0000216A File Offset: 0x0000036A
		public TextMeshProUGUI titleText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x060054CA RID: 21706 RVA: 0x0000216A File Offset: 0x0000036A
		public GameObject badge
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x060054CB RID: 21707 RVA: 0x0000216A File Offset: 0x0000036A
		public TextMeshProUGUI optionalText
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060054CC RID: 21708 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MDMarkupBoardContainerWidget(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(IMDMarkupContainer containerData)
		{
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x0000216D File Offset: 0x0000036D
		public void Output(MDMarkupGraphFactory graphFactory, Action onComplete)
		{
		}

		// Token: 0x060054CF RID: 21711 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnStart(Dictionary<string, object> args)
		{
		}

		// Token: 0x0400920D RID: 37389
		private readonly string k_ELabelTitleText;

		// Token: 0x0400920E RID: 37390
		private readonly string k_ELabelBadge;

		// Token: 0x0400920F RID: 37391
		private readonly string k_ELabelOptionalText;

		// Token: 0x04009210 RID: 37392
		private readonly string k_ELabelScrollView;

		// Token: 0x04009211 RID: 37393
		private readonly string k_ELabelFooter;

		// Token: 0x04009212 RID: 37394
		private MDMarkupBoardContainerWidget.Footer m_Footer;

		// Token: 0x04009213 RID: 37395
		private ScrollRect m_ScrollRect;

		// Token: 0x04009214 RID: 37396
		private FreeScrollView m_FreeScrollView;

		// Token: 0x04009215 RID: 37397
		private MDMarkupBoardContainer m_ContainerData;

		// Token: 0x02000B8B RID: 2955
		private class Footer : ElementWidgetBase
		{
			// Token: 0x060054D0 RID: 21712 RVA: 0x000F2C76 File Offset: 0x000F0E76
			public Footer(ElementObjectManager eom)
				: base(null)
			{
			}

			// Token: 0x060054D1 RID: 21713 RVA: 0x0000216A File Offset: 0x0000036A
			public SelectionButton InsertButton()
			{
				return null;
			}

			// Token: 0x060054D2 RID: 21714 RVA: 0x0000216A File Offset: 0x0000036A
			public TMP_Text InsertText()
			{
				return null;
			}

			// Token: 0x04009216 RID: 37398
			private readonly SelectionButton m_ButtonTemplate;

			// Token: 0x04009217 RID: 37399
			private readonly TMP_Text m_TextTemplate;
		}
	}
}
