using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005B7 RID: 1463
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal static class StyleSheetCache
	{
		// Token: 0x060027C9 RID: 10185 RVA: 0x000A3840 File Offset: 0x000A1A40
		internal static StylePropertyId[] GetPropertyIds(StyleSheet sheet, int ruleIndex)
		{
			StyleSheetCache.SheetHandleKey key = new StyleSheetCache.SheetHandleKey(sheet, ruleIndex);
			StylePropertyId[] propertyIds;
			bool flag = !StyleSheetCache.s_RulePropertyIdsCache.TryGetValue(key, out propertyIds);
			if (flag)
			{
				StyleRule rule = sheet.rules[ruleIndex];
				propertyIds = new StylePropertyId[rule.properties.Length];
				for (int i = 0; i < propertyIds.Length; i++)
				{
					propertyIds[i] = StyleSheetCache.GetPropertyId(rule, i);
				}
				StyleSheetCache.s_RulePropertyIdsCache.Add(key, propertyIds);
			}
			return propertyIds;
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x000A38C0 File Offset: 0x000A1AC0
		internal static StylePropertyId[] GetPropertyIds(StyleRule rule)
		{
			StylePropertyId[] propertyIds = new StylePropertyId[rule.properties.Length];
			for (int i = 0; i < propertyIds.Length; i++)
			{
				propertyIds[i] = StyleSheetCache.GetPropertyId(rule, i);
			}
			return propertyIds;
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x000A3900 File Offset: 0x000A1B00
		private static StylePropertyId GetPropertyId(StyleRule rule, int index)
		{
			StyleProperty property = rule.properties[index];
			string name = property.name;
			StylePropertyId id;
			bool flag = !StylePropertyUtil.s_NameToId.TryGetValue(name, out id);
			if (flag)
			{
				id = (property.isCustomProperty ? StylePropertyId.Custom : StylePropertyId.Unknown);
			}
			return id;
		}

		// Token: 0x04001503 RID: 5379
		private static StyleSheetCache.SheetHandleKeyComparer s_Comparer = new StyleSheetCache.SheetHandleKeyComparer();

		// Token: 0x04001504 RID: 5380
		private static Dictionary<StyleSheetCache.SheetHandleKey, StylePropertyId[]> s_RulePropertyIdsCache = new Dictionary<StyleSheetCache.SheetHandleKey, StylePropertyId[]>(StyleSheetCache.s_Comparer);

		// Token: 0x020005B8 RID: 1464
		private struct SheetHandleKey
		{
			// Token: 0x060027CD RID: 10189 RVA: 0x000A3963 File Offset: 0x000A1B63
			public SheetHandleKey(StyleSheet sheet, int index)
			{
				this.sheetInstanceID = sheet.GetInstanceID();
				this.index = index;
			}

			// Token: 0x04001505 RID: 5381
			public readonly int sheetInstanceID;

			// Token: 0x04001506 RID: 5382
			public readonly int index;
		}

		// Token: 0x020005B9 RID: 1465
		private class SheetHandleKeyComparer : IEqualityComparer<StyleSheetCache.SheetHandleKey>
		{
			// Token: 0x060027CE RID: 10190 RVA: 0x000A397C File Offset: 0x000A1B7C
			public bool Equals(StyleSheetCache.SheetHandleKey x, StyleSheetCache.SheetHandleKey y)
			{
				return x.sheetInstanceID == y.sheetInstanceID && x.index == y.index;
			}

			// Token: 0x060027CF RID: 10191 RVA: 0x000A39B0 File Offset: 0x000A1BB0
			public int GetHashCode(StyleSheetCache.SheetHandleKey key)
			{
				return key.sheetInstanceID.GetHashCode() ^ key.index.GetHashCode();
			}
		}
	}
}
