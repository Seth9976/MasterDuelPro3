using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000117 RID: 279
	internal class ListViewGroupConverter : TypeConverter
	{
		// Token: 0x06000AE6 RID: 2790 RVA: 0x00006F54 File Offset: 0x00005154
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002EA60 File Offset: 0x0002CC60
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new TypeConverter.StandardValuesCollection(new object[0]);
		}
	}
}
