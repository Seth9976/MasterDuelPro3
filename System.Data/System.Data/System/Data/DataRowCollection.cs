using System;
using System.Collections;

namespace System.Data
{
	/// <summary>Represents a collection of rows for a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000045 RID: 69
	public sealed class DataRowCollection : InternalDataCollectionBase
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x00017BD3 File Offset: 0x00015DD3
		internal DataRowCollection(DataTable table)
		{
			this._table = table;
		}

		/// <summary>Gets the total number of <see cref="T:System.Data.DataRow" /> objects in this collection.</summary>
		/// <returns>The total number of <see cref="T:System.Data.DataRow" /> objects in this collection.</returns>
		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00017BED File Offset: 0x00015DED
		public override int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets the row at the specified index.</summary>
		/// <returns>The specified <see cref="T:System.Data.DataRow" />.</returns>
		/// <param name="index">The zero-based index of the row to return. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000C1 RID: 193
		public DataRow this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.DataRow" /> to the <see cref="T:System.Data.DataRowCollection" /> object.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to add.</param>
		/// <exception cref="T:System.ArgumentNullException">The row is null. </exception>
		/// <exception cref="T:System.ArgumentException">The row either belongs to another table or already belongs to this table.</exception>
		/// <exception cref="T:System.Data.ConstraintException">The addition invalidates a constraint. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">The addition tries to put a null in a <see cref="T:System.Data.DataColumn" /> where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is false.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000490 RID: 1168 RVA: 0x00017C08 File Offset: 0x00015E08
		public void Add(DataRow row)
		{
			this._table.AddRow(row, -1);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00017C18 File Offset: 0x00015E18
		internal void DiffInsertAt(DataRow row, int pos)
		{
			if (pos < 0 || pos == this._list.Count)
			{
				this._table.AddRow(row, (pos > -1) ? (pos + 1) : (-1));
				return;
			}
			if (this._table.NestedParentRelations.Length == 0)
			{
				this._table.InsertRow(row, pos + 1, (pos > this._list.Count) ? (-1) : pos);
				return;
			}
			if (pos >= this._list.Count)
			{
				while (pos > this._list.Count)
				{
					this._list.Add(null);
					this._nullInList++;
				}
				this._table.AddRow(row, pos + 1);
				return;
			}
			if (this._list[pos] != null)
			{
				throw ExceptionBuilder.RowInsertTwice(pos, this._table.TableName);
			}
			this._list.RemoveAt(pos);
			this._nullInList--;
			this._table.InsertRow(row, pos + 1, pos);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <returns>The zero-based index of the row, or -1 if the row is not found in the collection.</returns>
		/// <param name="row">The DataRow to search for.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000492 RID: 1170 RVA: 0x00017D12 File Offset: 0x00015F12
		public int IndexOf(DataRow row)
		{
			if (row != null && row.Table == this._table && (row.RBTreeNodeId != 0 || row.RowState != DataRowState.Detached))
			{
				return this._list.IndexOf(row.RBTreeNodeId, row);
			}
			return -1;
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00017D4C File Offset: 0x00015F4C
		internal DataRow AddWithColumnEvents(params object[] values)
		{
			DataRow dataRow = this._table.NewRow(-1);
			dataRow.ItemArray = values;
			this._table.AddRow(dataRow, -1);
			return dataRow;
		}

		/// <summary>Creates a row using specified values and adds it to the <see cref="T:System.Data.DataRowCollection" />.</summary>
		/// <returns>None.</returns>
		/// <param name="values">The array of values that are used to create the new row. </param>
		/// <exception cref="T:System.ArgumentException">The array is larger than the number of columns in the table.</exception>
		/// <exception cref="T:System.InvalidCastException">A value does not match its respective column type. </exception>
		/// <exception cref="T:System.Data.ConstraintException">Adding the row invalidates a constraint. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">Trying to put a null in a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is false. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000494 RID: 1172 RVA: 0x00017D7C File Offset: 0x00015F7C
		public DataRow Add(params object[] values)
		{
			int num = this._table.NewRecordFromArray(values);
			DataRow dataRow = this._table.NewRow(num);
			this._table.AddRow(dataRow, -1);
			return dataRow;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00017DB1 File Offset: 0x00015FB1
		internal void ArrayAdd(DataRow row)
		{
			row.RBTreeNodeId = this._list.Add(row);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00017DC5 File Offset: 0x00015FC5
		internal void ArrayInsert(DataRow row, int pos)
		{
			row.RBTreeNodeId = this._list.Insert(pos, row);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00017DDA File Offset: 0x00015FDA
		internal void ArrayClear()
		{
			this._list.Clear();
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00017DE7 File Offset: 0x00015FE7
		internal void ArrayRemove(DataRow row)
		{
			if (row.RBTreeNodeId == 0)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.AttachedNodeWithZerorbTreeNodeId);
			}
			this._list.RBDelete(row.RBTreeNodeId);
			row.RBTreeNodeId = 0;
		}

		/// <summary>Copies all the <see cref="T:System.Data.DataRow" /> objects from the collection into the given array, starting at the given destination array index.</summary>
		/// <param name="ar">The one-dimensional array that is the destination of the elements copied from the DataRowCollection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		// Token: 0x06000499 RID: 1177 RVA: 0x00017E12 File Offset: 0x00016012
		public override void CopyTo(Array ar, int index)
		{
			this._list.CopyTo(ar, index);
		}

		/// <summary>Copies all the <see cref="T:System.Data.DataRow" /> objects from the collection into the given array, starting at the given destination array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the DataRowCollection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600049A RID: 1178 RVA: 0x00017E21 File Offset: 0x00016021
		public void CopyTo(DataRow[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for this collection.</returns>
		// Token: 0x0600049B RID: 1179 RVA: 0x00017E30 File Offset: 0x00016030
		public override IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		/// <summary>Removes the specified <see cref="T:System.Data.DataRow" /> from the collection.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600049C RID: 1180 RVA: 0x00017E40 File Offset: 0x00016040
		public void Remove(DataRow row)
		{
			if (row == null || row.Table != this._table || -1L == row.rowID)
			{
				throw ExceptionBuilder.RowOutOfRange();
			}
			if (row.RowState != DataRowState.Deleted && row.RowState != DataRowState.Detached)
			{
				row.Delete();
			}
			if (row.RowState != DataRowState.Detached)
			{
				row.AcceptChanges();
			}
		}

		/// <summary>Removes the row at the specified index from the collection.</summary>
		/// <param name="index">The index of the row to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600049D RID: 1181 RVA: 0x00017E95 File Offset: 0x00016095
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x0400016A RID: 362
		private readonly DataTable _table;

		// Token: 0x0400016B RID: 363
		private readonly DataRowCollection.DataRowTree _list = new DataRowCollection.DataRowTree();

		// Token: 0x0400016C RID: 364
		internal int _nullInList;

		// Token: 0x02000046 RID: 70
		private sealed class DataRowTree : RBTree<DataRow>
		{
			// Token: 0x0600049E RID: 1182 RVA: 0x00017EA4 File Offset: 0x000160A4
			internal DataRowTree()
				: base(TreeAccessMethod.INDEX_ONLY)
			{
			}

			// Token: 0x0600049F RID: 1183 RVA: 0x00017EAD File Offset: 0x000160AD
			protected override int CompareNode(DataRow record1, DataRow record2)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.CompareNodeInDataRowTree);
			}

			// Token: 0x060004A0 RID: 1184 RVA: 0x00017EB6 File Offset: 0x000160B6
			protected override int CompareSateliteTreeNode(DataRow record1, DataRow record2)
			{
				throw ExceptionBuilder.InternalRBTreeError(RBTreeError.CompareSateliteTreeNodeInDataRowTree);
			}
		}
	}
}
