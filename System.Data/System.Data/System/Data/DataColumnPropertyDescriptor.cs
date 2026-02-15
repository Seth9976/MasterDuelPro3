using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x02000037 RID: 55
	internal sealed class DataColumnPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x000143CE File Offset: 0x000125CE
		internal DataColumnPropertyDescriptor(DataColumn dataColumn)
			: base(dataColumn.ColumnName, null)
		{
			this.Column = dataColumn;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x000143E4 File Offset: 0x000125E4
		public override AttributeCollection Attributes
		{
			get
			{
				if (typeof(IList).IsAssignableFrom(this.PropertyType))
				{
					Attribute[] array = new Attribute[base.Attributes.Count + 1];
					base.Attributes.CopyTo(array, 0);
					array[array.Length - 1] = new ListBindableAttribute(false);
					return new AttributeCollection(array);
				}
				return base.Attributes;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00014442 File Offset: 0x00012642
		internal DataColumn Column { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001444A File Offset: 0x0001264A
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00014456 File Offset: 0x00012656
		public override bool IsReadOnly
		{
			get
			{
				return this.Column.ReadOnly;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00014463 File Offset: 0x00012663
		public override Type PropertyType
		{
			get
			{
				return this.Column.DataType;
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00014470 File Offset: 0x00012670
		public override bool Equals(object other)
		{
			return other is DataColumnPropertyDescriptor && ((DataColumnPropertyDescriptor)other).Column == this.Column;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001448F File Offset: 0x0001268F
		public override int GetHashCode()
		{
			return this.Column.GetHashCode();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001449C File Offset: 0x0001269C
		public override bool CanResetValue(object component)
		{
			DataRowView dataRowView = (DataRowView)component;
			if (!this.Column.IsSqlType)
			{
				return dataRowView.GetColumnValue(this.Column) != DBNull.Value;
			}
			return !DataStorage.IsObjectNull(dataRowView.GetColumnValue(this.Column));
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000144E8 File Offset: 0x000126E8
		public override object GetValue(object component)
		{
			return ((DataRowView)component).GetColumnValue(this.Column);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000144FB File Offset: 0x000126FB
		public override void ResetValue(object component)
		{
			((DataRowView)component).SetColumnValue(this.Column, DBNull.Value);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00014513 File Offset: 0x00012713
		public override void SetValue(object component, object value)
		{
			((DataRowView)component).SetColumnValue(this.Column, value);
			this.OnValueChanged(component, EventArgs.Empty);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}
}
