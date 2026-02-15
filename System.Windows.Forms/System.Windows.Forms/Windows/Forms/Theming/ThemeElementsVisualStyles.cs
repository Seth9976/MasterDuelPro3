using System;
using System.Windows.Forms.Theming.Default;
using System.Windows.Forms.Theming.VisualStyles;

namespace System.Windows.Forms.Theming
{
	// Token: 0x02000375 RID: 885
	internal class ThemeElementsVisualStyles : ThemeElementsDefault
	{
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001CEE RID: 7406 RVA: 0x0008840C File Offset: 0x0008660C
		public override global::System.Windows.Forms.Theming.Default.CheckBoxPainter CheckBoxPainter
		{
			get
			{
				if (this.checkBoxPainter == null)
				{
					this.checkBoxPainter = new global::System.Windows.Forms.Theming.VisualStyles.CheckBoxPainter();
				}
				return this.checkBoxPainter;
			}
		}
	}
}
