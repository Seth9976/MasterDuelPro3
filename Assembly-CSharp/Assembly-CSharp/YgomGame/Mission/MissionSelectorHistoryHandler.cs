using System;
using System.Collections.Generic;

namespace YgomGame.Mission
{
	// Token: 0x02000A39 RID: 2617
	public class MissionSelectorHistoryHandler : IMissionSelectorHistoryHandler
	{
		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06004C04 RID: 19460 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isSelected
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x00002739 File Offset: 0x00000939
		public MissionSelectorHistoryHandler(Func<bool> isSelectedFunc, Action<Dictionary<string, object>> saveSelectorHistoryCallback, Func<Dictionary<string, object>, bool> trySelectHistoryFunc)
		{
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetSaveArgs(string key, object value)
		{
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x0000216D File Offset: 0x0000036D
		public void SaveSelectorHistory()
		{
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool TrySelectHistory()
		{
			return false;
		}

		// Token: 0x04008A05 RID: 35333
		private readonly Func<bool> m_IsSelectedFunc;

		// Token: 0x04008A06 RID: 35334
		private readonly Action<Dictionary<string, object>> m_SaveSelectorHistoryCallback;

		// Token: 0x04008A07 RID: 35335
		private readonly Func<Dictionary<string, object>, bool> m_TrySelectHistoryFunc;

		// Token: 0x04008A08 RID: 35336
		private readonly Dictionary<string, object> m_SaveArgs;
	}
}
