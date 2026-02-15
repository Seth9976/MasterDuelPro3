using System;

namespace System.Windows.Forms
{
	// Token: 0x020000DA RID: 218
	internal class ImplicitHScrollBar : HScrollBar
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x00022EDE File Offset: 0x000210DE
		public ImplicitHScrollBar()
		{
			this.implicit_control = true;
			base.SetStyle(ControlStyles.Selectable, false);
		}
	}
}
