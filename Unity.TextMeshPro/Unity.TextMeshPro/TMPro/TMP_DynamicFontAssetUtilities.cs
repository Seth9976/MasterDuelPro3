using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000031 RID: 49
	internal class TMP_DynamicFontAssetUtilities
	{
		// Token: 0x0600011C RID: 284 RVA: 0x00005528 File Offset: 0x00003728
		private void InitializeSystemFontReferenceCache()
		{
			if (this.s_SystemFontLookup == null)
			{
				this.s_SystemFontLookup = new Dictionary<ulong, TMP_DynamicFontAssetUtilities.FontReference>();
			}
			else
			{
				this.s_SystemFontLookup.Clear();
			}
			if (this.s_SystemFontPaths == null)
			{
				this.s_SystemFontPaths = Font.GetPathsToOSFonts();
			}
			for (int i = 0; i < this.s_SystemFontPaths.Length; i++)
			{
				FontEngineError error = FontEngine.LoadFontFace(this.s_SystemFontPaths[i]);
				if (error != FontEngineError.Success)
				{
					Debug.LogWarning(string.Concat(new string[]
					{
						"Error [",
						error.ToString(),
						"] trying to load the font at path [",
						this.s_SystemFontPaths[i],
						"]."
					}));
				}
				else
				{
					string[] fontFaces = FontEngine.GetFontFaces();
					for (int j = 0; j < fontFaces.Length; j++)
					{
						TMP_DynamicFontAssetUtilities.FontReference fontRef = new TMP_DynamicFontAssetUtilities.FontReference(this.s_SystemFontPaths[i], fontFaces[j], j);
						if (!this.s_SystemFontLookup.ContainsKey(fontRef.hashCode))
						{
							this.s_SystemFontLookup.Add(fontRef.hashCode, fontRef);
							Debug.Log(string.Concat(new string[]
							{
								"[",
								i.ToString(),
								"] Family Name [",
								fontRef.familyName,
								"]   Style Name [",
								fontRef.styleName,
								"]   Index [",
								fontRef.faceIndex.ToString(),
								"]   HashCode [",
								fontRef.hashCode.ToString(),
								"]    Path [",
								fontRef.filePath,
								"]."
							}));
						}
					}
					FontEngine.UnloadFontFace();
				}
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000056CB File Offset: 0x000038CB
		public static bool TryGetSystemFontReference(string familyName, out TMP_DynamicFontAssetUtilities.FontReference fontRef)
		{
			return TMP_DynamicFontAssetUtilities.s_Instance.TryGetSystemFontReferenceInternal(familyName, null, out fontRef);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000056DA File Offset: 0x000038DA
		public static bool TryGetSystemFontReference(string familyName, string styleName, out TMP_DynamicFontAssetUtilities.FontReference fontRef)
		{
			return TMP_DynamicFontAssetUtilities.s_Instance.TryGetSystemFontReferenceInternal(familyName, styleName, out fontRef);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000056EC File Offset: 0x000038EC
		private bool TryGetSystemFontReferenceInternal(string familyName, string styleName, out TMP_DynamicFontAssetUtilities.FontReference fontRef)
		{
			if (this.s_SystemFontLookup == null)
			{
				this.InitializeSystemFontReferenceCache();
			}
			fontRef = default(TMP_DynamicFontAssetUtilities.FontReference);
			uint familyNameHashCode = TMP_TextUtilities.GetHashCodeCaseInSensitive(familyName);
			uint styleNameHashCode = (string.IsNullOrEmpty(styleName) ? this.s_RegularStyleNameHashCode : TMP_TextUtilities.GetHashCodeCaseInSensitive(styleName));
			ulong key = ((ulong)styleNameHashCode << 32) | (ulong)familyNameHashCode;
			if (this.s_SystemFontLookup.ContainsKey(key))
			{
				fontRef = this.s_SystemFontLookup[key];
				return true;
			}
			if (styleNameHashCode != this.s_RegularStyleNameHashCode)
			{
				return false;
			}
			foreach (KeyValuePair<ulong, TMP_DynamicFontAssetUtilities.FontReference> pair in this.s_SystemFontLookup)
			{
				if (pair.Value.familyName == familyName)
				{
					fontRef = pair.Value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x040000D0 RID: 208
		private static TMP_DynamicFontAssetUtilities s_Instance = new TMP_DynamicFontAssetUtilities();

		// Token: 0x040000D1 RID: 209
		private Dictionary<ulong, TMP_DynamicFontAssetUtilities.FontReference> s_SystemFontLookup;

		// Token: 0x040000D2 RID: 210
		private string[] s_SystemFontPaths;

		// Token: 0x040000D3 RID: 211
		private uint s_RegularStyleNameHashCode = 1291372090U;

		// Token: 0x02000032 RID: 50
		public struct FontReference
		{
			// Token: 0x06000122 RID: 290 RVA: 0x000057EC File Offset: 0x000039EC
			public FontReference(string fontFilePath, string faceNameAndStyle, int index)
			{
				this.familyName = null;
				this.styleName = null;
				this.faceIndex = index;
				uint familyNameHashCode = 0U;
				uint styleNameHashCode = 0U;
				this.filePath = fontFilePath;
				int length = faceNameAndStyle.Length;
				char[] conversionArray = new char[length];
				int readingFlag = 0;
				int writingIndex = 0;
				for (int i = 0; i < length; i++)
				{
					char c = faceNameAndStyle[i];
					if (readingFlag == 0)
					{
						if (i + 2 < length && c == ' ' && faceNameAndStyle[i + 1] == '-' && faceNameAndStyle[i + 2] == ' ')
						{
							readingFlag = 1;
							this.familyName = new string(conversionArray, 0, writingIndex);
							i += 2;
							writingIndex = 0;
						}
						else
						{
							familyNameHashCode = ((familyNameHashCode << 5) + familyNameHashCode) ^ (uint)TMP_TextUtilities.ToUpperFast(c);
							conversionArray[writingIndex++] = c;
						}
					}
					else if (readingFlag == 1)
					{
						styleNameHashCode = ((styleNameHashCode << 5) + styleNameHashCode) ^ (uint)TMP_TextUtilities.ToUpperFast(c);
						conversionArray[writingIndex++] = c;
						if (i + 1 == length)
						{
							this.styleName = new string(conversionArray, 0, writingIndex);
						}
					}
				}
				this.hashCode = ((ulong)styleNameHashCode << 32) | (ulong)familyNameHashCode;
			}

			// Token: 0x040000D4 RID: 212
			public string familyName;

			// Token: 0x040000D5 RID: 213
			public string styleName;

			// Token: 0x040000D6 RID: 214
			public int faceIndex;

			// Token: 0x040000D7 RID: 215
			public string filePath;

			// Token: 0x040000D8 RID: 216
			public ulong hashCode;
		}
	}
}
