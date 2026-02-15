using System;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005B6 RID: 1462
	internal static class StyleSelectorHelper
	{
		// Token: 0x060027C5 RID: 10181 RVA: 0x000A333C File Offset: 0x000A153C
		public static MatchResultInfo MatchesSelector(VisualElement element, StyleSelector selector)
		{
			bool match = true;
			StyleSelectorPart[] parts = selector.parts;
			int count = parts.Length;
			int i = 0;
			while (i < count && match)
			{
				switch (parts[i].type)
				{
				case StyleSelectorType.Wildcard:
					break;
				case StyleSelectorType.Type:
					match = string.Equals(element.typeName, parts[i].value, StringComparison.Ordinal);
					break;
				case StyleSelectorType.Class:
					match = element.ClassListContains(parts[i].value);
					break;
				case StyleSelectorType.PseudoClass:
					break;
				case StyleSelectorType.RecursivePseudoClass:
					goto IL_00C9;
				case StyleSelectorType.ID:
					match = string.Equals(element.name, parts[i].value, StringComparison.Ordinal);
					break;
				case StyleSelectorType.Predicate:
				{
					UQuery.IVisualPredicateWrapper w = parts[i].tempData as UQuery.IVisualPredicateWrapper;
					match = w != null && w.Predicate(element);
					break;
				}
				default:
					goto IL_00C9;
				}
				IL_00CD:
				i++;
				continue;
				IL_00C9:
				match = false;
				goto IL_00CD;
			}
			int triggerPseudoStateMask = 0;
			int dependencyPseudoMask = 0;
			bool saveMatch = match;
			bool flag = saveMatch && selector.pseudoStateMask != 0;
			if (flag)
			{
				match = (selector.pseudoStateMask & (int)element.pseudoStates) == selector.pseudoStateMask;
				bool flag2 = match;
				if (flag2)
				{
					dependencyPseudoMask = selector.pseudoStateMask;
				}
				else
				{
					triggerPseudoStateMask = selector.pseudoStateMask;
				}
			}
			bool flag3 = saveMatch && selector.negatedPseudoStateMask != 0;
			if (flag3)
			{
				match &= (selector.negatedPseudoStateMask & (int)(~(int)element.pseudoStates)) == selector.negatedPseudoStateMask;
				bool flag4 = match;
				if (flag4)
				{
					triggerPseudoStateMask |= selector.negatedPseudoStateMask;
				}
				else
				{
					dependencyPseudoMask |= selector.negatedPseudoStateMask;
				}
			}
			return new MatchResultInfo(match, (PseudoStates)triggerPseudoStateMask, (PseudoStates)dependencyPseudoMask);
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x000A34E0 File Offset: 0x000A16E0
		public static bool MatchRightToLeft(VisualElement element, StyleComplexSelector complexSelector, Action<VisualElement, MatchResultInfo> processResult)
		{
			VisualElement current = element;
			int nextIndex = complexSelector.selectors.Length - 1;
			VisualElement saved = null;
			int savedIdx = -1;
			while (nextIndex >= 0)
			{
				bool flag = current == null;
				if (flag)
				{
					break;
				}
				MatchResultInfo matchInfo = StyleSelectorHelper.MatchesSelector(current, complexSelector.selectors[nextIndex]);
				processResult(current, matchInfo);
				bool flag2 = !matchInfo.success;
				if (flag2)
				{
					bool flag3 = nextIndex < complexSelector.selectors.Length - 1 && complexSelector.selectors[nextIndex + 1].previousRelationship == StyleSelectorRelationship.Descendent;
					if (flag3)
					{
						current = current.parent;
					}
					else
					{
						bool flag4 = saved != null;
						if (!flag4)
						{
							break;
						}
						current = saved;
						nextIndex = savedIdx;
					}
				}
				else
				{
					bool flag5 = nextIndex < complexSelector.selectors.Length - 1 && complexSelector.selectors[nextIndex + 1].previousRelationship == StyleSelectorRelationship.Descendent;
					if (flag5)
					{
						saved = current.parent;
						savedIdx = nextIndex;
					}
					bool flag6 = --nextIndex < 0;
					if (flag6)
					{
						return true;
					}
					current = current.parent;
				}
			}
			return false;
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x000A35EC File Offset: 0x000A17EC
		private static void FastLookup(IDictionary<string, StyleComplexSelector> table, List<SelectorMatchRecord> matchedSelectors, StyleMatchingContext context, string input, ref SelectorMatchRecord record)
		{
			StyleComplexSelector currentComplexSelector;
			bool flag = table.TryGetValue(input, out currentComplexSelector);
			if (flag)
			{
				while (currentComplexSelector != null)
				{
					bool isCandidate = true;
					bool isMatchRightToLeft = false;
					bool flag2 = !currentComplexSelector.isSimple;
					if (flag2)
					{
						isCandidate = context.ancestorFilter.IsCandidate(currentComplexSelector);
					}
					bool flag3 = isCandidate;
					if (flag3)
					{
						isMatchRightToLeft = StyleSelectorHelper.MatchRightToLeft(context.currentElement, currentComplexSelector, context.processResult);
					}
					bool flag4 = isMatchRightToLeft;
					if (flag4)
					{
						record.complexSelector = currentComplexSelector;
						matchedSelectors.Add(record);
					}
					currentComplexSelector = currentComplexSelector.nextInTable;
				}
			}
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x000A367C File Offset: 0x000A187C
		public static void FindMatches(StyleMatchingContext context, List<SelectorMatchRecord> matchedSelectors, int parentSheetIndex)
		{
			Debug.Assert(matchedSelectors.Count == 0);
			Debug.Assert(context.currentElement != null, "context.currentElement != null");
			bool toggleRoot = false;
			HashSet<StyleSheet> processedStyleSheets = CollectionPool<HashSet<StyleSheet>, StyleSheet>.Get();
			try
			{
				VisualElement element = context.currentElement;
				for (int i = context.styleSheetCount - 1; i >= 0; i--)
				{
					StyleSheet styleSheet = context.GetStyleSheetAt(i);
					bool flag = !processedStyleSheets.Add(styleSheet);
					if (!flag)
					{
						bool flag2 = i > parentSheetIndex;
						if (flag2)
						{
							element.pseudoStates |= PseudoStates.Root;
							toggleRoot = true;
						}
						else
						{
							element.pseudoStates &= ~PseudoStates.Root;
						}
						SelectorMatchRecord record = new SelectorMatchRecord(styleSheet, i);
						StyleSelectorHelper.FastLookup(styleSheet.orderedTypeSelectors, matchedSelectors, context, element.typeName, ref record);
						StyleSelectorHelper.FastLookup(styleSheet.orderedTypeSelectors, matchedSelectors, context, "*", ref record);
						bool flag3 = !string.IsNullOrEmpty(element.name);
						if (flag3)
						{
							StyleSelectorHelper.FastLookup(styleSheet.orderedNameSelectors, matchedSelectors, context, element.name, ref record);
						}
						foreach (string @class in element.GetClassesForIteration())
						{
							StyleSelectorHelper.FastLookup(styleSheet.orderedClassSelectors, matchedSelectors, context, @class, ref record);
						}
					}
				}
				bool flag4 = toggleRoot;
				if (flag4)
				{
					element.pseudoStates &= ~PseudoStates.Root;
				}
			}
			finally
			{
				CollectionPool<HashSet<StyleSheet>, StyleSheet>.Release(processedStyleSheets);
			}
		}
	}
}
