using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows <see cref="T:System.Windows.Forms.CheckBox" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000030 RID: 48
	[DefaultProperty("Checked")]
	[DefaultEvent("CheckedChanged")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultBindingProperty("CheckState")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class CheckBox : ButtonBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.CheckBox" /> class.</summary>
		// Token: 0x060000FC RID: 252 RVA: 0x00004A6F File Offset: 0x00002C6F
		public CheckBox()
		{
			this.appearance = Appearance.Normal;
			this.auto_check = true;
			this.check_alignment = ContentAlignment.MiddleLeft;
			this.TextAlign = ContentAlignment.MiddleLeft;
			base.SetStyle(ControlStyles.StandardDoubleClick, false);
			base.SetAutoSizeMode(AutoSizeMode.GrowAndShrink);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004AA8 File Offset: 0x00002CA8
		internal override void Draw(PaintEventArgs pe)
		{
			Rectangle rectangle;
			Rectangle rectangle2;
			Rectangle rectangle3;
			ThemeEngine.Current.CalculateCheckBoxTextAndImageLayout(this, Point.Empty, out rectangle, out rectangle2, out rectangle3);
			if (base.FlatStyle != FlatStyle.System)
			{
				ThemeEngine.Current.DrawCheckBox(pe.Graphics, this, rectangle, rectangle2, rectangle3, pe.ClipRectangle);
				return;
			}
			ThemeEngine.Current.DrawCheckBox(pe.Graphics, base.ClientRectangle, this);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004B06 File Offset: 0x00002D06
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.AutoSize)
			{
				return ThemeEngine.Current.CalculateCheckBoxAutoSize(this);
			}
			return base.GetPreferredSizeCore(proposedSize);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004B23 File Offset: 0x00002D23
		internal override void HaveDoubleClick()
		{
			if (this.DoubleClick != null)
			{
				this.DoubleClick(this, EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the value that determines the appearance of a <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Appearance" /> values. The default value is <see cref="F:System.Windows.Forms.Appearance.Normal" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.Appearance" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00004B3E File Offset: 0x00002D3E
		[DefaultValue(Appearance.Normal)]
		[Localizable(true)]
		public Appearance Appearance
		{
			get
			{
				return this.appearance;
			}
		}

		/// <summary>Gets or sets the horizontal and vertical alignment of the check mark on a <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default value is MiddleLeft.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Drawing.ContentAlignment" /> enumeration values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00004B46 File Offset: 0x00002D46
		[Bindable(true)]
		[Localizable(true)]
		[DefaultValue(ContentAlignment.MiddleLeft)]
		public ContentAlignment CheckAlign
		{
			get
			{
				return this.check_alignment;
			}
		}

		/// <summary>Gets or set a value indicating whether the <see cref="T:System.Windows.Forms.CheckBox" /> is in the checked state.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.CheckBox" /> is in the checked state; otherwise, false. The default value is false.NoteIf the <see cref="P:System.Windows.Forms.CheckBox.ThreeState" /> property is set to true, the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> property will return true for either a Checked or Indeterminate<see cref="P:System.Windows.Forms.CheckBox.CheckState" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00004B4E File Offset: 0x00002D4E
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00004B5C File Offset: 0x00002D5C
		[Bindable(true)]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue(false)]
		[SettingsBindable(true)]
		public bool Checked
		{
			get
			{
				return this.check_state != CheckState.Unchecked;
			}
			set
			{
				if (value && this.check_state != CheckState.Checked)
				{
					this.check_state = CheckState.Checked;
					base.Invalidate();
					this.OnCheckedChanged(EventArgs.Empty);
					return;
				}
				if (!value && this.check_state != CheckState.Unchecked)
				{
					this.check_state = CheckState.Unchecked;
					base.Invalidate();
					this.OnCheckedChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the state of the <see cref="T:System.Windows.Forms.CheckBox" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.CheckState" /> enumeration values. The default value is Unchecked.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.CheckState" /> enumeration values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00004BB1 File Offset: 0x00002DB1
		// (set) Token: 0x06000105 RID: 261 RVA: 0x00004BBC File Offset: 0x00002DBC
		[DefaultValue(CheckState.Unchecked)]
		[RefreshProperties(RefreshProperties.All)]
		[Bindable(true)]
		public CheckState CheckState
		{
			get
			{
				return this.check_state;
			}
			set
			{
				if (value != this.check_state)
				{
					bool flag = this.check_state > CheckState.Unchecked;
					this.check_state = value;
					if (flag != this.check_state > CheckState.Unchecked)
					{
						this.OnCheckedChanged(EventArgs.Empty);
					}
					this.OnCheckStateChanged(EventArgs.Empty);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the alignment of the text on the <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleLeft" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004C09 File Offset: 0x00002E09
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00004C11 File Offset: 0x00002E11
		[DefaultValue(ContentAlignment.MiddleLeft)]
		[Localizable(true)]
		public override ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.CheckBox" /> will allow three check states rather than two.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.CheckBox" /> is able to display three check states; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004C1A File Offset: 0x00002E1A
		[DefaultValue(false)]
		public bool ThreeState
		{
			get
			{
				return this.three_state;
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00004030 File Offset: 0x00002230
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default size.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00004C22 File Offset: 0x00002E22
		protected override Size DefaultSize
		{
			get
			{
				return new Size(104, 24);
			}
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.CheckBox" /> control.</summary>
		/// <returns>A string that states the control type and the state of the <see cref="P:System.Windows.Forms.CheckBox.CheckState" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600010B RID: 267 RVA: 0x00004C2D File Offset: 0x00002E2D
		public override string ToString()
		{
			return base.ToString() + ", CheckState: " + (int)this.check_state;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckedChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600010C RID: 268 RVA: 0x00004C4C File Offset: 0x00002E4C
		protected virtual void OnCheckedChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CheckBox.CheckedChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.CheckBox.CheckStateChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600010D RID: 269 RVA: 0x00004C7C File Offset: 0x00002E7C
		protected virtual void OnCheckStateChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[CheckBox.CheckStateChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600010E RID: 270 RVA: 0x00004CAC File Offset: 0x00002EAC
		protected override void OnClick(EventArgs e)
		{
			if (this.auto_check)
			{
				switch (this.check_state)
				{
				case CheckState.Unchecked:
					if (this.three_state)
					{
						this.CheckState = CheckState.Indeterminate;
					}
					else
					{
						this.CheckState = CheckState.Checked;
					}
					break;
				case CheckState.Checked:
					this.CheckState = CheckState.Unchecked;
					break;
				case CheckState.Indeterminate:
					this.CheckState = CheckState.Checked;
					break;
				}
			}
			base.OnClick(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600010F RID: 271 RVA: 0x00004D0D File Offset: 0x00002F0D
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <param name="e">Contains data for the event.</param>
		// Token: 0x06000110 RID: 272 RVA: 0x00004D16 File Offset: 0x00002F16
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
		}

		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06000111 RID: 273 RVA: 0x000040BD File Offset: 0x000022BD
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
		}

		/// <summary>Processes a mnemonic character.</summary>
		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process.</param>
		// Token: 0x06000112 RID: 274 RVA: 0x00004D1F File Offset: 0x00002F1F
		protected override bool ProcessMnemonic(char charCode)
		{
			if (Control.IsMnemonic(charCode, this.Text))
			{
				base.Select();
				this.OnClick(EventArgs.Empty);
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.CheckBox.Checked" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000113 RID: 275 RVA: 0x00004D49 File Offset: 0x00002F49
		// (remove) Token: 0x06000114 RID: 276 RVA: 0x00004D5C File Offset: 0x00002F5C
		public event EventHandler CheckedChanged
		{
			add
			{
				base.Events.AddHandler(CheckBox.CheckedChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(CheckBox.CheckedChangedEvent, value);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00004D6F File Offset: 0x00002F6F
		// Note: this type is marked as 'beforefieldinit'.
		static CheckBox()
		{
			CheckBox.CheckedChangedEvent = new object();
			CheckBox.CheckStateChangedEvent = new object();
		}

		// Token: 0x04000114 RID: 276
		internal Appearance appearance;

		// Token: 0x04000115 RID: 277
		internal bool auto_check;

		// Token: 0x04000116 RID: 278
		internal ContentAlignment check_alignment;

		// Token: 0x04000117 RID: 279
		internal CheckState check_state;

		// Token: 0x04000118 RID: 280
		internal bool three_state;

		// Token: 0x04000119 RID: 281
		private static object AppearanceChangedEvent = new object();

		// Token: 0x0400011B RID: 283
		private static object CheckStateChangedEvent;

		// Token: 0x0400011C RID: 284
		[CompilerGenerated]
		private new EventHandler DoubleClick;
	}
}
