using System;
using System.Collections.Generic;
using YgomSystem.UI;

namespace YgomGame.Enquete
{
	// Token: 0x02000C32 RID: 3122
	public class SheetSelectionItemMap
	{
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060058F2 RID: 22770 RVA: 0x0000216A File Offset: 0x0000036A
		public IReadOnlyList<IReadOnlyList<SelectionItem>> selectionItems
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060058F3 RID: 22771 RVA: 0x0000216D File Offset: 0x0000036D
		public void AppendItem(SelectionItem selectionItems)
		{
		}

		// Token: 0x060058F4 RID: 22772 RVA: 0x0000216D File Offset: 0x0000036D
		public void AppendItemLine(SelectionItem selectionItems)
		{
		}

		// Token: 0x060058F5 RID: 22773 RVA: 0x0000216D File Offset: 0x0000036D
		public void AppendItemLine(List<SelectionItem> selectionItems)
		{
		}

		// Token: 0x060058F6 RID: 22774 RVA: 0x000F4D30 File Offset: 0x000F2F30
		public ValueTuple<bool, int, int> SearchItemIdx(SelectionItem searchItem)
		{
			return default(ValueTuple<bool, int, int>);
		}

		// Token: 0x060058F7 RID: 22775 RVA: 0x0000216A File Offset: 0x0000036A
		public SelectionItem SearchItem(int xIdx, int yIdx)
		{
			return null;
		}

		// Token: 0x060058F8 RID: 22776 RVA: 0x0000216A File Offset: 0x0000036A
		public List<SelectionItem> GetAllSelectionItems()
		{
			return null;
		}

		// Token: 0x040094F3 RID: 38131
		private readonly List<List<SelectionItem>> m_SelectionItems;

		// Token: 0x040094F4 RID: 38132
		private List<SelectionItem> m_SelectionItemSearchList;
	}
}
