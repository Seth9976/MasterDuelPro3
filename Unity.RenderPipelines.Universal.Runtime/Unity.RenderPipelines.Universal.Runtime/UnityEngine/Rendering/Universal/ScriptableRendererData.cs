using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x020000B2 RID: 178
	public abstract class ScriptableRendererData : ScriptableObject
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00011F8F File Offset: 0x0001018F
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00011F97 File Offset: 0x00010197
		internal bool isInvalidated { get; set; }

		// Token: 0x0600045F RID: 1119
		protected abstract ScriptableRenderer Create();

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00011FA0 File Offset: 0x000101A0
		public List<ScriptableRendererFeature> rendererFeatures
		{
			get
			{
				return this.m_RendererFeatures;
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00011FA8 File Offset: 0x000101A8
		public void SetDirty()
		{
			this.isInvalidated = true;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00011FB1 File Offset: 0x000101B1
		internal ScriptableRenderer InternalCreateRenderer()
		{
			this.isInvalidated = false;
			return this.Create();
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00011FC0 File Offset: 0x000101C0
		protected virtual void OnValidate()
		{
			this.SetDirty();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00011FC0 File Offset: 0x000101C0
		protected virtual void OnEnable()
		{
			this.SetDirty();
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00011FC8 File Offset: 0x000101C8
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00011FD0 File Offset: 0x000101D0
		public bool useNativeRenderPass
		{
			get
			{
				return this.m_UseNativeRenderPass;
			}
			set
			{
				this.SetDirty();
				this.m_UseNativeRenderPass = value;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00011FE0 File Offset: 0x000101E0
		public bool TryGetRendererFeature<T>(out T rendererFeature) where T : ScriptableRendererFeature
		{
			foreach (ScriptableRendererFeature target in this.rendererFeatures)
			{
				if (target.GetType() == typeof(T))
				{
					rendererFeature = target as T;
					return true;
				}
			}
			rendererFeature = default(T);
			return false;
		}

		// Token: 0x04000388 RID: 904
		[Obsolete("Moved to UniversalRenderPipelineDebugShaders on GraphicsSettings. #from(2023.3)", false)]
		public ScriptableRendererData.DebugShaderResources debugShaders;

		// Token: 0x04000389 RID: 905
		[Obsolete("Probe volume debug resource are now in the ProbeVolumeDebugResources class.")]
		public ScriptableRendererData.ProbeVolumeResources probeVolumeResources;

		// Token: 0x0400038B RID: 907
		[SerializeField]
		internal List<ScriptableRendererFeature> m_RendererFeatures = new List<ScriptableRendererFeature>(10);

		// Token: 0x0400038C RID: 908
		[SerializeField]
		internal List<long> m_RendererFeatureMap = new List<long>(10);

		// Token: 0x0400038D RID: 909
		[SerializeField]
		private bool m_UseNativeRenderPass;

		// Token: 0x020000B3 RID: 179
		[Obsolete("Moved to UniversalRenderPipelineDebugShaders on GraphicsSettings. #from(2023.3)", false)]
		[ReloadGroup]
		[Serializable]
		public sealed class DebugShaderResources
		{
			// Token: 0x0400038E RID: 910
			[Obsolete("Moved to UniversalRenderPipelineDebugShaders on GraphicsSettings. #from(2023.3)", false)]
			[Reload("Shaders/Debug/DebugReplacement.shader", ReloadAttribute.Package.Root)]
			public Shader debugReplacementPS;

			// Token: 0x0400038F RID: 911
			[Obsolete("Moved to UniversalRenderPipelineDebugShaders on GraphicsSettings. #from(2023.3)", false)]
			[Reload("Shaders/Debug/HDRDebugView.shader", ReloadAttribute.Package.Root)]
			public Shader hdrDebugViewPS;
		}

		// Token: 0x020000B4 RID: 180
		[ReloadGroup]
		[Obsolete("Probe volume debug resource are now in the ProbeVolumeDebugResources class.")]
		[Serializable]
		public sealed class ProbeVolumeResources
		{
			// Token: 0x04000390 RID: 912
			[Obsolete("This shader is now in the ProbeVolumeDebugResources class.")]
			public Shader probeVolumeDebugShader;

			// Token: 0x04000391 RID: 913
			[Obsolete("This shader is now in the ProbeVolumeDebugResources class.")]
			public Shader probeVolumeFragmentationDebugShader;

			// Token: 0x04000392 RID: 914
			[Obsolete("This shader is now in the ProbeVolumeDebugResources class.")]
			public Shader probeVolumeOffsetDebugShader;

			// Token: 0x04000393 RID: 915
			[Obsolete("This shader is now in the ProbeVolumeDebugResources class.")]
			public Shader probeVolumeSamplingDebugShader;

			// Token: 0x04000394 RID: 916
			[Obsolete("This shader is now in the ProbeVolumeDebugResources class.")]
			public Mesh probeSamplingDebugMesh;

			// Token: 0x04000395 RID: 917
			[Obsolete("This shader is now in the ProbeVolumeDebugResources class.")]
			public Texture2D probeSamplingDebugTexture;

			// Token: 0x04000396 RID: 918
			[Obsolete("This shader is now in the ProbeVolumeRuntimeResources class.")]
			public ComputeShader probeVolumeBlendStatesCS;
		}
	}
}
