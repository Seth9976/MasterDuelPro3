using System;
using System.Diagnostics;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000259 RID: 601
	[DebuggerDisplay("RayTracingAccelerationStructure ({handle.index})")]
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct RayTracingAccelerationStructureHandle
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x0003B65F File Offset: 0x0003985F
		public static RayTracingAccelerationStructureHandle nullHandle
		{
			get
			{
				return RayTracingAccelerationStructureHandle.s_NullHandle;
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0003B666 File Offset: 0x00039866
		internal RayTracingAccelerationStructureHandle(int handle)
		{
			this.handle = new ResourceHandle(handle, RenderGraphResourceType.AccelerationStructure, false);
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0003B676 File Offset: 0x00039876
		public static implicit operator RayTracingAccelerationStructure(RayTracingAccelerationStructureHandle handle)
		{
			if (!handle.IsValid())
			{
				return null;
			}
			return RenderGraphResourceRegistry.current.GetRayTracingAccelerationStructure(in handle);
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0003B68F File Offset: 0x0003988F
		public bool IsValid()
		{
			return this.handle.IsValid();
		}

		// Token: 0x04000A76 RID: 2678
		private static RayTracingAccelerationStructureHandle s_NullHandle;

		// Token: 0x04000A77 RID: 2679
		internal ResourceHandle handle;
	}
}
