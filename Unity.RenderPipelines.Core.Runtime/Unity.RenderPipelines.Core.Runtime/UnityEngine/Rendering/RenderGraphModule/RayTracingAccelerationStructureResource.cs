using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200025B RID: 603
	[DebuggerDisplay("RayTracingAccelerationStructureResource ({desc.name})")]
	internal class RayTracingAccelerationStructureResource : RenderGraphResource<RayTracingAccelerationStructureDesc, RayTracingAccelerationStructure>
	{
		// Token: 0x06001060 RID: 4192 RVA: 0x0003B69C File Offset: 0x0003989C
		public override string GetName()
		{
			return this.desc.name;
		}
	}
}
