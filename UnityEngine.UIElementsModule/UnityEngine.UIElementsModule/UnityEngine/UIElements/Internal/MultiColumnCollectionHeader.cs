using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.Pool;

namespace UnityEngine.UIElements.Internal
{
	// Token: 0x020005EB RID: 1515
	internal class MultiColumnCollectionHeader : VisualElement, IDisposable
	{
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x000A975A File Offset: 0x000A795A
		internal bool isApplyingViewState
		{
			get
			{
				return this.m_ApplyingViewState;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002920 RID: 10528 RVA: 0x000A9762 File Offset: 0x000A7962
		public Dictionary<Column, MultiColumnCollectionHeader.ColumnData> columnDataMap { get; } = new Dictionary<Column, MultiColumnCollectionHeader.ColumnData>();

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x000A976A File Offset: 0x000A796A
		public ColumnLayout columnLayout { get; }

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002922 RID: 10530 RVA: 0x000A9772 File Offset: 0x000A7972
		public VisualElement columnContainer { get; }

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x000A977A File Offset: 0x000A797A
		public VisualElement resizeHandleContainer { get; }

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002924 RID: 10532 RVA: 0x000A9782 File Offset: 0x000A7982
		public IEnumerable<SortColumnDescription> sortedColumns
		{
			get
			{
				return this.m_SortedColumns;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x000A9782 File Offset: 0x000A7982
		internal IReadOnlyList<SortColumnDescription> sortedColumnReadonly
		{
			get
			{
				return this.m_SortedColumns;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06002926 RID: 10534 RVA: 0x000A978A File Offset: 0x000A798A
		// (set) Token: 0x06002927 RID: 10535 RVA: 0x000A9792 File Offset: 0x000A7992
		public SortColumnDescriptions sortDescriptions
		{
			get
			{
				return this.m_SortDescriptions;
			}
			protected internal set
			{
				this.m_SortDescriptions = value;
				this.m_SortDescriptions.changed += this.UpdateSortedColumns;
				this.UpdateSortedColumns();
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002928 RID: 10536 RVA: 0x000A97BB File Offset: 0x000A79BB
		public Columns columns { get; }

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002929 RID: 10537 RVA: 0x000A97C3 File Offset: 0x000A79C3
		// (set) Token: 0x0600292A RID: 10538 RVA: 0x000A97CC File Offset: 0x000A79CC
		public bool sortingEnabled
		{
			get
			{
				return this.m_SortingEnabled;
			}
			set
			{
				bool flag = this.m_SortingEnabled == value;
				if (!flag)
				{
					this.m_SortingEnabled = value;
					this.UpdateSortingStatus();
					this.UpdateSortedColumns();
				}
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x0600292B RID: 10539 RVA: 0x000A9800 File Offset: 0x000A7A00
		// (remove) Token: 0x0600292C RID: 10540 RVA: 0x000A9838 File Offset: 0x000A7A38
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<int, float> columnResized;

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x0600292D RID: 10541 RVA: 0x000A9870 File Offset: 0x000A7A70
		// (remove) Token: 0x0600292E RID: 10542 RVA: 0x000A98A8 File Offset: 0x000A7AA8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action columnSortingChanged;

		// Token: 0x14000035 RID: 53
		// (add) Token: 0x0600292F RID: 10543 RVA: 0x000A98E0 File Offset: 0x000A7AE0
		// (remove) Token: 0x06002930 RID: 10544 RVA: 0x000A9918 File Offset: 0x000A7B18
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<ContextualMenuPopulateEvent, Column> contextMenuPopulateEvent;

		// Token: 0x14000036 RID: 54
		// (add) Token: 0x06002931 RID: 10545 RVA: 0x000A9950 File Offset: 0x000A7B50
		// (remove) Token: 0x06002932 RID: 10546 RVA: 0x000A9988 File Offset: 0x000A7B88
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action viewDataRestored;

		// Token: 0x06002933 RID: 10547 RVA: 0x000A99C0 File Offset: 0x000A7BC0
		public MultiColumnCollectionHeader(Columns columns, SortColumnDescriptions sortDescriptions, List<SortColumnDescription> sortedColumns)
		{
			base.AddToClassList(MultiColumnCollectionHeader.ussClassName);
			this.columns = columns;
			this.m_SortedColumns = sortedColumns;
			this.sortDescriptions = sortDescriptions;
			this.columnContainer = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			this.columnContainer.AddToClassList(MultiColumnCollectionHeader.columnContainerUssClassName);
			base.Add(this.columnContainer);
			this.resizeHandleContainer = new VisualElement
			{
				pickingMode = PickingMode.Ignore
			};
			this.resizeHandleContainer.AddToClassList(MultiColumnCollectionHeader.handleContainerUssClassName);
			this.resizeHandleContainer.StretchToParentSize();
			base.Add(this.resizeHandleContainer);
			this.columnLayout = new ColumnLayout(columns);
			this.columnLayout.layoutRequested += this.ScheduleDoLayout;
			foreach (Column column in columns.visibleList)
			{
				this.OnColumnAdded(column);
			}
			this.columns.columnAdded += this.OnColumnAdded;
			this.columns.columnRemoved += this.OnColumnRemoved;
			this.columns.columnChanged += this.OnColumnChanged;
			this.columns.columnReordered += this.OnColumnReordered;
			this.columns.columnResized += this.OnColumnResized;
			this.AddManipulator(new ContextualMenuManipulator(new Action<ContextualMenuPopulateEvent>(this.OnContextualMenuManipulator)));
			base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x000A9B88 File Offset: 0x000A7D88
		private void ScheduleDoLayout()
		{
			bool doLayoutScheduled = this.m_DoLayoutScheduled;
			if (!doLayoutScheduled)
			{
				base.schedule.Execute(new Action(this.DoLayout));
				this.m_DoLayoutScheduled = true;
			}
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x000A9BC4 File Offset: 0x000A7DC4
		private void ResizeToFit()
		{
			this.columnLayout.ResizeToFit(base.layout.width);
		}

		// Token: 0x06002936 RID: 10550 RVA: 0x000A9BEC File Offset: 0x000A7DEC
		private void UpdateSortedColumns()
		{
			bool sortingUpdatesTemporarilyDisabled = this.m_SortingUpdatesTemporarilyDisabled;
			if (!sortingUpdatesTemporarilyDisabled)
			{
				List<MultiColumnCollectionHeader.SortedColumnState> sortedColumnStates;
				using (CollectionPool<List<MultiColumnCollectionHeader.SortedColumnState>, MultiColumnCollectionHeader.SortedColumnState>.Get(out sortedColumnStates))
				{
					bool sortingEnabled = this.sortingEnabled;
					if (sortingEnabled)
					{
						foreach (SortColumnDescription desc in this.sortDescriptions)
						{
							Column column = null;
							bool flag = desc.columnIndex != -1;
							if (flag)
							{
								column = this.columns[desc.columnIndex];
							}
							else
							{
								bool flag2 = !string.IsNullOrEmpty(desc.columnName);
								if (flag2)
								{
									column = this.columns[desc.columnName];
								}
							}
							bool flag3 = column != null && column.sortable;
							if (flag3)
							{
								desc.column = column;
								sortedColumnStates.Add(new MultiColumnCollectionHeader.SortedColumnState(desc, desc.direction));
							}
							else
							{
								desc.column = null;
							}
						}
					}
					bool flag4 = this.m_OldSortedColumnStates.SequenceEqual(sortedColumnStates);
					if (flag4)
					{
						return;
					}
					this.m_SortedColumns.Clear();
					foreach (MultiColumnCollectionHeader.SortedColumnState state in sortedColumnStates)
					{
						this.m_SortedColumns.Add(state.columnDesc);
					}
					this.m_OldSortedColumnStates.CopyFrom(sortedColumnStates);
				}
				this.SaveViewState();
				this.RaiseColumnSortingChanged();
			}
		}

		// Token: 0x06002937 RID: 10551 RVA: 0x000A9DD0 File Offset: 0x000A7FD0
		private void UpdateColumnControls()
		{
			bool hasStretch = false;
			Column lastVisibleColumn = null;
			foreach (Column col in this.columns.visibleList)
			{
				hasStretch |= col.stretchable;
				MultiColumnCollectionHeader.ColumnData columnData = null;
				bool flag = this.columnDataMap.TryGetValue(col, out columnData);
				if (flag)
				{
					columnData.control.style.minWidth = col.minWidth;
					columnData.control.style.maxWidth = col.maxWidth;
					columnData.resizeHandle.style.display = ((this.columns.resizable && col.resizable) ? DisplayStyle.Flex : DisplayStyle.None);
				}
				lastVisibleColumn = col;
			}
			bool flag2 = hasStretch;
			if (flag2)
			{
				this.columnContainer.style.flexGrow = 1f;
				MultiColumnCollectionHeader.ColumnData columnData2;
				bool flag3 = this.columns.stretchMode == Columns.StretchMode.GrowAndFill && this.columnDataMap.TryGetValue(lastVisibleColumn, out columnData2);
				if (flag3)
				{
					columnData2.resizeHandle.style.display = DisplayStyle.None;
				}
			}
			else
			{
				this.columnContainer.style.flexGrow = 0f;
			}
			this.UpdateSortingStatus();
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000A9F44 File Offset: 0x000A8144
		private void OnColumnAdded(Column column, int index)
		{
			this.OnColumnAdded(column);
		}

		// Token: 0x06002939 RID: 10553 RVA: 0x000A9F50 File Offset: 0x000A8150
		private void OnColumnAdded(Column column)
		{
			bool flag = this.columnDataMap.ContainsKey(column);
			if (!flag)
			{
				MultiColumnHeaderColumn columnElement = new MultiColumnHeaderColumn(column);
				MultiColumnHeaderColumnResizeHandle resizeHandle = new MultiColumnHeaderColumnResizeHandle();
				columnElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnColumnControlGeometryChanged), TrickleDown.NoTrickleDown);
				columnElement.clickable.clickedWithEventInfo += this.OnColumnClicked;
				columnElement.mover.activeChanged += this.OnMoveManipulatorActivated;
				resizeHandle.dragArea.AddManipulator(new ColumnResizer(column));
				this.columnDataMap[column] = new MultiColumnCollectionHeader.ColumnData
				{
					control = columnElement,
					resizeHandle = resizeHandle
				};
				bool visible = column.visible;
				if (visible)
				{
					this.columnContainer.Insert(column.visibleIndex, columnElement);
					this.resizeHandleContainer.Insert(column.visibleIndex, resizeHandle);
				}
				else
				{
					this.OnColumnRemoved(column);
				}
				this.UpdateColumnControls();
				this.SaveViewState();
			}
		}

		// Token: 0x0600293A RID: 10554 RVA: 0x000AA044 File Offset: 0x000A8244
		private void OnColumnRemoved(Column column)
		{
			MultiColumnCollectionHeader.ColumnData data;
			bool flag = !this.columnDataMap.TryGetValue(column, out data);
			if (!flag)
			{
				this.CleanupColumnData(data);
				this.columnDataMap.Remove(column);
				this.UpdateColumnControls();
				this.SaveViewState();
			}
		}

		// Token: 0x0600293B RID: 10555 RVA: 0x000AA08C File Offset: 0x000A828C
		private void OnColumnChanged(Column column, ColumnDataType type)
		{
			bool flag = type == ColumnDataType.Visibility;
			if (flag)
			{
				bool visible = column.visible;
				if (visible)
				{
					this.OnColumnAdded(column);
				}
				else
				{
					this.OnColumnRemoved(column);
				}
				this.ApplyColumnSorting();
			}
			this.UpdateColumnControls();
			bool flag2 = type == ColumnDataType.Visibility;
			if (flag2)
			{
				this.SaveViewState();
			}
		}

		// Token: 0x0600293C RID: 10556 RVA: 0x000AA0E0 File Offset: 0x000A82E0
		private void OnColumnReordered(Column column, int from, int to)
		{
			bool flag = !column.visible || from == to;
			if (!flag)
			{
				MultiColumnCollectionHeader.ColumnData columnData;
				bool flag2 = this.columnDataMap.TryGetValue(column, out columnData);
				if (flag2)
				{
					int index = column.visibleIndex;
					bool flag3 = index == this.columns.visibleList.Count<Column>() - 1;
					if (flag3)
					{
						columnData.control.BringToFront();
					}
					else
					{
						bool flag4 = to > from;
						if (flag4)
						{
							index++;
						}
						columnData.control.PlaceBehind(this.columnContainer[index]);
						columnData.resizeHandle.PlaceBehind(this.resizeHandleContainer[index]);
					}
				}
				this.UpdateColumnControls();
				this.SaveViewState();
			}
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x000AA19A File Offset: 0x000A839A
		private void OnColumnResized(Column column)
		{
			this.SaveViewState();
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x000AA1A4 File Offset: 0x000A83A4
		private void OnContextualMenuManipulator(ContextualMenuPopulateEvent evt)
		{
			Column columnUnderMouse = null;
			bool canResizeToFit = this.columns.visibleList.Count<Column>() > 0;
			foreach (Column column2 in this.columns.visibleList)
			{
				bool flag = this.columns.stretchMode == Columns.StretchMode.GrowAndFill && canResizeToFit && column2.stretchable;
				if (flag)
				{
					canResizeToFit = false;
				}
				bool flag2 = columnUnderMouse == null;
				if (flag2)
				{
					MultiColumnCollectionHeader.ColumnData data;
					bool flag3 = this.columnDataMap.TryGetValue(column2, out data);
					if (flag3)
					{
						bool flag4 = data.control.layout.Contains(evt.localMousePosition);
						if (flag4)
						{
							columnUnderMouse = column2;
						}
					}
				}
			}
			evt.menu.AppendAction("Resize To Fit", delegate(DropdownMenuAction a)
			{
				this.ResizeToFit();
			}, canResizeToFit ? DropdownMenuAction.Status.Normal : DropdownMenuAction.Status.Disabled);
			evt.menu.AppendSeparator(null);
			using (IEnumerator<Column> enumerator2 = this.columns.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					Column column = enumerator2.Current;
					string title = column.title;
					bool flag5 = string.IsNullOrEmpty(title);
					if (flag5)
					{
						title = column.name;
					}
					bool flag6 = string.IsNullOrEmpty(title);
					if (flag6)
					{
						title = "Unnamed Column_" + column.index.ToString();
					}
					evt.menu.AppendAction(title, delegate(DropdownMenuAction a)
					{
						column.visible = !column.visible;
					}, delegate(DropdownMenuAction a)
					{
						bool flag7 = !string.IsNullOrEmpty(column.name) && this.columns.primaryColumnName == column.name;
						DropdownMenuAction.Status status;
						if (flag7)
						{
							status = DropdownMenuAction.Status.Disabled;
						}
						else
						{
							bool flag8 = !column.optional;
							if (flag8)
							{
								status = DropdownMenuAction.Status.Disabled;
							}
							else
							{
								bool visible = column.visible;
								if (visible)
								{
									status = DropdownMenuAction.Status.Checked;
								}
								else
								{
									status = DropdownMenuAction.Status.Normal;
								}
							}
						}
						return status;
					}, null);
				}
			}
			Action<ContextualMenuPopulateEvent, Column> action = this.contextMenuPopulateEvent;
			if (action != null)
			{
				action(evt, columnUnderMouse);
			}
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x000AA394 File Offset: 0x000A8594
		private void OnMoveManipulatorActivated(ColumnMover mover)
		{
			this.resizeHandleContainer.style.display = (mover.active ? DisplayStyle.None : DisplayStyle.Flex);
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x000AA3BC File Offset: 0x000A85BC
		private void OnGeometryChanged(GeometryChangedEvent e)
		{
			bool flag = float.IsNaN(e.newRect.width) || float.IsNaN(e.newRect.height);
			if (!flag)
			{
				this.columnLayout.Dirty();
				bool flag2 = e.layoutPass > 2;
				if (flag2)
				{
					this.ScheduleDoLayout();
				}
				else
				{
					this.DoLayout();
				}
			}
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000AA428 File Offset: 0x000A8628
		private void DoLayout()
		{
			this.columnLayout.DoLayout(base.layout.width);
			this.m_DoLayoutScheduled = false;
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x000AA458 File Offset: 0x000A8658
		private void OnColumnControlGeometryChanged(GeometryChangedEvent evt)
		{
			MultiColumnHeaderColumn columnControl = evt.target as MultiColumnHeaderColumn;
			bool flag = columnControl == null;
			if (!flag)
			{
				MultiColumnCollectionHeader.ColumnData controlData = this.columnDataMap[columnControl.column];
				controlData.resizeHandle.style.left = columnControl.layout.xMax;
				bool flag2 = Math.Abs(evt.newRect.width - evt.oldRect.width) < float.Epsilon;
				if (!flag2)
				{
					this.RaiseColumnResized(this.columnContainer.IndexOf(evt.elementTarget));
				}
			}
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000AA500 File Offset: 0x000A8700
		private void OnColumnClicked(EventBase evt)
		{
			bool flag = !this.sortingEnabled;
			if (!flag)
			{
				MultiColumnHeaderColumn columnControl = evt.currentTarget as MultiColumnHeaderColumn;
				bool flag2 = columnControl == null || !columnControl.column.sortable;
				if (!flag2)
				{
					IPointerEvent ptEvt = evt as IPointerEvent;
					bool flag3 = ptEvt != null;
					EventModifiers modifiers;
					if (flag3)
					{
						modifiers = ptEvt.modifiers;
					}
					else
					{
						IMouseEvent msEvt = evt as IMouseEvent;
						bool flag4 = msEvt != null;
						if (!flag4)
						{
							return;
						}
						modifiers = msEvt.modifiers;
					}
					this.m_SortingUpdatesTemporarilyDisabled = true;
					try
					{
						this.UpdateSortColumnDescriptionsOnClick(columnControl.column, modifiers);
					}
					finally
					{
						this.m_SortingUpdatesTemporarilyDisabled = false;
					}
					this.UpdateSortedColumns();
				}
			}
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000AA5BC File Offset: 0x000A87BC
		private void UpdateSortColumnDescriptionsOnClick(Column column, EventModifiers modifiers)
		{
			SortColumnDescription desc = this.sortDescriptions.FirstOrDefault((SortColumnDescription d) => d.column == column || (!string.IsNullOrEmpty(column.name) && d.columnName == column.name) || d.columnIndex == column.index);
			bool flag = desc != null;
			if (flag)
			{
				bool flag2 = modifiers == EventModifiers.Shift;
				if (flag2)
				{
					this.sortDescriptions.Remove(desc);
					return;
				}
				desc.direction = ((desc.direction == SortDirection.Ascending) ? SortDirection.Descending : SortDirection.Ascending);
			}
			else
			{
				desc = (string.IsNullOrEmpty(column.name) ? new SortColumnDescription(column.index, SortDirection.Ascending) : new SortColumnDescription(column.name, SortDirection.Ascending));
			}
			EventModifiers multiSortingModifier = EventModifiers.Control;
			RuntimePlatform platform = Application.platform;
			bool flag3 = platform == RuntimePlatform.OSXEditor || platform == RuntimePlatform.OSXPlayer;
			if (flag3)
			{
				multiSortingModifier = EventModifiers.Command;
			}
			bool flag4 = modifiers != multiSortingModifier;
			if (flag4)
			{
				this.sortDescriptions.Clear();
			}
			bool flag5 = !this.sortDescriptions.Contains(desc);
			if (flag5)
			{
				this.sortDescriptions.Add(desc);
			}
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000AA6C8 File Offset: 0x000A88C8
		public void ScrollHorizontally(float horizontalOffset)
		{
			base.transform.position = new Vector3(-horizontalOffset, base.transform.position.y, base.transform.position.z);
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000AA6FE File Offset: 0x000A88FE
		private void RaiseColumnResized(int columnIndex)
		{
			Action<int, float> action = this.columnResized;
			if (action != null)
			{
				action(columnIndex, this.columnContainer[columnIndex].resolvedStyle.width);
			}
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x000AA72C File Offset: 0x000A892C
		private void RaiseColumnSortingChanged()
		{
			this.ApplyColumnSorting();
			bool flag = !this.m_ApplyingViewState;
			if (flag)
			{
				Action action = this.columnSortingChanged;
				if (action != null)
				{
					action();
				}
			}
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x000AA760 File Offset: 0x000A8960
		private void ApplyColumnSorting()
		{
			foreach (Column column in this.columns.visibleList)
			{
				MultiColumnCollectionHeader.ColumnData columnData;
				bool flag = !this.columnDataMap.TryGetValue(column, out columnData);
				if (!flag)
				{
					columnData.control.sortOrderLabel = "";
					columnData.control.RemoveFromClassList(MultiColumnHeaderColumn.sortedAscendingUssClassName);
					columnData.control.RemoveFromClassList(MultiColumnHeaderColumn.sortedDescendingUssClassName);
				}
			}
			List<MultiColumnCollectionHeader.ColumnData> sortedColumnDataList = new List<MultiColumnCollectionHeader.ColumnData>();
			foreach (SortColumnDescription sortedColumn in this.sortedColumns)
			{
				MultiColumnCollectionHeader.ColumnData columnData2;
				bool flag2 = this.columnDataMap.TryGetValue(sortedColumn.column, out columnData2);
				if (flag2)
				{
					sortedColumnDataList.Add(columnData2);
					bool flag3 = sortedColumn.direction == SortDirection.Ascending;
					if (flag3)
					{
						columnData2.control.AddToClassList(MultiColumnHeaderColumn.sortedAscendingUssClassName);
					}
					else
					{
						columnData2.control.AddToClassList(MultiColumnHeaderColumn.sortedDescendingUssClassName);
					}
				}
			}
			bool flag4 = sortedColumnDataList.Count > 1;
			if (flag4)
			{
				for (int i = 0; i < sortedColumnDataList.Count; i++)
				{
					sortedColumnDataList[i].control.sortOrderLabel = (i + 1).ToString();
				}
			}
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x000AA8EC File Offset: 0x000A8AEC
		private void UpdateSortingStatus()
		{
			bool hasSortableColumns = false;
			foreach (Column column in this.columns.visibleList)
			{
				MultiColumnCollectionHeader.ColumnData columnData;
				bool flag = !this.columnDataMap.TryGetValue(column, out columnData);
				if (!flag)
				{
					bool flag2 = this.sortingEnabled && column.sortable;
					if (flag2)
					{
						hasSortableColumns = true;
					}
				}
			}
			foreach (Column column2 in this.columns.visibleList)
			{
				MultiColumnCollectionHeader.ColumnData columnData2;
				bool flag3 = !this.columnDataMap.TryGetValue(column2, out columnData2);
				if (!flag3)
				{
					bool flag4 = hasSortableColumns;
					if (flag4)
					{
						columnData2.control.AddToClassList(MultiColumnHeaderColumn.sortableUssClassName);
					}
					else
					{
						columnData2.control.RemoveFromClassList(MultiColumnHeaderColumn.sortableUssClassName);
					}
				}
			}
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000AAA04 File Offset: 0x000A8C04
		internal override void OnViewDataReady()
		{
			try
			{
				this.m_ApplyingViewState = true;
				base.OnViewDataReady();
				string key = base.GetFullHierarchicalViewDataKey();
				this.m_ViewState = base.GetOrCreateViewData<MultiColumnCollectionHeader.ViewState>(this.m_ViewState, key);
				this.m_ViewState.Apply(this);
				Action action = this.viewDataRestored;
				if (action != null)
				{
					action();
				}
			}
			finally
			{
				this.m_ApplyingViewState = false;
			}
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x000AAA78 File Offset: 0x000A8C78
		private void SaveViewState()
		{
			bool applyingViewState = this.m_ApplyingViewState;
			if (!applyingViewState)
			{
				MultiColumnCollectionHeader.ViewState viewState = this.m_ViewState;
				if (viewState != null)
				{
					viewState.Save(this);
				}
				base.SaveViewData();
			}
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x000AAAAC File Offset: 0x000A8CAC
		private void CleanupColumnData(MultiColumnCollectionHeader.ColumnData data)
		{
			data.control.UnregisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnColumnControlGeometryChanged), TrickleDown.NoTrickleDown);
			data.control.clickable.clickedWithEventInfo -= this.OnColumnClicked;
			data.control.mover.activeChanged -= this.OnMoveManipulatorActivated;
			data.control.RemoveFromHierarchy();
			data.control.Dispose();
			data.resizeHandle.RemoveFromHierarchy();
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x000AAB34 File Offset: 0x000A8D34
		public void Dispose()
		{
			this.sortDescriptions.changed -= this.UpdateSortedColumns;
			this.columnLayout.layoutRequested -= this.ScheduleDoLayout;
			this.columns.columnAdded -= this.OnColumnAdded;
			this.columns.columnRemoved -= this.OnColumnRemoved;
			this.columns.columnChanged -= this.OnColumnChanged;
			this.columns.columnReordered -= this.OnColumnReordered;
			this.columns.columnResized -= this.OnColumnResized;
			foreach (MultiColumnCollectionHeader.ColumnData data in this.columnDataMap.Values)
			{
				this.CleanupColumnData(data);
			}
			this.columnDataMap.Clear();
		}

		// Token: 0x040015B1 RID: 5553
		public static readonly string ussClassName = "unity-multi-column-header";

		// Token: 0x040015B2 RID: 5554
		public static readonly string columnContainerUssClassName = MultiColumnCollectionHeader.ussClassName + "__column-container";

		// Token: 0x040015B3 RID: 5555
		public static readonly string handleContainerUssClassName = MultiColumnCollectionHeader.ussClassName + "__resize-handle-container";

		// Token: 0x040015B4 RID: 5556
		public static readonly string reorderableUssClassName = MultiColumnCollectionHeader.ussClassName + "__header";

		// Token: 0x040015B5 RID: 5557
		private bool m_SortingEnabled;

		// Token: 0x040015B6 RID: 5558
		private List<SortColumnDescription> m_SortedColumns;

		// Token: 0x040015B7 RID: 5559
		private SortColumnDescriptions m_SortDescriptions;

		// Token: 0x040015B8 RID: 5560
		private List<MultiColumnCollectionHeader.SortedColumnState> m_OldSortedColumnStates = new List<MultiColumnCollectionHeader.SortedColumnState>();

		// Token: 0x040015B9 RID: 5561
		private bool m_SortingUpdatesTemporarilyDisabled;

		// Token: 0x040015BA RID: 5562
		private MultiColumnCollectionHeader.ViewState m_ViewState;

		// Token: 0x040015BB RID: 5563
		private bool m_ApplyingViewState;

		// Token: 0x040015BC RID: 5564
		private bool m_DoLayoutScheduled;

		// Token: 0x020005EC RID: 1516
		[Serializable]
		private class ViewState
		{
			// Token: 0x06002950 RID: 10576 RVA: 0x000AACA4 File Offset: 0x000A8EA4
			internal void Save(MultiColumnCollectionHeader header)
			{
				this.m_SortDescriptions.Clear();
				this.m_OrderedColumnStates.Clear();
				foreach (SortColumnDescription sortDesc in header.sortDescriptions)
				{
					this.m_SortDescriptions.Add(sortDesc);
				}
				foreach (Column column in header.columns.displayList)
				{
					MultiColumnCollectionHeader.ViewState.ColumnState columnState = new MultiColumnCollectionHeader.ViewState.ColumnState
					{
						index = column.index,
						name = column.name,
						actualWidth = column.desiredWidth,
						width = column.width,
						visible = column.visible
					};
					this.m_OrderedColumnStates.Add(columnState);
				}
				this.m_HasPersistedData = true;
			}

			// Token: 0x06002951 RID: 10577 RVA: 0x000AADB4 File Offset: 0x000A8FB4
			internal void Apply(MultiColumnCollectionHeader header)
			{
				bool flag = !this.m_HasPersistedData;
				if (!flag)
				{
					int minCount = Math.Min(this.m_OrderedColumnStates.Count, header.columns.Count);
					int nextValidOrderedIndex = 0;
					int orderedIndex = 0;
					while (orderedIndex < this.m_OrderedColumnStates.Count && nextValidOrderedIndex < minCount)
					{
						MultiColumnCollectionHeader.ViewState.ColumnState columnState = this.m_OrderedColumnStates[orderedIndex];
						Column column = null;
						bool flag2 = !string.IsNullOrEmpty(columnState.name);
						if (flag2)
						{
							bool flag3 = header.columns.Contains(columnState.name);
							if (flag3)
							{
								column = header.columns[columnState.name];
							}
							goto IL_00E2;
						}
						bool flag4 = columnState.index > header.columns.Count - 1;
						if (!flag4)
						{
							column = header.columns[columnState.index];
							bool flag5 = !string.IsNullOrEmpty(column.name);
							if (flag5)
							{
								column = null;
							}
							goto IL_00E2;
						}
						IL_0135:
						orderedIndex++;
						continue;
						IL_00E2:
						bool flag6 = column == null;
						if (flag6)
						{
							goto IL_0135;
						}
						header.columns.ReorderDisplay(column.displayIndex, nextValidOrderedIndex++);
						column.visible = columnState.visible;
						column.width = columnState.width;
						column.desiredWidth = columnState.actualWidth;
						goto IL_0135;
					}
					header.sortDescriptions.Clear();
					foreach (SortColumnDescription sortDesc in this.m_SortDescriptions)
					{
						header.sortDescriptions.Add(sortDesc);
					}
				}
			}

			// Token: 0x040015C6 RID: 5574
			[SerializeField]
			private bool m_HasPersistedData;

			// Token: 0x040015C7 RID: 5575
			[SerializeField]
			private List<SortColumnDescription> m_SortDescriptions = new List<SortColumnDescription>();

			// Token: 0x040015C8 RID: 5576
			[SerializeField]
			private List<MultiColumnCollectionHeader.ViewState.ColumnState> m_OrderedColumnStates = new List<MultiColumnCollectionHeader.ViewState.ColumnState>();

			// Token: 0x020005ED RID: 1517
			[Serializable]
			private struct ColumnState
			{
				// Token: 0x040015C9 RID: 5577
				public int index;

				// Token: 0x040015CA RID: 5578
				public string name;

				// Token: 0x040015CB RID: 5579
				public float actualWidth;

				// Token: 0x040015CC RID: 5580
				public Length width;

				// Token: 0x040015CD RID: 5581
				public bool visible;
			}
		}

		// Token: 0x020005EE RID: 1518
		internal class ColumnData
		{
			// Token: 0x17000AB3 RID: 2739
			// (get) Token: 0x06002953 RID: 10579 RVA: 0x000AAF97 File Offset: 0x000A9197
			// (set) Token: 0x06002954 RID: 10580 RVA: 0x000AAF9F File Offset: 0x000A919F
			public MultiColumnHeaderColumn control { get; set; }

			// Token: 0x17000AB4 RID: 2740
			// (get) Token: 0x06002955 RID: 10581 RVA: 0x000AAFA8 File Offset: 0x000A91A8
			// (set) Token: 0x06002956 RID: 10582 RVA: 0x000AAFB0 File Offset: 0x000A91B0
			public MultiColumnHeaderColumnResizeHandle resizeHandle { get; set; }
		}

		// Token: 0x020005EF RID: 1519
		private struct SortedColumnState
		{
			// Token: 0x06002958 RID: 10584 RVA: 0x000AAFB9 File Offset: 0x000A91B9
			public SortedColumnState(SortColumnDescription desc, SortDirection dir)
			{
				this.columnDesc = desc;
				this.direction = dir;
			}

			// Token: 0x040015D0 RID: 5584
			public SortColumnDescription columnDesc;

			// Token: 0x040015D1 RID: 5585
			public SortDirection direction;
		}
	}
}
