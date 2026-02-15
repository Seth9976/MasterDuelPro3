using System;

namespace YgomGame.Menu.Common
{
	// Token: 0x02000B34 RID: 2868
	public interface IAsyncProgressContent
	{
		// Token: 0x0600539D RID: 21405
		bool IsDone();

		// Token: 0x0600539E RID: 21406
		void ProgressUpdate();
	}
}
