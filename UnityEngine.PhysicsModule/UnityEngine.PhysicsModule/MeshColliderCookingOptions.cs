using System;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[Flags]
	public enum MeshColliderCookingOptions
	{
		// Token: 0x04000022 RID: 34
		None = 0,
		// Token: 0x04000023 RID: 35
		[Obsolete("No longer used because the problem this was trying to solve is gone since Unity 2018.3", true)]
		InflateConvexMesh = 1,
		// Token: 0x04000024 RID: 36
		CookForFasterSimulation = 2,
		// Token: 0x04000025 RID: 37
		EnableMeshCleaning = 4,
		// Token: 0x04000026 RID: 38
		WeldColocatedVertices = 8,
		// Token: 0x04000027 RID: 39
		UseFastMidphase = 16
	}
}
