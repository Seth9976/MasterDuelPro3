using System;
using System.Collections;
using System.Collections.Generic;
using YgomSystem;
using YgomSystem.Network;

namespace YgomGame
{
	// Token: 0x020007A0 RID: 1952
	public static class AccountUtility
	{
		// Token: 0x06003CAA RID: 15530 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator GetSteamAuthSession(bool cancel, Action<string> resultCallback)
		{
			return null;
		}

		// Token: 0x06003CAB RID: 15531 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator GetAuthSessionForKidCheck(Action<string, bool> resultCallback)
		{
			return null;
		}

		// Token: 0x06003CAC RID: 15532 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator callAPICoroutine(Handle apiHandle, Action<int> resultCallback)
		{
			return null;
		}

		// Token: 0x06003CAD RID: 15533 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator loginCoroutine(AccountUtility.AccoutCreateParamter createParam, Action<AccountUtility.LoginResultCode> resultCallback)
		{
			return null;
		}

		// Token: 0x06003CAE RID: 15534 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator resumeCoroutine(Action<bool> resultCallback)
		{
			return null;
		}

		// Token: 0x06003CAF RID: 15535 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator RefreshAuthCoroutine(Action<bool> resultCallback)
		{
			return null;
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Resume(Action<bool> resultCallback)
		{
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator ResumeCoroutine(Action<bool> resultCallback)
		{
			return null;
		}

		// Token: 0x06003CB2 RID: 15538 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CreateAccount(AccountUtility.AccoutCreateParamter parameter, Action<AccountUtility.LoginResultCode> resultCallback)
		{
		}

		// Token: 0x06003CB3 RID: 15539 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator CreateAccountCoroutine(AccountUtility.AccoutCreateParamter parameter, Action<AccountUtility.LoginResultCode> resultCallback)
		{
			return null;
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Auth(Action<AccountUtility.LoginResultCode> resultCallback)
		{
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator AuthCoroutine(Action<AccountUtility.LoginResultCode> resultCallback)
		{
			return null;
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RefreshAuth(Action<bool> resultCallback)
		{
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator ReagreeCoroutine(IReadOnlyList<AgreementKind> agreementKinds, AccountUtility.PrivacyOptionType privacyOption, Action<int> resultCallback)
		{
			return null;
		}

		// Token: 0x06003CB8 RID: 15544 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator UnlockCoroutine(string password, Action<bool> resultCallback)
		{
			return null;
		}

		// Token: 0x06003CB9 RID: 15545 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AlertXboxMultiplayOFFMessage(Action onEnd = null)
		{
		}

		// Token: 0x06003CBA RID: 15546 RVA: 0x000F4636 File Offset: 0x000F2836
		public static bool GetBanDialogData(out AccountUtility.BanType banType, out string title, out string message, out string button)
		{
			banType = AccountUtility.BanType.None;
			title = null;
			message = null;
			button = null;
			return false;
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x0000216A File Offset: 0x0000036A
		private static string getBanText(Dictionary<string, object> cw, string textIdKey, string textKey)
		{
			return null;
		}

		// Token: 0x0400352B RID: 13611
		private static SteamAuthSession steamAuthSession;

		// Token: 0x020007A1 RID: 1953
		public class AccoutCreateParamter
		{
			// Token: 0x0400352C RID: 13612
			public int countryNumeric;

			// Token: 0x0400352D RID: 13613
			public UserAgreementType userAgreementType;

			// Token: 0x0400352E RID: 13614
			public AccountUtility.PrivacyOptionType privacyOption;

			// Token: 0x0400352F RID: 13615
			public Dictionary<string, object> survayAnswer;
		}

		// Token: 0x020007A2 RID: 1954
		public enum PrivacyOptionType
		{
			// Token: 0x04003531 RID: 13617
			None,
			// Token: 0x04003532 RID: 13618
			OptIn,
			// Token: 0x04003533 RID: 13619
			OptOut
		}

		// Token: 0x020007A3 RID: 1955
		public enum LoginResultCode
		{
			// Token: 0x04003535 RID: 13621
			Success,
			// Token: 0x04003536 RID: 13622
			Error_Create,
			// Token: 0x04003537 RID: 13623
			Error_Auth,
			// Token: 0x04003538 RID: 13624
			Error_Platform_Check,
			// Token: 0x04003539 RID: 13625
			Error_Platform_Reauth,
			// Token: 0x0400353A RID: 13626
			Error_Agreement_Required,
			// Token: 0x0400353B RID: 13627
			Error_Password_Required,
			// Token: 0x0400353C RID: 13628
			Error_Banned,
			// Token: 0x0400353D RID: 13629
			Error_Unknown
		}

		// Token: 0x020007A4 RID: 1956
		public enum BanType
		{
			// Token: 0x0400353F RID: 13631
			None,
			// Token: 0x04003540 RID: 13632
			Alert,
			// Token: 0x04003541 RID: 13633
			LimitedBan,
			// Token: 0x04003542 RID: 13634
			PermanentBan,
			// Token: 0x04003543 RID: 13635
			Removed
		}
	}
}
