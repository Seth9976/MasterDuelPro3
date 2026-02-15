using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert <see cref="T:System.Boolean" /> objects to and from various other representations.</summary>
	// Token: 0x0200025B RID: 603
	public class BooleanConverter : TypeConverter
	{
		/// <summary>Gets a value indicating whether this converter can convert an object in the given source type to a Boolean object using the specified context.</summary>
		/// <returns>true if this object can perform the conversion; otherwise, false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="sourceType">A <see cref="T:System.Type" /> that represents the type you wish to convert from. </param>
		// Token: 0x06000E5C RID: 3676 RVA: 0x0003F094 File Offset: 0x0003D294
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		/// <summary>Converts the given value object to a Boolean object.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> that specifies the culture to which to convert.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> is not a valid value for the target type. </exception>
		/// <exception cref="T:System.NotSupportedException">The conversion cannot be performed. </exception>
		// Token: 0x06000E5D RID: 3677 RVA: 0x0003F2E4 File Offset: 0x0003D4E4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text != null)
			{
				text = text.Trim();
				try
				{
					return bool.Parse(text);
				}
				catch (FormatException ex)
				{
					throw new FormatException(SR.Format("{0} is not a valid value for {1}.", (string)value, "Boolean"), ex);
				}
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Gets a collection of standard values for the Boolean data type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> that holds a standard set of valid values.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000E5E RID: 3678 RVA: 0x0003F348 File Offset: 0x0003D548
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			TypeConverter.StandardValuesCollection standardValuesCollection;
			if ((standardValuesCollection = BooleanConverter.s_values) == null)
			{
				standardValuesCollection = (BooleanConverter.s_values = new TypeConverter.StandardValuesCollection(new object[] { true, false }));
			}
			return standardValuesCollection;
		}

		/// <summary>Gets a value indicating whether the list of standard values returned from the <see cref="M:System.ComponentModel.BooleanConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> method is an exclusive list.</summary>
		/// <returns>true because the <see cref="T:System.ComponentModel.TypeConverter.StandardValuesCollection" /> returned from <see cref="M:System.ComponentModel.BooleanConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> is an exhaustive list of possible values. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000E5F RID: 3679 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>Gets a value indicating whether this object supports a standard set of values that can be picked from a list.</summary>
		/// <returns>true because <see cref="M:System.ComponentModel.BooleanConverter.GetStandardValues(System.ComponentModel.ITypeDescriptorContext)" /> can be called to find a common set of values the object supports. This method never returns false.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		// Token: 0x06000E60 RID: 3680 RVA: 0x00003BCC File Offset: 0x00001DCC
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x040009C9 RID: 2505
		private static volatile TypeConverter.StandardValuesCollection s_values;
	}
}
