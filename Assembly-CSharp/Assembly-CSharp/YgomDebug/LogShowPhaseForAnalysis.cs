using System;
using YgomSystem.ElementSystem;

namespace YgomDebug
{
	// Token: 0x02001160 RID: 4448
	public class LogShowPhaseForAnalysis : LogItemBaseForAnalysis
	{
		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06008489 RID: 33929 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600848A RID: 33930 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowPhaseDataForAnalysis data)
		{
		}

		// Token: 0x0400BFD4 RID: 49108
		protected string LABEL_EO_PHASETEXT;

		// Token: 0x0400BFD5 RID: 49109
		protected string LABEL_EO_CARDNAME;

		// Token: 0x0400BFD6 RID: 49110
		private ElementObjectManager m_EOManager_Origin;
	}
}
