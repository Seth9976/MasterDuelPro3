using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000068 RID: 104
	public class TMP_ResourceManager
	{
		// Token: 0x06000344 RID: 836 RVA: 0x00011FA5 File Offset: 0x000101A5
		internal static TMP_Settings GetTextSettings()
		{
			if (TMP_ResourceManager.s_TextSettings == null)
			{
				TMP_ResourceManager.s_TextSettings = Resources.Load<TMP_Settings>("TextSettings");
			}
			return TMP_ResourceManager.s_TextSettings;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00011FC8 File Offset: 0x000101C8
		public static void AddFontAsset(TMP_FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			if (!TMP_ResourceManager.s_FontAssetReferences.ContainsKey(instanceID))
			{
				TMP_ResourceManager.FontAssetRef fontAssetRef = new TMP_ResourceManager.FontAssetRef(fontAsset.hashCode, fontAsset.familyNameHashCode, fontAsset.styleNameHashCode, fontAsset);
				TMP_ResourceManager.s_FontAssetReferences.Add(instanceID, fontAssetRef);
				if (!TMP_ResourceManager.s_FontAssetNameReferenceLookup.ContainsKey(fontAssetRef.nameHashCode))
				{
					TMP_ResourceManager.s_FontAssetNameReferenceLookup.Add(fontAssetRef.nameHashCode, fontAsset);
				}
				if (!TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.ContainsKey(fontAssetRef.familyNameAndStyleHashCode))
				{
					TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Add(fontAssetRef.familyNameAndStyleHashCode, fontAsset);
					return;
				}
			}
			else
			{
				TMP_ResourceManager.FontAssetRef fontAssetRef2 = TMP_ResourceManager.s_FontAssetReferences[instanceID];
				if (fontAssetRef2.nameHashCode == fontAsset.hashCode && fontAssetRef2.familyNameHashCode == fontAsset.familyNameHashCode && fontAssetRef2.styleNameHashCode == fontAsset.styleNameHashCode)
				{
					return;
				}
				if (fontAssetRef2.nameHashCode != fontAsset.hashCode)
				{
					TMP_ResourceManager.s_FontAssetNameReferenceLookup.Remove(fontAssetRef2.nameHashCode);
					fontAssetRef2.nameHashCode = fontAsset.hashCode;
					if (!TMP_ResourceManager.s_FontAssetNameReferenceLookup.ContainsKey(fontAssetRef2.nameHashCode))
					{
						TMP_ResourceManager.s_FontAssetNameReferenceLookup.Add(fontAssetRef2.nameHashCode, fontAsset);
					}
				}
				if (fontAssetRef2.familyNameHashCode != fontAsset.familyNameHashCode || fontAssetRef2.styleNameHashCode != fontAsset.styleNameHashCode)
				{
					TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(fontAssetRef2.familyNameAndStyleHashCode);
					fontAssetRef2.familyNameHashCode = fontAsset.familyNameHashCode;
					fontAssetRef2.styleNameHashCode = fontAsset.styleNameHashCode;
					fontAssetRef2.familyNameAndStyleHashCode = ((long)fontAsset.styleNameHashCode << 32) | (long)((ulong)fontAsset.familyNameHashCode);
					if (!TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.ContainsKey(fontAssetRef2.familyNameAndStyleHashCode))
					{
						TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Add(fontAssetRef2.familyNameAndStyleHashCode, fontAsset);
					}
				}
				TMP_ResourceManager.s_FontAssetReferences[instanceID] = fontAssetRef2;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00012170 File Offset: 0x00010370
		public static void RemoveFontAsset(TMP_FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			TMP_ResourceManager.FontAssetRef reference;
			if (TMP_ResourceManager.s_FontAssetReferences.TryGetValue(instanceID, out reference))
			{
				TMP_ResourceManager.s_FontAssetNameReferenceLookup.Remove(reference.nameHashCode);
				TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(reference.familyNameAndStyleHashCode);
				TMP_ResourceManager.s_FontAssetReferences.Remove(instanceID);
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000121C1 File Offset: 0x000103C1
		internal static bool TryGetFontAssetByName(int nameHashcode, out TMP_FontAsset fontAsset)
		{
			fontAsset = null;
			return TMP_ResourceManager.s_FontAssetNameReferenceLookup.TryGetValue(nameHashcode, out fontAsset);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000121D4 File Offset: 0x000103D4
		internal static bool TryGetFontAssetByFamilyName(int familyNameHashCode, int styleNameHashCode, out TMP_FontAsset fontAsset)
		{
			fontAsset = null;
			if (styleNameHashCode == 0)
			{
				styleNameHashCode = TMP_ResourceManager.k_RegularStyleHashCode;
			}
			long familyAndStyleNameHashCode = ((long)styleNameHashCode << 32) | (long)((ulong)familyNameHashCode);
			return TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.TryGetValue(familyAndStyleNameHashCode, out fontAsset);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00012203 File Offset: 0x00010403
		public static void ClearFontAssetGlyphCache()
		{
			TMP_ResourceManager.RebuildFontAssetCache();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0001220C File Offset: 0x0001040C
		internal static void RebuildFontAssetCache()
		{
			foreach (KeyValuePair<int, TMP_ResourceManager.FontAssetRef> pair in TMP_ResourceManager.s_FontAssetReferences)
			{
				TMP_ResourceManager.FontAssetRef fontAssetRef = pair.Value;
				TMP_FontAsset fontAsset = fontAssetRef.fontAsset;
				if (fontAsset == null)
				{
					TMP_ResourceManager.s_FontAssetNameReferenceLookup.Remove(fontAssetRef.nameHashCode);
					TMP_ResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(fontAssetRef.familyNameAndStyleHashCode);
					TMP_ResourceManager.s_FontAssetRemovalList.Add(pair.Key);
				}
				else
				{
					fontAsset.InitializeCharacterLookupDictionary();
					fontAsset.AddSynthesizedCharactersAndFaceMetrics();
				}
			}
			for (int i = 0; i < TMP_ResourceManager.s_FontAssetRemovalList.Count; i++)
			{
				TMP_ResourceManager.s_FontAssetReferences.Remove(TMP_ResourceManager.s_FontAssetRemovalList[i]);
			}
			TMP_ResourceManager.s_FontAssetRemovalList.Clear();
			TMPro_EventManager.ON_FONT_PROPERTY_CHANGED(true, null);
		}

		// Token: 0x04000253 RID: 595
		private static TMP_Settings s_TextSettings;

		// Token: 0x04000254 RID: 596
		private static readonly Dictionary<int, TMP_ResourceManager.FontAssetRef> s_FontAssetReferences = new Dictionary<int, TMP_ResourceManager.FontAssetRef>();

		// Token: 0x04000255 RID: 597
		private static readonly Dictionary<int, TMP_FontAsset> s_FontAssetNameReferenceLookup = new Dictionary<int, TMP_FontAsset>();

		// Token: 0x04000256 RID: 598
		private static readonly Dictionary<long, TMP_FontAsset> s_FontAssetFamilyNameAndStyleReferenceLookup = new Dictionary<long, TMP_FontAsset>();

		// Token: 0x04000257 RID: 599
		private static readonly List<int> s_FontAssetRemovalList = new List<int>(16);

		// Token: 0x04000258 RID: 600
		private static readonly int k_RegularStyleHashCode = TMP_TextUtilities.GetHashCode("Regular");

		// Token: 0x02000069 RID: 105
		private struct FontAssetRef
		{
			// Token: 0x0600034D RID: 845 RVA: 0x0001232F File Offset: 0x0001052F
			public FontAssetRef(int nameHashCode, int familyNameHashCode, int styleNameHashCode, TMP_FontAsset fontAsset)
			{
				this.nameHashCode = ((nameHashCode != 0) ? nameHashCode : familyNameHashCode);
				this.familyNameHashCode = familyNameHashCode;
				this.styleNameHashCode = styleNameHashCode;
				this.familyNameAndStyleHashCode = ((long)styleNameHashCode << 32) | (long)((ulong)familyNameHashCode);
				this.fontAsset = fontAsset;
			}

			// Token: 0x04000259 RID: 601
			public int nameHashCode;

			// Token: 0x0400025A RID: 602
			public int familyNameHashCode;

			// Token: 0x0400025B RID: 603
			public int styleNameHashCode;

			// Token: 0x0400025C RID: 604
			public long familyNameAndStyleHashCode;

			// Token: 0x0400025D RID: 605
			public readonly TMP_FontAsset fontAsset;
		}
	}
}
