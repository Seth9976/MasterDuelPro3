using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200006E RID: 110
	public class UniversalRenderPipelineDebugDisplaySettings : DebugDisplaySettings<UniversalRenderPipelineDebugDisplaySettings>
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000084EA File Offset: 0x000066EA
		// (set) Token: 0x06000272 RID: 626 RVA: 0x000084F2 File Offset: 0x000066F2
		private DebugDisplaySettingsCommon commonSettings { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000273 RID: 627 RVA: 0x000084FB File Offset: 0x000066FB
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00008503 File Offset: 0x00006703
		public DebugDisplaySettingsMaterial materialSettings { get; private set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000850C File Offset: 0x0000670C
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00008514 File Offset: 0x00006714
		public DebugDisplaySettingsRendering renderingSettings { get; private set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000851D File Offset: 0x0000671D
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00008525 File Offset: 0x00006725
		public DebugDisplaySettingsLighting lightingSettings { get; private set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000852E File Offset: 0x0000672E
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00008536 File Offset: 0x00006736
		public DebugDisplaySettingsVolume volumeSettings { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000853F File Offset: 0x0000673F
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00008547 File Offset: 0x00006747
		internal DebugDisplaySettingsStats<URPProfileId> displayStats { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00008550 File Offset: 0x00006750
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00008558 File Offset: 0x00006758
		internal DebugDisplayGPUResidentDrawer gpuResidentDrawerSettings { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00008564 File Offset: 0x00006764
		public override bool IsPostProcessingAllowed
		{
			get
			{
				DebugPostProcessingMode debugPostProcessingMode = this.renderingSettings.postProcessingDebugMode;
				switch (debugPostProcessingMode)
				{
				case DebugPostProcessingMode.Disabled:
					return false;
				case DebugPostProcessingMode.Auto:
				{
					bool postProcessingAllowed = true;
					foreach (IDebugDisplaySettingsData setting in this.m_Settings)
					{
						postProcessingAllowed &= setting.IsPostProcessingAllowed;
					}
					return postProcessingAllowed;
				}
				case DebugPostProcessingMode.Enabled:
					return true;
				default:
					throw new ArgumentOutOfRangeException("debugPostProcessingMode", string.Format("Invalid post-processing state {0}", debugPostProcessingMode));
				}
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x000085FC File Offset: 0x000067FC
		public UniversalRenderPipelineDebugDisplaySettings()
		{
			this.Reset();
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000860C File Offset: 0x0000680C
		public override void Reset()
		{
			base.Reset();
			this.displayStats = base.Add<DebugDisplaySettingsStats<URPProfileId>>(new DebugDisplaySettingsStats<URPProfileId>(new UniversalRenderPipelineDebugDisplayStats()));
			this.materialSettings = base.Add<DebugDisplaySettingsMaterial>(new DebugDisplaySettingsMaterial());
			this.lightingSettings = base.Add<DebugDisplaySettingsLighting>(new DebugDisplaySettingsLighting());
			this.renderingSettings = base.Add<DebugDisplaySettingsRendering>(new DebugDisplaySettingsRendering());
			this.volumeSettings = base.Add<DebugDisplaySettingsVolume>(new DebugDisplaySettingsVolume(new UniversalRenderPipelineVolumeDebugSettings()));
			this.commonSettings = base.Add<DebugDisplaySettingsCommon>(new DebugDisplaySettingsCommon());
			this.gpuResidentDrawerSettings = base.Add<DebugDisplayGPUResidentDrawer>(new DebugDisplayGPUResidentDrawer());
			Texture.streamingTextureDiscardUnusedMips = false;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x000086A6 File Offset: 0x000068A6
		internal void UpdateDisplayStats()
		{
			if (this.displayStats != null)
			{
				this.displayStats.debugDisplayStats.Update();
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x000086C0 File Offset: 0x000068C0
		internal void UpdateMaterials()
		{
			if (this.renderingSettings.mipInfoMode != DebugMipInfoMode.None)
			{
				Texture.SetStreamingTextureMaterialDebugProperties((this.renderingSettings.canAggregateData && this.renderingSettings.showInfoForAllSlots) ? (-1) : this.renderingSettings.mipDebugMaterialTextureSlot);
			}
		}
	}
}
