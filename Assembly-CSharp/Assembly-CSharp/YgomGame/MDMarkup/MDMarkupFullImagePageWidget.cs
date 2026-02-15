using System;
using System.Collections.Generic;
using YgomGame.Utility;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BAE RID: 2990
	public class MDMarkupFullImagePageWidget : MDMarkupPageWidgetBase
	{
		// Token: 0x06005578 RID: 21880 RVA: 0x000F4CAC File Offset: 0x000F2EAC
		public MDMarkupFullImagePageWidget(ElementObjectManager eom, MDMarkupIndentWidget indentWidget)
			: base(null, null)
		{
		}

		// Token: 0x06005579 RID: 21881 RVA: 0x0000216A File Offset: 0x0000036A
		protected override GlobalTextData GetCaptionText(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x0600557A RID: 21882 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string GetResourcePath(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x0600557B RID: 21883 RVA: 0x0000216A File Offset: 0x0000036A
		protected override List<URLSchemeButton> GetButtons(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}
	}
}
