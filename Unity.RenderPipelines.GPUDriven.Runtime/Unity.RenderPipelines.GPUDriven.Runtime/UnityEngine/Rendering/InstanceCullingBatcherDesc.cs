using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200004B RID: 75
	internal struct InstanceCullingBatcherDesc
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00008838 File Offset: 0x00006A38
		public static InstanceCullingBatcherDesc NewDefault()
		{
			return new InstanceCullingBatcherDesc
			{
				onCompleteCallback = null
			};
		}

		// Token: 0x0400014F RID: 335
		public OnCullingCompleteCallback onCompleteCallback;
	}
}
