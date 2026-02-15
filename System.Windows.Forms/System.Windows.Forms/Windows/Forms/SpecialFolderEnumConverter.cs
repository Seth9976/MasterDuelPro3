using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x020000B0 RID: 176
	internal class SpecialFolderEnumConverter : TypeConverter
	{
		// Token: 0x06000698 RID: 1688 RVA: 0x0001D02B File Offset: 0x0001B22B
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value == null || !(value is string))
			{
				return base.ConvertFrom(context, culture, value);
			}
			return Enum.Parse(typeof(Environment.SpecialFolder), (string)value, true);
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x0001D058 File Offset: 0x0001B258
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (value == null || !(value is Environment.SpecialFolder) || destinationType != typeof(string))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			return ((Environment.SpecialFolder)value).ToString();
		}
	}
}
