using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020004EF RID: 1263
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class VisualTreeStyleUpdaterTraversal : HierarchyTraversal
	{
		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x00081D36 File Offset: 0x0007FF36
		// (set) Token: 0x06002350 RID: 9040 RVA: 0x00081D3E File Offset: 0x0007FF3E
		private float currentPixelsPerPoint { get; set; } = 1f;

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x00081D47 File Offset: 0x0007FF47
		// (set) Token: 0x06002352 RID: 9042 RVA: 0x00081D4F File Offset: 0x0007FF4F
		private BaseVisualElementPanel currentPanel { get; set; }

		// Token: 0x06002353 RID: 9043 RVA: 0x00081D58 File Offset: 0x0007FF58
		public void PrepareTraversal(BaseVisualElementPanel panel, float pixelsPerPoint)
		{
			this.currentPanel = panel;
			this.currentPixelsPerPoint = pixelsPerPoint;
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x00081D6C File Offset: 0x0007FF6C
		public void AddChangedElement(VisualElement ve, VersionChangeType versionChangeType)
		{
			this.m_UpdateList.Add(ve);
			bool flag = (versionChangeType & VersionChangeType.StyleSheet) == VersionChangeType.StyleSheet;
			if (flag)
			{
				this.PropagateToChildren(ve);
			}
			this.PropagateToParents(ve);
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x00081DA3 File Offset: 0x0007FFA3
		public void Clear()
		{
			this.m_UpdateList.Clear();
			this.m_ParentList.Clear();
			this.m_TempMatchResults.Clear();
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x00081DCC File Offset: 0x0007FFCC
		private void PropagateToChildren(VisualElement ve)
		{
			int count = ve.hierarchy.childCount;
			for (int i = 0; i < count; i++)
			{
				VisualElement child = ve.hierarchy[i];
				bool result = this.m_UpdateList.Add(child);
				bool flag = result;
				if (flag)
				{
					this.PropagateToChildren(child);
				}
			}
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x00081E2C File Offset: 0x0008002C
		private void PropagateToParents(VisualElement ve)
		{
			for (VisualElement parent = ve.hierarchy.parent; parent != null; parent = parent.hierarchy.parent)
			{
				bool flag = !this.m_ParentList.Add(parent);
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x00081E7A File Offset: 0x0008007A
		private static void OnProcessMatchResult(VisualElement current, MatchResultInfo info)
		{
			current.triggerPseudoMask |= info.triggerPseudoMask;
			current.dependencyPseudoMask |= info.dependencyPseudoMask;
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x00081EA4 File Offset: 0x000800A4
		public override void TraverseRecursive(VisualElement element, int depth)
		{
			bool flag = this.ShouldSkipElement(element);
			if (!flag)
			{
				bool updateElement = this.m_UpdateList.Contains(element);
				bool flag2 = updateElement;
				if (flag2)
				{
					element.triggerPseudoMask = (PseudoStates)0;
					element.dependencyPseudoMask = (PseudoStates)0;
				}
				int originalStyleSheetCount = this.m_StyleMatchingContext.styleSheetCount;
				bool flag3 = element.styleSheetList != null;
				if (flag3)
				{
					for (int i = 0; i < element.styleSheetList.Count; i++)
					{
						StyleSheet addedStyleSheet = element.styleSheetList[i];
						bool flag4 = addedStyleSheet.flattenedRecursiveImports != null;
						if (flag4)
						{
							for (int j = 0; j < addedStyleSheet.flattenedRecursiveImports.Count; j++)
							{
								this.m_StyleMatchingContext.AddStyleSheet(addedStyleSheet.flattenedRecursiveImports[j]);
							}
						}
						this.m_StyleMatchingContext.AddStyleSheet(addedStyleSheet);
					}
				}
				StyleVariableContext originalVariableContext = this.m_StyleMatchingContext.variableContext;
				int originalCustomStyleCount = element.computedStyle.customPropertiesCount;
				bool flag5 = updateElement;
				if (flag5)
				{
					this.m_StyleMatchingContext.currentElement = element;
					StyleSelectorHelper.FindMatches(this.m_StyleMatchingContext, this.m_TempMatchResults, originalStyleSheetCount - 1);
					ComputedStyle newStyle = this.ProcessMatchedRules(element, this.m_TempMatchResults);
					newStyle.Acquire();
					bool hasInlineStyle = element.hasInlineStyle;
					if (hasInlineStyle)
					{
						element.inlineStyleAccess.ApplyInlineStyles(ref newStyle);
					}
					ComputedTransitionUtils.UpdateComputedTransitions(ref newStyle);
					bool flag6 = element.hasRunningAnimations && !ComputedTransitionUtils.SameTransitionProperty(element.computedStyle, ref newStyle);
					if (flag6)
					{
						this.CancelAnimationsWithNoTransitionProperty(element, ref newStyle);
					}
					bool flag7 = newStyle.hasTransition && element.styleInitialized;
					if (flag7)
					{
						this.ProcessTransitions(element, element.computedStyle, ref newStyle);
						element.SetComputedStyle(ref newStyle);
						this.ForceUpdateTransitions(element);
					}
					else
					{
						element.SetComputedStyle(ref newStyle);
					}
					newStyle.Release();
					element.styleInitialized = true;
					element.inheritedStylesHash = element.computedStyle.inheritedData.GetHashCode();
					this.m_StyleMatchingContext.currentElement = null;
					this.m_TempMatchResults.Clear();
				}
				else
				{
					this.m_StyleMatchingContext.variableContext = element.variableContext;
				}
				bool flag8 = updateElement && (originalCustomStyleCount > 0 || element.computedStyle.customPropertiesCount > 0) && element.HasSelfEventInterests(EventBase<CustomStyleResolvedEvent>.EventCategory);
				if (flag8)
				{
					using (CustomStyleResolvedEvent evt = EventBase<CustomStyleResolvedEvent>.GetPooled())
					{
						evt.elementTarget = element;
						EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(evt, this.currentPanel, element);
					}
				}
				this.m_StyleMatchingContext.ancestorFilter.PushElement(element);
				base.Recurse(element, depth);
				this.m_StyleMatchingContext.ancestorFilter.PopElement();
				this.m_StyleMatchingContext.variableContext = originalVariableContext;
				bool flag9 = this.m_StyleMatchingContext.styleSheetCount > originalStyleSheetCount;
				if (flag9)
				{
					this.m_StyleMatchingContext.RemoveStyleSheetRange(originalStyleSheetCount, this.m_StyleMatchingContext.styleSheetCount - originalStyleSheetCount);
				}
			}
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000821BC File Offset: 0x000803BC
		private void ProcessTransitions(VisualElement element, ref ComputedStyle oldStyle, ref ComputedStyle newStyle)
		{
			for (int i = newStyle.computedTransitions.Length - 1; i >= 0; i--)
			{
				ComputedTransitionProperty t = newStyle.computedTransitions[i];
				bool flag = element.hasInlineStyle && element.inlineStyleAccess.IsValueSet(t.id);
				if (!flag)
				{
					ComputedStyle.StartAnimation(element, t.id, ref oldStyle, ref newStyle, t.durationMs, t.delayMs, t.easingCurve);
				}
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x00082238 File Offset: 0x00080438
		private void ForceUpdateTransitions(VisualElement element)
		{
			element.styleAnimation.GetAllAnimations(this.m_AnimatedProperties);
			bool flag = this.m_AnimatedProperties.Count > 0;
			if (flag)
			{
				foreach (StylePropertyId id in this.m_AnimatedProperties)
				{
					element.styleAnimation.UpdateAnimation(id);
				}
				this.m_AnimatedProperties.Clear();
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x000822C8 File Offset: 0x000804C8
		internal void CancelAnimationsWithNoTransitionProperty(VisualElement element, ref ComputedStyle newStyle)
		{
			element.styleAnimation.GetAllAnimations(this.m_AnimatedProperties);
			foreach (StylePropertyId id in this.m_AnimatedProperties)
			{
				bool flag = !(ref newStyle).HasTransitionProperty(id);
				if (flag)
				{
					element.styleAnimation.CancelAnimation(id);
				}
			}
			this.m_AnimatedProperties.Clear();
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x00082354 File Offset: 0x00080554
		protected bool ShouldSkipElement(VisualElement element)
		{
			return !this.m_ParentList.Contains(element) && !this.m_UpdateList.Contains(element);
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x00082388 File Offset: 0x00080588
		private ComputedStyle ProcessMatchedRules(VisualElement element, List<SelectorMatchRecord> matchingSelectors)
		{
			matchingSelectors.Sort((SelectorMatchRecord a, SelectorMatchRecord b) => SelectorMatchRecord.Compare(a, b));
			long matchingRulesHash = (long)element.fullTypeName.GetHashCode();
			matchingRulesHash = (matchingRulesHash * 397L) ^ (long)this.currentPixelsPerPoint.GetHashCode();
			int oldVariablesHash = this.m_StyleMatchingContext.variableContext.GetVariableHash();
			int customPropertiesCount = 0;
			foreach (SelectorMatchRecord record in matchingSelectors)
			{
				customPropertiesCount += record.complexSelector.rule.customPropertiesCount;
			}
			bool flag = customPropertiesCount > 0;
			if (flag)
			{
				this.m_ProcessVarContext.AddInitialRange(this.m_StyleMatchingContext.variableContext);
			}
			foreach (SelectorMatchRecord record2 in matchingSelectors)
			{
				StyleSheet sheet = record2.sheet;
				StyleRule rule = record2.complexSelector.rule;
				int specificity = record2.complexSelector.specificity;
				matchingRulesHash = (matchingRulesHash * 397L) ^ (long)sheet.contentHash;
				matchingRulesHash = (matchingRulesHash * 397L) ^ (long)rule.GetHashCode();
				matchingRulesHash = (matchingRulesHash * 397L) ^ (long)specificity;
				bool flag2 = rule.customPropertiesCount > 0;
				if (flag2)
				{
					this.ProcessMatchedVariables(record2.sheet, rule);
				}
			}
			VisualElement parent = element.hierarchy.parent;
			int inheritedStyleHash = ((parent != null) ? parent.inheritedStylesHash : 0);
			matchingRulesHash = (matchingRulesHash * 397L) ^ (long)inheritedStyleHash;
			int variablesHash = oldVariablesHash;
			bool flag3 = customPropertiesCount > 0;
			if (flag3)
			{
				variablesHash = this.m_ProcessVarContext.GetVariableHash();
			}
			matchingRulesHash = (matchingRulesHash * 397L) ^ (long)variablesHash;
			bool flag4 = oldVariablesHash != variablesHash;
			if (flag4)
			{
				StyleVariableContext ctx;
				bool flag5 = !StyleCache.TryGetValue(variablesHash, out ctx);
				if (flag5)
				{
					ctx = new StyleVariableContext(this.m_ProcessVarContext);
					StyleCache.SetValue(variablesHash, ctx);
				}
				this.m_StyleMatchingContext.variableContext = ctx;
			}
			element.variableContext = this.m_StyleMatchingContext.variableContext;
			this.m_ProcessVarContext.Clear();
			ComputedStyle resolvedStyles;
			bool flag6 = !StyleCache.TryGetValue(matchingRulesHash, out resolvedStyles);
			if (flag6)
			{
				ref ComputedStyle ptr;
				if (parent != null)
				{
					ref ComputedStyle computedStyle = ref parent.computedStyle;
					ptr = parent.computedStyle;
				}
				else
				{
					ptr = InitialStyle.Get();
				}
				ref ComputedStyle parentStyle = ref ptr;
				resolvedStyles = ComputedStyle.Create(ref parentStyle);
				resolvedStyles.matchingRulesHash = matchingRulesHash;
				float dpiScaling = element.scaledPixelsPerPoint;
				foreach (SelectorMatchRecord record3 in matchingSelectors)
				{
					this.m_StylePropertyReader.SetContext(record3.sheet, record3.complexSelector, this.m_StyleMatchingContext.variableContext, dpiScaling);
					resolvedStyles.ApplyProperties(this.m_StylePropertyReader, ref parentStyle);
				}
				resolvedStyles.FinalizeApply(ref parentStyle);
				StyleCache.SetValue(matchingRulesHash, ref resolvedStyles);
			}
			return resolvedStyles;
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x000826B8 File Offset: 0x000808B8
		private void ProcessMatchedVariables(StyleSheet sheet, StyleRule rule)
		{
			foreach (StyleProperty property in rule.properties)
			{
				bool isCustomProperty = property.isCustomProperty;
				if (isCustomProperty)
				{
					StyleVariable sv = new StyleVariable(property.name, sheet, property.values);
					this.m_ProcessVarContext.Add(sv);
				}
			}
		}

		// Token: 0x04001014 RID: 4116
		private StyleVariableContext m_ProcessVarContext = new StyleVariableContext();

		// Token: 0x04001015 RID: 4117
		private HashSet<VisualElement> m_UpdateList = new HashSet<VisualElement>();

		// Token: 0x04001016 RID: 4118
		private HashSet<VisualElement> m_ParentList = new HashSet<VisualElement>();

		// Token: 0x04001017 RID: 4119
		private List<SelectorMatchRecord> m_TempMatchResults = new List<SelectorMatchRecord>();

		// Token: 0x04001019 RID: 4121
		private StyleMatchingContext m_StyleMatchingContext = new StyleMatchingContext(new Action<VisualElement, MatchResultInfo>(VisualTreeStyleUpdaterTraversal.OnProcessMatchResult));

		// Token: 0x0400101A RID: 4122
		private StylePropertyReader m_StylePropertyReader = new StylePropertyReader();

		// Token: 0x0400101C RID: 4124
		private readonly List<StylePropertyId> m_AnimatedProperties = new List<StylePropertyId>();
	}
}
