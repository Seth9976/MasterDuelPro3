using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003BE RID: 958
	public abstract class RenderPipelineAsset<TRenderPipeline> : RenderPipelineAsset where TRenderPipeline : RenderPipeline
	{
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x00038772 File Offset: 0x00036972
		public sealed override Type pipelineType
		{
			get
			{
				return typeof(TRenderPipeline);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x0003877E File Offset: 0x0003697E
		public override string renderPipelineShaderTag
		{
			get
			{
				return typeof(TRenderPipeline).Name;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x00038772 File Offset: 0x00036972
		[Obsolete("This property is obsolete. Use pipelineType instead. #from(23.2)", false)]
		protected internal sealed override Type renderPipelineType
		{
			get
			{
				return typeof(TRenderPipeline);
			}
		}
	}
}
