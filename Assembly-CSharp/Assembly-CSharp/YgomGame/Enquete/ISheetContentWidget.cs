using System;
using System.Collections.Generic;

namespace YgomGame.Enquete
{
	// Token: 0x02000C1E RID: 3102
	public interface ISheetContentWidget
	{
		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06005885 RID: 22661
		string label { get; }

		// Token: 0x06005886 RID: 22662
		void ImportInputValues(Dictionary<string, object> importValues);

		// Token: 0x06005887 RID: 22663
		void CollectSelectionItems(SheetSelectionItemMap sheetSelectionItemMap);

		// Token: 0x06005888 RID: 22664
		void CollectInputValues(Dictionary<string, object> resultValues);
	}
}
