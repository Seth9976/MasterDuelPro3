using System;
using System.Collections.Generic;

namespace YgomGame.Help
{
	// Token: 0x02000BF3 RID: 3059
	[Serializable]
	public class HelpSectionData
	{
		// Token: 0x060056DC RID: 22236 RVA: 0x0000216A File Offset: 0x0000036A
		public HelpRecordData GetRecordData(string recordLabel)
		{
			return null;
		}

		// Token: 0x04009397 RID: 37783
		public string label;

		// Token: 0x04009398 RID: 37784
		public string titleTid;

		// Token: 0x04009399 RID: 37785
		public List<HelpRecordData> records;

		// Token: 0x0400939A RID: 37786
		private Dictionary<string, HelpRecordData> m_RecordMap;
	}
}
