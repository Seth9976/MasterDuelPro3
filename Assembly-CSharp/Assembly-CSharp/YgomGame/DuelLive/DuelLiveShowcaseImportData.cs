using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.DuelLive
{
	// Token: 0x02000C59 RID: 3161
	[Serializable]
	public class DuelLiveShowcaseImportData
	{
		// Token: 0x06005A42 RID: 23106 RVA: 0x0000216A File Offset: 0x0000036A
		public DuelLiveImportSettingData[] GetSettingData()
		{
			return null;
		}

		// Token: 0x06005A43 RID: 23107 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<string, object> GetReplayData()
		{
			return null;
		}

		// Token: 0x06005A44 RID: 23108 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddData(DuelLiveImportSettingData data)
		{
		}

		// Token: 0x040095BF RID: 38335
		[SerializeField]
		private DuelLiveImportSettingData[] settingData;

		// Token: 0x040095C0 RID: 38336
		private Dictionary<string, object> m_Data;
	}
}
