using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000160 RID: 352
	public abstract class RenderPipelineGlobalSettings<TGlobalRenderPipelineSettings, TRenderPipeline> : RenderPipelineGlobalSettings where TGlobalRenderPipelineSettings : RenderPipelineGlobalSettings where TRenderPipeline : RenderPipeline
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00024EB6 File Offset: 0x000230B6
		public static TGlobalRenderPipelineSettings instance
		{
			get
			{
				return RenderPipelineGlobalSettings<TGlobalRenderPipelineSettings, TRenderPipeline>.s_Instance.Value;
			}
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x00005704 File Offset: 0x00003904
		public virtual void Reset()
		{
		}

		// Token: 0x040006C2 RID: 1730
		private static Lazy<TGlobalRenderPipelineSettings> s_Instance = new Lazy<TGlobalRenderPipelineSettings>(() => GraphicsSettings.GetSettingsForRenderPipeline<TRenderPipeline>() as TGlobalRenderPipelineSettings);
	}
}
