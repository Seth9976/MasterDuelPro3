using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x0200005F RID: 95
	internal class DynamicHeightVirtualizationController<T> : VerticalVirtualizationController<T> where T : ReusableCollectionItem, new()
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000EC58 File Offset: 0x0000CE58
		private float defaultExpectedHeight
		{
			get
			{
				bool flag = this.m_MinimumItemHeight > 0f;
				float num;
				if (flag)
				{
					num = this.m_MinimumItemHeight;
				}
				else
				{
					bool flag2 = this.m_CollectionView.m_ItemHeightIsInline && this.m_CollectionView.fixedItemHeight > 0f;
					if (flag2)
					{
						num = this.m_CollectionView.fixedItemHeight;
					}
					else
					{
						num = 22f;
					}
				}
				return num;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000ECBD File Offset: 0x0000CEBD
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000ECCA File Offset: 0x0000CECA
		private float contentPadding
		{
			get
			{
				return base.serializedData.contentPadding;
			}
			set
			{
				this.m_CollectionView.scrollView.contentContainer.style.paddingTop = value;
				base.serializedData.contentPadding = value;
				this.m_CollectionView.SaveViewData();
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000ED06 File Offset: 0x0000CF06
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000ED13 File Offset: 0x0000CF13
		private float contentHeight
		{
			get
			{
				return base.serializedData.contentHeight;
			}
			set
			{
				this.m_CollectionView.scrollView.contentContainer.style.height = value;
				base.serializedData.contentHeight = value;
				this.m_CollectionView.SaveViewData();
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000ED4F File Offset: 0x0000CF4F
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000ED5C File Offset: 0x0000CF5C
		private int anchoredIndex
		{
			get
			{
				return base.serializedData.anchoredItemIndex;
			}
			set
			{
				base.serializedData.anchoredItemIndex = value;
				this.m_CollectionView.SaveViewData();
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000ED77 File Offset: 0x0000CF77
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000ED84 File Offset: 0x0000CF84
		private float anchorOffset
		{
			get
			{
				return base.serializedData.anchorOffset;
			}
			set
			{
				base.serializedData.anchorOffset = value;
				this.m_CollectionView.SaveViewData();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000EDA0 File Offset: 0x0000CFA0
		private float viewportMaxOffset
		{
			get
			{
				return base.serializedData.scrollOffset.y + this.m_ScrollView.contentViewport.layout.height;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
		protected override bool alwaysRebindOnRefresh
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		public DynamicHeightVirtualizationController(BaseVerticalCollectionView collectionView)
			: base(collectionView)
		{
			this.m_FillCallback = new Action(this.Fill);
			this.m_ScrollCallback = new Action(this.OnScrollUpdate);
			this.m_GeometryChangedCallback = new Action<ReusableCollectionItem>(this.OnRecycledItemGeometryChanged);
			this.m_IndexOutOfBoundsPredicate = new Predicate<int>(this.IsIndexOutOfBounds);
			this.m_ScrollResetCallback = new Action(this.ResetScroll);
			collectionView.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanelEvent), TrickleDown.NoTrickleDown);
			collectionView.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChangedEvent), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000EEC8 File Offset: 0x0000D0C8
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

		// Token: 0x06000355 RID: 853 RVA: 0x0000EF20 File Offset: 0x0000D120
		public override void Refresh(bool rebuild)
		{
			this.CleanItemHeightCache();
			int previousActiveItemsCount = this.m_ActiveItems.Count;
			bool needsApply = false;
			if (rebuild)
			{
				this.m_WaitingCache.Clear();
			}
			else
			{
				needsApply |= this.m_WaitingCache.RemoveWhere(this.m_IndexOutOfBoundsPredicate) > 0;
			}
			base.Refresh(rebuild);
			this.m_ScrollDirection = DynamicHeightVirtualizationController<T>.ScrollDirection.Idle;
			this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.None;
			bool flag = this.m_CollectionView.HasValidDataAndBindings();
			if (flag)
			{
				bool flag2 = needsApply || previousActiveItemsCount != this.m_ActiveItems.Count;
				if (flag2)
				{
					this.contentHeight = this.GetExpectedContentHeight();
					float scrollableHeight = Mathf.Max(0f, this.contentHeight - this.m_ScrollView.contentViewport.layout.height);
					this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(scrollableHeight);
					this.m_ScrollView.verticalScroller.value = base.serializedData.scrollOffset.y;
					base.serializedData.scrollOffset.y = this.m_ScrollView.verticalScroller.value;
				}
				this.ScheduleFill();
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000F058 File Offset: 0x0000D258
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
					base.ShouldDeferScrollToItem(index);
					float currentContentHeight = this.m_ScrollView.contentContainer.layout.height;
					float viewportHeight = this.m_ScrollView.contentViewport.layout.height;
					bool flag3 = index == -1;
					if (flag3)
					{
						this.m_ForcedLastVisibleItem = base.itemsCount - 1;
						this.m_ForcedFirstVisibleItem = -1;
						this.m_StickToBottom = true;
						this.m_ScrollView.scrollOffset = new Vector2(0f, (viewportHeight >= currentContentHeight) ? 0f : currentContentHeight);
					}
					else
					{
						bool flag4 = this.firstVisibleIndex >= index;
						if (flag4)
						{
							this.m_ForcedFirstVisibleItem = index;
							this.m_ForcedLastVisibleItem = -1;
							this.m_ScrollView.scrollOffset = new Vector2(0f, this.GetContentHeightForIndex(index - 1));
						}
						else
						{
							float itemOffset = this.GetContentHeightForIndex(index);
							bool flag5 = float.IsNaN(viewportHeight) || itemOffset < this.contentPadding + viewportHeight;
							if (!flag5)
							{
								float yScrollOffset = itemOffset - viewportHeight + 22f;
								this.m_ForcedLastVisibleItem = index;
								this.m_ForcedFirstVisibleItem = -1;
								this.m_ScrollView.scrollOffset = new Vector2(0f, yScrollOffset);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000F1BC File Offset: 0x0000D3BC
		public override void Resize(Vector2 size)
		{
			float expectedContentHeight = this.GetExpectedContentHeight();
			this.contentHeight = Mathf.Max(expectedContentHeight, this.contentHeight);
			float viewportHeight = this.m_ScrollView.contentViewport.layout.height;
			float scrollableHeight = Mathf.Max(0f, this.contentHeight - viewportHeight);
			float scrollOffset = Mathf.Min(base.serializedData.scrollOffset.y, scrollableHeight);
			this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(scrollableHeight);
			this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(scrollOffset);
			base.serializedData.scrollOffset.y = this.m_ScrollView.verticalScroller.value;
			float resolvedViewportHeight = this.m_CollectionView.ResolveItemHeight(size.y);
			int itemCountFromHeight = Mathf.CeilToInt(resolvedViewportHeight / this.defaultExpectedHeight);
			int expectedItemCount = itemCountFromHeight;
			bool flag = expectedItemCount <= 0;
			if (!flag)
			{
				expectedItemCount += 2;
				int itemCount = Mathf.Min(expectedItemCount, base.itemsCount);
				bool flag2 = this.m_ActiveItems.Count != itemCount;
				if (flag2)
				{
					int initialItemCount = this.m_ActiveItems.Count;
					bool flag3 = initialItemCount > itemCount;
					if (flag3)
					{
						int removeCount = initialItemCount - itemCount;
						for (int i = 0; i < removeCount; i++)
						{
							int lastIndex = this.m_ActiveItems.Count - 1;
							this.ReleaseItem(lastIndex);
						}
					}
					else
					{
						int addCount = itemCount - this.m_ActiveItems.Count;
						int firstItem = ((this.firstVisibleIndex < 0) ? 0 : this.firstVisibleIndex);
						for (int j = 0; j < addCount; j++)
						{
							int index = j + firstItem + initialItemCount;
							T recycledItem = this.GetOrMakeItemAtIndex(-1, -1);
							bool flag4 = this.IsIndexOutOfBounds(index);
							if (flag4)
							{
								this.HideItem(this.m_ActiveItems.Count - 1);
							}
							else
							{
								base.Setup(recycledItem, index);
								this.MarkWaitingForLayout(recycledItem);
							}
						}
					}
				}
				long currentTimeMs = DateTime.UtcNow.Ticks / 10000L;
				bool flag5 = (float)(currentTimeMs - this.m_TimeSinceFillScheduledMs) > 100f && this.m_TimeSinceFillScheduledMs != 0L && !this.m_FillExecuted;
				if (flag5)
				{
					this.Fill();
					this.ResetScroll();
					this.m_TimeSinceFillScheduledMs = 0L;
				}
				else
				{
					bool flag6 = this.m_TimeSinceFillScheduledMs == 0L;
					if (flag6)
					{
						this.m_TimeSinceFillScheduledMs = DateTime.UtcNow.Ticks / 10000L;
					}
					this.ScheduleFill();
					this.ScheduleScrollDirectionReset();
					this.m_FillExecuted = false;
				}
				this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize;
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000F46C File Offset: 0x0000D66C
		public override void OnScroll(Vector2 scrollOffset)
		{
			bool flag = this.m_DelayedScrollOffset == scrollOffset;
			if (!flag)
			{
				this.m_DelayedScrollOffset = scrollOffset;
				bool flag2 = this.m_ForcedFirstVisibleItem != -1 || this.m_ForcedLastVisibleItem != -1;
				if (flag2)
				{
					this.OnScrollUpdate();
					this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.ForcedScroll;
				}
				else
				{
					DynamicHeightVirtualizationController<T>.VirtualizationChange lastChange = this.m_LastChange;
					bool flag3 = lastChange == DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize || lastChange == DynamicHeightVirtualizationController<T>.VirtualizationChange.ForcedScroll;
					if (flag3)
					{
						float viewportHeight = this.m_ScrollView.contentViewport.layout.height;
						float scrollableHeight = Mathf.Max(0f, this.contentHeight - viewportHeight);
						float offset = Mathf.Min(base.serializedData.scrollOffset.y, scrollableHeight);
						this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(scrollableHeight);
						this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(offset);
						base.serializedData.scrollOffset.y = this.m_ScrollView.verticalScroller.value;
					}
					else
					{
						this.ScheduleScroll();
					}
				}
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000F588 File Offset: 0x0000D788
		private void OnDetachFromPanelEvent(DetachFromPanelEvent evt)
		{
			IVisualElementScheduledItem scheduledItem = this.m_ScheduledItem;
			bool flag = scheduledItem != null && scheduledItem.isActive;
			if (flag)
			{
				this.m_ScheduledItem.Pause();
				this.m_ScheduledItem = null;
			}
			IVisualElementScheduledItem scrollScheduledItem = this.m_ScrollScheduledItem;
			bool flag2 = scrollScheduledItem != null && scrollScheduledItem.isActive;
			if (flag2)
			{
				this.m_ScrollScheduledItem.Pause();
				this.m_ScrollScheduledItem = null;
			}
			IVisualElementScheduledItem scrollResetScheduledItem = this.m_ScrollResetScheduledItem;
			bool flag3 = scrollResetScheduledItem != null && scrollResetScheduledItem.isActive;
			if (flag3)
			{
				this.m_ScrollResetScheduledItem.Pause();
				this.m_ScrollResetScheduledItem = null;
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000F618 File Offset: 0x0000D818
		private void OnScrollUpdate()
		{
			Vector2 scrollOffset = (float.IsNegativeInfinity(this.m_DelayedScrollOffset.y) ? base.serializedData.scrollOffset : this.m_DelayedScrollOffset);
			bool flag = float.IsNaN(this.m_ScrollView.contentViewport.layout.height) || float.IsNaN(scrollOffset.y);
			if (!flag)
			{
				this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.Scroll;
				float expectedContentHeight = this.GetExpectedContentHeight();
				this.contentHeight = Mathf.Max(expectedContentHeight, this.contentHeight);
				this.m_ScrollDirection = ((scrollOffset.y < base.serializedData.scrollOffset.y) ? DynamicHeightVirtualizationController<T>.ScrollDirection.Up : DynamicHeightVirtualizationController<T>.ScrollDirection.Down);
				float scrollableHeight = Mathf.Max(0f, this.contentHeight - this.m_ScrollView.contentViewport.layout.height);
				bool flag2 = scrollOffset.y <= 0f;
				if (flag2)
				{
					this.m_ForcedFirstVisibleItem = 0;
				}
				this.m_StickToBottom = scrollableHeight > 0f && Math.Abs(scrollOffset.y - this.m_ScrollView.verticalScroller.highValue) < float.Epsilon;
				base.serializedData.scrollOffset = scrollOffset;
				this.m_CollectionView.SaveViewData();
				int firstIndex = ((this.m_ForcedFirstVisibleItem != -1) ? this.m_ForcedFirstVisibleItem : this.GetFirstVisibleItem(base.serializedData.scrollOffset.y));
				float firstVisiblePadding = this.GetContentHeightForIndex(firstIndex - 1);
				this.contentPadding = firstVisiblePadding;
				this.m_ForcedFirstVisibleItem = -1;
				bool flag3 = firstIndex != this.firstVisibleIndex;
				if (flag3)
				{
					this.CycleItems(firstIndex);
				}
				else
				{
					this.Fill();
				}
				this.ScheduleScrollDirectionReset();
				this.m_DelayedScrollOffset = Vector2.negativeInfinity;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000F7DC File Offset: 0x0000D9DC
		private void CycleItems(int firstIndex)
		{
			bool flag = firstIndex == this.firstVisibleIndex;
			if (!flag)
			{
				T currentFirstVisibleItem = base.firstVisibleItem;
				this.contentPadding = this.GetContentHeightForIndex(firstIndex - 1);
				this.firstVisibleIndex = firstIndex;
				bool flag2 = this.m_ActiveItems.Count > 0;
				if (flag2)
				{
					bool flag3 = currentFirstVisibleItem == null || this.m_ActiveItems.Count <= Mathf.Abs(this.firstVisibleIndex - currentFirstVisibleItem.index);
					if (!flag3)
					{
						bool flag4 = this.firstVisibleIndex < currentFirstVisibleItem.index;
						if (flag4)
						{
							int count = currentFirstVisibleItem.index - this.firstVisibleIndex;
							List<T> inserting = this.m_ScrollInsertionList;
							for (int i = 0; i < count; i++)
							{
								List<T> activeItems = this.m_ActiveItems;
								T last = activeItems[activeItems.Count - 1];
								inserting.Insert(0, last);
								this.m_ActiveItems.RemoveAt(this.m_ActiveItems.Count - 1);
								last.rootElement.SendToBack();
							}
							this.m_ActiveItems.InsertRange(0, inserting);
							this.m_ScrollInsertionList.Clear();
						}
						else
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
							this.m_ScrollInsertionList.Clear();
						}
					}
					float itemContentOffset = this.contentPadding;
					for (int j = 0; j < this.m_ActiveItems.Count; j++)
					{
						T recycledItem = this.m_ActiveItems[j];
						int index = this.firstVisibleIndex + j;
						int previousIndex = recycledItem.index;
						bool wasVisible = recycledItem.rootElement.style.display == DisplayStyle.Flex;
						this.m_WaitingCache.Remove(previousIndex);
						bool flag5 = this.IsIndexOutOfBounds(index);
						if (flag5)
						{
							this.HideItem(j);
						}
						else
						{
							base.Setup(recycledItem, index);
							bool isItemOutsideViewport = itemContentOffset > this.viewportMaxOffset;
							bool flag6 = isItemOutsideViewport;
							if (flag6)
							{
								this.HideItem(j);
							}
							else
							{
								bool flag7 = index != previousIndex || !wasVisible;
								if (flag7)
								{
									this.MarkWaitingForLayout(recycledItem);
								}
							}
							itemContentOffset += this.GetExpectedItemHeight(index);
						}
					}
				}
				bool flag8 = this.m_LastChange != DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize;
				if (flag8)
				{
					this.UpdateAnchor();
				}
				this.ScheduleFill();
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		private bool NeedsFill()
		{
			bool flag = this.m_LastChange != DynamicHeightVirtualizationController<T>.VirtualizationChange.None || this.anchoredIndex < 0;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				T t = base.lastVisibleItem;
				int lastItemIndex = ((t != null) ? t.index : (-1));
				float contentOffset = this.contentPadding;
				bool flag3 = contentOffset > base.serializedData.scrollOffset.y;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					for (int i = this.firstVisibleIndex; i < base.itemsCount; i++)
					{
						bool flag4 = contentOffset > this.viewportMaxOffset || (contentOffset == this.viewportMaxOffset && !this.m_StickToBottom);
						if (flag4)
						{
							break;
						}
						contentOffset += this.GetExpectedItemHeight(i);
						bool flag5 = i > lastItemIndex;
						if (flag5)
						{
							return true;
						}
					}
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		private void Fill()
		{
			bool flag = !this.m_CollectionView.HasValidDataAndBindings();
			if (!flag)
			{
				this.m_FillExecuted = true;
				bool flag2 = this.m_ActiveItems.Count == 0;
				if (flag2)
				{
					this.contentHeight = 0f;
					this.contentPadding = 0f;
				}
				else
				{
					bool flag3 = this.anchoredIndex < 0;
					if (!flag3)
					{
						bool flag4 = this.contentPadding > this.contentHeight;
						if (flag4)
						{
							this.OnScrollUpdate();
						}
						else
						{
							float firstVisiblePadding = this.contentPadding;
							float contentOffset = this.contentPadding;
							int activeIndex = 0;
							for (int i = this.firstVisibleIndex; i < base.itemsCount; i++)
							{
								bool flag5 = contentOffset > this.viewportMaxOffset || (contentOffset == this.viewportMaxOffset && !this.m_StickToBottom);
								if (flag5)
								{
									break;
								}
								contentOffset += this.GetExpectedItemHeight(i);
								T item = this.m_ActiveItems[activeIndex++];
								bool flag6 = item.index != i || item.rootElement.style.display == DisplayStyle.None;
								if (flag6)
								{
									base.Setup(item, i);
									this.MarkWaitingForLayout(item);
								}
								bool flag7 = activeIndex >= this.m_ActiveItems.Count;
								if (flag7)
								{
									break;
								}
							}
							bool flag8 = this.firstVisibleIndex > 0 && this.contentPadding > base.serializedData.scrollOffset.y;
							if (flag8)
							{
								List<T> inserting = this.m_ScrollInsertionList;
								for (int j = this.m_ActiveItems.Count - 1; j >= activeIndex; j--)
								{
									bool flag9 = this.firstVisibleIndex == 0;
									if (flag9)
									{
										break;
									}
									T last = this.m_ActiveItems[j];
									inserting.Insert(0, last);
									this.m_ActiveItems.RemoveAt(this.m_ActiveItems.Count - 1);
									last.rootElement.SendToBack();
									int num = this.firstVisibleIndex - 1;
									this.firstVisibleIndex = num;
									int newIndex = num;
									base.Setup(last, newIndex);
									this.MarkWaitingForLayout(last);
									firstVisiblePadding -= this.GetExpectedItemHeight(newIndex);
									bool flag10 = firstVisiblePadding < base.serializedData.scrollOffset.y;
									if (flag10)
									{
										break;
									}
								}
								this.m_ActiveItems.InsertRange(0, inserting);
								this.m_ScrollInsertionList.Clear();
							}
							this.contentPadding = firstVisiblePadding;
							this.contentHeight = this.GetExpectedContentHeight();
							bool flag11 = this.m_LastChange != DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize;
							if (flag11)
							{
								this.UpdateAnchor();
							}
							bool flag12 = this.m_WaitingCache.Count == 0;
							if (flag12)
							{
								this.ResetScroll();
								this.ApplyScrollViewUpdate(true);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000FEA8 File Offset: 0x0000E0A8
		private void UpdateScrollViewContainer(float previousHeight, float newHeight)
		{
			bool stickToBottom = this.m_StickToBottom;
			if (!stickToBottom)
			{
				bool flag = this.m_ForcedLastVisibleItem >= 0;
				if (flag)
				{
					float lastItemHeight = this.GetContentHeightForIndex(this.m_ForcedLastVisibleItem);
					base.serializedData.scrollOffset.y = lastItemHeight + 22f - this.m_ScrollView.contentViewport.layout.height;
				}
				else
				{
					bool flag2 = this.m_ScrollDirection == DynamicHeightVirtualizationController<T>.ScrollDirection.Up;
					if (flag2)
					{
						SerializedVirtualizationData serializedData = base.serializedData;
						serializedData.scrollOffset.y = serializedData.scrollOffset.y + (newHeight - previousHeight);
					}
				}
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000FF40 File Offset: 0x0000E140
		private void ApplyScrollViewUpdate(bool dimensionsOnly = false)
		{
			float previousPadding = this.contentPadding;
			float previousScrollOffset = base.serializedData.scrollOffset.y;
			float itemOffset = previousScrollOffset - previousPadding;
			bool flag = this.anchoredIndex >= 0;
			if (flag)
			{
				bool flag2 = this.firstVisibleIndex != this.anchoredIndex;
				if (flag2)
				{
					this.CycleItems(this.anchoredIndex);
					this.ScheduleFill();
				}
				this.firstVisibleIndex = this.anchoredIndex;
				itemOffset = this.anchorOffset;
			}
			float expectedContentHeight = this.GetExpectedContentHeight();
			this.contentHeight = expectedContentHeight;
			this.contentPadding = this.GetContentHeightForIndex(this.firstVisibleIndex - 1);
			float scrollableHeight = Mathf.Max(0f, this.m_ScrollView.RoundToPanelPixelSize(expectedContentHeight - this.m_ScrollView.contentViewport.layout.height));
			float scrollOffset = Mathf.Min(this.contentPadding + itemOffset, scrollableHeight);
			bool flag3 = this.m_StickToBottom && scrollableHeight > 0f;
			if (flag3)
			{
				scrollOffset = scrollableHeight;
			}
			else
			{
				bool flag4 = this.m_ForcedLastVisibleItem != -1;
				if (flag4)
				{
					float lastItemHeight = this.GetContentHeightForIndex(this.m_ForcedLastVisibleItem);
					float lastItemViewportOffset = lastItemHeight + 22f - this.m_ScrollView.contentViewport.layout.height;
					scrollOffset = lastItemViewportOffset;
				}
			}
			this.m_ScrollView.verticalScroller.slider.SetHighValueWithoutNotify(scrollableHeight);
			this.m_ScrollView.verticalScroller.slider.SetValueWithoutNotify(scrollOffset);
			base.serializedData.scrollOffset.y = this.m_ScrollView.verticalScroller.slider.value;
			bool flag5 = dimensionsOnly || this.m_LastChange == DynamicHeightVirtualizationController<T>.VirtualizationChange.Resize;
			if (flag5)
			{
				this.ScheduleScrollDirectionReset();
			}
			else
			{
				bool flag6 = this.NeedsFill();
				if (flag6)
				{
					this.Fill();
				}
				else
				{
					float itemContentOffset = this.contentPadding;
					int previousFirstVisibleIndex = this.firstVisibleIndex;
					List<T> inserting = this.m_ScrollInsertionList;
					int bumpCount = 0;
					for (int i = 0; i < this.m_ActiveItems.Count; i++)
					{
						T item = this.m_ActiveItems[i];
						int index = item.index;
						bool flag7 = index < 0;
						if (flag7)
						{
							break;
						}
						float itemHeight = this.GetExpectedItemHeight(index);
						bool flag8 = this.m_ActiveItems[i].rootElement.style.display == DisplayStyle.Flex;
						if (flag8)
						{
							bool flag9 = itemContentOffset + itemHeight <= base.serializedData.scrollOffset.y;
							if (flag9)
							{
								item.rootElement.BringToFront();
								this.HideItem(i);
								inserting.Add(item);
								bumpCount++;
								int firstVisibleIndex = this.firstVisibleIndex;
								this.firstVisibleIndex = firstVisibleIndex + 1;
							}
							else
							{
								bool flag10 = itemContentOffset > this.viewportMaxOffset;
								if (flag10)
								{
									this.HideItem(i);
								}
							}
						}
						itemContentOffset += this.GetExpectedItemHeight(index);
					}
					this.m_ActiveItems.RemoveRange(0, bumpCount);
					this.m_ActiveItems.AddRange(inserting);
					this.m_ScrollInsertionList.Clear();
					bool flag11 = this.firstVisibleIndex != previousFirstVisibleIndex;
					if (flag11)
					{
						this.contentPadding = this.GetContentHeightForIndex(this.firstVisibleIndex - 1);
						this.UpdateAnchor();
					}
					this.ScheduleScrollDirectionReset();
					this.m_ForcedLastVisibleItem = -1;
					this.m_CollectionView.SaveViewData();
				}
				base.ScheduleDeferredScrollToItem();
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000102D8 File Offset: 0x0000E4D8
		private void UpdateAnchor()
		{
			this.anchoredIndex = this.firstVisibleIndex;
			this.anchorOffset = base.serializedData.scrollOffset.y - this.contentPadding;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00010308 File Offset: 0x0000E508
		private void ScheduleFill()
		{
			bool flag = this.m_ScheduledItem == null;
			if (flag)
			{
				this.m_ScheduledItem = this.m_CollectionView.schedule.Execute(this.m_FillCallback);
			}
			else
			{
				this.m_ScheduledItem.Pause();
				this.m_ScheduledItem.Resume();
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001035C File Offset: 0x0000E55C
		private void ScheduleScroll()
		{
			bool flag = this.m_ScrollScheduledItem == null;
			if (flag)
			{
				this.m_ScrollScheduledItem = this.m_CollectionView.schedule.Execute(this.m_ScrollCallback);
			}
			else
			{
				this.m_ScrollScheduledItem.Pause();
				this.m_ScrollScheduledItem.Resume();
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000103B0 File Offset: 0x0000E5B0
		private void ScheduleScrollDirectionReset()
		{
			bool flag = this.m_ScrollResetScheduledItem == null;
			if (flag)
			{
				this.m_ScrollResetScheduledItem = this.m_CollectionView.schedule.Execute(this.m_ScrollResetCallback);
			}
			else
			{
				this.m_ScrollResetScheduledItem.Pause();
				this.m_ScrollResetScheduledItem.Resume();
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00010402 File Offset: 0x0000E602
		private void ResetScroll()
		{
			this.m_ScrollDirection = DynamicHeightVirtualizationController<T>.ScrollDirection.Idle;
			this.m_LastChange = DynamicHeightVirtualizationController<T>.VirtualizationChange.None;
			this.m_ScrollView.UpdateContentViewTransform();
			this.UpdateAnchor();
			this.m_CollectionView.SaveViewData();
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00010434 File Offset: 0x0000E634
		public override int GetIndexFromPosition(Vector2 position)
		{
			int index = 0;
			for (float traversedHeight = 0f; traversedHeight < position.y; traversedHeight += this.GetExpectedItemHeight(index++))
			{
			}
			return index - 1;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00010470 File Offset: 0x0000E670
		public override float GetExpectedItemHeight(int index)
		{
			int draggedIndex = base.GetDraggedIndex();
			bool flag = draggedIndex >= 0 && index == draggedIndex;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				float height;
				num = (this.m_ItemHeightCache.TryGetValue(index, out height) ? height : this.defaultExpectedHeight);
			}
			return num;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000104BC File Offset: 0x0000E6BC
		private int GetFirstVisibleItem(float offset)
		{
			bool flag = offset <= 0f;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				int index = -1;
				while (offset > 0f)
				{
					index++;
					float height = this.GetExpectedItemHeight(index);
					offset -= height;
				}
				num = index;
			}
			return num;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00010508 File Offset: 0x0000E708
		public override float GetExpectedContentHeight()
		{
			return this.m_AccumulatedHeight + (float)(base.itemsCount - this.m_ItemHeightCache.Count) * this.defaultExpectedHeight;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001053C File Offset: 0x0000E73C
		private float GetContentHeightForIndex(int lastIndex)
		{
			DynamicHeightVirtualizationController<T>.<>c__DisplayClass69_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			bool flag = lastIndex < 0;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				CS$<>8__locals1.draggedIndex = base.GetDraggedIndex();
				DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo highestHeightInfo;
				bool flag2 = this.m_HighestCachedIndex <= lastIndex && this.m_ContentHeightCache.TryGetValue(this.m_HighestCachedIndex, out highestHeightInfo);
				if (flag2)
				{
					num = this.<GetContentHeightForIndex>g__GetContentHeightFromCachedHeight|69_0(lastIndex, in highestHeightInfo, ref CS$<>8__locals1);
				}
				else
				{
					float totalHeight = 0f;
					for (int i = lastIndex; i >= 0; i--)
					{
						DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo heightInfo;
						bool flag3 = this.m_ContentHeightCache.TryGetValue(i, out heightInfo);
						if (flag3)
						{
							return totalHeight + this.<GetContentHeightForIndex>g__GetContentHeightFromCachedHeight|69_0(i, in heightInfo, ref CS$<>8__locals1);
						}
						totalHeight += ((CS$<>8__locals1.draggedIndex == i) ? 0f : this.defaultExpectedHeight);
					}
					num = totalHeight;
				}
			}
			return num;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00010614 File Offset: 0x0000E814
		private DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo GetCachedContentHeight(int index)
		{
			while (index >= 0)
			{
				DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo content;
				bool flag = this.m_ContentHeightCache.TryGetValue(index, out content);
				if (flag)
				{
					return content;
				}
				index--;
			}
			return default(DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0001065C File Offset: 0x0000E85C
		private void RegisterItemHeight(int index, float height)
		{
			bool flag = height <= 0f;
			if (!flag)
			{
				float resolvedHeight = this.m_CollectionView.ResolveItemHeight(height);
				float value;
				bool flag2 = this.m_ItemHeightCache.TryGetValue(index, out value);
				if (flag2)
				{
					this.m_AccumulatedHeight -= value;
				}
				this.m_AccumulatedHeight += resolvedHeight;
				this.m_ItemHeightCache[index] = resolvedHeight;
				bool flag3 = index > this.m_HighestCachedIndex;
				if (flag3)
				{
					this.m_HighestCachedIndex = index;
				}
				bool isNew = value == 0f;
				DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo cached = this.GetCachedContentHeight(index - 1);
				this.m_ContentHeightCache[index] = new DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo(cached.sum + resolvedHeight, cached.count + 1);
				foreach (KeyValuePair<int, float> kvp in this.m_ItemHeightCache)
				{
					bool flag4 = kvp.Key > index;
					if (flag4)
					{
						DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo content = this.m_ContentHeightCache[kvp.Key];
						this.m_ContentHeightCache[kvp.Key] = new DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo(content.sum - value + resolvedHeight, isNew ? (content.count + 1) : content.count);
					}
				}
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x000107BC File Offset: 0x0000E9BC
		private void UnregisterItemHeight(int index)
		{
			float value;
			bool flag = !this.m_ItemHeightCache.TryGetValue(index, out value);
			if (!flag)
			{
				this.m_AccumulatedHeight -= value;
				this.m_ItemHeightCache.Remove(index);
				this.m_ContentHeightCache.Remove(index);
				int highestIndex = -1;
				foreach (KeyValuePair<int, float> kvp in this.m_ItemHeightCache)
				{
					bool flag2 = kvp.Key > index;
					if (flag2)
					{
						DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo content = this.m_ContentHeightCache[kvp.Key];
						this.m_ContentHeightCache[kvp.Key] = new DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo(content.sum - value, content.count - 1);
					}
					bool flag3 = kvp.Key > highestIndex;
					if (flag3)
					{
						highestIndex = kvp.Key;
					}
				}
				this.m_HighestCachedIndex = highestIndex;
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000108C4 File Offset: 0x0000EAC4
		private void CleanItemHeightCache()
		{
			bool flag = !this.IsIndexOutOfBounds(this.m_HighestCachedIndex);
			if (!flag)
			{
				List<int> unregisterList = CollectionPool<List<int>, int>.Get();
				try
				{
					foreach (int index in this.m_ItemHeightCache.Keys)
					{
						bool flag2 = this.IsIndexOutOfBounds(index);
						if (flag2)
						{
							unregisterList.Add(index);
						}
					}
					foreach (int index2 in unregisterList)
					{
						this.UnregisterItemHeight(index2);
					}
				}
				finally
				{
					CollectionPool<List<int>, int>.Release(unregisterList);
				}
				this.m_MinimumItemHeight = -1f;
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000109BC File Offset: 0x0000EBBC
		private void OnRecycledItemGeometryChanged(ReusableCollectionItem item)
		{
			bool flag = item.index == -1 || item.isDragGhost || float.IsNaN(item.rootElement.layout.height) || item.rootElement.layout.height == 0f;
			if (!flag)
			{
				bool flag2 = this.UpdateRegisteredHeight(item);
				if (flag2)
				{
					this.ApplyScrollViewUpdate(false);
				}
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00010A30 File Offset: 0x0000EC30
		private bool UpdateRegisteredHeight(ReusableCollectionItem item)
		{
			bool flag = item.index == -1 || item.isDragGhost || float.IsNaN(item.rootElement.layout.height) || item.rootElement.layout.height == 0f;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = item.rootElement.layout.height < this.defaultExpectedHeight;
				if (flag3)
				{
					this.m_MinimumItemHeight = item.rootElement.layout.height;
					this.Resize(this.m_ScrollView.layout.size);
				}
				float targetHeight = item.rootElement.layout.height - item.rootElement.resolvedStyle.paddingTop;
				float height;
				bool wasCached = this.m_ItemHeightCache.TryGetValue(item.index, out height);
				float previousHeight = (wasCached ? this.GetExpectedItemHeight(item.index) : this.defaultExpectedHeight);
				bool flag4 = this.m_WaitingCache.Count == 0;
				if (flag4)
				{
					bool flag5 = targetHeight > previousHeight;
					if (flag5)
					{
						this.m_StickToBottom = false;
					}
					else
					{
						float deltaHeight = targetHeight - previousHeight;
						float scrollableHeight = Mathf.Max(0f, this.contentHeight - this.m_ScrollView.contentViewport.layout.height);
						this.m_StickToBottom = scrollableHeight > 0f && base.serializedData.scrollOffset.y >= this.m_ScrollView.verticalScroller.highValue + deltaHeight;
					}
				}
				bool flag6 = !wasCached || !Mathf.Approximately(targetHeight, height);
				if (flag6)
				{
					this.RegisterItemHeight(item.index, targetHeight);
					this.UpdateScrollViewContainer(previousHeight, targetHeight);
					bool flag7 = this.m_WaitingCache.Count == 0;
					if (flag7)
					{
						return true;
					}
				}
				flag2 = this.m_WaitingCache.Remove(item.index) && this.m_WaitingCache.Count == 0;
			}
			return flag2;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00010C50 File Offset: 0x0000EE50
		internal override T GetOrMakeItemAtIndex(int activeItemIndex = -1, int scrollViewIndex = -1)
		{
			T item = base.GetOrMakeItemAtIndex(activeItemIndex, scrollViewIndex);
			item.onGeometryChanged += this.m_GeometryChangedCallback;
			return item;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00010C80 File Offset: 0x0000EE80
		internal override void ReleaseItem(int activeItemsIndex)
		{
			T item = this.m_ActiveItems[activeItemsIndex];
			item.onGeometryChanged -= this.m_GeometryChangedCallback;
			int index = item.index;
			this.UnregisterItemHeight(index);
			base.ReleaseItem(activeItemsIndex);
			this.m_WaitingCache.Remove(index);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00010CD6 File Offset: 0x0000EED6
		internal override void StartDragItem(ReusableCollectionItem item)
		{
			this.m_WaitingCache.Remove(item.index);
			base.StartDragItem(item);
			this.m_DraggedItem.onGeometryChanged -= this.m_GeometryChangedCallback;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00010D0C File Offset: 0x0000EF0C
		internal override void EndDrag(int dropIndex)
		{
			bool draggingDown = this.m_DraggedItem.index < dropIndex;
			int startIndex = this.m_DraggedItem.index;
			int increment = (draggingDown ? 1 : (-1));
			float startItemHeight = this.GetExpectedItemHeight(startIndex);
			for (int i = startIndex; i != dropIndex; i += increment)
			{
				float height = this.GetExpectedItemHeight(i);
				float nextHeight = this.GetExpectedItemHeight(i + increment);
				bool flag = Mathf.Approximately(height, nextHeight);
				if (!flag)
				{
					this.RegisterItemHeight(i, nextHeight);
				}
			}
			this.RegisterItemHeight(draggingDown ? (dropIndex - 1) : dropIndex, startItemHeight);
			bool flag2 = this.firstVisibleIndex > this.m_DraggedItem.index;
			if (flag2)
			{
				this.firstVisibleIndex = this.GetFirstVisibleItem(base.serializedData.scrollOffset.y);
				this.UpdateAnchor();
			}
			this.m_DraggedItem.onGeometryChanged += this.m_GeometryChangedCallback;
			base.EndDrag(dropIndex);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00010E10 File Offset: 0x0000F010
		private void HideItem(int activeItemsIndex)
		{
			T item = this.m_ActiveItems[activeItemsIndex];
			item.rootElement.style.display = DisplayStyle.None;
			this.m_WaitingCache.Remove(item.index);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00010E60 File Offset: 0x0000F060
		private void MarkWaitingForLayout(T item)
		{
			bool isDragGhost = item.isDragGhost;
			if (!isDragGhost)
			{
				this.m_WaitingCache.Add(item.index);
				item.rootElement.lastLayout = Rect.zero;
				item.rootElement.MarkDirtyRepaint();
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00010EBC File Offset: 0x0000F0BC
		private bool IsIndexOutOfBounds(int i)
		{
			return this.m_CollectionView.itemsSource == null || i >= base.itemsCount;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00010EEC File Offset: 0x0000F0EC
		[CompilerGenerated]
		private float <GetContentHeightForIndex>g__GetContentHeightFromCachedHeight|69_0(int index, in DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo heightInfo, ref DynamicHeightVirtualizationController<T>.<>c__DisplayClass69_0 A_3)
		{
			bool flag = A_3.draggedIndex >= 0 && index >= A_3.draggedIndex;
			float num;
			if (flag)
			{
				num = heightInfo.sum + (float)(index - heightInfo.count + 1) * this.defaultExpectedHeight - this.m_DraggedItem.rootElement.layout.height;
			}
			else
			{
				num = heightInfo.sum + (float)(index - heightInfo.count + 1) * this.defaultExpectedHeight;
			}
			return num;
		}

		// Token: 0x040001BA RID: 442
		private int m_HighestCachedIndex = -1;

		// Token: 0x040001BB RID: 443
		private readonly Dictionary<int, float> m_ItemHeightCache = new Dictionary<int, float>(32);

		// Token: 0x040001BC RID: 444
		private readonly Dictionary<int, DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo> m_ContentHeightCache = new Dictionary<int, DynamicHeightVirtualizationController<T>.ContentHeightCacheInfo>(32);

		// Token: 0x040001BD RID: 445
		private readonly HashSet<int> m_WaitingCache = new HashSet<int>(32);

		// Token: 0x040001BE RID: 446
		private int? m_ScrolledToItemIndex;

		// Token: 0x040001BF RID: 447
		private int m_ForcedFirstVisibleItem = -1;

		// Token: 0x040001C0 RID: 448
		private int m_ForcedLastVisibleItem = -1;

		// Token: 0x040001C1 RID: 449
		private bool m_StickToBottom;

		// Token: 0x040001C2 RID: 450
		private DynamicHeightVirtualizationController<T>.VirtualizationChange m_LastChange;

		// Token: 0x040001C3 RID: 451
		private DynamicHeightVirtualizationController<T>.ScrollDirection m_ScrollDirection;

		// Token: 0x040001C4 RID: 452
		private Vector2 m_DelayedScrollOffset = Vector2.negativeInfinity;

		// Token: 0x040001C5 RID: 453
		private float m_AccumulatedHeight;

		// Token: 0x040001C6 RID: 454
		private float m_MinimumItemHeight = -1f;

		// Token: 0x040001C7 RID: 455
		private Action m_FillCallback;

		// Token: 0x040001C8 RID: 456
		private Action m_ScrollCallback;

		// Token: 0x040001C9 RID: 457
		private Action m_ScrollResetCallback;

		// Token: 0x040001CA RID: 458
		private Action<ReusableCollectionItem> m_GeometryChangedCallback;

		// Token: 0x040001CB RID: 459
		private IVisualElementScheduledItem m_ScheduledItem;

		// Token: 0x040001CC RID: 460
		private IVisualElementScheduledItem m_ScrollScheduledItem;

		// Token: 0x040001CD RID: 461
		private IVisualElementScheduledItem m_ScrollResetScheduledItem;

		// Token: 0x040001CE RID: 462
		private Predicate<int> m_IndexOutOfBoundsPredicate;

		// Token: 0x040001CF RID: 463
		private bool m_FillExecuted;

		// Token: 0x040001D0 RID: 464
		private long m_TimeSinceFillScheduledMs;

		// Token: 0x02000060 RID: 96
		private readonly struct ContentHeightCacheInfo
		{
			// Token: 0x06000378 RID: 888 RVA: 0x00010F6E File Offset: 0x0000F16E
			public ContentHeightCacheInfo(float sum, int count)
			{
				this.sum = sum;
				this.count = count;
			}

			// Token: 0x040001D1 RID: 465
			public readonly float sum;

			// Token: 0x040001D2 RID: 466
			public readonly int count;
		}

		// Token: 0x02000061 RID: 97
		private enum VirtualizationChange
		{
			// Token: 0x040001D4 RID: 468
			None,
			// Token: 0x040001D5 RID: 469
			Resize,
			// Token: 0x040001D6 RID: 470
			Scroll,
			// Token: 0x040001D7 RID: 471
			ForcedScroll
		}

		// Token: 0x02000062 RID: 98
		private enum ScrollDirection
		{
			// Token: 0x040001D9 RID: 473
			Idle,
			// Token: 0x040001DA RID: 474
			Up,
			// Token: 0x040001DB RID: 475
			Down
		}
	}
}
