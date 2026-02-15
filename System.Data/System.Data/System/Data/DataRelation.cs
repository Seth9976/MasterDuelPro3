using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Data
{
	/// <summary>Represents a parent/child relationship between two <see cref="T:System.Data.DataTable" /> objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003B RID: 59
	[DefaultProperty("RelationName")]
	[TypeConverter(typeof(RelationshipConverter))]
	public class DataRelation
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified name, parent and child <see cref="T:System.Data.DataColumn" /> objects, and a value that indicates whether to create constraints.</summary>
		/// <param name="relationName">The name of the relation. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the relation. </param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the relation. </param>
		/// <param name="createConstraints">A value that indicates whether constraints are created. true, if constraints are created. Otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects contains null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types -Or- The tables do not belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x060003E2 RID: 994 RVA: 0x00014BC8 File Offset: 0x00012DC8
		public DataRelation(string relationName, DataColumn parentColumn, DataColumn childColumn, bool createConstraints)
		{
			DataCommonEventSource.Log.Trace<int, string, int, int, bool>("<ds.DataRelation.DataRelation|API> {0}, relationName='{1}', parentColumn={2}, childColumn={3}, createConstraints={4}", this.ObjectID, relationName, (parentColumn != null) ? parentColumn.ObjectID : 0, (childColumn != null) ? childColumn.ObjectID : 0, createConstraints);
			this.Create(relationName, new DataColumn[] { parentColumn }, new DataColumn[] { childColumn }, createConstraints);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified <see cref="T:System.Data.DataRelation" /> name and matched arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="relationName">The name of the relation. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects contains null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The <see cref="T:System.Data.DataColumn" /> objects have different data types -Or- One or both of the arrays are not composed of distinct columns from the same table.-Or- The tables do not belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x060003E3 RID: 995 RVA: 0x00014C4E File Offset: 0x00012E4E
		public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns)
			: this(relationName, parentColumns, childColumns, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelation" /> class using the specified name, matched arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects, and value that indicates whether to create constraints.</summary>
		/// <param name="relationName">The name of the relation. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="createConstraints">A value that indicates whether to create constraints. true, if constraints are created. Otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the <see cref="T:System.Data.DataColumn" /> objects is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types -Or- The tables do not belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x060003E4 RID: 996 RVA: 0x00014C5A File Offset: 0x00012E5A
		public DataRelation(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			this.Create(relationName, parentColumns, childColumns, createConstraints);
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio environment.</summary>
		/// <param name="relationName">The name of the relation. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the parent table of the relation. </param>
		/// <param name="childTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the child table of the relation. </param>
		/// <param name="parentColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the parent <see cref="T:System.Data.DataTable" /> of the relation. </param>
		/// <param name="childColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the child <see cref="T:System.Data.DataTable" /> of the relation. </param>
		/// <param name="nested">A value that indicates whether relationships are nested. </param>
		// Token: 0x060003E5 RID: 997 RVA: 0x00014C90 File Offset: 0x00012E90
		[Browsable(false)]
		public DataRelation(string relationName, string parentTableName, string childTableName, string[] parentColumnNames, string[] childColumnNames, bool nested)
		{
			this._relationName = relationName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._parentTableName = parentTableName;
			this._childTableName = childTableName;
			this._nested = nested;
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio environment.</summary>
		/// <param name="relationName">The name of the <see cref="T:System.Data.DataRelation" />. If null or an empty string (""), a default name will be given when the created object is added to the <see cref="T:System.Data.DataRelationCollection" />. </param>
		/// <param name="parentTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the parent table of the relation.</param>
		/// <param name="parentTableNamespace">The name of the parent table namespace.</param>
		/// <param name="childTableName">The name of the <see cref="T:System.Data.DataTable" /> that is the child table of the relation. </param>
		/// <param name="childTableNamespace">The name of the child table namespace.</param>
		/// <param name="parentColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the parent <see cref="T:System.Data.DataTable" /> of the relation.</param>
		/// <param name="childColumnNames">An array of <see cref="T:System.Data.DataColumn" /> object names in the child <see cref="T:System.Data.DataTable" /> of the relation.</param>
		/// <param name="nested">A value that indicates whether relationships are nested.</param>
		// Token: 0x060003E6 RID: 998 RVA: 0x00014CF4 File Offset: 0x00012EF4
		[Browsable(false)]
		public DataRelation(string relationName, string parentTableName, string parentTableNamespace, string childTableName, string childTableNamespace, string[] parentColumnNames, string[] childColumnNames, bool nested)
		{
			this._relationName = relationName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._parentTableName = parentTableName;
			this._childTableName = childTableName;
			this._parentTableNamespace = parentTableNamespace;
			this._childTableNamespace = childTableNamespace;
			this._nested = nested;
		}

		/// <summary>Gets the child <see cref="T:System.Data.DataColumn" /> objects of this relation.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00014D66 File Offset: 0x00012F66
		public virtual DataColumn[] ChildColumns
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKey.ToArray();
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00014D79 File Offset: 0x00012F79
		internal DataColumn[] ChildColumnsReference
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKey.ColumnsReference;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00014D8C File Offset: 0x00012F8C
		internal DataKey ChildKey
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKey;
			}
		}

		/// <summary>Gets the child table of this relation.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that is the child table of the relation.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00014D9A File Offset: 0x00012F9A
		public virtual DataTable ChildTable
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKey.Table;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataSet" /> to which the <see cref="T:System.Data.DataRelation" /> belongs.</summary>
		/// <returns>A <see cref="T:System.Data.DataSet" /> to which the <see cref="T:System.Data.DataRelation" /> belongs.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x00014DAD File Offset: 0x00012FAD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual DataSet DataSet
		{
			get
			{
				this.CheckStateForProperty();
				return this._dataSet;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00014DBB File Offset: 0x00012FBB
		internal string[] ParentColumnNames
		{
			get
			{
				return this._parentKey.GetColumnNames();
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x00014DC8 File Offset: 0x00012FC8
		internal string[] ChildColumnNames
		{
			get
			{
				return this._childKey.GetColumnNames();
			}
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00014DD8 File Offset: 0x00012FD8
		private static bool IsKeyNull(object[] values)
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

		// Token: 0x060003EF RID: 1007 RVA: 0x00014E00 File Offset: 0x00013000
		internal static DataRow[] GetChildRows(DataKey parentKey, DataKey childKey, DataRow parentRow, DataRowVersion version)
		{
			object[] keyValues = parentRow.GetKeyValues(parentKey, version);
			if (DataRelation.IsKeyNull(keyValues))
			{
				return childKey.Table.NewRowArray(0);
			}
			return childKey.GetSortIndex((version == DataRowVersion.Original) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows).GetRows(keyValues);
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00014E48 File Offset: 0x00013048
		internal static DataRow[] GetParentRows(DataKey parentKey, DataKey childKey, DataRow childRow, DataRowVersion version)
		{
			object[] keyValues = childRow.GetKeyValues(childKey, version);
			if (DataRelation.IsKeyNull(keyValues))
			{
				return parentKey.Table.NewRowArray(0);
			}
			return parentKey.GetSortIndex((version == DataRowVersion.Original) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows).GetRows(keyValues);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00014E90 File Offset: 0x00013090
		internal static DataRow GetParentRow(DataKey parentKey, DataKey childKey, DataRow childRow, DataRowVersion version)
		{
			if (!childRow.HasVersion((version == DataRowVersion.Original) ? DataRowVersion.Original : DataRowVersion.Current) && childRow._tempRecord == -1)
			{
				return null;
			}
			object[] keyValues = childRow.GetKeyValues(childKey, version);
			if (DataRelation.IsKeyNull(keyValues))
			{
				return null;
			}
			Index sortIndex = parentKey.GetSortIndex((version == DataRowVersion.Original) ? DataViewRowState.OriginalRows : DataViewRowState.CurrentRows);
			Range range = sortIndex.FindRecords(keyValues);
			if (range.IsNull)
			{
				return null;
			}
			if (range.Count > 1)
			{
				throw ExceptionBuilder.MultipleParents();
			}
			return parentKey.Table._recordManager[sortIndex.GetRecord(range.Min)];
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00014F2E File Offset: 0x0001312E
		internal void SetDataSet(DataSet dataSet)
		{
			if (this._dataSet != dataSet)
			{
				this._dataSet = dataSet;
			}
		}

		/// <summary>Gets an array of <see cref="T:System.Data.DataColumn" /> objects that are the parent columns of this <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that are the parent columns of this <see cref="T:System.Data.DataRelation" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00014F40 File Offset: 0x00013140
		public virtual DataColumn[] ParentColumns
		{
			get
			{
				this.CheckStateForProperty();
				return this._parentKey.ToArray();
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00014F53 File Offset: 0x00013153
		internal DataColumn[] ParentColumnsReference
		{
			get
			{
				return this._parentKey.ColumnsReference;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x00014F60 File Offset: 0x00013160
		internal DataKey ParentKey
		{
			get
			{
				this.CheckStateForProperty();
				return this._parentKey;
			}
		}

		/// <summary>Gets the parent <see cref="T:System.Data.DataTable" /> of this <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that is the parent table of this relation.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00014F6E File Offset: 0x0001316E
		public virtual DataTable ParentTable
		{
			get
			{
				this.CheckStateForProperty();
				return this._parentKey.Table;
			}
		}

		/// <summary>Gets or sets the name used to retrieve a <see cref="T:System.Data.DataRelation" /> from the <see cref="T:System.Data.DataRelationCollection" />.</summary>
		/// <returns>The name of the a <see cref="T:System.Data.DataRelation" />.</returns>
		/// <exception cref="T:System.ArgumentException">null or empty string ("") was passed into a <see cref="T:System.Data.DataColumn" /> that is a <see cref="T:System.Data.DataRelation" />. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The <see cref="T:System.Data.DataRelation" /> belongs to a collection that already contains a <see cref="T:System.Data.DataRelation" /> with the same name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00014F81 File Offset: 0x00013181
		[DefaultValue("")]
		public virtual string RelationName
		{
			get
			{
				this.CheckStateForProperty();
				return this._relationName;
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00014F90 File Offset: 0x00013190
		internal void CheckNamespaceValidityForNestedRelations(string ns)
		{
			foreach (object obj in this.ChildTable.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if ((dataRelation == this || dataRelation.Nested) && dataRelation.ParentTable.Namespace != ns)
				{
					throw ExceptionBuilder.InValidNestedRelation(this.ChildTable.TableName);
				}
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00015018 File Offset: 0x00013218
		internal void CheckNestedRelations()
		{
			DataCommonEventSource.Log.Trace<int>("<ds.DataRelation.CheckNestedRelations|INFO> {0}", this.ObjectID);
			DataTable parentTable = this.ParentTable;
			if (this.ChildTable != this.ParentTable)
			{
				List<DataTable> list = new List<DataTable>();
				list.Add(this.ChildTable);
				for (int i = 0; i < list.Count; i++)
				{
					foreach (DataRelation dataRelation in list[i].NestedParentRelations)
					{
						if (dataRelation.ParentTable == this.ChildTable && dataRelation.ChildTable != this.ChildTable)
						{
							throw ExceptionBuilder.LoopInNestedRelations(this.ChildTable.TableName);
						}
						if (!list.Contains(dataRelation.ParentTable))
						{
							list.Add(dataRelation.ParentTable);
						}
					}
				}
				return;
			}
			if (string.Compare(this.ChildTable.TableName, this.ChildTable.DataSet.DataSetName, true, this.ChildTable.DataSet.Locale) == 0)
			{
				throw ExceptionBuilder.SelfnestedDatasetConflictingName(this.ChildTable.TableName);
			}
		}

		/// <summary>Gets or sets a value that indicates whether <see cref="T:System.Data.DataRelation" /> objects are nested.</summary>
		/// <returns>true, if <see cref="T:System.Data.DataRelation" /> objects are nested; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00015122 File Offset: 0x00013322
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x00015130 File Offset: 0x00013330
		[DefaultValue(false)]
		public virtual bool Nested
		{
			get
			{
				this.CheckStateForProperty();
				return this._nested;
			}
			set
			{
				long num = DataCommonEventSource.Log.EnterScope<int, bool>("<ds.DataRelation.set_Nested|API> {0}, {1}", this.ObjectID, value);
				try
				{
					if (this._nested != value)
					{
						if (this._dataSet != null && value)
						{
							if (this.ChildTable.IsNamespaceInherited())
							{
								this.CheckNamespaceValidityForNestedRelations(this.ParentTable.Namespace);
							}
							ForeignKeyConstraint foreignKeyConstraint = this.ChildTable.Constraints.FindForeignKeyConstraint(this.ChildKey.ColumnsReference, this.ParentKey.ColumnsReference);
							if (foreignKeyConstraint != null)
							{
								foreignKeyConstraint.CheckConstraint();
							}
							this.ValidateMultipleNestedRelations();
						}
						if (!value && this._parentKey.ColumnsReference[0].ColumnMapping == MappingType.Hidden)
						{
							throw ExceptionBuilder.RelationNestedReadOnly();
						}
						if (value)
						{
							this.ParentTable.Columns.RegisterColumnName(this.ChildTable.TableName, null);
						}
						else
						{
							this.ParentTable.Columns.UnregisterName(this.ChildTable.TableName);
						}
						this.RaisePropertyChanging("Nested");
						if (value)
						{
							this.CheckNestedRelations();
							if (this.DataSet != null)
							{
								if (this.ParentTable == this.ChildTable)
								{
									foreach (object obj in this.ChildTable.Rows)
									{
										((DataRow)obj).CheckForLoops(this);
									}
									if (this.ChildTable.DataSet != null && string.Compare(this.ChildTable.TableName, this.ChildTable.DataSet.DataSetName, true, this.ChildTable.DataSet.Locale) == 0)
									{
										throw ExceptionBuilder.DatasetConflictingName(this._dataSet.DataSetName);
									}
									this.ChildTable._fNestedInDataset = false;
								}
								else
								{
									foreach (object obj2 in this.ChildTable.Rows)
									{
										((DataRow)obj2).GetParentRow(this);
									}
								}
							}
							DataTable parentTable = this.ParentTable;
							int num2 = parentTable.ElementColumnCount;
							parentTable.ElementColumnCount = num2 + 1;
						}
						else
						{
							DataTable parentTable2 = this.ParentTable;
							int num2 = parentTable2.ElementColumnCount;
							parentTable2.ElementColumnCount = num2 - 1;
						}
						this._nested = value;
						this.ChildTable.CacheNestedParent();
						if (value && string.IsNullOrEmpty(this.ChildTable.Namespace) && (this.ChildTable.NestedParentsCount > 1 || (this.ChildTable.NestedParentsCount > 0 && !this.ChildTable.DataSet.Relations.Contains(this.RelationName))))
						{
							string text = null;
							foreach (object obj3 in this.ChildTable.ParentRelations)
							{
								DataRelation dataRelation = (DataRelation)obj3;
								if (dataRelation.Nested)
								{
									if (text == null)
									{
										text = dataRelation.ParentTable.Namespace;
									}
									else if (string.Compare(text, dataRelation.ParentTable.Namespace, StringComparison.Ordinal) != 0)
									{
										this._nested = false;
										throw ExceptionBuilder.InvalidParentNamespaceinNestedRelation(this.ChildTable.TableName);
									}
								}
							}
							if (this.CheckMultipleNested && this.ChildTable._tableNamespace != null && this.ChildTable._tableNamespace.Length == 0)
							{
								throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
							}
							this.ChildTable._tableNamespace = null;
						}
					}
				}
				finally
				{
					DataCommonEventSource.Log.ExitScope(num);
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.UniqueConstraint" /> that guarantees that values in the parent column of a <see cref="T:System.Data.DataRelation" /> are unique.</summary>
		/// <returns>A <see cref="T:System.Data.UniqueConstraint" /> that makes sure that values in a parent column are unique.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00015514 File Offset: 0x00013714
		public virtual UniqueConstraint ParentKeyConstraint
		{
			get
			{
				this.CheckStateForProperty();
				return this._parentKeyConstraint;
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00015522 File Offset: 0x00013722
		internal void SetParentKeyConstraint(UniqueConstraint value)
		{
			this._parentKeyConstraint = value;
		}

		/// <summary>Gets the <see cref="T:System.Data.ForeignKeyConstraint" /> for the relation.</summary>
		/// <returns>A ForeignKeyConstraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0001552B File Offset: 0x0001372B
		public virtual ForeignKeyConstraint ChildKeyConstraint
		{
			get
			{
				this.CheckStateForProperty();
				return this._childKeyConstraint;
			}
		}

		/// <summary>Gets the collection that stores customized properties.</summary>
		/// <returns>A <see cref="T:System.Data.PropertyCollection" /> that contains customized properties.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0001553C File Offset: 0x0001373C
		[Browsable(false)]
		public PropertyCollection ExtendedProperties
		{
			get
			{
				PropertyCollection propertyCollection;
				if ((propertyCollection = this._extendedProperties) == null)
				{
					propertyCollection = (this._extendedProperties = new PropertyCollection());
				}
				return propertyCollection;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00015561 File Offset: 0x00013761
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x00015569 File Offset: 0x00013769
		internal bool CheckMultipleNested
		{
			get
			{
				return this._checkMultipleNested;
			}
			set
			{
				this._checkMultipleNested = value;
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00015572 File Offset: 0x00013772
		internal void SetChildKeyConstraint(ForeignKeyConstraint value)
		{
			this._childKeyConstraint = value;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001557C File Offset: 0x0001377C
		internal void CheckState()
		{
			if (this._dataSet == null)
			{
				this._parentKey.CheckState();
				this._childKey.CheckState();
				if (this._parentKey.Table.DataSet != this._childKey.Table.DataSet)
				{
					throw ExceptionBuilder.RelationDataSetMismatch();
				}
				if (this._childKey.ColumnsEqual(this._parentKey))
				{
					throw ExceptionBuilder.KeyColumnsIdentical();
				}
				for (int i = 0; i < this._parentKey.ColumnsReference.Length; i++)
				{
					if (this._parentKey.ColumnsReference[i].DataType != this._childKey.ColumnsReference[i].DataType || (this._parentKey.ColumnsReference[i].DataType == typeof(DateTime) && this._parentKey.ColumnsReference[i].DateTimeMode != this._childKey.ColumnsReference[i].DateTimeMode && (this._parentKey.ColumnsReference[i].DateTimeMode & this._childKey.ColumnsReference[i].DateTimeMode) != DataSetDateTime.Unspecified))
					{
						throw ExceptionBuilder.ColumnsTypeMismatch();
					}
				}
			}
		}

		/// <summary>This method supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <exception cref="T:System.Data.DataException">The parent and child tables belong to different <see cref="T:System.Data.DataSet" /> objects.-Or- One or more pairs of parent and child <see cref="T:System.Data.DataColumn" /> objects have mismatched data types.-Or- The parent and child <see cref="T:System.Data.DataColumn" /> objects are identical. </exception>
		// Token: 0x06000404 RID: 1028 RVA: 0x000156AC File Offset: 0x000138AC
		protected void CheckStateForProperty()
		{
			try
			{
				this.CheckState();
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				throw ExceptionBuilder.BadObjectPropertyAccess(ex.Message);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000156F8 File Offset: 0x000138F8
		private void Create(string relationName, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, string, bool>("<ds.DataRelation.Create|INFO> {0}, relationName='{1}', createConstraints={2}", this.ObjectID, relationName, createConstraints);
			try
			{
				this._parentKey = new DataKey(parentColumns, true);
				this._childKey = new DataKey(childColumns, true);
				if (parentColumns.Length != childColumns.Length)
				{
					throw ExceptionBuilder.KeyLengthMismatch();
				}
				for (int i = 0; i < parentColumns.Length; i++)
				{
					if (parentColumns[i].Table.DataSet == null || childColumns[i].Table.DataSet == null)
					{
						throw ExceptionBuilder.ParentOrChildColumnsDoNotHaveDataSet();
					}
				}
				this.CheckState();
				this._relationName = ((relationName == null) ? "" : relationName);
				this._createConstraints = createConstraints;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000157B8 File Offset: 0x000139B8
		internal DataRelation Clone(DataSet destination)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataRelation.Clone|INFO> {0}, destination={1}", this.ObjectID, (destination != null) ? destination.ObjectID : 0);
			DataTable dataTable = destination.Tables[this.ParentTable.TableName, this.ParentTable.Namespace];
			DataTable dataTable2 = destination.Tables[this.ChildTable.TableName, this.ChildTable.Namespace];
			int num = this._parentKey.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num];
			DataColumn[] array2 = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				array[i] = dataTable.Columns[this.ParentKey.ColumnsReference[i].ColumnName];
				array2[i] = dataTable2.Columns[this.ChildKey.ColumnsReference[i].ColumnName];
			}
			DataRelation dataRelation = new DataRelation(this._relationName, array, array2, false);
			dataRelation.CheckMultipleNested = false;
			dataRelation.Nested = this.Nested;
			dataRelation.CheckMultipleNested = true;
			if (this._extendedProperties != null)
			{
				foreach (object obj in this._extendedProperties.Keys)
				{
					dataRelation.ExtendedProperties[obj] = this._extendedProperties[obj];
				}
			}
			return dataRelation;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="pcevent">Parameter reference.</param>
		// Token: 0x06000407 RID: 1031 RVA: 0x00015944 File Offset: 0x00013B44
		protected internal void OnPropertyChanging(PropertyChangedEventArgs pcevent)
		{
			if (this.PropertyChanging != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelation.OnPropertyChanging|INFO> {0}", this.ObjectID);
				this.PropertyChanging(this, pcevent);
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="name">Parameter reference.</param>
		// Token: 0x06000408 RID: 1032 RVA: 0x00015970 File Offset: 0x00013B70
		protected internal void RaisePropertyChanging(string name)
		{
			this.OnPropertyChanging(new PropertyChangedEventArgs(name));
		}

		/// <summary>Gets the <see cref="P:System.Data.DataRelation.RelationName" />, if one exists.</summary>
		/// <returns>The value of the <see cref="P:System.Data.DataRelation.RelationName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000409 RID: 1033 RVA: 0x0001597E File Offset: 0x00013B7E
		public override string ToString()
		{
			return this.RelationName;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00015988 File Offset: 0x00013B88
		internal void ValidateMultipleNestedRelations()
		{
			if (!this.Nested || !this.CheckMultipleNested)
			{
				return;
			}
			if (this.ChildTable.NestedParentRelations.Length != 0)
			{
				DataColumn[] childColumns = this.ChildColumns;
				if (childColumns.Length != 1 || !this.IsAutoGenerated(childColumns[0]))
				{
					throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
				}
				if (!XmlTreeGen.AutoGenerated(this))
				{
					throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
				}
				foreach (object obj in this.ChildTable.Constraints)
				{
					Constraint constraint = (Constraint)obj;
					if (constraint is ForeignKeyConstraint)
					{
						if (!XmlTreeGen.AutoGenerated((ForeignKeyConstraint)constraint, true))
						{
							throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
						}
					}
					else if (!XmlTreeGen.AutoGenerated((UniqueConstraint)constraint))
					{
						throw ExceptionBuilder.TableCantBeNestedInTwoTables(this.ChildTable.TableName);
					}
				}
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00015A88 File Offset: 0x00013C88
		private bool IsAutoGenerated(DataColumn col)
		{
			if (col.ColumnMapping != MappingType.Hidden)
			{
				return false;
			}
			if (col.DataType != typeof(int))
			{
				return false;
			}
			string text = col.Table.TableName + "_Id";
			if (col.ColumnName == text || col.ColumnName == text + "_0")
			{
				return true;
			}
			text = this.ParentColumnsReference[0].Table.TableName + "_Id";
			return col.ColumnName == text || col.ColumnName == text + "_0";
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x00015B3D File Offset: 0x00013D3D
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x0400012C RID: 300
		private DataSet _dataSet;

		// Token: 0x0400012D RID: 301
		internal PropertyCollection _extendedProperties;

		// Token: 0x0400012E RID: 302
		internal string _relationName = string.Empty;

		// Token: 0x0400012F RID: 303
		private DataKey _childKey;

		// Token: 0x04000130 RID: 304
		private DataKey _parentKey;

		// Token: 0x04000131 RID: 305
		private UniqueConstraint _parentKeyConstraint;

		// Token: 0x04000132 RID: 306
		private ForeignKeyConstraint _childKeyConstraint;

		// Token: 0x04000133 RID: 307
		internal string[] _parentColumnNames;

		// Token: 0x04000134 RID: 308
		internal string[] _childColumnNames;

		// Token: 0x04000135 RID: 309
		internal string _parentTableName;

		// Token: 0x04000136 RID: 310
		internal string _childTableName;

		// Token: 0x04000137 RID: 311
		internal string _parentTableNamespace;

		// Token: 0x04000138 RID: 312
		internal string _childTableNamespace;

		// Token: 0x04000139 RID: 313
		internal bool _nested;

		// Token: 0x0400013A RID: 314
		internal bool _createConstraints;

		// Token: 0x0400013B RID: 315
		private bool _checkMultipleNested = true;

		// Token: 0x0400013C RID: 316
		private static int s_objectTypeCount;

		// Token: 0x0400013D RID: 317
		private readonly int _objectID = Interlocked.Increment(ref DataRelation.s_objectTypeCount);

		// Token: 0x0400013E RID: 318
		[CompilerGenerated]
		private PropertyChangedEventHandler PropertyChanging;
	}
}
