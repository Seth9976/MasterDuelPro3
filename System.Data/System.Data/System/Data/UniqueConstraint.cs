using System;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Represents a restriction on a set of columns in which all values must be unique.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000A4 RID: 164
	[DefaultProperty("ConstraintName")]
	public class UniqueConstraint : Constraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		// Token: 0x060007E5 RID: 2021 RVA: 0x00028240 File Offset: 0x00026440
		public UniqueConstraint(DataColumn column)
		{
			this.Create(null, new DataColumn[] { column });
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name and array of <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		// Token: 0x060007E6 RID: 2022 RVA: 0x00028266 File Offset: 0x00026466
		public UniqueConstraint(string name, DataColumn[] columns)
		{
			this.Create(name, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the given array of <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		// Token: 0x060007E7 RID: 2023 RVA: 0x00028276 File Offset: 0x00026476
		public UniqueConstraint(DataColumn[] columns)
		{
			this.Create(null, columns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="columnNames">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x060007E8 RID: 2024 RVA: 0x00028286 File Offset: 0x00026486
		[Browsable(false)]
		public UniqueConstraint(string name, string[] columnNames, bool isPrimaryKey)
		{
			this._constraintName = name;
			this._columnNames = columnNames;
			this._bPrimaryKey = isPrimaryKey;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x060007E9 RID: 2025 RVA: 0x000282A3 File Offset: 0x000264A3
		public UniqueConstraint(string name, DataColumn[] columns, bool isPrimaryKey)
		{
			this._bPrimaryKey = isPrimaryKey;
			this.Create(name, columns);
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x000282BA File Offset: 0x000264BA
		internal string[] ColumnNames
		{
			get
			{
				return this._key.GetColumnNames();
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x000282C7 File Offset: 0x000264C7
		internal Index ConstraintIndex
		{
			get
			{
				return this._constraintIndex;
			}
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000282CF File Offset: 0x000264CF
		internal void ConstraintIndexClear()
		{
			if (this._constraintIndex != null)
			{
				this._constraintIndex.RemoveRef();
				this._constraintIndex = null;
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000282EC File Offset: 0x000264EC
		internal void ConstraintIndexInitialize()
		{
			if (this._constraintIndex == null)
			{
				this._constraintIndex = this._key.GetSortIndex();
				this._constraintIndex.AddRef();
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00028312 File Offset: 0x00026512
		internal override void CheckState()
		{
			this.NonVirtualCheckState();
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0002831A File Offset: 0x0002651A
		private void NonVirtualCheckState()
		{
			this._key.CheckState();
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00003FD2 File Offset: 0x000021D2
		internal override void CheckCanAddToCollection(ConstraintCollection constraints)
		{
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00028328 File Offset: 0x00026528
		internal override bool CanBeRemovedFromCollection(ConstraintCollection constraints, bool fThrowException)
		{
			if (!this.Equals(constraints.Table._primaryKey))
			{
				ParentForeignKeyConstraintEnumerator parentForeignKeyConstraintEnumerator = new ParentForeignKeyConstraintEnumerator(this.Table.DataSet, this.Table);
				while (parentForeignKeyConstraintEnumerator.GetNext())
				{
					ForeignKeyConstraint foreignKeyConstraint = parentForeignKeyConstraintEnumerator.GetForeignKeyConstraint();
					if (this._key.ColumnsEqual(foreignKeyConstraint.ParentKey))
					{
						if (!fThrowException)
						{
							return false;
						}
						throw ExceptionBuilder.NeededForForeignKeyConstraint(this, foreignKeyConstraint);
					}
				}
				return true;
			}
			if (!fThrowException)
			{
				return false;
			}
			throw ExceptionBuilder.RemovePrimaryKey(constraints.Table);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000283A2 File Offset: 0x000265A2
		internal override bool CanEnableConstraint()
		{
			return !this.Table.EnforceConstraints || this.ConstraintIndex.CheckUnique();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000283C0 File Offset: 0x000265C0
		internal override bool IsConstraintViolated()
		{
			bool flag = false;
			Index constraintIndex = this.ConstraintIndex;
			if (constraintIndex.HasDuplicates)
			{
				object[] uniqueKeyValues = constraintIndex.GetUniqueKeyValues();
				for (int i = 0; i < uniqueKeyValues.Length; i++)
				{
					Range range = constraintIndex.FindRecords((object[])uniqueKeyValues[i]);
					if (1 < range.Count)
					{
						DataRow[] rows = constraintIndex.GetRows(range);
						string text = ExceptionBuilder.UniqueConstraintViolationText(this._key.ColumnsReference, (object[])uniqueKeyValues[i]);
						for (int j = 0; j < rows.Length; j++)
						{
							rows[j].RowError = text;
							foreach (DataColumn dataColumn in this._key.ColumnsReference)
							{
								rows[j].SetColumnError(dataColumn, text);
							}
						}
						flag = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00028494 File Offset: 0x00026694
		internal override void CheckConstraint(DataRow row, DataRowAction action)
		{
			if (this.Table.EnforceConstraints && (action == DataRowAction.Add || action == DataRowAction.Change || (action == DataRowAction.Rollback && row._tempRecord != -1)) && row.HaveValuesChanged(this.ColumnsReference) && this.ConstraintIndex.IsKeyRecordInIndex(row.GetDefaultRecord()))
			{
				object[] columnValues = row.GetColumnValues(this.ColumnsReference);
				throw ExceptionBuilder.ConstraintViolation(this.ColumnsReference, columnValues);
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x000284FF File Offset: 0x000266FF
		internal override bool ContainsColumn(DataColumn column)
		{
			return this._key.ContainsColumn(column);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x000211A7 File Offset: 0x0001F3A7
		internal override Constraint Clone(DataSet destination)
		{
			return this.Clone(destination, false);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00028510 File Offset: 0x00026710
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
			int num2 = this.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num2];
			for (int i = 0; i < num2; i++)
			{
				DataColumn dataColumn = this.ColumnsReference[i];
				num = dataTable.Columns.IndexOf(dataColumn.ColumnName);
				if (num < 0)
				{
					return null;
				}
				array[i] = dataTable.Columns[num];
			}
			UniqueConstraint uniqueConstraint = new UniqueConstraint(this.ConstraintName, array);
			foreach (object obj in base.ExtendedProperties.Keys)
			{
				uniqueConstraint.ExtendedProperties[obj] = base.ExtendedProperties[obj];
			}
			return uniqueConstraint;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0002863C File Offset: 0x0002683C
		internal UniqueConstraint Clone(DataTable table)
		{
			int num = this.ColumnsReference.Length;
			DataColumn[] array = new DataColumn[num];
			for (int i = 0; i < num; i++)
			{
				DataColumn dataColumn = this.ColumnsReference[i];
				int num2 = table.Columns.IndexOf(dataColumn.ColumnName);
				if (num2 < 0)
				{
					return null;
				}
				array[i] = table.Columns[num2];
			}
			UniqueConstraint uniqueConstraint = new UniqueConstraint(this.ConstraintName, array);
			foreach (object obj in base.ExtendedProperties.Keys)
			{
				uniqueConstraint.ExtendedProperties[obj] = base.ExtendedProperties[obj];
			}
			return uniqueConstraint;
		}

		/// <summary>Gets the array of columns that this constraint affects.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00028710 File Offset: 0x00026910
		[ReadOnly(true)]
		public virtual DataColumn[] Columns
		{
			get
			{
				return this._key.ToArray();
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0002871D File Offset: 0x0002691D
		internal DataColumn[] ColumnsReference
		{
			get
			{
				return this._key.ColumnsReference;
			}
		}

		/// <summary>Gets a value indicating whether or not the constraint is on a primary key.</summary>
		/// <returns>true, if the constraint is on a primary key; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x0002872A File Offset: 0x0002692A
		public bool IsPrimaryKey
		{
			get
			{
				return this.Table != null && this == this.Table._primaryKey;
			}
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00028744 File Offset: 0x00026944
		private void Create(string constraintName, DataColumn[] columns)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				if (columns[i].Computed)
				{
					throw ExceptionBuilder.ExpressionInConstraint(columns[i]);
				}
			}
			this._key = new DataKey(columns, true);
			this.ConstraintName = constraintName;
			this.NonVirtualCheckState();
		}

		/// <summary>Compares this constraint to a second to determine if both are identical.</summary>
		/// <returns>true, if the contraints are equal; otherwise, false.</returns>
		/// <param name="key2">The object to which this <see cref="T:System.Data.UniqueConstraint" /> is compared. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007FD RID: 2045 RVA: 0x0002878C File Offset: 0x0002698C
		public override bool Equals(object key2)
		{
			return key2 is UniqueConstraint && this.Key.ColumnsEqual(((UniqueConstraint)key2).Key);
		}

		/// <summary>Gets the hash code of this instance of the <see cref="T:System.Data.UniqueConstraint" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007FE RID: 2046 RVA: 0x000215CE File Offset: 0x0001F7CE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x17000144 RID: 324
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x000287BC File Offset: 0x000269BC
		internal override bool InCollection
		{
			set
			{
				base.InCollection = value;
				if (this._key.ColumnsReference.Length == 1)
				{
					this._key.ColumnsReference[0].InternalUnique(value);
				}
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x000287E8 File Offset: 0x000269E8
		internal DataKey Key
		{
			get
			{
				return this._key;
			}
		}

		/// <summary>Gets the table to which this constraint belongs.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> to which the constraint belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000801 RID: 2049 RVA: 0x000287F0 File Offset: 0x000269F0
		[ReadOnly(true)]
		public override DataTable Table
		{
			get
			{
				if (this._key.HasValue)
				{
					return this._key.Table;
				}
				return null;
			}
		}

		// Token: 0x04000330 RID: 816
		private DataKey _key;

		// Token: 0x04000331 RID: 817
		private Index _constraintIndex;

		// Token: 0x04000332 RID: 818
		internal bool _bPrimaryKey;

		// Token: 0x04000333 RID: 819
		internal string _constraintName;

		// Token: 0x04000334 RID: 820
		internal string[] _columnNames;
	}
}
