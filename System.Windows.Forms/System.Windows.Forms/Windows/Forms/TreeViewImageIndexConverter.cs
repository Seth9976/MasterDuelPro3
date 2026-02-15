using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	/// <summary>Provides a type converter to convert data for an image index to and from one data type to another for use by the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000208 RID: 520
	public class TreeViewImageIndexConverter : ImageIndexConverter
	{
		/// <returns>An <see cref="T:System.Object" /> that represents the converted <paramref name="value" />.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		// Token: 0x06001647 RID: 5703 RVA: 0x0006FA70 File Offset: 0x0006DC70
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || !(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			string text = (string)value;
			if (text.Equals("(default)", StringComparison.InvariantCultureIgnoreCase))
			{
				return -1;
			}
			if (text.Equals("(none)", StringComparison.InvariantCultureIgnoreCase))
			{
				return -2;
			}
			return int.Parse(text);
		}

		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture. </param>
		/// <param name="value">The <see cref="T:System.Object" /> to convert. </param>
		/// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.</param>
		// Token: 0x06001648 RID: 5704 RVA: 0x0006FAD0 File Offset: 0x0006DCD0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			if (value == null)
			{
				return string.Empty;
			}
			if (value is int && (int)value == -1)
			{
				return "(default)";
			}
			if (value is int && (int)value == -2)
			{
				return "(none)";
			}
			if (value is string && ((string)value).Length == 0)
			{
				return string.Empty;
			}
			return value.ToString();
		}

		/// <param name="context">Provides contextual information about the component.</param>
		// Token: 0x06001649 RID: 5705 RVA: 0x0006FB55 File Offset: 0x0006DD55
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(new int[] { -1, -2 });
		}
	}
}
