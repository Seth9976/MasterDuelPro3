using System;
using System.Collections.Generic;

namespace YgomSystem.Analyze
{
	// Token: 0x02000797 RID: 1943
	public class AnalyzeHistorySender
	{
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06003C79 RID: 15481 RVA: 0x0000216A File Offset: 0x0000036A
		private static AnalyzeHistorySender instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddCount(HistoryIDs historyId, int recordId = 0, int count = 1)
		{
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> Pop()
		{
			return null;
		}

		// Token: 0x04003500 RID: 13568
		private readonly int k_SendLimit;

		// Token: 0x04003501 RID: 13569
		private static AnalyzeHistorySender s_Instance;

		// Token: 0x04003502 RID: 13570
		private Dictionary<HistoryIDs, Dictionary<int, int>> m_HistoryMap;
	}
}
