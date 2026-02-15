using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x02000054 RID: 84
	internal sealed class DataTableTypeConverter : ReferenceConverter
	{
		// Token: 0x060004F5 RID: 1269 RVA: 0x00018D43 File Offset: 0x00016F43
		public DataTableTypeConverter()
			: base(typeof(DataTable))
		{
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00011ED5 File Offset: 0x000100D5
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}
