using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows control that displays a frame around a group of controls with an optional caption.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000BE RID: 190
	[DefaultProperty("Text")]
	[DefaultEvent("Enter")]
	[Designer("System.Windows.Forms.Design.GroupBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class GroupBox : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.GroupBox" /> class.</summary>
		// Token: 0x0600075E RID: 1886 RVA: 0x0002080C File Offset: 0x0001EA0C
		public GroupBox()
		{
			this.TabStop = false;
			this.flat_style = FlatStyle.Standard;
			base.SetStyle(ControlStyles.ContainerControl | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
			base.SetStyle(ControlStyles.Selectable, false);
		}

		/// <summary>Gets or sets a value that indicates whether the control will allow drag-and-drop operations and events to be used.</summary>
		/// <returns>true to allow drag-and-drop operations and events to be used; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x0002083A File Offset: 0x0001EA3A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Windows.Forms.GroupBox" /> resizes based on its contents.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.GroupBox" /> automatically resizes based on its contents; otherwise, false. The default is true.</returns>
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x000042BD File Offset: 0x000024BD
		// (set) Token: 0x06000761 RID: 1889 RVA: 0x000042C5 File Offset: 0x000024C5
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
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

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00004663 File Offset: 0x00002863
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x00020842 File Offset: 0x0001EA42
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.GroupBoxDefaultSize;
			}
		}

		/// <summary>Gets a rectangle that represents the dimensions of the <see cref="T:System.Windows.Forms.GroupBox" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> with the dimensions of the <see cref="T:System.Windows.Forms.GroupBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x00020850 File Offset: 0x0001EA50
		public override Rectangle DisplayRectangle
		{
			get
			{
				this.display_rectangle.X = base.Padding.Left;
				this.display_rectangle.Y = this.Font.Height + base.Padding.Top;
				this.display_rectangle.Width = base.Width - base.Padding.Horizontal;
				this.display_rectangle.Height = base.Height - this.Font.Height - base.Padding.Vertical;
				return this.display_rectangle;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the user can press the TAB key to give the focus to the <see cref="T:System.Windows.Forms.GroupBox" />.</summary>
		/// <returns>true to allow the user to press the TAB key to give the focus to the <see cref="T:System.Windows.Forms.GroupBox" />; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001CE RID: 462
		// (set) Token: 0x06000765 RID: 1893 RVA: 0x000208ED File Offset: 0x0001EAED
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new bool TabStop
		{
			set
			{
				base.TabStop = value;
			}
		}

		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x000043B4 File Offset: 0x000025B4
		// (set) Token: 0x06000767 RID: 1895 RVA: 0x000208F6 File Offset: 0x0001EAF6
		[Localizable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (base.Text == value)
				{
					return;
				}
				base.Text = value;
				this.Refresh();
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000768 RID: 1896 RVA: 0x00020914 File Offset: 0x0001EB14
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.Refresh();
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06000769 RID: 1897 RVA: 0x00020923 File Offset: 0x0001EB23
		protected override void OnPaint(PaintEventArgs e)
		{
			ThemeEngine.Current.DrawGroupBox(e.Graphics, base.ClientRectangle, this);
			base.OnPaint(e);
		}

		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x0600076A RID: 1898 RVA: 0x00020943 File Offset: 0x0001EB43
		protected override bool ProcessMnemonic(char charCode)
		{
			if (Control.IsMnemonic(charCode, this.Text))
			{
				if (base.Parent != null)
				{
					base.Parent.SelectNextControl(this, true, false, true, false);
				}
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Scales the <see cref="T:System.Windows.Forms.GroupBox" /> by the specified factor and scaling instruction.</summary>
		/// <param name="factor">The <see cref="T:System.Drawing.SizeF" /> that indicates the height and width of the scaled control.</param>
		/// <param name="specified">One of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values that indicates how the control should be scaled.</param>
		// Token: 0x0600076B RID: 1899 RVA: 0x0000677E File Offset: 0x0000497E
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.GroupBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600076C RID: 1900 RVA: 0x0001E8FC File Offset: 0x0001CAFC
		public override string ToString()
		{
			return base.GetType().FullName + ", Text: " + this.Text;
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x0600076D RID: 1901 RVA: 0x00020975 File Offset: 0x0001EB75
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Padding" /> structure that contains the default padding settings for a <see cref="T:System.Windows.Forms.GroupBox" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> with all its edges set to three pixels. </returns>
		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0000BC73 File Offset: 0x00009E73
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(3);
			}
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x00020980 File Offset: 0x0001EB80
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size size = new Size(base.Padding.Left, base.Padding.Top);
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				if (control.Dock == DockStyle.Fill)
				{
					if (control.Bounds.Right > size.Width)
					{
						size.Width = control.Bounds.Right;
					}
				}
				else if (control.Dock != DockStyle.Top && control.Dock != DockStyle.Bottom && control.Bounds.Right + control.Margin.Right > size.Width)
				{
					size.Width = control.Bounds.Right + control.Margin.Right;
				}
				if (control.Dock == DockStyle.Fill)
				{
					if (control.Bounds.Bottom > size.Height)
					{
						size.Height = control.Bounds.Bottom;
					}
				}
				else if (control.Dock != DockStyle.Left && control.Dock != DockStyle.Right && control.Bounds.Bottom + control.Margin.Bottom > size.Height)
				{
					size.Height = control.Bounds.Bottom + control.Margin.Bottom;
				}
			}
			size.Width += base.Padding.Right;
			size.Height += base.Padding.Bottom;
			size.Height += this.Font.Height;
			return size;
		}

		// Token: 0x040004C0 RID: 1216
		private FlatStyle flat_style;

		// Token: 0x040004C1 RID: 1217
		private Rectangle display_rectangle;
	}
}
