using System;
using YgomSystem.ElementSystem;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000BC6 RID: 3014
	public class MDMarkupTableCellFactory
	{
		// Token: 0x060055FE RID: 22014 RVA: 0x00002739 File Offset: 0x00000939
		public MDMarkupTableCellFactory(ElementObjectManager normalTemplate, ElementObjectManager headerTemplate, ElementObjectManager imageTemplate, ElementObjectManager cardTemplate, ElementObjectManager itemTemplate, ElementObjectManager bannerTemplate, ElementObjectManager buttonSTemplate, ElementObjectManager buttonMTemplate, ElementObjectManager buttonLTemplate)
		{
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x0000216A File Offset: 0x0000036A
		private ElementObjectManager GetTextCellTemplate(MDMarkupDef.TableRowStyle rowStyle)
		{
			return null;
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x0000216A File Offset: 0x0000036A
		public MDMarkupTableCellText CreateTextCell(MDMarkupTableRow parentRow, MDMarkupDef.TableRowStyle rowStyle)
		{
			return null;
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x0000216A File Offset: 0x0000036A
		public MDMarkupTableCellImage CreateImageCell(MDMarkupTableRow parentRow)
		{
			return null;
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x0000216A File Offset: 0x0000036A
		public MDMarkupTableCellCard CreateCardCell(MDMarkupTableRow parentRow)
		{
			return null;
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x0000216A File Offset: 0x0000036A
		public MDMarkupTableCellItem CreateItemCell(MDMarkupTableRow parentRow)
		{
			return null;
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x0000216A File Offset: 0x0000036A
		public MDMarkupTableCellBanner CreateBannerCell(MDMarkupTableRow parentRow)
		{
			return null;
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x0000216A File Offset: 0x0000036A
		public MDMarkupTableCellButton CreateButtonCell(MDMarkupTableRow parentRow, MDMarkupDef.ButtonStyle buttonStyle)
		{
			return null;
		}

		// Token: 0x040092FA RID: 37626
		private readonly ElementObjectManager m_NormalTextTemplate;

		// Token: 0x040092FB RID: 37627
		private readonly ElementObjectManager m_HeaderTextTemplate;

		// Token: 0x040092FC RID: 37628
		private readonly ElementObjectManager m_ImageTemplate;

		// Token: 0x040092FD RID: 37629
		private readonly ElementObjectManager m_CardTemplate;

		// Token: 0x040092FE RID: 37630
		private readonly ElementObjectManager m_ItemTemplate;

		// Token: 0x040092FF RID: 37631
		private readonly ElementObjectManager m_BannerTemplate;

		// Token: 0x04009300 RID: 37632
		private readonly ElementObjectManager m_ButtonSTemplate;

		// Token: 0x04009301 RID: 37633
		private readonly ElementObjectManager m_ButtonMTemplate;

		// Token: 0x04009302 RID: 37634
		private readonly ElementObjectManager m_ButtonLTemplate;
	}
}
