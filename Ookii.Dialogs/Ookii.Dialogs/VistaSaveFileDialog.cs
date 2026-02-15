using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs
{
	// Token: 0x02000050 RID: 80
	[Designer("System.Windows.Forms.Design.SaveFileDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxBitmap(typeof(SaveFileDialog), "SaveFileDialog.bmp")]
	[Description("Prompts the user to open a file.")]
	public class VistaSaveFileDialog : VistaFileDialog
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00009AD6 File Offset: 0x00007CD6
		public VistaSaveFileDialog()
			: this(false)
		{
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009AE4 File Offset: 0x00007CE4
		public VistaSaveFileDialog(bool forceDownlevel)
		{
			bool flag = forceDownlevel || !VistaFileDialog.IsVistaFileDialogSupported;
			if (flag)
			{
				base.DownlevelDialog = new SaveFileDialog();
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00009B18 File Offset: 0x00007D18
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00009B58 File Offset: 0x00007D58
		[DefaultValue(false)]
		[Category("Behavior")]
		[Description("A value indicating whether the dialog box prompts the user for permission to create a file if the user specifies a file that does not exist.")]
		public bool CreatePrompt
		{
			get
			{
				bool flag = base.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = ((SaveFileDialog)base.DownlevelDialog).CreatePrompt;
				}
				else
				{
					flag2 = base.GetOption(NativeMethods.FOS.FOS_CREATEPROMPT);
				}
				return flag2;
			}
			set
			{
				bool flag = base.DownlevelDialog != null;
				if (flag)
				{
					((SaveFileDialog)base.DownlevelDialog).CreatePrompt = value;
				}
				else
				{
					base.SetOption(NativeMethods.FOS.FOS_CREATEPROMPT, value);
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00009B94 File Offset: 0x00007D94
		// (set) Token: 0x06000230 RID: 560 RVA: 0x00009BD0 File Offset: 0x00007DD0
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("A value indicating whether the Save As dialog box displays a warning if the user specifies a file name that already exists.")]
		public bool OverwritePrompt
		{
			get
			{
				bool flag = base.DownlevelDialog != null;
				bool flag2;
				if (flag)
				{
					flag2 = ((SaveFileDialog)base.DownlevelDialog).OverwritePrompt;
				}
				else
				{
					flag2 = base.GetOption(NativeMethods.FOS.FOS_OVERWRITEPROMPT);
				}
				return flag2;
			}
			set
			{
				bool flag = base.DownlevelDialog != null;
				if (flag)
				{
					((SaveFileDialog)base.DownlevelDialog).OverwritePrompt = value;
				}
				else
				{
					base.SetOption(NativeMethods.FOS.FOS_OVERWRITEPROMPT, value);
				}
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009C08 File Offset: 0x00007E08
		public override void Reset()
		{
			base.Reset();
			bool flag = base.DownlevelDialog == null;
			if (flag)
			{
				this.OverwritePrompt = true;
			}
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00009C34 File Offset: 0x00007E34
		public Stream OpenFile()
		{
			bool flag = base.DownlevelDialog != null;
			Stream stream;
			if (flag)
			{
				stream = ((SaveFileDialog)base.DownlevelDialog).OpenFile();
			}
			else
			{
				string fileName = base.FileName;
				bool flag2 = string.IsNullOrEmpty(fileName);
				if (flag2)
				{
					throw new ArgumentNullException("FileName");
				}
				stream = new FileStream(fileName, 2, 3);
			}
			return stream;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009C8C File Offset: 0x00007E8C
		protected override void OnFileOk(CancelEventArgs e)
		{
			bool flag = base.DownlevelDialog == null;
			if (flag)
			{
				bool flag2 = this.CheckFileExists && !File.Exists(base.FileName);
				if (flag2)
				{
					base.PromptUser(ComDlgResources.FormatString(ComDlgResources.ComDlgResourceId.FileNotFound, new string[] { Path.GetFileName(base.FileName) }), 0, 48);
					e.Cancel = true;
					return;
				}
				bool flag3 = this.CreatePrompt && !File.Exists(base.FileName);
				if (flag3)
				{
					bool flag4 = !base.PromptUser(ComDlgResources.FormatString(ComDlgResources.ComDlgResourceId.CreatePrompt, new string[] { Path.GetFileName(base.FileName) }), 4, 48);
					if (flag4)
					{
						e.Cancel = true;
						return;
					}
				}
			}
			base.OnFileOk(e);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009D5C File Offset: 0x00007F5C
		internal override IFileDialog CreateFileDialog()
		{
			return (NativeFileSaveDialog)new FileSaveDialogRCW();
		}
	}
}
