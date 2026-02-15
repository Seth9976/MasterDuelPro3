using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;

namespace System.Windows.Forms
{
	/// <summary>Displays a dialog box from which the user can select a file.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000080 RID: 128
	[DefaultProperty("FileName")]
	[DefaultEvent("FileOk")]
	public abstract class FileDialog : CommonDialog
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x000137E0 File Offset: 0x000119E0
		internal FileDialog()
		{
			this.form = new CommonDialog.DialogForm(this);
			this.vfs = new MWFVFS();
			Size empty = Size.Empty;
			Point empty2 = Point.Empty;
			object value = MWFConfig.GetValue("FileDialog", "Width");
			object value2 = MWFConfig.GetValue("FileDialog", "Height");
			if (value2 != null && value != null)
			{
				empty = new Size((int)value, (int)value2);
			}
			object value3 = MWFConfig.GetValue("FileDialog", "X");
			object value4 = MWFConfig.GetValue("FileDialog", "Y");
			if (value3 != null && value4 != null)
			{
				empty2 = new Point((int)value3, (int)value4);
			}
			this.configFileNames = (string[])MWFConfig.GetValue("FileDialog", "FileNames");
			this.fileTypeComboBox = new ComboBox();
			this.backToolBarButton = new ToolBarButton();
			this.newdirToolBarButton = new ToolBarButton();
			this.searchSaveLabel = new Label();
			this.mwfFileView = new MWFFileView(this.vfs);
			this.fileNameLabel = new Label();
			this.fileNameComboBox = new ComboBox();
			this.dirComboBox = new DirComboBox(this.vfs);
			this.smallButtonToolBar = new ToolBar();
			this.menueToolBarButton = new ToolBarButton();
			this.fileTypeLabel = new Label();
			this.openSaveButton = new Button();
			this.helpButton = new Button();
			this.popupButtonPanel = new PopupButtonPanel();
			this.upToolBarButton = new ToolBarButton();
			this.cancelButton = new Button();
			this.form.CancelButton = this.cancelButton;
			this.imageListTopToolbar = new ImageList();
			this.menueToolBarButtonContextMenu = new ContextMenu();
			this.readonlyCheckBox = new CheckBox();
			this.form.SuspendLayout();
			this.imageListTopToolbar.ColorDepth = ColorDepth.Depth32Bit;
			this.imageListTopToolbar.ImageSize = new Size(16, 16);
			this.imageListTopToolbar.Images.Add(ResourceImageLoader.Get("go-previous.png"));
			this.imageListTopToolbar.Images.Add(ResourceImageLoader.Get("go-top.png"));
			this.imageListTopToolbar.Images.Add(ResourceImageLoader.Get("folder-new.png"));
			this.imageListTopToolbar.Images.Add(ResourceImageLoader.Get("preferences-system-windows.png"));
			this.imageListTopToolbar.TransparentColor = Color.Transparent;
			this.searchSaveLabel.FlatStyle = FlatStyle.System;
			this.searchSaveLabel.Location = new Point(6, 6);
			this.searchSaveLabel.Size = new Size(86, 22);
			this.searchSaveLabel.TextAlign = ContentAlignment.MiddleRight;
			this.dirComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			this.dirComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.dirComboBox.Location = new Point(99, 6);
			this.dirComboBox.Size = new Size(261, 22);
			this.dirComboBox.TabIndex = 7;
			this.smallButtonToolBar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			this.smallButtonToolBar.Appearance = ToolBarAppearance.Flat;
			this.smallButtonToolBar.AutoSize = false;
			this.smallButtonToolBar.Buttons.AddRange(new ToolBarButton[] { this.backToolBarButton, this.upToolBarButton, this.newdirToolBarButton, this.menueToolBarButton });
			this.smallButtonToolBar.ButtonSize = new Size(24, 24);
			this.smallButtonToolBar.Divider = false;
			this.smallButtonToolBar.Dock = DockStyle.None;
			this.smallButtonToolBar.DropDownArrows = true;
			this.smallButtonToolBar.ImageList = this.imageListTopToolbar;
			this.smallButtonToolBar.Location = new Point(372, 6);
			this.smallButtonToolBar.ShowToolTips = true;
			this.smallButtonToolBar.Size = new Size(140, 28);
			this.smallButtonToolBar.TabIndex = 8;
			this.smallButtonToolBar.TextAlign = ToolBarTextAlign.Right;
			this.popupButtonPanel.Dock = DockStyle.None;
			this.popupButtonPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			this.popupButtonPanel.Location = new Point(6, 35);
			this.popupButtonPanel.Size = new Size(89, 338);
			this.popupButtonPanel.TabIndex = 9;
			this.mwfFileView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.mwfFileView.Location = new Point(99, 35);
			this.mwfFileView.Size = new Size(450, 283);
			this.mwfFileView.MultiSelect = false;
			this.mwfFileView.TabIndex = 10;
			this.mwfFileView.RegisterSender(this.dirComboBox);
			this.mwfFileView.RegisterSender(this.popupButtonPanel);
			this.fileNameLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.fileNameLabel.FlatStyle = FlatStyle.System;
			this.fileNameLabel.Location = new Point(101, 326);
			this.fileNameLabel.Size = new Size(70, 21);
			this.fileNameLabel.Text = Locale.GetText("File name:");
			this.fileNameLabel.TextAlign = ContentAlignment.MiddleLeft;
			this.fileNameComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.fileNameComboBox.Location = new Point(195, 326);
			this.fileNameComboBox.Size = new Size(246, 22);
			this.fileNameComboBox.TabIndex = 1;
			this.fileNameComboBox.MaxDropDownItems = FileDialog.MaxFileNameItems;
			this.fileNameComboBox.RestoreContextMenu();
			this.UpdateRecentFiles();
			this.fileTypeLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			this.fileTypeLabel.FlatStyle = FlatStyle.System;
			this.fileTypeLabel.Location = new Point(101, 355);
			this.fileTypeLabel.Size = new Size(90, 21);
			this.fileTypeLabel.Text = Locale.GetText("Files of type:");
			this.fileTypeLabel.TextAlign = ContentAlignment.MiddleLeft;
			this.fileTypeComboBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.fileTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			this.fileTypeComboBox.Location = new Point(195, 355);
			this.fileTypeComboBox.Size = new Size(246, 22);
			this.fileTypeComboBox.TabIndex = 2;
			this.backToolBarButton.ImageIndex = 0;
			this.backToolBarButton.Enabled = false;
			this.backToolBarButton.Style = ToolBarButtonStyle.PushButton;
			this.mwfFileView.AddControlToEnableDisableByDirStack(this.backToolBarButton);
			this.upToolBarButton.ImageIndex = 1;
			this.upToolBarButton.Style = ToolBarButtonStyle.PushButton;
			this.mwfFileView.SetFolderUpToolBarButton(this.upToolBarButton);
			this.newdirToolBarButton.ImageIndex = 2;
			this.newdirToolBarButton.Style = ToolBarButtonStyle.PushButton;
			this.menueToolBarButton.ImageIndex = 3;
			this.menueToolBarButton.DropDownMenu = this.menueToolBarButtonContextMenu;
			this.menueToolBarButton.Style = ToolBarButtonStyle.DropDownButton;
			this.menueToolBarButtonContextMenu.MenuItems.AddRange(this.mwfFileView.ViewMenuItems);
			this.openSaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.openSaveButton.FlatStyle = FlatStyle.System;
			this.openSaveButton.Location = new Point(474, 326);
			this.openSaveButton.Size = new Size(75, 23);
			this.openSaveButton.TabIndex = 4;
			this.openSaveButton.FlatStyle = FlatStyle.System;
			this.cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.cancelButton.FlatStyle = FlatStyle.System;
			this.cancelButton.Location = new Point(474, 353);
			this.cancelButton.Size = new Size(75, 23);
			this.cancelButton.TabIndex = 5;
			this.cancelButton.Text = Locale.GetText("Cancel");
			this.cancelButton.FlatStyle = FlatStyle.System;
			this.helpButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			this.helpButton.FlatStyle = FlatStyle.System;
			this.helpButton.Location = new Point(474, 353);
			this.helpButton.Size = new Size(75, 23);
			this.helpButton.TabIndex = 6;
			this.helpButton.Text = Locale.GetText("Help");
			this.helpButton.FlatStyle = FlatStyle.System;
			this.helpButton.Visible = false;
			this.readonlyCheckBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			this.readonlyCheckBox.Text = Locale.GetText("Open Readonly");
			this.readonlyCheckBox.Location = new Point(195, 350);
			this.readonlyCheckBox.Size = new Size(245, 21);
			this.readonlyCheckBox.TabIndex = 3;
			this.readonlyCheckBox.FlatStyle = FlatStyle.System;
			this.readonlyCheckBox.Visible = false;
			this.form.SizeGripStyle = SizeGripStyle.Show;
			this.form.AcceptButton = this.openSaveButton;
			this.form.MaximizeBox = true;
			this.form.MinimizeBox = true;
			this.form.FormBorderStyle = FormBorderStyle.Sizable;
			this.form.ClientSize = new Size(555, 385);
			this.form.MinimumSize = this.form.Size;
			this.form.Controls.Add(this.smallButtonToolBar);
			this.form.Controls.Add(this.cancelButton);
			this.form.Controls.Add(this.openSaveButton);
			this.form.Controls.Add(this.mwfFileView);
			this.form.Controls.Add(this.fileTypeLabel);
			this.form.Controls.Add(this.fileNameLabel);
			this.form.Controls.Add(this.fileTypeComboBox);
			this.form.Controls.Add(this.fileNameComboBox);
			this.form.Controls.Add(this.dirComboBox);
			this.form.Controls.Add(this.searchSaveLabel);
			this.form.Controls.Add(this.popupButtonPanel);
			this.form.Controls.Add(this.helpButton);
			this.form.Controls.Add(this.readonlyCheckBox);
			this.form.ResumeLayout(true);
			if (empty != Size.Empty)
			{
				this.form.ClientSize = empty;
			}
			if (empty2 != Point.Empty)
			{
				this.form.Location = empty2;
			}
			this.openSaveButton.Click += this.OnClickOpenSaveButton;
			this.cancelButton.Click += this.OnClickCancelButton;
			this.helpButton.Click += this.OnClickHelpButton;
			this.smallButtonToolBar.ButtonClick += this.OnClickSmallButtonToolBar;
			this.fileTypeComboBox.SelectedIndexChanged += this.OnSelectedIndexChangedFileTypeComboBox;
			this.mwfFileView.SelectedFileChanged += this.OnSelectedFileChangedFileView;
			this.mwfFileView.ForceDialogEnd += this.OnForceDialogEndFileView;
			this.mwfFileView.SelectedFilesChanged += this.OnSelectedFilesChangedFileView;
			this.mwfFileView.ColumnClick += this.OnColumnClickFileView;
			this.dirComboBox.DirectoryChanged += this.OnDirectoryChangedDirComboBox;
			this.popupButtonPanel.DirectoryChanged += this.OnDirectoryChangedPopupButtonPanel;
			this.readonlyCheckBox.CheckedChanged += this.OnCheckCheckChanged;
			this.form.FormClosed += this.OnFileDialogFormClosed;
			this.custom_places = new FileDialogCustomPlacesCollection();
		}

