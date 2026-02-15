using System;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BC0 RID: 3008
	public class MDMarkupPagerFactory : MDMarkupWidgetFactoryBase
	{
		// Token: 0x060055D4 RID: 21972 RVA: 0x000F4CB6 File Offset: 0x000F2EB6
		public MDMarkupPagerFactory(ElementObjectManager template, MDMarkupDef.MarkupType markupType)
		{
		}

		// Token: 0x060055D5 RID: 21973 RVA: 0x0000216A File Offset: 0x0000036A
		public override IMDMarkupWidget CreateChild(MDMarkupIndentWidget indentWidget)
		{
			return null;
		}

		// Token: 0x040092E6 RID: 37606
		private readonly ElementObjectManager m_Template;

		// Token: 0x040092E7 RID: 37607
		private readonly MDMarkupDef.MarkupType markupType;
	}
}
