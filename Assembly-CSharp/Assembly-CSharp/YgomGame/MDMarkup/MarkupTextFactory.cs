using System;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BD4 RID: 3028
	public class MarkupTextFactory : MDMarkupWidgetFactoryBase
	{
		// Token: 0x06005650 RID: 22096 RVA: 0x000F4CB6 File Offset: 0x000F2EB6
		public MarkupTextFactory(ElementObjectManager template)
		{
		}

		// Token: 0x06005651 RID: 22097 RVA: 0x0000216A File Offset: 0x0000036A
		public override IMDMarkupWidget CreateChild(MDMarkupIndentWidget indentWidget)
		{
			return null;
		}

		// Token: 0x04009331 RID: 37681
		private readonly ElementObjectManager m_Template;
	}
}
