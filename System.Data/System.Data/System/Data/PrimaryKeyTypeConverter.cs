using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Data
{
	// Token: 0x02000086 RID: 134
	internal sealed class PrimaryKeyTypeConverter : ReferenceConverter
	{
		// Token: 0x060006E7 RID: 1767 RVA: 0x000225DE File Offset: 0x000207DE
		public PrimaryKeyTypeConverter()
			: base(typeof(DataColumn[]))
		{
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00012048 File Offset: 0x00010248
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000225F0 File Offset: 0x000207F0
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}
			if (!(destinationType == typeof(string)))
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
			return Array.Empty<DataColumn>().GetType().Name;
		}
	}
}
