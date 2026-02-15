using System;

namespace System.Windows.Forms
{
	// Token: 0x020000FC RID: 252
	internal class LabelEditTextBox : FixedSizeTextBox
	{
		// Token: 0x060008D0 RID: 2256 RVA: 0x00025619 File Offset: 0x00023819
		public LabelEditTextBox()
			: base(true, true)
		{
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00025624 File Offset: 0x00023824
		protected override bool IsInputKey(Keys key_data)
		{
			if ((key_data & Keys.Alt) == Keys.None)
			{
				Keys keys = key_data & Keys.KeyCode;
				if (keys == Keys.Return)
				{
					return true;
				}
				if (keys == Keys.Escape)
				{
					return true;
				}
			}
			return base.IsInputKey(key_data);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002565C File Offset: 0x0002385C
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (!base.Visible)
			{
				return;
			}
			Keys keyCode = e.KeyCode;
			if (keyCode == Keys.Return)
			{
				base.Visible = false;
				base.Parent.Focus();
				e.Handled = true;
				this.OnEditingFinished(e);
				return;
			}
			if (keyCode != Keys.Escape)
			{
				return;
			}
			base.Visible = false;
			base.Parent.Focus();
			e.Handled = true;
			this.OnEditingCancelled(e);
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x000256C7 File Offset: 0x000238C7
		protected override void OnLostFocus(EventArgs e)
		{
			if (base.Visible)
			{
				this.OnEditingFinished(e);
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000256D8 File Offset: 0x000238D8
		protected void OnEditingCancelled(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LabelEditTextBox.EditingCancelledEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00025708 File Offset: 0x00023908
		protected void OnEditingFinished(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[LabelEditTextBox.EditingFinishedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x060008D6 RID: 2262 RVA: 0x00025736 File Offset: 0x00023936
		// (remove) Token: 0x060008D7 RID: 2263 RVA: 0x00025749 File Offset: 0x00023949
		public event EventHandler EditingCancelled
		{
			add
			{
				base.Events.AddHandler(LabelEditTextBox.EditingCancelledEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LabelEditTextBox.EditingCancelledEvent, value);
			}
		}

		// Token: 0x14000033 RID: 51
		// (add) Token: 0x060008D8 RID: 2264 RVA: 0x0002575C File Offset: 0x0002395C
		// (remove) Token: 0x060008D9 RID: 2265 RVA: 0x0002575C File Offset: 0x0002395C
		public event EventHandler EditingFinished
		{
			add
			{
				base.Events.AddHandler(LabelEditTextBox.EditingFinishedEvent, value);
			}
			remove
			{
				base.Events.AddHandler(LabelEditTextBox.EditingFinishedEvent, value);
			}
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002576F File Offset: 0x0002396F
		// Note: this type is marked as 'beforefieldinit'.
		static LabelEditTextBox()
		{
			LabelEditTextBox.EditingCancelledEvent = new object();
			LabelEditTextBox.EditingFinishedEvent = new object();
		}
	}
}
