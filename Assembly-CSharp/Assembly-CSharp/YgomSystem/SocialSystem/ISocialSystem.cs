using System;

namespace YgomSystem.SocialSystem
{
	// Token: 0x020006CE RID: 1742
	public interface ISocialSystem
	{
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06003644 RID: 13892
		bool IsEnableAutoLogin { get; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06003645 RID: 13893
		bool IsFirstAutoLogin { get; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06003646 RID: 13894
		bool IsAuthenticated { get; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06003647 RID: 13895
		string socialId { get; }

		// Token: 0x06003648 RID: 13896
		void Activate();

		// Token: 0x06003649 RID: 13897
		void Authenticate(Action<bool> callback = null);

		// Token: 0x0600364A RID: 13898
		void SignOut();

		// Token: 0x0600364B RID: 13899
		void ShowAchievementUI();

		// Token: 0x0600364C RID: 13900
		string ConvertAchievementIdToKey(int achievementId);

		// Token: 0x0600364D RID: 13901
		void UnlockAchievement(string id, Action<bool> callback = null);
	}
}
