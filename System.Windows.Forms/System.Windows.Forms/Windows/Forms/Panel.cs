using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Used to group collections of controls.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000160 RID: 352
	[DefaultProperty("BorderStyle")]
	[DefaultEvent("Paint")]
	[Designer("System.Windows.Forms.Design.PanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[Docking(DockingBehavior.Ask)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	public class Panel : ScrollableControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Panel" /> class.</summary>
		// Token: 0x06000D97 RID: 3479 RVA: 0x0003B645 File Offset: 0x00039845
		public Panel()
		{
			base.TabStop = false;
			base.SetStyle(ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		/// <summary>Gets or sets a value that indicates whether the control resizes based on its contents.</summary>
		/// <returns>true if the control automatically resizes based on its contents; otherwise, false. The default is true.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x000042BD File Offset: 0x000024BD
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x000042C5 File Offset: 0x000024C5
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

		/// <summary>Indicates the border style for the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is BorderStyle.None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value when setting this property is not a valid <see cref="T:System.Windows.Forms.BorderStyle" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x00024E8B File Offset: 0x0002308B
		[DefaultValue(BorderStyle.None)]
		[DispId(-504)]
		public BorderStyle BorderStyle
		{
			get
			{
				return base.InternalBorderStyle;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x000043B4 File Offset: 0x000025B4
		// (set) Token: 0x06000D9C RID: 3484 RVA: 0x0003B66C File Offset: 0x0003986C
		[Bindable(false)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (value == this.Text)
				{
					return;
				}
				base.Text = value;
				this.Refresh();
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000D9D RID: 3485 RVA: 0x00009240 File Offset: 0x00007440
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0003B68A File Offset: 0x0003988A
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.PanelDefaultSize;
			}
		}

		/// <summary>Returns a string representation for this control.</summary>
		/// <returns>A <see cref="T:System.String" /> representation of the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D9F RID: 3487 RVA: 0x0003B696 File Offset: 0x00039896
		public override string ToString()
		{
			return base.ToString() + ", BorderStyle: " + this.BorderStyle;
		}

		/// <summary>Fires the event indicating that the panel has been resized. Inheriting controls should use this in favor of actually listening to the event, but should still call base.onResize to ensure that the event is fired for external listeners.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000DA0 RID: 3488 RVA: 0x0003B6B3 File Offset: 0x000398B3
		protected override void OnResize(EventArgs eventargs)
		{
			base.OnResize(eventargs);
			base.Invalidate(true);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0003B6C4 File Offset: 0x000398C4
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size empty = Size.Empty;
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				if (control.Dock == DockStyle.Fill)
				{
					if (control.Bounds.Right > empty.Width)
					{
						empty.Width = control.Bounds.Right;
					}
				}
				else if (control.Dock != DockStyle.Top && control.Dock != DockStyle.Bottom && (control.Anchor & AnchorStyles.Right) == AnchorStyles.None && control.Bounds.Right + control.Margin.Right > empty.Width)
				{
					empty.Width = control.Bounds.Right + control.Margin.Right;
				}
				if (control.Dock == DockStyle.Fill)
				{
					if (control.Bounds.Bottom > empty.Height)
					{
						empty.Height = control.Bounds.Bottom;
					}
				}
				else if (control.Dock != DockStyle.Left && control.Dock != DockStyle.Right && (control.Anchor & AnchorStyles.Bottom) == AnchorStyles.None && control.Bounds.Bottom + control.Margin.Bottom > empty.Height)
				{
					empty.Height = control.Bounds.Bottom + control.Margin.Bottom;
				}
			}
			return empty;
		}
	}
}
