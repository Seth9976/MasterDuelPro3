using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 64-bit signed integer objects to and from various other representations.</summary>
	// Token: 0x02000286 RID: 646
	public class Int64Converter : BaseNumberConverter
	{
		// Token: 0x17000340 RID: 832
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00041F71 File Offset: 0x00040171
		internal override Type TargetType
		{
			get
			{
				return typeof(long);
			}
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x00041F7D File Offset: 0x0004017D
		internal override object FromString(string value, int radix)
		{
			return Convert.ToInt64(value, radix);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x00041F8B File Offset: 0x0004018B
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return long.Parse(value, NumberStyles.Integer, formatInfo);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00041F9C File Offset: 0x0004019C
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((long)value).ToString("G", formatInfo);
		}
	}
}
