using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

namespace TMPro
{
	// Token: 0x020000B7 RID: 183
	[DisallowMultipleComponent]
	[RequireComponent(typeof(MeshRenderer))]
	[AddComponentMenu("Mesh/TextMeshPro - Text")]
	[ExecuteAlways]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/TextMeshPro/index.html")]
	public class TextMeshPro : TMP_Text, ILayoutElement
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0002F541 File Offset: 0x0002D741
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x0002F55E File Offset: 0x0002D75E
		public int sortingLayerID
		{
			get
			{
				if (this.renderer == null)
				{
					return 0;
				}
				return this.m_renderer.sortingLayerID;
			}
			set
			{
				if (this.renderer == null)
				{
					return;
				}
				this.m_renderer.sortingLayerID = value;
				this._SortingLayerID = value;
				this.UpdateSubMeshSortingLayerID(value);
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0002F589 File Offset: 0x0002D789
		// (set) Token: 0x06000673 RID: 1651 RVA: 0x0002F5A6 File Offset: 0x0002D7A6
		public int sortingOrder
		{
			get
			{
				if (this.renderer == null)
				{
					return 0;
				}
				return this.m_renderer.sortingOrder;
			}
			set
			{
				if (this.renderer == null)
				{
					return;
				}
				this.m_renderer.sortingOrder = value;
				this._SortingOrder = value;
				this.UpdateSubMeshSortingOrder(value);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0002F5D1 File Offset: 0x0002D7D1
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0002F5D9 File Offset: 0x0002D7D9
		public override bool autoSizeTextContainer
		{
			get
			{
				return this.m_autoSizeTextContainer;
			}
			set
			{
				if (this.m_autoSizeTextContainer == value)
				{
					return;
				}
				this.m_autoSizeTextContainer = value;
				if (this.m_autoSizeTextContainer)
				{
					TMP_UpdateManager.RegisterTextElementForLayoutRebuild(this);
					this.SetLayoutDirty();
				}
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00016B3A File Offset: 0x00014D3A
		[Obsolete("The TextContainer is now obsolete. Use the RectTransform instead.")]
		public TextContainer textContainer
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x000167D0 File Offset: 0x000149D0
		public new Transform transform
		{
			get
			{
				if (this.m_transform == null)
				{
					this.m_transform = base.GetComponent<Transform>();
				}
				return this.m_transform;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x0002F600 File Offset: 0x0002D800
		public Renderer renderer
		{
			get
			{
				if (this.m_renderer == null)
				{
					this.m_renderer = base.GetComponent<Renderer>();
				}
				return this.m_renderer;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0002F622 File Offset: 0x0002D822
		public override Mesh mesh
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
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0002F650 File Offset: 0x0002D850
		public MeshFilter meshFilter
		{
			get
			{
				if (this.m_meshFilter == null)
				{
					this.m_meshFilter = base.GetComponent<MeshFilter>();
					if (this.m_meshFilter == null)
					{
						this.m_meshFilter = base.gameObject.AddComponent<MeshFilter>();
						this.m_meshFilter.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset;
					}
				}
				return this.m_meshFilter;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0002F6A9 File Offset: 0x0002D8A9
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0002F6B1 File Offset: 0x0002D8B1
		public MaskingTypes maskType
		{
			get
			{
				return this.m_maskType;
			}
			set
			{
				this.m_maskType = value;
				this.SetMask(this.m_maskType);
			}
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0002F6C6 File Offset: 0x0002D8C6
		public void SetMask(MaskingTypes type, Vector4 maskCoords)
		{
			this.SetMask(type);
			this.SetMaskCoordinates(maskCoords);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0002F6D6 File Offset: 0x0002D8D6
		public void SetMask(MaskingTypes type, Vector4 maskCoords, float softnessX, float softnessY)
		{
			this.SetMask(type);
			this.SetMaskCoordinates(maskCoords, softnessX, softnessY);
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0002F6E9 File Offset: 0x0002D8E9
		public override void SetVerticesDirty()
		{
			if (this == null || !this.IsActive())
			{
				return;
			}
			TMP_UpdateManager.RegisterTextElementForGraphicRebuild(this);
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0002F703 File Offset: 0x0002D903
		public override void SetLayoutDirty()
		{
			this.m_isPreferredWidthDirty = true;
			this.m_isPreferredHeightDirty = true;
			if (this == null || !this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(base.rectTransform);
			this.m_isLayoutDirty = true;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000158AB File Offset: 0x00013AAB
		public override void SetMaterialDirty()
		{
			this.UpdateMaterial();
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0002F737 File Offset: 0x0002D937
		public override void SetAllDirty()
		{
			this.SetLayoutDirty();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0002F74C File Offset: 0x0002D94C
		public override void Rebuild(CanvasUpdate update)
		{
			if (this == null)
			{
				return;
			}
			if (update == CanvasUpdate.Prelayout)
			{
				if (this.m_autoSizeTextContainer)
				{
					this.m_rectTransform.sizeDelta = base.GetPreferredValues(float.PositiveInfinity, float.PositiveInfinity);
					return;
				}
			}
			else if (update == CanvasUpdate.PreRender)
			{
				this.OnPreRenderObject();
				if (!this.m_isMaterialDirty)
				{
					return;
				}
				this.UpdateMaterial();
				this.m_isMaterialDirty = false;
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0002F7AC File Offset: 0x0002D9AC
		protected override void UpdateMaterial()
		{
			if (this.renderer == null || this.m_sharedMaterial == null)
			{
				return;
			}
			if (this.m_renderer.sharedMaterial == null || this.m_renderer.sharedMaterial.GetInstanceID() != this.m_sharedMaterial.GetInstanceID())
			{
				this.m_renderer.sharedMaterial = this.m_sharedMaterial;
			}
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x0002F818 File Offset: 0x0002DA18
		public override void UpdateMeshPadding()
		{
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_enableExtraPadding, this.m_isUsingBold);
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			this.m_havePropertiesChanged = true;
			this.checkPaddingRequired = false;
			if (this.m_textInfo == null)
			{
				return;
			}
			for (int i = 1; i < this.m_textInfo.materialCount; i++)
			{
				this.m_subTextObjects[i].UpdateMeshPadding(this.m_enableExtraPadding, this.m_isUsingBold);
			}
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0002F899 File Offset: 0x0002DA99
		public override void ForceMeshUpdate(bool ignoreActiveState = false, bool forceTextReparsing = false)
		{
			this.m_havePropertiesChanged = true;
			this.m_ignoreActiveState = ignoreActiveState;
			this.OnPreRenderObject();
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0002F8AF File Offset: 0x0002DAAF
		public override TMP_TextInfo GetTextInfo(string text)
		{
			base.SetText(text);
			this.SetArraySizes(this.m_TextProcessingArray);
			this.m_renderMode = TextRenderFlags.DontRender;
			this.ComputeMarginSize();
			this.GenerateTextMesh();
			this.m_renderMode = TextRenderFlags.Render;
			return base.textInfo;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0002F8EC File Offset: 0x0002DAEC
		public override void ClearMesh(bool updateMesh)
		{
			if (this.m_textInfo.meshInfo[0].mesh == null)
			{
				this.m_textInfo.meshInfo[0].mesh = this.m_mesh;
			}
			this.m_textInfo.ClearMeshInfo(updateMesh);
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000689 RID: 1673 RVA: 0x0002F940 File Offset: 0x0002DB40
		// (remove) Token: 0x0600068A RID: 1674 RVA: 0x0002F978 File Offset: 0x0002DB78
		public override event Action<TMP_TextInfo> OnPreRenderText;

		// Token: 0x0600068B RID: 1675 RVA: 0x0002F9AD File Offset: 0x0002DBAD
		public override void UpdateGeometry(Mesh mesh, int index)
		{
			mesh.RecalculateBounds();
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0002F9B8 File Offset: 0x0002DBB8
		public override void UpdateVertexData(TMP_VertexDataUpdateFlags flags)
		{
			int materialCount = this.m_textInfo.materialCount;
			for (int i = 0; i < materialCount; i++)
			{
				Mesh mesh;
				if (i == 0)
				{
					mesh = this.m_mesh;
				}
				else
				{
					mesh = this.m_subTextObjects[i].mesh;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Vertices) == TMP_VertexDataUpdateFlags.Vertices)
				{
					mesh.vertices = this.m_textInfo.meshInfo[i].vertices;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Uv0) == TMP_VertexDataUpdateFlags.Uv0)
				{
					mesh.SetUVs(0, this.m_textInfo.meshInfo[i].uvs0);
				}
				if ((flags & TMP_VertexDataUpdateFlags.Uv2) == TMP_VertexDataUpdateFlags.Uv2)
				{
					mesh.uv2 = this.m_textInfo.meshInfo[i].uvs2;
				}
				if ((flags & TMP_VertexDataUpdateFlags.Colors32) == TMP_VertexDataUpdateFlags.Colors32)
				{
					mesh.colors32 = this.m_textInfo.meshInfo[i].colors32;
				}
				mesh.RecalculateBounds();
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0002FA90 File Offset: 0x0002DC90
		public override void UpdateVertexData()
		{
			int materialCount = this.m_textInfo.materialCount;
			for (int i = 0; i < materialCount; i++)
			{
				Mesh mesh;
				if (i == 0)
				{
					mesh = this.m_mesh;
				}
				else
				{
					this.m_textInfo.meshInfo[i].ClearUnusedVertices();
					mesh = this.m_subTextObjects[i].mesh;
				}
				mesh.vertices = this.m_textInfo.meshInfo[i].vertices;
				mesh.SetUVs(0, this.m_textInfo.meshInfo[i].uvs0);
				mesh.uv2 = this.m_textInfo.meshInfo[i].uvs2;
				mesh.colors32 = this.m_textInfo.meshInfo[i].colors32;
				mesh.RecalculateBounds();
			}
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0002FB62 File Offset: 0x0002DD62
		public void UpdateFontAsset()
		{
			this.LoadFontAsset();
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00002AAB File Offset: 0x00000CAB
		public void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00002AAB File Offset: 0x00000CAB
		public void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x0002FB6C File Offset: 0x0002DD6C
		protected override void Awake()
		{
			this.m_renderer = base.GetComponent<Renderer>();
			if (this.m_renderer == null)
			{
				this.m_renderer = base.gameObject.AddComponent<Renderer>();
			}
			this.m_rectTransform = base.rectTransform;
			this.m_transform = this.transform;
			this.m_meshFilter = base.GetComponent<MeshFilter>();
			if (this.m_meshFilter == null)
			{
				this.m_meshFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (this.m_mesh == null)
			{
				this.m_mesh = new Mesh();
				this.m_mesh.hideFlags = HideFlags.HideAndDontSave;
				this.m_meshFilter.sharedMesh = this.m_mesh;
				this.m_textInfo = new TMP_TextInfo(this);
			}
			this.m_meshFilter.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset;
			base.LoadDefaultSettings();
			this.LoadFontAsset();
			if (this.m_TextProcessingArray == null)
			{
				this.m_TextProcessingArray = new TMP_Text.TextProcessingElement[this.m_max_characters];
			}
			this.m_cached_TextElement = new TMP_Character();
			this.m_isFirstAllocation = true;
			this.m_havePropertiesChanged = true;
			this.m_isAwake = true;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x0002FC7C File Offset: 0x0002DE7C
		protected override void OnEnable()
		{
			if (!this.m_isAwake)
			{
				return;
			}
			if (!this.m_isRegisteredForEvents)
			{
				this.m_isRegisteredForEvents = true;
			}
			if (!this.m_IsTextObjectScaleStatic)
			{
				TMP_UpdateManager.RegisterTextObjectForUpdate(this);
			}
			this.meshFilter.sharedMesh = this.mesh;
			this.SetActiveSubMeshes(true);
			this.ComputeMarginSize();
			this.SetAllDirty();
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x0002FCD3 File Offset: 0x0002DED3
		protected override void OnDisable()
		{
			if (!this.m_isAwake)
			{
				return;
			}
			TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
			TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
			this.meshFilter.sharedMesh = null;
			this.SetActiveSubMeshes(false);
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0002FCFD File Offset: 0x0002DEFD
		protected override void OnDestroy()
		{
			if (this.m_mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_mesh);
			}
			this.m_isRegisteredForEvents = false;
			TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
			TMP_UpdateManager.UnRegisterTextObjectForUpdate(this);
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x0002FD2C File Offset: 0x0002DF2C
		protected override void LoadFontAsset()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			if (this.m_fontAsset == null)
			{
				if (TMP_Settings.defaultFontAsset != null)
				{
					this.m_fontAsset = TMP_Settings.defaultFontAsset;
				}
				if (this.m_fontAsset == null)
				{
					Debug.LogWarning("The LiberationSans SDF Font Asset was not found. There is no Font Asset assigned to " + base.gameObject.name + ".", this);
					return;
				}
				if (this.m_fontAsset.characterLookupTable == null)
				{
					Debug.Log("Dictionary is Null!");
				}
				this.m_sharedMaterial = this.m_fontAsset.material;
				this.m_sharedMaterial.SetFloat("_CullMode", 0f);
				this.m_renderer.receiveShadows = false;
				this.m_renderer.shadowCastingMode = ShadowCastingMode.Off;
			}
			else
			{
				if (this.m_fontAsset.characterLookupTable == null)
				{
					this.m_fontAsset.ReadFontAssetDefinition();
				}
				if (this.m_sharedMaterial == null || this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex) == null || this.m_fontAsset.atlasTexture.GetInstanceID() != this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
				{
					if (this.m_fontAsset.material == null)
					{
						Debug.LogWarning(string.Concat(new string[]
						{
							"The Font Atlas Texture of the Font Asset ",
							this.m_fontAsset.name,
							" assigned to ",
							base.gameObject.name,
							" is missing."
						}), this);
					}
					else
					{
						this.m_sharedMaterial = this.m_fontAsset.material;
					}
				}
			}
			this.ValidateEnvMapProperty();
			this.m_padding = this.GetPaddingForMaterial();
			this.m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(this.m_sharedMaterial);
			base.GetSpecialCharacters(this.m_fontAsset);
			this.SetMaterialDirty();
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x0002FEF8 File Offset: 0x0002E0F8
		private void ValidateEnvMapProperty()
		{
			if (this.m_sharedMaterial != null)
			{
				this.m_hasEnvMapProperty = this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_EnvMap) && this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_EnvMap) != null;
				return;
			}
			this.m_hasEnvMapProperty = false;
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0002FF4C File Offset: 0x0002E14C
		private void UpdateEnvMapMatrix()
		{
			if (!this.m_hasEnvMapProperty)
			{
				return;
			}
			Vector3 rotation = this.m_sharedMaterial.GetVector(ShaderUtilities.ID_EnvMatrixRotation);
			if (this.m_currentEnvMapRotation == rotation)
			{
				return;
			}
			this.m_currentEnvMapRotation = rotation;
			this.m_EnvMapMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(rotation), Vector3.one);
			this.m_sharedMaterial.SetMatrix(ShaderUtilities.ID_EnvMatrix, this.m_EnvMapMatrix);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x0002FFC0 File Offset: 0x0002E1C0
		private void SetMask(MaskingTypes maskType)
		{
			switch (maskType)
			{
			case MaskingTypes.MaskOff:
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				return;
			case MaskingTypes.MaskHard:
				this.m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				return;
			case MaskingTypes.MaskSoft:
				this.m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00030072 File Offset: 0x0002E272
		private void SetMaskCoordinates(Vector4 coords)
		{
			this.m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00030085 File Offset: 0x0002E285
		private void SetMaskCoordinates(Vector4 coords, float softX, float softY)
		{
			this.m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessX, softX);
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessY, softY);
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x000300BC File Offset: 0x0002E2BC
		private void EnableMasking()
		{
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				this.m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				this.m_isMaskingEnabled = true;
				this.UpdateMask();
			}
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00030118 File Offset: 0x0002E318
		private void DisableMasking()
		{
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				this.m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				this.m_isMaskingEnabled = false;
				this.UpdateMask();
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00030174 File Offset: 0x0002E374
		private void UpdateMask()
		{
			if (!this.m_isMaskingEnabled)
			{
				return;
			}
			if (this.m_isMaskingEnabled && this.m_fontMaterial == null)
			{
				this.CreateMaterialInstance();
			}
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0003019C File Offset: 0x0002E39C
		protected override Material GetMaterial(Material mat)
		{
			if (this.m_fontMaterial == null || this.m_fontMaterial.GetInstanceID() != mat.GetInstanceID())
			{
				this.m_fontMaterial = this.CreateMaterialInstance(mat);
			}
			this.m_sharedMaterial = this.m_fontMaterial;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetVerticesDirty();
			this.SetMaterialDirty();
			return this.m_sharedMaterial;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x00030204 File Offset: 0x0002E404
		protected override Material[] GetMaterials(Material[] mats)
		{
			int materialCount = this.m_textInfo.materialCount;
			if (this.m_fontMaterials == null)
			{
				this.m_fontMaterials = new Material[materialCount];
			}
			else if (this.m_fontMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize<Material>(ref this.m_fontMaterials, materialCount, false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				if (i == 0)
				{
					this.m_fontMaterials[i] = base.fontMaterial;
				}
				else
				{
					this.m_fontMaterials[i] = this.m_subTextObjects[i].material;
				}
			}
			this.m_fontSharedMaterials = this.m_fontMaterials;
			return this.m_fontMaterials;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00030291 File Offset: 0x0002E491
		protected override void SetSharedMaterial(Material mat)
		{
			this.m_sharedMaterial = mat;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetMaterialDirty();
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x000302AC File Offset: 0x0002E4AC
		protected override Material[] GetSharedMaterials()
		{
			int materialCount = this.m_textInfo.materialCount;
			if (this.m_fontSharedMaterials == null)
			{
				this.m_fontSharedMaterials = new Material[materialCount];
			}
			else if (this.m_fontSharedMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize<Material>(ref this.m_fontSharedMaterials, materialCount, false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				if (i == 0)
				{
					this.m_fontSharedMaterials[i] = this.m_sharedMaterial;
				}
				else
				{
					this.m_fontSharedMaterials[i] = this.m_subTextObjects[i].sharedMaterial;
				}
			}
			return this.m_fontSharedMaterials;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00030330 File Offset: 0x0002E530
		protected override void SetSharedMaterials(Material[] materials)
		{
			int materialCount = this.m_textInfo.materialCount;
			if (this.m_fontSharedMaterials == null)
			{
				this.m_fontSharedMaterials = new Material[materialCount];
			}
			else if (this.m_fontSharedMaterials.Length != materialCount)
			{
				TMP_TextInfo.Resize<Material>(ref this.m_fontSharedMaterials, materialCount, false);
			}
			for (int i = 0; i < materialCount; i++)
			{
				Texture mat_MainTex = materials[i].GetTexture(ShaderUtilities.ID_MainTex);
				if (i == 0)
				{
					if (!(mat_MainTex == null) && mat_MainTex.GetInstanceID() == this.m_sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
					{
						this.m_sharedMaterial = (this.m_fontSharedMaterials[i] = materials[i]);
						this.m_padding = this.GetPaddingForMaterial(this.m_sharedMaterial);
					}
				}
				else if (!(mat_MainTex == null) && mat_MainTex.GetInstanceID() == this.m_subTextObjects[i].sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID() && this.m_subTextObjects[i].isDefaultMaterial)
				{
					this.m_subTextObjects[i].sharedMaterial = (this.m_fontSharedMaterials[i] = materials[i]);
				}
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00030444 File Offset: 0x0002E644
		protected override void SetOutlineThickness(float thickness)
		{
			thickness = Mathf.Clamp01(thickness);
			this.m_renderer.material.SetFloat(ShaderUtilities.ID_OutlineWidth, thickness);
			if (this.m_fontMaterial == null)
			{
				this.m_fontMaterial = this.m_renderer.material;
			}
			this.m_fontMaterial = this.m_renderer.material;
			this.m_sharedMaterial = this.m_fontMaterial;
			this.m_padding = this.GetPaddingForMaterial();
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000304B8 File Offset: 0x0002E6B8
		protected override void SetFaceColor(Color32 color)
		{
			this.m_renderer.material.SetColor(ShaderUtilities.ID_FaceColor, color);
			if (this.m_fontMaterial == null)
			{
				this.m_fontMaterial = this.m_renderer.material;
			}
			this.m_sharedMaterial = this.m_fontMaterial;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x0003050C File Offset: 0x0002E70C
		protected override void SetOutlineColor(Color32 color)
		{
			this.m_renderer.material.SetColor(ShaderUtilities.ID_OutlineColor, color);
			if (this.m_fontMaterial == null)
			{
				this.m_fontMaterial = this.m_renderer.material;
			}
			this.m_sharedMaterial = this.m_fontMaterial;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00030560 File Offset: 0x0002E760
		private void CreateMaterialInstance()
		{
			Material mat = new Material(this.m_sharedMaterial);
			mat.shaderKeywords = this.m_sharedMaterial.shaderKeywords;
			Material material = mat;
			material.name += " Instance";
			this.m_fontMaterial = mat;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000305A8 File Offset: 0x0002E7A8
		protected override void SetShaderDepth()
		{
			if (this.m_isOverlay)
			{
				Material mat = this.m_renderer.material;
				this.m_sharedMaterial = mat;
				return;
			}
			Material mat2 = this.m_renderer.material;
			this.m_sharedMaterial = mat2;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000305E4 File Offset: 0x0002E7E4
		protected override void SetCulling()
		{
			if (this.m_isCullingEnabled)
			{
				this.m_renderer.material.SetFloat("_CullMode", 2f);
				for (int i = 1; i < this.m_subTextObjects.Length; i++)
				{
					if (!(this.m_subTextObjects[i] != null))
					{
						return;
					}
					Renderer renderer = this.m_subTextObjects[i].renderer;
					if (renderer != null)
					{
						renderer.material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 2f);
					}
				}
			}
			else
			{
				this.m_renderer.material.SetFloat("_CullMode", 0f);
				int j = 1;
				while (j < this.m_subTextObjects.Length && this.m_subTextObjects[j] != null)
				{
					Renderer renderer2 = this.m_subTextObjects[j].renderer;
					if (renderer2 != null)
					{
						renderer2.material.SetFloat(ShaderUtilities.ShaderTag_CullMode, 0f);
					}
					j++;
				}
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000306CC File Offset: 0x0002E8CC
		private void SetPerspectiveCorrection()
		{
			if (this.m_isOrthographic)
			{
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0f);
				return;
			}
			this.m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0.875f);
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00030704 File Offset: 0x0002E904
		internal override int SetArraySizes(TMP_Text.TextProcessingElement[] textProcessingArray)
		{
			int spriteCount = 0;
			this.m_totalCharacterCount = 0;
			this.m_isUsingBold = false;
			this.m_isTextLayoutPhase = false;
			this.tag_NoParsing = false;
			this.m_FontStyleInternal = this.m_fontStyle;
			this.m_fontStyleStack.Clear();
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : this.m_fontWeight);
			this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
			this.m_currentFontAsset = this.m_fontAsset;
			this.m_currentMaterial = this.m_sharedMaterial;
			this.m_currentMaterialIndex = 0;
			TMP_Text.m_materialReferenceStack.SetDefault(new MaterialReference(this.m_currentMaterialIndex, this.m_currentFontAsset, null, this.m_currentMaterial, this.m_padding));
			TMP_Text.m_materialReferenceIndexLookup.Clear();
			MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
			if (this.m_textInfo == null)
			{
				this.m_textInfo = new TMP_TextInfo(this.m_InternalTextProcessingArraySize);
			}
			else if (this.m_textInfo.characterInfo.Length < this.m_InternalTextProcessingArraySize)
			{
				TMP_TextInfo.Resize<TMP_CharacterInfo>(ref this.m_textInfo.characterInfo, this.m_InternalTextProcessingArraySize, false);
			}
			this.m_textElementType = TMP_TextElementType.Character;
			if (this.m_overflowMode == TextOverflowModes.Ellipsis)
			{
				base.GetEllipsisSpecialCharacter(this.m_currentFontAsset);
				if (this.m_Ellipsis.character != null)
				{
					if (this.m_Ellipsis.fontAsset.GetInstanceID() != this.m_currentFontAsset.GetInstanceID())
					{
						if (TMP_Settings.matchMaterialPreset && this.m_currentMaterial.GetInstanceID() != this.m_Ellipsis.fontAsset.material.GetInstanceID())
						{
							this.m_Ellipsis.material = TMP_MaterialManager.GetFallbackMaterial(this.m_currentMaterial, this.m_Ellipsis.fontAsset.material);
						}
						else
						{
							this.m_Ellipsis.material = this.m_Ellipsis.fontAsset.material;
						}
						this.m_Ellipsis.materialIndex = MaterialReference.AddMaterialReference(this.m_Ellipsis.material, this.m_Ellipsis.fontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
						TMP_Text.m_materialReferences[this.m_Ellipsis.materialIndex].referenceCount = 0;
					}
				}
				else
				{
					this.m_overflowMode = TextOverflowModes.Truncate;
					if (!TMP_Settings.warningsDisabled)
					{
						Debug.LogWarning("The character used for Ellipsis is not available in font asset [" + this.m_currentFontAsset.name + "] or any potential fallbacks. Switching Text Overflow mode to Truncate.", this);
					}
				}
			}
			bool ligature = this.m_ActiveFontFeatures.Contains(OTL_FeatureTag.liga);
			if (this.m_overflowMode == TextOverflowModes.Linked && this.m_linkedTextComponent != null && !this.m_isCalculatingPreferredValues)
			{
				this.m_linkedTextComponent.text = string.Empty;
			}
			int i = 0;
			while (i < textProcessingArray.Length && textProcessingArray[i].unicode != 0U)
			{
				if (this.m_textInfo.characterInfo == null || this.m_totalCharacterCount >= this.m_textInfo.characterInfo.Length)
				{
					TMP_TextInfo.Resize<TMP_CharacterInfo>(ref this.m_textInfo.characterInfo, this.m_totalCharacterCount + 1, true);
				}
				uint unicode = textProcessingArray[i].unicode;
				if (!this.m_isRichText || unicode != 60U)
				{
					goto IL_047F;
				}
				int prev_MaterialIndex = this.m_currentMaterialIndex;
				int endTagIndex;
				if (!base.ValidateHtmlTag(textProcessingArray, i + 1, out endTagIndex))
				{
					goto IL_047F;
				}
				int tagStartIndex = textProcessingArray[i].stringIndex;
				i = endTagIndex;
				if ((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
				{
					this.m_isUsingBold = true;
				}
				if (this.m_textElementType == TMP_TextElementType.Sprite)
				{
					MaterialReference[] materialReferences = TMP_Text.m_materialReferences;
					int currentMaterialIndex = this.m_currentMaterialIndex;
					materialReferences[currentMaterialIndex].referenceCount = materialReferences[currentMaterialIndex].referenceCount + 1;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].character = (char)(57344 + this.m_spriteIndex);
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].fontAsset = this.m_currentFontAsset;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].textElement = this.m_currentSpriteAsset.spriteCharacterTable[this.m_spriteIndex];
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = this.m_textElementType;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].index = tagStartIndex;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].stringLength = textProcessingArray[i].stringIndex - tagStartIndex + 1;
					this.m_textElementType = TMP_TextElementType.Character;
					this.m_currentMaterialIndex = prev_MaterialIndex;
					spriteCount++;
					this.m_totalCharacterCount++;
				}
				IL_0D09:
				i++;
				continue;
				IL_047F:
				bool isUsingAlternativeTypeface = false;
				bool isUsingFallbackOrAlternativeTypeface = false;
				TMP_FontAsset prev_fontAsset = this.m_currentFontAsset;
				Material prev_material = this.m_currentMaterial;
				int prev_materialIndex = this.m_currentMaterialIndex;
				if (this.m_textElementType == TMP_TextElementType.Character)
				{
					if ((this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
					{
						if (char.IsLower((char)unicode))
						{
							unicode = (uint)char.ToUpper((char)unicode);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
					{
						if (char.IsUpper((char)unicode))
						{
							unicode = (uint)char.ToLower((char)unicode);
						}
					}
					else if ((this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)unicode))
					{
						unicode = (uint)char.ToUpper((char)unicode);
					}
				}
				TMP_TextElement character = null;
				uint nextCharacter = ((i + 1 < textProcessingArray.Length) ? textProcessingArray[i + 1].unicode : 0U);
				if (base.emojiFallbackSupport && ((TMP_TextParsingUtilities.IsEmojiPresentationForm(unicode) && nextCharacter != 65038U) || (TMP_TextParsingUtilities.IsEmoji(unicode) && nextCharacter == 65039U)) && TMP_Settings.emojiFallbackTextAssets != null && TMP_Settings.emojiFallbackTextAssets.Count > 0)
				{
					character = TMP_FontAssetUtilities.GetTextElementFromTextAssets(unicode, this.m_currentFontAsset, TMP_Settings.emojiFallbackTextAssets, true, base.fontStyle, base.fontWeight, out isUsingAlternativeTypeface);
				}
				if (character == null)
				{
					character = base.GetTextElement(unicode, this.m_currentFontAsset, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface);
				}
				if (character == null)
				{
					base.DoMissingGlyphCallback((int)unicode, textProcessingArray[i].stringIndex, this.m_currentFontAsset);
					uint srcGlyph = unicode;
					unicode = (textProcessingArray[i].unicode = (uint)((TMP_Settings.missingGlyphCharacter == 0) ? 9633 : TMP_Settings.missingGlyphCharacter));
					character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(unicode, this.m_currentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface);
					if (character == null && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
					{
						character = TMP_FontAssetUtilities.GetCharacterFromFontAssets(unicode, this.m_currentFontAsset, TMP_Settings.fallbackFontAssets, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface);
					}
					if (character == null && TMP_Settings.defaultFontAsset != null)
					{
						character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(unicode, TMP_Settings.defaultFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface);
					}
					if (character == null)
					{
						unicode = (textProcessingArray[i].unicode = 32U);
						character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(unicode, this.m_currentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface);
					}
					if (character == null)
					{
						unicode = (textProcessingArray[i].unicode = 3U);
						character = TMP_FontAssetUtilities.GetCharacterFromFontAsset(unicode, this.m_currentFontAsset, true, this.m_FontStyleInternal, this.m_FontWeightInternal, out isUsingAlternativeTypeface);
					}
					if (!TMP_Settings.warningsDisabled)
					{
						Debug.LogWarning((srcGlyph > 65535U) ? string.Format("The character with Unicode value \\U{0:X8} was not found in the [{1}] font asset or any potential fallbacks. It was replaced by Unicode character \\u{2:X4} in text object [{3}].", new object[]
						{
							srcGlyph,
							this.m_fontAsset.name,
							character.unicode,
							base.name
						}) : string.Format("The character with Unicode value \\u{0:X4} was not found in the [{1}] font asset or any potential fallbacks. It was replaced by Unicode character \\u{2:X4} in text object [{3}].", new object[]
						{
							srcGlyph,
							this.m_fontAsset.name,
							character.unicode,
							base.name
						}), this);
					}
				}
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].alternativeGlyph = null;
				if (character.elementType == TextElementType.Character)
				{
					if (character.textAsset.instanceID != this.m_currentFontAsset.instanceID)
					{
						isUsingFallbackOrAlternativeTypeface = true;
						this.m_currentFontAsset = character.textAsset as TMP_FontAsset;
					}
					if ((nextCharacter >= 65024U && nextCharacter <= 65039U) || (nextCharacter >= 917760U && nextCharacter <= 917999U))
					{
						uint variantGlyphIndex = this.m_currentFontAsset.GetGlyphVariantIndex(unicode, nextCharacter);
						Glyph glyph;
						if (variantGlyphIndex != 0U && this.m_currentFontAsset.TryAddGlyphInternal(variantGlyphIndex, out glyph))
						{
							this.m_textInfo.characterInfo[this.m_totalCharacterCount].alternativeGlyph = glyph;
						}
						textProcessingArray[i + 1].unicode = 26U;
						i++;
					}
					List<LigatureSubstitutionRecord> records;
					if (ligature && this.m_currentFontAsset.fontFeatureTable.m_LigatureSubstitutionRecordLookup.TryGetValue(character.glyphIndex, out records))
					{
						if (records == null)
						{
							break;
						}
						for (int j = 0; j < records.Count; j++)
						{
							LigatureSubstitutionRecord record = records[j];
							int componentCount = record.componentGlyphIDs.Length;
							uint ligatureGlyphID = record.ligatureGlyphID;
							for (int k = 1; k < componentCount; k++)
							{
								uint componentUnicode = textProcessingArray[i + k].unicode;
								if (this.m_currentFontAsset.GetGlyphIndex(componentUnicode) != record.componentGlyphIDs[k])
								{
									ligatureGlyphID = 0U;
									break;
								}
							}
							Glyph glyph2;
							if (ligatureGlyphID != 0U && this.m_currentFontAsset.TryAddGlyphInternal(ligatureGlyphID, out glyph2))
							{
								this.m_textInfo.characterInfo[this.m_totalCharacterCount].alternativeGlyph = glyph2;
								for (int c = 0; c < componentCount; c++)
								{
									if (c == 0)
									{
										textProcessingArray[i + c].length = componentCount;
									}
									else
									{
										textProcessingArray[i + c].unicode = 26U;
									}
								}
								i += componentCount - 1;
								break;
							}
						}
					}
				}
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = TMP_TextElementType.Character;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].textElement = character;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].isUsingAlternateTypeface = isUsingAlternativeTypeface;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].character = (char)unicode;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].index = textProcessingArray[i].stringIndex;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].stringLength = textProcessingArray[i].length;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].fontAsset = this.m_currentFontAsset;
				if (character.elementType == TextElementType.Sprite)
				{
					TMP_SpriteAsset spriteAssetRef = character.textAsset as TMP_SpriteAsset;
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(spriteAssetRef.material, spriteAssetRef, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
					MaterialReference[] materialReferences2 = TMP_Text.m_materialReferences;
					int currentMaterialIndex2 = this.m_currentMaterialIndex;
					materialReferences2[currentMaterialIndex2].referenceCount = materialReferences2[currentMaterialIndex2].referenceCount + 1;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].elementType = TMP_TextElementType.Sprite;
					this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
					this.m_textElementType = TMP_TextElementType.Character;
					this.m_currentMaterialIndex = prev_materialIndex;
					spriteCount++;
					this.m_totalCharacterCount++;
					goto IL_0D09;
				}
				if (isUsingFallbackOrAlternativeTypeface && this.m_currentFontAsset.instanceID != this.m_fontAsset.instanceID)
				{
					if (TMP_Settings.matchMaterialPreset)
					{
						this.m_currentMaterial = TMP_MaterialManager.GetFallbackMaterial(this.m_currentMaterial, this.m_currentFontAsset.material);
					}
					else
					{
						this.m_currentMaterial = this.m_currentFontAsset.material;
					}
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
				}
				if (character != null && character.glyph.atlasIndex > 0)
				{
					this.m_currentMaterial = TMP_MaterialManager.GetFallbackMaterial(this.m_currentFontAsset, this.m_currentMaterial, character.glyph.atlasIndex);
					this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(this.m_currentMaterial, this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
					isUsingFallbackOrAlternativeTypeface = true;
				}
				if (!char.IsWhiteSpace((char)unicode) && unicode != 8203U)
				{
					if (TMP_Text.m_materialReferences[this.m_currentMaterialIndex].referenceCount < 16383)
					{
						MaterialReference[] materialReferences3 = TMP_Text.m_materialReferences;
						int currentMaterialIndex3 = this.m_currentMaterialIndex;
						materialReferences3[currentMaterialIndex3].referenceCount = materialReferences3[currentMaterialIndex3].referenceCount + 1;
					}
					else
					{
						this.m_currentMaterialIndex = MaterialReference.AddMaterialReference(new Material(this.m_currentMaterial), this.m_currentFontAsset, ref TMP_Text.m_materialReferences, TMP_Text.m_materialReferenceIndexLookup);
						MaterialReference[] materialReferences4 = TMP_Text.m_materialReferences;
						int currentMaterialIndex4 = this.m_currentMaterialIndex;
						materialReferences4[currentMaterialIndex4].referenceCount = materialReferences4[currentMaterialIndex4].referenceCount + 1;
					}
				}
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].material = this.m_currentMaterial;
				this.m_textInfo.characterInfo[this.m_totalCharacterCount].materialReferenceIndex = this.m_currentMaterialIndex;
				TMP_Text.m_materialReferences[this.m_currentMaterialIndex].isFallbackMaterial = isUsingFallbackOrAlternativeTypeface;
				if (isUsingFallbackOrAlternativeTypeface)
				{
					TMP_Text.m_materialReferences[this.m_currentMaterialIndex].fallbackMaterial = prev_material;
					this.m_currentFontAsset = prev_fontAsset;
					this.m_currentMaterial = prev_material;
					this.m_currentMaterialIndex = prev_materialIndex;
				}
				this.m_totalCharacterCount++;
				goto IL_0D09;
			}
			if (this.m_isCalculatingPreferredValues)
			{
				this.m_isCalculatingPreferredValues = false;
				return this.m_totalCharacterCount;
			}
			this.m_textInfo.spriteCount = spriteCount;
			int materialCount = (this.m_textInfo.materialCount = TMP_Text.m_materialReferenceIndexLookup.Count);
			if (materialCount > this.m_textInfo.meshInfo.Length)
			{
				TMP_TextInfo.Resize<TMP_MeshInfo>(ref this.m_textInfo.meshInfo, materialCount, false);
			}
			if (materialCount > this.m_subTextObjects.Length)
			{
				TMP_TextInfo.Resize<TMP_SubMesh>(ref this.m_subTextObjects, Mathf.NextPowerOfTwo(materialCount + 1));
			}
			if (this.m_VertexBufferAutoSizeReduction && this.m_textInfo.characterInfo.Length - this.m_totalCharacterCount > 256)
			{
				TMP_TextInfo.Resize<TMP_CharacterInfo>(ref this.m_textInfo.characterInfo, Mathf.Max(this.m_totalCharacterCount + 1, 256), true);
			}
			for (int l = 0; l < materialCount; l++)
			{
				if (l > 0)
				{
					if (this.m_subTextObjects[l] == null)
					{
						this.m_subTextObjects[l] = TMP_SubMesh.AddSubTextObject(this, TMP_Text.m_materialReferences[l]);
						this.m_textInfo.meshInfo[l].vertices = null;
					}
					if (this.m_subTextObjects[l].sharedMaterial == null || this.m_subTextObjects[l].sharedMaterial.GetInstanceID() != TMP_Text.m_materialReferences[l].material.GetInstanceID())
					{
						this.m_subTextObjects[l].sharedMaterial = TMP_Text.m_materialReferences[l].material;
						this.m_subTextObjects[l].fontAsset = TMP_Text.m_materialReferences[l].fontAsset;
						this.m_subTextObjects[l].spriteAsset = TMP_Text.m_materialReferences[l].spriteAsset;
					}
					if (TMP_Text.m_materialReferences[l].isFallbackMaterial)
					{
						this.m_subTextObjects[l].fallbackMaterial = TMP_Text.m_materialReferences[l].material;
						this.m_subTextObjects[l].fallbackSourceMaterial = TMP_Text.m_materialReferences[l].fallbackMaterial;
					}
				}
				int referenceCount = TMP_Text.m_materialReferences[l].referenceCount;
				if (this.m_textInfo.meshInfo[l].vertices == null || this.m_textInfo.meshInfo[l].vertices.Length < referenceCount * 4)
				{
					if (this.m_textInfo.meshInfo[l].vertices == null)
					{
						if (l == 0)
						{
							this.m_textInfo.meshInfo[l] = new TMP_MeshInfo(this.m_mesh, referenceCount + 1);
						}
						else
						{
							this.m_textInfo.meshInfo[l] = new TMP_MeshInfo(this.m_subTextObjects[l].mesh, referenceCount + 1);
						}
					}
					else
					{
						this.m_textInfo.meshInfo[l].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1));
					}
				}
				else if (this.m_VertexBufferAutoSizeReduction && referenceCount > 0 && this.m_textInfo.meshInfo[l].vertices.Length / 4 - referenceCount > 256)
				{
					this.m_textInfo.meshInfo[l].ResizeMeshInfo((referenceCount > 1024) ? (referenceCount + 256) : Mathf.NextPowerOfTwo(referenceCount + 1));
				}
				this.m_textInfo.meshInfo[l].material = TMP_Text.m_materialReferences[l].material;
			}
			int m = materialCount;
			while (m < this.m_subTextObjects.Length && this.m_subTextObjects[m] != null)
			{
				if (m < this.m_textInfo.meshInfo.Length)
				{
					this.m_textInfo.meshInfo[m].ClearUnusedVertices(0, true);
				}
				m++;
			}
			return this.m_totalCharacterCount;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00031824 File Offset: 0x0002FA24
		public override void ComputeMarginSize()
		{
			if (base.rectTransform != null)
			{
				Rect rect = this.m_rectTransform.rect;
				this.m_marginWidth = rect.width - this.m_margin.x - this.m_margin.z;
				this.m_marginHeight = rect.height - this.m_margin.y - this.m_margin.w;
				this.m_PreviousRectTransformSize = rect.size;
				this.m_PreviousPivotPosition = this.m_rectTransform.pivot;
				this.m_RectTransformCorners = this.GetTextContainerLocalCorners();
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000318C2 File Offset: 0x0002FAC2
		protected override void OnDidApplyAnimationProperties()
		{
			this.m_havePropertiesChanged = true;
			this.isMaskUpdateRequired = true;
			this.SetVerticesDirty();
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000318D8 File Offset: 0x0002FAD8
		protected override void OnTransformParentChanged()
		{
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000318E8 File Offset: 0x0002FAE8
		protected override void OnRectTransformDimensionsChange()
		{
			if (base.rectTransform != null && Mathf.Abs(this.m_rectTransform.rect.width - this.m_PreviousRectTransformSize.x) < 0.0001f && Mathf.Abs(this.m_rectTransform.rect.height - this.m_PreviousRectTransformSize.y) < 0.0001f && Mathf.Abs(this.m_rectTransform.pivot.x - this.m_PreviousPivotPosition.x) < 0.0001f && Mathf.Abs(this.m_rectTransform.pivot.y - this.m_PreviousPivotPosition.y) < 0.0001f)
			{
				return;
			}
			this.ComputeMarginSize();
			this.SetVerticesDirty();
			this.SetLayoutDirty();
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000319C0 File Offset: 0x0002FBC0
		internal override void InternalUpdate()
		{
			if (!this.m_havePropertiesChanged)
			{
				float lossyScaleY = this.m_rectTransform.lossyScale.y;
				if (lossyScaleY != this.m_previousLossyScaleY && this.m_TextProcessingArray[0].unicode != 0U)
				{
					float scaleDelta = lossyScaleY / this.m_previousLossyScaleY;
					if (scaleDelta < 0.8f || scaleDelta > 1.25f)
					{
						this.UpdateSDFScale(scaleDelta);
						this.m_previousLossyScaleY = lossyScaleY;
					}
				}
			}
			if (this.m_isUsingLegacyAnimationComponent)
			{
				this.m_havePropertiesChanged = true;
				this.OnPreRenderObject();
			}
			this.UpdateEnvMapMatrix();
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00031A44 File Offset: 0x0002FC44
		private void OnPreRenderObject()
		{
			if (!this.m_isAwake || (!this.IsActive() && !this.m_ignoreActiveState))
			{
				return;
			}
			if (this.m_fontAsset == null)
			{
				Debug.LogWarning("Please assign a Font Asset to this " + this.transform.name + " gameobject.", this);
				return;
			}
			if (this.m_havePropertiesChanged || this.m_isLayoutDirty)
			{
				if (this.isMaskUpdateRequired)
				{
					this.UpdateMask();
					this.isMaskUpdateRequired = false;
				}
				if (this.checkPaddingRequired)
				{
					this.UpdateMeshPadding();
				}
				base.ParseInputText();
				TMP_FontAsset.UpdateFontAssetsInUpdateQueue();
				if (this.m_enableAutoSizing)
				{
					this.m_fontSize = Mathf.Clamp(this.m_fontSizeBase, this.m_fontSizeMin, this.m_fontSizeMax);
				}
				this.m_maxFontSize = this.m_fontSizeMax;
				this.m_minFontSize = this.m_fontSizeMin;
				this.m_lineSpacingDelta = 0f;
				this.m_charWidthAdjDelta = 0f;
				this.m_isTextTruncated = false;
				this.m_havePropertiesChanged = false;
				this.m_isLayoutDirty = false;
				this.m_ignoreActiveState = false;
				this.m_IsAutoSizePointSizeSet = false;
				this.m_AutoSizeIterationCount = 0;
				this.SetActiveSubTextObjectRenderers(this.m_renderer.enabled);
				while (!this.m_IsAutoSizePointSizeSet)
				{
					this.GenerateTextMesh();
					this.m_AutoSizeIterationCount++;
				}
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00031B88 File Offset: 0x0002FD88
		protected virtual void GenerateTextMesh()
		{
			if (this.m_fontAsset == null || this.m_fontAsset.characterLookupTable == null)
			{
				Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned to Object ID: " + base.GetInstanceID().ToString());
				this.m_IsAutoSizePointSizeSet = true;
				return;
			}
			if (this.m_textInfo != null)
			{
				this.m_textInfo.Clear();
			}
			if (this.m_TextProcessingArray == null || this.m_TextProcessingArray.Length == 0 || this.m_TextProcessingArray[0].unicode == 0U)
			{
				this.ClearMesh(true);
				this.m_preferredWidth = 0f;
				this.m_preferredHeight = 0f;
				TMPro_EventManager.ON_TEXT_CHANGED(this);
				this.m_IsAutoSizePointSizeSet = true;
				return;
			}
			this.m_currentFontAsset = this.m_fontAsset;
			this.m_currentMaterial = this.m_sharedMaterial;
			this.m_currentMaterialIndex = 0;
			TMP_Text.m_materialReferenceStack.SetDefault(new MaterialReference(this.m_currentMaterialIndex, this.m_currentFontAsset, null, this.m_currentMaterial, this.m_padding));
			this.m_currentSpriteAsset = this.m_spriteAsset;
			if (this.m_spriteAnimator != null)
			{
				this.m_spriteAnimator.StopAllAnimations();
			}
			int totalCharacterCount = this.m_totalCharacterCount;
			float baseScale = this.m_fontSize / this.m_fontAsset.m_FaceInfo.pointSize * this.m_fontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
			float currentElementScale = baseScale;
			float currentEmScale = this.m_fontSize * 0.01f * (this.m_isOrthographic ? 1f : 0.1f);
			this.m_fontScaleMultiplier = 1f;
			this.m_currentFontSize = this.m_fontSize;
			this.m_sizeStack.SetDefault(this.m_currentFontSize);
			uint charCode = 0U;
			this.m_FontStyleInternal = this.m_fontStyle;
			this.m_FontWeightInternal = (((this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold) ? FontWeight.Bold : this.m_fontWeight);
			this.m_FontWeightStack.SetDefault(this.m_FontWeightInternal);
			this.m_fontStyleStack.Clear();
			this.m_lineJustification = this.m_HorizontalAlignment;
			this.m_lineJustificationStack.SetDefault(this.m_lineJustification);
			float padding = 0f;
			this.m_baselineOffset = 0f;
			this.m_baselineOffsetStack.Clear();
			bool beginUnderline = false;
			Vector3 underline_start = Vector3.zero;
			Vector3 underline_end = Vector3.zero;
			bool beginStrikethrough = false;
			Vector3 strikethrough_start = Vector3.zero;
			Vector3 strikethrough_end = Vector3.zero;
			bool beginHighlight = false;
			Vector3 highlight_start = Vector3.zero;
			Vector3 highlight_end = Vector3.zero;
			this.m_fontColor32 = this.m_fontColor;
			this.m_htmlColor = this.m_fontColor32;
			this.m_underlineColor = this.m_htmlColor;
			this.m_strikethroughColor = this.m_htmlColor;
			this.m_colorStack.SetDefault(this.m_htmlColor);
			this.m_underlineColorStack.SetDefault(this.m_htmlColor);
			this.m_strikethroughColorStack.SetDefault(this.m_htmlColor);
			this.m_HighlightStateStack.SetDefault(new HighlightState(this.m_htmlColor, TMP_Offset.zero));
			this.m_colorGradientPreset = null;
			this.m_colorGradientStack.SetDefault(null);
			this.m_ItalicAngle = (int)this.m_currentFontAsset.italicStyle;
			this.m_ItalicAngleStack.SetDefault(this.m_ItalicAngle);
			this.m_actionStack.Clear();
			this.m_FXScale = Vector3.one;
			this.m_FXRotation = Quaternion.identity;
			this.m_lineOffset = 0f;
			this.m_lineHeight = -32767f;
			float lineGap = this.m_currentFontAsset.m_FaceInfo.lineHeight - (this.m_currentFontAsset.m_FaceInfo.ascentLine - this.m_currentFontAsset.m_FaceInfo.descentLine);
			this.m_cSpacing = 0f;
			this.m_monoSpacing = 0f;
			this.m_xAdvance = 0f;
			this.tag_LineIndent = 0f;
			this.tag_Indent = 0f;
			this.m_indentStack.SetDefault(0f);
			this.tag_NoParsing = false;
			this.m_characterCount = 0;
			this.m_firstCharacterOfLine = this.m_firstVisibleCharacter;
			this.m_lastCharacterOfLine = 0;
			this.m_firstVisibleCharacterOfLine = 0;
			this.m_lastVisibleCharacterOfLine = 0;
			this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
			this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
			this.m_lineNumber = 0;
			this.m_startOfLineAscender = 0f;
			this.m_startOfLineDescender = 0f;
			this.m_lineVisibleCharacterCount = 0;
			this.m_lineVisibleSpaceCount = 0;
			bool isStartOfNewLine = true;
			this.m_IsDrivenLineSpacing = false;
			this.m_firstOverflowCharacterIndex = -1;
			this.m_LastBaseGlyphIndex = int.MinValue;
			bool kerning = this.m_ActiveFontFeatures.Contains(OTL_FeatureTag.kern);
			bool markToBase = this.m_ActiveFontFeatures.Contains(OTL_FeatureTag.mark);
			bool markToMark = this.m_ActiveFontFeatures.Contains(OTL_FeatureTag.mkmk);
			this.m_pageNumber = 0;
			int pageToDisplay = Mathf.Clamp(this.m_pageToDisplay - 1, 0, this.m_textInfo.pageInfo.Length - 1);
			this.m_textInfo.ClearPageInfo();
			Vector4 margins = this.m_margin;
			float marginWidth = ((this.m_marginWidth > 0f) ? this.m_marginWidth : 0f);
			float marginHeight = ((this.m_marginHeight > 0f) ? this.m_marginHeight : 0f);
			this.m_marginLeft = 0f;
			this.m_marginRight = 0f;
			this.m_width = -1f;
			float widthOfTextArea = marginWidth + 0.0001f - this.m_marginLeft - this.m_marginRight;
			this.m_meshExtents.min = TMP_Text.k_LargePositiveVector2;
			this.m_meshExtents.max = TMP_Text.k_LargeNegativeVector2;
			this.m_textInfo.ClearLineInfo();
			this.m_maxCapHeight = 0f;
			this.m_maxTextAscender = 0f;
			this.m_ElementDescender = 0f;
			this.m_PageAscender = 0f;
			float maxVisibleDescender = 0f;
			bool isMaxVisibleDescenderSet = false;
			this.m_isNewPage = false;
			bool isFirstWordOfLine = true;
			this.m_isNonBreakingSpace = false;
			bool ignoreNonBreakingSpace = false;
			int lastSoftLineBreak = 0;
			TMP_Text.CharacterSubstitution characterToSubstitute = new TMP_Text.CharacterSubstitution(-1, 0U);
			bool isSoftHyphenIgnored = false;
			base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, -1, -1);
			base.SaveWordWrappingState(ref TMP_Text.m_SavedLineState, -1, -1);
			base.SaveWordWrappingState(ref TMP_Text.m_SavedEllipsisState, -1, -1);
			base.SaveWordWrappingState(ref TMP_Text.m_SavedLastValidState, -1, -1);
			base.SaveWordWrappingState(ref TMP_Text.m_SavedSoftLineBreakState, -1, -1);
			TMP_Text.m_EllipsisInsertionCandidateStack.Clear();
			int restoreCount = 0;
			int i = 0;
			while (i < this.m_TextProcessingArray.Length && this.m_TextProcessingArray[i].unicode != 0U)
			{
				charCode = this.m_TextProcessingArray[i].unicode;
				if (restoreCount > 5)
				{
					Debug.LogError("Line breaking recursion max threshold hit... Character [" + charCode.ToString() + "] index: " + i.ToString());
					characterToSubstitute.index = this.m_characterCount;
					characterToSubstitute.unicode = 3U;
				}
				if (charCode != 26U)
				{
					if (this.m_isRichText && charCode == 60U)
					{
						this.m_isTextLayoutPhase = true;
						this.m_textElementType = TMP_TextElementType.Character;
						int endTagIndex;
						if (base.ValidateHtmlTag(this.m_TextProcessingArray, i + 1, out endTagIndex))
						{
							i = endTagIndex;
							if (this.m_textElementType == TMP_TextElementType.Character)
							{
								goto IL_40F0;
							}
						}
					}
					else
					{
						this.m_textElementType = this.m_textInfo.characterInfo[this.m_characterCount].elementType;
						this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
						this.m_currentFontAsset = this.m_textInfo.characterInfo[this.m_characterCount].fontAsset;
					}
					int previousMaterialIndex = this.m_currentMaterialIndex;
					bool isUsingAltTypeface = this.m_textInfo.characterInfo[this.m_characterCount].isUsingAlternateTypeface;
					this.m_isTextLayoutPhase = false;
					bool isInjectedCharacter = false;
					if (characterToSubstitute.index == this.m_characterCount)
					{
						charCode = characterToSubstitute.unicode;
						this.m_textElementType = TMP_TextElementType.Character;
						isInjectedCharacter = true;
						if (charCode != 3U)
						{
							if (charCode != 45U)
							{
								if (charCode == 8230U)
								{
									this.m_textInfo.characterInfo[this.m_characterCount].textElement = this.m_Ellipsis.character;
									this.m_textInfo.characterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
									this.m_textInfo.characterInfo[this.m_characterCount].fontAsset = this.m_Ellipsis.fontAsset;
									this.m_textInfo.characterInfo[this.m_characterCount].material = this.m_Ellipsis.material;
									this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex = this.m_Ellipsis.materialIndex;
									MaterialReference[] materialReferences = TMP_Text.m_materialReferences;
									int materialIndex = this.m_Underline.materialIndex;
									materialReferences[materialIndex].referenceCount = materialReferences[materialIndex].referenceCount + 1;
									this.m_isTextTruncated = true;
									characterToSubstitute.index = this.m_characterCount + 1;
									characterToSubstitute.unicode = 3U;
								}
							}
						}
						else
						{
							this.m_textInfo.characterInfo[this.m_characterCount].textElement = this.m_currentFontAsset.characterLookupTable[3U];
							this.m_isTextTruncated = true;
						}
					}
					if (this.m_characterCount < this.m_firstVisibleCharacter && charCode != 3U)
					{
						this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
						this.m_textInfo.characterInfo[this.m_characterCount].character = '\u200b';
						this.m_textInfo.characterInfo[this.m_characterCount].lineNumber = 0;
						this.m_characterCount++;
					}
					else
					{
						float smallCapsMultiplier = 1f;
						if (this.m_textElementType == TMP_TextElementType.Character)
						{
							if ((this.m_FontStyleInternal & FontStyles.UpperCase) == FontStyles.UpperCase)
							{
								if (char.IsLower((char)charCode))
								{
									charCode = (uint)char.ToUpper((char)charCode);
								}
							}
							else if ((this.m_FontStyleInternal & FontStyles.LowerCase) == FontStyles.LowerCase)
							{
								if (char.IsUpper((char)charCode))
								{
									charCode = (uint)char.ToLower((char)charCode);
								}
							}
							else if ((this.m_FontStyleInternal & FontStyles.SmallCaps) == FontStyles.SmallCaps && char.IsLower((char)charCode))
							{
								smallCapsMultiplier = 0.8f;
								charCode = (uint)char.ToUpper((char)charCode);
							}
						}
						float baselineOffset = 0f;
						float elementAscentLine = 0f;
						float elementDescentLine = 0f;
						if (this.m_textElementType == TMP_TextElementType.Sprite)
						{
							TMP_SpriteCharacter sprite = (TMP_SpriteCharacter)base.textInfo.characterInfo[this.m_characterCount].textElement;
							this.m_currentSpriteAsset = sprite.textAsset as TMP_SpriteAsset;
							this.m_spriteIndex = (int)sprite.glyphIndex;
							if (sprite == null)
							{
								goto IL_40F0;
							}
							if (charCode == 60U)
							{
								charCode = (uint)(57344 + this.m_spriteIndex);
							}
							else
							{
								this.m_spriteColor = TMP_Text.s_colorWhite;
							}
							float fontScale = this.m_currentFontSize / this.m_currentFontAsset.faceInfo.pointSize * this.m_currentFontAsset.faceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
							if (this.m_currentSpriteAsset.m_FaceInfo.pointSize > 0f)
							{
								float spriteScale = this.m_currentFontSize / this.m_currentSpriteAsset.m_FaceInfo.pointSize * this.m_currentSpriteAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
								currentElementScale = sprite.m_Scale * sprite.m_Glyph.scale * spriteScale;
								elementAscentLine = this.m_currentSpriteAsset.m_FaceInfo.ascentLine;
								baselineOffset = this.m_currentSpriteAsset.m_FaceInfo.baseline * fontScale * this.m_fontScaleMultiplier * this.m_currentSpriteAsset.m_FaceInfo.scale;
								elementDescentLine = this.m_currentSpriteAsset.m_FaceInfo.descentLine;
							}
							else
							{
								float spriteScale2 = this.m_currentFontSize / this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
								currentElementScale = this.m_currentFontAsset.m_FaceInfo.ascentLine / sprite.m_Glyph.metrics.height * sprite.m_Scale * sprite.m_Glyph.scale * spriteScale2;
								float scaleDelta = spriteScale2 / currentElementScale;
								elementAscentLine = this.m_currentFontAsset.m_FaceInfo.ascentLine * scaleDelta;
								baselineOffset = this.m_currentFontAsset.m_FaceInfo.baseline * fontScale * this.m_fontScaleMultiplier * this.m_currentFontAsset.m_FaceInfo.scale;
								elementDescentLine = this.m_currentFontAsset.m_FaceInfo.descentLine * scaleDelta;
							}
							this.m_cached_TextElement = sprite;
							this.m_textInfo.characterInfo[this.m_characterCount].elementType = TMP_TextElementType.Sprite;
							this.m_textInfo.characterInfo[this.m_characterCount].scale = currentElementScale;
							this.m_textInfo.characterInfo[this.m_characterCount].fontAsset = this.m_currentFontAsset;
							this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex = this.m_currentMaterialIndex;
							this.m_currentMaterialIndex = previousMaterialIndex;
							padding = 0f;
						}
						else if (this.m_textElementType == TMP_TextElementType.Character)
						{
							this.m_cached_TextElement = this.m_textInfo.characterInfo[this.m_characterCount].textElement;
							if (this.m_cached_TextElement == null)
							{
								goto IL_40F0;
							}
							this.m_currentFontAsset = this.m_textInfo.characterInfo[this.m_characterCount].fontAsset;
							this.m_currentMaterial = this.m_textInfo.characterInfo[this.m_characterCount].material;
							this.m_currentMaterialIndex = this.m_textInfo.characterInfo[this.m_characterCount].materialReferenceIndex;
							float adjustedScale;
							if (isInjectedCharacter && this.m_TextProcessingArray[i].unicode == 10U && this.m_characterCount != this.m_firstCharacterOfLine)
							{
								adjustedScale = this.m_textInfo.characterInfo[this.m_characterCount - 1].pointSize * smallCapsMultiplier / this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
							}
							else
							{
								adjustedScale = this.m_currentFontSize * smallCapsMultiplier / this.m_currentFontAsset.m_FaceInfo.pointSize * this.m_currentFontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f);
							}
							if (isInjectedCharacter && charCode == 8230U)
							{
								elementAscentLine = 0f;
								elementDescentLine = 0f;
							}
							else
							{
								elementAscentLine = this.m_currentFontAsset.m_FaceInfo.ascentLine;
								elementDescentLine = this.m_currentFontAsset.m_FaceInfo.descentLine;
							}
							currentElementScale = adjustedScale * this.m_fontScaleMultiplier * this.m_cached_TextElement.m_Scale * this.m_cached_TextElement.m_Glyph.scale;
							baselineOffset = this.m_currentFontAsset.m_FaceInfo.baseline * adjustedScale * this.m_fontScaleMultiplier * this.m_currentFontAsset.m_FaceInfo.scale;
							this.m_textInfo.characterInfo[this.m_characterCount].elementType = TMP_TextElementType.Character;
							this.m_textInfo.characterInfo[this.m_characterCount].scale = currentElementScale;
							padding = ((this.m_currentMaterialIndex == 0) ? this.m_padding : this.m_subTextObjects[this.m_currentMaterialIndex].padding);
						}
						float currentElementUnmodifiedScale = currentElementScale;
						if (charCode == 173U || charCode == 3U)
						{
							currentElementScale = 0f;
						}
						this.m_textInfo.characterInfo[this.m_characterCount].character = (char)charCode;
						this.m_textInfo.characterInfo[this.m_characterCount].pointSize = this.m_currentFontSize;
						this.m_textInfo.characterInfo[this.m_characterCount].color = this.m_htmlColor;
						this.m_textInfo.characterInfo[this.m_characterCount].underlineColor = this.m_underlineColor;
						this.m_textInfo.characterInfo[this.m_characterCount].strikethroughColor = this.m_strikethroughColor;
						this.m_textInfo.characterInfo[this.m_characterCount].highlightState = this.m_HighlightState;
						this.m_textInfo.characterInfo[this.m_characterCount].style = this.m_FontStyleInternal;
						Glyph altGlyph = this.m_textInfo.characterInfo[this.m_characterCount].alternativeGlyph;
						GlyphMetrics currentGlyphMetrics = ((altGlyph == null) ? this.m_cached_TextElement.m_Glyph.metrics : altGlyph.metrics);
						bool isWhiteSpace = charCode <= 65535U && char.IsWhiteSpace((char)charCode);
						GlyphValueRecord glyphAdjustments = default(GlyphValueRecord);
						float characterSpacingAdjustment = this.m_characterSpacing;
						if (kerning && this.m_textElementType == TMP_TextElementType.Character)
						{
							uint baseGlyphIndex = this.m_cached_TextElement.m_GlyphIndex;
							if (this.m_characterCount < totalCharacterCount - 1 && this.m_textInfo.characterInfo[this.m_characterCount + 1].elementType == TMP_TextElementType.Character)
							{
								uint key = (this.m_textInfo.characterInfo[this.m_characterCount + 1].textElement.m_GlyphIndex << 16) | baseGlyphIndex;
								GlyphPairAdjustmentRecord adjustmentPair;
								if (this.m_currentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key, out adjustmentPair))
								{
									glyphAdjustments = adjustmentPair.firstAdjustmentRecord.glyphValueRecord;
									characterSpacingAdjustment = (((adjustmentPair.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : characterSpacingAdjustment);
								}
							}
							if (this.m_characterCount >= 1)
							{
								uint previousGlyphIndex = this.m_textInfo.characterInfo[this.m_characterCount - 1].textElement.m_GlyphIndex;
								uint key2 = (baseGlyphIndex << 16) | previousGlyphIndex;
								GlyphPairAdjustmentRecord adjustmentPair;
								if (base.textInfo.characterInfo[this.m_characterCount - 1].elementType == TMP_TextElementType.Character && this.m_currentFontAsset.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.TryGetValue(key2, out adjustmentPair))
								{
									glyphAdjustments += adjustmentPair.secondAdjustmentRecord.glyphValueRecord;
									characterSpacingAdjustment = (((adjustmentPair.featureLookupFlags & FontFeatureLookupFlags.IgnoreSpacingAdjustments) == FontFeatureLookupFlags.IgnoreSpacingAdjustments) ? 0f : characterSpacingAdjustment);
								}
							}
						}
						this.m_textInfo.characterInfo[this.m_characterCount].adjustedHorizontalAdvance = glyphAdjustments.xAdvance;
						bool isBaseGlyph = TMP_TextParsingUtilities.IsBaseGlyph(charCode);
						if (isBaseGlyph)
						{
							this.m_LastBaseGlyphIndex = this.m_characterCount;
						}
						if (this.m_characterCount > 0 && !isBaseGlyph)
						{
							if (markToBase && this.m_LastBaseGlyphIndex != -2147483648 && this.m_LastBaseGlyphIndex == this.m_characterCount - 1)
							{
								uint baseGlyphIndex2 = this.m_textInfo.characterInfo[this.m_LastBaseGlyphIndex].textElement.glyph.index;
								uint key3 = (this.m_cached_TextElement.glyphIndex << 16) | baseGlyphIndex2;
								MarkToBaseAdjustmentRecord glyphAdjustmentRecord;
								if (this.m_currentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(key3, out glyphAdjustmentRecord))
								{
									float advanceOffset = (this.m_textInfo.characterInfo[this.m_LastBaseGlyphIndex].origin - this.m_xAdvance) / currentElementScale;
									glyphAdjustments.xPlacement = advanceOffset + glyphAdjustmentRecord.baseGlyphAnchorPoint.xCoordinate - glyphAdjustmentRecord.markPositionAdjustment.xPositionAdjustment;
									glyphAdjustments.yPlacement = glyphAdjustmentRecord.baseGlyphAnchorPoint.yCoordinate - glyphAdjustmentRecord.markPositionAdjustment.yPositionAdjustment;
									characterSpacingAdjustment = 0f;
								}
							}
							else
							{
								bool wasLookupApplied = false;
								if (markToMark)
								{
									int characterLookupIndex = this.m_characterCount - 1;
									while (characterLookupIndex >= 0 && characterLookupIndex != this.m_LastBaseGlyphIndex)
									{
										uint baseGlyphIndex3 = this.m_textInfo.characterInfo[characterLookupIndex].textElement.glyph.index;
										uint key4 = (this.m_cached_TextElement.glyphIndex << 16) | baseGlyphIndex3;
										MarkToMarkAdjustmentRecord glyphAdjustmentRecord2;
										if (this.m_currentFontAsset.fontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.TryGetValue(key4, out glyphAdjustmentRecord2))
										{
											float baseMarkOrigin = (this.m_textInfo.characterInfo[characterLookupIndex].origin - this.m_xAdvance) / currentElementScale;
											float currentBaseline = baselineOffset - this.m_lineOffset + this.m_baselineOffset;
											float baseMarkBaseline = (this.m_textInfo.characterInfo[characterLookupIndex].baseLine - currentBaseline) / currentElementScale;
											glyphAdjustments.xPlacement = baseMarkOrigin + glyphAdjustmentRecord2.baseMarkGlyphAnchorPoint.xCoordinate - glyphAdjustmentRecord2.combiningMarkPositionAdjustment.xPositionAdjustment;
											glyphAdjustments.yPlacement = baseMarkBaseline + glyphAdjustmentRecord2.baseMarkGlyphAnchorPoint.yCoordinate - glyphAdjustmentRecord2.combiningMarkPositionAdjustment.yPositionAdjustment;
											characterSpacingAdjustment = 0f;
											wasLookupApplied = true;
											break;
										}
										characterLookupIndex--;
									}
								}
								if (markToBase && this.m_LastBaseGlyphIndex != -2147483648 && !wasLookupApplied)
								{
									uint baseGlyphIndex4 = this.m_textInfo.characterInfo[this.m_LastBaseGlyphIndex].textElement.glyph.index;
									uint key5 = (this.m_cached_TextElement.glyphIndex << 16) | baseGlyphIndex4;
									MarkToBaseAdjustmentRecord glyphAdjustmentRecord3;
									if (this.m_currentFontAsset.fontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.TryGetValue(key5, out glyphAdjustmentRecord3))
									{
										float advanceOffset2 = (this.m_textInfo.characterInfo[this.m_LastBaseGlyphIndex].origin - this.m_xAdvance) / currentElementScale;
										glyphAdjustments.xPlacement = advanceOffset2 + glyphAdjustmentRecord3.baseGlyphAnchorPoint.xCoordinate - glyphAdjustmentRecord3.markPositionAdjustment.xPositionAdjustment;
										glyphAdjustments.yPlacement = glyphAdjustmentRecord3.baseGlyphAnchorPoint.yCoordinate - glyphAdjustmentRecord3.markPositionAdjustment.yPositionAdjustment;
										characterSpacingAdjustment = 0f;
									}
								}
							}
						}
						elementAscentLine += glyphAdjustments.yPlacement;
						elementDescentLine += glyphAdjustments.yPlacement;
						if (this.m_isRightToLeft)
						{
							this.m_xAdvance -= currentGlyphMetrics.horizontalAdvance * (1f - this.m_charWidthAdjDelta) * currentElementScale;
							if (isWhiteSpace || charCode == 8203U)
							{
								this.m_xAdvance -= this.m_wordSpacing * currentEmScale;
							}
						}
						float monoAdvance = 0f;
						if (this.m_monoSpacing != 0f)
						{
							if (this.m_duoSpace && (charCode == 46U || charCode == 58U || charCode == 44U))
							{
								monoAdvance = (this.m_monoSpacing / 4f - (currentGlyphMetrics.width / 2f + currentGlyphMetrics.horizontalBearingX) * currentElementScale) * (1f - this.m_charWidthAdjDelta);
							}
							else
							{
								monoAdvance = (this.m_monoSpacing / 2f - (currentGlyphMetrics.width / 2f + currentGlyphMetrics.horizontalBearingX) * currentElementScale) * (1f - this.m_charWidthAdjDelta);
							}
							this.m_xAdvance += monoAdvance;
						}
						float style_padding;
						float boldSpacingAdjustment;
						if (this.m_textElementType == TMP_TextElementType.Character && !isUsingAltTypeface && (this.m_FontStyleInternal & FontStyles.Bold) == FontStyles.Bold)
						{
							if (this.m_currentMaterial != null && this.m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale))
							{
								float gradientScale = this.m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
								style_padding = this.m_currentFontAsset.boldStyle / 4f * gradientScale * this.m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
								if (style_padding + padding > gradientScale)
								{
									padding = gradientScale - style_padding;
								}
							}
							else
							{
								style_padding = 0f;
							}
							boldSpacingAdjustment = this.m_currentFontAsset.boldSpacing;
						}
						else
						{
							if (this.m_currentMaterial != null && this.m_currentMaterial.HasProperty(ShaderUtilities.ID_GradientScale) && this.m_currentMaterial.HasProperty(ShaderUtilities.ID_ScaleRatio_A))
							{
								float gradientScale2 = this.m_currentMaterial.GetFloat(ShaderUtilities.ID_GradientScale);
								style_padding = this.m_currentFontAsset.normalStyle / 4f * gradientScale2 * this.m_currentMaterial.GetFloat(ShaderUtilities.ID_ScaleRatio_A);
								if (style_padding + padding > gradientScale2)
								{
									padding = gradientScale2 - style_padding;
								}
							}
							else
							{
								style_padding = 0f;
							}
							boldSpacingAdjustment = 0f;
						}
						Vector3 top_left;
						top_left.x = this.m_xAdvance + (currentGlyphMetrics.horizontalBearingX * this.m_FXScale.x - padding - style_padding + glyphAdjustments.xPlacement) * currentElementScale * (1f - this.m_charWidthAdjDelta);
						top_left.y = baselineOffset + (currentGlyphMetrics.horizontalBearingY + padding + glyphAdjustments.yPlacement) * currentElementScale - this.m_lineOffset + this.m_baselineOffset;
						top_left.z = 0f;
						Vector3 bottom_left;
						bottom_left.x = top_left.x;
						bottom_left.y = top_left.y - (currentGlyphMetrics.height + padding * 2f) * currentElementScale;
						bottom_left.z = 0f;
						Vector3 top_right;
						top_right.x = bottom_left.x + (currentGlyphMetrics.width * this.m_FXScale.x + padding * 2f + style_padding * 2f) * currentElementScale * (1f - this.m_charWidthAdjDelta);
						top_right.y = top_left.y;
						top_right.z = 0f;
						Vector3 bottom_right;
						bottom_right.x = top_right.x;
						bottom_right.y = bottom_left.y;
						bottom_right.z = 0f;
						if (this.m_textElementType == TMP_TextElementType.Character && !isUsingAltTypeface && (this.m_FontStyleInternal & FontStyles.Italic) == FontStyles.Italic)
						{
							float shear_value = (float)this.m_ItalicAngle * 0.01f;
							float midPoint = (this.m_currentFontAsset.m_FaceInfo.capLine - (this.m_currentFontAsset.m_FaceInfo.baseline + this.m_baselineOffset)) / 2f * this.m_fontScaleMultiplier * this.m_currentFontAsset.m_FaceInfo.scale;
							Vector3 topShear = new Vector3(shear_value * ((currentGlyphMetrics.horizontalBearingY + padding + style_padding - midPoint) * currentElementScale), 0f, 0f);
							Vector3 bottomShear = new Vector3(shear_value * ((currentGlyphMetrics.horizontalBearingY - currentGlyphMetrics.height - padding - style_padding - midPoint) * currentElementScale), 0f, 0f);
							top_left += topShear;
							bottom_left += bottomShear;
							top_right += topShear;
							bottom_right += bottomShear;
						}
						if (this.m_FXRotation != Quaternion.identity)
						{
							Matrix4x4 rotationMatrix = Matrix4x4.Rotate(this.m_FXRotation);
							Vector3 positionOffset = (top_right + bottom_left) / 2f;
							top_left = rotationMatrix.MultiplyPoint3x4(top_left - positionOffset) + positionOffset;
							bottom_left = rotationMatrix.MultiplyPoint3x4(bottom_left - positionOffset) + positionOffset;
							top_right = rotationMatrix.MultiplyPoint3x4(top_right - positionOffset) + positionOffset;
							bottom_right = rotationMatrix.MultiplyPoint3x4(bottom_right - positionOffset) + positionOffset;
						}
						this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft = bottom_left;
						this.m_textInfo.characterInfo[this.m_characterCount].topLeft = top_left;
						this.m_textInfo.characterInfo[this.m_characterCount].topRight = top_right;
						this.m_textInfo.characterInfo[this.m_characterCount].bottomRight = bottom_right;
						this.m_textInfo.characterInfo[this.m_characterCount].origin = this.m_xAdvance + glyphAdjustments.xPlacement * currentElementScale;
						this.m_textInfo.characterInfo[this.m_characterCount].baseLine = baselineOffset - this.m_lineOffset + this.m_baselineOffset + glyphAdjustments.yPlacement * currentElementScale;
						this.m_textInfo.characterInfo[this.m_characterCount].aspectRatio = (top_right.x - bottom_left.x) / (top_left.y - bottom_left.y);
						float elementAscender = ((this.m_textElementType == TMP_TextElementType.Character) ? (elementAscentLine * currentElementScale / smallCapsMultiplier + this.m_baselineOffset) : (elementAscentLine * currentElementScale + this.m_baselineOffset));
						float elementDescender = ((this.m_textElementType == TMP_TextElementType.Character) ? (elementDescentLine * currentElementScale / smallCapsMultiplier + this.m_baselineOffset) : (elementDescentLine * currentElementScale + this.m_baselineOffset));
						float adjustedAscender = elementAscender;
						float adjustedDescender = elementDescender;
						bool isFirstCharacterOfLine = this.m_characterCount == this.m_firstCharacterOfLine;
						if (isFirstCharacterOfLine || !isWhiteSpace)
						{
							if (this.m_baselineOffset != 0f)
							{
								adjustedAscender = Mathf.Max((elementAscender - this.m_baselineOffset) / this.m_fontScaleMultiplier, adjustedAscender);
								adjustedDescender = Mathf.Min((elementDescender - this.m_baselineOffset) / this.m_fontScaleMultiplier, adjustedDescender);
							}
							this.m_maxLineAscender = Mathf.Max(adjustedAscender, this.m_maxLineAscender);
							this.m_maxLineDescender = Mathf.Min(adjustedDescender, this.m_maxLineDescender);
						}
						if (isFirstCharacterOfLine || !isWhiteSpace)
						{
							this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender = adjustedAscender;
							this.m_textInfo.characterInfo[this.m_characterCount].adjustedDescender = adjustedDescender;
							this.m_ElementAscender = (this.m_textInfo.characterInfo[this.m_characterCount].ascender = elementAscender - this.m_lineOffset);
							this.m_ElementDescender = (this.m_textInfo.characterInfo[this.m_characterCount].descender = elementDescender - this.m_lineOffset);
						}
						else
						{
							this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender = this.m_maxLineAscender;
							this.m_textInfo.characterInfo[this.m_characterCount].adjustedDescender = this.m_maxLineDescender;
							this.m_ElementAscender = (this.m_textInfo.characterInfo[this.m_characterCount].ascender = this.m_maxLineAscender - this.m_lineOffset);
							this.m_ElementDescender = (this.m_textInfo.characterInfo[this.m_characterCount].descender = this.m_maxLineDescender - this.m_lineOffset);
						}
						if ((this.m_lineNumber == 0 || this.m_isNewPage) && (isFirstCharacterOfLine || !isWhiteSpace))
						{
							this.m_maxTextAscender = this.m_maxLineAscender;
							this.m_maxCapHeight = Mathf.Max(this.m_maxCapHeight, this.m_currentFontAsset.m_FaceInfo.capLine * currentElementScale / smallCapsMultiplier);
						}
						if (this.m_lineOffset == 0f && (isFirstCharacterOfLine || !isWhiteSpace))
						{
							this.m_PageAscender = ((this.m_PageAscender > elementAscender) ? this.m_PageAscender : elementAscender);
						}
						this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
						bool isJustifiedOrFlush = (this.m_lineJustification & HorizontalAlignmentOptions.Flush) == HorizontalAlignmentOptions.Flush || (this.m_lineJustification & HorizontalAlignmentOptions.Justified) == HorizontalAlignmentOptions.Justified;
						if (charCode == 9U || ((this.m_TextWrappingMode == TextWrappingModes.PreserveWhitespace || this.m_TextWrappingMode == TextWrappingModes.PreserveWhitespaceNoWrap) && (isWhiteSpace || charCode == 8203U)) || (!isWhiteSpace && charCode != 8203U && charCode != 173U && charCode != 3U) || (charCode == 173U && !isSoftHyphenIgnored) || this.m_textElementType == TMP_TextElementType.Sprite)
						{
							this.m_textInfo.characterInfo[this.m_characterCount].isVisible = true;
							float marginLeft = this.m_marginLeft;
							float marginRight = this.m_marginRight;
							if (isInjectedCharacter)
							{
								marginLeft = this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft;
								marginRight = this.m_textInfo.lineInfo[this.m_lineNumber].marginRight;
							}
							widthOfTextArea = ((this.m_width != -1f) ? Mathf.Min(marginWidth + 0.0001f - marginLeft - marginRight, this.m_width) : (marginWidth + 0.0001f - marginLeft - marginRight));
							float textWidth = Mathf.Abs(this.m_xAdvance) + ((!this.m_isRightToLeft) ? currentGlyphMetrics.horizontalAdvance : 0f) * (1f - this.m_charWidthAdjDelta) * ((charCode == 173U) ? currentElementUnmodifiedScale : currentElementScale);
							float textHeight = this.m_maxTextAscender - (this.m_maxLineDescender - this.m_lineOffset) + ((this.m_lineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f);
							int testedCharacterCount = this.m_characterCount;
							if (textHeight > marginHeight + 0.0001f)
							{
								if (this.m_firstOverflowCharacterIndex == -1)
								{
									this.m_firstOverflowCharacterIndex = this.m_characterCount;
								}
								if (this.m_enableAutoSizing)
								{
									if (this.m_lineSpacingDelta > this.m_lineSpacingMax && this.m_lineOffset > 0f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
									{
										float adjustmentDelta = (marginHeight - textHeight) / (float)this.m_lineNumber;
										this.m_lineSpacingDelta = Mathf.Max(this.m_lineSpacingDelta + adjustmentDelta / baseScale, this.m_lineSpacingMax);
										return;
									}
									if (this.m_fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
									{
										this.m_maxFontSize = this.m_fontSize;
										float sizeDelta = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
										this.m_fontSize -= sizeDelta;
										this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
										return;
									}
								}
								switch (this.m_overflowMode)
								{
								case TextOverflowModes.Ellipsis:
								{
									if (TMP_Text.m_EllipsisInsertionCandidateStack.Count == 0)
									{
										i = -1;
										this.m_characterCount = 0;
										characterToSubstitute.index = 0;
										characterToSubstitute.unicode = 3U;
										this.m_firstCharacterOfLine = 0;
										goto IL_40F0;
									}
									WordWrapState ellipsisState = TMP_Text.m_EllipsisInsertionCandidateStack.Pop();
									i = base.RestoreWordWrappingState(ref ellipsisState);
									i--;
									this.m_characterCount--;
									characterToSubstitute.index = this.m_characterCount;
									characterToSubstitute.unicode = 8230U;
									restoreCount++;
									goto IL_40F0;
								}
								case TextOverflowModes.Truncate:
									i = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLastValidState);
									characterToSubstitute.index = testedCharacterCount;
									characterToSubstitute.unicode = 3U;
									goto IL_40F0;
								case TextOverflowModes.Page:
									if (i < 0 || testedCharacterCount == 0)
									{
										i = -1;
										this.m_characterCount = 0;
										characterToSubstitute.index = 0;
										characterToSubstitute.unicode = 3U;
										goto IL_40F0;
									}
									if (this.m_maxLineAscender - this.m_maxLineDescender > marginHeight + 0.0001f)
									{
										i = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLineState);
										characterToSubstitute.index = testedCharacterCount;
										characterToSubstitute.unicode = 3U;
										goto IL_40F0;
									}
									i = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLineState);
									this.m_isNewPage = true;
									this.m_firstCharacterOfLine = this.m_characterCount;
									this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
									this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
									this.m_startOfLineAscender = 0f;
									this.m_xAdvance = 0f + this.tag_Indent;
									this.m_lineOffset = 0f;
									this.m_maxTextAscender = 0f;
									this.m_PageAscender = 0f;
									this.m_lineNumber++;
									this.m_pageNumber++;
									goto IL_40F0;
								case TextOverflowModes.Linked:
									i = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLastValidState);
									if (this.m_linkedTextComponent != null)
									{
										this.m_linkedTextComponent.text = this.text;
										this.m_linkedTextComponent.m_inputSource = this.m_inputSource;
										this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
										this.m_linkedTextComponent.ForceMeshUpdate(false, false);
										this.m_isTextTruncated = true;
									}
									characterToSubstitute.index = testedCharacterCount;
									characterToSubstitute.unicode = 3U;
									goto IL_40F0;
								}
							}
							if (isBaseGlyph && textWidth > widthOfTextArea * (isJustifiedOrFlush ? 1.05f : 1f))
							{
								if (this.m_TextWrappingMode != TextWrappingModes.NoWrap && this.m_TextWrappingMode != TextWrappingModes.PreserveWhitespaceNoWrap && this.m_characterCount != this.m_firstCharacterOfLine)
								{
									i = base.RestoreWordWrappingState(ref TMP_Text.m_SavedWordWrapState);
									float lineOffsetDelta;
									if (this.m_lineHeight == -32767f)
									{
										float ascender = this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender;
										lineOffsetDelta = ((this.m_lineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f) - this.m_maxLineDescender + ascender + (lineGap + this.m_lineSpacingDelta) * baseScale + this.m_lineSpacing * currentEmScale;
									}
									else
									{
										lineOffsetDelta = this.m_lineHeight + this.m_lineSpacing * currentEmScale;
										this.m_IsDrivenLineSpacing = true;
									}
									float newTextHeight = this.m_maxTextAscender + lineOffsetDelta + this.m_lineOffset - this.m_textInfo.characterInfo[this.m_characterCount].adjustedDescender;
									if (this.m_textInfo.characterInfo[this.m_characterCount - 1].character == '\u00ad' && !isSoftHyphenIgnored && (this.m_overflowMode == TextOverflowModes.Overflow || newTextHeight < marginHeight + 0.0001f))
									{
										characterToSubstitute.index = this.m_characterCount - 1;
										characterToSubstitute.unicode = 45U;
										i--;
										this.m_characterCount--;
										goto IL_40F0;
									}
									isSoftHyphenIgnored = false;
									if (this.m_textInfo.characterInfo[this.m_characterCount].character == '\u00ad')
									{
										isSoftHyphenIgnored = true;
										goto IL_40F0;
									}
									if (this.m_enableAutoSizing && isFirstWordOfLine)
									{
										if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
										{
											float adjustedTextWidth = textWidth;
											if (this.m_charWidthAdjDelta > 0f)
											{
												adjustedTextWidth /= 1f - this.m_charWidthAdjDelta;
											}
											float adjustmentDelta2 = textWidth - (widthOfTextArea - 0.0001f) * (isJustifiedOrFlush ? 1.05f : 1f);
											this.m_charWidthAdjDelta += adjustmentDelta2 / adjustedTextWidth;
											this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
											return;
										}
										if (this.m_fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
										{
											this.m_maxFontSize = this.m_fontSize;
											float sizeDelta2 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
											this.m_fontSize -= sizeDelta2;
											this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
											return;
										}
									}
									int savedSoftLineBreakingSpace = TMP_Text.m_SavedSoftLineBreakState.previous_WordBreak;
									if (isFirstWordOfLine && savedSoftLineBreakingSpace != -1 && savedSoftLineBreakingSpace != lastSoftLineBreak)
									{
										i = base.RestoreWordWrappingState(ref TMP_Text.m_SavedSoftLineBreakState);
										lastSoftLineBreak = savedSoftLineBreakingSpace;
										if (this.m_textInfo.characterInfo[this.m_characterCount - 1].character == '\u00ad')
										{
											characterToSubstitute.index = this.m_characterCount - 1;
											characterToSubstitute.unicode = 45U;
											i--;
											this.m_characterCount--;
											goto IL_40F0;
										}
									}
									if (newTextHeight <= marginHeight + 0.0001f)
									{
										base.InsertNewLine(i, baseScale, currentElementScale, currentEmScale, boldSpacingAdjustment, characterSpacingAdjustment, widthOfTextArea, lineGap, ref isMaxVisibleDescenderSet, ref maxVisibleDescender);
										isStartOfNewLine = true;
										isFirstWordOfLine = true;
										goto IL_40F0;
									}
									if (this.m_firstOverflowCharacterIndex == -1)
									{
										this.m_firstOverflowCharacterIndex = this.m_characterCount;
									}
									if (this.m_enableAutoSizing)
									{
										if (this.m_lineSpacingDelta > this.m_lineSpacingMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
										{
											float adjustmentDelta3 = (marginHeight - newTextHeight) / (float)(this.m_lineNumber + 1);
											this.m_lineSpacingDelta = Mathf.Max(this.m_lineSpacingDelta + adjustmentDelta3 / baseScale, this.m_lineSpacingMax);
											return;
										}
										if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
										{
											float adjustedTextWidth2 = textWidth;
											if (this.m_charWidthAdjDelta > 0f)
											{
												adjustedTextWidth2 /= 1f - this.m_charWidthAdjDelta;
											}
											float adjustmentDelta4 = textWidth - (widthOfTextArea - 0.0001f) * (isJustifiedOrFlush ? 1.05f : 1f);
											this.m_charWidthAdjDelta += adjustmentDelta4 / adjustedTextWidth2;
											this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
											return;
										}
										if (this.m_fontSize > this.m_fontSizeMin && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
										{
											this.m_maxFontSize = this.m_fontSize;
											float sizeDelta3 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
											this.m_fontSize -= sizeDelta3;
											this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
											return;
										}
									}
									switch (this.m_overflowMode)
									{
									case TextOverflowModes.Overflow:
									case TextOverflowModes.Masking:
									case TextOverflowModes.ScrollRect:
										base.InsertNewLine(i, baseScale, currentElementScale, currentEmScale, boldSpacingAdjustment, characterSpacingAdjustment, widthOfTextArea, lineGap, ref isMaxVisibleDescenderSet, ref maxVisibleDescender);
										isStartOfNewLine = true;
										isFirstWordOfLine = true;
										goto IL_40F0;
									case TextOverflowModes.Ellipsis:
									{
										if (TMP_Text.m_EllipsisInsertionCandidateStack.Count == 0)
										{
											i = -1;
											this.m_characterCount = 0;
											characterToSubstitute.index = 0;
											characterToSubstitute.unicode = 3U;
											this.m_firstCharacterOfLine = 0;
											goto IL_40F0;
										}
										WordWrapState ellipsisState2 = TMP_Text.m_EllipsisInsertionCandidateStack.Pop();
										i = base.RestoreWordWrappingState(ref ellipsisState2);
										i--;
										this.m_characterCount--;
										characterToSubstitute.index = this.m_characterCount;
										characterToSubstitute.unicode = 8230U;
										restoreCount++;
										goto IL_40F0;
									}
									case TextOverflowModes.Truncate:
										i = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLastValidState);
										characterToSubstitute.index = testedCharacterCount;
										characterToSubstitute.unicode = 3U;
										goto IL_40F0;
									case TextOverflowModes.Page:
										this.m_isNewPage = true;
										base.InsertNewLine(i, baseScale, currentElementScale, currentEmScale, boldSpacingAdjustment, characterSpacingAdjustment, widthOfTextArea, lineGap, ref isMaxVisibleDescenderSet, ref maxVisibleDescender);
										this.m_startOfLineAscender = 0f;
										this.m_lineOffset = 0f;
										this.m_maxTextAscender = 0f;
										this.m_PageAscender = 0f;
										this.m_pageNumber++;
										isStartOfNewLine = true;
										isFirstWordOfLine = true;
										goto IL_40F0;
									case TextOverflowModes.Linked:
										if (this.m_linkedTextComponent != null)
										{
											this.m_linkedTextComponent.text = this.text;
											this.m_linkedTextComponent.m_inputSource = this.m_inputSource;
											this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
											this.m_linkedTextComponent.ForceMeshUpdate(false, false);
											this.m_isTextTruncated = true;
										}
										characterToSubstitute.index = this.m_characterCount;
										characterToSubstitute.unicode = 3U;
										goto IL_40F0;
									}
								}
								else
								{
									if (this.m_enableAutoSizing && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
									{
										if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f)
										{
											float adjustedTextWidth3 = textWidth;
											if (this.m_charWidthAdjDelta > 0f)
											{
												adjustedTextWidth3 /= 1f - this.m_charWidthAdjDelta;
											}
											float adjustmentDelta5 = textWidth - (widthOfTextArea - 0.0001f) * (isJustifiedOrFlush ? 1.05f : 1f);
											this.m_charWidthAdjDelta += adjustmentDelta5 / adjustedTextWidth3;
											this.m_charWidthAdjDelta = Mathf.Min(this.m_charWidthAdjDelta, this.m_charWidthMaxAdj / 100f);
											return;
										}
										if (this.m_fontSize > this.m_fontSizeMin)
										{
											this.m_maxFontSize = this.m_fontSize;
											float sizeDelta4 = Mathf.Max((this.m_fontSize - this.m_minFontSize) / 2f, 0.05f);
											this.m_fontSize -= sizeDelta4;
											this.m_fontSize = Mathf.Max((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMin);
											return;
										}
									}
									switch (this.m_overflowMode)
									{
									case TextOverflowModes.Ellipsis:
									{
										if (TMP_Text.m_EllipsisInsertionCandidateStack.Count == 0)
										{
											i = -1;
											this.m_characterCount = 0;
											characterToSubstitute.index = 0;
											characterToSubstitute.unicode = 3U;
											this.m_firstCharacterOfLine = 0;
											goto IL_40F0;
										}
										WordWrapState ellipsisState3 = TMP_Text.m_EllipsisInsertionCandidateStack.Pop();
										i = base.RestoreWordWrappingState(ref ellipsisState3);
										i--;
										this.m_characterCount--;
										characterToSubstitute.index = this.m_characterCount;
										characterToSubstitute.unicode = 8230U;
										restoreCount++;
										goto IL_40F0;
									}
									case TextOverflowModes.Truncate:
										i = base.RestoreWordWrappingState(ref TMP_Text.m_SavedWordWrapState);
										characterToSubstitute.index = testedCharacterCount;
										characterToSubstitute.unicode = 3U;
										goto IL_40F0;
									case TextOverflowModes.Linked:
										i = base.RestoreWordWrappingState(ref TMP_Text.m_SavedWordWrapState);
										if (this.m_linkedTextComponent != null)
										{
											this.m_linkedTextComponent.text = this.text;
											this.m_linkedTextComponent.m_inputSource = this.m_inputSource;
											this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
											this.m_linkedTextComponent.ForceMeshUpdate(false, false);
											this.m_isTextTruncated = true;
										}
										characterToSubstitute.index = this.m_characterCount;
										characterToSubstitute.unicode = 3U;
										goto IL_40F0;
									}
								}
							}
							if (isWhiteSpace)
							{
								this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
								this.m_lastVisibleCharacterOfLine = this.m_characterCount;
								TMP_LineInfo[] lineInfo2 = this.m_textInfo.lineInfo;
								int lineNumber = this.m_lineNumber;
								this.m_lineVisibleSpaceCount = (lineInfo2[lineNumber].spaceCount = lineInfo2[lineNumber].spaceCount + 1);
								this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft = marginLeft;
								this.m_textInfo.lineInfo[this.m_lineNumber].marginRight = marginRight;
								this.m_textInfo.spaceCount++;
								if (charCode == 160U)
								{
									TMP_LineInfo[] lineInfo3 = this.m_textInfo.lineInfo;
									int lineNumber2 = this.m_lineNumber;
									lineInfo3[lineNumber2].controlCharacterCount = lineInfo3[lineNumber2].controlCharacterCount + 1;
								}
							}
							else if (charCode == 173U)
							{
								this.m_textInfo.characterInfo[this.m_characterCount].isVisible = false;
							}
							else
							{
								Color32 vertexColor;
								if (this.m_overrideHtmlColors)
								{
									vertexColor = this.m_fontColor32;
								}
								else
								{
									vertexColor = this.m_htmlColor;
								}
								if (this.m_textElementType == TMP_TextElementType.Character)
								{
									this.SaveGlyphVertexInfo(padding, style_padding, vertexColor);
								}
								else if (this.m_textElementType == TMP_TextElementType.Sprite)
								{
									this.SaveSpriteVertexInfo(vertexColor);
								}
								if (isStartOfNewLine)
								{
									isStartOfNewLine = false;
									this.m_firstVisibleCharacterOfLine = this.m_characterCount;
								}
								this.m_lineVisibleCharacterCount++;
								this.m_lastVisibleCharacterOfLine = this.m_characterCount;
								this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft = marginLeft;
								this.m_textInfo.lineInfo[this.m_lineNumber].marginRight = marginRight;
							}
						}
						else
						{
							if (this.m_overflowMode == TextOverflowModes.Linked && (charCode == 10U || charCode == 11U))
							{
								float num = this.m_maxTextAscender - (this.m_maxLineDescender - this.m_lineOffset) + ((this.m_lineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f);
								int testedCharacterCount2 = this.m_characterCount;
								if (num > marginHeight + 0.0001f)
								{
									if (this.m_firstOverflowCharacterIndex == -1)
									{
										this.m_firstOverflowCharacterIndex = this.m_characterCount;
									}
									i = base.RestoreWordWrappingState(ref TMP_Text.m_SavedLastValidState);
									if (this.m_linkedTextComponent != null)
									{
										this.m_linkedTextComponent.text = this.text;
										this.m_linkedTextComponent.m_inputSource = this.m_inputSource;
										this.m_linkedTextComponent.firstVisibleCharacter = this.m_characterCount;
										this.m_linkedTextComponent.ForceMeshUpdate(false, false);
										this.m_isTextTruncated = true;
									}
									characterToSubstitute.index = testedCharacterCount2;
									characterToSubstitute.unicode = 3U;
									goto IL_40F0;
								}
							}
							if ((charCode == 10U || charCode == 11U || charCode == 160U || charCode == 8199U || charCode == 8232U || charCode == 8233U || char.IsSeparator((char)charCode)) && charCode != 173U && charCode != 8203U && charCode != 8288U)
							{
								TMP_LineInfo[] lineInfo4 = this.m_textInfo.lineInfo;
								int lineNumber3 = this.m_lineNumber;
								lineInfo4[lineNumber3].spaceCount = lineInfo4[lineNumber3].spaceCount + 1;
								this.m_textInfo.spaceCount++;
							}
							if (charCode == 160U)
							{
								TMP_LineInfo[] lineInfo5 = this.m_textInfo.lineInfo;
								int lineNumber4 = this.m_lineNumber;
								lineInfo5[lineNumber4].controlCharacterCount = lineInfo5[lineNumber4].controlCharacterCount + 1;
							}
						}
						if (this.m_overflowMode == TextOverflowModes.Ellipsis && (!isInjectedCharacter || charCode == 45U))
						{
							float scale = this.m_currentFontSize / this.m_Ellipsis.fontAsset.m_FaceInfo.pointSize * this.m_Ellipsis.fontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f) * this.m_fontScaleMultiplier * this.m_Ellipsis.character.m_Scale * this.m_Ellipsis.character.m_Glyph.scale;
							float marginLeft2 = this.m_marginLeft;
							float marginRight2 = this.m_marginRight;
							if (charCode == 10U && this.m_characterCount != this.m_firstCharacterOfLine)
							{
								scale = this.m_textInfo.characterInfo[this.m_characterCount - 1].pointSize / this.m_Ellipsis.fontAsset.m_FaceInfo.pointSize * this.m_Ellipsis.fontAsset.m_FaceInfo.scale * (this.m_isOrthographic ? 1f : 0.1f) * this.m_fontScaleMultiplier * this.m_Ellipsis.character.m_Scale * this.m_Ellipsis.character.m_Glyph.scale;
								marginLeft2 = this.m_textInfo.lineInfo[this.m_lineNumber].marginLeft;
								marginRight2 = this.m_textInfo.lineInfo[this.m_lineNumber].marginRight;
							}
							float textHeight2 = this.m_maxTextAscender - (this.m_maxLineDescender - this.m_lineOffset) + ((this.m_lineOffset > 0f && !this.m_IsDrivenLineSpacing) ? (this.m_maxLineAscender - this.m_startOfLineAscender) : 0f);
							float num2 = Mathf.Abs(this.m_xAdvance) + ((!this.m_isRightToLeft) ? this.m_Ellipsis.character.m_Glyph.metrics.horizontalAdvance : 0f) * (1f - this.m_charWidthAdjDelta) * scale;
							float widthOfTextAreaForEllipsis = ((this.m_width != -1f) ? Mathf.Min(marginWidth + 0.0001f - marginLeft2 - marginRight2, this.m_width) : (marginWidth + 0.0001f - marginLeft2 - marginRight2));
							if (num2 < widthOfTextAreaForEllipsis * (isJustifiedOrFlush ? 1.05f : 1f) && textHeight2 < marginHeight + 0.0001f)
							{
								base.SaveWordWrappingState(ref TMP_Text.m_SavedEllipsisState, i, this.m_characterCount);
								TMP_Text.m_EllipsisInsertionCandidateStack.Push(TMP_Text.m_SavedEllipsisState);
							}
						}
						this.m_textInfo.characterInfo[this.m_characterCount].lineNumber = this.m_lineNumber;
						this.m_textInfo.characterInfo[this.m_characterCount].pageNumber = this.m_pageNumber;
						if ((charCode != 10U && charCode != 11U && charCode != 13U && !isInjectedCharacter) || this.m_textInfo.lineInfo[this.m_lineNumber].characterCount == 1)
						{
							this.m_textInfo.lineInfo[this.m_lineNumber].alignment = this.m_lineJustification;
						}
						if (charCode == 9U)
						{
							float tabSize = this.m_currentFontAsset.m_FaceInfo.tabWidth * (float)this.m_currentFontAsset.tabSize * currentElementScale;
							if (this.m_isRightToLeft)
							{
								float tabs = Mathf.Floor(this.m_xAdvance / tabSize) * tabSize;
								this.m_xAdvance = ((tabs < this.m_xAdvance) ? tabs : (this.m_xAdvance - tabSize));
							}
							else
							{
								float tabs2 = Mathf.Ceil(this.m_xAdvance / tabSize) * tabSize;
								this.m_xAdvance = ((tabs2 > this.m_xAdvance) ? tabs2 : (this.m_xAdvance + tabSize));
							}
						}
						else if (this.m_monoSpacing != 0f)
						{
							float monoAdjustment;
							if (this.m_duoSpace && (charCode == 46U || charCode == 58U || charCode == 44U))
							{
								monoAdjustment = this.m_monoSpacing / 2f - monoAdvance;
							}
							else
							{
								monoAdjustment = this.m_monoSpacing - monoAdvance;
							}
							this.m_xAdvance += (monoAdjustment + (this.m_currentFontAsset.normalSpacingOffset + characterSpacingAdjustment) * currentEmScale + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
							if (isWhiteSpace || charCode == 8203U)
							{
								this.m_xAdvance += this.m_wordSpacing * currentEmScale;
							}
						}
						else if (this.m_isRightToLeft)
						{
							this.m_xAdvance -= (glyphAdjustments.xAdvance * currentElementScale + (this.m_currentFontAsset.normalSpacingOffset + characterSpacingAdjustment + boldSpacingAdjustment) * currentEmScale + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
							if (isWhiteSpace || charCode == 8203U)
							{
								this.m_xAdvance -= this.m_wordSpacing * currentEmScale;
							}
						}
						else
						{
							this.m_xAdvance += ((currentGlyphMetrics.horizontalAdvance * this.m_FXScale.x + glyphAdjustments.xAdvance) * currentElementScale + (this.m_currentFontAsset.normalSpacingOffset + characterSpacingAdjustment + boldSpacingAdjustment) * currentEmScale + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
							if (isWhiteSpace || charCode == 8203U)
							{
								this.m_xAdvance += this.m_wordSpacing * currentEmScale;
							}
						}
						this.m_textInfo.characterInfo[this.m_characterCount].xAdvance = this.m_xAdvance;
						if (charCode == 13U)
						{
							this.m_xAdvance = 0f + this.tag_Indent;
						}
						if (this.m_overflowMode == TextOverflowModes.Page && charCode != 10U && charCode != 11U && charCode != 13U && charCode != 8232U && charCode != 8233U)
						{
							if (this.m_pageNumber + 1 > this.m_textInfo.pageInfo.Length)
							{
								TMP_TextInfo.Resize<TMP_PageInfo>(ref this.m_textInfo.pageInfo, this.m_pageNumber + 1, true);
							}
							this.m_textInfo.pageInfo[this.m_pageNumber].ascender = this.m_PageAscender;
							this.m_textInfo.pageInfo[this.m_pageNumber].descender = ((this.m_ElementDescender < this.m_textInfo.pageInfo[this.m_pageNumber].descender) ? this.m_ElementDescender : this.m_textInfo.pageInfo[this.m_pageNumber].descender);
							if (this.m_isNewPage)
							{
								this.m_isNewPage = false;
								this.m_textInfo.pageInfo[this.m_pageNumber].firstCharacterIndex = this.m_characterCount;
							}
							this.m_textInfo.pageInfo[this.m_pageNumber].lastCharacterIndex = this.m_characterCount;
						}
						if (charCode == 10U || charCode == 11U || charCode == 3U || charCode == 8232U || charCode == 8233U || (charCode == 45U && isInjectedCharacter) || this.m_characterCount == totalCharacterCount - 1)
						{
							float baselineAdjustmentDelta = this.m_maxLineAscender - this.m_startOfLineAscender;
							if (this.m_lineOffset > 0f && Math.Abs(baselineAdjustmentDelta) > 0.01f && !this.m_IsDrivenLineSpacing && !this.m_isNewPage)
							{
								base.AdjustLineOffset(this.m_firstCharacterOfLine, this.m_characterCount, baselineAdjustmentDelta);
								this.m_ElementDescender -= baselineAdjustmentDelta;
								this.m_lineOffset += baselineAdjustmentDelta;
								if (TMP_Text.m_SavedEllipsisState.lineNumber == this.m_lineNumber)
								{
									TMP_Text.m_SavedEllipsisState = TMP_Text.m_EllipsisInsertionCandidateStack.Pop();
									TMP_Text.m_SavedEllipsisState.startOfLineAscender = TMP_Text.m_SavedEllipsisState.startOfLineAscender + baselineAdjustmentDelta;
									TMP_Text.m_SavedEllipsisState.lineOffset = TMP_Text.m_SavedEllipsisState.lineOffset + baselineAdjustmentDelta;
									TMP_Text.m_EllipsisInsertionCandidateStack.Push(TMP_Text.m_SavedEllipsisState);
								}
							}
							this.m_isNewPage = false;
							float lineAscender = this.m_maxLineAscender - this.m_lineOffset;
							float lineDescender = this.m_maxLineDescender - this.m_lineOffset;
							this.m_ElementDescender = ((this.m_ElementDescender < lineDescender) ? this.m_ElementDescender : lineDescender);
							if (!isMaxVisibleDescenderSet)
							{
								maxVisibleDescender = this.m_ElementDescender;
							}
							if (this.m_useMaxVisibleDescender && (this.m_characterCount >= this.m_maxVisibleCharacters || this.m_lineNumber >= this.m_maxVisibleLines))
							{
								isMaxVisibleDescenderSet = true;
							}
							this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex = this.m_firstCharacterOfLine;
							this.m_textInfo.lineInfo[this.m_lineNumber].firstVisibleCharacterIndex = (this.m_firstVisibleCharacterOfLine = ((this.m_firstCharacterOfLine > this.m_firstVisibleCharacterOfLine) ? this.m_firstCharacterOfLine : this.m_firstVisibleCharacterOfLine));
							this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex = (this.m_lastCharacterOfLine = this.m_characterCount);
							this.m_textInfo.lineInfo[this.m_lineNumber].lastVisibleCharacterIndex = (this.m_lastVisibleCharacterOfLine = ((this.m_lastVisibleCharacterOfLine < this.m_firstVisibleCharacterOfLine) ? this.m_firstVisibleCharacterOfLine : this.m_lastVisibleCharacterOfLine));
							this.m_textInfo.lineInfo[this.m_lineNumber].characterCount = this.m_textInfo.lineInfo[this.m_lineNumber].lastCharacterIndex - this.m_textInfo.lineInfo[this.m_lineNumber].firstCharacterIndex + 1;
							this.m_textInfo.lineInfo[this.m_lineNumber].visibleCharacterCount = this.m_lineVisibleCharacterCount;
							this.m_textInfo.lineInfo[this.m_lineNumber].visibleSpaceCount = this.m_textInfo.lineInfo[this.m_lineNumber].lastVisibleCharacterIndex + 1 - this.m_lineVisibleCharacterCount;
							this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_firstVisibleCharacterOfLine].bottomLeft.x, lineDescender);
							this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].topRight.x, lineAscender);
							this.m_textInfo.lineInfo[this.m_lineNumber].length = this.m_textInfo.lineInfo[this.m_lineNumber].lineExtents.max.x - padding * currentElementScale;
							this.m_textInfo.lineInfo[this.m_lineNumber].width = widthOfTextArea;
							if (this.m_textInfo.lineInfo[this.m_lineNumber].characterCount == 1)
							{
								this.m_textInfo.lineInfo[this.m_lineNumber].alignment = this.m_lineJustification;
							}
							float maxAdvanceOffset = ((this.m_currentFontAsset.normalSpacingOffset + characterSpacingAdjustment + boldSpacingAdjustment) * currentEmScale + this.m_cSpacing) * (1f - this.m_charWidthAdjDelta);
							if (this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].isVisible)
							{
								this.m_textInfo.lineInfo[this.m_lineNumber].maxAdvance = this.m_textInfo.characterInfo[this.m_lastVisibleCharacterOfLine].xAdvance + (this.m_isRightToLeft ? maxAdvanceOffset : (-maxAdvanceOffset));
							}
							else
							{
								this.m_textInfo.lineInfo[this.m_lineNumber].maxAdvance = this.m_textInfo.characterInfo[this.m_lastCharacterOfLine].xAdvance + (this.m_isRightToLeft ? maxAdvanceOffset : (-maxAdvanceOffset));
							}
							this.m_textInfo.lineInfo[this.m_lineNumber].baseline = 0f - this.m_lineOffset;
							this.m_textInfo.lineInfo[this.m_lineNumber].ascender = lineAscender;
							this.m_textInfo.lineInfo[this.m_lineNumber].descender = lineDescender;
							this.m_textInfo.lineInfo[this.m_lineNumber].lineHeight = lineAscender - lineDescender + lineGap * baseScale;
							if (charCode == 10U || charCode == 11U || (charCode == 45U && isInjectedCharacter) || charCode == 8232U || charCode == 8233U)
							{
								base.SaveWordWrappingState(ref TMP_Text.m_SavedLineState, i, this.m_characterCount);
								this.m_lineNumber++;
								isStartOfNewLine = true;
								ignoreNonBreakingSpace = false;
								isFirstWordOfLine = true;
								this.m_firstCharacterOfLine = this.m_characterCount + 1;
								this.m_lineVisibleCharacterCount = 0;
								this.m_lineVisibleSpaceCount = 0;
								if (this.m_lineNumber >= this.m_textInfo.lineInfo.Length)
								{
									base.ResizeLineExtents(this.m_lineNumber);
								}
								float lastVisibleAscender = this.m_textInfo.characterInfo[this.m_characterCount].adjustedAscender;
								if (this.m_lineHeight == -32767f)
								{
									float lineOffsetDelta2 = 0f - this.m_maxLineDescender + lastVisibleAscender + (lineGap + this.m_lineSpacingDelta) * baseScale + (this.m_lineSpacing + ((charCode == 10U || charCode == 8233U) ? this.m_paragraphSpacing : 0f)) * currentEmScale;
									this.m_lineOffset += lineOffsetDelta2;
									this.m_IsDrivenLineSpacing = false;
								}
								else
								{
									this.m_lineOffset += this.m_lineHeight + (this.m_lineSpacing + ((charCode == 10U || charCode == 8233U) ? this.m_paragraphSpacing : 0f)) * currentEmScale;
									this.m_IsDrivenLineSpacing = true;
								}
								this.m_maxLineAscender = TMP_Text.k_LargeNegativeFloat;
								this.m_maxLineDescender = TMP_Text.k_LargePositiveFloat;
								this.m_startOfLineAscender = lastVisibleAscender;
								this.m_xAdvance = 0f + this.tag_LineIndent + this.tag_Indent;
								base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, i, this.m_characterCount);
								base.SaveWordWrappingState(ref TMP_Text.m_SavedLastValidState, i, this.m_characterCount);
								this.m_characterCount++;
								goto IL_40F0;
							}
							if (charCode == 3U)
							{
								i = this.m_TextProcessingArray.Length;
							}
						}
						if (this.m_textInfo.characterInfo[this.m_characterCount].isVisible)
						{
							this.m_meshExtents.min.x = Mathf.Min(this.m_meshExtents.min.x, this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft.x);
							this.m_meshExtents.min.y = Mathf.Min(this.m_meshExtents.min.y, this.m_textInfo.characterInfo[this.m_characterCount].bottomLeft.y);
							this.m_meshExtents.max.x = Mathf.Max(this.m_meshExtents.max.x, this.m_textInfo.characterInfo[this.m_characterCount].topRight.x);
							this.m_meshExtents.max.y = Mathf.Max(this.m_meshExtents.max.y, this.m_textInfo.characterInfo[this.m_characterCount].topRight.y);
						}
						if ((this.m_TextWrappingMode != TextWrappingModes.NoWrap && this.m_TextWrappingMode != TextWrappingModes.PreserveWhitespaceNoWrap) || this.m_overflowMode == TextOverflowModes.Truncate || this.m_overflowMode == TextOverflowModes.Ellipsis || this.m_overflowMode == TextOverflowModes.Linked)
						{
							bool shouldSaveHardLineBreak = false;
							bool shouldSaveSoftLineBreak = false;
							if ((isWhiteSpace || charCode == 8203U || charCode == 45U || charCode == 173U) && (!this.m_isNonBreakingSpace || ignoreNonBreakingSpace) && charCode != 160U && charCode != 8199U && charCode != 8209U && charCode != 8239U && charCode != 8288U)
							{
								if (charCode != 45U || this.m_characterCount <= 0 || !char.IsWhiteSpace(this.m_textInfo.characterInfo[this.m_characterCount - 1].character))
								{
									isFirstWordOfLine = false;
									shouldSaveHardLineBreak = true;
									TMP_Text.m_SavedSoftLineBreakState.previous_WordBreak = -1;
								}
							}
							else if (!this.m_isNonBreakingSpace && ((TMP_TextParsingUtilities.IsHangul(charCode) && !TMP_Settings.useModernHangulLineBreakingRules) || TMP_TextParsingUtilities.IsCJK(charCode)))
							{
								bool flag = TMP_Settings.linebreakingRules.leadingCharacters.Contains(charCode);
								bool isNextFollowingCharacter = this.m_characterCount < totalCharacterCount - 1 && TMP_Settings.linebreakingRules.followingCharacters.Contains((uint)this.m_textInfo.characterInfo[this.m_characterCount + 1].character);
								if (!flag)
								{
									if (!isNextFollowingCharacter)
									{
										isFirstWordOfLine = false;
										shouldSaveHardLineBreak = true;
									}
									if (isFirstWordOfLine)
									{
										if (isWhiteSpace)
										{
											shouldSaveSoftLineBreak = true;
										}
										shouldSaveHardLineBreak = true;
									}
								}
								else if (isFirstWordOfLine && isFirstCharacterOfLine)
								{
									if (isWhiteSpace)
									{
										shouldSaveSoftLineBreak = true;
									}
									shouldSaveHardLineBreak = true;
								}
							}
							else if (!this.m_isNonBreakingSpace && this.m_characterCount + 1 < totalCharacterCount && TMP_TextParsingUtilities.IsCJK((uint)this.m_textInfo.characterInfo[this.m_characterCount + 1].character))
							{
								shouldSaveHardLineBreak = true;
							}
							else if (isFirstWordOfLine)
							{
								if ((isWhiteSpace && charCode != 160U) || (charCode == 173U && !isSoftHyphenIgnored))
								{
									shouldSaveSoftLineBreak = true;
								}
								shouldSaveHardLineBreak = true;
							}
							if (shouldSaveHardLineBreak)
							{
								base.SaveWordWrappingState(ref TMP_Text.m_SavedWordWrapState, i, this.m_characterCount);
							}
							if (shouldSaveSoftLineBreak)
							{
								base.SaveWordWrappingState(ref TMP_Text.m_SavedSoftLineBreakState, i, this.m_characterCount);
							}
						}
						base.SaveWordWrappingState(ref TMP_Text.m_SavedLastValidState, i, this.m_characterCount);
						this.m_characterCount++;
					}
				}
				IL_40F0:
				i++;
			}
			float fontSizeDelta = this.m_maxFontSize - this.m_minFontSize;
			if (this.m_enableAutoSizing && fontSizeDelta > 0.051f && this.m_fontSize < this.m_fontSizeMax && this.m_AutoSizeIterationCount < this.m_AutoSizeMaxIterationCount)
			{
				if (this.m_charWidthAdjDelta < this.m_charWidthMaxAdj / 100f)
				{
					this.m_charWidthAdjDelta = 0f;
				}
				this.m_minFontSize = this.m_fontSize;
				float sizeDelta5 = Mathf.Max((this.m_maxFontSize - this.m_fontSize) / 2f, 0.05f);
				this.m_fontSize += sizeDelta5;
				this.m_fontSize = Mathf.Min((float)((int)(this.m_fontSize * 20f + 0.5f)) / 20f, this.m_fontSizeMax);
				return;
			}
			this.m_IsAutoSizePointSizeSet = true;
			if (this.m_AutoSizeIterationCount >= this.m_AutoSizeMaxIterationCount)
			{
				Debug.Log("Auto Size Iteration Count: " + this.m_AutoSizeIterationCount.ToString() + ". Final Point Size: " + this.m_fontSize.ToString());
			}
			if (this.m_characterCount == 0 || (this.m_characterCount == 1 && charCode == 3U))
			{
				this.ClearMesh(true);
				TMPro_EventManager.ON_TEXT_CHANGED(this);
				return;
			}
			int last_vert_index = TMP_Text.m_materialReferences[this.m_Underline.materialIndex].referenceCount * 4;
			this.m_textInfo.meshInfo[0].Clear(false);
			Vector3 anchorOffset = Vector3.zero;
			Vector3[] corners = this.m_RectTransformCorners;
			VerticalAlignmentOptions verticalAlignment = this.m_VerticalAlignment;
			if (verticalAlignment <= VerticalAlignmentOptions.Bottom)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Top)
				{
					if (verticalAlignment != VerticalAlignmentOptions.Middle)
					{
						if (verticalAlignment == VerticalAlignmentOptions.Bottom)
						{
							if (this.m_overflowMode != TextOverflowModes.Page)
							{
								anchorOffset = corners[0] + new Vector3(0f + margins.x, 0f - maxVisibleDescender + margins.w, 0f);
							}
							else
							{
								anchorOffset = corners[0] + new Vector3(0f + margins.x, 0f - this.m_textInfo.pageInfo[pageToDisplay].descender + margins.w, 0f);
							}
						}
					}
					else if (this.m_overflowMode != TextOverflowModes.Page)
					{
						anchorOffset = (corners[0] + corners[1]) / 2f + new Vector3(0f + margins.x, 0f - (this.m_maxTextAscender + margins.y + maxVisibleDescender - margins.w) / 2f, 0f);
					}
					else
					{
						anchorOffset = (corners[0] + corners[1]) / 2f + new Vector3(0f + margins.x, 0f - (this.m_textInfo.pageInfo[pageToDisplay].ascender + margins.y + this.m_textInfo.pageInfo[pageToDisplay].descender - margins.w) / 2f, 0f);
					}
				}
				else if (this.m_overflowMode != TextOverflowModes.Page)
				{
					anchorOffset = corners[1] + new Vector3(0f + margins.x, 0f - this.m_maxTextAscender - margins.y, 0f);
				}
				else
				{
					anchorOffset = corners[1] + new Vector3(0f + margins.x, 0f - this.m_textInfo.pageInfo[pageToDisplay].ascender - margins.y, 0f);
				}
			}
			else if (verticalAlignment != VerticalAlignmentOptions.Baseline)
			{
				if (verticalAlignment != VerticalAlignmentOptions.Geometry)
				{
					if (verticalAlignment == VerticalAlignmentOptions.Capline)
					{
						anchorOffset = (corners[0] + corners[1]) / 2f + new Vector3(0f + margins.x, 0f - (this.m_maxCapHeight - margins.y - margins.w) / 2f, 0f);
					}
				}
				else
				{
					anchorOffset = (corners[0] + corners[1]) / 2f + new Vector3(0f + margins.x, 0f - (this.m_meshExtents.max.y + margins.y + this.m_meshExtents.min.y - margins.w) / 2f, 0f);
				}
			}
			else
			{
				anchorOffset = (corners[0] + corners[1]) / 2f + new Vector3(0f + margins.x, 0f, 0f);
			}
			Vector3 justificationOffset = Vector3.zero;
			Vector3 offset = Vector3.zero;
			int wordCount = 0;
			int lineCount = 0;
			int lastLine = 0;
			bool isFirstSeperator = false;
			bool isStartOfWord = false;
			int wordFirstChar = 0;
			float lossyScale = (this.m_previousLossyScaleY = this.transform.lossyScale.y);
			Color32 underlineColor = Color.white;
			Color32 strikethroughColor = Color.white;
			HighlightState highlightState = new HighlightState(new Color32(byte.MaxValue, byte.MaxValue, 0, 64), TMP_Offset.zero);
			float xScale = 0f;
			float xScaleMax = 0f;
			float underlineStartScale = 0f;
			float underlineMaxScale = 0f;
			float underlineBaseLine = TMP_Text.k_LargePositiveFloat;
			int lastPage = 0;
			float strikethroughPointSize = 0f;
			float strikethroughScale = 0f;
			float strikethroughBaseline = 0f;
			TMP_CharacterInfo[] characterInfos = this.m_textInfo.characterInfo;
			int j = 0;
			while (j < this.m_characterCount)
			{
				TMP_FontAsset currentFontAsset = characterInfos[j].fontAsset;
				char unicode = characterInfos[j].character;
				bool isWhiteSpace2 = char.IsWhiteSpace(unicode);
				int currentLine = characterInfos[j].lineNumber;
				TMP_LineInfo lineInfo = this.m_textInfo.lineInfo[currentLine];
				lineCount = currentLine + 1;
				HorizontalAlignmentOptions lineAlignment = lineInfo.alignment;
				if (lineAlignment <= HorizontalAlignmentOptions.Justified)
				{
					switch (lineAlignment)
					{
					case HorizontalAlignmentOptions.Left:
						if (!this.m_isRightToLeft)
						{
							justificationOffset = new Vector3(0f + lineInfo.marginLeft, 0f, 0f);
						}
						else
						{
							justificationOffset = new Vector3(0f - lineInfo.maxAdvance, 0f, 0f);
						}
						break;
					case HorizontalAlignmentOptions.Center:
						justificationOffset = new Vector3(lineInfo.marginLeft + lineInfo.width / 2f - lineInfo.maxAdvance / 2f, 0f, 0f);
						break;
					case (HorizontalAlignmentOptions)3:
						break;
					case HorizontalAlignmentOptions.Right:
						if (!this.m_isRightToLeft)
						{
							justificationOffset = new Vector3(lineInfo.marginLeft + lineInfo.width - lineInfo.maxAdvance, 0f, 0f);
						}
						else
						{
							justificationOffset = new Vector3(lineInfo.marginLeft + lineInfo.width, 0f, 0f);
						}
						break;
					default:
						if (lineAlignment == HorizontalAlignmentOptions.Justified)
						{
							goto IL_48D9;
						}
						break;
					}
				}
				else
				{
					if (lineAlignment == HorizontalAlignmentOptions.Flush)
					{
						goto IL_48D9;
					}
					if (lineAlignment == HorizontalAlignmentOptions.Geometry)
					{
						justificationOffset = new Vector3(lineInfo.marginLeft + lineInfo.width / 2f - (lineInfo.lineExtents.min.x + lineInfo.lineExtents.max.x) / 2f, 0f, 0f);
					}
				}
				IL_4B57:
				offset = anchorOffset + justificationOffset;
				bool isCharacterVisible = characterInfos[j].isVisible;
				if (isCharacterVisible)
				{
					TMP_TextElementType elementType = characterInfos[j].elementType;
					if (elementType != TMP_TextElementType.Character)
					{
						if (elementType != TMP_TextElementType.Sprite)
						{
						}
					}
					else
					{
						Extents lineExtents = lineInfo.lineExtents;
						float uvOffset = this.m_uvLineOffset * (float)currentLine % 1f;
						switch (this.m_horizontalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfos[j].vertex_BL.uv2.x = 0f;
							characterInfos[j].vertex_TL.uv2.x = 0f;
							characterInfos[j].vertex_TR.uv2.x = 1f;
							characterInfos[j].vertex_BR.uv2.x = 1f;
							break;
						case TextureMappingOptions.Line:
							if (this.m_textAlignment != TextAlignmentOptions.Justified)
							{
								characterInfos[j].vertex_BL.uv2.x = (characterInfos[j].vertex_BL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + uvOffset;
								characterInfos[j].vertex_TL.uv2.x = (characterInfos[j].vertex_TL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + uvOffset;
								characterInfos[j].vertex_TR.uv2.x = (characterInfos[j].vertex_TR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + uvOffset;
								characterInfos[j].vertex_BR.uv2.x = (characterInfos[j].vertex_BR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + uvOffset;
							}
							else
							{
								characterInfos[j].vertex_BL.uv2.x = (characterInfos[j].vertex_BL.position.x + justificationOffset.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + uvOffset;
								characterInfos[j].vertex_TL.uv2.x = (characterInfos[j].vertex_TL.position.x + justificationOffset.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + uvOffset;
								characterInfos[j].vertex_TR.uv2.x = (characterInfos[j].vertex_TR.position.x + justificationOffset.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + uvOffset;
								characterInfos[j].vertex_BR.uv2.x = (characterInfos[j].vertex_BR.position.x + justificationOffset.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + uvOffset;
							}
							break;
						case TextureMappingOptions.Paragraph:
							characterInfos[j].vertex_BL.uv2.x = (characterInfos[j].vertex_BL.position.x + justificationOffset.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + uvOffset;
							characterInfos[j].vertex_TL.uv2.x = (characterInfos[j].vertex_TL.position.x + justificationOffset.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + uvOffset;
							characterInfos[j].vertex_TR.uv2.x = (characterInfos[j].vertex_TR.position.x + justificationOffset.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + uvOffset;
							characterInfos[j].vertex_BR.uv2.x = (characterInfos[j].vertex_BR.position.x + justificationOffset.x - this.m_meshExtents.min.x) / (this.m_meshExtents.max.x - this.m_meshExtents.min.x) + uvOffset;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							switch (this.m_verticalMapping)
							{
							case TextureMappingOptions.Character:
								characterInfos[j].vertex_BL.uv2.y = 0f;
								characterInfos[j].vertex_TL.uv2.y = 1f;
								characterInfos[j].vertex_TR.uv2.y = 0f;
								characterInfos[j].vertex_BR.uv2.y = 1f;
								break;
							case TextureMappingOptions.Line:
								characterInfos[j].vertex_BL.uv2.y = (characterInfos[j].vertex_BL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + uvOffset;
								characterInfos[j].vertex_TL.uv2.y = (characterInfos[j].vertex_TL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + uvOffset;
								characterInfos[j].vertex_TR.uv2.y = characterInfos[j].vertex_BL.uv2.y;
								characterInfos[j].vertex_BR.uv2.y = characterInfos[j].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.Paragraph:
								characterInfos[j].vertex_BL.uv2.y = (characterInfos[j].vertex_BL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y) + uvOffset;
								characterInfos[j].vertex_TL.uv2.y = (characterInfos[j].vertex_TL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y) + uvOffset;
								characterInfos[j].vertex_TR.uv2.y = characterInfos[j].vertex_BL.uv2.y;
								characterInfos[j].vertex_BR.uv2.y = characterInfos[j].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.MatchAspect:
								Debug.Log("ERROR: Cannot Match both Vertical & Horizontal.");
								break;
							}
							float xDelta = (1f - (characterInfos[j].vertex_BL.uv2.y + characterInfos[j].vertex_TL.uv2.y) * characterInfos[j].aspectRatio) / 2f;
							characterInfos[j].vertex_BL.uv2.x = characterInfos[j].vertex_BL.uv2.y * characterInfos[j].aspectRatio + xDelta + uvOffset;
							characterInfos[j].vertex_TL.uv2.x = characterInfos[j].vertex_BL.uv2.x;
							characterInfos[j].vertex_TR.uv2.x = characterInfos[j].vertex_TL.uv2.y * characterInfos[j].aspectRatio + xDelta + uvOffset;
							characterInfos[j].vertex_BR.uv2.x = characterInfos[j].vertex_TR.uv2.x;
							break;
						}
						}
						switch (this.m_verticalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfos[j].vertex_BL.uv2.y = 0f;
							characterInfos[j].vertex_TL.uv2.y = 1f;
							characterInfos[j].vertex_TR.uv2.y = 1f;
							characterInfos[j].vertex_BR.uv2.y = 0f;
							break;
						case TextureMappingOptions.Line:
							characterInfos[j].vertex_BL.uv2.y = (characterInfos[j].vertex_BL.position.y - lineInfo.descender) / (lineInfo.ascender - lineInfo.descender);
							characterInfos[j].vertex_TL.uv2.y = (characterInfos[j].vertex_TL.position.y - lineInfo.descender) / (lineInfo.ascender - lineInfo.descender);
							characterInfos[j].vertex_TR.uv2.y = characterInfos[j].vertex_TL.uv2.y;
							characterInfos[j].vertex_BR.uv2.y = characterInfos[j].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.Paragraph:
							characterInfos[j].vertex_BL.uv2.y = (characterInfos[j].vertex_BL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y);
							characterInfos[j].vertex_TL.uv2.y = (characterInfos[j].vertex_TL.position.y - this.m_meshExtents.min.y) / (this.m_meshExtents.max.y - this.m_meshExtents.min.y);
							characterInfos[j].vertex_TR.uv2.y = characterInfos[j].vertex_TL.uv2.y;
							characterInfos[j].vertex_BR.uv2.y = characterInfos[j].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							float yDelta = (1f - (characterInfos[j].vertex_BL.uv2.x + characterInfos[j].vertex_TR.uv2.x) / characterInfos[j].aspectRatio) / 2f;
							characterInfos[j].vertex_BL.uv2.y = yDelta + characterInfos[j].vertex_BL.uv2.x / characterInfos[j].aspectRatio;
							characterInfos[j].vertex_TL.uv2.y = yDelta + characterInfos[j].vertex_TR.uv2.x / characterInfos[j].aspectRatio;
							characterInfos[j].vertex_BR.uv2.y = characterInfos[j].vertex_BL.uv2.y;
							characterInfos[j].vertex_TR.uv2.y = characterInfos[j].vertex_TL.uv2.y;
							break;
						}
						}
						xScale = characterInfos[j].scale * Mathf.Abs(lossyScale) * (1f - this.m_charWidthAdjDelta);
						if (!characterInfos[j].isUsingAlternateTypeface && (characterInfos[j].style & FontStyles.Bold) == FontStyles.Bold)
						{
							xScale *= -1f;
						}
						characterInfos[j].vertex_BL.uv.w = xScale;
						characterInfos[j].vertex_TL.uv.w = xScale;
						characterInfos[j].vertex_TR.uv.w = xScale;
						characterInfos[j].vertex_BR.uv.w = xScale;
					}
					if (j < this.m_maxVisibleCharacters && wordCount < this.m_maxVisibleWords && currentLine < this.m_maxVisibleLines && this.m_overflowMode != TextOverflowModes.Page)
					{
						TMP_CharacterInfo[] array = characterInfos;
						int num3 = j;
						array[num3].vertex_BL.position = array[num3].vertex_BL.position + offset;
						TMP_CharacterInfo[] array2 = characterInfos;
						int num4 = j;
						array2[num4].vertex_TL.position = array2[num4].vertex_TL.position + offset;
						TMP_CharacterInfo[] array3 = characterInfos;
						int num5 = j;
						array3[num5].vertex_TR.position = array3[num5].vertex_TR.position + offset;
						TMP_CharacterInfo[] array4 = characterInfos;
						int num6 = j;
						array4[num6].vertex_BR.position = array4[num6].vertex_BR.position + offset;
					}
					else if (j < this.m_maxVisibleCharacters && wordCount < this.m_maxVisibleWords && currentLine < this.m_maxVisibleLines && this.m_overflowMode == TextOverflowModes.Page && characterInfos[j].pageNumber == pageToDisplay)
					{
						TMP_CharacterInfo[] array5 = characterInfos;
						int num7 = j;
						array5[num7].vertex_BL.position = array5[num7].vertex_BL.position + offset;
						TMP_CharacterInfo[] array6 = characterInfos;
						int num8 = j;
						array6[num8].vertex_TL.position = array6[num8].vertex_TL.position + offset;
						TMP_CharacterInfo[] array7 = characterInfos;
						int num9 = j;
						array7[num9].vertex_TR.position = array7[num9].vertex_TR.position + offset;
						TMP_CharacterInfo[] array8 = characterInfos;
						int num10 = j;
						array8[num10].vertex_BR.position = array8[num10].vertex_BR.position + offset;
					}
					else
					{
						characterInfos[j].vertex_BL.position = Vector3.zero;
						characterInfos[j].vertex_TL.position = Vector3.zero;
						characterInfos[j].vertex_TR.position = Vector3.zero;
						characterInfos[j].vertex_BR.position = Vector3.zero;
						characterInfos[j].isVisible = false;
					}
					if (QualitySettings.activeColorSpace == ColorSpace.Linear)
					{
						this.m_ConvertToLinearSpace = true;
					}
					else
					{
						this.m_ConvertToLinearSpace = false;
					}
					if (elementType == TMP_TextElementType.Character)
					{
						this.FillCharacterVertexBuffers(j);
					}
					else if (elementType == TMP_TextElementType.Sprite)
					{
						this.FillSpriteVertexBuffers(j);
					}
				}
				TMP_CharacterInfo[] characterInfo = this.m_textInfo.characterInfo;
				int num11 = j;
				characterInfo[num11].bottomLeft = characterInfo[num11].bottomLeft + offset;
				TMP_CharacterInfo[] characterInfo2 = this.m_textInfo.characterInfo;
				int num12 = j;
				characterInfo2[num12].topLeft = characterInfo2[num12].topLeft + offset;
				TMP_CharacterInfo[] characterInfo3 = this.m_textInfo.characterInfo;
				int num13 = j;
				characterInfo3[num13].topRight = characterInfo3[num13].topRight + offset;
				TMP_CharacterInfo[] characterInfo4 = this.m_textInfo.characterInfo;
				int num14 = j;
				characterInfo4[num14].bottomRight = characterInfo4[num14].bottomRight + offset;
				TMP_CharacterInfo[] characterInfo5 = this.m_textInfo.characterInfo;
				int num15 = j;
				characterInfo5[num15].origin = characterInfo5[num15].origin + offset.x;
				TMP_CharacterInfo[] characterInfo6 = this.m_textInfo.characterInfo;
				int num16 = j;
				characterInfo6[num16].xAdvance = characterInfo6[num16].xAdvance + offset.x;
				TMP_CharacterInfo[] characterInfo7 = this.m_textInfo.characterInfo;
				int num17 = j;
				characterInfo7[num17].ascender = characterInfo7[num17].ascender + offset.y;
				TMP_CharacterInfo[] characterInfo8 = this.m_textInfo.characterInfo;
				int num18 = j;
				characterInfo8[num18].descender = characterInfo8[num18].descender + offset.y;
				TMP_CharacterInfo[] characterInfo9 = this.m_textInfo.characterInfo;
				int num19 = j;
				characterInfo9[num19].baseLine = characterInfo9[num19].baseLine + offset.y;
				if (currentLine != lastLine || j == this.m_characterCount - 1)
				{
					if (currentLine != lastLine)
					{
						TMP_LineInfo[] lineInfo6 = this.m_textInfo.lineInfo;
						int num20 = lastLine;
						lineInfo6[num20].baseline = lineInfo6[num20].baseline + offset.y;
						TMP_LineInfo[] lineInfo7 = this.m_textInfo.lineInfo;
						int num21 = lastLine;
						lineInfo7[num21].ascender = lineInfo7[num21].ascender + offset.y;
						TMP_LineInfo[] lineInfo8 = this.m_textInfo.lineInfo;
						int num22 = lastLine;
						lineInfo8[num22].descender = lineInfo8[num22].descender + offset.y;
						TMP_LineInfo[] lineInfo9 = this.m_textInfo.lineInfo;
						int num23 = lastLine;
						lineInfo9[num23].maxAdvance = lineInfo9[num23].maxAdvance + offset.x;
						this.m_textInfo.lineInfo[lastLine].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[lastLine].firstCharacterIndex].bottomLeft.x, this.m_textInfo.lineInfo[lastLine].descender);
						this.m_textInfo.lineInfo[lastLine].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[lastLine].lastVisibleCharacterIndex].topRight.x, this.m_textInfo.lineInfo[lastLine].ascender);
					}
					if (j == this.m_characterCount - 1)
					{
						TMP_LineInfo[] lineInfo10 = this.m_textInfo.lineInfo;
						int num24 = currentLine;
						lineInfo10[num24].baseline = lineInfo10[num24].baseline + offset.y;
						TMP_LineInfo[] lineInfo11 = this.m_textInfo.lineInfo;
						int num25 = currentLine;
						lineInfo11[num25].ascender = lineInfo11[num25].ascender + offset.y;
						TMP_LineInfo[] lineInfo12 = this.m_textInfo.lineInfo;
						int num26 = currentLine;
						lineInfo12[num26].descender = lineInfo12[num26].descender + offset.y;
						TMP_LineInfo[] lineInfo13 = this.m_textInfo.lineInfo;
						int num27 = currentLine;
						lineInfo13[num27].maxAdvance = lineInfo13[num27].maxAdvance + offset.x;
						this.m_textInfo.lineInfo[currentLine].lineExtents.min = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[currentLine].firstCharacterIndex].bottomLeft.x, this.m_textInfo.lineInfo[currentLine].descender);
						this.m_textInfo.lineInfo[currentLine].lineExtents.max = new Vector2(this.m_textInfo.characterInfo[this.m_textInfo.lineInfo[currentLine].lastVisibleCharacterIndex].topRight.x, this.m_textInfo.lineInfo[currentLine].ascender);
					}
				}
				if (char.IsLetterOrDigit(unicode) || unicode == '-' || unicode == '\u00ad' || unicode == '‐' || unicode == '‑')
				{
					if (!isStartOfWord)
					{
						isStartOfWord = true;
						wordFirstChar = j;
					}
					if (isStartOfWord && j == this.m_characterCount - 1)
					{
						int size = this.m_textInfo.wordInfo.Length;
						int index = this.m_textInfo.wordCount;
						if (this.m_textInfo.wordCount + 1 > size)
						{
							TMP_TextInfo.Resize<TMP_WordInfo>(ref this.m_textInfo.wordInfo, size + 1);
						}
						int wordLastChar = j;
						this.m_textInfo.wordInfo[index].firstCharacterIndex = wordFirstChar;
						this.m_textInfo.wordInfo[index].lastCharacterIndex = wordLastChar;
						this.m_textInfo.wordInfo[index].characterCount = wordLastChar - wordFirstChar + 1;
						this.m_textInfo.wordInfo[index].textComponent = this;
						wordCount++;
						this.m_textInfo.wordCount++;
						TMP_LineInfo[] lineInfo14 = this.m_textInfo.lineInfo;
						int num28 = currentLine;
						lineInfo14[num28].wordCount = lineInfo14[num28].wordCount + 1;
					}
				}
				else if ((isStartOfWord || (j == 0 && (!char.IsPunctuation(unicode) || isWhiteSpace2 || unicode == '\u200b' || j == this.m_characterCount - 1))) && (j <= 0 || j >= characterInfos.Length - 1 || j >= this.m_characterCount || (unicode != '\'' && unicode != '’') || !char.IsLetterOrDigit(characterInfos[j - 1].character) || !char.IsLetterOrDigit(characterInfos[j + 1].character)))
				{
					int wordLastChar = ((j == this.m_characterCount - 1 && char.IsLetterOrDigit(unicode)) ? j : (j - 1));
					isStartOfWord = false;
					int size2 = this.m_textInfo.wordInfo.Length;
					int index2 = this.m_textInfo.wordCount;
					if (this.m_textInfo.wordCount + 1 > size2)
					{
						TMP_TextInfo.Resize<TMP_WordInfo>(ref this.m_textInfo.wordInfo, size2 + 1);
					}
					this.m_textInfo.wordInfo[index2].firstCharacterIndex = wordFirstChar;
					this.m_textInfo.wordInfo[index2].lastCharacterIndex = wordLastChar;
					this.m_textInfo.wordInfo[index2].characterCount = wordLastChar - wordFirstChar + 1;
					this.m_textInfo.wordInfo[index2].textComponent = this;
					wordCount++;
					this.m_textInfo.wordCount++;
					TMP_LineInfo[] lineInfo15 = this.m_textInfo.lineInfo;
					int num29 = currentLine;
					lineInfo15[num29].wordCount = lineInfo15[num29].wordCount + 1;
				}
				if ((this.m_textInfo.characterInfo[j].style & FontStyles.Underline) == FontStyles.Underline)
				{
					bool isUnderlineVisible = true;
					int currentPage = this.m_textInfo.characterInfo[j].pageNumber;
					this.m_textInfo.characterInfo[j].underlineVertexIndex = last_vert_index;
					if (j > this.m_maxVisibleCharacters || currentLine > this.m_maxVisibleLines || (this.m_overflowMode == TextOverflowModes.Page && currentPage + 1 != this.m_pageToDisplay))
					{
						isUnderlineVisible = false;
					}
					if (!isWhiteSpace2 && unicode != '\u200b')
					{
						underlineMaxScale = Mathf.Max(underlineMaxScale, this.m_textInfo.characterInfo[j].scale);
						xScaleMax = Mathf.Max(xScaleMax, Mathf.Abs(xScale));
						underlineBaseLine = Mathf.Min((currentPage == lastPage) ? underlineBaseLine : TMP_Text.k_LargePositiveFloat, this.m_textInfo.characterInfo[j].baseLine + base.font.m_FaceInfo.underlineOffset * underlineMaxScale);
						lastPage = currentPage;
					}
					if (!beginUnderline && isUnderlineVisible && j <= lineInfo.lastVisibleCharacterIndex && unicode != '\n' && unicode != '\v' && unicode != '\r' && (j != lineInfo.lastVisibleCharacterIndex || !char.IsSeparator(unicode)))
					{
						beginUnderline = true;
						underlineStartScale = this.m_textInfo.characterInfo[j].scale;
						if (underlineMaxScale == 0f)
						{
							underlineMaxScale = underlineStartScale;
							xScaleMax = xScale;
						}
						underline_start = new Vector3(this.m_textInfo.characterInfo[j].bottomLeft.x, underlineBaseLine, 0f);
						underlineColor = this.m_textInfo.characterInfo[j].underlineColor;
					}
					if (beginUnderline && this.m_characterCount == 1)
					{
						beginUnderline = false;
						underline_end = new Vector3(this.m_textInfo.characterInfo[j].topRight.x, underlineBaseLine, 0f);
						float underlineEndScale = this.m_textInfo.characterInfo[j].scale;
						this.DrawUnderlineMesh(underline_start, underline_end, ref last_vert_index, underlineStartScale, underlineEndScale, underlineMaxScale, xScaleMax, underlineColor);
						underlineMaxScale = 0f;
						xScaleMax = 0f;
						underlineBaseLine = TMP_Text.k_LargePositiveFloat;
					}
					else if (beginUnderline && (j == lineInfo.lastCharacterIndex || j >= lineInfo.lastVisibleCharacterIndex))
					{
						float underlineEndScale;
						if (isWhiteSpace2 || unicode == '\u200b')
						{
							int lastVisibleCharacterIndex = lineInfo.lastVisibleCharacterIndex;
							underline_end = new Vector3(this.m_textInfo.characterInfo[lastVisibleCharacterIndex].topRight.x, underlineBaseLine, 0f);
							underlineEndScale = this.m_textInfo.characterInfo[lastVisibleCharacterIndex].scale;
						}
						else
						{
							underline_end = new Vector3(this.m_textInfo.characterInfo[j].topRight.x, underlineBaseLine, 0f);
							underlineEndScale = this.m_textInfo.characterInfo[j].scale;
						}
						beginUnderline = false;
						this.DrawUnderlineMesh(underline_start, underline_end, ref last_vert_index, underlineStartScale, underlineEndScale, underlineMaxScale, xScaleMax, underlineColor);
						underlineMaxScale = 0f;
						xScaleMax = 0f;
						underlineBaseLine = TMP_Text.k_LargePositiveFloat;
					}
					else if (beginUnderline && !isUnderlineVisible)
					{
						beginUnderline = false;
						underline_end = new Vector3(this.m_textInfo.characterInfo[j - 1].topRight.x, underlineBaseLine, 0f);
						float underlineEndScale = this.m_textInfo.characterInfo[j - 1].scale;
						this.DrawUnderlineMesh(underline_start, underline_end, ref last_vert_index, underlineStartScale, underlineEndScale, underlineMaxScale, xScaleMax, underlineColor);
						underlineMaxScale = 0f;
						xScaleMax = 0f;
						underlineBaseLine = TMP_Text.k_LargePositiveFloat;
					}
					else if (beginUnderline && j < this.m_characterCount - 1 && !underlineColor.Compare(this.m_textInfo.characterInfo[j + 1].underlineColor))
					{
						beginUnderline = false;
						underline_end = new Vector3(this.m_textInfo.characterInfo[j].topRight.x, underlineBaseLine, 0f);
						float underlineEndScale = this.m_textInfo.characterInfo[j].scale;
						this.DrawUnderlineMesh(underline_start, underline_end, ref last_vert_index, underlineStartScale, underlineEndScale, underlineMaxScale, xScaleMax, underlineColor);
						underlineMaxScale = 0f;
						xScaleMax = 0f;
						underlineBaseLine = TMP_Text.k_LargePositiveFloat;
					}
				}
				else if (beginUnderline)
				{
					beginUnderline = false;
					underline_end = new Vector3(this.m_textInfo.characterInfo[j - 1].topRight.x, underlineBaseLine, 0f);
					float underlineEndScale = this.m_textInfo.characterInfo[j - 1].scale;
					this.DrawUnderlineMesh(underline_start, underline_end, ref last_vert_index, underlineStartScale, underlineEndScale, underlineMaxScale, xScaleMax, underlineColor);
					underlineMaxScale = 0f;
					xScaleMax = 0f;
					underlineBaseLine = TMP_Text.k_LargePositiveFloat;
				}
				bool flag2 = (this.m_textInfo.characterInfo[j].style & FontStyles.Strikethrough) == FontStyles.Strikethrough;
				float strikethroughOffset = currentFontAsset.m_FaceInfo.strikethroughOffset;
				if (flag2)
				{
					bool isStrikeThroughVisible = true;
					this.m_textInfo.characterInfo[j].strikethroughVertexIndex = last_vert_index;
					if (j > this.m_maxVisibleCharacters || currentLine > this.m_maxVisibleLines || (this.m_overflowMode == TextOverflowModes.Page && this.m_textInfo.characterInfo[j].pageNumber + 1 != this.m_pageToDisplay))
					{
						isStrikeThroughVisible = false;
					}
					if (!beginStrikethrough && isStrikeThroughVisible && j <= lineInfo.lastVisibleCharacterIndex && unicode != '\n' && unicode != '\v' && unicode != '\r' && (j != lineInfo.lastVisibleCharacterIndex || !char.IsSeparator(unicode)))
					{
						beginStrikethrough = true;
						strikethroughPointSize = this.m_textInfo.characterInfo[j].pointSize;
						strikethroughScale = this.m_textInfo.characterInfo[j].scale;
						strikethrough_start = new Vector3(this.m_textInfo.characterInfo[j].bottomLeft.x, this.m_textInfo.characterInfo[j].baseLine + strikethroughOffset * strikethroughScale, 0f);
						strikethroughColor = this.m_textInfo.characterInfo[j].strikethroughColor;
						strikethroughBaseline = this.m_textInfo.characterInfo[j].baseLine;
					}
					if (beginStrikethrough && this.m_characterCount == 1)
					{
						beginStrikethrough = false;
						strikethrough_end = new Vector3(this.m_textInfo.characterInfo[j].topRight.x, this.m_textInfo.characterInfo[j].baseLine + strikethroughOffset * strikethroughScale, 0f);
						this.DrawUnderlineMesh(strikethrough_start, strikethrough_end, ref last_vert_index, strikethroughScale, strikethroughScale, strikethroughScale, xScale, strikethroughColor);
					}
					else if (beginStrikethrough && j == lineInfo.lastCharacterIndex)
					{
						if (isWhiteSpace2 || unicode == '\u200b')
						{
							int lastVisibleCharacterIndex2 = lineInfo.lastVisibleCharacterIndex;
							strikethrough_end = new Vector3(this.m_textInfo.characterInfo[lastVisibleCharacterIndex2].topRight.x, this.m_textInfo.characterInfo[lastVisibleCharacterIndex2].baseLine + strikethroughOffset * strikethroughScale, 0f);
						}
						else
						{
							strikethrough_end = new Vector3(this.m_textInfo.characterInfo[j].topRight.x, this.m_textInfo.characterInfo[j].baseLine + strikethroughOffset * strikethroughScale, 0f);
						}
						beginStrikethrough = false;
						this.DrawUnderlineMesh(strikethrough_start, strikethrough_end, ref last_vert_index, strikethroughScale, strikethroughScale, strikethroughScale, xScale, strikethroughColor);
					}
					else if (beginStrikethrough && j < this.m_characterCount && (this.m_textInfo.characterInfo[j + 1].pointSize != strikethroughPointSize || !TMP_Math.Approximately(this.m_textInfo.characterInfo[j + 1].baseLine + offset.y, strikethroughBaseline)))
					{
						beginStrikethrough = false;
						int lastVisibleCharacterIndex3 = lineInfo.lastVisibleCharacterIndex;
						if (j > lastVisibleCharacterIndex3)
						{
							strikethrough_end = new Vector3(this.m_textInfo.characterInfo[lastVisibleCharacterIndex3].topRight.x, this.m_textInfo.characterInfo[lastVisibleCharacterIndex3].baseLine + strikethroughOffset * strikethroughScale, 0f);
						}
						else
						{
							strikethrough_end = new Vector3(this.m_textInfo.characterInfo[j].topRight.x, this.m_textInfo.characterInfo[j].baseLine + strikethroughOffset * strikethroughScale, 0f);
						}
						this.DrawUnderlineMesh(strikethrough_start, strikethrough_end, ref last_vert_index, strikethroughScale, strikethroughScale, strikethroughScale, xScale, strikethroughColor);
					}
					else if (beginStrikethrough && j < this.m_characterCount && currentFontAsset.GetInstanceID() != characterInfos[j + 1].fontAsset.GetInstanceID())
					{
						beginStrikethrough = false;
						strikethrough_end = new Vector3(this.m_textInfo.characterInfo[j].topRight.x, this.m_textInfo.characterInfo[j].baseLine + strikethroughOffset * strikethroughScale, 0f);
						this.DrawUnderlineMesh(strikethrough_start, strikethrough_end, ref last_vert_index, strikethroughScale, strikethroughScale, strikethroughScale, xScale, strikethroughColor);
					}
					else if (beginStrikethrough && !isStrikeThroughVisible)
					{
						beginStrikethrough = false;
						strikethrough_end = new Vector3(this.m_textInfo.characterInfo[j - 1].topRight.x, this.m_textInfo.characterInfo[j - 1].baseLine + strikethroughOffset * strikethroughScale, 0f);
						this.DrawUnderlineMesh(strikethrough_start, strikethrough_end, ref last_vert_index, strikethroughScale, strikethroughScale, strikethroughScale, xScale, strikethroughColor);
					}
				}
				else if (beginStrikethrough)
				{
					beginStrikethrough = false;
					strikethrough_end = new Vector3(this.m_textInfo.characterInfo[j - 1].topRight.x, this.m_textInfo.characterInfo[j - 1].baseLine + strikethroughOffset * strikethroughScale, 0f);
					this.DrawUnderlineMesh(strikethrough_start, strikethrough_end, ref last_vert_index, strikethroughScale, strikethroughScale, strikethroughScale, xScale, strikethroughColor);
				}
				if ((this.m_textInfo.characterInfo[j].style & FontStyles.Highlight) == FontStyles.Highlight)
				{
					bool isHighlightVisible = true;
					int currentPage2 = this.m_textInfo.characterInfo[j].pageNumber;
					if (j > this.m_maxVisibleCharacters || currentLine > this.m_maxVisibleLines || (this.m_overflowMode == TextOverflowModes.Page && currentPage2 + 1 != this.m_pageToDisplay))
					{
						isHighlightVisible = false;
					}
					if (!beginHighlight && isHighlightVisible && j <= lineInfo.lastVisibleCharacterIndex && unicode != '\n' && unicode != '\v' && unicode != '\r' && (j != lineInfo.lastVisibleCharacterIndex || !char.IsSeparator(unicode)))
					{
						beginHighlight = true;
						highlight_start = TMP_Text.k_LargePositiveVector2;
						highlight_end = TMP_Text.k_LargeNegativeVector2;
						highlightState = this.m_textInfo.characterInfo[j].highlightState;
					}
					if (beginHighlight)
					{
						TMP_CharacterInfo currentCharacter = this.m_textInfo.characterInfo[j];
						HighlightState currentState = currentCharacter.highlightState;
						bool isColorTransition = false;
						if (highlightState != currentState)
						{
							if (isWhiteSpace2)
							{
								highlight_end.x = (highlight_end.x - highlightState.padding.right + currentCharacter.origin) / 2f;
							}
							else
							{
								highlight_end.x = (highlight_end.x - highlightState.padding.right + currentCharacter.bottomLeft.x) / 2f;
							}
							highlight_start.y = Mathf.Min(highlight_start.y, currentCharacter.descender);
							highlight_end.y = Mathf.Max(highlight_end.y, currentCharacter.ascender);
							this.DrawTextHighlight(highlight_start, highlight_end, ref last_vert_index, highlightState.color);
							beginHighlight = true;
							highlight_start = new Vector2(highlight_end.x, currentCharacter.descender - currentState.padding.bottom);
							if (isWhiteSpace2)
							{
								highlight_end = new Vector2(currentCharacter.xAdvance + currentState.padding.right, currentCharacter.ascender + currentState.padding.top);
							}
							else
							{
								highlight_end = new Vector2(currentCharacter.topRight.x + currentState.padding.right, currentCharacter.ascender + currentState.padding.top);
							}
							highlightState = currentState;
							isColorTransition = true;
						}
						if (!isColorTransition)
						{
							if (isWhiteSpace2)
							{
								highlight_start.x = Mathf.Min(highlight_start.x, currentCharacter.origin - highlightState.padding.left);
								highlight_end.x = Mathf.Max(highlight_end.x, currentCharacter.xAdvance + highlightState.padding.right);
							}
							else
							{
								highlight_start.x = Mathf.Min(highlight_start.x, currentCharacter.bottomLeft.x - highlightState.padding.left);
								highlight_end.x = Mathf.Max(highlight_end.x, currentCharacter.topRight.x + highlightState.padding.right);
							}
							highlight_start.y = Mathf.Min(highlight_start.y, currentCharacter.descender - highlightState.padding.bottom);
							highlight_end.y = Mathf.Max(highlight_end.y, currentCharacter.ascender + highlightState.padding.top);
						}
					}
					if (beginHighlight && this.m_characterCount == 1)
					{
						beginHighlight = false;
						this.DrawTextHighlight(highlight_start, highlight_end, ref last_vert_index, highlightState.color);
					}
					else if (beginHighlight && (j == lineInfo.lastCharacterIndex || j >= lineInfo.lastVisibleCharacterIndex))
					{
						beginHighlight = false;
						this.DrawTextHighlight(highlight_start, highlight_end, ref last_vert_index, highlightState.color);
					}
					else if (beginHighlight && !isHighlightVisible)
					{
						beginHighlight = false;
						this.DrawTextHighlight(highlight_start, highlight_end, ref last_vert_index, highlightState.color);
					}
				}
				else if (beginHighlight)
				{
					beginHighlight = false;
					this.DrawTextHighlight(highlight_start, highlight_end, ref last_vert_index, highlightState.color);
				}
				lastLine = currentLine;
				j++;
				continue;
				IL_48D9:
				if (j > lineInfo.lastVisibleCharacterIndex || unicode == '\n' || unicode == '\u00ad' || unicode == '\u200b' || unicode == '\u2060' || unicode == '\u0003')
				{
					goto IL_4B57;
				}
				char character = characterInfos[lineInfo.lastCharacterIndex].character;
				bool isFlush = (lineAlignment & HorizontalAlignmentOptions.Flush) == HorizontalAlignmentOptions.Flush;
				if ((!char.IsControl(character) && currentLine < this.m_lineNumber) || isFlush || lineInfo.maxAdvance > lineInfo.width)
				{
					if (currentLine != lastLine || j == 0 || j == this.m_firstVisibleCharacter)
					{
						if (!this.m_isRightToLeft)
						{
							justificationOffset = new Vector3(lineInfo.marginLeft, 0f, 0f);
						}
						else
						{
							justificationOffset = new Vector3(lineInfo.marginLeft + lineInfo.width, 0f, 0f);
						}
						isFirstSeperator = char.IsSeparator(unicode);
						goto IL_4B57;
					}
					float gap = ((!this.m_isRightToLeft) ? (lineInfo.width - lineInfo.maxAdvance) : (lineInfo.width + lineInfo.maxAdvance));
					int visibleCount = lineInfo.visibleCharacterCount - 1 + lineInfo.controlCharacterCount;
					int spaces = lineInfo.spaceCount - lineInfo.controlCharacterCount;
					if (isFirstSeperator)
					{
						spaces--;
						visibleCount++;
					}
					float ratio = ((spaces > 0) ? this.m_wordWrappingRatios : 1f);
					if (spaces < 1)
					{
						spaces = 1;
					}
					if (unicode != '\u00a0' && (unicode == '\t' || char.IsSeparator(unicode)))
					{
						if (!this.m_isRightToLeft)
						{
							justificationOffset += new Vector3(gap * (1f - ratio) / (float)spaces, 0f, 0f);
							goto IL_4B57;
						}
						justificationOffset -= new Vector3(gap * (1f - ratio) / (float)spaces, 0f, 0f);
						goto IL_4B57;
					}
					else
					{
						if (!this.m_isRightToLeft)
						{
							justificationOffset += new Vector3(gap * ratio / (float)visibleCount, 0f, 0f);
							goto IL_4B57;
						}
						justificationOffset -= new Vector3(gap * ratio / (float)visibleCount, 0f, 0f);
						goto IL_4B57;
					}
				}
				else
				{
					if (!this.m_isRightToLeft)
					{
						justificationOffset = new Vector3(lineInfo.marginLeft, 0f, 0f);
						goto IL_4B57;
					}
					justificationOffset = new Vector3(lineInfo.marginLeft + lineInfo.width, 0f, 0f);
					goto IL_4B57;
				}
			}
			this.m_textInfo.meshInfo[this.m_Underline.materialIndex].vertexCount = last_vert_index;
			this.m_textInfo.characterCount = this.m_characterCount;
			this.m_textInfo.spriteCount = this.m_spriteCount;
			this.m_textInfo.lineCount = lineCount;
			this.m_textInfo.wordCount = ((wordCount != 0 && this.m_characterCount > 0) ? wordCount : 1);
			this.m_textInfo.pageCount = this.m_pageNumber + 1;
			if (this.m_renderMode == TextRenderFlags.Render && this.IsActive())
			{
				Action<TMP_TextInfo> onPreRenderText = this.OnPreRenderText;
				if (onPreRenderText != null)
				{
					onPreRenderText(this.m_textInfo);
				}
				if (this.m_geometrySortingOrder != VertexSortingOrder.Normal)
				{
					this.m_textInfo.meshInfo[0].SortGeometry(VertexSortingOrder.Reverse);
				}
				this.m_mesh.MarkDynamic();
				this.m_mesh.vertices = this.m_textInfo.meshInfo[0].vertices;
				this.m_mesh.SetUVs(0, this.m_textInfo.meshInfo[0].uvs0);
				this.m_mesh.uv2 = this.m_textInfo.meshInfo[0].uvs2;
				this.m_mesh.colors32 = this.m_textInfo.meshInfo[0].colors32;
				this.m_mesh.RecalculateBounds();
				for (int k = 1; k < this.m_textInfo.materialCount; k++)
				{
					this.m_textInfo.meshInfo[k].ClearUnusedVertices();
					if (!(this.m_subTextObjects[k] == null))
					{
						if (this.m_geometrySortingOrder != VertexSortingOrder.Normal)
						{
							this.m_textInfo.meshInfo[k].SortGeometry(VertexSortingOrder.Reverse);
						}
						this.m_subTextObjects[k].mesh.vertices = this.m_textInfo.meshInfo[k].vertices;
						this.m_subTextObjects[k].mesh.SetUVs(0, this.m_textInfo.meshInfo[k].uvs0);
						this.m_subTextObjects[k].mesh.uv2 = this.m_textInfo.meshInfo[k].uvs2;
						this.m_subTextObjects[k].mesh.colors32 = this.m_textInfo.meshInfo[k].colors32;
						this.m_subTextObjects[k].mesh.RecalculateBounds();
					}
				}
			}
			TMPro_EventManager.ON_TEXT_CHANGED(this);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00038FB5 File Offset: 0x000371B5
		protected override Vector3[] GetTextContainerLocalCorners()
		{
			if (this.m_rectTransform == null)
			{
				this.m_rectTransform = base.rectTransform;
			}
			this.m_rectTransform.GetLocalCorners(this.m_RectTransformCorners);
			return this.m_RectTransformCorners;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00038FE8 File Offset: 0x000371E8
		private void SetMeshFilters(bool state)
		{
			if (this.m_meshFilter != null)
			{
				if (state)
				{
					this.m_meshFilter.sharedMesh = this.m_mesh;
				}
				else
				{
					this.m_meshFilter.sharedMesh = null;
				}
			}
			int i = 1;
			while (i < this.m_subTextObjects.Length && this.m_subTextObjects[i] != null)
			{
				if (this.m_subTextObjects[i].meshFilter != null)
				{
					if (state)
					{
						this.m_subTextObjects[i].meshFilter.sharedMesh = this.m_subTextObjects[i].mesh;
					}
					else
					{
						this.m_subTextObjects[i].meshFilter.sharedMesh = null;
					}
				}
				i++;
			}
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00039094 File Offset: 0x00037294
		protected override void SetActiveSubMeshes(bool state)
		{
			int i = 1;
			while (i < this.m_subTextObjects.Length && this.m_subTextObjects[i] != null)
			{
				if (this.m_subTextObjects[i].enabled != state)
				{
					this.m_subTextObjects[i].enabled = state;
				}
				i++;
			}
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000390E4 File Offset: 0x000372E4
		protected void SetActiveSubTextObjectRenderers(bool state)
		{
			int i = 1;
			while (i < this.m_subTextObjects.Length && this.m_subTextObjects[i] != null)
			{
				Renderer subMeshRenderer = this.m_subTextObjects[i].renderer;
				if (subMeshRenderer != null && subMeshRenderer.enabled != state)
				{
					subMeshRenderer.enabled = state;
				}
				i++;
			}
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0003913C File Offset: 0x0003733C
		protected override void DestroySubMeshObjects()
		{
			int i = 1;
			while (i < this.m_subTextObjects.Length && this.m_subTextObjects[i] != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_subTextObjects[i]);
				i++;
			}
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0003917C File Offset: 0x0003737C
		internal void UpdateSubMeshSortingLayerID(int id)
		{
			for (int i = 1; i < this.m_subTextObjects.Length; i++)
			{
				TMP_SubMesh subMesh = this.m_subTextObjects[i];
				if (subMesh != null && subMesh.renderer != null)
				{
					subMesh.renderer.sortingLayerID = id;
				}
			}
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000391C8 File Offset: 0x000373C8
		internal void UpdateSubMeshSortingOrder(int order)
		{
			for (int i = 1; i < this.m_subTextObjects.Length; i++)
			{
				TMP_SubMesh subMesh = this.m_subTextObjects[i];
				if (subMesh != null && subMesh.renderer != null)
				{
					subMesh.renderer.sortingOrder = order;
				}
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00039214 File Offset: 0x00037414
		protected override Bounds GetCompoundBounds()
		{
			Bounds mainBounds = this.m_mesh.bounds;
			Vector3 min = mainBounds.min;
			Vector3 max = mainBounds.max;
			int i = 1;
			while (i < this.m_subTextObjects.Length && this.m_subTextObjects[i] != null)
			{
				Bounds subBounds = this.m_subTextObjects[i].mesh.bounds;
				min.x = ((min.x < subBounds.min.x) ? min.x : subBounds.min.x);
				min.y = ((min.y < subBounds.min.y) ? min.y : subBounds.min.y);
				max.x = ((max.x > subBounds.max.x) ? max.x : subBounds.max.x);
				max.y = ((max.y > subBounds.max.y) ? max.y : subBounds.max.y);
				i++;
			}
			Vector3 vector = (min + max) / 2f;
			Vector2 size = max - min;
			return new Bounds(vector, size);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x00039368 File Offset: 0x00037568
		private void UpdateSDFScale(float scaleDelta)
		{
			if (scaleDelta == 0f || scaleDelta == float.PositiveInfinity || scaleDelta == float.NegativeInfinity)
			{
				this.m_havePropertiesChanged = true;
				this.OnPreRenderObject();
				return;
			}
			for (int materialIndex = 0; materialIndex < this.m_textInfo.materialCount; materialIndex++)
			{
				TMP_MeshInfo meshInfo = this.m_textInfo.meshInfo[materialIndex];
				for (int i = 0; i < meshInfo.uvs0.Length; i++)
				{
					Vector4[] uvs = meshInfo.uvs0;
					int num = i;
					uvs[num].w = uvs[num].w * Mathf.Abs(scaleDelta);
				}
			}
			for (int j = 0; j < this.m_textInfo.meshInfo.Length; j++)
			{
				if (j == 0)
				{
					this.m_mesh.SetUVs(0, this.m_textInfo.meshInfo[0].uvs0);
				}
				else
				{
					this.m_subTextObjects[j].mesh.SetUVs(0, this.m_textInfo.meshInfo[j].uvs0);
				}
			}
		}

		// Token: 0x04000642 RID: 1602
		[SerializeField]
		internal int _SortingLayer;

		// Token: 0x04000643 RID: 1603
		[SerializeField]
		internal int _SortingLayerID;

		// Token: 0x04000644 RID: 1604
		[SerializeField]
		internal int _SortingOrder;

		// Token: 0x04000646 RID: 1606
		[SerializeField]
		private bool m_hasFontAssetChanged;

		// Token: 0x04000647 RID: 1607
		private float m_previousLossyScaleY = -1f;

		// Token: 0x04000648 RID: 1608
		[SerializeField]
		private Renderer m_renderer;

		// Token: 0x04000649 RID: 1609
		private MeshFilter m_meshFilter;

		// Token: 0x0400064A RID: 1610
		private bool m_isFirstAllocation;

		// Token: 0x0400064B RID: 1611
		private int m_max_characters = 8;

		// Token: 0x0400064C RID: 1612
		private int m_max_numberOfLines = 4;

		// Token: 0x0400064D RID: 1613
		private TMP_SubMesh[] m_subTextObjects = new TMP_SubMesh[8];

		// Token: 0x0400064E RID: 1614
		[SerializeField]
		private MaskingTypes m_maskType;

		// Token: 0x0400064F RID: 1615
		private Matrix4x4 m_EnvMapMatrix;

		// Token: 0x04000650 RID: 1616
		private Vector3[] m_RectTransformCorners = new Vector3[4];

		// Token: 0x04000651 RID: 1617
		[NonSerialized]
		private bool m_isRegisteredForEvents;

		// Token: 0x04000652 RID: 1618
		private static ProfilerMarker k_GenerateTextMarker = new ProfilerMarker("TMP Layout Text");

		// Token: 0x04000653 RID: 1619
		private static ProfilerMarker k_SetArraySizesMarker = new ProfilerMarker("TMP.SetArraySizes");

		// Token: 0x04000654 RID: 1620
		private static ProfilerMarker k_GenerateTextPhaseIMarker = new ProfilerMarker("TMP GenerateText - Phase I");

		// Token: 0x04000655 RID: 1621
		private static ProfilerMarker k_ParseMarkupTextMarker = new ProfilerMarker("TMP Parse Markup Text");

		// Token: 0x04000656 RID: 1622
		private static ProfilerMarker k_CharacterLookupMarker = new ProfilerMarker("TMP Lookup Character & Glyph Data");

		// Token: 0x04000657 RID: 1623
		private static ProfilerMarker k_HandleGPOSFeaturesMarker = new ProfilerMarker("TMP Handle GPOS Features");

		// Token: 0x04000658 RID: 1624
		private static ProfilerMarker k_CalculateVerticesPositionMarker = new ProfilerMarker("TMP Calculate Vertices Position");

		// Token: 0x04000659 RID: 1625
		private static ProfilerMarker k_ComputeTextMetricsMarker = new ProfilerMarker("TMP Compute Text Metrics");

		// Token: 0x0400065A RID: 1626
		private static ProfilerMarker k_HandleVisibleCharacterMarker = new ProfilerMarker("TMP Handle Visible Character");

		// Token: 0x0400065B RID: 1627
		private static ProfilerMarker k_HandleWhiteSpacesMarker = new ProfilerMarker("TMP Handle White Space & Control Character");

		// Token: 0x0400065C RID: 1628
		private static ProfilerMarker k_HandleHorizontalLineBreakingMarker = new ProfilerMarker("TMP Handle Horizontal Line Breaking");

		// Token: 0x0400065D RID: 1629
		private static ProfilerMarker k_HandleVerticalLineBreakingMarker = new ProfilerMarker("TMP Handle Vertical Line Breaking");

		// Token: 0x0400065E RID: 1630
		private static ProfilerMarker k_SaveGlyphVertexDataMarker = new ProfilerMarker("TMP Save Glyph Vertex Data");

		// Token: 0x0400065F RID: 1631
		private static ProfilerMarker k_ComputeCharacterAdvanceMarker = new ProfilerMarker("TMP Compute Character Advance");

		// Token: 0x04000660 RID: 1632
		private static ProfilerMarker k_HandleCarriageReturnMarker = new ProfilerMarker("TMP Handle Carriage Return");

		// Token: 0x04000661 RID: 1633
		private static ProfilerMarker k_HandleLineTerminationMarker = new ProfilerMarker("TMP Handle Line Termination");

		// Token: 0x04000662 RID: 1634
		private static ProfilerMarker k_SavePageInfoMarker = new ProfilerMarker("TMP Save Page Info");

		// Token: 0x04000663 RID: 1635
		private static ProfilerMarker k_SaveTextExtentMarker = new ProfilerMarker("TMP Save Text Extent");

		// Token: 0x04000664 RID: 1636
		private static ProfilerMarker k_SaveProcessingStatesMarker = new ProfilerMarker("TMP Save Processing States");

		// Token: 0x04000665 RID: 1637
		private static ProfilerMarker k_GenerateTextPhaseIIMarker = new ProfilerMarker("TMP GenerateText - Phase II");

		// Token: 0x04000666 RID: 1638
		private static ProfilerMarker k_GenerateTextPhaseIIIMarker = new ProfilerMarker("TMP GenerateText - Phase III");
	}
}
