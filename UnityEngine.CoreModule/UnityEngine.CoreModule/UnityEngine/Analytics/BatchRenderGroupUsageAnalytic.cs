using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine.Analytics
{
	// Token: 0x0200031B RID: 795
	[RequiredByNativeCode(GenerateProxy = true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class BatchRenderGroupUsageAnalytic : AnalyticsEventBase
	{
		// Token: 0x06001617 RID: 5655 RVA: 0x0002E68F File Offset: 0x0002C88F
		public BatchRenderGroupUsageAnalytic()
			: base("brgUsageEvent", 1, SendEventOptions.kAppendNone, "")
		{
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0002E6A8 File Offset: 0x0002C8A8
		[RequiredByNativeCode]
		public static BatchRenderGroupUsageAnalytic CreateBatchRenderGroupUsageAnalytic()
		{
			return new BatchRenderGroupUsageAnalytic();
		}

		// Token: 0x0400083C RID: 2108
		public int maxBRGInstance;

		// Token: 0x0400083D RID: 2109
		public int maxMeshCount;

		// Token: 0x0400083E RID: 2110
		public int maxMaterialCount;

		// Token: 0x0400083F RID: 2111
		public int maxDrawCommandBatch;
	}
}
