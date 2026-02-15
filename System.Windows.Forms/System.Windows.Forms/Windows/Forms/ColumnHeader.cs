using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Displays a single column header in a <see cref="T:System.Windows.Forms.ListView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000038 RID: 56
	[DefaultProperty("Text")]
	[DesignTimeVisible(false)]
	[ToolboxItem(false)]
	[TypeConverter(typeof(ColumnHeaderConverter))]
	public class ColumnHeader : Component, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeader" /> class.</summary>
		// Token: 0x06000127 RID: 295 RVA: 0x00005264 File Offset: 0x00003464
		public ColumnHeader()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeader" /> class with the image specified.</summary>
		/// <param name="imageIndex">The index of the image to display in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
		// Token: 0x06000128 RID: 296 RVA: 0x000052CC File Offset: 0x000034CC
		public ColumnHeader(int imageIndex)
		{
			this.ImageIndex = imageIndex;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ColumnHeader" /> class with the image specified.</summary>
		/// <param name="imageKey">The key of the image to display in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</param>
		// Token: 0x06000129 RID: 297 RVA: 0x0000533C File Offset: 0x0000353C
		public ColumnHeader(string imageKey)
		{
			this.ImageKey = imageKey;
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000053AB File Offset: 0x000035AB
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000053B3 File Offset: 0x000035B3
		internal bool Pressed
		{
			get
			{
				return this.pressed;
			}
			set
			{
				this.pressed = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000053BC File Offset: 0x000035BC
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000053C9 File Offset: 0x000035C9
		internal int X
		{
			get
			{
				return this.column_rect.X;
			}
			set
			{
				this.column_rect.X = value;
			}
		}

		// Token: 0x17000058 RID: 88
		// (set) Token: 0x0600012E RID: 302 RVA: 0x000053D7 File Offset: 0x000035D7
		internal int Y
		{
			set
			{
				this.column_rect.Y = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000053E5 File Offset: 0x000035E5
		internal int Wd
		{
			get
			{
				return this.column_rect.Width;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000053F2 File Offset: 0x000035F2
		internal int Ht
		{
			get
			{
				return this.column_rect.Height;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000131 RID: 305 RVA: 0x000053FF File Offset: 0x000035FF
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00005407 File Offset: 0x00003607
		internal Rectangle Rect
		{
			get
			{
				return this.column_rect;
			}
			set
			{
				this.column_rect = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00005410 File Offset: 0x00003610
		internal StringFormat Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00005418 File Offset: 0x00003618
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00005420 File Offset: 0x00003620
		internal int InternalDisplayIndex
		{
			get
			{
				return this.display_index;
			}
			set
			{
				this.display_index = value;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000542C File Offset: 0x0000362C
		internal void CalcColumnHeader()
		{
			if (this.text_alignment == HorizontalAlignment.Center)
			{
				this.format.Alignment = StringAlignment.Center;
			}
			else if (this.text_alignment == HorizontalAlignment.Right)
			{
				this.format.Alignment = StringAlignment.Far;
			}
			else
			{
				this.format.Alignment = StringAlignment.Near;
			}
			this.format.LineAlignment = StringAlignment.Center;
			this.format.Trimming = StringTrimming.EllipsisCharacter;
			this.format.FormatFlags = StringFormatFlags.NoWrap;
			if (this.owner != null)
			{
				this.column_rect.Height = ThemeEngine.Current.ListViewGetHeaderHeight(this.owner, this.owner.Font);
			}
			else
			{
				this.column_rect.Height = ThemeEngine.Current.ListViewGetHeaderHeight(null, ThemeEngine.Current.DefaultFont);
			}
			this.column_rect.Width = 0;
			if (this.width >= 0)
			{
				this.column_rect.Width = this.width;
				return;
			}
			if (this.Index != -1)
			{
				bool flag = this.Index == this.owner.Columns.Count - 1 && this.width == -2;
				Rectangle clientRectangle = this.owner.ClientRectangle;
				this.column_rect.Width = this.owner.GetChildColumnSize(this.Index).Width;
				this.width = this.column_rect.Width;
				if (flag && this.column_rect.X + this.column_rect.Width < clientRectangle.Width)
				{
					this.width = clientRectangle.Width - this.column_rect.X;
					if (this.owner.v_scroll.Visible)
					{
						this.width -= this.owner.v_scroll.Width;
					}
					this.column_rect.Width = this.width;
				}
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005601 File Offset: 0x00003801
		internal void SetListView(ListView list_view)
		{
			this.owner = list_view;
		}

		/// <summary>Gets or sets the index of the image displayed in the <see cref="T:System.Windows.Forms.ColumnHeader" />. </summary>
		/// <returns>The index of the image displayed in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Windows.Forms.ColumnHeader.ImageIndex" /> is less than -1.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000560A File Offset: 0x0000380A
		// (set) Token: 0x06000139 RID: 313 RVA: 0x00005612 File Offset: 0x00003812
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(ImageIndexConverter))]
		public int ImageIndex
		{
			get
			{
				return this.image_index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("ImageIndex");
				}
				this.image_index = value;
				this.image_key = string.Empty;
				if (this.owner != null)
				{
					this.owner.header_control.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the key of the image displayed in the column.</summary>
		/// <returns>The key of the image displayed in the column.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000564D File Offset: 0x0000384D
		// (set) Token: 0x0600013B RID: 315 RVA: 0x00005655 File Offset: 0x00003855
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(ImageKeyConverter))]
		public string ImageKey
		{
			get
			{
				return this.image_key;
			}
			set
			{
				this.image_key = ((value == null) ? string.Empty : value);
				this.image_index = -1;
				if (this.owner != null)
				{
					this.owner.header_control.Invalidate();
				}
			}
		}

		/// <summary>Gets the location with the <see cref="T:System.Windows.Forms.ListView" /> control's <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> of this column.</summary>
		/// <returns>The zero-based index of the column header within the <see cref="T:System.Windows.Forms.ListView.ColumnHeaderCollection" /> of the <see cref="T:System.Windows.Forms.ListView" /> control it is contained in.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00005687 File Offset: 0x00003887
		[Browsable(false)]
		public int Index
		{
			get
			{
				if (this.owner != null)
				{
					return this.owner.Columns.IndexOf(this);
				}
				return -1;
			}
		}

		/// <summary>Gets or sets the text displayed in the column header.</summary>
		/// <returns>The text displayed in the column header.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000056A4 File Offset: 0x000038A4
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000056AC File Offset: 0x000038AC
		[Localizable(true)]
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (this.text != value)
				{
					this.text = value;
					if (this.owner != null)
					{
						this.owner.Redraw(true);
					}
					this.OnUIATextChanged();
				}
			}
		}

		/// <summary>Gets or sets the horizontal alignment of the text displayed in the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> values. The default is <see cref="F:System.Windows.Forms.HorizontalAlignment.Left" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000056DD File Offset: 0x000038DD
		// (set) Token: 0x06000140 RID: 320 RVA: 0x000056E5 File Offset: 0x000038E5
		[DefaultValue(HorizontalAlignment.Left)]
		[Localizable(true)]
		public HorizontalAlignment TextAlign
		{
			get
			{
				return this.text_alignment;
			}
			set
			{
				this.text_alignment = value;
				if (this.owner != null)
				{
					this.owner.Redraw(true);
				}
			}
		}

		/// <summary>Gets or sets the width of the column.</summary>
		/// <returns>The width of the column, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00005702 File Offset: 0x00003902
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000570A File Offset: 0x0000390A
		[DefaultValue(60)]
		[Localizable(true)]
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (this.width != value)
				{
					this.width = value;
					if (this.owner != null)
					{
						this.owner.Redraw(true);
						this.owner.RaiseColumnWidthChanged(this);
					}
				}
			}
		}

		/// <summary>Creates an identical copy of the current <see cref="T:System.Windows.Forms.ColumnHeader" /> that is not attached to any list view control.</summary>
		/// <returns>An object representing a copy of this <see cref="T:System.Windows.Forms.ColumnHeader" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000143 RID: 323 RVA: 0x0000573C File Offset: 0x0000393C
		public object Clone()
		{
			return new ColumnHeader
			{
				text = this.text,
				text_alignment = this.text_alignment,
				width = this.width,
				owner = this.owner,
				format = (StringFormat)this.Format.Clone(),
				column_rect = Rectangle.Empty
			};
		}

		/// <summary>Returns a string representation of this column header.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the <see cref="T:System.ComponentModel.Component" />, if any, or null if the <see cref="T:System.ComponentModel.Component" /> is unnamed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000144 RID: 324 RVA: 0x0000579F File Offset: 0x0000399F
		public override string ToString()
		{
			return string.Format("ColumnHeader: Text: {0}", this.text);
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.ColumnHeader" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000145 RID: 325 RVA: 0x000057B1 File Offset: 0x000039B1
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000057BC File Offset: 0x000039BC
		private void OnUIATextChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[ColumnHeader.UIATextChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000131 RID: 305
		private StringFormat format = new StringFormat();

		// Token: 0x04000132 RID: 306
		private string text = "ColumnHeader";

		// Token: 0x04000133 RID: 307
		private HorizontalAlignment text_alignment;

		// Token: 0x04000134 RID: 308
		private int width = ThemeEngine.Current.ListViewDefaultColumnWidth;

		// Token: 0x04000135 RID: 309
		private int image_index = -1;

		// Token: 0x04000136 RID: 310
		private string image_key = string.Empty;

		// Token: 0x04000137 RID: 311
		private string name = string.Empty;

		// Token: 0x04000138 RID: 312
		private int display_index = -1;

		// Token: 0x04000139 RID: 313
		private Rectangle column_rect = Rectangle.Empty;

		// Token: 0x0400013A RID: 314
		private bool pressed;

		// Token: 0x0400013B RID: 315
		private ListView owner;

		// Token: 0x0400013C RID: 316
		private static object UIATextChangedEvent = new object();
	}
}
