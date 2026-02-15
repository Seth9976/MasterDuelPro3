using System;
using System.Collections.Generic;
using TMPro;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BCD RID: 3021
	public class MDMarkupTableWidget : MDMarkupWidgetBase, IMDMarkupCardContainWidget, IMDMarkupItemContainWidget, IMDMarkupLinkContainWidget, IMDMarkupLayoutWidget, IMDMarkupTMPWidget
	{
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x0600562F RID: 22063 RVA: 0x0000216A File Offset: 0x0000036A
		public float[] colSizes
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06005630 RID: 22064 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool isReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06005631 RID: 22065 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<IMDMarkupCardWidget> cardWidgets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06005632 RID: 22066 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<IMDMarkupItemWidget> itemWidgets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06005633 RID: 22067 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<IMDMarkupLinkWidget> linkWidgets
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x000F4CC8 File Offset: 0x000F2EC8
		public MDMarkupTableWidget(ElementObjectManager eom, MDMarkupIndentWidget indentWidget, MDMarkupTableRowFactory rowFactory, MDMarkupTableCellFactory cellFactory)
			: base(null, null)
		{
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x0000216D File Offset: 0x0000036D
		public override void BindContentData(IMDMarkupContent mdMarkupContent)
		{
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x0000216D File Offset: 0x0000036D
		public override void OnReady()
		{
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnConcreatedLayout()
		{
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddContainTMPTexts(List<TMP_Text> results)
		{
		}

		// Token: 0x04009317 RID: 37655
		private readonly MDMarkupTableRowFactory m_RowFactory;

		// Token: 0x04009318 RID: 37656
		private readonly MDMarkupTableCellFactory m_CellFactory;

		// Token: 0x04009319 RID: 37657
		private readonly List<IMDMarkupTMPWidget> m_TMPWidgets;

		// Token: 0x0400931A RID: 37658
		private readonly List<IMDMarkupAsyncWidget> m_AsyncWidgets;

		// Token: 0x0400931B RID: 37659
		private readonly List<IMDMarkupLayoutWidget> m_LayoutWidgets;

		// Token: 0x0400931C RID: 37660
		private float[] m_ColSizes;

		// Token: 0x0400931D RID: 37661
		public List<IMDMarkupCardWidget> m_CardWidgets;

		// Token: 0x0400931E RID: 37662
		public List<IMDMarkupItemWidget> m_ItemWidgets;

		// Token: 0x0400931F RID: 37663
		public List<IMDMarkupLinkWidget> m_LinkWidgets;
	}
}
