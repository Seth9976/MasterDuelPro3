using System;
using System.Collections.Generic;
using YgomSystem.UI;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B7C RID: 2940
	public interface IMDMarkupPageWidget
	{
		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06005490 RID: 21648
		List<SelectionButton> buttons { get; }

		// Token: 0x1400007D RID: 125
		// (add) Token: 0x06005491 RID: 21649
		// (remove) Token: 0x06005492 RID: 21650
		event Action<bool> onFocusPageEvent;
	}
}
