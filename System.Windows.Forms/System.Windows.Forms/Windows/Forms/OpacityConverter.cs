using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert opacity values to and from a string.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000157 RID: 343
	public class OpacityConverter : TypeConverter
	{
		/// <summary>Returns a value indicating whether this converter can convert an object of the specified source type to the native type of the converter that uses the specified context.</summary>
		/// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about the context of a type converter. </param>
		/// <param name="sourceType">The <see cref="T:System.Type" /> that represents the type you want to convert from. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D55 RID: 3413 RVA: 0x00022145 File Offset: 0x00020345
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string);
		}

		/// <summary>Converts the specified object to the converter's native type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about the context of a type converter. </param>
		/// <param name="culture">The locale information for the conversion. </param>
		/// <param name="value">The object to convert. </param>
		/// <exception cref="T:System.Exception">The object was not a supported type for the conversion.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="value" /> could not be properly converted to type <see cref="T:System.Double" />. -or- The resulting converted <paramref name="value" /> was less than zero percent or greater than one hundred percent.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D56 RID: 3414 RVA: 0x0003ABE4 File Offset: 0x00038DE4
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = (string)value;
				if (text.EndsWith("%"))
				{
					text = ((string)value).Substring(0, ((string)value).Length - 1);
				}
				return double.Parse(text, NumberStyles.Any, culture) / 100.0;
			}
			return base.ConvertFrom(context, culture, value);
		}

		/// <summary>Converts from the converter's native type to a value of the destination type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides information about the context of a type converter. </param>
		/// <param name="culture">The locale information for the conversion. </param>
		/// <param name="value">The value to convert. </param>
		/// <param name="destinationType">The type to convert the object to. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="destinationType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="value" /> cannot be converted to the <paramref name="destinationType" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D57 RID: 3415 RVA: 0x0003AC4C File Offset: 0x00038E4C
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return ((double)value * 100.0).ToString() + "%";
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
