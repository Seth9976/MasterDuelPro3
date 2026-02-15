using System;
using UnityEngine;

namespace YgomSystem
{
	// Token: 0x020004AD RID: 1197
	public class PSManager : MonoBehaviour
	{
		// Token: 0x06002682 RID: 9858 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StartPSPremiumCheck(bool IsSpectating, Action<bool> callback, bool openPlusDialog = false)
		{
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0000216D File Offset: 0x0000036D
		public static void EndPSPremiumCheck()
		{
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowPsStoreIcon(bool state, int postion = 0)
		{
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CheckCommunicationRestriction(Action<bool> callback)
		{
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StartTutorialActivity()
		{
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x0000216D File Offset: 0x0000036D
		public static void EndTutorialActivity()
		{
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StartMatchActivity()
		{
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x0000216D File Offset: 0x0000036D
		public static void EndMatchActivity()
		{
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0000216D File Offset: 0x0000036D
		public static void StartTeamMatchActivity()
		{
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x0000216D File Offset: 0x0000036D
		public static void EndTeamMatchActivity()
		{
		}

		// Token: 0x0600268C RID: 9868 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsPSPlatform()
		{
			return false;
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsMultiPlayOK()
		{
			return false;
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowMultiPlayNGMessage()
		{
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetPushContextId(bool isInvite = false)
		{
			return null;
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowEmptyStoreMessage()
		{
		}

		// Token: 0x040027A1 RID: 10145
		public const int ICON_CENTER = 0;

		// Token: 0x040027A2 RID: 10146
		public const int ICON_LEFT = 1;

		// Token: 0x040027A3 RID: 10147
		public const int ICON_RIGHT = 2;

		// Token: 0x040027A4 RID: 10148
		private const float premiumFeatureIntervalVal = 2f;
	}
}
