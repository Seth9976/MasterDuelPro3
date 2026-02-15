using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.Universal;

// Token: 0x02000004 RID: 4
public class FullScreenPassRendererFeature : ScriptableRendererFeature, ISerializationCallbackReceiver
{
	// Token: 0x06000003 RID: 3 RVA: 0x00002058 File Offset: 0x00000258
	public override void Create()
	{
		this.m_FullScreenPass = new FullScreenPassRendererFeature.FullScreenRenderPass(base.name);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000206B File Offset: 0x0000026B
	internal override bool RequireRenderingLayers(bool isDeferred, bool needsGBufferAccurateNormals, out RenderingLayerUtils.Event atEvent, out RenderingLayerUtils.MaskSize maskSize)
	{
		atEvent = RenderingLayerUtils.Event.Opaque;
		maskSize = RenderingLayerUtils.MaskSize.Bits8;
		return false;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002078 File Offset: 0x00000278
	public unsafe override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
	{
		if (*renderingData.cameraData.cameraType == CameraType.Preview || *renderingData.cameraData.cameraType == CameraType.Reflection || UniversalRenderer.IsOffscreenDepthTexture(ref renderingData.cameraData))
		{
			return;
		}
		if (this.passMaterial == null)
		{
			Debug.LogWarningFormat("The full screen feature \"{0}\" will not execute - no material is assigned. Please make sure a material is assigned for this feature on the renderer asset.", new object[] { base.name });
			return;
		}
		if (this.passIndex < 0 || this.passIndex >= this.passMaterial.passCount)
		{
			Debug.LogWarningFormat("The full screen feature \"{0}\" will not execute - the pass index is out of bounds for the material.", new object[] { base.name });
			return;
		}
		this.m_FullScreenPass.renderPassEvent = (RenderPassEvent)this.injectionPoint;
		this.m_FullScreenPass.ConfigureInput(this.requirements);
		this.m_FullScreenPass.SetupMembers(this.passMaterial, this.passIndex, this.fetchColorBuffer, this.bindDepthStencilAttachment);
		this.m_FullScreenPass.requiresIntermediateTexture = this.fetchColorBuffer;
		renderer.EnqueuePass(this.m_FullScreenPass);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002172 File Offset: 0x00000372
	protected override void Dispose(bool disposing)
	{
		this.m_FullScreenPass.Dispose();
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000217F File Offset: 0x0000037F
	private void UpgradeIfNeeded()
	{
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002181 File Offset: 0x00000381
	void ISerializationCallbackReceiver.OnBeforeSerialize()
	{
		if (this.m_Version == FullScreenPassRendererFeature.Version.Uninitialised)
		{
			this.m_Version = FullScreenPassRendererFeature.Version.AddFetchColorBufferCheckbox;
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002193 File Offset: 0x00000393
	void ISerializationCallbackReceiver.OnAfterDeserialize()
	{
		if (this.m_Version == FullScreenPassRendererFeature.Version.Uninitialised)
		{
			this.m_Version = FullScreenPassRendererFeature.Version.Initial;
		}
		this.UpgradeIfNeeded();
	}

	// Token: 0x04000001 RID: 1
	public FullScreenPassRendererFeature.InjectionPoint injectionPoint = FullScreenPassRendererFeature.InjectionPoint.AfterRenderingPostProcessing;

	// Token: 0x04000002 RID: 2
	public bool fetchColorBuffer = true;

	// Token: 0x04000003 RID: 3
	public ScriptableRenderPassInput requirements;

	// Token: 0x04000004 RID: 4
	public Material passMaterial;

	// Token: 0x04000005 RID: 5
	public int passIndex;

	// Token: 0x04000006 RID: 6
	public bool bindDepthStencilAttachment;

	// Token: 0x04000007 RID: 7
	private FullScreenPassRendererFeature.FullScreenRenderPass m_FullScreenPass;

	// Token: 0x04000008 RID: 8
	[SerializeField]
	[HideInInspector]
	private FullScreenPassRendererFeature.Version m_Version = FullScreenPassRendererFeature.Version.Uninitialised;

	// Token: 0x02000005 RID: 5
	public enum InjectionPoint
	{
		// Token: 0x0400000A RID: 10
		BeforeRenderingTransparents = 450,
		// Token: 0x0400000B RID: 11
		BeforeRenderingPostProcessing = 550,
		// Token: 0x0400000C RID: 12
		AfterRenderingPostProcessing = 600
	}

	// Token: 0x02000006 RID: 6
	internal class FullScreenRenderPass : ScriptableRenderPass
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021CC File Offset: 0x000003CC
		public FullScreenRenderPass(string passName)
		{
			base.profilingSampler = new ProfilingSampler(passName);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021E0 File Offset: 0x000003E0
		public void SetupMembers(Material material, int passIndex, bool fetchActiveColor, bool bindDepthStencilAttachment)
		{
			this.m_Material = material;
			this.m_PassIndex = passIndex;
			this.m_FetchActiveColor = fetchActiveColor;
			this.m_BindDepthStencilAttachment = bindDepthStencilAttachment;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021FF File Offset: 0x000003FF
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
		{
			base.ResetTarget();
			if (this.m_FetchActiveColor)
			{
				this.ReAllocate(*renderingData.cameraData.cameraTargetDescriptor);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002225 File Offset: 0x00000425
		internal void ReAllocate(RenderTextureDescriptor desc)
		{
			desc.msaaSamples = 1;
			desc.depthStencilFormat = GraphicsFormat.None;
			RenderingUtils.ReAllocateHandleIfNeeded(ref this.m_CopiedColor, in desc, FilterMode.Point, TextureWrapMode.Repeat, 1, 0f, "_FullscreenPassColorCopy");
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002252 File Offset: 0x00000452
		public void Dispose()
		{
			RTHandle copiedColor = this.m_CopiedColor;
			if (copiedColor == null)
			{
				return;
			}
			copiedColor.Release();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002264 File Offset: 0x00000464
		private static void ExecuteCopyColorPass(RasterCommandBuffer cmd, RTHandle sourceTexture)
		{
			Blitter.BlitTexture(cmd, sourceTexture, new Vector4(1f, 1f, 0f, 0f), 0f, false);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000228C File Offset: 0x0000048C
		private static void ExecuteMainPass(RasterCommandBuffer cmd, RTHandle sourceTexture, Material material, int passIndex)
		{
			FullScreenPassRendererFeature.FullScreenRenderPass.s_SharedPropertyBlock.Clear();
			if (sourceTexture != null)
			{
				FullScreenPassRendererFeature.FullScreenRenderPass.s_SharedPropertyBlock.SetTexture(ShaderPropertyId.blitTexture, sourceTexture);
			}
			FullScreenPassRendererFeature.FullScreenRenderPass.s_SharedPropertyBlock.SetVector(ShaderPropertyId.blitScaleBias, new Vector4(1f, 1f, 0f, 0f));
			cmd.DrawProcedural(Matrix4x4.identity, material, passIndex, MeshTopology.Triangles, 3, 1, FullScreenPassRendererFeature.FullScreenRenderPass.s_SharedPropertyBlock);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022F8 File Offset: 0x000004F8
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			ref CameraData cameraData = ref renderingData.cameraData;
			CommandBuffer cmd = *renderingData.commandBuffer;
			using (new ProfilingScope(cmd, base.profilingSampler))
			{
				RasterCommandBuffer rasterCmd = CommandBufferHelpers.GetRasterCommandBuffer(cmd);
				if (this.m_FetchActiveColor)
				{
					CoreUtils.SetRenderTarget(cmd, this.m_CopiedColor, ClearFlag.None, 0, CubemapFace.Unknown, -1);
					FullScreenPassRendererFeature.FullScreenRenderPass.ExecuteCopyColorPass(rasterCmd, cameraData.renderer->cameraColorTargetHandle);
				}
				if (this.m_BindDepthStencilAttachment)
				{
					CoreUtils.SetRenderTarget(cmd, cameraData.renderer->cameraColorTargetHandle, cameraData.renderer->cameraDepthTargetHandle, 0, CubemapFace.Unknown, -1);
				}
				else
				{
					CoreUtils.SetRenderTarget(cmd, cameraData.renderer->cameraColorTargetHandle, ClearFlag.None, 0, CubemapFace.Unknown, -1);
				}
				FullScreenPassRendererFeature.FullScreenRenderPass.ExecuteMainPass(rasterCmd, this.m_FetchActiveColor ? this.m_CopiedColor : null, this.m_Material, this.m_PassIndex);
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023D8 File Offset: 0x000005D8
		public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
		{
			UniversalResourceData resourcesData = frameData.Get<UniversalResourceData>();
			frameData.Get<UniversalCameraData>();
			TextureHandle source;
			TextureHandle destination;
			if (this.m_FetchActiveColor)
			{
				TextureDesc targetDesc = renderGraph.GetTextureDesc(resourcesData.cameraColor);
				targetDesc.name = "_CameraColorFullScreenPass";
				targetDesc.clearBuffer = false;
				source = resourcesData.activeColorTexture;
				destination = renderGraph.CreateTexture(in targetDesc);
				FullScreenPassRendererFeature.FullScreenRenderPass.CopyPassData passData;
				using (IRasterRenderGraphBuilder builder = renderGraph.AddRasterRenderPass<FullScreenPassRendererFeature.FullScreenRenderPass.CopyPassData>("Copy Color Full Screen", out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/RendererFeatures/FullScreenPassRendererFeature.cs", 227))
				{
					passData.inputTexture = source;
					builder.UseTexture(in passData.inputTexture, AccessFlags.Read);
					builder.SetRenderAttachment(destination, 0, AccessFlags.Write);
					builder.SetRenderFunc<FullScreenPassRendererFeature.FullScreenRenderPass.CopyPassData>(delegate(FullScreenPassRendererFeature.FullScreenRenderPass.CopyPassData data, RasterGraphContext rgContext)
					{
						FullScreenPassRendererFeature.FullScreenRenderPass.ExecuteCopyColorPass(rgContext.cmd, data.inputTexture);
					});
				}
				source = destination;
			}
			else
			{
				source = TextureHandle.nullHandle;
			}
			destination = resourcesData.activeColorTexture;
			FullScreenPassRendererFeature.FullScreenRenderPass.MainPassData passData2;
			using (IRasterRenderGraphBuilder builder2 = renderGraph.AddRasterRenderPass<FullScreenPassRendererFeature.FullScreenRenderPass.MainPassData>(base.passName, out passData2, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/RendererFeatures/FullScreenPassRendererFeature.cs", 251))
			{
				passData2.material = this.m_Material;
				passData2.passIndex = this.m_PassIndex;
				passData2.inputTexture = source;
				if (passData2.inputTexture.IsValid())
				{
					builder2.UseTexture(in passData2.inputTexture, AccessFlags.Read);
				}
				bool flag = (base.input & ScriptableRenderPassInput.Color) > ScriptableRenderPassInput.None;
				bool needsDepth = (base.input & ScriptableRenderPassInput.Depth) > ScriptableRenderPassInput.None;
				bool needsMotion = (base.input & ScriptableRenderPassInput.Motion) > ScriptableRenderPassInput.None;
				bool needsNormal = (base.input & ScriptableRenderPassInput.Normal) > ScriptableRenderPassInput.None;
				if (flag)
				{
					IBaseRenderGraphBuilder baseRenderGraphBuilder = builder2;
					TextureHandle textureHandle = resourcesData.cameraOpaqueTexture;
					baseRenderGraphBuilder.UseTexture(in textureHandle, AccessFlags.Read);
				}
				if (needsDepth)
				{
					IBaseRenderGraphBuilder baseRenderGraphBuilder2 = builder2;
					TextureHandle textureHandle = resourcesData.cameraDepthTexture;
					baseRenderGraphBuilder2.UseTexture(in textureHandle, AccessFlags.Read);
				}
				if (needsMotion)
				{
					IBaseRenderGraphBuilder baseRenderGraphBuilder3 = builder2;
					TextureHandle textureHandle = resourcesData.motionVectorColor;
					baseRenderGraphBuilder3.UseTexture(in textureHandle, AccessFlags.Read);
					IBaseRenderGraphBuilder baseRenderGraphBuilder4 = builder2;
					textureHandle = resourcesData.motionVectorDepth;
					baseRenderGraphBuilder4.UseTexture(in textureHandle, AccessFlags.Read);
				}
				if (needsNormal)
				{
					IBaseRenderGraphBuilder baseRenderGraphBuilder5 = builder2;
					TextureHandle textureHandle = resourcesData.cameraNormalsTexture;
					baseRenderGraphBuilder5.UseTexture(in textureHandle, AccessFlags.Read);
				}
				builder2.SetRenderAttachment(destination, 0, AccessFlags.Write);
				if (this.m_BindDepthStencilAttachment)
				{
					builder2.SetRenderAttachmentDepth(resourcesData.activeDepthTexture, AccessFlags.Write);
				}
				builder2.SetRenderFunc<FullScreenPassRendererFeature.FullScreenRenderPass.MainPassData>(delegate(FullScreenPassRendererFeature.FullScreenRenderPass.MainPassData data, RasterGraphContext rgContext)
				{
					FullScreenPassRendererFeature.FullScreenRenderPass.ExecuteMainPass(rgContext.cmd, data.inputTexture, data.material, data.passIndex);
				});
			}
		}

		// Token: 0x0400000D RID: 13
		private Material m_Material;

		// Token: 0x0400000E RID: 14
		private int m_PassIndex;

		// Token: 0x0400000F RID: 15
		private bool m_FetchActiveColor;

		// Token: 0x04000010 RID: 16
		private bool m_BindDepthStencilAttachment;

		// Token: 0x04000011 RID: 17
		private RTHandle m_CopiedColor;

		// Token: 0x04000012 RID: 18
		private static MaterialPropertyBlock s_SharedPropertyBlock = new MaterialPropertyBlock();

		// Token: 0x02000007 RID: 7
		private class CopyPassData
		{
			// Token: 0x04000013 RID: 19
			internal TextureHandle inputTexture;
		}

		// Token: 0x02000008 RID: 8
		private class MainPassData
		{
			// Token: 0x04000014 RID: 20
			internal Material material;

			// Token: 0x04000015 RID: 21
			internal int passIndex;

			// Token: 0x04000016 RID: 22
			internal TextureHandle inputTexture;
		}
	}

	// Token: 0x0200000A RID: 10
	private enum Version
	{
		// Token: 0x0400001B RID: 27
		Uninitialised = -1,
		// Token: 0x0400001C RID: 28
		Initial,
		// Token: 0x0400001D RID: 29
		AddFetchColorBufferCheckbox,
		// Token: 0x0400001E RID: 30
		Count,
		// Token: 0x0400001F RID: 31
		Latest = 1
	}
}
