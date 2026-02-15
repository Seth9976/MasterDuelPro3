using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200019B RID: 411
	internal class ListViewDragger : DragEventsProcessor
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00038C82 File Offset: 0x00036E82
		protected BaseVerticalCollectionView targetView
		{
			get
			{
				return this.m_Target as BaseVerticalCollectionView;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x00038C8F File Offset: 0x00036E8F
		protected ScrollView targetScrollView
		{
			get
			{
				return this.targetView.scrollView;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000BFB RID: 3067 RVA: 0x00038C9C File Offset: 0x00036E9C
		// (set) Token: 0x06000BFC RID: 3068 RVA: 0x00038CA4 File Offset: 0x00036EA4
		public ICollectionDragAndDropController dragAndDropController { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000BFD RID: 3069 RVA: 0x00038CAD File Offset: 0x00036EAD
		// (set) Token: 0x06000BFE RID: 3070 RVA: 0x00038CB8 File Offset: 0x00036EB8
		internal bool enabled
		{
			get
			{
				return this.m_Enabled;
			}
			set
			{
				this.m_Enabled = value;
				bool flag = this.targetView is BaseListView;
				if (flag)
				{
					foreach (ReusableCollectionItem item in this.targetView.activeItems)
					{
						ReusableListViewItem listItem = item as ReusableListViewItem;
						bool flag2 = listItem == null;
						if (!flag2)
						{
							listItem.SetDragHandleEnabled(this.targetView.dragger.enabled);
						}
					}
				}
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x00038D50 File Offset: 0x00036F50
		public ListViewDragger(BaseVerticalCollectionView listView)
			: base(listView)
		{
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00038D78 File Offset: 0x00036F78
		protected override bool CanStartDrag(Vector3 pointerPosition)
		{
			bool flag = this.dragAndDropController == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = !this.targetScrollView.contentContainer.worldBound.Contains(pointerPosition);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					ReusableCollectionItem recycledItem = this.GetRecycledItem(pointerPosition);
					bool flag4 = recycledItem != null && this.targetView.HasCanStartDrag();
					if (flag4)
					{
						IEnumerable<int> enumerable2;
						if (!this.targetView.selectedIds.Any<int>())
						{
							IEnumerable<int> enumerable = new int[] { recycledItem.id };
							enumerable2 = enumerable;
						}
						else
						{
							enumerable2 = this.targetView.selectedIds;
						}
						IEnumerable<int> ids = enumerable2;
						flag2 = this.targetView.RaiseCanStartDrag(recycledItem, ids);
					}
					else
					{
						bool flag5 = this.targetView.selectedIds.Any<int>();
						if (flag5)
						{
							flag2 = this.dragAndDropController.CanStartDrag(this.targetView.selectedIds);
						}
						else
						{
							flag2 = recycledItem != null && this.dragAndDropController.CanStartDrag(new int[] { recycledItem.id });
						}
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00038E7C File Offset: 0x0003707C
		protected internal override StartDragArgs StartDrag(Vector3 pointerPosition)
		{
			ReusableCollectionItem recycledItem = this.GetRecycledItem(pointerPosition);
			bool flag = recycledItem != null;
			IEnumerable<int> ids;
			if (flag)
			{
				bool flag2 = !this.targetView.selectedIndices.Contains(recycledItem.index);
				if (flag2)
				{
					this.targetView.SetSelection(recycledItem.index);
				}
				ids = this.targetView.selectedIds;
			}
			else
			{
				ids = (this.targetView.selectedIds.Any<int>() ? this.targetView.selectedIds : Enumerable.Empty<int>());
			}
			StartDragArgs startDragArgs = this.dragAndDropController.SetupDragAndDrop(ids, false);
			startDragArgs = this.targetView.RaiseSetupDragAndDrop(recycledItem, this.dragAndDropController.GetSortedSelectedIds(), startDragArgs);
			startDragArgs.SetGenericData("__unity-drag-and-drop__source-view", this.targetView);
			return startDragArgs;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00038F48 File Offset: 0x00037148
		protected internal override void UpdateDrag(Vector3 pointerPosition)
		{
			ListViewDragger.DragPosition dragPosition = default(ListViewDragger.DragPosition);
			DragVisualMode visualMode = this.GetVisualMode(pointerPosition, ref dragPosition);
			bool flag = visualMode == DragVisualMode.Rejected;
			if (flag)
			{
				this.ClearDragAndDropUI(false);
			}
			else
			{
				this.HandleDragAndScroll(pointerPosition);
				this.HandleAutoExpansion(pointerPosition);
				this.ApplyDragAndDropUI(dragPosition);
			}
			base.dragAndDrop.SetVisualMode(visualMode);
			base.dragAndDrop.UpdateDrag(pointerPosition);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00038FBC File Offset: 0x000371BC
		private DragVisualMode GetVisualMode(Vector3 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			bool flag = this.dragAndDropController == null;
			DragVisualMode dragVisualMode;
			if (flag)
			{
				dragVisualMode = DragVisualMode.Rejected;
			}
			else
			{
				bool foundPosition = this.TryGetDragPosition(pointerPosition, ref dragPosition);
				DragAndDropArgs args = this.MakeDragAndDropArgs(dragPosition);
				DragVisualMode mode = this.targetView.RaiseHandleDragAndDrop(pointerPosition, args);
				bool flag2 = mode > DragVisualMode.None;
				if (flag2)
				{
					dragVisualMode = mode;
				}
				else
				{
					dragVisualMode = (foundPosition ? this.dragAndDropController.HandleDragAndDrop(args) : DragVisualMode.Rejected);
				}
			}
			return dragVisualMode;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00039038 File Offset: 0x00037238
		protected internal override void OnDrop(Vector3 pointerPosition)
		{
			ListViewDragger.DragPosition dragPosition = default(ListViewDragger.DragPosition);
			bool flag = !this.TryGetDragPosition(pointerPosition, ref dragPosition);
			if (!flag)
			{
				DragAndDropArgs args = this.MakeDragAndDropArgs(dragPosition);
				DragVisualMode mode = this.targetView.RaiseDrop(pointerPosition, args);
				bool flag2 = mode > DragVisualMode.None;
				if (flag2)
				{
					bool flag3 = mode != DragVisualMode.Rejected;
					if (flag3)
					{
						base.dragAndDrop.AcceptDrag();
					}
					else
					{
						this.dragAndDropController.DragCleanup();
					}
				}
				else
				{
					bool flag4 = this.IsDraggingDisabled();
					if (!flag4)
					{
						bool flag5 = this.dragAndDropController.HandleDragAndDrop(args) != DragVisualMode.Rejected;
						if (flag5)
						{
							this.dragAndDropController.OnDrop(args);
							base.dragAndDrop.AcceptDrag();
						}
						else
						{
							this.dragAndDropController.DragCleanup();
						}
					}
				}
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00039114 File Offset: 0x00037314
		internal void HandleDragAndScroll(Vector2 pointerPosition)
		{
			bool scrollUp = pointerPosition.y < this.targetScrollView.worldBound.yMin + 5f;
			bool scrollDown = pointerPosition.y > this.targetScrollView.worldBound.yMax - 5f;
			bool flag = scrollUp || scrollDown;
			if (flag)
			{
				Vector2 offset = this.targetScrollView.scrollOffset + (scrollUp ? Vector2.down : Vector2.up) * 20f;
				offset.y = Mathf.Clamp(offset.y, 0f, Mathf.Max(0f, this.targetScrollView.contentContainer.worldBound.height - this.targetScrollView.contentViewport.worldBound.height));
				this.targetScrollView.scrollOffset = offset;
			}
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00039200 File Offset: 0x00037400
		private void HandleAutoExpansion(Vector2 pointerPosition)
		{
			ReusableCollectionItem recycledItem = this.GetRecycledItem(pointerPosition);
			bool flag = recycledItem == null;
			if (!flag)
			{
				this.dragAndDropController.HandleAutoExpand(recycledItem, pointerPosition);
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00039234 File Offset: 0x00037434
		private void ApplyDragAndDropUI(ListViewDragger.DragPosition dragPosition)
		{
			bool flag = this.m_LastDragPosition.Equals(dragPosition) || this.IsDraggingDisabled();
			if (!flag)
			{
				bool flag2 = this.m_DragHoverBar == null;
				if (flag2)
				{
					this.m_DragHoverBar = new VisualElement();
					this.m_DragHoverBar.AddToClassList(BaseVerticalCollectionView.dragHoverBarUssClassName);
					this.m_DragHoverBar.style.width = this.targetView.localBound.width;
					this.m_DragHoverBar.style.visibility = Visibility.Hidden;
					this.m_DragHoverBar.pickingMode = PickingMode.Ignore;
					this.targetView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.<ApplyDragAndDropUI>g__GeometryChangedCallback|31_0), TrickleDown.NoTrickleDown);
					this.targetScrollView.contentViewport.Add(this.m_DragHoverBar);
				}
				bool flag3 = this.m_DragHoverItemMarker == null && this.targetView is BaseTreeView;
				if (flag3)
				{
					this.m_DragHoverItemMarker = new VisualElement();
					this.m_DragHoverItemMarker.AddToClassList(BaseVerticalCollectionView.dragHoverMarkerUssClassName);
					this.m_DragHoverItemMarker.style.visibility = Visibility.Hidden;
					this.m_DragHoverItemMarker.pickingMode = PickingMode.Ignore;
					this.m_DragHoverBar.Add(this.m_DragHoverItemMarker);
					this.m_DragHoverSiblingMarker = new VisualElement();
					this.m_DragHoverSiblingMarker.AddToClassList(BaseVerticalCollectionView.dragHoverMarkerUssClassName);
					this.m_DragHoverSiblingMarker.style.visibility = Visibility.Hidden;
					this.m_DragHoverSiblingMarker.pickingMode = PickingMode.Ignore;
					this.targetScrollView.contentViewport.Add(this.m_DragHoverSiblingMarker);
				}
				this.ClearDragAndDropUI(false);
				this.m_LastDragPosition = dragPosition;
				switch (dragPosition.dropPosition)
				{
				case DragAndDropPosition.OverItem:
					dragPosition.recycledItem.rootElement.AddToClassList(BaseVerticalCollectionView.itemDragHoverUssClassName);
					break;
				case DragAndDropPosition.BetweenItems:
				{
					bool flag4 = dragPosition.insertAtIndex == 0;
					if (flag4)
					{
						this.PlaceHoverBarAt(0f, -1f, -1f);
					}
					else
					{
						ReusableCollectionItem beforeItem = this.targetView.GetRecycledItemFromIndex(dragPosition.insertAtIndex - 1);
						ReusableCollectionItem afterItem = this.targetView.GetRecycledItemFromIndex(dragPosition.insertAtIndex);
						this.PlaceHoverBarAtElement(beforeItem ?? afterItem);
					}
					break;
				}
				case DragAndDropPosition.OutsideItems:
				{
					ReusableCollectionItem recycledItem = this.targetView.GetRecycledItemFromIndex(this.targetView.itemsSource.Count - 1);
					bool flag5 = recycledItem != null;
					if (flag5)
					{
						this.PlaceHoverBarAtElement(recycledItem);
					}
					else
					{
						this.PlaceHoverBarAt(0f, -1f, -1f);
					}
					break;
				}
				default:
					throw new ArgumentOutOfRangeException("dropPosition", dragPosition.dropPosition, "Unsupported dropPosition value");
				}
			}
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x000394F4 File Offset: 0x000376F4
		protected virtual bool TryGetDragPosition(Vector2 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			ReusableCollectionItem recycledItem = this.GetRecycledItem(pointerPosition);
			bool flag = recycledItem == null;
			bool flag3;
			if (flag)
			{
				bool flag2 = !this.targetView.worldBound.Contains(pointerPosition);
				if (flag2)
				{
					flag3 = false;
				}
				else
				{
					dragPosition.dropPosition = DragAndDropPosition.OutsideItems;
					bool flag4 = pointerPosition.y >= this.targetScrollView.contentContainer.worldBound.yMax;
					if (flag4)
					{
						dragPosition.insertAtIndex = this.targetView.itemsSource.Count;
					}
					else
					{
						dragPosition.insertAtIndex = 0;
					}
					this.HandleTreePosition(pointerPosition, ref dragPosition);
					flag3 = true;
				}
			}
			else
			{
				bool flag5 = recycledItem.rootElement.worldBound.yMax - pointerPosition.y < 5f;
				if (flag5)
				{
					dragPosition.insertAtIndex = recycledItem.index + 1;
					dragPosition.dropPosition = DragAndDropPosition.BetweenItems;
				}
				else
				{
					bool flag6 = pointerPosition.y - recycledItem.rootElement.worldBound.yMin > 5f;
					if (flag6)
					{
						Vector2 scrollOffset = this.targetScrollView.scrollOffset;
						this.targetScrollView.ScrollTo(recycledItem.rootElement);
						bool flag7 = !Mathf.Approximately(scrollOffset.x, this.targetScrollView.scrollOffset.x) || !Mathf.Approximately(scrollOffset.y, this.targetScrollView.scrollOffset.y);
						if (flag7)
						{
							return this.TryGetDragPosition(pointerPosition, ref dragPosition);
						}
						dragPosition.recycledItem = recycledItem;
						dragPosition.insertAtIndex = recycledItem.index;
						dragPosition.dropPosition = DragAndDropPosition.OverItem;
					}
					else
					{
						dragPosition.insertAtIndex = recycledItem.index;
						dragPosition.dropPosition = DragAndDropPosition.BetweenItems;
					}
				}
				this.HandleTreePosition(pointerPosition, ref dragPosition);
				flag3 = true;
			}
			return flag3;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x000396C0 File Offset: 0x000378C0
		private void HandleTreePosition(Vector2 pointerPosition, ref ListViewDragger.DragPosition dragPosition)
		{
			dragPosition.parentId = -1;
			dragPosition.childIndex = -1;
			this.m_LeftIndentation = -1f;
			this.m_SiblingBottom = -1f;
			BaseTreeView treeView = this.targetView as BaseTreeView;
			bool flag = treeView == null;
			if (!flag)
			{
				bool flag2 = dragPosition.insertAtIndex < 0;
				if (!flag2)
				{
					BaseTreeViewController treeController = treeView.viewController;
					bool flag3 = dragPosition.dropPosition == DragAndDropPosition.OverItem;
					if (flag3)
					{
						dragPosition.parentId = treeController.GetIdForIndex(dragPosition.insertAtIndex);
						dragPosition.childIndex = -1;
					}
					else
					{
						bool flag4 = dragPosition.insertAtIndex <= 0;
						if (flag4)
						{
							dragPosition.childIndex = 0;
						}
						else
						{
							this.HandleSiblingInsertionAtAvailableDepthsAndChangeTargetIfNeeded(ref dragPosition, pointerPosition);
						}
					}
				}
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00039774 File Offset: 0x00037974
		private void HandleSiblingInsertionAtAvailableDepthsAndChangeTargetIfNeeded(ref ListViewDragger.DragPosition dragPosition, Vector2 pointerPosition)
		{
			BaseTreeView treeView = this.targetView as BaseTreeView;
			bool flag = treeView == null;
			if (!flag)
			{
				BaseTreeViewController treeController = treeView.viewController;
				int targetIndex = dragPosition.insertAtIndex;
				int initialTargetId = treeController.GetIdForIndex(targetIndex);
				int previousItemId;
				int nextItemId;
				this.GetPreviousAndNextItemsIgnoringDraggedItems(dragPosition.insertAtIndex, out previousItemId, out nextItemId);
				bool flag2 = previousItemId == BaseTreeView.invalidId;
				if (!flag2)
				{
					bool hoveringBetweenExpandedParentAndFirstChild = treeController.HasChildren(previousItemId) && treeView.IsExpanded(previousItemId);
					int previousItemDepth = treeController.GetIndentationDepth(previousItemId);
					int nextItemDepth = treeController.GetIndentationDepth(nextItemId);
					int minDepth = ((nextItemId != BaseTreeView.invalidId) ? nextItemDepth : 0);
					int maxDepth = treeController.GetIndentationDepth(previousItemId) + (hoveringBetweenExpandedParentAndFirstChild ? 1 : 0);
					int targetId = previousItemId;
					float toggleWidth = 15f;
					float indentWidth = 15f;
					bool flag3 = previousItemDepth > 0;
					if (flag3)
					{
						VisualElement rootElement = treeView.GetRootElementForId(previousItemId);
						VisualElement indentElement = rootElement.Q(BaseTreeView.itemIndentUssClassName, null);
						VisualElement toggle = rootElement.Q(BaseTreeView.itemToggleUssClassName, null);
						toggleWidth = toggle.layout.width;
						indentWidth = indentElement.layout.width / (float)previousItemDepth;
					}
					else
					{
						int initialItemDepth = treeView.viewController.GetIndentationDepth(initialTargetId);
						bool flag4 = initialItemDepth > 0;
						if (flag4)
						{
							VisualElement rootElement2 = treeView.GetRootElementForId(initialTargetId);
							VisualElement indentElement2 = rootElement2.Q(BaseTreeView.itemIndentUssClassName, null);
							VisualElement toggle2 = rootElement2.Q(BaseTreeView.itemToggleUssClassName, null);
							toggleWidth = toggle2.layout.width;
							indentWidth = indentElement2.layout.width / (float)initialItemDepth;
						}
					}
					bool flag5 = maxDepth <= minDepth;
					if (flag5)
					{
						this.m_LeftIndentation = toggleWidth + indentWidth * (float)minDepth;
						bool flag6 = hoveringBetweenExpandedParentAndFirstChild;
						if (flag6)
						{
							dragPosition.parentId = previousItemId;
							dragPosition.childIndex = 0;
						}
						else
						{
							dragPosition.parentId = treeController.GetParentId(previousItemId);
							dragPosition.childIndex = treeController.GetChildIndexForId(nextItemId);
						}
					}
					else
					{
						Vector2 localMousePosition = treeView.scrollView.contentContainer.WorldToLocal(pointerPosition);
						int cursorDepth = Mathf.FloorToInt((localMousePosition.x - toggleWidth) / indentWidth);
						bool flag7 = cursorDepth >= maxDepth;
						if (flag7)
						{
							this.m_LeftIndentation = toggleWidth + indentWidth * (float)maxDepth;
							bool flag8 = hoveringBetweenExpandedParentAndFirstChild;
							if (flag8)
							{
								dragPosition.parentId = previousItemId;
								dragPosition.childIndex = 0;
							}
							else
							{
								dragPosition.parentId = treeController.GetParentId(previousItemId);
								dragPosition.childIndex = treeController.GetChildIndexForId(previousItemId) + 1;
							}
						}
						else
						{
							int targetDepth;
							for (targetDepth = treeController.GetIndentationDepth(targetId); targetDepth > minDepth; targetDepth--)
							{
								bool flag9 = targetDepth == cursorDepth;
								if (flag9)
								{
									break;
								}
								targetId = treeController.GetParentId(targetId);
							}
							bool didChangeTargetToAncestor = targetId != initialTargetId;
							bool flag10 = didChangeTargetToAncestor;
							if (flag10)
							{
								VisualElement siblingRoot = treeView.GetRootElementForId(targetId);
								bool flag11 = siblingRoot != null;
								if (flag11)
								{
									VisualElement contentViewport = this.targetScrollView.contentViewport;
									Rect elementBounds = contentViewport.WorldToLocal(siblingRoot.worldBound);
									bool flag12 = contentViewport.localBound.yMin < elementBounds.yMax && elementBounds.yMax < contentViewport.localBound.yMax;
									if (flag12)
									{
										this.m_SiblingBottom = elementBounds.yMax;
									}
								}
							}
							dragPosition.parentId = treeController.GetParentId(targetId);
							dragPosition.childIndex = treeController.GetChildIndexForId(targetId) + 1;
							this.m_LeftIndentation = toggleWidth + indentWidth * (float)targetDepth;
						}
					}
				}
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00039AE4 File Offset: 0x00037CE4
		private void GetPreviousAndNextItemsIgnoringDraggedItems(int insertAtIndex, out int previousItemId, out int nextItemId)
		{
			previousItemId = (nextItemId = -1);
			int previousItemIndex = insertAtIndex - 1;
			int nextItemIndex = insertAtIndex;
			while (previousItemIndex >= 0)
			{
				int id = this.targetView.viewController.GetIdForIndex(previousItemIndex);
				bool flag = !this.dragAndDropController.GetSortedSelectedIds().Contains(id);
				if (flag)
				{
					previousItemId = id;
					break;
				}
				previousItemIndex--;
			}
			while (nextItemIndex < this.targetView.itemsSource.Count)
			{
				int id2 = this.targetView.viewController.GetIdForIndex(nextItemIndex);
				bool flag2 = !this.dragAndDropController.GetSortedSelectedIds().Contains(id2);
				if (flag2)
				{
					nextItemId = id2;
					break;
				}
				nextItemIndex++;
			}
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00039BA0 File Offset: 0x00037DA0
		protected DragAndDropArgs MakeDragAndDropArgs(ListViewDragger.DragPosition dragPosition)
		{
			object target = null;
			ReusableCollectionItem recycledItem = dragPosition.recycledItem;
			bool flag = recycledItem != null;
			if (flag)
			{
				target = this.targetView.viewController.GetItemForIndex(recycledItem.index);
			}
			return new DragAndDropArgs
			{
				target = target,
				insertAtIndex = dragPosition.insertAtIndex,
				parentId = dragPosition.parentId,
				childIndex = dragPosition.childIndex,
				dragAndDropPosition = dragPosition.dropPosition,
				dragAndDropData = DragAndDropUtility.GetDragAndDrop(this.m_Target.panel).data
			};
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00039C44 File Offset: 0x00037E44
		private float GetHoverBarTopPosition(ReusableCollectionItem item)
		{
			VisualElement contentViewport = this.targetScrollView.contentViewport;
			return Mathf.Min(contentViewport.WorldToLocal(item.rootElement.worldBound).yMax, contentViewport.localBound.yMax - 2f);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00039C98 File Offset: 0x00037E98
		private void PlaceHoverBarAtElement(ReusableCollectionItem item)
		{
			this.PlaceHoverBarAt(this.GetHoverBarTopPosition(item), this.m_LeftIndentation, this.m_SiblingBottom);
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00039CB8 File Offset: 0x00037EB8
		private void PlaceHoverBarAt(float top, float indentationPadding = -1f, float siblingBottom = -1f)
		{
			this.m_DragHoverBar.style.top = top;
			this.m_DragHoverBar.style.visibility = Visibility.Visible;
			bool flag = this.m_DragHoverItemMarker != null;
			if (flag)
			{
				this.m_DragHoverItemMarker.style.visibility = Visibility.Visible;
			}
			bool flag2 = indentationPadding >= 0f;
			if (flag2)
			{
				this.m_DragHoverBar.style.marginLeft = indentationPadding;
				this.m_DragHoverBar.style.width = this.targetView.localBound.width - indentationPadding;
				bool flag3 = siblingBottom > 0f && this.m_DragHoverSiblingMarker != null;
				if (flag3)
				{
					this.m_DragHoverSiblingMarker.style.top = siblingBottom;
					this.m_DragHoverSiblingMarker.style.visibility = Visibility.Visible;
					this.m_DragHoverSiblingMarker.style.marginLeft = indentationPadding;
				}
			}
			else
			{
				this.m_DragHoverBar.style.marginLeft = 0f;
				this.m_DragHoverBar.style.width = this.targetView.localBound.width;
			}
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00039E18 File Offset: 0x00038018
		protected override void ClearDragAndDropUI(bool dragCancelled)
		{
			if (dragCancelled)
			{
				this.dragAndDropController.DragCleanup();
			}
			this.targetView.elementPanel.cursorManager.ResetCursor();
			this.m_LastDragPosition = default(ListViewDragger.DragPosition);
			foreach (ReusableCollectionItem item in this.targetView.activeItems)
			{
				item.rootElement.RemoveFromClassList(BaseVerticalCollectionView.itemDragHoverUssClassName);
			}
			bool flag = this.m_DragHoverBar != null;
			if (flag)
			{
				this.m_DragHoverBar.style.visibility = Visibility.Hidden;
			}
			bool flag2 = this.m_DragHoverItemMarker != null;
			if (flag2)
			{
				this.m_DragHoverItemMarker.style.visibility = Visibility.Hidden;
			}
			bool flag3 = this.m_DragHoverSiblingMarker != null;
			if (flag3)
			{
				this.m_DragHoverSiblingMarker.style.visibility = Visibility.Hidden;
			}
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00039F20 File Offset: 0x00038120
		protected ReusableCollectionItem GetRecycledItem(Vector3 pointerPosition)
		{
			foreach (ReusableCollectionItem recycledItem in this.targetView.activeItems)
			{
				bool flag = recycledItem.rootElement.worldBound.Contains(pointerPosition);
				if (flag)
				{
					return recycledItem;
				}
			}
			return null;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00039F94 File Offset: 0x00038194
		private bool IsDraggingDisabled()
		{
			return this.targetView == base.dragAndDrop.data.source && !this.enabled;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00039FCC File Offset: 0x000381CC
		[CompilerGenerated]
		private void <ApplyDragAndDropUI>g__GeometryChangedCallback|31_0(GeometryChangedEvent e)
		{
			this.m_DragHoverBar.style.width = this.targetView.localBound.width;
		}

		// Token: 0x04000797 RID: 1943
		private ListViewDragger.DragPosition m_LastDragPosition;

		// Token: 0x04000798 RID: 1944
		private VisualElement m_DragHoverBar;

		// Token: 0x04000799 RID: 1945
		private VisualElement m_DragHoverItemMarker;

		// Token: 0x0400079A RID: 1946
		private VisualElement m_DragHoverSiblingMarker;

		// Token: 0x0400079B RID: 1947
		private float m_LeftIndentation = -1f;

		// Token: 0x0400079C RID: 1948
		private float m_SiblingBottom = -1f;

		// Token: 0x0400079D RID: 1949
		private bool m_Enabled = true;

		// Token: 0x0200019C RID: 412
		internal struct DragPosition : IEquatable<ListViewDragger.DragPosition>
		{
			// Token: 0x06000C14 RID: 3092 RVA: 0x0003A004 File Offset: 0x00038204
			public bool Equals(ListViewDragger.DragPosition other)
			{
				return this.insertAtIndex == other.insertAtIndex && this.parentId == other.parentId && this.childIndex == other.childIndex && object.Equals(this.recycledItem, other.recycledItem) && this.dropPosition == other.dropPosition;
			}

			// Token: 0x06000C15 RID: 3093 RVA: 0x0003A064 File Offset: 0x00038264
			public override bool Equals(object obj)
			{
				bool flag;
				if (obj is ListViewDragger.DragPosition)
				{
					ListViewDragger.DragPosition position = (ListViewDragger.DragPosition)obj;
					flag = this.Equals(position);
				}
				else
				{
					flag = false;
				}
				return flag;
			}

			// Token: 0x06000C16 RID: 3094 RVA: 0x0003A090 File Offset: 0x00038290
			public override int GetHashCode()
			{
				int hashCode = this.insertAtIndex;
				hashCode = (hashCode * 397) ^ this.parentId;
				hashCode = (hashCode * 397) ^ this.childIndex;
				int num = hashCode * 397;
				ReusableCollectionItem reusableCollectionItem = this.recycledItem;
				hashCode = num ^ ((reusableCollectionItem != null) ? reusableCollectionItem.GetHashCode() : 0);
				return (hashCode * 397) ^ (int)this.dropPosition;
			}

			// Token: 0x0400079F RID: 1951
			public int insertAtIndex;

			// Token: 0x040007A0 RID: 1952
			public int parentId;

			// Token: 0x040007A1 RID: 1953
			public int childIndex;

			// Token: 0x040007A2 RID: 1954
			public ReusableCollectionItem recycledItem;

			// Token: 0x040007A3 RID: 1955
			public DragAndDropPosition dropPosition;
		}
	}
}
