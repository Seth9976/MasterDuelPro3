using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Array" /> objects to and from various other representations.</summary>
	// Token: 0x02000253 RID: 595
	public class ArrayConverter : CollectionConverter
	{
		/// <summary>Converts the given value object to the specified destination type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The culture into which <paramref name="value" /> will be converted.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000E35 RID: 3637 RVA: 0x0003EB9C File Offset: 0x0003CD9C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string) && value is Array)
			{
				return SR.Format("{0} Array", value.GetType().Name);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <summary>Gets a collection of properties for the type of array specified by the value parameter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for an array, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that will be used as a filter. </param>
		// Token: 0x06000E36 RID: 3638 RVA: 0x0003EBFC File Offset: 0x0003CDFC
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (value == null)
			{
				return null;
			}
			PropertyDescriptor[] array = null;
			if (value.GetType().IsArray)
			{
				int length = ((Array)value).GetLength(0);
				array = new PropertyDescriptor[length];
				Type type = value.GetType();
				Type elementType = type.GetElementType();
				for (int i = 0; i < length; i++)
				{
					array[i] = new ArrayConverter.ArrayPropertyDescriptor(type, elementType, i);
				}
			}
			return new PropertyDescriptorCollection(array);
		}

		/// <summary>Gets a value indicating whether this object supports properties.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.ArrayConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should be called to find the properties of this object. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000E37 RID: 3639 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x02000254 RID: 596
		private class ArrayPropertyDescriptor : TypeConverter.SimplePropertyDescriptor
		{
			// Token: 0x06000E39 RID: 3641 RVA: 0x0003EC6A File Offset: 0x0003CE6A
			public ArrayPropertyDescriptor(Type arrayType, Type elementType, int index)
				: base(arrayType, "[" + index.ToString() + "]", elementType, null)
			{
				this._index = index;
			}

			// Token: 0x06000E3A RID: 3642 RVA: 0x0003EC94 File Offset: 0x0003CE94
			public override object GetValue(object instance)
			{
				Array array = instance as Array;
				if (array != null && array.GetLength(0) > this._index)
				{
					return array.GetValue(this._index);
				}
				return null;
			}

			// Token: 0x06000E3B RID: 3643 RVA: 0x0003ECC8 File Offset: 0x0003CEC8
			public override void SetValue(object instance, object value)
			{
				if (instance is Array)
				{
					Array array = (Array)instance;
					if (array.GetLength(0) > this._index)
					{
						array.SetValue(value, this._index);
					}
					this.OnValueChanged(instance, EventArgs.Empty);
				}
			}

			// Token: 0x040009B5 RID: 2485
			private readonly int _index;
		}
	}
}
