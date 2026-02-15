using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	/// <summary>Represents an action restriction enforced on a set of columns in a primary key/foreign key relationship when a value or row is either deleted or updated.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000078 RID: 120
	[DefaultProperty("ConstraintName")]
	public class ForeignKeyConstraint : Constraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x0600069A RID: 1690 RVA: 0x00020534 File Offset: 0x0001E734
		public ForeignKeyConstraint(DataColumn[] parentColumns, DataColumn[] childColumns)
			: this(null, parentColumns, childColumns)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified name, and arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="constraintName">The name of the <see cref="T:System.Data.ForeignKeyConstraint" />. If null or empty string, a default name will be given when added to the constraints collection. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x0600069B RID: 1691 RVA: 0x0002053F File Offset: 0x0001E73F
		public ForeignKeyConstraint(string constraintName, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			this._deleteRule = Rule.Cascade;
			this._updateRule = Rule.Cascade;
			base..ctor();
			this.Create(constraintName, parentColumns, childColumns);
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio  environment. <see cref="T:System.Data.ForeignKeyConstraint" /> objects created by using this constructor must then be added to the collection via <see cref="M:System.Data.ConstraintCollection.AddRange(System.Data.Constraint[])" />. Tables and columns with the specified names must exist at the time the method is called, or if <see cref="M:System.Data.DataTable.BeginInit" /> has been called prior to calling this constructor, the tables and columns with the specified names must exist at the time that <see cref="M:System.Data.DataTable.EndInit" /> is called.</summary>
		/// <param name="constraintName">The name of the constraint. </param>
		/// <param name="parentTableName">The name of the parent <see cref="T:System.Data.DataTable" /> that contains parent <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="parentColumnNames">An array of the names of parent <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="childColumnNames">An array of the names of child <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="acceptRejectRule">One of the <see cref="T:System.Data.AcceptRejectRule" /> values. Possible values include None, Cascade, and Default. </param>
		/// <param name="deleteRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is deleted. The default is Cascade. Possible values include: None, Cascade, SetNull, SetDefault, and Default. </param>
		/// <param name="updateRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is updated. The default is Cascade. Possible values include: None, Cascade, SetNull, SetDefault, and Default. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x0600069C RID: 1692 RVA: 0x00020560 File Offset: 0x0001E760
		[Browsable(false)]
		public ForeignKeyConstraint(string constraintName, string parentTableName, string[] parentColumnNames, string[] childColumnNames, AcceptRejectRule acceptRejectRule, Rule deleteRule, Rule updateRule)
		{
			this._deleteRule = Rule.Cascade;
			this._updateRule = Rule.Cascade;
			base..ctor();
			this._constraintName = constraintName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._parentTableName = parentTableName;
			this._acceptRejectRule = acceptRejectRule;
			this._deleteRule = deleteRule;
			this._updateRule = updateRule;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x000205B6 File Offset: 0x0001E7B6
		internal DataKey ChildKey
		{
			get
			{
				base.CheckStateForProperty();
				return this._childKey;
			}
		}

		/// <summary>Gets the child columns of this constraint.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that are the child columns of the constraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x000205C4 File Offset: 0x0001E7C4
		[ReadOnly(true)]
		public virtual DataColumn[] Columns
		{
			get
			{
				base.CheckStateForProperty();
				return this._childKey.ToArray();
			}
		}

		/// <summary>Gets the child table of this constraint.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that is the child table in the constraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x000205D7 File Offset: 0x0001E7D7
		[ReadOnly(true)]
		public override DataTable Table
		{
			get
			{
				base.CheckStateForProperty();
				return this._childKey.Table;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x000205EA File Offset: 0x0001E7EA
		internal string[] ParentColumnNames
		{
			get
			{
				return this._parentKey.GetColumnNames();
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x000205F7 File Offset: 0x0001E7F7
		internal string[] ChildColumnNames
		{
			get
			{
				return this._childKey.GetColumnNames();
			}
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x00020604 File Offset: 0x0001E804
		internal override void CheckCanAddToCollection(ConstraintCollection constraints)
		{
			if (this.Table != constraints.Table)
			{
				throw ExceptionBuilder.ConstraintAddFailed(constraints.Table);
			}
			if (this.Table.Locale.LCID != this.RelatedTable.Locale.LCID || this.Table.CaseSensitive != this.RelatedTable.CaseSensitive)
			{
				throw ExceptionBuilder.CaseLocaleMismatch();
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0000593A File Offset: 0x00003B3A
		internal override bool CanBeRemovedFromCollection(ConstraintCollection constraints, bool fThrowException)
		{
			return true;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0002066C File Offset: 0x0001E86C
		internal bool IsKeyNull(object[] values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				if (!DataStorage.IsObjectNull(values[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x00020694 File Offset: 0x0001E894
		internal override bool IsConstraintViolated()
		{
			Index sortIndex = this._childKey.GetSortIndex();
			object[] uniqueKeyValues = sortIndex.GetUniqueKeyValues();
			bool flag = false;
			Index sortIndex2 = this._parentKey.GetSortIndex();
			foreach (object[] array in uniqueKeyValues)
			{
				if (!this.IsKeyNull(array) && !sortIndex2.IsKeyInIndex(array))
				{
					DataRow[] rows = sortIndex.GetRows(sortIndex.FindRecords(array));
					string text = SR.Format("ForeignKeyConstraint {0} requires the child key values ({1}) to exist in the parent table.", this.ConstraintName, ExceptionBuilder.KeysToString(array));
					for (int j = 0; j < rows.Length; j++)
					{
						rows[j].RowError = text;
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00020740 File Offset: 0x0001E940
		internal override bool CanEnableConstraint()
		{
			if (this.Table.DataSet == null || !this.Table.DataSet.EnforceConstraints)
			{
				return true;
			}
			object[] uniqueKeyValues = this._childKey.GetSortIndex().GetUniqueKeyValues();
			Index sortIndex = this._parentKey.GetSortIndex();
			foreach (object[] array in uniqueKeyValues)
			{
				if (!this.IsKeyNull(array) && !sortIndex.IsKeyInIndex(array))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000207B8 File Offset: 0x0001E9B8
		internal void CascadeCommit(DataRow row)
		{
			if (row.RowState == DataRowState.Detached)
			{
				return;
			}
			if (this._acceptRejectRule == AcceptRejectRule.Cascade)
			{
				Index sortIndex = this._childKey.GetSortIndex((row.RowState == DataRowState.Deleted) ? DataViewRowState.Deleted : DataViewRowState.CurrentRows);
				object[] keyValues = row.GetKeyValues(this._parentKey, (row.RowState == DataRowState.Deleted) ? DataRowVersion.Original : DataRowVersion.Default);
				if (this.IsKeyNull(keyValues))
				{
					return;
				}
				Range range = sortIndex.FindRecords(keyValues);
				if (!range.IsNull)
				{
					foreach (DataRow dataRow in sortIndex.GetRows(range))
					{
						if (DataRowState.Detached != dataRow.RowState && !dataRow._inCascade)
						{
							dataRow.AcceptChanges();
						}
					}
				}
			}
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0002086C File Offset: 0x0001EA6C
		internal void CascadeDelete(DataRow row)
		{
			if (-1 == row._newRecord)
			{
				return;
			}
			object[] keyValues = row.GetKeyValues(this._parentKey, DataRowVersion.Current);
			if (this.IsKeyNull(keyValues))
			{
				return;
			}
			Index sortIndex = this._childKey.GetSortIndex();
			switch (this.DeleteRule)
			{
			case Rule.None:
				if (row.Table.DataSet.EnforceConstraints)
				{
					Range range = sortIndex.FindRecords(keyValues);
					if (!range.IsNull)
					{
						if (range.Count == 1 && sortIndex.GetRow(range.Min) == row)
						{
							return;
						}
						throw ExceptionBuilder.FailedCascadeDelete(this.ConstraintName);
					}
				}
				break;
			case Rule.Cascade:
			{
				object[] keyValues2 = row.GetKeyValues(this._parentKey, DataRowVersion.Default);
				Range range2 = sortIndex.FindRecords(keyValues2);
				if (!range2.IsNull)
				{
					foreach (DataRow dataRow in sortIndex.GetRows(range2))
					{
						if (!dataRow._inCascade)
						{
							dataRow.Table.DeleteRow(dataRow);
						}
					}
					return;
				}
				break;
			}
			case Rule.SetNull:
			{
				object[] array = new object[this._childKey.ColumnsReference.Length];
				for (int j = 0; j < this._childKey.ColumnsReference.Length; j++)
				{
					array[j] = DBNull.Value;
				}
				Range range3 = sortIndex.FindRecords(keyValues);
				if (!range3.IsNull)
				{
					DataRow[] rows2 = sortIndex.GetRows(range3);
					for (int k = 0; k < rows2.Length; k++)
					{
						if (row != rows2[k])
						{
							rows2[k].SetKeyValues(this._childKey, array);
						}
					}
					return;
				}
				break;
			}
			case Rule.SetDefault:
			{
				object[] array2 = new object[this._childKey.ColumnsReference.Length];
				for (int l = 0; l < this._childKey.ColumnsReference.Length; l++)
				{
					array2[l] = this._childKey.ColumnsReference[l].DefaultValue;
				}
				Range range4 = sortIndex.FindRecords(keyValues);
				if (!range4.IsNull)
				{
					DataRow[] rows3 = sortIndex.GetRows(range4);
					for (int m = 0; m < rows3.Length; m++)
					{
						if (row != rows3[m])
						{
							rows3[m].SetKeyValues(this._childKey, array2);
						}
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00020A98 File Offset: 0x0001EC98
		internal void CascadeRollback(DataRow row)
		{
			Index sortIndex = this._childKey.GetSortIndex((row.RowState == DataRowState.Deleted) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows);
			object[] keyValues = row.GetKeyValues(this._parentKey, (row.RowState == DataRowState.Modified) ? DataRowVersion.Current : DataRowVersion.Default);
			if (this.IsKeyNull(keyValues))
			{
				return;
			}
			Range range = sortIndex.FindRecords(keyValues);
			if (this._acceptRejectRule == AcceptRejectRule.Cascade)
			{
				if (!range.IsNull)
				{
					DataRow[] rows = sortIndex.GetRows(range);
					for (int i = 0; i < rows.Length; i++)
					{
						if (!rows[i]._inCascade)
						{
							rows[i].RejectChanges();
						}
					}
					return;
				}
			}
			else if (row.RowState != DataRowState.Deleted && row.Table.DataSet.EnforceConstraints && !range.IsNull)
			{
				if (range.Count == 1 && sortIndex.GetRow(range.Min) == row)
				{
					return;
				}
				if (row.HasKeyChanged(this._parentKey))
				{
					throw ExceptionBuilder.FailedCascadeUpdate(this.ConstraintName);
				}
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00020B94 File Offset: 0x0001ED94
		internal void CascadeUpdate(DataRow row)
		{
			if (-1 == row._newRecord)
			{
				return;
			}
			object[] keyValues = row.GetKeyValues(this._parentKey, DataRowVersion.Current);
			if (!this.Table.DataSet._fInReadXml && this.IsKeyNull(keyValues))
			{
				return;
			}
			Index sortIndex = this._childKey.GetSortIndex();
			switch (this.UpdateRule)
			{
			case Rule.None:
				if (row.Table.DataSet.EnforceConstraints && !sortIndex.FindRecords(keyValues).IsNull)
				{
					throw ExceptionBuilder.FailedCascadeUpdate(this.ConstraintName);
				}
				break;
			case Rule.Cascade:
			{
				Range range = sortIndex.FindRecords(keyValues);
				if (!range.IsNull)
				{
					object[] keyValues2 = row.GetKeyValues(this._parentKey, DataRowVersion.Proposed);
					DataRow[] rows = sortIndex.GetRows(range);
					for (int i = 0; i < rows.Length; i++)
					{
						rows[i].SetKeyValues(this._childKey, keyValues2);
					}
					return;
				}
				break;
			}
			case Rule.SetNull:
			{
				object[] array = new object[this._childKey.ColumnsReference.Length];
				for (int j = 0; j < this._childKey.ColumnsReference.Length; j++)
				{
					array[j] = DBNull.Value;
				}
				Range range2 = sortIndex.FindRecords(keyValues);
				if (!range2.IsNull)
				{
					DataRow[] rows2 = sortIndex.GetRows(range2);
					for (int k = 0; k < rows2.Length; k++)
					{
						rows2[k].SetKeyValues(this._childKey, array);
					}
					return;
				}
				break;
			}
			case Rule.SetDefault:
			{
				object[] array2 = new object[this._childKey.ColumnsReference.Length];
				for (int l = 0; l < this._childKey.ColumnsReference.Length; l++)
				{
					array2[l] = this._childKey.ColumnsReference[l].DefaultValue;
				}
				Range range3 = sortIndex.FindRecords(keyValues);
				if (!range3.IsNull)
				{
					DataRow[] rows3 = sortIndex.GetRows(range3);
					for (int m = 0; m < rows3.Length; m++)
					{
						rows3[m].SetKeyValues(this._childKey, array2);
					}
				}
				break;
			}
			default:
				return;
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00020D98 File Offset: 0x0001EF98
		internal void CheckCanClearParentTable(DataTable table)
		{
			if (this.Table.DataSet.EnforceConstraints && this.Table.Rows.Count > 0)
			{
				throw ExceptionBuilder.FailedClearParentTable(table.TableName, this.ConstraintName, this.Table.TableName);
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00020DE7 File Offset: 0x0001EFE7
		internal void CheckCanRemoveParentRow(DataRow row)
		{
			if (!this.Table.DataSet.EnforceConstraints)
			{
				return;
			}
			if (DataRelation.GetChildRows(this.ParentKey, this.ChildKey, row, DataRowVersion.Default).Length != 0)
			{
				throw ExceptionBuilder.RemoveParentRow(this);
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00020E20 File Offset: 0x0001F020
		internal void CheckCascade(DataRow row, DataRowAction action)
		{
			if (row._inCascade)
			{
				return;
			}
			row._inCascade = true;
			try
			{
				if (action == DataRowAction.Change)
				{
					if (row.HasKeyChanged(this._parentKey))
					{
						this.CascadeUpdate(row);
					}
				}
				else if (action == DataRowAction.Delete)
				{
					this.CascadeDelete(row);
				}
				else if (action == DataRowAction.Commit)
				{
					this.CascadeCommit(row);
				}
				else if (action == DataRowAction.Rollback)
				{
					this.CascadeRollback(row);
				}
			}
			finally
			{
				row._inCascade = false;
			}
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00020EA0 File Offset: 0x0001F0A0
		internal override void CheckConstraint(DataRow childRow, DataRowAction action)
		{
			if ((action == DataRowAction.Change || action == DataRowAction.Add || action == DataRowAction.Rollback) && this.Table.DataSet != null && this.Table.DataSet.EnforceConstraints && childRow.HasKeyChanged(this._childKey))
			{
				DataRowVersion dataRowVersion = ((action == DataRowAction.Rollback) ? DataRowVersion.Original : DataRowVersion.Current);
				object[] keyValues = childRow.GetKeyValues(this._childKey);
				if (childRow.HasVersion(dataRowVersion))
				{
					DataRow parentRow = DataRelation.GetParentRow(this.ParentKey, this.ChildKey, childRow, dataRowVersion);
					if (parentRow != null && parentRow._inCascade)
					{
						object[] keyValues2 = parentRow.GetKeyValues(this._parentKey, (action == DataRowAction.Rollback) ? dataRowVersion : DataRowVersion.Default);
						int num = childRow.Table.NewRecord();
						childRow.Table.SetKeyValues(this._childKey, keyValues2, num);
						if (this._childKey.RecordsEqual(childRow._tempRecord, num))
						{
							return;
						}
					}
				}
				object[] keyValues3 = childRow.GetKeyValues(this._childKey);
				if (!this.IsKeyNull(keyValues3) && !this._parentKey.GetSortIndex().IsKeyInIndex(keyValues3))
				{
					if (this._childKey.Table == this._parentKey.Table && childRow._tempRecord != -1)
					{
						int i;
						for (i = 0; i < keyValues3.Length; i++)
						{
							DataColumn dataColumn = this._parentKey.ColumnsReference[i];
							object obj = dataColumn.ConvertValue(keyValues3[i]);
							if (dataColumn.CompareValueTo(childRow._tempRecord, obj) != 0)
							{
								break;
							}
						}
						if (i == keyValues3.Length)
						{
							return;
						}
					}
					throw ExceptionBuilder.ForeignKeyViolation(this.ConstraintName, keyValues);
				}
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00021030 File Offset: 0x0001F230
		private void NonVirtualCheckState()
		{
			if (this._DataSet == null)
			{
				this._parentKey.CheckState();
				this._childKey.CheckState();
				if (this._parentKey.Table.DataSet != this._childKey.Table.DataSet)
				{
					throw ExceptionBuilder.TablesInDifferentSets();
				}
				for (int i = 0; i < this._parentKey.ColumnsReference.Length; i++)
				{
					if (this._parentKey.ColumnsReference[i].DataType != this._childKey.ColumnsReference[i].DataType || (this._parentKey.ColumnsReference[i].DataType == typeof(DateTime) && this._parentKey.ColumnsReference[i].DateTimeMode != this._childKey.ColumnsReference[i].DateTimeMode && (this._parentKey.ColumnsReference[i].DateTimeMode & this._childKey.ColumnsReference[i].DateTimeMode) != DataSetDateTime.Unspecified))
					{
						throw ExceptionBuilder.ColumnsTypeMismatch();
					}
				}
				if (this._childKey.ColumnsEqual(this._parentKey))
				{
					throw ExceptionBuilder.KeyColumnsIdentical();
				}
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0002115F File Offset: 0x0001F35F
		internal override void CheckState()
		{
			this.NonVirtualCheckState();
		}

		/// <summary>Indicates the action that should take place across this constraint when <see cref="M:System.Data.DataTable.AcceptChanges" /> is invoked.</summary>
		/// <returns>One of the <see cref="T:System.Data.AcceptRejectRule" /> values. Possible values include None, and Cascade. The default is None.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00021167 File Offset: 0x0001F367
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x00021175 File Offset: 0x0001F375
		[DefaultValue(AcceptRejectRule.None)]
		public virtual AcceptRejectRule AcceptRejectRule
		{
			get
			{
				base.CheckStateForProperty();
				return this._acceptRejectRule;
			}
			set
			{
				if (value <= AcceptRejectRule.Cascade)
				{
					this._acceptRejectRule = value;
					return;
				}
				throw ADP.InvalidAcceptRejectRule(value);
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00021189 File Offset: 0x0001F389
		internal override bool ContainsColumn(DataColumn column)
		{
			return this._parentKey.ContainsColumn(column) || this._childKey.ContainsColumn(column);
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x000211A7 File Offset: 0x0001F3A7
		internal override Constraint Clone(DataSet destination)
		{
			return this.Clone(destination, false);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x000211B4 File Offset: 0x0001F3B4
		internal override Constraint Clone(DataSet destination, bool ignorNSforTableLookup)
		{
			int num;
			if (ignorNSforTableLookup)
			{
				num = destination.Tables.IndexOf(this.Table.TableName);
			}
			else
			{
				num = destination.Tables.IndexOf(this.Table.TableName, this.Table.Namespace, false);
			}
			if (num < 0)
			{
				return null;
			}
			DataTable dataTable = destination.Tables[num];
			if (ignorNSforTableLookup)
			{
				num = destination.Tables.IndexOf(this.RelatedTable.TableName);
			}
			else
			{
				num = destination.Tables.IndexOf(this.RelatedTable.TableName, this.RelatedTable.Namespace, false);
			}
			if (num < 0)
			{
				return null;
			}
			DataTable dataTable2 = destination.Tables[num];
			int num2 = this.Columns.Length;
			DataColumn[] array = new DataColumn[num2];
			DataColumn[] array2 = new DataColumn[num2];
			for (int i = 0; i < num2; i++)
			{
				DataColumn dataColumn = this.Columns[i];
				num = dataTable.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array[i] = dataTable.Columns[num];
				dataColumn = this.RelatedColumnsReference[i];
				num = dataTable2.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array2[i] = dataTable2.Columns[num];
			}
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(this.ConstraintName, array2, array);
			foreignKeyConstraint.UpdateRule = this.UpdateRule;
			foreignKeyConstraint.DeleteRule = this.DeleteRule;
			foreignKeyConstraint.AcceptRejectRule = this.AcceptRejectRule;
			foreach (object obj in base.ExtendedProperties.Keys)
			{
				foreignKeyConstraint.ExtendedProperties[obj] = base.ExtendedProperties[obj];
			}
			return foreignKeyConstraint;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0002139C File Offset: 0x0001F59C
		internal ForeignKeyConstraint Clone(DataTable destination)
		{
			int num = this.Columns.Length;
			DataColumn[] array = new DataColumn[num];
			DataColumn[] array2 = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				DataColumn dataColumn = this.Columns[i];
				int num2 = destination.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array[i] = destination.Columns[num2];
				dataColumn = this.RelatedColumnsReference[i];
				num2 = destination.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array2[i] = destination.Columns[num2];
			}
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(this.ConstraintName, array2, array);
			foreignKeyConstraint.UpdateRule = this.UpdateRule;
			foreignKeyConstraint.DeleteRule = this.DeleteRule;
			foreignKeyConstraint.AcceptRejectRule = this.AcceptRejectRule;
			foreach (object obj in base.ExtendedProperties.Keys)
			{
				foreignKeyConstraint.ExtendedProperties[obj] = base.ExtendedProperties[obj];
			}
			return foreignKeyConstraint;
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000214DC File Offset: 0x0001F6DC
		private void Create(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			if (parentColumns.Length == 0 || childColumns.Length == 0)
			{
				throw ExceptionBuilder.KeyLengthZero();
			}
			if (parentColumns.Length != childColumns.Length)
			{
				throw ExceptionBuilder.KeyLengthMismatch();
			}
			for (int i = 0; i < parentColumns.Length; i++)
			{
				if (parentColumns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(parentColumns[i]);
				}
				if (childColumns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(childColumns[i]);
				}
			}
			this._parentKey = new DataKey(parentColumns, true);
			this._childKey = new DataKey(childColumns, true);
			this.ConstraintName = relationName;
			this.NonVirtualCheckState();
		}

		/// <summary>Gets or sets the action that occurs across this constraint when a row is deleted.</summary>
		/// <returns>One of the <see cref="T:System.Data.Rule" /> values. The default is Cascade.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00021560 File Offset: 0x0001F760
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0002156E File Offset: 0x0001F76E
		[DefaultValue(Rule.Cascade)]
		public virtual Rule DeleteRule
		{
			get
			{
				base.CheckStateForProperty();
				return this._deleteRule;
			}
			set
			{
				if (value <= Rule.SetDefault)
				{
					this._deleteRule = value;
					return;
				}
				throw ADP.InvalidRule(value);
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Data.ForeignKeyConstraint" /> is identical to the specified object.</summary>
		/// <returns>true, if the objects are identical; otherwise, false.</returns>
		/// <param name="key">The object to which this <see cref="T:System.Data.ForeignKeyConstraint" /> is compared. Two <see cref="T:System.Data.ForeignKeyConstraint" /> are equal if they constrain the same columns. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060006BA RID: 1722 RVA: 0x00021584 File Offset: 0x0001F784
		public override bool Equals(object key)
		{
			if (!(key is ForeignKeyConstraint))
			{
				return false;
			}
			ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)key;
			return this.ParentKey.ColumnsEqual(foreignKeyConstraint.ParentKey) && this.ChildKey.ColumnsEqual(foreignKeyConstraint.ChildKey);
		}

		/// <summary>Gets the hash code of this instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060006BB RID: 1723 RVA: 0x000215CE File Offset: 0x0001F7CE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>The parent columns of this constraint.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that are the parent columns of the constraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x000215D6 File Offset: 0x0001F7D6
		[ReadOnly(true)]
		public virtual DataColumn[] RelatedColumns
		{
			get
			{
				base.CheckStateForProperty();
				return this._parentKey.ToArray();
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x000215E9 File Offset: 0x0001F7E9
		internal DataColumn[] RelatedColumnsReference
		{
			get
			{
				base.CheckStateForProperty();
				return this._parentKey.ColumnsReference;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x000215FC File Offset: 0x0001F7FC
		internal DataKey ParentKey
		{
			get
			{
				base.CheckStateForProperty();
				return this._parentKey;
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0002160C File Offset: 0x0001F80C
		internal DataRelation FindParentRelation()
		{
			DataRelationCollection parentRelations = this.Table.ParentRelations;
			for (int i = 0; i < parentRelations.Count; i++)
			{
				if (parentRelations[i].ChildKeyConstraint == this)
				{
					return parentRelations[i];
				}
			}
			return null;
		}

		/// <summary>Gets the parent table of this constraint.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataTable" /> of this constraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0002164E File Offset: 0x0001F84E
		[ReadOnly(true)]
		public virtual DataTable RelatedTable
		{
			get
			{
				base.CheckStateForProperty();
				return this._parentKey.Table;
			}
		}

		/// <summary>Gets or sets the action that occurs across this constraint on when a row is updated.</summary>
		/// <returns>One of the <see cref="T:System.Data.Rule" /> values. The default is Cascade.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00021661 File Offset: 0x0001F861
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0002166F File Offset: 0x0001F86F
		[DefaultValue(Rule.Cascade)]
		public virtual Rule UpdateRule
		{
			get
			{
				base.CheckStateForProperty();
				return this._updateRule;
			}
			set
			{
				if (value <= Rule.SetDefault)
				{
					this._updateRule = value;
					return;
				}
				throw ADP.InvalidRule(value);
			}
		}

		// Token: 0x0400027A RID: 634
		internal Rule _deleteRule;

		// Token: 0x0400027B RID: 635
		internal Rule _updateRule;

		// Token: 0x0400027C RID: 636
		internal AcceptRejectRule _acceptRejectRule;

		// Token: 0x0400027D RID: 637
		private DataKey _childKey;

		// Token: 0x0400027E RID: 638
		private DataKey _parentKey;

		// Token: 0x0400027F RID: 639
		internal string _constraintName;

		// Token: 0x04000280 RID: 640
		internal string[] _parentColumnNames;

		// Token: 0x04000281 RID: 641
		internal string[] _childColumnNames;

		// Token: 0x04000282 RID: 642
		internal string _parentTableName;
	}
}
