using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000324 RID: 804
	[Flags]
	public enum RayTracingAccelerationStructureBuildFlags
	{
		// Token: 0x04000861 RID: 2145
		None = 0,
		// Token: 0x04000862 RID: 2146
		PreferFastTrace = 1,
		// Token: 0x04000863 RID: 2147
		PreferFastBuild = 2,
		// Token: 0x04000864 RID: 2148
		MinimizeMemory = 4
	}
}
