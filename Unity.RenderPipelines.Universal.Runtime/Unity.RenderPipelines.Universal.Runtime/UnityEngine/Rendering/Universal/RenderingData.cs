using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020001C8 RID: 456
	public struct RenderingData
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x000324EC File Offset: 0x000306EC
		internal RenderingData(ContextContainer frameData)
		{
			this.frameData = frameData;
			this.cameraData = new CameraData(frameData);
			this.lightData = new LightData(frameData);
			this.shadowData = new ShadowData(frameData);
			this.postProcessingData = new PostProcessingData(frameData);
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00032525 File Offset: 0x00030725
		internal UniversalRenderingData universalRenderingData
		{
			get
			{
				return this.frameData.Get<UniversalRenderingData>();
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00032532 File Offset: 0x00030732
		internal ref CommandBuffer commandBuffer
		{
			get
			{
				UniversalRenderingData universalRenderingData = this.frameData.Get<UniversalRenderingData>();
				if (universalRenderingData.m_CommandBuffer == null)
				{
					Debug.LogError("RenderingData.commandBuffer is null. RenderGraph does not support this property. Please use the command buffer provided by the RenderGraphContext.");
				}
				return ref universalRenderingData.m_CommandBuffer;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00032552 File Offset: 0x00030752
		public ref CullingResults cullResults
		{
			get
			{
				return ref this.frameData.Get<UniversalRenderingData>().cullResults;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00032564 File Offset: 0x00030764
		public ref bool supportsDynamicBatching
		{
			get
			{
				return ref this.frameData.Get<UniversalRenderingData>().supportsDynamicBatching;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00032576 File Offset: 0x00030776
		public ref PerObjectData perObjectData
		{
			get
			{
				return ref this.frameData.Get<UniversalRenderingData>().perObjectData;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00032588 File Offset: 0x00030788
		public ref bool postProcessingEnabled
		{
			get
			{
				return ref this.frameData.Get<UniversalPostProcessingData>().isEnabled;
			}
		}

		// Token: 0x04000A05 RID: 2565
		internal ContextContainer frameData;

		// Token: 0x04000A06 RID: 2566
		public CameraData cameraData;

		// Token: 0x04000A07 RID: 2567
		public LightData lightData;

		// Token: 0x04000A08 RID: 2568
		public ShadowData shadowData;

		// Token: 0x04000A09 RID: 2569
		public PostProcessingData postProcessingData;
	}
}
