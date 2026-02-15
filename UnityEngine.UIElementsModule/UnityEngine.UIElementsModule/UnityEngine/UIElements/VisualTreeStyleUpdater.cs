using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004ED RID: 1261
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class VisualTreeStyleUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x000819DA File Offset: 0x0007FBDA
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeStyleUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x06002342 RID: 9026 RVA: 0x000819E4 File Offset: 0x0007FBE4
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & (VersionChangeType.StyleSheet | VersionChangeType.TransitionProperty)) == (VersionChangeType)0;
			if (!flag)
			{
				this.m_Version += 1U;
				bool flag2 = (versionChangeType & VersionChangeType.StyleSheet) > (VersionChangeType)0;
				if (flag2)
				{
					bool isApplyingStyles = this.m_IsApplyingStyles;
					if (isApplyingStyles)
					{
						this.m_ApplyStyleUpdateList.Add(ve);
					}
					else
					{
						this.m_StyleContextHierarchyTraversal.AddChangedElement(ve, versionChangeType);
					}
				}
				bool flag3 = (versionChangeType & VersionChangeType.TransitionProperty) > (VersionChangeType)0;
				if (flag3)
				{
					this.m_TransitionPropertyUpdateList.Add(ve);
				}
			}
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x00081A68 File Offset: 0x0007FC68
		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				this.m_LastVersion = this.m_Version;
				this.ApplyStyles();
				this.m_StyleContextHierarchyTraversal.Clear();
				foreach (VisualElement ve in this.m_ApplyStyleUpdateList)
				{
					this.m_StyleContextHierarchyTraversal.AddChangedElement(ve, VersionChangeType.StyleSheet);
				}
				this.m_ApplyStyleUpdateList.Clear();
				foreach (VisualElement ve2 in this.m_TransitionPropertyUpdateList)
				{
					bool flag2 = ve2.hasRunningAnimations || ve2.hasCompletedAnimations;
					if (flag2)
					{
						ComputedTransitionUtils.UpdateComputedTransitions(ve2.computedStyle);
						this.m_StyleContextHierarchyTraversal.CancelAnimationsWithNoTransitionProperty(ve2, ve2.computedStyle);
					}
				}
				this.m_TransitionPropertyUpdateList.Clear();
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x00081B94 File Offset: 0x0007FD94
		// (set) Token: 0x06002345 RID: 9029 RVA: 0x00081B9C File Offset: 0x0007FD9C
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002346 RID: 9030 RVA: 0x00081BA8 File Offset: 0x0007FDA8
		protected override void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_StyleContextHierarchyTraversal.Clear();
				}
				this.disposed = true;
			}
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x00081BDC File Offset: 0x0007FDDC
		private void ApplyStyles()
		{
			Debug.Assert(base.visualTree.panel != null);
			this.m_IsApplyingStyles = true;
			this.m_StyleContextHierarchyTraversal.PrepareTraversal(base.panel, base.panel.scaledPixelsPerPoint);
			this.m_StyleContextHierarchyTraversal.Traverse(base.visualTree);
			this.m_IsApplyingStyles = false;
		}

		// Token: 0x04001006 RID: 4102
		private HashSet<VisualElement> m_ApplyStyleUpdateList = new HashSet<VisualElement>();

		// Token: 0x04001007 RID: 4103
		private HashSet<VisualElement> m_TransitionPropertyUpdateList = new HashSet<VisualElement>();

		// Token: 0x04001008 RID: 4104
		private bool m_IsApplyingStyles = false;

		// Token: 0x04001009 RID: 4105
		private uint m_Version = 0U;

		// Token: 0x0400100A RID: 4106
		private uint m_LastVersion = 0U;

		// Token: 0x0400100B RID: 4107
		private VisualTreeStyleUpdaterTraversal m_StyleContextHierarchyTraversal = new VisualTreeStyleUpdaterTraversal();

		// Token: 0x0400100C RID: 4108
		private static readonly string s_Description = "Update Style";

		// Token: 0x0400100D RID: 4109
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeStyleUpdater.s_Description);
	}
}
