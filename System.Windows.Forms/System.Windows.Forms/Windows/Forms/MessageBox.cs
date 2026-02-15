using System;
using System.Drawing;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Displays a message box that can contain text, buttons, and symbols that inform and instruct the user.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200013C RID: 316
	public class MessageBox
	{
		/// <summary>Displays a message box with specified text, caption, buttons, and icon.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <paramref name="buttons" /> parameter specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- The <paramref name="icon" /> parameter specified is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000CC1 RID: 3265 RVA: 0x00037946 File Offset: 0x00035B46
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return new MessageBox.MessageBoxForm(null, text, caption, buttons, icon).RunDialog();
		}

		/// <summary>Displays a message box in front of the specified object and with the specified text, caption, buttons, icon, default button, and options.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		/// <param name="owner">An implementation of <see cref="T:System.Windows.Forms.IWin32Window" /> that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the message box. </param>
		/// <param name="caption">The text to display in the title bar of the message box. </param>
		/// <param name="buttons">One of the <see cref="T:System.Windows.Forms.MessageBoxButtons" /> values that specifies which buttons to display in the message box. </param>
		/// <param name="icon">One of the <see cref="T:System.Windows.Forms.MessageBoxIcon" /> values that specifies which icon to display in the message box. </param>
		/// <param name="defaultButton">One of the <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" /> values the specifies the default button for the message box. </param>
		/// <param name="options">One of the <see cref="T:System.Windows.Forms.MessageBoxOptions" /> values that specifies which display and association options will be used for the message box. You may pass in 0 if you wish to use the defaults.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="buttons" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxButtons" />.-or- <paramref name="icon" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxIcon" />.-or- <paramref name="defaultButton" /> is not a member of <see cref="T:System.Windows.Forms.MessageBoxDefaultButton" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to display the <see cref="T:System.Windows.Forms.MessageBox" /> in a process that is not running in User Interactive mode. This is specified by the <see cref="P:System.Windows.Forms.SystemInformation.UserInteractive" /> property. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="options" /> specified both <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> and <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" />.-or- <paramref name="options" /> specified <see cref="F:System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly" /> or <see cref="F:System.Windows.Forms.MessageBoxOptions.ServiceNotification" /> and specified a value in the <paramref name="owner" /> parameter. These two options should be used only if you invoke the version of this method that does not take an <paramref name="owner" /> parameter.-or- <paramref name="buttons" /> specified an invalid combination of <see cref="T:System.Windows.Forms.MessageBoxButtons" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000CC2 RID: 3266 RVA: 0x00037957 File Offset: 0x00035B57
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options)
		{
			return new MessageBox.MessageBoxForm(owner, text, caption, buttons, icon, defaultButton, options, false).RunDialog();
		}

		// Token: 0x0200013D RID: 317
		internal class MessageBoxForm : Form
		{
			// Token: 0x06000CC3 RID: 3267 RVA: 0x00037970 File Offset: 0x00035B70
			public MessageBoxForm(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, bool displayHelpButton)
			{
				this.show_help = displayHelpButton;
				if (icon <= MessageBoxIcon.Error)
				{
					if (icon != MessageBoxIcon.None)
					{
						if (icon == MessageBoxIcon.Error)
						{
							this.icon_image = SystemIcons.Error;
							this.alert_type = AlertType.Error;
						}
					}
					else
					{
						this.icon_image = null;
						this.alert_type = AlertType.Default;
					}
				}
				else if (icon != MessageBoxIcon.Question)
				{
					if (icon != MessageBoxIcon.Exclamation)
					{
						if (icon == MessageBoxIcon.Asterisk)
						{
							this.icon_image = SystemIcons.Information;
							this.alert_type = AlertType.Information;
						}
					}
					else
					{
						this.icon_image = SystemIcons.Warning;
						this.alert_type = AlertType.Warning;
					}
				}
				else
				{
					this.icon_image = SystemIcons.Question;
					this.alert_type = AlertType.Question;
				}
				this.msgbox_text = text;
				this.msgbox_buttons = buttons;
				this.msgbox_default = MessageBoxDefaultButton.Button1;
				if (owner != null)
				{
					base.Owner = Control.FromHandle(owner.Handle).FindForm();
				}
				else if (Application.MWFThread.Current.Context != null)
				{
					base.Owner = Application.MWFThread.Current.Context.MainForm;
				}
				this.Text = caption;
				base.ControlBox = true;
				base.MinimizeBox = false;
				base.MaximizeBox = false;
				base.ShowInTaskbar = base.Owner == null;
				base.FormBorderStyle = FormBorderStyle.FixedDialog;
			}

			// Token: 0x06000CC4 RID: 3268 RVA: 0x00037A9F File Offset: 0x00035C9F
			public MessageBoxForm(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, MessageBoxOptions options, bool displayHelpButton)
				: this(owner, text, caption, buttons, icon, displayHelpButton)
			{
				this.msgbox_default = defaultButton;
			}

			// Token: 0x06000CC5 RID: 3269 RVA: 0x00037AB8 File Offset: 0x00035CB8
			public MessageBoxForm(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
				: this(owner, text, caption, buttons, icon, false)
			{
			}

			// Token: 0x1700033E RID: 830
			// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00037AC8 File Offset: 0x00035CC8
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style |= 113246208;
					if (!this.is_enabled)
					{
						createParams.Style |= 134217728;
					}
					return createParams;
				}
			}

			// Token: 0x06000CC7 RID: 3271 RVA: 0x00037B0C File Offset: 0x00035D0C
			public DialogResult RunDialog()
			{
				base.StartPosition = FormStartPosition.CenterScreen;
				if (!this.size_known)
				{
					this.InitFormsSize();
				}
				if (base.Owner != null)
				{
					base.TopMost = base.Owner.TopMost;
				}
				XplatUI.AudibleAlert(this.alert_type);
				base.ShowDialog();
				return base.DialogResult;
			}

			// Token: 0x06000CC8 RID: 3272 RVA: 0x00037B60 File Offset: 0x00035D60
			internal override void OnPaintInternal(PaintEventArgs e)
			{
				e.Graphics.DrawString(this.msgbox_text, this.Font, ThemeEngine.Current.ResPool.GetSolidBrush(Color.Black), this.text_rect);
				if (this.icon_image != null)
				{
					e.Graphics.DrawIcon(this.icon_image, 10, 10);
				}
			}

			// Token: 0x06000CC9 RID: 3273 RVA: 0x00037BBC File Offset: 0x00035DBC
			private void InitFormsSize()
			{
				int num = (int)((double)Screen.GetWorkingArea(this).Width * 0.6);
				if (num > 500)
				{
					float dpiX;
					using (Graphics graphics = base.CreateGraphics())
					{
						dpiX = graphics.DpiX;
					}
					int num2 = (int)((double)dpiX * 5.0);
					if (num2 < num)
					{
						num = num2;
					}
				}
				int num3 = 0;
				if (this.icon_image != null)
				{
					num3 = this.icon_image.Width + 10;
				}
				SizeF sizeF = TextRenderer.MeasureText(this.msgbox_text, this.Font, new Size(num - num3, int.MaxValue), TextFormatFlags.WordBreak);
				this.text_rect = default(RectangleF);
				this.text_rect.Height = sizeF.Height;
				if (this.icon_image != null)
				{
					sizeF.Width += (float)num3;
					if ((float)this.icon_image.Height > sizeF.Height)
					{
						this.text_rect.Location = new Point(this.icon_image.Width + 10 + 10, (int)((float)(this.icon_image.Height / 2) - sizeF.Height / 2f) + 10);
					}
					else
					{
						this.text_rect.Location = new Point(this.icon_image.Width + 10 + 10, 12);
					}
					if (sizeF.Height < (float)this.icon_image.Height)
					{
						sizeF.Height = (float)this.icon_image.Height;
					}
				}
				else
				{
					this.text_rect.Location = new Point(15, 10);
				}
				sizeF.Height += 20f;
				this.text_rect.Height = this.text_rect.Height + 10f;
				int num4;
				switch (this.msgbox_buttons)
				{
				case MessageBoxButtons.OK:
					num4 = 1;
					break;
				case MessageBoxButtons.OKCancel:
					num4 = 2;
					break;
				case MessageBoxButtons.AbortRetryIgnore:
					num4 = 3;
					break;
				case MessageBoxButtons.YesNoCancel:
					num4 = 3;
					break;
				case MessageBoxButtons.YesNo:
					num4 = 2;
					break;
				case MessageBoxButtons.RetryCancel:
					num4 = 2;
					break;
				default:
					num4 = 0;
					break;
				}
				if (this.show_help)
				{
					num4++;
				}
				int num5 = 91 * num4;
				Size size = new SizeF(Math.Min(Math.Max(TextRenderer.MeasureString(this.Text, new Font(Control.DefaultFont, FontStyle.Bold)).Width + 40f, sizeF.Width), (float)num), sizeF.Height).ToSize();
				if (size.Width > num5)
				{
					base.ClientSize = new Size(size.Width + 20, base.Height = size.Height + 40);
				}
				else
				{
					base.ClientSize = new Size(num5 + 20, base.Height = size.Height + 40);
				}
				this.text_rect.Width = (float)(size.Width - num3);
				this.button_left = base.ClientSize.Width / 2 - num5 / 2 + 5;
				this.AddButtons();
				this.size_known = true;
				MessageBoxDefaultButton messageBoxDefaultButton = this.msgbox_default;
				if (messageBoxDefaultButton != MessageBoxDefaultButton.Button2)
				{
					if (messageBoxDefaultButton != MessageBoxDefaultButton.Button3)
					{
						return;
					}
					if (this.buttons[2] != null)
					{
						base.ActiveControl = this.buttons[2];
					}
				}
				else if (this.buttons[1] != null)
				{
					base.ActiveControl = this.buttons[1];
					return;
				}
			}

			// Token: 0x06000CCA RID: 3274 RVA: 0x00037F34 File Offset: 0x00036134
			protected override bool ProcessDialogKey(Keys keyData)
			{
				if (keyData == Keys.Escape)
				{
					this.CancelClick(this, null);
					return true;
				}
				if ((keyData & Keys.Modifiers) == Keys.Control && ((keyData & Keys.KeyCode) == Keys.C || (keyData & Keys.KeyCode) == Keys.Insert))
				{
					this.Copy();
				}
				return base.ProcessDialogKey(keyData);
			}

			// Token: 0x06000CCB RID: 3275 RVA: 0x00037F84 File Offset: 0x00036184
			protected override bool ProcessDialogChar(char charCode)
			{
				if ((charCode == 'N' || charCode == 'n') && base.CancelButton != null && (base.CancelButton as Button).Text == "No")
				{
					base.CancelButton.PerformClick();
				}
				else if ((charCode == 'Y' || charCode == 'y') && (base.AcceptButton as Button).Text == "Yes")
				{
					base.AcceptButton.PerformClick();
				}
				else if ((charCode == 'A' || charCode == 'a') && base.CancelButton != null && (base.CancelButton as Button).Text == "Abort")
				{
					base.CancelButton.PerformClick();
				}
				else if ((charCode == 'R' || charCode == 'r') && (base.AcceptButton as Button).Text == "Retry")
				{
					base.AcceptButton.PerformClick();
				}
				else if ((charCode == 'I' || charCode == 'i') && this.buttons.Length >= 3 && this.buttons[2].Text == "Ignore")
				{
					this.buttons[2].PerformClick();
				}
				return base.ProcessDialogChar(charCode);
			}

			// Token: 0x06000CCC RID: 3276 RVA: 0x000380B8 File Offset: 0x000362B8
			private void Copy()
			{
				string text = "---------------------------" + Environment.NewLine;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(text);
				stringBuilder.Append(this.Text).Append(Environment.NewLine);
				stringBuilder.Append(text);
				stringBuilder.Append(this.msgbox_text).Append(Environment.NewLine);
				stringBuilder.Append(text);
				foreach (Button button in this.buttons)
				{
					if (button == null)
					{
						break;
					}
					stringBuilder.Append(button.Text).Append("   ");
				}
				stringBuilder.Append(Environment.NewLine);
				stringBuilder.Append(text);
				Clipboard.SetDataObject(new DataObject(DataFormats.Text, stringBuilder.ToString()));
			}

			// Token: 0x06000CCD RID: 3277 RVA: 0x00038180 File Offset: 0x00036380
			private void AddButtons()
			{
				if (!this.buttons_placed)
				{
					switch (this.msgbox_buttons)
					{
					case MessageBoxButtons.OK:
						this.buttons[0] = this.AddOkButton(0);
						break;
					case MessageBoxButtons.OKCancel:
						this.buttons[0] = this.AddOkButton(0);
						this.buttons[1] = this.AddCancelButton(1);
						break;
					case MessageBoxButtons.AbortRetryIgnore:
						this.buttons[0] = this.AddAbortButton(0);
						this.buttons[1] = this.AddRetryButton(1);
						this.buttons[2] = this.AddIgnoreButton(2);
						break;
					case MessageBoxButtons.YesNoCancel:
						this.buttons[0] = this.AddYesButton(0);
						this.buttons[1] = this.AddNoButton(1);
						this.buttons[2] = this.AddCancelButton(2);
						break;
					case MessageBoxButtons.YesNo:
						this.buttons[0] = this.AddYesButton(0);
						this.buttons[1] = this.AddNoButton(1);
						break;
					case MessageBoxButtons.RetryCancel:
						this.buttons[0] = this.AddRetryButton(0);
						this.buttons[1] = this.AddCancelButton(1);
						break;
					}
					if (this.show_help)
					{
						for (int i = 0; i <= 3; i++)
						{
							if (this.buttons[i] == null)
							{
								this.AddHelpButton(i);
								break;
							}
						}
					}
					this.buttons_placed = true;
				}
			}

			// Token: 0x06000CCE RID: 3278 RVA: 0x000382C4 File Offset: 0x000364C4
			private Button AddButton(string text, int left, EventHandler click_event)
			{
				Button button = new Button();
				button.Text = Locale.GetText(text);
				button.Width = 86;
				button.Height = 23;
				button.Top = base.ClientSize.Height - button.Height - 10;
				button.Left = 91 * left + this.button_left;
				if (click_event != null)
				{
					button.Click += click_event;
				}
				if (text == "OK" || text == "Retry" || text == "Yes")
				{
					base.AcceptButton = button;
				}
				else if (text == "Cancel" || text == "Abort" || text == "No")
				{
					base.CancelButton = button;
				}
				base.Controls.Add(button);
				return button;
			}

			// Token: 0x06000CCF RID: 3279 RVA: 0x00038397 File Offset: 0x00036597
			private Button AddOkButton(int left)
			{
				return this.AddButton("OK", left, new EventHandler(this.OkClick));
			}

			// Token: 0x06000CD0 RID: 3280 RVA: 0x000383B1 File Offset: 0x000365B1
			private Button AddCancelButton(int left)
			{
				return this.AddButton("Cancel", left, new EventHandler(this.CancelClick));
			}

			// Token: 0x06000CD1 RID: 3281 RVA: 0x000383CB File Offset: 0x000365CB
			private Button AddAbortButton(int left)
			{
				return this.AddButton("Abort", left, new EventHandler(this.AbortClick));
			}

			// Token: 0x06000CD2 RID: 3282 RVA: 0x000383E5 File Offset: 0x000365E5
			private Button AddRetryButton(int left)
			{
				return this.AddButton("Retry", left, new EventHandler(this.RetryClick));
			}

			// Token: 0x06000CD3 RID: 3283 RVA: 0x000383FF File Offset: 0x000365FF
			private Button AddIgnoreButton(int left)
			{
				return this.AddButton("Ignore", left, new EventHandler(this.IgnoreClick));
			}

			// Token: 0x06000CD4 RID: 3284 RVA: 0x00038419 File Offset: 0x00036619
			private Button AddYesButton(int left)
			{
				return this.AddButton("Yes", left, new EventHandler(this.YesClick));
			}

			// Token: 0x06000CD5 RID: 3285 RVA: 0x00038433 File Offset: 0x00036633
			private Button AddNoButton(int left)
			{
				return this.AddButton("No", left, new EventHandler(this.NoClick));
			}

			// Token: 0x06000CD6 RID: 3286 RVA: 0x0003844D File Offset: 0x0003664D
			private Button AddHelpButton(int left)
			{
				Button button = this.AddButton("Help", left, null);
				button.Click += delegate
				{
					base.Owner.RaiseHelpRequested(new HelpEventArgs(base.Owner.Location));
				};
				return button;
			}

			// Token: 0x06000CD7 RID: 3287 RVA: 0x0003846E File Offset: 0x0003666E
			private void OkClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}

			// Token: 0x06000CD8 RID: 3288 RVA: 0x0003847D File Offset: 0x0003667D
			private void CancelClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.Cancel;
				base.Close();
			}

			// Token: 0x06000CD9 RID: 3289 RVA: 0x0003848C File Offset: 0x0003668C
			private void AbortClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.Abort;
				base.Close();
			}

			// Token: 0x06000CDA RID: 3290 RVA: 0x0003849B File Offset: 0x0003669B
			private void RetryClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.Retry;
				base.Close();
			}

			// Token: 0x06000CDB RID: 3291 RVA: 0x000384AA File Offset: 0x000366AA
			private void IgnoreClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.Ignore;
				base.Close();
			}

			// Token: 0x06000CDC RID: 3292 RVA: 0x000384B9 File Offset: 0x000366B9
			private void YesClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.Yes;
				base.Close();
			}

			// Token: 0x06000CDD RID: 3293 RVA: 0x000384C8 File Offset: 0x000366C8
			private void NoClick(object sender, EventArgs e)
			{
				base.DialogResult = DialogResult.No;
				base.Close();
			}

			// Token: 0x04000802 RID: 2050
			private string msgbox_text;

			// Token: 0x04000803 RID: 2051
			private bool size_known;

			// Token: 0x04000804 RID: 2052
			private Icon icon_image;

			// Token: 0x04000805 RID: 2053
			private RectangleF text_rect;

			// Token: 0x04000806 RID: 2054
			private MessageBoxButtons msgbox_buttons;

			// Token: 0x04000807 RID: 2055
			private MessageBoxDefaultButton msgbox_default;

			// Token: 0x04000808 RID: 2056
			private bool buttons_placed;

			// Token: 0x04000809 RID: 2057
			private int button_left;

			// Token: 0x0400080A RID: 2058
			private Button[] buttons = new Button[4];

			// Token: 0x0400080B RID: 2059
			private bool show_help;

			// Token: 0x0400080C RID: 2060
			private AlertType alert_type;
		}
	}
}
