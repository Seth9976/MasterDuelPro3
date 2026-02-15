using System;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005B5 RID: 1461
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal struct SelectorMatchRecord
	{
		// Token: 0x060027C3 RID: 10179 RVA: 0x000A3276 File Offset: 0x000A1476
		public SelectorMatchRecord(StyleSheet sheet, int styleSheetIndexInStack)
		{
			this = default(SelectorMatchRecord);
			this.sheet = sheet;
			this.styleSheetIndexInStack = styleSheetIndexInStack;
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x000A3290 File Offset: 0x000A1490
		public static int Compare(SelectorMatchRecord a, SelectorMatchRecord b)
		{
			bool flag = a.sheet.isDefaultStyleSheet != b.sheet.isDefaultStyleSheet;
			int num;
			if (flag)
			{
				num = (a.sheet.isDefaultStyleSheet ? (-1) : 1);
			}
			else
			{
				int res = a.complexSelector.specificity.CompareTo(b.complexSelector.specificity);
				bool flag2 = res == 0;
				if (flag2)
				{
					res = a.styleSheetIndexInStack.CompareTo(b.styleSheetIndexInStack);
				}
				bool flag3 = res == 0;
				if (flag3)
				{
					res = a.complexSelector.orderInStyleSheet.CompareTo(b.complexSelector.orderInStyleSheet);
				}
				num = res;
			}
			return num;
		}

		// Token: 0x04001500 RID: 5376
		public StyleSheet sheet;

		// Token: 0x04001501 RID: 5377
		public int styleSheetIndexInStack;

		// Token: 0x04001502 RID: 5378
		public StyleComplexSelector complexSelector;
	}
}
