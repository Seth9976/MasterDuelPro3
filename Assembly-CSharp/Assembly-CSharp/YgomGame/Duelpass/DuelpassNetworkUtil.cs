using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using YgomSystem.Network;

namespace YgomGame.Duelpass
{
	// Token: 0x02000C3A RID: 3130
	public static class DuelpassNetworkUtil
	{
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06005932 RID: 22834 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06005933 RID: 22835 RVA: 0x0000216D File Offset: 0x0000036D
		public static List<DuelpassRewardContext> Contexts
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

		// Token: 0x06005934 RID: 22836 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RequestReceiveInfo(Action onComplete = null)
		{
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Init()
		{
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Update()
		{
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool WithinThePeriod()
		{
			return false;
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool ResultExists()
		{
			return false;
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<DuelpassRewardContext> GetDuelpassRewardContextList()
		{
			return null;
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetCurrentGrade()
		{
			return 0;
		}

		// Token: 0x0600593B RID: 22843 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelpassProgressBarContext GetProgressBarContext()
		{
			return null;
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelpassResultProgressBarContext GetResultProgressBarContext()
		{
			return null;
		}

		// Token: 0x0600593D RID: 22845 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetReceiveableRewardNum()
		{
			return 0;
		}

		// Token: 0x0600593E RID: 22846 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetLatestAchievedGrade()
		{
			return 0;
		}

		// Token: 0x0600593F RID: 22847 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetSeasonId()
		{
			return 0;
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetStartTime()
		{
			return null;
		}

		// Token: 0x06005941 RID: 22849 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetEndTime()
		{
			return null;
		}

		// Token: 0x06005942 RID: 22850 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetEndTimeStamp()
		{
			return 0;
		}

		// Token: 0x06005943 RID: 22851 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool HasGoldpass()
		{
			return false;
		}

		// Token: 0x06005944 RID: 22852 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelpassRewardContext GetContext(int rewardId)
		{
			return null;
		}

		// Token: 0x06005945 RID: 22853 RVA: 0x0000216D File Offset: 0x0000036D
		private static void OnCompleteRecieve(Handle h, Action onSuccess, Action onFaild)
		{
		}

		// Token: 0x06005946 RID: 22854 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ItemDialog(int rewardId)
		{
		}

		// Token: 0x06005947 RID: 22855 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Receive(int id, Action onSuccess = null, Action onFaild = null)
		{
		}

		// Token: 0x06005948 RID: 22856 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SingleReceive(int rewardId)
		{
		}

		// Token: 0x06005949 RID: 22857 RVA: 0x0000216D File Offset: 0x0000036D
		private static void OnSuccessSingleReceive(DuelpassRewardContext context)
		{
		}

		// Token: 0x0600594A RID: 22858 RVA: 0x0000216D File Offset: 0x0000036D
		private static void OnFaildSingleReceive()
		{
		}

		// Token: 0x0600594B RID: 22859 RVA: 0x0000216D File Offset: 0x0000036D
		public static void BulkReceive(int seasonId = -1, List<DuelpassRewardContext> contexts = null)
		{
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x0000216D File Offset: 0x0000036D
		private static void OnSuccessBulkReceive()
		{
		}

		// Token: 0x0600594D RID: 22861 RVA: 0x0000216D File Offset: 0x0000036D
		private static void OnFaildBulkReceive()
		{
		}

		// Token: 0x0600594E RID: 22862 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetShopId()
		{
			return 0;
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckReceivedItem()
		{
		}

		// Token: 0x0400951C RID: 38172
		public static Action onUpdate;
	}
}
