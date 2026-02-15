using System;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x0200019E RID: 414
	internal class ListViewDraggerAnimated : ListViewDragger
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x0003A1CC File Offset: 0x000383CC
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x0003A1D4 File Offset: 0x000383D4
		public bool isDragging { get; private set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0003A1DD File Offset: 0x000383DD
		public ReusableCollectionItem draggedItem
		{
			get
			{
				return this.m_Item;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
		protected override bool supportsDragEvents
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x0003A1E5 File Offset: 0x000383E5
		public ListViewDraggerAnimated(BaseVerticalCollectionView listView)
			: base(listView)
		{
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x0003A1F0 File Offset: 0x000383F0
		protected internal override StartDragArgs StartDrag(Vector3 pointerPosition)
		{
			bool flag = !base.enabled;
			StartDragArgs startDragArgs;
			if (flag)
			{
				startDragArgs = base.StartDrag(pointerPosition);
			}
			else
			{
				base.targetView.ClearSelection();
				ReusableCollectionItem recycledItem = base.GetRecycledItem(pointerPosition);
				bool flag2 = recycledItem == null;
				if (flag2)
				{
					startDragArgs = new StartDragArgs(string.Empty, DragVisualMode.Rejected);
				}
				else
				{
					base.targetView.SetSelection(recycledItem.index);
					this.isDragging = true;
					this.m_Item = recycledItem;
					base.targetView.virtualizationController.StartDragItem(this.m_Item);
					float y = this.m_Item.rootElement.layout.y;
					this.m_SelectionHeight = this.m_Item.rootElement.layout.height;
					this.m_Item.rootElement.style.position = Position.Absolute;
					this.m_Item.rootElement.style.height = this.m_Item.rootElement.layout.height;
					this.m_Item.rootElement.style.width = this.m_Item.rootElement.layout.width;
					this.m_Item.rootElement.style.top = y;
					this.m_DragStartIndex = this.m_Item.index;
					this.m_CurrentIndex = this.m_DragStartIndex;
					this.m_CurrentPointerPosition = pointerPosition;
					this.m_LocalOffsetOnStart = base.targetScrollView.contentContainer.WorldToLocal(pointerPosition).y - y;
					ReusableCollectionItem item = base.targetView.GetRecycledItemFromIndex(this.m_CurrentIndex + 1);
					bool flag3 = item != null;
					if (flag3)
					{
						this.m_OffsetItem = item;
						this.Animate(this.m_OffsetItem, this.m_SelectionHeight);
						this.m_OffsetItem.rootElement.style.paddingTop = this.m_SelectionHeight;
						bool flag4 = base.targetView.virtualizationMethod == CollectionVirtualizationMethod.FixedHeight;
						if (flag4)
						{
							this.m_OffsetItem.rootElement.style.height = base.targetView.fixedItemHeight + this.m_SelectionHeight;
						}
					}
					startDragArgs = base.dragAndDropController.SetupDragAndDrop(new int[] { this.m_Item.index }, true);
				}
			}
			return startDragArgs;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x0003A464 File Offset: 0x00038664
		protected internal override void UpdateDrag(Vector3 pointerPosition)
		{
			bool flag = !base.enabled;
			if (flag)
			{
				base.UpdateDrag(pointerPosition);
			}
			else
			{
				bool flag2 = this.m_Item == null;
				if (!flag2)
				{
					base.HandleDragAndScroll(pointerPosition);
					this.m_CurrentPointerPosition = pointerPosition;
					Vector2 positionInContainer = base.targetScrollView.contentContainer.WorldToLocal(this.m_CurrentPointerPosition);
					Rect itemLayout = this.m_Item.rootElement.layout;
					float contentHeight = base.targetScrollView.contentContainer.layout.height;
					itemLayout.y = Mathf.Clamp(positionInContainer.y - this.m_LocalOffsetOnStart, 0f, contentHeight - this.m_SelectionHeight);
					float y = base.targetScrollView.contentContainer.resolvedStyle.paddingTop;
					this.m_CurrentIndex = -1;
					foreach (ReusableCollectionItem item in base.targetView.activeItems)
					{
						bool flag3 = item.index < 0 || (item.rootElement.style.display == DisplayStyle.None && !item.isDragGhost);
						if (!flag3)
						{
							bool flag4 = item.index == this.m_Item.index && item.index < base.targetView.itemsSource.Count - 1;
							if (flag4)
							{
								float nextExpectedHeight = base.targetView.virtualizationController.GetExpectedItemHeight(item.index + 1);
								bool flag5 = itemLayout.y <= y + nextExpectedHeight * 0.5f;
								if (flag5)
								{
									this.m_CurrentIndex = item.index;
								}
							}
							else
							{
								float expectedHeight = base.targetView.virtualizationController.GetExpectedItemHeight(item.index);
								bool flag6 = itemLayout.y <= y + expectedHeight * 0.5f;
								if (flag6)
								{
									bool flag7 = this.m_CurrentIndex == -1;
									if (flag7)
									{
										this.m_CurrentIndex = item.index;
									}
									bool flag8 = this.m_OffsetItem == item;
									if (flag8)
									{
										break;
									}
									this.Animate(this.m_OffsetItem, 0f);
									this.Animate(item, this.m_SelectionHeight);
									this.m_OffsetItem = item;
									break;
								}
								else
								{
									y += expectedHeight;
								}
							}
						}
					}
					bool flag9 = this.m_CurrentIndex == -1;
					if (flag9)
					{
						this.m_CurrentIndex = base.targetView.itemsSource.Count;
						this.Animate(this.m_OffsetItem, 0f);
						this.m_OffsetItem = null;
					}
					this.m_Item.rootElement.layout = itemLayout;
					this.m_Item.rootElement.BringToFront();
				}
			}
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0003A75C File Offset: 0x0003895C
		private void Animate(ReusableCollectionItem element, float paddingTop)
		{
			bool flag = element == null;
			if (!flag)
			{
				bool flag2 = element.animator != null;
				if (flag2)
				{
					bool flag3 = (element.animator.isRunning && element.animator.to.paddingTop == paddingTop) || (!element.animator.isRunning && element.rootElement.style.paddingTop == paddingTop);
					if (flag3)
					{
						return;
					}
				}
				ValueAnimation<StyleValues> animator = element.animator;
				if (animator != null)
				{
					animator.Stop();
				}
				ValueAnimation<StyleValues> animator2 = element.animator;
				if (animator2 != null)
				{
					animator2.Recycle();
				}
				StyleValues targetStyle = ((base.targetView.virtualizationMethod == CollectionVirtualizationMethod.FixedHeight) ? new StyleValues
				{
					paddingTop = paddingTop,
					height = base.targetView.ResolveItemHeight(-1f) + paddingTop
				} : new StyleValues
				{
					paddingTop = paddingTop
				});
				element.animator = element.rootElement.experimental.animation.Start(targetStyle, 500);
				element.animator.KeepAlive();
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x0003A884 File Offset: 0x00038A84
		protected internal override void OnDrop(Vector3 pointerPosition)
		{
			bool flag = !base.enabled;
			if (flag)
			{
				base.OnDrop(pointerPosition);
			}
			else
			{
				bool flag2 = this.m_Item == null;
				if (!flag2)
				{
					this.isDragging = false;
					this.m_Item.rootElement.ClearManualLayout();
					base.targetView.virtualizationController.EndDrag(this.m_CurrentIndex);
					bool flag3 = this.m_OffsetItem != null;
					if (flag3)
					{
						ValueAnimation<StyleValues> animator = this.m_OffsetItem.animator;
						if (animator != null)
						{
							animator.Stop();
						}
						ValueAnimation<StyleValues> animator2 = this.m_OffsetItem.animator;
						if (animator2 != null)
						{
							animator2.Recycle();
						}
						this.m_OffsetItem.animator = null;
						this.m_OffsetItem.rootElement.style.paddingTop = 0f;
						bool flag4 = base.targetView.virtualizationMethod == CollectionVirtualizationMethod.FixedHeight;
						if (flag4)
						{
							this.m_OffsetItem.rootElement.style.height = base.targetView.ResolveItemHeight(-1f);
						}
					}
					ListViewDragger.DragPosition dragPosition = new ListViewDragger.DragPosition
					{
						recycledItem = this.m_Item,
						insertAtIndex = this.m_CurrentIndex,
						dropPosition = DragAndDropPosition.BetweenItems
					};
					DragAndDropArgs args = base.MakeDragAndDropArgs(dragPosition);
					base.dragAndDropController.OnDrop(args);
					base.dragAndDrop.AcceptDrag();
					this.m_Item = null;
					this.m_OffsetItem = null;
				}
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000020EA File Offset: 0x000002EA
		protected override void ClearDragAndDropUI(bool dragCancelled)
		{
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0003AA00 File Offset: 0x00038C00
		protected override bool TryGetDragPosition(Vector2 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			dragPosition.recycledItem = this.m_Item;
			dragPosition.insertAtIndex = this.m_CurrentIndex;
			dragPosition.dropPosition = DragAndDropPosition.BetweenItems;
			return true;
		}

		// Token: 0x040007A4 RID: 1956
		private int m_DragStartIndex;

		// Token: 0x040007A5 RID: 1957
		private int m_CurrentIndex;

		// Token: 0x040007A6 RID: 1958
		private float m_SelectionHeight;

		// Token: 0x040007A7 RID: 1959
		private float m_LocalOffsetOnStart;

		// Token: 0x040007A8 RID: 1960
		private Vector3 m_CurrentPointerPosition;

		// Token: 0x040007A9 RID: 1961
		private ReusableCollectionItem m_Item;

		// Token: 0x040007AA RID: 1962
		private ReusableCollectionItem m_OffsetItem;
	}
}
