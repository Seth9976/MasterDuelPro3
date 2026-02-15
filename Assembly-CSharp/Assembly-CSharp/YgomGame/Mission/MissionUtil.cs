using System;
using System.Collections.Generic;

namespace YgomGame.Mission
{
	// Token: 0x02000A3D RID: 2621
	public static class MissionUtil
	{
		// Token: 0x06004C16 RID: 19478 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsResidentTab(this MissionTabType tabType)
		{
			return false;
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ExistsUnlockResidentTab(this MissionTabType tabType)
		{
			return false;
		}

		// Token: 0x06004C18 RID: 19480 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetOrder(this MissionTabType tabType)
		{
			return 0;
		}

		// Token: 0x06004C19 RID: 19481 RVA: 0x000029CC File Offset: 0x00000BCC
		public static MissionTabType ToResidentTabType(this MissionCategory category)
		{
			return MissionTabType.All;
		}

		// Token: 0x06004C1A RID: 19482 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetMissionName(int logicNo, List<object> logicParams)
		{
			return null;
		}
	}
}
