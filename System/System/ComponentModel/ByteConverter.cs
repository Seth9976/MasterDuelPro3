using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x0200025C RID: 604
	public class ByteConverter : BaseNumberConverter
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0003F37A File Offset: 0x0003D57A
		internal override Type TargetType
		{
			get
			{
				return typeof(byte);
			}
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003F386 File Offset: 0x0003D586
		internal override object FromString(string value, int radix)
		{
			return Convert.ToByte(value, radix);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0003F394 File Offset: 0x0003D594
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return byte.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0003F3A4 File Offset: 0x0003D5A4
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((byte)value).ToString("G", formatInfo);
		}
	}
}
