using System;
using System.Collections.Generic;

namespace com.adjust.sdk
{
	// Token: 0x0200046C RID: 1132
	public class AdjustUtils
	{
		// Token: 0x0600258C RID: 9612 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ConvertLogLevel(AdjustLogLevel? logLevel)
		{
			return 0;
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int ConvertBool(bool? value)
		{
			return 0;
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000F165E File Offset: 0x000EF85E
		public static double ConvertDouble(double? value)
		{
			return 0.0;
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000F1669 File Offset: 0x000EF869
		public static long ConvertLong(long? value)
		{
			return 0L;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x0000216A File Offset: 0x0000036A
		public static string ConvertListToJson(List<string> list)
		{
			return null;
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetJsonResponseCompact(Dictionary<string, object> dictionary)
		{
			return null;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetJsonString(JSONNode node, string key)
		{
			return null;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0000216D File Offset: 0x0000036D
		public static void WriteJsonResponseDictionary(JSONClass jsonObject, Dictionary<string, object> output)
		{
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x0000216A File Offset: 0x0000036A
		public static string TryGetValue(Dictionary<string, string> dictionary, string key)
		{
			return null;
		}

		// Token: 0x0400270D RID: 9997
		public static string KeyAdid;

		// Token: 0x0400270E RID: 9998
		public static string KeyMessage;

		// Token: 0x0400270F RID: 9999
		public static string KeyNetwork;

		// Token: 0x04002710 RID: 10000
		public static string KeyAdgroup;

		// Token: 0x04002711 RID: 10001
		public static string KeyCampaign;

		// Token: 0x04002712 RID: 10002
		public static string KeyCreative;

		// Token: 0x04002713 RID: 10003
		public static string KeyWillRetry;

		// Token: 0x04002714 RID: 10004
		public static string KeyTimestamp;

		// Token: 0x04002715 RID: 10005
		public static string KeyCallbackId;

		// Token: 0x04002716 RID: 10006
		public static string KeyEventToken;

		// Token: 0x04002717 RID: 10007
		public static string KeyClickLabel;

		// Token: 0x04002718 RID: 10008
		public static string KeyTrackerName;

		// Token: 0x04002719 RID: 10009
		public static string KeyTrackerToken;

		// Token: 0x0400271A RID: 10010
		public static string KeyJsonResponse;

		// Token: 0x0400271B RID: 10011
		public static string KeyCostType;

		// Token: 0x0400271C RID: 10012
		public static string KeyCostAmount;

		// Token: 0x0400271D RID: 10013
		public static string KeyCostCurrency;

		// Token: 0x0400271E RID: 10014
		public static string KeyTestOptionsBaseUrl;

		// Token: 0x0400271F RID: 10015
		public static string KeyTestOptionsGdprUrl;

		// Token: 0x04002720 RID: 10016
		public static string KeyTestOptionsSubscriptionUrl;

		// Token: 0x04002721 RID: 10017
		public static string KeyTestOptionsExtraPath;

		// Token: 0x04002722 RID: 10018
		public static string KeyTestOptionsBasePath;

		// Token: 0x04002723 RID: 10019
		public static string KeyTestOptionsGdprPath;

		// Token: 0x04002724 RID: 10020
		public static string KeyTestOptionsDeleteState;

		// Token: 0x04002725 RID: 10021
		public static string KeyTestOptionsUseTestConnectionOptions;

		// Token: 0x04002726 RID: 10022
		public static string KeyTestOptionsTimerIntervalInMilliseconds;

		// Token: 0x04002727 RID: 10023
		public static string KeyTestOptionsTimerStartInMilliseconds;

		// Token: 0x04002728 RID: 10024
		public static string KeyTestOptionsSessionIntervalInMilliseconds;

		// Token: 0x04002729 RID: 10025
		public static string KeyTestOptionsSubsessionIntervalInMilliseconds;

		// Token: 0x0400272A RID: 10026
		public static string KeyTestOptionsTeardown;

		// Token: 0x0400272B RID: 10027
		public static string KeyTestOptionsNoBackoffWait;

		// Token: 0x0400272C RID: 10028
		public static string KeyTestOptionsiAdFrameworkEnabled;

		// Token: 0x0400272D RID: 10029
		public static string KeyTestOptionsAdServicesFrameworkEnabled;
	}
}
