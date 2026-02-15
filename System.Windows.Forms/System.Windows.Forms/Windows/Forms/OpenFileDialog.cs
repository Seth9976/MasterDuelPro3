using System;
using System.ComponentModel;
using System.IO;

namespace System.Windows.Forms
{
	/// <summary>Prompts the user to open a file. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000158 RID: 344
	public sealed class OpenFileDialog : FileDialog
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.OpenFileDialog" /> class.</summary>
		// Token: 0x06000D58 RID: 3416 RVA: 0x0003AC9C File Offset: 0x00038E9C
		public OpenFileDialog()
		{
			this.form.SuspendLayout();
			this.form.Text = Locale.GetText("Open");
			this.CheckFileExists = true;
			base.OpenSaveButtonText = Locale.GetText("Open");
			base.SearchSaveLabel = Locale.GetText("Look in:");
			this.fileDialogType = FileDialog.FileDialogType.OpenFileDialog;
			this.form.ResumeLayout(false);
		}

		/// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist. </summary>
		/// <returns>true if the dialog box displays a warning when the user specifies a file name that does not exist; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0003AD09 File Offset: 0x00038F09
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x0003AD11 File Offset: 0x00038F11
		[DefaultValue(true)]
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

		/// <summary>Gets or sets a value indicating whether the dialog box allows multiple files to be selected. </summary>
		/// <returns>true if the dialog box allows multiple files to be selected together or concurrently; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0003AD1A File Offset: 0x00038F1A
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x0003AD22 File Offset: 0x00038F22
		[DefaultValue(false)]
		public bool Multiselect
		{
			get
			{
				return base.BMultiSelect;
			}
			set
			{
				base.BMultiSelect = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the read-only check box is selected. </summary>
		/// <returns>true if the read-only check box is selected; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0003AD2B File Offset: 0x00038F2B
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x0003AD33 File Offset: 0x00038F33
		[DefaultValue(false)]
		public new bool ReadOnlyChecked
		{
			get
			{
				return base.ReadOnlyChecked;
			}
			set
			{
				base.ReadOnlyChecked = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the dialog box contains a read-only check box. </summary>
		/// <returns>true if the dialog box contains a read-only check box; otherwise, false. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0003AD3C File Offset: 0x00038F3C
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x0003AD44 File Offset: 0x00038F44
		[DefaultValue(false)]
		public new bool ShowReadOnly
		{
			get
			{
				return base.ShowReadOnly;
			}
			set
			{
				base.ShowReadOnly = value;
			}
		}

		/// <summary>Opens the file selected by the user, with read-only permission. The file is specified by the <see cref="P:System.Windows.Forms.FileDialog.FileName" /> property. </summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> that specifies the read-only file selected by the user.</returns>
		/// <exception cref="T:System.ArgumentNullException">The file name is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D61 RID: 3425 RVA: 0x0003AD4D File Offset: 0x00038F4D
		public Stream OpenFile()
		{
			if (base.FileName.Length == 0)
			{
				throw new ArgumentNullException("OpenFile", "FileName is null");
			}
			return new FileStream(base.FileName, FileMode.Open, FileAccess.Read);
		}

		/// <summary>Resets all properties to their default values. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000D62 RID: 3426 RVA: 0x0003AD79 File Offset: 0x00038F79
		public override void Reset()
		{
			base.Reset();
			base.BMultiSelect = false;
			base.CheckFileExists = true;
			base.ReadOnlyChecked = false;
			base.ShowReadOnly = false;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x0003ADA0 File Offset: 0x00038FA0
		internal override string DialogTitle
		{
			get
			{
				string text = base.DialogTitle;
				if (text.Length == 0)
				{
					text = Locale.GetText("Open");
				}
				return text;
			}
		}
	}
}
