using System;
using System.Collections.Generic;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B6E RID: 2926
	public interface IMDMarkupCardContainWidget
	{
		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06005470 RID: 21616
		IReadOnlyList<IMDMarkupCardWidget> cardWidgets { get; }
	}
}
