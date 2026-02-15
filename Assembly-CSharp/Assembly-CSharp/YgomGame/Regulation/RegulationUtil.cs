using System;
using System.Collections.Generic;

namespace YgomGame.Regulation
{
	// Token: 0x02000A0D RID: 2573
	public static class RegulationUtil
	{
		// Token: 0x06004ABC RID: 19132 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetRegulationName(int regulationID)
		{
			return null;
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x0000216D File Offset: 0x0000036D
		private static void LoadRegulationNames(object value)
		{
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetNotificator()
		{
		}

		// Token: 0x040088E9 RID: 35049
		internal const string UNKNOWN_REGULATION_NAME = "unknown";

		// Token: 0x040088EA RID: 35050
		private static Dictionary<int, string> c_cacheRegIDtoNameDic;
	}
}
