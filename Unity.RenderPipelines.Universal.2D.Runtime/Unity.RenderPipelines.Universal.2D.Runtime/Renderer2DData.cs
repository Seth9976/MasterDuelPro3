using System;
using System.Collections.Generic;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000051 RID: 81
	[ReloadGroup]
	[ExcludeFromPreset]
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", "Unity.RenderPipelines.Universal.Runtime", null)]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest/index.html?subfolder=/manual/2DRendererData-overview.html")]
	[Serializable]
	public class Renderer2DData : ScriptableRendererData
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00012F15 File Offset: 0x00011115
		public float hdrEmulationScale
		{
			get
			{
				return this.m_HDREmulationScale;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00012F1D File Offset: 0x0001111D
		internal float lightRenderTextureScale
		{
			get
			{
				return this.m_LightRenderTextureScale;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00012F25 File Offset: 0x00011125
		public Light2DBlendStyle[] lightBlendStyles
		{
			get
			{
				return this.m_LightBlendStyles;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00012F2D File Offset: 0x0001112D
		internal bool useDepthStencilBuffer
		{
			get
			{
				return this.m_UseDepthStencilBuffer;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00012F35 File Offset: 0x00011135
		// (set) Token: 0x0600023E RID: 574 RVA: 0x00012F3D File Offset: 0x0001113D
		internal PostProcessData postProcessData
		{
			get
			{
				return this.m_PostProcessData;
			}
			set
			{
				this.m_PostProcessData = value;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600023F RID: 575 RVA: 0x00012F46 File Offset: 0x00011146
		internal TransparencySortMode transparencySortMode
		{
			get
			{
				return this.m_TransparencySortMode;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00012F4E File Offset: 0x0001114E
		internal Vector3 transparencySortAxis
		{
			get
			{
				return this.m_TransparencySortAxis;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000241 RID: 577 RVA: 0x00012F56 File Offset: 0x00011156
		internal uint lightRenderTextureMemoryBudget
		{
			get
			{
				return this.m_MaxLightRenderTextureCount;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00012F5E File Offset: 0x0001115E
		internal uint shadowRenderTextureMemoryBudget
		{
			get
			{
				return this.m_MaxShadowRenderTextureCount;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00012F66 File Offset: 0x00011166
		internal bool useCameraSortingLayerTexture
		{
			get
			{
				return this.m_UseCameraSortingLayersTexture;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00012F6E File Offset: 0x0001116E
		internal int cameraSortingLayerTextureBound
		{
			get
			{
				return this.m_CameraSortingLayersTextureBound;
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000245 RID: 581 RVA: 0x00012F76 File Offset: 0x00011176
		internal Downsampling cameraSortingLayerDownsamplingMethod
		{
			get
			{
				return this.m_CameraSortingLayerDownsamplingMethod;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00012F7E File Offset: 0x0001117E
		protected override ScriptableRenderer Create()
		{
			return new Renderer2D(this);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00012F88 File Offset: 0x00011188
		internal void Dispose()
		{
			for (int i = 0; i < this.m_LightBlendStyles.Length; i++)
			{
				RTHandle renderTargetHandle = this.m_LightBlendStyles[i].renderTargetHandle;
				if (renderTargetHandle != null)
				{
					renderTargetHandle.Release();
				}
			}
			foreach (KeyValuePair<uint, Material> mat in this.lightMaterials)
			{
				CoreUtils.Destroy(mat.Value);
			}
			this.lightMaterials.Clear();
			CoreUtils.Destroy(this.spriteSelfShadowMaterial);
			CoreUtils.Destroy(this.spriteUnshadowMaterial);
			CoreUtils.Destroy(this.geometrySelfShadowMaterial);
			CoreUtils.Destroy(this.geometryUnshadowMaterial);
			CoreUtils.Destroy(this.projectedShadowMaterial);
			CoreUtils.Destroy(this.projectedUnshadowMaterial);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0001305C File Offset: 0x0001125C
		protected override void OnEnable()
		{
			base.OnEnable();
			for (int i = 0; i < this.m_LightBlendStyles.Length; i++)
			{
				this.m_LightBlendStyles[i].renderTargetHandleId = Shader.PropertyToID(string.Format("_ShapeLightTexture{0}", i));
				this.m_LightBlendStyles[i].renderTargetHandle = RTHandles.Alloc(this.m_LightBlendStyles[i].renderTargetHandleId, string.Format("_ShapeLightTexture{0}", i));
			}
			this.geometrySelfShadowMaterial = null;
			this.geometryUnshadowMaterial = null;
			this.spriteSelfShadowMaterial = null;
			this.spriteUnshadowMaterial = null;
			this.projectedShadowMaterial = null;
			this.projectedUnshadowMaterial = null;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0001310E File Offset: 0x0001130E
		internal Dictionary<uint, Material> lightMaterials { get; } = new Dictionary<uint, Material>();

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00013116 File Offset: 0x00011316
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0001311E File Offset: 0x0001131E
		internal Material spriteSelfShadowMaterial { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600024C RID: 588 RVA: 0x00013127 File Offset: 0x00011327
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0001312F File Offset: 0x0001132F
		internal Material spriteUnshadowMaterial { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00013138 File Offset: 0x00011338
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00013140 File Offset: 0x00011340
		internal Material geometrySelfShadowMaterial { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00013149 File Offset: 0x00011349
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00013151 File Offset: 0x00011351
		internal Material geometryUnshadowMaterial { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0001315A File Offset: 0x0001135A
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00013162 File Offset: 0x00011362
		internal Material projectedShadowMaterial { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0001316B File Offset: 0x0001136B
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00013173 File Offset: 0x00011373
		internal Material projectedUnshadowMaterial { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0001317C File Offset: 0x0001137C
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00013184 File Offset: 0x00011384
		internal ILight2DCullResult lightCullResult { get; set; }

		// Token: 0x040001E1 RID: 481
		[SerializeField]
		private TransparencySortMode m_TransparencySortMode;

		// Token: 0x040001E2 RID: 482
		[SerializeField]
		private Vector3 m_TransparencySortAxis = Vector3.up;

		// Token: 0x040001E3 RID: 483
		[SerializeField]
		private float m_HDREmulationScale = 1f;

		// Token: 0x040001E4 RID: 484
		[SerializeField]
		[Range(0.01f, 1f)]
		private float m_LightRenderTextureScale = 0.5f;

		// Token: 0x040001E5 RID: 485
		[SerializeField]
		[FormerlySerializedAs("m_LightOperations")]
		private Light2DBlendStyle[] m_LightBlendStyles;

		// Token: 0x040001E6 RID: 486
		[SerializeField]
		private bool m_UseDepthStencilBuffer = true;

		// Token: 0x040001E7 RID: 487
		[SerializeField]
		private bool m_UseCameraSortingLayersTexture;

		// Token: 0x040001E8 RID: 488
		[SerializeField]
		private int m_CameraSortingLayersTextureBound;

		// Token: 0x040001E9 RID: 489
		[SerializeField]
		private Downsampling m_CameraSortingLayerDownsamplingMethod;

		// Token: 0x040001EA RID: 490
		[SerializeField]
		private uint m_MaxLightRenderTextureCount = 16U;

		// Token: 0x040001EB RID: 491
		[SerializeField]
		private uint m_MaxShadowRenderTextureCount = 1U;

		// Token: 0x040001EC RID: 492
		[SerializeField]
		private PostProcessData m_PostProcessData;

		// Token: 0x040001F4 RID: 500
		internal RTHandle normalsRenderTarget;

		// Token: 0x040001F5 RID: 501
		internal RTHandle cameraSortingLayerRenderTarget;

		// Token: 0x02000052 RID: 82
		internal enum Renderer2DDefaultMaterialType
		{
			// Token: 0x040001F8 RID: 504
			Lit,
			// Token: 0x040001F9 RID: 505
			Unlit,
			// Token: 0x040001FA RID: 506
			Custom
		}
	}
}
