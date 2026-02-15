using System;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B77 RID: 2935
	public interface IMDMarkupItemWidget : IMDMarkupButtonWidget
	{
		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06005487 RID: 21639
		bool itemIsPeriod { get; }

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06005488 RID: 21640
		int itemCategory { get; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06005489 RID: 21641
		int itemId { get; }
	}
}
