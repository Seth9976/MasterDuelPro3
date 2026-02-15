using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020004EE RID: 1262
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class StyleMatchingContext
	{
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x00081C95 File Offset: 0x0007FE95
		public int styleSheetCount
		{
			get
			{
				return this.m_StyleSheetStack.Count;
			}
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x00081CA2 File Offset: 0x0007FEA2
		public StyleMatchingContext(Action<VisualElement, MatchResultInfo> processResult)
		{
			this.m_StyleSheetStack = new List<StyleSheet>();
			this.variableContext = StyleVariableContext.none;
			this.currentElement = null;
			this.processResult = processResult;
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x00081CDC File Offset: 0x0007FEDC
		public void AddStyleSheet(StyleSheet sheet)
		{
			bool flag = sheet == null;
			if (!flag)
			{
				this.m_StyleSheetStack.Add(sheet);
			}
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x00081D04 File Offset: 0x0007FF04
		public void RemoveStyleSheetRange(int index, int count)
		{
			this.m_StyleSheetStack.RemoveRange(index, count);
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x00081D18 File Offset: 0x0007FF18
		public StyleSheet GetStyleSheetAt(int index)
		{
			return this.m_StyleSheetStack[index];
		}

		// Token: 0x0400100F RID: 4111
		private List<StyleSheet> m_StyleSheetStack;

		// Token: 0x04001010 RID: 4112
		public StyleVariableContext variableContext;

		// Token: 0x04001011 RID: 4113
		public VisualElement currentElement;

		// Token: 0x04001012 RID: 4114
		public Action<VisualElement, MatchResultInfo> processResult;

		// Token: 0x04001013 RID: 4115
		public AncestorFilter ancestorFilter = new AncestorFilter();
	}
}
