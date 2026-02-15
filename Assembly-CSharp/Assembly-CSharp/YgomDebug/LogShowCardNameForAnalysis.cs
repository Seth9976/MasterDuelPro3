using System;
using YgomSystem.ElementSystem;

namespace YgomDebug
{
	// Token: 0x0200115E RID: 4446
	public class LogShowCardNameForAnalysis : LogItemBaseForAnalysis
	{
		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06008482 RID: 33922 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06008483 RID: 33923 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowCardNameDataForAnalysis data)
		{
		}

		// Token: 0x0400BFCA RID: 49098
		protected string LABEL_EO_CONTENT;

		// Token: 0x0400BFCB RID: 49099
		protected string LABEL_EO_COLORBARTEAM0;

		// Token: 0x0400BFCC RID: 49100
		protected string LABEL_EO_COLORBARTEAM1;

		// Token: 0x0400BFCD RID: 49101
		protected string LABEL_EO_CARDNAME;

		// Token: 0x0400BFCE RID: 49102
		private ElementObjectManager m_EOManager_Origin;
	}
}
