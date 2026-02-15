using System;
using System.Collections.Generic;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B71 RID: 2929
	public interface IMDMarkupContainerWidget
	{
		// Token: 0x0600547B RID: 21627
		void Initialize(IMDMarkupContainer containerData);

		// Token: 0x0600547C RID: 21628
		void Output(MDMarkupGraphFactory graphFactory, Action onComplete);

		// Token: 0x0600547D RID: 21629
		void OnStart(Dictionary<string, object> args);
	}
}
