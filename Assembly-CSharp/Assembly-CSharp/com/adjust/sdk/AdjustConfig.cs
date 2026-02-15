using System;

namespace com.adjust.sdk
{
	// Token: 0x02000460 RID: 1120
	public class AdjustConfig
	{
		// Token: 0x0600251A RID: 9498 RVA: 0x00002739 File Offset: 0x00000939
		public AdjustConfig(string appToken, AdjustEnvironment environment)
		{
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x00002739 File Offset: 0x00000939
		public AdjustConfig(string appToken, AdjustEnvironment environment, bool allowSuppressLogLevel)
		{
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x0000216D File Offset: 0x0000036D
		public void setLogLevel(AdjustLogLevel logLevel)
		{
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x0000216D File Offset: 0x0000036D
		public void setDefaultTracker(string defaultTracker)
		{
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x0000216D File Offset: 0x0000036D
		public void setExternalDeviceId(string externalDeviceId)
		{
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x0000216D File Offset: 0x0000036D
		public void setLaunchDeferredDeeplink(bool launchDeferredDeeplink)
		{
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x0000216D File Offset: 0x0000036D
		public void setSendInBackground(bool sendInBackground)
		{
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x0000216D File Offset: 0x0000036D
		public void setEventBufferingEnabled(bool eventBufferingEnabled)
		{
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x0000216D File Offset: 0x0000036D
		public void setNeedsCost(bool needsCost)
		{
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x0000216D File Offset: 0x0000036D
		public void setDelayStart(double delayStart)
		{
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x0000216D File Offset: 0x0000036D
		public void setUserAgent(string userAgent)
		{
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x0000216D File Offset: 0x0000036D
		public void setIsDeviceKnown(bool isDeviceKnown)
		{
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x0000216D File Offset: 0x0000036D
		public void setUrlStrategy(string urlStrategy)
		{
		}

		// Token: 0x06002527 RID: 9511 RVA: 0x0000216D File Offset: 0x0000036D
		public void deactivateSKAdNetworkHandling()
		{
		}

		// Token: 0x06002528 RID: 9512 RVA: 0x0000216D File Offset: 0x0000036D
		public void setDeferredDeeplinkDelegate(Action<string> deferredDeeplinkDelegate, string sceneName = "Adjust")
		{
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x0000216A File Offset: 0x0000036A
		public Action<string> getDeferredDeeplinkDelegate()
		{
			return null;
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAttributionChangedDelegate(Action<AdjustAttribution> attributionChangedDelegate, string sceneName = "Adjust")
		{
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x0000216A File Offset: 0x0000036A
		public Action<AdjustAttribution> getAttributionChangedDelegate()
		{
			return null;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x0000216D File Offset: 0x0000036D
		public void setEventSuccessDelegate(Action<AdjustEventSuccess> eventSuccessDelegate, string sceneName = "Adjust")
		{
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x0000216A File Offset: 0x0000036A
		public Action<AdjustEventSuccess> getEventSuccessDelegate()
		{
			return null;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x0000216D File Offset: 0x0000036D
		public void setEventFailureDelegate(Action<AdjustEventFailure> eventFailureDelegate, string sceneName = "Adjust")
		{
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x0000216A File Offset: 0x0000036A
		public Action<AdjustEventFailure> getEventFailureDelegate()
		{
			return null;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x0000216D File Offset: 0x0000036D
		public void setSessionSuccessDelegate(Action<AdjustSessionSuccess> sessionSuccessDelegate, string sceneName = "Adjust")
		{
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x0000216A File Offset: 0x0000036A
		public Action<AdjustSessionSuccess> getSessionSuccessDelegate()
		{
			return null;
		}

		// Token: 0x06002532 RID: 9522 RVA: 0x0000216D File Offset: 0x0000036D
		public void setSessionFailureDelegate(Action<AdjustSessionFailure> sessionFailureDelegate, string sceneName = "Adjust")
		{
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x0000216A File Offset: 0x0000036A
		public Action<AdjustSessionFailure> getSessionFailureDelegate()
		{
			return null;
		}

		// Token: 0x06002534 RID: 9524 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAppSecret(long secretId, long info1, long info2, long info3, long info4)
		{
		}

		// Token: 0x06002535 RID: 9525 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAllowiAdInfoReading(bool allowiAdInfoReading)
		{
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAllowAdServicesInfoReading(bool allowAdServicesInfoReading)
		{
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x0000216D File Offset: 0x0000036D
		public void setAllowIdfaReading(bool allowIdfaReading)
		{
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x0000216D File Offset: 0x0000036D
		public void setProcessName(string processName)
		{
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x0000216D File Offset: 0x0000036D
		[Obsolete]
		public void setReadMobileEquipmentIdentity(bool readMobileEquipmentIdentity)
		{
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x0000216D File Offset: 0x0000036D
		public void setPreinstallTrackingEnabled(bool preinstallTrackingEnabled)
		{
		}

		// Token: 0x0600253B RID: 9531 RVA: 0x0000216D File Offset: 0x0000036D
		public void setLogDelegate(Action<string> logDelegate)
		{
		}

		// Token: 0x040026B5 RID: 9909
		public const string AdjustUrlStrategyChina = "china";

		// Token: 0x040026B6 RID: 9910
		public const string AdjustUrlStrategyIndia = "india";

		// Token: 0x040026B7 RID: 9911
		public const string AdjustDataResidencyEU = "data-residency-eu";

		// Token: 0x040026B8 RID: 9912
		public const string AdjustAdRevenueSourceMopub = "mopub";

		// Token: 0x040026B9 RID: 9913
		public const string AdjustAdRevenueSourceAdmob = "admob";

		// Token: 0x040026BA RID: 9914
		public const string AdjustAdRevenueSourceFbNativeAd = "facebook_native_ad";

		// Token: 0x040026BB RID: 9915
		public const string AdjustAdRevenueSourceFbAudienceNetwork = "facebook_audience_network";

		// Token: 0x040026BC RID: 9916
		public const string AdjustAdRevenueSourceIronsource = "ironsource";

		// Token: 0x040026BD RID: 9917
		public const string AdjustAdRevenueSourceFyber = "fyber";

		// Token: 0x040026BE RID: 9918
		public const string AdjustAdRevenueSourceAerserv = "aerserv";

		// Token: 0x040026BF RID: 9919
		public const string AdjustAdRevenueSourceAppodeal = "appodeal";

		// Token: 0x040026C0 RID: 9920
		public const string AdjustAdRevenueSourceAdincube = "adincube";

		// Token: 0x040026C1 RID: 9921
		public const string AdjustAdRevenueSourceFusePowered = "fusepowered";

		// Token: 0x040026C2 RID: 9922
		public const string AdjustAdRevenueSourceAddaptr = "addapptr";

		// Token: 0x040026C3 RID: 9923
		public const string AdjustAdRevenueSourceMillenialMediation = "millennial_mediation";

		// Token: 0x040026C4 RID: 9924
		public const string AdjustAdRevenueSourceFlurry = "flurry";

		// Token: 0x040026C5 RID: 9925
		public const string AdjustAdRevenueSourceAdmost = "admost";

		// Token: 0x040026C6 RID: 9926
		public const string AdjustAdRevenueSourceDeltadna = "deltadna";

		// Token: 0x040026C7 RID: 9927
		public const string AdjustAdRevenueSourceUpsight = "upsight";

		// Token: 0x040026C8 RID: 9928
		public const string AdjustAdRevenueSourceUnityads = "unityads";

		// Token: 0x040026C9 RID: 9929
		public const string AdjustAdRevenueSourceAdtoapp = "adtoapp";

		// Token: 0x040026CA RID: 9930
		public const string AdjustAdRevenueSourceTapdaq = "tapdaq";

		// Token: 0x040026CB RID: 9931
		internal string appToken;

		// Token: 0x040026CC RID: 9932
		internal string sceneName;

		// Token: 0x040026CD RID: 9933
		internal string userAgent;

		// Token: 0x040026CE RID: 9934
		internal string defaultTracker;

		// Token: 0x040026CF RID: 9935
		internal string externalDeviceId;

		// Token: 0x040026D0 RID: 9936
		internal string urlStrategy;

		// Token: 0x040026D1 RID: 9937
		internal long? info1;

		// Token: 0x040026D2 RID: 9938
		internal long? info2;

		// Token: 0x040026D3 RID: 9939
		internal long? info3;

		// Token: 0x040026D4 RID: 9940
		internal long? info4;

		// Token: 0x040026D5 RID: 9941
		internal long? secretId;

		// Token: 0x040026D6 RID: 9942
		internal double? delayStart;

		// Token: 0x040026D7 RID: 9943
		internal bool? isDeviceKnown;

		// Token: 0x040026D8 RID: 9944
		internal bool? sendInBackground;

		// Token: 0x040026D9 RID: 9945
		internal bool? eventBufferingEnabled;

		// Token: 0x040026DA RID: 9946
		internal bool? allowSuppressLogLevel;

		// Token: 0x040026DB RID: 9947
		internal bool? needsCost;

		// Token: 0x040026DC RID: 9948
		internal bool launchDeferredDeeplink;

		// Token: 0x040026DD RID: 9949
		internal AdjustLogLevel? logLevel;

		// Token: 0x040026DE RID: 9950
		internal AdjustEnvironment environment;

		// Token: 0x040026DF RID: 9951
		internal Action<string> deferredDeeplinkDelegate;

		// Token: 0x040026E0 RID: 9952
		internal Action<AdjustEventSuccess> eventSuccessDelegate;

		// Token: 0x040026E1 RID: 9953
		internal Action<AdjustEventFailure> eventFailureDelegate;

		// Token: 0x040026E2 RID: 9954
		internal Action<AdjustSessionSuccess> sessionSuccessDelegate;

		// Token: 0x040026E3 RID: 9955
		internal Action<AdjustSessionFailure> sessionFailureDelegate;

		// Token: 0x040026E4 RID: 9956
		internal Action<AdjustAttribution> attributionChangedDelegate;

		// Token: 0x040026E5 RID: 9957
		internal bool? readImei;

		// Token: 0x040026E6 RID: 9958
		internal bool? preinstallTrackingEnabled;

		// Token: 0x040026E7 RID: 9959
		internal string processName;

		// Token: 0x040026E8 RID: 9960
		internal bool? allowiAdInfoReading;

		// Token: 0x040026E9 RID: 9961
		internal bool? allowAdServicesInfoReading;

		// Token: 0x040026EA RID: 9962
		internal bool? allowIdfaReading;

		// Token: 0x040026EB RID: 9963
		internal bool? skAdNetworkHandling;

		// Token: 0x040026EC RID: 9964
		internal Action<string> logDelegate;
	}
}
