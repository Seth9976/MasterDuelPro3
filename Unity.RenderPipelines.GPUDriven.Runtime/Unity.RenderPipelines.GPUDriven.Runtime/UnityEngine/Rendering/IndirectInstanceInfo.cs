using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000092 RID: 146
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/InstanceOcclusionCuller.cs", needAccessors = false)]
	internal struct IndirectInstanceInfo
	{
		// Token: 0x040002FC RID: 764
		public int drawOffsetAndSplitMask;

		// Token: 0x040002FD RID: 765
		public int instanceIndexAndCrossFade;
	}
}
