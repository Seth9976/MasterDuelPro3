using System;
using YgomSystem.ElementSystem;

namespace YgomGame.Duel
{
	// Token: 0x02000EC2 RID: 3778
	public class LogShowPhase : LogItemBase
	{
		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06006E32 RID: 28210 RVA: 0x0000216A File Offset: 0x0000036A
		protected ElementObjectManager m_EOManager
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06006E33 RID: 28211 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetData(ShowPhaseData data)
		{
		}

		// Token: 0x0400A905 RID: 43269
		protected string LABEL_EO_PHASETEXT;

		// Token: 0x0400A906 RID: 43270
		protected string LABEL_EO_CARDNAME;

		// Token: 0x0400A907 RID: 43271
		private ElementObjectManager m_EOManager_Origin;
	}
}
