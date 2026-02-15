using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000034 RID: 52
	[ExcludeFromPreset]
	[Serializable]
	public class TMP_FontAsset : TMP_Asset
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000058F9 File Offset: 0x00003AF9
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00005901 File Offset: 0x00003B01
		public FontAssetCreationSettings creationSettings
		{
			get
			{
				return this.m_CreationSettings;
			}
			set
			{
				this.m_CreationSettings = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000590A File Offset: 0x00003B0A
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00005912 File Offset: 0x00003B12
		public Font sourceFontFile
		{
			get
			{
				return this.m_SourceFontFile;
			}
			internal set
			{
				this.m_SourceFontFile = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000591B File Offset: 0x00003B1B
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00005923 File Offset: 0x00003B23
		public AtlasPopulationMode atlasPopulationMode
		{
			get
			{
				return this.m_AtlasPopulationMode;
			}
			set
			{
				this.m_AtlasPopulationMode = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000592C File Offset: 0x00003B2C
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00005952 File Offset: 0x00003B52
		internal int familyNameHashCode
		{
			get
			{
				if (this.m_FamilyNameHashCode == 0)
				{
					this.m_FamilyNameHashCode = TMP_TextUtilities.GetHashCode(this.m_FaceInfo.familyName);
				}
				return this.m_FamilyNameHashCode;
			}
			set
			{
				this.m_FamilyNameHashCode = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000595B File Offset: 0x00003B5B
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00005981 File Offset: 0x00003B81
		internal int styleNameHashCode
		{
			get
			{
				if (this.m_StyleNameHashCode == 0)
				{
					this.m_StyleNameHashCode = TMP_TextUtilities.GetHashCode(this.m_FaceInfo.styleName);
				}
				return this.m_StyleNameHashCode;
			}
			set
			{
				this.m_StyleNameHashCode = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000598A File Offset: 0x00003B8A
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00005992 File Offset: 0x00003B92
		public List<Glyph> glyphTable
		{
			get
			{
				return this.m_GlyphTable;
			}
			internal set
			{
				this.m_GlyphTable = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000599B File Offset: 0x00003B9B
		public Dictionary<uint, Glyph> glyphLookupTable
		{
			get
			{
				if (this.m_GlyphLookupDictionary == null)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_GlyphLookupDictionary;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000059B1 File Offset: 0x00003BB1
		// (set) Token: 0x06000131 RID: 305 RVA: 0x000059B9 File Offset: 0x00003BB9
		public List<TMP_Character> characterTable
		{
			get
			{
				return this.m_CharacterTable;
			}
			internal set
			{
				this.m_CharacterTable = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000132 RID: 306 RVA: 0x000059C2 File Offset: 0x00003BC2
		public Dictionary<uint, TMP_Character> characterLookupTable
		{
			get
			{
				if (this.m_CharacterLookupDictionary == null)
				{
					this.ReadFontAssetDefinition();
				}
				return this.m_CharacterLookupDictionary;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000059D8 File Offset: 0x00003BD8
		public Texture2D atlasTexture
		{
			get
			{
				if (this.m_AtlasTexture == null)
				{
					this.m_AtlasTexture = this.atlasTextures[0];
				}
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000134 RID: 308 RVA: 0x000059FC File Offset: 0x00003BFC
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00005A0B File Offset: 0x00003C0B
		public Texture2D[] atlasTextures
		{
			get
			{
				Texture2D[] atlasTextures = this.m_AtlasTextures;
				return this.m_AtlasTextures;
			}
			set
			{
				this.m_AtlasTextures = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00005A14 File Offset: 0x00003C14
		public int atlasTextureCount
		{
			get
			{
				return this.m_AtlasTextureIndex + 1;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00005A1E File Offset: 0x00003C1E
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00005A26 File Offset: 0x00003C26
		public bool isMultiAtlasTexturesEnabled
		{
			get
			{
				return this.m_IsMultiAtlasTexturesEnabled;
			}
			set
			{
				this.m_IsMultiAtlasTexturesEnabled = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00005A2F File Offset: 0x00003C2F
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00005A37 File Offset: 0x00003C37
		public bool getFontFeatures
		{
			get
			{
				return this.m_GetFontFeatures;
			}
			set
			{
				this.m_GetFontFeatures = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00005A40 File Offset: 0x00003C40
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00005A48 File Offset: 0x00003C48
		internal bool clearDynamicDataOnBuild
		{
			get
			{
				return this.m_ClearDynamicDataOnBuild;
			}
			set
			{
				this.m_ClearDynamicDataOnBuild = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00005A51 File Offset: 0x00003C51
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00005A59 File Offset: 0x00003C59
		public int atlasWidth
		{
			get
			{
				return this.m_AtlasWidth;
			}
			internal set
			{
				this.m_AtlasWidth = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00005A62 File Offset: 0x00003C62
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00005A6A File Offset: 0x00003C6A
		public int atlasHeight
		{
			get
			{
				return this.m_AtlasHeight;
			}
			internal set
			{
				this.m_AtlasHeight = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005A73 File Offset: 0x00003C73
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00005A7B File Offset: 0x00003C7B
		public int atlasPadding
		{
			get
			{
				return this.m_AtlasPadding;
			}
			internal set
			{
				this.m_AtlasPadding = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00005A84 File Offset: 0x00003C84
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00005A8C File Offset: 0x00003C8C
		public GlyphRenderMode atlasRenderMode
		{
			get
			{
				return this.m_AtlasRenderMode;
			}
			internal set
			{
				this.m_AtlasRenderMode = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00005A95 File Offset: 0x00003C95
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00005A9D File Offset: 0x00003C9D
		internal List<GlyphRect> usedGlyphRects
		{
			get
			{
				return this.m_UsedGlyphRects;
			}
			set
			{
				this.m_UsedGlyphRects = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00005AA6 File Offset: 0x00003CA6
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00005AAE File Offset: 0x00003CAE
		internal List<GlyphRect> freeGlyphRects
		{
			get
			{
				return this.m_FreeGlyphRects;
			}
			set
			{
				this.m_FreeGlyphRects = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00005AB7 File Offset: 0x00003CB7
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00005ABF File Offset: 0x00003CBF
		public TMP_FontFeatureTable fontFeatureTable
		{
			get
			{
				return this.m_FontFeatureTable;
			}
			internal set
			{
				this.m_FontFeatureTable = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00005AC8 File Offset: 0x00003CC8
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00005AD0 File Offset: 0x00003CD0
		public List<TMP_FontAsset> fallbackFontAssetTable
		{
			get
			{
				return this.m_FallbackFontAssetTable;
			}
			set
			{
				this.m_FallbackFontAssetTable = value;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005AD9 File Offset: 0x00003CD9
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00005AE1 File Offset: 0x00003CE1
		public TMP_FontWeightPair[] fontWeightTable
		{
			get
			{
				return this.m_FontWeightTable;
			}
			internal set
			{
				this.m_FontWeightTable = value;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600014F RID: 335 RVA: 0x00005AEA File Offset: 0x00003CEA
		[Obsolete("The fontInfo property and underlying type is now obsolete. Please use the faceInfo property and FaceInfo type instead.")]
		public FaceInfo_Legacy fontInfo
		{
			get
			{
				return this.m_fontInfo;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public static TMP_FontAsset CreateFontAsset(string familyName, string styleName, int pointSize = 90)
		{
			FontReference fontRef;
			if (FontEngine.TryGetSystemFontReference(familyName, styleName, out fontRef))
			{
				return TMP_FontAsset.CreateFontAsset(fontRef.filePath, fontRef.faceIndex, pointSize, 9, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.DynamicOS, true);
			}
			Debug.Log(string.Concat(new string[] { "Unable to find a font file with the specified Family Name [", familyName, "] and Style [", styleName, "]." }));
			return null;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00005B64 File Offset: 0x00003D64
		public static TMP_FontAsset CreateFontAsset(string fontFilePath, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight)
		{
			return TMP_FontAsset.CreateFontAsset(fontFilePath, faceIndex, samplingPointSize, atlasPadding, renderMode, atlasWidth, atlasHeight, AtlasPopulationMode.Dynamic, true);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00005B82 File Offset: 0x00003D82
		private static TMP_FontAsset CreateFontAsset(string fontFilePath, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode, bool enableMultiAtlasSupport = true)
		{
			if (FontEngine.LoadFontFace(fontFilePath, (float)samplingPointSize, faceIndex) != FontEngineError.Success)
			{
				Debug.Log("Unable to load font face from [" + fontFilePath + "].");
				return null;
			}
			TMP_FontAsset tmp_FontAsset = TMP_FontAsset.CreateFontAssetInstance(null, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
			tmp_FontAsset.m_SourceFontFilePath = fontFilePath;
			return tmp_FontAsset;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005BBE File Offset: 0x00003DBE
		public static TMP_FontAsset CreateFontAsset(Font font)
		{
			return TMP_FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.Dynamic, true);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005BDC File Offset: 0x00003DDC
		public static TMP_FontAsset CreateFontAsset(Font font, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
		{
			return TMP_FontAsset.CreateFontAsset(font, 0, samplingPointSize, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005BFB File Offset: 0x00003DFB
		private static TMP_FontAsset CreateFontAsset(Font font, int faceIndex, int samplingPointSize, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode = AtlasPopulationMode.Dynamic, bool enableMultiAtlasSupport = true)
		{
			if (FontEngine.LoadFontFace(font, (float)samplingPointSize, faceIndex) != FontEngineError.Success)
			{
				Debug.LogWarning("Unable to load font face for [" + font.name + "]. Make sure \"Include Font Data\" is enabled in the Font Import Settings.", font);
				return null;
			}
			return TMP_FontAsset.CreateFontAssetInstance(font, atlasPadding, renderMode, atlasWidth, atlasHeight, atlasPopulationMode, enableMultiAtlasSupport);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005C38 File Offset: 0x00003E38
		private static TMP_FontAsset CreateFontAssetInstance(Font font, int atlasPadding, GlyphRenderMode renderMode, int atlasWidth, int atlasHeight, AtlasPopulationMode atlasPopulationMode, bool enableMultiAtlasSupport)
		{
			TMP_FontAsset fontAsset = ScriptableObject.CreateInstance<TMP_FontAsset>();
			fontAsset.m_Version = "1.1.0";
			fontAsset.faceInfo = FontEngine.GetFaceInfo();
			if (atlasPopulationMode == AtlasPopulationMode.Dynamic && font != null)
			{
				fontAsset.sourceFontFile = font;
			}
			fontAsset.atlasPopulationMode = atlasPopulationMode;
			fontAsset.clearDynamicDataOnBuild = TMP_Settings.clearDynamicDataOnBuild;
			fontAsset.atlasWidth = atlasWidth;
			fontAsset.atlasHeight = atlasHeight;
			fontAsset.atlasPadding = atlasPadding;
			fontAsset.atlasRenderMode = renderMode;
			fontAsset.atlasTextures = new Texture2D[1];
			TextureFormat texFormat = (((renderMode & (GlyphRenderMode)65536) == (GlyphRenderMode)65536) ? TextureFormat.RGBA32 : TextureFormat.Alpha8);
			Texture2D texture = new Texture2D(1, 1, texFormat, false);
			fontAsset.atlasTextures[0] = texture;
			fontAsset.isMultiAtlasTexturesEnabled = enableMultiAtlasSupport;
			int packingModifier;
			if ((renderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16)
			{
				packingModifier = 0;
				Material tmp_material;
				if (texFormat == TextureFormat.Alpha8)
				{
					tmp_material = new Material(ShaderUtilities.ShaderRef_MobileBitmap);
				}
				else
				{
					tmp_material = new Material(Shader.Find("TextMeshPro/Sprite"));
				}
				tmp_material.SetTexture(ShaderUtilities.ID_MainTex, texture);
				tmp_material.SetFloat(ShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				tmp_material.SetFloat(ShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				fontAsset.material = tmp_material;
			}
			else
			{
				packingModifier = 1;
				Material tmp_material2 = new Material(ShaderUtilities.ShaderRef_MobileSDF);
				tmp_material2.SetTexture(ShaderUtilities.ID_MainTex, texture);
				tmp_material2.SetFloat(ShaderUtilities.ID_TextureWidth, (float)atlasWidth);
				tmp_material2.SetFloat(ShaderUtilities.ID_TextureHeight, (float)atlasHeight);
				tmp_material2.SetFloat(ShaderUtilities.ID_GradientScale, (float)(atlasPadding + packingModifier));
				tmp_material2.SetFloat(ShaderUtilities.ID_WeightNormal, fontAsset.normalStyle);
				tmp_material2.SetFloat(ShaderUtilities.ID_WeightBold, fontAsset.boldStyle);
				fontAsset.material = tmp_material2;
			}
			fontAsset.freeGlyphRects = new List<GlyphRect>(8)
			{
				new GlyphRect(0, 0, atlasWidth - packingModifier, atlasHeight - packingModifier)
			};
			fontAsset.usedGlyphRects = new List<GlyphRect>(8);
			fontAsset.ReadFontAssetDefinition();
			return fontAsset;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00005DEB File Offset: 0x00003FEB
		private void OnDestroy()
		{
			this.DestroyAtlasTextures();
			global::UnityEngine.Object.DestroyImmediate(this.m_Material);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005E00 File Offset: 0x00004000
		public void ReadFontAssetDefinition()
		{
			this.InitializeDictionaryLookupTables();
			this.AddSynthesizedCharactersAndFaceMetrics();
			if (this.m_FaceInfo.capLine == 0f && this.m_CharacterLookupDictionary.ContainsKey(88U))
			{
				uint glyphIndex = this.m_CharacterLookupDictionary[88U].glyphIndex;
				this.m_FaceInfo.capLine = this.m_GlyphLookupDictionary[glyphIndex].metrics.horizontalBearingY;
			}
			if (this.m_FaceInfo.meanLine == 0f && this.m_CharacterLookupDictionary.ContainsKey(120U))
			{
				uint glyphIndex2 = this.m_CharacterLookupDictionary[120U].glyphIndex;
				this.m_FaceInfo.meanLine = this.m_GlyphLookupDictionary[glyphIndex2].metrics.horizontalBearingY;
			}
			if (this.m_FaceInfo.scale == 0f)
			{
				this.m_FaceInfo.scale = 1f;
			}
			if (this.m_FaceInfo.strikethroughOffset == 0f)
			{
				this.m_FaceInfo.strikethroughOffset = this.m_FaceInfo.capLine / 2.5f;
			}
			if (this.m_AtlasPadding == 0 && base.material.HasProperty(ShaderUtilities.ID_GradientScale))
			{
				this.m_AtlasPadding = (int)base.material.GetFloat(ShaderUtilities.ID_GradientScale) - 1;
			}
			if (this.m_FaceInfo.unitsPerEM == 0)
			{
				this.m_FaceInfo.unitsPerEM = FontEngine.GetFaceInfo().unitsPerEM;
			}
			base.hashCode = TMP_TextUtilities.GetHashCode(base.name);
			this.familyNameHashCode = TMP_TextUtilities.GetHashCode(this.m_FaceInfo.familyName);
			this.styleNameHashCode = TMP_TextUtilities.GetHashCode(this.m_FaceInfo.styleName);
			base.materialHashCode = TMP_TextUtilities.GetSimpleHashCode(base.name + TMP_FontAsset.s_DefaultMaterialSuffix);
			TMP_ResourceManager.AddFontAsset(this);
			this.IsFontAssetLookupTablesDirty = false;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005FD8 File Offset: 0x000041D8
		internal void InitializeDictionaryLookupTables()
		{
			this.InitializeGlyphLookupDictionary();
			this.InitializeCharacterLookupDictionary();
			if ((this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS) && this.m_ShouldReimportFontFeatures)
			{
				this.ImportFontFeatures();
			}
			this.InitializeLigatureSubstitutionLookupDictionary();
			this.InitializeGlyphPaidAdjustmentRecordsLookupDictionary();
			this.InitializeMarkToBaseAdjustmentRecordsLookupDictionary();
			this.InitializeMarkToMarkAdjustmentRecordsLookupDictionary();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000602C File Offset: 0x0000422C
		internal void InitializeGlyphLookupDictionary()
		{
			if (this.m_GlyphLookupDictionary == null)
			{
				this.m_GlyphLookupDictionary = new Dictionary<uint, Glyph>();
			}
			else
			{
				this.m_GlyphLookupDictionary.Clear();
			}
			if (this.m_GlyphIndexList == null)
			{
				this.m_GlyphIndexList = new List<uint>();
			}
			else
			{
				this.m_GlyphIndexList.Clear();
			}
			if (this.m_GlyphIndexListNewlyAdded == null)
			{
				this.m_GlyphIndexListNewlyAdded = new List<uint>();
			}
			else
			{
				this.m_GlyphIndexListNewlyAdded.Clear();
			}
			int glyphCount = this.m_GlyphTable.Count;
			for (int i = 0; i < glyphCount; i++)
			{
				Glyph glyph = this.m_GlyphTable[i];
				uint index = glyph.index;
				if (!this.m_GlyphLookupDictionary.ContainsKey(index))
				{
					this.m_GlyphLookupDictionary.Add(index, glyph);
					this.m_GlyphIndexList.Add(index);
				}
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000060EC File Offset: 0x000042EC
		internal void InitializeCharacterLookupDictionary()
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				this.m_CharacterLookupDictionary = new Dictionary<uint, TMP_Character>();
			}
			else
			{
				this.m_CharacterLookupDictionary.Clear();
			}
			for (int i = 0; i < this.m_CharacterTable.Count; i++)
			{
				TMP_Character character = this.m_CharacterTable[i];
				uint unicode = character.unicode;
				uint glyphIndex = character.glyphIndex;
				if (!this.m_CharacterLookupDictionary.ContainsKey(unicode))
				{
					this.m_CharacterLookupDictionary.Add(unicode, character);
					character.textAsset = this;
					character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
				}
			}
			if (this.m_MissingUnicodesFromFontFile != null)
			{
				this.m_MissingUnicodesFromFontFile.Clear();
			}
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00006194 File Offset: 0x00004394
		internal void InitializeLigatureSubstitutionLookupDictionary()
		{
			if (this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup == null)
			{
				this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup = new Dictionary<uint, List<LigatureSubstitutionRecord>>();
			}
			else
			{
				this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.Clear();
			}
			List<LigatureSubstitutionRecord> substitutionRecords = this.m_FontFeatureTable.m_LigatureSubstitutionRecords;
			if (substitutionRecords != null)
			{
				for (int i = 0; i < substitutionRecords.Count; i++)
				{
					LigatureSubstitutionRecord record = substitutionRecords[i];
					if (record.componentGlyphIDs != null && record.componentGlyphIDs.Length != 0)
					{
						uint keyGlyphIndex = record.componentGlyphIDs[0];
						if (!this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.ContainsKey(keyGlyphIndex))
						{
							this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.Add(keyGlyphIndex, new List<LigatureSubstitutionRecord> { record });
						}
						else
						{
							this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup[keyGlyphIndex].Add(record);
						}
					}
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006260 File Offset: 0x00004460
		internal void InitializeGlyphPaidAdjustmentRecordsLookupDictionary()
		{
			if (this.m_KerningTable != null && this.m_KerningTable.kerningPairs != null && this.m_KerningTable.kerningPairs.Count > 0)
			{
				this.UpgradeGlyphAdjustmentTableToFontFeatureTable();
			}
			if (this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup == null)
			{
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup = new Dictionary<uint, GlyphPairAdjustmentRecord>();
			}
			else
			{
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Clear();
			}
			List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords = this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords;
			if (glyphPairAdjustmentRecords != null)
			{
				for (int i = 0; i < glyphPairAdjustmentRecords.Count; i++)
				{
					GlyphPairAdjustmentRecord record = glyphPairAdjustmentRecords[i];
					uint key = (record.secondAdjustmentRecord.glyphIndex << 16) | record.firstAdjustmentRecord.glyphIndex;
					if (!this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(key))
					{
						this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(key, record);
					}
				}
			}
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000633C File Offset: 0x0000453C
		internal void InitializeMarkToBaseAdjustmentRecordsLookupDictionary()
		{
			if (this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup == null)
			{
				this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup = new Dictionary<uint, MarkToBaseAdjustmentRecord>();
			}
			else
			{
				this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.Clear();
			}
			List<MarkToBaseAdjustmentRecord> adjustmentRecords = this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecords;
			if (adjustmentRecords != null)
			{
				for (int i = 0; i < adjustmentRecords.Count; i++)
				{
					MarkToBaseAdjustmentRecord record = adjustmentRecords[i];
					uint key = (record.markGlyphID << 16) | record.baseGlyphID;
					if (!this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.ContainsKey(key))
					{
						this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.Add(key, record);
					}
				}
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000063D8 File Offset: 0x000045D8
		internal void InitializeMarkToMarkAdjustmentRecordsLookupDictionary()
		{
			if (this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup == null)
			{
				this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup = new Dictionary<uint, MarkToMarkAdjustmentRecord>();
			}
			else
			{
				this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.Clear();
			}
			List<MarkToMarkAdjustmentRecord> adjustmentRecords = this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecords;
			if (adjustmentRecords != null)
			{
				for (int i = 0; i < adjustmentRecords.Count; i++)
				{
					MarkToMarkAdjustmentRecord record = adjustmentRecords[i];
					uint key = (record.combiningMarkGlyphID << 16) | record.baseMarkGlyphID;
					if (!this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.ContainsKey(key))
					{
						this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.Add(key, record);
					}
				}
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00006474 File Offset: 0x00004674
		internal void AddSynthesizedCharactersAndFaceMetrics()
		{
			bool isFontFaceLoaded = false;
			if (this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS)
			{
				isFontFaceLoaded = this.LoadFontFace() == FontEngineError.Success;
				if (!isFontFaceLoaded && !this.InternalDynamicOS && TMP_Settings.warningsDisabled)
				{
					Debug.LogWarning("Unable to load font face for [" + base.name + "] font asset.", this);
				}
			}
			this.AddSynthesizedCharacter(3U, isFontFaceLoaded, true);
			this.AddSynthesizedCharacter(9U, isFontFaceLoaded, true);
			this.AddSynthesizedCharacter(10U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(11U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(13U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(1564U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(8203U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(8206U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(8207U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(8232U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(8233U, isFontFaceLoaded, false);
			this.AddSynthesizedCharacter(8288U, isFontFaceLoaded, false);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00006558 File Offset: 0x00004758
		private void AddSynthesizedCharacter(uint unicode, bool isFontFaceLoaded, bool addImmediately = false)
		{
			if (this.m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return;
			}
			Glyph glyph;
			if (!isFontFaceLoaded || FontEngine.GetGlyphIndex(unicode) == 0U)
			{
				glyph = new Glyph(0U, new GlyphMetrics(0f, 0f, 0f, 0f, 0f), GlyphRect.zero, 1f, 0);
				this.m_CharacterLookupDictionary.Add(unicode, new TMP_Character(unicode, this, glyph));
				return;
			}
			if (!addImmediately)
			{
				return;
			}
			GlyphLoadFlags glyphLoadFlags = (((this.m_AtlasRenderMode & (GlyphRenderMode)4) == (GlyphRenderMode)4) ? (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_NO_BITMAP) : GlyphLoadFlags.LOAD_NO_BITMAP);
			if (FontEngine.TryGetGlyphWithUnicodeValue(unicode, glyphLoadFlags, out glyph))
			{
				this.m_CharacterLookupDictionary.Add(unicode, new TMP_Character(unicode, this, glyph));
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x000065F8 File Offset: 0x000047F8
		internal void AddCharacterToLookupCache(uint unicode, TMP_Character character)
		{
			this.m_CharacterLookupDictionary.Add(unicode, character);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00006608 File Offset: 0x00004808
		private FontEngineError LoadFontFace()
		{
			if (this.m_AtlasPopulationMode != AtlasPopulationMode.Dynamic)
			{
				return FontEngine.LoadFontFace(this.m_FaceInfo.familyName, this.m_FaceInfo.styleName, this.m_FaceInfo.pointSize);
			}
			if (FontEngine.LoadFontFace(this.m_SourceFontFile, this.m_FaceInfo.pointSize, this.m_FaceInfo.faceIndex) == FontEngineError.Success)
			{
				return FontEngineError.Success;
			}
			if (!string.IsNullOrEmpty(this.m_SourceFontFilePath))
			{
				return FontEngine.LoadFontFace(this.m_SourceFontFilePath, this.m_FaceInfo.pointSize, this.m_FaceInfo.faceIndex);
			}
			return FontEngineError.Invalid_Face;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000669C File Offset: 0x0000489C
		internal void SortCharacterTable()
		{
			if (this.m_CharacterTable != null && this.m_CharacterTable.Count > 0)
			{
				this.m_CharacterTable = this.m_CharacterTable.OrderBy((TMP_Character c) => c.unicode).ToList<TMP_Character>();
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000066F4 File Offset: 0x000048F4
		internal void SortGlyphTable()
		{
			if (this.m_GlyphTable != null && this.m_GlyphTable.Count > 0)
			{
				this.m_GlyphTable = this.m_GlyphTable.OrderBy((Glyph c) => c.index).ToList<Glyph>();
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000674C File Offset: 0x0000494C
		internal void SortFontFeatureTable()
		{
			this.m_FontFeatureTable.SortGlyphPairAdjustmentRecords();
			this.m_FontFeatureTable.SortMarkToBaseAdjustmentRecords();
			this.m_FontFeatureTable.SortMarkToMarkAdjustmentRecords();
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000676F File Offset: 0x0000496F
		internal void SortAllTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
			this.SortFontFeatureTable();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006783 File Offset: 0x00004983
		public bool HasCharacter(int character)
		{
			return this.characterLookupTable != null && this.m_CharacterLookupDictionary.ContainsKey((uint)character);
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000679C File Offset: 0x0000499C
		public bool HasCharacter(char character, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			if (this.characterLookupTable == null)
			{
				return false;
			}
			if (this.m_CharacterLookupDictionary.ContainsKey((uint)character))
			{
				return true;
			}
			TMP_Character returnedCharacter;
			if (tryAddCharacter && (this.m_AtlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS) && this.TryAddCharacterInternal((uint)character, out returnedCharacter))
			{
				return true;
			}
			if (searchFallbacks)
			{
				if (TMP_FontAsset.k_SearchedFontAssetLookup == null)
				{
					TMP_FontAsset.k_SearchedFontAssetLookup = new HashSet<int>();
				}
				else
				{
					TMP_FontAsset.k_SearchedFontAssetLookup.Clear();
				}
				TMP_FontAsset.k_SearchedFontAssetLookup.Add(base.GetInstanceID());
				if (this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0)
				{
					int i = 0;
					while (i < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[i] != null)
					{
						TMP_FontAsset fallback = this.fallbackFontAssetTable[i];
						int fallbackID = fallback.GetInstanceID();
						if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(fallbackID) && fallback.HasCharacter_Internal((uint)character, true, tryAddCharacter))
						{
							return true;
						}
						i++;
					}
				}
				if (TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
				{
					int j = 0;
					while (j < TMP_Settings.fallbackFontAssets.Count && TMP_Settings.fallbackFontAssets[j] != null)
					{
						TMP_FontAsset fallback2 = TMP_Settings.fallbackFontAssets[j];
						int fallbackID2 = fallback2.GetInstanceID();
						if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(fallbackID2) && fallback2.HasCharacter_Internal((uint)character, true, tryAddCharacter))
						{
							return true;
						}
						j++;
					}
				}
				if (TMP_Settings.defaultFontAsset != null)
				{
					TMP_FontAsset fallback3 = TMP_Settings.defaultFontAsset;
					int fallbackID3 = fallback3.GetInstanceID();
					if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(fallbackID3) && fallback3.HasCharacter_Internal((uint)character, true, tryAddCharacter))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006938 File Offset: 0x00004B38
		private bool HasCharacter_Internal(uint character, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			if (this.m_CharacterLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
				if (this.m_CharacterLookupDictionary == null)
				{
					return false;
				}
			}
			if (this.m_CharacterLookupDictionary.ContainsKey(character))
			{
				return true;
			}
			TMP_Character returnedCharacter;
			if (tryAddCharacter && (this.atlasPopulationMode == AtlasPopulationMode.Dynamic || this.m_AtlasPopulationMode == AtlasPopulationMode.DynamicOS) && this.TryAddCharacterInternal(character, out returnedCharacter))
			{
				return true;
			}
			if (searchFallbacks)
			{
				if (this.fallbackFontAssetTable == null || this.fallbackFontAssetTable.Count == 0)
				{
					return false;
				}
				int i = 0;
				while (i < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[i] != null)
				{
					TMP_FontAsset fallback = this.fallbackFontAssetTable[i];
					int fallbackID = fallback.GetInstanceID();
					if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(fallbackID) && fallback.HasCharacter_Internal(character, true, tryAddCharacter))
					{
						return true;
					}
					i++;
				}
			}
			return false;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006A04 File Offset: 0x00004C04
		public bool HasCharacters(string text, out List<char> missingCharacters)
		{
			if (this.characterLookupTable == null)
			{
				missingCharacters = null;
				return false;
			}
			missingCharacters = new List<char>();
			for (int i = 0; i < text.Length; i++)
			{
				if (!this.m_CharacterLookupDictionary.ContainsKey((uint)text[i]))
				{
					missingCharacters.Add(text[i]);
				}
			}
			return missingCharacters.Count == 0;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00006A64 File Offset: 0x00004C64
		public bool HasCharacters(string text, out uint[] missingCharacters, bool searchFallbacks = false, bool tryAddCharacter = false)
		{
			missingCharacters = null;
			if (this.characterLookupTable == null)
			{
				return false;
			}
			this.s_MissingCharacterList.Clear();
			for (int i = 0; i < text.Length; i++)
			{
				bool isMissingCharacter = true;
				uint character = (uint)text[i];
				TMP_Character returnedCharacter;
				if (!this.m_CharacterLookupDictionary.ContainsKey(character) && (!tryAddCharacter || (this.atlasPopulationMode != AtlasPopulationMode.Dynamic && this.m_AtlasPopulationMode != AtlasPopulationMode.DynamicOS) || !this.TryAddCharacterInternal(character, out returnedCharacter)))
				{
					if (searchFallbacks)
					{
						if (TMP_FontAsset.k_SearchedFontAssetLookup == null)
						{
							TMP_FontAsset.k_SearchedFontAssetLookup = new HashSet<int>();
						}
						else
						{
							TMP_FontAsset.k_SearchedFontAssetLookup.Clear();
						}
						TMP_FontAsset.k_SearchedFontAssetLookup.Add(base.GetInstanceID());
						if (this.fallbackFontAssetTable != null && this.fallbackFontAssetTable.Count > 0)
						{
							int j = 0;
							while (j < this.fallbackFontAssetTable.Count && this.fallbackFontAssetTable[j] != null)
							{
								TMP_FontAsset fallback = this.fallbackFontAssetTable[j];
								int fallbackID = fallback.GetInstanceID();
								if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(fallbackID) && fallback.HasCharacter_Internal(character, true, tryAddCharacter))
								{
									isMissingCharacter = false;
									break;
								}
								j++;
							}
						}
						if (isMissingCharacter && TMP_Settings.fallbackFontAssets != null && TMP_Settings.fallbackFontAssets.Count > 0)
						{
							int k = 0;
							while (k < TMP_Settings.fallbackFontAssets.Count && TMP_Settings.fallbackFontAssets[k] != null)
							{
								TMP_FontAsset fallback2 = TMP_Settings.fallbackFontAssets[k];
								int fallbackID2 = fallback2.GetInstanceID();
								if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(fallbackID2) && fallback2.HasCharacter_Internal(character, true, tryAddCharacter))
								{
									isMissingCharacter = false;
									break;
								}
								k++;
							}
						}
						if (isMissingCharacter && TMP_Settings.defaultFontAsset != null)
						{
							TMP_FontAsset fallback3 = TMP_Settings.defaultFontAsset;
							int fallbackID3 = fallback3.GetInstanceID();
							if (TMP_FontAsset.k_SearchedFontAssetLookup.Add(fallbackID3) && fallback3.HasCharacter_Internal(character, true, tryAddCharacter))
							{
								isMissingCharacter = false;
							}
						}
					}
					if (isMissingCharacter)
					{
						this.s_MissingCharacterList.Add(character);
					}
				}
			}
			if (this.s_MissingCharacterList.Count > 0)
			{
				missingCharacters = this.s_MissingCharacterList.ToArray();
				return false;
			}
			return true;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00006C74 File Offset: 0x00004E74
		public bool HasCharacters(string text)
		{
			if (this.characterLookupTable == null)
			{
				return false;
			}
			for (int i = 0; i < text.Length; i++)
			{
				if (!this.m_CharacterLookupDictionary.ContainsKey((uint)text[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00006CB4 File Offset: 0x00004EB4
		public static string GetCharacters(TMP_FontAsset fontAsset)
		{
			string characters = string.Empty;
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				characters += ((char)fontAsset.characterTable[i].unicode).ToString();
			}
			return characters;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00006D00 File Offset: 0x00004F00
		public static int[] GetCharactersArray(TMP_FontAsset fontAsset)
		{
			int[] characters = new int[fontAsset.characterTable.Count];
			for (int i = 0; i < fontAsset.characterTable.Count; i++)
			{
				characters[i] = (int)fontAsset.characterTable[i].unicode;
			}
			return characters;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00006D49 File Offset: 0x00004F49
		internal uint GetGlyphIndex(uint unicode)
		{
			if (this.m_CharacterLookupDictionary.ContainsKey(unicode))
			{
				return this.m_CharacterLookupDictionary[unicode].glyphIndex;
			}
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				return 0U;
			}
			return FontEngine.GetGlyphIndex(unicode);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006D7B File Offset: 0x00004F7B
		internal uint GetGlyphVariantIndex(uint unicode, uint variantSelectorUnicode)
		{
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				return 0U;
			}
			return FontEngine.GetVariantGlyphIndex(unicode, variantSelectorUnicode);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006D90 File Offset: 0x00004F90
		internal static void RegisterFontAssetForFontFeatureUpdate(TMP_FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			if (TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueueLookup.Add(instanceID))
			{
				TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Add(fontAsset);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006DBC File Offset: 0x00004FBC
		internal static void UpdateFontFeaturesForFontAssetsInQueue()
		{
			int count = TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Count;
			for (int i = 0; i < count; i++)
			{
				TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueue[i].UpdateGPOSFontFeaturesForNewlyAddedGlyphs();
			}
			if (count > 0)
			{
				TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueue.Clear();
				TMP_FontAsset.k_FontAssets_FontFeaturesUpdateQueueLookup.Clear();
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006E08 File Offset: 0x00005008
		internal static void RegisterAtlasTextureForApply(Texture2D texture)
		{
			int instanceID = texture.GetInstanceID();
			if (TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Add(instanceID))
			{
				TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueue.Add(texture);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00006E34 File Offset: 0x00005034
		internal static void UpdateAtlasTexturesInQueue()
		{
			int count = TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Count;
			for (int i = 0; i < count; i++)
			{
				TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueue[i].Apply(false, false);
			}
			if (count > 0)
			{
				TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueue.Clear();
				TMP_FontAsset.k_FontAssets_AtlasTexturesUpdateQueueLookup.Clear();
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00006E82 File Offset: 0x00005082
		internal static void UpdateFontAssetsInUpdateQueue()
		{
			TMP_FontAsset.UpdateAtlasTexturesInQueue();
			TMP_FontAsset.UpdateFontFeaturesForFontAssetsInQueue();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006E90 File Offset: 0x00005090
		public bool TryAddCharacters(uint[] unicodes, bool includeFontFeatures = false)
		{
			uint[] missingUnicodes;
			return this.TryAddCharacters(unicodes, out missingUnicodes, includeFontFeatures);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006EA8 File Offset: 0x000050A8
		public bool TryAddCharacters(uint[] unicodes, out uint[] missingUnicodes, bool includeFontFeatures = false)
		{
			if (unicodes == null || unicodes.Length == 0 || this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				if (this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided Unicode list is Null or Empty.", this);
				}
				missingUnicodes = null;
				return false;
			}
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				missingUnicodes = unicodes.ToArray<uint>();
				return false;
			}
			if (this.m_CharacterLookupDictionary == null || this.m_GlyphLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
			}
			this.m_GlyphsToAdd.Clear();
			this.m_GlyphsToAddLookup.Clear();
			this.m_CharactersToAdd.Clear();
			this.m_CharactersToAddLookup.Clear();
			this.s_MissingCharacterList.Clear();
			bool isMissingCharacters = false;
			int unicodeCount = unicodes.Length;
			for (int i = 0; i < unicodeCount; i++)
			{
				uint unicode = unicodes[i];
				if (!this.m_CharacterLookupDictionary.ContainsKey(unicode))
				{
					uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
					if (glyphIndex == 0U)
					{
						if (unicode != 160U)
						{
							if (unicode == 173U || unicode == 8209U)
							{
								glyphIndex = FontEngine.GetGlyphIndex(45U);
							}
						}
						else
						{
							glyphIndex = FontEngine.GetGlyphIndex(32U);
						}
						if (glyphIndex == 0U)
						{
							this.s_MissingCharacterList.Add(unicode);
							isMissingCharacters = true;
							goto IL_01BB;
						}
					}
					TMP_Character character = new TMP_Character(unicode, glyphIndex);
					if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
					{
						character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
						character.textAsset = this;
						this.m_CharacterTable.Add(character);
						this.m_CharacterLookupDictionary.Add(unicode, character);
					}
					else
					{
						if (this.m_GlyphsToAddLookup.Add(glyphIndex))
						{
							this.m_GlyphsToAdd.Add(glyphIndex);
						}
						if (this.m_CharactersToAddLookup.Add(unicode))
						{
							this.m_CharactersToAdd.Add(character);
						}
					}
				}
				IL_01BB:;
			}
			if (this.m_GlyphsToAdd.Count == 0)
			{
				missingUnicodes = unicodes;
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width <= 1 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height <= 1)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			Glyph[] glyphs;
			bool allGlyphsAddedToTexture = FontEngine.TryAddGlyphsToTexture(this.m_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyphs);
			int j = 0;
			while (j < glyphs.Length && glyphs[j] != null)
			{
				Glyph glyph = glyphs[j];
				uint glyphIndex2 = glyph.index;
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyphIndex2, glyph);
				this.m_GlyphIndexListNewlyAdded.Add(glyphIndex2);
				this.m_GlyphIndexList.Add(glyphIndex2);
				j++;
			}
			this.m_GlyphsToAdd.Clear();
			for (int k = 0; k < this.m_CharactersToAdd.Count; k++)
			{
				TMP_Character character2 = this.m_CharactersToAdd[k];
				Glyph glyph2;
				if (!this.m_GlyphLookupDictionary.TryGetValue(character2.glyphIndex, out glyph2))
				{
					this.m_GlyphsToAdd.Add(character2.glyphIndex);
				}
				else
				{
					character2.glyph = glyph2;
					character2.textAsset = this;
					this.m_CharacterTable.Add(character2);
					this.m_CharacterLookupDictionary.Add(character2.unicode, character2);
					this.m_CharactersToAdd.RemoveAt(k);
					k--;
				}
			}
			if (this.m_IsMultiAtlasTexturesEnabled && !allGlyphsAddedToTexture)
			{
				while (!allGlyphsAddedToTexture)
				{
					allGlyphsAddedToTexture = this.TryAddGlyphsToNewAtlasTexture();
				}
			}
			if (includeFontFeatures)
			{
				this.UpdateFontFeaturesForNewlyAddedGlyphs();
			}
			for (int l = 0; l < this.m_CharactersToAdd.Count; l++)
			{
				TMP_Character character3 = this.m_CharactersToAdd[l];
				this.s_MissingCharacterList.Add(character3.unicode);
			}
			missingUnicodes = null;
			if (this.s_MissingCharacterList.Count > 0)
			{
				missingUnicodes = this.s_MissingCharacterList.ToArray();
			}
			return allGlyphsAddedToTexture && !isMissingCharacters;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000072B4 File Offset: 0x000054B4
		public bool TryAddCharacters(string characters, bool includeFontFeatures = false)
		{
			string missingCharacters;
			return this.TryAddCharacters(characters, out missingCharacters, includeFontFeatures);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000072CC File Offset: 0x000054CC
		public bool TryAddCharacters(string characters, out string missingCharacters, bool includeFontFeatures = false)
		{
			if (string.IsNullOrEmpty(characters) || this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				if (this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because its AtlasPopulationMode is set to Static.", this);
				}
				else
				{
					Debug.LogWarning("Unable to add characters to font asset [" + base.name + "] because the provided character list is Null or Empty.", this);
				}
				missingCharacters = characters;
				return false;
			}
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				missingCharacters = characters;
				return false;
			}
			if (this.m_CharacterLookupDictionary == null || this.m_GlyphLookupDictionary == null)
			{
				this.ReadFontAssetDefinition();
			}
			this.m_GlyphsToAdd.Clear();
			this.m_GlyphsToAddLookup.Clear();
			this.m_CharactersToAdd.Clear();
			this.m_CharactersToAddLookup.Clear();
			this.s_MissingCharacterList.Clear();
			bool isMissingCharacters = false;
			int characterCount = characters.Length;
			for (int i = 0; i < characterCount; i++)
			{
				uint unicode = (uint)characters[i];
				if (!this.m_CharacterLookupDictionary.ContainsKey(unicode))
				{
					uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
					if (glyphIndex == 0U)
					{
						if (unicode != 160U)
						{
							if (unicode == 173U || unicode == 8209U)
							{
								glyphIndex = FontEngine.GetGlyphIndex(45U);
							}
						}
						else
						{
							glyphIndex = FontEngine.GetGlyphIndex(32U);
						}
						if (glyphIndex == 0U)
						{
							this.s_MissingCharacterList.Add(unicode);
							isMissingCharacters = true;
							goto IL_01BE;
						}
					}
					TMP_Character character = new TMP_Character(unicode, glyphIndex);
					if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
					{
						character.glyph = this.m_GlyphLookupDictionary[glyphIndex];
						character.textAsset = this;
						this.m_CharacterTable.Add(character);
						this.m_CharacterLookupDictionary.Add(unicode, character);
					}
					else
					{
						if (this.m_GlyphsToAddLookup.Add(glyphIndex))
						{
							this.m_GlyphsToAdd.Add(glyphIndex);
						}
						if (this.m_CharactersToAddLookup.Add(unicode))
						{
							this.m_CharactersToAdd.Add(character);
						}
					}
				}
				IL_01BE:;
			}
			if (this.m_GlyphsToAdd.Count == 0)
			{
				missingCharacters = characters;
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width <= 1 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height <= 1)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			Glyph[] glyphs;
			bool allGlyphsAddedToTexture = FontEngine.TryAddGlyphsToTexture(this.m_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyphs);
			int j = 0;
			while (j < glyphs.Length && glyphs[j] != null)
			{
				Glyph glyph = glyphs[j];
				uint glyphIndex2 = glyph.index;
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyphIndex2, glyph);
				this.m_GlyphIndexListNewlyAdded.Add(glyphIndex2);
				this.m_GlyphIndexList.Add(glyphIndex2);
				j++;
			}
			this.m_GlyphsToAdd.Clear();
			for (int k = 0; k < this.m_CharactersToAdd.Count; k++)
			{
				TMP_Character character2 = this.m_CharactersToAdd[k];
				Glyph glyph2;
				if (!this.m_GlyphLookupDictionary.TryGetValue(character2.glyphIndex, out glyph2))
				{
					this.m_GlyphsToAdd.Add(character2.glyphIndex);
				}
				else
				{
					character2.glyph = glyph2;
					character2.textAsset = this;
					this.m_CharacterTable.Add(character2);
					this.m_CharacterLookupDictionary.Add(character2.unicode, character2);
					this.m_CharactersToAdd.RemoveAt(k);
					k--;
				}
			}
			if (this.m_IsMultiAtlasTexturesEnabled && !allGlyphsAddedToTexture)
			{
				while (!allGlyphsAddedToTexture)
				{
					allGlyphsAddedToTexture = this.TryAddGlyphsToNewAtlasTexture();
				}
			}
			if (includeFontFeatures)
			{
				this.UpdateFontFeaturesForNewlyAddedGlyphs();
			}
			missingCharacters = string.Empty;
			for (int l = 0; l < this.m_CharactersToAdd.Count; l++)
			{
				TMP_Character character3 = this.m_CharactersToAdd[l];
				this.s_MissingCharacterList.Add(character3.unicode);
			}
			if (this.s_MissingCharacterList.Count > 0)
			{
				missingCharacters = this.s_MissingCharacterList.UintToString();
			}
			return allGlyphsAddedToTexture && !isMissingCharacters;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000076E0 File Offset: 0x000058E0
		internal bool AddGlyphInternal(uint glyphIndex)
		{
			Glyph glyph;
			return this.TryAddGlyphInternal(glyphIndex, out glyph);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000076F8 File Offset: 0x000058F8
		internal bool TryAddGlyphInternal(uint glyphIndex, out Glyph glyph)
		{
			glyph = null;
			if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				glyph = this.m_GlyphLookupDictionary[glyphIndex];
				return true;
			}
			if (this.m_AtlasPopulationMode == AtlasPopulationMode.Static)
			{
				return false;
			}
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				return false;
			}
			if (!this.m_AtlasTextures[this.m_AtlasTextureIndex].isReadable)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Unable to add the requested glyph to font asset [",
					base.name,
					"]'s atlas texture. Please make the texture [",
					this.m_AtlasTextures[this.m_AtlasTextureIndex].name,
					"] readable."
				}), this.m_AtlasTextures[this.m_AtlasTextureIndex]);
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width <= 1 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height <= 1)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			if (FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
			{
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				this.m_GlyphIndexList.Add(glyphIndex);
				this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				if (this.m_GetFontFeatures && TMP_Settings.getFontFeaturesAtRuntime)
				{
					this.UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
					TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
				}
				return true;
			}
			if (this.m_IsMultiAtlasTexturesEnabled && this.m_UsedGlyphRects.Count > 0)
			{
				this.SetupNewAtlasTexture();
				if (FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
				{
					glyph.atlasIndex = this.m_AtlasTextureIndex;
					this.m_GlyphTable.Add(glyph);
					this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
					this.m_GlyphIndexList.Add(glyphIndex);
					this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
					if (this.m_GetFontFeatures && TMP_Settings.getFontFeaturesAtRuntime)
					{
						this.UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
						TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000793C File Offset: 0x00005B3C
		internal bool TryAddCharacterInternal(uint unicode, out TMP_Character character)
		{
			character = null;
			if (this.m_MissingUnicodesFromFontFile.Contains(unicode))
			{
				return false;
			}
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				return false;
			}
			uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
			if (glyphIndex == 0U)
			{
				if (unicode != 160U)
				{
					if (unicode == 173U || unicode == 8209U)
					{
						glyphIndex = FontEngine.GetGlyphIndex(45U);
					}
				}
				else
				{
					glyphIndex = FontEngine.GetGlyphIndex(32U);
				}
				if (glyphIndex == 0U)
				{
					this.m_MissingUnicodesFromFontFile.Add(unicode);
					return false;
				}
			}
			if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				character = new TMP_Character(unicode, this, this.m_GlyphLookupDictionary[glyphIndex]);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				return true;
			}
			Glyph glyph = null;
			if (!this.m_AtlasTextures[this.m_AtlasTextureIndex].isReadable)
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"Unable to add the requested character to font asset [",
					base.name,
					"]'s atlas texture. Please make the texture [",
					this.m_AtlasTextures[this.m_AtlasTextureIndex].name,
					"] readable."
				}), this.m_AtlasTextures[this.m_AtlasTextureIndex]);
				return false;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width <= 1 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height <= 1)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			if (FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
			{
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				character = new TMP_Character(unicode, this, glyph);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				this.m_GlyphIndexList.Add(glyphIndex);
				this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				if (this.m_GetFontFeatures && TMP_Settings.getFontFeaturesAtRuntime)
				{
					this.UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
					TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
				}
				return true;
			}
			if (this.m_IsMultiAtlasTexturesEnabled && this.m_UsedGlyphRects.Count > 0)
			{
				this.SetupNewAtlasTexture();
				if (FontEngine.TryAddGlyphToTexture(glyphIndex, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyph))
				{
					glyph.atlasIndex = this.m_AtlasTextureIndex;
					this.m_GlyphTable.Add(glyph);
					this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
					character = new TMP_Character(unicode, this, glyph);
					this.m_CharacterTable.Add(character);
					this.m_CharacterLookupDictionary.Add(unicode, character);
					this.m_GlyphIndexList.Add(glyphIndex);
					this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
					if (this.m_GetFontFeatures && TMP_Settings.getFontFeaturesAtRuntime)
					{
						this.UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
						TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007C40 File Offset: 0x00005E40
		internal bool TryGetCharacter_and_QueueRenderToTexture(uint unicode, out TMP_Character character)
		{
			character = null;
			if (this.m_MissingUnicodesFromFontFile.Contains(unicode))
			{
				return false;
			}
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				return false;
			}
			uint glyphIndex = FontEngine.GetGlyphIndex(unicode);
			if (glyphIndex == 0U)
			{
				if (unicode != 160U)
				{
					if (unicode == 173U || unicode == 8209U)
					{
						glyphIndex = FontEngine.GetGlyphIndex(45U);
					}
				}
				else
				{
					glyphIndex = FontEngine.GetGlyphIndex(32U);
				}
				if (glyphIndex == 0U)
				{
					this.m_MissingUnicodesFromFontFile.Add(unicode);
					return false;
				}
			}
			if (this.m_GlyphLookupDictionary.ContainsKey(glyphIndex))
			{
				character = new TMP_Character(unicode, this, this.m_GlyphLookupDictionary[glyphIndex]);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				return true;
			}
			GlyphLoadFlags glyphLoadFlags = ((((GlyphRenderMode)4 & this.m_AtlasRenderMode) == (GlyphRenderMode)4) ? (GlyphLoadFlags.LOAD_NO_HINTING | GlyphLoadFlags.LOAD_NO_BITMAP) : GlyphLoadFlags.LOAD_NO_BITMAP);
			Glyph glyph = null;
			if (FontEngine.TryGetGlyphWithIndexValue(glyphIndex, glyphLoadFlags, out glyph))
			{
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				character = new TMP_Character(unicode, this, glyph);
				this.m_CharacterTable.Add(character);
				this.m_CharacterLookupDictionary.Add(unicode, character);
				this.m_GlyphIndexList.Add(glyphIndex);
				this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				if (this.m_GetFontFeatures && TMP_Settings.getFontFeaturesAtRuntime)
				{
					this.UpdateGSUBFontFeaturesForNewGlyphIndex(glyphIndex);
					TMP_FontAsset.RegisterFontAssetForFontFeatureUpdate(this);
				}
				this.m_GlyphsToRender.Add(glyph);
				return true;
			}
			return false;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00002AAB File Offset: 0x00000CAB
		internal void TryAddGlyphsToAtlasTextures()
		{
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007D94 File Offset: 0x00005F94
		private bool TryAddGlyphsToNewAtlasTexture()
		{
			this.SetupNewAtlasTexture();
			Glyph[] glyphs;
			bool allGlyphsAddedToTexture = FontEngine.TryAddGlyphsToTexture(this.m_GlyphsToAdd, this.m_AtlasPadding, GlyphPackingMode.BestShortSideFit, this.m_FreeGlyphRects, this.m_UsedGlyphRects, this.m_AtlasRenderMode, this.m_AtlasTextures[this.m_AtlasTextureIndex], out glyphs);
			int i = 0;
			while (i < glyphs.Length && glyphs[i] != null)
			{
				Glyph glyph = glyphs[i];
				uint glyphIndex = glyph.index;
				glyph.atlasIndex = this.m_AtlasTextureIndex;
				this.m_GlyphTable.Add(glyph);
				this.m_GlyphLookupDictionary.Add(glyphIndex, glyph);
				this.m_GlyphIndexListNewlyAdded.Add(glyphIndex);
				this.m_GlyphIndexList.Add(glyphIndex);
				i++;
			}
			this.m_GlyphsToAdd.Clear();
			for (int j = 0; j < this.m_CharactersToAdd.Count; j++)
			{
				TMP_Character character = this.m_CharactersToAdd[j];
				Glyph glyph2;
				if (!this.m_GlyphLookupDictionary.TryGetValue(character.glyphIndex, out glyph2))
				{
					this.m_GlyphsToAdd.Add(character.glyphIndex);
				}
				else
				{
					character.glyph = glyph2;
					character.textAsset = this;
					this.m_CharacterTable.Add(character);
					this.m_CharacterLookupDictionary.Add(character.unicode, character);
					this.m_CharactersToAdd.RemoveAt(j);
					j--;
				}
			}
			return allGlyphsAddedToTexture;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007EE4 File Offset: 0x000060E4
		private void SetupNewAtlasTexture()
		{
			this.m_AtlasTextureIndex++;
			if (this.m_AtlasTextures.Length == this.m_AtlasTextureIndex)
			{
				Array.Resize<Texture2D>(ref this.m_AtlasTextures, this.m_AtlasTextures.Length * 2);
			}
			TextureFormat texFormat = (((this.m_AtlasRenderMode & (GlyphRenderMode)65536) == (GlyphRenderMode)65536) ? TextureFormat.RGBA32 : TextureFormat.Alpha8);
			this.m_AtlasTextures[this.m_AtlasTextureIndex] = new Texture2D(this.m_AtlasWidth, this.m_AtlasHeight, texFormat, false);
			FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			int packingModifier = (((this.m_AtlasRenderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16) ? 0 : 1);
			this.m_FreeGlyphRects.Clear();
			this.m_FreeGlyphRects.Add(new GlyphRect(0, 0, this.m_AtlasWidth - packingModifier, this.m_AtlasHeight - packingModifier));
			this.m_UsedGlyphRects.Clear();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007FB8 File Offset: 0x000061B8
		internal void UpdateAtlasTexture()
		{
			if (this.m_GlyphsToRender.Count == 0)
			{
				return;
			}
			if (this.m_AtlasTextures[this.m_AtlasTextureIndex].width <= 1 || this.m_AtlasTextures[this.m_AtlasTextureIndex].height <= 1)
			{
				this.m_AtlasTextures[this.m_AtlasTextureIndex].Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight);
				FontEngine.ResetAtlasTexture(this.m_AtlasTextures[this.m_AtlasTextureIndex]);
			}
			this.m_AtlasTextures[this.m_AtlasTextureIndex].Apply(false, false);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00008042 File Offset: 0x00006242
		private void UpdateFontFeaturesForNewlyAddedGlyphs()
		{
			this.UpdateLigatureSubstitutionRecords();
			this.UpdateGlyphAdjustmentRecords();
			this.UpdateDiacriticalMarkAdjustmentRecords();
			this.m_GlyphIndexListNewlyAdded.Clear();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008061 File Offset: 0x00006261
		private void UpdateGPOSFontFeaturesForNewlyAddedGlyphs()
		{
			this.UpdateGlyphAdjustmentRecords();
			this.UpdateDiacriticalMarkAdjustmentRecords();
			this.m_GlyphIndexListNewlyAdded.Clear();
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000807C File Offset: 0x0000627C
		internal void ImportFontFeatures()
		{
			if (this.LoadFontFace() != FontEngineError.Success)
			{
				return;
			}
			GlyphPairAdjustmentRecord[] pairAdjustmentRecords = FontEngine.GetAllPairAdjustmentRecords();
			if (pairAdjustmentRecords != null)
			{
				this.AddPairAdjustmentRecords(pairAdjustmentRecords);
			}
			MarkToBaseAdjustmentRecord[] markToBaseRecords = FontEngine.GetAllMarkToBaseAdjustmentRecords();
			if (markToBaseRecords != null)
			{
				this.AddMarkToBaseAdjustmentRecords(markToBaseRecords);
			}
			MarkToMarkAdjustmentRecord[] markToMarkRecords = FontEngine.GetAllMarkToMarkAdjustmentRecords();
			if (markToMarkRecords != null)
			{
				this.AddMarkToMarkAdjustmentRecords(markToMarkRecords);
			}
			LigatureSubstitutionRecord[] records = FontEngine.GetAllLigatureSubstitutionRecords();
			if (records != null)
			{
				this.AddLigatureSubstitutionRecords(records);
			}
			this.m_ShouldReimportFontFeatures = false;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000080DC File Offset: 0x000062DC
		private void UpdateGSUBFontFeaturesForNewGlyphIndex(uint glyphIndex)
		{
			LigatureSubstitutionRecord[] records = FontEngine.GetLigatureSubstitutionRecords(glyphIndex);
			if (records != null)
			{
				this.AddLigatureSubstitutionRecords(records);
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x000080FC File Offset: 0x000062FC
		internal void UpdateLigatureSubstitutionRecords()
		{
			LigatureSubstitutionRecord[] records = FontEngine.GetLigatureSubstitutionRecords(this.m_GlyphIndexListNewlyAdded);
			if (records != null)
			{
				this.AddLigatureSubstitutionRecords(records);
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008120 File Offset: 0x00006320
		private void AddLigatureSubstitutionRecords(LigatureSubstitutionRecord[] records)
		{
			for (int i = 0; i < records.Length; i++)
			{
				LigatureSubstitutionRecord record = records[i];
				if (records[i].componentGlyphIDs == null || records[i].ligatureGlyphID == 0U)
				{
					return;
				}
				uint firstComponentGlyphIndex = record.componentGlyphIDs[0];
				LigatureSubstitutionRecord newRecord = new LigatureSubstitutionRecord
				{
					componentGlyphIDs = record.componentGlyphIDs,
					ligatureGlyphID = record.ligatureGlyphID
				};
				List<LigatureSubstitutionRecord> existingRecords;
				if (this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.TryGetValue(firstComponentGlyphIndex, out existingRecords))
				{
					foreach (LigatureSubstitutionRecord ligature in existingRecords)
					{
						if (newRecord == ligature)
						{
							return;
						}
					}
					this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup[firstComponentGlyphIndex].Add(newRecord);
				}
				else
				{
					this.m_FontFeatureTable.m_LigatureSubstitutionRecordLookup.Add(firstComponentGlyphIndex, new List<LigatureSubstitutionRecord> { newRecord });
				}
				this.m_FontFeatureTable.m_LigatureSubstitutionRecords.Add(newRecord);
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000823C File Offset: 0x0000643C
		internal void UpdateGlyphAdjustmentRecords()
		{
			GlyphPairAdjustmentRecord[] records = FontEngine.GetPairAdjustmentRecords(this.m_GlyphIndexListNewlyAdded);
			if (records != null)
			{
				this.AddPairAdjustmentRecords(records);
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008260 File Offset: 0x00006460
		private void AddPairAdjustmentRecords(GlyphPairAdjustmentRecord[] records)
		{
			float emScale = this.m_FaceInfo.pointSize / (float)this.m_FaceInfo.unitsPerEM;
			foreach (GlyphPairAdjustmentRecord record in records)
			{
				GlyphAdjustmentRecord first = record.firstAdjustmentRecord;
				GlyphAdjustmentRecord second = record.secondAdjustmentRecord;
				uint firstIndex = first.glyphIndex;
				uint secondIndexIndex = second.glyphIndex;
				if (firstIndex == 0U && secondIndexIndex == 0U)
				{
					return;
				}
				uint key = (secondIndexIndex << 16) | firstIndex;
				if (!this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(key))
				{
					GlyphValueRecord valueRecord = first.glyphValueRecord;
					valueRecord.xAdvance *= emScale;
					record.firstAdjustmentRecord = new GlyphAdjustmentRecord(firstIndex, valueRecord);
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(record);
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(key, record);
				}
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000833C File Offset: 0x0000653C
		internal void UpdateGlyphAdjustmentRecords(uint[] glyphIndexes)
		{
			GlyphPairAdjustmentRecord[] pairAdjustmentRecords = FontEngine.GetGlyphPairAdjustmentTable(glyphIndexes);
			if (pairAdjustmentRecords == null || pairAdjustmentRecords.Length == 0)
			{
				return;
			}
			if (this.m_FontFeatureTable == null)
			{
				this.m_FontFeatureTable = new TMP_FontFeatureTable();
			}
			int i = 0;
			while (i < pairAdjustmentRecords.Length && pairAdjustmentRecords[i].firstAdjustmentRecord.glyphIndex != 0U)
			{
				uint pairKey = (pairAdjustmentRecords[i].secondAdjustmentRecord.glyphIndex << 16) | pairAdjustmentRecords[i].firstAdjustmentRecord.glyphIndex;
				if (!this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.ContainsKey(pairKey))
				{
					GlyphPairAdjustmentRecord record = pairAdjustmentRecords[i];
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(record);
					this.m_FontFeatureTable.m_GlyphPairAdjustmentRecordLookup.Add(pairKey, record);
				}
				i++;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00008400 File Offset: 0x00006600
		internal void UpdateDiacriticalMarkAdjustmentRecords()
		{
			MarkToBaseAdjustmentRecord[] markToBaseRecords = FontEngine.GetMarkToBaseAdjustmentRecords(this.m_GlyphIndexListNewlyAdded);
			if (markToBaseRecords != null)
			{
				this.AddMarkToBaseAdjustmentRecords(markToBaseRecords);
			}
			MarkToMarkAdjustmentRecord[] markToMarkRecords = FontEngine.GetMarkToMarkAdjustmentRecords(this.m_GlyphIndexListNewlyAdded);
			if (markToMarkRecords != null)
			{
				this.AddMarkToMarkAdjustmentRecords(markToMarkRecords);
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000843C File Offset: 0x0000663C
		private void AddMarkToBaseAdjustmentRecords(MarkToBaseAdjustmentRecord[] records)
		{
			float emScale = this.m_FaceInfo.pointSize / (float)this.m_FaceInfo.unitsPerEM;
			for (int i = 0; i < records.Length; i++)
			{
				MarkToBaseAdjustmentRecord record = records[i];
				if (records[i].baseGlyphID == 0U || records[i].markGlyphID == 0U)
				{
					return;
				}
				uint key = (record.markGlyphID << 16) | record.baseGlyphID;
				if (!this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.ContainsKey(key))
				{
					MarkToBaseAdjustmentRecord newRecord = new MarkToBaseAdjustmentRecord
					{
						baseGlyphID = record.baseGlyphID,
						baseGlyphAnchorPoint = new GlyphAnchorPoint
						{
							xCoordinate = record.baseGlyphAnchorPoint.xCoordinate * emScale,
							yCoordinate = record.baseGlyphAnchorPoint.yCoordinate * emScale
						},
						markGlyphID = record.markGlyphID,
						markPositionAdjustment = new MarkPositionAdjustment
						{
							xPositionAdjustment = record.markPositionAdjustment.xPositionAdjustment * emScale,
							yPositionAdjustment = record.markPositionAdjustment.yPositionAdjustment * emScale
						}
					};
					this.m_FontFeatureTable.MarkToBaseAdjustmentRecords.Add(newRecord);
					this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecordLookup.Add(key, newRecord);
				}
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008598 File Offset: 0x00006798
		private void AddMarkToMarkAdjustmentRecords(MarkToMarkAdjustmentRecord[] records)
		{
			float emScale = this.m_FaceInfo.pointSize / (float)this.m_FaceInfo.unitsPerEM;
			for (int i = 0; i < records.Length; i++)
			{
				MarkToMarkAdjustmentRecord record = records[i];
				if (records[i].baseMarkGlyphID == 0U || records[i].combiningMarkGlyphID == 0U)
				{
					return;
				}
				uint key = (record.combiningMarkGlyphID << 16) | record.baseMarkGlyphID;
				if (!this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.ContainsKey(key))
				{
					MarkToMarkAdjustmentRecord newRecord = new MarkToMarkAdjustmentRecord
					{
						baseMarkGlyphID = record.baseMarkGlyphID,
						baseMarkGlyphAnchorPoint = new GlyphAnchorPoint
						{
							xCoordinate = record.baseMarkGlyphAnchorPoint.xCoordinate * emScale,
							yCoordinate = record.baseMarkGlyphAnchorPoint.yCoordinate * emScale
						},
						combiningMarkGlyphID = record.combiningMarkGlyphID,
						combiningMarkPositionAdjustment = new MarkPositionAdjustment
						{
							xPositionAdjustment = record.combiningMarkPositionAdjustment.xPositionAdjustment * emScale,
							yPositionAdjustment = record.combiningMarkPositionAdjustment.yPositionAdjustment * emScale
						}
					};
					this.m_FontFeatureTable.MarkToMarkAdjustmentRecords.Add(newRecord);
					this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecordLookup.Add(key, newRecord);
				}
			}
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000086F4 File Offset: 0x000068F4
		private void CopyListDataToArray<T>(List<T> srcList, ref T[] dstArray)
		{
			int size = srcList.Count;
			if (dstArray == null)
			{
				dstArray = new T[size];
			}
			else
			{
				Array.Resize<T>(ref dstArray, size);
			}
			for (int i = 0; i < size; i++)
			{
				dstArray[i] = srcList[i];
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00008738 File Offset: 0x00006938
		internal void UpdateFontAssetData()
		{
			uint[] unicodeCharacters = new uint[this.m_CharacterTable.Count];
			for (int i = 0; i < this.m_CharacterTable.Count; i++)
			{
				unicodeCharacters[i] = this.m_CharacterTable[i].unicode;
			}
			this.ClearCharacterAndGlyphTables();
			this.ClearFontFeaturesTables();
			this.ClearAtlasTextures(true);
			this.ReadFontAssetDefinition();
			if (unicodeCharacters.Length != 0)
			{
				this.TryAddCharacters(unicodeCharacters, this.m_GetFontFeatures && TMP_Settings.getFontFeaturesAtRuntime);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000087B5 File Offset: 0x000069B5
		public void ClearFontAssetData(bool setAtlasSizeToZero = false)
		{
			this.ClearCharacterAndGlyphTables();
			this.ClearFontFeaturesTables();
			this.ClearAtlasTextures(setAtlasSizeToZero);
			this.ReadFontAssetDefinition();
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000087D0 File Offset: 0x000069D0
		internal void ClearCharacterAndGlyphTablesInternal()
		{
			this.ClearCharacterAndGlyphTables();
			this.ClearAtlasTextures(true);
			this.ReadFontAssetDefinition();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000087E5 File Offset: 0x000069E5
		internal void ClearFontFeaturesInternal()
		{
			this.ClearFontFeaturesTables();
			this.ReadFontAssetDefinition();
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000087F4 File Offset: 0x000069F4
		private void ClearCharacterAndGlyphTables()
		{
			if (this.m_GlyphTable != null)
			{
				this.m_GlyphTable.Clear();
			}
			if (this.m_CharacterTable != null)
			{
				this.m_CharacterTable.Clear();
			}
			if (this.m_UsedGlyphRects != null)
			{
				this.m_UsedGlyphRects.Clear();
			}
			if (this.m_FreeGlyphRects != null)
			{
				int packingModifier = (((this.m_AtlasRenderMode & (GlyphRenderMode)16) == (GlyphRenderMode)16) ? 0 : 1);
				this.m_FreeGlyphRects.Clear();
				this.m_FreeGlyphRects.Add(new GlyphRect(0, 0, this.m_AtlasWidth - packingModifier, this.m_AtlasHeight - packingModifier));
			}
			if (this.m_GlyphsToRender != null)
			{
				this.m_GlyphsToRender.Clear();
			}
			if (this.m_GlyphsRendered != null)
			{
				this.m_GlyphsRendered.Clear();
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000088A8 File Offset: 0x00006AA8
		private void ClearFontFeaturesTables()
		{
			if (this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_LigatureSubstitutionRecords != null)
			{
				this.m_FontFeatureTable.m_LigatureSubstitutionRecords.Clear();
			}
			if (this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords != null)
			{
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Clear();
			}
			if (this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecords != null)
			{
				this.m_FontFeatureTable.m_MarkToBaseAdjustmentRecords.Clear();
			}
			if (this.m_FontFeatureTable != null && this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecords != null)
			{
				this.m_FontFeatureTable.m_MarkToMarkAdjustmentRecords.Clear();
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000894C File Offset: 0x00006B4C
		internal void ClearAtlasTextures(bool setAtlasSizeToZero = false)
		{
			this.m_AtlasTextureIndex = 0;
			if (this.m_AtlasTextures == null)
			{
				return;
			}
			Texture2D texture;
			for (int i = 1; i < this.m_AtlasTextures.Length; i++)
			{
				texture = this.m_AtlasTextures[i];
				if (!(texture == null))
				{
					global::UnityEngine.Object.DestroyImmediate(texture, true);
				}
			}
			Array.Resize<Texture2D>(ref this.m_AtlasTextures, 1);
			texture = (this.m_AtlasTexture = this.m_AtlasTextures[0]);
			bool isReadable = texture.isReadable;
			TextureFormat texFormat = (((this.m_AtlasRenderMode & (GlyphRenderMode)65536) == (GlyphRenderMode)65536) ? TextureFormat.RGBA32 : TextureFormat.Alpha8);
			if (setAtlasSizeToZero)
			{
				texture.Reinitialize(1, 1, texFormat, false);
			}
			else if (texture.width != this.m_AtlasWidth || texture.height != this.m_AtlasHeight)
			{
				texture.Reinitialize(this.m_AtlasWidth, this.m_AtlasHeight, texFormat, false);
			}
			FontEngine.ResetAtlasTexture(texture);
			texture.Apply();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008A24 File Offset: 0x00006C24
		private void DestroyAtlasTextures()
		{
			if (this.m_AtlasTextures == null)
			{
				return;
			}
			for (int i = 0; i < this.m_AtlasTextures.Length; i++)
			{
				Texture2D tex = this.m_AtlasTextures[i];
				if (tex != null)
				{
					global::UnityEngine.Object.DestroyImmediate(tex);
				}
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008A68 File Offset: 0x00006C68
		private void UpgradeGlyphAdjustmentTableToFontFeatureTable()
		{
			Debug.Log("Upgrading font asset [" + base.name + "] Glyph Adjustment Table.", this);
			if (this.m_FontFeatureTable == null)
			{
				this.m_FontFeatureTable = new TMP_FontFeatureTable();
			}
			int pairCount = this.m_KerningTable.kerningPairs.Count;
			this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords = new List<GlyphPairAdjustmentRecord>(pairCount);
			for (int i = 0; i < pairCount; i++)
			{
				KerningPair pair = this.m_KerningTable.kerningPairs[i];
				uint firstGlyphIndex = 0U;
				TMP_Character firstCharacter;
				if (this.m_CharacterLookupDictionary.TryGetValue(pair.firstGlyph, out firstCharacter))
				{
					firstGlyphIndex = firstCharacter.glyphIndex;
				}
				uint secondGlyphIndex = 0U;
				TMP_Character secondCharacter;
				if (this.m_CharacterLookupDictionary.TryGetValue(pair.secondGlyph, out secondCharacter))
				{
					secondGlyphIndex = secondCharacter.glyphIndex;
				}
				GlyphAdjustmentRecord firstAdjustmentRecord = new GlyphAdjustmentRecord(firstGlyphIndex, new GlyphValueRecord(pair.firstGlyphAdjustments.xPlacement, pair.firstGlyphAdjustments.yPlacement, pair.firstGlyphAdjustments.xAdvance, pair.firstGlyphAdjustments.yAdvance));
				GlyphAdjustmentRecord secondAdjustmentRecord = new GlyphAdjustmentRecord(secondGlyphIndex, new GlyphValueRecord(pair.secondGlyphAdjustments.xPlacement, pair.secondGlyphAdjustments.yPlacement, pair.secondGlyphAdjustments.xAdvance, pair.secondGlyphAdjustments.yAdvance));
				GlyphPairAdjustmentRecord record = new GlyphPairAdjustmentRecord(firstAdjustmentRecord, secondAdjustmentRecord);
				this.m_FontFeatureTable.m_GlyphPairAdjustmentRecords.Add(record);
			}
			this.m_KerningTable.kerningPairs = null;
			this.m_KerningTable = null;
		}

		// Token: 0x040000DD RID: 221
		[SerializeField]
		internal string m_SourceFontFileGUID;

		// Token: 0x040000DE RID: 222
		[SerializeField]
		internal FontAssetCreationSettings m_CreationSettings;

		// Token: 0x040000DF RID: 223
		[SerializeField]
		private Font m_SourceFontFile;

		// Token: 0x040000E0 RID: 224
		[SerializeField]
		private string m_SourceFontFilePath;

		// Token: 0x040000E1 RID: 225
		[SerializeField]
		private AtlasPopulationMode m_AtlasPopulationMode;

		// Token: 0x040000E2 RID: 226
		[SerializeField]
		internal bool InternalDynamicOS;

		// Token: 0x040000E3 RID: 227
		private int m_FamilyNameHashCode;

		// Token: 0x040000E4 RID: 228
		private int m_StyleNameHashCode;

		// Token: 0x040000E5 RID: 229
		[SerializeField]
		internal List<Glyph> m_GlyphTable = new List<Glyph>();

		// Token: 0x040000E6 RID: 230
		internal Dictionary<uint, Glyph> m_GlyphLookupDictionary;

		// Token: 0x040000E7 RID: 231
		[SerializeField]
		internal List<TMP_Character> m_CharacterTable = new List<TMP_Character>();

		// Token: 0x040000E8 RID: 232
		internal Dictionary<uint, TMP_Character> m_CharacterLookupDictionary;

		// Token: 0x040000E9 RID: 233
		internal Texture2D m_AtlasTexture;

		// Token: 0x040000EA RID: 234
		[SerializeField]
		internal Texture2D[] m_AtlasTextures;

		// Token: 0x040000EB RID: 235
		[SerializeField]
		internal int m_AtlasTextureIndex;

		// Token: 0x040000EC RID: 236
		[SerializeField]
		private bool m_IsMultiAtlasTexturesEnabled;

		// Token: 0x040000ED RID: 237
		[SerializeField]
		private bool m_GetFontFeatures = true;

		// Token: 0x040000EE RID: 238
		[SerializeField]
		private bool m_ClearDynamicDataOnBuild;

		// Token: 0x040000EF RID: 239
		[SerializeField]
		internal int m_AtlasWidth;

		// Token: 0x040000F0 RID: 240
		[SerializeField]
		internal int m_AtlasHeight;

		// Token: 0x040000F1 RID: 241
		[SerializeField]
		internal int m_AtlasPadding;

		// Token: 0x040000F2 RID: 242
		[SerializeField]
		internal GlyphRenderMode m_AtlasRenderMode;

		// Token: 0x040000F3 RID: 243
		[SerializeField]
		private List<GlyphRect> m_UsedGlyphRects;

		// Token: 0x040000F4 RID: 244
		[SerializeField]
		private List<GlyphRect> m_FreeGlyphRects;

		// Token: 0x040000F5 RID: 245
		[SerializeField]
		internal TMP_FontFeatureTable m_FontFeatureTable = new TMP_FontFeatureTable();

		// Token: 0x040000F6 RID: 246
		[SerializeField]
		internal bool m_ShouldReimportFontFeatures;

		// Token: 0x040000F7 RID: 247
		[SerializeField]
		internal List<TMP_FontAsset> m_FallbackFontAssetTable;

		// Token: 0x040000F8 RID: 248
		[SerializeField]
		private TMP_FontWeightPair[] m_FontWeightTable = new TMP_FontWeightPair[10];

		// Token: 0x040000F9 RID: 249
		[SerializeField]
		private TMP_FontWeightPair[] fontWeights;

		// Token: 0x040000FA RID: 250
		public float normalStyle;

		// Token: 0x040000FB RID: 251
		public float normalSpacingOffset;

		// Token: 0x040000FC RID: 252
		public float boldStyle = 0.75f;

		// Token: 0x040000FD RID: 253
		public float boldSpacing = 7f;

		// Token: 0x040000FE RID: 254
		public byte italicStyle = 35;

		// Token: 0x040000FF RID: 255
		public byte tabSize = 10;

		// Token: 0x04000100 RID: 256
		internal bool IsFontAssetLookupTablesDirty;

		// Token: 0x04000101 RID: 257
		[SerializeField]
		private FaceInfo_Legacy m_fontInfo;

		// Token: 0x04000102 RID: 258
		[SerializeField]
		internal List<TMP_Glyph> m_glyphInfoList;

		// Token: 0x04000103 RID: 259
		[SerializeField]
		[FormerlySerializedAs("m_kerningInfo")]
		internal KerningTable m_KerningTable = new KerningTable();

		// Token: 0x04000104 RID: 260
		[SerializeField]
		private List<TMP_FontAsset> fallbackFontAssets;

		// Token: 0x04000105 RID: 261
		[SerializeField]
		public Texture2D atlas;

		// Token: 0x04000106 RID: 262
		private static ProfilerMarker k_ReadFontAssetDefinitionMarker = new ProfilerMarker("TMP.ReadFontAssetDefinition");

		// Token: 0x04000107 RID: 263
		private static ProfilerMarker k_AddSynthesizedCharactersMarker = new ProfilerMarker("TMP.AddSynthesizedCharacters");

		// Token: 0x04000108 RID: 264
		private static ProfilerMarker k_TryAddGlyphMarker = new ProfilerMarker("TMP.TryAddGlyph");

		// Token: 0x04000109 RID: 265
		private static ProfilerMarker k_TryAddCharacterMarker = new ProfilerMarker("TMP.TryAddCharacter");

		// Token: 0x0400010A RID: 266
		private static ProfilerMarker k_TryAddCharactersMarker = new ProfilerMarker("TMP.TryAddCharacters");

		// Token: 0x0400010B RID: 267
		private static ProfilerMarker k_UpdateLigatureSubstitutionRecordsMarker = new ProfilerMarker("TMP.UpdateLigatureSubstitutionRecords");

		// Token: 0x0400010C RID: 268
		private static ProfilerMarker k_UpdateGlyphAdjustmentRecordsMarker = new ProfilerMarker("TMP.UpdateGlyphAdjustmentRecords");

		// Token: 0x0400010D RID: 269
		private static ProfilerMarker k_UpdateDiacriticalMarkAdjustmentRecordsMarker = new ProfilerMarker("TMP.UpdateDiacriticalAdjustmentRecords");

		// Token: 0x0400010E RID: 270
		private static ProfilerMarker k_ClearFontAssetDataMarker = new ProfilerMarker("TMP.ClearFontAssetData");

		// Token: 0x0400010F RID: 271
		private static ProfilerMarker k_UpdateFontAssetDataMarker = new ProfilerMarker("TMP.UpdateFontAssetData");

		// Token: 0x04000110 RID: 272
		private static string s_DefaultMaterialSuffix = " Atlas Material";

		// Token: 0x04000111 RID: 273
		private static HashSet<int> k_SearchedFontAssetLookup;

		// Token: 0x04000112 RID: 274
		private static List<TMP_FontAsset> k_FontAssets_FontFeaturesUpdateQueue = new List<TMP_FontAsset>();

		// Token: 0x04000113 RID: 275
		private static HashSet<int> k_FontAssets_FontFeaturesUpdateQueueLookup = new HashSet<int>();

		// Token: 0x04000114 RID: 276
		private static List<Texture2D> k_FontAssets_AtlasTexturesUpdateQueue = new List<Texture2D>();

		// Token: 0x04000115 RID: 277
		private static HashSet<int> k_FontAssets_AtlasTexturesUpdateQueueLookup = new HashSet<int>();

		// Token: 0x04000116 RID: 278
		private List<Glyph> m_GlyphsToRender = new List<Glyph>();

		// Token: 0x04000117 RID: 279
		private List<Glyph> m_GlyphsRendered = new List<Glyph>();

		// Token: 0x04000118 RID: 280
		private List<uint> m_GlyphIndexList = new List<uint>();

		// Token: 0x04000119 RID: 281
		private List<uint> m_GlyphIndexListNewlyAdded = new List<uint>();

		// Token: 0x0400011A RID: 282
		internal List<uint> m_GlyphsToAdd = new List<uint>();

		// Token: 0x0400011B RID: 283
		internal HashSet<uint> m_GlyphsToAddLookup = new HashSet<uint>();

		// Token: 0x0400011C RID: 284
		internal List<TMP_Character> m_CharactersToAdd = new List<TMP_Character>();

		// Token: 0x0400011D RID: 285
		internal HashSet<uint> m_CharactersToAddLookup = new HashSet<uint>();

		// Token: 0x0400011E RID: 286
		internal List<uint> s_MissingCharacterList = new List<uint>();

		// Token: 0x0400011F RID: 287
		internal HashSet<uint> m_MissingUnicodesFromFontFile = new HashSet<uint>();

		// Token: 0x04000120 RID: 288
		internal static uint[] k_GlyphIndexArray;
	}
}
