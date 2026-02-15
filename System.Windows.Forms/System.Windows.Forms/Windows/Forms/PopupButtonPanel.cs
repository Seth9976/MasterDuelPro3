using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000084 RID: 132
	internal class PopupButtonPanel : Control, IUpdateFolder
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x00015950 File Offset: 0x00013B50
		public PopupButtonPanel()
		{
			base.SuspendLayout();
			this.BackColor = Color.FromArgb(128, 128, 128);
			base.Size = new Size(89, 338);
			base.InternalBorderStyle = BorderStyle.Fixed3D;
			this.recentlyusedButton = new PopupButtonPanel.PopupButton();
			this.desktopButton = new PopupButtonPanel.PopupButton();
			this.personalButton = new PopupButtonPanel.PopupButton();
			this.mycomputerButton = new PopupButtonPanel.PopupButton();
			this.networkButton = new PopupButtonPanel.PopupButton();
			this.recentlyusedButton.Size = new Size(81, 64);
			this.recentlyusedButton.Image = ThemeEngine.Current.Images(UIIcon.PlacesRecentDocuments, 32);
			this.recentlyusedButton.BackColor = this.BackColor;
			this.recentlyusedButton.ForeColor = Color.Black;
			this.recentlyusedButton.Location = new Point(2, 2);
			this.recentlyusedButton.Text = Locale.GetText("Recently used");
			this.recentlyusedButton.Click += this.OnClickButton;
			this.desktopButton.Image = ThemeEngine.Current.Images(UIIcon.PlacesDesktop, 32);
			this.desktopButton.BackColor = this.BackColor;
			this.desktopButton.ForeColor = Color.Black;
			this.desktopButton.Size = new Size(81, 64);
			this.desktopButton.Location = new Point(2, 66);
			this.desktopButton.Text = Locale.GetText("Desktop");
			this.desktopButton.Click += this.OnClickButton;
			this.personalButton.Image = ThemeEngine.Current.Images(UIIcon.PlacesPersonal, 32);
			this.personalButton.BackColor = this.BackColor;
			this.personalButton.ForeColor = Color.Black;
			this.personalButton.Size = new Size(81, 64);
			this.personalButton.Location = new Point(2, 130);
			this.personalButton.Text = Locale.GetText("Personal");
			this.personalButton.Click += this.OnClickButton;
			this.mycomputerButton.Image = ThemeEngine.Current.Images(UIIcon.PlacesMyComputer, 32);
			this.mycomputerButton.BackColor = this.BackColor;
			this.mycomputerButton.ForeColor = Color.Black;
			this.mycomputerButton.Size = new Size(81, 64);
			this.mycomputerButton.Location = new Point(2, 194);
			this.mycomputerButton.Text = Locale.GetText("My Computer");
			this.mycomputerButton.Click += this.OnClickButton;
			this.networkButton.Image = ThemeEngine.Current.Images(UIIcon.PlacesMyNetwork, 32);
			this.networkButton.BackColor = this.BackColor;
			this.networkButton.ForeColor = Color.Black;
			this.networkButton.Size = new Size(81, 64);
			this.networkButton.Location = new Point(2, 258);
			this.networkButton.Text = Locale.GetText("My Network");
			this.networkButton.Click += this.OnClickButton;
			base.Controls.Add(this.recentlyusedButton);
			base.Controls.Add(this.desktopButton);
			base.Controls.Add(this.personalButton);
			base.Controls.Add(this.mycomputerButton);
			base.Controls.Add(this.networkButton);
			base.ResumeLayout(false);
			base.KeyDown += this.Key_Down;
			base.SetStyle(ControlStyles.StandardClick, false);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00015D14 File Offset: 0x00013F14
		private void OnClickButton(object sender, EventArgs e)
		{
			if (this.lastPopupButton != null && this.lastPopupButton != sender as PopupButtonPanel.PopupButton)
			{
				this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
			}
			this.lastPopupButton = sender as PopupButtonPanel.PopupButton;
			if (sender == this.recentlyusedButton)
			{
				this.currentPath = MWFVFS.RecentlyUsedPrefix;
			}
			else if (sender == this.desktopButton)
			{
				this.currentPath = MWFVFS.DesktopPrefix;
			}
			else if (sender == this.personalButton)
			{
				this.currentPath = MWFVFS.PersonalPrefix;
			}
			else if (sender == this.mycomputerButton)
			{
				this.currentPath = MWFVFS.MyComputerPrefix;
			}
			else if (sender == this.networkButton)
			{
				this.currentPath = MWFVFS.MyNetworkPrefix;
			}
			EventHandler eventHandler = (EventHandler)base.Events[PopupButtonPanel.PDirectoryChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00015DE0 File Offset: 0x00013FE0
		internal void OnUIAFocusedItemChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[PopupButtonPanel.UIAFocusedItemChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00015FB2 File Offset: 0x000141B2
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00015E14 File Offset: 0x00014014
		public string CurrentFolder
		{
			get
			{
				return this.currentPath;
			}
			set
			{
				if (value == MWFVFS.RecentlyUsedPrefix)
				{
					if (this.lastPopupButton != this.recentlyusedButton)
					{
						if (this.lastPopupButton != null)
						{
							this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
						}
						this.recentlyusedButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
						this.lastPopupButton = this.recentlyusedButton;
						return;
					}
				}
				else if (value == MWFVFS.DesktopPrefix)
				{
					if (this.lastPopupButton != this.desktopButton)
					{
						if (this.lastPopupButton != null)
						{
							this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
						}
						this.desktopButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
						this.lastPopupButton = this.desktopButton;
						return;
					}
				}
				else if (value == MWFVFS.PersonalPrefix)
				{
					if (this.lastPopupButton != this.personalButton)
					{
						if (this.lastPopupButton != null)
						{
							this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
						}
						this.personalButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
						this.lastPopupButton = this.personalButton;
						return;
					}
				}
				else if (value == MWFVFS.MyComputerPrefix)
				{
					if (this.lastPopupButton != this.mycomputerButton)
					{
						if (this.lastPopupButton != null)
						{
							this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
						}
						this.mycomputerButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
						this.lastPopupButton = this.mycomputerButton;
						return;
					}
				}
				else if (value == MWFVFS.MyNetworkPrefix)
				{
					if (this.lastPopupButton != this.networkButton)
					{
						if (this.lastPopupButton != null)
						{
							this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
						}
						this.networkButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
						this.lastPopupButton = this.networkButton;
						return;
					}
				}
				else if (this.lastPopupButton != null)
				{
					this.lastPopupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
					this.lastPopupButton = null;
				}
			}
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00015FBA File Offset: 0x000141BA
		protected override void OnGotFocus(EventArgs e)
		{
			if (this.lastPopupButton != this.recentlyusedButton)
			{
				this.recentlyusedButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Up;
				this.SetFocusButton(this.recentlyusedButton);
			}
			this.currentFocusIndex = 0;
			base.OnGotFocus(e);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00015FF0 File Offset: 0x000141F0
		protected override void OnLostFocus(EventArgs e)
		{
			if (this.focusButton != null && this.focusButton.ButtonState != PopupButtonPanel.PopupButton.PopupButtonState.Down)
			{
				this.focusButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
			}
			base.OnLostFocus(e);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001601B File Offset: 0x0001421B
		protected override bool IsInputKey(Keys key)
		{
			return key == Keys.Return || key - Keys.Left <= 3 || base.IsInputKey(key);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00016034 File Offset: 0x00014234
		private void Key_Down(object sender, KeyEventArgs e)
		{
			bool flag = false;
			if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
			{
				this.currentFocusIndex--;
				if (this.currentFocusIndex < 0)
				{
					this.currentFocusIndex = base.Controls.Count - 1;
				}
				flag = true;
			}
			else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right)
			{
				this.currentFocusIndex++;
				if (this.currentFocusIndex == base.Controls.Count)
				{
					this.currentFocusIndex = 0;
				}
				flag = true;
			}
			else if (e.KeyCode == Keys.Return)
			{
				this.focusButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
				this.OnClickButton(this.focusButton, EventArgs.Empty);
			}
			if (flag)
			{
				PopupButtonPanel.PopupButton popupButton = base.Controls[this.currentFocusIndex] as PopupButtonPanel.PopupButton;
				if (this.focusButton != null && this.focusButton.ButtonState != PopupButtonPanel.PopupButton.PopupButtonState.Down)
				{
					this.focusButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
				}
				if (popupButton.ButtonState != PopupButtonPanel.PopupButton.PopupButtonState.Down)
				{
					popupButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Up;
				}
				this.SetFocusButton(popupButton);
			}
			e.Handled = true;
		}

		// Token: 0x14000029 RID: 41
		// (add) Token: 0x06000578 RID: 1400 RVA: 0x00016146 File Offset: 0x00014346
		// (remove) Token: 0x06000579 RID: 1401 RVA: 0x00016159 File Offset: 0x00014359
		public event EventHandler DirectoryChanged
		{
			add
			{
				base.Events.AddHandler(PopupButtonPanel.PDirectoryChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(PopupButtonPanel.PDirectoryChangedEvent, value);
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001616C File Offset: 0x0001436C
		internal void SetFocusButton(PopupButtonPanel.PopupButton button)
		{
			if (button == this.focusButton)
			{
				return;
			}
			this.focusButton = button;
			this.OnUIAFocusedItemChanged();
		}

		// Token: 0x04000369 RID: 873
		private PopupButtonPanel.PopupButton recentlyusedButton;

		// Token: 0x0400036A RID: 874
		private PopupButtonPanel.PopupButton desktopButton;

		// Token: 0x0400036B RID: 875
		private PopupButtonPanel.PopupButton personalButton;

		// Token: 0x0400036C RID: 876
		private PopupButtonPanel.PopupButton mycomputerButton;

		// Token: 0x0400036D RID: 877
		private PopupButtonPanel.PopupButton networkButton;

		// Token: 0x0400036E RID: 878
		private PopupButtonPanel.PopupButton lastPopupButton;

		// Token: 0x0400036F RID: 879
		private PopupButtonPanel.PopupButton focusButton;

		// Token: 0x04000370 RID: 880
		private string currentPath;

		// Token: 0x04000371 RID: 881
		private int currentFocusIndex;

		// Token: 0x04000372 RID: 882
		private static object UIAFocusedItemChangedEvent = new object();

		// Token: 0x04000373 RID: 883
		private static object PDirectoryChangedEvent = new object();

		// Token: 0x02000085 RID: 133
		internal class PopupButton : Control
		{
			// Token: 0x0600057C RID: 1404 RVA: 0x0001619C File Offset: 0x0001439C
			public PopupButton()
			{
				this.text_format.Alignment = StringAlignment.Center;
				this.text_format.LineAlignment = StringAlignment.Near;
				base.SetStyle(ControlStyles.DoubleBuffer, true);
				base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.UserPaint, true);
				base.SetStyle(ControlStyles.Selectable, false);
			}

			// Token: 0x1700015C RID: 348
			// (set) Token: 0x0600057D RID: 1405 RVA: 0x00016209 File Offset: 0x00014409
			public Image Image
			{
				set
				{
					this.image = value;
					base.Invalidate();
				}
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x0600057F RID: 1407 RVA: 0x00016227 File Offset: 0x00014427
			// (set) Token: 0x0600057E RID: 1406 RVA: 0x00016218 File Offset: 0x00014418
			public PopupButtonPanel.PopupButton.PopupButtonState ButtonState
			{
				get
				{
					return this.popupButtonState;
				}
				set
				{
					this.popupButtonState = value;
					base.Invalidate();
				}
			}

			// Token: 0x06000580 RID: 1408 RVA: 0x0001622F File Offset: 0x0001442F
			protected override void OnPaint(PaintEventArgs pe)
			{
				this.Draw(pe);
				base.OnPaint(pe);
			}

			// Token: 0x06000581 RID: 1409 RVA: 0x00016240 File Offset: 0x00014440
			private void Draw(PaintEventArgs pe)
			{
				Graphics graphics = pe.Graphics;
				graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor), base.ClientRectangle);
				int num = 4;
				if (this.image != null)
				{
					int num2 = (base.ClientSize.Width - this.image.Width) / 2;
					int num3 = num;
					graphics.DrawImage(this.image, num2, num3);
				}
				if (this.Text != string.Empty)
				{
					if (this.text_rect == Rectangle.Empty)
					{
						int num4 = 2;
						int num5 = num4;
						int num6 = num + this.image.Height + 1;
						int num7 = base.ClientSize.Width - num5 - num4 - 1;
						int num8 = base.ClientSize.Height - num6 - 1;
						this.text_rect = new Rectangle(num5, num6, num7, num8);
					}
					graphics.DrawString(this.Text, this.Font, Brushes.White, this.text_rect, this.text_format);
				}
				PopupButtonPanel.PopupButton.PopupButtonState popupButtonState = this.popupButtonState;
				if (popupButtonState != PopupButtonPanel.PopupButton.PopupButtonState.Down)
				{
					if (popupButtonState == PopupButtonPanel.PopupButton.PopupButtonState.Up)
					{
						graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.White), 0, 0, base.ClientSize.Width - 1, 0);
						graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.White), 0, 0, 0, base.ClientSize.Height - 1);
						graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.Black), base.ClientSize.Width - 1, 0, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
						graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.Black), 0, base.ClientSize.Height - 1, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
						return;
					}
				}
				else
				{
					graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.Black), 0, 0, base.ClientSize.Width - 1, 0);
					graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.Black), 0, 0, 0, base.ClientSize.Height - 1);
					graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.White), base.ClientSize.Width - 1, 0, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
					graphics.DrawLine(ThemeEngine.Current.ResPool.GetPen(Color.White), 0, base.ClientSize.Height - 1, base.ClientSize.Width - 1, base.ClientSize.Height - 1);
				}
			}

			// Token: 0x06000582 RID: 1410 RVA: 0x00016560 File Offset: 0x00014760
			protected override void OnMouseEnter(EventArgs e)
			{
				if (this.popupButtonState != PopupButtonPanel.PopupButton.PopupButtonState.Down)
				{
					this.popupButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Up;
				}
				PopupButtonPanel popupButtonPanel = base.Parent as PopupButtonPanel;
				if (popupButtonPanel.focusButton != null && popupButtonPanel.focusButton.ButtonState == PopupButtonPanel.PopupButton.PopupButtonState.Up)
				{
					popupButtonPanel.focusButton.ButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
					popupButtonPanel.SetFocusButton(null);
				}
				base.Invalidate();
				base.OnMouseEnter(e);
			}

			// Token: 0x06000583 RID: 1411 RVA: 0x000165BF File Offset: 0x000147BF
			protected override void OnMouseLeave(EventArgs e)
			{
				if (this.popupButtonState == PopupButtonPanel.PopupButton.PopupButtonState.Up)
				{
					this.popupButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Normal;
				}
				base.Invalidate();
				base.OnMouseLeave(e);
			}

			// Token: 0x06000584 RID: 1412 RVA: 0x000165DE File Offset: 0x000147DE
			protected override void OnClick(EventArgs e)
			{
				this.popupButtonState = PopupButtonPanel.PopupButton.PopupButtonState.Down;
				base.Invalidate();
				base.OnClick(e);
			}

			// Token: 0x04000374 RID: 884
			private Image image;

			// Token: 0x04000375 RID: 885
			private PopupButtonPanel.PopupButton.PopupButtonState popupButtonState;

			// Token: 0x04000376 RID: 886
			private StringFormat text_format = new StringFormat();

			// Token: 0x04000377 RID: 887
			private Rectangle text_rect = Rectangle.Empty;

			// Token: 0x02000086 RID: 134
			internal enum PopupButtonState
			{
				// Token: 0x04000379 RID: 889
				Normal,
				// Token: 0x0400037A RID: 890
				Down,
				// Token: 0x0400037B RID: 891
				Up
			}
		}
	}
}
