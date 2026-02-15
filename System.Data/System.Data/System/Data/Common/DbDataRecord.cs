using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Implements <see cref="T:System.Data.IDataRecord" /> and <see cref="T:System.ComponentModel.ICustomTypeDescriptor" />, and provides data binding support for <see cref="T:System.Data.Common.DbEnumerator" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000F7 RID: 247
	public abstract class DbDataRecord : ICustomTypeDescriptor, IDataRecord
	{
		/// <summary>Indicates the number of fields within the current record. This property is read-only.</summary>
		/// <returns>The number of fields within the current record.</returns>
		/// <exception cref="T:System.NotSupportedException">Not connected to a data source to read from. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000D29 RID: 3369
		public abstract int FieldCount { get; }

		/// <summary>Indicates the value at the specified column in its native format given the column ordinal. This property is read-only.</summary>
		/// <returns>The value at the specified column in its native format.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000202 RID: 514
		public abstract object this[int i] { get; }

		/// <summary>Returns the name of the back-end data type.</summary>
		/// <returns>The name of the back-end data type.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D2B RID: 3371
		public abstract string GetDataTypeName(int i);

		/// <summary>Returns the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D2C RID: 3372
		public abstract Type GetFieldType(int i);

		/// <summary>Returns the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D2D RID: 3373
		public abstract int GetInt32(int i);

		/// <summary>Returns the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D2E RID: 3374
		public abstract long GetInt64(int i);

		/// <summary>Returns the name of the specified column.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D2F RID: 3375
		public abstract string GetName(int i);

		/// <summary>Returns the value of the specified column as a string.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D30 RID: 3376
		public abstract string GetString(int i);

		/// <summary>Returns the value at the specified column in its native format.</summary>
		/// <returns>The value to return.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D31 RID: 3377
		public abstract object GetValue(int i);

		/// <summary>Populates an array of objects with the column values of the current record.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> to copy the attribute fields into. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000D32 RID: 3378
		public abstract int GetValues(object[] values);

		/// <summary>Returns a collection of custom attributes for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> that contains the attributes for this object. </returns>
		// Token: 0x06000D33 RID: 3379 RVA: 0x00018094 File Offset: 0x00016294
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		/// <summary>Returns the class name of this instance of a component.</summary>
		/// <returns>The class name of the object, or null if the class does not have a name.</returns>
		// Token: 0x06000D34 RID: 3380 RVA: 0x00011F10 File Offset: 0x00010110
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		/// <summary>Returns the name of this instance of a component.</summary>
		/// <returns>The name of the object, or null if the object does not have a name.</returns>
		// Token: 0x06000D35 RID: 3381 RVA: 0x00011F10 File Offset: 0x00010110
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		/// <summary>Returns a type converter for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or null if there is no <see cref="T:System.ComponentModel.TypeConverter" /> for this object.</returns>
		// Token: 0x06000D36 RID: 3382 RVA: 0x00011F10 File Offset: 0x00010110
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		/// <summary>Returns the default event for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for this object, or null if this object does not have events.</returns>
		// Token: 0x06000D37 RID: 3383 RVA: 0x00011F10 File Offset: 0x00010110
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		/// <summary>Returns the default property for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for this object, or null if this object does not have properties.</returns>
		// Token: 0x06000D38 RID: 3384 RVA: 0x00011F10 File Offset: 0x00010110
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		/// <summary>Returns an editor of the specified type for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found.</returns>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
		// Token: 0x06000D39 RID: 3385 RVA: 0x00011F10 File Offset: 0x00010110
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		/// <summary>Returns the events for this instance of a component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for this component instance.</returns>
		// Token: 0x06000D3A RID: 3386 RVA: 0x0001809C File Offset: 0x0001629C
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>Returns the events for this instance of a component using the specified attribute array as a filter.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the filtered events for this component instance.</returns>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		// Token: 0x06000D3B RID: 3387 RVA: 0x0001809C File Offset: 0x0001629C
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		/// <summary>Returns the properties for this instance of a component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties for this component instance.</returns>
		// Token: 0x06000D3C RID: 3388 RVA: 0x000180A4 File Offset: 0x000162A4
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		/// <summary>Returns the properties for this instance of a component using the attribute array as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the filtered properties for this component instance.</returns>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		// Token: 0x06000D3D RID: 3389 RVA: 0x00044FCC File Offset: 0x000431CC
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return new PropertyDescriptorCollection(null);
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
		/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
		// Token: 0x06000D3E RID: 3390 RVA: 0x0000207F File Offset: 0x0000027F
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}
