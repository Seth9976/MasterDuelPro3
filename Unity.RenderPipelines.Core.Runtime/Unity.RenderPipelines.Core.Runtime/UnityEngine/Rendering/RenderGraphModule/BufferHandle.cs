using System;
using System.Diagnostics;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200025C RID: 604
	[DebuggerDisplay("Buffer ({handle.index})")]
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct BufferHandle
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0003B6B1 File Offset: 0x000398B1
		public static BufferHandle nullHandle
		{
			get
			{
				return BufferHandle.s_NullHandle;
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0003B6B8 File Offset: 0x000398B8
		internal BufferHandle(in ResourceHandle h)
		{
			this.handle = h;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0003B6C6 File Offset: 0x000398C6
		internal BufferHandle(int handle, bool shared = false)
		{
			this.handle = new ResourceHandle(handle, RenderGraphResourceType.Buffer, shared);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0003B6D6 File Offset: 0x000398D6
		public static implicit operator GraphicsBuffer(BufferHandle buffer)
		{
			if (!buffer.IsValid())
			{
				return null;
			}
			return RenderGraphResourceRegistry.current.GetBuffer(in buffer);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0003B6EF File Offset: 0x000398EF
		public bool IsValid()
		{
			return this.handle.IsValid();
		}

		// Token: 0x04000A79 RID: 2681
		private static BufferHandle s_NullHandle;

		// Token: 0x04000A7A RID: 2682
		internal ResourceHandle handle;
	}
}
