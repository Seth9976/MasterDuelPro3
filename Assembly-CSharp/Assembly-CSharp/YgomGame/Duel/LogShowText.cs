using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000EC4 RID: 3780
	public class LogShowText : LogItemBase
	{
		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06006E3A RID: 28218 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006E3B RID: 28219 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowTextData data)
		{
		}

		// Token: 0x0400A90A RID: 43274
		public static List<string> m_TextTable;

		// Token: 0x0400A90B RID: 43275
		protected string LABEL_EO_CONTENT;

		// Token: 0x0400A90C RID: 43276
		protected string LABEL_EO_COLORBARTEAM0;

		// Token: 0x0400A90D RID: 43277
		protected string LABEL_EO_COLORBARTEAM1;

		// Token: 0x0400A90E RID: 43278
		protected string LABEL_EO_TEXT;

		// Token: 0x0400A90F RID: 43279
		private ElementObjectManager m_EOManager_Origin;
	}
}
