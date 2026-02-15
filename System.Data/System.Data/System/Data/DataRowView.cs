using System;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data
{
	/// <summary>Represents a customized view of a <see cref="T:System.Data.DataRow" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004B RID: 75
	[DefaultMember("Item")]
	public class DataRowView : ICustomTypeDescriptor, IEditableObject, INotifyPropertyChanged
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x00017EBF File Offset: 0x000160BF
		internal DataRowView(DataView dataView, DataRow row)
		{
			this._dataView = dataView;
			this._row = row;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Data.DataRowView" /> is identical to the specified object.</summary>
		/// <returns>true if <paramref name="object" /> is a <see cref="T:System.Data.DataRowView" /> and it returns the same row as the current <see cref="T:System.Data.DataRowView" />; otherwise false.</returns>
		/// <param name="other">An <see cref="T:System.Object" /> to be compared. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060004A6 RID: 1190 RVA: 0x00017ED5 File Offset: 0x000160D5
		public override bool Equals(object other)
		{
			return this == other;
		}

		/// <summary>Returns the hash code of the <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code 1, which represents Boolean true if the value of this instance is nonzero; otherwise the integer zero, which represents Boolean false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060004A7 RID: 1191 RVA: 0x00017EDB File Offset: 0x000160DB
		public override int GetHashCode()
		{
			return this.Row.GetHashCode();
		}

		/// <summary>Gets the <see cref="T:System.Data.DataView" /> to which this row belongs.</summary>
		/// <returns>The DataView to which this row belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x00017EE8 File Offset: 0x000160E8
		public DataView DataView
		{
			get
			{
				return this._dataView;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00017EF0 File Offset: 0x000160F0
		private DataRowVersion RowVersionDefault
		{
			get
			{
				return this.Row.GetDefaultRowVersion(this._dataView.RowStateFilter);
			}
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x00017F08 File Offset: 0x00016108
		internal int GetRecord()
		{
			return this.Row.GetRecordFromVersion(this.RowVersionDefault);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00017F1B File Offset: 0x0001611B
		internal bool HasRecord()
		{
			return this.Row.HasVersion(this.RowVersionDefault);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00017F2E File Offset: 0x0001612E
		internal object GetColumnValue(DataColumn column)
		{
			return this.Row[column, this.RowVersionDefault];
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00017F44 File Offset: 0x00016144
		internal void SetColumnValue(DataColumn column, object value)
		{
			if (this._delayBeginEdit)
			{
				this._delayBeginEdit = false;
				this.Row.BeginEdit();
			}
			if (DataRowVersion.Original == this.RowVersionDefault)
			{
				throw ExceptionBuilder.SetFailed(column.ColumnName);
			}
			this.Row[column] = value;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified <see cref="T:System.Data.DataRelation" /> and parent..</summary>
		/// <returns>A <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> object.</param>
		/// <param name="followParent">The parent object.</param>
		// Token: 0x060004AE RID: 1198 RVA: 0x00017F94 File Offset: 0x00016194
		public DataView CreateChildView(DataRelation relation, bool followParent)
		{
			if (relation == null || relation.ParentKey.Table != this.DataView.Table)
			{
				throw ExceptionBuilder.CreateChildView();
			}
			RelatedView relatedView;
			if (!followParent)
			{
				int record = this.GetRecord();
				object[] keyValues = relation.ParentKey.GetKeyValues(record);
				relatedView = new RelatedView(relation.ChildColumnsReference, keyValues);
			}
			else
			{
				relatedView = new RelatedView(this, relation.ParentKey, relation.ChildColumnsReference);
			}
			relatedView.SetIndex("", DataViewRowState.CurrentRows, null);
			relatedView.SetDataViewManager(this.DataView.DataViewManager);
			return relatedView;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" /> with the specified child <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>a <see cref="T:System.Data.DataView" /> for the child <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004AF RID: 1199 RVA: 0x00018021 File Offset: 0x00016221
		public DataView CreateChildView(DataRelation relation)
		{
			return this.CreateChildView(relation, false);
		}

		/// <summary>Gets the <see cref="T:System.Data.DataRow" /> being viewed.</summary>
		/// <returns>The <see cref="T:System.Data.DataRow" /> being viewed by the <see cref="T:System.Data.DataRowView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x0001802B File Offset: 0x0001622B
		public DataRow Row
		{
			get
			{
				return this._row;
			}
		}

		/// <summary>Begins an edit procedure.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004B1 RID: 1201 RVA: 0x00018033 File Offset: 0x00016233
		public void BeginEdit()
		{
			this._delayBeginEdit = true;
		}

		/// <summary>Commits changes to the underlying <see cref="T:System.Data.DataRow" /> and ends the editing session that was begun with <see cref="M:System.Data.DataRowView.BeginEdit" />.  Use <see cref="M:System.Data.DataRowView.CancelEdit" /> to discard the changes made to the <see cref="T:System.Data.DataRow" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004B2 RID: 1202 RVA: 0x0001803C File Offset: 0x0001623C
		public void EndEdit()
		{
			if (this.IsNew)
			{
				this._dataView.FinishAddNew(true);
			}
			else
			{
				this.Row.EndEdit();
			}
			this._delayBeginEdit = false;
		}

		/// <summary>Indicates whether a <see cref="T:System.Data.DataRowView" /> is new.</summary>
		/// <returns>true if the row is new; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x00018066 File Offset: 0x00016266
		public bool IsNew
		{
			get
			{
				return this._row == this._dataView._addNewRow;
			}
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001807B File Offset: 0x0001627B
		internal void RaisePropertyChangedEvent(string propName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propName));
		}

		/// <summary>Returns a collection of custom attributes for this instance of a component.</summary>
		/// <returns>An AttributeCollection containing the attributes for this object.</returns>
		// Token: 0x060004B5 RID: 1205 RVA: 0x00018094 File Offset: 0x00016294
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		/// <summary>Returns the class name of this instance of a component.</summary>
		/// <returns>The class name of this instance of a component.</returns>
		// Token: 0x060004B6 RID: 1206 RVA: 0x00011F10 File Offset: 0x00010110
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		/// <summary>Returns the name of this instance of a component.</summary>
		/// <returns>The name of this instance of a component.</returns>
		// Token: 0x060004B7 RID: 1207 RVA: 0x00011F10 File Offset: 0x00010110
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		/// <summary>Returns a type converter for this instance of a component.</summary>
		/// <returns>The type converter for this instance of a component.</returns>
		// Token: 0x060004B8 RID: 1208 RVA: 0x00011F10 File Offset: 0x00010110
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		/// <summary>Returns the default event for this instance of a component.</summary>
		/// <returns>The default event for this instance of a component.</returns>
		// Token: 0x060004B9 RID: 1209 RVA: 0x00011F10 File Offset: 0x00010110
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		/// <summary>Returns the default property for this instance of a component.</summary>
		/// <returns>The default property for this instance of a component.</returns>
		// Token: 0x060004BA RID: 1210 RVA: 0x00011F10 File Offset: 0x00010110
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		/// <summary>Returns an editor of the specified type for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found.</returns>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object. </param>
		// Token: 0x060004BB RID: 1211 RVA: 0x00011F10 File Offset: 0x00010110
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		/// <summary>Returns the events for this instance of a component.</summary>
		/// <returns>The events for this instance of a component.</returns>
		// Token: 0x060004BC RID: 1212 RVA: 0x0001809C File Offset: 0x0001629C
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>Returns the events for this instance of a component with specified attributes.</summary>
		/// <returns>The events for this instance of a component.</returns>
		/// <param name="attributes">The attributes</param>
		// Token: 0x060004BD RID: 1213 RVA: 0x0001809C File Offset: 0x0001629C
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>Returns the properties for this instance of a component.</summary>
		/// <returns>The properties for this instance of a component.</returns>
		// Token: 0x060004BE RID: 1214 RVA: 0x000180A4 File Offset: 0x000162A4
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		/// <summary>Returns the properties for this instance of a component with specified attributes.</summary>
		/// <returns>The properties for this instance of a component.</returns>
		/// <param name="attributes">The attributes.</param>
		// Token: 0x060004BF RID: 1215 RVA: 0x000180AD File Offset: 0x000162AD
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._dataView.Table == null)
			{
				return DataRowView.s_zeroPropertyDescriptorCollection;
			}
			return this._dataView.Table.GetPropertyDescriptorCollection(attributes);
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
		/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found. </param>
		// Token: 0x060004C0 RID: 1216 RVA: 0x0000207F File Offset: 0x0000027F
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04000178 RID: 376
		private readonly DataView _dataView;

		// Token: 0x04000179 RID: 377
		private readonly DataRow _row;

		// Token: 0x0400017A RID: 378
		private bool _delayBeginEdit;

		// Token: 0x0400017B RID: 379
		private static readonly PropertyDescriptorCollection s_zeroPropertyDescriptorCollection = new PropertyDescriptorCollection(null);

		// Token: 0x0400017C RID: 380
		[CompilerGenerated]
		private PropertyChangedEventHandler PropertyChanged;
	}
}
