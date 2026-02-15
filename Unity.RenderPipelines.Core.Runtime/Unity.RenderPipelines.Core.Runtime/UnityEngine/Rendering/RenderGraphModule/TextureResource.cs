using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.RenderGraphModule
{
	// Token: 0x02000273 RID: 627
	[DebuggerDisplay("TextureResource ({desc.name})")]
	internal class TextureResource : RenderGraphResource<TextureDesc, RTHandle>
	{
		// Token: 0x06001118 RID: 4376 RVA: 0x0003DE63 File Offset: 0x0003C063
		public override string GetName()
		{
			if (!this.imported || this.shared)
			{
				return this.desc.name;
			}
			if (this.graphicsResource == null)
			{
				return "null resource";
			}
			return this.graphicsResource.name;
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x0003DE9A File Offset: 0x0003C09A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetDescHashCode()
		{
			return this.desc.GetHashCode();
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0003DEB0 File Offset: 0x0003C0B0
		public override void CreateGraphicsResource()
		{
			string name = this.GetName();
			if (name == "")
			{
				name = string.Format("RenderGraphTexture_{0}", TextureResource.m_TextureCreationIndex++);
			}
			switch (this.desc.sizeMode)
			{
			case TextureSizeMode.Explicit:
				this.graphicsResource = RTHandles.Alloc(this.desc.width, this.desc.height, this.desc.format, this.desc.slices, this.desc.filterMode, this.desc.wrapMode, this.desc.dimension, this.desc.enableRandomWrite, this.desc.useMipMap, this.desc.autoGenerateMips, this.desc.isShadowMap, this.desc.anisoLevel, this.desc.mipMapBias, this.desc.msaaSamples, this.desc.bindTextureMS, this.desc.useDynamicScale, this.desc.useDynamicScaleExplicit, this.desc.memoryless, this.desc.vrUsage, name);
				return;
			case TextureSizeMode.Scale:
				this.graphicsResource = RTHandles.Alloc(this.desc.scale, this.desc.format, this.desc.slices, this.desc.filterMode, this.desc.wrapMode, this.desc.dimension, this.desc.enableRandomWrite, this.desc.useMipMap, this.desc.autoGenerateMips, this.desc.isShadowMap, this.desc.anisoLevel, this.desc.mipMapBias, this.desc.msaaSamples, this.desc.bindTextureMS, this.desc.useDynamicScale, this.desc.useDynamicScaleExplicit, this.desc.memoryless, this.desc.vrUsage, name);
				return;
			case TextureSizeMode.Functor:
				this.graphicsResource = RTHandles.Alloc(this.desc.func, this.desc.format, this.desc.slices, this.desc.filterMode, this.desc.wrapMode, this.desc.dimension, this.desc.enableRandomWrite, this.desc.useMipMap, this.desc.autoGenerateMips, this.desc.isShadowMap, this.desc.anisoLevel, this.desc.mipMapBias, this.desc.msaaSamples, this.desc.bindTextureMS, this.desc.useDynamicScale, this.desc.useDynamicScaleExplicit, this.desc.memoryless, this.desc.vrUsage, name);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0003E190 File Offset: 0x0003C390
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override void UpdateGraphicsResource()
		{
			if (this.graphicsResource != null)
			{
				this.graphicsResource.m_Name = this.GetName();
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0003E1AB File Offset: 0x0003C3AB
		public override void ReleaseGraphicsResource()
		{
			if (this.graphicsResource != null)
			{
				this.graphicsResource.Release();
			}
			base.ReleaseGraphicsResource();
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0003E1C6 File Offset: 0x0003C3C6
		public override void LogCreation(RenderGraphLogger logger)
		{
			logger.LogLine(string.Format("Created Texture: {0} (Cleared: {1})", this.desc.name, this.desc.clearBuffer), Array.Empty<object>());
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x0003E1F8 File Offset: 0x0003C3F8
		public override void LogRelease(RenderGraphLogger logger)
		{
			logger.LogLine("Released Texture: " + this.desc.name, Array.Empty<object>());
		}

		// Token: 0x04000ADB RID: 2779
		private static int m_TextureCreationIndex;
	}
}
