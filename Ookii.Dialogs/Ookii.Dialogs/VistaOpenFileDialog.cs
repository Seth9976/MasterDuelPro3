using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	// Token: 0x0200004F RID: 79
	[ToolboxBitmap(typeof(OpenFileDialog), "OpenFileDialog.bmp")]
	[Description("Prompts the user to open a file.")]
	public class VistaOpenFileDialog : VistaFileDialog
	{
		// Token: 0x0600021C RID: 540 RVA: 0x0000973D File Offset: 0x0000793D
		public VistaOpenFileDialog()
			: this(false)
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009748 File Offset: 0x00007948
		public VistaOpenFileDialog(bool forceDownlevel)
		{
			bool flag = forceDownlevel || !VistaFileDialog.IsVistaFileDialogSupported;
			if (flag)
			{
				base.DownlevelDialog = new OpenFileDialog();
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000977C File Offset: 0x0000797C
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00009794 File Offset: 0x00007994
		[DefaultValue(true)]
		[Description("A value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.")]
		public override bool CheckFileExists
		{
			get
			{
				return base.CheckFileExists;
			}
			set
			{
				base.CheckFileExists = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000097A0 File Offset: 0x000079A0
		// (set) Token: 0x06000221 RID: 545 RVA: 0x000097E0 File Offset: 0x000079E0
		[Description("A value indicating whether the dialog box allows multiple files to be selected.")]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool Multiselect
		{
			get
			{
				bool flag = base.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = ((OpenFileDialog)base.DownlevelDialog).Multiselect;
				}
				else
				{
					flag2 = base.GetOption(NativeMethods.FOS.FOS_ALLOWMULTISELECT);
				}
				return flag2;
			}
			set
			{
				bool flag = base.DownlevelDialog != null;
				if (flag)
				{
					((OpenFileDialog)base.DownlevelDialog).Multiselect = value;
				}
				base.SetOption(NativeMethods.FOS.FOS_ALLOWMULTISELECT, value);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000981C File Offset: 0x00007A1C
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00009854 File Offset: 0x00007A54
		[Description("A value indicating whether the dialog box contains a read-only check box.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool ShowReadOnly
		{
			get
			{
				bool flag = base.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = ((OpenFileDialog)base.DownlevelDialog).ShowReadOnly;
				}
				else
				{
					flag2 = this._showReadOnly;
				}
				return flag2;
			}
			set
			{
				bool flag = base.DownlevelDialog != null;
				if (flag)
				{
					((OpenFileDialog)base.DownlevelDialog).ShowReadOnly = value;
				}
				else
				{
					this._showReadOnly = value;
				}
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000988C File Offset: 0x00007A8C
		// (set) Token: 0x06000225 RID: 549 RVA: 0x000098C4 File Offset: 0x00007AC4
		[DefaultValue(false)]
		[Description("A value indicating whether the read-only check box is selected.")]
		[Category("Behavior")]
		public bool ReadOnlyChecked
		{
			get
			{
				bool flag = base.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = ((OpenFileDialog)base.DownlevelDialog).ReadOnlyChecked;
				}
				else
				{
					flag2 = this._readOnlyChecked;
				}
				return flag2;
			}
			set
			{
				bool flag = base.DownlevelDialog != null;
				if (flag)
				{
					((OpenFileDialog)base.DownlevelDialog).ReadOnlyChecked = value;
				}
				else
				{
					this._readOnlyChecked = value;
				}
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000098FC File Offset: 0x00007AFC
		public override void Reset()
		{
			base.Reset();
			bool flag = base.DownlevelDialog == null;
			if (flag)
			{
				this.CheckFileExists = true;
				this._showReadOnly = false;
				this._readOnlyChecked = false;
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009938 File Offset: 0x00007B38
		public Stream OpenFile()
		{
			bool flag = base.DownlevelDialog != null;
			Stream stream;
			if (flag)
			{
				stream = ((OpenFileDialog)base.DownlevelDialog).OpenFile();
			}
			else
			{
				string fileName = base.FileName;
				bool flag2 = string.IsNullOrEmpty(fileName);
				if (flag2)
				{
					throw new ArgumentNullException("FileName");
				}
				stream = new FileStream(fileName, 3, 1);
			}
			return stream;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009990 File Offset: 0x00007B90
		internal override IFileDialog CreateFileDialog()
		{
			return (NativeFileOpenDialog)new FileOpenDialogRCW();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000099AC File Offset: 0x00007BAC
		internal override void SetDialogProperties(IFileDialog dialog)
		{
			base.SetDialogProperties(dialog);
			bool showReadOnly = this._showReadOnly;
			if (showReadOnly)
			{
				IFileDialogCustomize fileDialogCustomize = (IFileDialogCustomize)dialog;
				fileDialogCustomize.EnableOpenDropDown(16386);
				fileDialogCustomize.AddControlItem(16386, 16387, ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.OpenButton));
				fileDialogCustomize.AddControlItem(16386, 16388, ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.ReadOnly));
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00009A18 File Offset: 0x00007C18
		internal override void GetResult(IFileDialog dialog)
		{
			bool multiselect = this.Multiselect;
			if (multiselect)
			{
				IShellItemArray shellItemArray;
				((IFileOpenDialog)dialog).GetResults(out shellItemArray);
				uint num;
				shellItemArray.GetCount(out num);
				string[] array = new string[num];
				for (uint num2 = 0U; num2 < num; num2 += 1U)
				{
					IShellItem shellItem;
					shellItemArray.GetItemAt(num2, out shellItem);
					string text;
					shellItem.GetDisplayName((NativeMethods.SIGDN)2147844096U, out text);
					array[(int)num2] = text;
				}
				base.FileNamesInternal = array;
			}
			else
			{
				base.FileNamesInternal = null;
			}
			bool showReadOnly = this.ShowReadOnly;
			if (showReadOnly)
			{
				IFileDialogCustomize fileDialogCustomize = (IFileDialogCustomize)dialog;
				int num3;
				fileDialogCustomize.GetSelectedControlItem(16386, out num3);
				this._readOnlyChecked = num3 == 16388;
			}
			base.GetResult(dialog);
		}

		// Token: 0x0400020F RID: 527
		private bool _showReadOnly;

		// Token: 0x04000210 RID: 528
		private bool _readOnlyChecked;

		// Token: 0x04000211 RID: 529
		private const int _openDropDownId = 16386;

		// Token: 0x04000212 RID: 530
		private const int _openItemId = 16387;

		// Token: 0x04000213 RID: 531
		private const int _readOnlyItemId = 16388;
	}
}
