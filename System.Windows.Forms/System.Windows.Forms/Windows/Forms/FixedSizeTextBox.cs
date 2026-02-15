using System;

namespace System.Windows.Forms
{
	// Token: 0x020000A6 RID: 166
	internal class FixedSizeTextBox : TextBox
	{
		// Token: 0x06000666 RID: 1638 RVA: 0x0001BB98 File Offset: 0x00019D98
		public FixedSizeTextBox(bool fixed_horz, bool fixed_vert)
		{
			base.SetStyle(ControlStyles.FixedWidth, fixed_horz);
			base.SetStyle(ControlStyles.FixedHeight, fixed_vert);
		}
	}
}
