using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Threading;

namespace System.Data
{
	/// <summary>Represents a row of data in a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000040 RID: 64
	public class DataRow
	{
		/// <summary>Initializes a new instance of the DataRow. Constructs a row from the builder. Only for internal usage..</summary>
		/// <param name="builder">builder </param>
		// Token: 0x06000442 RID: 1090 RVA: 0x00016790 File Offset: 0x00014990
		protected internal DataRow(DataRowBuilder builder)
		{
			this._tempRecord = builder._record;
			this._table = builder._table;
			this._columns = this._table.Columns;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x000167F2 File Offset: 0x000149F2
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x00016805 File Offset: 0x00014A05
		internal DataColumn LastChangedColumn
		{
			get
			{
				if (this._countColumnChange == 1)
				{
					return this._lastChangedColumn;
				}
				return null;
			}
			set
			{
				this._countColumnChange++;
				this._lastChangedColumn = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0001681C File Offset: 0x00014A1C
		internal bool HasPropertyChanged
		{
			get
			{
				return 0 < this._countColumnChange;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00016827 File Offset: 0x00014A27
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0001682F File Offset: 0x00014A2F
		internal int RBTreeNodeId
		{
			get
			{
				return this._rbTreeNodeId;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, int>("<ds.DataRow.set_RBTreeNodeId|INFO> {0}, value={1}", this._objectID, value);
				this._rbTreeNodeId = value;
			}
		}

		/// <summary>Gets or sets the custom error description for a row.</summary>
		/// <returns>The text describing an error.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001684E File Offset: 0x00014A4E
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0001686C File Offset: 0x00014A6C
		public string RowError
		{
			get
			{
				if (this._error != null)
				{
					return this._error.Text;
				}
				return string.Empty;
			}
			set
			{
				DataCommonEventSource.Log.Trace<int, string>("<ds.DataRow.set_RowError|API> {0}, value='{1}'", this._objectID, value);
				if (this._error == null)
				{
					if (!string.IsNullOrEmpty(value))
					{
						this._error = new DataError(value);
					}
					this.RowErrorChanged();
					return;
				}
				if (this._error.Text != value)
				{
					this._error.Text = value;
					this.RowErrorChanged();
				}
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x000168D7 File Offset: 0x00014AD7
		private void RowErrorChanged()
		{
			if (this._oldRecord != -1)
			{
				this._table.RecordChanged(this._oldRecord);
			}
			if (this._newRecord != -1)
			{
				this._table.RecordChanged(this._newRecord);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001690D File Offset: 0x00014B0D
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x00016915 File Offset: 0x00014B15
		internal long rowID
		{
			get
			{
				return this._rowID;
			}
			set
			{
				this.ResetLastChangedColumn();
				this._rowID = value;
			}
		}

		/// <summary>Gets the current state of the row with regard to its relationship to the <see cref="T:System.Data.DataRowCollection" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00016924 File Offset: 0x00014B24
		public DataRowState RowState
		{
			get
			{
				if (this._oldRecord == this._newRecord)
				{
					if (this._oldRecord == -1)
					{
						return DataRowState.Detached;
					}
					if (0 < this._columns.ColumnsImplementingIChangeTrackingCount)
					{
						foreach (DataColumn dataColumn in this._columns.ColumnsImplementingIChangeTracking)
						{
							object obj = this[dataColumn];
							if (DBNull.Value != obj && ((IChangeTracking)obj).IsChanged)
							{
								return DataRowState.Modified;
							}
						}
					}
					return DataRowState.Unchanged;
				}
				else
				{
					if (this._oldRecord == -1)
					{
						return DataRowState.Added;
					}
					if (this._newRecord == -1)
					{
						return DataRowState.Deleted;
					}
					return DataRowState.Modified;
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> for which this row has a schema.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> to which this row belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000169B0 File Offset: 0x00014BB0
		public DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		/// <summary>Gets or sets the data stored in the column specified by index.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="columnIndex">The zero-based index of the column. </param>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when you try to set a value on a deleted row. </exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range. </exception>
		/// <exception cref="T:System.InvalidCastException">Occurs when you set the value and the new value's <see cref="T:System.Type" /> does not match <see cref="P:System.Data.DataColumn.DataType" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000BA RID: 186
		public object this[int columnIndex]
		{
			get
			{
				DataColumn dataColumn = this._columns[columnIndex];
				int defaultRecord = this.GetDefaultRecord();
				return dataColumn[defaultRecord];
			}
			set
			{
				DataColumn dataColumn = this._columns[columnIndex];
				this[dataColumn] = value;
			}
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00016A04 File Offset: 0x00014C04
		internal void CheckForLoops(DataRelation rel)
		{
			if (this._table._fInLoadDiffgram || (this._table.DataSet != null && this._table.DataSet._fInLoadDiffgram))
			{
				return;
			}
			int count = this._table.Rows.Count;
			int num = 0;
			for (DataRow dataRow = this.GetParentRow(rel); dataRow != null; dataRow = dataRow.GetParentRow(rel))
			{
				if (dataRow == this || num > count)
				{
					throw ExceptionBuilder.NestedCircular(this._table.TableName);
				}
				num++;
			}
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00016A84 File Offset: 0x00014C84
		internal int GetNestedParentCount()
		{
			int num = 0;
			foreach (DataRelation dataRelation in this._table.NestedParentRelations)
			{
				if (dataRelation != null)
				{
					if (dataRelation.ParentTable == this._table)
					{
						this.CheckForLoops(dataRelation);
					}
					if (this.GetParentRow(dataRelation) != null)
					{
						num++;
					}
				}
			}
			return num;
		}

		/// <summary>Gets or sets the data stored in the column specified by name.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <exception cref="T:System.ArgumentException">The column specified by <paramref name="columnName" /> cannot be found. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when you try to set a value on a deleted row. </exception>
		/// <exception cref="T:System.InvalidCastException">Occurs when you set a value and its <see cref="T:System.Type" /> does not match <see cref="P:System.Data.DataColumn.DataType" />. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">Occurs when you try to insert a null value into a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is set to false.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000BB RID: 187
		public object this[string columnName]
		{
			get
			{
				DataColumn dataColumn = this.GetDataColumn(columnName);
				int defaultRecord = this.GetDefaultRecord();
				return dataColumn[defaultRecord];
			}
			set
			{
				DataColumn dataColumn = this.GetDataColumn(columnName);
				this[dataColumn] = value;
			}
		}

		/// <summary>Gets or sets the data stored in the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" /> that contains the data. </param>
		/// <exception cref="T:System.ArgumentException">The column does not belong to this table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> is null. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to set a value on a deleted row. </exception>
		/// <exception cref="T:System.InvalidCastException">The data types of the value and the column do not match. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000BC RID: 188
		public object this[DataColumn column]
		{
			get
			{
				this.CheckColumn(column);
				int defaultRecord = this.GetDefaultRecord();
				return column[defaultRecord];
			}
			set
			{
				this.CheckColumn(column);
				if (this._inChangingEvent)
				{
					throw ExceptionBuilder.EditInRowChanging();
				}
				if (-1L != this.rowID && column.ReadOnly)
				{
					throw ExceptionBuilder.ReadOnly(column.ColumnName);
				}
				DataColumnChangeEventArgs dataColumnChangeEventArgs = null;
				if (this._table.NeedColumnChangeEvents)
				{
					dataColumnChangeEventArgs = new DataColumnChangeEventArgs(this, column, value);
					this._table.OnColumnChanging(dataColumnChangeEventArgs);
				}
				if (column.Table != this._table)
				{
					throw ExceptionBuilder.ColumnNotInTheTable(column.ColumnName, this._table.TableName);
				}
				if (-1L != this.rowID && column.ReadOnly)
				{
					throw ExceptionBuilder.ReadOnly(column.ColumnName);
				}
				object obj = ((dataColumnChangeEventArgs != null) ? dataColumnChangeEventArgs.ProposedValue : value);
				if (obj == null)
				{
					if (column.IsValueType)
					{
						throw ExceptionBuilder.CannotSetToNull(column);
					}
					obj = DBNull.Value;
				}
				bool flag = this.BeginEditInternal();
				try
				{
					int proposedRecordNo = this.GetProposedRecordNo();
					column[proposedRecordNo] = obj;
				}
				catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
				{
					if (flag)
					{
						this.CancelEdit();
					}
					throw;
				}
				this.LastChangedColumn = column;
				if (dataColumnChangeEventArgs != null)
				{
					this._table.OnColumnChanged(dataColumnChangeEventArgs);
				}
				if (flag)
				{
					this.EndEdit();
				}
			}
		}

		/// <summary>Gets the specified version of data stored in the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" /> that contains information about the column. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version that you want. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.ArgumentException">The column does not belong to the table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> argument contains null. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000BD RID: 189
		public object this[DataColumn column, DataRowVersion version]
		{
			get
			{
				this.CheckColumn(column);
				int recordFromVersion = this.GetRecordFromVersion(version);
				return column[recordFromVersion];
			}
		}

		/// <summary>Gets or sets all the values for this row through an array.</summary>
		/// <returns>An array of type <see cref="T:System.Object" />.</returns>
		/// <exception cref="T:System.ArgumentException">The array is larger than the number of columns in the table. </exception>
		/// <exception cref="T:System.InvalidCastException">A value in the array does not match its <see cref="P:System.Data.DataColumn.DataType" /> in its respective <see cref="T:System.Data.DataColumn" />. </exception>
		/// <exception cref="T:System.Data.ConstraintException">An edit broke a constraint. </exception>
		/// <exception cref="T:System.Data.ReadOnlyException">An edit tried to change the value of a read-only column. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">An edit tried to put a null value in a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> of the <see cref="T:System.Data.DataColumn" /> object is false. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The row has been deleted. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00016C9C File Offset: 0x00014E9C
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x00016CE8 File Offset: 0x00014EE8
		public object[] ItemArray
		{
			get
			{
				int defaultRecord = this.GetDefaultRecord();
				object[] array = new object[this._columns.Count];
				for (int i = 0; i < array.Length; i++)
				{
					DataColumn dataColumn = this._columns[i];
					array[i] = dataColumn[defaultRecord];
				}
				return array;
			}
			set
			{
				if (value == null)
				{
					throw ExceptionBuilder.ArgumentNull("ItemArray");
				}
				if (this._columns.Count < value.Length)
				{
					throw ExceptionBuilder.ValueArrayLength();
				}
				DataColumnChangeEventArgs dataColumnChangeEventArgs = null;
				if (this._table.NeedColumnChangeEvents)
				{
					dataColumnChangeEventArgs = new DataColumnChangeEventArgs(this);
				}
				bool flag = this.BeginEditInternal();
				for (int i = 0; i < value.Length; i++)
				{
					if (value[i] != null)
					{
						DataColumn dataColumn = this._columns[i];
						if (-1L != this.rowID && dataColumn.ReadOnly)
						{
							throw ExceptionBuilder.ReadOnly(dataColumn.ColumnName);
						}
						if (dataColumnChangeEventArgs != null)
						{
							dataColumnChangeEventArgs.InitializeColumnChangeEvent(dataColumn, value[i]);
							this._table.OnColumnChanging(dataColumnChangeEventArgs);
						}
						if (dataColumn.Table != this._table)
						{
							throw ExceptionBuilder.ColumnNotInTheTable(dataColumn.ColumnName, this._table.TableName);
						}
						if (-1L != this.rowID && dataColumn.ReadOnly)
						{
							throw ExceptionBuilder.ReadOnly(dataColumn.ColumnName);
						}
						if (this._tempRecord == -1)
						{
							this.BeginEditInternal();
						}
						object obj = ((dataColumnChangeEventArgs != null) ? dataColumnChangeEventArgs.ProposedValue : value[i]);
						if (obj == null)
						{
							if (dataColumn.IsValueType)
							{
								throw ExceptionBuilder.CannotSetToNull(dataColumn);
							}
							obj = DBNull.Value;
						}
						try
						{
							int proposedRecordNo = this.GetProposedRecordNo();
							dataColumn[proposedRecordNo] = obj;
						}
						catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
						{
							if (flag)
							{
								this.CancelEdit();
							}
							throw;
						}
						this.LastChangedColumn = dataColumn;
						if (dataColumnChangeEventArgs != null)
						{
							this._table.OnColumnChanged(dataColumnChangeEventArgs);
						}
					}
				}
				this.EndEdit();
			}
		}

		/// <summary>Commits all the changes made to this row since the last time <see cref="M:System.Data.DataRow.AcceptChanges" /> was called.</summary>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600045A RID: 1114 RVA: 0x00016E78 File Offset: 0x00015078
		public void AcceptChanges()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataRow.AcceptChanges|API> {0}", this._objectID);
			try
			{
				this.EndEdit();
				if (this.RowState != DataRowState.Detached && this.RowState != DataRowState.Deleted && this._columns.ColumnsImplementingIChangeTrackingCount > 0)
				{
					foreach (DataColumn dataColumn in this._columns.ColumnsImplementingIChangeTracking)
					{
						object obj = this[dataColumn];
						if (DBNull.Value != obj)
						{
							IChangeTracking changeTracking = (IChangeTracking)obj;
							if (changeTracking.IsChanged)
							{
								changeTracking.AcceptChanges();
							}
						}
					}
				}
				this._table.CommitRow(this);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Starts an edit operation on a <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <exception cref="T:System.Data.InRowChangingEventException">The method was called inside the <see cref="E:System.Data.DataTable.RowChanging" /> event. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The method was called upon a deleted row. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600045B RID: 1115 RVA: 0x00016F34 File Offset: 0x00015134
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void BeginEdit()
		{
			this.BeginEditInternal();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00016F40 File Offset: 0x00015140
		private bool BeginEditInternal()
		{
			if (this._inChangingEvent)
			{
				throw ExceptionBuilder.BeginEditInRowChanging();
			}
			if (this._tempRecord != -1)
			{
				if (this._tempRecord < this._table._recordManager.LastFreeRecord)
				{
					return false;
				}
				this._tempRecord = -1;
			}
			if (this._oldRecord != -1 && this._newRecord == -1)
			{
				throw ExceptionBuilder.DeletedRowInaccessible();
			}
			this.ResetLastChangedColumn();
			this._tempRecord = this._table.NewRecord(this._newRecord);
			return true;
		}

		/// <summary>Cancels the current edit on the row.</summary>
		/// <exception cref="T:System.Data.InRowChangingEventException">The method was called inside the <see cref="E:System.Data.DataTable.RowChanging" /> event. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600045D RID: 1117 RVA: 0x00016FBB File Offset: 0x000151BB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void CancelEdit()
		{
			if (this._inChangingEvent)
			{
				throw ExceptionBuilder.CancelEditInRowChanging();
			}
			this._table.FreeRecord(ref this._tempRecord);
			this.ResetLastChangedColumn();
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x00016FE2 File Offset: 0x000151E2
		private void CheckColumn(DataColumn column)
		{
			if (column == null)
			{
				throw ExceptionBuilder.ArgumentNull("column");
			}
			if (column.Table != this._table)
			{
				throw ExceptionBuilder.ColumnNotInTheTable(column.ColumnName, this._table.TableName);
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00017017 File Offset: 0x00015217
		internal void CheckInTable()
		{
			if (this.rowID == -1L)
			{
				throw ExceptionBuilder.RowNotInTheTable();
			}
		}

		/// <summary>Deletes the <see cref="T:System.Data.DataRow" />.</summary>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The <see cref="T:System.Data.DataRow" /> has already been deleted.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000460 RID: 1120 RVA: 0x00017029 File Offset: 0x00015229
		public void Delete()
		{
			if (this._inDeletingEvent)
			{
				throw ExceptionBuilder.DeleteInRowDeleting();
			}
			if (this._newRecord == -1)
			{
				return;
			}
			this._table.DeleteRow(this);
		}

		/// <summary>Ends the edit occurring on the row.</summary>
		/// <exception cref="T:System.Data.InRowChangingEventException">The method was called inside the <see cref="E:System.Data.DataTable.RowChanging" /> event. </exception>
		/// <exception cref="T:System.Data.ConstraintException">The edit broke a constraint. </exception>
		/// <exception cref="T:System.Data.ReadOnlyException">The row belongs to the table and the edit tried to change the value of a read-only column. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">The edit tried to put a null value into a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is false. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000461 RID: 1121 RVA: 0x00017050 File Offset: 0x00015250
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndEdit()
		{
			if (this._inChangingEvent)
			{
				throw ExceptionBuilder.EndEditInRowChanging();
			}
			if (this._newRecord == -1)
			{
				return;
			}
			if (this._tempRecord != -1)
			{
				try
				{
					this._table.SetNewRecord(this, this._tempRecord, DataRowAction.Change, false, true, true);
				}
				finally
				{
					this.ResetLastChangedColumn();
				}
			}
		}

		/// <summary>Sets the error description for a column specified by index.</summary>
		/// <param name="columnIndex">The zero-based index of the column. </param>
		/// <param name="error">The error description. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000462 RID: 1122 RVA: 0x000170B0 File Offset: 0x000152B0
		public void SetColumnError(int columnIndex, string error)
		{
			DataColumn dataColumn = this._columns[columnIndex];
			if (dataColumn == null)
			{
				throw ExceptionBuilder.ColumnOutOfRange(columnIndex);
			}
			this.SetColumnError(dataColumn, error);
		}

		/// <summary>Sets the error description for a column specified as a <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to set the error description for. </param>
		/// <param name="error">The error description. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000463 RID: 1123 RVA: 0x000170DC File Offset: 0x000152DC
		public void SetColumnError(DataColumn column, string error)
		{
			this.CheckColumn(column);
			long num = DataCommonEventSource.Log.EnterScope<int, int, string>("<ds.DataRow.SetColumnError|API> {0}, column={1}, error='{2}'", this._objectID, column.ObjectID, error);
			try
			{
				if (this._error == null)
				{
					this._error = new DataError();
				}
				if (this.GetColumnError(column) != error)
				{
					this._error.SetColumnError(column, error);
					this.RowErrorChanged();
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Gets the error description of the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>The text of the error description.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000464 RID: 1124 RVA: 0x00017160 File Offset: 0x00015360
		public string GetColumnError(DataColumn column)
		{
			this.CheckColumn(column);
			if (this._error == null)
			{
				this._error = new DataError();
			}
			return this._error.GetColumnError(column);
		}

		/// <summary>Clears the errors for the row. This includes the <see cref="P:System.Data.DataRow.RowError" /> and errors set with <see cref="M:System.Data.DataRow.SetColumnError(System.Int32,System.String)" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000465 RID: 1125 RVA: 0x00017188 File Offset: 0x00015388
		public void ClearErrors()
		{
			if (this._error != null)
			{
				this._error.Clear();
				this.RowErrorChanged();
			}
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000171A3 File Offset: 0x000153A3
		internal void ClearError(DataColumn column)
		{
			if (this._error != null)
			{
				this._error.Clear(column);
				this.RowErrorChanged();
			}
		}

		/// <summary>Gets a value that indicates whether there are errors in a row.</summary>
		/// <returns>true if the row contains an error; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000171BF File Offset: 0x000153BF
		public bool HasErrors
		{
			get
			{
				return this._error != null && this._error.HasErrors;
			}
		}

		/// <summary>Gets an array of columns that have errors.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that contain errors.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000468 RID: 1128 RVA: 0x000171D6 File Offset: 0x000153D6
		public DataColumn[] GetColumnsInError()
		{
			if (this._error != null)
			{
				return this._error.GetColumnsInError();
			}
			return Array.Empty<DataColumn>();
		}

		/// <summary>Gets the child rows of this <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The relation is null. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000469 RID: 1129 RVA: 0x000171F1 File Offset: 0x000153F1
		public DataRow[] GetChildRows(DataRelation relation)
		{
			return this.GetChildRows(relation, DataRowVersion.Default);
		}

		/// <summary>Gets the child rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is null. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600046A RID: 1130 RVA: 0x00017200 File Offset: 0x00015400
		public DataRow[] GetChildRows(DataRelation relation, DataRowVersion version)
		{
			if (relation == null)
			{
				return this._table.NewRowArray(0);
			}
			if (relation.DataSet != this._table.DataSet)
			{
				throw ExceptionBuilder.RowNotInTheDataSet();
			}
			if (relation.ParentKey.Table != this._table)
			{
				throw ExceptionBuilder.RelationForeignTable(relation.ParentTable.TableName, this._table.TableName);
			}
			return DataRelation.GetChildRows(relation.ParentKey, relation.ChildKey, this, version);
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001727C File Offset: 0x0001547C
		internal DataColumn GetDataColumn(string columnName)
		{
			DataColumn dataColumn = this._columns[columnName];
			if (dataColumn != null)
			{
				return dataColumn;
			}
			throw ExceptionBuilder.ColumnNotInTheTable(columnName, this._table.TableName);
		}

		/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> does not belong to the <see cref="T:System.Data.DataTable" />.The row is null. </exception>
		/// <exception cref="T:System.Data.DataException">A child row has multiple parents.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">This row does not belong to the child table of the <see cref="T:System.Data.DataRelation" /> object. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600046C RID: 1132 RVA: 0x000172AC File Offset: 0x000154AC
		public DataRow GetParentRow(DataRelation relation)
		{
			return this.GetParentRow(relation, DataRowVersion.Default);
		}

		/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. </param>
		/// <exception cref="T:System.ArgumentNullException">The row is null.The <paramref name="relation" /> does not belong to this table's parent relations. </exception>
		/// <exception cref="T:System.Data.DataException">A child row has multiple parents.</exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child table is not the table the row belongs to. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600046D RID: 1133 RVA: 0x000172BC File Offset: 0x000154BC
		public DataRow GetParentRow(DataRelation relation, DataRowVersion version)
		{
			if (relation == null)
			{
				return null;
			}
			if (relation.DataSet != this._table.DataSet)
			{
				throw ExceptionBuilder.RelationForeignRow();
			}
			if (relation.ChildKey.Table != this._table)
			{
				throw ExceptionBuilder.GetParentRowTableMismatch(relation.ChildTable.TableName, this._table.TableName);
			}
			return DataRelation.GetParentRow(relation.ParentKey, relation.ChildKey, this, version);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001732C File Offset: 0x0001552C
		internal DataRow GetNestedParentRow(DataRowVersion version)
		{
			foreach (DataRelation dataRelation in this._table.NestedParentRelations)
			{
				if (dataRelation != null)
				{
					if (dataRelation.ParentTable == this._table)
					{
						this.CheckForLoops(dataRelation);
					}
					DataRow parentRow = this.GetParentRow(dataRelation, version);
					if (parentRow != null)
					{
						return parentRow;
					}
				}
			}
			return null;
		}

		/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.DataRelation" /> does not belong to this row's <see cref="T:System.Data.DataSet" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The row is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child table is not the table the row belongs to. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a <see cref="T:System.Data.DataTable" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600046F RID: 1135 RVA: 0x0001737E File Offset: 0x0001557E
		public DataRow[] GetParentRows(DataRelation relation)
		{
			return this.GetParentRows(relation, DataRowVersion.Default);
		}

		/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.DataRelation" /> does not belong to this row's <see cref="T:System.Data.DataSet" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The row is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child table is not the table the row belongs to. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a <see cref="T:System.Data.DataTable" />. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000470 RID: 1136 RVA: 0x0001738C File Offset: 0x0001558C
		public DataRow[] GetParentRows(DataRelation relation, DataRowVersion version)
		{
			if (relation == null)
			{
				return this._table.NewRowArray(0);
			}
			if (relation.DataSet != this._table.DataSet)
			{
				throw ExceptionBuilder.RowNotInTheDataSet();
			}
			if (relation.ChildKey.Table != this._table)
			{
				throw ExceptionBuilder.GetParentRowTableMismatch(relation.ChildTable.TableName, this._table.TableName);
			}
			return DataRelation.GetParentRows(relation.ParentKey, relation.ChildKey, this, version);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x00017407 File Offset: 0x00015607
		internal object[] GetColumnValues(DataColumn[] columns)
		{
			return this.GetColumnValues(columns, DataRowVersion.Default);
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00017418 File Offset: 0x00015618
		internal object[] GetColumnValues(DataColumn[] columns, DataRowVersion version)
		{
			DataKey dataKey = new DataKey(columns, false);
			return this.GetKeyValues(dataKey, version);
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x00017438 File Offset: 0x00015638
		internal object[] GetKeyValues(DataKey key)
		{
			int defaultRecord = this.GetDefaultRecord();
			return key.GetKeyValues(defaultRecord);
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00017454 File Offset: 0x00015654
		internal object[] GetKeyValues(DataKey key, DataRowVersion version)
		{
			int recordFromVersion = this.GetRecordFromVersion(version);
			return key.GetKeyValues(recordFromVersion);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00017471 File Offset: 0x00015671
		internal int GetCurrentRecordNo()
		{
			if (this._newRecord == -1)
			{
				throw ExceptionBuilder.NoCurrentData();
			}
			return this._newRecord;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00017488 File Offset: 0x00015688
		internal int GetDefaultRecord()
		{
			if (this._tempRecord != -1)
			{
				return this._tempRecord;
			}
			if (this._newRecord != -1)
			{
				return this._newRecord;
			}
			throw (this._oldRecord == -1) ? ExceptionBuilder.RowRemovedFromTheTable() : ExceptionBuilder.DeletedRowInaccessible();
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x000174BF File Offset: 0x000156BF
		internal int GetOriginalRecordNo()
		{
			if (this._oldRecord == -1)
			{
				throw ExceptionBuilder.NoOriginalData();
			}
			return this._oldRecord;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000174D6 File Offset: 0x000156D6
		private int GetProposedRecordNo()
		{
			if (this._tempRecord == -1)
			{
				throw ExceptionBuilder.NoProposedData();
			}
			return this._tempRecord;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x000174F0 File Offset: 0x000156F0
		internal int GetRecordFromVersion(DataRowVersion version)
		{
			if (version <= DataRowVersion.Current)
			{
				if (version == DataRowVersion.Original)
				{
					return this.GetOriginalRecordNo();
				}
				if (version == DataRowVersion.Current)
				{
					return this.GetCurrentRecordNo();
				}
			}
			else
			{
				if (version == DataRowVersion.Proposed)
				{
					return this.GetProposedRecordNo();
				}
				if (version == DataRowVersion.Default)
				{
					return this.GetDefaultRecord();
				}
			}
			throw ExceptionBuilder.InvalidRowVersion();
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001754C File Offset: 0x0001574C
		internal DataRowVersion GetDefaultRowVersion(DataViewRowState viewState)
		{
			if (this._oldRecord == this._newRecord)
			{
				int oldRecord = this._oldRecord;
				return DataRowVersion.Default;
			}
			if (this._oldRecord == -1)
			{
				return DataRowVersion.Default;
			}
			if (this._newRecord == -1)
			{
				return DataRowVersion.Original;
			}
			if ((DataViewRowState.ModifiedCurrent & viewState) != DataViewRowState.None)
			{
				return DataRowVersion.Default;
			}
			return DataRowVersion.Original;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000175A8 File Offset: 0x000157A8
		internal DataViewRowState GetRecordState(int record)
		{
			if (record == -1)
			{
				return DataViewRowState.None;
			}
			if (record == this._oldRecord && record == this._newRecord)
			{
				return DataViewRowState.Unchanged;
			}
			if (record == this._oldRecord)
			{
				if (this._newRecord == -1)
				{
					return DataViewRowState.Deleted;
				}
				return DataViewRowState.ModifiedOriginal;
			}
			else
			{
				if (record != this._newRecord)
				{
					return DataViewRowState.None;
				}
				if (this._oldRecord == -1)
				{
					return DataViewRowState.Added;
				}
				return DataViewRowState.ModifiedCurrent;
			}
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x000175FE File Offset: 0x000157FE
		internal bool HasKeyChanged(DataKey key)
		{
			return this.HasKeyChanged(key, DataRowVersion.Current, DataRowVersion.Proposed);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00017611 File Offset: 0x00015811
		internal bool HasKeyChanged(DataKey key, DataRowVersion version1, DataRowVersion version2)
		{
			return !this.HasVersion(version1) || !this.HasVersion(version2) || !key.RecordsEqual(this.GetRecordFromVersion(version1), this.GetRecordFromVersion(version2));
		}

		/// <summary>Gets a value that indicates whether a specified version exists.</summary>
		/// <returns>true if the version exists; otherwise, false.</returns>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600047E RID: 1150 RVA: 0x00017640 File Offset: 0x00015840
		public bool HasVersion(DataRowVersion version)
		{
			if (version <= DataRowVersion.Current)
			{
				if (version == DataRowVersion.Original)
				{
					return this._oldRecord != -1;
				}
				if (version == DataRowVersion.Current)
				{
					return this._newRecord != -1;
				}
			}
			else
			{
				if (version == DataRowVersion.Proposed)
				{
					return this._tempRecord != -1;
				}
				if (version == DataRowVersion.Default)
				{
					return this._tempRecord != -1 || this._newRecord != -1;
				}
			}
			throw ExceptionBuilder.InvalidRowVersion();
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x000176BD File Offset: 0x000158BD
		internal bool HaveValuesChanged(DataColumn[] columns)
		{
			return this.HaveValuesChanged(columns, DataRowVersion.Current, DataRowVersion.Proposed);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000176D0 File Offset: 0x000158D0
		internal bool HaveValuesChanged(DataColumn[] columns, DataRowVersion version1, DataRowVersion version2)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				this.CheckColumn(columns[i]);
			}
			DataKey dataKey = new DataKey(columns, false);
			return this.HasKeyChanged(dataKey, version1, version2);
		}

		/// <summary>Gets a value that indicates whether the named column contains a null value.</summary>
		/// <returns>true if the column contains a null value; otherwise, false.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000481 RID: 1153 RVA: 0x00017708 File Offset: 0x00015908
		public bool IsNull(string columnName)
		{
			DataColumn dataColumn = this.GetDataColumn(columnName);
			int defaultRecord = this.GetDefaultRecord();
			return dataColumn.IsNull(defaultRecord);
		}

		/// <summary>Rejects all changes made to the row since <see cref="M:System.Data.DataRow.AcceptChanges" /> was last called.</summary>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000482 RID: 1154 RVA: 0x0001772C File Offset: 0x0001592C
		public void RejectChanges()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataRow.RejectChanges|API> {0}", this._objectID);
			try
			{
				if (this.RowState != DataRowState.Detached)
				{
					if (this._columns.ColumnsImplementingIChangeTrackingCount != this._columns.ColumnsImplementingIRevertibleChangeTrackingCount)
					{
						foreach (DataColumn dataColumn in this._columns.ColumnsImplementingIChangeTracking)
						{
							if (!dataColumn.ImplementsIRevertibleChangeTracking)
							{
								object obj;
								if (this.RowState != DataRowState.Deleted)
								{
									obj = this[dataColumn];
								}
								else
								{
									obj = this[dataColumn, DataRowVersion.Original];
								}
								if (DBNull.Value != obj && ((IChangeTracking)obj).IsChanged)
								{
									throw ExceptionBuilder.UDTImplementsIChangeTrackingButnotIRevertible(dataColumn.DataType.AssemblyQualifiedName);
								}
							}
						}
					}
					foreach (DataColumn dataColumn2 in this._columns.ColumnsImplementingIChangeTracking)
					{
						object obj2;
						if (this.RowState != DataRowState.Deleted)
						{
							obj2 = this[dataColumn2];
						}
						else
						{
							obj2 = this[dataColumn2, DataRowVersion.Original];
						}
						if (DBNull.Value != obj2 && ((IChangeTracking)obj2).IsChanged)
						{
							((IRevertibleChangeTracking)obj2).RejectChanges();
						}
					}
				}
				this._table.RollbackRow(this);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00017888 File Offset: 0x00015A88
		internal void ResetLastChangedColumn()
		{
			this._lastChangedColumn = null;
			this._countColumnChange = 0;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00017898 File Offset: 0x00015A98
		internal void SetKeyValues(DataKey key, object[] keyValues)
		{
			bool flag = true;
			bool flag2 = this._tempRecord == -1;
			for (int i = 0; i < keyValues.Length; i++)
			{
				if (!this[key.ColumnsReference[i]].Equals(keyValues[i]))
				{
					if (flag2 && flag)
					{
						flag = false;
						this.BeginEditInternal();
					}
					this[key.ColumnsReference[i]] = keyValues[i];
				}
			}
			if (!flag)
			{
				this.EndEdit();
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00017904 File Offset: 0x00015B04
		internal void SetNestedParentRow(DataRow parentRow, bool setNonNested)
		{
			if (parentRow == null)
			{
				this.SetParentRowToDBNull();
				return;
			}
			foreach (object obj in this._table.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if ((dataRelation.Nested || setNonNested) && dataRelation.ParentKey.Table == parentRow._table)
				{
					object[] keyValues = parentRow.GetKeyValues(dataRelation.ParentKey);
					this.SetKeyValues(dataRelation.ChildKey, keyValues);
					if (dataRelation.Nested)
					{
						if (parentRow._table == this._table)
						{
							this.CheckForLoops(dataRelation);
						}
						else
						{
							this.GetParentRow(dataRelation);
						}
					}
				}
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000179CC File Offset: 0x00015BCC
		internal void SetParentRowToDBNull()
		{
			foreach (object obj in this._table.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				this.SetParentRowToDBNull(dataRelation);
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00017A2C File Offset: 0x00015C2C
		internal void SetParentRowToDBNull(DataRelation relation)
		{
			if (relation.ChildKey.Table != this._table)
			{
				throw ExceptionBuilder.SetParentRowTableMismatch(relation.ChildKey.Table.TableName, this._table.TableName);
			}
			object[] array = new object[] { DBNull.Value };
			this.SetKeyValues(relation.ChildKey, array);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00017A90 File Offset: 0x00015C90
		internal int CopyValuesIntoStore(ArrayList storeList, ArrayList nullbitList, int storeIndex)
		{
			int num = 0;
			if (this._oldRecord != -1)
			{
				for (int i = 0; i < this._columns.Count; i++)
				{
					this._columns[i].CopyValueIntoStore(this._oldRecord, storeList[i], (BitArray)nullbitList[i], storeIndex);
				}
				num++;
				storeIndex++;
			}
			DataRowState rowState = this.RowState;
			if (DataRowState.Added == rowState || DataRowState.Modified == rowState)
			{
				for (int j = 0; j < this._columns.Count; j++)
				{
					this._columns[j].CopyValueIntoStore(this._newRecord, storeList[j], (BitArray)nullbitList[j], storeIndex);
				}
				num++;
				storeIndex++;
			}
			if (-1 != this._tempRecord)
			{
				for (int k = 0; k < this._columns.Count; k++)
				{
					this._columns[k].CopyValueIntoStore(this._tempRecord, storeList[k], (BitArray)nullbitList[k], storeIndex);
				}
				num++;
				storeIndex++;
			}
			return num;
		}

		// Token: 0x0400014D RID: 333
		private readonly DataTable _table;

		// Token: 0x0400014E RID: 334
		private readonly DataColumnCollection _columns;

		// Token: 0x0400014F RID: 335
		internal int _oldRecord = -1;

		// Token: 0x04000150 RID: 336
		internal int _newRecord = -1;

		// Token: 0x04000151 RID: 337
		internal int _tempRecord;

		// Token: 0x04000152 RID: 338
		internal long _rowID = -1L;

		// Token: 0x04000153 RID: 339
		internal DataRowAction _action;

		// Token: 0x04000154 RID: 340
		internal bool _inChangingEvent;

		// Token: 0x04000155 RID: 341
		internal bool _inDeletingEvent;

		// Token: 0x04000156 RID: 342
		internal bool _inCascade;

		// Token: 0x04000157 RID: 343
		private DataColumn _lastChangedColumn;

		// Token: 0x04000158 RID: 344
		private int _countColumnChange;

		// Token: 0x04000159 RID: 345
		private DataError _error;

		// Token: 0x0400015A RID: 346
		private int _rbTreeNodeId;

		// Token: 0x0400015B RID: 347
		private static int s_objectTypeCount;

		// Token: 0x0400015C RID: 348
		internal readonly int _objectID = Interlocked.Increment(ref DataRow.s_objectTypeCount);
	}
}
