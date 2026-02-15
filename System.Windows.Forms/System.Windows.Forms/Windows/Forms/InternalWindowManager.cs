using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020000DE RID: 222
	internal abstract class InternalWindowManager
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x000230DF File Offset: 0x000212DF
		public InternalWindowManager(Form form)
		{
			this.form = form;
			form.SizeChanged += this.FormSizeChangedHandler;
			this.title_buttons = new TitleButtons(form);
			ThemeEngine.Current.ManagedWindowSetButtonLocations(this);
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00023117 File Offset: 0x00021317
		public Form Form
		{
			get
			{
				return this.form;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0002311F File Offset: 0x0002131F
		public int IconWidth
		{
			get
			{
				return this.TitleBarHeight - 5;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00023129 File Offset: 0x00021329
		public TitleButtons TitleButtons
		{
			get
			{
				return this.title_buttons;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x00023131 File Offset: 0x00021331
		// (set) Token: 0x0600081F RID: 2079 RVA: 0x00023139 File Offset: 0x00021339
		internal Rectangle NormalBounds
		{
			get
			{
				return this.normal_bounds;
			}
			set
			{
				this.normal_bounds = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x00023142 File Offset: 0x00021342
		internal Size IconicSize
		{
			get
			{
				return SystemInformation.MinimizedWindowSize;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0002314C File Offset: 0x0002134C
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x000231A4 File Offset: 0x000213A4
		internal Rectangle IconicBounds
		{
			get
			{
				if (this.iconic_bounds == Rectangle.Empty)
				{
					return Rectangle.Empty;
				}
				Rectangle rectangle = this.iconic_bounds;
				rectangle.Y = this.Form.Parent.ClientRectangle.Bottom - this.iconic_bounds.Y;
				return rectangle;
			}
			set
			{
				this.iconic_bounds = value;
				this.iconic_bounds.Y = this.Form.Parent.ClientRectangle.Bottom - this.iconic_bounds.Y;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x000231E7 File Offset: 0x000213E7
		internal virtual Rectangle MaximizedBounds
		{
			get
			{
				return this.Form.Parent.ClientRectangle;
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000231FC File Offset: 0x000213FC
		public virtual void UpdateWindowState(FormWindowState old_window_state, FormWindowState new_window_state, bool force)
		{
			if (old_window_state == FormWindowState.Normal)
			{
				this.NormalBounds = this.form.Bounds;
			}
			else if (old_window_state == FormWindowState.Minimized)
			{
				this.IconicBounds = this.form.Bounds;
			}
			switch (new_window_state)
			{
			case FormWindowState.Normal:
				this.form.Bounds = this.NormalBounds;
				break;
			case FormWindowState.Minimized:
				if (this.IconicBounds == Rectangle.Empty)
				{
					Size iconicSize = this.IconicSize;
					Point point = new Point(0, this.Form.Parent.ClientSize.Height - iconicSize.Height);
					this.IconicBounds = new Rectangle(point, iconicSize);
				}
				this.form.Bounds = this.IconicBounds;
				break;
			case FormWindowState.Maximized:
				this.form.Bounds = this.MaximizedBounds;
				break;
			}
			this.UpdateWindowDecorations(new_window_state);
			this.form.ResetCursor();
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x000232E2 File Offset: 0x000214E2
		public virtual void UpdateWindowDecorations(FormWindowState window_state)
		{
			ThemeEngine.Current.ManagedWindowSetButtonLocations(this);
			if (this.form.IsHandleCreated)
			{
				XplatUI.RequestNCRecalc(this.form.Handle);
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x0002330C File Offset: 0x0002150C
		public virtual bool WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg <= Msg.WM_RBUTTONDOWN)
			{
				switch (msg)
				{
				case Msg.WM_NCCALCSIZE:
					return this.HandleNCCalcSize(ref m);
				case Msg.WM_NCHITTEST:
					return this.HandleNCHitTest(ref m);
				case Msg.WM_NCPAINT:
					return this.HandleNCPaint(ref m);
				default:
					switch (msg)
					{
					case Msg.WM_NCMOUSEMOVE:
						this.HandleNCMouseMove(ref m);
						return true;
					case Msg.WM_NCLBUTTONDOWN:
						this.HandleNCLButtonDown(ref m);
						return true;
					case Msg.WM_NCLBUTTONUP:
						this.HandleNCLButtonUp(ref m);
						return true;
					case Msg.WM_NCLBUTTONDBLCLK:
						this.HandleNCLButtonDblClick(ref m);
						break;
					default:
						switch (msg)
						{
						case Msg.WM_MOUSEMOVE:
							return this.HandleMouseMove(this.form, ref m);
						case Msg.WM_LBUTTONDOWN:
							return this.HandleLButtonDown(ref m);
						case Msg.WM_LBUTTONUP:
							this.HandleLButtonUp(ref m);
							break;
						case Msg.WM_LBUTTONDBLCLK:
							return this.HandleLButtonDblClick(ref m);
						case Msg.WM_RBUTTONDOWN:
							return this.HandleRButtonDown(ref m);
						}
						break;
					}
					break;
				}
			}
			else if (msg != Msg.WM_PARENTNOTIFY)
			{
				if (msg != Msg.WM_NCMOUSELEAVE)
				{
					if (msg == Msg.WM_MOUSELEAVE)
					{
						this.HandleMouseLeave(ref m);
					}
				}
				else
				{
					this.HandleNCMouseLeave(ref m);
				}
			}
			else if (Control.LowOrder(m.WParam.ToInt32()) == 513)
			{
				this.Activate();
			}
			return false;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x0002344C File Offset: 0x0002164C
		protected virtual bool HandleNCPaint(ref Message m)
		{
			PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref m, this.form.Handle, false);
			if (this.form.ActiveMenu != null)
			{
				Point menuOrigin = this.GetMenuOrigin();
				Rectangle rectangle = new Rectangle(menuOrigin.X, menuOrigin.Y, this.form.ClientSize.Width, 0);
				rectangle = Rectangle.Union(rectangle, paintEventArgs.ClipRectangle);
				paintEventArgs.SetClip(rectangle);
				paintEventArgs.Graphics.SetClip(rectangle);
				this.form.ActiveMenu.Draw(paintEventArgs, new Rectangle(menuOrigin.X, menuOrigin.Y, this.form.ClientSize.Width, 0));
			}
			if (this.HasBorders || (this.IsMinimized && (!this.Form.IsMdiChild || !this.IsMaximized)))
			{
				Rectangle rectangle = new Rectangle(0, 0, this.form.Width, this.form.Height);
				ThemeEngine.Current.DrawManagedWindowDecorations(paintEventArgs.Graphics, rectangle, this);
			}
			XplatUI.PaintEventEnd(ref m, this.form.Handle, false);
			return true;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00023570 File Offset: 0x00021770
		protected virtual bool HandleNCCalcSize(ref Message m)
		{
			if (m.WParam == (IntPtr)1)
			{
				XplatUIWin32.NCCALCSIZE_PARAMS nccalcsize_PARAMS = (XplatUIWin32.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(XplatUIWin32.NCCALCSIZE_PARAMS));
				nccalcsize_PARAMS.rgrc1 = this.NCCalcSize(nccalcsize_PARAMS.rgrc1);
				Marshal.StructureToPtr<XplatUIWin32.NCCALCSIZE_PARAMS>(nccalcsize_PARAMS, m.LParam, true);
			}
			else
			{
				XplatUIWin32.RECT rect = (XplatUIWin32.RECT)Marshal.PtrToStructure(m.LParam, typeof(XplatUIWin32.RECT));
				rect = this.NCCalcSize(rect);
				Marshal.StructureToPtr<XplatUIWin32.RECT>(rect, m.LParam, true);
			}
			return true;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00023600 File Offset: 0x00021800
		protected virtual XplatUIWin32.RECT NCCalcSize(XplatUIWin32.RECT proposed_window_rect)
		{
			int num = ThemeEngine.Current.ManagedWindowBorderWidth(this);
			if (this.HasBorders)
			{
				proposed_window_rect.top += this.TitleBarHeight + num;
				proposed_window_rect.bottom -= num;
				proposed_window_rect.left += num;
				proposed_window_rect.right -= num;
			}
			if (XplatUI.RequiresPositiveClientAreaSize)
			{
				if (proposed_window_rect.right <= proposed_window_rect.left)
				{
					proposed_window_rect.right += proposed_window_rect.left - proposed_window_rect.right + 1;
				}
				if (proposed_window_rect.top >= proposed_window_rect.bottom)
				{
					proposed_window_rect.bottom += proposed_window_rect.top - proposed_window_rect.bottom + 1;
				}
			}
			return proposed_window_rect;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000236B0 File Offset: 0x000218B0
		protected virtual bool HandleNCHitTest(ref Message m)
		{
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCPointToClient(ref num, ref num2);
			InternalWindowManager.FormPos formPos = this.FormPosForCoords(num, num2);
			if (formPos == InternalWindowManager.FormPos.TitleBar)
			{
				m.Result = new IntPtr(2);
				return true;
			}
			if (!this.IsSizable)
			{
				return false;
			}
			if (formPos <= InternalWindowManager.FormPos.Bottom)
			{
				switch (formPos)
				{
				case InternalWindowManager.FormPos.Top:
					m.Result = new IntPtr(12);
					return true;
				case InternalWindowManager.FormPos.TitleBar | InternalWindowManager.FormPos.Top:
				case InternalWindowManager.FormPos.TitleBar | InternalWindowManager.FormPos.Left:
				case InternalWindowManager.FormPos.TitleBar | InternalWindowManager.FormPos.Top | InternalWindowManager.FormPos.Left:
				case InternalWindowManager.FormPos.TitleBar | InternalWindowManager.FormPos.Right:
					break;
				case InternalWindowManager.FormPos.Left:
					m.Result = new IntPtr(10);
					return true;
				case InternalWindowManager.FormPos.TopLeft:
					m.Result = new IntPtr(13);
					return true;
				case InternalWindowManager.FormPos.Right:
					m.Result = new IntPtr(11);
					return true;
				case InternalWindowManager.FormPos.TopRight:
					m.Result = new IntPtr(14);
					return true;
				default:
					if (formPos == InternalWindowManager.FormPos.Bottom)
					{
						m.Result = new IntPtr(15);
						return true;
					}
					break;
				}
			}
			else
			{
				if (formPos == InternalWindowManager.FormPos.BottomLeft)
				{
					m.Result = new IntPtr(16);
					return true;
				}
				if (formPos == InternalWindowManager.FormPos.BottomRight)
				{
					m.Result = new IntPtr(17);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000237D7 File Offset: 0x000219D7
		public virtual void UpdateBorderStyle(FormBorderStyle border_style)
		{
			if (this.form.IsHandleCreated)
			{
				XplatUI.SetBorderStyle(this.form.Handle, border_style);
			}
			if (this.ShouldRemoveWindowManager(border_style))
			{
				this.form.RemoveWindowManager();
				return;
			}
			ThemeEngine.Current.ManagedWindowSetButtonLocations(this);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00023817 File Offset: 0x00021A17
		public virtual void SetWindowState(FormWindowState old_state, FormWindowState window_state)
		{
			this.UpdateWindowState(old_state, window_state, false);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00023822 File Offset: 0x00021A22
		public virtual FormWindowState GetWindowState()
		{
			return this.form.window_state;
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00023830 File Offset: 0x00021A30
		public virtual void PointToClient(ref int x, ref int y)
		{
			Rectangle workingArea = SystemInformation.WorkingArea;
			if (x > workingArea.Right)
			{
				x = workingArea.Right;
			}
			if (x < workingArea.Left)
			{
				x = workingArea.Left;
			}
			if (y < workingArea.Top)
			{
				y = workingArea.Top;
			}
			if (y > workingArea.Bottom)
			{
				y = workingArea.Bottom;
			}
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00023893 File Offset: 0x00021A93
		public virtual void PointToScreen(ref int x, ref int y)
		{
			XplatUI.ClientToScreen(this.form.Handle, ref x, ref y);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x000238A7 File Offset: 0x00021AA7
		protected virtual bool ShouldRemoveWindowManager(FormBorderStyle style)
		{
			return style != FormBorderStyle.FixedToolWindow && style != FormBorderStyle.SizableToolWindow;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000238B8 File Offset: 0x00021AB8
		public bool IconRectangleContains(int x, int y)
		{
			return this.ShowIcon && ThemeEngine.Current.ManagedWindowGetTitleBarIconArea(this).Contains(x, y);
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x000238E4 File Offset: 0x00021AE4
		public bool ShowIcon
		{
			get
			{
				return this.Form.ShowIcon && this.HasBorders && (this.IsMinimized || (!this.IsToolWindow && this.Form.FormBorderStyle != FormBorderStyle.FixedDialog));
			}
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00023922 File Offset: 0x00021B22
		protected virtual void Activate()
		{
			this.form.Invalidate(true);
			this.form.Update();
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00006F54 File Offset: 0x00005154
		public virtual bool IsActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0002393B File Offset: 0x00021B3B
		private void FormSizeChangedHandler(object sender, EventArgs e)
		{
			if (this.form.IsHandleCreated)
			{
				ThemeEngine.Current.ManagedWindowSetButtonLocations(this);
				XplatUI.InvalidateNC(this.form.Handle);
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00023965 File Offset: 0x00021B65
		protected virtual bool HandleRButtonDown(ref Message m)
		{
			this.Activate();
			return false;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00023965 File Offset: 0x00021B65
		protected virtual bool HandleLButtonDown(ref Message m)
		{
			this.Activate();
			return false;
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00002D70 File Offset: 0x00000F70
		protected virtual bool HandleLButtonDblClick(ref Message m)
		{
			return false;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00023970 File Offset: 0x00021B70
		protected virtual bool HandleNCMouseLeave(ref Message m)
		{
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCPointToClient(ref num, ref num2);
			if (this.FormPosForCoords(num, num2) != InternalWindowManager.FormPos.TitleBar)
			{
				this.HandleTitleBarLeave(num, num2);
				return true;
			}
			return true;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x000239C8 File Offset: 0x00021BC8
		protected virtual bool HandleNCMouseMove(ref Message m)
		{
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCPointToClient(ref num, ref num2);
			if (this.FormPosForCoords(num, num2) == InternalWindowManager.FormPos.TitleBar)
			{
				this.HandleTitleBarMouseMove(num, num2);
				return true;
			}
			if (this.form.ActiveMenu != null && XplatUI.IsEnabled(this.form.Handle))
			{
				MouseEventArgs mouseEventArgs = new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.form.mouse_clicks, num, num2, 0);
				this.form.ActiveMenu.OnMouseMove(this.form, mouseEventArgs);
			}
			return true;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00023A7C File Offset: 0x00021C7C
		protected virtual bool HandleNCLButtonDown(ref Message m)
		{
			this.Activate();
			this.start = Cursor.Position;
			this.virtual_position = this.form.Bounds;
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCPointToClient(ref num, ref num2);
			InternalWindowManager.FormPos formPos = this.FormPosForCoords(num, num2);
			if (this.form.ActiveMenu != null && XplatUI.IsEnabled(this.form.Handle))
			{
				MouseEventArgs mouseEventArgs = new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.form.mouse_clicks, num, num2 - this.TitleBarHeight, 0);
				this.form.ActiveMenu.OnMouseDown(this.form, mouseEventArgs);
			}
			if (formPos == InternalWindowManager.FormPos.TitleBar)
			{
				this.HandleTitleBarDown(num, num2);
				return true;
			}
			if (!this.IsSizable)
			{
				return false;
			}
			if ((formPos & InternalWindowManager.FormPos.AnyEdge) == InternalWindowManager.FormPos.None)
			{
				return false;
			}
			this.virtual_position = this.form.Bounds;
			this.state = InternalWindowManager.State.Sizing;
			this.sizing_edge = formPos;
			this.form.Capture = true;
			return true;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00023B9C File Offset: 0x00021D9C
		protected virtual void HandleNCLButtonDblClick(ref Message m)
		{
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCPointToClient(ref num, ref num2);
			InternalWindowManager.FormPos formPos = this.FormPosForCoords(num, num2);
			if (formPos == InternalWindowManager.FormPos.TitleBar || formPos == InternalWindowManager.FormPos.Top)
			{
				this.HandleTitleBarDoubleClick(num, num2);
			}
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0000493C File Offset: 0x00002B3C
		protected virtual void HandleTitleBarDoubleClick(int x, int y)
		{
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00023BF5 File Offset: 0x00021DF5
		protected virtual void HandleTitleBarLeave(int x, int y)
		{
			this.title_buttons.MouseLeave(x, y);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00023C04 File Offset: 0x00021E04
		protected virtual void HandleTitleBarMouseMove(int x, int y)
		{
			if (this.title_buttons.MouseMove(x, y))
			{
				XplatUI.InvalidateNC(this.form.Handle);
			}
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x00023C25 File Offset: 0x00021E25
		protected virtual void HandleTitleBarUp(int x, int y)
		{
			this.title_buttons.MouseUp(x, y);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00023C34 File Offset: 0x00021E34
		protected virtual void HandleTitleBarDown(int x, int y)
		{
			this.title_buttons.MouseDown(x, y);
			if (!this.TitleButtons.AnyPushedTitleButtons && !this.IsMaximized)
			{
				this.state = InternalWindowManager.State.Moving;
				this.clicked_point = new Point(x, y);
				if (this.form.Parent != null)
				{
					this.form.CaptureWithConfine(this.form.Parent);
				}
				else
				{
					this.form.Capture = true;
				}
			}
			XplatUI.InvalidateNC(this.form.Handle);
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x00023CB8 File Offset: 0x00021EB8
		private bool HandleMouseMove(Form form, ref Message m)
		{
			InternalWindowManager.State state = this.state;
			if (state == InternalWindowManager.State.Moving)
			{
				this.HandleWindowMove(m);
				return true;
			}
			if (state != InternalWindowManager.State.Sizing)
			{
				return false;
			}
			this.HandleSizing(m);
			return true;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x00023CF3 File Offset: 0x00021EF3
		private void HandleMouseLeave(ref Message m)
		{
			this.form.ResetCursor();
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00023D00 File Offset: 0x00021F00
		protected virtual void HandleWindowMove(Message m)
		{
			Point point = this.MouseMove(Cursor.Position);
			this.UpdateVP(this.virtual_position.X + point.X, this.virtual_position.Y + point.Y, this.virtual_position.Width, this.virtual_position.Height);
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00023D5C File Offset: 0x00021F5C
		private void HandleSizing(Message m)
		{
			Rectangle rectangle = this.virtual_position;
			int num;
			int num2;
			if (this.IsToolWindow)
			{
				int borderWidth = this.BorderWidth;
				num = 2 * (borderWidth + 2) + ThemeEngine.Current.ManagedWindowButtonSize(this).Width;
				num2 = 2 * borderWidth + this.TitleBarHeight;
			}
			else
			{
				Size minWindowTrackSize = SystemInformation.MinWindowTrackSize;
				num = minWindowTrackSize.Width;
				num2 = minWindowTrackSize.Height;
			}
			int num3 = Cursor.Position.X;
			int num4 = Cursor.Position.Y;
			this.PointToClient(ref num3, ref num4);
			if ((this.sizing_edge & InternalWindowManager.FormPos.Top) != InternalWindowManager.FormPos.None)
			{
				if (rectangle.Bottom - num4 < num2)
				{
					num4 = rectangle.Bottom - num2;
				}
				rectangle.Height = rectangle.Bottom - num4;
				rectangle.Y = num4;
			}
			else if ((this.sizing_edge & InternalWindowManager.FormPos.Bottom) != InternalWindowManager.FormPos.None)
			{
				int num5 = num4 - rectangle.Top;
				if (num5 <= num2)
				{
					num5 = num2;
				}
				rectangle.Height = num5;
			}
			if ((this.sizing_edge & InternalWindowManager.FormPos.Left) != InternalWindowManager.FormPos.None)
			{
				if (rectangle.Right - num3 < num)
				{
					num3 = rectangle.Right - num;
				}
				rectangle.Width = rectangle.Right - num3;
				rectangle.X = num3;
			}
			else if ((this.sizing_edge & InternalWindowManager.FormPos.Right) != InternalWindowManager.FormPos.None)
			{
				int num6 = num3 - this.form.Left;
				if (num6 <= num)
				{
					num6 = num;
				}
				rectangle.Width = num6;
			}
			this.UpdateVP(rectangle);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x00023EBB File Offset: 0x000220BB
		public bool IsMaximized
		{
			get
			{
				return this.GetWindowState() == FormWindowState.Maximized;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000847 RID: 2119 RVA: 0x00023EC6 File Offset: 0x000220C6
		public bool IsMinimized
		{
			get
			{
				return this.GetWindowState() == FormWindowState.Minimized;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00023ED4 File Offset: 0x000220D4
		public bool IsSizable
		{
			get
			{
				FormBorderStyle formBorderStyle = this.form.FormBorderStyle;
				return (formBorderStyle == FormBorderStyle.Sizable || formBorderStyle == FormBorderStyle.SizableToolWindow) && this.form.window_state != FormWindowState.Minimized;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00023F08 File Offset: 0x00022108
		public bool HasBorders
		{
			get
			{
				return this.form.FormBorderStyle > FormBorderStyle.None;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00023F18 File Offset: 0x00022118
		public bool IsToolWindow
		{
			get
			{
				return this.form.FormBorderStyle == FormBorderStyle.SizableToolWindow || this.form.FormBorderStyle == FormBorderStyle.FixedToolWindow || this.form.GetCreateParams().IsSet(WindowExStyles.WS_EX_TOOLWINDOW);
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00023F50 File Offset: 0x00022150
		public int TitleBarHeight
		{
			get
			{
				return ThemeEngine.Current.ManagedWindowTitleBarHeight(this);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00023F5D File Offset: 0x0002215D
		public int BorderWidth
		{
			get
			{
				return ThemeEngine.Current.ManagedWindowBorderWidth(this);
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00023F6A File Offset: 0x0002216A
		public virtual int MenuHeight
		{
			get
			{
				if (this.form.Menu == null)
				{
					return 0;
				}
				return ThemeEngine.Current.MenuHeight;
			}
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x00023F85 File Offset: 0x00022185
		protected void UpdateVP(Rectangle r)
		{
			this.UpdateVP(r.X, r.Y, r.Width, r.Height);
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x00023FA9 File Offset: 0x000221A9
		protected void UpdateVP(int x, int y, int w, int h)
		{
			this.virtual_position.X = x;
			this.virtual_position.Y = y;
			this.virtual_position.Width = w;
			this.virtual_position.Height = h;
			this.DrawVirtualPosition(this.virtual_position);
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00023FE8 File Offset: 0x000221E8
		protected virtual void HandleLButtonUp(ref Message m)
		{
			if (this.state == InternalWindowManager.State.Idle)
			{
				return;
			}
			this.ClearVirtualPosition();
			this.form.Capture = false;
			if (this.state == InternalWindowManager.State.Moving && this.form.Location != this.virtual_position.Location)
			{
				this.form.Location = this.virtual_position.Location;
			}
			else if (this.state == InternalWindowManager.State.Sizing && this.form.Bounds != this.virtual_position)
			{
				this.form.Bounds = this.virtual_position;
			}
			this.state = InternalWindowManager.State.Idle;
			this.OnWindowFinishedMoving();
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00024090 File Offset: 0x00022290
		private bool HandleNCLButtonUp(ref Message m)
		{
			if (this.form.Capture)
			{
				this.ClearVirtualPosition();
				this.form.Capture = false;
				this.state = InternalWindowManager.State.Idle;
				if (this.form.MdiContainer != null)
				{
					this.form.MdiContainer.SizeScrollBars();
				}
			}
			int num = Control.LowOrder(m.LParam.ToInt32());
			int num2 = Control.HighOrder((long)m.LParam.ToInt32());
			this.NCPointToClient(ref num, ref num2);
			if (this.FormPosForCoords(num, num2) == InternalWindowManager.FormPos.TitleBar)
			{
				this.HandleTitleBarUp(num, num2);
				return true;
			}
			return true;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00024129 File Offset: 0x00022329
		protected void DrawTitleButton(Graphics dc, TitleButton button, Rectangle clip)
		{
			if (!button.Rectangle.IntersectsWith(clip))
			{
				return;
			}
			ThemeEngine.Current.ManagedWindowDrawMenuButton(dc, button, clip, this);
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0000493C File Offset: 0x00002B3C
		public virtual void DrawMaximizedButtons(object sender, PaintEventArgs pe)
		{
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x00024148 File Offset: 0x00022348
		protected Point MouseMove(Point pos)
		{
			return new Point(pos.X - this.start.X, pos.Y - this.start.Y);
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00024175 File Offset: 0x00022375
		protected virtual void DrawVirtualPosition(Rectangle virtual_position)
		{
			this.form.Bounds = virtual_position;
			this.start = Cursor.Position;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0000493C File Offset: 0x00002B3C
		protected virtual void ClearVirtualPosition()
		{
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0000493C File Offset: 0x00002B3C
		protected virtual void OnWindowFinishedMoving()
		{
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x0002418E File Offset: 0x0002238E
		protected virtual void NCPointToClient(ref int x, ref int y)
		{
			this.form.PointToClient(ref x, ref y);
			this.NCClientToNC(ref x, ref y);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000241A5 File Offset: 0x000223A5
		protected virtual void NCClientToNC(ref int x, ref int y)
		{
			y += this.TitleBarHeight;
			y += this.BorderWidth;
			y += this.MenuHeight;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000241C8 File Offset: 0x000223C8
		internal Point GetMenuOrigin()
		{
			return new Point(this.BorderWidth, this.BorderWidth + this.TitleBarHeight);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000241E4 File Offset: 0x000223E4
		protected InternalWindowManager.FormPos FormPosForCoords(int x, int y)
		{
			int borderWidth = this.BorderWidth;
			if (y < this.TitleBarHeight + borderWidth)
			{
				if (y > borderWidth && x > borderWidth && x < this.form.Width - borderWidth)
				{
					return InternalWindowManager.FormPos.TitleBar;
				}
				if (x < borderWidth || (x < 20 && y < borderWidth))
				{
					return InternalWindowManager.FormPos.TopLeft;
				}
				if (x > this.form.Width - borderWidth || (x > this.form.Width - 20 && y < borderWidth))
				{
					return InternalWindowManager.FormPos.TopRight;
				}
				if (y < borderWidth)
				{
					return InternalWindowManager.FormPos.Top;
				}
			}
			else if (y > this.form.Height - 20)
			{
				if (x < borderWidth || (x < 20 && y > this.form.Height - borderWidth))
				{
					return InternalWindowManager.FormPos.BottomLeft;
				}
				if (x > this.form.Width - borderWidth * 2 || (x > this.form.Width - 20 && y > this.form.Height - borderWidth))
				{
					return InternalWindowManager.FormPos.BottomRight;
				}
				if (y > this.form.Height - borderWidth * 2)
				{
					return InternalWindowManager.FormPos.Bottom;
				}
			}
			else
			{
				if (x < borderWidth)
				{
					return InternalWindowManager.FormPos.Left;
				}
				if (x > this.form.Width - borderWidth * 2)
				{
					return InternalWindowManager.FormPos.Right;
				}
			}
			return InternalWindowManager.FormPos.None;
		}

		// Token: 0x04000530 RID: 1328
		private TitleButtons title_buttons;

		// Token: 0x04000531 RID: 1329
		internal Form form;

		// Token: 0x04000532 RID: 1330
		internal Point start;

		// Token: 0x04000533 RID: 1331
		internal InternalWindowManager.State state;

		// Token: 0x04000534 RID: 1332
		protected Point clicked_point;

		// Token: 0x04000535 RID: 1333
		private InternalWindowManager.FormPos sizing_edge;

		// Token: 0x04000536 RID: 1334
		internal Rectangle virtual_position;

		// Token: 0x04000537 RID: 1335
		private Rectangle normal_bounds;

		// Token: 0x04000538 RID: 1336
		private Rectangle iconic_bounds;

		// Token: 0x020000DF RID: 223
		public enum State
		{
			// Token: 0x0400053A RID: 1338
			Idle,
			// Token: 0x0400053B RID: 1339
			Moving,
			// Token: 0x0400053C RID: 1340
			Sizing
		}

		// Token: 0x020000E0 RID: 224
		[Flags]
		public enum FormPos
		{
			// Token: 0x0400053E RID: 1342
			None = 0,
			// Token: 0x0400053F RID: 1343
			TitleBar = 1,
			// Token: 0x04000540 RID: 1344
			Top = 2,
			// Token: 0x04000541 RID: 1345
			Left = 4,
			// Token: 0x04000542 RID: 1346
			Right = 8,
			// Token: 0x04000543 RID: 1347
			Bottom = 16,
			// Token: 0x04000544 RID: 1348
			TopLeft = 6,
			// Token: 0x04000545 RID: 1349
			TopRight = 10,
			// Token: 0x04000546 RID: 1350
			BottomLeft = 20,
			// Token: 0x04000547 RID: 1351
			BottomRight = 24,
			// Token: 0x04000548 RID: 1352
			AnyEdge = 30
		}
	}
}
