using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a control that allows the user to select a single item from a list that is displayed when the user clicks a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />. Although <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> and <see cref="T:System.Windows.Forms.ToolStripDropDown" /> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu" /> control of previous versions, <see cref="T:System.Windows.Forms.Menu" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001BE RID: 446
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.ToolStripDropDownDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class ToolStripDropDown : ToolStrip
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> class. </summary>
		// Token: 0x06001331 RID: 4913 RVA: 0x00062048 File Offset: 0x00060248
		public ToolStripDropDown()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			this.auto_close = true;
			this.is_visible = false;
			this.DefaultDropDownDirection = ToolStripDropDownDirection.Right;
			this.GripStyle = ToolStripGripStyle.Hidden;
			this.is_toplevel = true;
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x000620A9 File Offset: 0x000602A9
		// (set) Token: 0x06001333 RID: 4915 RVA: 0x000620B1 File Offset: 0x000602B1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> automatically adjusts its size when the form is resized. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control automatically resizes; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x000620BA File Offset: 0x000602BA
		// (set) Token: 0x06001335 RID: 4917 RVA: 0x000620C2 File Offset: 0x000602C2
		[DefaultValue(true)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		/// <summary>Gets or sets the direction in which the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is displayed relative to the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</returns>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001336 RID: 4918 RVA: 0x000620CB File Offset: 0x000602CB
		// (set) Token: 0x06001337 RID: 4919 RVA: 0x000620D3 File Offset: 0x000602D3
		public override ToolStripDropDownDirection DefaultDropDownDirection
		{
			get
			{
				return base.DefaultDropDownDirection;
			}
			set
			{
				base.DefaultDropDownDirection = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001338 RID: 4920 RVA: 0x000620DC File Offset: 0x000602DC
		// (set) Token: 0x06001339 RID: 4921 RVA: 0x000620E4 File Offset: 0x000602E4
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(DockStyle.None)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		/// <summary>Gets or sets the font of the text displayed on the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control.</returns>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x0600133A RID: 4922 RVA: 0x000620ED File Offset: 0x000602ED
		// (set) Token: 0x0600133B RID: 4923 RVA: 0x000620F5 File Offset: 0x000602F5
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000505 RID: 1285
		// (set) Token: 0x0600133C RID: 4924 RVA: 0x000620FE File Offset: 0x000602FE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(ToolStripGripStyle.Hidden)]
		public new ToolStripGripStyle GripStyle
		{
			set
			{
				base.GripStyle = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The coordinates of the upper-left corner of the control relative to the upper-left corner of its container.</returns>
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x000203B2 File Offset: 0x0001E5B2
		// (set) Token: 0x0600133E RID: 4926 RVA: 0x000203BA File Offset: 0x0001E5BA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		/// <summary>Determines the opacity of the form.</summary>
		/// <returns>The level of opacity for the form. The default is 1.00.</returns>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x00062107 File Offset: 0x00060307
		[DefaultValue(1.0)]
		[TypeConverter(typeof(OpacityConverter))]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public double Opacity
		{
			get
			{
				return this.opacity;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.ToolStripItem" /> that is the owner of this <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> that is the owner of this <see cref="T:System.Windows.Forms.ToolStripDropDown" />. The default value is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0006210F File Offset: 0x0006030F
		// (set) Token: 0x06001341 RID: 4929 RVA: 0x00062118 File Offset: 0x00060318
		[Browsable(false)]
		[DefaultValue(null)]
		public ToolStripItem OwnerItem
		{
			get
			{
				return this.owner_item;
			}
			set
			{
				this.owner_item = value;
				if (this.owner_item != null)
				{
					if (this.owner_item.Owner != null && this.owner_item.Owner.RenderMode != ToolStripRenderMode.ManagerRenderMode)
					{
						base.Renderer = this.owner_item.Owner.Renderer;
					}
					this.Font = this.owner_item.Font;
				}
			}
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x0003B96F File Offset: 0x00039B6F
		[Localizable(true)]
		[AmbientValue(RightToLeft.Inherit)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
		}

		/// <summary>Specifies the direction in which to draw the text on the item.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripTextDirection.Horizontal" />.</returns>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001343 RID: 4931 RVA: 0x0006217B File Offset: 0x0006037B
		[Browsable(false)]
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		public override ToolStripTextDirection TextDirection
		{
			get
			{
				return base.TextDirection;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is visible or hidden. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> is visible; otherwise, false. The default is false.</returns>
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001344 RID: 4932 RVA: 0x00062183 File Offset: 0x00060383
		[Browsable(false)]
		[Localizable(true)]
		[DefaultValue(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
		}

		/// <summary>Gets parameters of a new window.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.CreateParams" /> used when creating a new window.</returns>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001345 RID: 4933 RVA: 0x0006218C File Offset: 0x0006038C
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.Style = -2113929216;
				createParams.ClassStyle |= 131072;
				createParams.ExStyle |= 136;
				if (this.Opacity < 1.0 && this.allow_transparency)
				{
					createParams.ExStyle |= 524288;
				}
				if (this.TopMost)
				{
					createParams.ExStyle |= 8;
				}
				return createParams;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001346 RID: 4934 RVA: 0x00002D70 File Offset: 0x00000F70
		protected override DockStyle DefaultDock
		{
			get
			{
				return DockStyle.None;
			}
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001347 RID: 4935 RVA: 0x00062211 File Offset: 0x00060411
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(1, 2, 1, 2);
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001348 RID: 4936 RVA: 0x00006F54 File Offset: 0x00005154
		protected override bool DefaultShowItemToolTips
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets a value indicating whether the form should be displayed as a topmost form.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001349 RID: 4937 RVA: 0x00006F54 File Offset: 0x00005154
		protected virtual bool TopMost
		{
			get
			{
				return true;
			}
		}

		/// <summary>Closes the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control for the specified reason.</summary>
		/// <param name="reason">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownCloseReason" /> values.</param>
		// Token: 0x0600134A RID: 4938 RVA: 0x0006221C File Offset: 0x0006041C
		public void Close(ToolStripDropDownCloseReason reason)
		{
			if (!this.Visible)
			{
				return;
			}
			ToolStripDropDownClosingEventArgs toolStripDropDownClosingEventArgs = new ToolStripDropDownClosingEventArgs(reason);
			this.OnClosing(toolStripDropDownClosingEventArgs);
			if (toolStripDropDownClosingEventArgs.Cancel)
			{
				return;
			}
			if (!this.auto_close && reason != ToolStripDropDownCloseReason.CloseCalled)
			{
				return;
			}
			ToolStripManager.AppClicked -= this.ToolStripMenuTracker_AppClicked;
			ToolStripManager.AppFocusChange -= this.ToolStripMenuTracker_AppFocusChange;
			base.Hide();
			if (this.owner_item != null)
			{
				this.owner_item.Invalidate();
			}
			foreach (object obj in this.Items)
			{
				((ToolStripItem)obj).Dismiss(reason);
			}
			this.OnClosed(new ToolStripDropDownClosedEventArgs(reason));
		}

		/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified screen location.</summary>
		/// <param name="screenLocation">The horizontal and vertical location of the screen's upper-left corner, in pixels.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600134B RID: 4939 RVA: 0x000622E8 File Offset: 0x000604E8
		public void Show(Point screenLocation)
		{
			this.Show(screenLocation, this.DefaultDropDownDirection);
		}

		/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified control location.</summary>
		/// <param name="control">The control (typically, a <see cref="T:System.Windows.Forms.ToolStripDropDownButton" />) that is the reference point for the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> position.</param>
		/// <param name="position">The horizontal and vertical location of the reference control's upper-left corner, in pixels.</param>
		/// <exception cref="T:System.ArgumentNullException">The control specified by the <paramref name="control" /> parameter is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600134C RID: 4940 RVA: 0x000622F7 File Offset: 0x000604F7
		public void Show(Control control, Point position)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			XplatUI.SetOwner(base.Handle, control.Handle);
			this.Show(control.PointToScreen(position), this.DefaultDropDownDirection);
		}

		/// <summary>Positions the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> relative to the specified control location and with the specified direction relative to the parent control.</summary>
		/// <param name="position">The horizontal and vertical location of the reference control's upper-left corner, in pixels.</param>
		/// <param name="direction">One of the <see cref="T:System.Windows.Forms.ToolStripDropDownDirection" /> values.</param>
		// Token: 0x0600134D RID: 4941 RVA: 0x0006232C File Offset: 0x0006052C
		public void Show(Point position, ToolStripDropDownDirection direction)
		{
			base.PerformLayout();
			Point point = position;
			Point point2 = new Point(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
			if (this is ContextMenuStrip)
			{
				switch (direction)
				{
				case ToolStripDropDownDirection.AboveLeft:
					if (point.X - base.Width < 0)
					{
						direction = ToolStripDropDownDirection.AboveRight;
					}
					break;
				case ToolStripDropDownDirection.AboveRight:
					if (point.X + base.Width > point2.X)
					{
						direction = ToolStripDropDownDirection.AboveLeft;
					}
					break;
				case ToolStripDropDownDirection.BelowLeft:
					if (point.X - base.Width < 0)
					{
						direction = ToolStripDropDownDirection.BelowRight;
					}
					break;
				case ToolStripDropDownDirection.BelowRight:
				case ToolStripDropDownDirection.Default:
					if (point.X + base.Width > point2.X)
					{
						direction = ToolStripDropDownDirection.BelowLeft;
					}
					break;
				case ToolStripDropDownDirection.Left:
					if (point.X - base.Width < 0)
					{
						direction = ToolStripDropDownDirection.Right;
					}
					break;
				case ToolStripDropDownDirection.Right:
					if (point.X + base.Width > point2.X)
					{
						direction = ToolStripDropDownDirection.Left;
					}
					break;
				}
				switch (direction)
				{
				case ToolStripDropDownDirection.AboveLeft:
					if (point.Y - base.Height < 0)
					{
						direction = ToolStripDropDownDirection.BelowLeft;
					}
					break;
				case ToolStripDropDownDirection.AboveRight:
					if (point.Y - base.Height < 0)
					{
						direction = ToolStripDropDownDirection.BelowRight;
					}
					break;
				case ToolStripDropDownDirection.BelowLeft:
					if (point.Y + base.Height > point2.Y && point.Y - base.Height > 0)
					{
						direction = ToolStripDropDownDirection.AboveLeft;
					}
					break;
				case ToolStripDropDownDirection.BelowRight:
				case ToolStripDropDownDirection.Default:
					if (point.Y + base.Height > point2.Y && point.Y - base.Height > 0)
					{
						direction = ToolStripDropDownDirection.AboveRight;
					}
					break;
				case ToolStripDropDownDirection.Left:
					if (point.Y + base.Height > point2.Y && point.Y - base.Height > 0)
					{
						direction = ToolStripDropDownDirection.AboveLeft;
					}
					break;
				case ToolStripDropDownDirection.Right:
					if (point.Y + base.Height > point2.Y && point.Y - base.Height > 0)
					{
						direction = ToolStripDropDownDirection.AboveRight;
					}
					break;
				}
			}
			switch (direction)
			{
			case ToolStripDropDownDirection.AboveLeft:
				point.Y -= base.Height;
				point.X -= base.Width;
				break;
			case ToolStripDropDownDirection.AboveRight:
				point.Y -= base.Height;
				break;
			case ToolStripDropDownDirection.BelowLeft:
				point.X -= base.Width;
				break;
			case ToolStripDropDownDirection.Left:
				point.X -= base.Width;
				break;
			}
			if (point.X + base.Width > point2.X)
			{
				point.X = point2.X - base.Width;
			}
			if (point.X < 0)
			{
				point.X = 0;
			}
			if (point.Y + base.Height > point2.Y)
			{
				point.Y = point2.Y - base.Height;
			}
			if (point.Y < 0)
			{
				point.Y = 0;
			}
			if (this.Location != point)
			{
				this.Location = point;
			}
			CancelEventArgs cancelEventArgs = new CancelEventArgs();
			this.OnOpening(cancelEventArgs);
			if (cancelEventArgs.Cancel)
			{
				return;
			}
			ToolStripManager.AppClicked += this.ToolStripMenuTracker_AppClicked;
			ToolStripManager.AppFocusChange += this.ToolStripMenuTracker_AppFocusChange;
			base.Show();
			ToolStripManager.SetActiveToolStrip(this, ToolStripManager.ActivatedByKeyboard);
			this.OnOpened(EventArgs.Empty);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0000607F File Offset: 0x0000427F
		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		/// <summary>Applies various layout options to the <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.LayoutSettings" /> for this <see cref="T:System.Windows.Forms.ToolStripDropDown" />.</returns>
		/// <param name="style">One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The possibilities are <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Flow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.StackWithOverflow" />, <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Table" />, and <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow" />.</param>
		// Token: 0x0600134F RID: 4943 RVA: 0x000626BC File Offset: 0x000608BC
		protected override LayoutSettings CreateLayoutSettings(ToolStripLayoutStyle style)
		{
			return base.CreateLayoutSettings(style);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001350 RID: 4944 RVA: 0x000626C5 File Offset: 0x000608C5
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closed" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs" /> that contains the event data.</param>
		// Token: 0x06001351 RID: 4945 RVA: 0x000626D0 File Offset: 0x000608D0
		protected virtual void OnClosed(ToolStripDropDownClosedEventArgs e)
		{
			ToolStripDropDownClosedEventHandler toolStripDropDownClosedEventHandler = (ToolStripDropDownClosedEventHandler)base.Events[ToolStripDropDown.ClosedEvent];
			if (toolStripDropDownClosedEventHandler != null)
			{
				toolStripDropDownClosedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripDropDownClosingEventArgs" /> that contains the event data.</param>
		// Token: 0x06001352 RID: 4946 RVA: 0x00062700 File Offset: 0x00060900
		protected virtual void OnClosing(ToolStripDropDownClosingEventArgs e)
		{
			ToolStripDropDownClosingEventHandler toolStripDropDownClosingEventHandler = (ToolStripDropDownClosingEventHandler)base.Events[ToolStripDropDown.ClosingEvent];
			if (toolStripDropDownClosingEventHandler != null)
			{
				toolStripDropDownClosingEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001353 RID: 4947 RVA: 0x00062730 File Offset: 0x00060930
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (Application.MWFThread.Current.Context != null && Application.MWFThread.Current.Context.MainForm != null)
			{
				XplatUI.SetOwner(base.Handle, Application.MWFThread.Current.Context.MainForm.Handle);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ItemClicked" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripItemClickedEventArgs" /> that contains the event data.</param>
		// Token: 0x06001354 RID: 4948 RVA: 0x00062781 File Offset: 0x00060981
		protected override void OnItemClicked(ToolStripItemClickedEventArgs e)
		{
			base.OnItemClicked(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06001355 RID: 4949 RVA: 0x0006278C File Offset: 0x0006098C
		protected override void OnLayout(LayoutEventArgs e)
		{
			int num = 0;
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Available)
				{
					toolStripItem.SetPlacement(ToolStripItemPlacement.Main);
					num = Math.Max(num, toolStripItem.GetPreferredSize(Size.Empty).Width + toolStripItem.Margin.Horizontal);
				}
			}
			num += base.Padding.Horizontal;
			int left = base.Padding.Left;
			int num2 = base.Padding.Top;
			foreach (object obj2 in this.Items)
			{
				ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
				if (toolStripItem2.Available)
				{
					num2 += toolStripItem2.Margin.Top;
					Size preferredSize = toolStripItem2.GetPreferredSize(Size.Empty);
					int num3;
					if (preferredSize.Height > 22)
					{
						num3 = preferredSize.Height;
					}
					else if (toolStripItem2 is ToolStripSeparator)
					{
						num3 = 7;
					}
					else
					{
						num3 = 22;
					}
					toolStripItem2.SetBounds(new Rectangle(left, num2, preferredSize.Width, num3));
					num2 += num3 + toolStripItem2.Margin.Bottom;
				}
			}
			base.Size = new Size(num, num2 + base.Padding.Bottom);
			this.SetDisplayedItems();
			this.OnLayoutCompleted(EventArgs.Empty);
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseUp" /> event.</summary>
		/// <param name="mea">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x06001356 RID: 4950 RVA: 0x00062958 File Offset: 0x00060B58
		protected override void OnMouseUp(MouseEventArgs mea)
		{
			base.OnMouseUp(mea);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opened" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001357 RID: 4951 RVA: 0x00062964 File Offset: 0x00060B64
		protected virtual void OnOpened(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripDropDown.OpenedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opening" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06001358 RID: 4952 RVA: 0x00062994 File Offset: 0x00060B94
		protected virtual void OnOpening(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[ToolStripDropDown.OpeningEvent];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001359 RID: 4953 RVA: 0x000629C2 File Offset: 0x00060BC2
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (base.Parent is ToolStrip)
			{
				base.Renderer = (base.Parent as ToolStrip).Renderer;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.VisibleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600135A RID: 4954 RVA: 0x000629F0 File Offset: 0x00060BF0
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (this.owner_item != null && this.owner_item is ToolStripDropDownItem)
			{
				ToolStripDropDownItem toolStripDropDownItem = (ToolStripDropDownItem)this.owner_item;
				if (this.Visible)
				{
					toolStripDropDownItem.OnDropDownOpened(EventArgs.Empty);
					return;
				}
				toolStripDropDownItem.OnDropDownClosed(EventArgs.Empty);
			}
		}

		/// <summary>Processes a dialog box character.</summary>
		/// <returns>true if the character was processed by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process.</param>
		// Token: 0x0600135B RID: 4955 RVA: 0x00062A44 File Offset: 0x00060C44
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override bool ProcessDialogChar(char charCode)
		{
			return base.ProcessDialogChar(charCode);
		}

		/// <summary>Processes a dialog box key.</summary>
		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		// Token: 0x0600135C RID: 4956 RVA: 0x00062A4D File Offset: 0x00060C4D
		protected override bool ProcessDialogKey(Keys keyData)
		{
			return keyData == (Keys.LButton | Keys.Back | Keys.Control) || keyData == (Keys.LButton | Keys.Back | Keys.Shift | Keys.Control) || base.ProcessDialogKey(keyData);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process.</param>
		// Token: 0x0600135D RID: 4957 RVA: 0x00062A68 File Offset: 0x00060C68
		protected override bool ProcessMnemonic(char charCode)
		{
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Scales a control's location, size, padding and margin.</summary>
		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x0600135E RID: 4958 RVA: 0x0001D4DF File Offset: 0x0001B6DF
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x0600135F RID: 4959 RVA: 0x0001F3B0 File Offset: 0x0001D5B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			base.ScaleCore(dx, dy);
		}

		/// <summary>Performs the work of setting the specified bounds of this control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
		// Token: 0x06001360 RID: 4960 RVA: 0x00062A71 File Offset: 0x00060C71
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Adjusts the size of the owner <see cref="T:System.Windows.Forms.ToolStrip" /> to accommodate the <see cref="T:System.Windows.Forms.ToolStripDropDown" /> if the owner <see cref="T:System.Windows.Forms.ToolStrip" /> is currently displayed, or clears and resets active <see cref="T:System.Windows.Forms.ToolStripDropDown" /> child controls of the <see cref="T:System.Windows.Forms.ToolStrip" /> if the <see cref="T:System.Windows.Forms.ToolStrip" /> is not currently displayed.</summary>
		/// <param name="visible">true if the owner <see cref="T:System.Windows.Forms.ToolStrip" /> is currently displayed; otherwise, false. </param>
		// Token: 0x06001361 RID: 4961 RVA: 0x00062A80 File Offset: 0x00060C80
		protected override void SetVisibleCore(bool visible)
		{
			base.SetVisibleCore(visible);
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x06001362 RID: 4962 RVA: 0x00062A89 File Offset: 0x00060C89
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 33)
			{
				m.Result = (IntPtr)3;
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00062AA9 File Offset: 0x00060CA9
		internal override void Dismiss(ToolStripDropDownCloseReason reason)
		{
			this.Close(reason);
			base.Dismiss(reason);
			if (this.OwnerItem == null)
			{
				return;
			}
			ToolStripManager.SetActiveToolStrip(null, false);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00062AC9 File Offset: 0x00060CC9
		internal override ToolStrip GetTopLevelToolStrip()
		{
			if (this.OwnerItem == null)
			{
				return this;
			}
			return this.OwnerItem.GetTopLevelToolStrip();
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00062AE0 File Offset: 0x00060CE0
		internal override bool ProcessArrowKey(Keys keyData)
		{
			if (keyData > Keys.Escape)
			{
				switch (keyData)
				{
				case Keys.Left:
					goto IL_006F;
				case Keys.Up:
					break;
				case Keys.Right:
					this.GetTopLevelToolStrip().SelectNextToolStripItem(this.TopLevelOwnerItem, true);
					return true;
				case Keys.Down:
					goto IL_003A;
				default:
					if (keyData != (Keys.LButton | Keys.Back | Keys.Shift))
					{
						return false;
					}
					break;
				}
				this.SelectNextToolStripItem(base.GetCurrentlySelectedItem(), false);
				return true;
			}
			if (keyData != Keys.Tab)
			{
				if (keyData != Keys.Escape)
				{
					return false;
				}
				goto IL_006F;
			}
			IL_003A:
			this.SelectNextToolStripItem(base.GetCurrentlySelectedItem(), true);
			return true;
			IL_006F:
			this.Dismiss(ToolStripDropDownCloseReason.Keyboard);
			if (this.OwnerItem == null)
			{
				return true;
			}
			ToolStrip parent = this.OwnerItem.Parent;
			ToolStripManager.SetActiveToolStrip(parent, true);
			if (parent is MenuStrip && keyData == Keys.Left)
			{
				parent.SelectNextToolStripItem(this.TopLevelOwnerItem, false);
				this.TopLevelOwnerItem.Invalidate();
			}
			else if (parent is MenuStrip && keyData == Keys.Escape)
			{
				(parent as MenuStrip).MenuDroppedDown = false;
				this.TopLevelOwnerItem.Select();
			}
			return true;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00062BD0 File Offset: 0x00060DD0
		internal override ToolStripItem SelectNextToolStripItem(ToolStripItem start, bool forward)
		{
			ToolStripItem nextItem = this.GetNextItem(start, forward ? ArrowDirection.Down : ArrowDirection.Up);
			if (nextItem != null)
			{
				base.ChangeSelection(nextItem);
			}
			return nextItem;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0003730A File Offset: 0x0003550A
		private void ToolStripMenuTracker_AppFocusChange(object sender, EventArgs e)
		{
			this.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.AppFocusChange);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00037318 File Offset: 0x00035518
		private void ToolStripMenuTracker_AppClicked(object sender, EventArgs e)
		{
			this.GetTopLevelToolStrip().Dismiss(ToolStripDropDownCloseReason.AppClicked);
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x00002D70 File Offset: 0x00000F70
		internal override bool ActivateOnShow
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x00062BF8 File Offset: 0x00060DF8
		internal ToolStripItem TopLevelOwnerItem
		{
			get
			{
				ToolStrip owner;
				for (ToolStripItem toolStripItem = this.OwnerItem; toolStripItem != null; toolStripItem = (owner as ToolStripDropDown).OwnerItem)
				{
					owner = toolStripItem.Owner;
					if (owner == null || !(owner is ToolStripDropDown))
					{
						return toolStripItem;
					}
				}
				return null;
			}
		}

		// Token: 0x04000BCA RID: 3018
		private bool allow_transparency;

		// Token: 0x04000BCB RID: 3019
		private bool auto_close;

		// Token: 0x04000BCC RID: 3020
		private bool drop_shadow_enabled = true;

		// Token: 0x04000BCD RID: 3021
		private double opacity = 1.0;

		// Token: 0x04000BCE RID: 3022
		private ToolStripItem owner_item;

		// Token: 0x04000BCF RID: 3023
		private static object ClosedEvent = new object();

		// Token: 0x04000BD0 RID: 3024
		private static object ClosingEvent = new object();

		// Token: 0x04000BD1 RID: 3025
		private static object OpenedEvent = new object();

		// Token: 0x04000BD2 RID: 3026
		private static object OpeningEvent = new object();

		// Token: 0x04000BD3 RID: 3027
		private static object ScrollEvent = new object();
	}
}
