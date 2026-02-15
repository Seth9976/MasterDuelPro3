using System;

namespace YgomSystem.SocialSystem
{
	// Token: 0x020006D0 RID: 1744
	public class Social_None : ISocialSystem
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06003662 RID: 13922 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsEnableAutoLogin
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06003663 RID: 13923 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFirstAutoLogin
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06003664 RID: 13924 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsAuthenticated
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06003665 RID: 13925 RVA: 0x0000216A File Offset: 0x0000036A
		public string socialId
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x0000216D File Offset: 0x0000036D
		public void Activate()
		{
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x0000216D File Offset: 0x0000036D
		public void Authenticate(Action<bool> callback = null)
		{
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x0000216D File Offset: 0x0000036D
		public void SignOut()
		{
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowAchievementUI()
		{
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x0000216A File Offset: 0x0000036A
		public string ConvertAchievementIdToKey(int achievementId)
		{
			return null;
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x0000216D File Offset: 0x0000036D
		public void UnlockAchievement(string id, Action<bool> callback = null)
		{
		}
	}
}
