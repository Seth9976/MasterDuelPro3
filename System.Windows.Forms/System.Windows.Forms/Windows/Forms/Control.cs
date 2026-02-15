using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Defines the base class for controls, which are components with visual representation.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200004D RID: 77
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Text")]
	[DefaultEvent("Click")]
	[DesignerSerializer("System.Windows.Forms.Design.ControlCodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItemFilter("System.Windows.Forms")]
	public class Control : Component, ISynchronizeInvoke, IWin32Window, IBindableComponent, IComponent, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class with default settings.</summary>
		// Token: 0x06000255 RID: 597 RVA: 0x000098BC File Offset: 0x00007ABC
		public Control()
		{
			if (WindowsFormsSynchronizationContext.AutoInstall && !(SynchronizationContext.Current is WindowsFormsSynchronizationContext))
			{
				SynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
			}
			this.layout_type = Control.LayoutType.Anchor;
			this.anchor_style = AnchorStyles.Top | AnchorStyles.Left;
			this.is_created = false;
			this.is_visible = true;
			this.is_captured = false;
			this.is_disposed = false;
			this.is_enabled = true;
			this.is_entered = false;
			this.layout_pending = false;
			this.is_toplevel = false;
			this.causes_validation = true;
			this.has_focus = false;
			this.layout_suspended = 0;
			this.mouse_clicks = 1;
			this.tab_index = -1;
			this.cursor = null;
			this.right_to_left = RightToLeft.Inherit;
			this.border_style = BorderStyle.None;
			this.background_color = Color.Empty;
			this.dist_right = 0;
			this.dist_bottom = 0;
			this.tab_stop = true;
			this.ime_mode = ImeMode.Inherit;
			this.use_compatible_text_rendering = true;
			this.show_keyboard_cues = false;
			this.show_focus_cues = SystemInformation.MenuAccessKeysUnderlined;
			this.use_wait_cursor = false;
			this.backgroundimage_layout = ImageLayout.Tile;
			this.use_compatible_text_rendering = Application.use_compatible_text_rendering;
			this.padding = this.DefaultPadding;
			this.maximum_size = default(Size);
			this.minimum_size = default(Size);
			this.margin = this.DefaultMargin;
			this.auto_size_mode = AutoSizeMode.GrowOnly;
			this.control_style = ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.Selectable | ControlStyles.StandardDoubleClick | ControlStyles.AllPaintingInWmPaint;
			this.control_style |= ControlStyles.UseTextForAccessibility;
			this.parent = null;
			this.background_image = null;
			this.text = string.Empty;
			this.name = string.Empty;
			this.window_target = new Control.ControlWindowTarget(this);
			this.window = new Control.ControlNativeWindow(this);
			this.child_controls = this.CreateControlsInstance();
			this.bounds.Size = this.DefaultSize;
			this.client_size = this.ClientSizeFromSize(this.bounds.Size);
			this.client_rect = new Rectangle(Point.Empty, this.client_size);
			this.explicit_bounds = this.bounds;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control" /> class as a child control, with specific text.</summary>
		/// <param name="parent">The <see cref="T:System.Windows.Forms.Control" /> to be the parent of the control. </param>
		/// <param name="text">The text displayed by the control. </param>
		// Token: 0x06000256 RID: 598 RVA: 0x00009AB7 File Offset: 0x00007CB7
		public Control(Control parent, string text)
			: this()
		{
			this.Text = text;
			this.Parent = parent;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control" /> and its child controls and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000257 RID: 599 RVA: 0x00009AD0 File Offset: 0x00007CD0
		protected override void Dispose(bool disposing)
		{
			if (!this.is_disposed && disposing)
			{
				this.is_disposing = true;
				this.Capture = false;
				this.DisposeBackBuffer();
				if (this.InvokeRequired)
				{
					if (Application.MessageLoop && this.IsHandleCreated)
					{
						this.BeginInvokeInternal(new MethodInvoker(this.DestroyHandle), null);
					}
				}
				else
				{
					this.DestroyHandle();
				}
				if (this.parent != null)
				{
					this.parent.Controls.Remove(this);
				}
				Control[] allControls = this.child_controls.GetAllControls();
				for (int i = 0; i < allControls.Length; i++)
				{
					allControls[i].parent = null;
					allControls[i].Dispose();
				}
			}
			this.is_disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00009B88 File Offset: 0x00007D88
		internal Rectangle PaddingClientRectangle
		{
			get
			{
				return new Rectangle(this.ClientRectangle.Left + this.padding.Left, this.ClientRectangle.Top + this.padding.Top, this.ClientRectangle.Width - this.padding.Horizontal, this.ClientRectangle.Height - this.padding.Vertical);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00009C02 File Offset: 0x00007E02
		// (set) Token: 0x0600025A RID: 602 RVA: 0x00009C0A File Offset: 0x00007E0A
		internal MenuTracker ActiveTracker
		{
			get
			{
				return this.active_tracker;
			}
			set
			{
				if (value == this.active_tracker)
				{
					return;
				}
				this.Capture = value != null;
				this.active_tracker = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00009C28 File Offset: 0x00007E28
		internal bool InternalSelected
		{
			get
			{
				IContainerControl containerControl = this.GetContainerControl();
				return containerControl != null && containerControl.ActiveControl == this;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00009C4C File Offset: 0x00007E4C
		internal bool InternalContainsFocus
		{
			get
			{
				IntPtr focus = XplatUI.GetFocus();
				if (this.IsHandleCreated)
				{
					if (focus == this.Handle)
					{
						return true;
					}
					Control[] allControls = this.child_controls.GetAllControls();
					for (int i = 0; i < allControls.Length; i++)
					{
						if (allControls[i].InternalContainsFocus)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00009C9E File Offset: 0x00007E9E
		internal bool Entered
		{
			get
			{
				return this.is_entered;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00009CA6 File Offset: 0x00007EA6
		internal bool VisibleInternal
		{
			get
			{
				return this.is_visible;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600025F RID: 607 RVA: 0x00009CAE File Offset: 0x00007EAE
		internal Control.LayoutType ControlLayoutType
		{
			get
			{
				return this.layout_type;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00009CB6 File Offset: 0x00007EB6
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00009CC0 File Offset: 0x00007EC0
		internal BorderStyle InternalBorderStyle
		{
			get
			{
				return this.border_style;
			}
			set
			{
				if (!Enum.IsDefined(typeof(BorderStyle), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for BorderStyle", value));
				}
				if (this.border_style != value)
				{
					this.border_style = value;
					if (this.IsHandleCreated)
					{
						XplatUI.SetBorderStyle(this.window.Handle, (FormBorderStyle)this.border_style);
						this.RecreateHandle();
						this.Refresh();
						return;
					}
					this.client_size = this.ClientSizeFromSize(this.bounds.Size);
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (set) Token: 0x06000262 RID: 610 RVA: 0x00009D4C File Offset: 0x00007F4C
		internal Size InternalClientSize
		{
			set
			{
				this.client_size = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000263 RID: 611 RVA: 0x00006F54 File Offset: 0x00005154
		internal virtual bool ActivateOnShow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00009D55 File Offset: 0x00007F55
		// (set) Token: 0x06000265 RID: 613 RVA: 0x00009D5D File Offset: 0x00007F5D
		internal Rectangle ExplicitBounds
		{
			get
			{
				return this.explicit_bounds;
			}
			set
			{
				this.explicit_bounds = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00009D68 File Offset: 0x00007F68
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00009D88 File Offset: 0x00007F88
		internal bool ValidationFailed
		{
			get
			{
				ContainerControl containerControl = this.InternalGetContainerControl();
				return containerControl != null && containerControl.validation_failed;
			}
			set
			{
				ContainerControl containerControl = this.InternalGetContainerControl();
				if (containerControl != null)
				{
					containerControl.validation_failed = value;
				}
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009DA6 File Offset: 0x00007FA6
		internal IAsyncResult BeginInvokeInternal(Delegate method, object[] args)
		{
			return this.BeginInvokeInternal(method, args, this.FindControlToInvokeOn());
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009DB8 File Offset: 0x00007FB8
		internal IAsyncResult BeginInvokeInternal(Delegate method, object[] args, Control control)
		{
			AsyncMethodResult asyncMethodResult = new AsyncMethodResult();
			AsyncMethodData asyncMethodData = new AsyncMethodData();
			asyncMethodData.Handle = control.GetInvokableHandle();
			asyncMethodData.Method = method;
			asyncMethodData.Args = args;
			asyncMethodData.Result = asyncMethodResult;
			if (!ExecutionContext.IsFlowSuppressed())
			{
				asyncMethodData.Context = ExecutionContext.Capture();
			}
			XplatUI.SendAsyncMethod(asyncMethodData);
			return asyncMethodResult;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009E0B File Offset: 0x0000800B
		private IntPtr GetInvokableHandle()
		{
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			return this.window.Handle;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009E26 File Offset: 0x00008026
		internal void PointToClient(ref int x, ref int y)
		{
			XplatUI.ScreenToClient(this.Handle, ref x, ref y);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00009E35 File Offset: 0x00008035
		internal bool IsRecreating
		{
			get
			{
				return this.is_recreating;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00009E3D File Offset: 0x0000803D
		internal Graphics DeviceContext
		{
			get
			{
				return Hwnd.GraphicsContext;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009E44 File Offset: 0x00008044
		internal virtual int OverrideHeight(int height)
		{
			return height;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009E48 File Offset: 0x00008048
		private void ProcessActiveTracker(ref Message m)
		{
			bool flag = m.Msg == 514 || m.Msg == 517;
			MouseButtons mouseButtons = Control.FromParamToMouseButtons((long)m.WParam.ToInt32());
			if (flag)
			{
				Msg msg = (Msg)m.Msg;
				if (msg != Msg.WM_LBUTTONUP)
				{
					if (msg == Msg.WM_RBUTTONUP)
					{
						mouseButtons |= MouseButtons.Right;
					}
				}
				else
				{
					mouseButtons |= MouseButtons.Left;
				}
			}
			MouseEventArgs mouseEventArgs = new MouseEventArgs(mouseButtons, this.mouse_clicks, Control.MousePosition.X, Control.MousePosition.Y, 0);
			if (flag)
			{
				this.active_tracker.OnMouseUp(mouseEventArgs);
				this.mouse_clicks = 1;
				return;
			}
			if (!this.active_tracker.OnMouseDown(mouseEventArgs))
			{
				Control realChildAtPoint = this.GetRealChildAtPoint(Cursor.Position);
				if (realChildAtPoint != null)
				{
					Point point = realChildAtPoint.PointToClient(Cursor.Position);
					XplatUI.SendMessage(realChildAtPoint.Handle, (Msg)m.Msg, m.WParam, Control.MakeParam(point.X, point.Y));
				}
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009F54 File Offset: 0x00008154
		private Control FindControlToInvokeOn()
		{
			Control control = this;
			while (!control.IsHandleCreated)
			{
				control = control.parent;
				if (control == null)
				{
					break;
				}
			}
			if (control == null || !control.IsHandleCreated)
			{
				throw new InvalidOperationException("Cannot call Invoke or BeginInvoke on a control until the window handle is created");
			}
			return control;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009F8C File Offset: 0x0000818C
		private void InvalidateBackBuffer()
		{
			if (this.backbuffer != null)
			{
				this.backbuffer.Invalidate();
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009FA1 File Offset: 0x000081A1
		private Control.DoubleBuffer GetBackBuffer()
		{
			if (this.backbuffer == null)
			{
				this.backbuffer = new Control.DoubleBuffer(this);
			}
			return this.backbuffer;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009FBD File Offset: 0x000081BD
		private void DisposeBackBuffer()
		{
			if (this.backbuffer != null)
			{
				this.backbuffer.Dispose();
				this.backbuffer = null;
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009FDC File Offset: 0x000081DC
		internal static void SetChildColor(Control parent)
		{
			for (int i = 0; i < parent.child_controls.Count; i++)
			{
				Control control = parent.child_controls[i];
				if (control.child_controls.Count > 0)
				{
					Control.SetChildColor(control);
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000A020 File Offset: 0x00008220
		internal bool Select(Control control)
		{
			if (control == null)
			{
				return false;
			}
			IContainerControl containerControl = this.GetContainerControl();
			if (containerControl != null && (Control)containerControl != control)
			{
				containerControl.ActiveControl = control;
				if (containerControl.ActiveControl == control && !control.has_focus && control.IsHandleCreated)
				{
					XplatUI.SetFocus(control.window.Handle);
				}
			}
			else if (control.IsHandleCreated)
			{
				XplatUI.SetFocus(control.window.Handle);
			}
			return true;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000A090 File Offset: 0x00008290
		internal static IntPtr MakeParam(int low, int high)
		{
			return new IntPtr((high << 16) | (low & 65535));
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000A0A3 File Offset: 0x000082A3
		internal static int LowOrder(int param)
		{
			return (int)((short)(param & 65535));
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000A0AD File Offset: 0x000082AD
		internal static int HighOrder(long param)
		{
			return (int)((short)(param >> 16));
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A0B4 File Offset: 0x000082B4
		internal virtual void PaintControlBackground(PaintEventArgs pevent)
		{
			bool flag = (this.CreateParams.Style & 2048) != 0;
			if (((this.BackColor.A != 255 && this.GetStyle(ControlStyles.SupportsTransparentBackColor)) || flag) && this.parent != null)
			{
				PaintEventArgs paintEventArgs = new PaintEventArgs(pevent.Graphics, new Rectangle(pevent.ClipRectangle.X + this.Left, pevent.ClipRectangle.Y + this.Top, pevent.ClipRectangle.Width, pevent.ClipRectangle.Height));
				GraphicsState graphicsState = paintEventArgs.Graphics.Save();
				paintEventArgs.Graphics.TranslateTransform((float)(-(float)this.Left), (float)(-(float)this.Top));
				this.parent.OnPaintBackground(paintEventArgs);
				paintEventArgs.Graphics.Restore(graphicsState);
				graphicsState = paintEventArgs.Graphics.Save();
				paintEventArgs.Graphics.TranslateTransform((float)(-(float)this.Left), (float)(-(float)this.Top));
				this.parent.OnPaint(paintEventArgs);
				paintEventArgs.Graphics.Restore(graphicsState);
				paintEventArgs.SetGraphics(null);
			}
			if (this.clip_region != null && XplatUI.UserClipWontExposeParent && this.parent != null)
			{
				Hwnd hwnd = Hwnd.ObjectFromHandle(this.Handle);
				if (hwnd != null)
				{
					PaintEventArgs paintEventArgs2 = new PaintEventArgs(pevent.Graphics, new Rectangle(pevent.ClipRectangle.X + this.Left, pevent.ClipRectangle.Y + this.Top, pevent.ClipRectangle.Width, pevent.ClipRectangle.Height));
					Region region = new Region();
					region.MakeEmpty();
					region.Union(this.ClientRectangle);
					foreach (Rectangle rectangle in hwnd.ClipRectangles)
					{
						region.Union(rectangle);
					}
					GraphicsState graphicsState2 = paintEventArgs2.Graphics.Save();
					paintEventArgs2.Graphics.Clip = region;
					paintEventArgs2.Graphics.TranslateTransform((float)(-(float)this.Left), (float)(-(float)this.Top));
					this.parent.OnPaintBackground(paintEventArgs2);
					paintEventArgs2.Graphics.Restore(graphicsState2);
					graphicsState2 = paintEventArgs2.Graphics.Save();
					paintEventArgs2.Graphics.Clip = region;
					paintEventArgs2.Graphics.TranslateTransform((float)(-(float)this.Left), (float)(-(float)this.Top));
					this.parent.OnPaint(paintEventArgs2);
					paintEventArgs2.Graphics.Restore(graphicsState2);
					paintEventArgs2.SetGraphics(null);
					region.Intersect(this.clip_region);
					pevent.Graphics.Clip = region;
				}
			}
			if (this.background_image == null)
			{
				if (!flag)
				{
					Rectangle clipRectangle = pevent.ClipRectangle;
					Brush solidBrush = ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor);
					pevent.Graphics.FillRectangle(solidBrush, clipRectangle);
				}
				return;
			}
			this.DrawBackgroundImage(pevent.Graphics);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000A3D8 File Offset: 0x000085D8
		private void DrawBackgroundImage(Graphics g)
		{
			Rectangle rectangle = default(Rectangle);
			g.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor), this.ClientRectangle);
			switch (this.backgroundimage_layout)
			{
			case ImageLayout.None:
				rectangle.Location = Point.Empty;
				rectangle.Size = this.background_image.Size;
				break;
			case ImageLayout.Tile:
			{
				using (TextureBrush textureBrush = new TextureBrush(this.background_image, WrapMode.Tile))
				{
					g.FillRectangle(textureBrush, this.ClientRectangle);
				}
				return;
			}
			case ImageLayout.Center:
				rectangle.Location = new Point(this.ClientSize.Width / 2 - this.background_image.Width / 2, this.ClientSize.Height / 2 - this.background_image.Height / 2);
				rectangle.Size = this.background_image.Size;
				break;
			case ImageLayout.Stretch:
				rectangle = this.ClientRectangle;
				break;
			case ImageLayout.Zoom:
				rectangle = this.ClientRectangle;
				if ((float)this.background_image.Width / (float)this.background_image.Height < (float)rectangle.Width / (float)rectangle.Height)
				{
					rectangle.Width = (int)((float)this.background_image.Width * ((float)rectangle.Height / (float)this.background_image.Height));
					rectangle.X = (this.ClientRectangle.Width - rectangle.Width) / 2;
				}
				else
				{
					rectangle.Height = (int)((float)this.background_image.Height * ((float)rectangle.Width / (float)this.background_image.Width));
					rectangle.Y = (this.ClientRectangle.Height - rectangle.Height) / 2;
				}
				break;
			default:
				return;
			}
			g.DrawImage(this.background_image, rectangle);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000A5CC File Offset: 0x000087CC
		internal virtual void DndEnter(DragEventArgs e)
		{
			try
			{
				this.OnDragEnter(e);
			}
			catch
			{
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000A5F8 File Offset: 0x000087F8
		internal virtual void DndOver(DragEventArgs e)
		{
			try
			{
				this.OnDragOver(e);
			}
			catch
			{
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000A624 File Offset: 0x00008824
		internal virtual void DndDrop(DragEventArgs e)
		{
			try
			{
				this.OnDragDrop(e);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("MWF: Exception while dropping:");
				Console.Error.WriteLine(ex);
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000A668 File Offset: 0x00008868
		internal virtual void DndLeave(EventArgs e)
		{
			try
			{
				this.OnDragLeave(e);
			}
			catch
			{
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000A694 File Offset: 0x00008894
		internal virtual void DndFeedback(GiveFeedbackEventArgs e)
		{
			try
			{
				this.OnGiveFeedback(e);
			}
			catch
			{
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A6C0 File Offset: 0x000088C0
		internal virtual void DndContinueDrag(QueryContinueDragEventArgs e)
		{
			try
			{
				this.OnQueryContinueDrag(e);
			}
			catch
			{
			}
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A6EC File Offset: 0x000088EC
		internal static MouseButtons FromParamToMouseButtons(long param)
		{
			MouseButtons mouseButtons = MouseButtons.None;
			if ((param & 1L) != 0L)
			{
				mouseButtons |= MouseButtons.Left;
			}
			if ((param & 16L) != 0L)
			{
				mouseButtons |= MouseButtons.Middle;
			}
			if ((param & 2L) != 0L)
			{
				mouseButtons |= MouseButtons.Right;
			}
			return mouseButtons;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000A727 File Offset: 0x00008927
		internal virtual void FireEnter()
		{
			this.OnEnter(EventArgs.Empty);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000A734 File Offset: 0x00008934
		internal virtual void FireLeave()
		{
			this.OnLeave(EventArgs.Empty);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000A741 File Offset: 0x00008941
		internal virtual void FireValidating(CancelEventArgs ce)
		{
			this.OnValidating(ce);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000A74A File Offset: 0x0000894A
		internal virtual void FireValidated()
		{
			this.OnValidated(EventArgs.Empty);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000A757 File Offset: 0x00008957
		internal virtual bool ProcessControlMnemonic(char charCode)
		{
			return this.ProcessMnemonic(charCode);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000A760 File Offset: 0x00008960
		private static Control FindFlatForward(Control container, Control start)
		{
			Control control = null;
			int count = container.child_controls.Count;
			bool flag = false;
			int num;
			if (start != null)
			{
				num = start.tab_index;
			}
			else
			{
				num = -1;
			}
			for (int i = 0; i < count; i++)
			{
				if (start == container.child_controls[i])
				{
					flag = true;
				}
				else if ((control == null || control.tab_index > container.child_controls[i].tab_index) && (container.child_controls[i].tab_index > num || (flag && container.child_controls[i].tab_index == num)))
				{
					control = container.child_controls[i];
				}
			}
			return control;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000A808 File Offset: 0x00008A08
		private static Control FindControlForward(Control container, Control start)
		{
			if (start == null)
			{
				return Control.FindFlatForward(container, start);
			}
			if (start.child_controls != null && start.child_controls.Count > 0 && (start == container || !(start is IContainerControl) || !start.GetStyle(ControlStyles.ContainerControl)))
			{
				return Control.FindControlForward(start, null);
			}
			while (start != container)
			{
				Control control = Control.FindFlatForward(start.parent, start);
				if (control != null)
				{
					return control;
				}
				start = start.parent;
			}
			return null;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000A874 File Offset: 0x00008A74
		private static Control FindFlatBackward(Control container, Control start)
		{
			Control control = null;
			int count = container.child_controls.Count;
			bool flag = false;
			int maxValue;
			if (start != null)
			{
				maxValue = start.tab_index;
			}
			else
			{
				maxValue = int.MaxValue;
			}
			for (int i = count - 1; i >= 0; i--)
			{
				if (start == container.child_controls[i])
				{
					flag = true;
				}
				else if ((control == null || control.tab_index < container.child_controls[i].tab_index) && (container.child_controls[i].tab_index < maxValue || (flag && container.child_controls[i].tab_index == maxValue)))
				{
					control = container.child_controls[i];
				}
			}
			return control;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000A918 File Offset: 0x00008B18
		private static Control FindControlBackward(Control container, Control start)
		{
			Control control = null;
			if (start == null)
			{
				control = Control.FindFlatBackward(container, start);
			}
			else if (start != container && start.parent != null)
			{
				control = Control.FindFlatBackward(start.parent, start);
				if (control == null)
				{
					if (start.parent != container)
					{
						return start.parent;
					}
					return null;
				}
			}
			if (control == null || start.parent == null)
			{
				control = start;
			}
			while (control != null && (control == container || ((!(control is IContainerControl) || !control.GetStyle(ControlStyles.ContainerControl)) && control.child_controls != null && control.child_controls.Count > 0)))
			{
				control = Control.FindFlatBackward(control, null);
			}
			return control;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		internal virtual void HandleClick(int clicks, MouseEventArgs me)
		{
			bool style = this.GetStyle(ControlStyles.StandardClick);
			bool style2 = this.GetStyle(ControlStyles.StandardDoubleClick);
			if (clicks > 1 && style && style2)
			{
				this.OnDoubleClick(me);
				this.OnMouseDoubleClick(me);
				return;
			}
			if (clicks == 1 && style && !this.ValidationFailed)
			{
				this.OnClick(me);
				this.OnMouseClick(me);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000AA04 File Offset: 0x00008C04
		internal void CaptureWithConfine(Control ConfineWindow)
		{
			if (this.IsHandleCreated && !this.is_captured)
			{
				this.is_captured = true;
				XplatUI.GrabWindow(this.window.Handle, ConfineWindow.Handle);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000AA34 File Offset: 0x00008C34
		private void CheckDataBindings()
		{
			if (this.data_bindings == null)
			{
				return;
			}
			foreach (object obj in this.data_bindings)
			{
				((Binding)obj).Check();
			}
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000AA94 File Offset: 0x00008C94
		private void ChangeParent(Control new_parent)
		{
			bool enabled = this.Enabled;
			bool visible = this.Visible;
			Font font = this.Font;
			Color foreColor = this.ForeColor;
			Color backColor = this.BackColor;
			RightToLeft rightToLeft = this.RightToLeft;
			this.parent = new_parent;
			Form form = this as Form;
			if (form != null)
			{
				form.ChangingParent(new_parent);
			}
			else if (this.IsHandleCreated)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (new_parent != null && new_parent.IsHandleCreated)
				{
					intPtr = new_parent.Handle;
				}
				XplatUI.SetParent(this.Handle, intPtr);
			}
			this.OnParentChanged(EventArgs.Empty);
			if (enabled != this.Enabled)
			{
				this.OnEnabledChanged(EventArgs.Empty);
			}
			if (visible != this.Visible)
			{
				this.OnVisibleChanged(EventArgs.Empty);
			}
			if (font != this.Font)
			{
				this.OnFontChanged(EventArgs.Empty);
			}
			if (foreColor != this.ForeColor)
			{
				this.OnForeColorChanged(EventArgs.Empty);
			}
			if (backColor != this.BackColor)
			{
				this.OnBackColorChanged(EventArgs.Empty);
			}
			if (rightToLeft != this.RightToLeft)
			{
				this.OnRightToLeftChanged(EventArgs.Empty);
			}
			if (new_parent != null && new_parent.Created && this.is_visible && !this.Created)
			{
				this.CreateControl();
			}
			if (this.binding_context == null && this.Created)
			{
				this.OnBindingContextChanged(EventArgs.Empty);
			}
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000ABE4 File Offset: 0x00008DE4
		internal Size InternalSizeFromClientSize(Size clientSize)
		{
			Rectangle rectangle = new Rectangle(0, 0, clientSize.Width, clientSize.Height);
			CreateParams createParams = this.CreateParams;
			Rectangle rectangle2;
			if (XplatUI.CalculateWindowRect(ref rectangle, createParams, null, out rectangle2))
			{
				return new Size(rectangle2.Width, rectangle2.Height);
			}
			return Size.Empty;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000AC38 File Offset: 0x00008E38
		internal Size ClientSizeFromSize(Size size)
		{
			Size size2 = this.InternalSizeFromClientSize(size);
			if (size2 == Size.Empty)
			{
				return Size.Empty;
			}
			return new Size(size.Width - (size2.Width - size.Width), size.Height - (size2.Height - size.Height));
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000AC93 File Offset: 0x00008E93
		internal CreateParams GetCreateParams()
		{
			return this.CreateParams;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000AC9B File Offset: 0x00008E9B
		internal virtual Size GetPreferredSizeCore(Size proposedSize)
		{
			return this.explicit_bounds.Size;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000ACA8 File Offset: 0x00008EA8
		private void UpdateDistances()
		{
			if (this.parent != null)
			{
				if (this.bounds.Width >= 0)
				{
					this.dist_right = this.parent.ClientSize.Width - this.bounds.X - this.bounds.Width;
				}
				if (this.bounds.Height >= 0)
				{
					this.dist_bottom = this.parent.ClientSize.Height - this.bounds.Y - this.bounds.Height;
				}
				this.recalculate_distances = false;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000AD45 File Offset: 0x00008F45
		private Cursor GetAvailableCursor()
		{
			if (this.Cursor != null && this.Enabled)
			{
				return this.Cursor;
			}
			if (this.Parent != null)
			{
				return this.Parent.GetAvailableCursor();
			}
			return Cursors.Default;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000AD80 File Offset: 0x00008F80
		private void UpdateCursor()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			if (!this.Enabled)
			{
				XplatUI.SetCursor(this.window.Handle, this.GetAvailableCursor().handle);
				return;
			}
			Point point = this.PointToClient(Cursor.Position);
			if (!this.bounds.Contains(point) && !this.Capture)
			{
				return;
			}
			if (this.cursor != null || this.use_wait_cursor)
			{
				XplatUI.SetCursor(this.window.Handle, this.Cursor.handle);
				return;
			}
			XplatUI.SetCursor(this.window.Handle, this.GetAvailableCursor().handle);
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000AE2A File Offset: 0x0000902A
		private bool UseDoubleBuffering
		{
			get
			{
				return ThemeEngine.Current.DoubleBufferingSupported && (this.force_double_buffer || this.DoubleBuffered || (this.control_style & ControlStyles.DoubleBuffer) > (ControlStyles)0);
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000AE60 File Offset: 0x00009060
		internal void OnSizeInitializedOrChanged()
		{
			Form form = this as Form;
			if (form != null && form.WindowManager != null)
			{
				ThemeEngine.Current.ManagedWindowOnSizeInitializedOrChanged(form);
			}
		}

		/// <summary>Gets the default background color of the control.</summary>
		/// <returns>The default background <see cref="T:System.Drawing.Color" /> of the control. The default is <see cref="P:System.Drawing.SystemColors.Control" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000AE8A File Offset: 0x0000908A
		public static Color DefaultBackColor
		{
			get
			{
				return ThemeEngine.Current.DefaultControlBackColor;
			}
		}

		/// <summary>Gets the default font of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Font" /> of the control. The value returned will vary depending on the user's operating system the local culture setting of their system.</returns>
		/// <exception cref="T:System.ArgumentException">The default font or the regional alternative fonts are not installed on the client computer. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000AE96 File Offset: 0x00009096
		public static Font DefaultFont
		{
			get
			{
				return ThemeEngine.Current.DefaultFont;
			}
		}

		/// <summary>Gets the default foreground color of the control.</summary>
		/// <returns>The default foreground <see cref="T:System.Drawing.Color" /> of the control. The default is <see cref="P:System.Drawing.SystemColors.ControlText" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600029A RID: 666 RVA: 0x0000AEA2 File Offset: 0x000090A2
		public static Color DefaultForeColor
		{
			get
			{
				return ThemeEngine.Current.DefaultControlForeColor;
			}
		}

		/// <summary>Gets a value indicating which of the modifier keys (SHIFT, CTRL, and ALT) is in a pressed state.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.Keys" /> values. The default is <see cref="F:System.Windows.Forms.Keys.None" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000AEAE File Offset: 0x000090AE
		public static Keys ModifierKeys
		{
			get
			{
				return XplatUI.State.ModifierKeys;
			}
		}

		/// <summary>Gets the position of the mouse cursor in screen coordinates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that contains the coordinates of the mouse cursor relative to the upper-left corner of the screen.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000AEB5 File Offset: 0x000090B5
		public static Point MousePosition
		{
			get
			{
				return Cursor.Position;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control can accept data that the user drags onto it.</summary>
		/// <returns>true if drag-and-drop operations are allowed in the control; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000AEBC File Offset: 0x000090BC
		[DefaultValue(false)]
		[MWFCategory("Behavior")]
		public virtual bool AllowDrop
		{
			get
			{
				return this.allow_drop;
			}
		}

		/// <summary>Gets or sets the edges of the container to which a control is bound and determines how a control is resized with its parent. </summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values. The default is Top and Left.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000AEC4 File Offset: 0x000090C4
		// (set) Token: 0x0600029F RID: 671 RVA: 0x0000AECC File Offset: 0x000090CC
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(AnchorStyles.Top | AnchorStyles.Left)]
		[MWFCategory("Layout")]
		public virtual AnchorStyles Anchor
		{
			get
			{
				return this.anchor_style;
			}
			set
			{
				this.layout_type = Control.LayoutType.Anchor;
				if (this.anchor_style == value)
				{
					return;
				}
				this.anchor_style = value;
				this.dock_style = DockStyle.None;
				this.UpdateDistances();
				if (this.parent != null)
				{
					this.parent.PerformLayout(this, "Anchor");
				}
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000AF0C File Offset: 0x0000910C
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x0000AF14 File Offset: 0x00009114
		[RefreshProperties(RefreshProperties.All)]
		[Localizable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(false)]
		public virtual bool AutoSize
		{
			get
			{
				return this.auto_size;
			}
			set
			{
				if (this.auto_size != value)
				{
					this.auto_size = value;
					if (!value)
					{
						this.Size = this.explicit_bounds.Size;
					}
					else if (this.Parent != null)
					{
						this.Parent.PerformLayout(this, "AutoSize");
					}
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the size that is the upper limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000AF6B File Offset: 0x0000916B
		[AmbientValue("{Width=0, Height=0}")]
		[MWFCategory("Layout")]
		public virtual Size MaximumSize
		{
			get
			{
				return this.maximum_size;
			}
		}

		/// <summary>Gets or sets the size that is the lower limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000AF73 File Offset: 0x00009173
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000AF7B File Offset: 0x0000917B
		[MWFCategory("Layout")]
		public virtual Size MinimumSize
		{
			get
			{
				return this.minimum_size;
			}
			set
			{
				if (this.minimum_size != value)
				{
					this.minimum_size = value;
					this.Size = this.PreferredSize;
				}
			}
		}

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000AFA0 File Offset: 0x000091A0
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000AFF8 File Offset: 0x000091F8
		[DispId(-501)]
		[MWFCategory("Appearance")]
		public virtual Color BackColor
		{
			get
			{
				if (this.background_color.IsEmpty)
				{
					if (this.parent != null)
					{
						Color backColor = this.parent.BackColor;
						if (backColor.A == 255 || this.GetStyle(ControlStyles.SupportsTransparentBackColor))
						{
							return backColor;
						}
					}
					return Control.DefaultBackColor;
				}
				return this.background_color;
			}
			set
			{
				if (!value.IsEmpty && value.A != 255 && !this.GetStyle(ControlStyles.SupportsTransparentBackColor))
				{
					throw new ArgumentException("Transparent background colors are not supported on this control");
				}
				if (this.background_color != value)
				{
					this.background_color = value;
					Control.SetChildColor(this);
					this.OnBackColorChanged(EventArgs.Empty);
					this.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the background image displayed in the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B060 File Offset: 0x00009260
		[Localizable(true)]
		[DefaultValue(null)]
		[MWFCategory("Appearance")]
		public virtual Image BackgroundImage
		{
			get
			{
				return this.background_image;
			}
		}

		/// <summary>Gets or sets the background image layout as defined in the <see cref="T:System.Windows.Forms.ImageLayout" /> enumeration.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" /> (<see cref="F:System.Windows.Forms.ImageLayout.Center" /> , <see cref="F:System.Windows.Forms.ImageLayout.None" />, <see cref="F:System.Windows.Forms.ImageLayout.Stretch" />, <see cref="F:System.Windows.Forms.ImageLayout.Tile" />, or <see cref="F:System.Windows.Forms.ImageLayout.Zoom" />). <see cref="F:System.Windows.Forms.ImageLayout.Tile" /> is the default value.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified enumeration value does not exist. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x0000B068 File Offset: 0x00009268
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x0000B070 File Offset: 0x00009270
		[DefaultValue(ImageLayout.Tile)]
		[Localizable(true)]
		[MWFCategory("Appearance")]
		public virtual ImageLayout BackgroundImageLayout
		{
			get
			{
				return this.backgroundimage_layout;
			}
			set
			{
				if (Array.IndexOf(Enum.GetValues(typeof(ImageLayout)), value) == -1)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ImageLayout));
				}
				if (value != this.backgroundimage_layout)
				{
					this.backgroundimage_layout = value;
					this.Invalidate();
					this.OnBackgroundImageLayoutChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002AA RID: 682 RVA: 0x0000B0D1 File Offset: 0x000092D1
		// (set) Token: 0x060002AB RID: 683 RVA: 0x0000B103 File Offset: 0x00009303
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual BindingContext BindingContext
		{
			get
			{
				if (this.binding_context != null)
				{
					return this.binding_context;
				}
				if (this.Parent == null)
				{
					return null;
				}
				this.binding_context = this.Parent.BindingContext;
				return this.binding_context;
			}
			set
			{
				if (this.binding_context != value)
				{
					this.binding_context = value;
					this.OnBindingContextChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000B120 File Offset: 0x00009320
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Bottom
		{
			get
			{
				return this.bounds.Bottom;
			}
		}

		/// <summary>Gets or sets the size and location of the control including its nonclient elements, in pixels, relative to the parent control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> in pixels relative to the parent control that represents the size and location of the control including its nonclient elements.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B12D File Offset: 0x0000932D
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000B135 File Offset: 0x00009335
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
			set
			{
				this.SetBounds(value.Left, value.Top, value.Width, value.Height, BoundsSpecified.All);
			}
		}

		/// <summary>Gets a value indicating whether the control can receive focus.</summary>
		/// <returns>true if the control can receive focus; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000B15B File Offset: 0x0000935B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanFocus
		{
			get
			{
				return this.IsHandleCreated && this.Visible && this.Enabled;
			}
		}

		/// <summary>Gets a value indicating whether the control can be selected.</summary>
		/// <returns>true if the control can be selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x0000B178 File Offset: 0x00009378
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanSelect
		{
			get
			{
				if (!this.GetStyle(ControlStyles.Selectable))
				{
					return false;
				}
				for (Control control = this; control != null; control = control.parent)
				{
					if (!control.is_visible || !control.is_enabled)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00006947 File Offset: 0x00004B47
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000B1B5 File Offset: 0x000093B5
		internal virtual bool InternalCapture
		{
			get
			{
				return this.Capture;
			}
			set
			{
				this.Capture = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control has captured the mouse.</summary>
		/// <returns>true if the control has captured the mouse; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000B1BE File Offset: 0x000093BE
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000B1C6 File Offset: 0x000093C6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Capture
		{
			get
			{
				return this.is_captured;
			}
			set
			{
				if (value != this.is_captured)
				{
					if (value)
					{
						this.is_captured = true;
						XplatUI.GrabWindow(this.Handle, IntPtr.Zero);
						return;
					}
					if (this.IsHandleCreated)
					{
						XplatUI.UngrabWindow(this.Handle);
					}
					this.is_captured = false;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control causes validation to be performed on any controls that require validation when it receives focus.</summary>
		/// <returns>true if the control causes validation to be performed on any controls requiring validation when it receives focus; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000B206 File Offset: 0x00009406
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000B20E File Offset: 0x0000940E
		[DefaultValue(true)]
		[MWFCategory("Focus")]
		public bool CausesValidation
		{
			get
			{
				return this.causes_validation;
			}
			set
			{
				if (this.causes_validation != value)
				{
					this.causes_validation = value;
					this.OnCausesValidationChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the rectangle that represents the client area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the client area of the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000B22B File Offset: 0x0000942B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Rectangle ClientRectangle
		{
			get
			{
				this.client_rect.Width = this.client_size.Width;
				this.client_rect.Height = this.client_size.Height;
				return this.client_rect;
			}
		}

		/// <summary>Gets or sets the height and width of the client area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the dimensions of the client area of the control.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000B25F File Offset: 0x0000945F
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000B267 File Offset: 0x00009467
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Size ClientSize
		{
			get
			{
				return this.client_size;
			}
			set
			{
				this.SetClientSizeCore(value.Width, value.Height);
				this.OnClientSizeChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the shortcut menu associated with the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenu" /> that represents the shortcut menu associated with the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000B288 File Offset: 0x00009488
		// (set) Token: 0x060002BB RID: 699 RVA: 0x0000B290 File Offset: 0x00009490
		[Browsable(false)]
		[DefaultValue(null)]
		[MWFCategory("Behavior")]
		public virtual ContextMenu ContextMenu
		{
			get
			{
				return this.ContextMenuInternal;
			}
			set
			{
				this.ContextMenuInternal = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0000B299 File Offset: 0x00009499
		// (set) Token: 0x060002BD RID: 701 RVA: 0x0000B2A1 File Offset: 0x000094A1
		internal virtual ContextMenu ContextMenuInternal
		{
			get
			{
				return this.context_menu;
			}
			set
			{
				if (this.context_menu != value)
				{
					this.context_menu = value;
					this.OnContextMenuChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the collection of controls contained within the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control.ControlCollection" /> representing the collection of controls contained within the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002BE RID: 702 RVA: 0x0000B2BE File Offset: 0x000094BE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Control.ControlCollection Controls
		{
			get
			{
				return this.child_controls;
			}
		}

		/// <summary>Gets a value indicating whether the control has been created.</summary>
		/// <returns>true if the control has been created; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000B2C6 File Offset: 0x000094C6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Created
		{
			get
			{
				return !this.is_disposed && this.is_created;
			}
		}

		/// <summary>Gets or sets the cursor that is displayed when the mouse pointer is over the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor to display when the mouse pointer is over the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x0000B2D8 File Offset: 0x000094D8
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x0000B316 File Offset: 0x00009516
		[AmbientValue(null)]
		[MWFCategory("Appearance")]
		public virtual Cursor Cursor
		{
			get
			{
				if (this.use_wait_cursor)
				{
					return Cursors.WaitCursor;
				}
				if (this.cursor != null)
				{
					return this.cursor;
				}
				if (this.parent != null)
				{
					return this.parent.Cursor;
				}
				return Cursors.Default;
			}
			set
			{
				if (this.cursor == value)
				{
					return;
				}
				this.cursor = value;
				this.UpdateCursor();
				this.OnCursorChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the rectangle that represents the display area of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x0000B33F File Offset: 0x0000953F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual Rectangle DisplayRectangle
		{
			get
			{
				return this.ClientRectangle;
			}
		}

		/// <summary>Gets or sets which control borders are docked to its parent control and determines how a control is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DockStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000B347 File Offset: 0x00009547
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000B350 File Offset: 0x00009550
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(DockStyle.None)]
		[MWFCategory("Layout")]
		public virtual DockStyle Dock
		{
			get
			{
				return this.dock_style;
			}
			set
			{
				if (value != DockStyle.None)
				{
					this.layout_type = Control.LayoutType.Dock;
				}
				if (this.dock_style == value)
				{
					return;
				}
				if (!Enum.IsDefined(typeof(DockStyle), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DockStyle));
				}
				this.dock_style = value;
				this.anchor_style = AnchorStyles.Top | AnchorStyles.Left;
				if (this.dock_style == DockStyle.None)
				{
					this.bounds = this.explicit_bounds;
					this.layout_type = Control.LayoutType.Anchor;
				}
				if (this.parent != null)
				{
					this.parent.PerformLayout(this, "Dock");
				}
				else if (this.Controls.Count > 0)
				{
					this.PerformLayout();
				}
				this.OnDockChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker.</summary>
		/// <returns>true if the surface of the control should be drawn using double buffering; otherwise, false.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000B401 File Offset: 0x00009601
		protected virtual bool DoubleBuffered
		{
			get
			{
				return (this.control_style & ControlStyles.OptimizedDoubleBuffer) > (ControlStyles)0;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control can respond to user interaction.</summary>
		/// <returns>true if the control can respond to user interaction; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x0000B412 File Offset: 0x00009612
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0000B434 File Offset: 0x00009634
		[DispId(-514)]
		[Localizable(true)]
		[MWFCategory("Behavior")]
		public bool Enabled
		{
			get
			{
				return this.is_enabled && (this.parent == null || this.parent.Enabled);
			}
			set
			{
				if (this.is_enabled == value)
				{
					return;
				}
				bool flag = this.is_enabled;
				this.is_enabled = value;
				if (!value)
				{
					this.UpdateCursor();
				}
				if (flag != value && !value && this.has_focus)
				{
					this.SelectNextControl(this, true, true, true, true);
				}
				this.OnEnabledChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets a value indicating whether the control has input focus.</summary>
		/// <returns>true if the control has focus; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000B486 File Offset: 0x00009686
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Focused
		{
			get
			{
				return this.has_focus;
			}
		}

		/// <summary>Gets or sets the font of the text displayed by the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000B490 File Offset: 0x00009690
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000B4CA File Offset: 0x000096CA
		[DispId(-512)]
		[AmbientValue(null)]
		[Localizable(true)]
		[MWFCategory("Appearance")]
		public virtual Font Font
		{
			[return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.Font)]
			get
			{
				if (this.font != null)
				{
					return this.font;
				}
				if (this.parent != null)
				{
					Font font = this.parent.Font;
					if (font != null)
					{
						return font;
					}
				}
				return Control.DefaultFont;
			}
			[param: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = System.Drawing.Font)]
			set
			{
				if (this.font != null && this.font == value)
				{
					return;
				}
				this.font = value;
				this.Invalidate();
				this.OnFontChanged(EventArgs.Empty);
				this.PerformLayout();
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000B4FC File Offset: 0x000096FC
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000B52B File Offset: 0x0000972B
		[DispId(-513)]
		[MWFCategory("Appearance")]
		public virtual Color ForeColor
		{
			get
			{
				if (!this.foreground_color.IsEmpty)
				{
					return this.foreground_color;
				}
				if (this.parent != null)
				{
					return this.parent.ForeColor;
				}
				return Control.DefaultForeColor;
			}
			set
			{
				if (this.foreground_color != value)
				{
					this.foreground_color = value;
					this.Invalidate();
					this.OnForeColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the window handle that the control is bound to.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that contains the window handle (HWND) of the control.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000B553 File Offset: 0x00009753
		[DispId(-515)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IntPtr Handle
		{
			get
			{
				if (Control.verify_thread_handle && this.InvokeRequired)
				{
					throw new InvalidOperationException("Cross-thread access of handle detected. Handle access only valid on thread that created the control");
				}
				if (!this.IsHandleCreated)
				{
					this.CreateHandle();
				}
				return this.window.Handle;
			}
		}

		/// <summary>Gets or sets the height of the control.</summary>
		/// <returns>The height of the control in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000B588 File Offset: 0x00009788
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000B595 File Offset: 0x00009795
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Height
		{
			get
			{
				return this.bounds.Height;
			}
			set
			{
				this.SetBounds(this.bounds.X, this.bounds.Y, this.bounds.Width, value, BoundsSpecified.Height);
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode of the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values. The default is <see cref="F:System.Windows.Forms.ImeMode.Inherit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ImeMode" /> enumeration values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000CD RID: 205
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000B5C0 File Offset: 0x000097C0
		[AmbientValue(ImeMode.Inherit)]
		[Localizable(true)]
		[MWFCategory("Behavior")]
		public ImeMode ImeMode
		{
			set
			{
				if (this.ime_mode != value)
				{
					this.ime_mode = value;
					this.OnImeModeChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets a value indicating whether the caller must call an invoke method when making method calls to the control because the caller is on a different thread than the one the control was created on.</summary>
		/// <returns>true if the control's <see cref="P:System.Windows.Forms.Control.Handle" /> was created on a different thread than the calling thread (indicating that you must make calls to the control through an invoke method); otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000B5DD File Offset: 0x000097DD
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool InvokeRequired
		{
			get
			{
				return this.creator_thread != null && this.creator_thread != Thread.CurrentThread;
			}
		}

		/// <summary>Gets a value indicating whether the control has been disposed of.</summary>
		/// <returns>true if the control has been disposed of; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000B5F7 File Offset: 0x000097F7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsDisposed
		{
			get
			{
				return this.is_disposed;
			}
		}

		/// <summary>Gets a value indicating whether the control has a handle associated with it.</summary>
		/// <returns>true if a handle has been assigned to the control; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000B600 File Offset: 0x00009800
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsHandleCreated
		{
			get
			{
				if (this.window == null || this.window.Handle == IntPtr.Zero)
				{
					return false;
				}
				Hwnd hwnd = Hwnd.ObjectFromHandle(this.window.Handle);
				return hwnd == null || !hwnd.zombie;
			}
		}

		/// <summary>Gets a cached instance of the control's layout engine.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> for the control's contents.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000B64D File Offset: 0x0000984D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual LayoutEngine LayoutEngine
		{
			get
			{
				if (this.layout_engine == null)
				{
					this.layout_engine = new DefaultLayout();
				}
				return this.layout_engine;
			}
		}

		/// <summary>Gets or sets the distance, in pixels, between the left edge of the control and the left edge of its container's client area.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the left edge of the control and the left edge of its container's client area.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000B668 File Offset: 0x00009868
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000B675 File Offset: 0x00009875
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Left
		{
			get
			{
				return this.bounds.Left;
			}
			set
			{
				this.SetBounds(value, this.bounds.Y, this.bounds.Width, this.bounds.Height, BoundsSpecified.X);
			}
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the control relative to the upper-left corner of its container.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000B6A0 File Offset: 0x000098A0
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000B6AD File Offset: 0x000098AD
		[Localizable(true)]
		[MWFCategory("Layout")]
		public Point Location
		{
			get
			{
				return this.bounds.Location;
			}
			set
			{
				this.SetBounds(value.X, value.Y, this.bounds.Width, this.bounds.Height, BoundsSpecified.Location);
			}
		}

		/// <summary>Gets or sets the space between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the space between controls.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000B6DA File Offset: 0x000098DA
		[Localizable(true)]
		[MWFCategory("Layout")]
		public Padding Margin
		{
			get
			{
				return this.margin;
			}
		}

		/// <summary>Gets or sets the name of the control.</summary>
		/// <returns>The name of the control. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000B6E2 File Offset: 0x000098E2
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000B6EA File Offset: 0x000098EA
		[Browsable(false)]
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets padding within the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the control's internal spacing characteristics.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000B6F3 File Offset: 0x000098F3
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000B6FC File Offset: 0x000098FC
		[Localizable(true)]
		[MWFCategory("Layout")]
		public Padding Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				if (this.padding != value)
				{
					this.padding = value;
					this.OnPaddingChanged(EventArgs.Empty);
					if (this.AutoSize && this.Parent != null)
					{
						this.parent.PerformLayout(this, "Padding");
						return;
					}
					this.PerformLayout(this, "Padding");
				}
			}
		}

		/// <summary>Gets or sets the parent container of the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the parent or container control of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000B757 File Offset: 0x00009957
		// (set) Token: 0x060002DF RID: 735 RVA: 0x0000B760 File Offset: 0x00009960
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				if (value == this)
				{
					throw new ArgumentException("A circular control reference has been made. A control cannot be owned or parented to itself.");
				}
				if (this.parent != value)
				{
					if (value == null)
					{
						this.parent.Controls.Remove(this);
						this.parent = null;
						return;
					}
					value.Controls.Add(this);
				}
			}
		}

		/// <summary>Gets the size of a rectangular area into which the control can fit.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> containing the height and width, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000B7AD File Offset: 0x000099AD
		[Browsable(false)]
		public Size PreferredSize
		{
			get
			{
				return this.GetPreferredSize(Size.Empty);
			}
		}

		/// <summary>Gets a value indicating whether the control is currently re-creating its handle.</summary>
		/// <returns>true if the control is currently re-creating its handle; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00009E35 File Offset: 0x00008035
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool RecreatingHandle
		{
			get
			{
				return this.is_recreating;
			}
		}

		/// <summary>Gets or sets the window region associated with the control.</summary>
		/// <returns>The window <see cref="T:System.Drawing.Region" /> associated with the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000DA RID: 218
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000B7BA File Offset: 0x000099BA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Region Region
		{
			set
			{
				if (this.clip_region != value)
				{
					if (this.IsHandleCreated)
					{
						XplatUI.SetClipRegion(this.Handle, value);
					}
					this.clip_region = value;
					this.OnRegionChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the distance, in pixels, between the right edge of the control and the left edge of its container's client area.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the right edge of the control and the left edge of its container's client area.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000B7EB File Offset: 0x000099EB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Right
		{
			get
			{
				return this.bounds.Right;
			}
		}

		/// <summary>Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.RightToLeft" /> values. The default is <see cref="F:System.Windows.Forms.RightToLeft.Inherit" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.RightToLeft" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000B7F8 File Offset: 0x000099F8
		[AmbientValue(RightToLeft.Inherit)]
		[Localizable(true)]
		[MWFCategory("Appearance")]
		public virtual RightToLeft RightToLeft
		{
			get
			{
				if (this.right_to_left != RightToLeft.Inherit)
				{
					return this.right_to_left;
				}
				if (this.parent != null)
				{
					return this.parent.RightToLeft;
				}
				return RightToLeft.No;
			}
		}

		/// <summary>Gets or sets the site of the control.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the <see cref="T:System.Windows.Forms.Control" />, if any.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000B81F File Offset: 0x00009A1F
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000B828 File Offset: 0x00009A28
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override ISite Site
		{
			get
			{
				return base.Site;
			}
			set
			{
				base.Site = value;
				if (value != null)
				{
					AmbientProperties ambientProperties = (AmbientProperties)value.GetService(typeof(AmbientProperties));
					if (ambientProperties != null)
					{
						this.BackColor = ambientProperties.BackColor;
						this.ForeColor = ambientProperties.ForeColor;
						this.Cursor = ambientProperties.Cursor;
						this.Font = ambientProperties.Font;
					}
				}
			}
		}

		/// <summary>Gets or sets the height and width of the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> that represents the height and width of the control in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000B888 File Offset: 0x00009A88
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000B89B File Offset: 0x00009A9B
		[Localizable(true)]
		[MWFCategory("Layout")]
		public Size Size
		{
			get
			{
				return new Size(this.Width, this.Height);
			}
			set
			{
				this.SetBounds(this.bounds.X, this.bounds.Y, value.Width, value.Height, BoundsSpecified.Size);
			}
		}

		/// <summary>Gets or sets the tab order of the control within its container.</summary>
		/// <returns>The index value of the control within the set of controls within its container. The controls in the container are included in the tab order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000DF RID: 223
		// (set) Token: 0x060002E9 RID: 745 RVA: 0x0000B8C9 File Offset: 0x00009AC9
		[Localizable(true)]
		[MergableProperty(false)]
		[MWFCategory("Behavior")]
		public int TabIndex
		{
			set
			{
				if (this.tab_index != value)
				{
					this.tab_index = value;
					this.OnTabIndexChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this control using the TAB key.</summary>
		/// <returns>true if the user can give the focus to the control using the TAB key; otherwise, false. The default is true.NoteThis property will always return true for an instance of the <see cref="T:System.Windows.Forms.Form" /> class.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002EA RID: 746 RVA: 0x0000B8E6 File Offset: 0x00009AE6
		// (set) Token: 0x060002EB RID: 747 RVA: 0x0000B8EE File Offset: 0x00009AEE
		[DispId(-516)]
		[DefaultValue(true)]
		[MWFCategory("Behavior")]
		public bool TabStop
		{
			get
			{
				return this.tab_stop;
			}
			set
			{
				if (this.tab_stop != value)
				{
					this.tab_stop = value;
					this.OnTabStopChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000B90B File Offset: 0x00009B0B
		// (set) Token: 0x060002ED RID: 749 RVA: 0x0000B914 File Offset: 0x00009B14
		[DispId(-517)]
		[Localizable(true)]
		[Bindable(true)]
		[MWFCategory("Appearance")]
		public virtual string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				if (this.text != value)
				{
					this.text = value;
					this.UpdateWindowText();
					this.OnTextChanged(EventArgs.Empty);
					if (this.AutoSize && this.Parent != null && !(this is Label))
					{
						this.Parent.PerformLayout(this, "Text");
					}
				}
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000B97A File Offset: 0x00009B7A
		internal virtual void UpdateWindowText()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			XplatUI.Text(this.Handle, this.text);
		}

		/// <summary>Gets or sets the distance, in pixels, between the top edge of the control and the top edge of its container's client area.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the distance, in pixels, between the bottom edge of the control and the top edge of its container's client area.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000B997 File Offset: 0x00009B97
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Top
		{
			get
			{
				return this.bounds.Top;
			}
			set
			{
				this.SetBounds(this.bounds.X, value, this.bounds.Width, this.bounds.Height, BoundsSpecified.Y);
			}
		}

		/// <summary>Gets the parent control that is not parented by another Windows Forms control. Typically, this is the outermost <see cref="T:System.Windows.Forms.Form" /> that the control is contained in.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that represents the top-level control that contains the current control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Control TopLevelControl
		{
			get
			{
				Control control = this;
				while (control.parent != null)
				{
					control = control.parent;
				}
				if (!(control is Form))
				{
					return null;
				}
				return control;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control and all its child controls are displayed.</summary>
		/// <returns>true if the control and all its child controls are displayed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000B9FB File Offset: 0x00009BFB
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x0000BA1C File Offset: 0x00009C1C
		[Localizable(true)]
		[MWFCategory("Behavior")]
		public bool Visible
		{
			get
			{
				return this.is_visible && (this.parent == null || this.parent.Visible);
			}
			set
			{
				if (this.is_visible != value)
				{
					this.SetVisibleCore(value);
					if (this.parent != null)
					{
						this.parent.PerformLayout(this, "Visible");
					}
				}
			}
		}

		/// <summary>Gets or sets the width of the control.</summary>
		/// <returns>The width of the control in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000BA47 File Offset: 0x00009C47
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000BA54 File Offset: 0x00009C54
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Width
		{
			get
			{
				return this.bounds.Width;
			}
			set
			{
				this.SetBounds(this.bounds.X, this.bounds.Y, value, this.bounds.Height, BoundsSpecified.Width);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The NativeWindow contained within the control.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000BA7F File Offset: 0x00009C7F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IWindowTarget WindowTarget
		{
			get
			{
				return this.window_target;
			}
		}

		/// <summary>Determines if events can be raised on the control.</summary>
		/// <returns>true if the control is hosted as an ActiveX control whose events are not frozen; otherwise, false.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00006F54 File Offset: 0x00005154
		protected override bool CanRaiseEvents
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000BA88 File Offset: 0x00009C88
		protected virtual CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = new CreateParams();
				try
				{
					createParams.Caption = this.Text;
				}
				catch
				{
					createParams.Caption = this.text;
				}
				try
				{
					createParams.X = this.Left;
				}
				catch
				{
					createParams.X = this.bounds.X;
				}
				try
				{
					createParams.Y = this.Top;
				}
				catch
				{
					createParams.Y = this.bounds.Y;
				}
				try
				{
					createParams.Width = this.Width;
				}
				catch
				{
					createParams.Width = this.bounds.Width;
				}
				try
				{
					createParams.Height = this.Height;
				}
				catch
				{
					createParams.Height = this.bounds.Height;
				}
				createParams.ClassName = XplatUI.GetDefaultClassName(base.GetType());
				createParams.ClassStyle = 40;
				createParams.ExStyle = 0;
				createParams.Param = 0;
				if (this.allow_drop)
				{
					createParams.ExStyle |= 16;
				}
				if (this.parent != null && this.parent.IsHandleCreated)
				{
					createParams.Parent = this.parent.Handle;
				}
				createParams.Style = 1174405120;
				if (this.is_visible)
				{
					createParams.Style |= 268435456;
				}
				if (!this.is_enabled)
				{
					createParams.Style |= 134217728;
				}
				BorderStyle borderStyle = this.border_style;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				createParams.control = this;
				return createParams;
			}
		}

		/// <summary>Gets or sets the default cursor for the control.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.Cursor" /> representing the current default cursor.</returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000BC6C File Offset: 0x00009E6C
		protected virtual Cursor DefaultCursor
		{
			get
			{
				return Cursors.Default;
			}
		}

		/// <summary>Gets the space, in pixels, that is specified by default between controls.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the default space between controls.</returns>
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000BC73 File Offset: 0x00009E73
		protected virtual Padding DefaultMargin
		{
			get
			{
				return new Padding(3);
			}
		}

		/// <summary>Gets the internal spacing, in pixels, of the contents of a control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the internal spacing of the contents of a control.</returns>
		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000BC7C File Offset: 0x00009E7C
		protected virtual Padding DefaultPadding
		{
			get
			{
				return default(Padding);
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000BC92 File Offset: 0x00009E92
		protected virtual Size DefaultSize
		{
			get
			{
				return new Size(0, 0);
			}
		}

		/// <summary>Gets or sets the height of the font of the control.</summary>
		/// <returns>The height of the <see cref="T:System.Drawing.Font" /> of the control in pixels.</returns>
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000BC9B File Offset: 0x00009E9B
		protected int FontHeight
		{
			get
			{
				return this.Font.Height;
			}
		}

		/// <summary>Gets a value that determines the scaling of child controls. </summary>
		/// <returns>true if child controls will be scaled when the <see cref="M:System.Windows.Forms.Control.Scale(System.Single)" /> method on this control is called; otherwise, false. The default is true.</returns>
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000BCA8 File Offset: 0x00009EA8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual bool ScaleChildren
		{
			get
			{
				return this.ScaleChildrenInternal;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00006F54 File Offset: 0x00005154
		internal virtual bool ScaleChildrenInternal
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the control should display focus rectangles.</summary>
		/// <returns>true if the control should display focus rectangles; otherwise, false.</returns>
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected internal virtual bool ShowFocusCues
		{
			get
			{
				if (this is Form)
				{
					return this.show_focus_cues;
				}
				if (this.parent == null)
				{
					return false;
				}
				Form form = this.FindForm();
				return form != null && form.show_focus_cues;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000BCE8 File Offset: 0x00009EE8
		internal bool ShowKeyboardCuesInternal
		{
			get
			{
				return SystemInformation.MenuAccessKeysUnderlined || base.DesignMode || this.show_keyboard_cues;
			}
		}

		/// <summary>Returns the control that is currently associated with the specified handle.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> that represents the control associated with the specified handle; returns null if no control with the specified handle is found.</returns>
		/// <param name="handle">The window handle (HWND) to search for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000302 RID: 770 RVA: 0x0000BD01 File Offset: 0x00009F01
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Control FromHandle(IntPtr handle)
		{
			return Control.ControlNativeWindow.ControlFromHandle(handle);
		}

		/// <summary>Determines if the specified character is the mnemonic character assigned to the control in the specified string.</summary>
		/// <returns>true if the <paramref name="charCode" /> character is the mnemonic character assigned to the control; otherwise, false.</returns>
		/// <param name="charCode">The character to test. </param>
		/// <param name="text">The string to search. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000303 RID: 771 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public static bool IsMnemonic(char charCode, string text)
		{
			int num = text.IndexOf('&');
			return num != -1 && num + 1 < text.Length && text[num + 1] != '&' && char.ToUpper(charCode) == char.ToUpper(text.ToCharArray(num + 1, 1)[0]);
		}

		/// <summary>Executes the specified delegate asynchronously with the specified arguments, on the thread that the control's underlying handle was created on.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that represents the result of the <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" /> operation.</returns>
		/// <param name="method">A delegate to a method that takes parameters of the same number and type that are contained in the <paramref name="args" /> parameter. </param>
		/// <param name="args">An array of objects to pass as arguments to the given method. This can be null if no arguments are needed. </param>
		/// <exception cref="T:System.InvalidOperationException">No appropriate window handle can be found.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000304 RID: 772 RVA: 0x0000BD5A File Offset: 0x00009F5A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IAsyncResult BeginInvoke(Delegate method, params object[] args)
		{
			return this.BeginInvokeInternal(method, args);
		}

		/// <summary>Brings the control to the front of the z-order.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000305 RID: 773 RVA: 0x0000BD64 File Offset: 0x00009F64
		public void BringToFront()
		{
			if (this.parent != null)
			{
				this.parent.child_controls.SetChildIndex(this, 0);
				return;
			}
			if (this.IsHandleCreated)
			{
				XplatUI.SetZOrder(this.Handle, IntPtr.Zero, false, false);
			}
		}

		/// <summary>Retrieves a value indicating whether the specified control is a child of the control.</summary>
		/// <returns>true if the specified control is a child of the control; otherwise, false.</returns>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> to evaluate. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000306 RID: 774 RVA: 0x0000BD9C File Offset: 0x00009F9C
		public bool Contains(Control ctl)
		{
			while (ctl != null)
			{
				ctl = ctl.parent;
				if (ctl == this)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Forces the creation of the visible control, including the creation of the handle and any visible child controls.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000307 RID: 775 RVA: 0x0000BDB4 File Offset: 0x00009FB4
		public void CreateControl()
		{
			if (this.is_created)
			{
				return;
			}
			if (this.is_disposing)
			{
				return;
			}
			if (!this.is_visible)
			{
				return;
			}
			if (this.parent != null && !this.parent.Created)
			{
				return;
			}
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			if (!this.is_created)
			{
				this.is_created = true;
				foreach (Control control in this.Controls.GetAllControls())
				{
					if (!control.Created && !control.IsDisposed)
					{
						control.CreateControl();
					}
				}
				this.OnCreateControl();
			}
		}

		/// <summary>Creates the <see cref="T:System.Drawing.Graphics" /> for the control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> for the control.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000308 RID: 776 RVA: 0x0000BE49 File Offset: 0x0000A049
		public Graphics CreateGraphics()
		{
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			return Graphics.FromHwnd(this.window.Handle);
		}

		/// <summary>Retrieves the return value of the asynchronous operation represented by the <see cref="T:System.IAsyncResult" /> passed.</summary>
		/// <returns>The <see cref="T:System.Object" /> generated by the asynchronous operation.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that represents a specific invoke asynchronous operation, returned when calling <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="asyncResult" /> parameter value is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="asyncResult" /> object was not created by a preceding call of the <see cref="M:System.Windows.Forms.Control.BeginInvoke(System.Delegate)" /> method from the same control. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000309 RID: 777 RVA: 0x0000BE69 File Offset: 0x0000A069
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object EndInvoke(IAsyncResult asyncResult)
		{
			return ((AsyncMethodResult)asyncResult).EndInvoke();
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000BE78 File Offset: 0x0000A078
		internal Control FindRootParent()
		{
			Control control = this;
			while (control.Parent != null)
			{
				control = control.Parent;
			}
			return control;
		}

		/// <summary>Retrieves the form that the control is on.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Form" /> that the control is on.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x0600030B RID: 779 RVA: 0x0000BE9C File Offset: 0x0000A09C
		public Form FindForm()
		{
			for (Control control = this; control != null; control = control.Parent)
			{
				if (control is Form)
				{
					return (Form)control;
				}
			}
			return null;
		}

		/// <summary>Sets input focus to the control.</summary>
		/// <returns>true if the input focus request was successful; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600030C RID: 780 RVA: 0x0000BEC7 File Offset: 0x0000A0C7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool Focus()
		{
			return this.FocusInternal(false);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000BED0 File Offset: 0x0000A0D0
		internal virtual bool FocusInternal(bool skip_check)
		{
			if (skip_check || (this.CanFocus && this.IsHandleCreated && !this.has_focus && !this.is_focusing))
			{
				this.is_focusing = true;
				this.Select(this);
				this.is_focusing = false;
			}
			return this.has_focus;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000BF1C File Offset: 0x0000A11C
		internal Control GetRealChildAtPoint(Point pt)
		{
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			Control[] allControls = this.child_controls.GetAllControls();
			int i = 0;
			while (i < allControls.Length)
			{
				Control control = allControls[i];
				if (control.Bounds.Contains(this.PointToClient(pt)))
				{
					Control realChildAtPoint = control.GetRealChildAtPoint(pt);
					if (realChildAtPoint == null)
					{
						return control;
					}
					return realChildAtPoint;
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		/// <summary>Returns the next <see cref="T:System.Windows.Forms.ContainerControl" /> up the control's chain of parent controls.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IContainerControl" />, that represents the parent of the <see cref="T:System.Windows.Forms.Control" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600030F RID: 783 RVA: 0x0000BF80 File Offset: 0x0000A180
		public IContainerControl GetContainerControl()
		{
			for (Control control = this; control != null; control = control.parent)
			{
				if (control is IContainerControl && (control.control_style & ControlStyles.ContainerControl) != (ControlStyles)0)
				{
					return (IContainerControl)control;
				}
			}
			return null;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
		internal ContainerControl InternalGetContainerControl()
		{
			for (Control control = this; control != null; control = control.parent)
			{
				if (control is ContainerControl && (control.control_style & ControlStyles.ContainerControl) != (ControlStyles)0)
				{
					return control as ContainerControl;
				}
			}
			return null;
		}

		/// <summary>Retrieves the next control forward or back in the tab order of child controls.</summary>
		/// <returns>The next <see cref="T:System.Windows.Forms.Control" /> in the tab order.</returns>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> to start the search with. </param>
		/// <param name="forward">true to search forward in the tab order; false to search backward. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000311 RID: 785 RVA: 0x0000BFED File Offset: 0x0000A1ED
		public Control GetNextControl(Control ctl, bool forward)
		{
			if (!this.Contains(ctl))
			{
				ctl = this;
			}
			if (forward)
			{
				ctl = Control.FindControlForward(this, ctl);
			}
			else
			{
				ctl = Control.FindControlBackward(this, ctl);
			}
			if (ctl != this)
			{
				return ctl;
			}
			return null;
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="proposedSize">The custom-sized area for a control. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000312 RID: 786 RVA: 0x0000C01C File Offset: 0x0000A21C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public virtual Size GetPreferredSize(Size proposedSize)
		{
			Size preferredSizeCore = this.GetPreferredSizeCore(proposedSize);
			if (this.maximum_size.Width != 0 && preferredSizeCore.Width > this.maximum_size.Width)
			{
				preferredSizeCore.Width = this.maximum_size.Width;
			}
			if (this.maximum_size.Height != 0 && preferredSizeCore.Height > this.maximum_size.Height)
			{
				preferredSizeCore.Height = this.maximum_size.Height;
			}
			if (this.minimum_size.Width != 0 && preferredSizeCore.Width < this.minimum_size.Width)
			{
				preferredSizeCore.Width = this.minimum_size.Width;
			}
			if (this.minimum_size.Height != 0 && preferredSizeCore.Height < this.minimum_size.Height)
			{
				preferredSizeCore.Height = this.minimum_size.Height;
			}
			return preferredSizeCore;
		}

		/// <summary>Conceals the control from the user.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000313 RID: 787 RVA: 0x0000C0FE File Offset: 0x0000A2FE
		public void Hide()
		{
			this.Visible = false;
		}

		/// <summary>Invalidates the entire surface of the control and causes the control to be redrawn.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000314 RID: 788 RVA: 0x0000C107 File Offset: 0x0000A307
		public void Invalidate()
		{
			this.Invalidate(this.ClientRectangle, false);
		}

		/// <summary>Invalidates a specific region of the control and causes a paint message to be sent to the control. Optionally, invalidates the child controls assigned to the control.</summary>
		/// <param name="invalidateChildren">true to invalidate the control's child controls; otherwise, false. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000315 RID: 789 RVA: 0x0000C116 File Offset: 0x0000A316
		public void Invalidate(bool invalidateChildren)
		{
			this.Invalidate(this.ClientRectangle, invalidateChildren);
		}

		/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control.</summary>
		/// <param name="rc">A <see cref="T:System.Drawing.Rectangle" /> that represents the region to invalidate. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000316 RID: 790 RVA: 0x0000C125 File Offset: 0x0000A325
		public void Invalidate(Rectangle rc)
		{
			this.Invalidate(rc, false);
		}

		/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control. Optionally, invalidates the child controls assigned to the control.</summary>
		/// <param name="rc">A <see cref="T:System.Drawing.Rectangle" /> that represents the region to invalidate. </param>
		/// <param name="invalidateChildren">true to invalidate the control's child controls; otherwise, false. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000317 RID: 791 RVA: 0x0000C130 File Offset: 0x0000A330
		public void Invalidate(Rectangle rc, bool invalidateChildren)
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			if (rc.IsEmpty)
			{
				rc = this.ClientRectangle;
			}
			if (rc.Width > 0 && rc.Height > 0)
			{
				this.NotifyInvalidate(rc);
				XplatUI.Invalidate(this.Handle, rc, false);
				if (invalidateChildren)
				{
					Control[] allControls = this.child_controls.GetAllControls();
					for (int i = 0; i < allControls.Length; i++)
					{
						allControls[i].Invalidate();
					}
				}
				else
				{
					foreach (object obj in this.Controls)
					{
						Control control = (Control)obj;
						if (control.BackColor.A != 255)
						{
							control.Invalidate();
						}
					}
				}
			}
			this.OnInvalidated(new InvalidateEventArgs(rc));
		}

		/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to invalidate. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000318 RID: 792 RVA: 0x0000C21C File Offset: 0x0000A41C
		public void Invalidate(Region region)
		{
			this.Invalidate(region, false);
		}

		/// <summary>Invalidates the specified region of the control (adds it to the control's update region, which is the area that will be repainted at the next paint operation), and causes a paint message to be sent to the control. Optionally, invalidates the child controls assigned to the control.</summary>
		/// <param name="region">The <see cref="T:System.Drawing.Region" /> to invalidate. </param>
		/// <param name="invalidateChildren">true to invalidate the control's child controls; otherwise, false. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000319 RID: 793 RVA: 0x0000C228 File Offset: 0x0000A428
		public void Invalidate(Region region, bool invalidateChildren)
		{
			using (Graphics graphics = this.CreateGraphics())
			{
				RectangleF rectangleF = region.GetBounds(graphics);
				this.Invalidate(new Rectangle((int)rectangleF.X, (int)rectangleF.Y, (int)rectangleF.Width, (int)rectangleF.Height), invalidateChildren);
			}
		}

		/// <summary>Executes the specified delegate, on the thread that owns the control's underlying window handle, with the specified list of arguments.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the return value from the delegate being invoked, or null if the delegate has no return value.</returns>
		/// <param name="method">A delegate to a method that takes parameters of the same number and type that are contained in the <paramref name="args" /> parameter. </param>
		/// <param name="args">An array of objects to pass as arguments to the specified method. This parameter can be null if the method takes no arguments. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600031A RID: 794 RVA: 0x0000C28C File Offset: 0x0000A48C
		public object Invoke(Delegate method, params object[] args)
		{
			Control control = this.FindControlToInvokeOn();
			if (!this.InvokeRequired)
			{
				return method.DynamicInvoke(args);
			}
			IAsyncResult asyncResult = this.BeginInvokeInternal(method, args, control);
			return this.EndInvoke(asyncResult);
		}

		/// <summary>Forces the control to apply layout logic to all its child controls.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600031B RID: 795 RVA: 0x0000C2C1 File Offset: 0x0000A4C1
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void PerformLayout()
		{
			this.PerformLayout(null, null);
		}

		/// <summary>Forces the control to apply layout logic to all its child controls.</summary>
		/// <param name="affectedControl">A <see cref="T:System.Windows.Forms.Control" /> that represents the most recently changed control. </param>
		/// <param name="affectedProperty">The name of the most recently changed property on the control. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600031C RID: 796 RVA: 0x0000C2CC File Offset: 0x0000A4CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void PerformLayout(Control affectedControl, string affectedProperty)
		{
			LayoutEventArgs layoutEventArgs = new LayoutEventArgs(affectedControl, affectedProperty);
			foreach (Control control in this.Controls.GetAllControls())
			{
				if (control.recalculate_distances)
				{
					control.UpdateDistances();
				}
			}
			if (this.layout_suspended > 0)
			{
				this.layout_pending = true;
				return;
			}
			this.layout_pending = false;
			this.layout_suspended++;
			try
			{
				this.OnLayout(layoutEventArgs);
			}
			finally
			{
				this.layout_suspended--;
			}
		}

		/// <summary>Computes the location of the specified screen point into client coordinates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the converted <see cref="T:System.Drawing.Point" />, <paramref name="p" />, in client coordinates.</returns>
		/// <param name="p">The screen coordinate <see cref="T:System.Drawing.Point" /> to convert. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600031D RID: 797 RVA: 0x0000C35C File Offset: 0x0000A55C
		public Point PointToClient(Point p)
		{
			int x = p.X;
			int y = p.Y;
			XplatUI.ScreenToClient(this.Handle, ref x, ref y);
			return new Point(x, y);
		}

		/// <summary>Computes the location of the specified client point into screen coordinates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the converted <see cref="T:System.Drawing.Point" />, <paramref name="p" />, in screen coordinates.</returns>
		/// <param name="p">The client coordinate <see cref="T:System.Drawing.Point" /> to convert. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600031E RID: 798 RVA: 0x0000C390 File Offset: 0x0000A590
		public Point PointToScreen(Point p)
		{
			int x = p.X;
			int y = p.Y;
			XplatUI.ClientToScreen(this.Handle, ref x, ref y);
			return new Point(x, y);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		internal PreProcessControlState PreProcessControlMessageInternal(ref Message msg)
		{
			switch (msg.Msg)
			{
			case 256:
			case 260:
			{
				PreviewKeyDownEventArgs previewKeyDownEventArgs = new PreviewKeyDownEventArgs((Keys)(msg.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys));
				this.OnPreviewKeyDown(previewKeyDownEventArgs);
				if (previewKeyDownEventArgs.IsInputKey)
				{
					return PreProcessControlState.MessageNeeded;
				}
				if (this.PreProcessMessage(ref msg))
				{
					return PreProcessControlState.MessageProcessed;
				}
				if (this.IsInputKey((Keys)(msg.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys)))
				{
					return PreProcessControlState.MessageNeeded;
				}
				break;
			}
			case 258:
			case 262:
				if (this.PreProcessMessage(ref msg))
				{
					return PreProcessControlState.MessageProcessed;
				}
				if (this.IsInputChar((char)(int)msg.WParam))
				{
					return PreProcessControlState.MessageNeeded;
				}
				break;
			}
			return PreProcessControlState.MessageNotNeeded;
		}

		/// <summary>Preprocesses keyboard or input messages within the message loop before they are dispatched.</summary>
		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the message to process. The possible values are WM_KEYDOWN, WM_SYSKEYDOWN, WM_CHAR, and WM_SYSCHAR. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000320 RID: 800 RVA: 0x0000C478 File Offset: 0x0000A678
		public virtual bool PreProcessMessage(ref Message msg)
		{
			return this.InternalPreProcessMessage(ref msg);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000C484 File Offset: 0x0000A684
		internal virtual bool InternalPreProcessMessage(ref Message msg)
		{
			if (msg.Msg == 256 || msg.Msg == 260)
			{
				Keys keys = (Keys)(msg.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys);
				return this.ProcessCmdKey(ref msg, keys) || (!this.IsInputKey(keys) && this.ProcessDialogKey(keys));
			}
			if (msg.Msg == 258)
			{
				return !this.IsInputChar((char)(int)msg.WParam) && this.ProcessDialogChar((char)(int)msg.WParam);
			}
			return msg.Msg == 262 && (this.ProcessDialogChar((char)(int)msg.WParam) || ToolStripManager.ProcessMenuKey(ref msg));
		}

		/// <summary>Forces the control to invalidate its client area and immediately redraw itself and any child controls.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000322 RID: 802 RVA: 0x0000C540 File Offset: 0x0000A740
		public virtual void Refresh()
		{
			if (this.IsHandleCreated && this.Visible)
			{
				this.Invalidate(true);
				this.Update();
			}
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.Cursor" /> property to its default value.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000323 RID: 803 RVA: 0x0000C55F File Offset: 0x0000A75F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual void ResetCursor()
		{
			this.Cursor = null;
		}

		/// <summary>Resumes usual layout logic.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000324 RID: 804 RVA: 0x0000C568 File Offset: 0x0000A768
		public void ResumeLayout()
		{
			this.ResumeLayout(true);
		}

		/// <summary>Resumes usual layout logic, optionally forcing an immediate layout of pending layout requests.</summary>
		/// <param name="performLayout">true to execute pending layout requests; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000325 RID: 805 RVA: 0x0000C574 File Offset: 0x0000A774
		public void ResumeLayout(bool performLayout)
		{
			if (this.layout_suspended > 0)
			{
				this.layout_suspended--;
			}
			if (this.layout_suspended == 0)
			{
				if (this is ContainerControl)
				{
					(this as ContainerControl).PerformDelayedAutoScale();
				}
				if (!performLayout)
				{
					Control[] allControls = this.Controls.GetAllControls();
					for (int i = 0; i < allControls.Length; i++)
					{
						allControls[i].UpdateDistances();
					}
				}
				if (performLayout && this.layout_pending)
				{
					this.PerformLayout();
				}
			}
		}

		/// <summary>Scales the entire control and any child controls.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000326 RID: 806 RVA: 0x0000C5E9 File Offset: 0x0000A7E9
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete]
		public void Scale(float dx, float dy)
		{
			this.ScaleCore(dx, dy);
		}

		/// <summary>Scales the control and all child controls by the specified scaling factor.</summary>
		/// <param name="factor">A <see cref="T:System.Drawing.SizeF" /> containing the horizontal and vertical scaling factors.</param>
		// Token: 0x06000327 RID: 807 RVA: 0x0000C5F4 File Offset: 0x0000A7F4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void Scale(SizeF factor)
		{
			BoundsSpecified boundsSpecified = BoundsSpecified.All;
			this.SuspendLayout();
			if (this is ContainerControl)
			{
				if ((this as ContainerControl).IsAutoScaling)
				{
					boundsSpecified = BoundsSpecified.Size;
				}
				else if (this.IsContainerAutoScaling(this.Parent))
				{
					boundsSpecified = BoundsSpecified.Location;
				}
			}
			this.ScaleControl(factor, boundsSpecified);
			if (boundsSpecified != BoundsSpecified.Location && this.ScaleChildren)
			{
				foreach (Control control in this.Controls.GetAllControls())
				{
					control.Scale(factor);
					if (control is ContainerControl)
					{
						ContainerControl containerControl = control as ContainerControl;
						if (containerControl.AutoScaleMode == AutoScaleMode.Inherit && this.IsContainerAutoScaling(this))
						{
							containerControl.PerformAutoScale(true);
						}
					}
				}
			}
			this.ResumeLayout();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000C69E File Offset: 0x0000A89E
		internal ContainerControl FindContainer(Control c)
		{
			while (c != null && !(c is ContainerControl))
			{
				c = c.Parent;
			}
			return c as ContainerControl;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		private bool IsContainerAutoScaling(Control c)
		{
			ContainerControl containerControl = this.FindContainer(c);
			return containerControl != null && containerControl.IsAutoScaling;
		}

		/// <summary>Activates the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600032A RID: 810 RVA: 0x0000C6DC File Offset: 0x0000A8DC
		public void Select()
		{
			this.Select(false, false);
		}

		/// <summary>Activates the next control.</summary>
		/// <returns>true if a control was activated; otherwise, false.</returns>
		/// <param name="ctl">The <see cref="T:System.Windows.Forms.Control" /> at which to start the search. </param>
		/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order. </param>
		/// <param name="tabStopOnly">true to ignore the controls with the <see cref="P:System.Windows.Forms.Control.TabStop" /> property set to false; otherwise, false. </param>
		/// <param name="nested">true to include nested (children of child controls) child controls; otherwise, false. </param>
		/// <param name="wrap">true to continue searching from the first control in the tab order after the last control has been reached; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600032B RID: 811 RVA: 0x0000C6E8 File Offset: 0x0000A8E8
		public bool SelectNextControl(Control ctl, bool forward, bool tabStopOnly, bool nested, bool wrap)
		{
			if (!this.Contains(ctl) || (!nested && ctl.parent != this))
			{
				ctl = null;
			}
			Control control = ctl;
			for (;;)
			{
				control = this.GetNextControl(control, forward);
				if (control == null)
				{
					if (!wrap)
					{
						return false;
					}
					wrap = false;
				}
				else if (control.CanSelect && (control.parent == this || nested) && (control.tab_stop || !tabStopOnly))
				{
					break;
				}
				if (control == ctl)
				{
					return false;
				}
			}
			control.Select(true, true);
			return true;
		}

		/// <summary>Sends the control to the back of the z-order.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600032C RID: 812 RVA: 0x0000C755 File Offset: 0x0000A955
		public void SendToBack()
		{
			if (this.parent != null)
			{
				this.parent.child_controls.SetChildIndex(this, this.parent.child_controls.Count);
			}
		}

		/// <summary>Sets the specified bounds of the control to the specified location and size.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. For any parameter not specified, the current value will be used. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600032D RID: 813 RVA: 0x0000C780 File Offset: 0x0000A980
		public void SetBounds(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.X) == BoundsSpecified.None)
			{
				x = this.Left;
			}
			if ((specified & BoundsSpecified.Y) == BoundsSpecified.None)
			{
				y = this.Top;
			}
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.None)
			{
				width = this.Width;
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.None)
			{
				height = this.Height;
			}
			this.SetBoundsInternal(x, y, width, height, specified);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000C7D4 File Offset: 0x0000A9D4
		internal void SetBoundsInternal(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.bounds.X != x || (this.explicit_bounds.X != x && (specified & BoundsSpecified.X) == BoundsSpecified.X))
			{
				this.SetBoundsCore(x, y, width, height, specified);
			}
			else if (this.bounds.Y != y || (this.explicit_bounds.Y != y && (specified & BoundsSpecified.Y) == BoundsSpecified.Y))
			{
				this.SetBoundsCore(x, y, width, height, specified);
			}
			else if (this.bounds.Width != width || (this.explicit_bounds.Width != width && (specified & BoundsSpecified.Width) == BoundsSpecified.Width))
			{
				this.SetBoundsCore(x, y, width, height, specified);
			}
			else
			{
				if (this.bounds.Height == height && (this.explicit_bounds.Height == height || (specified & BoundsSpecified.Height) != BoundsSpecified.Height))
				{
					return;
				}
				this.SetBoundsCore(x, y, width, height, specified);
			}
			if (specified != BoundsSpecified.None)
			{
				this.UpdateDistances();
			}
			if (this.parent != null)
			{
				this.parent.PerformLayout(this, "Bounds");
			}
		}

		/// <summary>Displays the control to the user.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600032F RID: 815 RVA: 0x0000C8D2 File Offset: 0x0000AAD2
		public void Show()
		{
			this.Visible = true;
		}

		/// <summary>Temporarily suspends the layout logic for the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000330 RID: 816 RVA: 0x0000C8DB File Offset: 0x0000AADB
		public void SuspendLayout()
		{
			this.layout_suspended++;
		}

		/// <summary>Causes the control to redraw the invalidated regions within its client area.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000331 RID: 817 RVA: 0x0000C8EB File Offset: 0x0000AAEB
		public void Update()
		{
			if (this.IsHandleCreated)
			{
				XplatUI.UpdateWindow(this.window.Handle);
			}
		}

		/// <summary>Creates a new instance of the control collection for the control.</summary>
		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x06000332 RID: 818 RVA: 0x0000C905 File Offset: 0x0000AB05
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual Control.ControlCollection CreateControlsInstance()
		{
			return new Control.ControlCollection(this);
		}

		/// <summary>Creates a handle for the control.</summary>
		/// <exception cref="T:System.ObjectDisposedException">The object is in a disposed state. </exception>
		// Token: 0x06000333 RID: 819 RVA: 0x0000C910 File Offset: 0x0000AB10
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void CreateHandle()
		{
			if (this.IsDisposed)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.IsHandleCreated && !this.is_recreating)
			{
				return;
			}
			CreateParams createParams = this.CreateParams;
			this.window.CreateHandle(createParams);
			if (this.window.Handle != IntPtr.Zero)
			{
				this.creator_thread = Thread.CurrentThread;
				XplatUI.EnableWindow(this.window.Handle, this.is_enabled);
				if (this.clip_region != null)
				{
					XplatUI.SetClipRegion(this.window.Handle, this.clip_region);
				}
				if (this.parent != null && this.parent.IsHandleCreated)
				{
					XplatUI.SetParent(this.window.Handle, this.parent.Handle);
				}
				this.UpdateStyles();
				XplatUI.SetAllowDrop(this.window.Handle, this.allow_drop);
				if ((this.CreateParams.Style & 1073741824) != 0)
				{
					XplatUI.SetBorderStyle(this.window.Handle, (FormBorderStyle)this.border_style);
				}
				Rectangle rectangle = this.explicit_bounds;
				this.UpdateBounds();
				this.explicit_bounds = rectangle;
			}
		}

		/// <summary>Sends the specified message to the default window procedure.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06000334 RID: 820 RVA: 0x0000CA3D File Offset: 0x0000AC3D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void DefWndProc(ref Message m)
		{
			this.window.DefWndProc(ref m);
		}

		/// <summary>Destroys the handle associated with the control.</summary>
		// Token: 0x06000335 RID: 821 RVA: 0x0000CA4B File Offset: 0x0000AC4B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void DestroyHandle()
		{
			if (this.IsHandleCreated && this.window != null)
			{
				this.window.DestroyHandle();
			}
		}

		/// <summary>Retrieves a value indicating how a control will behave when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values. </returns>
		// Token: 0x06000336 RID: 822 RVA: 0x0000CA68 File Offset: 0x0000AC68
		protected internal AutoSizeMode GetAutoSizeMode()
		{
			return this.auto_size_mode;
		}

		/// <summary>Retrieves the bounds within which the control is scaled.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the bounds within which the control is scaled.</returns>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the area for which to retrieve the display bounds.</param>
		/// <param name="factor">The height and width of the control's bounds.</param>
		/// <param name="specified">One of the values of <see cref="T:System.Windows.Forms.BoundsSpecified" /> that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06000337 RID: 823 RVA: 0x0000CA70 File Offset: 0x0000AC70
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			if (!this.is_toplevel)
			{
				if ((specified & BoundsSpecified.X) == BoundsSpecified.X)
				{
					bounds.X = (int)Math.Round((double)((float)bounds.X * factor.Width));
				}
				if ((specified & BoundsSpecified.Y) == BoundsSpecified.Y)
				{
					bounds.Y = (int)Math.Round((double)((float)bounds.Y * factor.Height));
				}
			}
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width && !this.GetStyle(ControlStyles.FixedWidth))
			{
				int num = ((this is ComboBox) ? (ThemeEngine.Current.Border3DSize.Width * 2) : (this.bounds.Width - this.client_size.Width));
				bounds.Width = (int)Math.Round((double)((float)(bounds.Width - num) * factor.Width + (float)num));
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height && !this.GetStyle(ControlStyles.FixedHeight))
			{
				int num2 = ((this is ComboBox) ? (ThemeEngine.Current.Border3DSize.Height * 2) : (this.bounds.Height - this.client_size.Height));
				bounds.Height = (int)Math.Round((double)((float)(bounds.Height - num2) * factor.Height + (float)num2));
			}
			return bounds;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000CBA0 File Offset: 0x0000ADA0
		private Rectangle GetScaledBoundsOld(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			RectangleF rectangleF = new RectangleF(bounds.Location, bounds.Size);
			if (!this.is_toplevel)
			{
				if ((specified & BoundsSpecified.X) == BoundsSpecified.X)
				{
					rectangleF.X *= factor.Width;
				}
				if ((specified & BoundsSpecified.Y) == BoundsSpecified.Y)
				{
					rectangleF.Y *= factor.Height;
				}
			}
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width && !this.GetStyle(ControlStyles.FixedWidth))
			{
				int num = ((this is Form) ? (this.bounds.Width - this.client_size.Width) : 0);
				rectangleF.Width = (rectangleF.Width - (float)num) * factor.Width + (float)num;
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height && !this.GetStyle(ControlStyles.FixedHeight))
			{
				int num2 = ((this is Form) ? (this.bounds.Height - this.client_size.Height) : 0);
				rectangleF.Height = (rectangleF.Height - (float)num2) * factor.Height + (float)num2;
			}
			bounds.X = (int)Math.Round((double)rectangleF.X);
			bounds.Y = (int)Math.Round((double)rectangleF.Y);
			bounds.Width = (int)Math.Round((double)rectangleF.Right) - bounds.X;
			bounds.Height = (int)Math.Round((double)rectangleF.Bottom) - bounds.Y;
			return bounds;
		}

		/// <summary>Retrieves the value of the specified control style bit for the control.</summary>
		/// <returns>true if the specified control style bit is set to true; otherwise, false.</returns>
		/// <param name="flag">The <see cref="T:System.Windows.Forms.ControlStyles" /> bit to return the value from. </param>
		// Token: 0x06000339 RID: 825 RVA: 0x0000CD0D File Offset: 0x0000AF0D
		protected internal bool GetStyle(ControlStyles flag)
		{
			return (this.control_style & flag) > (ControlStyles)0;
		}

		/// <summary>Determines if the control is a top-level control.</summary>
		/// <returns>true if the control is a top-level control; otherwise, false.</returns>
		// Token: 0x0600033A RID: 826 RVA: 0x0000CD1A File Offset: 0x0000AF1A
		protected bool GetTopLevel()
		{
			return this.is_toplevel;
		}

		/// <summary>Called after the control has been added to another container.</summary>
		// Token: 0x0600033B RID: 827 RVA: 0x0000493C File Offset: 0x00002B3C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void InitLayout()
		{
		}

		/// <summary>Determines if a character is an input character that the control recognizes.</summary>
		/// <returns>true if the character should be sent directly to the control and not preprocessed; otherwise, false.</returns>
		/// <param name="charCode">The character to test. </param>
		// Token: 0x0600033C RID: 828 RVA: 0x0000CD22 File Offset: 0x0000AF22
		protected virtual bool IsInputChar(char charCode)
		{
			if (!this.IsHandleCreated)
			{
				this.CreateHandle();
			}
			return this.IsInputCharInternal(charCode);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00002D70 File Offset: 0x00000F70
		internal virtual bool IsInputCharInternal(char charCode)
		{
			return false;
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
		// Token: 0x0600033E RID: 830 RVA: 0x00002D70 File Offset: 0x00000F70
		protected virtual bool IsInputKey(Keys keyData)
		{
			return false;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event with a specified region of the control to invalidate.</summary>
		/// <param name="invalidatedArea">A <see cref="T:System.Drawing.Rectangle" /> representing the area to invalidate. </param>
		// Token: 0x0600033F RID: 831 RVA: 0x0000493C File Offset: 0x00002B3C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void NotifyInvalidate(Rectangle invalidatedArea)
		{
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06000340 RID: 832 RVA: 0x0000CD39 File Offset: 0x0000AF39
		protected virtual bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return (this.context_menu != null && this.context_menu.ProcessCmdKey(ref msg, keyData)) || (this.parent != null && this.parent.ProcessCmdKey(ref msg, keyData));
		}

		/// <summary>Processes a dialog character.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x06000341 RID: 833 RVA: 0x0000CD6B File Offset: 0x0000AF6B
		protected virtual bool ProcessDialogChar(char charCode)
		{
			return this.parent != null && this.parent.ProcessDialogChar(charCode);
		}

		/// <summary>Processes a dialog key.</summary>
		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06000342 RID: 834 RVA: 0x0000CD83 File Offset: 0x0000AF83
		protected virtual bool ProcessDialogKey(Keys keyData)
		{
			return this.parent != null && this.parent.ProcessDialogKey(keyData);
		}

		/// <summary>Processes a key message and generates the appropriate control events.</summary>
		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		// Token: 0x06000343 RID: 835 RVA: 0x0000CD9C File Offset: 0x0000AF9C
		protected virtual bool ProcessKeyEventArgs(ref Message m)
		{
			switch (m.Msg)
			{
			case 256:
			case 260:
			{
				KeyEventArgs keyEventArgs = new KeyEventArgs((Keys)(m.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys));
				this.OnKeyDown(keyEventArgs);
				this.suppressing_key_press = keyEventArgs.SuppressKeyPress;
				return keyEventArgs.Handled;
			}
			case 257:
			case 261:
			{
				KeyEventArgs keyEventArgs = new KeyEventArgs((Keys)(m.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys));
				this.OnKeyUp(keyEventArgs);
				return keyEventArgs.Handled;
			}
			case 258:
			case 262:
			{
				if (this.suppressing_key_press)
				{
					return true;
				}
				KeyPressEventArgs keyPressEventArgs = new KeyPressEventArgs((char)(int)m.WParam);
				this.OnKeyPress(keyPressEventArgs);
				m.WParam = (IntPtr)((int)keyPressEventArgs.KeyChar);
				return keyPressEventArgs.Handled;
			}
			}
			return false;
		}

		/// <summary>Processes a keyboard message.</summary>
		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		// Token: 0x06000344 RID: 836 RVA: 0x0000CE75 File Offset: 0x0000B075
		protected internal virtual bool ProcessKeyMessage(ref Message m)
		{
			return (this.parent != null && this.parent.ProcessKeyPreview(ref m)) || this.ProcessKeyEventArgs(ref m);
		}

		/// <summary>Previews a keyboard message.</summary>
		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		// Token: 0x06000345 RID: 837 RVA: 0x0000CE96 File Offset: 0x0000B096
		protected virtual bool ProcessKeyPreview(ref Message m)
		{
			return this.parent != null && this.parent.ProcessKeyPreview(ref m);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x06000346 RID: 838 RVA: 0x00002D70 File Offset: 0x00000F70
		protected virtual bool ProcessMnemonic(char charCode)
		{
			return false;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000CEB0 File Offset: 0x0000B0B0
		private void SetIsRecreating()
		{
			this.is_recreating = true;
			Control[] allControls = this.Controls.GetAllControls();
			for (int i = 0; i < allControls.Length; i++)
			{
				allControls[i].SetIsRecreating();
			}
		}

		/// <summary>Forces the re-creation of the handle for the control.</summary>
		// Token: 0x06000348 RID: 840 RVA: 0x0000CEE6 File Offset: 0x0000B0E6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void RecreateHandle()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			this.SetIsRecreating();
			if (this.IsHandleCreated)
			{
				this.DestroyHandle();
				return;
			}
			if (!this.is_created)
			{
				this.CreateControl();
			}
			else
			{
				this.CreateHandle();
			}
			this.is_recreating = false;
		}

		/// <summary>Scales a control's location, size, padding and margin.</summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06000349 RID: 841 RVA: 0x0000CF24 File Offset: 0x0000B124
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			Rectangle scaledBounds = this.GetScaledBounds(this.bounds, factor, specified);
			this.SetBounds(scaledBounds.X, scaledBounds.Y, scaledBounds.Width, scaledBounds.Height, specified);
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x0600034A RID: 842 RVA: 0x0000CF64 File Offset: 0x0000B164
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected virtual void ScaleCore(float dx, float dy)
		{
			Rectangle scaledBoundsOld = this.GetScaledBoundsOld(this.bounds, new SizeF(dx, dy), BoundsSpecified.All);
			this.SuspendLayout();
			this.SetBounds(scaledBoundsOld.X, scaledBoundsOld.Y, scaledBoundsOld.Width, scaledBoundsOld.Height, BoundsSpecified.All);
			if (this.ScaleChildrenInternal)
			{
				Control[] allControls = this.Controls.GetAllControls();
				for (int i = 0; i < allControls.Length; i++)
				{
					allControls[i].Scale(dx, dy);
				}
			}
			this.ResumeLayout();
		}

		/// <summary>Activates a child control. Optionally specifies the direction in the tab order to select the control from.</summary>
		/// <param name="directed">true to specify the direction of the control to select; otherwise, false. </param>
		/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order. </param>
		// Token: 0x0600034B RID: 843 RVA: 0x0000CFE4 File Offset: 0x0000B1E4
		protected virtual void Select(bool directed, bool forward)
		{
			IContainerControl containerControl = this.GetContainerControl();
			if (containerControl != null && (Control)containerControl != this)
			{
				containerControl.ActiveControl = this;
			}
		}

		/// <summary>Sets a value indicating how a control will behave when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled.</summary>
		/// <param name="mode">One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values.</param>
		// Token: 0x0600034C RID: 844 RVA: 0x0000D00B File Offset: 0x0000B20B
		protected void SetAutoSizeMode(AutoSizeMode mode)
		{
			if (this.auto_size_mode != mode)
			{
				this.auto_size_mode = mode;
				this.PerformLayout(this, "AutoSizeMode");
			}
		}

		/// <summary>Performs the work of setting the specified bounds of this control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
		// Token: 0x0600034D RID: 845 RVA: 0x0000D029 File Offset: 0x0000B229
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			this.SetBoundsCoreInternal(x, y, width, height, specified);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000D038 File Offset: 0x0000B238
		internal virtual void SetBoundsCoreInternal(int x, int y, int width, int height, BoundsSpecified specified)
		{
			height = this.OverrideHeight(height);
			Rectangle rectangle = this.explicit_bounds;
			Rectangle rectangle2 = new Rectangle(x, y, width, height);
			if (this.IsHandleCreated)
			{
				XplatUI.SetWindowPos(this.Handle, x, y, width, height);
				int num;
				int num2;
				int num3;
				int num4;
				XplatUI.GetWindowPos(this.Handle, this is Form, out num, out num2, out width, out height, out num3, out num4);
			}
			if ((specified & BoundsSpecified.X) == BoundsSpecified.X)
			{
				this.explicit_bounds.X = rectangle2.X;
			}
			else
			{
				this.explicit_bounds.X = rectangle.X;
			}
			if ((specified & BoundsSpecified.Y) == BoundsSpecified.Y)
			{
				this.explicit_bounds.Y = rectangle2.Y;
			}
			else
			{
				this.explicit_bounds.Y = rectangle.Y;
			}
			if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
			{
				this.explicit_bounds.Width = rectangle2.Width;
			}
			else
			{
				this.explicit_bounds.Width = rectangle.Width;
			}
			if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
			{
				this.explicit_bounds.Height = rectangle2.Height;
			}
			else
			{
				this.explicit_bounds.Height = rectangle.Height;
			}
			Rectangle rectangle3 = this.explicit_bounds;
			this.UpdateBounds(x, y, width, height);
			if (this.explicit_bounds.X == x)
			{
				this.explicit_bounds.X = rectangle3.X;
			}
			if (this.explicit_bounds.Y == y)
			{
				this.explicit_bounds.Y = rectangle3.Y;
			}
			if (this.explicit_bounds.Width == width)
			{
				this.explicit_bounds.Width = rectangle3.Width;
			}
			if (this.explicit_bounds.Height == height)
			{
				this.explicit_bounds.Height = rectangle3.Height;
			}
		}

		/// <summary>Sets the size of the client area of the control.</summary>
		/// <param name="x">The client area width, in pixels. </param>
		/// <param name="y">The client area height, in pixels. </param>
		// Token: 0x0600034F RID: 847 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void SetClientSizeCore(int x, int y)
		{
			Size size = this.InternalSizeFromClientSize(new Size(x, y));
			if (size != Size.Empty)
			{
				this.SetBounds(this.bounds.X, this.bounds.Y, size.Width, size.Height, BoundsSpecified.Size);
			}
		}

		/// <summary>Sets a specified <see cref="T:System.Windows.Forms.ControlStyles" /> flag to either true or false.</summary>
		/// <param name="flag">The <see cref="T:System.Windows.Forms.ControlStyles" /> bit to set. </param>
		/// <param name="value">true to apply the specified style to the control; otherwise, false. </param>
		// Token: 0x06000350 RID: 848 RVA: 0x0000D238 File Offset: 0x0000B438
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected internal void SetStyle(ControlStyles flag, bool value)
		{
			if (value)
			{
				this.control_style |= flag;
				return;
			}
			this.control_style &= ~flag;
		}

		/// <summary>Sets the control to the specified visible state.</summary>
		/// <param name="value">true to make the control visible; otherwise, false. </param>
		// Token: 0x06000351 RID: 849 RVA: 0x0000D25C File Offset: 0x0000B45C
		protected virtual void SetVisibleCore(bool value)
		{
			if (value != this.is_visible)
			{
				this.is_visible = value;
				if (this.is_visible && (this.window.Handle == IntPtr.Zero || !this.is_created))
				{
					this.CreateControl();
					if (!(this is Form))
					{
						this.UpdateZOrder();
					}
				}
				if (this.IsHandleCreated)
				{
					XplatUI.SetVisible(this.Handle, this.is_visible, true);
					if (!this.is_visible)
					{
						if (this.parent != null && this.parent.IsHandleCreated)
						{
							this.parent.Invalidate(this.bounds);
							this.parent.Update();
						}
						else
						{
							this.Refresh();
						}
					}
					else if (this.is_visible && this is Form)
					{
						if ((this as Form).WindowState != FormWindowState.Normal)
						{
							this.OnVisibleChanged(EventArgs.Empty);
						}
						else
						{
							XplatUI.SetWindowPos(this.window.Handle, this.bounds.X, this.bounds.Y, this.bounds.Width, this.bounds.Height);
						}
					}
					else if (this.parent != null)
					{
						this.parent.UpdateZOrderOfChild(this);
					}
					if (!(this is Form))
					{
						this.OnVisibleChanged(EventArgs.Empty);
						return;
					}
				}
				else
				{
					this.OnVisibleChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Determines the size of the entire control from the height and width of its client area.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value representing the height and width of the entire control.</returns>
		/// <param name="clientSize">A <see cref="T:System.Drawing.Size" /> value representing the height and width of the control's client area.</param>
		// Token: 0x06000352 RID: 850 RVA: 0x0000D3B6 File Offset: 0x0000B5B6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual Size SizeFromClientSize(Size clientSize)
		{
			return this.InternalSizeFromClientSize(clientSize);
		}

		/// <summary>Updates the bounds of the control with the current size and location.</summary>
		// Token: 0x06000353 RID: 851 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateBounds()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			int num6;
			XplatUI.GetWindowPos(this.Handle, this is Form, out num, out num2, out num3, out num4, out num5, out num6);
			this.UpdateBounds(num, num2, num3, num4, num5, num6);
		}

		/// <summary>Updates the bounds of the control with the specified size and location.</summary>
		/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> coordinate of the control. </param>
		/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> coordinate of the control. </param>
		/// <param name="width">The <see cref="P:System.Drawing.Size.Width" /> of the control. </param>
		/// <param name="height">The <see cref="P:System.Drawing.Size.Height" /> of the control. </param>
		// Token: 0x06000354 RID: 852 RVA: 0x0000D404 File Offset: 0x0000B604
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateBounds(int x, int y, int width, int height)
		{
			Rectangle rectangle = new Rectangle(0, 0, 0, 0);
			CreateParams createParams = this.CreateParams;
			XplatUI.CalculateWindowRect(ref rectangle, createParams, createParams.menu, out rectangle);
			this.UpdateBounds(x, y, width, height, width - (rectangle.Right - rectangle.Left), height - (rectangle.Bottom - rectangle.Top));
		}

		/// <summary>Updates the bounds of the control with the specified size, location, and client size.</summary>
		/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> coordinate of the control. </param>
		/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> coordinate of the control. </param>
		/// <param name="width">The <see cref="P:System.Drawing.Size.Width" /> of the control. </param>
		/// <param name="height">The <see cref="P:System.Drawing.Size.Height" /> of the control. </param>
		/// <param name="clientWidth">The client <see cref="P:System.Drawing.Size.Width" /> of the control. </param>
		/// <param name="clientHeight">The client <see cref="P:System.Drawing.Size.Height" /> of the control. </param>
		// Token: 0x06000355 RID: 853 RVA: 0x0000D464 File Offset: 0x0000B664
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateBounds(int x, int y, int width, int height, int clientWidth, int clientHeight)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.bounds.X != x || this.bounds.Y != y)
			{
				flag = true;
			}
			if (this.Bounds.Width != width || this.Bounds.Height != height)
			{
				flag2 = true;
			}
			this.bounds.X = x;
			this.bounds.Y = y;
			this.bounds.Width = width;
			this.bounds.Height = height;
			this.explicit_bounds = this.bounds;
			this.client_size.Width = clientWidth;
			this.client_size.Height = clientHeight;
			if (flag)
			{
				this.OnLocationChanged(EventArgs.Empty);
				if (!this.background_color.IsEmpty && this.background_color.A < 255)
				{
					this.Invalidate();
				}
			}
			if (flag2)
			{
				this.OnSizeInitializedOrChanged();
				this.OnSizeChanged(EventArgs.Empty);
				this.OnClientSizeChanged(EventArgs.Empty);
			}
		}

		/// <summary>Forces the assigned styles to be reapplied to the control.</summary>
		// Token: 0x06000356 RID: 854 RVA: 0x0000D561 File Offset: 0x0000B761
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateStyles()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			XplatUI.SetWindowStyle(this.window.Handle, this.CreateParams);
			this.OnStyleChanged(EventArgs.Empty);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000D590 File Offset: 0x0000B790
		private void UpdateZOrderOfChild(Control child)
		{
			if (this.IsHandleCreated && child.IsHandleCreated && child.parent == this && Hwnd.ObjectFromHandle(child.Handle).Mapped)
			{
				Control[] allControls = this.child_controls.GetAllControls();
				int num = Array.IndexOf<Control>(allControls, child);
				while (num > 0 && (!allControls[num - 1].IsHandleCreated || !allControls[num - 1].VisibleInternal || !Hwnd.ObjectFromHandle(allControls[num - 1].Handle).Mapped))
				{
					num--;
				}
				if (num > 0)
				{
					XplatUI.SetZOrder(child.Handle, allControls[num - 1].Handle, false, false);
					return;
				}
				IntPtr intPtr = this.AfterTopMostControl();
				if (intPtr != IntPtr.Zero && intPtr != child.Handle)
				{
					XplatUI.SetZOrder(child.Handle, intPtr, false, false);
					return;
				}
				XplatUI.SetZOrder(child.Handle, IntPtr.Zero, true, false);
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000D682 File Offset: 0x0000B882
		internal virtual IntPtr AfterTopMostControl()
		{
			return IntPtr.Zero;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000D68C File Offset: 0x0000B88C
		internal void UpdateChildrenZOrder()
		{
			if (!this.IsHandleCreated)
			{
				return;
			}
			Control[] array;
			if (this.child_controls.ImplicitControls == null)
			{
				array = new Control[this.child_controls.Count];
				this.child_controls.CopyTo(array, 0);
			}
			else
			{
				array = new Control[this.child_controls.Count + this.child_controls.ImplicitControls.Count];
				this.child_controls.CopyTo(array, 0);
				this.child_controls.ImplicitControls.CopyTo(array, this.child_controls.Count);
			}
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].IsHandleCreated && array[i].VisibleInternal)
				{
					Hwnd hwnd = Hwnd.ObjectFromHandle(array[i].Handle);
					if (hwnd != null && !hwnd.zero_sized)
					{
						arrayList.Add(array[i]);
					}
				}
			}
			for (int j = 1; j < arrayList.Count; j++)
			{
				Control control = (Control)arrayList[j - 1];
				XplatUI.SetZOrder(((Control)arrayList[j]).Handle, control.Handle, false, false);
			}
		}

		/// <summary>Updates the control in its parent's z-order.</summary>
		// Token: 0x0600035A RID: 858 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected void UpdateZOrder()
		{
			if (this.parent != null)
			{
				this.parent.UpdateZOrderOfChild(this);
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x0600035B RID: 859 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		protected virtual void WndProc(ref Message m)
		{
			if ((this.control_style & ControlStyles.EnableNotifyMessage) != (ControlStyles)0)
			{
				this.OnNotifyMessage(m);
			}
			Msg msg = (Msg)m.Msg;
			if (msg <= Msg.WM_WINDOWPOSCHANGED)
			{
				if (msg <= Msg.WM_KILLFOCUS)
				{
					if (msg <= Msg.WM_DESTROY)
					{
						if (msg == Msg.WM_CREATE)
						{
							this.WmCreate(ref m);
							return;
						}
						if (msg == Msg.WM_DESTROY)
						{
							this.WmDestroy(ref m);
							return;
						}
					}
					else
					{
						if (msg == Msg.WM_SETFOCUS)
						{
							this.WmSetFocus(ref m);
							return;
						}
						if (msg == Msg.WM_KILLFOCUS)
						{
							this.WmKillFocus(ref m);
							return;
						}
					}
				}
				else if (msg <= Msg.WM_SHOWWINDOW)
				{
					if (msg == Msg.WM_PAINT)
					{
						this.WmPaint(ref m);
						return;
					}
					switch (msg)
					{
					case Msg.WM_ERASEBKGND:
						this.WmEraseBackground(ref m);
						return;
					case Msg.WM_SYSCOLORCHANGE:
						this.WmSysColorChange(ref m);
						return;
					case Msg.WM_SHOWWINDOW:
						this.WmShowWindow(ref m);
						return;
					}
				}
				else
				{
					if (msg == Msg.WM_SETCURSOR)
					{
						this.WmSetCursor(ref m);
						return;
					}
					if (msg == Msg.WM_WINDOWPOSCHANGED)
					{
						this.WmWindowPosChanged(ref m);
						return;
					}
				}
			}
			else if (msg <= Msg.WM_CHANGEUISTATE)
			{
				if (msg <= Msg.WM_CONTEXTMENU)
				{
					if (msg == Msg.WM_HELP)
					{
						this.WmHelp(ref m);
						return;
					}
					if (msg == Msg.WM_CONTEXTMENU)
					{
						this.WmContextMenu(ref m);
						return;
					}
				}
				else
				{
					switch (msg)
					{
					case Msg.WM_KEYDOWN:
					case Msg.WM_KEYUP:
					case Msg.WM_CHAR:
					case Msg.WM_SYSKEYDOWN:
					case Msg.WM_SYSCHAR:
						this.WmKeys(ref m);
						return;
					case Msg.WM_DEADCHAR:
						break;
					case Msg.WM_SYSKEYUP:
						this.WmSysKeyUp(ref m);
						return;
					default:
						if (msg == Msg.WM_CHANGEUISTATE)
						{
							this.WmChangeUIState(ref m);
							return;
						}
						break;
					}
				}
			}
			else if (msg <= Msg.WM_CAPTURECHANGED)
			{
				if (msg == Msg.WM_UPDATEUISTATE)
				{
					this.WmUpdateUIState(ref m);
					return;
				}
				switch (msg)
				{
				case Msg.WM_MOUSEMOVE:
					this.WmMouseMove(ref m);
					return;
				case Msg.WM_LBUTTONDOWN:
					this.WmLButtonDown(ref m);
					return;
				case Msg.WM_LBUTTONUP:
					this.WmLButtonUp(ref m);
					return;
				case Msg.WM_LBUTTONDBLCLK:
					this.WmLButtonDblClick(ref m);
					return;
				case Msg.WM_RBUTTONDOWN:
					this.WmRButtonDown(ref m);
					return;
				case Msg.WM_RBUTTONUP:
					this.WmRButtonUp(ref m);
					return;
				case Msg.WM_RBUTTONDBLCLK:
					this.WmRButtonDblClick(ref m);
					return;
				case Msg.WM_MBUTTONDOWN:
					this.WmMButtonDown(ref m);
					return;
				case Msg.WM_MBUTTONUP:
					this.WmMButtonUp(ref m);
					return;
				case Msg.WM_MBUTTONDBLCLK:
					this.WmMButtonDblClick(ref m);
					return;
				case Msg.WM_MOUSEWHEEL:
					this.WmMouseWheel(ref m);
					return;
				case Msg.WM_CAPTURECHANGED:
					this.WmCaptureChanged(ref m);
					return;
				}
			}
			else
			{
				if (msg == Msg.WM_MOUSEHOVER)
				{
					this.WmMouseHover(ref m);
					return;
				}
				if (msg == Msg.WM_MOUSELEAVE)
				{
					this.WmMouseLeave(ref m);
					return;
				}
				if (msg == Msg.WM_MOUSE_ENTER)
				{
					this.WmMouseEnter(ref m);
					return;
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000DA64 File Offset: 0x0000BC64
		private void WmDestroy(ref Message m)
		{
			this.OnHandleDestroyed(EventArgs.Empty);
			this.window.InvalidateHandle();
			this.is_created = false;
			if (this.is_recreating)
			{
				this.CreateHandle();
				this.is_recreating = false;
			}
			if (this.is_disposing)
			{
				this.is_disposing = false;
				this.is_visible = false;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000DABC File Offset: 0x0000BCBC
		private void WmWindowPosChanged(ref Message m)
		{
			if (this.Visible)
			{
				Rectangle rectangle = this.explicit_bounds;
				this.UpdateBounds();
				this.explicit_bounds = rectangle;
				if (this.GetStyle(ControlStyles.ResizeRedraw))
				{
					this.Invalidate();
				}
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000DAF8 File Offset: 0x0000BCF8
		private void WmPaint(ref Message m)
		{
			IntPtr handle = this.Handle;
			PaintEventArgs paintEventArgs = XplatUI.PaintEventStart(ref m, handle, true);
			if (paintEventArgs == null)
			{
				return;
			}
			Control.DoubleBuffer doubleBuffer = null;
			if (this.UseDoubleBuffering)
			{
				doubleBuffer = this.GetBackBuffer();
				doubleBuffer.Start(paintEventArgs);
			}
			if (this.GetStyle(ControlStyles.OptimizedDoubleBuffer))
			{
				paintEventArgs.Graphics.SetClip(Rectangle.Intersect(paintEventArgs.ClipRectangle, this.ClientRectangle));
			}
			if (!this.GetStyle(ControlStyles.Opaque))
			{
				this.OnPaintBackground(paintEventArgs);
			}
			this.OnPaintBackgroundInternal(paintEventArgs);
			this.OnPaintInternal(paintEventArgs);
			if (!paintEventArgs.Handled)
			{
				this.OnPaint(paintEventArgs);
			}
			if (doubleBuffer != null)
			{
				doubleBuffer.End(paintEventArgs);
			}
			XplatUI.PaintEventEnd(ref m, handle, true);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000DB99 File Offset: 0x0000BD99
		private void WmEraseBackground(ref Message m)
		{
			m.Result = (IntPtr)1;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000DBA8 File Offset: 0x0000BDA8
		private void WmLButtonUp(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.active_tracker != null)
			{
				this.ProcessActiveTracker(ref m);
				return;
			}
			MouseEventArgs mouseEventArgs = new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()) | MouseButtons.Left, this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0);
			this.HandleClick(this.mouse_clicks, mouseEventArgs);
			this.OnMouseUp(mouseEventArgs);
			if (this.InternalCapture)
			{
				this.InternalCapture = false;
			}
			if (this.mouse_clicks > 1)
			{
				this.mouse_clicks = 1;
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000DC54 File Offset: 0x0000BE54
		private void WmLButtonDown(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.active_tracker != null)
			{
				this.ProcessActiveTracker(ref m);
				return;
			}
			this.ValidationFailed = false;
			if (this.CanSelect)
			{
				this.Select(true, true);
			}
			if (!this.ValidationFailed)
			{
				this.InternalCapture = true;
				this.OnMouseDown(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0000DCF4 File Offset: 0x0000BEF4
		private void WmLButtonDblClick(ref Message m)
		{
			this.InternalCapture = true;
			this.mouse_clicks++;
			this.OnMouseDown(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0000DD64 File Offset: 0x0000BF64
		private void WmMButtonUp(ref Message m)
		{
			MouseEventArgs mouseEventArgs = new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()) | MouseButtons.Middle, this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0);
			this.HandleClick(this.mouse_clicks, mouseEventArgs);
			this.OnMouseUp(mouseEventArgs);
			if (this.InternalCapture)
			{
				this.InternalCapture = false;
			}
			if (this.mouse_clicks > 1)
			{
				this.mouse_clicks = 1;
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		private void WmMButtonDown(ref Message m)
		{
			this.InternalCapture = true;
			this.OnMouseDown(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000DE58 File Offset: 0x0000C058
		private void WmMButtonDblClick(ref Message m)
		{
			this.InternalCapture = true;
			this.mouse_clicks++;
			this.OnMouseDown(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
		private void WmRButtonUp(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.active_tracker != null)
			{
				this.ProcessActiveTracker(ref m);
				return;
			}
			Point point = new Point(Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
			point = this.PointToScreen(point);
			MouseEventArgs mouseEventArgs = new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()) | MouseButtons.Right, this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0);
			this.HandleClick(this.mouse_clicks, mouseEventArgs);
			XplatUI.SendMessage(m.HWnd, Msg.WM_CONTEXTMENU, m.HWnd, (IntPtr)(point.X + (point.Y << 16)));
			this.OnMouseUp(mouseEventArgs);
			if (this.InternalCapture)
			{
				this.InternalCapture = false;
			}
			if (this.mouse_clicks > 1)
			{
				this.mouse_clicks = 1;
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0000DFD4 File Offset: 0x0000C1D4
		private void WmRButtonDown(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.active_tracker != null)
			{
				this.ProcessActiveTracker(ref m);
				return;
			}
			this.InternalCapture = true;
			this.OnMouseDown(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000E054 File Offset: 0x0000C254
		private void WmRButtonDblClick(ref Message m)
		{
			this.InternalCapture = true;
			this.mouse_clicks++;
			this.OnMouseDown(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000E0C4 File Offset: 0x0000C2C4
		private void WmContextMenu(ref Message m)
		{
			if (this.context_menu != null)
			{
				Point point = new Point(Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
				if (point.X == -1 || point.Y == -1)
				{
					point.X = this.Width / 2 + this.Left;
					point.Y = this.Height / 2 + this.Top;
					point = this.PointToScreen(point);
				}
				this.context_menu.Show(this, this.PointToClient(point));
				return;
			}
			if (this.context_menu == null && this.context_menu_strip != null)
			{
				Point point2 = new Point(Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
				if (point2.X == -1 || point2.Y == -1)
				{
					point2.X = this.Width / 2 + this.Left;
					point2.Y = this.Height / 2 + this.Top;
					point2 = this.PointToScreen(point2);
				}
				this.context_menu_strip.SetSourceControl(this);
				this.context_menu_strip.Show(this, this.PointToClient(point2));
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000E219 File Offset: 0x0000C419
		private void WmCreate(ref Message m)
		{
			this.OnHandleCreated(EventArgs.Empty);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000E228 File Offset: 0x0000C428
		private void WmMouseWheel(ref Message m)
		{
			this.DefWndProc(ref m);
			this.OnMouseWheel(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), Control.HighOrder((long)m.WParam)));
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000E294 File Offset: 0x0000C494
		private void WmMouseMove(ref Message m)
		{
			if (XplatUI.IsEnabled(this.Handle) && this.active_tracker != null)
			{
				MouseEventArgs mouseEventArgs = new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.MousePosition.X, Control.MousePosition.Y, 0);
				this.active_tracker.OnMotion(mouseEventArgs);
				return;
			}
			this.OnMouseMove(new MouseEventArgs(Control.FromParamToMouseButtons((long)m.WParam.ToInt32()), this.mouse_clicks, Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()), 0));
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000E34B File Offset: 0x0000C54B
		private void WmMouseEnter(ref Message m)
		{
			if (this.is_entered)
			{
				return;
			}
			this.is_entered = true;
			this.OnMouseEnter(EventArgs.Empty);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000E368 File Offset: 0x0000C568
		private void WmMouseLeave(ref Message m)
		{
			this.is_entered = false;
			this.OnMouseLeave(EventArgs.Empty);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000E37C File Offset: 0x0000C57C
		private void WmMouseHover(ref Message m)
		{
			this.OnMouseHover(EventArgs.Empty);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000E38C File Offset: 0x0000C58C
		private void WmShowWindow(ref Message m)
		{
			if (this.IsDisposed)
			{
				return;
			}
			Form form = this as Form;
			if (m.WParam.ToInt32() != 0)
			{
				if (m.LParam.ToInt32() == 0)
				{
					this.CreateControl();
					Control[] allControls = this.child_controls.GetAllControls();
					for (int i = 0; i < allControls.Length; i++)
					{
						if (allControls[i].is_visible && allControls[i].IsHandleCreated && XplatUI.GetParent(allControls[i].Handle) != this.window.Handle)
						{
							XplatUI.SetParent(allControls[i].Handle, this.window.Handle);
						}
					}
					this.UpdateChildrenZOrder();
				}
			}
			else if (this.parent != null && this.Focused)
			{
				Control control = (Control)this.parent.GetContainerControl();
				if (control != null && (form == null || !form.IsMdiChild))
				{
					control.SelectNextControl(this, true, true, true, true);
				}
			}
			if (form != null)
			{
				form.waiting_showwindow = false;
			}
			if (form != null)
			{
				if (!this.IsRecreating && (form.IsMdiChild || form.WindowState == FormWindowState.Normal))
				{
					this.OnVisibleChanged(EventArgs.Empty);
					return;
				}
			}
			else if (this.is_toplevel)
			{
				this.OnVisibleChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		private void WmSysKeyUp(ref Message m)
		{
			if (this.ProcessKeyMessage(ref m))
			{
				m.Result = IntPtr.Zero;
				return;
			}
			if ((m.WParam.ToInt32() & 65535) == 18)
			{
				Form form = this.FindForm();
				if (form != null && form.ActiveMenu != null)
				{
					form.ActiveMenu.ProcessCmdKey(ref m, (Keys)m.WParam.ToInt32());
				}
				else if (ToolStripManager.ProcessMenuKey(ref m))
				{
					return;
				}
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000E541 File Offset: 0x0000C741
		private void WmKeys(ref Message m)
		{
			if (this.ProcessKeyMessage(ref m))
			{
				m.Result = IntPtr.Zero;
				return;
			}
			this.DefWndProc(ref m);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000E560 File Offset: 0x0000C760
		private void WmHelp(ref Message m)
		{
			Point mousePosition;
			if (m.LParam != IntPtr.Zero)
			{
				HELPINFO helpinfo = default(HELPINFO);
				helpinfo = (HELPINFO)Marshal.PtrToStructure(m.LParam, typeof(HELPINFO));
				mousePosition = new Point(helpinfo.MousePos.x, helpinfo.MousePos.y);
			}
			else
			{
				mousePosition = Control.MousePosition;
			}
			this.OnHelpRequested(new HelpEventArgs(mousePosition));
			m.Result = (IntPtr)1;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000E5DF File Offset: 0x0000C7DF
		private void WmKillFocus(ref Message m)
		{
			this.has_focus = false;
			this.OnLostFocus(EventArgs.Empty);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0000E5F3 File Offset: 0x0000C7F3
		private void WmSetFocus(ref Message m)
		{
			if (!this.has_focus)
			{
				this.has_focus = true;
				this.OnGotFocus(EventArgs.Empty);
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0000E60F File Offset: 0x0000C80F
		private void WmSysColorChange(ref Message m)
		{
			ThemeEngine.Current.ResetDefaults();
			this.OnSystemColorsChanged(EventArgs.Empty);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0000E628 File Offset: 0x0000C828
		private void WmSetCursor(ref Message m)
		{
			if ((this.cursor == null && !this.use_wait_cursor) || (m.LParam.ToInt32() & 65535) != 1)
			{
				this.DefWndProc(ref m);
				return;
			}
			XplatUI.SetCursor(this.window.Handle, this.Cursor.handle);
			m.Result = (IntPtr)1;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000E691 File Offset: 0x0000C891
		private void WmCaptureChanged(ref Message m)
		{
			this.is_captured = false;
			this.OnMouseCaptureChanged(EventArgs.Empty);
			m.Result = (IntPtr)0;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		private void WmChangeUIState(ref Message m)
		{
			foreach (object obj in this.Controls)
			{
				XplatUI.SendMessage(((Control)obj).Handle, Msg.WM_UPDATEUISTATE, m.WParam, m.LParam);
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000E724 File Offset: 0x0000C924
		private void WmUpdateUIState(ref Message m)
		{
			int num = Control.LowOrder(m.WParam.ToInt32());
			int num2 = Control.HighOrder((long)m.WParam.ToInt32());
			if (num == 3)
			{
				return;
			}
			UICues uicues = UICues.None;
			if ((num2 & 2) != 0 && num == 2 != this.show_keyboard_cues)
			{
				uicues |= UICues.ChangeKeyboard;
				this.show_keyboard_cues = num == 2;
			}
			if ((num2 & 1) != 0 && num == 2 != this.show_focus_cues)
			{
				uicues |= UICues.ChangeFocus;
				this.show_focus_cues = num == 2;
			}
			if ((uicues & UICues.Changed) != UICues.None)
			{
				this.OnChangeUICues(new UICuesEventArgs(uicues));
				this.Invalidate();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.AutoSizeChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600037B RID: 891 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		protected virtual void OnAutoSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.AutoSizeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600037C RID: 892 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBackColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.BackColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentBackColorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackgroundImageLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600037D RID: 893 RVA: 0x0000E840 File Offset: 0x0000CA40
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBackgroundImageLayoutChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.BackgroundImageLayoutChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600037E RID: 894 RVA: 0x0000E870 File Offset: 0x0000CA70
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnBindingContextChanged(EventArgs e)
		{
			this.CheckDataBindings();
			EventHandler eventHandler = (EventHandler)base.Events[Control.BindingContextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentBindingContextChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CausesValidationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600037F RID: 895 RVA: 0x0000E8CC File Offset: 0x0000CACC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnCausesValidationChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.CausesValidationChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ChangeUICues" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.UICuesEventArgs" /> that contains the event data. </param>
		// Token: 0x06000380 RID: 896 RVA: 0x0000E8FC File Offset: 0x0000CAFC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnChangeUICues(UICuesEventArgs e)
		{
			UICuesEventHandler uicuesEventHandler = (UICuesEventHandler)base.Events[Control.ChangeUICuesEvent];
			if (uicuesEventHandler != null)
			{
				uicuesEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000381 RID: 897 RVA: 0x0000E92C File Offset: 0x0000CB2C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ClientSizeChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000382 RID: 898 RVA: 0x0000E95C File Offset: 0x0000CB5C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnClientSizeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ClientSizeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ContextMenuChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000383 RID: 899 RVA: 0x0000E98C File Offset: 0x0000CB8C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnContextMenuChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ContextMenuChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlAdded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data. </param>
		// Token: 0x06000384 RID: 900 RVA: 0x0000E9BC File Offset: 0x0000CBBC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnControlAdded(ControlEventArgs e)
		{
			ControlEventHandler controlEventHandler = (ControlEventHandler)base.Events[Control.ControlAddedEvent];
			if (controlEventHandler != null)
			{
				controlEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ControlRemoved" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data. </param>
		// Token: 0x06000385 RID: 901 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnControlRemoved(ControlEventArgs e)
		{
			ControlEventHandler controlEventHandler = (ControlEventHandler)base.Events[Control.ControlRemovedEvent];
			if (controlEventHandler != null)
			{
				controlEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.CreateControl" /> method.</summary>
		// Token: 0x06000386 RID: 902 RVA: 0x0000493C File Offset: 0x00002B3C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnCreateControl()
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CursorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000387 RID: 903 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnCursorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.CursorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentCursorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DockChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000388 RID: 904 RVA: 0x0000EA74 File Offset: 0x0000CC74
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDockChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.DockChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DoubleClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000389 RID: 905 RVA: 0x0000EAA4 File Offset: 0x0000CCA4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDoubleClick(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.DoubleClickEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragDrop" /> event.</summary>
		/// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x0600038A RID: 906 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragDrop(DragEventArgs drgevent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[Control.DragDropEvent];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, drgevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragEnter" /> event.</summary>
		/// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x0600038B RID: 907 RVA: 0x0000EB04 File Offset: 0x0000CD04
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragEnter(DragEventArgs drgevent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[Control.DragEnterEvent];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, drgevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600038C RID: 908 RVA: 0x0000EB34 File Offset: 0x0000CD34
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.DragLeaveEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DragOver" /> event.</summary>
		/// <param name="drgevent">A <see cref="T:System.Windows.Forms.DragEventArgs" /> that contains the event data. </param>
		// Token: 0x0600038D RID: 909 RVA: 0x0000EB64 File Offset: 0x0000CD64
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnDragOver(DragEventArgs drgevent)
		{
			DragEventHandler dragEventHandler = (DragEventHandler)base.Events[Control.DragOverEvent];
			if (dragEventHandler != null)
			{
				dragEventHandler(this, drgevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600038E RID: 910 RVA: 0x0000EB94 File Offset: 0x0000CD94
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnEnabledChanged(EventArgs e)
		{
			if (this.IsHandleCreated)
			{
				if (this is Form)
				{
					if (((Form)this).context == null)
					{
						XplatUI.EnableWindow(this.window.Handle, this.Enabled);
					}
				}
				else
				{
					XplatUI.EnableWindow(this.window.Handle, this.Enabled);
				}
				this.Refresh();
			}
			EventHandler eventHandler = (EventHandler)base.Events[Control.EnabledChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			Control[] allControls = this.Controls.GetAllControls();
			for (int i = 0; i < allControls.Length; i++)
			{
				allControls[i].OnParentEnabledChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600038F RID: 911 RVA: 0x0000EC38 File Offset: 0x0000CE38
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnEnter(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EnterEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000390 RID: 912 RVA: 0x0000EC68 File Offset: 0x0000CE68
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnFontChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.FontChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentFontChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000391 RID: 913 RVA: 0x0000ECC0 File Offset: 0x0000CEC0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnForeColorChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ForeColorChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentForeColorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GiveFeedback" /> event.</summary>
		/// <param name="gfbevent">A <see cref="T:System.Windows.Forms.GiveFeedbackEventArgs" /> that contains the event data. </param>
		// Token: 0x06000392 RID: 914 RVA: 0x0000ED18 File Offset: 0x0000CF18
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
		{
			GiveFeedbackEventHandler giveFeedbackEventHandler = (GiveFeedbackEventHandler)base.Events[Control.GiveFeedbackEvent];
			if (giveFeedbackEventHandler != null)
			{
				giveFeedbackEventHandler(this, gfbevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000393 RID: 915 RVA: 0x0000ED48 File Offset: 0x0000CF48
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnGotFocus(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.GotFocusEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000394 RID: 916 RVA: 0x0000ED78 File Offset: 0x0000CF78
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnHandleCreated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.HandleCreatedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000395 RID: 917 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnHandleDestroyed(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.HandleDestroyedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
		internal void RaiseHelpRequested(HelpEventArgs hevent)
		{
			this.OnHelpRequested(hevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HelpRequested" /> event.</summary>
		/// <param name="hevent">A <see cref="T:System.Windows.Forms.HelpEventArgs" /> that contains the event data. </param>
		// Token: 0x06000397 RID: 919 RVA: 0x0000EDE0 File Offset: 0x0000CFE0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnHelpRequested(HelpEventArgs hevent)
		{
			HelpEventHandler helpEventHandler = (HelpEventHandler)base.Events[Control.HelpRequestedEvent];
			if (helpEventHandler != null)
			{
				helpEventHandler(this, hevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ImeModeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000398 RID: 920 RVA: 0x0000EE10 File Offset: 0x0000D010
		protected virtual void OnImeModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ImeModeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Invalidated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.InvalidateEventArgs" /> that contains the event data. </param>
		// Token: 0x06000399 RID: 921 RVA: 0x0000EE40 File Offset: 0x0000D040
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnInvalidated(InvalidateEventArgs e)
		{
			if (this.UseDoubleBuffering)
			{
				if (e.InvalidRect == this.ClientRectangle)
				{
					this.InvalidateBackBuffer();
				}
				else if (this.backbuffer != null)
				{
					Rectangle rectangle = Rectangle.Inflate(e.InvalidRect, 1, 1);
					this.backbuffer.InvalidRegion.Union(rectangle);
				}
			}
			InvalidateEventHandler invalidateEventHandler = (InvalidateEventHandler)base.Events[Control.InvalidatedEvent];
			if (invalidateEventHandler != null)
			{
				invalidateEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x0600039A RID: 922 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnKeyDown(KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[Control.KeyDownEvent];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data. </param>
		// Token: 0x0600039B RID: 923 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnKeyPress(KeyPressEventArgs e)
		{
			KeyPressEventHandler keyPressEventHandler = (KeyPressEventHandler)base.Events[Control.KeyPressEvent];
			if (keyPressEventHandler != null)
			{
				keyPressEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x0600039C RID: 924 RVA: 0x0000EF18 File Offset: 0x0000D118
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnKeyUp(KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[Control.KeyUpEvent];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="levent">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
		// Token: 0x0600039D RID: 925 RVA: 0x0000EF48 File Offset: 0x0000D148
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLayout(LayoutEventArgs levent)
		{
			LayoutEventHandler layoutEventHandler = (LayoutEventHandler)base.Events[Control.LayoutEvent];
			if (layoutEventHandler != null)
			{
				layoutEventHandler(this, levent);
			}
			Size size = this.Size;
			if (this.Parent != null && this.AutoSize && !this.nested_layout && this.PreferredSize != size)
			{
				this.nested_layout = true;
				this.Parent.PerformLayout();
				this.nested_layout = false;
			}
			this.LayoutEngine.Layout(this, levent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600039E RID: 926 RVA: 0x0000EFCC File Offset: 0x0000D1CC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.LeaveEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LocationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600039F RID: 927 RVA: 0x0000EFFC File Offset: 0x0000D1FC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLocationChanged(EventArgs e)
		{
			this.OnMove(e);
			EventHandler eventHandler = (EventHandler)base.Events[Control.LocationChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003A0 RID: 928 RVA: 0x0000F034 File Offset: 0x0000D234
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnLostFocus(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.LostFocusEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseCaptureChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003A1 RID: 929 RVA: 0x0000F064 File Offset: 0x0000D264
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseCaptureChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.MouseCaptureChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060003A2 RID: 930 RVA: 0x0000F094 File Offset: 0x0000D294
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseClick(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.MouseClickEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDoubleClick" /> event.</summary>
		/// <param name="e">An <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060003A3 RID: 931 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseDoubleClick(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.MouseDoubleClickEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060003A4 RID: 932 RVA: 0x0000F0F4 File Offset: 0x0000D2F4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseDown(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.MouseDownEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003A5 RID: 933 RVA: 0x0000F124 File Offset: 0x0000D324
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseEnter(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.MouseEnterEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseHover" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003A6 RID: 934 RVA: 0x0000F154 File Offset: 0x0000D354
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseHover(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.MouseHoverEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003A7 RID: 935 RVA: 0x0000F184 File Offset: 0x0000D384
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.MouseLeaveEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060003A8 RID: 936 RVA: 0x0000F1B4 File Offset: 0x0000D3B4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseMove(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.MouseMoveEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060003A9 RID: 937 RVA: 0x0000F1E4 File Offset: 0x0000D3E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseUp(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.MouseUpEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060003AA RID: 938 RVA: 0x0000F214 File Offset: 0x0000D414
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMouseWheel(MouseEventArgs e)
		{
			MouseEventHandler mouseEventHandler = (MouseEventHandler)base.Events[Control.MouseWheelEvent];
			if (mouseEventHandler != null)
			{
				mouseEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Move" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003AB RID: 939 RVA: 0x0000F244 File Offset: 0x0000D444
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnMove(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.MoveEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Notifies the control of Windows messages.</summary>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> that represents the Windows message. </param>
		// Token: 0x060003AC RID: 940 RVA: 0x0000493C File Offset: 0x00002B3C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnNotifyMessage(Message m)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060003AD RID: 941 RVA: 0x0000F274 File Offset: 0x0000D474
		protected virtual void OnPaddingChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.PaddingChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x060003AE RID: 942 RVA: 0x0000F2A4 File Offset: 0x0000D4A4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnPaint(PaintEventArgs e)
		{
			PaintEventHandler paintEventHandler = (PaintEventHandler)base.Events[Control.PaintEvent];
			if (paintEventHandler != null)
			{
				paintEventHandler(this, e);
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000493C File Offset: 0x00002B3C
		internal virtual void OnPaintBackgroundInternal(PaintEventArgs e)
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000493C File Offset: 0x00002B3C
		internal virtual void OnPaintInternal(PaintEventArgs e)
		{
		}

		/// <summary>Paints the background of the control.</summary>
		/// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the control to paint. </param>
		// Token: 0x060003B1 RID: 945 RVA: 0x0000F2D2 File Offset: 0x0000D4D2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnPaintBackground(PaintEventArgs pevent)
		{
			this.PaintControlBackground(pevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event when the <see cref="P:System.Windows.Forms.Control.BackColor" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003B2 RID: 946 RVA: 0x0000F2DB File Offset: 0x0000D4DB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentBackColorChanged(EventArgs e)
		{
			if (this.background_color.IsEmpty && this.background_image == null)
			{
				this.Invalidate();
				this.OnBackColorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BindingContextChanged" /> event when the <see cref="P:System.Windows.Forms.Control.BindingContext" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003B3 RID: 947 RVA: 0x0000F2FF File Offset: 0x0000D4FF
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentBindingContextChanged(EventArgs e)
		{
			if (this.binding_context == null && this.Parent != null)
			{
				this.binding_context = this.Parent.binding_context;
				this.OnBindingContextChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003B4 RID: 948 RVA: 0x0000F32C File Offset: 0x0000D52C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ParentChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.CursorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060003B5 RID: 949 RVA: 0x0000493C File Offset: 0x00002B3C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentCursorChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Enabled" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003B6 RID: 950 RVA: 0x0000F35A File Offset: 0x0000D55A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentEnabledChanged(EventArgs e)
		{
			if (this.is_enabled)
			{
				this.OnEnabledChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Font" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003B7 RID: 951 RVA: 0x0000F36B File Offset: 0x0000D56B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentFontChanged(EventArgs e)
		{
			if (this.font == null)
			{
				this.Invalidate();
				this.OnFontChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event when the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003B8 RID: 952 RVA: 0x0000F382 File Offset: 0x0000D582
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentForeColorChanged(EventArgs e)
		{
			if (this.foreground_color.IsEmpty)
			{
				this.Invalidate();
				this.OnForeColorChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event when the <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003B9 RID: 953 RVA: 0x0000F39E File Offset: 0x0000D59E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentRightToLeftChanged(EventArgs e)
		{
			if (this.right_to_left == RightToLeft.Inherit)
			{
				this.Invalidate();
				this.OnRightToLeftChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event when the <see cref="P:System.Windows.Forms.Control.Visible" /> property value of the control's container changes.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003BA RID: 954 RVA: 0x0000F3B6 File Offset: 0x0000D5B6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnParentVisibleChanged(EventArgs e)
		{
			if (this.is_visible)
			{
				this.OnVisibleChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.QueryContinueDrag" /> event.</summary>
		/// <param name="qcdevent">A <see cref="T:System.Windows.Forms.QueryContinueDragEventArgs" /> that contains the event data. </param>
		// Token: 0x060003BB RID: 955 RVA: 0x0000F3C8 File Offset: 0x0000D5C8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
		{
			QueryContinueDragEventHandler queryContinueDragEventHandler = (QueryContinueDragEventHandler)base.Events[Control.QueryContinueDragEvent];
			if (queryContinueDragEventHandler != null)
			{
				queryContinueDragEventHandler(this, qcdevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PreviewKeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PreviewKeyDownEventArgs" /> that contains the event data.</param>
		// Token: 0x060003BC RID: 956 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
		{
			PreviewKeyDownEventHandler previewKeyDownEventHandler = (PreviewKeyDownEventHandler)base.Events[Control.PreviewKeyDownEvent];
			if (previewKeyDownEventHandler != null)
			{
				previewKeyDownEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RegionChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060003BD RID: 957 RVA: 0x0000F428 File Offset: 0x0000D628
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRegionChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.RegionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003BE RID: 958 RVA: 0x0000F456 File Offset: 0x0000D656
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnResize(EventArgs e)
		{
			this.OnResizeInternal(e);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000F460 File Offset: 0x0000D660
		internal virtual void OnResizeInternal(EventArgs e)
		{
			this.PerformLayout(this, "Bounds");
			EventHandler eventHandler = (EventHandler)base.Events[Control.ResizeEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003C0 RID: 960 RVA: 0x0000F49C File Offset: 0x0000D69C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.RightToLeftChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			for (int i = 0; i < this.child_controls.Count; i++)
			{
				this.child_controls[i].OnParentRightToLeftChanged(e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003C1 RID: 961 RVA: 0x0000F4F4 File Offset: 0x0000D6F4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnSizeChanged(EventArgs e)
		{
			this.DisposeBackBuffer();
			this.OnResize(e);
			EventHandler eventHandler = (EventHandler)base.Events[Control.SizeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.StyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003C2 RID: 962 RVA: 0x0000F530 File Offset: 0x0000D730
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.StyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003C3 RID: 963 RVA: 0x0000F560 File Offset: 0x0000D760
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnSystemColorsChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.SystemColorsChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TabIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003C4 RID: 964 RVA: 0x0000F590 File Offset: 0x0000D790
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnTabIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.TabIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TabStopChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003C5 RID: 965 RVA: 0x0000F5C0 File Offset: 0x0000D7C0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnTabStopChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.TabStopChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003C6 RID: 966 RVA: 0x0000F5F0 File Offset: 0x0000D7F0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnTextChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.TextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003C7 RID: 967 RVA: 0x0000F620 File Offset: 0x0000D820
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnValidated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.ValidatedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Validating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060003C8 RID: 968 RVA: 0x0000F650 File Offset: 0x0000D850
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnValidating(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[Control.ValidatingEvent];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060003C9 RID: 969 RVA: 0x0000F680 File Offset: 0x0000D880
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnVisibleChanged(EventArgs e)
		{
			if (this.Visible)
			{
				this.CreateControl();
			}
			EventHandler eventHandler = (EventHandler)base.Events[Control.VisibleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
			foreach (Control control in this.Controls.GetAllControls())
			{
				if (control.Visible)
				{
					control.OnParentVisibleChanged(e);
				}
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Control.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060003CA RID: 970 RVA: 0x0000F6E9 File Offset: 0x0000D8E9
		// (remove) Token: 0x060003CB RID: 971 RVA: 0x0000F6FC File Offset: 0x0000D8FC
		public event EventHandler BackgroundImageChanged
		{
			add
			{
				base.Events.AddHandler(Control.BackgroundImageChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.BackgroundImageChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060003CC RID: 972 RVA: 0x0000F70F File Offset: 0x0000D90F
		// (remove) Token: 0x060003CD RID: 973 RVA: 0x0000F722 File Offset: 0x0000D922
		public event EventHandler Click
		{
			add
			{
				base.Events.AddHandler(Control.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ClickEvent, value);
			}
		}

		/// <summary>Occurs when a control is removed from the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060003CE RID: 974 RVA: 0x0000F735 File Offset: 0x0000D935
		// (remove) Token: 0x060003CF RID: 975 RVA: 0x0000F748 File Offset: 0x0000D948
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(true)]
		public event ControlEventHandler ControlRemoved
		{
			add
			{
				base.Events.AddHandler(Control.ControlRemovedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ControlRemovedEvent, value);
			}
		}

		/// <summary>Occurs when the control is double-clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400000A RID: 10
		// (add) Token: 0x060003D0 RID: 976 RVA: 0x0000F75B File Offset: 0x0000D95B
		// (remove) Token: 0x060003D1 RID: 977 RVA: 0x0000F76E File Offset: 0x0000D96E
		public event EventHandler DoubleClick
		{
			add
			{
				base.Events.AddHandler(Control.DoubleClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.DoubleClickEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Enabled" /> property value has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x060003D2 RID: 978 RVA: 0x0000F781 File Offset: 0x0000D981
		// (remove) Token: 0x060003D3 RID: 979 RVA: 0x0000F794 File Offset: 0x0000D994
		public event EventHandler EnabledChanged
		{
			add
			{
				base.Events.AddHandler(Control.EnabledChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.EnabledChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Font" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060003D4 RID: 980 RVA: 0x0000F7A7 File Offset: 0x0000D9A7
		// (remove) Token: 0x060003D5 RID: 981 RVA: 0x0000F7BA File Offset: 0x0000D9BA
		public event EventHandler FontChanged
		{
			add
			{
				base.Events.AddHandler(Control.FontChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.FontChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.ForeColor" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060003D6 RID: 982 RVA: 0x0000F7CD File Offset: 0x0000D9CD
		// (remove) Token: 0x060003D7 RID: 983 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		public event EventHandler ForeColorChanged
		{
			add
			{
				base.Events.AddHandler(Control.ForeColorChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ForeColorChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control receives focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060003D8 RID: 984 RVA: 0x0000F7F3 File Offset: 0x0000D9F3
		// (remove) Token: 0x060003D9 RID: 985 RVA: 0x0000F806 File Offset: 0x0000DA06
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event EventHandler GotFocus
		{
			add
			{
				base.Events.AddHandler(Control.GotFocusEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.GotFocusEvent, value);
			}
		}

		/// <summary>Occurs when a handle is created for the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060003DA RID: 986 RVA: 0x0000F819 File Offset: 0x0000DA19
		// (remove) Token: 0x060003DB RID: 987 RVA: 0x0000F82C File Offset: 0x0000DA2C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event EventHandler HandleCreated
		{
			add
			{
				base.Events.AddHandler(Control.HandleCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.HandleCreatedEvent, value);
			}
		}

		/// <summary>Occurs when the control's handle is in the process of being destroyed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060003DC RID: 988 RVA: 0x0000F83F File Offset: 0x0000DA3F
		// (remove) Token: 0x060003DD RID: 989 RVA: 0x0000F852 File Offset: 0x0000DA52
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event EventHandler HandleDestroyed
		{
			add
			{
				base.Events.AddHandler(Control.HandleDestroyedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.HandleDestroyedEvent, value);
			}
		}

		/// <summary>Occurs when a control's display requires redrawing.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060003DE RID: 990 RVA: 0x0000F865 File Offset: 0x0000DA65
		// (remove) Token: 0x060003DF RID: 991 RVA: 0x0000F878 File Offset: 0x0000DA78
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event InvalidateEventHandler Invalidated
		{
			add
			{
				base.Events.AddHandler(Control.InvalidatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.InvalidatedEvent, value);
			}
		}

		/// <summary>Occurs when a key is pressed while the control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060003E0 RID: 992 RVA: 0x0000F88B File Offset: 0x0000DA8B
		// (remove) Token: 0x060003E1 RID: 993 RVA: 0x0000F89E File Offset: 0x0000DA9E
		public event KeyEventHandler KeyDown
		{
			add
			{
				base.Events.AddHandler(Control.KeyDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.KeyDownEvent, value);
			}
		}

		/// <summary>Occurs when a key is pressed while the control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060003E2 RID: 994 RVA: 0x0000F8B1 File Offset: 0x0000DAB1
		// (remove) Token: 0x060003E3 RID: 995 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
		public event KeyPressEventHandler KeyPress
		{
			add
			{
				base.Events.AddHandler(Control.KeyPressEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.KeyPressEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Location" /> property value has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060003E4 RID: 996 RVA: 0x0000F8D7 File Offset: 0x0000DAD7
		// (remove) Token: 0x060003E5 RID: 997 RVA: 0x0000F8EA File Offset: 0x0000DAEA
		public event EventHandler LocationChanged
		{
			add
			{
				base.Events.AddHandler(Control.LocationChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.LocationChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control loses focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060003E6 RID: 998 RVA: 0x0000F8FD File Offset: 0x0000DAFD
		// (remove) Token: 0x060003E7 RID: 999 RVA: 0x0000F910 File Offset: 0x0000DB10
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event EventHandler LostFocus
		{
			add
			{
				base.Events.AddHandler(Control.LostFocusEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.LostFocusEvent, value);
			}
		}

		/// <summary>Occurs when the control loses mouse capture.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060003E8 RID: 1000 RVA: 0x0000F923 File Offset: 0x0000DB23
		// (remove) Token: 0x060003E9 RID: 1001 RVA: 0x0000F936 File Offset: 0x0000DB36
		public event EventHandler MouseCaptureChanged
		{
			add
			{
				base.Events.AddHandler(Control.MouseCaptureChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseCaptureChangedEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is over the control and a mouse button is pressed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060003EA RID: 1002 RVA: 0x0000F949 File Offset: 0x0000DB49
		// (remove) Token: 0x060003EB RID: 1003 RVA: 0x0000F95C File Offset: 0x0000DB5C
		public event MouseEventHandler MouseDown
		{
			add
			{
				base.Events.AddHandler(Control.MouseDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseDownEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer enters the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060003EC RID: 1004 RVA: 0x0000F96F File Offset: 0x0000DB6F
		// (remove) Token: 0x060003ED RID: 1005 RVA: 0x0000F982 File Offset: 0x0000DB82
		public event EventHandler MouseEnter
		{
			add
			{
				base.Events.AddHandler(Control.MouseEnterEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseEnterEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer rests on the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060003EE RID: 1006 RVA: 0x0000F995 File Offset: 0x0000DB95
		// (remove) Token: 0x060003EF RID: 1007 RVA: 0x0000F9A8 File Offset: 0x0000DBA8
		public event EventHandler MouseHover
		{
			add
			{
				base.Events.AddHandler(Control.MouseHoverEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseHoverEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer leaves the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060003F0 RID: 1008 RVA: 0x0000F9BB File Offset: 0x0000DBBB
		// (remove) Token: 0x060003F1 RID: 1009 RVA: 0x0000F9CE File Offset: 0x0000DBCE
		public event EventHandler MouseLeave
		{
			add
			{
				base.Events.AddHandler(Control.MouseLeaveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseLeaveEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is moved over the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060003F2 RID: 1010 RVA: 0x0000F9E1 File Offset: 0x0000DBE1
		// (remove) Token: 0x060003F3 RID: 1011 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		public event MouseEventHandler MouseMove
		{
			add
			{
				base.Events.AddHandler(Control.MouseMoveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseMoveEvent, value);
			}
		}

		/// <summary>Occurs when the mouse pointer is over the control and a mouse button is released.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060003F4 RID: 1012 RVA: 0x0000FA07 File Offset: 0x0000DC07
		// (remove) Token: 0x060003F5 RID: 1013 RVA: 0x0000FA1A File Offset: 0x0000DC1A
		public event MouseEventHandler MouseUp
		{
			add
			{
				base.Events.AddHandler(Control.MouseUpEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseUpEvent, value);
			}
		}

		/// <summary>Occurs when the mouse wheel moves while the control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060003F6 RID: 1014 RVA: 0x0000FA2D File Offset: 0x0000DC2D
		// (remove) Token: 0x060003F7 RID: 1015 RVA: 0x0000FA40 File Offset: 0x0000DC40
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[Browsable(false)]
		public event MouseEventHandler MouseWheel
		{
			add
			{
				base.Events.AddHandler(Control.MouseWheelEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.MouseWheelEvent, value);
			}
		}

		/// <summary>Occurs when the control's padding changes.</summary>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060003F8 RID: 1016 RVA: 0x0000FA53 File Offset: 0x0000DC53
		// (remove) Token: 0x060003F9 RID: 1017 RVA: 0x0000FA66 File Offset: 0x0000DC66
		public event EventHandler PaddingChanged
		{
			add
			{
				base.Events.AddHandler(Control.PaddingChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.PaddingChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control is redrawn.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060003FA RID: 1018 RVA: 0x0000FA79 File Offset: 0x0000DC79
		// (remove) Token: 0x060003FB RID: 1019 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		public event PaintEventHandler Paint
		{
			add
			{
				base.Events.AddHandler(Control.PaintEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.PaintEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Parent" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060003FC RID: 1020 RVA: 0x0000FA9F File Offset: 0x0000DC9F
		// (remove) Token: 0x060003FD RID: 1021 RVA: 0x0000FAB2 File Offset: 0x0000DCB2
		public event EventHandler ParentChanged
		{
			add
			{
				base.Events.AddHandler(Control.ParentChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ParentChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control is resized.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060003FE RID: 1022 RVA: 0x0000FAC5 File Offset: 0x0000DCC5
		// (remove) Token: 0x060003FF RID: 1023 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public event EventHandler Resize
		{
			add
			{
				base.Events.AddHandler(Control.ResizeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ResizeEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.RightToLeft" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06000400 RID: 1024 RVA: 0x0000FAEB File Offset: 0x0000DCEB
		// (remove) Token: 0x06000401 RID: 1025 RVA: 0x0000FAFE File Offset: 0x0000DCFE
		public event EventHandler RightToLeftChanged
		{
			add
			{
				base.Events.AddHandler(Control.RightToLeftChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.RightToLeftChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Size" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06000402 RID: 1026 RVA: 0x0000FB11 File Offset: 0x0000DD11
		// (remove) Token: 0x06000403 RID: 1027 RVA: 0x0000FB24 File Offset: 0x0000DD24
		public event EventHandler SizeChanged
		{
			add
			{
				base.Events.AddHandler(Control.SizeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.SizeChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Text" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x06000404 RID: 1028 RVA: 0x0000FB37 File Offset: 0x0000DD37
		// (remove) Token: 0x06000405 RID: 1029 RVA: 0x0000FB4A File Offset: 0x0000DD4A
		public event EventHandler TextChanged
		{
			add
			{
				base.Events.AddHandler(Control.TextChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.TextChangedEvent, value);
			}
		}

		/// <summary>Occurs when the control is validating.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06000406 RID: 1030 RVA: 0x0000FB5D File Offset: 0x0000DD5D
		// (remove) Token: 0x06000407 RID: 1031 RVA: 0x0000FB70 File Offset: 0x0000DD70
		public event CancelEventHandler Validating
		{
			add
			{
				base.Events.AddHandler(Control.ValidatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.ValidatingEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.Control.Visible" /> property value changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06000408 RID: 1032 RVA: 0x0000FB83 File Offset: 0x0000DD83
		// (remove) Token: 0x06000409 RID: 1033 RVA: 0x0000FB96 File Offset: 0x0000DD96
		public event EventHandler VisibleChanged
		{
			add
			{
				base.Events.AddHandler(Control.VisibleChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Control.VisibleChangedEvent, value);
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000FBAC File Offset: 0x0000DDAC
		// Note: this type is marked as 'beforefieldinit'.
		static Control()
		{
			Control.BackgroundImageChangedEvent = new object();
			Control.BackgroundImageLayoutChangedEvent = new object();
			Control.BindingContextChangedEvent = new object();
			Control.CausesValidationChangedEvent = new object();
			Control.ChangeUICuesEvent = new object();
			Control.ClickEvent = new object();
			Control.ClientSizeChangedEvent = new object();
			Control.ContextMenuChangedEvent = new object();
			Control.ContextMenuStripChangedEvent = new object();
			Control.ControlAddedEvent = new object();
			Control.ControlRemovedEvent = new object();
			Control.CursorChangedEvent = new object();
			Control.DockChangedEvent = new object();
			Control.DoubleClickEvent = new object();
			Control.DragDropEvent = new object();
			Control.DragEnterEvent = new object();
			Control.DragLeaveEvent = new object();
			Control.DragOverEvent = new object();
			Control.EnabledChangedEvent = new object();
			Control.EnterEvent = new object();
			Control.FontChangedEvent = new object();
			Control.ForeColorChangedEvent = new object();
			Control.GiveFeedbackEvent = new object();
			Control.GotFocusEvent = new object();
			Control.HandleCreatedEvent = new object();
			Control.HandleDestroyedEvent = new object();
			Control.HelpRequestedEvent = new object();
			Control.ImeModeChangedEvent = new object();
			Control.InvalidatedEvent = new object();
			Control.KeyDownEvent = new object();
			Control.KeyPressEvent = new object();
			Control.KeyUpEvent = new object();
			Control.LayoutEvent = new object();
			Control.LeaveEvent = new object();
			Control.LocationChangedEvent = new object();
			Control.LostFocusEvent = new object();
			Control.MarginChangedEvent = new object();
			Control.MouseCaptureChangedEvent = new object();
			Control.MouseClickEvent = new object();
			Control.MouseDoubleClickEvent = new object();
			Control.MouseDownEvent = new object();
			Control.MouseEnterEvent = new object();
			Control.MouseHoverEvent = new object();
			Control.MouseLeaveEvent = new object();
			Control.MouseMoveEvent = new object();
			Control.MouseUpEvent = new object();
			Control.MouseWheelEvent = new object();
			Control.MoveEvent = new object();
			Control.PaddingChangedEvent = new object();
			Control.PaintEvent = new object();
			Control.ParentChangedEvent = new object();
			Control.PreviewKeyDownEvent = new object();
			Control.QueryAccessibilityHelpEvent = new object();
			Control.QueryContinueDragEvent = new object();
			Control.RegionChangedEvent = new object();
			Control.ResizeEvent = new object();
			Control.RightToLeftChangedEvent = new object();
			Control.SizeChangedEvent = new object();
			Control.StyleChangedEvent = new object();
			Control.SystemColorsChangedEvent = new object();
			Control.TabIndexChangedEvent = new object();
			Control.TabStopChangedEvent = new object();
			Control.TextChangedEvent = new object();
			Control.ValidatedEvent = new object();
			Control.ValidatingEvent = new object();
			Control.VisibleChangedEvent = new object();
		}

		// Token: 0x04000192 RID: 402
		internal Rectangle bounds;

		// Token: 0x04000193 RID: 403
		private Rectangle explicit_bounds;

		// Token: 0x04000194 RID: 404
		internal object creator_thread;

		// Token: 0x04000195 RID: 405
		internal Control.ControlNativeWindow window;

		// Token: 0x04000196 RID: 406
		private IWindowTarget window_target;

		// Token: 0x04000197 RID: 407
		private string name;

		// Token: 0x04000198 RID: 408
		private bool is_created;

		// Token: 0x04000199 RID: 409
		internal bool has_focus;

		// Token: 0x0400019A RID: 410
		internal bool is_visible;

		// Token: 0x0400019B RID: 411
		internal bool is_entered;

		// Token: 0x0400019C RID: 412
		internal bool is_enabled;

		// Token: 0x0400019D RID: 413
		private bool is_captured;

		// Token: 0x0400019E RID: 414
		internal bool is_toplevel;

		// Token: 0x0400019F RID: 415
		private bool is_recreating;

		// Token: 0x040001A0 RID: 416
		private bool causes_validation;

		// Token: 0x040001A1 RID: 417
		private bool is_focusing;

		// Token: 0x040001A2 RID: 418
		private int tab_index;

		// Token: 0x040001A3 RID: 419
		private bool tab_stop;

		// Token: 0x040001A4 RID: 420
		private bool is_disposed;

		// Token: 0x040001A5 RID: 421
		private bool is_disposing;

		// Token: 0x040001A6 RID: 422
		private Size client_size;

		// Token: 0x040001A7 RID: 423
		private Rectangle client_rect;

		// Token: 0x040001A8 RID: 424
		private ControlStyles control_style;

		// Token: 0x040001A9 RID: 425
		private ImeMode ime_mode;

		// Token: 0x040001AA RID: 426
		internal int mouse_clicks;

		// Token: 0x040001AB RID: 427
		private Cursor cursor;

		// Token: 0x040001AC RID: 428
		internal bool allow_drop;

		// Token: 0x040001AD RID: 429
		private Region clip_region;

		// Token: 0x040001AE RID: 430
		internal Color foreground_color;

		// Token: 0x040001AF RID: 431
		internal Color background_color;

		// Token: 0x040001B0 RID: 432
		private Image background_image;

		// Token: 0x040001B1 RID: 433
		internal Font font;

		// Token: 0x040001B2 RID: 434
		private string text;

		// Token: 0x040001B3 RID: 435
		internal BorderStyle border_style;

		// Token: 0x040001B4 RID: 436
		private bool show_keyboard_cues;

		// Token: 0x040001B5 RID: 437
		internal bool show_focus_cues;

		// Token: 0x040001B6 RID: 438
		internal bool force_double_buffer;

		// Token: 0x040001B7 RID: 439
		private LayoutEngine layout_engine;

		// Token: 0x040001B8 RID: 440
		internal int layout_suspended;

		// Token: 0x040001B9 RID: 441
		private bool layout_pending;

		// Token: 0x040001BA RID: 442
		internal AnchorStyles anchor_style;

		// Token: 0x040001BB RID: 443
		internal DockStyle dock_style;

		// Token: 0x040001BC RID: 444
		private Control.LayoutType layout_type;

		// Token: 0x040001BD RID: 445
		private bool recalculate_distances = true;

		// Token: 0x040001BE RID: 446
		internal int dist_right;

		// Token: 0x040001BF RID: 447
		internal int dist_bottom;

		// Token: 0x040001C0 RID: 448
		private Control.ControlCollection child_controls;

		// Token: 0x040001C1 RID: 449
		private Control parent;

		// Token: 0x040001C2 RID: 450
		private BindingContext binding_context;

		// Token: 0x040001C3 RID: 451
		private RightToLeft right_to_left;

		// Token: 0x040001C4 RID: 452
		private ContextMenu context_menu;

		// Token: 0x040001C5 RID: 453
		internal bool use_compatible_text_rendering;

		// Token: 0x040001C6 RID: 454
		private bool use_wait_cursor;

		// Token: 0x040001C7 RID: 455
		private AccessibleRole accessible_role = AccessibleRole.Default;

		// Token: 0x040001C8 RID: 456
		private Control.DoubleBuffer backbuffer;

		// Token: 0x040001C9 RID: 457
		private ControlBindingsCollection data_bindings;

		// Token: 0x040001CA RID: 458
		private static bool verify_thread_handle;

		// Token: 0x040001CB RID: 459
		private Padding padding;

		// Token: 0x040001CC RID: 460
		private ImageLayout backgroundimage_layout;

		// Token: 0x040001CD RID: 461
		private Size maximum_size;

		// Token: 0x040001CE RID: 462
		private Size minimum_size;

		// Token: 0x040001CF RID: 463
		private Padding margin;

		// Token: 0x040001D0 RID: 464
		private ContextMenuStrip context_menu_strip;

		// Token: 0x040001D1 RID: 465
		private bool nested_layout;

		// Token: 0x040001D2 RID: 466
		private AutoSizeMode auto_size_mode;

		// Token: 0x040001D3 RID: 467
		private bool suppressing_key_press;

		// Token: 0x040001D4 RID: 468
		private MenuTracker active_tracker;

		// Token: 0x040001D5 RID: 469
		private bool auto_size;

		// Token: 0x040001D6 RID: 470
		private static object AutoSizeChangedEvent = new object();

		// Token: 0x040001D7 RID: 471
		private static object BackColorChangedEvent = new object();

		// Token: 0x040001D9 RID: 473
		private static object BackgroundImageLayoutChangedEvent;

		// Token: 0x040001DA RID: 474
		private static object BindingContextChangedEvent;

		// Token: 0x040001DB RID: 475
		private static object CausesValidationChangedEvent;

		// Token: 0x040001DC RID: 476
		private static object ChangeUICuesEvent;

		// Token: 0x040001DE RID: 478
		private static object ClientSizeChangedEvent;

		// Token: 0x040001DF RID: 479
		private static object ContextMenuChangedEvent;

		// Token: 0x040001E0 RID: 480
		private static object ContextMenuStripChangedEvent;

		// Token: 0x040001E1 RID: 481
		private static object ControlAddedEvent;

		// Token: 0x040001E3 RID: 483
		private static object CursorChangedEvent;

		// Token: 0x040001E4 RID: 484
		private static object DockChangedEvent;

		// Token: 0x040001E6 RID: 486
		private static object DragDropEvent;

		// Token: 0x040001E7 RID: 487
		private static object DragEnterEvent;

		// Token: 0x040001E8 RID: 488
		private static object DragLeaveEvent;

		// Token: 0x040001E9 RID: 489
		private static object DragOverEvent;

		// Token: 0x040001EB RID: 491
		private static object EnterEvent;

		// Token: 0x040001EE RID: 494
		private static object GiveFeedbackEvent;

		// Token: 0x040001F2 RID: 498
		private static object HelpRequestedEvent;

		// Token: 0x040001F3 RID: 499
		private static object ImeModeChangedEvent;

		// Token: 0x040001F7 RID: 503
		private static object KeyUpEvent;

		// Token: 0x040001F8 RID: 504
		private static object LayoutEvent;

		// Token: 0x040001F9 RID: 505
		private static object LeaveEvent;

		// Token: 0x040001FC RID: 508
		private static object MarginChangedEvent;

		// Token: 0x040001FE RID: 510
		private static object MouseClickEvent;

		// Token: 0x040001FF RID: 511
		private static object MouseDoubleClickEvent;

		// Token: 0x04000207 RID: 519
		private static object MoveEvent;

		// Token: 0x0400020B RID: 523
		private static object PreviewKeyDownEvent;

		// Token: 0x0400020C RID: 524
		private static object QueryAccessibilityHelpEvent;

		// Token: 0x0400020D RID: 525
		private static object QueryContinueDragEvent;

		// Token: 0x0400020E RID: 526
		private static object RegionChangedEvent;

		// Token: 0x04000212 RID: 530
		private static object StyleChangedEvent;

		// Token: 0x04000213 RID: 531
		private static object SystemColorsChangedEvent;

		// Token: 0x04000214 RID: 532
		private static object TabIndexChangedEvent;

		// Token: 0x04000215 RID: 533
		private static object TabStopChangedEvent;

		// Token: 0x04000217 RID: 535
		private static object ValidatedEvent;

		// Token: 0x0200004E RID: 78
		internal enum LayoutType
		{
			// Token: 0x0400021B RID: 539
			Anchor,
			// Token: 0x0400021C RID: 540
			Dock
		}

		// Token: 0x0200004F RID: 79
		internal class ControlNativeWindow : NativeWindow
		{
			// Token: 0x0600040B RID: 1035 RVA: 0x0000FE61 File Offset: 0x0000E061
			public ControlNativeWindow(Control control)
			{
				this.owner = control;
			}

			// Token: 0x170000F2 RID: 242
			// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000FE70 File Offset: 0x0000E070
			public Control Owner
			{
				get
				{
					return this.owner;
				}
			}

			// Token: 0x0600040D RID: 1037 RVA: 0x0000FE78 File Offset: 0x0000E078
			protected override void OnHandleChange()
			{
				this.owner.WindowTarget.OnHandleChange(this.owner.Handle);
			}

			// Token: 0x0600040E RID: 1038 RVA: 0x0000FE98 File Offset: 0x0000E098
			internal static Control ControlFromHandle(IntPtr hWnd)
			{
				Control.ControlNativeWindow controlNativeWindow = (Control.ControlNativeWindow)NativeWindow.FromHandle(hWnd);
				if (controlNativeWindow != null)
				{
					return controlNativeWindow.owner;
				}
				return null;
			}

			// Token: 0x0600040F RID: 1039 RVA: 0x0000FEBC File Offset: 0x0000E0BC
			protected override void WndProc(ref Message m)
			{
				this.owner.WindowTarget.OnMessage(ref m);
			}

			// Token: 0x0400021D RID: 541
			private Control owner;
		}

		// Token: 0x02000050 RID: 80
		private class ControlWindowTarget : IWindowTarget
		{
			// Token: 0x06000410 RID: 1040 RVA: 0x0000FECF File Offset: 0x0000E0CF
			public ControlWindowTarget(Control control)
			{
				this.control = control;
			}

			// Token: 0x06000411 RID: 1041 RVA: 0x0000493C File Offset: 0x00002B3C
			public void OnHandleChange(IntPtr newHandle)
			{
			}

			// Token: 0x06000412 RID: 1042 RVA: 0x0000FEDE File Offset: 0x0000E0DE
			public void OnMessage(ref Message m)
			{
				this.control.WndProc(ref m);
			}

			// Token: 0x0400021E RID: 542
			private Control control;
		}

		// Token: 0x02000051 RID: 81
		private class DoubleBuffer : IDisposable
		{
			// Token: 0x06000413 RID: 1043 RVA: 0x0000FEEC File Offset: 0x0000E0EC
			public DoubleBuffer(Control parent)
			{
				this.parent = parent;
				this.real_graphics = new Stack();
				int num = parent.Width;
				int num2 = parent.Height;
				if (num < 1)
				{
					num = 1;
				}
				if (num2 < 1)
				{
					num2 = 1;
				}
				XplatUI.CreateOffscreenDrawable(parent.Handle, num, num2, out this.back_buffer);
				this.Invalidate();
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x0000FF44 File Offset: 0x0000E144
			public void Start(PaintEventArgs pe)
			{
				this.real_graphics.Push(pe.SetGraphics(XplatUI.GetOffscreenGraphics(this.back_buffer)));
			}

			// Token: 0x06000415 RID: 1045 RVA: 0x0000FF64 File Offset: 0x0000E164
			public void End(PaintEventArgs pe)
			{
				Graphics graphics = pe.SetGraphics((Graphics)this.real_graphics.Pop());
				if (this.pending_disposal)
				{
					this.Dispose();
				}
				else
				{
					XplatUI.BlitFromOffscreen(this.parent.Handle, pe.Graphics, this.back_buffer, graphics, pe.ClipRectangle);
					this.InvalidRegion.Exclude(pe.ClipRectangle);
				}
				graphics.Dispose();
			}

			// Token: 0x06000416 RID: 1046 RVA: 0x0000FFD2 File Offset: 0x0000E1D2
			public void Invalidate()
			{
				if (this.InvalidRegion != null)
				{
					this.InvalidRegion.Dispose();
				}
				this.InvalidRegion = new Region(this.parent.ClientRectangle);
			}

			// Token: 0x06000417 RID: 1047 RVA: 0x00010000 File Offset: 0x0000E200
			public void Dispose()
			{
				if (this.real_graphics.Count > 0)
				{
					this.pending_disposal = true;
					return;
				}
				XplatUI.DestroyOffscreenDrawable(this.back_buffer);
				if (this.InvalidRegion != null)
				{
					this.InvalidRegion.Dispose();
				}
				this.InvalidRegion = null;
				this.back_buffer = null;
				GC.SuppressFinalize(this);
			}

			// Token: 0x06000418 RID: 1048 RVA: 0x00010055 File Offset: 0x0000E255
			void IDisposable.Dispose()
			{
				this.Dispose();
			}

			// Token: 0x06000419 RID: 1049 RVA: 0x00010060 File Offset: 0x0000E260
			~DoubleBuffer()
			{
				this.Dispose();
			}

			// Token: 0x0400021F RID: 543
			public Region InvalidRegion;

			// Token: 0x04000220 RID: 544
			private Stack real_graphics;

			// Token: 0x04000221 RID: 545
			private object back_buffer;

			// Token: 0x04000222 RID: 546
			private Control parent;

			// Token: 0x04000223 RID: 547
			private bool pending_disposal;
		}

		/// <summary>Represents a collection of <see cref="T:System.Windows.Forms.Control" /> objects.</summary>
		// Token: 0x02000052 RID: 82
		[ListBindable(false)]
		[ComVisible(false)]
		public class ControlCollection : ArrangedElementCollection, IList, ICollection, IEnumerable, ICloneable
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Control.ControlCollection" /> class.</summary>
			/// <param name="owner">A <see cref="T:System.Windows.Forms.Control" /> representing the control that owns the control collection. </param>
			// Token: 0x0600041A RID: 1050 RVA: 0x0001008C File Offset: 0x0000E28C
			public ControlCollection(Control owner)
			{
				this.owner = owner;
			}

			/// <summary>Indicates the <see cref="T:System.Windows.Forms.Control" /> at the specified indexed location in the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.Control" /> located at the specified index location within the control collection.</returns>
			/// <param name="index">The index of the control to retrieve from the control collection. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than zero or is greater than or equal to the number of controls in the collection. </exception>
			// Token: 0x170000F3 RID: 243
			public virtual Control this[int index]
			{
				get
				{
					if (index < 0 || index >= this.list.Count)
					{
						throw new ArgumentOutOfRangeException("index", index, "ControlCollection does not have that many controls");
					}
					return (Control)this.list[index];
				}
			}

			/// <summary>Adds the specified control to the control collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to add to the control collection. </param>
			/// <exception cref="T:System.Exception">The specified control is a top-level control, or a circular control reference would result if this control were added to the control collection. </exception>
			/// <exception cref="T:System.ArgumentException">The object assigned to the <paramref name="value" /> parameter is not a <see cref="T:System.Windows.Forms.Control" />. </exception>
			// Token: 0x0600041C RID: 1052 RVA: 0x000100D8 File Offset: 0x0000E2D8
			public virtual void Add(Control value)
			{
				if (value == null)
				{
					return;
				}
				Form form = value as Form;
				Form form2 = this.owner as Form;
				bool flag = this.owner is MdiClient || (form2 != null && form2.IsMdiContainer);
				bool topLevel = value.GetTopLevel();
				bool flag2 = form != null && form.IsMdiChild;
				if (topLevel && (!flag || !flag2))
				{
					throw new ArgumentException("Cannot add a top level control to a control.", "value");
				}
				if (flag2 && form.MdiParent != null && form.MdiParent != this.owner && form.MdiParent != this.owner.Parent)
				{
					throw new ArgumentException("Form cannot be added to the Controls collection that has a valid MDI parent.", "value");
				}
				value.recalculate_distances = true;
				if (this.Contains(value))
				{
					this.owner.PerformLayout();
					return;
				}
				if (value.tab_index == -1)
				{
					int num = 0;
					int count = this.owner.child_controls.Count;
					for (int i = 0; i < count; i++)
					{
						int tab_index = this.owner.child_controls[i].tab_index;
						if (tab_index >= num)
						{
							num = tab_index + 1;
						}
					}
					value.tab_index = num;
				}
				if (value.parent != null)
				{
					value.parent.Controls.Remove(value);
				}
				this.all_controls = null;
				this.list.Add(value);
				value.ChangeParent(this.owner);
				value.InitLayout();
				if (this.owner.Visible)
				{
					this.owner.UpdateChildrenZOrder();
				}
				this.owner.PerformLayout(value, "Parent");
				this.owner.OnControlAdded(new ControlEventArgs(value));
			}

			// Token: 0x0600041D RID: 1053 RVA: 0x00010270 File Offset: 0x0000E470
			internal void AddToList(Control c)
			{
				this.all_controls = null;
				this.list.Add(c);
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x00010288 File Offset: 0x0000E488
			internal virtual void AddImplicit(Control control)
			{
				if (this.impl_list == null)
				{
					this.impl_list = new ArrayList();
				}
				if (this.AllContains(control))
				{
					this.owner.PerformLayout();
					return;
				}
				if (control.parent != null)
				{
					control.parent.Controls.Remove(control);
				}
				this.all_controls = null;
				this.impl_list.Add(control);
				control.ChangeParent(this.owner);
				control.InitLayout();
				if (this.owner.Visible)
				{
					this.owner.UpdateChildrenZOrder();
				}
				if (control.VisibleInternal)
				{
					this.owner.PerformLayout(control, "Parent");
				}
			}

			// Token: 0x0600041F RID: 1055 RVA: 0x00010330 File Offset: 0x0000E530
			internal virtual void AddRangeImplicit(Control[] controls)
			{
				if (controls == null)
				{
					throw new ArgumentNullException("controls");
				}
				this.owner.SuspendLayout();
				try
				{
					for (int i = 0; i < controls.Length; i++)
					{
						this.AddImplicit(controls[i]);
					}
				}
				finally
				{
					this.owner.ResumeLayout(false);
				}
			}

			/// <summary>Removes all controls from the collection.</summary>
			// Token: 0x06000420 RID: 1056 RVA: 0x0001038C File Offset: 0x0000E58C
			public new virtual void Clear()
			{
				this.all_controls = null;
				while (this.list.Count > 0)
				{
					this.Remove((Control)this.list[this.list.Count - 1]);
				}
			}

			// Token: 0x06000421 RID: 1057 RVA: 0x000103C8 File Offset: 0x0000E5C8
			internal virtual void ClearImplicit()
			{
				if (this.impl_list == null)
				{
					return;
				}
				this.all_controls = null;
				this.impl_list.Clear();
			}

			/// <summary>Determines whether the specified control is a member of the collection.</summary>
			/// <returns>true if the <see cref="T:System.Windows.Forms.Control" /> is a member of the collection; otherwise, false.</returns>
			/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to locate in the collection. </param>
			// Token: 0x06000422 RID: 1058 RVA: 0x000103E5 File Offset: 0x0000E5E5
			public bool Contains(Control control)
			{
				return this.list.Contains(control);
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x000103F3 File Offset: 0x0000E5F3
			internal bool ImplicitContains(Control value)
			{
				return this.impl_list != null && this.impl_list.Contains(value);
			}

			// Token: 0x06000424 RID: 1060 RVA: 0x0001040B File Offset: 0x0000E60B
			internal bool AllContains(Control value)
			{
				return this.Contains(value) || this.ImplicitContains(value);
			}

			/// <summary>Retrieves a reference to an enumerator object that is used to iterate over a <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" />.</returns>
			// Token: 0x06000425 RID: 1061 RVA: 0x0001041F File Offset: 0x0000E61F
			public override IEnumerator GetEnumerator()
			{
				return new Control.ControlCollection.ControlCollectionEnumerator(this.list);
			}

			// Token: 0x170000F4 RID: 244
			// (get) Token: 0x06000426 RID: 1062 RVA: 0x0001042C File Offset: 0x0000E62C
			internal ArrayList ImplicitControls
			{
				get
				{
					return this.impl_list;
				}
			}

			// Token: 0x06000427 RID: 1063 RVA: 0x00010434 File Offset: 0x0000E634
			internal Control[] GetAllControls()
			{
				if (this.all_controls != null)
				{
					return this.all_controls;
				}
				if (this.impl_list == null)
				{
					this.all_controls = (Control[])this.list.ToArray(typeof(Control));
					return this.all_controls;
				}
				this.all_controls = new Control[this.list.Count + this.impl_list.Count];
				this.impl_list.CopyTo(this.all_controls);
				this.list.CopyTo(this.all_controls, this.impl_list.Count);
				return this.all_controls;
			}

			/// <summary>Retrieves the index of the specified control in the control collection.</summary>
			/// <returns>A zero-based index value that represents the position of the specified <see cref="T:System.Windows.Forms.Control" /> in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
			/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to locate in the collection. </param>
			// Token: 0x06000428 RID: 1064 RVA: 0x000104D4 File Offset: 0x0000E6D4
			public int IndexOf(Control control)
			{
				return this.list.IndexOf(control);
			}

			/// <summary>Removes the specified control from the control collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to remove from the <see cref="T:System.Windows.Forms.Control.ControlCollection" />. </param>
			// Token: 0x06000429 RID: 1065 RVA: 0x000104E4 File Offset: 0x0000E6E4
			public virtual void Remove(Control value)
			{
				if (value == null || !this.list.Contains(value))
				{
					return;
				}
				this.all_controls = null;
				this.list.Remove(value);
				this.owner.PerformLayout(value, "Parent");
				this.owner.OnControlRemoved(new ControlEventArgs(value));
				ContainerControl containerControl = this.owner.InternalGetContainerControl();
				if (containerControl != null)
				{
					containerControl.ChildControlRemoved(value);
				}
				value.ChangeParent(null);
				this.owner.UpdateChildrenZOrder();
			}

			// Token: 0x0600042A RID: 1066 RVA: 0x00010560 File Offset: 0x0000E760
			internal virtual void RemoveImplicit(Control control)
			{
				if (this.impl_list != null)
				{
					this.all_controls = null;
					this.impl_list.Remove(control);
					this.owner.PerformLayout(control, "Parent");
					this.owner.OnControlRemoved(new ControlEventArgs(control));
				}
				control.ChangeParent(null);
				this.owner.UpdateChildrenZOrder();
			}

			/// <summary>Removes a control from the control collection at the specified indexed location.</summary>
			/// <param name="index">The index value of the <see cref="T:System.Windows.Forms.Control" /> to remove. </param>
			// Token: 0x0600042B RID: 1067 RVA: 0x000105BC File Offset: 0x0000E7BC
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.list.Count)
				{
					throw new ArgumentOutOfRangeException("index", index, "ControlCollection does not have that many controls");
				}
				this.Remove((Control)this.list[index]);
			}

			/// <summary>Sets the index of the specified child control in the collection to the specified index value.</summary>
			/// <param name="child">The <paramref name="child" /><see cref="T:System.Windows.Forms.Control" /> to search for. </param>
			/// <param name="newIndex">The new index value of the control. </param>
			/// <exception cref="T:System.ArgumentException">The <paramref name="child" /> control is not in the <see cref="T:System.Windows.Forms.Control.ControlCollection" />. </exception>
			// Token: 0x0600042C RID: 1068 RVA: 0x00010608 File Offset: 0x0000E808
			public virtual void SetChildIndex(Control child, int newIndex)
			{
				if (child == null)
				{
					throw new ArgumentNullException("child");
				}
				int num = this.list.IndexOf(child);
				if (num == -1)
				{
					throw new ArgumentException("Not a child control", "child");
				}
				if (num == newIndex)
				{
					return;
				}
				this.all_controls = null;
				this.list.RemoveAt(num);
				if (newIndex > this.list.Count)
				{
					this.list.Add(child);
				}
				else
				{
					this.list.Insert(newIndex, child);
				}
				child.UpdateZOrder();
				this.owner.PerformLayout();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			// Token: 0x0600042D RID: 1069 RVA: 0x00010698 File Offset: 0x0000E898
			int IList.Add(object control)
			{
				if (!(control is Control))
				{
					throw new ArgumentException("Object of type Control required", "control");
				}
				if (control == null)
				{
					throw new ArgumentException("control", "Cannot add null controls");
				}
				this.Add((Control)control);
				return this.IndexOf((Control)control);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			// Token: 0x0600042E RID: 1070 RVA: 0x000106E8 File Offset: 0x0000E8E8
			void IList.Remove(object control)
			{
				if (!(control is Control))
				{
					throw new ArgumentException("Object of type Control required", "control");
				}
				this.Remove((Control)control);
			}

			/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
			// Token: 0x0600042F RID: 1071 RVA: 0x0001070E File Offset: 0x0000E90E
			object ICloneable.Clone()
			{
				return new Control.ControlCollection(this.owner)
				{
					list = (ArrayList)this.list.Clone()
				};
			}

			// Token: 0x04000224 RID: 548
			private ArrayList impl_list;

			// Token: 0x04000225 RID: 549
			private Control[] all_controls;

			// Token: 0x04000226 RID: 550
			private Control owner;

			// Token: 0x02000053 RID: 83
			internal class ControlCollectionEnumerator : IEnumerator
			{
				// Token: 0x06000430 RID: 1072 RVA: 0x00010731 File Offset: 0x0000E931
				public ControlCollectionEnumerator(ArrayList collection)
				{
					this.list = collection;
				}

				// Token: 0x170000F5 RID: 245
				// (get) Token: 0x06000431 RID: 1073 RVA: 0x00010748 File Offset: 0x0000E948
				public object Current
				{
					get
					{
						object obj;
						try
						{
							obj = this.list[this.position];
						}
						catch (IndexOutOfRangeException)
						{
							throw new InvalidOperationException();
						}
						return obj;
					}
				}

				// Token: 0x06000432 RID: 1074 RVA: 0x00010784 File Offset: 0x0000E984
				public bool MoveNext()
				{
					this.position++;
					return this.position < this.list.Count;
				}

				// Token: 0x06000433 RID: 1075 RVA: 0x000107A7 File Offset: 0x0000E9A7
				public void Reset()
				{
					this.position = -1;
				}

				// Token: 0x04000227 RID: 551
				private ArrayList list;

				// Token: 0x04000228 RID: 552
				private int position = -1;
			}
		}
	}
}
