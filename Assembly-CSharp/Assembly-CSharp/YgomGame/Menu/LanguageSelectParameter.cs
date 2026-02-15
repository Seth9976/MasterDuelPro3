using System;
using YgomGame.TextIDs;

namespace YgomGame.Menu
{
	// Token: 0x02000AA1 RID: 2721
	public class LanguageSelectParameter
	{
		// Token: 0x04008D3A RID: 36154
		public IDS_SYS decisionButtonTextID;

		// Token: 0x04008D3B RID: 36155
		public bool disableBackButton;

		// Token: 0x04008D3C RID: 36156
		public int selectorPriority;

		// Token: 0x04008D3D RID: 36157
		public Action<string> resultCallback;
	}
}
