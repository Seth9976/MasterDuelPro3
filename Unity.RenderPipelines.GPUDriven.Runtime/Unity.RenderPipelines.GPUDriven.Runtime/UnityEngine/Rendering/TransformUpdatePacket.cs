using System;
using Unity.Mathematics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000088 RID: 136
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false, false, -1, "./Library/PackageCache/com.unity.render-pipelines.core/Runtime/GPUDriven/InstanceData/InstanceTransformUpdateDefs.cs")]
	internal struct TransformUpdatePacket
	{
		// Token: 0x040002C7 RID: 711
		public float4 localToWorld0;

		// Token: 0x040002C8 RID: 712
		public float4 localToWorld1;

		// Token: 0x040002C9 RID: 713
		public float4 localToWorld2;
	}
}
