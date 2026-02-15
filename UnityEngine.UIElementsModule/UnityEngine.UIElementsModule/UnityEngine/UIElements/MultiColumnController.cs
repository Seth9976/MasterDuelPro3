using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Pool;
using UnityEngine.UIElements.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x02000112 RID: 274
	public class MultiColumnController : IDisposable
	{
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600088B RID: 2187 RVA: 0x00029148 File Offset: 0x00027348
		// (remove) Token: 0x0600088C RID: 2188 RVA: 0x00029180 File Offset: 0x00027380
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action columnSortingChanged;

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600088D RID: 2189 RVA: 0x000291B8 File Offset: 0x000273B8
		// (remove) Token: 0x0600088E RID: 2190 RVA: 0x000291F0 File Offset: 0x000273F0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ContextualMenuPopulateEvent, Column> headerContextMenuPopulateEvent;

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x00029225 File Offset: 0x00027425
		internal MultiColumnCollectionHeader header
		{
			get
			{
				return this.m_MultiColumnHeader;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0002922D File Offset: 0x0002742D
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x00029235 File Offset: 0x00027435
		internal ColumnSortingMode sortingMode
		{
			get
			{
				return this.m_SortingMode;
			}
			set
			{
				this.m_SortingMode = value;
				this.header.sortingEnabled = this.m_SortingMode > ColumnSortingMode.None;
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00029254 File Offset: 0x00027454
		public MultiColumnController(Columns columns, SortColumnDescriptions sortDescriptions, List<SortColumnDescription> sortedColumns)
		{
			this.m_MultiColumnHeader = new MultiColumnCollectionHeader(columns, sortDescriptions, sortedColumns)
			{
				viewDataKey = MultiColumnController.k_HeaderViewDataKey
			};
			this.m_MultiColumnHeader.columnSortingChanged += this.OnColumnSortingChanged;
			this.m_MultiColumnHeader.contextMenuPopulateEvent += this.OnContextMenuPopulateEvent;
			this.m_MultiColumnHeader.columnResized += this.OnColumnResized;
			this.m_MultiColumnHeader.viewDataRestored += this.OnViewDataRestored;
			this.m_MultiColumnHeader.columns.columnAdded += this.OnColumnAdded;
			this.m_MultiColumnHeader.columns.columnRemoved += this.OnColumnRemoved;
			this.m_MultiColumnHeader.columns.columnReordered += this.OnColumnReordered;
			this.m_MultiColumnHeader.columns.columnChanged += this.OnColumnsChanged;
			this.m_MultiColumnHeader.columns.changed += this.OnColumnChanged;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00029374 File Offset: 0x00027574
		private static void BindCellItem<T>(VisualElement ve, int rowIndex, Column column, T item)
		{
			bool flag = column.bindCell != null;
			if (flag)
			{
				column.bindCell(ve, rowIndex);
			}
			else
			{
				MultiColumnController.DefaultBindCellItem<T>(ve, item);
			}
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x000293AB File Offset: 0x000275AB
		private static void UnbindCellItem(VisualElement ve, int rowIndex, Column column)
		{
			Action<VisualElement, int> unbindCell = column.unbindCell;
			if (unbindCell != null)
			{
				unbindCell(ve, rowIndex);
			}
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000293C4 File Offset: 0x000275C4
		private static VisualElement DefaultMakeCellItem()
		{
			Label label = new Label();
			label.AddToClassList(MultiColumnController.cellLabelUssClassName);
			return label;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000293EC File Offset: 0x000275EC
		private static void DefaultBindCellItem<T>(VisualElement ve, T item)
		{
			Label label = ve as Label;
			bool flag = label != null;
			if (flag)
			{
				label.text = item.ToString();
			}
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00029420 File Offset: 0x00027620
		public VisualElement MakeItem()
		{
			VisualElement container = new VisualElement
			{
				name = MultiColumnController.rowContainerUssClassName
			};
			container.AddToClassList(MultiColumnController.rowContainerUssClassName);
			foreach (Column column in this.m_MultiColumnHeader.columns.visibleList)
			{
				VisualElement cellContainer = new VisualElement();
				cellContainer.AddToClassList(MultiColumnController.cellUssClassName);
				Func<VisualElement> makeCell = column.makeCell;
				VisualElement cellItem = ((makeCell != null) ? makeCell() : null) ?? MultiColumnController.DefaultMakeCellItem();
				cellContainer.SetProperty(MultiColumnController.bindableElementPropertyName, cellItem);
				cellContainer.Add(cellItem);
				container.Add(cellContainer);
			}
			return container;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000294E8 File Offset: 0x000276E8
		public void BindItem<T>(VisualElement element, int index, T item)
		{
			int i = 0;
			foreach (Column column in this.m_MultiColumnHeader.columns.visibleList)
			{
				MultiColumnCollectionHeader.ColumnData columnData;
				bool flag = !this.m_MultiColumnHeader.columnDataMap.TryGetValue(column, out columnData);
				if (!flag)
				{
					VisualElement cellContainer = element[i++];
					VisualElement cellItem = cellContainer.GetProperty(MultiColumnController.bindableElementPropertyName) as VisualElement;
					MultiColumnController.BindCellItem<T>(cellItem, index, column, item);
					cellContainer.style.width = columnData.control.resolvedStyle.width;
					cellContainer.SetProperty(MultiColumnController.k_BoundColumnVePropertyName, column);
				}
			}
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000295C0 File Offset: 0x000277C0
		public void UnbindItem(VisualElement element, int index)
		{
			foreach (VisualElement cellContainer in element.Children())
			{
				Column column = cellContainer.GetProperty(MultiColumnController.k_BoundColumnVePropertyName) as Column;
				bool flag = column == null;
				if (!flag)
				{
					VisualElement cellItem = cellContainer.GetProperty(MultiColumnController.bindableElementPropertyName) as VisualElement;
					MultiColumnController.UnbindCellItem(cellItem, index, column);
				}
			}
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00029644 File Offset: 0x00027844
		public void DestroyItem(VisualElement element)
		{
			foreach (VisualElement cellContainer in element.Children())
			{
				Column column = cellContainer.GetProperty(MultiColumnController.k_BoundColumnVePropertyName) as Column;
				bool flag = column == null;
				if (!flag)
				{
					VisualElement cellItem = cellContainer.GetProperty(MultiColumnController.bindableElementPropertyName) as VisualElement;
					Action<VisualElement> destroyCell = column.destroyCell;
					if (destroyCell != null)
					{
						destroyCell(cellItem);
					}
					cellContainer.ClearProperty(MultiColumnController.k_BoundColumnVePropertyName);
				}
			}
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x000296DC File Offset: 0x000278DC
		public void PrepareView(BaseVerticalCollectionView collectionView)
		{
			bool flag = this.m_View != null;
			if (flag)
			{
				Debug.LogWarning("Trying to initialize multi column view more than once. This shouldn't happen.");
			}
			else
			{
				this.m_View = collectionView;
				this.m_HeaderContainer = new VisualElement
				{
					name = MultiColumnController.headerContainerUssClassName
				};
				this.m_HeaderContainer.AddToClassList(MultiColumnController.headerContainerUssClassName);
				this.m_HeaderContainer.viewDataKey = MultiColumnController.k_HeaderContainerViewDataKey;
				collectionView.scrollView.hierarchy.Insert(0, this.m_HeaderContainer);
				this.m_HeaderContainer.Add(this.m_MultiColumnHeader);
				this.m_View.scrollView.horizontalScroller.valueChanged += this.OnHorizontalScrollerValueChanged;
				this.m_View.scrollView.contentViewport.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnViewportGeometryChanged), TrickleDown.NoTrickleDown);
				this.m_MultiColumnHeader.columnContainer.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnColumnContainerGeometryChanged), TrickleDown.NoTrickleDown);
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x000297D8 File Offset: 0x000279D8
		public void Dispose()
		{
			bool flag = this.m_View != null;
			if (flag)
			{
				this.m_View.scrollView.horizontalScroller.valueChanged -= this.OnHorizontalScrollerValueChanged;
				this.m_View.scrollView.contentViewport.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnViewportGeometryChanged), TrickleDown.NoTrickleDown);
				this.m_View = null;
			}
			this.m_MultiColumnHeader.columnContainer.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnColumnContainerGeometryChanged), TrickleDown.NoTrickleDown);
			this.m_MultiColumnHeader.columnSortingChanged -= this.OnColumnSortingChanged;
			this.m_MultiColumnHeader.contextMenuPopulateEvent -= this.OnContextMenuPopulateEvent;
			this.m_MultiColumnHeader.columnResized -= this.OnColumnResized;
			this.m_MultiColumnHeader.viewDataRestored -= this.OnViewDataRestored;
			this.m_MultiColumnHeader.columns.columnAdded -= this.OnColumnAdded;
			this.m_MultiColumnHeader.columns.columnRemoved -= this.OnColumnRemoved;
			this.m_MultiColumnHeader.columns.columnReordered -= this.OnColumnReordered;
			this.m_MultiColumnHeader.columns.columnChanged -= this.OnColumnsChanged;
			this.m_MultiColumnHeader.columns.changed -= this.OnColumnChanged;
			this.m_MultiColumnHeader.RemoveFromHierarchy();
			this.m_MultiColumnHeader.Dispose();
			this.m_MultiColumnHeader = null;
			this.m_HeaderContainer.RemoveFromHierarchy();
			this.m_HeaderContainer = null;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00029982 File Offset: 0x00027B82
		private void OnHorizontalScrollerValueChanged(float v)
		{
			this.m_MultiColumnHeader.ScrollHorizontally(v);
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00029994 File Offset: 0x00027B94
		private void OnViewportGeometryChanged(GeometryChangedEvent evt)
		{
			float headerPadding = this.m_MultiColumnHeader.resolvedStyle.paddingLeft + this.m_MultiColumnHeader.resolvedStyle.paddingRight;
			this.m_MultiColumnHeader.style.maxWidth = evt.newRect.width - headerPadding;
			this.m_MultiColumnHeader.style.maxWidth = evt.newRect.width - headerPadding;
			this.UpdateContentContainer(this.m_View);
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00029A1D File Offset: 0x00027C1D
		private void OnColumnContainerGeometryChanged(GeometryChangedEvent evt)
		{
			this.UpdateContentContainer(this.m_View);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00029A30 File Offset: 0x00027C30
		private void UpdateContentContainer(BaseVerticalCollectionView collectionView)
		{
			float headerTotalWidth = this.m_MultiColumnHeader.columnContainer.layout.width;
			float targetWidth = Mathf.Max(headerTotalWidth, collectionView.scrollView.contentViewport.resolvedStyle.width);
			collectionView.scrollView.contentContainer.style.width = targetWidth;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00029A90 File Offset: 0x00027C90
		private void OnColumnSortingChanged()
		{
			this.UpdateDragger();
			bool flag = this.sortingMode == ColumnSortingMode.Default;
			if (flag)
			{
				this.m_View.RefreshItems();
			}
			Action action = this.columnSortingChanged;
			if (action != null)
			{
				action();
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00029AD4 File Offset: 0x00027CD4
		internal void UpdateDragger()
		{
			bool flag = this.sortingMode == ColumnSortingMode.None;
			if (flag)
			{
				this.m_View.dragger.enabled = true;
			}
			else
			{
				this.m_View.dragger.enabled = this.header.sortedColumnReadonly.Count == 0;
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00029B28 File Offset: 0x00027D28
		internal void SortIfNeeded()
		{
			this.UpdateDragger();
			bool flag = this.sortingMode == ColumnSortingMode.None || this.sortingMode != ColumnSortingMode.Default || this.m_View.itemsSource == null;
			if (!flag)
			{
				List<int> sortedIndices = this.m_SortedIndices;
				bool wasSorted = sortedIndices != null && sortedIndices.Count > 0;
				bool flag2 = wasSorted;
				if (flag2)
				{
					this.m_View.virtualizationController.UnbindAll();
				}
				List<int> sortedIndices2 = this.m_SortedIndices;
				if (sortedIndices2 != null)
				{
					sortedIndices2.Clear();
				}
				bool flag3 = this.header.sortedColumnReadonly.Count == 0;
				if (!flag3)
				{
					List<int> sortedList;
					using (CollectionPool<List<int>, int>.Get(out sortedList))
					{
						for (int i = 0; i < this.m_View.itemsSource.Count; i++)
						{
							sortedList.Add(i);
						}
						sortedList.Sort(new Comparison<int>(this.CombinedComparison));
						if (this.m_SortedIndices == null)
						{
							this.m_SortedIndices = new List<int>(this.m_View.itemsSource.Count);
						}
						this.m_SortedIndices.AddRange(sortedList);
					}
				}
			}
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00029C68 File Offset: 0x00027E68
		private int CombinedComparison(int a, int b)
		{
			BaseTreeViewController treeViewController = this.m_View.viewController as BaseTreeViewController;
			bool flag = treeViewController != null;
			if (flag)
			{
				int idA = treeViewController.GetIdForIndex(a);
				int idB = treeViewController.GetIdForIndex(b);
				int parentIdA = treeViewController.GetParentId(idA);
				int parentIdB = treeViewController.GetParentId(idB);
				bool flag2 = parentIdA != parentIdB;
				if (flag2)
				{
					int depthA = treeViewController.GetIndentationDepth(idA);
					int depthB = treeViewController.GetIndentationDepth(idB);
					int originalDepthA = depthA;
					int originalDepthB = depthB;
					while (depthA > depthB)
					{
						depthA--;
						idA = parentIdA;
						parentIdA = treeViewController.GetParentId(parentIdA);
					}
					while (depthB > depthA)
					{
						depthB--;
						idB = parentIdB;
						parentIdB = treeViewController.GetParentId(parentIdB);
					}
					while (parentIdA != parentIdB)
					{
						idA = parentIdA;
						idB = parentIdB;
						parentIdA = treeViewController.GetParentId(parentIdA);
						parentIdB = treeViewController.GetParentId(parentIdB);
					}
					bool flag3 = idA == idB;
					if (flag3)
					{
						return originalDepthA.CompareTo(originalDepthB);
					}
					a = treeViewController.GetIndexForId(idA);
					b = treeViewController.GetIndexForId(idB);
				}
			}
			int result = 0;
			foreach (SortColumnDescription sortedColumn in this.header.sortedColumns)
			{
				Comparison<int> comparison = sortedColumn.column.comparison;
				result = ((comparison != null) ? comparison(a, b) : 0);
				bool flag4 = result != 0;
				if (flag4)
				{
					bool flag5 = sortedColumn.direction == SortDirection.Descending;
					if (flag5)
					{
						result = -result;
					}
					break;
				}
			}
			return (result == 0) ? a.CompareTo(b) : result;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00029E28 File Offset: 0x00028028
		internal int GetSortedIndex(int index)
		{
			bool flag = this.m_SortedIndices == null;
			int num;
			if (flag)
			{
				num = index;
			}
			else
			{
				bool flag2 = index < 0 || index >= this.m_SortedIndices.Count;
				if (flag2)
				{
					num = index;
				}
				else
				{
					num = ((this.m_SortedIndices.Count > 0) ? this.m_SortedIndices[index] : index);
				}
			}
			return num;
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00029E88 File Offset: 0x00028088
		private void OnContextMenuPopulateEvent(ContextualMenuPopulateEvent evt, Column column)
		{
			Action<ContextualMenuPopulateEvent, Column> action = this.headerContextMenuPopulateEvent;
			if (action != null)
			{
				action(evt, column);
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00029EA0 File Offset: 0x000280A0
		private void OnColumnResized(int index, float width)
		{
			foreach (ReusableCollectionItem item in this.m_View.activeItems)
			{
				item.bindableElement.ElementAt(index).style.width = width;
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00029F10 File Offset: 0x00028110
		private void OnColumnAdded(Column column, int index)
		{
			this.m_View.Rebuild();
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00029F10 File Offset: 0x00028110
		private void OnColumnRemoved(Column column)
		{
			this.m_View.Rebuild();
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00029F20 File Offset: 0x00028120
		private void OnColumnReordered(Column column, int from, int to)
		{
			bool isApplyingViewState = this.m_MultiColumnHeader.isApplyingViewState;
			if (!isApplyingViewState)
			{
				this.m_View.Rebuild();
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00029F4C File Offset: 0x0002814C
		private void OnColumnsChanged(Column column, ColumnDataType type)
		{
			bool isApplyingViewState = this.m_MultiColumnHeader.isApplyingViewState;
			if (!isApplyingViewState)
			{
				bool flag = type == ColumnDataType.Visibility;
				if (flag)
				{
					this.m_View.ScheduleRebuild();
				}
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00029F80 File Offset: 0x00028180
		private void OnColumnChanged(ColumnsDataType type)
		{
			bool isApplyingViewState = this.m_MultiColumnHeader.isApplyingViewState;
			if (!isApplyingViewState)
			{
				bool flag = type == ColumnsDataType.PrimaryColumn;
				if (flag)
				{
					this.m_View.ScheduleRebuild();
				}
			}
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00029F10 File Offset: 0x00028110
		private void OnViewDataRestored()
		{
			this.m_View.Rebuild();
		}

		// Token: 0x04000563 RID: 1379
		private static readonly PropertyName k_BoundColumnVePropertyName = "__unity-multi-column-bound-column";

		// Token: 0x04000564 RID: 1380
		internal static readonly PropertyName bindableElementPropertyName = "__unity-multi-column-bindable-element";

		// Token: 0x04000565 RID: 1381
		internal static readonly string baseUssClassName = "unity-multi-column-view";

		// Token: 0x04000566 RID: 1382
		private static readonly string k_HeaderContainerViewDataKey = "unity-multi-column-header-container";

		// Token: 0x04000567 RID: 1383
		public static readonly string headerContainerUssClassName = MultiColumnController.baseUssClassName + "__header-container";

		// Token: 0x04000568 RID: 1384
		public static readonly string rowContainerUssClassName = MultiColumnController.baseUssClassName + "__row-container";

		// Token: 0x04000569 RID: 1385
		public static readonly string cellUssClassName = MultiColumnController.baseUssClassName + "__cell";

		// Token: 0x0400056A RID: 1386
		public static readonly string cellLabelUssClassName = MultiColumnController.cellUssClassName + "__label";

		// Token: 0x0400056B RID: 1387
		private static readonly string k_HeaderViewDataKey = "Header";

		// Token: 0x0400056E RID: 1390
		private List<int> m_SortedIndices;

		// Token: 0x0400056F RID: 1391
		private ColumnSortingMode m_SortingMode;

		// Token: 0x04000570 RID: 1392
		private BaseVerticalCollectionView m_View;

		// Token: 0x04000571 RID: 1393
		private VisualElement m_HeaderContainer;

		// Token: 0x04000572 RID: 1394
		private MultiColumnCollectionHeader m_MultiColumnHeader;
	}
}
