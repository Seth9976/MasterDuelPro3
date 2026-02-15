using System;
using System.Collections;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides automatic conversion between a nullable type and its underlying primitive type.</summary>
	// Token: 0x0200028F RID: 655
	public class NullableConverter : TypeConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.NullableConverter" /> class.</summary>
		/// <param name="type">The specified nullable type.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> is not a nullable type.</exception>
		// Token: 0x06000F81 RID: 3969 RVA: 0x0004230C File Offset: 0x0004050C
		public NullableConverter(Type type)
		{
			this.NullableType = type;
			this.UnderlyingType = Nullable.GetUnderlyingType(type);
			if (this.UnderlyingType == null)
			{
				throw new ArgumentException("The specified type is not a nullable type.", "type");
			}
			this.UnderlyingTypeConverter = TypeDescriptor.GetConverter(this.UnderlyingType);
		}

		/// <summary>Returns whether this converter can convert an object of the given type to the type of this converter, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" />  that provides a format context.</param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you want to convert from.</param>
		// Token: 0x06000F82 RID: 3970 RVA: 0x00042361 File Offset: 0x00040561
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == this.UnderlyingType)
			{
				return true;
			}
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.CanConvertFrom(context, sourceType);
			}
			return base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given object to the type of this converter, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F83 RID: 3971 RVA: 0x00042394 File Offset: 0x00040594
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || value.GetType() == this.UnderlyingType)
			{
				return value;
			}
			if (value is string && string.IsNullOrEmpty(value as string))
			{
				return null;
			}
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.ConvertFrom(context, culture, value);
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Returns whether this converter can convert the object to the specified type, using the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to.</param>
		// Token: 0x06000F84 RID: 3972 RVA: 0x000423F0 File Offset: 0x000405F0
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == this.UnderlyingType)
			{
				return true;
			}
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.CanConvertTo(context, destinationType);
			}
			return base.CanConvertTo(context, destinationType);
		}

		/// <summary>Converts the given value object to the specified type, using the specified context and culture information.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted value.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the value parameter to.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000F85 RID: 3973 RVA: 0x00042420 File Offset: 0x00040620
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == this.UnderlyingType && value != null && this.NullableType.IsInstanceOfType(value))
			{
				return value;
			}
			if (value == null)
			{
				if (destinationType == typeof(string))
				{
					return string.Empty;
				}
			}
			else if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.ConvertTo(context, culture, value, destinationType);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		/// <returns>An <see cref="T:System.Object" /> representing the given <see cref="T:System.Collections.IDictionary" />, or null if the object cannot be created. This method always returns null.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="propertyValues">An <see cref="T:System.Collections.IDictionary" /> of new property values. </param>
		// Token: 0x06000F86 RID: 3974 RVA: 0x000424A4 File Offset: 0x000406A4
		public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.CreateInstance(context, propertyValues);
			}
			return base.CreateInstance(context, propertyValues);
		}

		/// <returns>true if changing a property on this object requires a call to <see cref="M:System.ComponentModel.TypeConverter.CreateInstance(System.Collections.IDictionary)" /> to create a new value; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06000F87 RID: 3975 RVA: 0x000424C4 File Offset: 0x000406C4
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.GetCreateInstanceSupported(context);
			}
			return base.GetCreateInstanceSupported(context);
		}

		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for this data type, or null if there are no properties.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that specifies the type of array for which to get properties. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter. </param>
		// Token: 0x06000F88 RID: 3976 RVA: 0x000424E4 File Offset: 0x000406E4
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.GetProperties(context, value, attributes);
			}
			return base.GetProperties(context, value, attributes);
		}

		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetProperties(System.Object)" /> should be called to find the properties of this object; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06000F89 RID: 3977 RVA: 0x00042513 File Offset: 0x00040713
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.GetPropertiesSupported(context);
			}
			return base.GetPropertiesSupported(context);
		}

		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values, or null if the data type does not support a standard set of values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context that can be used to extract additional information about the environment from which this converter is invoked. This parameter or properties of this parameter can be null.</param>
		// Token: 0x06000F8A RID: 3978 RVA: 0x00042534 File Offset: 0x00040734
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				TypeConverter.StandardValuesCollection standardValues = this.UnderlyingTypeConverter.GetStandardValues(context);
				if (this.GetStandardValuesSupported(context) && standardValues != null)
				{
					object[] array = new object[standardValues.Count + 1];
					int num = 0;
					array[num++] = null;
					foreach (object obj in standardValues)
					{
						array[num++] = obj;
					}
					return new TypeConverter.StandardValuesCollection(array);
				}
			}
			return base.GetStandardValues(context);
		}

		/// <returns>true if the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> is an exhaustive list of possible values; false if other values are possible.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06000F8B RID: 3979 RVA: 0x000425D0 File Offset: 0x000407D0
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.GetStandardValuesExclusive(context);
			}
			return base.GetStandardValuesExclusive(context);
		}

		/// <returns>true if <see cref="M:System.ComponentModel.TypeConverter.GetStandardValues" /> should be called to find a common set of values the object supports; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
		// Token: 0x06000F8C RID: 3980 RVA: 0x000425EE File Offset: 0x000407EE
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return this.UnderlyingTypeConverter.GetStandardValuesSupported(context);
			}
			return base.GetStandardValuesSupported(context);
		}

		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to test for validity. </param>
		// Token: 0x06000F8D RID: 3981 RVA: 0x0004260C File Offset: 0x0004080C
		public override bool IsValid(ITypeDescriptorContext context, object value)
		{
			if (this.UnderlyingTypeConverter != null)
			{
				return value == null || this.UnderlyingTypeConverter.IsValid(context, value);
			}
			return base.IsValid(context, value);
		}

		/// <summary>Gets the nullable type.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the nullable type.</returns>
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0004263E File Offset: 0x0004083E
		public Type NullableType { get; }

		/// <summary>Gets the underlying type.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the underlying type.</returns>
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00042646 File Offset: 0x00040846
		public Type UnderlyingType { get; }

		/// <summary>Gets the underlying type converter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that represents the underlying type converter.</returns>
		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0004264E File Offset: 0x0004084E
		public TypeConverter UnderlyingTypeConverter { get; }
	}
}
