using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x020001A0 RID: 416
	internal class TreeViewReorderableDragAndDropController : BaseReorderableDragAndDropController
	{
		// Token: 0x06000C27 RID: 3111 RVA: 0x0003ABAA File Offset: 0x00038DAA
		public TreeViewReorderableDragAndDropController(BaseTreeView view)
			: base(view)
		{
			this.m_TreeView = view;
			this.m_ExpandDropItemCallback = new Action(this.ExpandDropItem);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0003ABDC File Offset: 0x00038DDC
		protected override int CompareId(int id1, int id2)
		{
			bool flag = id1 == id2;
			int num;
			if (flag)
			{
				num = id1.CompareTo(id2);
			}
			else
			{
				int parentId = id1;
				int parentId2 = id2;
				List<int> parentList;
				using (CollectionPool<List<int>, int>.Get(out parentList))
				{
					while (parentId != BaseTreeView.invalidId)
					{
						parentList.Add(parentId);
						parentId = this.m_TreeView.viewController.GetParentId(parentId);
					}
					List<int> parentList2;
					using (CollectionPool<List<int>, int>.Get(out parentList2))
					{
						while (parentId2 != BaseTreeView.invalidId)
						{
							parentList2.Add(parentId2);
							parentId2 = this.m_TreeView.viewController.GetParentId(parentId2);
						}
						parentList.Add(BaseTreeView.invalidId);
						parentList2.Add(BaseTreeView.invalidId);
						int i = 0;
						while (i < parentList.Count)
						{
							int parentId3 = parentList[i];
							int index2 = parentList2.IndexOf(parentId3);
							bool flag2 = index2 >= 0;
							if (flag2)
							{
								bool flag3 = i == 0;
								if (flag3)
								{
									return -1;
								}
								int previousId = ((i > 0) ? parentList[i - 1] : id1);
								int previousId2 = ((index2 > 0) ? parentList2[index2 - 1] : id2);
								int childIndex = this.m_TreeView.viewController.GetChildIndexForId(previousId);
								int childIndex2 = this.m_TreeView.viewController.GetChildIndexForId(previousId2);
								return childIndex.CompareTo(childIndex2);
							}
							else
							{
								i++;
							}
						}
						throw new ArgumentOutOfRangeException("[UI Toolkit] Trying to reorder ids that are not in the same tree.");
					}
				}
			}
			return num;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0003ADA8 File Offset: 0x00038FA8
		public override StartDragArgs SetupDragAndDrop(IEnumerable<int> itemIds, bool skipText = false)
		{
			StartDragArgs startDragArgs = base.SetupDragAndDrop(itemIds, skipText);
			this.m_DropData.draggedIds = base.GetSortedSelectedIds().ToArray<int>();
			return startDragArgs;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0003ADDC File Offset: 0x00038FDC
		public override DragVisualMode HandleDragAndDrop(IListDragAndDropArgs args)
		{
			return (args.dragAndDropData.source == this.m_TreeView) ? DragVisualMode.Move : DragVisualMode.Rejected;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0003AE08 File Offset: 0x00039008
		public override void OnDrop(IListDragAndDropArgs args)
		{
			int insertAtParentId = args.parentId;
			int insertAtChildIndex = args.childIndex;
			int insertIndexShift = 0;
			bool insertLast = args.dragAndDropPosition == DragAndDropPosition.OverItem || (insertAtParentId == -1 && insertAtChildIndex == -1);
			List<ValueTuple<int, int>> previousStates;
			using (CollectionPool<List<ValueTuple<int, int>>, ValueTuple<int, int>>.Get(out previousStates))
			{
				foreach (int id in this.m_DropData.draggedIds)
				{
					int parentId = this.m_TreeView.viewController.GetParentId(id);
					int childIndex = this.m_TreeView.viewController.GetChildIndexForId(id);
					previousStates.Add(new ValueTuple<int, int>(parentId, childIndex));
					bool flag = insertLast;
					if (flag)
					{
						this.m_TreeView.viewController.Move(id, insertAtParentId, -1, false);
					}
					else
					{
						int newChildIndex = insertAtChildIndex + insertIndexShift;
						bool flag2 = parentId != insertAtParentId || childIndex >= insertAtChildIndex;
						if (flag2)
						{
							insertIndexShift++;
						}
						this.m_TreeView.viewController.Move(id, insertAtParentId, newChildIndex, false);
					}
				}
				bool flag3 = args.dragAndDropPosition == DragAndDropPosition.OverItem;
				if (flag3)
				{
					this.m_TreeView.viewController.ExpandItem(insertAtParentId, false, false);
				}
				IVisualElementScheduledItem expandDropItemScheduledItem = this.m_ExpandDropItemScheduledItem;
				if (expandDropItemScheduledItem != null)
				{
					expandDropItemScheduledItem.Pause();
				}
				this.m_TreeView.RefreshItems();
				for (int i = 0; i < this.m_DropData.draggedIds.Length; i++)
				{
					int id2 = this.m_DropData.draggedIds[i];
					ValueTuple<int, int> previous = previousStates[i];
					int newParentId = this.m_TreeView.viewController.GetParentId(id2);
					int newChildIndex2 = this.m_TreeView.viewController.GetChildIndexForId(id2);
					bool flag4 = previous.Item1 == newParentId && previous.Item2 == newChildIndex2;
					if (!flag4)
					{
						this.m_TreeView.viewController.RaiseItemParentChanged(id2, insertAtParentId);
					}
				}
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x0003B01C File Offset: 0x0003921C
		public override void DragCleanup()
		{
			bool flag = this.m_DropData != null;
			if (flag)
			{
				bool flag2 = this.m_DropData.expandedIdsBeforeDrag != null;
				if (flag2)
				{
					this.RestoreExpanded(new List<int>(this.m_DropData.expandedIdsBeforeDrag));
				}
				this.m_DropData = new TreeViewReorderableDragAndDropController.DropData();
			}
			IVisualElementScheduledItem expandDropItemScheduledItem = this.m_ExpandDropItemScheduledItem;
			if (expandDropItemScheduledItem != null)
			{
				expandDropItemScheduledItem.Pause();
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0003B084 File Offset: 0x00039284
		private void RestoreExpanded(List<int> ids)
		{
			foreach (int itemId in this.m_TreeView.viewController.GetAllItemIds(null))
			{
				bool flag = !ids.Contains(itemId);
				if (flag)
				{
					this.m_TreeView.CollapseItem(itemId, false, true);
				}
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x0003B0FC File Offset: 0x000392FC
		public override void HandleAutoExpand(ReusableCollectionItem item, Vector2 pointerPosition)
		{
			int itemId = item.id;
			Rect targetItemRect = item.bindableElement.worldBound;
			Rect indentedContentRect = new Rect(targetItemRect.x, targetItemRect.y + 4f, targetItemRect.width, targetItemRect.height - 8f);
			bool hoveringOverIndentedContent = indentedContentRect.Contains(pointerPosition);
			Vector2 deltaPosition = this.m_DropData.expandItemBeginPosition - pointerPosition;
			bool flag = itemId != this.m_DropData.lastItemId || !hoveringOverIndentedContent || deltaPosition.sqrMagnitude >= 100f;
			if (flag)
			{
				this.m_DropData.lastItemId = itemId;
				this.m_DropData.expandItemBeginTimerMs = (float)Panel.TimeSinceStartupMs();
				this.m_DropData.expandItemBeginPosition = pointerPosition;
				this.DelayExpandDropItem();
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x0003B1C8 File Offset: 0x000393C8
		private void DelayExpandDropItem()
		{
			bool flag = this.m_ExpandDropItemScheduledItem == null;
			if (flag)
			{
				this.m_ExpandDropItemScheduledItem = this.m_TreeView.schedule.Execute(this.m_ExpandDropItemCallback).Every(10L);
			}
			else
			{
				this.m_ExpandDropItemScheduledItem.Pause();
				this.m_ExpandDropItemScheduledItem.Resume();
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0003B228 File Offset: 0x00039428
		private void ExpandDropItem()
		{
			bool expandTimerExpired = (float)Panel.TimeSinceStartupMs() - this.m_DropData.expandItemBeginTimerMs > 700f;
			bool mayExpand = expandTimerExpired;
			int itemId = this.m_DropData.lastItemId;
			bool flag = this.m_TreeView.viewController.Exists(itemId) && mayExpand;
			if (flag)
			{
				bool hasChildren = this.m_TreeView.viewController.HasChildren(itemId);
				bool isExpanded = this.m_TreeView.IsExpanded(itemId);
				bool flag2 = !hasChildren || isExpanded;
				if (!flag2)
				{
					List<int> list = CollectionPool<List<int>, int>.Get();
					this.m_TreeView.viewController.GetExpandedItemIds(list);
					TreeViewReorderableDragAndDropController.DropData dropData = this.m_DropData;
					if (dropData.expandedIdsBeforeDrag == null)
					{
						dropData.expandedIdsBeforeDrag = list.ToArray();
					}
					this.m_DropData.expandItemBeginTimerMs = (float)Panel.TimeSinceStartupMs();
					this.m_DropData.lastItemId = 0;
					this.m_TreeView.ExpandItem(itemId, false, true);
					CollectionPool<List<int>, int>.Release(list);
				}
			}
		}

		// Token: 0x040007AD RID: 1965
		protected TreeViewReorderableDragAndDropController.DropData m_DropData = new TreeViewReorderableDragAndDropController.DropData();

		// Token: 0x040007AE RID: 1966
		protected readonly BaseTreeView m_TreeView;

		// Token: 0x040007AF RID: 1967
		private IVisualElementScheduledItem m_ExpandDropItemScheduledItem;

		// Token: 0x040007B0 RID: 1968
		private Action m_ExpandDropItemCallback;

		// Token: 0x020001A1 RID: 417
		protected class DropData
		{
			// Token: 0x040007B1 RID: 1969
			public int[] expandedIdsBeforeDrag;

			// Token: 0x040007B2 RID: 1970
			public int[] draggedIds;

			// Token: 0x040007B3 RID: 1971
			public int lastItemId = -1;

			// Token: 0x040007B4 RID: 1972
			public float expandItemBeginTimerMs;

			// Token: 0x040007B5 RID: 1973
			public Vector2 expandItemBeginPosition;
		}
	}
}
