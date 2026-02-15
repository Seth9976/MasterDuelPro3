using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	// Token: 0x0200004E RID: 78
	[DefaultEvent("HelpRequest")]
	[Designer("System.Windows.Forms.Design.FolderBrowserDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("SelectedPath")]
	[Description("Prompts the user to select a folder.")]
	public sealed class VistaFolderBrowserDialog : CommonDialog
	{
		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000209 RID: 521 RVA: 0x000092CA File Offset: 0x000074CA
		// (remove) Token: 0x0600020A RID: 522 RVA: 0x000092D5 File Offset: 0x000074D5
		[Browsable(false)]
		[EditorBrowsable(1)]
		public event EventHandler HelpRequest
		{
			add
			{
				base.HelpRequest += value;
			}
			remove
			{
				base.HelpRequest -= value;
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000092E0 File Offset: 0x000074E0
		public VistaFolderBrowserDialog()
		{
			bool flag = !VistaFolderBrowserDialog.IsVistaFolderDialogSupported;
			if (flag)
			{
				this._downlevelDialog = new FolderBrowserDialog();
			}
			else
			{
				this.Reset();
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00009318 File Offset: 0x00007518
		[Browsable(false)]
		public static bool IsVistaFolderDialogSupported
		{
			get
			{
				return NativeMethods.IsWindowsVistaOrLater;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00009330 File Offset: 0x00007530
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00009364 File Offset: 0x00007564
		[Category("Folder Browsing")]
		[DefaultValue("")]
		[Localizable(true)]
		[Browsable(true)]
		[Description("The descriptive text displayed above the tree view control in the dialog box, or below the list view control in the Vista style dialog.")]
		public string Description
		{
			get
			{
				bool flag = this._downlevelDialog != null;
				string text;
				if (flag)
				{
					text = this._downlevelDialog.Description;
				}
				else
				{
					text = this._description;
				}
				return text;
			}
			set
			{
				bool flag = this._downlevelDialog != null;
				if (flag)
				{
					this._downlevelDialog.Description = value;
				}
				else
				{
					this._description = value ?? string.Empty;
				}
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000093A0 File Offset: 0x000075A0
		// (set) Token: 0x06000210 RID: 528 RVA: 0x000093D4 File Offset: 0x000075D4
		[Localizable(false)]
		[Description("The root folder where the browsing starts from. This property has no effect if the Vista style dialog is used.")]
		[Category("Folder Browsing")]
		[Browsable(true)]
		[DefaultValue(typeof(Environment.SpecialFolder), "Desktop")]
		public Environment.SpecialFolder RootFolder
		{
			get
			{
				bool flag = this._downlevelDialog != null;
				Environment.SpecialFolder specialFolder;
				if (flag)
				{
					specialFolder = this._downlevelDialog.RootFolder;
				}
				else
				{
					specialFolder = this._rootFolder;
				}
				return specialFolder;
			}
			set
			{
				bool flag = this._downlevelDialog != null;
				if (flag)
				{
					this._downlevelDialog.RootFolder = value;
				}
				else
				{
					this._rootFolder = value;
				}
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00009408 File Offset: 0x00007608
		// (set) Token: 0x06000212 RID: 530 RVA: 0x0000943C File Offset: 0x0000763C
		[Browsable(true)]
		[Editor("System.Windows.Forms.Design.SelectedPathEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Description("The path selected by the user.")]
		[DefaultValue("")]
		[Localizable(true)]
		[Category("Folder Browsing")]
		public string SelectedPath
		{
			get
			{
				bool flag = this._downlevelDialog != null;
				string text;
				if (flag)
				{
					text = this._downlevelDialog.SelectedPath;
				}
				else
				{
					text = this._selectedPath;
				}
				return text;
			}
			set
			{
				bool flag = this._downlevelDialog != null;
				if (flag)
				{
					this._downlevelDialog.SelectedPath = value;
				}
				else
				{
					this._selectedPath = value ?? string.Empty;
				}
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00009478 File Offset: 0x00007678
		// (set) Token: 0x06000214 RID: 532 RVA: 0x000094AC File Offset: 0x000076AC
		[Browsable(true)]
		[Localizable(false)]
		[Description("A value indicating whether the New Folder button appears in the folder browser dialog box. This property has no effect if the Vista style dialog is used; in that case, the New Folder button is always shown.")]
		[DefaultValue(true)]
		[Category("Folder Browsing")]
		public bool ShowNewFolderButton
		{
			get
			{
				bool flag = this._downlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = this._downlevelDialog.ShowNewFolderButton;
				}
				else
				{
					flag2 = this._showNewFolderButton;
				}
				return flag2;
			}
			set
			{
				bool flag = this._downlevelDialog != null;
				if (flag)
				{
					this._downlevelDialog.ShowNewFolderButton = value;
				}
				else
				{
					this._showNewFolderButton = value;
				}
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000094E0 File Offset: 0x000076E0
		// (set) Token: 0x06000216 RID: 534 RVA: 0x000094F8 File Offset: 0x000076F8
		[Category("Folder Browsing")]
		[DefaultValue(false)]
		[Description("A value that indicates whether to use the value of the Description property as the dialog title for Vista style dialogs. This property has no effect on old style dialogs.")]
		public bool UseDescriptionForTitle
		{
			get
			{
				return this._useDescriptionForTitle;
			}
			set
			{
				this._useDescriptionForTitle = value;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00009502 File Offset: 0x00007702
		public override void Reset()
		{
			this._description = string.Empty;
			this._useDescriptionForTitle = false;
			this._selectedPath = string.Empty;
			this._rootFolder = 0;
			this._showNewFolderButton = true;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009530 File Offset: 0x00007730
		protected override bool RunDialog(IntPtr hwndOwner)
		{
			bool flag = this._downlevelDialog != null;
			bool flag2;
			if (flag)
			{
				flag2 = this._downlevelDialog.ShowDialog((hwndOwner == IntPtr.Zero) ? null : new WindowHandleWrapper(hwndOwner)) == 1;
			}
			else
			{
				IFileDialog fileDialog = null;
				try
				{
					fileDialog = (NativeFileOpenDialog)new FileOpenDialogRCW();
					this.SetDialogProperties(fileDialog);
					int num = fileDialog.Show(hwndOwner);
					bool flag3 = num < 0;
					if (flag3)
					{
						bool flag4 = num == -2147023673;
						if (!flag4)
						{
							throw Marshal.GetExceptionForHR(num);
						}
						flag2 = false;
					}
					else
					{
						this.GetResult(fileDialog);
						flag2 = true;
					}
				}
				catch
				{
					flag2 = false;
				}
				finally
				{
					bool flag5 = fileDialog != null;
					if (flag5)
					{
						Marshal.FinalReleaseComObject(fileDialog);
					}
				}
			}
			return flag2;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000095F8 File Offset: 0x000077F8
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = disposing && this._downlevelDialog != null;
				if (flag)
				{
					this._downlevelDialog.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00009644 File Offset: 0x00007844
		private void SetDialogProperties(IFileDialog dialog)
		{
			bool flag = !string.IsNullOrEmpty(this._description);
			if (flag)
			{
				bool useDescriptionForTitle = this._useDescriptionForTitle;
				if (useDescriptionForTitle)
				{
					dialog.SetTitle(this._description);
				}
				else
				{
					IFileDialogCustomize fileDialogCustomize = (IFileDialogCustomize)dialog;
					fileDialogCustomize.AddText(0, this._description);
				}
			}
			dialog.SetOptions(NativeMethods.FOS.FOS_PICKFOLDERS | NativeMethods.FOS.FOS_FORCEFILESYSTEM | NativeMethods.FOS.FOS_FILEMUSTEXIST);
			bool flag2 = !string.IsNullOrEmpty(this._selectedPath);
			if (flag2)
			{
				string directoryName = Path.GetDirectoryName(this._selectedPath);
				bool flag3 = directoryName == null || !Directory.Exists(directoryName);
				if (flag3)
				{
					dialog.SetFileName(this._selectedPath);
				}
				else
				{
					string fileName = Path.GetFileName(this._selectedPath);
					dialog.SetFolder(NativeMethods.CreateItemFromParsingName(directoryName));
					dialog.SetFileName(fileName);
				}
			}
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009714 File Offset: 0x00007914
		private void GetResult(IFileDialog dialog)
		{
			IShellItem shellItem;
			dialog.GetResult(out shellItem);
			shellItem.GetDisplayName((NativeMethods.SIGDN)2147844096U, out this._selectedPath);
		}

		// Token: 0x04000209 RID: 521
		private FolderBrowserDialog _downlevelDialog;

		// Token: 0x0400020A RID: 522
		private string _description;

		// Token: 0x0400020B RID: 523
		private bool _useDescriptionForTitle;

		// Token: 0x0400020C RID: 524
		private string _selectedPath;

		// Token: 0x0400020D RID: 525
		private Environment.SpecialFolder _rootFolder;

		// Token: 0x0400020E RID: 526
		private bool _showNewFolderButton;
	}
}
