using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.UIElements.Layout;

namespace UnityEngine.UIElements
{
	// Token: 0x0200029E RID: 670
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class UIRLayoutUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001226 RID: 4646 RVA: 0x0004BB4E File Offset: 0x00049D4E
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return UIRLayoutUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0004BB58 File Offset: 0x00049D58
		public unsafe override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.Layout)) == (VersionChangeType)0;
			if (!flag)
			{
				LayoutNode layoutNode = *ve.layoutNode;
				bool isMeasureDefined = layoutNode.IsMeasureDefined;
				if (isMeasureDefined)
				{
					layoutNode.MarkDirty();
				}
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0004BB94 File Offset: 0x00049D94
		public override void Update()
		{
			int validateLayoutCount = 0;
			bool isDirty = base.visualTree.layoutNode.IsDirty;
			if (isDirty)
			{
				while (base.visualTree.layoutNode.IsDirty)
				{
					this.changeEventsList.Clear();
					bool flag = validateLayoutCount > 0;
					if (flag)
					{
						base.panel.ApplyStyles();
					}
					base.panel.duringLayoutPhase = true;
					base.visualTree.layoutNode.CalculateLayout(float.NaN, float.NaN);
					base.panel.duringLayoutPhase = false;
					this.UpdateSubTree(base.visualTree, this.changeEventsList);
					this.DispatchChangeEvents(this.changeEventsList, validateLayoutCount);
					bool flag2 = validateLayoutCount++ >= 10;
					if (flag2)
					{
						string text = "Layout update is struggling to process current layout (consider simplifying to avoid recursive layout): ";
						VisualElement visualTree = base.visualTree;
						Debug.LogError(text + ((visualTree != null) ? visualTree.ToString() : null));
						break;
					}
				}
			}
			base.visualTree.focusController.ReevaluateFocus();
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0004BC9C File Offset: 0x00049E9C
		private static bool UpdateHierarchyDisplayed(VisualElement ve, List<ValueTuple<Rect, Rect, VisualElement>> changeEvents, bool inheritedDisplayed = true)
		{
			bool isDisplayed = inheritedDisplayed & (ve.resolvedStyle.display != DisplayStyle.None);
			bool flag = inheritedDisplayed && !isDisplayed;
			if (flag)
			{
				ve.disableRendering = true;
			}
			else
			{
				bool flag2 = isDisplayed;
				if (flag2)
				{
					ve.disableRendering = false;
				}
			}
			bool flag3 = ve.areAncestorsAndSelfDisplayed == isDisplayed;
			bool flag4;
			if (flag3)
			{
				flag4 = false;
			}
			else
			{
				ve.areAncestorsAndSelfDisplayed = isDisplayed;
				bool flag5 = !isDisplayed;
				if (flag5)
				{
					if (inheritedDisplayed)
					{
						ve.IncrementVersion(VersionChangeType.Size);
					}
					bool flag6 = ve.HasSelfEventInterests(EventBase<GeometryChangedEvent>.EventCategory);
					if (flag6)
					{
						changeEvents.Add(new ValueTuple<Rect, Rect, VisualElement>(ve.lastLayout, Rect.zero, ve));
					}
					int childCount = ve.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						UIRLayoutUpdater.UpdateHierarchyDisplayed(ve.hierarchy[i], changeEvents, isDisplayed);
					}
				}
				flag4 = true;
			}
			return flag4;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x0004BD98 File Offset: 0x00049F98
		private void UpdateSubTree(VisualElement ve, List<ValueTuple<Rect, Rect, VisualElement>> changeEvents)
		{
			bool isDisplayedJustChanged = UIRLayoutUpdater.UpdateHierarchyDisplayed(ve, changeEvents, true);
			bool flag = !ve.areAncestorsAndSelfDisplayed;
			if (!flag)
			{
				Rect layoutRect = new Rect(ve.layoutNode.LayoutX, ve.layoutNode.LayoutY, ve.layoutNode.LayoutWidth, ve.layoutNode.LayoutHeight);
				Rect rawPadding = new Rect(ve.layoutNode.LayoutPaddingLeft, ve.layoutNode.LayoutPaddingLeft, ve.layoutNode.LayoutPaddingRight, ve.layoutNode.LayoutPaddingBottom);
				Rect layoutPseudoPaddingRect = new Rect(rawPadding.x, rawPadding.y, layoutRect.width - (rawPadding.x + rawPadding.width), layoutRect.height - (rawPadding.y + rawPadding.height));
				Rect lastLayoutRect = ve.lastLayout;
				Rect lastPseudoPaddingRect = ve.lastPseudoPadding;
				VersionChangeType changeType = (VersionChangeType)0;
				bool layoutSizeChanged = lastLayoutRect.size != layoutRect.size;
				bool pseudoPaddingSizeChanged = lastPseudoPaddingRect.size != layoutPseudoPaddingRect.size;
				bool flag2 = layoutSizeChanged || pseudoPaddingSizeChanged;
				if (flag2)
				{
					changeType |= VersionChangeType.Size | VersionChangeType.Repaint;
				}
				bool layoutPositionChanged = layoutRect.position != lastLayoutRect.position;
				bool paddingPositionChanged = layoutPseudoPaddingRect.position != lastPseudoPaddingRect.position;
				bool flag3 = layoutPositionChanged || paddingPositionChanged || isDisplayedJustChanged;
				if (flag3)
				{
					changeType |= VersionChangeType.Transform;
				}
				bool flag4 = isDisplayedJustChanged;
				if (flag4)
				{
					changeType |= VersionChangeType.Size;
				}
				bool flag5 = (changeType & (VersionChangeType.Transform | VersionChangeType.Size)) == VersionChangeType.Size;
				if (flag5)
				{
					bool flag6 = !ve.hasDefaultRotationAndScale;
					if (flag6)
					{
						bool flag7 = !Mathf.Approximately(ve.resolvedStyle.transformOrigin.x, 0f) || !Mathf.Approximately(ve.resolvedStyle.transformOrigin.y, 0f);
						if (flag7)
						{
							changeType |= VersionChangeType.Transform;
						}
					}
				}
				bool flag8 = changeType > (VersionChangeType)0;
				if (flag8)
				{
					ve.IncrementVersion(changeType);
				}
				ve.lastLayout = layoutRect;
				ve.lastPseudoPadding = layoutPseudoPaddingRect;
				bool hasNewLayout = ve.layoutNode.HasNewLayout;
				bool flag9 = hasNewLayout;
				if (flag9)
				{
					int childCount = ve.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						VisualElement child = ve.hierarchy[i];
						bool hasNewLayout2 = child.layoutNode.HasNewLayout;
						if (hasNewLayout2)
						{
							this.UpdateSubTree(child, changeEvents);
						}
					}
				}
				bool flag10 = (layoutSizeChanged || layoutPositionChanged || isDisplayedJustChanged) && ve.HasSelfEventInterests(EventBase<GeometryChangedEvent>.EventCategory);
				if (flag10)
				{
					changeEvents.Add(new ValueTuple<Rect, Rect, VisualElement>(isDisplayedJustChanged ? Rect.zero : lastLayoutRect, layoutRect, ve));
				}
				bool flag11 = hasNewLayout;
				if (flag11)
				{
					ve.layoutNode.MarkLayoutSeen();
				}
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0004C068 File Offset: 0x0004A268
		private void DispatchChangeEvents(List<ValueTuple<Rect, Rect, VisualElement>> changeEvents, int currentLayoutPass)
		{
			foreach (ValueTuple<Rect, Rect, VisualElement> valueTuple in changeEvents)
			{
				Rect oldRect = valueTuple.Item1;
				Rect newRect = valueTuple.Item2;
				VisualElement ve = valueTuple.Item3;
				using (GeometryChangedEvent evt = GeometryChangedEvent.GetPooled(oldRect, newRect))
				{
					evt.layoutPass = currentLayoutPass;
					evt.elementTarget = ve;
					EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(evt, base.panel, ve);
				}
			}
		}

		// Token: 0x04000A75 RID: 2677
		private static readonly string s_Description = "Update Layout";

		// Token: 0x04000A76 RID: 2678
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(UIRLayoutUpdater.s_Description);

		// Token: 0x04000A77 RID: 2679
		private static readonly ProfilerMarker k_ComputeLayoutMarker = new ProfilerMarker("LayoutUpdater.ComputeLayout");

		// Token: 0x04000A78 RID: 2680
		private static readonly ProfilerMarker k_UpdateSubTreeMarker = new ProfilerMarker("LayoutUpdater.UpdateSubTree");

		// Token: 0x04000A79 RID: 2681
		private static readonly ProfilerMarker k_DispatchChangeEventsMarker = new ProfilerMarker("LayoutUpdater.DispatchChangeEvents");

		// Token: 0x04000A7A RID: 2682
		private List<ValueTuple<Rect, Rect, VisualElement>> changeEventsList = new List<ValueTuple<Rect, Rect, VisualElement>>();
	}
}
