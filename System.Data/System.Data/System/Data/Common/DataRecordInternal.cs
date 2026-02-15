using System;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	// Token: 0x020000EA RID: 234
	internal sealed class DataRecordInternal : DbDataRecord, ICustomTypeDescriptor
	{
		// Token: 0x06000C6D RID: 3181 RVA: 0x00043814 File Offset: 0x00041A14
		internal DataRecordInternal(SchemaInfo[] schemaInfo, object[] values, PropertyDescriptorCollection descriptors, FieldNameLookup fieldNameLookup)
		{
			this._schemaInfo = schemaInfo;
			this._values = values;
			this._propertyDescriptors = descriptors;
			this._fieldNameLookup = fieldNameLookup;
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00043839 File Offset: 0x00041A39
		public override int FieldCount
		{
			get
			{
				return this._schemaInfo.Length;
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x00043844 File Offset: 0x00041A44
		public override int GetValues(object[] values)
		{
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = ((values.Length < this._schemaInfo.Length) ? values.Length : this._schemaInfo.Length);
			for (int i = 0; i < num; i++)
			{
				values[i] = this._values[i];
			}
			return num;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x00043892 File Offset: 0x00041A92
		public override string GetName(int i)
		{
			return this._schemaInfo[i].name;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x000438A5 File Offset: 0x00041AA5
		public override object GetValue(int i)
		{
			return this._values[i];
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x000438AF File Offset: 0x00041AAF
		public override string GetDataTypeName(int i)
		{
			return this._schemaInfo[i].typeName;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x000438C2 File Offset: 0x00041AC2
		public override Type GetFieldType(int i)
		{
			return this._schemaInfo[i].type;
		}

		// Token: 0x170001E3 RID: 483
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x000438DE File Offset: 0x00041ADE
		public override int GetInt32(int i)
		{
			return (int)this._values[i];
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x000438ED File Offset: 0x00041AED
		public override long GetInt64(int i)
		{
			return (long)this._values[i];
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x000438FC File Offset: 0x00041AFC
		public override string GetString(int i)
		{
			return (string)this._values[i];
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00018094 File Offset: 0x00016294
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00011F10 File Offset: 0x00010110
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00011F10 File Offset: 0x00010110
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00011F10 File Offset: 0x00010110
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x00011F10 File Offset: 0x00010110
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00011F10 File Offset: 0x00010110
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x00011F10 File Offset: 0x00010110
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0001809C File Offset: 0x0001629C
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0001809C File Offset: 0x0001629C
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000180A4 File Offset: 0x000162A4
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0004390B File Offset: 0x00041B0B
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._propertyDescriptors == null)
			{
				this._propertyDescriptors = new PropertyDescriptorCollection(null);
			}
			return this._propertyDescriptors;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0000207F File Offset: 0x0000027F
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x040004F2 RID: 1266
		private SchemaInfo[] _schemaInfo;

		// Token: 0x040004F3 RID: 1267
		private object[] _values;

		// Token: 0x040004F4 RID: 1268
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x040004F5 RID: 1269
		private FieldNameLookup _fieldNameLookup;
	}
}
