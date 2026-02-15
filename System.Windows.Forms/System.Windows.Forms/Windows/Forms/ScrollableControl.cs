using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Defines a base class for controls that support auto-scrolling behavior.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000183 RID: 387
	[Designer("System.Windows.Forms.Design.ScrollableControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class ScrollableControl : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollableControl" /> class.</summary>
		// Token: 0x06000E94 RID: 3732 RVA: 0x00042C60 File Offset: 0x00040E60
		public ScrollableControl()
		{
			base.SetStyle(ControlStyles.ContainerControl, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, false);
			this.auto_scroll = false;
			this.force_hscroll_visible = false;
			this.force_vscroll_visible = false;
			this.auto_scroll_margin = new Size(0, 0);
			this.auto_scroll_min_size = new Size(0, 0);
			this.scroll_position = new Point(0, 0);
			base.SizeChanged += this.Recalculate;
			base.VisibleChanged += this.VisibleChangedHandler;
			base.LocationChanged += this.LocationChangedHandler;
			base.ParentChanged += this.ParentChangedHandler;
			base.HandleCreated += this.AddScrollbars;
			this.CreateScrollbars();
			this.horizontalScroll = new HScrollProperties(this);
			this.verticalScroll = new VScrollProperties(this);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00042D3B File Offset: 0x00040F3B
		private void VisibleChangedHandler(object sender, EventArgs e)
		{
			this.Recalculate(false);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x00042D44 File Offset: 0x00040F44
		private void LocationChangedHandler(object sender, EventArgs e)
		{
			this.UpdateSizeGripVisible();
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00042D4C File Offset: 0x00040F4C
		private void ParentChangedHandler(object sender, EventArgs e)
		{
			if (this.old_parent == base.Parent)
			{
				return;
			}
			if (this.old_parent != null)
			{
				this.old_parent.SizeChanged -= this.Parent_SizeChanged;
				this.old_parent.PaddingChanged -= this.Parent_PaddingChanged;
			}
			if (base.Parent != null)
			{
				base.Parent.SizeChanged += this.Parent_SizeChanged;
				base.Parent.PaddingChanged += this.Parent_PaddingChanged;
			}
			this.old_parent = base.Parent;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x00042D44 File Offset: 0x00040F44
		private void Parent_PaddingChanged(object sender, EventArgs e)
		{
			this.UpdateSizeGripVisible();
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x00042D44 File Offset: 0x00040F44
		private void Parent_SizeChanged(object sender, EventArgs e)
		{
			this.UpdateSizeGripVisible();
		}

		/// <summary>Gets or sets a value indicating whether the container enables the user to scroll to any controls placed outside of its visible boundaries.</summary>
		/// <returns>true if the container enables auto-scrolling; otherwise, false. The default value is false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x00042DE0 File Offset: 0x00040FE0
		[DefaultValue(false)]
		[Localizable(true)]
		[MWFCategory("Layout")]
		public virtual bool AutoScroll
		{
			get
			{
				return this.auto_scroll;
			}
		}

		/// <summary>Gets the rectangle that represents the virtual display area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x00042DE8 File Offset: 0x00040FE8
		public override Rectangle DisplayRectangle
		{
			get
			{
				if (this.auto_scroll)
				{
					int num;
					if (this.canvas_size.Width <= base.DisplayRectangle.Width)
					{
						num = base.DisplayRectangle.Width;
						if (this.vscrollbar.VisibleInternal)
						{
							num -= this.vscrollbar.Width;
						}
					}
					else
					{
						num = this.canvas_size.Width;
					}
					int num2;
					if (this.canvas_size.Height <= base.DisplayRectangle.Height)
					{
						num2 = base.DisplayRectangle.Height;
						if (this.hscrollbar.VisibleInternal)
						{
							num2 -= this.hscrollbar.Height;
						}
					}
					else
					{
						num2 = this.canvas_size.Height;
					}
					this.display_rectangle.X = -this.scroll_position.X;
					this.display_rectangle.Y = -this.scroll_position.Y;
					this.display_rectangle.Width = Math.Max(this.auto_scroll_min_size.Width, num);
					this.display_rectangle.Height = Math.Max(this.auto_scroll_min_size.Height, num2);
				}
				else
				{
					this.display_rectangle = base.DisplayRectangle;
				}
				if (base.Padding != Padding.Empty)
				{
					this.display_rectangle.X = this.display_rectangle.X + base.Padding.Left;
					this.display_rectangle.Y = this.display_rectangle.Y + base.Padding.Top;
					this.display_rectangle.Width = this.display_rectangle.Width - base.Padding.Horizontal;
					this.display_rectangle.Height = this.display_rectangle.Height - base.Padding.Vertical;
				}
				return this.display_rectangle;
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000E9C RID: 3740 RVA: 0x00004663 File Offset: 0x00002863
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Scrolls the specified child control into view on an auto-scroll enabled control.</summary>
		/// <param name="activeControl">The child control to scroll into view. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000E9D RID: 3741 RVA: 0x00042FB8 File Offset: 0x000411B8
		public void ScrollControlIntoView(Control activeControl)
		{
			Rectangle rectangle = default(Rectangle);
			rectangle.Size = base.ClientSize;
			if (!this.AutoScroll || (!this.hscrollbar.VisibleInternal && !this.vscrollbar.VisibleInternal))
			{
				return;
			}
			if (!base.Contains(activeControl))
			{
				return;
			}
			if (this.vscrollbar.Visible)
			{
				rectangle.Width -= this.vscrollbar.Width;
			}
			if (this.hscrollbar.Visible)
			{
				rectangle.Height -= this.hscrollbar.Height;
			}
			if (rectangle.Contains(activeControl.Location) && rectangle.Contains(activeControl.Right, activeControl.Bottom))
			{
				return;
			}
			int num = 0;
			int num2 = 0;
			if (activeControl.Top <= 0 || activeControl.Height >= rectangle.Height)
			{
				num2 = -activeControl.Top;
			}
			else if (activeControl.Bottom > rectangle.Height)
			{
				num2 = rectangle.Height - activeControl.Bottom;
			}
			if (activeControl.Left <= 0 || activeControl.Width >= rectangle.Width)
			{
				num = -activeControl.Left;
			}
			else if (activeControl.Right > rectangle.Width)
			{
				num = rectangle.Width - activeControl.Right;
			}
			int num3 = this.hscrollbar.Value - num;
			int num4 = this.vscrollbar.Value - num2;
			if (this.hscrollbar.VisibleInternal)
			{
				if (num3 > this.hscrollbar.Maximum)
				{
					num3 = this.hscrollbar.Maximum;
				}
				else if (num3 < this.hscrollbar.Minimum)
				{
					num3 = this.hscrollbar.Minimum;
				}
				if (num3 != this.hscrollbar.Value)
				{
					this.hscrollbar.Value = num3;
				}
			}
			if (this.vscrollbar.VisibleInternal)
			{
				if (num4 > this.vscrollbar.Maximum)
				{
					num4 = this.vscrollbar.Maximum;
				}
				else if (num4 < this.vscrollbar.Minimum)
				{
					num4 = this.vscrollbar.Minimum;
				}
				if (num4 != this.vscrollbar.Value)
				{
					this.vscrollbar.Value = num4;
				}
			}
		}

		/// <summary>Adjusts the scroll bars on the container based on the current control positions and the control currently selected. </summary>
		/// <param name="displayScrollbars">true to show the scroll bars; otherwise, false. </param>
		// Token: 0x06000E9E RID: 3742 RVA: 0x00042D3B File Offset: 0x00040F3B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AdjustFormScrollbars(bool displayScrollbars)
		{
			this.Recalculate(false);
		}

		/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
		// Token: 0x06000E9F RID: 3743 RVA: 0x000431D6 File Offset: 0x000413D6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLayout(LayoutEventArgs levent)
		{
			this.CalculateCanvasSize(true);
			this.AdjustFormScrollbars(this.AutoScroll);
			base.OnLayout(levent);
			if (this is FlowLayoutPanel || this.autosized_child)
			{
				this.CalculateCanvasSize(false);
				this.AdjustFormScrollbars(this.AutoScroll);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06000EA0 RID: 3744 RVA: 0x00043218 File Offset: 0x00041418
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (this.vscrollbar.VisibleInternal)
			{
				if (e.Delta > 0)
				{
					if (this.vscrollbar.Minimum < this.vscrollbar.Value - this.vscrollbar.LargeChange)
					{
						this.vscrollbar.Value -= this.vscrollbar.LargeChange;
					}
					else
					{
						this.vscrollbar.Value = this.vscrollbar.Minimum;
					}
				}
				else
				{
					int num = this.vscrollbar.Maximum - this.vscrollbar.LargeChange + 1;
					if (num > this.vscrollbar.Value + this.vscrollbar.LargeChange)
					{
						this.vscrollbar.Value += this.vscrollbar.LargeChange;
					}
					else
					{
						this.vscrollbar.Value = num;
					}
				}
			}
			base.OnMouseWheel(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000EA1 RID: 3745 RVA: 0x000432FF File Offset: 0x000414FF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (base.Visible)
			{
				base.UpdateChildrenZOrder();
				base.PerformLayout(this, "Visible");
			}
			base.OnVisibleChanged(e);
		}

		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00043322 File Offset: 0x00041522
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			if (this.dock_padding != null)
			{
				this.dock_padding.Scale(dx, dy);
			}
			base.ScaleCore(dx, dy);
		}

		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06000EA3 RID: 3747 RVA: 0x0000677E File Offset: 0x0000497E
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06000EA4 RID: 3748 RVA: 0x00020975 File Offset: 0x0001EB75
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00043344 File Offset: 0x00041544
		internal override IntPtr AfterTopMostControl()
		{
			if (this.hscrollbar != null && this.hscrollbar.Visible)
			{
				return this.hscrollbar.Handle;
			}
			if (this.vscrollbar != null && this.vscrollbar.Visible)
			{
				return this.hscrollbar.Handle;
			}
			return base.AfterTopMostControl();
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0004339C File Offset: 0x0004159C
		internal virtual void CalculateCanvasSize(bool canOverride)
		{
			int count = base.Controls.Count;
			int num = 0;
			int num2 = 0;
			int num3 = this.hscrollbar.Value;
			int num4 = this.vscrollbar.Value;
			if (this.dock_padding != null)
			{
				num3 += this.dock_padding.Right;
				num4 += this.dock_padding.Bottom;
			}
			this.autosized_child = false;
			for (int i = 0; i < count; i++)
			{
				Control control = base.Controls[i];
				if (control.AutoSize)
				{
					this.autosized_child = true;
				}
				if (control.Dock == DockStyle.Right)
				{
					num3 += control.Width;
				}
				else if (control.Dock == DockStyle.Bottom)
				{
					num4 += control.Height;
				}
			}
			if (!this.auto_scroll_min_size.IsEmpty)
			{
				num = this.auto_scroll_min_size.Width;
				num2 = this.auto_scroll_min_size.Height;
			}
			for (int j = 0; j < count; j++)
			{
				Control control = base.Controls[j];
				switch (control.Dock)
				{
				case DockStyle.Top:
					if (control.Bottom + num4 > num2)
					{
						num2 = control.Bottom + num4;
					}
					break;
				case DockStyle.Bottom:
				case DockStyle.Right:
				case DockStyle.Fill:
					break;
				case DockStyle.Left:
					if (control.Right + num3 > num)
					{
						num = control.Right + num3;
					}
					break;
				default:
				{
					AnchorStyles anchor = control.Anchor;
					if ((anchor & AnchorStyles.Left) != AnchorStyles.None && (anchor & AnchorStyles.Right) == AnchorStyles.None && control.Right + num3 > num)
					{
						num = control.Right + num3;
					}
					if (((anchor & AnchorStyles.Top) != AnchorStyles.None || (anchor & AnchorStyles.Bottom) == AnchorStyles.None) && control.Bottom + num4 > num2)
					{
						num2 = control.Bottom + num4;
					}
					break;
				}
				}
			}
			this.canvas_size.Width = num;
			this.canvas_size.Height = num2;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0004355D File Offset: 0x0004175D
		internal void CreateDockPadding()
		{
			if (this.dock_padding == null)
			{
				this.dock_padding = new ScrollableControl.DockPaddingEdges(this);
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00043573 File Offset: 0x00041773
		private void Recalculate(object sender, EventArgs e)
		{
			this.Recalculate(true);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x0004357C File Offset: 0x0004177C
		private void Recalculate(bool doLayout)
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			Size size = this.canvas_size;
			Size clientSize = base.ClientSize;
			size.Width += this.auto_scroll_margin.Width;
			size.Height += this.auto_scroll_margin.Height;
			int num = clientSize.Width;
			int num2 = clientSize.Height;
			int num3;
			int num4;
			bool flag;
			bool flag2;
			do
			{
				num3 = num;
				num4 = num2;
				if ((this.force_hscroll_visible || (size.Width > num && this.auto_scroll)) && clientSize.Width > 0)
				{
					flag = true;
					num2 = clientSize.Height - SystemInformation.HorizontalScrollBarHeight;
				}
				else
				{
					flag = false;
					num2 = clientSize.Height;
				}
				if ((this.force_vscroll_visible || (size.Height > num2 && this.auto_scroll)) && clientSize.Height > 0)
				{
					flag2 = true;
					num = clientSize.Width - SystemInformation.VerticalScrollBarWidth;
				}
				else
				{
					flag2 = false;
					num = clientSize.Width;
				}
			}
			while (num != num3 || num2 != num4);
			if (num < 0)
			{
				num = 0;
			}
			if (num2 < 0)
			{
				num2 = 0;
			}
			Rectangle rectangle = new Rectangle(0, clientSize.Height - SystemInformation.HorizontalScrollBarHeight, base.ClientRectangle.Width, SystemInformation.HorizontalScrollBarHeight);
			Rectangle rectangle2 = new Rectangle(clientSize.Width - SystemInformation.VerticalScrollBarWidth, 0, SystemInformation.VerticalScrollBarWidth, base.ClientRectangle.Height);
			if (!this.vscrollbar.Visible)
			{
				this.vscrollbar.Value = 0;
			}
			if (!this.hscrollbar.Visible)
			{
				this.hscrollbar.Value = 0;
			}
			if (flag)
			{
				this.hscrollbar.manual_thumb_size = num;
				this.hscrollbar.LargeChange = num;
				this.hscrollbar.SmallChange = 5;
				this.hscrollbar.Maximum = size.Width - 1;
			}
			else
			{
				if (this.hscrollbar != null && this.hscrollbar.VisibleInternal)
				{
					this.ScrollWindow(-this.scroll_position.X, 0);
				}
				this.scroll_position.X = 0;
			}
			if (flag2)
			{
				this.vscrollbar.manual_thumb_size = num2;
				this.vscrollbar.LargeChange = num2;
				this.vscrollbar.SmallChange = 5;
				this.vscrollbar.Maximum = size.Height - 1;
			}
			else
			{
				if (this.vscrollbar != null && this.vscrollbar.VisibleInternal)
				{
					this.ScrollWindow(0, -this.scroll_position.Y);
				}
				this.scroll_position.Y = 0;
			}
			if (flag && flag2)
			{
				rectangle.Width -= SystemInformation.VerticalScrollBarWidth;
				rectangle2.Height -= SystemInformation.HorizontalScrollBarHeight;
				this.sizegrip.Bounds = new Rectangle(rectangle.Right, rectangle2.Bottom, SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
			}
			base.SuspendLayout();
			this.hscrollbar.SetBoundsInternal(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, BoundsSpecified.None);
			this.hscrollbar.Visible = flag;
			if (this.hscrollbar.Visible)
			{
				XplatUI.SetZOrder(this.hscrollbar.Handle, IntPtr.Zero, true, false);
			}
			this.vscrollbar.SetBoundsInternal(rectangle2.X, rectangle2.Y, rectangle2.Width, rectangle2.Height, BoundsSpecified.None);
			this.vscrollbar.Visible = flag2;
			if (this.vscrollbar.Visible)
			{
				XplatUI.SetZOrder(this.vscrollbar.Handle, IntPtr.Zero, true, false);
			}
			this.UpdateSizeGripVisible();
			base.ResumeLayout(doLayout);
			ContainerControl containerControl = this as ContainerControl;
			if (containerControl != null && containerControl.ActiveControl != null)
			{
				this.ScrollControlIntoView(containerControl.ActiveControl);
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00043930 File Offset: 0x00041B30
		internal void UpdateSizeGripVisible()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			this.sizegrip.CapturedControl = base.Parent;
			bool flag = this.hscrollbar.VisibleInternal && this.vscrollbar.VisibleInternal;
			bool flag2 = false;
			if (flag && base.Parent != null)
			{
				Point point = new Point(base.Parent.ClientRectangle.Bottom - base.Bottom, base.Parent.ClientRectangle.Right - base.Right);
				flag2 = point.X <= 2 && point.X >= 0 && point.Y <= 2 && point.Y >= 0;
			}
			this.sizegrip.Visible = flag;
			this.sizegrip.Enabled = flag2 || this.sizegrip.Capture;
			if (this.sizegrip.Visible)
			{
				XplatUI.SetZOrder(this.sizegrip.Handle, this.vscrollbar.Handle, false, false);
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00043A40 File Offset: 0x00041C40
		private void HandleScrollBar(object sender, EventArgs e)
		{
			if (sender == this.vscrollbar)
			{
				if (!this.vscrollbar.Visible)
				{
					return;
				}
				this.ScrollWindow(0, this.vscrollbar.Value - this.scroll_position.Y);
				return;
			}
			else
			{
				if (!this.hscrollbar.Visible)
				{
					return;
				}
				this.ScrollWindow(this.hscrollbar.Value - this.scroll_position.X, 0);
				return;
			}
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00043AAF File Offset: 0x00041CAF
		private void HandleScrollEvent(object sender, ScrollEventArgs args)
		{
			this.OnScroll(args);
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00043AB8 File Offset: 0x00041CB8
		private void AddScrollbars(object o, EventArgs e)
		{
			base.Controls.AddRangeImplicit(new Control[] { this.hscrollbar, this.vscrollbar, this.sizegrip });
			base.HandleCreated -= this.AddScrollbars;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00043AF8 File Offset: 0x00041CF8
		private void CreateScrollbars()
		{
			this.hscrollbar = new ImplicitHScrollBar();
			this.hscrollbar.Visible = false;
			this.hscrollbar.ValueChanged += this.HandleScrollBar;
			this.hscrollbar.Height = SystemInformation.HorizontalScrollBarHeight;
			this.hscrollbar.use_manual_thumb_size = true;
			this.hscrollbar.Scroll += this.HandleScrollEvent;
			this.vscrollbar = new ImplicitVScrollBar();
			this.vscrollbar.Visible = false;
			this.vscrollbar.ValueChanged += this.HandleScrollBar;
			this.vscrollbar.Width = SystemInformation.VerticalScrollBarWidth;
			this.vscrollbar.use_manual_thumb_size = true;
			this.vscrollbar.Scroll += this.HandleScrollEvent;
			this.sizegrip = new SizeGrip(this);
			this.sizegrip.Visible = false;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00043BE0 File Offset: 0x00041DE0
		private void ScrollWindow(int XOffset, int YOffset)
		{
			if (XOffset == 0 && YOffset == 0)
			{
				return;
			}
			base.SuspendLayout();
			int count = base.Controls.Count;
			for (int i = 0; i < count; i++)
			{
				base.Controls[i].Location = new Point(base.Controls[i].Left - XOffset, base.Controls[i].Top - YOffset);
			}
			this.scroll_position.X = this.scroll_position.X + XOffset;
			this.scroll_position.Y = this.scroll_position.Y + YOffset;
			XplatUI.ScrollWindow(base.Handle, base.ClientRectangle, -XOffset, -YOffset, false);
			base.ResumeLayout(false);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ScrollableControl.Scroll" /> event.</summary>
		/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data. </param>
		// Token: 0x06000EB0 RID: 3760 RVA: 0x00043C94 File Offset: 0x00041E94
		protected virtual void OnScroll(ScrollEventArgs se)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[ScrollableControl.OnScrollEvent];
			if (scrollEventHandler != null)
			{
				scrollEventHandler(this, se);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000EB1 RID: 3761 RVA: 0x0002541F File Offset: 0x0002361F
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
		}

		/// <summary>Paints the background of the control.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06000EB2 RID: 3762 RVA: 0x0000493E File Offset: 0x00002B3E
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000EB3 RID: 3763 RVA: 0x00025448 File Offset: 0x00023648
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		// Token: 0x04000965 RID: 2405
		private bool force_hscroll_visible;

		// Token: 0x04000966 RID: 2406
		private bool force_vscroll_visible;

		// Token: 0x04000967 RID: 2407
		private bool auto_scroll;

		// Token: 0x04000968 RID: 2408
		private Size auto_scroll_margin;

		// Token: 0x04000969 RID: 2409
		private Size auto_scroll_min_size;

		// Token: 0x0400096A RID: 2410
		private Point scroll_position;

		// Token: 0x0400096B RID: 2411
		private ScrollableControl.DockPaddingEdges dock_padding;

		// Token: 0x0400096C RID: 2412
		private SizeGrip sizegrip;

		// Token: 0x0400096D RID: 2413
		internal ImplicitHScrollBar hscrollbar;

		// Token: 0x0400096E RID: 2414
		internal ImplicitVScrollBar vscrollbar;

		// Token: 0x0400096F RID: 2415
		internal Size canvas_size;

		// Token: 0x04000970 RID: 2416
		private Rectangle display_rectangle;

		// Token: 0x04000971 RID: 2417
		private Control old_parent;

		// Token: 0x04000972 RID: 2418
		private HScrollProperties horizontalScroll;

		// Token: 0x04000973 RID: 2419
		private VScrollProperties verticalScroll;

		// Token: 0x04000974 RID: 2420
		private bool autosized_child;

		// Token: 0x04000975 RID: 2421
		private static object OnScrollEvent = new object();

		/// <summary>Determines the border padding for docked controls.</summary>
		// Token: 0x02000184 RID: 388
		[TypeConverter(typeof(ScrollableControl.DockPaddingEdgesConverter))]
		public class DockPaddingEdges : ICloneable
		{
			// Token: 0x06000EB5 RID: 3765 RVA: 0x00043CCE File Offset: 0x00041ECE
			internal DockPaddingEdges(Control owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets or sets the padding width for all edges of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x170003C1 RID: 961
			// (get) Token: 0x06000EB6 RID: 3766 RVA: 0x00043CE0 File Offset: 0x00041EE0
			[RefreshProperties(RefreshProperties.All)]
			public int All
			{
				get
				{
					return this.owner.Padding.All;
				}
			}

			/// <summary>Gets or sets the padding width for the bottom edge of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x170003C2 RID: 962
			// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00043D00 File Offset: 0x00041F00
			// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x00043D20 File Offset: 0x00041F20
			[RefreshProperties(RefreshProperties.All)]
			public int Bottom
			{
				get
				{
					return this.owner.Padding.Bottom;
				}
				set
				{
					this.owner.Padding = new Padding(this.Left, this.Top, this.Right, value);
				}
			}

			/// <summary>Gets or sets the padding width for the left edge of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x170003C3 RID: 963
			// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00043D48 File Offset: 0x00041F48
			// (set) Token: 0x06000EBA RID: 3770 RVA: 0x00043D68 File Offset: 0x00041F68
			[RefreshProperties(RefreshProperties.All)]
			public int Left
			{
				get
				{
					return this.owner.Padding.Left;
				}
				set
				{
					this.owner.Padding = new Padding(value, this.Top, this.Right, this.Bottom);
				}
			}

			/// <summary>Gets or sets the padding width for the right edge of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x170003C4 RID: 964
			// (get) Token: 0x06000EBB RID: 3771 RVA: 0x00043D90 File Offset: 0x00041F90
			// (set) Token: 0x06000EBC RID: 3772 RVA: 0x00043DB0 File Offset: 0x00041FB0
			[RefreshProperties(RefreshProperties.All)]
			public int Right
			{
				get
				{
					return this.owner.Padding.Right;
				}
				set
				{
					this.owner.Padding = new Padding(this.Left, this.Top, value, this.Bottom);
				}
			}

			/// <summary>Gets or sets the padding width for the top edge of a docked control.</summary>
			/// <returns>The padding width, in pixels.</returns>
			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x06000EBD RID: 3773 RVA: 0x00043DD8 File Offset: 0x00041FD8
			// (set) Token: 0x06000EBE RID: 3774 RVA: 0x00043DF8 File Offset: 0x00041FF8
			[RefreshProperties(RefreshProperties.All)]
			public int Top
			{
				get
				{
					return this.owner.Padding.Top;
				}
				set
				{
					this.owner.Padding = new Padding(this.Left, value, this.Right, this.Bottom);
				}
			}

			/// <returns>true if the specified object  is equal to the current object; otherwise, false.</returns>
			// Token: 0x06000EBF RID: 3775 RVA: 0x00043E20 File Offset: 0x00042020
			public override bool Equals(object other)
			{
				return other is ScrollableControl.DockPaddingEdges && (this.All == ((ScrollableControl.DockPaddingEdges)other).All && this.Left == ((ScrollableControl.DockPaddingEdges)other).Left && this.Right == ((ScrollableControl.DockPaddingEdges)other).Right && this.Top == ((ScrollableControl.DockPaddingEdges)other).Top && this.Bottom == ((ScrollableControl.DockPaddingEdges)other).Bottom);
			}

			/// <returns>A hash code for the current object.</returns>
			// Token: 0x06000EC0 RID: 3776 RVA: 0x00043E99 File Offset: 0x00042099
			public override int GetHashCode()
			{
				return this.All * this.Top * this.Bottom * this.Right * this.Left;
			}

			/// <summary>Returns an empty string.</summary>
			/// <returns>An empty string.</returns>
			// Token: 0x06000EC1 RID: 3777 RVA: 0x00043EC0 File Offset: 0x000420C0
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"All = ",
					this.All.ToString(),
					" Top = ",
					this.Top.ToString(),
					" Left = ",
					this.Left.ToString(),
					" Bottom = ",
					this.Bottom.ToString(),
					" Right = ",
					this.Right.ToString()
				});
			}

			// Token: 0x06000EC2 RID: 3778 RVA: 0x00043F58 File Offset: 0x00042158
			internal void Scale(float dx, float dy)
			{
				this.Left = (int)((float)this.Left * dx);
				this.Right = (int)((float)this.Right * dx);
				this.Top = (int)((float)this.Top * dy);
				this.Bottom = (int)((float)this.Bottom * dy);
			}

			/// <summary>Creates a new object that is a copy of the current instance.</summary>
			/// <returns>A new object that is a copy of the current instance.</returns>
			// Token: 0x06000EC3 RID: 3779 RVA: 0x00043FA5 File Offset: 0x000421A5
			object ICloneable.Clone()
			{
				return new ScrollableControl.DockPaddingEdges(this.owner);
			}

			// Token: 0x04000976 RID: 2422
			private Control owner;
		}

		/// <summary>A <see cref="T:System.ComponentModel.TypeConverter" /> for the <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> class.</summary>
		// Token: 0x02000185 RID: 389
		public class DockPaddingEdgesConverter : TypeConverter
		{
			/// <summary>Returns a collection of properties for the type of array specified by the value parameter, using the specified context and attributes.</summary>
			/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that are exposed for the <see cref="T:System.Windows.Forms.ScrollableControl" />.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
			/// <param name="value">An object that specifies the type of array for which to get properties.</param>
			/// <param name="attributes">An array of type attribute that is used as a filter.</param>
			// Token: 0x06000EC5 RID: 3781 RVA: 0x00043FB2 File Offset: 0x000421B2
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				return TypeDescriptor.GetProperties(typeof(ScrollableControl.DockPaddingEdges), attributes);
			}

			/// <summary>Returns whether the current object supports properties, using the specified context.</summary>
			/// <returns>true in all cases.</returns>
			/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context. </param>
			// Token: 0x06000EC6 RID: 3782 RVA: 0x00006F54 File Offset: 0x00005154
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}
	}
}
