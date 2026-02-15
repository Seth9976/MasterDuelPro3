using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Diagnostics.Design
{
	// Token: 0x02000006 RID: 6
	internal class StringValueConverter : TypeConverter
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000218C File Offset: 0x0000038C
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021CC File Offset: 0x000003CC
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string text = value as string;
			if (text == null)
			{
				return base.ConvertFrom(context, culture, value);
			}
			text = text.Trim();
			if (text.Length == 0)
			{
				return null;
			}
			return text;
		}
	}
}
