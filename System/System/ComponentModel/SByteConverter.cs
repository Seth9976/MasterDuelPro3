using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from a string.</summary>
	// Token: 0x0200029A RID: 666
	public class SByteConverter : BaseNumberConverter
	{
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000FFE RID: 4094 RVA: 0x00043C72 File Offset: 0x00041E72
		internal override Type TargetType
		{
			get
			{
				return typeof(sbyte);
			}
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00043C7E File Offset: 0x00041E7E
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSByte(value, radix);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00043C8C File Offset: 0x00041E8C
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return sbyte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x00043C9C File Offset: 0x00041E9C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((sbyte)value).ToString("G", formatInfo);
		}
	}
}
