using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 32-bit unsigned integer objects to and from various other representations.</summary>
	// Token: 0x020002A7 RID: 679
	public class UInt32Converter : BaseNumberConverter
	{
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x00044301 File Offset: 0x00042501
		internal override Type TargetType
		{
			get
			{
				return typeof(uint);
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0004430D File Offset: 0x0004250D
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt32(value, radix);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0004431B File Offset: 0x0004251B
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return uint.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0004432C File Offset: 0x0004252C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((uint)value).ToString("G", formatInfo);
		}
	}
}
