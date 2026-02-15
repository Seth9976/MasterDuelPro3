using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x02000043 RID: 67
	public class TMP_FontAssetUtilities
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00009448 File Offset: 0x00007648
		public static TMP_FontAssetUtilities instance
		{
			get
			{
				return TMP_FontAssetUtilities.s_Instance;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000944F File Offset: 0x0000764F
		public static TMP_Character GetCharacterFromFontAsset(uint unicode, TMP_FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			if (includeFallbacks)
			{
				if (TMP_FontAssetUtilities.k_SearchedAssets == null)
				{
					TMP_FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					TMP_FontAssetUtilities.k_SearchedAssets.Clear();
				}
			}
			return TMP_FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, sourceFontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00009480 File Offset: 0x00007680
		private static TMP_Character GetCharacterFromFontAsset_Internal(uint unicode, TMP_FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			TMP_Character character = null;
			bool isItalic = (fontStyle & FontStyles.Italic) == FontStyles.Italic;
			if (isItalic || fontWeight != FontWeight.Regular)
			{
				TMP_FontWeightPair[] fontWeights = sourceFontAsset.fontWeightTable;
				int fontWeightIndex = 4;
				if (fontWeight <= FontWeight.Regular)
				{
					if (fontWeight <= FontWeight.ExtraLight)
					{
						if (fontWeight != FontWeight.Thin)
						{
							if (fontWeight == FontWeight.ExtraLight)
							{
								fontWeightIndex = 2;
							}
						}
						else
						{
							fontWeightIndex = 1;
						}
					}
					else if (fontWeight != FontWeight.Light)
					{
						if (fontWeight == FontWeight.Regular)
						{
							fontWeightIndex = 4;
						}
					}
					else
					{
						fontWeightIndex = 3;
					}
				}
				else if (fontWeight <= FontWeight.SemiBold)
				{
					if (fontWeight != FontWeight.Medium)
					{
						if (fontWeight == FontWeight.SemiBold)
						{
							fontWeightIndex = 6;
						}
					}
					else
					{
						fontWeightIndex = 5;
					}
				}
				else if (fontWeight != FontWeight.Bold)
				{
					if (fontWeight != FontWeight.Heavy)
					{
						if (fontWeight == FontWeight.Black)
						{
							fontWeightIndex = 9;
						}
					}
					else
					{
						fontWeightIndex = 8;
					}
				}
				else
				{
					fontWeightIndex = 7;
				}
				TMP_FontAsset temp = (isItalic ? fontWeights[fontWeightIndex].italicTypeface : fontWeights[fontWeightIndex].regularTypeface);
				if (temp != null)
				{
					if (temp.characterLookupTable.TryGetValue(unicode, out character))
					{
						if (character.textAsset != null)
						{
							isAlternativeTypeface = true;
							return character;
						}
						temp.characterLookupTable.Remove(unicode);
					}
					if ((temp.atlasPopulationMode == AtlasPopulationMode.Dynamic || temp.atlasPopulationMode == AtlasPopulationMode.DynamicOS) && temp.TryAddCharacterInternal(unicode, out character))
					{
						isAlternativeTypeface = true;
						return character;
					}
				}
			}
			if (sourceFontAsset.characterLookupTable.TryGetValue(unicode, out character))
			{
				if (character.textAsset != null)
				{
					return character;
				}
				sourceFontAsset.characterLookupTable.Remove(unicode);
			}
			if ((sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic || sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.DynamicOS) && sourceFontAsset.TryAddCharacterInternal(unicode, out character))
			{
				return character;
			}
			if (character == null && includeFallbacks && sourceFontAsset.fallbackFontAssetTable != null)
			{
				List<TMP_FontAsset> fallbackFontAssets = sourceFontAsset.fallbackFontAssetTable;
				int fallbackCount = fallbackFontAssets.Count;
				if (fallbackCount == 0)
				{
					return null;
				}
				for (int i = 0; i < fallbackCount; i++)
				{
					TMP_FontAsset temp2 = fallbackFontAssets[i];
					if (!(temp2 == null))
					{
						int id = temp2.instanceID;
						if (TMP_FontAssetUtilities.k_SearchedAssets.Add(id))
						{
							character = TMP_FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, temp2, true, fontStyle, fontWeight, out isAlternativeTypeface);
							if (character != null)
							{
								return character;
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00009690 File Offset: 0x00007890
		public static TMP_Character GetCharacterFromFontAssets(uint unicode, TMP_FontAsset sourceFontAsset, List<TMP_FontAsset> fontAssets, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			if (fontAssets == null || fontAssets.Count == 0)
			{
				return null;
			}
			if (includeFallbacks)
			{
				if (TMP_FontAssetUtilities.k_SearchedAssets == null)
				{
					TMP_FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					TMP_FontAssetUtilities.k_SearchedAssets.Clear();
				}
			}
			int fontAssetCount = fontAssets.Count;
			for (int i = 0; i < fontAssetCount; i++)
			{
				TMP_FontAsset fontAsset = fontAssets[i];
				if (!(fontAsset == null))
				{
					TMP_Character character = TMP_FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, fontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
					if (character != null)
					{
						return character;
					}
				}
			}
			return null;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00009708 File Offset: 0x00007908
		internal static TMP_TextElement GetTextElementFromTextAssets(uint unicode, TMP_FontAsset sourceFontAsset, List<TMP_Asset> textAssets, bool includeFallbacks, FontStyles fontStyle, FontWeight fontWeight, out bool isAlternativeTypeface)
		{
			isAlternativeTypeface = false;
			if (textAssets == null || textAssets.Count == 0)
			{
				return null;
			}
			if (includeFallbacks)
			{
				if (TMP_FontAssetUtilities.k_SearchedAssets == null)
				{
					TMP_FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					TMP_FontAssetUtilities.k_SearchedAssets.Clear();
				}
			}
			int textAssetCount = textAssets.Count;
			for (int i = 0; i < textAssetCount; i++)
			{
				TMP_Asset textAsset = textAssets[i];
				if (!(textAsset == null))
				{
					if (textAsset.GetType() == typeof(TMP_FontAsset))
					{
						TMP_FontAsset fontAsset = textAsset as TMP_FontAsset;
						TMP_Character character = TMP_FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, fontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface);
						if (character != null)
						{
							return character;
						}
					}
					else
					{
						TMP_SpriteAsset spriteAsset = textAsset as TMP_SpriteAsset;
						TMP_SpriteCharacter spriteCharacter = TMP_FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, spriteAsset, true);
						if (spriteCharacter != null)
						{
							return spriteCharacter;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000097BC File Offset: 0x000079BC
		public static TMP_SpriteCharacter GetSpriteCharacterFromSpriteAsset(uint unicode, TMP_SpriteAsset spriteAsset, bool includeFallbacks)
		{
			if (spriteAsset == null)
			{
				return null;
			}
			TMP_SpriteCharacter spriteCharacter;
			if (spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out spriteCharacter))
			{
				return spriteCharacter;
			}
			if (includeFallbacks)
			{
				if (TMP_FontAssetUtilities.k_SearchedAssets == null)
				{
					TMP_FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					TMP_FontAssetUtilities.k_SearchedAssets.Clear();
				}
				TMP_FontAssetUtilities.k_SearchedAssets.Add(spriteAsset.instanceID);
				List<TMP_SpriteAsset> fallbackSpriteAsset = spriteAsset.fallbackSpriteAssets;
				if (fallbackSpriteAsset != null && fallbackSpriteAsset.Count > 0)
				{
					int fallbackCount = fallbackSpriteAsset.Count;
					for (int i = 0; i < fallbackCount; i++)
					{
						TMP_SpriteAsset temp = fallbackSpriteAsset[i];
						if (!(temp == null))
						{
							int id = temp.instanceID;
							if (TMP_FontAssetUtilities.k_SearchedAssets.Add(id))
							{
								spriteCharacter = TMP_FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, temp, true);
								if (spriteCharacter != null)
								{
									return spriteCharacter;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000987C File Offset: 0x00007A7C
		private static TMP_SpriteCharacter GetSpriteCharacterFromSpriteAsset_Internal(uint unicode, TMP_SpriteAsset spriteAsset, bool includeFallbacks)
		{
			TMP_SpriteCharacter spriteCharacter;
			if (spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out spriteCharacter))
			{
				return spriteCharacter;
			}
			if (includeFallbacks)
			{
				List<TMP_SpriteAsset> fallbackSpriteAsset = spriteAsset.fallbackSpriteAssets;
				if (fallbackSpriteAsset != null && fallbackSpriteAsset.Count > 0)
				{
					int fallbackCount = fallbackSpriteAsset.Count;
					for (int i = 0; i < fallbackCount; i++)
					{
						TMP_SpriteAsset temp = fallbackSpriteAsset[i];
						if (!(temp == null))
						{
							int id = temp.instanceID;
							if (TMP_FontAssetUtilities.k_SearchedAssets.Add(id))
							{
								spriteCharacter = TMP_FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, temp, true);
								if (spriteCharacter != null)
								{
									return spriteCharacter;
								}
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x04000166 RID: 358
		private static readonly TMP_FontAssetUtilities s_Instance = new TMP_FontAssetUtilities();

		// Token: 0x04000167 RID: 359
		private static HashSet<int> k_SearchedAssets;
	}
}
