using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert single-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x0200029C RID: 668
	public class SingleConverter : BaseNumberConverter
	{
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x000028AE File Offset: 0x00000AAE
		internal override bool AllowHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x00043D2F File Offset: 0x00041F2F
		internal override Type TargetType
		{
			get
			{
				return typeof(float);
			}
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00043D3B File Offset: 0x00041F3B
		internal override object FromString(string value, int radix)
		{
			return Convert.ToSingle(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x00043D4D File Offset: 0x00041F4D
		internal override object FromString(string value, NumberFormatInfo formatInfo)
		{
			return float.Parse(value, NumberStyles.Float, formatInfo);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x00043D60 File Offset: 0x00041F60
		internal override string ToString(object value, NumberFormatInfo formatInfo)
		{
			return ((float)value).ToString("R", formatInfo);
		}
	}
}
