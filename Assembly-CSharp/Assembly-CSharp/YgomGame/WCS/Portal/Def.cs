using System;

namespace YgomGame.WCS.Portal
{
	// Token: 0x02000806 RID: 2054
	public class Def
	{
		// Token: 0x02000807 RID: 2055
		public enum CampaignStatus
		{
			// Token: 0x040038D0 RID: 14544
			Off,
			// Token: 0x040038D1 RID: 14545
			Prepare,
			// Token: 0x040038D2 RID: 14546
			Primary,
			// Token: 0x040038D3 RID: 14547
			PreSemifinal,
			// Token: 0x040038D4 RID: 14548
			Semifinal,
			// Token: 0x040038D5 RID: 14549
			PreFinal,
			// Token: 0x040038D6 RID: 14550
			Final,
			// Token: 0x040038D7 RID: 14551
			Result
		}

		// Token: 0x02000808 RID: 2056
		public class CWPath
		{
			// Token: 0x040038D8 RID: 14552
			public static readonly string WcsfCampaign;

			// Token: 0x040038D9 RID: 14553
			public static readonly string MasterWcsfCampaign;

			// Token: 0x040038DA RID: 14554
			public static readonly string MasterWcsfTeam;

			// Token: 0x040038DB RID: 14555
			public static readonly string WcsfCampaign_room_info;

			// Token: 0x040038DC RID: 14556
			public static readonly string WcsfCampaign_table_info;

			// Token: 0x040038DD RID: 14557
			public static readonly string WcsfCampaign_room_member;

			// Token: 0x040038DE RID: 14558
			public static readonly string WcsfCampaign_tournament_score;
		}

		// Token: 0x02000809 RID: 2057
		public static class MMAAssetPath
		{
			// Token: 0x040038DF RID: 14559
			public static readonly string DUEL_RULE;
		}
	}
}
