using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x020000A8 RID: 168
	internal class FlatButtonAppearanceConverter : ExpandableObjectConverter
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x0001BC29 File Offset: 0x00019E29
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				return string.Empty;
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001BC4F File Offset: 0x00019E4F
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}
	}
}
