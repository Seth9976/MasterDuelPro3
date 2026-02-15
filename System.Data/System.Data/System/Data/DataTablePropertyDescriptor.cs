using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000053 RID: 83
	internal sealed class DataTablePropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00018CE6 File Offset: 0x00016EE6
		public DataTable Table { get; }

		// Token: 0x060004EA RID: 1258 RVA: 0x00018CEE File Offset: 0x00016EEE
		internal DataTablePropertyDescriptor(DataTable dataTable)
			: base(dataTable.TableName, null)
		{
			this.Table = dataTable;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001444A File Offset: 0x0001264A
		public override Type ComponentType
		{
			get
			{
				return typeof(DataRowView);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x00016743 File Offset: 0x00014943
		public override Type PropertyType
		{
			get
			{
				return typeof(IBindingList);
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00018D04 File Offset: 0x00016F04
		public override bool Equals(object other)
		{
			return other is DataTablePropertyDescriptor && ((DataTablePropertyDescriptor)other).Table == this.Table;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00018D23 File Offset: 0x00016F23
		public override int GetHashCode()
		{
			return this.Table.GetHashCode();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override bool CanResetValue(object component)
		{
			return false;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00018D30 File Offset: 0x00016F30
		public override object GetValue(object component)
		{
			return ((DataViewManagerListItemTypeDescriptor)component).GetDataView(this.Table);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00003FD2 File Offset: 0x000021D2
		public override void ResetValue(object component)
		{
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00003FD2 File Offset: 0x000021D2
		public override void SetValue(object component, object value)
		{
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}
	}
}
