using System;
using System.Collections.Generic;
using YgomGame.Utility;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BB3 RID: 2995
	public class MDMarkupHalfBannerMarkupPageWidget : MDMarkupPageWidgetBase
	{
		// Token: 0x06005598 RID: 21912 RVA: 0x000F4CAC File Offset: 0x000F2EAC
		public MDMarkupHalfBannerMarkupPageWidget(ElementObjectManager eom, MDMarkupIndentWidget indentWidget)
			: base(null, null)
		{
		}

		// Token: 0x06005599 RID: 21913 RVA: 0x0000216A File Offset: 0x0000036A
		protected override MDMarkupBannerContext GetBannerContext(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x0600559A RID: 21914 RVA: 0x0000216A File Offset: 0x0000036A
		protected override GlobalTextData GetCaptionText(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x0600559B RID: 21915 RVA: 0x0000216A File Offset: 0x0000036A
		protected override GlobalTextData GetText(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x0600559C RID: 21916 RVA: 0x0000216A File Offset: 0x0000036A
		protected override List<URLSchemeButton> GetButtons(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}

		// Token: 0x0600559D RID: 21917 RVA: 0x0000216A File Offset: 0x0000036A
		protected override List<IMDMarkupContent> GetMarkupContents(IMDMarkupContent mdMarkupContent)
		{
			return null;
		}
	}
}
