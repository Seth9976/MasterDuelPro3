using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005E RID: 94
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal abstract class CollectionVirtualizationController
	{
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000336 RID: 822
		// (set) Token: 0x06000337 RID: 823
		public abstract int firstVisibleIndex { get; protected set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000338 RID: 824
		public abstract int visibleItemCount { get; }

		// Token: 0x06000339 RID: 825 RVA: 0x0000EC46 File Offset: 0x0000CE46
		protected CollectionVirtualizationController(ScrollView scrollView)
		{
			this.m_ScrollView = scrollView;
		}

		// Token: 0x0600033A RID: 826
		public abstract void Refresh(bool rebuild);

		// Token: 0x0600033B RID: 827
		public abstract void ScrollToItem(int id);

		// Token: 0x0600033C RID: 828
		public abstract void Resize(Vector2 size);

		// Token: 0x0600033D RID: 829
		public abstract void OnScroll(Vector2 offset);

		// Token: 0x0600033E RID: 830
		public abstract int GetIndexFromPosition(Vector2 position);

		// Token: 0x0600033F RID: 831
		public abstract float GetExpectedItemHeight(int index);

		// Token: 0x06000340 RID: 832
		public abstract float GetExpectedContentHeight();

		// Token: 0x06000341 RID: 833
		public abstract void OnFocusIn(VisualElement leafTarget);

		// Token: 0x06000342 RID: 834
		public abstract void OnFocusOut(VisualElement willFocus);

		// Token: 0x06000343 RID: 835
		public abstract void UpdateBackground();

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000344 RID: 836
		public abstract IEnumerable<ReusableCollectionItem> activeItems { get; }

		// Token: 0x06000345 RID: 837
		internal abstract void StartDragItem(ReusableCollectionItem item);

		// Token: 0x06000346 RID: 838
		internal abstract void EndDrag(int dropIndex);

		// Token: 0x06000347 RID: 839
		public abstract void UnbindAll();

		// Token: 0x040001B9 RID: 441
		protected readonly ScrollView m_ScrollView;
	}
}
