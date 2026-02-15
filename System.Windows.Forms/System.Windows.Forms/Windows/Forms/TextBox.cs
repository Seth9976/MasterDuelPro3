using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows text box control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200018F RID: 399
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.TextBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class TextBox : TextBoxBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TextBox" /> class.</summary>
		// Token: 0x06000EF5 RID: 3829 RVA: 0x00044710 File Offset: 0x00042910
		public TextBox()
		{
			this.scrollbars = RichTextBoxScrollBars.None;
			this.alignment = HorizontalAlignment.Left;
			base.LostFocus += this.TextBox_LostFocus;
			base.RightToLeftChanged += this.TextBox_RightToLeftChanged;
			base.MouseWheel += this.TextBox_MouseWheel;
			this.BackColor = ThemeEngine.Current.ColorControl;
			this.ForeColor = ThemeEngine.Current.ColorControlText;
			this.backcolor_set = false;
			base.SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, false);
			base.SetStyle(ControlStyles.FixedHeight, true);
			this.undo = new MenuItem(Locale.GetText("&Undo"));
			this.cut = new MenuItem(Locale.GetText("Cu&t"));
			this.copy = new MenuItem(Locale.GetText("&Copy"));
			this.paste = new MenuItem(Locale.GetText("&Paste"));
			this.delete = new MenuItem(Locale.GetText("&Delete"));
			this.select_all = new MenuItem(Locale.GetText("Select &All"));
			this.menu = new ContextMenu(new MenuItem[]
			{
				this.undo,
				new MenuItem("-"),
				this.cut,
				this.copy,
				this.paste,
				this.delete,
				new MenuItem("-"),
				this.select_all
			});
			this.ContextMenu = this.menu;
			this.menu.Popup += this.menu_Popup;
			this.undo.Click += this.undo_Click;
			this.cut.Click += this.cut_Click;
			this.copy.Click += this.copy_Click;
			this.paste.Click += this.paste_Click;
			this.delete.Click += this.delete_Click;
			this.select_all.Click += this.select_all_Click;
			this.document.multiline = false;
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0004494D File Offset: 0x00042B4D
		private void TextBox_RightToLeftChanged(object sender, EventArgs e)
		{
			this.UpdateAlignment();
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x00044955 File Offset: 0x00042B55
		private void TextBox_LostFocus(object sender, EventArgs e)
		{
			if (this.hide_selection)
			{
				this.document.InvalidateSelectionArea();
			}
			if (this.auto_complete_listbox != null && this.auto_complete_listbox.Visible)
			{
				this.auto_complete_listbox.HideListBox(false);
			}
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0004498C File Offset: 0x00042B8C
		private void TextBox_MouseWheel(object o, MouseEventArgs args)
		{
			if (this.auto_complete_listbox == null || !this.auto_complete_listbox.Visible)
			{
				return;
			}
			int num = args.Delta / 120;
			this.auto_complete_listbox.Scroll(-num);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000449C6 File Offset: 0x00042BC6
		private void ProcessAutoCompleteInput(ref Message m, bool deleting_chars)
		{
			base.WndProc(ref m);
			this.auto_complete_original_text = this.Text;
			this.ShowAutoCompleteListBox(deleting_chars);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000449E4 File Offset: 0x00042BE4
		private void ShowAutoCompleteListBox(bool deleting_chars)
		{
			IList list;
			if (this.auto_complete_cb_source != null)
			{
				IList items = this.auto_complete_cb_source.Items;
				list = items;
			}
			else
			{
				IList items = this.auto_complete_custom_source;
				list = items;
			}
			IList list2 = list;
			bool flag = this.auto_complete_mode == AutoCompleteMode.Append || this.auto_complete_mode == AutoCompleteMode.SuggestAppend;
			bool flag2 = this.auto_complete_mode == AutoCompleteMode.Suggest || this.auto_complete_mode == AutoCompleteMode.SuggestAppend;
			if (this.Text.Length == 0)
			{
				if (this.auto_complete_listbox != null)
				{
					this.auto_complete_listbox.HideListBox(false);
				}
				return;
			}
			if (this.auto_complete_matches == null)
			{
				this.auto_complete_matches = new List<string>();
			}
			string text = this.Text;
			this.auto_complete_matches.Clear();
			for (int i = 0; i < list2.Count; i++)
			{
				string text2 = ((this.auto_complete_cb_source == null) ? this.auto_complete_custom_source[i] : this.auto_complete_cb_source.GetItemText(this.auto_complete_cb_source.Items[i]));
				if (text2.StartsWith(text, StringComparison.CurrentCultureIgnoreCase))
				{
					this.auto_complete_matches.Add(text2);
				}
			}
			this.auto_complete_matches.Sort();
			if (this.auto_complete_matches.Count == 0 || (this.auto_complete_matches.Count == 1 && this.auto_complete_matches[0].Equals(text, StringComparison.CurrentCultureIgnoreCase)))
			{
				if (this.auto_complete_listbox != null && this.auto_complete_listbox.Visible)
				{
					this.auto_complete_listbox.HideListBox(false);
				}
				return;
			}
			this.auto_complete_selected_index = (flag2 ? (-1) : 0);
			if (flag2)
			{
				if (this.auto_complete_listbox == null)
				{
					this.auto_complete_listbox = new TextBox.AutoCompleteListBox(this);
				}
				this.auto_complete_listbox.Location = base.PointToScreen(new Point(0, base.Height));
				this.auto_complete_listbox.ShowListBox();
			}
			if (flag && !deleting_chars)
			{
				this.AppendAutoCompleteMatch(0);
			}
			this.document.MoveCaret(CaretDirection.End);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x00044BAD File Offset: 0x00042DAD
		internal void HideAutoCompleteList()
		{
			if (this.auto_complete_listbox != null)
			{
				this.auto_complete_listbox.HideListBox(false);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x00044BC4 File Offset: 0x00042DC4
		internal bool IsAutoCompleteAvailable
		{
			get
			{
				if (this.auto_complete_source == AutoCompleteSource.None || this.auto_complete_mode == AutoCompleteMode.None)
				{
					return false;
				}
				if (this.auto_complete_source != AutoCompleteSource.CustomSource)
				{
					return false;
				}
				IList list;
				if (this.auto_complete_cb_source != null)
				{
					IList items = this.auto_complete_cb_source.Items;
					list = items;
				}
				else
				{
					IList items = this.auto_complete_custom_source;
					list = items;
				}
				IList list2 = list;
				return list2 != null && list2.Count != 0;
			}
		}

		// Token: 0x170003DF RID: 991
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x00044C22 File Offset: 0x00042E22
		internal ComboBox AutoCompleteInternalSource
		{
			set
			{
				this.auto_complete_cb_source = value;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x00044C2C File Offset: 0x00042E2C
		internal bool CanNavigateAutoCompleteList
		{
			get
			{
				if (this.auto_complete_mode == AutoCompleteMode.None)
				{
					return false;
				}
				if (this.auto_complete_matches == null || this.auto_complete_matches.Count == 0)
				{
					return false;
				}
				bool flag = this.auto_complete_listbox != null && this.auto_complete_listbox.Visible;
				return this.auto_complete_mode != AutoCompleteMode.Suggest || flag;
			}
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x00044C80 File Offset: 0x00042E80
		private bool NavigateAutoCompleteList(Keys key)
		{
			if (this.auto_complete_matches == null || this.auto_complete_matches.Count == 0)
			{
				return false;
			}
			bool flag = this.auto_complete_listbox != null && this.auto_complete_listbox.Visible;
			if (!flag && this.auto_complete_mode == AutoCompleteMode.Suggest)
			{
				return false;
			}
			int num = this.auto_complete_selected_index;
			if (key <= Keys.PageDown)
			{
				if (key != Keys.PageUp)
				{
					if (key != Keys.PageDown)
					{
						goto IL_0136;
					}
					if (this.auto_complete_mode == AutoCompleteMode.Append || !flag)
					{
						goto IL_0087;
					}
					if (num == -1)
					{
						num = 0;
						goto IL_0136;
					}
					if (num == this.auto_complete_matches.Count - 1)
					{
						num = -1;
						goto IL_0136;
					}
					num += this.auto_complete_listbox.page_size - 1;
					if (num >= this.auto_complete_matches.Count)
					{
						num = this.auto_complete_matches.Count - 1;
						goto IL_0136;
					}
					goto IL_0136;
				}
				else if (this.auto_complete_mode != AutoCompleteMode.Append && flag)
				{
					if (num == -1)
					{
						num = this.auto_complete_matches.Count - 1;
						goto IL_0136;
					}
					if (num == 0)
					{
						num = -1;
						goto IL_0136;
					}
					num -= this.auto_complete_listbox.page_size - 1;
					if (num < 0)
					{
						num = 0;
						goto IL_0136;
					}
					goto IL_0136;
				}
			}
			else if (key != Keys.Up)
			{
				if (key != Keys.Down)
				{
					goto IL_0136;
				}
				goto IL_0087;
			}
			num--;
			if (num < -1)
			{
				num = this.auto_complete_matches.Count - 1;
				goto IL_0136;
			}
			goto IL_0136;
			IL_0087:
			num++;
			if (num >= this.auto_complete_matches.Count)
			{
				num = -1;
			}
			IL_0136:
			if ((this.auto_complete_mode == AutoCompleteMode.Suggest || this.auto_complete_mode == AutoCompleteMode.SuggestAppend) && flag)
			{
				this.Text = ((num == -1) ? this.auto_complete_original_text : this.auto_complete_matches[num]);
				this.auto_complete_listbox.HighlightedIndex = num;
			}
			else
			{
				this.AppendAutoCompleteMatch((num < 0) ? 0 : num);
			}
			this.auto_complete_selected_index = num;
			this.document.MoveCaret(CaretDirection.End);
			return true;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x00044E2C File Offset: 0x0004302C
		private void AppendAutoCompleteMatch(int index)
		{
			this.Text = this.auto_complete_original_text + this.auto_complete_matches[index].Substring(this.auto_complete_original_text.Length);
			base.SelectionStart = this.auto_complete_original_text.Length;
			this.SelectionLength = this.auto_complete_matches[index].Length - this.auto_complete_original_text.Length;
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0000493C File Offset: 0x00002B3C
		internal virtual void OnAutoCompleteValueSelected(EventArgs args)
		{
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x00044E9C File Offset: 0x0004309C
		private void UpdateAlignment()
		{
			HorizontalAlignment horizontalAlignment = this.alignment;
			if (base.GetInheritedRtoL() == RightToLeft.Yes)
			{
				if (horizontalAlignment == HorizontalAlignment.Left)
				{
					horizontalAlignment = HorizontalAlignment.Right;
				}
				else if (horizontalAlignment == HorizontalAlignment.Right)
				{
					horizontalAlignment = HorizontalAlignment.Left;
				}
			}
			this.document.alignment = horizontalAlignment;
			if (this.Multiline)
			{
				if (this.alignment != HorizontalAlignment.Left)
				{
					this.document.Wrap = true;
				}
				else
				{
					this.document.Wrap = this.word_wrap;
				}
			}
			for (int i = 1; i <= this.document.Lines; i++)
			{
				this.document.GetLine(i).Alignment = horizontalAlignment;
			}
			this.document.RecalculateDocument(base.CreateGraphicsInternal());
			base.Invalidate();
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x00044F41 File Offset: 0x00043141
		internal override Color ChangeBackColor(Color backColor)
		{
			if (backColor == Color.Empty)
			{
				if (!base.ReadOnly)
				{
					backColor = SystemColors.Window;
				}
				this.backcolor_set = false;
			}
			return backColor;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x00044F67 File Offset: 0x00043167
		private void OnAutoCompleteCustomSourceChanged(object sender, CollectionChangeEventArgs e)
		{
			AutoCompleteSource autoCompleteSource = this.auto_complete_source;
		}

		/// <summary>Gets or sets a custom <see cref="T:System.Collections.Specialized.StringCollection" /> to use when the <see cref="P:System.Windows.Forms.TextBox.AutoCompleteSource" /> property is set to CustomSource.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> to use with <see cref="P:System.Windows.Forms.TextBox.AutoCompleteSource" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003E1 RID: 993
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x00044F74 File Offset: 0x00043174
		[MonoTODO("AutoCompletion algorithm is currently not implemented.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public AutoCompleteStringCollection AutoCompleteCustomSource
		{
			set
			{
				if (this.auto_complete_custom_source == value)
				{
					return;
				}
				if (this.auto_complete_custom_source != null)
				{
					this.auto_complete_custom_source.CollectionChanged -= this.OnAutoCompleteCustomSourceChanged;
				}
				this.auto_complete_custom_source = value;
				if (this.auto_complete_custom_source != null)
				{
					this.auto_complete_custom_source.CollectionChanged += this.OnAutoCompleteCustomSourceChanged;
				}
			}
		}

		/// <summary>Gets or sets an option that controls how automatic completion works for the <see cref="T:System.Windows.Forms.TextBox" />.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.AutoCompleteMode" />. The following are the values. <see cref="F:System.Windows.Forms.AutoCompleteMode.Append" />Appends the remainder of the most likely candidate string to the existing characters, highlighting the appended characters.<see cref="F:System.Windows.Forms.AutoCompleteMode.Suggest" />Displays the auxiliary drop-down list associated with the edit control. This drop-down is populated with one or more suggested completion strings.<see cref="F:System.Windows.Forms.AutoCompleteMode.SuggestAppend" />Appends both Suggest and Append options.<see cref="F:System.Windows.Forms.AutoCompleteMode.None" />Disables automatic completion. This is the default.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.AutoCompleteMode" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003E2 RID: 994
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x00044FD0 File Offset: 0x000431D0
		[MonoTODO("AutoCompletion algorithm is currently not implemented.")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(AutoCompleteMode.None)]
		public AutoCompleteMode AutoCompleteMode
		{
			set
			{
				if (this.auto_complete_mode == value)
				{
					return;
				}
				if (value < AutoCompleteMode.None || value > AutoCompleteMode.SuggestAppend)
				{
					throw new InvalidEnumArgumentException(Locale.GetText("Enum argument value '{0}' is not valid for AutoCompleteMode", new object[] { value }));
				}
				this.auto_complete_mode = value;
			}
		}

		/// <summary>Gets or sets a value specifying the source of complete strings used for automatic completion.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.AutoCompleteSource" />. The options are AllSystemSources, AllUrl, FileSystem, HistoryList, RecentlyUsedList, CustomSource, and None. The default is None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified value is not one of the values of <see cref="T:System.Windows.Forms.AutoCompleteSource" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003E3 RID: 995
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x0004500C File Offset: 0x0004320C
		[MonoTODO("AutoCompletion algorithm is currently not implemented.")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(AutoCompleteSource.None)]
		[TypeConverter(typeof(TextBoxAutoCompleteSourceConverter))]
		public AutoCompleteSource AutoCompleteSource
		{
			set
			{
				if (this.auto_complete_source == value)
				{
					return;
				}
				if (!Enum.IsDefined(typeof(AutoCompleteSource), value))
				{
					throw new InvalidEnumArgumentException(Locale.GetText("Enum argument value '{0}' is not valid for AutoCompleteSource", new object[] { value }));
				}
				this.auto_complete_source = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the text in the <see cref="T:System.Windows.Forms.TextBox" /> control should appear as the default password character.</summary>
		/// <returns>true if the text in the <see cref="T:System.Windows.Forms.TextBox" /> control should appear as the default password character; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00045060 File Offset: 0x00043260
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x00045068 File Offset: 0x00043268
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.Repaint)]
		public bool UseSystemPasswordChar
		{
			get
			{
				return this.use_system_password_char;
			}
			set
			{
				if (this.use_system_password_char != value)
				{
					this.use_system_password_char = value;
					if (!this.Multiline)
					{
						this.document.PasswordChar = this.PasswordChar.ToString();
					}
					else
					{
						this.document.PasswordChar = string.Empty;
					}
					base.CalculateDocument();
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the character used to mask characters of a password in a single-line <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>The character used to mask characters entered in a single-line <see cref="T:System.Windows.Forms.TextBox" /> control. Set the value of this property to 0 (character value) if you do not want the control to mask characters as they are typed. Equals 0 (character value) by default.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x000450C4 File Offset: 0x000432C4
		[Localizable(true)]
		[DefaultValue('\0')]
		[MWFCategory("Behavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public char PasswordChar
		{
			get
			{
				if (this.use_system_password_char)
				{
					return '*';
				}
				return this.password_char;
			}
		}

		/// <summary>Gets or sets which scroll bars should appear in a multiline <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ScrollBars" /> enumeration values that indicates whether a multiline <see cref="T:System.Windows.Forms.TextBox" /> control appears with no scroll bars, a horizontal scroll bar, a vertical scroll bar, or both. The default is ScrollBars.None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003E6 RID: 998
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x000450D8 File Offset: 0x000432D8
		[DefaultValue(ScrollBars.None)]
		[Localizable(true)]
		[MWFCategory("Appearance")]
		public ScrollBars ScrollBars
		{
			set
			{
				if (!Enum.IsDefined(typeof(ScrollBars), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ScrollBars));
				}
				if (value != (ScrollBars)this.scrollbars)
				{
					this.scrollbars = (RichTextBoxScrollBars)value;
					base.CalculateScrollBars();
				}
			}
		}

		/// <summary>Gets or sets the current text in the <see cref="T:System.Windows.Forms.TextBox" />.</summary>
		/// <returns>The text displayed in the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00045128 File Offset: 0x00043328
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00045130 File Offset: 0x00043330
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

		/// <summary>Gets or sets how text is aligned in a <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.HorizontalAlignment" /> enumeration values that specifies how text is aligned in the control. The default is HorizontalAlignment.Left.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value that is not within the range of valid values for the enumeration was assigned to the property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003E8 RID: 1000
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x00045139 File Offset: 0x00043339
		[DefaultValue(HorizontalAlignment.Left)]
		[Localizable(true)]
		[MWFCategory("Appearance")]
		public HorizontalAlignment TextAlign
		{
			set
			{
				if (value != this.alignment)
				{
					this.alignment = value;
					this.UpdateAlignment();
					this.OnTextAlignChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the required creation parameters when the control handle is created.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000F0F RID: 3855 RVA: 0x0004515C File Offset: 0x0004335C
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.TextBox" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000F10 RID: 3856 RVA: 0x000046A0 File Offset: 0x000028A0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Determines whether the specified key is an input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is an input key; otherwise, false.</returns>
		/// <param name="keyData">One of the key's values.</param>
		// Token: 0x06000F11 RID: 3857 RVA: 0x00045164 File Offset: 0x00043364
		protected override bool IsInputKey(Keys keyData)
		{
			return base.IsInputKey(keyData);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000F12 RID: 3858 RVA: 0x0004516D File Offset: 0x0004336D
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (this.selection_length == -1 && !this.has_been_focused)
			{
				base.SelectAllNoScroll();
			}
			this.has_been_focused = true;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06000F13 RID: 3859 RVA: 0x00045194 File Offset: 0x00043394
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TextBox.TextAlignChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000F14 RID: 3860 RVA: 0x000451A0 File Offset: 0x000433A0
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TextBox.TextAlignChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">A Windows Message object. </param>
		// Token: 0x06000F15 RID: 3861 RVA: 0x000451D0 File Offset: 0x000433D0
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_KEYDOWN)
			{
				if (msg != Msg.WM_CHAR)
				{
					if (msg == Msg.WM_LBUTTONDOWN)
					{
						this.has_been_focused = true;
						this.FocusInternal(true);
					}
				}
				else if (this.IsAutoCompleteAvailable)
				{
					int num = m.WParam.ToInt32();
					if (num != 13 && num != 27)
					{
						this.ProcessAutoCompleteInput(ref m, num == 8);
						return;
					}
				}
			}
			else if (this.IsAutoCompleteAvailable)
			{
				Keys keys = (Keys)m.WParam.ToInt32();
				if (keys <= Keys.PageDown)
				{
					if (keys == Keys.Return)
					{
						if (this.auto_complete_listbox != null && this.auto_complete_listbox.Visible)
						{
							this.auto_complete_listbox.HideListBox(false);
						}
						base.SelectAll();
						goto IL_011C;
					}
					if (keys != Keys.Escape)
					{
						if (keys - Keys.PageUp > 1)
						{
							goto IL_011C;
						}
					}
					else
					{
						if (this.auto_complete_listbox != null && this.auto_complete_listbox.Visible)
						{
							this.auto_complete_listbox.HideListBox(false);
							goto IL_011C;
						}
						goto IL_011C;
					}
				}
				else if (keys != Keys.Up && keys != Keys.Down)
				{
					if (keys != Keys.Delete)
					{
						goto IL_011C;
					}
					this.ProcessAutoCompleteInput(ref m, true);
					return;
				}
				if (this.NavigateAutoCompleteList(keys))
				{
					m.Result = IntPtr.Zero;
					return;
				}
			}
			IL_011C:
			base.WndProc(ref m);
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00045300 File Offset: 0x00043500
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x00045320 File Offset: 0x00043520
		internal override ContextMenu ContextMenuInternal
		{
			get
			{
				ContextMenu contextMenuInternal = base.ContextMenuInternal;
				if (contextMenuInternal == this.menu)
				{
					return null;
				}
				return contextMenuInternal;
			}
			set
			{
				base.ContextMenuInternal = value;
			}
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x00045329 File Offset: 0x00043529
		internal void RestoreContextMenu()
		{
			this.ContextMenuInternal = this.menu;
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x00045338 File Offset: 0x00043538
		private void menu_Popup(object sender, EventArgs e)
		{
			if (this.SelectionLength == 0)
			{
				this.cut.Enabled = false;
				this.copy.Enabled = false;
			}
			else
			{
				this.cut.Enabled = true;
				this.copy.Enabled = true;
			}
			if (this.SelectionLength == this.TextLength)
			{
				this.select_all.Enabled = false;
			}
			else
			{
				this.select_all.Enabled = true;
			}
			if (!base.CanUndo)
			{
				this.undo.Enabled = false;
			}
			else
			{
				this.undo.Enabled = true;
			}
			if (base.ReadOnly)
			{
				this.undo.Enabled = (this.cut.Enabled = (this.paste.Enabled = (this.delete.Enabled = false)));
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00045407 File Offset: 0x00043607
		private void undo_Click(object sender, EventArgs e)
		{
			base.Undo();
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0004540F File Offset: 0x0004360F
		private void cut_Click(object sender, EventArgs e)
		{
			base.Cut();
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x00045417 File Offset: 0x00043617
		private void copy_Click(object sender, EventArgs e)
		{
			base.Copy();
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0004541F File Offset: 0x0004361F
		private void paste_Click(object sender, EventArgs e)
		{
			base.Paste();
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00045427 File Offset: 0x00043627
		private void delete_Click(object sender, EventArgs e)
		{
			this.SelectedText = string.Empty;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00045434 File Offset: 0x00043634
		private void select_all_Click(object sender, EventArgs e)
		{
			base.SelectAll();
		}

		/// <summary>Gets or sets a value indicating whether this is a multiline <see cref="T:System.Windows.Forms.TextBox" /> control.</summary>
		/// <returns>true if the control is a multiline <see cref="T:System.Windows.Forms.TextBox" /> control; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0004543C File Offset: 0x0004363C
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x00045444 File Offset: 0x00043644
		public override bool Multiline
		{
			get
			{
				return base.Multiline;
			}
			set
			{
				base.Multiline = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000F22 RID: 3874 RVA: 0x0004544D File Offset: 0x0004364D
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000F23 RID: 3875 RVA: 0x00045456 File Offset: 0x00043656
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnHandleDestroyed(System.EventArgs)" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000F24 RID: 3876 RVA: 0x0004545F File Offset: 0x0004365F
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		// Token: 0x04000A31 RID: 2609
		private ContextMenu menu;

		// Token: 0x04000A32 RID: 2610
		private MenuItem undo;

		// Token: 0x04000A33 RID: 2611
		private MenuItem cut;

		// Token: 0x04000A34 RID: 2612
		private MenuItem copy;

		// Token: 0x04000A35 RID: 2613
		private MenuItem paste;

		// Token: 0x04000A36 RID: 2614
		private MenuItem delete;

		// Token: 0x04000A37 RID: 2615
		private MenuItem select_all;

		// Token: 0x04000A38 RID: 2616
		private bool use_system_password_char;

		// Token: 0x04000A39 RID: 2617
		private AutoCompleteStringCollection auto_complete_custom_source;

		// Token: 0x04000A3A RID: 2618
		private AutoCompleteMode auto_complete_mode;

		// Token: 0x04000A3B RID: 2619
		private AutoCompleteSource auto_complete_source = AutoCompleteSource.None;

		// Token: 0x04000A3C RID: 2620
		private TextBox.AutoCompleteListBox auto_complete_listbox;

		// Token: 0x04000A3D RID: 2621
		private string auto_complete_original_text;

		// Token: 0x04000A3E RID: 2622
		private int auto_complete_selected_index = -1;

		// Token: 0x04000A3F RID: 2623
		private List<string> auto_complete_matches;

		// Token: 0x04000A40 RID: 2624
		private ComboBox auto_complete_cb_source;

		// Token: 0x04000A41 RID: 2625
		private static object TextAlignChangedEvent = new object();

		// Token: 0x02000190 RID: 400
		private class AutoCompleteListBox : Control
		{
			// Token: 0x06000F26 RID: 3878 RVA: 0x00045474 File Offset: 0x00043674
			public AutoCompleteListBox(TextBox tb)
			{
				this.owner = tb;
				this.item_height = base.FontHeight + 2;
				this.vscroll = new VScrollBar();
				this.vscroll.ValueChanged += this.VScrollValueChanged;
				base.Controls.Add(this.vscroll);
				this.is_visible = false;
				base.InternalBorderStyle = BorderStyle.FixedSingle;
			}

			// Token: 0x170003EC RID: 1004
			// (get) Token: 0x06000F27 RID: 3879 RVA: 0x000454E4 File Offset: 0x000436E4
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style ^= 1073741824;
					createParams.Style ^= 268435456;
					createParams.Style |= int.MinValue;
					createParams.ExStyle |= 136;
					return createParams;
				}
			}

			// Token: 0x170003ED RID: 1005
			// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0004553F File Offset: 0x0004373F
			// (set) Token: 0x06000F29 RID: 3881 RVA: 0x00045548 File Offset: 0x00043748
			public int HighlightedIndex
			{
				get
				{
					return this.highlighted_index;
				}
				set
				{
					if (value == this.highlighted_index)
					{
						return;
					}
					if (this.highlighted_index != -1)
					{
						base.Invalidate(this.GetItemBounds(this.highlighted_index));
					}
					this.highlighted_index = value;
					if (this.highlighted_index != -1)
					{
						base.Invalidate(this.GetItemBounds(this.highlighted_index));
					}
					if (this.highlighted_index != -1)
					{
						this.EnsureVisible(this.highlighted_index);
					}
				}
			}

			// Token: 0x06000F2A RID: 3882 RVA: 0x000455B4 File Offset: 0x000437B4
			public void Scroll(int lines)
			{
				int num = this.vscroll.Maximum - this.page_size + 1;
				int num2 = this.vscroll.Value + lines;
				if (num2 > num)
				{
					num2 = num;
				}
				else if (num2 < this.vscroll.Minimum)
				{
					num2 = this.vscroll.Minimum;
				}
				this.vscroll.Value = num2;
			}

			// Token: 0x06000F2B RID: 3883 RVA: 0x00045614 File Offset: 0x00043814
			public void EnsureVisible(int index)
			{
				if (index < this.top_item)
				{
					this.vscroll.Value = index;
					return;
				}
				int num = this.vscroll.Maximum - this.page_size + 1;
				int num2 = base.Height / this.item_height;
				if (index > this.top_item + num2 - 1)
				{
					index = index - num2 + 1;
					this.vscroll.Value = ((index > num) ? num : index);
				}
			}

			// Token: 0x170003EE RID: 1006
			// (get) Token: 0x06000F2C RID: 3884 RVA: 0x00002D70 File Offset: 0x00000F70
			internal override bool ActivateOnShow
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06000F2D RID: 3885 RVA: 0x00045681 File Offset: 0x00043881
			private void VScrollValueChanged(object o, EventArgs args)
			{
				if (this.top_item == this.vscroll.Value)
				{
					return;
				}
				this.top_item = this.vscroll.Value;
				this.last_item = this.GetLastVisibleItem();
				base.Invalidate();
			}

			// Token: 0x06000F2E RID: 3886 RVA: 0x000456BC File Offset: 0x000438BC
			private int GetLastVisibleItem()
			{
				int height = base.Height;
				for (int i = this.top_item; i < this.owner.auto_complete_matches.Count; i++)
				{
					if ((i - this.top_item) * this.item_height + this.item_height >= height)
					{
						return i;
					}
				}
				return this.owner.auto_complete_matches.Count - 1;
			}

			// Token: 0x06000F2F RID: 3887 RVA: 0x00045720 File Offset: 0x00043920
			private Rectangle GetItemBounds(int index)
			{
				int num = index - this.top_item;
				Rectangle rectangle = new Rectangle(0, num * this.item_height, base.Width, this.item_height);
				if (this.vscroll.Visible)
				{
					rectangle.Width -= this.vscroll.Width;
				}
				return rectangle;
			}

			// Token: 0x06000F30 RID: 3888 RVA: 0x00045779 File Offset: 0x00043979
			private int GetItemAt(Point loc)
			{
				if (loc.Y > (this.last_item - this.top_item) * this.item_height + this.item_height)
				{
					return -1;
				}
				return loc.Y / this.item_height + this.top_item;
			}

			// Token: 0x06000F31 RID: 3889 RVA: 0x000457B8 File Offset: 0x000439B8
			private void LayoutListBox()
			{
				int num = this.owner.auto_complete_matches.Count * this.item_height;
				this.page_size = Math.Max(base.Height / this.item_height, 1);
				this.last_item = this.GetLastVisibleItem();
				if (base.Height < num)
				{
					this.vscroll.Visible = true;
					this.vscroll.Maximum = this.owner.auto_complete_matches.Count - 1;
					this.vscroll.LargeChange = this.page_size;
					this.vscroll.Location = new Point(base.Width - this.vscroll.Width, 0);
					this.vscroll.Height = base.Height - this.item_height;
				}
				else
				{
					this.vscroll.Visible = false;
				}
				this.resizer_bounds = new Rectangle(base.Width - this.item_height, base.Height - this.item_height, this.item_height, this.item_height);
			}

			// Token: 0x06000F32 RID: 3890 RVA: 0x000458BF File Offset: 0x00043ABF
			public void HideListBox(bool set_text)
			{
				if (set_text)
				{
					this.owner.Text = this.owner.auto_complete_matches[this.HighlightedIndex];
				}
				base.Capture = false;
				base.Hide();
			}

			// Token: 0x06000F33 RID: 3891 RVA: 0x000458F4 File Offset: 0x00043AF4
			public void ShowListBox()
			{
				if (!this.user_defined_size)
				{
					int num = ((this.owner.auto_complete_matches.Count > 7) ? (7 * this.item_height) : ((this.owner.auto_complete_matches.Count + 1) * this.item_height));
					base.Size = new Size(this.owner.Width, num);
				}
				else
				{
					this.LayoutListBox();
				}
				this.vscroll.Value = 0;
				this.HighlightedIndex = -1;
				base.Show();
				XplatUI.SetZOrder(base.Handle, IntPtr.Zero, true, false);
				base.Invalidate();
			}

			// Token: 0x06000F34 RID: 3892 RVA: 0x00045991 File Offset: 0x00043B91
			protected override void OnResize(EventArgs args)
			{
				base.OnResize(args);
				this.LayoutListBox();
				this.Refresh();
			}

			// Token: 0x06000F35 RID: 3893 RVA: 0x000459A6 File Offset: 0x00043BA6
			protected override void OnMouseDown(MouseEventArgs args)
			{
				base.OnMouseDown(args);
				if (!this.resizer_bounds.Contains(args.Location))
				{
					return;
				}
				this.user_defined_size = true;
				this.resizing = true;
				base.Capture = true;
			}

			// Token: 0x06000F36 RID: 3894 RVA: 0x000459D8 File Offset: 0x00043BD8
			protected override void OnMouseMove(MouseEventArgs args)
			{
				base.OnMouseMove(args);
				if (this.resizing)
				{
					Point mousePosition = Control.MousePosition;
					Point point = base.PointToScreen(Point.Empty);
					Size size = new Size(mousePosition.X - point.X, mousePosition.Y - point.Y);
					if (size.Height < this.item_height)
					{
						size.Height = this.item_height;
					}
					if (size.Width < this.item_height)
					{
						size.Width = this.item_height;
					}
					base.Size = size;
					return;
				}
				this.Cursor = (this.resizer_bounds.Contains(args.Location) ? Cursors.SizeNWSE : Cursors.Default);
				int itemAt = this.GetItemAt(args.Location);
				if (itemAt != -1)
				{
					this.HighlightedIndex = itemAt;
				}
			}

			// Token: 0x06000F37 RID: 3895 RVA: 0x00045AA8 File Offset: 0x00043CA8
			protected override void OnMouseUp(MouseEventArgs args)
			{
				base.OnMouseUp(args);
				if (this.GetItemAt(args.Location) != -1 && !this.resizing)
				{
					this.HideListBox(true);
				}
				this.owner.OnAutoCompleteValueSelected(EventArgs.Empty);
				this.resizing = false;
				base.Capture = false;
			}

			// Token: 0x06000F38 RID: 3896 RVA: 0x00045AF8 File Offset: 0x00043CF8
			internal override void OnPaintInternal(PaintEventArgs args)
			{
				Graphics graphics = args.Graphics;
				Brush solidBrush = ThemeEngine.Current.ResPool.GetSolidBrush(this.ForeColor);
				int highlightedIndex = this.HighlightedIndex;
				int num = 0;
				int lastVisibleItem = this.GetLastVisibleItem();
				for (int i = this.top_item; i <= lastVisibleItem; i++)
				{
					Rectangle itemBounds = this.GetItemBounds(i);
					if (itemBounds.IntersectsWith(args.ClipRectangle))
					{
						if (i == highlightedIndex)
						{
							graphics.FillRectangle(SystemBrushes.Highlight, itemBounds);
							graphics.DrawString(this.owner.auto_complete_matches[i], this.Font, SystemBrushes.HighlightText, itemBounds);
						}
						else
						{
							graphics.DrawString(this.owner.auto_complete_matches[i], this.Font, solidBrush, itemBounds);
						}
						num += this.item_height;
					}
				}
				ThemeEngine.Current.CPDrawSizeGrip(graphics, SystemColors.Control, this.resizer_bounds);
			}

			// Token: 0x04000A42 RID: 2626
			private TextBox owner;

			// Token: 0x04000A43 RID: 2627
			private VScrollBar vscroll;

			// Token: 0x04000A44 RID: 2628
			private int top_item;

			// Token: 0x04000A45 RID: 2629
			private int last_item;

			// Token: 0x04000A46 RID: 2630
			internal int page_size;

			// Token: 0x04000A47 RID: 2631
			private int item_height;

			// Token: 0x04000A48 RID: 2632
			private int highlighted_index = -1;

			// Token: 0x04000A49 RID: 2633
			private bool user_defined_size;

			// Token: 0x04000A4A RID: 2634
			private bool resizing;

			// Token: 0x04000A4B RID: 2635
			private Rectangle resizer_bounds;
		}
	}
}
