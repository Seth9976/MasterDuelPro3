using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x0200005F RID: 95
	internal sealed class DefaultValueTypeConverter : StringConverter
	{
		// Token: 0x060005AC RID: 1452 RVA: 0x0001A9D8 File Offset: 0x00018BD8
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (destinationType == typeof(string))
			{
				if (value == null)
				{
					return "<null>";
				}
				if (value == DBNull.Value)
				{
					return "<DBNull>";
				}
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001AA30 File Offset: 0x00018C30
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			if (value != null && value.GetType() == typeof(string))
			{
				string text = (string)value;
				if (string.Equals(text, "<null>", StringComparison.OrdinalIgnoreCase))
				{
					return null;
				}
				if (string.Equals(text, "<DBNull>", StringComparison.OrdinalIgnoreCase))
				{
					return DBNull.Value;
				}
			}
			return base.ConvertFrom(context, culture, value);
		}
	}
}
