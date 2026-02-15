using System;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B7B RID: 2939
	public interface IMDMarkupPageContent : IMDMarkupContent
	{
		// Token: 0x1400007C RID: 124
		// (add) Token: 0x0600548D RID: 21645
		// (remove) Token: 0x0600548E RID: 21646
		event Action<bool> onFocusPageEvent;

		// Token: 0x0600548F RID: 21647
		void InvokeOnFocusPageEvent(bool isFirst);
	}
}
