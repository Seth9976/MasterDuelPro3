using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000070 RID: 112
	[ExcludeFromPreset]
	[Serializable]
	public class TMP_Settings : ScriptableObject
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000355 RID: 853 RVA: 0x000123BC File Offset: 0x000105BC
		public static string version
		{
			get
			{
				return "1.4.0";
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000123C3 File Offset: 0x000105C3
		internal void SetAssetVersion()
		{
			this.assetVersion = TMP_Settings.s_CurrentAssetVersion;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000357 RID: 855 RVA: 0x000123D0 File Offset: 0x000105D0
		public static TextWrappingModes textWrappingMode
		{
			get
			{
				return TMP_Settings.instance.m_TextWrappingMode;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000123DC File Offset: 0x000105DC
		[Obsolete("The \"enableKerning\" property has been deprecated. Use the \"fontFeatures\" property to control what features are enabled by default on newly created text components.")]
		public static bool enableKerning
		{
			get
			{
				if (TMP_Settings.instance.m_ActiveFontFeatures != null)
				{
					return TMP_Settings.instance.m_ActiveFontFeatures.Contains(OTL_FeatureTag.kern);
				}
				return TMP_Settings.instance.m_enableKerning;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00012409 File Offset: 0x00010609
		public static List<OTL_FeatureTag> fontFeatures
		{
			get
			{
				return TMP_Settings.instance.m_ActiveFontFeatures;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00012415 File Offset: 0x00010615
		public static bool enableExtraPadding
		{
			get
			{
				return TMP_Settings.instance.m_enableExtraPadding;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00012421 File Offset: 0x00010621
		public static bool enableTintAllSprites
		{
			get
			{
				return TMP_Settings.instance.m_enableTintAllSprites;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0001242D File Offset: 0x0001062D
		public static bool enableParseEscapeCharacters
		{
			get
			{
				return TMP_Settings.instance.m_enableParseEscapeCharacters;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00012439 File Offset: 0x00010639
		public static bool enableRaycastTarget
		{
			get
			{
				return TMP_Settings.instance.m_EnableRaycastTarget;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00012445 File Offset: 0x00010645
		public static bool getFontFeaturesAtRuntime
		{
			get
			{
				return TMP_Settings.instance.m_GetFontFeaturesAtRuntime;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00012451 File Offset: 0x00010651
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0001245D File Offset: 0x0001065D
		public static int missingGlyphCharacter
		{
			get
			{
				return TMP_Settings.instance.m_missingGlyphCharacter;
			}
			set
			{
				TMP_Settings.instance.m_missingGlyphCharacter = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0001246A File Offset: 0x0001066A
		public static bool clearDynamicDataOnBuild
		{
			get
			{
				return TMP_Settings.instance.m_ClearDynamicDataOnBuild;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000362 RID: 866 RVA: 0x00012476 File Offset: 0x00010676
		public static bool warningsDisabled
		{
			get
			{
				return TMP_Settings.instance.m_warningsDisabled;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00012482 File Offset: 0x00010682
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0001248E File Offset: 0x0001068E
		public static TMP_FontAsset defaultFontAsset
		{
			get
			{
				return TMP_Settings.instance.m_defaultFontAsset;
			}
			set
			{
				TMP_Settings.instance.m_defaultFontAsset = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0001249B File Offset: 0x0001069B
		public static string defaultFontAssetPath
		{
			get
			{
				return TMP_Settings.instance.m_defaultFontAssetPath;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000366 RID: 870 RVA: 0x000124A7 File Offset: 0x000106A7
		public static float defaultFontSize
		{
			get
			{
				return TMP_Settings.instance.m_defaultFontSize;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000367 RID: 871 RVA: 0x000124B3 File Offset: 0x000106B3
		public static float defaultTextAutoSizingMinRatio
		{
			get
			{
				return TMP_Settings.instance.m_defaultAutoSizeMinRatio;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000368 RID: 872 RVA: 0x000124BF File Offset: 0x000106BF
		public static float defaultTextAutoSizingMaxRatio
		{
			get
			{
				return TMP_Settings.instance.m_defaultAutoSizeMaxRatio;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000369 RID: 873 RVA: 0x000124CB File Offset: 0x000106CB
		public static Vector2 defaultTextMeshProTextContainerSize
		{
			get
			{
				return TMP_Settings.instance.m_defaultTextMeshProTextContainerSize;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600036A RID: 874 RVA: 0x000124D7 File Offset: 0x000106D7
		public static Vector2 defaultTextMeshProUITextContainerSize
		{
			get
			{
				return TMP_Settings.instance.m_defaultTextMeshProUITextContainerSize;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600036B RID: 875 RVA: 0x000124E3 File Offset: 0x000106E3
		public static bool autoSizeTextContainer
		{
			get
			{
				return TMP_Settings.instance.m_autoSizeTextContainer;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600036C RID: 876 RVA: 0x000124EF File Offset: 0x000106EF
		// (set) Token: 0x0600036D RID: 877 RVA: 0x000124FB File Offset: 0x000106FB
		public static bool isTextObjectScaleStatic
		{
			get
			{
				return TMP_Settings.instance.m_IsTextObjectScaleStatic;
			}
			set
			{
				TMP_Settings.instance.m_IsTextObjectScaleStatic = value;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00012508 File Offset: 0x00010708
		// (set) Token: 0x0600036F RID: 879 RVA: 0x00012514 File Offset: 0x00010714
		public static List<TMP_FontAsset> fallbackFontAssets
		{
			get
			{
				return TMP_Settings.instance.m_fallbackFontAssets;
			}
			set
			{
				TMP_Settings.instance.m_fallbackFontAssets = value;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00012521 File Offset: 0x00010721
		public static bool matchMaterialPreset
		{
			get
			{
				return TMP_Settings.instance.m_matchMaterialPreset;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0001252D File Offset: 0x0001072D
		public static bool hideSubTextObjects
		{
			get
			{
				return TMP_Settings.instance.m_HideSubTextObjects;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00012539 File Offset: 0x00010739
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00012545 File Offset: 0x00010745
		public static TMP_SpriteAsset defaultSpriteAsset
		{
			get
			{
				return TMP_Settings.instance.m_defaultSpriteAsset;
			}
			set
			{
				TMP_Settings.instance.m_defaultSpriteAsset = value;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00012552 File Offset: 0x00010752
		public static string defaultSpriteAssetPath
		{
			get
			{
				return TMP_Settings.instance.m_defaultSpriteAssetPath;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0001255E File Offset: 0x0001075E
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0001256A File Offset: 0x0001076A
		public static bool enableEmojiSupport
		{
			get
			{
				return TMP_Settings.instance.m_enableEmojiSupport;
			}
			set
			{
				TMP_Settings.instance.m_enableEmojiSupport = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00012577 File Offset: 0x00010777
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00012583 File Offset: 0x00010783
		public static uint missingCharacterSpriteUnicode
		{
			get
			{
				return TMP_Settings.instance.m_MissingCharacterSpriteUnicode;
			}
			set
			{
				TMP_Settings.instance.m_MissingCharacterSpriteUnicode = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00012590 File Offset: 0x00010790
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0001259C File Offset: 0x0001079C
		public static List<TMP_Asset> emojiFallbackTextAssets
		{
			get
			{
				return TMP_Settings.instance.m_EmojiFallbackTextAssets;
			}
			set
			{
				TMP_Settings.instance.m_EmojiFallbackTextAssets = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600037B RID: 891 RVA: 0x000125A9 File Offset: 0x000107A9
		public static string defaultColorGradientPresetsPath
		{
			get
			{
				return TMP_Settings.instance.m_defaultColorGradientPresetsPath;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600037C RID: 892 RVA: 0x000125B5 File Offset: 0x000107B5
		// (set) Token: 0x0600037D RID: 893 RVA: 0x000125C1 File Offset: 0x000107C1
		public static TMP_StyleSheet defaultStyleSheet
		{
			get
			{
				return TMP_Settings.instance.m_defaultStyleSheet;
			}
			set
			{
				TMP_Settings.instance.m_defaultStyleSheet = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600037E RID: 894 RVA: 0x000125CE File Offset: 0x000107CE
		public static string styleSheetsResourcePath
		{
			get
			{
				return TMP_Settings.instance.m_StyleSheetsResourcePath;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000125DA File Offset: 0x000107DA
		public static TextAsset leadingCharacters
		{
			get
			{
				return TMP_Settings.instance.m_leadingCharacters;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000380 RID: 896 RVA: 0x000125E6 File Offset: 0x000107E6
		public static TextAsset followingCharacters
		{
			get
			{
				return TMP_Settings.instance.m_followingCharacters;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000381 RID: 897 RVA: 0x000125F2 File Offset: 0x000107F2
		public static TMP_Settings.LineBreakingTable linebreakingRules
		{
			get
			{
				if (TMP_Settings.instance.m_linebreakingRules == null)
				{
					TMP_Settings.LoadLinebreakingRules();
				}
				return TMP_Settings.instance.m_linebreakingRules;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0001260F File Offset: 0x0001080F
		// (set) Token: 0x06000383 RID: 899 RVA: 0x0001261B File Offset: 0x0001081B
		public static bool useModernHangulLineBreakingRules
		{
			get
			{
				return TMP_Settings.instance.m_UseModernHangulLineBreakingRules;
			}
			set
			{
				TMP_Settings.instance.m_UseModernHangulLineBreakingRules = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00012628 File Offset: 0x00010828
		public static TMP_Settings instance
		{
			get
			{
				if (TMP_Settings.isTMPSettingsNull)
				{
					TMP_Settings.s_Instance = Resources.Load<TMP_Settings>("TMP Settings");
					if (!TMP_Settings.isTMPSettingsNull && TMP_Settings.s_Instance.m_ActiveFontFeatures.Count == 1 && TMP_Settings.s_Instance.m_ActiveFontFeatures[0] == (OTL_FeatureTag)0U)
					{
						TMP_Settings.s_Instance.m_ActiveFontFeatures.Clear();
						if (TMP_Settings.s_Instance.m_enableKerning)
						{
							TMP_Settings.s_Instance.m_ActiveFontFeatures.Add(OTL_FeatureTag.kern);
						}
					}
				}
				return TMP_Settings.s_Instance;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000385 RID: 901 RVA: 0x000126AA File Offset: 0x000108AA
		internal static bool isTMPSettingsNull
		{
			get
			{
				return TMP_Settings.s_Instance == null;
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000126B8 File Offset: 0x000108B8
		public static TMP_Settings LoadDefaultSettings()
		{
			if (TMP_Settings.s_Instance == null)
			{
				TMP_Settings settings = Resources.Load<TMP_Settings>("TMP Settings");
				if (settings != null)
				{
					TMP_Settings.s_Instance = settings;
				}
			}
			return TMP_Settings.s_Instance;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000126F1 File Offset: 0x000108F1
		public static TMP_Settings GetSettings()
		{
			if (TMP_Settings.instance == null)
			{
				return null;
			}
			return TMP_Settings.instance;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00012707 File Offset: 0x00010907
		public static TMP_FontAsset GetFontAsset()
		{
			if (TMP_Settings.instance == null)
			{
				return null;
			}
			return TMP_Settings.instance.m_defaultFontAsset;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00012722 File Offset: 0x00010922
		public static TMP_SpriteAsset GetSpriteAsset()
		{
			if (TMP_Settings.instance == null)
			{
				return null;
			}
			return TMP_Settings.instance.m_defaultSpriteAsset;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001273D File Offset: 0x0001093D
		public static TMP_StyleSheet GetStyleSheet()
		{
			if (TMP_Settings.instance == null)
			{
				return null;
			}
			return TMP_Settings.instance.m_defaultStyleSheet;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00012758 File Offset: 0x00010958
		public static void LoadLinebreakingRules()
		{
			if (TMP_Settings.instance == null)
			{
				return;
			}
			if (TMP_Settings.s_Instance.m_linebreakingRules == null)
			{
				TMP_Settings.s_Instance.m_linebreakingRules = new TMP_Settings.LineBreakingTable();
			}
			TMP_Settings.s_Instance.m_linebreakingRules.leadingCharacters = TMP_Settings.GetCharacters(TMP_Settings.s_Instance.m_leadingCharacters);
			TMP_Settings.s_Instance.m_linebreakingRules.followingCharacters = TMP_Settings.GetCharacters(TMP_Settings.s_Instance.m_followingCharacters);
		}

		// Token: 0x0600038C RID: 908 RVA: 0x000127CC File Offset: 0x000109CC
		private static HashSet<uint> GetCharacters(TextAsset file)
		{
			HashSet<uint> dict = new HashSet<uint>();
			string text = file.text;
			for (int i = 0; i < text.Length; i++)
			{
				dict.Add((uint)text[i]);
			}
			return dict;
		}

		// Token: 0x0400030D RID: 781
		private static TMP_Settings s_Instance;

		// Token: 0x0400030E RID: 782
		[SerializeField]
		internal string assetVersion;

		// Token: 0x0400030F RID: 783
		internal static string s_CurrentAssetVersion = "2";

		// Token: 0x04000310 RID: 784
		[FormerlySerializedAs("m_enableWordWrapping")]
		[SerializeField]
		private TextWrappingModes m_TextWrappingMode;

		// Token: 0x04000311 RID: 785
		[SerializeField]
		private bool m_enableKerning;

		// Token: 0x04000312 RID: 786
		[SerializeField]
		private List<OTL_FeatureTag> m_ActiveFontFeatures = new List<OTL_FeatureTag> { (OTL_FeatureTag)0U };

		// Token: 0x04000313 RID: 787
		[SerializeField]
		private bool m_enableExtraPadding;

		// Token: 0x04000314 RID: 788
		[SerializeField]
		private bool m_enableTintAllSprites;

		// Token: 0x04000315 RID: 789
		[SerializeField]
		private bool m_enableParseEscapeCharacters;

		// Token: 0x04000316 RID: 790
		[SerializeField]
		private bool m_EnableRaycastTarget = true;

		// Token: 0x04000317 RID: 791
		[SerializeField]
		private bool m_GetFontFeaturesAtRuntime = true;

		// Token: 0x04000318 RID: 792
		[SerializeField]
		private int m_missingGlyphCharacter;

		// Token: 0x04000319 RID: 793
		[SerializeField]
		private bool m_ClearDynamicDataOnBuild = true;

		// Token: 0x0400031A RID: 794
		[SerializeField]
		private bool m_warningsDisabled;

		// Token: 0x0400031B RID: 795
		[SerializeField]
		private TMP_FontAsset m_defaultFontAsset;

		// Token: 0x0400031C RID: 796
		[SerializeField]
		private string m_defaultFontAssetPath;

		// Token: 0x0400031D RID: 797
		[SerializeField]
		private float m_defaultFontSize;

		// Token: 0x0400031E RID: 798
		[SerializeField]
		private float m_defaultAutoSizeMinRatio;

		// Token: 0x0400031F RID: 799
		[SerializeField]
		private float m_defaultAutoSizeMaxRatio;

		// Token: 0x04000320 RID: 800
		[SerializeField]
		private Vector2 m_defaultTextMeshProTextContainerSize;

		// Token: 0x04000321 RID: 801
		[SerializeField]
		private Vector2 m_defaultTextMeshProUITextContainerSize;

		// Token: 0x04000322 RID: 802
		[SerializeField]
		private bool m_autoSizeTextContainer;

		// Token: 0x04000323 RID: 803
		[SerializeField]
		private bool m_IsTextObjectScaleStatic;

		// Token: 0x04000324 RID: 804
		[SerializeField]
		private List<TMP_FontAsset> m_fallbackFontAssets;

		// Token: 0x04000325 RID: 805
		[SerializeField]
		private bool m_matchMaterialPreset;

		// Token: 0x04000326 RID: 806
		[SerializeField]
		private bool m_HideSubTextObjects = true;

		// Token: 0x04000327 RID: 807
		[SerializeField]
		private TMP_SpriteAsset m_defaultSpriteAsset;

		// Token: 0x04000328 RID: 808
		[SerializeField]
		private string m_defaultSpriteAssetPath;

		// Token: 0x04000329 RID: 809
		[SerializeField]
		private bool m_enableEmojiSupport;

		// Token: 0x0400032A RID: 810
		[SerializeField]
		private uint m_MissingCharacterSpriteUnicode;

		// Token: 0x0400032B RID: 811
		[SerializeField]
		private List<TMP_Asset> m_EmojiFallbackTextAssets;

		// Token: 0x0400032C RID: 812
		[SerializeField]
		private string m_defaultColorGradientPresetsPath;

		// Token: 0x0400032D RID: 813
		[SerializeField]
		private TMP_StyleSheet m_defaultStyleSheet;

		// Token: 0x0400032E RID: 814
		[SerializeField]
		private string m_StyleSheetsResourcePath;

		// Token: 0x0400032F RID: 815
		[SerializeField]
		private TextAsset m_leadingCharacters;

		// Token: 0x04000330 RID: 816
		[SerializeField]
		private TextAsset m_followingCharacters;

		// Token: 0x04000331 RID: 817
		[SerializeField]
		private TMP_Settings.LineBreakingTable m_linebreakingRules;

		// Token: 0x04000332 RID: 818
		[SerializeField]
		private bool m_UseModernHangulLineBreakingRules;

		// Token: 0x02000071 RID: 113
		public class LineBreakingTable
		{
			// Token: 0x04000333 RID: 819
			public HashSet<uint> leadingCharacters;

			// Token: 0x04000334 RID: 820
			public HashSet<uint> followingCharacters;
		}
	}
}
