using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides properties that specify the appearance of <see cref="T:System.Windows.Forms.Button" /> controls whose <see cref="T:System.Windows.Forms.FlatStyle" /> is <see cref="F:System.Windows.Forms.FlatStyle.Flat" />.</summary>
	// Token: 0x020000A7 RID: 167
	[TypeConverter(typeof(FlatButtonAppearanceConverter))]
	public class FlatButtonAppearance
	{
		// Token: 0x06000667 RID: 1639 RVA: 0x0001BBB4 File Offset: 0x00019DB4
		internal FlatButtonAppearance(ButtonBase owner)
		{
			this.owner = owner;
		}

		/// <summary>Gets or sets the color of the border around the button.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the border around the button.</returns>
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000668 RID: 1640 RVA: 0x0001BC01 File Offset: 0x00019E01
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(typeof(Color), "")]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public Color BorderColor
		{
			get
			{
				return this.borderColor;
			}
		}

		/// <summary>Gets or sets a value that specifies the size, in pixels, of the border around the button.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the size, in pixels, of the border around the button.</returns>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0001BC09 File Offset: 0x00019E09
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(1)]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public int BorderSize
		{
			get
			{
				return this.borderSize;
			}
		}

		/// <summary>Gets or sets the color of the client area of the button when the button is checked and the mouse pointer is outside the bounds of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the client area of the button.</returns>
		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600066A RID: 1642 RVA: 0x0001BC11 File Offset: 0x00019E11
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(typeof(Color), "")]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public Color CheckedBackColor
		{
			get
			{
				return this.checkedBackColor;
			}
		}

		/// <summary>Gets or sets the color of the client area of the button when the mouse is pressed within the bounds of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the client area of the button.</returns>
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0001BC19 File Offset: 0x00019E19
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(typeof(Color), "")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public Color MouseDownBackColor
		{
			get
			{
				return this.mouseDownBackColor;
			}
		}

		/// <summary>Gets or sets the color of the client area of the button when the mouse pointer is within the bounds of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure representing the color of the client area of the button.</returns>
		// Token: 0x1700018A RID: 394
		// (get) Token: 0x0600066C RID: 1644 RVA: 0x0001BC21 File Offset: 0x00019E21
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(typeof(Color), "")]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		public Color MouseOverBackColor
		{
			get
			{
				return this.mouseOverBackColor;
			}
		}

		// Token: 0x0400042D RID: 1069
		private Color borderColor = Color.Empty;

		// Token: 0x0400042E RID: 1070
		private int borderSize = 1;

		// Token: 0x0400042F RID: 1071
		private Color checkedBackColor = Color.Empty;

		// Token: 0x04000430 RID: 1072
		private Color mouseDownBackColor = Color.Empty;

		// Token: 0x04000431 RID: 1073
		private Color mouseOverBackColor = Color.Empty;

		// Token: 0x04000432 RID: 1074
		private ButtonBase owner;
	}
}
