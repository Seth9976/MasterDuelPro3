using System;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BD3 RID: 3027
	public abstract class MDMarkupWidgetFactoryBase : IMDMarkupWidgetFactory
	{
		// Token: 0x0600564E RID: 22094
		public abstract IMDMarkupWidget CreateChild(MDMarkupIndentWidget indentWidget);
	}
}
