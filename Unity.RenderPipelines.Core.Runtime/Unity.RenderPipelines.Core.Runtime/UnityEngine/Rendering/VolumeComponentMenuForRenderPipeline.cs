using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020001E9 RID: 489
	[Obsolete("VolumeComponentMenuForRenderPipelineAttribute is deprecated. Use VolumeComponentMenu with SupportedOnCurrentPipeline instead. #from(2023.1)", false)]
	public class VolumeComponentMenuForRenderPipeline : VolumeComponentMenu
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00033970 File Offset: 0x00031B70
		public Type[] pipelineTypes { get; }

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00033978 File Offset: 0x00031B78
		public VolumeComponentMenuForRenderPipeline(string menu, params Type[] pipelineTypes)
			: base(menu)
		{
			if (pipelineTypes == null)
			{
				throw new Exception("Specify a list of supported pipeline");
			}
			foreach (Type t in pipelineTypes)
			{
				if (!typeof(RenderPipeline).IsAssignableFrom(t))
				{
					throw new Exception(string.Format("You can only specify types that inherit from {0}, please check {1}", typeof(RenderPipeline), t));
				}
			}
			this.pipelineTypes = pipelineTypes;
		}
	}
}
