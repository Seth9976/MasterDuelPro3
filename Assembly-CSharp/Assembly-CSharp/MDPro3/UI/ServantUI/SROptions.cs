using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace MDPro3.UI.ServantUI
{
	// Token: 0x02001483 RID: 5251
	public class SROptions
	{
		// Token: 0x060099C5 RID: 39365 RVA: 0x0016EA88 File Offset: 0x0016CC88
		private void InitializeShadowMapFieldInfo()
		{
			this.urpa = Resources.Load<UniversalRenderPipelineAsset>("Settings/URPAsset");
			this.universalRenderPipelineAssetType = this.urpa.GetType();
			this.mainLightShadowmapResolutionFieldInfo = this.universalRenderPipelineAssetType.GetField("m_MainLightShadowmapResolution", BindingFlags.Instance | BindingFlags.NonPublic);
			this.supportsSoftShadowsFieldInfo = this.universalRenderPipelineAssetType.GetField("m_SoftShadowsSupported", BindingFlags.Instance | BindingFlags.NonPublic);
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x060099C6 RID: 39366 RVA: 0x0016EAE6 File Offset: 0x0016CCE6
		// (set) Token: 0x060099C7 RID: 39367 RVA: 0x0016EB12 File Offset: 0x0016CD12
		public ShadowResolution MainLightShadowResolution
		{
			get
			{
				if (this.mainLightShadowmapResolutionFieldInfo == null)
				{
					this.InitializeShadowMapFieldInfo();
				}
				return (ShadowResolution)this.mainLightShadowmapResolutionFieldInfo.GetValue(this.urpa);
			}
			set
			{
				if (this.mainLightShadowmapResolutionFieldInfo == null)
				{
					this.InitializeShadowMapFieldInfo();
				}
				this.mainLightShadowmapResolutionFieldInfo.SetValue(this.urpa, value);
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x060099C8 RID: 39368 RVA: 0x0016EB3F File Offset: 0x0016CD3F
		// (set) Token: 0x060099C9 RID: 39369 RVA: 0x0016EB6B File Offset: 0x0016CD6B
		public bool SupportsSoftShadows
		{
			get
			{
				if (this.mainLightShadowmapResolutionFieldInfo == null)
				{
					this.InitializeShadowMapFieldInfo();
				}
				return (bool)this.supportsSoftShadowsFieldInfo.GetValue(this.urpa);
			}
			set
			{
				if (this.mainLightShadowmapResolutionFieldInfo == null)
				{
					this.InitializeShadowMapFieldInfo();
				}
				this.supportsSoftShadowsFieldInfo.SetValue(this.urpa, value);
			}
		}

		// Token: 0x0400D77F RID: 55167
		private UniversalRenderPipelineAsset urpa;

		// Token: 0x0400D780 RID: 55168
		private Type universalRenderPipelineAssetType;

		// Token: 0x0400D781 RID: 55169
		private FieldInfo mainLightShadowmapResolutionFieldInfo;

		// Token: 0x0400D782 RID: 55170
		private FieldInfo supportsSoftShadowsFieldInfo;
	}
}
