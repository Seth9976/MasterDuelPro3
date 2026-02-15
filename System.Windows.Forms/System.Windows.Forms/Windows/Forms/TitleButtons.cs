using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x020000E2 RID: 226
	internal class TitleButtons : IEnumerable
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x00024334 File Offset: 0x00022534
		public TitleButtons(Form frm)
		{
			this.form = frm;
			this.Visible = true;
			this.MinimizeButton = new TitleButton(CaptionButton.Minimize, new EventHandler(this.ClickHandler));
			this.MaximizeButton = new TitleButton(CaptionButton.Maximize, new EventHandler(this.ClickHandler));
			this.RestoreButton = new TitleButton(CaptionButton.Restore, new EventHandler(this.ClickHandler));
			this.CloseButton = new TitleButton(CaptionButton.Close, new EventHandler(this.ClickHandler));
			this.HelpButton = new TitleButton(CaptionButton.Help, new EventHandler(this.ClickHandler));
			this.AllButtons = new TitleButton[] { this.MinimizeButton, this.MaximizeButton, this.RestoreButton, this.CloseButton, this.HelpButton };
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00024408 File Offset: 0x00022608
		private void ClickHandler(object sender, EventArgs e)
		{
			if (!this.Visible)
			{
				return;
			}
			switch (((TitleButton)sender).Caption)
			{
			case CaptionButton.Close:
				this.form.Close();
				return;
			case CaptionButton.Minimize:
				this.form.WindowState = FormWindowState.Minimized;
				return;
			case CaptionButton.Maximize:
				this.form.WindowState = FormWindowState.Maximized;
				return;
			case CaptionButton.Restore:
				this.form.WindowState = FormWindowState.Normal;
				return;
			case CaptionButton.Help:
				Console.WriteLine("Help not implemented.");
				return;
			default:
				return;
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00024484 File Offset: 0x00022684
		public TitleButton FindButton(int x, int y)
		{
			if (!this.Visible)
			{
				return null;
			}
			foreach (TitleButton titleButton in this.AllButtons)
			{
				if (titleButton.Visible && titleButton.Rectangle.Contains(x, y))
				{
					return titleButton;
				}
			}
			return null;
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x000244D0 File Offset: 0x000226D0
		public bool AnyPushedTitleButtons
		{
			get
			{
				if (!this.Visible)
				{
					return false;
				}
				foreach (TitleButton titleButton in this.AllButtons)
				{
					if (titleButton.Visible && titleButton.State == ButtonState.Pushed)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x00024518 File Offset: 0x00022718
		public IEnumerator GetEnumerator()
		{
			return this.AllButtons.GetEnumerator();
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x00024528 File Offset: 0x00022728
		public void ToolTipStart(TitleButton button)
		{
			this.tooltip_hovered_button = button;
			if (this.tooltip_hovered_button == this.tooltip_hidden_button)
			{
				return;
			}
			this.tooltip_hidden_button = null;
			if (this.tooltip != null && this.tooltip.Visible)
			{
				this.ToolTipShow(true);
			}
			if (this.tooltip_timer == null)
			{
				this.tooltip_timer = new Timer();
				this.tooltip_timer.Tick += this.ToolTipTimerTick;
			}
			this.tooltip_timer.Interval = 1000;
			this.tooltip_timer.Start();
			this.tooltip_hovered_button = button;
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000245BA File Offset: 0x000227BA
		public void ToolTipTimerTick(object sender, EventArgs e)
		{
			if (this.tooltip_timer.Interval == 3000)
			{
				this.tooltip_hidden_button = this.tooltip_hovered_button;
				this.ToolTipHide(false);
				return;
			}
			this.ToolTipShow(false);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000245EC File Offset: 0x000227EC
		public void ToolTipShow(bool only_refresh)
		{
			if (!this.form.Visible)
			{
				return;
			}
			string text = Locale.GetText(this.tooltip_hovered_button.Caption.ToString());
			this.tooltip_timer.Interval = 3000;
			this.tooltip_timer.Enabled = true;
			if (only_refresh && (this.tooltip == null || !this.tooltip.Visible))
			{
				return;
			}
			if (this.tooltip == null)
			{
				this.tooltip = new ToolTip.ToolTipWindow();
			}
			else
			{
				if (this.tooltip.Text == text && this.tooltip.Visible)
				{
					return;
				}
				if (this.tooltip.Visible)
				{
					this.tooltip.Visible = false;
				}
			}
			if (this.form.WindowState == FormWindowState.Maximized && this.form.MdiParent != null)
			{
				this.tooltip.Present(this.form.MdiParent, text);
				return;
			}
			this.tooltip.Present(this.form, text);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000246EC File Offset: 0x000228EC
		public void ToolTipHide(bool reset_hidden_button)
		{
			if (this.tooltip_timer != null)
			{
				this.tooltip_timer.Enabled = false;
			}
			if (this.tooltip != null && this.tooltip.Visible)
			{
				this.tooltip.Visible = false;
			}
			if (reset_hidden_button)
			{
				this.tooltip_hidden_button = null;
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00024738 File Offset: 0x00022938
		public bool MouseMove(int x, int y)
		{
			if (!this.Visible)
			{
				return false;
			}
			bool flag = false;
			bool anyPushedTitleButtons = this.AnyPushedTitleButtons;
			bool flag2 = false;
			TitleButton titleButton = this.FindButton(x, y);
			foreach (object obj in this)
			{
				TitleButton titleButton2 = (TitleButton)obj;
				if (titleButton2 != null && titleButton2.State != ButtonState.Inactive)
				{
					if (titleButton2 == titleButton)
					{
						if (anyPushedTitleButtons)
						{
							flag |= titleButton2.State != ButtonState.Pushed;
							titleButton2.State = ButtonState.Pushed;
						}
						this.ToolTipStart(titleButton2);
						flag2 = true;
						if (!titleButton2.Entered)
						{
							titleButton2.Entered = true;
							if (ThemeEngine.Current.ManagedWindowTitleButtonHasHotElementStyle(titleButton2, this.form))
							{
								flag = true;
							}
						}
					}
					else
					{
						if (anyPushedTitleButtons)
						{
							flag |= titleButton2.State > ButtonState.Normal;
							titleButton2.State = ButtonState.Normal;
						}
						if (titleButton2.Entered)
						{
							titleButton2.Entered = false;
							if (ThemeEngine.Current.ManagedWindowTitleButtonHasHotElementStyle(titleButton2, this.form))
							{
								flag = true;
							}
						}
					}
				}
			}
			if (!flag2)
			{
				this.ToolTipHide(false);
			}
			return flag;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00024870 File Offset: 0x00022A70
		public void MouseDown(int x, int y)
		{
			if (!this.Visible)
			{
				return;
			}
			this.ToolTipHide(false);
			foreach (object obj in this)
			{
				TitleButton titleButton = (TitleButton)obj;
				if (titleButton != null && titleButton.State != ButtonState.Inactive)
				{
					titleButton.State = ButtonState.Normal;
				}
			}
			TitleButton titleButton2 = this.FindButton(x, y);
			if (titleButton2 != null && titleButton2.State != ButtonState.Inactive)
			{
				titleButton2.State = ButtonState.Pushed;
			}
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00024908 File Offset: 0x00022B08
		public void MouseUp(int x, int y)
		{
			if (!this.Visible)
			{
				return;
			}
			TitleButton titleButton = this.FindButton(x, y);
			if (titleButton != null && titleButton.State != ButtonState.Inactive)
			{
				titleButton.OnClick();
			}
			foreach (object obj in this)
			{
				TitleButton titleButton2 = (TitleButton)obj;
				if (titleButton2 != null && titleButton2.State != ButtonState.Inactive)
				{
					titleButton2.State = ButtonState.Normal;
				}
			}
			if (titleButton == this.CloseButton && !this.form.closing)
			{
				XplatUI.InvalidateNC(this.form.Handle);
			}
			this.ToolTipHide(true);
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x000249C4 File Offset: 0x00022BC4
		internal void MouseLeave(int x, int y)
		{
			if (!this.Visible)
			{
				return;
			}
			foreach (object obj in this)
			{
				TitleButton titleButton = (TitleButton)obj;
				if (titleButton != null && titleButton.State != ButtonState.Inactive)
				{
					titleButton.State = ButtonState.Normal;
				}
			}
			this.ToolTipHide(true);
		}

		// Token: 0x0400054F RID: 1359
		public TitleButton MinimizeButton;

		// Token: 0x04000550 RID: 1360
		public TitleButton MaximizeButton;

		// Token: 0x04000551 RID: 1361
		public TitleButton RestoreButton;

		// Token: 0x04000552 RID: 1362
		public TitleButton CloseButton;

		// Token: 0x04000553 RID: 1363
		public TitleButton HelpButton;

		// Token: 0x04000554 RID: 1364
		public TitleButton[] AllButtons;

		// Token: 0x04000555 RID: 1365
		public bool Visible;

		// Token: 0x04000556 RID: 1366
		private ToolTip.ToolTipWindow tooltip;

		// Token: 0x04000557 RID: 1367
		private Timer tooltip_timer;

		// Token: 0x04000558 RID: 1368
		private TitleButton tooltip_hovered_button;

		// Token: 0x04000559 RID: 1369
		private TitleButton tooltip_hidden_button;

		// Token: 0x0400055A RID: 1370
		private Form form;
	}
}
