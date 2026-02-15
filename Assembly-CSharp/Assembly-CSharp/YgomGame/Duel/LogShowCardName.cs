using System;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000EC0 RID: 3776
	public class LogShowCardName : LogItemBase
	{
		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06006E2B RID: 28203 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006E2C RID: 28204 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowCardNameData data)
		{
		}

		// Token: 0x0400A8FB RID: 43259
		protected string LABEL_EO_CONTENT;

		// Token: 0x0400A8FC RID: 43260
		protected string LABEL_EO_COLORBARTEAM0;

		// Token: 0x0400A8FD RID: 43261
		protected string LABEL_EO_COLORBARTEAM1;

		// Token: 0x0400A8FE RID: 43262
		protected string LABEL_EO_CARDNAME;

		// Token: 0x0400A8FF RID: 43263
		private ElementObjectManager m_EOManager_Origin;
	}
}
