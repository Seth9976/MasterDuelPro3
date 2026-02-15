using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BC5 RID: 3013
	public class MDMarkupTableCellCard : ElementWidgetBase, IMDMarkupCardWidget, IMDMarkupButtonWidget
	{
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060055F1 RID: 22001 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060055F2 RID: 22002 RVA: 0x0000216D File Offset: 0x0000036D
		public bool borderVisible
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060055F3 RID: 22003 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060055F4 RID: 22004 RVA: 0x0000216D File Offset: 0x0000036D
		public int cardMrk
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060055F5 RID: 22005 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060055F6 RID: 22006 RVA: 0x0000216D File Offset: 0x0000036D
		public int cardPremire
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000856 RID: 2134
		// (get) Token: 0x060055F7 RID: 22007 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060055F8 RID: 22008 RVA: 0x0000216D File Offset: 0x0000036D
		public SelectionButton button
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MDMarkupTableCellCard(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetCard(int mrk, int premire, MDMarkupDef.CardSize cardSize, float overrideHeight = 0f)
		{
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAlignment(TextAlignmentOptions alignment)
		{
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSizeRate(float sizeRate)
		{
		}

		// Token: 0x060055FD RID: 22013 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClick(UnityAction callback)
		{
		}

		// Token: 0x040092F3 RID: 37619
		private readonly string k_ELabelButton;

		// Token: 0x040092F4 RID: 37620
		private readonly string k_ELabelRoot;

		// Token: 0x040092F5 RID: 37621
		private readonly string k_ELabelImage;

		// Token: 0x040092F6 RID: 37622
		public readonly LayoutElement m_LayoutElement;

		// Token: 0x040092F7 RID: 37623
		public readonly RectTransform m_Root;

		// Token: 0x040092F8 RID: 37624
		public readonly AspectRatioFitter m_AspectRatioFitter;

		// Token: 0x040092F9 RID: 37625
		public readonly RawImage m_CardImage;
	}
}
