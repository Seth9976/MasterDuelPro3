using System;
using System.Collections.Generic;

namespace YgomGame.Help
{
	// Token: 0x02000BF4 RID: 3060
	[Serializable]
	public class HelpSectionGroupData
	{
		// Token: 0x060056DE RID: 22238 RVA: 0x0000216A File Offset: 0x0000036A
		public HelpSectionData GetSectionData(string sectionLabel)
		{
			return null;
		}

		// Token: 0x0400939B RID: 37787
		public string label;

		// Token: 0x0400939C RID: 37788
		public string titleTid;

		// Token: 0x0400939D RID: 37789
		public bool visibleFromHelp;

		// Token: 0x0400939E RID: 37790
		public List<HelpSectionData> sections;

		// Token: 0x0400939F RID: 37791
		private Dictionary<string, HelpSectionData> m_SectionMap;
	}
}
