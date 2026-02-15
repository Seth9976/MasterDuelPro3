using System;

namespace YgomGame.Menu
{
	// Token: 0x02000AD8 RID: 2776
	public class PvpMenuDefine
	{
		// Token: 0x04008F5D RID: 36701
		public const string ARGNAME_MATCH = "match";

		// Token: 0x04008F5E RID: 36702
		public const string ARGNAME_PARAM = "param";

		// Token: 0x04008F5F RID: 36703
		public const string ARGNAME_DPARAM = "dparam";

		// Token: 0x04008F60 RID: 36704
		public const string ARGNAME_TYPE = "type";

		// Token: 0x04008F61 RID: 36705
		public const string ARGNAME_OTHER = "other";

		// Token: 0x04008F62 RID: 36706
		public const string ARGNAME_RANK_EVENT_ID = "rank_event_id";

		// Token: 0x04008F63 RID: 36707
		public const string ARGNAME_GAMEMODE = "GameMode";

		// Token: 0x04008F64 RID: 36708
		public const string ARGNAME_MODE = "mode";

		// Token: 0x04008F65 RID: 36709
		public const string ARGNAME_RESEARCH = "research";

		// Token: 0x04008F66 RID: 36710
		public const string ARGNAME_RESEARCHTIME = "researchTime";

		// Token: 0x04008F67 RID: 36711
		public const string ARGNAME_PS_ONLINE_ID = "ps_online_id";

		// Token: 0x04008F68 RID: 36712
		public const string ARGNAME_XBOX_ONLINE_ID = "xbox_online_id";

		// Token: 0x04008F69 RID: 36713
		public const string ARGNAME_TID = "tid";

		// Token: 0x04008F6A RID: 36714
		public const string ARGNAME_EXHID = "exhid";

		// Token: 0x04008F6B RID: 36715
		public const string ARGNAME_CID = "cid";

		// Token: 0x04008F6C RID: 36716
		public const string ARGNAME_WCS_ID = "wcs_id";

		// Token: 0x04008F6D RID: 36717
		public const string ARGNAME_DUEL_TRIAL_ID = "duel_trial_id";

		// Token: 0x04008F6E RID: 36718
		public const string ARGNAME_VERSUS_ID = "versus_id";

		// Token: 0x04008F6F RID: 36719
		public const string ARGNAME_RENTAL_STATE = "rental_state";

		// Token: 0x04008F70 RID: 36720
		public const string ARGNAME_SEASON_ID = "season_id";

		// Token: 0x04008F71 RID: 36721
		public const string ARGNAME_IS_TEAM_LEADER = "is_team_leader";

		// Token: 0x04008F72 RID: 36722
		public const string ARGNAME_OPP_TEAM_ID = "opp_team_id";

		// Token: 0x04008F73 RID: 36723
		public const string ARGNAME_MATCHING_KEY = "matching_key";

		// Token: 0x02000AD9 RID: 2777
		public enum MatchingType
		{
			// Token: 0x04008F75 RID: 36725
			UNKNOWN,
			// Token: 0x04008F76 RID: 36726
			FREE,
			// Token: 0x04008F77 RID: 36727
			RANK,
			// Token: 0x04008F78 RID: 36728
			TOURNAMENT,
			// Token: 0x04008F79 RID: 36729
			WATCH,
			// Token: 0x04008F7A RID: 36730
			ROOM,
			// Token: 0x04008F7B RID: 36731
			EXHIBITION,
			// Token: 0x04008F7C RID: 36732
			DUELISTCUP,
			// Token: 0x04008F7D RID: 36733
			RANKEVENT,
			// Token: 0x04008F7E RID: 36734
			TEAM,
			// Token: 0x04008F7F RID: 36735
			DUELTRIAL,
			// Token: 0x04008F80 RID: 36736
			WCS,
			// Token: 0x04008F81 RID: 36737
			VERSUS,
			// Token: 0x04008F82 RID: 36738
			WCS_FINAL,
			// Token: 0x04008F83 RID: 36739
			MAX
		}
	}
}
