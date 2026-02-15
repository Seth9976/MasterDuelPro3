using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit signed integer objects to and from other representations.</summary>
	// Token: 0x02000285 RID: 645
	public class Int32Converter : BaseNumberConverter
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x00041F25 File Offset: 0x00040125
		internal override Type TargetType
		{
			get
			{
				return typeof(int);
			}
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00041F31 File Offset: 0x00040131
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt32(value, radix);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00041F3F File Offset: 0x0004013F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return int.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00041F50 File Offset: 0x00040150
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((int)value).ToString("G", formatInfo);
		}
	}
}
