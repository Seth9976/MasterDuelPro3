using System;
using System.Collections;
using System.Drawing;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000132 RID: 306
	internal class MenuTracker
	{
		// Token: 0x06000C2A RID: 3114 RVA: 0x000345C0 File Offset: 0x000327C0
		public MenuTracker(Menu top_menu)
		{
			this.CurrentMenu = top_menu;
			this.TopMenu = top_menu;
			foreach (object obj in this.TopMenu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				this.AddShortcuts(menuItem);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0003464C File Offset: 0x0003284C
		public bool Navigating
		{
			get
			{
				return this.keynav_state != MenuTracker.KeyNavState.Idle || this.active;
			}
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00034660 File Offset: 0x00032860
		internal static Point ScreenToMenu(Menu menu, Point pnt)
		{
			int x = pnt.X;
			int y = pnt.Y;
			XplatUI.ScreenToMenu(menu.Wnd.window.Handle, ref x, ref y);
			return new Point(x, y);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000346A0 File Offset: 0x000328A0
		private void UpdateCursor()
		{
			Control realChildAtPoint = this.GrabControl.GetRealChildAtPoint(Cursor.Position);
			if (realChildAtPoint != null)
			{
				if (this.active)
				{
					XplatUI.SetCursor(realChildAtPoint.Handle, Cursors.Default.handle);
					return;
				}
				XplatUI.SetCursor(realChildAtPoint.Handle, realChildAtPoint.Cursor.handle);
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x000346F8 File Offset: 0x000328F8
		internal void Deactivate()
		{
			bool flag = this.keynav_state != MenuTracker.KeyNavState.Idle && this.TopMenu is MainMenu;
			this.active = false;
			this.popup_active = false;
			this.hotkey_active = false;
			if (this.GrabControl != null)
			{
				this.GrabControl.ActiveTracker = null;
			}
			this.keynav_state = MenuTracker.KeyNavState.Idle;
			if (this.TopMenu is ContextMenu)
			{
				PopUpWindow popUpWindow = this.TopMenu.Wnd as PopUpWindow;
				this.DeselectItem(this.TopMenu.SelectedItem);
				if (popUpWindow != null)
				{
					popUpWindow.HideWindow();
				}
			}
			else
			{
				this.DeselectItem(this.TopMenu.SelectedItem);
			}
			this.CurrentMenu = this.TopMenu;
			if (flag)
			{
				(this.TopMenu as MainMenu).Draw();
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x000347B8 File Offset: 0x000329B8
		private MenuItem FindItemByCoords(Menu menu, Point pt)
		{
			if (menu is MainMenu)
			{
				pt = MenuTracker.ScreenToMenu(menu, pt);
			}
			else
			{
				if (menu.Wnd == null)
				{
					return null;
				}
				pt = menu.Wnd.PointToClient(pt);
			}
			foreach (object obj in menu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				Rectangle bounds = menuItem.bounds;
				if (bounds.Contains(pt))
				{
					return menuItem;
				}
			}
			return null;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00034850 File Offset: 0x00032A50
		private MenuItem GetItemAtXY(int x, int y)
		{
			Point point = new Point(x, y);
			MenuItem menuItem = null;
			if (this.TopMenu.SelectedItem != null)
			{
				menuItem = this.FindSubItemByCoord(this.TopMenu.SelectedItem, Control.MousePosition);
			}
			if (menuItem == null)
			{
				menuItem = this.FindItemByCoords(this.TopMenu, point);
			}
			return menuItem;
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000348A0 File Offset: 0x00032AA0
		public bool OnMouseDown(MouseEventArgs args)
		{
			MenuItem itemAtXY = this.GetItemAtXY(args.X, args.Y);
			this.mouse_down = true;
			if (itemAtXY == null)
			{
				this.Deactivate();
				return false;
			}
			if ((args.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return true;
			}
			if (!itemAtXY.Enabled)
			{
				return true;
			}
			this.popdown_menu = this.active && itemAtXY.VisibleItems;
			if (itemAtXY.IsPopup || itemAtXY.Parent is MainMenu)
			{
				this.active = true;
				itemAtXY.Parent.InvalidateItem(itemAtXY);
			}
			if (this.CurrentMenu == this.TopMenu && !this.popdown_menu)
			{
				this.SelectItem(itemAtXY.Parent, itemAtXY, itemAtXY.IsPopup);
			}
			this.GrabControl.ActiveTracker = this;
			return true;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00034964 File Offset: 0x00032B64
		public void OnMotion(MouseEventArgs args)
		{
			if (args.Location == this.last_motion)
			{
				return;
			}
			this.last_motion = args.Location;
			MenuItem itemAtXY = this.GetItemAtXY(args.X, args.Y);
			this.UpdateCursor();
			if (this.CurrentMenu.SelectedItem == itemAtXY)
			{
				return;
			}
			this.GrabControl.ActiveTracker = ((this.active || itemAtXY != null) ? this : null);
			if (itemAtXY != null)
			{
				this.keynav_state = MenuTracker.KeyNavState.Idle;
				this.SelectItem(itemAtXY.Parent, itemAtXY, this.active && itemAtXY.IsPopup && this.popup_active && this.CurrentMenu.SelectedItem != itemAtXY);
				return;
			}
			MenuItem selectedItem = this.CurrentMenu.SelectedItem;
			if (this.active && selectedItem.VisibleItems && selectedItem.IsPopup && this.CurrentMenu is MainMenu)
			{
				return;
			}
			if (this.keynav_state == MenuTracker.KeyNavState.Navigating)
			{
				return;
			}
			if (selectedItem.Parent is MenuItem)
			{
				MenuItem menuItem = selectedItem.Parent as MenuItem;
				if (menuItem.IsPopup)
				{
					this.SelectItem(menuItem.Parent, menuItem, false);
					return;
				}
			}
			if (this.CurrentMenu != this.TopMenu)
			{
				this.CurrentMenu = this.CurrentMenu.parent_menu;
			}
			this.DeselectItem(selectedItem);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00034AAC File Offset: 0x00032CAC
		public void OnMouseUp(MouseEventArgs args)
		{
			if (!this.mouse_down)
			{
				return;
			}
			this.mouse_down = false;
			if ((args.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return;
			}
			MenuItem itemAtXY = this.GetItemAtXY(args.X, args.Y);
			if (itemAtXY == null)
			{
				this.Deactivate();
				return;
			}
			if (!itemAtXY.Enabled)
			{
				return;
			}
			if ((this.CurrentMenu == this.TopMenu && !(this.CurrentMenu is ContextMenu) && this.popdown_menu) || !itemAtXY.IsPopup)
			{
				this.Deactivate();
				this.UpdateCursor();
			}
			if (!itemAtXY.IsPopup)
			{
				this.DeselectItem(itemAtXY);
				if (this.TopMenu != null && this.TopMenu.Wnd != null)
				{
					Form form = this.TopMenu.Wnd.FindForm();
					if (form != null)
					{
						form.OnMenuComplete(EventArgs.Empty);
					}
				}
				itemAtXY.PerformClick();
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00034B80 File Offset: 0x00032D80
		public static bool TrackPopupMenu(Menu menu, Point pnt)
		{
			if (menu.MenuItems.Count <= 0)
			{
				return true;
			}
			MenuTracker tracker = menu.tracker;
			tracker.active = true;
			tracker.popup_active = true;
			Control sourceControl = (tracker.TopMenu as ContextMenu).SourceControl;
			tracker.GrabControl = sourceControl.FindForm();
			if (tracker.GrabControl == null)
			{
				tracker.GrabControl = sourceControl.FindRootParent();
			}
			tracker.GrabControl.ActiveTracker = tracker;
			menu.Wnd = new PopUpWindow(tracker.GrabControl, menu);
			menu.Wnd.Location = menu.Wnd.PointToClient(pnt);
			((PopUpWindow)menu.Wnd).ShowWindow();
			bool flag = true;
			object obj = XplatUI.StartLoop(Thread.CurrentThread);
			while (menu.Wnd != null && menu.Wnd.Visible && flag)
			{
				MSG msg = default(MSG);
				flag = XplatUI.GetMessage(obj, ref msg, IntPtr.Zero, 0, 0);
				Msg message = msg.message;
				if (message - Msg.WM_KEYDOWN <= 2 || message - Msg.WM_SYSKEYDOWN <= 2)
				{
					Control control = Control.FromHandle(msg.hwnd);
					if (control != null)
					{
						Message message2 = Message.Create(msg.hwnd, (int)msg.message, msg.wParam, msg.lParam);
						control.PreProcessControlMessageInternal(ref message2);
					}
				}
				else
				{
					XplatUI.TranslateMessage(ref msg);
					XplatUI.DispatchMessage(ref msg);
				}
			}
			if (tracker.GrabControl.IsDisposed)
			{
				return true;
			}
			if (!flag)
			{
				XplatUI.PostQuitMessage(0);
			}
			if (menu.Wnd != null)
			{
				menu.Wnd.Dispose();
				menu.Wnd = null;
			}
			return true;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x00034D14 File Offset: 0x00032F14
		private void DeselectItem(MenuItem item)
		{
			if (item == null)
			{
				return;
			}
			item.Selected = false;
			if (item.IsPopup)
			{
				MenuTracker.HideSubPopups(item, this.TopMenu);
				foreach (object obj in item.MenuItems)
				{
					MenuItem menuItem = (MenuItem)obj;
					if (menuItem.Selected)
					{
						this.DeselectItem(menuItem);
					}
				}
			}
			item.Parent.InvalidateItem(item);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00034DA0 File Offset: 0x00032FA0
		private void SelectItem(Menu menu, MenuItem item, bool execute)
		{
			MenuItem selectedItem = this.CurrentMenu.SelectedItem;
			if (selectedItem != item.Parent)
			{
				this.DeselectItem(selectedItem);
				if (this.CurrentMenu != menu && selectedItem.Parent != item && selectedItem.Parent is MenuItem)
				{
					this.DeselectItem(selectedItem.Parent as MenuItem);
				}
			}
			if (this.CurrentMenu != menu)
			{
				this.CurrentMenu = menu;
			}
			item.Selected = true;
			menu.InvalidateItem(item);
			if ((this.CurrentMenu == this.TopMenu && execute) || (this.CurrentMenu != this.TopMenu && this.popup_active))
			{
				item.PerformSelect();
			}
			if (execute && (selectedItem == null || item != selectedItem.Parent))
			{
				this.ExecFocusedItem(menu, item);
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00034E5C File Offset: 0x0003305C
		private void ExecFocusedItem(Menu menu, MenuItem item)
		{
			if (item == null)
			{
				return;
			}
			if (!item.Enabled)
			{
				return;
			}
			if (item.IsPopup)
			{
				this.ShowSubPopup(menu, item);
				return;
			}
			this.Deactivate();
			item.PerformClick();
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00034E88 File Offset: 0x00033088
		private void ShowSubPopup(Menu menu, MenuItem item)
		{
			if (!item.Enabled)
			{
				return;
			}
			if (!this.popdown_menu || !item.VisibleItems)
			{
				item.PerformPopup();
			}
			if (!item.VisibleItems)
			{
				return;
			}
			if (item.Wnd != null)
			{
				item.Wnd.Dispose();
			}
			this.popup_active = true;
			PopUpWindow popUpWindow = new PopUpWindow(this.GrabControl, item);
			Point point;
			if (menu is MainMenu)
			{
				point = new Point(item.X, item.Y + item.Height - 2 - menu.Height);
			}
			else
			{
				point = new Point(item.X + item.Width - 3, item.Y - 3);
			}
			point = menu.Wnd.PointToScreen(point);
			popUpWindow.Location = point;
			item.Wnd = popUpWindow;
			popUpWindow.ShowWindow();
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x00034F50 File Offset: 0x00033150
		public static void HideSubPopups(Menu menu, Menu topmenu)
		{
			foreach (object obj in menu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				if (menuItem.IsPopup)
				{
					MenuTracker.HideSubPopups(menuItem, null);
				}
			}
			if (menu.Wnd == null)
			{
				return;
			}
			PopUpWindow popUpWindow = menu.Wnd as PopUpWindow;
			if (popUpWindow != null)
			{
				popUpWindow.Hide();
				popUpWindow.Dispose();
			}
			menu.Wnd = null;
			if (topmenu != null && topmenu is MainMenu)
			{
				((MainMenu)topmenu).OnCollapse(EventArgs.Empty);
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00034FF8 File Offset: 0x000331F8
		private MenuItem FindSubItemByCoord(Menu menu, Point pnt)
		{
			foreach (object obj in menu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				if (menuItem.IsPopup && menuItem.Wnd != null && menuItem.Wnd.Visible && menuItem == menu.SelectedItem)
				{
					MenuItem menuItem2 = this.FindSubItemByCoord(menuItem, pnt);
					if (menuItem2 != null)
					{
						return menuItem2;
					}
				}
				if (menu.Wnd != null && menu.Wnd.Visible)
				{
					Rectangle bounds = menuItem.bounds;
					Point point = menu.Wnd.PointToScreen(new Point(menuItem.X, menuItem.Y));
					bounds.X = point.X;
					bounds.Y = point.Y;
					if (bounds.Contains(pnt))
					{
						return menuItem;
					}
				}
			}
			return null;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000350F8 File Offset: 0x000332F8
		private static MenuItem FindItemByKey(Menu menu, IntPtr key)
		{
			char c = char.ToUpper((char)(key.ToInt32() & 255));
			foreach (object obj in menu.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				if (menuItem.Mnemonic == c)
				{
					return menuItem;
				}
			}
			string text = c.ToString();
			foreach (object obj2 in menu.MenuItems)
			{
				MenuItem menuItem2 = (MenuItem)obj2;
				if (menuItem2.Text.StartsWith(text))
				{
					return menuItem2;
				}
			}
			return null;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x000351D8 File Offset: 0x000333D8
		private static MenuItem GetNextItem(Menu menu, MenuTracker.ItemNavigation navigation)
		{
			int i = 0;
			bool flag = false;
			for (int j = 0; j < menu.MenuItems.Count; j++)
			{
				MenuItem menuItem = menu.MenuItems[j];
				if (!menuItem.Separator && menuItem.Visible)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return null;
			}
			switch (navigation)
			{
			case MenuTracker.ItemNavigation.First:
				for (i = 0; i < menu.MenuItems.Count; i++)
				{
					MenuItem menuItem = menu.MenuItems[i];
					if (!menuItem.Separator && menuItem.Visible)
					{
						break;
					}
				}
				break;
			case MenuTracker.ItemNavigation.Next:
				i = ((menu.SelectedItem == null) ? (-1) : menu.SelectedItem.Index);
				for (i++; i < menu.MenuItems.Count; i++)
				{
					MenuItem menuItem = menu.MenuItems[i];
					if (!menuItem.Separator && menuItem.Visible)
					{
						break;
					}
				}
				if (i >= menu.MenuItems.Count)
				{
					for (i = 0; i < menu.MenuItems.Count; i++)
					{
						MenuItem menuItem = menu.MenuItems[i];
						if (!menuItem.Separator && menuItem.Visible)
						{
							break;
						}
					}
				}
				break;
			case MenuTracker.ItemNavigation.Previous:
				if (menu.SelectedItem != null)
				{
					i = menu.SelectedItem.Index;
				}
				for (i--; i >= 0; i--)
				{
					MenuItem menuItem = menu.MenuItems[i];
					if (!menuItem.Separator && menuItem.Visible)
					{
						break;
					}
				}
				if (i < 0)
				{
					for (i = menu.MenuItems.Count - 1; i >= 0; i--)
					{
						MenuItem menuItem = menu.MenuItems[i];
						if (!menuItem.Separator && menuItem.Visible)
						{
							break;
						}
					}
				}
				break;
			}
			return menu.MenuItems[i];
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00035394 File Offset: 0x00033594
		private void ProcessMenuKey(Msg msg_type)
		{
			if (this.TopMenu.MenuItems.Count == 0)
			{
				return;
			}
			MainMenu mainMenu = this.TopMenu as MainMenu;
			if (msg_type != Msg.WM_SYSKEYDOWN)
			{
				if (msg_type != Msg.WM_SYSKEYUP)
				{
					return;
				}
				switch (this.keynav_state)
				{
				case MenuTracker.KeyNavState.Idle:
				case MenuTracker.KeyNavState.Navigating:
					return;
				case MenuTracker.KeyNavState.Startup:
					this.keynav_state = MenuTracker.KeyNavState.NoPopups;
					this.SelectItem(this.TopMenu, this.TopMenu.MenuItems[0], false);
					return;
				}
				this.Deactivate();
				mainMenu.Draw();
			}
			else
			{
				MenuTracker.KeyNavState keyNavState = this.keynav_state;
				if (keyNavState == MenuTracker.KeyNavState.Idle)
				{
					this.keynav_state = MenuTracker.KeyNavState.Startup;
					this.hotkey_active = true;
					this.GrabControl.ActiveTracker = this;
					this.CurrentMenu = this.TopMenu;
					mainMenu.Draw();
					return;
				}
				if (keyNavState != MenuTracker.KeyNavState.Startup)
				{
					this.Deactivate();
					mainMenu.Draw();
					return;
				}
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00035470 File Offset: 0x00033670
		private bool ProcessMnemonic(Message msg, Keys key_data)
		{
			this.keynav_state = MenuTracker.KeyNavState.Navigating;
			MenuItem menuItem = MenuTracker.FindItemByKey(this.CurrentMenu, msg.WParam);
			if (menuItem == null || this.GrabControl == null || this.GrabControl.ActiveTracker == null)
			{
				return false;
			}
			this.active = true;
			this.GrabControl.ActiveTracker = this;
			this.SelectItem(this.CurrentMenu, menuItem, true);
			if (menuItem.IsPopup)
			{
				this.CurrentMenu = menuItem;
				this.SelectItem(menuItem, menuItem.MenuItems[0], false);
			}
			return true;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x000354F8 File Offset: 0x000336F8
		public void AddShortcuts(MenuItem item)
		{
			foreach (object obj in item.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				this.AddShortcuts(menuItem);
				if (menuItem.Shortcut != Shortcut.None)
				{
					this.shortcuts[(int)menuItem.Shortcut] = menuItem;
				}
			}
			if (item.Shortcut != Shortcut.None)
			{
				this.shortcuts[(int)item.Shortcut] = item;
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00035590 File Offset: 0x00033790
		public void RemoveShortcuts(MenuItem item)
		{
			foreach (object obj in item.MenuItems)
			{
				MenuItem menuItem = (MenuItem)obj;
				this.RemoveShortcuts(menuItem);
				if (menuItem.Shortcut != Shortcut.None)
				{
					this.shortcuts.Remove((int)menuItem.Shortcut);
				}
			}
			if (item.Shortcut != Shortcut.None)
			{
				this.shortcuts.Remove((int)item.Shortcut);
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00035628 File Offset: 0x00033828
		private bool ProcessShortcut(Keys keyData)
		{
			MenuItem menuItem = this.shortcuts[(int)keyData] as MenuItem;
			if (menuItem == null || !menuItem.Enabled)
			{
				return false;
			}
			if (this.active)
			{
				this.Deactivate();
			}
			menuItem.PerformClick();
			return true;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00035670 File Offset: 0x00033870
		public bool ProcessKeys(ref Message msg, Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt && this.active)
			{
				this.Deactivate();
				return false;
			}
			if ((keyData & Keys.Alt) == Keys.Alt && (keyData & Keys.F4) == Keys.F4)
			{
				if (this.GrabControl != null)
				{
					this.GrabControl.ActiveTracker = null;
				}
				return false;
			}
			if (msg.Msg != 261 && this.ProcessShortcut(keyData))
			{
				return true;
			}
			if ((keyData & Keys.KeyCode) == Keys.Menu && this.TopMenu is MainMenu)
			{
				this.ProcessMenuKey((Msg)msg.Msg);
				return true;
			}
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return this.ProcessMnemonic(msg, keyData);
			}
			if (msg.Msg == 261)
			{
				return false;
			}
			if (!this.Navigating)
			{
				return false;
			}
			if (keyData != Keys.Return)
			{
				if (keyData != Keys.Escape)
				{
					switch (keyData)
					{
					case Keys.Left:
						if (this.CurrentMenu is MainMenu)
						{
							MenuItem menuItem = MenuTracker.GetNextItem(this.CurrentMenu, MenuTracker.ItemNavigation.Previous);
							bool flag = menuItem.IsPopup && this.keynav_state != MenuTracker.KeyNavState.NoPopups;
							this.SelectItem(this.CurrentMenu, menuItem, flag);
							if (flag)
							{
								this.SelectItem(menuItem, menuItem.MenuItems[0], false);
								this.CurrentMenu = menuItem;
							}
						}
						else if (this.CurrentMenu.parent_menu is MainMenu)
						{
							MenuItem menuItem = MenuTracker.GetNextItem(this.CurrentMenu.parent_menu, MenuTracker.ItemNavigation.Previous);
							this.SelectItem(this.CurrentMenu.parent_menu, menuItem, menuItem.IsPopup);
							if (menuItem.IsPopup)
							{
								this.SelectItem(menuItem, menuItem.MenuItems[0], false);
								this.CurrentMenu = menuItem;
							}
						}
						else if (!(this.CurrentMenu is ContextMenu))
						{
							MenuTracker.HideSubPopups(this.CurrentMenu, this.TopMenu);
							if (this.CurrentMenu.parent_menu != null)
							{
								this.CurrentMenu = this.CurrentMenu.parent_menu;
							}
						}
						break;
					case Keys.Up:
					{
						if (this.CurrentMenu is MainMenu)
						{
							return true;
						}
						if (this.CurrentMenu.MenuItems.Count == 1 && this.CurrentMenu.parent_menu == this.TopMenu)
						{
							this.DeselectItem(this.CurrentMenu.SelectedItem);
							this.CurrentMenu = this.TopMenu;
							return true;
						}
						MenuItem menuItem = MenuTracker.GetNextItem(this.CurrentMenu, MenuTracker.ItemNavigation.Previous);
						if (menuItem != null)
						{
							this.SelectItem(this.CurrentMenu, menuItem, false);
						}
						break;
					}
					case Keys.Right:
						if (this.CurrentMenu is MainMenu)
						{
							MenuItem menuItem = MenuTracker.GetNextItem(this.CurrentMenu, MenuTracker.ItemNavigation.Next);
							bool flag2 = menuItem.IsPopup && this.keynav_state != MenuTracker.KeyNavState.NoPopups;
							this.SelectItem(this.CurrentMenu, menuItem, flag2);
							if (flag2)
							{
								this.SelectItem(menuItem, menuItem.MenuItems[0], false);
								this.CurrentMenu = menuItem;
							}
						}
						else if (this.CurrentMenu.SelectedItem != null && this.CurrentMenu.SelectedItem.IsPopup)
						{
							MenuItem menuItem = this.CurrentMenu.SelectedItem;
							this.ShowSubPopup(this.CurrentMenu, menuItem);
							this.SelectItem(menuItem, menuItem.MenuItems[0], false);
							this.CurrentMenu = menuItem;
						}
						else
						{
							Menu menu = this.CurrentMenu.parent_menu;
							while (menu != null && !(menu is MainMenu))
							{
								menu = menu.parent_menu;
							}
							if (menu is MainMenu)
							{
								MenuItem menuItem = MenuTracker.GetNextItem(menu, MenuTracker.ItemNavigation.Next);
								this.SelectItem(menu, menuItem, menuItem.IsPopup);
								if (menuItem.IsPopup)
								{
									this.SelectItem(menuItem, menuItem.MenuItems[0], false);
									this.CurrentMenu = menuItem;
								}
							}
						}
						break;
					case Keys.Down:
					{
						MenuItem menuItem;
						if (this.CurrentMenu is MainMenu)
						{
							if (this.CurrentMenu.SelectedItem != null && this.CurrentMenu.SelectedItem.IsPopup)
							{
								this.keynav_state = MenuTracker.KeyNavState.Navigating;
								menuItem = this.CurrentMenu.SelectedItem;
								this.ShowSubPopup(this.CurrentMenu, menuItem);
								this.SelectItem(menuItem, menuItem.MenuItems[0], false);
								this.CurrentMenu = menuItem;
								this.active = true;
								this.GrabControl.ActiveTracker = this;
							}
							return true;
						}
						menuItem = MenuTracker.GetNextItem(this.CurrentMenu, MenuTracker.ItemNavigation.Next);
						if (menuItem != null)
						{
							this.SelectItem(this.CurrentMenu, menuItem, false);
						}
						break;
					}
					default:
						this.ProcessMnemonic(msg, keyData);
						break;
					}
				}
				else
				{
					this.Deactivate();
				}
			}
			else if (this.CurrentMenu.SelectedItem != null && this.CurrentMenu.SelectedItem.IsPopup)
			{
				this.keynav_state = MenuTracker.KeyNavState.Navigating;
				MenuItem menuItem = this.CurrentMenu.SelectedItem;
				this.ShowSubPopup(this.CurrentMenu, menuItem);
				this.SelectItem(menuItem, menuItem.MenuItems[0], false);
				this.CurrentMenu = menuItem;
				this.active = true;
				this.GrabControl.ActiveTracker = this;
			}
			else
			{
				this.ExecFocusedItem(this.CurrentMenu, this.CurrentMenu.SelectedItem);
			}
			return this.active;
		}

		// Token: 0x040007AE RID: 1966
		internal bool active;

		// Token: 0x040007AF RID: 1967
		internal bool popup_active;

		// Token: 0x040007B0 RID: 1968
		internal bool popdown_menu;

		// Token: 0x040007B1 RID: 1969
		internal bool hotkey_active;

		// Token: 0x040007B2 RID: 1970
		private bool mouse_down;

		// Token: 0x040007B3 RID: 1971
		public Menu CurrentMenu;

		// Token: 0x040007B4 RID: 1972
		public Menu TopMenu;

		// Token: 0x040007B5 RID: 1973
		public Control GrabControl;

		// Token: 0x040007B6 RID: 1974
		private Point last_motion = Point.Empty;

		// Token: 0x040007B7 RID: 1975
		private MenuTracker.KeyNavState keynav_state;

		// Token: 0x040007B8 RID: 1976
		private Hashtable shortcuts = new Hashtable();

		// Token: 0x02000133 RID: 307
		private enum KeyNavState
		{
			// Token: 0x040007BA RID: 1978
			Idle,
			// Token: 0x040007BB RID: 1979
			Startup,
			// Token: 0x040007BC RID: 1980
			NoPopups,
			// Token: 0x040007BD RID: 1981
			Navigating
		}

		// Token: 0x02000134 RID: 308
		private enum ItemNavigation
		{
			// Token: 0x040007BF RID: 1983
			First,
			// Token: 0x040007C0 RID: 1984
			Last,
			// Token: 0x040007C1 RID: 1985
			Next,
			// Token: 0x040007C2 RID: 1986
			Previous
		}
	}
}
