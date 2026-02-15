using System;
using UnityEngine.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000139 RID: 313
	internal class ProbeVolumeDebugPass : ScriptableRenderPass
	{
		// Token: 0x060006ED RID: 1773 RVA: 0x00020D0C File Offset: 0x0001EF0C
		public ProbeVolumeDebugPass(RenderPassEvent evt, ComputeShader computeShader)
		{
			base.profilingSampler = new ProfilingSampler("Dispatch APV Debug");
			base.renderPassEvent = evt;
			this.m_ComputeShader = computeShader;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00020D32 File Offset: 0x0001EF32
		public void Setup(RTHandle depthBuffer, RTHandle normalBuffer)
		{
			this.m_DepthTexture = depthBuffer;
			this.m_NormalTexture = normalBuffer;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00020D44 File Offset: 0x0001EF44
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			if (!ProbeReferenceVolume.instance.isInitialized)
			{
				return;
			}
			ref CameraData cameraData = ref renderingData.cameraData;
			GraphicsBuffer resultBuffer;
			Vector2 coords;
			if (ProbeReferenceVolume.instance.GetProbeSamplingDebugResources(*cameraData.camera, out resultBuffer, out coords))
			{
				CommandBuffer commandBuffer = *renderingData.commandBuffer;
				int kernel = this.m_ComputeShader.FindKernel("ComputePositionNormal");
				commandBuffer.SetComputeTextureParam(this.m_ComputeShader, kernel, "_CameraDepthTexture", this.m_DepthTexture);
				commandBuffer.SetComputeTextureParam(this.m_ComputeShader, kernel, "_NormalBufferTexture", this.m_NormalTexture);
				commandBuffer.SetComputeVectorParam(this.m_ComputeShader, "_positionSS", new Vector4(coords.x, coords.y, 0f, 0f));
				commandBuffer.SetComputeBufferParam(this.m_ComputeShader, kernel, "_ResultBuffer", resultBuffer);
				commandBuffer.DispatchCompute(this.m_ComputeShader, kernel, 1, 1, 1);
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00020E20 File Offset: 0x0001F020
		internal void Render(RenderGraph renderGraph, ContextContainer frameData, TextureHandle depthPyramidBuffer, TextureHandle normalBuffer)
		{
			UniversalCameraData cameraData = frameData.Get<UniversalCameraData>();
			if (!ProbeReferenceVolume.instance.isInitialized)
			{
				return;
			}
			GraphicsBuffer resultBuffer;
			Vector2 coords;
			if (ProbeReferenceVolume.instance.GetProbeSamplingDebugResources(cameraData.camera, out resultBuffer, out coords))
			{
				ProbeVolumeDebugPass.WriteApvData passData;
				using (IComputeRenderGraphBuilder builder = renderGraph.AddComputePass<ProbeVolumeDebugPass.WriteApvData>(base.passName, out passData, base.profilingSampler, "./Library/PackageCache/com.unity.render-pipelines.universal/Runtime/Passes/ProbeVolumeDebugPass.cs", 81))
				{
					passData.clickCoordinates = coords;
					passData.computeShader = this.m_ComputeShader;
					passData.resultBuffer = renderGraph.ImportBuffer(resultBuffer, false);
					passData.depthBuffer = depthPyramidBuffer;
					passData.normalBuffer = normalBuffer;
					builder.UseBuffer(in passData.resultBuffer, AccessFlags.Write);
					builder.UseTexture(in passData.depthBuffer, AccessFlags.Read);
					builder.UseTexture(in passData.normalBuffer, AccessFlags.Read);
					builder.SetRenderFunc<ProbeVolumeDebugPass.WriteApvData>(delegate(ProbeVolumeDebugPass.WriteApvData data, ComputeGraphContext ctx)
					{
						int kernel = data.computeShader.FindKernel("ComputePositionNormal");
						ctx.cmd.SetComputeTextureParam(data.computeShader, kernel, "_CameraDepthTexture", data.depthBuffer);
						ctx.cmd.SetComputeTextureParam(data.computeShader, kernel, "_NormalBufferTexture", data.normalBuffer);
						ctx.cmd.SetComputeVectorParam(data.computeShader, "_positionSS", new Vector4(data.clickCoordinates.x, data.clickCoordinates.y, 0f, 0f));
						ctx.cmd.SetComputeBufferParam(data.computeShader, kernel, "_ResultBuffer", data.resultBuffer);
						ctx.cmd.DispatchCompute(data.computeShader, kernel, 1, 1, 1);
					});
				}
			}
		}

		// Token: 0x04000724 RID: 1828
		private ComputeShader m_ComputeShader;

		// Token: 0x04000725 RID: 1829
		private RTHandle m_DepthTexture;

		// Token: 0x04000726 RID: 1830
		private RTHandle m_NormalTexture;

		// Token: 0x0200013A RID: 314
		private class WriteApvData
		{
			// Token: 0x04000727 RID: 1831
			public ComputeShader computeShader;

			// Token: 0x04000728 RID: 1832
			public BufferHandle resultBuffer;

			// Token: 0x04000729 RID: 1833
			public Vector2 clickCoordinates;

			// Token: 0x0400072A RID: 1834
			public TextureHandle depthBuffer;

			// Token: 0x0400072B RID: 1835
			public TextureHandle normalBuffer;
		}
	}
}
