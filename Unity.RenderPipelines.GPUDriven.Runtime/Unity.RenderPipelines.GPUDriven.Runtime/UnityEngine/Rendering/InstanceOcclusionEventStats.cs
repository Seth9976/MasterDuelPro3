using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000023 RID: 35
	internal struct InstanceOcclusionEventStats
	{
		// Token: 0x04000067 RID: 103
		public int viewInstanceID;

		// Token: 0x04000068 RID: 104
		public InstanceOcclusionEventType eventType;

		// Token: 0x04000069 RID: 105
		public int occluderVersion;

		// Token: 0x0400006A RID: 106
		public int subviewMask;

		// Token: 0x0400006B RID: 107
		public OcclusionTest occlusionTest;

		// Token: 0x0400006C RID: 108
		public int visibleInstances;

		// Token: 0x0400006D RID: 109
		public int culledInstances;
	}
}
