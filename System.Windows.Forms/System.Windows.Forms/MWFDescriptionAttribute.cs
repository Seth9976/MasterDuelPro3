using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace System
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
	internal sealed class MWFDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002081 File Offset: 0x00000281
		public MWFDescriptionAttribute(string category)
			: base(category)
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000208A File Offset: 0x0000028A
		public override string Description
		{
			get
			{
				return Locale.GetText(base.Description);
			}
		}
	}
}
