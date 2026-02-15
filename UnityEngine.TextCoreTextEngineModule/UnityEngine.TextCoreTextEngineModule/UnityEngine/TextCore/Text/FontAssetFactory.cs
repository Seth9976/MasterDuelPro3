using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200002E RID: 46
	[NullableContext(1)]
	[Nullable(0)]
	internal class FontAssetFactory
	{
		// Token: 0x06000117 RID: 279 RVA: 0x000096C4 File Offset: 0x000078C4
		[return: Nullable(2)]
		internal static FontAsset CreateDefaultEditorFontAsset(Font font, Shader shader)
		{
			bool flag = font == null;
			FontAsset fontAsset2;
			if (flag)
			{
				fontAsset2 = null;
			}
			else
			{
				bool flag2 = font.name == "System Normal" || font.name == "System Small" || font.name == "System Big" || font.name == "System Warning";
				FontAsset fontAsset;
				if (flag2)
				{
					fontAsset = FontAsset.CreateFontAssetInternal(FontAssetFactory.k_SystemFontName, "Regular", 90);
					bool flag3 = fontAsset != null;
					if (flag3)
					{
						fontAsset.InternalDynamicOS = true;
						FontAsset boldFontAsset = FontAsset.CreateFontAssetInternal(FontAssetFactory.k_SystemFontName, "Bold", 90);
						bool flag4 = boldFontAsset != null;
						if (flag4)
						{
							boldFontAsset.InternalDynamicOS = true;
							fontAsset.fontWeightTable[7].regularTypeface = boldFontAsset;
							FontAssetFactory.SetupFontAssetSettings(boldFontAsset, shader);
						}
						FontAsset boldItalicFontAsset = FontAsset.CreateFontAssetInternal(FontAssetFactory.k_SystemFontName, "Bold Italic", 90);
						bool flag5 = boldItalicFontAsset != null;
						if (flag5)
						{
							boldItalicFontAsset.InternalDynamicOS = true;
							fontAsset.fontWeightTable[7].italicTypeface = boldItalicFontAsset;
							FontAssetFactory.SetupFontAssetSettings(boldItalicFontAsset, shader);
						}
						FontAsset italicFontAsset = FontAsset.CreateFontAssetInternal(FontAssetFactory.k_SystemFontName, "Italic", 90);
						bool flag6 = italicFontAsset != null;
						if (flag6)
						{
							italicFontAsset.InternalDynamicOS = true;
							fontAsset.fontWeightTable[4].italicTypeface = italicFontAsset;
							FontAssetFactory.SetupFontAssetSettings(italicFontAsset, shader);
						}
					}
				}
				else
				{
					bool flag7 = font.name == "System Normal Bold" || font.name == "System Small Bold";
					if (flag7)
					{
						fontAsset = FontAsset.CreateFontAssetInternal(FontAssetFactory.k_SystemFontName, "Bold", 90);
						bool flag8 = fontAsset != null;
						if (flag8)
						{
							fontAsset.InternalDynamicOS = true;
						}
					}
					else
					{
						bool flag9 = font.name == "Inter-Regular";
						if (flag9)
						{
							fontAsset = FontAsset.CreateFontAssetInternal("Inter", "Regular", 90);
							bool flag10 = fontAsset != null;
							if (flag10)
							{
								fontAsset.InternalDynamicOS = true;
								FontAsset boldFontAsset2 = FontAsset.CreateFontAssetInternal("Inter", "Bold", 90);
								bool flag11 = boldFontAsset2 != null;
								if (flag11)
								{
									boldFontAsset2.InternalDynamicOS = true;
									fontAsset.fontWeightTable[7].regularTypeface = boldFontAsset2;
									FontAssetFactory.SetupFontAssetSettings(boldFontAsset2, shader);
								}
								FontAsset italicFontAsset2 = FontAsset.CreateFontAssetInternal("Inter", "Italic", 90);
								bool flag12 = italicFontAsset2 != null;
								if (flag12)
								{
									italicFontAsset2.InternalDynamicOS = true;
									fontAsset.fontWeightTable[4].italicTypeface = italicFontAsset2;
									FontAssetFactory.SetupFontAssetSettings(italicFontAsset2, shader);
								}
								FontAsset boldItalicFontAsset2 = FontAsset.CreateFontAssetInternal("Inter", "Bold Italic", 90);
								bool flag13 = boldItalicFontAsset2 != null;
								if (flag13)
								{
									boldItalicFontAsset2.InternalDynamicOS = true;
									fontAsset.fontWeightTable[7].italicTypeface = boldItalicFontAsset2;
									FontAssetFactory.SetupFontAssetSettings(boldItalicFontAsset2, shader);
								}
							}
						}
						else
						{
							fontAsset = FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024, shader, AtlasPopulationMode.Dynamic, true);
						}
					}
				}
				bool flag14 = fontAsset != null;
				if (flag14)
				{
					FontAssetFactory.SetupFontAssetSettings(fontAsset, shader);
				}
				fontAsset2 = fontAsset;
			}
			return fontAsset2;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000099F0 File Offset: 0x00007BF0
		private static void SetupFontAssetSettings(FontAsset fontAsset, Shader shader)
		{
			bool flag = !fontAsset;
			if (!flag)
			{
				FontAssetFactory.SetHideFlags(fontAsset);
				fontAsset.material.shader = shader;
				fontAsset.isMultiAtlasTexturesEnabled = true;
				fontAsset.IsEditorFont = true;
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009A30 File Offset: 0x00007C30
		public static void SetHideFlags(FontAsset fontAsset)
		{
			bool flag = !fontAsset;
			if (!flag)
			{
				fontAsset.hideFlags = HideFlags.DontSave;
				fontAsset.atlasTextures[0].hideFlags = HideFlags.DontSave;
				fontAsset.material.hideFlags = HideFlags.DontSave;
			}
		}

		// Token: 0x0400013D RID: 317
		private static readonly HashSet<FontAsset> visitedFontAssets = new HashSet<FontAsset>();

		// Token: 0x0400013E RID: 318
		private static readonly string k_SystemFontName = "Lucida Grande";
	}
}
