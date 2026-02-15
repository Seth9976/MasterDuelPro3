using System;
using System.Windows.Forms.Theming.Default;

namespace System.Windows.Forms.Theming
{
	// Token: 0x02000374 RID: 884
	internal class ThemeElementsDefault
	{
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x06001CEA RID: 7402 RVA: 0x000883BB File Offset: 0x000865BB
		public virtual ButtonPainter ButtonPainter
		{
			get
			{
				if (this.buttonPainter == null)
				{
					this.buttonPainter = new ButtonPainter();
				}
				return this.buttonPainter;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x000883D6 File Offset: 0x000865D6
		public virtual LabelPainter LabelPainter
		{
			get
			{
				if (this.labelPainter == null)
				{
					this.labelPainter = new LabelPainter();
				}
				return this.labelPainter;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x000883F1 File Offset: 0x000865F1
		public virtual CheckBoxPainter CheckBoxPainter
		{
			get
			{
				if (this.checkBoxPainter == null)
				{
					this.checkBoxPainter = new CheckBoxPainter();
				}
				return this.checkBoxPainter;
			}
		}

		// Token: 0x04001842 RID: 6210
		protected ButtonPainter buttonPainter;

		// Token: 0x04001843 RID: 6211
		protected LabelPainter labelPainter;

		// Token: 0x04001844 RID: 6212
		protected CheckBoxPainter checkBoxPainter;
	}
}
