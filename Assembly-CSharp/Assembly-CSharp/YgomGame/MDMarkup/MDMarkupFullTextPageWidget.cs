using System;
using System.Collections.Generic;
using YgomGame.Utility;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BAF RID: 2991
	public class MDMarkupFullTextPageWidget : MDMarkupPageWidgetBase
	{
		// Token: 0x0600557C RID: 21884 RVA: 0x000F4CAC File Offset: 0x000F2EAC
		public MDMarkupFullTextPageWidget(ElementObjectManager eom, MDMarkupIndentWidget indentWidget)
			: base(null, null)
		{
		}

		// Token: 0x0600557D RID: 21885 RVA: 0x0000216A File Offset: 0x0000036A
		protected override GlobalTextData GetCaptionText(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x0600557E RID: 21886 RVA: 0x0000216A File Offset: 0x0000036A
		protected override GlobalTextData GetText(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x0600557F RID: 21887 RVA: 0x0000216A File Offset: 0x0000036A
		protected override List<URLSchemeButton> GetButtons(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}
	}
}
