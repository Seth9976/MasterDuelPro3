using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Threading;

namespace System.Data
{
	/// <summary>Represents the collection of <see cref="T:System.Data.DataRelation" /> objects for this <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003C RID: 60
	[DefaultEvent("CollectionChanged")]
	[DefaultProperty("Table")]
	public abstract class DataRelationCollection : InternalDataCollectionBase
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00015B45 File Offset: 0x00013D45
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataRelation" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.DataRelation" />, or a null value if the specified <see cref="T:System.Data.DataRelation" /> does not exist.</returns>
		/// <param name="index">The zero-based index to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A7 RID: 167
		public abstract DataRelation this[int index] { get; }

		/// <summary>Gets the <see cref="T:System.Data.DataRelation" /> object specified by name.</summary>
		/// <returns>The named <see cref="T:System.Data.DataRelation" />, or a null value if the specified <see cref="T:System.Data.DataRelation" /> does not exist.</returns>
		/// <param name="name">The name of the relation to find. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A8 RID: 168
		public abstract DataRelation this[string name] { get; }

		/// <summary>Adds a <see cref="T:System.Data.DataRelation" /> to the <see cref="T:System.Data.DataRelationCollection" />.</summary>
		/// <param name="relation">The DataRelation to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> parameter is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the specified name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000410 RID: 1040 RVA: 0x00015B50 File Offset: 0x00013D50
		public void Add(DataRelation relation)
		{
			long num = DataCommonEventSource.Log.EnterScope<int, int>("<ds.DataRelationCollection.Add|API> {0}, relation={1}", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			try
			{
				if (this._inTransition != relation)
				{
					this._inTransition = relation;
					try
					{
						this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, relation));
						this.AddCore(relation);
						this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, relation));
					}
					finally
					{
						this._inTransition = null;
					}
				}
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Performs verification on the table.</summary>
		/// <param name="relation">The relation to check.</param>
		/// <exception cref="T:System.ArgumentNullException">The relation is null. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		// Token: 0x06000411 RID: 1041 RVA: 0x00015BE4 File Offset: 0x00013DE4
		protected virtual void AddCore(DataRelation relation)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataRelationCollection.AddCore|INFO> {0}, relation={1}", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			if (relation == null)
			{
				throw ExceptionBuilder.ArgumentNull("relation");
			}
			relation.CheckState();
			DataSet dataSet = this.GetDataSet();
			if (relation.DataSet == dataSet)
			{
				throw ExceptionBuilder.RelationAlreadyInTheDataSet();
			}
			if (relation.DataSet != null)
			{
				throw ExceptionBuilder.RelationAlreadyInOtherDataSet();
			}
			if (relation.ChildTable.Locale.LCID != relation.ParentTable.Locale.LCID || relation.ChildTable.CaseSensitive != relation.ParentTable.CaseSensitive)
			{
				throw ExceptionBuilder.CaseLocaleMismatch();
			}
			if (relation.Nested)
			{
				relation.CheckNamespaceValidityForNestedRelations(relation.ParentTable.Namespace);
				relation.ValidateMultipleNestedRelations();
				DataTable parentTable = relation.ParentTable;
				int elementColumnCount = parentTable.ElementColumnCount;
				parentTable.ElementColumnCount = elementColumnCount + 1;
			}
		}

		/// <summary>Occurs when the collection has changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000412 RID: 1042 RVA: 0x00015CBE File Offset: 0x00013EBE
		// (remove) Token: 0x06000413 RID: 1043 RVA: 0x00015CEC File Offset: 0x00013EEC
		public event CollectionChangeEventHandler CollectionChanged
		{
			add
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.add_CollectionChanged|API> {0}", this.ObjectID);
				this._onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Combine(this._onCollectionChangedDelegate, value);
			}
			remove
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.remove_CollectionChanged|API> {0}", this.ObjectID);
				this._onCollectionChangedDelegate = (CollectionChangeEventHandler)Delegate.Remove(this._onCollectionChangedDelegate, value);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00015D1A File Offset: 0x00013F1A
		internal string AssignName()
		{
			string text = this.MakeName(this._defaultNameIndex);
			this._defaultNameIndex++;
			return text;
		}

		/// <summary>Clears the collection of any relations.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000415 RID: 1045 RVA: 0x00015D38 File Offset: 0x00013F38
		public virtual void Clear()
		{
			long num = DataCommonEventSource.Log.EnterScope<int>("<ds.DataRelationCollection.Clear|API> {0}", this.ObjectID);
			try
			{
				int count = this.Count;
				this.OnCollectionChanging(InternalDataCollectionBase.s_refreshEventArgs);
				for (int i = count - 1; i >= 0; i--)
				{
					this._inTransition = this[i];
					this.RemoveCore(this._inTransition);
				}
				this.OnCollectionChanged(InternalDataCollectionBase.s_refreshEventArgs);
				this._inTransition = null;
			}
			finally
			{
				DataCommonEventSource.Log.ExitScope(num);
			}
		}

		/// <summary>Verifies whether a <see cref="T:System.Data.DataRelation" /> with the specific name (case insensitive) exists in the collection.</summary>
		/// <returns>true, if a relation with the specified name exists; otherwise false.</returns>
		/// <param name="name">The name of the relation to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000416 RID: 1046 RVA: 0x00015DC4 File Offset: 0x00013FC4
		public virtual bool Contains(string name)
		{
			return this.InternalIndexOf(name) >= 0;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00015DD4 File Offset: 0x00013FD4
		internal int InternalIndexOf(string name)
		{
			int num = -1;
			if (name != null && 0 < name.Length)
			{
				int count = this.List.Count;
				for (int i = 0; i < count; i++)
				{
					DataRelation dataRelation = (DataRelation)this.List[i];
					int num2 = base.NamesEqual(dataRelation.RelationName, name, false, this.GetDataSet().Locale);
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

		/// <summary>This method supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>The referenced DataSet.</returns>
		// Token: 0x06000418 RID: 1048
		protected abstract DataSet GetDataSet();

		// Token: 0x06000419 RID: 1049 RVA: 0x00015E4C File Offset: 0x0001404C
		private string MakeName(int index)
		{
			if (index != 1)
			{
				return "Relation" + index.ToString(CultureInfo.InvariantCulture);
			}
			return "Relation1";
		}

		/// <summary>Raises the <see cref="E:System.Data.DataRelationCollection.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x0600041A RID: 1050 RVA: 0x00015E6E File Offset: 0x0001406E
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this._onCollectionChangedDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.OnCollectionChanged|INFO> {0}", this.ObjectID);
				this._onCollectionChangedDelegate(this, ccevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataRelationCollection.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x0600041B RID: 1051 RVA: 0x00015E9A File Offset: 0x0001409A
		protected virtual void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			if (this._onCollectionChangingDelegate != null)
			{
				DataCommonEventSource.Log.Trace<int>("<ds.DataRelationCollection.OnCollectionChanging|INFO> {0}", this.ObjectID);
				this._onCollectionChangingDelegate(this, ccevent);
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00015EC8 File Offset: 0x000140C8
		internal void RegisterName(string name)
		{
			DataCommonEventSource.Log.Trace<int, string>("<ds.DataRelationCollection.RegisterName|INFO> {0}, name='{1}'", this.ObjectID, name);
			CultureInfo locale = this.GetDataSet().Locale;
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				if (base.NamesEqual(name, this[i].RelationName, true, locale) != 0)
				{
					throw ExceptionBuilder.DuplicateRelation(this[i].RelationName);
				}
			}
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex), true, locale) != 0)
			{
				this._defaultNameIndex++;
			}
		}

		/// <summary>Removes the specified relation from the collection.</summary>
		/// <param name="relation">The relation to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The relation is a null value.</exception>
		/// <exception cref="T:System.ArgumentException">The relation does not belong to the collection.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600041D RID: 1053 RVA: 0x00015F58 File Offset: 0x00014158
		public void Remove(DataRelation relation)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataRelationCollection.Remove|API> {0}, relation={1}", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			if (this._inTransition == relation)
			{
				return;
			}
			this._inTransition = relation;
			try
			{
				this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, relation));
				this.RemoveCore(relation);
				this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, relation));
			}
			finally
			{
				this._inTransition = null;
			}
		}

		/// <summary>Removes the relation at the specified index from the collection.</summary>
		/// <param name="index">The index of the relation to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a relation at the specified index. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600041E RID: 1054 RVA: 0x00015FD4 File Offset: 0x000141D4
		public void RemoveAt(int index)
		{
			DataRelation dataRelation = this[index];
			if (dataRelation == null)
			{
				throw ExceptionBuilder.RelationOutOfRange(index);
			}
			this.Remove(dataRelation);
		}

		/// <summary>Performs a verification on the specified <see cref="T:System.Data.DataRelation" /> object.</summary>
		/// <param name="relation">The DataRelation object to verify. </param>
		/// <exception cref="T:System.ArgumentNullException">The collection does not have a relation at the specified index. </exception>
		/// <exception cref="T:System.ArgumentException">The specified relation does not belong to this collection, or it belongs to another collection. </exception>
		// Token: 0x0600041F RID: 1055 RVA: 0x00016000 File Offset: 0x00014200
		protected virtual void RemoveCore(DataRelation relation)
		{
			DataCommonEventSource.Log.Trace<int, int>("<ds.DataRelationCollection.RemoveCore|INFO> {0}, relation={1}", this.ObjectID, (relation != null) ? relation.ObjectID : 0);
			if (relation == null)
			{
				throw ExceptionBuilder.ArgumentNull("relation");
			}
			DataSet dataSet = this.GetDataSet();
			if (relation.DataSet != dataSet)
			{
				throw ExceptionBuilder.RelationNotInTheDataSet(relation.RelationName);
			}
			if (relation.Nested)
			{
				DataTable parentTable = relation.ParentTable;
				int elementColumnCount = parentTable.ElementColumnCount;
				parentTable.ElementColumnCount = elementColumnCount - 1;
				relation.ParentTable.Columns.UnregisterName(relation.ChildTable.TableName);
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00016090 File Offset: 0x00014290
		internal void UnregisterName(string name)
		{
			DataCommonEventSource.Log.Trace<int, string>("<ds.DataRelationCollection.UnregisterName|INFO> {0}, name='{1}'", this.ObjectID, name);
			if (base.NamesEqual(name, this.MakeName(this._defaultNameIndex - 1), true, this.GetDataSet().Locale) != 0)
			{
				do
				{
					this._defaultNameIndex--;
				}
				while (this._defaultNameIndex > 1 && !this.Contains(this.MakeName(this._defaultNameIndex - 1)));
			}
		}

		// Token: 0x0400013F RID: 319
		private DataRelation _inTransition;

		// Token: 0x04000140 RID: 320
		private int _defaultNameIndex = 1;

		// Token: 0x04000141 RID: 321
		private CollectionChangeEventHandler _onCollectionChangedDelegate;

		// Token: 0x04000142 RID: 322
		private CollectionChangeEventHandler _onCollectionChangingDelegate;

		// Token: 0x04000143 RID: 323
		private static int s_objectTypeCount;

		// Token: 0x04000144 RID: 324
		private readonly int _objectID = Interlocked.Increment(ref DataRelationCollection.s_objectTypeCount);

		// Token: 0x0200003D RID: 61
		internal sealed class DataTableRelationCollection : DataRelationCollection
		{
			// Token: 0x06000422 RID: 1058 RVA: 0x00016122 File Offset: 0x00014322
			internal DataTableRelationCollection(DataTable table, bool fParentCollection)
			{
				if (table == null)
				{
					throw ExceptionBuilder.RelationTableNull();
				}
				this._table = table;
				this._fParentCollection = fParentCollection;
				this._relations = new ArrayList();
			}

			// Token: 0x170000A9 RID: 169
			// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001614C File Offset: 0x0001434C
			protected override ArrayList List
			{
				get
				{
					return this._relations;
				}
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x00016154 File Offset: 0x00014354
			private void EnsureDataSet()
			{
				if (this._table.DataSet == null)
				{
					throw ExceptionBuilder.RelationTableWasRemoved();
				}
			}

			// Token: 0x06000425 RID: 1061 RVA: 0x00016169 File Offset: 0x00014369
			protected override DataSet GetDataSet()
			{
				this.EnsureDataSet();
				return this._table.DataSet;
			}

			// Token: 0x170000AA RID: 170
			public override DataRelation this[int index]
			{
				get
				{
					if (index >= 0 && index < this._relations.Count)
					{
						return (DataRelation)this._relations[index];
					}
					throw ExceptionBuilder.RelationOutOfRange(index);
				}
			}

			// Token: 0x170000AB RID: 171
			public override DataRelation this[string name]
			{
				get
				{
					int num = base.InternalIndexOf(name);
					if (num == -2)
					{
						throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
					}
					if (num >= 0)
					{
						return (DataRelation)this.List[num];
					}
					return null;
				}
			}

			// Token: 0x14000004 RID: 4
			// (add) Token: 0x06000428 RID: 1064 RVA: 0x000161E8 File Offset: 0x000143E8
			// (remove) Token: 0x06000429 RID: 1065 RVA: 0x00016220 File Offset: 0x00014420
			internal event CollectionChangeEventHandler RelationPropertyChanged;

			// Token: 0x0600042A RID: 1066 RVA: 0x00016255 File Offset: 0x00014455
			private void AddCache(DataRelation relation)
			{
				this._relations.Add(relation);
				if (!this._fParentCollection)
				{
					this._table.UpdatePropertyDescriptorCollectionCache();
				}
			}

			// Token: 0x0600042B RID: 1067 RVA: 0x00016278 File Offset: 0x00014478
			protected override void AddCore(DataRelation relation)
			{
				if (this._fParentCollection)
				{
					if (relation.ChildTable != this._table)
					{
						throw ExceptionBuilder.ChildTableMismatch();
					}
				}
				else if (relation.ParentTable != this._table)
				{
					throw ExceptionBuilder.ParentTableMismatch();
				}
				this.GetDataSet().Relations.Add(relation);
				this.AddCache(relation);
			}

			// Token: 0x0600042C RID: 1068 RVA: 0x000162D0 File Offset: 0x000144D0
			private void RemoveCache(DataRelation relation)
			{
				for (int i = 0; i < this._relations.Count; i++)
				{
					if (relation == this._relations[i])
					{
						this._relations.RemoveAt(i);
						if (!this._fParentCollection)
						{
							this._table.UpdatePropertyDescriptorCollectionCache();
						}
						return;
					}
				}
				throw ExceptionBuilder.RelationDoesNotExist();
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x00016328 File Offset: 0x00014528
			protected override void RemoveCore(DataRelation relation)
			{
				if (this._fParentCollection)
				{
					if (relation.ChildTable != this._table)
					{
						throw ExceptionBuilder.ChildTableMismatch();
					}
				}
				else if (relation.ParentTable != this._table)
				{
					throw ExceptionBuilder.ParentTableMismatch();
				}
				this.GetDataSet().Relations.Remove(relation);
				this.RemoveCache(relation);
			}

			// Token: 0x04000145 RID: 325
			private readonly DataTable _table;

			// Token: 0x04000146 RID: 326
			private readonly ArrayList _relations;

			// Token: 0x04000147 RID: 327
			private readonly bool _fParentCollection;
		}

		// Token: 0x0200003E RID: 62
		internal sealed class DataSetRelationCollection : DataRelationCollection
		{
			// Token: 0x0600042E RID: 1070 RVA: 0x0001637D File Offset: 0x0001457D
			internal DataSetRelationCollection(DataSet dataSet)
			{
				if (dataSet == null)
				{
					throw ExceptionBuilder.RelationDataSetNull();
				}
				this._dataSet = dataSet;
				this._relations = new ArrayList();
			}

			// Token: 0x170000AC RID: 172
			// (get) Token: 0x0600042F RID: 1071 RVA: 0x000163A0 File Offset: 0x000145A0
			protected override ArrayList List
			{
				get
				{
					return this._relations;
				}
			}

			// Token: 0x06000430 RID: 1072 RVA: 0x000163A8 File Offset: 0x000145A8
			public override void Clear()
			{
				base.Clear();
				if (this._dataSet._fInitInProgress && this._delayLoadingRelations != null)
				{
					this._delayLoadingRelations = null;
				}
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x000163CC File Offset: 0x000145CC
			protected override DataSet GetDataSet()
			{
				return this._dataSet;
			}

			// Token: 0x170000AD RID: 173
			public override DataRelation this[int index]
			{
				get
				{
					if (index >= 0 && index < this._relations.Count)
					{
						return (DataRelation)this._relations[index];
					}
					throw ExceptionBuilder.RelationOutOfRange(index);
				}
			}

			// Token: 0x170000AE RID: 174
			public override DataRelation this[string name]
			{
				get
				{
					int num = base.InternalIndexOf(name);
					if (num == -2)
					{
						throw ExceptionBuilder.CaseInsensitiveNameConflict(name);
					}
					if (num >= 0)
					{
						return (DataRelation)this.List[num];
					}
					return null;
				}
			}

			// Token: 0x06000434 RID: 1076 RVA: 0x00016440 File Offset: 0x00014640
			protected override void AddCore(DataRelation relation)
			{
				base.AddCore(relation);
				if (relation.ChildTable.DataSet != this._dataSet || relation.ParentTable.DataSet != this._dataSet)
				{
					throw ExceptionBuilder.ForeignRelation();
				}
				relation.CheckState();
				if (relation.Nested)
				{
					relation.CheckNestedRelations();
				}
				if (relation._relationName.Length == 0)
				{
					relation._relationName = base.AssignName();
				}
				else
				{
					base.RegisterName(relation._relationName);
				}
				DataKey childKey = relation.ChildKey;
				for (int i = 0; i < this._relations.Count; i++)
				{
					if (childKey.ColumnsEqual(((DataRelation)this._relations[i]).ChildKey) && relation.ParentKey.ColumnsEqual(((DataRelation)this._relations[i]).ParentKey))
					{
						throw ExceptionBuilder.RelationAlreadyExists();
					}
				}
				this._relations.Add(relation);
				((DataRelationCollection.DataTableRelationCollection)relation.ParentTable.ChildRelations).Add(relation);
				((DataRelationCollection.DataTableRelationCollection)relation.ChildTable.ParentRelations).Add(relation);
				relation.SetDataSet(this._dataSet);
				relation.ChildKey.GetSortIndex().AddRef();
				if (relation.Nested)
				{
					relation.ChildTable.CacheNestedParent();
				}
				ForeignKeyConstraint foreignKeyConstraint = relation.ChildTable.Constraints.FindForeignKeyConstraint(relation.ParentColumnsReference, relation.ChildColumnsReference);
				if (relation._createConstraints && foreignKeyConstraint == null)
				{
					relation.ChildTable.Constraints.Add(foreignKeyConstraint = new ForeignKeyConstraint(relation.ParentColumnsReference, relation.ChildColumnsReference));
					try
					{
						foreignKeyConstraint.ConstraintName = relation.RelationName;
					}
					catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
					{
						ExceptionBuilder.TraceExceptionWithoutRethrow(ex);
					}
				}
				UniqueConstraint uniqueConstraint = relation.ParentTable.Constraints.FindKeyConstraint(relation.ParentColumnsReference);
				relation.SetParentKeyConstraint(uniqueConstraint);
				relation.SetChildKeyConstraint(foreignKeyConstraint);
			}

			// Token: 0x06000435 RID: 1077 RVA: 0x00016644 File Offset: 0x00014844
			protected override void RemoveCore(DataRelation relation)
			{
				base.RemoveCore(relation);
				this._dataSet.OnRemoveRelationHack(relation);
				relation.SetDataSet(null);
				relation.ChildKey.GetSortIndex().RemoveRef();
				if (relation.Nested)
				{
					relation.ChildTable.CacheNestedParent();
				}
				for (int i = 0; i < this._relations.Count; i++)
				{
					if (relation == this._relations[i])
					{
						this._relations.RemoveAt(i);
						((DataRelationCollection.DataTableRelationCollection)relation.ParentTable.ChildRelations).Remove(relation);
						((DataRelationCollection.DataTableRelationCollection)relation.ChildTable.ParentRelations).Remove(relation);
						if (relation.Nested)
						{
							relation.ChildTable.CacheNestedParent();
						}
						base.UnregisterName(relation.RelationName);
						relation.SetParentKeyConstraint(null);
						relation.SetChildKeyConstraint(null);
						return;
					}
				}
				throw ExceptionBuilder.RelationDoesNotExist();
			}

			// Token: 0x04000149 RID: 329
			private readonly DataSet _dataSet;

			// Token: 0x0400014A RID: 330
			private readonly ArrayList _relations;

			// Token: 0x0400014B RID: 331
			private DataRelation[] _delayLoadingRelations;
		}
	}
}
