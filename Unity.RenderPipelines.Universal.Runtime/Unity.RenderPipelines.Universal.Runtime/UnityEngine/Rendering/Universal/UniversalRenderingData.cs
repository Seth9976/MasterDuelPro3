using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000C6 RID: 198
	public class UniversalRenderingData : ContextItem
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x00012E98 File Offset: 0x00011098
		internal CommandBuffer commandBuffer
		{
			get
			{
				if (this.m_CommandBuffer == null)
				{
					Debug.LogError("UniversalRenderingData.commandBuffer is null. RenderGraph does not support this property. Please use the command buffer provided by the RenderGraphContext.");
				}
				return this.m_CommandBuffer;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x00012EB2 File Offset: 0x000110B2
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x00012EBA File Offset: 0x000110BA
		public RenderingMode renderingMode { get; internal set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x00012EC3 File Offset: 0x000110C3
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x00012ECB File Offset: 0x000110CB
		public LayerMask opaqueLayerMask { get; internal set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00012ED4 File Offset: 0x000110D4
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x00012EDC File Offset: 0x000110DC
		public LayerMask transparentLayerMask { get; internal set; }

		// Token: 0x060004EC RID: 1260 RVA: 0x00012EE8 File Offset: 0x000110E8
		public override void Reset()
		{
			this.m_CommandBuffer = null;
			this.cullResults = default(CullingResults);
			this.supportsDynamicBatching = false;
			this.perObjectData = PerObjectData.None;
			this.renderingMode = RenderingMode.Forward;
			this.opaqueLayerMask = -1;
			this.transparentLayerMask = -1;
		}

		// Token: 0x04000452 RID: 1106
		internal CommandBuffer m_CommandBuffer;

		// Token: 0x04000453 RID: 1107
		public CullingResults cullResults;

		// Token: 0x04000454 RID: 1108
		public bool supportsDynamicBatching;

		// Token: 0x04000455 RID: 1109
		public PerObjectData perObjectData;
	}
}
