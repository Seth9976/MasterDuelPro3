using System;

namespace YgomGame.MDMarkup
{
	// Token: 0x02000B6C RID: 2924
	public interface IMDMarkupAsyncWidget
	{
		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x0600546C RID: 21612
		bool isReady { get; }

		// Token: 0x0600546D RID: 21613
		void OnReady();
	}
}
