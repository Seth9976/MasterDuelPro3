using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020002A8 RID: 680
	public class UInt64Converter : BaseNumberConverter
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x0004434D File Offset: 0x0004254D
		internal override Type TargetType
		{
			get
			{
				return typeof(ulong);
			}
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00044359 File Offset: 0x00042559
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt64(value, radix);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00044367 File Offset: 0x00042567
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ulong.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00044378 File Offset: 0x00042578
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ulong)value).ToString("G", formatInfo);
		}
	}
}
