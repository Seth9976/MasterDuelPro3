using System;
using System.Diagnostics;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011A RID: 282
	[UxmlObject]
	[Serializable]
	public class SortColumnDescription : INotifyBindablePropertyChanged
	{
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060008D5 RID: 2261 RVA: 0x0002A894 File Offset: 0x00028A94
		// (remove) Token: 0x060008D6 RID: 2262 RVA: 0x0002A8CC File Offset: 0x00028ACC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<BindablePropertyChangedEventArgs> propertyChanged;

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x0002A901 File Offset: 0x00028B01
		// (set) Token: 0x060008D8 RID: 2264 RVA: 0x0002A90C File Offset: 0x00028B0C
		[CreateProperty]
		public string columnName
		{
			get
			{
				return this.m_ColumnName;
			}
			set
			{
				bool flag = this.m_ColumnName == value;
				if (!flag)
				{
					this.m_ColumnName = value;
					Action<SortColumnDescription> action = this.changed;
					if (action != null)
					{
						action(this);
					}
					this.NotifyPropertyChanged(in SortColumnDescription.columnNameProperty);
				}
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0002A952 File Offset: 0x00028B52
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x0002A95C File Offset: 0x00028B5C
		[CreateProperty]
		public int columnIndex
		{
			get
			{
				return this.m_ColumnIndex;
			}
			set
			{
				bool flag = this.m_ColumnIndex == value;
				if (!flag)
				{
					this.m_ColumnIndex = value;
					Action<SortColumnDescription> action = this.changed;
					if (action != null)
					{
						action(this);
					}
					this.NotifyPropertyChanged(in SortColumnDescription.columnIndexProperty);
				}
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0002A99F File Offset: 0x00028B9F
		// (set) Token: 0x060008DC RID: 2268 RVA: 0x0002A9A7 File Offset: 0x00028BA7
		public Column column { get; internal set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0002A9B0 File Offset: 0x00028BB0
		// (set) Token: 0x060008DE RID: 2270 RVA: 0x0002A9B8 File Offset: 0x00028BB8
		[CreateProperty]
		public SortDirection direction
		{
			get
			{
				return this.m_SortDirection;
			}
			set
			{
				bool flag = this.m_SortDirection == value;
				if (!flag)
				{
					this.m_SortDirection = value;
					Action<SortColumnDescription> action = this.changed;
					if (action != null)
					{
						action(this);
					}
					this.NotifyPropertyChanged(in SortColumnDescription.directionProperty);
				}
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060008DF RID: 2271 RVA: 0x0002A9FC File Offset: 0x00028BFC
		// (remove) Token: 0x060008E0 RID: 2272 RVA: 0x0002AA34 File Offset: 0x00028C34
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<SortColumnDescription> changed;

		// Token: 0x060008E1 RID: 2273 RVA: 0x0002AA69 File Offset: 0x00028C69
		public SortColumnDescription()
		{
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002AA7A File Offset: 0x00028C7A
		public SortColumnDescription(int columnIndex, SortDirection direction)
		{
			this.columnIndex = columnIndex;
			this.direction = direction;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0002AA9B File Offset: 0x00028C9B
		public SortColumnDescription(string columnName, SortDirection direction)
		{
			this.columnName = columnName;
			this.direction = direction;
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0002AABC File Offset: 0x00028CBC
		private void NotifyPropertyChanged(in BindingId property)
		{
			EventHandler<BindablePropertyChangedEventArgs> eventHandler = this.propertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, new BindablePropertyChangedEventArgs(in property));
			}
		}

		// Token: 0x0400058E RID: 1422
		private static readonly BindingId columnNameProperty = "columnName";

		// Token: 0x0400058F RID: 1423
		private static readonly BindingId columnIndexProperty = "columnIndex";

		// Token: 0x04000590 RID: 1424
		private static readonly BindingId directionProperty = "direction";

		// Token: 0x04000591 RID: 1425
		[SerializeField]
		private int m_ColumnIndex = -1;

		// Token: 0x04000592 RID: 1426
		[SerializeField]
		private string m_ColumnName;

		// Token: 0x04000593 RID: 1427
		[SerializeField]
		private SortDirection m_SortDirection;

		// Token: 0x0200011B RID: 283
		[Obsolete("UxmlObjectFactory<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory<T> : UxmlObjectFactory<T, SortColumnDescription.UxmlObjectTraits<T>> where T : SortColumnDescription, new()
		{
		}

		// Token: 0x0200011C RID: 284
		[Obsolete("UxmlObjectFactory<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectFactory : SortColumnDescription.UxmlObjectFactory<SortColumnDescription>
		{
		}

		// Token: 0x0200011D RID: 285
		[Obsolete("UxmlObjectTraits<T> is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		internal class UxmlObjectTraits<T> : UnityEngine.UIElements.UxmlObjectTraits<T> where T : SortColumnDescription
		{
			// Token: 0x060008E8 RID: 2280 RVA: 0x0002AB1C File Offset: 0x00028D1C
			public override void Init(ref T obj, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ref obj, bag, cc);
				obj.columnName = this.m_ColumnName.GetValueFromBag(bag, cc);
				obj.columnIndex = this.m_ColumnIndex.GetValueFromBag(bag, cc);
				obj.direction = this.m_SortDescription.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000597 RID: 1431
			private readonly UxmlStringAttributeDescription m_ColumnName = new UxmlStringAttributeDescription
			{
				name = "column-name"
			};

			// Token: 0x04000598 RID: 1432
			private readonly UxmlIntAttributeDescription m_ColumnIndex = new UxmlIntAttributeDescription
			{
				name = "column-index",
				defaultValue = -1
			};

			// Token: 0x04000599 RID: 1433
			private readonly UxmlEnumAttributeDescription<SortDirection> m_SortDescription = new UxmlEnumAttributeDescription<SortDirection>
			{
				name = "direction",
				defaultValue = SortDirection.Ascending
			};
		}
	}
}
