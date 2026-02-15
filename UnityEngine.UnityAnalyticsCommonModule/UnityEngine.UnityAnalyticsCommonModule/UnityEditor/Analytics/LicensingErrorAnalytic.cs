using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200000E RID: 14
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class LicensingErrorAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000021EB File Offset: 0x000003EB
		public LicensingErrorAnalytic()
			: base("license_error", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002204 File Offset: 0x00000404
		[RequiredByNativeCode]
		internal static LicensingErrorAnalytic CreateLicensingErrorAnalytic()
		{
			return new LicensingErrorAnalytic();
		}

		// Token: 0x04000026 RID: 38
		public string licensingErrorType;

		// Token: 0x04000027 RID: 39
		public string additionalData;

		// Token: 0x04000028 RID: 40
		public string errorMessage;

		// Token: 0x04000029 RID: 41
		public string correlationId;

		// Token: 0x0400002A RID: 42
		public string sessionId;
	}
}
