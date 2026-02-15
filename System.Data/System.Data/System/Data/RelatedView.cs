using System;

namespace System.Data
{
	// Token: 0x02000092 RID: 146
	internal sealed class RelatedView : DataView, IFilter
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00024B48 File Offset: 0x00022D48
		public RelatedView(DataColumn[] columns, object[] values)
			: base(columns[0].Table, false)
		{
			if (values == null)
			{
				throw ExceptionBuilder.ArgumentNull("values");
			}
			this._parentRowView = null;
			this._parentKey = null;
			this._childKey = new DataKey(columns, true);
			this._filterValues = values;
			base.ResetRowViewCache();
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00024B9F File Offset: 0x00022D9F
		public RelatedView(DataRowView parentRowView, DataKey parentKey, DataColumn[] childKeyColumns)
			: base(childKeyColumns[0].Table, false)
		{
			this._filterValues = null;
			this._parentRowView = parentRowView;
			this._parentKey = new DataKey?(parentKey);
			this._childKey = new DataKey(childKeyColumns, true);
			base.ResetRowViewCache();
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00024BE0 File Offset: 0x00022DE0
		private object[] GetParentValues()
		{
			if (this._filterValues != null)
			{
				return this._filterValues;
			}
			if (!this._parentRowView.HasRecord())
			{
				return null;
			}
			return this._parentKey.Value.GetKeyValues(this._parentRowView.GetRecord());
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00024C2C File Offset: 0x00022E2C
		public bool Invoke(DataRow row, DataRowVersion version)
		{
			object[] parentValues = this.GetParentValues();
			if (parentValues == null)
			{
				return false;
			}
			object[] keyValues = row.GetKeyValues(this._childKey, version);
			bool flag = true;
			if (keyValues.Length != parentValues.Length)
			{
				flag = false;
			}
			else
			{
				for (int i = 0; i < keyValues.Length; i++)
				{
					if (!keyValues[i].Equals(parentValues[i]))
					{
						flag = false;
						break;
					}
				}
			}
			IFilter filter = base.GetFilter();
			if (filter != null)
			{
				flag &= filter.Invoke(row, version);
			}
			return flag;
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000207F File Offset: 0x0000027F
		internal override IFilter GetFilter()
		{
			return this;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00024C9C File Offset: 0x00022E9C
		public override DataRowView AddNew()
		{
			DataRowView dataRowView = base.AddNew();
			dataRowView.Row.SetKeyValues(this._childKey, this.GetParentValues());
			return dataRowView;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x00024CBB File Offset: 0x00022EBB
		internal override void SetIndex(string newSort, DataViewRowState newRowStates, IFilter newRowFilter)
		{
			base.SetIndex2(newSort, newRowStates, newRowFilter, false);
			base.Reset();
		}

		// Token: 0x040002E5 RID: 741
		private readonly DataKey? _parentKey;

		// Token: 0x040002E6 RID: 742
		private readonly DataKey _childKey;

		// Token: 0x040002E7 RID: 743
		private readonly DataRowView _parentRowView;

		// Token: 0x040002E8 RID: 744
		private readonly object[] _filterValues;
	}
}
