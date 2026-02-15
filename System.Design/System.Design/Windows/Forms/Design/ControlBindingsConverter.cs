using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000002 RID: 2
	internal class ControlBindingsConverter : TypeConverter
	{
		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		[MonoTODO]
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
			ControlBindingsCollection controlBindingsCollection = value as ControlBindingsCollection;
			object bindableComponent = controlBindingsCollection.BindableComponent;
			if (controlBindingsCollection != null && bindableComponent != null)
			{
				foreach (object obj in TypeDescriptor.GetProperties(bindableComponent, attributes))
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
					if (((BindableAttribute)propertyDescriptor.Attributes[typeof(BindableAttribute)]).Bindable)
					{
						propertyDescriptorCollection.Add(new ControlBindingsConverter.DataBindingPropertyDescriptor(propertyDescriptor, attributes, true));
					}
				}
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002100 File Offset: 0x00000300
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002103 File Offset: 0x00000303
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002121 File Offset: 0x00000321
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return string.Empty;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x02000003 RID: 3
		[MonoTODO]
		private class DataBindingPropertyDescriptor : PropertyDescriptor
		{
			// Token: 0x06000006 RID: 6 RVA: 0x00002147 File Offset: 0x00000347
			[MonoTODO]
			public DataBindingPropertyDescriptor(PropertyDescriptor property, Attribute[] attrs, bool readOnly)
				: base(property.Name, attrs)
			{
				this._readOnly = readOnly;
			}

			// Token: 0x06000007 RID: 7 RVA: 0x0000215D File Offset: 0x0000035D
			[MonoTODO]
			public override object GetValue(object component)
			{
				return null;
			}

			// Token: 0x06000008 RID: 8 RVA: 0x00002160 File Offset: 0x00000360
			[MonoTODO]
			public override void SetValue(object component, object value)
			{
			}

			// Token: 0x06000009 RID: 9 RVA: 0x00002162 File Offset: 0x00000362
			[MonoTODO]
			public override void ResetValue(object component)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600000A RID: 10 RVA: 0x00002169 File Offset: 0x00000369
			[MonoTODO]
			public override bool CanResetValue(object component)
			{
				return false;
			}

			// Token: 0x0600000B RID: 11 RVA: 0x00002169 File Offset: 0x00000369
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x0600000C RID: 12 RVA: 0x0000216C File Offset: 0x0000036C
			[MonoTODO]
			public override Type PropertyType
			{
				get
				{
					return typeof(ControlBindingsConverter.DataBindingPropertyDescriptor);
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x0600000D RID: 13 RVA: 0x0000215D File Offset: 0x0000035D
			[MonoTODO]
			public override TypeConverter Converter
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x0600000E RID: 14 RVA: 0x00002178 File Offset: 0x00000378
			public override Type ComponentType
			{
				get
				{
					return typeof(ControlBindingsCollection);
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x0600000F RID: 15 RVA: 0x00002184 File Offset: 0x00000384
			public override bool IsReadOnly
			{
				get
				{
					return this._readOnly;
				}
			}

			// Token: 0x04000001 RID: 1
			private bool _readOnly;
		}
	}
}
