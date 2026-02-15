using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200000F RID: 15
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class LicensingInitAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000221B File Offset: 0x0000041B
		public LicensingInitAnalytic()
			: base("license_init", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002234 File Offset: 0x00000434
		[RequiredByNativeCode]
		internal static LicensingInitAnalytic CreateLicensingInitAnalytic()
		{
			return new LicensingInitAnalytic();
		}

		// Token: 0x0400002B RID: 43
		public string licensingProtocolVersion;

		// Token: 0x0400002C RID: 44
		public string licensingClientVersion;

		// Token: 0x0400002D RID: 45
		public string channelType;

		// Token: 0x0400002E RID: 46
		public double initTime;

		// Token: 0x0400002F RID: 47
		public bool isLegacy;

		// Token: 0x04000030 RID: 48
		public string sessionId;

		// Token: 0x04000031 RID: 49
		public string correlationId;
	}
}
