using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit signed integer objects to and from other representations.</summary>
	// Token: 0x02000284 RID: 644
	public class Int16Converter : BaseNumberConverter
	{
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00041ED8 File Offset: 0x000400D8
		internal override Type TargetType
		{
			get
			{
				return typeof(short);
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x00041EE4 File Offset: 0x000400E4
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt16(value, radix);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x00041EF2 File Offset: 0x000400F2
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return short.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x00041F04 File Offset: 0x00040104
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((short)value).ToString("G", formatInfo);
		}
	}
}
