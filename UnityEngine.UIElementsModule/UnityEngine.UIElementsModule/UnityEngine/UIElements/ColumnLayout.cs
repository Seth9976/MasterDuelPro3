using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000107 RID: 263
	internal class ColumnLayout
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x00026B2A File Offset: 0x00024D2A
		public Columns columns
		{
			get
			{
				return this.m_Columns;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00026B34 File Offset: 0x00024D34
		public float columnsWidth
		{
			get
			{
				bool columnsWidthDirty = this.m_ColumnsWidthDirty;
				if (columnsWidthDirty)
				{
					this.m_ColumnsWidth = 0f;
					foreach (Column column in this.m_Columns.visibleList)
					{
						this.m_ColumnsWidth += column.desiredWidth;
					}
					this.m_ColumnsWidthDirty = false;
				}
				return this.m_ColumnsWidth;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x00026BC0 File Offset: 0x00024DC0
		public float layoutWidth
		{
			get
			{
				return this.m_LayoutWidth;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x00026BC8 File Offset: 0x00024DC8
		public float minColumnsWidth
		{
			get
			{
				return this.m_MinColumnsWidth;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x00026BD0 File Offset: 0x00024DD0
		public float maxColumnsWidth
		{
			get
			{
				return this.m_MaxColumnsWidth;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00026BD8 File Offset: 0x00024DD8
		public bool hasStretchableColumns
		{
			get
			{
				return this.m_StretchableColumns.Count > 0;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00026BE8 File Offset: 0x00024DE8
		public bool hasRelativeWidthColumns
		{
			get
			{
				return this.m_RelativeWidthColumns.Count > 0 || this.m_MixedWidthColumns.Count > 0;
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000829 RID: 2089 RVA: 0x00026C0C File Offset: 0x00024E0C
		// (remove) Token: 0x0600082A RID: 2090 RVA: 0x00026C44 File Offset: 0x00024E44
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action layoutRequested;

		// Token: 0x0600082B RID: 2091 RVA: 0x00026C7C File Offset: 0x00024E7C
		public ColumnLayout(Columns columns)
		{
			this.m_Columns = columns;
			for (int i = 0; i < columns.Count; i++)
			{
				this.OnColumnAdded(columns[i], i);
			}
			columns.columnAdded += this.OnColumnAdded;
			columns.columnRemoved += this.OnColumnRemoved;
			columns.columnReordered += this.OnColumnReordered;
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00026D90 File Offset: 0x00024F90
		public void Dirty()
		{
			bool isDirty = this.m_IsDirty;
			if (!isDirty)
			{
				this.m_IsDirty = true;
				this.ClearCache();
				Action action = this.layoutRequested;
				if (action != null)
				{
					action();
				}
			}
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00026DCA File Offset: 0x00024FCA
		private void OnColumnAdded(Column column, int index)
		{
			column.changed += this.OnColumnChanged;
			column.resized += this.OnColumnResized;
			this.Dirty();
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00026DFA File Offset: 0x00024FFA
		private void OnColumnRemoved(Column column)
		{
			column.changed -= this.OnColumnChanged;
			column.resized -= this.OnColumnResized;
			this.Dirty();
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00026E2A File Offset: 0x0002502A
		private void OnColumnReordered(Column column, int from, int to)
		{
			this.Dirty();
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00026E34 File Offset: 0x00025034
		private bool RequiresLayoutUpdate(ColumnDataType type)
		{
			return type - ColumnDataType.Visibility <= 4 || type - ColumnDataType.HeaderTemplate <= 1;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00026E60 File Offset: 0x00025060
		private void OnColumnChanged(Column column, ColumnDataType type)
		{
			bool flag = this.m_DragResizing || !this.RequiresLayoutUpdate(type);
			if (!flag)
			{
				this.Dirty();
			}
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00026E90 File Offset: 0x00025090
		private void OnColumnResized(Column column)
		{
			this.m_ColumnsWidthDirty = true;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00026E9C File Offset: 0x0002509C
		private static bool IsClamped(float value, float min, float max)
		{
			return value >= min && value <= max;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00026EBC File Offset: 0x000250BC
		public void DoLayout(float width)
		{
			this.m_LayoutWidth = width;
			bool isDirty = this.m_IsDirty;
			if (isDirty)
			{
				this.UpdateCache();
			}
			bool hasRelativeWidthColumns = this.hasRelativeWidthColumns;
			if (hasRelativeWidthColumns)
			{
				this.UpdateMinAndMaxColumnsWidth();
			}
			float totalColumnsWidth = 0f;
			float fixedColumnsWidth = 0f;
			float totalStretchableWidth = 0f;
			List<Column> stretchableColumnsWithInvalidWidth = new List<Column>();
			List<Column> stretchableColumnsWithValidWidth = new List<Column>();
			foreach (Column column in this.m_Columns)
			{
				bool flag = !column.visible;
				if (!flag)
				{
					float minWidth = column.GetMinWidth(this.m_LayoutWidth);
					float maxWidth = column.GetMaxWidth(this.m_LayoutWidth);
					float columnWidth = column.GetWidth(this.m_LayoutWidth);
					bool flag2 = float.IsNaN(column.desiredWidth);
					if (flag2)
					{
						bool flag3 = this.m_Columns.stretchMode == Columns.StretchMode.GrowAndFill && column.stretchable;
						if (flag3)
						{
							stretchableColumnsWithInvalidWidth.Add(column);
							continue;
						}
						column.desiredWidth = Mathf.Clamp(columnWidth, minWidth, maxWidth);
					}
					else
					{
						bool flag4 = this.m_Columns.stretchMode == Columns.StretchMode.GrowAndFill && column.stretchable;
						if (flag4)
						{
							stretchableColumnsWithValidWidth.Add(column);
							totalStretchableWidth += this.GetDesiredWidth(column);
						}
						bool flag5 = !ColumnLayout.IsClamped(column.desiredWidth, minWidth, maxWidth);
						if (flag5)
						{
							column.desiredWidth = Mathf.Clamp(columnWidth, minWidth, maxWidth);
						}
						bool flag6 = this.columns.stretchMode == Columns.StretchMode.Grow && column.width.unit == LengthUnit.Percent;
						if (flag6)
						{
							column.desiredWidth = Mathf.Clamp(columnWidth, minWidth, maxWidth);
						}
					}
					bool flag7 = !column.stretchable;
					if (flag7)
					{
						fixedColumnsWidth += column.desiredWidth;
					}
					totalColumnsWidth += column.desiredWidth;
				}
			}
			bool flag8 = stretchableColumnsWithInvalidWidth.Count > 0;
			if (flag8)
			{
				float availableWidth = Math.Max(0f, width - fixedColumnsWidth);
				int count = this.m_StretchableColumns.Count;
				stretchableColumnsWithInvalidWidth.Sort((Column c1, Column c2) => c1.GetMaxWidth(this.m_LayoutWidth).CompareTo(c2.GetMaxWidth(this.m_LayoutWidth)));
				foreach (Column column2 in stretchableColumnsWithInvalidWidth)
				{
					float widthPerColumn = availableWidth / (float)count;
					column2.desiredWidth = Mathf.Clamp(widthPerColumn, column2.GetMinWidth(this.m_LayoutWidth), column2.GetMaxWidth(this.m_LayoutWidth));
					availableWidth = Math.Max(0f, availableWidth - column2.desiredWidth);
					count--;
				}
				stretchableColumnsWithValidWidth.Sort((Column c1, Column c2) => c1.GetMaxWidth(this.m_LayoutWidth).CompareTo(c2.GetMaxWidth(this.m_LayoutWidth)));
				foreach (Column column3 in stretchableColumnsWithValidWidth)
				{
					float oldWidth = this.GetDesiredWidth(column3);
					float ratio = oldWidth / totalStretchableWidth;
					float widthPerColumn2 = availableWidth * ratio;
					column3.desiredWidth = Mathf.Clamp(widthPerColumn2, column3.GetMinWidth(this.m_LayoutWidth), column3.GetMaxWidth(this.m_LayoutWidth));
					availableWidth = Math.Max(0f, availableWidth - column3.desiredWidth);
					totalStretchableWidth -= oldWidth;
					count--;
				}
			}
			bool flag9 = this.hasStretchableColumns || (this.hasRelativeWidthColumns && this.m_Columns.stretchMode == Columns.StretchMode.GrowAndFill);
			if (flag9)
			{
				float deltaWidth = 0f;
				bool flag10 = this.m_Columns.stretchMode == Columns.StretchMode.Grow;
				if (flag10)
				{
					bool flag11 = !float.IsNaN(this.m_PreviousWidth);
					if (flag11)
					{
						deltaWidth = this.m_PreviousWidth - width;
					}
				}
				else
				{
					deltaWidth = this.columnsWidth - Mathf.Clamp(width, this.minColumnsWidth, this.maxColumnsWidth);
				}
				bool flag12 = deltaWidth != 0f;
				if (flag12)
				{
					List<Column> stretchableColumnsToFit;
					using (CollectionPool<List<Column>, Column>.Get(out stretchableColumnsToFit))
					{
						List<Column> fixedColumnsToFit;
						using (CollectionPool<List<Column>, Column>.Get(out fixedColumnsToFit))
						{
							List<Column> relativeWidthColumnsToFit;
							using (CollectionPool<List<Column>, Column>.Get(out relativeWidthColumnsToFit))
							{
								stretchableColumnsToFit.AddRange(this.m_StretchableColumns);
								fixedColumnsToFit.AddRange(this.m_FixedColumns);
								relativeWidthColumnsToFit.AddRange(this.m_RelativeWidthColumns);
								this.StretchResizeColumns(stretchableColumnsToFit, fixedColumnsToFit, relativeWidthColumnsToFit, ref deltaWidth, false, false);
							}
						}
					}
				}
			}
			this.m_PreviousWidth = width;
			this.m_IsDirty = false;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x000273E0 File Offset: 0x000255E0
		public void StretchResizeColumns(List<Column> stretchableColumns, List<Column> fixedColumns, List<Column> relativeWidthColumns, ref float delta, bool resizeToFit, bool dragResize)
		{
			bool flag = stretchableColumns.Count == 0 && relativeWidthColumns.Count == 0 && fixedColumns.Count == 0;
			if (!flag)
			{
				bool flag2 = delta > 0f;
				if (flag2)
				{
					this.DistributeOverflow(stretchableColumns, fixedColumns, relativeWidthColumns, ref delta, resizeToFit, dragResize);
				}
				else
				{
					this.DistributeExcess(stretchableColumns, fixedColumns, relativeWidthColumns, ref delta, resizeToFit, dragResize);
				}
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00027444 File Offset: 0x00025644
		private void DistributeOverflow(List<Column> stretchableColumns, List<Column> fixedColumns, List<Column> relativeWidthColumns, ref float delta, bool resizeToFit, bool dragResize)
		{
			float distributedDelta = Math.Abs(delta);
			bool flag = !resizeToFit && !dragResize;
			if (flag)
			{
				distributedDelta = this.RecomputeToDesiredWidth(fixedColumns, distributedDelta, true, true);
				distributedDelta = this.RecomputeToDesiredWidth(relativeWidthColumns, distributedDelta, true, true);
			}
			distributedDelta = this.RecomputeToMinWidthProportionally(stretchableColumns, distributedDelta, !resizeToFit && !dragResize);
			if (resizeToFit)
			{
				distributedDelta = this.RecomputeToMinWidthProportionally(relativeWidthColumns, distributedDelta, false);
				distributedDelta = this.RecomputeToMinWidthProportionally(fixedColumns, distributedDelta, false);
				distributedDelta = this.RecomputeToMinWidth(relativeWidthColumns, distributedDelta, false);
				distributedDelta = this.RecomputeToMinWidth(fixedColumns, distributedDelta, false);
			}
			else if (dragResize)
			{
				distributedDelta = this.RecomputeToMinWidth(relativeWidthColumns, distributedDelta, true);
				distributedDelta = this.RecomputeToMinWidth(fixedColumns, distributedDelta, true);
			}
			else
			{
				bool flag2 = distributedDelta > 0f;
				if (flag2)
				{
					distributedDelta = this.RecomputeToMinWidth(relativeWidthColumns, distributedDelta, true);
					distributedDelta = this.RecomputeToMinWidth(fixedColumns, distributedDelta, true);
				}
			}
			delta = Math.Max(0f, delta - distributedDelta);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002751C File Offset: 0x0002571C
		private void DistributeExcess(List<Column> stretchableColumns, List<Column> fixedColumns, List<Column> relativeWidthColumns, ref float delta, bool resizeToFit, bool dragResize)
		{
			float distributedDelta = Math.Abs(delta);
			bool flag = !resizeToFit && !dragResize;
			if (flag)
			{
				distributedDelta = this.RecomputeToDesiredWidth(fixedColumns, distributedDelta, true, false);
				distributedDelta = this.RecomputeToDesiredWidth(relativeWidthColumns, distributedDelta, true, false);
			}
			if (dragResize)
			{
				distributedDelta = this.RecomputeToDesiredWidth(fixedColumns, distributedDelta, true, false);
				distributedDelta = this.RecomputeToDesiredWidth(relativeWidthColumns, distributedDelta, true, false);
			}
			distributedDelta = this.RecomputeToMaxWidthProportionally(stretchableColumns, distributedDelta, !resizeToFit && !dragResize);
			if (resizeToFit)
			{
				distributedDelta = this.RecomputeToMaxWidthProportionally(relativeWidthColumns, distributedDelta, false);
				distributedDelta = this.RecomputeToMaxWidthProportionally(fixedColumns, distributedDelta, false);
				distributedDelta = this.RecomputeToMaxWidth(relativeWidthColumns, distributedDelta, false);
				distributedDelta = this.RecomputeToMaxWidth(fixedColumns, distributedDelta, false);
			}
			delta += distributedDelta;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x000275C4 File Offset: 0x000257C4
		private float RecomputeToMaxWidthProportionally(List<Column> columns, float distributedDelta, bool setDesiredWidthOnly = false)
		{
			bool flag = distributedDelta > 0f;
			if (flag)
			{
				columns.Sort((Column c1, Column c2) => c1.GetMaxWidth(this.m_LayoutWidth).CompareTo(c2.GetMaxWidth(this.m_LayoutWidth)));
				float totalColumnWidth = 0f;
				columns.ForEach(delegate(Column c)
				{
					totalColumnWidth += this.GetDesiredWidth(c);
				});
				for (int i = 0; i < columns.Count; i++)
				{
					Column column = columns[i];
					float oldWidth = this.GetDesiredWidth(column);
					float ratio = this.GetDesiredWidth(column) / totalColumnWidth;
					float deltaPerColumn = distributedDelta * ratio;
					float appliedDelta = 0f;
					float maxWidth = column.GetMaxWidth(this.m_LayoutWidth);
					bool flag2 = this.GetDesiredWidth(column) < maxWidth;
					if (flag2)
					{
						appliedDelta = Math.Min(deltaPerColumn, maxWidth - this.GetDesiredWidth(column));
					}
					bool flag3 = appliedDelta > 0f;
					if (flag3)
					{
						this.ResizeColumn(column, this.GetDesiredWidth(column) + appliedDelta, setDesiredWidthOnly);
					}
					totalColumnWidth -= oldWidth;
					distributedDelta -= appliedDelta;
					bool flag4 = distributedDelta <= 0f;
					if (flag4)
					{
						break;
					}
				}
			}
			return distributedDelta;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x000276F4 File Offset: 0x000258F4
		private float RecomputeToMinWidthProportionally(List<Column> columns, float distributedDelta, bool setDesiredWidthOnly = false)
		{
			bool flag = distributedDelta > 0f;
			if (flag)
			{
				columns.Sort((Column c1, Column c2) => c2.GetMinWidth(this.m_LayoutWidth).CompareTo(c1.GetMinWidth(this.m_LayoutWidth)));
				float totalColumnsWidth = 0f;
				columns.ForEach(delegate(Column c)
				{
					totalColumnsWidth += this.GetDesiredWidth(c);
				});
				for (int i = 0; i < columns.Count; i++)
				{
					Column column = columns[i];
					float oldWidth = this.GetDesiredWidth(column);
					float ratio = this.GetDesiredWidth(column) / totalColumnsWidth;
					float deltaPerColumn = distributedDelta * ratio;
					float appliedDelta = 0f;
					bool flag2 = this.GetDesiredWidth(column) > column.GetMinWidth(this.m_LayoutWidth);
					if (flag2)
					{
						appliedDelta = Math.Min(deltaPerColumn, this.GetDesiredWidth(column) - column.GetMinWidth(this.m_LayoutWidth));
					}
					bool flag3 = appliedDelta > 0f;
					if (flag3)
					{
						this.ResizeColumn(column, this.GetDesiredWidth(column) - appliedDelta, setDesiredWidthOnly);
					}
					totalColumnsWidth -= oldWidth;
					distributedDelta -= appliedDelta;
					bool flag4 = distributedDelta <= 0f;
					if (flag4)
					{
						break;
					}
				}
			}
			return distributedDelta;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00027828 File Offset: 0x00025A28
		private float RecomputeToDesiredWidth(List<Column> columns, float distributedDelta, bool setDesiredWidthOnly, bool distributeOverflow)
		{
			if (distributeOverflow)
			{
				for (int i = columns.Count - 1; i >= 0; i--)
				{
					distributedDelta = this.RecomputeToDesiredWidth(columns[i], distributedDelta, setDesiredWidthOnly, true);
					bool flag = distributedDelta <= 0f;
					if (flag)
					{
						break;
					}
				}
			}
			else
			{
				for (int j = 0; j < columns.Count; j++)
				{
					distributedDelta = this.RecomputeToDesiredWidth(columns[j], distributedDelta, setDesiredWidthOnly, false);
					bool flag2 = distributedDelta <= 0f;
					if (flag2)
					{
						break;
					}
				}
			}
			return distributedDelta;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x000278CC File Offset: 0x00025ACC
		private float RecomputeToDesiredWidth(Column column, float distributedDelta, bool setDesiredWidthOnly, bool distributeOverflow)
		{
			float appliedDelta = 0f;
			float clampedWidth = Mathf.Clamp(column.GetWidth(this.m_LayoutWidth), column.GetMinWidth(this.m_LayoutWidth), column.GetMaxWidth(this.m_LayoutWidth));
			bool flag = this.GetDesiredWidth(column) > clampedWidth && distributeOverflow;
			if (flag)
			{
				appliedDelta = Math.Min(distributedDelta, Math.Abs(this.GetDesiredWidth(column) - clampedWidth));
			}
			bool flag2 = this.GetDesiredWidth(column) < clampedWidth && !distributeOverflow;
			if (flag2)
			{
				appliedDelta = Math.Min(distributedDelta, Math.Abs(clampedWidth - this.GetDesiredWidth(column)));
			}
			float width = (distributeOverflow ? (this.GetDesiredWidth(column) - appliedDelta) : (this.GetDesiredWidth(column) + appliedDelta));
			bool flag3 = appliedDelta > 0f;
			if (flag3)
			{
				this.ResizeColumn(column, width, setDesiredWidthOnly);
			}
			distributedDelta -= appliedDelta;
			return distributedDelta;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002799C File Offset: 0x00025B9C
		private float RecomputeToMinWidth(List<Column> columns, float distributedDelta, bool setDesiredWidthOnly = false)
		{
			bool flag = distributedDelta > 0f;
			if (flag)
			{
				for (int i = columns.Count - 1; i >= 0; i--)
				{
					Column column = columns[i];
					float appliedDelta = 0f;
					bool flag2 = this.GetDesiredWidth(column) > column.GetMinWidth(this.m_LayoutWidth);
					if (flag2)
					{
						appliedDelta = Math.Min(distributedDelta, this.GetDesiredWidth(column) - column.GetMinWidth(this.m_LayoutWidth));
					}
					bool flag3 = appliedDelta > 0f;
					if (flag3)
					{
						this.ResizeColumn(column, this.GetDesiredWidth(column) - appliedDelta, setDesiredWidthOnly);
					}
					distributedDelta -= appliedDelta;
					bool flag4 = distributedDelta <= 0f;
					if (flag4)
					{
						break;
					}
				}
			}
			return distributedDelta;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00027A64 File Offset: 0x00025C64
		private float RecomputeToMaxWidth(List<Column> columns, float distributedDelta, bool setDesiredWidthOnly = false)
		{
			bool flag = distributedDelta > 0f;
			if (flag)
			{
				for (int i = 0; i < columns.Count; i++)
				{
					Column column = columns[i];
					float appliedDelta = 0f;
					bool flag2 = this.GetDesiredWidth(column) < column.GetMaxWidth(this.m_LayoutWidth);
					if (flag2)
					{
						appliedDelta = Math.Min(distributedDelta, Math.Abs(column.GetMaxWidth(this.m_LayoutWidth) - this.GetDesiredWidth(column)));
					}
					bool flag3 = appliedDelta > 0f;
					if (flag3)
					{
						this.ResizeColumn(column, this.GetDesiredWidth(column) + appliedDelta, setDesiredWidthOnly);
					}
					distributedDelta -= appliedDelta;
					bool flag4 = distributedDelta <= 0f;
					if (flag4)
					{
						break;
					}
				}
			}
			return distributedDelta;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00027B2C File Offset: 0x00025D2C
		public void ResizeToFit(float width)
		{
			float delta = this.columnsWidth - Mathf.Clamp(width, this.minColumnsWidth, this.maxColumnsWidth);
			List<Column> stretchableColumnsToFit;
			using (CollectionPool<List<Column>, Column>.Get(out stretchableColumnsToFit))
			{
				List<Column> fixedColumnsToFit;
				using (CollectionPool<List<Column>, Column>.Get(out fixedColumnsToFit))
				{
					List<Column> relativeWidthColumnsToFit;
					using (CollectionPool<List<Column>, Column>.Get(out relativeWidthColumnsToFit))
					{
						stretchableColumnsToFit.AddRange(this.m_StretchableColumns);
						fixedColumnsToFit.AddRange(this.m_FixedColumns);
						relativeWidthColumnsToFit.AddRange(this.m_RelativeWidthColumns);
						this.StretchResizeColumns(stretchableColumnsToFit, fixedColumnsToFit, relativeWidthColumnsToFit, ref delta, true, false);
						bool isDirty = this.m_IsDirty;
						if (isDirty)
						{
							this.UpdateCache();
						}
					}
				}
			}
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00027C10 File Offset: 0x00025E10
		private void ResizeColumn(Column column, float width, bool setDesiredWidthOnly = false)
		{
			Length widthInPercent = new Length(width / this.layoutWidth * 100f, LengthUnit.Percent);
			bool dragResizeInPreviewMode = this.m_DragResizeInPreviewMode;
			if (dragResizeInPreviewMode)
			{
				this.m_PreviewDesiredWidths[column] = width;
			}
			else
			{
				bool flag = !setDesiredWidthOnly;
				if (flag)
				{
					column.width = ((column.width.unit == LengthUnit.Percent) ? widthInPercent : width);
				}
				column.desiredWidth = width;
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00027C88 File Offset: 0x00025E88
		internal void BeginDragResize(Column column, float pos, bool previewMode)
		{
			bool isDirty = this.m_IsDirty;
			if (isDirty)
			{
				throw new Exception("Cannot begin resizing columns because the layout needs to be updated");
			}
			this.m_DragResizeInPreviewMode = previewMode;
			this.m_DragResizing = true;
			int index = column.visibleIndex;
			this.m_DragStartPos = pos;
			this.m_DragLastPos = pos;
			this.m_DragInitialColumnWidth = column.desiredWidth;
			this.m_DragStretchableColumns.Clear();
			this.m_DragFixedColumns.Clear();
			this.m_DragRelativeColumns.Clear();
			bool dragResizeInPreviewMode = this.m_DragResizeInPreviewMode;
			if (dragResizeInPreviewMode)
			{
				bool flag = this.m_PreviewDesiredWidths == null;
				if (flag)
				{
					this.m_PreviewDesiredWidths = new Dictionary<Column, float>();
				}
				this.m_PreviewDesiredWidths[column] = column.desiredWidth;
			}
			for (int i = index + 1; i < this.m_Columns.visibleList.Count<Column>(); i++)
			{
				Column otherColumn = this.m_Columns.visibleList.ElementAt(i);
				bool flag2 = !otherColumn.visible;
				if (!flag2)
				{
					bool stretchable = otherColumn.stretchable;
					if (stretchable)
					{
						this.m_DragStretchableColumns.Add(otherColumn);
					}
					else
					{
						bool flag3 = otherColumn.width.unit == LengthUnit.Percent;
						if (flag3)
						{
							this.m_DragRelativeColumns.Add(otherColumn);
						}
						else
						{
							this.m_DragFixedColumns.Add(otherColumn);
						}
					}
					bool dragResizeInPreviewMode2 = this.m_DragResizeInPreviewMode;
					if (dragResizeInPreviewMode2)
					{
						this.m_PreviewDesiredWidths[otherColumn] = otherColumn.desiredWidth;
					}
				}
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00027E08 File Offset: 0x00026008
		public float GetDesiredPosition(Column column)
		{
			bool flag = !column.visible;
			float num;
			if (flag)
			{
				num = float.NaN;
			}
			else
			{
				float pos = 0f;
				for (int i = 0; i < column.visibleIndex; i++)
				{
					Column otherColumn = this.m_Columns.visibleList.ElementAt(i);
					float width = this.GetDesiredWidth(otherColumn);
					bool flag2 = float.IsNaN(width);
					if (!flag2)
					{
						pos += width;
					}
				}
				num = pos;
			}
			return num;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00027E80 File Offset: 0x00026080
		public float GetDesiredWidth(Column c)
		{
			bool flag = this.m_DragResizeInPreviewMode && this.m_PreviewDesiredWidths.ContainsKey(c);
			float num;
			if (flag)
			{
				num = this.m_PreviewDesiredWidths[c];
			}
			else
			{
				num = c.desiredWidth;
			}
			return num;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00027EC4 File Offset: 0x000260C4
		public void DragResize(Column column, float pos)
		{
			float minWidth = column.GetMinWidth(this.m_LayoutWidth);
			float maxWidth = column.GetMaxWidth(this.m_LayoutWidth);
			bool flag = this.m_Columns.stretchMode == Columns.StretchMode.GrowAndFill;
			if (flag)
			{
				float delta = pos - this.m_DragLastPos;
				float newWidth = Mathf.Clamp(this.GetDesiredWidth(column) + delta, minWidth, maxWidth);
				delta = newWidth - this.GetDesiredWidth(column);
				bool flag2 = this.m_DragStretchableColumns.Count == 0 && delta < 0f;
				if (flag2)
				{
					this.StretchResizeColumns(this.m_DragStretchableColumns, this.m_DragFixedColumns, this.m_DragRelativeColumns, ref delta, false, true);
					newWidth = Mathf.Clamp(this.GetDesiredWidth(column) + newWidth - this.GetDesiredWidth(column), minWidth, maxWidth);
				}
				else
				{
					bool flag3 = delta > 0f && this.columnsWidth + delta < this.m_LayoutWidth;
					if (flag3)
					{
						float requiredDelta = ((delta < this.m_LayoutWidth - this.columnsWidth) ? 0f : (delta - (this.m_LayoutWidth - this.columnsWidth)));
						this.StretchResizeColumns(this.m_DragStretchableColumns, this.m_DragFixedColumns, this.m_DragRelativeColumns, ref requiredDelta, false, true);
						newWidth = Mathf.Clamp(this.GetDesiredWidth(column) + delta - requiredDelta, minWidth, maxWidth);
					}
					else
					{
						this.StretchResizeColumns(this.m_DragStretchableColumns, this.m_DragFixedColumns, this.m_DragRelativeColumns, ref delta, false, true);
						newWidth = Mathf.Clamp(this.GetDesiredWidth(column) + delta, minWidth, maxWidth);
					}
				}
				this.ResizeColumn(column, newWidth, false);
			}
			else
			{
				float delta2 = pos - this.m_DragStartPos;
				float newSize = Math.Max(minWidth, Math.Min(maxWidth, this.m_DragInitialColumnWidth + delta2));
				this.ResizeColumn(column, newSize, false);
			}
			this.m_DragLastPos = pos;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0002807C File Offset: 0x0002627C
		internal void EndDragResize(Column column, bool cancelled)
		{
			bool dragResizeInPreviewMode = this.m_DragResizeInPreviewMode;
			if (dragResizeInPreviewMode)
			{
				this.m_DragResizeInPreviewMode = false;
				bool flag = !cancelled;
				if (flag)
				{
					foreach (KeyValuePair<Column, float> columnPair in this.m_PreviewDesiredWidths)
					{
						this.ResizeColumn(columnPair.Key, columnPair.Value, columnPair.Key != column);
					}
				}
				this.m_PreviewDesiredWidths.Clear();
			}
			this.m_DragResizing = false;
			this.m_DragStretchableColumns.Clear();
			this.m_DragFixedColumns.Clear();
			this.m_DragRelativeColumns.Clear();
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00028144 File Offset: 0x00026344
		private void UpdateCache()
		{
			this.ClearCache();
			foreach (Column column in this.m_Columns.visibleList)
			{
				bool flag = column.stretchable && this.columns.stretchMode == Columns.StretchMode.GrowAndFill;
				if (flag)
				{
					this.m_StretchableColumns.Add(column);
				}
				else
				{
					bool flag2 = column.width.unit == LengthUnit.Pixel;
					if (flag2)
					{
						this.m_FixedColumns.Add(column);
					}
				}
				bool flag3 = column.width.unit == LengthUnit.Percent;
				if (flag3)
				{
					this.m_RelativeWidthColumns.Add(column);
				}
				bool flag4 = column.width.unit == LengthUnit.Pixel && (column.minWidth.unit == LengthUnit.Percent || column.maxWidth.unit == LengthUnit.Percent);
				if (flag4)
				{
					this.m_MixedWidthColumns.Add(column);
				}
				this.m_MaxColumnsWidth += column.GetMaxWidth(this.m_LayoutWidth);
				this.m_MinColumnsWidth += column.GetMinWidth(this.m_LayoutWidth);
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x000282A4 File Offset: 0x000264A4
		private void UpdateMinAndMaxColumnsWidth()
		{
			this.m_MaxColumnsWidth = 0f;
			this.m_MinColumnsWidth = 0f;
			foreach (Column column in this.m_Columns.visibleList)
			{
				this.m_MaxColumnsWidth += column.GetMaxWidth(this.m_LayoutWidth);
				this.m_MinColumnsWidth += column.GetMinWidth(this.m_LayoutWidth);
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002833C File Offset: 0x0002653C
		private void ClearCache()
		{
			this.m_StretchableColumns.Clear();
			this.m_RelativeWidthColumns.Clear();
			this.m_FixedColumns.Clear();
			this.m_MaxColumnsWidth = 0f;
			this.m_MinColumnsWidth = 0f;
			this.m_ColumnsWidthDirty = true;
		}

		// Token: 0x0400051F RID: 1311
		private List<Column> m_StretchableColumns = new List<Column>();

		// Token: 0x04000520 RID: 1312
		private List<Column> m_FixedColumns = new List<Column>();

		// Token: 0x04000521 RID: 1313
		private List<Column> m_RelativeWidthColumns = new List<Column>();

		// Token: 0x04000522 RID: 1314
		private List<Column> m_MixedWidthColumns = new List<Column>();

		// Token: 0x04000523 RID: 1315
		private Columns m_Columns;

		// Token: 0x04000524 RID: 1316
		private float m_ColumnsWidth = 0f;

		// Token: 0x04000525 RID: 1317
		private bool m_ColumnsWidthDirty = true;

		// Token: 0x04000526 RID: 1318
		private float m_MaxColumnsWidth = 0f;

		// Token: 0x04000527 RID: 1319
		private float m_MinColumnsWidth = 0f;

		// Token: 0x04000528 RID: 1320
		private bool m_IsDirty = false;

		// Token: 0x04000529 RID: 1321
		private float m_PreviousWidth = float.NaN;

		// Token: 0x0400052A RID: 1322
		private float m_LayoutWidth = float.NaN;

		// Token: 0x0400052B RID: 1323
		private bool m_DragResizeInPreviewMode;

		// Token: 0x0400052C RID: 1324
		private bool m_DragResizing = false;

		// Token: 0x0400052D RID: 1325
		private float m_DragStartPos;

		// Token: 0x0400052E RID: 1326
		private float m_DragLastPos;

		// Token: 0x0400052F RID: 1327
		private float m_DragInitialColumnWidth;

		// Token: 0x04000530 RID: 1328
		private List<Column> m_DragStretchableColumns = new List<Column>();

		// Token: 0x04000531 RID: 1329
		private List<Column> m_DragRelativeColumns = new List<Column>();

		// Token: 0x04000532 RID: 1330
		private List<Column> m_DragFixedColumns = new List<Column>();

		// Token: 0x04000533 RID: 1331
		private Dictionary<Column, float> m_PreviewDesiredWidths;
	}
}
