using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000185 RID: 389
	[ExcludeFromPreset]
	public abstract class ScriptableRendererFeature : ScriptableObject, IDisposable
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00027F3C File Offset: 0x0002613C
		public bool isActive
		{
			get
			{
				return this.m_Active;
			}
		}

		// Token: 0x06000844 RID: 2116
		public abstract void Create();

		// Token: 0x06000845 RID: 2117 RVA: 0x0000217F File Offset: 0x0000037F
		public virtual void OnCameraPreCull(ScriptableRenderer renderer, in CameraData cameraData)
		{
		}

		// Token: 0x06000846 RID: 2118
		public abstract void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData);

		// Token: 0x06000847 RID: 2119 RVA: 0x0000217F File Offset: 0x0000037F
		public virtual void SetupRenderPasses(ScriptableRenderer renderer, in RenderingData renderingData)
		{
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00027F44 File Offset: 0x00026144
		private void OnEnable()
		{
			if (RenderPipelineManager.currentPipeline is UniversalRenderPipeline)
			{
				this.Create();
			}
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x00027F44 File Offset: 0x00026144
		private void OnValidate()
		{
			if (RenderPipelineManager.currentPipeline is UniversalRenderPipeline)
			{
				this.Create();
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00002886 File Offset: 0x00000A86
		internal virtual bool SupportsNativeRenderPass()
		{
			return false;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00027F58 File Offset: 0x00026158
		internal virtual bool RequireRenderingLayers(bool isDeferred, bool needsGBufferAccurateNormals, out RenderingLayerUtils.Event atEvent, out RenderingLayerUtils.MaskSize maskSize)
		{
			atEvent = RenderingLayerUtils.Event.DepthNormalPrePass;
			maskSize = RenderingLayerUtils.MaskSize.Bits8;
			return false;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x00027F62 File Offset: 0x00026162
		public void SetActive(bool active)
		{
			this.m_Active = active;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x00027F6B File Offset: 0x0002616B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0000217F File Offset: 0x0000037F
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x040008C1 RID: 2241
		[SerializeField]
		[HideInInspector]
		private bool m_Active = true;
	}
}
