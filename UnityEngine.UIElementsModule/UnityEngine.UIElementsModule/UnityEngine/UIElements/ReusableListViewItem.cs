using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000066 RID: 102
	internal class ReusableListViewItem : ReusableCollectionItem
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600039C RID: 924 RVA: 0x000119C8 File Offset: 0x0000FBC8
		public override VisualElement rootElement
		{
			get
			{
				return this.m_Container ?? base.bindableElement;
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x000119DC File Offset: 0x0000FBDC
		public void Init(VisualElement item, bool usesAnimatedDragger)
		{
			base.Init(item);
			VisualElement root = new VisualElement
			{
				name = BaseListView.reorderableItemUssClassName
			};
			this.UpdateHierarchy(root, base.bindableElement, usesAnimatedDragger);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00011A14 File Offset: 0x0000FC14
		protected void UpdateHierarchy(VisualElement root, VisualElement item, bool usesAnimatedDragger)
		{
			if (usesAnimatedDragger)
			{
				bool flag = this.m_Container != null;
				if (!flag)
				{
					this.m_Container = root;
					this.m_Container.AddToClassList(BaseListView.reorderableItemUssClassName);
					this.m_DragHandle = new VisualElement
					{
						name = BaseListView.reorderableItemHandleUssClassName
					};
					this.m_DragHandle.AddToClassList(BaseListView.reorderableItemHandleUssClassName);
					VisualElement handle = new VisualElement
					{
						name = BaseListView.reorderableItemHandleBarUssClassName
					};
					handle.AddToClassList(BaseListView.reorderableItemHandleBarUssClassName);
					this.m_DragHandle.Add(handle);
					VisualElement handle2 = new VisualElement
					{
						name = BaseListView.reorderableItemHandleBarUssClassName
					};
					handle2.AddToClassList(BaseListView.reorderableItemHandleBarUssClassName);
					this.m_DragHandle.Add(handle2);
					this.m_ItemContainer = new VisualElement
					{
						name = BaseListView.reorderableItemContainerUssClassName
					};
					this.m_ItemContainer.AddToClassList(BaseListView.reorderableItemContainerUssClassName);
					this.m_ItemContainer.Add(item);
					this.m_Container.Add(this.m_DragHandle);
					this.m_Container.Add(this.m_ItemContainer);
				}
			}
			else
			{
				bool flag2 = this.m_Container == null;
				if (!flag2)
				{
					this.m_Container.RemoveFromHierarchy();
					this.m_Container = null;
				}
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00011B58 File Offset: 0x0000FD58
		public void UpdateDragHandle(bool needsDragHandle)
		{
			if (needsDragHandle)
			{
				bool flag = this.m_DragHandle.parent == null;
				if (flag)
				{
					this.rootElement.Insert(0, this.m_DragHandle);
					this.rootElement.AddToClassList(BaseListView.reorderableItemUssClassName);
				}
			}
			else
			{
				VisualElement dragHandle = this.m_DragHandle;
				bool flag2 = ((dragHandle != null) ? dragHandle.parent : null) != null;
				if (flag2)
				{
					this.m_DragHandle.RemoveFromHierarchy();
					this.rootElement.RemoveFromClassList(BaseListView.reorderableItemUssClassName);
				}
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00011BE4 File Offset: 0x0000FDE4
		public void SetDragHandleEnabled(bool enabled)
		{
			bool flag = this.m_DragHandle != null;
			if (flag)
			{
				this.m_DragHandle.SetEnabled(enabled);
				this.m_DragHandle.tooltip = (enabled ? null : ReusableListViewItem.k_SortingDisablesReorderingTooltip);
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00011C25 File Offset: 0x0000FE25
		public override void PreAttachElement()
		{
			base.PreAttachElement();
			this.rootElement.AddToClassList(BaseListView.itemUssClassName);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00011C40 File Offset: 0x0000FE40
		public override void DetachElement()
		{
			base.DetachElement();
			this.rootElement.RemoveFromClassList(BaseListView.itemUssClassName);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00011C5C File Offset: 0x0000FE5C
		public override void SetDragGhost(bool dragGhost)
		{
			base.SetDragGhost(dragGhost);
			bool flag = this.m_DragHandle != null;
			if (flag)
			{
				this.m_DragHandle.EnableInClassList("unity-hidden", base.isDragGhost);
			}
		}

		// Token: 0x040001E7 RID: 487
		private static readonly string k_SortingDisablesReorderingTooltip = "Reordering is disabled when the collection is being sorted.";

		// Token: 0x040001E8 RID: 488
		private VisualElement m_Container;

		// Token: 0x040001E9 RID: 489
		private VisualElement m_DragHandle;

		// Token: 0x040001EA RID: 490
		private VisualElement m_ItemContainer;
	}
}
