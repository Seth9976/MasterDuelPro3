using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020000E1 RID: 225
	internal class TitleButton
	{
		// Token: 0x0600085C RID: 2140 RVA: 0x000242F0 File Offset: 0x000224F0
		public TitleButton(CaptionButton caption, EventHandler clicked)
		{
			this.Caption = caption;
			this.Clicked = clicked;
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00024306 File Offset: 0x00022506
		public void OnClick()
		{
			if (this.Clicked != null)
			{
				this.Clicked(this, EventArgs.Empty);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00024321 File Offset: 0x00022521
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00024329 File Offset: 0x00022529
		public bool Entered
		{
			get
			{
				return this.entered;
			}
			set
			{
				this.entered = value;
			}
		}

		// Token: 0x04000549 RID: 1353
		public Rectangle Rectangle;

		// Token: 0x0400054A RID: 1354
		public ButtonState State;

		// Token: 0x0400054B RID: 1355
		public CaptionButton Caption;

		// Token: 0x0400054C RID: 1356
		private EventHandler Clicked;

		// Token: 0x0400054D RID: 1357
		public bool Visible;

		// Token: 0x0400054E RID: 1358
		private bool entered;
	}
}
