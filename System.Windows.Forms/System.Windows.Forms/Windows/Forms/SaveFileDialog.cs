using System;
using System.ComponentModel;
using System.IO;

namespace System.Windows.Forms
{
	/// <summary>Prompts the user to select a location for saving a file. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000177 RID: 375
	[Designer("System.Windows.Forms.Design.SaveFileDialogDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public sealed class SaveFileDialog : FileDialog
	{
		/// <summary>Initializes a new instance of this class.</summary>
		// Token: 0x06000E1E RID: 3614 RVA: 0x000408F0 File Offset: 0x0003EAF0
		public SaveFileDialog()
		{
			this.form.SuspendLayout();
			this.form.Text = Locale.GetText("Save As");
			base.FileTypeLabel = Locale.GetText("Save as type:");
			base.OpenSaveButtonText = Locale.GetText("Save");
			base.SearchSaveLabel = Locale.GetText("Save in:");
			this.fileDialogType = FileDialog.FileDialogType.SaveFileDialog;
			this.form.ResumeLayout(false);
		}

		/// <summary>Gets or sets a value indicating whether the dialog box prompts the user for permission to create a file if the user specifies a file that does not exist.</summary>
		/// <returns>true if the dialog box prompts the user before creating a file if the user specifies a file name that does not exist; false if the dialog box automatically creates the new file without prompting the user for permission. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0004096F File Offset: 0x0003EB6F
		// (set) Token: 0x06000E1F RID: 3615 RVA: 0x00040966 File Offset: 0x0003EB66
		[DefaultValue(false)]
		public bool CreatePrompt
		{
			get
			{
				return this.createPrompt;
			}
			set
			{
				this.createPrompt = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the Save As dialog box displays a warning if the user specifies a file name that already exists.</summary>
		/// <returns>true if the dialog box prompts the user before overwriting an existing file if the user specifies a file name that already exists; false if the dialog box automatically overwrites the existing file without prompting the user for permission. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00040980 File Offset: 0x0003EB80
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x00040977 File Offset: 0x0003EB77
		[DefaultValue(true)]
		public bool OverwritePrompt
		{
			get
			{
				return this.overwritePrompt;
			}
			set
			{
				this.overwritePrompt = value;
			}
		}

		/// <summary>Opens the file with read/write permission selected by the user.</summary>
		/// <returns>The read/write file selected by the user.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E23 RID: 3619 RVA: 0x00040988 File Offset: 0x0003EB88
		public Stream OpenFile()
		{
			if (base.FileName == null)
			{
				throw new ArgumentNullException("OpenFile", "FileName is null");
			}
			Stream stream;
			try
			{
				stream = new FileStream(base.FileName, FileMode.Create, FileAccess.ReadWrite);
			}
			catch (Exception)
			{
				stream = null;
			}
			return stream;
		}

		/// <summary>Resets all dialog box options to their default values.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E24 RID: 3620 RVA: 0x000409D4 File Offset: 0x0003EBD4
		public override void Reset()
		{
			base.Reset();
			this.overwritePrompt = true;
			this.createPrompt = false;
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000E25 RID: 3621 RVA: 0x000409EC File Offset: 0x0003EBEC
		internal override string DialogTitle
		{
			get
			{
				string text = base.DialogTitle;
				if (text.Length == 0)
				{
					text = Locale.GetText("Save As");
				}
				return text;
			}
		}
	}
}
