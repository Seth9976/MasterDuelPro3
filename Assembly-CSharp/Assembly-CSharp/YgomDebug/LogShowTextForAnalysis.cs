using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;

namespace YgomDebug
{
	// Token: 0x02001162 RID: 4450
	public class LogShowTextForAnalysis : LogItemBaseForAnalysis
	{
		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x06008491 RID: 33937 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06008492 RID: 33938 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowTextDataForAnalysis data)
		{
		}

		// Token: 0x0400BFD9 RID: 49113
		public static List<string> m_TextTable;

		// Token: 0x0400BFDA RID: 49114
		protected string LABEL_EO_CONTENT;

		// Token: 0x0400BFDB RID: 49115
		protected string LABEL_EO_COLORBARTEAM0;

		// Token: 0x0400BFDC RID: 49116
		protected string LABEL_EO_COLORBARTEAM1;

		// Token: 0x0400BFDD RID: 49117
		protected string LABEL_EO_TEXT;

		// Token: 0x0400BFDE RID: 49118
		private ElementObjectManager m_EOManager_Origin;
	}
}
