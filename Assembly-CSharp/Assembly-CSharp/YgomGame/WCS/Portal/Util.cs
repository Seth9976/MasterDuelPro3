using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using YgomGame.TextIDs;
using YgomSystem.Network;

namespace YgomGame.WCS.Portal
{
	// Token: 0x0200080C RID: 2060
	public class Util
	{
		// Token: 0x06003FB4 RID: 16308 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenPortal()
		{
		}

		// Token: 0x06003FB5 RID: 16309 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenPortalOnHome()
		{
		}

		// Token: 0x06003FB6 RID: 16310 RVA: 0x0000216D File Offset: 0x0000036D
		public static void GoToVotePushOnHome()
		{
		}

		// Token: 0x06003FB7 RID: 16311 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator GoToVotePushOnHomeCoroutine()
		{
			return null;
		}

		// Token: 0x06003FB8 RID: 16312 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowExpiredDialog(Action callback = null)
		{
		}

		// Token: 0x06003FB9 RID: 16313 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool HandleResultCode(Handle handle, UnityAction onSuccess, UnityAction<WcsCode> onFailed, bool showDialog = true)
		{
			return false;
		}

		// Token: 0x06003FBA RID: 16314 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsCampaignOpen()
		{
			return false;
		}

		// Token: 0x06003FBB RID: 16315 RVA: 0x000029CC File Offset: 0x00000BCC
		public static Def.CampaignStatus GetCampaignStatus()
		{
			return Def.CampaignStatus.Off;
		}

		// Token: 0x06003FBC RID: 16316 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetChampionTeamID()
		{
			return 0;
		}

		// Token: 0x06003FBD RID: 16317 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsReceivableVoteReward()
		{
			return false;
		}

		// Token: 0x06003FBE RID: 16318 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetCampaign()
		{
			return null;
		}

		// Token: 0x06003FBF RID: 16319 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetCampaignMaster()
		{
			return null;
		}

		// Token: 0x06003FC0 RID: 16320 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetTeamIconPathByIndex(int index, bool large)
		{
			return null;
		}

		// Token: 0x06003FC1 RID: 16321 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetTeamIconPath(int teamID, bool isLarge)
		{
			return null;
		}

		// Token: 0x06003FC2 RID: 16322 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetTeamAreaName(int teamID)
		{
			return null;
		}

		// Token: 0x06003FC3 RID: 16323 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetTeamName(int teamID)
		{
			return null;
		}

		// Token: 0x06003FC4 RID: 16324 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetTeamOrder(int teamID)
		{
			return 0;
		}

		// Token: 0x06003FC5 RID: 16325 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> GetTeamMaster(int teamID)
		{
			return null;
		}

		// Token: 0x040038F2 RID: 14578
		private static readonly string[] s_teamIconSmallPaths;

		// Token: 0x040038F3 RID: 14579
		private static readonly string[] s_teamIconLargePaths;

		// Token: 0x040038F4 RID: 14580
		private static readonly IDS_WCSPORTAL[] s_teamAreaIDs;
	}
}
