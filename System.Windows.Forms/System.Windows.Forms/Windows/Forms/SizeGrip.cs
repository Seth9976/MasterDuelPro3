using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x0200018A RID: 394
	internal class SizeGrip : Control
	{
		// Token: 0x06000ECB RID: 3787 RVA: 0x0004401B File Offset: 0x0004221B
		public SizeGrip(Control CapturedControl)
		{
			this.Cursor = Cursors.SizeNWSE;
			this.enabled = true;
			this.fill_background = true;
			base.Size = SizeGrip.GetDefaultSize();
			this.CapturedControl = CapturedControl;
		}

		// Token: 0x170003C7 RID: 967
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x0004404E File Offset: 0x0004224E
		public bool FillBackground
		{
			set
			{
				this.fill_background = value;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (set) Token: 0x06000ECD RID: 3789 RVA: 0x00044058 File Offset: 0x00042258
		public bool Virtual
		{
			set
			{
				if (this.is_virtual == value)
				{
					return;
				}
				this.is_virtual = value;
				if (this.is_virtual)
				{
					this.CapturedControl.MouseMove += this.HandleMouseMove;
					this.CapturedControl.MouseUp += this.HandleMouseUp;
					this.CapturedControl.MouseDown += this.HandleMouseDown;
					this.CapturedControl.EnabledChanged += this.HandleEnabledChanged;
					this.CapturedControl.Resize += this.HandleResize;
					return;
				}
				this.CapturedControl.MouseMove -= this.HandleMouseMove;
				this.CapturedControl.MouseUp -= this.HandleMouseUp;
				this.CapturedControl.MouseDown -= this.HandleMouseDown;
				this.CapturedControl.EnabledChanged -= this.HandleEnabledChanged;
				this.CapturedControl.Resize -= this.HandleResize;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00044165 File Offset: 0x00042365
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x0004416D File Offset: 0x0004236D
		public Control CapturedControl
		{
			get
			{
				return this.captured_control;
			}
			set
			{
				this.captured_control = value;
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00044176 File Offset: 0x00042376
		internal static Size GetDefaultSize()
		{
			return new Size(SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00044188 File Offset: 0x00042388
		internal static Rectangle GetDefaultRectangle(Control Parent)
		{
			Size defaultSize = SizeGrip.GetDefaultSize();
			return new Rectangle(Parent.ClientSize.Width - defaultSize.Width, Parent.ClientSize.Height - defaultSize.Height, defaultSize.Width, defaultSize.Height);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000441DA File Offset: 0x000423DA
		private void HandleResize(object sender, EventArgs e)
		{
			((Control)sender).Invalidate(this.last_painted_area);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000441F0 File Offset: 0x000423F0
		private void HandleEnabledChanged(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			this.enabled = control.Enabled;
			Cursor cursor;
			if (this.enabled)
			{
				cursor = Cursors.SizeNWSE;
			}
			else
			{
				cursor = Cursors.Default;
			}
			if (this.is_virtual)
			{
				if (this.CapturedControl != null)
				{
					this.CapturedControl.Cursor = cursor;
				}
			}
			else
			{
				this.Cursor = cursor;
			}
			control.Invalidate(SizeGrip.GetDefaultRectangle(control));
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00044258 File Offset: 0x00042458
		internal void HandlePaint(object sender, PaintEventArgs e)
		{
			if (base.Visible)
			{
				Control control = (Control)sender;
				Graphics graphics = e.Graphics;
				Rectangle defaultRectangle = SizeGrip.GetDefaultRectangle(control);
				if (!this.is_virtual || this.fill_background)
				{
					graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(ThemeEngine.Current.ColorControl), defaultRectangle);
				}
				if (this.enabled)
				{
					ControlPaint.DrawSizeGrip(graphics, this.BackColor, defaultRectangle);
				}
				this.last_painted_area = defaultRectangle;
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000442CC File Offset: 0x000424CC
		private void HandleMouseCaptureChanged(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			if (this.captured && !control.Capture)
			{
				this.captured = false;
				this.CapturedControl.Size = new Size(this.window_w, this.window_h);
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00044314 File Offset: 0x00042514
		internal void HandleMouseDown(object sender, MouseEventArgs e)
		{
			if (this.enabled)
			{
				Control control = (Control)sender;
				if (!SizeGrip.GetDefaultRectangle(control).Contains(e.X, e.Y))
				{
					return;
				}
				control.Capture = true;
				this.captured = true;
				this.capture_point = Control.MousePosition;
				this.window_w = this.CapturedControl.Width;
				this.window_h = this.CapturedControl.Height;
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00044388 File Offset: 0x00042588
		internal void HandleMouseMove(object sender, MouseEventArgs e)
		{
			Control control = (Control)sender;
			if (SizeGrip.GetDefaultRectangle(control).Contains(e.X, e.Y))
			{
				control.Cursor = Cursors.SizeNWSE;
			}
			else
			{
				control.Cursor = Cursors.Default;
			}
			if (this.captured)
			{
				Point mousePosition = Control.MousePosition;
				int num = mousePosition.X - this.capture_point.X;
				int num2 = mousePosition.Y - this.capture_point.Y;
				Control capturedControl = this.CapturedControl;
				Form form = capturedControl as Form;
				Size size = new Size(this.window_w + num, this.window_h + num2);
				Size size2 = ((form != null) ? form.MaximumSize : Size.Empty);
				Size size3 = ((form != null) ? form.MinimumSize : Size.Empty);
				if (size.Width > size2.Width && size2.Width > 0)
				{
					size.Width = size2.Width;
				}
				else if (size.Width < size3.Width)
				{
					size.Width = size3.Width;
				}
				if (size.Height > size2.Height && size2.Height > 0)
				{
					size.Height = size2.Height;
				}
				else if (size.Height < size3.Height)
				{
					size.Height = size3.Height;
				}
				if (size != capturedControl.Size)
				{
					capturedControl.Size = size;
				}
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00044504 File Offset: 0x00042704
		internal void HandleMouseUp(object sender, MouseEventArgs e)
		{
			if (this.captured)
			{
				Control control = (Control)sender;
				this.captured = false;
				control.Capture = false;
				control.Invalidate(this.last_painted_area);
				if (base.Parent is ScrollableControl)
				{
					((ScrollableControl)base.Parent).UpdateSizeGripVisible();
				}
				if (this.hide_pending)
				{
					base.Hide();
					this.hide_pending = false;
				}
			}
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0004456A File Offset: 0x0004276A
		protected override void SetVisibleCore(bool value)
		{
			if (!base.Capture)
			{
				base.SetVisibleCore(value);
				return;
			}
			if (!value)
			{
				this.hide_pending = true;
				return;
			}
			this.hide_pending = false;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0004458E File Offset: 0x0004278E
		protected override void OnPaint(PaintEventArgs pe)
		{
			this.HandlePaint(this, pe);
			base.OnPaint(pe);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0004459F File Offset: 0x0004279F
		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			base.OnMouseCaptureChanged(e);
			this.HandleMouseCaptureChanged(this, e);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000445B0 File Offset: 0x000427B0
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.HandleEnabledChanged(this, e);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000445C1 File Offset: 0x000427C1
		protected override void OnMouseDown(MouseEventArgs e)
		{
			this.HandleMouseDown(this, e);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x000445CB File Offset: 0x000427CB
		protected override void OnMouseMove(MouseEventArgs e)
		{
			this.HandleMouseMove(this, e);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000445D5 File Offset: 0x000427D5
		protected override void OnMouseUp(MouseEventArgs e)
		{
			this.HandleMouseUp(this, e);
		}

		// Token: 0x04000A1F RID: 2591
		private Point capture_point;

		// Token: 0x04000A20 RID: 2592
		private Control captured_control;

		// Token: 0x04000A21 RID: 2593
		private int window_w;

		// Token: 0x04000A22 RID: 2594
		private int window_h;

		// Token: 0x04000A23 RID: 2595
		private bool hide_pending;

		// Token: 0x04000A24 RID: 2596
		private bool captured;

		// Token: 0x04000A25 RID: 2597
		private bool is_virtual;

		// Token: 0x04000A26 RID: 2598
		private bool enabled;

		// Token: 0x04000A27 RID: 2599
		private bool fill_background;

		// Token: 0x04000A28 RID: 2600
		private Rectangle last_painted_area;
	}
}
