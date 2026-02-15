using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Defines a set of commonly used security identifiers (SIDs).</summary>
	// Token: 0x020003DA RID: 986
	[ComVisible(false)]
	public enum WellKnownSidType
	{
		/// <summary>Indicates a null SID.</summary>
		// Token: 0x04000FAD RID: 4013
		NullSid,
		/// <summary>Indicates a SID that matches everyone.</summary>
		// Token: 0x04000FAE RID: 4014
		WorldSid,
		/// <summary>Indicates a local SID.</summary>
		// Token: 0x04000FAF RID: 4015
		LocalSid,
		/// <summary>Indicates a SID that matches the owner or creator of an object.</summary>
		// Token: 0x04000FB0 RID: 4016
		CreatorOwnerSid,
		/// <summary>Indicates a SID that matches the creator group of an object.</summary>
		// Token: 0x04000FB1 RID: 4017
		CreatorGroupSid,
		/// <summary>Indicates a creator owner server SID.</summary>
		// Token: 0x04000FB2 RID: 4018
		CreatorOwnerServerSid,
		/// <summary>Indicates a creator group server SID.</summary>
		// Token: 0x04000FB3 RID: 4019
		CreatorGroupServerSid,
		/// <summary>Indicates a SID for the Windows NT authority.</summary>
		// Token: 0x04000FB4 RID: 4020
		NTAuthoritySid,
		/// <summary>Indicates a SID for a dial-up account.</summary>
		// Token: 0x04000FB5 RID: 4021
		DialupSid,
		/// <summary>Indicates a SID for a network account. This SID is added to the process of a token when it logs on across a network.</summary>
		// Token: 0x04000FB6 RID: 4022
		NetworkSid,
		/// <summary>Indicates a SID for a batch process. This SID is added to the process of a token when it logs on as a batch job.</summary>
		// Token: 0x04000FB7 RID: 4023
		BatchSid,
		/// <summary>Indicates a SID for an interactive account. This SID is added to the process of a token when it logs on interactively.</summary>
		// Token: 0x04000FB8 RID: 4024
		InteractiveSid,
		/// <summary>Indicates a SID for a service. This SID is added to the process of a token when it logs on as a service.</summary>
		// Token: 0x04000FB9 RID: 4025
		ServiceSid,
		/// <summary>Indicates a SID for the anonymous account.</summary>
		// Token: 0x04000FBA RID: 4026
		AnonymousSid,
		/// <summary>Indicates a proxy SID.</summary>
		// Token: 0x04000FBB RID: 4027
		ProxySid,
		/// <summary>Indicates a SID for an enterprise controller.</summary>
		// Token: 0x04000FBC RID: 4028
		EnterpriseControllersSid,
		/// <summary>Indicates a SID for self.</summary>
		// Token: 0x04000FBD RID: 4029
		SelfSid,
		/// <summary>Indicates a SID for an authenticated user.</summary>
		// Token: 0x04000FBE RID: 4030
		AuthenticatedUserSid,
		/// <summary>Indicates a SID for restricted code.</summary>
		// Token: 0x04000FBF RID: 4031
		RestrictedCodeSid,
		/// <summary>Indicates a SID that matches a terminal server account.</summary>
		// Token: 0x04000FC0 RID: 4032
		TerminalServerSid,
		/// <summary>Indicates a SID that matches remote logons.</summary>
		// Token: 0x04000FC1 RID: 4033
		RemoteLogonIdSid,
		/// <summary>Indicates a SID that matches logon IDs.</summary>
		// Token: 0x04000FC2 RID: 4034
		LogonIdsSid,
		/// <summary>Indicates a SID that matches the local system.</summary>
		// Token: 0x04000FC3 RID: 4035
		LocalSystemSid,
		/// <summary>Indicates a SID that matches a local service.</summary>
		// Token: 0x04000FC4 RID: 4036
		LocalServiceSid,
		/// <summary>Indicates a SID that matches a network service.</summary>
		// Token: 0x04000FC5 RID: 4037
		NetworkServiceSid,
		/// <summary>Indicates a SID that matches the domain account.</summary>
		// Token: 0x04000FC6 RID: 4038
		BuiltinDomainSid,
		/// <summary>Indicates a SID that matches the administrator account.</summary>
		// Token: 0x04000FC7 RID: 4039
		BuiltinAdministratorsSid,
		/// <summary>Indicates a SID that matches built-in user accounts.</summary>
		// Token: 0x04000FC8 RID: 4040
		BuiltinUsersSid,
		/// <summary>Indicates a SID that matches the guest account.</summary>
		// Token: 0x04000FC9 RID: 4041
		BuiltinGuestsSid,
		/// <summary>Indicates a SID that matches the power users group.</summary>
		// Token: 0x04000FCA RID: 4042
		BuiltinPowerUsersSid,
		/// <summary>Indicates a SID that matches the account operators account.</summary>
		// Token: 0x04000FCB RID: 4043
		BuiltinAccountOperatorsSid,
		/// <summary>Indicates a SID that matches the system operators group.</summary>
		// Token: 0x04000FCC RID: 4044
		BuiltinSystemOperatorsSid,
		/// <summary>Indicates a SID that matches the print operators group.</summary>
		// Token: 0x04000FCD RID: 4045
		BuiltinPrintOperatorsSid,
		/// <summary>Indicates a SID that matches the backup operators group.</summary>
		// Token: 0x04000FCE RID: 4046
		BuiltinBackupOperatorsSid,
		/// <summary>Indicates a SID that matches the replicator account.</summary>
		// Token: 0x04000FCF RID: 4047
		BuiltinReplicatorSid,
		/// <summary>Indicates a SID that matches pre-Windows 2000 compatible accounts.</summary>
		// Token: 0x04000FD0 RID: 4048
		BuiltinPreWindows2000CompatibleAccessSid,
		/// <summary>Indicates a SID that matches remote desktop users.</summary>
		// Token: 0x04000FD1 RID: 4049
		BuiltinRemoteDesktopUsersSid,
		/// <summary>Indicates a SID that matches the network operators group.</summary>
		// Token: 0x04000FD2 RID: 4050
		BuiltinNetworkConfigurationOperatorsSid,
		/// <summary>Indicates a SID that matches the account administrators group.</summary>
		// Token: 0x04000FD3 RID: 4051
		AccountAdministratorSid,
		/// <summary>Indicates a SID that matches the account guest group.</summary>
		// Token: 0x04000FD4 RID: 4052
		AccountGuestSid,
		/// <summary>Indicates a SID that matches the account Kerberos target group.</summary>
		// Token: 0x04000FD5 RID: 4053
		AccountKrbtgtSid,
		/// <summary>Indicates a SID that matches the account domain administrator group.</summary>
		// Token: 0x04000FD6 RID: 4054
		AccountDomainAdminsSid,
		/// <summary>Indicates a SID that matches the account domain users group.</summary>
		// Token: 0x04000FD7 RID: 4055
		AccountDomainUsersSid,
		/// <summary>Indicates a SID that matches the account domain guests group.</summary>
		// Token: 0x04000FD8 RID: 4056
		AccountDomainGuestsSid,
		/// <summary>Indicates a SID that matches the account computer group.</summary>
		// Token: 0x04000FD9 RID: 4057
		AccountComputersSid,
		/// <summary>Indicates a SID that matches the account controller group.</summary>
		// Token: 0x04000FDA RID: 4058
		AccountControllersSid,
		/// <summary>Indicates a SID that matches the certificate administrators group.</summary>
		// Token: 0x04000FDB RID: 4059
		AccountCertAdminsSid,
		/// <summary>Indicates a SID that matches the schema administrators group.</summary>
		// Token: 0x04000FDC RID: 4060
		AccountSchemaAdminsSid,
		/// <summary>Indicates a SID that matches the enterprise administrators group.</summary>
		// Token: 0x04000FDD RID: 4061
		AccountEnterpriseAdminsSid,
		/// <summary>Indicates a SID that matches the policy administrators group.</summary>
		// Token: 0x04000FDE RID: 4062
		AccountPolicyAdminsSid,
		/// <summary>Indicates a SID that matches the RAS and IAS server account.</summary>
		// Token: 0x04000FDF RID: 4063
		AccountRasAndIasServersSid,
		/// <summary>Indicates a SID present when the Microsoft NTLM authentication package authenticated the client.</summary>
		// Token: 0x04000FE0 RID: 4064
		NtlmAuthenticationSid,
		/// <summary>Indicates a SID present when the Microsoft Digest authentication package authenticated the client.</summary>
		// Token: 0x04000FE1 RID: 4065
		DigestAuthenticationSid,
		/// <summary>Indicates a SID present when the Secure Channel (SSL/TLS) authentication package authenticated the client.</summary>
		// Token: 0x04000FE2 RID: 4066
		SChannelAuthenticationSid,
		/// <summary>Indicates a SID present when the user authenticated from within the forest or across a trust that does not have the selective authentication option enabled. If this SID is present, then <see cref="F:System.Security.Principal.WellKnownSidType.OtherOrganizationSid" /> cannot be present.</summary>
		// Token: 0x04000FE3 RID: 4067
		ThisOrganizationSid,
		/// <summary>Indicates a SID present when the user authenticated across a forest with the selective authentication option enabled. If this SID is present, then <see cref="F:System.Security.Principal.WellKnownSidType.ThisOrganizationSid" /> cannot be present.</summary>
		// Token: 0x04000FE4 RID: 4068
		OtherOrganizationSid,
		/// <summary>Indicates a SID that allows a user to create incoming forest trusts. It is added to the token of users who are a member of the Incoming Forest Trust Builders built-in group in the root domain of the forest.</summary>
		// Token: 0x04000FE5 RID: 4069
		BuiltinIncomingForestTrustBuildersSid,
		/// <summary>Indicates a SID that matches the group of users that have remote access to schedule logging of performance counters on this computer.</summary>
		// Token: 0x04000FE6 RID: 4070
		BuiltinPerformanceMonitoringUsersSid,
		/// <summary>Indicates a SID that matches the group of users that have remote access to monitor the computer.</summary>
		// Token: 0x04000FE7 RID: 4071
		BuiltinPerformanceLoggingUsersSid,
		/// <summary>Indicates a SID that matches the Windows Authorization Access group.</summary>
		// Token: 0x04000FE8 RID: 4072
		BuiltinAuthorizationAccessSid,
		/// <summary>Indicates a SID is present in a server that can issue Terminal Server licenses.</summary>
		// Token: 0x04000FE9 RID: 4073
		WinBuiltinTerminalServerLicenseServersSid,
		/// <summary>Indicates the maximum defined SID in the <see cref="T:System.Security.Principal.WellKnownSidType" /> enumeration.</summary>
		// Token: 0x04000FEA RID: 4074
		MaxDefined = 60,
		// Token: 0x04000FEB RID: 4075
		WinBuiltinDCOMUsersSid,
		// Token: 0x04000FEC RID: 4076
		WinBuiltinIUsersSid,
		// Token: 0x04000FED RID: 4077
		WinIUserSid,
		// Token: 0x04000FEE RID: 4078
		WinBuiltinCryptoOperatorsSid,
		// Token: 0x04000FEF RID: 4079
		WinUntrustedLabelSid,
		// Token: 0x04000FF0 RID: 4080
		WinLowLabelSid,
		// Token: 0x04000FF1 RID: 4081
		WinMediumLabelSid,
		// Token: 0x04000FF2 RID: 4082
		WinHighLabelSid,
		// Token: 0x04000FF3 RID: 4083
		WinSystemLabelSid,
		// Token: 0x04000FF4 RID: 4084
		WinWriteRestrictedCodeSid,
		// Token: 0x04000FF5 RID: 4085
		WinCreatorOwnerRightsSid,
		// Token: 0x04000FF6 RID: 4086
		WinCacheablePrincipalsGroupSid,
		// Token: 0x04000FF7 RID: 4087
		WinNonCacheablePrincipalsGroupSid,
		// Token: 0x04000FF8 RID: 4088
		WinEnterpriseReadonlyControllersSid,
		// Token: 0x04000FF9 RID: 4089
		WinAccountReadonlyControllersSid,
		// Token: 0x04000FFA RID: 4090
		WinBuiltinEventLogReadersGroup,
		// Token: 0x04000FFB RID: 4091
		WinNewEnterpriseReadonlyControllersSid,
		// Token: 0x04000FFC RID: 4092
		WinBuiltinCertSvcDComAccessGroup,
		// Token: 0x04000FFD RID: 4093
		WinMediumPlusLabelSid,
		// Token: 0x04000FFE RID: 4094
		WinLocalLogonSid,
		// Token: 0x04000FFF RID: 4095
		WinConsoleLogonSid,
		// Token: 0x04001000 RID: 4096
		WinThisOrganizationCertificateSid,
		// Token: 0x04001001 RID: 4097
		WinApplicationPackageAuthoritySid,
		// Token: 0x04001002 RID: 4098
		WinBuiltinAnyPackageSid,
		// Token: 0x04001003 RID: 4099
		WinCapabilityInternetClientSid,
		// Token: 0x04001004 RID: 4100
		WinCapabilityInternetClientServerSid,
		// Token: 0x04001005 RID: 4101
		WinCapabilityPrivateNetworkClientServerSid,
		// Token: 0x04001006 RID: 4102
		WinCapabilityPicturesLibrarySid,
		// Token: 0x04001007 RID: 4103
		WinCapabilityVideosLibrarySid,
		// Token: 0x04001008 RID: 4104
		WinCapabilityMusicLibrarySid,
		// Token: 0x04001009 RID: 4105
		WinCapabilityDocumentsLibrarySid,
		// Token: 0x0400100A RID: 4106
		WinCapabilitySharedUserCertificatesSid,
		// Token: 0x0400100B RID: 4107
		WinCapabilityEnterpriseAuthenticationSid,
		// Token: 0x0400100C RID: 4108
		WinCapabilityRemovableStorageSid
	}
}
