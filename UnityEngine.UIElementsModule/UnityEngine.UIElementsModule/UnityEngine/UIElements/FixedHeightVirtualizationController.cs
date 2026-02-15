using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000064 RID: 100
	internal class FixedHeightVirtualizationController<T> : VerticalVirtualizationController<T> where T : ReusableCollectionItem, new()
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00010F7F File Offset: 0x0000F17F
		private float resolvedItemHeight
		{
			get
			{
				return this.m_CollectionView.ResolveItemHeight(-1f);
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00010F94 File Offset: 0x0000F194
		protected override bool VisibleItemPredicate(T i)
		{
			return true;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00010FA7 File Offset: 0x0000F1A7
		public FixedHeightVirtualizationController(BaseVerticalCollectionView collectionView)
			: base(collectionView)
		{
			collectionView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChangedEvent), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		private void OnGeometryChangedEvent(GeometryChangedEvent _)
		{
			bool flag = this.m_ScrolledToItemIndex != null;
			if (flag)
			{
				bool flag2 = base.ShouldDeferScrollToItem(this.m_ScrolledToItemIndex ?? (-1));
				if (flag2)
				{
					base.ScheduleDeferredScrollToItem();
				}
				this.m_ScrolledToItemIndex = null;
			}
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00011020 File Offset: 0x0000F220
		public override int GetIndexFromPosition(Vector2 position)
		{
			return (int)(position.y / this.resolvedItemHeight);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00011040 File Offset: 0x0000F240
		public override float GetExpectedItemHeight(int index)
		{
			return this.resolvedItemHeight;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00011058 File Offset: 0x0000F258
		public override float GetExpectedContentHeight()
		{
			return (float)base.itemsCount * this.resolvedItemHeight;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00011078 File Offset: 0x0000F278
		public override void ScrollToItem(int index)
		{
			bool flag = index < -1;
			if (!flag)
			{
				bool flag2 = this.visibleItemCount == 0;
				if (flag2)
				{
					this.m_ScrolledToItemIndex = new int?(index);
				}
				else
				{
					bool flag3 = base.ShouldDeferScrollToItem(index);
					if (flag3)
					{
						base.ScheduleDeferredScrollToItem();
					}
					float pixelAlignedItemHeight = this.resolvedItemHeight;
					bool flag4 = index == -1;
					if (flag4)
					{
						int actualCount = (int)(base.lastHeight / pixelAlignedItemHeight);
						bool flag5 = base.itemsCount < actualCount;
						if (flag5)
						{
							this.m_ScrollView.scrollOffset = new Vector2(0f, 0f);
						}
						else
						{
							this.m_ScrollView.scrollOffset = new Vector2(0f, (float)(base.itemsCount + 1) * pixelAlignedItemHeight);
						}
					}
					else
					{
						bool flag6 = this.firstVisibleIndex >= index;
						if (flag6)
						{
							this.m_ScrollView.scrollOffset = Vector2.up * (pixelAlignedItemHeight * (float)index);
						}
						else
						{
							int actualCount2 = (int)(base.lastHeight / pixelAlignedItemHeight);
							bool flag7 = index < this.firstVisibleIndex + actualCount2;
							if (!flag7)
							{
								int d = index - actualCount2 + 1;
								float visibleOffset = pixelAlignedItemHeight - (base.lastHeight - (float)actualCount2 * pixelAlignedItemHeight);
								float yScrollOffset = pixelAlignedItemHeight * (float)d + visibleOffset;
								this.m_ScrollView.scrollOffset = new Vector2(this.m_ScrollView.scrollOffset.x, yScrollOffset);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000111CC File Offset: 0x0000F3CC
		public override void Resize(Vector2 size)
		{
			float pixelAlignedItemHeight = this.resolvedItemHeight;
			float contentHeight = this.GetExpectedContentHeight();
			this.m_ScrollView.contentContainer.style.height = contentHeight;
			float scrollableHeight = Mathf.Max(0f, contentHeight - this.m_ScrollView.contentViewport.layout.height);
			float scrollOffset = Mathf.Min(base.serializedData.scrollOffset.y, scrollableHeight);
			this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(scrollableHeight);
			this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(scrollOffset);
			int itemCountFromHeight = (int)(this.m_CollectionView.ResolveItemHeight(size.y) / pixelAlignedItemHeight);
			bool flag = itemCountFromHeight > 0;
			if (flag)
			{
				itemCountFromHeight += 2;
			}
			int itemCount = Mathf.Min(itemCountFromHeight, base.itemsCount);
			bool flag2 = this.visibleItemCount != itemCount;
			if (flag2)
			{
				int initialVisibleCount = this.visibleItemCount;
				bool flag3 = this.visibleItemCount > itemCount;
				if (flag3)
				{
					int removeCount = initialVisibleCount - itemCount;
					for (int i = 0; i < removeCount; i++)
					{
						int lastIndex = this.m_ActiveItems.Count - 1;
						this.ReleaseItem(lastIndex);
					}
				}
				else
				{
					int addCount = itemCount - this.visibleItemCount;
					for (int j = 0; j < addCount; j++)
					{
						int index = j + this.firstVisibleIndex + initialVisibleCount;
						T recycledItem = this.GetOrMakeItemAtIndex(-1, -1);
						base.Setup(recycledItem, index);
					}
				}
			}
			this.OnScroll(new Vector2(0f, scrollOffset));
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001136C File Offset: 0x0000F56C
		public override void OnScroll(Vector2 scrollOffset)
		{
			float offset = Mathf.Max(0f, scrollOffset.y);
			float pixelAlignedItemHeight = this.resolvedItemHeight;
			int firstVisibleItemIndex = (int)(offset / pixelAlignedItemHeight);
			this.m_ScrollView.contentContainer.style.paddingTop = (float)firstVisibleItemIndex * pixelAlignedItemHeight;
			this.m_ScrollView.contentContainer.style.height = (float)base.itemsCount * pixelAlignedItemHeight;
			base.serializedData.scrollOffset.y = scrollOffset.y;
			bool flag = firstVisibleItemIndex != this.firstVisibleIndex;
			if (flag)
			{
				this.firstVisibleIndex = firstVisibleItemIndex;
				bool flag2 = this.m_ActiveItems.Count > 0;
				if (flag2)
				{
					bool flag3 = this.firstVisibleIndex < this.m_ActiveItems[0].index;
					if (flag3)
					{
						int count = this.m_ActiveItems[0].index - this.firstVisibleIndex;
						List<T> inserting = this.m_ScrollInsertionList;
						int i = 0;
						while (i < count && this.m_ActiveItems.Count > 0)
						{
							List<T> activeItems = this.m_ActiveItems;
							T last = activeItems[activeItems.Count - 1];
							inserting.Add(last);
							this.m_ActiveItems.RemoveAt(this.m_ActiveItems.Count - 1);
							last.rootElement.SendToBack();
							i++;
						}
						this.m_ActiveItems.InsertRange(0, inserting);
						this.m_ScrollInsertionList.Clear();
					}
					else
					{
						int firstVisibleIndex = this.firstVisibleIndex;
						List<T> activeItems2 = this.m_ActiveItems;
						bool flag4 = firstVisibleIndex < activeItems2[activeItems2.Count - 1].index;
						if (flag4)
						{
							List<T> inserting2 = this.m_ScrollInsertionList;
							int checkIndex = 0;
							while (this.firstVisibleIndex > this.m_ActiveItems[checkIndex].index)
							{
								T first = this.m_ActiveItems[checkIndex];
								inserting2.Add(first);
								checkIndex++;
								first.rootElement.BringToFront();
							}
							this.m_ActiveItems.RemoveRange(0, checkIndex);
							this.m_ActiveItems.AddRange(inserting2);
							inserting2.Clear();
						}
					}
					for (int j = 0; j < this.m_ActiveItems.Count; j++)
					{
						int index = j + this.firstVisibleIndex;
						base.Setup(this.m_ActiveItems[j], index);
					}
				}
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001160C File Offset: 0x0000F80C
		internal override T GetOrMakeItemAtIndex(int activeItemIndex = -1, int scrollViewIndex = -1)
		{
			T item = base.GetOrMakeItemAtIndex(activeItemIndex, scrollViewIndex);
			item.rootElement.style.height = this.resolvedItemHeight;
			return item;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001164C File Offset: 0x0000F84C
		internal override void EndDrag(int dropIndex)
		{
			this.m_DraggedItem.rootElement.style.height = this.resolvedItemHeight;
			bool flag = this.firstVisibleIndex > this.m_DraggedItem.index;
			if (flag)
			{
				this.m_ScrollView.verticalScroller.value = base.serializedData.scrollOffset.y - this.resolvedItemHeight;
			}
			base.EndDrag(dropIndex);
		}

		// Token: 0x040001DE RID: 478
		private int? m_ScrolledToItemIndex;
	}
}
