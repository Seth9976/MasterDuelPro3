using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000093 RID: 147
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/InstanceOcclusionCuller.cs", needAccessors = false)]
	internal struct IndirectDrawInfo
	{
		// Token: 0x040002FE RID: 766
		public uint indexCount;

		// Token: 0x040002FF RID: 767
		public uint firstIndex;

		// Token: 0x04000300 RID: 768
		public uint baseVertex;

		// Token: 0x04000301 RID: 769
		public uint firstInstanceGlobalIndex;

		// Token: 0x04000302 RID: 770
		public uint maxInstanceCount;
	}
}
