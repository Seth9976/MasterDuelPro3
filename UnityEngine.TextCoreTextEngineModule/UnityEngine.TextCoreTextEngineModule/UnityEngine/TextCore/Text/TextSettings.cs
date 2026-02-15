using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.Serialization;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000037 RID: 55
	[ExcludeFromObjectFactory]
	[ExcludeFromPreset]
	[Serializable]
	public class TextSettings : ScriptableObject
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000AE40 File Offset: 0x00009040
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000AE48 File Offset: 0x00009048
		public string version
		{
			get
			{
				return this.m_Version;
			}
			internal set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000AE51 File Offset: 0x00009051
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000AE59 File Offset: 0x00009059
		public FontAsset defaultFontAsset
		{
			get
			{
				return this.m_DefaultFontAsset;
			}
			set
			{
				this.m_DefaultFontAsset = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000AE62 File Offset: 0x00009062
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000AE6A File Offset: 0x0000906A
		public string defaultFontAssetPath
		{
			get
			{
				return this.m_DefaultFontAssetPath;
			}
			set
			{
				this.m_DefaultFontAssetPath = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000AE73 File Offset: 0x00009073
		// (set) Token: 0x0600015B RID: 347 RVA: 0x0000AE7B File Offset: 0x0000907B
		public List<FontAsset> fallbackFontAssets
		{
			get
			{
				return this.m_FallbackFontAssets;
			}
			set
			{
				this.m_FallbackFontAssets = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000AE84 File Offset: 0x00009084
		internal List<FontAsset> fallbackOSFontAssets
		{
			get
			{
				bool flag = this.GetStaticFallbackOSFontAsset() == null;
				if (flag)
				{
					this.SetStaticFallbackOSFontAsset(this.GetOSFontAssetList());
				}
				return this.GetStaticFallbackOSFontAsset();
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000AEB8 File Offset: 0x000090B8
		internal virtual List<FontAsset> GetStaticFallbackOSFontAsset()
		{
			return TextSettings.s_FallbackOSFontAssetInternal;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AECF File Offset: 0x000090CF
		internal virtual void SetStaticFallbackOSFontAsset(List<FontAsset> fontAssets)
		{
			TextSettings.s_FallbackOSFontAssetInternal = fontAssets;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AED8 File Offset: 0x000090D8
		internal virtual List<FontAsset> GetFallbackFontAssets(int textPixelSize = -1)
		{
			return this.fallbackFontAssets;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000AEF0 File Offset: 0x000090F0
		// (set) Token: 0x06000161 RID: 353 RVA: 0x0000AEF8 File Offset: 0x000090F8
		public bool matchMaterialPreset
		{
			get
			{
				return this.m_MatchMaterialPreset;
			}
			set
			{
				this.m_MatchMaterialPreset = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000AF01 File Offset: 0x00009101
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000AF09 File Offset: 0x00009109
		public int missingCharacterUnicode
		{
			get
			{
				return this.m_MissingCharacterUnicode;
			}
			set
			{
				this.m_MissingCharacterUnicode = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000AF12 File Offset: 0x00009112
		// (set) Token: 0x06000165 RID: 357 RVA: 0x0000AF1A File Offset: 0x0000911A
		public bool clearDynamicDataOnBuild
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000AF24 File Offset: 0x00009124
		// (set) Token: 0x06000167 RID: 359 RVA: 0x0000AF3C File Offset: 0x0000913C
		public bool enableEmojiSupport
		{
			get
			{
				return this.m_EnableEmojiSupport;
			}
			set
			{
				this.m_EnableEmojiSupport = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000AF46 File Offset: 0x00009146
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000AF4E File Offset: 0x0000914E
		public List<TextAsset> emojiFallbackTextAssets
		{
			get
			{
				return this.m_EmojiFallbackTextAssets;
			}
			set
			{
				this.m_EmojiFallbackTextAssets = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000AF57 File Offset: 0x00009157
		// (set) Token: 0x0600016B RID: 363 RVA: 0x0000AF5F File Offset: 0x0000915F
		public SpriteAsset defaultSpriteAsset
		{
			get
			{
				return this.m_DefaultSpriteAsset;
			}
			set
			{
				this.m_DefaultSpriteAsset = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000AF68 File Offset: 0x00009168
		// (set) Token: 0x0600016D RID: 365 RVA: 0x0000AF70 File Offset: 0x00009170
		public string defaultSpriteAssetPath
		{
			get
			{
				return this.m_DefaultSpriteAssetPath;
			}
			set
			{
				this.m_DefaultSpriteAssetPath = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000AF79 File Offset: 0x00009179
		// (set) Token: 0x0600016F RID: 367 RVA: 0x0000AF81 File Offset: 0x00009181
		[Obsolete("The Fallback Sprite Assets list is now obsolete. Use the emojiFallbackTextAssets instead.", true)]
		public List<SpriteAsset> fallbackSpriteAssets
		{
			get
			{
				return this.m_FallbackSpriteAssets;
			}
			set
			{
				this.m_FallbackSpriteAssets = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000AF92 File Offset: 0x00009192
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000AF8A File Offset: 0x0000918A
		internal static SpriteAsset s_GlobalSpriteAsset { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000AF99 File Offset: 0x00009199
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000AFA1 File Offset: 0x000091A1
		public uint missingSpriteCharacterUnicode
		{
			get
			{
				return this.m_MissingSpriteCharacterUnicode;
			}
			set
			{
				this.m_MissingSpriteCharacterUnicode = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000AFAA File Offset: 0x000091AA
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000AFB2 File Offset: 0x000091B2
		public TextStyleSheet defaultStyleSheet
		{
			get
			{
				return this.m_DefaultStyleSheet;
			}
			set
			{
				this.m_DefaultStyleSheet = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000AFBB File Offset: 0x000091BB
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000AFC3 File Offset: 0x000091C3
		public string styleSheetsResourcePath
		{
			get
			{
				return this.m_StyleSheetsResourcePath;
			}
			set
			{
				this.m_StyleSheetsResourcePath = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000AFCC File Offset: 0x000091CC
		// (set) Token: 0x06000179 RID: 377 RVA: 0x0000AFD4 File Offset: 0x000091D4
		public string defaultColorGradientPresetsPath
		{
			get
			{
				return this.m_DefaultColorGradientPresetsPath;
			}
			set
			{
				this.m_DefaultColorGradientPresetsPath = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000AFE0 File Offset: 0x000091E0
		// (set) Token: 0x0600017B RID: 379 RVA: 0x0000B01E File Offset: 0x0000921E
		public UnicodeLineBreakingRules lineBreakingRules
		{
			get
			{
				bool flag = this.m_UnicodeLineBreakingRules == null;
				if (flag)
				{
					this.m_UnicodeLineBreakingRules = new UnicodeLineBreakingRules();
					this.m_UnicodeLineBreakingRules.LoadLineBreakingRules();
				}
				return this.m_UnicodeLineBreakingRules;
			}
			set
			{
				this.m_UnicodeLineBreakingRules = value;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000B027 File Offset: 0x00009227
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000B02F File Offset: 0x0000922F
		public bool displayWarnings
		{
			get
			{
				return this.m_DisplayWarnings;
			}
			set
			{
				this.m_DisplayWarnings = value;
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B038 File Offset: 0x00009238
		private void OnEnable()
		{
			this.lineBreakingRules.LoadLineBreakingRules();
			this.SetStaticFallbackOSFontAsset(null);
			bool flag = TextSettings.s_GlobalSpriteAsset == null;
			if (flag)
			{
				TextSettings.s_GlobalSpriteAsset = Resources.Load<SpriteAsset>("Sprite Assets/Default Sprite Asset");
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000B07C File Offset: 0x0000927C
		protected void InitializeFontReferenceLookup()
		{
			bool flag = this.m_FontReferences == null;
			if (flag)
			{
				this.m_FontReferences = new List<TextSettings.FontReferenceMap>();
			}
			for (int i = 0; i < this.m_FontReferences.Count; i++)
			{
				TextSettings.FontReferenceMap fontRef = this.m_FontReferences[i];
				bool flag2 = fontRef.font == null || fontRef.fontAsset == null;
				if (flag2)
				{
					Debug.LogWarning("Deleting invalid font reference.");
					this.m_FontReferences.RemoveAt(i);
					i--;
				}
				else
				{
					int id = fontRef.font.GetHashCode() + fontRef.fontAsset.material.shader.GetHashCode();
					bool flag3 = !this.m_FontLookup.ContainsKey(id);
					if (flag3)
					{
						this.m_FontLookup.Add(id, fontRef.fontAsset);
					}
				}
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000B160 File Offset: 0x00009360
		protected FontAsset GetCachedFontAssetInternal(Font font)
		{
			return this.GetCachedFontAsset(font, TextShaderUtilities.ShaderRef_MobileSDF);
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000B180 File Offset: 0x00009380
		[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
		internal FontAsset GetCachedFontAsset(Font font, Shader shader)
		{
			bool flag = font == null || shader == null;
			FontAsset fontAsset2;
			if (flag)
			{
				fontAsset2 = null;
			}
			else
			{
				bool flag2 = this.m_FontLookup == null;
				if (flag2)
				{
					this.m_FontLookup = new Dictionary<int, FontAsset>();
					this.InitializeFontReferenceLookup();
				}
				int id = font.GetHashCode() + shader.GetHashCode();
				bool flag3 = this.m_FontLookup.ContainsKey(id);
				if (flag3)
				{
					fontAsset2 = this.m_FontLookup[id];
				}
				else
				{
					bool isExecutingJob = TextGenerator.IsExecutingJob;
					if (isExecutingJob)
					{
						fontAsset2 = null;
					}
					else
					{
						FontAsset fontAsset = FontAssetFactory.CreateDefaultEditorFontAsset(font, shader);
						bool flag4 = fontAsset != null;
						if (flag4)
						{
							this.m_FontReferences.Add(new TextSettings.FontReferenceMap(font, fontAsset));
							this.m_FontLookup.Add(id, fontAsset);
						}
						fontAsset2 = fontAsset;
					}
				}
			}
			return fontAsset2;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000B24C File Offset: 0x0000944C
		internal virtual Shader GetFontShader()
		{
			return TextShaderUtilities.ShaderRef_MobileSDF;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000B264 File Offset: 0x00009464
		private List<FontAsset> GetOSFontAssetList()
		{
			string[] fonts = Font.GetOSFallbacks();
			return FontAsset.CreateFontAssetOSFallbackList(fonts, this.GetFontShader(), 90f);
		}

		// Token: 0x04000163 RID: 355
		[SerializeField]
		protected string m_Version;

		// Token: 0x04000164 RID: 356
		[SerializeField]
		[FormerlySerializedAs("m_defaultFontAsset")]
		protected FontAsset m_DefaultFontAsset;

		// Token: 0x04000165 RID: 357
		[SerializeField]
		[FormerlySerializedAs("m_defaultFontAssetPath")]
		protected string m_DefaultFontAssetPath = "Fonts & Materials/";

		// Token: 0x04000166 RID: 358
		[FormerlySerializedAs("m_fallbackFontAssets")]
		[SerializeField]
		protected List<FontAsset> m_FallbackFontAssets;

		// Token: 0x04000167 RID: 359
		private static List<FontAsset> s_FallbackOSFontAssetInternal;

		// Token: 0x04000168 RID: 360
		[SerializeField]
		[FormerlySerializedAs("m_matchMaterialPreset")]
		protected bool m_MatchMaterialPreset;

		// Token: 0x04000169 RID: 361
		[FormerlySerializedAs("m_missingGlyphCharacter")]
		[SerializeField]
		protected int m_MissingCharacterUnicode;

		// Token: 0x0400016A RID: 362
		[SerializeField]
		protected bool m_ClearDynamicDataOnBuild = true;

		// Token: 0x0400016B RID: 363
		[SerializeField]
		private bool m_EnableEmojiSupport;

		// Token: 0x0400016C RID: 364
		[SerializeField]
		private List<TextAsset> m_EmojiFallbackTextAssets;

		// Token: 0x0400016D RID: 365
		[FormerlySerializedAs("m_defaultSpriteAsset")]
		[SerializeField]
		protected SpriteAsset m_DefaultSpriteAsset;

		// Token: 0x0400016E RID: 366
		[FormerlySerializedAs("m_defaultSpriteAssetPath")]
		[SerializeField]
		protected string m_DefaultSpriteAssetPath = "Sprite Assets/";

		// Token: 0x0400016F RID: 367
		[SerializeField]
		protected List<SpriteAsset> m_FallbackSpriteAssets;

		// Token: 0x04000171 RID: 369
		[SerializeField]
		protected uint m_MissingSpriteCharacterUnicode;

		// Token: 0x04000172 RID: 370
		[SerializeField]
		[FormerlySerializedAs("m_defaultStyleSheet")]
		protected TextStyleSheet m_DefaultStyleSheet;

		// Token: 0x04000173 RID: 371
		[SerializeField]
		protected string m_StyleSheetsResourcePath = "Text Style Sheets/";

		// Token: 0x04000174 RID: 372
		[SerializeField]
		[FormerlySerializedAs("m_defaultColorGradientPresetsPath")]
		protected string m_DefaultColorGradientPresetsPath = "Text Color Gradients/";

		// Token: 0x04000175 RID: 373
		[SerializeField]
		protected UnicodeLineBreakingRules m_UnicodeLineBreakingRules;

		// Token: 0x04000176 RID: 374
		[SerializeField]
		[FormerlySerializedAs("m_warningsDisabled")]
		protected bool m_DisplayWarnings = false;

		// Token: 0x04000177 RID: 375
		internal Dictionary<int, FontAsset> m_FontLookup;

		// Token: 0x04000178 RID: 376
		private List<TextSettings.FontReferenceMap> m_FontReferences = new List<TextSettings.FontReferenceMap>();

		// Token: 0x02000038 RID: 56
		[Serializable]
		private struct FontReferenceMap
		{
			// Token: 0x06000185 RID: 389 RVA: 0x0000B2E9 File Offset: 0x000094E9
			public FontReferenceMap(Font font, FontAsset fontAsset)
			{
				this.font = font;
				this.fontAsset = fontAsset;
			}

			// Token: 0x04000179 RID: 377
			public Font font;

			// Token: 0x0400017A RID: 378
			public FontAsset fontAsset;
		}
	}
}
