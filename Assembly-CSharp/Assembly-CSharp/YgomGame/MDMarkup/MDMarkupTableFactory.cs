using System;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BCA RID: 3018
	public class MDMarkupTableFactory : MDMarkupWidgetFactoryBase
	{
		// Token: 0x06005626 RID: 22054 RVA: 0x000F4CB6 File Offset: 0x000F2EB6
		public MDMarkupTableFactory(ElementObjectManager template, ElementObjectManager rowTemplate, ElementObjectManager cellHeaderTemplate, ElementObjectManager cellNormalTemplate, ElementObjectManager cellImageTemplate, ElementObjectManager cellCardTemplate, ElementObjectManager cellItemTemplate, ElementObjectManager bannerTemplate, ElementObjectManager buttonSTemplate, ElementObjectManager buttonMTemplate, ElementObjectManager buttonLTemplate)
		{
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x0000216A File Offset: 0x0000036A
		public override IMDMarkupWidget CreateChild(MDMarkupIndentWidget indentWidget)
		{
			return null;
		}

		// Token: 0x04009312 RID: 37650
		private readonly ElementObjectManager m_Template;

		// Token: 0x04009313 RID: 37651
		private readonly MDMarkupTableRowFactory m_RowFactory;

		// Token: 0x04009314 RID: 37652
		private readonly MDMarkupTableCellFactory m_CellFactory;
	}
}
