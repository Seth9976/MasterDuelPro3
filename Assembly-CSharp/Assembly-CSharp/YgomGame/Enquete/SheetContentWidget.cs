using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Enquete
{
	// Token: 0x02000C30 RID: 3120
	public class SheetContentWidget : ElementWidgetBase
	{
		// Token: 0x170008FE RID: 2302
		// (get) Token: 0x060058E8 RID: 22760 RVA: 0x0000216A File Offset: 0x0000036A
		private string YgomGame_002EEnquete_002EISheetContentWidget_002Elabel
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060058E9 RID: 22761 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public SheetContentWidget(ElementObjectManager eom, string label)
			: base(null)
		{
		}

		// Token: 0x060058EA RID: 22762 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void ImportInputValues(Dictionary<string, object> importValues)
		{
		}

		// Token: 0x060058EB RID: 22763 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void CollectSelectionItems(SheetSelectionItemMap sheetSelectionItemMap)
		{
		}

		// Token: 0x060058EC RID: 22764 RVA: 0x0000216D File Offset: 0x0000036D
		public virtual void CollectInputValues(Dictionary<string, object> resultValues)
		{
		}

		// Token: 0x040094F0 RID: 38128
		public string label;
	}
}
