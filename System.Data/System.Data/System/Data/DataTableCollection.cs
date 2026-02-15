using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace System.Data
{
	/// <summary>Represents the collection of tables for the <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000050 RID: 80
	[ListBindable(false)]
	[DefaultEvent("CollectionChanged")]
	public sealed class DataTableCollection : InternalDataCollectionBase
	{
		// Token: 0x060004C5 RID: 1221 RVA: 0x000180F0 File Offset: 0x000162F0
		internal DataTableCollection(DataSet dataSet)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataTableCollection.DataTableCollection|INFO> {0}, dataSet={1}", this.ObjectID, (dataSet != null) ? dataSet.ObjectID : 0);
			this._dataSet = dataSet;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001814D File Offset: 0x0001634D
		protected override ArrayList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00018155 File Offset: 0x00016355
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> object at the specified index.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" />with the specified index; otherwise null if the <see cref="T:System.Data.DataTable" /> does not exist.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000C8 RID: 200
		public DataTable this[int index]
		{
			get
			{
				DataTable dataTable;
				try
				{
					dataTable = (DataTable)this._list[index];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw ExceptionBuilder.TableOutOfRange(index);
				}
				return dataTable;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> object with the specified name.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> with the specified name; otherwise null if the <see cref="T:System.Data.DataTable" /> does not exist.</returns>
		/// <param name="name">The name of the DataTable to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000C9 RID: 201
		public DataTable this[string name]
		{
			get
			{
				int num = this.InternalIndexOf(name);
				if (num == -2)
				{
					throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
				}
				if (num == -3)
				{
					throw ExceptionBuilder.NamespaceNameConflict(name);
				}
				if (num >= 0)
				{
					return (DataTable)this._list[num];
				}
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> object with the specified name in the specified namespace.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> with the specified name; otherwise null if the <see cref="T:System.Data.DataTable" /> does not exist.</returns>
		/// <param name="name">The name of the DataTable to find.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000CA RID: 202
		public DataTable this[string name, string tableNamespace]
		{
			get
			{
				if (tableNamespace == null)
				{
					throw ExceptionBuilder.ArgumentNull("tableNamespace");
				}
				int num = this.InternalIndexOf(name, tableNamespace);
				if (num == -2)
				{
					throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
				}
				if (num >= 0)
				{
					return (DataTable)this._list[num];
				}
				return null;
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00018228 File Offset: 0x00016428
		internal DataTable GetTable(string name, string ns)
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (dataTable.TableName == name && dataTable.Namespace == ns)
				{
					return dataTable;
				}
			}
			return null;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001827C File Offset: 0x0001647C
		internal DataTable GetTableSmart(string name, string ns)
		{
			int num = 0;
			DataTable dataTable = null;
			for (int i = 0; i < this._list.Count; i++)
			{
				DataTable dataTable2 = (DataTable)this._list[i];
				if (dataTable2.TableName == name)
				{
					if (dataTable2.Namespace == ns)
					{
						return dataTable2;
					}
					num++;
					dataTable = dataTable2;
				}
			}
			if (num != 1)
			{
				return null;
			}
			return dataTable;
		}

		/// <summary>Adds the specified DataTable to the collection.</summary>
		/// <param name="table">The DataTable object to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The value specified for the table is null. </exception>
		/// <exception cref="T:System.ArgumentException">The table already belongs to this collection, or belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">A table in the collection has the same name. The comparison is not case sensitive. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004CD RID: 1229 RVA: 0x000182E0 File Offset: 0x000164E0
		public void Add(DataTable table)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataTableCollection.Add|API> {0}, table={1}", this.ObjectID, (table != null) ? table.ObjectID : 0);
			try
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
				this.BaseAdd(table);
				this.ArrayAdd(table);
				if (table.SetLocaleValue(this._dataSet.Locale, false, false) || table.SetCaseSensitiveValue(this._dataSet.CaseSensitive, false, false))
				{
					table.ResetIndexes();
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Occurs after the <see cref="T:System.Data.DataTableCollection" /> is changed because of <see cref="T:System.Data.DataTable" /> objects being added or removed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060004CE RID: 1230 RVA: 0x00018388 File Offset: 0x00016588
		// (remove) Token: 0x060004CF RID: 1231 RVA: 0x000183B6 File Offset: 0x000165B6
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.add_CollectionChanged|API> {0}", this.ObjectID);
				this._onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this._onCollectionChangedDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.remove_CollectionChanged|API> {0}", this.ObjectID);
				this._onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this._onCollectionChangedDelegate, value);
			}
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x000183E4 File Offset: 0x000165E4
		private void ArrayAdd(DataTable table)
		{
			this._list.Add(table);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x000183F4 File Offset: 0x000165F4
		internal string AssignName()
		{
			string text;
			while (this.Contains(text = this.MakeName(this._defaultNameIndex)))
			{
				this._defaultNameIndex++;
			}
			return text;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001842C File Offset: 0x0001662C
		private void BaseAdd(DataTable table)
		{
			if (table == null)
			{
				throw ExceptionBuilder.ArgumentNull("table");
			}
			if (table.DataSet == this._dataSet)
			{
				throw ExceptionBuilder.TableAlreadyInTheDataSet();
			}
			if (table.DataSet != null)
			{
				throw ExceptionBuilder.TableAlreadyInOtherDataSet();
			}
			if (table.TableName.Length == 0)
			{
				table.TableName = this.AssignName();
			}
			else
			{
				if (base.NamesEqual(table.TableName, this._dataSet.DataSetName, false, this._dataSet.Locale) != 0 && !table._fNestedInDataset)
				{
					throw ExceptionBuilder.DatasetConflictingName(this._dataSet.DataSetName);
				}
				this.RegisterName(table.TableName, table.Namespace);
			}
			table.SetDataSet(this._dataSet);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000184E0 File Offset: 0x000166E0
		private void BaseGroupSwitch(DataTable[] oldArray, int oldLength, DataTable[] newArray, int newLength)
		{
			int num = 0;
			for (int i = 0; i < oldLength; i++)
			{
				bool flag = false;
				for (int j = num; j < newLength; j++)
				{
					if (oldArray[i] == newArray[j])
					{
						if (num == j)
						{
							num++;
						}
						flag = true;
						break;
					}
				}
				if (!flag && oldArray[i].DataSet == this._dataSet)
				{
					this.BaseRemove(oldArray[i]);
				}
			}
			for (int k = 0; k < newLength; k++)
			{
				if (newArray[k].DataSet != this._dataSet)
				{
					this.BaseAdd(newArray[k]);
					this._list.Add(newArray[k]);
				}
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00018576 File Offset: 0x00016776
		private void BaseRemove(DataTable table)
		{
			if (this.CanRemove(table, true))
			{
				this.UnregisterName(table.TableName);
				table.SetDataSet(null);
			}
			this._list.Remove(table);
			this._dataSet.OnRemovedTable(table);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x000185B0 File Offset: 0x000167B0
		internal bool CanRemove(DataTable table, bool fThrowException)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int, bool>("<ds.DataTableCollection.CanRemove|INFO> {0}, table={1}, fThrowException={2}", this.ObjectID, (table != null) ? table.ObjectID : 0, fThrowException);
			bool flag;
			try
			{
				if (table == null)
				{
					if (fThrowException)
					{
						throw ExceptionBuilder.ArgumentNull("table");
					}
					flag = false;
				}
				else if (table.DataSet != this._dataSet)
				{
					if (fThrowException)
					{
						throw ExceptionBuilder.TableNotInTheDataSet(table.TableName);
					}
					flag = false;
				}
				else
				{
					this._dataSet.OnRemoveTable(table);
					if (table.ChildRelations.Count != 0 || table.ParentRelations.Count != 0)
					{
						if (fThrowException)
						{
							throw ExceptionBuilder.TableInRelation();
						}
						flag = false;
					}
					else
					{
						ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this._dataSet, table);
						while (parentForeignKeyConstraintEnumerator.GetNext())
						{
							ForeignKeyConstraint foreignKeyConstraint = parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
							if (foreignKeyConstraint.Table != table || foreignKeyConstraint.RelatedTable != table)
							{
								if (!fThrowException)
								{
									return false;
								}
								throw ExceptionBuilder.TableInConstraint(table, foreignKeyConstraint);
							}
						}
						ChildForeignKeyConstraintEnumerator childForeignKeyConstraintEnumerator = new ChildForeignKeyConstraintEnumerator(this._dataSet, table);
						while (childForeignKeyConstraintEnumerator.GetNext())
						{
							ForeignKeyConstraint foreignKeyConstraint2 = childForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
							if (foreignKeyConstraint2.Table != table || foreignKeyConstraint2.RelatedTable != table)
							{
								if (!fThrowException)
								{
									return false;
								}
								throw ExceptionBuilder.TableInConstraint(table, foreignKeyConstraint2);
							}
						}
						flag = true;
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
			return flag;
		}

		/// <summary>Clears the collection of all <see cref="T:System.Data.DataTable" /> objects.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004D6 RID: 1238 RVA: 0x000186F8 File Offset: 0x000168F8
		public void Clear()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataTableCollection.Clear|API> {0}", this.ObjectID);
			try
			{
				int count = this._list.Count;
				DataTable[] array = new DataTable[this._list.Count];
				this._list.CopyTo(array, 0);
				this.OnCollectionChanging(InternalDataCollectionBase.s_refreshEventArgs);
				if (this._dataSet._fInitInProgress && this._delayedAddRangeTables != null)
				{
					this._delayedAddRangeTables = null;
				}
				this.BaseGroupSwitch(array, count, null, 0);
				this._list.Clear();
				this.OnCollectionChanged(InternalDataCollectionBase.s_refreshEventArgs);
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Data.DataTable" /> object with the specified name exists in the collection.</summary>
		/// <returns>true if the specified table exists; otherwise false.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004D7 RID: 1239 RVA: 0x000187AC File Offset: 0x000169AC
		public bool Contains(string name)
		{
			return this.InternalIndexOf(name) >= 0;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000187BC File Offset: 0x000169BC
		internal bool Contains(string name, string tableNamespace, bool checkProperty, bool caseSensitive)
		{
			if (!caseSensitive)
			{
				return this.InternalIndexOf(name) >= 0;
			}
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				string text = (checkProperty ? dataTable.Namespace : dataTable._tableNamespace);
				if (base.NamesEqual(dataTable.TableName, name, true, this._dataSet.Locale) == 1 && text == tableNamespace)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00018840 File Offset: 0x00016A40
		internal bool Contains(string name, bool caseSensitive)
		{
			if (!caseSensitive)
			{
				return this.InternalIndexOf(name) >= 0;
			}
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (base.NamesEqual(dataTable.TableName, name, true, this._dataSet.Locale) == 1)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.DataTable" /> object.</summary>
		/// <returns>The zero-based index of the table, or -1 if the table is not found in the collection.</returns>
		/// <param name="table">The DataTable to search for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004DA RID: 1242 RVA: 0x000188A8 File Offset: 0x00016AA8
		public int IndexOf(DataTable table)
		{
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				if (table == (DataTable)this._list[i])
				{
					return i;
				}
			}
			return -1;
		}

		/// <summary>Gets the index in the collection of the <see cref="T:System.Data.DataTable" /> object with the specified name.</summary>
		/// <returns>The zero-based index of the DataTable with the specified name, or -1 if the table does not exist in the collection.NoteReturns -1 when two or more tables have the same name but different namespaces. The call does not succeed if there is any ambiguity when matching a table name to exactly one table.</returns>
		/// <param name="tableName">The name of the DataTable object to look for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004DB RID: 1243 RVA: 0x000188E4 File Offset: 0x00016AE4
		public int IndexOf(string tableName)
		{
			int num = this.InternalIndexOf(tableName);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00018900 File Offset: 0x00016B00
		internal int IndexOf(string tableName, string tableNamespace, bool chekforNull)
		{
			if (chekforNull)
			{
				if (tableName == null)
				{
					throw ExceptionBuilder.ArgumentNull("tableName");
				}
				if (tableNamespace == null)
				{
					throw ExceptionBuilder.ArgumentNull("tableNamespace");
				}
			}
			int num = this.InternalIndexOf(tableName, tableNamespace);
			if (num >= 0)
			{
				return num;
			}
			return -1;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001893C File Offset: 0x00016B3C
		internal void ReplaceFromInference(List<DataTable> tableList)
		{
			this._list.Clear();
			this._list.AddRange(tableList);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00018958 File Offset: 0x00016B58
		internal int InternalIndexOf(string tableName)
		{
			int num = -1;
			if (tableName != null && 0 < tableName.Length)
			{
				int count = this._list.Count;
				for (int i = 0; i < count; i++)
				{
					DataTable dataTable = (DataTable)this._list[i];
					int num2 = base.NamesEqual(dataTable.TableName, tableName, false, this._dataSet.Locale);
					if (num2 == 1)
					{
						for (int j = i + 1; j < count; j++)
						{
							DataTable dataTable2 = (DataTable)this._list[j];
							if (base.NamesEqual(dataTable2.TableName, tableName, false, this._dataSet.Locale) == 1)
							{
								return -3;
							}
						}
						return i;
					}
					if (num2 == -1)
					{
						num = ((num == -1) ? i : (-2));
					}
				}
			}
			return num;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00018A24 File Offset: 0x00016C24
		internal int InternalIndexOf(string tableName, string tableNamespace)
		{
			int num = -1;
			if (tableName != null && 0 < tableName.Length)
			{
				int count = this._list.Count;
				for (int i = 0; i < count; i++)
				{
					DataTable dataTable = (DataTable)this._list[i];
					int num2 = base.NamesEqual(dataTable.TableName, tableName, false, this._dataSet.Locale);
					if (num2 == 1 && dataTable.Namespace == tableNamespace)
					{
						return i;
					}
					if (num2 == -1 && dataTable.Namespace == tableNamespace)
					{
						num = ((num == -1) ? i : (-2));
					}
				}
			}
			return num;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00018ABD File Offset: 0x00016CBD
		private string MakeName(int index)
		{
			if (1 != index)
			{
				return "Table" + index.ToString(CultureInfo.InvariantCulture);
			}
			return "Table1";
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00018ADF File Offset: 0x00016CDF
		private void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this._onCollectionChangedDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.OnCollectionChanged|INFO> {0}", this.ObjectID);
				this._onCollectionChangedDelegate(this, ccevent);
			}
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00018B0B File Offset: 0x00016D0B
		private void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			if (this._onCollectionChangingDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataTableCollection.OnCollectionChanging|INFO> {0}", this.ObjectID);
				this._onCollectionChangingDelegate(this, ccevent);
			}
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x00018B38 File Offset: 0x00016D38
		internal void RegisterName(string name, string tbNamespace)
		{
			DataCommonEventSource.Log.Trace<int, string, string>("<ds.DataTableCollection.RegisterName|INFO> {0}, name='{1}', tbNamespace='{2}'", this.ObjectID, name, tbNamespace);
			CultureInfo locale = this._dataSet.Locale;
			int count = this._list.Count;
			for (int i = 0; i < count; i++)
			{
				DataTable dataTable = (DataTable)this._list[i];
				if (base.NamesEqual(name, dataTable.TableName, true, locale) != 0 && tbNamespace == dataTable.Namespace)
				{
					throw ExceptionBuilder.DuplicateTableName(((DataTable)this._list[i]).TableName);
				}
			}
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex), true, locale) != 0)
			{
				this._defaultNameIndex++;
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Data.DataTable" /> object from the collection.</summary>
		/// <param name="table">The DataTable to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The value specified for the table is null. </exception>
		/// <exception cref="T:System.ArgumentException">The table does not belong to this collection.-or- The table is part of a relationship. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060004E4 RID: 1252 RVA: 0x00018BF4 File Offset: 0x00016DF4
		public void Remove(DataTable table)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataTableCollection.Remove|API> {0}, table={1}", this.ObjectID, (table != null) ? table.ObjectID : 0);
			try
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, table));
				this.BaseRemove(table);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, table));
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00018C64 File Offset: 0x00016E64
		internal void UnregisterName(string name)
		{
			DataCommonEventSource.Log.Trace<int, string>("<ds.DataTableCollection.UnregisterName|INFO> {0}, name='{1}'", this.ObjectID, name);
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex - 1), true, this._dataSet.Locale) != 0)
			{
				do
				{
					this._defaultNameIndex--;
				}
				while (this._defaultNameIndex > 1 && !this.Contains(this.MakeName(this._defaultNameIndex - 1)));
			}
		}

		// Token: 0x04000186 RID: 390
		private readonly DataSet _dataSet;

		// Token: 0x04000187 RID: 391
		private readonly ArrayList _list = new ArrayList();

		// Token: 0x04000188 RID: 392
		private int _defaultNameIndex = 1;

		// Token: 0x04000189 RID: 393
		private DataTable[] _delayedAddRangeTables;

		// Token: 0x0400018A RID: 394
		private CollectionChangeEventHandler _onCollectionChangedDelegate;

		// Token: 0x0400018B RID: 395
		private CollectionChangeEventHandler _onCollectionChangingDelegate;

		// Token: 0x0400018C RID: 396
		private static int s_objectTypeCount;

		// Token: 0x0400018D RID: 397
		private readonly int _objectID = Interlocked.Increment(ref DataTableCollection.s_objectTypeCount);
	}
}
