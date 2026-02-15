using System;
using System.Collections.Generic;

namespace TMPro
{
	// Token: 0x02000042 RID: 66
	public static class TMP_FontUtilities
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00009300 File Offset: 0x00007500
		public static TMP_FontAsset SearchForCharacter(TMP_FontAsset font, uint unicode, out TMP_Character character)
		{
			if (TMP_FontUtilities.k_searchedFontAssets == null)
			{
				TMP_FontUtilities.k_searchedFontAssets = new List<int>();
			}
			TMP_FontUtilities.k_searchedFontAssets.Clear();
			return TMP_FontUtilities.SearchForCharacterInternal(font, unicode, out character);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009325 File Offset: 0x00007525
		public static TMP_FontAsset SearchForCharacter(List<TMP_FontAsset> fonts, uint unicode, out TMP_Character character)
		{
			return TMP_FontUtilities.SearchForCharacterInternal(fonts, unicode, out character);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009330 File Offset: 0x00007530
		private static TMP_FontAsset SearchForCharacterInternal(TMP_FontAsset font, uint unicode, out TMP_Character character)
		{
			character = null;
			if (font == null)
			{
				return null;
			}
			if (font.characterLookupTable.TryGetValue(unicode, out character))
			{
				if (character.textAsset != null)
				{
					return font;
				}
				font.characterLookupTable.Remove(unicode);
			}
			if (font.fallbackFontAssetTable != null && font.fallbackFontAssetTable.Count > 0)
			{
				int i = 0;
				while (i < font.fallbackFontAssetTable.Count && character == null)
				{
					TMP_FontAsset temp = font.fallbackFontAssetTable[i];
					if (!(temp == null))
					{
						int id = temp.GetInstanceID();
						if (!TMP_FontUtilities.k_searchedFontAssets.Contains(id))
						{
							TMP_FontUtilities.k_searchedFontAssets.Add(id);
							temp = TMP_FontUtilities.SearchForCharacterInternal(temp, unicode, out character);
							if (temp != null)
							{
								return temp;
							}
						}
					}
					i++;
				}
			}
			return null;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000093F4 File Offset: 0x000075F4
		private static TMP_FontAsset SearchForCharacterInternal(List<TMP_FontAsset> fonts, uint unicode, out TMP_Character character)
		{
			character = null;
			if (fonts != null && fonts.Count > 0)
			{
				for (int i = 0; i < fonts.Count; i++)
				{
					TMP_FontAsset fontAsset = TMP_FontUtilities.SearchForCharacterInternal(fonts[i], unicode, out character);
					if (fontAsset != null)
					{
						return fontAsset;
					}
				}
			}
			return null;
		}

		// Token: 0x04000165 RID: 357
		private static List<int> k_searchedFontAssets;
	}
}
