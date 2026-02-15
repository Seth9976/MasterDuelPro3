using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x0200026F RID: 623
	[DebuggerDisplay("Texture ({handle.index})")]
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.RenderGraphModule", "UnityEngine.Rendering.RenderGraphModule", null)]
	public struct TextureHandle
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001100 RID: 4352 RVA: 0x0003D8D7 File Offset: 0x0003BAD7
		public static TextureHandle nullHandle
		{
			get
			{
				return TextureHandle.s_NullHandle;
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x0003D8DE File Offset: 0x0003BADE
		internal TextureHandle(in ResourceHandle h)
		{
			this.handle = h;
			this.builtin = false;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x0003D8F3 File Offset: 0x0003BAF3
		internal TextureHandle(int handle, bool shared = false, bool builtin = false)
		{
			this.handle = new ResourceHandle(handle, RenderGraphResourceType.Texture, shared);
			this.builtin = builtin;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x0003D90C File Offset: 0x0003BB0C
		public static implicit operator RenderTargetIdentifier(TextureHandle texture)
		{
			if (!texture.IsValid())
			{
				return default(RenderTargetIdentifier);
			}
			return RenderGraphResourceRegistry.current.GetTexture(in texture);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x0003D93D File Offset: 0x0003BB3D
		public static implicit operator Texture(TextureHandle texture)
		{
			return texture.IsValid() ? RenderGraphResourceRegistry.current.GetTexture(in texture) : null;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x0003D95C File Offset: 0x0003BB5C
		public static implicit operator RenderTexture(TextureHandle texture)
		{
			return texture.IsValid() ? RenderGraphResourceRegistry.current.GetTexture(in texture) : null;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x0003D97B File Offset: 0x0003BB7B
		public static implicit operator RTHandle(TextureHandle texture)
		{
			if (!texture.IsValid())
			{
				return null;
			}
			return RenderGraphResourceRegistry.current.GetTexture(in texture);
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x0003D994 File Offset: 0x0003BB94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsValid()
		{
			return this.handle.IsValid();
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x0003D9A1 File Offset: 0x0003BBA1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool IsBuiltin()
		{
			return this.builtin;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0003D9A9 File Offset: 0x0003BBA9
		public TextureDesc GetDescriptor(RenderGraph renderGraph)
		{
			return renderGraph.GetTextureDesc(this);
		}

		// Token: 0x04000AB4 RID: 2740
		private static TextureHandle s_NullHandle;

		// Token: 0x04000AB5 RID: 2741
		internal ResourceHandle handle;

		// Token: 0x04000AB6 RID: 2742
		private bool builtin;
	}
}
