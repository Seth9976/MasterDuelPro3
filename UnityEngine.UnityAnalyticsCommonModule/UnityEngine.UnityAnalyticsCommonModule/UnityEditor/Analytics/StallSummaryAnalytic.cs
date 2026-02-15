using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000020 RID: 32
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class StallSummaryAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000024EB File Offset: 0x000006EB
		public StallSummaryAnalytic()
			: base("editorStallSummary", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002504 File Offset: 0x00000704
		[RequiredByNativeCode]
		internal static StallSummaryAnalytic CreateStallSummaryAnalytic()
		{
			return new StallSummaryAnalytic();
		}

		// Token: 0x04000057 RID: 87
		public double Duration;
	}
}
