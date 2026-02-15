using System;
using UnityEngine.ResourceManagement.Util;

namespace UnityEngine.ResourceManagement.Profiling
{
	// Token: 0x02000070 RID: 112
	internal struct BundleFrameData
	{
		// Token: 0x04000123 RID: 291
		public int BundleCode;

		// Token: 0x04000124 RID: 292
		public int ReferenceCount;

		// Token: 0x04000125 RID: 293
		public float PercentComplete;

		// Token: 0x04000126 RID: 294
		public ContentStatus Status;

		// Token: 0x04000127 RID: 295
		public BundleSource Source;

		// Token: 0x04000128 RID: 296
		public BundleOptions LoadingOptions;
	}
}
