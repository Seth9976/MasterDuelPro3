using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert double-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x02000272 RID: 626
	public class DoubleConverter : BaseNumberConverter
	{
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x000028AE File Offset: 0x00000AAE
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00041481 File Offset: 0x0003F681
		internal override Type TargetType
		{
			get
			{
				return typeof(double);
			}
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0004148D File Offset: 0x0003F68D
		internal override object FromString(string value, int radix)
		{
			return Convert.ToDouble(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0004149F File Offset: 0x0003F69F
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return double.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000414B4 File Offset: 0x0003F6B4
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((double)value).ToString("R", formatInfo);
		}
	}
}
