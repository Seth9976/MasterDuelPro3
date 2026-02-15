using System;
using System.Collections.Generic;

namespace YgomGame
{
	// Token: 0x020007A7 RID: 1959
	public static class AgreementUtility
	{
		// Token: 0x06003CBE RID: 15550 RVA: 0x0000216D File Offset: 0x0000036D
		private static void debugLog(string msg)
		{
		}

		// Token: 0x06003CBF RID: 15551 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Clear()
		{
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x000029CC File Offset: 0x00000BCC
		public static UserAgreementType GetCurrentAgreementType()
		{
			return UserAgreementType.None;
		}

		// Token: 0x06003CC1 RID: 15553 RVA: 0x0000216A File Offset: 0x0000036A
		public static AgreementKind[] GetRequiredKinds(UserAgreementType agreementType)
		{
			return null;
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long GetAgreementKindRevision(AgreementKind kind)
		{
			return 0L;
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetAgreementKindUrl(AgreementKind kind, string lang = "")
		{
			return null;
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetTermOfServiceUrl(PlatformTOSKind TOSKind, string lang = "")
		{
			return null;
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x000029CC File Offset: 0x00000BCC
		public static AgreeRequirementType GetRequirementType()
		{
			return AgreeRequirementType.None;
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x0000216A File Offset: 0x0000036A
		public static List<AgreementKind> GetReagreeKinds()
		{
			return null;
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsNeedReagreeAgeGate()
		{
			return false;
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearReagree()
		{
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x0000216A File Offset: 0x0000036A
		public static string[] MakeAgreementRevisionParameter(IReadOnlyList<AgreementKind> kinds)
		{
			return null;
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ImportAgreementInfo(object value)
		{
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ImportNeedReagree(object value)
		{
		}

		// Token: 0x04003550 RID: 13648
		private static readonly Dictionary<UserAgreementType, AgreementKind[]> c_requiredKindsTable;

		// Token: 0x04003551 RID: 13649
		private static AgreementUtility.DataManager s_instance;

		// Token: 0x020007A8 RID: 1960
		private class AgreementKindInfo
		{
			// Token: 0x06003CCC RID: 15564 RVA: 0x00002739 File Offset: 0x00000939
			private AgreementKindInfo()
			{
			}

			// Token: 0x06003CCD RID: 15565 RVA: 0x00002739 File Offset: 0x00000939
			public AgreementKindInfo(AgreementKind kind, long revision, LanguageUrlSet urls)
			{
			}

			// Token: 0x04003552 RID: 13650
			public AgreementKind kind;

			// Token: 0x04003553 RID: 13651
			public long revision;

			// Token: 0x04003554 RID: 13652
			public LanguageUrlSet urls;
		}

		// Token: 0x020007A9 RID: 1961
		private class DataManager
		{
			// Token: 0x17000475 RID: 1141
			// (get) Token: 0x06003CCE RID: 15566 RVA: 0x000029CC File Offset: 0x00000BCC
			public UserAgreementType currentAgreementType
			{
				get
				{
					return UserAgreementType.None;
				}
			}

			// Token: 0x17000476 RID: 1142
			// (get) Token: 0x06003CCF RID: 15567 RVA: 0x000029CC File Offset: 0x00000BCC
			public AgreeRequirementType requirementType
			{
				get
				{
					return AgreeRequirementType.None;
				}
			}

			// Token: 0x17000477 RID: 1143
			// (get) Token: 0x06003CD0 RID: 15568 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool isNeedReagreeAgeGate
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003CD1 RID: 15569 RVA: 0x0000216A File Offset: 0x0000036A
			private AgreementUtility.AgreementKindInfo getAgreementKindInfo(AgreementKind kind)
			{
				return null;
			}

			// Token: 0x06003CD2 RID: 15570 RVA: 0x0000216D File Offset: 0x0000036D
			public void Clear()
			{
			}

			// Token: 0x06003CD3 RID: 15571 RVA: 0x000F1669 File Offset: 0x000EF869
			public long GetAgreementKindRevision(AgreementKind kind)
			{
				return 0L;
			}

			// Token: 0x06003CD4 RID: 15572 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetAgreementKindUrl(AgreementKind kind, string lang)
			{
				return null;
			}

			// Token: 0x06003CD5 RID: 15573 RVA: 0x0000216A File Offset: 0x0000036A
			public string GetTermOfServiceUrl(PlatformTOSKind TOSKind, string lang)
			{
				return null;
			}

			// Token: 0x06003CD6 RID: 15574 RVA: 0x0000216A File Offset: 0x0000036A
			public List<AgreementKind> GetReagreeKinds()
			{
				return null;
			}

			// Token: 0x06003CD7 RID: 15575 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearReagree()
			{
			}

			// Token: 0x06003CD8 RID: 15576 RVA: 0x0000216A File Offset: 0x0000036A
			public string[] MakeAgreementRevisionParameter(IReadOnlyList<AgreementKind> kinds)
			{
				return null;
			}

			// Token: 0x06003CD9 RID: 15577 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportAgreementInfo(object value)
			{
			}

			// Token: 0x06003CDA RID: 15578 RVA: 0x0000216D File Offset: 0x0000036D
			public void ImportNeedReagree(object value)
			{
			}

			// Token: 0x04003555 RID: 13653
			private Dictionary<AgreementKind, AgreementUtility.AgreementKindInfo> m_kindInfos;

			// Token: 0x04003556 RID: 13654
			private UserAgreementType m_currentAgreementType;

			// Token: 0x04003557 RID: 13655
			private Dictionary<PlatformTOSKind, LanguageUrlSet> m_TOSUrls;

			// Token: 0x04003558 RID: 13656
			private AgreeRequirementType m_requirementType;

			// Token: 0x04003559 RID: 13657
			private List<AgreementKind> m_reagreeKinds;

			// Token: 0x0400355A RID: 13658
			private bool m_reagreeAgeGate;
		}
	}
}
