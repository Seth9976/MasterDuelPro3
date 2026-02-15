using System;
using System.Collections;
using System.Collections.Generic;
using YgomSystem;

namespace YgomGame.Menu
{
	// Token: 0x02000A7A RID: 2682
	public class GameEntrySequenceViewController : BaseMenuViewController
	{
		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x06004E6C RID: 20076 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x000029CC File Offset: 0x00000BCC
		private UserAgreementType currentAgreementType()
		{
			return UserAgreementType.None;
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool isMobile()
		{
			return false;
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x000029CC File Offset: 0x00000BCC
		private static AccountUtility.PrivacyOptionType getUSAPrivacyOption(int birthYear, int birthMonth, int birthDay)
		{
			return AccountUtility.PrivacyOptionType.None;
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x0000216D File Offset: 0x0000036D
		private static void quitGameEntry()
		{
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x0000216D File Offset: 0x0000036D
		private void saveStepHistory(StepSequencer seq)
		{
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x0000216D File Offset: 0x0000036D
		private void backStepHistory(StepSequencer seq)
		{
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x0000216D File Offset: 0x0000036D
		private void clearStepHistory()
		{
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepStart(StepSequencer seq)
		{
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepCountrySelect(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepStartingAgeGate(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E7A RID: 20090 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepUSAStateSelect(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepStartingTermOfService(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepStartingPrivacyPolicy(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepStartSurvey(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepAccountCreate(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepExistingLogin(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepReagreementAgeGate(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepReagreementTermOfService(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepReagreementPrivacyPolicy(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepReagree(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E84 RID: 20100 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator stepPasswordInput(StepSequencer seq)
		{
			return null;
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepRestoreUnfinishedProducts(StepSequencer seq)
		{
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepErrorAbort(StepSequencer seq)
		{
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x0000216D File Offset: 0x0000036D
		private void stepFinish(StepSequencer seq)
		{
		}

		// Token: 0x06004E88 RID: 20104 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator callTermOfServiceView(UserAgreementType agreementType, string url, Action<int> resultCallback)
		{
			return null;
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator callPrivacyPolicyView(UserAgreementType agreementType, Action<int> resultCallback)
		{
			return null;
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator callAgeGateView(int defaultYear, int defaultMonth, int defaultDay, Action<int, int, int> resultCallback)
		{
			return null;
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator showErrorDialog(string title, string msg)
		{
			return null;
		}

		// Token: 0x04008C39 RID: 35897
		private StepSequencer m_sequencer;

		// Token: 0x04008C3A RID: 35898
		private Stack<GameEntrySequenceViewController.Step> m_backSteps;

		// Token: 0x04008C3B RID: 35899
		private GameEntrySequenceViewController.UserData m_userData;

		// Token: 0x04008C3C RID: 35900
		private CountryList m_countryList;

		// Token: 0x04008C3D RID: 35901
		private USAStateList m_stateList;

		// Token: 0x02000A7B RID: 2683
		private enum Step
		{
			// Token: 0x04008C3F RID: 35903
			Start,
			// Token: 0x04008C40 RID: 35904
			CountrySelect,
			// Token: 0x04008C41 RID: 35905
			StartingAgeGate,
			// Token: 0x04008C42 RID: 35906
			USAStateSelect,
			// Token: 0x04008C43 RID: 35907
			StartingTermOfService,
			// Token: 0x04008C44 RID: 35908
			StartingPrivacyPolicy,
			// Token: 0x04008C45 RID: 35909
			StartSurvey,
			// Token: 0x04008C46 RID: 35910
			AccountCreate,
			// Token: 0x04008C47 RID: 35911
			ExistingLogin,
			// Token: 0x04008C48 RID: 35912
			ReagreementAgeGate,
			// Token: 0x04008C49 RID: 35913
			ReagreementTermOfService,
			// Token: 0x04008C4A RID: 35914
			ReagreementPrivacyPolicy,
			// Token: 0x04008C4B RID: 35915
			Reagree,
			// Token: 0x04008C4C RID: 35916
			PasswordInput,
			// Token: 0x04008C4D RID: 35917
			RestoreProduct,
			// Token: 0x04008C4E RID: 35918
			Finish = 1000,
			// Token: 0x04008C4F RID: 35919
			ErrorAbort = 9999
		}

		// Token: 0x02000A7C RID: 2684
		private class UserData
		{
			// Token: 0x04008C50 RID: 35920
			public bool isFirstPlay;

			// Token: 0x04008C51 RID: 35921
			public int countryCode;

			// Token: 0x04008C52 RID: 35922
			public int stateCode;

			// Token: 0x04008C53 RID: 35923
			public int ageGateYear;

			// Token: 0x04008C54 RID: 35924
			public int ageGateMonth;

			// Token: 0x04008C55 RID: 35925
			public int ageGateDay;

			// Token: 0x04008C56 RID: 35926
			public Dictionary<string, object> surveyAnswer;
		}
	}
}
