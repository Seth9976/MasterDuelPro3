using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows button control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000027 RID: 39
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.ButtonBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class Button : ButtonBase, IButtonControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Button" /> class.</summary>
		// Token: 0x060000B7 RID: 183 RVA: 0x00004004 File Offset: 0x00002204
		public Button()
		{
			this.dialog_result = DialogResult.None;
			base.SetStyle(ControlStyles.StandardDoubleClick, false);
		}

		/// <summary>Gets or sets a value that is returned to the parent form when the button is clicked.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values. The default value is None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DialogResult" /> values. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000401F File Offset: 0x0000221F
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00004027 File Offset: 0x00002227
		[DefaultValue(DialogResult.None)]
		[MWFCategory("Behavior")]
		public virtual DialogResult DialogResult
		{
			get
			{
				return this.dialog_result;
			}
			set
			{
				this.dialog_result = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.CreateParams" /> on the base class when creating a window. </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> object on the base class when creating a window.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004030 File Offset: 0x00002230
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Notifies the <see cref="T:System.Windows.Forms.Button" /> whether it is the default button so that it can adjust its appearance accordingly.</summary>
		/// <param name="value">true if the button is to have the appearance of the default button; otherwise, false. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060000BB RID: 187 RVA: 0x00004038 File Offset: 0x00002238
		public virtual void NotifyDefault(bool value)
		{
			base.IsDefault = value;
		}

		/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for a button.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060000BC RID: 188 RVA: 0x00004041 File Offset: 0x00002241
		public void PerformClick()
		{
			if (base.CanSelect)
			{
				this.OnClick(EventArgs.Empty);
			}
		}

		/// <filterpriority>2</filterpriority>
		// Token: 0x060000BD RID: 189 RVA: 0x00004056 File Offset: 0x00002256
		public override string ToString()
		{
			return base.ToString() + ", Text: " + this.Text;
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060000BE RID: 190 RVA: 0x00004070 File Offset: 0x00002270
		protected override void OnClick(EventArgs e)
		{
			if (this.dialog_result != DialogResult.None)
			{
				Form form = base.FindForm();
				if (form != null)
				{
					form.DialogResult = this.dialog_result;
				}
			}
			base.OnClick(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060000BF RID: 191 RVA: 0x000040A2 File Offset: 0x000022A2
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <param name="e">Provides information for the event.</param>
		// Token: 0x060000C0 RID: 192 RVA: 0x000040AB File Offset: 0x000022AB
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		/// <param name="e">Provides missing information for the event.</param>
		// Token: 0x060000C1 RID: 193 RVA: 0x000040B4 File Offset: 0x000022B4
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="mevent">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060000C2 RID: 194 RVA: 0x000040BD File Offset: 0x000022BD
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060000C3 RID: 195 RVA: 0x000040C6 File Offset: 0x000022C6
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
		}

		/// <summary>Processes a mnemonic character. </summary>
		/// <returns>true if the mnemonic was processed; otherwise, false.</returns>
		/// <param name="charCode">The mnemonic character entered. </param>
		// Token: 0x060000C4 RID: 196 RVA: 0x000040CF File Offset: 0x000022CF
		protected override bool ProcessMnemonic(char charCode)
		{
			if (base.UseMnemonic && Control.IsMnemonic(charCode, this.Text))
			{
				this.PerformClick();
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x060000C5 RID: 197 RVA: 0x000040F6 File Offset: 0x000022F6
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004100 File Offset: 0x00002300
		internal override void Draw(PaintEventArgs pevent)
		{
			if (base.FlatStyle == FlatStyle.System)
			{
				base.Draw(pevent);
				return;
			}
			Rectangle rectangle;
			Rectangle rectangle2;
			ThemeEngine.Current.CalculateButtonTextAndImageLayout(pevent.Graphics, this, out rectangle, out rectangle2);
			if (base.FlatStyle == FlatStyle.Standard)
			{
				ThemeEngine.Current.DrawButton(pevent.Graphics, this, rectangle, rectangle2, pevent.ClipRectangle);
				return;
			}
			if (base.FlatStyle == FlatStyle.Flat)
			{
				ThemeEngine.Current.DrawFlatButton(pevent.Graphics, this, rectangle, rectangle2, pevent.ClipRectangle);
				return;
			}
			if (base.FlatStyle == FlatStyle.Popup)
			{
				ThemeEngine.Current.DrawPopupButton(pevent.Graphics, this, rectangle, rectangle2, pevent.ClipRectangle);
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000419A File Offset: 0x0000239A
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.AutoSize)
			{
				return ThemeEngine.Current.CalculateButtonAutoSize(this);
			}
			return base.GetPreferredSizeCore(proposedSize);
		}

		// Token: 0x040000E8 RID: 232
		private DialogResult dialog_result;
	}
}
