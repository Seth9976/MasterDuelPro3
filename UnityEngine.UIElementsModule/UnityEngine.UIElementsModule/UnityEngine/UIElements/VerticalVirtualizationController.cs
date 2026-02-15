using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x0200006A RID: 106
	internal abstract class VerticalVirtualizationController<T> : CollectionVirtualizationController where T : ReusableCollectionItem, new()
	{
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00012314 File Offset: 0x00010514
		public override IEnumerable<ReusableCollectionItem> activeItems
		{
			get
			{
				return this.m_ActiveItems;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001231C File Offset: 0x0001051C
		internal int itemsCount
		{
			get
			{
				return this.m_CollectionView.itemsSource.Count;
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001232E File Offset: 0x0001052E
		protected virtual bool VisibleItemPredicate(T i)
		{
			return i.rootElement.style.display == DisplayStyle.Flex;
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00012350 File Offset: 0x00010550
		internal T firstVisibleItem
		{
			get
			{
				foreach (T item in this.m_ActiveItems)
				{
					bool flag = this.m_VisibleItemPredicateDelegate(item);
					if (flag)
					{
						return item;
					}
				}
				return default(T);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x000123C4 File Offset: 0x000105C4
		internal T lastVisibleItem
		{
			get
			{
				int end = this.m_ActiveItems.Count;
				while (end > 0)
				{
					T item = this.m_ActiveItems[--end];
					bool flag = this.m_VisibleItemPredicateDelegate(item);
					if (flag)
					{
						return item;
					}
				}
				return default(T);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00012420 File Offset: 0x00010620
		public override int visibleItemCount
		{
			get
			{
				int count = 0;
				foreach (T item in this.m_ActiveItems)
				{
					bool flag = this.m_VisibleItemPredicateDelegate(item);
					if (flag)
					{
						count++;
					}
				}
				return count;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0001248C File Offset: 0x0001068C
		protected SerializedVirtualizationData serializedData
		{
			get
			{
				return this.m_CollectionView.serializedVirtualizationData;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00012499 File Offset: 0x00010699
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x000124D7 File Offset: 0x000106D7
		public override int firstVisibleIndex
		{
			get
			{
				return Mathf.Min(this.serializedData.firstVisibleIndex, (this.m_CollectionView.viewController != null) ? (this.m_CollectionView.viewController.GetItemsCount() - 1) : this.serializedData.firstVisibleIndex);
			}
			protected set
			{
				this.serializedData.firstVisibleIndex = value;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x000124E5 File Offset: 0x000106E5
		protected float lastHeight
		{
			get
			{
				return this.m_CollectionView.lastHeight;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000C45B File Offset: 0x0000A65B
		protected virtual bool alwaysRebindOnRefresh
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000124F4 File Offset: 0x000106F4
		protected VerticalVirtualizationController(BaseVerticalCollectionView collectionView)
			: base(collectionView.scrollView)
		{
			this.m_CollectionView = collectionView;
			this.m_ActiveItems = new List<T>();
			this.m_VisibleItemPredicateDelegate = new Func<T, bool>(this.VisibleItemPredicate);
			this.m_PerformDeferredScrollToItem = new Action(this.PerformDeferredScrollToItem);
			this.m_ScrollView.contentContainer.disableClipping = false;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000125E8 File Offset: 0x000107E8
		public override void Refresh(bool rebuild)
		{
			bool hasValidBindings = this.m_CollectionView.HasValidDataAndBindings();
			BaseVerticalCollectionView collectionView = this.m_CollectionView;
			IList itemsSource = this.m_CollectionView.itemsSource;
			collectionView.m_PreviousRefreshedCount = ((itemsSource != null) ? itemsSource.Count : 0);
			for (int i = 0; i < this.m_ActiveItems.Count; i++)
			{
				int index = this.firstVisibleIndex + i;
				T recycledItem = this.m_ActiveItems[i];
				bool isVisible = recycledItem.rootElement.style.display == DisplayStyle.Flex;
				if (rebuild)
				{
					bool flag = hasValidBindings && recycledItem.index != -1;
					if (flag)
					{
						this.m_CollectionView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
					}
					this.m_Pool.Release(recycledItem);
				}
				else
				{
					bool flag2 = this.m_CollectionView.itemsSource != null && index >= 0 && index < this.itemsCount;
					if (flag2)
					{
						bool flag3 = !hasValidBindings;
						if (!flag3)
						{
							bool flag4 = isVisible || this.alwaysRebindOnRefresh;
							if (flag4)
							{
								bool flag5 = recycledItem.index != -1;
								if (flag5)
								{
									this.m_CollectionView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
								}
								this.Setup(recycledItem, index);
							}
						}
					}
					else
					{
						bool flag6 = isVisible;
						if (flag6)
						{
							this.ReleaseItem(i--);
						}
					}
				}
			}
			if (rebuild)
			{
				this.m_Pool.Clear();
				this.m_ActiveItems.Clear();
				this.m_ScrollView.Clear();
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000127AC File Offset: 0x000109AC
		public override void UnbindAll()
		{
			bool hasValidBindings = this.m_CollectionView.HasValidDataAndBindings();
			bool flag = !hasValidBindings;
			if (!flag)
			{
				foreach (T recycledItem in this.m_ActiveItems)
				{
					this.m_CollectionView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
				}
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00012838 File Offset: 0x00010A38
		protected void Setup(T recycledItem, int newIndex)
		{
			bool wasGhostItem = recycledItem.isDragGhost;
			bool flag = this.GetDraggedIndex() == newIndex;
			if (flag)
			{
				bool flag2 = recycledItem.index != -1;
				if (flag2)
				{
					this.m_CollectionView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
				}
				recycledItem.SetDragGhost(true);
				recycledItem.index = this.m_DraggedItem.index;
				recycledItem.rootElement.style.display = DisplayStyle.Flex;
				this.m_CollectionView.viewController.SetBindingContext(recycledItem, recycledItem.index);
			}
			else
			{
				bool flag3 = wasGhostItem;
				if (flag3)
				{
					recycledItem.SetDragGhost(false);
				}
				bool flag4 = newIndex >= this.itemsCount;
				if (flag4)
				{
					recycledItem.rootElement.style.display = DisplayStyle.None;
					bool flag5 = recycledItem.index >= 0 && recycledItem.index < this.itemsCount;
					if (flag5)
					{
						this.m_CollectionView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
						recycledItem.index = -1;
					}
				}
				else
				{
					recycledItem.rootElement.style.display = DisplayStyle.Flex;
					int newId = this.m_CollectionView.viewController.GetIdForIndex(newIndex);
					bool flag6 = recycledItem.index == newIndex && recycledItem.id == newId;
					if (!flag6)
					{
						bool useAlternateUss = this.m_CollectionView.showAlternatingRowBackgrounds != AlternatingRowBackground.None && newIndex % 2 == 1;
						recycledItem.rootElement.EnableInClassList(BaseVerticalCollectionView.itemAlternativeBackgroundUssClassName, useAlternateUss);
						int previousIndex = recycledItem.index;
						bool flag7 = recycledItem.index != -1;
						if (flag7)
						{
							this.m_CollectionView.viewController.InvokeUnbindItem(recycledItem, recycledItem.index);
						}
						recycledItem.index = newIndex;
						recycledItem.id = newId;
						int indexInParent = newIndex - this.firstVisibleIndex;
						bool flag8 = indexInParent >= this.m_ScrollView.contentContainer.childCount;
						if (flag8)
						{
							recycledItem.rootElement.BringToFront();
						}
						else
						{
							bool flag9 = indexInParent >= 0;
							if (flag9)
							{
								recycledItem.rootElement.PlaceBehind(this.m_ScrollView.contentContainer[indexInParent]);
							}
							else
							{
								recycledItem.rootElement.SendToBack();
							}
						}
						this.m_CollectionView.viewController.InvokeBindItem(recycledItem, newIndex);
						this.HandleFocus(recycledItem, previousIndex);
					}
				}
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00012B40 File Offset: 0x00010D40
		protected bool ShouldDeferScrollToItem(int index)
		{
			bool isDirty = this.m_ScrollView.contentContainer.layoutNode.IsDirty;
			bool flag;
			if (isDirty)
			{
				this.m_DeferredScrollToItemIndex = new int?(index);
				flag = true;
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00012B80 File Offset: 0x00010D80
		protected void ScheduleDeferredScrollToItem()
		{
			bool flag = this.m_DeferredScrollToItemIndex == null;
			if (!flag)
			{
				bool flag2 = this.m_ScheduleDeferredScrollToItem == null;
				if (flag2)
				{
					this.m_ScheduleDeferredScrollToItem = this.m_CollectionView.schedule.Execute(this.m_PerformDeferredScrollToItem);
				}
				else
				{
					this.m_ScheduleDeferredScrollToItem.Pause();
					this.m_ScheduleDeferredScrollToItem.Resume();
				}
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00012BE8 File Offset: 0x00010DE8
		private void PerformDeferredScrollToItem()
		{
			bool flag = this.m_DeferredScrollToItemIndex != null;
			if (flag)
			{
				int index = this.m_DeferredScrollToItemIndex.Value;
				this.m_DeferredScrollToItemIndex = null;
				this.ScrollToItem(index);
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00012C28 File Offset: 0x00010E28
		public override void OnFocusIn(VisualElement leafTarget)
		{
			bool flag = leafTarget == this.m_ScrollView.contentContainer;
			if (!flag)
			{
				this.m_LastFocusedElementTreeChildIndexes.Clear();
				bool flag2 = this.m_ScrollView.contentContainer.FindElementInTree(leafTarget, this.m_LastFocusedElementTreeChildIndexes);
				if (flag2)
				{
					VisualElement recycledElement = this.m_ScrollView.contentContainer[this.m_LastFocusedElementTreeChildIndexes[0]];
					foreach (ReusableCollectionItem recycledItem in this.activeItems)
					{
						bool flag3 = recycledItem.rootElement == recycledElement;
						if (flag3)
						{
							this.m_LastFocusedElementIndex = recycledItem.index;
							break;
						}
					}
					this.m_LastFocusedElementTreeChildIndexes.RemoveAt(0);
				}
				else
				{
					this.m_LastFocusedElementIndex = -1;
				}
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00012D0C File Offset: 0x00010F0C
		public override void OnFocusOut(VisualElement willFocus)
		{
			bool flag = willFocus == null || willFocus != this.m_ScrollView.contentContainer;
			if (flag)
			{
				this.m_LastFocusedElementTreeChildIndexes.Clear();
				this.m_LastFocusedElementIndex = -1;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00012D4C File Offset: 0x00010F4C
		private void HandleFocus(ReusableCollectionItem recycledItem, int previousIndex)
		{
			bool flag = this.m_LastFocusedElementIndex == -1;
			if (!flag)
			{
				bool flag2 = this.m_LastFocusedElementIndex == recycledItem.index;
				if (flag2)
				{
					VisualElement visualElement = recycledItem.rootElement.ElementAtTreePath(this.m_LastFocusedElementTreeChildIndexes);
					if (visualElement != null)
					{
						visualElement.Focus();
					}
				}
				else
				{
					bool flag3 = this.m_LastFocusedElementIndex != previousIndex;
					if (flag3)
					{
						VisualElement visualElement2 = recycledItem.rootElement.ElementAtTreePath(this.m_LastFocusedElementTreeChildIndexes);
						if (visualElement2 != null)
						{
							visualElement2.Blur();
						}
					}
					else
					{
						this.m_ScrollView.contentContainer.Focus();
					}
				}
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00012DDC File Offset: 0x00010FDC
		public override void UpdateBackground()
		{
			float backgroundFillHeight;
			bool flag = this.m_CollectionView.showAlternatingRowBackgrounds != AlternatingRowBackground.All || (backgroundFillHeight = this.m_ScrollView.contentViewport.resolvedStyle.height - this.GetExpectedContentHeight()) <= 0f;
			if (flag)
			{
				VisualElement emptyRows = this.m_EmptyRows;
				if (emptyRows != null)
				{
					emptyRows.RemoveFromHierarchy();
				}
			}
			else
			{
				bool flag2 = this.lastVisibleItem == null;
				if (!flag2)
				{
					bool flag3 = this.m_EmptyRows == null;
					if (flag3)
					{
						this.m_EmptyRows = new VisualElement
						{
							classList = { BaseVerticalCollectionView.backgroundFillUssClassName }
						};
					}
					bool flag4 = this.m_EmptyRows.parent == null;
					if (flag4)
					{
						this.m_ScrollView.contentViewport.Add(this.m_EmptyRows);
					}
					float pixelAlignedItemHeight = this.GetExpectedItemHeight(-1);
					int itemCount = Mathf.FloorToInt(backgroundFillHeight / pixelAlignedItemHeight) + 1;
					bool flag5 = itemCount > this.m_EmptyRows.childCount;
					if (flag5)
					{
						int itemsToAdd = itemCount - this.m_EmptyRows.childCount;
						for (int i = 0; i < itemsToAdd; i++)
						{
							VisualElement row = new VisualElement();
							row.style.flexShrink = 0f;
							this.m_EmptyRows.Add(row);
						}
					}
					T t = this.lastVisibleItem;
					int index = ((t != null) ? t.index : (-1));
					int emptyRowCount = this.m_EmptyRows.hierarchy.childCount;
					for (int j = 0; j < emptyRowCount; j++)
					{
						VisualElement child = this.m_EmptyRows.hierarchy[j];
						index++;
						child.style.height = pixelAlignedItemHeight;
						child.EnableInClassList(BaseVerticalCollectionView.itemAlternativeBackgroundUssClassName, index % 2 == 1);
					}
				}
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00012FB8 File Offset: 0x000111B8
		internal override void StartDragItem(ReusableCollectionItem item)
		{
			this.m_DraggedItem = item as T;
			int activeIndex = this.m_ActiveItems.IndexOf(this.m_DraggedItem);
			this.m_ActiveItems.RemoveAt(activeIndex);
			T replacementItem = this.GetOrMakeItemAtIndex(activeIndex, activeIndex);
			this.Setup(replacementItem, this.m_DraggedItem.index);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00013018 File Offset: 0x00011218
		internal override void EndDrag(int dropIndex)
		{
			ReusableCollectionItem item = this.m_CollectionView.GetRecycledItemFromIndex(dropIndex);
			int activeItemIndex = ((item != null) ? this.m_ScrollView.IndexOf(item.rootElement) : this.m_ActiveItems.Count);
			this.m_ScrollView.Insert(activeItemIndex, this.m_DraggedItem.rootElement);
			this.m_ActiveItems.Insert(activeItemIndex, this.m_DraggedItem);
			for (int i = 0; i < this.m_ActiveItems.Count; i++)
			{
				T activeItem = this.m_ActiveItems[i];
				bool isDragGhost = activeItem.isDragGhost;
				if (isDragGhost)
				{
					activeItem.index = -1;
					this.ReleaseItem(i);
					i--;
				}
			}
			bool flag = Math.Min(dropIndex, this.itemsCount - 1) != this.m_DraggedItem.index;
			if (flag)
			{
				bool flag2 = this.lastVisibleItem != null;
				if (flag2)
				{
					this.lastVisibleItem.rootElement.style.display = DisplayStyle.None;
				}
				bool flag3 = this.m_DraggedItem.index < dropIndex;
				if (flag3)
				{
					this.m_CollectionView.viewController.InvokeUnbindItem(this.m_DraggedItem, this.m_DraggedItem.index);
					this.m_DraggedItem.index = -1;
				}
				else
				{
					bool flag4 = item != null;
					if (flag4)
					{
						this.m_CollectionView.viewController.InvokeUnbindItem(item, item.index);
						item.index = -1;
					}
				}
			}
			this.m_DraggedItem = default(T);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000131D4 File Offset: 0x000113D4
		internal virtual T GetOrMakeItemAtIndex(int activeItemIndex = -1, int scrollViewIndex = -1)
		{
			T item = this.m_Pool.Get();
			bool flag = item.rootElement == null;
			if (flag)
			{
				this.m_CollectionView.viewController.InvokeMakeItem(item);
				item.onDestroy += this.OnDestroyItem;
			}
			item.PreAttachElement();
			bool flag2 = activeItemIndex == -1;
			if (flag2)
			{
				this.m_ActiveItems.Add(item);
			}
			else
			{
				this.m_ActiveItems.Insert(activeItemIndex, item);
			}
			bool flag3 = scrollViewIndex == -1;
			if (flag3)
			{
				this.m_ScrollView.Add(item.rootElement);
			}
			else
			{
				this.m_ScrollView.Insert(scrollViewIndex, item.rootElement);
			}
			return item;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000132AC File Offset: 0x000114AC
		internal virtual void ReleaseItem(int activeItemsIndex)
		{
			T item = this.m_ActiveItems[activeItemsIndex];
			bool flag = item.index != -1;
			if (flag)
			{
				this.m_CollectionView.viewController.InvokeUnbindItem(item, item.index);
			}
			this.m_Pool.Release(item);
			this.m_ActiveItems.Remove(item);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001331A File Offset: 0x0001151A
		private void OnDestroyItem(ReusableCollectionItem item)
		{
			this.m_CollectionView.viewController.InvokeDestroyItem(item);
			item.onDestroy -= this.OnDestroyItem;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00013344 File Offset: 0x00011544
		protected int GetDraggedIndex()
		{
			ListViewDraggerAnimated dragger = this.m_CollectionView.dragger as ListViewDraggerAnimated;
			bool flag = dragger != null && dragger.isDragging;
			int num;
			if (flag)
			{
				num = dragger.draggedItem.index;
			}
			else
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x040001F7 RID: 503
		private readonly ObjectPool<T> m_Pool = new ObjectPool<T>(() => new T(), null, delegate(T i)
		{
			i.DetachElement();
		}, delegate(T i)
		{
			i.DestroyElement();
		}, true, 10, 10000);

		// Token: 0x040001F8 RID: 504
		protected BaseVerticalCollectionView m_CollectionView;

		// Token: 0x040001F9 RID: 505
		protected List<T> m_ActiveItems;

		// Token: 0x040001FA RID: 506
		protected T m_DraggedItem;

		// Token: 0x040001FB RID: 507
		private int? m_DeferredScrollToItemIndex;

		// Token: 0x040001FC RID: 508
		private readonly Action m_PerformDeferredScrollToItem;

		// Token: 0x040001FD RID: 509
		private IVisualElementScheduledItem m_ScheduleDeferredScrollToItem;

		// Token: 0x040001FE RID: 510
		private int m_LastFocusedElementIndex = -1;

		// Token: 0x040001FF RID: 511
		private List<int> m_LastFocusedElementTreeChildIndexes = new List<int>();

		// Token: 0x04000200 RID: 512
		protected readonly Func<T, bool> m_VisibleItemPredicateDelegate;

		// Token: 0x04000201 RID: 513
		protected List<T> m_ScrollInsertionList = new List<T>();

		// Token: 0x04000202 RID: 514
		private VisualElement m_EmptyRows;
	}
}
