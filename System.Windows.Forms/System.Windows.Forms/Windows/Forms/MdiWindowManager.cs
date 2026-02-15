using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200012D RID: 301
	internal class MdiWindowManager : InternalWindowManager
	{
		// Token: 0x06000BB5 RID: 2997 RVA: 0x00032AD2 File Offset: 0x00030CD2
		public void RaiseActivated()
		{
			if (this.last_activation_event == 1)
			{
				return;
			}
			this.last_activation_event = 1;
			this.form.OnActivatedInternal();
			this.form.SelectActiveControl();
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00032AFB File Offset: 0x00030CFB
		public void RaiseDeactivate()
		{
			if (this.last_activation_event != 1)
			{
				return;
			}
			this.last_activation_event = 2;
			this.form.OnDeactivateInternal();
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x00002D70 File Offset: 0x00000F70
		public override int MenuHeight
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000BB8 RID: 3000 RVA: 0x00032B19 File Offset: 0x00030D19
		// (set) Token: 0x06000BB9 RID: 3001 RVA: 0x00032B21 File Offset: 0x00030D21
		internal bool IsVisiblePending
		{
			get
			{
				return this.is_visible_pending;
			}
			set
			{
				this.is_visible_pending = value;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000BBA RID: 3002 RVA: 0x00032B2C File Offset: 0x00030D2C
		private TitleButtons MaximizedTitleButtons
		{
			get
			{
				if (this.maximized_title_buttons == null)
				{
					this.maximized_title_buttons = new TitleButtons(base.Form);
					this.maximized_title_buttons.CloseButton.Visible = true;
					this.maximized_title_buttons.RestoreButton.Visible = true;
					this.maximized_title_buttons.MinimizeButton.Visible = true;
				}
				return this.maximized_title_buttons;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x00032B8C File Offset: 0x00030D8C
		internal override Rectangle MaximizedBounds
		{
			get
			{
				Rectangle clientRectangle = this.mdi_container.ClientRectangle;
				int num = ThemeEngine.Current.ManagedWindowBorderWidth(this);
				int titleBarHeight = base.TitleBarHeight;
				return new Rectangle(clientRectangle.Left - num, clientRectangle.Top - titleBarHeight - num, clientRectangle.Width + num * 2, clientRectangle.Height + titleBarHeight + num * 2);
			}
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00032BEC File Offset: 0x00030DEC
		public MdiWindowManager(Form form, MdiClient mdi_container)
			: base(form)
		{
			this.mdi_container = mdi_container;
			if (form.WindowState == FormWindowState.Normal)
			{
				base.NormalBounds = form.Bounds;
			}
			this.form_closed_handler = new EventHandler(this.FormClosed);
			form.Closed += this.form_closed_handler;
			form.TextChanged += this.FormTextChangedHandler;
			form.SizeChanged += this.FormSizeChangedHandler;
			form.LocationChanged += this.FormLocationChangedHandler;
			form.VisibleChanged += this.FormVisibleChangedHandler;
			this.draw_maximized_buttons = new PaintEventHandler(this.DrawMaximizedButtons);
			this.CreateIconMenus();
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x00032C9C File Offset: 0x00030E9C
		private void FormVisibleChangedHandler(object sender, EventArgs e)
		{
			if (this.mdi_container == null)
			{
				return;
			}
			if (this.form.Visible)
			{
				this.mdi_container.ActivateChild(this.form);
				return;
			}
			if (this.mdi_container.Controls.Count > 1)
			{
				this.mdi_container.ActivateActiveMdiChild();
			}
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00032CEF File Offset: 0x00030EEF
		private void FormTextChangedHandler(object sender, EventArgs e)
		{
			this.mdi_container.SetParentText(false);
			if (this.form.MdiParent.MainMenuStrip != null)
			{
				this.form.MdiParent.MainMenuStrip.RefreshMdiItems();
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x00032D24 File Offset: 0x00030F24
		private void FormLocationChangedHandler(object sender, EventArgs e)
		{
			if (this.form.window_state == FormWindowState.Minimized)
			{
				base.IconicBounds = this.form.Bounds;
			}
			this.form.MdiParent.MdiContainer.SizeScrollBars();
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00032D5C File Offset: 0x00030F5C
		private void FormSizeChangedHandler(object sender, EventArgs e)
		{
			if (this.form.window_state == FormWindowState.Maximized && this.form.Bounds != this.MaximizedBounds)
			{
				this.form.Bounds = this.MaximizedBounds;
			}
			this.form.MdiParent.MdiContainer.SizeScrollBars();
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x00032DB5 File Offset: 0x00030FB5
		public MainMenu MergedMenu
		{
			get
			{
				if (this.merged_menu == null)
				{
					this.merged_menu = this.CreateMergedMenu();
				}
				return this.merged_menu;
			}
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00032DD4 File Offset: 0x00030FD4
		private MainMenu CreateMergedMenu()
		{
			Form form = (Form)this.mdi_container.Parent;
			MainMenu mainMenu;
			if (form.Menu != null)
			{
				mainMenu = form.Menu.CloneMenu();
			}
			else
			{
				mainMenu = new MainMenu();
			}
			FormWindowState windowState = this.form.WindowState;
			mainMenu.MergeMenu(this.form.Menu);
			mainMenu.MenuChanged += this.MenuChangedHandler;
			mainMenu.SetForm(form);
			return mainMenu;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00032E47 File Offset: 0x00031047
		public MainMenu MaximizedMenu
		{
			get
			{
				if (this.maximized_menu == null)
				{
					this.maximized_menu = this.CreateMaximizedMenu();
				}
				return this.maximized_menu;
			}
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00032E64 File Offset: 0x00031064
		private MainMenu CreateMaximizedMenu()
		{
			Form form = (Form)this.mdi_container.Parent;
			if (this.form.MainMenuStrip != null || form.MainMenuStrip != null)
			{
				return null;
			}
			MainMenu mainMenu = new MainMenu();
			if (form.Menu != null)
			{
				MainMenu mainMenu2 = form.Menu.CloneMenu();
				mainMenu.MergeMenu(mainMenu2);
			}
			if (this.form.Menu != null)
			{
				MainMenu mainMenu3 = this.form.Menu.CloneMenu();
				mainMenu.MergeMenu(mainMenu3);
			}
			if (mainMenu.MenuItems.Count == 0)
			{
				mainMenu.MenuItems.Add(new MenuItem());
			}
			mainMenu.MenuItems.Insert(0, this.icon_menu);
			mainMenu.SetForm(form);
			return mainMenu;
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x00032F18 File Offset: 0x00031118
		private void CreateIconMenus()
		{
			this.icon_menu = new MenuItem();
			this.icon_popup_menu = new ContextMenu();
			this.icon_menu.OwnerDraw = true;
			this.icon_menu.MeasureItem += this.MeasureIconMenuItem;
			this.icon_menu.DrawItem += this.DrawIconMenuItem;
			this.icon_menu.Click += this.ClickIconMenuItem;
			MenuItem menuItem = new MenuItem("&Restore", new EventHandler(this.RestoreItemHandler));
			MenuItem menuItem2 = new MenuItem("&Move", new EventHandler(this.MoveItemHandler));
			MenuItem menuItem3 = new MenuItem("&Size", new EventHandler(this.SizeItemHandler));
			MenuItem menuItem4 = new MenuItem("Mi&nimize", new EventHandler(this.MinimizeItemHandler));
			MenuItem menuItem5 = new MenuItem("Ma&ximize", new EventHandler(this.MaximizeItemHandler));
			MenuItem menuItem6 = new MenuItem("&Close", new EventHandler(this.CloseItemHandler));
			MenuItem menuItem7 = new MenuItem("Nex&t", new EventHandler(this.NextItemHandler));
			this.icon_menu.MenuItems.AddRange(new MenuItem[] { menuItem, menuItem2, menuItem3, menuItem4, menuItem5, menuItem6, menuItem7 });
			this.icon_popup_menu.MenuItems.AddRange(new MenuItem[] { menuItem, menuItem2, menuItem3, menuItem4, menuItem5, menuItem6, menuItem7 });
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0003309C File Offset: 0x0003129C
		private void ClickIconMenuItem(object sender, EventArgs e)
		{
			if ((DateTime.Now - this.icon_clicked_time).TotalMilliseconds <= (double)SystemInformation.DoubleClickTime)
			{
				this.form.Close();
				return;
			}
			this.icon_clicked_time = DateTime.Now;
			Point point = Point.Empty;
			point = this.form.MdiParent.PointToScreen(point);
			point = this.form.PointToClient(point);
			this.ShowPopup(point);
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0003310C File Offset: 0x0003130C
		internal void ShowPopup(Point pnt)
		{
			if (this.form.WindowState == FormWindowState.Maximized && this.form.MdiParent.MainMenuStrip != null && this.form.MdiParent.MainMenuStrip.Items.Count > 0)
			{
				ToolStripItem toolStripItem = this.form.MdiParent.MainMenuStrip.Items[0];
				if (toolStripItem is MdiControlStrip.SystemMenuItem)
				{
					(toolStripItem as MdiControlStrip.SystemMenuItem).ShowDropDown();
					return;
				}
			}
			this.icon_popup_menu.MenuItems[0].Enabled = this.form.window_state > FormWindowState.Normal;
			this.icon_popup_menu.MenuItems[1].Enabled = this.form.window_state != FormWindowState.Maximized;
			this.icon_popup_menu.MenuItems[2].Enabled = this.form.window_state != FormWindowState.Maximized;
			this.icon_popup_menu.MenuItems[3].Enabled = this.form.window_state != FormWindowState.Minimized;
			this.icon_popup_menu.MenuItems[4].Enabled = this.form.window_state != FormWindowState.Maximized;
			this.icon_popup_menu.MenuItems[5].Enabled = true;
			this.icon_popup_menu.MenuItems[6].Enabled = true;
			this.icon_popup_menu.Show(this.form, pnt);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00033286 File Offset: 0x00031486
		private void RestoreItemHandler(object sender, EventArgs e)
		{
			this.form.WindowState = FormWindowState.Normal;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x00033294 File Offset: 0x00031494
		private void MoveItemHandler(object sender, EventArgs e)
		{
			int num = 0;
			int num2 = 0;
			this.PointToScreen(ref num, ref num2);
			Cursor.Position = new Point(num, num2);
			this.form.Cursor = Cursors.Cross;
			this.state = InternalWindowManager.State.Moving;
			this.form.Capture = true;
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000332E0 File Offset: 0x000314E0
		private void SizeItemHandler(object sender, EventArgs e)
		{
			int num = 0;
			int num2 = 0;
			this.PointToScreen(ref num, ref num2);
			Cursor.Position = new Point(num, num2);
			this.form.Cursor = Cursors.Cross;
			this.state = InternalWindowManager.State.Sizing;
			this.form.Capture = true;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x0003332A File Offset: 0x0003152A
		private void MinimizeItemHandler(object sender, EventArgs e)
		{
			this.form.WindowState = FormWindowState.Minimized;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00033338 File Offset: 0x00031538
		private void MaximizeItemHandler(object sender, EventArgs e)
		{
			if (this.form.WindowState != FormWindowState.Maximized)
			{
				this.form.WindowState = FormWindowState.Maximized;
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x00033354 File Offset: 0x00031554
		private void CloseItemHandler(object sender, EventArgs e)
		{
			this.form.Close();
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00033361 File Offset: 0x00031561
		private void NextItemHandler(object sender, EventArgs e)
		{
			this.mdi_container.ActivateNextChild();
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00033370 File Offset: 0x00031570
		private void DrawIconMenuItem(object sender, DrawItemEventArgs de)
		{
			de.Graphics.DrawIcon(this.form.Icon, new Rectangle(de.Bounds.X + 2, de.Bounds.Y + 2, de.Bounds.Height - 4, de.Bounds.Height - 4));
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000333D8 File Offset: 0x000315D8
		private void MeasureIconMenuItem(object sender, MeasureItemEventArgs me)
		{
			int menuHeight = SystemInformation.MenuHeight;
			me.ItemHeight = menuHeight;
			me.ItemWidth = menuHeight + 2;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000333FB File Offset: 0x000315FB
		private void MenuChangedHandler(object sender, EventArgs e)
		{
			this.CreateMergedMenu();
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00033404 File Offset: 0x00031604
		public override void PointToClient(ref int x, ref int y)
		{
			XplatUI.ScreenToClient(this.mdi_container.Handle, ref x, ref y);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00033418 File Offset: 0x00031618
		public override void PointToScreen(ref int x, ref int y)
		{
			XplatUI.ClientToScreen(this.mdi_container.Handle, ref x, ref y);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0003342C File Offset: 0x0003162C
		public override void UpdateWindowDecorations(FormWindowState window_state)
		{
			if (this.MaximizedMenu != null)
			{
				if (window_state > FormWindowState.Minimized)
				{
					if (window_state == FormWindowState.Maximized)
					{
						this.MaximizedMenu.Paint += this.draw_maximized_buttons;
						this.MaximizedTitleButtons.Visible = true;
						base.TitleButtons.Visible = false;
					}
				}
				else
				{
					this.MaximizedMenu.Paint -= this.draw_maximized_buttons;
					this.MaximizedTitleButtons.Visible = false;
					base.TitleButtons.Visible = true;
				}
			}
			base.UpdateWindowDecorations(window_state);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000334A6 File Offset: 0x000316A6
		public override void SetWindowState(FormWindowState old_state, FormWindowState window_state)
		{
			this.mdi_container.SetWindowState(this.form, old_state, window_state, false);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000334BC File Offset: 0x000316BC
		private void FormClosed(object sender, EventArgs e)
		{
			this.mdi_container.ChildFormClosed(this.form);
			if (this.form.MdiParent.MainMenuStrip != null)
			{
				this.form.MdiParent.MainMenuStrip.RefreshMdiItems();
			}
			this.mdi_container.RemoveControlMenuItems(this);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00033510 File Offset: 0x00031710
		public override void DrawMaximizedButtons(object sender, PaintEventArgs pe)
		{
			Size size = ThemeEngine.Current.ManagedWindowGetMenuButtonSize(this);
			Point menuOrigin = XplatUI.GetMenuOrigin(this.mdi_container.ParentForm.Handle);
			int num = ThemeEngine.Current.ManagedWindowBorderWidth(this);
			TitleButtons maximizedTitleButtons = this.MaximizedTitleButtons;
			maximizedTitleButtons.Visible = true;
			base.TitleButtons.Visible = false;
			maximizedTitleButtons.CloseButton.Rectangle = new Rectangle(this.mdi_container.ParentForm.Size.Width - 1 - num - size.Width - 2, menuOrigin.Y + 2, size.Width, size.Height);
			maximizedTitleButtons.RestoreButton.Rectangle = new Rectangle(maximizedTitleButtons.CloseButton.Rectangle.Left - 2 - size.Width, menuOrigin.Y + 2, size.Width, size.Height);
			maximizedTitleButtons.MinimizeButton.Rectangle = new Rectangle(maximizedTitleButtons.RestoreButton.Rectangle.Left - size.Width, menuOrigin.Y + 2, size.Width, size.Height);
			base.DrawTitleButton(pe.Graphics, maximizedTitleButtons.MinimizeButton, pe.ClipRectangle);
			base.DrawTitleButton(pe.Graphics, maximizedTitleButtons.RestoreButton, pe.ClipRectangle);
			base.DrawTitleButton(pe.Graphics, maximizedTitleButtons.CloseButton, pe.ClipRectangle);
			TitleButton minimizeButton = maximizedTitleButtons.MinimizeButton;
			minimizeButton.Rectangle.Y = minimizeButton.Rectangle.Y - menuOrigin.Y;
			TitleButton restoreButton = maximizedTitleButtons.RestoreButton;
			restoreButton.Rectangle.Y = restoreButton.Rectangle.Y - menuOrigin.Y;
			TitleButton closeButton = maximizedTitleButtons.CloseButton;
			closeButton.Rectangle.Y = closeButton.Rectangle.Y - menuOrigin.Y;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000336D8 File Offset: 0x000318D8
		public bool HandleMenuMouseDown(MainMenu menu, int x, int y)
		{
			Point point = MenuTracker.ScreenToMenu(menu, new Point(x, y));
			this.HandleTitleBarDown(point.X, point.Y);
			return base.TitleButtons.AnyPushedTitleButtons;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00033714 File Offset: 0x00031914
		public void HandleMenuMouseUp(MainMenu menu, int x, int y)
		{
			Point point = MenuTracker.ScreenToMenu(menu, new Point(x, y));
			this.HandleTitleBarUp(point.X, point.Y);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00033744 File Offset: 0x00031944
		public void HandleMenuMouseLeave(MainMenu menu, int x, int y)
		{
			Point point = MenuTracker.ScreenToMenu(menu, new Point(x, y));
			this.HandleTitleBarLeave(point.X, point.Y);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00033774 File Offset: 0x00031974
		public void HandleMenuMouseMove(MainMenu menu, int x, int y)
		{
			Point point = MenuTracker.ScreenToMenu(menu, new Point(x, y));
			this.HandleTitleBarMouseMove(point.X, point.Y);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x000337A3 File Offset: 0x000319A3
		protected override void HandleTitleBarLeave(int x, int y)
		{
			base.HandleTitleBarLeave(x, y);
			if (this.maximized_title_buttons != null)
			{
				this.maximized_title_buttons.MouseLeave(x, y);
			}
			if (base.IsMaximized)
			{
				XplatUI.InvalidateNC(this.form.MdiParent.Handle);
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000337E0 File Offset: 0x000319E0
		protected override void HandleTitleBarUp(int x, int y)
		{
			if (!base.IconRectangleContains(x, y))
			{
				bool isMaximized = base.IsMaximized;
				base.HandleTitleBarUp(x, y);
				if (this.maximized_title_buttons != null && isMaximized)
				{
					this.maximized_title_buttons.MouseUp(x, y);
				}
				if (base.IsMaximized)
				{
					XplatUI.InvalidateNC(this.mdi_container.Parent.Handle);
				}
				return;
			}
			if (this.icon_dont_show_popup)
			{
				this.icon_dont_show_popup = false;
				return;
			}
			if (base.IsMaximized)
			{
				this.ClickIconMenuItem(null, null);
				return;
			}
			this.ShowPopup(Point.Empty);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0003386A File Offset: 0x00031A6A
		protected override void HandleTitleBarDoubleClick(int x, int y)
		{
			if (base.IconRectangleContains(x, y))
			{
				this.form.Close();
			}
			else if (this.form.MaximizeBox)
			{
				this.form.WindowState = FormWindowState.Maximized;
			}
			base.HandleTitleBarDoubleClick(x, y);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000338A4 File Offset: 0x00031AA4
		protected override void HandleTitleBarDown(int x, int y)
		{
			if (!base.IconRectangleContains(x, y))
			{
				base.HandleTitleBarDown(x, y);
				if (this.maximized_title_buttons != null)
				{
					this.maximized_title_buttons.MouseDown(x, y);
				}
				if (base.IsMaximized)
				{
					XplatUI.InvalidateNC(this.mdi_container.Parent.Handle);
				}
				return;
			}
			if ((DateTime.Now - this.icon_clicked_time).TotalMilliseconds <= (double)SystemInformation.DoubleClickTime && this.icon_clicked.X == x && this.icon_clicked.Y == y)
			{
				this.form.Close();
				return;
			}
			this.icon_clicked_time = DateTime.Now;
			this.icon_clicked.X = x;
			this.icon_clicked.Y = y;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00033961 File Offset: 0x00031B61
		protected override void HandleTitleBarMouseMove(int x, int y)
		{
			base.HandleTitleBarMouseMove(x, y);
			if (this.maximized_title_buttons != null && this.maximized_title_buttons.MouseMove(x, y))
			{
				XplatUI.InvalidateNC(this.form.MdiParent.Handle);
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00033998 File Offset: 0x00031B98
		protected override bool HandleLButtonDblClick(ref Message m)
		{
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCClientToNC(ref num, ref num2);
			if (base.IconRectangleContains(num, num2))
			{
				this.icon_popup_menu.Wnd.Hide();
				this.form.Close();
				return true;
			}
			return base.HandleLButtonDblClick(ref m);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00033A08 File Offset: 0x00031C08
		protected override bool HandleLButtonDown(ref Message m)
		{
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCClientToNC(ref num, ref num2);
			if (base.IconRectangleContains(num, num2))
			{
				if ((DateTime.Now - this.icon_clicked_time).TotalMilliseconds <= (double)SystemInformation.DoubleClickTime)
				{
					if (this.icon_popup_menu != null && this.icon_popup_menu.Wnd != null)
					{
						this.icon_popup_menu.Wnd.Hide();
					}
					this.form.Close();
					return true;
				}
				if (this.form.Capture)
				{
					this.icon_dont_show_popup = true;
				}
			}
			return base.HandleLButtonDown(ref m);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00002D70 File Offset: 0x00000F70
		protected override bool ShouldRemoveWindowManager(FormBorderStyle style)
		{
			return false;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00033AC0 File Offset: 0x00031CC0
		protected override void HandleWindowMove(Message m)
		{
			Point position = Cursor.Position;
			Point point = base.MouseMove(position);
			if (point.X == 0 && point.Y == 0)
			{
				return;
			}
			int num = this.virtual_position.X + point.X;
			int num2 = this.virtual_position.Y + point.Y;
			Rectangle clientRectangle = this.mdi_container.ClientRectangle;
			if (this.mdi_container.VerticalScrollbarVisible)
			{
				clientRectangle.Width -= SystemInformation.VerticalScrollBarWidth;
			}
			if (this.mdi_container.HorizontalScrollbarVisible)
			{
				clientRectangle.Height -= SystemInformation.HorizontalScrollBarHeight;
			}
			base.UpdateVP(num, num2, this.form.Width, this.form.Height);
			this.start = position;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00033B88 File Offset: 0x00031D88
		protected override bool HandleNCMouseMove(ref Message m)
		{
			XplatUI.RequestAdditionalWM_NCMessages(this.form.Handle, true, true);
			return base.HandleNCMouseMove(ref m);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00033BA3 File Offset: 0x00031DA3
		protected override void DrawVirtualPosition(Rectangle virtual_position)
		{
			this.ClearVirtualPosition();
			if (this.form.Parent != null)
			{
				XplatUI.DrawReversibleRectangle(this.form.Parent.Handle, virtual_position, 2);
			}
			this.prev_virtual_position = virtual_position;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00033BD8 File Offset: 0x00031DD8
		protected override void ClearVirtualPosition()
		{
			if (this.prev_virtual_position != Rectangle.Empty && this.form.Parent != null)
			{
				XplatUI.DrawReversibleRectangle(this.form.Parent.Handle, this.prev_virtual_position, 2);
			}
			this.prev_virtual_position = Rectangle.Empty;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00033C2B File Offset: 0x00031E2B
		protected override void OnWindowFinishedMoving()
		{
			this.form.Refresh();
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x00033C38 File Offset: 0x00031E38
		public override bool IsActive
		{
			get
			{
				return this.mdi_container != null && this.mdi_container.ActiveMdiChild == this.form;
			}
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00033C57 File Offset: 0x00031E57
		protected override void Activate()
		{
			if (this.mdi_container.ActiveMdiChild != this.form)
			{
				this.mdi_container.ActivateChild(this.form);
			}
			base.Activate();
		}

		// Token: 0x04000790 RID: 1936
		private MainMenu merged_menu;

		// Token: 0x04000791 RID: 1937
		private MainMenu maximized_menu;

		// Token: 0x04000792 RID: 1938
		private MenuItem icon_menu;

		// Token: 0x04000793 RID: 1939
		private ContextMenu icon_popup_menu;

		// Token: 0x04000794 RID: 1940
		internal bool was_minimized;

		// Token: 0x04000795 RID: 1941
		private PaintEventHandler draw_maximized_buttons;

		// Token: 0x04000796 RID: 1942
		internal EventHandler form_closed_handler;

		// Token: 0x04000797 RID: 1943
		private MdiClient mdi_container;

		// Token: 0x04000798 RID: 1944
		private Rectangle prev_virtual_position;

		// Token: 0x04000799 RID: 1945
		private Point icon_clicked;

		// Token: 0x0400079A RID: 1946
		private DateTime icon_clicked_time;

		// Token: 0x0400079B RID: 1947
		private bool icon_dont_show_popup;

		// Token: 0x0400079C RID: 1948
		private TitleButtons maximized_title_buttons;

		// Token: 0x0400079D RID: 1949
		private bool is_visible_pending;

		// Token: 0x0400079E RID: 1950
		private byte last_activation_event;
	}
}
