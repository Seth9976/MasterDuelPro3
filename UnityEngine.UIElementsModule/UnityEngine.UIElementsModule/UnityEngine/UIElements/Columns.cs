using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010B RID: 267
	[UxmlObject]
	public class Columns : ICollection<Column>, IEnumerable<Column>, IEnumerable, INotifyBindablePropertyChanged
	{
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000850 RID: 2128 RVA: 0x00028480 File Offset: 0x00026680
		// (remove) Token: 0x06000851 RID: 2129 RVA: 0x000284B8 File Offset: 0x000266B8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x000284ED File Offset: 0x000266ED
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x000284F8 File Offset: 0x000266F8
		[CreateProperty]
		public string primaryColumnName
		{
			get
			{
				return this.m_PrimaryColumnName;
			}
			set
			{
				bool flag = this.m_PrimaryColumnName == value;
				if (!flag)
				{
					this.m_PrimaryColumnName = value;
					this.NotifyChange(ColumnsDataType.PrimaryColumn);
					this.NotifyPropertyChanged(in Columns.primaryColumnNameProperty);
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00028533 File Offset: 0x00026733
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0002853C File Offset: 0x0002673C
		[CreateProperty]
		public bool reorderable
		{
			get
			{
				return this.m_Reorderable;
			}
			set
			{
				bool flag = this.m_Reorderable == value;
				if (!flag)
				{
					this.m_Reorderable = value;
					this.NotifyChange(ColumnsDataType.Reorderable);
					this.NotifyPropertyChanged(in Columns.reorderableProperty);
				}
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00028574 File Offset: 0x00026774
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0002857C File Offset: 0x0002677C
		[CreateProperty]
		public bool resizable
		{
			get
			{
				return this.m_Resizable;
			}
			set
			{
				bool flag = this.m_Resizable == value;
				if (!flag)
				{
					this.m_Resizable = value;
					this.NotifyChange(ColumnsDataType.Resizable);
					this.NotifyPropertyChanged(in Columns.resizableProperty);
				}
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x000285B4 File Offset: 0x000267B4
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x000285BC File Offset: 0x000267BC
		[CreateProperty]
		public bool resizePreview
		{
			get
			{
				return this.m_ResizePreview;
			}
			set
			{
				bool flag = this.m_ResizePreview == value;
				if (!flag)
				{
					this.m_ResizePreview = value;
					this.NotifyChange(ColumnsDataType.ResizePreview);
					this.NotifyPropertyChanged(in Columns.resizePreviewProperty);
				}
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x000285F4 File Offset: 0x000267F4
		internal IEnumerable<Column> displayList
		{
			get
			{
				this.InitOrderColumns();
				return this.m_DisplayColumns;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00028614 File Offset: 0x00026814
		internal IEnumerable<Column> visibleList
		{
			get
			{
				this.UpdateVisibleColumns();
				return this.m_VisibleColumns;
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x0600085C RID: 2140 RVA: 0x00028634 File Offset: 0x00026834
		// (remove) Token: 0x0600085D RID: 2141 RVA: 0x0002866C File Offset: 0x0002686C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<ColumnsDataType> changed;

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000286A1 File Offset: 0x000268A1
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x000286AC File Offset: 0x000268AC
		[CreateProperty]
		public Columns.StretchMode stretchMode
		{
			get
			{
				return this.m_StretchMode;
			}
			set
			{
				bool flag = this.m_StretchMode == value;
				if (!flag)
				{
					this.m_StretchMode = value;
					this.NotifyChange(ColumnsDataType.StretchMode);
					this.NotifyPropertyChanged(in Columns.stretchModeProperty);
				}
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000860 RID: 2144 RVA: 0x000286E4 File Offset: 0x000268E4
		// (remove) Token: 0x06000861 RID: 2145 RVA: 0x0002871C File Offset: 0x0002691C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column, int> columnAdded;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000862 RID: 2146 RVA: 0x00028754 File Offset: 0x00026954
		// (remove) Token: 0x06000863 RID: 2147 RVA: 0x0002878C File Offset: 0x0002698C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column> columnRemoved;

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000864 RID: 2148 RVA: 0x000287C4 File Offset: 0x000269C4
		// (remove) Token: 0x06000865 RID: 2149 RVA: 0x000287FC File Offset: 0x000269FC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column, ColumnDataType> columnChanged;

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06000866 RID: 2150 RVA: 0x00028834 File Offset: 0x00026A34
		// (remove) Token: 0x06000867 RID: 2151 RVA: 0x0002886C File Offset: 0x00026A6C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column> columnResized;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000868 RID: 2152 RVA: 0x000288A4 File Offset: 0x00026AA4
		// (remove) Token: 0x06000869 RID: 2153 RVA: 0x000288DC File Offset: 0x00026ADC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column, int, int> columnReordered;

		// Token: 0x0600086A RID: 2154 RVA: 0x00028914 File Offset: 0x00026B14
		public bool IsPrimary(Column column)
		{
			return this.primaryColumnName == column.name || (string.IsNullOrEmpty(this.primaryColumnName) && column.visibleIndex == 0);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00028958 File Offset: 0x00026B58
		public IEnumerator<Column> GetEnumerator()
		{
			return this.m_Columns.GetEnumerator();
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00028978 File Offset: 0x00026B78
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00028990 File Offset: 0x00026B90
		public void Add(Column item)
		{
			this.Insert(this.m_Columns.Count, item);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x000289A8 File Offset: 0x00026BA8
		public void Clear()
		{
			while (this.m_Columns.Count > 0)
			{
				this.Remove(this.m_Columns[this.m_Columns.Count - 1]);
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x000289EC File Offset: 0x00026BEC
		public bool Contains(Column item)
		{
			return this.m_Columns.Contains(item);
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x00028A0C File Offset: 0x00026C0C
		public bool Contains(string name)
		{
			foreach (Column column in this.m_Columns)
			{
				bool flag = column.name == name;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00028A70 File Offset: 0x00026C70
		public void CopyTo(Column[] array, int arrayIndex)
		{
			this.m_Columns.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00028A84 File Offset: 0x00026C84
		public bool Remove(Column column)
		{
			bool flag = column == null;
			if (flag)
			{
				throw new ArgumentException("Cannot remove null column");
			}
			bool flag2 = this.m_Columns.Remove(column);
			bool flag3;
			if (flag2)
			{
				List<Column> displayColumns = this.m_DisplayColumns;
				if (displayColumns != null)
				{
					displayColumns.Remove(column);
				}
				List<Column> visibleColumns = this.m_VisibleColumns;
				if (visibleColumns != null)
				{
					visibleColumns.Remove(column);
				}
				column.collection = null;
				column.propertyChanged -= this.OnColumnsPropertyChanged;
				column.changed -= this.OnColumnChanged;
				column.resized -= this.OnColumnResized;
				Action<Column> action = this.columnRemoved;
				if (action != null)
				{
					action(column);
				}
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00028B3C File Offset: 0x00026D3C
		private void OnColumnsPropertyChanged(object sender, BindablePropertyChangedEventArgs args)
		{
			Column c = (Column)sender;
			int index = this.m_Columns.IndexOf(c);
			bool flag = index > 0;
			if (flag)
			{
				string fullPath = string.Format("columns[{0}].{1}", index, args.propertyName);
				BindingId bindingId = fullPath;
				this.NotifyPropertyChanged(in bindingId);
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00028B98 File Offset: 0x00026D98
		private void OnColumnChanged(Column column, ColumnDataType type)
		{
			bool flag = type == ColumnDataType.Visibility;
			if (flag)
			{
				this.DirtyVisibleColumns();
			}
			Action<Column, ColumnDataType> action = this.columnChanged;
			if (action != null)
			{
				action(column, type);
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00028BC9 File Offset: 0x00026DC9
		private void OnColumnResized(Column column)
		{
			Action<Column> action = this.columnResized;
			if (action != null)
			{
				action(column);
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00028BDF File Offset: 0x00026DDF
		public int Count
		{
			get
			{
				return this.m_Columns.Count;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00028BEC File Offset: 0x00026DEC
		public bool IsReadOnly
		{
			get
			{
				return this.m_Columns.IsReadOnly;
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00028BFC File Offset: 0x00026DFC
		public int IndexOf(Column column)
		{
			return this.m_Columns.IndexOf(column);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00028C1C File Offset: 0x00026E1C
		public void Insert(int index, Column column)
		{
			bool flag = column == null;
			if (flag)
			{
				throw new ArgumentException("Cannot insert null column");
			}
			bool flag2 = column.collection == this;
			if (flag2)
			{
				throw new ArgumentException("Already contains this column");
			}
			bool flag3 = column.collection != null;
			if (flag3)
			{
				column.collection.Remove(column);
			}
			this.m_Columns.Insert(index, column);
			bool flag4 = this.m_DisplayColumns != null;
			if (flag4)
			{
				this.m_DisplayColumns.Insert(index, column);
				this.DirtyVisibleColumns();
			}
			column.collection = this;
			column.propertyChanged += this.OnColumnsPropertyChanged;
			column.changed += this.OnColumnChanged;
			column.resized += this.OnColumnResized;
			Action<Column, int> action = this.columnAdded;
			if (action != null)
			{
				action(column, index);
			}
		}

		// Token: 0x17000173 RID: 371
		public Column this[int index]
		{
			get
			{
				return this.m_Columns[index];
			}
		}

		// Token: 0x17000174 RID: 372
		public Column this[string name]
		{
			get
			{
				foreach (Column column in this.m_Columns)
				{
					bool flag = column.name == name;
					if (flag)
					{
						return column;
					}
				}
				return null;
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00028D7C File Offset: 0x00026F7C
		public void ReorderDisplay(int from, int to)
		{
			this.InitOrderColumns();
			Column col = this.m_DisplayColumns[from];
			this.m_DisplayColumns.RemoveAt(from);
			this.m_DisplayColumns.Insert(to, col);
			this.DirtyVisibleColumns();
			Action<Column, int, int> action = this.columnReordered;
			if (action != null)
			{
				action(col, from, to);
			}
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x00028DD8 File Offset: 0x00026FD8
		private void InitOrderColumns()
		{
			bool flag = this.m_DisplayColumns == null;
			if (flag)
			{
				this.m_DisplayColumns = new List<Column>(this);
			}
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00028E04 File Offset: 0x00027004
		private void DirtyVisibleColumns()
		{
			this.m_VisibleColumnsDirty = true;
			bool flag = this.m_VisibleColumns != null;
			if (flag)
			{
				this.m_VisibleColumns.Clear();
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00028E34 File Offset: 0x00027034
		private void UpdateVisibleColumns()
		{
			bool flag = !this.m_VisibleColumnsDirty;
			if (!flag)
			{
				this.InitOrderColumns();
				bool flag2 = this.m_VisibleColumns == null;
				if (flag2)
				{
					this.m_VisibleColumns = new List<Column>(this.m_Columns.Count);
				}
				this.m_VisibleColumns.AddRange(this.m_DisplayColumns.FindAll((Column c) => c.visible));
				this.m_VisibleColumnsDirty = false;
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00028EBA File Offset: 0x000270BA
		private void NotifyChange(ColumnsDataType type)
		{
			Action<ColumnsDataType> action = this.changed;
			if (action != null)
			{
				action(type);
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00028ED0 File Offset: 0x000270D0
		private void NotifyPropertyChanged(in BindingId property)
		{
			EventHandler<BindablePropertyChangedEventArgs> eventHandler = this.propertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, new BindablePropertyChangedEventArgs(in property));
			}
		}

		// Token: 0x0400053F RID: 1343
		private static readonly BindingId primaryColumnNameProperty = "primaryColumnName";

		// Token: 0x04000540 RID: 1344
		private static readonly BindingId reorderableProperty = "reorderable";

		// Token: 0x04000541 RID: 1345
		private static readonly BindingId resizableProperty = "resizable";

		// Token: 0x04000542 RID: 1346
		private static readonly BindingId resizePreviewProperty = "resizePreview";

		// Token: 0x04000543 RID: 1347
		private static readonly BindingId stretchModeProperty = "stretchMode";

		// Token: 0x04000544 RID: 1348
		private IList<Column> m_Columns = new List<Column>();

		// Token: 0x04000545 RID: 1349
		private List<Column> m_DisplayColumns;

		// Token: 0x04000546 RID: 1350
		private List<Column> m_VisibleColumns;

		// Token: 0x04000547 RID: 1351
		private bool m_VisibleColumnsDirty = true;

		// Token: 0x04000548 RID: 1352
		private Columns.StretchMode m_StretchMode = Columns.StretchMode.GrowAndFill;

		// Token: 0x04000549 RID: 1353
		private bool m_Reorderable = true;

		// Token: 0x0400054A RID: 1354
		private bool m_Resizable = true;

		// Token: 0x0400054B RID: 1355
		private bool m_ResizePreview;

		// Token: 0x0400054C RID: 1356
		private string m_PrimaryColumnName;

		// Token: 0x0200010C RID: 268
		public enum StretchMode
		{
			// Token: 0x04000555 RID: 1365
			Grow,
			// Token: 0x04000556 RID: 1366
			GrowAndFill
		}

		// Token: 0x0200010D RID: 269
		[Obsolete("UxmlObjectFactory<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory<T> : UxmlObjectFactory<T, Columns.UxmlObjectTraits<T>> where T : Columns, new()
		{
		}

		// Token: 0x0200010E RID: 270
		[Obsolete("UxmlObjectFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory : Columns.UxmlObjectFactory<Columns>
		{
		}

		// Token: 0x0200010F RID: 271
		[Obsolete("UxmlObjectTraits<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectTraits<T> : UnityEngine.UIElements.UxmlObjectTraits<T> where T : Columns
		{
			// Token: 0x06000886 RID: 2182 RVA: 0x00028F88 File Offset: 0x00027188
			public override void Init(ref T obj, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ref obj, bag, cc);
				obj.primaryColumnName = this.m_PrimaryColumnName.GetValueFromBag(bag, cc);
				obj.stretchMode = this.m_StretchMode.GetValueFromBag(bag, cc);
				obj.reorderable = this.m_Reorderable.GetValueFromBag(bag, cc);
				obj.resizable = this.m_Resizable.GetValueFromBag(bag, cc);
				obj.resizePreview = this.m_ResizePreview.GetValueFromBag(bag, cc);
				List<Column> columnList = this.m_Columns.GetValueFromBag(bag, cc);
				bool flag = columnList != null;
				if (flag)
				{
					foreach (Column column in columnList)
					{
						obj.Add(column);
					}
				}
			}

			// Token: 0x04000557 RID: 1367
			private readonly UxmlStringAttributeDescription m_PrimaryColumnName = new UxmlStringAttributeDescription
			{
				name = "primary-column-name"
			};

			// Token: 0x04000558 RID: 1368
			private readonly UxmlEnumAttributeDescription<Columns.StretchMode> m_StretchMode = new UxmlEnumAttributeDescription<Columns.StretchMode>
			{
				name = "stretch-mode",
				defaultValue = Columns.StretchMode.GrowAndFill
			};

			// Token: 0x04000559 RID: 1369
			private readonly UxmlBoolAttributeDescription m_Reorderable = new UxmlBoolAttributeDescription
			{
				name = "reorderable",
				defaultValue = true
			};

			// Token: 0x0400055A RID: 1370
			private readonly UxmlBoolAttributeDescription m_Resizable = new UxmlBoolAttributeDescription
			{
				name = "resizable",
				defaultValue = true
			};

			// Token: 0x0400055B RID: 1371
			private readonly UxmlBoolAttributeDescription m_ResizePreview = new UxmlBoolAttributeDescription
			{
				name = "resize-preview"
			};

			// Token: 0x0400055C RID: 1372
			private readonly UxmlObjectListAttributeDescription<Column> m_Columns = new UxmlObjectListAttributeDescription<Column>();
		}
	}
}
