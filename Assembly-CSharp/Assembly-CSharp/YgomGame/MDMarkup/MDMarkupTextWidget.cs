using System;
using System.Collections.Generic;
using TMPro;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BD0 RID: 3024
	public class MDMarkupTextWidget : MDMarkupWidgetBase, IMDMarkupTMPWidget
	{
		// Token: 0x06005643 RID: 22083 RVA: 0x000F4CC8 File Offset: 0x000F2EC8
		public MDMarkupTextWidget(ElementObjectManager eom, MDMarkupIndentWidget indentWidget)
			: base(null, null)
		{
		}

		// Token: 0x06005644 RID: 22084 RVA: 0x0000216D File Offset: 0x0000036D
		public override void BindContentData(IMDMarkupContent contentData)
		{
		}

		// Token: 0x06005645 RID: 22085 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddContainTMPTexts(List<TMP_Text> results)
		{
		}

		// Token: 0x0400932E RID: 37678
		private readonly string k_ELabelText;

		// Token: 0x0400932F RID: 37679
		public readonly TextMeshProUGUI text;
	}
}
