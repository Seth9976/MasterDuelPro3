using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a line used to group items of a <see cref="T:System.Windows.Forms.ToolStrip" /> or the drop-down items of a <see cref="T:System.Windows.Forms.MenuStrip" /> or <see cref="T:System.Windows.Forms.ContextMenuStrip" /> or other <see cref="T:System.Windows.Forms.ToolStripDropDown" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001EC RID: 492
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
	public class ToolStripSeparator : ToolStripItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> class. </summary>
		// Token: 0x060014F4 RID: 5364 RVA: 0x000697F2 File Offset: 0x000679F2
		public ToolStripSeparator()
		{
			base.Dock = DockStyle.Fill;
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The image to display in the background of the separator.</returns>
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00069801 File Offset: 0x00067A01
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00069809 File Offset: 0x00067A09
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> can be selected. </summary>
		/// <returns>true if the component using the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> is in design mode; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00002D70 File Offset: 0x00000F70
		public override bool CanSelect
		{
			get
			{
				return false;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00066833 File Offset: 0x00064A33
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x0006683B File Offset: 0x00064A3B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00069811 File Offset: 0x00067A11
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>The image to be displayed.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x00069819 File Offset: 0x00067A19
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x00069821 File Offset: 0x00067A21
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image Image
		{
			get
			{
				return base.Image;
			}
			set
			{
				base.Image = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the item’s text.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0006982A File Offset: 0x00067A2A
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x00069832 File Offset: 0x00067A32
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values.</returns>
		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0006983B File Offset: 0x00067A3B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		public override ToolStripTextDirection TextDirection
		{
			get
			{
				return base.TextDirection;
			}
		}

		/// <summary>Gets the spacing between the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> and an adjacent item.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x00069844 File Offset: 0x00067A44
		protected internal override Padding DefaultMargin
		{
			get
			{
				return default(Padding);
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />, measured in pixels. The default is 100 pixels horizontally.</returns>
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0006985A File Offset: 0x00067A5A
		protected override Size DefaultSize
		{
			get
			{
				return new Size(6, 6);
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> can be fitted.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the height and width of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />, in pixels.</returns>
		/// <param name="constrainingSize">A <see cref="T:System.Drawing.Size" /> representing the height and width of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />, in pixels.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001502 RID: 5378 RVA: 0x0006985A File Offset: 0x00067A5A
		public override Size GetPreferredSize(Size constrainingSize)
		{
			return new Size(6, 6);
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001503 RID: 5379 RVA: 0x00069863 File Offset: 0x00067A63
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001504 RID: 5380 RVA: 0x0006986C File Offset: 0x00067A6C
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (base.Owner != null)
			{
				if (base.IsOnDropDown)
				{
					base.Owner.Renderer.DrawSeparator(new ToolStripSeparatorRenderEventArgs(e.Graphics, this, base.Owner.Orientation != Orientation.Horizontal));
					return;
				}
				base.Owner.Renderer.DrawSeparator(new ToolStripSeparatorRenderEventArgs(e.Graphics, this, base.Owner.Orientation == Orientation.Horizontal));
			}
		}

		/// <summary>Sets the size and location of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</summary>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> specifying the size and location of the <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		// Token: 0x06001505 RID: 5381 RVA: 0x00066E6A File Offset: 0x0006506A
		protected internal override void SetBounds(Rectangle rect)
		{
			base.SetBounds(rect);
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x00006F54 File Offset: 0x00005154
		internal override ToolStripTextDirection DefaultTextDirection
		{
			get
			{
				return ToolStripTextDirection.Horizontal;
			}
		}
	}
}
