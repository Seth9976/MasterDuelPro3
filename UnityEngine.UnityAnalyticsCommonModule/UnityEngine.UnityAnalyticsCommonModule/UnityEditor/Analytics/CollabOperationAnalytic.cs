using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEditor.Analytics
{
	// Token: 0x0200000D RID: 13
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class CollabOperationAnalytic : AnalyticsEventBase
	{
		// Token: 0x06000015 RID: 21 RVA: 0x000021BB File Offset: 0x000003BB
		public CollabOperationAnalytic()
			: base("collabOperation", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021D4 File Offset: 0x000003D4
		[RequiredByNativeCode]
		internal static CollabOperationAnalytic CreateCollabOperationAnalytic()
		{
			return new CollabOperationAnalytic();
		}

		// Token: 0x04000021 RID: 33
		public string category;

		// Token: 0x04000022 RID: 34
		public string operation;

		// Token: 0x04000023 RID: 35
		public string result;

		// Token: 0x04000024 RID: 36
		public long start_ts;

		// Token: 0x04000025 RID: 37
		public long duration;
	}
}
