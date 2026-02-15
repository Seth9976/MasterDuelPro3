using System;
using System.Diagnostics;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200026B RID: 619
	[DebuggerDisplay("RendererList ({handle})")]
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct RendererListHandle
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0003D832 File Offset: 0x0003BA32
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x0003D83A File Offset: 0x0003BA3A
		internal int handle { readonly get; private set; }

		// Token: 0x060010F9 RID: 4345 RVA: 0x0003D843 File Offset: 0x0003BA43
		internal RendererListHandle(int handle, RendererListHandleType type = RendererListHandleType.Renderers)
		{
			this.handle = handle;
			this.m_IsValid = true;
			this.type = type;
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0003D85A File Offset: 0x0003BA5A
		public static implicit operator int(RendererListHandle handle)
		{
			return handle.handle;
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0003D863 File Offset: 0x0003BA63
		public static implicit operator RendererList(RendererListHandle rendererList)
		{
			if (!rendererList.IsValid())
			{
				return RendererList.nullRendererList;
			}
			return RenderGraphResourceRegistry.current.GetRendererList(in rendererList);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0003D880 File Offset: 0x0003BA80
		public bool IsValid()
		{
			return this.m_IsValid;
		}

		// Token: 0x04000AA9 RID: 2729
		internal RendererListHandleType type;

		// Token: 0x04000AAA RID: 2730
		private bool m_IsValid;
	}
}
