using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000065 RID: 101
	internal class TextResourceManager
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x0002F6C4 File Offset: 0x0002D8C4
		internal static void AddFontAsset(FontAsset fontAsset)
		{
			int instanceID = fontAsset.instanceID;
			bool flag = !TextResourceManager.s_FontAssetReferences.ContainsKey(instanceID);
			if (flag)
			{
				TextResourceManager.FontAssetRef fontAssetRef = new TextResourceManager.FontAssetRef(fontAsset.hashCode, fontAsset.familyNameHashCode, fontAsset.styleNameHashCode, fontAsset);
				TextResourceManager.s_FontAssetReferences.Add(instanceID, fontAssetRef);
				bool flag2 = !TextResourceManager.s_FontAssetNameReferenceLookup.ContainsKey(fontAssetRef.nameHashCode);
				if (flag2)
				{
					TextResourceManager.s_FontAssetNameReferenceLookup.Add(fontAssetRef.nameHashCode, fontAsset);
				}
				bool flag3 = !TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.ContainsKey(fontAssetRef.familyNameAndStyleHashCode);
				if (flag3)
				{
					TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Add(fontAssetRef.familyNameAndStyleHashCode, fontAsset);
				}
			}
			else
			{
				TextResourceManager.FontAssetRef fontAssetRef2 = TextResourceManager.s_FontAssetReferences[instanceID];
				bool flag4 = fontAssetRef2.nameHashCode == fontAsset.hashCode && fontAssetRef2.familyNameHashCode == fontAsset.familyNameHashCode && fontAssetRef2.styleNameHashCode == fontAsset.styleNameHashCode;
				if (!flag4)
				{
					bool flag5 = fontAssetRef2.nameHashCode != fontAsset.hashCode;
					if (flag5)
					{
						TextResourceManager.s_FontAssetNameReferenceLookup.Remove(fontAssetRef2.nameHashCode);
						fontAssetRef2.nameHashCode = fontAsset.hashCode;
						bool flag6 = !TextResourceManager.s_FontAssetNameReferenceLookup.ContainsKey(fontAssetRef2.nameHashCode);
						if (flag6)
						{
							TextResourceManager.s_FontAssetNameReferenceLookup.Add(fontAssetRef2.nameHashCode, fontAsset);
						}
					}
					bool flag7 = fontAssetRef2.familyNameHashCode != fontAsset.familyNameHashCode || fontAssetRef2.styleNameHashCode != fontAsset.styleNameHashCode;
					if (flag7)
					{
						TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Remove(fontAssetRef2.familyNameAndStyleHashCode);
						fontAssetRef2.familyNameHashCode = fontAsset.familyNameHashCode;
						fontAssetRef2.styleNameHashCode = fontAsset.styleNameHashCode;
						fontAssetRef2.familyNameAndStyleHashCode = ((long)fontAsset.styleNameHashCode << 32) | (long)((ulong)fontAsset.familyNameHashCode);
						bool flag8 = !TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.ContainsKey(fontAssetRef2.familyNameAndStyleHashCode);
						if (flag8)
						{
							TextResourceManager.s_FontAssetFamilyNameAndStyleReferenceLookup.Add(fontAssetRef2.familyNameAndStyleHashCode, fontAsset);
						}
					}
					TextResourceManager.s_FontAssetReferences[instanceID] = fontAssetRef2;
				}
			}
		}

		// Token: 0x0400044A RID: 1098
		private static readonly Dictionary<int, TextResourceManager.FontAssetRef> s_FontAssetReferences = new Dictionary<int, TextResourceManager.FontAssetRef>();

		// Token: 0x0400044B RID: 1099
		private static readonly Dictionary<int, FontAsset> s_FontAssetNameReferenceLookup = new Dictionary<int, FontAsset>();

		// Token: 0x0400044C RID: 1100
		private static readonly Dictionary<long, FontAsset> s_FontAssetFamilyNameAndStyleReferenceLookup = new Dictionary<long, FontAsset>();

		// Token: 0x0400044D RID: 1101
		private static readonly List<int> s_FontAssetRemovalList = new List<int>(16);

		// Token: 0x0400044E RID: 1102
		private static readonly int k_RegularStyleHashCode = TextUtilities.GetHashCodeCaseInSensitive("Regular");

		// Token: 0x02000066 RID: 102
		private struct FontAssetRef
		{
			// Token: 0x060002D3 RID: 723 RVA: 0x0002F907 File Offset: 0x0002DB07
			public FontAssetRef(int nameHashCode, int familyNameHashCode, int styleNameHashCode, FontAsset fontAsset)
			{
				this.nameHashCode = ((nameHashCode != 0) ? nameHashCode : familyNameHashCode);
				this.familyNameHashCode = familyNameHashCode;
				this.styleNameHashCode = styleNameHashCode;
				this.familyNameAndStyleHashCode = ((long)styleNameHashCode << 32) | (long)((ulong)familyNameHashCode);
				this.fontAsset = fontAsset;
			}

			// Token: 0x0400044F RID: 1103
			public int nameHashCode;

			// Token: 0x04000450 RID: 1104
			public int familyNameHashCode;

			// Token: 0x04000451 RID: 1105
			public int styleNameHashCode;

			// Token: 0x04000452 RID: 1106
			public long familyNameAndStyleHashCode;

			// Token: 0x04000453 RID: 1107
			public readonly FontAsset fontAsset;
		}
	}
}
