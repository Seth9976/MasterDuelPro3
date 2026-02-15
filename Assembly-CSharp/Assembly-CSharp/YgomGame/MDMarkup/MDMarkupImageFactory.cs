using System;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BB8 RID: 3000
	public class MDMarkupImageFactory : MDMarkupWidgetFactoryBase
	{
		// Token: 0x060055AD RID: 21933 RVA: 0x000F4CB6 File Offset: 0x000F2EB6
		public MDMarkupImageFactory(ElementObjectManager template)
		{
		}

		// Token: 0x060055AE RID: 21934 RVA: 0x0000216A File Offset: 0x0000036A
		public override IMDMarkupWidget CreateChild(MDMarkupIndentWidget indentWidget)
		{
			return null;
		}

		// Token: 0x040092C3 RID: 37571
		private readonly ElementObjectManager m_Template;
	}
}
