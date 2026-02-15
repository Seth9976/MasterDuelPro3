using System;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BB6 RID: 2998
	public class MDMarkupHeaderFactory : MDMarkupWidgetFactoryBase
	{
		// Token: 0x060055A9 RID: 21929 RVA: 0x000F4CB6 File Offset: 0x000F2EB6
		public MDMarkupHeaderFactory(ElementObjectManager template)
		{
		}

		// Token: 0x060055AA RID: 21930 RVA: 0x0000216A File Offset: 0x0000036A
		public override IMDMarkupWidget CreateChild(MDMarkupIndentWidget indentWidget)
		{
			return null;
		}

		// Token: 0x040092C2 RID: 37570
		private readonly ElementObjectManager m_Template;
	}
}
