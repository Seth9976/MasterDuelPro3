using System;
using System.Runtime.CompilerServices;

namespace YgomGame.Team
{
	// Token: 0x020008B6 RID: 2230
	public class OpponentTeamInfo
	{
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600412B RID: 16683 RVA: 0x0000216D File Offset: 0x0000036D
		public int duelDurationId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600412C RID: 16684 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600412D RID: 16685 RVA: 0x0000216D File Offset: 0x0000036D
		public int teamId
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600412E RID: 16686 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600412F RID: 16687 RVA: 0x0000216D File Offset: 0x0000036D
		public int mrk
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06004130 RID: 16688 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06004131 RID: 16689 RVA: 0x0000216D File Offset: 0x0000036D
		public string matchKey
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06004132 RID: 16690 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isValid
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x00002739 File Offset: 0x00000939
		private OpponentTeamInfo()
		{
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x0000216A File Offset: 0x0000036A
		public static OpponentTeamInfo AcquireFromCW()
		{
			return null;
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x0000216A File Offset: 0x0000036A
		public static OpponentTeamInfo LoadFromCW(object root)
		{
			return null;
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsOpponentFixed()
		{
			return false;
		}

		// Token: 0x04007F8E RID: 32654
		public static readonly string CW_PATH;

		// Token: 0x04007F8F RID: 32655
		private const string CW_PATH_DURATIONID = "duel_time";

		// Token: 0x04007F90 RID: 32656
		private const string CW_PATH_TEAM_ID = "opp_team_id";

		// Token: 0x04007F91 RID: 32657
		private const string CW_PATH_MRK = "opp_team_mrk";

		// Token: 0x04007F92 RID: 32658
		private const string CW_PATH_MATCHING_KEY = "matching_key";
	}
}
