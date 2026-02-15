using System;
using System.Runtime.CompilerServices;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C46 RID: 3142
	public class DuelpassRewardContext
	{
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060059B8 RID: 22968 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060059B9 RID: 22969 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsPeriod
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060059BA RID: 22970 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060059BB RID: 22971 RVA: 0x0000216D File Offset: 0x0000036D
		public int ItemCategory
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

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060059BC RID: 22972 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060059BD RID: 22973 RVA: 0x0000216D File Offset: 0x0000036D
		public int ItemId
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

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060059BE RID: 22974 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060059BF RID: 22975 RVA: 0x0000216D File Offset: 0x0000036D
		public int ItemCount
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

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060059C0 RID: 22976 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060059C1 RID: 22977 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsRecommend
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060059C2 RID: 22978 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060059C3 RID: 22979 RVA: 0x0000216D File Offset: 0x0000036D
		public int Grade
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

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060059C4 RID: 22980 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060059C5 RID: 22981 RVA: 0x0000216D File Offset: 0x0000036D
		public int RewardId
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

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060059C6 RID: 22982 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060059C7 RID: 22983 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsReceived
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x060059C8 RID: 22984 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060059C9 RID: 22985 RVA: 0x0000216D File Offset: 0x0000036D
		public PASS_TYPE PassType
		{
			[CompilerGenerated]
			get
			{
				return PASS_TYPE.NORMAL;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x060059CA RID: 22986 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060059CB RID: 22987 RVA: 0x0000216D File Offset: 0x0000036D
		public bool IsAchieved
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x060059CC RID: 22988 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060059CD RID: 22989 RVA: 0x0000216D File Offset: 0x0000036D
		public bool HasGoldPass
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x00002739 File Offset: 0x00000939
		public DuelpassRewardContext(object master, int rewardId, bool isRecieved, bool isAchieved, bool hasGoldPass)
		{
		}

		// Token: 0x060059CF RID: 22991 RVA: 0x00002739 File Offset: 0x00000939
		public DuelpassRewardContext(object master, int grade, int rewardId, bool isReceived, bool isAchieved, PASS_TYPE passType, bool hasGoldPass)
		{
		}

		// Token: 0x060059D0 RID: 22992 RVA: 0x0000216D File Offset: 0x0000036D
		public void Receive()
		{
		}

		// Token: 0x060059D1 RID: 22993 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool CanReceive()
		{
			return false;
		}
	}
}
