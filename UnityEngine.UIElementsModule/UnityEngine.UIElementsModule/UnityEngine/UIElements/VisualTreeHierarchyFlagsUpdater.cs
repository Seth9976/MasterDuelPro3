using System;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020004E8 RID: 1256
	internal class VisualTreeHierarchyFlagsUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x00081565 File Offset: 0x0007F765
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeHierarchyFlagsUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x0008156C File Offset: 0x0007F76C
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size | VersionChangeType.EventCallbackCategories | VersionChangeType.Picking)) == (VersionChangeType)0;
			if (!flag)
			{
				bool mustDirtyWorldTransform = (versionChangeType & VersionChangeType.Transform) > (VersionChangeType)0;
				bool mustDirtyWorldClip = (versionChangeType & (VersionChangeType.Overflow | VersionChangeType.BorderWidth | VersionChangeType.Transform | VersionChangeType.Size)) > (VersionChangeType)0;
				bool mustDirtyEventParentCategories = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.EventCallbackCategories)) > (VersionChangeType)0;
				VisualElementFlags mustDirtyFlags = (mustDirtyWorldTransform ? (VisualElementFlags.WorldTransformDirty | VisualElementFlags.WorldBoundingBoxDirty) : ((VisualElementFlags)0)) | (mustDirtyWorldClip ? VisualElementFlags.WorldClipDirty : ((VisualElementFlags)0)) | (mustDirtyEventParentCategories ? VisualElementFlags.EventInterestParentCategoriesDirty : ((VisualElementFlags)0));
				VisualElementFlags needDirtyFlags = mustDirtyFlags & ~ve.m_Flags;
				bool flag2 = needDirtyFlags > (VisualElementFlags)0;
				if (flag2)
				{
					VisualTreeHierarchyFlagsUpdater.DirtyHierarchy(ve, needDirtyFlags);
				}
				VisualTreeHierarchyFlagsUpdater.DirtyBoundingBoxHierarchy(ve);
				this.m_Version += 1U;
			}
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00081600 File Offset: 0x0007F800
		private static void DirtyHierarchy(VisualElement ve, VisualElementFlags mustDirtyFlags)
		{
			ve.m_Flags |= mustDirtyFlags;
			int count = ve.hierarchy.childCount;
			for (int i = 0; i < count; i++)
			{
				VisualElement child = ve.hierarchy[i];
				VisualElementFlags needDirtyFlags = mustDirtyFlags & ~child.m_Flags;
				bool flag = needDirtyFlags > (VisualElementFlags)0;
				if (flag)
				{
					VisualTreeHierarchyFlagsUpdater.DirtyHierarchy(child, needDirtyFlags);
				}
			}
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x00081670 File Offset: 0x0007F870
		private static void DirtyBoundingBoxHierarchy(VisualElement ve)
		{
			ve.isBoundingBoxDirty = true;
			ve.isWorldBoundingBoxDirty = true;
			VisualElement parent = ve.hierarchy.parent;
			while (parent != null && !parent.isBoundingBoxDirty)
			{
				parent.isBoundingBoxDirty = true;
				parent.isWorldBoundingBoxDirty = true;
				parent = parent.hierarchy.parent;
			}
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000816D4 File Offset: 0x0007F8D4
		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				this.m_LastVersion = this.m_Version;
				base.panel.UpdateElementUnderPointers();
				base.panel.visualTree.UpdateBoundingBox();
			}
		}

		// Token: 0x04000FF4 RID: 4084
		private uint m_Version = 0U;

		// Token: 0x04000FF5 RID: 4085
		private uint m_LastVersion = 0U;

		// Token: 0x04000FF6 RID: 4086
		private static readonly string s_Description = "Update Hierarchy Flags";

		// Token: 0x04000FF7 RID: 4087
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeHierarchyFlagsUpdater.s_Description);
	}
}
