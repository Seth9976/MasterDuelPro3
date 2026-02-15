using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace System
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
	internal sealed class MWFCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
		public MWFCategoryAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002079 File Offset: 0x00000279
		protected override string GetLocalizedString(string value)
		{
			return Locale.GetText(value);
		}
	}
}
