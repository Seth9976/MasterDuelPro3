using System;
using System.Runtime.InteropServices;
using UnityEngine.Analytics;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000A4 RID: 164
	[RequiredByNativeCode(GenerateProxy = true)]
	[ExcludeFromDocs]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class BatchRendererGroupRuntimeAnalytic : AnalyticsEventBase
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x00006A44 File Offset: 0x00004C44
		private BatchRendererGroupRuntimeAnalytic()
			: base("brgPlayerUsage", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00006A5C File Offset: 0x00004C5C
		[RequiredByNativeCode]
		public static BatchRendererGroupRuntimeAnalytic CreateBatchRendererGroupRuntimeAnalytic()
		{
			return new BatchRendererGroupRuntimeAnalytic();
		}

		// Token: 0x040001F2 RID: 498
		private int brgRuntimeStatus;
	}
}
