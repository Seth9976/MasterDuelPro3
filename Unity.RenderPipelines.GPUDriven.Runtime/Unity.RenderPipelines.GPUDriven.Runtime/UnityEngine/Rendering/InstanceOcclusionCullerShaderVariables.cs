using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009F RID: 159
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/InstanceOcclusionCullerShaderVariables.cs", needAccessors = false, generateCBuffer = true)]
	internal struct InstanceOcclusionCullerShaderVariables
	{
		// Token: 0x04000345 RID: 837
		public uint _DrawInfoAllocIndex;

		// Token: 0x04000346 RID: 838
		public uint _DrawInfoCount;

		// Token: 0x04000347 RID: 839
		public uint _InstanceInfoAllocIndex;

		// Token: 0x04000348 RID: 840
		public uint _InstanceInfoCount;

		// Token: 0x04000349 RID: 841
		public int _BoundingSphereInstanceDataAddress;

		// Token: 0x0400034A RID: 842
		public int _DebugCounterIndex;

		// Token: 0x0400034B RID: 843
		public int _InstanceMultiplierShift;

		// Token: 0x0400034C RID: 844
		public int _InstanceOcclusionCullerPad0;
	}
}
