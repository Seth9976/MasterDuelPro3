using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x02000101 RID: 257
	[UxmlObject]
	public class Column : INotifyBindablePropertyChanged
	{
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060007DF RID: 2015 RVA: 0x00025DD0 File Offset: 0x00023FD0
		// (remove) Token: 0x060007E0 RID: 2016 RVA: 0x00025E08 File Offset: 0x00024008
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x00025E3D File Offset: 0x0002403D
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x00025E48 File Offset: 0x00024048
		[CreateProperty]
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = this.m_Name == value;
				if (!flag)
				{
					this.m_Name = value;
					this.NotifyChange(ColumnDataType.Name);
					this.NotifyPropertyChanged(in Column.nameProperty);
				}
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00025E83 File Offset: 0x00024083
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x00025E8C File Offset: 0x0002408C
		[CreateProperty]
		public string title
		{
			get
			{
				return this.m_Title;
			}
			set
			{
				bool flag = this.m_Title == value;
				if (!flag)
				{
					this.m_Title = value;
					this.NotifyChange(ColumnDataType.Title);
					this.NotifyPropertyChanged(in Column.titleProperty);
				}
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00025EC7 File Offset: 0x000240C7
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x00025ED0 File Offset: 0x000240D0
		[CreateProperty]
		public Background icon
		{
			get
			{
				return this.m_Icon;
			}
			set
			{
				bool flag = this.m_Icon == value;
				if (!flag)
				{
					this.m_Icon = value;
					this.NotifyChange(ColumnDataType.Icon);
					this.NotifyPropertyChanged(in Column.iconProperty);
				}
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00025F0B File Offset: 0x0002410B
		public Comparison<int> comparison { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x00025F13 File Offset: 0x00024113
		internal int index
		{
			get
			{
				Columns collection = this.collection;
				return (collection != null) ? collection.IndexOf(this) : (-1);
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x00025F28 File Offset: 0x00024128
		internal int displayIndex
		{
			get
			{
				Columns collection = this.collection;
				List<Column> list = ((collection != null) ? collection.displayList : null) as List<Column>;
				return (list != null) ? list.IndexOf(this) : (-1);
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00025F4E File Offset: 0x0002414E
		internal int visibleIndex
		{
			get
			{
				Columns collection = this.collection;
				List<Column> list = ((collection != null) ? collection.visibleList : null) as List<Column>;
				return (list != null) ? list.IndexOf(this) : (-1);
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x00025F74 File Offset: 0x00024174
		// (set) Token: 0x060007EC RID: 2028 RVA: 0x00025F7C File Offset: 0x0002417C
		[CreateProperty]
		public bool visible
		{
			get
			{
				return this.m_Visible;
			}
			set
			{
				bool flag = this.m_Visible == value;
				if (!flag)
				{
					this.m_Visible = value;
					this.NotifyChange(ColumnDataType.Visibility);
					this.NotifyPropertyChanged(in Column.visibleProperty);
				}
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x00025FB4 File Offset: 0x000241B4
		// (set) Token: 0x060007EE RID: 2030 RVA: 0x00025FBC File Offset: 0x000241BC
		[CreateProperty]
		public Length width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				bool flag = this.m_Width == value;
				if (!flag)
				{
					this.m_Width = value;
					this.desiredWidth = float.NaN;
					this.NotifyChange(ColumnDataType.Width);
					this.NotifyPropertyChanged(in Column.widthProperty);
				}
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x00026003 File Offset: 0x00024203
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x0002600C File Offset: 0x0002420C
		[CreateProperty]
		public Length minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				bool flag = this.m_MinWidth == value;
				if (!flag)
				{
					this.m_MinWidth = value;
					this.NotifyChange(ColumnDataType.MinWidth);
					this.NotifyPropertyChanged(in Column.minWidthProperty);
				}
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x00026047 File Offset: 0x00024247
		// (set) Token: 0x060007F2 RID: 2034 RVA: 0x00026050 File Offset: 0x00024250
		[CreateProperty]
		public Length maxWidth
		{
			get
			{
				return this.m_MaxWidth;
			}
			set
			{
				bool flag = this.m_MaxWidth == value;
				if (!flag)
				{
					this.m_MaxWidth = value;
					this.NotifyChange(ColumnDataType.MaxWidth);
					this.NotifyPropertyChanged(in Column.maxWidthProperty);
				}
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060007F3 RID: 2035 RVA: 0x0002608B File Offset: 0x0002428B
		// (set) Token: 0x060007F4 RID: 2036 RVA: 0x00026094 File Offset: 0x00024294
		internal float desiredWidth
		{
			get
			{
				return this.m_DesiredWidth;
			}
			set
			{
				bool flag = this.m_DesiredWidth == value;
				if (!flag)
				{
					this.m_DesiredWidth = value;
					Action<Column> action = this.resized;
					if (action != null)
					{
						action(this);
					}
				}
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060007F5 RID: 2037 RVA: 0x000260CB File Offset: 0x000242CB
		// (set) Token: 0x060007F6 RID: 2038 RVA: 0x000260D4 File Offset: 0x000242D4
		[CreateProperty]
		public bool sortable
		{
			get
			{
				return this.m_Sortable;
			}
			set
			{
				bool flag = this.m_Sortable == value;
				if (!flag)
				{
					this.m_Sortable = value;
					this.NotifyChange(ColumnDataType.Sortable);
					this.NotifyPropertyChanged(in Column.sortableProperty);
				}
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x0002610C File Offset: 0x0002430C
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x00026114 File Offset: 0x00024314
		[CreateProperty]
		public bool stretchable
		{
			get
			{
				return this.m_Stretchable;
			}
			set
			{
				bool flag = this.m_Stretchable == value;
				if (!flag)
				{
					this.m_Stretchable = value;
					this.NotifyChange(ColumnDataType.Stretchable);
					this.NotifyPropertyChanged(in Column.stretchableProperty);
				}
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x0002614C File Offset: 0x0002434C
		// (set) Token: 0x060007FA RID: 2042 RVA: 0x00026154 File Offset: 0x00024354
		[CreateProperty]
		public bool optional
		{
			get
			{
				return this.m_Optional;
			}
			set
			{
				bool flag = this.m_Optional == value;
				if (!flag)
				{
					this.m_Optional = value;
					this.NotifyChange(ColumnDataType.Optional);
					this.NotifyPropertyChanged(in Column.optionalProperty);
				}
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0002618D File Offset: 0x0002438D
		// (set) Token: 0x060007FC RID: 2044 RVA: 0x00026198 File Offset: 0x00024398
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
					this.NotifyChange(ColumnDataType.Resizable);
					this.NotifyPropertyChanged(in Column.resizableProperty);
				}
			}
		}

		// Token: 0x17000157 RID: 343
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x000261D1 File Offset: 0x000243D1
		public string bindingPath
		{
			[CompilerGenerated]
			set
			{
				this.<bindingPath>k__BackingField = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x000261DA File Offset: 0x000243DA
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x000261E4 File Offset: 0x000243E4
		[CreateProperty]
		public VisualTreeAsset headerTemplate
		{
			get
			{
				return this.m_HeaderTemplate;
			}
			set
			{
				bool flag = this.m_HeaderTemplate == value;
				if (!flag)
				{
					this.m_HeaderTemplate = value;
					this.NotifyChange(ColumnDataType.HeaderTemplate);
					this.NotifyPropertyChanged(in Column.headerTemplateProperty);
				}
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00026220 File Offset: 0x00024420
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00026228 File Offset: 0x00024428
		[CreateProperty]
		public VisualTreeAsset cellTemplate
		{
			get
			{
				return this.m_CellTemplate;
			}
			set
			{
				bool flag = this.m_CellTemplate == value;
				if (!flag)
				{
					this.m_CellTemplate = value;
					this.NotifyChange(ColumnDataType.CellTemplate);
					this.NotifyPropertyChanged(in Column.cellTemplateProperty);
				}
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00026264 File Offset: 0x00024464
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0002626C File Offset: 0x0002446C
		public Func<VisualElement> makeHeader
		{
			get
			{
				return this.m_MakeHeader;
			}
			set
			{
				bool flag = this.m_MakeHeader == value;
				if (!flag)
				{
					this.m_MakeHeader = value;
					this.NotifyChange(ColumnDataType.HeaderTemplate);
				}
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0002629C File Offset: 0x0002449C
		public Action<VisualElement> bindHeader
		{
			get
			{
				return this.m_BindHeader;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x000262A4 File Offset: 0x000244A4
		public Action<VisualElement> unbindHeader
		{
			get
			{
				return this.m_UnbindHeader;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x000262AC File Offset: 0x000244AC
		public Action<VisualElement> destroyHeader
		{
			get
			{
				return this.m_DestroyHeader;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x000262B4 File Offset: 0x000244B4
		// (set) Token: 0x06000808 RID: 2056 RVA: 0x000262BC File Offset: 0x000244BC
		public Func<VisualElement> makeCell
		{
			get
			{
				return this.m_MakeCell;
			}
			set
			{
				bool flag = this.m_MakeCell == value;
				if (!flag)
				{
					this.m_MakeCell = value;
					this.NotifyChange(ColumnDataType.CellTemplate);
				}
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000809 RID: 2057 RVA: 0x000262EC File Offset: 0x000244EC
		public Action<VisualElement, int> bindCell
		{
			get
			{
				return this.m_BindCell;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x000262F4 File Offset: 0x000244F4
		public Action<VisualElement, int> unbindCell
		{
			get
			{
				return this.m_UnbindCellItem;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x000262FC File Offset: 0x000244FC
		public Action<VisualElement> destroyCell { get; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x00026304 File Offset: 0x00024504
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x0002630C File Offset: 0x0002450C
		public Columns collection { get; internal set; }

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600080E RID: 2062 RVA: 0x00026318 File Offset: 0x00024518
		// (remove) Token: 0x0600080F RID: 2063 RVA: 0x00026350 File Offset: 0x00024550
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column, ColumnDataType> changed;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000810 RID: 2064 RVA: 0x00026388 File Offset: 0x00024588
		// (remove) Token: 0x06000811 RID: 2065 RVA: 0x000263C0 File Offset: 0x000245C0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<Column> resized;

		// Token: 0x06000812 RID: 2066 RVA: 0x000263F5 File Offset: 0x000245F5
		private void NotifyChange(ColumnDataType type)
		{
			Action<Column, ColumnDataType> action = this.changed;
			if (action != null)
			{
				action(this, type);
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0002640C File Offset: 0x0002460C
		private void NotifyPropertyChanged(in BindingId property)
		{
			EventHandler<BindablePropertyChangedEventArgs> eventHandler = this.propertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, new BindablePropertyChangedEventArgs(in property));
			}
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00026428 File Offset: 0x00024628
		internal float GetWidth(float layoutWidth)
		{
			return (this.width.unit == LengthUnit.Pixel) ? this.width.value : (this.width.value * layoutWidth / 100f);
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00026470 File Offset: 0x00024670
		internal float GetMaxWidth(float layoutWidth)
		{
			return (this.maxWidth.unit == LengthUnit.Pixel) ? this.maxWidth.value : (this.maxWidth.value * layoutWidth / 100f);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000264B8 File Offset: 0x000246B8
		internal float GetMinWidth(float layoutWidth)
		{
			return (this.minWidth.unit == LengthUnit.Pixel) ? this.minWidth.value : (this.minWidth.value * layoutWidth / 100f);
		}

		// Token: 0x040004E7 RID: 1255
		private static readonly BindingId nameProperty = "name";

		// Token: 0x040004E8 RID: 1256
		private static readonly BindingId titleProperty = "title";

		// Token: 0x040004E9 RID: 1257
		private static readonly BindingId iconProperty = "icon";

		// Token: 0x040004EA RID: 1258
		private static readonly BindingId visibleProperty = "visible";

		// Token: 0x040004EB RID: 1259
		private static readonly BindingId widthProperty = "width";

		// Token: 0x040004EC RID: 1260
		private static readonly BindingId minWidthProperty = "minWidth";

		// Token: 0x040004ED RID: 1261
		private static readonly BindingId maxWidthProperty = "maxWidth";

		// Token: 0x040004EE RID: 1262
		private static readonly BindingId sortableProperty = "sortable";

		// Token: 0x040004EF RID: 1263
		private static readonly BindingId stretchableProperty = "stretchable";

		// Token: 0x040004F0 RID: 1264
		private static readonly BindingId optionalProperty = "optional";

		// Token: 0x040004F1 RID: 1265
		private static readonly BindingId resizableProperty = "resizable";

		// Token: 0x040004F2 RID: 1266
		private static readonly BindingId headerTemplateProperty = "headerTemplate";

		// Token: 0x040004F3 RID: 1267
		private static readonly BindingId cellTemplateProperty = "cellTemplate";

		// Token: 0x040004F4 RID: 1268
		private string m_Name;

		// Token: 0x040004F5 RID: 1269
		private string m_Title;

		// Token: 0x040004F6 RID: 1270
		private Background m_Icon;

		// Token: 0x040004F7 RID: 1271
		private bool m_Visible = true;

		// Token: 0x040004F8 RID: 1272
		private Length m_Width = 0f;

		// Token: 0x040004F9 RID: 1273
		private Length m_MinWidth = 35f;

		// Token: 0x040004FA RID: 1274
		private Length m_MaxWidth = 8388608f;

		// Token: 0x040004FB RID: 1275
		private float m_DesiredWidth = float.NaN;

		// Token: 0x040004FC RID: 1276
		private bool m_Stretchable;

		// Token: 0x040004FD RID: 1277
		private bool m_Sortable = true;

		// Token: 0x040004FE RID: 1278
		private bool m_Optional = true;

		// Token: 0x040004FF RID: 1279
		private bool m_Resizable = true;

		// Token: 0x04000500 RID: 1280
		private VisualTreeAsset m_HeaderTemplate;

		// Token: 0x04000501 RID: 1281
		private VisualTreeAsset m_CellTemplate;

		// Token: 0x04000502 RID: 1282
		private Func<VisualElement> m_MakeHeader;

		// Token: 0x04000503 RID: 1283
		private Action<VisualElement> m_BindHeader;

		// Token: 0x04000504 RID: 1284
		private Action<VisualElement> m_UnbindHeader;

		// Token: 0x04000505 RID: 1285
		private Action<VisualElement> m_DestroyHeader;

		// Token: 0x04000506 RID: 1286
		private Func<VisualElement> m_MakeCell;

		// Token: 0x04000507 RID: 1287
		private Action<VisualElement, int> m_BindCell;

		// Token: 0x04000508 RID: 1288
		private Action<VisualElement, int> m_UnbindCellItem;

		// Token: 0x02000102 RID: 258
		[Obsolete("UxmlObjectFactory<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory<T> : UxmlObjectFactory<T, Column.UxmlObjectTraits<T>> where T : Column, new()
		{
		}

		// Token: 0x02000103 RID: 259
		[Obsolete("UxmlObjectFactory<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory : Column.UxmlObjectFactory<Column>
		{
		}

		// Token: 0x02000104 RID: 260
		[Obsolete("UxmlObjectTraits<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectTraits<T> : UnityEngine.UIElements.UxmlObjectTraits<T> where T : Column
		{
			// Token: 0x0600081B RID: 2075 RVA: 0x00026650 File Offset: 0x00024850
			private static Length ParseLength(string str, Length defaultValue)
			{
				float value = defaultValue.value;
				LengthUnit unit = defaultValue.unit;
				int digitEndIndex = 0;
				int unitIndex = -1;
				for (int i = 0; i < str.Length; i++)
				{
					char c = str[i];
					bool flag = char.IsLetter(c) || c == '%';
					if (flag)
					{
						unitIndex = i;
						break;
					}
					digitEndIndex++;
				}
				string floatStr = str.Substring(0, digitEndIndex);
				string unitStr = string.Empty;
				bool flag2 = unitIndex > 0;
				if (flag2)
				{
					unitStr = str.Substring(unitIndex, str.Length - unitIndex).ToLowerInvariant();
				}
				float v;
				bool flag3 = float.TryParse(floatStr, out v);
				if (flag3)
				{
					value = v;
				}
				string text = unitStr;
				string text2 = text;
				if (!(text2 == "px"))
				{
					if (text2 == "%")
					{
						unit = LengthUnit.Percent;
					}
				}
				else
				{
					unit = LengthUnit.Pixel;
				}
				return new Length(value, unit);
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x0002673C File Offset: 0x0002493C
			public override void Init(ref T obj, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ref obj, bag, cc);
				obj.name = this.m_Name.GetValueFromBag(bag, cc);
				obj.title = this.m_Text.GetValueFromBag(bag, cc);
				obj.visible = this.m_Visible.GetValueFromBag(bag, cc);
				obj.width = Column.UxmlObjectTraits<T>.ParseLength(this.m_Width.GetValueFromBag(bag, cc), default(Length));
				obj.maxWidth = Column.UxmlObjectTraits<T>.ParseLength(this.m_MaxWidth.GetValueFromBag(bag, cc), new Length(8388608f));
				obj.minWidth = Column.UxmlObjectTraits<T>.ParseLength(this.m_MinWidth.GetValueFromBag(bag, cc), new Length(35f));
				obj.sortable = this.m_Sortable.GetValueFromBag(bag, cc);
				obj.stretchable = this.m_Stretch.GetValueFromBag(bag, cc);
				obj.optional = this.m_Optional.GetValueFromBag(bag, cc);
				obj.resizable = this.m_Resizable.GetValueFromBag(bag, cc);
				obj.bindingPath = this.m_BindingPath.GetValueFromBag(bag, cc);
				string headerTemplateId = this.m_HeaderTemplateId.GetValueFromBag(bag, cc);
				bool flag = !string.IsNullOrEmpty(headerTemplateId);
				if (flag)
				{
					Column.UxmlObjectTraits<T>.<>c__DisplayClass14_0 CS$<>8__locals1 = new Column.UxmlObjectTraits<T>.<>c__DisplayClass14_0();
					Column.UxmlObjectTraits<T>.<>c__DisplayClass14_0 CS$<>8__locals2 = CS$<>8__locals1;
					VisualTreeAsset visualTreeAsset = cc.visualTreeAsset;
					CS$<>8__locals2.asset = ((visualTreeAsset != null) ? visualTreeAsset.ResolveTemplate(headerTemplateId) : null);
					obj.makeHeader = delegate
					{
						bool flag3 = CS$<>8__locals1.asset != null;
						VisualElement visualElement;
						if (flag3)
						{
							visualElement = CS$<>8__locals1.asset.Instantiate();
						}
						else
						{
							visualElement = new Label(BaseVerticalCollectionView.k_InvalidTemplateError);
						}
						return visualElement;
					};
				}
				string cellTemplateId = this.m_CellTemplateId.GetValueFromBag(bag, cc);
				bool flag2 = !string.IsNullOrEmpty(cellTemplateId);
				if (flag2)
				{
					Column.UxmlObjectTraits<T>.<>c__DisplayClass14_1 CS$<>8__locals3 = new Column.UxmlObjectTraits<T>.<>c__DisplayClass14_1();
					Column.UxmlObjectTraits<T>.<>c__DisplayClass14_1 CS$<>8__locals4 = CS$<>8__locals3;
					VisualTreeAsset visualTreeAsset2 = cc.visualTreeAsset;
					CS$<>8__locals4.asset = ((visualTreeAsset2 != null) ? visualTreeAsset2.ResolveTemplate(cellTemplateId) : null);
					obj.makeCell = delegate
					{
						bool flag4 = CS$<>8__locals3.asset != null;
						VisualElement visualElement2;
						if (flag4)
						{
							visualElement2 = CS$<>8__locals3.asset.Instantiate();
						}
						else
						{
							visualElement2 = new Label(BaseVerticalCollectionView.k_InvalidTemplateError);
						}
						return visualElement2;
					};
				}
			}

			// Token: 0x04000510 RID: 1296
			private UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
			{
				name = "name"
			};

			// Token: 0x04000511 RID: 1297
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "title"
			};

			// Token: 0x04000512 RID: 1298
			private UxmlBoolAttributeDescription m_Visible = new UxmlBoolAttributeDescription
			{
				name = "visible",
				defaultValue = true
			};

			// Token: 0x04000513 RID: 1299
			private UxmlStringAttributeDescription m_Width = new UxmlStringAttributeDescription
			{
				name = "width"
			};

			// Token: 0x04000514 RID: 1300
			private UxmlStringAttributeDescription m_MinWidth = new UxmlStringAttributeDescription
			{
				name = "min-width"
			};

			// Token: 0x04000515 RID: 1301
			private UxmlStringAttributeDescription m_MaxWidth = new UxmlStringAttributeDescription
			{
				name = "max-width"
			};

			// Token: 0x04000516 RID: 1302
			private UxmlBoolAttributeDescription m_Stretch = new UxmlBoolAttributeDescription
			{
				name = "stretchable"
			};

			// Token: 0x04000517 RID: 1303
			private UxmlBoolAttributeDescription m_Sortable = new UxmlBoolAttributeDescription
			{
				name = "sortable",
				defaultValue = true
			};

			// Token: 0x04000518 RID: 1304
			private UxmlBoolAttributeDescription m_Optional = new UxmlBoolAttributeDescription
			{
				name = "optional",
				defaultValue = true
			};

			// Token: 0x04000519 RID: 1305
			private UxmlBoolAttributeDescription m_Resizable = new UxmlBoolAttributeDescription
			{
				name = "resizable",
				defaultValue = true
			};

			// Token: 0x0400051A RID: 1306
			private UxmlStringAttributeDescription m_HeaderTemplateId = new UxmlStringAttributeDescription
			{
				name = "header-template"
			};

			// Token: 0x0400051B RID: 1307
			private UxmlStringAttributeDescription m_CellTemplateId = new UxmlStringAttributeDescription
			{
				name = "cell-template"
			};

			// Token: 0x0400051C RID: 1308
			private UxmlStringAttributeDescription m_BindingPath = new UxmlStringAttributeDescription
			{
				name = "binding-path"
			};
		}
	}
}
