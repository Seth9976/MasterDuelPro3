using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality required by text controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000193 RID: 403
	[ComVisible(true)]
	[DefaultBindingProperty("Text")]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("TextChanged")]
	[Designer("System.Windows.Forms.Design.TextBoxBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class TextBoxBase : Control
	{
		// Token: 0x06000F3E RID: 3902 RVA: 0x00045C60 File Offset: 0x00043E60
		internal TextBoxBase()
		{
			this.alignment = HorizontalAlignment.Left;
			this.accepts_return = false;
			this.accepts_tab = false;
			this.auto_size = true;
			base.InternalBorderStyle = BorderStyle.Fixed3D;
			this.actual_border_style = BorderStyle.Fixed3D;
			this.character_casing = CharacterCasing.Normal;
			this.hide_selection = true;
			this.max_length = 32767;
			this.password_char = '\0';
			this.read_only = false;
			this.word_wrap = true;
			this.richtext = false;
			this.show_selection = false;
			this.enable_links = false;
			this.list_links = new ArrayList();
			this.current_link = null;
			this.show_caret_w_selection = this is TextBox;
			this.document = new Document(this);
			this.document.WidthChanged += this.document_WidthChanged;
			this.document.HeightChanged += this.document_HeightChanged;
			this.document.Wrap = false;
			this.click_last = DateTime.Now;
			this.click_mode = CaretSelection.Position;
			base.MouseDown += this.TextBoxBase_MouseDown;
			base.MouseUp += this.TextBoxBase_MouseUp;
			base.MouseMove += this.TextBoxBase_MouseMove;
			base.SizeChanged += this.TextBoxBase_SizeChanged;
			base.FontChanged += this.TextBoxBase_FontOrColorChanged;
			base.ForeColorChanged += this.TextBoxBase_FontOrColorChanged;
			base.MouseWheel += this.TextBoxBase_MouseWheel;
			base.RightToLeftChanged += this.TextBoxBase_RightToLeftChanged;
			this.scrollbars = RichTextBoxScrollBars.None;
			this.hscroll = new ImplicitHScrollBar();
			this.hscroll.ValueChanged += this.hscroll_ValueChanged;
			this.hscroll.SetStyle(ControlStyles.Selectable, false);
			this.hscroll.Enabled = false;
			this.hscroll.Visible = false;
			this.hscroll.Maximum = int.MaxValue;
			this.vscroll = new ImplicitVScrollBar();
			this.vscroll.ValueChanged += this.vscroll_ValueChanged;
			this.vscroll.SetStyle(ControlStyles.Selectable, false);
			this.vscroll.Enabled = false;
			this.vscroll.Visible = false;
			this.vscroll.Maximum = int.MaxValue;
			base.SuspendLayout();
			base.Controls.AddImplicit(this.hscroll);
			base.Controls.AddImplicit(this.vscroll);
			base.ResumeLayout();
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick, false);
			base.SetStyle(ControlStyles.UseTextForAccessibility, false);
			base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
			this.canvas_width = base.ClientSize.Width;
			this.canvas_height = base.ClientSize.Height;
			this.document.ViewPortWidth = this.canvas_width;
			this.document.ViewPortHeight = this.canvas_height;
			this.Cursor = Cursors.IBeam;
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x00045F56 File Offset: 0x00044156
		internal string CaseAdjust(string s)
		{
			if (this.character_casing == CharacterCasing.Normal)
			{
				return s;
			}
			if (this.character_casing == CharacterCasing.Lower)
			{
				return s.ToLower();
			}
			return s.ToUpper();
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0000B888 File Offset: 0x00009A88
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			return new Size(base.Width, base.Height);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x00045F78 File Offset: 0x00044178
		internal override void HandleClick(int clicks, MouseEventArgs me)
		{
			bool style = base.GetStyle(ControlStyles.StandardClick);
			bool style2 = base.GetStyle(ControlStyles.StandardDoubleClick);
			base.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
			base.HandleClick(clicks, me);
			if (!style)
			{
				base.SetStyle(ControlStyles.StandardClick, false);
			}
			if (!style2)
			{
				base.SetStyle(ControlStyles.StandardDoubleClick, false);
			}
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x00045FCD File Offset: 0x000441CD
		internal override void PaintControlBackground(PaintEventArgs pevent)
		{
			if (!ThemeEngine.Current.TextBoxBaseShouldPaintBackground(this))
			{
				return;
			}
			base.PaintControlBackground(pevent);
		}

		/// <summary>Gets or sets a value indicating whether the height of the control automatically adjusts when the font assigned to the control is changed.</summary>
		/// <returns>true if the height of the control automatically adjusts when the font is changed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00045FE4 File Offset: 0x000441E4
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x00045FEC File Offset: 0x000441EC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(true)]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[MWFCategory("Behavior")]
		public override bool AutoSize
		{
			get
			{
				return this.auto_size;
			}
			set
			{
				if (value != this.auto_size)
				{
					this.auto_size = value;
					if (this.auto_size && this.PreferredHeight != base.Height)
					{
						base.Height = this.PreferredHeight;
					}
				}
			}
		}

		/// <summary>Gets or sets the background color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x000042CE File Offset: 0x000024CE
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x00046020 File Offset: 0x00044220
		[DispId(-501)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				this.backcolor_set = true;
				base.BackColor = this.ChangeBackColor(value);
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>The background image for the object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x00005B72 File Offset: 0x00003D72
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
		}

		/// <summary>Gets or sets the border type of the text box control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BorderStyle" /> that represents the border type of the text box control. The default is Fixed3D.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x00046036 File Offset: 0x00044236
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x00046040 File Offset: 0x00044240
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[MWFCategory("Appearance")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.actual_border_style;
			}
			set
			{
				if (value == this.actual_border_style)
				{
					return;
				}
				if (this.actual_border_style != BorderStyle.Fixed3D || value != BorderStyle.Fixed3D)
				{
					base.Invalidate();
				}
				this.actual_border_style = value;
				this.document.UpdateMargins();
				if (value != BorderStyle.Fixed3D)
				{
					value = BorderStyle.None;
				}
				base.InternalBorderStyle = value;
				this.OnBorderStyleChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets a value indicating whether the user can undo the previous operation in a text box control.</summary>
		/// <returns>true if the user can undo the previous operation performed in a text box control; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x00046095 File Offset: 0x00044295
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool CanUndo
		{
			get
			{
				return this.document.undo.CanUndo;
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the control's foreground color.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00005E5F File Offset: 0x0000405F
		// (set) Token: 0x06000F4C RID: 3916 RVA: 0x0003B989 File Offset: 0x00039B89
		[DispId(-513)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Gets or sets the lines of text in a text box control.</summary>
		/// <returns>An array of strings that contains the text in a text box control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x000460A8 File Offset: 0x000442A8
		[MergableProperty(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.Windows.Forms.Design.StringArrayEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[MWFCategory("Appearance")]
		public string[] Lines
		{
			get
			{
				int lines = this.document.Lines;
				if (lines == 1 && this.document.GetLine(1).text.Length == 0)
				{
					return new string[0];
				}
				ArrayList arrayList = new ArrayList();
				int i = 1;
				while (i <= lines)
				{
					StringBuilder stringBuilder = new StringBuilder();
					Line line;
					do
					{
						line = this.document.GetLine(i++);
						stringBuilder.Append(line.TextWithoutEnding());
					}
					while (line.ending == LineEnding.Wrap && i <= lines);
					arrayList.Add(stringBuilder.ToString());
				}
				return (string[])arrayList.ToArray(typeof(string));
			}
		}

		/// <summary>Gets or sets the maximum number of characters the user can type or paste into the text box control.</summary>
		/// <returns>The number of characters that can be entered into the control. The default is 32767.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value assigned to the property is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000F4E RID: 3918 RVA: 0x00046148 File Offset: 0x00044348
		// (set) Token: 0x06000F4F RID: 3919 RVA: 0x0004615F File Offset: 0x0004435F
		[DefaultValue(32767)]
		[Localizable(true)]
		[MWFCategory("Behavior")]
		public virtual int MaxLength
		{
			get
			{
				if (this.max_length == 2147483646)
				{
					return 0;
				}
				return this.max_length;
			}
			set
			{
				if (value != this.max_length)
				{
					if (value == 0)
					{
						value = 2147483646;
					}
					this.max_length = value;
				}
			}
		}

		/// <summary>Gets or sets a value that indicates that the text box control has been modified by the user since the control was created or its contents were last set.</summary>
		/// <returns>true if the control's contents have been modified; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003F7 RID: 1015
		// (set) Token: 0x06000F50 RID: 3920 RVA: 0x0004617B File Offset: 0x0004437B
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool Modified
		{
			set
			{
				if (value != this.modified)
				{
					this.modified = value;
					this.OnModifiedChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether this is a multiline text box control.</summary>
		/// <returns>true if the control is a multiline text box control; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00046198 File Offset: 0x00044398
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x000461A8 File Offset: 0x000443A8
		[DefaultValue(false)]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[MWFCategory("Behavior")]
		public virtual bool Multiline
		{
			get
			{
				return this.document.multiline;
			}
			set
			{
				if (value != this.document.multiline)
				{
					this.document.multiline = value;
					if (this is TextBox)
					{
						base.SetStyle(ControlStyles.FixedHeight, !value);
					}
					this.SetBoundsCore(base.Left, base.Top, base.Width, base.ExplicitBounds.Height, BoundsSpecified.None);
					if (base.Parent != null)
					{
						base.Parent.PerformLayout();
					}
					this.OnMultilineChanged(EventArgs.Empty);
				}
				if (this.document.multiline)
				{
					this.document.Wrap = this.word_wrap;
					this.document.PasswordChar = "";
				}
				else
				{
					this.document.Wrap = false;
					if (this.password_char != '\0')
					{
						if (this is TextBox)
						{
							this.document.PasswordChar = (this as TextBox).PasswordChar.ToString();
						}
					}
					else
					{
						this.document.PasswordChar = "";
					}
				}
				if (base.IsHandleCreated)
				{
					this.CalculateDocument();
				}
			}
		}

		/// <summary>Gets the preferred height for a text box.</summary>
		/// <returns>The preferred height of a text box.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x000462B4 File Offset: 0x000444B4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int PreferredHeight
		{
			get
			{
				int num = base.Height - base.ClientSize.Height;
				if (this.BorderStyle != BorderStyle.None)
				{
					return this.Font.Height + 7 + num;
				}
				return this.Font.Height + this.TopMargin + num;
			}
		}

		/// <summary>Gets or sets a value indicating whether text in the text box is read-only.</summary>
		/// <returns>true if the text box is read-only; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00046303 File Offset: 0x00044503
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x0004630C File Offset: 0x0004450C
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(false)]
		[MWFCategory("Behavior")]
		public bool ReadOnly
		{
			get
			{
				return this.read_only;
			}
			set
			{
				if (value != this.read_only)
				{
					this.read_only = value;
					if (!this.backcolor_set)
					{
						if (this.read_only)
						{
							this.background_color = SystemColors.Control;
						}
						else
						{
							this.background_color = SystemColors.Window;
						}
					}
					this.OnReadOnlyChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating the currently selected text in the control.</summary>
		/// <returns>A string that represents the currently selected text in the text box.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x00046362 File Offset: 0x00044562
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x0004636F File Offset: 0x0004456F
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string SelectedText
		{
			get
			{
				return this.document.GetSelection();
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.document.ReplaceSelection(this.CaseAdjust(value), false);
				this.ScrollToCaret();
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the number of characters selected in the text box.</summary>
		/// <returns>The number of characters selected in the text box.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0004639F File Offset: 0x0004459F
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x000463AC File Offset: 0x000445AC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int SelectionLength
		{
			get
			{
				return this.document.SelectionLength();
			}
			set
			{
				if (value < 0)
				{
					string text = string.Format("'{0}' is not a valid value for 'SelectionLength'", value);
					throw new ArgumentOutOfRangeException("SelectionLength", text);
				}
				this.document.InvalidateSelectionArea();
				if (value != 0)
				{
					this.selection_length = value;
					int num = this.document.LineTagToCharIndex(this.document.selection_start.line, this.document.selection_start.pos);
					Line line;
					LineTag lineTag;
					int num2;
					this.document.CharIndexToLineTag(num + value, out line, out lineTag, out num2);
					this.document.SetSelectionEnd(line, num2, true);
					this.document.PositionCaret(line, num2);
					return;
				}
				this.selection_length = -1;
				this.document.SetSelectionEnd(this.document.selection_start.line, this.document.selection_start.pos, true);
				this.document.PositionCaret(this.document.selection_start.line, this.document.selection_start.pos);
			}
		}

		/// <summary>Gets or sets the starting point of text selected in the text box.</summary>
		/// <returns>The starting position of text selected in the text box.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than zero.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003FD RID: 1021
		// (set) Token: 0x06000F5A RID: 3930 RVA: 0x000464AC File Offset: 0x000446AC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int SelectionStart
		{
			set
			{
				if (value < 0)
				{
					string text = string.Format("'{0}' is not a valid value for 'SelectionStart'", value);
					throw new ArgumentOutOfRangeException("SelectionStart", text);
				}
				this.has_been_focused = true;
				this.document.InvalidateSelectionArea();
				this.document.SetSelectionStart(value, false);
				if (this.selection_length > -1)
				{
					this.document.SetSelectionEnd(value + this.selection_length, true);
				}
				else
				{
					this.document.SetSelectionEnd(value, true);
				}
				this.document.PositionCaret(this.document.selection_start.line, this.document.selection_start.pos);
				this.ScrollToCaret();
			}
		}

		/// <summary>Gets or sets the current text in the text box.</summary>
		/// <returns>The text displayed in the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x00046558 File Offset: 0x00044758
		// (set) Token: 0x06000F5C RID: 3932 RVA: 0x000465D8 File Offset: 0x000447D8
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[Localizable(true)]
		public override string Text
		{
			get
			{
				if (this.document == null || this.document.Root == null || this.document.Root.text == null)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 1; i <= this.document.Lines; i++)
				{
					Line line = this.document.GetLine(i);
					stringBuilder.Append(line.text.ToString());
				}
				return stringBuilder.ToString();
			}
			set
			{
				this.has_been_focused = false;
				if (value == this.Text)
				{
					return;
				}
				this.document.Empty();
				if (value != null && value != "")
				{
					this.document.Insert(this.document.GetLine(1), 0, false, value);
				}
				else if (base.IsHandleCreated)
				{
					this.document.SetSelectionToCaret(true);
					this.CalculateDocument();
				}
				this.document.PositionCaret(this.document.GetLine(1), 0);
				this.document.SetSelectionToCaret(true);
				this.ScrollToCaret();
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the length of text in the control.</summary>
		/// <returns>The number of characters contained in the text of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x00046681 File Offset: 0x00044881
		[Browsable(false)]
		public virtual int TextLength
		{
			get
			{
				if (this.document == null || this.document.Root == null || this.document.Root.text == null)
				{
					return 0;
				}
				return this.Text.Length;
			}
		}

		/// <summary>Indicates whether a multiline text box control automatically wraps words to the beginning of the next line when necessary.</summary>
		/// <returns>true if the multiline text box control wraps words; false if the text box control automatically scrolls horizontally when the user types past the right edge of the control. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x000466B7 File Offset: 0x000448B7
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x000466BF File Offset: 0x000448BF
		[DefaultValue(true)]
		[Localizable(true)]
		[MWFCategory("Behavior")]
		public bool WordWrap
		{
			get
			{
				return this.word_wrap;
			}
			set
			{
				if (value != this.word_wrap)
				{
					if (this.document.multiline)
					{
						this.word_wrap = value;
						this.document.Wrap = value;
					}
					this.CalculateDocument();
				}
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00005B7A File Offset: 0x00003D7A
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x00005B82 File Offset: 0x00003D82
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Gets or sets the default cursor for the control.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.Cursor" /> representing the current default cursor.</returns>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x000466F0 File Offset: 0x000448F0
		protected override Cursor DefaultCursor
		{
			get
			{
				return Cursors.IBeam;
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> representing the information needed when creating a control.</returns>
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x00004663 File Offset: 0x00002863
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000F64 RID: 3940 RVA: 0x000466F7 File Offset: 0x000448F7
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 20);
			}
		}

		/// <summary>Gets or sets a value indicating whether control drawing is done in a buffer before the control is displayed. This property is not relevant for this class.</summary>
		/// <returns>true to implement double buffering on the control; otherwise, false.</returns>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x00002D70 File Offset: 0x00000F70
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override bool DoubleBuffered
		{
			get
			{
				return false;
			}
		}

		/// <summary>Copies the current selection in the text box to the Clipboard.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F66 RID: 3942 RVA: 0x00046704 File Offset: 0x00044904
		public void Copy()
		{
			DataObject dataObject = new DataObject(DataFormats.Text, this.SelectedText);
			if (this is RichTextBox)
			{
				dataObject.SetData(DataFormats.Rtf, ((RichTextBox)this).SelectedRtf);
			}
			Clipboard.SetDataObject(dataObject);
		}

		/// <summary>Moves the current selection in the text box to the Clipboard.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F67 RID: 3943 RVA: 0x00046748 File Offset: 0x00044948
		public void Cut()
		{
			DataObject dataObject = new DataObject(DataFormats.Text, this.SelectedText);
			if (this is RichTextBox)
			{
				dataObject.SetData(DataFormats.Rtf, ((RichTextBox)this).SelectedRtf);
			}
			Clipboard.SetDataObject(dataObject);
			this.document.undo.BeginUserAction(Locale.GetText("Cut"));
			this.document.ReplaceSelection(string.Empty, false);
			this.document.undo.EndUserAction();
			this.Modified = true;
			this.OnTextChanged(EventArgs.Empty);
		}

		/// <summary>Replaces the current selection in the text box with the contents of the Clipboard.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F68 RID: 3944 RVA: 0x000467D7 File Offset: 0x000449D7
		public void Paste()
		{
			this.Paste(Clipboard.GetDataObject(), null, false);
		}

		/// <summary>Scrolls the contents of the control to the current caret position.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F69 RID: 3945 RVA: 0x000467E7 File Offset: 0x000449E7
		public void ScrollToCaret()
		{
			if (base.IsHandleCreated)
			{
				this.CaretMoved(this, EventArgs.Empty);
			}
		}

		/// <summary>Selects all text in the text box.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F6A RID: 3946 RVA: 0x00046800 File Offset: 0x00044A00
		public void SelectAll()
		{
			Line line = this.document.GetLine(this.document.Lines);
			this.document.SetSelectionStart(this.document.GetLine(1), 0, false);
			this.document.SetSelectionEnd(line, line.text.Length, true);
			this.document.PositionCaret(this.document.selection_end.line, this.document.selection_end.pos);
			this.selection_length = -1;
			this.CaretMoved(this, null);
			this.document.DisplayCaret();
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0004689C File Offset: 0x00044A9C
		internal void SelectAllNoScroll()
		{
			Line line = this.document.GetLine(this.document.Lines);
			this.document.SetSelectionStart(this.document.GetLine(1), 0, false);
			this.document.SetSelectionEnd(line, line.text.Length, false);
			this.document.PositionCaret(this.document.selection_end.line, this.document.selection_end.pos);
			this.selection_length = -1;
			this.document.DisplayCaret();
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.TextBoxBase" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.TextBoxBase" />. The string includes the type and the <see cref="T:System.Windows.Forms.TextBoxBase" /> property of the control.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000F6C RID: 3948 RVA: 0x00004056 File Offset: 0x00002256
		public override string ToString()
		{
			return base.ToString() + ", Text: " + this.Text;
		}

		/// <summary>Undoes the last edit operation in the text box.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000F6D RID: 3949 RVA: 0x0004692E File Offset: 0x00044B2E
		[MonoInternalNote("Deleting is classed as Typing, instead of its own Undo event")]
		public void Undo()
		{
			if (this.document.undo.Undo())
			{
				this.Modified = true;
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x00046954 File Offset: 0x00044B54
		protected override void CreateHandle()
		{
			this.CalculateDocument();
			base.CreateHandle();
			this.document.AlignCaret();
			this.ScrollToCaret();
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0000493C File Offset: 0x00002B3C
		internal virtual void HandleLinkClicked(TextBoxBase.LinkRectangle link_clicked)
		{
		}

		/// <summary>Determines whether the specified key is an input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is an input key; otherwise, false.</returns>
		/// <param name="keyData">One of the Keys value.</param>
		// Token: 0x06000F70 RID: 3952 RVA: 0x00046974 File Offset: 0x00044B74
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) != Keys.None)
			{
				return base.IsInputKey(keyData);
			}
			Keys keys = keyData & Keys.KeyCode;
			if (keys == Keys.Tab)
			{
				return this.accepts_tab && this.document.multiline && (keyData & Keys.Control) == Keys.None;
			}
			if (keys != Keys.Return)
			{
				return keys - Keys.PageUp <= 7;
			}
			return this.accepts_return && this.document.multiline;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.BorderStyleChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000F71 RID: 3953 RVA: 0x000469E8 File Offset: 0x00044BE8
		protected virtual void OnBorderStyleChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.BorderStyleChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000F72 RID: 3954 RVA: 0x00046A16 File Offset: 0x00044C16
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (this.auto_size && !this.document.multiline && this.PreferredHeight != base.Height)
			{
				base.Height = this.PreferredHeight;
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000F73 RID: 3955 RVA: 0x00046A4E File Offset: 0x00044C4E
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.FixupHeight();
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000F74 RID: 3956 RVA: 0x00006538 File Offset: 0x00004738
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.ModifiedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000F75 RID: 3957 RVA: 0x00046A60 File Offset: 0x00044C60
		protected virtual void OnModifiedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.ModifiedChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.MultilineChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000F76 RID: 3958 RVA: 0x00046A90 File Offset: 0x00044C90
		protected virtual void OnMultilineChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.MultilineChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>This method is not relevant for this class.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000F77 RID: 3959 RVA: 0x0002541F File Offset: 0x0002361F
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBoxBase.ReadOnlyChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000F78 RID: 3960 RVA: 0x00046AC0 File Offset: 0x00044CC0
		protected virtual void OnReadOnlyChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.ReadOnlyChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Processes a command key.</summary>
		/// <returns>true if the command key was processed by the control; otherwise, false.</returns>
		/// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the shortcut key to process. </param>
		// Token: 0x06000F79 RID: 3961 RVA: 0x00046AEE File Offset: 0x00044CEE
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return base.ProcessCmdKey(ref msg, keyData);
		}

		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06000F7A RID: 3962 RVA: 0x00046AF8 File Offset: 0x00044CF8
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (this.accepts_tab && (keyData & (Keys.LButton | Keys.Back | Keys.Control)) == (Keys.LButton | Keys.Back | Keys.Control))
			{
				keyData ^= Keys.Control;
			}
			return base.ProcessDialogKey(keyData);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x00046B20 File Offset: 0x00044D20
		private bool ProcessKey(Keys keyData)
		{
			bool flag = (Control.ModifierKeys & Keys.Control) > Keys.None;
			bool flag2 = (Control.ModifierKeys & Keys.Shift) > Keys.None;
			Keys keys;
			if (this.shortcuts_enabled)
			{
				keys = keyData & Keys.KeyCode;
				if (keys <= Keys.Delete)
				{
					if (keys == Keys.Insert)
					{
						if (!this.read_only)
						{
							if (flag2)
							{
								this.Paste(Clipboard.GetDataObject(), null, true);
								return true;
							}
							if (flag)
							{
								this.Copy();
								return true;
							}
						}
						return false;
					}
					if (keys == Keys.Delete)
					{
						if (!this.read_only)
						{
							if (flag2 && !this.read_only)
							{
								this.Cut();
								return true;
							}
							if (this.document.selection_visible)
							{
								this.document.ReplaceSelection("", false);
							}
							else if (this.document.CaretPosition >= this.document.CaretLine.TextLengthWithoutEnding())
							{
								if (this.document.CaretLine.LineNo < this.document.Lines)
								{
									Line line = this.document.GetLine(this.document.CaretLine.LineNo + 1);
									this.document.Invalidate(line, 0, line, line.text.Length);
									this.document.Combine(this.document.CaretLine, line);
									this.document.UpdateView(this.document.CaretLine, this.document.Lines, 0);
								}
							}
							else if (!flag)
							{
								this.document.DeleteChar(this.document.CaretTag.Line, this.document.CaretPosition, true);
							}
							else
							{
								int num = this.document.CaretPosition;
								while (num < this.document.CaretLine.Text.Length && !Document.IsWordSeparator(this.document.CaretLine.Text[num]))
								{
									num++;
								}
								if (num < this.document.CaretLine.Text.Length)
								{
									num++;
								}
								this.document.DeleteChars(this.document.CaretTag.Line, this.document.CaretPosition, num - this.document.CaretPosition);
							}
							this.document.AlignCaret();
							this.document.UpdateCaret();
							this.CaretMoved(this, null);
							this.Modified = true;
							this.OnTextChanged(EventArgs.Empty);
							return true;
						}
					}
				}
				else if (keys != Keys.A)
				{
					if (keys != Keys.C)
					{
						switch (keys)
						{
						case Keys.V:
							return flag && !this.read_only && this.Paste(Clipboard.GetDataObject(), null, true);
						case Keys.X:
							if (flag && !this.read_only)
							{
								this.Cut();
								return true;
							}
							return false;
						case Keys.Z:
							if (flag && !this.read_only)
							{
								this.Undo();
								return true;
							}
							return false;
						}
					}
					else
					{
						if (flag)
						{
							this.Copy();
							return true;
						}
						return false;
					}
				}
				else
				{
					if (flag)
					{
						this.SelectAll();
						return true;
					}
					return false;
				}
			}
			keys = keyData & Keys.KeyCode;
			if (keys != Keys.Tab)
			{
				switch (keys)
				{
				case Keys.PageUp:
					if ((Control.ModifierKeys & Keys.Control) != Keys.None)
					{
						this.document.MoveCaret(CaretDirection.CtrlPgUp);
					}
					else
					{
						this.document.MoveCaret(CaretDirection.PgUp);
					}
					this.document.DisplayCaret();
					return true;
				case Keys.PageDown:
					if ((Control.ModifierKeys & Keys.Control) != Keys.None)
					{
						this.document.MoveCaret(CaretDirection.CtrlPgDn);
					}
					else
					{
						this.document.MoveCaret(CaretDirection.PgDn);
					}
					this.document.DisplayCaret();
					return true;
				case Keys.End:
					if ((Control.ModifierKeys & Keys.Control) != Keys.None)
					{
						this.document.MoveCaret(CaretDirection.CtrlEnd);
					}
					else
					{
						this.document.MoveCaret(CaretDirection.End);
					}
					if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
					{
						this.document.SetSelectionToCaret(true);
					}
					else
					{
						this.document.SetSelectionToCaret(false);
					}
					this.CaretMoved(this, null);
					return true;
				case Keys.Home:
					if ((Control.ModifierKeys & Keys.Control) != Keys.None)
					{
						this.document.MoveCaret(CaretDirection.CtrlHome);
					}
					else
					{
						this.document.MoveCaret(CaretDirection.Home);
					}
					if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
					{
						this.document.SetSelectionToCaret(true);
					}
					else
					{
						this.document.SetSelectionToCaret(false);
					}
					this.CaretMoved(this, null);
					return true;
				case Keys.Left:
					if (flag)
					{
						this.document.MoveCaret(CaretDirection.WordBack);
					}
					else if (!this.document.selection_visible || flag2)
					{
						this.document.MoveCaret(CaretDirection.CharBack);
					}
					else
					{
						this.document.MoveCaret(CaretDirection.SelectionStart);
					}
					if (!flag2)
					{
						this.document.SetSelectionToCaret(true);
					}
					else
					{
						this.document.SetSelectionToCaret(false);
					}
					this.CaretMoved(this, null);
					return true;
				case Keys.Up:
					if (flag)
					{
						if (this.document.CaretPosition == 0)
						{
							this.document.MoveCaret(CaretDirection.LineUp);
						}
						else
						{
							this.document.MoveCaret(CaretDirection.Home);
						}
					}
					else
					{
						this.document.MoveCaret(CaretDirection.LineUp);
					}
					if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
					{
						this.document.SetSelectionToCaret(true);
					}
					else
					{
						this.document.SetSelectionToCaret(false);
					}
					this.CaretMoved(this, null);
					return true;
				case Keys.Right:
					if (flag)
					{
						this.document.MoveCaret(CaretDirection.WordForward);
					}
					else if (!this.document.selection_visible || flag2)
					{
						this.document.MoveCaret(CaretDirection.CharForward);
					}
					else
					{
						this.document.MoveCaret(CaretDirection.SelectionEnd);
					}
					if (!flag2)
					{
						this.document.SetSelectionToCaret(true);
					}
					else
					{
						this.document.SetSelectionToCaret(false);
					}
					this.CaretMoved(this, null);
					return true;
				case Keys.Down:
					if (flag)
					{
						if (this.document.CaretPosition == this.document.CaretLine.Text.Length)
						{
							this.document.MoveCaret(CaretDirection.LineDown);
						}
						else
						{
							this.document.MoveCaret(CaretDirection.End);
						}
					}
					else
					{
						this.document.MoveCaret(CaretDirection.LineDown);
					}
					if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
					{
						this.document.SetSelectionToCaret(true);
					}
					else
					{
						this.document.SetSelectionToCaret(false);
					}
					this.CaretMoved(this, null);
					return true;
				}
			}
			else if (!this.read_only && this.accepts_tab && this.document.multiline)
			{
				this.document.InsertCharAtCaret('\t', true);
				this.CaretMoved(this, null);
				this.Modified = true;
				this.OnTextChanged(EventArgs.Empty);
				return true;
			}
			return false;
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0000493C File Offset: 0x00002B3C
		internal virtual void RaiseSelectionChanged()
		{
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x00047180 File Offset: 0x00045380
		private void HandleBackspace(bool control)
		{
			bool flag = false;
			if (this.document.selection_visible)
			{
				this.document.undo.BeginUserAction(Locale.GetText("Delete"));
				this.document.ReplaceSelection("", false);
				this.document.undo.EndUserAction();
				flag = true;
				this.document.SetSelectionToCaret(true);
			}
			else
			{
				this.document.SetSelectionToCaret(true);
				if (this.document.CaretPosition == 0)
				{
					if (this.document.CaretLine.LineNo > 1)
					{
						Line line = this.document.GetLine(this.document.CaretLine.LineNo - 1);
						int num = line.TextLengthWithoutEnding();
						this.document.Invalidate(line, 0, line, line.text.Length);
						this.document.Combine(line, this.document.CaretLine);
						this.document.UpdateView(line, this.document.Lines - line.LineNo, 0);
						this.document.PositionCaret(line, num);
						this.document.SetSelectionToCaret(true);
						this.document.UpdateCaret();
						flag = true;
					}
				}
				else
				{
					if (!control || this.document.CaretPosition == 0)
					{
						LineTag caretTag = this.document.CaretTag;
						int caretPosition = this.document.CaretPosition;
						this.document.MoveCaret(CaretDirection.CharBack);
						this.document.DeleteChar(caretTag.Line, caretPosition, false);
						this.document.SetSelectionToCaret(true);
					}
					else
					{
						int num2 = this.document.CaretPosition - 1;
						while (num2 > 0 && !Document.IsWordSeparator(this.document.CaretLine.Text[num2 - 1]))
						{
							num2--;
						}
						this.document.undo.BeginUserAction(Locale.GetText("Delete"));
						this.document.DeleteChars(this.document.CaretTag.Line, num2, this.document.CaretPosition - num2);
						this.document.undo.EndUserAction();
						this.document.PositionCaret(this.document.CaretLine, num2);
						this.document.SetSelectionToCaret(true);
					}
					this.document.UpdateCaret();
					flag = true;
				}
			}
			this.CaretMoved(this, null);
			if (flag)
			{
				this.Modified = true;
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x000473F4 File Offset: 0x000455F4
		private void HandleEnter()
		{
			if (!this.read_only && this.document.multiline && (this.accepts_return || (base.FindForm() != null && base.FindForm().AcceptButton == null) || (Control.ModifierKeys & Keys.Control) != Keys.None))
			{
				if (this.document.selection_visible)
				{
					this.document.ReplaceSelection("", false);
				}
				Line caretLine = this.document.CaretLine;
				this.document.Split(this.document.CaretLine, this.document.CaretTag, this.document.CaretPosition);
				caretLine.ending = this.document.StringToLineEnding(Environment.NewLine);
				this.document.InsertString(caretLine, caretLine.text.Length, this.document.LineEndingToString(caretLine.ending));
				this.document.UpdateView(caretLine, this.document.Lines - caretLine.line_no, 0);
				this.CaretMoved(this, null);
				this.Modified = true;
				this.OnTextChanged(EventArgs.Empty);
			}
		}

		/// <summary>Sets the specified bounds of the <see cref="T:System.Windows.Forms.TextBoxBase" /> control.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">Not used.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06000F7F RID: 3967 RVA: 0x00047518 File Offset: 0x00045718
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (!this.richtext && !this.document.multiline && height != this.PreferredHeight)
			{
				if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
				{
					Rectangle explicitBounds = base.ExplicitBounds;
					explicitBounds.Height = height;
					base.ExplicitBounds = explicitBounds;
					specified &= ~BoundsSpecified.Height;
				}
				height = this.PreferredHeight;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <param name="m">A Windows Message Object. </param>
		// Token: 0x06000F80 RID: 3968 RVA: 0x00047580 File Offset: 0x00045780
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg <= Msg.WM_KILLFOCUS)
			{
				if (msg == Msg.WM_SETFOCUS)
				{
					base.WndProc(ref m);
					this.document.CaretHasFocus();
					return;
				}
				if (msg == Msg.WM_KILLFOCUS)
				{
					base.WndProc(ref m);
					this.document.CaretLostFocus();
					return;
				}
			}
			else if (msg != Msg.WM_NCPAINT)
			{
				if (msg != Msg.WM_KEYDOWN)
				{
					if (msg == Msg.WM_CHAR)
					{
						if (this.ProcessKeyMessage(ref m))
						{
							m.Result = IntPtr.Zero;
							return;
						}
						if (this.read_only)
						{
							return;
						}
						m.Result = IntPtr.Zero;
						int num = m.WParam.ToInt32();
						if (num == 127)
						{
							this.HandleBackspace(true);
							return;
						}
						if (num >= 32)
						{
							if (this.document.selection_visible)
							{
								this.document.ReplaceSelection("", false);
							}
							char c = (char)(int)m.WParam;
							CharacterCasing characterCasing = this.character_casing;
							if (characterCasing != CharacterCasing.Upper)
							{
								if (characterCasing == CharacterCasing.Lower)
								{
									c = char.ToLower((char)(int)m.WParam);
								}
							}
							else
							{
								c = char.ToUpper((char)(int)m.WParam);
							}
							if (this.document.Length < this.max_length)
							{
								this.document.InsertCharAtCaret(c, true);
								this.OnTextUpdate();
								this.CaretMoved(this, null);
								this.Modified = true;
								this.OnTextChanged(EventArgs.Empty);
								return;
							}
							XplatUI.AudibleAlert(AlertType.Default);
							return;
						}
						else
						{
							if (num == 8)
							{
								this.HandleBackspace(false);
								return;
							}
							if (num == 13)
							{
								this.HandleEnter();
							}
							return;
						}
					}
				}
				else
				{
					if (this.ProcessKeyMessage(ref m) || this.ProcessKey((Keys)(m.WParam.ToInt32() | (int)XplatUI.State.ModifierKeys)))
					{
						m.Result = IntPtr.Zero;
						return;
					}
					this.DefWndProc(ref m);
					return;
				}
			}
			else
			{
				if (!ThemeEngine.Current.TextBoxBaseHandleWmNcPaint(this, ref m))
				{
					base.WndProc(ref m);
					return;
				}
				return;
			}
			base.WndProc(ref m);
		}

		/// <summary>Occurs when the text box is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x06000F81 RID: 3969 RVA: 0x0004775A File Offset: 0x0004595A
		// (remove) Token: 0x06000F82 RID: 3970 RVA: 0x00047763 File Offset: 0x00045963
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x00002D70 File Offset: 0x00000F70
		internal override bool ScaleChildrenInternal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000F84 RID: 3972 RVA: 0x0004776C File Offset: 0x0004596C
		// (set) Token: 0x06000F85 RID: 3973 RVA: 0x00047786 File Offset: 0x00045986
		internal bool ShowSelection
		{
			get
			{
				return this.show_selection || !this.hide_selection || this.has_focus;
			}
			set
			{
				if (this.show_selection == value)
				{
					return;
				}
				this.show_selection = value;
				this.document.InvalidateSelectionArea();
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x000477A4 File Offset: 0x000459A4
		// (set) Token: 0x06000F87 RID: 3975 RVA: 0x000477B1 File Offset: 0x000459B1
		internal int TopMargin
		{
			get
			{
				return this.document.top_margin;
			}
			set
			{
				this.document.top_margin = value;
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x000477BF File Offset: 0x000459BF
		internal Graphics CreateGraphicsInternal()
		{
			if (base.IsHandleCreated)
			{
				return base.CreateGraphics();
			}
			return base.DeviceContext;
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x000477D6 File Offset: 0x000459D6
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			this.Draw(pevent.Graphics, pevent.ClipRectangle);
			pevent.Handled = true;
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000477F1 File Offset: 0x000459F1
		internal void Draw(Graphics g, Rectangle clippingArea)
		{
			ThemeEngine.Current.TextBoxBaseFillBackground(this, g, clippingArea);
			this.document.Draw(g, clippingArea);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0004780D File Offset: 0x00045A0D
		private void FixupHeight()
		{
			if (!this.richtext && !this.document.multiline && this.PreferredHeight != base.Height)
			{
				base.Height = this.PreferredHeight;
			}
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x00047840 File Offset: 0x00045A40
		private bool IsDoubleClick(MouseEventArgs e)
		{
			if ((DateTime.Now - this.click_last).TotalMilliseconds > (double)SystemInformation.DoubleClickTime)
			{
				return false;
			}
			Size doubleClickSize = SystemInformation.DoubleClickSize;
			return e.X >= this.click_point_x - doubleClickSize.Width / 2 && e.X <= this.click_point_x + doubleClickSize.Width / 2 && e.Y >= this.click_point_y - doubleClickSize.Height / 2 && e.Y <= this.click_point_y + doubleClickSize.Height / 2;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x000478DC File Offset: 0x00045ADC
		private void TextBoxBase_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if ((Control.ModifierKeys & Keys.Shift) > Keys.None)
				{
					this.document.PositionCaret(e.X + this.document.ViewPortX, e.Y + this.document.ViewPortY);
					this.document.SetSelectionToCaret(false);
					this.document.DisplayCaret();
					return;
				}
				bool flag = this.IsDoubleClick(e);
				if (this.current_link != null)
				{
					this.HandleLinkClicked(this.current_link);
					return;
				}
				if (this.document.selection_visible && !flag)
				{
					this.document.SetSelectionToCaret(true);
					this.click_mode = CaretSelection.Position;
				}
				this.document.PositionCaret(e.X + this.document.ViewPortX, e.Y + this.document.ViewPortY);
				if (flag)
				{
					switch (this.click_mode)
					{
					case CaretSelection.Position:
						this.SelectWord();
						this.click_mode = CaretSelection.Word;
						break;
					case CaretSelection.Word:
						if (this is TextBox)
						{
							this.document.SetSelectionToCaret(true);
							this.click_mode = CaretSelection.Position;
						}
						else
						{
							this.document.ExpandSelection(CaretSelection.Line, false);
							this.click_mode = CaretSelection.Line;
						}
						break;
					case CaretSelection.Line:
						this.document.SetSelectionToCaret(true);
						this.SelectWord();
						this.click_mode = CaretSelection.Word;
						break;
					}
				}
				else
				{
					this.document.SetSelectionToCaret(true);
					this.click_mode = CaretSelection.Position;
				}
				this.click_point_x = e.X;
				this.click_point_y = e.Y;
				this.click_last = DateTime.Now;
			}
			if (e.Button == MouseButtons.Middle && XplatUI.RunningOnUnix)
			{
				Document.Marker marker;
				marker.tag = this.document.FindCursor(e.X + this.document.ViewPortX, e.Y + this.document.ViewPortY, out marker.pos);
				marker.line = marker.tag.Line;
				marker.height = marker.tag.Height;
				this.document.SetSelection(marker.line, marker.pos, marker.line, marker.pos);
				this.Paste(Clipboard.GetDataObject(true), null, true);
			}
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x00047B20 File Offset: 0x00045D20
		private void TextBoxBase_MouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (this.click_mode == CaretSelection.Position)
				{
					this.document.SetSelectionToCaret(false);
					this.document.DisplayCaret();
					if (this.Text.Length > 0)
					{
						this.RaiseSelectionChanged();
					}
				}
				if (this.scroll_timer != null)
				{
					this.scroll_timer.Enabled = false;
				}
				return;
			}
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x00047B84 File Offset: 0x00045D84
		private void SizeControls()
		{
			if (this.hscroll.Visible)
			{
				this.canvas_height = base.ClientSize.Height - this.hscroll.Height;
			}
			else
			{
				this.canvas_height = base.ClientSize.Height;
			}
			if (this.vscroll.Visible)
			{
				this.canvas_width = base.ClientSize.Width - this.vscroll.Width;
				if (this.GetInheritedRtoL() == RightToLeft.Yes)
				{
					this.document.OffsetX = this.vscroll.Width;
				}
				else
				{
					this.document.OffsetX = 0;
				}
			}
			else
			{
				this.canvas_width = base.ClientSize.Width;
				this.document.OffsetX = 0;
			}
			this.document.ViewPortWidth = this.canvas_width;
			this.document.ViewPortHeight = this.canvas_height;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00047C74 File Offset: 0x00045E74
		private void PositionControls()
		{
			if (this.canvas_height < 1 || this.canvas_width < 1)
			{
				return;
			}
			int num = (this.vscroll.Visible ? this.vscroll.Width : 0);
			int num2 = (this.hscroll.Visible ? this.hscroll.Height : 0);
			if (this.GetInheritedRtoL() == RightToLeft.Yes)
			{
				this.hscroll.Bounds = new Rectangle(base.ClientRectangle.Left + num, Math.Max(0, base.ClientRectangle.Height - this.hscroll.Height), base.ClientSize.Width, this.hscroll.Height);
				this.vscroll.Bounds = new Rectangle(base.ClientRectangle.Left, base.ClientRectangle.Top, this.vscroll.Width, Math.Max(0, base.ClientSize.Height - num2));
				return;
			}
			this.hscroll.Bounds = new Rectangle(base.ClientRectangle.Left, Math.Max(0, base.ClientRectangle.Height - this.hscroll.Height), Math.Max(0, base.ClientSize.Width - num), this.hscroll.Height);
			this.vscroll.Bounds = new Rectangle(Math.Max(0, base.ClientRectangle.Right - this.vscroll.Width), base.ClientRectangle.Top, this.vscroll.Width, Math.Max(0, base.ClientSize.Height - num2));
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x00047E40 File Offset: 0x00046040
		internal RightToLeft GetInheritedRtoL()
		{
			for (Control control = this; control != null; control = control.Parent)
			{
				if (control.RightToLeft != RightToLeft.Inherit)
				{
					return control.RightToLeft;
				}
			}
			return RightToLeft.No;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x00047E6C File Offset: 0x0004606C
		private void TextBoxBase_SizeChanged(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				this.CalculateDocument();
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00047E6C File Offset: 0x0004606C
		private void TextBoxBase_RightToLeftChanged(object o, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				this.CalculateDocument();
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x00047E7C File Offset: 0x0004607C
		private void TextBoxBase_MouseWheel(object sender, MouseEventArgs e)
		{
			if (!this.vscroll.Enabled)
			{
				return;
			}
			if (e.Delta < 0)
			{
				this.vscroll.Value = Math.Min(this.vscroll.Value + SystemInformation.MouseWheelScrollLines * 5, Math.Max(0, this.vscroll.Maximum - this.document.ViewPortHeight + 1));
				return;
			}
			this.vscroll.Value = Math.Max(0, this.vscroll.Value - SystemInformation.MouseWheelScrollLines * 5);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00047F08 File Offset: 0x00046108
		internal virtual void SelectWord()
		{
			StringBuilder text = this.document.caret.line.text;
			int num = this.document.caret.pos;
			int i = this.document.caret.pos;
			if (text.Length >= 1)
			{
				if (num > 0)
				{
					num--;
					i--;
				}
				while (num > 0 && text[num] == ' ')
				{
					num--;
				}
				if (num > 0)
				{
					while (num > 0 && text[num] != ' ')
					{
						num--;
					}
					if (text[num] == ' ')
					{
						num++;
					}
				}
				if (text[i] == ' ')
				{
					while (i < text.Length)
					{
						if (text[i] != ' ')
						{
							break;
						}
						i++;
					}
				}
				else
				{
					while (i < text.Length)
					{
						if (text[i] == ' ')
						{
							break;
						}
						i++;
					}
					while (i < text.Length && text[i] == ' ')
					{
						i++;
					}
				}
				this.document.SetSelection(this.document.caret.line, num, this.document.caret.line, i);
				this.document.PositionCaret(this.document.selection_end.line, this.document.selection_end.pos);
				this.document.DisplayCaret();
				return;
			}
			if (this.document.caret.line.line_no >= this.document.Lines)
			{
				return;
			}
			Line line = this.document.GetLine(this.document.caret.line.line_no + 1);
			this.document.PositionCaret(line, 0);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000480B0 File Offset: 0x000462B0
		internal void CalculateDocument()
		{
			this.CalculateScrollBars();
			this.document.RecalculateDocument(this.CreateGraphicsInternal());
			if (this.document.caret.line != null && this.document.caret.line.Y < this.document.ViewPortHeight)
			{
				this.vscroll.Value = 0;
			}
			base.Invalidate();
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0004811C File Offset: 0x0004631C
		internal void CalculateScrollBars()
		{
			this.SizeControls();
			if (this.document.Width >= this.document.ViewPortWidth)
			{
				this.hscroll.SetValues(0, Math.Max(1, this.document.Width), -1, (this.document.ViewPortWidth < 0) ? 0 : this.document.ViewPortWidth);
				if (this.document.multiline)
				{
					this.hscroll.Enabled = true;
				}
			}
			else
			{
				this.hscroll.Enabled = false;
				this.hscroll.Maximum = this.document.ViewPortWidth;
			}
			if (this.document.Height >= this.document.ViewPortHeight)
			{
				this.vscroll.SetValues(0, Math.Max(1, this.document.Height), -1, (this.document.ViewPortHeight < 0) ? 0 : this.document.ViewPortHeight);
				if (this.document.multiline)
				{
					this.vscroll.Enabled = true;
				}
			}
			else
			{
				this.vscroll.Enabled = false;
				this.vscroll.Maximum = this.document.ViewPortHeight;
			}
			RichTextBoxScrollBars richTextBoxScrollBars;
			if (!this.WordWrap)
			{
				richTextBoxScrollBars = this.scrollbars;
				if (richTextBoxScrollBars <= RichTextBoxScrollBars.Both)
				{
					if (richTextBoxScrollBars == RichTextBoxScrollBars.Horizontal || richTextBoxScrollBars == RichTextBoxScrollBars.Both)
					{
						if (this.richtext)
						{
							this.hscroll.Visible = this.hscroll.Enabled;
							goto IL_01A8;
						}
						this.hscroll.Visible = this.Multiline;
						goto IL_01A8;
					}
				}
				else if (richTextBoxScrollBars == RichTextBoxScrollBars.ForcedHorizontal || richTextBoxScrollBars == RichTextBoxScrollBars.ForcedBoth)
				{
					this.hscroll.Visible = true;
					goto IL_01A8;
				}
				this.hscroll.Visible = false;
			}
			else
			{
				this.hscroll.Visible = false;
			}
			IL_01A8:
			richTextBoxScrollBars = this.scrollbars;
			if (richTextBoxScrollBars - RichTextBoxScrollBars.Vertical > 1)
			{
				if (richTextBoxScrollBars - RichTextBoxScrollBars.ForcedVertical > 1)
				{
					this.vscroll.Visible = false;
				}
				else
				{
					this.vscroll.Visible = true;
				}
			}
			else if (this.richtext)
			{
				this.vscroll.Visible = this.vscroll.Enabled;
			}
			else
			{
				this.vscroll.Visible = this.Multiline;
			}
			this.PositionControls();
			this.SizeControls();
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00048340 File Offset: 0x00046540
		private void document_WidthChanged(object sender, EventArgs e)
		{
			this.CalculateScrollBars();
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00048340 File Offset: 0x00046540
		private void document_HeightChanged(object sender, EventArgs e)
		{
			this.CalculateScrollBars();
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00048348 File Offset: 0x00046548
		private void ScrollLinks(int xChange, int yChange)
		{
			foreach (object obj in this.list_links)
			{
				((TextBoxBase.LinkRectangle)obj).Scroll(xChange, yChange);
			}
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x000483A0 File Offset: 0x000465A0
		private void hscroll_ValueChanged(object sender, EventArgs e)
		{
			int viewPortX = this.document.ViewPortX;
			this.document.ViewPortX = this.hscroll.Value;
			if (this.Focused)
			{
				this.document.CaretLostFocus();
			}
			if (this.vscroll.Visible)
			{
				if (this.GetInheritedRtoL() == RightToLeft.Yes)
				{
					XplatUI.ScrollWindow(base.Handle, new Rectangle(this.vscroll.Width, 0, base.ClientSize.Width - this.vscroll.Width, base.ClientSize.Height), viewPortX - this.hscroll.Value, 0, false);
				}
				else
				{
					XplatUI.ScrollWindow(base.Handle, new Rectangle(0, 0, base.ClientSize.Width - this.vscroll.Width, base.ClientSize.Height), viewPortX - this.hscroll.Value, 0, false);
				}
			}
			else
			{
				XplatUI.ScrollWindow(base.Handle, base.ClientRectangle, viewPortX - this.hscroll.Value, 0, false);
			}
			this.ScrollLinks(viewPortX - this.hscroll.Value, 0);
			if (this.Focused)
			{
				this.document.CaretHasFocus();
			}
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.HScrolledEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x00048508 File Offset: 0x00046708
		private void vscroll_ValueChanged(object sender, EventArgs e)
		{
			int viewPortY = this.document.ViewPortY;
			this.document.ViewPortY = this.vscroll.Value;
			if (this.Focused)
			{
				this.document.CaretLostFocus();
			}
			if (this.hscroll.Visible)
			{
				XplatUI.ScrollWindow(base.Handle, new Rectangle(0, 0, base.ClientSize.Width, base.ClientSize.Height - this.hscroll.Height), 0, viewPortY - this.vscroll.Value, false);
			}
			else
			{
				XplatUI.ScrollWindow(base.Handle, base.ClientRectangle, 0, viewPortY - this.vscroll.Value, false);
			}
			this.ScrollLinks(0, viewPortY - this.vscroll.Value);
			if (this.Focused)
			{
				this.document.CaretHasFocus();
			}
			EventHandler eventHandler = (EventHandler)base.Events[TextBoxBase.VScrolledEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00048610 File Offset: 0x00046810
		private void TextBoxBase_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && base.Capture)
			{
				if (!base.ClientRectangle.Contains(e.X, e.Y))
				{
					if (this.scroll_timer == null)
					{
						this.scroll_timer = new Timer();
						this.scroll_timer.Interval = 100;
						this.scroll_timer.Tick += this.ScrollTimerTickHandler;
					}
					if (!this.scroll_timer.Enabled)
					{
						this.scroll_timer.Start();
						this.ScrollTimerTickHandler(null, EventArgs.Empty);
					}
				}
				this.document.PositionCaret(e.X + this.document.ViewPortX, e.Y + this.document.ViewPortY);
				if (this.click_mode == CaretSelection.Position)
				{
					this.document.SetSelectionToCaret(false);
					this.document.DisplayCaret();
				}
			}
			bool flag = false;
			foreach (object obj in this.list_links)
			{
				TextBoxBase.LinkRectangle linkRectangle = (TextBoxBase.LinkRectangle)obj;
				if (linkRectangle.LinkAreaRectangle.Contains(e.X, e.Y))
				{
					XplatUI.SetCursor(this.window.Handle, Cursors.Hand.handle);
					flag = true;
					this.current_link = linkRectangle;
					break;
				}
			}
			if (!flag)
			{
				XplatUI.SetCursor(this.window.Handle, this.DefaultCursor.handle);
				this.current_link = null;
			}
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000487B0 File Offset: 0x000469B0
		private void TextBoxBase_FontOrColorChanged(object sender, EventArgs e)
		{
			this.document.SuspendRecalc();
			for (int i = 1; i <= this.document.Lines; i++)
			{
				Line line = this.document.GetLine(i);
				if (LineTag.FormatText(line, 1, line.text.Length, this.Font, this.ForeColor, Color.Empty, FormatSpecified.Font | FormatSpecified.Color))
				{
					this.document.RecalculateDocument(this.CreateGraphicsInternal(), line.LineNo, line.LineNo, false);
				}
			}
			this.document.ResumeRecalc(false);
			this.document.AlignCaret();
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00048848 File Offset: 0x00046A48
		private void ScrollTimerTickHandler(object sender, EventArgs e)
		{
			Point point = Cursor.Position;
			point = base.PointToClient(point);
			if (point.X < base.ClientRectangle.Left)
			{
				this.document.MoveCaret(CaretDirection.CharBackNoWrap);
				this.document.SetSelectionToCaret(false);
				this.CaretMoved(this, null);
				return;
			}
			if (point.X > base.ClientRectangle.Right)
			{
				this.document.MoveCaret(CaretDirection.CharForwardNoWrap);
				this.document.SetSelectionToCaret(false);
				this.CaretMoved(this, null);
				return;
			}
			if (point.Y > base.ClientRectangle.Bottom)
			{
				this.document.MoveCaret(CaretDirection.LineDown);
				this.document.SetSelectionToCaret(false);
				this.CaretMoved(this, null);
				return;
			}
			if (point.Y < base.ClientRectangle.Top)
			{
				this.document.MoveCaret(CaretDirection.LineUp);
				this.document.SetSelectionToCaret(false);
				this.CaretMoved(this, null);
			}
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x00048944 File Offset: 0x00046B44
		internal void CaretMoved(object sender, EventArgs e)
		{
			if (!base.IsHandleCreated || this.canvas_width < 1 || this.canvas_height < 1)
			{
				return;
			}
			this.document.MoveCaretToTextTag();
			Point caret = this.document.Caret;
			if (this.document.CaretLine.alignment == HorizontalAlignment.Left)
			{
				if (caret.X < this.document.ViewPortX)
				{
					do
					{
						if (this.hscroll.Value - this.document.ViewPortWidth / 3 >= this.hscroll.Minimum)
						{
							this.hscroll.SafeValueSet(this.hscroll.Value - this.document.ViewPortWidth / 3);
						}
						else
						{
							this.hscroll.Value = this.hscroll.Minimum;
						}
					}
					while (this.hscroll.Value > caret.X);
				}
				if (caret.X >= this.document.ViewPortWidth + this.document.ViewPortX && this.hscroll.Value != this.hscroll.Maximum)
				{
					if (caret.X - this.document.ViewPortWidth + 1 <= this.hscroll.Maximum)
					{
						if (caret.X - this.document.ViewPortWidth >= 0)
						{
							this.hscroll.SafeValueSet(caret.X - this.document.ViewPortWidth + 1);
						}
						else
						{
							this.hscroll.Value = 0;
						}
					}
					else
					{
						this.hscroll.Value = this.hscroll.Maximum;
					}
				}
			}
			else
			{
				HorizontalAlignment horizontalAlignment = this.document.CaretLine.alignment;
			}
			if (this.Text.Length > 0)
			{
				this.RaiseSelectionChanged();
			}
			if (!this.document.multiline)
			{
				return;
			}
			int num = this.document.CaretLine.Height + 1;
			if (caret.Y < this.document.ViewPortY)
			{
				this.vscroll.SafeValueSet(caret.Y);
			}
			if (caret.Y + num > this.document.ViewPortY + this.canvas_height)
			{
				this.vscroll.Value = Math.Min(this.vscroll.Maximum, caret.Y - this.canvas_height + num);
			}
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x00048B98 File Offset: 0x00046D98
		internal bool Paste(IDataObject clip, DataFormats.Format format, bool obey_length)
		{
			if (clip == null)
			{
				return false;
			}
			if (format == null)
			{
				if (this is RichTextBox && clip.GetDataPresent(DataFormats.Rtf))
				{
					format = DataFormats.GetFormat(DataFormats.Rtf);
				}
				else if (this is RichTextBox && clip.GetDataPresent(DataFormats.Bitmap))
				{
					format = DataFormats.GetFormat(DataFormats.Bitmap);
				}
				else if (clip.GetDataPresent(DataFormats.UnicodeText))
				{
					format = DataFormats.GetFormat(DataFormats.UnicodeText);
				}
				else
				{
					if (!clip.GetDataPresent(DataFormats.Text))
					{
						return false;
					}
					format = DataFormats.GetFormat(DataFormats.Text);
				}
			}
			else
			{
				if (format.Name == DataFormats.Rtf && !(this is RichTextBox))
				{
					return false;
				}
				if (!clip.GetDataPresent(format.Name))
				{
					return false;
				}
			}
			if (format.Name == DataFormats.Rtf)
			{
				this.document.undo.BeginUserAction(Locale.GetText("Paste"));
				((RichTextBox)this).SelectedRtf = (string)clip.GetData(DataFormats.Rtf);
				this.document.undo.EndUserAction();
				this.Modified = true;
				return true;
			}
			if (format.Name == DataFormats.Bitmap)
			{
				this.document.undo.BeginUserAction(Locale.GetText("Paste"));
				this.document.MoveCaret(CaretDirection.CharForward);
				this.document.undo.EndUserAction();
				return true;
			}
			string text;
			if (format.Name == DataFormats.UnicodeText)
			{
				text = (string)clip.GetData(DataFormats.UnicodeText);
			}
			else
			{
				if (!(format.Name == DataFormats.Text))
				{
					return false;
				}
				text = (string)clip.GetData(DataFormats.Text);
			}
			if (!obey_length)
			{
				this.document.undo.BeginUserAction(Locale.GetText("Paste"));
				this.SelectedText = text;
				this.document.undo.EndUserAction();
			}
			else if (text.Length + (this.document.Length - this.SelectedText.Length) < this.max_length)
			{
				this.document.undo.BeginUserAction(Locale.GetText("Paste"));
				this.SelectedText = text;
				this.document.undo.EndUserAction();
			}
			else if (this.document.Length - this.SelectedText.Length < this.max_length)
			{
				this.document.undo.BeginUserAction(Locale.GetText("Paste"));
				this.SelectedText = text.Substring(0, this.max_length - (this.document.Length - this.SelectedText.Length));
				this.document.undo.EndUserAction();
			}
			this.Modified = true;
			return true;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x00009E44 File Offset: 0x00008044
		internal virtual Color ChangeBackColor(Color backColor)
		{
			return backColor;
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00006F54 File Offset: 0x00005154
		internal override bool IsInputCharInternal(char charCode)
		{
			return true;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0000493C File Offset: 0x00002B3C
		internal virtual void OnTextUpdate()
		{
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000FA5 RID: 4005 RVA: 0x00006714 File Offset: 0x00004914
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="mevent">The event data.</param>
		// Token: 0x06000FA6 RID: 4006 RVA: 0x00048E61 File Offset: 0x00047061
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
		}

		// Token: 0x04000A4E RID: 2638
		internal HorizontalAlignment alignment;

		// Token: 0x04000A4F RID: 2639
		internal bool accepts_tab;

		// Token: 0x04000A50 RID: 2640
		internal bool accepts_return;

		// Token: 0x04000A51 RID: 2641
		internal bool auto_size;

		// Token: 0x04000A52 RID: 2642
		internal bool backcolor_set;

		// Token: 0x04000A53 RID: 2643
		internal CharacterCasing character_casing;

		// Token: 0x04000A54 RID: 2644
		internal bool hide_selection;

		// Token: 0x04000A55 RID: 2645
		private int max_length;

		// Token: 0x04000A56 RID: 2646
		internal bool modified;

		// Token: 0x04000A57 RID: 2647
		internal char password_char;

		// Token: 0x04000A58 RID: 2648
		internal bool read_only;

		// Token: 0x04000A59 RID: 2649
		internal bool word_wrap;

		// Token: 0x04000A5A RID: 2650
		internal Document document;

		// Token: 0x04000A5B RID: 2651
		internal ImplicitHScrollBar hscroll;

		// Token: 0x04000A5C RID: 2652
		internal ImplicitVScrollBar vscroll;

		// Token: 0x04000A5D RID: 2653
		internal RichTextBoxScrollBars scrollbars;

		// Token: 0x04000A5E RID: 2654
		internal Timer scroll_timer;

		// Token: 0x04000A5F RID: 2655
		internal bool richtext;

		// Token: 0x04000A60 RID: 2656
		internal bool show_selection;

		// Token: 0x04000A61 RID: 2657
		internal ArrayList list_links;

		// Token: 0x04000A62 RID: 2658
		private TextBoxBase.LinkRectangle current_link;

		// Token: 0x04000A63 RID: 2659
		private bool enable_links;

		// Token: 0x04000A64 RID: 2660
		internal bool has_been_focused;

		// Token: 0x04000A65 RID: 2661
		internal int selection_length = -1;

		// Token: 0x04000A66 RID: 2662
		internal bool show_caret_w_selection;

		// Token: 0x04000A67 RID: 2663
		internal int canvas_width;

		// Token: 0x04000A68 RID: 2664
		internal int canvas_height;

		// Token: 0x04000A69 RID: 2665
		internal static int track_width = 2;

		// Token: 0x04000A6A RID: 2666
		internal static int track_border = 5;

		// Token: 0x04000A6B RID: 2667
		internal DateTime click_last;

		// Token: 0x04000A6C RID: 2668
		internal int click_point_x;

		// Token: 0x04000A6D RID: 2669
		internal int click_point_y;

		// Token: 0x04000A6E RID: 2670
		internal CaretSelection click_mode;

		// Token: 0x04000A6F RID: 2671
		internal BorderStyle actual_border_style;

		// Token: 0x04000A70 RID: 2672
		internal bool shortcuts_enabled = true;

		// Token: 0x04000A71 RID: 2673
		private static object AcceptsTabChangedEvent = new object();

		// Token: 0x04000A72 RID: 2674
		private static object AutoSizeChangedEvent = new object();

		// Token: 0x04000A73 RID: 2675
		private static object BorderStyleChangedEvent = new object();

		// Token: 0x04000A74 RID: 2676
		private static object HideSelectionChangedEvent = new object();

		// Token: 0x04000A75 RID: 2677
		private static object ModifiedChangedEvent = new object();

		// Token: 0x04000A76 RID: 2678
		private static object MultilineChangedEvent = new object();

		// Token: 0x04000A77 RID: 2679
		private static object ReadOnlyChangedEvent = new object();

		// Token: 0x04000A78 RID: 2680
		private static object HScrolledEvent = new object();

		// Token: 0x04000A79 RID: 2681
		private static object VScrolledEvent = new object();

		// Token: 0x02000194 RID: 404
		internal class LinkRectangle
		{
			// Token: 0x06000FA8 RID: 4008 RVA: 0x00048EDF File Offset: 0x000470DF
			public LinkRectangle(Rectangle rect)
			{
				this.link_tag = null;
				this.link_area_rectangle = rect;
			}

			// Token: 0x17000409 RID: 1033
			// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x00048EF5 File Offset: 0x000470F5
			public Rectangle LinkAreaRectangle
			{
				get
				{
					return this.link_area_rectangle;
				}
			}

			// Token: 0x1700040A RID: 1034
			// (set) Token: 0x06000FAA RID: 4010 RVA: 0x00048EFD File Offset: 0x000470FD
			public LineTag LinkTag
			{
				set
				{
					this.link_tag = value;
				}
			}

			// Token: 0x06000FAB RID: 4011 RVA: 0x00048F06 File Offset: 0x00047106
			public void Scroll(int x_change, int y_change)
			{
				this.link_area_rectangle.X = this.link_area_rectangle.X + x_change;
				this.link_area_rectangle.Y = this.link_area_rectangle.Y + y_change;
			}

			// Token: 0x04000A7A RID: 2682
			private Rectangle link_area_rectangle;

			// Token: 0x04000A7B RID: 2683
			private LineTag link_tag;
		}
	}
}
