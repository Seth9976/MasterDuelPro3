using System;
using System.Collections.Generic;

namespace YgomGame.Mission
{
	// Token: 0x02000A44 RID: 2628
	public class TabContextImportable : TabContext
	{
		// Token: 0x06004C96 RID: 19606 RVA: 0x000F4A3A File Offset: 0x000F2C3A
		public TabContextImportable(MissionTabType tabType, int campaignPoolId = 0, long campaignBeginTs = 0L, string tabNameTextId = null, string tabShortNameTextId = null)
			: base(MissionTabType.All, 0, 0L, null, null)
		{
		}

		// Token: 0x06004C97 RID: 19607 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportOverride(List<Dictionary<string, object>> masters, List<Dictionary<string, object>> datas)
		{
		}

		// Token: 0x06004C98 RID: 19608 RVA: 0x0000216D File Offset: 0x0000036D
		public void ImportOverride(Dictionary<string, object> master, Dictionary<string, object> data)
		{
		}

		// Token: 0x06004C99 RID: 19609 RVA: 0x0000216D File Offset: 0x0000036D
		private void InnerImportOverride(Dictionary<string, object> master, Dictionary<string, object> data)
		{
		}
	}
}
