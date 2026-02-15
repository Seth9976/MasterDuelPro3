using System;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Serialization;
using UnityEngine.U2D;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000027 RID: 39
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[MovedFrom(true, "UnityEngine.Experimental.Rendering.Universal", "Unity.RenderPipelines.Universal.Runtime", null)]
	[AddComponentMenu("Rendering/2D/Light 2D")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@latest/index.html?subfolder=/manual/2DLightProperties.html")]
	public sealed class Light2D : Light2DBase, ISerializationCallbackReceiver
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000ADD9 File Offset: 0x00008FD9
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x0000ADE1 File Offset: 0x00008FE1
		internal LightUtility.LightMeshVertex[] vertices
		{
			get
			{
				return this.m_Vertices;
			}
			set
			{
				this.m_Vertices = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000ADEA File Offset: 0x00008FEA
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x0000ADF2 File Offset: 0x00008FF2
		internal ushort[] indices
		{
			get
			{
				return this.m_Triangles;
			}
			set
			{
				this.m_Triangles = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000ADFB File Offset: 0x00008FFB
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x0000AE03 File Offset: 0x00009003
		internal int batchSlotIndex
		{
			get
			{
				return this.m_BatchSlotIndex;
			}
			set
			{
				this.m_BatchSlotIndex = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000AE0C File Offset: 0x0000900C
		internal int[] affectedSortingLayers
		{
			get
			{
				return this.m_ApplyToSortingLayers;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000AE14 File Offset: 0x00009014
		private int lightCookieSpriteInstanceID
		{
			get
			{
				Sprite lightCookieSprite = this.lightCookieSprite;
				if (lightCookieSprite == null)
				{
					return 0;
				}
				return lightCookieSprite.GetInstanceID();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000AE27 File Offset: 0x00009027
		internal bool useCookieSprite
		{
			get
			{
				return (this.lightType == Light2D.LightType.Point || this.lightType == Light2D.LightType.Sprite) && this.lightCookieSprite != null && this.lightCookieSprite.texture != null;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000EA RID: 234 RVA: 0x0000AE5E File Offset: 0x0000905E
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000AE66 File Offset: 0x00009066
		internal BoundingSphere boundingSphere { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000EC RID: 236 RVA: 0x0000AE6F File Offset: 0x0000906F
		internal Mesh lightMesh
		{
			get
			{
				if (null == this.m_Mesh)
				{
					this.m_Mesh = new Mesh();
				}
				return this.m_Mesh;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000AE90 File Offset: 0x00009090
		internal bool hasCachedMesh
		{
			get
			{
				return this.vertices.Length > 1 && this.indices.Length > 1;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000EE RID: 238 RVA: 0x0000AEAA File Offset: 0x000090AA
		// (set) Token: 0x060000EF RID: 239 RVA: 0x0000AEB2 File Offset: 0x000090B2
		public Light2D.LightType lightType
		{
			get
			{
				return this.m_LightType;
			}
			set
			{
				if (this.m_LightType != value)
				{
					this.UpdateMesh(false);
				}
				this.m_LightType = value;
				Light2DManager.ErrorIfDuplicateGlobalLight(this);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000AED1 File Offset: 0x000090D1
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x0000AED9 File Offset: 0x000090D9
		public int blendStyleIndex
		{
			get
			{
				return this.m_BlendStyleIndex;
			}
			set
			{
				this.m_BlendStyleIndex = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000AEE2 File Offset: 0x000090E2
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x0000AEEA File Offset: 0x000090EA
		public float shadowIntensity
		{
			get
			{
				return this.m_ShadowIntensity;
			}
			set
			{
				this.m_ShadowIntensity = Mathf.Clamp01(value);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000AEF8 File Offset: 0x000090F8
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x0000AF00 File Offset: 0x00009100
		public float shadowSoftness
		{
			get
			{
				return this.m_ShadowSoftness;
			}
			set
			{
				this.m_ShadowSoftness = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000AF09 File Offset: 0x00009109
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x0000AF11 File Offset: 0x00009111
		public bool shadowsEnabled
		{
			get
			{
				return this.m_ShadowsEnabled;
			}
			set
			{
				this.m_ShadowsEnabled = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000AF1A File Offset: 0x0000911A
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x0000AF22 File Offset: 0x00009122
		public float shadowVolumeIntensity
		{
			get
			{
				return this.m_ShadowVolumeIntensity;
			}
			set
			{
				this.m_ShadowVolumeIntensity = Mathf.Clamp01(value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000AF30 File Offset: 0x00009130
		// (set) Token: 0x060000FB RID: 251 RVA: 0x0000AF38 File Offset: 0x00009138
		public bool volumetricShadowsEnabled
		{
			get
			{
				return this.m_ShadowVolumeIntensityEnabled;
			}
			set
			{
				this.m_ShadowVolumeIntensityEnabled = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000AF41 File Offset: 0x00009141
		// (set) Token: 0x060000FD RID: 253 RVA: 0x0000AF49 File Offset: 0x00009149
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000AF52 File Offset: 0x00009152
		// (set) Token: 0x060000FF RID: 255 RVA: 0x0000AF5A File Offset: 0x0000915A
		public float intensity
		{
			get
			{
				return this.m_Intensity;
			}
			set
			{
				this.m_Intensity = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000AF63 File Offset: 0x00009163
		[Obsolete]
		public float volumeOpacity
		{
			get
			{
				return this.m_LightVolumeIntensity;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000AF63 File Offset: 0x00009163
		// (set) Token: 0x06000102 RID: 258 RVA: 0x0000AF6B File Offset: 0x0000916B
		public float volumeIntensity
		{
			get
			{
				return this.m_LightVolumeIntensity;
			}
			set
			{
				this.m_LightVolumeIntensity = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000AF74 File Offset: 0x00009174
		// (set) Token: 0x06000104 RID: 260 RVA: 0x0000AF7C File Offset: 0x0000917C
		[Obsolete]
		public bool volumeIntensityEnabled
		{
			get
			{
				return this.m_LightVolumeEnabled;
			}
			set
			{
				this.m_LightVolumeEnabled = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000AF74 File Offset: 0x00009174
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000AF7C File Offset: 0x0000917C
		public bool volumetricEnabled
		{
			get
			{
				return this.m_LightVolumeEnabled;
			}
			set
			{
				this.m_LightVolumeEnabled = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000AF85 File Offset: 0x00009185
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000AF9D File Offset: 0x0000919D
		public Sprite lightCookieSprite
		{
			get
			{
				if (this.m_LightType == Light2D.LightType.Point)
				{
					return this.m_DeprecatedPointLightCookieSprite;
				}
				return this.m_LightCookieSprite;
			}
			set
			{
				this.m_LightCookieSprite = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000AFA6 File Offset: 0x000091A6
		// (set) Token: 0x0600010A RID: 266 RVA: 0x0000AFAE File Offset: 0x000091AE
		public float falloffIntensity
		{
			get
			{
				return this.m_FalloffIntensity;
			}
			set
			{
				this.m_FalloffIntensity = Mathf.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000AFC6 File Offset: 0x000091C6
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000AFCE File Offset: 0x000091CE
		public float shadowSoftnessFalloffIntensity
		{
			get
			{
				return this.m_ShadowSoftnessFalloffIntensity;
			}
			set
			{
				this.m_ShadowSoftnessFalloffIntensity = Mathf.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000AFE6 File Offset: 0x000091E6
		[Obsolete]
		public bool alphaBlendOnOverlap
		{
			get
			{
				return this.m_OverlapOperation == Light2D.OverlapOperation.AlphaBlend;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000AFF1 File Offset: 0x000091F1
		// (set) Token: 0x0600010F RID: 271 RVA: 0x0000AFF9 File Offset: 0x000091F9
		public Light2D.OverlapOperation overlapOperation
		{
			get
			{
				return this.m_OverlapOperation;
			}
			set
			{
				this.m_OverlapOperation = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000B002 File Offset: 0x00009202
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000B00A File Offset: 0x0000920A
		public int lightOrder
		{
			get
			{
				return this.m_LightOrder;
			}
			set
			{
				this.m_LightOrder = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000B013 File Offset: 0x00009213
		public float normalMapDistance
		{
			get
			{
				return this.m_NormalMapDistance;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000B01B File Offset: 0x0000921B
		public Light2D.NormalMapQuality normalMapQuality
		{
			get
			{
				return this.m_NormalMapQuality;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000B023 File Offset: 0x00009223
		public bool renderVolumetricShadows
		{
			get
			{
				return this.volumetricShadowsEnabled && this.shadowVolumeIntensity > 0f;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000B03C File Offset: 0x0000923C
		internal void MarkForUpdate()
		{
			this.forceUpdate = true;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000B045 File Offset: 0x00009245
		internal void CacheValues()
		{
			this.m_CachedPosition = base.transform.position;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000B058 File Offset: 0x00009258
		internal int GetTopMostLitLayer()
		{
			int largestIndex = int.MinValue;
			int largestLayer = 0;
			SortingLayer[] layers = Light2DManager.GetCachedSortingLayer();
			for (int i = 0; i < this.m_ApplyToSortingLayers.Length; i++)
			{
				for (int layer = layers.Length - 1; layer >= largestLayer; layer--)
				{
					if (layers[layer].id == this.m_ApplyToSortingLayers[i])
					{
						largestIndex = layers[layer].value;
						largestLayer = layer;
					}
				}
			}
			return largestIndex;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x0000B0C4 File Offset: 0x000092C4
		internal Bounds UpdateSpriteMesh()
		{
			if (this.m_LightCookieSprite == null && (this.m_Vertices.Length != 1 || this.m_Triangles.Length != 1))
			{
				this.m_Vertices = new LightUtility.LightMeshVertex[1];
				this.m_Triangles = new ushort[1];
			}
			return LightUtility.GenerateSpriteMesh(this, this.m_LightCookieSprite, LightBatch.GetBatchColor());
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000B120 File Offset: 0x00009320
		internal void UpdateBatchSlotIndex()
		{
			if (this.lightMesh && this.lightMesh.colors != null && this.lightMesh.colors.Length != 0)
			{
				this.m_BatchSlotIndex = LightBatch.GetBatchSlotIndex(this.lightMesh.colors[0].b);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000B178 File Offset: 0x00009378
		internal bool NeedsColorIndexBaking()
		{
			return this.lightMesh && LightBatch.isBatchingSupported && this.lightMesh.colors.Length != 0 && this.lightMesh.colors[0].b == 0f;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000B1C6 File Offset: 0x000093C6
		internal void UpdateCookieSpriteTexture()
		{
			RTHandle cookieSpriteTexture = this.m_CookieSpriteTexture;
			if (cookieSpriteTexture != null)
			{
				cookieSpriteTexture.Release();
			}
			if (this.useCookieSprite)
			{
				this.m_CookieSpriteTexture = RTHandles.Alloc(this.lightCookieSprite.texture);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000B1F8 File Offset: 0x000093F8
		internal void UpdateMesh(bool forceUpdate = false)
		{
			int shapePathHash = LightUtility.GetShapePathHash(this.shapePath);
			bool fallOffSizeChanged = LightUtility.CheckForChange(this.m_ShapeLightFalloffSize, ref this.m_PreviousShapeLightFalloffSize);
			bool parametricRadiusChanged = LightUtility.CheckForChange(this.m_ShapeLightParametricRadius, ref this.m_PreviousShapeLightParametricRadius);
			bool parametricSidesChanged = LightUtility.CheckForChange(this.m_ShapeLightParametricSides, ref this.m_PreviousShapeLightParametricSides);
			bool parametricAngleOffsetChanged = LightUtility.CheckForChange(this.m_ShapeLightParametricAngleOffset, ref this.m_PreviousShapeLightParametricAngleOffset);
			bool spriteInstanceChanged = LightUtility.CheckForChange(this.lightCookieSpriteInstanceID, ref this.m_PreviousLightCookieSprite);
			bool shapePathHashChanged = LightUtility.CheckForChange(shapePathHash, ref this.m_PreviousShapePathHash);
			bool lightTypeChanged = LightUtility.CheckForChange(this.m_LightType, ref this.m_PreviousLightType);
			if (fallOffSizeChanged || parametricRadiusChanged || parametricSidesChanged || parametricAngleOffsetChanged || spriteInstanceChanged || shapePathHashChanged || lightTypeChanged || this.NeedsColorIndexBaking() || forceUpdate)
			{
				float batchChannelColor = LightBatch.GetBatchColor();
				switch (this.m_LightType)
				{
				case Light2D.LightType.Parametric:
					this.m_LocalBounds = LightUtility.GenerateParametricMesh(this, this.m_ShapeLightParametricRadius, this.m_ShapeLightFalloffSize, this.m_ShapeLightParametricAngleOffset, this.m_ShapeLightParametricSides, batchChannelColor);
					break;
				case Light2D.LightType.Freeform:
					this.m_LocalBounds = LightUtility.GenerateShapeMesh(this, this.m_ShapePath, this.m_ShapeLightFalloffSize, batchChannelColor);
					break;
				case Light2D.LightType.Sprite:
					this.m_LocalBounds = this.UpdateSpriteMesh();
					break;
				case Light2D.LightType.Point:
					this.m_LocalBounds = LightUtility.GenerateParametricMesh(this, 1.412135f, 0f, 0f, 4, batchChannelColor);
					break;
				}
				this.UpdateCookieSpriteTexture();
				this.UpdateBatchSlotIndex();
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000B354 File Offset: 0x00009554
		internal void UpdateBoundingSphere()
		{
			if (this.isPointLight)
			{
				this.boundingSphere = new BoundingSphere(base.transform.position, this.m_PointLightOuterRadius);
				return;
			}
			Vector3 maxBound = base.transform.TransformPoint(Vector3.Max(this.m_LocalBounds.max, this.m_LocalBounds.max + this.m_ShapeLightFalloffOffset));
			Vector3 minBound = base.transform.TransformPoint(Vector3.Min(this.m_LocalBounds.min, this.m_LocalBounds.min + this.m_ShapeLightFalloffOffset));
			Vector3 center = 0.5f * (maxBound + minBound);
			float radius = Vector3.Magnitude(maxBound - center);
			this.boundingSphere = new BoundingSphere(center, radius);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000B420 File Offset: 0x00009620
		internal bool IsLitLayer(int layer)
		{
			if (this.m_ApplyToSortingLayers == null)
			{
				return false;
			}
			for (int i = 0; i < this.m_ApplyToSortingLayers.Length; i++)
			{
				if (this.m_ApplyToSortingLayers[i] == layer)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000B458 File Offset: 0x00009658
		internal Matrix4x4 GetMatrix()
		{
			Matrix4x4 matrix = base.transform.localToWorldMatrix;
			if (this.lightType == Light2D.LightType.Point)
			{
				Vector3 scale = new Vector3(this.pointLightOuterRadius, this.pointLightOuterRadius, this.pointLightOuterRadius);
				matrix = Matrix4x4.TRS(base.transform.position, base.transform.rotation, scale);
			}
			return matrix;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000B4B1 File Offset: 0x000096B1
		private void Awake()
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000B4B3 File Offset: 0x000096B3
		private void OnEnable()
		{
			this.m_PreviousLightCookieSprite = this.lightCookieSpriteInstanceID;
			Light2DManager.RegisterLight(this);
			this.UpdateCookieSpriteTexture();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000B4CD File Offset: 0x000096CD
		private void OnDisable()
		{
			Light2DManager.DeregisterLight(this);
			RTHandle cookieSpriteTexture = this.m_CookieSpriteTexture;
			if (cookieSpriteTexture == null)
			{
				return;
			}
			cookieSpriteTexture.Release();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000B4E5 File Offset: 0x000096E5
		private void LateUpdate()
		{
			if (this.m_LightType == Light2D.LightType.Global)
			{
				return;
			}
			this.UpdateMesh(this.forceUpdate);
			this.UpdateBoundingSphere();
			this.forceUpdate = false;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000B50A File Offset: 0x0000970A
		public void OnBeforeSerialize()
		{
			this.m_ComponentVersion = Light2D.ComponentVersions.Version_2;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000B514 File Offset: 0x00009714
		public void OnAfterDeserialize()
		{
			if (this.m_ComponentVersion == Light2D.ComponentVersions.Version_Unserialized)
			{
				this.m_ShadowVolumeIntensityEnabled = this.m_ShadowVolumeIntensity > 0f;
				this.m_ShadowsEnabled = this.m_ShadowIntensity > 0f;
				this.m_LightVolumeEnabled = this.m_LightVolumeIntensity > 0f;
				this.m_NormalMapQuality = ((!this.m_UseNormalMap) ? Light2D.NormalMapQuality.Disabled : this.m_NormalMapQuality);
				this.m_OverlapOperation = (this.m_AlphaBlendOnOverlap ? Light2D.OverlapOperation.AlphaBlend : this.m_OverlapOperation);
				this.m_ComponentVersion = Light2D.ComponentVersions.Version_1;
			}
			if (this.m_ComponentVersion < Light2D.ComponentVersions.Version_2)
			{
				this.m_ShadowSoftness = 0f;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000B5AB File Offset: 0x000097AB
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000B5B3 File Offset: 0x000097B3
		public float pointLightInnerAngle
		{
			get
			{
				return this.m_PointLightInnerAngle;
			}
			set
			{
				this.m_PointLightInnerAngle = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000B5BC File Offset: 0x000097BC
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000B5C4 File Offset: 0x000097C4
		public float pointLightOuterAngle
		{
			get
			{
				return this.m_PointLightOuterAngle;
			}
			set
			{
				this.m_PointLightOuterAngle = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000B5CD File Offset: 0x000097CD
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000B5D5 File Offset: 0x000097D5
		public float pointLightInnerRadius
		{
			get
			{
				return this.m_PointLightInnerRadius;
			}
			set
			{
				this.m_PointLightInnerRadius = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000B5DE File Offset: 0x000097DE
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000B5E6 File Offset: 0x000097E6
		public float pointLightOuterRadius
		{
			get
			{
				return this.m_PointLightOuterRadius;
			}
			set
			{
				this.m_PointLightOuterRadius = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000B013 File Offset: 0x00009213
		[Obsolete("pointLightDistance has been changed to normalMapDistance", true)]
		public float pointLightDistance
		{
			get
			{
				return this.m_NormalMapDistance;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000B01B File Offset: 0x0000921B
		[Obsolete("pointLightQuality has been changed to normalMapQuality", true)]
		public Light2D.NormalMapQuality pointLightQuality
		{
			get
			{
				return this.m_NormalMapQuality;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000B5EF File Offset: 0x000097EF
		internal bool isPointLight
		{
			get
			{
				return this.m_LightType == Light2D.LightType.Point;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000B5FA File Offset: 0x000097FA
		public int shapeLightParametricSides
		{
			get
			{
				return this.m_ShapeLightParametricSides;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000B602 File Offset: 0x00009802
		public float shapeLightParametricAngleOffset
		{
			get
			{
				return this.m_ShapeLightParametricAngleOffset;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000B60A File Offset: 0x0000980A
		// (set) Token: 0x06000134 RID: 308 RVA: 0x0000B612 File Offset: 0x00009812
		public float shapeLightParametricRadius
		{
			get
			{
				return this.m_ShapeLightParametricRadius;
			}
			internal set
			{
				this.m_ShapeLightParametricRadius = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000B61B File Offset: 0x0000981B
		// (set) Token: 0x06000136 RID: 310 RVA: 0x0000B623 File Offset: 0x00009823
		public float shapeLightFalloffSize
		{
			get
			{
				return this.m_ShapeLightFalloffSize;
			}
			set
			{
				this.m_ShapeLightFalloffSize = Mathf.Max(0f, value);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000B636 File Offset: 0x00009836
		// (set) Token: 0x06000138 RID: 312 RVA: 0x0000B63E File Offset: 0x0000983E
		public Vector3[] shapePath
		{
			get
			{
				return this.m_ShapePath;
			}
			internal set
			{
				this.m_ShapePath = value;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000B63E File Offset: 0x0000983E
		public void SetShapePath(Vector3[] path)
		{
			this.m_ShapePath = path;
		}

		// Token: 0x040000A5 RID: 165
		private const Light2D.ComponentVersions k_CurrentComponentVersion = Light2D.ComponentVersions.Version_2;

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		private Light2D.ComponentVersions m_ComponentVersion;

		// Token: 0x040000A7 RID: 167
		[SerializeField]
		private Light2D.LightType m_LightType = Light2D.LightType.Point;

		// Token: 0x040000A8 RID: 168
		[SerializeField]
		[FormerlySerializedAs("m_LightOperationIndex")]
		private int m_BlendStyleIndex;

		// Token: 0x040000A9 RID: 169
		[SerializeField]
		private float m_FalloffIntensity = 0.5f;

		// Token: 0x040000AA RID: 170
		[ColorUsage(true)]
		[SerializeField]
		private Color m_Color = Color.white;

		// Token: 0x040000AB RID: 171
		[SerializeField]
		private float m_Intensity = 1f;

		// Token: 0x040000AC RID: 172
		[FormerlySerializedAs("m_LightVolumeOpacity")]
		[SerializeField]
		private float m_LightVolumeIntensity = 1f;

		// Token: 0x040000AD RID: 173
		[FormerlySerializedAs("m_LightVolumeIntensityEnabled")]
		[SerializeField]
		private bool m_LightVolumeEnabled;

		// Token: 0x040000AE RID: 174
		[SerializeField]
		private int[] m_ApplyToSortingLayers;

		// Token: 0x040000AF RID: 175
		[Reload("Textures/2D/Sparkle.png", ReloadAttribute.Package.Root)]
		[SerializeField]
		private Sprite m_LightCookieSprite;

		// Token: 0x040000B0 RID: 176
		[FormerlySerializedAs("m_LightCookieSprite")]
		[SerializeField]
		private Sprite m_DeprecatedPointLightCookieSprite;

		// Token: 0x040000B1 RID: 177
		[SerializeField]
		private int m_LightOrder;

		// Token: 0x040000B2 RID: 178
		[SerializeField]
		private bool m_AlphaBlendOnOverlap;

		// Token: 0x040000B3 RID: 179
		[SerializeField]
		private Light2D.OverlapOperation m_OverlapOperation;

		// Token: 0x040000B4 RID: 180
		[FormerlySerializedAs("m_PointLightDistance")]
		[SerializeField]
		private float m_NormalMapDistance = 3f;

		// Token: 0x040000B5 RID: 181
		[FormerlySerializedAs("m_PointLightQuality")]
		[SerializeField]
		private Light2D.NormalMapQuality m_NormalMapQuality = Light2D.NormalMapQuality.Disabled;

		// Token: 0x040000B6 RID: 182
		[SerializeField]
		private bool m_UseNormalMap;

		// Token: 0x040000B7 RID: 183
		[FormerlySerializedAs("m_ShadowIntensityEnabled")]
		[SerializeField]
		private bool m_ShadowsEnabled = true;

		// Token: 0x040000B8 RID: 184
		[Range(0f, 1f)]
		[SerializeField]
		private float m_ShadowIntensity = 0.75f;

		// Token: 0x040000B9 RID: 185
		[Range(0f, 1f)]
		[SerializeField]
		private float m_ShadowSoftness = 0.3f;

		// Token: 0x040000BA RID: 186
		[Range(0f, 1f)]
		[SerializeField]
		private float m_ShadowSoftnessFalloffIntensity = 0.5f;

		// Token: 0x040000BB RID: 187
		[SerializeField]
		private bool m_ShadowVolumeIntensityEnabled;

		// Token: 0x040000BC RID: 188
		[Range(0f, 1f)]
		[SerializeField]
		private float m_ShadowVolumeIntensity = 0.75f;

		// Token: 0x040000BD RID: 189
		private Mesh m_Mesh;

		// Token: 0x040000BE RID: 190
		[NonSerialized]
		private LightUtility.LightMeshVertex[] m_Vertices = new LightUtility.LightMeshVertex[1];

		// Token: 0x040000BF RID: 191
		[NonSerialized]
		private ushort[] m_Triangles = new ushort[1];

		// Token: 0x040000C0 RID: 192
		private int m_PreviousLightCookieSprite;

		// Token: 0x040000C1 RID: 193
		internal Vector3 m_CachedPosition;

		// Token: 0x040000C2 RID: 194
		private int m_BatchSlotIndex;

		// Token: 0x040000C3 RID: 195
		internal RTHandle m_CookieSpriteTexture;

		// Token: 0x040000C4 RID: 196
		internal TextureHandle m_CookieSpriteTextureHandle;

		// Token: 0x040000C5 RID: 197
		[SerializeField]
		private Bounds m_LocalBounds;

		// Token: 0x040000C7 RID: 199
		internal bool forceUpdate;

		// Token: 0x040000C8 RID: 200
		[SerializeField]
		private float m_PointLightInnerAngle = 360f;

		// Token: 0x040000C9 RID: 201
		[SerializeField]
		private float m_PointLightOuterAngle = 360f;

		// Token: 0x040000CA RID: 202
		[SerializeField]
		private float m_PointLightInnerRadius;

		// Token: 0x040000CB RID: 203
		[SerializeField]
		private float m_PointLightOuterRadius = 1f;

		// Token: 0x040000CC RID: 204
		[SerializeField]
		private int m_ShapeLightParametricSides = 5;

		// Token: 0x040000CD RID: 205
		[SerializeField]
		private float m_ShapeLightParametricAngleOffset;

		// Token: 0x040000CE RID: 206
		[SerializeField]
		private float m_ShapeLightParametricRadius = 1f;

		// Token: 0x040000CF RID: 207
		[SerializeField]
		private float m_ShapeLightFalloffSize = 0.5f;

		// Token: 0x040000D0 RID: 208
		[SerializeField]
		private Vector2 m_ShapeLightFalloffOffset = Vector2.zero;

		// Token: 0x040000D1 RID: 209
		[SerializeField]
		private Vector3[] m_ShapePath;

		// Token: 0x040000D2 RID: 210
		private float m_PreviousShapeLightFalloffSize = -1f;

		// Token: 0x040000D3 RID: 211
		private int m_PreviousShapeLightParametricSides = -1;

		// Token: 0x040000D4 RID: 212
		private float m_PreviousShapeLightParametricAngleOffset = -1f;

		// Token: 0x040000D5 RID: 213
		private float m_PreviousShapeLightParametricRadius = -1f;

		// Token: 0x040000D6 RID: 214
		private int m_PreviousShapePathHash = -1;

		// Token: 0x040000D7 RID: 215
		private Light2D.LightType m_PreviousLightType;

		// Token: 0x02000028 RID: 40
		public enum DeprecatedLightType
		{
			// Token: 0x040000D9 RID: 217
			Parametric
		}

		// Token: 0x02000029 RID: 41
		public enum LightType
		{
			// Token: 0x040000DB RID: 219
			Parametric,
			// Token: 0x040000DC RID: 220
			Freeform,
			// Token: 0x040000DD RID: 221
			Sprite,
			// Token: 0x040000DE RID: 222
			Point,
			// Token: 0x040000DF RID: 223
			Global
		}

		// Token: 0x0200002A RID: 42
		public enum NormalMapQuality
		{
			// Token: 0x040000E1 RID: 225
			Disabled = 2,
			// Token: 0x040000E2 RID: 226
			Fast = 0,
			// Token: 0x040000E3 RID: 227
			Accurate
		}

		// Token: 0x0200002B RID: 43
		public enum OverlapOperation
		{
			// Token: 0x040000E5 RID: 229
			Additive,
			// Token: 0x040000E6 RID: 230
			AlphaBlend
		}

		// Token: 0x0200002C RID: 44
		private enum ComponentVersions
		{
			// Token: 0x040000E8 RID: 232
			Version_Unserialized,
			// Token: 0x040000E9 RID: 233
			Version_1,
			// Token: 0x040000EA RID: 234
			Version_2
		}
	}
}
