using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000432 RID: 1074
	[HelpURL("UIE-USS")]
	[Serializable]
	public class StyleSheet : ScriptableObject
	{
		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x00070F0C File Offset: 0x0006F10C
		// (set) Token: 0x06001F0E RID: 7950 RVA: 0x00070F24 File Offset: 0x0006F124
		public bool importedWithErrors
		{
			get
			{
				return this.m_ImportedWithErrors;
			}
			internal set
			{
				this.m_ImportedWithErrors = value;
			}
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06001F0F RID: 7951 RVA: 0x00070F30 File Offset: 0x0006F130
		// (set) Token: 0x06001F10 RID: 7952 RVA: 0x00070F48 File Offset: 0x0006F148
		public bool importedWithWarnings
		{
			get
			{
				return this.m_ImportedWithWarnings;
			}
			internal set
			{
				this.m_ImportedWithWarnings = value;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x00070F54 File Offset: 0x0006F154
		// (set) Token: 0x06001F12 RID: 7954 RVA: 0x00070F6C File Offset: 0x0006F16C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleRule[] rules
		{
			get
			{
				return this.m_Rules;
			}
			set
			{
				this.m_Rules = value;
				this.SetupReferences();
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x00070F80 File Offset: 0x0006F180
		// (set) Token: 0x06001F14 RID: 7956 RVA: 0x00070F98 File Offset: 0x0006F198
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleComplexSelector[] complexSelectors
		{
			get
			{
				return this.m_ComplexSelectors;
			}
			set
			{
				this.m_ComplexSelectors = value;
				this.SetupReferences();
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x00070FAC File Offset: 0x0006F1AC
		internal List<StyleSheet> flattenedRecursiveImports
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.m_FlattenedImportedStyleSheets;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x00070FC4 File Offset: 0x0006F1C4
		// (set) Token: 0x06001F17 RID: 7959 RVA: 0x00070FDC File Offset: 0x0006F1DC
		public int contentHash
		{
			get
			{
				return this.m_ContentHash;
			}
			set
			{
				this.m_ContentHash = value;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x00070FE8 File Offset: 0x0006F1E8
		// (set) Token: 0x06001F19 RID: 7961 RVA: 0x00071000 File Offset: 0x0006F200
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool isDefaultStyleSheet
		{
			get
			{
				return this.m_IsDefaultStyleSheet;
			}
			set
			{
				this.m_IsDefaultStyleSheet = value;
				bool flag = this.flattenedRecursiveImports != null;
				if (flag)
				{
					foreach (StyleSheet importedStyleSheet in this.flattenedRecursiveImports)
					{
						importedStyleSheet.isDefaultStyleSheet = value;
					}
				}
			}
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x00071070 File Offset: 0x0006F270
		private bool TryCheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle, out T value)
		{
			bool result = false;
			value = default(T);
			bool flag = handle.valueType == type && handle.valueIndex >= 0 && handle.valueIndex < list.Length;
			if (flag)
			{
				value = list[handle.valueIndex];
				result = true;
			}
			else
			{
				Debug.LogErrorFormat(this, "Trying to read value of type {0} while reading a value of type {1}", new object[] { type, handle.valueType });
			}
			return result;
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x000710F8 File Offset: 0x0006F2F8
		private T CheckAccess<T>(T[] list, StyleValueType type, StyleValueHandle handle)
		{
			T value = default(T);
			bool flag = handle.valueType != type;
			if (flag)
			{
				Debug.LogErrorFormat(this, "Trying to read value of type {0} while reading a value of type {1}", new object[] { type, handle.valueType });
			}
			else
			{
				bool flag2 = list == null || handle.valueIndex < 0 || handle.valueIndex >= list.Length;
				if (flag2)
				{
					Debug.LogError("Accessing invalid property", this);
				}
				else
				{
					value = list[handle.valueIndex];
				}
			}
			return value;
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x00071192 File Offset: 0x0006F392
		internal virtual void OnEnable()
		{
			this.SetupReferences();
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x0007119C File Offset: 0x0006F39C
		internal void FlattenImportedStyleSheetsRecursive()
		{
			this.m_FlattenedImportedStyleSheets = new List<StyleSheet>();
			this.FlattenImportedStyleSheetsRecursive(this);
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x000711B4 File Offset: 0x0006F3B4
		private void FlattenImportedStyleSheetsRecursive(StyleSheet sheet)
		{
			bool flag = sheet.imports == null;
			if (!flag)
			{
				for (int i = 0; i < sheet.imports.Length; i++)
				{
					StyleSheet importedStyleSheet = sheet.imports[i].styleSheet;
					bool flag2 = importedStyleSheet == null;
					if (!flag2)
					{
						importedStyleSheet.isDefaultStyleSheet = this.isDefaultStyleSheet;
						this.FlattenImportedStyleSheetsRecursive(importedStyleSheet);
						this.m_FlattenedImportedStyleSheets.Add(importedStyleSheet);
					}
				}
			}
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x00071230 File Offset: 0x0006F430
		private void SetupReferences()
		{
			bool flag = this.complexSelectors == null || this.rules == null;
			if (!flag)
			{
				foreach (StyleRule rule in this.rules)
				{
					foreach (StyleProperty property in rule.properties)
					{
						bool flag2 = StyleSheet.CustomStartsWith(property.name, StyleSheet.kCustomPropertyMarker);
						if (flag2)
						{
							rule.customPropertiesCount++;
							property.isCustomProperty = true;
						}
						foreach (StyleValueHandle handle in property.values)
						{
							bool flag3 = handle.IsVarFunction();
							if (flag3)
							{
								property.requireVariableResolve = true;
								break;
							}
						}
					}
				}
				int i = 0;
				int count = this.complexSelectors.Length;
				while (i < count)
				{
					this.complexSelectors[i].CachePseudoStateMasks();
					i++;
				}
				this.orderedClassSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.orderedNameSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				this.orderedTypeSelectors = new Dictionary<string, StyleComplexSelector>(StringComparer.Ordinal);
				int j = 0;
				while (j < this.complexSelectors.Length)
				{
					StyleComplexSelector complexSel = this.complexSelectors[j];
					bool flag4 = complexSel.ruleIndex < this.rules.Length;
					if (flag4)
					{
						complexSel.rule = this.rules[complexSel.ruleIndex];
					}
					complexSel.CalculateHashes();
					complexSel.orderInStyleSheet = j;
					StyleSelector lastSelector = complexSel.selectors[complexSel.selectors.Length - 1];
					StyleSelectorPart part = lastSelector.parts[0];
					string key = part.value;
					Dictionary<string, StyleComplexSelector> tableToUse = null;
					switch (part.type)
					{
					case StyleSelectorType.Wildcard:
					case StyleSelectorType.Type:
						key = part.value ?? "*";
						tableToUse = this.orderedTypeSelectors;
						break;
					case StyleSelectorType.Class:
						tableToUse = this.orderedClassSelectors;
						break;
					case StyleSelectorType.PseudoClass:
						key = "*";
						tableToUse = this.orderedTypeSelectors;
						break;
					case StyleSelectorType.RecursivePseudoClass:
						goto IL_0233;
					case StyleSelectorType.ID:
						tableToUse = this.orderedNameSelectors;
						break;
					default:
						goto IL_0233;
					}
					IL_0252:
					bool flag5 = tableToUse != null;
					if (flag5)
					{
						StyleComplexSelector previous;
						bool flag6 = tableToUse.TryGetValue(key, out previous);
						if (flag6)
						{
							complexSel.nextInTable = previous;
						}
						tableToUse[key] = complexSel;
					}
					j++;
					continue;
					IL_0233:
					Debug.LogError(string.Format("Invalid first part type {0}", part.type), this);
					goto IL_0252;
				}
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000714E0 File Offset: 0x0006F6E0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleValueKeyword ReadKeyword(StyleValueHandle handle)
		{
			return (StyleValueKeyword)handle.valueIndex;
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x000714F8 File Offset: 0x0006F6F8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal float ReadFloat(StyleValueHandle handle)
		{
			bool flag = handle.valueType == StyleValueType.Dimension;
			float num;
			if (flag)
			{
				Dimension dimension = this.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);
				num = dimension.value;
			}
			else
			{
				num = this.CheckAccess<float>(this.floats, StyleValueType.Float, handle);
			}
			return num;
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x00071540 File Offset: 0x0006F740
		internal bool TryReadFloat(StyleValueHandle handle, out float value)
		{
			bool flag = this.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out value);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Dimension dimensionValue;
				bool isDimension = this.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Float, handle, out dimensionValue);
				value = dimensionValue.value;
				flag2 = isDimension;
			}
			return flag2;
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x00071584 File Offset: 0x0006F784
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Dimension ReadDimension(StyleValueHandle handle)
		{
			bool flag = handle.valueType == StyleValueType.Float;
			Dimension dimension;
			if (flag)
			{
				float value = this.CheckAccess<float>(this.floats, StyleValueType.Float, handle);
				dimension = new Dimension(value, Dimension.Unit.Unitless);
			}
			else
			{
				dimension = this.CheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle);
			}
			return dimension;
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x000715CC File Offset: 0x0006F7CC
		internal bool TryReadDimension(StyleValueHandle handle, out Dimension value)
		{
			bool flag = this.TryCheckAccess<Dimension>(this.dimensions, StyleValueType.Dimension, handle, out value);
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				float floatValue = 0f;
				bool isFloat = this.TryCheckAccess<float>(this.floats, StyleValueType.Float, handle, out floatValue);
				value = new Dimension(floatValue, Dimension.Unit.Unitless);
				flag2 = isFloat;
			}
			return flag2;
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x0007161C File Offset: 0x0006F81C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Color ReadColor(StyleValueHandle handle)
		{
			return this.CheckAccess<Color>(this.colors, StyleValueType.Color, handle);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0007163C File Offset: 0x0006F83C
		internal bool TryReadColor(StyleValueHandle handle, out Color value)
		{
			return this.TryCheckAccess<Color>(this.colors, StyleValueType.Color, handle, out value);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x00071660 File Offset: 0x0006F860
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string ReadString(StyleValueHandle handle)
		{
			return this.CheckAccess<string>(this.strings, StyleValueType.String, handle);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x00071684 File Offset: 0x0006F884
		internal bool TryReadString(StyleValueHandle handle, out string value)
		{
			return this.TryCheckAccess<string>(this.strings, StyleValueType.String, handle, out value);
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x000716A8 File Offset: 0x0006F8A8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string ReadEnum(StyleValueHandle handle)
		{
			return this.CheckAccess<string>(this.strings, StyleValueType.Enum, handle);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x000716C8 File Offset: 0x0006F8C8
		internal bool TryReadEnum(StyleValueHandle handle, out string value)
		{
			return this.TryCheckAccess<string>(this.strings, StyleValueType.Enum, handle, out value);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x000716EC File Offset: 0x0006F8EC
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string ReadVariable(StyleValueHandle handle)
		{
			return this.CheckAccess<string>(this.strings, StyleValueType.Variable, handle);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0007170C File Offset: 0x0006F90C
		internal bool TryReadVariable(StyleValueHandle handle, out string value)
		{
			return this.TryCheckAccess<string>(this.strings, StyleValueType.Variable, handle, out value);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x00071730 File Offset: 0x0006F930
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string ReadResourcePath(StyleValueHandle handle)
		{
			return this.CheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00071750 File Offset: 0x0006F950
		internal bool TryReadResourcePath(StyleValueHandle handle, out string value)
		{
			return this.TryCheckAccess<string>(this.strings, StyleValueType.ResourcePath, handle, out value);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00071774 File Offset: 0x0006F974
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Object ReadAssetReference(StyleValueHandle handle)
		{
			return this.CheckAccess<Object>(this.assets, StyleValueType.AssetReference, handle);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00071794 File Offset: 0x0006F994
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string ReadMissingAssetReferenceUrl(StyleValueHandle handle)
		{
			return this.CheckAccess<string>(this.strings, StyleValueType.MissingAssetReference, handle);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x000717B8 File Offset: 0x0006F9B8
		internal bool TryReadAssetReference(StyleValueHandle handle, out Object value)
		{
			return this.TryCheckAccess<Object>(this.assets, StyleValueType.AssetReference, handle, out value);
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x000717DC File Offset: 0x0006F9DC
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleValueFunction ReadFunction(StyleValueHandle handle)
		{
			return (StyleValueFunction)handle.valueIndex;
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x000717F4 File Offset: 0x0006F9F4
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string ReadFunctionName(StyleValueHandle handle)
		{
			bool flag = handle.valueType != StyleValueType.Function;
			string text;
			if (flag)
			{
				Debug.LogErrorFormat(this, string.Format("Trying to read value of type {0} while reading a value of type {1}", StyleValueType.Function, handle.valueType), Array.Empty<object>());
				text = string.Empty;
			}
			else
			{
				StyleValueFunction svf = (StyleValueFunction)handle.valueIndex;
				text = svf.ToUssString();
			}
			return text;
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00071858 File Offset: 0x0006FA58
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal ScalableImage ReadScalableImage(StyleValueHandle handle)
		{
			return this.CheckAccess<ScalableImage>(this.scalableImages, StyleValueType.ScalableImage, handle);
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x0007187C File Offset: 0x0006FA7C
		private static bool CustomStartsWith(string originalString, string pattern)
		{
			int originalLength = originalString.Length;
			int patternLength = pattern.Length;
			int originalPos = 0;
			int patternPos = 0;
			while (originalPos < originalLength && patternPos < patternLength && originalString[originalPos] == pattern[patternPos])
			{
				originalPos++;
				patternPos++;
			}
			return (patternPos == patternLength && originalLength >= patternLength) || (originalPos == originalLength && patternLength >= originalLength);
		}

		// Token: 0x04000D9D RID: 3485
		[SerializeField]
		private bool m_ImportedWithErrors;

		// Token: 0x04000D9E RID: 3486
		[SerializeField]
		private bool m_ImportedWithWarnings;

		// Token: 0x04000D9F RID: 3487
		[SerializeField]
		private StyleRule[] m_Rules;

		// Token: 0x04000DA0 RID: 3488
		[SerializeField]
		private StyleComplexSelector[] m_ComplexSelectors;

		// Token: 0x04000DA1 RID: 3489
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal float[] floats;

		// Token: 0x04000DA2 RID: 3490
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Dimension[] dimensions;

		// Token: 0x04000DA3 RID: 3491
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Color[] colors;

		// Token: 0x04000DA4 RID: 3492
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[SerializeField]
		internal string[] strings;

		// Token: 0x04000DA5 RID: 3493
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal Object[] assets;

		// Token: 0x04000DA6 RID: 3494
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleSheet.ImportStruct[] imports;

		// Token: 0x04000DA7 RID: 3495
		[SerializeField]
		private List<StyleSheet> m_FlattenedImportedStyleSheets;

		// Token: 0x04000DA8 RID: 3496
		[SerializeField]
		private int m_ContentHash;

		// Token: 0x04000DA9 RID: 3497
		[SerializeField]
		internal ScalableImage[] scalableImages;

		// Token: 0x04000DAA RID: 3498
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedNameSelectors;

		// Token: 0x04000DAB RID: 3499
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedTypeSelectors;

		// Token: 0x04000DAC RID: 3500
		[NonSerialized]
		internal Dictionary<string, StyleComplexSelector> orderedClassSelectors;

		// Token: 0x04000DAD RID: 3501
		[NonSerialized]
		private bool m_IsDefaultStyleSheet;

		// Token: 0x04000DAE RID: 3502
		private static string kCustomPropertyMarker = "--";

		// Token: 0x02000433 RID: 1075
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		[Serializable]
		internal struct ImportStruct
		{
			// Token: 0x04000DAF RID: 3503
			public StyleSheet styleSheet;

			// Token: 0x04000DB0 RID: 3504
			public string[] mediaQueries;
		}
	}
}
