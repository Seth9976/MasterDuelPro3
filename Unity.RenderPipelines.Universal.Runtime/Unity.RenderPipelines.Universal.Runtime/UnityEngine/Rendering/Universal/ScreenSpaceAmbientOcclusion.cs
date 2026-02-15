using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000177 RID: 375
	[SupportedOnRenderer(typeof(UniversalRendererData))]
	[DisallowMultipleRendererFeature("Screen Space Ambient Occlusion")]
	[Tooltip("The Ambient Occlusion effect darkens creases, holes, intersections and surfaces that are close to each other.")]
	public class ScreenSpaceAmbientOcclusion : ScriptableRendererFeature
	{
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00025D57 File Offset: 0x00023F57
		internal ref ScreenSpaceAmbientOcclusionSettings settings
		{
			get
			{
				return ref this.m_Settings;
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00025D60 File Offset: 0x00023F60
		public override void Create()
		{
			if (this.m_SSAOPass == null)
			{
				this.m_SSAOPass = new ScreenSpaceAmbientOcclusionPass();
			}
			if (this.m_Settings.SampleCount > 0)
			{
				this.m_Settings.AOMethod = ScreenSpaceAmbientOcclusionSettings.AOMethodOptions.InterleavedGradient;
				if (this.m_Settings.SampleCount > 11)
				{
					this.m_Settings.Samples = ScreenSpaceAmbientOcclusionSettings.AOSampleOption.High;
				}
				else if (this.m_Settings.SampleCount > 8)
				{
					this.m_Settings.Samples = ScreenSpaceAmbientOcclusionSettings.AOSampleOption.Medium;
				}
				else
				{
					this.m_Settings.Samples = ScreenSpaceAmbientOcclusionSettings.AOSampleOption.Low;
				}
				this.m_Settings.SampleCount = -1;
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00025DEC File Offset: 0x00023FEC
		public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
		{
			if (UniversalRenderer.IsOffscreenDepthTexture(ref renderingData.cameraData))
			{
				return;
			}
			if (!this.GetMaterials())
			{
				Debug.LogErrorFormat("{0}.AddRenderPasses(): Missing material. {1} render pass will not be added.", new object[]
				{
					base.GetType().Name,
					base.name
				});
				return;
			}
			if (this.m_SSAOPass.Setup(ref this.m_Settings, ref renderer, ref this.m_Material, ref this.m_BlueNoise256Textures))
			{
				renderer.EnqueuePass(this.m_SSAOPass);
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00025E64 File Offset: 0x00024064
		protected override void Dispose(bool disposing)
		{
			ScreenSpaceAmbientOcclusionPass ssaopass = this.m_SSAOPass;
			if (ssaopass != null)
			{
				ssaopass.Dispose();
			}
			this.m_SSAOPass = null;
			CoreUtils.Destroy(this.m_Material);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00025E89 File Offset: 0x00024089
		private bool GetMaterials()
		{
			if (this.m_Material == null && this.m_Shader != null)
			{
				this.m_Material = CoreUtils.CreateEngineMaterial(this.m_Shader);
			}
			return this.m_Material != null;
		}

		// Token: 0x0400088A RID: 2186
		[SerializeField]
		private ScreenSpaceAmbientOcclusionSettings m_Settings = new ScreenSpaceAmbientOcclusionSettings();

		// Token: 0x0400088B RID: 2187
		[SerializeField]
		[HideInInspector]
		[Reload("Textures/BlueNoise256/LDR_LLL1_{0}.png", 0, 7, ReloadAttribute.Package.Root)]
		internal Texture2D[] m_BlueNoise256Textures;

		// Token: 0x0400088C RID: 2188
		[SerializeField]
		[HideInInspector]
		[Reload("Shaders/Utils/ScreenSpaceAmbientOcclusion.shader", ReloadAttribute.Package.Root)]
		private Shader m_Shader;

		// Token: 0x0400088D RID: 2189
		private Material m_Material;

		// Token: 0x0400088E RID: 2190
		private ScreenSpaceAmbientOcclusionPass m_SSAOPass;

		// Token: 0x0400088F RID: 2191
		internal const string k_AOInterleavedGradientKeyword = "_INTERLEAVED_GRADIENT";

		// Token: 0x04000890 RID: 2192
		internal const string k_AOBlueNoiseKeyword = "_BLUE_NOISE";

		// Token: 0x04000891 RID: 2193
		internal const string k_OrthographicCameraKeyword = "_ORTHOGRAPHIC";

		// Token: 0x04000892 RID: 2194
		internal const string k_SourceDepthLowKeyword = "_SOURCE_DEPTH_LOW";

		// Token: 0x04000893 RID: 2195
		internal const string k_SourceDepthMediumKeyword = "_SOURCE_DEPTH_MEDIUM";

		// Token: 0x04000894 RID: 2196
		internal const string k_SourceDepthHighKeyword = "_SOURCE_DEPTH_HIGH";

		// Token: 0x04000895 RID: 2197
		internal const string k_SourceDepthNormalsKeyword = "_SOURCE_DEPTH_NORMALS";

		// Token: 0x04000896 RID: 2198
		internal const string k_SampleCountLowKeyword = "_SAMPLE_COUNT_LOW";

		// Token: 0x04000897 RID: 2199
		internal const string k_SampleCountMediumKeyword = "_SAMPLE_COUNT_MEDIUM";

		// Token: 0x04000898 RID: 2200
		internal const string k_SampleCountHighKeyword = "_SAMPLE_COUNT_HIGH";
	}
}
