using System;

namespace System.Windows.Forms
{
	// Token: 0x020000DB RID: 219
	internal class ImplicitVScrollBar : VScrollBar
	{
		// Token: 0x06000810 RID: 2064 RVA: 0x00022EF9 File Offset: 0x000210F9
		public ImplicitVScrollBar()
		{
			this.implicit_control = true;
			base.SetStyle(ControlStyles.Selectable, false);
		}
	}
}
