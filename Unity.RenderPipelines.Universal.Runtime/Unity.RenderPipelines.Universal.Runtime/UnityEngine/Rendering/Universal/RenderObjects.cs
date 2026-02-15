using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200016C RID: 364
	[ExcludeFromPreset]
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", null, null)]
	[Tooltip("Render Objects simplifies the injection of additional render passes by exposing a selection of commonly used settings.")]
	public class RenderObjects : ScriptableRendererFeature
	{
		// Token: 0x060007DE RID: 2014 RVA: 0x00025A60 File Offset: 0x00023C60
		public override void Create()
		{
			RenderObjects.FilterSettings filter = this.settings.filterSettings;
			if (this.settings.Event < RenderPassEvent.BeforeRenderingPrePasses)
			{
				this.settings.Event = RenderPassEvent.BeforeRenderingPrePasses;
			}
			this.renderObjectsPass = new RenderObjectsPass(this.settings.passTag, this.settings.Event, filter.PassNames, filter.RenderQueueType, filter.LayerMask, this.settings.cameraSettings);
			switch (this.settings.overrideMode)
			{
			case RenderObjects.RenderObjectsSettings.OverrideMaterialMode.None:
				this.renderObjectsPass.overrideMaterial = null;
				this.renderObjectsPass.overrideShader = null;
				break;
			case RenderObjects.RenderObjectsSettings.OverrideMaterialMode.Material:
				this.renderObjectsPass.overrideMaterial = this.settings.overrideMaterial;
				this.renderObjectsPass.overrideMaterialPassIndex = this.settings.overrideMaterialPassIndex;
				this.renderObjectsPass.overrideShader = null;
				break;
			case RenderObjects.RenderObjectsSettings.OverrideMaterialMode.Shader:
				this.renderObjectsPass.overrideMaterial = null;
				this.renderObjectsPass.overrideShader = this.settings.overrideShader;
				this.renderObjectsPass.overrideShaderPassIndex = this.settings.overrideShaderPassIndex;
				break;
			}
			if (this.settings.overrideDepthState)
			{
				this.renderObjectsPass.SetDepthState(this.settings.enableWrite, this.settings.depthCompareFunction);
			}
			if (this.settings.stencilSettings.overrideStencilState)
			{
				this.renderObjectsPass.SetStencilState(this.settings.stencilSettings.stencilReference, this.settings.stencilSettings.stencilCompareFunction, this.settings.stencilSettings.passOperation, this.settings.stencilSettings.failOperation, this.settings.stencilSettings.zFailOperation);
			}
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00025C28 File Offset: 0x00023E28
		public unsafe override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (*renderingData.cameraData.cameraType == CameraType.Preview || UniversalRenderer.IsOffscreenDepthTexture(ref renderingData.cameraData))
			{
				return;
			}
			renderer.EnqueuePass(this.renderObjectsPass);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000039B4 File Offset: 0x00001BB4
		internal override bool SupportsNativeRenderPass()
		{
			return true;
		}

		// Token: 0x04000852 RID: 2130
		public RenderObjects.RenderObjectsSettings settings = new RenderObjects.RenderObjectsSettings();

		// Token: 0x04000853 RID: 2131
		private RenderObjectsPass renderObjectsPass;

		// Token: 0x0200016D RID: 365
		[Serializable]
		public class RenderObjectsSettings
		{
			// Token: 0x04000854 RID: 2132
			public string passTag = "RenderObjectsFeature";

			// Token: 0x04000855 RID: 2133
			public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;

			// Token: 0x04000856 RID: 2134
			public RenderObjects.FilterSettings filterSettings = new RenderObjects.FilterSettings();

			// Token: 0x04000857 RID: 2135
			public Material overrideMaterial;

			// Token: 0x04000858 RID: 2136
			public int overrideMaterialPassIndex;

			// Token: 0x04000859 RID: 2137
			public Shader overrideShader;

			// Token: 0x0400085A RID: 2138
			public int overrideShaderPassIndex;

			// Token: 0x0400085B RID: 2139
			public RenderObjects.RenderObjectsSettings.OverrideMaterialMode overrideMode = RenderObjects.RenderObjectsSettings.OverrideMaterialMode.Material;

			// Token: 0x0400085C RID: 2140
			public bool overrideDepthState;

			// Token: 0x0400085D RID: 2141
			public CompareFunction depthCompareFunction = CompareFunction.LessEqual;

			// Token: 0x0400085E RID: 2142
			public bool enableWrite = true;

			// Token: 0x0400085F RID: 2143
			public StencilStateData stencilSettings = new StencilStateData();

			// Token: 0x04000860 RID: 2144
			public RenderObjects.CustomCameraSettings cameraSettings = new RenderObjects.CustomCameraSettings();

			// Token: 0x0200016E RID: 366
			public enum OverrideMaterialMode
			{
				// Token: 0x04000862 RID: 2146
				None,
				// Token: 0x04000863 RID: 2147
				Material,
				// Token: 0x04000864 RID: 2148
				Shader
			}
		}

		// Token: 0x0200016F RID: 367
		[Serializable]
		public class FilterSettings
		{
			// Token: 0x060007E3 RID: 2019 RVA: 0x00025CC7 File Offset: 0x00023EC7
			public FilterSettings()
			{
				this.RenderQueueType = RenderQueueType.Opaque;
				this.LayerMask = 0;
			}

			// Token: 0x04000865 RID: 2149
			public RenderQueueType RenderQueueType;

			// Token: 0x04000866 RID: 2150
			public LayerMask LayerMask;

			// Token: 0x04000867 RID: 2151
			public string[] PassNames;
		}

		// Token: 0x02000170 RID: 368
		[Serializable]
		public class CustomCameraSettings
		{
			// Token: 0x04000868 RID: 2152
			public bool overrideCamera;

			// Token: 0x04000869 RID: 2153
			public bool restoreCamera = true;

			// Token: 0x0400086A RID: 2154
			public Vector4 offset;

			// Token: 0x0400086B RID: 2155
			public float cameraFieldOfView = 60f;
		}
	}
}
