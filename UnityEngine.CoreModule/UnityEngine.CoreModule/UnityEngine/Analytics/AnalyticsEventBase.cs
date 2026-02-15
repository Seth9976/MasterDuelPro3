using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x0200031A RID: 794
	[RequiredByNativeCode(GenerateProxy = true)]
	[StructLayout(LayoutKind.Sequential)]
	public class AnalyticsEventBase
	{
		// Token: 0x06001616 RID: 5654 RVA: 0x0002E668 File Offset: 0x0002C868
		public AnalyticsEventBase(string eventName, int eventVersion, SendEventOptions sendEventOptions = SendEventOptions.kAppendNone, string eventPrefix = "")
		{
			this.eventName = eventName;
			this.eventVersion = eventVersion;
			this.sendEventOptions = sendEventOptions;
			this.eventPrefix = eventPrefix;
		}

		// Token: 0x04000838 RID: 2104
		private string eventName;

		// Token: 0x04000839 RID: 2105
		private int eventVersion;

		// Token: 0x0400083A RID: 2106
		private string eventPrefix;

		// Token: 0x0400083B RID: 2107
		private SendEventOptions sendEventOptions;
	}
}
