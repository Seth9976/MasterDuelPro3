using System;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BC1 RID: 3009
	public class MDMarkupPrefabWidget : MDMarkupWidgetBase
	{
		// Token: 0x060055D6 RID: 21974 RVA: 0x000F4CC8 File Offset: 0x000F2EC8
		public MDMarkupPrefabWidget(MDMarkupIndentWidget indentWidget, MDMarkupPrefabsFactory prefabsFactory)
			: base(null, null)
		{
		}

		// Token: 0x060055D7 RID: 21975 RVA: 0x0000216D File Offset: 0x0000036D
		public override void BindContentData(IMDMarkupContent contentData)
		{
		}

		// Token: 0x040092E8 RID: 37608
		private readonly MDMarkupPrefabsFactory m_PrefabsFactory;
	}
}
