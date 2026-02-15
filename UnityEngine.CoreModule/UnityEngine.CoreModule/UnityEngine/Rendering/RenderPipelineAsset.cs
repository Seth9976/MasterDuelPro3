using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003BD RID: 957
	public abstract class RenderPipelineAsset : ScriptableObject
	{
		// Token: 0x060019CB RID: 6603 RVA: 0x00038690 File Offset: 0x00036890
		internal RenderPipeline InternalCreatePipeline()
		{
			RenderPipeline pipeline = null;
			try
			{
				pipeline = this.CreatePipeline();
			}
			catch (InvalidImportException)
			{
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
			return pipeline;
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Material defaultMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Shader autodeskInteractiveShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060019CE RID: 6606 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Shader autodeskInteractiveTransparentShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Shader autodeskInteractiveMaskedShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060019D0 RID: 6608 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Shader terrainDetailLitShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Shader terrainDetailGrassShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060019D2 RID: 6610 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Shader terrainDetailGrassBillboardShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Material defaultParticleMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060019D4 RID: 6612 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Material defaultLineMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Material defaultTerrainMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Material defaultUIMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Material defaultUIOverdrawMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Material defaultUIETC1SupportedMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Material default2DMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Material default2DMaskMaterial
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Shader defaultShader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Shader defaultSpeedTree7Shader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Shader defaultSpeedTree8Shader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x0002144D File Offset: 0x0001F64D
		public virtual Shader defaultSpeedTree9Shader
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x000386E0 File Offset: 0x000368E0
		public virtual string renderPipelineShaderTag
		{
			get
			{
				Debug.LogWarning("The property renderPipelineShaderTag has not been overridden. At build time, any shader variants that use any RenderPipeline tag will be stripped.");
				return string.Empty;
			}
		}

		// Token: 0x060019E0 RID: 6624
		protected abstract RenderPipeline CreatePipeline();

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x00038704 File Offset: 0x00036904
		public virtual Type pipelineType
		{
			get
			{
				Debug.LogWarning("You must either inherit from RenderPipelineAsset<TRenderPipeline> or override pipelineType property.");
				return null;
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x00038722 File Offset: 0x00036922
		internal string pipelineTypeFullName
		{
			get
			{
				Type pipelineType = this.pipelineType;
				return ((pipelineType != null) ? pipelineType.FullName : null) ?? string.Empty;
			}
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00003D56 File Offset: 0x00001F56
		protected virtual void EnsureGlobalSettings()
		{
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x0003873F File Offset: 0x0003693F
		protected virtual void OnValidate()
		{
			RenderPipelineManager.RecreateCurrentPipeline(this);
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00038749 File Offset: 0x00036949
		protected virtual void OnDisable()
		{
			RenderPipelineManager.CleanupRenderPipeline();
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x00038754 File Offset: 0x00036954
		[Obsolete("This property is obsolete. Use pipelineType instead. #from(23.2)", false)]
		protected internal virtual Type renderPipelineType
		{
			get
			{
				Debug.LogWarning("You must either inherit from RenderPipelineAsset<TRenderPipeline> or override renderPipelineType property");
				return null;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x0002144D File Offset: 0x0001F64D
		[Obsolete("This property is obsolete. Use RenderingLayerMask API and Tags & Layers project settings instead. #from(23.3)", false)]
		public virtual string[] renderingLayerMaskNames
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x0002144D File Offset: 0x0001F64D
		[Obsolete("This property is obsolete. Use RenderingLayerMask API and Tags & Layers project settings instead. #from(23.3)", false)]
		public virtual string[] prefixedRenderingLayerMaskNames
		{
			get
			{
				return null;
			}
		}
	}
}
