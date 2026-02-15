using System;

namespace YgomGame.Dialog.CommonDialog
{
	// Token: 0x02000F85 RID: 3973
	public interface IContentPostAllInsertedHandler
	{
		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06007495 RID: 29845
		bool rebuildLayoutOnPostAllInserted { get; }

		// Token: 0x06007496 RID: 29846
		void OnPostAllInserted();
	}
}
