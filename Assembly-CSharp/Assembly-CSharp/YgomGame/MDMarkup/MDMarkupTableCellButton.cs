using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine.Events;
using YgomSystem.ElementSystem;
using YgomSystem.UI;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BC4 RID: 3012
	public class MDMarkupTableCellButton : ElementWidgetBase, IMDMarkupLinkWidget, IMDMarkupButtonWidget, IMDMarkupTMPWidget
	{
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x060055E4 RID: 21988 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060055E5 RID: 21989 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x060055E6 RID: 21990 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060055E7 RID: 21991 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x060055E8 RID: 21992 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060055E9 RID: 21993 RVA: 0x0000216D File Offset: 0x0000036D
		public string link
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

		// Token: 0x060055EA RID: 21994 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MDMarkupTableCellButton(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060055EB RID: 21995 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetText(string text)
		{
		}

		// Token: 0x060055EC RID: 21996 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetLink(string link)
		{
		}

		// Token: 0x060055ED RID: 21997 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAlignment(TextAlignmentOptions alignment)
		{
		}

		// Token: 0x060055EE RID: 21998 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSizeRate(float sizeRate)
		{
		}

		// Token: 0x060055EF RID: 21999 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOnClick(UnityAction callback)
		{
		}

		// Token: 0x060055F0 RID: 22000 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddContainTMPTexts(List<TMP_Text> results)
		{
		}

		// Token: 0x040092F0 RID: 37616
		private readonly string k_ELabelButton;

		// Token: 0x040092F1 RID: 37617
		private readonly string k_ELabelText;

		// Token: 0x040092F2 RID: 37618
		public readonly TMP_Text text;
	}
}
