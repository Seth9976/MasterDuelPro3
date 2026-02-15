using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace UnityEngine.Rendering
{
	// Token: 0x0200009C RID: 156
	public class DebugUI
	{
		// Token: 0x0200009D RID: 157
		public class Container : DebugUI.Widget, DebugUI.IContainer
		{
			// Token: 0x17000062 RID: 98
			// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000E8FD File Offset: 0x0000CAFD
			internal bool hideDisplayName
			{
				get
				{
					return string.IsNullOrEmpty(base.displayName) || base.displayName.StartsWith("#");
				}
			}

			// Token: 0x17000063 RID: 99
			// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000E91E File Offset: 0x0000CB1E
			// (set) Token: 0x06000609 RID: 1545 RVA: 0x0000E926 File Offset: 0x0000CB26
			public ObservableList<DebugUI.Widget> children { get; private set; }

			// Token: 0x17000064 RID: 100
			// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000E92F File Offset: 0x0000CB2F
			// (set) Token: 0x0600060B RID: 1547 RVA: 0x0000E938 File Offset: 0x0000CB38
			public override DebugUI.Panel panel
			{
				get
				{
					return this.m_Panel;
				}
				internal set
				{
					if (value != null && value.flags.HasFlag(DebugUI.Flags.FrequentlyUsed))
					{
						return;
					}
					this.m_Panel = value;
					int numChildren = this.children.Count;
					for (int i = 0; i < numChildren; i++)
					{
						this.children[i].panel = value;
					}
				}
			}

			// Token: 0x0600060C RID: 1548 RVA: 0x0000E993 File Offset: 0x0000CB93
			public Container()
				: this(string.Empty, new ObservableList<DebugUI.Widget>())
			{
			}

			// Token: 0x0600060D RID: 1549 RVA: 0x0000E9A5 File Offset: 0x0000CBA5
			public Container(string id)
				: this("#" + id, new ObservableList<DebugUI.Widget>())
			{
			}

			// Token: 0x0600060E RID: 1550 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
			public Container(string displayName, ObservableList<DebugUI.Widget> children)
			{
				base.displayName = displayName;
				this.children = children;
				children.ItemAdded += this.OnItemAdded;
				children.ItemRemoved += this.OnItemRemoved;
				for (int i = 0; i < this.children.Count; i++)
				{
					this.OnItemAdded(this.children, new ListChangedEventArgs<DebugUI.Widget>(i, this.children[i]));
				}
			}

			// Token: 0x0600060F RID: 1551 RVA: 0x0000EA3C File Offset: 0x0000CC3C
			internal override void GenerateQueryPath()
			{
				base.GenerateQueryPath();
				int numChildren = this.children.Count;
				for (int i = 0; i < numChildren; i++)
				{
					this.children[i].GenerateQueryPath();
				}
			}

			// Token: 0x06000610 RID: 1552 RVA: 0x0000EA78 File Offset: 0x0000CC78
			protected virtual void OnItemAdded(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = this.m_Panel;
					e.item.parent = this;
				}
				if (this.m_Panel != null)
				{
					this.m_Panel.SetDirty();
				}
			}

			// Token: 0x06000611 RID: 1553 RVA: 0x0000EAB2 File Offset: 0x0000CCB2
			protected virtual void OnItemRemoved(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = null;
					e.item.parent = null;
				}
				if (this.m_Panel != null)
				{
					this.m_Panel.SetDirty();
				}
			}

			// Token: 0x06000612 RID: 1554 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
			public override int GetHashCode()
			{
				int hash = 17;
				hash = hash * 23 + base.queryPath.GetHashCode();
				hash = hash * 23 + base.isHidden.GetHashCode();
				int numChildren = this.children.Count;
				for (int i = 0; i < numChildren; i++)
				{
					hash = hash * 23 + this.children[i].GetHashCode();
				}
				return hash;
			}

			// Token: 0x04000209 RID: 521
			private const string k_IDToken = "#";
		}

		// Token: 0x0200009E RID: 158
		public class Foldout : DebugUI.Container, DebugUI.IValueField
		{
			// Token: 0x17000065 RID: 101
			// (get) Token: 0x06000613 RID: 1555 RVA: 0x000090C6 File Offset: 0x000072C6
			public bool isReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000066 RID: 102
			// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000EB4D File Offset: 0x0000CD4D
			// (set) Token: 0x06000615 RID: 1557 RVA: 0x0000EB55 File Offset: 0x0000CD55
			public string[] columnLabels
			{
				get
				{
					return this.m_ColumnLabels;
				}
				set
				{
					this.m_ColumnLabels = value;
					this.m_Dirty = true;
				}
			}

			// Token: 0x17000067 RID: 103
			// (get) Token: 0x06000616 RID: 1558 RVA: 0x0000EB65 File Offset: 0x0000CD65
			// (set) Token: 0x06000617 RID: 1559 RVA: 0x0000EB6D File Offset: 0x0000CD6D
			public string[] columnTooltips
			{
				get
				{
					return this.m_ColumnTooltips;
				}
				set
				{
					this.m_ColumnTooltips = value;
					this.m_Dirty = true;
				}
			}

			// Token: 0x17000068 RID: 104
			// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000EB80 File Offset: 0x0000CD80
			internal List<GUIContent> rowContents
			{
				get
				{
					if (this.m_Dirty)
					{
						if (this.m_ColumnTooltips == null)
						{
							this.m_ColumnTooltips = new string[this.m_ColumnLabels.Length];
							Array.Fill<string>(this.columnTooltips, string.Empty);
						}
						else if (this.m_ColumnTooltips.Length != this.m_ColumnLabels.Length)
						{
							throw new Exception("Dimension for labels and tooltips on Foldout - " + base.displayName + ", do not match");
						}
						this.m_RowContents.Clear();
						for (int i = 0; i < this.m_ColumnLabels.Length; i++)
						{
							string label = this.columnLabels[i] ?? string.Empty;
							string tooltip = this.m_ColumnTooltips[i] ?? string.Empty;
							this.m_RowContents.Add(new GUIContent(label, tooltip));
						}
						this.m_Dirty = false;
					}
					return this.m_RowContents;
				}
			}

			// Token: 0x06000619 RID: 1561 RVA: 0x0000EC53 File Offset: 0x0000CE53
			public Foldout()
			{
			}

			// Token: 0x0600061A RID: 1562 RVA: 0x0000EC66 File Offset: 0x0000CE66
			public Foldout(string displayName, ObservableList<DebugUI.Widget> children, string[] columnLabels = null, string[] columnTooltips = null)
				: base(displayName, children)
			{
				this.columnLabels = columnLabels;
				this.columnTooltips = columnTooltips;
			}

			// Token: 0x0600061B RID: 1563 RVA: 0x0000EC8A File Offset: 0x0000CE8A
			public bool GetValue()
			{
				return this.opened;
			}

			// Token: 0x0600061C RID: 1564 RVA: 0x0000EC92 File Offset: 0x0000CE92
			object DebugUI.IValueField.GetValue()
			{
				return this.GetValue();
			}

			// Token: 0x0600061D RID: 1565 RVA: 0x0000EC9F File Offset: 0x0000CE9F
			public void SetValue(object value)
			{
				this.SetValue((bool)value);
			}

			// Token: 0x0600061E RID: 1566 RVA: 0x000093A8 File Offset: 0x000075A8
			public object ValidateValue(object value)
			{
				return value;
			}

			// Token: 0x0600061F RID: 1567 RVA: 0x0000ECAD File Offset: 0x0000CEAD
			public void SetValue(bool value)
			{
				this.opened = value;
			}

			// Token: 0x0400020B RID: 523
			public bool opened;

			// Token: 0x0400020C RID: 524
			public bool isHeader;

			// Token: 0x0400020D RID: 525
			public List<DebugUI.Foldout.ContextMenuItem> contextMenuItems;

			// Token: 0x0400020E RID: 526
			private bool m_Dirty;

			// Token: 0x0400020F RID: 527
			private string[] m_ColumnLabels;

			// Token: 0x04000210 RID: 528
			private string[] m_ColumnTooltips;

			// Token: 0x04000211 RID: 529
			private List<GUIContent> m_RowContents = new List<GUIContent>();

			// Token: 0x0200009F RID: 159
			public struct ContextMenuItem
			{
				// Token: 0x04000212 RID: 530
				public string displayName;

				// Token: 0x04000213 RID: 531
				public Action action;
			}
		}

		// Token: 0x020000A0 RID: 160
		public class HBox : DebugUI.Container
		{
			// Token: 0x06000620 RID: 1568 RVA: 0x0000ECB6 File Offset: 0x0000CEB6
			public HBox()
			{
				base.displayName = "HBox";
			}
		}

		// Token: 0x020000A1 RID: 161
		public class VBox : DebugUI.Container
		{
			// Token: 0x06000621 RID: 1569 RVA: 0x0000ECC9 File Offset: 0x0000CEC9
			public VBox()
			{
				base.displayName = "VBox";
			}
		}

		// Token: 0x020000A2 RID: 162
		public class Table : DebugUI.Container
		{
			// Token: 0x06000622 RID: 1570 RVA: 0x0000ECDC File Offset: 0x0000CEDC
			public Table()
			{
				base.displayName = "Array";
			}

			// Token: 0x06000623 RID: 1571 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
			public void SetColumnVisibility(int index, bool visible)
			{
				bool[] columns = this.VisibleColumns;
				if (index < 0 || index > columns.Length)
				{
					return;
				}
				columns[index] = visible;
			}

			// Token: 0x06000624 RID: 1572 RVA: 0x0000ED14 File Offset: 0x0000CF14
			public bool GetColumnVisibility(int index)
			{
				bool[] columns = this.VisibleColumns;
				return index >= 0 && index <= columns.Length && columns[index];
			}

			// Token: 0x17000069 RID: 105
			// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000ED38 File Offset: 0x0000CF38
			public bool[] VisibleColumns
			{
				get
				{
					if (this.m_Header != null)
					{
						return this.m_Header;
					}
					int columnCount = 0;
					if (base.children.Count != 0)
					{
						columnCount = ((DebugUI.Container)base.children[0]).children.Count;
						for (int i = 1; i < base.children.Count; i++)
						{
							if (((DebugUI.Container)base.children[i]).children.Count != columnCount)
							{
								Debug.LogError("All rows must have the same number of children.");
								return null;
							}
						}
					}
					this.m_Header = new bool[columnCount];
					for (int j = 0; j < columnCount; j++)
					{
						this.m_Header[j] = true;
					}
					return this.m_Header;
				}
			}

			// Token: 0x06000626 RID: 1574 RVA: 0x0000EDE6 File Offset: 0x0000CFE6
			protected override void OnItemAdded(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				base.OnItemAdded(sender, e);
				this.m_Header = null;
			}

			// Token: 0x06000627 RID: 1575 RVA: 0x0000EDF7 File Offset: 0x0000CFF7
			protected override void OnItemRemoved(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				base.OnItemRemoved(sender, e);
				this.m_Header = null;
			}

			// Token: 0x04000214 RID: 532
			private static GUIStyle columnHeaderStyle = new GUIStyle
			{
				alignment = TextAnchor.MiddleCenter
			};

			// Token: 0x04000215 RID: 533
			public bool isReadOnly;

			// Token: 0x04000216 RID: 534
			private bool[] m_Header;

			// Token: 0x020000A3 RID: 163
			public class Row : DebugUI.Foldout
			{
				// Token: 0x06000629 RID: 1577 RVA: 0x0000EE1B File Offset: 0x0000D01B
				public Row()
				{
					base.displayName = "Row";
				}
			}
		}

		// Token: 0x020000A4 RID: 164
		public abstract class Field<T> : DebugUI.Widget, DebugUI.IValueField
		{
			// Token: 0x1700006A RID: 106
			// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000EE2E File Offset: 0x0000D02E
			// (set) Token: 0x0600062B RID: 1579 RVA: 0x0000EE36 File Offset: 0x0000D036
			public Func<T> getter { get; set; }

			// Token: 0x1700006B RID: 107
			// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000EE3F File Offset: 0x0000D03F
			// (set) Token: 0x0600062D RID: 1581 RVA: 0x0000EE47 File Offset: 0x0000D047
			public Action<T> setter { get; set; }

			// Token: 0x0600062E RID: 1582 RVA: 0x0000EE50 File Offset: 0x0000D050
			object DebugUI.IValueField.ValidateValue(object value)
			{
				return this.ValidateValue((T)((object)value));
			}

			// Token: 0x0600062F RID: 1583 RVA: 0x000093A8 File Offset: 0x000075A8
			public virtual T ValidateValue(T value)
			{
				return value;
			}

			// Token: 0x06000630 RID: 1584 RVA: 0x0000EE63 File Offset: 0x0000D063
			object DebugUI.IValueField.GetValue()
			{
				return this.GetValue();
			}

			// Token: 0x06000631 RID: 1585 RVA: 0x0000EE70 File Offset: 0x0000D070
			public T GetValue()
			{
				return this.getter();
			}

			// Token: 0x06000632 RID: 1586 RVA: 0x0000EE7D File Offset: 0x0000D07D
			public void SetValue(object value)
			{
				this.SetValue((T)((object)value));
			}

			// Token: 0x06000633 RID: 1587 RVA: 0x0000EE8C File Offset: 0x0000D08C
			public virtual void SetValue(T value)
			{
				T v = this.ValidateValue(value);
				if (v == null || !v.Equals(this.getter()))
				{
					this.setter(v);
					Action<DebugUI.Field<T>, T> action = this.onValueChanged;
					if (action == null)
					{
						return;
					}
					action(this, v);
				}
			}

			// Token: 0x04000219 RID: 537
			public Action<DebugUI.Field<T>, T> onValueChanged;
		}

		// Token: 0x020000A5 RID: 165
		public class BoolField : DebugUI.Field<bool>
		{
		}

		// Token: 0x020000A6 RID: 166
		public class HistoryBoolField : DebugUI.BoolField
		{
			// Token: 0x1700006C RID: 108
			// (get) Token: 0x06000636 RID: 1590 RVA: 0x0000EEF6 File Offset: 0x0000D0F6
			// (set) Token: 0x06000637 RID: 1591 RVA: 0x0000EEFE File Offset: 0x0000D0FE
			public Func<bool>[] historyGetter { get; set; }

			// Token: 0x1700006D RID: 109
			// (get) Token: 0x06000638 RID: 1592 RVA: 0x0000EF07 File Offset: 0x0000D107
			public int historyDepth
			{
				get
				{
					Func<bool>[] historyGetter = this.historyGetter;
					if (historyGetter == null)
					{
						return 0;
					}
					return historyGetter.Length;
				}
			}

			// Token: 0x06000639 RID: 1593 RVA: 0x0000EF17 File Offset: 0x0000D117
			public bool GetHistoryValue(int historyIndex)
			{
				return this.historyGetter[historyIndex]();
			}
		}

		// Token: 0x020000A7 RID: 167
		public class IntField : DebugUI.Field<int>
		{
			// Token: 0x0600063B RID: 1595 RVA: 0x0000EF2E File Offset: 0x0000D12E
			public override int ValidateValue(int value)
			{
				if (this.min != null)
				{
					value = Mathf.Max(value, this.min());
				}
				if (this.max != null)
				{
					value = Mathf.Min(value, this.max());
				}
				return value;
			}

			// Token: 0x0400021B RID: 539
			public Func<int> min;

			// Token: 0x0400021C RID: 540
			public Func<int> max;

			// Token: 0x0400021D RID: 541
			public int incStep = 1;

			// Token: 0x0400021E RID: 542
			public int intStepMult = 10;
		}

		// Token: 0x020000A8 RID: 168
		public class UIntField : DebugUI.Field<uint>
		{
			// Token: 0x0600063D RID: 1597 RVA: 0x0000EF7E File Offset: 0x0000D17E
			public override uint ValidateValue(uint value)
			{
				if (this.min != null)
				{
					value = (uint)Mathf.Max((int)value, (int)this.min());
				}
				if (this.max != null)
				{
					value = (uint)Mathf.Min((int)value, (int)this.max());
				}
				return value;
			}

			// Token: 0x0400021F RID: 543
			public Func<uint> min;

			// Token: 0x04000220 RID: 544
			public Func<uint> max;

			// Token: 0x04000221 RID: 545
			public uint incStep = 1U;

			// Token: 0x04000222 RID: 546
			public uint intStepMult = 10U;
		}

		// Token: 0x020000A9 RID: 169
		public class FloatField : DebugUI.Field<float>
		{
			// Token: 0x0600063F RID: 1599 RVA: 0x0000EFCE File Offset: 0x0000D1CE
			public override float ValidateValue(float value)
			{
				if (this.min != null)
				{
					value = Mathf.Max(value, this.min());
				}
				if (this.max != null)
				{
					value = Mathf.Min(value, this.max());
				}
				return value;
			}

			// Token: 0x04000223 RID: 547
			public Func<float> min;

			// Token: 0x04000224 RID: 548
			public Func<float> max;

			// Token: 0x04000225 RID: 549
			public float incStep = 0.1f;

			// Token: 0x04000226 RID: 550
			public float incStepMult = 10f;

			// Token: 0x04000227 RID: 551
			public int decimals = 3;
		}

		// Token: 0x020000AA RID: 170
		public abstract class EnumField<T> : DebugUI.Field<T>
		{
			// Token: 0x1700006E RID: 110
			// (get) Token: 0x06000641 RID: 1601 RVA: 0x0000F02C File Offset: 0x0000D22C
			// (set) Token: 0x06000642 RID: 1602 RVA: 0x0000F034 File Offset: 0x0000D234
			public int[] enumValues
			{
				get
				{
					return this.m_EnumValues;
				}
				set
				{
					int? num = ((value != null) ? new int?(value.Distinct<int>().Count<int>()) : null);
					int? num2 = ((value != null) ? new int?(value.Count<int>()) : null);
					if (!((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null))))
					{
						Debug.LogWarning(base.displayName + " - The values of the enum are duplicated, this might lead to a errors displaying the enum");
					}
					this.m_EnumValues = value;
				}
			}

			// Token: 0x06000643 RID: 1603 RVA: 0x0000F0BC File Offset: 0x0000D2BC
			protected void AutoFillFromType(Type enumType)
			{
				if (enumType == null || !enumType.IsEnum)
				{
					throw new ArgumentException("enumType must not be null and it must be an Enum type");
				}
				List<GUIContent> tmpNames;
				using (ListPool<GUIContent>.Get(out tmpNames))
				{
					List<int> tmpValues;
					using (ListPool<int>.Get(out tmpValues))
					{
						foreach (FieldInfo fieldInfo2 in from fieldInfo in enumType.GetFields(BindingFlags.Static | BindingFlags.Public)
							where !fieldInfo.IsDefined(typeof(ObsoleteAttribute)) && !fieldInfo.IsDefined(typeof(HideInInspector))
							select fieldInfo)
						{
							InspectorNameAttribute description = fieldInfo2.GetCustomAttribute<InspectorNameAttribute>();
							GUIContent displayName = new GUIContent((description == null) ? DebugUI.EnumField<T>.s_NicifyRegEx.Replace(fieldInfo2.Name, "$1 ") : description.displayName);
							tmpNames.Add(displayName);
							tmpValues.Add((int)Enum.Parse(enumType, fieldInfo2.Name));
						}
						this.enumNames = tmpNames.ToArray();
						this.enumValues = tmpValues.ToArray();
					}
				}
			}

			// Token: 0x04000228 RID: 552
			public GUIContent[] enumNames;

			// Token: 0x04000229 RID: 553
			private int[] m_EnumValues;

			// Token: 0x0400022A RID: 554
			private static Regex s_NicifyRegEx = new Regex("([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))", RegexOptions.Compiled);
		}

		// Token: 0x020000AC RID: 172
		public class EnumField : DebugUI.EnumField<int>
		{
			// Token: 0x1700006F RID: 111
			// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000F24C File Offset: 0x0000D44C
			internal int[] indexes
			{
				get
				{
					int[] array;
					if ((array = this.m_Indexes) == null)
					{
						int num = 0;
						GUIContent[] enumNames = this.enumNames;
						array = (this.m_Indexes = Enumerable.Range(num, (enumNames != null) ? enumNames.Length : 0).ToArray<int>());
					}
					return array;
				}
			}

			// Token: 0x17000070 RID: 112
			// (get) Token: 0x0600064A RID: 1610 RVA: 0x0000F286 File Offset: 0x0000D486
			// (set) Token: 0x0600064B RID: 1611 RVA: 0x0000F28E File Offset: 0x0000D48E
			public Func<int> getIndex { get; set; }

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x0600064C RID: 1612 RVA: 0x0000F297 File Offset: 0x0000D497
			// (set) Token: 0x0600064D RID: 1613 RVA: 0x0000F29F File Offset: 0x0000D49F
			public Action<int> setIndex { get; set; }

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x0600064E RID: 1614 RVA: 0x0000F2A8 File Offset: 0x0000D4A8
			// (set) Token: 0x0600064F RID: 1615 RVA: 0x0000F2B5 File Offset: 0x0000D4B5
			public int currentIndex
			{
				get
				{
					return this.getIndex();
				}
				set
				{
					this.setIndex(value);
				}
			}

			// Token: 0x17000073 RID: 115
			// (set) Token: 0x06000650 RID: 1616 RVA: 0x0000F2C3 File Offset: 0x0000D4C3
			public Type autoEnum
			{
				set
				{
					base.AutoFillFromType(value);
					this.InitQuickSeparators();
				}
			}

			// Token: 0x06000651 RID: 1617 RVA: 0x0000F2D4 File Offset: 0x0000D4D4
			internal void InitQuickSeparators()
			{
				IEnumerable<string> enumNamesPrefix = this.enumNames.Select(delegate(GUIContent x)
				{
					string[] splitted = x.text.Split('/', StringSplitOptions.None);
					if (splitted.Length == 1)
					{
						return "";
					}
					return splitted[0];
				});
				this.quickSeparators = new int[enumNamesPrefix.Distinct<string>().Count<string>()];
				string lastPrefix = null;
				int i = 0;
				int wholeNameIndex = 0;
				while (i < this.quickSeparators.Length)
				{
					string currentTestedPrefix = enumNamesPrefix.ElementAt(wholeNameIndex);
					while (lastPrefix == currentTestedPrefix)
					{
						currentTestedPrefix = enumNamesPrefix.ElementAt(++wholeNameIndex);
					}
					lastPrefix = currentTestedPrefix;
					this.quickSeparators[i] = wholeNameIndex++;
					i++;
				}
			}

			// Token: 0x06000652 RID: 1618 RVA: 0x0000F36C File Offset: 0x0000D56C
			public override void SetValue(int value)
			{
				int validValue = this.ValidateValue(value);
				int newCurrentIndex = Array.IndexOf<int>(base.enumValues, validValue);
				if (this.currentIndex != newCurrentIndex && !validValue.Equals(base.getter()))
				{
					base.setter(validValue);
					Action<DebugUI.Field<int>, int> onValueChanged = this.onValueChanged;
					if (onValueChanged != null)
					{
						onValueChanged(this, validValue);
					}
					if (newCurrentIndex > -1)
					{
						this.currentIndex = newCurrentIndex;
					}
				}
			}

			// Token: 0x0400022D RID: 557
			internal int[] quickSeparators;

			// Token: 0x0400022E RID: 558
			private int[] m_Indexes;
		}

		// Token: 0x020000AE RID: 174
		public class ObjectPopupField : DebugUI.Field<Object>
		{
			// Token: 0x17000074 RID: 116
			// (get) Token: 0x06000657 RID: 1623 RVA: 0x0000F417 File Offset: 0x0000D617
			// (set) Token: 0x06000658 RID: 1624 RVA: 0x0000F41F File Offset: 0x0000D61F
			public Func<IEnumerable<Object>> getObjects { get; set; }
		}

		// Token: 0x020000AF RID: 175
		public class HistoryEnumField : DebugUI.EnumField
		{
			// Token: 0x17000075 RID: 117
			// (get) Token: 0x0600065A RID: 1626 RVA: 0x0000F430 File Offset: 0x0000D630
			// (set) Token: 0x0600065B RID: 1627 RVA: 0x0000F438 File Offset: 0x0000D638
			public Func<int>[] historyIndexGetter { get; set; }

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x0600065C RID: 1628 RVA: 0x0000F441 File Offset: 0x0000D641
			public int historyDepth
			{
				get
				{
					Func<int>[] historyIndexGetter = this.historyIndexGetter;
					if (historyIndexGetter == null)
					{
						return 0;
					}
					return historyIndexGetter.Length;
				}
			}

			// Token: 0x0600065D RID: 1629 RVA: 0x0000F451 File Offset: 0x0000D651
			public int GetHistoryValue(int historyIndex)
			{
				return this.historyIndexGetter[historyIndex]();
			}
		}

		// Token: 0x020000B0 RID: 176
		public class BitField : DebugUI.EnumField<Enum>
		{
			// Token: 0x17000077 RID: 119
			// (get) Token: 0x0600065F RID: 1631 RVA: 0x0000F468 File Offset: 0x0000D668
			// (set) Token: 0x06000660 RID: 1632 RVA: 0x0000F470 File Offset: 0x0000D670
			public Type enumType
			{
				get
				{
					return this.m_EnumType;
				}
				set
				{
					this.m_EnumType = value;
					base.AutoFillFromType(value);
				}
			}

			// Token: 0x04000235 RID: 565
			private Type m_EnumType;
		}

		// Token: 0x020000B1 RID: 177
		public class MaskField : DebugUI.EnumField<uint>
		{
			// Token: 0x06000662 RID: 1634 RVA: 0x0000F488 File Offset: 0x0000D688
			public void Fill(string[] names)
			{
				List<GUIContent> tmpNames;
				using (ListPool<GUIContent>.Get(out tmpNames))
				{
					List<int> tmpValues;
					using (ListPool<int>.Get(out tmpValues))
					{
						for (int i = 0; i < names.Length; i++)
						{
							tmpNames.Add(new GUIContent(names[i]));
							tmpValues.Add(i);
						}
						this.enumNames = tmpNames.ToArray();
						base.enumValues = tmpValues.ToArray();
					}
				}
			}

			// Token: 0x06000663 RID: 1635 RVA: 0x0000F520 File Offset: 0x0000D720
			public override void SetValue(uint value)
			{
				uint validValue = this.ValidateValue(value);
				if (!validValue.Equals(base.getter()))
				{
					base.setter(validValue);
					Action<DebugUI.Field<uint>, uint> onValueChanged = this.onValueChanged;
					if (onValueChanged == null)
					{
						return;
					}
					onValueChanged(this, validValue);
				}
			}
		}

		// Token: 0x020000B2 RID: 178
		public class ColorField : DebugUI.Field<Color>
		{
			// Token: 0x06000665 RID: 1637 RVA: 0x0000F570 File Offset: 0x0000D770
			public override Color ValidateValue(Color value)
			{
				if (!this.hdr)
				{
					value.r = Mathf.Clamp01(value.r);
					value.g = Mathf.Clamp01(value.g);
					value.b = Mathf.Clamp01(value.b);
					value.a = Mathf.Clamp01(value.a);
				}
				return value;
			}

			// Token: 0x04000236 RID: 566
			public bool hdr;

			// Token: 0x04000237 RID: 567
			public bool showAlpha = true;

			// Token: 0x04000238 RID: 568
			public bool showPicker = true;

			// Token: 0x04000239 RID: 569
			public float incStep = 0.025f;

			// Token: 0x0400023A RID: 570
			public float incStepMult = 5f;

			// Token: 0x0400023B RID: 571
			public int decimals = 3;
		}

		// Token: 0x020000B3 RID: 179
		public class Vector2Field : DebugUI.Field<Vector2>
		{
			// Token: 0x0400023C RID: 572
			public float incStep = 0.025f;

			// Token: 0x0400023D RID: 573
			public float incStepMult = 10f;

			// Token: 0x0400023E RID: 574
			public int decimals = 3;
		}

		// Token: 0x020000B4 RID: 180
		public class Vector3Field : DebugUI.Field<Vector3>
		{
			// Token: 0x0400023F RID: 575
			public float incStep = 0.025f;

			// Token: 0x04000240 RID: 576
			public float incStepMult = 10f;

			// Token: 0x04000241 RID: 577
			public int decimals = 3;
		}

		// Token: 0x020000B5 RID: 181
		public class Vector4Field : DebugUI.Field<Vector4>
		{
			// Token: 0x04000242 RID: 578
			public float incStep = 0.025f;

			// Token: 0x04000243 RID: 579
			public float incStepMult = 10f;

			// Token: 0x04000244 RID: 580
			public int decimals = 3;
		}

		// Token: 0x020000B6 RID: 182
		public class ObjectField : DebugUI.Field<Object>
		{
			// Token: 0x04000245 RID: 581
			public Type type = typeof(Object);
		}

		// Token: 0x020000B7 RID: 183
		public class ObjectListField : DebugUI.Field<Object[]>
		{
			// Token: 0x04000246 RID: 582
			public Type type = typeof(Object);
		}

		// Token: 0x020000B8 RID: 184
		public class MessageBox : DebugUI.Widget
		{
			// Token: 0x17000078 RID: 120
			// (get) Token: 0x0600066C RID: 1644 RVA: 0x0000F6A0 File Offset: 0x0000D8A0
			public string message
			{
				get
				{
					if (this.messageCallback != null)
					{
						return this.messageCallback();
					}
					return base.displayName;
				}
			}

			// Token: 0x04000247 RID: 583
			public DebugUI.MessageBox.Style style;

			// Token: 0x04000248 RID: 584
			public Func<string> messageCallback;

			// Token: 0x020000B9 RID: 185
			public enum Style
			{
				// Token: 0x0400024A RID: 586
				Info,
				// Token: 0x0400024B RID: 587
				Warning,
				// Token: 0x0400024C RID: 588
				Error
			}
		}

		// Token: 0x020000BA RID: 186
		public class RuntimeDebugShadersMessageBox : DebugUI.MessageBox
		{
			// Token: 0x0600066E RID: 1646 RVA: 0x0000F6BC File Offset: 0x0000D8BC
			public RuntimeDebugShadersMessageBox()
			{
				base.displayName = "Warning: the debug shader variants are missing. Ensure that the \"Strip Runtime Debug Shaders\" option is disabled in the SRP Graphics Settings.";
				this.style = DebugUI.MessageBox.Style.Warning;
				this.isHiddenCallback = delegate
				{
					ShaderStrippingSetting shaderStrippingSetting;
					return !GraphicsSettings.TryGetRenderPipelineSettings<ShaderStrippingSetting>(out shaderStrippingSetting) || !shaderStrippingSetting.stripRuntimeDebugShaders;
				};
			}
		}

		// Token: 0x020000BC RID: 188
		public class Panel : DebugUI.IContainer, IComparable<DebugUI.Panel>
		{
			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000672 RID: 1650 RVA: 0x0000F729 File Offset: 0x0000D929
			// (set) Token: 0x06000673 RID: 1651 RVA: 0x0000F731 File Offset: 0x0000D931
			public DebugUI.Flags flags { get; set; }

			// Token: 0x1700007A RID: 122
			// (get) Token: 0x06000674 RID: 1652 RVA: 0x0000F73A File Offset: 0x0000D93A
			// (set) Token: 0x06000675 RID: 1653 RVA: 0x0000F742 File Offset: 0x0000D942
			public string displayName { get; set; }

			// Token: 0x1700007B RID: 123
			// (get) Token: 0x06000676 RID: 1654 RVA: 0x0000F74B File Offset: 0x0000D94B
			// (set) Token: 0x06000677 RID: 1655 RVA: 0x0000F753 File Offset: 0x0000D953
			public int groupIndex { get; set; }

			// Token: 0x1700007C RID: 124
			// (get) Token: 0x06000678 RID: 1656 RVA: 0x0000F75C File Offset: 0x0000D95C
			public string queryPath
			{
				get
				{
					return this.displayName;
				}
			}

			// Token: 0x1700007D RID: 125
			// (get) Token: 0x06000679 RID: 1657 RVA: 0x0000F764 File Offset: 0x0000D964
			public bool isEditorOnly
			{
				get
				{
					return (this.flags & DebugUI.Flags.EditorOnly) > DebugUI.Flags.None;
				}
			}

			// Token: 0x1700007E RID: 126
			// (get) Token: 0x0600067A RID: 1658 RVA: 0x0000F771 File Offset: 0x0000D971
			public bool isRuntimeOnly
			{
				get
				{
					return (this.flags & DebugUI.Flags.RuntimeOnly) > DebugUI.Flags.None;
				}
			}

			// Token: 0x1700007F RID: 127
			// (get) Token: 0x0600067B RID: 1659 RVA: 0x0000F77E File Offset: 0x0000D97E
			public bool isInactiveInEditor
			{
				get
				{
					return this.isRuntimeOnly && !Application.isPlaying;
				}
			}

			// Token: 0x17000080 RID: 128
			// (get) Token: 0x0600067C RID: 1660 RVA: 0x0000F792 File Offset: 0x0000D992
			public bool editorForceUpdate
			{
				get
				{
					return (this.flags & DebugUI.Flags.EditorForceUpdate) > DebugUI.Flags.None;
				}
			}

			// Token: 0x17000081 RID: 129
			// (get) Token: 0x0600067D RID: 1661 RVA: 0x0000F79F File Offset: 0x0000D99F
			// (set) Token: 0x0600067E RID: 1662 RVA: 0x0000F7A7 File Offset: 0x0000D9A7
			public ObservableList<DebugUI.Widget> children { get; private set; }

			// Token: 0x14000009 RID: 9
			// (add) Token: 0x0600067F RID: 1663 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
			// (remove) Token: 0x06000680 RID: 1664 RVA: 0x0000F7E8 File Offset: 0x0000D9E8
			public event Action<DebugUI.Panel> onSetDirty = delegate
			{
			};

			// Token: 0x06000681 RID: 1665 RVA: 0x0000F820 File Offset: 0x0000DA20
			public Panel()
			{
				this.children = new ObservableList<DebugUI.Widget>();
				this.children.ItemAdded += this.OnItemAdded;
				this.children.ItemRemoved += this.OnItemRemoved;
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x0000F893 File Offset: 0x0000DA93
			protected virtual void OnItemAdded(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = this;
					e.item.parent = this;
				}
				this.SetDirty();
			}

			// Token: 0x06000683 RID: 1667 RVA: 0x0000F8BB File Offset: 0x0000DABB
			protected virtual void OnItemRemoved(ObservableList<DebugUI.Widget> sender, ListChangedEventArgs<DebugUI.Widget> e)
			{
				if (e.item != null)
				{
					e.item.panel = null;
					e.item.parent = null;
				}
				this.SetDirty();
			}

			// Token: 0x06000684 RID: 1668 RVA: 0x0000F8E4 File Offset: 0x0000DAE4
			public void SetDirty()
			{
				int numChildren = this.children.Count;
				for (int i = 0; i < numChildren; i++)
				{
					this.children[i].GenerateQueryPath();
				}
				this.onSetDirty(this);
			}

			// Token: 0x06000685 RID: 1669 RVA: 0x0000F928 File Offset: 0x0000DB28
			public override int GetHashCode()
			{
				int hash = 17;
				hash = hash * 23 + this.displayName.GetHashCode();
				int numChildren = this.children.Count;
				for (int i = 0; i < numChildren; i++)
				{
					hash = hash * 23 + this.children[i].GetHashCode();
				}
				return hash;
			}

			// Token: 0x06000686 RID: 1670 RVA: 0x0000F97C File Offset: 0x0000DB7C
			int IComparable<DebugUI.Panel>.CompareTo(DebugUI.Panel other)
			{
				if (other != null)
				{
					return this.groupIndex.CompareTo(other.groupIndex);
				}
				return 1;
			}
		}

		// Token: 0x020000BE RID: 190
		[Flags]
		public enum Flags
		{
			// Token: 0x04000257 RID: 599
			None = 0,
			// Token: 0x04000258 RID: 600
			EditorOnly = 2,
			// Token: 0x04000259 RID: 601
			RuntimeOnly = 4,
			// Token: 0x0400025A RID: 602
			EditorForceUpdate = 8,
			// Token: 0x0400025B RID: 603
			FrequentlyUsed = 16
		}

		// Token: 0x020000BF RID: 191
		public abstract class Widget
		{
			// Token: 0x17000082 RID: 130
			// (get) Token: 0x0600068A RID: 1674 RVA: 0x0000E92F File Offset: 0x0000CB2F
			// (set) Token: 0x0600068B RID: 1675 RVA: 0x0000F9AE File Offset: 0x0000DBAE
			public virtual DebugUI.Panel panel
			{
				get
				{
					return this.m_Panel;
				}
				internal set
				{
					this.m_Panel = value;
				}
			}

			// Token: 0x17000083 RID: 131
			// (get) Token: 0x0600068C RID: 1676 RVA: 0x0000F9B7 File Offset: 0x0000DBB7
			// (set) Token: 0x0600068D RID: 1677 RVA: 0x0000F9BF File Offset: 0x0000DBBF
			public virtual DebugUI.IContainer parent
			{
				get
				{
					return this.m_Parent;
				}
				internal set
				{
					this.m_Parent = value;
				}
			}

			// Token: 0x17000084 RID: 132
			// (get) Token: 0x0600068E RID: 1678 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
			// (set) Token: 0x0600068F RID: 1679 RVA: 0x0000F9D0 File Offset: 0x0000DBD0
			public DebugUI.Flags flags { get; set; }

			// Token: 0x17000085 RID: 133
			// (get) Token: 0x06000690 RID: 1680 RVA: 0x0000F9D9 File Offset: 0x0000DBD9
			// (set) Token: 0x06000691 RID: 1681 RVA: 0x0000F9E1 File Offset: 0x0000DBE1
			public string displayName { get; set; }

			// Token: 0x17000086 RID: 134
			// (get) Token: 0x06000692 RID: 1682 RVA: 0x0000F9EA File Offset: 0x0000DBEA
			// (set) Token: 0x06000693 RID: 1683 RVA: 0x0000F9F2 File Offset: 0x0000DBF2
			public string tooltip { get; set; }

			// Token: 0x17000087 RID: 135
			// (get) Token: 0x06000694 RID: 1684 RVA: 0x0000F9FB File Offset: 0x0000DBFB
			// (set) Token: 0x06000695 RID: 1685 RVA: 0x0000FA03 File Offset: 0x0000DC03
			public string queryPath { get; private set; }

			// Token: 0x17000088 RID: 136
			// (get) Token: 0x06000696 RID: 1686 RVA: 0x0000FA0C File Offset: 0x0000DC0C
			public bool isEditorOnly
			{
				get
				{
					return this.flags.HasFlag(DebugUI.Flags.EditorOnly);
				}
			}

			// Token: 0x17000089 RID: 137
			// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000FA24 File Offset: 0x0000DC24
			public bool isRuntimeOnly
			{
				get
				{
					return this.flags.HasFlag(DebugUI.Flags.RuntimeOnly);
				}
			}

			// Token: 0x1700008A RID: 138
			// (get) Token: 0x06000698 RID: 1688 RVA: 0x0000FA3C File Offset: 0x0000DC3C
			public bool isInactiveInEditor
			{
				get
				{
					return this.isRuntimeOnly && !Application.isPlaying;
				}
			}

			// Token: 0x1700008B RID: 139
			// (get) Token: 0x06000699 RID: 1689 RVA: 0x0000FA50 File Offset: 0x0000DC50
			public bool isHidden
			{
				get
				{
					Func<bool> func = this.isHiddenCallback;
					return func != null && func();
				}
			}

			// Token: 0x0600069A RID: 1690 RVA: 0x0000FA63 File Offset: 0x0000DC63
			internal virtual void GenerateQueryPath()
			{
				this.queryPath = this.displayName.Trim();
				if (this.m_Parent != null)
				{
					this.queryPath = this.m_Parent.queryPath + " -> " + this.queryPath;
				}
			}

			// Token: 0x0600069B RID: 1691 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
			public override int GetHashCode()
			{
				return this.queryPath.GetHashCode() ^ this.isHidden.GetHashCode();
			}

			// Token: 0x1700008C RID: 140
			// (set) Token: 0x0600069C RID: 1692 RVA: 0x0000FAC7 File Offset: 0x0000DCC7
			public DebugUI.Widget.NameAndTooltip nameAndTooltip
			{
				set
				{
					this.displayName = value.name;
					this.tooltip = value.tooltip;
				}
			}

			// Token: 0x0400025C RID: 604
			protected DebugUI.Panel m_Panel;

			// Token: 0x0400025D RID: 605
			protected DebugUI.IContainer m_Parent;

			// Token: 0x04000262 RID: 610
			public Func<bool> isHiddenCallback;

			// Token: 0x020000C0 RID: 192
			public struct NameAndTooltip
			{
				// Token: 0x04000263 RID: 611
				public string name;

				// Token: 0x04000264 RID: 612
				public string tooltip;
			}
		}

		// Token: 0x020000C1 RID: 193
		public interface IContainer
		{
			// Token: 0x1700008D RID: 141
			// (get) Token: 0x0600069E RID: 1694
			ObservableList<DebugUI.Widget> children { get; }

			// Token: 0x1700008E RID: 142
			// (get) Token: 0x0600069F RID: 1695
			// (set) Token: 0x060006A0 RID: 1696
			string displayName { get; set; }

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x060006A1 RID: 1697
			string queryPath { get; }
		}

		// Token: 0x020000C2 RID: 194
		public interface IValueField
		{
			// Token: 0x060006A2 RID: 1698
			object GetValue();

			// Token: 0x060006A3 RID: 1699
			void SetValue(object value);

			// Token: 0x060006A4 RID: 1700
			object ValidateValue(object value);
		}

		// Token: 0x020000C3 RID: 195
		public class Button : DebugUI.Widget
		{
			// Token: 0x17000090 RID: 144
			// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0000FAE1 File Offset: 0x0000DCE1
			// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0000FAE9 File Offset: 0x0000DCE9
			public Action action { get; set; }
		}

		// Token: 0x020000C4 RID: 196
		public class Value : DebugUI.Widget
		{
			// Token: 0x17000091 RID: 145
			// (get) Token: 0x060006A8 RID: 1704 RVA: 0x0000FAF2 File Offset: 0x0000DCF2
			// (set) Token: 0x060006A9 RID: 1705 RVA: 0x0000FAFA File Offset: 0x0000DCFA
			public Func<object> getter { get; set; }

			// Token: 0x060006AA RID: 1706 RVA: 0x0000FB03 File Offset: 0x0000DD03
			public Value()
			{
				base.displayName = "";
			}

			// Token: 0x060006AB RID: 1707 RVA: 0x0000FB21 File Offset: 0x0000DD21
			public virtual object GetValue()
			{
				return this.getter();
			}

			// Token: 0x060006AC RID: 1708 RVA: 0x0000FB2E File Offset: 0x0000DD2E
			public virtual string FormatString(object value)
			{
				if (!string.IsNullOrEmpty(this.formatString))
				{
					return string.Format(this.formatString, value);
				}
				return string.Format("{0}", value);
			}

			// Token: 0x04000267 RID: 615
			public float refreshRate = 0.1f;

			// Token: 0x04000268 RID: 616
			public string formatString;
		}

		// Token: 0x020000C5 RID: 197
		public class ProgressBarValue : DebugUI.Value
		{
			// Token: 0x060006AD RID: 1709 RVA: 0x0000FB58 File Offset: 0x0000DD58
			public override string FormatString(object value)
			{
				float percentage = DebugUI.ProgressBarValue.<FormatString>g__Remap01|2_0(Mathf.Clamp((float)value, this.min, this.max), this.min, this.max);
				return string.Format("{0:P1}", percentage);
			}

			// Token: 0x060006AF RID: 1711 RVA: 0x0000FBB1 File Offset: 0x0000DDB1
			[CompilerGenerated]
			internal static float <FormatString>g__Remap01|2_0(float v, float x0, float y0)
			{
				return (v - x0) / (y0 - x0);
			}

			// Token: 0x04000269 RID: 617
			public float min;

			// Token: 0x0400026A RID: 618
			public float max = 1f;
		}

		// Token: 0x020000C6 RID: 198
		public class ValueTuple : DebugUI.Widget
		{
			// Token: 0x17000092 RID: 146
			// (get) Token: 0x060006B0 RID: 1712 RVA: 0x0000FBBA File Offset: 0x0000DDBA
			public int numElements
			{
				get
				{
					return this.values.Length;
				}
			}

			// Token: 0x17000093 RID: 147
			// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0000FBC4 File Offset: 0x0000DDC4
			public float refreshRate
			{
				get
				{
					DebugUI.Value value = this.values.FirstOrDefault<DebugUI.Value>();
					if (value == null)
					{
						return 0.1f;
					}
					return value.refreshRate;
				}
			}

			// Token: 0x0400026B RID: 619
			public DebugUI.Value[] values;

			// Token: 0x0400026C RID: 620
			public int pinnedElementIndex = -1;
		}
	}
}
