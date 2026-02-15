using System;
using System.Collections.Generic;
using UnityEngine;

namespace com.adjust.sdk
{
	// Token: 0x0200045D RID: 1117
	public class Adjust : MonoBehaviour
	{
		// Token: 0x060024D7 RID: 9431 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnApplicationPause(bool pauseStatus)
		{
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x0000216D File Offset: 0x0000036D
		public static void start(AdjustConfig adjustConfig)
		{
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x0000216D File Offset: 0x0000036D
		public static void trackEvent(AdjustEvent adjustEvent)
		{
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x0000216D File Offset: 0x0000036D
		public static void setEnabled(bool enabled)
		{
		}

		// Token: 0x060024DC RID: 9436 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool isEnabled()
		{
			return false;
		}

		// Token: 0x060024DD RID: 9437 RVA: 0x0000216D File Offset: 0x0000036D
		public static void setOfflineMode(bool enabled)
		{
		}

		// Token: 0x060024DE RID: 9438 RVA: 0x0000216D File Offset: 0x0000036D
		public static void setDeviceToken(string deviceToken)
		{
		}

		// Token: 0x060024DF RID: 9439 RVA: 0x0000216D File Offset: 0x0000036D
		public static void gdprForgetMe()
		{
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x0000216D File Offset: 0x0000036D
		public static void disableThirdPartySharing()
		{
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x0000216D File Offset: 0x0000036D
		public static void appWillOpenUrl(string url)
		{
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x0000216D File Offset: 0x0000036D
		public static void sendFirstPackages()
		{
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x0000216D File Offset: 0x0000036D
		public static void addSessionPartnerParameter(string key, string value)
		{
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0000216D File Offset: 0x0000036D
		public static void addSessionCallbackParameter(string key, string value)
		{
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0000216D File Offset: 0x0000036D
		public static void removeSessionPartnerParameter(string key)
		{
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x0000216D File Offset: 0x0000036D
		public static void removeSessionCallbackParameter(string key)
		{
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x0000216D File Offset: 0x0000036D
		public static void resetSessionPartnerParameters()
		{
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x0000216D File Offset: 0x0000036D
		public static void resetSessionCallbackParameters()
		{
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x0000216D File Offset: 0x0000036D
		public static void trackAdRevenue(string source, string payload)
		{
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0000216D File Offset: 0x0000036D
		public static void trackAppStoreSubscription(AdjustAppStoreSubscription subscription)
		{
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x0000216D File Offset: 0x0000036D
		public static void trackPlayStoreSubscription(AdjustPlayStoreSubscription subscription)
		{
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x0000216D File Offset: 0x0000036D
		public static void trackThirdPartySharing(AdjustThirdPartySharing thirdPartySharing)
		{
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x0000216D File Offset: 0x0000036D
		public static void trackMeasurementConsent(bool measurementConsent)
		{
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x0000216D File Offset: 0x0000036D
		public static void requestTrackingAuthorizationWithCompletionHandler(Action<int> statusCallback, string sceneName = "Adjust")
		{
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x0000216D File Offset: 0x0000036D
		public static void updateConversionValue(int conversionValue)
		{
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int getAppTrackingAuthorizationStatus()
		{
			return 0;
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x0000216A File Offset: 0x0000036A
		public static string getAdid()
		{
			return null;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x0000216A File Offset: 0x0000036A
		public static AdjustAttribution getAttribution()
		{
			return null;
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x0000216A File Offset: 0x0000036A
		public static string getWinAdid()
		{
			return null;
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x0000216A File Offset: 0x0000036A
		public static string getIdfa()
		{
			return null;
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x0000216A File Offset: 0x0000036A
		public static string getSdkVersion()
		{
			return null;
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x0000216D File Offset: 0x0000036D
		[Obsolete]
		public static void setReferrer(string referrer)
		{
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x0000216D File Offset: 0x0000036D
		public static void getGoogleAdId(Action<string> onDeviceIdsRead)
		{
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x0000216A File Offset: 0x0000036A
		public static string getAmazonAdId()
		{
			return null;
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x000029CC File Offset: 0x00000BCC
		private static bool IsEditor()
		{
			return false;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetTestOptions(Dictionary<string, string> testOptions)
		{
		}

		// Token: 0x040026A2 RID: 9890
		private const string errorMsgEditor = "[Adjust]: SDK can not be used in Editor.";

		// Token: 0x040026A3 RID: 9891
		private const string errorMsgStart = "[Adjust]: SDK not started. Start it manually using the 'start' method.";

		// Token: 0x040026A4 RID: 9892
		private const string errorMsgPlatform = "[Adjust]: SDK can only be used in Android, iOS, Windows Phone 8.1, Windows Store or Universal Windows apps.";

		// Token: 0x040026A5 RID: 9893
		public bool startManually;

		// Token: 0x040026A6 RID: 9894
		public bool eventBuffering;

		// Token: 0x040026A7 RID: 9895
		public bool sendInBackground;

		// Token: 0x040026A8 RID: 9896
		public bool launchDeferredDeeplink;

		// Token: 0x040026A9 RID: 9897
		public string appToken;

		// Token: 0x040026AA RID: 9898
		public AdjustLogLevel logLevel;

		// Token: 0x040026AB RID: 9899
		public AdjustEnvironment environment;
	}
}
