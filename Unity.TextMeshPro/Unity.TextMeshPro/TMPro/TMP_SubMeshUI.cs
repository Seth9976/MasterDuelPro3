using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x0200007D RID: 125
	[ExecuteAlways]
	[RequireComponent(typeof(CanvasRenderer))]
	public class TMP_SubMeshUI : MaskableGraphic
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00015377 File Offset: 0x00013577
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0001537F File Offset: 0x0001357F
		public TMP_FontAsset fontAsset
		{
			get
			{
				return this.m_fontAsset;
			}
			set
			{
				this.m_fontAsset = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x00015388 File Offset: 0x00013588
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x00015390 File Offset: 0x00013590
		public TMP_SpriteAsset spriteAsset
		{
			get
			{
				return this.m_spriteAsset;
			}
			set
			{
				this.m_spriteAsset = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x00015399 File Offset: 0x00013599
		public override Texture mainTexture
		{
			get
			{
				if (this.sharedMaterial != null)
				{
					return this.sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex);
				}
				return null;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x000153BB File Offset: 0x000135BB
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x000153CC File Offset: 0x000135CC
		public override Material material
		{
			get
			{
				return this.GetMaterial(this.m_sharedMaterial);
			}
			set
			{
				if (this.m_sharedMaterial != null && this.m_sharedMaterial.GetInstanceID() == value.GetInstanceID())
				{
					return;
				}
				this.m_material = value;
				this.m_sharedMaterial = value;
				this.m_padding = this.GetPaddingForMaterial();
				this.SetVerticesDirty();
				this.SetMaterialDirty();
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x00015423 File Offset: 0x00013623
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0001542B File Offset: 0x0001362B
		public Material sharedMaterial
		{
			get
			{
				return this.m_sharedMaterial;
			}
			set
			{
				this.SetSharedMaterial(value);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x00015434 File Offset: 0x00013634
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0001543C File Offset: 0x0001363C
		public Material fallbackMaterial
		{
			get
			{
				return this.m_fallbackMaterial;
			}
			set
			{
				if (this.m_fallbackMaterial == value)
				{
					return;
				}
				if (this.m_fallbackMaterial != null && this.m_fallbackMaterial != value)
				{
					TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				}
				this.m_fallbackMaterial = value;
				TMP_MaterialManager.AddFallbackMaterialReference(this.m_fallbackMaterial);
				this.SetSharedMaterial(this.m_fallbackMaterial);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0001549D File Offset: 0x0001369D
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x000154A5 File Offset: 0x000136A5
		public Material fallbackSourceMaterial
		{
			get
			{
				return this.m_fallbackSourceMaterial;
			}
			set
			{
				this.m_fallbackSourceMaterial = value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x000154AE File Offset: 0x000136AE
		public override Material materialForRendering
		{
			get
			{
				return TMP_MaterialManager.GetMaterialForRendering(this, this.m_sharedMaterial);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x000154BC File Offset: 0x000136BC
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x000154C4 File Offset: 0x000136C4
		public bool isDefaultMaterial
		{
			get
			{
				return this.m_isDefaultMaterial;
			}
			set
			{
				this.m_isDefaultMaterial = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000154CD File Offset: 0x000136CD
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x000154D5 File Offset: 0x000136D5
		public float padding
		{
			get
			{
				return this.m_padding;
			}
			set
			{
				this.m_padding = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000154DE File Offset: 0x000136DE
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0001550C File Offset: 0x0001370C
		public Mesh mesh
		{
			get
			{
				if (this.m_mesh == null)
				{
					this.m_mesh = new Mesh();
					this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
				}
				return this.m_mesh;
			}
			set
			{
				this.m_mesh = value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00015515 File Offset: 0x00013715
		public TMP_Text textComponent
		{
			get
			{
				if (this.m_TextComponent == null)
				{
					this.m_TextComponent = base.GetComponentInParent<TextMeshProUGUI>();
				}
				return this.m_TextComponent;
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00015538 File Offset: 0x00013738
		public static TMP_SubMeshUI AddSubTextObject(TextMeshProUGUI textComponent, MaterialReference materialReference)
		{
			GameObject gameObject = new GameObject();
			gameObject.hideFlags = (TMP_Settings.hideSubTextObjects ? HideFlags.HideAndDontSave : HideFlags.DontSave);
			gameObject.transform.SetParent(textComponent.transform, false);
			gameObject.transform.SetAsFirstSibling();
			gameObject.layer = textComponent.gameObject.layer;
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.anchorMin = Vector2.zero;
			rectTransform.anchorMax = Vector2.one;
			rectTransform.sizeDelta = Vector2.zero;
			rectTransform.pivot = textComponent.rectTransform.pivot;
			gameObject.AddComponent<LayoutElement>().ignoreLayout = true;
			TMP_SubMeshUI tmp_SubMeshUI = gameObject.AddComponent<TMP_SubMeshUI>();
			tmp_SubMeshUI.m_TextComponent = textComponent;
			tmp_SubMeshUI.m_materialReferenceIndex = materialReference.index;
			tmp_SubMeshUI.m_fontAsset = materialReference.fontAsset;
			tmp_SubMeshUI.m_spriteAsset = materialReference.spriteAsset;
			tmp_SubMeshUI.m_isDefaultMaterial = materialReference.isDefaultMaterial;
			tmp_SubMeshUI.maskable = textComponent.maskable;
			tmp_SubMeshUI.SetSharedMaterial(materialReference.material);
			return tmp_SubMeshUI;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00015622 File Offset: 0x00013822
		protected override void OnEnable()
		{
			if (!this.m_isRegisteredForEvents)
			{
				this.m_isRegisteredForEvents = true;
			}
			if (base.hideFlags != HideFlags.DontSave)
			{
				base.hideFlags = HideFlags.DontSave;
			}
			this.m_ShouldRecalculateStencil = true;
			this.RecalculateClipping();
			this.RecalculateMasking();
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00015658 File Offset: 0x00013858
		protected override void OnDisable()
		{
			base.OnDisable();
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00015680 File Offset: 0x00013880
		protected override void OnDestroy()
		{
			if (this.m_mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_mesh);
			}
			if (this.m_MaskMaterial != null)
			{
				TMP_MaterialManager.ReleaseStencilMaterial(this.m_MaskMaterial);
			}
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
			this.m_isRegisteredForEvents = false;
			this.RecalculateClipping();
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.havePropertiesChanged = true;
				this.m_TextComponent.SetAllDirty();
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00015711 File Offset: 0x00013911
		protected override void OnTransformParentChanged()
		{
			if (!this.IsActive())
			{
				return;
			}
			this.m_ShouldRecalculateStencil = true;
			this.RecalculateClipping();
			this.RecalculateMasking();
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00015730 File Offset: 0x00013930
		public override Material GetModifiedMaterial(Material baseMaterial)
		{
			Material mat = baseMaterial;
			if (this.m_ShouldRecalculateStencil)
			{
				Transform rootCanvas = MaskUtilities.FindRootSortOverrideCanvas(base.transform);
				this.m_StencilValue = (base.maskable ? MaskUtilities.GetStencilDepth(base.transform, rootCanvas) : 0);
				this.m_ShouldRecalculateStencil = false;
			}
			if (this.m_StencilValue > 0)
			{
				Material maskMat = StencilMaterial.Add(mat, (1 << this.m_StencilValue) - 1, StencilOp.Keep, CompareFunction.Equal, ColorWriteMask.All, (1 << this.m_StencilValue) - 1, 0);
				StencilMaterial.Remove(this.m_MaskMaterial);
				this.m_MaskMaterial = maskMat;
				mat = this.m_MaskMaterial;
			}
			return mat;
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000157C0 File Offset: 0x000139C0
		public float GetPaddingForMaterial()
		{
			return ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_TextComponent.extraPadding, this.m_TextComponent.isUsingBold);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000157E3 File Offset: 0x000139E3
		public float GetPaddingForMaterial(Material mat)
		{
			return ShaderUtilities.GetPadding(mat, this.m_TextComponent.extraPadding, this.m_TextComponent.isUsingBold);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00015801 File Offset: 0x00013A01
		public void UpdateMeshPadding(bool isExtraPadding, bool isUsingBold)
		{
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, isExtraPadding, isUsingBold);
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00002AAB File Offset: 0x00000CAB
		public override void SetAllDirty()
		{
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00002AAB File Offset: 0x00000CAB
		public override void SetVerticesDirty()
		{
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00002AAB File Offset: 0x00000CAB
		public override void SetLayoutDirty()
		{
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00015816 File Offset: 0x00013A16
		public override void SetMaterialDirty()
		{
			this.m_materialDirty = true;
			this.UpdateMaterial();
			if (this.m_OnDirtyMaterialCallback != null)
			{
				this.m_OnDirtyMaterialCallback();
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00015838 File Offset: 0x00013A38
		public void SetPivotDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			base.rectTransform.pivot = this.m_TextComponent.rectTransform.pivot;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001585E File Offset: 0x00013A5E
		private Transform GetRootCanvasTransform()
		{
			if (this.m_RootCanvasTransform == null)
			{
				this.m_RootCanvasTransform = this.m_TextComponent.canvas.rootCanvas.transform;
			}
			return this.m_RootCanvasTransform;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x00002AAB File Offset: 0x00000CAB
		public override void Cull(Rect clipRect, bool validRect)
		{
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00002AAB File Offset: 0x00000CAB
		protected override void UpdateGeometry()
		{
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001588F File Offset: 0x00013A8F
		public override void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.PreRender)
			{
				if (!this.m_materialDirty)
				{
					return;
				}
				this.UpdateMaterial();
				this.m_materialDirty = false;
			}
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000158AB File Offset: 0x00013AAB
		public void RefreshMaterial()
		{
			this.UpdateMaterial();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000158B4 File Offset: 0x00013AB4
		protected override void UpdateMaterial()
		{
			if (this.m_sharedMaterial == null)
			{
				return;
			}
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ShaderTag_CullMode) && this.textComponent.fontSharedMaterial != null)
			{
				float cullMode = this.textComponent.fontSharedMaterial.GetFloat(ShaderUtilities.ShaderTag_CullMode);
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_CullMode, cullMode);
			}
			base.canvasRenderer.materialCount = 1;
			base.canvasRenderer.SetMaterial(this.materialForRendering, 0);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001593A File Offset: 0x00013B3A
		public override void RecalculateClipping()
		{
			base.RecalculateClipping();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00015423 File Offset: 0x00013623
		private Material GetMaterial()
		{
			return this.m_sharedMaterial;
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00015944 File Offset: 0x00013B44
		private Material GetMaterial(Material mat)
		{
			if (this.m_material == null || this.m_material.GetInstanceID() != mat.GetInstanceID())
			{
				this.m_material = this.CreateMaterialInstance(mat);
			}
			this.m_sharedMaterial = this.m_material;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
			return this.m_sharedMaterial;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00015243 File Offset: 0x00013443
		private Material CreateMaterialInstance(Material source)
		{
			Material material = new Material(source);
			material.shaderKeywords = source.shaderKeywords;
			material.name += " (Instance)";
			return material;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000159A9 File Offset: 0x00013BA9
		private Material GetSharedMaterial()
		{
			return base.canvasRenderer.GetMaterial();
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000159B6 File Offset: 0x00013BB6
		private void SetSharedMaterial(Material mat)
		{
			this.m_sharedMaterial = mat;
			this.m_Material = this.m_sharedMaterial;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetMaterialDirty();
		}

		// Token: 0x040003BC RID: 956
		[SerializeField]
		private TMP_FontAsset m_fontAsset;

		// Token: 0x040003BD RID: 957
		[SerializeField]
		private TMP_SpriteAsset m_spriteAsset;

		// Token: 0x040003BE RID: 958
		[SerializeField]
		private Material m_material;

		// Token: 0x040003BF RID: 959
		[SerializeField]
		private Material m_sharedMaterial;

		// Token: 0x040003C0 RID: 960
		private Material m_fallbackMaterial;

		// Token: 0x040003C1 RID: 961
		private Material m_fallbackSourceMaterial;

		// Token: 0x040003C2 RID: 962
		[SerializeField]
		private bool m_isDefaultMaterial;

		// Token: 0x040003C3 RID: 963
		[SerializeField]
		private float m_padding;

		// Token: 0x040003C4 RID: 964
		private Mesh m_mesh;

		// Token: 0x040003C5 RID: 965
		[SerializeField]
		private TextMeshProUGUI m_TextComponent;

		// Token: 0x040003C6 RID: 966
		[NonSerialized]
		private bool m_isRegisteredForEvents;

		// Token: 0x040003C7 RID: 967
		private bool m_materialDirty;

		// Token: 0x040003C8 RID: 968
		[SerializeField]
		private int m_materialReferenceIndex;

		// Token: 0x040003C9 RID: 969
		private Transform m_RootCanvasTransform;
	}
}
