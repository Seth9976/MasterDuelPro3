using System;
using System.Collections.Generic;
using YgomGame.Utility;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BB5 RID: 2997
	public class MDMarkupHalfImageTextPageWidget : MDMarkupPageWidgetBase
	{
		// Token: 0x060055A4 RID: 21924 RVA: 0x000F4CAC File Offset: 0x000F2EAC
		public MDMarkupHalfImageTextPageWidget(ElementObjectManager eom, MDMarkupIndentWidget indentWidget)
			: base(null, null)
		{
		}

		// Token: 0x060055A5 RID: 21925 RVA: 0x0000216A File Offset: 0x0000036A
		protected override GlobalTextData GetCaptionText(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x060055A6 RID: 21926 RVA: 0x0000216A File Offset: 0x0000036A
		protected override GlobalTextData GetText(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x060055A7 RID: 21927 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string GetResourcePath(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x060055A8 RID: 21928 RVA: 0x0000216A File Offset: 0x0000036A
		protected override List<URLSchemeButton> GetButtons(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}
	}
}
