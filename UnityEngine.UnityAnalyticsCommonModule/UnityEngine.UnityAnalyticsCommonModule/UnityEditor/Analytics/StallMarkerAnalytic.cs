using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x02000021 RID: 33
	[ExcludeFromDocs]
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class StallMarkerAnalytic : AnalyticsEventBase
	{
		// Token: 0x0600003C RID: 60 RVA: 0x0000251B File Offset: 0x0000071B
		public StallMarkerAnalytic()
			: base("editorStallMarker", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002534 File Offset: 0x00000734
		[RequiredByNativeCode]
		internal static StallMarkerAnalytic CreateStallMarkerAnalytic()
		{
			return new StallMarkerAnalytic();
		}

		// Token: 0x04000058 RID: 88
		public string Name;

		// Token: 0x04000059 RID: 89
		public bool HasProgressMarkup;

		// Token: 0x0400005A RID: 90
		public double Duration;
	}
}
