using System;
using UnityEngine.Rendering.Universal.Internal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000149 RID: 329
	internal class TransparentSettingsPass : ScriptableRenderPass
	{
		// Token: 0x06000722 RID: 1826 RVA: 0x00022BD8 File Offset: 0x00020DD8
		public TransparentSettingsPass(RenderPassEvent evt, bool shadowReceiveSupported)
		{
			base.profilingSampler = new ProfilingSampler("Set Transparent Parameters");
			base.renderPassEvent = evt;
			this.m_shouldReceiveShadows = shadowReceiveSupported;
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00022BFE File Offset: 0x00020DFE
		public bool Setup()
		{
			return !this.m_shouldReceiveShadows;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00022C0C File Offset: 0x00020E0C
		[Obsolete("This rendering path is for compatibility mode only (when Render Graph is disabled). Use Render Graph API instead.", false)]
		public unsafe override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
		{
			CommandBuffer cmd = *renderingData.commandBuffer;
			using (new ProfilingScope(cmd, base.profilingSampler))
			{
				TransparentSettingsPass.ExecutePass(CommandBufferHelpers.GetRasterCommandBuffer(cmd), this.m_shouldReceiveShadows);
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00022C60 File Offset: 0x00020E60
		public static void ExecutePass(RasterCommandBuffer cmd, bool shouldReceiveShadows)
		{
			MainLightShadowCasterPass.SetEmptyMainLightShadowParams(cmd);
			AdditionalLightsShadowCasterPass.SetEmptyAdditionalLightShadowParams(cmd, AdditionalLightsShadowCasterPass.s_EmptyAdditionalLightIndexToShadowParams);
		}

		// Token: 0x040007AC RID: 1964
		private bool m_shouldReceiveShadows;
	}
}
