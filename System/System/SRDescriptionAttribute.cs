using System;
using System.ComponentModel;

namespace System
{
	// Token: 0x0200010E RID: 270
	[AttributeUsage(AttributeTargets.All)]
	internal class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0001C2C0 File Offset: 0x0001A4C0
		public SRDescriptionAttribute(string description)
			: base(description)
		{
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0001C2C9 File Offset: 0x0001A4C9
		public override string Description
		{
			get
			{
				if (!this.isReplaced)
				{
					this.isReplaced = true;
					base.DescriptionValue = global::Locale.GetText(base.DescriptionValue);
				}
				return base.DescriptionValue;
			}
		}

		// Token: 0x04000490 RID: 1168
		private bool isReplaced;
	}
}
