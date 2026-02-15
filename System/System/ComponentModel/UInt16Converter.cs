using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020002A6 RID: 678
	public class UInt16Converter : BaseNumberConverter
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x000442B5 File Offset: 0x000424B5
		internal override Type TargetType
		{
			get
			{
				return typeof(ushort);
			}
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x000442C1 File Offset: 0x000424C1
		internal override object FromString(string value, int radix)
		{
			return Convert.ToUInt16(value, radix);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x000442CF File Offset: 0x000424CF
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return ushort.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x000442E0 File Offset: 0x000424E0
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((ushort)value).ToString("G", formatInfo);
		}
	}
}
