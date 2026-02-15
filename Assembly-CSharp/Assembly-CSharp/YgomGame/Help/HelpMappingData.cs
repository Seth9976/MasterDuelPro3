using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Help
{
	// Token: 0x02000BF1 RID: 3057
	[Serializable]
	public class HelpMappingData
	{
		// Token: 0x060056D5 RID: 22229 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GetAsync(Action<HelpMappingData> onComplete)
		{
		}

		// Token: 0x060056D6 RID: 22230 RVA: 0x0000216A File Offset: 0x0000036A
		public static HelpMappingData FromJson(string json)
		{
			return null;
		}

		// Token: 0x060056D7 RID: 22231 RVA: 0x0000216A File Offset: 0x0000036A
		public string ToJson()
		{
			return null;
		}

		// Token: 0x060056D8 RID: 22232 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool ValidHelpPath(string fullPath)
		{
			return false;
		}

		// Token: 0x060056D9 RID: 22233 RVA: 0x0000216A File Offset: 0x0000036A
		public HelpSectionData GetSectionData(string groupLabel, string sectionLabel)
		{
			return null;
		}

		// Token: 0x04009391 RID: 37777
		public const string k_JsonPath = "Help/HelpMapping";

		// Token: 0x04009392 RID: 37778
		[SerializeField]
		public List<HelpSectionGroupData> groups;

		// Token: 0x04009393 RID: 37779
		private Dictionary<string, HelpSectionGroupData> m_SectionGroupMap;
	}
}
