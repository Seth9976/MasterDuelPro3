using System;

namespace Unity.Profiling.LowLevel
{
	// Token: 0x02000030 RID: 48
	public enum ProfilerMarkerDataType : byte
	{
		// Token: 0x0400007C RID: 124
		InstanceId = 1,
		// Token: 0x0400007D RID: 125
		Int32,
		// Token: 0x0400007E RID: 126
		UInt32,
		// Token: 0x0400007F RID: 127
		Int64,
		// Token: 0x04000080 RID: 128
		UInt64,
		// Token: 0x04000081 RID: 129
		Float,
		// Token: 0x04000082 RID: 130
		Double,
		// Token: 0x04000083 RID: 131
		String16 = 9,
		// Token: 0x04000084 RID: 132
		Blob8 = 11,
		// Token: 0x04000085 RID: 133
		GfxResourceId
	}
}