		/// <summary>Gets or sets a value indicating whether the dialog box automatically adds an extension to a file name if the user omits the extension.</summary>
		/// <returns>true if the dialog box adds an extension to a file name if the user omits the extension; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00014407 File Offset: 0x00012607
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x0001440F File Offset: 0x0001260F
		[DefaultValue(true)]
		public bool AddExtension
		{
			get
			{
				return this.addExtension;
			}
			set
			{
				this.addExtension = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.</summary>
		/// <returns>true if the dialog box displays a warning if the user specifies a file name that does not exist; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x00014418 File Offset: 0x00012618
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x00014420 File Offset: 0x00012620
		[DefaultValue(false)]
		public virtual bool CheckFileExists
		{
			get
			{
				return this.checkFileExists;
			}
			set
			{
				this.checkFileExists = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a path that does not exist.</summary>
		/// <returns>true if the dialog box displays a warning when the user specifies a path that does not exist; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00014429 File Offset: 0x00012629
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x00014431 File Offset: 0x00012631
		[DefaultValue(true)]
		public bool CheckPathExists
		{
			get
			{
				return this.checkPathExists;
			}
			set
			{
				this.checkPathExists = value;
			}
		}

		/// <summary>Gets or sets the default file name extension.</summary>
		/// <returns>The default file name extension. The returned string does not include the period. The default value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0001443A File Offset: 0x0001263A
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x00014450 File Offset: 0x00012650
		[DefaultValue("")]
		public string DefaultExt
		{
			get
			{
				if (this.defaultExt == null)
				{
					return string.Empty;
				}
				return this.defaultExt;
			}
			set
			{
				if (value != null && value.Length > 0 && value[0] == '.')
				{
					value = value.Substring(1);
				}
				this.defaultExt = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box returns the location of the file referenced by the shortcut or whether it returns the location of the shortcut (.lnk).</summary>
		/// <returns>true if the dialog box returns the location of the file referenced by the shortcut; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00014479 File Offset: 0x00012679
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x00014481 File Offset: 0x00012681
		[DefaultValue(true)]
		public bool DereferenceLinks
		{
			get
			{
				return this.dereferenceLinks;
			}
			set
			{
				this.dereferenceLinks = value;
			}
		}

		/// <summary>Gets or sets a string containing the file name selected in the file dialog box.</summary>
		/// <returns>The file name selected in the file dialog box. The default value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0001448C File Offset: 0x0001268C
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x000144EC File Offset: 0x000126EC
		[DefaultValue("")]
		public string FileName
		{
			get
			{
				if (this.fileNames == null || this.fileNames.Length == 0)
				{
					return string.Empty;
				}
				if (this.fileNames[0].Length == 0)
				{
					return string.Empty;
				}
				if (!this.checkForIllegalChars)
				{
					return this.fileNames[0];
				}
				Path.GetFullPath(this.fileNames[0]);
				return this.fileNames[0];
			}
			set
			{
				if (value != null)
				{
					this.fileNames = new string[] { value };
				}
				else
				{
					this.fileNames = new string[0];
				}
				this.checkForIllegalChars = false;
			}
		}

		/// <summary>Gets the file names of all selected files in the dialog box.</summary>
		/// <returns>An array of type <see cref="T:System.String" />, containing the file names of all selected files in the dialog box.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00014518 File Offset: 0x00012718
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string[] FileNames
		{
			get
			{
				if (this.fileNames == null || this.fileNames.Length == 0)
				{
					return new string[0];
				}
				string[] array = new string[this.fileNames.Length];
				this.fileNames.CopyTo(array, 0);
				if (!this.checkForIllegalChars)
				{
					return array;
				}
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					Path.GetFullPath(array2[i]);
				}
				return array;
			}
		}

		/// <summary>Gets or sets the current file name filter string, which determines the choices that appear in the "Save as file type" or "Files of type" box in the dialog box.</summary>
		/// <returns>The file filtering options available in the dialog box.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Filter" /> format is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0001457C File Offset: 0x0001277C
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x00014584 File Offset: 0x00012784
		[DefaultValue("")]
		[Localizable(true)]
		public string Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				if (value == null)
				{
					this.filter = "";
					if (this.fileFilter != null)
					{
						this.fileFilter.FilterArrayList.Clear();
					}
				}
				else
				{
					if (!FileFilter.CheckFilter(value))
					{
						throw new ArgumentException("The provided filter string is invalid. The filter string should contain a description of the filter, followed by the  vertical bar (|) and the filter pattern. The strings for different filtering options should also be separated by the vertical bar. Example: Text files (*.txt)|*.txt|All files (*.*)|*.*");
					}
					this.filter = value;
					this.fileFilter = new FileFilter(this.filter);
				}
				this.UpdateFilters();
			}
		}

		/// <summary>Gets or sets the index of the filter currently selected in the file dialog box.</summary>
		/// <returns>A value containing the index of the filter currently selected in the file dialog box. The default value is 1.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x000145EC File Offset: 0x000127EC
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x000145F4 File Offset: 0x000127F4
		[DefaultValue(1)]
		public int FilterIndex
		{
			get
			{
				return this.filterIndex;
			}
			set
			{
				this.filterIndex = value;
			}
		}

		/// <summary>Gets or sets the initial directory displayed by the file dialog box.</summary>
		/// <returns>The initial directory displayed by the file dialog box. The default is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x000145FD File Offset: 0x000127FD
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x00014613 File Offset: 0x00012813
		[DefaultValue("")]
		public string InitialDirectory
		{
			get
			{
				if (this.initialDirectory == null)
				{
					return string.Empty;
				}
				return this.initialDirectory;
			}
			set
			{
				this.initialDirectory = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box restores the current directory before closing.</summary>
		/// <returns>true if the dialog box restores the current directory to its original value if the user changed the directory while searching for files; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001461C File Offset: 0x0001281C
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x00014624 File Offset: 0x00012824
		[DefaultValue(false)]
		public bool RestoreDirectory
		{
			get
			{
				return this.restoreDirectory;
			}
			set
			{
				this.restoreDirectory = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Help button is displayed in the file dialog box.</summary>
		/// <returns>true if the dialog box includes a help button; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001462D File Offset: 0x0001282D
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00014635 File Offset: 0x00012835
		[DefaultValue(false)]
		public bool ShowHelp
		{
			get
			{
				return this.showHelp;
			}
			set
			{
				this.showHelp = value;
				this.ResizeAndRelocateForHelpOrReadOnly();
			}
		}

		/// <summary>Gets or sets whether the dialog box supports displaying and saving files that have multiple file name extensions.</summary>
		/// <returns>true if the dialog box supports multiple file name extensions; otherwise, false. The default is false. </returns>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00014644 File Offset: 0x00012844
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0001464C File Offset: 0x0001284C
		[DefaultValue(false)]
		public bool SupportMultiDottedExtensions
		{
			get
			{
				return this.supportMultiDottedExtensions;
			}
			set
			{
				this.supportMultiDottedExtensions = value;
			}
		}

		/// <summary>Gets or sets the file dialog box title.</summary>
		/// <returns>The file dialog box title. The default value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00014655 File Offset: 0x00012855
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0001466B File Offset: 0x0001286B
		[DefaultValue("")]
		[Localizable(true)]
		public string Title
		{
			get
			{
				if (this.title == null)
				{
					return string.Empty;
				}
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box accepts only valid Win32 file names.</summary>
		/// <returns>true if the dialog box accepts only valid Win32 file names; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014F RID: 335
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00014674 File Offset: 0x00012874
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0001467C File Offset: 0x0001287C
		[DefaultValue(true)]
		public bool ValidateNames
		{
			get
			{
				return this.validateNames;
			}
			set
			{
				this.validateNames = value;
			}
		}

		/// <summary>Resets all properties to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600053D RID: 1341 RVA: 0x00014688 File Offset: 0x00012888
		public override void Reset()
		{
			this.addExtension = true;
			this.checkFileExists = false;
			this.checkPathExists = true;
			this.DefaultExt = null;
			this.dereferenceLinks = true;
			this.FileName = null;
			this.Filter = string.Empty;
			this.FilterIndex = 1;
			this.InitialDirectory = null;
			this.restoreDirectory = false;
			this.SupportMultiDottedExtensions = false;
			this.ShowHelp = false;
			this.Title = null;
			this.validateNames = true;
			this.UpdateFilters();
		}

		/// <summary>Provides a string version of this object.</summary>
		/// <returns>A string version of this object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600053E RID: 1342 RVA: 0x00014701 File Offset: 0x00012901
		public override string ToString()
		{
			return string.Format("{0}: Title: {1}, FileName: {2}", base.ToString(), this.Title, this.FileName);
		}

		/// <summary>Occurs when the user clicks on the Open or Save button on a file dialog box.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x0600053F RID: 1343 RVA: 0x0001471F File Offset: 0x0001291F
		// (remove) Token: 0x06000540 RID: 1344 RVA: 0x00014732 File Offset: 0x00012932
		public event CancelEventHandler FileOk
		{
			add
			{
				base.Events.AddHandler(FileDialog.EventFileOk, value);
			}
			remove
			{
				base.Events.RemoveHandler(FileDialog.EventFileOk, value);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x00014745 File Offset: 0x00012945
		internal virtual string DialogTitle
		{
			get
			{
				return this.Title;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.FileDialog.FileOk" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data. </param>
		// Token: 0x06000542 RID: 1346 RVA: 0x00014750 File Offset: 0x00012950
		protected void OnFileOk(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[FileDialog.EventFileOk];
			if (cancelEventHandler != null)
			{
				cancelEventHandler(this, e);
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001477E File Offset: 0x0001297E
		private void CleanupOnClose()
		{
			this.mwfFileView.StopThumbnailCreation();
			this.WriteConfigValues();
			Mime.CleanFileCache();
			this.disable_form_closed_event = true;
		}

		/// <summary>Specifies a common dialog box.</summary>
		/// <returns>true if the file could be opened; otherwise, false.</returns>
		/// <param name="hWndOwner">A value that represents the window handle of the owner window for the common dialog box. </param>
		// Token: 0x06000544 RID: 1348 RVA: 0x000147A0 File Offset: 0x000129A0
		protected override bool RunDialog(IntPtr hWndOwner)
		{
			this.ReadConfigValues();
			this.form.Text = this.DialogTitle;
			string text;
			if (this.fileNames != null && this.fileNames.Length != 0)
			{
				text = this.fileNames[0];
			}
			else
			{
				text = string.Empty;
			}
			this.SelectFilter();
			this.form.Refresh();
			this.SetFileAndDirectory(text);
			this.fileNameComboBox.Select();
			return true;
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0001481B File Offset: 0x00012A1B
		// (set) Token: 0x06000545 RID: 1349 RVA: 0x0001480C File Offset: 0x00012A0C
		internal virtual bool ShowReadOnly
		{
			get
			{
				return this.showReadOnly;
			}
			set
			{
				this.showReadOnly = value;
				this.ResizeAndRelocateForHelpOrReadOnly();
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x00014838 File Offset: 0x00012A38
		// (set) Token: 0x06000547 RID: 1351 RVA: 0x00014823 File Offset: 0x00012A23
		internal virtual bool ReadOnlyChecked
		{
			get
			{
				return this.readOnlyChecked;
			}
			set
			{
				this.readOnlyChecked = value;
				this.readonlyCheckBox.Checked = value;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x00014855 File Offset: 0x00012A55
		// (set) Token: 0x06000549 RID: 1353 RVA: 0x00014840 File Offset: 0x00012A40
		internal bool BMultiSelect
		{
			get
			{
				return this.multiSelect;
			}
			set
			{
				this.multiSelect = value;
				this.mwfFileView.MultiSelect = value;
			}
		}

		// Token: 0x17000154 RID: 340
		// (set) Token: 0x0600054B RID: 1355 RVA: 0x0001485D File Offset: 0x00012A5D
		internal string OpenSaveButtonText
		{
			set
			{
				this.openSaveButton.Text = value;
			}
		}

		// Token: 0x17000155 RID: 341
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0001486B File Offset: 0x00012A6B
		internal string SearchSaveLabel
		{
			set
			{
				this.searchSaveLabel.Text = value;
			}
		}

		// Token: 0x17000156 RID: 342
		// (set) Token: 0x0600054D RID: 1357 RVA: 0x00014879 File Offset: 0x00012A79
		internal string FileTypeLabel
		{
			set
			{
				this.fileTypeLabel.Text = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x00014888 File Offset: 0x00012A88
		internal string CustomFilter
		{
			get
			{
				string text = this.fileNameComboBox.Text;
				if (text.IndexOfAny(this.wildcard_chars) == -1)
				{
					return null;
				}
				return text;
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000148B4 File Offset: 0x00012AB4
		private void SelectFilter()
		{
			int num = this.filterIndex - 1;
			if (this.mwfFileView.FilterArrayList == null || this.mwfFileView.FilterArrayList.Count == 0)
			{
				num = -1;
			}
			else if (num < 0 || num >= this.mwfFileView.FilterArrayList.Count)
			{
				num = 0;
			}
			this.do_not_call_OnSelectedIndexChangedFileTypeComboBox = true;
			this.fileTypeComboBox.BeginUpdate();
			this.fileTypeComboBox.SelectedIndex = num;
			this.fileTypeComboBox.EndUpdate();
			this.do_not_call_OnSelectedIndexChangedFileTypeComboBox = false;
			this.mwfFileView.FilterIndex = num + 1;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00014944 File Offset: 0x00012B44
		private void SetFileAndDirectory(string fname)
		{
			if (fname.Length == 0)
			{
				this.mwfFileView.ChangeDirectory(null, this.lastFolder);
				this.fileNameComboBox.Text = null;
				return;
			}
			if (!Path.IsPathRooted(fname))
			{
				this.mwfFileView.ChangeDirectory(null, this.lastFolder);
				this.fileNameComboBox.Text = fname;
				return;
			}
			string directoryName = Path.GetDirectoryName(fname);
			if (directoryName != null && directoryName.Length > 0 && Directory.Exists(directoryName))
			{
				this.fileNameComboBox.Text = Path.GetFileName(fname);
				this.mwfFileView.ChangeDirectory(null, directoryName);
				return;
			}
			this.fileNameComboBox.Text = fname;
			this.mwfFileView.ChangeDirectory(null, this.lastFolder);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000149FC File Offset: 0x00012BFC
		private void OnClickOpenSaveButton(object sender, EventArgs e)
		{
			this.checkForIllegalChars = true;
			if (this.fileDialogType == FileDialog.FileDialogType.OpenFileDialog)
			{
				ListView.SelectedListViewItemCollection selectedItems = this.mwfFileView.SelectedItems;
				if (selectedItems.Count > 0 && selectedItems[0] != null)
				{
					if (selectedItems.Count == 1)
					{
						FSEntry fsentry = (selectedItems[0] as FileViewListViewItem).FSEntry;
						if ((fsentry.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
						{
							this.mwfFileView.ChangeDirectory(null, fsentry.FullName, this.CustomFilter);
							return;
						}
					}
					else
					{
						foreach (object obj in selectedItems)
						{
							FSEntry fsentry2 = ((FileViewListViewItem)obj).FSEntry;
							if ((fsentry2.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
							{
								this.mwfFileView.ChangeDirectory(null, fsentry2.FullName, this.CustomFilter);
								return;
							}
						}
					}
				}
			}
			if (this.fileNameComboBox.Text.IndexOfAny(this.wildcard_chars) != -1)
			{
				this.mwfFileView.UpdateFileView(this.fileNameComboBox.Text);
				return;
			}
			ArrayList arrayList = new ArrayList();
			FileDialog.FileNamesTokenizer fileNamesTokenizer = new FileDialog.FileNamesTokenizer(this.fileNameComboBox.Text, this.multiSelect);
			fileNamesTokenizer.GetNextFile();
			while (fileNamesTokenizer.CurrentToken != FileDialog.TokenType.EOF)
			{
				string text = fileNamesTokenizer.TokenText;
				if (!Path.IsPathRooted(text))
				{
					if (this.mwfFileView.CurrentRealFolder != null)
					{
						text = Path.Combine(this.mwfFileView.CurrentRealFolder, text);
					}
					else if (this.mwfFileView.CurrentFSEntry != null)
					{
						text = this.mwfFileView.CurrentFSEntry.FullName;
					}
				}
				string text2;
				if (new FileInfo(text).Exists || this.fileDialogType == FileDialog.FileDialogType.SaveFileDialog)
				{
					text2 = text;
				}
				else
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(text);
					if (directoryInfo.Exists)
					{
						this.mwfFileView.ChangeDirectory(null, directoryInfo.FullName, this.CustomFilter);
						this.fileNameComboBox.Text = null;
						return;
					}
					text2 = text;
				}
				if (this.addExtension && Path.GetExtension(text).Length == 0)
				{
					string text3 = string.Empty;
					if (this.AddFilterExtension(text2))
					{
						text3 = this.GetExtension(text2);
					}
					if (text3.Length == 0 && this.DefaultExt.Length > 0)
					{
						text3 = "." + this.DefaultExt;
						if (this.checkFileExists && !File.Exists(text2 + text3))
						{
							text3 = string.Empty;
						}
					}
					text2 += text3;
				}
				if (this.checkFileExists && !File.Exists(text2))
				{
					MessageBox.Show(Locale.GetText("\"{0}\" does not exist. Please verify that you have entered the correct file name.", new object[] { text2 }), this.openSaveButton.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				if (this.fileDialogType == FileDialog.FileDialogType.SaveFileDialog)
				{
					if (this.overwritePrompt && File.Exists(text2) && MessageBox.Show(Locale.GetText("\"{0}\" already exists. Do you want to overwrite it?", new object[] { text2 }), this.openSaveButton.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
					{
						return;
					}
					if (this.createPrompt && !File.Exists(text2) && MessageBox.Show(Locale.GetText("\"{0}\" does not exist. Do you want to create it?", new object[] { text2 }), this.openSaveButton.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.Cancel)
					{
						return;
					}
				}
				arrayList.Add(text2);
				fileNamesTokenizer.GetNextFile();
			}
			if (arrayList.Count <= 0)
			{
				foreach (object obj2 in this.mwfFileView.SelectedItems)
				{
					FSEntry fsentry3 = ((FileViewListViewItem)obj2).FSEntry;
					if ((fsentry3.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
					{
						this.mwfFileView.ChangeDirectory(null, fsentry3.FullName, this.CustomFilter);
						return;
					}
				}
				return;
			}
			this.fileNames = new string[arrayList.Count];
			for (int i = 0; i < arrayList.Count; i++)
			{
				string text4 = (string)arrayList[i];
				this.fileNames[i] = text4;
				this.mwfFileView.WriteRecentlyUsed(text4);
				if (File.Exists(text4) && this.fileNameComboBox.Items.IndexOf(text4) == -1)
				{
					this.fileNameComboBox.Items.Insert(0, text4);
				}
			}
			while (this.fileNameComboBox.Items.Count > FileDialog.MaxFileNameItems)
			{
				this.fileNameComboBox.Items.RemoveAt(FileDialog.MaxFileNameItems);
			}
			if (this.checkPathExists && this.mwfFileView.CurrentRealFolder != null && !Directory.Exists(this.mwfFileView.CurrentRealFolder))
			{
				MessageBox.Show(Locale.GetText("\"{0}\" does not exist. Please verify that you have entered the correct directory name.", new object[] { this.mwfFileView.CurrentRealFolder }), this.openSaveButton.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				if (this.InitialDirectory.Length == 0 || !Directory.Exists(this.InitialDirectory))
				{
					this.mwfFileView.ChangeDirectory(null, this.lastFolder, this.CustomFilter);
					return;
				}
				this.mwfFileView.ChangeDirectory(null, this.InitialDirectory, this.CustomFilter);
				return;
			}
			else
			{
				if (this.restoreDirectory)
				{
					this.lastFolder = this.restoreDirectoryString;
				}
				else
				{
					this.lastFolder = this.mwfFileView.CurrentFolder;
				}
				this.filterIndex = this.fileTypeComboBox.SelectedIndex + 1;
				CancelEventArgs cancelEventArgs = new CancelEventArgs();
				this.OnFileOk(cancelEventArgs);
				if (cancelEventArgs.Cancel)
				{
					return;
				}
				this.CleanupOnClose();
				this.form.DialogResult = DialogResult.OK;
			}
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00014FA8 File Offset: 0x000131A8
		private bool AddFilterExtension(string fileName)
		{
			if (this.fileDialogType != FileDialog.FileDialogType.OpenFileDialog)
			{
				return true;
			}
			if (this.DefaultExt.Length == 0)
			{
				return true;
			}
			if (this.checkFileExists)
			{
				return !File.Exists(fileName + "." + this.DefaultExt);
			}
			return !File.Exists(fileName);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00014FFC File Offset: 0x000131FC
		private string GetExtension(string fileName)
		{
			string text = string.Empty;
			if (this.fileFilter == null || this.fileTypeComboBox.SelectedIndex == -1)
			{
				return text;
			}
			FilterStruct filterStruct = (FilterStruct)this.fileFilter.FilterArrayList[this.fileTypeComboBox.SelectedIndex];
			for (int i = 0; i < filterStruct.filters.Count; i++)
			{
				string text2 = filterStruct.filters[i];
				if (text2.StartsWith("*"))
				{
					text2 = text2.Remove(0, 1);
				}
				if (text2.IndexOf('*') == -1)
				{
					if (!this.supportMultiDottedExtensions)
					{
						int num = text2.LastIndexOf('.');
						if (num > 0 && text2.LastIndexOf('.', num - 1) != -1)
						{
							text2 = text2.Remove(0, num);
						}
					}
					if (!this.checkFileExists)
					{
						text = text2;
						break;
					}
					if (this.fileDialogType == FileDialog.FileDialogType.SaveFileDialog && this.DefaultExt.Length > 0)
					{
						text = text2;
						break;
					}
					if (File.Exists(fileName + text2))
					{
						text = text2;
						break;
					}
					if (this.fileDialogType == FileDialog.FileDialogType.SaveFileDialog && this.DefaultExt.Length > 0)
					{
						text = text2;
						break;
					}
				}
			}
			return text;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0001511A File Offset: 0x0001331A
		private void OnClickCancelButton(object sender, EventArgs e)
		{
			if (this.restoreDirectory)
			{
				this.mwfFileView.CurrentFolder = this.restoreDirectoryString;
			}
			this.CleanupOnClose();
			this.form.DialogResult = DialogResult.Cancel;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00015147 File Offset: 0x00013347
		private void OnClickHelpButton(object sender, EventArgs e)
		{
			this.OnHelpRequest(e);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00015150 File Offset: 0x00013350
		private void OnClickSmallButtonToolBar(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == this.upToolBarButton)
			{
				this.mwfFileView.OneDirUp(this.CustomFilter);
				return;
			}
			if (e.Button == this.backToolBarButton)
			{
				this.mwfFileView.PopDir(this.CustomFilter);
				return;
			}
			if (e.Button == this.newdirToolBarButton)
			{
				this.mwfFileView.CreateNewFolder();
			}
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000151B6 File Offset: 0x000133B6
		private void OnSelectedIndexChangedFileTypeComboBox(object sender, EventArgs e)
		{
			if (this.do_not_call_OnSelectedIndexChangedFileTypeComboBox)
			{
				this.do_not_call_OnSelectedIndexChangedFileTypeComboBox = false;
				return;
			}
			this.UpdateRecentFiles();
			this.mwfFileView.FilterIndex = this.fileTypeComboBox.SelectedIndex + 1;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000151E6 File Offset: 0x000133E6
		private void OnSelectedFileChangedFileView(object sender, EventArgs e)
		{
			this.fileNameComboBox.Text = this.mwfFileView.CurrentFSEntry.Name;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00015204 File Offset: 0x00013404
		private void OnSelectedFilesChangedFileView(object sender, EventArgs e)
		{
			string selectedFilesString = this.mwfFileView.SelectedFilesString;
			if (selectedFilesString != null && selectedFilesString.Length != 0)
			{
				this.fileNameComboBox.Text = selectedFilesString;
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00015234 File Offset: 0x00013434
		private void OnForceDialogEndFileView(object sender, EventArgs e)
		{
			this.OnClickOpenSaveButton(this, EventArgs.Empty);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00015242 File Offset: 0x00013442
		private void OnDirectoryChangedDirComboBox(object sender, EventArgs e)
		{
			this.mwfFileView.ChangeDirectory(sender, this.dirComboBox.CurrentFolder, this.CustomFilter);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00015261 File Offset: 0x00013461
		private void OnDirectoryChangedPopupButtonPanel(object sender, EventArgs e)
		{
			this.mwfFileView.ChangeDirectory(sender, this.popupButtonPanel.CurrentFolder, this.CustomFilter);
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00015280 File Offset: 0x00013480
		private void OnCheckCheckChanged(object sender, EventArgs e)
		{
			this.ReadOnlyChecked = this.readonlyCheckBox.Checked;
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00015293 File Offset: 0x00013493
		private void OnFileDialogFormClosed(object sender, FormClosedEventArgs e)
		{
			this.HandleFormClosedEvent(sender);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x0001529C File Offset: 0x0001349C
		private void OnColumnClickFileView(object sender, ColumnClickEventArgs e)
		{
			if (this.file_view_comparer == null)
			{
				this.file_view_comparer = new MwfFileViewItemComparer(true);
			}
			this.file_view_comparer.ColumnIndex = e.Column;
			this.file_view_comparer.Ascendent = !this.file_view_comparer.Ascendent;
			if (this.mwfFileView.ListViewItemSorter == null)
			{
				this.mwfFileView.ListViewItemSorter = this.file_view_comparer;
				return;
			}
			this.mwfFileView.Sort();
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00015311 File Offset: 0x00013511
		private void HandleFormClosedEvent(object sender)
		{
			if (!this.disable_form_closed_event)
			{
				this.OnClickCancelButton(sender, EventArgs.Empty);
			}
			this.disable_form_closed_event = false;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x00015330 File Offset: 0x00013530
		private void UpdateFilters()
		{
			if (this.fileFilter == null)
			{
				this.fileFilter = new FileFilter();
			}
			ArrayList filterArrayList = this.fileFilter.FilterArrayList;
			this.fileTypeComboBox.BeginUpdate();
			this.fileTypeComboBox.Items.Clear();
			foreach (object obj in filterArrayList)
			{
				FilterStruct filterStruct = (FilterStruct)obj;
				this.fileTypeComboBox.Items.Add(filterStruct.filterName);
			}
			this.fileTypeComboBox.EndUpdate();
			this.mwfFileView.FilterArrayList = filterArrayList;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000153E8 File Offset: 0x000135E8
		private void UpdateRecentFiles()
		{
			this.fileNameComboBox.Items.Clear();
			if (this.configFileNames != null)
			{
				foreach (string text in this.configFileNames)
				{
					if (text != null && text.Trim().Length != 0)
					{
						if (this.fileNameComboBox.Items.Count >= FileDialog.MaxFileNameItems)
						{
							break;
						}
						this.fileNameComboBox.Items.Add(text);
					}
				}
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00015460 File Offset: 0x00013660
		private void ResizeAndRelocateForHelpOrReadOnly()
		{
			this.form.SuspendLayout();
			int num = this.form.Size.Width - this.form.MinimumSize.Width;
			int num2 = this.form.Size.Height - this.form.MinimumSize.Height;
			if (!this.ShowHelp && !this.ShowReadOnly)
			{
				num2 += 29;
			}
			this.mwfFileView.Size = new Size(450 + num, 254 + num2);
			this.fileNameLabel.Location = new Point(101, 298 + num2);
			this.fileNameComboBox.Location = new Point(195, 298 + num2);
			this.fileTypeLabel.Location = new Point(101, 326 + num2);
			this.fileTypeComboBox.Location = new Point(195, 326 + num2);
			this.openSaveButton.Location = new Point(474 + num, 298 + num2);
			this.cancelButton.Location = new Point(474 + num, 324 + num2);
			this.helpButton.Location = new Point(474 + num, 353 + num2);
			this.readonlyCheckBox.Location = new Point(195, 350 + num2);
			this.helpButton.Visible = this.ShowHelp;
			this.readonlyCheckBox.Visible = this.ShowReadOnly;
			this.form.ResumeLayout();
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00015608 File Offset: 0x00013808
		private void WriteConfigValues()
		{
			MWFConfig.SetValue("FileDialog", "Width", this.form.ClientSize.Width);
			MWFConfig.SetValue("FileDialog", "Height", this.form.ClientSize.Height);
			MWFConfig.SetValue("FileDialog", "X", this.form.Location.X);
			MWFConfig.SetValue("FileDialog", "Y", this.form.Location.Y);
			MWFConfig.SetValue("FileDialog", "LastFolder", this.lastFolder);
			string[] array = new string[this.fileNameComboBox.Items.Count];
			this.fileNameComboBox.Items.CopyTo(array, 0);
			MWFConfig.SetValue("FileDialog", "FileNames", array);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00015700 File Offset: 0x00013900
		private void ReadConfigValues()
		{
			this.lastFolder = (string)MWFConfig.GetValue("FileDialog", "LastFolder");
			if (this.lastFolder != null && this.lastFolder.IndexOf("://") == -1 && !Directory.Exists(this.lastFolder))
			{
				this.lastFolder = MWFVFS.DesktopPrefix;
			}
			if (this.InitialDirectory.Length > 0 && Directory.Exists(this.InitialDirectory))
			{
				this.lastFolder = this.InitialDirectory;
			}
			else if (this.lastFolder == null || this.lastFolder.Length == 0)
			{
				this.lastFolder = Environment.CurrentDirectory;
			}
			if (this.RestoreDirectory)
			{
				this.restoreDirectoryString = this.lastFolder;
			}
		}

		/// <summary>Owns the <see cref="E:System.Windows.Forms.FileDialog.FileOk" /> event.</summary>
		// Token: 0x04000328 RID: 808
		protected static readonly object EventFileOk = new object();

		// Token: 0x04000329 RID: 809
		private static int MaxFileNameItems = 10;

		// Token: 0x0400032A RID: 810
		private bool addExtension = true;

		// Token: 0x0400032B RID: 811
		private bool checkFileExists;

		// Token: 0x0400032C RID: 812
		private bool checkPathExists = true;

		// Token: 0x0400032D RID: 813
		private string defaultExt;

		// Token: 0x0400032E RID: 814
		private bool dereferenceLinks = true;

		// Token: 0x0400032F RID: 815
		private string[] fileNames;

		// Token: 0x04000330 RID: 816
		private string filter = "";

		// Token: 0x04000331 RID: 817
		private int filterIndex = 1;

		// Token: 0x04000332 RID: 818
		private string initialDirectory;

		// Token: 0x04000333 RID: 819
		private bool restoreDirectory;

		// Token: 0x04000334 RID: 820
		private bool showHelp;

		// Token: 0x04000335 RID: 821
		private string title;

		// Token: 0x04000336 RID: 822
		private bool validateNames = true;

		// Token: 0x04000337 RID: 823
		private bool auto_upgrade_enable = true;

		// Token: 0x04000338 RID: 824
		private FileDialogCustomPlacesCollection custom_places;

		// Token: 0x04000339 RID: 825
		private bool supportMultiDottedExtensions;

		// Token: 0x0400033A RID: 826
		private bool checkForIllegalChars = true;

		// Token: 0x0400033B RID: 827
		private Button cancelButton;

		// Token: 0x0400033C RID: 828
		private ToolBarButton upToolBarButton;

		// Token: 0x0400033D RID: 829
		private PopupButtonPanel popupButtonPanel;

		// Token: 0x0400033E RID: 830
		private Button openSaveButton;

		// Token: 0x0400033F RID: 831
		private Button helpButton;

		// Token: 0x04000340 RID: 832
		private Label fileTypeLabel;

		// Token: 0x04000341 RID: 833
		private ToolBarButton menueToolBarButton;

		// Token: 0x04000342 RID: 834
		private ContextMenu menueToolBarButtonContextMenu;

		// Token: 0x04000343 RID: 835
		private ToolBar smallButtonToolBar;

		// Token: 0x04000344 RID: 836
		private DirComboBox dirComboBox;

		// Token: 0x04000345 RID: 837
		private ComboBox fileNameComboBox;

		// Token: 0x04000346 RID: 838
		private Label fileNameLabel;

		// Token: 0x04000347 RID: 839
		private MWFFileView mwfFileView;

		// Token: 0x04000348 RID: 840
		private MwfFileViewItemComparer file_view_comparer;

		// Token: 0x04000349 RID: 841
		private Label searchSaveLabel;

		// Token: 0x0400034A RID: 842
		private ToolBarButton newdirToolBarButton;

		// Token: 0x0400034B RID: 843
		private ToolBarButton backToolBarButton;

		// Token: 0x0400034C RID: 844
		private ComboBox fileTypeComboBox;

		// Token: 0x0400034D RID: 845
		private ImageList imageListTopToolbar;

		// Token: 0x0400034E RID: 846
		private CheckBox readonlyCheckBox;

		// Token: 0x0400034F RID: 847
		private bool multiSelect;

		// Token: 0x04000350 RID: 848
		private string restoreDirectoryString = string.Empty;

		// Token: 0x04000351 RID: 849
		internal FileDialog.FileDialogType fileDialogType;

		// Token: 0x04000352 RID: 850
		private bool do_not_call_OnSelectedIndexChangedFileTypeComboBox;

		// Token: 0x04000353 RID: 851
		private bool showReadOnly;

		// Token: 0x04000354 RID: 852
		private bool readOnlyChecked;

		// Token: 0x04000355 RID: 853
		internal bool createPrompt;

		// Token: 0x04000356 RID: 854
		internal bool overwritePrompt = true;

		// Token: 0x04000357 RID: 855
		private FileFilter fileFilter;

		// Token: 0x04000358 RID: 856
		private string[] configFileNames;

		// Token: 0x04000359 RID: 857
		private string lastFolder = string.Empty;

		// Token: 0x0400035A RID: 858
		private MWFVFS vfs;

		// Token: 0x0400035B RID: 859
		private readonly char[] wildcard_chars = new char[] { '*', '?' };

		// Token: 0x0400035C RID: 860
		private bool disable_form_closed_event;

		// Token: 0x02000081 RID: 129
		internal enum FileDialogType
		{
			// Token: 0x0400035E RID: 862
			OpenFileDialog,
			// Token: 0x0400035F RID: 863
			SaveFileDialog
		}

		// Token: 0x02000082 RID: 130
		private class FileNamesTokenizer
		{
			// Token: 0x06000567 RID: 1383 RVA: 0x000157CA File Offset: 0x000139CA
			public FileNamesTokenizer(string text, bool allowMultiple)
			{
				this._text = text;
				this._position = 0;
				this._tokenType = FileDialog.TokenType.BOF;
				this._allowMultiple = allowMultiple;
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x06000568 RID: 1384 RVA: 0x000157EE File Offset: 0x000139EE
			public FileDialog.TokenType CurrentToken
			{
				get
				{
					return this._tokenType;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000569 RID: 1385 RVA: 0x000157F6 File Offset: 0x000139F6
			public string TokenText
			{
				get
				{
					return this._tokenText;
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x0600056A RID: 1386 RVA: 0x000157FE File Offset: 0x000139FE
			public bool AllowMultiple
			{
				get
				{
					return this._allowMultiple;
				}
			}

			// Token: 0x0600056B RID: 1387 RVA: 0x00015808 File Offset: 0x00013A08
			private int ReadChar()
			{
				if (this._position < this._text.Length)
				{
					string text = this._text;
					int position = this._position;
					this._position = position + 1;
					return (int)text[position];
				}
				return -1;
			}

			// Token: 0x0600056C RID: 1388 RVA: 0x00015846 File Offset: 0x00013A46
			private int PeekChar()
			{
				if (this._position < this._text.Length)
				{
					return (int)this._text[this._position];
				}
				return -1;
			}

			// Token: 0x0600056D RID: 1389 RVA: 0x00015870 File Offset: 0x00013A70
			private void SkipWhitespaceAndQuotes()
			{
				int num;
				while ((num = this.PeekChar()) != -1 && ((ushort)num == 34 || char.IsWhiteSpace((char)num)))
				{
					this.ReadChar();
				}
			}

			// Token: 0x0600056E RID: 1390 RVA: 0x000158A0 File Offset: 0x00013AA0
			public void GetNextFile()
			{
				if (this._tokenType == FileDialog.TokenType.EOF)
				{
					throw new Exception("");
				}
				this.SkipWhitespaceAndQuotes();
				if (this.PeekChar() == -1)
				{
					this._tokenType = FileDialog.TokenType.EOF;
					return;
				}
				this._tokenType = FileDialog.TokenType.FileName;
				StringBuilder stringBuilder = new StringBuilder();
				int num;
				while ((num = this.PeekChar()) != -1)
				{
					if ((ushort)num == 34)
					{
						this.ReadChar();
						if (this.AllowMultiple)
						{
							break;
						}
						int position = this._position;
						this.SkipWhitespaceAndQuotes();
						if (this.PeekChar() == -1)
						{
							break;
						}
						this._position = position + 1;
						stringBuilder.Append((char)num);
					}
					else
					{
						stringBuilder.Append((char)this.ReadChar());
					}
				}
				this._tokenText = stringBuilder.ToString();
			}

			// Token: 0x04000360 RID: 864
			private readonly bool _allowMultiple;

			// Token: 0x04000361 RID: 865
			private int _position;

			// Token: 0x04000362 RID: 866
			private readonly string _text;

			// Token: 0x04000363 RID: 867
			private FileDialog.TokenType _tokenType;

			// Token: 0x04000364 RID: 868
			private string _tokenText;
		}

		// Token: 0x02000083 RID: 131
		internal enum TokenType
		{
			// Token: 0x04000366 RID: 870
			BOF,
			// Token: 0x04000367 RID: 871
			EOF,
			// Token: 0x04000368 RID: 872
			FileName
		}
	}
}
