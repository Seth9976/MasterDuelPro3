using System;
using System.Collections.Generic;
using TMPro;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BC9 RID: 3017
	public class MDMarkupTableCellText : ElementWidgetBase, IMDMarkupTMPWidget
	{
		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x0600561F RID: 22047 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06005620 RID: 22048 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x06005621 RID: 22049 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public MDMarkupTableCellText(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetAlignment(TextAlignmentOptions alignment)
		{
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetText(string text)
		{
		}

		// Token: 0x06005624 RID: 22052 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSizeRate(float sizeRate)
		{
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddContainTMPTexts(List<TMP_Text> results)
		{
		}

		// Token: 0x04009310 RID: 37648
		private readonly string k_ELabelText;

		// Token: 0x04009311 RID: 37649
		public readonly TextMeshProUGUI text;
	}
}
