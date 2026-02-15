using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	/// <summary>Represents a collection of constraints for a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200002E RID: 46
	[DefaultEvent("CollectionChanged")]
	public sealed class ConstraintCollection : InternalDataCollectionBase
	{
		// Token: 0x0600035A RID: 858 RVA: 0x000127C6 File Offset: 0x000109C6
		internal ConstraintCollection(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600035B RID: 859 RVA: 0x000127E7 File Offset: 0x000109E7
		protected override ArrayList List
		{
			get
			{
				return this._list;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Constraint" /> from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.Constraint" /> at the specified index.</returns>
		/// <param name="index">The index of the constraint to return. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700007E RID: 126
		public Constraint this[int index]
		{
			get
			{
				if (index >= 0 && index < this.List.Count)
				{
					return (Constraint)this.List[index];
				}
				throw ExceptionBuilder.ConstraintOutOfRange(index);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0001281B File Offset: 0x00010A1B
		internal DataTable Table
		{
			get
			{
				return this._table;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Constraint" /> from the collection with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.Constraint" /> with the specified name; otherwise a null value if the <see cref="T:System.Data.Constraint" /> does not exist.</returns>
		/// <param name="name">The <see cref="P:System.Data.Constraint.ConstraintName" /> of the constraint to return. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000080 RID: 128
		public Constraint this[string name]
		{
			get
			{
				int num = this.InternalIndexOf(name);
				if (num == -2)
				{
					throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
				}
				if (num >= 0)
				{
					return (Constraint)this.List[num];
				}
				return null;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.Constraint" /> object to the collection.</summary>
		/// <param name="constraint">The Constraint to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="constraint" /> argument is null. </exception>
		/// <exception cref="T:System.ArgumentException">The constraint already belongs to this collection, or belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a constraint with the same name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600035F RID: 863 RVA: 0x0001285C File Offset: 0x00010A5C
		public void Add(Constraint constraint)
		{
			this.Add(constraint, true);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00012868 File Offset: 0x00010A68
		internal void Add(Constraint constraint, bool addUniqueWhenAddingForeign)
		{
			if (constraint == null)
			{
				throw ExceptionBuilder.ArgumentNull("constraint");
			}
			if (this.FindConstraint(constraint) != null)
			{
				throw ExceptionBuilder.DuplicateConstraint(this.FindConstraint(constraint).ConstraintName);
			}
			if (1 < this._table.NestedParentRelations.Length && !this.AutoGenerated(constraint))
			{
				throw ExceptionBuilder.CantAddConstraintToMultipleNestedTable(this._table.TableName);
			}
			if (constraint is UniqueConstraint)
			{
				if (((UniqueConstraint)constraint)._bPrimaryKey && this.Table._primaryKey != null)
				{
					throw ExceptionBuilder.AddPrimaryKeyConstraint();
				}
				this.AddUniqueConstraint((UniqueConstraint)constraint);
			}
			else if (constraint is ForeignKeyConstraint)
			{
				ForeignKeyConstraint foreignKeyConstraint = (ForeignKeyConstraint)constraint;
				if (addUniqueWhenAddingForeign && foreignKeyConstraint.RelatedTable.Constraints.FindKeyConstraint(foreignKeyConstraint.RelatedColumnsReference) == null)
				{
					if (constraint.ConstraintName.Length == 0)
					{
						constraint.ConstraintName = this.AssignName();
					}
					else
					{
						this.RegisterName(constraint.ConstraintName);
					}
					UniqueConstraint uniqueConstraint = new UniqueConstraint(foreignKeyConstraint.RelatedColumnsReference);
					foreignKeyConstraint.RelatedTable.Constraints.Add(uniqueConstraint);
				}
				this.AddForeignKeyConstraint((ForeignKeyConstraint)constraint);
			}
			this.BaseAdd(constraint);
			this.ArrayAdd(constraint);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, constraint));
			if (constraint is UniqueConstraint && ((UniqueConstraint)constraint)._bPrimaryKey)
			{
				this.Table.PrimaryKey = ((UniqueConstraint)constraint).ColumnsReference;
			}
		}

		/// <summary>Constructs a new <see cref="T:System.Data.UniqueConstraint" /> with the specified name, array of <see cref="T:System.Data.DataColumn" /> objects, and value that indicates whether the column is a primary key, and adds it to the collection.</summary>
		/// <returns>A new UniqueConstraint.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.UniqueConstraint" />. </param>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to which the constraint applies. </param>
		/// <param name="primaryKey">Specifies whether the column should be the primary key. If true, the column will be a primary key column.</param>
		/// <exception cref="T:System.ArgumentException">The constraint already belongs to this collection.-Or- The constraint belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a constraint with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000361 RID: 865 RVA: 0x000129C0 File Offset: 0x00010BC0
		public Constraint Add(string name, DataColumn[] columns, bool primaryKey)
		{
			UniqueConstraint uniqueConstraint = new UniqueConstraint(name, columns);
			this.Add(uniqueConstraint);
			if (primaryKey)
			{
				this.Table.PrimaryKey = columns;
			}
			return uniqueConstraint;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000129EC File Offset: 0x00010BEC
		private void AddUniqueConstraint(UniqueConstraint constraint)
		{
			DataColumn[] columnsReference = constraint.ColumnsReference;
			for (int i = 0; i < columnsReference.Length; i++)
			{
				if (columnsReference[i].Table != this._table)
				{
					throw ExceptionBuilder.ConstraintForeignTable();
				}
			}
			constraint.ConstraintIndexInitialize();
			if (!constraint.CanEnableConstraint())
			{
				constraint.ConstraintIndexClear();
				throw ExceptionBuilder.UniqueConstraintViolation();
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00012A3E File Offset: 0x00010C3E
		private void AddForeignKeyConstraint(ForeignKeyConstraint constraint)
		{
			if (!constraint.CanEnableConstraint())
			{
				throw ExceptionBuilder.ConstraintParentValues();
			}
			constraint.CheckCanAddToCollection(this);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00012A58 File Offset: 0x00010C58
		private bool AutoGenerated(Constraint constraint)
		{
			ForeignKeyConstraint foreignKeyConstraint = constraint as ForeignKeyConstraint;
			if (foreignKeyConstraint != null)
			{
				return XmlTreeGen.AutoGenerated(foreignKeyConstraint, false);
			}
			return XmlTreeGen.AutoGenerated((UniqueConstraint)constraint);
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00012A82 File Offset: 0x00010C82
		private void ArrayAdd(Constraint constraint)
		{
			this.List.Add(constraint);
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00012A91 File Offset: 0x00010C91
		private void ArrayRemove(Constraint constraint)
		{
			this.List.Remove(constraint);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00012A9F File Offset: 0x00010C9F
		internal string AssignName()
		{
			string text = this.MakeName(this._defaultNameIndex);
			this._defaultNameIndex++;
			return text;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00012ABB File Offset: 0x00010CBB
		private void BaseAdd(Constraint constraint)
		{
			if (constraint == null)
			{
				throw ExceptionBuilder.ArgumentNull("constraint");
			}
			if (constraint.ConstraintName.Length == 0)
			{
				constraint.ConstraintName = this.AssignName();
			}
			else
			{
				this.RegisterName(constraint.ConstraintName);
			}
			constraint.InCollection = true;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00012AFC File Offset: 0x00010CFC
		private void BaseGroupSwitch(Constraint[] oldArray, int oldLength, Constraint[] newArray, int newLength)
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
				if (!flag)
				{
					this.BaseRemove(oldArray[i]);
					this.List.Remove(oldArray[i]);
				}
			}
			for (int k = 0; k < newLength; k++)
			{
				if (!newArray[k].InCollection)
				{
					this.BaseAdd(newArray[k]);
				}
				this.List.Add(newArray[k]);
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00012B8C File Offset: 0x00010D8C
		private void BaseRemove(Constraint constraint)
		{
			if (constraint == null)
			{
				throw ExceptionBuilder.ArgumentNull("constraint");
			}
			if (constraint.Table != this._table)
			{
				throw ExceptionBuilder.ConstraintRemoveFailed();
			}
			this.UnregisterName(constraint.ConstraintName);
			constraint.InCollection = false;
			if (constraint is UniqueConstraint)
			{
				for (int i = 0; i < this.Table.ChildRelations.Count; i++)
				{
					DataRelation dataRelation = this.Table.ChildRelations[i];
					if (dataRelation.ParentKeyConstraint == constraint)
					{
						dataRelation.SetParentKeyConstraint(null);
					}
				}
				((UniqueConstraint)constraint).ConstraintIndexClear();
				return;
			}
			if (constraint is ForeignKeyConstraint)
			{
				for (int j = 0; j < this.Table.ParentRelations.Count; j++)
				{
					DataRelation dataRelation2 = this.Table.ParentRelations[j];
					if (dataRelation2.ChildKeyConstraint == constraint)
					{
						dataRelation2.SetChildKeyConstraint(null);
					}
				}
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00012C64 File Offset: 0x00010E64
		internal bool CanRemove(Constraint constraint, bool fThrowException)
		{
			return constraint.CanBeRemovedFromCollection(this, fThrowException);
		}

		/// <summary>Clears the collection of any <see cref="T:System.Data.Constraint" /> objects.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600036C RID: 876 RVA: 0x00012C70 File Offset: 0x00010E70
		public void Clear()
		{
			if (this._table != null)
			{
				this._table.PrimaryKey = null;
				for (int i = 0; i < this._table.ParentRelations.Count; i++)
				{
					this._table.ParentRelations[i].SetChildKeyConstraint(null);
				}
				for (int j = 0; j < this._table.ChildRelations.Count; j++)
				{
					this._table.ChildRelations[j].SetParentKeyConstraint(null);
				}
			}
			if (this._table.fInitInProgress && this._delayLoadingConstraints != null)
			{
				this._delayLoadingConstraints = null;
				this._fLoadForeignKeyConstraintsOnly = false;
			}
			int count = this.List.Count;
			Constraint[] array = new Constraint[this.List.Count];
			this.List.CopyTo(array, 0);
			try
			{
				this.BaseGroupSwitch(array, count, null, 0);
			}
			catch (Exception ex) when (ADP.IsCatchableOrSecurityExceptionType(ex))
			{
				this.BaseGroupSwitch(null, 0, array, count);
				this.List.Clear();
				for (int k = 0; k < count; k++)
				{
					this.List.Add(array[k]);
				}
				throw;
			}
			this.List.Clear();
			this.OnCollectionChanged(InternalDataCollectionBase.s_refreshEventArgs);
		}

		/// <summary>Indicates whether the <see cref="T:System.Data.Constraint" /> object specified by name exists in the collection.</summary>
		/// <returns>true if the collection contains the specified constraint; otherwise, false.</returns>
		/// <param name="name">The <see cref="P:System.Data.Constraint.ConstraintName" /> of the constraint. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600036D RID: 877 RVA: 0x00012DC4 File Offset: 0x00010FC4
		public bool Contains(string name)
		{
			return this.InternalIndexOf(name) >= 0;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00012DD4 File Offset: 0x00010FD4
		internal bool Contains(string name, bool caseSensitive)
		{
			if (!caseSensitive)
			{
				return this.Contains(name);
			}
			int num = this.InternalIndexOf(name);
			return num >= 0 && name == ((Constraint)this.List[num]).ConstraintName;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00012E18 File Offset: 0x00011018
		internal Constraint FindConstraint(Constraint constraint)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				if (((Constraint)this.List[i]).Equals(constraint))
				{
					return (Constraint)this.List[i];
				}
			}
			return null;
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00012E6C File Offset: 0x0001106C
		internal UniqueConstraint FindKeyConstraint(DataColumn[] columns)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				UniqueConstraint uniqueConstraint = this.List[i] as UniqueConstraint;
				if (uniqueConstraint != null && ConstraintCollection.CompareArrays(uniqueConstraint.Key.ColumnsReference, columns))
				{
					return uniqueConstraint;
				}
			}
			return null;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00012EC0 File Offset: 0x000110C0
		internal UniqueConstraint FindKeyConstraint(DataColumn column)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				UniqueConstraint uniqueConstraint = this.List[i] as UniqueConstraint;
				if (uniqueConstraint != null && uniqueConstraint.Key.ColumnsReference.Length == 1 && uniqueConstraint.Key.ColumnsReference[0] == column)
				{
					return uniqueConstraint;
				}
			}
			return null;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00012F24 File Offset: 0x00011124
		internal ForeignKeyConstraint FindForeignKeyConstraint(DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				ForeignKeyConstraint foreignKeyConstraint = this.List[i] as ForeignKeyConstraint;
				if (foreignKeyConstraint != null && ConstraintCollection.CompareArrays(foreignKeyConstraint.ParentKey.ColumnsReference, parentColumns) && ConstraintCollection.CompareArrays(foreignKeyConstraint.ChildKey.ColumnsReference, childColumns))
				{
					return foreignKeyConstraint;
				}
			}
			return null;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00012F90 File Offset: 0x00011190
		private static bool CompareArrays(DataColumn[] a1, DataColumn[] a2)
		{
			if (a1.Length != a2.Length)
			{
				return false;
			}
			for (int i = 0; i < a1.Length; i++)
			{
				bool flag = false;
				for (int j = 0; j < a2.Length; j++)
				{
					if (a1[i] == a2[j])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00012FD8 File Offset: 0x000111D8
		internal int InternalIndexOf(string constraintName)
		{
			int num = -1;
			if (constraintName != null && 0 < constraintName.Length)
			{
				int count = this.List.Count;
				for (int i = 0; i < count; i++)
				{
					Constraint constraint = (Constraint)this.List[i];
					int num2 = base.NamesEqual(constraint.ConstraintName, constraintName, false, this._table.Locale);
					if (num2 == 1)
					{
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

		// Token: 0x06000375 RID: 885 RVA: 0x00013050 File Offset: 0x00011250
		private string MakeName(int index)
		{
			if (1 == index)
			{
				return "Constraint1";
			}
			return "Constraint" + index.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00013072 File Offset: 0x00011272
		private void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			CollectionChangeEventHandler onCollectionChanged = this._onCollectionChanged;
			if (onCollectionChanged == null)
			{
				return;
			}
			onCollectionChanged(this, ccevent);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00013088 File Offset: 0x00011288
		internal void RegisterName(string name)
		{
			int count = this.List.Count;
			for (int i = 0; i < count; i++)
			{
				if (base.NamesEqual(name, ((Constraint)this.List[i]).ConstraintName, true, this._table.Locale) != 0)
				{
					throw ExceptionBuilder.DuplicateConstraintName(((Constraint)this.List[i]).ConstraintName);
				}
			}
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex), true, this._table.Locale) != 0)
			{
				this._defaultNameIndex++;
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Constraint" /> from the collection.</summary>
		/// <param name="constraint">The <see cref="T:System.Data.Constraint" /> to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="constraint" /> argument is null. </exception>
		/// <exception cref="T:System.ArgumentException">The constraint does not belong to the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000378 RID: 888 RVA: 0x00013124 File Offset: 0x00011324
		public void Remove(Constraint constraint)
		{
			if (constraint == null)
			{
				throw ExceptionBuilder.ArgumentNull("constraint");
			}
			if (this.CanRemove(constraint, true))
			{
				this.BaseRemove(constraint);
				this.ArrayRemove(constraint);
				if (constraint is UniqueConstraint && ((UniqueConstraint)constraint).IsPrimaryKey)
				{
					this.Table.PrimaryKey = null;
				}
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, constraint));
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00013188 File Offset: 0x00011388
		internal void UnregisterName(string name)
		{
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex - 1), true, this._table.Locale) != 0)
			{
				do
				{
					this._defaultNameIndex--;
				}
				while (this._defaultNameIndex > 1 && !this.Contains(this.MakeName(this._defaultNameIndex - 1)));
			}
		}

		// Token: 0x0400010B RID: 267
		private readonly DataTable _table;

		// Token: 0x0400010C RID: 268
		private readonly ArrayList _list = new ArrayList();

		// Token: 0x0400010D RID: 269
		private int _defaultNameIndex = 1;

		// Token: 0x0400010E RID: 270
		private CollectionChangeEventHandler _onCollectionChanged;

		// Token: 0x0400010F RID: 271
		private Constraint[] _delayLoadingConstraints;

		// Token: 0x04000110 RID: 272
		private bool _fLoadForeignKeyConstraintsOnly;
	}
}
