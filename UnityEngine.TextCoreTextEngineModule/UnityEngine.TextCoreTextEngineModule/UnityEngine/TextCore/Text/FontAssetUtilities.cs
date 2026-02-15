using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200002F RID: 47
	internal static class FontAssetUtilities
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00009A8C File Offset: 0x00007C8C
		internal static Character GetCharacterFromFontAsset(uint unicode, FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface, bool populateLigatures)
		{
			if (includeFallbacks)
			{
				bool flag = FontAssetUtilities.k_SearchedAssets == null;
				if (flag)
				{
					FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					FontAssetUtilities.k_SearchedAssets.Clear();
				}
			}
			return FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, sourceFontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009AD8 File Offset: 0x00007CD8
		private static Character GetCharacterFromFontAsset_Internal(uint unicode, FontAsset sourceFontAsset, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface, bool populateLigatures)
		{
			bool canWriteOnAsset = !TextGenerator.IsExecutingJob;
			isAlternativeTypeface = false;
			Character character = null;
			bool isItalic = (fontStyle & FontStyles.Italic) == FontStyles.Italic;
			bool flag = isItalic || fontWeight != TextFontWeight.Regular;
			if (flag)
			{
				FontWeightPair[] fontWeights = sourceFontAsset.fontWeightTable;
				int fontWeightIndex = TextUtilities.GetTextFontWeightIndex(fontWeight);
				FontAsset temp = (isItalic ? fontWeights[fontWeightIndex].italicTypeface : fontWeights[fontWeightIndex].regularTypeface);
				bool flag2 = temp != null;
				if (flag2)
				{
					bool flag3 = !canWriteOnAsset && temp.m_CharacterLookupDictionary == null;
					if (flag3)
					{
						return null;
					}
					bool characterInLookupCache = temp.GetCharacterInLookupCache(unicode, fontStyle, fontWeight, out character);
					if (characterInLookupCache)
					{
						bool flag4 = character.textAsset != null;
						if (flag4)
						{
							isAlternativeTypeface = true;
							return character;
						}
						bool flag5 = !canWriteOnAsset;
						if (flag5)
						{
							return null;
						}
						temp.RemoveCharacterInLookupCache(unicode, fontStyle, fontWeight);
					}
					bool flag6 = temp.atlasPopulationMode == AtlasPopulationMode.Dynamic || temp.atlasPopulationMode == AtlasPopulationMode.DynamicOS;
					if (flag6)
					{
						bool flag7 = !canWriteOnAsset;
						if (flag7)
						{
							bool flag8 = !temp.m_MissingUnicodesFromFontFile.Contains(unicode);
							if (flag8)
							{
								return null;
							}
						}
						else
						{
							bool flag9 = temp.TryAddCharacterInternal(unicode, fontStyle, fontWeight, out character, populateLigatures);
							if (flag9)
							{
								isAlternativeTypeface = true;
								return character;
							}
						}
					}
					else
					{
						bool characterInLookupCache2 = temp.GetCharacterInLookupCache(unicode, FontStyles.Normal, TextFontWeight.Regular, out character);
						if (characterInLookupCache2)
						{
							bool flag10 = character.textAsset != null;
							if (flag10)
							{
								isAlternativeTypeface = true;
								return character;
							}
							bool flag11 = !canWriteOnAsset;
							if (flag11)
							{
								return null;
							}
							temp.RemoveCharacterInLookupCache(unicode, fontStyle, fontWeight);
						}
					}
				}
			}
			bool flag12 = !canWriteOnAsset && sourceFontAsset.m_CharacterLookupDictionary == null;
			Character character2;
			if (flag12)
			{
				character2 = null;
			}
			else
			{
				bool characterInLookupCache3 = sourceFontAsset.GetCharacterInLookupCache(unicode, fontStyle, fontWeight, out character);
				if (characterInLookupCache3)
				{
					bool flag13 = character.textAsset != null;
					if (flag13)
					{
						return character;
					}
					bool flag14 = !canWriteOnAsset;
					if (flag14)
					{
						return null;
					}
					sourceFontAsset.RemoveCharacterInLookupCache(unicode, fontStyle, fontWeight);
				}
				bool flag15 = sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.Dynamic || sourceFontAsset.atlasPopulationMode == AtlasPopulationMode.DynamicOS;
				if (flag15)
				{
					bool flag16 = !canWriteOnAsset;
					if (flag16)
					{
						return null;
					}
					bool flag17 = sourceFontAsset.TryAddCharacterInternal(unicode, fontStyle, fontWeight, out character, populateLigatures);
					if (flag17)
					{
						return character;
					}
				}
				else
				{
					bool characterInLookupCache4 = sourceFontAsset.GetCharacterInLookupCache(unicode, FontStyles.Normal, TextFontWeight.Regular, out character);
					if (characterInLookupCache4)
					{
						bool flag18 = character.textAsset != null;
						if (flag18)
						{
							return character;
						}
						bool flag19 = !canWriteOnAsset;
						if (flag19)
						{
							return null;
						}
						sourceFontAsset.RemoveCharacterInLookupCache(unicode, fontStyle, fontWeight);
					}
				}
				bool flag20 = character == null && !canWriteOnAsset;
				if (flag20)
				{
					character2 = null;
				}
				else
				{
					bool flag21 = character == null && includeFallbacks && sourceFontAsset.fallbackFontAssetTable != null;
					if (flag21)
					{
						List<FontAsset> fallbackFontAssets = sourceFontAsset.fallbackFontAssetTable;
						int fallbackCount = fallbackFontAssets.Count;
						bool flag22 = fallbackCount == 0;
						if (flag22)
						{
							return null;
						}
						for (int i = 0; i < fallbackCount; i++)
						{
							FontAsset temp2 = fallbackFontAssets[i];
							bool flag23 = temp2 == null;
							if (!flag23)
							{
								int id = temp2.GetHashCode();
								bool flag24 = !FontAssetUtilities.k_SearchedAssets.Add(id);
								if (!flag24)
								{
									character = FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, temp2, true, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
									bool flag25 = character != null;
									if (flag25)
									{
										return character;
									}
								}
							}
						}
					}
					character2 = null;
				}
			}
			return character2;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009E68 File Offset: 0x00008068
		internal static Character GetCharacterFromFontAssetsInternal(uint unicode, FontAsset sourceFontAsset, List<FontAsset> fontAssets, List<FontAsset> OSFallbackList, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface, bool populateLigatures = true)
		{
			isAlternativeTypeface = false;
			if (includeFallbacks)
			{
				bool flag = FontAssetUtilities.k_SearchedAssets == null;
				if (flag)
				{
					FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
				}
				else
				{
					FontAssetUtilities.k_SearchedAssets.Clear();
				}
			}
			Character character = FontAssetUtilities.GetCharacterFromFontAssetsInternal(unicode, fontAssets, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
			bool flag2 = character != null;
			Character character2;
			if (flag2)
			{
				character2 = character;
			}
			else
			{
				character2 = FontAssetUtilities.GetCharacterFromFontAssetsInternal(unicode, OSFallbackList, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
			}
			return character2;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00009EDC File Offset: 0x000080DC
		private static Character GetCharacterFromFontAssetsInternal(uint unicode, List<FontAsset> fontAssets, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface, bool populateLigatures = true)
		{
			isAlternativeTypeface = false;
			bool flag = fontAssets == null || fontAssets.Count == 0;
			Character character2;
			if (flag)
			{
				character2 = null;
			}
			else
			{
				if (includeFallbacks)
				{
					bool flag2 = FontAssetUtilities.k_SearchedAssets == null;
					if (flag2)
					{
						FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
					}
					else
					{
						FontAssetUtilities.k_SearchedAssets.Clear();
					}
				}
				int fontAssetCount = fontAssets.Count;
				for (int i = 0; i < fontAssetCount; i++)
				{
					FontAsset fontAsset = fontAssets[i];
					bool flag3 = fontAsset == null;
					if (!flag3)
					{
						Character character = FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, fontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
						bool flag4 = character != null;
						if (flag4)
						{
							return character;
						}
					}
				}
				character2 = null;
			}
			return character2;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00009F94 File Offset: 0x00008194
		internal static TextElement GetTextElementFromTextAssets(uint unicode, FontAsset sourceFontAsset, List<TextAsset> textAssets, bool includeFallbacks, FontStyles fontStyle, TextFontWeight fontWeight, out bool isAlternativeTypeface, bool populateLigatures)
		{
			isAlternativeTypeface = false;
			bool flag = textAssets == null || textAssets.Count == 0;
			TextElement textElement;
			if (flag)
			{
				textElement = null;
			}
			else
			{
				if (includeFallbacks)
				{
					bool flag2 = FontAssetUtilities.k_SearchedAssets == null;
					if (flag2)
					{
						FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
					}
					else
					{
						FontAssetUtilities.k_SearchedAssets.Clear();
					}
				}
				int textAssetCount = textAssets.Count;
				for (int i = 0; i < textAssetCount; i++)
				{
					TextAsset textAsset = textAssets[i];
					bool flag3 = textAsset == null;
					if (!flag3)
					{
						bool flag4 = textAsset.GetType() == typeof(FontAsset);
						if (flag4)
						{
							FontAsset fontAsset = textAsset as FontAsset;
							Character character = FontAssetUtilities.GetCharacterFromFontAsset_Internal(unicode, fontAsset, includeFallbacks, fontStyle, fontWeight, out isAlternativeTypeface, populateLigatures);
							bool flag5 = character != null;
							if (flag5)
							{
								return character;
							}
						}
						else
						{
							SpriteAsset spriteAsset = textAsset as SpriteAsset;
							SpriteCharacter spriteCharacter = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, spriteAsset, true);
							bool flag6 = spriteCharacter != null;
							if (flag6)
							{
								return spriteCharacter;
							}
						}
					}
				}
				textElement = null;
			}
			return textElement;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000A0A4 File Offset: 0x000082A4
		public static SpriteCharacter GetSpriteCharacterFromSpriteAsset(uint unicode, SpriteAsset spriteAsset, bool includeFallbacks)
		{
			bool canWriteOnAsset = !TextGenerator.IsExecutingJob;
			bool flag = spriteAsset == null;
			SpriteCharacter spriteCharacter2;
			if (flag)
			{
				spriteCharacter2 = null;
			}
			else
			{
				SpriteCharacter spriteCharacter;
				bool flag2 = spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out spriteCharacter);
				if (flag2)
				{
					spriteCharacter2 = spriteCharacter;
				}
				else
				{
					if (includeFallbacks)
					{
						bool flag3 = FontAssetUtilities.k_SearchedAssets == null;
						if (flag3)
						{
							FontAssetUtilities.k_SearchedAssets = new HashSet<int>();
						}
						else
						{
							FontAssetUtilities.k_SearchedAssets.Clear();
						}
						FontAssetUtilities.k_SearchedAssets.Add(spriteAsset.GetHashCode());
						List<SpriteAsset> fallbackSpriteAsset = spriteAsset.fallbackSpriteAssets;
						bool flag4 = fallbackSpriteAsset != null && fallbackSpriteAsset.Count > 0;
						if (flag4)
						{
							int fallbackCount = fallbackSpriteAsset.Count;
							for (int i = 0; i < fallbackCount; i++)
							{
								SpriteAsset temp = fallbackSpriteAsset[i];
								bool flag5 = temp == null;
								if (!flag5)
								{
									int id = temp.GetHashCode();
									bool flag6 = !FontAssetUtilities.k_SearchedAssets.Add(id);
									if (!flag6)
									{
										spriteCharacter = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, temp, true);
										bool flag7 = spriteCharacter != null;
										if (flag7)
										{
											return spriteCharacter;
										}
									}
								}
							}
						}
					}
					spriteCharacter2 = null;
				}
			}
			return spriteCharacter2;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000A1C8 File Offset: 0x000083C8
		private static SpriteCharacter GetSpriteCharacterFromSpriteAsset_Internal(uint unicode, SpriteAsset spriteAsset, bool includeFallbacks)
		{
			SpriteCharacter spriteCharacter;
			bool flag = spriteAsset.spriteCharacterLookupTable.TryGetValue(unicode, out spriteCharacter);
			SpriteCharacter spriteCharacter2;
			if (flag)
			{
				spriteCharacter2 = spriteCharacter;
			}
			else
			{
				if (includeFallbacks)
				{
					List<SpriteAsset> fallbackSpriteAsset = spriteAsset.fallbackSpriteAssets;
					bool flag2 = fallbackSpriteAsset != null && fallbackSpriteAsset.Count > 0;
					if (flag2)
					{
						int fallbackCount = fallbackSpriteAsset.Count;
						for (int i = 0; i < fallbackCount; i++)
						{
							SpriteAsset temp = fallbackSpriteAsset[i];
							bool flag3 = temp == null;
							if (!flag3)
							{
								int id = temp.GetHashCode();
								bool flag4 = !FontAssetUtilities.k_SearchedAssets.Add(id);
								if (!flag4)
								{
									spriteCharacter = FontAssetUtilities.GetSpriteCharacterFromSpriteAsset_Internal(unicode, temp, true);
									bool flag5 = spriteCharacter != null;
									if (flag5)
									{
										return spriteCharacter;
									}
								}
							}
						}
					}
				}
				spriteCharacter2 = null;
			}
			return spriteCharacter2;
		}

		// Token: 0x0400013F RID: 319
		private static HashSet<int> k_SearchedAssets;
	}
}
