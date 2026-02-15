using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000014 RID: 20
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class PackageManagerBaseAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000023 RID: 35 RVA: 0x0000230B File Offset: 0x0000050B
		public PackageManagerBaseAnalytic(string eventName)
			: base(eventName, 1, SendEventOptions.kAppendNone, "packageManager")
		{
		}

		// Token: 0x04000039 RID: 57
		public long start_ts;

		// Token: 0x0400003A RID: 58
		public long duration;

		// Token: 0x0400003B RID: 59
		public bool blocking;

		// Token: 0x0400003C RID: 60
		public string package_id;

		// Token: 0x0400003D RID: 61
		public int status_code;

		// Token: 0x0400003E RID: 62
		public string error_message;
	}
}
