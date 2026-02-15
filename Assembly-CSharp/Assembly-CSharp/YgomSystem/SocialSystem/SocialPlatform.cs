using System;
using System.Collections;
using UnityEngine;

namespace YgomSystem.SocialSystem
{
	// Token: 0x020006CF RID: 1743
	public class SocialPlatform
	{
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600364E RID: 13902 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsEnable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600364F RID: 13903 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsAuthenticated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06003650 RID: 13904 RVA: 0x0000216A File Offset: 0x0000036A
		public static string socialId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06003651 RID: 13905 RVA: 0x0000216A File Offset: 0x0000036A
		public static string socialHashId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06003652 RID: 13906 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool IsEnableAutoLogin
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool IsInitAutoLogin()
		{
			return false;
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Initialize()
		{
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Activate()
		{
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Authenticate(Action<bool> callback = null)
		{
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SignOut()
		{
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UpdateSocialId()
		{
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x0000216D File Offset: 0x0000036D
		private static void StartWatchAuthenticate()
		{
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator yStartWatchAuthenticate(bool isAuthenticated)
		{
			return null;
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x0000216D File Offset: 0x0000036D
		private static void UpdateLoginStatus(bool isAutheticated)
		{
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ShowAchievementUI()
		{
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x0000216D File Offset: 0x0000036D
		private static void SetAchievementNotificator()
		{
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x0000216D File Offset: 0x0000036D
		private static void AchievementNotificator(object value)
		{
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x0000216D File Offset: 0x0000036D
		public static void UnlockAchievement(string id, Action<bool> callback = null)
		{
		}

		// Token: 0x0400312F RID: 12591
		private static ISocialSystem s_instance;

		// Token: 0x04003130 RID: 12592
		private static Coroutine yWatchObserver;
	}
}
