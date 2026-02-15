using System;
using System.Collections.Generic;

namespace YgomGame.Mission
{
	// Token: 0x02000A38 RID: 2616
	public class MissionSelectorHistory
	{
		// Token: 0x06004C00 RID: 19456 RVA: 0x0000216D File Offset: 0x0000036D
		public void Assign(IMissionSelectorHistoryHandler handler)
		{
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x0000216D File Offset: 0x0000036D
		public void Save()
		{
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TrySelect()
		{
			return false;
		}

		// Token: 0x04008A04 RID: 35332
		private List<IMissionSelectorHistoryHandler> m_Handlers;
	}
}
