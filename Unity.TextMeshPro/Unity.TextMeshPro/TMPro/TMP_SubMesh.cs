using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200007C RID: 124
	[RequireComponent(typeof(MeshRenderer))]
	[ExecuteAlways]
	public class TMP_SubMesh : MonoBehaviour
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003DE RID: 990 RVA: 0x00014DB9 File Offset: 0x00012FB9
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00014DC1 File Offset: 0x00012FC1
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00014DCA File Offset: 0x00012FCA
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x00014DD2 File Offset: 0x00012FD2
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

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00014DDB File Offset: 0x00012FDB
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00014DEC File Offset: 0x00012FEC
		public Material material
		{
			get
			{
				return this.GetMaterial(this.m_sharedMaterial);
			}
			set
			{
				if (this.m_sharedMaterial.GetInstanceID() == value.GetInstanceID())
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00014E35 File Offset: 0x00013035
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00014E3D File Offset: 0x0001303D
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

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00014E46 File Offset: 0x00013046
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00014E50 File Offset: 0x00013050
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

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00014EB1 File Offset: 0x000130B1
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00014EB9 File Offset: 0x000130B9
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

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00014EC2 File Offset: 0x000130C2
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x00014ECA File Offset: 0x000130CA
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00014ED3 File Offset: 0x000130D3
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x00014EDB File Offset: 0x000130DB
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00014EE4 File Offset: 0x000130E4
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

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x00014F08 File Offset: 0x00013108
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

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00014F61 File Offset: 0x00013161
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00014F8F File Offset: 0x0001318F
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

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00014F98 File Offset: 0x00013198
		public TMP_Text textComponent
		{
			get
			{
				if (this.m_TextComponent == null)
				{
					this.m_TextComponent = base.GetComponentInParent<TextMeshPro>();
				}
				return this.m_TextComponent;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00014FBC File Offset: 0x000131BC
		public static TMP_SubMesh AddSubTextObject(TextMeshPro textComponent, MaterialReference materialReference)
		{
			GameObject gameObject = new GameObject();
			gameObject.hideFlags = (TMP_Settings.hideSubTextObjects ? HideFlags.HideAndDontSave : HideFlags.DontSave);
			gameObject.transform.SetParent(textComponent.transform, false);
			gameObject.transform.localPosition = Vector3.zero;
			gameObject.transform.localRotation = Quaternion.identity;
			gameObject.transform.localScale = Vector3.one;
			gameObject.layer = textComponent.gameObject.layer;
			TMP_SubMesh tmp_SubMesh = gameObject.AddComponent<TMP_SubMesh>();
			tmp_SubMesh.m_TextComponent = textComponent;
			tmp_SubMesh.m_fontAsset = materialReference.fontAsset;
			tmp_SubMesh.m_spriteAsset = materialReference.spriteAsset;
			tmp_SubMesh.m_isDefaultMaterial = materialReference.isDefaultMaterial;
			tmp_SubMesh.SetSharedMaterial(materialReference.material);
			tmp_SubMesh.renderer.sortingLayerID = textComponent.renderer.sortingLayerID;
			tmp_SubMesh.renderer.sortingOrder = textComponent.renderer.sortingOrder;
			return tmp_SubMesh;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001509C File Offset: 0x0001329C
		private void OnEnable()
		{
			if (!this.m_isRegisteredForEvents)
			{
				this.m_isRegisteredForEvents = true;
			}
			if (base.hideFlags != HideFlags.DontSave)
			{
				base.hideFlags = HideFlags.DontSave;
			}
			this.meshFilter.sharedMesh = this.mesh;
			if (this.m_sharedMaterial != null)
			{
				this.m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, new Vector4(-32767f, -32767f, 32767f, 32767f));
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00015112 File Offset: 0x00013312
		private void OnDisable()
		{
			this.m_meshFilter.sharedMesh = null;
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00015140 File Offset: 0x00013340
		private void OnDestroy()
		{
			if (this.m_mesh != null)
			{
				global::UnityEngine.Object.DestroyImmediate(this.m_mesh);
			}
			if (this.m_fallbackMaterial != null)
			{
				TMP_MaterialManager.ReleaseFallbackMaterial(this.m_fallbackMaterial);
				this.m_fallbackMaterial = null;
			}
			this.m_isRegisteredForEvents = false;
			if (this.m_TextComponent != null)
			{
				this.m_TextComponent.havePropertiesChanged = true;
				this.m_TextComponent.SetAllDirty();
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000151B2 File Offset: 0x000133B2
		public void DestroySelf()
		{
			global::UnityEngine.Object.Destroy(base.gameObject, 1f);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000151C4 File Offset: 0x000133C4
		private Material GetMaterial(Material mat)
		{
			if (this.m_renderer == null)
			{
				this.m_renderer = base.GetComponent<Renderer>();
			}
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

		// Token: 0x060003F9 RID: 1017 RVA: 0x00015243 File Offset: 0x00013443
		private Material CreateMaterialInstance(Material source)
		{
			Material material = new Material(source);
			material.shaderKeywords = source.shaderKeywords;
			material.name += " (Instance)";
			return material;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001526D File Offset: 0x0001346D
		private Material GetSharedMaterial()
		{
			if (this.m_renderer == null)
			{
				this.m_renderer = base.GetComponent<Renderer>();
			}
			return this.m_renderer.sharedMaterial;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00015294 File Offset: 0x00013494
		private void SetSharedMaterial(Material mat)
		{
			this.m_sharedMaterial = mat;
			this.m_padding = this.GetPaddingForMaterial();
			this.SetMaterialDirty();
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000152AF File Offset: 0x000134AF
		public float GetPaddingForMaterial()
		{
			return ShaderUtilities.GetPadding(this.m_sharedMaterial, this.m_TextComponent.extraPadding, this.m_TextComponent.isUsingBold);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000152D2 File Offset: 0x000134D2
		public void UpdateMeshPadding(bool isExtraPadding, bool isUsingBold)
		{
			this.m_padding = ShaderUtilities.GetPadding(this.m_sharedMaterial, isExtraPadding, isUsingBold);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00002AAB File Offset: 0x00000CAB
		public void SetVerticesDirty()
		{
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000152E7 File Offset: 0x000134E7
		public void SetMaterialDirty()
		{
			this.UpdateMaterial();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000152F0 File Offset: 0x000134F0
		protected void UpdateMaterial()
		{
			if (this.renderer == null || this.m_sharedMaterial == null)
			{
				return;
			}
			this.m_renderer.sharedMaterial = this.m_sharedMaterial;
			if (this.m_sharedMaterial.HasProperty(ShaderUtilities.ShaderTag_CullMode) && this.textComponent.fontSharedMaterial != null)
			{
				float cullMode = this.textComponent.fontSharedMaterial.GetFloat(ShaderUtilities.ShaderTag_CullMode);
				this.m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_CullMode, cullMode);
			}
		}

		// Token: 0x040003AF RID: 943
		[SerializeField]
		private TMP_FontAsset m_fontAsset;

		// Token: 0x040003B0 RID: 944
		[SerializeField]
		private TMP_SpriteAsset m_spriteAsset;

		// Token: 0x040003B1 RID: 945
		[SerializeField]
		private Material m_material;

		// Token: 0x040003B2 RID: 946
		[SerializeField]
		private Material m_sharedMaterial;

		// Token: 0x040003B3 RID: 947
		private Material m_fallbackMaterial;

		// Token: 0x040003B4 RID: 948
		private Material m_fallbackSourceMaterial;

		// Token: 0x040003B5 RID: 949
		[SerializeField]
		private bool m_isDefaultMaterial;

		// Token: 0x040003B6 RID: 950
		[SerializeField]
		private float m_padding;

		// Token: 0x040003B7 RID: 951
		[SerializeField]
		private Renderer m_renderer;

		// Token: 0x040003B8 RID: 952
		private MeshFilter m_meshFilter;

		// Token: 0x040003B9 RID: 953
		private Mesh m_mesh;

		// Token: 0x040003BA RID: 954
		[SerializeField]
		private TextMeshPro m_TextComponent;

		// Token: 0x040003BB RID: 955
		[NonSerialized]
		private bool m_isRegisteredForEvents;
	}
}
